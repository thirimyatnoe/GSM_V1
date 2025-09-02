Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule

Public Class frm_rpt_CashTransactionByCustomer
    Private _CustomerID As String = ""
    Private ReportDA As CashReceipt.ICashReceiptController = Factory.Instance.CreateCashReceiptController
    Private _GeneralLedgerByLocation As GeneralLedgerByLocation.IGeneralLedgerByLocationController = Factory.Instance.CreateGeneralLedgerByLocationController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _GoldQ As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
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

#End Region

    Private Sub frm_CustomerTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rptViewer.RefreshReport()
        Get_Combos()
        _CustomerID = "0"
        chkCustomerName.Checked = True
        txtCustomerCode.Enabled = True
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtCustomer.Enabled = False
        txtAddress.Enabled = False
        btnCustomer.Enabled = True
        Me.CboLocation.Enabled = True
        cboGoldQ.Enabled = False
        ChkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim tmpdt As New DataTable
        Dim curRDLC As String = ""
        Dim dtOtherCash As New DataTable
        Dim dtCashReceipt As New DataTable
        Dim Type As String

        Dim Str As String = ""

        'If chkCustomerReceipt.Checked Then
        'Str = " And PayDate>='" & Format(dtpFromDate.Value.Date, "yyyy/MM/dd") & " 00:00:00' AND PayDate<= '" & Format(dtpToDate.Value.Date, "yyyy/MM/dd") & " 23:59:59'"

        'If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
        '    MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        'End If

        'dtCashReceipt = ReportDA.GetDebtInDataByType("All", dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, Str)
        'If (ChkLocation.Checked) Then
        '    tmpdt = _GeneralLedgerByLocation.GetAllCustomerReceipt(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
        'Else
        '    MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        'End If
        'curRDLC = "UI.rpt_CustomerReceipt.rdlc"

        'If tmpdt.Rows.Count = 0 Then
        '    MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        'End If
        'rptViewer.Reset()
        'rptViewer.LocalReport.ReportEmbeddedResource = curRDLC
        'rptViewer.LocalReport.DataSources.Clear()
        'rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsDailyTransaction_DailyTransaction", tmpdt))
        'rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsCashReceipt_CashReceipt", dtCashReceipt))
        'rptViewer.RefreshReport()
        'Else
        If rptAll.Checked Then
            Type = "All"
        ElseIf rbtOrderReturn.Checked Then
            Type = "OrderInvoice"
        ElseIf rbtRepairReturn.Checked Then
            Type = "RepairReturn"
        ElseIf rbtWSInvoice.Checked Then
            Type = "WholeSaleInvoice"
        ElseIf rbtSaleInvoice.Checked Then
            Type = "SaleInvoice"
        ElseIf rbtSaleGems.Checked Then
            Type = "SaleGems"
        ElseIf rbtSalesVolume.Checked Then
            Type = "SalesVolume"
        ElseIf rbtConsignmentSale.Checked Then
            Type = "ConsignmentSaleInvoice"
        ElseIf rbtLooseDiamond.Checked Then
            Type = "SaleLooseDiamond"
        End If

        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If _CustomerID = "0" Then
            MsgBox("Please select customer first!", MsgBoxStyle.Information, "GoldSmith Management System")
            Exit Sub
        End If

        If optNil.Checked Then
            Str = "Nill"
        ElseIf optNotNil.Checked Then
            Str = "Not Nill"
        End If

        'If rptAll.Checked Then
        tmpdt = _GeneralLedgerByLocation.GetAllCashTransaction(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, Type, cboGoldQ.SelectedValue, txtCustomerCode.Text, CboLocation.SelectedValue, _CustomerID, Str)
        'End If
        Dim FinalCredit As Integer = "0"
        'For Each dr As DataRow In tmpdt.Rows
        '    FinalCredit = dr.Item("CreditAmount")
        'Next
        Dim Opening As Long
        If tmpdt.Rows.Count > 0 Then
            Opening = tmpdt.Rows(0).Item("CreditAmount")
            For k As Integer = 0 To tmpdt.Rows.Count - 1

                tmpdt.Rows(k).Item("CreditAmount") = Opening + tmpdt.Rows(k).Item("VCredit") - tmpdt.Rows(k).Item("ReceiveAmount")
                Opening = tmpdt.Rows(k).Item("CreditAmount")
            Next
        End If

        curRDLC = "UI.rpt_AllCashTransaction.rdlc"

        If tmpdt.Rows.Count = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, AppName)
        End If

        rptViewer.Reset()
        rptViewer.LocalReport.ReportEmbeddedResource = curRDLC
        rptViewer.LocalReport.DataSources.Clear()
        rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsAllCashTransaction_AllCashTransaction", tmpdt))

        Dim FinalCreditAmount(0) As ReportParameter
        FinalCreditAmount(0) = New ReportParameter("FinalCreditAmount", Opening)
        rptViewer.LocalReport.SetParameters(FinalCreditAmount)

        rptViewer.RefreshReport()


        'End If

    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""

        If chkGoldQly.Checked Then
            GetFilterString += " And D.GoldQualityID  = '" & cboGoldQ.SelectedValue & "'"
        End If

        If ChkLocation.Checked Then
            GetFilterString += " And H.LocationID  = '" & CboLocation.SelectedValue & "'"
        End If

        If chkCustomerName.Checked Then
            GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
        End If

    End Function
    Private Sub chkGoldQly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQly.CheckedChanged
        If (chkGoldQly.Checked) Then
            cboGoldQ.Enabled = True
        Else
            cboGoldQ.Enabled = False
        End If
    End Sub
    Private Sub cboGoldQ_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQ.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQ, e)
    End Sub

    Private Sub cboGoldQ_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQ.Leave
        AutoCompleteCombo_Leave(cboGoldQ, "")
    End Sub
    
    Private Sub Get_Combos()
        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboGoldQ.DisplayMember = "GoldQuality"
        cboGoldQ.ValueMember = "@GoldQualityID"
        cboGoldQ.DataSource = _GoldQ.GetAllGoldQuality().DefaultView

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs)
        openhelp("CashIn\CashOut")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
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
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
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
End Class