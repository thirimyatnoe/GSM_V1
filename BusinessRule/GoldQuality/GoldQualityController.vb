Imports DataAccess.GoldQuality
Namespace GoldQuality
    Public Class GoldQualityController
        Implements IGoldQualityController

#Region "Private Members"

        Private _objGoldQualityDA As IGoldQualityDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IGoldQualityController = New GoldQualityController

#End Region

#Region "Constructors"

        Private Sub New()
            _objGoldQualityDA = DataAccess.Factory.Instance.CreateGoldQualityDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGoldQualityController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function InsertGoldQuality(ByVal obj As CommonInfo.GoldQualityInfo) As Boolean Implements IGoldQualityController.InsertGoldQuality
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            If obj.GoldQualityID = "0" Then
                obj.GoldQualityID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.GoldQuality, CommonInfo.EnumSetting.GenerateKeyType.GoldQuality.ToString, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                           DateTime.Now, _
                           Global_UserID, _
                           CommonInfo.EnumSetting.GenerateKeyType.GoldQuality.ToString, _
                           CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                           obj.GoldQualityID, _
                           "Insert Gold Quality")
                Return _objGoldQualityDA.InsertGoldQuality(obj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                           DateTime.Now, _
                           Global_UserID, _
                           CommonInfo.EnumSetting.GenerateKeyType.GoldQuality.ToString, _
                           CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                           obj.GoldQualityID, _
                           "Update Gold Quality")
                Return _objGoldQualityDA.UpdateGoldQuality(obj)
            End If
        End Function
        Public Function DeleteGoldQuality(ByVal GoldQualityID As String) As Boolean Implements IGoldQualityController.DeleteGoldQuality
            If _objGoldQualityDA.DeleteGoldQuality(GoldQualityID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                           DateTime.Now, _
                           Global_UserID, _
                           CommonInfo.EnumSetting.GenerateKeyType.GoldQuality.ToString, _
                           CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                           GoldQualityID, _
                           "Delete Gold Quality")
                Return True
            Else
                Return False
            End If
        End Function
        Public Function GetAllGoldQuality() As System.Data.DataTable Implements IGoldQualityController.GetAllGoldQuality
            Return _objGoldQualityDA.GetAllGoldQuality()
        End Function
        Public Function GetAllGoldQualityByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IGoldQualityController.GetAllGoldQualityByLocation
            Return _objGoldQualityDA.GetAllGoldQualityByLocation(LocationID)
        End Function

        Public Function GetGoldQuality(ByVal GoldQualityID As String) As CommonInfo.GoldQualityInfo Implements IGoldQualityController.GetGoldQuality
            Return _objGoldQualityDA.GetGoldQuality(GoldQualityID)
        End Function

        Public Function GetAllGoldQualityFromSearchBox() As System.Data.DataTable Implements IGoldQualityController.GetAllGoldQualityFromSearchBox
            Return _objGoldQualityDA.GetAllGoldQualityFromSearchBox()
        End Function

        Public Function GetrptGoldQuality() As DataTable Implements IGoldQualityController.GetrptGoldQuality
            Return _objGoldQualityDA.GetrptGoldQuality
        End Function

        Public Function HasGoldQuality(ByVal GoldQuality As String, ByVal Prefix As String) As DataTable Implements IGoldQualityController.HasGoldQuality
            Return _objGoldQualityDA.HasGoldQuality(GoldQuality, Prefix)
        End Function

        Public Function HasGoldQualityAndPrefix(ByVal GoldQuality As String, ByVal Prefix As String, ByVal GoldQualityID As String) As DataTable Implements IGoldQualityController.HasGoldQualityAndPrefix
            Return _objGoldQualityDA.HasGoldQualityAndPrefix(GoldQuality, Prefix, GoldQualityID)
        End Function

        Public Function HasPrefixForUpdate(ByVal Prefix As String, ByVal GoldQualityID As String) As DataTable Implements IGoldQualityController.HasPrefixForUpdate
            Return _objGoldQualityDA.HasPrefixForUpdate(Prefix, GoldQualityID)
        End Function

        Public Function HasGoldQualityForUpdate(ByVal GoldQuality As String, ByVal GoldQualityID As String) As DataTable Implements IGoldQualityController.HasGoldQualityForUpdate
            Return _objGoldQualityDA.HasGoldQualityForUpdate(GoldQuality, GoldQualityID)
        End Function

        Public Function HasPrefixForUpdateUseItemCode(ByVal GoldQualityID As String) As DataTable Implements IGoldQualityController.HasPrefixForUpdateUseItemCode
            Return _objGoldQualityDA.HasPrefixForUpdateUseItemCode(GoldQualityID)
        End Function

        Public Function CheckIsExitSolidGoldInGoldQuality(Optional GoldQualityID As String = "") As Boolean Implements IGoldQualityController.CheckIsExitSolidGoldInGoldQuality
            Return _objGoldQualityDA.CheckIsExitSolidGoldInGoldQuality(GoldQualityID)
        End Function
        Public Function GetAllGoldQualityForWhiteGold() As System.Data.DataTable Implements IGoldQualityController.GetAllGoldQualityForWhiteGold
            Return _objGoldQualityDA.GetAllGoldQualityForWhiteGold()
        End Function

        Public Function GetAllGoldQualityDataForGoldPrice() As System.Data.DataTable Implements IGoldQualityController.GetAllGoldQualityDataForGoldPrice
            Return _objGoldQualityDA.GetAllGoldQualityDataForGoldPrice()
        End Function

        Public Function GetAllGoldQualityByItemCategory(ByVal ItemCategory As String) As CommonInfo.GoldQualityInfo Implements IGoldQualityController.GetAllGoldQualityByItemCategory
            Return _objGoldQualityDA.GetAllGoldQualityByItemCategory(ItemCategory)
        End Function
    End Class

End Namespace
