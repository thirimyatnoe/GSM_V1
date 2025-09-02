Imports CommonInfo
Namespace ReturnAdvance
    Public Interface IReturnAdvanceController
        Function SaveReturnAdvance(ByVal ReturnAdvanceObj As ReturnAdvanceInfo, ByVal _dtReturnAdvanceItem As DataTable) As Boolean
        Function DeleteReturnAdvance(ByVal ReturnAdvanceID As String) As Boolean
        Function GetAllReturnAdvance() As DataTable
        Function GetAllReturnAdvanceInCashReceipt() As DataTable
        'Function GetAllSaleGem() As DataTable
        Function GetReturnAdvance(ByVal ReturnAdvanceID As String) As ReturnAdvanceInfo
        'Function GetAllReturnAdvanceForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable

        Function GetReturnAdvanceItem(ByVal ReturnAdvanceID As String) As DataTable
        Function GetReturnAdvanceItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal optType As String = "") As DataTable
        Function GetReturnAdvancePrint(ByVal ReturnAdvanceID As String) As DataTable

        'Function InsertReturnAdvanceUserID(ByVal SalesGemsID As String, ByVal UserID As String) As Boolean
        Function GetReturnAdvanceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        'Function GetReturnAdvanceBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As ReturnAdvanceItemInfo
        'Function GetAllReturnAdvanceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        'Function GetReturnAdvanceItemByReturnAdvanceItemID(ByVal ReturnAdvanceItemID As String) As DataTable
    End Interface
End Namespace

