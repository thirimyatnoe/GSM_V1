Imports CommonInfo
Namespace ItemName
    Public Interface IItemNameController
        Function InsertItemName(ByVal obj As CommonInfo.ItemNameInfo, ByVal _dtItemNameDetail As DataTable) As Boolean
        Function GetItemNameList() As DataTable
        Function GetItemNameListByItemCategory(ByVal ItemCategoryID As String) As DataTable
        Function GetItemID(ByVal ItemID As String) As DataTable
        Function GetItemName(ByVal ItemNameID As String) As ItemNameInfo
        Function GetItemNameByCategory(ByVal ItemCategoryID As String) As ItemNameInfo
        Function GetItemNameID(ByVal ItemName As String) As ItemNameInfo
        Function GetItemNamePhoto(ByVal ItemNameID As String) As ItemNameInfo
        Function GetItemPhoto(ByVal ItemNameID As String) As ItemNameInfo
        Function GetrptItemName() As DataTable
        ''Function GetItemPhoto(ByVal ItemNameID As String) As ItemNameInfo
        Function GetAllItemName() As DataTable
    End Interface
End Namespace

