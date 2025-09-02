Imports CommonInfo
Namespace PurchaseItem
    Public Interface IPurchaseItemDA
        Function InsertPuchaseHeader(ByVal Obj As PurchaseHeaderInfo) As Boolean
        Function UpdatePuchaseHeader(ByVal Obj As PurchaseHeaderInfo) As Boolean
        Function DeletePuchaseHeader(ByVal PurchaseHeaderID As String) As Boolean

        Function InsertPuchaseDetail(ByVal ObjItem As PurchaseDetailInfo) As Boolean
        Function UpdatePuchaseDetail(ByVal ObjItem As PurchaseDetailInfo) As Boolean
        Function DeletePuchasDetail(ByVal PurchaseDetailID As String) As Boolean

        Function InsertPuchasGems(ByVal ObjGems As PurchaseGemInfo) As Boolean
        Function UpdatePuchasGems(ByVal ObjGems As PurchaseGemInfo) As Boolean
        Function DeletePuchaseGems(ByVal PurchaseGemID As String) As Boolean
        Function DeletePuchaseGemsByDetailID(ByVal PurchaseDetailID As String) As Boolean

        Function GetPurchaseHeaderByID(ByVal PurchaseHeaderID As String) As PurchaseHeaderInfo
        Function GetAllPuchaseHeader() As DataTable
        Function GetPurchaseDetailByID(ByVal PurchaseHeaderID As String) As DataTable
        Function GetPurchaseDetailGemByID(ByVal PurchaseHeaderID As String) As DataTable
        'Function GetPurchaseDetailDataByHeaderID(ByVal PurchaseHeaderID As String, Optional ByVal IsGems As Boolean = False) As DataTable
        'Function UpdatePurchaseHeaderbyIsOut(ByVal PurchaseHeaderID As String, ByVal IsOut As Boolean) As Boolean
        'Function GetAllPurchaseHeaderByIsOut() As DataTable
        Function GetPurchaseGemDataByPurchaseGemID(ByVal PurchaseGemID As String) As DataTable
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

        Function GetPurchaseGemsItemByDetailID(ByVal PurchaseDetailID As String) As DataTable
        Function CheckIsUseInSaleInvoiceHeader(ByVal PurchaseHeaderID As String) As Boolean
        Function GetPurchaseHeaderDataBySaleInvoice(ByVal PurchaseHeaderID As String, ByVal SaleInvoiceHeaderID As String, ByVal Type As String) As PurchaseHeaderInfo
        Function GetSaleInvoiceDataByHeaderID(ByVal PurchaseHeaderID As String) As DataTable
        Function UpdateSaleInvoiceDataByPurchaseHeaderID(ByVal Obj As CommonInfo.SaleInvoiceHeaderInfo) As Boolean
        Function GetSaleVolumeInvoiceDataByHeaderID(ByVal PurchaseHeaderID As String) As DataTable
        Function GetSaleLooseDiamondInvoiceDataByHeaderID(ByVal PurchaseHeaderID As String) As DataTable
        Function UpdateSaleVolumeDataByPurchaseHeaderID(ByVal Obj As CommonInfo.SalesVolumeHeaderInfo) As Boolean
        Function UpdateSaleLooseDiamondDataByPurchaseHeaderID(ByVal Obj As CommonInfo.SaleLooseDiamondHeaderInfo) As Boolean
        Function CheckIsUseInSaleVolumeHeader(ByVal PurchaseHeaderID As String) As Boolean
        Function GetAllPurchasePrintForSaleVolume(ByVal SalesVolumeID As String) As DataTable
        Function GetAllPurchasePrintForSaleLooseDiamond(ByVal SaleLooseDiamondID As String) As DataTable
        Function GetPurchaseInvoiceSummayReportByDate(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceDailyTransactionReport(ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetPurchaseInvoiceGemReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function InsertPurchaseFromSupplier(ByVal Obj As PurchaseHeaderInfo) As Boolean
        Function UpdatePurchaseFromSupplier(ByVal Obj As PurchaseHeaderInfo) As Boolean
        Function InsertPurchaseFromSupplierItem(ByVal Obj As PurchaseDetailInfo) As Boolean
        Function UpdatePurchaseFromSupplierItem(ByVal Obj As PurchaseDetailInfo) As Boolean
        Function DeletePurchaseFromSupplierItem(ByVal PurchaseFromSupplierItemID As String) As Boolean
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

