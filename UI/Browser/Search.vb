Public Class Search
    Implements IDisposable

    Delegate Sub Search(ByVal DataSource As Object)
    Public Event SelectRecord As Search
    Private MultiSelection As Boolean = False
    Private FindFast_Caption As String
    Public RaseEvents As Boolean = False
    Public Shared MyanmarFont As Font
    Public Shared EnglishFont As Font
    Private Exit_Flag As Boolean
    Private Filter_Flag As Boolean
    Private DT As DataTable
    Public ReturnDT As New DataTable
    Public ReturnDataRow As DataRow

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal Table As Object, ByVal MultiSelect As Boolean, ByVal DisplayCaption As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.MultiSelection = MultiSelect
        DT = CType(Table, DataTable)
        FindFast_Caption = DisplayCaption
        ReturnDT = Nothing
        ReturnDataRow = Nothing
        MyanmarFont = New Font("Myanmar3", 9.0!)
    End Sub

    Private Sub Search_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DT = Nothing
        ReturnDT = Nothing
        ReturnDataRow = Nothing
    End Sub

    Private Sub Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboCtia.Items.Clear()
        cboCtia.Items.Add("Contains")
        cboCtia.Items.Add("Starts with")
        cboCtia.SelectedIndex = 0

        Exit_Flag = False
        Filter_Flag = True

        Me.Text = FindFast_Caption
        Me.MinimumSize = Me.Size
        DataField_Display()
    End Sub

    Private Sub DataField_Display()
        cboSList.Items.Clear()

        For Each dcol As DataColumn In DT.Columns
            'If dcol.Caption.StartsWith("@") = False Then cboSList.Items.Add(dcol.Caption)

            If dcol.Caption.StartsWith("@") = False AndAlso dcol.Caption.StartsWith("$") = False Then
                cboSList.Items.Add(dcol.Caption)
            End If


        Next

        If (cboSList.Items.Count > 0) Then cboSList.SelectedIndex = 0

        If (DT.Rows.Count > 0) Then
            DG.AutoGenerateColumns = False
            DG.DataSource = DT.DefaultView
            Call DG_Format()

            If DT.Rows.Count > 0 Then
                DG.Rows(0).Selected = True
            End If
        Else
            MsgBox("There is no record.", vbInformation)
            Exit_Flag = True
        End If

    End Sub

    Private Sub DG_Format()
        Dim grd_width As Long
        Dim flgVisible As Boolean
        Dim lbl As New System.Windows.Forms.Label
        Dim dc As System.Windows.Forms.DataGridViewColumn
        Dim dcCheck As DataGridViewCheckBoxColumn
        Me.Text = FindFast_Caption

        With DG
            .Font = New Font("Myanmar3", 9.0!)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = MultiSelection

            lbl.AutoSize = True
            grd_width = 0

            For i As Integer = 0 To DT.Columns.Count - 1
                lbl.Text = DT.Columns(i).Caption
                flgVisible = True

                dc = New DataGridViewColumn(New DataGridViewTextBoxCell())
                dc.ReadOnly = True

                If DT.Columns(i).Caption.EndsWith("_") Then
                    dc.DefaultCellStyle.Font = MyanmarFont
                    dc.Name = DT.Columns(i).Caption.Remove(DT.Columns(i).Caption.Length - 1, 1)
                Else
                    dc.DefaultCellStyle.Font = New Font("Myanmar3", 9.0!)
                    dc.Name = DT.Columns(i).Caption
                End If

                If DT.Columns(i).Caption.StartsWith("@") Then
                    dc.Name = dc.Name.Remove(0, 1)
                    flgVisible = False
                Else
                    dc.Width = lbl.Width + Len(dc.Name) + 30
                    grd_width += lbl.Width
                End If

                If DT.Columns(i).Caption.StartsWith("$") Then
                    dcCheck = New DataGridViewCheckBoxColumn
                    dcCheck.Name = DT.Columns(i).Caption.Remove(0, 1)
                    dcCheck.DataPropertyName = DT.Columns(i).Caption
                    dcCheck.ReadOnly = True
                    .Columns.Add(dcCheck)
                    .Columns(dcCheck.Name).Visible = flgVisible
                    GoTo CheckBoxloop
                End If


                dc.DataPropertyName = DT.Columns(i).Caption
                dc.HeaderText = dc.Name

                .Columns.Add(dc)

                .Columns(dc.Name).Visible = flgVisible
