Imports CommonInfo
Namespace MortgageReturn
    Public Interface IMortgageReturnDA
        Function InsertMortgageReturn(ByVal MortgageReturnObj As MortgageReturnInfo) As Boolean
        Function UpdateMortgageReturn(ByVal MortgageReturnObj As MortgageReturnInfo) As Boolean
        Function DeleteMortgageReturn(ByVal MortgageReturnID As String) As Boolean
        Function GetMortgageReturn(ByVal MortgageReturnID As String) As MortgageReturnInfo
        Function GetMortgageReturnDataTable(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgageReturnList() As DataTable
        Function GetAllMortgageReturnFromSearchBox() As DataTable
        Function GetMortgageReturnPrint(ByVal MortgageRetrunID As String) As DataTable
        Function UpdateMortgageInvoiceByPayback(ByVal Obj As MortgageInvoiceInfo, ByVal MortgageReturnID As String) As Boolean
        Function GetMortgageReturnFromInterest(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgageReturnDate(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgageReturn() As DataTable
        Function GetMortgageReturnByID(ByVal MortgageReturnID As String) As MortgageReturnInfo
        Function GetMortgageReturnItem(ByVal MortgageReturnID As String) As DataTable
        Function InsertMortgageReturnItem(ByVal MortgageReturnItemObj As MortgageReturnItemInfo) As Boolean
        Function DeleteMortgageReturnItem(ByVal MortgageReturnItemID As String) As Boolean
        Function GetMortgageReturnByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As DataTable
        Function GetMortgageReturnItemPrint(ByVal MortgageReturnID As String) As DataTable
        Function UpdateMortgageReturnItem(ByVal MortgageReturnItemObj As MortgageReturnItemInfo) As Boolean
    End Interface
End Namespace

