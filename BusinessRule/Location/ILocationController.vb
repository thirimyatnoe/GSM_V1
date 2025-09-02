Imports CommonInfo
Namespace Location
    Public Interface ILocationController
        Function InsertLocation(ByVal LocationObj As LocationInfo) As Boolean
        Function GetAllLocationList() As DataTable
        Function GetSubCounter(ByVal LocationID As String, Optional ByVal cristr As String = "") As DataTable
        Function DeleteLocation(ByVal LocationID As String) As Boolean
        Function GetLocationByID(ByVal LocationID As String) As LocationInfo


        Function GetLocationID(Optional ByVal LocationID As String = "") As DataTable
        Function GetCounterList(Optional ByVal LocationID As String = "") As DataTable

        Function GetCurrentLocationID() As String
        Function SaveCurrentLocation(ByVal LocationID As String) As Boolean
        Function DeleteCurrentLocation() As Boolean
        Function CountIsOrderCountByLocationID(ByVal LocationID As String) As Integer
        Function GetCounterByCounterID(ByVal LocationID As String, Optional ByVal cristr As String = "") As String
        Function GetLocationIDByLocName(ByVal Location As String) As LocationInfo
        Function GetAllLocation() As DataTable
        Function GetAllLocationData() As DataTable
        Function GetAllLocationExportData() As DataTable
        Function CheckIsExitHeadOfficeInLocation(Optional LocationID As String = "") As DataTable
        Function CheckTransferInfo() As GlobalSettingInfo
        Function GetCompanyProfileList(Optional ByVal str As String = "") As DataTable
    End Interface
End Namespace

