Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo

Public Class frm_rpt_RepairReport
    Private _CustomerID As String = ""
    Private _RepairReceiveController As Repair.IRepairController = Factory.Instance.CreateRepairController
    Private _RepairReturnController As RepairReturn.IRepairReturnController = Factory.Instance.CreateRepairReturnController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCatController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _ItemNameController As ItemName.IItemNameController = Factory.Instance.CreateItemName
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
    Private Sub cboCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboCategory, e)
    End Sub

    Private Sub cboCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.Leave
        AutoCompleteCombo_Leave(cboCategory, "")
    End Sub

    Private Sub cboGoldQuality_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQuality.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQuality, e)
    End Sub

    Private Sub cboGoldQuality_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQuality.Leave
        AutoCompleteCombo_Leave(cboGoldQuality, "")
    End Sub

    'Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboLocation.KeyUp
    '    AutoCompleteCombo_KeyUp(cboLocation, e)
    'End Sub

    'Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.Leave
    '    AutoCompleteCombo_Leave(cboLocation, "")
    'End Sub

#End Region

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim dt As New DataTable
        Dim dtDetail As New DataTable
        Dim ReturnRepairDetailID As String = ""
        Dim ReturnRepairGemID As String = ""
        Dim RepairID As String = ""

        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If ChkLocation.Checked Then
            If optRepairReceive.Checked = True Then
                dt = _RepairReceiveController.GetRepairReceiveSummary(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i).Item("RepairID") <> RepairID Then
                            dt.Rows(i).Item("TotalAdvanceAmount") = dt.Rows(i).Item("AdvanceRepairAmount")
                        Else
                            dt.Rows(i).Item("TotalAdvanceAmount") = 0
                        End If
                        RepairID = dt.Rows(i).Item("RepairID")
                    Next
                End If
            Else
                If radSummary.Checked Then
                    dt = _RepairReturnController.GetRepairReturnSummary(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                Else
                    dt = _RepairReturnController.GetReturnRepairDetail(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If Not IsDBNull(dt.Rows(i).Item("ReturnRepairGemID")) Then

                                If dt.Rows(i).Item("ReturnRepairDetailID") <> ReturnRepairDetailID Then
                                    dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + dt.Rows(i).Item("ReturnNetAmount")
                                Else
                                    dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + 0
                                End If

                                ReturnRepairGemID = dt.Rows(i).Item("ReturnRepairGemID")
                                dtDetail = _RepairReturnController.GetRepairReturnDetailByRepairReturnDetailGem(ReturnRepairGemID)
                                For j As Integer = 0 To dtDetail.Rows.Count - 1
                                    ReturnRepairDetailID = dtDetail.Rows(j).Item("ReturnRepairDetailID")
                                Next

                            Else
                                dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("ReturnNetAmount") + 0
                            End If

                        Next
                    End If
                End If
            End If
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count() = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
        End If

        rpt_Repair.Reset()
        If optRepairReceive.Checked Then
            rpt_Repair.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_RepairReceiveReport_Summary.rdlc", "UI.rpt_RepairReceiveReport_Detail.rdlc")
            rpt_Repair.LocalReport.DataSources.Clear()
            rpt_Repair.LocalReport.DataSources.Add(New ReportDataSource("dsRepairReceive_RepairVoucher", dt))
        Else
            rpt_Repair.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_RepairReturnSummary.rdlc", "UI.rpt_RepairReturnDetail.rdlc")
            rpt_Repair.LocalReport.DataSources.Clear()
            rpt_Repair.LocalReport.DataSources.Add(New ReportDataSource("dsRepairReturn_RepairReturn", dt))
        End If
        If radDetail.Checked Then
            If optReturn.Checked Then
                Dim ItemTG(0) As ReportParameter
                Dim ItemTK(0) As ReportParameter
                Dim ReturnItemTK(0) As ReportParameter
                Dim ReturnItemTG(0) As ReportParameter
                Dim OrgGoldTG(0) As ReportParameter
                Dim OrgGoldTK(0) As ReportParameter
                Dim ReturnGoldTG(0) As ReportParameter
                Dim ReturnGoldTK(0) As ReportParameter
                Dim WasteTG(0) As ReportParameter
                Dim WasteTK(0) As ReportParameter
                Dim OrgGemsTK(0) As ReportParameter
                Dim OrgGemsTG(0) As ReportParameter
                Dim ReturnGemsTK(0) As ReportParameter
                Dim ReturnGemsTG(0) As ReportParameter

                Dim ItemAmount(0) As ReportParameter
                Dim NetAmount(0) As ReportParameter
                Dim PaidAmount(0) As ReportParameter
                Dim BalanceAmount(0) As ReportParameter
                Dim Quantity(0) As ReportParameter
                Dim objDetial As New CommonInfo.RepairReturnDetailInfo

                objDetial = _RepairReturnController.GetRepairReturnInvoiceDetailForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                ItemTG(0) = New ReportParameter("ItemTG", objDetial.ItemTG)
                rpt_Repair.LocalReport.SetParameters(ItemTG)

                ItemTK(0) = New ReportParameter("ItemTK", objDetial.ItemTK)
                rpt_Repair.LocalReport.SetParameters(ItemTK)

                ReturnItemTG(0) = New ReportParameter("ReturnItemTG", objDetial.ReturnItemTG)
                rpt_Repair.LocalReport.SetParameters(ReturnItemTG)

                ReturnItemTK(0) = New ReportParameter("ReturnItemTK", objDetial.ReturnItemTK)
                rpt_Repair.LocalReport.SetParameters(ReturnItemTK)

                OrgGoldTG(0) = New ReportParameter("OrgGoldTG", objDetial.OrgGoldTG)
                rpt_Repair.LocalReport.SetParameters(OrgGoldTG)

                OrgGoldTK(0) = New ReportParameter("OrgGoldTK", objDetial.OrgGoldTK)
                rpt_Repair.LocalReport.SetParameters(OrgGoldTK)

                ReturnGoldTK(0) = New ReportParameter("ReturnGoldTK", objDetial.ReturnGoldTK)
                rpt_Repair.LocalReport.SetParameters(ReturnGoldTK)

                ReturnGoldTG(0) = New ReportParameter("ReturnGoldTG", objDetial.ReturnGoldTG)
                rpt_Repair.LocalReport.SetParameters(ReturnGoldTG)

                WasteTG(0) = New ReportParameter("WasteTG", objDetial.WasteTG)
                rpt_Repair.LocalReport.SetParameters(WasteTG)

                WasteTK(0) = New ReportParameter("WasteTK", objDetial.WasteTK)
                rpt_Repair.LocalReport.SetParameters(WasteTK)

                OrgGemsTK(0) = New ReportParameter("OrgGemTK", objDetial.OrgGemTK)
                rpt_Repair.LocalReport.SetParameters(OrgGemsTK)

                OrgGemsTG(0) = New ReportParameter("OrgGemTG", objDetial.OrgGemTG)
                rpt_Repair.LocalReport.SetParameters(OrgGemsTG)

                ReturnGemsTK(0) = New ReportParameter("ReturnGemTK", objDetial.ReturnGemTK)
                rpt_Repair.LocalReport.SetParameters(ReturnGemsTK)

                ReturnGemsTG(0) = New ReportParameter("ReturnGemTG", objDetial.ReturnGemTG)
                rpt_Repair.LocalReport.SetParameters(ReturnGemsTG)

                ItemAmount(0) = New ReportParameter("ItemAmount", objDetial.ItemAmount)
                rpt_Repair.LocalReport.SetParameters(ItemAmount)

                Dim dtTotal As New DataTable
                Dim NetAmt As Integer = 0
                Dim PaidAmt As Integer = 0
                Dim BalanceAmt As Integer = 0
                dtTotal = _RepairReturnController.GetRepairReturnStockReportForTotalByDetail(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                If dtTotal.Rows.Count() > 0 Then
                    For Each dr As DataRow In dtTotal.Rows
                        NetAmt += dr.Item("NetAmount")
                        PaidAmt += dr.Item("PaidAmount")
                        BalanceAmt += dr.Item("BalanceAmount")
                    Next
                Else
                    NetAmt = 0
                    PaidAmt = 0
                    BalanceAmt = 0
                End If

                NetAmount(0) = New ReportParameter("NetAmount", NetAmt)
                rpt_Repair.LocalReport.SetParameters(NetAmount)

                PaidAmount(0) = New ReportParameter("PaidAmount", PaidAmt)
                rpt_Repair.LocalReport.SetParameters(PaidAmount)

                BalanceAmount(0) = New ReportParameter("BalanceAmount", BalanceAmt)
                rpt_Repair.LocalReport.SetParameters(BalanceAmount)
            End If

            Dim IsSelect As Boolean = False
            Dim Selected(0) As ReportParameter
            If chkCustomerName.Checked Or chkItemCategory.Checked Or chkGoldQuality.Checked Or chkItemName.Checked Then
                IsSelect = True
            End If
            Selected(0) = New ReportParameter("Selected", IsSelect)
            rpt_Repair.LocalReport.SetParameters(Selected)
        End If
        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_Repair.LocalReport.SetParameters(G_PToY)
        rpt_Repair.RefreshReport()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rpt_Repair_Load(sender As Object, e As EventArgs) Handles rpt_Repair.Load
        GetCombo()
        cboGoldQuality.Enabled = False
        cboCategory.Enabled = False
        cboItemName.Enabled = False
        chkCancel.Enabled = False
        txtCustomerCode.Enabled = False
        txtCustomerName.Enabled = False
        btnCustomer.Enabled = False
        chkItemName.Enabled = False
        chkCancel.Enabled = True
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
        Me.rpt_Repair.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""

        If optRepairReceive.Checked Then
            If (chkGoldQuality.Checked) Then
                GetFilterString += " And D.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
            End If

            If (chkItemCategory.Checked) Then
                GetFilterString += " And D.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
            End If

            If (chkItemName.Checked) Then
                GetFilterString += " And D.ItemNameID = '" & cboItemName.SelectedValue & "'"
            End If

            If (chkCustomerName.Checked) Then
                GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
            End If

        Else
            If (chkGoldQuality.Checked) Then
                GetFilterString += " And RD.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
            End If

            If (chkItemCategory.Checked) Then
                GetFilterString += " And RD.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
            End If

            If (chkItemName.Checked) Then
                GetFilterString += " And RD.ItemNameID = '" & cboItemName.SelectedValue & "'"
            End If

            If (chkCustomerName.Checked) Then
                GetFilterString += " And RH.CustomerID  = '" & _CustomerID & "'"
            End If
        End If

        If ChkLocation.Checked Then
            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
        'If chkCancel.Checked Then
        '    GetFilterString += " And H.IsCancel =1 "
        'End If
    End Function
    Private Sub GetCombo()

        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView

        'cboLocation.DisplayMember = "Location"
        'cboLocation.ValueMember = "@LocationID"
        'cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboCategory.DisplayMember = "ItemCategory_"
        cboCategory.ValueMember = "@ItemCategoryID"
        cboCategory.DataSource = _ItemCatController.GetAllItemCategory().DefaultView
    End Sub
    Private Sub RefreshItemNameCbo(ByVal ItemID As String)
        Dim dt As New DataTable
        dt = _ItemNameController.GetItemNameListByItemCategory(ItemID)
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

    Private Sub cboCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCategory.SelectedValueChanged
        Dim itemid As String
        itemid = cboCategory.SelectedValue
        RefreshItemNameCbo(itemid)
    End Sub

    Private Sub chkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemName.CheckedChanged
        If (chkItemName.Checked) Then
            cboItemName.Enabled = True
        Else
            cboItemName.Enabled = False
        End If
    End Sub

    Private Sub chkItemCategory_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemCategory.CheckedChanged
        If (chkItemCategory.Checked) Then
            cboCategory.Enabled = True
            chkItemName.Enabled = True
        Else
            cboCategory.Enabled = False
            chkItemName.Enabled = False
            chkItemName.Checked = False
            cboItemName.Enabled = False
        End If
    End Sub

    Private Sub chkGoldQuality_CheckedChanged(sender As Object, e As EventArgs) Handles chkGoldQuality.CheckedChanged
        If (chkGoldQuality.Checked) Then
            cboGoldQuality.Enabled = True
        Else
            cboGoldQuality.Enabled = False
        End If
    End Sub

    Private Sub chkCustomerName_CheckedChanged(sender As Object, e As EventArgs) Handles chkCustomerName.CheckedChanged
        If (chkCustomerName.Checked) Then
            txtCustomerName.Enabled = True
            txtCustomerCode.Enabled = True
            btnCustomer.Enabled = True
        Else
            txtCustomerCode.Text = ""
            txtCustomerName.Text = ""
            _CustomerID = ""
            txtCustomerName.Enabled = False
            txtCustomerCode.Enabled = False
            btnCustomer.Enabled = False
        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If chkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
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
                txtCustomerName.Text = DataItem("CustomerName_")
            Else
                MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtCustomerName.Text = ""
            End If
        End If
    End Sub
    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("RepairReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtCustomerCode_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerCode.TextChanged
        Dim objCustomer As CustomerInfo
        If _IsCustomerName = False Then
            _IsCustomerCode = True
            If txtCustomerCode.Text <> "" Then
                objCustomer = _CustomerController.GetCustomerCode(txtCustomerCode.Text)
                If objCustomer.CustomerID <> "" Then
                    If objCustomer.IsInactive = 0 Then
                        With objCustomer
                            _CustomerID = .CustomerID
                            txtCustomerCode.Text = .CustomerCode
                            txtCustomerName.Text = .CustomerName
                        End With
                    Else
                        MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                        _CustomerID = ""
                        txtCustomerName.Text = ""
                        Exit Sub
                    End If
                    txtCustomerCode.Enabled = True
                Else
                    _CustomerID = ""
                    txtCustomerName.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomerName.Text = ""
            End If
            _IsCustomerCode = False
        End If
    End Sub

    Private Sub txtCustomerName_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerName.TextChanged
        If _IsCustomerCode = False Then
            Dim dt As New DataTable
            Dim DataCollection As New AutoCompleteStringCollection()
            _IsCustomerName = True
            If txtCustomerName.Text <> "" Then
                dt = _CustomerController.GetCustomerDataByCustomerName(txtCustomerName.Text.Trim)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0).Item("IsInactive") = False) Then
                        _CustomerID = dt.Rows(0).Item("CustomerID")
                        txtCustomerCode.Text = dt.Rows(0).Item("CustomerCode")
                        txtCustomerName.Text = dt.Rows(0).Item("CustomerName")
                    Else
                        _CustomerID = ""
                        txtCustomerCode.Text = ""
                    End If
                Else
                    _CustomerID = ""
                    txtCustomerCode.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomerCode.Text = ""
            End If
            _IsCustomerName = False
        End If
    End Sub
End Class