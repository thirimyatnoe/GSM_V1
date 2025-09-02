<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ConsignmentSale
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ConsignmentSale))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnBroker = New System.Windows.Forms.Button()
        Me.txtBrokerName = New System.Windows.Forms.TextBox()
        Me.txtBrokerCode = New System.Windows.Forms.TextBox()
        Me.txtBalanceAmt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPaidAmt = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtAddOrSubAmt = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNetAmt = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtTotalAmt = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lnkCustomer = New System.Windows.Forms.LinkLabel()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.dtpConsignDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSearchConsign = New System.Windows.Forms.Button()
        Me.txtConsignmentSaleID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnWholeSale = New System.Windows.Forms.Button()
        Me.txtWholeSaleID = New System.Windows.Forms.TextBox()
        Me.lblWholeSale = New System.Windows.Forms.Label()
        Me.optPay = New System.Windows.Forms.RadioButton()
        Me.optDirectSale = New System.Windows.Forms.RadioButton()
        Me.txtDiscountAmt = New System.Windows.Forms.TextBox()
        Me.lnkB = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblCurrentLocationName = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtDifferentAmount = New System.Windows.Forms.TextBox()
        Me.txtPurchaseAmount = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnSearchPurchase = New System.Windows.Forms.Button()
        Me.txtPurchaseVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label6.Location = New System.Drawing.Point(80, 483)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 19)
        Me.Label6.TabIndex = 991
        Me.Label6.Text = "Discount လျှော့ငွေ"
        '
        'btnBroker
        '
        Me.btnBroker.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBroker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBroker.Image = CType(resources.GetObject("btnBroker.Image"), System.Drawing.Image)
        Me.btnBroker.Location = New System.Drawing.Point(1245, 88)
        Me.btnBroker.Name = "btnBroker"
        Me.btnBroker.Size = New System.Drawing.Size(30, 23)
        Me.btnBroker.TabIndex = 12
        Me.btnBroker.UseVisualStyleBackColor = True
        Me.btnBroker.Visible = False
        '
        'txtBrokerName
        '
        Me.txtBrokerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBrokerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBrokerName.Location = New System.Drawing.Point(1039, 87)
        Me.txtBrokerName.Name = "txtBrokerName"
        Me.txtBrokerName.Size = New System.Drawing.Size(200, 20)
        Me.txtBrokerName.TabIndex = 954
        Me.txtBrokerName.Visible = False
        '
        'txtBrokerCode
        '
        Me.txtBrokerCode.BackColor = System.Drawing.Color.White
        Me.txtBrokerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBrokerCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBrokerCode.Location = New System.Drawing.Point(978, 87)
        Me.txtBrokerCode.Name = "txtBrokerCode"
        Me.txtBrokerCode.ReadOnly = True
        Me.txtBrokerCode.Size = New System.Drawing.Size(60, 20)
        Me.txtBrokerCode.TabIndex = 953
        Me.txtBrokerCode.Visible = False
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtBalanceAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmt.Location = New System.Drawing.Point(434, 557)
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReadOnly = True
        Me.txtBalanceAmt.Size = New System.Drawing.Size(115, 21)
        Me.txtBalanceAmt.TabIndex = 970
        Me.txtBalanceAmt.Text = "0"
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(388, 556)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 17)
        Me.Label3.TabIndex = 984
        Me.Label3.Text = "ကျန်ငွေ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPaidAmt
        '
        Me.txtPaidAmt.BackColor = System.Drawing.Color.White
        Me.txtPaidAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmt.Location = New System.Drawing.Point(434, 533)
        Me.txtPaidAmt.Name = "txtPaidAmt"
        Me.txtPaidAmt.Size = New System.Drawing.Size(115, 21)
        Me.txtPaidAmt.TabIndex = 17
        Me.txtPaidAmt.Text = "0"
        Me.txtPaidAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(392, 532)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(38, 17)
        Me.Label40.TabIndex = 983
        Me.Label40.Text = "ပေးငွေ"
        '
        'txtAddOrSubAmt
        '
        Me.txtAddOrSubAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAddOrSubAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddOrSubAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddOrSubAmt.Location = New System.Drawing.Point(186, 533)
        Me.txtAddOrSubAmt.Name = "txtAddOrSubAmt"
        Me.txtAddOrSubAmt.ReadOnly = True
        Me.txtAddOrSubAmt.Size = New System.Drawing.Size(114, 21)
        Me.txtAddOrSubAmt.TabIndex = 968
        Me.txtAddOrSubAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label18.Location = New System.Drawing.Point(108, 534)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(79, 19)
        Me.Label18.TabIndex = 982
        Me.Label18.Text = "ပိုငွေ/လျော့ငွေ"
        '
        'txtNetAmt
        '
        Me.txtNetAmt.BackColor = System.Drawing.Color.White
        Me.txtNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAmt.Location = New System.Drawing.Point(186, 508)
        Me.txtNetAmt.Name = "txtNetAmt"
        Me.txtNetAmt.Size = New System.Drawing.Size(114, 21)
        Me.txtNetAmt.TabIndex = 16
        Me.txtNetAmt.Text = "0"
        Me.txtNetAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label19.Location = New System.Drawing.Point(68, 507)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(119, 19)
        Me.Label19.TabIndex = 981
        Me.Label19.Text = "အသားတင်ကျသင့်ငွေ"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmt.Location = New System.Drawing.Point(186, 458)
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.Size = New System.Drawing.Size(114, 21)
        Me.txtTotalAmt.TabIndex = 963
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label20.Location = New System.Drawing.Point(79, 458)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(108, 19)
        Me.Label20.TabIndex = 980
        Me.Label20.Text = "စုစုပေါင်းကျသင့်ငွေ"
        '
        'lnkCustomer
        '
        Me.lnkCustomer.AutoSize = True
        Me.lnkCustomer.BackColor = System.Drawing.Color.Transparent
        Me.lnkCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.lnkCustomer.Location = New System.Drawing.Point(1065, 64)
        Me.lnkCustomer.Name = "lnkCustomer"
        Me.lnkCustomer.Size = New System.Drawing.Size(65, 16)
        Me.lnkCustomer.TabIndex = 10
        Me.lnkCustomer.TabStop = True
        Me.lnkCustomer.Text = "Customer"
        Me.lnkCustomer.Visible = False
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(1167, 21)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(30, 23)
        Me.btnCustomer.TabIndex = 11
        Me.btnCustomer.UseVisualStyleBackColor = True
        Me.btnCustomer.Visible = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtCustomerName.Location = New System.Drawing.Point(189, 90)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(200, 26)
        Me.txtCustomerName.TabIndex = 951
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(128, 90)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReadOnly = True
        Me.txtCustomerCode.Size = New System.Drawing.Size(60, 26)
        Me.txtCustomerCode.TabIndex = 950
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label1.Location = New System.Drawing.Point(362, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 16)
        Me.Label1.TabIndex = 978
        Me.Label1.Text = "Staff"
        '
        'cboStaff
        '
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(405, 12)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(194, 25)
        Me.cboStaff.TabIndex = 9
        '
        'dtpConsignDate
        '
        Me.dtpConsignDate.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpConsignDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpConsignDate.Location = New System.Drawing.Point(129, 37)
        Me.dtpConsignDate.Name = "dtpConsignDate"
        Me.dtpConsignDate.Size = New System.Drawing.Size(152, 20)
        Me.dtpConsignDate.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label10.Location = New System.Drawing.Point(89, 36)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 16)
        Me.Label10.TabIndex = 976
        Me.Label10.Text = "Date"
        '
        'btnSearchConsign
        '
        Me.btnSearchConsign.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchConsign.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchConsign.Image = CType(resources.GetObject("btnSearchConsign.Image"), System.Drawing.Image)
        Me.btnSearchConsign.Location = New System.Drawing.Point(251, 12)
        Me.btnSearchConsign.Name = "btnSearchConsign"
        Me.btnSearchConsign.Size = New System.Drawing.Size(30, 21)
        Me.btnSearchConsign.TabIndex = 4
        Me.btnSearchConsign.UseVisualStyleBackColor = True
        '
        'txtConsignmentSaleID
        '
        Me.txtConsignmentSaleID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtConsignmentSaleID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConsignmentSaleID.Location = New System.Drawing.Point(129, 12)
        Me.txtConsignmentSaleID.Name = "txtConsignmentSaleID"
        Me.txtConsignmentSaleID.ReadOnly = True
        Me.txtConsignmentSaleID.Size = New System.Drawing.Size(119, 20)
        Me.txtConsignmentSaleID.TabIndex = 946
        Me.txtConsignmentSaleID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(28, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 13)
        Me.Label9.TabIndex = 975
        Me.Label9.Text = "Consignment ID"
        '
        'btnWholeSale
        '
        Me.btnWholeSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWholeSale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWholeSale.Image = CType(resources.GetObject("btnWholeSale.Image"), System.Drawing.Image)
        Me.btnWholeSale.Location = New System.Drawing.Point(251, 63)
        Me.btnWholeSale.Name = "btnWholeSale"
        Me.btnWholeSale.Size = New System.Drawing.Size(30, 21)
        Me.btnWholeSale.TabIndex = 8
        Me.btnWholeSale.UseVisualStyleBackColor = True
        '
        'txtWholeSaleID
        '
        Me.txtWholeSaleID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWholeSaleID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWholeSaleID.Location = New System.Drawing.Point(129, 63)
        Me.txtWholeSaleID.Name = "txtWholeSaleID"
        Me.txtWholeSaleID.ReadOnly = True
        Me.txtWholeSaleID.Size = New System.Drawing.Size(119, 20)
        Me.txtWholeSaleID.TabIndex = 992
        Me.txtWholeSaleID.TabStop = False
        '
        'lblWholeSale
        '
        Me.lblWholeSale.AutoSize = True
        Me.lblWholeSale.BackColor = System.Drawing.Color.Transparent
        Me.lblWholeSale.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWholeSale.Location = New System.Drawing.Point(44, 65)
        Me.lblWholeSale.Name = "lblWholeSale"
        Me.lblWholeSale.Size = New System.Drawing.Size(81, 13)
        Me.lblWholeSale.TabIndex = 994
        Me.lblWholeSale.Text = "Wholesale ID"
        '
        'optPay
        '
        Me.optPay.AutoSize = True
        Me.optPay.BackColor = System.Drawing.Color.Transparent
        Me.optPay.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPay.Location = New System.Drawing.Point(1025, 46)
        Me.optPay.Name = "optPay"
        Me.optPay.Size = New System.Drawing.Size(78, 17)
        Me.optPay.TabIndex = 7
        Me.optPay.Text = "From Pay"
        Me.optPay.UseVisualStyleBackColor = False
        Me.optPay.Visible = False
        '
        'optDirectSale
        '
        Me.optDirectSale.AutoSize = True
        Me.optDirectSale.BackColor = System.Drawing.Color.Transparent
        Me.optDirectSale.Checked = True
        Me.optDirectSale.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDirectSale.Location = New System.Drawing.Point(1017, 38)
        Me.optDirectSale.Name = "optDirectSale"
        Me.optDirectSale.Size = New System.Drawing.Size(86, 17)
        Me.optDirectSale.TabIndex = 6
        Me.optDirectSale.TabStop = True
        Me.optDirectSale.Text = "Direct Sale"
        Me.optDirectSale.UseVisualStyleBackColor = False
        Me.optDirectSale.Visible = False
        '
        'txtDiscountAmt
        '
        Me.txtDiscountAmt.BackColor = System.Drawing.Color.White
        Me.txtDiscountAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscountAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscountAmt.Location = New System.Drawing.Point(186, 483)
        Me.txtDiscountAmt.Name = "txtDiscountAmt"
        Me.txtDiscountAmt.Size = New System.Drawing.Size(114, 21)
        Me.txtDiscountAmt.TabIndex = 998
        Me.txtDiscountAmt.Text = "0"
        Me.txtDiscountAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lnkB
        '
        Me.lnkB.AutoSize = True
        Me.lnkB.BackColor = System.Drawing.Color.Transparent
        Me.lnkB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.lnkB.Location = New System.Drawing.Point(925, 90)
        Me.lnkB.Name = "lnkB"
        Me.lnkB.Size = New System.Drawing.Size(48, 16)
        Me.lnkB.TabIndex = 999
        Me.lnkB.TabStop = True
        Me.lnkB.Text = "Broker"
        Me.lnkB.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label2.Location = New System.Drawing.Point(57, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 16)
        Me.Label2.TabIndex = 1000
        Me.Label2.Text = "Customer"
        '
        'btnPrint
        '
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(930, 53)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 31)
        Me.btnPrint.TabIndex = 1360
        Me.btnPrint.Text = "Print "
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Khaki
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(874, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 23)
        Me.Label5.TabIndex = 1370
        Me.Label5.Text = "ရွှေအတင်အလေးချိန်"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(751, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 23)
        Me.Label4.TabIndex = 1369
        Me.Label4.Text = "ကျောက်ချိန်"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LightBlue
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(632, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 23)
        Me.Label7.TabIndex = 1368
        Me.Label7.Text = "အလျော့တွက်"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Moccasin
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(473, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(159, 23)
        Me.Label8.TabIndex = 1367
        Me.Label8.Text = "အထည်ချိန်စုစုပေါင်း"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCurrentLocationName
        '
        Me.lblCurrentLocationName.AutoSize = True
        Me.lblCurrentLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentLocationName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLocationName.ForeColor = System.Drawing.Color.Blue
        Me.lblCurrentLocationName.Location = New System.Drawing.Point(768, 10)
        Me.lblCurrentLocationName.Name = "lblCurrentLocationName"
        Me.lblCurrentLocationName.Size = New System.Drawing.Size(99, 20)
        Me.lblCurrentLocationName.TabIndex = 1371
        Me.lblCurrentLocationName.Text = "GlobalGold"
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.grdItems.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdItems.Location = New System.Drawing.Point(13, 152)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.RowHeadersWidth = 25
        Me.grdItems.Size = New System.Drawing.Size(1025, 255)
        Me.grdItems.TabIndex = 1372
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Barcode#"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 85
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "G"
        Me.DataGridViewTextBoxColumn2.MaxInputLength = 10
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 65
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn3.HeaderText = "Rate"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 65
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn4.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 65
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label14.Location = New System.Drawing.Point(366, 508)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 19)
        Me.Label14.TabIndex = 1466
        Me.Label14.Text = "ခြားနားငွေ"
        '
        'txtDifferentAmount
        '
        Me.txtDifferentAmount.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtDifferentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDifferentAmount.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.txtDifferentAmount.Location = New System.Drawing.Point(434, 507)
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
        Me.txtPurchaseAmount.Location = New System.Drawing.Point(434, 482)
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
        Me.Label13.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label13.Location = New System.Drawing.Point(354, 483)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 19)
        Me.Label13.TabIndex = 1464
        Me.Label13.Text = "လဲခြင်းမှရငွေ"
        '
        'btnSearchPurchase
        '
        Me.btnSearchPurchase.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearchPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchPurchase.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchPurchase.Image = CType(resources.GetObject("btnSearchPurchase.Image"), System.Drawing.Image)
        Me.btnSearchPurchase.Location = New System.Drawing.Point(553, 458)
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
        Me.txtPurchaseVoucherNo.Location = New System.Drawing.Point(434, 458)
        Me.txtPurchaseVoucherNo.Name = "txtPurchaseVoucherNo"
        Me.txtPurchaseVoucherNo.ShortcutsEnabled = False
        Me.txtPurchaseVoucherNo.Size = New System.Drawing.Size(115, 21)
        Me.txtPurchaseVoucherNo.TabIndex = 1460
        Me.txtPurchaseVoucherNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label11.Location = New System.Drawing.Point(329, 458)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 19)
        Me.Label11.TabIndex = 1461
        Me.Label11.Text = "လဲခြင်းဘောက်ချာ"
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(985, 1)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 1636
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'lblLogInUserName
        '
        Me.lblLogInUserName.AutoSize = True
        Me.lblLogInUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblLogInUserName.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogInUserName.ForeColor = System.Drawing.Color.Blue
        Me.lblLogInUserName.Location = New System.Drawing.Point(768, 52)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(151, 23)
        Me.lblLogInUserName.TabIndex = 1639
        Me.lblLogInUserName.Text = "Thiri Myat Noe"
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
        Me.grpMember.Location = New System.Drawing.Point(594, 460)
        Me.grpMember.Margin = New System.Windows.Forms.Padding(2)
        Me.grpMember.Name = "grpMember"
        Me.grpMember.Padding = New System.Windows.Forms.Padding(2)
        Me.grpMember.Size = New System.Drawing.Size(424, 91)
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
        Me.dtpExpireDate.Location = New System.Drawing.Point(307, 38)
        Me.dtpExpireDate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtpExpireDate.Name = "dtpExpireDate"
        Me.dtpExpireDate.Size = New System.Drawing.Size(54, 20)
        Me.dtpExpireDate.TabIndex = 1678
        Me.dtpExpireDate.Visible = False
        '
        'txtMemberID
        '
        Me.txtMemberID.BackColor = System.Drawing.Color.Linen
        Me.txtMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberID.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberID.Location = New System.Drawing.Point(712, 90)
        Me.txtMemberID.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberID.Name = "txtMemberID"
        Me.txtMemberID.ReadOnly = True
        Me.txtMemberID.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberID.TabIndex = 1677
        '
        'txtMemberName
        '
        Me.txtMemberName.BackColor = System.Drawing.Color.Linen
        Me.txtMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberName.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberName.Location = New System.Drawing.Point(553, 90)
        Me.txtMemberName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberName.Name = "txtMemberName"
        Me.txtMemberName.ReadOnly = True
        Me.txtMemberName.Size = New System.Drawing.Size(150, 24)
        Me.txtMemberName.TabIndex = 1676
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BackColor = System.Drawing.Color.Linen
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.Location = New System.Drawing.Point(390, 90)
        Me.txtMemberCode.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.ReadOnly = True
        Me.txtMemberCode.Size = New System.Drawing.Size(155, 24)
        Me.txtMemberCode.TabIndex = 1675
        '
        'frm_ConsignmentSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1043, 643)
        Me.Controls.Add(Me.dtpExpireDate)
        Me.Controls.Add(Me.txtMemberID)
        Me.Controls.Add(Me.txtMemberName)
        Me.Controls.Add(Me.txtMemberCode)
        Me.Controls.Add(Me.grpMember)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtDifferentAmount)
        Me.Controls.Add(Me.txtPurchaseAmount)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnSearchPurchase)
        Me.Controls.Add(Me.txtPurchaseVoucherNo)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.grdItems)
        Me.Controls.Add(Me.lblCurrentLocationName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lnkB)
        Me.Controls.Add(Me.txtDiscountAmt)
        Me.Controls.Add(Me.optPay)
        Me.Controls.Add(Me.optDirectSale)
        Me.Controls.Add(Me.btnWholeSale)
        Me.Controls.Add(Me.txtWholeSaleID)
        Me.Controls.Add(Me.lblWholeSale)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnBroker)
        Me.Controls.Add(Me.txtBrokerName)
        Me.Controls.Add(Me.txtBrokerCode)
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
        Me.Controls.Add(Me.lnkCustomer)
        Me.Controls.Add(Me.btnCustomer)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.txtCustomerCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.dtpConsignDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnSearchConsign)
        Me.Controls.Add(Me.txtConsignmentSaleID)
        Me.Controls.Add(Me.Label9)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_ConsignmentSale"
        Me.Text = "Consignment Sale"
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtConsignmentSaleID, 0)
        Me.Controls.SetChildIndex(Me.btnSearchConsign, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.dtpConsignDate, 0)
        Me.Controls.SetChildIndex(Me.cboStaff, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtCustomerCode, 0)
        Me.Controls.SetChildIndex(Me.txtCustomerName, 0)
        Me.Controls.SetChildIndex(Me.btnCustomer, 0)
        Me.Controls.SetChildIndex(Me.lnkCustomer, 0)
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
        Me.Controls.SetChildIndex(Me.txtBrokerCode, 0)
        Me.Controls.SetChildIndex(Me.txtBrokerName, 0)
        Me.Controls.SetChildIndex(Me.btnBroker, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.lblWholeSale, 0)
        Me.Controls.SetChildIndex(Me.txtWholeSaleID, 0)
        Me.Controls.SetChildIndex(Me.btnWholeSale, 0)
        Me.Controls.SetChildIndex(Me.optDirectSale, 0)
        Me.Controls.SetChildIndex(Me.optPay, 0)
        Me.Controls.SetChildIndex(Me.txtDiscountAmt, 0)
        Me.Controls.SetChildIndex(Me.lnkB, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.lblCurrentLocationName, 0)
        Me.Controls.SetChildIndex(Me.grdItems, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.txtPurchaseVoucherNo, 0)
        Me.Controls.SetChildIndex(Me.btnSearchPurchase, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.txtPurchaseAmount, 0)
        Me.Controls.SetChildIndex(Me.txtDifferentAmount, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
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
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnBroker As System.Windows.Forms.Button
    Friend WithEvents txtBrokerName As System.Windows.Forms.TextBox
    Friend WithEvents txtBrokerCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBalanceAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPaidAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtAddOrSubAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lnkCustomer As System.Windows.Forms.LinkLabel
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents dtpConsignDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSearchConsign As System.Windows.Forms.Button
    Friend WithEvents txtConsignmentSaleID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnWholeSale As System.Windows.Forms.Button
    Friend WithEvents txtWholeSaleID As System.Windows.Forms.TextBox
    Friend WithEvents lblWholeSale As System.Windows.Forms.Label
    Friend WithEvents optPay As System.Windows.Forms.RadioButton
    Friend WithEvents optDirectSale As System.Windows.Forms.RadioButton
    Friend WithEvents txtDiscountAmt As System.Windows.Forms.TextBox
    Friend WithEvents lnkB As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentLocationName As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtDifferentAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnSearchPurchase As System.Windows.Forms.Button
    Friend WithEvents txtPurchaseVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
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
