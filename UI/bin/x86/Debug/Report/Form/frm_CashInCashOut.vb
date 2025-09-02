Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule

Public Class frm_CashInCashOut
    Private _GeneralLedgerByLocation As GeneralLedgerByLocation.IGeneralLedgerByLocationController = Factory.Instance.CreateGeneralLedgerByLocationController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
#Region " Auto Completion ComboBox "
    Private Sub AutoCompleteCombo_KeyUp(ByVal cbo As ComboBox, ByVal e As KeyEventArgs)
        Dim sTypedText As String
        Dim iFoundIndex As Integer
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        'Allow select keys without Autocompleting

        Select Case e.KeyCode
            Case Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.Down
                Return
        End Select

        'Get the Typed Text and Find it in the list

        sTypedText = cbo.Text
        iFoundIndex = cbo.FindString(sTypedText)

        'If we found the Typed Text in the list then Autocomplete

        If iFoundIndex >= 0 Then

            'Get the Item from the list (Return Type depends if Datasource was bound 

            ' or List Created)

            oFoundItem = cbo.Items(iFoundIndex)

            'Use the ListControl.GetItemText to resolve the Name in case the Combo 

            ' was Data bound

            sFoundText = cbo.GetItemText(oFoundItem)

            'Append then found text to the typed text to preserve case

            sAppendText = sFoundText.Substring(sTypedText.Length)
            cbo.Text = sTypedText & sAppendText

            'Select the Appended Text

            If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
            If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length

        End If

    End Sub

    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox)
        Dim iFoundIndex As Integer

        iFoundIndex = cbo.FindStringExact(cbo.Text)

        cbo.SelectedIndex = iFoundIndex

    End Sub
    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox, ByVal e As String)
        'sya 2/10/2008
        Dim sTypedText As String = ""
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        Dim iFoundIndex As Integer
        iFoundIndex = cbo.FindStringExact(cbo.Text)

        If iFoundIndex = -1 Then
            sTypedText = cbo.Text
            iFoundIndex = cbo.FindString(sTypedText)

            If iFoundIndex = -1 Then
                If sTypedText.IndexOf(" ") < 0 Then
                    iFoundIndex = -1
                Else
                    sTypedText = sTypedText.Remove(sTypedText.IndexOf(" "))
                    iFoundIndex = cbo.FindString(sTypedText)
                End If
            End If

            If iFoundIndex >= 0 Then
                oFoundItem = cbo.Items(iFoundIndex)
                sFoundText = cbo.GetItemText(oFoundItem)
                sAppendText = sFoundText.Substring(sTypedText.Length)
                cbo.Text = sTypedText & sAppendText

                If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
                If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length
            End If
        Else
            cbo.SelectedIndex = iFoundIndex
        End If

        If iFoundIndex = -1 Then
            For i As Integer = 0 To cbo.Items.Count - 1
                oFoundItem = cbo.Items(i)
                sFoundText = cbo.GetItemText(oFoundItem)

                If sFoundText.IndexOf(" ") < 0 Then
                    iFoundIndex = -1
                Else
                    sFoundText = sFoundText.Remove(sFoundText.IndexOf(" "))
                    iFoundIndex = cbo.FindString(sFoundText)
                End If

                If sTypedText.Contains(sFoundText) = True Then
                    iFoundIndex = i
                    cbo.SelectedIndex = i
                    Exit For
                Else
                    cbo.Text = ""
                    cbo.SelectedIndex = -1
                End If
            Next
        End If
    End Sub
#End Region
#Region " ComboBox Events "
   
#End Region

    Private Sub frm_CashInCashOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()
        Me.CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
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

    End Function
    Private Sub Get_Combos()
        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtOtherCash As New DataTable
        Dim ds As DataSet
        Dim title As String = ""
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If
        If ChkLocation.Checked Then
            ds = _GeneralLedgerByLocation.GetAllCashInCashOutReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, CboLocation.SelectedValue)
        Else
            ds = _GeneralLedgerByLocation.GetAllCashInCashOutReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, "")
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count = 0 Then MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        If Not IsDBNull(ds) Then
            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                ds.Tables(0).Rows(i).Item("OutTitle") = ds.Tables(1).Rows(i).Item("Title")
                ds.Tables(0).Rows(i).Item("OutAmount") = ds.Tables(1).Rows(i).Item("Amount")
            Next
        End If

        title = dtpFromDate.Value.ToString("dd/MM/yyyy") + " မှ " + dtpToDate.Value.ToString("dd/MM/yyyy") + " အထိ ငွေအဝင်အထွက် စာရင်း"
        dtOtherCash = _GeneralLedgerByLocation.GetAllRecordOtherCashData(dtpFromDate.Value.Date, dtpToDate.Value.Date)
        Dim tmpdtHeader As DataTable
        tmpdtHeader = ds.Tables(0)
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_CashInCashOut.rdlc"
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsCashInCashOut", tmpdtHeader))
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

        Dim CTitle(0) As ReportParameter
        CTitle(0) = New ReportParameter("CTitle", title)
        rptViewer.LocalReport.SetParameters(CTitle)
        rptViewer.RefreshReport()
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("CashIn\CashOut")
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