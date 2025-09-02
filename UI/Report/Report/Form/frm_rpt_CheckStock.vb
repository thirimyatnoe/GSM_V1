Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Public Class frm_rpt_CheckStock

    Dim PhotoPath As String = ""
    Private _CheckStockController As CheckStock.ICheckStockController = Factory.Instance.CreateCheckStockController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController

    Private Sub frm_rpt_CheckStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetCombo()
        cboLocation.Enabled = False

        PhotoPath = Global_PhotoPath
        Me.RptViewer.RefreshReport()
    End Sub
    Public Sub GetCombo()
        cboLocation.DisplayMember = "Location"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub

    Private Function GetFilterString() As String
        GetFilterString = ""
            If (chkLocation.Checked And cboLocation.SelectedIndex <> -1) Then
            GetFilterString += "  and CS.LocationID = '" & cboLocation.SelectedValue & "' "
            End If

    End Function

  



    Private Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtM As New DataTable
        Dim dtE As New DataTable
        Dim curRDLC As String = ""
        dt = _CheckStockController.GetCheckStockReport(dtpDate.Value.Date, GetFilterString)
        'dtM = _CheckStockController.GetMCheckStockReport(dtpDate.Value.Date, GetFilterString)
        'dtE = _CheckStockController.GetECheckStockReport(dtpDate.Value.Date, GetFilterString)

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        curRDLC = "\Report\RDLC\rpt_CheckStock.rdlc"

        RptViewer.Reset()

        RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & curRDLC
        RptViewer.LocalReport.DataSources.Clear()
        RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsCheckStock_CheckStock", dt))

        ' RptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_CheckStock.rdlc"
        'RptViewer.LocalReport.ReportEmbeddedResource = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_CheckStock.rdl "
        'RptViewer.LocalReport.DataSources.Clear()
        'RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsCheckStock_CheckStock", dt))
        'RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsCheckStock_CheckStock", dtM))
        'RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsCheckStock_CheckStock", dtE))

        'RptViewer.LocalReport.EnableExternalImages = True
        'Dim P(0) As ReportParameter
        'PhotoPath = Global_PhotoPath & "\"
        'P(0) = New ReportParameter("P", PhotoPath)
        'RptViewer.LocalReport.SetParameters(P)
        RptViewer.RefreshReport()

    End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLocation.CheckedChanged
        If chkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ''Test CheckStock 

    'TT
End Class