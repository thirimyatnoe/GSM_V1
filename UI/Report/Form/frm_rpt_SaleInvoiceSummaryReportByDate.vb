Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports System.IO

Public Class frm_rpt_SaleInvoiceSummaryReportByDate
    Private ReportDA As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    ' Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
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
    Private Sub cboCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboCategory, e)
    End Sub

    Private Sub cboCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboCategory, "")
    End Sub

    Private Sub cboGoldQuality_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboGoldQuality, e)
    End Sub

    Private Sub cboGoldQuality_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboGoldQuality, "")
    End Sub
    'Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    AutoCompleteCombo_KeyUp(cboLocation, e)
    'End Sub

    'Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
    '    AutoCompleteCombo_Leave(cboLocation, "")
    'End Sub
#End Region

    Private Sub frm_rpt_SaleInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()
        cboGoldQuality.Enabled = False
        cboCategory.Enabled = False
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
        radAll.Checked = True

    End Sub
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim curRDLC As String = ""
        Dim title As String = ""

        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If ChkLocation.Checked Then
            If chkMostSaleItem.Checked Then
                dt = ReportDA.GetMostSaleItemData(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            ElseIf chkSalePercentage.Checked Then
                dt = ReportDA.GetForSalePercentageReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, "Asc", GetFilterString)
            Else
                dt = ReportDA.GetSaleSummaryReportByDateANDByItemName(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                If dt.Rows.Count > 0 Then
                    Dim _SaleInvoiceHeaderID As String = ""
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If _SaleInvoiceHeaderID <> dt.Rows(i).Item("SaleInvoiceHeaderID") Then



                            dt.Rows(i).Item("TotalNetAmount") = dt.Rows(i).Item("NetAmount")
                            dt.Rows(i).Item("TotalAddOrSub") = dt.Rows(i).Item("AddOrSub")
                        Else
                            dt.Rows(i).Item("TotalNetAmount") = 0
                            dt.Rows(i).Item("TotalAddOrSub") = 0
                        End If
                        _SaleInvoiceHeaderID = dt.Rows(i).Item("SaleInvoiceHeaderID")
                    Next
                End If
            End If
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If

        If chkMostSaleItem.Checked Then
            curRDLC = "UI.rpt_NewMostSaleItem.rdlc"
        ElseIf chkSalePercentage.Checked Then
            curRDLC = "UI.rpt_SalesPercentageReport.rdlc"
        Else
            If optByDate.Checked Then
                curRDLC = "UI.rpt_SaleInvoiceSummaryReportByDate.rdlc"
            ElseIf optByItemCategory.Checked Then
                curRDLC = "UI.rpt_SaleInvoiceSummaryReportByItemName.rdlc"
            ElseIf optByChart.Checked Then
                curRDLC = "UI.rpt_SaleReportByChart.rdlc"
            End If
        End If
        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = curRDLC
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))

        If chkMostSaleItem.Checked Or chkSalePercentage.Checked Then
            title = "နေ့စွဲ " + dtpFromDate.Value.ToString("dd-MM-yyyy") + " မှ " + dtpToDate.Value.ToString("dd-MM-yyyy") + " အထိ"
            Dim CTitle(0) As ReportParameter
            CTitle(0) = New ReportParameter("CTitle", title)
            rptViewer.LocalReport.SetParameters(CTitle)
        End If

        Dim TitleType As String = ""
        Dim Title1(0) As ReportParameter

        If radAll.Checked Then
            TitleType = "(All)"
        ElseIf radStock.Checked Then
            TitleType = "(ရွှေထည်)"
        ElseIf radDiamondStock.Checked Then
            TitleType = "(စိန်ထည်)"
        End If
        Title1(0) = New ReportParameter("Title1", TitleType)
        rptViewer.LocalReport.SetParameters(Title1)

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)
        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If radStock.Checked Then
            GetFilterString += " And F.IsDiamond=0"
        End If
        If radDiamondStock.Checked Then
            GetFilterString += " And F.IsDiamond=1"
        End If
        If (ChkLocation.Checked) Then
            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
    End Function

    Private Sub Get_Combos()
        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView

        cboCategory.DisplayMember = "ItemCategory_"
        cboCategory.ValueMember = "@ItemCategoryID"
        cboCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView

        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub
    'Private Sub chkGoldQuality_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkByChart.CheckedChanged
    '    If (chkByChart.Checked) Then
    '        cboGoldQuality.Enabled = True
    '    Else
    '        cboGoldQuality.Enabled = False
    '    End If
    'End Sub
    Private Sub chkItemCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCategory.CheckedChanged
        If (chkItemCategory.Checked) Then
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


    'Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If chkLocation.Checked Then
    '        cboLocation.Enabled = True
    '    Else
    '        cboLocation.Enabled = False
    '    End If
    'End Sub
    'Private Sub radDetail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If radDetail.Checked Then
    '        chkAddOrSub.Enabled = True
    '    Else
    '        chkAddOrSub.Enabled = False
    '        chkAddOrSub.Checked = False
    '    End If
    'End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("SaleInvoiceByDateReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub chkSalePercentage_CheckedChanged(sender As Object, e As EventArgs) Handles chkSalePercentage.CheckedChanged
        If chkSalePercentage.Checked Then
            grpByGroup.Enabled = False
            chkMostSaleItem.Checked = False
        Else
            If chkMostSaleItem.Checked Then
                grpByGroup.Enabled = False
            Else
                grpByGroup.Enabled = True
            End If
        End If
    End Sub

    Private Sub chkMostSaleItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostSaleItem.CheckedChanged
        If chkMostSaleItem.Checked Then
            grpByGroup.Enabled = False
            chkSalePercentage.Checked = False
        Else

            If chkSalePercentage.Checked Then
                grpByGroup.Enabled = False
            Else
                grpByGroup.Enabled = True
            End If
        End If
    End Sub
End Class