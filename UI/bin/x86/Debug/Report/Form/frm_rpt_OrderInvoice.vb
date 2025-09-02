Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo

Public Class frm_rpt_OrderInvoice
    Private _CustomerID As String = ""
    Private _OrderDA As OrderInvoice.IOrderInvoiceController = Factory.Instance.CreateOrderInvoiceController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _GoldQ As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCat As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName

    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
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

    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(CboLocation, e)
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(CboLocation, "")
    End Sub

#End Region

    Private Sub frm_rpt_SaleOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetCombo()
        cboGoldQuality.Enabled = False
        cboCategory.Enabled = False
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
        chkCancel.Enabled = False
        txtCustomerCode.Enabled = False
        txtCustomerName.Enabled = False
        btnCustomer.Enabled = False
        chkCancel.Enabled = True
        chkByDueDate.Checked = False
        chkByDueDate.Enabled = True
        Me.rpt_OrderInvoice.RefreshReport()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Dim dt As New DataTable
        Dim dtDetail As New DataTable
        Dim OrderInvoiceDetailID As String = ""
        Dim OrderInvoiceDetailGemsID As String = ""
        Dim OrderInvoiceID As String = ""
        Dim criStr As String = ""

        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If
        If ChkLocation.Checked Then
            If radDetail.Checked Then
                If optOrderOnly.Checked Then
                    If chkByDueDate.Checked Then
                        criStr = " ORDER BY H.DueDate ASC  "
                    Else
                        criStr = " ORDER BY H.OrderDate DESC, H.OrderInvoiceID DESC  "
                    End If
                    dt = _OrderDA.GetOrderInvoiceDetailReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, optReturn.Checked, GetFilterString() & criStr)
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item("OrderInvoiceID") <> OrderInvoiceID Then
                                dt.Rows(i).Item("TotalAdvanceAmount") = dt.Rows(i).Item("AdvanceAmount")
                                dt.Rows(i).Item("TotalNetAmount") = dt.Rows(i).Item("AllTotalAmount") + dt.Rows(i).Item("AllAddOrSub")
                            Else
                                dt.Rows(i).Item("TotalAdvanceAmount") = 0
                                dt.Rows(i).Item("TotalNetAmount") = 0
                            End If
                            OrderInvoiceID = dt.Rows(i).Item("OrderInvoiceID")
                        Next
                    End If
                Else
                    dt = _OrderDA.GetOrderReturnDetailReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, optReturn.Checked, GetFilterString)

                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If Not IsDBNull(dt.Rows(i).Item("OrderReturnGemID")) Then

                                If dt.Rows(i).Item("OrderInvoiceDetailID") <> OrderInvoiceDetailID Then
                                    dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + dt.Rows(i).Item("ItemNetAmount")
                                Else
                                    dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + 0
                                End If

                                OrderInvoiceDetailGemsID = dt.Rows(i).Item("OrderReturnGemID")
                                dtDetail = _OrderDA.GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID(OrderInvoiceDetailGemsID)
                                For j As Integer = 0 To dtDetail.Rows.Count - 1
                                    OrderInvoiceDetailID = dtDetail.Rows(j).Item("OrderInvoiceDetailID")
                                Next

                            Else
                                dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("ItemNetAmount") + 0
                            End If

                        Next
                    End If
                End If
            Else
                If optOrderOnly.Checked Then
                    dt = _OrderDA.GerOrderSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, optReturn.Checked, GetFilterString)
                Else
                    dt = _OrderDA.GetOrderReturnSummaryReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, optReturn.Checked, GetFilterString)
                End If
            End If
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If dt.Rows.Count() = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
            rpt_OrderInvoice.Reset()
        End If
        rpt_OrderInvoice.Reset()
        If optReturn.Checked Then
            rpt_OrderInvoice.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_OrderInvoice_ReturnSummary.rdlc", "UI.rpt_OrderInvoice_ReturnDetail.rdlc")
            rpt_OrderInvoice.LocalReport.DataSources.Clear()
            rpt_OrderInvoice.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_OrderInvoice", dt))
        Else
            rpt_OrderInvoice.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_OrderInvoice_Summary.rdlc", "UI.rpt_OrderInvoice_Detail.rdlc")
            rpt_OrderInvoice.LocalReport.DataSources.Clear()
            rpt_OrderInvoice.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoice_dtOrderInvoice", dt))
        End If

        If radDetail.Checked Then
            If optReturn.Checked Then
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
                Dim ItemAmount(0) As ReportParameter

                Dim TotalAmount(0) As ReportParameter
                Dim NetAmount(0) As ReportParameter
                Dim PaidAmount(0) As ReportParameter
                Dim BalanceAmount(0) As ReportParameter

                Dim Quantity(0) As ReportParameter
                Dim objDetial As New CommonInfo.OrderInvoiceDetailInfo
                objDetial = _OrderDA.GetOrderInvoiceDetailForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                ItemTG(0) = New ReportParameter("ItemTG", objDetial.ItemTG)
                rpt_OrderInvoice.LocalReport.SetParameters(ItemTG)

                GoldTG(0) = New ReportParameter("GoldTG", objDetial.GoldTG)
                rpt_OrderInvoice.LocalReport.SetParameters(GoldTG)

                GemsTG(0) = New ReportParameter("GemsTG", objDetial.GemsTG)
                rpt_OrderInvoice.LocalReport.SetParameters(GemsTG)

                WasteTG(0) = New ReportParameter("WasteTG", objDetial.WasteTG)
                rpt_OrderInvoice.LocalReport.SetParameters(WasteTG)

                TotalTG(0) = New ReportParameter("TotalTG", objDetial.TotalTG)
                rpt_OrderInvoice.LocalReport.SetParameters(TotalTG)

                ItemTK(0) = New ReportParameter("ItemTK", objDetial.ItemTK)
                rpt_OrderInvoice.LocalReport.SetParameters(ItemTK)

                GoldTK(0) = New ReportParameter("GoldTK", objDetial.GoldTK)
                rpt_OrderInvoice.LocalReport.SetParameters(GoldTK)

                GemsTK(0) = New ReportParameter("GemsTK", objDetial.GemsTK)
                rpt_OrderInvoice.LocalReport.SetParameters(GemsTK)

                WasteTK(0) = New ReportParameter("WasteTK", objDetial.WasteTK)
                rpt_OrderInvoice.LocalReport.SetParameters(WasteTK)

                TotalTK(0) = New ReportParameter("TotalTK", objDetial.TotalTK)
                rpt_OrderInvoice.LocalReport.SetParameters(TotalTK)

                ItemAmount(0) = New ReportParameter("ItemAmount", objDetial.ItemAmount)
                rpt_OrderInvoice.LocalReport.SetParameters(ItemAmount)


                QTY(0) = New ReportParameter("QTY", objDetial.QTY)
                rpt_OrderInvoice.LocalReport.SetParameters(QTY)

                Dim dtTotal As New DataTable
                Dim TotalAmt As Integer = 0
                Dim NetAmt As Integer = 0
                Dim PaidAmt As Integer = 0
                Dim BalanceAmt As Integer = 0
                dtTotal = _OrderDA.GetOrderReturnReportForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                If dtTotal.Rows.Count() > 0 Then
                    For Each dr As DataRow In dtTotal.Rows
                        TotalAmt += dr.Item("TotalAmount")
                        NetAmt += dr.Item("NetAmount")
                        BalanceAmt += dr.Item("BalanceAmount")
                        PaidAmt += dr.Item("PaidAmount")
                    Next
                Else
                    TotalAmt = 0
                    NetAmt = 0
                    PaidAmt = 0
                    BalanceAmt = 0
                End If
                TotalAmount(0) = New ReportParameter("TotalAmount", TotalAmt)
                rpt_OrderInvoice.LocalReport.SetParameters(TotalAmount)

                NetAmount(0) = New ReportParameter("NetAmount", NetAmt)
                rpt_OrderInvoice.LocalReport.SetParameters(NetAmount)

                PaidAmount(0) = New ReportParameter("PaidAmount", PaidAmt)
                rpt_OrderInvoice.LocalReport.SetParameters(PaidAmount)

                BalanceAmount(0) = New ReportParameter("BalanceAmount", BalanceAmt)
                rpt_OrderInvoice.LocalReport.SetParameters(BalanceAmount)

                Dim IsSelect As Boolean = False
                Dim Selected(0) As ReportParameter
                If chkCustomerName.Checked Or chkItemCategory.Checked Or chkGoldQuality.Checked Then
                    IsSelect = True
                End If
                Selected(0) = New ReportParameter("Selected", IsSelect)
                rpt_OrderInvoice.LocalReport.SetParameters(Selected)
            Else
                Dim PayGoldTG(0) As ReportParameter
                Dim PayGoldTK(0) As ReportParameter
                Dim EstimateGoldTG(0) As ReportParameter
                Dim EstimateGoldTK(0) As ReportParameter
                Dim EstimateGemTG(0) As ReportParameter
                Dim EstimateGemTK(0) As ReportParameter
                Dim QTY(0) As ReportParameter
                Dim AdvanceAmount(0) As ReportParameter
                Dim NetAmount(0) As ReportParameter

                Dim obj As New CommonInfo.OrderInvoiceInfo
                obj = _OrderDA.GetOrderInvoiceReportForTotal(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

                PayGoldTK(0) = New ReportParameter("PayGoldTK", obj.PayGoldTK)
                rpt_OrderInvoice.LocalReport.SetParameters(PayGoldTK)

                EstimateGoldTK(0) = New ReportParameter("EstimateGoldTK", obj.EstimateGoldTK)
                rpt_OrderInvoice.LocalReport.SetParameters(EstimateGoldTK)

                EstimateGoldTG(0) = New ReportParameter("EstimateGoldTG", obj.EstimateGoldTG)
                rpt_OrderInvoice.LocalReport.SetParameters(EstimateGoldTG)

                EstimateGemTK(0) = New ReportParameter("EstimateGemTK", obj.GemsTK)
                rpt_OrderInvoice.LocalReport.SetParameters(EstimateGemTK)

                EstimateGemTG(0) = New ReportParameter("EstimateGemTG", obj.GemsTG)
                rpt_OrderInvoice.LocalReport.SetParameters(EstimateGemTG)

                QTY(0) = New ReportParameter("QTY", obj.QTY)
                rpt_OrderInvoice.LocalReport.SetParameters(QTY)

                NetAmount(0) = New ReportParameter("NetAmount", obj.NetAmount)
                rpt_OrderInvoice.LocalReport.SetParameters(NetAmount)

                AdvanceAmount(0) = New ReportParameter("AdvanceAmount", obj.AdvanceAmount)
                rpt_OrderInvoice.LocalReport.SetParameters(AdvanceAmount)

                Dim IsSelect As Boolean = False
                Dim Selected(0) As ReportParameter
                If chkCustomerName.Checked Or chkItemCategory.Checked Or chkGoldQuality.Checked Then
                    IsSelect = True
                End If
                Selected(0) = New ReportParameter("Selected", IsSelect)
                rpt_OrderInvoice.LocalReport.SetParameters(Selected)
            End If
        End If

        Dim TitleType As String = ""
        If radAll.Checked Then
            TitleType = "(All)"
        ElseIf radStock.Checked Then
            TitleType = "(ရွှေထည်)"
        ElseIf radDiamondStock.Checked Then
            TitleType = "(စိန်ထည်)"
        End If

        Dim CTitle(0) As ReportParameter
        CTitle(0) = New ReportParameter("CTitle", TitleType)
        rpt_OrderInvoice.LocalReport.SetParameters(CTitle)

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_OrderInvoice.LocalReport.SetParameters(G_PToY)
        rpt_OrderInvoice.RefreshReport()
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

    Private Function GetFilterString() As String
        GetFilterString = ""
        If radDetail.Checked Then
            If optOrderOnly.Checked Then
                'GetFilterString = " Where H.IsDelete=0  AND H.OrderDate BETWEEN '" & CDate(dtpFromDate.Value.Date & " 00:00:00") & "' And '" & CDate(dtpToDate.Value.Date & " 23:59:59") & "'"
                GetFilterString = " Where H.IsDelete=0  AND H.OrderDate BETWEEN @FromDate and @ToDate "
            Else
                'GetFilterString = " Where H.IsDelete=0 and H.ReturnDate  BETWEEN '" & CDate(dtpFromDate.Value.Date & " 00:00:00") & "' And '" & CDate(dtpToDate.Value.Date & " 23:59:59") & "'"
                GetFilterString = " Where H.IsDelete=0 and H.ReturnDate  BETWEEN @FromDate and @ToDate "
            End If
        Else
            If optReturn.Checked Then
                'GetFilterString = " Where H.IsDelete=0 and H.ReturnDate BETWEEN '" & CDate(dtpFromDate.Value.Date & " 00:00:00") & "' And '" & CDate(dtpToDate.Value.Date & " 23:59:59") & "'"
                GetFilterString = " Where H.IsDelete=0 and H.ReturnDate BETWEEN @FromDate and @ToDate "
            Else
                'GetFilterString = " Where H.IsDelete=0 AND H.OrderDate BETWEEN '" & CDate(dtpFromDate.Value.Date & " 00:00:00") & "' And '" & CDate(dtpToDate.Value.Date & " 23:59:59") & "'"
                GetFilterString = " Where H.IsDelete=0 AND H.OrderDate BETWEEN @FromDate and @ToDate "
            End If
        End If



        If (chkCustomerName.Checked) Then
            If optOrderOnly.Checked Then
                'GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
                GetFilterString += " AND H.CustomerID  In (" & _CustomerID & ")"
            Else
                'GetFilterString += " And OH.CustomerID  = '" & _CustomerID & "'"
                GetFilterString += " AND OH.CustomerID  In (" & _CustomerID & ")"
            End If
        End If
        If optOrderOnly.Checked Then
            If (chkGoldQuality.Checked) Then
                GetFilterString += " And D.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
            End If

            If (chkItemCategory.Checked) Then
                GetFilterString += " And D.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
            End If

            If (chkItemName.Checked) Then
                GetFilterString += " And D.ItemNameID = '" & cboItemName.SelectedValue & "'"
            End If
            If chkCancel.Checked Then
                GetFilterString += " And H.IsCancel =1 "
            End If

            If radStock.Checked Then
                GetFilterString += " And D.IsDiamond =0"
            End If
            If radDiamondStock.Checked Then
                GetFilterString += " And D.IsDiamond =1"
            End If
        Else
            If (chkGoldQuality.Checked) Then
                GetFilterString += " And F.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
            End If

            If (chkItemCategory.Checked) Then
                GetFilterString += " And F.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
            End If

            If (chkItemName.Checked) Then
                GetFilterString += " And F.ItemNameID = '" & cboItemName.SelectedValue & "'"
            End If

            If radStock.Checked Then
                GetFilterString += " And F.IsDiamond =0"
            End If
            If radDiamondStock.Checked Then
                GetFilterString += " And F.IsDiamond =1"
            End If
        End If
        If ChkLocation.Checked Then
            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
    End Function
    Private Sub GetCombo()

        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = _GoldQ.GetAllGoldQuality().DefaultView

        'cboLocation.DisplayMember = "Location"
        'cboLocation.ValueMember = "@LocationID"
        'cboLocation.DataSource = _Location.GetAllLocationList().DefaultView
        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboCategory.DisplayMember = "ItemCategory_"
        cboCategory.ValueMember = "@ItemCategoryID"
        cboCategory.DataSource = _ItemCat.GetAllItemCategory().DefaultView
    End Sub

    Private Sub chkGoldQuality_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQuality.CheckedChanged
        If (chkGoldQuality.Checked) Then
            cboGoldQuality.Enabled = True
        Else
            cboGoldQuality.Enabled = False
        End If
    End Sub

    'Private Sub chkItemCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCategory.CheckedChanged
    '    If (chkItemCategory.Checked) Then
    '        cboCategory.Enabled = True
    '    Else
    '        cboCategory.Enabled = False
    '    End If
    'End Sub
    Private Sub chkCustomerName_CheckedChanged(sender As Object, e As EventArgs) Handles chkCustomerName.CheckedChanged
        If (chkCustomerName.Checked) Then
            txtCustomerName.Enabled = True
            txtCustomerCode.Enabled = True
            btnCustomer.Enabled = True
        Else
            txtCustomerName.Enabled = False
            txtCustomerCode.Enabled = False
            btnCustomer.Enabled = False
            txtCustomerCode.Text = ""
            txtCustomerName.Text = ""
            _CustomerID = ""
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLocation.CheckedChanged
    '    cboLocation.Enabled = chkLocation.Checked
    'End Sub
    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLocation.CheckedChanged
        If chkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub

    Private Sub radDetail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radDetail.CheckedChanged
        If (radDetail.Checked) Then
            If (optReturn.Checked) Then
                chkCancel.Enabled = False
                chkCancel.Checked = False
                chkByDueDate.Enabled = False
                chkByDueDate.Checked = False
            Else
                chkCancel.Enabled = True
                chkByDueDate.Enabled = True
            End If
        End If
    End Sub

    Private Sub optReturn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optReturn.CheckedChanged
        If (optReturn.Checked) Then
            chkCancel.Enabled = False
            chkCancel.Checked = False
            chkItemCategory.Enabled = True
            chkItemName.Enabled = False

        Else
            chkCancel.Enabled = True

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
        '        txtCustomerName.Text = DataItem("CustomerName_")
        '    Else
        '        MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
        '        _CustomerID = ""
        '        txtCustomerCode.Text = ""
        '        txtCustomerName.Text = ""
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
                txtCustomerName.Text = ""

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
                    With objCustomer
                        txtCustomerName.Text = .CustomerName

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
                '_CustomerID = ""
                txtCustomerName.Text = ""
            End If
            _IsCustomerCode = False
        End If
    End Sub

    Private Sub optOrderOnly_CheckedChanged(sender As Object, e As EventArgs) Handles optOrderOnly.CheckedChanged
        If optOrderOnly.Checked Then
            chkCancel.Enabled = True
            chkByDueDate.Enabled = True
            chkItemName.Checked = False
            chkItemName.Enabled = False
            cboItemName.Enabled = False
        Else
            chkByDueDate.Enabled = False
            chkItemCategory.Enabled = True
            chkItemName.Enabled = True
            chkCancel.Checked = False
            chkCancel.Enabled = False
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

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("OrderReport")
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