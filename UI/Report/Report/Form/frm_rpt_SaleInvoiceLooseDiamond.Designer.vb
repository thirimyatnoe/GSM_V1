<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_SaleInvoiceLooseDiamond
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_SaleInvoiceLooseDiamond))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.lblBarcodeNo = New System.Windows.Forms.Label()
        Me.txtBarcodeNo = New System.Windows.Forms.TextBox()
        Me.chkStaff = New System.Windows.Forms.CheckBox()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.chkGemsCategory = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radSummary = New System.Windows.Forms.RadioButton()
        Me.radDetail = New System.Windows.Forms.RadioButton()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1070, 146)
        Me.Panel1.TabIndex = 0
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1015, 7)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 6
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.ChkLocation)
        Me.GroupBox2.Controls.Add(Me.CboLocation)
        Me.GroupBox2.Controls.Add(Me.lblBarcodeNo)
        Me.GroupBox2.Controls.Add(Me.txtBarcodeNo)
        Me.GroupBox2.Controls.Add(Me.chkStaff)
        Me.GroupBox2.Controls.Add(Me.cboStaff)
        Me.GroupBox2.Controls.Add(Me.chkGemsCategory)
        Me.GroupBox2.Controls.Add(Me.cboCategory)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(364, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(637, 92)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(333, 56)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1507
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(430, 53)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(195, 27)
        Me.CboLocation.TabIndex = 1508
        '
        'lblBarcodeNo
        '
        Me.lblBarcodeNo.AutoSize = True
        Me.lblBarcodeNo.BackColor = System.Drawing.Color.Transparent
        Me.lblBarcodeNo.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblBarcodeNo.ForeColor = System.Drawing.Color.Blue
        Me.lblBarcodeNo.Location = New System.Drawing.Point(333, 18)
        Me.lblBarcodeNo.Name = "lblBarcodeNo"
        Me.lblBarcodeNo.Size = New System.Drawing.Size(79, 17)
        Me.lblBarcodeNo.TabIndex = 905
        Me.lblBarcodeNo.Text = "BarcodeNo"
        '
        'txtBarcodeNo
        '
        Me.txtBarcodeNo.BackColor = System.Drawing.Color.White
        Me.txtBarcodeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcodeNo.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtBarcodeNo.Location = New System.Drawing.Point(430, 17)
        Me.txtBarcodeNo.Name = "txtBarcodeNo"
        Me.txtBarcodeNo.Size = New System.Drawing.Size(195, 24)
        Me.txtBarcodeNo.TabIndex = 904
        Me.txtBarcodeNo.Text = " "
        '
        'chkStaff
        '
        Me.chkStaff.AutoSize = True
        Me.chkStaff.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStaff.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkStaff.Location = New System.Drawing.Point(12, 56)
        Me.chkStaff.Name = "chkStaff"
        Me.chkStaff.Size = New System.Drawing.Size(69, 21)
        Me.chkStaff.TabIndex = 7
        Me.chkStaff.Text = "ဝန်ထမ်း"
        Me.chkStaff.UseVisualStyleBackColor = True
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(126, 55)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(195, 27)
        Me.cboStaff.TabIndex = 6
        '
        'chkGemsCategory
        '
        Me.chkGemsCategory.AutoSize = True
        Me.chkGemsCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGemsCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGemsCategory.Location = New System.Drawing.Point(12, 21)
        Me.chkGemsCategory.Name = "chkGemsCategory"
        Me.chkGemsCategory.Size = New System.Drawing.Size(107, 21)
        Me.chkGemsCategory.TabIndex = 3
        Me.chkGemsCategory.Text = "စိန်အမျိုးအစား"
        Me.chkGemsCategory.UseVisualStyleBackColor = True
        '
        'cboCategory
        '
        Me.cboCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(127, 16)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(194, 27)
        Me.cboCategory.TabIndex = 2
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(818, 104)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(88, 31)
        Me.btnPreview.TabIndex = 4
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
        Me.btnClose.Location = New System.Drawing.Point(912, 104)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radSummary)
        Me.GroupBox1.Controls.Add(Me.radDetail)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(346, 49)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " View"
        '
        'radSummary
        '
        Me.radSummary.AutoSize = True
        Me.radSummary.Checked = True
        Me.radSummary.Location = New System.Drawing.Point(67, 16)
        Me.radSummary.Name = "radSummary"
        Me.radSummary.Size = New System.Drawing.Size(84, 21)
        Me.radSummary.TabIndex = 0
        Me.radSummary.TabStop = True
        Me.radSummary.Text = "Summary"
        Me.radSummary.UseVisualStyleBackColor = True
        '
        'radDetail
        '
        Me.radDetail.AutoSize = True
        Me.radDetail.Location = New System.Drawing.Point(202, 16)
        Me.radDetail.Name = "radDetail"
        Me.radDetail.Size = New System.Drawing.Size(66, 21)
        Me.radDetail.TabIndex = 0
        Me.radDetail.Text = "Detail"
        Me.radDetail.UseVisualStyleBackColor = True
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
        Me.grpByDate.Location = New System.Drawing.Point(11, 67)
        Me.grpByDate.Name = "grpByDate"
        Me.grpByDate.Size = New System.Drawing.Size(347, 52)
        Me.grpByDate.TabIndex = 0
        Me.grpByDate.TabStop = False
        Me.grpByDate.Text = "Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(226, 18)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(96, 18)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(51, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(199, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "To"
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 146)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(1070, 351)
        Me.rptViewer.TabIndex = 1
        Me.rptViewer.TabStop = False
        '
        'frm_rpt_SaleInvoiceLooseDiamond
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 497)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_rpt_SaleInvoiceLooseDiamond"
        Me.Text = "Sale Invoice(LooseDiamond)"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkStaff As System.Windows.Forms.CheckBox
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents chkGemsCategory As System.Windows.Forms.CheckBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radSummary As System.Windows.Forms.RadioButton
    Friend WithEvents radDetail As System.Windows.Forms.RadioButton
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents lblBarcodeNo As System.Windows.Forms.Label
    Friend WithEvents txtBarcodeNo As System.Windows.Forms.TextBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
