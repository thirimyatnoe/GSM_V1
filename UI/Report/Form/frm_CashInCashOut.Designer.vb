<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CashInCashOut
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CashInCashOut))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.optBank = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkGoldQuality = New System.Windows.Forms.CheckBox()
        Me.chkItemCategory = New System.Windows.Forms.CheckBox()
        Me.cboGoldQuality = New System.Windows.Forms.ComboBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.optBank)
        Me.Panel1.Controls.Add(Me.optCash)
        Me.Panel1.Controls.Add(Me.optAll)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(960, 98)
        Me.Panel1.TabIndex = 0
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(527, 13)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 5
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(614, 10)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(171, 27)
        Me.CboLocation.TabIndex = 6
        '
        'optBank
        '
        Me.optBank.AutoSize = True
        Me.optBank.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optBank.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optBank.Location = New System.Drawing.Point(461, 13)
        Me.optBank.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optBank.Name = "optBank"
        Me.optBank.Size = New System.Drawing.Size(59, 21)
        Me.optBank.TabIndex = 4
        Me.optBank.Text = "Bank"
        Me.optBank.UseVisualStyleBackColor = True
        '
        'optCash
        '
        Me.optCash.AutoSize = True
        Me.optCash.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optCash.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optCash.Location = New System.Drawing.Point(389, 13)
        Me.optCash.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(57, 21)
        Me.optCash.TabIndex = 3
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = True
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Checked = True
        Me.optAll.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optAll.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optAll.Location = New System.Drawing.Point(336, 13)
        Me.optAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(47, 21)
        Me.optAll.TabIndex = 2
        Me.optAll.TabStop = True
        Me.optAll.Text = "All"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(817, 14)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 3
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'grpByDate
        '
        Me.grpByDate.BackColor = System.Drawing.Color.Transparent
        Me.grpByDate.Controls.Add(Me.dtpToDate)
        Me.grpByDate.Controls.Add(Me.dtpFromDate)
        Me.grpByDate.Controls.Add(Me.Label1)
        Me.grpByDate.Controls.Add(Me.Label2)
        Me.grpByDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpByDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpByDate.Location = New System.Drawing.Point(12, 1)
        Me.grpByDate.Name = "grpByDate"
        Me.grpByDate.Size = New System.Drawing.Size(310, 66)
        Me.grpByDate.TabIndex = 0
        Me.grpByDate.TabStop = False
        Me.grpByDate.Text = "Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(194, 23)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.TabStop = False
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(64, 23)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpFromDate.TabIndex = 0
        Me.dtpFromDate.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.59!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(167, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 2
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
        Me.btnPreview.Location = New System.Drawing.Point(336, 55)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 7
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
        Me.btnClose.Location = New System.Drawing.Point(475, 55)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.chkGoldQuality)
        Me.GroupBox2.Controls.Add(Me.chkItemCategory)
        Me.GroupBox2.Controls.Add(Me.cboGoldQuality)
        Me.GroupBox2.Controls.Add(Me.cboCategory)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(827, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(133, 33)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Visible = False
        '
        'chkGoldQuality
        '
        Me.chkGoldQuality.AutoSize = True
        Me.chkGoldQuality.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldQuality.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQuality.Location = New System.Drawing.Point(15, 16)
        Me.chkGoldQuality.Name = "chkGoldQuality"
        Me.chkGoldQuality.Size = New System.Drawing.Size(112, 21)
        Me.chkGoldQuality.TabIndex = 0
        Me.chkGoldQuality.Text = "Gold Quality"
        Me.chkGoldQuality.UseVisualStyleBackColor = True
        '
        'chkItemCategory
        '
        Me.chkItemCategory.AutoSize = True
        Me.chkItemCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCategory.Location = New System.Drawing.Point(16, 59)
        Me.chkItemCategory.Name = "chkItemCategory"
        Me.chkItemCategory.Size = New System.Drawing.Size(117, 21)
        Me.chkItemCategory.TabIndex = 2
        Me.chkItemCategory.Text = "Item Category"
        Me.chkItemCategory.UseVisualStyleBackColor = True
        '
        'cboGoldQuality
        '
        Me.cboGoldQuality.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboGoldQuality.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboGoldQuality.FormattingEnabled = True
        Me.cboGoldQuality.Location = New System.Drawing.Point(28, 15)
        Me.cboGoldQuality.Name = "cboGoldQuality"
        Me.cboGoldQuality.Size = New System.Drawing.Size(72, 27)
        Me.cboGoldQuality.TabIndex = 1
        '
        'cboCategory
        '
        Me.cboCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(43, 56)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(72, 27)
        Me.cboCategory.TabIndex = 3
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 98)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(960, 326)
        Me.rptViewer.TabIndex = 1
        Me.rptViewer.TabStop = False
        '
        'frm_CashInCashOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 424)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_CashInCashOut"
        Me.Text = "Cash In/ Cash Out"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkGoldQuality As System.Windows.Forms.CheckBox
    Friend WithEvents chkItemCategory As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQuality As System.Windows.Forms.ComboBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents optBank As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
