Imports DataAccess.GoldSmith
Imports CommonInfo
Namespace GoldSmith
    Public Class GoldSmithController
        Implements IGoldSmithController


#Region "Private Members"

        Private _objGoldSmithDA As IGoldSmithDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IGoldSmithController = New GoldSmithController

#End Region

#Region "Constructors"

        Private Sub New()
            _objGoldSmithDA = DataAccess.Factory.Instance.CreateGoldSmithDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGoldSmithController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteGoldSmith(GoldSmithID As String) As Boolean Implements IGoldSmithController.DeleteGoldSmith
            If _objGoldSmithDA.DeleteGoldSmith(GoldSmithID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.GoldSmith.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                          GoldSmithID, _
                                          "Delete GoldSmith")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllGoldSmith() As DataTable Implements IGoldSmithController.GetAllGoldSmith
            Return _objGoldSmithDA.GetAllGoldSmith()
        End Function

        Public Function GetGoldSmith() As DataTable Implements IGoldSmithController.GetGoldSmith

        End Function

        Public Function GetGoldSmithByID(GoldSmithID As String) As GoldSmithInfo Implements IGoldSmithController.GetGoldSmithByID
            Return _objGoldSmithDA.GetGoldSmithByID(GoldSmithID)
        End Function
        Public Function GetAllGoldSmithList() As System.Data.DataTable Implements IGoldSmithController.GetAllGoldSmithList
            Return _objGoldSmithDA.GetAllGoldSmithList()
        End Function
        Public Function GetAllGoldSmithListByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IGoldSmithController.GetAllGoldSmithListByLocation
            Return _objGoldSmithDA.GetAllGoldSmithListByLocation(LocationID)
        End Function

        Public Function GetDefaultGoldSmith() As System.Data.DataTable Implements IGoldSmithController.GetDefaultGoldSmith
            Return _objGoldSmithDA.GetDefaultGoldSmith()
        End Function
        Public Function GetGoldSmithNameListByGoldSmithID(ByVal GoldSmithID As String) As System.Data.DataTable Implements IGoldSmithController.GetGoldSmithNameListByGoldSmithID
            Return _objGoldSmithDA.GetGoldSmithNameListByGoldSmithID(GoldSmithID)
        End Function
        Public Function GetGoldSmithNameListByStock(ByVal GoldSmithID As String) As System.Data.DataTable Implements IGoldSmithController.GetGoldSmithNameListByStock
            Return _objGoldSmithDA.GetGoldSmithNameListByStock(GoldSmithID)
        End Function

        Public Function GetGoldSmithCode(_GoldSmithCode As String) As GoldSmithInfo Implements IGoldSmithController.GetGoldSmithCode
            Return _objGoldSmithDA.GetGoldSmithCode(_GoldSmithCode)
        End Function

        Public Function SaveGoldSmith(GoldSmithObj As GoldSmithInfo) As String Implements IGoldSmithController.SaveGoldSmith
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False
            If GoldSmithObj.GoldSmithID = "" Then
                GoldSmithObj.GoldSmithID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.GoldSmith, CommonInfo.EnumSetting.GenerateKeyType.GoldSmith.ToString, Now)
                GoldSmithObj.GoldSmithCode = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.GoldSmithCode, EnumSetting.GenerateKeyType.GoldSmithCode, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.GoldSmith.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                          GoldSmithObj.GoldSmithID, _
                                          "Insert GoldSmith")
                bolRet = _objGoldSmithDA.InsertGoldSmith(GoldSmithObj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.GoldSmith.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                          GoldSmithObj.GoldSmithID, _
                                          "Update GoldSmith")
                bolRet = _objGoldSmithDA.UpdateGoldSmith(GoldSmithObj)
            End If
            If bolRet = True Then
                Return GoldSmithObj.GoldSmithID
            Else
                Return ""
            End If
        End Function
    End Class
End Namespace

