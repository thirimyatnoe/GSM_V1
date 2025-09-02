<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_MasterSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_MasterSetup))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.chkLocation = New System.Windows.Forms.CheckBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtGemsCategory = New System.Windows.Forms.RadioButton()
        Me.rbtItemName = New System.Windows.Forms.RadioButton()
        Me.rbtGoldQuality = New System.Windows.Forms.RadioButton()
        Me.rbtSalesStaffs = New System.Windows.Forms.RadioButton()
        Me.rbtItemCategory = New System.Windows.Forms.RadioButton()
        Me.rbtCustomer = New System.Windows.Forms.RadioButton()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightYellow
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(901, 118)
        Me.Panel1.TabIndex = 0
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(627, 48)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 3
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.GroupBox2.Controls.Add(Me.cboLocation)
        Me.GroupBox2.Controls.Add(Me.chkLocation)
        Me.GroupBox2.Location = New System.Drawing.Point(999, 145)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(285, 47)
        Me.GroupBox2.TabIndex = 216
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Visible = False
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(87, 15)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(185, 26)
        Me.cboLocation.TabIndex = 215
        '
        'chkLocation
        '
        Me.chkLocation.AutoSize = True
        Me.chkLocation.BackColor = System.Drawing.Color.Transparent
        Me.chkLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkLocation.Location = New System.Drawing.Point(6, 19)
        Me.chkLocation.Name = "chkLocation"
        Me.chkLocation.Size = New System.Drawing.Size(75, 17)
        Me.chkLocation.TabIndex = 214
        Me.chkLocation.Text = "Location"
        Me.chkLocation.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(437, 50)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtGemsCategory)
        Me.GroupBox1.Controls.Add(Me.rbtItemName)
        Me.GroupBox1.Controls.Add(Me.rbtGoldQuality)
        Me.GroupBox1.Controls.Add(Me.rbtSalesStaffs)
        Me.GroupBox1.Controls.Add(Me.rbtItemCategory)
        Me.GroupBox1.Controls.Add(Me.rbtCustomer)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(416, 103)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'rbtGemsCategory
        '
        Me.rbtGemsCategory.AutoSize = True
        Me.rbtGemsCategory.Location = New System.Drawing.Point(148, 67)
        Me.rbtGemsCategory.Name = "rbtGemsCategory"
        Me.rbtGemsCategory.Size = New System.Drawing.Size(133, 21)
        Me.rbtGemsCategory.TabIndex = 5
        Me.rbtGemsCategory.Text = "ကျောက်အမျိုးအစား"
        Me.rbtGemsCategory.UseVisualStyleBackColor = True
        '
        'rbtItemName
        '
        Me.rbtItemName.AutoSize = True
        Me.rbtItemName.Location = New System.Drawing.Point(299, 26)
        Me.rbtItemName.Name = "rbtItemName"
        Me.rbtItemName.Size = New System.Drawing.Size(91, 21)
        Me.rbtItemName.TabIndex = 2
        Me.rbtItemName.Text = "အမျိုးအမည်"
        Me.rbtItemName.UseVisualStyleBackColor = True
        '
        'rbtGoldQuality
        '
        Me.rbtGoldQuality.AutoSize = True
        Me.rbtGoldQuality.Location = New System.Drawing.Point(26, 67)
        Me.rbtGoldQuality.Name = "rbtGoldQuality"
        Me.rbtGoldQuality.Size = New System.Drawing.Size(62, 21)
        Me.rbtGoldQuality.TabIndex = 1
        Me.rbtGoldQuality.Text = "ရွှေရည်"
        Me.rbtGoldQuality.UseVisualStyleBackColor = True
        '
        'rbtSalesStaffs
        '
        Me.rbtSalesStaffs.AutoSize = True
        Me.rbtSalesStaffs.Location = New System.Drawing.Point(299, 67)
        Me.rbtSalesStaffs.Name = "rbtSalesStaffs"
        Me.rbtSalesStaffs.Size = New System.Drawing.Size(115, 21)
        Me.rbtSalesStaffs.TabIndex = 3
        Me.rbtSalesStaffs.Text = "အရောင်းဝန်ထမ်း"
        Me.rbtSalesStaffs.UseVisualStyleBackColor = True
        '
        'rbtItemCategory
        '
        Me.rbtItemCategory.AutoSize = True
        Me.rbtItemCategory.Location = New System.Drawing.Point(148, 26)
        Me.rbtItemCategory.Name = "rbtItemCategory"
        Me.rbtItemCategory.Size = New System.Drawing.Size(90, 21)
        Me.rbtItemCategory.TabIndex = 4
        Me.rbtItemCategory.Text = "အမျိုးအစား"
        Me.rbtItemCategory.UseVisualStyleBackColor = True
        '
        'rbtCustomer
        '
        Me.rbtCustomer.AutoSize = True
        Me.rbtCustomer.Checked = True
        Me.rbtCustomer.Location = New System.Drawing.Point(26, 26)
        Me.rbtCustomer.Name = "rbtCustomer"
        Me.rbtCustomer.Size = New System.Drawing.Size(61, 21)
        Me.rbtCustomer.TabIndex = 0
        Me.rbtCustomer.TabStop = True
        Me.rbtCustomer.Text = "ဝယ်သူ"
        Me.rbtCustomer.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(534, 50)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 118)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(901, 386)
        Me.ReportViewer1.TabIndex = 1
        Me.ReportViewer1.TabStop = False
        '
        'frm_rpt_MasterSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 504)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_rpt_MasterSetup"
        Me.Text = "Master Setup Report"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents chkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtItemName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtGoldQuality As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSalesStaffs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtItemCategory As System.Windows.Forms.RadioButton
    Friend WithEvents rbtCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents rbtGemsCategory As System.Windows.Forms.RadioButton
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
