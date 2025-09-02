Imports CommonInfo
Namespace InternationalDiamond
    Public Interface IIntDiamondPriceRateDA
        Function InsertIntDiamond(ByVal obj As IntDiamondPriceRateInfo) As Boolean
        Function UpdateIntDiamond(ByVal obj As IntDiamondPriceRateInfo) As Boolean
        Function DeleteIntDiamond(ByVal DefineID As String) As Boolean
        Function GetIntDiamondList(ByVal argShape As String, ByVal argCaratFrom As Double, ByVal argCaratTo As Double) As DataTable
        Function GetIntDiamondByIntDiamondID(ByVal DefineID As String) As IntDiamondPriceRateInfo
        Function GetIntDiamondData(ByVal Carat As Decimal) As IntDiamondPriceRateInfo
        Function GetSaleReturnPercentByMaxDate(Optional ByVal cristr As String = "") As IntDiamondPriceRateInfo
        Function GetIntDiamondListForView() As DataTable
        Function GetAllDiamondPrice() As DataTable
    End Interface
End Namespace

