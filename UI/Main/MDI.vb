Imports System.Windows.Forms
Imports BusinessRule
Imports BusinessRule.UserManagement
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

Public Class MDI

    Private _GenerateFormatController As GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Dim _GlobalInfoController As BusinessRule.GlobalSetting.IGlobalSettingController = BusinessRule.Factory.Instance.CreateGlobalSettingController
    Public isRet As Boolean = False
    Dim constr As String
    Dim DBPath As String = ""
    Public Type As String = ""
#Region " Private Methods "

    Public Function CheckMenu(ByVal Counts As Integer, ByVal FormName As String) As Boolean

        Dim i As Integer
        Dim CheckForm As Boolean
        CheckForm = True
        If Me.ActiveMdiChild Is Nothing Then
            CheckForm = False
        Else
            If Counts > 0 Then
                CheckForm = False
                For i = 0 To Counts - 1.0R
                    If Me.MdiChildren(i).Name = FormName Then
                        'Me.MdiChildren(i).Dispose()
                        CheckForm = True
                        Exit For
                    End If
                Next
            End If
        End If
        CheckMenu = CheckForm
    End Function
    Public Function CheckForm(ByVal FormName As String) As Boolean

        Dim i As Integer
        Dim childformcount As Integer = Me.MdiChildren.Length
        If Me.ActiveMdiChild Is Nothing Then
            Return False
        Else
            If childformcount > 0 Then
                For i = 0 To childformcount - 1
                    If Me.MdiChildren(i).Name = FormName Then
                        Return True
                    End If
                Next
            End If
            Return False
        End If
    End Function

    Public Sub OpenForm(ByVal Form As Form, Optional ByVal WindowState As System.Windows.Forms.FormWindowState = FormWindowState.Maximized)

        If Not CheckForm(Form.Name) Then
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
            Form.MdiParent = Me

            Form.StartPosition = FormStartPosition.CenterScreen
            Form.WindowState = WindowState

            Form.Show()
            Me.Cursor = Windows.Forms.Cursors.Default
        Else
            Form.BringToFront()
        End If
    End Sub

    Protected Sub SetFormTitle(ByVal WindowState As Integer, Optional ByVal itemTitle As String = "")
        Dim sep As String = " - "
        Dim winT As String = AppName
        '        Dim objReg As New BLGeneral.Register.Register(SubMain.ProcessorID, Global_UserID)

        If itemTitle = "" Then
            Me.Text = winT '& IIf(objReg.LicenseStatus = CommonInfo.EnumClass.LicenseStatus.UnRegistered, sep & appDemo, "")
        ElseIf itemTitle <> "" Then
            Me.Text = winT & IIf(WindowState <> FormWindowState.Maximized, sep & itemTitle, "") 'IIf(objReg.LicenseStatus = CommonInfo.EnumClass.LicenseStatus.UnRegistered, sep & appDemo, "") & IIf(WindowState <> FormWindowState.Maximized, sep & itemTitle, "")
        Else
            Me.Text = winT
        End If
    End Sub
