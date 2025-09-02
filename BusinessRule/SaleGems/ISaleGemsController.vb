Imports CommonInfo
Namespace SaleGems
    Public Interface ISaleGemsController
        Function SaveSaleGems(ByVal SaleGemsObj As SaleGemsInfo, ByVal _dtSaleGemsItem As DataTable, ByVal _dtOtherCash As DataTable) As Boolean
        Function DeleteSaleGems(ByVal SaleGemsID As String) As Boolean
        Function GetAllSaleGems() As DataTable
        Function GetAllSaleGem() As DataTable
        Function GetSaleGems(ByVal SaleGemsID As String) As SaleGemsInfo
        Function GetAllSaleGemsForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable

        Function GetSaleGemsItem(ByVal SaleGemsID As String) As DataTable
        Function GetSaleGemsItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleGemsPrint(ByVal SaleGemsID As String) As DataTable

        Function InsertSaleGemsUserID(ByVal SalesGemsID As String, ByVal UserID As String) As Boolean
        Function GetSaleGemsReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleGemsBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As SaleGemsItemInfo
        Function GetAllSaleGemsVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleGemsItemBySaleGemsItemID(ByVal SaleGemsItemID As String) As DataTable
        Function GetSaleGemsDataByCustomerIDAndItemCode(ByVal CustomerID As String, Optional ByVal CriStr As String = "") As DataTable
        Function GetOtherCashDataByVoucherNo(ByVal SaleInoiceHeaderID As String) As DataTable

    End Interface
End Namespace

