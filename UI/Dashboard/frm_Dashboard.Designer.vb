<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Dashboard
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
        Me.components = New System.ComponentModel.Container()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.SaleReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.SaleCReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.StockBalanceReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.cboCashdate = New System.Windows.Forms.ComboBox()
        Me.DsSaleInvoiceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cboCashSaleType = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.dtpSaleTDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpSaleFDate = New System.Windows.Forms.DateTimePicker()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboSortingType = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboSaleCategoryType = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpSaleCategoryTDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpSaleCategoryFDate = New System.Windows.Forms.DateTimePicker()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSale = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSaleCash = New System.Windows.Forms.TextBox()
        Me.txtSaleCredit = New System.Windows.Forms.TextBox()
        Me.txtSaleBalance = New System.Windows.Forms.TextBox()
        Me.txtWSaleCash = New System.Windows.Forms.TextBox()
        Me.txtWSaleCredit = New System.Windows.Forms.TextBox()
        Me.txtWSaleBalance = New System.Windows.Forms.TextBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CboBalanceType = New System.Windows.Forms.ComboBox()
        Me.Type = New System.Windows.Forms.Label()
        Me.cboItemType = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CboType = New System.Windows.Forms.ComboBox()
        Me.DBTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.DsWholeSaleInvoice = New UI.dsWholeSaleInvoice()
        Me.DsWholeSaleInvoiceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.txtGoldPrice = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboSaleType = New System.Windows.Forms.ComboBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTotalCreidt = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtTotalStock = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtGTotal = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.DsSaleInvoiceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsWholeSaleInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsWholeSaleInvoiceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rptViewer
        '
        Me.rptViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.rptViewer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rptViewer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rptViewer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.rptViewer.Location = New System.Drawing.Point(197, 80)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.ShowToolBar = False
        Me.rptViewer.Size = New System.Drawing.Size(565, 246)
        Me.rptViewer.TabIndex = 3
        Me.rptViewer.TabStop = False
        '
        'SaleReportViewer
        '
        Me.SaleReportViewer.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.SaleReportViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.SaleReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SaleReportViewer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaleReportViewer.Location = New System.Drawing.Point(795, 80)
        Me.SaleReportViewer.Name = "SaleReportViewer"
        Me.SaleReportViewer.ShowToolBar = False
        Me.SaleReportViewer.Size = New System.Drawing.Size(551, 246)
        Me.SaleReportViewer.TabIndex = 5
        Me.SaleReportViewer.TabStop = False
        '
        'SaleCReportViewer
        '
        Me.SaleCReportViewer.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.SaleCReportViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.SaleCReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SaleCReportViewer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaleCReportViewer.Location = New System.Drawing.Point(795, 377)
        Me.SaleCReportViewer.Name = "SaleCReportViewer"
        Me.SaleCReportViewer.ShowToolBar = False
        Me.SaleCReportViewer.Size = New System.Drawing.Size(550, 242)
        Me.SaleCReportViewer.TabIndex = 7
        Me.SaleCReportViewer.TabStop = False
        '
        'StockBalanceReportViewer
        '
        Me.StockBalanceReportViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.StockBalanceReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.StockBalanceReportViewer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StockBalanceReportViewer.Location = New System.Drawing.Point(195, 377)
        Me.StockBalanceReportViewer.Name = "StockBalanceReportViewer"
        Me.StockBalanceReportViewer.ShowToolBar = False
        Me.StockBalanceReportViewer.Size = New System.Drawing.Size(567, 242)
        Me.StockBalanceReportViewer.TabIndex = 6
        Me.StockBalanceReportViewer.TabStop = False
        '
        'cboCashdate
        '
        Me.cboCashdate.FormattingEnabled = True
        Me.cboCashdate.Items.AddRange(New Object() {"Day ", "Month", "Year"})
        Me.cboCashdate.Location = New System.Drawing.Point(276, 51)
        Me.cboCashdate.Name = "cboCashdate"
        Me.cboCashdate.Size = New System.Drawing.Size(107, 21)
        Me.cboCashdate.TabIndex = 19
        '
        'cboCashSaleType
        '
        Me.cboCashSaleType.FormattingEnabled = True
        Me.cboCashSaleType.Items.AddRange(New Object() {"Sale", "Whole Sale"})
        Me.cboCashSaleType.Location = New System.Drawing.Point(464, 52)
        Me.cboCashSaleType.Name = "cboCashSaleType"
        Me.cboCashSaleType.Size = New System.Drawing.Size(107, 21)
        Me.cboCashSaleType.TabIndex = 21
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label15.Location = New System.Drawing.Point(195, 55)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 16)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Date Type"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label16.Location = New System.Drawing.Point(390, 55)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 16)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Sale Type"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label25.Location = New System.Drawing.Point(942, 54)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(53, 16)
        Me.Label25.TabIndex = 26
        Me.Label25.Text = "To Date"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label26.Location = New System.Drawing.Point(791, 54)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(69, 16)
        Me.Label26.TabIndex = 24
        Me.Label26.Text = "From Date"
        '
        'dtpSaleTDate
        '
        Me.dtpSaleTDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpSaleTDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSaleTDate.Location = New System.Drawing.Point(996, 52)
        Me.dtpSaleTDate.Name = "dtpSaleTDate"
        Me.dtpSaleTDate.Size = New System.Drawing.Size(79, 20)
        Me.dtpSaleTDate.TabIndex = 25
        Me.dtpSaleTDate.TabStop = False
        '
        'dtpSaleFDate
        '
        Me.dtpSaleFDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpSaleFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSaleFDate.Location = New System.Drawing.Point(861, 52)
        Me.dtpSaleFDate.Name = "dtpSaleFDate"
        Me.dtpSaleFDate.Size = New System.Drawing.Size(81, 20)
        Me.dtpSaleFDate.TabIndex = 23
        Me.dtpSaleFDate.TabStop = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label27.Location = New System.Drawing.Point(1075, 54)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 16)
        Me.Label27.TabIndex = 28
        Me.Label27.Text = "Sale Type"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label13.Location = New System.Drawing.Point(195, 346)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 16)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Sorting Type"
        '
        'cboSortingType
        '
        Me.cboSortingType.FormattingEnabled = True
        Me.cboSortingType.Items.AddRange(New Object() {"Max", "Min"})
        Me.cboSortingType.Location = New System.Drawing.Point(281, 344)
        Me.cboSortingType.Name = "cboSortingType"
        Me.cboSortingType.Size = New System.Drawing.Size(137, 21)
        Me.cboSortingType.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Location = New System.Drawing.Point(1065, 344)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Sale Type"
        '
        'cboSaleCategoryType
        '
        Me.cboSaleCategoryType.FormattingEnabled = True
        Me.cboSaleCategoryType.Items.AddRange(New Object() {"Sale", "Whole Sale"})
        Me.cboSaleCategoryType.Location = New System.Drawing.Point(1129, 341)
        Me.cboSaleCategoryType.Name = "cboSaleCategoryType"
        Me.cboSaleCategoryType.Size = New System.Drawing.Size(66, 21)
        Me.cboSaleCategoryType.TabIndex = 35
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label7.Location = New System.Drawing.Point(937, 344)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 16)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "To Date"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label14.Location = New System.Drawing.Point(791, 344)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(69, 16)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "From Date"
        '
        'dtpSaleCategoryTDate
        '
        Me.dtpSaleCategoryTDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpSaleCategoryTDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSaleCategoryTDate.Location = New System.Drawing.Point(990, 341)
        Me.dtpSaleCategoryTDate.Name = "dtpSaleCategoryTDate"
        Me.dtpSaleCategoryTDate.Size = New System.Drawing.Size(75, 20)
        Me.dtpSaleCategoryTDate.TabIndex = 33
        Me.dtpSaleCategoryTDate.TabStop = False
        '
        'dtpSaleCategoryFDate
        '
        Me.dtpSaleCategoryFDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpSaleCategoryFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSaleCategoryFDate.Location = New System.Drawing.Point(860, 341)
        Me.dtpSaleCategoryFDate.Name = "dtpSaleCategoryFDate"
        Me.dtpSaleCategoryFDate.Size = New System.Drawing.Size(76, 20)
        Me.dtpSaleCategoryFDate.TabIndex = 31
        Me.dtpSaleCategoryFDate.TabStop = False
        '
        'ListView2
        '
        Me.ListView2.BackColor = System.Drawing.Color.Gray
        Me.ListView2.Location = New System.Drawing.Point(180, 0)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(1164, 47)
        Me.ListView2.TabIndex = 38
        Me.ListView2.UseCompatibleStateImageBehavior = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label2.Location = New System.Drawing.Point(1, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 23)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Sale"
        '
        'lblSale
        '
        Me.lblSale.AutoSize = True
        Me.lblSale.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblSale.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSale.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lblSale.Location = New System.Drawing.Point(15, 106)
        Me.lblSale.Name = "lblSale"
        Me.lblSale.Size = New System.Drawing.Size(40, 20)
        Me.lblSale.TabIndex = 42
        Me.lblSale.Text = "Cash"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label4.Location = New System.Drawing.Point(15, 164)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 20)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Credit"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label6.Location = New System.Drawing.Point(15, 219)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 20)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Total"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label3.Location = New System.Drawing.Point(-1, 266)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 23)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "WholeSale"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Gray
        Me.Label11.Font = New System.Drawing.Font("Myanmar3", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label11.Location = New System.Drawing.Point(190, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(121, 32)
        Me.Label11.TabIndex = 50
        Me.Label11.Text = "Dashboard"
        '
        'txtSaleCash
        '
        Me.txtSaleCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSaleCash.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleCash.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleCash.ForeColor = System.Drawing.Color.Gold
        Me.txtSaleCash.Location = New System.Drawing.Point(69, 103)
        Me.txtSaleCash.Name = "txtSaleCash"
        Me.txtSaleCash.Size = New System.Drawing.Size(99, 25)
        Me.txtSaleCash.TabIndex = 51
        Me.txtSaleCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSaleCredit
        '
        Me.txtSaleCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSaleCredit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleCredit.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleCredit.ForeColor = System.Drawing.Color.Gold
        Me.txtSaleCredit.Location = New System.Drawing.Point(69, 159)
        Me.txtSaleCredit.Name = "txtSaleCredit"
        Me.txtSaleCredit.Size = New System.Drawing.Size(99, 25)
        Me.txtSaleCredit.TabIndex = 52
        Me.txtSaleCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSaleBalance
        '
        Me.txtSaleBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSaleBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleBalance.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleBalance.ForeColor = System.Drawing.Color.Gold
        Me.txtSaleBalance.Location = New System.Drawing.Point(69, 216)
        Me.txtSaleBalance.Name = "txtSaleBalance"
        Me.txtSaleBalance.Size = New System.Drawing.Size(99, 25)
        Me.txtSaleBalance.TabIndex = 53
        Me.txtSaleBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWSaleCash
        '
        Me.txtWSaleCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWSaleCash.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWSaleCash.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWSaleCash.ForeColor = System.Drawing.Color.Gold
        Me.txtWSaleCash.Location = New System.Drawing.Point(69, 309)
        Me.txtWSaleCash.Name = "txtWSaleCash"
        Me.txtWSaleCash.Size = New System.Drawing.Size(99, 25)
        Me.txtWSaleCash.TabIndex = 54
        Me.txtWSaleCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWSaleCredit
        '
        Me.txtWSaleCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWSaleCredit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWSaleCredit.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWSaleCredit.ForeColor = System.Drawing.Color.Gold
        Me.txtWSaleCredit.Location = New System.Drawing.Point(69, 368)
        Me.txtWSaleCredit.Name = "txtWSaleCredit"
        Me.txtWSaleCredit.Size = New System.Drawing.Size(99, 25)
        Me.txtWSaleCredit.TabIndex = 55
        Me.txtWSaleCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWSaleBalance
        '
        Me.txtWSaleBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWSaleBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWSaleBalance.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWSaleBalance.ForeColor = System.Drawing.Color.Gold
        Me.txtWSaleBalance.Location = New System.Drawing.Point(69, 418)
        Me.txtWSaleBalance.Name = "txtWSaleBalance"
        Me.txtWSaleBalance.Size = New System.Drawing.Size(99, 25)
        Me.txtWSaleBalance.TabIndex = 56
        Me.txtWSaleBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label12.Location = New System.Drawing.Point(586, 55)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 16)
        Me.Label12.TabIndex = 58
        Me.Label12.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"Detail", "Summary"})
        Me.cboStatus.Location = New System.Drawing.Point(652, 53)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(107, 21)
        Me.cboStatus.TabIndex = 57
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label17.Location = New System.Drawing.Point(432, 347)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(43, 16)
        Me.Label17.TabIndex = 60
        Me.Label17.Text = "Status"
        '
        'CboBalanceType
        '
        Me.CboBalanceType.AutoCompleteCustomSource.AddRange(New String() {"Stock Balance", "Customer Credit"})
        Me.CboBalanceType.FormattingEnabled = True
        Me.CboBalanceType.Items.AddRange(New Object() {"Stock Balance", "Customer Credit"})
        Me.CboBalanceType.Location = New System.Drawing.Point(479, 345)
        Me.CboBalanceType.Name = "CboBalanceType"
        Me.CboBalanceType.Size = New System.Drawing.Size(107, 21)
        Me.CboBalanceType.TabIndex = 59
        '
        'Type
        '
        Me.Type.AutoSize = True
        Me.Type.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Type.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Type.Location = New System.Drawing.Point(1195, 344)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(66, 16)
        Me.Type.TabIndex = 62
        Me.Type.Text = "Item Type"
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteCustomSource.AddRange(New String() {"Item Name", "Item Category"})
        Me.cboItemType.FormattingEnabled = True
        Me.cboItemType.Items.AddRange(New Object() {"Item Name", "Item Category"})
        Me.cboItemType.Location = New System.Drawing.Point(1262, 341)
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.Size = New System.Drawing.Size(85, 21)
        Me.cboItemType.TabIndex = 61
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label18.Location = New System.Drawing.Point(1217, 54)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(36, 16)
        Me.Label18.TabIndex = 64
        Me.Label18.Text = "Type"
        '
        'CboType
        '
        Me.CboType.AutoCompleteCustomSource.AddRange(New String() {"Item Name", "Item Category"})
        Me.CboType.FormattingEnabled = True
        Me.CboType.Items.AddRange(New Object() {"Amount", "Gram"})
        Me.CboType.Location = New System.Drawing.Point(1258, 52)
        Me.CboType.Name = "CboType"
        Me.CboType.Size = New System.Drawing.Size(86, 21)
        Me.CboType.TabIndex = 63
        '
        'DBTimer
        '
        Me.DBTimer.Enabled = True
        '
        'DsWholeSaleInvoice
        '
        Me.DsWholeSaleInvoice.DataSetName = "dsWholeSaleInvoice"
        Me.DsWholeSaleInvoice.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsWholeSaleInvoiceBindingSource
        '
        Me.DsWholeSaleInvoiceBindingSource.DataSource = Me.DsWholeSaleInvoice
        Me.DsWholeSaleInvoiceBindingSource.Position = 0
        '
        'lblTimer
        '
        Me.lblTimer.BackColor = System.Drawing.Color.Gold
        Me.lblTimer.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimer.Location = New System.Drawing.Point(1263, 6)
        Me.lblTimer.Name = "lblTimer"
        Me.lblTimer.Size = New System.Drawing.Size(79, 32)
        Me.lblTimer.TabIndex = 65
        Me.lblTimer.Text = "180"
        '
        'txtGoldPrice
        '
        Me.txtGoldPrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtGoldPrice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoldPrice.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldPrice.ForeColor = System.Drawing.Color.Red
        Me.txtGoldPrice.Location = New System.Drawing.Point(87, 14)
        Me.txtGoldPrice.Name = "txtGoldPrice"
        Me.txtGoldPrice.Size = New System.Drawing.Size(89, 25)
        Me.txtGoldPrice.TabIndex = 67
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(-1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 23)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "Gold Price"
        '
        'cboSaleType
        '
        Me.cboSaleType.FormattingEnabled = True
        Me.cboSaleType.Items.AddRange(New Object() {"Sale", "Whole Sale"})
        Me.cboSaleType.Location = New System.Drawing.Point(1140, 51)
        Me.cboSaleType.Name = "cboSaleType"
        Me.cboSaleType.Size = New System.Drawing.Size(75, 21)
        Me.cboSaleType.TabIndex = 68
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Location = New System.Drawing.Point(-34, 43)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(215, 594)
        Me.ListView1.TabIndex = 37
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label8.Location = New System.Drawing.Point(16, 314)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 20)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Cash"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label9.Location = New System.Drawing.Point(16, 372)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 20)
        Me.Label9.TabIndex = 47
        Me.Label9.Text = "Credit"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label10.Location = New System.Drawing.Point(16, 422)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 20)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "Total"
        '
        'txtTotalCreidt
        '
        Me.txtTotalCreidt.BackColor = System.Drawing.Color.Gray
        Me.txtTotalCreidt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalCreidt.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCreidt.ForeColor = System.Drawing.Color.Gold
        Me.txtTotalCreidt.Location = New System.Drawing.Point(514, 6)
        Me.txtTotalCreidt.Name = "txtTotalCreidt"
        Me.txtTotalCreidt.Size = New System.Drawing.Size(99, 25)
        Me.txtTotalCreidt.TabIndex = 70
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Gray
        Me.Label19.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label19.Location = New System.Drawing.Point(370, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(138, 20)
        Me.Label19.TabIndex = 69
        Me.Label19.Text = "Total Credit Balance"
        '
        'txtTotalStock
        '
        Me.txtTotalStock.BackColor = System.Drawing.Color.Gray
        Me.txtTotalStock.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalStock.Font = New System.Drawing.Font("Myanmar3", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalStock.ForeColor = System.Drawing.Color.Gold
        Me.txtTotalStock.Location = New System.Drawing.Point(818, 6)
        Me.txtTotalStock.Name = "txtTotalStock"
        Me.txtTotalStock.Size = New System.Drawing.Size(99, 25)
        Me.txtTotalStock.TabIndex = 72
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Gray
        Me.Label20.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label20.Location = New System.Drawing.Point(664, 6)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(136, 20)
        Me.Label20.TabIndex = 71
        Me.Label20.Text = "Total Stock Balance"
        '
        'txtGTotal
        '
        Me.txtGTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtGTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGTotal.Font = New System.Drawing.Font("Myanmar3", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGTotal.ForeColor = System.Drawing.Color.Gold
        Me.txtGTotal.Location = New System.Drawing.Point(69, 530)
        Me.txtGTotal.Name = "txtGTotal"
        Me.txtGTotal.Size = New System.Drawing.Size(99, 29)
        Me.txtGTotal.TabIndex = 74
        Me.txtGTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label21.Font = New System.Drawing.Font("Myanmar3", 12.0!)
        Me.Label21.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label21.Location = New System.Drawing.Point(1, 506)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(97, 23)
        Me.Label21.TabIndex = 73
        Me.Label21.Text = "Grand Total"
        '
        'Timer1
        '
        Me.Timer1.Enabled = Global.UI.My.MySettings.Default.Timer
        '
        'frm_Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(1358, 638)
        Me.Controls.Add(Me.txtGTotal)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtTotalStock)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtTotalCreidt)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.cboSaleType)
        Me.Controls.Add(Me.txtGoldPrice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblTimer)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.CboType)
        Me.Controls.Add(Me.Type)
        Me.Controls.Add(Me.cboItemType)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.CboBalanceType)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.txtWSaleBalance)
        Me.Controls.Add(Me.txtWSaleCredit)
        Me.Controls.Add(Me.txtWSaleCash)
        Me.Controls.Add(Me.txtSaleBalance)
        Me.Controls.Add(Me.txtSaleCredit)
        Me.Controls.Add(Me.txtSaleCash)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblSale)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboSaleCategoryType)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.dtpSaleCategoryTDate)
        Me.Controls.Add(Me.dtpSaleCategoryFDate)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboSortingType)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.dtpSaleTDate)
        Me.Controls.Add(Me.dtpSaleFDate)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboCashSaleType)
        Me.Controls.Add(Me.cboCashdate)
        Me.Controls.Add(Me.SaleCReportViewer)
        Me.Controls.Add(Me.StockBalanceReportViewer)
        Me.Controls.Add(Me.SaleReportViewer)
        Me.Controls.Add(Me.rptViewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frm_Dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frm_Dashboard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DsSaleInvoiceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsWholeSaleInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsWholeSaleInvoiceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DsSaleInvoice As UI.dsSaleInvoice
    Friend WithEvents DsSaleInvoiceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsWholeSaleInvoice As UI.dsWholeSaleInvoice
    Friend WithEvents DsWholeSaleInvoiceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents SaleReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents SaleCReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents StockBalanceReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents cboCashdate As System.Windows.Forms.ComboBox
    Friend WithEvents cboCashSaleType As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents dtpSaleTDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSaleFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboSortingType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboSaleCategoryType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpSaleCategoryTDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSaleCategoryFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSale As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSaleCash As System.Windows.Forms.TextBox
    Friend WithEvents txtSaleCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtSaleBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtWSaleCash As System.Windows.Forms.TextBox
    Friend WithEvents txtWSaleCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtWSaleBalance As System.Windows.Forms.TextBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CboBalanceType As System.Windows.Forms.ComboBox
    Friend WithEvents Type As System.Windows.Forms.Label
    Friend WithEvents cboItemType As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CboType As System.Windows.Forms.ComboBox
    Friend WithEvents Timer4 As System.Windows.Forms.Timer
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Private WithEvents DBTimer As System.Windows.Forms.Timer
    Friend WithEvents lblTimer As System.Windows.Forms.Label
    Friend WithEvents txtGoldPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboSaleType As System.Windows.Forms.ComboBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTotalCreidt As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTotalStock As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtGTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
End Class
