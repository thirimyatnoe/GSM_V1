Imports CommonInfo

Namespace DailyExpense
    Public Interface IDailyExpenseDA
        Function InsertDailyExpenseHeader(ByVal DailyExpenseHeaderObj As DailyExpenseInfo) As Boolean
        Function UpdateDailyExpenseHeader(ByVal DailyExpenseHeaderObj As DailyExpenseInfo) As Boolean
        Function DeleteDailyExpenseHeader(ByVal DailyExpenseHeaderID As String) As Boolean
        Function GetDailyExpenseHeader(ByVal DailyExpenseHeaderID As String) As DailyExpenseInfo
        Function GetDailyExpenseHeaderList() As DataTable

        Function DeleteDailyExpenseItem(ByVal ItemID As String) As Boolean
        Function InsertDailyExpenseItem(ByVal DailyExpenseItemObj As DailyExpenseItemInfo) As Boolean
        Function UpdateDailyExpenseItem(ByVal DailyExpenseItemObj As DailyExpenseItemInfo) As Boolean
        Function GetDailyExpenseItems(ByVal DailyExpenseHeaderID) As DataTable
    End Interface
End Namespace
