Imports CommonInfo
Namespace GoldQuality
    Public Interface IGoldQualityDA
        Function InsertGoldQuality(ByVal GoldQualityObj As GoldQualityInfo) As Boolean
        Function UpdateGoldQuality(ByVal GoldQualityObj As GoldQualityInfo) As Boolean
        Function DeleteGoldQuality(ByVal AccountID As String) As Boolean
        Function GetAllGoldQuality() As DataTable
        Function GetAllGoldQualityByLocation(ByVal LocationID As String) As DataTable

        Function GetGoldQuality(ByVal GoldQualityID As String) As GoldQualityInfo
        Function GetAllGoldQualityFromSearchBox() As DataTable
        Function GetrptGoldQuality() As DataTable
        Function HasGoldQuality(ByVal GoldQuality As String, ByVal Prefix As String) As DataTable
        Function HasGoldQualityAndPrefix(ByVal GoldQuality As String, ByVal Prefix As String, ByVal GoldQualityID As String) As DataTable
        Function HasPrefixForUpdate(ByVal Prefix As String, ByVal GoldQualityID As String) As DataTable
        Function HasGoldQualityForUpdate(ByVal GoldQuality As String, ByVal GoldQualityID As String) As DataTable
        Function HasPrefixForUpdateUseItemCode(ByVal GoldQualityID As String) As DataTable
        Function CheckIsExitSolidGoldInGoldQuality(Optional GoldQualityID As String = "") As Boolean
        Function GetAllGoldQualityForWhiteGold() As DataTable
        Function GetAllGoldQualityDataForGoldPrice() As DataTable
        Function GetAllGoldQualityByItemCategory(ByVal ItemCategory As String) As CommonInfo.GoldQualityInfo
    End Interface
End Namespace

