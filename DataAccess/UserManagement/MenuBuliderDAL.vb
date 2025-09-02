Option Explicit On
Imports System.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common

Namespace UserManagement

    Public Class MenuBuliderDAL
        Private DB As Database


        Public Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

        'sp_GE_UserLevelMenu_Check

        Public Function UserMenuLevelCheck(ByVal argMenuID As String, ByVal argMenuName As String, ByVal argUserLevel As String, ByVal argUserLevelID As Integer) As Boolean
            Dim bolRet As Boolean
            Dim DBComm As DbCommand
            Dim strCommandText As String
            strCommandText = " SELECT COUNT(1) FROM tb_GE_UserMenu WHERE MenuID = @MenuID And MenuName = @MenuName"
            DBComm = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@MenuID", DbType.String, argMenuID)
            DB.AddInParameter(DBComm, "@MenuName", DbType.String, argMenuName)
            If CInt(DB.ExecuteScalar(DBComm)) = 0 Then
                DBComm.CommandText = "INSERT INTO tb_GE_UserMenu (MenuID, MenuName) VALUES (@MenuID, @MenuName) "
                DB.ExecuteNonQuery(DBComm)
            End If
            If argUserLevel = "Administrator" Then
                bolRet = True
            Else
                DBComm.CommandText = " SELECT [_" & argUserLevelID & "] FROM tb_GE_UserMenu WHERE MenuID = @MenuID And MenuName = @MenuName  "




                bolRet = CType(IIf(IsDBNull(DB.ExecuteScalar(DBComm)), 0, DB.ExecuteScalar(DBComm)), Boolean)
            End If
            Return bolRet
        End Function


        Public Function UserLevelMenuNameCheck(ByVal MenuName As String, ByVal UserLevel As String, ByVal UserLevelID As Integer) As Boolean
            Dim DBComm As DbCommand
            Dim strCommandText As String
            If UserLevel = "Administrator" Then
                Return True
            End If
            strCommandText = " SELECT COUNT(1) FROM tb_GE_UserMenu WHERE MenuName = @MenuName AND [_" & UserLevelID & "] = 1"
            DBComm = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@MenuName", DbType.String, MenuName)
            If CInt(DB.ExecuteScalar(DBComm)) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function


        Public Function GetMenuUserLevel(ByVal UserLevelID As Integer, ByVal UserLevel As String) As DataTable   ' argUserLevel="" For all userlevel return
            Dim dt As New DataTable
            Dim strCommandText As String = ""
            If UserLevel = "Administrator" Then
                strCommandText = " Select MenuID, MenuName, 1 As [_" & UserLevelID & "] From [tb_GE_UserMenu] Order By MenuID  "
            Else
                strCommandText = " Select MenuID, MenuName, [_" & UserLevelID & "]  From [tb_GE_UserMenu] Order By MenuID"
            End If

            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            Return DB.ExecuteDataSet(DBComm).Tables(0)
        End Function

        Public Function UpdateMenuUserLevel(ByVal UserLevelID As Integer, ByVal MenuID As String, ByVal State As Boolean) As Boolean
            Dim bolRet As Boolean
            Dim strCommandText As String = ""
            strCommandText = "UPDATE tb_GE_UserMenu SET [_" & UserLevelID & "] = @State WHERE MenuID = @MenuID"
            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@State", DbType.Boolean, State)
            DB.AddInParameter(DBComm, "@MenuID", DbType.String, MenuID)

            bolRet = DB.ExecuteScalar(DBComm)
            Return bolRet
        End Function

        '--------------------------------------
        'Edited by Nay Chi Zaw ( 18 Feb 2008 )
        Public Function SaveMenuUserEmployee(ByVal UserLevelID As Integer, ByVal EmployeeID As String) As Boolean
            Dim strCommandText As String = ""
            strCommandText = "INSERT INTO tb_GE_UserEmployee VALUES (@UserLevelID, @EmployeeID) "
            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@UserLevelID", DbType.Int32, UserLevelID)
            DB.AddInParameter(DBComm, "@EmployeeID", DbType.Int32, EmployeeID)

            DB.ExecuteNonQuery(DBComm)
            Return True
        End Function

        Public Function DeleteMenuUserEmployee(ByVal UserLevelID As Integer) As Boolean
            Dim strCommandText As String = ""

            strCommandText = "Delete From tb_GE_UserEmployee Where UserLevelID = @UserLevelID "
            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@UserLevelID", DbType.Int32, UserLevelID)
            DB.ExecuteNonQuery(DBComm)
            Return True

        End Function

        Public Function GetEmployeebyLocation(ByVal Cri As String) As DataTable
            Dim DT As New DataTable
            Dim strCommandText As String = "SELECT Distinct EmployeeID, EmployeeName,EmployeeCode  FROM tb_GE_Employee WHERE DOR IS NULL " & Cri & " Order By EmployeeID "
            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            Return DB.ExecuteDataSet(DBComm).Tables(0)
        End Function

        Public Function GetEmployeebyUserLevel(ByVal UserLevelID As String, Optional ByVal UserLevel As String = vbNullString) As DataTable
            Dim DT As New DataTable
            Dim DBComm As DbCommand
            Dim strCommandText As String
            If UserLevel = "Administrator" Then
                strCommandText = "SELECT EmployeeID FROM tb_GE_Employee WHERE DOR IS NULL"
                DBComm = DB.GetSqlStringCommand(strCommandText)
            Else
                strCommandText = "SELECT EmployeeID FROM tb_GE_UserEmployee WHERE UserLevelID = @UserLevelID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@UserLevelID", DbType.Int32, UserLevelID)
            End If
            Try

                Return DB.ExecuteDataSet(DBComm).Tables(0)
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function
        '--------------------------------------

        Public Function DeleteUserLevel(ByVal UserLevelID As Integer, ByVal ForceDeleteUser As Boolean) As Boolean
            Dim strUserLevel As String
            Dim DBComm As DbCommand
            Dim strCommandText As String = ""
            Try
                strCommandText = "SELECT UserLevel From tb_GE_UserLevel Where SysID = @UserLevelID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@UserLevelID", DbType.Int32, UserLevelID)
                strUserLevel = DB.ExecuteScalar(DBComm)
                If ForceDeleteUser Then
                    DBComm.CommandText = "Delete From [tb_GE_SystemUser] Where UserLevelID = @UserLevelID"
                    DB.ExecuteNonQuery(DBComm)
                End If
                DBComm.CommandText = "Delete From tb_GE_UserLevel Where SysID = @UserLevelID"
                DB.ExecuteNonQuery(DBComm)

                DBComm.CommandText = "ALTER TABLE tb_GE_UserMenu DROP COLUMN [_" & UserLevelID & "]"
                DB.ExecuteNonQuery(DBComm)

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Sub DeleteUnuseMenu(ByVal MenuIDList As String)
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            strCommandText = " DELETE FROM tb_GE_UserMenu WHERE MenuID + '##' + MenuName  NOT IN (" & MenuIDList.Trim(",") & ")"
            DBComm = DB.GetSqlStringCommand(strCommandText)
            DB.ExecuteNonQuery(DBComm)
        End Sub

        Public Function GetUserLevelID(ByVal UserLevel As String) As Integer
            Dim UserLevelID As Integer
            Dim strCommandText As String = ""
            strCommandText = "SELECT SysID FROM tb_GE_UserLevel WHERE UserLevel = @UserLevel"
            Try
                Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@UserLevel", DbType.String, UserLevel)
                UserLevelID = DB.ExecuteScalar(DBComm)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        '--------------
        Public Function GetMenuUserLevelForRibbon(ByVal UserLevelID As Integer, ByVal UserLevel As String) As DataTable   ' argUserLevel="" For all userlevel return
            Dim dt As New DataTable
            Dim strCommandText As String = ""
            If UserLevel = "Administrator" Then
                strCommandText = " Select MenuID, MenuName, 1 As [_" & UserLevelID & "] From [tb_GE_UserMenu] Order By MenuID  "
            Else
                ' strCommandText = " Select MenuID, MenuName, [_" & UserLevelID & "]  From [tb_GE_UserMenu] Order By MenuID"
                strCommandText = " Select MenuID, MenuName, [_" & UserLevelID & "]  From [tb_GE_UserMenu] where [_" & UserLevelID & "] <> 0 Order By MenuID"
            End If

            Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
            Return DB.ExecuteDataSet(DBComm).Tables(0)
        End Function
    End Class
End Namespace