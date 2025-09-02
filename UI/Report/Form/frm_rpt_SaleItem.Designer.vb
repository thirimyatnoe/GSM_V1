<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_SaleItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_SaleItem))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTG = New System.Windows.Forms.TextBox()
        Me.txtFG = New System.Windows.Forms.TextBox()
        Me.chkGram = New System.Windows.Forms.CheckBox()
        Me.grpSType = New System.Windows.Forms.GroupBox()
        Me.radSExit = New System.Windows.Forms.RadioButton()
        Me.radSBalance = New System.Windows.Forms.RadioButton()
        Me.radSAll = New System.Windows.Forms.RadioButton()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.chkInactiveStock = New System.Windows.Forms.CheckBox()
        Me.chkGoldSmith = New System.Windows.Forms.CheckBox()
        Me.chkShopItem = New System.Windows.Forms.CheckBox()
        Me.chkIsFix = New System.Windows.Forms.CheckBox()
        Me.chkIsOrder = New System.Windows.Forms.CheckBox()
        Me.chkIsClosed = New System.Windows.Forms.CheckBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.radDailyStock = New System.Windows.Forms.RadioButton()
        Me.grpBoxView = New System.Windows.Forms.GroupBox()
        Me.radByGoldQuality = New System.Windows.Forms.RadioButton()
        Me.radOverView = New System.Windows.Forms.RadioButton()
        Me.radSummary = New System.Windows.Forms.RadioButton()
        Me.radDetail = New System.Windows.Forms.RadioButton()
        Me.grpStockType = New System.Windows.Forms.GroupBox()
        Me.radPlatinum = New System.Windows.Forms.RadioButton()
        Me.radAll = New System.Windows.Forms.RadioButton()
        Me.rbtnDiamondStock = New System.Windows.Forms.RadioButton()
        Me.radStock = New System.Windows.Forms.RadioButton()
        Me.radVolume = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpType = New System.Windows.Forms.GroupBox()
        Me.optByGivenDate = New System.Windows.Forms.RadioButton()
        Me.optBalanceStocks = New System.Windows.Forms.RadioButton()
        Me.optAllStocks = New System.Windows.Forms.RadioButton()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnCounter = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpBoxDate = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cboGoldSmith = New System.Windows.Forms.ComboBox()
        Me.chkByLocation = New System.Windows.Forms.CheckBox()
        Me.chkGS = New System.Windows.Forms.CheckBox()
        Me.txtOriginalCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.cboSupplier = New System.Windows.Forms.ComboBox()
        Me.lblBarcodeNo = New System.Windows.Forms.Label()
        Me.chkSupplier = New System.Windows.Forms.CheckBox()
        Me.txtBarcodeNo = New System.Windows.Forms.TextBox()
        Me.lblGoldSmith = New System.Windows.Forms.Label()
        Me.txtGoldSmith = New System.Windows.Forms.TextBox()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.chkStaff = New System.Windows.Forms.CheckBox()
        Me.cboItemName = New System.Windows.Forms.ComboBox()
        Me.chkItemName = New System.Windows.Forms.CheckBox()
        Me.cboItemCat = New System.Windows.Forms.ComboBox()
        Me.chkItemCat = New System.Windows.Forms.CheckBox()
        Me.LBCounter = New System.Windows.Forms.ListBox()
        Me.cboGoldQ = New System.Windows.Forms.ComboBox()
        Me.chkGoldQly = New System.Windows.Forms.CheckBox()
        Me.RptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.radLooseDiamond = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        Me.grpSType.SuspendLayout()
        Me.grpBoxView.SuspendLayout()
        Me.grpStockType.SuspendLayout()
        Me.grpType.SuspendLayout()
        Me.grpBoxDate.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtTG)
        Me.Panel1.Controls.Add(Me.txtFG)
        Me.Panel1.Controls.Add(Me.chkGram)
        Me.Panel1.Controls.Add(Me.grpSType)
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.chkInactiveStock)
        Me.Panel1.Controls.Add(Me.chkGoldSmith)
        Me.Panel1.Controls.Add(Me.chkShopItem)
        Me.Panel1.Controls.Add(Me.chkIsFix)
        Me.Panel1.Controls.Add(Me.chkIsOrder)
        Me.Panel1.Controls.Add(Me.chkIsClosed)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.radDailyStock)
        Me.Panel1.Controls.Add(Me.grpBoxView)
        Me.Panel1.Controls.Add(Me.grpStockType)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.grpType)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnCounter)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.grpBoxDate)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1367, 269)
        Me.Panel1.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label8.Location = New System.Drawing.Point(214, 199)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 15)
        Me.Label8.TabIndex = 1506
        Me.Label8.Text = "To"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(111, 199)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 15)
        Me.Label5.TabIndex = 1484
        Me.Label5.Text = "From"
        '
        'txtTG
        '
        Me.txtTG.BackColor = System.Drawing.Color.White
        Me.txtTG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTG.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtTG.Location = New System.Drawing.Point(187, 217)
        Me.txtTG.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtTG.Name = "txtTG"
        Me.txtTG.Size = New System.Drawing.Size(81, 26)
        Me.txtTG.TabIndex = 1505
        Me.txtTG.Text = " "
        '
        'txtFG
        '
        Me.txtFG.BackColor = System.Drawing.Color.White
        Me.txtFG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFG.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtFG.Location = New System.Drawing.Point(93, 217)
        Me.txtFG.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtFG.Name = "txtFG"
        Me.txtFG.Size = New System.Drawing.Size(78, 26)
        Me.txtFG.TabIndex = 1484
        Me.txtFG.Text = " "
        '
        'chkGram
        '
        Me.chkGram.AutoSize = True
        Me.chkGram.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkGram.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGram.Location = New System.Drawing.Point(17, 200)
        Me.chkGram.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkGram.Name = "chkGram"
        Me.chkGram.Size = New System.Drawing.Size(61, 21)
        Me.chkGram.TabIndex = 1504
        Me.chkGram.Text = "Gram"
        Me.chkGram.UseVisualStyleBackColor = True
        '
        'grpSType
        '
        Me.grpSType.BackColor = System.Drawing.SystemColors.Control
        Me.grpSType.Controls.Add(Me.radSExit)
        Me.grpSType.Controls.Add(Me.radSBalance)
        Me.grpSType.Controls.Add(Me.radSAll)
        Me.grpSType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSType.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpSType.Location = New System.Drawing.Point(861, 96)
        Me.grpSType.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpSType.Name = "grpSType"
        Me.grpSType.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpSType.Size = New System.Drawing.Size(229, 69)
        Me.grpSType.TabIndex = 1503
        Me.grpSType.TabStop = False
        Me.grpSType.Text = "Type"
        '
        'radSExit
        '
        Me.radSExit.AutoSize = True
        Me.radSExit.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radSExit.Location = New System.Drawing.Point(150, 24)
        Me.radSExit.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radSExit.Name = "radSExit"
        Me.radSExit.Size = New System.Drawing.Size(77, 21)
        Me.radSExit.TabIndex = 2
        Me.radSExit.Text = "ရောင်းပြီး"
        Me.radSExit.UseVisualStyleBackColor = True
        '
        'radSBalance
        '
        Me.radSBalance.AutoSize = True
        Me.radSBalance.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radSBalance.Location = New System.Drawing.Point(76, 24)
        Me.radSBalance.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radSBalance.Name = "radSBalance"
        Me.radSBalance.Size = New System.Drawing.Size(76, 21)
        Me.radSBalance.TabIndex = 1
        Me.radSBalance.Text = "လက်ကျန်"
        Me.radSBalance.UseVisualStyleBackColor = True
        '
        'radSAll
        '
        Me.radSAll.AutoSize = True
        Me.radSAll.Checked = True
        Me.radSAll.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radSAll.Location = New System.Drawing.Point(7, 24)
        Me.radSAll.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radSAll.Name = "radSAll"
        Me.radSAll.Size = New System.Drawing.Size(68, 21)
        Me.radSAll.TabIndex = 0
        Me.radSAll.TabStop = True
        Me.radSAll.Text = "အားလုံး"
        Me.radSAll.UseVisualStyleBackColor = True
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(840, 34)
        Me.ChkLocation.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1481
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(930, 29)
        Me.CboLocation.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(174, 25)
        Me.CboLocation.TabIndex = 1482
        '
        'chkInactiveStock
        '
        Me.chkInactiveStock.AutoSize = True
        Me.chkInactiveStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactiveStock.ForeColor = System.Drawing.Color.Blue
        Me.chkInactiveStock.Location = New System.Drawing.Point(1142, 126)
        Me.chkInactiveStock.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkInactiveStock.Name = "chkInactiveStock"
        Me.chkInactiveStock.Size = New System.Drawing.Size(114, 19)
        Me.chkInactiveStock.TabIndex = 1480
        Me.chkInactiveStock.Text = "Inactive Stock"
        Me.chkInactiveStock.UseVisualStyleBackColor = True
        '
        'chkGoldSmith
        '
        Me.chkGoldSmith.AutoSize = True
        Me.chkGoldSmith.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkGoldSmith.ForeColor = System.Drawing.Color.Blue
        Me.chkGoldSmith.Location = New System.Drawing.Point(1241, 26)
        Me.chkGoldSmith.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkGoldSmith.Name = "chkGoldSmith"
        Me.chkGoldSmith.Size = New System.Drawing.Size(116, 21)
        Me.chkGoldSmith.TabIndex = 1476
        Me.chkGoldSmith.Text = "အပ်ရောင်းပစ္စည်း"
        Me.chkGoldSmith.UseVisualStyleBackColor = True
        '
        'chkShopItem
        '
        Me.chkShopItem.AutoSize = True
        Me.chkShopItem.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkShopItem.ForeColor = System.Drawing.Color.Blue
        Me.chkShopItem.Location = New System.Drawing.Point(1142, 63)
        Me.chkShopItem.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkShopItem.Name = "chkShopItem"
        Me.chkShopItem.Size = New System.Drawing.Size(82, 21)
        Me.chkShopItem.TabIndex = 1475
        Me.chkShopItem.Text = "ဆိုင်ပစ္စည်း"
        Me.chkShopItem.UseVisualStyleBackColor = True
        '
        'chkIsFix
        '
        Me.chkIsFix.AutoSize = True
        Me.chkIsFix.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkIsFix.ForeColor = System.Drawing.Color.Blue
        Me.chkIsFix.Location = New System.Drawing.Point(1142, 26)
        Me.chkIsFix.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkIsFix.Name = "chkIsFix"
        Me.chkIsFix.Size = New System.Drawing.Size(74, 21)
        Me.chkIsFix.TabIndex = 1477
        Me.chkIsFix.Text = "ဒုံးပစ္စည်း"
        Me.chkIsFix.UseVisualStyleBackColor = True
        '
        'chkIsOrder
        '
        Me.chkIsOrder.AutoSize = True
        Me.chkIsOrder.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkIsOrder.ForeColor = System.Drawing.Color.Blue
        Me.chkIsOrder.Location = New System.Drawing.Point(1241, 62)
        Me.chkIsOrder.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkIsOrder.Name = "chkIsOrder"
        Me.chkIsOrder.Size = New System.Drawing.Size(99, 21)
        Me.chkIsOrder.TabIndex = 1478
        Me.chkIsOrder.Text = "အော်ဒါပစ္စည်း"
        Me.chkIsOrder.UseVisualStyleBackColor = True
        '
        'chkIsClosed
        '
        Me.chkIsClosed.AutoSize = True
        Me.chkIsClosed.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkIsClosed.ForeColor = System.Drawing.Color.Blue
        Me.chkIsClosed.Location = New System.Drawing.Point(1142, 92)
        Me.chkIsClosed.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkIsClosed.Name = "chkIsClosed"
        Me.chkIsClosed.Size = New System.Drawing.Size(75, 21)
        Me.chkIsClosed.TabIndex = 1479
        Me.chkIsClosed.Text = "ပိတ်သိမ်း"
        Me.chkIsClosed.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1320, 100)
        Me.btnHelpbook.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(37, 42)
        Me.btnHelpbook.TabIndex = 1471
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'radDailyStock
        '
        Me.radDailyStock.AutoSize = True
        Me.radDailyStock.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.radDailyStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radDailyStock.Location = New System.Drawing.Point(1144, 238)
        Me.radDailyStock.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radDailyStock.Name = "radDailyStock"
        Me.radDailyStock.Size = New System.Drawing.Size(153, 17)
        Me.radDailyStock.TabIndex = 3
        Me.radDailyStock.Text = "DailyStockTransaction"
        Me.radDailyStock.UseVisualStyleBackColor = True
        Me.radDailyStock.Visible = False
        '
        'grpBoxView
        '
        Me.grpBoxView.Controls.Add(Me.radByGoldQuality)
        Me.grpBoxView.Controls.Add(Me.radOverView)
        Me.grpBoxView.Controls.Add(Me.radSummary)
        Me.grpBoxView.Controls.Add(Me.radDetail)
        Me.grpBoxView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxView.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpBoxView.Location = New System.Drawing.Point(6, 63)
        Me.grpBoxView.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpBoxView.Name = "grpBoxView"
        Me.grpBoxView.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpBoxView.Size = New System.Drawing.Size(394, 59)
        Me.grpBoxView.TabIndex = 3
        Me.grpBoxView.TabStop = False
        Me.grpBoxView.Text = " View"
        '
        'radByGoldQuality
        '
        Me.radByGoldQuality.AutoSize = True
        Me.radByGoldQuality.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radByGoldQuality.Location = New System.Drawing.Point(265, 23)
        Me.radByGoldQuality.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radByGoldQuality.Name = "radByGoldQuality"
        Me.radByGoldQuality.Size = New System.Drawing.Size(125, 21)
        Me.radByGoldQuality.TabIndex = 4
        Me.radByGoldQuality.Text = "ByGoldQuality"
        Me.radByGoldQuality.UseVisualStyleBackColor = True
        '
        'radOverView
        '
        Me.radOverView.AutoSize = True
        Me.radOverView.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radOverView.Location = New System.Drawing.Point(174, 23)
        Me.radOverView.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radOverView.Name = "radOverView"
        Me.radOverView.Size = New System.Drawing.Size(91, 21)
        Me.radOverView.TabIndex = 3
        Me.radOverView.Text = "OverView"
        Me.radOverView.UseVisualStyleBackColor = True
        '
        'radSummary
        '
        Me.radSummary.AutoSize = True
        Me.radSummary.Checked = True
        Me.radSummary.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radSummary.Location = New System.Drawing.Point(6, 23)
        Me.radSummary.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
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
        Me.radDetail.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radDetail.Location = New System.Drawing.Point(91, 24)
        Me.radDetail.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radDetail.Name = "radDetail"
        Me.radDetail.Size = New System.Drawing.Size(85, 21)
        Me.radDetail.TabIndex = 1
        Me.radDetail.Text = "အသေးစိတ်"
        Me.radDetail.UseVisualStyleBackColor = True
        '
        'grpStockType
        '
        Me.grpStockType.Controls.Add(Me.radLooseDiamond)
        Me.grpStockType.Controls.Add(Me.radPlatinum)
        Me.grpStockType.Controls.Add(Me.radAll)
        Me.grpStockType.Controls.Add(Me.rbtnDiamondStock)
        Me.grpStockType.Controls.Add(Me.radStock)
        Me.grpStockType.Controls.Add(Me.radVolume)
        Me.grpStockType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpStockType.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpStockType.Location = New System.Drawing.Point(6, 10)
        Me.grpStockType.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpStockType.Name = "grpStockType"
        Me.grpStockType.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpStockType.Size = New System.Drawing.Size(499, 54)
        Me.grpStockType.TabIndex = 0
        Me.grpStockType.TabStop = False
        Me.grpStockType.Text = " Stock Type"
        '
        'radPlatinum
        '
        Me.radPlatinum.AutoSize = True
        Me.radPlatinum.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radPlatinum.Location = New System.Drawing.Point(203, 24)
        Me.radPlatinum.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radPlatinum.Name = "radPlatinum"
        Me.radPlatinum.Size = New System.Drawing.Size(90, 21)
        Me.radPlatinum.TabIndex = 4
        Me.radPlatinum.Text = "ပလက်တီနမ်"
        Me.radPlatinum.UseVisualStyleBackColor = True
        '
        'radAll
        '
        Me.radAll.AutoSize = True
        Me.radAll.Checked = True
        Me.radAll.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radAll.Location = New System.Drawing.Point(10, 23)
        Me.radAll.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(47, 21)
        Me.radAll.TabIndex = 3
        Me.radAll.TabStop = True
        Me.radAll.Text = "All"
        Me.radAll.UseVisualStyleBackColor = True
        '
        'rbtnDiamondStock
        '
        Me.rbtnDiamondStock.AutoSize = True
        Me.rbtnDiamondStock.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.rbtnDiamondStock.Location = New System.Drawing.Point(129, 24)
        Me.rbtnDiamondStock.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.rbtnDiamondStock.Name = "rbtnDiamondStock"
        Me.rbtnDiamondStock.Size = New System.Drawing.Size(68, 21)
        Me.rbtnDiamondStock.TabIndex = 2
        Me.rbtnDiamondStock.Text = "စိန်ထည်"
        Me.rbtnDiamondStock.UseVisualStyleBackColor = True
        '
        'radStock
        '
        Me.radStock.AutoSize = True
        Me.radStock.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radStock.Location = New System.Drawing.Point(63, 24)
        Me.radStock.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radStock.Name = "radStock"
        Me.radStock.Size = New System.Drawing.Size(67, 21)
        Me.radStock.TabIndex = 0
        Me.radStock.Text = "ရွှေထည်"
        Me.radStock.UseVisualStyleBackColor = True
        '
        'radVolume
        '
        Me.radVolume.AutoSize = True
        Me.radVolume.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radVolume.Location = New System.Drawing.Point(299, 24)
        Me.radVolume.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radVolume.Name = "radVolume"
        Me.radVolume.Size = New System.Drawing.Size(76, 21)
        Me.radVolume.TabIndex = 1
        Me.radVolume.Text = "Volume"
        Me.radVolume.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label2.Location = New System.Drawing.Point(1554, 257)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 200
        Me.Label2.Text = "Counter"
        Me.Label2.Visible = False
        '
        'grpType
        '
        Me.grpType.BackColor = System.Drawing.SystemColors.Control
        Me.grpType.Controls.Add(Me.optByGivenDate)
        Me.grpType.Controls.Add(Me.optBalanceStocks)
        Me.grpType.Controls.Add(Me.optAllStocks)
        Me.grpType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpType.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpType.Location = New System.Drawing.Point(511, 14)
        Me.grpType.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpType.Name = "grpType"
        Me.grpType.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpType.Size = New System.Drawing.Size(320, 69)
        Me.grpType.TabIndex = 1
        Me.grpType.TabStop = False
        Me.grpType.Text = "Type"
        '
        'optByGivenDate
        '
        Me.optByGivenDate.AutoSize = True
        Me.optByGivenDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optByGivenDate.Location = New System.Drawing.Point(170, 24)
        Me.optByGivenDate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.optByGivenDate.Name = "optByGivenDate"
        Me.optByGivenDate.Size = New System.Drawing.Size(151, 21)
        Me.optByGivenDate.TabIndex = 2
        Me.optByGivenDate.Text = "စာရင်းသွင်းနေစွဲ့အလိုက်"
        Me.optByGivenDate.UseVisualStyleBackColor = True
        '
        'optBalanceStocks
        '
        Me.optBalanceStocks.AutoSize = True
        Me.optBalanceStocks.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optBalanceStocks.Location = New System.Drawing.Point(92, 25)
        Me.optBalanceStocks.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
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
        Me.optAllStocks.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.optAllStocks.Location = New System.Drawing.Point(22, 26)
        Me.optAllStocks.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.optAllStocks.Name = "optAllStocks"
        Me.optAllStocks.Size = New System.Drawing.Size(68, 21)
        Me.optAllStocks.TabIndex = 0
        Me.optAllStocks.TabStop = True
        Me.optAllStocks.Text = "အားလုံး"
        Me.optAllStocks.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(1142, 172)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(73, 31)
        Me.btnPreview.TabIndex = 5
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnCounter
        '
        Me.btnCounter.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.btnCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCounter.Image = CType(resources.GetObject("btnCounter.Image"), System.Drawing.Image)
        Me.btnCounter.Location = New System.Drawing.Point(1175, 215)
        Me.btnCounter.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnCounter.Name = "btnCounter"
        Me.btnCounter.Size = New System.Drawing.Size(34, 41)
        Me.btnCounter.TabIndex = 2
        Me.btnCounter.UseVisualStyleBackColor = True
        Me.btnCounter.Visible = False
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(1241, 172)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(64, 30)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpBoxDate
        '
        Me.grpBoxDate.Controls.Add(Me.Label7)
        Me.grpBoxDate.Controls.Add(Me.Label6)
        Me.grpBoxDate.Controls.Add(Me.dtpToDate)
        Me.grpBoxDate.Controls.Add(Me.dtpFromDate)
        Me.grpBoxDate.Controls.Add(Me.Label1)
        Me.grpBoxDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.grpBoxDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpBoxDate.Location = New System.Drawing.Point(6, 132)
        Me.grpBoxDate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpBoxDate.Name = "grpBoxDate"
        Me.grpBoxDate.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpBoxDate.Size = New System.Drawing.Size(325, 58)
        Me.grpBoxDate.TabIndex = 2
        Me.grpBoxDate.TabStop = False
        Me.grpBoxDate.Text = "နေ့စွဲ"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(155, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "အထိ"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(8, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 17)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "မှ"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(196, 22)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(116, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(31, 22)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(116, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(36, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 15)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "From"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cboGoldSmith)
        Me.GroupBox3.Controls.Add(Me.chkByLocation)
        Me.GroupBox3.Controls.Add(Me.chkGS)
        Me.GroupBox3.Controls.Add(Me.txtOriginalCode)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtVoucherNo)
        Me.GroupBox3.Controls.Add(Me.cboSupplier)
        Me.GroupBox3.Controls.Add(Me.lblBarcodeNo)
        Me.GroupBox3.Controls.Add(Me.chkSupplier)
        Me.GroupBox3.Controls.Add(Me.txtBarcodeNo)
        Me.GroupBox3.Controls.Add(Me.lblGoldSmith)
        Me.GroupBox3.Controls.Add(Me.txtGoldSmith)
        Me.GroupBox3.Controls.Add(Me.cboStaff)
        Me.GroupBox3.Controls.Add(Me.chkStaff)
        Me.GroupBox3.Controls.Add(Me.cboItemName)
        Me.GroupBox3.Controls.Add(Me.chkItemName)
        Me.GroupBox3.Controls.Add(Me.cboItemCat)
        Me.GroupBox3.Controls.Add(Me.chkItemCat)
        Me.GroupBox3.Controls.Add(Me.LBCounter)
        Me.GroupBox3.Controls.Add(Me.cboGoldQ)
        Me.GroupBox3.Controls.Add(Me.chkGoldQly)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.9!, System.Drawing.FontStyle.Bold)
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox3.Location = New System.Drawing.Point(435, 79)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox3.Size = New System.Drawing.Size(704, 191)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select By"
        '
        'cboGoldSmith
        '
        Me.cboGoldSmith.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.cboGoldSmith.FormattingEnabled = True
        Me.cboGoldSmith.Location = New System.Drawing.Point(100, 151)
        Me.cboGoldSmith.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.cboGoldSmith.Name = "cboGoldSmith"
        Me.cboGoldSmith.Size = New System.Drawing.Size(116, 25)
        Me.cboGoldSmith.TabIndex = 911
        '
        'chkByLocation
        '
        Me.chkByLocation.AutoSize = True
        Me.chkByLocation.BackColor = System.Drawing.Color.Transparent
        Me.chkByLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkByLocation.ForeColor = System.Drawing.Color.Red
        Me.chkByLocation.Location = New System.Drawing.Point(562, 86)
        Me.chkByLocation.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkByLocation.Name = "chkByLocation"
        Me.chkByLocation.Size = New System.Drawing.Size(136, 17)
        Me.chkByLocation.TabIndex = 1483
        Me.chkByLocation.Text = "Transfer To Branch"
        Me.chkByLocation.UseVisualStyleBackColor = False
        Me.chkByLocation.Visible = False
        '
        'chkGS
        '
        Me.chkGS.AutoSize = True
        Me.chkGS.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkGS.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGS.Location = New System.Drawing.Point(7, 155)
        Me.chkGS.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkGS.Name = "chkGS"
        Me.chkGS.Size = New System.Drawing.Size(97, 21)
        Me.chkGS.TabIndex = 910
        Me.chkGS.Text = "ပန်းထိမ်ဆရာ"
        Me.chkGS.UseVisualStyleBackColor = True
        '
        'txtOriginalCode
        '
        Me.txtOriginalCode.BackColor = System.Drawing.Color.White
        Me.txtOriginalCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalCode.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtOriginalCode.Location = New System.Drawing.Point(580, 113)
        Me.txtOriginalCode.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtOriginalCode.Name = "txtOriginalCode"
        Me.txtOriginalCode.Size = New System.Drawing.Size(118, 26)
        Me.txtOriginalCode.TabIndex = 909
        Me.txtOriginalCode.Text = " "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(494, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 15)
        Me.Label4.TabIndex = 908
        Me.Label4.Text = "O_Code"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(493, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 15)
        Me.Label3.TabIndex = 907
        Me.Label3.Text = "Voucher No"
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.BackColor = System.Drawing.Color.White
        Me.txtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVoucherNo.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtVoucherNo.Location = New System.Drawing.Point(580, 151)
        Me.txtVoucherNo.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.Size = New System.Drawing.Size(118, 26)
        Me.txtVoucherNo.TabIndex = 906
        Me.txtVoucherNo.Text = " "
        '
        'cboSupplier
        '
        Me.cboSupplier.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.cboSupplier.FormattingEnabled = True
        Me.cboSupplier.Location = New System.Drawing.Point(308, 63)
        Me.cboSupplier.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.cboSupplier.Name = "cboSupplier"
        Me.cboSupplier.Size = New System.Drawing.Size(108, 25)
        Me.cboSupplier.TabIndex = 905
        '
        'lblBarcodeNo
        '
        Me.lblBarcodeNo.AutoSize = True
        Me.lblBarcodeNo.BackColor = System.Drawing.Color.Transparent
        Me.lblBarcodeNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblBarcodeNo.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblBarcodeNo.Location = New System.Drawing.Point(224, 112)
        Me.lblBarcodeNo.Name = "lblBarcodeNo"
        Me.lblBarcodeNo.Size = New System.Drawing.Size(78, 15)
        Me.lblBarcodeNo.TabIndex = 903
        Me.lblBarcodeNo.Text = "BarcodeNo"
        '
        'chkSupplier
        '
        Me.chkSupplier.AutoSize = True
        Me.chkSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSupplier.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkSupplier.Location = New System.Drawing.Point(225, 70)
        Me.chkSupplier.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkSupplier.Name = "chkSupplier"
        Me.chkSupplier.Size = New System.Drawing.Size(80, 19)
        Me.chkSupplier.TabIndex = 904
        Me.chkSupplier.Text = "Supplier"
        Me.chkSupplier.UseVisualStyleBackColor = True
        '
        'txtBarcodeNo
        '
        Me.txtBarcodeNo.BackColor = System.Drawing.Color.White
        Me.txtBarcodeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcodeNo.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtBarcodeNo.Location = New System.Drawing.Point(308, 111)
        Me.txtBarcodeNo.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtBarcodeNo.Name = "txtBarcodeNo"
        Me.txtBarcodeNo.Size = New System.Drawing.Size(108, 24)
        Me.txtBarcodeNo.TabIndex = 902
        Me.txtBarcodeNo.Text = " "
        '
        'lblGoldSmith
        '
        Me.lblGoldSmith.AutoSize = True
        Me.lblGoldSmith.BackColor = System.Drawing.Color.Transparent
        Me.lblGoldSmith.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblGoldSmith.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblGoldSmith.Location = New System.Drawing.Point(222, 155)
        Me.lblGoldSmith.Name = "lblGoldSmith"
        Me.lblGoldSmith.Size = New System.Drawing.Size(137, 16)
        Me.lblGoldSmith.TabIndex = 901
        Me.lblGoldSmith.Text = "GoldSmith Remark"
        '
        'txtGoldSmith
        '
        Me.txtGoldSmith.BackColor = System.Drawing.Color.White
        Me.txtGoldSmith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldSmith.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtGoldSmith.Location = New System.Drawing.Point(365, 150)
        Me.txtGoldSmith.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtGoldSmith.Name = "txtGoldSmith"
        Me.txtGoldSmith.Size = New System.Drawing.Size(124, 26)
        Me.txtGoldSmith.TabIndex = 794
        Me.txtGoldSmith.Text = " "
        '
        'cboStaff
        '
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(308, 25)
        Me.cboStaff.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(108, 25)
        Me.cboStaff.TabIndex = 7
        '
        'chkStaff
        '
        Me.chkStaff.AutoSize = True
        Me.chkStaff.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkStaff.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkStaff.Location = New System.Drawing.Point(225, 26)
        Me.chkStaff.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkStaff.Name = "chkStaff"
        Me.chkStaff.Size = New System.Drawing.Size(69, 21)
        Me.chkStaff.TabIndex = 6
        Me.chkStaff.Text = "ဝန်ထမ်း"
        Me.chkStaff.UseVisualStyleBackColor = True
        '
        'cboItemName
        '
        Me.cboItemName.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.cboItemName.FormattingEnabled = True
        Me.cboItemName.Location = New System.Drawing.Point(99, 112)
        Me.cboItemName.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.cboItemName.Name = "cboItemName"
        Me.cboItemName.Size = New System.Drawing.Size(117, 25)
        Me.cboItemName.TabIndex = 5
        '
        'chkItemName
        '
        Me.chkItemName.AutoSize = True
        Me.chkItemName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkItemName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemName.Location = New System.Drawing.Point(7, 117)
        Me.chkItemName.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkItemName.Name = "chkItemName"
        Me.chkItemName.Size = New System.Drawing.Size(96, 21)
        Me.chkItemName.TabIndex = 4
        Me.chkItemName.Text = "အမျိုးအမည် "
        Me.chkItemName.UseVisualStyleBackColor = True
        '
        'cboItemCat
        '
        Me.cboItemCat.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.cboItemCat.FormattingEnabled = True
        Me.cboItemCat.Location = New System.Drawing.Point(98, 66)
        Me.cboItemCat.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.cboItemCat.Name = "cboItemCat"
        Me.cboItemCat.Size = New System.Drawing.Size(118, 25)
        Me.cboItemCat.TabIndex = 3
        '
        'chkItemCat
        '
        Me.chkItemCat.AutoSize = True
        Me.chkItemCat.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkItemCat.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCat.Location = New System.Drawing.Point(7, 70)
        Me.chkItemCat.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkItemCat.Name = "chkItemCat"
        Me.chkItemCat.Size = New System.Drawing.Size(91, 21)
        Me.chkItemCat.TabIndex = 2
        Me.chkItemCat.Text = "အမျိုးအစား"
        Me.chkItemCat.UseVisualStyleBackColor = True
        '
        'LBCounter
        '
        Me.LBCounter.FormattingEnabled = True
        Me.LBCounter.ItemHeight = 15
        Me.LBCounter.Location = New System.Drawing.Point(1003, 142)
        Me.LBCounter.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.LBCounter.Name = "LBCounter"
        Me.LBCounter.Size = New System.Drawing.Size(83, 34)
        Me.LBCounter.TabIndex = 2
        Me.LBCounter.Visible = False
        '
        'cboGoldQ
        '
        Me.cboGoldQ.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoldQ.FormattingEnabled = True
        Me.cboGoldQ.Location = New System.Drawing.Point(96, 21)
        Me.cboGoldQ.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.cboGoldQ.Name = "cboGoldQ"
        Me.cboGoldQ.Size = New System.Drawing.Size(120, 25)
        Me.cboGoldQ.TabIndex = 1
        '
        'chkGoldQly
        '
        Me.chkGoldQly.AutoSize = True
        Me.chkGoldQly.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkGoldQly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQly.Location = New System.Drawing.Point(7, 25)
        Me.chkGoldQly.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chkGoldQly.Name = "chkGoldQly"
        Me.chkGoldQly.Size = New System.Drawing.Size(63, 21)
        Me.chkGoldQly.TabIndex = 0
        Me.chkGoldQly.Text = "ရွှေရည်"
        Me.chkGoldQly.UseVisualStyleBackColor = True
        '
        'RptViewer
        '
        Me.RptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RptViewer.Location = New System.Drawing.Point(0, 269)
        Me.RptViewer.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RptViewer.Name = "RptViewer"
        Me.RptViewer.Size = New System.Drawing.Size(1367, 35)
        Me.RptViewer.TabIndex = 1
        Me.RptViewer.TabStop = False
        '
        'radLooseDiamond
        '
        Me.radLooseDiamond.AutoSize = True
        Me.radLooseDiamond.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radLooseDiamond.Location = New System.Drawing.Point(374, 24)
        Me.radLooseDiamond.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radLooseDiamond.Name = "radLooseDiamond"
        Me.radLooseDiamond.Size = New System.Drawing.Size(122, 21)
        Me.radLooseDiamond.TabIndex = 5
        Me.radLooseDiamond.Text = "LooseDiamond"
        Me.radLooseDiamond.UseVisualStyleBackColor = True
        '
        'frm_rpt_SaleItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1367, 304)
        Me.Controls.Add(Me.RptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Myanmar3", 8.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "frm_rpt_SaleItem"
        Me.Text = "Stock Item"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpSType.ResumeLayout(False)
        Me.grpSType.PerformLayout()
        Me.grpBoxView.ResumeLayout(False)
        Me.grpBoxView.PerformLayout()
        Me.grpStockType.ResumeLayout(False)
        Me.grpStockType.PerformLayout()
        Me.grpType.ResumeLayout(False)
        Me.grpType.PerformLayout()
        Me.grpBoxDate.ResumeLayout(False)
        Me.grpBoxDate.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents grpBoxDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCounter As System.Windows.Forms.Button
    Friend WithEvents LBCounter As System.Windows.Forms.ListBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grpType As System.Windows.Forms.GroupBox
    Friend WithEvents optBalanceStocks As System.Windows.Forms.RadioButton
    Friend WithEvents optAllStocks As System.Windows.Forms.RadioButton
    Friend WithEvents optByGivenDate As System.Windows.Forms.RadioButton
    Friend WithEvents grpStockType As System.Windows.Forms.GroupBox
    Friend WithEvents radStock As System.Windows.Forms.RadioButton
    Friend WithEvents radVolume As System.Windows.Forms.RadioButton
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents radDailyStock As System.Windows.Forms.RadioButton
    Friend WithEvents chkGoldSmith As System.Windows.Forms.CheckBox
    Friend WithEvents chkShopItem As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsFix As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsOrder As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsClosed As System.Windows.Forms.CheckBox
    Friend WithEvents grpBoxView As System.Windows.Forms.GroupBox
    Friend WithEvents radOverView As System.Windows.Forms.RadioButton
    Friend WithEvents radSummary As System.Windows.Forms.RadioButton
    Friend WithEvents radDetail As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblBarcodeNo As System.Windows.Forms.Label
    Friend WithEvents txtBarcodeNo As System.Windows.Forms.TextBox
    Friend WithEvents lblGoldSmith As System.Windows.Forms.Label
    Friend WithEvents txtGoldSmith As System.Windows.Forms.TextBox
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents chkStaff As System.Windows.Forms.CheckBox
    Friend WithEvents cboItemName As System.Windows.Forms.ComboBox
    Friend WithEvents chkItemName As System.Windows.Forms.CheckBox
    Friend WithEvents cboItemCat As System.Windows.Forms.ComboBox
    Friend WithEvents chkItemCat As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQ As System.Windows.Forms.ComboBox
    Friend WithEvents chkGoldQly As System.Windows.Forms.CheckBox
    Friend WithEvents rbtnDiamondStock As System.Windows.Forms.RadioButton
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents radPlatinum As System.Windows.Forms.RadioButton
    Friend WithEvents cboSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents chkSupplier As System.Windows.Forms.CheckBox
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtOriginalCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkInactiveStock As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldSmith As System.Windows.Forms.ComboBox
    Friend WithEvents chkGS As System.Windows.Forms.CheckBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents grpSType As System.Windows.Forms.GroupBox
    Friend WithEvents radSExit As System.Windows.Forms.RadioButton
    Friend WithEvents radSBalance As System.Windows.Forms.RadioButton
    Friend WithEvents radSAll As System.Windows.Forms.RadioButton
    Friend WithEvents chkByLocation As System.Windows.Forms.CheckBox
    Friend WithEvents radByGoldQuality As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTG As System.Windows.Forms.TextBox
    Friend WithEvents txtFG As System.Windows.Forms.TextBox
    Friend WithEvents chkGram As System.Windows.Forms.CheckBox
    Friend WithEvents radLooseDiamond As System.Windows.Forms.RadioButton
End Class
