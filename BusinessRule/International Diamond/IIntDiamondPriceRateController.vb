Imports CommonInfo
Namespace InternationalDiamond
    Public Interface IIntDiamondPriceRateController
        Function SaveIntDiamond(ByVal obj As IntDiamondPriceRateInfo) As Boolean
        Function GetIntDiamondList(ByVal argShape As String, ByVal argCaratFrom As Double, ByVal argCaratTo As Double) As DataTable
        Function GetIntDiamondByIntDiamondID(ByVal DefineID As String) As IntDiamondPriceRateInfo
        Function GetIntDiamondData(ByVal Carat As Decimal) As IntDiamondPriceRateInfo
        Function GetSaleReturnPercentByMaxDate(Optional ByVal cristr As String = "") As IntDiamondPriceRateInfo
        Function GetIntDiamondListForView() As DataTable
        Function DeleteIntDiamondPrice(ByVal DefineID As String) As Boolean
        Function GetAllDiamondPrice() As DataTable
    End Interface
End Namespace

