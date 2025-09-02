Imports DataAccess.ExportServiceLogs


Namespace ExportServiceLogs

    Public Class ExportServiceLogsController
        Implements IExportServiceLogsController


#Region "Private Members"

        Private _objExportServiceLogsDA As IExportServiceLogsDA

        Private Shared ReadOnly _instance As IExportServiceLogsController = New ExportServiceLogsController

#End Region

#Region "Constructors"

        Public Sub New()
            _objExportServiceLogsDA = DataAccess.Factory.Instance.CreateExportServiceLogsDA

        End Sub

#End Region
#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IExportServiceLogsController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetLastUploadDataByExportID(ExportID As String, cristr As String) As DataTable Implements IExportServiceLogsController.GetLastUploadDataByExportID
            Return _objExportServiceLogsDA.GetLastUploadDataByExportID(ExportID, cristr)
        End Function

        Public Function GetTotalLogDays() As Integer Implements IExportServiceLogsController.GetTotalLogDays
            Return _objExportServiceLogsDA.GetTotalLogDays()
        End Function

        Public Function GetUploadDownloadByStr(cristr As String) As DataTable Implements IExportServiceLogsController.GetUploadDownloadByStr
            Return _objExportServiceLogsDA.GetUploadDownloadByStr(cristr)
        End Function

        Public Function InsertUploadDownload(ExportServiceLogsObj As CommonInfo.ExportServiceLogsInfo) As Integer Implements IExportServiceLogsController.InsertUploadDownload
            Return _objExportServiceLogsDA.InsertUploadDownload(ExportServiceLogsObj)
        End Function

        Public Function UpdateUploadDownload(ExportServiceLogsObj As CommonInfo.ExportServiceLogsInfo) As Boolean Implements IExportServiceLogsController.UpdateUploadDownload
            Return _objExportServiceLogsDA.UpdateUploadDownload(ExportServiceLogsObj)
        End Function
    End Class
End Namespace