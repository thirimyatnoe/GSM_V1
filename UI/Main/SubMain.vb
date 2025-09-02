Imports BusinessRule
Imports BusinessRule.UserManagement
Imports System.Configuration
Imports Operational.AppConfiguration

Module SubMain
    'Public Const AppName = "Gold Smith Management System"  '"Global Wave" "TouchIn TouchOut" '"SilverLiners" '"Payroll and Time Attendance System"
    Public Const AppName = "TMS"
    Public Const appDemo = "[DEMO]" ' add by HWM 1/Dec 2008
    Public Const Msg_Save = "Save Successfully"
    Public Const Msg_Confirm_Del = "Sure you want to delete this record !  "
    Public Const Msg_Delete = "Delete Successfully"
    Public Const Msg_DelRec_UnFound = "Delete Record Not Found !"
    Public Const Msg_SelRec_ToDel = "Please Select a Record to Delete ! "
    Public Const PackageType = "GSM"
    Public Const AppVersion = "6.4900"

    Public sHelpFile As String = Application.StartupPath & "\POS.chm"

    ''--------------- Company --------------------------
    Public MDI_BACK_COLOR As Color = Color.FromArgb(232, 238, 250)
    Public MDI_BACK_IMAGE_LAYOUT As ImageLayout = ImageLayout.None
    Public COMPANY_URL As String = "http://www.globalwavetechnology.com"
    Public LOGO_SIZE As Size = New Size(149, 52)
    Public LOGO_POSITION As Point = New Point(9, 26)
    ''--------------- Company --------------------------

    Public Global_CurrentUser As String


    Public Global_UserLevel As String
    Public Global_UserLevelID As Integer
    Public Global_UserID As String
    Public Global_CurrentLocationID As String
    Public Global_CurrentLocationName As String
    Public Global_KyatToGram As Decimal
    Public Global_GramToKarat As Decimal
    Public Global_KaratToYati As Decimal
    Public Global_YatiToB As Decimal
    Public Global_BToP As Decimal
    Public Global_PToY As Decimal
    Public Global_PhotoPath As String
    ''
    Public Global_PaperHeight As String
    Public Global_PaperWidth As String
    Public Global_X As String
    Public Global_Y As String
    Public Global_Height As String
    Public Global_Narrow As String
    Public Global_Wide As String
    Public Global_PrinterName As String
    Public Global_LogoPhoto As String
    Public Global_IsCarat As Integer
    Public Global_IsReuseBarcode As Boolean
    Public Global_IsLocationName As Boolean
    Public Global_ShopName As String
    Public Global_ShopMMName As String
    Public Global_RightPosX As String
    Public Global_IsGemWgt As Boolean
    Public Global_IsGemPrice As Boolean
    Public Global_VisualStyle As Integer
    Public Global_IsShowGW As Boolean

    'Add New Fields for Barcode Setting (04.06.2014)
    Public Global_IsPrefix As Boolean
    Public Global_IsLength As Boolean
    Public Global_IsFixItem As Boolean
    Public Global_IsFixGold As Boolean
    Public Global_IsFixGemQTY As Boolean
    Public Global_IsFixGemWeight As Boolean
    Public Global_IsDetail As Boolean
    Public Global_IsOriginalCode As Boolean
    Public Global_IsPriceCode As Boolean
    Public Global_IsDescription As Boolean
    Public Global_IsWaste As Boolean
    Public Global_IsShowGram As Boolean
    Public Global_IsCash As Boolean
    Public Global_IsSpeedEntry As Boolean
    Public Global_DecimalFormat As Integer


    Public CompanyMode As String ' = "Single"
    Public _TimeZone As String
    Public ProcessorID As String = ""
    Public Company_HOID As String
    Public Company_HOName As String
    Public Company_HOAddress As String
    Public Company_HOPhone As String
    Public DisplayBoxCompanyName As String
    Public Global_LicensedEmployee As Integer = 0

    Public Global_AllowDis As Integer = 0
    Public Global_IsAllowUpdate As Boolean
    Public Global_InterestRate As Integer
    Public Global_InterestPeriod As Integer
    Public Global_IsUsedSettingPeriod As Boolean
    Public Global_EnablePaidAmount As Boolean
    Public Global_IsAllowSale As Boolean
    Public Global_IsAllowSaleReturn As Boolean
    Public Global_IsAllowStock As Boolean
    Public Global_AllowEditWeight As Decimal
    Public Global_IsFixPrice As Boolean
    ''
    Public Global_QRCode As String
    Public Global_LeftFontSize As Integer
    Public Global_RightFontSize As Integer
    Public Global_IsHeadOffice As Boolean
    Public Global_OneMonthCalculation As Boolean
    Public Global_OverDueday As Integer
    Public Global_IsHoToBranch As Boolean
    Public Global_MachineType As String
    Public Global_Prefix As Integer
    Public Global_Postfix As Integer
    Public Global_IsHoMaster As Boolean
    Public Global_VendorSetting As Boolean
    Public Global_GIsFixPrice As Boolean
    Public Global_IsUseMember As Boolean
    Public Global_IsMemberCustomer As Boolean
    Public Global_CompanyName As String
    Public Global_MemberConstant As String = "GwtMember@2020"

    Public Global_ExpireDate As Nullable(Of Date) = "2012/12/01" 'Nothing or yyyy/MM/dd

    Public Function CheckDatabaseSetting() As Boolean
        Dim clsDBConBL As New DBConnection.DBConnection
        Return clsDBConBL.IsExistsConfiguration()
        'Return clsGeneral.clsDBConnection.IsDataBaseServerExist
    End Function

    Public Function Login() As Boolean
        Dim RBool As Boolean = False
        Dim frm As New UI.frmLogin

        frm.ShowDialog()

        If frm.LoginSuccess = True Then
            'If IsFinishTried() = True Then
            Global_UserID = frm.CurrentUser
            Global_UserLevel = frm.CurrentuserLevel
            Global_UserLevelID = frm.CurrentuserLevelID
            Dim _objLocation As Location.ILocationController = Factory.Instance.CreateLocationController
            Dim _objConverter As Converter.IConverterController = Factory.Instance.CreateConverterController
            Dim _objPhotoPath As PhotoPath.IPhotoPathController = Factory.Instance.CreatePhotoPathController
            Dim _objBarcodeSetting As Barcodesetting.IBarcodeSettingController = Factory.Instance.CreateBarcodeSettingController
            Dim _objGlobalSettingCon As GlobalSetting.IGlobalSettingController = Factory.Instance.CreateGlobalSettingController


            ' Dim id As String = _objLocation.GetCurrentLocationID
            Dim id As String = AppConfiguration.ReadAppSettings("CurrentLocationID")


            'Add new Feature for Barcode Setting
            Dim dt As New DataTable
            dt = _objBarcodeSetting.GetBarcode()
            If dt.Rows.Count() > 0 Then
                Global_PaperHeight = dt.Rows(0).Item("PaperHeight").ToString()
                Global_PaperWidth = dt.Rows(0).Item("PaperWidth").ToString()
                Global_X = dt.Rows(0).Item("X").ToString()
                Global_Y = dt.Rows(0).Item("Y").ToString()
                Global_Height = dt.Rows(0).Item("Height").ToString()
                Global_Narrow = dt.Rows(0).Item("Narrow").ToString()
                Global_Wide = dt.Rows(0).Item("Wide").ToString()
                Global_PrinterName = dt.Rows(0).Item("PrinterName").ToString()
                Global_IsLocationName = dt.Rows(0).Item("IsLocationName")
                Global_RightPosX = dt.Rows(0).Item("RightPositionX").ToString()
                Global_IsGemWgt = dt.Rows(0).Item("IsIncludeGemWgt")
                Global_IsGemPrice = dt.Rows(0).Item("IsIncludeGemPrice")
                Global_IsOriginalCode = dt.Rows(0).Item("IsOriginalCode")
                Global_IsPriceCode = dt.Rows(0).Item("IsPriceCode")
                Global_IsWaste = dt.Rows(0).Item("IsWaste")
                Global_IsDescription = dt.Rows(0).Item("IsDescription")
                Global_IsShowGram = dt.Rows(0).Item("IsGram")
                Global_IsShowGW = IIf(IsDBNull(dt.Rows(0).Item("IsShowGW")), 0, dt.Rows(0).Item("IsShowGW"))
                'add new fields at 04.06.2014
                Global_IsPrefix = dt.Rows(0).Item("IsPrefix")
                Global_IsLength = dt.Rows(0).Item("IsLength")
                Global_IsFixItem = dt.Rows(0).Item("IsFixItem")
                Global_IsFixGold = dt.Rows(0).Item("IsFixGold")
                Global_IsFixGemQTY = dt.Rows(0).Item("IsFixGemQTY")
                Global_IsFixGemWeight = dt.Rows(0).Item("IsFixGemWeight")
                Global_IsDetail = dt.Rows(0).Item("IsAllDetail")
                Global_IsFixPrice = dt.Rows(0).Item("IsFixPrice")
                Global_LeftFontSize = dt.Rows(0).Item("LeftFontSize")
                Global_RightFontSize = dt.Rows(0).Item("RightFontSize")

                If dt.Rows(0).Item("EngName").ToString() <> "" Then
                    Global_ShopName = dt.Rows(0).Item("EngName").ToString()
                ElseIf dt.Rows(0).Item("MMName").ToString() <> "" Then
                    Global_ShopMMName = dt.Rows(0).Item("MMName").ToString()
                End If
            Else
                Global_PaperHeight = "0"
                Global_PaperWidth = "0"
                Global_X = "0"
                Global_Y = "0"
                Global_Height = "0"
                Global_Narrow = "0"
                Global_Wide = "0"
                Global_PrinterName = "0"
                Global_RightFontSize = "19"
                Global_LeftFontSize = "19"
            End If

            'End
            '' For GlobalSetting
            Dim dtGlobal As New DataTable
            dtGlobal = _objGlobalSettingCon.GetAllGlobalSetting()
            If dtGlobal.Rows.Count() > 0 Then
                Global_LogoPhoto = dtGlobal.Rows(0).Item("Photo").ToString()
                    Global_IsCarat = dtGlobal.Rows(0).Item("IsCarat")
                    Global_IsCarat = dtGlobal.Rows(0).Item("IsCarat")
                    Global_IsReuseBarcode = dtGlobal.Rows(0).Item("IsReuseBarcode")
                    Global_AllowDis = dtGlobal.Rows(0).Item("AllowDis")
                    Global_IsCash = dtGlobal.Rows(0).Item("IsCash")
                    Global_IsSpeedEntry = dtGlobal.Rows(0).Item("IsSpeedEntry")
                    Global_DecimalFormat = dtGlobal.Rows(0).Item("DecimalFormat")
                    Global_IsAllowUpdate = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsAllowUpdate")), 0, dtGlobal.Rows(0).Item("IsAllowUpdate"))
                    Global_InterestPeriod = IIf(IsDBNull(dtGlobal.Rows(0).Item("InterestPeriod")), 0, dtGlobal.Rows(0).Item("InterestPeriod"))
                    Global_IsUsedSettingPeriod = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsUsedSettingPeriod")), 0, dtGlobal.Rows(0).Item("IsUsedSettingPeriod"))
                    Global_InterestRate = dtGlobal.Rows(0).Item("InterestRate")
                    Global_EnablePaidAmount = dtGlobal.Rows(0).Item("EnablePaidAmount")
                    Global_IsAllowSaleReturn = dtGlobal.Rows(0).Item("IsAllowSaleReturn")
                    Global_IsAllowSale = dtGlobal.Rows(0).Item("IsAllowSale")
                    Global_IsAllowStock = dtGlobal.Rows(0).Item("IsAllowStock")
                    Global_QRCode = dtGlobal.Rows(0).Item("QRCode")
                    Global_AllowEditWeight = dtGlobal.Rows(0).Item("AllowStockWeight")
                    Global_OneMonthCalculation = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsOneMonthCalculation")), 0, dtGlobal.Rows(0).Item("IsOneMonthCalculation"))
                    Global_OverDueday = IIf(IsDBNull(dtGlobal.Rows(0).Item("OverDay")), 0, dtGlobal.Rows(0).Item("OverDay"))
                    Global_IsHoToBranch = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsHoToBranch")), 0, dtGlobal.Rows(0).Item("IsHoToBranch"))
                    Global_MachineType = IIf(IsDBNull(dtGlobal.Rows(0).Item("MachineType")), "", dtGlobal.Rows(0).Item("MachineType"))
                    Global_Prefix = IIf(IsDBNull(dtGlobal.Rows(0).Item("Prefix")), 0, dtGlobal.Rows(0).Item("Prefix"))
                    Global_Postfix = IIf(IsDBNull(dtGlobal.Rows(0).Item("Postfix")), 0, dtGlobal.Rows(0).Item("Postfix"))
                    Global_IsHoMaster = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsHoMaster")), 0, dtGlobal.Rows(0).Item("IsHoMaster"))
                Global_VendorSetting = IIf(IsDBNull(dtGlobal.Rows(0).Item("SoftwareVendorSetting")), 0, dtGlobal.Rows(0).Item("SoftwareVendorSetting"))
                Global_GIsFixPrice = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsFixPrice")), 0, dtGlobal.Rows(0).Item("IsFixPrice"))
                Global_IsUseMember = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsUseMember")), 0, dtGlobal.Rows(0).Item("IsUseMember"))
                Global_IsMemberCustomer = IIf(IsDBNull(dtGlobal.Rows(0).Item("IsMemberCustomer")), 0, dtGlobal.Rows(0).Item("IsMemberCustomer"))
                Global_CompanyName = IIf(IsDBNull(dtGlobal.Rows(0).Item("RegName")), "", dtGlobal.Rows(0).Item("RegName"))

                Else
                    Global_LogoPhoto = ""
                    Global_IsCarat = 0
                    Global_IsReuseBarcode = False
                    Global_AllowDis = 0
                    Global_IsCash = False
                    Global_IsSpeedEntry = False
                    Global_DecimalFormat = 1
                    Global_IsAllowUpdate = False
                    Global_InterestPeriod = 0
                    Global_IsUsedSettingPeriod = 0
                    Global_InterestRate = 1
                    Global_EnablePaidAmount = False
                    Global_IsAllowSaleReturn = True
                    Global_IsAllowSale = True
                    Global_IsAllowStock = True
                    Global_QRCode = ""
                    Global_AllowEditWeight = 0.0
                    Global_OneMonthCalculation = False
                    Global_OverDueday = 0
                    Global_IsHoToBranch = 0
                    Global_MachineType = ""
                    Global_Prefix = 0
                    Global_Postfix = 0
                    Global_IsHoMaster = 1
                Global_VendorSetting = 0
                Global_GIsFixPrice = 0
                Global_IsUseMember = 0
                Global_IsMemberCustomer = 0
                Global_CompanyName = ""
                End If

                'Edit Oct25,2012
                Global_KyatToGram = _objConverter.GetMeasurement("Kyat", "Gram")
                Global_GramToKarat = _objConverter.GetMeasurement("Gram", "Karat")
                Global_KaratToYati = _objConverter.GetMeasurement("Karat", "Yati")
                Global_YatiToB = _objConverter.GetMeasurement("Yati", "B")
                Global_BToP = _objConverter.GetMeasurement("B", "P")
                Global_PToY = Format(_objConverter.GetMeasurement("P", "Y"), "0.0")



                Dim infoLocation As CommonInfo.LocationInfo = _objLocation.GetLocationByID(id)
                Dim str As String = infoLocation.Location
                Global_CurrentLocationID = id
                Global_CurrentLocationName = infoLocation.Location
                Global_IsHeadOffice = infoLocation.IsHeadOffice


                Global_PhotoPath = AppConfiguration.ReadAppSettings("PhotoPath")
                BusinessRule.BLGeneralClass.Set_Global_UserID(Global_CurrentUser, Global_CurrentLocationID, Global_IsReuseBarcode, Global_IsCash)
                BusinessRule.BLGeneralClass.Set_Global_GoldWeight(Global_KyatToGram, Global_GramToKarat, Global_KaratToYati, Global_YatiToB, Global_BToP, Global_PToY)
                RBool = True
                frm.Dispose()
            Else
                RBool = False
            End If
        Return RBool
    End Function

    Public Function ServerConfiguration() As Boolean
        Dim fDBCon As New frm_GE_DBConnection
        fDBCon.isMain = True
        fDBCon.ShowDialog()
        If fDBCon.Connect = False Then
            'MsgBox("Could not connect to Database", MsgBoxStyle.Exclamation, AppName)
            fDBCon.Dispose()
            fDBCon = Nothing
            Return False
        Else
            fDBCon.Dispose()
            fDBCon = Nothing
            Return True
        End If

    End Function
    Public Function TimeDiff(ByVal Time1 As Date, ByVal Time2 As Date, ByVal RetVal As DateInterval) As Double

        Time1 = Today & " " & Format(Time1, "hh:mm:ss tt")
        Time2 = Today & " " & Format(Time2, "hh:mm:ss tt")

        If CDate(Time1) > CDate(Time2) Then
            TimeDiff = DateDiff(RetVal, Time1, DateAdd(DateInterval.Day, 1, Time2))
        Else
            TimeDiff = DateDiff(RetVal, Time1, Time2)
        End If

    End Function

    Public Enum IsInitIstallStatus
        SQL = 0
        Access = 1
        ExistDB = 2
    End Enum

    Public Function IsMainMenuItemExist(ByVal CheckMenu As MainMenu, ByVal MenuName As String) As Boolean
        Dim bolReturn As Boolean = False
        For Each CheckMenuItem As MenuItem In CheckMenu.MenuItems
            If IsMenuItemExist(CheckMenuItem, MenuName) Then
                bolReturn = True
                Exit For
            End If
        Next
        Return bolReturn
    End Function

    Private Function IsMenuItemExist(ByVal checkMenuItem As MenuItem, ByVal MenuName As String) As Boolean
        Dim UserMenuControl As New MenuBuilder

        Dim bolReturn As Boolean = False
        If checkMenuItem IsNot Nothing Then
            For I As Integer = 0 To checkMenuItem.MenuItems.Count - 1
                If checkMenuItem.MenuItems.Item(I).Break = False Then
                    If checkMenuItem.MenuItems.Item(I).Tag = MenuName Then
                        If UserMenuControl.UserLevelMenuNameCheck(Global_UserLevelID, Global_UserLevel, checkMenuItem.MenuItems.Item(I).Text) Then
                            bolReturn = True
                            Exit For
                        End If

                    End If
                Else
                    IsMenuItemExist(checkMenuItem.MenuItems.Item(I), MenuName)
                End If
            Next
        End If
        Return bolReturn
    End Function


    Public Function InitInstallationServer() As Boolean
        Dim ServerInstall As New frm_GE_Server
        Dim bolReturn As Boolean
        ServerInstall.ShowDialog()
        bolReturn = ServerInstall.Connect
        ServerInstall.Dispose()
        Return bolReturn
    End Function


    Public Sub GetCompanyProfile()

        Dim CurrentCompanyID As String = AppConfiguration.ReadAppSettings("CurrentCompanyID")
        'Dim CurrentCompanyID As String = config.AppSettings.Settings("CurrentCompanyID").Value

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

        End If

    End Sub


End Module
