Option Explicit On
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports CommonInfo

Namespace UserManagement

    Public Class UserLevelDAL
        Private Db As Database
        Dim ArrayParam() As SqlClient.SqlParameter

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
        End Sub

        Public Sub AddUserLevel(ByVal UserLevelInfo As UserLevelInfo)
            Dim strColName As String
            Dim dbcommSave As DbCommand
            Dim dbcommAlterTable As DbCommand
            Dim strsql As String = ""

            strsql = " INSERT INTO tb_GE_UserLevel (SysID, UserLevel, [Description], Remark)  VALUES (@SysID, @UserLevel,  @Description ,  @Remark)  "
            dbcommSave = Db.GetSqlStringCommand(strsql)
            Db.AddInParameter(dbcommSave, "@SysID", DbType.Int32, UserLevelInfo.SysID)
            Db.AddInParameter(dbcommSave, "@UserLevel", DbType.String, UserLevelInfo.UserLevel)
            Db.AddInParameter(dbcommSave, "@Description", DbType.String, UserLevelInfo.Description)
            Db.AddInParameter(dbcommSave, "@Remark", DbType.String, UserLevelInfo.Remark)
            Db.ExecuteNonQuery(dbcommSave)

            strColName = "[_" & UserLevelInfo.SysID & "]"
            dbcommAlterTable = Db.GetSqlStringCommand("ALTER TABLE tb_GE_UserMenu ADD " & strColName & " Bit NULL")
            Try

           
                Db.ExecuteNonQuery(dbcommAlterTable)
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
        End Sub

        Public Sub UpdateUserLevel(ByVal UserLevelInfo As UserLevelInfo)
            Dim dbcommSave As DbCommand
            Dim strsql As String = ""

            strsql = "UPDATE tb_GE_UserLevel SET UserLevel = @UserLevel, [Description] = @Description, Remark = @Remark WHERE SysID = @SysID "
            dbcommSave = Db.GetSqlStringCommand(strsql)
            Db.AddInParameter(dbcommSave, "@UserLevel", DbType.String, UserLevelInfo.UserLevel)
            Db.AddInParameter(dbcommSave, "@Description", DbType.String, UserLevelInfo.Description)
            Db.AddInParameter(dbcommSave, "@Remark", DbType.String, UserLevelInfo.Remark)
            Db.AddInParameter(dbcommSave, "@SysID", DbType.Int32, UserLevelInfo.SysID)
            Db.ExecuteNonQuery(dbcommSave)

        End Sub

        Public Function IsDuplicateUserLevelName(ByVal UserLevelID As Integer, ByVal UserLevel As String) As Boolean
            Dim dbcommSave As DbCommand
            Dim strsql As String = ""

            strsql = "SELECT COUNT(1) FROM tb_GE_UserLevel WHERE SysID <> @UserLevelID AND UserLevel = @UserLevel"
            dbcommSave = Db.GetSqlStringCommand(strsql)
            Db.AddInParameter(dbcommSave, "@UserLevelID", DbType.Int32, UserLevelID)
            Db.AddInParameter(dbcommSave, "@UserLevel", DbType.String, UserLevel)
            If CInt(Db.ExecuteScalar(dbcommSave)) > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetUserLevel(Optional ByVal SysID As Integer = 0) As DataTable  'SysID = 0 to retrieve all UserLevels
            Dim dt As New DataTable
            Dim strsql As String
            If SysID > 0 Then
                strsql = "Select * From [tb_GE_UserLevel] Where SysID = " & SysID
            Else
                strsql = "Select * From [tb_GE_UserLevel]  "
            End If
            Dim DBComm As DbCommand = Db.GetSqlStringCommand(strsql)
            dt.Load(Db.ExecuteReader(DBComm))
            Return dt
        End Function
    End Class

End Namespace