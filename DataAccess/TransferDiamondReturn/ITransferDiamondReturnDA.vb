Imports CommonInfo
Namespace TransferDiamondReturn
    Public Interface ITransferDiamondReturnDA
        Function InsertTransferReturn(ByVal TransferReturnObj As TransferReturnDiamondInfo) As Boolean
        Function UpdateTransferReturn(ByVal TransferReturnObj As TransferReturnDiamondInfo) As Boolean
        Function DeleteTransferReturn(ByVal TransferReturnID As String) As Boolean
        Function GetAllTransferReturn() As DataTable
        Function GetTransferReturnByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetTransferReturn(ByVal TransferReturnID As String) As TransferReturnDiamondInfo
        Function InsertTransferReturnItem(ByVal ObjDmgItem As TransferReturnDiamondItemInfo) As Boolean
        Function UpdateTransferReturnItem(ByVal ObjDmgItem As TransferReturnDiamondItemInfo) As Boolean
        Function DeleteTransferReturnItem(ByVal TransferReturnItemID As String) As Boolean
        Function GetTransferReturnItem(ByVal TransferReturnID As String) As DataTable
        Function GetHeaderTransferReturnList() As DataTable
        Function GetForSaleIDByTransferReturnID(ByVal TransferReturnID As String) As DataTable
        Function GetBranchTransferReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        'Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        'Function UpdateTransferReturnItemReturn(ByVal dr As System.Data.DataRow) As Boolean
    End Interface
End Namespace
