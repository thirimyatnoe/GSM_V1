Imports System.Windows.Forms
Imports SMS.CommonInfo
Imports SMS.BusinessRule
Imports System.Windows.Forms.Design
Imports SMS.DataAccess 
Imports CommonInfo

Public Class SearchControlHost
    Inherits UserControl



    Private FiLastRow As Integer = -1
    Private _selectedData As DataTable
    Dim FRM_Name As String
    Delegate Sub SearchData(ByVal DataSource As Object)
    Public Event SelectRecord As SearchData
    Private MultiSelection As Boolean = False
    Private FindFast_Caption As String
    Public RaseEvents As Boolean = False
    Public Shared MyanmarFont As Font
    Public Shared EnglishFont As Font
    Private Exit_Flag As Boolean
    Private Filter_Flag As Boolean
    Private DT As DataTable
    Public ReturnDT As New DataTable
    Public ReturnDataTable As DataTable
    Public ReturnDataRow As DataRow
    'Private _CompanyController As New CompanyProfile.CompanyProfileController
    'Private _CustomerController As New Customer.CustomerController
    'Private _CustGroupController As New CustomerGroup.CustomerGroupController 'mly add for customer group
    'Private _SupplierController As New Supplier.SupplierController
    'Private _SalePersonController As New SalePerson.SalePersonController
    'Private _StockController As New Stock.StockController
    'Private _CategoryController As New Category.CategoryController
    'Private _KeywordController As New Keyword.KeywordController
    'Private _LocalizedController As New Localized.LocalizedController
    'Private _settingcontroller As New Setting.SettingController
    'Private _AdjustmentController As New AdjustmentStock.AdjustmentStockController
    'Private _SaleController As New Sale.SaleController
    'Private _PurchaseController As New Purchase.PurchaseController
    'Private _StockAttributeController As New StockAttribute.StockAttributeController
    'Private _LoginController As New BusinessRule.UserManagement.LogIn
    'Private _CounterController As New Counter.CounterController
    'Private _LocationController As New Location.LocationController
    'Private _ExpenseController As New Expense.ExpenseController
    'Private _IncomeController As New Income.IncomeController
    'Private _User As New UserManagement.User 'mly 3:00 PM 3/11/2016
    '  Dim Name As String
    'Private _editor As ListViewHost
    Private languagefont As Font
    Private inputfont As Font
    Private IsBig As Boolean = False

    'Public Sub New(ByVal Table As Object, ByVal MultiSelect As Boolean, ByVal DisplayCaption As String)
    ' This call is required by the Windows Form Designer.
    ' InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    Public Sub New(ByVal Table As Object, ByVal MultiSelect As Boolean, ByVal DisplayCaption As String, ByVal cri As String)

        'TMN 6 / 8 / 2018
        'InitializeComponent()
        'Me.MultiSelection = MultiSelect
        'DT = CType(Table, DataTable)
        'FindFast_Caption = DisplayCaption
        'ReturnDT = Nothing
        'ReturnDataRow = Nothing
        'MyanmarFont = New Font("Myanmar3", 9.0!)
        '-------------------------------------------
        ' This call is required by the Windows Form Designer.
        'InitializeComponent()
        '' Add any initialization after the InitializeComponent() call.
        'Me.MultiSelection = MultiSelect


        'DT = CType(Table, DataTable)
        'FindFast_Caption = DisplayCaption
        'ReturnDT = Nothing
        'ReturnDataRow = Nothing

        '_editor = New ListViewHost()

        DT = Nothing
        'DG.DataSource = DT
        InitializeComponent()

        Me.MultiSelection = MultiSelect
        Dim dtnew As New DataTable
        FRM_Name = Table.ToString

        Name = Table.ToString
        DT = CType(Table, DataTable)
        FindFast_Caption = DisplayCaption
        ReturnDT = Nothing
        ReturnDataRow = Nothing
        DG.Styles.Clear()
        DG.DataSource = DT.DefaultView
        Call DG_Format(IsBig)
        ReturnDT = DT.DefaultView.ToTable
        ''''''''''''

        cboCtia.Items.Clear()
        cboCtia.Items.Add("Contains")
        cboCtia.Items.Add("Starts with")
        cboCtia.SelectedIndex = 0

        Exit_Flag = False
        Filter_Flag = True

        Me.Text = FindFast_Caption
        Me.MinimumSize = Me.Size
        DataField_Display()
        txtSelect.Focus()

        DG.FilterBarStyle.BackColor = Color.AliceBlue
        DG.FilterBarStyle.BackColor2 = Color.White
        DG.FilterBarStyle.GradientMode = C1.Win.C1TrueDBGrid.GradientModeEnum.Vertical
        '.FilterBar = True
        DG.FilterBarStyle.ForeColor = Color.Black

        ReturnDT = Nothing
        ReturnDataRow = Nothing
    End Sub

    Private Sub SearchControlHost_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        'DT.DefaultView.RowStateFilter = DataViewRowState.CurrentRows
    End Sub

    Private Sub SearchControlHost_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        'DT.DefaultView.RowStateFilter = DataViewRowState.CurrentRows
    End Sub

    Private Sub Search_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            DT = Nothing
            ReturnDT = Nothing
            ReturnDataRow = Nothing
            Me.ParentForm.Close()
        End If
    End Sub

    Private Sub SearchControlHost_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        'DT = Nothing
        'ReturnDT = Nothing
        'ReturnDataRow = Nothing
        'Dim drv As DataRowView
        'Dim dr As DataRow

        'With DG
        '    If .SelectedRows.Count > 0 Then

        '        drv = DT.DefaultView.Item(DG.Row)
        '        dr = drv.Row
        '        ReturnDataRow = dr
        '        If MultiSelection = True Then
        '            DT.DefaultView.RowStateFilter = DataViewRowState.CurrentRows
        '            'For i As Integer = 0 To .SelectedRows.Count - 1
        '            '    DT.DefaultView.Item(.SelectedRows(i)).Row.SetModified()
        '            'Next
        '            DT.DefaultView.RowStateFilter = DataViewRowState.ModifiedOriginal
        '            ReturnDT = DT.DefaultView.ToTable

        '            ' SelectedData = ReturnDT

        '            Dim formColl As FormCollection = Application.OpenForms
        '            Dim count As Int32 = formColl.Count()

        '            For Each f As Form In formColl
        '                If f.Name = "MDI" Then

        '                    f.BringToFront()
        '                    Exit For
        '                End If

        '            Next

        '            ' Me.ParentForm.Close()


        '        End If
        '    End If
        'End With
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
        txtSelect.Focus()

        DG.FilterBarStyle.BackColor = Color.AliceBlue
        DG.FilterBarStyle.BackColor2 = Color.White
        DG.FilterBarStyle.GradientMode = C1.Win.C1TrueDBGrid.GradientModeEnum.Vertical

        DG.FilterBarStyle.ForeColor = Color.Black

        Me.DG.RecordSelectors = True


        txtSelect.Font = New Font("Myanmar3", 9.0)
        txtSelect.Select()
    End Sub
    Private Sub _form_Deactivate(ByVal sender As Object, ByVal e As EventArgs)
        CType(Me, IWindowsFormsEditorService).CloseDropDown()
    End Sub

    ' close drop down when the user presses the Esc button
    Private Sub _form_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Escape Then
            CType(Me, IWindowsFormsEditorService).CloseDropDown()
            e.Handled = True
        End If
    End Sub
    Private Sub DataField_Display()
        cboSList.Items.Clear()

        For Each dcol As DataColumn In DT.Columns
            If dcol.Caption.StartsWith("@") = False AndAlso dcol.Caption.StartsWith("$") = False Then
                cboSList.Items.Add(dcol.Caption)
            End If


        Next

        If (cboSList.Items.Count > 0) Then cboSList.SelectedIndex = 0

        If (DT.Rows.Count > 0) Then
            DG.Styles.Clear()
            DG.DataSource = DT.DefaultView
            Call DG_Format(IsBig)

        Else
            MsgBox("There is no record.", vbInformation)
            Exit_Flag = True
        End If

    End Sub

    Private Sub DG_Format(ByVal isBig As Boolean)
        Dim grd_width As Long
        Dim lbl As New System.Windows.Forms.Label
        Dim chkbox As New System.Windows.Forms.CheckBox
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

            '.FilterBarStyle.BackColor = Color.AliceBlue
            '.FilterBarStyle.BackColor2 = Color.White
            '.FilterBarStyle.GradientMode = C1.Win.C1TrueDBGrid.GradientModeEnum.Vertical
            '.FilterBar = True
            '.FilterBarStyle.ForeColor = Color.Black
            '.Caption = FindFast_Caption
            '.BackColor = Color.AliceBlue
            '.BorderStyle = BorderStyle.None
            'If Global_VisualStyle = EnumSetting.VisualStyle.Office2010Blue Then
            '    .CaptionStyle.BackColor = System.Drawing.Color.FromArgb(164, 195, 235)
            '    .CaptionStyle.ForeColor = Color.Black
            'ElseIf Global_VisualStyle = EnumSetting.VisualStyle.Office2010Silver Then
            '    .CaptionStyle.BackColor = System.Drawing.Color.FromArgb(170, 174, 181)
            '    .CaptionStyle.ForeColor = Color.Black
            'ElseIf Global_VisualStyle = EnumSetting.VisualStyle.Office2010Black Then
            '    .CaptionStyle.BackColor = System.Drawing.Color.FromArgb(70, 70, 70)
            '    .CaptionStyle.ForeColor = Color.White
            '    Label1.ForeColor = Color.White
            '    GroupBox1.ForeColor = Color.White
            'End If
            '' .CaptionStyle.BackColor = Color.Maroon

            ''.CaptionStyle.Borders.Color = Color.Maroon
            '.HeadingStyle.BackColor = Color.WhiteSmoke
            '.HeadingStyle.BackColor2 = Color.White
            '.HeadingStyle.ForeColor = Color.Black
            '.MultiSelect = CType(MultiSelection, C1.Win.C1TrueDBGrid.MultiSelectEnum)
            '.Font = New Font("Myanmar3", 9.0!)
            '.RowHeight = 20
            '.AlternatingRows = True
            '.OddRowStyle.BackColor = Color.AliceBlue
            '.EvenRowStyle.BackColor = Color.GhostWhite
            '.SelectedStyle.BackColor = Color.Gainsboro
            '.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.Simple


        End With



        Dim i As Integer
        Dim captiontext As String
        'Dim lbl As New System.Windows.Forms.Label
        lbl.AutoSize = True
        With DG
            ''Dim dcIsReturn As New System.Windows.Forms.CheckBox
            ' ''With dcIsReturn
            ' ''    .Text = "IsCheck"
            ' ''    .Name = "IsCheck"
            ' ''    .Visible = True
            ' ''    .Width = 50
            ' ''End With
            ''.Splits(0).DisplayColumns(0).Width = dcIsReturn.Width + 15
            ''.Splits(0).DisplayColumns(0).Visible = True
            ''.Columns(0).Caption = "ISCheck"

            For i = 0 To DT.Columns.Count - 2
                '  For i = 0 To DT.Columns.Count - 1
                With DG

                    captiontext = DT.Columns(i).Caption
                    lbl.Text = DT.Columns(i).Caption

                    .Splits(0).DisplayColumns(i).Locked = True

                    lbl.Text = DT.Columns(i).Caption
                    If DT.Columns(i).Caption.StartsWith("@") Then
                        .Splits(0).DisplayColumns(i).Width = 0
                        .Splits(0).DisplayColumns(i).Visible = False

                    Else

                        .Splits(0).DisplayColumns(lbl.Text).Width = lbl.Width + 15
                        .Splits(0).DisplayColumns(lbl.Text).Visible = True
                        .Columns(lbl.Text).Caption = captiontext
                        .Splits(0).DisplayColumns(0).Locked = False
                    End If

                End With
            Next


            For i = 0 To DT.Columns.Count - 1
                lbl.Text = DT.Columns(i).Caption
                If isBig = True Then
                    .Splits(0).DisplayColumns(lbl.Text).Width = lbl.Width + 100
                End If
            Next
            If grd_width > Me.Width - 80 Then
                If grd_width > 750 Then
                    Me.Width = 750
                Else
                    Me.Width = CInt(grd_width + 80)
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
        'sb.Append(("Convert([" + dc.DataField + "], 'System.String') = " + "'" + Replace(dc.FilterText, "'", "''") + "'"))
        '        Else
        'sb.Append(("Convert([" + dc.DataField + "], 'System.String') like" + "'%" + Replace(dc.FilterText, "'", "''") + "%'"))

        If (Trim(txtSelect.Text) <> "") Then
            If (cboCtia.SelectedIndex = 1) Then
                DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like '" + Replace(txtSelect.Text, "'", "''") + "%'"
            Else
                DT.DefaultView.RowFilter = "Convert([" + cboSList.Text + "], 'System.String') like '%" + txtSelect.Text + "%'"
            End If
        Else
            DT.DefaultView.RowFilter = ""
        End If
        If DT.Rows.Count > 0 Then
            DG.DataSource = DT.DefaultView
        End If

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

        If DT.Rows.Count > 0 Then
            DG.DataSource = DT.DefaultView
        End If
    End Sub

    Private Sub DG_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
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

    Private Sub cboSList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSList.SelectedIndexChanged
        If cboSList.Text.EndsWith("_") Then
            txtSelect.Font = MyanmarFont
            DT.CaseSensitive = True
        Else
            txtSelect.Font = EnglishFont
            DT.CaseSensitive = False
        End If
    End Sub
    Private Sub Search_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DT = Nothing
        ReturnDT = Nothing
        ReturnDataRow = Nothing
        ' Me.ParentForm.Close()
    End Sub
    Private Sub DG_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Call cmdSelect_Click(sender, e)
    End Sub
   

    Public Shared Function FindFast(ByVal Table As Object, ByVal DisplayCaption As String) As DataTable
        Dim RSYSID As DataTable = Nothing
        Using SearchForm As UI.SearchControlHost = New UI.SearchControlHost(Table, True, DisplayCaption, "")
            Dim Pan As New Panel
            Pan.Width = SearchForm.Width
            Pan.Height = SearchForm.Height + 5

            Pan.Controls.Add(SearchForm)
            SearchForm.Dock = DockStyle.Fill
            Using NewForm As C1.Win.C1Ribbon.C1RibbonForm = New C1.Win.C1Ribbon.C1RibbonForm
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
                NewForm.ShowDialog()
                RSYSID = SearchForm.ReturnDT
            End Using
        End Using

        Return RSYSID
    End Function

    Public Shared Function FindFast(ByVal Table As Object, ByVal MultiSelect As Boolean, ByVal DisplayCaption As String) As DataTable

        Using SearchForm As UI.SearchControlHost = New UI.SearchControlHost(Table, MultiSelect, DisplayCaption, "")
            SearchForm.Show()
            Return SearchForm.ReturnDT
        End Using
        'Return RSYSID
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

    Public Function OpenForm() As DataTable

        Return ReturnDT
    End Function
    Public Property SelectedData() As DataTable
        Get
            Dim result As DataTable = Nothing
            Try

                Dim drv As DataRowView
                Dim dr As DataRow

                With DG
                    If .SelectedRows.Count > 0 Then

                        drv = DT.DefaultView.Item(DG.Row)
                        dr = drv.Row
                        ReturnDataRow = dr
                        Dim flag As Boolean = False
                        If MultiSelection = True Then

                            For i As Integer = 0 To .SelectedRows.Count - 1
                                DT.DefaultView.Item(.SelectedRows(i)).Row.SetModified()
                            Next
                            DT.DefaultView.RowStateFilter = DataViewRowState.ModifiedOriginal
                            ReturnDT = DT.DefaultView.ToTable
                        End If
                    Else
                        ReturnDT = New DataTable
                    End If
                End With

            Catch ex As Exception
                ReturnDT = DT.DefaultView.ToTable
            End Try
            result = ReturnDT
            Return result
        End Get
        Set(ByVal value As DataTable)

            If value.Rows.Count > 0 Then
                DG.SelectedRows.Clear()
                For i As Integer = 0 To value.Rows.Count - 1

                    For x As Integer = 0 To DT.Rows.Count - 1
                        If DT.Rows(x).Item(0).ToString = value.Rows(i).Item(0).ToString Then
                            DG.SelectedRows.Add(x)
                        End If
                    Next
                Next
            End If
            _selectedData = value
            OnSelectedDataChanged(EventArgs.Empty)
        End Set

    End Property
    Public Property TreeView() As TreeView
        Get
            Dim tmpnode As TreeNode
            Dim tmpdt As DataTable
            Dim tmpdtright As New DataTable
            Dim dr As DataRow
            Dim dtcompanyTV As New DataTable
            Dim tvMenu As New System.Windows.Forms.TreeView
            Dim argNode As New TreeNode

            tvMenu.Nodes.Clear()
            Dim oNode As New System.Windows.Forms.TreeNode
            oNode = New System.Windows.Forms.TreeNode
            Try
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0
                oNode.Text = "Application Main Menu"
                oNode.Tag = "Application Main Menu"
                tvMenu.Nodes.Add(oNode)
            Catch ex As Exception
                MsgBox("Cannot Create Initial Node:" & ex.ToString)
                '  Exit Property
            End Try

            ' dtcompanyTV = _CompanyController.GetCompanyProfileList(" Where CompanyID ='" & Company_HOID & "'")
            dtcompanyTV.DefaultView.RowFilter = " CompanyID Like '" & Company_HOID & "*'"
            tmpdt = dtcompanyTV.DefaultView.ToTable
            If tmpdt.Rows.Count = 0 Then
                argNode.ImageIndex = 1
                argNode.SelectedImageIndex = 1
            Else
                argNode.ImageIndex = 0
                argNode.SelectedImageIndex = 0
            End If

            For Each dr In tmpdt.Rows
                If dr("CompanyName").ToString.Trim <> "" And dr("CompanyName").ToString.Trim <> "-" Then
                    tmpnode = argNode.Nodes.Add(dr("CompanyID").ToString, dr("CompanyName").ToString.Trim)
                    Dim tmprightnode As New TreeNode
                    tmprightnode = tmpnode
                    tmprightnode = tmpnode.Nodes.Add("MainStore")
                    ' AddChildMenu(dr("CompanyID").ToString, tmpnode)
                End If

            Next
            tvMenu.ExpandAll()
            Return tvMenu
        End Get
        Set(ByVal value As TreeView)
            Dim tmpnode As TreeNode
            Dim tmpdt As DataTable
            Dim tmpdtright As New DataTable
            Dim dr As DataRow
            Dim dtcompanyTV As New DataTable
            Dim tvMenu As New System.Windows.Forms.TreeView
            Dim argNode As New TreeNode

            tvMenu.Nodes.Clear()
            Dim oNode As New System.Windows.Forms.TreeNode
            oNode = New System.Windows.Forms.TreeNode
            Try
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0
                oNode.Text = "Application Main Menu"
                oNode.Tag = "Application Main Menu"
                tvMenu.Nodes.Add(oNode)
            Catch ex As Exception
                MsgBox("Cannot Create Initial Node:" & ex.ToString)
                '  Exit Property
            End Try

            'dtcompanyTV = _CompanyController.GetCompanyProfileList(" Where CompanyID ='" & Company_HOID & "'")
            dtcompanyTV.DefaultView.RowFilter = " CompanyID Like '" & Company_HOID & "*'"
            tmpdt = dtcompanyTV.DefaultView.ToTable
            If tmpdt.Rows.Count = 0 Then
                argNode.ImageIndex = 1
                argNode.SelectedImageIndex = 1
            Else
                argNode.ImageIndex = 0
                argNode.SelectedImageIndex = 0
            End If

            For Each dr In tmpdt.Rows
                If dr("CompanyName").ToString.Trim <> "" And dr("CompanyName").ToString.Trim <> "-" Then
                    tmpnode = argNode.Nodes.Add(dr("CompanyID").ToString, dr("CompanyName").ToString.Trim)
                    Dim tmprightnode As New TreeNode
                    tmprightnode = tmpnode
                    tmprightnode = tmpnode.Nodes.Add("MainStore")
                    ' AddChildMenu(dr("CompanyID").ToString, tmpnode)
                End If

            Next
            tvMenu.ExpandAll()
            '  Return tvMenu
        End Set
    End Property

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        Dim drv As DataRowView
        Dim dr As DataRow
        With DG
            If .SelectedRows.Count > 0 Then
                drv = DT.DefaultView.Item(DG.Row)
                dr = drv.Row
                ReturnDataRow = dr
                If MultiSelection = True Then
                    DT.DefaultView.RowStateFilter = DataViewRowState.CurrentRows

                    For i As Integer = 0 To .SelectedRows.Count - 1
                        If DT.DefaultView.RowStateFilter = DataViewRowState.CurrentRows Then

                        End If
                        'DT.DefaultView.Item(.SelectedRows(i)).Row.SetModified()
                    Next
                    DT.DefaultView.RowStateFilter = DataViewRowState.ModifiedOriginal
                    ReturnDT = DT.DefaultView.ToTable
                    SelectedData = ReturnDT

                    Dim formColl As FormCollection = Application.OpenForms
                    Dim count As Int32 = formColl.Count()

                    For Each f As Form In formColl
                        If f.Name = "MDI" Then

                            f.BringToFront()
                            Exit For
                        End If

                    Next
                End If
            Else
                'drv = DT.DefaultView.Item(DG.Row)
                'dr = drv.Row
                'ReturnDataRow = dr
                ReturnDataRow = Nothing
                ReturnDT = Nothing
            End If

        End With
        Me.ParentForm.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DT = Nothing
        ReturnDT = Nothing
        ReturnDataRow = Nothing
        Me.ParentForm.Close()
        'Dim formColl As FormCollection = Application.OpenForms
        'Dim count As Int32 = formColl.Count()

        'For Each f As Form In formColl
        '    If f.Name = "MDI" Then
        '        f.BringToFront()
        '        Exit For
        '    End If

        'Next
    End Sub
    Private Sub ChkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSelectAll.CheckedChanged
        Dim i As Integer = 0
        Dim j As Integer = 67
        Dim l As Integer = DT.Rows.Count
        If ChkSelectAll.Checked = True Then
            For Each dr As DataRow In DT.Rows
                dr.Item("$IsCheck") = True
                i = i + j
                Me.DG.Row = Me.DG.RowContaining(i)
                Me.DG.SelectedRows.Add(Me.DG.Row)
                j = 20
            Next
        Else
            For Each dr As DataRow In DT.Rows
                dr.Item("$IsCheck") = False
            Next
            Me.DG.SelectedRows.Clear()
        End If
    End Sub

    'Private Sub DG_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles DG.MouseEnter

    'End Sub

    'Private Sub DG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DG.KeyPress

    'End Sub

    Private Sub DG_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DG.MouseClick
        '    'If e.Location = 0 Then
        '    'If e.X = 74 Then]

        If DG.RowContaining(e.X) = 0 Then
            If e.Button = MouseButtons.Left Then
                Me.DG.Row = Me.DG.RowContaining(e.Y)
                Dim c As Boolean = False
                Dim index As Integer = 0
                If DG.SelectedRows.Count > 0 Then
                    For i As Integer = 0 To DG.SelectedRows.Count - 1
                        If DG.SelectedRows(i) = Me.DG.Row Then
                            c = True
                            index = i
                        End If
                    Next
                    If c = False Then
                        Me.DG.SelectedRows.Add(Me.DG.Row)
                    Else
                        Me.DG.SelectedRows.RemoveAt(index)
                    End If
                    c = False
                    index = 0
                Else
                    Me.DG.SelectedRows.Add(Me.DG.Row)
                End If
            End If
        End If

    End Sub

    Private Sub DG_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs)

    End Sub

    Public Event EnterData As EventHandler
    ''' <summary>
    ''' Raises the <see cref="SelectedFontChanged"/> event.
    ''' </summary>
    Protected Overridable Sub OnSelectedDataChanged(ByVal e As EventArgs)
        RaiseEvent EnterData(Me, e)
    End Sub

    Private Sub HandleSelectedDataChanged(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnSelectedDataChanged(e)
    End Sub

    'Private Sub DG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DG.MouseDown
    ' Dim a As Integer
    'a = DG.RowContaining(e.X)
    'Me.DG.Row = Me.DG.RowContaining(e.Y)

    'Me.DG.SelectedRows.Add(Me.DG.Row)

    'End If

    'Dim iRow As Integer
    'iRow = DG.RowContaining(e.Y)
    'If e.Button = MouseButtons.Left Then


    '    FiLastRow = iRow
    '    Me.DG.SelectedRows.Add(iRow)
    'End If
    ' End Sub

    'Private Sub DG_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles DG.SelChange
    '    e.Cancel = True
    'End Sub
    Private Sub DG_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DG.MouseDoubleClick

        Me.DG.Row = Me.DG.RowContaining(e.Y)
        Dim c As Boolean = False
        Dim index As Integer = 0
        If DG.SelectedRows.Count > 0 Then
            For i As Integer = 0 To DG.SelectedRows.Count - 1
                If DG.SelectedRows(i) = Me.DG.Row Then
                    c = True
                    index = i
                End If
            Next
            If c = False Then
                Me.DG.SelectedRows.Add(Me.DG.Row)
            Else
                Me.DG.SelectedRows.RemoveAt(index)
            End If
            c = False
            index = 0
        Else
            Me.DG.SelectedRows.Add(Me.DG.Row)
        End If
        'Call cmdSelect_Click(sender, e)

        Dim drv As DataRowView
        Dim dr As DataRow
        With DG
            If .SelectedRows.Count > 0 Then
                drv = DT.DefaultView.Item(DG.Row)
                dr = drv.Row
                ReturnDataRow = dr
                If MultiSelection = True Then
                    DT.DefaultView.RowStateFilter = DataViewRowState.CurrentRows

                    For i As Integer = 0 To .SelectedRows.Count - 1
                        If DT.DefaultView.RowStateFilter = DataViewRowState.CurrentRows Then

                        End If
                        DT.DefaultView.Item(.SelectedRows(i)).Row.SetModified()
                    Next
                    DT.DefaultView.RowStateFilter = DataViewRowState.ModifiedOriginal
                    ReturnDT = DT.DefaultView.ToTable
                    SelectedData = ReturnDT

                    Dim formColl As FormCollection = Application.OpenForms
                    Dim count As Int32 = formColl.Count()

                    For Each f As Form In formColl
                        If f.Name = "MDI" Then

                            f.BringToFront()
                            Exit For
                        End If

                    Next
                End If
            Else
                ReturnDataRow = Nothing
                ReturnDT = Nothing
            End If

        End With
        Me.ParentForm.Close()
    End Sub

End Class
