Imports CommonInfo
Imports BusinessRule
Imports Operational
Imports System.Net
Imports System.Management
Imports System.Configuration
Imports Operational.AppConfiguration
Imports Operational.Cryptography
Imports System.IO
Imports System.Text

Public Class frm_GE_Splash
    Inherits System.Windows.Forms.Form

    'Private ScriptAccessFileName As String = Application.StartupPath & "\Script\UpdateAccessScriptV1.1.sql"
    'Private ScriptSqlFileName As String = Application.StartupPath & "\Script\UpdateAccessScriptV1.1.sql"

    Private ScriptSqlvoneFileName As String = Application.StartupPath & "\Script\UpdateSqlScriptV1.2.sql"
    Private ScriptAlterFileName As String = Application.StartupPath & "\Script\DBScript.sql"

    Dim AppConfig As New AppConfiguration

    Dim pTrustedConnection As Boolean
    Dim pKey As String = "ConnectionString"
    Dim pValue As String
    Public Connect As Boolean
    Public isMain As Boolean


    Dim objDBConnection As New DataAccess.DBConnection.DBConnectionDAL

    Dim DBPath As String = ""
    Dim constr As String
    Dim dbtype As String

    Dim aConfig As ConfigurationSettings
    Dim tabCon As Integer = Nothing
    Dim tmppath As String = ""
    Public MYCONFIG As String = ""
    Dim SQLDatabase As String = ""
    Dim SQLuser As String = ""
    Dim SQLPassword As String = ""
    Dim AccessDatabase As String = ""
    Dim AccessPassword As String = ""
    Dim ServerName As String = ""

    Dim tmpserver() As String
    Dim tmpDatabase1() As String
    Dim tmpDatabase2() As String
    Dim tmpUser1() As String
    Dim tmpUser2() As String
    Dim tmpPassword1() As String
    Friend WithEvents lblversion As System.Windows.Forms.Label
    Dim tmpPassword2() As String

    Private _GenerateController As GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private _SettingController As Setting.ISettingController = Factory.Instance.CreateSettingController

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblStatus As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblversion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Location = New System.Drawing.Point(16, 363)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(673, 20)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Application is starting ..."
        '
        'lblversion
        '
        Me.lblversion.AutoSize = True
        Me.lblversion.BackColor = System.Drawing.Color.Transparent
        Me.lblversion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblversion.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblversion.Location = New System.Drawing.Point(470, 182)
        Me.lblversion.Name = "lblversion"
        Me.lblversion.Size = New System.Drawing.Size(100, 17)
        Me.lblversion.TabIndex = 3
        Me.lblversion.Text = "Version 4.5000"
        Me.lblversion.Visible = False
        '
        'frm_GE_Splash
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.UI.My.Resources.Resources.SplashScreen
        Me.ClientSize = New System.Drawing.Size(499, 224)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblversion)
        Me.Controls.Add(Me.lblStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frm_GE_Splash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim objDACrypto As New DACrypto
    Dim EncryptKey As String = ConfigurationManager.AppSettings("GetEncrypt")
    Dim strGetKey As String = ""
    Dim strSerialKey As String = ""
    Dim strNumberKey As String = ""
    Dim strGlobalKey As String = ""
    Dim strEncrypt As String = ""
    Dim strGenerateKey As String = ""
    Friend Const gtBACKSLASH As String = "\"


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try

            ' My.MySettings.Default.MatchMachine
            Timer1.Stop()


            Dim clsDONGLE As New clsDongle


            lblStatus.Text = "Checking database connection ... "
            'test myw
            If CheckDatabaseSetting() = False Then
                lblStatus.Text = "Database connection failed  ... "
                Dim IsServerCheck As New BusinessRule.DBConnection.DBConnection
                If IsServerCheck.IsServerMachine Then
                    'Me.Hide()
                    If InitInstallationServer() = False Then End
                Else
                    If ServerConfiguration() = False Then End
                End If

            End If


            'test

            ' ''Dim DBServerName As String = ""
            ' ''Dim Sconnection As String = System.Configuration.ConfigurationManager.ConnectionStrings("DataAccess").ConnectionString

            ' ''If Sconnection <> "" Then
            ' ''    DBServerName = Split(Sconnection, "Data Source=", 2)(1)
            ' ''    DBServerName = DBServerName.Split("\")(0)
            ' ''    If DBServerName.Equals("(local)") Or DBServerName.Equals("local") Or DBServerName.Equals(".") Then
            ' ''        DBServerName = Environment.MachineName
            ' ''    End If
            ' ''    '''BLDevice.MachineName.Name = DBServerName

            ' ''    Dim IsUsedService As Boolean
            ' ''    IsUsedService = CBool(System.Configuration.ConfigurationManager.AppSettings("UsedService"))
            ' ''    If IsUsedService = True Then
            ' ''        Try
            ' ''            chanTcp = New TcpChannel

            ' ''            If ChannelServices.RegisteredChannels.Length < 1 Then
            ' ''                ChannelServices.RegisterChannel(chanTcp, False)
            ' ''            End If

            ' ''            IService = DirectCast(Activator.GetObject(GetType(IGWTServices.IGWTService), "tcp://" & BLDevice.MachineName.Name & ":7500/GWTServiceFPProcess"), IGWTService)
            ' ''        Catch ex As Exception

            ' ''            MessageBox.Show("Matching Server Service must be running.")
            ' ''            End
            ' ''        End Try

            ' ''        Dim ProcesserDT As New DataTable
            ' ''        Try
            ' ''            ProcesserDT = IService.SystemConfiguration()
            ' ''        Catch ex As Exception
            ' ''            Timer1.Stop()
            ' ''            MessageBox.Show("Matching Server Database connection is fail.", "Time Attendance", MessageBoxButtons.OK)
            ' ''        End Try

            ' ''        If (ProcesserDT.Rows.Count) Then
            ' ''            Dim Decr As New Operational.Cryptography.DACrypto
            ' ''            SubMain.ProcessorID = Decr.Decrypt(ProcesserDT.Rows(0)("ProcessorId"))
            ' ''        End If
            ' ''    Else
            ' ''        SubMain.ProcessorID = ""
            ' ''    End If


            ' ''End If

          
            Dim type As String = ""
            'If AppConfiguration.ReadAppSettings("SerialNumber") Then
            type = AppConfiguration.ReadAppSettings("SerialNumber")
            'End If
            'type = ""

            'MsgBox("" & config.ConnectionStrings.ConnectionStrings.Item("DataAccess").ToString & "")

            Dim _Type As String
            Dim _LicensedType As Integer
            Dim decrykey As String
            Dim SNumber As String = type
            SNumber = SNumber.Replace("-", "")
            decrykey = SNumber.Substring(20)
            _Type = decrykey.Substring(3)

            _LicensedType = objDACrypto.GetBase10(_Type)
            'If _LicensedType = 1 Then
            'CompanyMode = "Single"
            'Else
            CompanyMode = "Multiple"
            'End If


            Dim cm As New Object
            Dim db As New Object
            Dim dbp As New Object
            Dim dbc As New Object

            dbc = _SettingController.GetApplicationOptionByKeyName("Display_Box Company Name:")
            If Not IsNothing(dbc) Then
                DisplayBoxCompanyName = dbc
            End If

            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")

            Dim objCrypto As New Cryptography.DACrypto()
            Dim Processor As String = ""
            Dim Unique As String = ""
            Dim Name As String = ""
            Dim Manufacture As String = ""
            Dim Clock As String = ""
            For Each queryObj As ManagementObject In searcher.Get()
                Processor = queryObj("ProcessorId")
                Unique = queryObj("UniqueId")
                Name = queryObj("Name")
                Manufacture = queryObj("Manufacturer")
                Clock = queryObj("MaxClockSpeed")
            Next

            Processor = Processor & "GWT"
            SubMain.ProcessorID = objCrypto.Encrypt(Processor)

            _TimeZone = ""

        Catch ex As Exception

        End Try
        Me.Hide()
        Me.Dispose()

        'Dim objReg As New BLGeneral.Register.Register(SubMain.ProcessorID, Global_UserID)
        'Dim objEmp As New BLGeneral.Employee.Employee
        'Dim bolRequireActivate As Boolean = False

        'If objReg.LicenseStatus = EnumClass.LicenseStatus.UnRegistered Then
        '    bolRequireActivate = True
        '    MsgBox("Unregistered Version.", MsgBoxStyle.Exclamation, "Time Attendance")
        'ElseIf objReg.LicenseStatus = EnumClass.LicenseStatus.InvalidLicense Then
        '    bolRequireActivate = True
        '    MsgBox("Invalid License Key", MsgBoxStyle.Exclamation, "Time Attendance")
        'ElseIf objReg.LicenseStatus = EnumClass.LicenseStatus.ExpiredLicense Then
        '    bolRequireActivate = True
        '    MsgBox("Expired License Day", MsgBoxStyle.Exclamation, "Time Attendance")
        'Else
        '    If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
        '        bolRequireActivate = True
        '        MsgBox("Reach Maximum Licensed Employee", MsgBoxStyle.Exclamation, "Time Attendance")
        '    End If
        'End If

        'If bolRequireActivate Then
        '    Dim OPen As New frm_License
        '    Dim objLicenseInfo As General.LicenseInfo
        '    Dim objGeneral As New BLGeneral.General.General
        '    objLicenseInfo = frm_License.RegisterFormOpen()
        '    If objLicenseInfo.CustomerReferenceNo.Trim = "" Then   'Try
        '        If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
        '            MsgBox("Reach the limit of Trial Version.", MsgBoxStyle.Exclamation, "Time Attendance")
        '            End
        '        Else
        '            MsgBox("You are using Trial Version.", MsgBoxStyle.Exclamation, "Time Attendance")
        '        End If
        '    Else
        '        If objReg.UpdateLicense(objLicenseInfo, SubMain.ProcessorID, Global_UserID) = False Then
        '            MsgBox("Invalid License Key", MsgBoxStyle.Exclamation, "Time Attendance")
        '            End
        '        Else
        '            If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
        '                MsgBox("Reach the Maximum Licensed Employee.", MsgBoxStyle.Exclamation, "Time Attendance")
        '                End
        '            ElseIf objLicenseInfo.LicensedStatus = "UnlimitedDayLicense" Then
        '                MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")
        '            ElseIf objReg.ValidLicensedDay < objGeneral.GetTotalUsageDay() Then
        '                MsgBox("Reach the Maximum Licensed Day.", MsgBoxStyle.Exclamation, "Time Attendance")
        '                End
        '            Else
        '                MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")
        '            End If
        '        End If
        '    End If
        'End If

        'Global_LicensedEmployee = objReg.ValidLicensedEmployee

        'test myw

        lblversion.Text = AppVersion
        GetServerConnection()

        GetCompanyProfile()

        VersionUpgrade()
        RunSqlScript()
        'LicenseKey()

        If Login() = False Then End

        'OpenMdi()

        lblStatus.Text = "Preparing the Company infomation ..."
        'CallCompanySetting()

        'Me.Close()
    End Sub
    Private Sub RunSqlScript()
        If dbtype = "System.Data.SqlClient" Then
            If ServerName = "" Then
                MessageBox.Show("You have to type Server Name to save connection", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Try
                If constr.Contains("Integrated Security =SSPI") Then
                    pTrustedConnection = True
                Else
                    pTrustedConnection = False
                End If

                If File.Exists(ScriptAlterFileName) Then
                    If objDBConnection.CheckExitColumnFieldInTable(SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection) Then
                        Try
                            If objDBConnection.UpdateDatabase(ScriptAlterFileName, SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection) = True Then
                                Me.Cursor = Cursors.Arrow
                                Exit Sub
                            End If
                        Catch ex As Exception
                            MsgBox("Run Script Fail", MsgBoxStyle.Exclamation, "Global")
                        End Try
                        End If
                    End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
                Connect = False
            End Try
        End If
    End Sub
    Private Sub GetServerConnection()

        dbtype = AppConfiguration.ReadProviderName()
        constr = AppConfiguration.ReadConnectionString()

        If dbtype = "System.Data.SqlClient" Then

            tmpserver = Split(constr, "Data Source=", 2)

            ServerName = tmpserver(1)

            tmpDatabase1 = Split(constr, "Initial Catalog=", 2)
            tmpDatabase2 = Split(tmpDatabase1(1), ";", 2)
            'cboDatabase.Items.Add(tmpDatabase2(0))
            'cboDatabase.SelectedIndex = 0
            SQLDatabase = tmpDatabase2(0)

            tmpUser1 = Split(constr, "User ID=", 2)
            If tmpUser1.GetUpperBound(0) > 0 Then

                tmpUser2 = Split(tmpUser1(1), ";", 2)
                If tmpUser2(0) <> Nothing Then
                    Dim str As String = tmpUser2(0)

                    SQLuser = str

                End If

            End If

            tmpPassword1 = Split(constr, "password=", 2)
            If tmpPassword1.GetUpperBound(0) > 0 Then
                tmpPassword2 = Split(tmpPassword1(1), ";", 2)
                If tmpPassword2(0) <> Nothing Then
                    SQLPassword = tmpPassword2(0)
                End If
            End If
            If constr.Contains("Integrated Security =SSPI") Then
                pTrustedConnection = True
            Else
                pTrustedConnection = False
            End If
        End If
    End Sub

    ''Private Sub VersionUpgrade()

    ''    If dbtype = "System.Data.SqlClient" Then
    ''        If ServerName = "" Then
    ''            MessageBox.Show("You have to type Server Name to save connection", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''            Exit Sub
    ''        End If
    ''        Try

    ''            If constr.Contains("Integrated Security =SSPI") Then
    ''                pTrustedConnection = True
    ''            Else
    ''                pTrustedConnection = False
    ''            End If
    ''            If objDBConnection.IsValidSQLVersion(SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection) = False Then

    ''                Dim dtversion As DataTable
    ''                dtversion = objDBConnection.GetVersion()
    ''                If dtversion.Rows.Count > 0 Then



    ''                    If dtversion.Rows(0).Item("VersionNo").ToString <> AppVersoin Then
    ''                        'version 2
    ''                        Try
    ''                            If objDBConnection.UpdateDatabase(ScriptSqlvoneFileName, SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection) = True Then
    ''                                Me.Cursor = Cursors.Arrow


    ''                                Exit Sub
    ''                            End If
    ''                        Catch ex As Exception
    ''                            MsgBox("Script v 1.1 Error", MsgBoxStyle.Exclamation, "Global")
    ''                        End Try

    ''                        Me.Cursor = Cursors.Arrow
    ''                    End If
    ''                End If
    ''            Else
    ''                Dim dtversion As DataTable
    ''                dtversion = objDBConnection.GetVersion()
    ''                If dtversion.Rows.Count > 0 Then
    ''                    If dtversion.Rows(0).Item("VersionNo").ToString <> AppVersoin Then
    ''                        'version 2
    ''                        Try
    ''                            If objDBConnection.UpdateDatabase(ScriptSqlvoneFileName, SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection) = True Then
    ''                                Me.Cursor = Cursors.Arrow

    ''                                Exit Sub
    ''                            End If
    ''                        Catch ex As Exception
    ''                            MsgBox("Script v 1.1 Error", MsgBoxStyle.Exclamation, "Global")
    ''                        End Try

    ''                        Me.Cursor = Cursors.Arrow
    ''                    End If


    ''                End If
    ''            End If

    ''        Catch ex As Exception
    ''            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
    ''            Connect = False
    ''        End Try
    ''    End If


    ''End Sub

    Private Sub VersionUpgrade()

        Dim ansCreateDB As MsgBoxResult = vbNo
        Dim version As String = ""
        version = _SettingController.GetVersion()
        Dim sb As New StringBuilder
        Dim str() As String = {}
        Dim strfile As String
        Dim strversion As String = ""
        

        If IO.Directory.Exists(Application.StartupPath & "\Script\UpdateSqlScripts\") Then
            IO.File.Exists(Application.StartupPath & "\Script\UpdateSqlScripts\")
            Dim di As New IO.DirectoryInfo(Application.StartupPath & "\Script\UpdateSqlScripts\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo
            For Each dra In diar
                strversion = dra.Name.Substring(0, dra.Name.Length - 4)
                If CDbl(version) < CDbl(strversion) Then
                    sb.Append(dra.Name & ",")
                End If
            Next

            If sb.ToString <> "" Then
                str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(CChar(","))
            End If
            Array.Sort(str)
            For Each strfile In str
                objDBConnection.UpdateDatabase(Application.StartupPath & "\Script\UpdateSqlScripts\" & strfile, SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection)
            Next
        End If

    End Sub

    'Private Function IsRegister() As Boolean
    '    Dim Reg As New GWTRegistry
    '    If Reg.RegistryRead() Then
    '        Reg = Nothing
    '        Return True
    '    Else
    '        Reg = Nothing
    '        Dim OPen As New frm_License
    '        Return frm_License.RegisterFormOpen()
    '    End If
    'End Function

    'Private Function LicenseKey() As Boolean
    '    Dim objReg As New Register.Register(SubMain.ProcessorID, CompanyMode, _TimeZone, Global_UserID)
    '    'Dim objEmp As New BLGeneral.Employee.Employee
    '    Dim bolRequireActivate As Boolean = False

    '    ' MsgBox("lincense 1", MsgBoxStyle.Information, "")
    '    If objReg.LicenseStatus = EnumSetting.LicenseStatus.UnRegistered Then
    '        bolRequireActivate = True
    '        MsgBox("Unregistered Version.", MsgBoxStyle.Exclamation, AppName)

    '    ElseIf objReg.LicenseStatus = EnumSetting.LicenseStatus.InvalidLicense Then
    '        bolRequireActivate = True
    '        MsgBox("Invalid License Key", MsgBoxStyle.Exclamation, AppName)

    '    ElseIf objReg.LicenseStatus = EnumSetting.LicenseStatus.ExpiredLicense Then
    '        bolRequireActivate = True
    '        MsgBox("Expired License Day", MsgBoxStyle.Exclamation, AppName)

    '    Else
    '        'If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
    '        '    bolRequireActivate = True
    '        '    MsgBox("Reach Maximum Licensed Employee", MsgBoxStyle.Exclamation, "Time Attendance")
    '        'End If
    '    End If
    '    '  MsgBox("lincense 2", MsgBoxStyle.Information, "")
    '    If bolRequireActivate Then
    '        Dim OPen As New frm_License
    '        Dim objLicenseInfo As RegistryInfo
    '        'Dim objGeneral As New BLGeneral.General
    '        objLicenseInfo = frm_License.RegisterFormOpen()

    '        '  Me.Timer1.Start()



    '        'Me.Close()
    '        'Application.Restart()
    '        If objLicenseInfo.CustomerReferenceNo.Trim = "" Then   'Try
    '            'If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
    '            '    MsgBox("Reach the limit of Trial Version.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '    End
    '            'Else
    '            '    MsgBox("You are using Trial Version.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            'End If
    '        Else

    '            '   If CompanyMode = "Single" Then
    '            If objReg.UpdateLicenseSingle(objLicenseInfo, SubMain.ProcessorID, Global_UserID) = False Then
    '                MsgBox("Invalid License Key", MsgBoxStyle.Critical, Me.Text)
    '                Return False
    '                Exit Function
    '            Else




    '                'Me.Timer1.Start()
    '                Return True
    '                '   MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")

    '            End If
    '            '    Else
    '            '    If objReg.UpdateLicenseMulti(objLicenseInfo, SubMain.ProcessorID, Global_UserID) = False Then
    '            '        MsgBox("Invalid License Key", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '        Return False
    '            '        End
    '            '    Else
    '            '        'If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
    '            '        '    MsgBox("Reach the Maximum Licensed Employee.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '        '    End
    '            '        'ElseIf objLicenseInfo.LicensedStatus = "UnlimitedDayLicense" Then
    '            '        '    MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")
    '            '        '    'ElseIf objReg.ValidLicensedDay < objGeneral.GetTotalUsageDay() Then
    '            '        '    '    MsgBox("Reach the Maximum Licensed Day.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '        '    '    End
    '            '        'Else
    '            '        '    MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")
    '            '        'End If
    '            '    End If
    '            'End If

    '        End If
    '    End If
    '    ' MsgBox("lincense 3", MsgBoxStyle.Information, "")
    '    Global_LicensedEmployee = objReg.ValidLicensedEmployee
    'End Function
    'Private Function LicenseKey() As Boolean
    '    Dim objReg As New Register.Register(SubMain.ProcessorID, CompanyMode, _TimeZone, Global_UserID)
    '    'Dim objEmp As New BLGeneral.Employee.Employee
    '    Dim bolRequireActivate As Boolean = False

    '    ' MsgBox("lincense 1", MsgBoxStyle.Information, "")
    '    If objReg.LicenseStatus = EnumSetting.LicenseStatus.UnRegistered Then
    '        bolRequireActivate = True
    '        MsgBox("Unregistered Version.", MsgBoxStyle.Exclamation, AppName)

    '    ElseIf objReg.LicenseStatus = EnumSetting.LicenseStatus.InvalidLicense Then
    '        bolRequireActivate = True
    '        MsgBox("Invalid License Key", MsgBoxStyle.Exclamation, AppName)

    '    ElseIf objReg.LicenseStatus = EnumSetting.LicenseStatus.ExpiredLicense Then
    '        bolRequireActivate = True
    '        MsgBox("Expired License Day", MsgBoxStyle.Exclamation, AppName)

    '    Else
    '        'If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
    '        '    bolRequireActivate = True
    '        '    MsgBox("Reach Maximum Licensed Employee", MsgBoxStyle.Exclamation, "Time Attendance")
    '        'End If
    '    End If
    '    '  MsgBox("lincense 2", MsgBoxStyle.Information, "")
    '    If bolRequireActivate Then
    '        Dim OPen As New frm_License
    '        Dim objLicenseInfo As RegistryInfo
    '        'Dim objGeneral As New BLGeneral.General
    '        objLicenseInfo = frm_License.RegisterFormOpen()

    '        '  Me.Timer1.Start()



    '        'Me.Close()
    '        'Application.Restart()
    '        If objLicenseInfo.CustomerReferenceNo.Trim = "" Then   'Try
    '            'If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
    '            '    MsgBox("Reach the limit of Trial Version.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '    End
    '            'Else
    '            '    MsgBox("You are using Trial Version.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            'End If
    '        Else

    '            '   If CompanyMode = "Single" Then
    '            If objReg.UpdateLicenseSingle(objLicenseInfo, SubMain.ProcessorID, Global_UserID) = False Then
    '                MsgBox("Invalid License Key", MsgBoxStyle.Critical, Me.Text)
    '                Return False
    '                Exit Function
    '            Else




    '                'Me.Timer1.Start()
    '                Return True
    '                '   MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")

    '            End If
    '            '    Else
    '            '    If objReg.UpdateLicenseMulti(objLicenseInfo, SubMain.ProcessorID, Global_UserID) = False Then
    '            '        MsgBox("Invalid License Key", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '        Return False
    '            '        End
    '            '    Else
    '            '        'If objReg.ValidLicensedEmployee < objEmp.GetEnrolledEmployeeCount() Then
    '            '        '    MsgBox("Reach the Maximum Licensed Employee.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '        '    End
    '            '        'ElseIf objLicenseInfo.LicensedStatus = "UnlimitedDayLicense" Then
    '            '        '    MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")
    '            '        '    'ElseIf objReg.ValidLicensedDay < objGeneral.GetTotalUsageDay() Then
    '            '        '    '    MsgBox("Reach the Maximum Licensed Day.", MsgBoxStyle.Exclamation, "Time Attendance")
    '            '        '    '    End
    '            '        'Else
    '            '        '    MsgBox("License Activate Successfully", MsgBoxStyle.Information, "License Activation")
    '            '        'End If
    '            '    End If
    '            'End If

    '        End If
    '    End If
    '    ' MsgBox("lincense 3", MsgBoxStyle.Information, "")
    '    Global_LicensedEmployee = objReg.ValidLicensedEmployee
    'End Function
End Class
