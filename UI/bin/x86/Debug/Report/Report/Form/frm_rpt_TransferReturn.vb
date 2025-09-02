Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule

Public Class frm_rpt_TransferReturn
    Private ReportDA As TransferReturn.ITransferReturnController = Factory.Instance.CreateTransferReturnController
    Private _BranchController As Branch.IBranchController = Factory.Instance.CreateBranchController
    Private _Customer As Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _GemCategory As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private LDReportDA As TransferDiamondReturn.ITransferDiamondReturnController = Factory.Instance.CreateTransferDiamondReturnController
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
    ''Private Sub cboCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboCategory.KeyUp
    ''    AutoCompleteCombo_KeyUp(cboCategory, e)
    ''End Sub

    ''Private Sub cboCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.Leave
    ''    AutoCompleteCombo_Leave(cboCategory, "")
    ''End Sub

    Private Sub cboGoldQ_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQ.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQ, e)
    End Sub

    Private Sub cboGoldQ_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQ.Leave
        AutoCompleteCombo_Leave(cboGoldQ, "")
    End Sub
    Private Sub cboBranch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboBranch.KeyUp
        AutoCompleteCombo_KeyUp(cboBranch, e)
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBranch.Leave
        AutoCompleteCombo_Leave(cboBranch, "")
    End Sub
    ''Private Sub cboCustomer_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboCustomer.KeyUp
    ''    AutoCompleteCombo_KeyUp(cboCustomer, e)
    ''End Sub

    ''Private Sub cboCustomer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustomer.Leave
    ''    AutoCompleteCombo_Leave(cboCustomer, "")
    ''End Sub
#End Region

    Private Sub frm_rpt_BranchTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()
        cboBranch.Enabled = False
        ChkItemName.Checked = False
        cboItemName.Enabled = False
        cboGoldQ.Enabled = False
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        chkGemsCategory.Checked = False
        chkGemsCategory.Visible = False
        CboGemsCategory.Visible = False
        CboGemsCategory.Enabled = False
        CboLocation.SelectedValue = Global_CurrentLocationID
        chkItemCategory.Checked = False

        cboCategory.Enabled = False
        txtBarcodeNo.Text = ""

    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        If chkTRLooseDiamond.Checked Then
            dt = LDReportDA.GetBranchTransferReturnReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_TransferReturnDiamond_Summary.rdlc", "UI.rpt_TransferReturnDiamond_Detail.rdlc")
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsTransferReturn_TransferReturnData", dt))
        Else
            dt = ReportDA.GetBranchTransferReturnReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

            
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_TransferReturn_Summary.rdlc", "UI.rpt_TransferReturn_Detail.rdlc")
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsTransferReturn_TransferReturnData", dt))
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)
        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If (chkBranch.Checked) Then
            GetFilterString += " And H.CurrentLocationID = '" & cboBranch.SelectedValue & "'"
        End If
        If (chkItemCategory.Checked) Then
            GetFilterString += " And C.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
        End If
        If (ChkItemName.Checked) Then
            GetFilterString += " And N.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If
        If (txtBarcodeNo.Text <> "") Then
            GetFilterString += " And S.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'"
        End If
        If (chkGoldQly.Checked) Then
            GetFilterString += "And G.GoldQualityID='" & cboGoldQ.SelectedValue & "'"
        End If

        If (chkGemsCategory.Checked) Then
            GetFilterString += "And S.SDGemsCategoryID='" & CboGemsCategory.SelectedValue & "'"
        End If
        If (txtBarcodeNo.Text <> "") Then
            GetFilterString += "And S.ItemCode='" & txtBarcodeNo.Text & "'"
        End If
        'If (ChkLocation.Checked) Then
        '    GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        'End If
    End Function
    Private Sub Get_Combos()
        cboGoldQ.DisplayMember = "GoldQuality"
        cboGoldQ.ValueMember = "@GoldQualityID"
        cboGoldQ.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView

        'cboBranch.DisplayMember = "BranchName"
        'cboBranch.ValueMember = "BranchID"
        'cboBranch.DataSource = _BranchController.GetAllBranchList().DefaultView
        cboBranch.DisplayMember = "Location"
        cboBranch.ValueMember = "@LocationID"
        cboBranch.DataSource = _Location.GetAllLocation.DefaultView

        cboCategory.DisplayMember = "ItemCategory_"
        cboCategory.ValueMember = "@ItemCategoryID"
        cboCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView

        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        CboGemsCategory.DisplayMember = "GemsCategory_"
        CboGemsCategory.ValueMember = "@GemsCategoryID"
        CboGemsCategory.DataSource = _GemCategory.GetAllGemsCategory
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub chkBranch_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBranch.CheckedChanged
        If chkBranch.Checked Then
            cboBranch.Enabled = True
        Else
            cboBranch.Enabled = False
        End If
    End Sub

    Private Sub chkItemCategory_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemCategory.CheckedChanged
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

    Private Sub ChkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles ChkItemName.CheckedChanged
        If (ChkItemName.Checked) Then
            cboItemName.Enabled = True
        Else
            cboItemName.Enabled = False
        End If
    End Sub
    Private Sub chkGoldQly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQly.CheckedChanged
        If (chkGoldQly.Checked) Then
            cboGoldQ.Enabled = True
        Else
            cboGoldQ.Enabled = False
        End If
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategory.SelectedIndexChanged
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
    Private Sub chkTRLooseDiamond_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRLooseDiamond.CheckedChanged
        If chkTRLooseDiamond.Checked = True Then
            chkGemsCategory.Enabled = True
            chkGemsCategory.Visible = True
            CboGemsCategory.Visible = True

            cboCategory.Visible = False
            chkItemCategory.Visible = False

            cboCategory.Enabled = False
            chkItemCategory.Enabled = False
            ChkItemName.Enabled = False
            cboItemName.Enabled = False
            chkGoldQly.Enabled = False
            cboGoldQ.Enabled = False

        Else
            chkGemsCategory.Enabled = False
            chkGemsCategory.Visible = False
            CboGemsCategory.Visible = False

            cboCategory.Enabled = True
            chkItemCategory.Enabled = True
            'ChkItemName.Enabled = True
            'cboItemName.Enabled = True
            chkGoldQly.Enabled = True
            cboGoldQ.Enabled = True
            cboCategory.Visible = True
            chkItemCategory.Visible = True
        End If
    End Sub
    Private Sub chkGemsCategory_CheckedChanged(sender As Object, e As EventArgs) Handles chkGemsCategory.CheckedChanged
        If (chkGemsCategory.Checked) Then
            CboGemsCategory.Enabled = True
        Else
            CboGemsCategory.Enabled = False
        End If
    End Sub
    Private Sub cboGemsCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboGemsCategory, e)
    End Sub

    Private Sub cboGemsCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboGemsCategory, "")
    End Sub
End Class