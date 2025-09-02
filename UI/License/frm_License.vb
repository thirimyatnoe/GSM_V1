Imports System
Imports UI.Register.Register
Imports UI.Register
Imports BusinessRule.UserManagement
Imports Operational.Cryptography
Imports BusinessRule.EventLogs
Imports System.Reflection
Imports System.IO
Imports System.Net
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports CommonInfo
Imports BusinessRule
Imports Operational.AppConfiguration
Imports System.Configuration
Imports Operational
Imports BusinessRule.Setting
Imports BusinessRule.Location
Imports System.Management

Public Class frm_License
    Dim SystemConfig As New SystemConfigurationInfo
    Dim objLicenseInfo As New RegistryInfo
    Dim MD5 As New Operational.Register.MD5
    Dim objDACrypto As New DACrypto
    Dim objRegister As New Register.Register(SubMain.ProcessorID, CompanyMode, _TimeZone, Global_UserID)
    Dim ext As Boolean = False
    Dim ProcessorId As String = SubMain.ProcessorID
    Private _MachineName As String = ""
    Public MyForm As frm_License
    Public Delegate Sub CloseForm(ByVal IsClose As Boolean)
    Dim objEventLog As New EventLog
    Public MyFormClose As CloseForm
    Private chanTcp As IChannel
    'Private IService As IGWTServices
    Dim str As String = ""
    Dim cmdactivateclick As Boolean = False
    Public frmStatus As Boolean
    Dim GenerateCompanyID As String
    Dim ActivateStatus As Boolean = False

    Dim objCompanyInfo As New CompanyProfileInfo
    'Dim objCompany As New Company
    Dim CompanyID As Long
    Dim _Locationid As String
    Private objAppOption As Setting.ISettingController = Factory.Instance.CreateSettingController
    Private objCompany As CompanyProfile.ICompanyProfileController = Factory.Instance.CreateCompanyProfileController
    Private _objLocation As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _GenerateController As GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        'MyFormClose = New CloseForm(AddressOf Me.PictureVisible)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Sub New(ByVal ext As Boolean)
        InitializeComponent()
        'MyFormClose = New CloseForm(AddressOf PictureVisible)
        Me.ext = ext
    End Sub

    Private Sub frm_License_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If ext And frmStatus Then
            If cmdactivateclick Then
                MsgBox("You need to Log-Off for change Company Mode.", MsgBoxStyle.Information, Me.Text)
                frm_GE_Splash.Close()
                Application.Restart()

            End If

            Me.Close()



            'frm_GE_Splash.Close()
            'Application.Restart()

        ElseIf ActivateStatus Then
            '_IsExit = True
            Me.Close()
            'frm_GE_Splash.Show()
            'frm_GE_Splash.Timer1.Start()
            ' frm_GE_Splash.Close()
            'frm_GE_Splash.Show()
            'frm_GE_Splash.Timer1.Start()

            'frm_GE_Splash.Close()
            'frm_GE_Splash.Show()
            'frm_GE_Splash.Timer1.Start()
            'Application.Restart()
        Else
            End
        End If
    End Sub

    Private Sub frm_License_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCompanyName.Enabled = True
        txtReferenceNo.Enabled = True


        'lblVersion.Text = "Version - " & AppVersion

        'pictureBoxProgress.Visible = False
        'pictureBoxProgress.Refresh()
        If ext Then
            txtCompanyName.Text = objAppOption.GetApplicationOptionByKeyNameBy("Company", Global_UserID)

            txtReferenceNo.Text = objAppOption.GetApplicationOptionByKeyNameBy("CustomerReferenceNo", Global_UserID)
            If txtReferenceNo.Text.Trim <> "" Then
                Dim LicensedDay As String = objAppOption.GetApplicationOptionByKeyNameBy("LicensedDay", Global_UserID)
                If LicensedDay = "0" Then
                    'lblMessage.Text = "Your license is valid for " & objAppOption.GetApplicationOptionByKeyNameBy("LicensedEmployee", Global_UserID) & " employees and unlimited licensed days."
                    lblMessage.Text = "Your license is valid for unlimited licensed days."
                Else
                    lblMessage.Text = "Your license is valid for " & objAppOption.GetApplicationOptionByKeyNameBy("LicensedEmployee", Global_UserID) & " employees and " & LicensedDay & " licensed days."
                End If
            Else
                lblMessage.Text = ""
            End If

            ' cmdTry.Enabled = False

        Else

            txtReferenceNo.Text = objAppOption.GetApplicationOptionByKeyNameBy("CustomerReferenceNo", Global_UserID)
            If CompanyMode = "Single" Then
                txtCompanyName.Text = objAppOption.GetApplicationOptionByKeyNameBy("Company", Global_UserID)
            End If

            'Dim Str As String = My.Settings.Default.SerialNumber  'My.Settings.Default.SerialNumber ' objAppOption.GetApplicationOptionByKeyNameBy("SerialNumber", Global_UserID)
            'If Str.Length > 0 Then
            '    Dim sv() As String = Str.Split("-")
            '    If sv.Length > 0 Then
            '        SerialKey1TextBox.Text = sv(0)
            '        SerialKey2TextBox.Text = sv(1)
            '        SerialKey3TextBox.Text = sv(2)
            '        SerialKey4TextBox.Text = sv(3)
            '        SerialKey5TextBox.Text = sv(4)
            '    End If
            'End If
            SerialKey1TextBox.Text = ""
            SerialKey2TextBox.Text = ""
            SerialKey3TextBox.Text = ""
            SerialKey4TextBox.Text = ""
            SerialKey5TextBox.Text = ""

            'txtReferenceNo.Text = ""
            'txtCompanyName.Text = ""
            lblMessage.Text = ""
            'cmdTry.Enabled = True

        End If

        If ext Then
            txtCompanyName.Enabled = False
            txtReferenceNo.Enabled = False
        End If
    End Sub

    Private Sub btnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivate.Click


        If txtCompanyName.Text.Trim = "" Then
            MsgBox("Please enter Company Name.", MsgBoxStyle.Information, Me.Text)
            txtCompanyName.Focus()
            Exit Sub
        End If
        If txtReferenceNo.Text.Trim = "" Then
            MsgBox("Please enter Customer Reference Number.", MsgBoxStyle.Information, Me.Text)
            txtReferenceNo.Focus()
            Exit Sub
        End If
        If txtProcessorID.Text.Trim = "" Then
            MsgBox("Please get Serial Key.", MsgBoxStyle.Information, Me.Text)
            txtProcessorID.Focus()
            Exit Sub
        End If



        If SerialKey1TextBox.Text.Trim = "" Or SerialKey2TextBox.Text.Trim = "" Or SerialKey3TextBox.Text.Trim = "" Or SerialKey4TextBox.Text.Trim = "" Or SerialKey5TextBox.Text.Trim = "" Then
            MsgBox("Please enter Serial Key.", MsgBoxStyle.Information, Me.Text)
            If SerialKey1TextBox.Text.Trim = "" Then SerialKey1TextBox.Focus()
            If SerialKey2TextBox.Text.Trim = "" Then SerialKey2TextBox.Focus()
            If SerialKey3TextBox.Text.Trim = "" Then SerialKey3TextBox.Focus()
            If SerialKey4TextBox.Text.Trim = "" Then SerialKey4TextBox.Focus()
            If SerialKey5TextBox.Text.Trim = "" Then SerialKey5TextBox.Focus()
            Exit Sub
        End If

        Dim FileMap As ExeConfigurationFileMap
        Dim oldcompanyid As String
        'Dim oldlocationid As String


        oldcompanyid = AppConfiguration.ReadAppSettings("CurrentCompanyID")
        'oldlocationid = config.AppSettings.Settings("Location").Value
        Dim Processor As String = ""
        Dim objCrypto As New Cryptography.DACrypto()

        cmdactivateclick = True
        If txtProcessorID.Text.Length > 0 Then
            Processor = txtProcessorID.Text.Trim  'objCrypto.Decrypt(txtProcessorID.Text.Trim)



            SubMain.ProcessorID = Processor 'Processor.Substring(0, Processor.Length - 3) 'gwt
            ProcessorId = SubMain.ProcessorID
        Else


            'Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")

            'For Each queryObj As ManagementObject In searcher.Get()
            '    Processor = queryObj("ProcessorId")
            'Next

            'Processor = Processor & "GWT"
            'SubMain.ProcessorID = objCrypto.Encrypt(Processor)
            'ProcessorId = objCrypto.Encrypt(Processor)
            SubMain.ProcessorID = ""
            ProcessorId = ""
        End If


        Dim type As String = SerialKey1TextBox.Text.Trim & SerialKey2TextBox.Text.Trim & SerialKey3TextBox.Text.Trim & SerialKey4TextBox.Text.Trim & SerialKey5TextBox.Text.Trim
        '''''''trz
        Dim _Type As String
        Dim _CID As String
        Dim _LicensedType As Integer
        Dim _LicensedCID As String
        Dim decrykey As String
        Dim SNumber As String = type

        decrykey = SNumber.Substring(20)
        _Type = decrykey.Substring(3)
        _CID = decrykey.Substring(0, 3)

        _LicensedType = objDACrypto.GetBase10(_Type)
        _LicensedCID = objDACrypto.GetBase10(_CID)

        If _LicensedType = 1 Then
            CompanyMode = "Single"


        Else
            CompanyMode = "Multiple"

        End If
        If _LicensedCID.Length = 1 Then
            GenerateCompanyID = "0" & _LicensedCID
        Else
            GenerateCompanyID = _LicensedCID
        End If



        '********** Time Zone ************** '
        ''zyz 15/1/2011 
        'If UseTimezone Then
        '    With TimeZoneInfo.CurrentTimeZone
        '        If .DisplayName.Contains("-") Then
        '            _TimeZone = .DisplayName.Substring(.DisplayName.IndexOf("-") + 1, 5)
        '        ElseIf .DisplayName.Contains("+") Then
        '            _TimeZone = .DisplayName.Substring(.DisplayName.IndexOf("+") + 1, 5)
        '        End If

        '    End With
        'Else
        '    _TimeZone = ""
        'End If

        _TimeZone = ""



        Try
            Dim Status As Boolean
            Dim _CompanyName As String
            'If CompanyMode = "Single" Then
            If txtCompanyName.Text.Trim = "" Then
                MsgBox("Comapny Name must be enter!", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If
            _CompanyName = txtCompanyName.Text.Trim
            Status = Check_SerialKey(txtReferenceNo.Text.Trim, ProcessorId, _CompanyName, _TimeZone, SerialKey1TextBox.Text.Trim & SerialKey2TextBox.Text.Trim & SerialKey3TextBox.Text.Trim & SerialKey4TextBox.Text.Trim & SerialKey5TextBox.Text.Trim)
            If Status = True Then
                If objRegister.UpdateLicenseSingle(objLicenseInfo, ProcessorId, Global_UserID) = False Then
                    MsgBox("Invalid License Key", MsgBoxStyle.Critical, Me.Text)
                    Exit Sub
                Else
                    Dim dt As DataTable = objCompany.GetCompanyProfileList("")
                    If dt.Rows.Count = 0 Then
                        With objCompanyInfo
                            .CompanyID = GenerateCompanyID 'CompanyID
                            .CompanyName = _CompanyName
                            .Address = ""
                            .Telephone = ""
                            .Email = ""
                            .Fax = ""
                            .WebSite = ""
                            .CompanyLogo = Nothing
                            If _LicensedCID = 1 Then
                                .HO = True
                            Else
                                .HO = False
                            End If

                            '.BudgetStart = 0

                            '.Monday = 0
                            '.Tuesday = 0
                            '.Wednesday = 0
                            '.Thursday = 0
                            '.Friday = 0
                            '.Saturday = 0
                            '.Sunday = 0

                            ''sya
                            '.city = ""
                            '.State = ""
                            '.Zip = ""
                            '.Country = ""
                        End With
                        objCompany.InsertCompanyProfileByKeyGenerate(objCompanyInfo) '  objCompany.SaveCompanyProfile(objCompanyInfo)



                        Dim FileMap2 As ExeConfigurationFileMap

                        FileMap2 = New ExeConfigurationFileMap
                        FileMap2.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                        Dim config2 As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                        config2.AppSettings.Settings("CurrentCompanyID").Value = GenerateCompanyID
                        'config2.AppSettings.Settings("Location").Value = _Locationid
                        config2.Save()



                    Else

                        objCompanyInfo = objCompany.GetCompanyProfile(oldcompanyid)
                        objCompanyInfo.CompanyID = GenerateCompanyID
                        objCompanyInfo.CompanyName = _CompanyName
                        objCompany.UpdateCompanyProfileByKeyGenerate(objCompanyInfo, oldcompanyid)

                        ''''''''' insert location'''''''''''''

                        'Dim dtgenerate As New DataTable
                        'dtgenerate = _GenerateController.Getdocuments()

                        'If dtgenerate.Rows.Count > 0 Then
                        '    For i As Integer = 0 To dtgenerate.Rows.Count - 1
                        '        If CBool(dtgenerate.Rows(i).Item("isusecid")) = True Then
                        '            Dim objgenerate As New GenerateFormatInfo
                        '            objgenerate.GenerateFormatID = dtgenerate.Rows(i).Item("GenerateFormatID").ToString
                        '            objgenerate.GenerateType = dtgenerate.Rows(i).Item("GenerateType").ToString
                        '            objgenerate.Prefix = dtgenerate.Rows(i).Item("Prefix").ToString
                        '            objgenerate.FormatDate = dtgenerate.Rows(i).Item("FormatDate")
                        '            objgenerate.Postfix = dtgenerate.Rows(i).Item("Postfix").ToString
                        '            objgenerate.NumberCount = dtgenerate.Rows(i).Item("NumberCount")
                        '            objgenerate.CompanyID = objCompanyInfo.CompanyID
                        '            objgenerate.Prefix2 = dtgenerate.Rows(i).Item("Prefix2").ToString
                        '            objgenerate.Postfix2 = dtgenerate.Rows(i).Item("Postfix2").ToString

                        '            _GenerateController.UpdateGenerateFormatForTransaction(objgenerate)
                        '        End If
                        '    Next
                        'End If

                        'Dim objlocationinfo As New CommonInfo.locationinfo
                        'Dim dtchecklocation As New DataTable
                        'dtchecklocation = _objLocation.GetLocation("AND L.CompanyID = '" & objCompanyInfo.CompanyID & "' AND L.Location ='MainStore'")
                        'If dtchecklocation.Rows.Count = 0 Then
                        '    Dim locationid
                        '    With objlocationinfo
                        '        .Locationid = ""
                        '        .Companyid = objCompanyInfo.CompanyID
                        '        .Location = "MainStore"
                        '    End With
                        '    locationid = _objLocation.SaveLocationinfo(objlocationinfo)
                        '    If locationid = "" Then
                        '        MsgBox("Location saving error", MsgBoxStyle.Information, "GlobalPOS")
                        '        Exit Sub
                        '    Else
                        '        _Locationid = locationid
                        '    End If
                        'Else
                        '    _Locationid = dtchecklocation.Rows(0).Item("@LocationID")
                        'End If
                        Dim FileMap3 As ExeConfigurationFileMap

                        FileMap3 = New ExeConfigurationFileMap
                        FileMap3.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                        Dim config3 As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                        config3.AppSettings.Settings("CurrentCompanyID").Value = GenerateCompanyID
                        'config3.AppSettings.Settings("Location").Value = _Locationid
                        config3.Save()


                    End If
                    'MsgBox("Activate successfully.", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                MsgBox("Invalid customer reference number.  Please contact your software vendor.", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If
            MsgBox("Activate Successfully with CompanyName: " & _CompanyName, MsgBoxStyle.Information, Me.Text)
            ActivateStatus = True
            Me.Close()
            'frm_GE_Splash.Close()
            ' Application.Restart()
            'frm_GE_Splash.Show()
            'frm_GE_Splash.Timer1.Start()

            'Else
            '_CompanyName = txtCompanyName.Text.Trim
            'Status = Check_SerialKeyMulti(txtReferenceNo.Text.Trim, ProcessorId, _CompanyName, SerialKey1TextBox.Text.Trim & SerialKey2TextBox.Text.Trim & SerialKey3TextBox.Text.Trim & SerialKey4TextBox.Text.Trim & SerialKey5TextBox.Text.Trim)
            'If Status = True Then
            '    objLicenseInfo.Company = _CompanyName
            '    If objRegister.UpdateLicenseMulti(objLicenseInfo, SubMain.ProcessorID, Global_UserID) = False Then
            '        MsgBox("Invalid License Key", MsgBoxStyle.Critical, Me.Text)
            '        Exit Sub
            '    Else


            '        ' MsgBox("Activate successfully.", MsgBoxStyle.Information, Me.Text)
            '    End If
            'Else
            '    MsgBox("Invalid customer reference number.  Please contact your software vendor.", MsgBoxStyle.Critical, Me.Text)
            '    Exit Sub
            'End If
            'MsgBox("Activate Successfully with Multiple Company ", MsgBoxStyle.Information, Me.Text)
            'ActivateStatus = True
            'Me.Close()
            '   End If


        Catch ex As Exception

            MsgBox("An error occurs during activation.", MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub
    Public Sub GetCompanyProfile()
        'If My.MySettings.Default.CurrentCompanyID = "01" Then
        '    Dim frm As New SMS.UI.frm_CompanyProfile
        '    frm.ShowDialog()
        'End If
        Dim FileMap As ExeConfigurationFileMap

        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim CurrentCompanyID As String = config.AppSettings.Settings("CurrentCompanyID").Value


        Dim dt As New DataTable
        Dim _CompanyFiles As CompanyProfile.ICompanyProfileController = Factory.Instance.CreateCompanyProfileController
        dt = _CompanyFiles.GetCompanyProfileList
        If dt.Rows.Count > 0 Then
            dt = _CompanyFiles.GetCompanyProfileList(" Where CompanyID ='" & CurrentCompanyID & "'")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Company_HOID = dt.Rows(0).Item("CompanyID")
                    Company_HOName = dt.Rows(0).Item("CompanyName")
                    Company_HOAddress = dt.Rows(0).Item("Address")
                    Company_HOPhone = dt.Rows(0).Item("Telephone")

                End If
            End If
        Else
            Dim frm As New UI.frm_CompanyProfile
            frm.ShowDialog()
            GetCompanyProfile()
        End If

    End Sub
    'Public Sub PictureVisible(ByVal IsClose As Boolean)
    '    Me.pictureBoxProgress.Visible = False
    '    Me.pictureBoxProgress.Refresh()
    '    If IsClose = True Then
    '        Me.Close()
    '    End If

    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If ext And frmStatus Then
            Me.Close()
        Else
            End
        End If

    End Sub

    Private Sub cmdTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dt As DataTable = objCompany.GetCompanyProfileList(0)
        If dt.Rows.Count <= 0 Then
            Dim _CompanyName As String = IIf(txtCompanyName.Text.Trim = "", "Default Company", txtCompanyName.Text.Trim)
            With objCompanyInfo
                .CompanyID = CompanyID
                .CompanyName = _CompanyName
                .Address = ""
                .Telephone = ""
                .Email = ""
                .Fax = ""
                .WebSite = ""
                .CompanyLogo = Nothing

                '.BudgetStart = 0

                '.Monday = 0
                '.Tuesday = 0
                '.Wednesday = 0
                '.Thursday = 0
                '.Friday = 0
                '.Saturday = 0
                '.Sunday = 0

                ''sya
                '.City = ""
                '.State = ""
                '.Zip = ""
                '.Country = ""

            End With
            objCompany.SaveCompanyProfile(objCompanyInfo)
        End If

        Me.Close()
    End Sub

    Public Function RegisterFormOpen() As RegistryInfo
        Me.ShowDialog()
        Return Me.objLicenseInfo
    End Function

    ''TNZS
    Private Function Check_SerialKeyMulti(ByVal _CustomerReferenceNo As String, ByVal _ProcessorID As String, ByVal _CompanyName As String, ByVal SerialKey As String) As Boolean
        Try
            Dim ActivateKey As String = ""
            Dim LocalKey As String = ""
            Dim _LicensedEmployee As Integer = 0
            Dim _LicensedDay As Integer = 0

            'LocalKey = objDACrypto.EncryptKey(_CustomerReferenceNo & _ProcessorID & SerialKey.Substring(20, 5))
            'LocalKey = objDACrypto.EncryptKey(_CustomerReferenceNo & _ProcessorID & SerialKey.Substring(15, 5))
            LocalKey = objDACrypto.EncryptKeyMulti(_CustomerReferenceNo & _ProcessorID & _CompanyName & SerialKey.Substring(15, 5) & SerialKey.Substring(20, 5))
            LocalKey = LocalKey.Substring(0, 15) & SerialKey.Substring(15, 5)
            'ActivateKey = SerialKey.Substring(0, 20)
            ActivateKey = SerialKey.Substring(0, 20)
            _LicensedEmployee = Convert.ToInt64(objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)), 16)

            'sya
            _LicensedDay = Convert.ToInt64(objDACrypto.EncryptCharToHex(SerialKey.Substring(15, 5)), 16)

            If ActivateKey <> Nothing AndAlso ActivateKey = LocalKey Then
                objLicenseInfo.LicensedEmployee = _LicensedEmployee
                objLicenseInfo.CustomerReferenceNo = _CustomerReferenceNo
                objLicenseInfo.ServerName = _MachineName

                'sya
                objLicenseInfo.LicensedDay = _LicensedDay
                If _LicensedDay = 0 Then objLicenseInfo.LicensedStatus = "UnlimitedDayLicense"

                'objLicenseInfo.SerialKey = objDACrypto.KeyGenerate(_CustomerReferenceNo & _ProcessorID & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)) & "-" & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)))
                objLicenseInfo.SerialKey = objDACrypto.KeyGenerate(_CustomerReferenceNo & _ProcessorID & _CompanyName & objDACrypto.EncryptCharToHex(SerialKey.Substring(15, 5)) & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)) & "-" & objDACrypto.EncryptCharToHex(SerialKey.Substring(15, 5)) & "-" & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)))

                Return True
            Else
                objLicenseInfo = New RegistryInfo
                Return False
            End If
        Catch ex As Exception

            objLicenseInfo = New RegistryInfo
            Return False
        End Try
    End Function
    Private Function Check_SerialKey(ByVal _CustomerReferenceNo As String, ByVal _ProcessorID As String, ByVal _Company As String, ByVal _Timezone As String, ByVal SerialKey As String) As Boolean
        Try
            Dim ActivateKey As String = ""
            Dim LocalKey As String = ""
            Dim _LicensedType As Integer
            Dim _LicensedCID As Integer
            Dim decrykey As String
            Dim _CID As String
            Dim _TypeCount As String

            Dim LicenseName As String

            'zyz ****** 12.1.2011 *********
            ActivateKey = SerialKey.Substring(0, 20)

            decrykey = SerialKey.Substring(20)
            _CID = decrykey.Substring(0, 3)
            _TypeCount = decrykey.Substring(3)

            _LicensedType = objDACrypto.GetBase10(_TypeCount)
            _LicensedCID = objDACrypto.GetBase10(_CID)

            LocalKey = objDACrypto.EncryptKey(_CustomerReferenceNo & _ProcessorID & _Company & _Timezone & Hex(_LicensedCID).PadLeft(5, "0").ToString & Hex(_LicensedType).PadLeft(5, "0").ToString)
            LocalKey = LocalKey.Substring(0, 20)
            If ActivateKey <> Nothing AndAlso ActivateKey = LocalKey Then
                objLicenseInfo.LicensedEmployee = _LicensedType
                objLicenseInfo.CustomerReferenceNo = _CustomerReferenceNo
                objLicenseInfo.ServerName = _MachineName
                objLicenseInfo.Company = _Company
                objLicenseInfo.Timezone = _Timezone

                'sya
                objLicenseInfo.LicensedDay = _LicensedCID
                If _LicensedCID = 0 Then objLicenseInfo.LicensedStatus = "UnlimitedDayLicense"

                LicenseName = _CustomerReferenceNo & _ProcessorID & _Company & _Timezone & Hex(_LicensedCID).PadLeft(5, "0").ToString & Hex(_LicensedType).PadLeft(5, "0").ToString
                objLicenseInfo.SerialKey = objDACrypto.KeyGenerateSingle(LicenseName.Trim) & "-" & objDACrypto.GetBase36(_LicensedCID).PadLeft(3, "A") & objDACrypto.GetBase36(_LicensedType).PadLeft(2, "A")
                'objLicenseInfo.SerialKey = objDACrypto.KeyGenerate(_CustomerReferenceNo & _ProcessorID & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)) & "-" & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)))
                ' objLicenseInfo.SerialKey = objDACrypto.KeyGenerate(_CustomerReferenceNo & _ProcessorID & objDACrypto.EncryptCharToHex(SerialKey.Substring(15, 5)) & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)) & "-" & objDACrypto.EncryptCharToHex(SerialKey.Substring(15, 5)) & "-" & objDACrypto.EncryptCharToHex(SerialKey.Substring(20, 5)))
                Return True
            Else
                objLicenseInfo = New RegistryInfo
                Return False
            End If
        Catch ex As Exception

            objLicenseInfo = New RegistryInfo
            Return False
        End Try
    End Function

    Private Sub SerialKey1TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey1TextBox.KeyDown
        Dim str1 As String = My.Computer.Clipboard.GetText()
        If e.KeyCode = Keys.V Then
            If e.Control = True Then
                str1 = RemoveInvalidChars(str1)
                PasteStr(str1, 1)
            End If
        End If
    End Sub

    ' add by HWM 27/Nov 2008


    Private Sub SerialKey2TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey2TextBox.KeyDown
        Dim str2 As String = My.Computer.Clipboard.GetText()
        If e.KeyCode = Keys.V Then
            If e.Control = True Then
                str2 = RemoveInvalidChars(str2)
                PasteStr(str2, 2)
            End If
        End If
    End Sub




    Private Sub SerialKey3TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey3TextBox.KeyDown
        Dim str3 As String = My.Computer.Clipboard.GetText()
        If e.KeyCode = Keys.V Then
            If e.Control = True Then
                str3 = RemoveInvalidChars(str3)
                PasteStr(str3, 3)
            End If
        End If
    End Sub



    Private Sub SerialKey4TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey4TextBox.KeyDown
        Dim str4 As String = My.Computer.Clipboard.GetText()
        If e.KeyCode = Keys.V Then
            If e.Control = True Then
                str4 = RemoveInvalidChars(str4)
                PasteStr(str4, 4)
            End If
        End If
    End Sub

    Private Sub SerialKey5TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey5TextBox.KeyDown
        Dim str5 As String = My.Computer.Clipboard.GetText()
        If e.KeyCode = Keys.V Then
            If e.Control = True Then
                str5 = RemoveInvalidChars(str5)
                PasteStr(str5, 5)
            End If
        End If
    End Sub

    Private Sub SerialKey1TextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey1TextBox.KeyUp
        Dim txtbox As TextBox = CType(sender, TextBox)
        If InStr("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", Chr(e.KeyValue)) > 0 Then
            If SerialKey1TextBox.Text.Length >= 5 Then
                SerialKey1TextBox.MaxLength = 5
                SerialKey2TextBox.Focus()
            End If
        End If
    End Sub

    Private Sub SerialKey2TextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey2TextBox.KeyUp
        If InStr("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", Chr(e.KeyValue)) > 0 Then
            If SerialKey2TextBox.Text.Length >= 5 Then
                SerialKey2TextBox.MaxLength = 5
                SerialKey3TextBox.Focus()
            End If
        End If
    End Sub

    Private Sub SerialKey3TextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey3TextBox.KeyUp
        If InStr("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", Chr(e.KeyValue)) > 0 Then
            If SerialKey3TextBox.Text.Length >= 5 Then
                SerialKey3TextBox.MaxLength = 5
                SerialKey4TextBox.Focus()
            End If
        End If
    End Sub

    Private Sub SerialKey4TextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey4TextBox.KeyUp
        If InStr("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", Chr(e.KeyValue)) > 0 Then
            If SerialKey4TextBox.Text.Length >= 5 Then
                SerialKey4TextBox.MaxLength = 5
                SerialKey5TextBox.Focus()
            End If
        End If
    End Sub

    Private Sub SerialKey5TextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SerialKey5TextBox.KeyUp
        If InStr("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", Chr(e.KeyValue)) > 0 Then
            If SerialKey5TextBox.Text.Length >= 5 Then
                SerialKey5TextBox.MaxLength = 5
            End If
        End If
    End Sub

    Private Sub SerialKeyTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles SerialKey1TextBox.KeyPress, SerialKey2TextBox.KeyPress, SerialKey3TextBox.KeyPress, SerialKey4TextBox.KeyPress, SerialKey5TextBox.KeyPress
        If InStr("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" & Chr(8), e.KeyChar) = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Function RemoveInvalidChars(ByVal parstr As String) As String
        parstr = parstr.Replace("-", "")
        parstr = parstr.Replace(".", "")
        parstr = parstr.Replace(" ", "")
        Return parstr
    End Function

    Private Sub PasteStr(ByVal parstr As String, ByVal tindex As Integer)
        Dim str(4) As String
        Dim i As Integer

        For i = 0 To 4
            If parstr.Length <= i * 5 Then
                str(i) = ""
            ElseIf parstr.Length >= (i + 1) * 5 Then
                str(i) = parstr.Substring(i * 5, 5)
            Else
                str(i) = parstr.Substring(i * 5)
            End If
        Next

        Select Case tindex
            Case 1
                If str(0).Length > 0 Then SerialKey1TextBox.Text = str(0)
                If str(1).Length > 0 Then SerialKey2TextBox.Text = str(1)
                If str(2).Length > 0 Then SerialKey3TextBox.Text = str(2)
                If str(3).Length > 0 Then SerialKey4TextBox.Text = str(3)
                If str(4).Length > 0 Then SerialKey5TextBox.Text = str(4)
            Case 2
                If str(0).Length > 0 Then SerialKey2TextBox.Text = str(0)
                If str(1).Length > 0 Then SerialKey3TextBox.Text = str(1)
                If str(2).Length > 0 Then SerialKey4TextBox.Text = str(2)
                If str(3).Length > 0 Then SerialKey5TextBox.Text = str(3)
            Case 3
                If str(0).Length > 0 Then SerialKey3TextBox.Text = str(0)
                If str(1).Length > 0 Then SerialKey4TextBox.Text = str(1)
                If str(2).Length > 0 Then SerialKey5TextBox.Text = str(2)
            Case 4
                If str(0).Length > 0 Then SerialKey4TextBox.Text = str(0)
                If str(1).Length > 0 Then SerialKey5TextBox.Text = str(1)
            Case 5
                If str(0).Length > 0 Then SerialKey5TextBox.Text = str(0)
        End Select
    End Sub

    Private Sub btnGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGet.Click
        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
        Dim Processor As String = ""
        Dim objCrypto As New Cryptography.DACrypto()
        For Each queryObj As ManagementObject In searcher.Get()
            Processor = queryObj("ProcessorId")
        Next

        Processor = Processor & "GWT"
        txtProcessorID.Text = objCrypto.Encrypt(Processor)
    End Sub

End Class
