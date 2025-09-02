Imports CommonInfo
Namespace Customer
    Public Interface ICustomerController
        Function SaveCustomer(ByVal CustomerObj As CustomerInfo) As String
        Function DeleteCustomer(ByVal CustomerID As String) As Boolean
        Function GetAllCustomer() As DataTable
        Function GetCustomerByID(ByVal CustomerID As String) As CustomerInfo
        Function GetCustomerCode(ByVal _CustomerCode As String) As CustomerInfo
        Function GetCustomer() As DataTable
        Function GetCustomerDataByCustomerName(ByVal CustomerName As String, Optional CustomerID As String = "") As DataTable
        Function GetAllCustomerAutoCompleteData(Optional CustomerName As String = "") As DataTable
        Function GetAllCustomerForSearch(Optional CustomerName As String = "") As DataTable
    End Interface
End Namespace
