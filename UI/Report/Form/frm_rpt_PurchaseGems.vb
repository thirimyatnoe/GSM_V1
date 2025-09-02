Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule

Public Class frm_rpt_PurchaseGems
    Dim dt As DataTable
    Private _PurchaseGemsController As PurchaseGems.IPurchaseGemsController = Factory.Instance.CreatePurchaseGemsController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _GemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
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
    Private Sub cboGemsCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGemsCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboGemsCategory, e)
    End Sub

    Private Sub cboGemsCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGemsCategory.Leave
        AutoCompleteCombo_Leave(cboGemsCategory, "")
    End Sub

    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboLocation.KeyUp
        AutoCompleteCombo_KeyUp(cboLocation, e)
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.Leave
        AutoCompleteCombo_Leave(cboLocation, "")
    End Sub
#End Region

    Private Sub frm_rpt_PurchaseGems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rpt_PurchaseGems.RefreshReport()
        GetCombo()
        cboLocation.Enabled = False
        cboGemsCategory.Enabled = False
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable

        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If
        dt = _PurchaseGemsController.GetPurchaseGemsReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        
        rpt_PurchaseGems.Reset()
        rpt_PurchaseGems.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_PurchaseGems_Summary.rdlc", "UI.rpt_PurchaseGems_Detailc.rdlc")
        rpt_PurchaseGems.LocalReport.DataSources.Clear()
        rpt_PurchaseGems.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseGems_dsPurchaseGems", dt))
        If radDetail.Checked = True Then
            Dim LocName(0) As ReportParameter
            If (chkLocation.Checked) Then
                LocName(0) = New ReportParameter("LocName", _Location.GetLocationByID(cboLocation.SelectedValue).Location)
                rpt_PurchaseGems.LocalReport.SetParameters(LocName)
            Else
                LocName(0) = New ReportParameter("LocName", "ALL")
                rpt_PurchaseGems.LocalReport.SetParameters(LocName)
            End If
        End If
        rpt_PurchaseGems.RefreshReport()
    End Sub

    Private Function GetFilterString() As String
        GetFilterString = ""
        If (chkLocation.Checked) Then
            GetFilterString += " And H.LocationID = '" & cboLocation.SelectedValue & "'"
        End If
        If (chkGemsCategory.Checked) Then
            GetFilterString += " And I.GemsCategoryID = '" & cboGemsCategory.SelectedValue & "' "
        End If

    End Function
    Private Sub GetCombo()
        cboLocation.DisplayMember = "Location"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboGemsCategory.DisplayMember = "GemsCategory_"
        cboGemsCategory.ValueMember = "@GemsCategoryID"
        cboGemsCategory.DataSource = _GemsCategoryController.GetAllGemsCategory
    End Sub
    Private Sub chkGemsCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGemsCategory.CheckedChanged
        cboGemsCategory.Enabled = chkGemsCategory.Checked
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLocation.CheckedChanged
        cboLocation.Enabled = chkLocation.Checked
    End Sub


End Class