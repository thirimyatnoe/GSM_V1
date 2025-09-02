Imports Microsoft.Win32
Public Class SystemConfigurationInfo
    Private _MachineName As String
    Private _UserName As String
    Private _OSVersion As String
    Private _DomainName As String

    Public Sub New()
        Me._MachineName = Environment.MachineName
        Me._UserName = Environment.UserName
        Me._DomainName = Environment.UserDomainName
        Me._OSVersion = Environment.OSVersion.VersionString
    End Sub

    Public Property MachineName() As String
        Get
            Return _MachineName
        End Get
        Set(ByVal value As String)
            _MachineName = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Public Property OSVersion() As String
        Get
            Return _OSVersion
        End Get
        Set(ByVal value As String)
            _OSVersion = value
        End Set
    End Property

    Public Property DomainName() As String
        Get
            Return _DomainName
        End Get
        Set(ByVal value As String)
            _DomainName = value
        End Set
    End Property
End Class

