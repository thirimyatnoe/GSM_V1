Imports CommonInfo
Namespace RepairReturn
    Public Interface IRepairReturnController

        Function SaveRepairReturn(ByVal objReturn As CommonInfo.RepairReturnHeaderInfo, ByVal _dtRepairDetail As System.Data.DataTable, ByVal _dtAllGem As DataTable) As Boolean
        Function DeleteRepairReturn(ByVal RepairReturnID As String, ByVal RepairID As String) As Boolean
        Function GetAllRepairReturnHeader() As System.Data.DataTable
        Function GetRepairReturnHeaderInfoByID(ByVal RepairReturnID As String) As CommonInfo.RepairReturnHeaderInfo
        Function GetRepairReturnDetailByHeaderID(ByVal RepairReturnID As String) As System.Data.DataTable
        Function GetRepairReturnGemDataByHeaderID(ByVal RepairReturnID As String) As System.Data.DataTable
        Function GetRepairReturnForVoucher(ByVal ReturnID As String) As System.Data.DataTable
        Function GetRepairReturnSummary(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable
        Function GetRepairReturnInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairReturnDetailInfo
        Function GetRepairReturnStockReportForTotalByDetail(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable
        Function GetRepairReturnDetailByRepairReturnDetailGem(ByVal ReturnRepairGemID As String) As DataTable
        Function GetReturnRepairDetail(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable
    End Interface
End Namespace

