Imports Microsoft.Reporting.WinForms
Imports BusinessRule
Imports System.Configuration
Imports CommonInfo
Public Class frm_rpt_ProfitForSaleItem
    Private ReportDA As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _GoldSmith As GoldSmith.IGoldSmithController = Factory.Instance.CreateGoldSmithController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _CustomerID As String = ""
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
    Private Sub frm_rpt_ProfitForSaleItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        cboGoldQuality.Enabled = False
        cboCategory.Enabled = False
        chkItemName.Checked = False
        chkItemName.Enabled = False
        cboItemName.Enabled = False
        radAll.Checked = True
        chkBalanceStockByValue.Checked = False
        chkGoldSmith.Checked = False
        chkShopItem.Checked = False
        txtGoldSmith.Text = ""

        _CustomerID = "0"
        chkCustomerName.Checked = False
        txtCustomerCode.Enabled = False
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtCustomer.Enabled = False
        txtAddress.Enabled = False
        btnCustomer.Enabled = False
        chkGS.Checked = False
        cboGoldSmith.Enabled = False
        cboLocation.Enabled = True
        ChkLocation.Checked = True

        If Global_IsHeadOffice Then
            'cboLocation.SelectedValue = Global_CurrentLocationID
            cboLocation.DisplayMember = "Location_"
            cboLocation.ValueMember = "@LocationID"
            cboLocation.DataSource = _Location.GetAllLocationList().DefaultView


        Else
            cboLocation.DisplayMember = "Location_"
            cboLocation.ValueMember = "@LocationID"
            cboLocation.DataSource = _Location.GetAllLocationList().DefaultView
            'cboLocation.SelectedValue = Global_CurrentLocationID
            cboLocation.Enabled = False
            ChkLocation.Enabled = False
        End If

        Get_Combos()
        cboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtItem As New DataTable
        Dim TotalAmt As Integer = 0
        Dim curRDLC As String = ""
        Dim Title(0) As ReportParameter
        Dim TotalAmount(0) As ReportParameter
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If chkBalanceStockByValue.Checked Then
            Dim GetFilterString As String = ""

            If chkOrderStock.Checked Then
                GetFilterString += " And F.IsOrder = 1"
            End If

            If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
                GetFilterString += " And F.GoldSmith='' "
            End If

            If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
                GetFilterString += " And F.GoldSmith<>'' "
            End If

            If (txtGoldSmith.Text <> "") Then
                GetFilterString += " And F.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'"
            End If

            dt = ReportDA.GetBalanceStockByValue(GetFilterString)
            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
            End If
            rptViewer.Reset()
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_BalanceStockByValue.rdl"
            'rptViewer.LocalReport.ReportEmbeddedResource = "\Report\RDLC\rpt_BalanceStockByValue.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))
        Else
            If optSaleStock.Checked Then
                If ChkLocation.Checked Then
                    If chkOrderStock.Checked Then
                        dt = ReportDA.GetProfitForSaleItem("OrderStock", dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        dt = ReportDA.GetProfitForSaleItem("SaleStock", dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    End If
                Else
                    MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
                End If

                If dt.Rows.Count > 0 Then
                    Dim VoucherNo As String = ""
                    For Each dr As DataRow In dt.Rows
                        If dr.Item("VoucherNo") <> VoucherNo Then
                            dr.Item("TotalSumAmount") = dr.Item("TotalNetAmount")
                            dr.Item("DiscountAmount") = dr.Item("DiscountAmount")

                        Else
                            dr.Item("TotalSumAmount") = 0
                            dr.Item("DiscountAmount") = 0
                        End If
                        VoucherNo = dr.Item("VoucherNo")
                    Next
                End If

                If dt.Rows.Count = 0 Then
                    MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                End If

                rptViewer.Reset()
                rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & IIf(radDetail.Checked, "\Report\RDLC\rpt_ProfitForSaleItem_Detail.rdl", "\Report\RDLC\rpt_ProfitForSaleItem_Summary_Report.rdl")
                rptViewer.LocalReport.DataSources.Clear()
                rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))

            ElseIf optBalanceStocks.Checked Then
                If ChkLocation.Checked Then
                    dt = ReportDA.GetProfitForSaleItem("BalanceStock", dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                Else
                    MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
                End If

                If dt.Rows.Count = 0 Then
                    MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                End If
                If radDetail.Checked Then
                    curRDLC = "\Report\RDLC\rpt_ProfitForBalanceItem_Detail.rdl"
                Else
                    curRDLC = "\Report\RDLC\rpt_ProfitForBalanceStock_Report_Summary.rdl"
                End If
                rptViewer.Reset()
                rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & curRDLC
                rptViewer.LocalReport.DataSources.Clear()
                rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))

                End If

                Dim TitleType As String = ""
                If radAll.Checked Then
                    TitleType = "(All)"
                ElseIf radStock.Checked Then
                    TitleType = "(ရွှေထည်)"
                ElseIf radDiamondStock.Checked Then
                    TitleType = "(စိန်ထည်)"
                End If

                If radDetail.Checked Then
                    If chkOrderStock.Checked Then
                        If optBalanceStocks.Checked Then
                            Title(0) = New ReportParameter("Title", "အော်ဒါလက်ကျန် ပစ္စည်းအမြတ်စာရင်း" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        Else
                            Title(0) = New ReportParameter("Title", "အော်ဒါပစ္စည်း အမြတ်စာရင်း" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        End If
                    Else
                        If optBalanceStocks.Checked Then
                            Title(0) = New ReportParameter("Title", "လက်ကျန်ပစ္စည်း အမြတ်စာရင်း" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        Else
                            Title(0) = New ReportParameter("Title", "ရောင်းပြီး ပစ္စည်းအမြတ်စာရင်း" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        End If
                    End If
                Else
                    If chkOrderStock.Checked Then
                        If optBalanceStocks.Checked Then
                            Title(0) = New ReportParameter("Title", "အော်ဒါလက်ကျန် ပစ္စည်းအမြတ်စာရင်းချုပ်" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        Else
                            Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက်အော်ဒါပစ္စည်း အမြတ်စာရင်းချုပ်" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        End If
                    Else
                        If optBalanceStocks.Checked Then
                            Title(0) = New ReportParameter("Title", "လက်ကျန်ပစ္စည်း အမြတ်စာရင်းချုပ်" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        Else
                            Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက်ရောင်းပြီး ပစ္စည်းအမြတ်စာရင်းချုပ်" & TitleType)
                            rptViewer.LocalReport.SetParameters(Title)
                        End If
                    End If
                End If

                Dim IsSelect As Boolean = False
                Dim Selected(0) As ReportParameter
                If chkGoldQuality.Checked Or chkItemCategory.Checked Or chkItemName.Checked Or chkShopItem.Checked Or chkGoldSmith.Checked Then
                    IsSelect = True
                End If
                Selected(0) = New ReportParameter("Selected", IsSelect)
                rptViewer.LocalReport.SetParameters(Selected)
        End If

        ' Shop and GoldSmith Waste
        Dim isShop As Boolean = 0
        If radShopWaste.Checked Then
            isShop = 1
        Else
            isShop = 0
        End If
        Dim IsShopWaste(0) As ReportParameter
        IsShopWaste(0) = New ReportParameter("IsShopWaste", isShop)
        rptViewer.LocalReport.SetParameters(IsShopWaste)

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)

        Dim GDecFormat(0) As ReportParameter
        GDecFormat(0) = New ReportParameter("GDecFormat", Global_DecimalFormat)
        rptViewer.LocalReport.SetParameters(GDecFormat)

        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""

        If chkOrderStock.Checked Then
            GetFilterString += " And I.IsOrder = 1"
        End If

        If chkCustomerName.Checked And optSaleStock.Checked Then
            If chkOrderStock.Checked Then
                GetFilterString += " And O.CustomerID  = '" & _CustomerID & "'"
            Else
                GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
            End If
        End If

        If (chkGoldQuality.Checked) Then
            GetFilterString += " And I.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
        End If

        If (chkItemCategory.Checked) Then
            GetFilterString += " And I.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
        End If
        If (chkGS.Checked) Then
            GetFilterString += " And I.GoldSmithID = '" & cboGoldSmith.SelectedValue & "'"
        End If
        If (chkItemName.Checked) Then
            GetFilterString += " And I.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If

        If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
            GetFilterString += " And I.GoldSmith='' "
        End If

        If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
            GetFilterString += " And I.GoldSmith<>'' "
        End If

        If (txtGoldSmith.Text <> "") Then
            GetFilterString += " And I.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'"
        End If
        If radStock.Checked Then
            GetFilterString += " And I.IsDiamond=0 AND G.IsGramRate=0 "
        End If
        If radDiamondStock.Checked Then
            GetFilterString += " And I.IsDiamond=1"
        End If
        If (radPlatinum.Checked) Then
            GetFilterString += " And  G.IsGramRate=1 AND I.IsDiamond=0 "
        End If
        If optBalanceStocks.Checked Then
            If ChkLocation.Checked Then
                GetFilterString += " And I.LocationID = '" & cboLocation.SelectedValue & "'"
            End If
        Else
            If ChkLocation.Checked Then
                GetFilterString += " And H.LocationID = '" & cboLocation.SelectedValue & "'"
            End If
        End If
    End Function
    Private Function GetFilter() As String
        GetFilter = ""
        If optBalanceStocks.Checked Then
            If (chkGoldQuality.Checked) Then
                GetFilter += " And I.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
            End If
            If (chkItemCategory.Checked) Then
                GetFilter += " And I.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
            End If
            If (chkItemName.Checked) Then
                GetFilter += " And I.ItemNameID = '" & cboItemName.SelectedValue & "'"
            End If
            If ChkLocation.Checked Then
                GetFilter += " And I.LocationID = '" & cboLocation.SelectedValue & "'"
            End If
        Else
            If ChkLocation.Checked Then
                GetFilter += " And H.LocationID = '" & cboLocation.SelectedValue & "'"
            End If
        End If
    End Function
    Private Sub Get_Combos()

        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView

        cboCategory.DisplayMember = "ItemCategory_"
        cboCategory.ValueMember = "@ItemCategoryID"
        cboCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView

        cboGoldSmith.DisplayMember = "Name_"
        cboGoldSmith.ValueMember = "@GoldSmithID"
        cboGoldSmith.DataSource = _GoldSmith.GetAllGoldSmithList().DefaultView
    End Sub
    Private Sub chkGoldQuality_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQuality.CheckedChanged
        If (chkGoldQuality.Checked) Then
            cboGoldQuality.Enabled = True
        Else
            cboGoldQuality.Enabled = False
        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If ChkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub

    Private Sub chkItemCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCategory.CheckedChanged
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

    'Private Sub radDetail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radDetail.CheckedChanged
    '    If radDetail.Checked Then
    '        chkAddOrSub.Enabled = True
    '    Else
    '        chkAddOrSub.Enabled = False
    '        chkAddOrSub.Checked = False
    '    End If
    'End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("ProfitForSaleItemReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub chkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemName.CheckedChanged
        If (chkItemName.Checked) Then
            cboItemName.Enabled = True
        Else
            cboItemName.Enabled = False

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
            cboItemName.DisplayMember = "ItemName"
            cboItemName.ValueMember = "ItemNameID"
            cboItemName.Text = ""
            cboItemName.SelectedIndex = -1
        End If
    End Sub

    Private Sub chkBalanceStockByValue_CheckedChanged(sender As Object, e As EventArgs) Handles chkBalanceStockByValue.CheckedChanged
        If chkBalanceStockByValue.Checked Then
            grpBoxStockType.Enabled = False
            grpBoxType.Enabled = False
            grpByDate.Enabled = False
            grpView.Enabled = False
            grpCategory.Enabled = False
            _CustomerID = "0"
            chkCustomerName.Checked = False
            lblAddress.Enabled = False
            chkCustomerName.Enabled = False
            txtCustomerCode.Enabled = False
            txtCustomerCode.Text = ""
            txtCustomer.Text = ""
            txtAddress.Text = ""
            txtCustomer.Enabled = False
            txtAddress.Enabled = False
            btnCustomer.Enabled = False

        Else
            grpBoxStockType.Enabled = True
            grpBoxType.Enabled = True
            grpByDate.Enabled = True
            grpView.Enabled = True
            grpCategory.Enabled = True
            If optBalanceStocks.Checked Then
                _CustomerID = "0"
                chkCustomerName.Checked = False
                lblAddress.Enabled = False
                chkCustomerName.Enabled = False
                txtCustomerCode.Enabled = False
                txtCustomerCode.Text = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
                txtCustomer.Enabled = False
                txtAddress.Enabled = False
                btnCustomer.Enabled = False
            Else
                chkCustomerName.Enabled = True
                lblAddress.Enabled = True
            End If

        End If
    End Sub

    'Private Sub optBalanceStocks_CheckedChanged(sender As Object, e As EventArgs) Handles optBalanceStocks.CheckedChanged
    '    If optBalanceStocks.Checked Then
    '        grpCategory.Enabled = True
    '    Else
    '        cboGoldQuality.Enabled = False
    '        cboCategory.Enabled = False
    '        chkItemName.Checked = False
    '        chkItemName.Enabled = False
    '        cboItemName.Enabled = False
    '        chkGoldQuality.Checked = False
    '        chkItemCategory.Checked = False
    '        grpCategory.Enabled = False
    '    End If
    'End Sub

    Private Sub chkShopItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkShopItem.CheckedChanged
        If chkShopItem.Checked Then
            If chkGoldSmith.Checked Then
                chkGoldSmith.Checked = False
            End If
            chkOrderStock.Checked = False
        End If
    End Sub
    Private Sub chkGoldSmith_CheckedChanged(sender As Object, e As EventArgs) Handles chkGoldSmith.CheckedChanged
        If chkGoldSmith.Checked Then
            If chkShopItem.Checked Then
                chkShopItem.Checked = False
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

    Private Sub optBalanceStocks_CheckedChanged(sender As Object, e As EventArgs) Handles optBalanceStocks.CheckedChanged
        If optBalanceStocks.Checked Then
            _CustomerID = "0"
            chkCustomerName.Checked = False
            lblAddress.Enabled = False
            chkCustomerName.Enabled = False
            txtCustomerCode.Enabled = False
            txtCustomerCode.Text = ""
            txtCustomer.Text = ""
            txtAddress.Text = ""
            txtCustomer.Enabled = False
            txtAddress.Enabled = False
            btnCustomer.Enabled = False
        Else
            chkCustomerName.Enabled = True
            lblAddress.Enabled = True
        End If
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
                            txtCustomer.Text = .CustomerName
                            txtAddress.Text = .CustomerAddress
                        End With
                    Else
                        MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                        _CustomerID = ""
                        txtCustomer.Text = ""
                        txtAddress.Text = ""
                        Exit Sub
                    End If
                    txtCustomerCode.Enabled = True
                Else
                    _CustomerID = ""
                    txtCustomer.Text = ""
                    txtAddress.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
            End If
            _IsCustomerCode = False
        End If
    End Sub

    Private Sub chkOrderStock_CheckedChanged(sender As Object, e As EventArgs) Handles chkOrderStock.CheckedChanged
        If chkOrderStock.Checked Then
            chkShopItem.Checked = False
        End If
    End Sub

    Private Sub cboGoldSmith_KeyUp(sender As Object, e As KeyEventArgs) Handles cboGoldSmith.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldSmith, e)
    End Sub

    Private Sub cboGoldSmith_Leave(sender As Object, e As EventArgs) Handles cboGoldSmith.Leave
        AutoCompleteCombo_Leave(cboGoldSmith, "")
    End Sub

    Private Sub cboGoldSmith_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboGoldSmith.SelectedValueChanged
        Dim goldsmithid As String
        goldsmithid = cboGoldSmith.SelectedValue
    End Sub

    Private Sub chkGS_CheckedChanged(sender As Object, e As EventArgs) Handles chkGS.CheckedChanged
        If (chkGS.Checked) Then

            cboGoldSmith.Enabled = True
        Else
            cboGoldSmith.Enabled = False

        End If
    End Sub

    Private Sub cboLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLocation.SelectedIndexChanged

    End Sub
End Class