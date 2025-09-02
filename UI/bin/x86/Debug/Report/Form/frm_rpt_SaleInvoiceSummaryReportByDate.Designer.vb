<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_SaleInvoiceSummaryReportByDate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_SaleInvoiceSummaryReportByDate))
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.radDiamondStock = New System.Windows.Forms.RadioButton()
        Me.radAll = New System.Windows.Forms.RadioButton()
        Me.radStock = New System.Windows.Forms.RadioButton()
        Me.optByChart = New System.Windows.Forms.RadioButton()
        Me.chkMostSaleItem = New System.Windows.Forms.CheckBox()
        Me.chkSalePercentage = New System.Windows.Forms.CheckBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.grpByGroup = New System.Windows.Forms.GroupBox()
        Me.optByItemCategory = New System.Windows.Forms.RadioButton()
        Me.optByDate = New System.Windows.Forms.RadioButton()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkByChart = New System.Windows.Forms.CheckBox()
        Me.chkItemCategory = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.cboGoldQuality = New System.Windows.Forms.ComboBox()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.grpByGroup.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rptViewer.Location = New System.Drawing.Point(0, 125)
        Me.rptViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(1354, 485)
        Me.rptViewer.TabIndex = 1
        Me.rptViewer.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.optByChart)
        Me.Panel1.Controls.Add(Me.chkMostSaleItem)
        Me.Panel1.Controls.Add(Me.chkSalePercentage)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.grpByGroup)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1354, 125)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.radDiamondStock)
        Me.GroupBox4.Controls.Add(Me.radAll)
        Me.GroupBox4.Controls.Add(Me.radStock)
        Me.GroupBox4.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox4.Location = New System.Drawing.Point(12, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(291, 52)
        Me.GroupBox4.TabIndex = 1504
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Type"
        '
        'radDiamondStock
        '
        Me.radDiamondStock.AutoSize = True
        Me.radDiamondStock.Location = New System.Drawing.Point(210, 18)
        Me.radDiamondStock.Name = "radDiamondStock"
        Me.radDiamondStock.Size = New System.Drawing.Size(68, 21)
        Me.radDiamondStock.TabIndex = 2
        Me.radDiamondStock.Text = "စိန်ထည်"
        Me.radDiamondStock.UseVisualStyleBackColor = True
        '
        'radAll
        '
        Me.radAll.AutoSize = True
        Me.radAll.Checked = True
        Me.radAll.Location = New System.Drawing.Point(48, 18)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(47, 21)
        Me.radAll.TabIndex = 0
        Me.radAll.TabStop = True
        Me.radAll.Text = "All"
        Me.radAll.UseVisualStyleBackColor = True
        '
        'radStock
        '
        Me.radStock.AutoSize = True
        Me.radStock.Location = New System.Drawing.Point(122, 16)
        Me.radStock.Name = "radStock"
        Me.radStock.Size = New System.Drawing.Size(67, 21)
        Me.radStock.TabIndex = 1
        Me.radStock.Text = "ရွှေထည်"
        Me.radStock.UseVisualStyleBackColor = True
        '
        'optByChart
        '
        Me.optByChart.AutoSize = True
        Me.optByChart.Location = New System.Drawing.Point(1090, 27)
        Me.optByChart.Name = "optByChart"
        Me.optByChart.Size = New System.Drawing.Size(66, 20)
        Me.optByChart.TabIndex = 1479
        Me.optByChart.Text = "ByChart"
        Me.optByChart.UseVisualStyleBackColor = True
        Me.optByChart.Visible = False
        '
        'chkMostSaleItem
        '
        Me.chkMostSaleItem.AutoSize = True
        Me.chkMostSaleItem.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMostSaleItem.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkMostSaleItem.Location = New System.Drawing.Point(766, 87)
        Me.chkMostSaleItem.Name = "chkMostSaleItem"
        Me.chkMostSaleItem.Size = New System.Drawing.Size(170, 21)
        Me.chkMostSaleItem.TabIndex = 1485
        Me.chkMostSaleItem.Text = "အရောင်းရဆုံးပစ္စည်းစာရင်း"
        Me.chkMostSaleItem.UseVisualStyleBackColor = True
        '
        'chkSalePercentage
        '
        Me.chkSalePercentage.AutoSize = True
        Me.chkSalePercentage.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSalePercentage.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkSalePercentage.Location = New System.Drawing.Point(643, 87)
        Me.chkSalePercentage.Name = "chkSalePercentage"
        Me.chkSalePercentage.Size = New System.Drawing.Size(127, 21)
        Me.chkSalePercentage.TabIndex = 1484
        Me.chkSalePercentage.Text = "Sale Percentage"
        Me.chkSalePercentage.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(967, 24)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 1483
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'grpByGroup
        '
        Me.grpByGroup.BackColor = System.Drawing.Color.Transparent
        Me.grpByGroup.Controls.Add(Me.optByItemCategory)
        Me.grpByGroup.Controls.Add(Me.optByDate)
        Me.grpByGroup.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpByGroup.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpByGroup.Location = New System.Drawing.Point(10, 59)
        Me.grpByGroup.Name = "grpByGroup"
        Me.grpByGroup.Size = New System.Drawing.Size(293, 56)
        Me.grpByGroup.TabIndex = 1477
        Me.grpByGroup.TabStop = False
        Me.grpByGroup.Text = "Group By"
        '
        'optByItemCategory
        '
        Me.optByItemCategory.AutoSize = True
        Me.optByItemCategory.Location = New System.Drawing.Point(151, 24)
        Me.optByItemCategory.Name = "optByItemCategory"
        Me.optByItemCategory.Size = New System.Drawing.Size(129, 21)
        Me.optByItemCategory.TabIndex = 1
        Me.optByItemCategory.Text = "အမျိုးအမည်အလိုက်"
        Me.optByItemCategory.UseVisualStyleBackColor = True
        '
        'optByDate
        '
        Me.optByDate.AutoSize = True
        Me.optByDate.Checked = True
        Me.optByDate.Location = New System.Drawing.Point(42, 23)
        Me.optByDate.Name = "optByDate"
        Me.optByDate.Size = New System.Drawing.Size(87, 21)
        Me.optByDate.TabIndex = 0
        Me.optByDate.TabStop = True
        Me.optByDate.Text = "နေ့စွဲအလိုက်"
        Me.optByDate.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(371, 81)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 1481
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(464, 82)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 1482
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.GroupBox2.Controls.Add(Me.chkByChart)
        Me.GroupBox2.Controls.Add(Me.chkItemCategory)
        Me.GroupBox2.Controls.Add(Me.cboCategory)
        Me.GroupBox2.Controls.Add(Me.cboGoldQuality)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(1023, 63)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(133, 45)
        Me.GroupBox2.TabIndex = 1478
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Visible = False
        '
        'chkByChart
        '
        Me.chkByChart.AutoSize = True
        Me.chkByChart.BackColor = System.Drawing.Color.Transparent
        Me.chkByChart.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkByChart.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkByChart.Location = New System.Drawing.Point(16, 2)
        Me.chkByChart.Name = "chkByChart"
        Me.chkByChart.Size = New System.Drawing.Size(80, 21)
        Me.chkByChart.TabIndex = 6
        Me.chkByChart.Text = "ByChart"
        Me.chkByChart.UseVisualStyleBackColor = False
        Me.chkByChart.Visible = False
        '
        'chkItemCategory
        '
        Me.chkItemCategory.AutoSize = True
        Me.chkItemCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCategory.Location = New System.Drawing.Point(16, 59)
        Me.chkItemCategory.Name = "chkItemCategory"
        Me.chkItemCategory.Size = New System.Drawing.Size(117, 21)
        Me.chkItemCategory.TabIndex = 2
        Me.chkItemCategory.Text = "Item Category"
        Me.chkItemCategory.UseVisualStyleBackColor = True
        '
        'cboCategory
        '
        Me.cboCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(43, 56)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(72, 27)
        Me.cboCategory.TabIndex = 3
        '
        'cboGoldQuality
        '
        Me.cboGoldQuality.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboGoldQuality.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboGoldQuality.FormattingEnabled = True
        Me.cboGoldQuality.Location = New System.Drawing.Point(43, 29)
        Me.cboGoldQuality.Name = "cboGoldQuality"
        Me.cboGoldQuality.Size = New System.Drawing.Size(72, 27)
        Me.cboGoldQuality.TabIndex = 1
        '
        'grpByDate
        '
        Me.grpByDate.BackColor = System.Drawing.Color.Transparent
        Me.grpByDate.Controls.Add(Me.dtpToDate)
        Me.grpByDate.Controls.Add(Me.dtpFromDate)
        Me.grpByDate.Controls.Add(Me.Label1)
        Me.grpByDate.Controls.Add(Me.Label2)
        Me.grpByDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpByDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpByDate.Location = New System.Drawing.Point(313, 7)
        Me.grpByDate.Name = "grpByDate"
        Me.grpByDate.Size = New System.Drawing.Size(321, 56)
        Me.grpByDate.TabIndex = 1480
        Me.grpByDate.TabStop = False
        Me.grpByDate.Text = "Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(194, 23)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpToDate.TabIndex = 2
        Me.dtpToDate.TabStop = False
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(52, 23)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpFromDate.TabIndex = 1
        Me.dtpFromDate.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "မှ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(158, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "အထိ"
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(643, 33)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1505
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(761, 30)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(171, 27)
        Me.CboLocation.TabIndex = 1506
        '
        'frm_rpt_SaleInvoiceSummaryReportByDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 610)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frm_rpt_SaleInvoiceSummaryReportByDate"
        Me.Text = "Sale Invoice Summary"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.grpByGroup.ResumeLayout(False)
        Me.grpByGroup.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents optByChart As System.Windows.Forms.RadioButton
    Friend WithEvents chkMostSaleItem As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalePercentage As System.Windows.Forms.CheckBox
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents grpByGroup As System.Windows.Forms.GroupBox
    Friend WithEvents optByItemCategory As System.Windows.Forms.RadioButton
    Friend WithEvents optByDate As System.Windows.Forms.RadioButton
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkByChart As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemCategory As System.Windows.Forms.CheckBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cboGoldQuality As System.Windows.Forms.ComboBox
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents radDiamondStock As System.Windows.Forms.RadioButton
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents radStock As System.Windows.Forms.RadioButton
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
