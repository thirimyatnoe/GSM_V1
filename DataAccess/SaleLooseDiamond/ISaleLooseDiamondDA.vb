Imports CommonInfo
Namespace SaleLooseDiamond

    Public Interface ISaleLooseDiamondDA
        Function InsertSaleLooseDiamondHeader(ByVal SaleLooseDiamondObj As SaleLooseDiamondHeaderInfo) As Boolean
        Function UpdateSaleLooseDiamondHeader(ByVal SaleLooseDiamondObj As SaleLooseDiamondHeaderInfo) As Boolean
        Function DeleteSaleLooseDiamondHeader(ByVal SaleLooseDiamondHeaderID As String) As Boolean

        Function InsertSaleLooseDiamondDetail(ByVal ObjSaleLooseDiamondItem As SaleLooseDiamondDetailInfo) As Boolean
        Function UpdateSaleLooseDiamondDetail(ByVal ObjSaleLooseDiamondItem As SaleLooseDiamondDetailInfo) As Boolean
        Function DeleteSaleLooseDiamondDetail(ByVal SaleLooseDiamondDetailID As String) As Boolean

        Function GetSaleLooseDiamondDetailByID(ByVal SaleLooseDiamondHeaderID As String) As DataTable

        Function GetAllSaleLooseDiamond() As DataTable
        Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondHeaderID As String) As SaleLooseDiamondHeaderInfo
        Function GetSaleLooseDiamondPrint(ByVal SaleLooseDiamondID As String) As DataTable
        Function GetSalesVolumeDataByHeaderIDAndItemCode(ByVal SalesVolumeHeaderID As String, Optional ByVal ItemCode As String = "") As DataTable
        Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetSaleLooseDiamondByID(ByVal SaleLooseDiamondID As String) As SaleLooseDiamondHeaderInfo
        Function GetSalesInvoiceLooseDiamondReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetSaleLooseDiamondReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetProfitForSaleDiamondItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable
        Function GetSaleLooseDiamondDetailByDetailID(ByVal SaleLooseDiamondDetailID As String) As DataTable
        Function GetSaleLooseDiamondDataByCustomerIDAndItemCode(ByVal CustomerID As String, Optional ByVal criStr As String = "") As DataTable
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean
    End Interface
End Namespace

