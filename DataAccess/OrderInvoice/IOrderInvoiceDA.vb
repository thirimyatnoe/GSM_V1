Imports CommonInfo
Namespace OrderInvoice

    Public Interface IOrderInvoiceDA
        Function InsertOrderInvoice(ByVal OrderInvoiceObj As CommonInfo.OrderInvoiceInfo) As Boolean
        Function UpdateOrderInvoice(ByVal OrderInvoiceObj As OrderInvoiceInfo) As Boolean
        Function DeleteOrderInvoice(ByVal OrderInvoiceID As String) As Boolean
        Function DeleteOrderInvoiceReturn(ByVal OrderReturnHeaderID As String) As Boolean
        Function GetAllOrderInvoiceOrderList() As DataTable
        Function GetOrderInvoice(ByVal OrderInvoiceID As String) As OrderInvoiceInfo
        Function InsertOrderInvoiceItem(ByVal ObjOrderInvoiceItem As OrderInvoiceGemsItemInfo) As Boolean
        Function UpdateOrderInvoiceItem(ByVal ObjOrderInvoiceItem As OrderInvoiceGemsItemInfo) As Boolean
        Function DeleteOrderInvoiceItem(ByVal OrderInvoiceItemID As String) As Boolean
        Function GetOrderInvoiceDetailReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As DataTable
        Function GetOrderInvoicePrint(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReturnPrint(ByVal OrderInvoiceID As String) As DataTable
        Function InsertOrderInvoiceDetail(ByVal DetailObj As OrderInvoiceDetailInfo) As Boolean
        Function UpdateOrderInvoiceDetail(ByVal DetailObj As OrderInvoiceDetailInfo) As Boolean
        Function DeleteOrderInvoiceDetail(ByVal OrderInvoiceDetailID As String) As Boolean
        Function InsertOrderReceiveDetail(ByVal DetailObj As OrderReceiveDetailInfo) As Boolean
        Function UpdateOrderReceiveDetail(ByVal DetailObj As OrderReceiveDetailInfo) As Boolean
        Function DeleteOrderReceiveDetail(ByVal OrderReceiveDetailID As String) As Boolean
        Function GetOrderInvoiceGemsItemHeaderID(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReceiveDetailGemByID(ByVal OrderReceiveDetailID As String) As DataTable
        Function DeleteOrderReturnGemsItemByGemsID(ByVal OrderReturnGemID As String) As Boolean
        Function DeleteOrderReceiveDetailGemsItemByGemsID(ByVal OrderInvoiceGemsItemID As String) As Boolean
        Function GetOrderInvoiceHeaderID(ByVal OrderInvoiceID As String) As OrderInvoiceInfo
        Function GetOrderReceiveDetail(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReturnDetailReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "")
        Function GetOrderInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As OrderInvoiceDetailInfo
        Function GetOrderInvoiceDataByHeaderIDAndItemCode(ByVal OrderInvoiceID As String, Optional ByVal ItemCode As String = "", Optional ByVal argForSaleIDStr As String = "") As DataTable
        Function GetOrderInvoiceGemDataByOrderDetailID(ByVal OrderInvoiceDetailID As String) As DataTable
        Function InsertOrderReturnGemItem(ByVal ObjOrderReturnGem As OrderReturnGemsItemInfo) As Boolean
        Function UpdateOrderInvoiceDetailGem(ByVal ObjOrderInvoiceItem As OrderInvoiceDetailGemInfo) As Boolean
        Function GetOrderInvoiceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As OrderInvoiceInfo
        Function GetOrderReturnReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetOrderReturnSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "")
        Function GerOrderSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "")
        Function GetAllOrderInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetAllOrderReturnVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID(ByVal OrderInvoiceDetailGemsID As String) As DataTable
        Function GetOrderReturnDetailPrint(ByVal OrderReturnHeaderID As String) As DataTable
        Function GetReceiveDataByOrderInvocieID(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReceivePrint(ByVal OrderInvoiceID As String) As DataTable
        Function GetAllOrderReceive() As DataTable
        Function GetOrderGemsByReceive(ByVal OrderInvoiceID As String) As DataTable
        Function GetOrderReceiveDetailID(ByVal OrderReceiveDetailID As String) As OrderReceiveDetailInfo
        Function GetAllOrderReceiveHeader() As DataTable
        Function GetOrderInvoiceInfoByDetailID(ByVal OrderReceiveDetailID As String) As OrderInvoiceInfo
        Function InsertOrderInvoiceReturn(ByVal OrderInvoiceObj As CommonInfo.OrderReturnHeader) As Boolean
        Function UpdateOrderReturnHeader(ByVal OrderInvoiceObj As CommonInfo.OrderReturnHeader) As Boolean
        Function GetBalanceAmountByOrderInvoiceID(OrderInvoiceID As String, Optional OrderReturnHeaderID As String = "") As DataTable
        Function GetOrderInvoiceReturnHeader(OrderReturnHeaderID As String) As OrderReturnHeader
        Function GetOrderReturnDetailByOrderInvoiceID(OrderInvoiceID As String) As DataTable
        Function UpdateOrderReceiveByIsReturn(ByVal Obj As CommonInfo.OrderInvoiceInfo) As Boolean
        Function GetAllOrderReturnHeader() As DataTable
        Function GetOrderReturnDetailByHeaderID(OrderReturnHeaderID As String) As DataTable
        Function GetOrderReturnGemDataByHeaderID(OrderReturnHeaderID As String) As DataTable
        Function GetOrderReturneGemsItemByDetailID(OrderInvoiceDetailID As String) As DataTable
        Function UpdateOrderReturnDetailByIsReturn(ByVal OrderInvoiceObj As CommonInfo.OrderInvoiceDetailInfo) As Boolean
        Function GetOrderForItemName(OrderInvoiceID As String) As DataTable
        Function GetOrderReturnGem(OrderReturnHeaderID As String) As DataTable
        Function GetOrderTaxVoucher(ByVal OrderReturnHeaderID As String) As DataTable
    End Interface
End Namespace

