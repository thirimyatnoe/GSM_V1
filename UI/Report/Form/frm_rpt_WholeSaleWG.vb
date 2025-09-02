Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo

Public Class frm_rpt_WholeSaleWG

    Private _FromGoldTG As Decimal = 0.0
    Private _ToGoldTG As Decimal = 0.0
    Private _CustType As String = ""
    Private _CustomerID As String
    Private _CustomerCode As String = ""

    Private objWSaleInvoiceController As WholeSaleInvoice.IWholeSaleInvoiceController = Factory.Instance.CreateWholeSaleInvoiceController
    Private objWSaleReturnController As WholeSaleReturn.IWholeSaleReturnController = Factory.Instance.CreateWholeSaleReturnController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _GetKeyword As BusinessRule.Keyword.IKeywordController = Factory.Instance.CreateKeyWordController
    Private objCustomerController As Customer.ICustomerController = Factory.Instance.CreateCustomerController
    Private objConsignmentSaleController As ConsignmentSale.IConsignmentSaleController = Factory.Instance.CreateConsignmentSaleController
    Dim LocationID As String
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
    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboLocation.KeyUp
        AutoCompleteCombo_KeyUp(cboLocation, e)
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.Leave
        AutoCompleteCombo_Leave(cboLocation, "")
    End Sub
