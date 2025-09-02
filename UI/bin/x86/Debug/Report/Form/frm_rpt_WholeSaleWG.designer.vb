<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_WholeSaleWG
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_WholeSaleWG))
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboItemName = New System.Windows.Forms.ComboBox()
        Me.ChkItemName = New System.Windows.Forms.CheckBox()
        Me.chkGoldQuality = New System.Windows.Forms.CheckBox()
        Me.cboGoldQuality = New System.Windows.Forms.ComboBox()
        Me.chkItemCategory = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radSummaryByDate = New System.Windows.Forms.RadioButton()
        Me.radSummary = New System.Windows.Forms.RadioButton()
        Me.radDetail = New System.Windows.Forms.RadioButton()
        Me.chkCustomer = New System.Windows.Forms.CheckBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optConsignBalance = New System.Windows.Forms.RadioButton()
        Me.optConsignmentSale = New System.Windows.Forms.RadioButton()
        Me.optPayReturn = New System.Windows.Forms.RadioButton()
        Me.optSaleReturn = New System.Windows.Forms.RadioButton()
        Me.optSale = New System.Windows.Forms.RadioButton()
        Me.optPay = New System.Windows.Forms.RadioButton()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.chkLocation = New System.Windows.Forms.CheckBox()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBarcodeNo = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 132)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(1118, 397)
        Me.rptViewer.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtBarcodeNo)
        Me.Panel1.Controls.Add(Me.cboItemName)
        Me.Panel1.Controls.Add(Me.ChkItemName)
        Me.Panel1.Controls.Add(Me.chkGoldQuality)
        Me.Panel1.Controls.Add(Me.cboGoldQuality)
        Me.Panel1.Controls.Add(Me.chkItemCategory)
        Me.Panel1.Controls.Add(Me.cboCategory)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.chkCustomer)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnCustomer)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.cboLocation)
        Me.Panel1.Controls.Add(Me.chkLocation)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1118, 132)
        Me.Panel1.TabIndex = 4
        '
        'cboItemName
        '
        Me.cboItemName.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboItemName.FormattingEnabled = True
        Me.cboItemName.Location = New System.Drawing.Point(707, 43)
        Me.cboItemName.Name = "cboItemName"
        Me.cboItemName.Size = New System.Drawing.Size(171, 27)
        Me.cboItemName.TabIndex = 821
        '
        'ChkItemName
        '
        Me.ChkItemName.AutoSize = True
        Me.ChkItemName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkItemName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkItemName.Location = New System.Drawing.Point(579, 46)
        Me.ChkItemName.Name = "ChkItemName"
        Me.ChkItemName.Size = New System.Drawing.Size(126, 21)
        Me.ChkItemName.TabIndex = 820
        Me.ChkItemName.Text = "ပစ္စည်းအမျိုးအမည်"
        Me.ChkItemName.UseVisualStyleBackColor = True
        '
        'chkGoldQuality
        '
        Me.chkGoldQuality.AutoSize = True
        Me.chkGoldQuality.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldQuality.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQuality.Location = New System.Drawing.Point(579, 76)
        Me.chkGoldQuality.Name = "chkGoldQuality"
        Me.chkGoldQuality.Size = New System.Drawing.Size(63, 21)
        Me.chkGoldQuality.TabIndex = 818
        Me.chkGoldQuality.Text = "ရွှေရည်"
        Me.chkGoldQuality.UseVisualStyleBackColor = True
        '
        'cboGoldQuality
        '
        Me.cboGoldQuality.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboGoldQuality.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboGoldQuality.FormattingEnabled = True
        Me.cboGoldQuality.Location = New System.Drawing.Point(707, 73)
        Me.cboGoldQuality.Name = "cboGoldQuality"
        Me.cboGoldQuality.Size = New System.Drawing.Size(171, 27)
        Me.cboGoldQuality.TabIndex = 819
        '
        'chkItemCategory
        '
        Me.chkItemCategory.AutoSize = True
        Me.chkItemCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCategory.Location = New System.Drawing.Point(579, 15)
        Me.chkItemCategory.Name = "chkItemCategory"
        Me.chkItemCategory.Size = New System.Drawing.Size(125, 21)
        Me.chkItemCategory.TabIndex = 816
        Me.chkItemCategory.Text = "ပစ္စည်းအမျိုးအစား"
        Me.chkItemCategory.UseVisualStyleBackColor = True
        '
        'cboCategory
        '
        Me.cboCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(708, 12)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(171, 27)
        Me.cboCategory.TabIndex = 817
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radSummaryByDate)
        Me.GroupBox1.Controls.Add(Me.radSummary)
        Me.GroupBox1.Controls.Add(Me.radDetail)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(13, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(207, 76)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View By"
        '
        'radSummaryByDate
        '
        Me.radSummaryByDate.AutoSize = True
        Me.radSummaryByDate.BackColor = System.Drawing.SystemColors.Control
        Me.radSummaryByDate.Checked = True
        Me.radSummaryByDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radSummaryByDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radSummaryByDate.Location = New System.Drawing.Point(9, 43)
        Me.radSummaryByDate.Name = "radSummaryByDate"
        Me.radSummaryByDate.Size = New System.Drawing.Size(140, 21)
        Me.radSummaryByDate.TabIndex = 2
        Me.radSummaryByDate.TabStop = True
        Me.radSummaryByDate.Text = "Summary By Date"
        Me.radSummaryByDate.UseVisualStyleBackColor = False
        '
        'radSummary
        '
        Me.radSummary.AutoSize = True
        Me.radSummary.BackColor = System.Drawing.SystemColors.Control
        Me.radSummary.Checked = True
        Me.radSummary.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radSummary.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radSummary.Location = New System.Drawing.Point(9, 15)
        Me.radSummary.Name = "radSummary"
        Me.radSummary.Size = New System.Drawing.Size(85, 21)
        Me.radSummary.TabIndex = 0
        Me.radSummary.TabStop = True
        Me.radSummary.Text = "စာရင်းချုပ်"
        Me.radSummary.UseVisualStyleBackColor = False
        '
        'radDetail
        '
        Me.radDetail.AutoSize = True
        Me.radDetail.BackColor = System.Drawing.SystemColors.Control
        Me.radDetail.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radDetail.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radDetail.Location = New System.Drawing.Point(95, 15)
        Me.radDetail.Name = "radDetail"
        Me.radDetail.Size = New System.Drawing.Size(85, 21)
        Me.radDetail.TabIndex = 1
        Me.radDetail.Text = "အသေးစိတ်"
        Me.radDetail.UseVisualStyleBackColor = False
        '
        'chkCustomer
        '
        Me.chkCustomer.AutoSize = True
        Me.chkCustomer.BackColor = System.Drawing.Color.Transparent
        Me.chkCustomer.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkCustomer.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkCustomer.Location = New System.Drawing.Point(886, 18)
        Me.chkCustomer.Name = "chkCustomer"
        Me.chkCustomer.Size = New System.Drawing.Size(87, 21)
        Me.chkCustomer.TabIndex = 5
        Me.chkCustomer.Text = "Customer"
        Me.chkCustomer.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(915, 88)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 8
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(1085, 16)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(30, 23)
        Me.btnCustomer.TabIndex = 7
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(1022, 89)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtCustomerName.Location = New System.Drawing.Point(906, 47)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(203, 26)
        Me.txtCustomerName.TabIndex = 815
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(975, 16)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(107, 26)
        Me.txtCustomerCode.TabIndex = 6
        Me.txtCustomerCode.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.optConsignBalance)
        Me.GroupBox2.Controls.Add(Me.optConsignmentSale)
        Me.GroupBox2.Controls.Add(Me.optPayReturn)
        Me.GroupBox2.Controls.Add(Me.optSaleReturn)
        Me.GroupBox2.Controls.Add(Me.optSale)
        Me.GroupBox2.Controls.Add(Me.optPay)
        Me.GroupBox2.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox2.Location = New System.Drawing.Point(227, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(346, 77)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Type"
        '
        'optConsignBalance
        '
        Me.optConsignBalance.AutoSize = True
        Me.optConsignBalance.BackColor = System.Drawing.SystemColors.Control
        Me.optConsignBalance.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optConsignBalance.Location = New System.Drawing.Point(209, 13)
        Me.optConsignBalance.Name = "optConsignBalance"
        Me.optConsignBalance.Size = New System.Drawing.Size(130, 21)
        Me.optConsignBalance.TabIndex = 5
        Me.optConsignBalance.Text = "ConsignBalance"
        Me.optConsignBalance.UseVisualStyleBackColor = False
        '
        'optConsignmentSale
        '
        Me.optConsignmentSale.AutoSize = True
        Me.optConsignmentSale.BackColor = System.Drawing.SystemColors.Control
        Me.optConsignmentSale.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optConsignmentSale.Location = New System.Drawing.Point(73, 13)
        Me.optConsignmentSale.Name = "optConsignmentSale"
        Me.optConsignmentSale.Size = New System.Drawing.Size(137, 21)
        Me.optConsignmentSale.TabIndex = 1
        Me.optConsignmentSale.Text = "ConsignmentSale"
        Me.optConsignmentSale.UseVisualStyleBackColor = False
        '
        'optPayReturn
        '
        Me.optPayReturn.AutoSize = True
        Me.optPayReturn.BackColor = System.Drawing.SystemColors.Control
        Me.optPayReturn.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optPayReturn.Location = New System.Drawing.Point(73, 36)
        Me.optPayReturn.Name = "optPayReturn"
        Me.optPayReturn.Size = New System.Drawing.Size(96, 21)
        Me.optPayReturn.TabIndex = 3
        Me.optPayReturn.Text = "Pay Return"
        Me.optPayReturn.UseVisualStyleBackColor = False
        '
        'optSaleReturn
        '
        Me.optSaleReturn.AutoSize = True
        Me.optSaleReturn.BackColor = System.Drawing.SystemColors.Control
        Me.optSaleReturn.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optSaleReturn.Location = New System.Drawing.Point(209, 35)
        Me.optSaleReturn.Name = "optSaleReturn"
        Me.optSaleReturn.Size = New System.Drawing.Size(100, 21)
        Me.optSaleReturn.TabIndex = 4
        Me.optSaleReturn.Text = "Sale Return"
        Me.optSaleReturn.UseVisualStyleBackColor = False
        '
        'optSale
        '
        Me.optSale.AutoSize = True
        Me.optSale.BackColor = System.Drawing.SystemColors.Control
        Me.optSale.Checked = True
        Me.optSale.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optSale.Location = New System.Drawing.Point(7, 13)
        Me.optSale.Name = "optSale"
        Me.optSale.Size = New System.Drawing.Size(53, 21)
        Me.optSale.TabIndex = 0
        Me.optSale.TabStop = True
        Me.optSale.Text = "Sale"
        Me.optSale.UseVisualStyleBackColor = False
        '
        'optPay
        '
        Me.optPay.AutoSize = True
        Me.optPay.BackColor = System.Drawing.SystemColors.Control
        Me.optPay.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optPay.Location = New System.Drawing.Point(7, 36)
        Me.optPay.Name = "optPay"
        Me.optPay.Size = New System.Drawing.Size(49, 21)
        Me.optPay.TabIndex = 2
        Me.optPay.Text = "Pay"
        Me.optPay.UseVisualStyleBackColor = False
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(707, 102)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(170, 26)
        Me.cboLocation.TabIndex = 4
        '
        'chkLocation
        '
        Me.chkLocation.AutoSize = True
        Me.chkLocation.BackColor = System.Drawing.Color.Transparent
        Me.chkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkLocation.Location = New System.Drawing.Point(579, 103)
        Me.chkLocation.Name = "chkLocation"
        Me.chkLocation.Size = New System.Drawing.Size(84, 21)
        Me.chkLocation.TabIndex = 3
        Me.chkLocation.Text = "Location"
        Me.chkLocation.UseVisualStyleBackColor = False
        '
        'grpByDate
        '
        Me.grpByDate.BackColor = System.Drawing.Color.Transparent
        Me.grpByDate.Controls.Add(Me.dtpToDate)
        Me.grpByDate.Controls.Add(Me.dtpFromDate)
        Me.grpByDate.Controls.Add(Me.Label1)
        Me.grpByDate.Controls.Add(Me.Label2)
        Me.grpByDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.grpByDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpByDate.Location = New System.Drawing.Point(13, 78)
        Me.grpByDate.Name = "grpByDate"
        Me.grpByDate.Size = New System.Drawing.Size(303, 48)
        Me.grpByDate.TabIndex = 2
        Me.grpByDate.TabStop = False
        Me.grpByDate.Text = "Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(189, 16)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(51, 14)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(10, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(158, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(320, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 17)
        Me.Label4.TabIndex = 1491
        Me.Label4.Text = "BarcodeNo"
        '
        'txtBarcodeNo
        '
        Me.txtBarcodeNo.BackColor = System.Drawing.Color.White
        Me.txtBarcodeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcodeNo.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtBarcodeNo.Location = New System.Drawing.Point(404, 96)
        Me.txtBarcodeNo.Name = "txtBarcodeNo"
        Me.txtBarcodeNo.Size = New System.Drawing.Size(169, 24)
        Me.txtBarcodeNo.TabIndex = 1490
        Me.txtBarcodeNo.Text = " "
        '
        'frm_rpt_WholeSaleWG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 529)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_rpt_WholeSaleWG"
        Me.Text = "WholeSale Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents chkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents chkCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents optSale As System.Windows.Forms.RadioButton
    Friend WithEvents optPay As System.Windows.Forms.RadioButton
    Friend WithEvents optPayReturn As System.Windows.Forms.RadioButton
    Friend WithEvents optSaleReturn As System.Windows.Forms.RadioButton
    Friend WithEvents optConsignmentSale As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radSummary As System.Windows.Forms.RadioButton
    Friend WithEvents radDetail As System.Windows.Forms.RadioButton
    Friend WithEvents chkItemCategory As System.Windows.Forms.CheckBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cboItemName As System.Windows.Forms.ComboBox
    Friend WithEvents ChkItemName As System.Windows.Forms.CheckBox
    Friend WithEvents chkGoldQuality As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQuality As System.Windows.Forms.ComboBox
    Friend WithEvents optConsignBalance As System.Windows.Forms.RadioButton
    Friend WithEvents radSummaryByDate As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBarcodeNo As System.Windows.Forms.TextBox
End Class
