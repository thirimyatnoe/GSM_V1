<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchControlHost
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SearchControlHost))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ChkSelectAll = New System.Windows.Forms.CheckBox()
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
        Me.GroupBox1.Controls.Add(Me.ChkSelectAll)
        Me.GroupBox1.Controls.Add(Me.cmdCancel)
        Me.GroupBox1.Controls.Add(Me.cmdSelect)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSelect)
        Me.GroupBox1.Controls.Add(Me.cboCtia)
        Me.GroupBox1.Controls.Add(Me.cboSList)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(612, 91)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "&Option"
        '
        'ChkSelectAll
        '
        Me.ChkSelectAll.AutoSize = True
        Me.ChkSelectAll.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ChkSelectAll.Location = New System.Drawing.Point(374, 54)
        Me.ChkSelectAll.Name = "ChkSelectAll"
        Me.ChkSelectAll.Size = New System.Drawing.Size(70, 17)
        Me.ChkSelectAll.TabIndex = 8
        Me.ChkSelectAll.Text = "Select All"
        Me.ChkSelectAll.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(531, 50)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdSelect
        '
        Me.cmdSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSelect.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelect.Location = New System.Drawing.Point(450, 50)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(75, 23)
        Me.cmdSelect.TabIndex = 2
        Me.cmdSelect.Text = "Select"
        Me.cmdSelect.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(227, 16)
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
        Me.txtSelect.Location = New System.Drawing.Point(244, 53)
        Me.txtSelect.Name = "txtSelect"
        Me.txtSelect.Size = New System.Drawing.Size(126, 25)
        Me.txtSelect.TabIndex = 4
        '
        'cboCtia
        '
        Me.cboCtia.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboCtia.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboCtia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCtia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboCtia.FormattingEnabled = True
        Me.cboCtia.Location = New System.Drawing.Point(121, 53)
        Me.cboCtia.Name = "cboCtia"
        Me.cboCtia.Size = New System.Drawing.Size(104, 21)
        Me.cboCtia.TabIndex = 3
        '
        'cboSList
        '
        Me.cboSList.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboSList.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboSList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboSList.FormattingEnabled = True
        Me.cboSList.Location = New System.Drawing.Point(17, 52)
        Me.cboSList.Name = "cboSList"
        Me.cboSList.Size = New System.Drawing.Size(98, 21)
        Me.cboSList.TabIndex = 2
        '
        'DG
        '
        Me.DG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG.Images.Add(CType(resources.GetObject("DG.Images"), System.Drawing.Image))
        Me.DG.Location = New System.Drawing.Point(0, 91)
        Me.DG.Name = "DG"
        Me.DG.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.DG.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.DG.PreviewInfo.ZoomFactor = 75.0R
        Me.DG.PrintInfo.PageSettings = CType(resources.GetObject("DG.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.DG.Size = New System.Drawing.Size(612, 350)
        Me.DG.TabIndex = 9
        Me.DG.Text = "C1TrueDBGrid1"
        Me.DG.PropBag = resources.GetString("DG.PropBag")
        '
        'SearchControlHost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.DG)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "SearchControlHost"
        Me.Size = New System.Drawing.Size(612, 441)
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
    Friend WithEvents ChkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents DG As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
