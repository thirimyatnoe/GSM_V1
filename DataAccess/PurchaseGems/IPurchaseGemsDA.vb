Imports CommonInfo
Namespace PurchaseGems

    Public Interface IPurchaseGemsDA
        Function InsertPurchaseGems(ByVal PurchaseGemsObj As PurchaseGemsInfo) As Boolean
        Function UpdatePurchaseGems(ByVal PurchaseGemsObj As PurchaseGemsInfo) As Boolean
        Function DeletePurchaseGems(ByVal PurchaseGemsID As String) As Boolean
        Function GetAllPurchaseGems() As DataTable
        Function GetAllPurchaseGem() As DataTable
        Function GetPurchaseGems(ByVal PurchaseGemsID As String) As PurchaseGemsInfo
        Function GetPurchaseGemsReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function InsertPurchaseGemsItem(ByVal ObjPurchaseGemsItem As PurchaseGemsItemInfo) As Boolean
        Function UpdatePurchaseGemsItem(ByVal ObjPurchaseGemsItem As PurchaseGemsItemInfo) As Boolean
        Function DeletePurchaseGemsItem(ByVal PurchaseGemsItemID As String) As Boolean
        Function GetPurchaseGemsItem(ByVal PurchaseGemsID As String) As DataTable
        Function GetPurchaseGemsPrint(ByVal PurchaseGemsID As String) As DataTable

        Function InsertPurchaseGemsUserID(ByVal PurchaseGemsID As String, ByVal UserID As String) As Boolean
         End Interface
End Namespace

