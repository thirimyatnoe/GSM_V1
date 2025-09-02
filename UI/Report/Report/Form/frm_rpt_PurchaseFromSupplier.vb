Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo
Public Class frm_rpt_PurchaseFromSupplier
    Dim _SupplierID As String = ""

    Private objPurchaseController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private objSupplierController As Supplier.ISupplierController = Factory.Instance.CreateSupplierController
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
    End Sub
    Private Sub frm_rpt_PurchaseFromSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        ChkLocation.Checked = True
        CboLocation.Enabled = True
        GetCombo()
        CboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
    Private Sub GetCombo()
        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim PurchaseFromSupplierID As String = ""
        Dim title As String = ""
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If ChkLocation.Checked Then
            dt = objPurchaseController.GetPurchaseInvoiceFromSupplierReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                If Not IsDBNull(dt.Rows(i).Item("PurchaseFromSupplierID")) Then
                    If dt.Rows(i).Item("PurchaseFromSupplierID") <> PurchaseFromSupplierID Then
                        dt.Rows(i).Item("AllNetAmount") = dt.Rows(i).Item("NetAmount")
                        dt.Rows(i).Item("AllPaidAmount") = dt.Rows(i).Item("PaidAmount")
                    Else
                        dt.Rows(i).Item("AllNetAmount") = 0
                        dt.Rows(i).Item("AllPaidAmount") = 0
                    End If
                    PurchaseFromSupplierID = dt.Rows(i).Item("PurchaseFromSupplierID")
                End If
            Next
        End If
        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_PurchaseInvoiceWG_Summary.rdlc", "UI.rpt_PurchaseInvoiceWG_Detail.rdlc")
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseSupplier_PurchaseSupplier", dt))
        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If (_SupplierID <> "") Then
            GetFilterString += " And H.SupplierID = '" & _SupplierID & "'"
        End If
        If (ChkLocation.Checked) Then
            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
    End Function
    Private Sub btnSupplierSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSupplierSearch.Click
        Dim DataItem As DataRow
        Dim dtSupplier As New DataTable

        dtSupplier = objSupplierController.GetAllSupplierList()
        DataItem = DirectCast(SearchData.FindFast(dtSupplier, "Supplier List"), DataRow)

        If DataItem IsNot Nothing Then
            _SupplierID = DataItem.Item("@SupplierID").ToString()
            txtSupplierCode.Text = DataItem.Item("SupplierCode")
            txtSupplierName.Text = DataItem.Item("SupplierName_")
            txtSupplierAddress.Text = DataItem.Item("Address_")
        End If

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub txtSupplierCode_TextChanged(sender As Object, e As EventArgs) Handles txtSupplierCode.TextChanged
        Dim objSupplier As New SupplierInfo
        objSupplier = objSupplierController.GetSupplierDataByCode(txtSupplierCode.Text)
        txtSupplierName.Text = ""
        txtSupplierAddress.Text = ""
        _SupplierID = ""
        If Not IsNothing(objSupplier.SupplierName) Then
            ShowDataForSupplier(objSupplier)
        End If
    End Sub

    Private Sub ShowDataForSupplier(ByVal obj As CommonInfo.SupplierInfo)
        With obj
            txtSupplierCode.Text = .SupplierCode
            txtSupplierName.Text = .SupplierName
            txtSupplierAddress.Text = .SupplierAddress
            _SupplierID = .SupplierID
        End With

    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("PurchaseFromSupplier")
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