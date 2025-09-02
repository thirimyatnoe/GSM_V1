Imports DataAccess.SalesVolume
Imports DataAccess.SalesItem
Imports CommonInfo
Namespace SalesVolume
    Public Class SalesVolumeController
        Implements ISalesVolumeController
#Region "Private Members"

        Private _objSalesItemDA As ISalesItemDA
        Private _objSalesVolumeDA As ISalesVolumeDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISalesVolumeController = New SalesVolumeController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSalesVolumeDA = DataAccess.Factory.Instance.CreateSalesVolumeDA
            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub
#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesVolumeController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date, Optional ByVal CheckState As Boolean = False)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate
                .IsSolidVolume = CheckState
            End With
            _objSalesItemDA.UpdateSaleItemIsExit(objSaleItem)
        End Sub

        Public Function GetAllSalesVolume() As System.Data.DataTable Implements ISalesVolumeController.GetAllSalesVolume
            Return _objSalesVolumeDA.GetAllSalesVolume()
        End Function

        Public Function GetSalesVolumeHeaderByID(ByVal SalesVolumeHeaderID As String) As CommonInfo.SalesVolumeHeaderInfo Implements ISalesVolumeController.GetSalesVolumeHeaderByID
            Return _objSalesVolumeDA.GetSalesVolumeHeaderByID(SalesVolumeHeaderID)
        End Function

        Public Function GetSalesVolumeDetailByID(ByVal SalesVolumeHeaderID As String) As System.Data.DataTable Implements ISalesVolumeController.GetSalesVolumeDetailByID
            Return _objSalesVolumeDA.GetSalesVolumeDetailByID(SalesVolumeHeaderID)
        End Function

        Public Function SaveSalesVolume(ByVal obj As CommonInfo.SalesVolumeHeaderInfo, ByVal _dtSalesVolumeDetail As DataTable) As Boolean Implements ISalesVolumeController.SaveSalesVolume
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

            Dim bolRet As Boolean = True
            If obj.SalesVolumeID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.SaleVolumeStock.ToString)
                obj.SalesVolumeID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.SaleDate)

                'obj.SalesVolumeID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.SalesVolume, CommonInfo.EnumSetting.GenerateKeyType.SalesVolume.ToString, obj.SaleDate)
                bolRet = _objSalesVolumeDA.InsertSalesVolumeHeader(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleVolumeStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.SalesVolumeID, _
                                       "Insert Sale Volume Stock")


            Else
                bolRet = _objSalesVolumeDA.UpdateSalesVolumeHeader(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleVolumeStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.SalesVolumeID, _
                                       "Update Sale Volume Stock")

                Dim tmpdt As New DataTable
                tmpdt = _objSalesVolumeDA.GetSalesVolumeDetailByID(obj.SalesVolumeID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByLoss(tmpdr.Item("ForSaleID"), tmpdr.Item("QTY"), tmpdr("ItemTK"), tmpdr("ItemTG"), "+")
                        UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now, obj.IsSolidVolume)
                    Next
                End If

            End If

            If bolRet = True Then
                For Each dr As DataRow In _dtSalesVolumeDetail.Rows
                    Dim objItemDetail As New SalesVolumeDetailInfo

                    If dr.RowState = DataRowState.Added Then

                        With objItemDetail
                            .SalesVolumeDetailID = dr.Item("SalesVolumeDetailID")
                            .SalesVolumeID = obj.SalesVolumeID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .Length = dr.Item("Length")
                            .SalesRate = dr.Item("SalesRate")
                            .QTY = dr.Item("QTY")
                            .ItemTK = dr.Item("ItemTK")
                            .ItemTG = dr.Item("ItemTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .GoldPrice = dr.Item("GoldPrice")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .FixPrice = dr.Item("FixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .DesignCharges = dr.Item("DesignCharges")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                        End With
                        bolRet = _objSalesVolumeDA.InsertSalesVolumeDetail(objItemDetail)
                        UpdateSalesItemByLoss(dr.Item("ForSaleID"), dr.Item("QTY"), dr("ItemTK"), dr("ItemTG"), "-")
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, Now, obj.IsSolidVolume)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objItemDetail
                            .SalesVolumeDetailID = dr.Item("SalesVolumeDetailID")
                            .SalesVolumeID = obj.SalesVolumeID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .Length = dr.Item("Length")
                            .SalesRate = dr.Item("SalesRate")
                            .QTY = dr.Item("QTY")
                            .ItemTK = dr.Item("ItemTK")
                            .ItemTG = dr.Item("ItemTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .GoldPrice = dr.Item("GoldPrice")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .FixPrice = dr.Item("FixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .DesignCharges = dr.Item("DesignCharges")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                        End With

                        bolRet = _objSalesVolumeDA.UpdateSalesVolumeDetail(objItemDetail)
                        UpdateSalesItemByLoss(dr.Item("ForSaleID"), dr.Item("QTY"), dr("ItemTK"), dr("ItemTG"), "-")
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, Now, obj.IsSolidVolume)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateSalesItemByLoss(dr.Item("ForSaleID"), dr.Item("QTY"), dr("ItemTK"), dr("ItemTG"), "-")
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, Now, obj.IsSolidVolume)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objSalesVolumeDA.DeleteSalesVolumeDetail(CStr(dr.Item("SalesVolumeDetailID", DataRowVersion.Original)))
                    End If
                Next
            End If
            Return bolRet
        End Function

        Private Sub UpdateSalesItemByLoss(ByVal argForSaleID As String, ByVal argqty As Integer, ByVal argItemTK As Decimal, ByVal argItemTG As Decimal, ByVal argopt As String, Optional ByVal cristr As String = "")
            _objSalesItemDA.UpdateLossQTYandGramByForSaleID(argForSaleID, argqty, argItemTK, argItemTG, argopt, cristr)
        End Sub

        Private Sub UpdateSalesItemByQTY(ByVal argForSaleID As String, ByVal argqty As Integer, ByVal argItemTK As Decimal, ByVal argItemTG As Decimal, ByVal argopt As String, Optional ByVal cristr As String = "")
            _objSalesItemDA.UpdateSalesItemByQTYandWeight(argForSaleID, argqty, argItemTK, argItemTG, argopt, cristr)
        End Sub

        Public Function GetSalesVolumePrint(ByVal SalesVolumeID As String) As System.Data.DataTable Implements ISalesVolumeController.GetSalesVolumePrint
            Return _objSalesVolumeDA.GetSalesVolumePrint(SalesVolumeID)
        End Function

        Public Function DeleteSalesVolume(ByVal obj As CommonInfo.SalesVolumeHeaderInfo) As Boolean Implements ISalesVolumeController.DeleteSalesVolume
            Dim tmpdt As New DataTable
            tmpdt = _objSalesVolumeDA.GetSalesVolumeDetailByID(obj.SalesVolumeID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByLoss(tmpdr.Item("ForSaleID"), tmpdr.Item("QTY"), tmpdr("ItemTK"), tmpdr("ItemTG"), "+")
                    UpdateSalesItemByQTY(tmpdr.Item("ForSaleID"), tmpdr.Item("QTY"), tmpdr("ItemTK"), tmpdr("ItemTG"), "+")
                    UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), True, Now)
                Next
            End If

            If _objSalesVolumeDA.DeleteSalesVolumeHeader(obj.SalesVolumeID) Then

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleVolumeStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       obj.SalesVolumeID, _
                                       "Delete Sale Volume Stock")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetSalesVolumeDataByHeaderIDAndItemCode(SalesVolumeHeaderID As String, Optional ItemCode As String = "") As DataTable Implements ISalesVolumeController.GetSalesVolumeDataByHeaderIDAndItemCode
            Return _objSalesVolumeDA.GetSalesVolumeDataByHeaderIDAndItemCode(SalesVolumeHeaderID, ItemCode)
        End Function

        'Public Function GetSalesVolumeGemDataBySaleDetailID(SalesVolumeDetailID As String) As DataTable Implements ISalesVolumeController.GetSalesVolumeGemDataBySaleDetailID
        '    Return _objSalesVolumeDA.GetSalesVolumeGemDataBySaleDetailID(SalesVolumeDetailID)
        'End Function
        Public Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesVolumeController.GetSalesVolumeDateByForSaleID
            Return _objSalesVolumeDA.GetSalesVolumeDateByForSaleID(ForSaleID)
        End Function
        Public Function GetSaleVolumeByID(ByVal SalesVolumeID As String) As SalesVolumeHeaderInfo Implements ISalesVolumeController.GetSaleVolumeByID
            Return _objSalesVolumeDA.GetSaleVolumeByID(SalesVolumeID)
        End Function
        Public Function GetSalesInvoiceVolumeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeController.GetSalesInvoiceVolumeReport
            Return _objSalesVolumeDA.GetSalesInvoiceVolumeReport(FromDate, ToDate, criStr)
        End Function

        Public Function GetSalesInvoiceVolumeReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeController.GetSalesInvoiceVolumeReportForTotal
            Return _objSalesVolumeDA.GetSalesInvoiceVolumeReportForTotal(FromDate, ToDate, criStr)
        End Function
        Public Function GetProfitForSaleVoulumeItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeController.GetProfitForSaleVoulumeItem
            Return _objSalesVolumeDA.GetProfitForSaleVoulumeItem(FromDate, ToDate, criStr)
        End Function

        Public Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeController.GetAllSalesVolumeVoucherPrint
            Return _objSalesVolumeDA.GetAllSalesVolumeVoucherPrint(FromDate, ToDate, criStr)
        End Function
        Public Function GetSaleVolumeDetailByDetailID(ByVal SalesVolumeDetailID As String) As System.Data.DataTable Implements ISalesVolumeController.GetSaleVolumeDetailByDetailID
            Return _objSalesVolumeDA.GetSaleVolumeDetailByDetailID(SalesVolumeDetailID)
        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleVolumeID As String) As Boolean Implements ISalesVolumeController.UpdateRedeemID

            If _objSalesVolumeDA.UpdateRedeemID(RedeemID, SaleVolumeID) Then

                Return True
            Else
                Return False
            End If
        End Function
        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleVolumeID As String) As Boolean Implements ISalesVolumeController.UpdateTransactionID

            If _objSalesVolumeDA.UpdateTransactionID(TransactionID, SaleVolumeID) Then

                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

