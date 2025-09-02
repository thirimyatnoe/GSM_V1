Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
'Imports System.Configuration
'Imports Operational
'Imports System.IO
Public Class frm_rpt_StockCardReport
    '  Private Shared config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

    Private ReportDA As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _GoldQ As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCat As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
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

    Private Sub frm_rpt_SaleItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboGoldQ.Enabled = False
        cboItemCat.Enabled = False
        chkItemName.Checked = False
        chkItemName.Enabled = False
        cboItemName.Enabled = False
        radAll.Checked = True
        CboLocation.Enabled = True
        ChkLocation.Checked = True
        GetCombo()
        CboLocation.SelectedValue = Global_CurrentLocationID
        Me.RptViewer.RefreshReport()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click

        'Dim FileMap As ExeConfigurationFileMap
        'FileMap = New ExeConfigurationFileMap
        'FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        'config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim dt As New DataTable
        If ChkLocation.Checked Then
            dt = ReportDA.GetStockItemCardReport(dtpFromDate.Value, dtpToDate.Value, GetFilterString)
        Else
            MsgBox("Please Select Location.", MsgBoxStyle.Information, AppName)
        End If
        If dt.Rows.Count() = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
        End If




        RptViewer.Reset()
        RptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_StockCardReport.rdlc"
        '   RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_StockCardReport.rdl"
        RptViewer.LocalReport.DataSources.Clear()
        RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleItem_SaleItem", dt))

        Dim Title As String = ""
        Dim CTitle(0) As ReportParameter
        Dim G_PToY(0) As ReportParameter
        If radAll.Checked Then
            Title = "အထည်ကဒ်အဝင်စာရင်းချုပ်(All)"
        ElseIf radStock.Checked Then
            Title = "အထည်ကဒ်အဝင်စာရင်းချုပ်(ရွှေထည်)"
        ElseIf radDiamondStock.Checked Then
            Title = "အထည်ကဒ်အဝင်စာရင်းချုပ်(စိန်ထည်)"
        End If

        CTitle(0) = New ReportParameter("CTitle", Title)
        RptViewer.LocalReport.SetParameters(CTitle)

        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        RptViewer.LocalReport.SetParameters(G_PToY)
        RptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If (chkGoldQly.Checked) Then
            GetFilterString += " And F.GoldQualityID = '" & cboGoldQ.SelectedValue & "'"
        End If
        If (chkItemCat.Checked) Then
            GetFilterString += " And F.ItemCategoryID = '" & cboItemCat.SelectedValue & "'"
        End If

        If (chkItemName.Checked) Then
            GetFilterString += " And F.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If
        If radStock.Checked Then
            GetFilterString += " And F.IsDiamond = 0"
        End If
        If radDiamondStock.Checked Then
            GetFilterString += " And F.IsDiamond = 1"
        End If

        If chkBalance.Checked Then
            GetFilterString += " And F.IsExit = 0"
        End If
        If chkSale.Checked Then
            GetFilterString += " And F.IsExit = 1"
        End If
        If (ChkLocation.Checked) Then
            GetFilterString += " And F.LocationID = '" & CboLocation.SelectedValue & "'"
        End If
    End Function
    Private Sub GetCombo()
        cboGoldQ.DisplayMember = "GoldQuality"
        cboGoldQ.ValueMember = "@GoldQualityID"
        cboGoldQ.DataSource = _GoldQ.GetAllGoldQuality().DefaultView

        cboItemCat.DisplayMember = "ItemCategory_"
        cboItemCat.ValueMember = "@ItemCategoryID"
        cboItemCat.DataSource = _ItemCat.GetAllItemCategory().DefaultView

        CboLocation.DisplayMember = "Location_"
        CboLocation.ValueMember = "@LocationID"
        CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
    End Sub


    Private Sub chkGoldQly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQly.CheckedChanged
        If (chkGoldQly.Checked) Then
            cboGoldQ.Enabled = True
        Else
            cboGoldQ.Enabled = False
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("SalesItemReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
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

    Private Sub chkItemName_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemName.CheckedChanged
        If (chkItemName.Checked) Then
            cboItemName.Enabled = True
        Else
            cboItemName.Enabled = False
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
    Private Sub chkItemCat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCat.CheckedChanged
        If (chkItemCat.Checked) Then
            chkItemName.Enabled = True
            cboItemCat.Enabled = True
        Else
            cboItemCat.Enabled = False
            chkItemName.Enabled = False
            chkItemName.Checked = False
            cboItemName.Enabled = False
        End If
    End Sub


    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        openhelp("StockCardReport")
    End Sub
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub
End Class