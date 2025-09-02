Imports CommonInfo
Namespace ExportData
    Public Interface IExportDataController
        Function InsertExportData(ByVal ExportDataObj As ExportDataInfo) As Boolean
        Function DeleteExportData(ByVal ExportID As Integer) As Boolean
        Function UpdateExportData(ByVal ExportDataObj As ExportDataInfo) As Boolean
        Function GetExportData() As DataTable
        Function GetAllExportData() As DataTable
        Function GetAllServiceData(Optional ByVal cristr As String = "") As DataTable

    End Interface
End Namespace



