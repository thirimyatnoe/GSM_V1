<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CustomReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CustomReport))
        Me.chkCheckListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtReportName = New System.Windows.Forms.TextBox()
        Me.txtReportCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'chkCheckListBox
        '
        Me.chkCheckListBox.FormattingEnabled = True
        Me.chkCheckListBox.Items.AddRange(New Object() {"Customer", "GoldQuality", "ItemCategory", "ItemName", "GemsCategory", "Staff", "FromDate", "ToDate"})
        Me.chkCheckListBox.Location = New System.Drawing.Point(99, 39)
        Me.chkCheckListBox.Name = "chkCheckListBox"
        Me.chkCheckListBox.Size = New System.Drawing.Size(127, 139)
        Me.chkCheckListBox.TabIndex = 201
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 202
        Me.Label1.Text = "Report Name"
        '
        'txtReportName
        '
        Me.txtReportName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportName.Location = New System.Drawing.Point(99, 12)
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.Size = New System.Drawing.Size(235, 21)
        Me.txtReportName.TabIndex = 203
        '
        'txtReportCode
        '
        Me.txtReportCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportCode.Location = New System.Drawing.Point(99, 193)
        Me.txtReportCode.Multiline = True
        Me.txtReportCode.Name = "txtReportCode"
        Me.txtReportCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReportCode.Size = New System.Drawing.Size(891, 308)
        Me.txtReportCode.TabIndex = 205
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 193)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 204
        Me.Label2.Text = "Report Code"
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(340, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(30, 21)
        Me.btnSearch.TabIndex = 206
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'frm_CustomReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1009, 554)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtReportCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtReportName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkCheckListBox)
        Me.Name = "frm_CustomReport"
        Me.Text = "Custom Report"
        Me.Controls.SetChildIndex(Me.chkCheckListBox, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtReportName, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtReportCode, 0)
        Me.Controls.SetChildIndex(Me.btnSearch, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkCheckListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtReportName As System.Windows.Forms.TextBox
    Friend WithEvents txtReportCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button

End Class
