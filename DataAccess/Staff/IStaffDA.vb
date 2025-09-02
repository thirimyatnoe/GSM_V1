Imports CommonInfo
Namespace Staff
    Public Interface IStaffDA
        Function InsertStaff(ByVal StaffObj As StaffInfo) As Boolean
        Function GetStaffList() As DataTable
        Function GetStaffListByLocation(ByVal LocationID As String) As DataTable

        Function UpdateStaff(ByVal StaffObj As StaffInfo) As Boolean
        Function DeleteStaff(ByVal StaffID As String) As Boolean
        Function GetStaff(ByVal StaffID As String) As StaffInfo
        Function GetrptStaff() As DataTable
        Function GetStaffDataByStaff(ByVal Staff As String, Optional StaffID As String = "") As DataTable
    End Interface
End Namespace

