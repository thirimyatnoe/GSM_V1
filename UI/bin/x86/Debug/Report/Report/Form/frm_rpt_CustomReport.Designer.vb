<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_CustomReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_CustomReport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpCustomer = New System.Windows.Forms.GroupBox()
        Me.chkStaff = New System.Windows.Forms.CheckBox()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.chkCustomerName = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboReportName = New System.Windows.Forms.ComboBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpGQ = New System.Windows.Forms.GroupBox()
        Me.chkItemName = New System.Windows.Forms.CheckBox()
        Me.cboItemName = New System.Windows.Forms.ComboBox()
        Me.cboGemsCategory = New System.Windows.Forms.ComboBox()
        Me.chkGems = New System.Windows.Forms.CheckBox()
        Me.chkItemCat = New System.Windows.Forms.CheckBox()
        Me.chkGemsCategory = New System.Windows.Forms.CheckBox()
        Me.chkGoldQ = New System.Windows.Forms.CheckBox()
        Me.cboGoldQ = New System.Windows.Forms.ComboBox()
        Me.cboItemCat = New System.Windows.Forms.ComboBox()
        Me.grpDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1.SuspendLayout()
        Me.grpCustomer.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpGQ.SuspendLayout()
        Me.grpDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.Panel1.Controls.Add(Me.grpCustomer)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.grpGQ)
        Me.Panel1.Controls.Add(Me.grpDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1232, 167)
        Me.Panel1.TabIndex = 0
        '
        'grpCustomer
        '
        Me.grpCustomer.BackColor = System.Drawing.Color.Transparent
        Me.grpCustomer.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.grpCustomer.Controls.Add(Me.chkStaff)
        Me.grpCustomer.Controls.Add(Me.cboStaff)
        Me.grpCustomer.Controls.Add(Me.txtCustomerCode)
        Me.grpCustomer.Controls.Add(Me.btnCustomer)
        Me.grpCustomer.Controls.Add(Me.txtCustomerName)
        Me.grpCustomer.Controls.Add(Me.chkCustomerName)
        Me.grpCustomer.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCustomer.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpCustomer.Location = New System.Drawing.Point(868, 3)
        Me.grpCustomer.Name = "grpCustomer"
        Me.grpCustomer.Size = New System.Drawing.Size(340, 127)
        Me.grpCustomer.TabIndex = 3
        Me.grpCustomer.TabStop = False
        '
        'chkStaff
        '
        Me.chkStaff.AutoSize = True
        Me.chkStaff.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStaff.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkStaff.Location = New System.Drawing.Point(39, 73)
        Me.chkStaff.Name = "chkStaff"
        Me.chkStaff.Size = New System.Drawing.Size(57, 21)
        Me.chkStaff.TabIndex = 3
        Me.chkStaff.Text = "Staff"
        Me.chkStaff.UseVisualStyleBackColor = True
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(132, 73)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(171, 27)
        Me.cboStaff.TabIndex = 4
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Zawgyi-One", 8.25!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(132, 12)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(71, 25)
        Me.txtCustomerCode.TabIndex = 1
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(205, 13)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(38, 21)
        Me.btnCustomer.TabIndex = 0
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(132, 41)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(202, 26)
        Me.txtCustomerName.TabIndex = 2
        '
        'chkCustomerName
        '
        Me.chkCustomerName.AutoSize = True
        Me.chkCustomerName.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.chkCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkCustomerName.Location = New System.Drawing.Point(39, 15)
        Me.chkCustomerName.Name = "chkCustomerName"
        Me.chkCustomerName.Size = New System.Drawing.Size(87, 21)
        Me.chkCustomerName.TabIndex = 0
        Me.chkCustomerName.Text = "Customer"
        Me.chkCustomerName.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.GroupBox1.Controls.Add(Me.cboReportName)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(302, 83)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report Name"
        '
        'cboReportName
        '
        Me.cboReportName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReportName.FormattingEnabled = True
        Me.cboReportName.Location = New System.Drawing.Point(5, 35)
        Me.cboReportName.Name = "cboReportName"
        Me.cboReportName.Size = New System.Drawing.Size(291, 22)
        Me.cboReportName.TabIndex = 0
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(58, 105)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 4
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(155, 105)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpGQ
        '
        Me.grpGQ.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.grpGQ.Controls.Add(Me.chkItemName)
        Me.grpGQ.Controls.Add(Me.cboItemName)
        Me.grpGQ.Controls.Add(Me.cboGemsCategory)
        Me.grpGQ.Controls.Add(Me.chkGems)
        Me.grpGQ.Controls.Add(Me.chkItemCat)
        Me.grpGQ.Controls.Add(Me.chkGemsCategory)
        Me.grpGQ.Controls.Add(Me.chkGoldQ)
        Me.grpGQ.Controls.Add(Me.cboGoldQ)
        Me.grpGQ.Controls.Add(Me.cboItemCat)
        Me.grpGQ.Location = New System.Drawing.Point(504, 12)
        Me.grpGQ.Name = "grpGQ"
        Me.grpGQ.Size = New System.Drawing.Size(358, 149)
        Me.grpGQ.TabIndex = 2
        Me.grpGQ.TabStop = False
        '
        'chkItemName
        '
        Me.chkItemName.AutoSize = True
        Me.chkItemName.BackColor = System.Drawing.Color.Transparent
        Me.chkItemName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemName.Location = New System.Drawing.Point(20, 83)
        Me.chkItemName.Name = "chkItemName"
        Me.chkItemName.Size = New System.Drawing.Size(95, 21)
        Me.chkItemName.TabIndex = 4
        Me.chkItemName.Text = "Item Name"
        Me.chkItemName.UseVisualStyleBackColor = False
        '
        'cboItemName
        '
        Me.cboItemName.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboItemName.FormattingEnabled = True
        Me.cboItemName.Location = New System.Drawing.Point(144, 80)
        Me.cboItemName.Name = "cboItemName"
        Me.cboItemName.Size = New System.Drawing.Size(203, 27)
        Me.cboItemName.TabIndex = 5
        '
        'cboGemsCategory
        '
        Me.cboGemsCategory.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboGemsCategory.FormattingEnabled = True
        Me.cboGemsCategory.Location = New System.Drawing.Point(144, 113)
        Me.cboGemsCategory.Name = "cboGemsCategory"
        Me.cboGemsCategory.Size = New System.Drawing.Size(203, 27)
        Me.cboGemsCategory.TabIndex = 7
        '
        'chkGems
        '
        Me.chkGems.AutoSize = True
        Me.chkGems.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.chkGems.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGems.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGems.Location = New System.Drawing.Point(20, 113)
        Me.chkGems.Name = "chkGems"
        Me.chkGems.Size = New System.Drawing.Size(124, 21)
        Me.chkGems.TabIndex = 6
        Me.chkGems.Text = "Gems Category"
        Me.chkGems.UseVisualStyleBackColor = True
        '
        'chkItemCat
        '
        Me.chkItemCat.AutoSize = True
        Me.chkItemCat.BackColor = System.Drawing.Color.Transparent
        Me.chkItemCat.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemCat.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkItemCat.Location = New System.Drawing.Point(20, 50)
        Me.chkItemCat.Name = "chkItemCat"
        Me.chkItemCat.Size = New System.Drawing.Size(117, 21)
        Me.chkItemCat.TabIndex = 2
        Me.chkItemCat.Text = "Item Category"
        Me.chkItemCat.UseVisualStyleBackColor = False
        '
        'chkGemsCategory
        '
        Me.chkGemsCategory.AutoSize = True
        Me.chkGemsCategory.BackColor = System.Drawing.Color.Transparent
        Me.chkGemsCategory.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGemsCategory.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGemsCategory.Location = New System.Drawing.Point(20, 51)
        Me.chkGemsCategory.Name = "chkGemsCategory"
        Me.chkGemsCategory.Size = New System.Drawing.Size(118, 21)
        Me.chkGemsCategory.TabIndex = 8
        Me.chkGemsCategory.Text = "Gem Category"
        Me.chkGemsCategory.UseVisualStyleBackColor = False
        '
        'chkGoldQ
        '
        Me.chkGoldQ.AutoSize = True
        Me.chkGoldQ.BackColor = System.Drawing.Color.Transparent
        Me.chkGoldQ.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldQ.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkGoldQ.Location = New System.Drawing.Point(20, 17)
        Me.chkGoldQ.Name = "chkGoldQ"
        Me.chkGoldQ.Size = New System.Drawing.Size(112, 21)
        Me.chkGoldQ.TabIndex = 0
        Me.chkGoldQ.Text = "Gold Quality"
        Me.chkGoldQ.UseVisualStyleBackColor = False
        '
        'cboGoldQ
        '
        Me.cboGoldQ.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoldQ.FormattingEnabled = True
        Me.cboGoldQ.Location = New System.Drawing.Point(144, 14)
        Me.cboGoldQ.Name = "cboGoldQ"
        Me.cboGoldQ.Size = New System.Drawing.Size(203, 27)
        Me.cboGoldQ.TabIndex = 1
        '
        'cboItemCat
        '
        Me.cboItemCat.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.cboItemCat.FormattingEnabled = True
        Me.cboItemCat.Location = New System.Drawing.Point(144, 47)
        Me.cboItemCat.Name = "cboItemCat"
        Me.cboItemCat.Size = New System.Drawing.Size(203, 27)
        Me.cboItemCat.TabIndex = 3
        '
        'grpDate
        '
        Me.grpDate.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.grpDate.Controls.Add(Me.dtpToDate)
        Me.grpDate.Controls.Add(Me.Label1)
        Me.grpDate.Controls.Add(Me.dtpFromDate)
        Me.grpDate.Controls.Add(Me.Label2)
        Me.grpDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpDate.Location = New System.Drawing.Point(331, 12)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(167, 96)
        Me.grpDate.TabIndex = 1
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "By Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(53, 57)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(104, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(53, 25)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(104, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "To"
        '
        'RptViewer
        '
        Me.RptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RptViewer.Location = New System.Drawing.Point(0, 167)
        Me.RptViewer.Name = "RptViewer"
        Me.RptViewer.Size = New System.Drawing.Size(1232, 364)
        Me.RptViewer.TabIndex = 1
        Me.RptViewer.TabStop = False
        '
        'frm_rpt_CustomReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1232, 531)
        Me.Controls.Add(Me.RptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_rpt_CustomReport"
        Me.Text = "Custom Report "
        Me.Panel1.ResumeLayout(False)
        Me.grpCustomer.ResumeLayout(False)
        Me.grpCustomer.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.grpGQ.ResumeLayout(False)
        Me.grpGQ.PerformLayout()
        Me.grpDate.ResumeLayout(False)
        Me.grpDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grpGQ As System.Windows.Forms.GroupBox
    Friend WithEvents chkItemCat As System.Windows.Forms.CheckBox
    Friend WithEvents chkGemsCategory As System.Windows.Forms.CheckBox
    Friend WithEvents chkGoldQ As System.Windows.Forms.CheckBox
    Friend WithEvents cboGoldQ As System.Windows.Forms.ComboBox
    Friend WithEvents cboItemCat As System.Windows.Forms.ComboBox
    Friend WithEvents grpDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboReportName As System.Windows.Forms.ComboBox
    Friend WithEvents RptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents cboGemsCategory As System.Windows.Forms.ComboBox
    Friend WithEvents chkGems As System.Windows.Forms.CheckBox
    Friend WithEvents grpCustomer As System.Windows.Forms.GroupBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents chkCustomerName As System.Windows.Forms.CheckBox
    Friend WithEvents chkStaff As System.Windows.Forms.CheckBox
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents chkItemName As System.Windows.Forms.CheckBox
    Friend WithEvents cboItemName As System.Windows.Forms.ComboBox
End Class
