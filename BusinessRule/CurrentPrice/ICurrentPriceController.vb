Imports CommonInfo
Namespace CurrentPrice
    Public Interface ICurrentPriceController
        Function InsertCurrentPrice(ByVal CurrentPriceObj As CurrentPriceInfo) As Boolean
        Function DeleteCurrentPrice(ByVal DefineID As String) As Boolean
        Function GetCurrentPrice(ByVal DefineID As String) As CurrentPriceInfo
        Function GetAllCurrentPrice() As DataTable
        Function GetCurrentPriceByGoldID(ByVal GoldQualityID As String) As CurrentPriceInfo
        Function GetAllCurrentPriceForPreview(Optional cristr As String = "") As DataTable
        Function SaveGoldPriceData(ByVal dtGoldPrice As DataTable, ByVal DefineDate As Date) As Boolean
        Function GetAllGoldPriceListByDateTime() As DataTable
        Function GetGoldPriceDataByDateTime(ByVal DefineDate As Date) As DataTable
        Function GetSolidGoldPrice() As DataTable
        Function DeleteGoldPriceData(ByVal _dtGoldPrice As DataTable) As Boolean

    End Interface

End Namespace


