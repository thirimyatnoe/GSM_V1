Imports CommonInfo
Namespace Repair
    Public Interface IRepairDA

        Function InsertRepairHeader(ByVal Obj As CommonInfo.RepairHeaderInfo) As Boolean
        Function UpdateRepairHeader(ByVal Obj As CommonInfo.RepairHeaderInfo) As Boolean
        Function InsertRepairReceiveDetail(DetailObj As RepairDetailInfo) As Boolean
        Function UpdateRepairReceiveDetail(DetailObj As RepairDetailInfo) As Boolean
        Function DeleteRepairDetail(ByVal RepairDetailID As String) As Boolean

        Function GetAllRepairHeader() As DataTable
        Function GetRepairHeaderInfo(RepairID As String) As RepairHeaderInfo
        Function GetRepairReceiveDetail(RepairID As String) As DataTable
        '#For RepairReturn
        Function GetReturnRepairHeaderForIsPaid() As DataTable
        Function GetForRepairReturnbyRepairDetail(RepairID As String, Optional ByVal BarcodeNo As String = "") As DataTable
        Function GetRepairReceiveDetailForUpdate(RepairID As String) As DataTable
        Function GetRepairDetailInfo(BarcodeNo As String) As RepairDetailInfo
        Function GetRepairReceiveForVoucher(RepairID As String) As DataTable

        Function CheckIsUseInRepairReturnHeader(ByVal RepairID As String) As Boolean

        Function DeleteRepairReceiveHeader(ByVal RepairID As String) As Boolean
        Function GetBalaceAmountByReceiveID(RepairID As String) As DataTable
        Function GetRepairReceiveSummary(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable
        Function GetRepairStockDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairDetailInfo
    End Interface
End Namespace

