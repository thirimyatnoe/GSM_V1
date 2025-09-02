<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GE_EventLogs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GE_EventLogs))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.chkAction = New System.Windows.Forms.CheckBox()
        Me.cboAction = New System.Windows.Forms.ComboBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.grpDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.chkSource = New System.Windows.Forms.CheckBox()
        Me.cboSource = New System.Windows.Forms.ComboBox()
        Me.grd = New System.Windows.Forms.DataGridView()
        Me.txtAffectedID = New System.Windows.Forms.TextBox()
        Me.chkAffectedID = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.grpDate.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(915, 8)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 49
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.Panel1.Controls.Add(Me.chkAffectedID)
        Me.Panel1.Controls.Add(Me.txtAffectedID)
        Me.Panel1.Controls.Add(Me.btnExport)
        Me.Panel1.Controls.Add(Me.chkAction)
        Me.Panel1.Controls.Add(Me.cboAction)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.grpDate)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.chkSource)
        Me.Panel1.Controls.Add(Me.cboSource)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1215, 80)
        Me.Panel1.TabIndex = 2
        '
        'btnExport
        '
        Me.btnExport.BackgroundImage = CType(resources.GetObject("btnExport.BackgroundImage"), System.Drawing.Image)
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ForeColor = System.Drawing.Color.DarkRed
        Me.btnExport.Location = New System.Drawing.Point(1067, 40)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(104, 31)
        Me.btnExport.TabIndex = 1466
        Me.btnExport.Text = "Export To Excel"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'chkAction
        '
        Me.chkAction.AutoSize = True
        Me.chkAction.BackColor = System.Drawing.Color.Transparent
        Me.chkAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkAction.Location = New System.Drawing.Point(264, 54)
        Me.chkAction.Name = "chkAction"
        Me.chkAction.Size = New System.Drawing.Size(62, 17)
        Me.chkAction.TabIndex = 1465
        Me.chkAction.Text = "Action"
        Me.chkAction.UseVisualStyleBackColor = False
        '
        'cboAction
        '
        Me.cboAction.Font = New System.Drawing.Font("Zawgyi-One", 8.25!)
        Me.cboAction.FormattingEnabled = True
        Me.cboAction.Items.AddRange(New Object() {"INSERT", "UPDATE", "DELETE"})
        Me.cboAction.Location = New System.Drawing.Point(365, 49)
        Me.cboAction.Name = "cboAction"
        Me.cboAction.Size = New System.Drawing.Size(256, 26)
        Me.cboAction.TabIndex = 1464
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1008, 8)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 29)
        Me.btnHelpbook.TabIndex = 1461
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClear.Location = New System.Drawing.Point(3, 46)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(135, 31)
        Me.btnClear.TabIndex = 51
        Me.btnClear.Text = "Clear all events logs"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'grpDate
        '
        Me.grpDate.Controls.Add(Me.dtpToDate)
        Me.grpDate.Controls.Add(Me.Label1)
        Me.grpDate.Controls.Add(Me.dtpFromDate)
        Me.grpDate.Controls.Add(Me.Label2)
        Me.grpDate.Location = New System.Drawing.Point(3, 3)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(255, 41)
        Me.grpDate.TabIndex = 50
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(166, 15)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(83, 20)
        Me.dtpToDate.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(50, 15)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(83, 20)
        Me.dtpFromDate.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(141, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "To"
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(915, 43)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 48
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'chkSource
        '
        Me.chkSource.AutoSize = True
        Me.chkSource.BackColor = System.Drawing.Color.Transparent
        Me.chkSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkSource.Location = New System.Drawing.Point(264, 20)
        Me.chkSource.Name = "chkSource"
        Me.chkSource.Size = New System.Drawing.Size(93, 17)
        Me.chkSource.TabIndex = 47
        Me.chkSource.Text = "Menu Name"
        Me.chkSource.UseVisualStyleBackColor = False
        '
        'cboSource
        '
        Me.cboSource.Font = New System.Drawing.Font("Zawgyi-One", 8.25!)
        Me.cboSource.FormattingEnabled = True
        Me.cboSource.Items.AddRange(New Object() {"BarcodeNo", "PurchaseStock", "SaleStock", "SaleVolumeStock", "SaleGems", "OrderStock", "OrderStockReturn", "RepairStock", "RepairStockReturn", "CashReceiptOnCredit", "CurrentPrice", "DailyExpense", "DailyIncome", "Location", "Customer", "Staff", "GemsCategory", "GoldQuality", "ItemCategory", "ItemName", ""})
        Me.cboSource.Location = New System.Drawing.Point(365, 15)
        Me.cboSource.Name = "cboSource"
        Me.cboSource.Size = New System.Drawing.Size(256, 26)
        Me.cboSource.TabIndex = 46
        '
        'grd
        '
        Me.grd.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.Location = New System.Drawing.Point(0, 80)
        Me.grd.Name = "grd"
        Me.grd.RowHeadersWidth = 25
        Me.grd.Size = New System.Drawing.Size(1215, 182)
        Me.grd.TabIndex = 5
        '
        'txtAffectedID
        '
        Me.txtAffectedID.BackColor = System.Drawing.Color.White
        Me.txtAffectedID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAffectedID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAffectedID.HideSelection = False
        Me.txtAffectedID.Location = New System.Drawing.Point(721, 15)
        Me.txtAffectedID.Name = "txtAffectedID"
        Me.txtAffectedID.Size = New System.Drawing.Size(169, 22)
        Me.txtAffectedID.TabIndex = 1467
        '
        'chkAffectedID
        '
        Me.chkAffectedID.AutoSize = True
        Me.chkAffectedID.BackColor = System.Drawing.Color.Transparent
        Me.chkAffectedID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkAffectedID.Location = New System.Drawing.Point(627, 17)
        Me.chkAffectedID.Name = "chkAffectedID"
        Me.chkAffectedID.Size = New System.Drawing.Size(87, 17)
        Me.chkAffectedID.TabIndex = 1468
        Me.chkAffectedID.Text = "AffectedID"
        Me.chkAffectedID.UseVisualStyleBackColor = False
        '
        'frm_GE_EventLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 262)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_GE_EventLogs"
        Me.Text = "Event Logs List"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpDate.ResumeLayout(False)
        Me.grpDate.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grpDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkSource As System.Windows.Forms.CheckBox
    Friend WithEvents cboSource As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents chkAction As System.Windows.Forms.CheckBox
    Friend WithEvents cboAction As System.Windows.Forms.ComboBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents txtAffectedID As System.Windows.Forms.TextBox
    Friend WithEvents chkAffectedID As System.Windows.Forms.CheckBox
End Class
