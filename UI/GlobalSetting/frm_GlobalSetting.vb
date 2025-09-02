Imports BusinessRule
Imports CommonInfo
Imports System.Configuration
Imports Operational.AppConfiguration

Public Class frm_GlobalSetting
    Implements IFormProcess
    Dim OpenFileDia As New OpenFileDialog
    Dim PName As String
    Dim DefaultPhoto As String
    Dim dbType As String

    Dim DBPath As String = ""
    Dim constr As String

    Dim pTrustedConnection As Boolean
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
    Dim tmpPassword2() As String
    Dim objDBConnection As New DataAccess.DBConnection.DBConnectionDAL


    Private objGlobalSettingController As GlobalSetting.IGlobalSettingController = Factory.Instance.CreateGlobalSettingController
    Private Sub frm_Measurement_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        btnAdd.Focus()
    End Sub

    Private Sub frm_GlobalSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "Global Setting"
        MyBase.btnNew.Visible = False
        MyBase.btnDelete.Visible = False

        LoadGlobalSetting()
        If chkIsUsedSetting.Checked Then
            txtInterestPeriod.Enabled = True
        Else
            txtInterestPeriod.Enabled = False
        End If
    End Sub

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()

    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim info As GlobalSettingInfo
        objGlobalSettingController.DeleteGlobalSetting()

        If IsFillData() = False Then Exit Function
        info = Get_Data()
        If objGlobalSettingController.SaveGlobalSetting(info) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function Get_Data() As CommonInfo.GlobalSettingInfo
        Dim objGlobalInfo As New CommonInfo.GlobalSettingInfo
        With objGlobalInfo
            .DatabaseSharePath = txtSharePath.Text
            .Photo = PName
            If (optNormal.Checked) Then
                .IsCarat = 0
            ElseIf (optRati.Checked) Then
                .IsCarat = 1
            Else
                .IsCarat = 2
            End If
            .IsReuseBarcode = chkReuse.Checked
            .AllowDis = IIf(txtAllowDis.Text = "", 0, txtAllowDis.Text)
            .IsCash = chkIsCash.Checked
            .IsExactPrice = chkExactPrice.Checked
            .DiffChangeRate = IIf(txtDiffChangeRate.Text = "", 0, txtDiffChangeRate.Text)
            .DiffPurchaseRate = IIf(txtDiffPurchaseRate.Text = "", 0, txtDiffPurchaseRate.Text)
            .IsSpeedEntry = chkSpeedEntry.Checked
            .DecimalFormat = cboDecimalFormat.SelectedItem
            .IsAllowUpdate = chkUpdateItem.Checked
            .InterestRate = IIf(txtInterestRate.Text = "", 0, txtInterestRate.Text)
            .InterestPeriod = IIf(txtInterestPeriod.Text = "", 0, txtInterestPeriod.Text)
            .IsUsedSettingPeriod = chkIsUsedSetting.Checked
            .EnablePaidAmount = chkEnablePaidAmount.Checked
            .IsAllowSale = chkIsAllowToControlSale.Checked
            .IsAllowSaleReturn = chkIsAllowToControlSaleReturn.Checked
            .IsAllowStock = chkIsAllowToControlStock.Checked
            .QRCode = IIf(txtQRCode.Text = "", "", txtQRCode.Text)
            .AllowEditWeight = CDec(IIf(txtWeight.Text = "", 0.0, txtWeight.Text))
            .IsOneMonthCalculation = chkOneMonthCalculation.Checked
            .OverDay = txtOverDueDay.Text
            .IsHoToBranch = chkUseTransfer.Checked
            .MachineType = cboMachineType.SelectedItem
            .Prefix = IIf(txtPrefix.Text = "", 0, txtPrefix.Text)
            .Postfix = IIf(txtPostfix.Text = "", 0, txtPostfix.Text)
            .IsHoMaster = chkHOMaster.Checked
            .SoftwareVendorSetting = chkVendorSetting.Checked
            .IsFixPrice = chkIsFixPrice.Checked
            .IsUseMember = chkIsUseMember.Checked
            .IsMemberCustomer = chkIsMemberCustomer.Checked
            .RegName = txtCompanyName.Text

        End With
        Return objGlobalInfo
    End Function

    Private Function IsFillData() As Boolean
        Dim IsBool As Boolean = True
        If txtSharePath.Text = "" Then
            MsgBox("Please Fill Server Share Path Name ", MsgBoxStyle.Information, AppName)
            IsBool = False
            Exit Function
        End If
        If chkOneMonthCalculation.Checked Then
            If Val(txtOverDueDay.Text) <= 0 Or Val(txtOverDueDay.Text) > 30 Then
                MsgBox("Please Fill Valid Day in Mortgage Box ", MsgBoxStyle.Information, AppName)
                IsBool = False
                Exit Function
            End If
        Else

        End If
        Return IsBool
    End Function
    Private Sub Clear()
        lblItemImage.Image = Nothing
        lblPhoto.Visible = True
        PName = ""
        btnAdd.Text = "Add"
        txtSharePath.Text = ""
        optNormal.Checked = True
        optCarat.Checked = False
        optRati.Checked = False
        chkReuse.Checked = False
        txtAllowDis.Text = ""
        chkIsCash.Checked = False
        chkExactPrice.Checked = False
        chkSpeedEntry.Checked = False
        txtDiffPurchaseRate.Text = "0"
        txtDiffChangeRate.Text = "0"
        'cboDecimalFormat.SelectedIndex = -1
        txtInterestRate.Text = "0"
        txtInterestPeriod.Text = "0"
        chkEnablePaidAmount.Checked = False
        chkIsAllowToControlSale.Checked = True
        chkIsAllowToControlSaleReturn.Checked = True
        chkIsAllowToControlStock.Checked = True
        txtQRCode.Text = ""
        chkIsUsedSetting.Checked = False
        chkOneMonthCalculation.Checked = False
        txtOverDueDay.Text = "0"
        txtPrefix.Text = 0
        txtPostfix.Text = 0
        chkHOMaster.Checked = False
        chkIsFixPrice.Checked = False
        chkIsMemberCustomer.Checked = False
        chkIsUseMember.Checked = False
        txtCompanyName.Text = ""
    End Sub
    Private Sub txtDiffPurchaseRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiffPurchaseRate.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtDiffChangeRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiffChangeRate.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If btnAdd.Text = "Add" Then
            If Global_PhotoPath <> "" Then

                OpenFileDia.Filter = "Image (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png;"
                '"Image (*.jpg;)|*.jpg;"
                OpenFileDia.FileName = Global_PhotoPath + "\"
                OpenFileDia.InitialDirectory = OpenFileDia.FileName
                OpenFileDia.ShowDialog()
                If OpenFileDia.FileName <> "" Then
                    If OpenFileDia.InitialDirectory = OpenFileDia.FileName Then
                        lblItemImage.Image = Nothing
                        btnAdd.Text = "Add"
                        lblPhoto.Visible = True
                        PName = ""
                        Exit Sub
                    End If
                    lblItemImage.Image = System.Drawing.Image.FromFile(OpenFileDia.FileName)
                    PName = OpenFileDia.FileName.Substring(Global_PhotoPath.Length + 1)
                    btnAdd.Text = "Remove"

                End If
                lblPhoto.Visible = False
            End If
        Else
            lblItemImage.Image = Nothing
            btnAdd.Text = "Add"
            lblPhoto.Visible = True
            PName = ""
        End If

    End Sub
    Private Sub LoadGlobalSetting()
        Dim dt As New DataTable
        Dim objGlobal As New GlobalSettingInfo
        Dim Type As String

        Type = AppConfiguration.ReadAppSettings("Type")

        If Type = "1" Then
            GRPMortgage.Visible = True
        Else
            GRPMortgage.Visible = False
        End If
        dt = objGlobalSettingController.GetAllGlobalSetting()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.NewRow
                dr = dt.Rows(i)
                If dr.Item("Photo") <> "" Then
                    Try
                        lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + dr.Item("Photo"))
                        PName = dr.Item("Photo")
                        btnAdd.Text = "Remove"
                        lblPhoto.Visible = False
                    Catch ex As Exception
                        MsgBox(ex.Message + " does not exist.")
                        lblItemImage.Image = Nothing
                        lblPhoto.Visible = True
                        PName = ""
                        btnAdd.Text = "Add"
                    End Try
                Else
                    lblItemImage.Image = Nothing
                    lblPhoto.Visible = True
                    PName = ""
                    btnAdd.Text = "Add"
                End If
                txtSharePath.Text = dr.Item("DatabaseSharePath")

                If dr.Item("IsCarat") = 0 Then
                    optNormal.Checked = True
                ElseIf dr.Item("IsCarat") = 1 Then
                    optRati.Checked = True
                Else
                    optCarat.Checked = True
                End If
                chkReuse.Checked = dr.Item("IsReuseBarcode")
                txtAllowDis.Text = dr.Item("AllowDis")
                chkIsCash.Checked = dr.Item("IsCash")
                chkExactPrice.Checked = dr.Item("IsExactPrice")
                txtDiffChangeRate.Text = dr.Item("DiffChangeRate")
                txtDiffPurchaseRate.Text = dr.Item("DiffPurchaseRate")
                chkSpeedEntry.Checked = dr.Item("IsSpeedEntry")
                'cboDecimalFormat.SelectedValue = dr.Item("DecimalFormat")
                cboDecimalFormat.SelectedIndex = dr.Item("DecimalFormat")
                chkUpdateItem.Checked = dr.Item("IsAllowUpdate")
                txtInterestPeriod.Text = dr.Item("InterestPeriod")
                chkIsUsedSetting.Checked = dr.Item("IsUsedSettingPeriod")
                txtInterestRate.Text = dr.Item("InterestRate")
                chkEnablePaidAmount.Checked = dr.Item("EnablePaidAmount")
                chkIsAllowToControlSaleReturn.Checked = dr.Item("IsAllowSaleReturn")
                chkIsAllowToControlSale.Checked = dr.Item("IsAllowSale")
                chkIsAllowToControlStock.Checked = dr.Item("IsAllowStock")
                txtQRCode.Text = dr.Item("QRCode")
                txtWeight.Text = dr.Item("AllowStockWeight")
                chkOneMonthCalculation.Checked = dr.Item("isOneMonthCalculation")
                If chkOneMonthCalculation.Checked Then
                    txtOverDueDay.Enabled = True
                    txtOverDueDay.Text = dr.Item("OverDay")
                Else
                    txtOverDueDay.Enabled = False
                    txtOverDueDay.Text = dr.Item("OverDay")
                End If
                chkUseTransfer.Checked = dr.Item("IsHoToBranch")
                cboMachineType.SelectedItem = dr.Item("MachineType")
                txtPrefix.Text = dr.Item("Prefix")
                txtPostfix.Text = dr.Item("Postfix")
                chkHOMaster.Checked = dr.Item("IsHoMaster")
                chkVendorSetting.Checked = dr.Item("SoftwareVendorSetting")
                chkIsFixPrice.Checked = dr.Item("IsFixPrice")
                chkIsUseMember.Checked = dr.Item("IsUseMember")
                chkIsMemberCustomer.Checked = dr.Item("IsMemberCustomer")
                txtCompanyName.Text = dr.Item("RegName")
            Next
        Else
            Clear()
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("GlobalSetting")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtAllowDis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAllowDis.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    'Private Sub cboDecimalFormat_TextChanged(sender As Object, e As EventArgs)
    '    If cboDecimalFormat.Text = "0" Then
    '        txtDecimalFormat.Text = "12345"
    '    ElseIf cboDecimalFormat.Text = "1" Then
    '        txtDecimalFormat.Text = "12345.6"
    '    ElseIf cboDecimalFormat.Text = "2" Then
    '        txtDecimalFormat.Text = "12345.67"
    '    ElseIf cboDecimalFormat.Text = "3" Then
    '        txtDecimalFormat.Text = "12345.678"
    '    ElseIf cboDecimalFormat.Text = "4" Then
    '        txtDecimalFormat.Text = "12345.6789"
    '    End If
    'End Sub


    Private Sub cboDecimalFormat_TextChanged(sender As Object, e As EventArgs) Handles cboDecimalFormat.TextChanged
        If cboDecimalFormat.Text = "0" Then
            txtDecimalFormat.Text = "12345"
        ElseIf cboDecimalFormat.Text = "1" Then
            txtDecimalFormat.Text = "12345.6"
        ElseIf cboDecimalFormat.Text = "2" Then
            txtDecimalFormat.Text = "12345.67"
        ElseIf cboDecimalFormat.Text = "3" Then
            txtDecimalFormat.Text = "12345.678"
        ElseIf cboDecimalFormat.Text = "4" Then
            txtDecimalFormat.Text = "12345.6789"
        End If
    End Sub

    Private Sub chkIsUsedSetting_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsUsedSetting.CheckedChanged
        If chkIsUsedSetting.Checked Then
            txtInterestPeriod.Enabled = True
            If CInt(txtInterestPeriod.Text) <= 0 Then
                txtInterestPeriod.Text = 3
            End If
        Else
            txtInterestPeriod.Enabled = False
        End If
    End Sub

    Private Sub btnClearTransaction_Click(sender As Object, e As EventArgs) Handles btnClearTransaction.Click
        If MsgBox("Are you sure to clear All Transactions? ", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.Yes Then
            GetServerConnection()
            If IO.File.Exists(Application.StartupPath & "\Script\ClearAllTransactions.Sql") Then
                objDBConnection.UpdateDatabase(Application.StartupPath & "\Script\ClearAllTransactions.sql", SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection)
                MsgBox("Successful Clear All Transactions!", MsgBoxStyle.Information, AppName)
            End If
        End If
    End Sub
    Private Sub btnResetDatabase_Click(sender As Object, e As EventArgs) Handles btnResetDatabase.Click
        If MsgBox("Are you sure to Reset Database? ", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.Yes Then
            GetServerConnection()
            If IO.File.Exists(Application.StartupPath & "\Script\ResetDatabase.Sql") Then
                objDBConnection.UpdateDatabase(Application.StartupPath & "\Script\ResetDatabase.sql", SQLDatabase, ServerName, SQLuser, SQLPassword, pTrustedConnection)
                MsgBox("Successful Reset Database!", MsgBoxStyle.Information, AppName)
            End If
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

    Private Sub chkOneMonthCalculation_CheckedChanged(sender As Object, e As EventArgs) Handles chkOneMonthCalculation.CheckedChanged
        If chkOneMonthCalculation.Checked Then
            txtOverDueDay.Enabled = True
        Else
            txtOverDueDay.Enabled = False
        End If
    End Sub
End Class
