Imports CommonInfo
Namespace Setting
    Public Interface ISettingDA
        Function SaveSetting(ByVal settingobj As CommonInfo.SettingInfo) As Boolean
        Function SaveSettingBy(ByVal settingobj As CommonInfo.SettingInfo, ByVal CurrentUserID As String) As Boolean
        Function DeleteSetting(ByVal KeyName As String) As Boolean

        Function GetApplicationOptionByKeyName(ByVal KeyName As String)
        Function GetKeyvalue(ByVal KeyName As String) As SettingInfo
        Function GetKeyName() As DataTable
        Function getdatabykeyname(ByVal KeyName As String) As SettingInfo
        Function GetApplicationOptionByKeyNameBy(ByVal KeyName As String, ByVal CurrentUserID As String) As Object
        Function GetVersion() As String
    End Interface
End Namespace