CheckBoxloop:

            Next

            If grd_width > Me.Width - 80 Then
                If grd_width > 750 Then
                    Me.Width = 750
                Else
                    Me.Width = grd_width + 80
                End If
            End If
        End With
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        Dim drv As DataRowView
        Dim dr As DataRow
        With DG
            If .SelectedRows.Count > 0 Then
                drv = DT.DefaultView.Item(.SelectedRows(0).Index)
                dr = drv.Row
                ReturnDataRow = dr
                If MultiSelection = True Then
                    For i As Integer = 0 To .SelectedRows.Count - 1
                        DT.DefaultView.Item(.SelectedRows(i).Index).Row.SetModified()
                    Next
                    DT.DefaultView.RowStateFilter = DataViewRowState.ModifiedOriginal
                    ReturnDT = DT.DefaultView.ToTable
                End If
            End If
        End With
        Me.ParentForm.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DT = Nothing
        ReturnDT = Nothing
        ReturnDataRow = Nothing
        Me.ParentForm.Close()
    End Sub

    Private Sub txtSelect_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSelect.TextChanged
        Ctia()
    End Sub
    Private Sub Ctia()
        If (Trim(txtSelect.Text) <> "") Then
            If (cboCtia.SelectedIndex = 0) Then
                If cboSList.Text.EndsWith("_") Then
                    DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like  '%" + Replace(txtSelect.Text, "'", "''") + "%'"
                Else
                    DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like  '%" + Replace(txtSelect.Text, "'", "''") + "%'"
                End If
            Else
                If cboSList.Text.EndsWith("_") Then
                    DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like  '" + Replace(txtSelect.Text, "'", "''") + "%'"
                Else
                    DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like  '" + Replace(txtSelect.Text, "'", "''") + "%'"
                End If
            End If
        Else
            DT.DefaultView.RowFilter = ""
        End If
        If DT.Rows.Count > 0 Then
            DG.DataSource = DT.DefaultView
        End If
    End Sub
    'Private Sub Ctia()
    '    If (Trim(txtSelect.Text) <> "") Then
    '        If (cboCtia.SelectedIndex = 0) Then
    '            If cboSList.Text.EndsWith("_") Then
    '                DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like  '%" + Replace(txtSelect.Text, "'", "''") + "%'"
    '            Else

    '            End If
    '        Else
    '            If cboSList.Text.EndsWith("_") Then
    '                DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like  '" + Replace(txtSelect.Text, "'", "''") + "%'"
    '            Else
    '                DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like  '" + Replace(txtSelect.Text, "'", "''") + "%'"
    '            End If
    '        End If
    '    Else
    '        DT.DefaultView.RowFilter = ""
    '    End If
    '    If DT.Rows.Count > 0 Then
    '        DG.DataSource = DT.DefaultView
    '    End If
    'End Sub

    Private Sub DG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DG.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Call cmdSelect_Click(sender, e)
        End If
    End Sub

    Private Sub cboSList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSList.SelectedIndexChanged
        If cboSList.Text.EndsWith("_") Then
            txtSelect.Font = MyanmarFont
            DT.CaseSensitive = False
        Else
            txtSelect.Font = EnglishFont
            DT.CaseSensitive = False
        End If
    End Sub

    Private Sub DG_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DG.DoubleClick
        Call cmdSelect_Click(sender, e)
    End Sub

    Public Shared Function FindFast(ByVal Table As Object, ByVal DisplayCaption As String) As DataRow
        Dim RSYSID As DataRow = Nothing
        Using SearchForm As UI.Search = New UI.Search(Table, False, DisplayCaption)
            Dim Pan As New Panel
            Pan.Width = SearchForm.Width
            Pan.Height = SearchForm.Height + 5
            Pan.Controls.Add(SearchForm)

            Using NewForm As Form = New Form
                NewForm.Width = Pan.Width + 45
                NewForm.Height = Pan.Height + 26
                NewForm.Text = "Search"
                NewForm.ShowInTaskbar = False
                NewForm.MinimizeBox = False
                NewForm.MaximizeBox = False
                NewForm.StartPosition = FormStartPosition.CenterScreen
                NewForm.Controls.Add(Pan)
                NewForm.ShowDialog()
                RSYSID = SearchForm.ReturnDataRow
            End Using
        End Using
        Return RSYSID
    End Function

    Public Shared Function FindFast(ByVal Table As Object, ByVal MultiSelect As Boolean, ByVal DisplayCaption As String) As DataTable
        Using SearchForm As UI.Search = New UI.Search(Table, MultiSelect, DisplayCaption)
            Dim Pan As New Panel
            Pan.Width = SearchForm.Width
            Pan.Height = SearchForm.Height + 5
            Pan.Controls.Add(SearchForm)

            Using NewForm As Form = New Form
                NewForm.Width = Pan.Width + 5
                NewForm.Height = Pan.Height + 26
                NewForm.Text = "Search"
                NewForm.ShowInTaskbar = False
                NewForm.MinimizeBox = False
                NewForm.MaximizeBox = False
                NewForm.StartPosition = FormStartPosition.CenterScreen
                NewForm.Controls.Add(Pan)
                NewForm.ShowDialog()
                Return SearchForm.ReturnDT
            End Using
        End Using
        'Return RSYSID
    End Function

    'Private Sub cboCtia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCtia.SelectedIndexChanged
    '    Ctia()
    'End Sub
End Class
