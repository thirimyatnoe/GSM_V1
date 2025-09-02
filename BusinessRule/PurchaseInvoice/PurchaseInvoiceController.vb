'Imports DataAccess.PurchaseInvoice
'Imports DataAccess.SalesItemInvoice
'Imports DataAccess.SalesItem
'Imports CommonInfo
'Namespace PurchaseInvoice
'    Public Class PurchaseInvoiceController
'        Implements IPurchaseInvoiceController

'#Region "Private Members"

'        Private _objPurchaseInvoiceDA As IPurchaseInvoiceDA
'        Private _objSaleInvoiceDA As ISalesItemInvoiceDA
'        Private _objSalesItemDA As ISalesItemDA
'        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
'        Private Shared ReadOnly _instance As IPurchaseInvoiceController = New PurchaseInvoiceController

'#End Region

'#Region "Constructors"

'        Private Sub New()
'            _objPurchaseInvoiceDA = DataAccess.Factory.Instance.CreatePurchaseInvoiceDA
'            _objSaleInvoiceDA = DataAccess.Factory.Instance.CreateSalesItemInvoiceDA
'            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA

'        End Sub

'#End Region

'#Region "Public Properties"

'        Public Shared ReadOnly Property Instance() As IPurchaseInvoiceController
'            Get
'                Return _instance
'            End Get
'        End Property

'#End Region
'        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
'            Dim objSaleItem As New CommonInfo.SalesItemInfo
'            With objSaleItem
'                .ForSaleID = argForSaleID
'                .IsExit = argIsExit
'                .ExitDate = argExitDate
'            End With
'            _objSalesItemDA.UpdateSaleItemIsExit(objSaleItem)
'        End Sub

'        Public Function DeletePurchaseInvoice(ByVal PurchaseInvoiceID As String, Optional ByVal SaleInvoiceID As String = "") As Boolean Implements IPurchaseInvoiceController.DeletePurchaseInvoice
'            Dim ForSaleInvoiceID As String = ""
'            If _objPurchaseInvoiceDA.DeletePurchaseInvoice(PurchaseInvoiceID) Then
'                If SaleInvoiceID <> "" Then 'For IsReturn=1
'                    _objSaleInvoiceDA.SetSalesInvoiceToReturn(SaleInvoiceID, 0)
'                End If

'                If SaleInvoiceID <> "SaleInvoiceID" Then

'                    ForSaleInvoiceID = _objSaleInvoiceDA.GetForSaleIDBySaleInvoice(SaleInvoiceID)
'                    UpdateSalesItemByIsExit(ForSaleInvoiceID, True, Now)
'                End If

'                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
'                                                       DateTime.Now, _
'                                                       Global_UserID, _
'                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoice.ToString, _
'                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
'                                                       PurchaseInvoiceID, _
'                                                       "Delete Purchase Invoice")
'                Return True
'            Else
'                Return False
'            End If
'        End Function
'        Public Function GetPurchaseInvoice(ByVal PurchaseInvoiceID As String) As CommonInfo.PurchaseInvoiceInfo Implements IPurchaseInvoiceController.GetPurchaseInvoice
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoice(PurchaseInvoiceID)
'        End Function

'        Public Function SavePurchaseInvoice(ByVal PurchaseInvoiceObj As CommonInfo.PurchaseInvoiceInfo, ByVal PurchaseInvoiceGemsItem As CommonInfo.PurchaseInvoiceGemsItemInfo, ByVal _dtPurchaseInvoiceGemsItem As System.Data.DataTable, Optional ByVal SaleInvoiceID As String = "", Optional ByVal ItemCode As String = "") As Boolean Implements IPurchaseInvoiceController.SavePurchaseInvoice
'            Dim objGeneralController As General.IGeneralController
'            objGeneralController = Factory.Instance.CreateGeneralController
'            Dim bolRet As Boolean = False
'            Dim ForSaleInvoiceID As String = ""
'            If PurchaseInvoiceObj.PurchaseInvoiceID = "" Then
'                PurchaseInvoiceObj.PurchaseInvoiceID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoice, CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoice.ToString, PurchaseInvoiceObj.PDate)
'                bolRet = _objPurchaseInvoiceDA.InsertPurchaseInvoice(PurchaseInvoiceObj)
'                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
'                                                                       DateTime.Now, _
'                                                                       Global_UserID, _
'                                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoice.ToString, _
'                                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
'                                                                       PurchaseInvoiceObj.PurchaseInvoiceID, _
'                                                                       "Insert Purchase Invoice")
'                If SaleInvoiceID <> "SaleInvoiceID" Then
'                    ForSaleInvoiceID = _objSalesItemDA.GetSalesIDByItemCode(ItemCode)
'                    UpdateSalesItemByIsExit(ForSaleInvoiceID, False, PurchaseInvoiceObj.PDate)
'                End If
'            Else
'                bolRet = _objPurchaseInvoiceDA.UpdatePurchaseInvoice(PurchaseInvoiceObj)
'                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
'                                                       DateTime.Now, _
'                                                       Global_UserID, _
'                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoice.ToString, _
'                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
'                                                       PurchaseInvoiceObj.PurchaseInvoiceID, _
'                                                       "Update Purchase Invoice")

