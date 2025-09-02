Imports CommonInfo
Namespace Dashboard
    Public Interface IDashboardDA

        Function GetAllCashAndCredit(Optional ByVal SaleType As String = "", Optional ByVal Cristr As String = "", Optional ByVal DateType As String = "") As DataTable
        Function GetAllSaleByDate(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Type As String) As DataTable
        Function GetAllStockBalance(Optional ByVal SortingType As String = "", Optional ByVal BalanceType As String = "") As DataTable
        Function GetAllSaleByCategory(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Type As String, Optional ByVal ItemType As String = "") As DataTable
        Function GetAllSaleByLocation(Optional ByVal SaleType As String = "", Optional ByVal Cristr As String = "", Optional ByVal DateType As String = "") As DataTable
        Function GetAllCredit() As DataTable
    End Interface
End Namespace
