Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports System.Configuration
Imports Operational
Imports System.IO
Public Class frm_rpt_SaleItem
    Private Shared config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

    Private ReportDA As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _ConverterCon As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GoldQ As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCat As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _Loc As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _Location As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _GoldSmith As GoldSmith.IGoldSmithController = Factory.Instance.CreateGoldSmithController
    Private _StaffCon As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _Supplier As Supplier.ISupplierController = Factory.Instance.CreateSupplierController
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
    Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboLocation, e)
    End Sub

    Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboLocation, "")
    End Sub
    Private Sub cboLocation_SelectedValueChanged(sender As Object, e As EventArgs) Handles CboLocation.SelectedValueChanged

        LocationID = CboLocation.SelectedValue

        LocationID = CboLocation.SelectedValue
        cboItemCat.SelectedValue = ""
        cboGoldQ.SelectedValue = ""
        cboStaff.SelectedValue = ""
        cboGoldSmith.SelectedValue = ""
        GetCombo()

    End Sub

    Private Sub cboItemName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboLocation.KeyUp
        AutoCompleteCombo_KeyUp(cboItemName, e)
    End Sub

    Private Sub cboItemName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.Leave
        AutoCompleteCombo_Leave(cboItemName, "")
    End Sub

#End Region

    Private Sub frm_rpt_SaleItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboGoldQ.Enabled = False
        cboItemCat.Enabled = False

        chkItemName.Checked = False
        chkItemName.Enabled = False
        cboItemName.Enabled = False
        chkStaff.Checked = False
        cboStaff.Enabled = False
        'chkGS.Enabled = False
        cboGoldSmith.Enabled = False
        cboSupplier.Enabled = False
        txtGoldSmith.Text = ""
        txtBarcodeNo.Text = ""
        chkShopItem.Checked = False
        chkGoldSmith.Checked = False
        radAll.Checked = True

        grpSType.Enabled = False
        GetCombo()
        ChkLocation.Enabled = True
        ChkLocation.Checked = True
        chkGram.Checked = False
        txtFG.Enabled = False
        txtFG.Text = ""
        txtFG.BackColor = Color.Gainsboro
        txtTG.BackColor = Color.Gainsboro
        txtTG.Enabled = False
        txtTG.Text = ""
        If Global_IsHeadOffice Then
            CboLocation.SelectedValue = Global_CurrentLocationID
            CboLocation.DisplayMember = "Location_"
            CboLocation.ValueMember = "@LocationID"
            CboLocation.DataSource = _Location.GetAllLocationList().DefaultView


        Else
            CboLocation.DisplayMember = "Location_"
            CboLocation.ValueMember = "@LocationID"
            CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
            CboLocation.SelectedValue = Global_CurrentLocationID
            CboLocation.Enabled = False
            ChkLocation.Enabled = False
        End If
        LocationID = Global_CurrentLocationID

        Me.RptViewer.RefreshReport()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim dt As New DataTable
        Dim dtOrder As New DataTable
        Dim StockStatus As String = ""
        Dim curRDLC As String = ""
        Dim TTitle As String = ""
        Dim filepath As String = ""

        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
            MsgBox("FromDate must be less than ToDate", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

        If radSBalance.Checked = True Then
            StockStatus = "Balance"
        End If
        If radSAll.Checked = True Then
            StockStatus = "All"
        End If
        If radSExit.Checked = True Then
            StockStatus = "Exit"
        End If
        If radVolume.Checked = False And radLooseDiamond.Checked = False Then
            If radDetail.Checked Then
                If ChkLocation.Checked Then
                    ' dt = ReportDA.GetForSaleReportByLocation(GetFilterString, CboLocation.SelectedValue)
                    If optByGivenDate.Checked Then
                        dt = ReportDA.GetForSaleForReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, StockStatus)
                    ElseIf chkIsClosed.Checked Then
                        dt = ReportDA.GetForSaleForIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        If Global_IsHoToBranch And Global_IsHeadOffice And CboLocation.SelectedValue <> Global_CurrentLocationID And optBalanceStocks.Checked Then
                            dt = ReportDA.GetStockBalanceFromHO(GetFilterString, CboLocation.SelectedValue)
                        Else
                            dt = ReportDA.GetForSaleForReport(GetFilterString)
                        End If

                    End If
                Else
                    If optByGivenDate.Checked Then
                        dt = ReportDA.GetForSaleForReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, StockStatus)
                    ElseIf chkIsClosed.Checked Then
                        dt = ReportDA.GetForSaleForIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        dt = ReportDA.GetForSaleForReport(GetFilterString)
                    End If
                End If

                If dt.Rows.Count() = 0 Then
                    MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
                    RptViewer.Reset()
                End If
                If chkByLocation.Checked = True Then
                    curRDLC = "\Report\RDLC\rpt_StockItemByLoc_Detail.rdl"
                Else
                    If radPlatinum.Checked = True Then
                        curRDLC = "\Report\RDLC\rpt_SaleItem_Detail_Platinum.rdl "
                    ElseIf rbtnDiamondStock.Checked Then
                        curRDLC = "\Report\RDLC\rpt_SaleItem_Detail_Diamond.rdl "
                    ElseIf radStock.Checked Then
                        curRDLC = "\Report\RDLC\rpt_SaleItem_Detail_Gold.rdl "
                    Else
                        curRDLC = "\Report\RDLC\rpt_SaleItem_Detail.rdl"
                    End If
                End If

            ElseIf radSummary.Checked Then
                If ChkLocation.Checked Then

                    If optByGivenDate.Checked Then
                        dt = ReportDA.GetForSaleForSummaryReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, StockStatus)
                    ElseIf chkIsClosed.Checked Then
                        dt = ReportDA.GetForSaleDataBySummaryIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        If Global_IsHoToBranch And Global_IsHeadOffice And CboLocation.SelectedValue <> Global_CurrentLocationID And optBalanceStocks.Checked Then
                            dt = ReportDA.GetStockBalanceFromHO(GetFilterString, CboLocation.SelectedValue)
                        Else
                            If CboLocation.SelectedValue = Global_CurrentLocationID Then
                                dt = ReportDA.GetForSaleDataBySummaryReport(GetFilterString)
                            Else
                                dt = ReportDA.GetForSaleDataBySummaryReportByLocation(GetFilterString, CboLocation.SelectedValue)
                            End If
                        End If


                    End If
                Else
                    If optByGivenDate.Checked Then
                        dt = ReportDA.GetForSaleForSummaryReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, StockStatus)
                    ElseIf chkIsClosed.Checked Then
                        dt = ReportDA.GetForSaleDataBySummaryIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    Else
                        dt = ReportDA.GetForSaleDataBySummaryReport(GetFilterString)
                    End If
                End If

                If dt.Rows.Count() = 0 Then
                    MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
                End If

                If optByGivenDate.Checked Then
                    curRDLC = "\Report\RDLC\rpt_StockItemByDate_Summary.rdl"
                Else
                    curRDLC = "\Report\RDLC\rpt_StockItem_Summary.rdl"
                End If

            ElseIf radOverView.Checked Then
                'If ChkLocation.Checked Then
                '    dt = ReportDA.GetForSaleDataBySummaryReportByLocation(GetFilterString, CboLocation.SelectedValue)
                'Else
                If optByGivenDate.Checked Then
                    dt = ReportDA.GetForSaleForSummaryReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, StockStatus)
                    dtOrder = ReportDA.GetForSaleForSummaryReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, " And H.IsOrder = '1' " + GetFilterString(), StockStatus)
                ElseIf chkIsClosed.Checked Then
                    dt = ReportDA.GetForSaleDataBySummaryIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    dtOrder = ReportDA.GetForSaleDataBySummaryIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, " And H.IsOrder = '1' " + GetFilterString())
                Else
                    If Global_IsHoToBranch And Global_IsHeadOffice And CboLocation.SelectedValue <> Global_CurrentLocationID And optBalanceStocks.Checked Then
                        dt = ReportDA.GetStockBalanceFromHO(GetFilterString, CboLocation.SelectedValue)
                    Else
                        dt = ReportDA.GetForSaleDataBySummaryReport(GetFilterString)
                        dtOrder = ReportDA.GetForSaleDataBySummaryReport(" And H.IsOrder = '1' " + GetFilterString())
                    End If

                End If
                'End If
                If dt.Rows.Count() = 0 Then
                    MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
                End If
                curRDLC = "\Report\RDLC\rpt_StockItemOverViewSummary.rdl"
            ElseIf radByGoldQuality.Checked Then

                If optByGivenDate.Checked Then
                    dt = ReportDA.GetForSaleForSummaryReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString, StockStatus)
                    dtOrder = ReportDA.GetForSaleForSummaryReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, " And H.IsOrder = '1' " + GetFilterString(), StockStatus)
                ElseIf chkIsClosed.Checked Then
                    dt = ReportDA.GetForSaleDataBySummaryIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
                    dtOrder = ReportDA.GetForSaleDataBySummaryIsCloseReport(dtpFromDate.Value.Date, dtpToDate.Value.Date, " And H.IsOrder = '1' " + GetFilterString())
                Else
                    If Global_IsHoToBranch And Global_IsHeadOffice And CboLocation.SelectedValue <> Global_CurrentLocationID And optBalanceStocks.Checked Then
                        dt = ReportDA.GetStockBalanceFromHO(GetFilterString, CboLocation.SelectedValue)
                    Else
                        dt = ReportDA.GetForSaleDataBySummaryReport(GetFilterString)
                        dtOrder = ReportDA.GetForSaleDataBySummaryReport(" And H.IsOrder = '1' " + GetFilterString())
                    End If

                End If
                'End If
                If dt.Rows.Count() = 0 Then
                    MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
                End If
                curRDLC = "\Report\RDLC\rpt_SaleItem_Report_ByGoldQuality.rdl"
            End If
            RptViewer.Reset()
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & curRDLC
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleItem_SaleItem", dt))
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleItem_SaleItemforOrder", dtOrder))
        ElseIf radVolume.Checked = True Then
            'If ChkLocation.Checked Then
            If optByGivenDate.Checked Then
                dt = ReportDA.GetForSaleVolumeForReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            Else
                dt = ReportDA.GetForSaleVolumeForReport(GetFilterString)
            End If
            'Else
            'MsgBox("Please Select Location.", MsgBoxStyle.Information, Me.Text)
            'End If
            If dt.Rows.Count() = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
                RptViewer.Reset()
            End If

            If radSummary.Checked Then
                If optBalanceStocks.Checked Then
                    curRDLC = "\Report\RDLC\rpt_BalanceStockForVolume_Summary.rdl"
                ElseIf optByGivenDate.Checked Then
                    curRDLC = "\Report\RDLC\rpt_VolumeStockByGivenDate_Summary.rdl"
                Else
                    curRDLC = "\Report\RDLC\rpt_StockItemVolume_Summary.rdl"
                End If

            Else
                If optBalanceStocks.Checked Then
                    curRDLC = " \Report\RDLC\rpt_VolumeBalanceStock.rdl"
                Else
                    curRDLC = "\Report\RDLC\rpt_SaleItemVolume_Detail.rdl"
                End If
            End If

            RptViewer.Reset()
            '   RptViewer.LocalReport.ReportEmbeddedResource = curRDLC
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & curRDLC
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleItem_SaleItem", dt))
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleItem_SaleItemforOrder", dtOrder))
        Else
            'If ChkLocation.Checked Then
            If optByGivenDate.Checked Then
                dt = ReportDA.GetForSaleLooseDiamondForReportByDatePeriod(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            Else
                dt = ReportDA.GetForSaleLooseDiamondForReport(GetFilterString)
            End If
            'Else
            'MsgBox("Please Select Location.", MsgBoxStyle.Information, Me.Text)
            'End If
            If dt.Rows.Count() = 0 Then
                MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
                RptViewer.Reset()
            End If

            If radSummary.Checked Then
                If optBalanceStocks.Checked Then
                    curRDLC = "\Report\RDLC\rpt_BalanceStockForLooseDiamond_Summary.rdl"
                ElseIf optByGivenDate.Checked Then
                    curRDLC = "\Report\RDLC\rpt_LooseDiamondStockByGivenDate_Summary.rdl"
                Else
                    curRDLC = "\Report\RDLC\rpt_StockItemLooseDiamond_Summary.rdl"
                End If

            Else
                If optBalanceStocks.Checked Then
                    curRDLC = " \Report\RDLC\rpt_LooseDiamondBalanceStock.rdl"
                Else
                    curRDLC = "\Report\RDLC\rpt_SaleItemLooseDiamond_Detail.rdl"
                End If
            End If

            RptViewer.Reset()
            '   RptViewer.LocalReport.ReportEmbeddedResource = curRDLC
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & curRDLC
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleItem_SaleItem", dt))
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleItem_SaleItemforOrder", dtOrder))
        End If

        If radVolume.Checked = False And radDetail.Checked And rbtnDiamondStock.Checked And radLooseDiamond.Checked = False Then
            Dim QTY(0) As ReportParameter
            Dim ItemTK(0) As ReportParameter
            Dim ItemTG(0) As ReportParameter
            Dim GoldTK(0) As ReportParameter
            Dim GoldTG(0) As ReportParameter
            Dim WasteTK(0) As ReportParameter
            Dim WasteTG(0) As ReportParameter
            Dim GemsTK(0) As ReportParameter
            Dim GemsTG(0) As ReportParameter
            Dim FixPrice(0) As ReportParameter

            Dim dtTotal As New DataTable
            Dim TotalQTY As Integer = 0
            Dim TItemTK As Decimal = 0.0
            Dim TItemTG As Decimal = 0.0
            Dim TGoldTK As Decimal = 0.0
            Dim TGoldTG As Decimal = 0.0
            Dim TWasteTK As Decimal = 0.0
            Dim TWasteTG As Decimal = 0.0
            Dim TGemsTK As Decimal = 0.0
            Dim TGemsTG As Decimal = 0.0
            Dim TotalFixPrice As String = "0"

            If optByGivenDate.Checked Then
                dtTotal = ReportDA.GetForSaleForReportByDatePeriodAndTotalWeight(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            Else
                If Global_IsHoToBranch And Global_IsHeadOffice And CboLocation.SelectedValue <> Global_CurrentLocationID And optBalanceStocks.Checked Then
                    dtTotal = ReportDA.GetBalanceFromHOByTotalWeight(GetFilterString, CboLocation.SelectedValue)
                Else
                    dtTotal = ReportDA.GetForSaleForReportByTotalWeight(GetFilterString)
                End If
            End If

            For Each dr As DataRow In dtTotal.Rows
                TotalQTY = dr.Item("TotalQTY")
                TItemTK = IIf(IsDBNull(dr.Item("ItemTK")) = True, 0, dr.Item("ItemTK"))
                TItemTG = IIf(IsDBNull(dr.Item("ItemTG")) = True, 0, dr.Item("ItemTG"))
                TGoldTK = IIf(IsDBNull(dr.Item("GoldTK")) = True, 0, dr.Item("GoldTK"))
                TGoldTG = IIf(IsDBNull(dr.Item("GoldTG")) = True, 0, dr.Item("GoldTG"))
                TWasteTK = IIf(IsDBNull(dr.Item("WasteTK")) = True, 0, dr.Item("WasteTK"))
                TWasteTG = IIf(IsDBNull(dr.Item("WasteTG")) = True, 0, dr.Item("WasteTG"))
                TGemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                TGemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
                TotalFixPrice = IIf(IsDBNull(dr.Item("TotalFixPrice")) = True, 0, dr.Item("TotalFixPrice"))
            Next

            QTY(0) = New ReportParameter("QTY", TotalQTY)
            RptViewer.LocalReport.SetParameters(QTY)

            ItemTK(0) = New ReportParameter("ItemTK", TItemTK)
            RptViewer.LocalReport.SetParameters(ItemTK)

            ItemTG(0) = New ReportParameter("ItemTG", TItemTG)
            RptViewer.LocalReport.SetParameters(ItemTG)

            GoldTK(0) = New ReportParameter("GoldTK", TGoldTK)
            RptViewer.LocalReport.SetParameters(GoldTK)

            GoldTG(0) = New ReportParameter("GoldTG", TGoldTG)
            RptViewer.LocalReport.SetParameters(GoldTG)

            WasteTK(0) = New ReportParameter("WasteTK", TWasteTK)
            RptViewer.LocalReport.SetParameters(WasteTK)

            WasteTG(0) = New ReportParameter("WasteTG", TWasteTG)
            RptViewer.LocalReport.SetParameters(WasteTG)

            GemsTK(0) = New ReportParameter("GemsTK", TGemsTK)
            RptViewer.LocalReport.SetParameters(GemsTK)

            GemsTG(0) = New ReportParameter("GemsTG", TGemsTG)
            RptViewer.LocalReport.SetParameters(GemsTG)

            FixPrice(0) = New ReportParameter("FixPrice", TotalFixPrice)
            RptViewer.LocalReport.SetParameters(FixPrice)
        End If

        If radVolume.Checked = False And radDetail.Checked And rbtnDiamondStock.Checked = False And radLooseDiamond.Checked = False Then
            Dim QTY(0) As ReportParameter
            Dim ItemTK(0) As ReportParameter
            Dim ItemTG(0) As ReportParameter
            Dim GoldTK(0) As ReportParameter
            Dim GoldTG(0) As ReportParameter
            Dim WasteTK(0) As ReportParameter
            Dim WasteTG(0) As ReportParameter
            Dim GemsTK(0) As ReportParameter
            Dim GemsTG(0) As ReportParameter

            Dim dtTotal As New DataTable
            Dim TotalQTY As Integer = 0
            Dim TItemTK As Decimal = 0.0
            Dim TItemTG As Decimal = 0.0
            Dim TGoldTK As Decimal = 0.0
            Dim TGoldTG As Decimal = 0.0
            Dim TWasteTK As Decimal = 0.0
            Dim TWasteTG As Decimal = 0.0
            Dim TGemsTK As Decimal = 0.0
            Dim TGemsTG As Decimal = 0.0

            If optByGivenDate.Checked Then
                dtTotal = ReportDA.GetForSaleForReportByDatePeriodAndTotalWeight(dtpFromDate.Value.Date, dtpToDate.Value.Date, GetFilterString)
            Else
                If Global_IsHoToBranch And Global_IsHeadOffice And CboLocation.SelectedValue <> Global_CurrentLocationID And optBalanceStocks.Checked Then
                    dtTotal = ReportDA.GetBalanceFromHOByTotalWeight(GetFilterString, CboLocation.SelectedValue)
                Else
                    dtTotal = ReportDA.GetForSaleForReportByTotalWeight(GetFilterString)
                End If

            End If

            For Each dr As DataRow In dtTotal.Rows
                TotalQTY = dr.Item("TotalQTY")
                TItemTK = IIf(IsDBNull(dr.Item("ItemTK")) = True, 0, dr.Item("ItemTK"))
                TItemTG = IIf(IsDBNull(dr.Item("ItemTG")) = True, 0, dr.Item("ItemTG"))
                TGoldTK = IIf(IsDBNull(dr.Item("GoldTK")) = True, 0, dr.Item("GoldTK"))
                TGoldTG = IIf(IsDBNull(dr.Item("GoldTG")) = True, 0, dr.Item("GoldTG"))
                TWasteTK = IIf(IsDBNull(dr.Item("WasteTK")) = True, 0, dr.Item("WasteTK"))
                TWasteTG = IIf(IsDBNull(dr.Item("WasteTG")) = True, 0, dr.Item("WasteTG"))
                TGemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                TGemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
            Next

            QTY(0) = New ReportParameter("QTY", TotalQTY)
            RptViewer.LocalReport.SetParameters(QTY)

            ItemTK(0) = New ReportParameter("ItemTK", TItemTK)
            RptViewer.LocalReport.SetParameters(ItemTK)

            ItemTG(0) = New ReportParameter("ItemTG", TItemTG)
            RptViewer.LocalReport.SetParameters(ItemTG)

            GoldTK(0) = New ReportParameter("GoldTK", TGoldTK)
            RptViewer.LocalReport.SetParameters(GoldTK)

            GoldTG(0) = New ReportParameter("GoldTG", TGoldTG)
            RptViewer.LocalReport.SetParameters(GoldTG)

            WasteTK(0) = New ReportParameter("WasteTK", TWasteTK)
            RptViewer.LocalReport.SetParameters(WasteTK)

            WasteTG(0) = New ReportParameter("WasteTG", TWasteTG)
            RptViewer.LocalReport.SetParameters(WasteTG)

            GemsTK(0) = New ReportParameter("GemsTK", TGemsTK)
            RptViewer.LocalReport.SetParameters(GemsTK)

            GemsTG(0) = New ReportParameter("GemsTG", TGemsTG)
            RptViewer.LocalReport.SetParameters(GemsTG)

        End If


        Dim Title(0) As ReportParameter
        If radStock.Checked Then
            If radDetail.Checked Then
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်း ပစ္စည်းစာရင်း(ရွှေထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်းလက်ကျန် ပစ္စည်းစာရင်း(ရွှေထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်ရောင်းပစ္စည်းစာရင်း(ရွှေထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If

            Else
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း ပစ္စည်းစာရင်းချုပ်(ရွှေထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း လက်ကျန်ပစ္စည်းစာရင်းချုပ်(ရွှေထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်​​ရောင်းပစ္စည်းစာရင်းချုပ်(ရွှေထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            End If
        ElseIf radAll.Checked Then
            If radDetail.Checked Then
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်း ပစ္စည်းစာရင်း(All)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်းလက်ကျန် ပစ္စည်းစာရင်း(All)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်ရောင်းပစ္စည်းစာရင်း(All)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If

            Else
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း ပစ္စည်းစာရင်းချုပ်(All)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း လက်ကျန်ပစ္စည်းစာရင်းချုပ်(All)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်​​ရောင်းပစ္စည်းစာရင်းချုပ်(All)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            End If
        ElseIf radVolume.Checked Then
            If radDetail.Checked Then
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်း ပစ္စည်းစာရင်း(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်းလက်ကျန် ပစ္စည်းစာရင်း(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်ရောင်းပစ္စည်းစာရင်း(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If

            Else
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း ပစ္စည်းစာရင်းချုပ်(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း လက်ကျန်ပစ္စည်းစာရင်းချုပ်(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်​​ရောင်းပစ္စည်းစာရင်းချုပ်(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            End If
        ElseIf radLooseDiamond.Checked Then
            If radDetail.Checked Then
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်း ပစ္စည်းစာရင်း(LooseDiamond)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်းလက်ကျန် ပစ္စည်းစာရင်း(LooseDiamond)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်ရောင်းပစ္စည်းစာရင်း(LooseDiamond)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If

            Else
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း ပစ္စည်းစာရင်းချုပ်(LooseDiamond)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း လက်ကျန်ပစ္စည်းစာရင်းချုပ်(LooseDiamond)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်​​ရောင်းပစ္စည်းစာရင်းချုပ်(LooseDiamond)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            End If
        ElseIf rbtnDiamondStock.Checked Then
            If radDetail.Checked Then
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်း ပစ္စည်းစာရင်း(စိန်ထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်းလက်ကျန် ပစ္စည်းစာရင်း(စိန်ထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်ရောင်းပစ္စည်းစာရင်း(စိန်ထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            Else
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း ပစ္စည်းစာရင်းချုပ်(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း လက်ကျန်ပစ္စည်းစာရင်းချုပ်(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်​​ရောင်းပစ္စည်းစာရင်းချုပ်(Volume)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            End If

        ElseIf radPlatinum.Checked Then
            If radDetail.Checked Then
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်း ပစ္စည်းစာရင်း(ပလက်တီနမ်)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်ရောင်းလက်ကျန် ပစ္စည်းစာရင်း(ပလက်တီနမ်)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်ရောင်းပစ္စည်းစာရင်း(ပလက်တီနမ်)")
                    RptViewer.LocalReport.SetParameters(Title)


                End If




            Else
                If optAllStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း ပစ္စည်းစာရင်းချုပ်(စိန်ထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                ElseIf optBalanceStocks.Checked Then
                    Title(0) = New ReportParameter("Title", "ဆိုင်တင်​​ရောင်း လက်ကျန်ပစ္စည်းစာရင်းချုပ်(စိန်ထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                Else
                    Title(0) = New ReportParameter("Title", "နေ့စွဲအလိုက် ဆိုင်တင်​​ရောင်းပစ္စည်းစာရင်းချုပ်(စိန်ထည်)")
                    RptViewer.LocalReport.SetParameters(Title)
                End If
            End If
        End If

        If radByGoldQuality.Checked Then
            Dim IsDiamond(0) As ReportParameter
            If rbtnDiamondStock.Checked Then
                IsDiamond(0) = New ReportParameter("IsDiamond", True)
                RptViewer.LocalReport.SetParameters(IsDiamond)
            Else
                IsDiamond(0) = New ReportParameter("IsDiamond", False)
                RptViewer.LocalReport.SetParameters(IsDiamond)
            End If

        End If

        Dim ByDate(0) As ReportParameter
        If optByGivenDate.Checked Then
            ByDate(0) = New ReportParameter("ByDate", "ShowDate")
            RptViewer.LocalReport.SetParameters(ByDate)
        Else
            ByDate(0) = New ReportParameter("ByDate", "None")
            RptViewer.LocalReport.SetParameters(ByDate)
        End If
        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        RptViewer.LocalReport.SetParameters(G_PToY)
        RptViewer.RefreshReport()
    End Sub
    'Private Function GetFilterString() As String
    '    GetFilterString = ""
    '    If Global_IsHoMaster = False Then
    '        If (chkGoldQly.Checked) Then
    '            GetFilterString += " And H.GoldQualityID = '" & cboGoldQ.SelectedValue & "'" & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkItemCat.Checked) Then
    '            GetFilterString += " And H.ItemCategoryID = '" & cboItemCat.SelectedValue & "'" & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If (chkItemName.Checked) Then
    '            GetFilterString += " And H.ItemNameID = '" & cboItemName.SelectedValue & "'" & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkGS.Checked) Then
    '            GetFilterString += " And H.GoldSmithID = '" & cboGoldSmith.SelectedValue & "'" & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If (radStock.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0 AND IsDiamond=0  AND IsGramRate=0 and  H.LocationID= " & "'" & LocationID & "'"
    '        End If
    '        If (ChkLocation.Checked) Then
    '            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
    '        End If
    '        If (radAll.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (radVolume.Checked) Then
    '            GetFilterString += " And H.IsVolume =1 Or H.IsSolidVolume=1" & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If (rbtnDiamondStock.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0 AND IsDiamond=1 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If (radPlatinum.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0 AND IsGramRate=1 AND IsDiamond=0 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If


    '        If (chkIsClosed.Checked) Then
    '            GetFilterString += " And H.IsClosed =1 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkIsOrder.Checked) Then
    '            GetFilterString += " And H.IsOrder =1 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkInactiveStock.Checked) Then
    '            GetFilterString += " And H.IsDelete =1 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkInactiveStock.Checked) = False Then
    '            GetFilterString += " And H.IsDelete =0 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If

    '        If (chkIsFix.Checked) Then
    '            GetFilterString += " And H.IsFixPrice =1 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If (chkSupplier.Checked) Then
    '            GetFilterString += " And H.SupplierID ='" & cboSupplier.SelectedValue & "'" & " and  H.LocationID=" & "'" & LocationID & "'"
    '        End If
    '        If optBalanceStocks.Checked Then
    '            If radStock.Checked Or radAll.Checked Or radPlatinum.Checked Or rbtnDiamondStock.Checked Then
    '                If chkByLocation.Checked = False Then
    '                    GetFilterString += " AND H.IsExit=0 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '                End If
    '            ElseIf radVolume.Checked Then
    '                GetFilterString += " AND H.LossQTY<> 0 " & " and  H.LocationID=" & "'" & LocationID & "'"
    '            End If
    '        End If

    '        If (chkStaff.Checked) Then
    '            GetFilterString += " And H.StaffID ='" & cboStaff.SelectedValue & "'"
    '        End If
    '        If (txtGoldSmith.Text <> "") Then
    '            GetFilterString += " And H.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'"
    '        End If
    '        If (txtBarcodeNo.Text <> "") Then
    '            GetFilterString += " And H.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'"
    '        End If
    '        If (txtOriginalCode.Text <> "") Then
    '            GetFilterString += " And H.OriginalCode LIKE '%" & txtOriginalCode.Text.Trim & "%'"
    '        End If

    '        If (txtVoucherNo.Text <> "") Then
    '            GetFilterString += " And H.SupplierVou LIKE '%" & txtVoucherNo.Text.Trim & "%'"
    '        End If

    '        If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
    '            GetFilterString += " And H.GoldSmith='' "
    '        End If

    '        If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
    '            GetFilterString += " And H.GoldSmith<>'' "
    '        End If
    '        'If (radSBalance.Checked) Then
    '        '    GetFilterString += " And H.IsExit<>1 "
    '        'End If
    '        If (radSExit.Checked) Then
    '            GetFilterString += " And H.IsExit=1 "
    '        End If
    '        If (radSBalance.Checked) Then
    '            GetFilterString += " And H.IsExit=0 "
    '        End If
    '    Else
    '        If (chkGoldQly.Checked) Then
    '            GetFilterString += " And H.GoldQualityID = '" & cboGoldQ.SelectedValue & "'"
    '        End If
    '        If (chkItemCat.Checked) Then
    '            GetFilterString += " And H.ItemCategoryID = '" & cboItemCat.SelectedValue & "'"
    '        End If

    '        If (chkItemName.Checked) Then
    '            GetFilterString += " And H.ItemNameID = '" & cboItemName.SelectedValue & "'"
    '        End If
    '        If (chkGS.Checked) Then
    '            GetFilterString += " And H.GoldSmithID = '" & cboGoldSmith.SelectedValue & "'"
    '        End If

    '        If (radStock.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0 AND IsDiamond=0  AND IsGramRate=0"
    '        End If
    '        If (ChkLocation.Checked) Then
    '            GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
    '        End If
    '        If (radAll.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0"
    '        End If
    '        If (radVolume.Checked) Then
    '            GetFilterString += " And H.IsVolume =1 Or H.IsSolidVolume=1"
    '        End If

    '        If (rbtnDiamondStock.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0 AND IsDiamond=1"
    '        End If

    '        If (radPlatinum.Checked) Then
    '            GetFilterString += " And H.IsVolume = 0 And H.IsSolidVolume=0 AND IsGramRate=1 AND IsDiamond=0 "
    '        End If


    '        If (chkIsClosed.Checked) Then
    '            GetFilterString += " And H.IsClosed =1"
    '        End If
    '        If (chkIsOrder.Checked) Then
    '            GetFilterString += " And H.IsOrder =1"
    '        End If
    '        If (chkInactiveStock.Checked) Then
    '            GetFilterString += " And H.IsDelete =1"
    '        End If
    '        If (chkInactiveStock.Checked) = False Then
    '            GetFilterString += " And H.IsDelete =0"
    '        End If

    '        If (chkIsFix.Checked) Then
    '            GetFilterString += " And H.IsFixPrice =1"
    '        End If
    '        If (chkSupplier.Checked) Then
    '            GetFilterString += " And H.SupplierID ='" & cboSupplier.SelectedValue & "'"
    '        End If
    '        If optBalanceStocks.Checked Then
    '            If radStock.Checked Or radAll.Checked Or radPlatinum.Checked Or rbtnDiamondStock.Checked Then
    '                If chkByLocation.Checked = False Then
    '                    GetFilterString += " AND H.IsExit=0"
    '                End If
    '            ElseIf radVolume.Checked Then
    '                GetFilterString += " AND H.LossQTY<> 0"
    '            End If
    '        End If

    '        If (chkStaff.Checked) Then
    '            GetFilterString += " And H.StaffID ='" & cboStaff.SelectedValue & "'"
    '        End If
    '        If (txtGoldSmith.Text <> "") Then
    '            GetFilterString += " And H.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'"
    '        End If
    '        If (txtBarcodeNo.Text <> "") Then
    '            GetFilterString += " And H.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'"
    '        End If
    '        If (txtOriginalCode.Text <> "") Then
    '            GetFilterString += " And H.OriginalCode LIKE '%" & txtOriginalCode.Text.Trim & "%'"
    '        End If

    '        If (txtVoucherNo.Text <> "") Then
    '            GetFilterString += " And H.SupplierVou LIKE '%" & txtVoucherNo.Text.Trim & "%'"
    '        End If

    '        If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
    '            GetFilterString += " And H.GoldSmith='' "
    '        End If

    '        If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
    '            GetFilterString += " And H.GoldSmith<>'' "
    '        End If
    '        'If (radSBalance.Checked) Then
    '        '    GetFilterString += " And H.IsExit<>1 "
    '        'End If
    '        If (radSExit.Checked) Then
    '            GetFilterString += " And H.IsExit=1 "
    '        End If
    '        If (radSBalance.Checked) Then
    '            GetFilterString += " And H.IsExit=0 "
    '        End If
    '    End If



    'End Function

    Private Function GetFilterString() As String
        GetFilterString = ""

        If (chkGoldQly.Checked) Then
            GetFilterString += " And H.GoldQualityID = '" & cboGoldQ.SelectedValue & "'"
        End If
        If (chkItemCat.Checked) Then
            GetFilterString += " And H.ItemCategoryID = '" & cboItemCat.SelectedValue & "'"
        End If

        If (chkItemName.Checked) Then
            GetFilterString += " And H.ItemNameID = '" & cboItemName.SelectedValue & "'"
        End If
        If (chkGS.Checked) Then
            GetFilterString += " And H.GoldSmithID = '" & cboGoldSmith.SelectedValue & "'"
        End If

        If (radStock.Checked) Then
            GetFilterString += " And (H.IsVolume = 0 And H.IsSolidVolume=0) AND IsDiamond=0  AND IsGramRate=0"
        End If

        If Global_IsHeadOffice Then
            If (ChkLocation.Checked) Then
                GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
            End If
        Else
            If optBalanceStocks.Checked Then
                If (ChkLocation.Checked) Then
                    GetFilterString += " And H.LocationID = '" & CboLocation.SelectedValue & "'"
                End If
            End If
        End If

        If (radAll.Checked) Then
            GetFilterString += " And (H.IsVolume = 0 And H.IsSolidVolume=0) And H.IsLooseDiamond=0 "
        End If
        If (radVolume.Checked) Then
            GetFilterString += " And (H.IsVolume =1 Or H.IsSolidVolume=1)"
        End If
        If (radLooseDiamond.Checked) Then
            GetFilterString += " And H.IsLooseDiamond=1"
        End If

        If (rbtnDiamondStock.Checked) Then
            GetFilterString += " And (H.IsVolume = 0 And H.IsSolidVolume=0) AND IsDiamond=1"
        End If

        If (radPlatinum.Checked) Then
            GetFilterString += " And (H.IsVolume = 0 And H.IsSolidVolume=0) AND IsGramRate=1 AND IsDiamond=0 And H.IsLooseDiamond=0 "
        End If


        If (chkIsClosed.Checked) Then
            GetFilterString += " And H.IsClosed =1"
        End If
        If (chkIsOrder.Checked) Then
            GetFilterString += " And H.IsOrder =1"
        End If
        If (chkInactiveStock.Checked) Then
            GetFilterString += " And H.IsDelete =1"
        End If
        If (chkInactiveStock.Checked) = False Then
            GetFilterString += " And H.IsDelete =0"
        End If

        If (chkIsFix.Checked) Then
            GetFilterString += " And H.IsFixPrice =1"
        End If
        If (chkSupplier.Checked) Then
            GetFilterString += " And H.SupplierID ='" & cboSupplier.SelectedValue & "'"
        End If
        If optBalanceStocks.Checked Then
            If radStock.Checked Or radAll.Checked Or radPlatinum.Checked Or rbtnDiamondStock.Checked Then
                'If chkByLocation.Checked = False Then
                '    GetFilterString += " AND H.IsExit=0"
                If CboLocation.SelectedValue = Global_CurrentLocationID Then
                    GetFilterString += " AND H.IsExit=0"
                End If
            Else
                If radVolume.Checked Then
                    GetFilterString += " AND H.LossQTY<> 0"
                End If
            End If
           
        End If

        If (chkStaff.Checked) Then
            GetFilterString += " And H.StaffID ='" & cboStaff.SelectedValue & "'"
        End If
        If (txtGoldSmith.Text <> "") Then
            GetFilterString += " And H.GoldSmith LIKE N'%" & txtGoldSmith.Text.Trim & "%'"
        End If
        If (txtBarcodeNo.Text <> "") Then
            GetFilterString += " And H.ItemCode LIKE '%" & txtBarcodeNo.Text.Trim & "%'"
        End If
        If (txtOriginalCode.Text <> "") Then
            GetFilterString += " And H.OriginalCode LIKE '%" & txtOriginalCode.Text.Trim & "%'"
        End If

        If (txtVoucherNo.Text <> "") Then
            GetFilterString += " And H.SupplierVou LIKE '%" & txtVoucherNo.Text.Trim & "%'"
        End If

        If chkShopItem.Checked = True And chkGoldSmith.Checked = False Then
            GetFilterString += " And H.GoldSmith='' "
        End If

        If chkGoldSmith.Checked = True And chkShopItem.Checked = False Then
            GetFilterString += " And H.GoldSmith<>'' "
        End If
        'If (radSBalance.Checked) Then
        '    GetFilterString += " And H.IsExit<>1 "
        'End If
        If optByGivenDate.Checked Then
            If (radSExit.Checked) Then
                GetFilterString += " And H.IsExit=1 "
            End If
            If (radSBalance.Checked) Then
                GetFilterString += " And H.IsExit=0 "
            End If
        End If
        If chkGram.Checked Then
            GetFilterString += " And H.ItemTG Between " & txtFG.Text & " And " & txtTG.Text
        End If



    End Function

    Private Sub GetCombo()
        If Global_IsHoMaster = False Then
            cboGoldQ.DisplayMember = "GoldQuality"
            cboGoldQ.ValueMember = "@GoldQualityID"
            cboGoldQ.DataSource = _GoldQ.GetAllGoldQualityByLocation(LocationID).DefaultView

            cboItemCat.DisplayMember = "ItemCategory_"
            cboItemCat.ValueMember = "@ItemCategoryID"
            cboItemCat.DataSource = _ItemCat.GetAllItemCategoryByLocation(LocationID).DefaultView

            cboStaff.DisplayMember = "Staff_"
            cboStaff.ValueMember = "StaffID"
            cboStaff.DataSource = _StaffCon.GetStaffListByLocation(LocationID).DefaultView

            cboSupplier.DisplayMember = "SupplierName_"
            cboSupplier.ValueMember = "@SupplierID"
            cboSupplier.DataSource = _Supplier.GetAllSupplierListByLocation(LocationID).DefaultView

            cboGoldSmith.DisplayMember = "Name_"
            cboGoldSmith.ValueMember = "@GoldSmithID"
            cboGoldSmith.DataSource = _GoldSmith.GetAllGoldSmithListByLocation(LocationID).DefaultView

        Else
            cboGoldQ.DisplayMember = "GoldQuality"
            cboGoldQ.ValueMember = "@GoldQualityID"
            cboGoldQ.DataSource = _GoldQ.GetAllGoldQuality().DefaultView

            cboItemCat.DisplayMember = "ItemCategory_"
            cboItemCat.ValueMember = "@ItemCategoryID"
            cboItemCat.DataSource = _ItemCat.GetAllItemCategory().DefaultView

            'CboLocation.DisplayMember = "Location_"
            'CboLocation.ValueMember = "@LocationID"
            'CboLocation.DataSource = _Location.GetAllLocationList().DefaultView

            cboStaff.DisplayMember = "Staff_"
            cboStaff.ValueMember = "StaffID"
            cboStaff.DataSource = _StaffCon.GetStaffList().DefaultView


            cboSupplier.DisplayMember = "SupplierName_"
            cboSupplier.ValueMember = "@SupplierID"
            cboSupplier.DataSource = _Supplier.GetAllSupplierList().DefaultView

            cboGoldSmith.DisplayMember = "Name_"
            cboGoldSmith.ValueMember = "@GoldSmithID"
            cboGoldSmith.DataSource = _GoldSmith.GetAllGoldSmithList().DefaultView
        End If



    End Sub

    Private Sub radDetail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radDetail.CheckedChanged
        If (radDetail.Checked) Then
            chkGoldQly.Enabled = True
            chkItemCat.Enabled = True
            'chkGS.Enabled = True
            chkLocation.Enabled = True
            chkItemName.Enabled = False
            If Global_IsHeadOffice Then
                CboLocation.SelectedValue = Global_CurrentLocationID
                CboLocation.DisplayMember = "Location_"
                CboLocation.ValueMember = "@LocationID"
                CboLocation.DataSource = _Location.GetAllLocationList().DefaultView


            Else
                CboLocation.DisplayMember = "Location_"
                CboLocation.ValueMember = "@LocationID"
                CboLocation.DataSource = _Location.GetAllLocationList().DefaultView
                CboLocation.SelectedValue = Global_CurrentLocationID
                CboLocation.Enabled = False
                ChkLocation.Enabled = False
            End If
        End If
    End Sub
    'Private Sub chkByLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkByLocation.CheckedChanged
    '    If chkByLocation.Checked = True Then
    '        ChkLocation.Enabled = True
    '        optAllStocks.Enabled = False
    '        optByGivenDate.Enabled = False
    '        optBalanceStocks.Checked = True
    '        'cboLocation.Enabled = True
    '    Else
    '        chkLocation.Enabled = False
    '        cboLocation.Enabled = False
    '        ChkLocation.Checked = False
    '        optAllStocks.Enabled = True
    '        optByGivenDate.Enabled = True
    '        optAllStocks.Checked = True

    '    End If

    'End Sub
    Private Sub chkGoldQly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGoldQly.CheckedChanged
        If (chkGoldQly.Checked) Then
            cboGoldQ.Enabled = True
        Else
            cboGoldQ.Enabled = False
        End If
    End Sub
    Private Sub chkLocation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocation.CheckedChanged
        If (ChkLocation.Checked) Then
            CboLocation.Enabled = True
        Else
            CboLocation.Enabled = False
        End If
    End Sub

    Private Sub chkItemCat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemCat.CheckedChanged
        If (chkItemCat.Checked) Then
            If radDailyStock.Checked = False Then
                chkItemName.Enabled = True
            End If
            cboItemCat.Enabled = True
        Else
            cboItemCat.Enabled = False
            chkItemName.Enabled = False
            chkItemName.Checked = False
            cboItemName.Enabled = False

        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub optByGivenDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optByGivenDate.CheckedChanged
        grpBoxDate.Enabled = optByGivenDate.Checked
        If optByGivenDate.Checked Then
            grpSType.Enabled = True
            chkIsClosed.Checked = False
            chkInactiveStock.Checked = False
            chkIsClosed.Enabled = False
            chkInactiveStock.Enabled = False

        Else
            grpSType.Enabled = False
            chkIsClosed.Enabled = True
            chkInactiveStock.Enabled = True
        End If
    End Sub
    Private Sub chkisclosed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsClosed.CheckedChanged
        If chkIsClosed.Checked = True Then
            grpBoxDate.Enabled = True
        Else
            grpBoxDate.Enabled = False
        End If
    End Sub
    Private Sub optAllStocks_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAllStocks.CheckedChanged
        grpBoxDate.Enabled = Not optAllStocks.Checked
    End Sub

    Private Sub optBalanceStocks_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optBalanceStocks.CheckedChanged
        grpBoxDate.Enabled = Not optBalanceStocks.Checked
    End Sub

    Private Sub radStock_CheckedChanged(sender As Object, e As EventArgs) Handles radStock.CheckedChanged
        If radStock.Checked Then
            chkIsClosed.Enabled = True
            chkIsOrder.Enabled = True
        End If
    End Sub

    Private Sub radVolume_CheckedChanged(sender As Object, e As EventArgs) Handles radVolume.CheckedChanged
        If radVolume.Checked Then
            chkIsClosed.Checked = False
            chkIsOrder.Checked = False
            chkIsClosed.Enabled = False
            chkIsOrder.Enabled = False
            radOverView.Checked = False
            radOverView.Enabled = False
            radByGoldQuality.Checked = False
            radByGoldQuality.Enabled = False
            optAllStocks.Checked = True
            txtGoldSmith.Enabled = False
            chkShopItem.Checked = False
            chkGoldSmith.Checked = False
            chkShopItem.Enabled = False
            chkGoldSmith.Enabled = False
        Else
            chkIsClosed.Enabled = True
            chkIsOrder.Enabled = True
            radOverView.Enabled = True
            txtGoldSmith.Enabled = True
            chkShopItem.Enabled = True
            chkGoldSmith.Enabled = True
            radByGoldQuality.Enabled = True
        End If
    End Sub
    Private Sub radLooseDiamond_CheckedChanged(sender As Object, e As EventArgs) Handles radLooseDiamond.CheckedChanged
        If radLooseDiamond.Checked Then
            chkIsClosed.Checked = False
            chkIsOrder.Checked = False
            chkIsClosed.Enabled = False
            chkIsOrder.Enabled = False
            radOverView.Checked = False
            radOverView.Enabled = False
            radByGoldQuality.Checked = False
            radByGoldQuality.Enabled = False
            optAllStocks.Checked = True
            txtGoldSmith.Enabled = False
            chkShopItem.Checked = False
            chkGoldSmith.Checked = False
            chkShopItem.Enabled = False
            chkGoldSmith.Enabled = False
        Else
            chkIsClosed.Enabled = True
            chkIsOrder.Enabled = True
            radOverView.Enabled = True
            txtGoldSmith.Enabled = True
            chkShopItem.Enabled = True
            chkGoldSmith.Enabled = True
            radByGoldQuality.Enabled = True
        End If
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

    Private Sub radDailyStock_CheckedChanged(sender As Object, e As EventArgs) Handles radDailyStock.CheckedChanged
        If radDailyStock.Checked Then
            grpBoxDate.Enabled = True
            dtpFromDate.Enabled = True
            dtpToDate.Enabled = False
            grpBoxView.Enabled = False
            chkIsClosed.Checked = False
            chkIsFix.Checked = False
            chkIsOrder.Checked = False
            chkIsOrder.Enabled = False
            chkIsFix.Enabled = False
            chkIsClosed.Enabled = False
            radStock.Checked = True
            radVolume.Enabled = False
            radSummary.Checked = True
            chkItemName.Checked = False
            chkItemName.Enabled = False
            cboItemName.Enabled = False
            chkStaff.Checked = False
            chkStaff.Enabled = False
            cboStaff.Enabled = False
            txtGoldSmith.Text = ""
            txtGoldSmith.Enabled = False
            txtBarcodeNo.Text = ""
            txtBarcodeNo.Enabled = False
        Else
            chkIsOrder.Enabled = True
            chkIsFix.Enabled = True
            chkIsClosed.Enabled = True
            grpBoxView.Enabled = True
            radVolume.Enabled = True
            If chkItemCat.Checked Then
                chkItemName.Enabled = True
                cboItemName.Enabled = True
            End If
            If optByGivenDate.Checked Then
                dtpToDate.Enabled = True
            Else
                grpBoxDate.Enabled = False
            End If
            chkStaff.Enabled = True
            chkGS.Enabled = True
            txtGoldSmith.Enabled = True
            txtBarcodeNo.Enabled = True
        End If
    End Sub

    Private Sub chkStaff_CheckedChanged(sender As Object, e As EventArgs) Handles chkStaff.CheckedChanged
        If (chkStaff.Checked) Then
            cboStaff.Enabled = True
        Else
            cboStaff.Enabled = False
        End If
    End Sub

    Private Sub radOverView_CheckedChanged(sender As Object, e As EventArgs) Handles radOverView.CheckedChanged
        If radOverView.Checked Then
            chkIsOrder.Checked = False
            chkIsOrder.Enabled = False
        Else
            chkIsOrder.Enabled = True
        End If
    End Sub
    Private Sub radByGoldQuality_CheckedChanged(sender As Object, e As EventArgs) Handles radByGoldQuality.CheckedChanged
        If radByGoldQuality.Checked Then
            chkIsOrder.Checked = False
            chkIsOrder.Enabled = False
        Else
            chkIsOrder.Enabled = True
        End If
    End Sub

    'Private Sub optShopStock_CheckedChanged(sender As Object, e As EventArgs) Handles optShopStock.CheckedChanged
    '    grpBoxDate.Enabled = Not optShopStock.Checked
    'End Sub

    'Private Sub optGoldSmithStock_CheckedChanged(sender As Object, e As EventArgs) Handles optGoldSmithStock.CheckedChanged
    '    grpBoxDate.Enabled = Not optGoldSmithStock.Checked
    'End Sub


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

    Private Sub rbtnDiamondStock_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDiamondStock.CheckedChanged
        If rbtnDiamondStock.Checked Then
            chkIsClosed.Enabled = True
            chkIsOrder.Enabled = True
        End If
    End Sub


    Private Sub chkSupplier_CheckedChanged(sender As Object, e As EventArgs) Handles chkSupplier.CheckedChanged
        If (chkSupplier.Checked) Then
            cboSupplier.Enabled = True
        Else
            cboSupplier.Enabled = False
        End If
    End Sub


    Private Sub cboGoldSmith_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboGoldSmith.SelectedValueChanged
        Dim goldSmithID As String
        goldSmithID = cboGoldSmith.SelectedValue

    End Sub

    Private Sub chkGS_CheckedChanged(sender As Object, e As EventArgs) Handles chkGS.CheckedChanged
        If (chkGS.Checked) Then
            
            cboGoldSmith.Enabled = True
        Else
            cboGoldSmith.Enabled = False

        End If
    End Sub

    Private Sub cboGoldSmith_KeyUp(sender As Object, e As KeyEventArgs) Handles cboGoldSmith.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldSmith, e)
    End Sub

    Private Sub cboGoldSmith_Leave(sender As Object, e As EventArgs) Handles cboGoldSmith.Leave
        AutoCompleteCombo_Leave(cboGoldSmith, "")
    End Sub
    'Edit Test

    Private Sub chkGram_CheckedChanged(sender As Object, e As EventArgs) Handles chkGram.CheckedChanged
        If chkGram.Checked Then
            txtFG.Enabled = True
            txtTG.Enabled = True
            txtFG.BackColor = Color.White
            txtTG.BackColor = Color.White
        Else
            txtFG.Enabled = False
            txtTG.Enabled = False
            txtFG.BackColor = Color.Gainsboro
            txtTG.BackColor = Color.Gainsboro
        End If
    End Sub
    Private Sub txtFG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFG.KeyPress
        ValidateNumeric(sender, e, True)
    End Sub
    Protected Sub ValidateNumeric(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal DecimalAllo As Boolean = False)
        Dim SenderTextBox As TextBox = DirectCast(sender, TextBox)
        Dim DecPos As Integer = SenderTextBox.Text.IndexOf(".")
        If DecimalAllo = True Then
            If e.KeyChar.ToString = "." AndAlso (DecPos >= 0) Then
                e.Handled = True
                Exit Sub
            End If
            If KeyValidation(InStr("1234567890.", e.KeyChar.ToString, CompareMethod.Text) > 0, e) Then Return
        Else
            If KeyValidation(InStr("1234567890", e.KeyChar.ToString, CompareMethod.Text) > 0, e) Then Return
        End If
    End Sub
    Private Function KeyValidation(ByVal Bool As Boolean, ByRef e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Dim RBoolean As Boolean = Bool
        Dim Index As Integer
        If Bool = False Then
            Index = AscW(e.KeyChar)
            If Index = Keys.Back Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
        Return RBoolean
    End Function
    Private Sub txtTG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTG.KeyPress
        ValidateNumeric(sender, e, True)
    End Sub
End Class