'                If SaleInvoiceID <> "SaleInvoiceID" Then
'                    ForSaleInvoiceID = _objSalesItemDA.GetSalesIDByItemCode(ItemCode)
'                    UpdateSalesItemByIsExit(ForSaleInvoiceID, False, PurchaseInvoiceObj.PDate)
'                End If

'            End If



'            If bolRet Then
'                If SaleInvoiceID <> "SaleInvoiceID" Then 'For IsReturn=1

'                    bolRet = _objSaleInvoiceDA.SetSalesInvoiceToReturn(SaleInvoiceID, 1)

'                End If
'                For Each dr As DataRow In _dtPurchaseInvoiceGemsItem.Rows
'                    Dim objPurchaseInvoiceGemsItem As New PurchaseInvoiceGemsItemInfo
'                    If dr.RowState = DataRowState.Added Then
'                        With objPurchaseInvoiceGemsItem
'                            .PurchaseInvoiceID = PurchaseInvoiceObj.PurchaseInvoiceID
'                            .PurchaseInvoiceGemsItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoiceGemsItem, CommonInfo.EnumSetting.GenerateKeyType.PurchaseInvoiceGemsItem.ToString, PurchaseInvoiceObj.PDate)
'                            .GemsCategoryID = dr.Item("@GemsCategoryID")
'                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
'                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
'                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, "-", dr.Item("YOrCOrG"))
'                            .GemTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
'                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
'                            .GemsName = dr.Item("GemsName")
'                            If Not IsDBNull(dr.Item("FixType")) Then
'                                If (dr.Item("FixType") = "Fix") Then
'                                    .FixType = 1
'                                ElseIf (dr.Item("FixType") = "ByWeight") Then
'                                    .FixType = 2
'                                Else
'                                    .FixType = 3
'                                End If
'                            Else
'                                .FixType = 0
'                            End If


'                            .PurchaseRate = dr.Item("PurchaseRate")
'                            .Amount = dr.Item("Amount")




'                        End With

'                        bolRet = _objPurchaseInvoiceDA.InsertPurchaseInvoiceItem(objPurchaseInvoiceGemsItem)

'                    ElseIf dr.RowState = DataRowState.Modified Then

'                        With objPurchaseInvoiceGemsItem
'                            .PurchaseInvoiceID = PurchaseInvoiceObj.PurchaseInvoiceID
'                            '.PurchaseInvoiceGemsItemID = dr.Item("PurchaseInvoiceGemsItemID")
'                            .GemsCategoryID = dr.Item("@GemsCategoryID")
'                            '.GemsK = IIf(IsDBNull(dr.Item("GemsK")) = True, 0, dr.Item("GemsK"))
'                            '.GemsP = IIf(IsDBNull(dr.Item("GemsP")) = True, 0, dr.Item("GemsP"))
'                            '.GemsY = IIf(IsDBNull(dr.Item("GemsY")) = True, 0, dr.Item("GemsY"))
'                            '.GemsC = IIf(IsDBNull(dr.Item("GemsC")) = True, 0, dr.Item("GemsC"))

'                            '.GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
'                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))

'                            .GemTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
'                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, "-", dr.Item("YOrCOrG"))
'                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
'                            .GemsName = dr.Item("GemsName")
'                            If Not IsDBNull(dr.Item("FixType")) Then
'                                If (dr.Item("FixType") = "Fix") Then
'                                    .FixType = 1
'                                ElseIf (dr.Item("FixType") = "ByWeight") Then
'                                    .FixType = 2
'                                Else
'                                    .FixType = 3
'                                End If
'                            Else
'                                .FixType = 0
'                            End If


