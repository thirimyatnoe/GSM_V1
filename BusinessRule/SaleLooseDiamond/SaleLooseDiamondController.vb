Imports DataAccess.SaleLooseDiamond
Imports DataAccess.SalesItem
Imports DataAccess.SalesItemInvoice
Imports CommonInfo
Namespace SaleLooseDiamond
    Public Class SaleLooseDiamondController
        Implements ISaleLooseDiamondController
#Region "Private Members"

        Private _objSalesItemDA As ISalesItemDA
        Private _objSalesInvoiceDA As ISalesItemInvoiceDA
        Private _objSaleLooseDiamondDA As ISaleLooseDiamondDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISaleLooseDiamondController = New SaleLooseDiamondController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSalesInvoiceDA = DataAccess.Factory.Instance.CreateSalesItemInvoiceDA
            _objSaleLooseDiamondDA = DataAccess.Factory.Instance.CreateSaleLooseDiamondDA
            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub
#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISaleLooseDiamondController
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
                .IsLooseDiamond = CheckState
            End With
            _objSalesItemDA.UpdateSaleItemIsExit(objSaleItem)
        End Sub

        Public Function GetAllSaleLooseDiamond() As System.Data.DataTable Implements ISaleLooseDiamondController.GetAllSaleLooseDiamond
            Return _objSaleLooseDiamondDA.GetAllSaleLooseDiamond()
        End Function

        Public Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondHeaderID As String) As CommonInfo.SaleLooseDiamondHeaderInfo Implements ISaleLooseDiamondController.GetSaleLooseDiamondHeaderByID
            Return _objSaleLooseDiamondDA.GetSaleLooseDiamondHeaderByID(SaleLooseDiamondHeaderID)
        End Function

        Public Function GetSaleLooseDiamondDetailByID(ByVal SaleLooseDiamondHeaderID As String) As System.Data.DataTable Implements ISaleLooseDiamondController.GetSaleLooseDiamondDetailByID
            Return _objSaleLooseDiamondDA.GetSaleLooseDiamondDetailByID(SaleLooseDiamondHeaderID)
        End Function

        Public Function SaveSaleLooseDiamondHeader(ByVal obj As CommonInfo.SaleLooseDiamondHeaderInfo, ByVal _dtSaleLooseDiamondDetail As DataTable, ByVal _dtOtherCash As DataTable) As Boolean Implements ISaleLooseDiamondController.SaveSaleLooseDiamondHeader
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

            Dim bolRet As Boolean = True
            If obj.SaleLooseDiamondID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.SaleLooseDiamond.ToString)
                obj.SaleLooseDiamondID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.SaleDate)

                'obj.SaleLooseDiamondID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.SalesVolume, CommonInfo.EnumSetting.GenerateKeyType.SalesVolume.ToString, obj.SaleDate)
                bolRet = _objSaleLooseDiamondDA.InsertSaleLooseDiamondHeader(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleLooseDiamond.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.SaleLooseDiamondID, _
                                       "Insert Sale Loose Diamond Stock")


            Else
                bolRet = _objSaleLooseDiamondDA.UpdateSaleLooseDiamondHeader(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleLooseDiamond.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.SaleLooseDiamondID, _
                                       "Update Sale Loose Diamond Stock")

                Dim tmpdt As New DataTable
                tmpdt = _objSaleLooseDiamondDA.GetSaleLooseDiamondDetailByID(obj.SaleLooseDiamondID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByLoss(tmpdr.Item("ForSaleID"), tmpdr.Item("QTY"), tmpdr("ItemTK"), tmpdr("ItemTG"), "+")
                        UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                    Next
                End If

            End If

            If bolRet = True Then
                For Each dr As DataRow In _dtSaleLooseDiamondDetail.Rows
                    Dim objItemDetail As New SaleLooseDiamondDetailInfo

                    If dr.RowState = DataRowState.Added Then

                        With objItemDetail
                            .SaleLooseDiamondDetailID = dr.Item("SaleLooseDiamondDetailID")
                            .SaleLooseDiamondID = obj.SaleLooseDiamondID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .Color = dr.Item("Color")
                            .Clarity = dr.Item("Clarity")
                            .Shape = dr.Item("Shape")
                            .SalesRate = dr.Item("SalesRate")
                            .QTY = dr.Item("QTY")
                            .ItemTK = dr.Item("ItemTK")
                            .ItemTG = dr.Item("ItemTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .FixPrice = dr.Item("FixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .DesignCharges = dr.Item("DesignCharges")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                            .SellingAmt = dr.Item("SellingAmt")
                            .SellingRate = dr.Item("SellingRate")
                            .GemsTW = dr.Item("GemsTW")
                            .IsOriginalFixedPrice = dr.Item("IsOriginalFixedPrice")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .IsOriginalPriceCarat = dr.Item("IsOriginalPriceCarat")
                            .OriginalPriceCarat = dr.Item("OriginalPriceCarat")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .IsSaleReturn = dr.Item("IsSaleReturn")
                            .GemsPrice = dr.Item("GemsPrice")
                        End With
                        bolRet = _objSaleLooseDiamondDA.InsertSaleLooseDiamondDetail(objItemDetail)
                        UpdateSalesItemByLoss(dr.Item("ForSaleID"), dr.Item("QTY"), dr("ItemTK"), dr("ItemTG"), "-")
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, Now)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objItemDetail
                            .SaleLooseDiamondDetailID = dr.Item("SaleLooseDiamondDetailID")
                            .SaleLooseDiamondID = obj.SaleLooseDiamondID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .SalesRate = dr.Item("SalesRate")
                            .QTY = dr.Item("QTY")
                            .ItemTK = dr.Item("ItemTK")
                            .ItemTG = dr.Item("ItemTG")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .FixPrice = dr.Item("FixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .DesignCharges = dr.Item("DesignCharges")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                            .SaleLooseDiamondID = obj.SaleLooseDiamondID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .Color = dr.Item("Color")
                            .Clarity = dr.Item("Clarity")
                            .Shape = dr.Item("Shape")
                            .SalesRate = dr.Item("SalesRate")
                            .QTY = dr.Item("QTY")
                            .ItemTK = dr.Item("ItemTK")
                            .ItemTG = dr.Item("ItemTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .FixPrice = dr.Item("FixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .DesignCharges = dr.Item("DesignCharges")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                            .SellingAmt = dr.Item("SellingAmt")
                            .SellingRate = dr.Item("SellingRate")
                            .GemsTW = dr.Item("GemsTW")
                            .IsOriginalFixedPrice = dr.Item("IsOriginalFixedPrice")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .IsOriginalPriceCarat = dr.Item("IsOriginalPriceCarat")
                            .OriginalPriceCarat = dr.Item("OriginalPriceCarat")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .IsSaleReturn = dr.Item("IsSaleReturn")
                            .GemsPrice = dr.Item("GemsPrice")
                        End With

                        bolRet = _objSaleLooseDiamondDA.UpdateSaleLooseDiamondDetail(objItemDetail)
                        UpdateSalesItemByLoss(dr.Item("ForSaleID"), dr.Item("QTY"), dr("ItemTK"), dr("ItemTG"), "-")
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, Now)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateSalesItemByLoss(dr.Item("ForSaleID"), dr.Item("QTY"), dr("ItemTK"), dr("ItemTG"), "-")
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, Now)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objSaleLooseDiamondDA.DeleteSaleLooseDiamondDetail(CStr(dr.Item("SaleLooseDiamondDetailID", DataRowVersion.Original)))
                    End If
                Next
            End If
            If _dtOtherCash.Rows.Count > 0 Then
                If bolRet = True Then
                    For Each drRecord As DataRow In _dtOtherCash.Rows
                        Dim objRecord As New CommonInfo.OtherCashInfo
                        If drRecord.RowState = DataRowState.Added Then
                            With objRecord
                                .RecordCashID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OtherCash, EnumSetting.GenerateKeyType.OtherCash.ToString, Now.Date)
                                .VoucherNo = obj.SaleLooseDiamondID
                                .CashTypeID = drRecord.Item("CashTypeID")
                                .ExchangeRate = drRecord.Item("ExchangeRate")
                                .Amount = drRecord.Item("Amount")
                            End With
                            bolRet = _objSalesInvoiceDA.InsertRecordCash(objRecord)
                        ElseIf drRecord.RowState = DataRowState.Modified Then
                            With objRecord
                                .RecordCashID = drRecord.Item("RecordCashID")
                                .VoucherNo = obj.SaleLooseDiamondID
                                .CashTypeID = drRecord.Item("CashTypeID")
                                .ExchangeRate = drRecord.Item("ExchangeRate")
                                .Amount = drRecord.Item("Amount")
                            End With
                            bolRet = _objSalesInvoiceDA.UpdateRecordCash(objRecord)
                        ElseIf drRecord.RowState = DataRowState.Deleted Then
                            _objSalesInvoiceDA.DeleteRecordCash(CStr(drRecord.Item("RecordCashID", DataRowVersion.Original)))
                        End If
                    Next
                End If
            End If
            Return bolRet
        End Function

        Private Sub UpdateSalesItemByLoss(ByVal argForSaleID As String, ByVal argqty As Integer, ByVal argItemTK As Decimal, ByVal argItemTG As Decimal, ByVal argopt As String, Optional ByVal cristr As String = "")
            _objSalesItemDA.UpdateLossQTYandGramByForSaleID(argForSaleID, argqty, argItemTK, argItemTG, argopt, cristr)
        End Sub

        Private Sub UpdateSalesItemByQTY(ByVal argForSaleID As String, ByVal argqty As Integer, ByVal argItemTK As Decimal, ByVal argItemTG As Decimal, ByVal argopt As String, Optional ByVal cristr As String = "")
            _objSalesItemDA.UpdateSalesItemByQTYandWeight(argForSaleID, argqty, argItemTK, argItemTG, argopt, cristr)
        End Sub

        Public Function GetSaleLooseDiamondPrint(ByVal SaleLooseDiamondID As String) As System.Data.DataTable Implements ISaleLooseDiamondController.GetSaleLooseDiamondPrint
            Return _objSaleLooseDiamondDA.GetSaleLooseDiamondPrint(SaleLooseDiamondID)
        End Function

        Public Function DeleteSaleLooseDiamond(ByVal obj As CommonInfo.SaleLooseDiamondHeaderInfo) As Boolean Implements ISaleLooseDiamondController.DeleteSaleLooseDiamond
            Dim tmpdt As New DataTable
            tmpdt = _objSaleLooseDiamondDA.GetSaleLooseDiamondDetailByID(obj.SaleLooseDiamondID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByLoss(tmpdr.Item("ForSaleID"), tmpdr.Item("QTY"), tmpdr("ItemTK"), tmpdr("ItemTG"), "+")
                    UpdateSalesItemByQTY(tmpdr.Item("ForSaleID"), tmpdr.Item("QTY"), tmpdr("ItemTK"), tmpdr("ItemTG"), "+")
                    UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), True, Now, True)
                Next
            End If

            If _objSaleLooseDiamondDA.DeleteSaleLooseDiamondHeader(obj.SaleLooseDiamondID) Then

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleVolumeStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       obj.SaleLooseDiamondID, _
                                       "Delete Sale Volume Stock")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetSalesVolumeDataByHeaderIDAndItemCode(SalesVolumeHeaderID As String, Optional ItemCode As String = "") As DataTable Implements ISaleLooseDiamondController.GetSalesVolumeDataByHeaderIDAndItemCode
            Return _objSaleLooseDiamondDA.GetSalesVolumeDataByHeaderIDAndItemCode(SalesVolumeHeaderID, ItemCode)
        End Function

        'Public Function GetSalesVolumeGemDataBySaleDetailID(SaleLooseDiamondDetailID As String) As DataTable Implements ISaleLooseDiamondController.GetSalesVolumeGemDataBySaleDetailID
        '    Return _objSaleLooseDiamondDA.GetSalesVolumeGemDataBySaleDetailID(SaleLooseDiamondDetailID)
        'End Function
        Public Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ISaleLooseDiamondController.GetSalesVolumeDateByForSaleID
            Return _objSaleLooseDiamondDA.GetSalesVolumeDateByForSaleID(ForSaleID)
        End Function
        Public Function GetSaleLooseDiamondByID(ByVal SaleLooseDiamondID As String) As SaleLooseDiamondHeaderInfo Implements ISaleLooseDiamondController.GetSaleLooseDiamondByID
            Return _objSaleLooseDiamondDA.GetSaleLooseDiamondByID(SaleLooseDiamondID)
        End Function
        Public Function GetSalesInvoiceLooseDiamondReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondController.GetSalesInvoiceLooseDiamondReport
            Return _objSaleLooseDiamondDA.GetSalesInvoiceLooseDiamondReport(FromDate, ToDate, criStr)
        End Function

        Public Function GetSaleLooseDiamondReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondController.GetSaleLooseDiamondReportForTotal
            Return _objSaleLooseDiamondDA.GetSaleLooseDiamondReportForTotal(FromDate, ToDate, criStr)
        End Function
        Public Function GetProfitForSaleDiamondItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondController.GetProfitForSaleDiamondItem
            Return _objSaleLooseDiamondDA.GetProfitForSaleDiamondItem(FromDate, ToDate, criStr)
        End Function

        Public Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondController.GetAllSalesVolumeVoucherPrint
            Return _objSaleLooseDiamondDA.GetAllSalesVolumeVoucherPrint(FromDate, ToDate, criStr)
        End Function
        Public Function GetSaleLooseDiamondDetailByDetailID(ByVal SaleLooseDiamondDetailID As String) As System.Data.DataTable Implements ISaleLooseDiamondController.GetSaleLooseDiamondDetailByDetailID
            Return _objSaleLooseDiamondDA.GetSaleLooseDiamondDetailByDetailID(SaleLooseDiamondDetailID)
        End Function
        Public Function GetSaleLooseDiamondDataByCustomerIDAndItemCode(CustomerID As String, Optional criStr As String = "") As DataTable Implements ISaleLooseDiamondController.GetSaleLooseDiamondDataByCustomerIDAndItemCode
            Return _objSaleLooseDiamondDA.GetSaleLooseDiamondDataByCustomerIDAndItemCode(CustomerID, criStr)
        End Function
        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleVolumeID As String) As Boolean Implements ISaleLooseDiamondController.UpdateTransactionID

            If _objSaleLooseDiamondDA.UpdateTransactionID(TransactionID, SaleVolumeID) Then

                Return True
            Else
                Return False
            End If
        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements ISaleLooseDiamondController.UpdateRedeemID

            If _objSaleLooseDiamondDA.UpdateRedeemID(RedeemID, SaleInvoiceID) Then

                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

