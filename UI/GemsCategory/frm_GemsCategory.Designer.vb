<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GemsCategory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GemsCategory))
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.txtGemsCategoryID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtGemsCategory = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.optdiamonds = New System.Windows.Forms.RadioButton()
        Me.optothers = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTax = New System.Windows.Forms.TextBox()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'SearchButton
        '
        Me.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchButton.Image = CType(resources.GetObject("SearchButton.Image"), System.Drawing.Image)
        Me.SearchButton.Location = New System.Drawing.Point(236, 17)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(30, 21)
        Me.SearchButton.TabIndex = 4
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'txtGemsCategoryID
        '
        Me.txtGemsCategoryID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtGemsCategoryID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGemsCategoryID.Location = New System.Drawing.Point(131, 17)
        Me.txtGemsCategoryID.Name = "txtGemsCategoryID"
        Me.txtGemsCategoryID.ReadOnly = True
        Me.txtGemsCategoryID.Size = New System.Drawing.Size(98, 20)
        Me.txtGemsCategoryID.TabIndex = 205
        Me.txtGemsCategoryID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(19, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 17)
        Me.Label9.TabIndex = 206
        Me.Label9.Text = "Gems Category ID"
        '
        'txtGemsCategory
        '
        Me.txtGemsCategory.BackColor = System.Drawing.Color.White
        Me.txtGemsCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGemsCategory.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGemsCategory.Location = New System.Drawing.Point(130, 43)
        Me.txtGemsCategory.Name = "txtGemsCategory"
        Me.txtGemsCategory.Size = New System.Drawing.Size(222, 28)
        Me.txtGemsCategory.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label4.Location = New System.Drawing.Point(15, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 19)
        Me.Label4.TabIndex = 203
        Me.Label4.Text = "ကျောက်အမျိုးအစား"
        '
        'optdiamonds
        '
        Me.optdiamonds.AutoSize = True
        Me.optdiamonds.Checked = True
        Me.optdiamonds.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optdiamonds.Location = New System.Drawing.Point(130, 166)
        Me.optdiamonds.Name = "optdiamonds"
        Me.optdiamonds.Size = New System.Drawing.Size(102, 22)
        Me.optdiamonds.TabIndex = 207
        Me.optdiamonds.TabStop = True
        Me.optdiamonds.Text = "စိန္အမ်ိဳးအစား"
        Me.optdiamonds.UseVisualStyleBackColor = True
        Me.optdiamonds.Visible = False
        '
        'optothers
        '
        Me.optothers.AutoSize = True
        Me.optothers.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optothers.Location = New System.Drawing.Point(235, 166)
        Me.optothers.Name = "optothers"
        Me.optothers.Size = New System.Drawing.Size(162, 22)
        Me.optothers.TabIndex = 208
        Me.optothers.Text = "အၿခားေက်ာက္အမ်ိဳးအစား"
        Me.optothers.UseVisualStyleBackColor = True
        Me.optothers.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 18)
        Me.Label1.TabIndex = 209
        Me.Label1.Text = "ေက်ာက္အုပ္စု"
        Me.Label1.Visible = False
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(355, 8)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 597
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(74, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 17)
        Me.Label2.TabIndex = 598
        Me.Label2.Text = "Tax(%)"
        '
        'txtTax
        '
        Me.txtTax.BackColor = System.Drawing.Color.White
        Me.txtTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTax.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTax.Location = New System.Drawing.Point(128, 106)
        Me.txtTax.Name = "txtTax"
        Me.txtTax.Size = New System.Drawing.Size(95, 28)
        Me.txtTax.TabIndex = 6
        '
        'txtPrefix
        '
        Me.txtPrefix.BackColor = System.Drawing.Color.White
        Me.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrefix.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrefix.Location = New System.Drawing.Point(129, 74)
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(95, 28)
        Me.txtPrefix.TabIndex = 599
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(82, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 17)
        Me.Label3.TabIndex = 600
        Me.Label3.Text = "Prefix"
        '
        'frm_GemsCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 188)
        Me.Controls.Add(Me.txtPrefix)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTax)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.optothers)
        Me.Controls.Add(Me.optdiamonds)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.txtGemsCategoryID)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtGemsCategory)
        Me.Controls.Add(Me.Label4)
        Me.KeyPreview = True
        Me.Name = "frm_GemsCategory"
        Me.Text = "Gems Category"
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtGemsCategory, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtGemsCategoryID, 0)
        Me.Controls.SetChildIndex(Me.SearchButton, 0)
        Me.Controls.SetChildIndex(Me.optdiamonds, 0)
        Me.Controls.SetChildIndex(Me.optothers, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtTax, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtPrefix, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents txtGemsCategoryID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtGemsCategory As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents optdiamonds As System.Windows.Forms.RadioButton
    Friend WithEvents optothers As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTax As System.Windows.Forms.TextBox
    Friend WithEvents txtPrefix As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
