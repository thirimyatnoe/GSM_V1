Imports DataAccess.EventLogs
Namespace EventLogs
    Public Class EventLogsController
        Dim objEventLogDAL As New EventLogDAL
        Public Function ReadLog(ByVal argFromDate As Nullable(Of DateTime), ByVal argToDate As Nullable(Of DateTime), ByVal argType As String, ByVal argSource As String, ByVal argAffected As String, Optional ByVal argDataChange As String = "") As DataTable
            Return objEventLogDAL.ReadLog(argFromDate, argToDate, argType, argSource, argAffected, argDataChange)
        End Function
        Public Function ClearAllLogNow() As Boolean
            Return objEventLogDAL.ClearAllLogNow
        End Function
        Public Function GetForAffectedIDByBarcodeNo(ByVal BarcodeNo As String) As String
            Return objEventLogDAL.GetForAffectedIDByBarcodeNo(BarcodeNo)
        End Function

    End Class
End Namespace

