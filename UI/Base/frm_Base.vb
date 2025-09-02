Public Class frm_Base

#Region "AddEventHandalers"
    Private Sub AddEventHandalers(ByVal ControlContainers As Control)
        For Each ctrl As Control In ControlContainers.Controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
                AddHandler ctrl.Enter, AddressOf ProcessEnter
                AddHandler ctrl.Leave, AddressOf ProcessLeave
                AddHandler ctrl.KeyDown, AddressOf ProcessKeyDown
            End If

            If ctrl.HasChildren Then
                AddEventHandalers(ctrl)
            End If
        Next
    End Sub
#End Region

    Private Sub Base_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddEventHandalers(Me)
        Me.KeyPreview = True
    End Sub

#Region "ProcessLeave"
    Private Sub ProcessLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, Control).BackColor = Color.FromKnownColor(KnownColor.Window)
    End Sub
#End Region

#Region "ProcessEnter"
    Private Sub ProcessEnter(ByVal sender As Object, ByVal e As System.EventArgs)

        DirectCast(sender, Control).BackColor = System.Drawing.ColorTranslator.FromOle(RGB(228, 252, 249))    'colortrans Color("228, 252, 249") 'New Color(228, 252, 249) 'Color.Crimson  'RGB(228, 252, 249) 'Color.SkyBlue
    End Sub
#End Region

#Region "ProcessKeyDown"
    Private Sub ProcessKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{Tab}")
    End Sub
#End Region

#Region " SetFormTitle"
    Protected Sub SetFormTitle(ByVal itemTitle As String)
        Dim separator As String = " - "
        Dim windowText As String = Me.Text

        If Me.Text.Contains(separator) Then
            windowText = windowText.Remove(Me.Text.IndexOf(separator))
        End If

        If String.IsNullOrEmpty(itemTitle) Then
            Me.Text = windowText
        Else
            Me.Text = windowText & separator & itemTitle
        End If
    End Sub
