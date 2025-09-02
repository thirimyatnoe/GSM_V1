Imports CommonInfo
Namespace WholeSaleReturn
    Public Interface IWholeSaleReturnDA
        Function InsertWholeSaleReturn(ByVal WholeSaleReturnObj As WholeSaleReturnInfo) As Boolean
        Function UpdateWholeSaleReturn(ByVal WholeSaleReturnObj As WholeSaleReturnInfo) As Boolean
        Function DeleteWholeSaleReturn(ByVal WholeSaleReturnID As String) As Boolean
        Function GetAllWholeSaleReturn() As DataTable
        Function GetWholeSaleReturnByID(ByVal WholeSaleReturnID As String) As WholeSaleReturnInfo

        Function InsertWholeSaleReturnItem(ByVal ObjWholeSaleReturnItem As WholeSaleReturnItemInfo) As Boolean
        Function UpdateWholeSaleReturnItem(ByVal ObjWholeSaleReturnItem As WholeSaleReturnItemInfo) As Boolean
        Function DeleteWholeSaleReturnItem(ByVal WholeSaleReturnItemID As String) As Boolean
        Function GetWholeSaleReturnItem(ByVal WholeSaleReturnItemID As String) As DataTable

        Function GetWholeSaleReturnItemByID(ByVal WholeSaleReturnID As String) As DataTable
        Function GetWholeSaleReturnByWSID(ByVal WholeSaleInvoiceID As String) As DataTable
        Function GetWholeSaleReturnPrint(ByVal WholeSaleReturnID As String) As DataTable
        Function GetWholeSaleReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetWholeSaleReturnReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
    End Interface
End Namespace
