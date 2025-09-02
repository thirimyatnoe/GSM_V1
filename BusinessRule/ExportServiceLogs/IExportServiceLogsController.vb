Imports CommonInfo
Namespace ExportServiceLogs
    Public Interface IExportServiceLogsController
        Function InsertUploadDownload(ByVal ExportServiceLogsObj As ExportServiceLogsInfo) As Integer
        Function UpdateUploadDownload(ByVal ExportServiceLogsObj As ExportServiceLogsInfo) As Boolean
        Function GetUploadDownloadByStr(ByVal cristr As String) As System.Data.DataTable
        Function GetTotalLogDays() As Integer
        Function GetLastUploadDataByExportID(ByVal ExportID As String, ByVal cristr As String) As DataTable
    End Interface
End Namespace




