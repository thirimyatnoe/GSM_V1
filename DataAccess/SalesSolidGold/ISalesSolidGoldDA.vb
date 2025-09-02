Imports CommonInfo
Namespace SalesSolidGold
    Public Interface ISalesSolidGoldDA
        Function InsertSalesSolidGold(ByVal SaleSolidGoldObj As SalesSolidGoldInfo) As Boolean
        Function UpdateSalesSolidGold(ByVal SaleSolidGoldObj As SalesSolidGoldInfo) As Boolean
        Function DeleteSalesSolidGold(ByVal SaleSolidGoldID As String) As Boolean

        Function InsertSalesSolidGoldItem(ByVal objItem As SalesSolidGoldItemInfo) As Boolean
        Function UpdateSalesSolidGoldItem(ByVal objItem As SalesSolidGoldItemInfo) As Boolean
        Function DeleteSalesSolidGoldItem(ByVal SalesSolidGoldItemID As String) As Boolean

        Function GetAllSalesSolidGold() As DataTable
        Function GetSalesSolidGoldItem(ByVal SaleSolidGoldID As String) As DataTable
        Function GetSalesSolidGold(ByVal SaleSolidGoldID As String) As SalesSolidGoldInfo

        Function GetSalesSolidGoldPrint(ByVal SaleSolidGoldID As String) As DataTable
        'Function GetCurrentGoldWgtByForSaleID(ByVal ForSaleID As String) As SalesSolidGoldInfo
        Function GetLeftGoldWgtByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetSalesSolidGoldReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String, Optional ByVal cristr As String = "") As DataTable
        Function GetTotalGoldWeightbyDate(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String) As DataTable
    End Interface
End Namespace

