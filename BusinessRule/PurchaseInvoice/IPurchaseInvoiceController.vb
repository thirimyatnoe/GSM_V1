'Imports CommonInfo
'Namespace PurchaseInvoice
'    Public Interface IPurchaseInvoiceController
'        Function SavePurchaseInvoice(ByVal PurchaseInvoiceObj As PurchaseInvoiceInfo, ByVal PurchaseInvoiceGemsItem As PurchaseInvoiceGemsItemInfo, ByVal _dtPurchaseInvoiceItem As DataTable, Optional ByVal SaleInvoiceID As String = "", Optional ByVal ItemCode As String = "") As Boolean
'        Function DeletePurchaseInvoice(ByVal PurchaseInvoiceID As String, Optional ByVal SaleInvoiceID As String = "") As Boolean
'        Function GetPurchaseInvoice(ByVal PurchaseInvoiceID As String) As PurchaseInvoiceInfo
'        Function GetAllPurchaseInvoice() As DataTable

'        Function GetPurchaseInvoiceGemsItem(ByVal PurchaseInvoiceID As String) As DataTable

'        Function GetPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
'        Function GetPurchaseInvoiceForBarcodeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
'        ''-- Should be in CurrentPriceDA
'        Function GetPurchasePercent(ByVal GoldQualityID As String) As Integer
'        Function GetExchangePercent(ByVal GoldQualityID As String) As Integer
'        ''--Should be in one Function instead of these three.
'        Function GetAllPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
'        Function GetAllPurchaseInvoiceForDetailRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
'        Function GetPurchaseInvoiceSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
'        Function GetPurchaseSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As DataTable
'        Function GetPurchaseInvoicePrint(ByVal PurchaseInvoiceID As String) As DataTable

'        Function InsertPurchaseInvoiceUserID(ByVal PurchaseInvoiceID As String, ByVal UserID As String) As Boolean
'        Function GetSaleInvoiceItemFromPurchaseInvoice(ByVal PurchaseInvoiceID As String, ByVal SaleInvoice As String) As DataTable
'        Function GetPurchaseInvoiceGemReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
'        Function GetPurchaseInvoiceDailyTransactionReport(ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
'    End Interface
'End Namespace
