Imports CommonInfo

Namespace DailyIncome
    Public Interface IDailyIncomeController
        Function SaveDailyIncome(ByRef DailyIncomeObj As DailyIncomeInfo, ByVal DailyIncomeItems As DataTable) As Boolean
        Function DeleteDailyIncome(ByVal DailyIncomeHeaderID As String) As Boolean
        Function GetDailyIncomeHeader(ByVal DailyIncomeID As String) As DailyIncomeInfo
        Function GetDailyIncomeItems(ByVal DailyIncomeID As String) As DataTable
        Function GetDailyIncomeList() As DataTable
        Function GetDailyIncomeExpenseReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal CriStr1 As String, Optional ByVal criStr As String = "") As DataTable
    End Interface
End Namespace

