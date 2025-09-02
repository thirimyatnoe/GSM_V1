Imports CommonInfo
Namespace Transfer
    Public Interface ITransferController
        Function DeleteTransfer(ByVal TransferID As String) As Boolean
        Function GetTransfer(ByVal TransferID As String) As TransferInfo
        Function GetAllTransfer() As DataTable

        Function SaveTransfer(ByVal TransferObj As TransferInfo, ByVal _dtTransferItem As DataTable) As Boolean
        Function GetTransferItem(ByVal TransferID As String) As DataTable

        Function GetHeaderTransferList() As DataTable
        Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        
    End Interface
End Namespace

