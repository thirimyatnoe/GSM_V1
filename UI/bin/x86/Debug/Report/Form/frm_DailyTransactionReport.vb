Imports BusinessRule
Imports Microsoft.Reporting.WinForms

Public Class frm_DailyTransactionReport
    Dim dt As DataTable
    Private _GeneralLedgerByLocation As GeneralLedgerByLocation.IGeneralLedgerByLocationController = Factory.Instance.CreateGeneralLedgerByLocationController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController

    Private Sub frm_DailyTransactionReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()

        Me.CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
  
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub Get_Combos()
        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub

    Private Function GetFilterString() As String
        GetFilterString = ""

        If (optAll.Checked) Then
            GetFilterString += ""
        End If
        If (optBank.Checked) Then
            GetFilterString += " AND  IsBank = 1 "
        End If

        If (optCash.Checked) Then
            GetFilterString += " AND IsBank=0 "
        End If

        If (ChkLocation.Checked) Then
            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        End If

    End Function



    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim tmpdt As New DataTable
        Dim curRDLC As String = ""
        Dim dtOtherCash As New DataTable

        If Me.ChkLocation.Checked Then
            tmpdt = _GeneralLedgerByLocation.GetDailyTransactonByLocation(dtpFromDate.Value.Date, CboLocation.SelectedValue, GetFilterString)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        curRDLC = "UI.rpt_DailyTransaction.rdlc"
        If tmpdt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        dtOtherCash = _GeneralLedgerByLocation.GetAllRecordOtherCashDataByDate(dtpFromDate.Value.Date)
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = curRDLC
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsDailyTransaction_DailyTransaction", tmpdt))
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsOtherCash_dtOtherCash", dtOtherCash))
        Dim ShowOtherCash(0) As ReportParameter
        Dim IsShow As Boolean
        If dtOtherCash.Rows.Count > 0 Then
            IsShow = True
        Else
            IsShow = False
        End If
        ShowOtherCash(0) = New ReportParameter("ShowOtherCash", IsShow)
        rptViewer.LocalReport.SetParameters(ShowOtherCash)

        rptViewer.RefreshReport()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("DailyTransaction")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub

End Class