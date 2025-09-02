Imports CommonInfo
Namespace SalesItemInvoice

    Public Interface ISalesItemInvoiceDA
        Function InsertSalesInvoiceHeader(ByVal SalesInvoiceObj As SaleInvoiceHeaderInfo) As Boolean
        Function UpdateSalesInvoiceHeader(ByVal SalesInvoiceObj As SaleInvoiceHeaderInfo) As Boolean
        Function DeleteSalesInvoiceHeader(ByVal SalesInvoiceHeaderID As String) As Boolean

        Function InsertSaleInvoiceDetail(ByVal ObjSalesInvoiceItem As SalesInvoiceDetailInfo) As Boolean
        Function UpdateSaleInvoiceDetail(ByVal ObjSalesInvoiceItem As SalesInvoiceDetailInfo) As Boolean
        Function DeleteSaleInvoiceDetail(ByVal SaleInvoiceDetailID As String) As Boolean

        Function InsertSaleInvoiceDetailGem(ByVal ObjSalesInvoiceItem As SaleInvoiceDetailGemInfo) As Boolean
        Function UpdateSaleInvoiceDetailGem(ByVal ObjSalesInvoiceItem As SaleInvoiceDetailGemInfo) As Boolean
        Function DeleteSaleInvoiceDetailGem(ByVal SalesInvoiceGemItemID As String) As Boolean

        Function GetSalesInvoiceDetailByID(ByVal SaleInvoiceHeaderID As String) As DataTable
        Function GetSaleInvoiceDetailGemByID(ByVal SaleInvoiceDetailID As String) As DataTable

        Function GetAllSalesInvoice() As DataTable
        Function GetSaleInvoiceHeaderByID(ByVal SalesInvoiceHeaderID As String) As SaleInvoiceHeaderInfo
        Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondHeaderID As String) As SaleLooseDiamondHeaderInfo

        Function GetSaleInvoiceDetailGemByHeaderID(ByVal SalesInvoiceHeaderID As String) As DataTable
        Function GetSalesInvoicePrint(ByVal SaleInvoiceID As String) As DataTable
        Function GetSalesInvoiceDataByHeaderIDAndItemCode(ByVal SaleInvoiceHeaderID As String, Optional ByVal ItemCode As String = "", Optional ByVal argForSaleIDStr As String = "") As DataTable
        Function GetSalesInvoiceGemDataBySaleDetailID(ByVal SaleInvoiceDetailID As String) As DataTable
        Function GetAllSalesInvoiceForPurchase(Optional ByVal IsReuseBarcode As Boolean = False) As DataTable
        Function GetProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetSalesInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetSalesInvoiceReportForSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSalesInvoiceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As SalesInvoiceDetailInfo
        Function GetAllSaleInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetOrderTaxSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable


        Function GetSalesInvoiceTaxVoucher(ByVal SaleInvoiceHeaderID As String) As DataTable

        Function GetSaleInvoiceGemDataBySaleInvoiceGemsItemID(ByVal SalesInvoiceGemItemID As String) As DataTable
        Function GetSalesInvoiceDetailPrintByID(ByVal SaleInvoiceID As String) As DataTable
        Function GetSumProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetForSalePercentageReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal SortBy As String, Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleGem(ByVal SaleInvoiceHeaderID As String) As DataTable
        Function UpdateSaleInvoiceDetailByIsReturn(ByVal SalesDetailObj As SalesInvoiceDetailInfo) As Boolean
        Function UpdateConsignmentSaleDetailByIsReturn(ByVal ConsignmentSaleItemObj As ConsignmentSaleItemInfo) As Boolean
        Function UpdateSaleGemsItemByIsReturn(ByVal SalesGemsItemObj As SaleGemsItemInfo) As Boolean
        Function UpdateSaleLooseDiamondByIsReturn(ByVal Obj As SaleLooseDiamondDetailInfo) As Boolean
        'Function UpdateSaleLooseDiamondDetailByIsReturn(ByVal SaleLooseDiamondItemObj As SaleLooseDiamondDetailInfo) As Boolean

        Function GetMostSaleItemData(FromDate As Date, ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleSummaryReportByDateANDByItemName(FromDate As Date, ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetBalanceStockByValue(ByVal criStr As String) As DataTable
        Function SetSalesInvoiceToReturn(ByVal SalesInvoiceID As String, Optional ByVal IsReturn As Integer = 0) As Boolean
        Function GetForSaleIDBySaleInvoice(ByVal SalesInvoiceID As String) As String
        Function GetSalesInvoiceDataByCustomerIDAndItemCode(ByVal CustomerID As String, Optional ByVal criStr As String = "") As DataTable

        Function InsertRecordCash(ByVal ObjOtherCash As OtherCashInfo) As Boolean
        Function UpdateRecordCash(ByVal ObjOtherCash As OtherCashInfo) As Boolean
        Function DeleteRecordCash(ByVal OtherCashID As String) As Boolean
        Function GetOtherCashDataByVoucherNo(ByVal SaleInvoiceHeaderID As String) As DataTable

        Function GetSalesInvoiceDataSaleDetailID(Optional ByVal criStr As String = "") As DataTable
        Function GetAllSalesInvoiceDataByItemCode() As DataTable
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean
        ' Function UpdateSaleInvoiceDetailByIsReturn(ByVal SalesDetailObj As SalesInvoiceDetailInfo) As Boolean

    End Interface
End Namespace

