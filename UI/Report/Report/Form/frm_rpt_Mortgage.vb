Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo

Public Class frm_rpt_Mortgage
    Dim dt As DataTable
    Private _MortgageController As MortgageInvoice.IMortgageInvoiceController = Factory.Instance.CreateMortgageInvoiceController
    Private _LocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemCategory As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQuality As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
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
    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboLocation, e)
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboLocation, "")
    End Sub

    Private Sub cboGoldQ_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQ.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQ, e)
    End Sub

    Private Sub cboGoldQ_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQ.Leave
        AutoCompleteCombo_Leave(cboGoldQ, "")
    End Sub

    Private Sub cboItemCat_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemCat.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCat, e)
    End Sub

    Private Sub cboItemCat_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCat.Leave
        AutoCompleteCombo_Leave(cboItemCat, "")
    End Sub
#End Region
    Dim StrCri As String
    Private _CustomerID As String = ""

    Private Sub frm_rpt_Mortgage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rpt_Mortgage.RefreshReport()
        Get_Combos()
        cboLocation.Enabled = False
        cboGoldQ.Enabled = False
        cboItemCat.Enabled = False
        btnCustomer.Enabled = False
        grpType.Enabled = False
        CboLocation.Enabled = True
        chkLocation.Checked = True
        CboLocation.SelectedValue = Global_CurrentLocationID
        grpBoxView.Enabled = False
        chkOldReturn.Enabled = False

    End Sub
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Dim dt As New DataTable
        Dim dtTotal As New DataTable
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim TotalLoanAmount As Long = 0
        Dim TotalInterest As Long = 0
        Dim TotalPaid As Long = 0
        Dim TotalAddOrSub As Long = 0

        If chkLocation.Checked Then
            If radAll.Checked Then dt = _MortgageController.GetAllMortgageReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

            If radReceive.Checked Then dt = _MortgageController.GetMortgageReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

            If radReturn.Checked Then
                If chkOldReturn.Checked = False Then
                    dt = _MortgageController.GetMortgageReturnReportNew(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                Else
                    dt = _MortgageController.GetMortgageReturnReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                End If
            End If


            If radDisable.Checked Then dt = _MortgageController.GetMortgageInvoiceDisableReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

            If radPayback.Checked Then dt = _MortgageController.GetMortgagePaybackReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If

        If radInterest.Checked Then
            ds = _MortgageController.GetMortgageInterestReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)

            Dim tmpdtHeader As DataTable
            Dim tmpdtItem As DataTable
            tmpdtHeader = ds.Tables(0)
            tmpdtItem = ds.Tables(1)

            rpt_Mortgage.Reset()
            rpt_Mortgage.LocalReport.ReportEmbeddedResource = "UI.rpt_MortgageInterest.rdlc"
            rpt_Mortgage.LocalReport.DataSources.Clear()
            rpt_Mortgage.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_Mortgage", tmpdtHeader))

            If tmpdtItem.Rows.Count > 0 Then
                Dim TotalAmount As Integer = 0
                Dim TotalAmt(0) As ReportParameter
                Dim PaybackAmount As Integer = 0
                Dim PaybackAmt(0) As ReportParameter
                Dim TotalInstAmt As Integer = 0
                Dim TotalInst(0) As ReportParameter
                'Dim PaybackBalanceAmount As Integer = 0
                'Dim PaybackBalanceAmt(0) As ReportParameter

                For Each dr As DataRow In tmpdtItem.Rows
                    TotalAmount += dr.Item("TotalAmount")
                    PaybackAmount += dr.Item("PaybackAmt")
                    TotalInstAmt += dr.Item("InterestAmount")
                Next
                TotalInst(0) = New ReportParameter("TotalInst", TotalInstAmt)
                TotalAmt(0) = New ReportParameter("TotalAmt", TotalAmount)
                PaybackAmt(0) = New ReportParameter("PaybackAmt", PaybackAmount)

                rpt_Mortgage.LocalReport.SetParameters(TotalAmt)
                rpt_Mortgage.LocalReport.SetParameters(TotalInst)
            End If
        ElseIf radHistory.Checked Then
            ds1 = _MortgageController.GetMortgageCustomerHistoryReport(GetFilterString)

            Dim tmpdtHeader As DataTable
            Dim tmpdtInterest As DataTable
            Dim tmpdtPayback As DataTable

            tmpdtHeader = ds1.Tables(0)
            tmpdtInterest = ds1.Tables(1)
            tmpdtPayback = ds1.Tables(2)

            rpt_Mortgage.Reset()
            rpt_Mortgage.LocalReport.ReportEmbeddedResource = "UI.rpt_MortgageHistory.rdlc"
            rpt_Mortgage.LocalReport.DataSources.Clear()
            rpt_Mortgage.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_Mortgage", tmpdtHeader))
            rpt_Mortgage.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_MortgageInterest", tmpdtInterest))
            rpt_Mortgage.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_MortgagePayback", tmpdtPayback))

            'If tmpdtItem.Rows.Count > 0 Then
            '    Dim TotalAmount As Integer = 0
            '    Dim TotalAmt(0) As ReportParameter
            '    Dim PaybackAmount As Integer = 0
            '    Dim PaybackAmt(0) As ReportParameter
            '    'Dim PaybackBalanceAmount As Integer = 0
            '    'Dim PaybackBalanceAmt(0) As ReportParameter

            '    For Each dr As DataRow In tmpdtItem.Rows
            '        TotalAmount += dr.Item("TotalAmount")
            '        PaybackAmount += dr.Item("PaybackAmt")
            '    Next
            '    TotalAmt(0) = New ReportParameter("TotalAmt", TotalAmount)
            '    PaybackAmt(0) = New ReportParameter("PaybackAmt", PaybackAmount)

            '    rpt_Mortgage.LocalReport.SetParameters(TotalAmt)
            'End If
            If tmpdtHeader.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
            End If

        Else

            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
            End If
            rpt_Mortgage.Reset()
            If (radAll.Checked) Then
                rpt_Mortgage.LocalReport.ReportEmbeddedResource = "UI.rpt_MortgageAll.rdlc"
            ElseIf (radReceive.Checked) Then
                rpt_Mortgage.LocalReport.ReportEmbeddedResource = "UI.rpt_MortgageReceive.rdlc"
            ElseIf (radPayback.Checked) Then
                rpt_Mortgage.LocalReport.ReportEmbeddedResource = "UI.rpt_MortgagePayback.rdlc"
            ElseIf (radReturn.Checked) Then
                rpt_Mortgage.LocalReport.ReportEmbeddedResource = "UI.rpt_MortgageReturn.rdlc"
            ElseIf (radDisable.Checked) Then
                rpt_Mortgage.LocalReport.ReportEmbeddedResource = "UI.rpt_MortgageDisable.rdlc"

            End If

            rpt_Mortgage.LocalReport.DataSources.Clear()

            rpt_Mortgage.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_Mortgage", dt))

        End If

        If radReturn.Checked Then
            If chkOldReturn.Checked Then
                dtTotal = _MortgageController.GetMortgageReturnReportSum(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            Else
                dtTotal = _MortgageController.GetMortgageReturnReportSumNew(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            End If

            If dtTotal.Rows.Count > 0 Then
                For Each dr As DataRow In dtTotal.Rows
                    TotalLoanAmount += dr.Item("TotalAmount")
                    TotalInterest += dr.Item("InterestAmount")
                    TotalPaid += dr.Item("PaidAmount")
                    TotalAddOrSub += dr.Item("AddOrSub")
                Next
            End If

            Dim TotalAmtPara(0) As ReportParameter
            Dim TotalInterestPara(0) As ReportParameter
            Dim TotalPaidPara(0) As ReportParameter
            Dim TotalAddOrSubPara(0) As ReportParameter

            TotalAmtPara(0) = New ReportParameter("TotalAmtPara", TotalPaid)
            rpt_Mortgage.LocalReport.SetParameters(TotalAmtPara)

            TotalInterestPara(0) = New ReportParameter("TotalInterestPara", TotalInterest)
            rpt_Mortgage.LocalReport.SetParameters(TotalInterestPara)

            TotalPaidPara(0) = New ReportParameter("TotalPaidPara", TotalPaid)
            rpt_Mortgage.LocalReport.SetParameters(TotalPaidPara)

            TotalAddOrSubPara(0) = New ReportParameter("TotalAddOrSubPara", TotalAddOrSub)
            rpt_Mortgage.LocalReport.SetParameters(TotalAddOrSubPara)
        End If

        If radPayback.Checked Then
            'If chkOldReturn.Checked Then
            '    dtTotal = _MortgageController.GetMortgageReturnReportSum(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            'Else
            '    dtTotal = _MortgageController.GetMortgageReturnReportSumNew(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            'End If

            Dim dtTotalPayback As DataTable
            Dim TotalPayback As Integer
            Dim TotalPaidPayback As Integer
            Dim TotalDiscount As Integer
            dtTotalPayback = _MortgageController.GetMortgagePaybackTotalReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dtTotalPayback.Rows.Count > 0 Then
                For Each dr As DataRow In dtTotalPayback.Rows
                    TotalPayback += dr.Item("TotalAmount")
                    TotalPaidPayback += dr.Item("PaidAmount")
                    TotalDiscount += dr.Item("DiscountAmount")
                Next
            End If

            Dim TotalPaybackPara(0) As ReportParameter
            Dim TotalPaidPaybackPara(0) As ReportParameter
            Dim TotalDiscountPara(0) As ReportParameter

            TotalPaybackPara(0) = New ReportParameter("TotalPaybackPara", TotalPayback)
            rpt_Mortgage.LocalReport.SetParameters(TotalPaybackPara)

            TotalPaidPaybackPara(0) = New ReportParameter("TotalPaidPaybackPara", TotalPaidPayback)
            rpt_Mortgage.LocalReport.SetParameters(TotalPaidPaybackPara)

            TotalDiscountPara(0) = New ReportParameter("TotalDiscountPara", TotalDiscount)
            rpt_Mortgage.LocalReport.SetParameters(TotalDiscountPara)

        End If

        Dim LocName(0) As ReportParameter
        If (chkLocation.Checked) Then
            LocName(0) = New ReportParameter("LocName", _LocationController.GetLocationByID(CboLocation.SelectedValue).Location)
            rpt_Mortgage.LocalReport.SetParameters(LocName)
        Else
            LocName(0) = New ReportParameter("LocName", "ALL")
            rpt_Mortgage.LocalReport.SetParameters(LocName)
        End If
        rpt_Mortgage.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If (radReturn.Checked) Then
            If (chkLocation.Checked) Then
                GetFilterString += " And H.LocationID = '" & cboLocation.SelectedValue & "' "
            End If
            If (chkCustomerName.Checked) Then
                GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
            End If
        Else
            If (chkLocation.Checked) Then
                GetFilterString += " And H.LocationID = '" & cboLocation.SelectedValue & "' "
            End If
            If chkGoldQ.Checked Then
                GetFilterString += " And I.GoldQualityID = '" & cboGoldQ.SelectedValue & "' "
            End If
            If chkItemCat.Checked Then
                GetFilterString += " And I.ItemCategoryID = '" & cboItemCat.SelectedValue & "' "
            End If

            If (radDisable.Checked) Then
                GetFilterString += " And IsDisable = '1'"
            End If
            If (chkCustomerName.Checked) Then
                GetFilterString += " And H.CustomerID  = '" & _CustomerID & "'"
            End If
            If (optBalanceStocks.Checked) Then
                GetFilterString += "And H.isReturn=0 And I.MortgageItemID Not in (select MortgageItemID from tbl_MortgageReturnItem RI Inner Join tbl_MortgageReturn R On R.MortgageReturnID=RI.MortgageReturnID where R.IsDelete=0 )"
            End If

        End If

    End Function
    Private Sub Get_Combos()
        'cboLocation.DisplayMember = "Location"
        'cboLocation.ValueMember = "@LocationID"
        'cboLocation.DataSource = _LocationController.GetAllLocationList().DefaultView
        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
        cboLocation.DataSource = _Location.GetAllLocationList().DefaultView

        cboGoldQ.DisplayMember = "GoldQuality"
        cboGoldQ.ValueMember = "@GoldQualityID"
        cboGoldQ.DataSource = _GoldQuality.GetAllGoldQuality().DefaultView

        cboItemCat.DisplayMember = "ItemCategory_"
        cboItemCat.ValueMember = "@ItemCategoryID"
        cboItemCat.DataSource = _ItemCategory.GetAllItemCategory().DefaultView
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkGoldQ_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQ.CheckedChanged
        cboGoldQ.Enabled = chkGoldQ.Checked
    End Sub

    Private Sub chkItemCat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCat.CheckedChanged
        cboItemCat.Enabled = chkItemCat.Checked
    End Sub

    Private Sub radReturn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radReturn.CheckedChanged
        chkGoldQ.Enabled = Not radReturn.Checked
        chkItemCat.Enabled = Not radReturn.Checked
        If radReturn.Checked Then
            chkOldReturn.Enabled = True
        Else
            chkOldReturn.Enabled = False
        End If
    End Sub
    Private Sub radReceive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radReceive.CheckedChanged
        If radReceive.Checked Then
            grpType.Enabled = True
        Else
            grpType.Enabled = False
        End If
    End Sub

    Private Sub radInterest_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radInterest.CheckedChanged
        chkGoldQ.Enabled = Not radReturn.Checked
        chkItemCat.Enabled = Not radReturn.Checked
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
            End If
        End If

    End Sub
    Private Sub GetStringID(ByVal dt As DataTable, ByVal FieldName As String, ByVal ColumnName As String)
        StrCri = ReturnStringCri(dt, FieldName, "")
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
        If chkCustomerName.Checked = True Then
            btnCustomer.Enabled = True
        Else
            btnCustomer.Enabled = False

        End If
    End Sub

    Private Sub radHistory_CheckedChanged(sender As Object, e As EventArgs) Handles radHistory.CheckedChanged
        If radHistory.Checked = True Then
          

            dtpFromDate.Enabled = False
            dtpToDate.Enabled = False
            chkItemCat.Enabled = False
            chkGoldQ.Enabled = False

            If (txtCustomerCode.Text = "") Then
                MsgBox("Please Select  Customer !", MsgBoxStyle.Information, AppName)
                chkCustomerName.Focus()
                Exit Sub
            End If
        Else
            dtpFromDate.Enabled = True
            dtpToDate.Enabled = True
            chkItemCat.Enabled = True
            chkGoldQ.Enabled = True

        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles chkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub

End Class