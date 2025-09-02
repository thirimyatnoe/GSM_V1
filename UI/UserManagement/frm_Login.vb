Imports System.IO
Imports BusinessRule
Imports BusinessRule.UserManagement
Public Class frmLogin
    Inherits System.Windows.Forms.Form

#Region "Variable"

    Private _LoginController As New LogIn
    'Public Const AppName = "Stock Control System"
    Public LoginSuccess As Boolean


    Public CurrentUser As String
    Public CurrentuserLevel As String
    Public CurrentuserLevelID As Integer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblversion As System.Windows.Forms.Label
    Private dtUserInfo As New DataTable

#End Region

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
    Friend WithEvents CmdExit As System.Windows.Forms.Button
    Friend WithEvents Cmdsave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtPassword As System.Windows.Forms.TextBox
    Friend WithEvents CboUserList As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CboUserList = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CmdExit = New System.Windows.Forms.Button()
        Me.Cmdsave = New System.Windows.Forms.Button()
        Me.lblversion = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.CboUserList)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TxtPassword)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(337, 97)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'CboUserList
        '
        Me.CboUserList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboUserList.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CboUserList.Location = New System.Drawing.Point(102, 22)
        Me.CboUserList.Name = "CboUserList"
        Me.CboUserList.Size = New System.Drawing.Size(217, 25)
        Me.CboUserList.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(13, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 22)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Password :"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(4, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 22)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "User Name :"
        '
        'TxtPassword
        '
        Me.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPassword.Location = New System.Drawing.Point(101, 58)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.Size = New System.Drawing.Size(221, 24)
        Me.TxtPassword.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(101, 21)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(221, 28)
        Me.Panel1.TabIndex = 6
        '
        'CmdExit
        '
        Me.CmdExit.BackColor = System.Drawing.Color.SteelBlue
        Me.CmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdExit.ForeColor = System.Drawing.Color.White
        Me.CmdExit.Location = New System.Drawing.Point(263, 118)
        Me.CmdExit.Name = "CmdExit"
        Me.CmdExit.Size = New System.Drawing.Size(95, 30)
        Me.CmdExit.TabIndex = 4
        Me.CmdExit.Text = "Cancel"
        Me.CmdExit.UseVisualStyleBackColor = False
        '
        'Cmdsave
        '
        Me.Cmdsave.BackColor = System.Drawing.Color.SteelBlue
        Me.Cmdsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cmdsave.ForeColor = System.Drawing.Color.White
        Me.Cmdsave.Location = New System.Drawing.Point(162, 118)
        Me.Cmdsave.Name = "Cmdsave"
        Me.Cmdsave.Size = New System.Drawing.Size(95, 30)
        Me.Cmdsave.TabIndex = 3
        Me.Cmdsave.Text = "OK"
        Me.Cmdsave.UseVisualStyleBackColor = False
        '
        'lblversion
        '
        Me.lblversion.AutoSize = True
        Me.lblversion.BackColor = System.Drawing.Color.Transparent
        Me.lblversion.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblversion.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblversion.Location = New System.Drawing.Point(17, 131)
        Me.lblversion.Name = "lblversion"
        Me.lblversion.Size = New System.Drawing.Size(105, 17)
        Me.lblversion.TabIndex = 6
        Me.lblversion.Text = "Version- 6.4900"
        '
        'frmLogin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 17)
        Me.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.ClientSize = New System.Drawing.Size(391, 173)
        Me.Controls.Add(Me.lblversion)
        Me.Controls.Add(Me.CmdExit)
        Me.Controls.Add(Me.Cmdsave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Login ...."
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub Cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmdsave.Click
        Try
            CurrentUser = CboUserList.SelectedValue
            Global_CurrentUser = CurrentUser
            CurrentuserLevelID = _LoginController.ValidateUserLogin(CboUserList.SelectedValue, TxtPassword.Text)


            If CurrentuserLevelID = 0 Then
                LoginSuccess = False
                MsgBox("Wrong password!", MsgBoxStyle.Exclamation, "Login")
                TxtPassword.SelectionStart = 0
                TxtPassword.SelectionLength = TxtPassword.TextLength
                TxtPassword.Focus()
            Else
                CurrentuserLevel = _LoginController.GetUserLevel(CurrentuserLevelID).Rows(0)("UserLevel")
                LoginSuccess = True
                Me.Close()
            End If
        Catch ex As Exception
            LoginSuccess = False
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub SetUserInfo()
        Try
            dtUserInfo = _LoginController.GetUserInfo()

            With CboUserList
                .DataSource = dtUserInfo.DefaultView
                .ValueMember = "UserID"
                .DisplayMember = "UserName"
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub frm_GE_Login_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If Check_Expire() = True Then CmdExit_Click(sender, e)
        SetUserInfo()
        Me.AcceptButton = Cmdsave
        Me.CancelButton = CmdExit
    End Sub

    Private Function Check_Expire() As Boolean
        If _LoginController.CheckExpire(Global_ExpireDate) = True Then
            Check_Expire = True
            MsgBox("Software is Expired!", MsgBoxStyle.Information, AppName)
        Else
            Exit Function
        End If
    End Function

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_GE_Login_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        TxtPassword.Focus()
    End Sub

    Private Sub TxtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPassword.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


End Class
