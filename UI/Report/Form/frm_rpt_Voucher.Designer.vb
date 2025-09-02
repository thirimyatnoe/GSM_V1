<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_rpt_Voucher
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_rpt_Voucher))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.grpDate = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.GroupVoucher = New System.Windows.Forms.GroupBox()
        Me.lblVoucherNo = New System.Windows.Forms.Label()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.chkByVoucher = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radPurchaseGem = New System.Windows.Forms.RadioButton()
        Me.radPurchase = New System.Windows.Forms.RadioButton()
        Me.radSaleGems = New System.Windows.Forms.RadioButton()
        Me.radOrderReturn = New System.Windows.Forms.RadioButton()
        Me.radSaleVolume = New System.Windows.Forms.RadioButton()
        Me.radOrderInvoice = New System.Windows.Forms.RadioButton()
        Me.radSaleInvoice = New System.Windows.Forms.RadioButton()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Panel1.SuspendLayout()
        Me.grpDate.SuspendLayout()
        Me.GroupVoucher.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel1.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.grpDate)
        Me.Panel1.Controls.Add(Me.GroupVoucher)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(813, 159)
        Me.Panel1.TabIndex = 0
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(774, 55)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 1468
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'grpDate
        '
        Me.grpDate.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.grpDate.Controls.Add(Me.Label1)
        Me.grpDate.Controls.Add(Me.Label2)
        Me.grpDate.Controls.Add(Me.dtpFromDate)
        Me.grpDate.Controls.Add(Me.dtpToDate)
        Me.grpDate.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.grpDate.Location = New System.Drawing.Point(12, 99)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(332, 53)
        Me.grpDate.TabIndex = 2
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(37, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(184, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "To"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(81, 16)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpFromDate.TabIndex = 0
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(212, 16)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(100, 26)
        Me.dtpToDate.TabIndex = 1
        '
        'GroupVoucher
        '
        Me.GroupVoucher.BackColor = System.Drawing.Color.Transparent
        Me.GroupVoucher.Controls.Add(Me.lblVoucherNo)
        Me.GroupVoucher.Controls.Add(Me.txtVoucherNo)
        Me.GroupVoucher.Controls.Add(Me.chkByVoucher)
        Me.GroupVoucher.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupVoucher.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupVoucher.Location = New System.Drawing.Point(350, 97)
        Me.GroupVoucher.Name = "GroupVoucher"
        Me.GroupVoucher.Size = New System.Drawing.Size(393, 52)
        Me.GroupVoucher.TabIndex = 3
        Me.GroupVoucher.TabStop = False
        '
        'lblVoucherNo
        '
        Me.lblVoucherNo.AutoSize = True
        Me.lblVoucherNo.BackColor = System.Drawing.Color.Transparent
        Me.lblVoucherNo.Enabled = False
        Me.lblVoucherNo.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.lblVoucherNo.ForeColor = System.Drawing.Color.Black
        Me.lblVoucherNo.Location = New System.Drawing.Point(116, 22)
        Me.lblVoucherNo.Name = "lblVoucherNo"
        Me.lblVoucherNo.Size = New System.Drawing.Size(90, 17)
        Me.lblVoucherNo.TabIndex = 8
        Me.lblVoucherNo.Text = "Voucher No:"
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.BackColor = System.Drawing.Color.White
        Me.txtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVoucherNo.Enabled = False
        Me.txtVoucherNo.Font = New System.Drawing.Font("Zawgyi-One", 9.0!)
        Me.txtVoucherNo.Location = New System.Drawing.Point(212, 17)
        Me.txtVoucherNo.Multiline = True
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.Size = New System.Drawing.Size(167, 27)
        Me.txtVoucherNo.TabIndex = 1
        Me.txtVoucherNo.TabStop = False
        '
        'chkByVoucher
        '
        Me.chkByVoucher.AutoSize = True
        Me.chkByVoucher.BackColor = System.Drawing.Color.White
        Me.chkByVoucher.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.chkByVoucher.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.chkByVoucher.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkByVoucher.Location = New System.Drawing.Point(11, 21)
        Me.chkByVoucher.Name = "chkByVoucher"
        Me.chkByVoucher.Size = New System.Drawing.Size(99, 21)
        Me.chkByVoucher.TabIndex = 0
        Me.chkByVoucher.Text = "ByVoucher"
        Me.chkByVoucher.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.radPurchaseGem)
        Me.GroupBox1.Controls.Add(Me.radPurchase)
        Me.GroupBox1.Controls.Add(Me.radSaleGems)
        Me.GroupBox1.Controls.Add(Me.radOrderReturn)
        Me.GroupBox1.Controls.Add(Me.radSaleVolume)
        Me.GroupBox1.Controls.Add(Me.radOrderInvoice)
        Me.GroupBox1.Controls.Add(Me.radSaleInvoice)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(568, 88)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Voucher"
        '
        'radPurchaseGem
        '
        Me.radPurchaseGem.AutoSize = True
        Me.radPurchaseGem.Checked = True
        Me.radPurchaseGem.Location = New System.Drawing.Point(19, 25)
        Me.radPurchaseGem.Name = "radPurchaseGem"
        Me.radPurchaseGem.Size = New System.Drawing.Size(117, 21)
        Me.radPurchaseGem.TabIndex = 0
        Me.radPurchaseGem.TabStop = True
        Me.radPurchaseGem.Text = "PurchaseGems"
        Me.radPurchaseGem.UseVisualStyleBackColor = True
        '
        'radPurchase
        '
        Me.radPurchase.AutoSize = True
        Me.radPurchase.Location = New System.Drawing.Point(19, 55)
        Me.radPurchase.Name = "radPurchase"
        Me.radPurchase.Size = New System.Drawing.Size(82, 21)
        Me.radPurchase.TabIndex = 4
        Me.radPurchase.Text = "Purchase"
        Me.radPurchase.UseVisualStyleBackColor = True
        '
        'radSaleGems
        '
        Me.radSaleGems.AutoSize = True
        Me.radSaleGems.Location = New System.Drawing.Point(474, 25)
        Me.radSaleGems.Name = "radSaleGems"
        Me.radSaleGems.Size = New System.Drawing.Size(88, 21)
        Me.radSaleGems.TabIndex = 3
        Me.radSaleGems.Text = "SaleGems"
        Me.radSaleGems.UseVisualStyleBackColor = True
        '
        'radOrderReturn
        '
        Me.radOrderReturn.AutoSize = True
        Me.radOrderReturn.Location = New System.Drawing.Point(147, 55)
        Me.radOrderReturn.Name = "radOrderReturn"
        Me.radOrderReturn.Size = New System.Drawing.Size(157, 21)
        Me.radOrderReturn.TabIndex = 5
        Me.radOrderReturn.Text = "OrderInvoice Return"
        Me.radOrderReturn.UseVisualStyleBackColor = True
        '
        'radSaleVolume
        '
        Me.radSaleVolume.AutoSize = True
        Me.radSaleVolume.Location = New System.Drawing.Point(310, 55)
        Me.radSaleVolume.Name = "radSaleVolume"
        Me.radSaleVolume.Size = New System.Drawing.Size(151, 21)
        Me.radSaleVolume.TabIndex = 6
        Me.radSaleVolume.Text = "SaleVolumeInvoice"
        Me.radSaleVolume.UseVisualStyleBackColor = True
        '
        'radOrderInvoice
        '
        Me.radOrderInvoice.AutoSize = True
        Me.radOrderInvoice.Location = New System.Drawing.Point(147, 25)
        Me.radOrderInvoice.Name = "radOrderInvoice"
        Me.radOrderInvoice.Size = New System.Drawing.Size(110, 21)
        Me.radOrderInvoice.TabIndex = 1
        Me.radOrderInvoice.Text = "OrderInvoice"
        Me.radOrderInvoice.UseVisualStyleBackColor = True
        '
        'radSaleInvoice
        '
        Me.radSaleInvoice.AutoSize = True
        Me.radSaleInvoice.Location = New System.Drawing.Point(310, 25)
        Me.radSaleInvoice.Name = "radSaleInvoice"
        Me.radSaleInvoice.Size = New System.Drawing.Size(101, 21)
        Me.radSaleInvoice.TabIndex = 2
        Me.radSaleInvoice.Text = "SaleInvoice"
        Me.radSaleInvoice.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(586, 57)
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
        Me.btnClose.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(681, 57)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 159)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(813, 318)
        Me.rptViewer.TabIndex = 5
        Me.rptViewer.TabStop = False
        '
        'frm_rpt_Voucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 477)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_rpt_Voucher"
        Me.Text = "Vouchers"
        Me.Panel1.ResumeLayout(False)
        Me.grpDate.ResumeLayout(False)
        Me.grpDate.PerformLayout()
        Me.GroupVoucher.ResumeLayout(False)
        Me.GroupVoucher.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radSaleInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents radOrderReturn As System.Windows.Forms.RadioButton
    Friend WithEvents radSaleVolume As System.Windows.Forms.RadioButton
    Friend WithEvents radOrderInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents radPurchase As System.Windows.Forms.RadioButton
    Friend WithEvents radSaleGems As System.Windows.Forms.RadioButton
    Friend WithEvents chkByVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents lblVoucherNo As System.Windows.Forms.Label
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupVoucher As System.Windows.Forms.GroupBox
    Friend WithEvents grpDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents radPurchaseGem As System.Windows.Forms.RadioButton
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
End Class
