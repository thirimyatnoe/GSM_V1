Imports CommonInfo
Namespace Converter
    Public Interface IConverterController
        Function ConvertKPYCToGoldTK(ByRef argGoldWeight As GoldWeightInfo) As Decimal
        Function ConvertGoldTKToKPYC(ByRef argGoldWeight As GoldWeightInfo) As GoldWeightInfo
        Function GetMeasurement(ByVal argFromMeasurement As String, ByVal argToMeasurement As String) As Decimal
    End Interface
End Namespace

