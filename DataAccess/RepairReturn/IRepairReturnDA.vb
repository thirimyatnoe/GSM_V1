Imports CommonInfo
Namespace RepairReturn
    Public Interface IRepairReturnDA
        Function InsertRepairReturnHeader(ByVal Obj As CommonInfo.RepairReturnHeaderInfo) As Boolean
        Function UpdateRepairReturnHeader(ByVal Obj As CommonInfo.RepairReturnHeaderInfo) As Boolean
        Function InsertRepairRreturnDetail(ByVal obj As CommonInfo.RepairReturnDetailInfo) As Boolean
        Function UpdateRepairRreturnDetail(ByVal obj As CommonInfo.RepairReturnDetailInfo) As Boolean
        Function DeleteReturnRepairDetail(ByVal ReturnRepairDetailID As String) As Boolean
        Function GetReturnRepairGemItemByDetailID(ByVal ReturnRepairDetailID As String) As System.Data.DataTable
        Function DeleteRepairReturnGemsItemByGemsID(ByVal ReturnRepairGemID As String) As Boolean
        Function InsertRepairReturnGemItem(ByVal Obj As CommonInfo.RepairReturnGemInfo) As Boolean
        Function GetRepairReturnDetailForUpdate(RepairID As String) As DataTable
        Function UpdateRepairReceiveHeaderByIsReturn(Obj As CommonInfo.RepairHeaderInfo) As Boolean
        Function GetRepairReturnDetailByHeaderID(ByVal RepairReturnHeaderID As String) As System.Data.DataTable
        Function UpdateRepairDetailByIsExit(ByVal obj As CommonInfo.RepairDetailInfo) As Boolean
        Function GetRepairReturnDetailByRepairID(ByVal RepairID As String) As System.Data.DataTable
        Function DeleteRepairReturn(ByVal RepairReturnID As String) As Boolean
        Function GetAllRepairReturnHeader() As System.Data.DataTable
        Function GetRepairReturnHeaderInfoByID(ByVal RepairReturnID As String) As RepairReturnHeaderInfo
        Function GetRepairReturnGemDataByHeaderID(ByVal RepairReturnID As String) As System.Data.DataTable
        Function GetRepairReturnForVoucher(ByVal RepairID As String) As System.Data.DataTable
        Function GetRepairReturnSummary(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable
        Function GetRepairReturnInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairReturnDetailInfo
        Function GetRepairReturnStockReportForTotalByDetail(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable
        Function GetRepairReturnDetailByRepairReturnDetailGem(ByVal ReturnRepairGemID As String) As DataTable
        Function GetReturnRepairDetail(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable
    End Interface
End Namespace


