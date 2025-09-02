Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
'Imports System.String
Imports System.Windows
Imports Operational.AppConfiguration
Public Class frm_ReturnAdvance

    Implements IFormProcess
    Private _ReturnAdvanceID As String
    Private _ReturnAdvanceItemID As String
    Private _StaffID As String
    Private _LocationID As String
    Private _GemsCategoryID As String
    Private GemTK As Decimal = 0.0
    Private GemTG As Decimal = 0.0
    Private GemTW As Decimal = 0.0
    Private TotalTK As Decimal = 0.0
    Private TotalTW As Decimal = 0.0
    Private _TotalTG As Decimal = 0.0
    Private _ItemTGemsTK As Decimal = 0.0
    Private _ItemTGemsTG As Decimal = 0.0
    Private _ItemTG As Decimal = 0.0

    Private _CustomerID As String = ""
    Private _CurrentQTY As Integer = 0
    Private _OldQTY As Integer = 0
    Private _IsAllowDiscount As Boolean = False
    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False

    Private _dtReturnAdvanceItem As New DataTable
    Private _Staff As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _ConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _ReturnAdvance As ReturnAdvance.IReturnAdvanceController = Factory.Instance.CreateReturnAdvanceController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Dim IsCheck As Boolean = False ' For Gems

#Region " Auto Completion ComboBox "
    Private Sub AutoCompleteCombo_KeyUp(ByVal cbo As ComboBox, ByVal e As KeyEventArgs)
        Dim sTypedText As String
        Dim iFoundIndex As Integer
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        'Allow select keys without Autocompleting

        Select Case e.KeyCode
            Case Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.Down
                Return
        End Select

        'Get the Typed Text and Find it in the list

        sTypedText = cbo.Text
        iFoundIndex = cbo.FindString(sTypedText)

        'If we found the Typed Text in the list then Autocomplete

        If iFoundIndex >= 0 Then

            'Get the Item from the list (Return Type depends if Datasource was bound 

            ' or List Created)

            oFoundItem = cbo.Items(iFoundIndex)

            'Use the ListControl.GetItemText to resolve the Name in case the Combo 

            ' was Data bound

            sFoundText = cbo.GetItemText(oFoundItem)

            'Append then found text to the typed text to preserve case

            sAppendText = sFoundText.Substring(sTypedText.Length)
            cbo.Text = sTypedText & sAppendText

            'Select the Appended Text

            If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
            If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length

        End If

    End Sub

    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox)
        Dim iFoundIndex As Integer

        iFoundIndex = cbo.FindStringExact(cbo.Text)

        cbo.SelectedIndex = iFoundIndex

    End Sub
    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox, ByVal e As String)
        'sya 2/10/2008
        Dim sTypedText As String = ""
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        Dim iFoundIndex As Integer
        iFoundIndex = cbo.FindStringExact(cbo.Text)

        If iFoundIndex = -1 Then
            sTypedText = cbo.Text
            iFoundIndex = cbo.FindString(sTypedText)

            If iFoundIndex = -1 Then
                If sTypedText.IndexOf(" ") < 0 Then
                    iFoundIndex = -1
                Else
                    sTypedText = sTypedText.Remove(sTypedText.IndexOf(" "))
                    iFoundIndex = cbo.FindString(sTypedText)
                End If
            End If

            If iFoundIndex >= 0 Then
                oFoundItem = cbo.Items(iFoundIndex)
                sFoundText = cbo.GetItemText(oFoundItem)
                sAppendText = sFoundText.Substring(sTypedText.Length)
                cbo.Text = sTypedText & sAppendText

                If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
                If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length
            End If
        Else
            cbo.SelectedIndex = iFoundIndex
        End If

        If iFoundIndex = -1 Then
            For i As Integer = 0 To cbo.Items.Count - 1
                oFoundItem = cbo.Items(i)
                sFoundText = cbo.GetItemText(oFoundItem)

                If sFoundText.IndexOf(" ") < 0 Then
                    iFoundIndex = -1
                Else
                    sFoundText = sFoundText.Remove(sFoundText.IndexOf(" "))
                    iFoundIndex = cbo.FindString(sFoundText)
                End If

                If sTypedText.Contains(sFoundText) = True Then
                    iFoundIndex = i
                    cbo.SelectedIndex = i
                    Exit For
                Else
                    cbo.Text = ""
                    cbo.SelectedIndex = -1
                End If
            Next
        End If
    End Sub
