Imports CommonInfo
Namespace SalesVolume

    Public Interface ISalesVolumeDA
        Function InsertSalesVolumeHeader(ByVal SalesVolumeObj As SalesVolumeHeaderInfo) As Boolean
        Function UpdateSalesVolumeHeader(ByVal SalesVolumeObj As SalesVolumeHeaderInfo) As Boolean
        Function DeleteSalesVolumeHeader(ByVal SalesVolumeHeaderID As String) As Boolean

        Function InsertSalesVolumeDetail(ByVal ObjSalesVolumeItem As SalesVolumeDetailInfo) As Boolean
        Function UpdateSalesVolumeDetail(ByVal ObjSalesVolumeItem As SalesVolumeDetailInfo) As Boolean
        Function DeleteSalesVolumeDetail(ByVal SalesVolumeDetailID As String) As Boolean

        Function GetSalesVolumeDetailByID(ByVal SalesVolumeHeaderID As String) As DataTable

        Function GetAllSalesVolume() As DataTable
        Function GetSalesVolumeHeaderByID(ByVal SalesVolumeHeaderID As String) As SalesVolumeHeaderInfo
        Function GetSalesVolumePrint(ByVal SalesVolumeID As String) As DataTable
        Function GetSalesVolumeDataByHeaderIDAndItemCode(ByVal SalesVolumeHeaderID As String, Optional ByVal ItemCode As String = "") As DataTable
        Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetSaleVolumeByID(ByVal SaleVolumeID As String) As SalesVolumeHeaderInfo
        Function GetSalesInvoiceVolumeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetSalesInvoiceVolumeReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetProfitForSaleVoulumeItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetSaleVolumeDetailByDetailID(ByVal SalesVolumeDetailID As String) As DataTable
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleVolumeID As String) As Boolean
    End Interface
End Namespace

