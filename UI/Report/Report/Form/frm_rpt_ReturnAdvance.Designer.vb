<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_ReturnAdvance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_ReturnAdvance))
        Me.rptReturnAdvance = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.chkCustomerName = New System.Windows.Forms.CheckBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radNotUse = New System.Windows.Forms.RadioButton()
        Me.radAll = New System.Windows.Forms.RadioButton()
        Me.radUsed = New System.Windows.Forms.RadioButton()
        Me.cboGemsCategory = New System.Windows.Forms.ComboBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.chkGemsCategory = New System.Windows.Forms.CheckBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
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
        'rptReturnAdvance
        '
        Me.rptReturnAdvance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptReturnAdvance.Location = New System.Drawing.Point(0, 128)
        Me.rptReturnAdvance.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rptReturnAdvance.Name = "rptReturnAdvance"
        Me.rptReturnAdvance.Size = New System.Drawing.Size(1354, 334)
        Me.rptReturnAdvance.TabIndex = 5
        Me.rptReturnAdvance.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.txtCustomer)
        Me.Panel1.Controls.Add(Me.lblAddress)
        Me.Panel1.Controls.Add(Me.txtAddress)
        Me.Panel1.Controls.Add(Me.chkCustomerName)
        Me.Panel1.Controls.Add(Me.btnCustomer)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.cboGemsCategory)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.chkGemsCategory)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.grpDate)
        Me.Panel1.Controls.Add(Me.grpview)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1354, 128)
        Me.Panel1.TabIndex = 0
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.Linen
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCustomer.Location = New System.Drawing.Point(727, 22)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.Size = New System.Drawing.Size(172, 27)
        Me.txtCustomer.TabIndex = 1527
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.lblAddress.Location = New System.Drawing.Point(619, 81)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 20)
        Me.lblAddress.TabIndex = 1528
        Me.lblAddress.Text = "လိပ်စာ"
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.Linen
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtAddress.Location = New System.Drawing.Point(668, 80)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(274, 44)
        Me.txtAddress.TabIndex = 1526
        Me.txtAddress.TabStop = False
        '
        'chkCustomerName
        '
        Me.chkCustomerName.AutoSize = True
        Me.chkCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkCustomerName.Location = New System.Drawing.Point(606, 25)
        Me.chkCustomerName.Name = "chkCustomerName"
        Me.chkCustomerName.Size = New System.Drawing.Size(62, 21)
        Me.chkCustomerName.TabIndex = 1529
        Me.chkCustomerName.Text = "ဝယ်သူ"
        Me.chkCustomerName.UseVisualStyleBackColor = True
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(900, 22)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(41, 25)
        Me.btnCustomer.TabIndex = 1525
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Zawgyi-One", 8.25!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(668, 22)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(59, 25)
        Me.txtCustomerCode.TabIndex = 1524
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radNotUse)
        Me.GroupBox1.Controls.Add(Me.radAll)
        Me.GroupBox1.Controls.Add(Me.radUsed)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(350, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(251, 51)
        Me.GroupBox1.TabIndex = 1473
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select"
        '
        'radNotUse
        '
        Me.radNotUse.AutoSize = True
        Me.radNotUse.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radNotUse.Location = New System.Drawing.Point(149, 18)
        Me.radNotUse.Name = "radNotUse"
        Me.radNotUse.Size = New System.Drawing.Size(86, 21)
        Me.radNotUse.TabIndex = 4
        Me.radNotUse.Text = "Not Used"
        Me.radNotUse.UseVisualStyleBackColor = True
        '
        'radAll
        '
        Me.radAll.AutoSize = True
        Me.radAll.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radAll.Location = New System.Drawing.Point(6, 19)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(47, 21)
        Me.radAll.TabIndex = 3
        Me.radAll.Text = "All"
        Me.radAll.UseVisualStyleBackColor = True
        '
        'radUsed
        '
        Me.radUsed.AutoSize = True
        Me.radUsed.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radUsed.Location = New System.Drawing.Point(76, 19)
        Me.radUsed.Name = "radUsed"
        Me.radUsed.Size = New System.Drawing.Size(58, 21)
        Me.radUsed.TabIndex = 2
        Me.radUsed.Text = "Used"
        Me.radUsed.UseVisualStyleBackColor = True
        '
        'cboGemsCategory
        '
        Me.cboGemsCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboGemsCategory.FormattingEnabled = True
        Me.cboGemsCategory.Location = New System.Drawing.Point(1020, 12)
        Me.cboGemsCategory.Name = "cboGemsCategory"
        Me.cboGemsCategory.Size = New System.Drawing.Size(197, 27)
        Me.cboGemsCategory.TabIndex = 1
        Me.cboGemsCategory.Visible = False
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1254, 12)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 1472
        Me.btnHelpbook.UseVisualStyleBackColor = False
        Me.btnHelpbook.Visible = False
        '
        'chkGemsCategory
        '
        Me.chkGemsCategory.AutoSize = True
        Me.chkGemsCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGemsCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGemsCategory.Location = New System.Drawing.Point(1020, 15)
        Me.chkGemsCategory.Name = "chkGemsCategory"
        Me.chkGemsCategory.Size = New System.Drawing.Size(134, 21)
        Me.chkGemsCategory.TabIndex = 0
        Me.chkGemsCategory.Text = "ကျောက်အမျိုးအစား"
        Me.chkGemsCategory.UseVisualStyleBackColor = True
        Me.chkGemsCategory.Visible = False
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(350, 86)
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
        Me.btnClose.Location = New System.Drawing.Point(472, 86)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 1469
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
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
        Me.ChkLocation.Location = New System.Drawing.Point(606, 54)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1530
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(727, 51)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(172, 27)
        Me.CboLocation.TabIndex = 1531
        '
        'frm_rpt_ReturnAdvance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 462)
        Me.Controls.Add(Me.rptReturnAdvance)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Myanmar3", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frm_rpt_ReturnAdvance"
        Me.Text = "Return Advance"
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
    Friend WithEvents rptReturnAdvance As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radNotUse As System.Windows.Forms.RadioButton
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents radUsed As System.Windows.Forms.RadioButton
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents chkCustomerName As System.Windows.Forms.CheckBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
