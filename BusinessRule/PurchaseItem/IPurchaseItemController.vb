Imports CommonInfo
Namespace PurchaseItem
    Public Interface IPurchaseItemController
        Function SavePuchaseItem(ByVal Obj As PurchaseHeaderInfo, ByVal _dtDetail As DataTable, ByVal _dtGems As DataTable) As Boolean
        Function DeletePuchaseHeader(ByVal Obj As PurchaseHeaderInfo) As Boolean
        Function GetPurchaseHeaderByID(ByVal PurchaseHeaderID As String) As PurchaseHeaderInfo
        Function GetAllPuchaseHeader() As DataTable
        'Function GetAllPurchaseHeaderByIsOut() As DataTable
        Function GetPurchaseDetailByID(ByVal PurchaseHeaderID As String) As DataTable
        Function GetPurchaseDetailGemByID(ByVal PurchaseHeaderID As String) As DataTable
        Function GetPurchaseGemDataByPurchaseGemID(ByVal PurchaseGemID As String) As DataTable
        'Function GetPurchaseDetailDataByHeaderID(ByVal PurchaseHeaderID As String, Optional ByVal IsGems As Boolean = False) As DataTable
        Function GetPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceForBarcodeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As PurchaseDetailInfo
        Function GetPurchaseInvoiceReportForTotalAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoicePrint(ByVal PurchaseHeaderID As String) As DataTable
        Function GetPurchaseInvoiceOnlyGemPrint(ByVal PurchaseHeaderID As String) As DataTable
        Function GetPurchaseInvoiceLooseDiamondPrint(ByVal PurchaseHeaderID As String) As DataTable
        Function GetAllPurchaseInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetAllPurchaseGemsVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceDetailPrint(ByVal PurchaseHeaderID As String) As DataTable
        Function GetAllPuchaseHeaderDataBySaleInvoice(ByVal SaleInvoiceHeaderID As String, ByVal Type As String) As DataTable
        Function GetAllPurchaseHeaderDataByConsignmentSaleInvoice(ByVal ConsignmentSaleID As String, ByVal Type As String) As DataTable
        Function GetAllPuchaseHeaderDataBySaleGems(ByVal SaleGemsID As String) As DataTable

        Function GetAllPurchasePrint(ByVal SaleInvoiceHeaderID As String) As DataTable
        Function GetAllPurchasePrintByConsignmentSale(ByVal ConsignmentSaleID As String) As DataTable
        Function GetAllPurchaseGemsPrint(ByVal SaleGemsID As String) As DataTable
        Function GetPurchaseHeaderDataBySaleInvoice(ByVal PurchaseHeaderID As String, ByVal SaleInvoiceHeaderID As String, ByVal Type As String) As PurchaseHeaderInfo
        Function GetAllPurchasePrintForSaleVolume(ByVal SaleInvoiceHeaderID As String) As DataTable
        Function GetAllPurchasePrintForSaleLooseDiamond(ByVal SaleLooseDiamondID As String) As DataTable
        Function GetPurchaseInvoiceSummayReportByDate(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceGemReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceDailyTransactionReport(ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function SavePurchaseFromSupplier(ByVal Obj As PurchaseHeaderInfo, ByVal _dtPurchaseInvoiceItem As DataTable) As Boolean
        Function DeletePurchaseFromSupplier(ByVal PurchaseFromSupplierID As String) As Boolean
        Function GetAllPurchaseFromSupplier() As DataTable
        Function GetPurchaseFromSupplier(ByVal PurchaseFromSupplierID As String) As PurchaseHeaderInfo
        Function GetPurchaseFromSupplierItem(ByVal PurchaseFromSupplierID As String) As DataTable
        Function GetPurchaseInvoiceFromSupplierReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseGemByID(ByVal PurchaseHeaderID As String) As DataTable
        Function GetPurchaseDiamondByID(ByVal PurchaseHeaderID As String) As DataTable
        Function GetPurchaseInvoiceLooseDiamondReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceForLooseDiamondReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
    End Interface
End Namespace

