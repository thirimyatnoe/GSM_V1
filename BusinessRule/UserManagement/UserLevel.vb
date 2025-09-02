Imports DataAccess.UserManagement
Imports CommonInfo

Namespace UserManagement
    Public Class UserLevel
        Dim objUserLevelDAL As New UserLevelDAL
        Dim objGeneral As General.IGeneralController = Factory.Instance.CreateGeneralController

        Public Function SaveUserLevel(ByVal UserLevelInfo As UserLevelInfo) As Boolean
            If objUserLevelDAL.IsDuplicateUserLevelName(UserLevelInfo.SysID, UserLevelInfo.UserLevel) Then
                Return False
            End If

            If UserLevelInfo.SysID = 0 Then
                UserLevelInfo.SysID = objGeneral.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.UserLevel, "UserLevelID", Now)
                objUserLevelDAL.AddUserLevel(UserLevelInfo)

            Else
                objUserLevelDAL.UpdateUserLevel(UserLevelInfo)
            End If
            Return True
        End Function
        Public Function GetUserLevel(Optional ByVal UserLevelID As Integer = 0) As DataTable
            Dim dt As New DataTable
            dt = objUserLevelDAL.GetUserLevel(UserLevelID)
            Return dt
        End Function
    End Class
End Namespace