#End Region



    Private Sub MDI_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If isRet = False Then

            If (MessageBox.Show("Sure to Exit from this system", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)) = DialogResult.No Then
                e.Cancel = True
            Else

                If (MessageBox.Show("Do you want to Backup Database? If you want, please click 'Yes' Or if you don't, please click 'No'", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)) = DialogResult.No Then
                    frm_GE_Splash.Dispose()
                    Global.System.Windows.Forms.Application.Exit()
                Else

                    Dim cmd As SqlCommand
                    Dim con As SqlConnection
                    Dim _FileName As String = ""
                    Dim _FilePath As String = ""
                    Dim _tmpFile As String = ""
                    Dim DBName As String = ""
                    Dim StrFileName As String = ""
                    Dim tmpDBPath As String = ""

                    Dim GlobalInfo As New CommonInfo.GlobalSettingInfo
                    GlobalInfo = _GlobalInfoController.GetAllGlobalSettingInfo()
                    If IsNothing(GlobalInfo.DatabaseSharePath) Or GlobalInfo.DatabaseSharePath = "" Then
                        tmpDBPath = ""
                    Else
                        tmpDBPath = GlobalInfo.DatabaseSharePath & "\"
                    End If
                    DBPath = tmpDBPath

                    Operational.FileDialog.FileDialogue.FileExtension = "bak"
                    StrFileName = Operational.FileDialog.FileDialogue.SaveAsFile

                    If StrFileName = "" Then
                        MessageBox.Show("You have to select Database Path", "Choose path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    'cls_DisplayMessage("Starting Backup Process...")
                    _FilePath = StrFileName.Substring(0, StrFileName.LastIndexOf("\") + 1)
                    _FileName = StrFileName.Substring(StrFileName.LastIndexOf("\") + 1)
                    _tmpFile = Format(DateAndTime.Now, "yyyy MM dd hh mm ss").ToString & ".bak"
                    constr = AppConfiguration.ReadConnectionString
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
                        'cls_DisplayMessage("Finished Backup Process...")
                        MessageBox.Show("Database Backup Complete", AppName & " : Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        'cls_DisplayMessage(ex.Message)
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        MsgBox(ex.Message)
                    End Try

                    frm_GE_Splash.Dispose()
                    Global.System.Windows.Forms.Application.Exit()

                End If

            End If

         
        End If
    End Sub
    Private Sub MDIPOS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Hide()
        frm_GE_Splash.ShowDialog()
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "Welcome To " + AppName
        Me.BackColor = MDI_BACK_COLOR
        Me.BackgroundImageLayout = MDI_BACK_IMAGE_LAYOUT
        Me.Show()

        Dim UserMenuControl As New MenuBuilder
        Type = AppConfiguration.ReadAppSettings("Type")
        If Type = "1" Then
            mnu_GSM_MortgageReceive.Tag = "mnu_GSM_MortgageReceive"
            mnu_GSM_MortageInterest.Tag = "mnu_GSM_MortgageInterest"
            mnu_GSM_MortagePayback.Tag = "mnu_GSM_MortgagePayback"
            mnu_GSM_MortageReturn.Tag = "mnu_GSM_MortgageReturn"
            mnu_GSM_MortgagReport.Tag = "mnu_GSM_MortgagReport"
            mnu_GSM_MortageDisable.Tag = "mnu_GSM_MortgagDisable"

        End If
        UserMenuControl.UserMenuLevelCheck(MainMenu1, Global_UserLevel, Global_UserLevelID, PackageType)

      
    End Sub

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer = 0

    Private Sub mnu_GSM_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_Location.Click
        OpenForm(frm_Location, FormWindowState.Normal)
    End Sub



    Private Sub mnu_GSM_SalesStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_SalesStaff.Click
        OpenForm(frm_Staff, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_GoldQuality_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_GoldQuality.Click
        OpenForm(frm_GoldQuality, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_ItemCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_ItemCategory.Click
        OpenForm(frm_ItemCategory, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_ItemName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_ItemName.Click
        OpenForm(frm_ItemName, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_GemsCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_GemsCategory.Click
        OpenForm(frm_GemsCategory, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_SalesItemSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_SalesItemSetup.Click
     

        OpenForm(frm_SaleItemSetupWithVolume, FormWindowState.Normal)
    End Sub

    'Private Sub mnu_GSM_BranchItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'OpenForm(frm_BranchTransfer, FormWindowState.Normal)
    'End Sub

    'Private Sub mnu_GSM_ReAddedItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    OpenForm(frm_ReAddedItem, FormWindowState.Normal)
    'End Sub

    'Private Sub mnu_GSM_PurchaseGems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    OpenForm(frm_PurchaseGems, FormWindowState.Normal)
    'End Sub

    'Private Sub mnu_GSM_DamageItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    OpenForm(frm_DamageItem, FormWindowState.Normal)
    'End Sub

    Private Sub mnu_GSM_PurchaseInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_PurchaseInvoice.Click
        'OpenForm(frm_PurchaseInvoice, FormWindowState.Normal)
        'OpenForm(frm_PurchaseItem, FormWindowState.Normal)
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(CommonInfo.EnumSetting.TableType.PurchaseStock.ToString)
        If objGenerateFormat.GenerateFormatID <> 0 Then

        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
            OpenForm(frm_GenerateFomat, FormWindowState.Normal)
            Exit Sub
        End If
        OpenForm(frm_PurchaseRow, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_SalesGems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_SalesGems.Click
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(CommonInfo.EnumSetting.TableType.SalesGem.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then

        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
            OpenForm(frm_GenerateFomat, FormWindowState.Normal)
            Exit Sub
        End If

        OpenForm(frm_SalesGems, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_SalesInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_SalesInvoice.Click
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(CommonInfo.EnumSetting.TableType.SaleStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then

        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
            OpenForm(frm_GenerateFomat, FormWindowState.Normal)
            Exit Sub
        End If

        OpenForm(frm_SaleItemInvoice, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_OrderInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_OrderInvoice.Click
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(CommonInfo.EnumSetting.TableType.OrderStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then

        Else
            MsgBox("Please Fill the format for this form at Generate Format Form First", MsgBoxStyle.Information, AppName)
            OpenForm(frm_GenerateFomat, FormWindowState.Normal)
            Exit Sub
        End If


        OpenForm(frm_NewOrderInvoice, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_OrderInvoiceRet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_OrderInvoiceRet.Click
        OpenForm(frm_OrderInvoiceReturn, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_CashReceiptOnCredit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_CashReceiptOnCredit.Click
        OpenForm(frm_CashReceiptOnCredit, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_Customer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_Customer.Click
        OpenForm(frm_Customer, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_DailyExpense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_DailyExpense.Click
        OpenForm(frm_DailyExpense, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_DailyIncome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_DailyIncome.Click
        OpenForm(frm_DailyIncome, FormWindowState.Normal)
    End Sub
    Private Sub mnu_GSM_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_Exit.Click
        Me.Close()
    End Sub
    Private Sub mnu_GSM_rptOrderInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_rptOrderInvoice.Click
        OpenForm(frm_rpt_OrderInvoice, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_rptStockItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenForm(frm_rpt_SaleItem, FormWindowState.Maximized)
    End Sub
    Private Sub mnu_GSM_Logoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_Logoff.Click
        Dim formColl As New FormCollection()
        formColl = Application.OpenForms()
        Dim count As Int32 = formColl.Count()
        Dim i As Integer = 0
        For i = 0 To formColl.Count - 1
            If formColl.Count > 1 Then
                If formColl(i).Name <> "MDI" Then
                    formColl(i).Close()
                    i = i - 1
                End If
            End If
        Next
        MDIPOS_Load(sender, e)
    End Sub
    Private Sub mnu_SMS_DatabaseUtilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_DatabaseUtilities.Click
        OpenForm(frm_GE_DBConnection, FormWindowState.Normal)
    End Sub
    Private Sub mnu_SMS_Users_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_Users.Click
        OpenForm(frm_User, FormWindowState.Normal)
    End Sub

    Private Sub mnu_SMS_UserMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_UserMenu.Click
        OpenForm(frm_UserMenu, FormWindowState.Maximized)
    End Sub
    Private Sub mnu_GSM_rptDailyIncomeExpense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_rptDailyIncomeExpense.Click
        OpenForm(frm_rpt_DailyExpenseIncome, FormWindowState.Maximized)
    End Sub
    Private Sub mnu_GSM_PhotoPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_GSM_PhotoPath.Click
        OpenForm(frm_PhotoPathConfig, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_EventLogs_Click(sender As Object, e As EventArgs) Handles mnu_GSM_EventLogs.Click
        OpenForm(frm_GE_EventLogs, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_SalesInvoiceVolume_Click(sender As Object, e As EventArgs) Handles mnu_GSM_SalesInvoiceVolume.Click
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(CommonInfo.EnumSetting.TableType.SaleVolumeStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
            OpenForm(frm_GenerateFomat, FormWindowState.Normal)
            Exit Sub
        End If

        OpenForm(frm_SaleVolumeInvoice, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_Measurement_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Measurement.Click
        OpenForm(frm_Measurement, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_CurrentPrice_Click(sender As Object, e As EventArgs) Handles mnu_GSM_CurrentPrice.Click
        OpenForm(frm_CurrentPrice, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_GenerateFormat_Click(sender As Object, e As EventArgs) Handles mnu_GSM_GenerateFormat.Click
        OpenForm(frm_GenerateFomat, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_BarcodeSetting_Click(sender As Object, e As EventArgs) Handles mnu_GSM_BarcodeSetting.Click
        OpenForm(frm_NewBarcodeSetting, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_VoucherSetting_Click(sender As Object, e As EventArgs) Handles mnu_GSM_VoucherSetting.Click
        OpenForm(frm_VoucherSetting, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_GlobalSetting_Click(sender As Object, e As EventArgs) Handles mnu_GSM_GlobalSetting.Click
        OpenForm(frm_GlobalSetting, FormWindowState.Normal)

    End Sub

    Private Sub mnu_GSM_BarcodeInfo_Click(sender As Object, e As EventArgs) Handles mnu_GSM_BarcodeInfo.Click
        OpenForm(frm_BarcodeInfo, FormWindowState.Normal)
    End Sub


    Private Sub mnu_GSM_rptCashReceiptonCredit_Click_1(sender As Object, e As EventArgs) Handles mnu_GSM_rptCashReceiptonCredit.Click
        OpenForm(frm_rpt_CashReceipt, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_Vouchers_Click_1(sender As Object, e As EventArgs)
        OpenForm(frm_rpt_Voucher, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_CompanyProfile_Click(sender As Object, e As EventArgs) Handles mnu_GSM_CompanyProfile.Click
        OpenForm(frm_CompanyProfile, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_CustomReport_Click_1(sender As Object, e As EventArgs) Handles mnu_GSM_CustomReport.Click
        OpenForm(frm_CustomReport, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_rpt_CustomReport_Click(sender As Object, e As EventArgs)
        OpenForm(frm_rpt_CustomReport, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_Help_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Help.Click
        Process.Start(My.Application.Info.DirectoryPath & "\Files\User Manual.pdf")
    End Sub

    Private Sub mnu_GSM_TermsAndContidions_Click(sender As Object, e As EventArgs) Handles mnu_GSM_TermsAndContidions.Click
        Process.Start(My.Application.Info.DirectoryPath & "\Files\Terms And Condition.pdf")
    End Sub

    Private Sub mnu_GSM_rpt_MasterSetup_Click(sender As Object, e As EventArgs) Handles mnu_GSM_rpt_MasterSetup.Click
        OpenForm(frm_rpt_MasterSetup, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_rpt_Template_Click(sender As Object, e As EventArgs)
        OpenForm(frm_VoucherTemplate, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_Repair_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Repair.Click
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(CommonInfo.EnumSetting.TableType.RepairStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then

        Else
            MsgBox("Please Fill the format for this form at Generate Format Form First", MsgBoxStyle.Information, AppName)
            OpenForm(frm_GenerateFomat, FormWindowState.Normal)
            Exit Sub
        End If

        OpenForm(frm_Repair, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_RepairReturn_Click(sender As Object, e As EventArgs) Handles mnu_GSM_RepairReturn.Click

        OpenForm(frm_RepairReturn, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_Repair_Report_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Repair_Report.Click
        OpenForm(frm_rpt_RepairReport, FormWindowState.Maximized)
    End Sub

   
    Private Sub SbMnuSaleStock_Click(sender As Object, e As EventArgs) Handles submnu_GSM_SI_Report.Click
        OpenForm(frm_rpt_SaleInvoice, FormWindowState.Maximized)
    End Sub

    Private Sub submnu_GSM_SaleSummary_Report_Click(sender As Object, e As EventArgs) Handles submnu_GSM_SaleSummary_Report.Click
        OpenForm(frm_rpt_SaleInvoiceSummaryReportByDate, FormWindowState.Maximized)
    End Sub

    Private Sub submnu_GSM_SaleVolume_Report_Click(sender As Object, e As EventArgs) Handles submnu_GSM_SaleVolume_Report.Click
        OpenForm(frm_rpt_SaleInvoiceVolume, FormWindowState.Maximized)
    End Sub

    Private Sub submnu_GSM_SaleGems_Report_Click(sender As Object, e As EventArgs) Handles submnu_GSM_SaleGems_Report.Click
        OpenForm(frm_rpt_SaleGems, FormWindowState.Maximized)
    End Sub

    Private Sub mnuItem_GSM_Purchase_Rpt_Click(sender As Object, e As EventArgs) Handles mnuItem_GSM_Purchase_Rpt.Click
        OpenForm(frm_rpt_PurchaseInvoice, FormWindowState.Maximized)
    End Sub

    Private Sub mnuItem_GSM_ProfitStockItem_Rpt_Click(sender As Object, e As EventArgs) Handles mnuItem_GSM_ProfitStockItem_Rpt.Click
        OpenForm(frm_rpt_ProfitForSaleItem, FormWindowState.Maximized)
    End Sub

    Private Sub mnuItem_GSM_ProfitVolumeStock_Rpt_Click(sender As Object, e As EventArgs) Handles mnuItem_GSM_ProfitVolumeStock_Rpt.Click
        OpenForm(frm_rpt_ProfitForVolumeSaleItem, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_GeneralLedgerbyLocation_Click(sender As Object, e As EventArgs) Handles mnu_GSM_GeneralLedgerbyLocation.Click
        OpenForm(frm_GeneralLedgerByLocation, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_CashInCashOut_Report_Click(sender As Object, e As EventArgs) Handles mnu_GSM_CashInCashOut_Report.Click
        OpenForm(frm_CashInCashOut, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_DailyTransReport_Click(sender As Object, e As EventArgs) Handles mnu_GSM_DailyTransReport.Click
        OpenForm(frm_DailyTransactionReport, FormWindowState.Maximized)
    End Sub
    'Private Sub mnu_GSM_AllTransaction_Click(sender As Object, e As EventArgs) Handles mnu_GSM_AllTransaction.Click
    '    OpenForm(frm_rpt_Transaction, FormWindowState.Maximized)
    'End Sub
    Private Sub mnu_GSM_AllTransReport_Click(sender As Object, e As EventArgs) Handles mnu_GSM_AllTransReport.Click
        OpenForm(frm_CustomerTransaction, FormWindowState.Maximized)
    End Sub

    Private Sub mnuItem_GSM_PurchaseSummaryByDate_Rpt_Click(sender As Object, e As EventArgs) Handles mnuItem_GSM_PurchaseSummaryByDate_Rpt.Click
        OpenForm(frm_PurchaseSummaryReportByDate, FormWindowState.Maximized)
    End Sub
    Private Sub mnu_GSM_FAQ_Click(sender As Object, e As EventArgs) Handles mnu_GSM_FAQ.Click
        Process.Start(My.Application.Info.DirectoryPath & "\Files\GlobalGoldFAQ.pdf")
    End Sub

    Private Sub mnu_GSM_StockItemReport_Click(sender As Object, e As EventArgs) Handles mnu_GSM_StockItemReport.Click
        OpenForm(frm_rpt_SaleItem, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_StockCardReport_Click(sender As Object, e As EventArgs) Handles mnu_GSM_StockCardReport.Click
        OpenForm(frm_rpt_StockCardReport, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_CurrentGoldPrice_Click(sender As Object, e As EventArgs) Handles mnu_GSM_CurrentGoldPrice.Click
        OpenForm(frm_CurrentPriceForGold, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_StockTransactionReport_Click(sender As Object, e As EventArgs) Handles mnu_GSM_StockTransactionReport.Click
        OpenForm(frm_MonthlyStockReport, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_Supplier_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Supplier.Click
        OpenForm(frm_Supplier, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_PurchaseFromSupplier_Click(sender As Object, e As EventArgs) Handles mnu_GSM_PurchaseFromSupplier.Click
        OpenForm(frm_PurchaseFromSupplier, FormWindowState.Normal)
    End Sub
    Private Sub mnu_GSM_CashType_Click(sender As Object, e As EventArgs) Handles mnu_GSM_CashType.Click
        OpenForm(frm_CashType, FormWindowState.Normal)
    End Sub

    Private Sub mnuItem_GSM_PurchaseFromSupplier_Click(sender As Object, e As EventArgs) Handles mnuItem_GSM_PurchaseFromSupplier.Click
        OpenForm(frm_rpt_PurchaseFromSupplier, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_BranchSetup_Click(sender As Object, e As EventArgs) Handles mnu_GSM_BranchSetup.Click
        OpenForm(frm_BranchSetup, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_DivideByLocation_Click(sender As Object, e As EventArgs) Handles mnu_GSM_DivideByLocation.Click
        OpenForm(frm_DivideByLocation, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_ImportFromHO_Click(sender As Object, e As EventArgs) Handles mnu_GSM_ImportFromHO.Click
        OpenForm(frm_ImportDatabaseForBranch, FormWindowState.Normal)
    End Sub
    

    Private Sub mnu_GSM_BranchTransfer_Click(sender As Object, e As EventArgs) Handles mnu_GSM_BranchTransfer.Click
        OpenForm(frm_rpt_Transfer, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_Tax_Report_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Tax_Report.Click
        OpenForm(frm_rpt_Tax, FormWindowState.Maximized)
    End Sub

   
    Private Sub mnu_ExportData_Click(sender As Object, e As EventArgs) Handles mnu_GSM_ExportData.Click
        If Global_VendorSetting Then
            OpenForm(frm_ExportData, FormWindowState.Normal)
        End If
    End Sub

    Private Sub mnu_GSM_Waste_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Waste.Click
        OpenForm(frm_WasteSetup, FormWindowState.Normal)
    End Sub

    'Private Sub mnu_GSM_ImportMasterHo_Click(sender As Object, e As EventArgs) Handles mnu_GSM_ImportMasterHo.Click
    '    OpenForm(frm_ImportDatabase, FormWindowState.Normal)

    'End Sub

   
    Private Sub mnu_GSM_GoldSmith_Click(sender As Object, e As EventArgs) Handles mnu_GSM_GoldSmith.Click
        OpenForm(frm_GoldSmith, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_ReturnAdvance_Click(sender As Object, e As EventArgs) Handles mnu_GSM_ReturnAdvance.Click
        OpenForm(frm_ReturnAdvance, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_rptReturnAdvance_Click(sender As Object, e As EventArgs) Handles mnu_GSM_rptReturnAdvance.Click
        OpenForm(frm_rpt_ReturnAdvance, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_MortgageReceive_Click(sender As Object, e As EventArgs) Handles mnu_GSM_MortgageReceive.Click
        OpenForm(frm_MortgageReceive, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_MortageInterest_Click(sender As Object, e As EventArgs) Handles mnu_GSM_MortageInterest.Click
        OpenForm(frm_MortgageInterest, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_MortageReturn_Click(sender As Object, e As EventArgs) Handles mnu_GSM_MortageReturn.Click
        OpenForm(frm_MortgageReturn, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_MortgagReport_Click(sender As Object, e As EventArgs) Handles mnu_GSM_MortgagReport.Click
        OpenForm(frm_rpt_Mortgage, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_MortageDisable_Click(sender As Object, e As EventArgs) Handles mnu_GSM_MortageDisable.Click
        OpenForm(frm_MortgageDisable, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_MortagePayback_Click(sender As Object, e As EventArgs) Handles mnu_GSM_MortagePayback.Click
        OpenForm(frm_MortgagePayback, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_WholeSalesInvoice_Click(sender As Object, e As EventArgs) Handles mnu_GSM_WholeSalesInvoice.Click
        OpenForm(frm_Wholesales, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_WholeSalesReturn_Click(sender As Object, e As EventArgs) Handles mnu_GSM_WholeSalesReturn.Click
        OpenForm(frm_WholesalesReturn, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_ConsignmentSale_Click(sender As Object, e As EventArgs) Handles mnu_GSM_ConsignmentSale.Click
        OpenForm(frm_ConsignmentSale, FormWindowState.Normal)
    End Sub

    'Private Sub mnu_GSM_Dashboard_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Dashboard.Click
    '    OpenForm(frm_Dashboard, FormWindowState.Maximized)
    'End Sub
    Private Sub mnu_GSM_WS_Report_Click_1(sender As Object, e As EventArgs) Handles mnu_GSM_WS_Report.Click
        OpenForm(frm_rpt_WholeSaleWG, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_ExportToBranch_Click(sender As Object, e As EventArgs) Handles mnu_GSM_ExportToBranch.Click
        If Global_IsHoToBranch And Global_IsHeadOffice Then
            OpenForm(frm_BranchTransfer, FormWindowState.Normal)
        Else
            MsgBox("This transaction can use only in Ho.", vbInformation, AppName)
        End If
    End Sub
    Private Sub mnu_GSM_TransferReturn_Click(sender As Object, e As EventArgs) Handles mnu_GSM_TransferReturn.Click
        OpenForm(frm_TransferReturnToHO, FormWindowState.Maximized)
    End Sub
    Private Sub mnu_GSM_BranchTransferReturn_Click(sender As Object, e As EventArgs) Handles mnu_GSM_BranchTransferReturn.Click
        OpenForm(frm_rpt_TransferReturn, FormWindowState.Maximized)
    End Sub
    Private Sub mnu_GSM_StockChecking_Click(sender As Object, e As EventArgs) Handles mnu_GSM_StockChecking.Click
        OpenForm(frm_StockChecking, FormWindowState.Normal)
    End Sub
    Private Sub mnu_GSM_CheckStockReport_Click(sender As Object, e As EventArgs) Handles mnu_GSM_CheckStockReport.Click
        OpenForm(frm_rpt_CheckStock, FormWindowState.Maximized)
    End Sub
    Private Sub mnu_GSM_BarcodeTrack_Click(sender As Object, e As EventArgs) Handles mnu_GSM_BarcodeTrack.Click
        OpenForm(frm_rpt_BarcodeTrack, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_Dashboard_Click(sender As Object, e As EventArgs) Handles mnu_GSM_Dashboard.Click
        OpenForm(frm_Dashboard, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_rptAllCashTransactionByCustomer_Click(sender As Object, e As EventArgs) Handles mnu_GSM_rptCashTransactionReportByCustomer.Click
        OpenForm(frm_rpt_CashTransactionByCustomer, FormWindowState.Maximized)
    End Sub

    Private Sub mnu_GSM_DefineDiamondPrice_Click(sender As Object, e As EventArgs) Handles mnu_GSM_DefineDiamondPrice.Click
        OpenForm(frm_InternationalDiamondPrice, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_SaleLooseDiamond_Click(sender As Object, e As EventArgs) Handles mnu_GSM_SaleLooseDiamond.Click
        OpenForm(frm_SaleLooseDiamond, FormWindowState.Normal)
    End Sub

    Private Sub mnu_GSM_TransferDiamond_Click(sender As Object, e As EventArgs) Handles mnu_GSM_TransferDiamond.Click
        If Global_IsHoToBranch And Global_IsHeadOffice Then
            OpenForm(frm_BranchTransferDiamond, FormWindowState.Normal)
        Else
            MsgBox("This transaction can use only in Ho.", vbInformation, AppName)
        End If
    End Sub

    Private Sub mnu_GSM_TransferReturnDiamond_Click(sender As Object, e As EventArgs) Handles mnu_GSM_TransferReturnDiamond.Click
        OpenForm(frm_TransferReturnLooseDiamondToHO, FormWindowState.Normal)
    End Sub

    Private Sub submnu_GSM_SaleLooseDiamond_Report_Click(sender As Object, e As EventArgs) Handles submnu_GSM_SaleLooseDiamond_Report.Click
        OpenForm(frm_rpt_SaleInvoiceLooseDiamond, FormWindowState.Maximized)
    End Sub

    Private Sub mnuItem_GSM_ProfitLooseDiamondStock_Rpt_Click(sender As Object, e As EventArgs) Handles mnuItem_GSM_ProfitLooseDiamondStock_Rpt.Click
        OpenForm(frm_rpt_ProfitForDiamondeSaleItem, FormWindowState.Maximized)
    End Sub
End Class

