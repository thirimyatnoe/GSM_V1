Imports CommonInfo
Namespace TransferDiamond
    Public Interface ITransferDiamondDA
        Function InsertTransfer(ByVal TransferObj As TransferDiamondInfo) As Boolean
        Function UpdateTransfer(ByVal TransferObj As TransferDiamondInfo) As Boolean
        Function DeleteTransfer(ByVal TransferID As String) As Boolean
        Function GetAllTransfer() As DataTable
        Function GetTransferByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetTransfer(ByVal TransferID As String) As TransferDiamondInfo
        Function InsertTransferItem(ByVal ObjDmgItem As TransferDiamondItemInfo) As Boolean
        Function UpdateTransferItem(ByVal ObjDmgItem As TransferDiamondItemInfo) As Boolean
        Function DeleteTransferItem(ByVal TransferItemID As String) As Boolean
        Function GetTransferItem(ByVal TransferID As String) As DataTable
        Function GetHeaderTransferList() As DataTable
        Function GetForSaleIDByTransferID(ByVal TransferID As String) As DataTable
        Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function UpdateTransferItemReturn(ByVal dr As System.Data.DataRow) As Boolean
    End Interface
End Namespace
