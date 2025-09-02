Imports CommonInfo.EnumSetting
Imports Operational.Cryptography
Imports System.Net
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting
Imports Operational.AppConfiguration
Imports BusinessRule
Imports CommonInfo
Imports System.Configuration
Imports System.IO

Namespace Register




    Public Class Register
        Dim objDACrypto As New DACrypto
        ' Dim objGeneral As New General.General
        'Dim MD5 As New MD5
        Private Const FREE_LICENSED_EMPLOYEE As Long = 0
        Private _LicenseStatus As LicenseStatus
        Private _ValidLicensedEmployee As Long
        Private _ValidLicensedDay As Long
        Private Const FREE_LICENSED_DAY As Long = 30

        Private _SettingController As Setting.ISettingController = Factory.Instance.CreateSettingController
        Dim SNumber As String = ""


        Public ReadOnly Property LicenseStatus() As LicenseStatus
            Get
                Return _LicenseStatus
            End Get
        End Property

        Public ReadOnly Property ValidLicensedEmployee() As Long
            Get
                Return _ValidLicensedEmployee
            End Get
        End Property

        Public ReadOnly Property ValidLicensedDay() As Long
            Get
                Return _ValidLicensedDay
            End Get
        End Property

        Public Sub New(ByVal _ProcessorID As String, ByVal CompanyType As String, ByVal TimeZone As String, ByVal CurrentUserID As String)
            '' ----------- uncomment to apply license validation --------------------------- ''
            'Dim objAppOpt As New BusinessRule   ApplicationOption.ApplicationOption
            Dim LicenseName As String = ""
            Dim SerialNumber As String = ""
            Dim LicensedEmployee As Long
            Dim LicensedDay As Long = 0
            Dim LicensedCID As Long = 0
            Dim Company As String = ""
            Dim LicenseRecord As String
            Dim LicenseInfo As Boolean



            Dim EncryptKey As String = ConfigurationManager.AppSettings("GetEncrypt")

            'Dim FileMap As ExeConfigurationFileMap

            'FileMap = New ExeConfigurationFileMap
            'FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "SMS.UI.exe.config")
            'Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
            'Dim SNumber As String = config.AppSettings.Settings("SerialNumber").Value
            'Dim SCompanyID As String = config.AppSettings.Settings("CurrentCompanyID").Value

            Dim SNumber As String = ""
            SNumber = AppConfiguration.ReadAppSettings("SerialNumber")
            Dim SCompanyID As String = ""
            SCompanyID = AppConfiguration.ReadAppSettings("CurrentCompanyID")

          
            ' Dim SNumber1 As String = My.Settings.Default.SerialNumber

            LicenseName = _SettingController.GetApplicationOptionByKeyNameBy("CustomerReferenceNo", CurrentUserID)


            If LicenseName.Trim = "" Then
                _LicenseStatus = LicenseStatus.UnRegistered
                _ValidLicensedEmployee = FREE_LICENSED_EMPLOYEE
            Else

                ' MsgBox("my.mysetting" & SNumber)
                ' MsgBox("appsetting" & sn)

                SerialNumber = SNumber ' _SettingController.GetApplicationOptionByKeyNameBy("SerialNumber", CurrentUserID)
                LicensedEmployee = 0  ' _SettingController.GetApplicationOptionByKeyNameBy("LicensedEmployee", CurrentUserID)
                Company = _SettingController.GetApplicationOptionByKeyNameBy("Company", CurrentUserID)
                Dim lDay As String
                lDay = SCompanyID ' _SettingController.GetApplicationOptionByKeyNameBy("LicensedDay", CurrentUserID)
                If lDay <> "" Then
                    LicensedCID = CInt(lDay)
                End If
                lDay = 0
                If lDay <> "" Then
                      LicensedDay = CInt(lDay)
                End If
                If CompanyType = "Single" Then
                    LicensedEmployee = 1  'single
                Else
                    LicensedEmployee = 2  'multi
                End If
                ''zyz 11/1/2011
                'If CompanyType = "Single" Then

                'LicenseRecord = LicenseName & _ProcessorID & Company & TimeZone & Hex(LicensedDay).PadLeft(5, "0").ToString & Hex(LicensedEmployee).PadLeft(5, "0").ToString & "-" & objDACrypto.GetBase36(LicensedDay).PadLeft(3, "A") & objDACrypto.GetBase36(LicensedEmployee).PadLeft(2, "A")
                LicenseRecord = LicenseName & _ProcessorID & Company & TimeZone & Hex(LicensedCID).PadLeft(5, "0").ToString & Hex(LicensedEmployee).PadLeft(5, "0").ToString & "-" & objDACrypto.GetBase36(LicensedCID).PadLeft(3, "A") & objDACrypto.GetBase36(LicensedEmployee).PadLeft(2, "A")
                LicenseInfo = LicenseValidateSingle(SerialNumber, LicenseRecord)
                '    Else
                '    LicensedEmployee = 2        'multi
                '    LicenseInfo = LicenseValidate(SerialNumber, LicenseName & _ProcessorID & Company & Hex(LicensedCID).PadLeft(5, "0") & Hex(LicensedEmployee).PadLeft(5, "0") & "-" & Hex(LicensedCID).PadLeft(5, "0") & "-" & Hex(LicensedEmployee).PadLeft(5, "0"))
                'End If
                If LicenseInfo Then
                    If LicensedDay = 0 Then
                        _ValidLicensedEmployee = LicensedEmployee
                        _LicenseStatus = LicenseStatus.Registered  ''''LicenseStatus.UnlimitedDayLicense 
                    ElseIf LicenseValidateDay(LicensedDay) Then
                        _ValidLicensedEmployee = LicensedEmployee
                        _LicenseStatus = LicenseStatus.Registered
                        _ValidLicensedDay = LicensedDay
                    Else
                        _ValidLicensedDay = LicensedDay
                        _LicenseStatus = LicenseStatus.ExpiredLicense
                    End If
                Else
                    _LicenseStatus = LicenseStatus.InvalidLicense
                    _ValidLicensedEmployee = FREE_LICENSED_EMPLOYEE
                End If

            End If

            '_ValidLicensedEmployee = 1000
            '_LicenseStatus = CommonInfo.EnumClass.LicenseStatus.Registered
        End Sub
        Private Sub Save()
            Try
                Dim FileMap As ExeConfigurationFileMap

                FileMap = New ExeConfigurationFileMap
                FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

                Dim run As String = ""
                'If chkAutoStart.Checked = True Then
                '    run = "auto"
                'Else
                '    run = "none"
                'End If
                config.AppSettings.Settings("AutoRun").Value = run
                config.Save()

            Catch ex As FileNotFoundException

            End Try
        End Sub
        ''TNZS
        Public Function UpdateLicenseMulti(ByVal LicenseInfo As CommonInfo.RegistryInfo, ByVal _ProcessorID As String, ByVal CurrentUserID As String) As Boolean
            Try
                'If LicenseValidate(LicenseInfo.SerialKey, LicenseInfo.CustomerReferenceNo & _ProcessorID & Hex(LicenseInfo.LicensedEmployee).PadLeft(5, "0") & "-" & Hex(LicenseInfo.LicensedEmployee).PadLeft(5, "0")) Then

                If LicenseValidate(LicenseInfo.SerialKey, LicenseInfo.CustomerReferenceNo & _ProcessorID & LicenseInfo.Company & Hex(LicenseInfo.LicensedDay).PadLeft(5, "0") & Hex(LicenseInfo.LicensedEmployee).PadLeft(5, "0") & "-" & Hex(LicenseInfo.LicensedDay).PadLeft(5, "0") & "-" & Hex(LicenseInfo.LicensedEmployee).PadLeft(5, "0")) Then
                    'Dim objAppOpt As New ApplicationOption.ApplicationOption

                    _SettingController.SaveSettingBy(New SettingInfo("CustomerReferenceNo", "Purchase License Name", LicenseInfo.CustomerReferenceNo), CurrentUserID)


                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("SerialNumber").Value = LicenseInfo.SerialKey
                    config.Save()


                    '_SettingController.SaveSettingBy(New SettingInfo("LicensedEmployee", "Purchase Licensed Employee", LicenseInfo.LicensedEmployee), CurrentUserID)
                    ' _SettingController.SaveSettingBy(New SettingInfo("SerialNumber", "Purchase Serial Number", LicenseInfo.SerialKey), CurrentUserID)
                    _SettingController.SaveSettingBy(New SettingInfo("LicensedDay", "Purchase Licensed Day", LicenseInfo.LicensedDay), CurrentUserID)

                    _ValidLicensedEmployee = LicenseInfo.LicensedEmployee
                    _ValidLicensedDay = LicenseInfo.LicensedDay

                    _LicenseStatus = CommonInfo.EnumSetting.LicenseStatus.Registered
                    Return True
                Else
                    _LicenseStatus = LicenseStatus.InvalidLicense
                    _ValidLicensedEmployee = FREE_LICENSED_EMPLOYEE
                    _ValidLicensedDay = FREE_LICENSED_DAY
                    Return False
                End If
            Catch ex As Exception

                Return False
            End Try

        End Function
        ''zyz
        Public Function UpdateLicenseSingle(ByVal LicenseInfo As CommonInfo.RegistryInfo, ByVal _ProcessorID As String, ByVal CurrentUserID As String) As Boolean
            Dim LicenseName As String = ""

            LicenseName = LicenseInfo.CustomerReferenceNo & _ProcessorID & LicenseInfo.Company & LicenseInfo.Timezone & Hex(LicenseInfo.LicensedDay).PadLeft(5, "0").ToString & Hex(LicenseInfo.LicensedEmployee).PadLeft(5, "0").ToString & "-" & objDACrypto.GetBase36(LicenseInfo.LicensedDay).PadLeft(3, "A") & objDACrypto.GetBase36(LicenseInfo.LicensedEmployee).PadLeft(2, "A")
            Try
                'If LicenseValidate(LicenseInfo.SerialKey, LicenseInfo.CustomerReferenceNo & _ProcessorID & Hex(LicenseInfo.LicensedEmployee).PadLeft(5, "0") & "-" & Hex(LicenseInfo.LicensedEmployee).PadLeft(5, "0")) Then

                If LicenseValidateSingle(LicenseInfo.SerialKey, LicenseName) Then
                    ' Dim objAppOpt As New ApplicationOption.ApplicationOption

                    _SettingController.SaveSettingBy(New SettingInfo("CustomerReferenceNo", "Purchase License Name", LicenseInfo.CustomerReferenceNo), CurrentUserID)
                    '_SettingController.SaveSettingBy(New SettingInfo("LicensedEmployee", "Purchase Licensed Employee", LicenseInfo.LicensedEmployee), CurrentUserID)


                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("SerialNumber").Value = LicenseInfo.SerialKey

                    Dim LincenseCID As String = LicenseInfo.LicensedDay
                    Dim GenerateCompanyID As String
                    If LincenseCID.Length = 1 Then
                        GenerateCompanyID = "0" & LincenseCID
                    Else
                        GenerateCompanyID = LincenseCID
                    End If

                    config.AppSettings.Settings("CurrentCompanyID").Value = GenerateCompanyID

                    ' MsgBox("" & ConfigurationManager.AppSettings("CurrentCompanyID") & "", MsgBoxStyle.Information, "")
                    'Dim GenerateCompanyID As String
                    'If LicenseInfo.LicensedDay.ToString.Length = 1 Then
                    '    GenerateCompanyID = "0" & LicenseInfo.LicensedDay
                    'Else
                    '    GenerateCompanyID = LicenseInfo.LicensedDay
                    'End If
                    'config.AppSettings.Settings("CurrenntCompanyID").Value = GenerateCompanyID
                    config.Save()


                    'My.MySettings.Default.SerialNumber = LicenseInfo.SerialKey
                    ' My.Settings.Default.SerialNumber = LicenseInfo.SerialKey

                    ' MsgBox("my.mysettting" & My.Settings.Default.SerialNumber)
                    ' MsgBox("appsetting" & ConfigurationManager.AppSettings.Item("SerialNumber"))

                    '_SettingController.SaveSettingBy(New SettingInfo("SerialNumber", "Purchase Serial Number", LicenseInfo.SerialKey), CurrentUserID)
                    _SettingController.SaveSettingBy(New SettingInfo("LicensedDay", "Purchase Licensed Day", LicenseInfo.LicensedDay), CurrentUserID)
                    _SettingController.SaveSettingBy(New SettingInfo("Company", "Company", LicenseInfo.Company), CurrentUserID)
                    ' _SettingController.SaveSettingBy(New SettingInfo("TimeZone", "TimeZone", LicenseInfo.Timezone), CurrentUserID)

                    _ValidLicensedEmployee = LicenseInfo.LicensedEmployee
                    _ValidLicensedDay = LicenseInfo.LicensedDay

                    _LicenseStatus = CommonInfo.EnumSetting.LicenseStatus.Registered
                    Return True
                Else
                    _LicenseStatus = LicenseStatus.InvalidLicense
                    _ValidLicensedEmployee = FREE_LICENSED_EMPLOYEE
                    _ValidLicensedDay = FREE_LICENSED_DAY
                    Return False
                End If
            Catch ex As Exception

                Return False
            End Try

        End Function
#Region "Private Methods"

        Private Function ValidateLicense(ByVal SerialKey As String, ByVal LicenseName As String, ByVal EmployeeCount As Long) As Boolean
            Dim tmpKey As String
            tmpKey = RegisterCrypto.Encrypt(LicenseName.PadRight(20) & EmployeeCount.ToString.PadRight(5), LicenseName.PadRight(20))
            If FormatKey(tmpKey) = SerialKey Then
                Return True
            Else
                Return False
            End If

        End Function
        ''TNZS
        Public Function LicenseValidate(ByVal SerialKey As String, ByVal LicenseName As String) As Boolean
            Dim tmpKey As String
            tmpKey = objDACrypto.KeyGenerate(LicenseName)
            'tmpKey = tmpKey & "-" & LicenseName.Substring(15, 5)
            If tmpKey = SerialKey Then
                Return True
            Else
                Return False
            End If

        End Function
        Public Function LicenseValidateSingle(ByVal SerialKey As String, ByVal LicenseName As String) As Boolean
            Dim tmpKey As String
            Dim LastKey As String
            Dim LicenseKey As String
            LicenseKey = LicenseName.Substring(0, LicenseName.Length - 6)
            LastKey = LicenseName.Substring(LicenseName.Length - 6)
            tmpKey = objDACrypto.KeyGenerateSingle(LicenseKey)
            'tmpKey = tmpKey & "-" & LicenseName.Substring(15, 5)
            If tmpKey & LastKey = SerialKey Then
                Return True
            Else
                Return False
            End If

        End Function

        ''sya
        Private Function LicenseValidateDay(ByVal LicenseDay As Long) As Boolean

            'Dim days As Long
            'days = objGeneral.GetTotalUsageDay()
            'If LicenseDay >= days Then
            '    Return True
            'Else
            '    Return False
            'End If

        End Function

        Private Function FormatKey(ByVal Key As String) As String
            Dim strReturn As String
            Key = Key.Replace("+", "")
            Key = Key.Replace("/", "")
            Key = Key.Replace("-", "")
            Key = Key.Replace("=", "")

            If Key.Length > 25 Then
                Key = Key.Substring(0, 25).ToUpper
            End If
            strReturn = Key.Substring(0, 5) + "-" + Key.Substring(5, 5) + "-" + Key.Substring(10, 5) + "-" + Key.Substring(15, 5) + "-" + Key.Substring(20, 5)
            Return strReturn
        End Function

        Public Function GetProcessorID(ByRef MachineName As String) As String
            ' Dim objAppOpt As New ApplicationOption.ApplicationOption
            Dim Crypto As New DACrypto
            Dim ServerName As String = ""
            'Dim IService As IGWTServices.IGWTService = Nothing
            ' Dim chanTcp As TcpChannel
            Dim DT As New DataTable
            Dim DBType As String = ""
            Dim constr As String
            Dim tmpserver() As String
            Try
                DBType = AppConfiguration.ReadProviderName
                constr = AppConfiguration.ReadConnectionString
                If DBType = "System.Data.SqlClient" Then
                    tmpserver = Split(constr, "Data Source=", 2)
                    ServerName = UCase(tmpserver(1))

                    If ServerName = "(LOCAL)" OrElse ServerName = "." OrElse ServerName.Contains(".\") OrElse ServerName.Contains("(LOCAL)\") Then
                        ServerName = Environment.MachineName
                    Else
                        tmpserver = Split(ServerName, "\", 2)
                        ServerName = tmpserver(0)
                    End If
                Else
                    'ServerName = objAppOpt.GetApplicationOptionByKeyName("RegisterServer")
                End If

                'If ServerName = "" Or ServerName = Environment.MachineName Then
                '    IService = New GWTServices.GWTService
                'Else
                '    chanTcp = New TcpChannel
                '    If ChannelServices.RegisteredChannels.Length < 1 Then
                '        ChannelServices.RegisterChannel(chanTcp, False)
                '    End If
                '    IService = DirectCast(Activator.GetObject(GetType(IGWTServices.IGWTService), "tcp://" + ServerName + ":9500/ABSPTAServiceProcess"), IGWTService)
                'End If

                'DT = IService.SystemConfiguration()
                'MachineName = Crypto.Decrypt(DT.Rows(0).Item("SystemName"))
                'Return Crypto.Decrypt(DT.Rows(0).Item("ProcessorId"))
                Return ""
            Catch ex As Exception

                Return ""
            End Try


        End Function
#End Region

    End Class
End Namespace