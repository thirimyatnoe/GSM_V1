Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports System.IO

Public Class frm_PurchaseSummaryReportByDate
    Private ReportDA As BusinessRule.PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
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
    Private Sub GetCombo()
        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub
#Region " ComboBox Events "
#End Region

    Private Sub frm_PurchaseSummaryReportByDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        optAll.Checked = True
        optByDate.Checked = True
        GetCombo()
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If ChkLocation.Checked Then
            dt = ReportDA.GetPurchaseInvoiceSummayReportByDate(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count > 0 Then
            Dim _PurchaseHeaderID As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                If _PurchaseHeaderID <> dt.Rows(i).Item("PurchaseHeaderID") Then
                    dt.Rows(i).Item("TotalNetAmount") = dt.Rows(i).Item("NetAmount")
                    dt.Rows(i).Item("TotalAddOrSub") = dt.Rows(i).Item("AllAddOrSub")
                Else
                    dt.Rows(i).Item("TotalNetAmount") = 0
                    dt.Rows(i).Item("TotalAddOrSub") = 0
                End If
                _PurchaseHeaderID = dt.Rows(i).Item("PurchaseHeaderID")
            Next
        End If
        rptViewer.Reset()

        If optByDate.Checked Then
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseSummaryReportByDate.rdlc"
        ElseIf optByItemCategory.Checked Then
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseSummaryReportByItemName.rdlc"
        End If
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))

        Dim TitleChange(0) As ReportParameter
        If optByDate.Checked Then
            TitleChange(0) = New ReportParameter("TitleChange", "နေ့စွဲအလိုက် အ၀ယ်၊အလဲစာရင်းချုပ်")
            rptViewer.LocalReport.SetParameters(TitleChange)
        ElseIf optByItemCategory.Checked Then
            TitleChange(0) = New ReportParameter("TitleChange", "ပစ္စည်းအမျိုးအစားအလိုက် အ၀ယ်၊အလဲစာရင်းချုပ်")
            rptViewer.LocalReport.SetParameters(TitleChange)
        End If

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)
        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If (radBarcode.Checked) Then
            GetFilterString += " And PD.ForSaleID <>'' AND P.IsGem=0"
        ElseIf radNoBarcode.Checked Then
            GetFilterString += " And PD.ForSaleID ='' AND P.IsGem=0"
        ElseIf optAll.Checked Then
            GetFilterString += " AND P.IsGem=0"
        End If
        If (ChkLocation.Checked) Then
            GetFilterString += " And P.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("PurchaseSummaryReportBYDate")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If chkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub
End Class