#End Region

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        BaseProcess("New")
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure to delete?", MsgBoxStyle.YesNo, "Jewellery Shop Management System") = MsgBoxResult.Yes Then
            BaseProcess("Delete")
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        BaseProcess("Save")
    End Sub

    Private Sub BaseProcess(ByVal Process As String)
        Try
            Dim frmMDIChild As IFormProcess
            frmMDIChild = TryCast(Me, IFormProcess)
            If frmMDIChild Is Nothing Then
                MessageBox.Show("You must open a window")
            Else
                Select Case Process
                    Case "New"
                        frmMDIChild.ProcessNew()
                    Case "Save"
                        If frmMDIChild.ProcessSave() = True Then
                            MessageBox.Show("Save Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Case "Delete"
                        If frmMDIChild.ProcessDelete() = True Then
                            MessageBox.Show("Delete Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                End Select
            End If
        Catch ex As InvalidOperationException
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Protected Sub ValidateNumeric(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal DecimalAllo As Boolean = False)
        Dim SenderTextBox As TextBox = DirectCast(sender, TextBox)
        Dim DecPos As Integer = SenderTextBox.Text.IndexOf(".")
        If DecimalAllo = True Then
            If e.KeyChar.ToString = "." AndAlso (DecPos >= 0) Then
                e.Handled = True
                Exit Sub
            End If
            If KeyValidation(InStr("1234567890.", e.KeyChar.ToString, CompareMethod.Text) > 0, e) Then Return
        Else
            If KeyValidation(InStr("1234567890", e.KeyChar.ToString, CompareMethod.Text) > 0, e) Then Return
        End If
    End Sub

    Protected Sub ValidateNumericAllowMinus(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal DecimalAllo As Boolean = False)
        Dim SenderTextBox As TextBox = DirectCast(sender, TextBox)
        Dim DecPos As Integer = SenderTextBox.Text.IndexOf(".")
        Dim MinusPos As Integer = SenderTextBox.Text.IndexOf("-")
        If e.KeyChar.ToString = "-" Then
            If SenderTextBox.Text.Length > 0 Then
                If SenderTextBox.SelectionStart <> 0 OrElse (MinusPos >= 0) Then
                    e.Handled = True
                    Exit Sub
                End If

            End If
        End If
        If DecimalAllo = True Then
            If e.KeyChar.ToString = "." AndAlso (DecPos >= 0) Then
                e.Handled = True
                Exit Sub
            End If
            If KeyValidation(InStr("-1234567890.", e.KeyChar.ToString, CompareMethod.Text) > 0, e) Then Return
        Else
            If KeyValidation(InStr("-1234567890", e.KeyChar.ToString, CompareMethod.Text) > 0, e) Then Return
        End If
    End Sub

    Private Function KeyValidation(ByVal Bool As Boolean, ByRef e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Dim RBoolean As Boolean = Bool
        Dim Index As Integer
        If Bool = False Then
            Index = AscW(e.KeyChar)
            If Index = Keys.Back Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
        Return RBoolean
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Protected Function IsTextBoxFill(ByVal txtBox As TextBox) As Boolean
        If txtBox.Text.Trim = "" Then
            MsgBox(txtBox.Tag & " Field need to fill !", MsgBoxStyle.Exclamation, Me.Text)
            txtBox.Focus()
            Return False
        End If
        Return True
    End Function

    Protected Function IsComboboxFill(ByVal cbobox As ComboBox) As Boolean
        If cbobox.SelectedIndex = -1 Then
            MsgBox("Select " & cbobox.Tag & "!", MsgBoxStyle.Exclamation, Me.Text)
            cbobox.Focus()
            Return False
        End If
        Return True
    End Function


    Protected Sub ChangeButtonEnableStage(ByVal ButtonEnableStage As CommonInfo.EnumSetting.ButtonEnableStage)
        If CommonInfo.EnumSetting.ButtonEnableStage.Save = ButtonEnableStage Then
            btnSave.Enabled = True
            btnSave.Text = "&Save"
            btnDelete.Enabled = False
        ElseIf CommonInfo.EnumSetting.ButtonEnableStage.Update = ButtonEnableStage Then
            btnSave.Enabled = True
            btnSave.Text = "&Update"
            btnDelete.Enabled = True
        End If
    End Sub
    Protected Sub ShowGridSerialNo(ByVal grd_Name As DataGridView, Optional ByVal grd_ColumnName As String = "SrNo")
        For i As Integer = 0 To grd_Name.Rows.Count - 2
            grd_Name.Rows(i).Cells(grd_ColumnName).Value = i + 1
        Next
    End Sub
    Protected Function CalculateTotalFromGrid(ByVal grd_Name As DataGridView, ByVal grd_ColumnName As String) As String
        Try
            Dim Total As Double = 0.0
            For i As Integer = 0 To grd_Name.Rows.Count - 2
                If grd_Name.Rows(i).Cells(grd_ColumnName).Value.ToString.Trim <> "" Then
                    Total += CDbl(grd_Name.Rows(i).Cells(grd_ColumnName).Value)
                End If
            Next
            Return CStr(Total)
        Catch ex As System.Exception
            MsgBox(ex.ToString)
            Return "0.0"
        End Try
    End Function

    Protected Function IsFill_DataInGrid(ByVal grd_Name As DataGridView, ByVal dtGrid As DataTable, ByVal grd_CheckColumnName As String) As Boolean
        For i As Integer = 0 To dtGrid.Rows.Count - 1
            If dtGrid.Rows(i).RowState = DataRowState.Deleted Then Continue For

            If dtGrid.Rows(i).Item(grd_CheckColumnName).ToString = "" Then
                MessageBox.Show("Please fill "" " & grd_Name.Columns(grd_CheckColumnName).HeaderText & " "" in Row < No.= " & i + 1 & " >.", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
                grd_Name.Focus()
                Return False
            End If
            If grd_CheckColumnName = "Quantity" Then
                If dtGrid.Rows(i).Item(grd_CheckColumnName) = 0 Then
                    MessageBox.Show("Please fill "" " & grd_Name.Columns(grd_CheckColumnName).HeaderText & " "" in Row < No.= " & i + 1 & " >.", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    grd_Name.Focus()
                    Return False
                End If
            End If
            If grd_CheckColumnName = "OpeningQty" Then
                If dtGrid.Rows(i).Item(grd_CheckColumnName) = 0 Then
                    MessageBox.Show("Please fill "" " & grd_Name.Columns(grd_CheckColumnName).HeaderText & " "" in Row < No.= " & i + 1 & " >.", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    grd_Name.Focus()
                    Return False
                End If
            End If
            If grd_CheckColumnName = "OpeningCost" Then
                If dtGrid.Rows(i).Item(grd_CheckColumnName) = 0 Then
                    MessageBox.Show("Please fill "" " & grd_Name.Columns(grd_CheckColumnName).HeaderText & " "" in Row < No.= " & i + 1 & " >.", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    grd_Name.Focus()
                    Return False
                End If
            End If
        Next

        Return True
    End Function
    Protected Sub addGridDataErrorHandlers(ByVal grdItems As DataGridView)
        AddHandler grdItems.DataError, AddressOf grdItems_DataError
        AddHandler grdItems.CellValidating, AddressOf grdItems_CellValidating
    End Sub
    Private Sub grdItems_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs)
        Dim grdDataError As DataGridView = CType(sender, DataGridView)

        If grdDataError.IsCurrentCellDirty Then
            If grdDataError.Rows(e.RowIndex).ErrorText <> "" Then
                grdDataError.Rows(e.RowIndex).ErrorText = String.Empty
            End If
        End If
    End Sub
    Private Sub grdItems_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
        Dim grdDataError As DataGridView = CType(sender, DataGridView)

        If e.Exception.Message.ToString = "Input string was not in a correct format." Then
            ' if you add "Error Row Icon" you need to clear "Error Row Icon" in "CellValidating Event" if "Allow Data" is added
            grdDataError.Rows(e.RowIndex).ErrorText = "The wrong data format in """ & grdDataError.Columns(e.ColumnIndex).HeaderText & """ column ! You are" & vbNewLine & "not allow to do other action until fill correct format."

            MessageBox.Show("Please fill number only in """ + grdDataError.Columns(e.ColumnIndex).Name.ToString + """ column!", _
            "Wrong Data Format", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Protected Function IsFill_AtLeastOneRowInGrid(ByVal dtGrid As DataTable) As Boolean
        If dtGrid.Rows.Count = 0 Then
            MessageBox.Show("Please fill data in table!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function
    Protected Function IsFill_DataInTextBox(ByVal txtBox As String) As Boolean
        If txtBox = "" Then
            MessageBox.Show("Please fill data in textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function



End Class
