<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_DailyExpenseIncome
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_DailyExpenseIncome))
        Me.grpview = New System.Windows.Forms.GroupBox()
        Me.radSummary = New System.Windows.Forms.RadioButton()
        Me.radDetail = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optBank = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.chkIsBank = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optIncome = New System.Windows.Forms.RadioButton()
        Me.optExpense = New System.Windows.Forms.RadioButton()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.chkLocation = New System.Windows.Forms.CheckBox()
        Me.grpDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.grpview.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpview
        '
        Me.grpview.BackColor = System.Drawing.SystemColors.Control
        Me.grpview.Controls.Add(Me.radSummary)
        Me.grpview.Controls.Add(Me.radDetail)
        Me.grpview.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpview.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpview.Location = New System.Drawing.Point(343, 12)
        Me.grpview.Name = "grpview"
        Me.grpview.Size = New System.Drawing.Size(247, 55)
        Me.grpview.TabIndex = 2
        Me.grpview.TabStop = False
        Me.grpview.Text = "View"
        '
        'radSummary
        '
        Me.radSummary.AutoSize = True
        Me.radSummary.BackColor = System.Drawing.SystemColors.Control
        Me.radSummary.Checked = True
        Me.radSummary.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.radSummary.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radSummary.Location = New System.Drawing.Point(47, 21)
        Me.radSummary.Name = "radSummary"
        Me.radSummary.Size = New System.Drawing.Size(84, 21)
        Me.radSummary.TabIndex = 0
        Me.radSummary.TabStop = True
        Me.radSummary.Text = "Summary"
        Me.radSummary.UseVisualStyleBackColor = False
        '
        'radDetail
        '
        Me.radDetail.AutoSize = True
        Me.radDetail.BackColor = System.Drawing.SystemColors.Control
        Me.radDetail.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.radDetail.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radDetail.Location = New System.Drawing.Point(156, 21)
        Me.radDetail.Name = "radDetail"
        Me.radDetail.Size = New System.Drawing.Size(66, 21)
        Me.radDetail.TabIndex = 1
        Me.radDetail.Text = "Detail"
        Me.radDetail.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.optBank)
        Me.Panel1.Controls.Add(Me.optCash)
        Me.Panel1.Controls.Add(Me.optAll)
        Me.Panel1.Controls.Add(Me.chkIsBank)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.grpDate)
        Me.Panel1.Controls.Add(Me.grpview)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(995, 146)
        Me.Panel1.TabIndex = 0
        '
        'optBank
        '
        Me.optBank.AutoSize = True
        Me.optBank.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optBank.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optBank.Location = New System.Drawing.Point(766, 31)
        Me.optBank.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optBank.Name = "optBank"
        Me.optBank.Size = New System.Drawing.Size(59, 21)
        Me.optBank.TabIndex = 233
        Me.optBank.Text = "Bank"
        Me.optBank.UseVisualStyleBackColor = True
        '
        'optCash
        '
        Me.optCash.AutoSize = True
        Me.optCash.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optCash.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optCash.Location = New System.Drawing.Point(678, 32)
        Me.optCash.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(57, 21)
        Me.optCash.TabIndex = 232
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = True
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Checked = True
        Me.optAll.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optAll.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optAll.Location = New System.Drawing.Point(605, 32)
        Me.optAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(47, 21)
        Me.optAll.TabIndex = 231
        Me.optAll.TabStop = True
        Me.optAll.Text = "All"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'chkIsBank
        '
        Me.chkIsBank.AutoSize = True
        Me.chkIsBank.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsBank.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkIsBank.Location = New System.Drawing.Point(884, 61)
        Me.chkIsBank.Name = "chkIsBank"
        Me.chkIsBank.Size = New System.Drawing.Size(71, 21)
        Me.chkIsBank.TabIndex = 223
        Me.chkIsBank.Text = "IsBank"
        Me.chkIsBank.UseVisualStyleBackColor = True
        Me.chkIsBank.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optIncome)
        Me.GroupBox1.Controls.Add(Me.optExpense)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(18, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(307, 58)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'optIncome
        '
        Me.optIncome.AutoSize = True
        Me.optIncome.BackColor = System.Drawing.SystemColors.Control
        Me.optIncome.Checked = True
        Me.optIncome.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optIncome.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optIncome.Location = New System.Drawing.Point(14, 22)
        Me.optIncome.Name = "optIncome"
        Me.optIncome.Size = New System.Drawing.Size(112, 21)
        Me.optIncome.TabIndex = 0
        Me.optIncome.TabStop = True
        Me.optIncome.Text = "Daily Income"
        Me.optIncome.UseVisualStyleBackColor = False
        '
        'optExpense
        '
        Me.optExpense.AutoSize = True
        Me.optExpense.BackColor = System.Drawing.SystemColors.Control
        Me.optExpense.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExpense.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optExpense.Location = New System.Drawing.Point(144, 22)
        Me.optExpense.Name = "optExpense"
        Me.optExpense.Size = New System.Drawing.Size(119, 21)
        Me.optExpense.TabIndex = 1
        Me.optExpense.Text = "Daily Expense"
        Me.optExpense.UseVisualStyleBackColor = False
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(884, 19)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 5
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboLocation)
        Me.GroupBox2.Controls.Add(Me.chkLocation)
        Me.GroupBox2.Location = New System.Drawing.Point(604, 79)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(273, 51)
        Me.GroupBox2.TabIndex = 220
        Me.GroupBox2.TabStop = False
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(92, 13)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(174, 26)
        Me.cboLocation.TabIndex = 22
        '
        'chkLocation
        '
        Me.chkLocation.AutoSize = True
        Me.chkLocation.BackColor = System.Drawing.Color.Transparent
        Me.chkLocation.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.chkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkLocation.Location = New System.Drawing.Point(5, 17)
        Me.chkLocation.Name = "chkLocation"
        Me.chkLocation.Size = New System.Drawing.Size(84, 21)
        Me.chkLocation.TabIndex = 18
        Me.chkLocation.Text = "Location"
        Me.chkLocation.UseVisualStyleBackColor = False
        '
        'grpDate
        '
        Me.grpDate.Controls.Add(Me.dtpToDate)
        Me.grpDate.Controls.Add(Me.Label1)
        Me.grpDate.Controls.Add(Me.dtpFromDate)
        Me.grpDate.Controls.Add(Me.Label2)
        Me.grpDate.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpDate.Location = New System.Drawing.Point(18, 73)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(307, 58)
        Me.grpDate.TabIndex = 1
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(178, 21)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(99, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(11, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(55, 21)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(95, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(152, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To"
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(357, 94)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 3
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
        Me.btnClose.Location = New System.Drawing.Point(478, 94)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rptViewer
        '
        Me.rptViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 146)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(995, 449)
        Me.rptViewer.TabIndex = 1
        Me.rptViewer.TabStop = False
        '
        'frm_rpt_DailyExpenseIncome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(995, 595)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_rpt_DailyExpenseIncome"
        Me.Text = "Daily Income/Daily Expense"
        Me.grpview.ResumeLayout(False)
        Me.grpview.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpDate.ResumeLayout(False)
        Me.grpDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpview As System.Windows.Forms.GroupBox
    Friend WithEvents radSummary As System.Windows.Forms.RadioButton
    Friend WithEvents radDetail As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents chkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents grpDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optIncome As System.Windows.Forms.RadioButton
    Friend WithEvents optExpense As System.Windows.Forms.RadioButton
    Friend WithEvents chkIsBank As System.Windows.Forms.CheckBox
    Friend WithEvents optBank As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
End Class
