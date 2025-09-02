<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_CashTransactionByCustomer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_CashTransactionByCustomer))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboGoldQ = New System.Windows.Forms.ComboBox()
        Me.chkGoldQly = New System.Windows.Forms.CheckBox()
        Me.grpBytype = New System.Windows.Forms.GroupBox()
        Me.optNotNil = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.optNil = New System.Windows.Forms.RadioButton()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtWSInvoice = New System.Windows.Forms.RadioButton()
        Me.rbtConsignmentSale = New System.Windows.Forms.RadioButton()
        Me.rptAll = New System.Windows.Forms.RadioButton()
        Me.rbtRepairReturn = New System.Windows.Forms.RadioButton()
        Me.rbtSalesVolume = New System.Windows.Forms.RadioButton()
        Me.rbtOrderReturn = New System.Windows.Forms.RadioButton()
        Me.rbtSaleGems = New System.Windows.Forms.RadioButton()
        Me.rbtSaleInvoice = New System.Windows.Forms.RadioButton()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.chkCustomerName = New System.Windows.Forms.CheckBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.rbtLooseDiamond = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        Me.grpBytype.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.cboGoldQ)
        Me.Panel1.Controls.Add(Me.chkGoldQly)
        Me.Panel1.Controls.Add(Me.grpBytype)
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label46)
        Me.Panel1.Controls.Add(Me.chkCustomerName)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.btnCustomer)
        Me.Panel1.Controls.Add(Me.txtCustomer)
        Me.Panel1.Controls.Add(Me.txtAddress)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1108, 187)
        Me.Panel1.TabIndex = 0
        '
        'cboGoldQ
        '
        Me.cboGoldQ.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoldQ.FormattingEnabled = True
        Me.cboGoldQ.Location = New System.Drawing.Point(110, 71)
        Me.cboGoldQ.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.cboGoldQ.Name = "cboGoldQ"
        Me.cboGoldQ.Size = New System.Drawing.Size(216, 25)
        Me.cboGoldQ.TabIndex = 1508
        '
        'chkGoldQly
        '
        Me.chkGoldQly.AutoSize = True
        Me.chkGoldQly.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkGoldQly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQly.Location = New System.Drawing.Point(17, 75)
        Me.chkGoldQly.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkGoldQly.Name = "chkGoldQly"
        Me.chkGoldQly.Size = New System.Drawing.Size(63, 21)
        Me.chkGoldQly.TabIndex = 1507
        Me.chkGoldQly.Text = "ရွှေရည်"
        Me.chkGoldQly.UseVisualStyleBackColor = True
        '
        'grpBytype
        '
        Me.grpBytype.Controls.Add(Me.optNotNil)
        Me.grpBytype.Controls.Add(Me.optAll)
        Me.grpBytype.Controls.Add(Me.optNil)
        Me.grpBytype.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBytype.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpBytype.Location = New System.Drawing.Point(12, 133)
        Me.grpBytype.Name = "grpBytype"
        Me.grpBytype.Size = New System.Drawing.Size(314, 44)
        Me.grpBytype.TabIndex = 1506
        Me.grpBytype.TabStop = False
        '
        'optNotNil
        '
        Me.optNotNil.AutoSize = True
        Me.optNotNil.Location = New System.Drawing.Point(186, 20)
        Me.optNotNil.Name = "optNotNil"
        Me.optNotNil.Size = New System.Drawing.Size(75, 21)
        Me.optNotNil.TabIndex = 2
        Me.optNotNil.Text = "Not Nil"
        Me.optNotNil.UseVisualStyleBackColor = True
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Checked = True
        Me.optAll.Location = New System.Drawing.Point(27, 20)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(69, 21)
        Me.optAll.TabIndex = 0
        Me.optAll.TabStop = True
        Me.optAll.Text = "By All"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'optNil
        '
        Me.optNil.AutoSize = True
        Me.optNil.Location = New System.Drawing.Point(119, 20)
        Me.optNil.Name = "optNil"
        Me.optNil.Size = New System.Drawing.Size(47, 21)
        Me.optNil.TabIndex = 1
        Me.optNil.Text = "Nil"
        Me.optNil.UseVisualStyleBackColor = True
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(17, 106)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1503
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(110, 103)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(216, 27)
        Me.CboLocation.TabIndex = 1504
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtLooseDiamond)
        Me.GroupBox1.Controls.Add(Me.rbtWSInvoice)
        Me.GroupBox1.Controls.Add(Me.rbtConsignmentSale)
        Me.GroupBox1.Controls.Add(Me.rptAll)
        Me.GroupBox1.Controls.Add(Me.rbtRepairReturn)
        Me.GroupBox1.Controls.Add(Me.rbtSalesVolume)
        Me.GroupBox1.Controls.Add(Me.rbtOrderReturn)
        Me.GroupBox1.Controls.Add(Me.rbtSaleGems)
        Me.GroupBox1.Controls.Add(Me.rbtSaleInvoice)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(337, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(687, 93)
        Me.GroupBox1.TabIndex = 1505
        Me.GroupBox1.TabStop = False
        '
        'rbtWSInvoice
        '
        Me.rbtWSInvoice.AutoSize = True
        Me.rbtWSInvoice.Location = New System.Drawing.Point(395, 20)
        Me.rbtWSInvoice.Name = "rbtWSInvoice"
        Me.rbtWSInvoice.Size = New System.Drawing.Size(95, 21)
        Me.rbtWSInvoice.TabIndex = 8
        Me.rbtWSInvoice.Text = "WholeSale"
        Me.rbtWSInvoice.UseVisualStyleBackColor = True
        '
        'rbtConsignmentSale
        '
        Me.rbtConsignmentSale.AutoSize = True
        Me.rbtConsignmentSale.BackColor = System.Drawing.SystemColors.Control
        Me.rbtConsignmentSale.Location = New System.Drawing.Point(395, 61)
        Me.rbtConsignmentSale.Name = "rbtConsignmentSale"
        Me.rbtConsignmentSale.Size = New System.Drawing.Size(137, 21)
        Me.rbtConsignmentSale.TabIndex = 7
        Me.rbtConsignmentSale.Text = "ConsignmentSale"
        Me.rbtConsignmentSale.UseVisualStyleBackColor = False
        '
        'rptAll
        '
        Me.rptAll.AutoSize = True
        Me.rptAll.Checked = True
        Me.rptAll.Location = New System.Drawing.Point(15, 20)
        Me.rptAll.Name = "rptAll"
        Me.rptAll.Size = New System.Drawing.Size(47, 21)
        Me.rptAll.TabIndex = 6
        Me.rptAll.TabStop = True
        Me.rptAll.Text = "All"
        Me.rptAll.UseVisualStyleBackColor = True
        '
        'rbtRepairReturn
        '
        Me.rbtRepairReturn.AutoSize = True
        Me.rbtRepairReturn.Location = New System.Drawing.Point(244, 21)
        Me.rbtRepairReturn.Name = "rbtRepairReturn"
        Me.rbtRepairReturn.Size = New System.Drawing.Size(115, 21)
        Me.rbtRepairReturn.TabIndex = 5
        Me.rbtRepairReturn.Text = "ပြင်ထည်ရွေးခြင်း"
        Me.rbtRepairReturn.UseVisualStyleBackColor = True
        '
        'rbtSalesVolume
        '
        Me.rbtSalesVolume.AutoSize = True
        Me.rbtSalesVolume.BackColor = System.Drawing.SystemColors.Control
        Me.rbtSalesVolume.Location = New System.Drawing.Point(244, 62)
        Me.rbtSalesVolume.Name = "rbtSalesVolume"
        Me.rbtSalesVolume.Size = New System.Drawing.Size(145, 21)
        Me.rbtSalesVolume.TabIndex = 4
        Me.rbtSalesVolume.Text = "ရောင်းခြင်း(Volume)"
        Me.rbtSalesVolume.UseVisualStyleBackColor = False
        '
        'rbtOrderReturn
        '
        Me.rbtOrderReturn.AutoSize = True
        Me.rbtOrderReturn.Location = New System.Drawing.Point(113, 21)
        Me.rbtOrderReturn.Name = "rbtOrderReturn"
        Me.rbtOrderReturn.Size = New System.Drawing.Size(116, 21)
        Me.rbtOrderReturn.TabIndex = 1
        Me.rbtOrderReturn.Text = "အပ်ထည်ရွေးခြင်း"
        Me.rbtOrderReturn.UseVisualStyleBackColor = True
        '
        'rbtSaleGems
        '
        Me.rbtSaleGems.AutoSize = True
        Me.rbtSaleGems.Location = New System.Drawing.Point(113, 61)
        Me.rbtSaleGems.Name = "rbtSaleGems"
        Me.rbtSaleGems.Size = New System.Drawing.Size(89, 21)
        Me.rbtSaleGems.TabIndex = 2
        Me.rbtSaleGems.Text = "SaleOther"
        Me.rbtSaleGems.UseVisualStyleBackColor = True
        '
        'rbtSaleInvoice
        '
        Me.rbtSaleInvoice.AutoSize = True
        Me.rbtSaleInvoice.Location = New System.Drawing.Point(15, 62)
        Me.rbtSaleInvoice.Name = "rbtSaleInvoice"
        Me.rbtSaleInvoice.Size = New System.Drawing.Size(85, 21)
        Me.rbtSaleInvoice.TabIndex = 3
        Me.rbtSaleInvoice.Text = "ရောင်းခြင်း"
        Me.rbtSaleInvoice.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label46.Location = New System.Drawing.Point(355, 134)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(48, 20)
        Me.Label46.TabIndex = 1500
        Me.Label46.Text = "လိပ်စာ"
        '
        'chkCustomerName
        '
        Me.chkCustomerName.AutoSize = True
        Me.chkCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkCustomerName.Location = New System.Drawing.Point(342, 105)
        Me.chkCustomerName.Name = "chkCustomerName"
        Me.chkCustomerName.Size = New System.Drawing.Size(62, 21)
        Me.chkCustomerName.TabIndex = 1501
        Me.chkCustomerName.Text = "ဝယ်သူ"
        Me.chkCustomerName.UseVisualStyleBackColor = True
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Zawgyi-One", 8.25!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(404, 102)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(59, 25)
        Me.txtCustomerCode.TabIndex = 1496
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(636, 102)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(41, 25)
        Me.btnCustomer.TabIndex = 1497
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.White
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCustomer.Location = New System.Drawing.Point(463, 102)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(172, 27)
        Me.txtCustomer.TabIndex = 1499
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtAddress.Location = New System.Drawing.Point(404, 133)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(274, 44)
        Me.txtAddress.TabIndex = 1498
        Me.txtAddress.TabStop = False
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
        Me.grpByDate.Location = New System.Drawing.Point(12, 1)
        Me.grpByDate.Name = "grpByDate"
        Me.grpByDate.Size = New System.Drawing.Size(314, 66)
        Me.grpByDate.TabIndex = 0
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
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.TabStop = False
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(64, 23)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpFromDate.TabIndex = 0
        Me.dtpFromDate.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.59!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(167, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To"
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(701, 141)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 1
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
        Me.btnClose.Location = New System.Drawing.Point(800, 141)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 187)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(1108, 237)
        Me.rptViewer.TabIndex = 1
        Me.rptViewer.TabStop = False
        '
        'rbtLooseDiamond
        '
        Me.rbtLooseDiamond.AutoSize = True
        Me.rbtLooseDiamond.Location = New System.Drawing.Point(528, 21)
        Me.rbtLooseDiamond.Name = "rbtLooseDiamond"
        Me.rbtLooseDiamond.Size = New System.Drawing.Size(153, 21)
        Me.rbtLooseDiamond.TabIndex = 10
        Me.rbtLooseDiamond.Text = "Sale LooseDiamond"
        Me.rbtLooseDiamond.UseVisualStyleBackColor = True
        '
        'frm_rpt_CashTransactionByCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 424)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_rpt_CashTransactionByCustomer"
        Me.Text = "All Transaction"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpBytype.ResumeLayout(False)
        Me.grpBytype.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents chkCustomerName As System.Windows.Forms.CheckBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtWSInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents rbtConsignmentSale As System.Windows.Forms.RadioButton
    Friend WithEvents rptAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbtRepairReturn As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSalesVolume As System.Windows.Forms.RadioButton
    Friend WithEvents rbtOrderReturn As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSaleGems As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSaleInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents grpBytype As System.Windows.Forms.GroupBox
    Friend WithEvents optNotNil As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents optNil As System.Windows.Forms.RadioButton
    Friend WithEvents cboGoldQ As System.Windows.Forms.ComboBox
    Friend WithEvents chkGoldQly As System.Windows.Forms.CheckBox
    Friend WithEvents rbtLooseDiamond As System.Windows.Forms.RadioButton
End Class
