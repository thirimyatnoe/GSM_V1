<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PhotoPathConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_PhotoPathConfig))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPhotoPath = New System.Windows.Forms.TextBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 17)
        Me.Label1.TabIndex = 202
        Me.Label1.Text = "Photo Path"
        '
        'txtPhotoPath
        '
        Me.txtPhotoPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhotoPath.Location = New System.Drawing.Point(71, 19)
        Me.txtPhotoPath.Multiline = True
        Me.txtPhotoPath.Name = "txtPhotoPath"
        Me.txtPhotoPath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPhotoPath.Size = New System.Drawing.Size(291, 49)
        Me.txtPhotoPath.TabIndex = 201
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(374, 11)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 1447
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_PhotoPathConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(412, 129)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPhotoPath)
        Me.KeyPreview = True
        Me.Name = "frm_PhotoPathConfig"
        Me.Text = "Photo Path"
        Me.Controls.SetChildIndex(Me.txtPhotoPath, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPhotoPath As System.Windows.Forms.TextBox
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button

End Class
