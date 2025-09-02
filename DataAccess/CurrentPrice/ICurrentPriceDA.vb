Imports CommonInfo
Namespace CurrentPrice
    Public Interface ICurrentPriceDA
        Function InsertCurrentPrice(ByVal CurrentPriceObj As CurrentPriceInfo) As Boolean
        Function UpdateCurrentPrice(ByVal CurrentPriceObj As CurrentPriceInfo) As Boolean
        Function DeleteCurrentPrice(ByVal DefineID As String) As Boolean
        Function GetAllCurrentPrice() As DataTable
        Function GetCurrentPrice(ByVal DefineID As String) As CurrentPriceInfo
        Function GetCurrentPriceByGoldID(ByVal GoldQualityID As String) As CurrentPriceInfo
        Function GetAllCurrentPriceForPreview(Optional cristr As String = "") As DataTable
        Function GetAllGoldPriceListByDateTime() As DataTable
        Function GetGoldPriceDataByDateTime(ByVal DefineDate As Date) As DataTable
        Function GetSolidGoldPrice() As DataTable
    End Interface
End Namespace


