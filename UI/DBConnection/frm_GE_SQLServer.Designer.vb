<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GE_SQLServer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SqlPannel = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.rbtSQL = New System.Windows.Forms.RadioButton()
        Me.gbSQL = New System.Windows.Forms.GroupBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rbtWindow = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.SqlPannel.SuspendLayout()
        Me.gbSQL.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SqlPannel
        '
        Me.SqlPannel.BackColor = System.Drawing.Color.Transparent
        Me.SqlPannel.Controls.Add(Me.txtFilePath)
        Me.SqlPannel.Controls.Add(Me.Label4)
        Me.SqlPannel.Controls.Add(Me.txtServer)
        Me.SqlPannel.Controls.Add(Me.rbtSQL)
        Me.SqlPannel.Controls.Add(Me.gbSQL)
        Me.SqlPannel.Controls.Add(Me.rbtWindow)
        Me.SqlPannel.Controls.Add(Me.Label1)
        Me.SqlPannel.Location = New System.Drawing.Point(5, 36)
        Me.SqlPannel.Name = "SqlPannel"
        Me.SqlPannel.Size = New System.Drawing.Size(344, 265)
        Me.SqlPannel.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 195)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Database File Path:"
        '
        'txtServer
        '
        Me.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServer.Location = New System.Drawing.Point(101, 19)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(195, 21)
        Me.txtServer.TabIndex = 19
        '
        'rbtSQL
        '
        Me.rbtSQL.AutoSize = True
        Me.rbtSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtSQL.Location = New System.Drawing.Point(32, 83)
        Me.rbtSQL.Name = "rbtSQL"
        Me.rbtSQL.Size = New System.Drawing.Size(156, 17)
        Me.rbtSQL.TabIndex = 10
        Me.rbtSQL.Text = "SQL Server Authentications"
        Me.rbtSQL.UseVisualStyleBackColor = True
        '
        'gbSQL
        '
        Me.gbSQL.Controls.Add(Me.txtPassword)
        Me.gbSQL.Controls.Add(Me.txtUserName)
        Me.gbSQL.Controls.Add(Me.Label3)
        Me.gbSQL.Controls.Add(Me.Label2)
        Me.gbSQL.Location = New System.Drawing.Point(17, 84)
        Me.gbSQL.Name = "gbSQL"
        Me.gbSQL.Size = New System.Drawing.Size(313, 102)
        Me.gbSQL.TabIndex = 11
        Me.gbSQL.TabStop = False
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(87, 64)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(108)
        Me.txtPassword.Size = New System.Drawing.Size(195, 20)
        Me.txtPassword.TabIndex = 5
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.Location = New System.Drawing.Point(87, 28)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(195, 21)
        Me.txtUserName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Password :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "User Name :"
        '
        'rbtWindow
        '
        Me.rbtWindow.AutoSize = True
        Me.rbtWindow.Checked = True
        Me.rbtWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtWindow.Location = New System.Drawing.Point(32, 57)
        Me.rbtWindow.Name = "rbtWindow"
        Me.rbtWindow.Size = New System.Drawing.Size(140, 17)
        Me.rbtWindow.TabIndex = 9
        Me.rbtWindow.TabStop = True
        Me.rbtWindow.Text = "Window Authentications"
        Me.rbtWindow.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Server Name :"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.AliceBlue
        Me.ToolStrip1.BackgroundImage = Global.UI.My.Resources.Resources._8
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.ToolStripSeparator2, Me.btnClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(363, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.AliceBlue
        Me.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnSave.Image = Global.UI.My.Resources.Resources.background
        Me.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(38, 22)
        Me.btnSave.Text = "Save "
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnClose
        '
        Me.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnClose.Image = Global.UI.My.Resources.Resources.background
        Me.btnClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(40, 22)
        Me.btnClose.Text = "Close"
        '
        'txtFilePath
        '
        Me.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilePath.Location = New System.Drawing.Point(129, 192)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(195, 21)
        Me.txtFilePath.TabIndex = 6
        '
        'frm_GE_SQLServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = Global.UI.My.MySettings.Default.FormBackground
        Me.ClientSize = New System.Drawing.Size(363, 313)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.SqlPannel)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_GE_SQLServer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create SQL Database"
        Me.SqlPannel.ResumeLayout(False)
        Me.SqlPannel.PerformLayout()
        Me.gbSQL.ResumeLayout(False)
        Me.gbSQL.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SqlPannel As System.Windows.Forms.Panel
    Friend WithEvents rbtSQL As System.Windows.Forms.RadioButton
    Friend WithEvents gbSQL As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rbtWindow As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
End Class
