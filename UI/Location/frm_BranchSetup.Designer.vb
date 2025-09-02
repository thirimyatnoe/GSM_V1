<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BranchSetup
    Inherits UI.frm_Base

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
        Me.txtBranchName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lstBranch = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'txtBranchName
        '
        Me.txtBranchName.BackColor = System.Drawing.Color.White
        Me.txtBranchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBranchName.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtBranchName.Location = New System.Drawing.Point(97, 12)
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.Size = New System.Drawing.Size(239, 26)
        Me.txtBranchName.TabIndex = 211
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label4.Location = New System.Drawing.Point(19, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 19)
        Me.Label4.TabIndex = 212
        Me.Label4.Text = "ဆိုင်ခွဲအမည်"
        '
        'lstBranch
        '
        Me.lstBranch.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.lstBranch.FormattingEnabled = True
        Me.lstBranch.ItemHeight = 17
        Me.lstBranch.Location = New System.Drawing.Point(6, 44)
        Me.lstBranch.Name = "lstBranch"
        Me.lstBranch.Size = New System.Drawing.Size(378, 293)
        Me.lstBranch.TabIndex = 213
        '
        'frm_BranchSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(393, 390)
        Me.Controls.Add(Me.lstBranch)
        Me.Controls.Add(Me.txtBranchName)
        Me.Controls.Add(Me.Label4)
        Me.KeyPreview = True
        Me.Name = "frm_BranchSetup"
        Me.Text = "Branch Setup"
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtBranchName, 0)
        Me.Controls.SetChildIndex(Me.lstBranch, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtBranchName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lstBranch As System.Windows.Forms.ListBox

End Class
