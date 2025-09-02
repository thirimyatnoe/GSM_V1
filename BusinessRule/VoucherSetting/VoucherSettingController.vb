Imports DataAccess.VoucherSetting
Imports CommonInfo
Namespace VoucherSetting
    Public Class VoucherSettingController
        Implements IVoucherSettingController

#Region "Private Members"

        Private _objVoucherSettingDA As IVoucherSettingDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IVoucherSettingController = New VoucherSettingController

#End Region

#Region "Constructors"

        Private Sub New()
            _objVoucherSettingDA = DataAccess.Factory.Instance.CreateVoucherSettingDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IVoucherSettingController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetAllVoucherSetting() As System.Data.DataTable Implements IVoucherSettingController.GetAllVoucherSetting
            Return _objVoucherSettingDA.GetAllVoucherSetting
        End Function

        Public Function SaveVoucherSetting(ByVal info As CommonInfo.VoucherSettingInfo) As Boolean Implements IVoucherSettingController.SaveVoucherSetting
            Return _objVoucherSettingDA.InsertVoucherSetting(info)
        End Function

        Public Function DeleteVoucherSetting() As Boolean Implements IVoucherSettingController.DeleteVoucherSetting
            Return _objVoucherSettingDA.DeleteVoucherSetting()
        End Function
        Public Function GetAllVoucherSettingByVoucher() As System.Data.DataTable Implements IVoucherSettingController.GetAllVoucherSettingByVoucher
            Return _objVoucherSettingDA.GetAllVoucherSettingByVoucher
        End Function
    End Class
End Namespace