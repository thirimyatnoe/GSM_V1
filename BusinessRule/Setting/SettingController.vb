Imports CommonInfo
Imports DataAccess.Setting

Namespace Setting
    Friend Class SettingController
        Implements ISettingController








        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private _objSettingDA As ISettingDA
        Private Shared ReadOnly _instance As ISettingController = New SettingController
        Private Sub New()
            _objSettingDA = DataAccess.Factory.Instance.CreateSettingDA

        End Sub

        Public Shared ReadOnly Property Instance() As ISettingController
            Get
                Return _instance
            End Get
        End Property

        Public Function SaveSetting(ByVal settingobj As CommonInfo.SettingInfo) As Boolean Implements ISettingController.SaveSetting

            _objSettingDA.DeleteSetting(settingobj.KeyName)



            Return _objSettingDA.SaveSetting(settingobj)

        End Function

        Public Function GetApplicationOptionByKeyName(ByVal KeyName As String) As Object Implements ISettingController.GetApplicationOptionByKeyName
            Return _objSettingDA.GetApplicationOptionByKeyName(KeyName)
        End Function

        Public Function GetKeyvalue(ByVal KeyName As String) As CommonInfo.SettingInfo Implements ISettingController.GetKeyvalue
            Return _objSettingDA.GetKeyvalue(KeyName)
        End Function


        Public Function GetKeyName() As System.Data.DataTable Implements ISettingController.GetKeyName
            Return _objSettingDA.GetKeyName()
        End Function

        Public Function getdatabykeyname(ByVal KeyName As String) As CommonInfo.SettingInfo Implements ISettingController.getdatabykeyname
            Return _objSettingDA.getdatabykeyname(KeyName)
        End Function

        Public Function GetApplicationOptionByKeyNameBy(ByVal KeyName As String, ByVal CurrentUserID As String) As Object Implements ISettingController.GetApplicationOptionByKeyNameBy
            Return _objSettingDA.GetApplicationOptionByKeyNameBy(KeyName, CurrentUserID)
        End Function

        Public Function SaveSettingBy(ByVal settingobj As CommonInfo.SettingInfo, ByVal CurrentUserID As String) As Boolean Implements ISettingController.SaveSettingBy
            Return _objSettingDA.SaveSettingBy(settingobj, CurrentUserID)
        End Function

        Public Function GetVersion() As String Implements ISettingController.GetVersion
            Return _objSettingDA.GetVersion()
        End Function
    End Class

End Namespace
