<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_TransferReturn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_TransferReturn))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.cboGoldQ = New System.Windows.Forms.ComboBox()
        Me.chkGoldQly = New System.Windows.Forms.CheckBox()
        Me.cboItemName = New System.Windows.Forms.ComboBox()
        Me.ChkItemName = New System.Windows.Forms.CheckBox()
        Me.chkItemCategory = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.lblBarcodeNo = New System.Windows.Forms.Label()
        Me.txtBarcodeNo = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radSummary = New System.Windows.Forms.RadioButton()
        Me.radDetail = New System.Windows.Forms.RadioButton()
        Me.chkBranch = New System.Windows.Forms.CheckBox()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.chkTRLooseDiamond = New System.Windows.Forms.CheckBox()
        Me.chkGemsCategory = New System.Windows.Forms.CheckBox()
        Me.CboGemsCategory = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.chkTRLooseDiamond)
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.cboGoldQ)
        Me.Panel1.Controls.Add(Me.chkGoldQly)
        Me.Panel1.Controls.Add(Me.cboItemName)
        Me.Panel1.Controls.Add(Me.ChkItemName)
        Me.Panel1.Controls.Add(Me.chkItemCategory)
        Me.Panel1.Controls.Add(Me.cboCategory)
        Me.Panel1.Controls.Add(Me.lblBarcodeNo)
        Me.Panel1.Controls.Add(Me.txtBarcodeNo)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.chkBranch)
        Me.Panel1.Controls.Add(Me.cboBranch)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.chkGemsCategory)
        Me.Panel1.Controls.Add(Me.CboGemsCategory)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1006, 144)
        Me.Panel1.TabIndex = 1
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(678, 47)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 914
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        Me.ChkLocation.Visible = False
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(769, 44)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(171, 27)
        Me.CboLocation.TabIndex = 915
        Me.CboLocation.Visible = False
        '
        'cboGoldQ
        '
        Me.cboGoldQ.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboGoldQ.FormattingEnabled = True
        Me.cboGoldQ.Location = New System.Drawing.Point(471, 110)
        Me.cboGoldQ.Name = "cboGoldQ"
        Me.cboGoldQ.Size = New System.Drawing.Size(200, 27)
        Me.cboGoldQ.TabIndex = 913
        '
        'chkGoldQly
        '
        Me.chkGoldQly.AutoSize = True
        Me.chkGoldQly.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldQly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQly.Location = New System.Drawing.Point(342, 113)
        Me.chkGoldQly.Name = "chkGoldQly"
        Me.chkGoldQly.Size = New System.Drawing.Size(63, 21)
        Me.chkGoldQly.TabIndex = 912
        Me.chkGoldQly.Text = "ရွှေရည်"
        Me.chkGoldQly.UseVisualStyleBackColor = True
        '
        'cboItemName
        '
        Me.cboItemName.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboItemName.FormattingEnabled = True
        Me.cboItemName.Location = New System.Drawing.Point(471, 77)
        Me.cboItemName.Name = "cboItemName"
        Me.cboItemName.Size = New System.Drawing.Size(200, 27)
        Me.cboItemName.TabIndex = 911
        '
        'ChkItemName
        '
        Me.ChkItemName.AutoSize = True
        Me.ChkItemName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkItemName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkItemName.Location = New System.Drawing.Point(342, 80)
        Me.ChkItemName.Name = "ChkItemName"
        Me.ChkItemName.Size = New System.Drawing.Size(126, 21)
        Me.ChkItemName.TabIndex = 910
        Me.ChkItemName.Text = "ပစ္စည်းအမျိုးအမည်"
        Me.ChkItemName.UseVisualStyleBackColor = True
        '
        'chkItemCategory
        '
        Me.chkItemCategory.AutoSize = True
        Me.chkItemCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCategory.Location = New System.Drawing.Point(342, 47)
        Me.chkItemCategory.Name = "chkItemCategory"
        Me.chkItemCategory.Size = New System.Drawing.Size(125, 21)
        Me.chkItemCategory.TabIndex = 908
        Me.chkItemCategory.Text = "ပစ္စည်းအမျိုးအစား"
        Me.chkItemCategory.UseVisualStyleBackColor = True
        '
        'cboCategory
        '
        Me.cboCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(471, 44)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(200, 27)
        Me.cboCategory.TabIndex = 909
        '
        'lblBarcodeNo
        '
        Me.lblBarcodeNo.AutoSize = True
        Me.lblBarcodeNo.BackColor = System.Drawing.Color.Transparent
        Me.lblBarcodeNo.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblBarcodeNo.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblBarcodeNo.Location = New System.Drawing.Point(678, 14)
        Me.lblBarcodeNo.Name = "lblBarcodeNo"
        Me.lblBarcodeNo.Size = New System.Drawing.Size(79, 17)
        Me.lblBarcodeNo.TabIndex = 907
        Me.lblBarcodeNo.Text = "BarcodeNo"
        '
        'txtBarcodeNo
        '
        Me.txtBarcodeNo.BackColor = System.Drawing.Color.White
        Me.txtBarcodeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcodeNo.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtBarcodeNo.Location = New System.Drawing.Point(769, 11)
        Me.txtBarcodeNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtBarcodeNo.Name = "txtBarcodeNo"
        Me.txtBarcodeNo.Size = New System.Drawing.Size(171, 24)
        Me.txtBarcodeNo.TabIndex = 906
        Me.txtBarcodeNo.Text = " "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radSummary)
        Me.GroupBox1.Controls.Add(Me.radDetail)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(309, 45)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View"
        '
        'radSummary
        '
        Me.radSummary.AutoSize = True
        Me.radSummary.Checked = True
        Me.radSummary.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radSummary.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radSummary.Location = New System.Drawing.Point(49, 14)
        Me.radSummary.Name = "radSummary"
        Me.radSummary.Size = New System.Drawing.Size(85, 21)
        Me.radSummary.TabIndex = 0
        Me.radSummary.TabStop = True
        Me.radSummary.Text = "စာရင်းချုပ်"
        Me.radSummary.UseVisualStyleBackColor = True
        '
        'radDetail
        '
        Me.radDetail.AutoSize = True
        Me.radDetail.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radDetail.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.radDetail.Location = New System.Drawing.Point(166, 16)
        Me.radDetail.Name = "radDetail"
        Me.radDetail.Size = New System.Drawing.Size(85, 21)
        Me.radDetail.TabIndex = 1
        Me.radDetail.Text = "အသေးစိတ်"
        Me.radDetail.UseVisualStyleBackColor = True
        '
        'chkBranch
        '
        Me.chkBranch.AutoSize = True
        Me.chkBranch.BackColor = System.Drawing.Color.Transparent
        Me.chkBranch.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBranch.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkBranch.Location = New System.Drawing.Point(343, 14)
        Me.chkBranch.Name = "chkBranch"
        Me.chkBranch.Size = New System.Drawing.Size(56, 21)
        Me.chkBranch.TabIndex = 16
        Me.chkBranch.Text = "ဆိုင်ခွဲ"
        Me.chkBranch.UseVisualStyleBackColor = False
        '
        'cboBranch
        '
        Me.cboBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboBranch.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Location = New System.Drawing.Point(471, 11)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(201, 27)
        Me.cboBranch.TabIndex = 17
        '
        'grpByDate
        '
        Me.grpByDate.BackColor = System.Drawing.Color.Transparent
        Me.grpByDate.Controls.Add(Me.dtpToDate)
        Me.grpByDate.Controls.Add(Me.dtpFromDate)
        Me.grpByDate.Controls.Add(Me.Label1)
        Me.grpByDate.Controls.Add(Me.Label2)
        Me.grpByDate.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grpByDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpByDate.Location = New System.Drawing.Point(12, 1)
        Me.grpByDate.Name = "grpByDate"
        Me.grpByDate.Size = New System.Drawing.Size(310, 56)
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
        Me.dtpToDate.Size = New System.Drawing.Size(100, 24)
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.TabStop = False
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(64, 23)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 24)
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
        Me.btnPreview.Location = New System.Drawing.Point(723, 77)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 1
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
        Me.btnClose.Location = New System.Drawing.Point(842, 77)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 144)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(1006, 320)
        Me.rptViewer.TabIndex = 2
        Me.rptViewer.TabStop = False
        '
        'chkTRLooseDiamond
        '
        Me.chkTRLooseDiamond.AutoSize = True
        Me.chkTRLooseDiamond.BackColor = System.Drawing.Color.Transparent
        Me.chkTRLooseDiamond.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTRLooseDiamond.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkTRLooseDiamond.Location = New System.Drawing.Point(12, 110)
        Me.chkTRLooseDiamond.Name = "chkTRLooseDiamond"
        Me.chkTRLooseDiamond.Size = New System.Drawing.Size(222, 21)
        Me.chkTRLooseDiamond.TabIndex = 917
        Me.chkTRLooseDiamond.Text = "TransferReturn LooseDiamond"
        Me.chkTRLooseDiamond.UseVisualStyleBackColor = False
        '
        'chkGemsCategory
        '
        Me.chkGemsCategory.AutoSize = True
        Me.chkGemsCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGemsCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGemsCategory.Location = New System.Drawing.Point(342, 47)
        Me.chkGemsCategory.Name = "chkGemsCategory"
        Me.chkGemsCategory.Size = New System.Drawing.Size(107, 21)
        Me.chkGemsCategory.TabIndex = 918
        Me.chkGemsCategory.Text = "စိန်အမျိုးအစား"
        Me.chkGemsCategory.UseVisualStyleBackColor = True
        '
        'CboGemsCategory
        '
        Me.CboGemsCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboGemsCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboGemsCategory.FormattingEnabled = True
        Me.CboGemsCategory.Location = New System.Drawing.Point(471, 44)
        Me.CboGemsCategory.Name = "CboGemsCategory"
        Me.CboGemsCategory.Size = New System.Drawing.Size(200, 27)
        Me.CboGemsCategory.TabIndex = 919
        '
        'frm_rpt_TransferReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 464)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_rpt_TransferReturn"
        Me.Text = "Branch Transfer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkBranch As System.Windows.Forms.CheckBox
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radSummary As System.Windows.Forms.RadioButton
    Friend WithEvents radDetail As System.Windows.Forms.RadioButton
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents lblBarcodeNo As System.Windows.Forms.Label
    Friend WithEvents txtBarcodeNo As System.Windows.Forms.TextBox
    Friend WithEvents chkItemCategory As System.Windows.Forms.CheckBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cboItemName As System.Windows.Forms.ComboBox
    Friend WithEvents ChkItemName As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQ As System.Windows.Forms.ComboBox
    Friend WithEvents chkGoldQly As System.Windows.Forms.CheckBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents chkTRLooseDiamond As System.Windows.Forms.CheckBox
    Friend WithEvents chkGemsCategory As System.Windows.Forms.CheckBox
    Friend WithEvents CboGemsCategory As System.Windows.Forms.ComboBox
End Class
