<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PurchaseFromSupplier
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_PurchaseFromSupplier))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dtpPDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearchButton = New System.Windows.Forms.Button()
        Me.txtPurchaseFromSupplierID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnSupplierSearch = New System.Windows.Forms.Button()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.grdPurchaseItem = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDisAmount = New System.Windows.Forms.TextBox()
        Me.txtDisRate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBalanceAmt = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtPaidAmt = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtAddOrSub = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtNetAmt = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtTotalAmt = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtGridTotal = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtExRate = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtVoucher = New System.Windows.Forms.TextBox()
        Me.optBank = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.optConsignment = New System.Windows.Forms.RadioButton()
        Me.optCredit = New System.Windows.Forms.RadioButton()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
        Me.lnkSupplier = New System.Windows.Forms.LinkLabel()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        CType(Me.grdPurchaseItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpPDate
        '
        Me.dtpPDate.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPDate.Location = New System.Drawing.Point(83, 41)
        Me.dtpPDate.Name = "dtpPDate"
        Me.dtpPDate.RightToLeftLayout = True
        Me.dtpPDate.Size = New System.Drawing.Size(151, 20)
        Me.dtpPDate.TabIndex = 1464
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(45, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 17)
        Me.Label3.TabIndex = 1462
        Me.Label3.Text = "Date"
        '
        'btnSearchButton
        '
        Me.btnSearchButton.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchButton.Image = CType(resources.GetObject("btnSearchButton.Image"), System.Drawing.Image)
        Me.btnSearchButton.Location = New System.Drawing.Point(202, 12)
        Me.btnSearchButton.Name = "btnSearchButton"
        Me.btnSearchButton.Size = New System.Drawing.Size(45, 21)
        Me.btnSearchButton.TabIndex = 1460
        Me.btnSearchButton.UseVisualStyleBackColor = False
        '
        'txtPurchaseFromSupplierID
        '
        Me.txtPurchaseFromSupplierID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtPurchaseFromSupplierID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseFromSupplierID.Location = New System.Drawing.Point(83, 12)
        Me.txtPurchaseFromSupplierID.Name = "txtPurchaseFromSupplierID"
        Me.txtPurchaseFromSupplierID.ReadOnly = True
        Me.txtPurchaseFromSupplierID.Size = New System.Drawing.Size(117, 20)
        Me.txtPurchaseFromSupplierID.TabIndex = 1463
        Me.txtPurchaseFromSupplierID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(23, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 17)
        Me.Label9.TabIndex = 1461
        Me.Label9.Text = "Stock #"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(46, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 17)
        Me.Label1.TabIndex = 1472
        Me.Label1.Text = "Staff"
        '
        'cboStaff
        '
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(83, 67)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(193, 27)
        Me.cboStaff.TabIndex = 1471
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BackColor = System.Drawing.Color.White
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtSupplierCode.Location = New System.Drawing.Point(83, 100)
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(83, 26)
        Me.txtSupplierCode.TabIndex = 1475
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BackColor = System.Drawing.Color.White
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtSupplierName.Location = New System.Drawing.Point(172, 100)
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(243, 26)
        Me.txtSupplierName.TabIndex = 1476
        '
        'btnSupplierSearch
        '
        Me.btnSupplierSearch.BackColor = System.Drawing.Color.Transparent
        Me.btnSupplierSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierSearch.Image = CType(resources.GetObject("btnSupplierSearch.Image"), System.Drawing.Image)
        Me.btnSupplierSearch.Location = New System.Drawing.Point(421, 103)
        Me.btnSupplierSearch.Name = "btnSupplierSearch"
        Me.btnSupplierSearch.Size = New System.Drawing.Size(45, 21)
        Me.btnSupplierSearch.TabIndex = 1477
        Me.btnSupplierSearch.UseVisualStyleBackColor = False
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.Color.Transparent
        Me.Label63.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(350, 42)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(50, 17)
        Me.Label63.TabIndex = 1479
        Me.Label63.Text = "Remark"
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.White
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtRemark.Location = New System.Drawing.Point(412, 38)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(224, 59)
        Me.txtRemark.TabIndex = 1478
        '
        'grdPurchaseItem
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Myanmar3", 9.7!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchaseItem.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPurchaseItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Myanmar3", 9.7!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdPurchaseItem.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdPurchaseItem.Location = New System.Drawing.Point(6, 157)
        Me.grdPurchaseItem.Name = "grdPurchaseItem"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Myanmar3", 9.7!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchaseItem.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdPurchaseItem.RowHeadersWidth = 25
        Me.grdPurchaseItem.Size = New System.Drawing.Size(690, 244)
        Me.grdPurchaseItem.TabIndex = 1480
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Gainsboro
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 130)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(690, 21)
        Me.Label7.TabIndex = 1481
        Me.Label7.Text = "Purchase Items"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDisAmount
        '
        Me.txtDisAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDisAmount.BackColor = System.Drawing.Color.White
        Me.txtDisAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDisAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDisAmount.Location = New System.Drawing.Point(432, 429)
        Me.txtDisAmount.Name = "txtDisAmount"
        Me.txtDisAmount.ReadOnly = True
        Me.txtDisAmount.Size = New System.Drawing.Size(69, 20)
        Me.txtDisAmount.TabIndex = 1500
        Me.txtDisAmount.TabStop = False
        Me.txtDisAmount.Text = "0"
        Me.txtDisAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDisRate
        '
        Me.txtDisRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDisRate.BackColor = System.Drawing.Color.White
        Me.txtDisRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDisRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDisRate.Location = New System.Drawing.Point(400, 429)
        Me.txtDisRate.Name = "txtDisRate"
        Me.txtDisRate.Size = New System.Drawing.Size(32, 20)
        Me.txtDisRate.TabIndex = 1488
        Me.txtDisRate.Text = "0"
        Me.txtDisRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.Location = New System.Drawing.Point(330, 433)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 1499
        Me.Label8.Text = "Discount(%)"
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtBalanceAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtBalanceAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBalanceAmt.Location = New System.Drawing.Point(596, 430)
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReadOnly = True
        Me.txtBalanceAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtBalanceAmt.TabIndex = 1493
        Me.txtBalanceAmt.TabStop = False
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label41
        '
        Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label41.Location = New System.Drawing.Point(505, 434)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(85, 13)
        Me.Label41.TabIndex = 1498
        Me.Label41.Text = "Balance Amount"
        '
        'txtPaidAmt
        '
        Me.txtPaidAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPaidAmt.BackColor = System.Drawing.Color.White
        Me.txtPaidAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPaidAmt.Location = New System.Drawing.Point(596, 407)
        Me.txtPaidAmt.Name = "txtPaidAmt"
        Me.txtPaidAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtPaidAmt.TabIndex = 1490
        Me.txtPaidAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label40.Location = New System.Drawing.Point(523, 411)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(67, 13)
        Me.Label40.TabIndex = 1497
        Me.Label40.Text = "Paid Amount"
        '
        'txtAddOrSub
        '
        Me.txtAddOrSub.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAddOrSub.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAddOrSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddOrSub.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAddOrSub.Location = New System.Drawing.Point(400, 475)
        Me.txtAddOrSub.Name = "txtAddOrSub"
        Me.txtAddOrSub.ReadOnly = True
        Me.txtAddOrSub.Size = New System.Drawing.Size(100, 20)
        Me.txtAddOrSub.TabIndex = 1492
        Me.txtAddOrSub.TabStop = False
        Me.txtAddOrSub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label39.Location = New System.Drawing.Point(334, 477)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(60, 13)
        Me.Label39.TabIndex = 1496
        Me.Label39.Text = "Add or Sub"
        '
        'txtNetAmt
        '
        Me.txtNetAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtNetAmt.BackColor = System.Drawing.Color.White
        Me.txtNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNetAmt.Location = New System.Drawing.Point(400, 452)
        Me.txtNetAmt.Name = "txtNetAmt"
        Me.txtNetAmt.Size = New System.Drawing.Size(101, 20)
        Me.txtNetAmt.TabIndex = 1489
        Me.txtNetAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label38.Location = New System.Drawing.Point(330, 456)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(63, 13)
        Me.Label38.TabIndex = 1495
        Me.Label38.Text = "Net Amount"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalAmt.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTotalAmt.Location = New System.Drawing.Point(400, 406)
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.Size = New System.Drawing.Size(101, 20)
        Me.txtTotalAmt.TabIndex = 1491
        Me.txtTotalAmt.TabStop = False
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label36.Location = New System.Drawing.Point(323, 408)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(70, 13)
        Me.Label36.TabIndex = 1494
        Me.Label36.Text = "Total Amount"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(141, 437)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 1506
        Me.Label2.Text = "Total Amount"
        '
        'txtGridTotal
        '
        Me.txtGridTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtGridTotal.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtGridTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGridTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtGridTotal.Location = New System.Drawing.Point(213, 433)
        Me.txtGridTotal.Name = "txtGridTotal"
        Me.txtGridTotal.ReadOnly = True
        Me.txtGridTotal.Size = New System.Drawing.Size(100, 20)
        Me.txtGridTotal.TabIndex = 1505
        Me.txtGridTotal.TabStop = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.Location = New System.Drawing.Point(130, 459)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 1504
        Me.Label5.Text = "Exchange Rate"
        '
        'txtExRate
        '
        Me.txtExRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtExRate.BackColor = System.Drawing.Color.White
        Me.txtExRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtExRate.Location = New System.Drawing.Point(213, 456)
        Me.txtExRate.Name = "txtExRate"
        Me.txtExRate.Size = New System.Drawing.Size(100, 20)
        Me.txtExRate.TabIndex = 1502
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.Location = New System.Drawing.Point(154, 412)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 1503
        Me.Label6.Text = "Voucher #"
        '
        'txtVoucher
        '
        Me.txtVoucher.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtVoucher.BackColor = System.Drawing.Color.White
        Me.txtVoucher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVoucher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtVoucher.Location = New System.Drawing.Point(213, 410)
        Me.txtVoucher.Name = "txtVoucher"
        Me.txtVoucher.Size = New System.Drawing.Size(100, 20)
        Me.txtVoucher.TabIndex = 1501
        '
        'optBank
        '
        Me.optBank.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optBank.AutoSize = True
        Me.optBank.BackColor = System.Drawing.Color.Transparent
        Me.optBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.optBank.Location = New System.Drawing.Point(60, 479)
        Me.optBank.Name = "optBank"
        Me.optBank.Size = New System.Drawing.Size(50, 17)
        Me.optBank.TabIndex = 1510
        Me.optBank.TabStop = True
        Me.optBank.Text = "Bank"
        Me.optBank.UseVisualStyleBackColor = False
        '
        'optCash
        '
        Me.optCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optCash.AutoSize = True
        Me.optCash.BackColor = System.Drawing.Color.Transparent
        Me.optCash.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.optCash.Location = New System.Drawing.Point(5, 479)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(49, 17)
        Me.optCash.TabIndex = 1509
        Me.optCash.TabStop = True
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = False
        '
        'dtpDueDate
        '
        Me.dtpDueDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpDueDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpDueDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(13, 448)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(86, 20)
        Me.dtpDueDate.TabIndex = 1511
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.Location = New System.Drawing.Point(10, 432)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 13)
        Me.Label13.TabIndex = 1512
        Me.Label13.Text = "Due Date"
        '
        'optConsignment
        '
        Me.optConsignment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optConsignment.AutoSize = True
        Me.optConsignment.BackColor = System.Drawing.Color.Transparent
        Me.optConsignment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.optConsignment.Location = New System.Drawing.Point(59, 405)
        Me.optConsignment.Name = "optConsignment"
        Me.optConsignment.Size = New System.Drawing.Size(86, 17)
        Me.optConsignment.TabIndex = 1508
        Me.optConsignment.TabStop = True
        Me.optConsignment.Text = "Consignment"
        Me.optConsignment.UseVisualStyleBackColor = False
        '
        'optCredit
        '
        Me.optCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optCredit.AutoSize = True
        Me.optCredit.BackColor = System.Drawing.Color.Transparent
        Me.optCredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.optCredit.Location = New System.Drawing.Point(6, 405)
        Me.optCredit.Name = "optCredit"
        Me.optCredit.Size = New System.Drawing.Size(52, 17)
        Me.optCredit.TabIndex = 1507
        Me.optCredit.TabStop = True
        Me.optCredit.Text = "Credit"
        Me.optCredit.UseVisualStyleBackColor = False
        '
        'lblLogInUserName
        '
        Me.lblLogInUserName.AutoSize = True
        Me.lblLogInUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblLogInUserName.Font = New System.Drawing.Font("Myanmar3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogInUserName.ForeColor = System.Drawing.Color.Violet
        Me.lblLogInUserName.Location = New System.Drawing.Point(307, 15)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(90, 22)
        Me.lblLogInUserName.TabIndex = 1513
        Me.lblLogInUserName.Text = "LogInUser"
        '
        'lnkSupplier
        '
        Me.lnkSupplier.AutoSize = True
        Me.lnkSupplier.Location = New System.Drawing.Point(34, 103)
        Me.lnkSupplier.Name = "lnkSupplier"
        Me.lnkSupplier.Size = New System.Drawing.Size(45, 13)
        Me.lnkSupplier.TabIndex = 1514
        Me.lnkSupplier.TabStop = True
        Me.lnkSupplier.Text = "Supplier"
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(657, 4)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 32)
        Me.btnHelpbook.TabIndex = 1515
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_PurchaseFromSupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(701, 549)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.lnkSupplier)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.optBank)
        Me.Controls.Add(Me.optCash)
        Me.Controls.Add(Me.dtpDueDate)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.optConsignment)
        Me.Controls.Add(Me.optCredit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtGridTotal)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtExRate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtVoucher)
        Me.Controls.Add(Me.txtDisAmount)
        Me.Controls.Add(Me.txtDisRate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtBalanceAmt)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.txtPaidAmt)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.txtAddOrSub)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.txtNetAmt)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.txtTotalAmt)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.grdPurchaseItem)
        Me.Controls.Add(Me.Label63)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.btnSupplierSearch)
        Me.Controls.Add(Me.txtSupplierName)
        Me.Controls.Add(Me.txtSupplierCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.dtpPDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnSearchButton)
        Me.Controls.Add(Me.txtPurchaseFromSupplierID)
        Me.Controls.Add(Me.Label9)
        Me.KeyPreview = True
        Me.Name = "frm_PurchaseFromSupplier"
        Me.Text = "Purchase From Supplier"
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtPurchaseFromSupplierID, 0)
        Me.Controls.SetChildIndex(Me.btnSearchButton, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.dtpPDate, 0)
        Me.Controls.SetChildIndex(Me.cboStaff, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtSupplierCode, 0)
        Me.Controls.SetChildIndex(Me.txtSupplierName, 0)
        Me.Controls.SetChildIndex(Me.btnSupplierSearch, 0)
        Me.Controls.SetChildIndex(Me.txtRemark, 0)
        Me.Controls.SetChildIndex(Me.Label63, 0)
        Me.Controls.SetChildIndex(Me.grdPurchaseItem, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label36, 0)
        Me.Controls.SetChildIndex(Me.txtTotalAmt, 0)
        Me.Controls.SetChildIndex(Me.Label38, 0)
        Me.Controls.SetChildIndex(Me.txtNetAmt, 0)
        Me.Controls.SetChildIndex(Me.Label39, 0)
        Me.Controls.SetChildIndex(Me.txtAddOrSub, 0)
        Me.Controls.SetChildIndex(Me.Label40, 0)
        Me.Controls.SetChildIndex(Me.txtPaidAmt, 0)
        Me.Controls.SetChildIndex(Me.Label41, 0)
        Me.Controls.SetChildIndex(Me.txtBalanceAmt, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.txtDisRate, 0)
        Me.Controls.SetChildIndex(Me.txtDisAmount, 0)
        Me.Controls.SetChildIndex(Me.txtVoucher, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.txtExRate, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.txtGridTotal, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.optCredit, 0)
        Me.Controls.SetChildIndex(Me.optConsignment, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.dtpDueDate, 0)
        Me.Controls.SetChildIndex(Me.optCash, 0)
        Me.Controls.SetChildIndex(Me.optBank, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
        Me.Controls.SetChildIndex(Me.lnkSupplier, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        CType(Me.grdPurchaseItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpPDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearchButton As System.Windows.Forms.Button
    Friend WithEvents txtPurchaseFromSupplierID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierSearch As System.Windows.Forms.Button
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents grdPurchaseItem As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDisAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtDisRate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtPaidAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtAddOrSub As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtGridTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtExRate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtVoucher As System.Windows.Forms.TextBox
    Friend WithEvents optBank As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents optConsignment As System.Windows.Forms.RadioButton
    Friend WithEvents optCredit As System.Windows.Forms.RadioButton
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
    Friend WithEvents lnkSupplier As System.Windows.Forms.LinkLabel
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button

End Class