#End Region

    Private Sub frm_ReturnAdvance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        MyBase.addGridDataErrorHandlers(grdCategory)
        GetStaffByLocation()
        ClearData()
        _LocationID = Global_CurrentLocationID
        Me.KeyPreview = True
    End Sub

    Private Sub frm_ReturnAdvance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                MyBase.btnNew.PerformClick()
            Case Keys.F2
                MyBase.btnSave.PerformClick()
            Case Keys.F3
                If btnDelete.Enabled = True Then
                    MyBase.btnDelete.PerformClick()
                End If
            Case Keys.F4
                MyBase.btnClose.PerformClick()
            Case Keys.F5
                btnPrint.PerformClick()
        End Select
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        'Dim dt As New DataTable
        'dt = objGeneralController.CheckExitVoucherForCashReceipt("tbl_CashReceipt", "AND VoucherNo='" + _ReturnAdvanceID + "' AND Type='SalesGems'")
        'If dt.Rows.Count() > 0 Then
        '    MsgBox("This VoucherNo is Use in CashReceipt!", MsgBoxStyle.Information, "")
        '    Return False
        '    Exit Function
        'End If

        If _ReturnAdvance.DeleteReturnAdvance(_ReturnAdvanceID) Then
            ClearData()
            btnDelete.Enabled = False
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        ClearData()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            Dim objReturnAdvance As New ReturnAdvanceInfo
            objReturnAdvance = GetDataReturnAdvance()
            If _ReturnAdvance.SaveReturnAdvance(objReturnAdvance, _dtReturnAdvanceItem) Then
                _ReturnAdvanceID = objReturnAdvance.ReturnAdvanceID
                If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                    Dim frmPrint As New frm_ReportViewer
                    Dim dt As New DataTable
                    dt = _ReturnAdvance.GetReturnAdvancePrint(_ReturnAdvanceID)
                    frmPrint.PrintReturnAdvance(dt)
                    ClearData()
                    frmPrint.WindowState = FormWindowState.Maximized
                    frmPrint.Show()
                    ClearData()
                Else
                    ClearData()
                    Return True
                End If
            Else
                Return False
            End If
        End If
    End Function

    Private Function IsFillData() As Boolean
        If _dtReturnAdvanceItem.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If


        If _dtReturnAdvanceItem.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        If grdCategory.Rows.Count <= 1 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        'For Each drDetail As DataRow In _dtReturnAdvanceItem.Rows
        '    If drDetail.RowState <> DataRowState.Deleted Then
        '        If IsDBNull(drDetail("GemsCategoryID")) Then
        '            MsgBox("Please Select Gem Category!", MsgBoxStyle.Information, AppName)
        '            grdCategory.Focus()
        '            Return False
        '        End If
        '    End If
        'Next

        If cboStaff.SelectedIndex = -1 Then
            MsgBox("Select Staff !", MsgBoxStyle.Information, AppName)
            cboStaff.Focus()
            Return False
        End If

        If _CustomerID = "" Then
            MsgBox("Please Select Customer !", MsgBoxStyle.Information, AppName)
            txtCustomerCode.Focus()
            Return False
        End If
        Return True
    End Function

    Public Function GetDataReturnAdvance() As ReturnAdvanceInfo
        Dim objReturnAdvance = New ReturnAdvanceInfo
        With objReturnAdvance
            .ReturnAdvanceID = _ReturnAdvanceID
            .ReturnAdvanceDate = dtpReturnAdvanceDate.Value
            .staffID = cboStaff.SelectedValue
            .CustomerID = _CustomerID
            .TotalTG = txtTotalG.Text
            .Address = txtAddress.Text
            .TotalAmount = txtTotalAmt.Text
            .Discount = txtDiscountAmt.Text
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .NetAmount = txtNetAmt.Text
        End With

        Return objReturnAdvance

    End Function

    Public Sub showReturnAdvanceData(ByVal objReturnAdvance As CommonInfo.ReturnAdvanceInfo)
        Dim objCustomer As New CustomerInfo
        With objReturnAdvance
            dtpReturnAdvanceDate.Value = .ReturnAdvanceDate
            txtReturnAdvanceID.Text = .ReturnAdvanceID
            cboStaff.SelectedValue = .StaffID
            cboStaff.Text = _Staff.GetStaff(.StaffID).Staff
            _CustomerID = .CustomerID
            objCustomer = _CustomerController.GetCustomerByID(_CustomerID)
            txtCustomerCode.Text = objCustomer.CustomerCode
            txtCustomer.Text = objCustomer.CustomerName
            txtAddress.Text = objCustomer.CustomerAddress
            txtTotalAmt.Text = Format(Val(.TotalAmount), "###,##0.##")
            txtDiscountAmt.Text = Format(Val(.Discount), "###,##0.##")
            txtRemark.Text = .Remark
            txtTotalG.Text = .TotalTG

        End With
    End Sub
    Private Sub ReturnAdvanceGenerateFormat()

        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.ReturnAdvance.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtReturnAdvanceID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpReturnAdvanceDate.Value)
        Else
            MsgBox("Please Fill the format at Generate Format Form", MsgBoxStyle.OkOnly, AppName)
        End If

    End Sub

    Public Sub ClearData()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _ReturnAdvanceID = "0"
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        ReturnAdvanceGenerateFormat()
        dtpReturnAdvanceDate.Value = Now
        cboStaff.SelectedValue = -1
        cboStaff.Text = ""
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtRemark.Text = ""
        _CustomerID = ""


        txtDiscountAmt.Text = "0"
        txtTotalAmt.Text = "0"
        txtNetAmt.Text = "0"
        _IsAllowDiscount = False

        Dim dc As DataColumn
        _dtReturnAdvanceItem = New DataTable
        _dtReturnAdvanceItem.Columns.Add("SrNo", System.Type.GetType("System.String"))
        _dtReturnAdvanceItem.Columns.Add("ReturnAdvanceItemID", System.Type.GetType("System.String"))
        _dtReturnAdvanceItem.Columns.Add("ReturnAdvanceID", System.Type.GetType("System.String"))
        _dtReturnAdvanceItem.Columns.Add("ItemTG", System.Type.GetType("System.String"))
        _dtReturnAdvanceItem.Columns.Add("Qty", System.Type.GetType("System.Int32"))
        _dtReturnAdvanceItem.Columns.Add("SaleRate", System.Type.GetType("System.Int64"))
        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtReturnAdvanceItem.Columns.Add(dc)
        _dtReturnAdvanceItem.Columns.Add("Remark", System.Type.GetType("System.String"))
        _dtReturnAdvanceItem.Columns.Add("IsUsed", System.Type.GetType("System.Boolean"))

        grdCategory.AutoGenerateColumns = False
        grdCategory.DataSource = _dtReturnAdvanceItem
        FormatGridReturnAdvance()

        _CustomerID = ""

    End Sub
#Region "FormatItemGrid"


    Public Sub FormatGridReturnAdvance()
        With grdCategory
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)

            Dim dcSNO As New DataGridViewTextBoxColumn
            With dcSNO
                .HeaderText = "No."
                .DataPropertyName = "SrNo"
                .Name = "SrNo"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 50
                .Visible = True
                .ReadOnly = True
            End With
            .Columns.Add(dcSNO)

            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "ReturnAdvanceItemID"
            dclID.DataPropertyName = "ReturnAdvanceItemID"
            dclID.Name = "ReturnAdvanceItemID"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcID As New DataGridViewTextBoxColumn()
            dcID.HeaderText = "ReturnAdvanceID"
            dcID.DataPropertyName = "ReturnAdvanceID"
            dcID.Name = "ReturnAdvanceID"
            dcID.Visible = False
            .Columns.Add(dcID)



            Dim dc4 As New DataGridViewTextBoxColumn()
            dc4.HeaderText = "Gram"
            dc4.DataPropertyName = "ItemTG"
            dc4.Name = "ItemTG"
            dc4.Width = 100
            dc4.Visible = True
            dc4.DefaultCellStyle.Format = "0.00"
            dc4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc4.SortMode = DataGridViewColumnSortMode.NotSortable
            dc4.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc4)

            Dim dc7 As New DataGridViewTextBoxColumn()
            dc7.HeaderText = "QTY"
            dc7.DataPropertyName = "Qty"
            dc7.Name = "Qty"
            dc7.Width = 50
            dc7.Visible = True
            dc7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc7.SortMode = DataGridViewColumnSortMode.NotSortable
            dc4.DefaultCellStyle.Format = "###,##0.##"
            dc7.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc7)

            Dim dcUP As New DataGridViewTextBoxColumn()
            dcUP.HeaderText = "Amount"
            dcUP.DataPropertyName = "Amount"
            dcUP.Name = "Amount"
            dcUP.Width = 100
            dcUP.Visible = True
            dcUP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dcUP.SortMode = DataGridViewColumnSortMode.NotSortable
            dcUP.DefaultCellStyle.Format = "###,##0.##"
            dcUP.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dcUP)

            Dim dcAmt As New DataGridViewTextBoxColumn()
            dcAmt.HeaderText = "UnitPrice"
            dcAmt.DataPropertyName = "SaleRate"
            dcAmt.Name = "SaleRate"
            dcAmt.Width = 100
            dcAmt.Visible = True
            dcAmt.ReadOnly = True
            dcAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dcAmt.DefaultCellStyle.Format = "###,##0.##"
            dcAmt.SortMode = DataGridViewColumnSortMode.NotSortable
            dcAmt.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dcAmt)



            Dim dcRemark As New DataGridViewTextBoxColumn()
            dcRemark.HeaderText = "Remark"
            dcRemark.DataPropertyName = "Remark"
            dcRemark.Name = "Remark"
            dcRemark.Width = 500
            dcRemark.Visible = True
            .Columns.Add(dcRemark)

            Dim dcIsUsed As New DataGridViewCheckBoxColumn()
            With dcIsUsed
                .HeaderText = "IsUsed"
                .DataPropertyName = "IsUsed"
                .Name = "IsUsed"
                .Width = 50
                .Visible = True
                .ReadOnly = True
            End With
            .Columns.Add(dcIsUsed)


        End With

    End Sub
#End Region

    'Public Sub GetCombo()
    '    cboSalesStaff.DisplayMember = "Staff_"
    '    cboSalesStaff.ValueMember = "StaffID"
    '    cboSalesStaff.DataSource = _Staff.GetStaffList().DefaultView
    'End Sub

    Private Sub GetStaffByLocation()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _Staff.GetStaffList().DefaultView
        cboStaff.SelectedIndex = -1

    End Sub

    Private Sub grdCategory_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCategory.CellValidated

        If grdCategory.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdCategory.Columns(e.ColumnIndex).Name

                Case "Amount"

                    If Not CDec((IsDBNull(Format(grdCategory.Rows(e.RowIndex).Cells("Amount").Value, "###,##0.##")))) Then
                        grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value = CDec(CDec(grdCategory.Rows(e.RowIndex).Cells("Amount").Value) / (CDec(grdCategory.Rows(e.RowIndex).Cells("ItemTG").FormattedValue)))
                    End If

            End Select

        End If
        CalculategrdTotalAmount()
    End Sub
    Private Sub CalculategrdCategory()
        Dim _ItemTG As Decimal
        For i As Integer = 0 To grdCategory.RowCount - 1
            If Not grdCategory.Rows(i).IsNewRow Then

                If grdCategory.Rows(i).Cells("ItemTG").FormattedValue <> "" Then
                    _ItemTG += Val(CDec(grdCategory.Rows(i).Cells("ItemTG").FormattedValue))
                End If

            End If
        Next

        txtTotalG.Text = Format(_ItemTG, "0.000")

        _dtReturnAdvanceItem = grdCategory.DataSource
    End Sub
    Private Sub CalculategrdTotalAmount()
        Dim totalamt As Decimal = 0
        Dim ItemTG As Decimal = 0
        If grdCategory.RowCount - 1 = 0 Then
            txtTotalAmt.Text = "0"
        End If
        For i As Integer = 0 To grdCategory.RowCount - 1
            If Not grdCategory.Rows(i).IsNewRow Then
                If grdCategory.Rows(i).Cells("Amount").FormattedValue <> "" Then
                    totalamt += Val(CLng(grdCategory.Rows(i).Cells("Amount").FormattedValue))
                End If
                If grdCategory.Rows(i).Cells("ItemTG").FormattedValue <> "" Then
                    ItemTG += Val(CDec(grdCategory.Rows(i).Cells("ItemTG").FormattedValue))
                End If

            End If
        Next
        txtTotalAmt.Text = Format(Val(totalamt), "###,##0.##")
        txtTotalG.Text = ItemTG
        txtDiscountAmt.Text = "0"
        CalculateFinalAmount()
    End Sub

    Private Sub txtNetAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtNetAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmt.TextChanged
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        'If txtAddOrSub.Text = "" Then txtAddOrSub.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        'If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        'If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"

        'txtAddOrSub.Text = Format(Val((CLng(txtNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text)) - CLng(txtTotalAmt.Text)), "###,##0.##")
        'If Global_IsCash Then
        '    txtPaidAmt.Text = Format(Val(CLng(txtNetAmt.Text)), "###,##0.##")
        'End If
        'txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtPaidAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPaidAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        'If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"

        'txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
    End Sub

    Private Sub btnSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchButton.Click
        Dim DataItem As DataRow
        Dim dtReturnAdvance As New DataTable
        Dim dtReturnAdvanceItem As New DataTable
        Dim objReturnAdvance As New ReturnAdvanceInfo
        Dim objReturnAdvanceItem As New ReturnAdvanceItemInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        dtReturnAdvance = _ReturnAdvance.GetAllReturnAdvance()
        DataItem = DirectCast(SearchData.FindFast(dtReturnAdvance, "ReturnAdvance List"), DataRow)
        If DataItem IsNot Nothing Then
            _ReturnAdvanceID = DataItem.Item("VoucherNo").ToString()
            objReturnAdvance = _ReturnAdvance.GetReturnAdvance(_ReturnAdvanceID)
            showReturnAdvanceData(objReturnAdvance)
            _dtReturnAdvanceItem = _ReturnAdvance.GetReturnAdvanceItem(_ReturnAdvanceID)
            grdCategory.DataSource = _dtReturnAdvanceItem
            CalculategrdCategory()
            MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        dt = _ReturnAdvance.GetReturnAdvancePrint(_ReturnAdvanceID)

        frmPrint.PrintReturnAdvance(dt)

        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub
    Private Sub txtTotalAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAmt.TextChanged
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"

        txtDiscountAmt.Text = "0"

        CalculateFinalAmount()
    End Sub

    Private Sub grdCategory_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdCategory.RowsRemoved
        If grdCategory.RowCount > 0 Then
            CalculategrdCategory()
            CalculategrdTotalAmount()
        End If
    End Sub

    Private Sub cboStaff_Click(sender As Object, e As EventArgs) Handles cboStaff.Click
        GetStaffByLocation()
    End Sub

    Private Sub cboSalesStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboSalesStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub

    Private Sub txtDiscountAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscountAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDiscountAmt_TextChanged(sender As Object, e As EventArgs) Handles txtDiscountAmt.TextChanged
        CalculateFinalAmount()
    End Sub
    Private Function IsAllowAddOrSub() As Boolean
        If _IsAllowDiscount = False Then
            If Global_UserLevel <> "Administrator" Then
                If Val(txtDiscountAmt.Text) > Global_AllowDis Then
                    MsgBox("Discount Amount Is Not Allow!", MsgBoxStyle.Information, AppName)
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    Private Sub CalculateFinalAmount()
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        txtNetAmt.Text = Format((CLng(txtTotalAmt.Text) - (CLng(txtDiscountAmt.Text))), "###,##0.##")

    End Sub
    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow
        dt = _CustomerController.GetAllCustomerAutoCompleteData()

        DataItem = DirectCast(SearchData.FindFast(dt, "Customer List"), DataRow)
        If DataItem IsNot Nothing Then
            If DataItem("$Inactive") = False Then
                _CustomerID = DataItem("@CustomerID")
                txtCustomerCode.Text = DataItem("CustomerCode")
                txtCustomer.Text = DataItem("CustomerName_")
                txtAddress.Text = DataItem("CustomerAddress_")
            Else
                MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
                Exit Sub
            End If

        End If
    End Sub


    Private Sub txtCustomerCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtCustomerCode_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerCode.TextChanged
        Dim objCustomer As CustomerInfo
        If _IsCustomerName = False Then
            _IsCustomerCode = True
            If txtCustomerCode.Text <> "" Then
                objCustomer = _CustomerController.GetCustomerCode(txtCustomerCode.Text)
                If objCustomer.CustomerID <> "" Then
                    If objCustomer.IsInactive = 0 Then
                        With objCustomer
                            _CustomerID = .CustomerID
                            txtCustomerCode.Text = .CustomerCode
                            txtCustomer.Text = .CustomerName
                            txtAddress.Text = .CustomerAddress
                        End With
                    Else
                        MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                        _CustomerID = ""
                        txtCustomer.Text = ""
                        txtAddress.Text = ""
                        Exit Sub
                    End If
                    txtCustomerCode.Enabled = True
                Else
                    _CustomerID = ""
                    txtCustomer.Text = ""
                    txtAddress.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
            End If
            _IsCustomerCode = False
        End If
    End Sub

    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsK") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsP") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsY") Then
        '    If IsDBNull(grdCategory.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdCategory.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdCategory.CurrentRow.Cells("GemsY").FormattedValue) Then
        '        If grdCategory.CurrentRow.Cells("YOrCOrG").FormattedValue <> "0" Then
        '            grdCategory.CurrentRow.Cells("YOrCOrG").Value = "0"
        '        End If
        '    End If
        'End If

        If grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("ItemTG") Then
            If IsDBNull(IsDBNull(grdCategory.CurrentRow.Cells("ItemTG").FormattedValue) = False) Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If
        ElseIf grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("SaleRate") Then
            If IsDBNull(grdCategory.CurrentRow.Cells("SaleRate").FormattedValue) = False Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Chr(46) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If
        End If
    End Sub
    Private Sub grdCategory_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCategory.EditingControlShowing
        If grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("ItemTG") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("SaleRate") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("Remark") Then
            Exit Sub
        End If

        Dim txtbox As TextBox = CType(e.Control, TextBox)
        If Not (txtbox Is Nothing) Then
            AddHandler txtbox.KeyPress, AddressOf txtBox_KeyPress
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm As New frm_CustomerShow
        Dim ObjCustomer As CustomerInfo
        If _CustomerID <> "" Then
            ObjCustomer = _CustomerController.GetCustomerByID(_CustomerID)
            With ObjCustomer
                frm._CustomerID = _CustomerID
                frm._CustomerCode = .CustomerCode
                frm.txtCustomerID.Text = _CustomerID
                frm.txtCustomerCode.Text = .CustomerCode
                frm.txtName.Text = .CustomerName
                frm.txtAddress.Text = .CustomerAddress
                frm.txtPhone.Text = .CustomerTel
                frm.txtRemark.Text = .Remark
                frm.chkInactive.Checked = IIf(CBool(.IsInactive) = True, True, False)
                frm._IsUpdate = True
            End With
        End If
        frm.ShowDialog()
        If frm._CustomerID <> "" Then
            _CustomerID = frm._CustomerID
            ObjCustomer = _CustomerController.GetCustomerByID(_CustomerID)
            txtCustomerCode.Text = ObjCustomer.CustomerCode
            txtCustomer.Text = ObjCustomer.CustomerName
            txtAddress.Text = ObjCustomer.CustomerAddress
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("ReturnAdvance")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtCustomer.TextChanged
        If _IsCustomerCode = False Then
            Dim dt As New DataTable
            Dim DataCollection As New AutoCompleteStringCollection()
            _IsCustomerName = True
            If txtCustomer.Text <> "" Then
                dt = _CustomerController.GetCustomerDataByCustomerName(txtCustomer.Text.Trim)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0).Item("IsInactive") = False) Then
                        _CustomerID = dt.Rows(0).Item("CustomerID")
                        txtCustomerCode.Text = dt.Rows(0).Item("CustomerCode")
                        txtCustomer.Text = dt.Rows(0).Item("CustomerName")
                        txtAddress.Text = dt.Rows(0).Item("CustomerAddress")
                    Else
                        _CustomerID = ""
                        txtCustomerCode.Text = ""
                        txtAddress.Text = ""
                    End If
                Else
                    _CustomerID = ""
                    txtCustomerCode.Text = ""
                    txtAddress.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtAddress.Text = ""
            End If
            _IsCustomerName = False
        End If
    End Sub

    Private Sub dtpSaleDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpReturnAdvanceDate.ValueChanged
        ReturnAdvanceGenerateFormat()
    End Sub

    Private Sub txtTotalAmt_Validated(sender As Object, e As EventArgs) Handles txtTotalAmt.Validated
        txtTotalAmt.Text = Format(Val(CLng(txtTotalAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtDiscountAmt_Validated(sender As Object, e As EventArgs) Handles txtDiscountAmt.Validated
        txtDiscountAmt.Text = Format(Val(CLng(txtDiscountAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtNetAmt_Validated(sender As Object, e As EventArgs) Handles txtNetAmt.Validated
        txtNetAmt.Text = Format(Val(CLng(txtNetAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtPaidAmt_Validated(sender As Object, e As EventArgs)
        'txtPaidAmt.Text = Format(Val(CLng(txtPaidAmt.Text)), "###,##0.##")
    End Sub


    Private Sub grdCategory_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles grdCategory.RowStateChanged
        MyBase.ShowGridSerialNo(grdCategory)
    End Sub
End Class
