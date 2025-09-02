Imports CommonInfo
Imports DataAccess.UserManagement

Namespace UserManagement

    Public Class User
        Dim objUserDAL As New UserDAL

        Public Function SaveUser(ByVal UserInfo As UserInfo) As Boolean
            Return objUserDAL.SaveUser(UserInfo)
        End Function

        Public Function DeleteUser(Optional ByVal SysID As Integer = 0) As Boolean
            Return objUserDAL.DeleteUser(SysID)
        End Function

        Public Function GetUser(ByVal UserInfo As UserInfo, Optional ByVal IsNone As Boolean = False, Optional ByVal IsAll As Boolean = False, Optional ByVal IsMore As Boolean = False) As DataTable
            Dim dt As New DataTable
            Dim dr As DataRow
            dt = objUserDAL.GetUser(UserInfo)

            If IsNone = True Then
                dr = dt.NewRow
                dr(0) = UserInfo.UserID
                dr(1) = -1
                dr(2) = "None"
                dt.Rows.Add(dr)
            End If

            If IsNone = True Then
                dr = dt.NewRow
                dr(0) = UserInfo.UserID
                dr(1) = 0
                dr(2) = "All"
                dt.Rows.Add(dr)
            End If

            Return dt
        End Function

        Public Function GetUserLevel(ByVal UserInfo As UserInfo, Optional ByVal IsNone As Boolean = False, Optional ByVal IsAll As Boolean = False, Optional ByVal IsMore As Boolean = False) As DataTable
            Dim dt As New DataTable
            Dim dr As DataRow
            dt = objUserDAL.GetUserLevel(UserInfo)

            If IsNone = True Then
                dr = dt.NewRow
                dr(0) = UserInfo.UserID
                dr(1) = -1
                dr(2) = "None"
                dt.Rows.Add(dr)
            End If

            If IsNone = True Then
                dr = dt.NewRow
                dr(0) = UserInfo.UserID
                dr(1) = 0
                dr(2) = "All"
                dt.Rows.Add(dr)
            End If

            Return dt
        End Function

        Public Function GetUserLevelBySysID(ByVal argSysID As Integer) As DataTable
            Return objUserDAL.GetUserLevelBySysID(argSysID)
        End Function

        Public Function GetUserByID(ByVal argID As String) As DataTable
            Return objUserDAL.GetUserByID(argID)
        End Function

        Public Function GetUserBrowser() As DataTable
            Return objUserDAL.GetUserBrowser()
        End Function
    End Class

End Namespace