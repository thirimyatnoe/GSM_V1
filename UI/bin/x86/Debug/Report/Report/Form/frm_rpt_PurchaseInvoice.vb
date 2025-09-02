Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo
Public Class frm_rpt_PurchaseInvoice
    Dim dt As DataTable

    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemCategory As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQuality As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private objGemsCatController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _PurchaseInvoice As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _StaffCon As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _CustomerID As String = ""
    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
    Dim locationid As String
    Dim strCri As String

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
#Region " Combox Events "
    Private Sub cboItemCat_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemCat.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCat, e)
    End Sub

    Private Sub cboItemCat_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCat.Leave
        AutoCompleteCombo_Leave(cboItemCat, "")
    End Sub

    Private Sub cboGoldQ_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQ.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQ, e)
    End Sub

    Private Sub cboGoldQ_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQ.Leave
        AutoCompleteCombo_Leave(cboGoldQ, "")
    End Sub
    Private Sub cboLocation_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboLocation.SelectedValueChanged

        LocationID = CboLocation.SelectedValue

        cboItemCat.SelectedValue = ""
        cboGoldQ.SelectedValue = ""
        cboStaff.SelectedValue = ""

        GetCombo()
    End Sub
    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboLocation, e)
    End Sub
    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboLocation, "")
    End Sub
    Private Sub cboGemCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGemCategory.Leave
        AutoCompleteCombo_Leave(cboGemCategory, "")
    End Sub

    Private Sub cboGemCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboGemCategory, e)
    End Sub

