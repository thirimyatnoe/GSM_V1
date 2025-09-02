Imports CommonInfo

Namespace SalesItem
    Public Interface ISalesItemController
        Function GetSalesIDByItemCode(ByVal argItemCode As String) As String
        Function GetSalesItemGems(ByVal SalesItemGemsID As String) As DataTable
        Function GetForSalesItemForSaleInvoice(Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal Status As String = "") As DataTable
        Function GetForSaleReportByLocation(ByVal FilterString As String, ByVal LocationID As String) As DataTable
        Function GetForSaleForReport(Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleForIsCloseReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function SaveForSaleHeader(ByVal HSalesItemObj As SalesItemInfo, ByVal SalesItemObj As SalesItemInfo, ByVal ItemCatObj As ItemCategoryInfo, ByVal _dtSalesGems As DataTable, Optional ByVal _GetItemCodeFromGF As String = "") As Boolean
        Function GetAllForSaleHeader(Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleInfo(ByVal ForSaleID As String) As SalesItemInfo
        Function GetForSaleInfo_History(ByVal ForSaleID As String) As SalesItemInfo
        Function GetForSaleInfoY(ByVal ForSaleID As String) As SalesItemInfo

        Function DeleteForSale(ByVal ForSaleID As String) As Boolean
        Function GetForSaleGems(ByVal ForSaleID As String) As DataTable
        Function GetForSaleItemInfo(ByVal ForSaleHeaderID As String, ByVal cristr As String) As SalesItemInfo
        Function GetForSaleInfoByItemCode(ByVal ItemCode As String, Optional ByVal argForSaleIDStr As String = "") As SalesItemInfo
        Function GetForSaleInfoByWSItemCode(ByVal ItemCode As String, ByVal argForSaleIDStr As String) As SalesItemInfo

        Function GetForSaleDataByItemCode(ByVal StrCri As String) As DataTable

        Function GetForSalesItemForOrderInvoice(ByVal OrderInvoiceID As String, Optional ByVal argForSaleIDStr As String = "") As DataTable
        Function GetForSaleInfoByItemCodeANDOrderInvoiceID(ByVal ItemCode As String, ByVal OrderInvoiceID As String, Optional ByVal argForSaleIDStr As String = "") As SalesItemInfo
        Function GetForSaleForReportByTotalWeight(Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleForReportByDatePeriodAndTotalWeight(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleVolumeForReport(Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleVolumeForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleDataBySummaryReport(Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleDataBySummaryReportByLocation(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As DataTable
        Function GetForSaleDataBySummaryIsCloseReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleForSummaryReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal Status As String = "") As DataTable
        Function GetForSalesVolumeItemForSaleInvoice(Optional ByVal cristr As String = "", Optional ByVal CheckState As Boolean = False) As DataTable
        Function GetSaleItemDataByItemCodeAndForSaleID(ByVal ItemCode As String, Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleDataByItemCodeAndIsExit(ByVal ItemCode As String, ByVal IsExit As Boolean, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleItemDataByOrderReceiveDetailID(ByVal OrderReceiveDetailID As String) As DataTable
        Function GetSaleInvoiceForRepair(Optional cristr As String = "", Optional ByVal BarcodeNo As String = "") As DataTable
        Function GetSaleInvoiceObjDataForRepair(cristr As String) As SalesInvoiceDetailInfo
        Function GetBalanceStockForDate(FromDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "") As DataTable
        Function GetStockItemCardReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable
        Function GetAllStockDataForMonthly(FromDate As Date, ToDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False, Optional ByVal global_isHOToBranch As Boolean = False) As DataTable
        Function GetAllStockDataForMonthlyByTransferDate(FromDate As Date, ToDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False, Optional ByVal global_isHOToBranch As Boolean = False) As DataTable
        Function GetAllStockDataForMonthlyForOffline(FromDate As Date, ToDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False) As DataTable
        Function GetForSaleItemListForTransfer(ByVal argForSaleIDStr As String) As DataTable
        Function GetForSaleItemListForTransferDiamond(ByVal argForSaleIDStr As String) As DataTable
        Function GetForSaleItemListForWholesales(ByVal argForSaleIDStr As String) As DataTable

        Function GetForSaleItembyItemCode(ByVal ItemCode As String, ByVal argForSaleIDStr As String) As SalesItemInfo
        Function GetForSaleInfoByBarCodeNo(ByVal ItemCode As String, ByVal ItemCategoryID As String, ByVal argForSaleIDStr As String) As SalesItemInfo
        Function GetBarcodeTrack(ByVal BarcodeNo As String) As DataTable
        Function GetStockBalanceFromHO(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As DataTable
        Function GetBalanceFromHOByTotalWeight(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As DataTable
        Function GetForSalesItemForSaleLooseDiamond(Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleLooseDiamondForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetForSaleLooseDiamondForReport(Optional ByVal cristr As String = "") As DataTable
    End Interface
End Namespace

