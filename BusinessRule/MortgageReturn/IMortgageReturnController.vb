Imports CommonInfo
Namespace MortgageReturn
    Public Interface IMortgageReturnController
        Function SaveMortgageReturn(ByVal MortgageReturnObj As MortgageReturnInfo, ByVal dtMortgageInvoiceItem As DataTable) As Boolean
        Function DeleteMortgageReturn(ByVal MortgageReturnID As String) As Boolean
        Function GetMortgageReturn(ByVal MortgageReturnID As String) As MortgageReturnInfo
        Function GetMortgageReturnDataTable(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgageReturnList() As DataTable
        Function GetAllMortgageReturnFromSearchBox() As DataTable
        Function GetMortgageReturnPrint(ByVal MortgageRetrunID As String) As DataTable
        Function GetMortgageReturnFromInterest(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgageReturnDate(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgageReturn() As DataTable
        Function GetMortgageReturnByID(ByVal MortgageReturnID As String) As MortgageReturnInfo
        Function GetMortgageReturnItem(ByVal MortgageReturnID As String) As DataTable
        Function GetMortgageReturnByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As DataTable
        Function GetMortgageReturnItemPrint(ByVal MortgageReturnID As String) As DataTable

    End Interface
End Namespace

