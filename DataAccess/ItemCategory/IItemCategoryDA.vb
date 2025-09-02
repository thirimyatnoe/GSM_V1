Imports CommonInfo
Namespace ItemCategory
    Public Interface IItemCategoryDA
        Function InsertItemCategory(ByVal ItemCategoryObj As ItemCategoryInfo) As Boolean
        Function UpdateItemCategory(ByVal ItemCategoryObj As ItemCategoryInfo) As Boolean
        Function DeleteItemCategory(ByVal ItemCategoryID As String) As Boolean
        Function GetAllItemCategory() As DataTable
        Function GetAllItemCategoryByLocation(ByVal LocationID As String) As DataTable

        Function GetItemCategory(ByVal ItemCategoryID As String) As ItemCategoryInfo
        Function GetItemCategoryName(ByVal ItemCategory As String) As ItemCategoryInfo
        Function GetAllItemCategoryFromSearchBox() As DataTable

        Function HasItemCategory(ByVal ItemCategoryName As String, ByVal Prefix As String) As DataTable
        Function HasItemCategoryForUpdate(ByVal ItemCategoryName As String, ByVal OldItemCategory As String, ByVal OldPrefix As String, ByVal OldTax As Integer) As DataTable
        Function HasPrefixForUpdate(ByVal prefix As String, ByVal OldPrefix As String) As DataTable
        Function HasPrefixForUpdateUseItemCode(ByVal ItemCategoryId As String) As DataTable
        Function GetrptItemCategory() As DataTable
        Function HasTaxForUpdate(ByVal tax As String, ByVal OldTax As String) As DataTable
        Function HasTaxForUpdateUseItemCode(ByVal ItemCategoryId As String) As DataTable
        Function HasItemCategoryAndPrefix(ByVal ItemCategory As String, ByVal Prefix As String, ByVal ItemCategoryID As String) As DataTable
        Function GetBalance(ByVal CategoryID As String) As System.Data.DataTable
    End Interface
End Namespace

