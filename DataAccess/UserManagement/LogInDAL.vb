Imports System.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Operational.Cryptography

Namespace UserManagement

    Public Class LogInDAL

        Private DB As Database

        Public Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

        Public Function CheckExpire(ByVal expDate As Nullable(Of Date)) As Boolean
            If expDate.HasValue Then
                Dim strSQLEventLog As String = "SELECT Count(1) From tb_GE_EventLogs WHERE LogDateTime > '" & expDate.Value.ToString("yyyy/MM/dd") & "'"


                Dim DBComm As DbCommand

                DBComm = DB.GetSqlStringCommand(strSQLEventLog)
                If CInt(DB.ExecuteScalar(DBComm)) > 0 Then Return True
                Return False
            Else
                Return False
            End If

        End Function

        Public Function ValidateUserLogin(ByVal UserID As String, ByVal PWD As String) As Integer  '' Validate and return user level ID of this user
            Dim objRet As Object
            Dim strCommandText As String = "SELECT UserLevelID FROM tb_GE_SystemUser WHERE UserID = @UserID And Password = @PWD "

            Using objCrypto As New DACrypto()
                Try
                    PWD = objCrypto.Encrypt(PWD)
                Catch SecurityExceptionErr As Security.SecurityException
                    Return ""
                End Try
            End Using

            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@UserID", DbType.String, UserID)
            DB.AddInParameter(DBComm, "@PWD", DbType.String, PWD)

            objRet = DB.ExecuteScalar(DBComm)

            Return IIf(objRet Is Nothing, 0, objRet)
        End Function

        Public Function ValidateUserForPrintSecurity(ByVal PWD As String) As String
            Dim objRet As Object
            Dim strCommandText As String = "SELECT UserID FROM tb_GE_SystemUser WHERE  Password = @PWD "

            Using objCrypto As New DACrypto()
                Try
                    PWD = objCrypto.Encrypt(PWD)
                Catch SecurityExceptionErr As Security.SecurityException
                    Return ""
                End Try
            End Using

            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@PWD", DbType.String, PWD)

            objRet = DB.ExecuteScalar(DBComm)

            Return IIf(objRet Is Nothing, "", objRet)
        End Function

        Public Function GetUserInfo() As DataTable
            Dim DBComm As DbCommand = DB.GetSqlStringCommand("Select * from tb_GE_SystemUser Order by UserLevelID asc")
            Return DB.ExecuteDataSet(DBComm).Tables(0)

        End Function


        Public Function GetUserLevel(ByVal SysID As Integer) As DataTable
            Dim strCommandText As String = " SELECT * from tb_GE_UserLevel WHERE SysID= @SysID "
            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@SysID", DbType.Int32, SysID)
            Return DB.ExecuteDataSet(DBComm).Tables(0)
        End Function

    End Class
End Namespace