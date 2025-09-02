
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System
Imports System.Windows.Forms
Imports System.Text
Imports Operational.Zip

'Imports ADOX

Namespace DBConnection

    Public Class DBConnectionDAL

#Region "MS SQL"

        'Public Function ConnectServer(ByVal argConnectionString As String) As SQLServer2
        '    Dim pServer As String
        '    Dim pUserName As String
        '    Dim pPassword As String
        '    Dim pDataBase As String
        '    Dim pIntegratedSecurity As Boolean

        '    pServer = SplitConnectionString(argConnectionString, "Data Source")
        '    pUserName = SplitConnectionString(argConnectionString, "User ID")
        '    pPassword = SplitConnectionString(argConnectionString, "Password")
        '    pDataBase = SplitConnectionString(argConnectionString, "Initial Catalog")
        '    pIntegratedSecurity = IIf(SplitConnectionString(argConnectionString, "Integrated Security").Trim = "", False, True)

        '    Dim msvr As SQLServer2 = New SQLServer2

        '    Try
        '        msvr.EnableBcp = True
        '        If pIntegratedSecurity Then
        '            msvr.LoginSecure = True
        '            msvr.Connect(pServer)
        '        Else
        '            msvr.Connect(pServer, pUserName, pPassword)
        '        End If

        '    Catch ex As Exception
        '        msvr = Nothing
        '    End Try
        '    Return msvr
        'End Function

        'Public Function BackUpDB(ByVal argConnection As String, ByVal argTempDir As String, ByVal argBackUpFileName As String, ByVal pb As ProgressBar, ByVal frm As Form) As String
        '    Dim db As Database2
        '    Dim svr As SQLServer2
        '    Dim pbstep As Integer
        '    pb.Maximum = 1000
        '    pb.Value = 0

        '    svr = ConnectServer(argConnection)
        '    db = svr.Databases.Item(SplitConnectionString(argConnection, "Initial Catalog"))

        '    pb.Increment(100)
        '    frm.Refresh()

        '    Dim objBCP As New SQLDMO.BulkCopy2
        '    objBCP.IncludeIdentityValues = True
        '    objBCP.UseExistingConnection = True
        '    objBCP.DataFileType = SQLDMO_DATAFILE_TYPE.SQLDMODataFile_NativeFormat

        '    Dim sqlTable As Table2

        '    Try

        '        If My.Computer.FileSystem.DirectoryExists(argTempDir) Then
        '            My.Computer.FileSystem.DeleteDirectory(argTempDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
        '        End If

        '        'Create temporary directory.
        '        If Not My.Computer.FileSystem.DirectoryExists(argTempDir) Then
        '            My.Computer.FileSystem.CreateDirectory(argTempDir)
        '        End If

        '        pb.Increment(100)
        '        frm.Refresh()

        '        pbstep = 700 / db.Tables.Count

        '        For Each sqlTable In db.Tables

        '            If sqlTable.Name.StartsWith("tb_") Then

        '                objBCP.DataFilePath = argTempDir + sqlTable.Name + ".dat"
        '                sqlTable.ExportData(objBCP)

        '                If My.Computer.FileSystem.GetFileInfo(objBCP.DataFilePath).Length <= 0 Then
        '                    My.Computer.FileSystem.DeleteFile(objBCP.DataFilePath)
        '                End If
        '                pb.Increment(pbstep)
        '                frm.Refresh()
        '            End If

        '        Next

        '        'Since script for all objects and data are now saved to temporary directory, zip this temporary folder to a file with extension *.SQLBACKUP.
        '        Zip.ZipIT(argTempDir, argBackUpFileName)
        '        pb.Increment(100)
        '        frm.Refresh()

        '        My.Computer.FileSystem.DeleteDirectory(argTempDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
        '        Return "Done"
        '    Catch ex As Exception
        '        Return ex.Message
        '    Finally
        '        If Not svr Is Nothing Then
        '            svr.DisConnect()
        '            svr = Nothing
        '        End If

        '    End Try

        'End Function

        'Public Function RestoreDB(ByVal argConnection As String, ByVal argTempDir As String, ByVal argBackUpFileName As String, ByVal pb As ProgressBar, ByVal frm As Form) As String
        '    Dim db As Database2
        '    Dim svr As New SQLServer2
        '    Dim pbstep As Integer
        '    pb.Maximum = 1000
        '    pb.Value = 0

        '    Try
        '        If argBackUpFileName.Trim = "" Then
        '            Return "Invalid Back Up File!"
        '            Exit Function
        '        End If

        '        If Not My.Computer.FileSystem.FileExists(argBackUpFileName) Then
        '            Return "Specified backup file does not exists."
        '            Exit Function
        '        Else
        '            Zip.UnzipIT(argBackUpFileName, argTempDir)
        '            pb.Increment(100)
        '            frm.Refresh()
        '        End If

        '        svr = ConnectServer(argConnection)
        '        db = svr.Databases.Item(SplitConnectionString(argConnection, "Initial Catalog"))

        '        pb.Increment(100)
        '        frm.Refresh()
        '        Dim objBCP As New SQLDMO.BulkCopy2

        '        objBCP.DataFileType = SQLDMO_DATAFILE_TYPE.SQLDMODataFile_NativeFormat
        '        objBCP.UseExistingConnection = True
        '        objBCP.ServerBCPKeepIdentity = True
        '        objBCP.IncludeIdentityValues = True

        '        Dim sqlTable As Table2

        '        pbstep = 700 / db.Tables.Count

        '        For Each sqlTable In db.Tables

        '            If sqlTable.Name.StartsWith("tb_") Then
        '                objBCP.DataFilePath = argTempDir + sqlTable.Name + ".dat"
        '                If My.Computer.FileSystem.FileExists(objBCP.DataFilePath) Then
        '                    Try
        '                        db.ExecuteImmediate("delete from " + sqlTable.Name)
        '                        'db.Tables.Refresh()
        '                        'sqlTable.ImportData(objBCP)
        '                    Catch ex As Exception

        '                    End Try
        '                End If
        '                pb.Increment(pbstep)
        '                frm.Refresh()
        '            End If
        '        Next

        '        Dim flag As Boolean = True
        '        For Each sqlTable In db.Tables

        '            If sqlTable.Name.StartsWith("tb_") Then
        '                objBCP.DataFilePath = argTempDir + sqlTable.Name + ".dat"
        '                If My.Computer.FileSystem.FileExists(objBCP.DataFilePath) Then
        '                    '  Try
        '                    db.ExecuteImmediate("delete from " + sqlTable.Name)
        '                    db.Tables.Refresh()
        '                    If (sqlTable.Name <> "tb_GE_UserMenu") Then
        '                        sqlTable.ImportData(objBCP)
        '                    End If

        '                    ''update by zmn for user level dynamic column error
        '                    If (sqlTable.Name = "tb_GE_UserMenu") Then
        '                        db.ExecuteImmediate("drop table tb_GE_UserMenu") '+ sqlTable.Name)
        '                        Dim sqlcreate As String
        '                        Dim objUserLevel As New DALGeneral.UserManagement.UserLevelDAL
        '                        Dim dtUserLevel As DataTable
        '                        Dim drUserLevel As DataRow
        '                        dtUserLevel = objUserLevel.GetUserLevel()

        '                        sqlcreate = "create table tb_GE_UserMenu (MenuID nvarchar(50) NOT NULL,	MenuName nvarchar(100) NULL "
        '                        For Each drUserLevel In dtUserLevel.Rows
        '                            If drUserLevel("UserLevel") <> "Administrator" Then
        '                                sqlcreate = sqlcreate & ", [_" & drUserLevel("SysID") & "] [bit] NULL "
        '                            End If
        '                        Next
        '                        sqlcreate = sqlcreate & ") ON [PRIMARY]"
        '                        db.ExecuteImmediate(sqlcreate)
        '                        sqlTable.ImportData(objBCP)

        '                    End If
        '                    '  Catch ex As Exception

        '                    ' End Try
        '                End If
        '                pb.Increment(pbstep)
        '                frm.Refresh()
        '            End If
        '        Next
        '        My.Computer.FileSystem.DeleteDirectory(argTempDir, FileIO.DeleteDirectoryOption.DeleteAllContents)

        '        pb.Increment(100)
        '        frm.Refresh()
        '        Return "Done"

        '    Catch ex As Exception
        '        Return ex.Message
        '    Finally
        '        If Not svr Is Nothing Then
        '            svr.DisConnect()
        '            svr = Nothing
        '        End If

        '    End Try

        'End Function

        Public Function InstallNewDatabase(ByVal PInstallScriptFile As String, ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean, ByVal FilePath As String) As Boolean

            If CreateNewDatabase(PDatabaseName, pServerName, PUserID, PPassword, pTrustedConnection, FilePath) Then
                If Execute_SqlScript(PInstallScriptFile, CreateConnectionString(pServerName, PDatabaseName, PUserID, PPassword, pTrustedConnection)) Then
                    Return True
                End If
            End If

            Return False
        End Function

        Public Function UpdateDatabase(ByVal PInstallScriptFile As String, ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Boolean

            'If CreateNewDatabase(PDatabaseName, pServerName, PUserID, PPassword, pTrustedConnection) Then
            If Execute_SqlScript(PInstallScriptFile, CreateConnectionString(pServerName, PDatabaseName, PUserID, PPassword, pTrustedConnection)) Then
                Return True
            End If
            ' End If

            Return False
        End Function

        Public Function Execute_SqlScript(ByVal scriptFileName As String, ByVal dbConnectionString As String) As Boolean

            Dim sr As System.IO.StreamReader = Nothing
            Dim sb As StringBuilder = Nothing

            Dim line As String = ""
            Dim sqlcmd As SqlCommand
            Dim sqlcn As SqlConnection
            Dim sqlTrans As SqlTransaction

            sqlcn = New SqlConnection(dbConnectionString)
            sqlcn.Open()
            sqlTrans = sqlcn.BeginTransaction
            sqlcmd = sqlcn.CreateCommand

            Try

                sqlcmd.Transaction = sqlTrans
                sqlcmd.CommandType = CommandType.Text
                sr = New System.IO.StreamReader(scriptFileName)

                Do
                    sb = New StringBuilder
                    Do
                        line = sr.ReadLine()

                        If (line = "GO" Or line = "" Or line Is Nothing) Then Exit Do

                        sb.Append(ControlChars.CrLf & line)

                    Loop

                    If (line Is Nothing Or line = "") Then Exit Do

                    sqlcmd.CommandText = sb.ToString

                    sqlcmd.ExecuteNonQuery()

                Loop Until line Is Nothing


                If Not sqlTrans Is Nothing Then
                    sqlTrans.Commit()
                End If

                Execute_SqlScript = True
            Catch ex As Exception
                If Not sqlTrans Is Nothing Then
                    sqlTrans.Rollback()
                End If
                Execute_SqlScript = False
                MsgBox(ex.Message)
            Finally

                If sr IsNot Nothing Then sr.Close()

                Reset()

            End Try

        End Function

        Public Function GetMasterConnectionString(ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As String
            Dim str As String = ""

            str = String.Format("Server={0};", pServerName)

            If (pTrustedConnection) Then
                str = str & "Trusted_Connection=True;"
            Else
                str = str & String.Format("UId={0};PWD={1};", PUserID, PPassword)
            End If

            Return str
        End Function

        'Public Function GetSQLServerList(ByVal IsLocalOnly As Boolean) As DataTable
        '    Dim iIndex As Integer
        '    Dim dtResult As New DataTable
        '    Dim drResult As DataRow
        '    dtResult.Columns.Add("ServerName", System.Type.GetType("System.String"))

        '    Try
        '        Dim oSQLServer As New SQLDMO.SQLServer()
        '        Dim oServerList As SQLDMO.NameList = oSQLServer.Application.ListAvailableSQLServers
        '        For iIndex = 1 To oServerList.Count
        '            If IsLocalOnly Then
        '                If Not (oServerList.Item(iIndex).ToString = Environment.MachineName Or oServerList.Item(iIndex).ToString.StartsWith("(local)") Or oServerList.Item(iIndex).ToString.StartsWith(Environment.MachineName & "\")) Then
        '                    Continue For
        '                End If
        '            End If
        '            drResult = dtResult.NewRow
        '            drResult("ServerName") = oServerList.Item(iIndex).ToString
        '            dtResult.Rows.Add(drResult)
        '        Next
        '    Catch ex As Exception
        '        Throw New Exception(ex.ToString)
        '    End Try

        '    Return dtResult
        'End Function

        Public Function GetDatabase(ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As DataTable

            Dim SqlConn As SqlConnection = New SqlConnection(GetMasterConnectionString(pServerName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            Dim sqlDr As SqlDataReader
            Dim dtDatabase As New DataTable

            sqlcmd.CommandText = "SELECT [Name] FROM master.dbo.sysDatabases ORDER BY [Name]"
            Try
                SqlConn.Open()
                sqlDr = sqlcmd.ExecuteReader()
                dtDatabase.Load(sqlDr)
                sqlDr.Close()

            Catch ex As Exception
                ex.ToString()
            End Try
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If
            Return dtDatabase

        End Function

        Public Function DeleteDatabase(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Boolean
            Dim SqlConn As SqlConnection = New SqlConnection(GetMasterConnectionString(pServerName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()

            sqlcmd.CommandText = "DROP DATABASE " & PDatabaseName
            Try
                SqlConn.Open()
                sqlcmd.ExecuteNonQuery()
                DeleteDatabase = True
            Catch ex As Exception
                DeleteDatabase = False
            End Try

            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If

        End Function


        Public Function CreateNewDatabase(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean, ByVal FilePath As String) As Boolean
            Dim SqlConn As SqlConnection = New SqlConnection(GetMasterConnectionString(pServerName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            If FilePath = "" Then
                sqlcmd.CommandText = "CREATE DATABASE " & PDatabaseName

            Else
                sqlcmd.CommandText = String.Format("CREATE DATABASE {0} ON PRIMARY (NAME={0}, FILENAME='{1}')", PDatabaseName, FilePath & "\globalgold.mdf")
            End If

            'sqlcmd.CommandText = "CREATE DATABASE " & PDatabaseName
            Try
                SqlConn.Open()
                sqlcmd.ExecuteNonQuery()
                CreateNewDatabase = True
            Catch ex As Exception
                CreateNewDatabase = False
            End Try

            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If

        End Function

        Public Function CreateConnectionString(ByVal argServerName As String, ByVal argDatabaseName As String, ByVal argUserID As String, ByVal argPassword As String, ByVal argTrustedConnection As Boolean) As String
            Dim retStr As String

            If argTrustedConnection = True Then
                retStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" & argDatabaseName.Trim & ";Data Source=" & argServerName.Trim & ""
            Else
                retStr = "Persist Security Info=False;User ID=" & argUserID.Trim & ";Password=" & argPassword.Trim & ";Initial Catalog=" & argDatabaseName.Trim & ";Data Source=" & argServerName.Trim & ""
            End If

            Return retStr

        End Function

        Public Function SplitConnectionString(ByVal argConnectionString As String, ByVal argKeyName As String) As String
            Dim retStr As String

            Dim tmpStr() As String
            Dim tmpKeyValue() As String
            Dim i As String

            retStr = ""
            tmpStr = argConnectionString.Split(";")

            For Each i In tmpStr
                tmpKeyValue = i.Split("=")

                If tmpKeyValue.Length < 2 Then
                    'skip
                ElseIf tmpKeyValue(0).Trim = argKeyName.Trim Then
                    retStr = tmpKeyValue(1).Trim
                    Exit For
                End If
            Next

            Return retStr
        End Function

        Public Function IsExistsConfiguration() As Boolean
            Dim IsReturn As Boolean
            Try
                Dim DB As Microsoft.Practices.EnterpriseLibrary.Data.Database
                DB = DatabaseFactory.CreateDatabase
                DB.CreateConnection().Open()
                IsReturn = True
                DB.CreateConnection.Close()
                Return IsReturn
            Catch ex As Exception
                Return False
            End Try

        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="PDatabaseName"></param>
        ''' <param name="pServerName"></param>
        ''' <param name="PUserID"></param>
        ''' <param name="PPassword"></param>
        ''' <param name="pTrustedConnection"></param>
        ''' <returns>0=DataBase Server OK, 1=Server Not Found, 2=Database Not Found</returns>
        ''' <remarks></remarks>
        Public Function CheckDatabase(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Integer
            Dim SqlConn As SqlConnection = New SqlConnection(GetMasterConnectionString(pServerName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            Dim intReturn As Integer
            sqlcmd.CommandText = "SELECT COUNT(1) FROM master.dbo.sysDatabases WHERE name='" & PDatabaseName & "'"
            Try
                SqlConn.Open()
                If CType(sqlcmd.ExecuteScalar(), Integer) > 0 Then
                    intReturn = 0   ' No Error and Database exists
                Else
                    intReturn = 2    ' Database not found
                End If
            Catch ex As Exception
                intReturn = 1    ' Server not found
            End Try

            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
                SqlConn.Dispose()
            End If
            Return intReturn
        End Function

        Public Function IsValidSQLDataStructure(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Boolean
            Dim bolReturn As Boolean
            Dim SqlConn As SqlConnection = New SqlConnection(CreateConnectionString(pServerName, PDatabaseName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            sqlcmd.CommandText = "SELECT COUNT(1) FROM sysobjects WHERE type = 'U'" & _
                                " AND NAME IN ('tb_GE_SystemUser')"

            Try
                SqlConn.Open()
                If CType(sqlcmd.ExecuteScalar(), Integer) = 1 Then  ' Check table count
                    bolReturn = True
                Else
                    bolReturn = False
                End If
            Catch ex As Exception
                bolReturn = False    ' Server not found
            End Try
            If sqlcmd.Connection.State = ConnectionState.Open Then
                sqlcmd.Connection.Close()
            End If

            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If

            sqlcmd.Dispose()
            SqlConn.Dispose()

            Return bolReturn

        End Function

        Public Function IsValidSQLVersion(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Boolean
            Dim bolReturn As Boolean
            Dim SqlConn As SqlConnection = New SqlConnection(CreateConnectionString(pServerName, PDatabaseName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            sqlcmd.CommandText = "SELECT COUNT(1) FROM sysobjects WHERE type = 'U'" & _
                                "AND NAME IN ('tbl_Version')"

            Try
                SqlConn.Open()
                If CType(sqlcmd.ExecuteScalar(), Integer) = 1 Then  ' Check table count
                    bolReturn = True
                Else
                    bolReturn = False
                End If
            Catch ex As Exception
                bolReturn = False    ' Server not found
            End Try
            If sqlcmd.Connection.State = ConnectionState.Open Then
                sqlcmd.Connection.Close()
            End If

            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If

            sqlcmd.Dispose()
            SqlConn.Dispose()

            Return bolReturn

        End Function

        Public Function GetVersion() As System.Data.DataTable

            Dim SqlConn As SqlConnection = New SqlConnection()
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            Dim sqlDr As SqlDataReader
            Dim dtDatabase As New DataTable

            sqlcmd.CommandText = "SELECT Top 1 VersionNo FROM tbl_Version order by VersionNo desc"
            Try
                SqlConn.Open()
                sqlDr = sqlcmd.ExecuteReader()
                dtDatabase.Load(sqlDr)
                sqlDr.Close()

            Catch ex As Exception
                ex.ToString()
            End Try
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If
            Return dtDatabase
        End Function

        Public Function isExistsDatabase(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Boolean
            Dim SqlConn As SqlConnection = New SqlConnection(GetMasterConnectionString(pServerName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            Dim dr As SqlDataReader
            Dim dt As New DataTable

            sqlcmd.CommandText = "SELECT COUNT(1) FROM master.dbo.sysDatabases WHERE name='" & PDatabaseName & "'"
            Try
                SqlConn.Open()

                If CInt(sqlcmd.ExecuteScalar()) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                'Print(ex.ToString())
                ' objEventLog.AddLog(clsEnum.EventState.Error, Date.Now, CurrentUserSysID, "Database Configuration", "", "View", "", "System Error : " & ex.Message.ToString())
            End Try
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If
            Return False
        End Function

        Public Function CheckExitColumnFieldInTable(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Boolean
            Dim SqlConn As SqlConnection = New SqlConnection(CreateConnectionString(pServerName, PDatabaseName, PUserID, PPassword, pTrustedConnection))
            Dim sqlcmd As SqlCommand = SqlConn.CreateCommand()
            Dim sqlDr As SqlDataReader
            Dim dtDatabase As New DataTable

            'sqlcmd.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_PurchaseHeader' AND COLUMN_NAME = 'SaleInvoiceHeaderID') THEN 1 ELSE 0 END"
            sqlcmd.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tbl_ForSale' AND COLUMN_NAME = 'IsDiamond') THEN 0 ELSE 1 END"
            Try
                SqlConn.Open()
                sqlDr = sqlcmd.ExecuteReader()
                dtDatabase.Load(sqlDr)
                sqlDr.Close()
                If CInt(dtDatabase.Rows(0)(0)) = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                ex.ToString()
                Return False
            End Try
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If
        End Function
#End Region

#Region "MS Access"

        Public Function AccessBackup(ByVal sourceFile As String, ByVal destination As String)
            Try
                If Not My.Computer.FileSystem.FileExists(sourceFile) Then
                    Throw New Exception("File Not Found")
                End If

                If My.Computer.FileSystem.FileExists(destination) Then
                    My.Computer.FileSystem.DeleteFile(destination)
                End If
                My.Computer.FileSystem.CopyFile(sourceFile, destination)
                Return "Done"
            Catch ex As Exception
                Return ex
            End Try
        End Function

        Public Function AccessRestore(ByVal sourceFile As String, ByVal destination As String)
            Return AccessBackup(sourceFile, destination)
        End Function

        Public Function CreateAccessConnectionString(ByVal FileName As String, ByVal PPassword As String) As String
            Dim str As String = ""
            If InStr(FileName, ".mdb", CompareMethod.Text) <= 0 Then
                FileName = FileName & ".mdb"
            End If
            str = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", FileName)
            If PPassword <> "" Then
                str = str & String.Format("Jet OLEDB:Database Password={0}", PPassword)
            End If
            Return str
        End Function

        ''' <returns>0=Access Database OK, 1=File Not Found, 2=Incorrect Password, 3=Invalid Database structure</returns>
        Public Function CheckAccessDatabase(ByVal FileName As String, ByVal PPassword As String) As Integer
            Dim OledbConn As OleDbConnection = New OleDbConnection(CreateAccessConnectionString(FileName, PPassword))
            Dim intReturn As Integer
            Dim dtTableList As DataTable
            Dim strRestriction(3) As String
            strRestriction(3) = "Table"
            Try
                OledbConn.Open()
                dtTableList = OledbConn.GetSchema("Tables", strRestriction)
                If dtTableList.Rows.Count > 1 Then   ' Check table count
                    intReturn = 0   ' No Error
                Else
                    intReturn = 3   ' Invalid database structure
                End If

            Catch ex As Exception
                If ex.Message = "Not a valid password." Then
                    intReturn = 2    ' Invalid password
                ElseIf ex.Message.StartsWith("Could not find file") Then
                    intReturn = 1    ' File Not found
                Else
                    intReturn = 3    ' Unexpected Error
                End If
            End Try

            If OledbConn.State = ConnectionState.Open Then
                OledbConn.Close()
            End If

            Return intReturn
        End Function


        Private Function Execute_AccessScript(ByVal scriptFileName As String, ByVal dbConnectionString As String) As Boolean

            Dim sr As System.IO.StreamReader = Nothing
            Dim sb As StringBuilder = Nothing

            Dim line As String = ""
            Dim sqlcmd As OleDbCommand
            Dim sqlcn As OleDbConnection
            Dim sqlTrans As OleDbTransaction

            sqlcn = New OleDbConnection(dbConnectionString)
            sqlcn.Open()
            sqlTrans = sqlcn.BeginTransaction
            sqlcmd = sqlcn.CreateCommand

            Try
                sqlcmd.Transaction = sqlTrans
                sqlcmd.CommandType = CommandType.Text
                sr = New System.IO.StreamReader(scriptFileName)

                Do
                    sb = New StringBuilder
                    Do
                        line = sr.ReadLine()

                        If (line = "GO" Or line Is Nothing) Then Exit Do

                        sb.Append(ControlChars.CrLf & line)

                    Loop

                    If line Is Nothing Then Exit Do

                    sqlcmd.CommandText = sb.ToString

                    sqlcmd.ExecuteNonQuery()

                Loop Until line Is Nothing


                If Not sqlTrans Is Nothing Then
                    sqlTrans.Commit()
                End If

                Execute_AccessScript = True
            Catch ex As Exception
                If Not sqlTrans Is Nothing Then
                    sqlTrans.Rollback()
                End If
                Execute_AccessScript = False
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Data Base Connection Error")
            Finally

                If sr IsNot Nothing Then sr.Close()

                Reset()

            End Try

        End Function

#End Region

    End Class

End Namespace