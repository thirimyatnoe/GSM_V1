Imports CommonInfo
Namespace GlobalSetting
    Public Interface IGlobalSettingController
        Function DeleteGlobalSetting() As Boolean
        Function SaveGlobalSetting(ByVal info As GlobalSettingInfo) As Boolean
        Function GetAllGlobalSetting() As DataTable
        Function GetAllGlobalSettingInfo() As GlobalSettingInfo
    End Interface
End Namespace
