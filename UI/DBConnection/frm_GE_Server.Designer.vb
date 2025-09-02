<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GE_Server
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
        Me.cmdAccess = New System.Windows.Forms.Button()
        Me.cmdSQLDB = New System.Windows.Forms.Button()
        Me.cmdExistDB = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdAccess
        '
        Me.cmdAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAccess.Location = New System.Drawing.Point(31, 12)
        Me.cmdAccess.Name = "cmdAccess"
        Me.cmdAccess.Size = New System.Drawing.Size(266, 31)
        Me.cmdAccess.TabIndex = 0
        Me.cmdAccess.Text = "Install New DataBase in Access"
        Me.cmdAccess.UseVisualStyleBackColor = True
        Me.cmdAccess.Visible = False
        '
        'cmdSQLDB
        '
        Me.cmdSQLDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSQLDB.Location = New System.Drawing.Point(31, 58)
        Me.cmdSQLDB.Name = "cmdSQLDB"
        Me.cmdSQLDB.Size = New System.Drawing.Size(266, 31)
        Me.cmdSQLDB.TabIndex = 1
        Me.cmdSQLDB.Text = "Install New DataBase in SQL"
        Me.cmdSQLDB.UseVisualStyleBackColor = True
        '
        'cmdExistDB
        '
        Me.cmdExistDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExistDB.Location = New System.Drawing.Point(31, 111)
        Me.cmdExistDB.Name = "cmdExistDB"
        Me.cmdExistDB.Size = New System.Drawing.Size(266, 31)
        Me.cmdExistDB.TabIndex = 2
        Me.cmdExistDB.Text = "Use Existing DataBase"
        Me.cmdExistDB.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Gray
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.GhostWhite
        Me.Label7.Location = New System.Drawing.Point(7, 181)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(329, 34)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "If MS Access is used as database, it is better to use this" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "software standalone r" & _
    "ather than client/sever."
        Me.Label7.Visible = False
        '
        'frm_GE_Server
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = Global.UI.My.MySettings.Default.FormBackground
        Me.ClientSize = New System.Drawing.Size(343, 229)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmdExistDB)
        Me.Controls.Add(Me.cmdSQLDB)
        Me.Controls.Add(Me.cmdAccess)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_GE_Server"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Server"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdAccess As System.Windows.Forms.Button
    Friend WithEvents cmdSQLDB As System.Windows.Forms.Button
    Friend WithEvents cmdExistDB As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
