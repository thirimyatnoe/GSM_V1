<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GE_DBConnection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GE_DBConnection))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabConnect = New System.Windows.Forms.TabPage()
        Me.AccessPannel = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAccessPassword = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.butOpenFile = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAccessDBName = New System.Windows.Forms.TextBox()
        Me.SqlPannel = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboDatabase = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtSQL = New System.Windows.Forms.RadioButton()
        Me.gbSQL = New System.Windows.Forms.GroupBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rbtWindow = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tabBackup = New System.Windows.Forms.TabPage()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.pbBackupRestore = New System.Windows.Forms.ProgressBar()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.pbBackup = New System.Windows.Forms.ProgressBar()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.OptAccess = New System.Windows.Forms.RadioButton()
        Me.OptDB = New System.Windows.Forms.RadioButton()
        Me.mdbOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.tabConnect.SuspendLayout()
        Me.AccessPannel.SuspendLayout()
        Me.SqlPannel.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbSQL.SuspendLayout()
        Me.tabBackup.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabConnect)
        Me.TabControl1.Controls.Add(Me.tabBackup)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TabControl1.Location = New System.Drawing.Point(14, 21)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(364, 330)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.Tag = ""
        '
        'tabConnect
        '
        Me.tabConnect.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.tabConnect.Controls.Add(Me.AccessPannel)
        Me.tabConnect.Controls.Add(Me.SqlPannel)
        Me.tabConnect.Location = New System.Drawing.Point(4, 22)
        Me.tabConnect.Name = "tabConnect"
        Me.tabConnect.Padding = New System.Windows.Forms.Padding(3)
        Me.tabConnect.Size = New System.Drawing.Size(356, 304)
        Me.tabConnect.TabIndex = 0
        Me.tabConnect.Tag = "0"
        Me.tabConnect.Text = "Connection Properites                   "
        Me.tabConnect.UseVisualStyleBackColor = True
        '
        'AccessPannel
        '
        Me.AccessPannel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.AccessPannel.Controls.Add(Me.Label7)
        Me.AccessPannel.Controls.Add(Me.txtAccessPassword)
        Me.AccessPannel.Controls.Add(Me.Label6)
        Me.AccessPannel.Controls.Add(Me.butOpenFile)
        Me.AccessPannel.Controls.Add(Me.Label5)
        Me.AccessPannel.Controls.Add(Me.txtAccessDBName)
        Me.AccessPannel.Location = New System.Drawing.Point(332, 0)
        Me.AccessPannel.Name = "AccessPannel"
        Me.AccessPannel.Size = New System.Drawing.Size(24, 304)
        Me.AccessPannel.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Gray
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.GhostWhite
        Me.Label7.Location = New System.Drawing.Point(6, 286)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(327, 15)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "If you use by Access Database, you should use Standalone."
        '
        'txtAccessPassword
        '
        Me.txtAccessPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccessPassword.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.txtAccessPassword.Location = New System.Drawing.Point(98, 91)
        Me.txtAccessPassword.Name = "txtAccessPassword"
        Me.txtAccessPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(108)
        Me.txtAccessPassword.Size = New System.Drawing.Size(195, 20)
        Me.txtAccessPassword.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Password :"
        '
        'butOpenFile
        '
        Me.butOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.butOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.butOpenFile.Location = New System.Drawing.Point(278, 47)
        Me.butOpenFile.Name = "butOpenFile"
        Me.butOpenFile.Size = New System.Drawing.Size(27, 23)
        Me.butOpenFile.TabIndex = 16
        Me.butOpenFile.TabStop = False
        Me.butOpenFile.Text = "....."
        Me.butOpenFile.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "File Name :"
        '
        'txtAccessDBName
        '
        Me.txtAccessDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccessDBName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccessDBName.Location = New System.Drawing.Point(98, 49)
        Me.txtAccessDBName.Name = "txtAccessDBName"
        Me.txtAccessDBName.Size = New System.Drawing.Size(174, 21)
        Me.txtAccessDBName.TabIndex = 8
        '
        'SqlPannel
        '
        Me.SqlPannel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.SqlPannel.Controls.Add(Me.btnSave)
        Me.SqlPannel.Controls.Add(Me.txtServer)
        Me.SqlPannel.Controls.Add(Me.GroupBox1)
        Me.SqlPannel.Controls.Add(Me.rbtSQL)
        Me.SqlPannel.Controls.Add(Me.gbSQL)
        Me.SqlPannel.Controls.Add(Me.rbtWindow)
        Me.SqlPannel.Controls.Add(Me.Label1)
        Me.SqlPannel.Location = New System.Drawing.Point(-4, 1)
        Me.SqlPannel.Name = "SqlPannel"
        Me.SqlPannel.Size = New System.Drawing.Size(360, 303)
        Me.SqlPannel.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.Transparent
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(190, 261)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(139, 24)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Change Connection"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'txtServer
        '
        Me.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServer.Location = New System.Drawing.Point(105, 19)
        Me.txtServer.Multiline = True
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(196, 22)
        Me.txtServer.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboDatabase)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 193)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 62)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'cboDatabase
        '
        Me.cboDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cboDatabase.FormattingEnabled = True
        Me.cboDatabase.Location = New System.Drawing.Point(87, 21)
        Me.cboDatabase.Name = "cboDatabase"
        Me.cboDatabase.Size = New System.Drawing.Size(193, 21)
        Me.cboDatabase.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Databases :"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(86, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(195, 23)
        Me.Panel1.TabIndex = 15
        '
        'rbtSQL
        '
        Me.rbtSQL.AutoSize = True
        Me.rbtSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtSQL.Location = New System.Drawing.Point(32, 83)
        Me.rbtSQL.Name = "rbtSQL"
        Me.rbtSQL.Size = New System.Drawing.Size(155, 17)
        Me.rbtSQL.TabIndex = 3
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
        Me.txtUserName.Size = New System.Drawing.Size(195, 20)
        Me.txtUserName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
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
        Me.rbtWindow.Size = New System.Drawing.Size(139, 17)
        Me.rbtWindow.TabIndex = 2
        Me.rbtWindow.TabStop = True
        Me.rbtWindow.Text = "Window Authentications"
        Me.rbtWindow.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Server Name :"
        '
        'tabBackup
        '
        Me.tabBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.tabBackup.Controls.Add(Me.lblStatus)
        Me.tabBackup.Controls.Add(Me.lblMessage)
        Me.tabBackup.Controls.Add(Me.pbBackupRestore)
        Me.tabBackup.Controls.Add(Me.btnRestore)
        Me.tabBackup.Controls.Add(Me.txtFileName)
        Me.tabBackup.Controls.Add(Me.btnBackup)
        Me.tabBackup.Controls.Add(Me.pbBackup)
        Me.tabBackup.Location = New System.Drawing.Point(4, 22)
        Me.tabBackup.Name = "tabBackup"
        Me.tabBackup.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBackup.Size = New System.Drawing.Size(356, 304)
        Me.tabBackup.TabIndex = 1
        Me.tabBackup.Tag = "1"
        Me.tabBackup.Text = "Backup/Restore    Database            "
        Me.tabBackup.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.White
        Me.lblStatus.ForeColor = System.Drawing.Color.Blue
        Me.lblStatus.Location = New System.Drawing.Point(13, 14)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(252, 23)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "Status"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Location = New System.Drawing.Point(16, 134)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(325, 81)
        Me.lblMessage.TabIndex = 5
        Me.lblMessage.Text = resources.GetString("lblMessage.Text")
        '
        'pbBackupRestore
        '
        Me.pbBackupRestore.Location = New System.Drawing.Point(10, 43)
        Me.pbBackupRestore.Name = "pbBackupRestore"
        Me.pbBackupRestore.Size = New System.Drawing.Size(336, 23)
        Me.pbBackupRestore.TabIndex = 0
        '
        'btnRestore
        '
        Me.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestore.Image = CType(resources.GetObject("btnRestore.Image"), System.Drawing.Image)
        Me.btnRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRestore.Location = New System.Drawing.Point(198, 92)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(143, 24)
        Me.btnRestore.TabIndex = 4
        Me.btnRestore.Text = " Restore Database"
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileName.Enabled = False
        Me.txtFileName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName.Location = New System.Drawing.Point(271, 15)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(75, 21)
        Me.txtFileName.TabIndex = 2
        Me.txtFileName.Visible = False
        '
        'btnBackup
        '
        Me.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackup.Image = CType(resources.GetObject("btnBackup.Image"), System.Drawing.Image)
        Me.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBackup.Location = New System.Drawing.Point(17, 92)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(143, 24)
        Me.btnBackup.TabIndex = 1
        Me.btnBackup.Text = "Backup Database"
        Me.btnBackup.UseVisualStyleBackColor = True
        '
        'pbBackup
        '
        Me.pbBackup.Location = New System.Drawing.Point(19, 134)
        Me.pbBackup.Maximum = 4
        Me.pbBackup.Name = "pbBackup"
        Me.pbBackup.Size = New System.Drawing.Size(324, 15)
        Me.pbBackup.TabIndex = 0
        Me.pbBackup.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ToolStrip1.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripbtnClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(384, 25)
        Me.ToolStrip1.TabIndex = 78
        Me.ToolStrip1.Text = "ToolStrip1"
        Me.ToolStrip1.Visible = False
        '
        'ToolStripbtnClose
        '
        Me.ToolStripbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripbtnClose.Image = Global.UI.My.Resources.Resources.background
        Me.ToolStripbtnClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripbtnClose.Name = "ToolStripbtnClose"
        Me.ToolStripbtnClose.Size = New System.Drawing.Size(504, 9)
        Me.ToolStripbtnClose.Text = "Close"
        Me.ToolStripbtnClose.ToolTipText = "Close Ctrl+C"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.OptAccess)
        Me.GroupBox2.Controls.Add(Me.OptDB)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(369, 12)
        Me.GroupBox2.TabIndex = 79
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Visible = False
        '
        'OptAccess
        '
        Me.OptAccess.AutoSize = True
        Me.OptAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OptAccess.Location = New System.Drawing.Point(186, 11)
        Me.OptAccess.Name = "OptAccess"
        Me.OptAccess.Size = New System.Drawing.Size(57, 17)
        Me.OptAccess.TabIndex = 1
        Me.OptAccess.Text = "Access"
        Me.OptAccess.UseVisualStyleBackColor = True
        '
        'OptDB
        '
        Me.OptDB.AutoSize = True
        Me.OptDB.Checked = True
        Me.OptDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OptDB.Location = New System.Drawing.Point(125, 11)
        Me.OptDB.Name = "OptDB"
        Me.OptDB.Size = New System.Drawing.Size(38, 17)
        Me.OptDB.TabIndex = 0
        Me.OptDB.TabStop = True
        Me.OptDB.Text = "Sql"
        Me.OptDB.UseVisualStyleBackColor = True
        '
        'mdbOpenFileDialog
        '
        Me.mdbOpenFileDialog.FileName = "Select Access Database"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.CheckPathExists = False
        Me.SaveFileDialog1.OverwritePrompt = False
        Me.SaveFileDialog1.ValidateNames = False
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(377, 3)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 36)
        Me.btnHelpbook.TabIndex = 1462
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_GE_DBConnection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(409, 360)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_GE_DBConnection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Database Maintenance"
        Me.TabControl1.ResumeLayout(False)
        Me.tabConnect.ResumeLayout(False)
        Me.AccessPannel.ResumeLayout(False)
        Me.AccessPannel.PerformLayout()
        Me.SqlPannel.ResumeLayout(False)
        Me.SqlPannel.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbSQL.ResumeLayout(False)
        Me.gbSQL.PerformLayout()
        Me.tabBackup.ResumeLayout(False)
        Me.tabBackup.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabConnect As System.Windows.Forms.TabPage
    Friend WithEvents tabBackup As System.Windows.Forms.TabPage
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents pbBackup As System.Windows.Forms.ProgressBar
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents OptDB As System.Windows.Forms.RadioButton
    Friend WithEvents OptAccess As System.Windows.Forms.RadioButton
    Friend WithEvents mdbOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SqlPannel As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbtSQL As System.Windows.Forms.RadioButton
    Friend WithEvents gbSQL As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rbtWindow As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AccessPannel As System.Windows.Forms.Panel
    Friend WithEvents butOpenFile As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccessDBName As System.Windows.Forms.TextBox
    Friend WithEvents txtAccessPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pbBackupRestore As System.Windows.Forms.ProgressBar
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    'Friend WithEvents skinEngine1 As New Sunisoft.IrisSkin.SkinEngine
End Class
