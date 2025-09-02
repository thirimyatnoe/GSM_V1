Imports DataAccess.GlobalSetting
Imports CommonInfo
Namespace GlobalSetting
    Public Class GlobalSettingController
        Implements IGlobalSettingController

#Region "Private Members"

        Private _objGlobalSettingDA As IGlobalSettingDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IGlobalSettingController = New GlobalSettingController

#End Region

#Region "Constructors"

        Private Sub New()
            _objGlobalSettingDA = DataAccess.Factory.Instance.CreateGlobalSettingDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGlobalSettingController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetAllGlobalSetting() As System.Data.DataTable Implements IGlobalSettingController.GetAllGlobalSetting
            Return _objGlobalSettingDA.GetAllGlobalSetting
        End Function

        Public Function SaveGlobalSetting(ByVal info As CommonInfo.GlobalSettingInfo) As Boolean Implements IGlobalSettingController.SaveGlobalSetting
            Return _objGlobalSettingDA.InsertGlobalSetting(info)
        End Function

        Public Function DeleteGlobalSetting() As Boolean Implements IGlobalSettingController.DeleteGlobalSetting
            Return _objGlobalSettingDA.DeleteGlobalSetting()
        End Function
        Public Function GetAllGlobalSettingInfo() As CommonInfo.GlobalSettingInfo Implements IGlobalSettingController.GetAllGlobalSettingInfo
            Return _objGlobalSettingDA.GetAllGlobalSettingInfo
        End Function
    End Class
End Namespace