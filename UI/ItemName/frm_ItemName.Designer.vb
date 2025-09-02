<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ItemName
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ItemName))
        Me.grdItemName = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboItemCategory = New System.Windows.Forms.ComboBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        CType(Me.grdItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdItemName
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItemName.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdItemName.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItemName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdItemName.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grdItemName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemName.Location = New System.Drawing.Point(11, 46)
        Me.grdItemName.Name = "grdItemName"
        Me.grdItemName.RowHeadersWidth = 25
        Me.grdItemName.Size = New System.Drawing.Size(378, 258)
        Me.grdItemName.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label5.Location = New System.Drawing.Point(9, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 19)
        Me.Label5.TabIndex = 527
        Me.Label5.Text = "ပစ္စည်းအမျိုးအစား"
        '
        'cboItemCategory
        '
        Me.cboItemCategory.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemCategory.FormattingEnabled = True
        Me.cboItemCategory.Location = New System.Drawing.Point(116, 11)
        Me.cboItemCategory.Name = "cboItemCategory"
        Me.cboItemCategory.Size = New System.Drawing.Size(194, 27)
        Me.cboItemCategory.TabIndex = 4
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(364, 6)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 596
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_ItemName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 358)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboItemCategory)
        Me.Controls.Add(Me.grdItemName)
        Me.KeyPreview = True
        Me.Name = "frm_ItemName"
        Me.Text = "ItemName"
        Me.TransparencyKey = System.Drawing.Color.Empty
        Me.Controls.SetChildIndex(Me.grdItemName, 0)
        Me.Controls.SetChildIndex(Me.cboItemCategory, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        CType(Me.grdItemName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdItemName As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboItemCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
End Class
