Imports CommonInfo
Namespace SalesItemInvoice
    Public Interface ISalesItemInvoiceController

        ' Function GetTaxReportDetail(FromDate As Date, ToDate As Date, SalesInvoiceHeaderID As String) As DataTable

        Function SaveSalesInvoice(ByVal SalesInvoiceObj As SaleInvoiceHeaderInfo, ByVal _dtSaleInvoiceDetail As DataTable, ByVal _dtSaleInvoiceDetailGem As DataTable, ByVal _dtOtherCash As DataTable, ByVal _dtItemData_His As DataTable) As Boolean
        Function DeleteSalesInvoice(ByVal SalesInvoiceObj As SaleInvoiceHeaderInfo) As Boolean

        Function GetAllSalesInvoice() As DataTable
        Function GetSaleInvoiceHeaderByID(ByVal SalesInvoiceHeaderID As String) As SaleInvoiceHeaderInfo
        Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondHeaderID As String) As SaleLooseDiamondHeaderInfo

        Function GetSalesInvoiceDetailByID(ByVal SalesInvoiceHeaderID As String) As DataTable
        Function GetSaleInvoiceDetailGemByHeaderID(ByVal SalesInvoiceHeaderID As String) As DataTable
        Function GetSalesInvoicePrint(ByVal SaleInvoiceID As String) As DataTable
        Function GetSalesInvoiceDataByHeaderIDAndItemCode(ByVal SaleInvoiceHeaderID As String, Optional ByVal ItemCode As String = "", Optional ByVal argForSaleIDStr As String = "") As DataTable
        Function GetSalesInvoiceGemDataBySaleDetailID(ByVal SaleInvoiceDetailID As String) As DataTable
        Function GetAllSalesInvoiceForPurchase(Optional ByVal IsReuseBarcode As Boolean = False) As DataTable
        Function GetProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetSalesInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetSalesInvoiceReportForSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSalesInvoiceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSalesInvoiceTaxVoucher(ByVal SaleInvoiceHeaderID As String) As DataTable

        Function GetOrderTaxSummaryReport(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable
        Function GetSaleInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As SalesInvoiceDetailInfo
        Function GetAllSaleInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleInvoiceGemDataBySaleInvoiceGemsItemID(ByVal SalesInvoiceGemItemID As String) As DataTable
        Function GetSalesInvoiceDetailPrintByID(ByVal SaleInvoiceID As String) As DataTable
        Function GetForSalePercentageReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal SortBy As String, Optional ByVal cristr As String = "") As DataTable
        Function GetSumProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetForSaleGem(ByVal SaleInvoiceHeaderID As String) As DataTable
        Function GetMostSaleItemData(FromDate As Date, ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleSummaryReportByDateANDByItemName(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetBalanceStockByValue(ByVal criStr As String) As DataTable
        Function GetSalesInvoiceDataByCustomerIDAndItemCode(ByVal CustomerID As String, Optional ByVal CriStr As String = "") As DataTable
        Function GetOtherCashDataByVoucherNo(ByVal SaleInoiceHeaderID As String) As DataTable
        Function GetSalesInvoiceDataSaleDetailID(Optional ByVal criStr As String = "") As DataTable
        Function GetAllSalesInvoiceDataByItemCode() As DataTable
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean
    End Interface
End Namespace
