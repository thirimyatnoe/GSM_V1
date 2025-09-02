Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
Imports System.String
Imports System.Windows
Imports Operational.AppConfiguration

Public Class frm_rpt_MasterSetup
    'Private _Customer As Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _Customer As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _Gemscategory As BusinessRule.GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _GoldQuality As BusinessRule.GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCat As BusinessRule.ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _ItemName As BusinessRule.ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _Staff As BusinessRule.Staff.IStaffController = Factory.Instance.CreateStaffController

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


    Private Sub frm_rpt_Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      
        Me.ReportViewer1.RefreshReport()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim StrcurRDLC As String = ""


        If rbtCustomer.Checked Then
            dt = _Customer.GetCustomer
            StrcurRDLC = "UI.rpt_Customer.rdl"
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportEmbeddedResource = StrcurRDLC
            ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_Customer.rdl"
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsCustomer_Customer", dt))
        ElseIf rbtGemsCategory.Checked Then
            dt = _Gemscategory.GetrptGemsCategory
            StrcurRDLC = "UI.rpt_GemsCategory.rdl"
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportEmbeddedResource = StrcurRDLC
            ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_GemsCategory.rdl"
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsCustomer_GemsCategory", dt))
        ElseIf rbtGoldQuality.Checked Then
            dt = _GoldQuality.GetrptGoldQuality
            StrcurRDLC = "UI.rpt_GoldQuality.rdl"
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportEmbeddedResource = StrcurRDLC
            ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_GoldQuality.rdl"
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsCustomer_GoldQuality", dt))
        ElseIf rbtItemCategory.Checked Then
            dt = _ItemCat.GetrptItemCategory
            StrcurRDLC = "UI.rpt_ItemCategory.rdl"
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportEmbeddedResource = StrcurRDLC
            ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_ItemCategory.rdl"
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsCustomer_ItemCategory", dt))
        ElseIf rbtItemName.Checked Then
            dt = _ItemName.GetrptItemName
            StrcurRDLC = "UI.rpt_ItemName.rdl"
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportEmbeddedResource = StrcurRDLC
            ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_ItemName.rdl"
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsCustomer_ItemName", dt))
        ElseIf rbtSalesStaffs.Checked Then
            dt = _Staff.GetrptStaff
            StrcurRDLC = "UI.rpt_Staff.rdl"
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportEmbeddedResource = StrcurRDLC
            ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_Staff.rdl"
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsCustomer_Staff", dt))
        End If
        ReportViewer1.RefreshReport()

    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("MasterSetupReport")
    End Sub

    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class