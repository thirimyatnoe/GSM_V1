Imports DataAccess.PurchaseItem
Imports DataAccess.SalesItem
Imports DataAccess.SalesItemInvoice
Imports DataAccess.OrderInvoice
Imports DataAccess.SaleGems
Imports DataAccess.SaleLooseDiamond
Imports CommonInfo
Namespace PurchaseItem
    Public Class PurchaseItemController
        Implements IPurchaseItemController
#Region "Private Members"

        Private _objPurchaseItemDA As IPurchaseItemDA
        Private _objSaleItemDA As ISalesItemDA
        Private _objSaleInvoiceDA As ISalesItemInvoiceDA
        Private _objSaleLooseDiamondDA As ISaleLooseDiamondDA
        Private _objSaleGemsDA As ISaleGemsDA

        Private _objOrderInvoiceDA As IOrderInvoiceDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IPurchaseItemController = New PurchaseItemController

#End Region

#Region "Constructors"

        Private Sub New()
            _objPurchaseItemDA = DataAccess.Factory.Instance.CreatePurchaseItemDA
            _objSaleItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
            _objSaleInvoiceDA = DataAccess.Factory.Instance.CreateSalesItemInvoiceDA
            _objSaleLooseDiamondDA = DataAccess.Factory.Instance.CreateSaleLooseDiamondDA
            _objOrderInvoiceDA = DataAccess.Factory.Instance.CreateOrderInvoiceDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IPurchaseItemController
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
            _objSaleItemDA.UpdateSaleItemIsExit(objSaleItem)
        End Sub

        Private Sub UpdateSalesItemByIsClosed(ByVal argForSaleID As String, ByVal argIsClose As Boolean)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsClosed = argIsClose
            End With
            _objSaleItemDA.UpdateSaleItemIsClose(objSaleItem)
        End Sub
        Private Sub UpdateSalesItemByIsOrder(ByVal argForSaleID As String, ByVal argIsClose As Boolean)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsOrder = argIsClose
            End With
            _objSaleItemDA.UpdateSaleItemIsOrder(objSaleItem)
        End Sub
        Private Sub UpdateSaleInvoiceDetailByIsReturn(ByVal argSaleInvoiceDetailID As String, ByVal argIsReturn As Boolean)
            Dim objSaleInvoice As New CommonInfo.SalesInvoiceDetailInfo
            With objSaleInvoice
                .SaleInvoiceDetailID = argSaleInvoiceDetailID
                .IsReturn = argIsReturn
            End With
            _objSaleInvoiceDA.UpdateSaleInvoiceDetailByIsReturn(objSaleInvoice)
        End Sub
        Private Sub UpdateConsignmentSaleDetailByIsReturn(ByVal argConsignmentSaleItemID As String, ByVal argIsReturn As Boolean)
            Dim objConsignmentSaleItem As New CommonInfo.ConsignmentSaleItemInfo
            With objConsignmentSaleItem
                .ConsignmentSaleItemID = argConsignmentSaleItemID
                .IsReturn = argIsReturn
            End With
            _objSaleInvoiceDA.UpdateConsignmentSaleDetailByIsReturn(objConsignmentSaleItem)
        End Sub

        Private Sub UpdateSaleGemsItemByIsReturn(ByVal argSaleGemsItemID As String, ByVal argIsReturn As Boolean)
            Dim objSaleGemsItem As New CommonInfo.SaleGemsItemInfo
            With objSaleGemsItem
                .SaleGemsItemID = argSaleGemsItemID
                .IsReturn = argIsReturn
            End With
            _objSaleInvoiceDA.UpdateSaleGemsItemByIsReturn(objSaleGemsItem)
        End Sub
        Private Sub UpdateSaleLooseDiamondDetailByIsReturn(ByVal argSaleLooseDiamondDetailID As String, ByVal argIsReturn As Boolean)
            Dim objSaleLooseDiamondItem As New CommonInfo.SaleLooseDiamondDetailInfo
            With objSaleLooseDiamondItem
                .SaleLooseDiamondDetailID = argSaleLooseDiamondDetailID
                .IsReturn = argIsReturn
            End With
            _objSaleInvoiceDA.UpdateSaleLooseDiamondByIsReturn(objSaleLooseDiamondItem)
        End Sub

        Private Sub UpdateOrderReturnDetailByIsReturn(ByVal argOrderInvoiceDetailID As String, ByVal argIsReturn As Boolean)
            Dim objOrderInvoice As New CommonInfo.OrderInvoiceDetailInfo
            With objOrderInvoice
                .OrderInvoiceDetailID = argOrderInvoiceDetailID
                .IsReturn = argIsReturn
            End With
            _objOrderInvoiceDA.UpdateOrderReturnDetailByIsReturn(objOrderInvoice)
        End Sub
        Public Function DeletePuchaseHeader(Obj As PurchaseHeaderInfo) As Boolean Implements IPurchaseItemController.DeletePuchaseHeader

            If (_objPurchaseItemDA.CheckIsUseInSaleInvoiceHeader(Obj.PurchaseHeaderID)) = False And (_objPurchaseItemDA.CheckIsUseInSaleVolumeHeader(Obj.PurchaseHeaderID)) = False Then
                If Global_IsReuseBarcode = False Then
                    Dim tmpdt As New DataTable
                    tmpdt = _objPurchaseItemDA.GetPurchaseDetailByID(Obj.PurchaseHeaderID)
                    If tmpdt.Rows.Count > 0 Then
                        For Each tmpdr As DataRow In tmpdt.Rows
                            If tmpdr.Item("IsOrder") = True Then
                                UpdateOrderReturnDetailByIsReturn(tmpdr.Item("SaleInvoiceDetailID"), False)
                            Else
                                UpdateSaleInvoiceDetailByIsReturn(tmpdr.Item("SaleInvoiceDetailID"), False)
                            End If
                        Next
                    End If
                End If

                If _objPurchaseItemDA.DeletePuchaseHeader(Obj.PurchaseHeaderID) Then
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.PurchaseStock.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                          Obj.PurchaseHeaderID, _
                                          "Delete Purchase Stock")
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Function

        Public Function GetAllPuchaseHeader() As DataTable Implements IPurchaseItemController.GetAllPuchaseHeader
            Return _objPurchaseItemDA.GetAllPuchaseHeader()
        End Function

        Public Function GetPurchaseDetailGemByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseDetailGemByID
            Return _objPurchaseItemDA.GetPurchaseDetailGemByID(PurchaseHeaderID)
        End Function

        Public Function GetPurchaseDetailByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseDetailByID
            Return _objPurchaseItemDA.GetPurchaseDetailByID(PurchaseHeaderID)
        End Function

        Public Function GetPurchaseHeaderByID(PurchaseHeaderID As String) As PurchaseHeaderInfo Implements IPurchaseItemController.GetPurchaseHeaderByID
            Return _objPurchaseItemDA.GetPurchaseHeaderByID(PurchaseHeaderID)
        End Function

        Public Function SavePuchaseItem(Obj As PurchaseHeaderInfo, _dtDetail As DataTable, _dtGems As DataTable) As Boolean Implements IPurchaseItemController.SavePuchaseItem
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim bolRet As Boolean = False

            If Obj.PurchaseHeaderID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.PurchaseStock.ToString)
                Obj.PurchaseHeaderID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, Obj.PurchaseDate)
                bolRet = _objPurchaseItemDA.InsertPuchaseHeader(Obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       Obj.PurchaseHeaderID, _
                                       "Insert Purchase Stock")


            Else

                bolRet = _objPurchaseItemDA.UpdatePuchaseHeader(Obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       Obj.PurchaseHeaderID, _
                                       "Update Purchase Stock")



                Dim tmpdt As New DataTable
                tmpdt = _objPurchaseItemDA.GetPurchaseDetailByID(Obj.PurchaseHeaderID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        If tmpdr("SaleInvoiceDetailID") <> "" Then
                            If tmpdr.Item("IsOrder") = True Then
                                UpdateOrderReturnDetailByIsReturn(tmpdr.Item("SaleInvoiceDetailID"), False)
                            Else
                                UpdateSaleInvoiceDetailByIsReturn(tmpdr.Item("SaleInvoiceDetailID"), False)
                            End If
                        End If
                        If tmpdr("ConsignmentSaleItemID") <> "" Then
                            UpdateConsignmentSaleDetailByIsReturn(tmpdr.Item("ConsignmentSaleItemID"), False)
                        End If
                        If tmpdr("SaleGemsItemID") <> "" Then
                            UpdateSaleGemsItemByIsReturn(tmpdr.Item("SaleGemsItemID"), False)
                        End If
                        'If tmpdr("SaleLooseDiamondDetailID") <> "" Then
                        '    UpdateSaleLooseDiamondDetailByIsReturn(tmpdr.Item("SaleLooseDiamondDetailID"), False)
                        'End If
                    Next
                End If

                '' for SaleInvoice Change Stock
                Dim dtItem As New DataTable
                dtItem = _objPurchaseItemDA.GetSaleInvoiceDataByHeaderID(Obj.PurchaseHeaderID)
                If dtItem.Rows.Count > 0 Then
                    For Each tmpItem As DataRow In dtItem.Rows
                        If Obj.IsChange = False Then
                            UpdateSaleInvoiceDataByPurchaseHeaderID(tmpItem.Item("PurchaseHeaderID"), 0)
                        End If
                        UpdateSaleInvoiceDataByPurchaseHeaderID(tmpItem.Item("PurchaseHeaderID"), (Obj.AllTotalAmount - Obj.AllAddOrSub))
                    Next
                End If

                '' for SaleVolumeInvoice
                Dim dtVolume As New DataTable
                dtVolume = _objPurchaseItemDA.GetSaleVolumeInvoiceDataByHeaderID(Obj.PurchaseHeaderID)
                If dtVolume.Rows.Count > 0 Then
                    For Each tmpItem As DataRow In dtVolume.Rows
                        If Obj.IsChange = False Then
                            UpdateSaleVolumeInvoiceDataByPurchaseHeaderID(tmpItem.Item("PurchaseHeaderID"), 0)
                        End If
                        UpdateSaleVolumeInvoiceDataByPurchaseHeaderID(tmpItem.Item("PurchaseHeaderID"), (Obj.AllTotalAmount - Obj.AllAddOrSub))
                    Next
                End If

                '' for SaleLooseDiamond
                Dim dtDiamond As New DataTable
                dtDiamond = _objPurchaseItemDA.GetSaleLooseDiamondInvoiceDataByHeaderID(Obj.PurchaseHeaderID)
                If dtDiamond.Rows.Count > 0 Then
                    For Each tmpItem As DataRow In dtDiamond.Rows
                        If Obj.IsChange = False Then
                            UpdateSaleLooseDiamondInvoiceDataByPurchaseHeaderID(tmpItem.Item("PurchaseHeaderID"), 0)
                        End If
                        UpdateSaleLooseDiamondInvoiceDataByPurchaseHeaderID(tmpItem.Item("PurchaseHeaderID"), (Obj.AllTotalAmount - Obj.AllAddOrSub))
                    Next
                End If

            End If

            If bolRet Then
                For Each dr As DataRow In _dtDetail.Rows
                    Dim objPDetail As New CommonInfo.PurchaseDetailInfo
                    If dr.RowState = DataRowState.Added Then
                        With objPDetail
                            .PurchaseDetailID = dr.Item("PurchaseDetailID")
                            .PurchaseHeaderID = Obj.PurchaseHeaderID
                            .SaleInvoiceDetailID = IIf(IsDBNull(dr.Item("SaleInvoiceDetailID")), "", dr.Item("SaleInvoiceDetailID"))
                            .ConsignmentSaleItemID = IIf(IsDBNull(dr.Item("ConsignmentSaleItemID")), "", dr.Item("ConsignmentSaleItemID"))
                            .SaleGemsItemID = IIf(IsDBNull(dr.Item("SaleGemsItemID")), "", dr.Item("SaleGemsItemID"))
                            .BarcodeNo = IIf(IsDBNull(dr.Item("BarcodeNo")), "", dr.Item("BarcodeNo"))
                            .ForSaleID = IIf(IsDBNull(dr.Item("ForSaleID")), "", dr.Item("ForSaleID"))
                            .OldSaleAmount = IIf(IsDBNull(dr.Item("OldSaleAmount")), 0, dr.Item("OldSaleAmount"))
                            .ItemCategoryID = IIf(IsDBNull(dr.Item("ItemCategoryID")), "", dr.Item("ItemCategoryID"))
                            .ItemNameID = IIf(IsDBNull(dr.Item("ItemNameID")), "", dr.Item("ItemNameID"))
                            .GoldQualityID = IIf(IsDBNull(dr.Item("GoldQualityID")), "", dr.Item("GoldQualityID"))
                            .CurrentPrice = dr.Item("CurrentPrice")
                            .SaleRate = dr.Item("SaleRate")
                            .Length = IIf(IsDBNull(dr.Item("Length")), "-", dr.Item("Length"))
                            .QTY = dr.Item("QTY")
                            .TotalTK = dr.Item("TotalTK")
                            .TotalTG = dr.Item("TotalTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .TotalGemTK = dr.Item("TotalGemTK")
                            .TotalGemTG = dr.Item("TotalGemTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .PWasteTK = dr.Item("PWasteTK")
                            .PWasteTG = dr.Item("PWasteTG")
                            .IsDamage = dr.Item("IsDamage")
                            .IsChange = dr.Item("IsChange")
                            .TotalAmount = dr.Item("TotalAmount")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .FixType = dr.Item("FixType")
                            .GemTW = dr.Item("GemTW")
                            .ItemName = IIf(IsDBNull(dr.Item("GemsName")), "", dr.Item("GemsName"))
                            .IsClose = "0" 'dr.Item("IsClose")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
                            .IsDone = dr.Item("IsDone")
                            .DoneAmount = dr.Item("DoneAmount")
                            .IsSalePercent = dr.Item("IsSalePercent")
                            .SalePercent = dr.Item("SalePercent")
                            .SalePercentAmount = dr.Item("SalePercentAmount")
                            .AddSub = dr.Item("AddSub")
                            .IsShop = IIf(IsDBNull(dr.Item("IsShop")), False, dr.Item("IsShop"))
                            .IsOrder = IIf(IsDBNull(dr.Item("IsOrder")), False, dr.Item("IsOrder"))
                            .SaleLooseDiamondDetailID = IIf(IsDBNull(dr.Item("SaleLooseDiamondDetailID")), "", dr.Item("SaleLooseDiamondDetailID"))
                            .PGemsCategoryID = IIf(IsDBNull(dr.Item("PGemsCategoryID")), "", dr.Item("PGemsCategoryID"))
                            .PGemsName = IIf(IsDBNull(dr.Item("PGemsName")), "", dr.Item("PGemsName"))
                            .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                            .Shape = IIf(IsDBNull(dr.Item("Shape")), "", dr.Item("Shape"))
                            .Clarity = IIf(IsDBNull(dr.Item("Clarity")), "", dr.Item("Clarity"))
                        End With
                        bolRet = _objPurchaseItemDA.InsertPuchaseDetail(objPDetail)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objPDetail
                            .PurchaseDetailID = dr.Item("PurchaseDetailID")
                            .PurchaseHeaderID = Obj.PurchaseHeaderID
                            .SaleInvoiceDetailID = IIf(IsDBNull(dr.Item("SaleInvoiceDetailID")), "", dr.Item("SaleInvoiceDetailID"))
                            .ConsignmentSaleItemID = IIf(IsDBNull(dr.Item("ConsignmentSaleItemID")), "", dr.Item("ConsignmentSaleItemID"))
                            .SaleGemsItemID = IIf(IsDBNull(dr.Item("SaleGemsItemID")), "", dr.Item("SaleGemsItemID"))
                            .BarcodeNo = IIf(IsDBNull(dr.Item("BarcodeNo")), "", dr.Item("BarcodeNo"))
                            .ForSaleID = IIf(IsDBNull(dr.Item("ForSaleID")), "", dr.Item("ForSaleID"))
                            .OldSaleAmount = IIf(IsDBNull(dr.Item("OldSaleAmount")), 0, dr.Item("OldSaleAmount"))
                            .ItemCategoryID = IIf(IsDBNull(dr.Item("ItemCategoryID")), "", dr.Item("ItemCategoryID"))
                            .ItemNameID = IIf(IsDBNull(dr.Item("ItemNameID")), "", dr.Item("ItemNameID"))
                            .GoldQualityID = IIf(IsDBNull(dr.Item("GoldQualityID")), "", dr.Item("GoldQualityID"))
                            .CurrentPrice = dr.Item("CurrentPrice")
                            .SaleRate = dr.Item("SaleRate")
                            .Length = IIf(IsDBNull(dr.Item("Length")), "-", dr.Item("Length"))
                            .QTY = dr.Item("QTY")
                            .TotalTK = dr.Item("TotalTK")
                            .TotalTG = dr.Item("TotalTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .TotalGemTK = dr.Item("TotalGemTK")
                            .TotalGemTG = dr.Item("TotalGemTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .PWasteTK = dr.Item("PWasteTK")
                            .PWasteTG = dr.Item("PWasteTG")
                            .IsDamage = dr.Item("IsDamage")
                            .IsChange = dr.Item("IsChange")
                            .TotalAmount = dr.Item("TotalAmount")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .FixType = dr.Item("FixType")
                            .GemTW = dr.Item("GemTW")
                            .ItemName = dr.Item("GemsName")
                            .IsClose = dr.Item("IsClose")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
                            .IsDone = dr.Item("IsDone")
                            .DoneAmount = dr.Item("DoneAmount")
                            .IsSalePercent = dr.Item("IsSalePercent")
                            .SalePercent = dr.Item("SalePercent")
                            .SalePercentAmount = dr.Item("SalePercentAmount")
                            .AddSub = dr.Item("AddSub")
                            .IsShop = IIf(IsDBNull(dr.Item("IsShop")), False, dr.Item("IsShop"))
                            .IsOrder = IIf(IsDBNull(dr.Item("IsOrder")), False, dr.Item("IsOrder"))
                            .SaleLooseDiamondDetailID = IIf(IsDBNull(dr.Item("SaleLooseDiamondDetailID")), "", dr.Item("SaleLooseDiamondDetailID"))
                            .PGemsCategoryID = IIf(IsDBNull(dr.Item("PGemsCategoryID")), "", dr.Item("PGemsCategoryID"))
                            .PGemsName = IIf(IsDBNull(dr.Item("PGemsName")), "", dr.Item("PGemsName"))
                            .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                            .Shape = IIf(IsDBNull(dr.Item("Shape")), "", dr.Item("Shape"))
                            .Clarity = IIf(IsDBNull(dr.Item("Clarity")), "", dr.Item("Clarity"))
                        End With
                        bolRet = _objPurchaseItemDA.UpdatePuchaseDetail(objPDetail)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objPDetail
                            .PurchaseDetailID = dr.Item("PurchaseDetailID")
                            .PurchaseHeaderID = Obj.PurchaseHeaderID
                            .SaleInvoiceDetailID = IIf(IsDBNull(dr.Item("SaleInvoiceDetailID")), "", dr.Item("SaleInvoiceDetailID"))
                            .ConsignmentSaleItemID = IIf(IsDBNull(dr.Item("ConsignmentSaleItemID")), "", dr.Item("ConsignmentSaleItemID"))
                            .SaleGemsItemID = IIf(IsDBNull(dr.Item("SaleGemsItemID")), "", dr.Item("SaleGemsItemID"))
                            .BarcodeNo = IIf(IsDBNull(dr.Item("BarcodeNo")), "", dr.Item("BarcodeNo"))
                            .ForSaleID = IIf(IsDBNull(dr.Item("ForSaleID")), "", dr.Item("ForSaleID"))
                            .OldSaleAmount = IIf(IsDBNull(dr.Item("OldSaleAmount")), 0, dr.Item("OldSaleAmount"))
                            .ItemCategoryID = IIf(IsDBNull(dr.Item("ItemCategoryID")), "", dr.Item("ItemCategoryID"))
                            .ItemNameID = IIf(IsDBNull(dr.Item("ItemNameID")), "", dr.Item("ItemNameID"))
                            .GoldQualityID = IIf(IsDBNull(dr.Item("GoldQualityID")), "", dr.Item("GoldQualityID"))
                            .CurrentPrice = dr.Item("CurrentPrice")
                            .SaleRate = dr.Item("SaleRate")
                            .Length = IIf(IsDBNull(dr.Item("Length")), "-", dr.Item("Length"))
                            .QTY = dr.Item("QTY")
                            .TotalTK = dr.Item("TotalTK")
                            .TotalTG = dr.Item("TotalTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .TotalGemTK = dr.Item("TotalGemTK")
                            .TotalGemTG = dr.Item("TotalGemTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .PWasteTK = dr.Item("PWasteTK")
                            .PWasteTG = dr.Item("PWasteTG")
                            .IsDamage = dr.Item("IsDamage")
                            .IsChange = dr.Item("IsChange")
                            .TotalAmount = dr.Item("TotalAmount")
                            .YOrCOrG = dr.Item("YOrCOrG")
                            .FixType = dr.Item("FixType")
                            .GemTW = dr.Item("GemTW")
                            .ItemName = dr.Item("GemsName")
                            .IsClose = "0" 'dr.Item("IsClose")
                            .GoldPrice = dr.Item("GoldPrice")
                            .GemsPrice = dr.Item("GemsPrice")
                            .IsDone = dr.Item("IsDone")
                            .DoneAmount = dr.Item("DoneAmount")
                            .IsSalePercent = dr.Item("IsSalePercent")
                            .SalePercent = dr.Item("SalePercent")
                            .SalePercentAmount = dr.Item("SalePercentAmount")
                            .AddSub = dr.Item("AddSub")
                            .IsShop = IIf(IsDBNull(dr.Item("IsShop")), False, dr.Item("IsShop"))
                            .IsOrder = IIf(IsDBNull(dr.Item("IsOrder")), False, dr.Item("IsOrder"))
                            .SaleLooseDiamondDetailID = IIf(IsDBNull(dr.Item("SaleLooseDiamondDetailID")), "", dr.Item("SaleLooseDiamondDetailID"))
                            .PGemsCategoryID = IIf(IsDBNull(dr.Item("PGemsCategoryID")), "", dr.Item("PGemsCategoryID"))
                            .PGemsName = IIf(IsDBNull(dr.Item("PGemsName")), "", dr.Item("PGemsName"))
                            .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                            .Shape = IIf(IsDBNull(dr.Item("Shape")), "", dr.Item("Shape"))
                            .Clarity = IIf(IsDBNull(dr.Item("Clarity")), "", dr.Item("Clarity"))
                        End With
                        bolRet = _objPurchaseItemDA.UpdatePuchaseDetail(objPDetail)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objPurchaseItemDA.DeletePuchasDetail(CStr(dr.Item("PurchaseDetailID", DataRowVersion.Original)))
                        If _dtGems.Rows.Count > 0 Then
                            Dim i As Integer = _dtGems.Rows.Count - 1
                            Dim row As DataRow
                            While i >= 0
                                row = _dtGems.Rows(i)
                                If row.Item("PurchaseDetailID") = CStr(dr.Item("PurchaseDetailID", DataRowVersion.Original)) Then
                                    _dtGems.Rows.Remove(row)
                                End If
                                i = i - 1
                            End While
                        End If
                    End If
                    '''''''''''''''''''
                    If dr.RowState <> DataRowState.Deleted Then
                        If dr.Item("SaleInvoiceDetailID") <> "" Then
                            If dr.Item("IsOrder") = True Then
                                UpdateOrderReturnDetailByIsReturn(dr.Item("SaleInvoiceDetailID"), True) ''For OrderReturnDetailTable
                            Else
                                UpdateSaleInvoiceDetailByIsReturn(dr.Item("SaleInvoiceDetailID"), True) ''For SaleInvoiceDetailTable
                            End If
                        End If
                        If IsDBNull(dr.Item("ConsignmentSaleItemID")) Then
                            dr.Item("ConsignmentSaleitemID") = "0"
                        End If
                        If dr.Item("ConsignmentSaleItemID") <> "0" Then
                            UpdateConsignmentSaleDetailByIsReturn(dr.Item("ConsignmentSaleItemID"), True)
                        End If

                        If dr.Item("SaleGemsItemID") <> "" Then
                            UpdateSaleGemsItemByIsReturn(dr.Item("SaleGemsItemID"), True)  'For Sale Gems
                        End If
                        If dr.Item("SaleLooseDiamondDetailID") <> "" Then
                            UpdateSaleLooseDiamondDetailByIsReturn(dr.Item("SaleLooseDiamondDetailID"), True)  'For Sale LooseDiamond
                        End If
                        ''''''''''''''''''''''''''
                        ''For Delete GemTable
                        Dim dt As New DataTable
                        dt = _objPurchaseItemDA.GetPurchaseGemsItemByDetailID(dr.Item("PurchaseDetailID"))
                        If dt.Rows.Count > 0 Then
                            For Each tmpdr As DataRow In dt.Rows
                                _objPurchaseItemDA.DeletePuchaseGems(tmpdr.Item("PurchaseGemID"))
                            Next
                        End If
                    End If


                Next
            End If


            'For Gems
            For Each drGems As DataRow In _dtGems.Rows
                Dim objPGems As New CommonInfo.PurchaseGemInfo
                If drGems.RowState <> DataRowState.Deleted Then
                    With objPGems
                        .PurchaseGemID = drGems.Item("PurchaseGemID")
                        .PurchaseDetailID = drGems.Item("PurchaseDetailID")
                        .GemsCategoryID = drGems.Item("GemsCategoryID")
                        .GemsName = IIf(IsDBNull(drGems.Item("GemsName")) = True, "-", drGems.Item("GemsName"))
                        .GemsTK = IIf(IsDBNull(drGems.Item("GemsTK")), "0", drGems.Item("GemsTK"))
                        .GemsTG = IIf(IsDBNull(drGems.Item("GemsTG")), "0", drGems.Item("GemsTG"))
                        .YOrCOrG = IIf(IsDBNull(drGems.Item("YOrCOrG")) = True, 0, drGems.Item("YOrCOrG"))
                        .GemTW = IIf(IsDBNull(drGems.Item("GemTW")) = True, 0, drGems.Item("GemTW"))
                        .QTY = IIf(IsDBNull(drGems.Item("QTY")) = True, 0, drGems.Item("QTY"))
                        .FixType = IIf(drGems.Item("FixType") = "_", 0, drGems.Item("FixType"))
                        .Discount = IIf(IsDBNull(drGems.Item("Discount")) = True, 0, drGems.Item("Discount"))
                        .PurchaseRate = IIf(drGems.Item("PurchaseRate") = "0", 0, drGems.Item("PurchaseRate"))
                        .Amount = drGems.Item("Amount")
                    End With
                    bolRet = _objPurchaseItemDA.InsertPuchasGems(objPGems)


                ElseIf drGems.RowState = DataRowState.Deleted Then
                    bolRet = _objPurchaseItemDA.DeletePuchaseGems(drGems.Item("PurchaseGemID"))
                End If
            Next


            Return bolRet
        End Function

        Private Sub UpdateSaleInvoiceDataByPurchaseHeaderID(ByVal argForSaleID As String, ByVal PurchaseAmount As Integer)
            Dim objSaleHeader As New CommonInfo.SaleInvoiceHeaderInfo
            With objSaleHeader
                .PurchaseHeaderID = argForSaleID
                .PurchaseAmount = PurchaseAmount
            End With
            _objPurchaseItemDA.UpdateSaleInvoiceDataByPurchaseHeaderID(objSaleHeader)
        End Sub

        Private Sub UpdateSaleVolumeInvoiceDataByPurchaseHeaderID(ByVal argForSaleID As String, ByVal PurchaseAmount As Integer)
            Dim objSaleVolume As New CommonInfo.SalesVolumeHeaderInfo
            With objSaleVolume
                .PurchaseHeaderID = argForSaleID
                .PurchaseAmount = PurchaseAmount
            End With
            _objPurchaseItemDA.UpdateSaleVolumeDataByPurchaseHeaderID(objSaleVolume)
        End Sub
        Private Sub UpdateSaleLooseDiamondInvoiceDataByPurchaseHeaderID(ByVal argForSaleID As String, ByVal PurchaseAmount As Integer)
            Dim objSaleDiamond As New CommonInfo.SaleLooseDiamondHeaderInfo
            With objSaleDiamond
                .PurchaseHeaderID = argForSaleID
                .PurchaseAmount = PurchaseAmount
            End With
            _objPurchaseItemDA.UpdateSaleLooseDiamondDataByPurchaseHeaderID(objSaleDiamond)
        End Sub
        'Public Function GetPurchaseDetailDataByHeaderID(PurchaseHeaderID As String, Optional ByVal IsGems As Boolean = False) As DataTable Implements IPurchaseItemController.GetPurchaseDetailDataByHeaderID
        '    Return _objPurchaseItemDA.GetPurchaseDetailDataByHeaderID(PurchaseHeaderID, IsGems)
        'End Function

        'Public Function GetAllPurchaseHeaderByIsOut() As DataTable Implements IPurchaseItemController.GetAllPurchaseHeaderByIsOut
        '    Return _objPurchaseItemDA.GetAllPurchaseHeaderByIsOut()
        'End Function

        Public Function GetPurchaseGemDataByPurchaseGemID(PurchaseGemID As String) As DataTable Implements IPurchaseItemController.GetPurchaseGemDataByPurchaseGemID
            Return _objPurchaseItemDA.GetPurchaseGemDataByPurchaseGemID(PurchaseGemID)
        End Function

        Public Function GetPurchaseInvoiceForBarcodeReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceForBarcodeReport
            Return _objPurchaseItemDA.GetPurchaseInvoiceForBarcodeReport(FromDate, ToDate, cristr)
        End Function
        Public Function GetPurchaseInvoiceForLooseDiamondReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceForLooseDiamondReport
            Return _objPurchaseItemDA.GetPurchaseInvoiceForLooseDiamondReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemController.GetPurchaseInvoiceReport
            Return _objPurchaseItemDA.GetPurchaseInvoiceReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetPurchaseInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As CommonInfo.PurchaseDetailInfo Implements IPurchaseItemController.GetPurchaseInvoiceDetailForTotal
            Return _objPurchaseItemDA.GetPurchaseInvoiceDetailForTotal(FromDate, ToDate, cristr)
        End Function
        Public Function GetPurchaseInvoiceReportForTotalAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemController.GetPurchaseInvoiceReportForTotalAmount
            Return _objPurchaseItemDA.GetPurchaseInvoiceReportForTotalAmount(FromDate, ToDate, cristr)
        End Function
        Public Function GetPurchaseInvoicePrint(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseInvoicePrint
            Return _objPurchaseItemDA.GetPurchaseInvoicePrint(PurchaseHeaderID)
        End Function

        Public Function GetPurchaseInvoiceOnlyGemPrint(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceOnlyGemPrint
            Return _objPurchaseItemDA.GetPurchaseInvoiceOnlyGemPrint(PurchaseHeaderID)
        End Function
        Public Function GetPurchaseInvoiceLooseDiamondPrint(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceLooseDiamondPrint
            Return _objPurchaseItemDA.GetPurchaseInvoiceLooseDiamondPrint(PurchaseHeaderID)
        End Function
        Public Function GetAllPurchaseInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemController.GetAllPurchaseInvoiceVoucherPrint
            Return _objPurchaseItemDA.GetAllPurchaseInvoiceVoucherPrint(FromDate, ToDate, cristr)
        End Function

        Public Function GetAllPurchaseGemsVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemController.GetAllPurchaseGemsVoucherPrint
            Return _objPurchaseItemDA.GetAllPurchaseGemsVoucherPrint(FromDate, ToDate, cristr)
        End Function
        Public Function GetPurchaseInvoiceDetailPrint(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceDetailPrint
            Return _objPurchaseItemDA.GetPurchaseInvoiceDetailPrint(PurchaseHeaderID)
        End Function

        Public Function GetAllPuchaseHeaderDataBySaleInvoice(SaleInvoiceHeaderID As String, ByVal Type As String) As DataTable Implements IPurchaseItemController.GetAllPuchaseHeaderDataBySaleInvoice
            Return _objPurchaseItemDA.GetAllPuchaseHeaderDataBySaleInvoice(SaleInvoiceHeaderID, Type)
        End Function

        Public Function GetAllPurchaseHeaderDataByConsignmentSaleInvoice(ConsignmentSaleID As String, ByVal Type As String) As DataTable Implements IPurchaseItemController.GetAllPurchaseHeaderDataByConsignmentSaleInvoice
            Return _objPurchaseItemDA.GetAllPurchaseHeaderDataByConsignmentSaleInvoice(ConsignmentSaleID, Type)
        End Function
        Public Function GetAllPuchaseHeaderDataBySaleGems(SaleGemsID As String) As DataTable Implements IPurchaseItemController.GetAllPuchaseHeaderDataBySaleGems
            Return _objPurchaseItemDA.GetAllPuchaseHeaderDataBySaleGems(SaleGemsID)
        End Function

        Public Function GetAllPurchasePrint(SaleInvoiceHeaderID As String) As DataTable Implements IPurchaseItemController.GetAllPurchasePrint
            Return _objPurchaseItemDA.GetAllPurchasePrint(SaleInvoiceHeaderID)
        End Function
        Public Function GetAllPurchasePrintByConsignmentSale(ConsignmentSaleID As String) As DataTable Implements IPurchaseItemController.GetAllPurchasePrintByConsignmentSale
            Return _objPurchaseItemDA.GetAllPurchasePrintByConsignmentSale(ConsignmentSaleID)
        End Function
        Public Function GetAllPurchaseGemsPrint(SaleGemsID As String) As DataTable Implements IPurchaseItemController.GetAllPurchaseGemsPrint
            Return _objPurchaseItemDA.GetAllPurchaseGemsPrint(SaleGemsID)
        End Function

        Public Function GetPurchaseHeaderDataBySaleInvoice(PurchaseHeaderID As String, SaleInvoiceHeaderID As String, ByVal Type As String) As PurchaseHeaderInfo Implements IPurchaseItemController.GetPurchaseHeaderDataBySaleInvoice
            Return _objPurchaseItemDA.GetPurchaseHeaderDataBySaleInvoice(PurchaseHeaderID, SaleInvoiceHeaderID, Type)
        End Function

        Public Function GetAllPurchasePrintForSaleLooseDiamond(SaleLooseDiamondID As String) As DataTable Implements IPurchaseItemController.GetAllPurchasePrintForSaleLooseDiamond
            Return _objPurchaseItemDA.GetAllPurchasePrintForSaleLooseDiamond(SaleLooseDiamondID)
        End Function
        Public Function GetAllPurchasePrintForSaleVolume(SalesVolumeID As String) As DataTable Implements IPurchaseItemController.GetAllPurchasePrintForSaleVolume
            Return _objPurchaseItemDA.GetAllPurchasePrintForSaleVolume(SalesVolumeID)
        End Function
        Public Function GetPurchaseInvoiceSummayReportByDate(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemController.GetPurchaseInvoiceSummayReportByDate
            Return _objPurchaseItemDA.GetPurchaseInvoiceSummayReportByDate(FromDate, ToDate, cristr)
        End Function
        Public Function GetPurchaseInvoiceDailyTransactionReport(ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceDailyTransactionReport
            Return _objPurchaseItemDA.GetPurchaseInvoiceDailyTransactionReport(ToDate, cristr)
        End Function
        Public Function GetPurchaseInvoiceGemReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceGemReport
            Return _objPurchaseItemDA.GetPurchaseInvoiceGemReport(FromDate, ToDate, cristr)
        End Function
        Public Function GetPurchaseInvoiceLooseDiamondReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemController.GetPurchaseInvoiceLooseDiamondReport
            Return _objPurchaseItemDA.GetPurchaseInvoiceLooseDiamondReport(FromDate, ToDate, cristr)
        End Function

        Public Function SavePurchaseFromSupplier(ByVal Obj As CommonInfo.PurchaseHeaderInfo, ByVal _dtPurchaseInvoiceItem As System.Data.DataTable) As Boolean Implements IPurchaseItemController.SavePurchaseFromSupplier

            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            If Obj.PurchaseFromSupplierID = "0" Then
                Obj.PurchaseFromSupplierID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoice, CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoice.ToString, Obj.PDate)
                bolRet = _objPurchaseItemDA.InsertPurchaseFromSupplier(Obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseFromSupplier.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                                       Obj.PurchaseFromSupplierID, _
                                                                       "Insert Purchase From Supplier")
            Else
                bolRet = _objPurchaseItemDA.UpdatePurchaseFromSupplier(Obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseFromSupplier.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                       Obj.PurchaseFromSupplierID, _
                                                       "Update Purchase From Supplier")


            End If


            If bolRet = True Then
                For Each dr As DataRow In _dtPurchaseInvoiceItem.Rows
                    Dim objPurchaseInvoiceItem As New PurchaseDetailInfo

                    If dr.RowState = DataRowState.Added Then

                        With objPurchaseInvoiceItem
                            .PurchaseFromSupplierID = Obj.PurchaseFromSupplierID
                            .PurchaseFromSupplierItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.PurchaseFromSupplierItem, CommonInfo.EnumSetting.GenerateKeyType.PurchaseFromSupplierItem.ToString, Obj.PDate)
                            .OriginalCode = IIf(IsDBNull(dr.Item("OriginalCode")), "", dr.Item("OriginalCode"))
                            .GramWeight = IIf(IsDBNull(dr.Item("GWeight")), "0", dr.Item("GWeight"))
                            .QTY = IIf(IsDBNull(dr.Item("QTY")), "0", dr.Item("QTY"))
                            .Rate = IIf(IsDBNull(dr.Item("Rate")), "0", dr.Item("Rate"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")), "0", dr.Item("Amount"))
                        End With

                        bolRet = _objPurchaseItemDA.InsertPurchaseFromSupplierItem(objPurchaseInvoiceItem)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objPurchaseInvoiceItem
                            .PurchaseFromSupplierID = Obj.PurchaseFromSupplierID
                            .PurchaseFromSupplierItemID = dr.Item("PurchaseFromSupplierItemID")
                            .OriginalCode = IIf(IsDBNull(dr.Item("OriginalCode")), "", dr.Item("OriginalCode"))
                            .GramWeight = IIf(IsDBNull(dr.Item("GWeight")), "0", dr.Item("GWeight"))
                            .QTY = IIf(IsDBNull(dr.Item("QTY")), "0", dr.Item("QTY"))
                            .Rate = IIf(IsDBNull(dr.Item("Rate")), "0", dr.Item("Rate"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")), "0", dr.Item("Amount"))

                        End With

                        bolRet = _objPurchaseItemDA.UpdatePurchaseFromSupplierItem(objPurchaseInvoiceItem)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objPurchaseItemDA.DeletePurchaseFromSupplierItem(CStr(dr.Item("PurchaseFromSupplierItemID", DataRowVersion.Original)))
                    End If
                Next

            End If

            Return bolRet
        End Function

        Public Function DeletePurchaseFromSupplier(ByVal PurchaseFromSupplierID As String) As Boolean Implements IPurchaseItemController.DeletePurchaseFromSupplier
            If _objPurchaseItemDA.DeletePurchaseFromSupplier(PurchaseFromSupplierID) Then

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseFromSupplier.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       PurchaseFromSupplierID, _
                                                       "Delete Purchase From Supplier")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllPurchaseInvoice() As System.Data.DataTable Implements IPurchaseItemController.GetAllPurchaseFromSupplier
            Return _objPurchaseItemDA.GetAllPurchaseFromSupplier()
        End Function

        Public Function GetPurchaseFromSupplier(ByVal PurchaseFromSupplierID As String) As CommonInfo.PurchaseHeaderInfo Implements IPurchaseItemController.GetPurchaseFromSupplier
            Return _objPurchaseItemDA.GetPurchaseFromSupplier(PurchaseFromSupplierID)
        End Function

        Public Function GetPurchaseInvoiceItem(ByVal PurchaseFromSupplierID As String) As System.Data.DataTable Implements IPurchaseItemController.GetPurchaseFromSupplierItem
            Return _objPurchaseItemDA.GetPurchaseFromSupplierItem(PurchaseFromSupplierID)
        End Function
        Public Function GetPurchaseInvoiceFromSupplierReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemController.GetPurchaseInvoiceFromSupplierReport
            Return _objPurchaseItemDA.GetPurchaseInvoiceFromSupplierReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetPurchaseGemByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseGemByID
            Return _objPurchaseItemDA.GetPurchaseGemByID(PurchaseHeaderID)
        End Function
        Public Function GetPurchaseDiamondByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemController.GetPurchaseDiamondByID
            Return _objPurchaseItemDA.GetPurchaseDiamondByID(PurchaseHeaderID)
        End Function
    End Class
End Namespace

