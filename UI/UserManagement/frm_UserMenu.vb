Option Explicit On

Imports System.IO
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Collections
Imports BusinessRule
Imports BusinessRule.UserManagement
Imports CommonInfo

Public Class frm_UserMenu
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstvwUserLevel As System.Windows.Forms.ListView
    Friend WithEvents tvMenu As System.Windows.Forms.TreeView
    Friend WithEvents ImageList3 As System.Windows.Forms.ImageList
    Friend WithEvents TStripMnuItemNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TStripMnuItemEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TStripMnuItemDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripBtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripbtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents TStripLblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripbtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_UserMenu))
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Administrator", 0)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstvwUserLevel = New System.Windows.Forms.ListView()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tvMenu = New System.Windows.Forms.TreeView()
        Me.ImageList3 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TStripMnuItemNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.TStripMnuItemEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.TStripMnuItemDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TStripLblNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripBtnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripbtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripbtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackgroundImage = CType(resources.GetObject("GroupBox1.BackgroundImage"), System.Drawing.Image)
        Me.GroupBox1.Controls.Add(Me.lstvwUserLevel)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 35)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(284, 578)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "User Level :"
        '
        'lstvwUserLevel
        '
        Me.lstvwUserLevel.BackColor = System.Drawing.SystemColors.Window
        Me.lstvwUserLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstvwUserLevel.ContextMenuStrip = Me.ContextMenuStrip
        Me.lstvwUserLevel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstvwUserLevel.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.lstvwUserLevel.LargeImageList = Me.ImageList2
        Me.lstvwUserLevel.Location = New System.Drawing.Point(3, 17)
        Me.lstvwUserLevel.MultiSelect = False
        Me.lstvwUserLevel.Name = "lstvwUserLevel"
        Me.lstvwUserLevel.ShowItemToolTips = True
        Me.lstvwUserLevel.Size = New System.Drawing.Size(278, 558)
        Me.lstvwUserLevel.TabIndex = 0
        Me.lstvwUserLevel.UseCompatibleStateImageBehavior = False
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "")
        Me.ImageList2.Images.SetKeyName(1, "Administrator2.jpg")
        Me.ImageList2.Images.SetKeyName(2, "User.jpg")
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackgroundImage = CType(resources.GetObject("GroupBox2.BackgroundImage"), System.Drawing.Image)
        Me.GroupBox2.Controls.Add(Me.tvMenu)
        Me.GroupBox2.Location = New System.Drawing.Point(312, 35)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(319, 578)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Menu List :"
        '
        'tvMenu
        '
        Me.tvMenu.BackColor = System.Drawing.SystemColors.Window
        Me.tvMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvMenu.CheckBoxes = True
        Me.tvMenu.Dock = System.Windows.Forms.DockStyle.Left
        Me.tvMenu.ImageIndex = 1
        Me.tvMenu.ImageList = Me.ImageList3
        Me.tvMenu.Location = New System.Drawing.Point(3, 17)
        Me.tvMenu.Name = "tvMenu"
        Me.tvMenu.SelectedImageIndex = 2
        Me.tvMenu.Size = New System.Drawing.Size(307, 558)
        Me.tvMenu.TabIndex = 0
        '
        'ImageList3
        '
        Me.ImageList3.ImageStream = CType(resources.GetObject("ImageList3.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList3.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList3.Images.SetKeyName(0, "")
        Me.ImageList3.Images.SetKeyName(1, "")
        Me.ImageList3.Images.SetKeyName(2, "")
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "action_go.gif")
        Me.ImageList1.Images.SetKeyName(1, "icon_user.gif")
        '
        'TStripMnuItemNew
        '
        Me.TStripMnuItemNew.Name = "TStripMnuItemNew"
        Me.TStripMnuItemNew.Size = New System.Drawing.Size(107, 22)
        Me.TStripMnuItemNew.Text = "New"
        '
        'TStripMnuItemEdit
        '
        Me.TStripMnuItemEdit.Name = "TStripMnuItemEdit"
        Me.TStripMnuItemEdit.Size = New System.Drawing.Size(107, 22)
        Me.TStripMnuItemEdit.Text = "Edit"
        '
        'TStripMnuItemDelete
        '
        Me.TStripMnuItemDelete.Name = "TStripMnuItemDelete"
        Me.TStripMnuItemDelete.Size = New System.Drawing.Size(107, 22)
        Me.TStripMnuItemDelete.Text = "Delete"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.AllowDrop = True
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TStripMnuItemNew, Me.TStripMnuItemEdit, Me.TStripMnuItemDelete})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(108, 70)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TStripLblNew, Me.ToolStripSeparator1, Me.ToolStripBtnSave, Me.ToolStripSeparator2, Me.ToolStripbtnEdit, Me.ToolStripSeparator3, Me.ToolStripbtnDelete, Me.ToolStripSeparator5, Me.ToolStripbtnClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(979, 33)
        Me.ToolStrip1.TabIndex = 85
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TStripLblNew
        '
        Me.TStripLblNew.BackColor = System.Drawing.Color.SteelBlue
        Me.TStripLblNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TStripLblNew.ForeColor = System.Drawing.Color.White
        Me.TStripLblNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TStripLblNew.Name = "TStripLblNew"
        Me.TStripLblNew.Size = New System.Drawing.Size(86, 30)
        Me.TStripLblNew.Text = "New User Level"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 33)
        '
        'ToolStripBtnSave
        '
        Me.ToolStripBtnSave.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripBtnSave.ForeColor = System.Drawing.Color.White
        Me.ToolStripBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripBtnSave.Name = "ToolStripBtnSave"
        Me.ToolStripBtnSave.Size = New System.Drawing.Size(90, 30)
        Me.ToolStripBtnSave.Text = "Save User Menu"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 33)
        '
        'ToolStripbtnEdit
        '
        Me.ToolStripbtnEdit.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripbtnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripbtnEdit.ForeColor = System.Drawing.Color.White
        Me.ToolStripbtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripbtnEdit.Name = "ToolStripbtnEdit"
        Me.ToolStripbtnEdit.Size = New System.Drawing.Size(30, 30)
        Me.ToolStripbtnEdit.Text = "Edit"
        Me.ToolStripbtnEdit.ToolTipText = "Edit User Level"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 33)
        '
        'ToolStripbtnDelete
        '
        Me.ToolStripbtnDelete.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripbtnDelete.ForeColor = System.Drawing.Color.White
        Me.ToolStripbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripbtnDelete.Name = "ToolStripbtnDelete"
        Me.ToolStripbtnDelete.Size = New System.Drawing.Size(43, 30)
        Me.ToolStripbtnDelete.Text = "Delete"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 33)
        '
        'ToolStripbtnClose
        '
        Me.ToolStripbtnClose.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripbtnClose.ForeColor = System.Drawing.Color.White
        Me.ToolStripbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripbtnClose.Name = "ToolStripbtnClose"
        Me.ToolStripbtnClose.Size = New System.Drawing.Size(38, 30)
        Me.ToolStripbtnClose.Text = "Close"
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(329, -1)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 33)
        Me.btnHelpbook.TabIndex = 1460
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_UserMenu
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(979, 618)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frm_UserMenu"
        Me.Text = "User Level Control"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Variables"
    Private _UserController As New User
    Private _objMenuBuilderController As New MenuBuilder
    Private _objUserLevelController As New UserLevel

    Private UserLvIDT As DataTable
    Private MenuLevelDT As DataTable
    Private AfterCheckEvent_Flg As Boolean   ''false=Not to fire, true=to fire

