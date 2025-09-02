Imports CommonInfo
Namespace WholeSaleReturn
    Public Interface IWholeSaleReturnController
        Function SaveWholeSaleReturn(ByVal obj As WholeSaleReturnInfo, ByVal _dtSalesInvoiceItem As DataTable) As Boolean
        Function DeleteWholeSaleReturn(ByVal WholeSaleReturnID As String) As Boolean
        Function GetAllWholeSaleReturn() As DataTable
        Function GetWholeSaleReturnByID(ByVal WholeSaleReturnID As String) As WholeSaleReturnInfo

        Function GetWholeSalesReturnItem(ByVal WholeSalesReturnID As String) As DataTable
        Function GetWholeSaleReturnByWSID(ByVal WholeSaleInvoiceID As String) As DataTable
        Function GetWholeSaleReturnItemByID(ByVal WholeSaleReturnID As String) As DataTable
        Function GetWholeSaleReturnPrint(ByVal WholeSaleReturnID As String) As DataTable
        Function GetWholeSaleReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetWholeSaleReturnReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable




    End Interface
End Namespace

