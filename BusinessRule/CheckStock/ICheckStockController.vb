Imports CommonInfo
Namespace CheckStock
    Public Interface ICheckStockController
        Function InsertCheckStock(ByVal objCheckStock As CheckStockInfo, ByVal _dtMissing As System.Data.DataTable, ByVal _dtExtra As DataTable, ByVal _dtFind As DataTable) As Boolean
        Function GetCheckStockPrint(ByVal CheckStockID As String) As DataTable
        Function GetMCheckStockPrint(ByVal CheckStockID As String) As DataTable
        Function GetECheckStockPrint(ByVal CheckStockID As String) As DataTable
        Function GetCheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetMCheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetECheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetAllCheckStockLists(Optional ByVal cristr As String = "") As DataTable
        Function GetCheckStockByID(ByVal CheckStockID As String) As CheckStockInfo
        Function GetCheckStockItem(ByVal CheckStockID As String) As DataTable
        Function GetCheckStockEItem(ByVal CheckStockID As String) As DataTable
        Function DeleteCheckStock(ByVal CheckStockID As String) As Boolean
        Function ResetIsCheck(ByVal ItemCategoryID As String) As Boolean

        'test
    End Interface
End Namespace