Imports Microsoft.Reporting.WinForms
Imports System.Data.SqlClient
Imports System.Data.Common
Imports BusinessRule
Imports Operational
Public Class frm_rpt_CustomReport

    Dim ReportCode As String
    Public dtCustomer As New DataTable
    Public dtItemCategory As New DataTable
    Public dtGoldQuality As New DataTable
    Public dtStaff As New DataTable
    Public dtItemName As New DataTable
    Public dtGemsCategory As New DataTable
    Public dtReturn As New DataTable

    Dim ByCustomerStr As String
    Dim ByItemCategoryStr As String
    Dim ByGoldQualityStr As String
    Dim ByItemNameStr As String
    Dim ByStaffStr As String
    Dim ByGemsCategoryStr As String
    Dim _CustomerID As String

    Dim _fromdate As DateTime
    Dim _todate As DateTime

    Private _CustomReportController As CustomReport.ICustomReportController = Factory.Instance.CreateCustomReportController
    Private _CustomerController As Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _ItemCatController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _StaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _GemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _ItemNameCon As ItemName.IItemNameController = Factory.Instance.CreateItemName

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
    Private Sub cboItemCat_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemCat.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCat, e)
    End Sub

    Private Sub cboItemCat_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCat.Leave
        AutoCompleteCombo_Leave(cboItemCat, "")
    End Sub
    Private Sub cboItemName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemName.KeyUp
        AutoCompleteCombo_KeyUp(cboItemName, e)
    End Sub

    Private Sub cboItemName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemName.Leave
        AutoCompleteCombo_Leave(cboItemName, "")
    End Sub

    Private Sub cboGoldQ_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQ.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQ, e)
    End Sub

    Private Sub cboGoldQ_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQ.Leave
        AutoCompleteCombo_Leave(cboGoldQ, "")
    End Sub
    Private Sub cboGemsCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGemsCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboGemsCategory, e)
    End Sub

    Private Sub cboGemsCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGemsCategory.Leave
        AutoCompleteCombo_Leave(cboGemsCategory, "")
    End Sub

    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub
