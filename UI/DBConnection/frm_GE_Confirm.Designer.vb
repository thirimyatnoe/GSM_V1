<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GE_Confirm
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
        Me.cmdOK = New System.Windows.Forms.Button
        Me.gbSQL = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.rbtExisting = New System.Windows.Forms.RadioButton
        Me.rbtOverwrite = New System.Windows.Forms.RadioButton
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.gbSQL.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOK.Location = New System.Drawing.Point(167, 112)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 28)
        Me.cmdOK.TabIndex = 20
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'gbSQL
        '
        Me.gbSQL.Controls.Add(Me.Label1)
        Me.gbSQL.Controls.Add(Me.rbtExisting)
        Me.gbSQL.Controls.Add(Me.rbtOverwrite)
        Me.gbSQL.Location = New System.Drawing.Point(6, 0)
        Me.gbSQL.Name = "gbSQL"
        Me.gbSQL.Size = New System.Drawing.Size(318, 105)
        Me.gbSQL.TabIndex = 21
        Me.gbSQL.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(122, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Database already exists."
        '
        'rbtExisting
        '
        Me.rbtExisting.AutoSize = True
        Me.rbtExisting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtExisting.Location = New System.Drawing.Point(17, 74)
        Me.rbtExisting.Name = "rbtExisting"
        Me.rbtExisting.Size = New System.Drawing.Size(128, 17)
        Me.rbtExisting.TabIndex = 12
        Me.rbtExisting.Text = "Use existing database"
        Me.rbtExisting.UseVisualStyleBackColor = True
        '
        'rbtOverwrite
        '
        Me.rbtOverwrite.AutoSize = True
        Me.rbtOverwrite.Checked = True
        Me.rbtOverwrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtOverwrite.Location = New System.Drawing.Point(17, 49)
        Me.rbtOverwrite.Name = "rbtOverwrite"
        Me.rbtOverwrite.Size = New System.Drawing.Size(234, 17)
        Me.rbtOverwrite.TabIndex = 11
        Me.rbtOverwrite.TabStop = True
        Me.rbtOverwrite.Text = "Overwrite existing and create new database "
        Me.rbtOverwrite.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(247, 112)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 28)
        Me.cmdCancel.TabIndex = 22
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frm_GE_Confirm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = Global.UI.My.MySettings.Default.FormBackGround
        Me.ClientSize = New System.Drawing.Size(331, 146)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.gbSQL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_GE_Confirm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Confirm Action"
        Me.gbSQL.ResumeLayout(False)
        Me.gbSQL.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents gbSQL As System.Windows.Forms.GroupBox
    Friend WithEvents rbtExisting As System.Windows.Forms.RadioButton
    Friend WithEvents rbtOverwrite As System.Windows.Forms.RadioButton
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
