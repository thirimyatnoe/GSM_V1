Imports DataAccess.GemsCategory
Namespace GemsCategory
    Public Class GemsCategoryController
        Implements IGemsCategoryController


#Region "Private Members"

        Private _objGemsCategoryDA As IGemsCategoryDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IGemsCategoryController = New GemsCategoryController

#End Region

#Region "Constructors"

        Private Sub New()
            _objGemsCategoryDA = DataAccess.Factory.Instance.CreateGemsCategoryDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGemsCategoryController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteGemsCategory(ByVal GemsCategoryID As String) As Boolean Implements IGemsCategoryController.DeleteGemsCategory
            If _objGemsCategoryDA.DeleteGemsCategory(GemsCategoryID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.GemsCategory.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                            GemsCategoryID, _
                            "Delete Gems Category")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllGemsCategory() As System.Data.DataTable Implements IGemsCategoryController.GetAllGemsCategory
            Return _objGemsCategoryDA.GetAllGemsCategory()
        End Function

        Public Function GetGemsCategory(ByVal GemsCategoryID As String) As CommonInfo.GemsCategoryInfo Implements IGemsCategoryController.GetGemsCategory
            Return _objGemsCategoryDA.GetGemsCategory(GemsCategoryID)
        End Function

        Public Function InsertGemsCategory(ByVal obj As CommonInfo.GemsCategoryInfo) As Boolean Implements IGemsCategoryController.InsertGemsCategory
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            If obj.GemsCategoryID = "0" Then
                obj.GemsCategoryID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.GemsCategory, CommonInfo.EnumSetting.GenerateKeyType.GemsCategory.ToString, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.GemsCategory.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                            obj.GemsCategoryID, _
                            "Insert Gems Category")
                Return _objGemsCategoryDA.InsertGemsCategory(obj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.GemsCategory.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                            obj.GemsCategoryID, _
                            "Update Gems Category")
                Return _objGemsCategoryDA.UpdateGemsCategory(obj)
            End If
        End Function
        Public Function GetAllGemsCategoryForGridCombo() As System.Data.DataTable Implements IGemsCategoryController.GetAllGemsCategoryForGridCombo
            Return _objGemsCategoryDA.GetAllGemsCategoryForGridCombo()
        End Function

        Public Function GetAllGemsCategoryFromSearchBox() As System.Data.DataTable Implements IGemsCategoryController.GetAllGemsCategoryFromSearchBox
            Return _objGemsCategoryDA.GetAllGemsCategoryFromSearchBox()
        End Function

        Public Function GetrptGemsCategory() As DataTable Implements IGemsCategoryController.GetrptGemsCategory
            Return _objGemsCategoryDA.GetrptGemsCategory()
        End Function

        Public Function GetGemsCategoryByGemsCategory(ByVal GemsCategory As String, Optional ByVal GemsCategoryID As String = "") As DataTable Implements IGemsCategoryController.GetGemsCategoryByGemsCategory

            Return _objGemsCategoryDA.GetGemsCategoryByGemsCategory(GemsCategory, GemsCategoryID)
        End Function
        Public Function HasPrefixForUpdate(ByVal prefix As String, ByVal OldPrefix As String) As System.Data.DataTable Implements IGemsCategoryController.HasPrefixForUpdate
            Return _objGemsCategoryDA.HasPrefixForUpdate(prefix, OldPrefix)
        End Function
    End Class
End Namespace

