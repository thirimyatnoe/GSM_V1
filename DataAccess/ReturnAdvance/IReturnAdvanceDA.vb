Imports CommonInfo
Namespace ReturnAdvance
    Public Interface IReturnAdvanceDA
        Function InsertReturnAdvance(ByVal ReturnAdvanceObj As ReturnAdvanceInfo) As Boolean
        Function UpdateReturnAdvance(ByVal ReturnAdvanceObj As ReturnAdvanceInfo) As Boolean
        Function DeleteReturnAdvance(ByVal ReturnAdvanceID As String) As Boolean
        Function GetAllReturnAdvance() As DataTable
        Function GetAllReturnAdvanceInCashReceipt() As DataTable
        Function UpdateReturnAdvanceItemIsUsed(ByVal ReturnAdvanceID As CommonInfo.ReturnAdvanceItemInfo) As Boolean

        Function GetReturnAdvance(ByVal ReturnAdvanceID As String) As ReturnAdvanceInfo
        'Function GetAllReturnAdvanceForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable

        Function InsertReturnAdvanceItem(ByVal ObjReturnAdvanceItem As ReturnAdvanceItemInfo) As Boolean
        Function UpdateReturnAdvanceItem(ByVal ObjReturnAdvanceItem As ReturnAdvanceItemInfo) As Boolean
        Function DeleteReturnAdvanceItem(ByVal ReturnAdvanceItemID As String) As Boolean
        Function GetReturnAdvancePrint(ByVal ReturnAdvanceID As String) As DataTable
        Function GetReturnAdvanceItem(ByVal ReturnAdvanceID As String) As DataTable
        Function GetReturnAdvanceItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal optType As String = "") As DataTable


        'Function InsertReturnAdvanceUserID(ByVal ReturnAdvanceID As String, ByVal UserID As String) As Boolean
        Function GetReturnAdvanceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        'Function GetReturnAdvanceBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As ReturnAdvanceItemInfo
        'Function GetAllReturnAdvanceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        'Function GetReturnAdvanceItemByReturnAdvanceItemID(ByVal ReturnAdvanceItemID As String) As DataTable
    End Interface
End Namespace

