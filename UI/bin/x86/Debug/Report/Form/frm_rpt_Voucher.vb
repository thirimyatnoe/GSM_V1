Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Public Class frm_rpt_Voucher
    Private ReportDA As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private _LocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _OrderInvoiceController As OrderInvoice.IOrderInvoiceController = Factory.Instance.CreateOrderInvoiceController
    Private objSalesVolumeController As SalesVolume.ISalesVolumeController = Factory.Instance.CreateSalesVolumeController
    Private objPurItemController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private _SaleGems As SaleGems.ISaleGemsController = Factory.Instance.CreateSaleGemsController
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

    Private Sub frm_rpt_CashReceipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.rptViewer.RefreshReport()
        chkByVoucher.Enabled = True
        chkByVoucher.Checked = False
        'GroupVoucher.Enabled = False
        lblVoucherNo.Enabled = False
        txtVoucherNo.Enabled = False
        radPurchase.Checked = True
        radPurchaseGem.Focus()
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim curRDLC As String = ""
        Dim PhotoPath As String = ""
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If radSaleInvoice.Checked Then
            dt = ReportDA.GetAllSaleInvoiceVoucherPrint(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                rptViewer.Reset()
                'Exit Sub
            End If
            If dt.Rows.Count() = 0 Then
                MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                'Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    If Not (IsDBNull(dr.Item("Photo"))) Then
                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                    Else
                        dr.Item("Photo") = ""
                    End If

                Next
            End If
            rptViewer.Reset()
            'rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleInvoiceVoucher.rdlc"
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleInvoiceVoucher.rdl"
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleInvoiceVoucher.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))
            rptViewer.LocalReport.EnableExternalImages = True
            'Dim PhotoOne(0) As ReportParameter
            'PhotoPath = Global_PhotoPath & "\"
        ElseIf radOrderInvoice.Checked Then

            dt = _OrderInvoiceController.GetAllOrderInvoiceVoucherPrint(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                rptViewer.Reset()
                'Exit Sub
            End If
            rptViewer.Reset()
            'rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderInvoiceVoucher.rdlc"
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderInvoiceVoucher.rdl"
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_OrderInvoiceVoucher.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceItem_OrderInvoiceItem", dt))
        ElseIf radSaleVolume.Checked Then

            dt = objSalesVolumeController.GetAllSalesVolumeVoucherPrint(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                rptViewer.Reset()
                ' Exit Sub
            End If
            rptViewer.Reset()
            'rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleVolumeVoucherPrint.rdlc"
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleVolumeVoucherPrint.rdl"
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleVolumeVoucherPrint.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSalesVolumeInvoice_SalesVolumeInvoice", dt))
        ElseIf radPurchase.Checked Then

            dt = objPurItemController.GetAllPurchaseInvoiceVoucherPrint(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                rptViewer.Reset()
                '  Exit Sub
            End If
            rptViewer.Reset()
            'rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseInvoiceVoucher.rdlc"
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseInvoiceVoucher.rdl"
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_PurchaseInvoiceVoucher.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
        ElseIf radSaleGems.Checked Then
            dt = _SaleGems.GetAllSaleGemsVoucherPrint(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                rptViewer.Reset()
                '  Exit Sub
            End If
            rptViewer.Reset()
            'rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleGemsVoucherPrint.rdlc"
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleGemsVoucherPrint.rdl"
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleGemsVoucherPrint.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleGems_SaleGems", dt))
        ElseIf radOrderReturn.Checked Then
            dt = _OrderInvoiceController.GetAllOrderReturnVoucherPrint(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dt.Rows.Count() = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                ' Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                Next
            End If
            'rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderReturnVoucherPrint.rdlc"
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderReturnVoucherPrint.rdl"
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_OrderReturnVoucherPrint.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoice_OrderInvoice", dt))
            rptViewer.LocalReport.EnableExternalImages = True

        ElseIf radPurchaseGem.Checked Then
            dt = objPurItemController.GetAllPurchaseGemsVoucherPrint(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            If dt.Rows.Count = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, AppName)
                rptViewer.Reset()
                ' Exit Sub
            End If
            rptViewer.Reset()
            'rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGemVoucherPrint.rdlc"
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGemVoucherPrint.rdl"
            rptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_PurchaseGemVoucherPrint.rdl"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
        End If

        rptViewer.RefreshReport()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If radSaleInvoice.Checked Then
            If (chkByVoucher.Checked) Then
                GetFilterString += " And H.SaleInvoiceHeaderID = '" & txtVoucherNo.Text.Trim & "'"
            End If
        ElseIf radOrderInvoice.Checked Then
            If (chkByVoucher.Checked) Then
                GetFilterString += " And O.OrderInvoiceID = '" & txtVoucherNo.Text.Trim & "'"
            End If
        ElseIf radSaleVolume.Checked Then
            If (chkByVoucher.Checked) Then
                GetFilterString += " And H.SalesVolumeID = '" & txtVoucherNo.Text.Trim & "'"
            End If

        ElseIf radPurchase.Checked Then
            If (chkByVoucher.Checked) Then
                GetFilterString += " And H.PurchaseHeaderID = '" & txtVoucherNo.Text.Trim & "'"
            End If
        ElseIf radSaleGems.Checked Then
            If (chkByVoucher.Checked) Then
                GetFilterString += " And H.SaleGemsID = '" & txtVoucherNo.Text.Trim & "'"
            End If
        ElseIf radOrderReturn.Checked Then
            If (chkByVoucher.Checked) Then
                GetFilterString += " And H.OrderInvoiceID = '" & txtVoucherNo.Text.Trim & "'"
            End If
        ElseIf radPurchaseGem.Checked Then
            If (chkByVoucher.Checked) Then
                GetFilterString += " And PH.PurchaseHeaderID = '" & txtVoucherNo.Text.Trim & "'"
            End If
        End If
    End Function

    Private Sub chkByVoucher_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkByVoucher.CheckedChanged
        If (chkByVoucher.Checked) Then
            lblVoucherNo.Enabled = True
            txtVoucherNo.Enabled = True
        Else
            lblVoucherNo.Enabled = False
            txtVoucherNo.Enabled = False
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("VoucherReport")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class