Imports CommonInfo
Namespace MortgagePayback
    Public Interface IMortgagePaybackController
        Function SaveMortgagePayback(ByVal MortgagePaybackObj As MortgagePaybackInfo, ByVal MortgageInvoicePaybackobj As MortgageInvoiceInfo, ByVal dtMortgageInvoiceItem As DataTable) As Boolean
        Function DeleteMortgagePayback(ByVal MortgageInvoiceID As String, ByVal MortgagePaybackID As String, ByVal _dtMortgageInvoice As DataTable) As Boolean
        Function GetMortgagePayback(ByVal MortgagePaybackID As String) As MortgagePaybackInfo
        Function GetMortgagePaybackDataTable(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgagePaybackList() As DataTable
        Function GetAllMortgagePaybackFromSearchBox() As DataTable
        Function GetMortgagePaybackPrint(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgagePaybackFromInterest(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgagePaybackDate(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgagePayback() As DataTable
        Function GetMortgagePaybackByID(ByVal MortgagePaybackID As String) As MortgagePaybackInfo
        Function GetMortgagePaybackItem(ByVal MortgagePaybackID As String) As DataTable
        Function GetMortgagePaybackByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As DataTable
        Function GetMortgagePaybackItemPrint(ByVal MortgagePaybackID As String) As DataTable
    End Interface
End Namespace

