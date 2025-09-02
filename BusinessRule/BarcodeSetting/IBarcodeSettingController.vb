Imports CommonInfo
Namespace Barcodesetting

    Public Interface IBarcodeSettingController

        Function SaveBarcodeSetting(ByVal objBarcode As BarcodePrinterInfo) As Boolean
        Function DeleteBarcodeSetting() As Boolean
        Function GetBarcode() As DataTable
    End Interface

End Namespace
