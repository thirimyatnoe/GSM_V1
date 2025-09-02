<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GenerateFomat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GenerateFomat))
        Me.txtGenerateFormat = New System.Windows.Forms.TextBox()
        Me.GpFormat = New System.Windows.Forms.GroupBox()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboNumberCount = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPrefix2 = New System.Windows.Forms.TextBox()
        Me.cboDateFormat2 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboDateFormat1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.cboTableType = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.optFirst = New System.Windows.Forms.RadioButton()
        Me.optLast = New System.Windows.Forms.RadioButton()
        Me.optNot = New System.Windows.Forms.RadioButton()
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.chkIsGenerate = New System.Windows.Forms.CheckBox()
        Me.GpFormat.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtGenerateFormat
        '
        Me.txtGenerateFormat.BackColor = System.Drawing.SystemColors.Window
        Me.txtGenerateFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGenerateFormat.Location = New System.Drawing.Point(403, 7)
        Me.txtGenerateFormat.Multiline = True
        Me.txtGenerateFormat.Name = "txtGenerateFormat"
        Me.txtGenerateFormat.Size = New System.Drawing.Size(197, 21)
        Me.txtGenerateFormat.TabIndex = 5
        Me.txtGenerateFormat.Visible = False
        '
        'GpFormat
        '
        Me.GpFormat.BackColor = System.Drawing.Color.Transparent
        Me.GpFormat.Controls.Add(Me.txtResult)
        Me.GpFormat.Controls.Add(Me.Label6)
        Me.GpFormat.Controls.Add(Me.cboNumberCount)
        Me.GpFormat.Controls.Add(Me.Label5)
        Me.GpFormat.Controls.Add(Me.Label4)
        Me.GpFormat.Controls.Add(Me.txtPrefix2)
        Me.GpFormat.Controls.Add(Me.cboDateFormat2)
        Me.GpFormat.Controls.Add(Me.Label3)
        Me.GpFormat.Controls.Add(Me.cboDateFormat1)
        Me.GpFormat.Controls.Add(Me.Label2)
        Me.GpFormat.Controls.Add(Me.lblPrefix)
        Me.GpFormat.Controls.Add(Me.txtPrefix)
        Me.GpFormat.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold)
        Me.GpFormat.Location = New System.Drawing.Point(7, 144)
        Me.GpFormat.Name = "GpFormat"
        Me.GpFormat.Size = New System.Drawing.Size(653, 171)
        Me.GpFormat.TabIndex = 8
        Me.GpFormat.TabStop = False
        Me.GpFormat.Text = "Format"
        '
        'txtResult
        '
        Me.txtResult.BackColor = System.Drawing.SystemColors.Window
        Me.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResult.ForeColor = System.Drawing.Color.DarkRed
        Me.txtResult.Location = New System.Drawing.Point(171, 80)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ReadOnly = True
        Me.txtResult.Size = New System.Drawing.Size(202, 79)
        Me.txtResult.TabIndex = 64
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(116, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 63
        Me.Label6.Text = "Result :"
        '
        'cboNumberCount
        '
        Me.cboNumberCount.FormattingEnabled = True
        Me.cboNumberCount.Items.AddRange(New Object() {"0000", "00000"})
        Me.cboNumberCount.Location = New System.Drawing.Point(460, 41)
        Me.cboNumberCount.Name = "cboNumberCount"
        Me.cboNumberCount.Size = New System.Drawing.Size(123, 24)
        Me.cboNumberCount.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(463, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 16)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Number  "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(392, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 16)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Prefix 2 "
        '
        'txtPrefix2
        '
        Me.txtPrefix2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrefix2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrefix2.Location = New System.Drawing.Point(389, 40)
        Me.txtPrefix2.Multiline = True
        Me.txtPrefix2.Name = "txtPrefix2"
        Me.txtPrefix2.Size = New System.Drawing.Size(64, 21)
        Me.txtPrefix2.TabIndex = 3
        '
        'cboDateFormat2
        '
        Me.cboDateFormat2.DisplayMember = "FormatDate2"
        Me.cboDateFormat2.FormattingEnabled = True
        Me.cboDateFormat2.Items.AddRange(New Object() {"yy"})
        Me.cboDateFormat2.Location = New System.Drawing.Point(244, 43)
        Me.cboDateFormat2.Name = "cboDateFormat2"
        Me.cboDateFormat2.Size = New System.Drawing.Size(140, 24)
        Me.cboDateFormat2.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(245, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 16)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Date Format 2 "
        '
        'cboDateFormat1
        '
        Me.cboDateFormat1.DisplayMember = "FormatDate1"
        Me.cboDateFormat1.FormattingEnabled = True
        Me.cboDateFormat1.Items.AddRange(New Object() {"yy"})
        Me.cboDateFormat1.Location = New System.Drawing.Point(89, 43)
        Me.cboDateFormat1.Name = "cboDateFormat1"
        Me.cboDateFormat1.Size = New System.Drawing.Size(140, 24)
        Me.cboDateFormat1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(102, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 16)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "Date Format "
        '
        'lblPrefix
        '
        Me.lblPrefix.AutoSize = True
        Me.lblPrefix.BackColor = System.Drawing.Color.Transparent
        Me.lblPrefix.Font = New System.Drawing.Font("Myanmar3", 8.249999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrefix.Location = New System.Drawing.Point(13, 21)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(47, 16)
        Me.lblPrefix.TabIndex = 54
        Me.lblPrefix.Text = "Prefix "
        '
        'txtPrefix
        '
        Me.txtPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrefix.Location = New System.Drawing.Point(10, 43)
        Me.txtPrefix.Multiline = True
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(64, 21)
        Me.txtPrefix.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(490, 31)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(110, 13)
        Me.Label17.TabIndex = 234
        Me.Label17.Text = "Generate Format :"
        Me.Label17.Visible = False
        '
        'SearchButton
        '
        Me.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchButton.Image = CType(resources.GetObject("SearchButton.Image"), System.Drawing.Image)
        Me.SearchButton.Location = New System.Drawing.Point(603, 7)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(30, 21)
        Me.SearchButton.TabIndex = 238
        Me.SearchButton.UseVisualStyleBackColor = True
        Me.SearchButton.Visible = False
        '
        'cboTableType
        '
        Me.cboTableType.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.cboTableType.FormattingEnabled = True
        Me.cboTableType.Items.AddRange(New Object() {".....Selected.....", "PurchaseStock", "SalesGem", "SaleStock", "SaleVolumeStock", "OrderStock", "Barcode", "RepairStock", "ReturnAdvance", "WholeSaleStock", "WholeSaleReturnStock", "ConsignmentSaleStock", "MortgageItemCode", "MortgageInvoiceCode", "MortgageReceive", "DiamondBarcode", "SaleLooseDiamond"})
        Me.cboTableType.Location = New System.Drawing.Point(192, 9)
        Me.cboTableType.Name = "cboTableType"
        Me.cboTableType.Size = New System.Drawing.Size(197, 25)
        Me.cboTableType.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Myanmar3", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(19, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 16)
        Me.Label7.TabIndex = 240
        Me.Label7.Text = "For Generate Format Table :"
        '
        'optFirst
        '
        Me.optFirst.AutoSize = True
        Me.optFirst.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.optFirst.Location = New System.Drawing.Point(17, 49)
        Me.optFirst.Name = "optFirst"
        Me.optFirst.Size = New System.Drawing.Size(329, 21)
        Me.optFirst.TabIndex = 242
        Me.optFirst.TabStop = True
        Me.optFirst.Text = "ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကိုရှေ့ဆုံးတွင်ထားပါ။"
        Me.optFirst.UseVisualStyleBackColor = True
        '
        'optLast
        '
        Me.optLast.AutoSize = True
        Me.optLast.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.optLast.Location = New System.Drawing.Point(17, 77)
        Me.optLast.Name = "optLast"
        Me.optLast.Size = New System.Drawing.Size(346, 21)
        Me.optLast.TabIndex = 243
        Me.optLast.TabStop = True
        Me.optLast.Text = "ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကိုနောက်ဆုံးတွင်ထားပါ။"
        Me.optLast.UseVisualStyleBackColor = True
        '
        'optNot
        '
        Me.optNot.AutoSize = True
        Me.optNot.Font = New System.Drawing.Font("Myanmar3", 9.0!)
        Me.optNot.Location = New System.Drawing.Point(17, 105)
        Me.optNot.Name = "optNot"
        Me.optNot.Size = New System.Drawing.Size(359, 21)
        Me.optNot.TabIndex = 244
        Me.optNot.TabStop = True
        Me.optNot.Text = "ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကို Barcode တွင်မထည့်ပါ။"
        Me.optNot.UseVisualStyleBackColor = True
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(641, 3)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(33, 32)
        Me.btnHelpbook.TabIndex = 1459
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'chkIsGenerate
        '
        Me.chkIsGenerate.AutoSize = True
        Me.chkIsGenerate.BackColor = System.Drawing.Color.Transparent
        Me.chkIsGenerate.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkIsGenerate.Font = New System.Drawing.Font("Myanmar3", 8.5!, System.Drawing.FontStyle.Bold)
        Me.chkIsGenerate.ForeColor = System.Drawing.Color.Blue
        Me.chkIsGenerate.Location = New System.Drawing.Point(374, 51)
        Me.chkIsGenerate.Name = "chkIsGenerate"
        Me.chkIsGenerate.Size = New System.Drawing.Size(95, 21)
        Me.chkIsGenerate.TabIndex = 1473
        Me.chkIsGenerate.Text = "IsGenerate"
        Me.chkIsGenerate.UseVisualStyleBackColor = False
        '
        'frm_GenerateFomat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(677, 365)
        Me.Controls.Add(Me.chkIsGenerate)
        Me.Controls.Add(Me.btnHelpbook)
        Me.Controls.Add(Me.optNot)
        Me.Controls.Add(Me.optLast)
        Me.Controls.Add(Me.optFirst)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboTableType)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.txtGenerateFormat)
        Me.Controls.Add(Me.GpFormat)
        Me.Controls.Add(Me.Label17)
        Me.KeyPreview = True
        Me.Name = "frm_GenerateFomat"
        Me.Text = "Generate Format"
        Me.Controls.SetChildIndex(Me.Label17, 0)
        Me.Controls.SetChildIndex(Me.GpFormat, 0)
        Me.Controls.SetChildIndex(Me.txtGenerateFormat, 0)
        Me.Controls.SetChildIndex(Me.SearchButton, 0)
        Me.Controls.SetChildIndex(Me.cboTableType, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.optFirst, 0)
        Me.Controls.SetChildIndex(Me.optLast, 0)
        Me.Controls.SetChildIndex(Me.optNot, 0)
        Me.Controls.SetChildIndex(Me.btnHelpbook, 0)
        Me.Controls.SetChildIndex(Me.chkIsGenerate, 0)
        Me.GpFormat.ResumeLayout(False)
        Me.GpFormat.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtGenerateFormat As System.Windows.Forms.TextBox
    ' Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GpFormat As System.Windows.Forms.GroupBox
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboNumberCount As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPrefix2 As System.Windows.Forms.TextBox
    Friend WithEvents cboDateFormat2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboDateFormat1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPrefix As System.Windows.Forms.Label
    Friend WithEvents txtPrefix As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents cboTableType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents optFirst As System.Windows.Forms.RadioButton
    Friend WithEvents optLast As System.Windows.Forms.RadioButton
    Friend WithEvents optNot As System.Windows.Forms.RadioButton
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button
    Friend WithEvents chkIsGenerate As System.Windows.Forms.CheckBox

End Class
