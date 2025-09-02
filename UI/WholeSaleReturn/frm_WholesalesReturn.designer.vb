<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_WholesalesReturn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_WholesalesReturn))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.dtpReturnDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SearchWholeSale = New System.Windows.Forms.Button()
        Me.txtWSReturnID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.WSReturnItemID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WSaleReturnID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ForSaleID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BarcodeNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtSaleReturnAmt = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtSaleAmt = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtAddSub = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtNetAmt = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtTotalAmt = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtBalanceAmt = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtPaidAmt = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.btnWholeSaleID = New System.Windows.Forms.Button()
        Me.txtWholeSalesID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPayType = New System.Windows.Forms.Label()
        Me.optSaleReturn = New System.Windows.Forms.RadioButton()
        Me.optPayReturn = New System.Windows.Forms.RadioButton()
        Me.optSale = New System.Windows.Forms.RadioButton()
        Me.lblCurrentLocationName = New System.Windows.Forms.Label()
        Me.lblTotalDate = New System.Windows.Forms.Label()
        Me.lblWDate = New System.Windows.Forms.Label()
        Me.txtDiscountDis = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCommisionDis = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDiscountAmt = New System.Windows.Forms.TextBox()
        Me.chkView = New System.Windows.Forms.CheckBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtRemark
        '
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtRemark.Location = New System.Drawing.Point(557, 50)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(250, 72)
        Me.txtRemark.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label8.Location = New System.Drawing.Point(498, 54)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 16)
        Me.Label8.TabIndex = 876
        Me.Label8.Text = "Remark"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label3.Location = New System.Drawing.Point(92, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 16)
        Me.Label3.TabIndex = 874
        Me.Label3.Text = "Customer"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtCustomerName.Location = New System.Drawing.Point(263, 100)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(160, 26)
        Me.txtCustomerName.TabIndex = 7
        Me.txtCustomerName.TabStop = False
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(162, 100)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReadOnly = True
        Me.txtCustomerCode.Size = New System.Drawing.Size(100, 26)
        Me.txtCustomerCode.TabIndex = 6
        Me.txtCustomerCode.TabStop = False
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(1108, 133)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(30, 21)
        Me.btnCustomer.TabIndex = 8
        Me.btnCustomer.TabStop = False
        Me.btnCustomer.UseVisualStyleBackColor = True
        Me.btnCustomer.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label1.Location = New System.Drawing.Point(515, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 16)
        Me.Label1.TabIndex = 868
        Me.Label1.Text = "Staff"
        '
        'cboStaff
        '
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 8.999999!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(557, 16)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(194, 25)
        Me.cboStaff.TabIndex = 9
        '
        'dtpReturnDate
        '
        Me.dtpReturnDate.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReturnDate.Location = New System.Drawing.Point(162, 42)
        Me.dtpReturnDate.Name = "dtpReturnDate"
        Me.dtpReturnDate.Size = New System.Drawing.Size(156, 20)
        Me.dtpReturnDate.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label10.Location = New System.Drawing.Point(79, 42)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 16)
        Me.Label10.TabIndex = 866
        Me.Label10.Text = "Return Date"
        '
        'SearchWholeSale
        '
        Me.SearchWholeSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchWholeSale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchWholeSale.Image = CType(resources.GetObject("SearchWholeSale.Image"), System.Drawing.Image)
        Me.SearchWholeSale.Location = New System.Drawing.Point(288, 7)
        Me.SearchWholeSale.Name = "SearchWholeSale"
        Me.SearchWholeSale.Size = New System.Drawing.Size(30, 21)
        Me.SearchWholeSale.TabIndex = 2
        Me.SearchWholeSale.UseVisualStyleBackColor = True
        '
        'txtWSReturnID
        '
        Me.txtWSReturnID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWSReturnID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWSReturnID.Location = New System.Drawing.Point(162, 7)
        Me.txtWSReturnID.Name = "txtWSReturnID"
        Me.txtWSReturnID.ReadOnly = True
        Me.txtWSReturnID.Size = New System.Drawing.Size(119, 20)
        Me.txtWSReturnID.TabIndex = 1
        Me.txtWSReturnID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(28, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(129, 13)
        Me.Label9.TabIndex = 865
        Me.Label9.Text = "Wholesales Return ID"
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
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.WSReturnItemID, Me.WSaleReturnID, Me.ForSaleID, Me.BarcodeNo, Me.Gram, Me.Rate, Me.Amount})
        Me.grdItems.Location = New System.Drawing.Point(0, 161)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.RowHeadersWidth = 25
        Me.grdItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdItems.Size = New System.Drawing.Size(1308, 237)
        Me.grdItems.TabIndex = 11
        '
        'WSReturnItemID
        '
        Me.WSReturnItemID.HeaderText = "WSReturnItemID"
        Me.WSReturnItemID.Name = "WSReturnItemID"
        Me.WSReturnItemID.Visible = False
        '
        'WSaleReturnID
        '
        Me.WSaleReturnID.HeaderText = "@WSReturnID"
        Me.WSaleReturnID.Name = "WSaleReturnID"
        Me.WSaleReturnID.Visible = False
        '
        'ForSaleID
        '
        Me.ForSaleID.HeaderText = "ForSaleID"
        Me.ForSaleID.Name = "ForSaleID"
        Me.ForSaleID.Visible = False
        '
        'BarcodeNo
        '
        Me.BarcodeNo.HeaderText = "BarcodeNo"
        Me.BarcodeNo.Name = "BarcodeNo"
        Me.BarcodeNo.Width = 150
        '
        'Gram
        '
        Me.Gram.HeaderText = "Gram"
        Me.Gram.Name = "Gram"
        '
        'Rate
        '
        Me.Rate.HeaderText = "Rate"
        Me.Rate.Name = "Rate"
        '
        'Amount
        '
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        '
        'txtSaleReturnAmt
        '
        Me.txtSaleReturnAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtSaleReturnAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleReturnAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleReturnAmt.Location = New System.Drawing.Point(450, 467)
        Me.txtSaleReturnAmt.Name = "txtSaleReturnAmt"
        Me.txtSaleReturnAmt.ReadOnly = True
        Me.txtSaleReturnAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtSaleReturnAmt.TabIndex = 879
        Me.txtSaleReturnAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label18.Location = New System.Drawing.Point(338, 467)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(104, 16)
        Me.Label18.TabIndex = 882
        Me.Label18.Text = "Sale Return Amt"
        '
        'txtSaleAmt
        '
        Me.txtSaleAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtSaleAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleAmt.Location = New System.Drawing.Point(1000, 65)
        Me.txtSaleAmt.Name = "txtSaleAmt"
        Me.txtSaleAmt.ReadOnly = True
        Me.txtSaleAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtSaleAmt.TabIndex = 880
        Me.txtSaleAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSaleAmt.Visible = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label20.Location = New System.Drawing.Point(1006, 46)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(65, 16)
        Me.Label20.TabIndex = 881
        Me.Label20.Text = "Sale Amt:"
        Me.Label20.Visible = False
        '
        'txtAddSub
        '
        Me.txtAddSub.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAddSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddSub.Location = New System.Drawing.Point(734, 493)
        Me.txtAddSub.Name = "txtAddSub"
        Me.txtAddSub.ReadOnly = True
        Me.txtAddSub.Size = New System.Drawing.Size(100, 20)
        Me.txtAddSub.TabIndex = 884
        Me.txtAddSub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label39.Location = New System.Drawing.Point(657, 494)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(75, 16)
        Me.Label39.TabIndex = 888
        Me.Label39.Text = "Add or Sub"
        '
        'txtNetAmt
        '
        Me.txtNetAmt.BackColor = System.Drawing.Color.White
        Me.txtNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmt.Location = New System.Drawing.Point(734, 467)
        Me.txtNetAmt.Name = "txtNetAmt"
        Me.txtNetAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtNetAmt.TabIndex = 12
        Me.txtNetAmt.Text = "0"
        Me.txtNetAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label38.Location = New System.Drawing.Point(654, 467)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(77, 16)
        Me.Label38.TabIndex = 887
        Me.Label38.Text = "Net Amount"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmt.Location = New System.Drawing.Point(450, 494)
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalAmt.TabIndex = 885
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label36.Location = New System.Drawing.Point(357, 493)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(87, 16)
        Me.Label36.TabIndex = 886
        Me.Label36.Text = "Total Amount"
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtBalanceAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmt.Location = New System.Drawing.Point(734, 546)
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReadOnly = True
        Me.txtBalanceAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtBalanceAmt.TabIndex = 890
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label41.Location = New System.Drawing.Point(629, 545)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(106, 16)
        Me.Label41.TabIndex = 892
        Me.Label41.Text = "Balance Amount"
        '
        'txtPaidAmt
        '
        Me.txtPaidAmt.BackColor = System.Drawing.Color.White
        Me.txtPaidAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidAmt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmt.Location = New System.Drawing.Point(734, 519)
        Me.txtPaidAmt.Name = "txtPaidAmt"
        Me.txtPaidAmt.Size = New System.Drawing.Size(100, 21)
        Me.txtPaidAmt.TabIndex = 13
        Me.txtPaidAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label40.Location = New System.Drawing.Point(649, 519)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(84, 16)
        Me.Label40.TabIndex = 891
        Me.Label40.Text = "Paid Amount"
        '
        'btnWholeSaleID
        '
        Me.btnWholeSaleID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWholeSaleID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWholeSaleID.Image = CType(resources.GetObject("btnWholeSaleID.Image"), System.Drawing.Image)
        Me.btnWholeSaleID.Location = New System.Drawing.Point(284, 76)
        Me.btnWholeSaleID.Name = "btnWholeSaleID"
        Me.btnWholeSaleID.Size = New System.Drawing.Size(30, 21)
        Me.btnWholeSaleID.TabIndex = 4
        Me.btnWholeSaleID.UseVisualStyleBackColor = True
        '
        'txtWholeSalesID
        '
        Me.txtWholeSalesID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWholeSalesID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWholeSalesID.Location = New System.Drawing.Point(162, 76)
        Me.txtWholeSalesID.Name = "txtWholeSalesID"
        Me.txtWholeSalesID.ReadOnly = True
        Me.txtWholeSalesID.Size = New System.Drawing.Size(119, 20)
        Me.txtWholeSalesID.TabIndex = 4
        Me.txtWholeSalesID.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(70, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 901
        Me.Label2.Text = "Wholesales ID"
        '
        'lblPayType
        '
        Me.lblPayType.AutoSize = True
        Me.lblPayType.BackColor = System.Drawing.Color.Transparent
        Me.lblPayType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayType.Location = New System.Drawing.Point(470, 93)
        Me.lblPayType.Name = "lblPayType"
        Me.lblPayType.Size = New System.Drawing.Size(65, 13)
        Me.lblPayType.TabIndex = 903
        Me.lblPayType.Text = "Pay Type  "
        '
        'optSaleReturn
        '
        Me.optSaleReturn.AutoSize = True
        Me.optSaleReturn.BackColor = System.Drawing.Color.Transparent
        Me.optSaleReturn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSaleReturn.Location = New System.Drawing.Point(1059, 115)
        Me.optSaleReturn.Name = "optSaleReturn"
        Me.optSaleReturn.Size = New System.Drawing.Size(40, 17)
        Me.optSaleReturn.TabIndex = 7
        Me.optSaleReturn.TabStop = True
        Me.optSaleReturn.Text = "SR"
        Me.optSaleReturn.UseVisualStyleBackColor = False
        Me.optSaleReturn.Visible = False
        '
        'optPayReturn
        '
        Me.optPayReturn.AutoSize = True
        Me.optPayReturn.BackColor = System.Drawing.Color.Transparent
        Me.optPayReturn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPayReturn.Location = New System.Drawing.Point(1060, 137)
        Me.optPayReturn.Name = "optPayReturn"
        Me.optPayReturn.Size = New System.Drawing.Size(40, 17)
        Me.optPayReturn.TabIndex = 5
        Me.optPayReturn.TabStop = True
        Me.optPayReturn.Text = "PR"
        Me.optPayReturn.UseVisualStyleBackColor = False
        Me.optPayReturn.Visible = False
        '
        'optSale
        '
        Me.optSale.AutoSize = True
        Me.optSale.BackColor = System.Drawing.Color.Transparent
        Me.optSale.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSale.Location = New System.Drawing.Point(1060, 91)
        Me.optSale.Name = "optSale"
        Me.optSale.Size = New System.Drawing.Size(32, 17)
        Me.optSale.TabIndex = 6
        Me.optSale.TabStop = True
        Me.optSale.Text = "S"
        Me.optSale.UseVisualStyleBackColor = False
        Me.optSale.Visible = False
        '
        'lblCurrentLocationName
        '
        Me.lblCurrentLocationName.AutoSize = True
        Me.lblCurrentLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentLocationName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLocationName.ForeColor = System.Drawing.Color.Blue
        Me.lblCurrentLocationName.Location = New System.Drawing.Point(802, 12)
        Me.lblCurrentLocationName.Name = "lblCurrentLocationName"
        Me.lblCurrentLocationName.Size = New System.Drawing.Size(99, 20)
        Me.lblCurrentLocationName.TabIndex = 1063
        Me.lblCurrentLocationName.Text = "GlobalGold"
        '
        'lblTotalDate
        '
        Me.lblTotalDate.AutoSize = True
        Me.lblTotalDate.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDate.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblTotalDate.Location = New System.Drawing.Point(995, 102)
        Me.lblTotalDate.Name = "lblTotalDate"
        Me.lblTotalDate.Size = New System.Drawing.Size(12, 13)
        Me.lblTotalDate.TabIndex = 1065
        Me.lblTotalDate.Text = "-"
        '
        'lblWDate
        '
        Me.lblWDate.AutoSize = True
        Me.lblWDate.BackColor = System.Drawing.Color.Transparent
        Me.lblWDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWDate.ForeColor = System.Drawing.Color.Red
        Me.lblWDate.Location = New System.Drawing.Point(995, 139)
        Me.lblWDate.Name = "lblWDate"
        Me.lblWDate.Size = New System.Drawing.Size(12, 13)
        Me.lblWDate.TabIndex = 1066
        Me.lblWDate.Text = "-"
        '
        'txtDiscountDis
        '
        Me.txtDiscountDis.BackColor = System.Drawing.Color.White
        Me.txtDiscountDis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscountDis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscountDis.Location = New System.Drawing.Point(263, 529)
        Me.txtDiscountDis.Name = "txtDiscountDis"
        Me.txtDiscountDis.Size = New System.Drawing.Size(32, 21)
        Me.txtDiscountDis.TabIndex = 1067
        Me.txtDiscountDis.Text = "0"
        Me.txtDiscountDis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscountDis.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Label6.Location = New System.Drawing.Point(386, 519)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 16)
        Me.Label6.TabIndex = 1068
        Me.Label6.Text = "Discount"
        '
        'txtCommisionDis
        '
        Me.txtCommisionDis.BackColor = System.Drawing.Color.White
        Me.txtCommisionDis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCommisionDis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommisionDis.Location = New System.Drawing.Point(332, 520)
        Me.txtCommisionDis.Name = "txtCommisionDis"
        Me.txtCommisionDis.Size = New System.Drawing.Size(32, 21)
        Me.txtCommisionDis.TabIndex = 1070
        Me.txtCommisionDis.Text = "0"
        Me.txtCommisionDis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCommisionDis.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(813, 101)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 31)
        Me.btnPrint.TabIndex = 1359
        Me.btnPrint.Text = "Print "
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Khaki
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(857, 138)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 23)
        Me.Label5.TabIndex = 1366
        Me.Label5.Text = "ရွှေအတင်အလေးချိန်"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(735, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 23)
        Me.Label4.TabIndex = 1365
        Me.Label4.Text = "ကျောက်ချိန်"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LightBlue
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(616, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 23)
        Me.Label7.TabIndex = 1364
        Me.Label7.Text = "အလျော့တွက်"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Moccasin
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(495, 138)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(121, 23)
        Me.Label11.TabIndex = 1363
        Me.Label11.Text = "အထည်ချိန်စုစုပေါင်း"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDiscountAmt
        '
        Me.txtDiscountAmt.BackColor = System.Drawing.Color.White
        Me.txtDiscountAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscountAmt.Location = New System.Drawing.Point(450, 520)
        Me.txtDiscountAmt.Name = "txtDiscountAmt"
        Me.txtDiscountAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtDiscountAmt.TabIndex = 1367
        Me.txtDiscountAmt.Text = "0"
        Me.txtDiscountAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkView
        '
        Me.chkView.AutoSize = True
        Me.chkView.Location = New System.Drawing.Point(332, 77)
        Me.chkView.Name = "chkView"
        Me.chkView.Size = New System.Drawing.Size(94, 17)
        Me.chkView.TabIndex = 1368
        Me.chkView.Text = "View Pay Only"
        Me.chkView.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(923, 5)
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
        Me.lblLogInUserName.Location = New System.Drawing.Point(813, 49)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(151, 23)
        Me.lblLogInUserName.TabIndex = 1637
        Me.lblLogInUserName.Text = "Thiri Myat Noe"
        '
        'frm_WholesalesReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1320, 609)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.chkView)
        Me.Controls.Add(Me.txtDiscountAmt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.txtCommisionDis)
        Me.Controls.Add(Me.txtDiscountDis)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblWDate)
        Me.Controls.Add(Me.lblTotalDate)
        Me.Controls.Add(Me.lblCurrentLocationName)
        Me.Controls.Add(Me.optSaleReturn)
        Me.Controls.Add(Me.optPayReturn)
        Me.Controls.Add(Me.optSale)
        Me.Controls.Add(Me.lblPayType)
        Me.Controls.Add(Me.btnWholeSaleID)
        Me.Controls.Add(Me.txtWholeSalesID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBalanceAmt)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.txtPaidAmt)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.txtAddSub)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.txtNetAmt)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.txtTotalAmt)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.txtSaleReturnAmt)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtSaleAmt)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.grdItems)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.txtCustomerCode)
        Me.Controls.Add(Me.btnCustomer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.dtpReturnDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.SearchWholeSale)
        Me.Controls.Add(Me.txtWSReturnID)
        Me.Controls.Add(Me.Label9)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_WholesalesReturn"
        Me.Text = "Wholesales Return"
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtWSReturnID, 0)
        Me.Controls.SetChildIndex(Me.SearchWholeSale, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.dtpReturnDate, 0)
        Me.Controls.SetChildIndex(Me.cboStaff, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnCustomer, 0)
        Me.Controls.SetChildIndex(Me.txtCustomerCode, 0)
        Me.Controls.SetChildIndex(Me.txtCustomerName, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.txtRemark, 0)
        Me.Controls.SetChildIndex(Me.grdItems, 0)
        Me.Controls.SetChildIndex(Me.Label20, 0)
        Me.Controls.SetChildIndex(Me.txtSaleAmt, 0)
        Me.Controls.SetChildIndex(Me.Label18, 0)
        Me.Controls.SetChildIndex(Me.txtSaleReturnAmt, 0)
        Me.Controls.SetChildIndex(Me.Label36, 0)
        Me.Controls.SetChildIndex(Me.txtTotalAmt, 0)
        Me.Controls.SetChildIndex(Me.Label38, 0)
        Me.Controls.SetChildIndex(Me.txtNetAmt, 0)
        Me.Controls.SetChildIndex(Me.Label39, 0)
        Me.Controls.SetChildIndex(Me.txtAddSub, 0)
        Me.Controls.SetChildIndex(Me.Label40, 0)
        Me.Controls.SetChildIndex(Me.txtPaidAmt, 0)
        Me.Controls.SetChildIndex(Me.Label41, 0)
        Me.Controls.SetChildIndex(Me.txtBalanceAmt, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtWholeSalesID, 0)
        Me.Controls.SetChildIndex(Me.btnWholeSaleID, 0)
        Me.Controls.SetChildIndex(Me.lblPayType, 0)
        Me.Controls.SetChildIndex(Me.optSale, 0)
        Me.Controls.SetChildIndex(Me.optPayReturn, 0)
        Me.Controls.SetChildIndex(Me.optSaleReturn, 0)
        Me.Controls.SetChildIndex(Me.lblCurrentLocationName, 0)
        Me.Controls.SetChildIndex(Me.lblTotalDate, 0)
        Me.Controls.SetChildIndex(Me.lblWDate, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.txtDiscountDis, 0)
        Me.Controls.SetChildIndex(Me.txtCommisionDis, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.txtDiscountAmt, 0)
        Me.Controls.SetChildIndex(Me.chkView, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents dtpReturnDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents SearchWholeSale As System.Windows.Forms.Button
    Friend WithEvents txtWSReturnID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtSaleReturnAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSaleAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtAddSub As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtPaidAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnWholeSaleID As System.Windows.Forms.Button
    Friend WithEvents txtWholeSalesID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPayType As System.Windows.Forms.Label
    Friend WithEvents optSaleReturn As System.Windows.Forms.RadioButton
    Friend WithEvents optPayReturn As System.Windows.Forms.RadioButton
    Friend WithEvents optSale As System.Windows.Forms.RadioButton
    Friend WithEvents WSReturnItemID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WSaleReturnID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ForSaleID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BarcodeNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Gram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblCurrentLocationName As System.Windows.Forms.Label
    Friend WithEvents lblTotalDate As System.Windows.Forms.Label
    Friend WithEvents lblWDate As System.Windows.Forms.Label
    Friend WithEvents txtDiscountDis As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCommisionDis As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDiscountAmt As System.Windows.Forms.TextBox
    Friend WithEvents chkView As System.Windows.Forms.CheckBox
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label

End Class
