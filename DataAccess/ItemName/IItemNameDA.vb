Imports CommonInfo
Namespace ItemName
    Public Interface IItemNameDA
        Function InsertItemName(ByVal ItemNameObj As ItemNameInfo) As Boolean
        Function InsertItemPhoto(ByVal ItemNameObj As ItemNameInfo) As Boolean
        Function GetItemNameList() As DataTable
        Function GetItemNameListByItemCategory(ByVal ItemCategoryID As String) As DataTable
        Function GetItemID(ByVal ItemID As String) As DataTable
        Function UpdateItemName(ByVal ItemNameObj As ItemNameInfo) As Boolean
        Function UpdateItemPhoto(ByVal ItemNameObj As ItemNameInfo) As Boolean
        Function DeleteItemName(ByVal ItemNameID As String) As Boolean
        Function GetItemName(ByVal ItemNameID As String) As ItemNameInfo
        Function GetItemNameID(ByVal ItemName As String) As ItemNameInfo
        Function GetItemNamePhoto(ByVal ItemNameID As String) As ItemNameInfo
        Function GetItemPhoto(ByVal ItemNameID As String) As ItemNameInfo
        Function GetItemNameByCategory(ByVal ItemCategoryID As String) As ItemNameInfo
        Function GetrptItemName() As DataTable
        Function GetAllItemName() As DataTable
        Function GetItemNameByItemName(ByVal ItemName As String, ByVal ItemCategoryID As String) As DataTable
        Function GetItemNameByIDForUpdate(ByVal ItemNameID As String, ByVal ItemName As String, ByVal ItemCategoryID As String) As DataTable
        Function HasPrefixForUpdateUseItemCode(ByVal ItemNameID As String) As DataTable
    End Interface
End Namespace

