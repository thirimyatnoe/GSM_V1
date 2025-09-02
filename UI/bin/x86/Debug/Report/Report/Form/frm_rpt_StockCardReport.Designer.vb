<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_StockCardReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_StockCardReport))
        Me.RptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cboItemName = New System.Windows.Forms.ComboBox()
        Me.chkItemName = New System.Windows.Forms.CheckBox()
        Me.cboItemCat = New System.Windows.Forms.ComboBox()
        Me.chkItemCat = New System.Windows.Forms.CheckBox()
        Me.cboGoldQ = New System.Windows.Forms.ComboBox()
        Me.chkGoldQly = New System.Windows.Forms.CheckBox()
        Me.grpBoxDate = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpStockType = New System.Windows.Forms.GroupBox()
        Me.radAll = New System.Windows.Forms.RadioButton()
        Me.radDiamondStock = New System.Windows.Forms.RadioButton()
        Me.radStock = New System.Windows.Forms.RadioButton()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.chkBalance = New System.Windows.Forms.CheckBox()
        Me.chkSale = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.GroupBox3.SuspendLayout()
        Me.grpBoxDate.SuspendLayout()
        Me.grpStockType.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RptViewer
        '
        Me.RptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RptViewer.Location = New System.Drawing.Point(0, 227)
        Me.RptViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RptViewer.Name = "RptViewer"
        Me.RptViewer.Size = New System.Drawing.Size(1293, 471)
        Me.RptViewer.TabIndex = 2
        Me.RptViewer.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cboItemName)
        Me.GroupBox3.Controls.Add(Me.chkItemName)
        Me.GroupBox3.Controls.Add(Me.cboItemCat)
        Me.GroupBox3.Controls.Add(Me.chkItemCat)
        Me.GroupBox3.Controls.Add(Me.cboGoldQ)
        Me.GroupBox3.Controls.Add(Me.chkGoldQly)
        Me.GroupBox3.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox3.Location = New System.Drawing.Point(12, 74)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(334, 130)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select By"
        '
        'cboItemName
        '
        Me.cboItemName.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboItemName.FormattingEnabled = True
        Me.cboItemName.Location = New System.Drawing.Point(104, 91)
        Me.cboItemName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboItemName.Name = "cboItemName"
        Me.cboItemName.Size = New System.Drawing.Size(214, 27)
        Me.cboItemName.TabIndex = 5
        '
        'chkItemName
        '
        Me.chkItemName.AutoSize = True
        Me.chkItemName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemName.Location = New System.Drawing.Point(12, 94)
        Me.chkItemName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkItemName.Name = "chkItemName"
        Me.chkItemName.Size = New System.Drawing.Size(96, 21)
        Me.chkItemName.TabIndex = 4
        Me.chkItemName.Text = "အမျိုးအမည် "
        Me.chkItemName.UseVisualStyleBackColor = True
        '
        'cboItemCat
        '
        Me.cboItemCat.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboItemCat.FormattingEnabled = True
        Me.cboItemCat.Location = New System.Drawing.Point(104, 54)
        Me.cboItemCat.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboItemCat.Name = "cboItemCat"
        Me.cboItemCat.Size = New System.Drawing.Size(214, 27)
        Me.cboItemCat.TabIndex = 3
        '
        'chkItemCat
        '
        Me.chkItemCat.AutoSize = True
        Me.chkItemCat.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemCat.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCat.Location = New System.Drawing.Point(12, 57)
        Me.chkItemCat.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkItemCat.Name = "chkItemCat"
        Me.chkItemCat.Size = New System.Drawing.Size(91, 21)
        Me.chkItemCat.TabIndex = 2
        Me.chkItemCat.Text = "အမျိုးအစား"
        Me.chkItemCat.UseVisualStyleBackColor = True
        '
        'cboGoldQ
        '
        Me.cboGoldQ.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoldQ.FormattingEnabled = True
        Me.cboGoldQ.Location = New System.Drawing.Point(104, 17)
        Me.cboGoldQ.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboGoldQ.Name = "cboGoldQ"
        Me.cboGoldQ.Size = New System.Drawing.Size(214, 27)
        Me.cboGoldQ.TabIndex = 1
        '
        'chkGoldQly
        '
        Me.chkGoldQly.AutoSize = True
        Me.chkGoldQly.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldQly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQly.Location = New System.Drawing.Point(12, 20)
        Me.chkGoldQly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkGoldQly.Name = "chkGoldQly"
        Me.chkGoldQly.Size = New System.Drawing.Size(63, 21)
        Me.chkGoldQly.TabIndex = 0
        Me.chkGoldQly.Text = "ရွှေရည်"
        Me.chkGoldQly.UseVisualStyleBackColor = True
        '
        'grpBoxDate
        '
        Me.grpBoxDate.Controls.Add(Me.Label7)
        Me.grpBoxDate.Controls.Add(Me.Label6)
        Me.grpBoxDate.Controls.Add(Me.dtpToDate)
        Me.grpBoxDate.Controls.Add(Me.dtpFromDate)
        Me.grpBoxDate.Controls.Add(Me.Label1)
        Me.grpBoxDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpBoxDate.Location = New System.Drawing.Point(258, 11)
        Me.grpBoxDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpBoxDate.Name = "grpBoxDate"
        Me.grpBoxDate.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpBoxDate.Size = New System.Drawing.Size(514, 55)
        Me.grpBoxDate.TabIndex = 2
        Me.grpBoxDate.TabStop = False
        Me.grpBoxDate.Text = "နေ့စွဲ"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(257, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "အထိ"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(21, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 17)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "မှ"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy hh:mm:ss tt"
        Me.dtpToDate.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(296, 22)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(203, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy hh:mm:ss tt"
        Me.dtpFromDate.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(43, 21)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(208, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(31, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label2.Location = New System.Drawing.Point(1261, 209)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 200
        Me.Label2.Text = "Counter"
        Me.Label2.Visible = False
        '
        'grpStockType
        '
        Me.grpStockType.Controls.Add(Me.radAll)
        Me.grpStockType.Controls.Add(Me.radDiamondStock)
        Me.grpStockType.Controls.Add(Me.radStock)
        Me.grpStockType.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpStockType.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpStockType.Location = New System.Drawing.Point(12, 10)
        Me.grpStockType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpStockType.Name = "grpStockType"
        Me.grpStockType.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpStockType.Size = New System.Drawing.Size(239, 56)
        Me.grpStockType.TabIndex = 0
        Me.grpStockType.TabStop = False
        Me.grpStockType.Text = " Stock Type"
        '
        'radAll
        '
        Me.radAll.AutoSize = True
        Me.radAll.Checked = True
        Me.radAll.Location = New System.Drawing.Point(24, 23)
        Me.radAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(47, 21)
        Me.radAll.TabIndex = 3
        Me.radAll.TabStop = True
        Me.radAll.Text = "All"
        Me.radAll.UseVisualStyleBackColor = True
        '
        'radDiamondStock
        '
        Me.radDiamondStock.AutoSize = True
        Me.radDiamondStock.Location = New System.Drawing.Point(165, 23)
        Me.radDiamondStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.radDiamondStock.Name = "radDiamondStock"
        Me.radDiamondStock.Size = New System.Drawing.Size(68, 21)
        Me.radDiamondStock.TabIndex = 2
        Me.radDiamondStock.Text = "စိန်ထည်"
        Me.radDiamondStock.UseVisualStyleBackColor = True
        '
        'radStock
        '
        Me.radStock.AutoSize = True
        Me.radStock.Location = New System.Drawing.Point(84, 23)
        Me.radStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.radStock.Name = "radStock"
        Me.radStock.Size = New System.Drawing.Size(67, 21)
        Me.radStock.TabIndex = 0
        Me.radStock.Text = "ရွှေထည်"
        Me.radStock.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1335, 4)
        Me.btnHelpbook.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 48)
        Me.btnHelpbook.TabIndex = 1471
        Me.btnHelpbook.UseVisualStyleBackColor = False
        Me.btnHelpbook.Visible = False
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(455, 129)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 34)
        Me.btnClose.TabIndex = 1473
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(362, 129)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 34)
        Me.btnPreview.TabIndex = 1472
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'chkBalance
        '
        Me.chkBalance.AutoSize = True
        Me.chkBalance.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBalance.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkBalance.Location = New System.Drawing.Point(362, 171)
        Me.chkBalance.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkBalance.Name = "chkBalance"
        Me.chkBalance.Size = New System.Drawing.Size(77, 21)
        Me.chkBalance.TabIndex = 1474
        Me.chkBalance.Text = "လက်ကျန်"
        Me.chkBalance.UseVisualStyleBackColor = True
        '
        'chkSale
        '
        Me.chkSale.AutoSize = True
        Me.chkSale.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSale.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkSale.Location = New System.Drawing.Point(445, 171)
        Me.chkSale.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkSale.Name = "chkSale"
        Me.chkSale.Size = New System.Drawing.Size(78, 21)
        Me.chkSale.TabIndex = 1475
        Me.chkSale.Text = "ရောင်းပြီး"
        Me.chkSale.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.btnHelp)
        Me.Panel1.Controls.Add(Me.chkSale)
        Me.Panel1.Controls.Add(Me.chkBalance)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.grpStockType)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.grpBoxDate)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1293, 227)
        Me.Panel1.TabIndex = 1
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.Color.White
        Me.btnHelp.BackgroundImage = CType(resources.GetObject("btnHelp.BackgroundImage"), System.Drawing.Image)
        Me.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.Location = New System.Drawing.Point(739, 91)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(33, 32)
        Me.btnHelp.TabIndex = 1476
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(362, 91)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1477
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(459, 89)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(171, 27)
        Me.CboLocation.TabIndex = 1478
        '
        'frm_rpt_StockCardReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1293, 698)
        Me.Controls.Add(Me.RptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_rpt_StockCardReport"
        Me.Text = "StockCard Report"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.grpBoxDate.ResumeLayout(False)
        Me.grpBoxDate.PerformLayout()
        Me.grpStockType.ResumeLayout(False)
        Me.grpStockType.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cboItemName As System.Windows.Forms.ComboBox
    Friend WithEvents chkItemName As System.Windows.Forms.CheckBox
    Friend WithEvents cboItemCat As System.Windows.Forms.ComboBox
    Friend WithEvents chkItemCat As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQ As System.Windows.Forms.ComboBox
    Friend WithEvents chkGoldQly As System.Windows.Forms.CheckBox
    Friend WithEvents grpBoxDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpStockType As System.Windows.Forms.GroupBox
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents radDiamondStock As System.Windows.Forms.RadioButton
    Friend WithEvents radStock As System.Windows.Forms.RadioButton
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents chkBalance As System.Windows.Forms.CheckBox
    Friend WithEvents chkSale As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
