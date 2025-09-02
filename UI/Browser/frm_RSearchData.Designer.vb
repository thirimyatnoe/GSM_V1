<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchData
    Inherits System.Windows.Forms.UserControl


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SearchData))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CmdNew = New C1.Win.C1Input.C1Button()
        Me.cmdCancel = New C1.Win.C1Input.C1Button()
        Me.cmdSelect = New C1.Win.C1Input.C1Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSelect = New System.Windows.Forms.TextBox()
        Me.cboCtia = New System.Windows.Forms.ComboBox()
        Me.cboSList = New System.Windows.Forms.ComboBox()
        Me.DG = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.CmdNew)
        Me.GroupBox1.Controls.Add(Me.cmdCancel)
        Me.GroupBox1.Controls.Add(Me.cmdSelect)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSelect)
        Me.GroupBox1.Controls.Add(Me.cboCtia)
        Me.GroupBox1.Controls.Add(Me.cboSList)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 91)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "&Option"
        '
        'CmdNew
        '
        Me.CmdNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdNew.ForeColor = System.Drawing.Color.RoyalBlue
        Me.CmdNew.Location = New System.Drawing.Point(738, 49)
        Me.CmdNew.Name = "CmdNew"
        Me.CmdNew.Size = New System.Drawing.Size(51, 23)
        Me.CmdNew.TabIndex = 6
        Me.CmdNew.Text = "New"
        Me.CmdNew.UseVisualStyleBackColor = True
        Me.CmdNew.Visible = False
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Location = New System.Drawing.Point(661, 49)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdSelect
        '
        Me.cmdSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSelect.Location = New System.Drawing.Point(580, 49)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(75, 23)
        Me.cmdSelect.TabIndex = 3
        Me.cmdSelect.Text = "Select"
        Me.cmdSelect.UseVisualStyleBackColor = True
        Me.cmdSelect.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.cmdSelect.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(317, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Search Form"
        '
        'txtSelect
        '
        Me.txtSelect.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSelect.Font = New System.Drawing.Font("Zawgyi-One", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelect.Location = New System.Drawing.Point(388, 49)
        Me.txtSelect.Name = "txtSelect"
        Me.txtSelect.Size = New System.Drawing.Size(182, 25)
        Me.txtSelect.TabIndex = 2
        '
        'cboCtia
        '
        Me.cboCtia.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboCtia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCtia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboCtia.FormattingEnabled = True
        Me.cboCtia.Location = New System.Drawing.Point(230, 49)
        Me.cboCtia.Name = "cboCtia"
        Me.cboCtia.Size = New System.Drawing.Size(140, 21)
        Me.cboCtia.TabIndex = 1
        '
        'cboSList
        '
        Me.cboSList.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboSList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboSList.FormattingEnabled = True
        Me.cboSList.Location = New System.Drawing.Point(74, 50)
        Me.cboSList.Name = "cboSList"
        Me.cboSList.Size = New System.Drawing.Size(140, 21)
        Me.cboSList.TabIndex = 0
        '
        'DG
        '
        Me.DG.AllowColMove = False
        Me.DG.AllowUpdate = False
        Me.DG.AllowUpdateOnBlur = False
        Me.DG.CaptionHeight = 17
        Me.DG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG.Font = New System.Drawing.Font("Myanmar3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DG.Images.Add(CType(resources.GetObject("DG.Images"), System.Drawing.Image))
        Me.DG.Location = New System.Drawing.Point(0, 91)
        Me.DG.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.DG.Name = "DG"
        Me.DG.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.DG.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.DG.PreviewInfo.ZoomFactor = 75.0R
        Me.DG.PrintInfo.PageSettings = CType(resources.GetObject("DG.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.DG.RowHeight = 15
        Me.DG.Size = New System.Drawing.Size(792, 350)
        Me.DG.TabIndex = 5
        Me.DG.Text = "C1TrueDBGrid1"
        Me.DG.PropBag = resources.GetString("DG.PropBag")
        '
        'SearchData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DG)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "SearchData"
        Me.Size = New System.Drawing.Size(792, 441)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCancel As C1.Win.C1Input.C1Button
    Friend WithEvents cmdSelect As C1.Win.C1Input.C1Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSelect As System.Windows.Forms.TextBox
    Friend WithEvents cboCtia As System.Windows.Forms.ComboBox
    Friend WithEvents cboSList As System.Windows.Forms.ComboBox
    Private WithEvents DG As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents CmdNew As C1.Win.C1Input.C1Button
End Class
