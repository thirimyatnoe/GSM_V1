Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo
Imports BusinessRule.UserManagement

Public Class frm_rpt_SaleInvoice
    Private _CustomerID As String = ""
    Private _LoginController As New LogIn
    Private ReportDA As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _GoldSmith As GoldSmith.IGoldSmithController = Factory.Instance.CreateGoldSmithController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private dtUserInfo As New DataTable
    Public CurrentUser As String
    Public CurrentuserLevel As String
    Public CurrentuserLevelID As Integer
    Dim dtReturn As New DataTable
    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
    Dim Fromdate As DateTime
    Dim strCri As String
    Dim LocationID As String
    Dim ToDate As DateTime
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
        SetUserInfo()

        cboGoldQuality.Enabled = False
        cboCategory.Enabled = False
        cboStaff.Enabled = False
        chkFix.Checked = False
        ChkItemName.Checked = False
        ChkItemName.Enabled = False
        cboItemName.Enabled = False
        cboGoldSmith.Enabled = False
        ChkLocation.Enabled = True
        ChkLocation.Checked = True


        Fromdate = CDate(Format(dtpFromDate.Value, "MM/dd/yyyy"))

        ToDate = CDate(Format(dtpToDate.Value, "MM/dd/yyyy"))

        If Global_IsHeadOffice Then
            'CboLocation.SelectedValue = Global_CurrentLocationID
            CboLocation.DisplayMember = "Location_"
            CboLocation.ValueMember = "@LocationID"
            CboLocation.DataSource = _Location.GetAllLocationList().DefaultView


        Else
            CboLocation.DisplayMember = "Location_"
            CboLocation.ValueMember = "@LocationID"
            CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
            'CboLocation.SelectedValue = Global_CurrentLocationID
            CboLocation.Enabled = False
            ChkLocation.Enabled = False
        End If
        CboLocation.SelectedValue = Global_CurrentLocationID
        LocationID = Global_CurrentLocationID

        radAll.Checked = True
        _CustomerID = "0"
        chkCustomerName.Checked = False
        txtCustomerCode.Enabled = False
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtCustomer.Enabled = False
        txtAddress.Enabled = False
        btnCustomer.Enabled = False
        'chkIsChange.Checked = False
        'chkAdvanceSaleReceive.Checked = False
        'chkAdvanceSaleReturn.Checked = False
        'chkCancel.Checked = False
        chkType.Checked = False
        radAdvanceSaleReturn.Enabled = False
        radAdvanceSaleReceive.Enabled = False
        radIsChange.Enabled = False
        radCancel.Enabled = False
        radDebt.Enabled = False
        radPaidTo.Enabled = False
        radOtherCash.Checked = False
        txtGoldSmith.Text = ""
        txtBarcodeNo.Text = ""
        chkGoldSmith.Checked = False
        chkShopItem.Checked = False
        chkGS.Checked = False
        radOtherCash.Enabled = False
        radAdvanceSaleReceive.Checked = True
        'CboLocation.DisplayMember = "Location_"
        'CboLocation.ValueMember = "@LocationID"
        'CboLocation.DataSource = _Location.GetAllLocationList().DefaultView

    End Sub
    Private Sub SetUserInfo()
        Try
            dtUserInfo = _LoginController.GetUserInfo()

            'With CboUserList
            '    .DataSource = dtUserInfo.DefaultView
            '    .ValueMember = "UserID"
            '    .DisplayMember = "UserName"
            'End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtDetail As New DataTable
        Dim isPlatinumG As String = "All"
        Dim SaleInvoiceDetailID As String = ""
        Dim SalesInvoiceGemItemID As String = ""
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If


        If ChkLocation.Checked Then
            If radDetail.Checked Then
                dt = ReportDA.GetSalesInvoiceReport(dtpFromDate.Value.ToShortDateString, dtpToDate.Value.Date, GetFilterString)
                If dt.Rows.Count > 0 Then
                    Dim _SaleInvoiceHeaderID As String = ""

                    For i As Integer = 0 To dt.Rows.Count - 1
                        If Not IsDBNull(dt.Rows(i).Item("SalesInvoiceGemItemID")) Then

                            If dt.Rows(i).Item("SaleInvoiceDetailID") <> SaleInvoiceDetailID Then
                                dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + dt.Rows(i).Item("ItemNetAmount")
                            Else
                                dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + 0
                            End If

                            SalesInvoiceGemItemID = dt.Rows(i).Item("SalesInvoiceGemItemID")
                            dtDetail = ReportDA.GetSaleInvoiceGemDataBySaleInvoiceGemsItemID(SalesInvoiceGemItemID)
                            For j As Integer = 0 To dtDetail.Rows.Count - 1
                                SaleInvoiceDetailID = dtDetail.Rows(j).Item("SaleInvoiceDetailID")
                            Next
                        Else
                            dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("ItemNetAmount") + 0
                        End If

                        If _SaleInvoiceHeaderID <> dt.Rows(i).Item("SaleInvoiceHeaderID") Then
                            dt.Rows(i).Item("NetAmount") = dt.Rows(i).Item("NetAmount")
                            dt.Rows(i).Item("DiscountAmount") = dt.Rows(i).Item("DiscountAmount")
                            dt.Rows(i).Item("PromotionAmount") = dt.Rows(i).Item("PromotionAmount")
                            dt.Rows(i).Item("TotalPaidAmount") = dt.Rows(i).Item("TotalPaidAmount")
                            dt.Rows(i).Item("PaidAmount") = dt.Rows(i).Item("PaidAmount")
                            dt.Rows(i).Item("BalanceAmount") = dt.Rows(i).Item("BalanceAmount")

                            If (CBool(dt.Rows(i).Item("IsAdvance")) = True And CBool(dt.Rows(i).Item("IsCancel")) = True) Then
                                dt.Rows(i).Item("CashOutAmount") = dt.Rows(i).Item("TotalPaidAmount")
                            ElseIf CInt(dt.Rows(i).Item("PaidAmount")) < 0 Then
                                dt.Rows(i).Item("CashOutAmount") = 0 - dt.Rows(i).Item("PaidAmount")
                            Else
                                dt.Rows(i).Item("CashInAmount") = dt.Rows(i).Item("PaidAmount")
                            End If
                            dt.Rows(i).Item("DiscountAmount") = dt.Rows(i).Item("DiscountAmount")
                        Else
                            dt.Rows(i).Item("NetAmount") = 0
                            dt.Rows(i).Item("DiscountAmount") = 0
                            dt.Rows(i).Item("PromotionAmount") = 0
                            dt.Rows(i).Item("TotalPaidAmount") = 0
                            dt.Rows(i).Item("PaidAmount") = 0
                            dt.Rows(i).Item("BalanceAmount") = 0
                            dt.Rows(i).Item("CashInAmount") = 0
                            dt.Rows(i).Item("CashOutAmount") = 0
                        End If

                        _SaleInvoiceHeaderID = dt.Rows(i).Item("SaleInvoiceHeaderID")
                    Next
                End If
            ElseIf radSummary.Checked Then
                dt = ReportDA.GetSalesInvoiceReportForSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                'dt = ReportDA.GetSalesInvoiceReportForSummaryReport(Fromdate.Date, ToDate.Date, GetFilterString)
            End If
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        rptViewer.Reset()
        If radSummary.Checked Then
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleInvoice_Report_Summary.rdlc"
        Else
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleInvoice_Detail.rdl"
        End If
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))
        If radDetail.Checked Then
            Dim ItemTG(0) As ReportParameter
            Dim GoldTG(0) As ReportParameter
            Dim GemsTG(0) As ReportParameter
            Dim WasteTG(0) As ReportParameter
            Dim TotalTG(0) As ReportParameter
            Dim ItemTK(0) As ReportParameter
            Dim GoldTK(0) As ReportParameter
            Dim GemsTK(0) As ReportParameter
            Dim WasteTK(0) As ReportParameter
            Dim TotalTK(0) As ReportParameter
            Dim QTY(0) As ReportParameter
            Dim TotalAmount(0) As ReportParameter
            Dim NetAmount(0) As ReportParameter
            Dim PaidAmount(0) As ReportParameter
            Dim BalanceAmount(0) As ReportParameter
            Dim ChangeAmount(0) As ReportParameter
            Dim RefundAmount(0) As ReportParameter
            Dim Quantity(0) As ReportParameter
            Dim obj As New CommonInfo.SalesInvoiceDetailInfo

            obj = ReportDA.GetSaleInvoiceDetailForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

            If radPlatinum.Checked Or radDiamondStock.Checked Then
                isPlatinumG = "Platinum"
            Else
                isPlatinumG = "All"
            End If

            Dim IsPlatinum(0) As ReportParameter
            IsPlatinum(0) = New ReportParameter("IsPlatinum", isPlatinumG)

            rptViewer.LocalReport.SetParameters(IsPlatinum)

            ItemTG(0) = New ReportParameter("ItemTG", obj.ItemTG)

            rptViewer.LocalReport.SetParameters(ItemTG)

            GoldTG(0) = New ReportParameter("GoldTG", obj.GoldTG)
            rptViewer.LocalReport.SetParameters(GoldTG)

            GemsTG(0) = New ReportParameter("GemsTG", obj.GemsTG)
            rptViewer.LocalReport.SetParameters(GemsTG)

            WasteTG(0) = New ReportParameter("WasteTG", obj.WasteTG)
            rptViewer.LocalReport.SetParameters(WasteTG)

            TotalTG(0) = New ReportParameter("TotalTG", obj.TotalTG)
            rptViewer.LocalReport.SetParameters(TotalTG)

            ItemTK(0) = New ReportParameter("ItemTK", obj.ItemTK)
            rptViewer.LocalReport.SetParameters(ItemTK)

            GoldTK(0) = New ReportParameter("GoldTK", obj.GoldTK)
            rptViewer.LocalReport.SetParameters(GoldTK)

            GemsTK(0) = New ReportParameter("GemsTK", obj.GemsTK)
            rptViewer.LocalReport.SetParameters(GemsTK)

            WasteTK(0) = New ReportParameter("WasteTK", obj.WasteTK)
            rptViewer.LocalReport.SetParameters(WasteTK)

            TotalTK(0) = New ReportParameter("TotalTK", obj.TotalTK)
            rptViewer.LocalReport.SetParameters(TotalTK)

            QTY(0) = New ReportParameter("QTY", obj.QTY)
            rptViewer.LocalReport.SetParameters(QTY)

            
            '''''''''''''''''''
            'Dim dtTotal As New DataTable
            'Dim TotalAmt As Integer = 0
            'Dim NetAmt As Integer = 0
            'Dim PaidAmt As Integer = 0
            'Dim BalanceAmt As Integer = 0
            'Dim ChangeAmt As Integer = 0
            'Dim RefundAmt As Integer = 0
            'dtTotal = ReportDA.GetSalesInvoiceReportForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            'If dtTotal.Rows.Count() > 0 Then
            '    For Each dr As DataRow In dtTotal.Rows
            '        TotalAmt += IIf(IsDBNull(dr.Item("TotalAmount")) = True, 0, dr.Item("TotalAmount"))
            '        NetAmt += IIf(IsDBNull(dr.Item("NetAmount")) = True, 0, dr.Item("NetAmount"))
            '        PaidAmt += IIf(IsDBNull(dr.Item("PaidAmount")) = True, 0, dr.Item("PaidAmount"))
            '        BalanceAmt += IIf(IsDBNull(dr.Item("BalanceAmount")) = True, 0, dr.Item("BalanceAmount"))
            '        ChangeAmt += IIf(IsDBNull(dr.Item("ChangeAmount")) = True, 0, dr.Item("ChangeAmount"))
            '        RefundAmt += IIf(IsDBNull(dr.Item("RefundAmount")) = True, 0, dr.Item("RefundAmount"))
            '    Next
            'Else
            '    TotalAmt = 0
            '    NetAmt = 0
            '    PaidAmt = 0
            '    BalanceAmt = 0
            '    ChangeAmt = 0
            '    RefundAmt = 0
            'End If
            'TotalAmount(0) = New ReportParameter("TotalAmount", TotalAmt)
            'rptViewer.LocalReport.SetParameters(TotalAmount)

            'NetAmount(0) = New ReportParameter("NetAmount", NetAmt)
            'rptViewer.LocalReport.SetParameters(NetAmount)

            'PaidAmount(0) = New ReportParameter("PaidAmount", PaidAmt)
            'rptViewer.LocalReport.SetParameters(PaidAmount)

            'BalanceAmount(0) = New ReportParameter("BalanceAmount", BalanceAmt)
            'rptViewer.LocalReport.SetParameters(BalanceAmount)

            'ChangeAmount(0) = New ReportParameter("ChangeAmount", ChangeAmt)
            'rptViewer.LocalReport.SetParameters(ChangeAmount)

            'RefundAmount(0) = New ReportParameter("RefundAmount", RefundAmt)
            'rptViewer.LocalReport.SetParameters(RefundAmount)
            ''''''''''''''''''''''''''
            Dim IsSelect As Boolean = False
            Dim Selected(0) As ReportParameter
            If chkGoldQuality.Checked Or chkItemCategory.Checked Or chkFix.Checked Or txtGoldSmith.Text <> "" Or txtBarcodeNo.Text <> "" Or chkGoldSmith.Checked Or chkShopItem.Checked Then
                IsSelect = True
            End If
            Selected(0) = New ReportParameter("Selected", IsSelect)
            rptViewer.LocalReport.SetParameters(Selected)
        End If

        Dim Title(0) As ReportParameter
        Dim Title1(0) As ReportParameter
        If radStock.Checked Then
            If radDetail.Checked Then
                Title(0) = New ReportParameter("Title", "ရတနာအထည်ရောင်းခြင်းစာရင်း(ရွှေထည်)")
                rptViewer.LocalReport.SetParameters(Title)
            Else
                Title1(0) = New ReportParameter("Title1", "ရတနာအထည်ရောင်းခြင်းစာရင်းချုပ်(ရွှေထည်)")
                rptViewer.LocalReport.SetParameters(Title1)
            End If
        ElseIf radAll.Checked Then
            If radDetail.Checked Then
                Title(0) = New ReportParameter("Title", "ရတနာအထည်ရောင်းခြင်းစာရင်း(All)")
                rptViewer.LocalReport.SetParameters(Title)
            Else
                Title1(0) = New ReportParameter("Title1", "ရတနာအထည်ရောင်းခြင်းစာရင်းချုပ်(All)")
                rptViewer.LocalReport.SetParameters(Title1)
            End If
        ElseIf radDiamondStock.Checked Then
            If radDetail.Checked Then
                Title(0) = New ReportParameter("Title", "ရတနာအထည်ရောင်းခြင်းစာရင်း(စိန်ထည်)")
                rptViewer.LocalReport.SetParameters(Title)
            Else
                Title1(0) = New ReportParameter("Title1", "ရတနာအထည်ရောင်းခြင်းစာရင်းချုပ်(စိန်ထည်)")
                rptViewer.LocalReport.SetParameters(Title1)
            End If
        ElseIf radPlatinum.Checked Then
            If radDetail.Checked Then
                Title(0) = New ReportParameter("Title", "ရတနာအထည်ရောင်းခြင်းစာရင်း(ပလက်တီနမ်)")
                rptViewer.LocalReport.SetParameters(Title)
            Else
                Title1(0) = New ReportParameter("Title1", "ရတနာအထည်ရောင်းခြင်းစာရင်းချုပ်(ပလက်တီနမ်)")
                rptViewer.LocalReport.SetParameters(Title1)
            End If

        End If


        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)

        rptViewer.LocalReport.SetParameters(G_PToY)
        rptViewer.RefreshReport()
    End Sub
    'Private Function GetFilterString() As String
    '    GetFilterString = ""
    '    If Global_IsHoMaster = False Then
    '        If (chkGoldQuality.Checked) Then
    '            GetFilterString += " And F.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkItemCategory.Checked) Then
    '            GetFilterString += " And F.ItemCategoryID = '" & cboCategory.SelectedValue & "'" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (ChkItemName.Checked) Then
    '            GetFilterString += " And F.ItemNameID = '" & cboItemName.SelectedValue & "'" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkFix.Checked) Then
    '            GetFilterString += " And F.IsFixPrice =1 " & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkStaff.Checked) Then
    '            GetFilterString += " And H.StaffID = '" & cboStaff.SelectedValue & "'" & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (ChkLocation.Checked) Then
    '            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
    '        End If
    '        If (chkCustomerName.Checked) Then
    '            'GetFilterString += " And H.CustomerID  ='" & _CustomerID & "'"

    '            GetFilterString += " AND H.CustomerID  In (" & _CustomerID & ")" & " and  H.LocationID=" & "'" & LocationID & "'"

    '        End If
    '        If (chkGS.Checked) Then
    '            GetFilterString += " And F.GoldSmithID = '" & cboGoldSmith.SelectedValue & "'" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (txtGoldSmith.Text <> "") Then
    '            GetFilterString += " And F.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (txtBarcodeNo.Text <> "") Then
    '            GetFilterString += " And D.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
    '            GetFilterString += " And F.GoldSmith='' " & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
    '            GetFilterString += " And F.GoldSmith<>'' " & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If radStock.Checked Then
    '            GetFilterString += " And F.IsDiamond=0 And IsGramRate=0" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If radDiamondStock.Checked Then
    '            GetFilterString += " And F.IsDiamond=1" & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (radPlatinum.Checked) Then
    '            GetFilterString += " And IsGramRate=1 AND IsDiamond=0 " & " and  F.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If chkType.Checked Then
    '            If (radAdvanceSaleReceive.Checked) Then
    '                GetFilterString += " And H.IsAdvance = 1 AND H.PaidAmount=0 And H.IsCancel =0 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If

    '            If (radAdvanceSaleReturn.Checked) Then
    '                GetFilterString += " And H.IsAdvance = 1 AND H.PaidAmount>0 And H.IsCancel = 0" & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If

    '            If (radCancel.Checked) Then
    '                GetFilterString += " And H.IsCancel = 1" & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If
    '            If (radIsChange.Checked) Then
    '                GetFilterString += " And H.PurchaseHeaderID  <> ''" & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If
    '            If (radDebt.Checked) Then
    '                GetFilterString += " And (((H.TotalAmount+H.AddOrSub)-(H.DiscountAmount+((H.TotalAmount*H.PromotionDiscount)/100)))-(H.PaidAmount+H.AllAdvanceAmount+H.PurchaseAmount))<>0 AND H.PaidAmount> CASE H.IsAdvance WHEN 0 THEN -1 ELSE 0 END AND H.IsCancel=0 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If

    '            If (radPaidTo.Checked) Then
    '                GetFilterString += " And (PaidAmount<0 OR (IsAdvance=1 AND IsCancel=1)) " & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If

    '            If (radOtherCash.Checked) Then
    '                GetFilterString += " And H.IsOtherCash = 1" & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If
    '        End If

    '    Else
    '        If (chkGoldQuality.Checked) Then
    '            GetFilterString += " And F.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
    '        End If
    '        If (chkItemCategory.Checked) Then
    '            GetFilterString += " And F.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
    '        End If
    '        If (ChkItemName.Checked) Then
    '            GetFilterString += " And F.ItemNameID = '" & cboItemName.SelectedValue & "'"
    '        End If
    '        If (chkFix.Checked) Then
    '            GetFilterString += " And F.IsFixPrice =1 "
    '        End If
    '        If (chkStaff.Checked) Then
    '            GetFilterString += " And H.StaffID = '" & cboStaff.SelectedValue & "'"
    '        End If
    '        If (ChkLocation.Checked) Then
    '            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
    '        End If
    '        If (chkCustomerName.Checked) Then
    '            'GetFilterString += " And H.CustomerID  ='" & _CustomerID & "'"

    '            GetFilterString += " AND H.CustomerID  In (" & _CustomerID & ")"

    '        End If
    '        If (chkGS.Checked) Then
    '            GetFilterString += " And F.GoldSmithID = '" & cboGoldSmith.SelectedValue & "'"
    '        End If
    '        If (txtGoldSmith.Text <> "") Then
    '            GetFilterString += " And F.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'"
    '        End If
    '        If (txtBarcodeNo.Text <> "") Then
    '            GetFilterString += " And D.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'"
    '        End If

    '        If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
    '            GetFilterString += " And F.GoldSmith='' "
    '        End If

    '        If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
    '            GetFilterString += " And F.GoldSmith<>'' "
    '        End If

    '        If radStock.Checked Then
    '            GetFilterString += " And F.IsDiamond=0 And IsGramRate=0"
    '        End If

    '        If radDiamondStock.Checked Then
    '            GetFilterString += " And F.IsDiamond=1"
    '        End If
    '        If (radPlatinum.Checked) Then
    '            GetFilterString += " And IsGramRate=1 AND IsDiamond=0 "
    '        End If

    '        If chkType.Checked Then
    '            If (radAdvanceSaleReceive.Checked) Then
    '                GetFilterString += " And H.IsAdvance = 1 AND H.PaidAmount=0 And H.IsCancel =0 "
    '            End If

    '            If (radAdvanceSaleReturn.Checked) Then
    '                GetFilterString += " And H.IsAdvance = 1 AND H.PaidAmount>0 And H.IsCancel = 0"
    '            End If

    '            If (radCancel.Checked) Then
    '                GetFilterString += " And H.IsCancel = 1"
    '            End If
    '            If (radIsChange.Checked) Then
    '                GetFilterString += " And H.PurchaseHeaderID  <> ''"
    '            End If
    '            If (radDebt.Checked) Then
    '                GetFilterString += " And (((H.TotalAmount+H.AddOrSub)-(H.DiscountAmount+((H.TotalAmount*H.PromotionDiscount)/100)))-(H.PaidAmount+H.AllAdvanceAmount+H.PurchaseAmount))<>0 AND H.PaidAmount> CASE H.IsAdvance WHEN 0 THEN -1 ELSE 0 END AND H.IsCancel=0 "
    '            End If

    '            If (radPaidTo.Checked) Then
    '                GetFilterString += " And (PaidAmount<0 OR (IsAdvance=1 AND IsCancel=1)) "
    '            End If

    '            If (radOtherCash.Checked) Then
    '                GetFilterString += " And H.IsOtherCash = 1"
    '            End If
    '        End If
    '    End If

    'End Function
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
        If (chkCancel.Checked) Then
            GetFilterString += " And H.IsDelete =1 "
        Else
            GetFilterString += " And H.IsDelete =0 "
        End If
        If (chkFix.Checked) Then
            GetFilterString += " And F.IsFixPrice =1 "
        End If
        If (chkStaff.Checked) Then
            GetFilterString += " And H.StaffID = '" & cboStaff.SelectedValue & "'"
        End If
        If (ChkLocation.Checked) Then
            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
        If (chkCustomerName.Checked) Then
            'GetFilterString += " And H.CustomerID  ='" & _CustomerID & "'"

            GetFilterString += " AND H.CustomerID  In (" & _CustomerID & ")"

        End If
        If (chkGS.Checked) Then
            GetFilterString += " And F.GoldSmithID = '" & cboGoldSmith.SelectedValue & "'"
        End If
        If (txtGoldSmith.Text <> "") Then
            GetFilterString += " And F.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'"
        End If
        If (txtBarcodeNo.Text <> "") Then
            GetFilterString += " And D.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'"
        End If

        If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
            GetFilterString += " And F.GoldSmith='' "
        End If

        If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
            GetFilterString += " And F.GoldSmith<>'' "
        End If

        If radStock.Checked Then
            GetFilterString += " And F.IsDiamond=0 And IsGramRate=0"
        End If

        If radDiamondStock.Checked Then
            GetFilterString += " And F.IsDiamond=1"
        End If
        If (radPlatinum.Checked) Then
            GetFilterString += " And IsGramRate=1 AND IsDiamond=0 "
        End If

        If chkType.Checked Then
            If (radAdvanceSaleReceive.Checked) Then
                GetFilterString += " And H.IsAdvance = 1 AND H.PaidAmount=0 And H.IsCancel =0 "
            End If

            If (radAdvanceSaleReturn.Checked) Then
                GetFilterString += " And H.IsAdvance = 1 AND H.PaidAmount>0 And H.IsCancel = 0"
            End If

            If (radCancel.Checked) Then
                GetFilterString += " And H.IsCancel = 1"
            End If
            If (radIsChange.Checked) Then
                GetFilterString += " And H.PurchaseHeaderID  <> ''"
            End If
            If (radDebt.Checked) Then
                GetFilterString += " And (((H.TotalAmount+H.AddOrSub)-(H.DiscountAmount+((H.TotalAmount*H.PromotionDiscount)/100)))-(H.PaidAmount+H.AllAdvanceAmount+H.PurchaseAmount))<>0 AND H.PaidAmount> CASE H.IsAdvance WHEN 0 THEN -1 ELSE 0 END AND H.IsCancel=0 "
            End If

            If (radPaidTo.Checked) Then
                GetFilterString += " And (PaidAmount<0 OR (IsAdvance=1 AND IsCancel=1) ) "
            End If

            If (radOtherCash.Checked) Then
                GetFilterString += " And H.IsOtherCash = 1"
            End If
        End If
    End Function
    Private Sub Get_Combos()
        If Global_IsHoMaster = False Then
            cboGoldQuality.DisplayMember = "GoldQuality"
            cboGoldQuality.ValueMember = "@GoldQualityID"
            cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQualityByLocation(LocationID).DefaultView

            cboCategory.DisplayMember = "ItemCategory_"
            cboCategory.ValueMember = "@ItemCategoryID"
            cboCategory.DataSource = _ItemCategoryController.GetAllItemCategoryByLocation(LocationID).DefaultView

            cboStaff.DisplayMember = "Staff_"
            cboStaff.ValueMember = "StaffID"
            cboStaff.DataSource = objStaffController.GetStaffListByLocation(LocationID).DefaultView

            cboGoldSmith.DisplayMember = "Name_"
            cboGoldSmith.ValueMember = "@GoldSmithID"
            cboGoldSmith.DataSource = _GoldSmith.GetAllGoldSmithListByLocation(LocationID).DefaultView
        Else
            cboGoldQuality.DisplayMember = "GoldQuality"
            cboGoldQuality.ValueMember = "@GoldQualityID"
            cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView

            cboCategory.DisplayMember = "ItemCategory_"
            cboCategory.ValueMember = "@ItemCategoryID"
            cboCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView

            cboStaff.DisplayMember = "Staff_"
            cboStaff.ValueMember = "StaffID"
            cboStaff.DataSource = objStaffController.GetStaffList().DefaultView

            cboGoldSmith.DisplayMember = "Name_"
            cboGoldSmith.ValueMember = "@GoldSmithID"
            cboGoldSmith.DataSource = _GoldSmith.GetAllGoldSmithList().DefaultView

        End If

        'CboLocation.DisplayMember = "Location_"
        'CboLocation.ValueMember = "@LocationID"
        'CboLocation.DataSource = _Location.GetAllLocationList().DefaultView


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

    Private Sub txtCustomerCode_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerCode.TextChanged
        Dim objCustomer As CustomerInfo
        If _IsCustomerName = False Then
            _IsCustomerCode = True
            If txtCustomerCode.Text <> "" Then
                objCustomer = _CustomerController.GetCustomerCode(txtCustomerCode.Text)
                If objCustomer.CustomerID <> "" Then
                    If objCustomer.IsInactive = 0 Then
                        With objCustomer
                            _CustomerID = "'" & .CustomerID & "'"
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
                '_CustomerID = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
            End If
            _IsCustomerCode = False
        End If
    End Sub
    Private Sub btnCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomer.Click
        Dim dt As New DataTable
        Dim dtReturn As DataTable
        dt = _CustomerController.GetAllCustomerAutoCompleteData()
        dtReturn = DirectCast(SearchControlHost.FindFast(dt, "Customer List"), DataTable) '07072018MultiSelectCustomer

        If dtReturn IsNot Nothing Then
            If dtReturn.Rows.Count > 1 Then
                GetStringID(dtReturn, "@CustomerID", "")
                _CustomerID = strCri
                txtCustomerCode.Text = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
            Else
                GetStringID(dtReturn, "@CustomerID", "")
                _CustomerID = Mid(strCri, 2, strCri.Length - 2)


                Dim objCustomer As CustomerInfo
                objCustomer = _CustomerController.GetCustomerByID(_CustomerID)
                If objCustomer.CustomerID <> "" Then
                    If objCustomer.IsInactive = 0 Then
                        With objCustomer
                            _CustomerID = "'" & .CustomerID & "'"
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
                    With objCustomer
                        txtCustomer.Text = .CustomerName
                        txtAddress.Text = .CustomerAddress
                    End With
                End If
            End If
        End If

        'If DataItem IsNot Nothing Then
        '    If DataItem("$Inactive") = False Then
        '        _CustomerID = DataItem("@CustomerID")
        '        txtCustomerCode.Text = DataItem("CustomerCode")
        '        txtCustomer.Text = DataItem("CustomerName_")
        '        txtAddress.Text = DataItem("CustomerAddress_")
        '    Else
        '        MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
        '        _CustomerID = ""
        '        txtCustomerCode.Text = ""
        '        txtCustomer.Text = ""
        '        txtAddress.Text = ""
        '        Exit Sub
        '    End If
        'End If
    End Sub
    Private Sub GetStringID(ByVal dt As DataTable, ByVal FieldName As String, ByVal ColumnName As String)
        strCri = ReturnStringCri(dt, FieldName, "")
    End Sub
    Private Function ReturnStringCri(ByVal dt As DataTable, ByVal Name As String, Optional ByVal OtherFieldName As String = "") As String
        Dim strCust As String = ""
        If OtherFieldName <> "" Then
            For Each row As DataRow In dt.Rows
                strCust += " [" & row(Name).ToString & "]=N'" & row(OtherFieldName).ToString & "'" & " AND"
            Next
            If strCust.EndsWith("AND") Then
                strCust = strCust.Remove(strCust.Length - 3, 3)
            End If
        Else
            For Each row As DataRow In dt.Rows
                strCust += "'" & row(Name).ToString & "'" & ","
            Next
            If strCust <> "" Then
                strCust = Mid(strCust, 1, strCust.Length - 1)
            End If
        End If

        Return strCust
    End Function

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

    Private Sub chkType_CheckedChanged(sender As Object, e As EventArgs) Handles chkType.CheckedChanged
        If chkType.Checked Then
            radAdvanceSaleReturn.Enabled = True
            radAdvanceSaleReceive.Enabled = True
            radIsChange.Enabled = True
            radCancel.Enabled = True
            radDebt.Enabled = True
            radPaidTo.Enabled = True
            radOtherCash.Enabled = True
        Else
            radAdvanceSaleReceive.Checked = True
            radAdvanceSaleReturn.Enabled = False
            radAdvanceSaleReceive.Enabled = False
            radIsChange.Enabled = False
            radCancel.Enabled = False
            radDebt.Enabled = False
            radPaidTo.Enabled = False
            radOtherCash.Enabled = False
        End If
    End Sub

    Private Sub chkShopItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkShopItem.CheckedChanged
        If chkShopItem.Checked Then
            If chkGoldSmith.Checked Then
                chkGoldSmith.Checked = False
            End If
        End If
    End Sub
    Private Sub chkGoldSmith_CheckedChanged(sender As Object, e As EventArgs) Handles chkGoldSmith.CheckedChanged
        If chkGoldSmith.Checked Then
            If chkShopItem.Checked Then
                chkShopItem.Checked = False
            End If
        End If
    End Sub

    Private Sub txtCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtCustomer.TextChanged
        If _IsCustomerCode = False Then
            Dim dt As New DataTable
            Dim DataCollection As New AutoCompleteStringCollection()
            _IsCustomerName = True
            If txtCustomer.Text <> "" Then
                dt = _CustomerController.GetCustomerDataByCustomerName(txtCustomer.Text.Trim)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0).Item("IsInactive") = False) Then
                        _CustomerID = dt.Rows(0).Item("CustomerID")
                        txtCustomerCode.Text = dt.Rows(0).Item("CustomerCode")
                        txtCustomer.Text = dt.Rows(0).Item("CustomerName")
                        txtAddress.Text = dt.Rows(0).Item("CustomerAddress")
                    Else
                        _CustomerID = ""
                        txtCustomerCode.Text = ""
                        txtAddress.Text = ""
                    End If
                Else
                    _CustomerID = ""
                    txtCustomerCode.Text = ""
                    txtAddress.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtAddress.Text = ""
            End If
            _IsCustomerName = False
        End If
    End Sub

    Private Sub chkGS_CheckedChanged(sender As Object, e As EventArgs) Handles chkGS.CheckedChanged
        If (chkGS.Checked) Then
            cboGoldSmith.Enabled = True
        Else
            cboGoldSmith.Enabled = False
        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub

    Private Sub cboGoldSmith_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboGoldSmith.SelectedValueChanged
        Dim goldsmithid As String
        goldsmithid = cboGoldSmith.SelectedValue
    End Sub


    Private Sub cboLocation_SelectedValueChanged(sender As Object, e As EventArgs) Handles CboLocation.SelectedValueChanged

        LocationID = CboLocation.SelectedValue
        cboCategory.SelectedValue = ""
        cboGoldQuality.SelectedValue = ""
        cboStaff.SelectedValue = ""
        cboGoldSmith.SelectedValue = ""
        Get_Combos()


    End Sub

    Private Sub cboGoldSmith_KeyUp(sender As Object, e As KeyEventArgs) Handles cboGoldSmith.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldSmith, e)
    End Sub

    Private Sub cboGoldSmith_Leave(sender As Object, e As EventArgs) Handles cboGoldSmith.Leave
        AutoCompleteCombo_Leave(cboGoldSmith, "")
    End Sub


End Class