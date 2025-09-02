Imports CommonInfo
Namespace SalesOrder
    Public Interface ISalesOrderController
        Function SaveSalesOrder(ByVal SalesOrderObj As SalesOrderInfo, ByVal _dtSalesOrderItem As DataTable) As Boolean
        Function DeleteSalesOrder(ByVal SalesOrderObj As SalesOrderInfo) As Boolean
        Function GetAllSalesOrder(Optional ByVal cristr As String = "") As DataTable
        Function GetSalesOrder(ByVal SalesOrderID As String) As SalesOrderInfo
        Function GetSalesOrderItem(ByVal SalesOrderID As String) As DataTable
        Function GetSalesOrderForReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As DataTable
        Function GetSalesOrderPrint(ByVal SaleOrderID As String) As DataTable
        Function GetAllSaleOrderFromSearchBox() As DataTable
    End Interface
End Namespace
