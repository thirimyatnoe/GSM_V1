Imports CommonInfo

Namespace DailyExpense
    Public Interface IDailyExpenseController
        Function SaveDailyExpense(ByRef DailyExpenseObj As DailyExpenseInfo, ByVal DailyExpenseItems As DataTable) As Boolean
        Function DeleteDailyExpense(ByVal DailyExpenseHeaderID As String) As Boolean
        Function GetDailyExpenseHeader(ByVal DailyExpenseID As String) As DailyExpenseInfo
        Function GetDailyExpenseItems(ByVal DailyExpenseID As String) As DataTable
        Function GetDailyExpenseList() As DataTable
    End Interface
End Namespace

