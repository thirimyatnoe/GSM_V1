Imports Microsoft.Reporting.WinForms
Imports System.String
Imports BusinessRule
Imports CommonInfo
Public Class frm_Dashboard
    Private DashboardDA As Dashboard.IDashboardController = Factory.Instance.CreateDashboardController
    Private CurrentPriceDA As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Dim second As Integer

    Private Sub frm_Dashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtGoldPrice As DataTable

        Dim dr As DataRow
        dtGoldPrice = CurrentPriceDA.GetSolidGoldPrice()
        For Each dr In dtGoldPrice.Rows
            txtGoldPrice.Text = Format(dr.Item("salesrate"), "#,##0")
        Next

        cboCashdate.SelectedIndex = 0
        cboCashSaleType.SelectedIndex = 0
        dtpSaleFDate.Value = Now.Date
        dtpSaleTDate.Value = Now.Date
        cboSaleType.SelectedIndex = 0
        cboSortingType.SelectedIndex = 0
        dtpSaleCategoryFDate.Value = Now.Date
        dtpSaleCategoryTDate.Value = Now.Date
        cboSaleCategoryType.SelectedIndex = 0
        cboStatus.SelectedIndex = 0
        CboBalanceType.SelectedIndex = 0
        cboItemType.SelectedIndex = 0
        CboType.SelectedIndex = 0
        lblTimer.Text = 180
        DBTimer.Interval = 1000
        DBTimer.Start()

        ' Call CashCreditPreview()
        'Call SalePreview()
        'Call StockBalancePreview()
        'Call SaleCategoryPreview()
        'Sale Cash

        Call GetSaleAmount()
    End Sub
    Private Function GetFilterString() As String
        GetFilterString = ""
        If cboCashdate.SelectedIndex = 0 Then
            GetFilterString += "'" & DateAdd("D", -30, Date.Now.Date) & " 00:00:00' And '" & Date.Now.Date & " 23:59:59'"
        ElseIf cboCashdate.SelectedIndex = 1 Then
            GetFilterString += "'" & DateAdd("M", -12, Date.Now.Date) & " 00:00:00' And '" & Date.Now.Date & " 23:59:59'"
        Else
            GetFilterString += "'" & DateAdd("YYYY", -6, Date.Now.Date) & " 00:00:00' And '" & Date.Now.Date & " 23:59:59'"
        End If


    End Function
    Private Sub CashCreditPreview()
        Dim dt As New DataTable
        If cboStatus.SelectedItem = "Detail" Then
            dt = DashboardDA.GetAllCashAndCredit(cboCashSaleType.SelectedItem, GetFilterString, cboCashdate.SelectedItem)
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_CreditCashDashboard.rdlc"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleCashAndCredit_dtSaleCashAndCredit", dt))

            rptViewer.RefreshReport()
        Else
            dt = DashboardDA.GetAllSaleByLocation(cboCashSaleType.SelectedItem, GetFilterString, cboCashdate.SelectedItem)
            rptViewer.Reset()
            rptViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleByLocationDashboard.rdlc"
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleSummaryDB_dtSaleSummaryDB", dt))

            rptViewer.RefreshReport()
        End If
    End Sub


    Private Sub cboCashdate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCashdate.SelectedIndexChanged
        Call CashCreditPreview()
    End Sub

    Private Sub cboCashSaleType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCashSaleType.SelectedIndexChanged
        Call CashCreditPreview()
    End Sub

    Private Sub SalePreview()
        Dim dt As New DataTable

        dt = DashboardDA.GetAllSaleByDate(dtpSaleFDate.Value.Date, dtpSaleTDate.Value.Date, cboSaleType.SelectedItem)
        SaleReportViewer.Reset()
        If CboType.SelectedItem = "Amount" Then
            SaleReportViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleDashboard.rdlc"
            SaleReportViewer.LocalReport.DataSources.Clear()
            SaleReportViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleDB_dtSaleDB", dt))
        Else
            SaleReportViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleGramDashboard.rdlc"
            SaleReportViewer.LocalReport.DataSources.Clear()
            SaleReportViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleDB_dtSaleDB", dt))
        End If

        SaleReportViewer.RefreshReport()
    End Sub


    Private Sub dtpSaleFDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSaleFDate.ValueChanged
        SalePreview()
    End Sub

    Private Sub dtpSaleTDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSaleTDate.ValueChanged
        SalePreview()
    End Sub

    Private Sub cboSaleType_SelectedIndexChanged(sender As Object, e As EventArgs)
        SalePreview()
    End Sub
    Private Sub StockBalancePreview()
        Dim dt As New DataTable

        dt = DashboardDA.GetAllStockBalance(cboSortingType.SelectedItem, CboBalanceType.SelectedItem)
        If CboBalanceType.SelectedIndex = 0 Then
            StockBalanceReportViewer.Reset()
            StockBalanceReportViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_StockBalanceDashboard.rdlc"
            StockBalanceReportViewer.LocalReport.DataSources.Clear()
            StockBalanceReportViewer.LocalReport.DataSources.Add(New ReportDataSource("dsStockBalance_dtStockBalance", dt))
        Else
            StockBalanceReportViewer.Reset()
            StockBalanceReportViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_CustomerCreditBalanceDashboard.rdlc"
            StockBalanceReportViewer.LocalReport.DataSources.Clear()
            StockBalanceReportViewer.LocalReport.DataSources.Add(New ReportDataSource("dtCreditBalance_dsCreditBalance", dt))
        End If

        StockBalanceReportViewer.RefreshReport()
    End Sub
    Private Sub SaleCategoryPreview()
        Dim dt As New DataTable

        dt = DashboardDA.GetAllSaleByCategory(dtpSaleCategoryFDate.Value.Date, dtpSaleCategoryTDate.Value.Date, cboSaleCategoryType.SelectedItem, cboItemType.SelectedItem)
        SaleCReportViewer.Reset()
        If cboItemType.SelectedIndex = 1 Then
            SaleCReportViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleCategoryDashboard.rdlc"
            SaleCReportViewer.LocalReport.DataSources.Clear()
            SaleCReportViewer.LocalReport.DataSources.Add(New ReportDataSource("dtSaleCategory_dsSaleCategory", dt))
        Else
            SaleCReportViewer.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleItemNameDashboard.rdlc"
            SaleCReportViewer.LocalReport.DataSources.Clear()
            SaleCReportViewer.LocalReport.DataSources.Add(New ReportDataSource("dtSaleItemName_dsSaleItemName", dt))
        End If
        SaleCReportViewer.RefreshReport()
    End Sub
    Private Sub cboSortingType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSortingType.SelectedIndexChanged
        Call StockBalancePreview()
    End Sub

    Private Sub dtpSaleCategoryFDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSaleCategoryFDate.ValueChanged
        Call SaleCategoryPreview()
    End Sub

    Private Sub dtpSaleCategoryTDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSaleCategoryTDate.ValueChanged
        Call SaleCategoryPreview()
    End Sub

    Private Sub cboSaleCategoryType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSaleCategoryType.SelectedIndexChanged
        Call SaleCategoryPreview()
    End Sub
    Private Sub GetSaleAmount()
        Dim dtSale As DataTable
        Dim dtCredit As DataTable
        Dim dtBalance As DataTable
        Dim AllBalance As Long = 0
        Dim SaleAmount As Long = 0
        Dim SaleCreditAmount As Long = 0
        Dim dtWSale As DataTable
        Dim WSaleAmount As Long = 0
        Dim WSaleCreditmount As Long = 0
        Dim AllCredit As Long = 0

        dtSale = DashboardDA.GetAllCashAndCredit("Sale", "'" & Date.Now.Date & " 00:00:00' And '" & Date.Now.Date & " 23:59:59'", cboCashdate.SelectedItem)
        For Each dr As DataRow In dtSale.Rows
            SaleAmount += dr.Item("SaleCashAmount")
        Next

        For Each dr As DataRow In dtSale.Rows
            SaleCreditAmount += dr.Item("SaleCreditAmount")
        Next

        dtWSale = DashboardDA.GetAllCashAndCredit("WholeSale", "'" & Date.Now.Date & " 00:00:00' And '" & Date.Now.Date & " 23:59:59'", cboCashdate.SelectedItem)
        For Each dr As DataRow In dtWSale.Rows
            WSaleAmount += dr.Item("SaleCashAmount")
        Next

        ' dtWSale = DashboardDA.GetAllCashAndCredit("WholeSale", GetFilterString, cboCashdate.SelectedItem)
        For Each dr As DataRow In dtWSale.Rows
            WSaleCreditmount += dr.Item("SaleCreditAmount")
        Next

        dtCredit = DashboardDA.GetAllCredit()
        For Each dr As DataRow In dtCredit.Rows
            AllCredit += dr.Item("NetAmount") - dr.Item("TotalPaidAmount")
        Next

        dtBalance = DashboardDA.GetAllStockBalance(cboSortingType.SelectedItem, "Stock Balance")
        For Each dr As DataRow In dtBalance.Rows
            AllBalance += dr.Item("StockBalance")
        Next

        txtSaleCash.Text = Format(Val(SaleAmount), "#,##0")
        txtSaleCredit.Text = Format(SaleCreditAmount, "#,##0")
        txtSaleBalance.Text = Format(SaleAmount + SaleCreditAmount, "#,##0")

        txtWSaleCash.Text = Format(WSaleAmount, "#,##0")
        txtWSaleCredit.Text = Format(WSaleCreditmount, "#,##0")
        txtWSaleBalance.Text = Format(WSaleAmount + WSaleCreditmount, "#,##0")

        txtTotalCreidt.Text = Format(AllCredit, "#,##0")
        txtTotalStock.Text = Format(AllBalance, "#,##0")
        txtGTotal.Text = Format(Val(txtSaleBalance.Text) + Val(txtWSaleBalance.Text), "#,##0")
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        Call CashCreditPreview()
    End Sub

    Private Sub CboBalanceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboBalanceType.SelectedIndexChanged
        Call StockBalancePreview()
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboItemType.SelectedIndexChanged
        Call SaleCategoryPreview()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboType.SelectedIndexChanged
        SalePreview()
    End Sub
    Private Sub DBTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBTimer.Tick
        lblTimer.Text = CInt(lblTimer.Text) - 1
        If CInt(lblTimer.Text) = 0 Then
            TimerCall()
            DBTimer.Stop()
            DBTimer.Start()
        End If
    End Sub
    Private Sub TimerCall()
        Dim dtGoldPrice As DataTable

        Dim dr As DataRow
        dtGoldPrice = CurrentPriceDA.GetSolidGoldPrice()
        For Each dr In dtGoldPrice.Rows
            txtGoldPrice.Text = Format(dr.Item("salesrate"), "#,##0")
        Next

        cboCashdate.SelectedIndex = 0
        cboCashSaleType.SelectedIndex = 0
        dtpSaleFDate.Value = Now.Date
        dtpSaleTDate.Value = Now.Date
        cboSaleType.SelectedIndex = 0
        cboSortingType.SelectedIndex = 0
        dtpSaleCategoryFDate.Value = Now.Date
        dtpSaleCategoryTDate.Value = Now.Date
        cboSaleCategoryType.SelectedIndex = 0
        cboStatus.SelectedIndex = 0
        CboBalanceType.SelectedIndex = 0
        cboItemType.SelectedIndex = 0
        CboType.SelectedIndex = 0
        DBTimer.Interval = 1000
        lblTimer.Text = 180
        DBTimer.Start()

        ' Call CashCreditPreview()
        'Call SalePreview()
        'Call StockBalancePreview()
        'Call SaleCategoryPreview()
        'Sale Cash

        Call GetSaleAmount()
    End Sub

    Private Sub cboSaleType_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cboSaleType.SelectedIndexChanged
        SalePreview()
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click

    End Sub
End Class