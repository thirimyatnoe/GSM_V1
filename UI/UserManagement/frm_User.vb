Option Explicit On 
Imports System.Data

Imports System.Configuration
Imports BusinessRule
Imports BusinessRule.UserManagement
Imports CommonInfo


Public Class frm_User
    Inherits System.Windows.Forms.Form

#Region "Variables"
    Private _UserController As New User
    Private _UserLevelController As New UserLevel
    Public Const AppName = "Stock Control System"
    Dim Flag As Boolean = False
    Dim Status As String
    Dim dtUser As New DataTable
    Dim varSysID As Integer
    Dim varUserID As String
    Dim UserInfo As New UserInfo

#End Region


    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripBtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripbtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripBtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnBrowseUser As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel


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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CboUserLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtUserID As System.Windows.Forms.TextBox
    Friend WithEvents TxtUserName As System.Windows.Forms.TextBox
    Friend WithEvents TxtPassword As System.Windows.Forms.TextBox
    Friend WithEvents TxtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents TxtRemark As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_User))
        Me.TxtUserID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.TxtRemark = New System.Windows.Forms.TextBox()
        Me.CboUserLevel = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripBtnNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripBtnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripbtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnBrowseUser = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtUserID
        '
        Me.TxtUserID.BackColor = System.Drawing.SystemColors.Window
        Me.TxtUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtUserID.Location = New System.Drawing.Point(145, 11)
        Me.TxtUserID.Name = "TxtUserID"
        Me.TxtUserID.Size = New System.Drawing.Size(199, 21)
        Me.TxtUserID.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 16)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "UserID  : "
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 16)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "UserName  : "
        '
        'TxtUserName
        '
        Me.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtUserName.Location = New System.Drawing.Point(145, 38)
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.Size = New System.Drawing.Size(226, 21)
        Me.TxtUserName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 16)
        Me.Label3.TabIndex = 67
        Me.Label3.Text = "Password  : "
        '
        'TxtPassword
        '
        Me.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPassword.Location = New System.Drawing.Point(145, 65)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.Size = New System.Drawing.Size(226, 21)
        Me.TxtPassword.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(32, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 18)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "confirm Password  : "
        '
        'TxtConfirmPassword
        '
        Me.TxtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtConfirmPassword.Location = New System.Drawing.Point(145, 92)
        Me.TxtConfirmPassword.Name = "TxtConfirmPassword"
        Me.TxtConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtConfirmPassword.Size = New System.Drawing.Size(225, 21)
        Me.TxtConfirmPassword.TabIndex = 3
        '
        'TxtRemark
        '
        Me.TxtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemark.Location = New System.Drawing.Point(145, 148)
        Me.TxtRemark.Multiline = True
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.Size = New System.Drawing.Size(226, 35)
        Me.TxtRemark.TabIndex = 5
        '
        'CboUserLevel
        '
        Me.CboUserLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboUserLevel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CboUserLevel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboUserLevel.Location = New System.Drawing.Point(146, 119)
        Me.CboUserLevel.Name = "CboUserLevel"
        Me.CboUserLevel.Size = New System.Drawing.Size(152, 21)
        Me.CboUserLevel.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(32, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 16)
        Me.Label5.TabIndex = 74
        Me.Label5.Text = "User Level :"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(32, 151)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 16)
        Me.Label6.TabIndex = 75
        Me.Label6.Text = "Remark :"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripBtnNew, Me.ToolStripSeparator1, Me.ToolStripBtnSave, Me.ToolStripSeparator3, Me.ToolStripbtnDelete, Me.ToolStripSeparator2, Me.ToolStripbtnClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(409, 32)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripBtnNew
        '
        Me.ToolStripBtnNew.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripBtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripBtnNew.ForeColor = System.Drawing.Color.White
        Me.ToolStripBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripBtnNew.Name = "ToolStripBtnNew"
        Me.ToolStripBtnNew.Size = New System.Drawing.Size(58, 29)
        Me.ToolStripBtnNew.Text = "New User"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripBtnSave
        '
        Me.ToolStripBtnSave.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripBtnSave.ForeColor = System.Drawing.Color.White
        Me.ToolStripBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripBtnSave.Name = "ToolStripBtnSave"
        Me.ToolStripBtnSave.Size = New System.Drawing.Size(36, 29)
        Me.ToolStripBtnSave.Text = "Save"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripbtnDelete
        '
        Me.ToolStripbtnDelete.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripbtnDelete.ForeColor = System.Drawing.Color.White
        Me.ToolStripbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripbtnDelete.Name = "ToolStripbtnDelete"
        Me.ToolStripbtnDelete.Size = New System.Drawing.Size(43, 29)
        Me.ToolStripbtnDelete.Text = "Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripbtnClose
        '
        Me.ToolStripbtnClose.BackColor = System.Drawing.Color.SteelBlue
        Me.ToolStripbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripbtnClose.ForeColor = System.Drawing.Color.White
        Me.ToolStripbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripbtnClose.Name = "ToolStripbtnClose"
        Me.ToolStripbtnClose.Size = New System.Drawing.Size(38, 29)
        Me.ToolStripbtnClose.Text = "Close"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnBrowseUser)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.CboUserLevel)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.TxtRemark)
        Me.Panel1.Controls.Add(Me.TxtConfirmPassword)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.TxtPassword)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.TxtUserName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.TxtUserID)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(12, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(396, 207)
        Me.Panel1.TabIndex = 78
        '
        'btnBrowseUser
        '
        Me.btnBrowseUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBrowseUser.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseUser.Location = New System.Drawing.Point(348, 11)
        Me.btnBrowseUser.Name = "btnBrowseUser"
        Me.btnBrowseUser.Size = New System.Drawing.Size(21, 21)
        Me.btnBrowseUser.TabIndex = 138
        Me.btnBrowseUser.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(145, 118)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(154, 23)
        Me.Panel2.TabIndex = 139
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(379, 0)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(30, 34)
        Me.btnHelpbook.TabIndex = 1460
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_User
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(409, 249)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_User"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User "
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Event handler"

    Private Sub frm_GE_User_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombos()
        Call CleanUp()
    End Sub

#Region "Click Event"



    Private Sub ToolStripbtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Panel1.Visible = False
        ToolStripBtnSave.Visible = False

    End Sub

    Private Sub ToolStripBtnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripBtnNew.Click
        EnabledControl(True)
        CleanUp()
        Panel1.Visible = True
        Status = "New"
        ToolStripBtnSave.Visible = True
    End Sub

    Private Sub ToolStripBtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripBtnSave.Click

        Try
            Get_Data()
            If ValidateData() = False Then Exit Sub
            If _UserController.SaveUser(UserInfo) Then
                MessageBox.Show("Successfully Save.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ToolStripBtnNew_Click(sender, e)
            Else
                MessageBox.Show("User ID already exists!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub btnBrowseUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseUser.Click
        Dim DataItem As DataRow
        Dim dt As New DataTable

        dtUser = _UserController.GetUserBrowser()
        DataItem = DirectCast(Search.FindFast(dtUser, "Search"), DataRow)

        If DataItem IsNot Nothing Then

            FillUser(DataItem)
        End If

    End Sub


    Private Sub ToolStripbtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripbtnClose.Click
        Me.Close()
    End Sub

    Private Sub ToolStripbtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripbtnDelete.Click

        If varSysID = 0 Then Exit Sub

        If TxtUserID.Text = "Administrator" Then
            MsgBox("Can't delete built in Administrator Account", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to delete?", AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then Exit Sub

        If _UserController.DeleteUser(varSysID) = True Then

            MessageBox.Show("Delete Successful.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            CleanUp()
        End If

    End Sub

    Private Sub CmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call CleanUp()
        EnabledControl(True)
        Status = "Save"
        CboUserLevel.SelectedIndex = -1
        TxtUserID.Focus()
        TxtUserID.SelectionStart = 0
        TxtUserID.SelectionLength = TxtUserID.TextLength
    End Sub

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

#End Region

#Region "textbox event"

    Private Sub TxtUserID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtUserID.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub TxtUserName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtUserName.GotFocus
        TxtUserName.SelectionStart = 0
        TxtUserName.SelectionLength = TxtUserName.TextLength
    End Sub

    Private Sub TxtUserName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtUserName.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub TxtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPassword.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub TxtConfirmPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtConfirmPassword.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub TxtRemark_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtRemark.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub CboUserLevel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CboUserLevel.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub TxtRemark_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRemark.GotFocus
        TxtRemark.SelectionStart = 0
        TxtRemark.SelectionLength = TxtRemark.TextLength
    End Sub

    Private Sub TxtConfirmPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtConfirmPassword.GotFocus
        TxtConfirmPassword.SelectionStart = 0
        TxtConfirmPassword.SelectionLength = TxtConfirmPassword.TextLength
    End Sub

    Private Sub TxtUserID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtUserID.GotFocus
        TxtUserID.SelectionStart = 0
        TxtUserID.SelectionLength = TxtUserID.TextLength
    End Sub

    Private Sub TxtPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPassword.GotFocus
        TxtPassword.SelectionStart = 0
        TxtPassword.SelectionLength = TxtPassword.TextLength
    End Sub

#End Region


#End Region

#Region "Private Methods"

    Sub CleanUp()
        TxtUserID.Text = ""
        TxtUserName.Text = ""
        TxtPassword.Text = ""
        TxtConfirmPassword.Text = ""
        TxtRemark.Text = ""
        With UserInfo
            .SysID = 0
            .UserID = 0
            .UserName = ""
            .UserPassword = ""
            .UserLevelID = 0
            .Remark = ""
        End With
        varSysID = 0
    End Sub


    'Sub FillData_Cbo()
    '    frm_GE_ComboReference.intComboID = 4
    '    frm_GE_ComboReference.ShowDialog()
    '    LoadCombos(ComboReferenceInfo.ComboTypeNumber.Country)
    '    If frm_GE_ComboReference.RefID <> 0 Then
    '        CboUserLevel.SelectedValue = frm_GE_ComboReference.RefID
    '    End If
    'End Sub

    Private Sub LoadCombos()
        Try
            With CboUserLevel
                .DisplayMember = "UserLevel"
                .ValueMember = "SysID"
                .DataSource = _UserController.GetUserLevel(UserInfo)
            End With
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "Error In FillCbo")
            Exit Sub
        End Try

    End Sub


    Private Sub Get_Data()
        With UserInfo
            .SysID = varSysID
            .UserID = CStr(TxtUserID.Text.Trim)
            .UserName = CStr(TxtUserName.Text.Trim)
            .UserPassword = CStr(TxtConfirmPassword.Text.Trim)
            .UserLevelID = CInt(CboUserLevel.SelectedValue)
            .Remark = CStr(TxtRemark.Text.Trim)
        End With
    End Sub
    Private Function ValidateData() As Boolean

        If TxtUserID.Text.Trim = "" Then
            MsgBox("Need UserID", MsgBoxStyle.Information)
            TxtUserID.Focus()
            Return False
        End If
        If TxtUserName.Text.Trim = "" Then
            MsgBox("Need UserName", MsgBoxStyle.Information)
            TxtUserName.Focus()
            Return False
        End If
        If TxtPassword.Text.Trim = "" Then
            MsgBox("Please Type Password", MsgBoxStyle.Information)
            TxtPassword.Focus()
            Return False
        End If
        If TxtConfirmPassword.Text.Trim = "" Then
            MsgBox("Please Type ConfirmPassword", MsgBoxStyle.Information)
            TxtConfirmPassword.Focus()
            Return False
        End If
        If CboUserLevel.SelectedIndex = -1 Then
            MsgBox(" Need User Level", MsgBoxStyle.Information)
            CboUserLevel.Focus()
            Return False
        End If

        If TxtConfirmPassword.Text <> TxtPassword.Text Then
            MsgBox(" Confirm Password is not match! ", MsgBoxStyle.Information)
            TxtConfirmPassword.Focus()
            TxtConfirmPassword.SelectionStart = 0
            TxtConfirmPassword.SelectionLength = TxtConfirmPassword.TextLength
            Return False
        End If

        Return True


    End Function


    Private Sub EnabledControl(ByVal flag As Boolean)
        TxtUserID.Enabled = flag
        TxtUserName.Enabled = flag
        TxtPassword.Enabled = flag
        TxtConfirmPassword.Enabled = flag
        CboUserLevel.Enabled = flag
        TxtRemark.Enabled = flag
    End Sub

    Private Sub FillUser(ByVal drow As DataRow)
        EnabledControl(True)
        varSysID = drow("@SysID")
        'varUserID = drow("UserID")
        varUserID = drow("User Code")
        'TxtUserID.Text = drow("UserID")
        TxtUserID.Text = drow("User Code")
        TxtUserName.Text = drow("User Name")
        TxtPassword.Text = drow("@Password")
        TxtConfirmPassword.Text = drow("@Password")
        CboUserLevel.Text = drow("UserLevel")
        TxtRemark.Text = drow("Remark")

        If TxtUserID.Text = "Administrator" Then   ''Built In Administrator account not allow to change id, name, level and remark
            TxtUserID.Enabled = False
            TxtUserName.Enabled = False
            CboUserLevel.Enabled = False
            TxtRemark.Enabled = False
        End If

    End Sub


#End Region



    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("User")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class
