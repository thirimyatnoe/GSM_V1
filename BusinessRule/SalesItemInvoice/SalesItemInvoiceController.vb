Imports DataAccess.SalesItemInvoice
Imports DataAccess.SalesItem
Imports CommonInfo
Namespace SalesItemInvoice
    Public Class SalesItemInvoiceController
        Implements ISalesItemInvoiceController



#Region "Private Members"

        Private _objSalesItemDA As ISalesItemDA
        Private _objSalesInvoiceDA As ISalesItemInvoiceDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISalesItemInvoiceController = New SalesItemInvoiceController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSalesInvoiceDA = DataAccess.Factory.Instance.CreateSalesItemInvoiceDA
            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesItemInvoiceController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate

            End With
            _objSalesItemDA.UpdateSaleItemIsExit(objSaleItem)
        End Sub

        Public Function GetAllSalesInvoice() As System.Data.DataTable Implements ISalesItemInvoiceController.GetAllSalesInvoice
            Return _objSalesInvoiceDA.GetAllSalesInvoice()
        End Function

      
        Public Function GetSaleInvoiceHeaderByID(ByVal SalesInvoiceHeaderID As String) As CommonInfo.SaleInvoiceHeaderInfo Implements ISalesItemInvoiceController.GetSaleInvoiceHeaderByID
            Return _objSalesInvoiceDA.GetSaleInvoiceHeaderByID(SalesInvoiceHeaderID)
        End Function
        Public Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondHeaderID As String) As CommonInfo.SaleLooseDiamondHeaderInfo Implements ISalesItemInvoiceController.GetSaleLooseDiamondHeaderByID
            Return _objSalesInvoiceDA.GetSaleLooseDiamondHeaderByID(SaleLooseDiamondHeaderID)
        End Function


        Public Function GetSalesInvoiceDetailByID(ByVal SalesInvoiceHeaderID As String) As System.Data.DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceDetailByID
            Return _objSalesInvoiceDA.GetSalesInvoiceDetailByID(SalesInvoiceHeaderID)
        End Function

        Public Function GetSaleInvoiceDetailGemByHeaderID(ByVal SalesInvoiceHeaderID As String) As System.Data.DataTable Implements ISalesItemInvoiceController.GetSaleInvoiceDetailGemByHeaderID
            Return _objSalesInvoiceDA.GetSaleInvoiceDetailGemByHeaderID(SalesInvoiceHeaderID)
        End Function



        Public Function SaveSalesInvoice(ByVal obj As CommonInfo.SaleInvoiceHeaderInfo, ByVal _dtSaleInvoiceDetail As DataTable, ByVal _dtSaleInvoiceDetailGem As DataTable, ByVal _dtOtherCash As DataTable, ByVal _dtItemData_His As DataTable) As Boolean Implements ISalesItemInvoiceController.SaveSalesInvoice

            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim bolRet As Boolean = False
            Dim _ChangeItemStrValue As String = ""
            Dim _ChangeWasteStrValue As String = ""
            Dim _ItemCode As String = ""
            Dim _ItemK As Decimal
            Dim _ItemP As Decimal
            Dim _ItemY As Decimal
            Dim _WasteK As Decimal
            Dim _WasteP As Decimal
            Dim _WasteY As Decimal



            If obj.SaleInvoiceHeaderID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.SaleStock.ToString)
                obj.SaleInvoiceHeaderID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.SaleDate)
                bolRet = _objSalesInvoiceDA.InsertSalesInvoiceHeader(obj)
              

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.SaleInvoiceHeaderID, _
                                       "Insert Sale Stock")


            Else
                bolRet = _objSalesInvoiceDA.UpdateSalesInvoiceHeader(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.SaleInvoiceHeaderID, _
                                       "Update Sale Stock")

                Dim tmpdt As New DataTable
                tmpdt = _objSalesInvoiceDA.GetSalesInvoiceDetailByID(obj.SaleInvoiceHeaderID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        If tmpdr.Item("IsSaleReturn") = False Then
                            UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, obj.SaleDate)
                        End If

                    Next
                End If
            End If

            If bolRet = True Then
              
                For Each dr As DataRow In _dtSaleInvoiceDetail.Rows
                    Dim objItemDetail As New SalesInvoiceDetailInfo
                    _ChangeItemStrValue = ""
                    _ChangeWasteStrValue = ""
                    If dr.RowState = DataRowState.Added Then

                        With objItemDetail

                            .SaleInvoiceDetailID = dr.Item("SaleInvoiceDetailID")
                            .SaleInvoiceHeaderID = obj.SaleInvoiceHeaderID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .WasteTG = dr.Item("WasteTG")
                            .WasteTK = dr.Item("WasteTK")
                            .GemsTG = dr.Item("GemsTG")
                            .GemsTK = dr.Item("GemsTK")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .IsOriginalFixedPrice = dr.Item("IsOriginalFixedPrice")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .IsOriginalPriceGram = dr.Item("IsOriginalPriceGram")
                            .OriginalPriceGram = dr.Item("OriginalPriceGram")
                            .OriginalPriceTK = dr.Item("OriginalPriceTK")
                            .OriginalGemsPrice = dr.Item("OriginalGemsPrice")
                            .OriginalOtherPrice = dr.Item("OriginalOtherPrice")
                            .PurchaseWasteTK = dr.Item("PurchaseWasteTK")
                            .PurchaseWasteTG = dr.Item("PurchaseWasteTG")
                            .PurchaseWasteTG = dr.Item("PurchaseWasteTG")
                            .ItemTaxPer = dr.Item("ItemTaxPer")
                            .ItemTax = dr.Item("ItemTax")
                            .IsSaleReturn = dr.Item("IsSaleReturn")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .DesignCharges = dr.Item("DesignCharges")
                            _ItemCode = dr.Item("ItemCode")
                            _ItemK = dr.Item("ItemK")
                            _ItemP = dr.Item("ItemP")
                            _ItemY = dr.Item("ItemY")
                            _WasteK = dr.Item("WasteK")
                            _WasteP = dr.Item("WasteP")
                            _WasteY = dr.Item("WasteY")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                            .SellingRate = dr.Item("SellingRate")
                            .SellingAmt = dr.Item("SellingAmt")

                        End With
                        bolRet = _objSalesInvoiceDA.InsertSaleInvoiceDetail(objItemDetail)
                        If obj.IsCancel = False Then
                            If dr.Item("IsSaleReturn") = False Then
                                UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, obj.SaleDate)
                            End If
                        End If


                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objItemDetail

                            .SaleInvoiceDetailID = dr.Item("SaleInvoiceDetailID")
                            .SaleInvoiceHeaderID = obj.SaleInvoiceHeaderID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .WasteTG = dr.Item("WasteTG")
                            .WasteTK = dr.Item("WasteTK")
                            .GemsTG = dr.Item("GemsTG")
                            .GemsTK = dr.Item("GemsTK")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .IsOriginalFixedPrice = dr.Item("IsOriginalFixedPrice")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .IsOriginalPriceGram = dr.Item("IsOriginalPriceGram")
                            .OriginalPriceGram = dr.Item("OriginalPriceGram")
                            .OriginalPriceTK = dr.Item("OriginalPriceTK")
                            .OriginalGemsPrice = dr.Item("OriginalGemsPrice")
                            .OriginalOtherPrice = dr.Item("OriginalOtherPrice")
                            .PurchaseWasteTK = dr.Item("PurchaseWasteTK")
                            .PurchaseWasteTG = dr.Item("PurchaseWasteTG")
                            .ItemTaxPer = dr.Item("ItemTaxPer")
                            .ItemTax = dr.Item("ItemTax")
                            .IsSaleReturn = dr.Item("IsSaleReturn")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .DesignCharges = dr.Item("DesignCharges")
                            _ItemCode = dr.Item("ItemCode")
                            _ItemK = dr.Item("ItemK")
                            _ItemP = dr.Item("ItemP")
                            _ItemY = dr.Item("ItemY")
                            _WasteK = dr.Item("WasteK")
                            _WasteP = dr.Item("WasteP")
                            _WasteY = dr.Item("WasteY")
                            .DesignChargesRate = dr.Item("DesignChargesRate")
                            .SellingRate = dr.Item("SellingRate")
                            .SellingAmt = dr.Item("SellingAmt")
                        End With
                        bolRet = _objSalesInvoiceDA.UpdateSaleInvoiceDetail(objItemDetail)
                        If obj.IsCancel = False Then
                            If dr.Item("IsSaleReturn") = False Then
                                UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, obj.SaleDate)
                            End If
                        End If
                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objItemDetail
                            .SaleInvoiceDetailID = dr.Item("SaleInvoiceDetailID")
                            .SaleInvoiceHeaderID = obj.SaleInvoiceHeaderID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .WasteTG = dr.Item("WasteTG")
                            .WasteTK = dr.Item("WasteTK")
                            .GemsTG = dr.Item("GemsTG")
                            .GemsTK = dr.Item("GemsTK")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
                            .IsFixPrice = dr.Item("IsFixPrice")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .IsOriginalFixedPrice = dr.Item("IsOriginalFixedPrice")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .IsOriginalPriceGram = dr.Item("IsOriginalPriceGram")
                            .OriginalPriceGram = dr.Item("OriginalPriceGram")
                            .OriginalPriceTK = dr.Item("OriginalPriceTK")
                            .OriginalGemsPrice = dr.Item("OriginalGemsPrice")
                            .OriginalOtherPrice = dr.Item("OriginalOtherPrice")
                            .PurchaseWasteTK = dr.Item("PurchaseWasteTK")
                            .PurchaseWasteTG = dr.Item("PurchaseWasteTG")
                            .ItemTaxPer = dr.Item("ItemTaxPer")
                            .ItemTax = dr.Item("ItemTax")
                            .IsSaleReturn = dr.Item("IsSaleReturn")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .DesignCharges = dr.Item("DesignCharges")
                            _ItemCode = dr.Item("ItemCode")
                            _ItemK = dr.Item("ItemK")
                            _ItemP = dr.Item("ItemP")
                            _ItemY = dr.Item("ItemY")
                            _WasteK = dr.Item("WasteK")
                            _WasteP = dr.Item("WasteP")
                            _WasteY = dr.Item("WasteY")
                        End With
                        bolRet = _objSalesInvoiceDA.UpdateSaleInvoiceDetail(objItemDetail)
                        If obj.IsCancel = False Then
                            If dr.Item("IsSaleReturn") = False Then
                                UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, obj.SaleDate)
                            End If
                        End If
                    ElseIf dr.RowState = DataRowState.Deleted Then

                        Dim row As DataRow
                        Dim j As Integer = _dtSaleInvoiceDetailGem.Rows.Count() - 1
                        While j >= 0
                            row = _dtSaleInvoiceDetailGem.Rows(j)
                            If row.Item("@SaleInvoiceDetailID") = dr.Item("SaleInvoiceDetailID", DataRowVersion.Original) Then
                                _dtSaleInvoiceDetailGem.Rows.Remove(row)
                            End If
                            j = j - 1
                        End While
                        bolRet = _objSalesInvoiceDA.DeleteSaleInvoiceDetail(CStr(dr.Item("SaleInvoiceDetailID", DataRowVersion.Original)))
                    End If
                    For Each drItem_His As DataRow In _dtItemData_His.Rows
                        If _ItemCode = drItem_His.Item("ItemCode") Then

                            If (drItem_His.Item("ItemK") <> _ItemK) Then
                                _ChangeItemStrValue += drItem_His.Item("ItemK") & " -> " & _ItemK & "K "
                            End If
                            If (drItem_His.Item("ItemP")) <> _ItemP Then
                                _ChangeItemStrValue += drItem_His.Item("ItemP") & " -> " & _ItemP & "P "
                            End If

                            If (drItem_His.Item("ItemY") <> _ItemY) Then
                                _ChangeItemStrValue += drItem_His.Item("ItemY") & " -> " & _ItemY & "Y "
                            End If

                            If (drItem_His.Item("WasteK") <> _WasteK) Then
                                _ChangeWasteStrValue += drItem_His.Item("WasteK") & " -> " & _WasteK & "K "
                            End If
                            If (drItem_His.Item("WasteP") <> _WasteP) Then
                                _ChangeWasteStrValue += drItem_His.Item("WasteP") & " -> " & _WasteP & "P "
                            End If
                            If (drItem_His.Item("WasteY") <> _WasteY) Then
                                _ChangeWasteStrValue += drItem_His.Item("WasteY") & " -> " & _WasteY & "Y "
                            End If

                        End If
                    Next


                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                           DateTime.Now, _
                           Global_UserID, _
                           CommonInfo.EnumSetting.GenerateKeyType.SaleStock.ToString, _
                           CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                           obj.SaleInvoiceHeaderID, _
                           "Insert Sale Stock " & _ItemCode & " -" & "ItemWeight " & _ChangeItemStrValue & " Waste " & _ChangeWasteStrValue)


                    If dr.RowState <> DataRowState.Deleted Then
                        Dim dt As New DataTable
                        dt = _objSalesInvoiceDA.GetSaleInvoiceDetailGemByID(dr.Item("SaleInvoiceDetailID"))
                        If dt.Rows.Count > 0 Then   'For User Change DiamondItemBarcodeNo 
                            For Each tmpdr As DataRow In dt.Rows
                                _objSalesInvoiceDA.DeleteSaleInvoiceDetailGem(tmpdr.Item("SalesInvoiceGemItemID"))
                            Next
                        End If
                    End If
                Next


            End If


            If bolRet = True Then
                For Each dr As DataRow In _dtSaleInvoiceDetailGem.Rows
                    Dim objGemItem As New SaleInvoiceDetailGemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objGemItem
                            .SalesInvoiceGemItemID = dr.Item("SalesInvoiceGemItemID")
                            .SalesInvoiceDetailID = dr.Item("@SaleInvoiceDetailID")
                            .GemsCategoryID = dr.Item("@GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .GemsTW = dr.Item("GemsTW")
                            .Qty = dr.Item("Qty")
                            .UnitPrice = dr.Item("UnitPrice")
                            .Type = dr.Item("Type")
                            .Amount = dr.Item("Amount")
                            .GemsRemark = dr.Item("GemsRemark")
                            .GemTaxPer = Format(dr.Item("GemTaxPer"), "###,##0.##")
                            .GemTax = dr.Item("GemTax")
                        End With

                        bolRet = _objSalesInvoiceDA.InsertSaleInvoiceDetailGem(objGemItem)
                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objGemItem
                            .SalesInvoiceGemItemID = dr.Item("SalesInvoiceGemItemID")
                            .SalesInvoiceDetailID = dr.Item("@SaleInvoiceDetailID")
                            .GemsCategoryID = dr.Item("@GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .GemsTW = dr.Item("GemsTW")
                            .Qty = dr.Item("Qty")
                            .UnitPrice = dr.Item("UnitPrice")
                            .Type = dr.Item("Type")
                            .Amount = dr.Item("Amount")
                            .GemsRemark = dr.Item("GemsRemark")
                            .GemTaxPer = dr.Item("GemTaxPer")
                            .GemTax = dr.Item("GemTax")
                        End With

                        bolRet = _objSalesInvoiceDA.InsertSaleInvoiceDetailGem(objGemItem)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objGemItem
                            .SalesInvoiceGemItemID = dr.Item("SalesInvoiceGemItemID")
                            .SalesInvoiceDetailID = dr.Item("@SaleInvoiceDetailID")
                            .GemsCategoryID = dr.Item("@GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .GemsTW = dr.Item("GemsTW")
                            .Qty = dr.Item("Qty")
                            .UnitPrice = dr.Item("UnitPrice")
                            .Type = dr.Item("Type")
                            .Amount = dr.Item("Amount")
                            .GemsRemark = dr.Item("GemsRemark")
                            .GemTaxPer = dr.Item("GemTaxPer")
                            .GemTax = dr.Item("GemTax")

                        End With
                        bolRet = _objSalesInvoiceDA.InsertSaleInvoiceDetailGem(objGemItem)
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
                                .VoucherNo = obj.SaleInvoiceHeaderID
                                .CashTypeID = drRecord.Item("CashTypeID")
                                .ExchangeRate = drRecord.Item("ExchangeRate")
                                .Amount = drRecord.Item("Amount")
                            End With
                            bolRet = _objSalesInvoiceDA.InsertRecordCash(objRecord)
                        ElseIf drRecord.RowState = DataRowState.Modified Then
                            With objRecord
                                .RecordCashID = drRecord.Item("RecordCashID")
                                .VoucherNo = obj.SaleInvoiceHeaderID
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

        Public Function GetSalesInvoicePrint(ByVal SaleInvoiceID As String) As System.Data.DataTable Implements ISalesItemInvoiceController.GetSalesInvoicePrint
            Return _objSalesInvoiceDA.GetSalesInvoicePrint(SaleInvoiceID)
        End Function

        Public Function GetProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetProfitForSaleItem
            Return _objSalesInvoiceDA.GetProfitForSaleItem(argType, FromDate, ToDate, criStr)
        End Function

        Public Function DeleteSalesInvoice(ByVal obj As CommonInfo.SaleInvoiceHeaderInfo) As Boolean Implements ISalesItemInvoiceController.DeleteSalesInvoice
            Dim tmpdt As New DataTable
            tmpdt = _objSalesInvoiceDA.GetSalesInvoiceDetailByID(obj.SaleInvoiceHeaderID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    If tmpdr.Item("IsSaleReturn") = False Then
                        UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                    End If
                Next
            End If

            If _objSalesInvoiceDA.DeleteSalesInvoiceHeader(obj.SaleInvoiceHeaderID) Then

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       obj.SaleInvoiceHeaderID, _
                                       "Delete Sales Stock")
                Return True
            Else
                Return False
            End If
        End Function


        Public Function GetSalesInvoiceDataByHeaderIDAndItemCode(SaleInvoiceHeaderID As String, Optional ItemCode As String = "", Optional argForSaleIDStr As String = "") As DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceDataByHeaderIDAndItemCode
            Return _objSalesInvoiceDA.GetSalesInvoiceDataByHeaderIDAndItemCode(SaleInvoiceHeaderID, ItemCode, argForSaleIDStr)
        End Function

        Public Function GetSalesInvoiceGemDataBySaleDetailID(SaleInvoiceDetailID As String) As DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceGemDataBySaleDetailID
            Return _objSalesInvoiceDA.GetSalesInvoiceGemDataBySaleDetailID(SaleInvoiceDetailID)
        End Function
        Public Function GetAllSalesInvoiceForPurchase(Optional ByVal IsReuseBarcode As Boolean = False) As System.Data.DataTable Implements ISalesItemInvoiceController.GetAllSalesInvoiceForPurchase
            Return _objSalesInvoiceDA.GetAllSalesInvoiceForPurchase(IsReuseBarcode)
        End Function
        Public Function GetSalesInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceReport
            Return _objSalesInvoiceDA.GetSalesInvoiceReport(FromDate, ToDate, criStr)
        End Function

        Public Function GetSalesInvoiceReportForSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceReportForSummaryReport
            Return _objSalesInvoiceDA.GetSalesInvoiceReportForSummaryReport(FromDate, ToDate, criStr)
        End Function
        Public Function GetSalesInvoiceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceReportForTotal
            Return _objSalesInvoiceDA.GetSalesInvoiceReportForTotal(FromDate, ToDate, criStr)
        End Function

        Public Function GetSaleInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As SalesInvoiceDetailInfo Implements ISalesItemInvoiceController.GetSaleInvoiceDetailForTotal
            Return _objSalesInvoiceDA.GetSaleInvoiceDetailForTotal(FromDate, ToDate, criStr)
        End Function
        Public Function GetAllSaleInvoiceVoucherPrint(FromDate As Date, ToDate As Date, Optional criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetAllSaleInvoiceVoucherPrint
            Return _objSalesInvoiceDA.GetAllSaleInvoiceVoucherPrint(FromDate, ToDate, criStr)
        End Function
        Public Function GetOrderTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetOrderTaxSummaryReport
            Return _objSalesInvoiceDA.GetOrderTaxSummaryReport(FromDate, ToDate, criStr)
        End Function
        Public Function GetSaleTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetSaleTaxSummaryReport
            Return _objSalesInvoiceDA.GetSaleTaxSummaryReport(FromDate, ToDate, criStr)
        End Function

        Public Function GetTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetTaxSummaryReport
            Return _objSalesInvoiceDA.GetTaxSummaryReport(FromDate, ToDate, cristr)
        End Function
        Public Function GetSaleInvoiceGemDataBySaleInvoiceGemsItemID(ByVal SalesInvoiceGemItemID As String) As System.Data.DataTable Implements ISalesItemInvoiceController.GetSaleInvoiceGemDataBySaleInvoiceGemsItemID
            Return _objSalesInvoiceDA.GetSaleInvoiceGemDataBySaleInvoiceGemsItemID(SalesInvoiceGemItemID)
        End Function
        Public Function GetSalesInvoiceDetailPrintByID(ByVal SaleInvoiceID As String) As System.Data.DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceDetailPrintByID
            Return _objSalesInvoiceDA.GetSalesInvoiceDetailPrintByID(SaleInvoiceID)
        End Function
        Public Function GetSumProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceController.GetSumProfitForSaleItem
            Return _objSalesInvoiceDA.GetSumProfitForSaleItem(argType, FromDate, ToDate, criStr)
        End Function
        Public Function GetForSalePercentageReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal SortBy As String, Optional ByVal cristr As String = "") As DataTable Implements ISalesItemInvoiceController.GetForSalePercentageReport
            Return _objSalesInvoiceDA.GetForSalePercentageReport(FromDate, ToDate, SortBy, cristr)
        End Function
        Public Function GetForSaleGem(SaleInvoiceHeaderID As String) As DataTable Implements ISalesItemInvoiceController.GetForSaleGem
            Return _objSalesInvoiceDA.GetForSaleGem(SaleInvoiceHeaderID)
        End Function
        Public Function GetMostSaleItemData(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements ISalesItemInvoiceController.GetMostSaleItemData
            Return _objSalesInvoiceDA.GetMostSaleItemData(FromDate, ToDate, cristr)
        End Function
        Public Function GetSaleSummaryReportByDateANDByItemName(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements ISalesItemInvoiceController.GetSaleSummaryReportByDateANDByItemName
            Return _objSalesInvoiceDA.GetSaleSummaryReportByDateANDByItemName(FromDate, ToDate, cristr)
        End Function
        Public Function GetBalanceStockByValue(ByVal criStr As String) As DataTable Implements ISalesItemInvoiceController.GetBalanceStockByValue
            Return _objSalesInvoiceDA.GetBalanceStockByValue(criStr)
        End Function
        Public Function GetSalesInvoiceDataByCustomerIDAndItemCode(CustomerID As String, Optional criStr As String = "") As DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceDataByCustomerIDAndItemCode
            Return _objSalesInvoiceDA.GetSalesInvoiceDataByCustomerIDAndItemCode(CustomerID, criStr)
        End Function
        Public Function GetOtherCashDataByVoucherNo(ByVal SaleInvoiceHeaderID As String) As DataTable Implements ISalesItemInvoiceController.GetOtherCashDataByVoucherNo
            Return _objSalesInvoiceDA.GetOtherCashDataByVoucherNo(SaleInvoiceHeaderID)
        End Function
        Public Function GetSalesInvoiceDataSaleDetailID(Optional criStr As String = "") As DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceDataSaleDetailID
            Return _objSalesInvoiceDA.GetSalesInvoiceDataSaleDetailID(criStr)
        End Function
        Public Function GetAllSalesInvoiceDataByItemCode() As System.Data.DataTable Implements ISalesItemInvoiceController.GetAllSalesInvoiceDataByItemCode
            Return _objSalesInvoiceDA.GetAllSalesInvoiceDataByItemCode()
        End Function
        Public Function GetSalesInvoiceTaxVoucher(ByVal SaleInvoiceHeaderID As String) As DataTable Implements ISalesItemInvoiceController.GetSalesInvoiceTaxVoucher
            Return _objSalesInvoiceDA.GetSalesInvoiceTaxVoucher(SaleInvoiceHeaderID)
        End Function

        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements ISalesItemInvoiceController.UpdateRedeemID

            If _objSalesInvoiceDA.UpdateRedeemID(RedeemID, SaleInvoiceID) Then

                Return True
            Else
                Return False
            End If
        End Function
        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean Implements ISalesItemInvoiceController.UpdateTransactionID

            If _objSalesInvoiceDA.UpdateTransactionID(TransactionID, SaleInvoiceID) Then

                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

