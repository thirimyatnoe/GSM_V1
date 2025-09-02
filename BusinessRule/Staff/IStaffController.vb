Imports CommonInfo
Namespace Staff
    Public Interface IStaffController
        Function InsertStaff(ByVal _dtStaff As DataTable) As Boolean
        Function GetStaffList() As DataTable
        Function GetStaffListByLocation(ByVal LocationID As String) As DataTable

        Function GetStaff(ByVal StaffID As String) As StaffInfo
        Function GetrptStaff() As DataTable
        Function GetStaffDataByStaff(ByVal Staff As String, Optional StaffID As String = "") As DataTable
    End Interface
End Namespace

