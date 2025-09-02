Imports CommonInfo
Namespace WholeSaleInvoice
    Public Interface IWholeSaleInvoiceDA
        Function InsertWholeSaleInvoice(ByVal WholeSaleInvoiceObj As WholeSaleInvoiceInfo) As Boolean
        Function UpdateWholeSaleInvoice(ByVal WholeSaleInvoiceObj As WholeSaleInvoiceInfo) As Boolean
        Function DeleteWholeSaleInvoice(ByVal WholeSaleInvoiceID As String) As Boolean
        Function GetAllWholeSaleInvoice() As DataTable
        Function GetWholeSaleInvoiceByID(ByVal WholeSaleInvoiceID As String) As WholeSaleInvoiceInfo
      
        Function InsertWholeSaleInvoiceItem(ByVal ObjWholeSaleInvoiceItem As WholeSaleInvoiceItemInfo) As Boolean
        Function UpdateWholeSaleInvoiceItem(ByVal ObjWholeSaleInvoiceItem As WholeSaleInvoiceItemInfo) As Boolean
        Function DeleteWholeSaleInvoiceItem(ByVal WholeSaleInvoiceItemID As String) As Boolean
        Function GetWholeSaleInvoiceItem(ByVal WholesaleInvoiceItemID As String) As DataTable

        Function GetWholeSaleInvoiceItemByID(ByVal WholeSaleInvoiceID As String) As DataTable
        Function GetBarcodeByWholeSaleID(ByVal argstr As String, Optional ByVal cristr As String = "", Optional ByVal WholeSaleID As String = "") As WholeSaleInvoiceItemInfo
        Function GetBarcodeDataByWholeSaleID(ByVal argstr As String, Optional ByVal WholeSaleID As String = "") As DataTable
        Function GetItemCodeByWholeSaleID(Optional ByVal WholeSaleID As String = "") As DataTable
        Function GetBarcodeDataByWholesaleInvoiceID(ByVal WholesaleInvoiceID As String) As DataTable
        Function GetAllWholesaleInvoiceByConsignmentType() As DataTable

        Function GetWSInvoiceAndCSInvoice() As DataTable
        Function GetWSInvoice() As DataTable
        Function GetWholeSaleInvoiceForCommissionReport(ByVal WDate As Date, ByVal CustomerID As String, ByVal PayType As Integer, Optional ByVal cristr As String = "") As DataTable
        Function GetWholeSalePrint(ByVal WholeSalesInvoiceID As String) As DataTable
        Function GetWholeSaleReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetConsignBalanceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetConsignBalanceReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable

        Function GetWholeSaleTotalPaidAmtReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal WSType As String = "", Optional ByVal GetFilterString As String = "") As DataTable
        Function GetWholeSaleSummaryReportByCustomer(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function UpdateWholeSalesItem(ByVal Obj As WholeSaleInvoiceItemInfo) As Boolean
        Function UpdateSalesItem(ByVal Obj As WholeSaleInvoiceItemInfo) As Boolean
        Function GetWholeSaleReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean
    End Interface
End Namespace
