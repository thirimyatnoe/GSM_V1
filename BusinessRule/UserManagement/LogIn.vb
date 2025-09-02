Imports DataAccess.UserManagement

Namespace UserManagement

    Public Class LogIn

        Dim objLogInDAL As New LogInDAL

        Public Function CheckExpire(ByVal expDate As Nullable(Of Date)) As Boolean
            Return objLogInDAL.CheckExpire(expDate)
        End Function

        Public Function GetUserInfo() As DataTable
            Return objLogInDAL.GetUserInfo()
        End Function

        'Public Function GetAllUserLevel(ByVal UserLevel As String) As DataTable
        '    Return objLogInDAL.GetAllUserLevel(UserLevel)
        'End Function

        Public Function GetUserLevel(ByVal SysID As Integer) As DataTable
            Return objLogInDAL.GetUserLevel(SysID)

            'Dim dt As New DataTable
            'dt.Load(SqlHelper.ExecuteReader(MyBase.Connection, "sp_GE_UserLevelBySysID_Get", SysID))
            'Return dt
        End Function

        Public Function ValidateUserLogin(ByVal UserID As String, ByVal PWD As String) As Integer  '' Validate and return user level of this user
            Return objLogInDAL.ValidateUserLogin(UserID, PWD)
        End Function
        Public Function ValidateUserForPrintSecurity(ByVal PWD As String) As String  '' Validate  userid for print
            Return objLogInDAL.ValidateUserForPrintSecurity(PWD)
        End Function
    End Class

End Namespace