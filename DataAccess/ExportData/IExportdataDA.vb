Imports CommonInfo
Namespace ExportData
    Public Interface IExportDataDA
        Function InsertExportData(ByVal ExportDataObj As ExportDataInfo) As Boolean
        Function UpdateExportData(ByVal ExportDataObj As ExportDataInfo) As Boolean

        Function DeleteExportData(ByVal ExportDataID As Integer) As Boolean
        Function GetAllExportData() As DataTable
        Function GetAllServiceData(Optional ByVal cristr As String = "") As DataTable


    End Interface

End Namespace
