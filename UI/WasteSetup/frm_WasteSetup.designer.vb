<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_WasteSetup
    Inherits UI.frm_Base

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_WasteSetup))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
        Me.btnSearchWasteHeaderID = New System.Windows.Forms.Button()
        Me.lblCurrentLocationName = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtWasteSetupHeaderID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboItemCategory = New System.Windows.Forms.ComboBox()
        Me.cboItemName = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.grdWasteDetail = New System.Windows.Forms.DataGridView()
        Me.GoldQualityID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemNameDetailID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemNameID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        CType(Me.grdWasteDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLogInUserName
        '
        Me.lblLogInUserName.AutoSize = True
        Me.lblLogInUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblLogInUserName.Font = New System.Drawing.Font("Myanmar3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogInUserName.ForeColor = System.Drawing.Color.Violet
        Me.lblLogInUserName.Location = New System.Drawing.Point(492, 43)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(90, 22)
        Me.lblLogInUserName.TabIndex = 17
        Me.lblLogInUserName.Text = "LogInUser"
        Me.lblLogInUserName.Visible = False
        '
        'btnSearchWasteHeaderID
        '
        Me.btnSearchWasteHeaderID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchWasteHeaderID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchWasteHeaderID.Image = CType(resources.GetObject("btnSearchWasteHeaderID.Image"), System.Drawing.Image)
        Me.btnSearchWasteHeaderID.Location = New System.Drawing.Point(270, 15)
        Me.btnSearchWasteHeaderID.Name = "btnSearchWasteHeaderID"
        Me.btnSearchWasteHeaderID.Size = New System.Drawing.Size(41, 21)
        Me.btnSearchWasteHeaderID.TabIndex = 2
        Me.btnSearchWasteHeaderID.UseVisualStyleBackColor = True
        '
        'lblCurrentLocationName
        '
        Me.lblCurrentLocationName.AutoSize = True
        Me.lblCurrentLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentLocationName.Font = New System.Drawing.Font("Myanmar3", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblCurrentLocationName.ForeColor = System.Drawing.Color.DarkCyan
        Me.lblCurrentLocationName.Location = New System.Drawing.Point(488, 81)
        Me.lblCurrentLocationName.Name = "lblCurrentLocationName"
        Me.lblCurrentLocationName.Size = New System.Drawing.Size(84, 19)
        Me.lblCurrentLocationName.TabIndex = 16
        Me.lblCurrentLocationName.Text = "HeadOffice"
        Me.lblCurrentLocationName.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label10.Location = New System.Drawing.Point(30, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(112, 20)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "ပစ္စည်းအမျိုးအစား"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(153, 118)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(419, 23)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "အတင်  မှ  အထိ"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWasteSetupHeaderID
        '
        Me.txtWasteSetupHeaderID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWasteSetupHeaderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWasteSetupHeaderID.Location = New System.Drawing.Point(145, 15)
        Me.txtWasteSetupHeaderID.Name = "txtWasteSetupHeaderID"
        Me.txtWasteSetupHeaderID.ReadOnly = True
        Me.txtWasteSetupHeaderID.Size = New System.Drawing.Size(119, 20)
        Me.txtWasteSetupHeaderID.TabIndex = 1
        Me.txtWasteSetupHeaderID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(30, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 17)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Waste Setup ID:"
        '
        'cboItemCategory
        '
        Me.cboItemCategory.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.cboItemCategory.FormattingEnabled = True
        Me.cboItemCategory.Location = New System.Drawing.Point(145, 41)
        Me.cboItemCategory.Name = "cboItemCategory"
        Me.cboItemCategory.Size = New System.Drawing.Size(217, 27)
        Me.cboItemCategory.TabIndex = 3
        '
        'cboItemName
        '
        Me.cboItemName.Font = New System.Drawing.Font("Myanmar3", 9.5!)
        Me.cboItemName.FormattingEnabled = True
        Me.cboItemName.Location = New System.Drawing.Point(145, 81)
        Me.cboItemName.Name = "cboItemName"
        Me.cboItemName.Size = New System.Drawing.Size(217, 27)
        Me.cboItemName.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label2.Location = New System.Drawing.Point(12, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 20)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "ပစ္စည်းအမျိုးအမည်"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(576, 118)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(141, 23)
        Me.Label13.TabIndex = 202
        Me.Label13.Text = "အထည် အလျော့"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdWasteDetail
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdWasteDetail.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdWasteDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdWasteDetail.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdWasteDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdWasteDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdWasteDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GoldQualityID, Me.ItemNameDetailID, Me.ItemNameID, Me.Column1, Me.Column2, Me.Column3, Me.Column5, Me.Column6, Me.Column7})
        Me.grdWasteDetail.Location = New System.Drawing.Point(12, 144)
        Me.grdWasteDetail.MultiSelect = False
        Me.grdWasteDetail.Name = "grdWasteDetail"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdWasteDetail.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdWasteDetail.RowHeadersWidth = 25
        Me.grdWasteDetail.Size = New System.Drawing.Size(705, 417)
        Me.grdWasteDetail.TabIndex = 5
        '
        'GoldQualityID
        '
        Me.GoldQualityID.HeaderText = "GoldQualityID"
        Me.GoldQualityID.Name = "GoldQualityID"
        Me.GoldQualityID.Width = 72
        '
        'ItemNameDetailID
        '
        Me.ItemNameDetailID.HeaderText = "ItemNameDetailID"
        Me.ItemNameDetailID.Name = "ItemNameDetailID"
        Me.ItemNameDetailID.Visible = False
        '
        'ItemNameID
        '
        Me.ItemNameID.HeaderText = "ItemNameID"
        Me.ItemNameID.Name = "ItemNameID"
        Me.ItemNameID.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "MinK"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.HeaderText = "MinP"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 50
        '
        'Column3
        '
        Me.Column3.HeaderText = "MinY"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 50
        '
        'Column5
        '
        Me.Column5.HeaderText = "MaxK"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 50
        '
        'Column6
        '
        Me.Column6.HeaderText = "MaxP"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 50
        '
        'Column7
        '
        Me.Column7.HeaderText = "MaxY"
        Me.Column7.Name = "Column7"
        Me.Column7.Width = 50
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(667, 15)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 32)
        Me.btnHelpbook.TabIndex = 225
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_WasteSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 609)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.grdWasteDetail)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboItemCategory)
        Me.Controls.Add(Me.cboItemName)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.btnSearchWasteHeaderID)
        Me.Controls.Add(Me.lblCurrentLocationName)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtWasteSetupHeaderID)
        Me.Controls.Add(Me.Label9)
        Me.KeyPreview = True
        Me.Name = "frm_WasteSetup"
        Me.Text = "Waste Setup"
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtWasteSetupHeaderID, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.lblCurrentLocationName, 0)
        Me.Controls.SetChildIndex(Me.btnSearchWasteHeaderID, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
        Me.Controls.SetChildIndex(Me.cboItemName, 0)
        Me.Controls.SetChildIndex(Me.cboItemCategory, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.grdWasteDetail, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        CType(Me.grdWasteDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
    Friend WithEvents btnSearchWasteHeaderID As System.Windows.Forms.Button
    Friend WithEvents lblCurrentLocationName As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtWasteSetupHeaderID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboItemCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cboItemName As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents grdWasteDetail As System.Windows.Forms.DataGridView
    Friend WithEvents GoldQualityID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemNameDetailID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemNameID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
End Class
