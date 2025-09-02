Imports CommonInfo
Namespace SaleLooseDiamond
    Public Interface ISaleLooseDiamondController

        Function SaveSaleLooseDiamondHeader(ByVal SaleLooseDiamondObj As SaleLooseDiamondHeaderInfo, ByVal _dtSaleLooseDiamondDetail As DataTable, ByVal _dtOtherCash As DataTable) As Boolean
        Function DeleteSaleLooseDiamond(ByVal SaleLooseDiamondObj As SaleLooseDiamondHeaderInfo) As Boolean

        Function GetAllSaleLooseDiamond() As DataTable
        Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondHeaderID As String) As SaleLooseDiamondHeaderInfo
        Function GetSaleLooseDiamondDetailByID(ByVal SaleLooseDiamondHeaderID As String) As DataTable
        'Function GetSalesVolumeDetailGemByHeaderID(ByVal SalesVolumeHeaderID As String) As DataTable
        Function GetSaleLooseDiamondPrint(ByVal SaleLooseDiamondID As String) As DataTable
        Function GetSalesVolumeDataByHeaderIDAndItemCode(ByVal SalesVolumeHeaderID As String, Optional ByVal ItemCode As String = "") As DataTable
        'Function GetSalesVolumeGemDataBySaleDetailID(ByVal SalesVolumeDetailID As String) As DataTable
        Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As DataTable
        Function GetSaleLooseDiamondByID(ByVal SaleLooseDiamondID As String) As SaleLooseDiamondHeaderInfo
        Function GetSalesInvoiceLooseDiamondReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetSaleLooseDiamondReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetProfitForSaleDiamondItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As DataTable
        Function GetSaleLooseDiamondDetailByDetailID(ByVal SaleLooseDiamondDetailID As String) As DataTable
        Function GetSaleLooseDiamondDataByCustomerIDAndItemCode(ByVal CustomerID As String, Optional ByVal CriStr As String = "") As DataTable
        Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean
        Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean
    End Interface
End Namespace
