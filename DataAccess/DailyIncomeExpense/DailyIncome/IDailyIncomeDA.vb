Imports CommonInfo

Namespace DailyIncome
    Public Interface IDailyIncomeDA
        Function InsertDailyIncomeHeader(ByVal DailyIncomeHeaderObj As DailyIncomeInfo) As Boolean
        Function UpdateDailyIncomeHeader(ByVal DailyIncomeHeaderObj As DailyIncomeInfo) As Boolean
        Function DeleteDailyIncomeHeader(ByVal DailyIncomeHeaderID As String) As Boolean
        Function GetDailyIncomeHeader(ByVal DailyIncomeHeaderID As String) As DailyIncomeInfo
        Function GetDailyIncomeHeaderList() As DataTable

        Function DeleteDailyIncomeItem(ByVal ItemID As String) As Boolean
        Function InsertDailyIncomeItem(ByVal DailyIncomeItemObj As DailyIncomeItemInfo) As Boolean
        Function UpdateDailyIncomeItem(ByVal DailyIncomeItemObj As DailyIncomeItemInfo) As Boolean
        Function GetDailyIncomeItems(ByVal DailyIncomeHeaderID) As DataTable
        Function GetDailyIncomeExpenseReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal CriStr1 As String, Optional ByVal criStr As String = "") As DataTable
    End Interface
End Namespace
