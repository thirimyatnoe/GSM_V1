Imports Microsoft.Reporting.WinForms
Imports BusinessRule
Imports System.Configuration

Public Class frm_rpt_ProfitForVolumeSaleItem
    Private ReportDA As SalesVolume.ISalesVolumeController = Factory.Instance.CreateSalesVolumeController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController

    Private Sub frm_rpt_ProfitForSaleItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()

        cboGoldQuality.Enabled = False
        cboCategory.Enabled = False
        chkItemName.Enabled = False
        cboItemName.Enabled = False
        chkItemName.Checked = False
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
            dt = ReportDA.GetProfitForSaleVoulumeItem(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_ProfitforSaleVolume_Report_Summary.rdlc", "UI.rpt_ProfitForSaleVolumeItem.rdlc")
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSalesVolumeInvoice_SalesVolumeInvoice", dt))

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)
        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If (chkGoldQuality.Checked) Then
            GetFilterString += " And F.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
        End If
        If (chkItemCategory.Checked) Then
            GetFilterString += " And F.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
        End If

        If (chkItemName.Checked) Then
            GetFilterString += " And F.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If
        If ChkLocation.Checked Then
            GetFilterString += " And H.LocationID = '" & cboLocation.SelectedValue & "'"
        End If
    End Function
    Private Sub Get_Combos()
        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView

        cboCategory.DisplayMember = "ItemCategory_"
        cboCategory.ValueMember = "@ItemCategoryID"
        cboCategory.DataSource = _ItemCategoryController.GetAllItemCategory()

        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub
    Private Sub chkGoldQuality_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQuality.CheckedChanged
        If (chkGoldQuality.Checked) Then
            cboGoldQuality.Enabled = True
        Else
            cboGoldQuality.Enabled = False
        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If ChkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub
    Private Sub chkItemCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCategory.CheckedChanged
        If (chkItemCategory.Checked) Then
            cboCategory.Enabled = True
            chkItemName.Enabled = True
        Else
            cboCategory.Enabled = False
            chkItemName.Enabled = False
            chkItemName.Checked = False
            cboItemName.Enabled = False
        End If
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("ProfitforVolumeItemReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub chkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemName.CheckedChanged
        If (chkItemName.Checked) Then
            cboItemName.Enabled = True
        Else
            cboItemName.Enabled = False

        End If

    End Sub

   
    Private Sub cboCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategory.SelectedIndexChanged
        Dim itemid As String
        itemid = cboCategory.SelectedValue
        RefreshItemNameCbo(itemid)
    End Sub
    Private Sub RefreshItemNameCbo(ByVal ItemID As String)
        Dim dt As New DataTable
        dt = _ItemName.GetItemNameListByItemCategory(ItemID)
        If dt.Rows.Count > 0 Then
            cboItemName.DataSource = dt.DefaultView
            cboItemName.DisplayMember = "ItemName_"
            cboItemName.ValueMember = "ItemNameID"
            cboItemName.SelectedIndex = 0
        Else
            dt.Rows.Clear()
            cboItemName.DataSource = dt.DefaultView
            cboItemName.DisplayMember = "ItemName"
            cboItemName.ValueMember = "ItemNameID"
            cboItemName.Text = ""
            cboItemName.SelectedIndex = -1

        End If
    End Sub

End Class