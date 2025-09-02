Imports CommonInfo
Namespace ConsignmentSale
    Public Interface IConsignmentSaleController
        Function SaveConsignmentSale(ByVal obj As ConsignmentSaleInfo, ByVal _dtConsignSaleItem As DataTable) As Boolean
        Function DeleteConsignmentSale(ByVal ConsignmentSaleID As String) As Boolean
        Function GetAllConsignmentSale() As DataTable
        Function GetConsignmentSaleByID(ByVal ConsignmentSaleID As String) As ConsignmentSaleInfo

        Function GetConsignmentSaleItem(ByVal ConsignmentSaleItemID As String) As DataTable
        Function GetConsignmentSaleItemByID(ByVal ConsignmentSaleID As String) As DataTable
        Function GetBarcodeByConsignmentSaleID(ByVal argstr As String, Optional ByVal cristr As String = "", Optional ByVal ConsignmentSaleID As String = "") As ConsignmentSaleItemInfo
        Function GetBarcodeDataByConsignmentSaleID(ByVal argstr As String, Optional ByVal ConsignmentSaleID As String = "") As DataTable
        Function GetItemCodeByConsignmentSaleID(Optional ByVal ConsignmentSaleID As String = "") As DataTable
        Function GetConsignmentSaleReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetConsignmentSaleReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetConsignmentSalePrint(ByVal ConsignmentSaleID As String) As DataTable
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean
    End Interface
End Namespace
