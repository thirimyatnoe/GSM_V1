Imports CommonInfo
Namespace MortgagePayback
    Public Interface IMortgagePaybackDA
        Function InsertMortgagePayback(ByVal MortgagePaybackObj As MortgagePaybackInfo) As Boolean
        Function UpdateMortgagePayback(ByVal MortgagePaybackObj As MortgagePaybackInfo) As Boolean
        Function DeleteMortgagePayback(ByVal MortgagePaybackID As String) As Boolean
        Function GetMortgagePayback(ByVal MortgagePaybackID As String) As MortgagePaybackInfo
        Function GetMortgagePaybackDataTable(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgagePaybackList() As DataTable
        Function GetAllMortgagePaybackFromSearchBox() As DataTable
        Function GetMortgagePaybackPrint(ByVal MortgageInvoiceID As String) As DataTable

        Function UpdateMortgageInvoiceByPayback(ByVal Obj As MortgageInvoiceInfo, ByVal MortgagePaybackID As String) As Boolean
        Function GetMortgagePaybackFromInterest(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgagePaybackDate(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgagePayback() As DataTable
        Function GetMortgagePaybackByID(ByVal MortgagePaybackID As String) As MortgagePaybackInfo
        Function GetMortgagePaybackItem(ByVal MortgagePaybackID As String) As DataTable
        Function InsertMortgagePaybackItem(ByVal MortgagePaybackItemObj As MortgagePaybackItemInfo) As Boolean
        Function DeleteMortgagePaybackItem(ByVal MortgagePaybackItemID As String) As Boolean
        Function GetMortgagePaybackByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As DataTable
        Function GetMortgagePaybackItemPrint(ByVal MortgagePaybackID As String) As DataTable
        Function UpdateMortgagePaybackItem(ByVal MortgagePaybackItemObj As MortgagePaybackItemInfo) As Boolean
    End Interface
End Namespace

