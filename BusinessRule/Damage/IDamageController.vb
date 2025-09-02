Imports CommonInfo
Namespace Damage
    Public Interface IDamageController
        Function DeleteDamage(ByVal DamageID As String) As Boolean
        Function GetDamage(ByVal DamageID As String) As DamageInfo
        Function GetAllDamage() As DataTable
        Function SaveDamage(ByVal DamageObj As DamageInfo, ByVal _dtDamageItem As DataTable) As Boolean
        Function SaveReadded(ByVal DamageItemObj As DamageItemInfo, ByVal _dtDamageItem As DataTable) As Boolean
        Function GetDamageItem(ByVal DamageID As String) As DataTable
        Function GetDamageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetDamageSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As DataTable
        Function GetAllDamageFromSearchBox() As DataTable
    End Interface


End Namespace