#End Region

#Region "Method"

    'Public Sub RefreshEmployeeView()

    '    _objMenuBuilderController.UpdateTreeViewByUserLevel(tvLocation, lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text, lstvwUserLevel.SelectedItems(0).Text)
    '    If lstvwUserLevel.SelectedItems(0).Text = "Administrator" Then
    '        For Each node As TreeNode In tvLocation.Nodes
    '            node.Checked = True
    '            ChangeColor(node, Color.Gray)
    '        Next
    '    Else
    '        For Each node As TreeNode In tvLocation.Nodes
    '            ChangeColor(node, Color.Black)
    '        Next
    '    End If
    'End Sub

    Public Sub ChangeColor(ByVal parTreeNode As Windows.Forms.TreeNode, ByVal color As Color)
        Dim i As Integer
        'parTreeNode.BackColor = color
        parTreeNode.ForeColor = color
        For i = 0 To parTreeNode.Nodes.Count - 1
            ChangeColor(parTreeNode.Nodes(i), color)
        Next
    End Sub
    '--------------
#End Region

    Private Sub frm_GE_UserMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'tvLocation.SendToBack()
        'LoadEmployeeView(True)

        lstvwUserLevel.ContextMenuStrip = ContextMenuStrip2


        LoaduserLevelView()
        LoadUserMenuView()
        Me.WindowState = FormWindowState.Maximized
    End Sub


    Private Sub LoaduserLevelView()
        Dim i As Integer = 0
        UserLvIDT = _objUserLevelController.GetUserLevel(0)

        With lstvwUserLevel
            .Items.Clear()
            .Columns.Clear()

            .Columns.Add("UserLevel", "UserLevel")
            .Columns.Add("SysID", "SysID")
            .Columns.Add("Descirption", "Descirption")
            .Columns.Add("Remark", "Remark")

            For Each rw As DataRow In UserLvIDT.Rows
                Dim item1 As New ListViewItem(CStr(rw.Item("UserLevel")))

                item1.SubItems.Add(CStr(rw.Item("SysID"))).Name = "SysID"
                item1.SubItems.Add(CStr(rw.Item("Description"))).Name = "Description"
                item1.SubItems.Add(CStr(rw.Item("Remark"))).Name = "Remark"
                If rw.Item("UserLevel") = "Administrator" Then
                    item1.ImageIndex = 1
                Else
                    item1.ImageIndex = 2
                End If

                .Items.Add(item1)
            Next
            If .Items.Count > 0 Then
                .Items(0).Focused = True
                .SelectedItems.Clear()
                .Items(0).Selected = True
            End If

        End With

    End Sub

    Private Sub LoadUserMenuView()
        If lstvwUserLevel.SelectedItems.Count < 1 Then
            tvMenu.Nodes.Clear()
            Exit Sub
        End If

        MenuLevelDT = _objMenuBuilderController.GetMenuUserLevel(lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text, lstvwUserLevel.SelectedItems(0).Text)

        tvMenu.Nodes.Clear()
        Dim oNode As New System.Windows.Forms.TreeNode
        oNode = New System.Windows.Forms.TreeNode

        Try
            oNode.ImageIndex = 0
            oNode.SelectedImageIndex = 0
            oNode.Text = "Application Main Menu"
            oNode.Tag = "Application Main Menu"
            tvMenu.Nodes.Add(oNode)
            'oNode.Nodes.Add("")
        Catch ex As Exception
            MsgBox("Cannot Create Initial Node:" & ex.ToString)
            Exit Sub
        End Try

        If UserLvIDT Is Nothing Then
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Try
            AfterCheckEvent_Flg = False
            AddChildMenu("", oNode)
            AfterCheckEvent_Flg = True

            lstvwUserLevel.Focus()
            tvMenu.ExpandAll()

            If lstvwUserLevel.SelectedItems(0).Text = "Administrator" Then    'Not allow edit user menu for Default Administrator Level
                ToolStripbtnDelete.Enabled = False
                ToolStripBtnSave.Enabled = False
                ToolStripbtnEdit.Enabled = False
                TStripMnuItemDelete.Enabled = False
                TStripMnuItemEdit.Enabled = False
                'tvMenu.Enabled = False
                For Each node As TreeNode In tvMenu.Nodes
                    ChangeColor(node, Color.Gray)
                Next

            Else
                ToolStripbtnDelete.Enabled = True
                ToolStripBtnSave.Enabled = True
                ToolStripbtnEdit.Enabled = True
                TStripMnuItemDelete.Enabled = True
                TStripMnuItemEdit.Enabled = True
                '                tvMenu.Enabled = True
                For Each node As TreeNode In tvMenu.Nodes
                    ChangeColor(node, Color.Black)
                Next

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub AddChildMenu(ByVal argMenuID As String, ByVal argNode As TreeNode)
        Dim tmpnode As TreeNode
        Dim tmpdt As DataTable
        Dim dr As DataRow
        'One lower level and start with argMenuID
        MenuLevelDT.DefaultView.RowFilter = "Len(MenuID) = " & argMenuID.Length + 2 & " And MenuID Like '" & argMenuID & "*'"

        tmpdt = MenuLevelDT.DefaultView.ToTable
        If tmpdt.Rows.Count = 0 Then
            argNode.ImageIndex = 1
            argNode.SelectedImageIndex = 1
        Else
            argNode.ImageIndex = 0
            argNode.SelectedImageIndex = 0
        End If
        For Each dr In tmpdt.Rows
            If dr("MenuName").ToString.Trim <> "" And dr("MenuName").ToString.Trim <> "-" Then
                tmpnode = argNode.Nodes.Add(dr("MenuID").ToString, dr("MenuName").ToString.Trim)
                tmpnode.Checked = IIf(IsDBNull(dr("_" & lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text)), 0, dr("_" & lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text))
                'tmpnode.Checked = dr(lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text)
                AddChildMenu(dr("MenuID"), tmpnode)
            End If

        Next
    End Sub

    Private Sub tvMenu_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvMenu.AfterCheck

        If AfterCheckEvent_Flg Then
            AfterCheckEvent_Flg = False
            If Not e.Node.Parent Is Nothing Then
                e.Node.Parent.Checked = True
            End If
            CheckChangeWholeNode(e.Node, e.Node.Checked)
            AfterCheckEvent_Flg = True
        End If
    End Sub

    Public Sub CheckChangeWholeNode(ByVal parTreeNode As Windows.Forms.TreeNode, ByVal parCheckState As Boolean)
        Dim i As Integer
        parTreeNode.Checked = parCheckState
        For i = 0 To parTreeNode.Nodes.Count - 1
            CheckChangeWholeNode(parTreeNode.Nodes(i), parCheckState)
        Next
    End Sub

    Private Sub FrmUserMenu_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        tvMenu.ExpandAll()
    End Sub

    'Private Sub _Refresh()
    '    Call LoaduserLevelView()
    'End Sub


    Private Sub TStripMnuItemNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TStripMnuItemNew.Click
        NewUserLevel()
    End Sub

    Private Sub TStripMnuItemEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TStripMnuItemEdit.Click
        EditUserLevel()
    End Sub

    Private Sub TStripMnuItemDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TStripMnuItemDelete.Click
        DeleteUserLevel()
    End Sub


    Private Sub ToolStripBtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripBtnSave.Click
        Try

            SaveMenuUserLevel(tvMenu.Nodes(0))

            MsgBox("Successfully Save", MsgBoxStyle.Information, "Gold & Jewelry System")
        Catch ex As Exception
            MsgBox("Can't complete saving. " & ex.Message)
        End Try
    End Sub

    Private Sub ToolStripbtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripbtnEdit.Click
        EditUserLevel()
    End Sub

    Private Sub ToolStripbtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripbtnDelete.Click
        DeleteUserLevel()
    End Sub

    Private Sub ToolStripbtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripbtnClose.Click
        Me.Close()
    End Sub

    Private Sub TStripLblNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TStripLblNew.Click
        NewUserLevel()
    End Sub

    Private Sub SaveMenuUserLevel(ByVal argNode As TreeNode)

        Dim tmpNode As TreeNode

        If argNode.Name.Trim <> "" Then
            _objMenuBuilderController.UpdateMenuUserLevel(lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text, argNode.Name, argNode.Checked)
        End If

        For Each tmpNode In argNode.Nodes
            SaveMenuUserLevel(tmpNode)
        Next

    End Sub


    Private Sub NewUserLevel()
        Dim frmUserLevel As New frm_UserLevel
        frmUserLevel.NewUserLevel()
        Call LoaduserLevelView()
    End Sub

    Private Sub EditUserLevel()
        If lstvwUserLevel.SelectedItems.Count > 0 Then

            Dim frmUserLevel As New frm_UserLevel
            Dim objUserLevel As New UserLevelInfo

            objUserLevel.SysID = lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text
            objUserLevel.UserLevel = lstvwUserLevel.SelectedItems(0).Text
            objUserLevel.Description = lstvwUserLevel.SelectedItems(0).SubItems("Description").Text
            objUserLevel.Remark = lstvwUserLevel.SelectedItems(0).SubItems("Remark").Text

            frmUserLevel.EditUserLevel(objUserLevel)
            Call LoaduserLevelView()
        Else
            MsgBox("Please select user level to edit!")
        End If
    End Sub

    Private Sub DeleteUserLevel()
        Dim SysID As Integer

        If lstvwUserLevel.SelectedItems.Count < 1 Then
            MsgBox("Please select user level to delete!")
            Exit Sub
        End If


        SysID = CInt(lstvwUserLevel.SelectedItems(0).SubItems("SysID").Text)

        If lstvwUserLevel.SelectedItems(0).Text = "Administrator" Then
            MsgBox("Cann't delete Administrator User Level!", MsgBoxStyle.Information, "Gold & Jewelry System")
            Exit Sub
        End If

        If lstvwUserLevel.FocusedItem.Index >= 0 Then
            If MessageBox.Show("Are you sure to delete " + vbCrLf + lstvwUserLevel.SelectedItems(0).Text, "Gold & Jewelry System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            Try
                If _objMenuBuilderController.DeleteUserLevel(SysID, True) = True Then
                    'If _UserController.DeleteUser(SysID) = True Then
                    MessageBox.Show("Delete Successful.", "Gold & Jewelry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Call LoaduserLevelView()
                    'End If

                Else
                    If MessageBox.Show("User level in use!  Do you want to delete both user level and assignment users?", "Delete Userlevel and Users", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        _objMenuBuilderController.DeleteUserLevel(SysID, True)
                        MessageBox.Show("Delete Successful.", "Gold & Jewelry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Call LoaduserLevelView()
                    End If
                End If
            Catch ex As Exception
            MsgBox("Can't delete " & lstvwUserLevel.SelectedItems(0).Text & "!" & ex.Message)
        End Try
        Else
            MsgBox("Please select the User Level you want to delete!", MsgBoxStyle.Information)
        End If
    End Sub

    'Private Sub lstvwUserLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        LoadUserMenuView()
    '        ChangeSelectionColor()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub ChangeSelectionColor()
        Dim lstItem As ListViewItem
        For Each lstItem In lstvwUserLevel.Items
            If lstItem.Selected Then
                lstItem.BackColor = Color.Blue
                lstItem.ForeColor = Color.White
            Else
                lstItem.BackColor = Color.White
                lstItem.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub lstvwUserLevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstvwUserLevel.SelectedIndexChanged
        Try
            If lstvwUserLevel.SelectedItems.Count > 0 Then
                LoadUserMenuView()
                ChangeSelectionColor()
                ' ---
                'RefreshEmployeeView()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub tvLocation_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

        If e.Node.Nodes.Count > 0 Then
            For i As Integer = 0 To e.Node.Nodes.Count - 1
                e.Node.Nodes(i).Checked = e.Node.Checked
            Next
        End If
    End Sub

    Private Sub tvLocation_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs)
        'If e.Node.BackColor = Color.LightGray Then
        '    If lstvwUserLevel.SelectedItems(0).Text = "Administrator" Then
        '        e.Cancel = True
        '    Else
        '        e.Cancel = False
        '    End If

        'End If
        If e.Node.ForeColor = Color.Gray Then
            If lstvwUserLevel.SelectedItems(0).Text = "Administrator" Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If

        End If
    End Sub

    Private Sub tvMenu_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvMenu.BeforeCheck
        If e.Node.ForeColor = Color.Gray Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("UserMenu")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class
