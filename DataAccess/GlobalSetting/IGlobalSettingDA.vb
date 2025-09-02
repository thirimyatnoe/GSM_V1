Imports CommonInfo
Namespace GlobalSetting

    Public Interface IGlobalSettingDA
        Function InsertGlobalSetting(ByVal info As GlobalSettingInfo) As Boolean
        Function DeleteGlobalSetting() As Boolean
        Function GetAllGlobalSetting() As DataTable
        Function GetAllGlobalSettingInfo() As GlobalSettingInfo
    End Interface
End Namespace
