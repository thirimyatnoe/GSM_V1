<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExportData
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ExportData))
        Me.grdExport = New System.Windows.Forms.DataGridView()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.grdlocation = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbotransaction = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtToMail = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCCMail = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.grdExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdlocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdExport
        '
        Me.grdExport.AllowUserToAddRows = False
        Me.grdExport.Location = New System.Drawing.Point(13, 38)
        Me.grdExport.Name = "grdExport"
        Me.grdExport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdExport.Size = New System.Drawing.Size(510, 317)
        Me.grdExport.TabIndex = 21
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(800, 436)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(87, 21)
        Me.btnclose.TabIndex = 15
        Me.btnclose.Text = "&Close"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnnew
        '
        Me.btnnew.Location = New System.Drawing.Point(800, 398)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(87, 21)
        Me.btnnew.TabIndex = 16
        Me.btnnew.Text = "&New"
        Me.btnnew.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(684, 398)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(87, 21)
        Me.btnImport.TabIndex = 17
        Me.btnImport.Text = "&Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnexport
        '
        Me.btnexport.Location = New System.Drawing.Point(564, 398)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(87, 21)
        Me.btnexport.TabIndex = 18
        Me.btnexport.Text = "&Export"
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'btndelete
        '
        Me.btndelete.Location = New System.Drawing.Point(684, 436)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(87, 21)
        Me.btndelete.TabIndex = 19
        Me.btndelete.Text = "&Delete"
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'grdlocation
        '
        Me.grdlocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlocation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.grdlocation.Location = New System.Drawing.Point(558, 93)
        Me.grdlocation.Name = "grdlocation"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.grdlocation.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdlocation.Size = New System.Drawing.Size(336, 262)
        Me.grdlocation.TabIndex = 24
        '
        'Column1
        '
        Me.Column1.HeaderText = "LocationID"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Location"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "AllUse"
        Me.Column3.Name = "Column3"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(550, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Transaction Type"
        '
        'cbotransaction
        '
        Me.cbotransaction.FormattingEnabled = True
        Me.cbotransaction.Items.AddRange(New Object() {"--Select--", "Master", "Stock", "Current-Price", "Expense-Income", "Customer,Supplier,Sale-Person", "Other-Transaction", "Transfer-Return", "Shop-Item"})
        Me.cbotransaction.Location = New System.Drawing.Point(694, 30)
        Me.cbotransaction.Name = "cbotransaction"
        Me.cbotransaction.Size = New System.Drawing.Size(177, 21)
        Me.cbotransaction.TabIndex = 22
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(564, 436)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(87, 21)
        Me.btnSave.TabIndex = 25
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(922, 16)
        Me.btnHelpbook.Margin = New System.Windows.Forms.Padding(2)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(36, 33)
        Me.btnHelpbook.TabIndex = 226
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'txtCompanyName
        '
        Me.txtCompanyName.BackColor = System.Drawing.Color.White
        Me.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyName.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCompanyName.Location = New System.Drawing.Point(119, 364)
        Me.txtCompanyName.Multiline = True
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(329, 29)
        Me.txtCompanyName.TabIndex = 1374
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Myanmar3", 9.749999!)
        Me.Label8.Location = New System.Drawing.Point(12, 366)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 19)
        Me.Label8.TabIndex = 1375
        Me.Label8.Text = "Company Name"
        '
        'txtToMail
        '
        Me.txtToMail.BackColor = System.Drawing.Color.White
        Me.txtToMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToMail.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtToMail.Location = New System.Drawing.Point(119, 397)
        Me.txtToMail.Multiline = True
        Me.txtToMail.Name = "txtToMail"
        Me.txtToMail.Size = New System.Drawing.Size(329, 29)
        Me.txtToMail.TabIndex = 1376
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.749999!)
        Me.Label2.Location = New System.Drawing.Point(57, 399)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 19)
        Me.Label2.TabIndex = 1377
        Me.Label2.Text = "To Mail"
        '
        'txtCCMail
        '
        Me.txtCCMail.BackColor = System.Drawing.Color.White
        Me.txtCCMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCMail.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.txtCCMail.Location = New System.Drawing.Point(119, 430)
        Me.txtCCMail.Multiline = True
        Me.txtCCMail.Name = "txtCCMail"
        Me.txtCCMail.Size = New System.Drawing.Size(329, 29)
        Me.txtCCMail.TabIndex = 1378
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 9.749999!)
        Me.Label3.Location = New System.Drawing.Point(53, 432)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 19)
        Me.Label3.TabIndex = 1379
        Me.Label3.Text = "CC Mail"
        '
        'frm_ExportData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 493)
        Me.Controls.Add(Me.txtCCMail)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtToMail)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCompanyName)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdExport)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnnew)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnexport)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.grdlocation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbotransaction)
        Me.Name = "frm_ExportData"
        Me.Text = "Export Data"
        CType(Me.grdExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdlocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdExport As System.Windows.Forms.DataGridView
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnnew As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents grdlocation As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbotransaction As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtToMail As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCCMail As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
