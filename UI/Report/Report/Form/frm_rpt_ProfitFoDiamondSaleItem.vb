Imports Microsoft.Reporting.WinForms
Imports BusinessRule
Imports System.Configuration

Public Class frm_rpt_ProfitForDiamondeSaleItem
    Private ReportDA As SaleLooseDiamond.ISaleLooseDiamondController = Factory.Instance.CreateSaleLooseDiamondController
    Private _GemCategory As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController

    Private Sub frm_rpt_ProfitForSaleItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()

        chkGemsCategory.Checked = False
        cboCategory.Enabled = False
      
        cboLocation.Enabled = True
        ChkLocation.Checked = True
        Get_Combos()
        cboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim curRDLC As String = ""
        Dim Title(0) As ReportParameter
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If
        If ChkLocation.Checked Then
            dt = ReportDA.GetProfitForSaleDiamondItem(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_ProfitforSaleDiamond_Report_Summary.rdlc", "UI.rpt_ProfitForSaleDiamondItem.rdlc")
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleLooseDiamond_SaleLooseDiamond", dt))

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)
        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If (chkGemsCategory.Checked) Then
            GetFilterString += " And F.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
        End If

        If ChkLocation.Checked Then
            GetFilterString += " And H.LocationID = '" & cboLocation.SelectedValue & "'"
        End If
    End Function
    Private Sub Get_Combos()

        cboCategory.DisplayMember = "GemsCategory_"
        cboCategory.ValueMember = "@GemsCategoryID"
        cboCategory.DataSource = _GemCategory.GetAllGemsCategory

        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If ChkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub
    Private Sub chkItemCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGemsCategory.CheckedChanged
        If (chkGemsCategory.Checked) Then
            cboCategory.Enabled = True
        Else
            cboCategory.Enabled = False
        End If
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("ProfitforDiamondItemReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class