'                            .PurchaseRate = dr.Item("PurchaseRate")
'                            .Amount = dr.Item("Amount")


'                        End With

'                        bolRet = _objPurchaseInvoiceDA.UpdatePurchaseInvoiceItem(objPurchaseInvoiceGemsItem)

'                    ElseIf dr.RowState = DataRowState.Deleted Then
'                        bolRet = _objPurchaseInvoiceDA.DeletePurchaseInvoiceItem(CStr(dr.Item("PurchaseInvoiceGemsItemID", DataRowVersion.Original)))
'                    End If
'                Next

'            End If

'            Return bolRet
'        End Function

'        Public Function GetPurchaseInvoiceGemsItem(ByVal PurchaseInvoiceID As String) As System.Data.DataTable Implements IPurchaseInvoiceController.GetPurchaseInvoiceGemsItem
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoiceItem(PurchaseInvoiceID)
'        End Function
'        Public Function GetAllPurchaseInvoice() As System.Data.DataTable Implements IPurchaseInvoiceController.GetAllPurchaseInvoice
'            Return _objPurchaseInvoiceDA.GetAllPurchaseInvoice()
'        End Function

'        Public Function GetAllPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceController.GetAllPurchaseInvoiceReport
'            Return _objPurchaseInvoiceDA.GetAllPurchaseInvoiceReport(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetPurchasePercent(ByVal GoldQualityID As String) As Integer Implements IPurchaseInvoiceController.GetPurchasePercent
'            Return _objPurchaseInvoiceDA.GetPurchasePercent(GoldQualityID)
'        End Function

'        Public Function GetExchangePercent(ByVal GoldQualityID As String) As Integer Implements IPurchaseInvoiceController.GetExchangePercent
'            Return _objPurchaseInvoiceDA.GetExchangePercent(GoldQualityID)
'        End Function

'        Public Function GetAllPurchaseInvoiceForDetailRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceController.GetAllPurchaseInvoiceForDetailRpt
'            Return _objPurchaseInvoiceDA.GetAllPurchaseInvoiceForDetailRpt(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetPurchaseInvoiceSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceController.GetPurchaseInvoiceSummaryReport
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoiceSummaryReport(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceController.GetPurchaseInvoiceReport
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoiceReport(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetPurchaseSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As System.Data.DataTable Implements IPurchaseInvoiceController.GetPurchaseSummaryByGoldQualityAndItemCategory
'            Return _objPurchaseInvoiceDA.GetPurchaseSummaryByGoldQualityAndItemCategory(ForDate, GoldQualityID, ItemCategoryID)
'        End Function

'        Public Function GetPurchaseInvoicePrint(ByVal PurchaseInvoiceID As String) As System.Data.DataTable Implements IPurchaseInvoiceController.GetPurchaseInvoicePrint
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoicePrint(PurchaseInvoiceID)
'        End Function

'        Public Function InsertPurchaseInvoiceUserID(ByVal PurchaseInvoiceID As String, ByVal UserID As String) As Boolean Implements IPurchaseInvoiceController.InsertPurchaseInvoiceUserID
'            Return _objPurchaseInvoiceDA.InsertPurchaseInvoiceUserID(PurchaseInvoiceID, UserID)
'        End Function

'        Public Function GetSaleInvoiceItemFromPurchaseInvoice(PurchaseInvoiceID As String, SaleInvoice As String) As DataTable Implements IPurchaseInvoiceController.GetSaleInvoiceItemFromPurchaseInvoice
'            Return _objPurchaseInvoiceDA.GetSaleInvoiceItemFromPurchaseInvoice(PurchaseInvoiceID, SaleInvoice)
'        End Function

'        Public Function GetPurchaseInvoiceForBarcodeReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseInvoiceController.GetPurchaseInvoiceForBarcodeReport
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoiceForBarcodeReport(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetPurchaseInvoiceGemReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseInvoiceController.GetPurchaseInvoiceGemReport
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoiceGemReport(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetPurchaseInvoiceDailyTransactionReport(ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseInvoiceController.GetPurchaseInvoiceDailyTransactionReport
'            Return _objPurchaseInvoiceDA.GetPurchaseInvoiceDailyTransactionReport(ToDate, cristr)
'        End Function
'    End Class
'End Namespace