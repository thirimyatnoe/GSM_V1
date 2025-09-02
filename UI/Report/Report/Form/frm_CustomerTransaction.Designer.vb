<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CustomerTransaction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CustomerTransaction))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkCustomerReceipt = New System.Windows.Forms.CheckBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.chkCustomerName = New System.Windows.Forms.CheckBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.grpByDate = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.grpByDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.chkCustomerReceipt)
        Me.Panel1.Controls.Add(Me.Label46)
        Me.Panel1.Controls.Add(Me.chkCustomerName)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.btnCustomer)
        Me.Panel1.Controls.Add(Me.txtCustomer)
        Me.Panel1.Controls.Add(Me.txtAddress)
        Me.Panel1.Controls.Add(Me.grpByDate)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1108, 96)
        Me.Panel1.TabIndex = 0
        '
        'chkCustomerReceipt
        '
        Me.chkCustomerReceipt.AutoSize = True
        Me.chkCustomerReceipt.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerReceipt.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkCustomerReceipt.Location = New System.Drawing.Point(691, 15)
        Me.chkCustomerReceipt.Name = "chkCustomerReceipt"
        Me.chkCustomerReceipt.Size = New System.Drawing.Size(134, 21)
        Me.chkCustomerReceipt.TabIndex = 1502
        Me.chkCustomerReceipt.Text = "Customer Report"
        Me.chkCustomerReceipt.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label46.Location = New System.Drawing.Point(343, 45)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(48, 20)
        Me.Label46.TabIndex = 1500
        Me.Label46.Text = "လိပ်စာ"
        '
        'chkCustomerName
        '
        Me.chkCustomerName.AutoSize = True
        Me.chkCustomerName.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkCustomerName.Location = New System.Drawing.Point(330, 16)
        Me.chkCustomerName.Name = "chkCustomerName"
        Me.chkCustomerName.Size = New System.Drawing.Size(62, 21)
        Me.chkCustomerName.TabIndex = 1501
        Me.chkCustomerName.Text = "ဝယ်သူ"
        Me.chkCustomerName.UseVisualStyleBackColor = True
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Zawgyi-One", 8.25!)
        Me.txtCustomerCode.Location = New System.Drawing.Point(392, 13)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(59, 25)
        Me.txtCustomerCode.TabIndex = 1496
        '
        'btnCustomer
        '
        Me.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(624, 13)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(41, 25)
        Me.btnCustomer.TabIndex = 1497
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.White
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCustomer.Location = New System.Drawing.Point(451, 13)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(172, 27)
        Me.txtCustomer.TabIndex = 1499
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtAddress.Location = New System.Drawing.Point(392, 44)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(274, 44)
        Me.txtAddress.TabIndex = 1498
        Me.txtAddress.TabStop = False
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
        Me.btnPreview.Location = New System.Drawing.Point(693, 57)
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
        Me.btnClose.Location = New System.Drawing.Point(792, 57)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 96)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(1108, 328)
        Me.rptViewer.TabIndex = 1
        Me.rptViewer.TabStop = False
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(829, 14)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 1503
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(922, 11)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(171, 27)
        Me.CboLocation.TabIndex = 1504
        '
        'frm_CustomerTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 424)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_CustomerTransaction"
        Me.Text = "All Transaction"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpByDate.ResumeLayout(False)
        Me.grpByDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grpByDate As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents chkCustomerName As System.Windows.Forms.CheckBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents chkCustomerReceipt As System.Windows.Forms.CheckBox
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
