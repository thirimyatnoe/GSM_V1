Imports CommonInfo
Namespace TransferReturn
    Public Interface ITransferReturnDA
        Function InsertTransferReturn(ByVal TransferReturnObj As TransferReturnInfo) As Boolean
        Function UpdateTransferReturn(ByVal TransferReturnObj As TransferReturnInfo) As Boolean
        Function DeleteTransferReturn(ByVal TransferReturnID As String) As Boolean
        Function GetAllTransferReturn() As DataTable
        Function GetTransferReturnByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetTransferReturn(ByVal TransferReturnID As String) As TransferReturnInfo
        Function InsertTransferReturnItem(ByVal ObjDmgItem As TransferReturnItemInfo) As Boolean
        Function UpdateTransferReturnItem(ByVal ObjDmgItem As TransferReturnItemInfo) As Boolean
        Function DeleteTransferReturnItem(ByVal TransferReturnItemID As String) As Boolean
        Function GetTransferReturnItem(ByVal TransferReturnID As String) As DataTable
        Function GetHeaderTransferReturnList() As DataTable
        Function GetForSaleIDByTransferReturnID(ByVal TransferReturnID As String) As DataTable
        Function GetBranchTransferReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        'Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        'Function UpdateTransferReturnItemReturn(ByVal dr As System.Data.DataRow) As Boolean
    End Interface
End Namespace
