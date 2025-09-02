Imports CommonInfo
Namespace SalesOrder

    Public Interface ISalesOrderDA
        Function InsertSalesOrder(ByVal SalesOrderObj As SalesOrderInfo) As Boolean
        Function UpdateSalesOrder(ByVal SalesOrderObj As SalesOrderInfo) As Boolean
        Function DeleteSalesOrder(ByVal SalesOrderID As String) As Boolean
        Function GetAllSalesOrder(Optional ByVal cristr As String = "") As DataTable
        Function GetSalesOrder(ByVal SalesOrderID As String) As SalesOrderInfo

        Function InsertSalesOrderItem(ByVal ObjSalesOrderItem As SalesOrderGemsItemInfo) As Boolean
        Function UpdateSalesOrderItem(ByVal ObjSalesOrderItem As SalesOrderGemsItemInfo) As Boolean
        Function DeleteSalesOrderItem(ByVal SalesOrderItemID As String) As Boolean
        Function GetSalesOrderItem(ByVal SalesOrderID As String) As DataTable
        Function GetSalesOrderForReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As DataTable
        Function GetSalesOrderPrint(ByVal SaleOrderID As String) As DataTable
        Function GetAllSaleOrderFromSearchBox() As DataTable
        Function UpdateSalesOrderReturnToCompile(ByVal Obj As SalesOrderInfo) As Boolean
    End Interface
End Namespace

