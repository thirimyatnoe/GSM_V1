<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CashType
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CashType))
        Me.grdCash = New System.Windows.Forms.DataGridView()
        Me.CashTypeID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CashType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        CType(Me.grdCash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdCash
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdCash.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCash.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdCash.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdCash.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grdCash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCash.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CashTypeID, Me.CashType})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Myanmar3", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdCash.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdCash.Location = New System.Drawing.Point(13, 14)
        Me.grdCash.Margin = New System.Windows.Forms.Padding(4)
        Me.grdCash.Name = "grdCash"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Myanmar3", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCash.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdCash.RowHeadersWidth = 25
        Me.grdCash.Size = New System.Drawing.Size(436, 400)
        Me.grdCash.TabIndex = 202
        '
        'CashTypeID
        '
        Me.CashTypeID.HeaderText = "CashTypeID"
        Me.CashTypeID.Name = "CashTypeID"
        Me.CashTypeID.Visible = False
        Me.CashTypeID.Width = 70
        '
        'CashType
        '
        Me.CashType.HeaderText = "Cash Type"
        Me.CashType.Name = "CashType"
        Me.CashType.Width = 200
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(469, 14)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(49, 48)
        Me.btnHelpbook.TabIndex = 227
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_CashType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.ClientSize = New System.Drawing.Size(530, 489)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.grdCash)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frm_CashType"
        Me.Text = "Cash Type"
        Me.Controls.SetChildIndex(Me.grdCash, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        CType(Me.grdCash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdCash As System.Windows.Forms.DataGridView
    Friend WithEvents CashTypeID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CashType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button

End Class
