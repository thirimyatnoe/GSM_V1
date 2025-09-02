Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Configuration

Namespace ExportServiceLogs

    Class ExportServiceLogsDA
        Implements IExportServiceLogsDA

#Region "Private Members"

        Private DB As Database
        Private AcessDBPath As String
        Private Shared ReadOnly _instance As IExportServiceLogsDA = New ExportServiceLogsDA
        Private Shared config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Private Shared Connection As ConnectionStringSettings
        Private Shared connections As ConnectionStringSettingsCollection
        Public _ConectionString As String = ""

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ExportServiceLogsDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function InsertUploadDownload(ByVal ExportServiceLogsObj As ExportServiceLogsInfo) As Integer Implements IExportServiceLogsDA.InsertUploadDownload

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO  tbl_ExportServiceLogs (logdatetime,logtype,synctype,status,ExportID,ExportFilePath,message,FailBranchID) "
                strCommandText = (strCommandText + " VALUES (@logdatetime, @logtype,@synctype,@status,@ExportID,@ExportFilePath,@message,@FailBranchID)")
                strCommandText = strCommandText & " SELECT @@Identity"
                DBComm = Me.DB.GetSqlStringCommand(strCommandText)
                Me.DB.AddInParameter(DBComm, "@logdatetime", DbType.DateTime, ExportServiceLogsObj.logdatetime)
                Me.DB.AddInParameter(DBComm, "@logtype", DbType.String, ExportServiceLogsObj.logtype)
                Me.DB.AddInParameter(DBComm, "@synctype", DbType.String, ExportServiceLogsObj.synctype)
                Me.DB.AddInParameter(DBComm, "@status", DbType.String, ExportServiceLogsObj.status)
                Me.DB.AddInParameter(DBComm, "@ExportID", DbType.String, ExportServiceLogsObj.ExportID)
                Me.DB.AddInParameter(DBComm, "@ExportFilePath", DbType.String, ExportServiceLogsObj.ExportFilePath)
                Me.DB.AddInParameter(DBComm, "@message", DbType.String, ExportServiceLogsObj.Message)
                Me.DB.AddInParameter(DBComm, "@FailBranchID ", DbType.String, ExportServiceLogsObj.FailBranchID)
                Dim newID As Integer = CInt(Me.DB.ExecuteScalar(DBComm))
                If (newID > 0) Then
                    Return newID
                Else
                    Return 0
                End If

            Catch ex As Exception
                Return 0
            End Try

        End Function

        Public Function UpdateUploadDownload(ByVal ExportServiceLogsObj As ExportServiceLogsInfo) As Boolean Implements IExportServiceLogsDA.UpdateUploadDownload
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "  UPDATE tbl_ExportServiceLogs SET synctype = @synctype, status = @status, message = @message , UploadBranchID = @UploadBranchID , FailBranchID = @FailBranchID "
                strCommandText = (strCommandText + "   WHERE ID= @ID ")
                '  strCommandText += "   WHERE logtype = @logtype and synctype = @synctype ";
                DBComm = Me.DB.GetSqlStringCommand(strCommandText)
                'Me.DB.AddInParameter(DBComm, "@logdatetime", DbType.DateTime, ExportServiceLogsObj.logdatetime)
                Me.DB.AddInParameter(DBComm, "@synctype", DbType.String, ExportServiceLogsObj.synctype)
543:            Me.DB.AddInParameter(DBComm, "@UploadBranchID ", DbType.String, ExportServiceLogsObj.UploadBranchID)
                Me.DB.AddInParameter(DBComm, "@FailBranchID ", DbType.String, ExportServiceLogsObj.FailBranchID)
                Me.DB.AddInParameter(DBComm, "@status", DbType.String, ExportServiceLogsObj.status)
                Me.DB.AddInParameter(DBComm, "@message", DbType.String, ExportServiceLogsObj.Message)
                Me.DB.AddInParameter(DBComm, "@ID", DbType.Int16, ExportServiceLogsObj.ID)
                If (Me.DB.ExecuteNonQuery(DBComm) > 0) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try

        End Function

        Public Function GetUploadDownloadByStr(ByVal cristr As String) As System.Data.DataTable Implements IExportServiceLogsDA.GetUploadDownloadByStr
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select * from tbl_ExportServiceLogs where 1=1  " _
                            + cristr + " order by logdatetime desc "
                DBComm = Me.DB.GetSqlStringCommand(strCommandText)
                dtResult = Me.DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function

        Public Function GetTotalLogDays() As Integer Implements IExportServiceLogsDA.GetTotalLogDays
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim count As Int32 = 0
            Try
                strCommandText = "SELECT DATEDIFF(day, min(logdatetime) , GETDATE()  ) As logsday from tbl_ExportServiceLogs"
                DBComm = Me.DB.GetSqlStringCommand(strCommandText)
                count = CInt(Me.DB.ExecuteScalar(DBComm))
            Catch ex As Exception

            End Try
            Return count
        End Function

        Public Function GetLastUploadDataByExportID(ByVal ExportID As String, ByVal cristr As String) As DataTable Implements IExportServiceLogsDA.GetLastUploadDataByExportID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT TOP (1) ID, logdatetime, logtype, synctype, status  " _
                                 + " FROM tbl_ExportServiceLogs WHERE  (ExportID = @ExportID) " + cristr + "  " _
                                 + " order by logdatetime desc  "
                             
                DBComm = Me.DB.GetSqlStringCommand(strCommandText)
                Me.DB.AddInParameter(DBComm, "@ExportID", DbType.String, ExportID)
                dtResult = Me.DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace