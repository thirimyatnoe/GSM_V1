Imports BusinessRule
Imports CommonInfo
Public Class frm_PurchaseFromSupplier
    Implements IFormProcess

    Private _PurchaseFromSupplierID As String
    Private _SupplierID As String
    Private _dtPurchaseItem As DataTable
    Private objPurchaseController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private objSupplierController As Supplier.ISupplierController = Factory.Instance.CreateSupplierController
    Dim _DiffAmount As Decimal = 0

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

    Private Sub frm_Purchase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'FormatGrid()
        lblLogInUserName.Text = Global_CurrentUser
        ClearData()
        GetCombo()
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If objPurchaseController.DeletePurchaseFromSupplier(_PurchaseFromSupplierID) Then
            ClearData()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        ClearData()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objPurchaseHeader As New PurchaseHeaderInfo
        If IsFillData() Then
            objPurchaseHeader = GetData()
            If objPurchaseController.SavePurchaseFromSupplier(objPurchaseHeader, _dtPurchaseItem) Then
                ClearData()
                Return True
            Else
                Return False
            End If
        End If
    End Function

#Region " Private Methods "

    Public Function IsFillData() As Boolean

        If cboStaff.SelectedIndex = -1 Then
            MsgBox("Select Staff !", MsgBoxStyle.Information, AppName)
            cboStaff.Focus()
            Return False
        End If

        If txtSupplierCode.Text = "" Then
            MsgBox("Enter Supplier Code!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        If _dtPurchaseItem.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        Return True

    End Function
    Public Function GetData() As CommonInfo.PurchaseHeaderInfo
        Dim objPurchaseHeader As New CommonInfo.PurchaseHeaderInfo

        With objPurchaseHeader
            .PurchaseFromSupplierID = _PurchaseFromSupplierID
            .PDate = dtpPDate.Value
            .StaffID = cboStaff.SelectedValue
            .SupplierID = _SupplierID
            .Remark = IIf(txtRemark.Text.Trim = "", "-", txtRemark.Text.Trim)
            .DueDate = Now.Date

            If (optCredit.Checked) Then
                .PayType = 0
                .DueDate = dtpDueDate.Value
            ElseIf (optConsignment.Checked) Then
                .PayType = 1
            ElseIf (optCash.Checked) Then
                .PayType = 2
            Else
                .PayType = 3
            End If

            .Voucher = IIf(txtVoucher.Text.Trim = "", "-", txtVoucher.Text.Trim)
            .ExchangeRate = IIf(txtExRate.Text = "", 0, txtExRate.Text)
            .TotalAmount = txtTotalAmt.Text
            .AddOrSub = txtAddOrSub.Text
            .DiscountRate = IIf(txtDisRate.Text = "", 0, txtDisRate.Text)
            .PaidAmount = txtPaidAmt.Text
            .CommissionRate = 0
            .LocationID = Global_CurrentLocationID 'Global_CurrentLocationID
        End With
        Return objPurchaseHeader
    End Function

    Public Sub GetCombo()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = objStaffController.GetStaffList().DefaultView

    End Sub

    Public Sub ClearData()

        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController

        txtPurchaseFromSupplierID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.PurchaseFromSupplier, EnumSetting.GenerateKeyType.PurchaseFromSupplier.ToString, Now)
        _PurchaseFromSupplierID = "0"
        dtpPDate.Value = Now.Date
        cboStaff.SelectedValue = -1
        _SupplierID = "0"
        txtSupplierCode.Text = ""
        txtSupplierName.Text = ""
        txtRemark.Text = ""
        btnSearchButton().Focus()

        txtVoucher.Text = ""
        optCredit.Checked = True
        dtpDueDate.Value = Now.Date
        optCash.Checked = False
        optBank.Checked = False

        Dim dc As New DataColumn
        _dtPurchaseItem = New DataTable

        _dtPurchaseItem.Columns.Add("PurchaseFromSupplierItemID", System.Type.GetType("System.String"))
        _dtPurchaseItem.Columns.Add("PurchaseFromSupplierID", System.Type.GetType("System.String"))
        _dtPurchaseItem.Columns.Add("OriginalCode", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "GWeight"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtPurchaseItem.Columns.Add(dc)

        _dtPurchaseItem.Columns.Add("QTY", System.Type.GetType("System.Int32"))

        dc = New DataColumn
        dc.ColumnName = "Rate"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtPurchaseItem.Columns.Add(dc)

        _dtPurchaseItem.Columns.Add("Amount", System.Type.GetType("System.Decimal"))
        _dtPurchaseItem.Columns.Add("IsReject", System.Type.GetType("System.Boolean"))

        grdPurchaseItem.AutoGenerateColumns = False
        grdPurchaseItem.ReadOnly = False
        grdPurchaseItem.DataSource = _dtPurchaseItem
        FormatGridItem()

        txtGridTotal.Text = "0"
        txtTotalAmt.Text = "0"
        txtExRate.Text = "0"
        txtDisRate.Text = ""
        txtDisAmount.Text = "0"
        txtNetAmt.Text = "0"
        txtAddOrSub.Text = "0"
        txtPaidAmt.Text = "0"
        txtBalanceAmt.Text = "0"

    End Sub
    Public Sub FormatGridItem()

        With grdPurchaseItem
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Zawgyi-one", 9.25)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40


            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "PurchaseFromSupplierItemID"
                .DataPropertyName = "PurchaseFromSupplierItemID"
                .Name = "PurchaseFromSupplierItemID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "PurchaseFromSupplierID"
                .DataPropertyName = "PurchaseFromSupplierID"
                .Name = "PurchaseFromSupplierID"
                .Visible = False
            End With
            .Columns.Add(dcID)


            Dim dcOrg As New DataGridViewTextBoxColumn()
            With dcOrg
                .HeaderText = "OriginalCode"
                .DataPropertyName = "OriginalCode"
                .Name = "OriginalCode"
                .Width = 150
                .Visible = True
            End With
            .Columns.Add(dcOrg)

            Dim dcWeight As New DataGridViewTextBoxColumn()
            With dcWeight
                .HeaderText = "Gram"
                .DataPropertyName = "GWeight"
                .Name = "GWeight"
                .Width = 80
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "0.0"
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcWeight)

            Dim dc7 As New DataGridViewTextBoxColumn()
            dc7.HeaderText = "QTY"
            dc7.DataPropertyName = "QTY"
            dc7.Name = "QTY"
            dc7.Width = 60
            dc7.Visible = True
            dc7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc7.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc7)

            Dim dcPurchaseRate As New DataGridViewTextBoxColumn()
            With dcPurchaseRate
                .HeaderText = "Rate"
                .DataPropertyName = "Rate"
                .Name = "Rate"
                .Width = 120
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPurchaseRate)

            Dim dcAmt As New DataGridViewTextBoxColumn()
            With dcAmt
                .HeaderText = "Amount"
                .DataPropertyName = "Amount"
                .Name = "Amount"
                .Width = 120
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcAmt)
        End With
    End Sub

    Private Sub CalculategrdTotalAmount()
        Dim grdtotalAmt As Decimal = 0.0

        For i As Integer = 0 To grdPurchaseItem.RowCount - 1
            If Not grdPurchaseItem.Rows(i).IsNewRow Then
                grdtotalAmt += CDec(Val(grdPurchaseItem.Rows(i).Cells("Amount").FormattedValue))
            End If

        Next
        txtGridTotal.Text = CStr(grdtotalAmt)
    End Sub

    Private Sub showPurchaseFromSupplierData(ByVal objPurInvoice As CommonInfo.PurchaseHeaderInfo)

        With objPurInvoice
            txtPurchaseFromSupplierID.Text = .PurchaseFromSupplierID
            dtpPDate.Value = .PDate
            cboStaff.SelectedValue = .StaffID
            txtSupplierCode.Text = objSupplierController.GetSupplierByID(_SupplierID).SupplierCode
            txtSupplierName.Text = objSupplierController.GetSupplierByID(_SupplierID).SupplierName
            txtRemark.Text = .Remark
            dtpDueDate.Value = .DueDate
            txtVoucher.Text = .Voucher

            If (.PayType = "0") Then
                optCredit.Checked = True
            ElseIf (.PayType = "1") Then
                optConsignment.Checked = True
            ElseIf (.PayType = "2") Then
                optCash.Checked = True
            Else
                optBank.Checked = True
            End If


        End With

    End Sub

    Private Sub ShowDataForSupplier(ByVal obj As CommonInfo.SupplierInfo)
        With obj
            txtSupplierCode.Text = .SupplierCode
            txtSupplierName.Text = .SupplierName
            _SupplierID = .SupplierID
        End With

    End Sub
#End Region

    Private Sub btnSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchButton.Click
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)

        Dim DataItem As DataRow
        Dim dtPurInvoice As New DataTable
        Dim objPurInvoice As New PurchaseHeaderInfo

        dtPurInvoice = objPurchaseController.GetAllPurchaseFromSupplier()
        DataItem = DirectCast(SearchData.FindFast(dtPurInvoice, "PurchaseFromSupplier List"), DataRow)


        If DataItem IsNot Nothing Then
            _PurchaseFromSupplierID = DataItem.Item("PurchaseFromSupplierID").ToString()
            _SupplierID = DataItem.Item("@SupplierID").ToString()

            objPurInvoice = objPurchaseController.GetPurchaseFromSupplier(_PurchaseFromSupplierID)

            showPurchaseFromSupplierData(objPurInvoice)

            _dtPurchaseItem = objPurchaseController.GetPurchaseFromSupplierItem(_PurchaseFromSupplierID)
            grdPurchaseItem.DataSource = _dtPurchaseItem
            grdPurchaseItem.ReadOnly = False
            CalculategrdTotalAmount()

            With objPurInvoice
                txtTotalAmt.Text = .TotalAmount
                txtExRate.Text = .ExchangeRate
                txtDisRate.Text = .DiscountRate
                txtAddOrSub.Text = .AddOrSub
                txtNetAmt.Text = CStr(CLng(txtTotalAmt.Text) - CLng(txtDisAmount.Text) - CLng(txtAddOrSub.Text))

                txtPaidAmt.Text = .PaidAmount
                txtBalanceAmt.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text))

            End With
            btnDelete.Enabled = True
        End If

    End Sub

    Private Sub grdItem_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPurchaseItem.CellValidated
        If grdPurchaseItem.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdPurchaseItem.Columns(e.ColumnIndex).Name
                Case "GWeight", "QTY", "Rate"

                    If Not (IsDBNull(grdPurchaseItem.Rows(e.RowIndex).Cells("Rate").Value)) Then
                        grdPurchaseItem.Rows(e.RowIndex).Cells("Amount").Value = Format(grdPurchaseItem.Rows(e.RowIndex).Cells("GWeight").Value * grdPurchaseItem.Rows(e.RowIndex).Cells("Rate").Value, "0.00")
                    End If

            End Select
            CalculategrdTotalAmount()
        End If
    End Sub

    Private Sub grdItem_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdPurchaseItem.RowsRemoved
        If (grdPurchaseItem.RowCount > 0) Then
            CalculategrdTotalAmount()
        End If
    End Sub

    Private Sub txtSupplierCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSupplierCode.TextChanged
        Dim objSupplier As New SupplierInfo
        objSupplier = objSupplierController.GetSupplierDataByCode(txtSupplierCode.Text)
        txtSupplierName.Text = ""
        If Not IsNothing(objSupplier.SupplierName) Then
            ShowDataForSupplier(objSupplier)
        End If
    End Sub

    Private Sub btnSupplierSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSupplierSearch.Click
        Dim DataItem As DataRow
        Dim dtSupplier As New DataTable

        dtSupplier = objSupplierController.GetAllSupplierList()
        DataItem = DirectCast(SearchData.FindFast(dtSupplier, "Supplier List"), DataRow)

        If DataItem IsNot Nothing Then
            _SupplierID = DataItem.Item("@SupplierID").ToString()
            txtSupplierCode.Text = DataItem.Item("SupplierCode")
            txtSupplierName.Text = DataItem.Item("SupplierName_")
        End If

    End Sub

    Private Sub txtExRate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtExRate.KeyPress
        ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtExRate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExRate.TextChanged
        If txtExRate.Text = "" Then txtExRate.Text = "0"
        If txtGridTotal.Text = "" Then txtGridTotal.Text = "0"

        If txtExRate.Text <> "" Then
            txtTotalAmt.Text = CInt(txtGridTotal.Text) * CInt(txtExRate.Text)
        End If
        If txtExRate.Text = "0" Then
            txtTotalAmt.Text = CInt(txtGridTotal.Text)
        End If
    End Sub

    Private Sub txtDisRate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDisRate.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDisRate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDisRate.TextChanged
        If txtDisRate.Text = "" Then txtDisRate.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"

        txtDisAmount.Text = CStr(CDec(CDec(txtTotalAmt.Text) * CDec(txtDisRate.Text)) / 100)
        _DiffAmount = txtTotalAmt.Text - txtDisAmount.Text
        txtNetAmt.Text = _DiffAmount
    End Sub

    Private Sub txtNetAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtNetAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmt.TextChanged
        If (txtNetAmt.Text = "") Then
            txtNetAmt.Text = "0"
        End If
        If (txtNetAmt.Text <> "") And (txtTotalAmt.Text <> "") Then
            txtAddOrSub.Text = CStr(CLng(_DiffAmount) - CLng(txtNetAmt.Text))
            txtPaidAmt.Text = txtNetAmt.Text
        End If
    End Sub

    Private Sub txtPaidAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPaidAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaidAmt.TextChanged
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        txtBalanceAmt.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text))
    End Sub

    Private Sub txtTotalAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAmt.TextChanged
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDisRate.Text = "" Then txtDisRate.Text = "0"

        If CLng(txtDisRate.Text) >= 0 Then
            txtDisAmount.Text = CStr(CDec(CDec(txtTotalAmt.Text) * CDec(txtDisRate.Text)) / 100)
            _DiffAmount = txtTotalAmt.Text - txtDisAmount.Text
            txtNetAmt.Text = _DiffAmount
        End If

    End Sub

    Private Sub txtGridTotal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGridTotal.TextChanged
        If txtGridTotal.Text = "" Then txtGridTotal.Text = "0"
        If txtExRate.Text = "" Then txtExRate.Text = "0"
        If txtGridTotal.Text <> "0" Then
            txtTotalAmt.Text = txtGridTotal.Text
        End If

        If txtExRate.Text <> "0" Then
            txtTotalAmt.Text = CStr(CDec(txtGridTotal.Text) * CInt(txtExRate.Text))
        End If
    End Sub

    Private Sub lnkSupplier_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSupplier.LinkClicked
        Dim frm As New frm_Supplier
        frm.ShowDialog()
    End Sub

    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub

    Private Sub btnSupplierSearch_Click_1(sender As Object, e As EventArgs) Handles btnSupplierSearch.Click
        Dim objSupplier As New SupplierInfo
        objSupplier = objSupplierController.GetSupplierDataByCode(txtSupplierCode.Text)
        txtSupplierName.Text = ""
        If Not IsNothing(objSupplier.SupplierName) Then
            ShowDataForSupplier(objSupplier)
        End If
    End Sub


    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("PurchaseRaw")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

   
End Class
