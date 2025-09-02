<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_VoucherSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_VoucherSetting))
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.btnfont = New System.Windows.Forms.Button()
        Me.txtFirstTitle = New System.Windows.Forms.TextBox()
        Me.txtSecondTitle = New System.Windows.Forms.TextBox()
        Me.btnSecondTitle = New System.Windows.Forms.Button()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.txtResult2 = New System.Windows.Forms.TextBox()
        Me.btnAddress = New System.Windows.Forms.Button()
        Me.txtResult3 = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblPhoto = New System.Windows.Forms.Label()
        Me.lblItemImage = New System.Windows.Forms.PictureBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox4.SuspendLayout()
        CType(Me.lblItemImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FontDialog1
        '
        Me.FontDialog1.Color = System.Drawing.SystemColors.ControlText
        '
        'btnfont
        '
        Me.btnfont.BackColor = System.Drawing.Color.White
        Me.btnfont.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnfont.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnfont.Image = CType(resources.GetObject("btnfont.Image"), System.Drawing.Image)
        Me.btnfont.Location = New System.Drawing.Point(831, 36)
        Me.btnfont.Name = "btnfont"
        Me.btnfont.Size = New System.Drawing.Size(40, 36)
        Me.btnfont.TabIndex = 5
        Me.btnfont.UseVisualStyleBackColor = False
        '
        'txtFirstTitle
        '
        Me.txtFirstTitle.BackColor = System.Drawing.Color.White
        Me.txtFirstTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFirstTitle.Font = New System.Drawing.Font("Zawgyi-One", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstTitle.Location = New System.Drawing.Point(147, 25)
        Me.txtFirstTitle.Multiline = True
        Me.txtFirstTitle.Name = "txtFirstTitle"
        Me.txtFirstTitle.Size = New System.Drawing.Size(678, 65)
        Me.txtFirstTitle.TabIndex = 4
        Me.txtFirstTitle.TabStop = False
        Me.txtFirstTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSecondTitle
        '
        Me.txtSecondTitle.BackColor = System.Drawing.Color.White
        Me.txtSecondTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecondTitle.Font = New System.Drawing.Font("Zawgyi-One", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecondTitle.Location = New System.Drawing.Point(147, 96)
        Me.txtSecondTitle.Multiline = True
        Me.txtSecondTitle.Name = "txtSecondTitle"
        Me.txtSecondTitle.Size = New System.Drawing.Size(678, 57)
        Me.txtSecondTitle.TabIndex = 6
        Me.txtSecondTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSecondTitle
        '
        Me.btnSecondTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSecondTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSecondTitle.Image = CType(resources.GetObject("btnSecondTitle.Image"), System.Drawing.Image)
        Me.btnSecondTitle.Location = New System.Drawing.Point(831, 105)
        Me.btnSecondTitle.Name = "btnSecondTitle"
        Me.btnSecondTitle.Size = New System.Drawing.Size(40, 36)
        Me.btnSecondTitle.TabIndex = 7
        Me.btnSecondTitle.UseVisualStyleBackColor = True
        '
        'txtResult
        '
        Me.txtResult.BackColor = System.Drawing.SystemColors.Window
        Me.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtResult.ForeColor = System.Drawing.Color.DarkRed
        Me.txtResult.Location = New System.Drawing.Point(147, 246)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ReadOnly = True
        Me.txtResult.Size = New System.Drawing.Size(724, 68)
        Me.txtResult.TabIndex = 205
        Me.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtResult2
        '
        Me.txtResult2.BackColor = System.Drawing.SystemColors.Window
        Me.txtResult2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtResult2.ForeColor = System.Drawing.Color.DarkRed
        Me.txtResult2.Location = New System.Drawing.Point(147, 314)
        Me.txtResult2.Multiline = True
        Me.txtResult2.Name = "txtResult2"
        Me.txtResult2.ReadOnly = True
        Me.txtResult2.Size = New System.Drawing.Size(724, 68)
        Me.txtResult2.TabIndex = 206
        Me.txtResult2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnAddress
        '
        Me.btnAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddress.Image = CType(resources.GetObject("btnAddress.Image"), System.Drawing.Image)
        Me.btnAddress.Location = New System.Drawing.Point(831, 177)
        Me.btnAddress.Name = "btnAddress"
        Me.btnAddress.Size = New System.Drawing.Size(40, 36)
        Me.btnAddress.TabIndex = 9
        Me.btnAddress.UseVisualStyleBackColor = True
        '
        'txtResult3
        '
        Me.txtResult3.BackColor = System.Drawing.SystemColors.Window
        Me.txtResult3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtResult3.ForeColor = System.Drawing.Color.DarkRed
        Me.txtResult3.Location = New System.Drawing.Point(147, 384)
        Me.txtResult3.Multiline = True
        Me.txtResult3.Name = "txtResult3"
        Me.txtResult3.ReadOnly = True
        Me.txtResult3.Size = New System.Drawing.Size(724, 68)
        Me.txtResult3.TabIndex = 209
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnAdd)
        Me.GroupBox4.Controls.Add(Me.lblPhoto)
        Me.GroupBox4.Controls.Add(Me.lblItemImage)
        Me.GroupBox4.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.DeepPink
        Me.GroupBox4.Location = New System.Drawing.Point(896, 20)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(196, 205)
        Me.GroupBox4.TabIndex = 10
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Voucher Header Background"
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(68, 168)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(74, 25)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "&Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblPhoto
        '
        Me.lblPhoto.AutoSize = True
        Me.lblPhoto.ForeColor = System.Drawing.Color.Navy
        Me.lblPhoto.Location = New System.Drawing.Point(82, 80)
        Me.lblPhoto.Name = "lblPhoto"
        Me.lblPhoto.Size = New System.Drawing.Size(40, 17)
        Me.lblPhoto.TabIndex = 807
        Me.lblPhoto.Text = "Photo"
        '
        'lblItemImage
        '
        Me.lblItemImage.BackColor = System.Drawing.Color.Snow
        Me.lblItemImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblItemImage.Location = New System.Drawing.Point(25, 19)
        Me.lblItemImage.Name = "lblItemImage"
        Me.lblItemImage.Size = New System.Drawing.Size(148, 143)
        Me.lblItemImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.lblItemImage.TabIndex = 806
        Me.lblItemImage.TabStop = False
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Font = New System.Drawing.Font("Zawgyi-One", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(147, 157)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(678, 83)
        Me.txtAddress.TabIndex = 8
        '
        'btnPrint
        '
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Myanmar3", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(1047, 443)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(90, 31)
        Me.btnPrint.TabIndex = 11
        Me.btnPrint.Text = "Preview"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1110, 2)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 36)
        Me.btnHelpbook.TabIndex = 12
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 46)
        Me.Label1.TabIndex = 210
        Me.Label1.Text = "Main Header " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 46)
        Me.Label2.TabIndex = 211
        Me.Label2.Text = "Second Header " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "         Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(36, 167)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 46)
        Me.Label3.TabIndex = 212
        Me.Label3.Text = "Address and " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Phone no:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(2, 336)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(137, 23)
        Me.Label4.TabIndex = 213
        Me.Label4.Text = "Result for Header"
        '
        'frm_VoucherSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1144, 524)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txtResult3)
        Me.Controls.Add(Me.btnAddress)
        Me.Controls.Add(Me.txtResult2)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.btnSecondTitle)
        Me.Controls.Add(Me.txtSecondTitle)
        Me.Controls.Add(Me.btnfont)
        Me.Controls.Add(Me.txtFirstTitle)
        Me.Name = "frm_VoucherSetting"
        Me.Text = "Voucher Setting"
        Me.Controls.SetChildIndex(Me.txtFirstTitle, 0)
        Me.Controls.SetChildIndex(Me.btnfont, 0)
        Me.Controls.SetChildIndex(Me.txtSecondTitle, 0)
        Me.Controls.SetChildIndex(Me.btnSecondTitle, 0)
        Me.Controls.SetChildIndex(Me.txtResult, 0)
        Me.Controls.SetChildIndex(Me.txtResult2, 0)
        Me.Controls.SetChildIndex(Me.btnAddress, 0)
        Me.Controls.SetChildIndex(Me.txtResult3, 0)
        Me.Controls.SetChildIndex(Me.GroupBox4, 0)
        Me.Controls.SetChildIndex(Me.txtAddress, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.lblItemImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents btnfont As System.Windows.Forms.Button
    Friend WithEvents txtFirstTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtSecondTitle As System.Windows.Forms.TextBox
    Friend WithEvents btnSecondTitle As System.Windows.Forms.Button
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents txtResult2 As System.Windows.Forms.TextBox
    Friend WithEvents btnAddress As System.Windows.Forms.Button
    Friend WithEvents txtResult3 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblPhoto As System.Windows.Forms.Label
    Friend WithEvents lblItemImage As System.Windows.Forms.PictureBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
