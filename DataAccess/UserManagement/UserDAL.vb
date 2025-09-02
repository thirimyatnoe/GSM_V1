Option Explicit On 
Imports System.Configuration
Imports System.data.SqlClient
Imports Operational.Cryptography
Imports CommonInfo
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Namespace UserManagement


    Public Class UserDAL
        Private Db As Database
        Dim ArrayParam() As SqlClient.SqlParameter

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
        End Sub


        Public Function SaveUser(ByVal objUserInfo As UserInfo) As Boolean
            If IsDuplicateUser(objUserInfo.SysID, objUserInfo.UserID) Then
                Return False
            End If
            Using objCrypto As New DACrypto()
                Try
                    If IsDuplicateUser(objUserInfo.SysID, objUserInfo.UserID) Then

                    End If
                    objUserInfo.UserPassword = objCrypto.Encrypt(objUserInfo.UserPassword)
                Catch SecurityExceptionErr As Security.SecurityException
                    Return False
                End Try
            End Using
            Dim strsql As String = ""
            If objUserInfo.SysID = 0 Then
                strsql = " Insert Into [tb_GE_SystemUser] (UserID, UserName, [Password], UserLevelID, Remark)  Values (@UserID, @UserName, @Password, @UserLevelID, @Remark)  "
                Dim DBComm As DbCommand = Db.GetSqlStringCommand(strsql)
                Db.AddInParameter(DBComm, "@UserID", DbType.String, objUserInfo.UserID)
                Db.AddInParameter(DBComm, "@UserName", DbType.String, objUserInfo.UserName)
                Db.AddInParameter(DBComm, "@Password", DbType.String, objUserInfo.UserPassword)
                Db.AddInParameter(DBComm, "@UserLevelID", DbType.String, objUserInfo.UserLevelID)
                Db.AddInParameter(DBComm, "@Remark", DbType.String, objUserInfo.Remark)
                Db.ExecuteNonQuery(DBComm)
            Else
                strsql = " Update [tb_GE_SystemUser] Set UserID = @UserID, UserName = @UserName, [Password] = @Password, UserLevelID = @UserLevelID, Remark = @Remark Where SysID = @SysID  "
                Dim DBComm As DbCommand = Db.GetSqlStringCommand(strsql)
                Db.AddInParameter(DBComm, "@UserID", DbType.String, objUserInfo.UserID)
                Db.AddInParameter(DBComm, "@UserName", DbType.String, objUserInfo.UserName)
                Db.AddInParameter(DBComm, "@Password", DbType.String, objUserInfo.UserPassword)
                Db.AddInParameter(DBComm, "@UserLevelID", DbType.String, objUserInfo.UserLevelID)
                Db.AddInParameter(DBComm, "@Remark", DbType.String, objUserInfo.Remark)
                Db.AddInParameter(DBComm, "@SysID", DbType.Int32, objUserInfo.SysID)
                Db.ExecuteNonQuery(DBComm)
            End If
            Return True
        End Function

        Private Function IsDuplicateUser(ByVal SysID As Integer, ByVal UserID As String) As Boolean
            Dim dbcommSave As DbCommand
            Dim strsql As String = ""
            strsql = "Select COUNT(1) From tb_GE_SystemUser Where SysID <> @SysID And UserID = @UserID"
            dbcommSave = Db.GetSqlStringCommand(strsql)
            Db.AddInParameter(dbcommSave, "@SysID", DbType.Int32, SysID)
            Db.AddInParameter(dbcommSave, "@UserID", DbType.String, UserID)
            If CInt(Db.ExecuteScalar(dbcommSave)) > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function DeleteUser(ByVal argSysID As Integer) As Boolean
            Dim strCommandText As String = ""
            strCommandText = "DELETE FROM  [tb_GE_SystemUser] WHERE SysID=" & argSysID
            'strCommandText = "DELETE FROM  [tb_GE_SystemUser] WHERE UserLevelID=" & argSysID
            Dim DBComm As DbCommand = Db.GetSqlStringCommand(strCommandText)
            'Return CType(Db.ExecuteScalar(DBComm), Boolean)
            Return CBool(Db.ExecuteNonQuery(DBComm))
        End Function

        'sp_GE_User_Get
        Public Function GetUser(ByVal UserInfo As UserInfo) As DataTable
            Dim dt As New DataTable
            Dim dr As DataRow
            Dim Cri As String = vbNullString
            Dim strsql As String
            If UserInfo.SysID > 0 Then
                strsql = "SELECT * FROM [tb_GE_SystemUser]  WHERE 1=1 And SysID= " & UserInfo.SysID
            Else
                strsql = "SELECT * FROM [tb_GE_SystemUser]  WHERE 1=1 "
            End If
            Dim DBComm As DbCommand = Db.GetSqlStringCommand(strsql)
            dt.Load(Db.ExecuteReader(DBComm))

            If dt.Rows.Count > 0 Then
                Using objCrypto As New DACrypto()
                    Try
                        For Each dr In dt.Rows
                            dr("Password") = objCrypto.Decrypt(dr("Password"))
                        Next
                    Catch SecurityExceptionErr As Security.SecurityException
                        MsgBox("Error Getting User Info")
                    End Try
                End Using
            End If
            Return dt
        End Function

        Public Function GetUserLevel(ByVal UserInfo As UserInfo) As DataTable
            Dim dt As New DataTable
            Dim strsql As String

            If UserInfo.SysID > 0 Then
                strsql = "Select * From [tb_GE_UserLevel] Where SysID = " & UserInfo.SysID
            Else
                strsql = "Select * From [tb_GE_UserLevel]  "
            End If
            Dim DBComm As DbCommand = Db.GetSqlStringCommand(strsql)
            dt.Load(Db.ExecuteReader(DBComm))
            Return dt
        End Function

        Public Function GetUserLevelBySysID(ByVal SysID As Integer) As DataTable
            Dim dt As New DataTable
            ReDim ArrayParam(1)
            Dim strsql As String = " SELECT * from tb_GE_UserLevel WHERE SysID= @SysID "
            Dim DBComm As DbCommand = Db.GetSqlStringCommand(strsql)
            Db.AddInParameter(DBComm, "@SysID", DbType.Int32, SysID)
            dt.Load(Db.ExecuteReader(DBComm))
            Return dt
        End Function

        Public Function GetUserByID(ByVal argID As String) As DataTable
            Dim dt As New DataTable
            Dim DBComm As DbCommand = Db.GetSqlStringCommand("Select * From  [tb_GE_SystemUser]  Where UserID ='" & argID & "'")
            dt.Load(Db.ExecuteReader(DBComm))
            If dt.Rows.Count > 0 Then
                Using objCrypto As New DACrypto()
                    Try
                        dt.Rows(0)("Password") = objCrypto.Decrypt(dt.Rows(0)("Password"))
                    Catch SecurityExceptionErr As Security.SecurityException
                        MsgBox("Error Getting User Info")
                    End Try
                End Using
            End If
            Return dt
        End Function

        Public Function GetUserBrowser() As DataTable
            Dim dt As New DataTable
            Dim strSql As String = ""
            Dim dr As DataRow

            strSql += " SELECT SU.SysID AS [@SYSID], UserID As [User Code], UserName As [User Name],[Password] AS [@Password],"
            strSql += " UserLevelID As [@UserLevelID],UserLevel,SU.Remark "
            strSql += " FROM [tb_GE_SystemUser] SU left JOIN tb_GE_UserLevel UL "
            strSql += " ON SU.UserLevelID = UL.SysID"
            Dim DBComm As DbCommand = Db.GetSqlStringCommand(strSql)
            dt.Load(Db.ExecuteReader(DBComm))

            If dt.Rows.Count > 0 Then
                Using objCrypto As New DACrypto()
                    Try
                        For Each dr In dt.Rows
                            dr("@Password") = objCrypto.Decrypt(dr("@Password"))
                        Next
                    Catch SecurityExceptionErr As Security.SecurityException
                        MsgBox("Error Getting User Info")
                    End Try
                End Using
            End If

            Return dt
        End Function
    End Class

End Namespace