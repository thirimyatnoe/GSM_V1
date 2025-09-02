Imports CommonInfo
Namespace Repair
    Public Interface IRepairController

        Function SaveRepairReceive(ByVal obj As CommonInfo.RepairHeaderInfo, ByVal _dtRepairDetail As DataTable) As Boolean
        Function GetAllRepairHeader() As DataTable
        Function GetRepairHeaderInfo(RepairID As String) As RepairHeaderInfo
        Function GetRepairReceiveDetail(RepairID As String) As DataTable
        Function DeleteRepairReceive(RepairID As String) As Boolean
        '#ForRepairReturn
        Function GetReturnRepairHeaderForIsPaid() As DataTable
        Function GetForRepairReturnbyRepairDetail(RepairID As String, Optional ByVal BarcodeNo As String = "") As DataTable
        Function GetRepairReceiveDetailForUpdate(RepairID As String) As DataTable
        Function GetRepairDetailInfo(BarcodeNo As String) As RepairDetailInfo
        Function GetBalaceAmountByReceiveID(ByVal RepairID As String) As DataTable
        Function GetRepairReceiveForVoucher(RepairID As String) As DataTable
        Function GetRepairReceiveSummary(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable
        Function GetRepairStockDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairDetailInfo
    End Interface
End Namespace

