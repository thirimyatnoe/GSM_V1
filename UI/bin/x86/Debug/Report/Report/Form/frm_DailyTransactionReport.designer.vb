<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DailyTransactionReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_DailyTransactionReport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optBank = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.chkIsBank = New System.Windows.Forms.CheckBox()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.grpDate = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.rptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ChkLocation = New System.Windows.Forms.CheckBox()
        Me.CboLocation = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.grpDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChkLocation)
        Me.Panel1.Controls.Add(Me.CboLocation)
        Me.Panel1.Controls.Add(Me.optBank)
        Me.Panel1.Controls.Add(Me.optCash)
        Me.Panel1.Controls.Add(Me.optAll)
        Me.Panel1.Controls.Add(Me.chkIsBank)
        Me.Panel1.Controls.Add(Me.btnHelpbook)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.grpDate)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(903, 98)
        Me.Panel1.TabIndex = 1
        '
        'optBank
        '
        Me.optBank.AutoSize = True
        Me.optBank.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optBank.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optBank.Location = New System.Drawing.Point(423, 21)
        Me.optBank.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optBank.Name = "optBank"
        Me.optBank.Size = New System.Drawing.Size(59, 21)
        Me.optBank.TabIndex = 230
        Me.optBank.Text = "Bank"
        Me.optBank.UseVisualStyleBackColor = True
        '
        'optCash
        '
        Me.optCash.AutoSize = True
        Me.optCash.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optCash.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optCash.Location = New System.Drawing.Point(335, 22)
        Me.optCash.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(57, 21)
        Me.optCash.TabIndex = 229
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = True
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Checked = True
        Me.optAll.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.optAll.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.optAll.Location = New System.Drawing.Point(262, 22)
        Me.optAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(47, 21)
        Me.optAll.TabIndex = 228
        Me.optAll.TabStop = True
        Me.optAll.Text = "All"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'chkIsBank
        '
        Me.chkIsBank.AutoSize = True
        Me.chkIsBank.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsBank.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkIsBank.Location = New System.Drawing.Point(772, 23)
        Me.chkIsBank.Name = "chkIsBank"
        Me.chkIsBank.Size = New System.Drawing.Size(71, 21)
        Me.chkIsBank.TabIndex = 222
        Me.chkIsBank.Text = "IsBank"
        Me.chkIsBank.UseVisualStyleBackColor = True
        Me.chkIsBank.Visible = False
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(849, 3)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 35)
        Me.btnHelpbook.TabIndex = 12
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(571, 53)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 11
        Me.btnPreview.Text = "View"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'grpDate
        '
        Me.grpDate.Controls.Add(Me.Label1)
        Me.grpDate.Controls.Add(Me.dtpFromDate)
        Me.grpDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDate.Location = New System.Drawing.Point(21, 3)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(222, 54)
        Me.grpDate.TabIndex = 0
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.Label1.Location = New System.Drawing.Point(39, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "နေ့စွဲ"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(73, 19)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(114, 20)
        Me.dtpFromDate.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkRed
        Me.btnClose.Location = New System.Drawing.Point(675, 53)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 31)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rptViewer
        '
        Me.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rptViewer.Location = New System.Drawing.Point(0, 98)
        Me.rptViewer.Name = "rptViewer"
        Me.rptViewer.Size = New System.Drawing.Size(903, 540)
        Me.rptViewer.TabIndex = 2
        '
        'ChkLocation
        '
        Me.ChkLocation.AutoSize = True
        Me.ChkLocation.Font = New System.Drawing.Font("Myanmar3", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLocation.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ChkLocation.Location = New System.Drawing.Point(490, 21)
        Me.ChkLocation.Name = "ChkLocation"
        Me.ChkLocation.Size = New System.Drawing.Size(84, 21)
        Me.ChkLocation.TabIndex = 231
        Me.ChkLocation.Text = "တည်နေရာ"
        Me.ChkLocation.UseVisualStyleBackColor = True
        '
        'CboLocation
        '
        Me.CboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CboLocation.Font = New System.Drawing.Font("Myanmar3", 10.0!)
        Me.CboLocation.FormattingEnabled = True
        Me.CboLocation.Location = New System.Drawing.Point(590, 18)
        Me.CboLocation.Name = "CboLocation"
        Me.CboLocation.Size = New System.Drawing.Size(171, 27)
        Me.CboLocation.TabIndex = 232
        '
        'frm_DailyTransactionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(903, 638)
        Me.Controls.Add(Me.rptViewer)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_DailyTransactionReport"
        Me.Text = "Daily Transaction"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpDate.ResumeLayout(False)
        Me.grpDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grpDate As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents rptViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents chkIsBank As System.Windows.Forms.CheckBox
    Friend WithEvents optBank As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents ChkLocation As System.Windows.Forms.CheckBox
    Friend WithEvents CboLocation As System.Windows.Forms.ComboBox
End Class
