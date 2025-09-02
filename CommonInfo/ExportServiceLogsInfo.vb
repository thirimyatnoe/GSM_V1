Public Class ExportServiceLogsInfo

    Private _ID As Integer

    Public Property ID As Integer
        Get
            Return Me._ID
        End Get
        Set(value As Integer)
            Me._ID = value
        End Set
    End Property

    Private _logdatetime As DateTime

    Public Property logdatetime As DateTime
        Get
            Return Me._logdatetime
        End Get
        Set(value As DateTime)
            Me._logdatetime = value
        End Set
    End Property

    Private _logtype As String

    Public Property logtype As String
        Get
            Return Me._logtype
        End Get
        Set(value As String)
            Me._logtype = value
        End Set
    End Property

    Private _synctype As String

    Public Property synctype As String
        Get
            Return Me._synctype
        End Get
        Set(value As String)
            Me._synctype = value
        End Set
    End Property

    Private _status As String

    Public Property status As String
        Get
            Return Me._status
        End Get
        Set(value As String)
            Me._status = value
        End Set
    End Property

    Private _ExportID As String

    Public Property ExportID As String
        Get
            Return Me._ExportID
        End Get
        Set(value As String)
            Me._ExportID = value
        End Set
    End Property

    Private _Message As String

    Public Property Message As String
        Get
            Return Me._Message
        End Get
        Set(value As String)
            Me._Message = value
        End Set
    End Property

    Private _UploadBranchID As String
    Public Property UploadBranchID As String
        Get
            Return Me._UploadBranchID
        End Get
        Set(value As String)
            Me._UploadBranchID = value
        End Set
    End Property

    Private _FailBranchID As String
    Public Property FailBranchID As String
        Get
            Return Me._FailBranchID
        End Get
        Set(value As String)
            Me._FailBranchID = value
        End Set
    End Property

    Private _ExportFilePath As String
    Public Property ExportFilePath As String
        Get
            Return Me._ExportFilePath
        End Get
        Set(value As String)
            Me._ExportFilePath = value
        End Set
    End Property
End Class