<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_Mortgage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_Mortgage))
        Me.rpt_Mortgage = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpBoxView = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.grpType = New System.Windows.Forms.GroupBox()
        Me.optBalanceStocks = New System.Windows.Forms.RadioButton()
        Me.optAllStocks = New System.Windows.Forms.RadioButton()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.chkCustomerName = New System.Windows.Forms.CheckBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.chkLocation = New System.Windows.Forms.CheckBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.radSummary = New System.Windows.Forms.RadioButton()
        Me.radDetail = New System.Windows.Forms.RadioButton()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkGoldQ = New System.Windows.Forms.CheckBox()
        Me.chkItemCat = New System.Windows.Forms.CheckBox()
        Me.cboGoldQ = New System.Windows.Forms.ComboBox()
        Me.cboItemCat = New System.Windows.Forms.ComboBox()
        Me.grpview = New System.Windows.Forms.GroupBox()
        Me.radHistory = New System.Windows.Forms.RadioButton()
        Me.radAll = New System.Windows.Forms.RadioButton()
        Me.radPayback = New System.Windows.Forms.RadioButton()
        Me.radDisable = New System.Windows.Forms.RadioButton()
        Me.radReceive = New System.Windows.Forms.RadioButton()
        Me.radInterest = New System.Windows.Forms.RadioButton()
        Me.radReturn = New System.Windows.Forms.RadioButton()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkOldReturn = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.grpBoxView.SuspendLayout()
        Me.grpType.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpview.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'rpt_Mortgage
        '
        Me.rpt_Mortgage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rpt_Mortgage.Location = New System.Drawing.Point(0, 180)
        Me.rpt_Mortgage.Name = "rpt_Mortgage"
        Me.rpt_Mortgage.Size = New System.Drawing.Size(1160, 303)
        Me.rpt_Mortgage.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkOldReturn)
        Me.Panel1.Controls.Add(Me.grpBoxView)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.grpType)
        Me.Panel1.Controls.Add(Me.Label46)
        Me.Panel1.Controls.Add(Me.chkCustomerName)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.btnCustomer)
        Me.Panel1.Controls.Add(Me.chkLocation)
        Me.Panel1.Controls.Add(Me.txtCustomer)
        Me.Panel1.Controls.Add(Me.txtAddress)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.btnView)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.grpview)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1160, 180)
        Me.Panel1.TabIndex = 2
        '
        'grpBoxView
        '
        Me.grpBoxView.Controls.Add(Me.RadioButton1)
        Me.grpBoxView.Controls.Add(Me.RadioButton2)
        Me.grpBoxView.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxView.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpBoxView.Location = New System.Drawing.Point(5, 86)
        Me.grpBoxView.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpBoxView.Name = "grpBoxView"
        Me.grpBoxView.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpBoxView.Size = New System.Drawing.Size(294, 46)
        Me.grpBoxView.TabIndex = 1504
        Me.grpBoxView.TabStop = False
        Me.grpBoxView.Text = " View"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(54, 19)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(85, 21)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "စာရင်းချုပ်"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(174, 18)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(85, 21)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "အသေးစိတ်"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(429, 97)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(193, 27)
        Me.CboLocation.TabIndex = 1503
        '
        'grpType
        '
        Me.grpType.BackColor = System.Drawing.SystemColors.Control
        Me.grpType.Controls.Add(Me.optBalanceStocks)
        Me.grpType.Controls.Add(Me.optAllStocks)
        Me.grpType.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpType.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpType.Location = New System.Drawing.Point(690, 96)
        Me.grpType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpType.Name = "grpType"
        Me.grpType.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpType.Size = New System.Drawing.Size(256, 56)
        Me.grpType.TabIndex = 1502
        Me.grpType.TabStop = False
        Me.grpType.Text = "Type"
        '
        'optBalanceStocks
        '
        Me.optBalanceStocks.AutoSize = True
        Me.optBalanceStocks.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optBalanceStocks.Location = New System.Drawing.Point(144, 20)
        Me.optBalanceStocks.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optBalanceStocks.Name = "optBalanceStocks"
        Me.optBalanceStocks.Size = New System.Drawing.Size(76, 21)
        Me.optBalanceStocks.TabIndex = 1
        Me.optBalanceStocks.Text = "လက်ကျန်"
        Me.optBalanceStocks.UseVisualStyleBackColor = True
        '
        'optAllStocks
        '
        Me.optAllStocks.AutoSize = True
        Me.optAllStocks.Checked = True
        Me.optAllStocks.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optAllStocks.Location = New System.Drawing.Point(38, 21)
        Me.optAllStocks.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optAllStocks.Name = "optAllStocks"
        Me.optAllStocks.Size = New System.Drawing.Size(68, 21)
        Me.optAllStocks.TabIndex = 0
        Me.optAllStocks.TabStop = True
        Me.optAllStocks.Text = "အားလုံး"
        Me.optAllStocks.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label46.Location = New System.Drawing.Point(628, 50)
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
        Me.chkCustomerName.Location = New System.Drawing.Point(628, 20)
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
        Me.txtCustomerCode.Location = New System.Drawing.Point(690, 17)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(77, 25)
        Me.txtCustomerCode.TabIndex = 1496
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(946, 17)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(41, 25)
        Me.btnCustomer.TabIndex = 1497
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'chkLocation
        '
        Me.chkLocation.AutoSize = True
        Me.chkLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkLocation.Location = New System.Drawing.Point(341, 103)
        Me.chkLocation.Name = "chkLocation"
        Me.chkLocation.Size = New System.Drawing.Size(75, 17)
        Me.chkLocation.TabIndex = 6
        Me.chkLocation.Text = "Location"
        Me.chkLocation.UseVisualStyleBackColor = True
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.White
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCustomer.Location = New System.Drawing.Point(773, 17)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(172, 27)
        Me.txtCustomer.TabIndex = 1499
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtAddress.Location = New System.Drawing.Point(690, 48)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(274, 44)
        Me.txtAddress.TabIndex = 1498
        Me.txtAddress.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.radSummary)
        Me.GroupBox2.Controls.Add(Me.radDetail)
        Me.GroupBox2.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox2.Location = New System.Drawing.Point(1086, 34)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(55, 42)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "View"
        Me.GroupBox2.Visible = False
        '
        'radSummary
        '
        Me.radSummary.AutoSize = True
        Me.radSummary.Checked = True
        Me.radSummary.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radSummary.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radSummary.Location = New System.Drawing.Point(347, 10)
        Me.radSummary.Name = "radSummary"
        Me.radSummary.Size = New System.Drawing.Size(84, 21)
        Me.radSummary.TabIndex = 0
        Me.radSummary.TabStop = True
        Me.radSummary.Text = "Summary"
        Me.radSummary.UseVisualStyleBackColor = True
        '
        'radDetail
        '
        Me.radDetail.AutoSize = True
        Me.radDetail.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radDetail.Location = New System.Drawing.Point(100, 16)
        Me.radDetail.Name = "radDetail"
        Me.radDetail.Size = New System.Drawing.Size(66, 21)
        Me.radDetail.TabIndex = 1
        Me.radDetail.Text = "Detail"
        Me.radDetail.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnView.ForeColor = System.Drawing.Color.DarkRed
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnView.Location = New System.Drawing.Point(952, 126)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(87, 31)
        Me.btnView.TabIndex = 12
        Me.btnView.Text = "View"
        Me.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(1064, 126)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkGoldQ)
        Me.GroupBox1.Controls.Add(Me.chkItemCat)
        Me.GroupBox1.Controls.Add(Me.cboGoldQ)
        Me.GroupBox1.Controls.Add(Me.cboItemCat)
        Me.GroupBox1.Location = New System.Drawing.Point(325, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(297, 80)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'chkGoldQ
        '
        Me.chkGoldQ.AutoSize = True
        Me.chkGoldQ.BackColor = System.Drawing.Color.Transparent
        Me.chkGoldQ.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkGoldQ.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQ.Location = New System.Drawing.Point(16, 14)
        Me.chkGoldQ.Name = "chkGoldQ"
        Me.chkGoldQ.Size = New System.Drawing.Size(112, 21)
        Me.chkGoldQ.TabIndex = 8
        Me.chkGoldQ.Text = "Gold Quality"
        Me.chkGoldQ.UseVisualStyleBackColor = False
        '
        'chkItemCat
        '
        Me.chkItemCat.AutoSize = True
        Me.chkItemCat.BackColor = System.Drawing.Color.Transparent
        Me.chkItemCat.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkItemCat.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCat.Location = New System.Drawing.Point(16, 46)
        Me.chkItemCat.Name = "chkItemCat"
        Me.chkItemCat.Size = New System.Drawing.Size(117, 21)
        Me.chkItemCat.TabIndex = 10
        Me.chkItemCat.Text = "Item Category"
        Me.chkItemCat.UseVisualStyleBackColor = False
        '
        'cboGoldQ
        '
        Me.cboGoldQ.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.cboGoldQ.FormattingEnabled = True
        Me.cboGoldQ.Location = New System.Drawing.Point(135, 10)
        Me.cboGoldQ.Name = "cboGoldQ"
        Me.cboGoldQ.Size = New System.Drawing.Size(150, 25)
        Me.cboGoldQ.TabIndex = 9
        '
        'cboItemCat
        '
        Me.cboItemCat.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.cboItemCat.FormattingEnabled = True
        Me.cboItemCat.Location = New System.Drawing.Point(136, 42)
        Me.cboItemCat.Name = "cboItemCat"
        Me.cboItemCat.Size = New System.Drawing.Size(150, 25)
        Me.cboItemCat.TabIndex = 11
        '
        'grpview
        '
        Me.grpview.BackColor = System.Drawing.SystemColors.Control
        Me.grpview.Controls.Add(Me.radHistory)
        Me.grpview.Controls.Add(Me.radAll)
        Me.grpview.Controls.Add(Me.radPayback)
        Me.grpview.Controls.Add(Me.radDisable)
        Me.grpview.Controls.Add(Me.radReceive)
        Me.grpview.Controls.Add(Me.radInterest)
        Me.grpview.Controls.Add(Me.radReturn)
        Me.grpview.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.grpview.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpview.Location = New System.Drawing.Point(6, 134)
        Me.grpview.Name = "grpview"
        Me.grpview.Size = New System.Drawing.Size(616, 41)
        Me.grpview.TabIndex = 5
        Me.grpview.TabStop = False
        Me.grpview.Text = "Type"
        '
        'radHistory
        '
        Me.radHistory.AutoSize = True
        Me.radHistory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radHistory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radHistory.Location = New System.Drawing.Point(465, 14)
        Me.radHistory.Name = "radHistory"
        Me.radHistory.Size = New System.Drawing.Size(138, 21)
        Me.radHistory.TabIndex = 15
        Me.radHistory.Text = "Customer History"
        Me.radHistory.UseVisualStyleBackColor = True
        '
        'radAll
        '
        Me.radAll.AutoSize = True
        Me.radAll.Checked = True
        Me.radAll.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radAll.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radAll.Location = New System.Drawing.Point(6, 14)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(47, 21)
        Me.radAll.TabIndex = 14
        Me.radAll.TabStop = True
        Me.radAll.Text = "All"
        Me.radAll.UseVisualStyleBackColor = True
        '
        'radPayback
        '
        Me.radPayback.AutoSize = True
        Me.radPayback.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radPayback.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radPayback.Location = New System.Drawing.Point(144, 14)
        Me.radPayback.Name = "radPayback"
        Me.radPayback.Size = New System.Drawing.Size(79, 21)
        Me.radPayback.TabIndex = 13
        Me.radPayback.Text = "Payback"
        Me.radPayback.UseVisualStyleBackColor = True
        '
        'radDisable
        '
        Me.radDisable.AutoSize = True
        Me.radDisable.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radDisable.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radDisable.Location = New System.Drawing.Point(384, 14)
        Me.radDisable.Name = "radDisable"
        Me.radDisable.Size = New System.Drawing.Size(75, 21)
        Me.radDisable.TabIndex = 12
        Me.radDisable.Text = "Disable"
        Me.radDisable.UseVisualStyleBackColor = True
        '
        'radReceive
        '
        Me.radReceive.AutoSize = True
        Me.radReceive.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radReceive.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radReceive.Location = New System.Drawing.Point(61, 14)
        Me.radReceive.Name = "radReceive"
        Me.radReceive.Size = New System.Drawing.Size(77, 21)
        Me.radReceive.TabIndex = 5
        Me.radReceive.Text = "Receive"
        Me.radReceive.UseVisualStyleBackColor = True
        '
        'radInterest
        '
        Me.radInterest.AutoSize = True
        Me.radInterest.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radInterest.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radInterest.Location = New System.Drawing.Point(229, 14)
        Me.radInterest.Name = "radInterest"
        Me.radInterest.Size = New System.Drawing.Size(74, 21)
        Me.radInterest.TabIndex = 10
        Me.radInterest.Text = "Interest"
        Me.radInterest.UseVisualStyleBackColor = True
        '
        'radReturn
        '
        Me.radReturn.AutoSize = True
        Me.radReturn.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radReturn.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radReturn.Location = New System.Drawing.Point(309, 14)
        Me.radReturn.Name = "radReturn"
        Me.radReturn.Size = New System.Drawing.Size(69, 21)
        Me.radReturn.TabIndex = 11
        Me.radReturn.Text = "Return"
        Me.radReturn.UseVisualStyleBackColor = True
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
        Me.grpByDate.Location = New System.Drawing.Point(5, 13)
        Me.grpByDate.Name = "grpByDate"
        Me.grpByDate.Size = New System.Drawing.Size(294, 68)
        Me.grpByDate.TabIndex = 2
        Me.grpByDate.TabStop = False
        Me.grpByDate.Text = "Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(188, 17)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(83, 21)
        Me.dtpToDate.TabIndex = 4
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(56, 17)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(83, 21)
        Me.dtpFromDate.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(152, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "To"
        '
        'chkOldReturn
        '
        Me.chkOldReturn.AutoSize = True
        Me.chkOldReturn.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOldReturn.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkOldReturn.Location = New System.Drawing.Point(632, 154)
        Me.chkOldReturn.Name = "chkOldReturn"
        Me.chkOldReturn.Size = New System.Drawing.Size(192, 21)
        Me.chkOldReturn.TabIndex = 1505
        Me.chkOldReturn.Text = "အဟောင်းဘောင်ချာ ကြည့်မည်။"
        Me.chkOldReturn.UseVisualStyleBackColor = True
        '
        'frm_rpt_Mortgage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1160, 483)
        Me.Controls.Add(Me.rpt_Mortgage)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_rpt_Mortgage"
        Me.Text = "Mortgage Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpBoxView.ResumeLayout(False)
        Me.grpBoxView.PerformLayout()
        Me.grpType.ResumeLayout(False)
        Me.grpType.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpview.ResumeLayout(False)
        Me.grpview.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rpt_Mortgage As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpview As System.Windows.Forms.GroupBox
    Friend WithEvents radReceive As System.Windows.Forms.RadioButton
    Friend WithEvents radInterest As System.Windows.Forms.RadioButton
    Friend WithEvents radReturn As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkGoldQ As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemCat As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQ As System.Windows.Forms.ComboBox
    Friend WithEvents cboItemCat As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents radSummary As System.Windows.Forms.RadioButton
    Friend WithEvents radDetail As System.Windows.Forms.RadioButton
    Friend WithEvents radDisable As System.Windows.Forms.RadioButton
    Friend WithEvents radPayback As System.Windows.Forms.RadioButton
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents chkCustomerName As System.Windows.Forms.CheckBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents radHistory As System.Windows.Forms.RadioButton
    Friend WithEvents grpType As System.Windows.Forms.GroupBox
    Friend WithEvents optBalanceStocks As System.Windows.Forms.RadioButton
    Friend WithEvents optAllStocks As System.Windows.Forms.RadioButton
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents grpBoxView As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents chkOldReturn As System.Windows.Forms.CheckBox
End Class
