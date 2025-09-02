Imports CommonInfo
Namespace TransferReturn
    Public Interface ITransferReturnController
        Function DeleteTransferReturn(ByVal TransferReturnID As String) As Boolean
        Function GetTransferReturn(ByVal TransferReturnID As String) As TransferReturnInfo
        Function GetAllTransferReturn() As DataTable

        Function SaveTransferReturn(ByVal TransferObj As TransferReturnInfo, ByVal _dtTransferReturnItem As DataTable) As Boolean
        Function GetTransferReturnItem(ByVal TransferReturnID As String) As DataTable

        Function GetHeaderTransferReturnList() As DataTable
        Function GetBranchTransferReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        'Function GetBranchTransferReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable

    End Interface
End Namespace

