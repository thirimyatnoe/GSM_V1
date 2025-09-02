Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Public Class frm_rpt_ReturnAdvance

    Private ReportDA As ReturnAdvance.IReturnAdvanceController = Factory.Instance.CreateReturnAdvanceController
    Private _GemCategory As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _CustomerID As String = ""
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
    Private Sub cboGemsCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboGemsCategory, e)
    End Sub

    Private Sub cboGemsCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboGemsCategory, "")
    End Sub

    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboLocation, e)
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboLocation, "")
    End Sub
#End Region

    Private Sub frm_rpt_ReturnAdvance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboGemsCategory.Enabled = False
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
        radAll.Checked = True
        GetCombo()
        _CustomerID = "0"
        chkCustomerName.Checked = False
        txtCustomerCode.Enabled = False
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtCustomer.Enabled = False
        txtAddress.Enabled = False
        btnCustomer.Enabled = False
        Me.rptReturnAdvance.RefreshReport()
    End Sub

    Private Sub btnPreview_Click_1(sender As Object, e As EventArgs) Handles btnPreview.Click

        Dim dt As New DataTable

        Dim optType As String = ""
        If (radAll.Checked) Then
            optType = "All"
        ElseIf (radUsed.Checked) Then
            optType = "Used"
        Else
            optType = "NotUsed"
        End If

        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If ChkLocation.Checked Then
            dt = ReportDA.GetReturnAdvanceItemForRpt(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, optType)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count() = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
        End If
        rptReturnAdvance.Reset()
        rptReturnAdvance.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_ReturnAdvance_Summary.rdlc", "UI.rpt_ReturnAdvance_Detail.rdlc")
        rptReturnAdvance.LocalReport.DataSources.Clear()
        rptReturnAdvance.LocalReport.DataSources.Add(New ReportDataSource("dsReturnAdvance_ReturnAdvance", dt))

        If radDetail.Checked Then
            Dim TotalAmount(0) As ReportParameter
            Dim NetAmount(0) As ReportParameter
            Dim PaidAmount(0) As ReportParameter
            Dim dtTotal As New DataTable
            Dim TotalAmt As Integer = 0
            Dim NetAmt As Integer = 0
            Dim PaidAmt As Integer = 0
            dtTotal = ReportDA.GetReturnAdvanceReportForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dtTotal.Rows.Count() > 0 Then
                For Each dr As DataRow In dtTotal.Rows
                    TotalAmt += dr.Item("TotalAmount")
                    NetAmt += dr.Item("NetAmount")
                Next
            Else
                TotalAmt = 0
                NetAmt = 0
                PaidAmt = 0
            End If
            TotalAmount(0) = New ReportParameter("TotalAmount", TotalAmt)
            rptReturnAdvance.LocalReport.SetParameters(TotalAmount)

            NetAmount(0) = New ReportParameter("NetAmount", NetAmt)
            rptReturnAdvance.LocalReport.SetParameters(NetAmount)

            PaidAmount(0) = New ReportParameter("PaidAmount", PaidAmt)
            rptReturnAdvance.LocalReport.SetParameters(PaidAmount)

            Dim IsSelect As Boolean = False
            Dim Selected(0) As ReportParameter
            If chkGemsCategory.Checked Then
                IsSelect = True
            End If
            Selected(0) = New ReportParameter("Selected", IsSelect)
            rptReturnAdvance.LocalReport.SetParameters(Selected)
        End If
        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptReturnAdvance.LocalReport.SetParameters(G_PToY)
        rptReturnAdvance.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""

        If chkCustomerName.Checked Then
            GetFilterString += " And Cu.CustomerID  = '" & _CustomerID & "'"
        End If
        If (ChkLocation.Checked) Then
            GetFilterString += " And S.LocationID = '" & CboLocation.SelectedValue & "'"
        End If

    End Function
    Private Sub GetCombo()
        CboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboGemsCategory.DisplayMember = "GemsCategory_"
        cboGemsCategory.ValueMember = "@GemsCategoryID"
        cboGemsCategory.DataSource = _GemCategory.GetAllGemsCategory
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub chkGemsCategory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGemsCategory.CheckedChanged
        cboGemsCategory.Enabled = chkGemsCategory.Checked
    End Sub

    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("SalesGemsReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub btnCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomer.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow

        dt = _CustomerController.GetAllCustomerAutoCompleteData()
        DataItem = DirectCast(SearchData.FindFast(dt, "Customer List"), DataRow)
        If DataItem IsNot Nothing Then
            If DataItem("$Inactive") = False Then
                _CustomerID = DataItem("@CustomerID")
                txtCustomerCode.Text = DataItem("CustomerCode")
                txtCustomer.Text = DataItem("CustomerName_")
                txtAddress.Text = DataItem("CustomerAddress_")
            Else
                MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
                Exit Sub
            End If
        End If
    End Sub

    Private Sub chkCustomerName_CheckedChanged(sender As Object, e As EventArgs) Handles chkCustomerName.CheckedChanged
        If (chkCustomerName.Checked) Then
            txtCustomer.Enabled = True
            txtAddress.Enabled = True
            txtCustomerCode.Enabled = True
            btnCustomer.Enabled = True
        Else
            txtCustomer.Enabled = False
            txtCustomerCode.Enabled = False
            txtAddress.Enabled = False
            btnCustomer.Enabled = False
            _CustomerID = ""
            txtCustomerCode.Text = ""
            txtCustomer.Text = ""
            txtAddress.Text = ""
        End If
    End Sub
End Class