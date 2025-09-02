<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_SaleVolumeInvoice
    Inherits UI.frm_Base

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_SaleVolumeInvoice))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SearchSale = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblCurrentLocationName = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpSaleDate = New System.Windows.Forms.DateTimePicker()
        Me.txtSalesVolumeID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCurrentPrice = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.chkIsFixPrice = New System.Windows.Forms.CheckBox()
        Me.txtLength = New System.Windows.Forms.TextBox()
        Me.txtGoldQuality = New System.Windows.Forms.TextBox()
        Me.txtItemCategory = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtBarcodeNo = New System.Windows.Forms.TextBox()
        Me.txtItemY = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txtWasteTK = New System.Windows.Forms.TextBox()
        Me.txtItemTK = New System.Windows.Forms.TextBox()
        Me.txtItemK = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtItemP = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtWasteK = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtWasteP = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtWasteY = New System.Windows.Forms.TextBox()
        Me.txtItemTG = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtWasteTG = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.LnkTotalNoWaste = New System.Windows.Forms.LinkLabel()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblQTY = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtTotalTK = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTotalK = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTotalP = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotalY = New System.Windows.Forms.TextBox()
        Me.txtTotalTG = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtAddSub = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtNetAmt = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtTotalAmt = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtSaleFixPrice = New System.Windows.Forms.TextBox()
        Me.txtPromotionDis = New System.Windows.Forms.TextBox()
        Me.txtPromotionAmt = New System.Windows.Forms.TextBox()
        Me.lblPromotion = New System.Windows.Forms.Label()
        Me.txtAllAddOrSub = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtAllNetAmt = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtAllTotalAmt = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtDiscountAmt = New System.Windows.Forms.TextBox()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.txtBalanceAmt = New System.Windows.Forms.TextBox()
        Me.txtPaidAmt = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.grdDetail = New System.Windows.Forms.DataGridView()
        Me.GoldG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SalesRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblWeight = New System.Windows.Forms.Label()
        Me.chkSaleFix = New System.Windows.Forms.CheckBox()
        Me.txtGoldPrice = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblIsGram = New System.Windows.Forms.Label()
        Me.lblDonePrice = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.SearchItem = New System.Windows.Forms.Button()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtDifferentAmount = New System.Windows.Forms.TextBox()
        Me.txtPurchaseAmount = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnSearchPurchase = New System.Windows.Forms.Button()
        Me.txtPurchaseVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDesignCharges = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ChkIsSolidVolume = New System.Windows.Forms.CheckBox()
        Me.txtDesignRate = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtMemberID = New System.Windows.Forms.TextBox()
        Me.txtMemberName = New System.Windows.Forms.TextBox()
        Me.txtMemberCode = New System.Windows.Forms.TextBox()
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
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMember.SuspendLayout()
        Me.SuspendLayout()
        '
        'SearchSale
        '
        Me.SearchSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchSale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchSale.Image = CType(resources.GetObject("SearchSale.Image"), System.Drawing.Image)
        Me.SearchSale.Location = New System.Drawing.Point(255, 7)
        Me.SearchSale.Name = "SearchSale"
        Me.SearchSale.Size = New System.Drawing.Size(41, 21)
        Me.SearchSale.TabIndex = 4
        Me.SearchSale.UseVisualStyleBackColor = True
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Zawgyi-One", 8.25!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(445, 11)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(107, 25)
        Me.txtCustomerCode.TabIndex = 7
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(729, 9)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(41, 26)
        Me.btnCustomer.TabIndex = 9
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.White
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCustomer.Location = New System.Drawing.Point(558, 9)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(165, 27)
        Me.txtCustomer.TabIndex = 8
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtAddress.Location = New System.Drawing.Point(445, 40)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(325, 49)
        Me.txtAddress.TabIndex = 1353
        Me.txtAddress.TabStop = False
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label46.Location = New System.Drawing.Point(391, 42)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(48, 20)
        Me.Label46.TabIndex = 1354
        Me.Label46.Text = "လိပ်စာ"
        '
        'lblCurrentLocationName
        '
        Me.lblCurrentLocationName.AutoSize = True
        Me.lblCurrentLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentLocationName.Font = New System.Drawing.Font("Myanmar3", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLocationName.ForeColor = System.Drawing.Color.DarkCyan
        Me.lblCurrentLocationName.Location = New System.Drawing.Point(302, 11)
        Me.lblCurrentLocationName.Name = "lblCurrentLocationName"
        Me.lblCurrentLocationName.Size = New System.Drawing.Size(84, 19)
        Me.lblCurrentLocationName.TabIndex = 1351
        Me.lblCurrentLocationName.Text = "HeadOffice"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label10.Location = New System.Drawing.Point(29, 67)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 20)
        Me.Label10.TabIndex = 1350
        Me.Label10.Text = "တာဝန်ခံဝန်ထမ်း"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label29.Location = New System.Drawing.Point(99, 37)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(34, 20)
        Me.Label29.TabIndex = 1349
        Me.Label29.Text = "နေ့စွဲ"
        '
        'txtRemark
        '
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtRemark.Location = New System.Drawing.Point(961, 36)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(228, 75)
        Me.txtRemark.TabIndex = 10
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label22.Location = New System.Drawing.Point(891, 37)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(59, 20)
        Me.Label22.TabIndex = 1348
        Me.Label22.Text = "မှတ်ချက်"
        '
        'cboStaff
        '
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(135, 64)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(228, 27)
        Me.cboStaff.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LightBlue
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(2, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1187, 23)
        Me.Label7.TabIndex = 1347
        Me.Label7.Text = "Sale Volume Items"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpSaleDate
        '
        Me.dtpSaleDate.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpSaleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSaleDate.Location = New System.Drawing.Point(135, 36)
        Me.dtpSaleDate.Name = "dtpSaleDate"
        Me.dtpSaleDate.Size = New System.Drawing.Size(142, 21)
        Me.dtpSaleDate.TabIndex = 5
        '
        'txtSalesVolumeID
        '
        Me.txtSalesVolumeID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSalesVolumeID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesVolumeID.Location = New System.Drawing.Point(135, 7)
        Me.txtSalesVolumeID.Name = "txtSalesVolumeID"
        Me.txtSalesVolumeID.ReadOnly = True
        Me.txtSalesVolumeID.Size = New System.Drawing.Size(119, 21)
        Me.txtSalesVolumeID.TabIndex = 1346
        Me.txtSalesVolumeID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(12, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(121, 17)
        Me.Label9.TabIndex = 1345
        Me.Label9.Text = "Sale Voucher No:"
        '
        'txtCurrentPrice
        '
        Me.txtCurrentPrice.BackColor = System.Drawing.Color.White
        Me.txtCurrentPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrentPrice.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtCurrentPrice.Location = New System.Drawing.Point(122, 312)
        Me.txtCurrentPrice.Name = "txtCurrentPrice"
        Me.txtCurrentPrice.Size = New System.Drawing.Size(125, 22)
        Me.txtCurrentPrice.TabIndex = 13
        Me.txtCurrentPrice.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label1.Location = New System.Drawing.Point(1, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 20)
        Me.Label1.TabIndex = 1370
        Me.Label1.Text = "ကွင်းတိုင်း/ကြိုးရှည်"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label33.Location = New System.Drawing.Point(46, 184)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(75, 20)
        Me.Label33.TabIndex = 1369
        Me.Label33.Text = "အမျိုးအစား"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label34.Location = New System.Drawing.Point(44, 218)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(77, 20)
        Me.Label34.TabIndex = 1368
        Me.Label34.Text = "အမျိုးအမည်"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label35.Location = New System.Drawing.Point(55, 310)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(66, 20)
        Me.Label35.TabIndex = 1367
        Me.Label35.Text = "ပေါက်စျေး"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label42.Location = New System.Drawing.Point(73, 280)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(48, 20)
        Me.Label42.TabIndex = 1366
        Me.Label42.Text = "ရွှေရည်"
        '
        'chkIsFixPrice
        '
        Me.chkIsFixPrice.AutoSize = True
        Me.chkIsFixPrice.BackColor = System.Drawing.Color.Transparent
        Me.chkIsFixPrice.Enabled = False
        Me.chkIsFixPrice.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkIsFixPrice.Location = New System.Drawing.Point(311, 157)
        Me.chkIsFixPrice.Name = "chkIsFixPrice"
        Me.chkIsFixPrice.Size = New System.Drawing.Size(84, 21)
        Me.chkIsFixPrice.TabIndex = 1365
        Me.chkIsFixPrice.Text = "Fix Price"
        Me.chkIsFixPrice.UseVisualStyleBackColor = False
        '
        'txtLength
        '
        Me.txtLength.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLength.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtLength.Location = New System.Drawing.Point(122, 248)
        Me.txtLength.Name = "txtLength"
        Me.txtLength.ReadOnly = True
        Me.txtLength.Size = New System.Drawing.Size(192, 26)
        Me.txtLength.TabIndex = 1362
        Me.txtLength.TabStop = False
        '
        'txtGoldQuality
        '
        Me.txtGoldQuality.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtGoldQuality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldQuality.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtGoldQuality.Location = New System.Drawing.Point(122, 279)
        Me.txtGoldQuality.Name = "txtGoldQuality"
        Me.txtGoldQuality.ReadOnly = True
        Me.txtGoldQuality.Size = New System.Drawing.Size(192, 27)
        Me.txtGoldQuality.TabIndex = 1363
        Me.txtGoldQuality.TabStop = False
        '
        'txtItemCategory
        '
        Me.txtItemCategory.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtItemCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCategory.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtItemCategory.Location = New System.Drawing.Point(122, 182)
        Me.txtItemCategory.Name = "txtItemCategory"
        Me.txtItemCategory.ReadOnly = True
        Me.txtItemCategory.Size = New System.Drawing.Size(233, 27)
        Me.txtItemCategory.TabIndex = 1361
        Me.txtItemCategory.TabStop = False
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtItemName.Location = New System.Drawing.Point(122, 215)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(233, 27)
        Me.txtItemName.TabIndex = 1360
        Me.txtItemName.TabStop = False
        '
        'txtBarcodeNo
        '
        Me.txtBarcodeNo.BackColor = System.Drawing.Color.White
        Me.txtBarcodeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcodeNo.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtBarcodeNo.Location = New System.Drawing.Point(122, 154)
        Me.txtBarcodeNo.Name = "txtBarcodeNo"
        Me.txtBarcodeNo.Size = New System.Drawing.Size(132, 22)
        Me.txtBarcodeNo.TabIndex = 11
        '
        'txtItemY
        '
        Me.txtItemY.BackColor = System.Drawing.Color.Linen
        Me.txtItemY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemY.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemY.Location = New System.Drawing.Point(570, 221)
        Me.txtItemY.MaxLength = 4
        Me.txtItemY.Name = "txtItemY"
        Me.txtItemY.ReadOnly = True
        Me.txtItemY.Size = New System.Drawing.Size(37, 21)
        Me.txtItemY.TabIndex = 17
        Me.txtItemY.TabStop = False
        Me.txtItemY.Text = "0"
        Me.txtItemY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label51.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label51.Location = New System.Drawing.Point(775, 252)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(26, 16)
        Me.Label51.TabIndex = 1392
        Me.Label51.Text = "TK"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label50.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label50.Location = New System.Drawing.Point(775, 224)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(26, 16)
        Me.Label50.TabIndex = 1391
        Me.Label50.Text = "TK"
        '
        'txtWasteTK
        '
        Me.txtWasteTK.BackColor = System.Drawing.Color.Linen
        Me.txtWasteTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWasteTK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWasteTK.Location = New System.Drawing.Point(717, 250)
        Me.txtWasteTK.MaxLength = 6
        Me.txtWasteTK.Name = "txtWasteTK"
        Me.txtWasteTK.ReadOnly = True
        Me.txtWasteTK.Size = New System.Drawing.Size(57, 21)
        Me.txtWasteTK.TabIndex = 1390
        Me.txtWasteTK.TabStop = False
        Me.txtWasteTK.Text = "0"
        Me.txtWasteTK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtItemTK
        '
        Me.txtItemTK.BackColor = System.Drawing.Color.Linen
        Me.txtItemTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemTK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemTK.Location = New System.Drawing.Point(717, 221)
        Me.txtItemTK.MaxLength = 6
        Me.txtItemTK.Name = "txtItemTK"
        Me.txtItemTK.ReadOnly = True
        Me.txtItemTK.Size = New System.Drawing.Size(57, 21)
        Me.txtItemTK.TabIndex = 18
        Me.txtItemTK.TabStop = False
        Me.txtItemTK.Text = "0"
        Me.txtItemTK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtItemK
        '
        Me.txtItemK.BackColor = System.Drawing.Color.Linen
        Me.txtItemK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemK.Location = New System.Drawing.Point(461, 221)
        Me.txtItemK.MaxLength = 3
        Me.txtItemK.Name = "txtItemK"
        Me.txtItemK.ReadOnly = True
        Me.txtItemK.Size = New System.Drawing.Size(35, 21)
        Me.txtItemK.TabIndex = 15
        Me.txtItemK.TabStop = False
        Me.txtItemK.Text = "0"
        Me.txtItemK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label23.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label23.Location = New System.Drawing.Point(497, 224)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(18, 16)
        Me.Label23.TabIndex = 1382
        Me.Label23.Text = "K"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(554, 225)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 16)
        Me.Label2.TabIndex = 1376
        Me.Label2.Text = "P"
        '
        'txtItemP
        '
        Me.txtItemP.BackColor = System.Drawing.Color.Linen
        Me.txtItemP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemP.Location = New System.Drawing.Point(515, 221)
        Me.txtItemP.MaxLength = 2
        Me.txtItemP.Name = "txtItemP"
        Me.txtItemP.ReadOnly = True
        Me.txtItemP.Size = New System.Drawing.Size(37, 21)
        Me.txtItemP.TabIndex = 16
        Me.txtItemP.TabStop = False
        Me.txtItemP.Text = "0"
        Me.txtItemP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(607, 225)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 16)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Y"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label24.Location = New System.Drawing.Point(497, 251)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(18, 16)
        Me.Label24.TabIndex = 1384
        Me.Label24.Text = "K"
        '
        'txtWasteK
        '
        Me.txtWasteK.BackColor = System.Drawing.Color.Linen
        Me.txtWasteK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWasteK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWasteK.Location = New System.Drawing.Point(461, 248)
        Me.txtWasteK.MaxLength = 3
        Me.txtWasteK.Name = "txtWasteK"
        Me.txtWasteK.ReadOnly = True
        Me.txtWasteK.Size = New System.Drawing.Size(35, 21)
        Me.txtWasteK.TabIndex = 19
        Me.txtWasteK.TabStop = False
        Me.txtWasteK.Text = "0"
        Me.txtWasteK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label30.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label30.Location = New System.Drawing.Point(554, 251)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(16, 16)
        Me.Label30.TabIndex = 1385
        Me.Label30.Text = "P"
        '
        'txtWasteP
        '
        Me.txtWasteP.BackColor = System.Drawing.Color.Linen
        Me.txtWasteP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWasteP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWasteP.Location = New System.Drawing.Point(515, 249)
        Me.txtWasteP.MaxLength = 2
        Me.txtWasteP.Name = "txtWasteP"
        Me.txtWasteP.ReadOnly = True
        Me.txtWasteP.Size = New System.Drawing.Size(37, 21)
        Me.txtWasteP.TabIndex = 20
        Me.txtWasteP.TabStop = False
        Me.txtWasteP.Text = "0"
        Me.txtWasteP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label32.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label32.Location = New System.Drawing.Point(607, 252)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(18, 16)
        Me.Label32.TabIndex = 1386
        Me.Label32.Text = "Y"
        '
        'txtWasteY
        '
        Me.txtWasteY.BackColor = System.Drawing.Color.Linen
        Me.txtWasteY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWasteY.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWasteY.Location = New System.Drawing.Point(570, 249)
        Me.txtWasteY.MaxLength = 4
        Me.txtWasteY.Name = "txtWasteY"
        Me.txtWasteY.ReadOnly = True
        Me.txtWasteY.Size = New System.Drawing.Size(37, 21)
        Me.txtWasteY.TabIndex = 21
        Me.txtWasteY.TabStop = False
        Me.txtWasteY.Text = "0"
        Me.txtWasteY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtItemTG
        '
        Me.txtItemTG.BackColor = System.Drawing.Color.Linen
        Me.txtItemTG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemTG.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemTG.Location = New System.Drawing.Point(625, 221)
        Me.txtItemTG.MaxLength = 6
        Me.txtItemTG.Name = "txtItemTG"
        Me.txtItemTG.Size = New System.Drawing.Size(56, 21)
        Me.txtItemTG.TabIndex = 18
        Me.txtItemTG.TabStop = False
        Me.txtItemTG.Text = "0"
        Me.txtItemTG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(681, 224)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 16)
        Me.Label6.TabIndex = 1387
        Me.Label6.Text = "Gram"
        '
        'txtWasteTG
        '
        Me.txtWasteTG.BackColor = System.Drawing.Color.Linen
        Me.txtWasteTG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWasteTG.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWasteTG.Location = New System.Drawing.Point(625, 249)
        Me.txtWasteTG.MaxLength = 6
        Me.txtWasteTG.Name = "txtWasteTG"
        Me.txtWasteTG.Size = New System.Drawing.Size(56, 21)
        Me.txtWasteTG.TabIndex = 22
        Me.txtWasteTG.TabStop = False
        Me.txtWasteTG.Text = "0"
        Me.txtWasteTG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(680, 251)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 16)
        Me.Label8.TabIndex = 1388
        Me.Label8.Text = "Gram"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label47.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label47.Location = New System.Drawing.Point(378, 249)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(82, 20)
        Me.Label47.TabIndex = 1373
        Me.Label47.Text = "အလျော့တွက်"
        '
        'LnkTotalNoWaste
        '
        Me.LnkTotalNoWaste.AutoSize = True
        Me.LnkTotalNoWaste.BackColor = System.Drawing.Color.Transparent
        Me.LnkTotalNoWaste.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.LnkTotalNoWaste.Location = New System.Drawing.Point(390, 222)
        Me.LnkTotalNoWaste.Name = "LnkTotalNoWaste"
        Me.LnkTotalNoWaste.Size = New System.Drawing.Size(70, 20)
        Me.LnkTotalNoWaste.TabIndex = 13
        Me.LnkTotalNoWaste.TabStop = True
        Me.LnkTotalNoWaste.Text = "အထည်ချိန်"
        '
        'txtQty
        '
        Me.txtQty.BackColor = System.Drawing.Color.White
        Me.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQty.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtQty.Location = New System.Drawing.Point(461, 194)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(71, 21)
        Me.txtQty.TabIndex = 14
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblQTY
        '
        Me.lblQTY.AutoSize = True
        Me.lblQTY.BackColor = System.Drawing.Color.Transparent
        Me.lblQTY.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.lblQTY.Location = New System.Drawing.Point(379, 194)
        Me.lblQTY.Name = "lblQTY"
        Me.lblQTY.Size = New System.Drawing.Size(81, 20)
        Me.lblQTY.TabIndex = 1395
        Me.lblQTY.Text = "အရေအတွက်"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.Color.Transparent
        Me.Label52.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label52.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label52.Location = New System.Drawing.Point(775, 283)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(26, 16)
        Me.Label52.TabIndex = 1406
        Me.Label52.Text = "TK"
        '
        'txtTotalTK
        '
        Me.txtTotalTK.BackColor = System.Drawing.Color.Linen
        Me.txtTotalTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalTK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTK.Location = New System.Drawing.Point(717, 281)
        Me.txtTotalTK.MaxLength = 6
        Me.txtTotalTK.Name = "txtTotalTK"
        Me.txtTotalTK.ReadOnly = True
        Me.txtTotalTK.Size = New System.Drawing.Size(57, 21)
        Me.txtTotalTK.TabIndex = 1405
        Me.txtTotalTK.TabStop = False
        Me.txtTotalTK.Text = "0"
        Me.txtTotalTK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(497, 282)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(18, 16)
        Me.Label4.TabIndex = 1400
        Me.Label4.Text = "K"
        '
        'txtTotalK
        '
        Me.txtTotalK.BackColor = System.Drawing.Color.Linen
        Me.txtTotalK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalK.Location = New System.Drawing.Point(461, 278)
        Me.txtTotalK.MaxLength = 3
        Me.txtTotalK.Name = "txtTotalK"
        Me.txtTotalK.ReadOnly = True
        Me.txtTotalK.Size = New System.Drawing.Size(35, 21)
        Me.txtTotalK.TabIndex = 21
        Me.txtTotalK.TabStop = False
        Me.txtTotalK.Text = "0"
        Me.txtTotalK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(554, 281)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 16)
        Me.Label5.TabIndex = 1401
        Me.Label5.Text = "P"
        '
        'txtTotalP
        '
        Me.txtTotalP.BackColor = System.Drawing.Color.Linen
        Me.txtTotalP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalP.Location = New System.Drawing.Point(515, 279)
        Me.txtTotalP.MaxLength = 2
        Me.txtTotalP.Name = "txtTotalP"
        Me.txtTotalP.ReadOnly = True
        Me.txtTotalP.Size = New System.Drawing.Size(37, 21)
        Me.txtTotalP.TabIndex = 22
        Me.txtTotalP.TabStop = False
        Me.txtTotalP.Text = "0"
        Me.txtTotalP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label17.Location = New System.Drawing.Point(607, 282)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(18, 16)
        Me.Label17.TabIndex = 1402
        Me.Label17.Text = "Y"
        '
        'txtTotalY
        '
        Me.txtTotalY.BackColor = System.Drawing.Color.Linen
        Me.txtTotalY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalY.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalY.Location = New System.Drawing.Point(570, 278)
        Me.txtTotalY.MaxLength = 4
        Me.txtTotalY.Name = "txtTotalY"
        Me.txtTotalY.ReadOnly = True
        Me.txtTotalY.Size = New System.Drawing.Size(37, 21)
        Me.txtTotalY.TabIndex = 23
        Me.txtTotalY.TabStop = False
        Me.txtTotalY.Text = "0"
        Me.txtTotalY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTotalTG
        '
        Me.txtTotalTG.BackColor = System.Drawing.Color.Linen
        Me.txtTotalTG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalTG.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTG.Location = New System.Drawing.Point(625, 280)
        Me.txtTotalTG.MaxLength = 6
        Me.txtTotalTG.Name = "txtTotalTG"
        Me.txtTotalTG.ReadOnly = True
        Me.txtTotalTG.Size = New System.Drawing.Size(55, 21)
        Me.txtTotalTG.TabIndex = 24
        Me.txtTotalTG.TabStop = False
        Me.txtTotalTG.Text = "0"
        Me.txtTotalTG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label44.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label44.Location = New System.Drawing.Point(680, 281)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(40, 16)
        Me.Label44.TabIndex = 1404
        Me.Label44.Text = "Gram"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label31.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label31.Location = New System.Drawing.Point(359, 279)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(101, 20)
        Me.Label31.TabIndex = 1396
        Me.Label31.Text = "အလေးချိန်ပေါင်း"
        '
        'txtAddSub
        '
        Me.txtAddSub.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAddSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddSub.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtAddSub.Location = New System.Drawing.Point(962, 288)
        Me.txtAddSub.Name = "txtAddSub"
        Me.txtAddSub.ReadOnly = True
        Me.txtAddSub.Size = New System.Drawing.Size(88, 21)
        Me.txtAddSub.TabIndex = 1410
        Me.txtAddSub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label39.Location = New System.Drawing.Point(875, 287)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(84, 20)
        Me.Label39.TabIndex = 1416
        Me.Label39.Text = "ပိုငွေ/လျော့ငွေ"
        '
        'txtNetAmt
        '
        Me.txtNetAmt.BackColor = System.Drawing.Color.White
        Me.txtNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmt.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtNetAmt.Location = New System.Drawing.Point(962, 313)
        Me.txtNetAmt.Name = "txtNetAmt"
        Me.txtNetAmt.Size = New System.Drawing.Size(88, 21)
        Me.txtNetAmt.TabIndex = 23
        Me.txtNetAmt.Text = "0"
        Me.txtNetAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label18.Location = New System.Drawing.Point(831, 312)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(128, 20)
        Me.Label18.TabIndex = 1415
        Me.Label18.Text = "အသားတင်ကျသင့်ငွေ"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmt.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtTotalAmt.Location = New System.Drawing.Point(962, 261)
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.Size = New System.Drawing.Size(88, 21)
        Me.txtTotalAmt.TabIndex = 1411
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label19.Location = New System.Drawing.Point(843, 260)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(116, 20)
        Me.Label19.TabIndex = 1414
        Me.Label19.Text = "စုစုပေါင်းကျသင့်ငွေ"
        '
        'txtSaleFixPrice
        '
        Me.txtSaleFixPrice.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtSaleFixPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleFixPrice.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtSaleFixPrice.Location = New System.Drawing.Point(961, 158)
        Me.txtSaleFixPrice.Name = "txtSaleFixPrice"
        Me.txtSaleFixPrice.ReadOnly = True
        Me.txtSaleFixPrice.Size = New System.Drawing.Size(89, 21)
        Me.txtSaleFixPrice.TabIndex = 26
        Me.txtSaleFixPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPromotionDis
        '
        Me.txtPromotionDis.BackColor = System.Drawing.Color.White
        Me.txtPromotionDis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPromotionDis.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtPromotionDis.Location = New System.Drawing.Point(694, 425)
        Me.txtPromotionDis.Name = "txtPromotionDis"
        Me.txtPromotionDis.Size = New System.Drawing.Size(32, 21)
        Me.txtPromotionDis.TabIndex = 28
        Me.txtPromotionDis.Text = "0"
        Me.txtPromotionDis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPromotionAmt
        '
        Me.txtPromotionAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtPromotionAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPromotionAmt.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtPromotionAmt.Location = New System.Drawing.Point(729, 425)
        Me.txtPromotionAmt.Name = "txtPromotionAmt"
        Me.txtPromotionAmt.ReadOnly = True
        Me.txtPromotionAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtPromotionAmt.TabIndex = 1433
        Me.txtPromotionAmt.Text = "0"
        Me.txtPromotionAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPromotion
        '
        Me.lblPromotion.AutoSize = True
        Me.lblPromotion.BackColor = System.Drawing.Color.Transparent
        Me.lblPromotion.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.lblPromotion.Location = New System.Drawing.Point(604, 425)
        Me.lblPromotion.Name = "lblPromotion"
        Me.lblPromotion.Size = New System.Drawing.Size(91, 19)
        Me.lblPromotion.TabIndex = 1434
        Me.lblPromotion.Text = "Promotion(%)"
        '
        'txtAllAddOrSub
        '
        Me.txtAllAddOrSub.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAllAddOrSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAllAddOrSub.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtAllAddOrSub.Location = New System.Drawing.Point(729, 376)
        Me.txtAllAddOrSub.Name = "txtAllAddOrSub"
        Me.txtAllAddOrSub.ReadOnly = True
        Me.txtAllAddOrSub.Size = New System.Drawing.Size(100, 21)
        Me.txtAllAddOrSub.TabIndex = 1427
        Me.txtAllAddOrSub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label28.Location = New System.Drawing.Point(631, 376)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(84, 20)
        Me.Label28.TabIndex = 1431
        Me.Label28.Text = "ပိုငွေ/လျော့ငွေ"
        '
        'txtAllNetAmt
        '
        Me.txtAllNetAmt.BackColor = System.Drawing.Color.White
        Me.txtAllNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAllNetAmt.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtAllNetAmt.Location = New System.Drawing.Point(729, 452)
        Me.txtAllNetAmt.Name = "txtAllNetAmt"
        Me.txtAllNetAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtAllNetAmt.TabIndex = 27
        Me.txtAllNetAmt.Text = "0"
        Me.txtAllNetAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label41.Location = New System.Drawing.Point(600, 449)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(128, 20)
        Me.Label41.TabIndex = 1430
        Me.Label41.Text = "အသားတင်ကျသင့်ငွေ"
        '
        'txtAllTotalAmt
        '
        Me.txtAllTotalAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAllTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAllTotalAmt.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtAllTotalAmt.Location = New System.Drawing.Point(729, 349)
        Me.txtAllTotalAmt.Name = "txtAllTotalAmt"
        Me.txtAllTotalAmt.ReadOnly = True
        Me.txtAllTotalAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtAllTotalAmt.TabIndex = 1428
        Me.txtAllTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label43.Location = New System.Drawing.Point(599, 349)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(116, 20)
        Me.Label43.TabIndex = 1429
        Me.Label43.Text = "စုစုပေါင်းကျသင့်ငွေ"
        '
        'txtDiscountAmt
        '
        Me.txtDiscountAmt.BackColor = System.Drawing.Color.White
        Me.txtDiscountAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscountAmt.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtDiscountAmt.Location = New System.Drawing.Point(729, 400)
        Me.txtDiscountAmt.Name = "txtDiscountAmt"
        Me.txtDiscountAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtDiscountAmt.TabIndex = 26
        Me.txtDiscountAmt.Text = "0"
        Me.txtDiscountAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.Color.Transparent
        Me.Label113.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label113.Location = New System.Drawing.Point(600, 400)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(115, 20)
        Me.Label113.TabIndex = 1425
        Me.Label113.Text = "Discount လျှော့ငွေ"
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.BackColor = System.Drawing.Color.Transparent
        Me.Label79.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label79.Location = New System.Drawing.Point(890, 452)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(51, 20)
        Me.Label79.TabIndex = 1423
        Me.Label79.Text = "ကျန်ငွေ"
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtBalanceAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmt.Location = New System.Drawing.Point(942, 450)
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReadOnly = True
        Me.txtBalanceAmt.Size = New System.Drawing.Size(115, 21)
        Me.txtBalanceAmt.TabIndex = 1421
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPaidAmt
        '
        Me.txtPaidAmt.BackColor = System.Drawing.Color.White
        Me.txtPaidAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidAmt.Location = New System.Drawing.Point(942, 424)
        Me.txtPaidAmt.Name = "txtPaidAmt"
        Me.txtPaidAmt.ShortcutsEnabled = False
        Me.txtPaidAmt.Size = New System.Drawing.Size(115, 21)
        Me.txtPaidAmt.TabIndex = 29
        Me.txtPaidAmt.Text = "0"
        Me.txtPaidAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label40.Location = New System.Drawing.Point(895, 425)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(46, 20)
        Me.Label40.TabIndex = 1422
        Me.Label40.Text = "ပေးငွေ"
        '
        'btnPrint
        '
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(782, 47)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(90, 31)
        Me.btnPrint.TabIndex = 30
        Me.btnPrint.Text = "Print "
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'grdDetail
        '
        Me.grdDetail.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.5!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GoldG, Me.SalesRate, Me.DataGridViewTextBoxColumn1})
        Me.grdDetail.Location = New System.Drawing.Point(6, 352)
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RowHeadersWidth = 25
        Me.grdDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdDetail.Size = New System.Drawing.Size(564, 225)
        Me.grdDetail.TabIndex = 1418
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
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn1.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 65
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightBlue
        Me.Panel3.Location = New System.Drawing.Point(-5, 340)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1172, 3)
        Me.Panel3.TabIndex = 1417
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClear.ForeColor = System.Drawing.Color.Navy
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(729, 306)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 31)
        Me.btnClear.TabIndex = 25
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.ForeColor = System.Drawing.Color.Navy
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(648, 306)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(72, 31)
        Me.btnAdd.TabIndex = 24
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblWeight
        '
        Me.lblWeight.AutoSize = True
        Me.lblWeight.BackColor = System.Drawing.Color.Transparent
        Me.lblWeight.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblWeight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblWeight.Location = New System.Drawing.Point(394, 158)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(39, 17)
        Me.lblWeight.TabIndex = 1438
        Me.lblWeight.Text = "Kyat"
        '
        'chkSaleFix
        '
        Me.chkSaleFix.AutoSize = True
        Me.chkSaleFix.BackColor = System.Drawing.Color.Transparent
        Me.chkSaleFix.Font = New System.Drawing.Font("Zawgyi-One", 9.0!)
        Me.chkSaleFix.Location = New System.Drawing.Point(1090, 6)
        Me.chkSaleFix.Name = "chkSaleFix"
        Me.chkSaleFix.Size = New System.Drawing.Size(57, 24)
        Me.chkSaleFix.TabIndex = 1439
        Me.chkSaleFix.Text = "ဒံုးေစ်း"
        Me.chkSaleFix.UseVisualStyleBackColor = False
        Me.chkSaleFix.Visible = False
        '
        'txtGoldPrice
        '
        Me.txtGoldPrice.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtGoldPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldPrice.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtGoldPrice.Location = New System.Drawing.Point(961, 184)
        Me.txtGoldPrice.Name = "txtGoldPrice"
        Me.txtGoldPrice.ReadOnly = True
        Me.txtGoldPrice.Size = New System.Drawing.Size(89, 21)
        Me.txtGoldPrice.TabIndex = 1440
        Me.txtGoldPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label27.Location = New System.Drawing.Point(920, 185)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(39, 20)
        Me.Label27.TabIndex = 1441
        Me.Label27.Text = "ရွှေဖိုး"
        '
        'lblIsGram
        '
        Me.lblIsGram.AutoSize = True
        Me.lblIsGram.BackColor = System.Drawing.Color.Transparent
        Me.lblIsGram.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblIsGram.Location = New System.Drawing.Point(249, 313)
        Me.lblIsGram.Name = "lblIsGram"
        Me.lblIsGram.Size = New System.Drawing.Size(67, 17)
        Me.lblIsGram.TabIndex = 1442
        Me.lblIsGram.Text = "၁ ဂရမ်စျေး"
        '
        'lblDonePrice
        '
        Me.lblDonePrice.AutoSize = True
        Me.lblDonePrice.BackColor = System.Drawing.Color.Transparent
        Me.lblDonePrice.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.lblDonePrice.Location = New System.Drawing.Point(913, 158)
        Me.lblDonePrice.Name = "lblDonePrice"
        Me.lblDonePrice.Size = New System.Drawing.Size(46, 20)
        Me.lblDonePrice.TabIndex = 25
        Me.lblDonePrice.Text = "ဒုံးစျေး"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(38, 157)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 17)
        Me.Label12.TabIndex = 1443
        Me.Label12.Text = "Barcode No"
        '
        'SearchItem
        '
        Me.SearchItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchItem.Image = CType(resources.GetObject("SearchItem.Image"), System.Drawing.Image)
        Me.SearchItem.Location = New System.Drawing.Point(260, 155)
        Me.SearchItem.Name = "SearchItem"
        Me.SearchItem.Size = New System.Drawing.Size(38, 21)
        Me.SearchItem.TabIndex = 12
        Me.SearchItem.UseVisualStyleBackColor = True
        '
        'lblLogInUserName
        '
        Me.lblLogInUserName.AutoSize = True
        Me.lblLogInUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblLogInUserName.Font = New System.Drawing.Font("Myanmar3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogInUserName.ForeColor = System.Drawing.Color.Violet
        Me.lblLogInUserName.Location = New System.Drawing.Point(915, 7)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(90, 22)
        Me.lblLogInUserName.TabIndex = 1445
        Me.lblLogInUserName.Text = "LogInUser"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel1.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.LinkLabel1.Location = New System.Drawing.Point(391, 10)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(47, 20)
        Me.LinkLabel1.TabIndex = 1446
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "ဝယ်သူ"
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1154, -2)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 1453
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label14.Location = New System.Drawing.Point(871, 403)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 20)
        Me.Label14.TabIndex = 1466
        Me.Label14.Text = "ခြားနားငွေ"
        '
        'txtDifferentAmount
        '
        Me.txtDifferentAmount.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtDifferentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDifferentAmount.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtDifferentAmount.Location = New System.Drawing.Point(942, 399)
        Me.txtDifferentAmount.Name = "txtDifferentAmount"
        Me.txtDifferentAmount.ReadOnly = True
        Me.txtDifferentAmount.Size = New System.Drawing.Size(115, 21)
        Me.txtDifferentAmount.TabIndex = 1465
        Me.txtDifferentAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPurchaseAmount
        '
        Me.txtPurchaseAmount.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtPurchaseAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseAmount.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtPurchaseAmount.Location = New System.Drawing.Point(942, 373)
        Me.txtPurchaseAmount.Name = "txtPurchaseAmount"
        Me.txtPurchaseAmount.ReadOnly = True
        Me.txtPurchaseAmount.ShortcutsEnabled = False
        Me.txtPurchaseAmount.Size = New System.Drawing.Size(115, 21)
        Me.txtPurchaseAmount.TabIndex = 1463
        Me.txtPurchaseAmount.Text = "0"
        Me.txtPurchaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label13.Location = New System.Drawing.Point(858, 377)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(83, 20)
        Me.Label13.TabIndex = 1464
        Me.Label13.Text = "လဲခြင်းမှရငွေ"
        '
        'btnSearchPurchase
        '
        Me.btnSearchPurchase.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearchPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchPurchase.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchPurchase.Image = CType(resources.GetObject("btnSearchPurchase.Image"), System.Drawing.Image)
        Me.btnSearchPurchase.Location = New System.Drawing.Point(1064, 374)
        Me.btnSearchPurchase.Name = "btnSearchPurchase"
        Me.btnSearchPurchase.Size = New System.Drawing.Size(34, 21)
        Me.btnSearchPurchase.TabIndex = 1462
        Me.btnSearchPurchase.UseVisualStyleBackColor = False
        '
        'txtPurchaseVoucherNo
        '
        Me.txtPurchaseVoucherNo.BackColor = System.Drawing.Color.White
        Me.txtPurchaseVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseVoucherNo.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtPurchaseVoucherNo.Location = New System.Drawing.Point(942, 348)
        Me.txtPurchaseVoucherNo.Name = "txtPurchaseVoucherNo"
        Me.txtPurchaseVoucherNo.ShortcutsEnabled = False
        Me.txtPurchaseVoucherNo.Size = New System.Drawing.Size(115, 21)
        Me.txtPurchaseVoucherNo.TabIndex = 28
        Me.txtPurchaseVoucherNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label11.Location = New System.Drawing.Point(831, 350)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(110, 20)
        Me.Label11.TabIndex = 1461
        Me.Label11.Text = "လဲခြင်းဘောက်ချာ"
        '
        'txtDesignCharges
        '
        Me.txtDesignCharges.BackColor = System.Drawing.Color.White
        Me.txtDesignCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesignCharges.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtDesignCharges.Location = New System.Drawing.Point(962, 236)
        Me.txtDesignCharges.Name = "txtDesignCharges"
        Me.txtDesignCharges.Size = New System.Drawing.Size(88, 21)
        Me.txtDesignCharges.TabIndex = 1467
        Me.txtDesignCharges.Text = "0"
        Me.txtDesignCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label15.Location = New System.Drawing.Point(912, 235)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 20)
        Me.Label15.TabIndex = 1468
        Me.Label15.Text = "လက်ခ"
        '
        'ChkIsSolidVolume
        '
        Me.ChkIsSolidVolume.AutoSize = True
        Me.ChkIsSolidVolume.BackColor = System.Drawing.Color.Lavender
        Me.ChkIsSolidVolume.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ChkIsSolidVolume.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ChkIsSolidVolume.Location = New System.Drawing.Point(684, 157)
        Me.ChkIsSolidVolume.Name = "ChkIsSolidVolume"
        Me.ChkIsSolidVolume.Size = New System.Drawing.Size(115, 21)
        Me.ChkIsSolidVolume.TabIndex = 1469
        Me.ChkIsSolidVolume.Text = "Solid Volume"
        Me.ChkIsSolidVolume.UseVisualStyleBackColor = False
        '
        'txtDesignRate
        '
        Me.txtDesignRate.BackColor = System.Drawing.Color.White
        Me.txtDesignRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesignRate.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtDesignRate.Location = New System.Drawing.Point(962, 211)
        Me.txtDesignRate.Name = "txtDesignRate"
        Me.txtDesignRate.Size = New System.Drawing.Size(88, 21)
        Me.txtDesignRate.TabIndex = 1470
        Me.txtDesignRate.Text = "0"
        Me.txtDesignRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label16.Location = New System.Drawing.Point(891, 210)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(70, 20)
        Me.Label16.TabIndex = 1471
        Me.Label16.Text = "လက်ခနှုန်း"
        '
        'txtMemberID
        '
        Me.txtMemberID.BackColor = System.Drawing.Color.Linen
        Me.txtMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberID.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberID.Location = New System.Drawing.Point(767, 95)
        Me.txtMemberID.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberID.Name = "txtMemberID"
        Me.txtMemberID.ReadOnly = True
        Me.txtMemberID.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberID.TabIndex = 1651
        '
        'txtMemberName
        '
        Me.txtMemberName.BackColor = System.Drawing.Color.Linen
        Me.txtMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberName.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberName.Location = New System.Drawing.Point(608, 95)
        Me.txtMemberName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberName.Name = "txtMemberName"
        Me.txtMemberName.ReadOnly = True
        Me.txtMemberName.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberName.TabIndex = 1650
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BackColor = System.Drawing.Color.Linen
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.Location = New System.Drawing.Point(445, 95)
        Me.txtMemberCode.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.ReadOnly = True
        Me.txtMemberCode.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberCode.TabIndex = 1649
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
        Me.grpMember.Location = New System.Drawing.Point(708, 485)
        Me.grpMember.Margin = New System.Windows.Forms.Padding(2)
        Me.grpMember.Name = "grpMember"
        Me.grpMember.Padding = New System.Windows.Forms.Padding(2)
        Me.grpMember.Size = New System.Drawing.Size(349, 101)
        Me.grpMember.TabIndex = 1652
        Me.grpMember.TabStop = False
        Me.grpMember.Text = "Redeem"
        '
        'lblRedeemItem
        '
        Me.lblRedeemItem.AutoSize = True
        Me.lblRedeemItem.Location = New System.Drawing.Point(207, 19)
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
        Me.txtMemberDis.Location = New System.Drawing.Point(104, 70)
        Me.txtMemberDis.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberDis.Name = "txtMemberDis"
        Me.txtMemberDis.Size = New System.Drawing.Size(32, 21)
        Me.txtMemberDis.TabIndex = 1619
        Me.txtMemberDis.Text = "0"
        Me.txtMemberDis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPointBalance
        '
        Me.lblPointBalance.AutoSize = True
        Me.lblPointBalance.Location = New System.Drawing.Point(300, 43)
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
        Me.txtMemberDisAmt.Location = New System.Drawing.Point(135, 70)
        Me.txtMemberDisAmt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberDisAmt.Name = "txtMemberDisAmt"
        Me.txtMemberDisAmt.Size = New System.Drawing.Size(69, 21)
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
        Me.Label72.Location = New System.Drawing.Point(3, 72)
        Me.Label72.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(103, 19)
        Me.Label72.TabIndex = 1620
        Me.Label72.Text = "Member Dis(%)"
        '
        'lblPoint
        '
        Me.lblPoint.AutoSize = True
        Me.lblPoint.Location = New System.Drawing.Point(207, 43)
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
        Me.Label73.Location = New System.Drawing.Point(3, 45)
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
        Me.btnRedeem.Location = New System.Drawing.Point(207, 65)
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
        Me.txtPoint.Size = New System.Drawing.Size(100, 21)
        Me.txtPoint.TabIndex = 1610
        Me.txtPoint.Text = "0"
        Me.txtPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValue
        '
        Me.txtValue.BackColor = System.Drawing.Color.White
        Me.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValue.Location = New System.Drawing.Point(105, 41)
        Me.txtValue.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(100, 21)
        Me.txtValue.TabIndex = 1608
        Me.txtValue.Text = "0"
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnRedeemClear
        '
        Me.btnRedeemClear.Location = New System.Drawing.Point(279, 65)
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
        Me.dtpExpireDate.Location = New System.Drawing.Point(887, 66)
        Me.dtpExpireDate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtpExpireDate.Name = "dtpExpireDate"
        Me.dtpExpireDate.Size = New System.Drawing.Size(54, 21)
        Me.dtpExpireDate.TabIndex = 1653
        Me.dtpExpireDate.Visible = False
        '
        'frm_SaleVolumeInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 637)
        Me.Controls.Add(Me.dtpExpireDate)
        Me.Controls.Add(Me.grpMember)
        Me.Controls.Add(Me.txtMemberID)
        Me.Controls.Add(Me.txtMemberName)
        Me.Controls.Add(Me.txtMemberCode)
        Me.Controls.Add(Me.txtDesignRate)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.ChkIsSolidVolume)
        Me.Controls.Add(Me.txtDesignCharges)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtDifferentAmount)
        Me.Controls.Add(Me.txtPurchaseAmount)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnSearchPurchase)
        Me.Controls.Add(Me.txtPurchaseVoucherNo)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.SearchItem)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblDonePrice)
        Me.Controls.Add(Me.lblIsGram)
        Me.Controls.Add(Me.txtGoldPrice)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.chkSaleFix)
        Me.Controls.Add(Me.lblWeight)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtPromotionDis)
        Me.Controls.Add(Me.txtPromotionAmt)
        Me.Controls.Add(Me.lblPromotion)
        Me.Controls.Add(Me.txtAllAddOrSub)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.txtAllNetAmt)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.txtAllTotalAmt)
        Me.Controls.Add(Me.Label43)
        Me.Controls.Add(Me.txtDiscountAmt)
        Me.Controls.Add(Me.Label113)
        Me.Controls.Add(Me.Label79)
        Me.Controls.Add(Me.txtBalanceAmt)
        Me.Controls.Add(Me.txtPaidAmt)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.grdDetail)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.txtAddSub)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.txtNetAmt)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtTotalAmt)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtSaleFixPrice)
        Me.Controls.Add(Me.Label52)
        Me.Controls.Add(Me.txtTotalTK)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtTotalK)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtTotalP)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtTotalY)
        Me.Controls.Add(Me.txtTotalTG)
        Me.Controls.Add(Me.Label44)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.lblQTY)
        Me.Controls.Add(Me.txtItemY)
        Me.Controls.Add(Me.Label51)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.txtWasteTK)
        Me.Controls.Add(Me.txtItemTK)
        Me.Controls.Add(Me.txtItemK)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtItemP)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtWasteK)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.txtWasteP)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.txtWasteY)
        Me.Controls.Add(Me.txtItemTG)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtWasteTG)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label47)
        Me.Controls.Add(Me.LnkTotalNoWaste)
        Me.Controls.Add(Me.txtCurrentPrice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.Label42)
        Me.Controls.Add(Me.chkIsFixPrice)
        Me.Controls.Add(Me.txtLength)
        Me.Controls.Add(Me.txtGoldQuality)
        Me.Controls.Add(Me.txtItemCategory)
        Me.Controls.Add(Me.txtItemName)
        Me.Controls.Add(Me.txtBarcodeNo)
        Me.Controls.Add(Me.SearchSale)
        Me.Controls.Add(Me.txtCustomerCode)
        Me.Controls.Add(Me.btnCustomer)
        Me.Controls.Add(Me.txtCustomer)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.Label46)
        Me.Controls.Add(Me.lblCurrentLocationName)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpSaleDate)
        Me.Controls.Add(Me.txtSalesVolumeID)
        Me.Controls.Add(Me.Label9)
        Me.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.KeyPreview = True
        Me.Name = "frm_SaleVolumeInvoice"
        Me.Text = "SaleVolume Invoice"
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtSalesVolumeID, 0)
        Me.Controls.SetChildIndex(Me.dtpSaleDate, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.cboStaff, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.txtRemark, 0)
        Me.Controls.SetChildIndex(Me.Label29, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.lblCurrentLocationName, 0)
        Me.Controls.SetChildIndex(Me.Label46, 0)
        Me.Controls.SetChildIndex(Me.txtAddress, 0)
        Me.Controls.SetChildIndex(Me.txtCustomer, 0)
        Me.Controls.SetChildIndex(Me.btnCustomer, 0)
        Me.Controls.SetChildIndex(Me.txtCustomerCode, 0)
        Me.Controls.SetChildIndex(Me.SearchSale, 0)
        Me.Controls.SetChildIndex(Me.txtBarcodeNo, 0)
        Me.Controls.SetChildIndex(Me.txtItemName, 0)
        Me.Controls.SetChildIndex(Me.txtItemCategory, 0)
        Me.Controls.SetChildIndex(Me.txtGoldQuality, 0)
        Me.Controls.SetChildIndex(Me.txtLength, 0)
        Me.Controls.SetChildIndex(Me.chkIsFixPrice, 0)
        Me.Controls.SetChildIndex(Me.Label42, 0)
        Me.Controls.SetChildIndex(Me.Label35, 0)
        Me.Controls.SetChildIndex(Me.Label34, 0)
        Me.Controls.SetChildIndex(Me.Label33, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtCurrentPrice, 0)
        Me.Controls.SetChildIndex(Me.LnkTotalNoWaste, 0)
        Me.Controls.SetChildIndex(Me.Label47, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.txtWasteTG, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.txtItemTG, 0)
        Me.Controls.SetChildIndex(Me.txtWasteY, 0)
        Me.Controls.SetChildIndex(Me.Label32, 0)
        Me.Controls.SetChildIndex(Me.txtWasteP, 0)
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.txtWasteK, 0)
        Me.Controls.SetChildIndex(Me.Label24, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtItemP, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label23, 0)
        Me.Controls.SetChildIndex(Me.txtItemK, 0)
        Me.Controls.SetChildIndex(Me.txtItemTK, 0)
        Me.Controls.SetChildIndex(Me.txtWasteTK, 0)
        Me.Controls.SetChildIndex(Me.Label50, 0)
        Me.Controls.SetChildIndex(Me.Label51, 0)
        Me.Controls.SetChildIndex(Me.txtItemY, 0)
        Me.Controls.SetChildIndex(Me.lblQTY, 0)
        Me.Controls.SetChildIndex(Me.txtQty, 0)
        Me.Controls.SetChildIndex(Me.Label31, 0)
        Me.Controls.SetChildIndex(Me.Label44, 0)
        Me.Controls.SetChildIndex(Me.txtTotalTG, 0)
        Me.Controls.SetChildIndex(Me.txtTotalY, 0)
        Me.Controls.SetChildIndex(Me.Label17, 0)
        Me.Controls.SetChildIndex(Me.txtTotalP, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.txtTotalK, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtTotalTK, 0)
        Me.Controls.SetChildIndex(Me.Label52, 0)
        Me.Controls.SetChildIndex(Me.txtSaleFixPrice, 0)
        Me.Controls.SetChildIndex(Me.Label19, 0)
        Me.Controls.SetChildIndex(Me.txtTotalAmt, 0)
        Me.Controls.SetChildIndex(Me.Label18, 0)
        Me.Controls.SetChildIndex(Me.txtNetAmt, 0)
        Me.Controls.SetChildIndex(Me.Label39, 0)
        Me.Controls.SetChildIndex(Me.txtAddSub, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.grdDetail, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.Label40, 0)
        Me.Controls.SetChildIndex(Me.txtPaidAmt, 0)
        Me.Controls.SetChildIndex(Me.txtBalanceAmt, 0)
        Me.Controls.SetChildIndex(Me.Label79, 0)
        Me.Controls.SetChildIndex(Me.Label113, 0)
        Me.Controls.SetChildIndex(Me.txtDiscountAmt, 0)
        Me.Controls.SetChildIndex(Me.Label43, 0)
        Me.Controls.SetChildIndex(Me.txtAllTotalAmt, 0)
        Me.Controls.SetChildIndex(Me.Label41, 0)
        Me.Controls.SetChildIndex(Me.txtAllNetAmt, 0)
        Me.Controls.SetChildIndex(Me.Label28, 0)
        Me.Controls.SetChildIndex(Me.txtAllAddOrSub, 0)
        Me.Controls.SetChildIndex(Me.lblPromotion, 0)
        Me.Controls.SetChildIndex(Me.txtPromotionAmt, 0)
        Me.Controls.SetChildIndex(Me.txtPromotionDis, 0)
        Me.Controls.SetChildIndex(Me.btnAdd, 0)
        Me.Controls.SetChildIndex(Me.btnClear, 0)
        Me.Controls.SetChildIndex(Me.lblWeight, 0)
        Me.Controls.SetChildIndex(Me.chkSaleFix, 0)
        Me.Controls.SetChildIndex(Me.Label27, 0)
        Me.Controls.SetChildIndex(Me.txtGoldPrice, 0)
        Me.Controls.SetChildIndex(Me.lblIsGram, 0)
        Me.Controls.SetChildIndex(Me.lblDonePrice, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.SearchItem, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.txtPurchaseVoucherNo, 0)
        Me.Controls.SetChildIndex(Me.btnSearchPurchase, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.txtPurchaseAmount, 0)
        Me.Controls.SetChildIndex(Me.txtDifferentAmount, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.txtDesignCharges, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSolidVolume, 0)
        Me.Controls.SetChildIndex(Me.Label16, 0)
        Me.Controls.SetChildIndex(Me.txtDesignRate, 0)
        Me.Controls.SetChildIndex(Me.txtMemberCode, 0)
        Me.Controls.SetChildIndex(Me.txtMemberName, 0)
        Me.Controls.SetChildIndex(Me.txtMemberID, 0)
        Me.Controls.SetChildIndex(Me.grpMember, 0)
        Me.Controls.SetChildIndex(Me.dtpExpireDate, 0)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMember.ResumeLayout(False)
        Me.grpMember.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SearchSale As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentLocationName As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpSaleDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSalesVolumeID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCurrentPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents chkIsFixPrice As System.Windows.Forms.CheckBox
    Friend WithEvents txtLength As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldQuality As System.Windows.Forms.TextBox
    Friend WithEvents txtItemCategory As System.Windows.Forms.TextBox
    Friend WithEvents txtItemName As System.Windows.Forms.TextBox
    Friend WithEvents txtBarcodeNo As System.Windows.Forms.TextBox
    Friend WithEvents txtItemY As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents txtWasteTK As System.Windows.Forms.TextBox
    Friend WithEvents txtItemTK As System.Windows.Forms.TextBox
    Friend WithEvents txtItemK As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtItemP As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtWasteK As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtWasteP As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtWasteY As System.Windows.Forms.TextBox
    Friend WithEvents txtItemTG As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtWasteTG As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents LnkTotalNoWaste As System.Windows.Forms.LinkLabel
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblQTY As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtTotalTK As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTotalK As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTotalP As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTotalY As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalTG As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtAddSub As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtSaleFixPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtPromotionDis As System.Windows.Forms.TextBox
    Friend WithEvents txtPromotionAmt As System.Windows.Forms.TextBox
    Friend WithEvents lblPromotion As System.Windows.Forms.Label
    Friend WithEvents txtAllAddOrSub As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtAllNetAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtAllTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtDiscountAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtPaidAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents grdDetail As System.Windows.Forms.DataGridView
    Friend WithEvents GoldG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SalesRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    'Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents chkSaleFix As System.Windows.Forms.CheckBox
    Friend WithEvents txtGoldPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblIsGram As System.Windows.Forms.Label
    Friend WithEvents lblDonePrice As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents SearchItem As System.Windows.Forms.Button
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtDifferentAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnSearchPurchase As System.Windows.Forms.Button
    Friend WithEvents txtPurchaseVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDesignCharges As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ChkIsSolidVolume As System.Windows.Forms.CheckBox
    Friend WithEvents txtDesignRate As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtMemberID As System.Windows.Forms.TextBox
    Friend WithEvents txtMemberName As System.Windows.Forms.TextBox
    Friend WithEvents txtMemberCode As System.Windows.Forms.TextBox
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
End Class
