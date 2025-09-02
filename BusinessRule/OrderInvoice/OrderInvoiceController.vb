Imports DataAccess.OrderInvoice
Imports CommonInfo
Imports DataAccess.SalesItem
Namespace OrderInvoice
    Public Class OrderInvoiceController
        Implements IOrderInvoiceController

#Region "Private Members"

        Private _objOrderInvoiceDA As IOrderInvoiceDA
        Private _objSalesItemDA As ISalesItemDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IOrderInvoiceController = New OrderInvoiceController

#End Region

#Region "Constructors"

        Private Sub New()
            _objOrderInvoiceDA = DataAccess.Factory.Instance.CreateOrderInvoiceDA
            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IOrderInvoiceController
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

        Public Function DeleteOrderInvoice(ByVal OrderInvoiceID As String) As Boolean Implements IOrderInvoiceController.DeleteOrderInvoice
            Dim dt As New DataTable
            dt = _objOrderInvoiceDA.GetReceiveDataByOrderInvocieID(OrderInvoiceID)

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    _objSalesItemDA.UpdateForSaleItemByOrderInvoiceID(dr.Item("OrderReceiveDetailID"))
                Next
            End If


            If _objOrderInvoiceDA.DeleteOrderInvoice(OrderInvoiceID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.OrderStock.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       OrderInvoiceID, _
                                                       "Delete Order Stock")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function DeleteOrderInvoiceReturn(ByVal OrderReturnHeaderID As String) As Boolean Implements IOrderInvoiceController.DeleteOrderInvoiceReturn
            Dim tmpdt As New DataTable
            Dim Obj As New OrderReturnHeader

            tmpdt = _objOrderInvoiceDA.GetOrderReturnDetailByHeaderID(OrderReturnHeaderID)
            If tmpdt.Rows.Count > 0 Then
                Obj = _objOrderInvoiceDA.GetOrderInvoiceReturnHeader(OrderReturnHeaderID)
                UpdateOrderReceiveByIsReturn(Obj.OrderInvoiceID, False, Now)

                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                Next
            End If

            If _objOrderInvoiceDA.DeleteOrderInvoiceReturn(OrderReturnHeaderID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.OrderStockReturn.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       OrderReturnHeaderID, _
                                                       "Delete Order Stock Return")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetOrderInvoice(ByVal OrderInvoiceID As String) As CommonInfo.OrderInvoiceInfo Implements IOrderInvoiceController.GetOrderInvoice
            Return _objOrderInvoiceDA.GetOrderInvoice(OrderInvoiceID)
        End Function
        Public Function SaveOrderInvoiceReceive(obj As OrderInvoiceInfo, _dtOrderInvoiceDetailItem As DataTable, _dtOrderGemsItem As DataTable) As Boolean Implements IOrderInvoiceController.SaveOrderInvoiceReceive

            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController

            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim bolRet As Boolean = True

            If obj.OrderInvoiceID = "" Then

                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.OrderStock.ToString)
                obj.OrderInvoiceID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.OrderDate)

                bolRet = _objOrderInvoiceDA.InsertOrderInvoice(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.OrderStock.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                                       obj.OrderInvoiceID, _
                                                                       "Insert Order Stock")

            Else

                bolRet = _objOrderInvoiceDA.UpdateOrderInvoice(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.OrderStock.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                                       obj.OrderInvoiceID, _
                                                                   "Update Order Stock")
            End If
            If bolRet = True Then

                For Each dr As DataRow In _dtOrderInvoiceDetailItem.Rows

                    Dim objOrderReceiveDetail As New OrderReceiveDetailInfo

                    If dr.RowState = DataRowState.Added Then
                        With objOrderReceiveDetail
                            .OrderReceiveDetailID = dr.Item("OrderReceiveDetailID")
                            .OrderInvoiceID = obj.OrderInvoiceID
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .GoldSmithID = dr.Item("GoldSmithID")
                            .Width = dr.Item("Width")
                            .Length = dr.Item("Length")
                            .OrderRate = dr.Item("OrderRate")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemPrice = dr.Item("GemPrice")
                            .DesignCharges = dr.Item("DesignCharges")
                            .PlatingFee = dr.Item("PlatingFee")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .MountingFee = dr.Item("MountingFee")
                            .TotalGemTK = dr.Item("TotalGemTK")
                            .TotalGemTG = dr.Item("TotalGemTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .Design = dr.Item("Design")
                            .IsDiamond = dr.Item("IsDiamond")

                        End With
                        bolRet = _objOrderInvoiceDA.InsertOrderReceiveDetail(objOrderReceiveDetail)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objOrderReceiveDetail
                            .OrderReceiveDetailID = dr.Item("OrderReceiveDetailID")
                            .OrderInvoiceID = obj.OrderInvoiceID
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldSmithID = dr.Item("GoldSmithID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .Width = dr.Item("Width")
                            .Length = dr.Item("Length")
                            .OrderRate = dr.Item("OrderRate")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemPrice = dr.Item("GemPrice")
                            .DesignCharges = dr.Item("DesignCharges")
                            .PlatingFee = dr.Item("PlatingFee")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .MountingFee = dr.Item("MountingFee")
                            .TotalGemTK = dr.Item("TotalGemTK")
                            .TotalGemTG = dr.Item("TotalGemTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .TotalAmount = dr.Item("TotalAmount")
                            .AddOrSub = dr.Item("AddOrSub")
                            .Design = dr.Item("Design")
                            .IsDiamond = dr.Item("IsDiamond")
                        End With
                        bolRet = _objOrderInvoiceDA.UpdateOrderReceiveDetail(objOrderReceiveDetail)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        _objSalesItemDA.UpdateForSaleItemByOrderInvoiceID(CStr(dr.Item("OrderReceiveDetailID", DataRowVersion.Original)))
                        Dim row As DataRow
                        Dim j As Integer = _dtOrderGemsItem.Rows.Count() - 1
                        While j >= 0
                            row = _dtOrderGemsItem.Rows(j)
                            If row.Item("OrderReceiveDetailID") = dr.Item("OrderReceiveDetailID", DataRowVersion.Original) Then
                                _dtOrderGemsItem.Rows.Remove(row)
                            End If
                            j = j - 1
                        End While
                        bolRet = _objOrderInvoiceDA.DeleteOrderReceiveDetail(CStr(dr.Item("OrderReceiveDetailID", DataRowVersion.Original)))
                    End If

                    If dr.RowState <> DataRowState.Deleted Then
                        Dim dt As New DataTable
                        dt = _objOrderInvoiceDA.GetOrderReceiveDetailGemByID(dr.Item("OrderReceiveDetailID"))

                        If dt.Rows.Count > 0 Then   'For User Change DiamondItemBarcodeNo 
                            For Each tmpdr As DataRow In dt.Rows
                                _objOrderInvoiceDA.DeleteOrderReceiveDetailGemsItemByGemsID(tmpdr.Item("OrderInvoiceGemsItemID"))
                            Next
                        End If

                    End If
                Next
            End If


            If bolRet = True Then
                For Each dr As DataRow In _dtOrderGemsItem.Rows
                    Dim objGemItem As New OrderInvoiceGemsItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objGemItem
                            .OrderInvoiceGemsItemID = dr.Item("OrderInvoiceGemsItemID")
                            .OrderReceiveDetailID = dr.Item("OrderReceiveDetailID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = IIf(IsDBNull(dr.Item("GemsName")) = True, "", dr.Item("GemsName"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, "0", dr.Item("YOrCOrG"))
                            .GemsTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
                            .UnitPrice = IIf(IsDBNull(dr.Item("UnitPrice")) = True, 0, dr.Item("UnitPrice"))
                            .Type = IIf(IsDBNull(dr.Item("Type")) = True, "", dr.Item("Type"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")) = True, 0, dr.Item("Amount"))
                            .IsCustomerGem = IIf(IsDBNull(dr.Item("IsCustomerGem")) = True, False, dr.Item("IsCustomerGem"))
                        End With

                        bolRet = _objOrderInvoiceDA.InsertOrderInvoiceItem(objGemItem)
                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objGemItem
                            .OrderInvoiceGemsItemID = dr.Item("OrderInvoiceGemsItemID")
                            .OrderReceiveDetailID = dr.Item("OrderReceiveDetailID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = IIf(IsDBNull(dr.Item("GemsName")) = True, "", dr.Item("GemsName"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, "0", dr.Item("YOrCOrG"))
                            .GemsTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
                            .UnitPrice = IIf(IsDBNull(dr.Item("UnitPrice")) = True, 0, dr.Item("UnitPrice"))
                            .Type = IIf(IsDBNull(dr.Item("Type")) = True, "", dr.Item("Type"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")) = True, 0, dr.Item("Amount"))
                            .IsCustomerGem = IIf(IsDBNull(dr.Item("IsCustomerGem")) = True, False, dr.Item("IsCustomerGem"))
                        End With

                        bolRet = _objOrderInvoiceDA.InsertOrderInvoiceItem(objGemItem)
                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objGemItem
                            .OrderInvoiceGemsItemID = dr.Item("OrderInvoiceGemsItemID")
                            .OrderReceiveDetailID = dr.Item("OrderReceiveDetailID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = IIf(IsDBNull(dr.Item("GemsName")) = True, "", dr.Item("GemsName"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, "0", dr.Item("YOrCOrG"))
                            .GemsTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
                            .UnitPrice = IIf(IsDBNull(dr.Item("UnitPrice")) = True, 0, dr.Item("UnitPrice"))
                            .Type = IIf(IsDBNull(dr.Item("Type")) = True, "", dr.Item("Type"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")) = True, 0, dr.Item("Amount"))
                            .IsCustomerGem = IIf(IsDBNull(dr.Item("IsCustomerGem")) = True, False, dr.Item("IsCustomerGem"))
                        End With
                        bolRet = _objOrderInvoiceDA.InsertOrderInvoiceItem(objGemItem)
                    End If
                Next
            End If
            Return bolRet
        End Function


        Public Function SaveOrderInvoiceReturn(ByVal objReturn As CommonInfo.OrderReturnHeader, ByVal _dtItemBarcode As System.Data.DataTable, ByVal _dtOrderInvoiceDetailGem As DataTable) As Boolean Implements IOrderInvoiceController.SaveOrderInvoiceReturn
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False
            'Dim StrODHeaderID As Integer
            If objReturn.OrderReturnHeaderID = "0" Then
                objReturn.OrderReturnHeaderID = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.OrderReturnHeader, EnumSetting.GenerateKeyType.OrderReturnHeader.ToString, Now)
                bolRet = _objOrderInvoiceDA.InsertOrderInvoiceReturn(objReturn)
                'If (StrODHeaderID > 0) Then
                '    bolRet = True
                'Else
                '    bolRet = False

                'End If
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.OrderStockReturn.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                                       objReturn.OrderInvoiceID, _
                                                                       "Insert Order Stock Return")

            Else

                bolRet = _objOrderInvoiceDA.UpdateOrderReturnHeader(objReturn)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.OrderStockReturn.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                                       objReturn.OrderInvoiceID, _
                                                                       "Update Order Stock Return")

                Dim tmpdt As New DataTable
                tmpdt = _objOrderInvoiceDA.GetOrderReturnDetailByHeaderID(objReturn.OrderReturnHeaderID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                    Next
                End If
            End If

            If bolRet = True Then
                Dim objOrderDetailInfo As New OrderInvoiceDetailInfo
                For Each dr As DataRow In _dtItemBarcode.Rows
                    If dr.RowState = DataRowState.Added Then
                        With objOrderDetailInfo
                            .OrderInvoiceDetailID = dr.Item("OrderInvoiceDetailID")
                            'If objReturn.OrderReturnHeaderID = "0" Then
                            '    .OrderReturnHeaderID = StrODHeaderID
                            'Else
                            .OrderReturnHeaderID = objReturn.OrderReturnHeaderID
                            'End If
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
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
                        End With
                        _objOrderInvoiceDA.InsertOrderInvoiceDetail(objOrderDetailInfo)
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, objReturn.ReturnDate)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objOrderDetailInfo
                            .OrderInvoiceDetailID = dr.Item("OrderInvoiceDetailID")
                            'If objReturn.OrderReturnHeaderID = "0" Then
                            '    .OrderReturnHeaderID = objReturn.OrderReturnHeaderID
                            'Else
                            .OrderReturnHeaderID = objReturn.OrderReturnHeaderID
                            'End If
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
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

                        End With
                        _objOrderInvoiceDA.UpdateOrderInvoiceDetail(objOrderDetailInfo)
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, objReturn.ReturnDate)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objOrderDetailInfo
                            .OrderInvoiceDetailID = dr.Item("OrderInvoiceDetailID")
                            'If objReturn.OrderReturnHeaderID = "0" Then
                            '    .OrderReturnHeaderID = StrODHeaderID
                            'Else
                            .OrderReturnHeaderID = objReturn.OrderReturnHeaderID
                            'End If
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
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
                            .ItemTax = dr.Item("ITemTax")
                        End With
                        _objOrderInvoiceDA.UpdateOrderInvoiceDetail(objOrderDetailInfo)
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, objReturn.ReturnDate)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        Dim row As DataRow
                        Dim j As Integer = _dtOrderInvoiceDetailGem.Rows.Count() - 1
                        While j >= 0
                            row = _dtOrderInvoiceDetailGem.Rows(j)
                            If row.Item("OrderInvoiceDetailID") = dr.Item("OrderInvoiceDetailID", DataRowVersion.Original) Then
                                _dtOrderInvoiceDetailGem.Rows.Remove(row)
                            End If
                            j = j - 1
                        End While
                        bolRet = _objOrderInvoiceDA.DeleteOrderInvoiceDetail(CStr(dr.Item("OrderInvoiceDetailID", DataRowVersion.Original)))
                    End If

                    If dr.RowState <> DataRowState.Deleted Then
                        Dim dt As New DataTable
                        dt = _objOrderInvoiceDA.GetOrderReturneGemsItemByDetailID(dr.Item("OrderInvoiceDetailID"))
                        If dt.Rows.Count > 0 Then
                            For Each tmpdr As DataRow In dt.Rows
                                _objOrderInvoiceDA.DeleteOrderReturnGemsItemByGemsID(tmpdr.Item("OrderReturnGemID"))
                            Next
                        End If
                    End If
                Next
            End If

            If bolRet = True Then
                For Each dr As DataRow In _dtOrderInvoiceDetailGem.Rows
                    Dim objGemItem As New OrderReturnGemsItemInfo
                    If dr.RowState = DataRowState.Added Then
                        With objGemItem
                            .OrderReturnGemID = dr.Item("OrderReturnGemID")
                            .OrderInvoiceDetailID = dr.Item("OrderInvoiceDetailID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .GemsTW = dr.Item("GemsTW")
                            .Qty = dr.Item("Qty")
                            .UnitPrice = dr.Item("UnitPrice")
                            .SaleType = dr.Item("SaleType")
                            .Amount = dr.Item("Amount")
                            .GemsRemark = dr.Item("GemsRemark")
                            .GemTaxPer = dr.Item("GemTaxPer")
                            .GemTax = dr.Item("GemTax")
                        End With

                        bolRet = _objOrderInvoiceDA.InsertOrderReturnGemItem(objGemItem)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objGemItem
                            .OrderReturnGemID = dr.Item("OrderReturnGemID")
                            .OrderInvoiceDetailID = dr.Item("OrderInvoiceDetailID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .GemsTW = dr.Item("GemsTW")
                            .Qty = dr.Item("Qty")
                            .UnitPrice = dr.Item("UnitPrice")
                            .SaleType = dr.Item("SaleType")
                            .Amount = dr.Item("Amount")
                            .GemsRemark = dr.Item("GemsRemark")
                            .GemTaxPer = dr.Item("GemTaxPer")
                            .GemTax = dr.Item("GemTax")
                        End With
                        bolRet = _objOrderInvoiceDA.InsertOrderReturnGemItem(objGemItem)
                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objGemItem
                            .OrderReturnGemID = dr.Item("OrderReturnGemID")
                            .OrderInvoiceDetailID = dr.Item("OrderInvoiceDetailID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .GemsTW = dr.Item("GemsTW")
                            .Qty = dr.Item("Qty")
                            .UnitPrice = dr.Item("UnitPrice")
                            .SaleType = dr.Item("SaleType")
                            .Amount = dr.Item("Amount")
                            .GemsRemark = dr.Item("GemsRemark")
                            .GemTaxPer = dr.Item("GemTaxPer")
                            .GemTax = dr.Item("GemTax")
                        End With
                        bolRet = _objOrderInvoiceDA.InsertOrderReturnGemItem(objGemItem)
                    End If
                Next
            End If

            If objReturn.OrderInvoiceID <> "0" Then
                Dim dtreceive As DataTable
                Dim dtreturn As New DataTable
                Dim ReceiveQTY As Integer = 0
                Dim ReturnQTY As Integer = 0
                dtreceive = _objOrderInvoiceDA.GetOrderReceiveDetail(objReturn.OrderInvoiceID)
                ReceiveQTY = dtreceive.Rows.Count
                dtreturn = _objOrderInvoiceDA.GetOrderReturnDetailByOrderInvoiceID(objReturn.OrderInvoiceID)
                ReturnQTY = dtreturn.Rows.Count

                If (ReceiveQTY = ReturnQTY) Then
                    UpdateOrderReceiveByIsReturn(objReturn.OrderInvoiceID, True, Now)
                ElseIf objReturn.OrderReturnHeaderID <> "0" Then
                    UpdateOrderReceiveByIsReturn(objReturn.OrderInvoiceID, False, Now)
                End If
            End If

            'If (bolRet = True) Then
            '    If objReturn.OrderReturnHeaderID = 0 Then
            '        Return StrODHeaderID
            '    Else
            '        Return objReturn.OrderReturnHeaderID
            '    End If
            'Else
            '    Return 0
            'End If
            Return bolRet
        End Function

        Private Sub UpdateOrderReceiveByIsReturn(ByVal argOrderInvoiceID As String, ByVal argIsReturn As Boolean, ByVal argRetrieveDate As Date)
            Dim objOrderInvoice As New CommonInfo.OrderInvoiceInfo
            With objOrderInvoice
                .OrderInvoiceID = argOrderInvoiceID
                .IsRetrieved = argIsReturn
                .OrderRetrieveDate = argRetrieveDate
            End With
            _objOrderInvoiceDA.UpdateOrderReceiveByIsReturn(objOrderInvoice)
        End Sub
        Public Function GetOrderInvoiceDetailReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IOrderInvoiceController.GetOrderInvoiceDetailReport
            Return _objOrderInvoiceDA.GetOrderInvoiceDetailReport(FromDate, ToDate, IsReturn, cristr)
        End Function

        Public Function GetOrderInvoicePrint(ByVal OrderInvoiceID As String) As System.Data.DataTable Implements IOrderInvoiceController.GetOrderInvoicePrint
            Return _objOrderInvoiceDA.GetOrderInvoicePrint(OrderInvoiceID)
        End Function

        Public Function GetOrderReturnPrint(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceController.GetOrderReturnPrint
            Return _objOrderInvoiceDA.GetOrderReturnPrint(OrderInvoiceID)
        End Function

        Public Function GetOrderReturnDetailReport(FromDate As Date, ToDate As Date, IsReturn As Boolean, Optional cristr As String = "") As Object Implements IOrderInvoiceController.GetOrderReturnDetailReport
            Return _objOrderInvoiceDA.GetOrderReturnDetailReport(FromDate, ToDate, IsReturn, cristr)
        End Function

        Public Function GetOrderInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As OrderInvoiceDetailInfo Implements IOrderInvoiceController.GetOrderInvoiceDetailForTotal
            Return _objOrderInvoiceDA.GetOrderInvoiceDetailForTotal(FromDate, ToDate, criStr)
        End Function

        Public Function GetOrderInvoiceDataByHeaderIDAndItemCode(OrderInvoiceID As String, Optional ItemCode As String = "", Optional ByVal argForSaleIDStr As String = "") As DataTable Implements IOrderInvoiceController.GetOrderInvoiceDataByHeaderIDAndItemCode
            Return _objOrderInvoiceDA.GetOrderInvoiceDataByHeaderIDAndItemCode(OrderInvoiceID, ItemCode, argForSaleIDStr)
        End Function

        Public Function GetOrderInvoiceGemDataByOrderDetailID(OrderInvoiceDetailID As String) As DataTable Implements IOrderInvoiceController.GetOrderInvoiceGemDataByOrderDetailID
            Return _objOrderInvoiceDA.GetOrderInvoiceGemDataByOrderDetailID(OrderInvoiceDetailID)
        End Function

        Public Function GetOrderInvoiceReportForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As OrderInvoiceInfo Implements IOrderInvoiceController.GetOrderInvoiceReportForTotal
            Return _objOrderInvoiceDA.GetOrderInvoiceReportForTotal(FromDate, ToDate, criStr)
        End Function

        Public Function GetOrderReturnReportForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As System.Data.DataTable Implements IOrderInvoiceController.GetOrderReturnReportForTotal
            Return _objOrderInvoiceDA.GetOrderReturnReportForTotal(FromDate, ToDate, criStr)
        End Function

        Public Function GetOrderInvoiceSummaryReport(FromDate As Date, ToDate As Date, IsReturn As Boolean, Optional cristr As String = "") As Object Implements IOrderInvoiceController.GetOrderReturnSummaryReport
            Return _objOrderInvoiceDA.GetOrderReturnSummaryReport(FromDate, ToDate, IsReturn, cristr)
        End Function

        Public Function GerOrderSummaryReport(FromDate As Date, ToDate As Date, IsReturn As Boolean, Optional cristr As String = "") As Object Implements IOrderInvoiceController.GerOrderSummaryReport
            Return _objOrderInvoiceDA.GerOrderSummaryReport(FromDate, ToDate, IsReturn, cristr)
        End Function

        Public Function GetAllOrderInvoiceVoucherPrint(FromDate As Date, ToDate As Date, Optional criStr As String = "") As System.Data.DataTable Implements IOrderInvoiceController.GetAllOrderInvoiceVoucherPrint
            Return _objOrderInvoiceDA.GetAllOrderInvoiceVoucherPrint(FromDate, ToDate, criStr)
        End Function

        Public Function GetAllOrderReturnVoucherPrint(FromDate As Date, ToDate As Date, Optional criStr As String = "") As System.Data.DataTable Implements IOrderInvoiceController.GetAllOrderReturnVoucherPrint
            Return _objOrderInvoiceDA.GetAllOrderReturnVoucherPrint(FromDate, ToDate, criStr)
        End Function

        Public Function GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID(ByVal OrderInvoiceDetailGemsID As String) As System.Data.DataTable Implements IOrderInvoiceController.GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID
            Return _objOrderInvoiceDA.GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID(OrderInvoiceDetailGemsID)
        End Function
        Public Function GetOrderReturnDetailPrint(OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceController.GetOrderReturnDetailPrint
            Return _objOrderInvoiceDA.GetOrderReturnDetailPrint(OrderReturnHeaderID)
        End Function

        Public Function GetOrderInvoiceHeaderID(OrderInvoiceID As String) As OrderInvoiceInfo Implements IOrderInvoiceController.GetOrderInvoiceHeaderID
            Return _objOrderInvoiceDA.GetOrderInvoiceHeaderID(OrderInvoiceID)
        End Function

        Public Function GetOrderReceiveDetail(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceController.GetOrderReceiveDetail
            Return _objOrderInvoiceDA.GetOrderReceiveDetail(OrderInvoiceID)
        End Function

        Public Function GetOrderInvoiceGemsItemHeaderID(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceController.GetOrderInvoiceGemsItemHeaderID
            Return _objOrderInvoiceDA.GetOrderInvoiceGemsItemHeaderID(OrderInvoiceID)
        End Function

        Public Function GetOrderReceivePrint(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceController.GetOrderReceivePrint
            Return _objOrderInvoiceDA.GetOrderReceivePrint(OrderInvoiceID)
        End Function

        Public Function GetAllOrderReceive() As DataTable Implements IOrderInvoiceController.GetAllOrderReceive
            Return _objOrderInvoiceDA.GetAllOrderReceive()
        End Function

        Public Function GetOrderGemsByReceive(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceController.GetOrderGemsByReceive
            Return _objOrderInvoiceDA.GetOrderGemsByReceive(OrderInvoiceID)
        End Function

        Public Function GetOrderReceiveDetailID(OrderReceiveDetailID As String) As OrderReceiveDetailInfo Implements IOrderInvoiceController.GetOrderReceiveDetailID
            Return _objOrderInvoiceDA.GetOrderReceiveDetailID(OrderReceiveDetailID)
        End Function

        Public Function GetAllOrderReceiveHeader() As DataTable Implements IOrderInvoiceController.GetAllOrderReceiveHeader
            Return _objOrderInvoiceDA.GetAllOrderReceiveHeader()
        End Function
        Public Function GetOrderInvoiceInfoByDetailID(ByVal OrderReceiveDetailID As String) As CommonInfo.OrderInvoiceInfo Implements IOrderInvoiceController.GetOrderInvoiceInfoByDetailID
            Return _objOrderInvoiceDA.GetOrderInvoiceInfoByDetailID(OrderReceiveDetailID)
        End Function

        Public Function GetAllOrderInvoiceOrderList() As DataTable Implements IOrderInvoiceController.GetAllOrderInvoiceOrderList
            Return _objOrderInvoiceDA.GetAllOrderInvoiceOrderList()
        End Function
        Public Function GetBalanceAmountByOrderInvoiceID(ByVal OrderInvoiceID As String, Optional OrderReturnHeaderID As String = "") As DataTable Implements IOrderInvoiceController.GetBalanceAmountByOrderInvoiceID
            Return _objOrderInvoiceDA.GetBalanceAmountByOrderInvoiceID(OrderInvoiceID, OrderReturnHeaderID)
        End Function
        Public Function GetOrderInvoiceReturnHeader(ByVal OrderReturnHeaderID As String) As OrderReturnHeader Implements IOrderInvoiceController.GetOrderInvoiceReturnHeader
            Return _objOrderInvoiceDA.GetOrderInvoiceReturnHeader(OrderReturnHeaderID)
        End Function

        Public Function GetAllOrderReturnHeader() As DataTable Implements IOrderInvoiceController.GetAllOrderReturnHeader
            Return _objOrderInvoiceDA.GetAllOrderReturnHeader()
        End Function
        Public Function GetOrderReturnDetailByHeaderID(ByVal OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceController.GetOrderReturnDetailByHeaderID
            Return _objOrderInvoiceDA.GetOrderReturnDetailByHeaderID(OrderReturnHeaderID)
        End Function
        Public Function GetOrderReturnGemDataByHeaderID(ByVal OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceController.GetOrderReturnGemDataByHeaderID
            Return _objOrderInvoiceDA.GetOrderReturnGemDataByHeaderID(OrderReturnHeaderID)
        End Function
        Public Function GetOrderForItemName(ByVal OrderInvoiceID As String) As DataTable Implements IOrderInvoiceController.GetOrderForItemName
            Return _objOrderInvoiceDA.GetOrderForItemName(OrderInvoiceID)
        End Function
        Public Function GetOrderReturnGem(ByVal OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceController.GetOrderReturnGem
            Return _objOrderInvoiceDA.GetOrderReturnGem(OrderReturnHeaderID)
        End Function
        Public Function GetOrderTaxVoucher(ByVal OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceController.GetOrderTaxVoucher
            Return _objOrderInvoiceDA.GetOrderTaxVoucher(OrderReturnHeaderID)
        End Function
    End Class
End Namespace

