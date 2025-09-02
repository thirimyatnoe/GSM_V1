<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_SaleGems
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_SaleGems))
        Me.rptSaleGems = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboGemsCategory = New System.Windows.Forms.ComboBox()
        Me.chkGemsCategory = New System.Windows.Forms.CheckBox()
        Me.grpDate = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.grpview = New System.Windows.Forms.GroupBox()
        Me.radSummary = New System.Windows.Forms.RadioButton()
        Me.radDetail = New System.Windows.Forms.RadioButton()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpDate.SuspendLayout()
        Me.grpview.SuspendLayout()
        Me.SuspendLayout()
        '
        'rptSaleGems
        '
        Me.rptSaleGems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptSaleGems.Location = New System.Drawing.Point(0, 128)
        Me.rptSaleGems.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rptSaleGems.Name = "rptSaleGems"
        Me.rptSaleGems.Size = New System.Drawing.Size(1354, 334)
        Me.rptSaleGems.TabIndex = 5
        Me.rptSaleGems.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.grpDate)
        Me.Panel1.Controls.Add(Me.grpview)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1354, 128)
        Me.Panel1.TabIndex = 0
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1332, 4)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 1472
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(705, 70)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 1468
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
        Me.btnClose.Location = New System.Drawing.Point(800, 70)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 1469
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboGemsCategory)
        Me.GroupBox1.Controls.Add(Me.chkGemsCategory)
        Me.GroupBox1.Location = New System.Drawing.Point(350, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(362, 50)
        Me.GroupBox1.TabIndex = 1467
        Me.GroupBox1.TabStop = False
        '
        'cboGemsCategory
        '
        Me.cboGemsCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboGemsCategory.FormattingEnabled = True
        Me.cboGemsCategory.Location = New System.Drawing.Point(152, 14)
        Me.cboGemsCategory.Name = "cboGemsCategory"
        Me.cboGemsCategory.Size = New System.Drawing.Size(197, 27)
        Me.cboGemsCategory.TabIndex = 1
        '
        'chkGemsCategory
        '
        Me.chkGemsCategory.AutoSize = True
        Me.chkGemsCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGemsCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGemsCategory.Location = New System.Drawing.Point(15, 17)
        Me.chkGemsCategory.Name = "chkGemsCategory"
        Me.chkGemsCategory.Size = New System.Drawing.Size(134, 21)
        Me.chkGemsCategory.TabIndex = 0
        Me.chkGemsCategory.Text = "ကျောက်အမျိုးအစား"
        Me.chkGemsCategory.UseVisualStyleBackColor = True
        '
        'grpDate
        '
        Me.grpDate.Controls.Add(Me.Label1)
        Me.grpDate.Controls.Add(Me.Label2)
        Me.grpDate.Controls.Add(Me.dtpFromDate)
        Me.grpDate.Controls.Add(Me.dtpToDate)
        Me.grpDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpDate.Location = New System.Drawing.Point(12, 64)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(332, 53)
        Me.grpDate.TabIndex = 1466
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "နေ့စွဲ"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(36, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 17)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "မှ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(175, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 17)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "ထိ"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(63, 16)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(215, 16)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'grpview
        '
        Me.grpview.BackColor = System.Drawing.SystemColors.Control
        Me.grpview.Controls.Add(Me.radSummary)
        Me.grpview.Controls.Add(Me.radDetail)
        Me.grpview.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpview.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpview.Location = New System.Drawing.Point(12, 12)
        Me.grpview.Name = "grpview"
        Me.grpview.Size = New System.Drawing.Size(332, 50)
        Me.grpview.TabIndex = 1465
        Me.grpview.TabStop = False
        Me.grpview.Text = "View"
        '
        'radSummary
        '
        Me.radSummary.AutoSize = True
        Me.radSummary.Checked = True
        Me.radSummary.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radSummary.Location = New System.Drawing.Point(52, 19)
        Me.radSummary.Name = "radSummary"
        Me.radSummary.Size = New System.Drawing.Size(85, 21)
        Me.radSummary.TabIndex = 0
        Me.radSummary.TabStop = True
        Me.radSummary.Text = "စာရင်းချုပ်"
        Me.radSummary.UseVisualStyleBackColor = True
        '
        'radDetail
        '
        Me.radDetail.AutoSize = True
        Me.radDetail.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radDetail.Location = New System.Drawing.Point(190, 19)
        Me.radDetail.Name = "radDetail"
        Me.radDetail.Size = New System.Drawing.Size(85, 21)
        Me.radDetail.TabIndex = 1
        Me.radDetail.Text = "အသေးစိတ်"
        Me.radDetail.UseVisualStyleBackColor = True
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(365, 75)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1509
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(501, 72)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(198, 27)
        Me.CboLocation.TabIndex = 1510
        '
        'frm_rpt_SaleGems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 462)
        Me.Controls.Add(Me.rptSaleGems)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Myanmar3", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frm_rpt_SaleGems"
        Me.Text = "Sale Gems"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpDate.ResumeLayout(False)
        Me.grpDate.PerformLayout()
        Me.grpview.ResumeLayout(False)
        Me.grpview.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rptSaleGems As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboGemsCategory As System.Windows.Forms.ComboBox
    Friend WithEvents chkGemsCategory As System.Windows.Forms.CheckBox
    Friend WithEvents grpDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents grpview As System.Windows.Forms.GroupBox
    Friend WithEvents radSummary As System.Windows.Forms.RadioButton
    Friend WithEvents radDetail As System.Windows.Forms.RadioButton
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
