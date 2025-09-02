Imports CommonInfo
Namespace OrderInvoice
    Public Interface IOrderInvoiceController
        Function SaveOrderInvoiceReceive(ByVal obj As OrderInvoiceInfo, ByVal _dtOrderInvoiceDetailItem As DataTable, ByVal _dtOrderGemsItem As DataTable) As Boolean
        Function SaveOrderInvoiceReturn(ByVal obj As CommonInfo.OrderReturnHeader, ByVal _dtItemBarcode As DataTable, ByVal _dtOrderInvoiceDetailGem As DataTable) As Boolean
        Function DeleteOrderInvoice(ByVal OrderInvoiceID As String) As Boolean
        Function DeleteOrderInvoiceReturn(ByVal OrderInvoiceID As String) As Boolean
        Function GetAllOrderInvoiceOrderList() As DataTable
        Function GetOrderInvoice(ByVal OrderInvoiceID As String) As OrderInvoiceInfo

        'Function GetOrderInvoiceStock(ByVal OrderInvoiceID As String) As DataTable
        'Function GetOrderReceiveData(Optional ByVal cristr As String = "") As DataTable
        Function GetOrderInvoiceHeaderID(ByVal OrderInvoiceID As String) As OrderInvoiceInfo
        Function GetOrderReceiveDetail(ByVal OrderInvoiceID As String) As DataTable

        'Function GetOrderInvoiceItem(ByVal OrderInvoiceID As String) As DataTable
        'Function GetOrderInvoiceItemForRetun(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderInvoiceDetailReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As DataTable
        Function GetOrderInvoicePrint(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReturnPrint(ByVal OrderInvoiceID As String) As DataTable
        'Function GetAllOrderInvoiceFromSearchBox() As DataTable

        'Function InsertOrderInvoiceUserID(ByVal OrderInvoiceID As String, ByVal UserID As String) As Boolean
        'Function UpdateOrderInvoiceUserID(ByVal OrderInvoiceID As String, ByVal UserID As String) As Boolean
        'Function GetAllOrderInvoiceByStock() As DataTable
        'Function GetOrderInvoiceDetailByID(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReturnDetailReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "")
        Function GetOrderInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As OrderInvoiceDetailInfo
        Function GetOrderInvoiceDataByHeaderIDAndItemCode(ByVal OrderInvoiceID As String, Optional ByVal ItemCode As String = "", Optional ByVal argForSaleIDStr As String = "") As DataTable
        Function GetOrderInvoiceGemDataByOrderDetailID(ByVal OrderInvoiceDetailID As String) As DataTable
        'Function GetOrderInvoiceDetailGemByHeaderID(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderInvoiceGemsItemHeaderID(ByVal OrderInvoiceID As String) As DataTable

        Function GetOrderInvoiceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As OrderInvoiceInfo
        Function GetOrderReturnReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable

        Function GetOrderReturnSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "")
        Function GerOrderSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "")
        Function GetAllOrderInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetAllOrderReturnVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID(ByVal OrderInvoiceDetailGemsID As String) As DataTable
        Function GetOrderReturnDetailPrint(ByVal OrderInvoiceID As String) As DataTable

        Function GetOrderReceivePrint(ByVal OrderInvoiceID As String) As DataTable
        Function GetAllOrderReceive() As DataTable
        Function GetOrderGemsByReceive(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReceiveDetailID(ByVal OrderReceiveDetailID As String) As OrderReceiveDetailInfo
        Function GetAllOrderReceiveHeader() As DataTable
        'Function GetForvoucherNo(OrderInvoiceID As String) As DataTable
        Function GetOrderInvoiceInfoByDetailID(ByVal OrderReceiveDetailID As String) As OrderInvoiceInfo
        Function GetBalanceAmountByOrderInvoiceID(ByVal OrderInvoiceID As String, Optional OrderReturnHeaderID As String = "") As DataTable
        Function GetOrderInvoiceReturnHeader(ByVal OrderReturnHeaderID As String) As OrderReturnHeader
        Function GetAllOrderReturnHeader() As DataTable
        Function GetOrderReturnDetailByHeaderID(ByVal OrderReturnHeaderID As String) As DataTable
        Function GetOrderReturnGemDataByHeaderID(ByVal OrderReturnHeaderID As String) As DataTable
        Function GetOrderForItemName(OrderInvoiceID As String) As DataTable
        Function GetOrderReturnGem(OrderReturnHeaderID As String) As DataTable
        Function GetOrderTaxVoucher(ByVal OrderReturnHeaderID As String) As DataTable
    End Interface
End Namespace
