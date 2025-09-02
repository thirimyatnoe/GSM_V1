<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_OtherCash
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_OtherCash))
        Me.grdOtherCash = New System.Windows.Forms.DataGridView()
        Me.CashType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ExchangeRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        CType(Me.grdOtherCash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdOtherCash
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Lavender
        Me.grdOtherCash.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.grdOtherCash.BackgroundColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdOtherCash.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdOtherCash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOtherCash.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CashType, Me.ExchangeRate, Me.Amount, Me.TotalAmount})
        Me.grdOtherCash.Location = New System.Drawing.Point(12, 12)
        Me.grdOtherCash.Name = "grdOtherCash"
        Me.grdOtherCash.RowHeadersWidth = 25
        Me.grdOtherCash.Size = New System.Drawing.Size(479, 206)
        Me.grdOtherCash.TabIndex = 1556
        '
        'CashType
        '
        Me.CashType.HeaderText = "Cash Type"
        Me.CashType.Name = "CashType"
        Me.CashType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CashType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CashType.Width = 150
        '
        'ExchangeRate
        '
        Me.ExchangeRate.HeaderText = "Exchange Rate"
        Me.ExchangeRate.Name = "ExchangeRate"
        Me.ExchangeRate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ExchangeRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ExchangeRate.Width = 130
        '
        'Amount
        '
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.ReadOnly = True
        Me.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Amount.Width = 70
        '
        'TotalAmount
        '
        Me.TotalAmount.HeaderText = "Total"
        Me.TotalAmount.Name = "TotalAmount"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Navy
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(224, 227)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(69, 31)
        Me.btnCancel.TabIndex = 1558
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.Navy
        Me.btnOk.Image = CType(resources.GetObject("btnOk.Image"), System.Drawing.Image)
        Me.btnOk.Location = New System.Drawing.Point(149, 227)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(69, 31)
        Me.btnOk.TabIndex = 1557
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'frm_OtherCash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(501, 269)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.grdOtherCash)
        Me.Location = New System.Drawing.Point(50, 50)
        Me.Name = "frm_OtherCash"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Other Cash"
        CType(Me.grdOtherCash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdOtherCash As System.Windows.Forms.DataGridView
    Friend WithEvents CashType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ExchangeRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
