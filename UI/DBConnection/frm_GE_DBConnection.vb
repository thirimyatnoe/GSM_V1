Imports System
Imports System.Xml
Imports System.Configuration
Imports Operational.Cryptography
Imports Operational.FileDialog
Imports Operational.AppConfiguration
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessRule.DBConnection

Public Class frm_GE_DBConnection

    Private ScriptAccessFileName As String = Application.StartupPath & "\InstallAccessScript.sql"
    Private ScriptSqlFileName As String = Application.StartupPath & "\InstallSqlScript.sql"

    Dim pTrustedConnection As Boolean
    Dim pKey As String = "ConnectionString"
    Dim pValue As String
    Public Connect As Boolean
    Public isMain As Boolean
    'Public isRet As Boolean = False

    Dim objSettings As New DACrypto
    Dim objDBConnection As New DBConnection
    Dim DBPath As String = ""
    Dim constr As String
    Dim dbtype As String

    Dim tabCon As Integer = Nothing
    Dim DBServerName As String = ""
    Dim tmpDBPath As String = ""
#Region "Control Action"

    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click

        ''TYW
        Dim cmd As SqlCommand
        Dim con As SqlConnection
        Dim _FileName As String = ""
        Dim _FilePath As String = ""
        Dim _tmpFile As String = ""
        Dim DBName As String = ""

        Operational.FileDialog.FileDialogue.FileExtension = "bak"
        txtFileName.Text = Operational.FileDialog.FileDialogue.SaveAsFile

        If txtFileName.Text = "" Then
            MessageBox.Show("You have to select Database Path", "Choose path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        cls_DisplayMessage("Starting Backup Process...")
        _FilePath = txtFileName.Text.Substring(0, txtFileName.Text.LastIndexOf("\") + 1)
        _FileName = txtFileName.Text.Substring(txtFileName.Text.LastIndexOf("\") + 1)
        _tmpFile = Format(DateAndTime.Now, "yyyy MM dd hh mm ss").ToString & ".bak"
        con = New SqlConnection(constr)
        DBName = con.Database

        Try

            If con.DataSource = "(local)" Or con.DataSource = "local" Or con.DataSource.ToUpper = Environment.MachineName.ToUpper Then
                cmd = New SqlCommand("backup database " & DBName & " to disk='" & Path.Combine(_FilePath, _FileName) & "'", con)
            Else
                cmd = New SqlCommand("backup database " & DBName & " to disk='" & Path.Combine(DBPath, _tmpFile) & "'", con)
            End If
            con.Open()
            cmd.ExecuteNonQuery()
            If con.DataSource = "(local)" Or con.DataSource = "local" Or con.DataSource.ToUpper = Environment.MachineName.ToUpper Then
            Else
                File.Copy(Path.Combine(DBPath, _tmpFile), Path.Combine(_FilePath, _FileName))
            End If
            cmd.Dispose()
            con.Close()
            cls_DisplayMessage("Finished Backup Process...")
            MessageBox.Show("Database Backup Complete", AppName & " : Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            cls_DisplayMessage(ex.Message)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub cls_DisplayMessage(ByVal Message As String)
        lblStatus.Text = Message
        System.Windows.Forms.Application.DoEvents()
    End Sub
    Private Sub btnRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestore.Click
        Dim dlgResult As DialogResult
        dlgResult = MsgBox("Restoring database will overwrite your existing data." & vbCrLf & "Are you sure to do this action ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Database Restore")

        If dlgResult = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Dim command As String = ""
        Dim command2 As String = ""
        Dim DBName As String = ""
        Dim _FileName As String = ""
        Dim _FilePath As String = ""
        Dim _tmpFile As String = ""
        Dim FilePath As String = ""
        Dim tem As String()
        Dim tem2 As String()
        Dim commandRecordRestore As String = ""
        'Dim RetMsg As String
        'Dim tmpserver() As String
        'Dim tmpDatabase1() As String
        '  Dim AccessDBFileName As String = ""
        ' OpenFileDialog1.FileExtension = "bak"
        Operational.FileDialog.FileDialogue.FileExtension = "bak"
        FilePath = Operational.FileDialog.FileDialogue.OpenFile()

        Try
            If FilePath = "" Then
                MessageBox.Show("You have to choose Path of Database Back File", AppName & " : Choose path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            cls_DisplayMessage("Starting Restore Process...")
            _FilePath = FilePath.Substring(0, FilePath.LastIndexOf("\") + 1)
            _FileName = FilePath.Substring(FilePath.LastIndexOf("\") + 1)
            _tmpFile = Format(DateAndTime.Now, "yyyy MM dd hh mm ss").ToString & ".bak"
            tem = Split(constr, "Initial Catalog=", 2)
            tem2 = Split(tem(1), ";", 2)
            constr = constr.Replace(tem2(0), "master")


            ' test code
            ''Dim tmpserver() As String
            ''Dim tmpDatabase1() As String

            ''tmpserver = Split(constr, "Data Source=", 2)
            ''tmpDatabase1 = Split(tmpserver(1), ";", 2)

            con = New SqlConnection(constr)
            Try
                con.Open()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, AppName)
            End Try

            DBName = tem2(0)
            If File.Exists(DBPath & _FileName) = False Then
                If con.DataSource = "(local)" Or con.DataSource = "local" Or con.DataSource.ToUpper = Environment.MachineName.ToUpper Then
                Else
                    If File.Exists(Path.Combine(DBPath, _FileName)) Then
                        File.Copy(Path.Combine(_FilePath, _FileName), Path.Combine(DBPath, _FileName), True) ' Copy is client
                    Else
                        File.Copy(Path.Combine(_FilePath, _FileName), Path.Combine(DBPath, _FileName)) ' Copy is client
                    End If
                End If
            End If
            DBPath = DBPath & "\" & _FileName

            'ExportEventLogTableToCSVFile()


            ''''''''''''''''''  Add Eventlogs Table  in Master Database''''''''''''''''''
            Try
                If con.State = ConnectionState.Closed Then con.Open()

                ''  cmd.Transaction = SqlTrans
                commandRecordRestore = " Select * Into [RestoreDatabseFor_" & DBName & "_AT_" & Format(DateAndTime.Now, "yyyy/MM/dd hh:mm:ss").ToString & "] From " & DBName & "..[tb_GE_EventLogs]"
                If con.State = ConnectionState.Closed Then con.Open()
                cmd = New SqlCommand(commandRecordRestore, con)
                cmd.ExecuteNonQuery()
                If con.State = ConnectionState.Open Then con.Close()
            Catch ex As Exception
                If con.State = ConnectionState.Open Then con.Close()
            End Try
            ''''''''''''''''''End  Add Eventlogs Table in  Master Database''''''''''''''''''
            With cmd

                Try

                    command = " ALTER DATABASE " & DBName & " SET OFFLINE WITH ROLLBACK IMMEDIATE"
                    If con.DataSource = "(local)" Or con.DataSource = "local" Or con.DataSource.ToUpper = Environment.MachineName.ToUpper Then
                        'If con.DataSource = "(local)" Or con.DataSource = "local" Or con.DataSource.ToUpper = tmpDatabase1(0).ToUpper Then

                        command2 = " restore database " & DBName & " from disk = '" & _FilePath & _FileName & "' WITH REPLACE "  'Client restore
                    Else
                        command2 = " restore database " & DBName & " from disk = '" & FilePath & "' WITH REPLACE "                  'Server restore

                    End If
                    'command2 = " restore database " & DBName & " from disk = '" & DBPath & "'"


                    If con.State = ConnectionState.Closed Then con.Open()
                    .Connection = con
                    .CommandText = command
                    .CommandTimeout = 0
                    .ExecuteNonQuery()
                    Try
                        .CommandText = command2
                        .CommandTimeout = 0
                        .ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    .CommandText = " ALTER DATABASE " & DBName & " SET Online "
                    .CommandTimeout = 0
                    .ExecuteNonQuery()

                Catch ex As Exception
                    .CommandText = " ALTER DATABASE " & DBName & " SET Online "
                    .CommandTimeout = 0
                    .ExecuteNonQuery()

                End Try

            End With

            Me.Enabled = True
            Me.btnBackup.Enabled = True
            Me.btnRestore.Enabled = True
            Me.Cursor = Windows.Forms.Cursors.Default
            btnRestore.Enabled = True
            btnBackup.Enabled = True
            'If CurrentUserLevelID <> AppAdminUserLevelID Then
            '    UserControl()
            'End If
            cls_DisplayMessage("Finished Restore Process...")
            ''  objEventLog.AddLog(clsEnum.EventState.Information, Date.Now, CurrentUserSysID, Me.Text, "Restore Database", "Edit", "", "")
            pbBackupRestore.Value = pbBackupRestore.Maximum
            MessageBox.Show("Database Restore Process Successfully." & vbCrLf & AppName & " is restarting now.", AppName & " : Database Restore", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '' objEventLog.AddLog(clsEnum.EventState.Information, Date.Now, CurrentUserSysID.ToString, CStr(Me.Tag), "", "Log Off", "", "")
            System.Windows.Forms.Application.Exit()
            Process.Start(System.Windows.Forms.Application.ExecutablePath)

        Catch ex As Exception
            cls_DisplayMessage(ex.Message)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            MsgBox(ex.Message)
            '  objEventLog.AddLog(clsEnum.EventState.Error, Date.Now, CurrentUserSysID, Me.Text, "Restore Database", "Edit", "", "System Error : " & ex.Message)
            Exit Sub
        End Try
    End Sub
    'myw 1.9.2010
    'Private Sub AddEventHandalers(ByVal ControlContainers As Control)
    '    For Each ctrl As Control In ControlContainers.Controls

    '        AddHandler ctrl.KeyPress, AddressOf Button_KeyPress
    '        If ctrl.HasChildren Then
    '            AddEventHandalers(ctrl)
    '        End If
    '    Next
    'End Sub
    'myw 1.9.2010
    'Private Sub Button_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If Asc(e.KeyChar) = 3 Then
    '        ToolStripbtnClose_Click(ToolStripbtnClose, New System.EventArgs)
    '    End If
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        'Dim sqlStatus As Integer
        'Dim accessStatus As Integer
        'If OptDB.Checked = True Then
        If btnSave.Text = "Change Connection" Then
            GroupBox2.Enabled = True
            cboDatabase.Enabled = True
            txtServer.Enabled = True
            txtServer.Text = ""
            ' LoadSQLServers()
            cboDatabase.DropDownStyle = ComboBoxStyle.DropDown

            TabControl1.TabPages.Remove(tabBackup)
            tabCon = 1
            btnSave.Text = "Save Connection"
        Else
            Dim isExists As Boolean
            Dim ansCreateDB = vbNo


            If txtServer.Text = "" Then
                MessageBox.Show("You have to type Server Name to save connection", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Try
                If cboDatabase.Text.Trim.Equals("") Then
                    MsgBox("You have to type or Select database name", MsgBoxStyle.Information, AppName)
                    cboDatabase.Focus()
                    Exit Sub
                End If
                isExists = objDBConnection.isExistsDatabase(cboDatabase.Text, IIf(txtServer.Text.Trim.ToLower = "(local)", "(local)", txtServer.Text.Trim), txtUserName.Text, txtPassword.Text, pTrustedConnection)
                If Not isExists Then
                    ansCreateDB = MessageBox.Show("This database does not exists. Do you want to create a new database ", "Create New Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If ansCreateDB = vbNo Then
                        Exit Sub
                    End If
                    lblMessage.Text = "Installing New Database.  Please wait..."
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    'If objDBConnection.InstallNewDatabase(ScriptFileName, cboDatabase.Text, cboServer.Text, txtUser.Text, txtPassword.Text, pTrustedConnection) = False Then
                    '    MsgBox("Install Database Fail!")
                    '    Exit Sub
                    'End If
                    Me.Cursor = Cursors.Arrow
                    lblMessage.Text = ""
                End If

                pValue = objDBConnection.CreateConnectionString(txtServer.Text, cboDatabase.Text, txtUserName.Text, txtPassword.Text, pTrustedConnection)
                AppConfiguration.AddConfiguration("MsSql", "System.Data.SqlClient", pValue)
                AppConfiguration.EncryptConfiguration()
                'Encrypt
                '**** pValue = objCrypto.Encrypt(pValue)
                'If objSettings.WriteReg(pValue) <> "ERROR" Then
                '****   If _My_Own_Settings.MyPut(key, pValue) <> "ERROR" Then
                Connect = True
                If isMain = True Then
                    MessageBox.Show("Server setting was configured successfully.  You have to restart application in order to change server settings", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Application.Exit()
                    ' objEventLog.AddLog(clsEnum.EventState.Information, Date.Now, CurrentUserSysID, Me.Text, "", "Edit", "")
                    ' 'Application.Restart()
                    End

                Else
                    If (MessageBox.Show("Server setting was configured successfully.  It is highly recommended to restart application in order to change server settings. Click 'Yes' to change immediately or 'No' to change later. If you click 'No', your old server setting still affect.", "Restart Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        Application.Exit()
                        '  objEventLog.AddLog(clsEnum.EventState.Information, Date.Now, CurrentUserSysID, Me.Text, "", "Edit", "")
                        'Application.Restart()
                        End
                    Else
                        Me.Close()
                    End If
                End If
                '****   End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                '  objEventLog.AddLog(clsEnum.EventState.Error, Date.Now, CurrentUserSysID, Me.Text, "", "Edit", "", "System Error : " & ex.Message.ToString())

            End Try
        End If
        'End If
        ''If btnSave.Text = "Change Connection" Then
        ''    'EnableControl()
        ''    'EnableControlAccess()

        ''    txtServer.Enabled = True
        ''    'txtUserName.Text = ""
        ''    txtPassword.Text = ""
        ''    txtUserName.ReadOnly = False
        ''    txtPassword.ReadOnly = False
        ''    rbtWindow.Checked = True
        ''    rbtSQL.Enabled = True
        ''    rbtWindow.Enabled = True
        ''    cboDatabase.Enabled = True
        ''    'cboDatabase.Items.Clear()
        ''    'cboDatabase.Text = ""
        ''    GroupBox2.Enabled = True
        ''    txtServer.Text = ""
        ''    txtServer.Focus()

        ''    TabControl1.TabPages.Remove(tabBackup)
        ''    rbtSQL.Checked = True
        ''    btnSave.Text = "Save Connection"
        ''    tabCon = 1
        ''    Exit Sub
        ''Else

        ''    accessStatus = objDBConnection.CheckAccessDatabase(txtAccessDBName.Text, "Admin", txtAccessPassword.Text)
        ''    If accessStatus = 2 Then
        ''        MsgBox("Incorrect Access database password. ", MsgBoxStyle.Exclamation, "Database connection")
        ''        Exit Sub
        ''    ElseIf accessStatus = 1 Then
        ''        If Not objDBConnection.IsServerMachine Then
        ''            MsgBox("This database does not exists.  Please use valid database or create a new one from server machine.", MsgBoxStyle.Critical, "Database Connection")
        ''            Exit Sub
        ''        End If

        ''        ansCreateDB = MessageBox.Show("This database does not exists. Do you want to create a new database?", "Create New Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ''        If ansCreateDB = vbNo Then
        ''            Exit Sub
        ''        End If

        ''        If objDBConnection.InstallNewAccessDatabase(ScriptAccessFileName, txtAccessDBName.Text, "Admin", txtAccessPassword.Text) = False Then
        ''            MsgBox("Install Database Fail!", MsgBoxStyle.Critical, AppName)
        ''            Exit Sub
        ''        End If
        ''    ElseIf accessStatus = 3 Then
        ''        MsgBox("Invalid access database!", MsgBoxStyle.Critical, "Database Connection")
        ''        Exit Sub
        ''    End If
        ''End If

        ''pValue = objDBConnection.CreateAccessConnectionString(txtAccessDBName.Text, "Admin", txtAccessPassword.Text)
        ''AppConfiguration.AddConfiguration("Access", "System.Data.OleDb", pValue)
        ''AppConfiguration.EncryptConfiguration()
        ''If isMain = True Then
        ''    MessageBox.Show("Access setting was configured successfully.  You have to restart application in order to change server settings", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ''    Application.Restart()
        ''    End

        ''Else
        ''    If (MessageBox.Show("Access setting was configured successfully.  It is highly recommended to restart application in order to change server settings. Click 'Yes' to change immediately or 'No' to change later. If you click 'No', your old server setting still affect.", "Restart Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
        ''        Application.Restart()
        ''        End
        ''    Else
        ''        Me.Close()
        ''    End If
        ''End If

    End Sub

    Private Sub cboDatabase_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDatabase.DropDown
        If txtServer.Text.Trim = "" Then
            MessageBox.Show("You have to type Server Name", "Invalid Servername", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If rbtSQL.Checked = True Then
            If txtUserName.Text = "" Then
                MessageBox.Show("You have to type User Name", "Invalid User Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If
        ConnectServerAndLoadDatabase()
    End Sub

    Private Sub rbtWindow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtWindow.CheckedChanged
        If rbtWindow.Checked = True Then
            gbSQL.Enabled = False
            pTrustedConnection = True

        Else
            gbSQL.Enabled = True
            pTrustedConnection = False

        End If

    End Sub

    Private Sub rbtSQL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtSQL.CheckedChanged
        If rbtSQL.Checked = True Then
            gbSQL.Enabled = True
            pTrustedConnection = False
        Else
            gbSQL.Enabled = False
            pTrustedConnection = True
        End If

    End Sub


    Private Sub butOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpenFile.Click
        SaveFileDialog1.Filter = "Access Files |*.MDB|All Files|*.*"
        Operational.FileDialog.FileDialogue.FileExtension = "mdb;*.MDB|All Files|*.*"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                txtAccessDBName.Text = SaveFileDialog1.FileName
            Catch ex As Exception
                If ex.Message = "The UNC path should be of the form \\server\share." Then
                    MessageBox.Show("Please select correct database path", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Try

        End If
    End Sub
    Private Sub frm_DatabaseConnection_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        WindowState = FormWindowState.Normal
    End Sub

    Private Sub frm_GE_DBConnection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'AddEventHandalers(Me)
        CheckTab()
        Dim con As SqlConnection
        If tabCon <> 1 Then


            ''Dim objApplicationOption As New ApplicationOption
            If isMain = False Then GetServerConnection()
            txtServer.Enabled = False
            rbtWindow.Checked = True
            GroupBox2.Enabled = False
            cboDatabase.Enabled = False


            Dim dtAppOption As New DataTable
            Dim DBServerName As String = ""

            Dim tmpDBPath As String = ""
            Dim conpath As String = ""
            Dim frontpath As String = ""
            Dim backpath As String = ""
            'test at 13.8.2013
            'Dim tmptest1() As String
            'Dim tmpRes1() As String
            'end test



            '    dbtype = AppConfiguration.ReadProviderName


            '    If dbtype = "System.Data.SqlClient" Then


            '        constr = AppConfiguration.ReadConnectionString

            '        tmptest1 = Split(constr, "Data Source=", 2)
            '        tmpRes1 = Split(tmptest1(1), ";", 2)

            '        con = New SqlConnection(constr)
            '        DBServerName = con.DataSource
            '        'If DBServerName.ToUpper = Environment.MachineName.ToUpper Or DBServerName.Equals("(local)") Or DBServerName.Equals("local") Then
            '        If DBServerName.ToUpper = tmpRes1(0).ToUpper Or DBServerName.Equals("(local)") Or DBServerName.Equals("local") Then
            '            btnBackup.Enabled = True
            '            btnRestore.Enabled = True
            '        Else
            '            ''tmpDBPath = objApplicationOption.GetApplicationOptionByKeyName("DatabasePath", Global_UserID)
            '            DBPath = tmpDBPath
            '            If tmpDBPath <> "" Then
            '                If Directory.Exists(tmpDBPath) Then
            '                    btnBackup.Enabled = True
            '                    btnRestore.Enabled = True
            '                Else
            '                    MessageBox.Show("Make Sure Database Server Share Path in Application Setting", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                    btnBackup.Enabled = False
            '                    btnRestore.Enabled = False
            '                    lblMessage.Visible = True
            '                End If
            '            Else
            '                lblMessage.Visible = True
            '                btnBackup.Enabled = False
            '                btnRestore.Enabled = False
            '            End If
            '        End If
            '    Else
            '    End If

            constr = AppConfiguration.ReadConnectionString
            con = New SqlConnection(constr)
            DBServerName = con.DataSource

            Dim _GlobalInfoController As BusinessRule.GlobalSetting.IGlobalSettingController = BusinessRule.Factory.Instance.CreateGlobalSettingController
            '  Dim argApp As New clsApplicationSetting
            Dim GlobalInfo As New CommonInfo.GlobalSettingInfo
            GlobalInfo = _GlobalInfoController.GetAllGlobalSettingInfo
            If IsNothing(GlobalInfo.DatabaseSharePath) Or GlobalInfo.DatabaseSharePath = "" Then
                tmpDBPath = ""
            Else
                tmpDBPath = GlobalInfo.DatabaseSharePath
            End If
            DBPath = tmpDBPath

            If DBServerName.ToUpper = Environment.MachineName.ToUpper Or DBServerName.Equals("(local)") Or DBServerName.Equals("local") Then
                btnBackup.Enabled = True
                btnRestore.Enabled = True
            Else
                ''tmpDBPath = objApplicationOption.GetApplicationOptionByKeyName("DatabasePath", Global_UserID)
                'DBPath = tmpDBPath
                If tmpDBPath <> "" Then
                    If Directory.Exists(tmpDBPath) Then
                        btnBackup.Enabled = True
                        btnRestore.Enabled = True
                    Else
                        MessageBox.Show("Make Sure Database Server Share Path in Application Setting", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        btnBackup.Enabled = False
                        btnRestore.Enabled = False
                        lblMessage.Visible = True
                    End If
                Else

                    If tmpDBPath <> "" Then
                        If Directory.Exists(tmpDBPath) Then
                            btnBackup.Enabled = True
                            btnRestore.Enabled = True
                            'UserControl()
                        Else
                            MessageBox.Show("Please make sure Path : " & tmpDBPath & " is exists in Server's PC.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            btnBackup.Enabled = False
                            btnRestore.Enabled = False
                            lblMessage.Visible = True
                        End If
                    Else
                        MessageBox.Show("Please fill ""Server Share Path"" in Application Setting", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        lblMessage.Visible = True
                        btnBackup.Enabled = False
                        btnRestore.Enabled = False
                    End If
                End If
            End If




        End If

        ''''TYW

        'Dim con As SqlConnection

        ''constr = AppConfiguration.ReadConnectionString

        ''con = New SqlConnection(constr)
        ''DBServerName = con.DataSource
        ''If DBServerName.ToUpper = Environment.MachineName.ToUpper Or DBServerName.Equals("(local)") Or DBServerName.Equals("local") Then
        ''    btnBackup.Enabled = True
        ''    btnRestore.Enabled = True
        ''Else
        ''    ''tmpDBPath = objApplicationOption.GetApplicationOptionByKeyName("DatabasePath", Global_UserID)
        ''    DBPath = tmpDBPath
        ''    If tmpDBPath <> "" Then
        ''        If Directory.Exists(tmpDBPath) Then
        ''            btnBackup.Enabled = True
        ''            btnRestore.Enabled = True
        ''        Else
        ''            MessageBox.Show("Make Sure Database Server Share Path in Application Setting", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''            btnBackup.Enabled = False
        ''            btnRestore.Enabled = False
        ''            lblMessage.Visible = True
        ''        End If
        ''    Else
        ''        Dim _GlobalInfoController As BusinessRule.GlobalSetting.IGlobalSettingController = BusinessRule.Factory.Instance.CreateGlobalSettingController
        ''        '  Dim argApp As New clsApplicationSetting
        ''        Dim GlobalInfo As New CommonInfo.GlobalSettingInfo
        ''        GlobalInfo = _GlobalInfoController.GetAllGlobalSettingInfo
        ''        If IsNothing(GlobalInfo.DatabaseSharePath) Or GlobalInfo.DatabaseSharePath = "" Then
        ''            tmpDBPath = ""
        ''        Else
        ''            tmpDBPath = GlobalInfo.DatabaseSharePath
        ''        End If
        ''        DBPath = tmpDBPath
        ''        If tmpDBPath <> "" Then
        ''            If Directory.Exists(tmpDBPath) Then
        ''                btnBackup.Enabled = True
        ''                btnRestore.Enabled = True
        ''                'UserControl()
        ''            Else
        ''                MessageBox.Show("Please make sure Path : " & tmpDBPath & " is exists in Server's PC.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''                btnBackup.Enabled = False
        ''                btnRestore.Enabled = False
        ''                lblMessage.Visible = True
        ''            End If
        ''        Else
        ''            MessageBox.Show("Please fill ""Server Share Path"" in Application Setting", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''            lblMessage.Visible = True
        ''            btnBackup.Enabled = False
        ''            btnRestore.Enabled = False
        ''        End If
        ''    End If
        ''End If




    End Sub

    Private Sub ToolStripbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripbtnClose.Click
        Me.Close()
    End Sub

    Private Sub OptDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptDB.CheckedChanged
        OptAccess.Checked = Not OptDB.Checked
        OptionChange(OptDB.Checked)
    End Sub



    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim f As Font
        Dim backBrush As Brush
        Dim foreBrush As Brush

        If (e.Index = Me.TabControl1.SelectedIndex) Then
            f = New Font(e.Font, FontStyle.Bold Or FontStyle.Bold)
            f = New Font(e.Font, FontStyle.Bold)

            backBrush = New System.Drawing.SolidBrush(Color.FromArgb(224, 224, 224))
            foreBrush = New SolidBrush(Color.Black)
        Else
            f = e.Font
            backBrush = New System.Drawing.SolidBrush(Color.FromArgb(224, 224, 224))
            foreBrush = New SolidBrush(Color.DarkGray)
        End If


        Dim tabName As String
        Dim sf As New StringFormat
        tabName = Me.TabControl1.TabPages(e.Index).Text


        sf.Alignment = StringAlignment.Center
        e.Graphics.FillRectangle(backBrush, e.Bounds)
        Dim r As Rectangle
        r = e.Bounds
        r = New Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3)
        e.Graphics.DrawString(tabName, f, foreBrush, r, sf)

        sf.Dispose()

        If (e.Index = Me.TabControl1.SelectedIndex) Then
            f.Dispose()
            backBrush.Dispose()
        Else
            backBrush.Dispose()
            foreBrush.Dispose()
        End If
    End Sub

#End Region



#Region "Private Methods"

    Private Sub ConnectServerAndLoadDatabase()
        Dim dt_Database As New DataTable
        OptDB.Checked = True
        dt_Database = objDBConnection.getDatabase(txtServer.Text.Trim, txtUserName.Text, txtPassword.Text, pTrustedConnection)
        If dt_Database.Rows.Count > 0 Then
            cboDatabase.DataSource = dt_Database
            cboDatabase.ValueMember = "name"
            cboDatabase.DisplayMember = "name"
        End If
    End Sub

    Private Sub CheckTab()
        If isMain = True Then
            btnSave.Text = "Save Connection"
            TabControl1.TabPages.Remove(tabBackup)
            tabCon = 1
        Else
            tabBackup.Enabled = True
        End If
    End Sub

    Private Sub EnableControl()
        ''txtServer.Enabled = True
        ' ''cmdRefreshServer.Enabled = True
        ' ''txtUserName.Text = ""
        ' ''txtPassword.Text = ""
        ''txtUserName.ReadOnly = False
        ''txtPassword.ReadOnly = False
        '' ''rbtWindow.Checked = True
        ''rbtSQL.Enabled = True
        ''rbtWindow.Enabled = True
        ''cboDatabase.Enabled = True
        ''cboDatabase.Items.Clear()
        ' ''cboDatabase.Text = ""
        ''GroupBox2.Enabled = True
        '' '' txtServer.Text = ""
        ''txtServer.Focus()

        txtServer.Enabled = True
        'txtUserName.Text = ""
        'txtPassword.Text = ""
        txtUserName.ReadOnly = False
        txtPassword.ReadOnly = False
        'rbtWindow.Checked = True
        rbtSQL.Enabled = True
        rbtWindow.Enabled = True
        cboDatabase.Enabled = True
        'cboDatabase.Items.Clear()
        'cboDatabase.Text = ""
        GroupBox2.Enabled = True
        'txtServer.Text = ""
        txtServer.Focus()
    End Sub

    Private Sub EnableControlAccess()
        txtAccessDBName.Enabled = True
        txtAccessPassword.Enabled = True
        txtAccessDBName.ReadOnly = False
        txtAccessPassword.ReadOnly = False
        butOpenFile.Enabled = True
        txtAccessDBName.Text = ""
        txtAccessPassword.Text = ""
        GroupBox2.Enabled = True
        txtAccessDBName.Focus()
    End Sub

    Private Sub GetServerConnection()
        Dim constr As String
        Dim tmpserver() As String
        Dim tmpDatabase1() As String
        Dim tmpDatabase2() As String
        Dim tmpUser1() As String
        Dim tmpUser2() As String
        Dim tmpPassword1() As String
        Dim tmpPassword2() As String
        Dim DBType As String = ""

        DBType = AppConfiguration.ReadProviderName
        constr = AppConfiguration.ReadConnectionString

        If DBType = "System.Data.SqlClient" Then
            OptDB.Checked = True
            tmpserver = Split(constr, "Data Source=", 2)
            txtServer.Text = tmpserver(1)

            tmpDatabase1 = Split(constr, "Initial Catalog=", 2)
            tmpDatabase2 = Split(tmpDatabase1(1), ";", 2)
            cboDatabase.Items.Add(tmpDatabase2(0))
            cboDatabase.SelectedIndex = 0


            tmpUser1 = Split(constr, "User ID=", 2)
            If tmpUser1.GetUpperBound(0) > 0 Then

                tmpUser2 = Split(tmpUser1(1), ";", 2)
                If tmpUser2(0) <> Nothing Then
                    txtUserName.Text = tmpUser2(0)
                    rbtSQL.Checked = True
                End If

            End If

            tmpPassword1 = Split(constr, "Password=", 2)
            If tmpPassword1.GetUpperBound(0) > 0 Then
                tmpPassword2 = Split(tmpPassword1(1), ";", 2)
                If tmpPassword2(0) <> Nothing Then
                    txtPassword.Text = tmpPassword2(0)
                End If
            End If
            'OptionChange(True)
            'Else
            '    OptAccess.Checked = True
            '    tmpserver = Split(constr, "Data Source=", 2)
            '    tmpDatabase1 = Split(tmpserver(1), ";", 2)
            '    If tmpDatabase1(0) <> Nothing Then
            '        txtAccessDBName.Text = tmpDatabase1(0)
            '    End If

            '    tmpPassword1 = Split(constr, "Jet OLEDB:Database Password=", 2)
            '    If tmpPassword1.GetUpperBound(0) > 0 Then
            '        tmpPassword2 = Split(tmpPassword1(1), ";", 2)
            '        If tmpPassword2(0) <> Nothing Then
            '            txtAccessPassword.Text = tmpPassword2(0)
            '        End If
            '    End If
            '    OptionChange(False)
        End If
        'txtServer.Enabled = False
        ''cmdRefreshServer.Enabled = False
        'cboDatabase.Enabled = False
        'txtUserName.ReadOnly = True
        'txtPassword.ReadOnly = True
        'rbtSQL.Enabled = False
        'rbtWindow.Enabled = False
        'txtAccessDBName.Enabled = False
        'txtAccessPassword.Enabled = False
        'txtAccessDBName.ReadOnly = True
        'txtAccessPassword.ReadOnly = True
        'butOpenFile.Enabled = False
        'GroupBox2.Enabled = False
    End Sub

    Private Sub OptionChange(ByVal Bool As Boolean)
        AccessPannel.Visible = Not Bool
        SqlPannel.Visible = Bool
    End Sub

#End Region
    'Private Sub txtServer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServer.Validated
    '    If txtServer.Text.Trim = "" Then
    '        MessageBox.Show("You have to type Server Name", "Invalid Servername", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        txtServer.Focus()
    '        Exit Sub
    '    End If
    'End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("DBUtilities")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class

