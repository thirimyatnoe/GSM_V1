Imports CommonInfo
Namespace CashReceipt
    Public Interface ICashReceiptDA
        Function GetPurchaseGemsCashReceipt() As DataTable
        Function GetPurchaseInvoiceCashReceipt() As DataTable
        Function GetSaleGemsCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GetSaleInvoiceCashReceipt() As DataTable
        Function GetWholeSalesCashReceipt() As DataTable
        Function GetOrderCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GetPurchaseHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GetSaleInvoiceHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GetSaleLooseDiamondHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GetWholeSaleInvoiceHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GetConsignmentSaleCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GerSaleInvoiceVolumeCashReceipt(ByVal IsCash As Boolean) As DataTable
        Function GetCashReceipt(ByVal VoucherNo As String, Optional ByVal Type As String = "") As DataTable
        Function InsertCashReceipt(ByVal CashReceiptObj As CashReceiptInfo) As Boolean
        Function UpdateCashReceipt(ByVal CashReceiptObj As CashReceiptInfo) As Boolean
        Function DeleteCashReceipt(ByVal CashReceiptID As String) As Boolean
        Function GetDebtDataByType(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "", Optional ByVal Str As String = "") As DataTable
        Function GetDebtInDataByType(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "", Optional ByVal Str As String = "") As DataTable

        Function GetCashReceiptforPrint(ByVal VoucherNo As String, Optional ByVal Type As String = "") As DataTable
        Function GetReturnRepairStockCashReceipt(ByVal IsCash As Boolean) As DataTable

    End Interface
End Namespace
