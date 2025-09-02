Imports CommonInfo
Namespace Location
    Public Interface ILocationDA
        Function InsertLocation(ByVal LocationObj As LocationInfo) As Boolean
        Function UpdateLocation(ByVal LocationObj As LocationInfo) As Boolean
        Function DeleteLocation(ByVal LocationID As String) As Boolean
        Function GetLocationByID(ByVal LocationID As String) As LocationInfo
        Function GetAllLocationList() As DataTable

        Function GetLocationID(Optional ByVal LocationID As String = "") As DataTable
        Function GetCounterList(Optional ByVal LocationID As String = "") As DataTable

        Function InsertCounter(ByVal CounterObj As CounterInfo) As Boolean
        Function UpdateCounter(ByVal CounterObj As CounterInfo) As Boolean
        Function DeleteCouter(ByVal LocationID As String, ByVal CounterID As String) As Boolean
        Function GetSubCounter(ByVal LocationID As String, Optional ByVal cristr As String = "") As DataTable

        Function GetAllLocationExportData() As DataTable

        Function GetCurrentLocationID() As String
        Function SaveCurrentLocation(ByVal LocationID As String) As Boolean
        Function DeleteCurrentLocation() As Boolean
        Function CountIsOrderCountByLocationID(ByVal LocationID As String) As Integer
        Function GetCounterByCounterID(ByVal LocationID As String, Optional ByVal cristr As String = "") As String
        Function GetLocationIDByLocName(ByVal Location As String) As LocationInfo
        Function GetAllLocation() As DataTable
        Function GetAllLocationData() As DataTable
        Function CheckIsExitHeadOfficeInLocation(Optional LocationID As String = "") As DataTable
        Function CheckTransferInfo() As GlobalSettingInfo
        Function GetCompanyProfileList(Optional ByVal str As String = "") As DataTable

    End Interface
End Namespace

