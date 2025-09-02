Imports CommonInfo
Namespace SalesVolume
    Public Interface ISalesVolumeController

        Function SaveSalesVolume(ByVal SalesVolumeObj As SalesVolumeHeaderInfo, ByVal _dtSalesVolumeDetail As DataTable) As Boolean
        Function DeleteSalesVolume(ByVal SalesVolumeObj As SalesVolumeHeaderInfo) As Boolean

        Function GetAllSalesVolume() As DataTable
        Function GetSalesVolumeHeaderByID(ByVal SalesVolumeHeaderID As String) As SalesVolumeHeaderInfo
        Function GetSalesVolumeDetailByID(ByVal SalesVolumeHeaderID As String) As DataTable
        'Function GetSalesVolumeDetailGemByHeaderID(ByVal SalesVolumeHeaderID As String) As DataTable
        Function GetSalesVolumePrint(ByVal SalesVolumeID As String) As DataTable
        Function GetSalesVolumeDataByHeaderIDAndItemCode(ByVal SalesVolumeHeaderID As String, Optional ByVal ItemCode As String = "") As DataTable
        'Function GetSalesVolumeGemDataBySaleDetailID(ByVal SalesVolumeDetailID As String) As DataTable
        Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetSaleVolumeByID(ByVal SaleVolumeID As String) As SalesVolumeHeaderInfo
        Function GetSalesInvoiceVolumeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetSalesInvoiceVolumeReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetProfitForSaleVoulumeItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetSaleVolumeDetailByDetailID(ByVal SaleVolumeDetailID As String) As DataTable
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleVolumeID As String) As Boolean
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleVolumeID As String) As Boolean
    End Interface
End Namespace
