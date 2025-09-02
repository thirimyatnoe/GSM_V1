Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule

Public Class frm_rpt_SaleInvoiceLooseDiamond
    Private ReportDA As SaleLooseDiamond.ISaleLooseDiamondController = Factory.Instance.CreateSaleLooseDiamondController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _GemCategory As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
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
    Private Sub cboCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboCategory, e)
    End Sub

    Private Sub cboCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.Leave
        AutoCompleteCombo_Leave(cboCategory, "")
    End Sub

    'Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    AutoCompleteCombo_KeyUp(cboLocation, e)
    'End Sub

    'Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
    '    AutoCompleteCombo_Leave(cboLocation, "")
    'End Sub
#End Region

    Private Sub frm_rpt_SaleInvoiceLooseDiamond_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()
        CboLocation.Enabled = False
        cboCategory.Enabled = False
        ChkLocation.Checked = True
        CboLocation.Enabled = True
        CboLocation.SelectedValue = Global_CurrentLocationID
        cboStaff.Enabled = False
        txtBarcodeNo.Text = ""
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If ChkLocation.Checked Then
            dt = ReportDA.GetSalesInvoiceLooseDiamondReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count > 0 Then
            Dim _SaleLooseDiamondID As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                If _SaleLooseDiamondID <> dt.Rows(i).Item("SaleLooseDiamondID") Then
                    dt.Rows(i).Item("TotalNetAmount") = dt.Rows(i).Item("NetAmount")
                Else
                    dt.Rows(i).Item("TotalNetAmount") = 0
                End If
                _SaleLooseDiamondID = dt.Rows(i).Item("SaleLooseDiamondID")
            Next
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_SaleLooseDiamondByDate_Summary.rdlc", "UI.rpt_SaleLooseDiamondByVoucher_Detail.rdlc")
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleLooseDiamond_SaleLooseDiamond", dt))

        If radDetail.Checked Then
            Dim TotalAmount(0) As ReportParameter
            Dim NetAmount(0) As ReportParameter
            Dim PaidAmount(0) As ReportParameter
            Dim BalanceAmount(0) As ReportParameter

            Dim dtTotal As New DataTable
            Dim TotalAmt As Integer = 0
            Dim NetAmt As Integer = 0
            Dim PaidAmt As Integer = 0
            Dim BalanceAmt As Integer = 0
            If dt.Rows.Count > 0 Then
                dtTotal = ReportDA.GetSaleLooseDiamondReportForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                For Each dr As DataRow In dtTotal.Rows
                    TotalAmt += dr.Item("TotalAmount")
                    NetAmt += dr.Item("NetAmount")
                    PaidAmt += dr.Item("PaidAmount")
                    BalanceAmt += dr.Item("BalanceAmount")
                Next
            Else
                TotalAmt = 0
                NetAmt = 0
                PaidAmt = 0
                BalanceAmt = 0
            End If
            TotalAmount(0) = New ReportParameter("TotalAmount", TotalAmt)
            rptViewer.LocalReport.SetParameters(TotalAmount)

            NetAmount(0) = New ReportParameter("NetAmount", NetAmt)
            rptViewer.LocalReport.SetParameters(NetAmount)

            PaidAmount(0) = New ReportParameter("PaidAmount", PaidAmt)
            rptViewer.LocalReport.SetParameters(PaidAmount)

            BalanceAmount(0) = New ReportParameter("BalanceAmount", BalanceAmt)
            rptViewer.LocalReport.SetParameters(BalanceAmount)

            Dim IsSelect As Boolean = False
            Dim Selected(0) As ReportParameter
            If chkStaff.Checked Or chkGemsCategory.Checked Or txtBarcodeNo.Text <> "" Then
                IsSelect = True
            End If
            Selected(0) = New ReportParameter("Selected", IsSelect)
            rptViewer.LocalReport.SetParameters(Selected)
        End If

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)

        Dim G_GToCarat(0) As ReportParameter
        G_GToCarat(0) = New ReportParameter("G_GToCarat", Global_GramToKarat)
        rptViewer.LocalReport.SetParameters(G_GToCarat)

        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
       
        If (chkGemsCategory.Checked) Then
            GetFilterString += " And F.SDGemsCategoryID = '" & cboCategory.SelectedValue & "'"
        End If
        If (chkStaff.Checked) Then
            GetFilterString += " And H.StaffID = '" & cboStaff.SelectedValue & "'"
        End If
        If txtBarcodeNo.Text <> "" Then
            GetFilterString += " And D.ItemCode = '" & txtBarcodeNo.Text.Trim() & "'"
        End If
        If (ChkLocation.Checked) Then
            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
    End Function
    Private Sub Get_Combos()

        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView


        'cboCategory.DisplayMember = "ItemCategory_"
        'cboCategory.ValueMember = "@ItemCategoryID"
        'cboCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView

        cboCategory.DisplayMember = "GemsCategory_"
        cboCategory.ValueMember = "@GemsCategoryID"
        cboCategory.DataSource = _GemCategory.GetAllGemsCategory

        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = objStaffController.GetStaffList().DefaultView
    End Sub
    Private Sub chkStaff_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStaff.CheckedChanged
        If (chkStaff.Checked) Then
            cboStaff.Enabled = True
        Else
            cboStaff.Enabled = False
        End If
    End Sub

    Private Sub chkItemCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGemsCategory.CheckedChanged
        If (chkGemsCategory.Checked) Then
            cboCategory.Enabled = True
        Else
            cboCategory.Enabled = False

        End If

    End Sub
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("SaleVolumeReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class