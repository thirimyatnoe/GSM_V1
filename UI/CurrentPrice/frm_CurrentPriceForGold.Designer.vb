<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CurrentPriceForGold
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CurrentPriceForGold))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtSolidGoldRate = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnCalculate = New System.Windows.Forms.Button()
        Me.grdGoldPrice = New System.Windows.Forms.DataGridView()
        Me.lblIsGram = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        CType(Me.grdGoldPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtSolidGoldRate
        '
        Me.txtSolidGoldRate.BackColor = System.Drawing.Color.White
        Me.txtSolidGoldRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSolidGoldRate.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtSolidGoldRate.Location = New System.Drawing.Point(107, 42)
        Me.txtSolidGoldRate.Name = "txtSolidGoldRate"
        Me.txtSolidGoldRate.Size = New System.Drawing.Size(100, 22)
        Me.txtSolidGoldRate.TabIndex = 1447
        Me.txtSolidGoldRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(17, 40)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(84, 19)
        Me.Label38.TabIndex = 1455
        Me.Label38.Text = "အခေါက်စျေး"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd-MM-yyyy hh:mm tt"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(107, 12)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(150, 20)
        Me.dtpDate.TabIndex = 1445
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(65, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 19)
        Me.Label10.TabIndex = 1453
        Me.Label10.Text = "နေ့စွဲ"
        '
        'btnCalculate
        '
        Me.btnCalculate.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalculate.ForeColor = System.Drawing.Color.Navy
        Me.btnCalculate.Image = CType(resources.GetObject("btnCalculate.Image"), System.Drawing.Image)
        Me.btnCalculate.Location = New System.Drawing.Point(418, 32)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(87, 38)
        Me.btnCalculate.TabIndex = 1456
        Me.btnCalculate.Text = "Calculate"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'grdGoldPrice
        '
        Me.grdGoldPrice.AllowUserToAddRows = False
        Me.grdGoldPrice.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.grdGoldPrice.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdGoldPrice.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdGoldPrice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdGoldPrice.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grdGoldPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdGoldPrice.Location = New System.Drawing.Point(9, 85)
        Me.grdGoldPrice.Name = "grdGoldPrice"
        Me.grdGoldPrice.RowHeadersWidth = 25
        Me.grdGoldPrice.Size = New System.Drawing.Size(943, 280)
        Me.grdGoldPrice.TabIndex = 1457
        '
        'lblIsGram
        '
        Me.lblIsGram.AutoSize = True
        Me.lblIsGram.BackColor = System.Drawing.Color.Transparent
        Me.lblIsGram.Font = New System.Drawing.Font("Myanmar3", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblIsGram.Location = New System.Drawing.Point(210, 43)
        Me.lblIsGram.Name = "lblIsGram"
        Me.lblIsGram.Size = New System.Drawing.Size(92, 17)
        Me.lblIsGram.TabIndex = 1458
        Me.lblIsGram.Text = "၁ ကျပ်သားစျေး"
        Me.lblIsGram.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(263, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(39, 21)
        Me.btnSearch.TabIndex = 1459
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'btnPrint
        '
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Zawgyi-One", 9.25!)
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(545, 36)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(81, 31)
        Me.btnPrint.TabIndex = 1460
        Me.btnPrint.Text = "View"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(918, 8)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 32)
        Me.btnHelpbook.TabIndex = 1461
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_CurrentPriceForGold
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(962, 418)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.lblIsGram)
        Me.Controls.Add(Me.grdGoldPrice)
        Me.Controls.Add(Me.btnCalculate)
        Me.Controls.Add(Me.txtSolidGoldRate)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.Label10)
        Me.KeyPreview = True
        Me.Name = "frm_CurrentPriceForGold"
        Me.Text = "Define Gold Price"
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.dtpDate, 0)
        Me.Controls.SetChildIndex(Me.Label38, 0)
        Me.Controls.SetChildIndex(Me.txtSolidGoldRate, 0)
        Me.Controls.SetChildIndex(Me.btnCalculate, 0)
        Me.Controls.SetChildIndex(Me.grdGoldPrice, 0)
        Me.Controls.SetChildIndex(Me.lblIsGram, 0)
        Me.Controls.SetChildIndex(Me.btnSearch, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        CType(Me.grdGoldPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSolidGoldRate As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnCalculate As System.Windows.Forms.Button
    Friend WithEvents grdGoldPrice As System.Windows.Forms.DataGridView
    Friend WithEvents lblIsGram As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button

End Class
