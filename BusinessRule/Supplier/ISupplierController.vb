Imports CommonInfo
Namespace Supplier
    Public Interface ISupplierController
        Function SaveSupplier(ByVal SupplierObj As SupplierInfo) As String
        Function DeleteSupplier(ByVal SupplierID As String) As Boolean
        Function GetAllSupplier() As DataTable
        Function GetSupplierByID(ByVal SupplierID As String) As SupplierInfo
        Function GetSupplierDataByCode(ByVal SupplierCode As String) As SupplierInfo
        Function GetAllSupplierList() As DataTable
        Function GetAllSupplierListByLocation(ByVal LocationID As String) As DataTable

    End Interface
End Namespace
