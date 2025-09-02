Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports CommonInfo

Namespace ExportServiceLogs

    Public Interface IExportServiceLogsDA

        Function InsertUploadDownload(ByVal ExportServiceLogsObj As ExportServiceLogsInfo) As Integer
        Function UpdateUploadDownload(ByVal ExportServiceLogsObj As ExportServiceLogsInfo) As Boolean
        Function GetUploadDownloadByStr(ByVal cristr As String) As System.Data.DataTable
        Function GetLastUploadDataByExportID(ByVal ExportID As String, ByVal cristr As String) As System.Data.DataTable
        Function GetTotalLogDays() As Integer
    End Interface
End Namespace