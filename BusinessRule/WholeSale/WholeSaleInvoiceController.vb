Imports DataAccess.WholeSaleInvoice
Imports CommonInfo
Imports DataAccess.SalesItem

Namespace WholeSaleInvoice
    Public Class WholeSaleInvoiceController
        Implements IWholeSaleInvoiceController

#Region "Private Members"

        Private _objWholeSaleInvoiceDA As IWholeSaleInvoiceDA
        Private _objForSaleDA As ISalesItemDA

        'Private _objJournalTransactionDA As IJournalTransactionDA
        'Private _objAccountItemFixedDA As IAccountItemFixedDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IWholeSaleInvoiceController = New WholeSaleInvoiceController

#End Region

#Region "Constructors"

        Private Sub New()
            _objWholeSaleInvoiceDA = DataAccess.Factory.Instance.CreateWholeSaleInvoiceDA
            _objForSaleDA = DataAccess.Factory.Instance.CreateSalesItemDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IWholeSaleInvoiceController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objForSale As New CommonInfo.SalesItemInfo

            With objForSale
                .IsExit = argIsExit
                .ForSaleID = argForSaleID
                .ExitDate = argExitDate
            End With
            _objForSaleDA.UpdateSaleItemIsExit(objForSale)
        End Sub

        Public Function DeleteWholeSaleInvoice(ByVal WholeSaleInvoiceID As String) As Boolean Implements IWholeSaleInvoiceController.DeleteWholeSaleInvoice
            Dim tmpdt As New DataTable

            tmpdt = _objWholeSaleInvoiceDA.GetWholeSaleInvoiceItem(WholeSaleInvoiceID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                Next
            End If

            If _objWholeSaleInvoiceDA.DeleteWholeSaleInvoice(WholeSaleInvoiceID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.WholeSaleInvoice.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       WholeSaleInvoiceID, _
                                       "Delete WholeSales Invoice")


                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllWholeSaleInvoice() As System.Data.DataTable Implements IWholeSaleInvoiceController.GetAllWholeSaleInvoice
            Return _objWholeSaleInvoiceDA.GetAllWholeSaleInvoice
        End Function

        Public Function GetWholeSaleInvoiceByID(ByVal WholeSaleInvoiceID As String) As CommonInfo.WholeSaleInvoiceInfo Implements IWholeSaleInvoiceController.GetWholeSaleInvoiceByID
            Return _objWholeSaleInvoiceDA.GetWholeSaleInvoiceByID(WholeSaleInvoiceID)
        End Function

        Public Function SaveWholeSaleInvoice(ByVal obj As CommonInfo.WholeSaleInvoiceInfo, ByVal _dtWholeSalesInvoiceItem As System.Data.DataTable) As Boolean Implements IWholeSaleInvoiceController.SaveWholeSaleInvoice
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController

            Dim bolRet As Boolean = False

            If obj.WholesaleInvoiceID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.WholeSaleStock.ToString)
                obj.WholesaleInvoiceID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.WDate)
                bolRet = _objWholeSaleInvoiceDA.InsertWholeSaleInvoice(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.WholeSaleInvoice.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.WholesaleInvoiceID, _
                                       "Insert WholeSale Invoice")


            Else
                bolRet = _objWholeSaleInvoiceDA.UpdateWholeSaleInvoice(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.WholeSaleInvoice.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.WholesaleInvoiceID, _
                                       "Update WholeSales Invoice")

                Dim tmpdt As New DataTable
                tmpdt = _objWholeSaleInvoiceDA.GetWholeSaleInvoiceItemByID(obj.WholesaleInvoiceID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByIsExit(tmpdr.Item("@ForSaleID"), False, Now)
                    Next
                End If

            End If
            If bolRet = True Then
                'UpdateSalesItemByIsExit(obj.ForSaleID, True, obj.SDate)
                For Each dr As DataRow In _dtWholeSalesInvoiceItem.Rows
                    Dim objWSaleInvoiceItem As New WholeSaleInvoiceItemInfo
                    If dr.RowState = DataRowState.Added Then
                        If Not IsDBNull(dr.Item("@ForSaleID")) Then
                            With objWSaleInvoiceItem
                                .WholesaleInvoiceItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.WholeSaleInvoiceItem, "WholesaleInvoiceItemID", obj.WDate)
                                .WholesaleInvoiceID = obj.WholesaleInvoiceID
                                .ForSaleID = dr.Item("@ForSaleID")
                                .ItemNameID = dr.Item("ItemNameID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .ItemCode = dr.Item("BarcodeNo")
                                .SalesRate = dr.Item("Rate")
                                .ItemTK = dr.Item("ItemTK")
                                .ItemTG = dr.Item("ItemTG")
                                .GemsTK = dr.Item("GemsTK")
                                .GemsTG = dr.Item("GemsTG")
                                .WasteTK = dr.Item("WasteTK")
                                .WasteTG = dr.Item("WasteTG")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")

                                .GoldPrice = dr.Item("Amount")
                                .FixPrice = dr.Item("FixPrice")
                                If obj.PayType = 1 Then
                                    .IsReturn = False
                                    .IsSale = False
                                Else
                                    .IsReturn = False
                                    .IsSale = True
                                End If
                                .DesignCharges = IIf(IsDBNull(dr.Item("DesignCharges")), 0, dr.Item("DesignCharges"))
                                .DesignChargesRate = IIf(IsDBNull(dr.Item("DesignChargesRate")), 0, dr.Item("DesignChargesRate"))
                                .ItemDisPercent = IIf(IsDBNull(dr.Item("disPercent")), 0, dr.Item("disPercent"))
                                .ItemDisAmount = IIf(IsDBNull(dr.Item("disAmount")), 0, dr.Item("disAmount"))
                                .GemsPrice = IIf(IsDBNull(dr.Item("GemsPrice")), 0, dr.Item("GemsPrice"))
                            End With
                            bolRet = _objWholeSaleInvoiceDA.InsertWholeSaleInvoiceItem(objWSaleInvoiceItem)
                            UpdateSalesItemByIsExit(objWSaleInvoiceItem.ForSaleID, True, obj.WDate)
                        End If

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objWSaleInvoiceItem
                            .WholesaleInvoiceItemID = dr.Item("WholesaleInvoiceItemID")
                            .WholesaleInvoiceID = obj.WholesaleInvoiceID
                            .ForSaleID = dr.Item("@ForSaleID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .ItemCode = dr.Item("BarcodeNo")
                            .SalesRate = dr.Item("Rate")
                            .ItemTK = dr.Item("ItemTK")
                            .ItemTG = dr.Item("ItemTG")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .GoldPrice = dr.Item("Amount")
                            .FixPrice = dr.Item("FixPrice")
                            If obj.PayType = 1 Then
                                .IsReturn = False
                                .IsSale = False
                            Else
                                .IsReturn = False
                                .IsSale = True
                            End If
                            '.TotalG = dr.Item("Gram")
                            .SalesRate = dr.Item("Rate")
                            .DesignCharges = dr.Item("DesignCharges")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                            .ItemDisPercent = IIf(IsDBNull(dr.Item("DisPercent")), 0, dr.Item("DisPercent"))
                            .ItemDisAmount = IIf(IsDBNull(dr.Item("DisAmount")), 0, dr.Item("DisAmount"))
                            .GemsPrice = IIf(IsDBNull(dr.Item("GemsPrice")), 0, dr.Item("GemsPrice"))
                        End With
                        bolRet = _objWholeSaleInvoiceDA.UpdateWholeSaleInvoiceItem(objWSaleInvoiceItem)
                        UpdateSalesItemByIsExit(objWSaleInvoiceItem.ForSaleID, True, obj.WDate)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objWholeSaleInvoiceDA.DeleteWholeSaleInvoiceItem(CStr(dr.Item("WholesaleInvoiceItemID", DataRowVersion.Original)))
                        ' UpdateSalesItemByIsExit(objWSaleInvoiceItem.ForSaleID, False, obj.WDate)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateSalesItemByIsExit(dr.Item("@ForSaleID"), True, obj.WDate)
                    End If
                Next

            End If
            Return bolRet
        End Function

        Public Function GetWholeSalesInvoiceItem(ByVal WholeSalesInvoiceID As String) As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSalesInvoiceItem
            Return _objWholeSaleInvoiceDA.GetWholeSaleInvoiceItem(WholeSalesInvoiceID)
        End Function

        Public Function GetWholeSaleInvoiceItemByID(ByVal WholeSaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSaleInvoiceItemByID
            Return _objWholeSaleInvoiceDA.GetWholeSaleInvoiceItemByID(WholeSaleInvoiceID)
        End Function

        Public Function GetBarcodeByWholeSaleID(ByVal argstr As String, Optional ByVal cristr As String = "", Optional ByVal WholeSaleID As String = "") As CommonInfo.WholeSaleInvoiceItemInfo Implements IWholeSaleInvoiceController.GetBarcodeByWholeSaleID
            Return _objWholeSaleInvoiceDA.GetBarcodeByWholeSaleID(argstr, cristr, WholeSaleID)
        End Function
        Public Function GetBarcodeDataByWholeSaleID(ByVal argstr As String, Optional ByVal WholeSaleID As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetBarcodeDataByWholeSaleID
            Return _objWholeSaleInvoiceDA.GetBarcodeDataByWholeSaleID(argstr, WholeSaleID)
        End Function
        Public Function GetItemCodeByWholeSaleID(Optional ByVal WholeSaleID As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetItemCodeByWholeSaleID
            Return _objWholeSaleInvoiceDA.GetItemCodeByWholeSaleID(WholeSaleID)
        End Function

        Public Function GetBarcodeDataByWholesaleInvoiceID(ByVal WholesaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleInvoiceController.GetBarcodeDataByWholesaleInvoiceID
            Return _objWholeSaleInvoiceDA.GetBarcodeDataByWholesaleInvoiceID(WholesaleInvoiceID)
        End Function

        Public Function GetAllWholesaleInvoiceByConsignmentType() As System.Data.DataTable Implements IWholeSaleInvoiceController.GetAllWholesaleInvoiceByConsignmentType
            Return _objWholeSaleInvoiceDA.GetAllWholesaleInvoiceByConsignmentType()
        End Function

        Public Function GetWSInvoiceAndCSInvoice() As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWSInvoiceAndCSInvoice
            Return _objWholeSaleInvoiceDA.GetWSInvoiceAndCSInvoice()
        End Function
        Public Function GetWSInvoice() As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWSInvoice
            Return _objWholeSaleInvoiceDA.GetWSInvoice()
        End Function

        Public Function GetWholeSaleInvoiceForCommissionReport(ByVal WDate As Date, ByVal CustomerID As String, ByVal PayType As Integer, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSaleInvoiceForCommissionReport
            Return _objWholeSaleInvoiceDA.GetWholeSaleInvoiceForCommissionReport(WDate, CustomerID, PayType, cristr)
        End Function

        Public Function GetWholeSalePrint(ByVal WholeSaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSalePrint
            Return _objWholeSaleInvoiceDA.GetWholeSalePrint(WholeSaleInvoiceID)
        End Function

        Public Function GetWholeSaleReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSaleReport
            Return _objWholeSaleInvoiceDA.GetWholeSaleReport(FromDate, ToDate, GetFilterString)
        End Function
        Public Function GetWholeSaleReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSaleReportAmount
            Return _objWholeSaleInvoiceDA.GetWholeSaleReportAmount(FromDate, ToDate, GetFilterString)
        End Function

        Public Function GetWholeSaleTotalPaidAmtReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal WSType As String = "", Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSaleTotalPaidAmtReport
            Return _objWholeSaleInvoiceDA.GetWholeSaleTotalPaidAmtReport(FromDate, ToDate, WSType, GetFilterString)
        End Function

        Public Function GetWholeSaleSummaryReportByCustomer(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetWholeSaleSummaryReportByCustomer
            Return _objWholeSaleInvoiceDA.GetWholeSaleSummaryReportByCustomer(FromDate, ToDate, GetFilterString)
        End Function

        Public Function GetConsignBalanceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetConsignBalanceReport
            Return _objWholeSaleInvoiceDA.GetConsignBalanceReport(FromDate, ToDate, GetFilterString)
        End Function
        Public Function GetConsignBalanceReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceController.GetConsignBalanceReportAmount
            Return _objWholeSaleInvoiceDA.GetConsignBalanceReportAmount(FromDate, ToDate, GetFilterString)
        End Function
        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleVolumeID As String) As Boolean Implements IWholeSaleInvoiceController.UpdateTransactionID

            If _objWholeSaleInvoiceDA.UpdateTransactionID(TransactionID, SaleVolumeID) Then

                Return True
            Else
                Return False
            End If
        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements IWholeSaleInvoiceController.UpdateRedeemID

            If _objWholeSaleInvoiceDA.UpdateRedeemID(RedeemID, SaleInvoiceID) Then

                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