#End Region

    Private Sub frm_rpt_SaleInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()
        cboLocation.Enabled = False
        cboCategory.Enabled = False
        cboItemName.Enabled = False
        cboGoldQuality.Enabled = False
        txtCustomerCode.Enabled = False
        txtCustomerName.Enabled = False
        btnCustomer.Enabled = False
        chkLocation.Checked = True
        cboLocation.Enabled = True

        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
    Private Sub cboLocation_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboLocation.SelectedValueChanged

        LocationID = CboLocation.SelectedValue
        cboCategory.SelectedValue = ""
        cboGoldQuality.SelectedValue = ""
        Get_Combos()


    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtAmt As New DataTable
        Dim curRDLC As String = ""

        If chkLocation.Checked Then

            If optSale.Checked = True Then
                dt = objWSaleInvoiceController.GetWholeSaleReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                dtAmt = objWSaleInvoiceController.GetWholeSaleReportAmount(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            ElseIf optPay.Checked = True Then
                dt = objWSaleInvoiceController.GetWholeSaleReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                dtAmt = objWSaleInvoiceController.GetWholeSaleReportAmount(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            ElseIf optConsignBalance.Checked = True Then
                dt = objWSaleInvoiceController.GetConsignBalanceReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                dtAmt = objWSaleInvoiceController.GetConsignBalanceReportAmount(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            ElseIf optSaleReturn.Checked Then
                dt = objWSaleReturnController.GetWholeSaleReturnReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                dtAmt = objWSaleReturnController.GetWholeSaleReturnReportAmount(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            ElseIf optPayReturn.Checked Then
                dt = objWSaleReturnController.GetWholeSaleReturnReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                dtAmt = objWSaleReturnController.GetWholeSaleReturnReportAmount(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            ElseIf optConsignmentSale.Checked Then
                dt = objConsignmentSaleController.GetConsignmentSaleReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                dtAmt = objConsignmentSaleController.GetConsignmentSaleReportAmount(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            End If
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If
        'End If
        If dt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If
        If optSale.Checked Then
            If radSummaryByDate.Checked Then
                curRDLC = "UI.rpt_WholeSaleSummaryByDate.rdlc"
            Else
                curRDLC = "UI.rpt_WholeSaleReport.rdlc"
            End If
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_WholeSaleReport_Summary.rdlc", curRDLC)
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSale_WholeSale", dt))

            If radDetail.Checked Then
                Dim dtP As New DataTable
                Dim PAmt As Integer = 0
                Dim TAmt As Integer = 0
                Dim NAmt As Integer = 0
                Dim DAmt As Integer = 0
                dtP = objWSaleInvoiceController.GetWholeSaleTotalPaidAmtReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, "WholeSaleInvoice", GetFilterString)
                Dim PaidAmt(0) As ReportParameter
                Dim NetAmt(0) As ReportParameter
                Dim TotalAmt(0) As ReportParameter
                Dim DisAmount(0) As ReportParameter

                If dtP.Rows.Count > 0 Then
                    For Each dr As DataRow In dtP.Rows
                        PAmt += IIf(IsDBNull(dr.Item("PaidAmount")), 0, dr.Item("PaidAmount"))
                        NAmt += IIf(IsDBNull(dr.Item("NetAmount")), 0, dr.Item("NetAmount"))
                        TAmt += IIf(IsDBNull(dr.Item("TotalPayment")), 0, dr.Item("TotalPayment"))
                        DAmt += IIf(IsDBNull(dr.Item("Discount")), 0, dr.Item("Discount"))
                    Next

                    PaidAmt(0) = New ReportParameter("PaidAmt", PAmt)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", NAmt)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", TAmt)
                    rptViewer.LocalReport.SetParameters(TotalAmt)

                    DisAmount(0) = New ReportParameter("DisAmount", DAmt)
                    rptViewer.LocalReport.SetParameters(DisAmount)

                Else
                    PaidAmt(0) = New ReportParameter("PaidAmt", 0)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", 0)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", 0)
                    rptViewer.LocalReport.SetParameters(TotalAmt)

                    DisAmount(0) = New ReportParameter("DisAmount", 0)
                    rptViewer.LocalReport.SetParameters(DisAmount)
                End If

               

            End If
            If radSummaryByDate.Checked Then
                Dim NetAmount As Integer = 0
                Dim AllNetAmount(0) As ReportParameter
                If dtAmt.Rows.Count > 0 Then
                    For Each dr As DataRow In dtAmt.Rows
                        NetAmount += IIf(IsDBNull(dr.Item("WSNetAmount")), 0, dr.Item("WSNetAmount"))
                    Next
                    AllNetAmount(0) = New ReportParameter("AllNetAmount", NetAmount)
                    rptViewer.LocalReport.SetParameters(AllNetAmount)
                End If

            End If
        End If

        If optPay.Checked Then
            'curRDLC = "UI.rpt_WholeSaleReport.rdlc"
            If radSummaryByDate.Checked Then
                curRDLC = "UI.rpt_WholeSaleSummaryByDate.rdlc"
            Else
                curRDLC = "UI.rpt_WholeSaleReport.rdlc"
            End If
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_WholeSaleReport_Summary.rdlc", curRDLC)
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSale_WholeSale", dt))

            If radDetail.Checked Then
                Dim dtP As New DataTable
                Dim PAmt As Integer = 0
                Dim TAmt As Integer = 0
                Dim NAmt As Integer = 0
                dtP = objWSaleInvoiceController.GetWholeSaleTotalPaidAmtReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, "WholeSaleInvoice", GetFilterString)
                Dim PaidAmt(0) As ReportParameter
                Dim NetAmt(0) As ReportParameter
                Dim TotalAmt(0) As ReportParameter
                If dtP.Rows.Count > 0 Then
                    For Each dr As DataRow In dtP.Rows
                        PAmt += IIf(IsDBNull(dr.Item("PaidAmount")), 0, dr.Item("PaidAmount"))
                        NAmt += IIf(IsDBNull(dr.Item("NetAmount")), 0, dr.Item("NetAmount"))
                        TAmt += IIf(IsDBNull(dr.Item("TotalPayment")), 0, dr.Item("TotalPayment"))
                    Next
                    PaidAmt(0) = New ReportParameter("PaidAmt", PAmt)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", NAmt)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", TAmt)
                    rptViewer.LocalReport.SetParameters(TotalAmt)
                Else
                    PaidAmt(0) = New ReportParameter("PaidAmt", 0)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", 0)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", 0)
                    rptViewer.LocalReport.SetParameters(TotalAmt)

                End If

            End If
            If radSummaryByDate.Checked Then
                Dim NetAmount As Integer = 0
                Dim AllNetAmount(0) As ReportParameter
                If dtAmt.Rows.Count > 0 Then
                    For Each dr As DataRow In dtAmt.Rows
                        NetAmount += IIf(IsDBNull(dr.Item("WSNetAmount")), 0, dr.Item("WSNetAmount"))
                    Next
                    AllNetAmount(0) = New ReportParameter("AllNetAmount", NetAmount)
                    rptViewer.LocalReport.SetParameters(AllNetAmount)
                End If

            End If
        End If

        If optConsignBalance.Checked Then
            'curRDLC = "UI.rpt_WholeSaleReport.rdlc"
            If radSummaryByDate.Checked Then
                curRDLC = "UI.rpt_WholeSaleSummaryByDate.rdlc"
            Else
                curRDLC = "UI.rpt_WholeSaleReport.rdlc"
            End If
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_WholeSaleReport_Summary.rdlc", curRDLC)
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSale_WholeSale", dt))

            If radDetail.Checked Then
                Dim dtP As New DataTable
                Dim PAmt As Integer = 0
                Dim TAmt As Integer = 0
                Dim NAmt As Integer = 0
                dtP = objWSaleInvoiceController.GetWholeSaleTotalPaidAmtReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, "WholeSaleInvoice", GetFilterString)
                Dim PaidAmt(0) As ReportParameter
                Dim NetAmt(0) As ReportParameter
                Dim TotalAmt(0) As ReportParameter
                If dtP.Rows.Count > 0 Then
                    For Each dr As DataRow In dtP.Rows
                        PAmt += IIf(IsDBNull(dr.Item("PaidAmount")), 0, dr.Item("PaidAmount"))
                        NAmt += IIf(IsDBNull(dr.Item("NetAmount")), 0, dr.Item("NetAmount"))
                        TAmt += IIf(IsDBNull(dr.Item("TotalPayment")), 0, dr.Item("TotalPayment"))
                    Next
                    PaidAmt(0) = New ReportParameter("PaidAmt", PAmt)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", NAmt)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", TAmt)
                    rptViewer.LocalReport.SetParameters(TotalAmt)
                Else
                    PaidAmt(0) = New ReportParameter("PaidAmt", 0)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", 0)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", 0)
                    rptViewer.LocalReport.SetParameters(TotalAmt)

                End If

                If dt.Rows.Count > 0 Then
                    NAmt = 0
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i).Item("NetAmount") = dt.Rows(i).Item("GoldPrice")
                        dt.Rows(i).Item("Discount") = 0
                        NAmt += dt.Rows(i).Item("GoldPrice")
                    Next
                End If

                NetAmt(0) = New ReportParameter("NetAmt", NAmt)
                rptViewer.LocalReport.SetParameters(NetAmt)

            End If
            If radSummaryByDate.Checked Then
                Dim NetAmount As Integer = 0
                Dim AllNetAmount(0) As ReportParameter
                If dtAmt.Rows.Count > 0 Then
                    For Each dr As DataRow In dtAmt.Rows
                        NetAmount += IIf(IsDBNull(dr.Item("WSNetAmount")), 0, dr.Item("WSNetAmount"))
                    Next
                    AllNetAmount(0) = New ReportParameter("AllNetAmount", NetAmount)
                    rptViewer.LocalReport.SetParameters(AllNetAmount)
                End If

            End If
        End If

        If optSaleReturn.Checked Then
            'curRDLC = "UI.rpt_WholeSaleReturnReport.rdlc"
            If radSummaryByDate.Checked Then
                curRDLC = "UI.rpt_SaleReturnSummaryByDate.rdlc"
            Else
                curRDLC = "UI.rpt_WholeSaleReturnReport.rdlc"
            End If
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_WholeSaleReturnReport_Summary.rdlc", curRDLC)
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSaleReturn_WholeSaleReturn", dt))

            If radDetail.Checked Then
                Dim dtP As New DataTable
                Dim PAmt As Integer = 0
                Dim TAmt As Integer = 0
                Dim NAmt As Integer = 0
                dtP = objWSaleInvoiceController.GetWholeSaleTotalPaidAmtReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, "WholeSaleReturn", GetFilterString)
                Dim PaidAmt(0) As ReportParameter
                Dim NetAmt(0) As ReportParameter
                Dim TotalAmt(0) As ReportParameter
                If dtP.Rows.Count > 0 Then
                    For Each dr As DataRow In dtP.Rows
                        PAmt += IIf(IsDBNull(dr.Item("PaidAmount")), 0, dr.Item("PaidAmount"))
                        NAmt += IIf(IsDBNull(dr.Item("NetAmount")), 0, dr.Item("NetAmount"))
                        TAmt += IIf(IsDBNull(dr.Item("TotalPayment")), 0, dr.Item("TotalPayment"))
                    Next
                    PaidAmt(0) = New ReportParameter("PaidAmt", PAmt)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", NAmt)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", TAmt)
                    rptViewer.LocalReport.SetParameters(TotalAmt)
                Else
                    PaidAmt(0) = New ReportParameter("PaidAmt", 0)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", 0)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", 0)
                    rptViewer.LocalReport.SetParameters(TotalAmt)

                End If

                If dt.Rows.Count > 0 Then
                    Dim _SaleReturnID As String = ""

                    For i As Integer = 0 To dt.Rows.Count - 1

                        If _SaleReturnID <> dt.Rows(i).Item("WholeSaleReturnID") Then
                            dt.Rows(i).Item("TotalAmount") = dt.Rows(i).Item("TotalAmount")
                            dt.Rows(i).Item("Discount") = dt.Rows(i).Item("Discount")

                        Else
                            dt.Rows(i).Item("TotalAmount") = 0
                            dt.Rows(i).Item("Discount") = 0
                        End If

                        _SaleReturnID = dt.Rows(i).Item("WholeSaleReturnID")
                    Next
                End If

            End If
            If radSummaryByDate.Checked Then
                Dim NetAmount As Integer = 0
                Dim AllNetAmount(0) As ReportParameter
                If dtAmt.Rows.Count > 0 Then
                    For Each dr As DataRow In dtAmt.Rows
                        NetAmount += IIf(IsDBNull(dr.Item("WSNetAmount")), 0, dr.Item("WSNetAmount"))
                    Next
                    AllNetAmount(0) = New ReportParameter("AllNetAmount", NetAmount)
                    rptViewer.LocalReport.SetParameters(AllNetAmount)
                End If

            End If
        End If

        If optPayReturn.Checked Then
            'curRDLC = "UI.rpt_WholeSaleReturnReport.rdlc"
            If radSummaryByDate.Checked Then
                curRDLC = "UI.rpt_SaleReturnSummaryByDate.rdlc"
            Else
                curRDLC = "UI.rpt_WholeSaleReturnReport.rdlc"
            End If
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_WholeSaleReturnReport_Summary.rdlc", curRDLC)
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSaleReturn_WholeSaleReturn", dt))

            If radDetail.Checked Then
                Dim dtP As New DataTable
                Dim PAmt As Integer = 0
                Dim TAmt As Integer = 0
                Dim NAmt As Integer = 0
                dtP = objWSaleInvoiceController.GetWholeSaleTotalPaidAmtReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, "WholeSaleReturn", GetFilterString)
                Dim PaidAmt(0) As ReportParameter
                Dim NetAmt(0) As ReportParameter
                Dim TotalAmt(0) As ReportParameter
                If dtP.Rows.Count > 0 Then
                    For Each dr As DataRow In dtP.Rows
                        PAmt += IIf(IsDBNull(dr.Item("PaidAmount")), 0, dr.Item("PaidAmount"))
                        NAmt += IIf(IsDBNull(dr.Item("NetAmount")), 0, dr.Item("NetAmount"))
                        TAmt += IIf(IsDBNull(dr.Item("TotalPayment")), 0, dr.Item("TotalPayment"))
                    Next
                    PaidAmt(0) = New ReportParameter("PaidAmt", PAmt)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", NAmt)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", TAmt)
                    rptViewer.LocalReport.SetParameters(TotalAmt)
                Else
                    PaidAmt(0) = New ReportParameter("PaidAmt", 0)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", 0)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", 0)
                    rptViewer.LocalReport.SetParameters(TotalAmt)

                End If


                If dt.Rows.Count > 0 Then
                    Dim _WholeSaleReturnID As String = ""

                    For i As Integer = 0 To dt.Rows.Count - 1

                        If _WholeSaleReturnID <> dt.Rows(i).Item("WholeSaleReturnID") Then
                            dt.Rows(i).Item("TotalAmount") = dt.Rows(i).Item("TotalAmount")
                            dt.Rows(i).Item("Discount") = dt.Rows(i).Item("Discount")

                        Else
                            dt.Rows(i).Item("TotalAmount") = 0
                            dt.Rows(i).Item("Discount") = 0
                        End If

                        _WholeSaleReturnID = dt.Rows(i).Item("WholeSaleReturnID")
                    Next
                End If



            End If
            If radSummaryByDate.Checked Then
                Dim NetAmount As Integer = 0
                Dim AllNetAmount(0) As ReportParameter
                If dtAmt.Rows.Count > 0 Then
                    For Each dr As DataRow In dtAmt.Rows
                        NetAmount += IIf(IsDBNull(dr.Item("WSNetAmount")), 0, dr.Item("WSNetAmount"))
                    Next
                    AllNetAmount(0) = New ReportParameter("AllNetAmount", NetAmount)
                    rptViewer.LocalReport.SetParameters(AllNetAmount)
                End If

            End If
        End If

        If optConsignmentSale.Checked Then
            'curRDLC = "UI.rpt_ConsignmentSaleWGReport.rdlc"
            If radSummaryByDate.Checked Then
                curRDLC = "UI.rpt_ConsignmentSaleSummaryByDate.rdlc"
            Else
                curRDLC = "UI.rpt_ConsignmentSaleWGReport.rdlc"
            End If
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = IIf(radSummary.Checked, "UI.rpt_ConsignmentSaleReport_Summary.rdlc", curRDLC)
            rptViewer.LocalReport.DataSources.Clear()
            ' If radSummaryByDate.Checked Then
            'rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSale_WholeSale", dt))
            'Else
            'rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsConsignmentSale_ConsignmentSale", dt))
            'End If
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsConsignmentSale_ConsignmentSale", dt))

            If radDetail.Checked Then
                Dim dtP As New DataTable
                Dim PAmt As Integer = 0
                Dim TAmt As Integer = 0
                Dim NAmt As Integer = 0
                dtP = objWSaleInvoiceController.GetWholeSaleTotalPaidAmtReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, "ConsignmentSale", GetFilterString)
                Dim PaidAmt(0) As ReportParameter
                Dim NetAmt(0) As ReportParameter
                Dim TotalAmt(0) As ReportParameter
                If dtP.Rows.Count > 0 Then
                    For Each dr As DataRow In dtP.Rows
                        PAmt += IIf(IsDBNull(dr.Item("PaidAmount")), 0, dr.Item("PaidAmount"))
                        NAmt += IIf(IsDBNull(dr.Item("NetAmount")), 0, dr.Item("NetAmount"))
                        TAmt += IIf(IsDBNull(dr.Item("TotalPayment")), 0, dr.Item("TotalPayment"))
                    Next
                    PaidAmt(0) = New ReportParameter("PaidAmt", PAmt)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", NAmt)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", TAmt)
                    rptViewer.LocalReport.SetParameters(TotalAmt)
                Else
                    PaidAmt(0) = New ReportParameter("PaidAmt", 0)
                    rptViewer.LocalReport.SetParameters(PaidAmt)

                    NetAmt(0) = New ReportParameter("NetAmt", 0)
                    rptViewer.LocalReport.SetParameters(NetAmt)

                    TotalAmt(0) = New ReportParameter("TotalAmt", 0)
                    rptViewer.LocalReport.SetParameters(TotalAmt)

                End If

            End If
            If radSummaryByDate.Checked Then
                Dim NetAmount As Integer = 0
                Dim AllNetAmount(0) As ReportParameter
                If dtAmt.Rows.Count > 0 Then
                    For Each dr As DataRow In dtAmt.Rows
                        NetAmount += IIf(IsDBNull(dr.Item("WSNetAmount")), 0, dr.Item("WSNetAmount"))
                    Next
                    AllNetAmount(0) = New ReportParameter("AllNetAmount", NetAmount)
                    rptViewer.LocalReport.SetParameters(AllNetAmount)
                End If

            End If
        End If
        If optSale.Checked Or optPay.Checked Then
            If dt.Rows.Count > 0 Then
                Dim _WSaleInvoiceHeaderID As String = ""

                For i As Integer = 0 To dt.Rows.Count - 1
                    'If Not IsDBNull(dt.Rows(i).Item("SalesInvoiceGemItemID")) Then

                    '    If dt.Rows(i).Item("SaleInvoiceDetailID") <> SaleInvoiceDetailID Then
                    '        dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + dt.Rows(i).Item("ItemNetAmount")
                    '    Else
                    '        dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("SumTotalAmount") + 0
                    '    End If

                    '    SalesInvoiceGemItemID = dt.Rows(i).Item("SalesInvoiceGemItemID")
                    '    dtDetail = ReportDA.GetSaleInvoiceGemDataBySaleInvoiceGemsItemID(SalesInvoiceGemItemID)
                    '    For j As Integer = 0 To dtDetail.Rows.Count - 1
                    '        SaleInvoiceDetailID = dtDetail.Rows(j).Item("SaleInvoiceDetailID")
                    '    Next
                    'Else
                    '    dt.Rows(i).Item("SumTotalAmount") = dt.Rows(i).Item("ItemNetAmount") + 0
                    'End If

                    If _WSaleInvoiceHeaderID <> dt.Rows(i).Item("WholeSaleInvoiceID") Then
                        dt.Rows(i).Item("NetAmount") = dt.Rows(i).Item("NetAmount")
                        dt.Rows(i).Item("Discount") = dt.Rows(i).Item("Discount")
                        'dt.Rows(i).Item("PromotionAmount") = dt.Rows(i).Item("PromotionAmount")
                        'dt.Rows(i).Item("TotalPaidAmount") = dt.Rows(i).Item("TotalPaidAmount")
                        'dt.Rows(i).Item("PaidAmount") = dt.Rows(i).Item("PaidAmount")
                        'dt.Rows(i).Item("BalanceAmount") = dt.Rows(i).Item("BalanceAmount")

                        'If (CBool(dt.Rows(i).Item("IsAdvance")) = True And CBool(dt.Rows(i).Item("IsCancel")) = True) Then
                        '    dt.Rows(i).Item("CashOutAmount") = dt.Rows(i).Item("TotalPaidAmount")
                        'ElseIf CInt(dt.Rows(i).Item("PaidAmount")) < 0 Then
                        '    dt.Rows(i).Item("CashOutAmount") = 0 - dt.Rows(i).Item("PaidAmount")
                        'Else
                        '    dt.Rows(i).Item("CashInAmount") = dt.Rows(i).Item("PaidAmount")
                        'End If
                        'dt.Rows(i).Item("DiscountAmount") = dt.Rows(i).Item("DiscountAmount")
                    Else
                        dt.Rows(i).Item("NetAmount") = 0
                        dt.Rows(i).Item("Discount") = 0
                    End If

                    _WSaleInvoiceHeaderID = dt.Rows(i).Item("WholeSaleInvoiceID")
                Next
            End If
        End If

        Dim LocName(0) As ReportParameter
        If (chkLocation.Checked) Then
            LocName(0) = New ReportParameter("LocName", _Location.GetLocationByID(cboLocation.SelectedValue).Location)
            rptViewer.LocalReport.SetParameters(LocName)
        Else
            LocName(0) = New ReportParameter("LocName", "ALL")
            rptViewer.LocalReport.SetParameters(LocName)
        End If

        Dim IsSelect As Boolean = False
        Dim Selected(0) As ReportParameter
        If chkLocation.Checked Or chkCustomer.Checked Then
            IsSelect = True
        End If
        If radDetail.Checked Then
            Selected(0) = New ReportParameter("Selected", IsSelect)
            rptViewer.LocalReport.SetParameters(Selected)
        End If
        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rptViewer.LocalReport.SetParameters(G_PToY)

        rptViewer.RefreshReport()
    End Sub

    Private Sub cboCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboCategory, e)
    End Sub

    Private Sub cboCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboCategory, "")
    End Sub


    Private Function GetFilterString() As String
        GetFilterString = ""
        If (chkLocation.Checked) Then
            GetFilterString += " And W.LocationID = '" & cboLocation.SelectedValue & "'"
        End If

        If (chkCustomer.Checked) Then
            GetFilterString += " And W.CustomerID = '" & _CustomerID & "'"
        End If

        If (optSale.Checked) Then
            GetFilterString += " And (W.PayType='0' OR W.PayType='2' OR W.PayType='3') "
        End If
        If (chkItemCategory.Checked) Then
            GetFilterString += " And C.ItemCategoryID = '" & cboCategory.SelectedValue & "'"
        End If
        If (chkGoldQuality.Checked) Then
            GetFilterString += " And GQ.GoldQualityID = '" & cboGoldQuality.SelectedValue & "'"
        End If

        If (ChkItemName.Checked) Then
            GetFilterString += " And I.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If
        If (optPay.Checked) Then
            GetFilterString += " And W.PayType='1' "
        End If

        If (optPayReturn.Checked) Then
            GetFilterString += " And WI.IsSale='0' AND WI.IsReturn='1' "
        End If

        If (optSaleReturn.Checked) Then
            GetFilterString += " And WI.IsSale='1' AND WI.IsReturn='1' "
        End If

        ''If (chkItemCategory.Checked) Then
        ''    GetFilterString += " And H.ItemCategoryID = '" & cboItemCategory.SelectedValue & "'"
        ''End If
        ''If (chkOriginalCode.Checked) Then
        ''    GetFilterString += " And D.DesignID = '" & cboOriginalCode.SelectedValue & "'"
        ''End If
        ''If (chkColor.Checked) Then
        ''    GetFilterString += " And H.ColorCodeID = '" & cboColor.SelectedValue & "'"
        ''End If

        ''If (chkWeight.Checked) Then
        ''    GetFilterString += " And H.GoldTG between " & _FromGoldTG & " And " & _ToGoldTG & ""
        ''End If

        'If (chkAddOrSub.Checked) Then
        '    GetFilterString += " And H.AddOrSub < '0'"
        'End If
        'If (chkDiscount.Checked) Then
        '    GetFilterString += " And H.DiscountAmount > '0'"
        'End If

        If (txtBarcodeNo.Text <> "") Then
            GetFilterString += " And F.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'"
        End If

    End Function

    'Private Function cristr() As String
    '    cristr = ""
    '    If (radInvoiceID.Checked) Then
    '        cristr += " Order by H.SaleInvoiceID "
    '    ElseIf (radDiscount.Checked) Then
    '        cristr += " Order by H.DiscountAmount desc "
    '    ElseIf (radSubAmount.Checked) Then
    '        cristr += " Order by H.AddOrSub asc "

    '    End If
    'End Function

    Private Sub Get_Combos()

        If Global_IsHoMaster = True Then
            cboGoldQuality.DisplayMember = "GoldQuality"
            cboGoldQuality.ValueMember = "@GoldQualityID"
            cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView

            cboCategory.DisplayMember = "ItemCategory_"
            cboCategory.ValueMember = "@ItemCategoryID"
            cboCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView
        Else
            cboGoldQuality.DisplayMember = "GoldQuality"
            cboGoldQuality.ValueMember = "@GoldQualityID"
            cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQualityByLocation(LocationID).DefaultView

            cboCategory.DisplayMember = "ItemCategory_"
            cboCategory.ValueMember = "@ItemCategoryID"
            cboCategory.DataSource = _ItemCategoryController.GetAllItemCategoryByLocation(LocationID).DefaultView
        End If
    End Sub
    

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub chkLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLocation.CheckedChanged
        If chkLocation.Checked Then
            cboLocation.Enabled = True
        Else
            cboLocation.Enabled = False
        End If
    End Sub
    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Dim DataItem As DataRow
        Dim dtCustomer As New DataTable
        Dim objCustomer As New CustomerInfo
        dtCustomer = objCustomerController.GetAllCustomer()
        DataItem = DirectCast(SearchData.FindFast(dtCustomer, "Customer List"), DataRow)
        If DataItem IsNot Nothing Then
            _CustomerID = DataItem.Item("@CustomerID").ToString
            txtCustomerCode.Text = DataItem.Item("CustomerCode").ToString
            txtCustomerName.Text = DataItem.Item("CustomerName_").ToString
            '_CustType = DataItem.Item("SaleType").ToString
            '_DealerPayAmount = DataItem.Item("")
        End If
    End Sub

    Private Sub chkCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomer.CheckedChanged
        If chkCustomer.Checked Then
            txtCustomerCode.Enabled = True
            txtCustomerName.Enabled = True
            btnCustomer.Enabled = True
        Else
            txtCustomerCode.Enabled = False
            txtCustomerName.Enabled = False
            btnCustomer.Enabled = False
        End If
    End Sub

    Private Sub txtCustomerCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerCode.TextChanged
        Dim obj As New CommonInfo.CustomerInfo
        If txtCustomerCode.Text <> "" Then
            _CustomerCode = txtCustomerCode.Text.ToString
            obj = objCustomerController.GetCustomerCode(_CustomerCode)
            With obj
                txtCustomerName.Text = .CustomerName
                _CustomerID = .CustomerID
                '_CustType = .SaleType
            End With
        End If
    End Sub

   
    Private Sub chkItemCategory_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemCategory.CheckedChanged
        If (chkItemCategory.Checked) Then
            cboCategory.Enabled = True
        Else
            cboCategory.Enabled = False
        End If
    End Sub

    
    Private Sub ChkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles ChkItemName.CheckedChanged
        If (ChkItemName.Checked) Then
            cboItemName.Enabled = True
        Else
            cboItemName.Enabled = False
        End If
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

    Private Sub cboCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategory.SelectedIndexChanged
        Dim itemid As String
        itemid = cboCategory.SelectedValue
        RefreshItemNameCbo(itemid)
    End Sub

    Private Sub chkGoldQuality_CheckedChanged(sender As Object, e As EventArgs) Handles chkGoldQuality.CheckedChanged
        If (chkGoldQuality.Checked) Then
            cboGoldQuality.Enabled = True
        Else
            cboGoldQuality.Enabled = False
        End If
    End Sub

End Class