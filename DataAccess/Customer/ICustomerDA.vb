Imports CommonInfo
Namespace Customer
    Public Interface ICustomerDA
        Function InsertCustomer(ByVal CustomerObj As CustomerInfo) As Boolean
        Function UpdateCustomer(ByVal CustomerObj As CustomerInfo) As Boolean
        Function DeleteCustomer(ByVal CustomerID As String) As Boolean
        Function GetAllCustomer() As DataTable
        Function GetCustomerByID(ByVal CustomerID As String) As CustomerInfo
        Function GetCustomerCode(ByVal _CustomerCode As String) As CustomerInfo
        Function GetCustomer() As DataTable
        Function GetCustomerDataByCustomerName(ByVal CustomerName As String, Optional ByVal CustomerID As String = "") As DataTable
        Function GetAllCustomerAutoCompleteData(Optional ByVal CustomerName As String = "") As DataTable
        Function GetAllCustomerForSearch(Optional ByVal CustomerName As String = "") As DataTable
    End Interface
End Namespace
