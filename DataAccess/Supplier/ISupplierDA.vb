Imports CommonInfo
Namespace Supplier
    Public Interface ISupplierDA
        Function InsertSupplier(ByVal SupplierObj As SupplierInfo) As Boolean
        Function UpdateSupplier(ByVal SupplierObj As SupplierInfo) As Boolean
        Function DeleteSupplier(ByVal SupplierID As String) As Boolean
        Function GetAllSupplier() As DataTable
        Function GetSupplierByID(ByVal SupplierID As String) As SupplierInfo
        Function GetSupplierDataByCode(ByVal SupplierCode As String) As SupplierInfo
        Function GetAllSupplierList() As DataTable
        Function GetAllSupplierListByLocation(ByVal LocationID As String) As DataTable

    End Interface
End Namespace
