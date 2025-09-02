Imports CommonInfo
Namespace SaleGems
    Public Interface ISaleGemsDA
        Function InsertSaleGems(ByVal SaleGemsObj As SaleGemsInfo) As Boolean
        Function UpdateSaleGems(ByVal SaleGemsObj As SaleGemsInfo) As Boolean
        Function DeleteSaleGems(ByVal SaleGemsID As String) As Boolean
        Function GetAllSaleGems() As DataTable
        Function GetAllSaleGem() As DataTable
        Function GetSaleGems(ByVal SaleGemsID As String) As SaleGemsInfo
        Function GetAllSaleGemsForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable

        Function InsertSaleGemsItem(ByVal ObjSaleGemsItem As SaleGemsItemInfo) As Boolean
        Function UpdateSaleGemsItem(ByVal ObjSaleGemsItem As SaleGemsItemInfo) As Boolean
        Function DeleteSaleGemsItem(ByVal SaleGemsItemID As String) As Boolean
        Function GetSaleGemsItem(ByVal SaleGemsID As String) As DataTable
        Function GetSaleGemsItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleGemsPrint(ByVal SaleGemsID As String) As DataTable

        Function InsertSaleGemsUserID(ByVal SaleGemsID As String, ByVal UserID As String) As Boolean
        Function GetSaleGemsReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleGemsBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As SaleGemsItemInfo
        Function GetAllSaleGemsVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetSaleGemsItemBySaleGemsItemID(ByVal SaleGemsItemID As String) As DataTable
        Function GetSaleGemsDataByCustomerIDAndItemCode(ByVal CustomerID As String, Optional ByVal criStr As String = "") As DataTable
        'TMN 07022019 for Chein
        Function InsertRecordCash(ByVal ObjOtherCash As OtherCashInfo) As Boolean
        Function UpdateRecordCash(ByVal ObjOtherCash As OtherCashInfo) As Boolean
        Function DeleteRecordCash(ByVal OtherCashID As String) As Boolean
        Function GetOtherCashDataByVoucherNo(ByVal SaleGemsID As String) As DataTable


    End Interface
End Namespace

