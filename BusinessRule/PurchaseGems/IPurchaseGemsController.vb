Imports CommonInfo
Namespace PurchaseGems
    Public Interface IPurchaseGemsController

        Function SavePurchaseGems(ByVal PurchaseGemsObj As PurchaseGemsInfo, ByVal _dtPurchaseGemsItem As DataTable) As Boolean
        Function DeletePurchaseGems(ByVal PurchaseGemsID As String) As Boolean
        Function GetAllPurchaseGems() As DataTable
        Function GetAllPurchaseGem() As DataTable
        Function GetPurchaseGems(ByVal PurchaseGemsID As String) As PurchaseGemsInfo
        Function GetPurchaseGemsReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetPurchaseGemsItem(ByVal PurchaseGemsID As String) As DataTable
        Function GetPurchaseGemsPrint(ByVal PurchaseGemsID As String) As DataTable

        Function InsertPurchaseGemsUserID(ByVal PurchaseGemsID As String, ByVal UserID As String) As Boolean
    End Interface
End Namespace
