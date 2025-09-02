Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo

Public Class frm_rpt_Tax

    Private _CustomerID As String = ""

    Private ReportDA As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController

    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
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

    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub

#End Region

    Private Sub frm_rpt_SaleInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()
        cboGoldQuality.Enabled = False
        cboCategory.Enabled = False
        cboStaff.Enabled = False

        ChkItemName.Checked = False
        ChkItemName.Enabled = False
        cboItemName.Enabled = False
        radAll.Checked = True
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
        _CustomerID = "0"


    End Sub
    
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtDetail As New DataTable
        Dim SaleInvoiceDetailID As String = ""
        Dim SalesInvoiceGemItemID As String = ""
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If ChkLocation.Checked Then
            If rdoALL.Checked Then
                If radDetail.Checked Then
                    dt = ReportDA.GetTaxSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                ElseIf radSummary.Checked Then
                    dt = ReportDA.GetTaxSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                End If

            ElseIf rdoSale.Checked Then

                If radDetail.Checked Then
                    dt = ReportDA.GetSaleTaxSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                ElseIf radSummary.Checked Then
                    dt = ReportDA.GetSaleTaxSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                End If
            Else
                If radDetail.Checked Then
                    dt = ReportDA.GetOrderTaxSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                ElseIf radSummary.Checked Then
                    dt = ReportDA.GetOrderTaxSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                End If

            End If
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        rptViewer.Reset()
        ' rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_Tax_Report_Summary.rdlc", "UI.rpt_Tax_Report_Detail.rdlc")
        If rdoOrder.Checked Then
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_OrderTax_Report_Summary.rdlc", "UI.rpt_OrderTax_Report_Detail.rdlc")

        ElseIf rdoSale.Checked Then
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_SaleTax_Report_Summary.rdlc", "UI.rpt_SaleTax_Report_Detail.rdlc")
        Else
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_Tax_Report_Summary.rdlc", "UI.rpt_Tax_Report_Detail.rdlc")
        End If

        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_OrderInvoice", dt))
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
        If (ChkItemName.Checked) Then
            GetFilterString += " And F.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If

        If (chkStaff.Checked) Then
            GetFilterString += " And H.StaffID = '" & cboStaff.SelectedValue & "'"
        End If
        'If (chkCustomerName.Checked) Then
        '    If rdoSale.Checked Then
        '        GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
        '    Else
        '        GetFilterString += " And R.CustomerID  = '" & _CustomerID & "'"
        '    End If
        '    '   GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
        'End If


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

        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = objStaffController.GetStaffList().DefaultView

        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub
    Private Sub chkGoldQuality_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQuality.CheckedChanged
        If (chkGoldQuality.Checked) Then
            cboGoldQuality.Enabled = True
        Else
            cboGoldQuality.Enabled = False
        End If
    End Sub
    Private Sub chkStaff_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStaff.CheckedChanged
        If (chkStaff.Checked) Then
            cboStaff.Enabled = True
        Else
            cboStaff.Enabled = False
        End If
    End Sub

    Private Sub chkItemCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCategory.CheckedChanged
        If (chkItemCategory.Checked) Then
            cboCategory.Enabled = True
            ChkItemName.Enabled = True
        Else
            cboCategory.Enabled = False
            ChkItemName.Enabled = False
            ChkItemName.Checked = False
            cboItemName.Enabled = False
        End If
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("SaleInvoiceReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub ChkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles ChkItemName.CheckedChanged
        If (ChkItemName.Checked) Then
            cboItemName.Enabled = True
        Else
            cboItemName.Enabled = False
        End If
    End Sub
    Private Sub cboItemCat_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCategory.SelectedValueChanged
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
            cboItemName.DisplayMember = "ItemName_"
            cboItemName.ValueMember = "ItemNameID"
            cboItemName.Text = ""
            cboItemName.SelectedIndex = -1
        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub
End Class