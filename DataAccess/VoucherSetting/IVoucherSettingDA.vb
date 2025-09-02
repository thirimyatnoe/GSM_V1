Imports CommonInfo
Namespace VoucherSetting

    Public Interface IVoucherSettingDA
        Function InsertVoucherSetting(ByVal info As VoucherSettingInfo) As Boolean
        Function DeleteVoucherSetting() As Boolean
        Function GetAllVoucherSetting() As DataTable
        Function GetAllVoucherSettingByVoucher() As DataTable
    End Interface
End Namespace