#End Region
    Private Sub frm_rpt_PurchaseInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboLocation.Enabled = True
        chkLocation.Checked = True
        cboLocation.SelectedValue = Global_CurrentLocationID
        cboGoldQ.Enabled = False
        cboItemCat.Enabled = False
        chkItemCat.Visible = True
        cboItemCat.Visible = True
        chkGemsCategory.Visible = False
        cboGemCategory.Visible = False
        chkGoldQ.Enabled = True
        chkItemName.Checked = False
        chkItemName.Enabled = False
        cboItemName.Enabled = False
        cboStaff.Enabled = False
        _CustomerID = ""
        chkCustomerName.Checked = False
        txtCustomerCode.Enabled = False
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtCustomer.Enabled = False
        txtAddress.Enabled = False
        btnCustomer.Enabled = False
        locationid = Global_CurrentLocationID
        GetCombo()

        If Global_IsHeadOffice Then

            cboLocation.DisplayMember = "Location_"
            cboLocation.ValueMember = "@LocationID"
            cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        Else
            cboLocation.DisplayMember = "Location_"
            cboLocation.ValueMember = "@LocationID"
            cboLocation.DataSource = _Location.GetAllLocationList().DefaultView
            cboLocation.SelectedValue = Global_CurrentLocationID
            cboLocation.Enabled = False
            chkLocation.Enabled = False
        End If
        cboLocation.SelectedValue = Global_CurrentLocationID
        Me.RptViewer.RefreshReport()
    End Sub

    Private Function GetFilterString() As String
        GetFilterString = ""
        If (radShopStock.Checked) Then
            GetFilterString += " And PD.IsShop =1 AND P.IsGem=0 AND P.IsLooseDiamond=0 "
        ElseIf (radGems.Checked) Then
            GetFilterString += " AND P.IsGem=1 "
        ElseIf radPurchase.Checked Then
            GetFilterString += " And PD.IsShop =0 AND P.IsGem=0 AND P.IsLooseDiamond=0 "
        ElseIf radAll.Checked Then
            GetFilterString += " And P.IsGem=0 And P.IsLooseDiamond=0 "
        ElseIf radLooseDiamond.Checked Then
            GetFilterString += " And P.IsLooseDiamond=1 "
        End If

        If chkCustomerName.Checked Then
            'GetFilterString += " And P.CustomerID  = '" & _CustomerID & "'"
            GetFilterString += " AND P.CustomerID  In (" & _CustomerID & ")"
        End If

        If (chkLocation.Checked) Then
            GetFilterString += " And P.LocationID = '" & cboLocation.SelectedValue & "'"
        End If
        If (chkGoldQ.Checked) Then
            GetFilterString += " And PD.GoldQualityID = '" & cboGoldQ.SelectedValue & "'"
        End If
        If (chkItemCat.Checked) Then
            GetFilterString += " And PD.ItemCategoryID = '" & cboItemCat.SelectedValue & "'"
        End If
        If (chkItemName.Checked) Then
            GetFilterString += " And PD.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If
        If radGems.Checked Then
            If (chkGemsCategory.Checked) Then
                GetFilterString += " And PD.ItemCategoryID = '" & cboGemCategory.SelectedValue & "'"
            End If
        ElseIf radLooseDiamond.Checked Then
            If (chkGemsCategory.Checked) Then
                GetFilterString += " And PD.PGemsCategoryID = '" & cboGemCategory.SelectedValue & "'"
            End If
        End If

        If (chkIsChange.Checked) Then
            GetFilterString += " And P.IsChange=1"
        End If

        If (chkIsPurchase.Checked) Then
            GetFilterString += " And P.IsChange=0"
        End If
        If (chkStaff.Checked) Then
            GetFilterString += " And P.StaffID ='" & cboStaff.SelectedValue & "'"
        End If
    End Function

    Private Sub GetCombo()
        'cboLocation.DisplayMember = "Location_"
        'cboLocation.ValueMember = "@LocationID"
        'cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboGoldQ.DisplayMember = "GoldQuality"
        cboGoldQ.ValueMember = "@GoldQualityID"
        cboGoldQ.DataSource = _GoldQuality.GetAllGoldQuality().DefaultView

        cboItemCat.DisplayMember = "ItemCategory_"
        cboItemCat.ValueMember = "@ItemCategoryID"
        cboItemCat.DataSource = _ItemCategory.GetAllItemCategory().DefaultView

        cboGemCategory.DisplayMember = "GemsCategory"
        cboGemCategory.ValueMember = "@GemsCategoryID"
        cboGemCategory.DataSource = objGemsCatController.GetAllGemsCategoryForGridCombo()

        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _StaffCon.GetStaffListByLocation(locationid).DefaultView

       
    End Sub
    Private Sub chkStaff_CheckedChanged(sender As Object, e As EventArgs) Handles chkStaff.CheckedChanged
        If (chkStaff.Checked) Then
            cboStaff.Enabled = True
        Else
            cboStaff.Enabled = False
        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLocation.CheckedChanged
        If chkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub
    Private Sub chkGoldQ_CheckedChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQ.CheckedChanged
        cboGoldQ.Enabled = chkGoldQ.Checked
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

    Private Sub chkItemCat_CheckedChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCat.CheckedChanged
        'cboItemCat.Enabled = chkItemCat.Checked
        If (chkItemCat.Checked) Then
            cboItemCat.Enabled = True
            ' cboItemName.Enabled = True
            chkItemName.Enabled = True
        Else
            cboItemCat.Enabled = False
            chkItemName.Enabled = False
            chkItemName.Checked = False
            cboItemName.Enabled = False

        End If
    End Sub


    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtDetail As New DataTable
        Dim PurchaseGemID As String = ""
        Dim PurchaseDetailID As String = ""
        Dim PurchaseHeaderID As String = ""
        Dim PurchaseVoucherNo As String = ""
        Dim ToDate As String = ""

        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If radDaily.Checked Then
            Dim SumofTotalTK As Decimal = "0.0"
            Dim AllTotalTK(0) As ReportParameter
            Dim FromDate(0) As ReportParameter

            If chkLocation.Checked Then
                dt = _PurchaseInvoice.GetPurchaseInvoiceDailyTransactionReport(dtpFromDate.Value.Date, GetFilterString)
            Else
                MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
            End If

            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    SumofTotalTK += dt.Rows(i).Item("TotalTK")
                    If Not IsDBNull(dt.Rows(i).Item("PurchaseHeaderID")) Then

                        If dt.Rows(i).Item("PurchaseHeaderID") <> PurchaseHeaderID Then
                            dt.Rows(i).Item("AllTotalAmount") = dt.Rows(i).Item("AllTotalAmount")
                            dt.Rows(i).Item("PurchasePaidAmount") = dt.Rows(i).Item("AllTotalAmount") - dt.Rows(i).Item("AllAddOrSub")
                        Else
                            dt.Rows(i).Item("AllTotalAmount") = 0
                            dt.Rows(i).Item("PurchasePaidAmount") = 0
                        End If

                        PurchaseHeaderID = dt.Rows(i).Item("PurchaseHeaderID")
                    Else
                        dt.Rows(i).Item("AllTotalAmount") = dt.Rows(i).Item("AllTotalAmount")
                        dt.Rows(i).Item("PurchasePaidAmount") = dt.Rows(i).Item("AllTotalAmount") - dt.Rows(i).Item("AllAddOrSub")
                    End If
                Next
            End If

            If dt.Rows.Count() = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                RptViewer.Reset()
            End If

            RptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_NewOnlyPurchaseSummaryReport.rdlc"
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
            Dim TitleChange(0) As ReportParameter

            If chkIsChange.Checked Then
                TitleChange(0) = New ReportParameter("TitleChange", "ပြန်လဲရွှေ စာရင်း")
                RptViewer.LocalReport.SetParameters(TitleChange)
            ElseIf chkIsPurchase.Checked Then
                TitleChange(0) = New ReportParameter("TitleChange", "ပစ္စည်းအ၀ယ် စာရင်း")
                RptViewer.LocalReport.SetParameters(TitleChange)
            Else
                TitleChange(0) = New ReportParameter("TitleChange", "ပစ္စည်းအ၀ယ်၊အလဲ စာရင်း")
                RptViewer.LocalReport.SetParameters(TitleChange)
            End If
            AllTotalTK(0) = New ReportParameter("AllTotalTK", SumofTotalTK)
            RptViewer.LocalReport.SetParameters(AllTotalTK)

            FromDate(0) = New ReportParameter("FromDate", dtpFromDate.Value.Date)
            RptViewer.LocalReport.SetParameters(FromDate)
        Else

            If radGems.Checked = False And radLooseDiamond.Checked = False Then
                If radDetail.Checked Then
                    If chkLocation.Checked Then
                        dt = _PurchaseInvoice.GetPurchaseInvoiceForBarcodeReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
                    End If
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If Not IsDBNull(dt.Rows(i).Item("PurchaseGemID")) Then
                                If dt.Rows(i).Item("PurchaseDetailID") <> PurchaseDetailID Then
                                    dt.Rows(i).Item("ItemTotalAmount") = dt.Rows(i).Item("ItemTotalAmount") + dt.Rows(i).Item("NetAmount")
                                Else
                                    dt.Rows(i).Item("ItemTotalAmount") = dt.Rows(i).Item("ItemTotalAmount") + 0
                                End If
                                PurchaseGemID = dt.Rows(i).Item("PurchaseGemID")
                                dtDetail = _PurchaseInvoice.GetPurchaseGemDataByPurchaseGemID(PurchaseGemID)
                                For j As Integer = 0 To dtDetail.Rows.Count - 1
                                    PurchaseDetailID = dtDetail.Rows(j).Item("PurchaseDetailID")
                                Next

                            Else
                                dt.Rows(i).Item("ItemTotalAmount") = dt.Rows(i).Item("NetAmount") + 0
                            End If

                        Next
                    End If
                Else
                    If chkLocation.Checked Then
                        dt = _PurchaseInvoice.GetPurchaseInvoiceReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
                    End If
                End If

            ElseIf radGems.Checked = True Then
                If chkLocation.Checked Then
                    If radSummary.Checked Then
                        dt = _PurchaseInvoice.GetPurchaseInvoiceGemReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        dt = _PurchaseInvoice.GetPurchaseInvoiceForBarcodeReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                    End If
                Else
                    MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
                End If
            Else
                If chkLocation.Checked Then
                    If radSummary.Checked Then
                        dt = _PurchaseInvoice.GetPurchaseInvoiceLooseDiamondReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        dt = _PurchaseInvoice.GetPurchaseInvoiceForLooseDiamondReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                    End If
                Else
                    MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
                End If
            End If

            If dt.Rows.Count() = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                RptViewer.Reset()
            End If

            RptViewer.Reset()
            If radGems.Checked = False And radLooseDiamond.Checked = False Then
                RptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_PurchaseInvoiceBy_Summary.rdlc", "UI.rpt_PurchaseInvoice_Detail.rdlc")
            ElseIf radGems.Checked = True Then
                RptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_PurchaseInvoice_GemSummary.rdlc", "UI.rpt_PurchaseInvoiceByGems_Detail.rdlc")
            Else
                RptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_PurchaseInvoice_LooseDiamondSummary.rdlc", "UI.rpt_PurchaseInvoiceByLooseDiamond_Detail.rdlc")
            End If

            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))

            If radDetail.Checked And radGems.Checked = False And radLooseDiamond.Checked = False Then
                Dim GoldTG(0) As ReportParameter
                Dim GemsTG(0) As ReportParameter
                Dim TotalTG(0) As ReportParameter
                Dim GoldTK(0) As ReportParameter
                Dim GemsTK(0) As ReportParameter
                Dim TotalTK(0) As ReportParameter
                Dim PWasteTK(0) As ReportParameter
                Dim PWasteTG(0) As ReportParameter
                Dim QTY(0) As ReportParameter
                Dim ItemAmount(0) As ReportParameter

                Dim obj As New CommonInfo.PurchaseDetailInfo
                obj = _PurchaseInvoice.GetPurchaseInvoiceDetailForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                GoldTG(0) = New ReportParameter("GoldTG", obj.GoldTG)
                RptViewer.LocalReport.SetParameters(GoldTG)

                GoldTK(0) = New ReportParameter("GoldTK", obj.GoldTK)
                RptViewer.LocalReport.SetParameters(GoldTK)

                GemsTG(0) = New ReportParameter("GemsTG", obj.TotalGemTG)
                RptViewer.LocalReport.SetParameters(GemsTG)

                GemsTK(0) = New ReportParameter("GemsTK", obj.TotalGemTK)
                RptViewer.LocalReport.SetParameters(GemsTK)

                TotalTG(0) = New ReportParameter("TotalTG", obj.TotalTG)
                RptViewer.LocalReport.SetParameters(TotalTG)

                TotalTK(0) = New ReportParameter("TotalTK", obj.TotalTK)
                RptViewer.LocalReport.SetParameters(TotalTK)

                PWasteTG(0) = New ReportParameter("PWasteTG", obj.PWasteTG)
                RptViewer.LocalReport.SetParameters(PWasteTG)

                PWasteTK(0) = New ReportParameter("PWasteTK", obj.PWasteTK)
                RptViewer.LocalReport.SetParameters(PWasteTK)

                ItemAmount(0) = New ReportParameter("ItemAmount", obj.TotalAmount)
                RptViewer.LocalReport.SetParameters(ItemAmount)

                QTY(0) = New ReportParameter("QTY", obj.QTY)
                RptViewer.LocalReport.SetParameters(QTY)
            End If

            If radDetail.Checked Then
                Dim dtTotal As New DataTable
                Dim TotalAmount(0) As ReportParameter
                Dim NetAmount(0) As ReportParameter
                Dim PaidAmount(0) As ReportParameter
                Dim TotalAmt As Integer = 0
                Dim NetAmt As Integer = 0
                Dim PaidAmt As Integer = 0
                dtTotal = _PurchaseInvoice.GetPurchaseInvoiceReportForTotalAmount(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                If dtTotal.Rows.Count() > 0 Then
                    For Each dr As DataRow In dtTotal.Rows
                        TotalAmt += dr.Item("TotalAmount")
                        NetAmt += dr.Item("NetAmount")
                        PaidAmt += dr.Item("PaidAmount")
                    Next
                Else
                    TotalAmt = 0
                    NetAmt = 0
                    PaidAmt = 0
                End If
                TotalAmount(0) = New ReportParameter("TotalAmount", TotalAmt)
                RptViewer.LocalReport.SetParameters(TotalAmount)

                NetAmount(0) = New ReportParameter("NetAmount", NetAmt)
                RptViewer.LocalReport.SetParameters(NetAmount)

                PaidAmount(0) = New ReportParameter("PaidAmount", PaidAmt)
                RptViewer.LocalReport.SetParameters(PaidAmount)
            End If

            Dim Title(0) As ReportParameter
            If radDetail.Checked Then
                If (radShopStock.Checked) Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်ရွှေပြန်ဝယ်ခြင်း စာရင်း")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf radPurchase.Checked Then
                    Title(0) = New ReportParameter("Title", "အပြင်ရွှေပြန်ဝယ်ခြင်း စာရင်း")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "ပစ္စည်းအဝယ် စာရင်း")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            Else
                If (radShopStock.Checked) Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်ရွှေပြန်ဝယ်ခြင်း စာရင်းချုပ်")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf radPurchase.Checked Then
                    Title(0) = New ReportParameter("Title", "အပြင်ရွှေပြန်ဝယ်ခြင်း စာရင်းချုပ်")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "ပစ္စည်းအဝယ် စာရင်းချုပ်")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            End If
            Dim IsSelect As Boolean = False
            Dim Selected(0) As ReportParameter
            If chkGoldQ.Checked Or chkItemCat.Checked Or chkGemsCategory.Checked Or radShopStock.Checked Or radPurchase.Checked Then
                IsSelect = True
            End If
            If radDetail.Checked Then
                Selected(0) = New ReportParameter("Selected", IsSelect)
                RptViewer.LocalReport.SetParameters(Selected)
            End If
        End If

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        RptViewer.LocalReport.SetParameters(G_PToY)
        RptViewer.RefreshReport()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkGemsCategory_CheckedChanged(sender As Object, e As EventArgs) Handles chkGemsCategory.CheckedChanged
        cboGemCategory.Enabled = chkGemsCategory.Checked
    End Sub

    Private Sub radGems_CheckedChanged(sender As Object, e As EventArgs) Handles radGems.CheckedChanged
        If radGems.Checked Then
            chkGemsCategory.Visible = True
            cboGemCategory.Visible = True
            cboGemCategory.Enabled = False
            chkItemCat.Visible = False
            cboItemCat.Visible = False
            cboItemCat.Enabled = False
            chkGoldQ.Checked = False
            chkGoldQ.Enabled = False
            chkGemsCategory.Checked = False
            chkItemCat.Checked = False
            If radDaily.Checked Then
                chkGemsCategory.Enabled = False
            Else
                chkGemsCategory.Enabled = True
            End If
        Else
            chkGemsCategory.Visible = False
            cboGemCategory.Visible = False
            chkItemCat.Visible = True
            cboItemCat.Visible = True
            cboItemCat.Enabled = False
            chkGemsCategory.Checked = False
            chkItemCat.Checked = False
            If radDaily.Checked Then
                chkItemCat.Enabled = False
                chkGoldQ.Enabled = False
            Else
                chkItemCat.Enabled = True
                chkGoldQ.Enabled = True
            End If
        End If
    End Sub
    Private Sub radLooseDiamond_CheckedChanged(sender As Object, e As EventArgs) Handles radLooseDiamond.CheckedChanged
        If radLooseDiamond.Checked Then
            chkGemsCategory.Visible = True
            cboGemCategory.Visible = True
            cboGemCategory.Enabled = False
            chkItemCat.Visible = False
            cboItemCat.Visible = False
            cboItemCat.Enabled = False
            chkGoldQ.Checked = False
            chkGoldQ.Enabled = False
            chkGemsCategory.Checked = False
            chkItemCat.Checked = False
            If radDaily.Checked Then
                chkGemsCategory.Enabled = False
            Else
                chkGemsCategory.Enabled = True
            End If
        Else
            chkGemsCategory.Visible = False
            cboGemCategory.Visible = False
            chkItemCat.Visible = True
            cboItemCat.Visible = True
            cboItemCat.Enabled = False
            chkGemsCategory.Checked = False
            chkItemCat.Checked = False
            If radDaily.Checked Then
                chkItemCat.Enabled = False
                chkGoldQ.Enabled = False
            Else
                chkItemCat.Enabled = True
                chkGoldQ.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("PurchaseInvoiceReport")
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

    Private Sub cboItemCat_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboItemCat.SelectedValueChanged
        Dim itemid As String
        itemid = cboItemCat.SelectedValue
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

    Private Sub radDaily_CheckedChanged(sender As Object, e As EventArgs) Handles radDaily.CheckedChanged
        If radDaily.Checked Then
            dtpToDate.Enabled = False
            chkGoldQ.Checked = False
            chkGoldQ.Enabled = False
            If radGems.Checked = True Then
                chkGemsCategory.Checked = False
                chkGemsCategory.Enabled = False
            Else
                chkItemCat.Checked = False
                chkItemCat.Enabled = False
            End If
           
            chkItemName.Checked = False
            chkItemName.Enabled = False
            'radShopStock.Enabled = False
            'radPurchase.Enabled = False
        Else
            dtpToDate.Enabled = True
            chkGoldQ.Enabled = True
            If radGems.Checked = True Then
                chkGemsCategory.Enabled = True
            Else
                chkItemCat.Enabled = True
            End If
            'radShopStock.Enabled = True
            'radPurchase.Enabled = True
        End If
    End Sub

    'Private Sub chkIsChange_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsChange.CheckedChanged
    '    If chkIsChange.Checked Then
    '        dtpToDate.Enabled = False
    '        radDaily.Enabled = False
    '        radGems.Enabled = False
    '        radDetail.Enabled = False
    '    Else
    '        dtpToDate.Enabled = True
    '        radDaily.Enabled = True
    '        radGems.Enabled = True
    '        radDetail.Enabled = True
    '    End If
    'End Sub

    Private Sub chkIsChange_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsChange.CheckedChanged
        If chkIsChange.Checked Then
            chkIsPurchase.Checked = False
        End If
    End Sub

    Private Sub chkIsPurchase_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsPurchase.CheckedChanged
        If chkIsPurchase.Checked Then
            chkIsChange.Checked = False
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
        'Dim dt As New DataTable
        'Dim DataItem As DataRow

        'dt = _CustomerController.GetAllCustomerAutoCompleteData()
        'DataItem = DirectCast(SearchData.FindFast(dt, "Customer List"), DataRow)
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
        Dim dt As New DataTable
        Dim dtReturn As DataTable
        dt = _CustomerController.GetAllCustomerAutoCompleteData()
        'DataItem = DirectCast(SearchData.FindFast(dt, "Customer List"), DataRow)
        'MyBase.New(New SearchControlHost(Name, True, Header, cri))
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

End Class