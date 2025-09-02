Imports CommonInfo

Namespace BarcodeSetting
    Public Interface IBarcodeSettingDA
        Function InsertBarcodSetting(ByVal objBarcode As BarcodePrinterInfo) As Boolean
        Function DeleteBarcodeSetting() As Boolean
        Function GetBarcode() As DataTable
    End Interface
End Namespace

