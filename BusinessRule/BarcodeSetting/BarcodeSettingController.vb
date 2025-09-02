
Imports DataAccess.BarcodeSetting
Imports CommonInfo
Namespace Barcodesetting
    Public Class BarcodeSettingController
        Implements IBarcodeSettingController



#Region "Private Members"

        Private _objBarcodeSettinttDA As IBarcodeSettingDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IBarcodeSettingController = New BarcodeSettingController

#End Region

#Region "Constructor"
        Private Sub New()
            _objBarcodeSettinttDA = DataAccess.Factory.Instance.CreateBarCodeSettingDA
        End Sub
#End Region

#Region "Public Property"
        Public Shared ReadOnly Property Instance() As IBarcodeSettingController
            Get
                Return _instance
            End Get
        End Property
#End Region


        Public Function DeleteBarcodeSetting() As Boolean Implements IBarcodeSettingController.DeleteBarcodeSetting
            Return _objBarcodeSettinttDA.DeleteBarcodeSetting
        End Function

        Public Function SaveBarcodeSetting(objBarcode As CommonInfo.BarcodePrinterInfo) As Boolean Implements IBarcodeSettingController.SaveBarcodeSetting
            Return _objBarcodeSettinttDA.InsertBarcodSetting(objBarcode)
        End Function

        Public Function GetBarcode() As DataTable Implements IBarcodeSettingController.GetBarcode
            Return _objBarcodeSettinttDA.GetBarcode()
        End Function
    End Class

End Namespace



