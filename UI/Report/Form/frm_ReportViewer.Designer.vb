<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ReportViewer
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ReportViewer))
        Me.rpt_SaleInvoice_Print1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.SuspendLayout()
        '
        'rpt_SaleInvoice_Print1
        '
        Me.rpt_SaleInvoice_Print1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rpt_SaleInvoice_Print1.Location = New System.Drawing.Point(0, 0)
        Me.rpt_SaleInvoice_Print1.Name = "rpt_SaleInvoice_Print1"
        Me.rpt_SaleInvoice_Print1.Size = New System.Drawing.Size(292, 266)
        Me.rpt_SaleInvoice_Print1.TabIndex = 0
        '
        'frm_ReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.rpt_SaleInvoice_Print1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frm_ReportViewer"
        Me.Text = "Report Viewer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rpt_SaleInvoice_Print1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
