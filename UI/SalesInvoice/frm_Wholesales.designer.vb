<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Wholesales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Wholesales))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.dtpWholeSaleInvoice = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SearchWholeSale = New System.Windows.Forms.Button()
        Me.txtWSInvoiceID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.BarcodeNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GoldG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SalesRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtPaidAmt = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtAddOrSubAmt = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNetAmt = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtTotalAmt = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtBalanceAmt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.optConsignment = New System.Windows.Forms.RadioButton()
        Me.optCredit = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCurrentLocationName = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lnkCustomer = New System.Windows.Forms.LinkLabel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTotalQTY = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtTotalItemG = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.lblGold = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.txtTotalItemK = New System.Windows.Forms.TextBox()
        Me.txtTotalItemY = New System.Windows.Forms.TextBox()
        Me.txtTotalItemP = New System.Windows.Forms.TextBox()
        Me.txtDesignCharges = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
        Me.txtDisPercent = New System.Windows.Forms.TextBox()
        Me.txtDiscountAmt = New System.Windows.Forms.TextBox()
        Me.grpMember = New System.Windows.Forms.GroupBox()
        Me.lblRedeemItem = New System.Windows.Forms.Label()
        Me.txtMemberDis = New System.Windows.Forms.TextBox()
        Me.lblPointBalance = New System.Windows.Forms.Label()
        Me.txtMemberDisAmt = New System.Windows.Forms.TextBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.lblPoint = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.lblRedeemPoint = New System.Windows.Forms.Label()
        Me.btnRedeem = New System.Windows.Forms.Button()
        Me.txtPoint = New System.Windows.Forms.TextBox()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.btnRedeemClear = New System.Windows.Forms.Button()
        Me.dtpExpireDate = New System.Windows.Forms.DateTimePicker()
        Me.txtMemberID = New System.Windows.Forms.TextBox()
        Me.txtMemberName = New System.Windows.Forms.TextBox()
        Me.txtMemberCode = New System.Windows.Forms.TextBox()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMember.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(734, 7)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(31, 23)
        Me.btnCustomer.TabIndex = 6
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtCustomerName.Location = New System.Drawing.Point(438, 6)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(281, 26)
        Me.txtCustomerName.TabIndex = 5
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(377, 6)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReadOnly = True
        Me.txtCustomerCode.Size = New System.Drawing.Size(61, 26)
        Me.txtCustomerCode.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label1.Location = New System.Drawing.Point(94, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 16)
        Me.Label1.TabIndex = 809
        Me.Label1.Text = "Staff"
        '
        'cboStaff
        '
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(137, 67)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(194, 25)
        Me.cboStaff.TabIndex = 3
        '
        'dtpWholeSaleInvoice
        '
        Me.dtpWholeSaleInvoice.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpWholeSaleInvoice.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWholeSaleInvoice.Location = New System.Drawing.Point(137, 34)
        Me.dtpWholeSaleInvoice.Name = "dtpWholeSaleInvoice"
        Me.dtpWholeSaleInvoice.Size = New System.Drawing.Size(152, 20)
        Me.dtpWholeSaleInvoice.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label10.Location = New System.Drawing.Point(96, 33)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 16)
        Me.Label10.TabIndex = 806
        Me.Label10.Text = "Date"
        '
        'SearchWholeSale
        '
        Me.SearchWholeSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchWholeSale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchWholeSale.Image = CType(resources.GetObject("SearchWholeSale.Image"), System.Drawing.Image)
        Me.SearchWholeSale.Location = New System.Drawing.Point(259, 9)
        Me.SearchWholeSale.Name = "SearchWholeSale"
        Me.SearchWholeSale.Size = New System.Drawing.Size(30, 21)
        Me.SearchWholeSale.TabIndex = 1
        Me.SearchWholeSale.UseVisualStyleBackColor = True
        '
        'txtWSInvoiceID
        '
        Me.txtWSInvoiceID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWSInvoiceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWSInvoiceID.Location = New System.Drawing.Point(137, 9)
        Me.txtWSInvoiceID.Name = "txtWSInvoiceID"
        Me.txtWSInvoiceID.ReadOnly = True
        Me.txtWSInvoiceID.Size = New System.Drawing.Size(119, 20)
        Me.txtWSInvoiceID.TabIndex = 0
        Me.txtWSInvoiceID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(32, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 16)
        Me.Label9.TabIndex = 805
        Me.Label9.Text = "Wholesales ID"
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BarcodeNo, Me.GoldG, Me.SalesRate, Me.Amount})
        Me.grdItems.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdItems.Location = New System.Drawing.Point(3, 135)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.RowHeadersWidth = 25
        Me.grdItems.Size = New System.Drawing.Size(1310, 283)
        Me.grdItems.TabIndex = 16
        '
        'BarcodeNo
        '
        Me.BarcodeNo.HeaderText = "Barcode#"
        Me.BarcodeNo.Name = "BarcodeNo"
        Me.BarcodeNo.Width = 85
        '
        'GoldG
        '
        Me.GoldG.HeaderText = "G"
        Me.GoldG.MaxInputLength = 10
        Me.GoldG.Name = "GoldG"
        Me.GoldG.Width = 65
        '
        'SalesRate
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.SalesRate.DefaultCellStyle = DataGridViewCellStyle2
        Me.SalesRate.HeaderText = "Rate"
        Me.SalesRate.Name = "SalesRate"
        Me.SalesRate.Width = 65
        '
        'Amount
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Amount.DefaultCellStyle = DataGridViewCellStyle3
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.Width = 65
        '
        'txtPaidAmt
        '
        Me.txtPaidAmt.BackColor = System.Drawing.Color.White
        Me.txtPaidAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmt.Location = New System.Drawing.Point(721, 559)
        Me.txtPaidAmt.Name = "txtPaidAmt"
        Me.txtPaidAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtPaidAmt.TabIndex = 23
        Me.txtPaidAmt.Text = "0"
        Me.txtPaidAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label40.Location = New System.Drawing.Point(633, 558)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(84, 16)
        Me.Label40.TabIndex = 841
        Me.Label40.Text = "Paid Amount"
        '
        'txtAddOrSubAmt
        '
        Me.txtAddOrSubAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAddOrSubAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddOrSubAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddOrSubAmt.Location = New System.Drawing.Point(721, 532)
        Me.txtAddOrSubAmt.Name = "txtAddOrSubAmt"
        Me.txtAddOrSubAmt.ReadOnly = True
        Me.txtAddOrSubAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtAddOrSubAmt.TabIndex = 22
        Me.txtAddOrSubAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label18.Location = New System.Drawing.Point(641, 533)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(75, 16)
        Me.Label18.TabIndex = 840
        Me.Label18.Text = "Add or Sub"
        '
        'txtNetAmt
        '
        Me.txtNetAmt.BackColor = System.Drawing.Color.White
        Me.txtNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAmt.Location = New System.Drawing.Point(720, 505)
        Me.txtNetAmt.Name = "txtNetAmt"
        Me.txtNetAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtNetAmt.TabIndex = 21
        Me.txtNetAmt.Text = "0"
        Me.txtNetAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(588, 506)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(119, 19)
        Me.Label19.TabIndex = 839
        Me.Label19.Text = "အသားတင်ကျသင့်ငွေ"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmt.Location = New System.Drawing.Point(720, 451)
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtTotalAmt.TabIndex = 17
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label20.Location = New System.Drawing.Point(607, 450)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(108, 19)
        Me.Label20.TabIndex = 838
        Me.Label20.Text = "စုစုပေါင်းကျသင့်ငွေ"
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtBalanceAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmt.Location = New System.Drawing.Point(721, 584)
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReadOnly = True
        Me.txtBalanceAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtBalanceAmt.TabIndex = 24
        Me.txtBalanceAmt.Text = "0"
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label3.Location = New System.Drawing.Point(613, 583)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 16)
        Me.Label3.TabIndex = 843
        Me.Label3.Text = "Balance Amount"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'optCash
        '
        Me.optCash.AutoSize = True
        Me.optCash.BackColor = System.Drawing.Color.Transparent
        Me.optCash.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCash.Location = New System.Drawing.Point(227, 466)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(49, 17)
        Me.optCash.TabIndex = 10
        Me.optCash.TabStop = True
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = False
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpDueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(393, 490)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(86, 21)
        Me.dtpDueDate.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(335, 494)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Due Date"
        '
        'optConsignment
        '
        Me.optConsignment.AutoSize = True
        Me.optConsignment.BackColor = System.Drawing.Color.Transparent
        Me.optConsignment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optConsignment.Location = New System.Drawing.Point(227, 492)
        Me.optConsignment.Name = "optConsignment"
        Me.optConsignment.Size = New System.Drawing.Size(87, 17)
        Me.optConsignment.TabIndex = 13
        Me.optConsignment.TabStop = True
        Me.optConsignment.Text = "Consignment"
        Me.optConsignment.UseVisualStyleBackColor = False
        '
        'optCredit
        '
        Me.optCredit.AutoSize = True
        Me.optCredit.BackColor = System.Drawing.Color.Transparent
        Me.optCredit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCredit.Location = New System.Drawing.Point(402, 459)
        Me.optCredit.Name = "optCredit"
        Me.optCredit.Size = New System.Drawing.Size(54, 17)
        Me.optCredit.TabIndex = 12
        Me.optCredit.TabStop = True
        Me.optCredit.Text = "Credit"
        Me.optCredit.UseVisualStyleBackColor = False
        Me.optCredit.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label6.Location = New System.Drawing.Point(553, 480)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(133, 19)
        Me.Label6.TabIndex = 945
        Me.Label6.Text = "Discount လျှော့ငွေ (%)"
        '
        'lblCurrentLocationName
        '
        Me.lblCurrentLocationName.AutoSize = True
        Me.lblCurrentLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentLocationName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLocationName.ForeColor = System.Drawing.Color.Blue
        Me.lblCurrentLocationName.Location = New System.Drawing.Point(1066, 6)
        Me.lblCurrentLocationName.Name = "lblCurrentLocationName"
        Me.lblCurrentLocationName.Size = New System.Drawing.Size(99, 20)
        Me.lblCurrentLocationName.TabIndex = 1061
        Me.lblCurrentLocationName.Text = "GlobalGold"
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtAddress.Location = New System.Drawing.Point(377, 36)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(391, 43)
        Me.txtAddress.TabIndex = 1261
        Me.txtAddress.TabStop = False
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label46.Location = New System.Drawing.Point(324, 36)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(48, 20)
        Me.Label46.TabIndex = 1262
        Me.Label46.Text = "လိပ်စာ"
        '
        'lnkCustomer
        '
        Me.lnkCustomer.AutoSize = True
        Me.lnkCustomer.BackColor = System.Drawing.Color.Transparent
        Me.lnkCustomer.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.lnkCustomer.Location = New System.Drawing.Point(327, 9)
        Me.lnkCustomer.Name = "lnkCustomer"
        Me.lnkCustomer.Size = New System.Drawing.Size(47, 20)
        Me.lnkCustomer.TabIndex = 1357
        Me.lnkCustomer.TabStop = True
        Me.lnkCustomer.Text = "ဝယ်သူ"
        '
        'btnPrint
        '
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(1209, 63)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 31)
        Me.btnPrint.TabIndex = 1358
        Me.btnPrint.Text = "Print "
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Moccasin
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(454, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(159, 23)
        Me.Label7.TabIndex = 1359
        Me.Label7.Text = "အထည်ချိန်စုစုပေါင်း"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.LightBlue
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(613, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 23)
        Me.Label2.TabIndex = 1360
        Me.Label2.Text = "အလျော့တွက်"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(732, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 23)
        Me.Label4.TabIndex = 1361
        Me.Label4.Text = "ကျောက်ချိန်"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Khaki
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(855, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 23)
        Me.Label5.TabIndex = 1362
        Me.Label5.Text = "ရွှေအတင်အလေးချိန်"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRemark
        '
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtRemark.Location = New System.Drawing.Point(871, 39)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(195, 57)
        Me.txtRemark.TabIndex = 1363
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label22.Location = New System.Drawing.Point(867, 9)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(59, 20)
        Me.Label22.TabIndex = 1364
        Me.Label22.Text = "မှတ်ချက်"
        '
        'txtTotalQTY
        '
        Me.txtTotalQTY.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalQTY.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalQTY.Location = New System.Drawing.Point(424, 426)
        Me.txtTotalQTY.MaxLength = 6
        Me.txtTotalQTY.Name = "txtTotalQTY"
        Me.txtTotalQTY.ReadOnly = True
        Me.txtTotalQTY.Size = New System.Drawing.Size(55, 21)
        Me.txtTotalQTY.TabIndex = 1632
        Me.txtTotalQTY.Text = "0.0"
        Me.txtTotalQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(356, 429)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(65, 17)
        Me.Label16.TabIndex = 1631
        Me.Label16.Text = "TotalQTY"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotalItemG
        '
        Me.txtTotalItemG.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalItemG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalItemG.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalItemG.Location = New System.Drawing.Point(253, 426)
        Me.txtTotalItemG.MaxLength = 6
        Me.txtTotalItemG.Name = "txtTotalItemG"
        Me.txtTotalItemG.ReadOnly = True
        Me.txtTotalItemG.Size = New System.Drawing.Size(55, 21)
        Me.txtTotalItemG.TabIndex = 1630
        Me.txtTotalItemG.Text = "0.0"
        Me.txtTotalItemG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label61.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label61.Location = New System.Drawing.Point(234, 429)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(18, 16)
        Me.Label61.TabIndex = 1629
        Me.Label61.Text = "Y"
        '
        'lblGold
        '
        Me.lblGold.AutoSize = True
        Me.lblGold.BackColor = System.Drawing.Color.Transparent
        Me.lblGold.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.lblGold.ForeColor = System.Drawing.Color.Black
        Me.lblGold.Location = New System.Drawing.Point(5, 427)
        Me.lblGold.Name = "lblGold"
        Me.lblGold.Size = New System.Drawing.Size(78, 17)
        Me.lblGold.TabIndex = 1628
        Me.lblGold.Text = "TotalWeight"
        Me.lblGold.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.Color.Transparent
        Me.Label66.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label66.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label66.Location = New System.Drawing.Point(176, 428)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(16, 16)
        Me.Label66.TabIndex = 1627
        Me.Label66.Text = "P"
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.BackColor = System.Drawing.Color.Transparent
        Me.Label67.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label67.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label67.Location = New System.Drawing.Point(120, 428)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(18, 16)
        Me.Label67.TabIndex = 1626
        Me.Label67.Text = "K"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.BackColor = System.Drawing.Color.Transparent
        Me.Label84.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label84.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label84.Location = New System.Drawing.Point(310, 429)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(40, 16)
        Me.Label84.TabIndex = 1625
        Me.Label84.Text = "Gram"
        '
        'txtTotalItemK
        '
        Me.txtTotalItemK.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalItemK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalItemK.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalItemK.Location = New System.Drawing.Point(85, 425)
        Me.txtTotalItemK.MaxLength = 3
        Me.txtTotalItemK.Name = "txtTotalItemK"
        Me.txtTotalItemK.ReadOnly = True
        Me.txtTotalItemK.Size = New System.Drawing.Size(35, 21)
        Me.txtTotalItemK.TabIndex = 1622
        Me.txtTotalItemK.TabStop = False
        Me.txtTotalItemK.Text = "0"
        Me.txtTotalItemK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTotalItemY
        '
        Me.txtTotalItemY.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalItemY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalItemY.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalItemY.Location = New System.Drawing.Point(199, 426)
        Me.txtTotalItemY.MaxLength = 3
        Me.txtTotalItemY.Name = "txtTotalItemY"
        Me.txtTotalItemY.ReadOnly = True
        Me.txtTotalItemY.Size = New System.Drawing.Size(35, 21)
        Me.txtTotalItemY.TabIndex = 1624
        Me.txtTotalItemY.TabStop = False
        Me.txtTotalItemY.Text = "0"
        Me.txtTotalItemY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTotalItemP
        '
        Me.txtTotalItemP.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalItemP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalItemP.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalItemP.Location = New System.Drawing.Point(140, 425)
        Me.txtTotalItemP.MaxLength = 2
        Me.txtTotalItemP.Name = "txtTotalItemP"
        Me.txtTotalItemP.ReadOnly = True
        Me.txtTotalItemP.Size = New System.Drawing.Size(35, 21)
        Me.txtTotalItemP.TabIndex = 1623
        Me.txtTotalItemP.TabStop = False
        Me.txtTotalItemP.Text = "0"
        Me.txtTotalItemP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDesignCharges
        '
        Me.txtDesignCharges.BackColor = System.Drawing.Color.White
        Me.txtDesignCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesignCharges.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtDesignCharges.Location = New System.Drawing.Point(720, 425)
        Me.txtDesignCharges.Name = "txtDesignCharges"
        Me.txtDesignCharges.Size = New System.Drawing.Size(100, 21)
        Me.txtDesignCharges.TabIndex = 1633
        Me.txtDesignCharges.Text = "0"
        Me.txtDesignCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label15.Location = New System.Drawing.Point(667, 424)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 20)
        Me.Label15.TabIndex = 1634
        Me.Label15.Text = "လက်ခ"
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1264, 7)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 1635
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'lblLogInUserName
        '
        Me.lblLogInUserName.AutoSize = True
        Me.lblLogInUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblLogInUserName.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogInUserName.ForeColor = System.Drawing.Color.Blue
        Me.lblLogInUserName.Location = New System.Drawing.Point(1067, 31)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(151, 23)
        Me.lblLogInUserName.TabIndex = 1638
        Me.lblLogInUserName.Text = "Thiri Myat Noe"
        '
        'txtDisPercent
        '
        Me.txtDisPercent.BackColor = System.Drawing.Color.White
        Me.txtDisPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDisPercent.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisPercent.Location = New System.Drawing.Point(688, 478)
        Me.txtDisPercent.Name = "txtDisPercent"
        Me.txtDisPercent.Size = New System.Drawing.Size(31, 21)
        Me.txtDisPercent.TabIndex = 1639
        Me.txtDisPercent.Text = "0"
        Me.txtDisPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDiscountAmt
        '
        Me.txtDiscountAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtDiscountAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscountAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscountAmt.Location = New System.Drawing.Point(720, 478)
        Me.txtDiscountAmt.Name = "txtDiscountAmt"
        Me.txtDiscountAmt.ReadOnly = True
        Me.txtDiscountAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtDiscountAmt.TabIndex = 1640
        Me.txtDiscountAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grpMember
        '
        Me.grpMember.BackColor = System.Drawing.Color.Transparent
        Me.grpMember.Controls.Add(Me.lblRedeemItem)
        Me.grpMember.Controls.Add(Me.txtMemberDis)
        Me.grpMember.Controls.Add(Me.lblPointBalance)
        Me.grpMember.Controls.Add(Me.txtMemberDisAmt)
        Me.grpMember.Controls.Add(Me.Label72)
        Me.grpMember.Controls.Add(Me.lblPoint)
        Me.grpMember.Controls.Add(Me.Label73)
        Me.grpMember.Controls.Add(Me.lblRedeemPoint)
        Me.grpMember.Controls.Add(Me.btnRedeem)
        Me.grpMember.Controls.Add(Me.txtPoint)
        Me.grpMember.Controls.Add(Me.txtValue)
        Me.grpMember.Controls.Add(Me.btnRedeemClear)
        Me.grpMember.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMember.ForeColor = System.Drawing.Color.Black
        Me.grpMember.Location = New System.Drawing.Point(855, 426)
        Me.grpMember.Margin = New System.Windows.Forms.Padding(2)
        Me.grpMember.Name = "grpMember"
        Me.grpMember.Padding = New System.Windows.Forms.Padding(2)
        Me.grpMember.Size = New System.Drawing.Size(449, 91)
        Me.grpMember.TabIndex = 1671
        Me.grpMember.TabStop = False
        Me.grpMember.Text = "Redeem"
        '
        'lblRedeemItem
        '
        Me.lblRedeemItem.AutoSize = True
        Me.lblRedeemItem.Location = New System.Drawing.Point(273, 17)
        Me.lblRedeemItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblRedeemItem.Name = "lblRedeemItem"
        Me.lblRedeemItem.Size = New System.Drawing.Size(81, 13)
        Me.lblRedeemItem.TabIndex = 1622
        Me.lblRedeemItem.Text = "Redeem Item"
        '
        'txtMemberDis
        '
        Me.txtMemberDis.BackColor = System.Drawing.Color.White
        Me.txtMemberDis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberDis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberDis.Location = New System.Drawing.Point(104, 66)
        Me.txtMemberDis.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberDis.Name = "txtMemberDis"
        Me.txtMemberDis.Size = New System.Drawing.Size(41, 21)
        Me.txtMemberDis.TabIndex = 1619
        Me.txtMemberDis.Text = "0"
        Me.txtMemberDis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPointBalance
        '
        Me.lblPointBalance.AutoSize = True
        Me.lblPointBalance.Location = New System.Drawing.Point(366, 43)
        Me.lblPointBalance.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPointBalance.Name = "lblPointBalance"
        Me.lblPointBalance.Size = New System.Drawing.Size(36, 13)
        Me.lblPointBalance.TabIndex = 1616
        Me.lblPointBalance.Text = "Point"
        '
        'txtMemberDisAmt
        '
        Me.txtMemberDisAmt.BackColor = System.Drawing.Color.White
        Me.txtMemberDisAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberDisAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberDisAmt.Location = New System.Drawing.Point(146, 66)
        Me.txtMemberDisAmt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberDisAmt.Name = "txtMemberDisAmt"
        Me.txtMemberDisAmt.Size = New System.Drawing.Size(104, 21)
        Me.txtMemberDisAmt.TabIndex = 1621
        Me.txtMemberDisAmt.Text = "0"
        Me.txtMemberDisAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label72
        '
        Me.Label72.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label72.AutoSize = True
        Me.Label72.BackColor = System.Drawing.Color.Transparent
        Me.Label72.Font = New System.Drawing.Font("Myanmar3", 9.25!)
        Me.Label72.Location = New System.Drawing.Point(3, 66)
        Me.Label72.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(103, 19)
        Me.Label72.TabIndex = 1620
        Me.Label72.Text = "Member Dis(%)"
        '
        'lblPoint
        '
        Me.lblPoint.AutoSize = True
        Me.lblPoint.Location = New System.Drawing.Point(273, 43)
        Me.lblPoint.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPoint.Name = "lblPoint"
        Me.lblPoint.Size = New System.Drawing.Size(94, 13)
        Me.lblPoint.TabIndex = 1615
        Me.lblPoint.Text = "Point Balance -"
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.BackColor = System.Drawing.Color.Transparent
        Me.Label73.Font = New System.Drawing.Font("Myanmar3", 9.25!)
        Me.Label73.Location = New System.Drawing.Point(3, 40)
        Me.Label73.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(55, 19)
        Me.Label73.TabIndex = 1614
        Me.Label73.Text = "Amount"
        '
        'lblRedeemPoint
        '
        Me.lblRedeemPoint.AutoSize = True
        Me.lblRedeemPoint.BackColor = System.Drawing.Color.Transparent
        Me.lblRedeemPoint.Font = New System.Drawing.Font("Myanmar3", 9.25!)
        Me.lblRedeemPoint.Location = New System.Drawing.Point(3, 21)
        Me.lblRedeemPoint.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRedeemPoint.Name = "lblRedeemPoint"
        Me.lblRedeemPoint.Size = New System.Drawing.Size(39, 19)
        Me.lblRedeemPoint.TabIndex = 1609
        Me.lblRedeemPoint.Text = "Point"
        '
        'btnRedeem
        '
        Me.btnRedeem.Location = New System.Drawing.Point(273, 62)
        Me.btnRedeem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRedeem.Name = "btnRedeem"
        Me.btnRedeem.Size = New System.Drawing.Size(70, 25)
        Me.btnRedeem.TabIndex = 1613
        Me.btnRedeem.Text = "Redeem"
        Me.btnRedeem.UseVisualStyleBackColor = True
        '
        'txtPoint
        '
        Me.txtPoint.BackColor = System.Drawing.Color.White
        Me.txtPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPoint.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPoint.Location = New System.Drawing.Point(105, 15)
        Me.txtPoint.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtPoint.Name = "txtPoint"
        Me.txtPoint.Size = New System.Drawing.Size(146, 21)
        Me.txtPoint.TabIndex = 1610
        Me.txtPoint.Text = "0"
        Me.txtPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValue
        '
        Me.txtValue.BackColor = System.Drawing.Color.White
        Me.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValue.Location = New System.Drawing.Point(105, 40)
        Me.txtValue.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(146, 21)
        Me.txtValue.TabIndex = 1608
        Me.txtValue.Text = "0"
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnRedeemClear
        '
        Me.btnRedeemClear.Location = New System.Drawing.Point(345, 62)
        Me.btnRedeemClear.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRedeemClear.Name = "btnRedeemClear"
        Me.btnRedeemClear.Size = New System.Drawing.Size(64, 25)
        Me.btnRedeemClear.TabIndex = 13
        Me.btnRedeemClear.Text = "Clear"
        Me.btnRedeemClear.UseVisualStyleBackColor = True
        '
        'dtpExpireDate
        '
        Me.dtpExpireDate.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpireDate.Location = New System.Drawing.Point(778, 49)
        Me.dtpExpireDate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtpExpireDate.Name = "dtpExpireDate"
        Me.dtpExpireDate.Size = New System.Drawing.Size(54, 20)
        Me.dtpExpireDate.TabIndex = 1675
        Me.dtpExpireDate.Visible = False
        '
        'txtMemberID
        '
        Me.txtMemberID.BackColor = System.Drawing.Color.Linen
        Me.txtMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberID.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberID.Location = New System.Drawing.Point(699, 82)
        Me.txtMemberID.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberID.Name = "txtMemberID"
        Me.txtMemberID.ReadOnly = True
        Me.txtMemberID.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberID.TabIndex = 1674
        '
        'txtMemberName
        '
        Me.txtMemberName.BackColor = System.Drawing.Color.Linen
        Me.txtMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberName.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberName.Location = New System.Drawing.Point(540, 82)
        Me.txtMemberName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberName.Name = "txtMemberName"
        Me.txtMemberName.ReadOnly = True
        Me.txtMemberName.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberName.TabIndex = 1673
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BackColor = System.Drawing.Color.Linen
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.Location = New System.Drawing.Point(377, 82)
        Me.txtMemberCode.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.ReadOnly = True
        Me.txtMemberCode.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberCode.TabIndex = 1672
        '
        'frm_Wholesales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1313, 656)
        Me.Controls.Add(Me.dtpExpireDate)
        Me.Controls.Add(Me.txtMemberID)
        Me.Controls.Add(Me.txtMemberName)
        Me.Controls.Add(Me.txtMemberCode)
        Me.Controls.Add(Me.grpMember)
        Me.Controls.Add(Me.txtDiscountAmt)
        Me.Controls.Add(Me.txtDisPercent)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.txtDesignCharges)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtTotalQTY)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtTotalItemG)
        Me.Controls.Add(Me.Label61)
        Me.Controls.Add(Me.lblGold)
        Me.Controls.Add(Me.Label66)
        Me.Controls.Add(Me.Label67)
        Me.Controls.Add(Me.Label84)
        Me.Controls.Add(Me.txtTotalItemK)
        Me.Controls.Add(Me.txtTotalItemY)
        Me.Controls.Add(Me.txtTotalItemP)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.lnkCustomer)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.Label46)
        Me.Controls.Add(Me.lblCurrentLocationName)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.optCash)
        Me.Controls.Add(Me.dtpDueDate)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.optConsignment)
        Me.Controls.Add(Me.optCredit)
        Me.Controls.Add(Me.txtBalanceAmt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtPaidAmt)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.txtAddOrSubAmt)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtNetAmt)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtTotalAmt)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.grdItems)
        Me.Controls.Add(Me.btnCustomer)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.txtCustomerCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.dtpWholeSaleInvoice)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.SearchWholeSale)
        Me.Controls.Add(Me.txtWSInvoiceID)
        Me.Controls.Add(Me.Label9)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_Wholesales"
        Me.Text = "WholeSales Invoice"
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtWSInvoiceID, 0)
        Me.Controls.SetChildIndex(Me.SearchWholeSale, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.dtpWholeSaleInvoice, 0)
        Me.Controls.SetChildIndex(Me.cboStaff, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtCustomerCode, 0)
        Me.Controls.SetChildIndex(Me.txtCustomerName, 0)
        Me.Controls.SetChildIndex(Me.btnCustomer, 0)
        Me.Controls.SetChildIndex(Me.grdItems, 0)
        Me.Controls.SetChildIndex(Me.Label20, 0)
        Me.Controls.SetChildIndex(Me.txtTotalAmt, 0)
        Me.Controls.SetChildIndex(Me.Label19, 0)
        Me.Controls.SetChildIndex(Me.txtNetAmt, 0)
        Me.Controls.SetChildIndex(Me.Label18, 0)
        Me.Controls.SetChildIndex(Me.txtAddOrSubAmt, 0)
        Me.Controls.SetChildIndex(Me.Label40, 0)
        Me.Controls.SetChildIndex(Me.txtPaidAmt, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtBalanceAmt, 0)
        Me.Controls.SetChildIndex(Me.optCredit, 0)
        Me.Controls.SetChildIndex(Me.optConsignment, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.dtpDueDate, 0)
        Me.Controls.SetChildIndex(Me.optCash, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.lblCurrentLocationName, 0)
        Me.Controls.SetChildIndex(Me.Label46, 0)
        Me.Controls.SetChildIndex(Me.txtAddress, 0)
        Me.Controls.SetChildIndex(Me.lnkCustomer, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.txtRemark, 0)
        Me.Controls.SetChildIndex(Me.txtTotalItemP, 0)
        Me.Controls.SetChildIndex(Me.txtTotalItemY, 0)
        Me.Controls.SetChildIndex(Me.txtTotalItemK, 0)
        Me.Controls.SetChildIndex(Me.Label84, 0)
        Me.Controls.SetChildIndex(Me.Label67, 0)
        Me.Controls.SetChildIndex(Me.Label66, 0)
        Me.Controls.SetChildIndex(Me.lblGold, 0)
        Me.Controls.SetChildIndex(Me.Label61, 0)
        Me.Controls.SetChildIndex(Me.txtTotalItemG, 0)
        Me.Controls.SetChildIndex(Me.Label16, 0)
        Me.Controls.SetChildIndex(Me.txtTotalQTY, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.txtDesignCharges, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
        Me.Controls.SetChildIndex(Me.txtDisPercent, 0)
        Me.Controls.SetChildIndex(Me.txtDiscountAmt, 0)
        Me.Controls.SetChildIndex(Me.grpMember, 0)
        Me.Controls.SetChildIndex(Me.txtMemberCode, 0)
        Me.Controls.SetChildIndex(Me.txtMemberName, 0)
        Me.Controls.SetChildIndex(Me.txtMemberID, 0)
        Me.Controls.SetChildIndex(Me.dtpExpireDate, 0)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMember.ResumeLayout(False)
        Me.grpMember.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents dtpWholeSaleInvoice As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents SearchWholeSale As System.Windows.Forms.Button
    Friend WithEvents txtWSInvoiceID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents BarcodeNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GoldG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SalesRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtPaidAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtAddOrSubAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents optConsignment As System.Windows.Forms.RadioButton
    Friend WithEvents optCredit As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentLocationName As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents lnkCustomer As System.Windows.Forms.LinkLabel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtTotalQTY As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtTotalItemG As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents lblGold As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents txtTotalItemK As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalItemY As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalItemP As System.Windows.Forms.TextBox
    Friend WithEvents txtDesignCharges As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
    Friend WithEvents txtDisPercent As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscountAmt As System.Windows.Forms.TextBox
    Friend WithEvents grpMember As System.Windows.Forms.GroupBox
    Friend WithEvents lblRedeemItem As System.Windows.Forms.Label
    Friend WithEvents txtMemberDis As System.Windows.Forms.TextBox
    Friend WithEvents lblPointBalance As System.Windows.Forms.Label
    Friend WithEvents txtMemberDisAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents lblPoint As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents lblRedeemPoint As System.Windows.Forms.Label
    Friend WithEvents btnRedeem As System.Windows.Forms.Button
    Friend WithEvents txtPoint As System.Windows.Forms.TextBox
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents btnRedeemClear As System.Windows.Forms.Button
    Friend WithEvents dtpExpireDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtMemberID As System.Windows.Forms.TextBox
    Friend WithEvents txtMemberName As System.Windows.Forms.TextBox
    Friend WithEvents txtMemberCode As System.Windows.Forms.TextBox

End Class
