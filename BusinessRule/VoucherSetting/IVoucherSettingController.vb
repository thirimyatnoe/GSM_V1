Imports CommonInfo
Namespace VoucherSetting
    Public Interface IVoucherSettingController
        Function DeleteVoucherSetting() As Boolean
        Function SaveVoucherSetting(ByVal info As VoucherSettingInfo) As Boolean
        Function GetAllVoucherSetting() As DataTable
        Function GetAllVoucherSettingByVoucher() As DataTable
    End Interface
End Namespace
