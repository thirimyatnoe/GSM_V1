Imports CommonInfo
Namespace MortgageInterest
    Public Interface IMortgageInterestDA
        Function InsertMortgageInterest(ByVal MortgageInterestObj As MortgageInterestInfo) As Boolean
        Function UpdateMortgageInterest(ByVal MortgageInterestObj As MortgageInterestInfo) As Boolean
        Function DeleteMortgageInterest(ByVal MortgageInterestID As String) As Boolean
        Function GetMortgageInterest(ByVal MortgageInterestID As String) As MortgageInterestInfo
        Function GetMortgageInterestDataTable(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgageInterestHistoryDataTable(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgageInterestList() As DataTable
        Function GetAllMortgageInterestFromSearchBox() As DataTable
        Function GetMortgageInterestPrint(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgageInterestDate(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgageInterestByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As DataTable
    End Interface
End Namespace

