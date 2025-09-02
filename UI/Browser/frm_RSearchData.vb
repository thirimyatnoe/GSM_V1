Imports System.Windows.Forms
Imports CommonInfo
Imports BusinessRule


Public Class SearchData

    Implements IDisposable

    Delegate Sub SearchData(ByVal DataSource As Object)
    Public Event SelectRecord As SearchData
    Private MultiSelection As Boolean = True
    Private FindFast_Caption As String
    Public RaseEvents As Boolean = False
    Public Shared MyanmarFont As Font
    Public Shared EnglishFont As Font
    Private Exit_Flag As Boolean
    Private Filter_Flag As Boolean
    Private Formname As String
    Public Shared dcaption As String
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

        'InitializeComponent()

        'Me.MultiSelection = MultiSelect
        'Dim dtnew As DataTable
        'If Table = "Customer" Then
        '    dtnew = _CustomerController.GetAllCustomerList
        'End If

        'DT = CType(dtnew, DataTable)
        'FindFast_Caption = DisplayCaption
        'ReturnDT = Nothing
        'ReturnDataRow = Nothing
    End Sub

    Private Sub Search_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DT = Nothing
        ReturnDT = Nothing
        ReturnDataRow = Nothing
    End Sub

    Private Sub SearchData_GiveFeedback(sender As Object, e As GiveFeedbackEventArgs) Handles Me.GiveFeedback

    End Sub

    Private Sub Search_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            DT = Nothing
            ReturnDT = Nothing
            ReturnDataRow = Nothing
            Me.ParentForm.Close()
        End If
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

        'If Global_VisualStyle = EnumSetting.VisualStyle.Office2010Black Then
        '    cmdSelect.VisualStyle = EnumSetting.VisualStyle.Office2010Black
        '    cmdCancel.VisualStyle = EnumSetting.VisualStyle.Office2010Black
        'ElseIf Global_VisualStyle = EnumSetting.VisualStyle.Office2010Silver Then
        '    cmdSelect.VisualStyle = EnumSetting.VisualStyle.Office2010Silver
        '    cmdCancel.VisualStyle = EnumSetting.VisualStyle.Office2010Silver
        'End If

        'DG.FilterBarStyle.GradientMode = C1.Win.C1TrueDBGrid.GradientModeEnum.Vertical
        'DG.FilterBarStyle.ForeColor = Color.Maroon


        txtSelect.Font = New Font("Myanmar3", 9.0)
        txtSelect.Select()
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
            ' DG.AutoGenerateColumns = False
            DG.Styles.Clear()
            DG.DataSource = DT.DefaultView
            Call DG_Format()

            'If DT.Rows.Count > 0 Then
            '    ' DG.Rows(0).Selected = True
            '    DG.SelectedRows.Add(0)
            '    DG.Row = 0
            'End If
        Else
            MsgBox("There is no record.", vbInformation)
            Exit_Flag = True
        End If

    End Sub

    Private Sub DG_Format()
        Dim grd_width As Long

        Dim lbl As New System.Windows.Forms.Label
        Dim dc As New C1.Win.C1TrueDBGrid.C1DataColumn

        Me.Text = FindFast_Caption

        DG.Styles.Clear()
        With DG

            .BorderStyle = BorderStyle.None
            .Caption = FindFast_Caption
            .FilterBar = True
            .MultiSelect = True
            .Font = New Font("Myanmar3", 9.0!)
            .CaptionStyle.Font = New Font("Myanmar3", 9.0!)
            .RowHeight = 20
            .FilterBar = True
            .AllowFilter = False
            '.AlternatingRows = True
            .SelectedStyle.BackColor = Color.DodgerBlue
            .SelectedStyle.ForeColor = Color.White
            '.OddRowStyle.BackColor = Color.LightBlue
            '.EvenRowStyle.BackColor = Color.GhostWhite
        End With

        Dim i As Integer

        Dim captiontext As String
        lbl.AutoSize = True
        With DG


            For i = 0 To DT.Columns.Count - 1
                With DG


                    captiontext = DT.Columns(i).Caption
                    lbl.Text = DT.Columns(i).Caption

                    If DT.Columns(i).Caption.StartsWith("@") Then
                        .Splits(0).DisplayColumns(i).Width = 0
                        .Splits(0).DisplayColumns(i).Visible = False
                    Else
                        If DT.Columns(i).Caption.StartsWith("$") Then
                            .Columns(lbl.Text).Caption = captiontext.Remove(0, 1)
                            .Splits(0).DisplayColumns(lbl.Text).Width = lbl.Width
                        ElseIf DT.Columns(i).Caption.EndsWith("_") Then
                            .Columns(lbl.Text).Caption = captiontext.Remove(captiontext.Length - 1, 1)
                            .Splits(0).DisplayColumns(lbl.Text).Width = lbl.Width + 15
                        Else
                            .Columns(lbl.Text).Caption = captiontext
                            .Splits(0).DisplayColumns(lbl.Text).Width = lbl.Width + 50
                        End If
                        .Splits(0).DisplayColumns(lbl.Text).Visible = True
                        .Splits(0).DisplayColumns(lbl.Text).Locked = True
                        .Splits(0).DisplayColumns(lbl.Text).Style.Font = New Font("Myanmar3", 9.0!)
                        .Splits(0).DisplayColumns(lbl.Text).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
                    End If

                End With
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

    'Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
    '    Dim drv As DataRowView
    '    Dim dr As DataRow
    '    With DG
    '        If .SelectedRows.Count > 0 Then
    '            drv = DT.DefaultView.Item(.SelectedRows(0).Index)
    '            dr = drv.Row
    '            ReturnDataRow = dr
    '            If MultiSelection = True Then
    '                For i As Integer = 0 To .SelectedRows.Count - 1
    '                    DT.DefaultView.Item(.SelectedRows(i).Index).Row.SetModified()
    '                Next
    '                DT.DefaultView.RowStateFilter = DataViewRowState.ModifiedOriginal
    '                ReturnDT = DT.DefaultView.ToTable
    '            End If
    '        End If
    '    End With
    '    Me.ParentForm.Close()
    'End Sub

    'Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    '    DT = Nothing
    '    ReturnDT = Nothing
    '    ReturnDataRow = Nothing
    '    Me.ParentForm.Close()
    'End Sub

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
    Private Sub DG_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DG.GotFocus
        'DG.SelectedRows.Add(DG.Row)
        'DG.Row = DG.Row
    End Sub

    Private Sub DG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DG.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Call cmdSelect_Click(sender, e)
        ElseIf e.KeyCode = Keys.Tab Then
            cboSList.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            DT = Nothing
            ReturnDT = Nothing
            ReturnDataRow = Nothing
            Me.ParentForm.Close()
        End If
    End Sub

    Private Sub cboSList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboSList.KeyDown
        Search_KeyDown(sender, e)
    End Sub

    Private Sub cboCtia_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboCtia.KeyDown
        Search_KeyDown(sender, e)
    End Sub

    Private Sub cmdSelect_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmdSelect.KeyDown
        Search_KeyDown(sender, e)
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
        dcaption = DisplayCaption
        Using SearchForm As UI.SearchData = New UI.SearchData(Table, True, DisplayCaption)
            Dim Pan As New Panel
            Pan.Width = SearchForm.Width
            Pan.Height = SearchForm.Height + 5

            Pan.Controls.Add(SearchForm)
            SearchForm.Dock = DockStyle.Fill

            Using NewForm As C1.Win.C1Ribbon.C1RibbonForm = New C1.Win.C1Ribbon.C1RibbonForm
                'Using NewForm As Form = New Form
                NewForm.Width = Pan.Width + 5
                NewForm.Height = Pan.Height + 26
                NewForm.Text = "Search"
                NewForm.ShowInTaskbar = False
                NewForm.MinimizeBox = False
                NewForm.MaximizeBox = True
                NewForm.FormBorderStyle = FormBorderStyle.FixedToolWindow
                NewForm.WindowState = FormWindowState.Normal
                NewForm.StartPosition = FormStartPosition.CenterScreen
                NewForm.Controls.Add(Pan)
                Pan.Dock = DockStyle.Fill
                'NewForm.BackgroundColor = Color.Gray
                NewForm.ShowDialog()
                RSYSID = SearchForm.ReturnDataRow
            End Using
        End Using
        Return RSYSID
    End Function

    Public Shared Function FindFast(ByVal Table As Object, ByVal MultiSelect As Boolean, ByVal DisplayCaption As String) As DataTable
        Dim RDT As New DataTable
        Using SearchForm As UI.SearchData = New UI.SearchData(Table, MultiSelect, DisplayCaption)
            Dim Pan As New Panel
            dcaption = DisplayCaption
            Pan.Width = SearchForm.Width
            Pan.Height = SearchForm.Height + 5
            Pan.Controls.Add(SearchForm)
            SearchForm.Dock = DockStyle.Fill
            Using NewForm As C1.Win.C1Ribbon.C1RibbonForm = New C1.Win.C1Ribbon.C1RibbonForm
                'Using NewForm As Form = New Form
                NewForm.Width = Pan.Width + 5
                NewForm.Height = Pan.Height + 26
                NewForm.Text = "Search"
                NewForm.ShowInTaskbar = False
                NewForm.MinimizeBox = False
                NewForm.MaximizeBox = True
                NewForm.FormBorderStyle = FormBorderStyle.FixedToolWindow
                NewForm.WindowState = FormWindowState.Normal
                NewForm.StartPosition = FormStartPosition.CenterScreen
                NewForm.Controls.Add(Pan)
                Pan.Dock = DockStyle.Fill
                NewForm.Text = DisplayCaption
                NewForm.Tag = DisplayCaption
                NewForm.ShowDialog()
                'openform = form
                Return SearchForm.ReturnDT
            End Using
        End Using
        '  Return ReturnDT
    End Function


    Private Sub txtSelect_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSelect.KeyDown
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            DG.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            DT = Nothing
            ReturnDT = Nothing
            ReturnDataRow = Nothing
            Me.ParentForm.Close()
        End If

    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        Dim drv As DataRowView
        Dim dr As DataRow
        With DG
            If .SelectedRows.Count > 0 Then
                drv = DT.DefaultView.Item(DG.Row)
                dr = drv.Row
                ReturnDataRow = dr
                If MultiSelection = True Then
                    For i As Integer = 0 To .SelectedRows.Count - 1
                        DT.DefaultView.Item(.SelectedRows(i)).Row.SetModified()
                    Next
                    DT.DefaultView.RowStateFilter = DataViewRowState.ModifiedOriginal
                    ReturnDT = DT.DefaultView.ToTable
                End If
            Else
                drv = DT.DefaultView.Item(DG.Row)
                dr = drv.Row
                ReturnDataRow = dr
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
    Private Sub DG_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DG.MouseClick
        If MultiSelection = False Then
            DG.SelectedRows.Add(DG.Row)
            DG.Row = DG.Row
        End If

    End Sub

    Private Sub DG_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles DG.RowColChange

    End Sub

    Private Sub CmdNew_Click(sender As Object, e As EventArgs) Handles CmdNew.Click
        Select Case Me.Text
            Case "Stock List"
                If MDI.CheckForm("frm_PurchaseRow") Then
                    Exit Sub
                End If
                'frm_MDI.Frm_Name = "Stock"
                Dim frm As New frm_PurchaseRow
                frm.ShowDialog()
                'DT = New DataTable
                'DT = _GetStock.GetStockList("")
                DG.DataSource = DT
                DG_Format()


                'Case "Customer List"
                '    Dim dtCustomer As New DataTable
                '    If frm_MDI.GetExitForm("frm_RibbonCustomer") Then
                '        Exit Sub
                '        frm_RibbonCustomer.Dispose()
                '    End If
                '    frm_MDI.Frm_Name = "Customer"
                '    Dim frm As New frm_RibbonCustomer
                '    frm.openfrom = "notmenu"
                '    frm.ShowDialog()
                '    dtCustomer = _CustomerController.GetCustomerList("FT")
                '    DT = New DataTable
                '    DT = dtCustomer
                '    DG.DataSource = DT
                '    DG_Format()
                'Case "Supplier List"
                '    If frm_MDI.GetExitForm("frm_RibbonCustomer") Then
                '        Exit Sub
                '        frm_RibbonCustomer.Dispose()
                '    End If
                '    frm_MDI.Frm_Name = "Supplier"
                '    Dim frm As New frm_RibbonSupplier
                '    frm.openfrom = "notmenu"
                '    frm.ShowDialog()
                '    DT = New DataTable
                '    DT = _GetSupplier.GetSupplierList("FT")
                '    DG.DataSource = DT
                '    DG_Format()
        End Select
        'If Me.Text = "Stock List" Then
    End Sub
    Private Sub DG_FilterChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles DG.FilterChange
        ' build our filter expression
        Dim sb As New System.Text.StringBuilder()

        Dim dc As C1.Win.C1TrueDBGrid.C1DataColumn
        For Each dc In Me.DG.Columns
            If dc.FilterText.Length > 0 Then
                If sb.Length > 0 Then
                    sb.Append(" AND ")
                End If

                If dc.DataField.StartsWith("$") Then
                    sb.Append(("Convert([" + dc.DataField + "], 'System.String') = " + "'" + Replace(dc.FilterText, "'", "''") + "'"))
                Else
                    sb.Append(("Convert([" + dc.DataField + "], 'System.String') like" + "'%" + Replace(dc.FilterText, "'", "''") + "%'"))
                End If

            End If
        Next dc
        ' filter the data
        DT.DefaultView.RowFilter = sb.ToString()

        DG.SelectedRows.Clear()

        If MultiSelection = False Then
            DG.SelectedRows.Add(DG.Row)
            DG.Row = DG.Row

        End If
    End Sub

End Class
