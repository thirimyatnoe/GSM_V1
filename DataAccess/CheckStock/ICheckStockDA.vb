Imports CommonInfo
Namespace CheckStock
    Public Interface ICheckStockDA
        Function InsertCheckStock(ByVal objCheckStock As CheckStockInfo) As Boolean
        Function InsertCheckStockItem(ByVal objCheckStockItem As CheckStockItemInfo) As Boolean
        Function InsertECheckStockItem(ByVal objECheckStockItem As ECheckStockItemInfo) As Boolean
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
        Function UpdateCheckStock(ByVal objCheckStock As CheckStockInfo) As Boolean
        Function UpdateMCheckStock(ByVal objMCheckStock As CheckStockItemInfo) As Boolean
        Function DeleteCheckStock(ByVal CheckStockID As String) As Boolean
        Function DeleteECheckStock(ByVal ECheckStockItemID As String) As Boolean
        Function DeleteMCheckStock(ByVal CheckStockItemID As String) As Boolean
        Function UpdateECheckStock(ByVal objECheckStock As ECheckStockItemInfo) As Boolean
        Function UpdateIsCheck(ByVal ForSaleID As String, ByVal IsCheck As Boolean) As Boolean
        Function ResetIsCheck(ByVal ItemCategoryID As String) As Boolean
        'test
    End Interface
End Namespace
