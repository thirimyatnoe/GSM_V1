<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ItemCategory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ItemCategory))
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.txtItemCategoryID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtItemCategory = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTax = New System.Windows.Forms.TextBox()
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
        'txtItemCategoryID
        '
        Me.txtItemCategoryID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtItemCategoryID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCategoryID.Location = New System.Drawing.Point(131, 17)
        Me.txtItemCategoryID.Name = "txtItemCategoryID"
        Me.txtItemCategoryID.ReadOnly = True
        Me.txtItemCategoryID.Size = New System.Drawing.Size(98, 20)
        Me.txtItemCategoryID.TabIndex = 4
        Me.txtItemCategoryID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(22, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(102, 17)
        Me.Label9.TabIndex = 206
        Me.Label9.Text = "Item Category ID"
        '
        'txtItemCategory
        '
        Me.txtItemCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCategory.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtItemCategory.Location = New System.Drawing.Point(131, 43)
        Me.txtItemCategory.Name = "txtItemCategory"
        Me.txtItemCategory.Size = New System.Drawing.Size(222, 27)
        Me.txtItemCategory.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label4.Location = New System.Drawing.Point(20, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 19)
        Me.Label4.TabIndex = 203
        Me.Label4.Text = "ပစ္စည်းအမျိုးအစား"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(84, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 207
        Me.Label1.Text = "Prefix"
        '
        'txtPrefix
        '
        Me.txtPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrefix.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.txtPrefix.Location = New System.Drawing.Point(131, 76)
        Me.txtPrefix.MaxLength = 10
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(98, 23)
        Me.txtPrefix.TabIndex = 6
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(366, 3)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 226
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(76, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 17)
        Me.Label2.TabIndex = 227
        Me.Label2.Text = "Tax(%)"
        '
        'txtTax
        '
        Me.txtTax.BackColor = System.Drawing.SystemColors.Window
        Me.txtTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTax.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.txtTax.Location = New System.Drawing.Point(131, 105)
        Me.txtTax.MaxLength = 10
        Me.txtTax.Name = "txtTax"
        Me.txtTax.Size = New System.Drawing.Size(98, 23)
        Me.txtTax.TabIndex = 7
        '
        'frm_ItemCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(405, 183)
        Me.Controls.Add(Me.txtTax)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.txtPrefix)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.txtItemCategoryID)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtItemCategory)
        Me.Controls.Add(Me.Label4)
        Me.KeyPreview = True
        Me.Name = "frm_ItemCategory"
        Me.Text = "ItemCategory"
        Me.TransparencyKey = System.Drawing.Color.Empty
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtItemCategory, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtItemCategoryID, 0)
        Me.Controls.SetChildIndex(Me.SearchButton, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtPrefix, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtTax, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents txtItemCategoryID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtItemCategory As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPrefix As System.Windows.Forms.TextBox
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTax As System.Windows.Forms.TextBox
End Class
