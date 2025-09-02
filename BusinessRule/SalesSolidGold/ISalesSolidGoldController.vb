Imports CommonInfo
Namespace SalesSolidGold
    Public Interface ISalesSolidGoldController
        Function SaveSalesSolidGold(ByVal SaleSolidGoldObj As SalesSolidGoldInfo, ByVal dtItem As DataTable) As Boolean
        Function DeleteSalesSolidGold(ByVal SaleSolidGoldObj As SalesSolidGoldInfo) As Boolean
        Function GetAllSalesSolidGold() As DataTable
        Function GetSalesSolidGold(ByVal SaleSolidGoldID As String) As SalesSolidGoldInfo
        Function GetSalesSolidGoldItem(ByVal SaleSolidGoldID As String) As DataTable
        Function GetSalesSolidGoldPrint(ByVal SaleSolidGoldID As String) As DataTable
        'Function GetCurrentGoldWgtByForSaleID(ByVal ForSaleID As String) As SalesSolidGoldInfo
        Function GetLeftGoldWgtByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetSalesSolidGoldReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String, Optional ByVal cristr As String = "") As DataTable
        Function GetTotalGoldWeightbyDate(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String) As DataTable
    End Interface
End Namespace

