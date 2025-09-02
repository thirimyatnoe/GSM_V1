Imports Microsoft.Reporting.WinForms
Imports BusinessRule
Public Class frm_rpt_DailyExpenseIncome
    Dim dt As New DataTable
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _DailyIncome As DailyIncome.IDailyIncomeController = Factory.Instance.CreateDailyIncomeController

    '#Region " Auto Completion ComboBox "
    '    Private Sub AutoCompleteCombo_KeyUp(ByVal cbo As ComboBox, ByVal e As KeyEventArgs)
    '        Dim sTypedText As String
    '        Dim iFoundIndex As Integer
    '        Dim oFoundItem As Object
    '        Dim sFoundText As String
    '        Dim sAppendText As String

    '        'Allow select keys without Autocompleting

    '        Select Case e.KeyCode
    '            Case Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.Down
    '                Return
    '        End Select

    '        'Get the Typed Text and Find it in the list

    '        sTypedText = cbo.Text
    '        iFoundIndex = cbo.FindString(sTypedText)

    '        'If we found the Typed Text in the list then Autocomplete

    '        If iFoundIndex >= 0 Then

    '            'Get the Item from the list (Return Type depends if Datasource was bound 

    '            ' or List Created)

    '            oFoundItem = cbo.Items(iFoundIndex)

    '            'Use the ListControl.GetItemText to resolve the Name in case the Combo 

    '            ' was Data bound

    '            sFoundText = cbo.GetItemText(oFoundItem)

    '            'Append then found text to the typed text to preserve case

    '            sAppendText = sFoundText.Substring(sTypedText.Length)
    '            cbo.Text = sTypedText & sAppendText

    '            'Select the Appended Text

    '            If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
    '            If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length

    '        End If

    '    End Sub

    '    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox)
    '        Dim iFoundIndex As Integer

    '        iFoundIndex = cbo.FindStringExact(cbo.Text)

    '        cbo.SelectedIndex = iFoundIndex

    '    End Sub
    '    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox, ByVal e As String)
    '        'sya 2/10/2008
    '        Dim sTypedText As String = ""
    '        Dim oFoundItem As Object
    '        Dim sFoundText As String
    '        Dim sAppendText As String

    '        Dim iFoundIndex As Integer
    '        iFoundIndex = cbo.FindStringExact(cbo.Text)

    '        If iFoundIndex = -1 Then
    '            sTypedText = cbo.Text
    '            iFoundIndex = cbo.FindString(sTypedText)

    '            If iFoundIndex = -1 Then
    '                If sTypedText.IndexOf(" ") < 0 Then
    '                    iFoundIndex = -1
    '                Else
    '                    sTypedText = sTypedText.Remove(sTypedText.IndexOf(" "))
    '                    iFoundIndex = cbo.FindString(sTypedText)
    '                End If
    '            End If

    '            If iFoundIndex >= 0 Then
    '                oFoundItem = cbo.Items(iFoundIndex)
    '                sFoundText = cbo.GetItemText(oFoundItem)
    '                sAppendText = sFoundText.Substring(sTypedText.Length)
    '                cbo.Text = sTypedText & sAppendText

    '                If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
    '                If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length
    '            End If
    '        Else
    '            cbo.SelectedIndex = iFoundIndex
    '        End If

    '        If iFoundIndex = -1 Then
    '            For i As Integer = 0 To cbo.Items.Count - 1
    '                oFoundItem = cbo.Items(i)
    '                sFoundText = cbo.GetItemText(oFoundItem)

    '                If sFoundText.IndexOf(" ") < 0 Then
    '                    iFoundIndex = -1
    '                Else
    '                    sFoundText = sFoundText.Remove(sFoundText.IndexOf(" "))
    '                    iFoundIndex = cbo.FindString(sFoundText)
    '                End If

    '                If sTypedText.Contains(sFoundText) = True Then
    '                    iFoundIndex = i
    '                    cbo.SelectedIndex = i
    '                    Exit For
    '                Else
    '                    cbo.Text = ""
    '                    cbo.SelectedIndex = -1
    '                End If
    '            Next
    '        End If
    '    End Sub
    '#End Region

    '#Region " ComboBox Events "
    '    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboLocation.KeyUp
    '        AutoCompleteCombo_KeyUp(cboLocation, e)
    '    End Sub

    '    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.Leave
    '        AutoCompleteCombo_Leave(cboLocation, "")
    '    End Sub
    '#End Region
    'Private Function GetFilterString() As String
    '    GetFilterString = ""

    '    'If (chkLocation.Checked) Then
    '    '    GetFilterString += " AND H.LocationID = '" & cboLocation.SelectedValue & "'"
    '    'End If
    'End Function

    'Private Sub GetCombo()
    '    cboLocation.DisplayMember = "Location"
    '    cboLocation.ValueMember = "@LocationID"
    '    cboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    'End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim Cristr As String = ""
        Dim CriStr1 As String = ""

        If optExpense.Checked Then
            Cristr = "ByExpense"
        Else
            Cristr = "ByIncome"
        End If
        If optBank.Checked Then
            CriStr1 = " AND IsBank= 1 "
        ElseIf optCash.Checked Then
            CriStr1 = " AND IsBank=0 "
        Else
            CriStr1 = ""
        End If
        If (chkLocation.Checked) Then
            CriStr1 += " And H.LocationID = '" & cboLocation.SelectedValue & "'"
        End If

        If chkLocation.Checked Then
            dt = _DailyIncome.GetDailyIncomeExpenseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, CriStr1, Cristr)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count() = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_DailyIncomeExpenseReport_Summary.rdlc", "UI.rpt_DailyIncomeExpenseReport.rdlc")
        'rptViewer.LocalReport.ReportPath = IIf(radSummary.Checked, My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_DailyIncomeExpense_Summary.rdl", My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_DailyIncomeExpense.rdl")
        'rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_DailyIncomeExpense_Summary.rdl", "UI.rpt_DailyIncomeExpense.rdl")
        'rptViewer.LocalReport.ReportPath = IIf(radSummary.Checked, My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_DailyIncomeExpense_Summary.rdl", My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_DailyIncomeExpense.rdl")
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsDailyIncomeExpense_dtDailyIncomeExpense", dt))

        Dim Title(0) As ReportParameter
        If radDetail.Checked Then
            If optExpense.Checked Then
                Title(0) = New ReportParameter("Title", "နေ့စဉ်အသုံးစရိတ် စာရင်း")
                rptViewer.LocalReport.SetParameters(Title)
            Else
                Title(0) = New ReportParameter("Title", "နေ့စဉ်အခြားဝင်ငွေ စာရင်း")
                rptViewer.LocalReport.SetParameters(Title)
            End If
        Else
            If optExpense.Checked Then
                Title(0) = New ReportParameter("Title", "နေ့စဉ်အသုံးစရိတ် စာရင်းချုပ်")
                rptViewer.LocalReport.SetParameters(Title)
            Else
                Title(0) = New ReportParameter("Title", "နေ့စဉ်အခြားဝင်ငွေ စာရင်းချုပ်")
                rptViewer.LocalReport.SetParameters(Title)
            End If
        End If

        rptViewer.RefreshReport()
    End Sub

    Private Sub frm_rpt_DailyExpenseIncome_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.rptViewer.RefreshReport()
        GetCombo()
        cboLocation.Enabled = True
        chkLocation.Checked = True
        cboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
    Private Sub GetCombo()
        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub

    'Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLocation.CheckedChanged
    '    cboLocation.Enabled = chkLocation.Checked
    'End Sub
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

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("DailyExpenseIncomeReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class