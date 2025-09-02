Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Public Class frm_rpt_BarcodeTrack
    Dim cristr As String = ""
    Dim bolret As Boolean
    Private ReportDA As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    'Private DiaStockDA As StockDiamond.IStockDiamondController = Factory.Instance.CreateStockDiamondController
    Private m_ChildFormNumber As Integer = 0

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        If txtBarcodeNo.Text <> "" Then
            dt = ReportDA.GetBarcodeTrack(txtBarcodeNo.Text)
            If dt.Rows.Count() = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
            End If
            rptBarcodeTrack.Reset()
            rptBarcodeTrack.LocalReport.ReportEmbeddedResource = "UI.rpt_BarcodeTrack.rdlc"
            rptBarcodeTrack.LocalReport.DataSources.Clear()
            rptBarcodeTrack.LocalReport.DataSources.Add(New ReportDataSource("BarcodeTrack_BarcodeTrack", dt))

            Dim BarcodeType(0) As ReportParameter
            BarcodeType(0) = New ReportParameter("BarcodeType", "Diamond")
            rptBarcodeTrack.LocalReport.SetParameters(BarcodeType)

            rptBarcodeTrack.RefreshReport()
        Else
            MsgBox("Please Entry BarcodeNo ! ", MsgBoxStyle.Information, Me.Text)
            txtBarcodeNo.Focus()
        End If
        'rptBarcodeTrack.RefreshReport()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub frm_rpt_BarcodeTrack_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub btnCheckStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Openform(UI.frm_RFIDDataChecking, FormWindowState.Maximized)
        'Openform(UI.frm_StockChecking, FormWindowState.Maximized)
    End Sub

End Class