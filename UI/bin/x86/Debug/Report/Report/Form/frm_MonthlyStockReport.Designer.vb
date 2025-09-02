<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MonthlyStockReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MonthlyStockReport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpBoxView = New System.Windows.Forms.GroupBox()
        Me.radOffline = New System.Windows.Forms.RadioButton()
        Me.radOnline = New System.Windows.Forms.RadioButton()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.chkDailyStockTransaction = New System.Windows.Forms.CheckBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpBoxDate = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cboItemCat = New System.Windows.Forms.ComboBox()
        Me.chkItemCat = New System.Windows.Forms.CheckBox()
        Me.cboGoldQ = New System.Windows.Forms.ComboBox()
        Me.chkGoldQly = New System.Windows.Forms.CheckBox()
        Me.RptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.RadioButton7 = New System.Windows.Forms.RadioButton()
        Me.RadioButton8 = New System.Windows.Forms.RadioButton()
        Me.RadioButton9 = New System.Windows.Forms.RadioButton()
        Me.RadioButton10 = New System.Windows.Forms.RadioButton()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.RadioButton11 = New System.Windows.Forms.RadioButton()
        Me.RadioButton12 = New System.Windows.Forms.RadioButton()
        Me.RadioButton13 = New System.Windows.Forms.RadioButton()
        Me.RadioButton14 = New System.Windows.Forms.RadioButton()
        Me.radGivenDate = New System.Windows.Forms.RadioButton()
        Me.radTransferDate = New System.Windows.Forms.RadioButton()
        Me.gptDateType = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        Me.grpBoxView.SuspendLayout()
        Me.grpBoxDate.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.gptDateType.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.gptDateType)
        Me.Panel1.Controls.Add(Me.grpBoxView)
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.btnHelp)
        Me.Panel1.Controls.Add(Me.chkDailyStockTransaction)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.grpBoxDate)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1293, 181)
        Me.Panel1.TabIndex = 2
        '
        'grpBoxView
        '
        Me.grpBoxView.Controls.Add(Me.GroupBox2)
        Me.grpBoxView.Controls.Add(Me.GroupBox1)
        Me.grpBoxView.Controls.Add(Me.radOffline)
        Me.grpBoxView.Controls.Add(Me.radOnline)
        Me.grpBoxView.Controls.Add(Me.GroupBox5)
        Me.grpBoxView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxView.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpBoxView.Location = New System.Drawing.Point(641, 10)
        Me.grpBoxView.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpBoxView.Name = "grpBoxView"
        Me.grpBoxView.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.grpBoxView.Size = New System.Drawing.Size(279, 58)
        Me.grpBoxView.TabIndex = 1481
        Me.grpBoxView.TabStop = False
        Me.grpBoxView.Text = " Transfer"
        '
        'radOffline
        '
        Me.radOffline.AutoSize = True
        Me.radOffline.Checked = True
        Me.radOffline.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radOffline.Location = New System.Drawing.Point(6, 23)
        Me.radOffline.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radOffline.Name = "radOffline"
        Me.radOffline.Size = New System.Drawing.Size(128, 21)
        Me.radOffline.TabIndex = 0
        Me.radOffline.TabStop = True
        Me.radOffline.Text = "Offline Transfer"
        Me.radOffline.UseVisualStyleBackColor = True
        '
        'radOnline
        '
        Me.radOnline.AutoSize = True
        Me.radOnline.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radOnline.Location = New System.Drawing.Point(139, 22)
        Me.radOnline.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radOnline.Name = "radOnline"
        Me.radOnline.Size = New System.Drawing.Size(126, 21)
        Me.radOnline.TabIndex = 1
        Me.radOnline.Text = "Online Transfer"
        Me.radOnline.UseVisualStyleBackColor = True
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(354, 38)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1479
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(448, 36)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(171, 27)
        Me.CboLocation.TabIndex = 1480
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.Color.White
        Me.btnHelp.BackgroundImage = CType(resources.GetObject("btnHelp.BackgroundImage"), System.Drawing.Image)
        Me.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.Location = New System.Drawing.Point(586, 3)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(33, 32)
        Me.btnHelp.TabIndex = 1477
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'chkDailyStockTransaction
        '
        Me.chkDailyStockTransaction.AutoSize = True
        Me.chkDailyStockTransaction.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkDailyStockTransaction.ForeColor = System.Drawing.Color.Red
        Me.chkDailyStockTransaction.Location = New System.Drawing.Point(355, 87)
        Me.chkDailyStockTransaction.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkDailyStockTransaction.Name = "chkDailyStockTransaction"
        Me.chkDailyStockTransaction.Size = New System.Drawing.Size(218, 21)
        Me.chkDailyStockTransaction.TabIndex = 1475
        Me.chkDailyStockTransaction.Text = "နေ့စဉ်ပစ္စည်းအ၀င်အထွက်စာရင်းချုပ်"
        Me.chkDailyStockTransaction.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(358, 118)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 34)
        Me.btnPreview.TabIndex = 1472
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
        Me.btnClose.Location = New System.Drawing.Point(479, 118)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 34)
        Me.btnClose.TabIndex = 1473
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1335, 4)
        Me.btnHelpbook.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 48)
        Me.btnHelpbook.TabIndex = 1471
        Me.btnHelpbook.UseVisualStyleBackColor = False
        Me.btnHelpbook.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label2.Location = New System.Drawing.Point(1261, 209)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 200
        Me.Label2.Text = "Counter"
        Me.Label2.Visible = False
        '
        'grpBoxDate
        '
        Me.grpBoxDate.Controls.Add(Me.Label7)
        Me.grpBoxDate.Controls.Add(Me.Label6)
        Me.grpBoxDate.Controls.Add(Me.dtpToDate)
        Me.grpBoxDate.Controls.Add(Me.dtpFromDate)
        Me.grpBoxDate.Controls.Add(Me.Label1)
        Me.grpBoxDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpBoxDate.Location = New System.Drawing.Point(12, 13)
        Me.grpBoxDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpBoxDate.Name = "grpBoxDate"
        Me.grpBoxDate.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpBoxDate.Size = New System.Drawing.Size(334, 55)
        Me.grpBoxDate.TabIndex = 2
        Me.grpBoxDate.TabStop = False
        Me.grpBoxDate.Text = "နေ့စွဲ"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(168, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "အထိ"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(21, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 17)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "မှ"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy "
        Me.dtpToDate.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(207, 21)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(117, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy "
        Me.dtpFromDate.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(43, 21)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(119, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(31, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "From"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cboItemCat)
        Me.GroupBox3.Controls.Add(Me.chkItemCat)
        Me.GroupBox3.Controls.Add(Me.cboGoldQ)
        Me.GroupBox3.Controls.Add(Me.chkGoldQly)
        Me.GroupBox3.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox3.Location = New System.Drawing.Point(12, 74)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(334, 93)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select By"
        '
        'cboItemCat
        '
        Me.cboItemCat.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboItemCat.FormattingEnabled = True
        Me.cboItemCat.Location = New System.Drawing.Point(104, 54)
        Me.cboItemCat.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboItemCat.Name = "cboItemCat"
        Me.cboItemCat.Size = New System.Drawing.Size(214, 27)
        Me.cboItemCat.TabIndex = 3
        '
        'chkItemCat
        '
        Me.chkItemCat.AutoSize = True
        Me.chkItemCat.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemCat.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCat.Location = New System.Drawing.Point(12, 57)
        Me.chkItemCat.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkItemCat.Name = "chkItemCat"
        Me.chkItemCat.Size = New System.Drawing.Size(91, 21)
        Me.chkItemCat.TabIndex = 2
        Me.chkItemCat.Text = "အမျိုးအစား"
        Me.chkItemCat.UseVisualStyleBackColor = True
        '
        'cboGoldQ
        '
        Me.cboGoldQ.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoldQ.FormattingEnabled = True
        Me.cboGoldQ.Location = New System.Drawing.Point(104, 17)
        Me.cboGoldQ.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboGoldQ.Name = "cboGoldQ"
        Me.cboGoldQ.Size = New System.Drawing.Size(214, 27)
        Me.cboGoldQ.TabIndex = 1
        '
        'chkGoldQly
        '
        Me.chkGoldQly.AutoSize = True
        Me.chkGoldQly.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldQly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQly.Location = New System.Drawing.Point(12, 20)
        Me.chkGoldQly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkGoldQly.Name = "chkGoldQly"
        Me.chkGoldQly.Size = New System.Drawing.Size(63, 21)
        Me.chkGoldQly.TabIndex = 0
        Me.chkGoldQly.Text = "ရွှေရည်"
        Me.chkGoldQly.UseVisualStyleBackColor = True
        '
        'RptViewer
        '
        Me.RptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RptViewer.Location = New System.Drawing.Point(0, 181)
        Me.RptViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RptViewer.Name = "RptViewer"
        Me.RptViewer.Size = New System.Drawing.Size(1293, 517)
        Me.RptViewer.TabIndex = 3
        Me.RptViewer.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(2, 64)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(271, 58)
        Me.GroupBox1.TabIndex = 1482
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Transfer"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton1.Location = New System.Drawing.Point(6, 23)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(128, 21)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Offline Transfer"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton2.Location = New System.Drawing.Point(140, 22)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(126, 21)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "Online Transfer"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.RadioButton5)
        Me.GroupBox2.Controls.Add(Me.RadioButton6)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox2.Location = New System.Drawing.Point(3, 66)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(271, 58)
        Me.GroupBox2.TabIndex = 1483
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Transfer"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RadioButton3)
        Me.GroupBox4.Controls.Add(Me.RadioButton4)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox4.Location = New System.Drawing.Point(2, 64)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox4.Size = New System.Drawing.Size(271, 58)
        Me.GroupBox4.TabIndex = 1482
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = " Transfer"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Checked = True
        Me.RadioButton3.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton3.Location = New System.Drawing.Point(6, 23)
        Me.RadioButton3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(128, 21)
        Me.RadioButton3.TabIndex = 0
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Offline Transfer"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton4.Location = New System.Drawing.Point(140, 22)
        Me.RadioButton4.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(126, 21)
        Me.RadioButton4.TabIndex = 1
        Me.RadioButton4.Text = "Online Transfer"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Checked = True
        Me.RadioButton5.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton5.Location = New System.Drawing.Point(6, 23)
        Me.RadioButton5.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(128, 21)
        Me.RadioButton5.TabIndex = 0
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "Offline Transfer"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton6.Location = New System.Drawing.Point(140, 22)
        Me.RadioButton6.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(126, 21)
        Me.RadioButton6.TabIndex = 1
        Me.RadioButton6.Text = "Online Transfer"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.GroupBox6)
        Me.GroupBox5.Controls.Add(Me.GroupBox8)
        Me.GroupBox5.Controls.Add(Me.RadioButton13)
        Me.GroupBox5.Controls.Add(Me.RadioButton14)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox5.Location = New System.Drawing.Point(8, 68)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox5.Size = New System.Drawing.Size(271, 58)
        Me.GroupBox5.TabIndex = 1484
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = " Transfer"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Controls.Add(Me.RadioButton9)
        Me.GroupBox6.Controls.Add(Me.RadioButton10)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox6.Location = New System.Drawing.Point(3, 66)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox6.Size = New System.Drawing.Size(271, 58)
        Me.GroupBox6.TabIndex = 1483
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = " Transfer"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.RadioButton7)
        Me.GroupBox7.Controls.Add(Me.RadioButton8)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox7.Location = New System.Drawing.Point(2, 64)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox7.Size = New System.Drawing.Size(271, 58)
        Me.GroupBox7.TabIndex = 1482
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = " Transfer"
        '
        'RadioButton7
        '
        Me.RadioButton7.AutoSize = True
        Me.RadioButton7.Checked = True
        Me.RadioButton7.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton7.Location = New System.Drawing.Point(6, 23)
        Me.RadioButton7.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(128, 21)
        Me.RadioButton7.TabIndex = 0
        Me.RadioButton7.TabStop = True
        Me.RadioButton7.Text = "Offline Transfer"
        Me.RadioButton7.UseVisualStyleBackColor = True
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = True
        Me.RadioButton8.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton8.Location = New System.Drawing.Point(140, 22)
        Me.RadioButton8.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(126, 21)
        Me.RadioButton8.TabIndex = 1
        Me.RadioButton8.Text = "Online Transfer"
        Me.RadioButton8.UseVisualStyleBackColor = True
        '
        'RadioButton9
        '
        Me.RadioButton9.AutoSize = True
        Me.RadioButton9.Checked = True
        Me.RadioButton9.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton9.Location = New System.Drawing.Point(6, 23)
        Me.RadioButton9.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton9.Name = "RadioButton9"
        Me.RadioButton9.Size = New System.Drawing.Size(128, 21)
        Me.RadioButton9.TabIndex = 0
        Me.RadioButton9.TabStop = True
        Me.RadioButton9.Text = "Offline Transfer"
        Me.RadioButton9.UseVisualStyleBackColor = True
        '
        'RadioButton10
        '
        Me.RadioButton10.AutoSize = True
        Me.RadioButton10.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton10.Location = New System.Drawing.Point(140, 22)
        Me.RadioButton10.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton10.Name = "RadioButton10"
        Me.RadioButton10.Size = New System.Drawing.Size(126, 21)
        Me.RadioButton10.TabIndex = 1
        Me.RadioButton10.Text = "Online Transfer"
        Me.RadioButton10.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.RadioButton11)
        Me.GroupBox8.Controls.Add(Me.RadioButton12)
        Me.GroupBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox8.Location = New System.Drawing.Point(2, 64)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox8.Size = New System.Drawing.Size(271, 58)
        Me.GroupBox8.TabIndex = 1482
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = " Transfer"
        '
        'RadioButton11
        '
        Me.RadioButton11.AutoSize = True
        Me.RadioButton11.Checked = True
        Me.RadioButton11.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton11.Location = New System.Drawing.Point(6, 23)
        Me.RadioButton11.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton11.Name = "RadioButton11"
        Me.RadioButton11.Size = New System.Drawing.Size(128, 21)
        Me.RadioButton11.TabIndex = 0
        Me.RadioButton11.TabStop = True
        Me.RadioButton11.Text = "Offline Transfer"
        Me.RadioButton11.UseVisualStyleBackColor = True
        '
        'RadioButton12
        '
        Me.RadioButton12.AutoSize = True
        Me.RadioButton12.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton12.Location = New System.Drawing.Point(140, 22)
        Me.RadioButton12.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton12.Name = "RadioButton12"
        Me.RadioButton12.Size = New System.Drawing.Size(126, 21)
        Me.RadioButton12.TabIndex = 1
        Me.RadioButton12.Text = "Online Transfer"
        Me.RadioButton12.UseVisualStyleBackColor = True
        '
        'RadioButton13
        '
        Me.RadioButton13.AutoSize = True
        Me.RadioButton13.Checked = True
        Me.RadioButton13.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton13.Location = New System.Drawing.Point(6, 23)
        Me.RadioButton13.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton13.Name = "RadioButton13"
        Me.RadioButton13.Size = New System.Drawing.Size(128, 21)
        Me.RadioButton13.TabIndex = 0
        Me.RadioButton13.TabStop = True
        Me.RadioButton13.Text = "Offline Transfer"
        Me.RadioButton13.UseVisualStyleBackColor = True
        '
        'RadioButton14
        '
        Me.RadioButton14.AutoSize = True
        Me.RadioButton14.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.RadioButton14.Location = New System.Drawing.Point(140, 22)
        Me.RadioButton14.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.RadioButton14.Name = "RadioButton14"
        Me.RadioButton14.Size = New System.Drawing.Size(126, 21)
        Me.RadioButton14.TabIndex = 1
        Me.RadioButton14.Text = "Online Transfer"
        Me.RadioButton14.UseVisualStyleBackColor = True
        '
        'radGivenDate
        '
        Me.radGivenDate.AutoSize = True
        Me.radGivenDate.Checked = True
        Me.radGivenDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radGivenDate.Location = New System.Drawing.Point(6, 20)
        Me.radGivenDate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radGivenDate.Name = "radGivenDate"
        Me.radGivenDate.Size = New System.Drawing.Size(121, 21)
        Me.radGivenDate.TabIndex = 0
        Me.radGivenDate.TabStop = True
        Me.radGivenDate.Text = "By Given Date"
        Me.radGivenDate.UseVisualStyleBackColor = True
        '
        'radTransferDate
        '
        Me.radTransferDate.AutoSize = True
        Me.radTransferDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.radTransferDate.Location = New System.Drawing.Point(139, 19)
        Me.radTransferDate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.radTransferDate.Name = "radTransferDate"
        Me.radTransferDate.Size = New System.Drawing.Size(134, 21)
        Me.radTransferDate.TabIndex = 1
        Me.radTransferDate.Text = "By Transfer Date"
        Me.radTransferDate.UseVisualStyleBackColor = True
        '
        'gptDateType
        '
        Me.gptDateType.Controls.Add(Me.radTransferDate)
        Me.gptDateType.Controls.Add(Me.radGivenDate)
        Me.gptDateType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gptDateType.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.gptDateType.Location = New System.Drawing.Point(641, 89)
        Me.gptDateType.Name = "gptDateType"
        Me.gptDateType.Size = New System.Drawing.Size(279, 51)
        Me.gptDateType.TabIndex = 1482
        Me.gptDateType.TabStop = False
        Me.gptDateType.Text = "Date Type"
        '
        'frm_MonthlyStockReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1293, 698)
        Me.Controls.Add(Me.RptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_MonthlyStockReport"
        Me.Text = "Monthly Stock Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpBoxView.ResumeLayout(False)
        Me.grpBoxView.PerformLayout()
        Me.grpBoxDate.ResumeLayout(False)
        Me.grpBoxDate.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.gptDateType.ResumeLayout(False)
        Me.gptDateType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpBoxDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cboItemCat As System.Windows.Forms.ComboBox
    Friend WithEvents chkItemCat As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQ As System.Windows.Forms.ComboBox
    Friend WithEvents chkGoldQly As System.Windows.Forms.CheckBox
    Friend WithEvents RptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents chkDailyStockTransaction As System.Windows.Forms.CheckBox
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents grpBoxView As System.Windows.Forms.GroupBox
    Friend WithEvents radOffline As System.Windows.Forms.RadioButton
    Friend WithEvents radOnline As System.Windows.Forms.RadioButton
    Friend WithEvents gptDateType As System.Windows.Forms.GroupBox
    Friend WithEvents radTransferDate As System.Windows.Forms.RadioButton
    Friend WithEvents radGivenDate As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton6 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton7 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton8 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton9 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton10 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton11 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton12 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton13 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton14 As System.Windows.Forms.RadioButton
End Class