#End Region


    Private Sub frm_rpt_CustomReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Clear()
    End Sub

    Private Sub Clear()
        ReportCode = ""
        cboReportName.Text = ""
        cboReportName.Items.Clear()


        ByCustomerStr = ""
        ByItemCategoryStr = ""
        ByGoldQualityStr = ""
        ByGemsCategoryStr = ""
        ByStaffStr = ""

        grpDate.Visible = False
        grpGQ.Visible = False
        grpCustomer.Visible = False

        dtpFromDate.Visible = False
        dtpToDate.Visible = False

        chkGoldQ.Visible = False
        chkGems.Visible = False
        chkItemCat.Visible = False
        chkCustomerName.Visible = False
        chkItemName.Visible = False
        chkStaff.Visible = False
        cboGoldQ.Visible = False
        cboItemCat.Visible = False
        cboGemsCategory.Visible = False
        cboItemName.Visible = False
        cboStaff.Visible = False
        txtCustomerCode.Visible = False
        txtCustomerName.Visible = False
        btnCustomer.Visible = False

        If dtReturn.Columns.Count = 0 Then
            dtReturn.Columns.Add("Type")
            dtReturn.Columns.Add("Value")
        End If

        Me.rptViewer.RefreshReport()
    End Sub
    Private Sub Preview()
        'GetCri()

        LoadCretia(dtReturn)

        rptViewer.Reset()

        Dim objinterface As ITest
        Dim dterr As New DataTable
        Dim ds As DataSet = Nothing
        Dim dt As New System.Data.DataTable
        'Dim ds As DataSet = Nothing

        objinterface = Scripting.CompileReportScriptObj(ReportCode, dterr)
        If dterr.Rows.Count > 0 Then
            MsgBox(dterr.Rows(0)(0) & "-" & dterr.Rows(0)(1))
        Else

            ds = objinterface.GenerateReportDocument(ByCustomerStr, ByGoldQualityStr, ByItemCategoryStr, ByItemNameStr, ByGemsCategoryStr, ByStaffStr, dtpFromDate.Value, dtpToDate.Value)


            RptViewer.Reset()
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\CustomRDLC\" & cboReportName.Text.Trim & ".rdlc"
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables.Count - 1
                    Dim dsname As String
                    'dsname = "dsSaleInvoice_" & ds.Tables(0).TableName
                    dsname = ds.Tables(0).TableName
                    RptViewer.LocalReport.DataSources.Add(New ReportDataSource(dsname, ds.Tables(i)))
                Next
            End If
            RptViewer.RefreshReport()
        End If
    End Sub

    Public Sub LoadCretia(ByVal dtResult As DataTable)

        ByCustomerStr = ""
        ByGoldQualityStr = ""
        ByItemCategoryStr = ""
        ByItemNameStr = ""
        ByStaffStr = ""
        ByGemsCategoryStr = ""

        '_fromdate = dtpFromDate.Value
        '_todate = dtpToDate.Value
        If chkCustomerName.Checked Then
            ByCustomerStr = _CustomerID
        End If

        If chkGoldQ.Checked And cboGoldQ.SelectedValue <> -1 Then
            ByGoldQualityStr = cboGoldQ.SelectedValue
        End If

        If chkGems.Checked And cboGemsCategory.SelectedValue <> -1 Then
            ByGemsCategoryStr = cboGemsCategory.SelectedValue
        End If

        If chkItemCat.Checked And cboItemCat.SelectedValue <> -1 Then
            ByItemCategoryStr = cboItemCat.SelectedValue
        End If

        If chkItemName.Checked And cboItemName.SelectedValue <> "" Then
            ByItemNameStr = cboItemName.SelectedValue
        End If

        'If IsNothing(dtResult) Then Exit Sub
        'If dtResult.Rows.Count > 0 Then

        '    For i As Integer = 0 To dtResult.Rows.Count - 1
        '        If dtResult.Rows(i).Item("Type") = "FromDate" Then
        '            _fromdate = dtpFromDate.Value
        '        ElseIf dtResult.Rows(i).Item("Type") = "ToDate" Then
        '            _todate = dtpToDate.Value
        '        ElseIf dtResult.Rows(i).Item("Type") = "ByCustomer" Then
        '            ByCustomerStr = dtResult.Rows(i).Item("Value")

        '        ElseIf dtResult.Rows(i).Item("Type") = "ByGoldQuality" Then
        '            ByGoldQualityStr = dtResult.Rows(i).Item("Value")

        '        ElseIf dtResult.Rows(i).Item("Type") = "ByItemCategory" Then
        '            ByItemCategoryStr = dtResult.Rows(i).Item("Value")

        '        ElseIf dtResult.Rows(i).Item("Type") = "ByItemName" Then
        '            ByItemNameStr = dtResult.Rows(i).Item("Value")

        '        ElseIf dtResult.Rows(i).Item("Type") = "ByStaff" Then
        '            ByStaffStr = dtResult.Rows(i).Item("Value")

        '        ElseIf dtResult.Rows(i).Item("Type") = "ByGemsCategory" Then
        '            ByGemsCategoryStr = dtResult.Rows(i).Item("Value")

        '        End If
        '    Next
        'End If




    End Sub

    Private Sub GetCri()

        If dtCustomer.Rows.Count > 0 Then
            AddDataRow(dtCustomer, "CustomerID", "ByCustomer")
        End If

        If dtGoldQuality.Rows.Count > 0 Then
            AddDataRow(dtGoldQuality, "GoldQualityID", "ByGoldQuality")
        End If

        If dtItemCategory.Rows.Count > 0 Then
            AddDataRow(dtItemCategory, "ItemCategoryID", "ByItemCategory")
        End If

        If dtItemName.Rows.Count > 0 Then
            AddDataRow(dtItemName, "ItemNameID", "ByItemName")
        End If

        If dtGemsCategory.Rows.Count > 0 Then
            AddDataRow(dtGemsCategory, "GemsCategoryID", "ByGemsCategory")
        End If

        If dtStaff.Rows.Count > 0 Then
            AddDataRow(dtStaff, "StaffID", "ByStaff")
        End If
        'If chkGoldQ.Checked Then
        '    ByGoldQualityStr = cboGoldQ.SelectedValue
        'End If

        'If chkItemCat.Checked Then
        '    ByGoldQualityStr = cboItemCat.SelectedValue
        'End If

        'If chkGems.Checked Then
        '    ByGemsCategoryStr = cboGemsCategory.SelectedValue
        'End If

        'If chkStaff.Checked Then
        '    ByStaffStr = cboStaff.SelectedValue
        'End If

        'If chkCustomerName.Checked Then
        '    ByCustomerStr = _CustomerID
        'End If
    End Sub



    Private Sub AddDataRow(ByVal dt As DataTable, ByVal FieldName As String, ByVal ColumnName As String)
        Dim strcat As String
        strcat = ReturnStringCri(dt, FieldName)
        Dim drview As DataRow
        drview = dtReturn.NewRow
        drview.Item("Type") = ColumnName
        drview.Item("Value") = strcat
        dtReturn.Rows.Add(drview)
    End Sub
    Private Function ReturnStringCri(ByVal dt As DataTable, ByVal Name As String) As String
        Dim strCust As String = ""
        For Each row As DataRow In dt.Rows
            strCust += "'" & row(Name) & "'" & ","
        Next
        If strCust <> "" Then
            strCust = Mid(strCust, 1, strCust.Length - 1)
        End If
        Return strCust
    End Function

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Preview()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub cboReportName_DropDown(sender As Object, e As EventArgs) Handles cboReportName.DropDown
        cboReportName.Text = ""
        cboReportName.Items.Clear()

        Dim dt As New DataTable
        dt = _CustomReportController.GetCustomReportByStr("")
        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                cboReportName.Items.Add(dt.Rows(i).Item("ReportName"))
            Next
        End If
    End Sub

    Private Sub cboReportName_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboReportName.SelectedValueChanged
        Dim dt As New DataTable
        dt = _CustomReportController.GetCustomReportByStr(" and ReportName ='" & cboReportName.Text.Trim & "'")

        If dt.Rows.Count > 0 Then
            ReportCode = dt.Rows(0).Item("ReportCode").ToString

            If IIf(IsDBNull(dt.Rows(0).Item("CriCustomer")), False, dt.Rows(0).Item("CriCustomer")) = True Then
                grpCustomer.Visible = True
                chkCustomerName.Visible = True

            End If

            If IIf(IsDBNull(dt.Rows(0).Item("CriGoldQuality")), False, dt.Rows(0).Item("CriGoldQuality")) = True Then
                grpGQ.Visible = True
                chkGoldQ.Visible = True
                GetGoldQuality()
            End If

            If IIf(IsDBNull(dt.Rows(0).Item("CriItemCategory")), False, dt.Rows(0).Item("CriItemCategory")) = True Then
                grpGQ.Visible = True
                chkItemCat.Visible = True
                GetItemCategory()
            End If

            If IIf(IsDBNull(dt.Rows(0).Item("CriGemsCategory")), False, dt.Rows(0).Item("CriGemsCategory")) = True Then
                grpGQ.Visible = True
                chkGems.Visible = True
                GetGemsCategory()
            End If

            If IIf(IsDBNull(dt.Rows(0).Item("CriStaff")), False, dt.Rows(0).Item("CriStaff")) = True Then
                grpCustomer.Visible = True
                chkStaff.Visible = True
                GetStaff()
            End If

            If IIf(IsDBNull(dt.Rows(0).Item("CriItemName")), False, dt.Rows(0).Item("CriItemName")) = True Then
                grpGQ.Visible = True
                chkItemName.Visible = True
                chkItemName.Enabled = False
                ' GetItemName()
            End If

            If IIf(IsDBNull(dt.Rows(0).Item("CriFromDate")), False, dt.Rows(0).Item("CriFromDate")) = True Then
                grpDate.Visible = True
                dtpFromDate.Visible = True
                dtpFromDate.Value = Now.Date
            End If

            If IIf(IsDBNull(dt.Rows(0).Item("CriToDate")), False, dt.Rows(0).Item("CriToDate")) = True Then
                grpDate.Visible = True
                dtpToDate.Visible = True
                dtpToDate.Value = Now.Date
            End If

        End If
    End Sub

    Private Sub GetGoldQuality()
        cboGoldQ.DisplayMember = "GoldQuality"
        cboGoldQ.ValueMember = "@GoldQualityID"
        cboGoldQ.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView
    End Sub

    Private Sub GetItemCategory()
        cboItemCat.DisplayMember = "ItemCategory_"
        cboItemCat.ValueMember = "@ItemCategoryID"
        cboItemCat.DataSource = _ItemCatController.GetAllItemCategory().DefaultView
    End Sub

    Private Sub GetGemsCategory()
        cboGemsCategory.DisplayMember = "GemsCategory_"
        cboGemsCategory.ValueMember = "@GemsCategoryID"
        cboGemsCategory.DataSource = _GemsCategoryController.GetAllGemsCategory().DefaultView
    End Sub

    Private Sub GetStaff()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _StaffController.GetStaffList().DefaultView
    End Sub

    Private Sub cboItemCat_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCat.SelectedValueChanged
        Dim itemid As String
        itemid = cboItemCat.SelectedValue
        RefreshItemNameCbo(itemid)
    End Sub

    Private Sub RefreshItemNameCbo(ByVal ItemID As String)
        Dim dt As New DataTable
        dt = _ItemNameCon.GetItemNameListByItemCategory(ItemID)
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
    Private Sub chkCustomerName_CheckedChanged(sender As Object, e As EventArgs) Handles chkCustomerName.CheckedChanged
        If chkCustomerName.Checked Then
            btnCustomer.Visible = True
            txtCustomerCode.Visible = True
            txtCustomerName.Visible = True
        Else
            btnCustomer.Visible = False
            txtCustomerCode.Visible = False
            txtCustomerName.Visible = False
            _CustomerID = ""
        End If
    End Sub

    Private Sub chkGems_CheckedChanged(sender As Object, e As EventArgs) Handles chkGems.CheckedChanged
        If chkGems.Checked Then
            cboGemsCategory.Visible = True
        Else
            cboGemsCategory.Visible = False
            cboGemsCategory.SelectedValue = -1
        End If
    End Sub

    Private Sub chkGoldQ_CheckedChanged(sender As Object, e As EventArgs) Handles chkGoldQ.CheckedChanged
        If chkGoldQ.Checked Then
            cboGoldQ.Visible = True
        Else
            cboGoldQ.Visible = False
            cboGoldQ.SelectedValue = -1
        End If
    End Sub

    Private Sub chkItemCat_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemCat.CheckedChanged
        If chkItemCat.Checked Then
            cboItemCat.Visible = True
            chkItemName.Enabled = True
        Else
            cboItemCat.Visible = False
            chkItemName.Enabled = False
            cboItemCat.SelectedValue = -1
        End If
    End Sub

    Private Sub chkStaff_CheckedChanged(sender As Object, e As EventArgs) Handles chkStaff.CheckedChanged
        If chkStaff.Checked Then
            cboStaff.Visible = True
        Else
            cboStaff.Visible = False
            cboStaff.SelectedValue = -1
        End If
    End Sub

    Private Sub btnCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomer.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow

        dt = _CustomerController.GetAllCustomer()
        DataItem = DirectCast(SearchData.FindFast(dt, "Customer List"), DataRow)
        If DataItem IsNot Nothing Then
            _CustomerID = DataItem("@CustomerID")
            txtCustomerCode.Text = DataItem("CustomerCode")
            txtCustomerName.Text = DataItem("CustomerName_")
        End If
    End Sub

    Private Sub chkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemName.CheckedChanged
        If chkItemName.Checked Then
            cboItemName.Visible = True
        Else
            cboItemName.Visible = False
            cboItemName.SelectedValue = -1
        End If
    End Sub
End Class