Imports CommonInfo
Namespace Damage
    Public Interface IDamageDA
        Function InsertDamage(ByVal DamageObj As DamageInfo) As Boolean
        Function UpdateDamage(ByVal DamageObj As DamageInfo) As Boolean
        Function UpdateReadded(ByVal DamageObj As DamageItemInfo) As Boolean
        Function DeleteDamage(ByVal DamageID As String) As Boolean
        Function GetAllDamage() As DataTable
        Function GetDamage(ByVal DamageID As String) As DamageInfo
        Function InsertDamageItem(ByVal ObjDmgItem As DamageItemInfo) As Boolean
        Function UpdateDamageItem(ByVal ObjDmgItem As DamageItemInfo) As Boolean
        Function UpdateReaddedItem(ByVal ObjDmgItem As DamageItemInfo) As Boolean
        Function DeleteDamageItem(ByVal DamageItemID As String) As Boolean
        Function GetDamageItem(ByVal DamageID As String) As DataTable
        Function GetForSaleIDByDamageID(ByVal DamageID As String) As DataTable

        Function GetDamageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetDamageSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As DataTable
        Function GetAllDamageFromSearchBox() As DataTable
    End Interface
End Namespace


