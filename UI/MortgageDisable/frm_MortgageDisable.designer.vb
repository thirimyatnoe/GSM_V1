<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MortgageDisable
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MortgageDisable))
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtPeriod = New System.Windows.Forms.TextBox()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.lblLocationName = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.grdItem = New System.Windows.Forms.DataGridView()
        Me.btnClick = New System.Windows.Forms.PictureBox()
        Me.TreeViewSplitter = New System.Windows.Forms.Splitter()
        Me.grd = New System.Windows.Forms.DataGridView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnHelpbook = New System.Windows.Forms.Button()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClick, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.btnHelpbook)
        Me.Panel3.Controls.Add(Me.txtPeriod)
        Me.Panel3.Controls.Add(Me.lblPeriod)
        Me.Panel3.Controls.Add(Me.dtpDate)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.btnPreview)
        Me.Panel3.Controls.Add(Me.lblLocationName)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1043, 52)
        Me.Panel3.TabIndex = 4
        '
        'txtPeriod
        '
        Me.txtPeriod.BackColor = System.Drawing.Color.White
        Me.txtPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPeriod.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeriod.Location = New System.Drawing.Point(671, 12)
        Me.txtPeriod.Name = "txtPeriod"
        Me.txtPeriod.Size = New System.Drawing.Size(100, 21)
        Me.txtPeriod.TabIndex = 1370
        Me.txtPeriod.Text = "0"
        Me.txtPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriod.Font = New System.Drawing.Font("Myanmar3", 9.749999!)
        Me.lblPeriod.Location = New System.Drawing.Point(560, 14)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(98, 19)
        Me.lblPeriod.TabIndex = 1371
        Me.lblPeriod.Text = "အပေါင်ဆုံးကာလ"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(287, 17)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(100, 21)
        Me.dtpDate.TabIndex = 513
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Myanmar3", 9.25!)
        Me.Label10.Location = New System.Drawing.Point(192, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 19)
        Me.Label10.TabIndex = 514
        Me.Label10.Text = "အပေါင်ဆုံးနေ့စွဲ"
        '
        'btnPreview
        '
        Me.btnPreview.BackgroundImage = CType(resources.GetObject("btnPreview.BackgroundImage"), System.Drawing.Image)
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Myanmar3", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(402, 15)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(152, 31)
        Me.btnPreview.TabIndex = 509
        Me.btnPreview.Text = "အပေါင်ဆုံးစာရင်း"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = True
        Me.lblLocationName.BackColor = System.Drawing.Color.Transparent
        Me.lblLocationName.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.ForeColor = System.Drawing.Color.Blue
        Me.lblLocationName.Location = New System.Drawing.Point(8, 14)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(58, 23)
        Me.lblLocationName.TabIndex = 508
        Me.lblLocationName.Text = "Shop"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.grdItem)
        Me.Panel4.Controls.Add(Me.btnClick)
        Me.Panel4.Controls.Add(Me.TreeViewSplitter)
        Me.Panel4.Controls.Add(Me.grd)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 52)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1043, 468)
        Me.Panel4.TabIndex = 5
        '
        'grdItem
        '
        Me.grdItem.AllowUserToAddRows = False
        Me.grdItem.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.grdItem.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdItem.BackgroundColor = System.Drawing.Color.LightGray
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdItem.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdItem.Dock = System.Windows.Forms.DockStyle.Left
        Me.grdItem.GridColor = System.Drawing.Color.White
        Me.grdItem.Location = New System.Drawing.Point(402, 0)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.ReadOnly = True
        Me.grdItem.RowHeadersVisible = False
        Me.grdItem.RowHeadersWidth = 25
        Me.grdItem.Size = New System.Drawing.Size(489, 468)
        Me.grdItem.TabIndex = 31
        '
        'btnClick
        '
        Me.btnClick.Image = CType(resources.GetObject("btnClick.Image"), System.Drawing.Image)
        Me.btnClick.Location = New System.Drawing.Point(391, 185)
        Me.btnClick.Name = "btnClick"
        Me.btnClick.Size = New System.Drawing.Size(10, 58)
        Me.btnClick.TabIndex = 30
        Me.btnClick.TabStop = False
        '
        'TreeViewSplitter
        '
        Me.TreeViewSplitter.Location = New System.Drawing.Point(387, 0)
        Me.TreeViewSplitter.Name = "TreeViewSplitter"
        Me.TreeViewSplitter.Size = New System.Drawing.Size(15, 468)
        Me.TreeViewSplitter.TabIndex = 25
        Me.TreeViewSplitter.TabStop = False
        '
        'grd
        '
        Me.grd.AllowUserToAddRows = False
        Me.grd.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.grd.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grd.BackgroundColor = System.Drawing.Color.LightGray
        Me.grd.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grd.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Dock = System.Windows.Forms.DockStyle.Left
        Me.grd.GridColor = System.Drawing.Color.White
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.Name = "grd"
        Me.grd.RowHeadersVisible = False
        Me.grd.RowHeadersWidth = 25
        Me.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grd.Size = New System.Drawing.Size(387, 468)
        Me.grd.TabIndex = 24
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "arrow.jpg")
        Me.ImageList1.Images.SetKeyName(1, "arrow-over.jpg")
        '
        'btnHelpbook
        '
        Me.btnHelpbook.BackColor = System.Drawing.Color.White
        Me.btnHelpbook.BackgroundImage = CType(resources.GetObject("btnHelpbook.BackgroundImage"), System.Drawing.Image)
        Me.btnHelpbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHelpbook.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelpbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpbook.Location = New System.Drawing.Point(990, 4)
        Me.btnHelpbook.Name = "btnHelpbook"
        Me.btnHelpbook.Size = New System.Drawing.Size(32, 32)
        Me.btnHelpbook.TabIndex = 1378
        Me.btnHelpbook.UseVisualStyleBackColor = False
        '
        'frm_MortgageDisable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1043, 566)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "frm_MortgageDisable"
        Me.Text = "Mortgage Disable Redeem"
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.Panel4, 0)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClick, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblLocationName As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents TreeViewSplitter As System.Windows.Forms.Splitter
    Friend WithEvents btnClick As System.Windows.Forms.PictureBox
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPeriod As System.Windows.Forms.TextBox
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents btnHelpbook As System.Windows.Forms.Button

End Class
