<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PhotoPath
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_PhotoPath))
        Me.txtPhotoPath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblPhotoPathID = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtPhotoPath
        '
        Me.txtPhotoPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhotoPath.Location = New System.Drawing.Point(83, 70)
        Me.txtPhotoPath.Multiline = True
        Me.txtPhotoPath.Name = "txtPhotoPath"
        Me.txtPhotoPath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPhotoPath.Size = New System.Drawing.Size(313, 49)
        Me.txtPhotoPath.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(18, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Photo Path"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(4, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Photo Path ID"
        Me.Label2.Visible = False
        '
        'lblPhotoPathID
        '
        Me.lblPhotoPathID.BackColor = System.Drawing.Color.White
        Me.lblPhotoPathID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPhotoPathID.Location = New System.Drawing.Point(83, 50)
        Me.lblPhotoPathID.Name = "lblPhotoPathID"
        Me.lblPhotoPathID.Size = New System.Drawing.Size(70, 18)
        Me.lblPhotoPathID.TabIndex = 6
        Me.lblPhotoPathID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPhotoPathID.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(159, 47)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(30, 21)
        Me.btnSearch.TabIndex = 803
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnSearch.Visible = False
        '
        'frm_PhotoPath
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(412, 170)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPhotoPath)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblPhotoPathID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_PhotoPath"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Photo Path"
        Me.Controls.SetChildIndex(Me.lblPhotoPathID, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtPhotoPath, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnSearch, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPhotoPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPhotoPathID As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button

End Class
