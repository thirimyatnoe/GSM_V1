<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_VoucherTemplate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_VoucherTemplate))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optSaleInvoice = New System.Windows.Forms.RadioButton()
        Me.optOrderReturn = New System.Windows.Forms.RadioButton()
        Me.optSaleGem = New System.Windows.Forms.RadioButton()
        Me.optOrderReceive = New System.Windows.Forms.RadioButton()
        Me.optPurchaseGem = New System.Windows.Forms.RadioButton()
        Me.optSaleVolume = New System.Windows.Forms.RadioButton()
        Me.optPurchaseInvoice = New System.Windows.Forms.RadioButton()
        Me.chkDirectPrint = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboChangeTemplate = New System.Windows.Forms.ComboBox()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.RptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.chkDirectPrint)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cboChangeTemplate)
        Me.Panel1.Controls.Add(Me.btnCreate)
        Me.Panel1.Controls.Add(Me.btnEdit)
        Me.Panel1.Controls.Add(Me.btnExport)
        Me.Panel1.Controls.Add(Me.btnImport)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1084, 154)
        Me.Panel1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.optSaleInvoice)
        Me.GroupBox1.Controls.Add(Me.optOrderReturn)
        Me.GroupBox1.Controls.Add(Me.optSaleGem)
        Me.GroupBox1.Controls.Add(Me.optOrderReceive)
        Me.GroupBox1.Controls.Add(Me.optPurchaseGem)
        Me.GroupBox1.Controls.Add(Me.optSaleVolume)
        Me.GroupBox1.Controls.Add(Me.optPurchaseInvoice)
        Me.GroupBox1.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(636, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 142)
        Me.GroupBox1.TabIndex = 1489
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Type"
        '
        'optSaleInvoice
        '
        Me.optSaleInvoice.AutoSize = True
        Me.optSaleInvoice.BackColor = System.Drawing.Color.Transparent
        Me.optSaleInvoice.Checked = True
        Me.optSaleInvoice.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optSaleInvoice.ForeColor = System.Drawing.Color.Black
        Me.optSaleInvoice.Location = New System.Drawing.Point(35, 25)
        Me.optSaleInvoice.Name = "optSaleInvoice"
        Me.optSaleInvoice.Size = New System.Drawing.Size(101, 21)
        Me.optSaleInvoice.TabIndex = 1480
        Me.optSaleInvoice.TabStop = True
        Me.optSaleInvoice.Text = "SaleInvoice"
        Me.optSaleInvoice.UseVisualStyleBackColor = False
        '
        'optOrderReturn
        '
        Me.optOrderReturn.AutoSize = True
        Me.optOrderReturn.BackColor = System.Drawing.Color.Transparent
        Me.optOrderReturn.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optOrderReturn.ForeColor = System.Drawing.Color.Black
        Me.optOrderReturn.Location = New System.Drawing.Point(169, 51)
        Me.optOrderReturn.Name = "optOrderReturn"
        Me.optOrderReturn.Size = New System.Drawing.Size(105, 21)
        Me.optOrderReturn.TabIndex = 1481
        Me.optOrderReturn.Text = "OrderReturn"
        Me.optOrderReturn.UseVisualStyleBackColor = False
        '
        'optSaleGem
        '
        Me.optSaleGem.AutoSize = True
        Me.optSaleGem.BackColor = System.Drawing.Color.Transparent
        Me.optSaleGem.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optSaleGem.ForeColor = System.Drawing.Color.Black
        Me.optSaleGem.Location = New System.Drawing.Point(35, 109)
        Me.optSaleGem.Name = "optSaleGem"
        Me.optSaleGem.Size = New System.Drawing.Size(82, 21)
        Me.optSaleGem.TabIndex = 1486
        Me.optSaleGem.Text = "SaleGem"
        Me.optSaleGem.UseVisualStyleBackColor = False
        '
        'optOrderReceive
        '
        Me.optOrderReceive.AutoSize = True
        Me.optOrderReceive.BackColor = System.Drawing.Color.Transparent
        Me.optOrderReceive.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optOrderReceive.ForeColor = System.Drawing.Color.Black
        Me.optOrderReceive.Location = New System.Drawing.Point(35, 51)
        Me.optOrderReceive.Name = "optOrderReceive"
        Me.optOrderReceive.Size = New System.Drawing.Size(113, 21)
        Me.optOrderReceive.TabIndex = 1482
        Me.optOrderReceive.Text = "OrderReceive"
        Me.optOrderReceive.UseVisualStyleBackColor = False
        '
        'optPurchaseGem
        '
        Me.optPurchaseGem.AutoSize = True
        Me.optPurchaseGem.BackColor = System.Drawing.Color.Transparent
        Me.optPurchaseGem.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optPurchaseGem.ForeColor = System.Drawing.Color.Black
        Me.optPurchaseGem.Location = New System.Drawing.Point(169, 79)
        Me.optPurchaseGem.Name = "optPurchaseGem"
        Me.optPurchaseGem.Size = New System.Drawing.Size(111, 21)
        Me.optPurchaseGem.TabIndex = 1485
        Me.optPurchaseGem.Text = "PurchaseGem"
        Me.optPurchaseGem.UseVisualStyleBackColor = False
        '
        'optSaleVolume
        '
        Me.optSaleVolume.AutoSize = True
        Me.optSaleVolume.BackColor = System.Drawing.Color.Transparent
        Me.optSaleVolume.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optSaleVolume.ForeColor = System.Drawing.Color.Black
        Me.optSaleVolume.Location = New System.Drawing.Point(169, 25)
        Me.optSaleVolume.Name = "optSaleVolume"
        Me.optSaleVolume.Size = New System.Drawing.Size(161, 21)
        Me.optSaleVolume.TabIndex = 1483
        Me.optSaleVolume.Text = "SaleInvoice(Volume)"
        Me.optSaleVolume.UseVisualStyleBackColor = False
        '
        'optPurchaseInvoice
        '
        Me.optPurchaseInvoice.AutoSize = True
        Me.optPurchaseInvoice.BackColor = System.Drawing.Color.Transparent
        Me.optPurchaseInvoice.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optPurchaseInvoice.ForeColor = System.Drawing.Color.Black
        Me.optPurchaseInvoice.Location = New System.Drawing.Point(35, 79)
        Me.optPurchaseInvoice.Name = "optPurchaseInvoice"
        Me.optPurchaseInvoice.Size = New System.Drawing.Size(130, 21)
        Me.optPurchaseInvoice.TabIndex = 1484
        Me.optPurchaseInvoice.Text = "PurchaseInvoice"
        Me.optPurchaseInvoice.UseVisualStyleBackColor = False
        '
        'chkDirectPrint
        '
        Me.chkDirectPrint.AutoSize = True
        Me.chkDirectPrint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkDirectPrint.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.chkDirectPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkDirectPrint.Location = New System.Drawing.Point(994, 112)
        Me.chkDirectPrint.Name = "chkDirectPrint"
        Me.chkDirectPrint.Size = New System.Drawing.Size(87, 20)
        Me.chkDirectPrint.TabIndex = 1478
        Me.chkDirectPrint.Tag = "AutoPrint"
        Me.chkDirectPrint.Text = "Auto Print"
        Me.chkDirectPrint.UseVisualStyleBackColor = False
        Me.chkDirectPrint.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(9, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 17)
        Me.Label1.TabIndex = 1477
        Me.Label1.Text = "Change Template"
        '
        'cboChangeTemplate
        '
        Me.cboChangeTemplate.Font = New System.Drawing.Font("Myanmar3", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboChangeTemplate.FormattingEnabled = True
        Me.cboChangeTemplate.Location = New System.Drawing.Point(134, 77)
        Me.cboChangeTemplate.Name = "cboChangeTemplate"
        Me.cboChangeTemplate.Size = New System.Drawing.Size(218, 27)
        Me.cboChangeTemplate.TabIndex = 1476
        '
        'btnCreate
        '
        Me.btnCreate.BackgroundImage = CType(resources.GetObject("btnCreate.BackgroundImage"), System.Drawing.Image)
        Me.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreate.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnCreate.ForeColor = System.Drawing.Color.DarkRed
        Me.btnCreate.Location = New System.Drawing.Point(12, 29)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(148, 31)
        Me.btnCreate.TabIndex = 1475
        Me.btnCreate.Text = "Create New Template"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.BackgroundImage = CType(resources.GetObject("btnEdit.BackgroundImage"), System.Drawing.Image)
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnEdit.ForeColor = System.Drawing.Color.DarkRed
        Me.btnEdit.Location = New System.Drawing.Point(166, 28)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(148, 33)
        Me.btnEdit.TabIndex = 1474
        Me.btnEdit.Text = "Edit Template"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.BackgroundImage = CType(resources.GetObject("btnExport.BackgroundImage"), System.Drawing.Image)
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnExport.ForeColor = System.Drawing.Color.DarkRed
        Me.btnExport.Location = New System.Drawing.Point(320, 29)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(148, 31)
        Me.btnExport.TabIndex = 1473
        Me.btnExport.Text = "Export Template"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.BackgroundImage = CType(resources.GetObject("btnImport.BackgroundImage"), System.Drawing.Image)
        Me.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImport.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnImport.ForeColor = System.Drawing.Color.DarkRed
        Me.btnImport.Location = New System.Drawing.Point(474, 29)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(148, 31)
        Me.btnImport.TabIndex = 1472
        Me.btnImport.Text = "Import Template"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(1089, 0)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 1471
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.DarkRed
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(985, 28)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(87, 31)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.Visible = False
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(985, 65)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'RptViewer
        '
        Me.RptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RptViewer.Location = New System.Drawing.Point(0, 154)
        Me.RptViewer.Name = "RptViewer"
        Me.RptViewer.Size = New System.Drawing.Size(1084, 256)
        Me.RptViewer.TabIndex = 2
        Me.RptViewer.TabStop = False
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        '
        'frm_VoucherTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 410)
        Me.Controls.Add(Me.RptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_VoucherTemplate"
        Me.Text = "Vourcher Template"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents cboChangeTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents chkDirectPrint As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optSaleInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents optOrderReturn As System.Windows.Forms.RadioButton
    Friend WithEvents optSaleGem As System.Windows.Forms.RadioButton
    Friend WithEvents optOrderReceive As System.Windows.Forms.RadioButton
    Friend WithEvents optPurchaseGem As System.Windows.Forms.RadioButton
    Friend WithEvents optSaleVolume As System.Windows.Forms.RadioButton
    Friend WithEvents optPurchaseInvoice As System.Windows.Forms.RadioButton
End Class
