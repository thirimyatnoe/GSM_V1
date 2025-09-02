<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CashReceiptOnCredit
    Inherits UI.frm_Base

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CashReceiptOnCredit))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.rbtSaleInvoice = New System.Windows.Forms.RadioButton()
        Me.rbtSaleGems = New System.Windows.Forms.RadioButton()
        Me.rbtOrder = New System.Windows.Forms.RadioButton()
        Me.rbtPurchaseGems = New System.Windows.Forms.RadioButton()
        Me.rbtRepairReturn = New System.Windows.Forms.RadioButton()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtBalanceAmt = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtPaidAmt = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtTotalAmt = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtPayAmt = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.dtpPayDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.grdCashReceipt = New System.Windows.Forms.DataGridView()
        Me.PayDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblCurrentLocationName = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadLooseDiamond = New System.Windows.Forms.RadioButton()
        Me.rbtConsignmentSale = New System.Windows.Forms.RadioButton()
        Me.rbtWholeSaleInvoice = New System.Windows.Forms.RadioButton()
        Me.rbtSaleInvoiceVolume = New System.Windows.Forms.RadioButton()
        Me.rbtSalesGems = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.chkIsCash = New System.Windows.Forms.CheckBox()
        Me.chkIsBank = New System.Windows.Forms.CheckBox()
        Me.btnRAdvance = New System.Windows.Forms.Button()
        CType(Me.grdCashReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbtSaleInvoice
        '
        Me.rbtSaleInvoice.AutoSize = True
        Me.rbtSaleInvoice.BackColor = System.Drawing.Color.Transparent
        Me.rbtSaleInvoice.Checked = True
        Me.rbtSaleInvoice.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.rbtSaleInvoice.ForeColor = System.Drawing.Color.Indigo
        Me.rbtSaleInvoice.Location = New System.Drawing.Point(10, 22)
        Me.rbtSaleInvoice.Name = "rbtSaleInvoice"
        Me.rbtSaleInvoice.Size = New System.Drawing.Size(97, 23)
        Me.rbtSaleInvoice.TabIndex = 1
        Me.rbtSaleInvoice.TabStop = True
        Me.rbtSaleInvoice.Text = "Sale Stock"
        Me.rbtSaleInvoice.UseVisualStyleBackColor = False
        '
        'rbtSaleGems
        '
        Me.rbtSaleGems.AutoSize = True
        Me.rbtSaleGems.BackColor = System.Drawing.Color.WhiteSmoke
        Me.rbtSaleGems.Location = New System.Drawing.Point(135, 4)
        Me.rbtSaleGems.Name = "rbtSaleGems"
        Me.rbtSaleGems.Size = New System.Drawing.Size(79, 17)
        Me.rbtSaleGems.TabIndex = 1
        Me.rbtSaleGems.Text = "Sales Gems"
        Me.rbtSaleGems.UseVisualStyleBackColor = False
        Me.rbtSaleGems.Visible = False
        '
        'rbtOrder
        '
        Me.rbtOrder.AutoSize = True
        Me.rbtOrder.BackColor = System.Drawing.Color.Transparent
        Me.rbtOrder.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.rbtOrder.ForeColor = System.Drawing.Color.Indigo
        Me.rbtOrder.Location = New System.Drawing.Point(428, 22)
        Me.rbtOrder.Name = "rbtOrder"
        Me.rbtOrder.Size = New System.Drawing.Size(157, 23)
        Me.rbtOrder.TabIndex = 2
        Me.rbtOrder.Text = "Order Stock Return"
        Me.rbtOrder.UseVisualStyleBackColor = False
        '
        'rbtPurchaseGems
        '
        Me.rbtPurchaseGems.AutoSize = True
        Me.rbtPurchaseGems.BackColor = System.Drawing.Color.WhiteSmoke
        Me.rbtPurchaseGems.Location = New System.Drawing.Point(29, 4)
        Me.rbtPurchaseGems.Name = "rbtPurchaseGems"
        Me.rbtPurchaseGems.Size = New System.Drawing.Size(98, 17)
        Me.rbtPurchaseGems.TabIndex = 0
        Me.rbtPurchaseGems.Text = "Purchase Gems"
        Me.rbtPurchaseGems.UseVisualStyleBackColor = False
        Me.rbtPurchaseGems.Visible = False
        '
        'rbtRepairReturn
        '
        Me.rbtRepairReturn.AutoSize = True
        Me.rbtRepairReturn.BackColor = System.Drawing.Color.Transparent
        Me.rbtRepairReturn.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.rbtRepairReturn.ForeColor = System.Drawing.Color.Indigo
        Me.rbtRepairReturn.Location = New System.Drawing.Point(608, 22)
        Me.rbtRepairReturn.Name = "rbtRepairReturn"
        Me.rbtRepairReturn.Size = New System.Drawing.Size(163, 23)
        Me.rbtRepairReturn.TabIndex = 0
        Me.rbtRepairReturn.Text = "Repair Stock Return"
        Me.rbtRepairReturn.UseVisualStyleBackColor = False
        '
        'SearchButton
        '
        Me.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchButton.Image = CType(resources.GetObject("SearchButton.Image"), System.Drawing.Image)
        Me.SearchButton.Location = New System.Drawing.Point(263, 150)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(30, 21)
        Me.SearchButton.TabIndex = 5
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVoucherNo.Location = New System.Drawing.Point(127, 151)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.ReadOnly = True
        Me.txtVoucherNo.Size = New System.Drawing.Size(130, 21)
        Me.txtVoucherNo.TabIndex = 774
        Me.txtVoucherNo.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(47, 156)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 17)
        Me.Label9.TabIndex = 776
        Me.Label9.Text = "Voucher #"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LightBlue
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(8, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(830, 21)
        Me.Label7.TabIndex = 777
        Me.Label7.Text = "Cash Receipt on Credit"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDate
        '
        Me.txtDate.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Location = New System.Drawing.Point(127, 180)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.Size = New System.Drawing.Size(130, 21)
        Me.txtDate.TabIndex = 778
        Me.txtDate.TabStop = False
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCustomer.Location = New System.Drawing.Point(127, 210)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.Size = New System.Drawing.Size(234, 27)
        Me.txtCustomer.TabIndex = 779
        Me.txtCustomer.TabStop = False
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.BackColor = System.Drawing.Color.Transparent
        Me.lblCustomer.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.lblCustomer.Location = New System.Drawing.Point(65, 213)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustomer.Size = New System.Drawing.Size(47, 20)
        Me.lblCustomer.TabIndex = 781
        Me.lblCustomer.Text = "ဝယ်သူ"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label10.Location = New System.Drawing.Point(87, 180)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 20)
        Me.Label10.TabIndex = 780
        Me.Label10.Text = "နေ့စွဲ"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtBalanceAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmt.Location = New System.Drawing.Point(636, 216)
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReadOnly = True
        Me.txtBalanceAmt.Size = New System.Drawing.Size(146, 22)
        Me.txtBalanceAmt.TabIndex = 784
        Me.txtBalanceAmt.TabStop = False
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label39.Location = New System.Drawing.Point(583, 216)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(51, 20)
        Me.Label39.TabIndex = 787
        Me.Label39.Text = "ကျန်ငွေ"
        '
        'txtPaidAmt
        '
        Me.txtPaidAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtPaidAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidAmt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmt.Location = New System.Drawing.Point(636, 185)
        Me.txtPaidAmt.Name = "txtPaidAmt"
        Me.txtPaidAmt.Size = New System.Drawing.Size(146, 22)
        Me.txtPaidAmt.TabIndex = 782
        Me.txtPaidAmt.TabStop = False
        Me.txtPaidAmt.Text = "0"
        Me.txtPaidAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label38.Location = New System.Drawing.Point(571, 185)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(63, 20)
        Me.Label38.TabIndex = 786
        Me.Label38.Text = "ပေးပြီးငွေ"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmt.Location = New System.Drawing.Point(636, 153)
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.Size = New System.Drawing.Size(146, 22)
        Me.txtTotalAmt.TabIndex = 783
        Me.txtTotalAmt.TabStop = False
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label36.Location = New System.Drawing.Point(518, 153)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(116, 20)
        Me.Label36.TabIndex = 785
        Me.Label36.Text = "စုစုပေါင်းကျသင့်ငွေ"
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.White
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtRemark.Location = New System.Drawing.Point(73, 320)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(241, 80)
        Me.txtRemark.TabIndex = 8
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label43.Location = New System.Drawing.Point(13, 322)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(59, 20)
        Me.Label43.TabIndex = 794
        Me.Label43.Text = "မှတ်ချက်"
        '
        'txtPayAmt
        '
        Me.txtPayAmt.BackColor = System.Drawing.Color.White
        Me.txtPayAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayAmt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayAmt.Location = New System.Drawing.Point(73, 290)
        Me.txtPayAmt.Name = "txtPayAmt"
        Me.txtPayAmt.Size = New System.Drawing.Size(139, 22)
        Me.txtPayAmt.TabIndex = 7
        Me.txtPayAmt.Text = " "
        Me.txtPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label40.Location = New System.Drawing.Point(26, 291)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(46, 20)
        Me.Label40.TabIndex = 793
        Me.Label40.Text = "ပေးငွေ"
        '
        'dtpPayDate
        '
        Me.dtpPayDate.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpPayDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPayDate.Location = New System.Drawing.Point(73, 263)
        Me.dtpPayDate.Name = "dtpPayDate"
        Me.dtpPayDate.Size = New System.Drawing.Size(139, 21)
        Me.dtpPayDate.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label2.Location = New System.Drawing.Point(38, 263)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 20)
        Me.Label2.TabIndex = 792
        Me.Label2.Text = "နေ့စွဲ"
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClear.ForeColor = System.Drawing.Color.Firebrick
        Me.btnClear.Location = New System.Drawing.Point(172, 406)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(70, 25)
        Me.btnClear.TabIndex = 10
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.ForeColor = System.Drawing.Color.Firebrick
        Me.btnAdd.Location = New System.Drawing.Point(96, 406)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 25)
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = "&Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'grdCashReceipt
        '
        Me.grdCashReceipt.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue
        Me.grdCashReceipt.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCashReceipt.BackgroundColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCashReceipt.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdCashReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCashReceipt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PayDate, Me.Amount, Me.Remark})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdCashReceipt.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdCashReceipt.GridColor = System.Drawing.SystemColors.Control
        Me.grdCashReceipt.Location = New System.Drawing.Point(326, 261)
        Me.grdCashReceipt.Name = "grdCashReceipt"
        Me.grdCashReceipt.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCashReceipt.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdCashReceipt.RowHeadersWidth = 25
        Me.grdCashReceipt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCashReceipt.Size = New System.Drawing.Size(511, 144)
        Me.grdCashReceipt.TabIndex = 797
        Me.grdCashReceipt.TabStop = False
        '
        'PayDate
        '
        Me.PayDate.HeaderText = "ReceiptDate"
        Me.PayDate.Name = "PayDate"
        Me.PayDate.ReadOnly = True
        '
        'Amount
        '
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.ReadOnly = True
        '
        'Remark
        '
        Me.Remark.HeaderText = "Remark"
        Me.Remark.Name = "Remark"
        Me.Remark.ReadOnly = True
        '
        'lblCurrentLocationName
        '
        Me.lblCurrentLocationName.AutoSize = True
        Me.lblCurrentLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentLocationName.Font = New System.Drawing.Font("Myanmar3", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLocationName.ForeColor = System.Drawing.Color.DarkCyan
        Me.lblCurrentLocationName.Location = New System.Drawing.Point(354, 6)
        Me.lblCurrentLocationName.Name = "lblCurrentLocationName"
        Me.lblCurrentLocationName.Size = New System.Drawing.Size(89, 19)
        Me.lblCurrentLocationName.TabIndex = 798
        Me.lblCurrentLocationName.Text = "Head Office"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.GroupBox1.Controls.Add(Me.RadLooseDiamond)
        Me.GroupBox1.Controls.Add(Me.rbtConsignmentSale)
        Me.GroupBox1.Controls.Add(Me.rbtWholeSaleInvoice)
        Me.GroupBox1.Controls.Add(Me.rbtSaleInvoiceVolume)
        Me.GroupBox1.Controls.Add(Me.rbtSalesGems)
        Me.GroupBox1.Controls.Add(Me.rbtSaleInvoice)
        Me.GroupBox1.Controls.Add(Me.rbtOrder)
        Me.GroupBox1.Controls.Add(Me.rbtRepairReturn)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(7, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(830, 81)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'RadLooseDiamond
        '
        Me.RadLooseDiamond.AutoSize = True
        Me.RadLooseDiamond.BackColor = System.Drawing.Color.Transparent
        Me.RadLooseDiamond.Checked = True
        Me.RadLooseDiamond.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.RadLooseDiamond.ForeColor = System.Drawing.Color.Indigo
        Me.RadLooseDiamond.Location = New System.Drawing.Point(367, 51)
        Me.RadLooseDiamond.Name = "RadLooseDiamond"
        Me.RadLooseDiamond.Size = New System.Drawing.Size(164, 23)
        Me.RadLooseDiamond.TabIndex = 7
        Me.RadLooseDiamond.TabStop = True
        Me.RadLooseDiamond.Text = "Sale Loose Diamond"
        Me.RadLooseDiamond.UseVisualStyleBackColor = False
        '
        'rbtConsignmentSale
        '
        Me.rbtConsignmentSale.AutoSize = True
        Me.rbtConsignmentSale.BackColor = System.Drawing.Color.Transparent
        Me.rbtConsignmentSale.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.rbtConsignmentSale.ForeColor = System.Drawing.Color.Indigo
        Me.rbtConsignmentSale.Location = New System.Drawing.Point(165, 50)
        Me.rbtConsignmentSale.Name = "rbtConsignmentSale"
        Me.rbtConsignmentSale.Size = New System.Drawing.Size(184, 23)
        Me.rbtConsignmentSale.TabIndex = 6
        Me.rbtConsignmentSale.Text = "ConsignmentSale Stock"
        Me.rbtConsignmentSale.UseVisualStyleBackColor = False
        '
        'rbtWholeSaleInvoice
        '
        Me.rbtWholeSaleInvoice.AutoSize = True
        Me.rbtWholeSaleInvoice.BackColor = System.Drawing.Color.Transparent
        Me.rbtWholeSaleInvoice.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.rbtWholeSaleInvoice.ForeColor = System.Drawing.Color.Indigo
        Me.rbtWholeSaleInvoice.Location = New System.Drawing.Point(10, 51)
        Me.rbtWholeSaleInvoice.Name = "rbtWholeSaleInvoice"
        Me.rbtWholeSaleInvoice.Size = New System.Drawing.Size(140, 23)
        Me.rbtWholeSaleInvoice.TabIndex = 5
        Me.rbtWholeSaleInvoice.Text = "WholeSale Stock"
        Me.rbtWholeSaleInvoice.UseVisualStyleBackColor = False
        '
        'rbtSaleInvoiceVolume
        '
        Me.rbtSaleInvoiceVolume.AutoSize = True
        Me.rbtSaleInvoiceVolume.BackColor = System.Drawing.Color.Transparent
        Me.rbtSaleInvoiceVolume.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.rbtSaleInvoiceVolume.ForeColor = System.Drawing.Color.Indigo
        Me.rbtSaleInvoiceVolume.Location = New System.Drawing.Point(128, 22)
        Me.rbtSaleInvoiceVolume.Name = "rbtSaleInvoiceVolume"
        Me.rbtSaleInvoiceVolume.Size = New System.Drawing.Size(165, 23)
        Me.rbtSaleInvoiceVolume.TabIndex = 4
        Me.rbtSaleInvoiceVolume.Text = "Sale Stock (Volume)"
        Me.rbtSaleInvoiceVolume.UseVisualStyleBackColor = False
        '
        'rbtSalesGems
        '
        Me.rbtSalesGems.AutoSize = True
        Me.rbtSalesGems.BackColor = System.Drawing.Color.Transparent
        Me.rbtSalesGems.Font = New System.Drawing.Font("Myanmar3", 9.5!, System.Drawing.FontStyle.Bold)
        Me.rbtSalesGems.ForeColor = System.Drawing.Color.Indigo
        Me.rbtSalesGems.Location = New System.Drawing.Point(308, 22)
        Me.rbtSalesGems.Name = "rbtSalesGems"
        Me.rbtSalesGems.Size = New System.Drawing.Size(97, 23)
        Me.rbtSalesGems.TabIndex = 3
        Me.rbtSalesGems.Text = "Sale Other"
        Me.rbtSalesGems.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightBlue
        Me.Panel3.Location = New System.Drawing.Point(7, 251)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(775, 3)
        Me.Panel3.TabIndex = 801
        '
        'lblLogInUserName
        '
        Me.lblLogInUserName.AutoSize = True
        Me.lblLogInUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblLogInUserName.Font = New System.Drawing.Font("Myanmar3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogInUserName.ForeColor = System.Drawing.Color.Violet
        Me.lblLogInUserName.Location = New System.Drawing.Point(597, 5)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(90, 22)
        Me.lblLogInUserName.TabIndex = 1355
        Me.lblLogInUserName.Text = "LogInUser"
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(804, -1)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 1456
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(326, 411)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(87, 31)
        Me.btnPrint.TabIndex = 1457
        Me.btnPrint.Text = "Print"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'chkIsCash
        '
        Me.chkIsCash.AutoSize = True
        Me.chkIsCash.BackColor = System.Drawing.Color.Transparent
        Me.chkIsCash.Font = New System.Drawing.Font("Myanmar3", 9.8!, System.Drawing.FontStyle.Bold)
        Me.chkIsCash.ForeColor = System.Drawing.Color.Blue
        Me.chkIsCash.Location = New System.Drawing.Point(299, 151)
        Me.chkIsCash.Name = "chkIsCash"
        Me.chkIsCash.Size = New System.Drawing.Size(87, 24)
        Me.chkIsCash.TabIndex = 1460
        Me.chkIsCash.Text = "ငွေချေပြီး"
        Me.chkIsCash.UseVisualStyleBackColor = False
        '
        'chkIsBank
        '
        Me.chkIsBank.AutoSize = True
        Me.chkIsBank.BackColor = System.Drawing.Color.Transparent
        Me.chkIsBank.Font = New System.Drawing.Font("Myanmar3", 9.8!, System.Drawing.FontStyle.Bold)
        Me.chkIsBank.ForeColor = System.Drawing.Color.Blue
        Me.chkIsBank.Location = New System.Drawing.Point(225, 264)
        Me.chkIsBank.Name = "chkIsBank"
        Me.chkIsBank.Size = New System.Drawing.Size(78, 24)
        Me.chkIsBank.TabIndex = 1461
        Me.chkIsBank.Text = "IsBank"
        Me.chkIsBank.UseVisualStyleBackColor = False
        '
        'btnRAdvance
        '
        Me.btnRAdvance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRAdvance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRAdvance.Image = CType(resources.GetObject("btnRAdvance.Image"), System.Drawing.Image)
        Me.btnRAdvance.Location = New System.Drawing.Point(224, 291)
        Me.btnRAdvance.Name = "btnRAdvance"
        Me.btnRAdvance.Size = New System.Drawing.Size(30, 21)
        Me.btnRAdvance.TabIndex = 1462
        Me.btnRAdvance.UseVisualStyleBackColor = True
        '
        'frm_CashReceiptOnCredit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(843, 499)
        Me.Controls.Add(Me.btnRAdvance)
        Me.Controls.Add(Me.chkIsBank)
        Me.Controls.Add(Me.chkIsCash)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.rbtPurchaseGems)
        Me.Controls.Add(Me.rbtSaleGems)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.lblCurrentLocationName)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdCashReceipt)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label43)
        Me.Controls.Add(Me.txtPayAmt)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.dtpPayDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBalanceAmt)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.txtPaidAmt)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.txtTotalAmt)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtCustomer)
        Me.Controls.Add(Me.lblCustomer)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.txtVoucherNo)
        Me.Controls.Add(Me.Label9)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "frm_CashReceiptOnCredit"
        Me.RightToLeftLayout = True
        Me.Text = "Cash Receipt "
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtVoucherNo, 0)
        Me.Controls.SetChildIndex(Me.SearchButton, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.lblCustomer, 0)
        Me.Controls.SetChildIndex(Me.txtCustomer, 0)
        Me.Controls.SetChildIndex(Me.txtDate, 0)
        Me.Controls.SetChildIndex(Me.Label36, 0)
        Me.Controls.SetChildIndex(Me.txtTotalAmt, 0)
        Me.Controls.SetChildIndex(Me.Label38, 0)
        Me.Controls.SetChildIndex(Me.txtPaidAmt, 0)
        Me.Controls.SetChildIndex(Me.Label39, 0)
        Me.Controls.SetChildIndex(Me.txtBalanceAmt, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.dtpPayDate, 0)
        Me.Controls.SetChildIndex(Me.Label40, 0)
        Me.Controls.SetChildIndex(Me.txtPayAmt, 0)
        Me.Controls.SetChildIndex(Me.Label43, 0)
        Me.Controls.SetChildIndex(Me.txtRemark, 0)
        Me.Controls.SetChildIndex(Me.btnAdd, 0)
        Me.Controls.SetChildIndex(Me.btnClear, 0)
        Me.Controls.SetChildIndex(Me.grdCashReceipt, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.lblCurrentLocationName, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.rbtSaleGems, 0)
        Me.Controls.SetChildIndex(Me.rbtPurchaseGems, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.chkIsCash, 0)
        Me.Controls.SetChildIndex(Me.chkIsBank, 0)
        Me.Controls.SetChildIndex(Me.btnRAdvance, 0)
        CType(Me.grdCashReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbtSaleInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSaleGems As System.Windows.Forms.RadioButton
    Friend WithEvents rbtOrder As System.Windows.Forms.RadioButton
    Friend WithEvents rbtPurchaseGems As System.Windows.Forms.RadioButton
    Friend WithEvents rbtRepairReturn As System.Windows.Forms.RadioButton
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtPaidAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtPayAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents dtpPayDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents PayDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblCurrentLocationName As System.Windows.Forms.Label
    Friend WithEvents grdCashReceipt As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbtSalesGems As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSaleInvoiceVolume As System.Windows.Forms.RadioButton
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents chkIsCash As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsBank As System.Windows.Forms.CheckBox
    Friend WithEvents rbtWholeSaleInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents rbtConsignmentSale As System.Windows.Forms.RadioButton
    Friend WithEvents btnRAdvance As System.Windows.Forms.Button
    Friend WithEvents RadLooseDiamond As System.Windows.Forms.RadioButton

End Class
