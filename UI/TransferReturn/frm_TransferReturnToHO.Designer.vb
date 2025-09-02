<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_TransferReturnToHO
    Inherits UI.frm_Base

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_TransferReturnToHO))
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.txtTotalQTY = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.grdTransferReturnITem = New System.Windows.Forms.DataGridView()
        Me.ItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SearchItem = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalPriceGram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalPriceTK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalFixedPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalGemsPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriceCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FixPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpTDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.txtTransferID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Save = New System.Windows.Forms.SaveFileDialog()
        Me.lblLogInUserName = New System.Windows.Forms.Label()
        Me.lblCurrentLocationName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTotalTG = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.lblGold = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.txtTotalK = New System.Windows.Forms.TextBox()
        Me.txtTotalY = New System.Windows.Forms.TextBox()
        Me.txtTotalP = New System.Windows.Forms.TextBox()
        CType(Me.grdTransferReturnITem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboStaff
        '
        Me.cboStaff.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(137, 71)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(194, 25)
        Me.cboStaff.TabIndex = 766
        '
        'txtTotalQTY
        '
        Me.txtTotalQTY.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalQTY.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalQTY.Location = New System.Drawing.Point(625, 79)
        Me.txtTotalQTY.MaxLength = 3
        Me.txtTotalQTY.Name = "txtTotalQTY"
        Me.txtTotalQTY.ReadOnly = True
        Me.txtTotalQTY.Size = New System.Drawing.Size(59, 21)
        Me.txtTotalQTY.TabIndex = 784
        Me.txtTotalQTY.TabStop = False
        Me.txtTotalQTY.Text = "0"
        Me.txtTotalQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 9.25!)
        Me.Label2.Location = New System.Drawing.Point(544, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 19)
        Me.Label2.TabIndex = 785
        Me.Label2.Text = "Total QTY"
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.White
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.txtRemark.Location = New System.Drawing.Point(625, 8)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(250, 65)
        Me.txtRemark.TabIndex = 767
        '
        'grdTransferReturnITem
        '
        Me.grdTransferReturnITem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTransferReturnITem.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTransferReturnITem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ItemCode, Me.SearchItem, Me.ItemName, Me.Length, Me.OriginalPriceGram, Me.OriginalPriceTK, Me.OriginalFixedPrice, Me.OriginalGemsPrice, Me.PriceCode, Me.FixPrice, Me.Width, Me.Gram})
        Me.grdTransferReturnITem.Location = New System.Drawing.Point(12, 167)
        Me.grdTransferReturnITem.Name = "grdTransferReturnITem"
        Me.grdTransferReturnITem.RowHeadersWidth = 25
        Me.grdTransferReturnITem.Size = New System.Drawing.Size(1210, 362)
        Me.grdTransferReturnITem.TabIndex = 769
        '
        'ItemCode
        '
        Me.ItemCode.HeaderText = "Code"
        Me.ItemCode.Name = "ItemCode"
        Me.ItemCode.Width = 110
        '
        'SearchItem
        '
        Me.SearchItem.HeaderText = "..."
        Me.SearchItem.Name = "SearchItem"
        Me.SearchItem.Width = 25
        '
        'ItemName
        '
        Me.ItemName.HeaderText = "Name"
        Me.ItemName.Name = "ItemName"
        Me.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ItemName.Width = 200
        '
        'Length
        '
        Me.Length.HeaderText = "Length"
        Me.Length.Name = "Length"
        Me.Length.Width = 80
        '
        'OriginalPriceGram
        '
        Me.OriginalPriceGram.HeaderText = "OriginalPriceGram"
        Me.OriginalPriceGram.Name = "OriginalPriceGram"
        '
        'OriginalPriceTK
        '
        Me.OriginalPriceTK.HeaderText = "OriginalPriceTK"
        Me.OriginalPriceTK.Name = "OriginalPriceTK"
        '
        'OriginalFixedPrice
        '
        Me.OriginalFixedPrice.HeaderText = "OriginalFixedPrice"
        Me.OriginalFixedPrice.Name = "OriginalFixedPrice"
        '
        'OriginalGemsPrice
        '
        Me.OriginalGemsPrice.HeaderText = "OriginalGemsPrice"
        Me.OriginalGemsPrice.Name = "OriginalGemsPrice"
        '
        'PriceCode
        '
        Me.PriceCode.HeaderText = "PriceCode"
        Me.PriceCode.Name = "PriceCode"
        '
        'FixPrice
        '
        Me.FixPrice.HeaderText = "FixPrice"
        Me.FixPrice.Name = "FixPrice"
        '
        'Width
        '
        Me.Width.HeaderText = "Width"
        Me.Width.Name = "Width"
        Me.Width.Width = 80
        '
        'Gram
        '
        Me.Gram.HeaderText = "Gram"
        Me.Gram.Name = "Gram"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Gainsboro
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1210, 21)
        Me.Label7.TabIndex = 776
        Me.Label7.Text = "Transfer Return Items"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpTDate
        '
        Me.dtpTDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpTDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTDate.Location = New System.Drawing.Point(137, 37)
        Me.dtpTDate.Name = "dtpTDate"
        Me.dtpTDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpTDate.TabIndex = 765
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Myanmar3", 9.25!)
        Me.Label10.Location = New System.Drawing.Point(102, 38)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 19)
        Me.Label10.TabIndex = 774
        Me.Label10.Text = "နေ့စွဲ"
        '
        'SearchButton
        '
        Me.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchButton.Image = CType(resources.GetObject("SearchButton.Image"), System.Drawing.Image)
        Me.SearchButton.Location = New System.Drawing.Point(253, 11)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(39, 21)
        Me.SearchButton.TabIndex = 764
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'txtTransferID
        '
        Me.txtTransferID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTransferID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferID.Location = New System.Drawing.Point(137, 11)
        Me.txtTransferID.Name = "txtTransferID"
        Me.txtTransferID.ReadOnly = True
        Me.txtTransferID.Size = New System.Drawing.Size(110, 20)
        Me.txtTransferID.TabIndex = 763
        Me.txtTransferID.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(18, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(119, 16)
        Me.Label9.TabIndex = 773
        Me.Label9.Text = "Transfer Return No"
        '
        'Save
        '
        Me.Save.FileName = "MSIM"
        '
        'lblLogInUserName
        '
        Me.lblLogInUserName.AutoSize = True
        Me.lblLogInUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblLogInUserName.Font = New System.Drawing.Font("Myanmar3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogInUserName.ForeColor = System.Drawing.Color.Violet
        Me.lblLogInUserName.Location = New System.Drawing.Point(279, 37)
        Me.lblLogInUserName.Name = "lblLogInUserName"
        Me.lblLogInUserName.Size = New System.Drawing.Size(90, 22)
        Me.lblLogInUserName.TabIndex = 1357
        Me.lblLogInUserName.Text = "LogInUser"
        '
        'lblCurrentLocationName
        '
        Me.lblCurrentLocationName.AutoSize = True
        Me.lblCurrentLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentLocationName.Font = New System.Drawing.Font("Myanmar3", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLocationName.ForeColor = System.Drawing.Color.DarkCyan
        Me.lblCurrentLocationName.Location = New System.Drawing.Point(298, 13)
        Me.lblCurrentLocationName.Name = "lblCurrentLocationName"
        Me.lblCurrentLocationName.Size = New System.Drawing.Size(88, 19)
        Me.lblCurrentLocationName.TabIndex = 1356
        Me.lblCurrentLocationName.Text = "Head Office"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label4.Location = New System.Drawing.Point(79, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 20)
        Me.Label4.TabIndex = 1358
        Me.Label4.Text = "တာဝန်ခံ"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Myanmar3", 9.8!)
        Me.Label22.Location = New System.Drawing.Point(560, 9)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(59, 20)
        Me.Label22.TabIndex = 1359
        Me.Label22.Text = "မှတ်ချက်"
        '
        'txtTotalTG
        '
        Me.txtTotalTG.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalTG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalTG.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalTG.Location = New System.Drawing.Point(793, 108)
        Me.txtTotalTG.MaxLength = 6
        Me.txtTotalTG.Name = "txtTotalTG"
        Me.txtTotalTG.ReadOnly = True
        Me.txtTotalTG.Size = New System.Drawing.Size(55, 21)
        Me.txtTotalTG.TabIndex = 1630
        Me.txtTotalTG.Text = "0.0"
        Me.txtTotalTG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label61.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label61.Location = New System.Drawing.Point(774, 111)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(18, 16)
        Me.Label61.TabIndex = 1629
        Me.Label61.Text = "Y"
        '
        'lblGold
        '
        Me.lblGold.AutoSize = True
        Me.lblGold.BackColor = System.Drawing.Color.Transparent
        Me.lblGold.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.lblGold.ForeColor = System.Drawing.Color.Black
        Me.lblGold.Location = New System.Drawing.Point(545, 109)
        Me.lblGold.Name = "lblGold"
        Me.lblGold.Size = New System.Drawing.Size(78, 17)
        Me.lblGold.TabIndex = 1628
        Me.lblGold.Text = "TotalWeight"
        Me.lblGold.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.Color.Transparent
        Me.Label66.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label66.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label66.Location = New System.Drawing.Point(716, 110)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(16, 16)
        Me.Label66.TabIndex = 1627
        Me.Label66.Text = "P"
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.BackColor = System.Drawing.Color.Transparent
        Me.Label67.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label67.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label67.Location = New System.Drawing.Point(660, 110)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(18, 16)
        Me.Label67.TabIndex = 1626
        Me.Label67.Text = "K"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.BackColor = System.Drawing.Color.Transparent
        Me.Label84.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label84.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label84.Location = New System.Drawing.Point(850, 111)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(40, 16)
        Me.Label84.TabIndex = 1625
        Me.Label84.Text = "Gram"
        '
        'txtTotalK
        '
        Me.txtTotalK.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalK.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalK.Location = New System.Drawing.Point(625, 107)
        Me.txtTotalK.MaxLength = 3
        Me.txtTotalK.Name = "txtTotalK"
        Me.txtTotalK.ReadOnly = True
        Me.txtTotalK.Size = New System.Drawing.Size(35, 21)
        Me.txtTotalK.TabIndex = 1622
        Me.txtTotalK.TabStop = False
        Me.txtTotalK.Text = "0"
        Me.txtTotalK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTotalY
        '
        Me.txtTotalY.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalY.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalY.Location = New System.Drawing.Point(739, 108)
        Me.txtTotalY.MaxLength = 3
        Me.txtTotalY.Name = "txtTotalY"
        Me.txtTotalY.ReadOnly = True
        Me.txtTotalY.Size = New System.Drawing.Size(35, 21)
        Me.txtTotalY.TabIndex = 1624
        Me.txtTotalY.TabStop = False
        Me.txtTotalY.Text = "0"
        Me.txtTotalY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTotalP
        '
        Me.txtTotalP.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalP.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotalP.Location = New System.Drawing.Point(680, 107)
        Me.txtTotalP.MaxLength = 2
        Me.txtTotalP.Name = "txtTotalP"
        Me.txtTotalP.ReadOnly = True
        Me.txtTotalP.Size = New System.Drawing.Size(35, 21)
        Me.txtTotalP.TabIndex = 1623
        Me.txtTotalP.TabStop = False
        Me.txtTotalP.Text = "0"
        Me.txtTotalP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frm_TransferReturnToHO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.txtTotalTG)
        Me.Controls.Add(Me.Label61)
        Me.Controls.Add(Me.lblGold)
        Me.Controls.Add(Me.Label66)
        Me.Controls.Add(Me.Label67)
        Me.Controls.Add(Me.Label84)
        Me.Controls.Add(Me.txtTotalK)
        Me.Controls.Add(Me.txtTotalY)
        Me.Controls.Add(Me.txtTotalP)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblLogInUserName)
        Me.Controls.Add(Me.lblCurrentLocationName)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.txtTotalQTY)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.grdTransferReturnITem)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpTDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.txtTransferID)
        Me.Controls.Add(Me.Label9)
        Me.KeyPreview = True
        Me.Name = "frm_TransferReturnToHO"
        Me.Text = "Transfer Return Items"
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtTransferID, 0)
        Me.Controls.SetChildIndex(Me.SearchButton, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.dtpTDate, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.grdTransferReturnITem, 0)
        Me.Controls.SetChildIndex(Me.txtRemark, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtTotalQTY, 0)
        Me.Controls.SetChildIndex(Me.cboStaff, 0)
        Me.Controls.SetChildIndex(Me.lblCurrentLocationName, 0)
        Me.Controls.SetChildIndex(Me.lblLogInUserName, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.txtTotalP, 0)
        Me.Controls.SetChildIndex(Me.txtTotalY, 0)
        Me.Controls.SetChildIndex(Me.txtTotalK, 0)
        Me.Controls.SetChildIndex(Me.Label84, 0)
        Me.Controls.SetChildIndex(Me.Label67, 0)
        Me.Controls.SetChildIndex(Me.Label66, 0)
        Me.Controls.SetChildIndex(Me.lblGold, 0)
        Me.Controls.SetChildIndex(Me.Label61, 0)
        Me.Controls.SetChildIndex(Me.txtTotalTG, 0)
        CType(Me.grdTransferReturnITem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents txtTotalQTY As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents grdTransferReturnITem As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpTDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents txtTransferID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Save As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblLogInUserName As System.Windows.Forms.Label
    Friend WithEvents lblCurrentLocationName As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtTotalTG As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents lblGold As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents txtTotalK As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalY As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalP As System.Windows.Forms.TextBox
    Friend WithEvents ItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SearchItem As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Length As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriginalPriceGram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriginalPriceTK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriginalFixedPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriginalGemsPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PriceCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FixPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Width As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Gram As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
