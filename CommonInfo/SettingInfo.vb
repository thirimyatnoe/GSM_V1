Public Class SettingInfo

    Private _KeyName As String
    Private _Description As String
    Private _KeyValue As String

    Public Sub New(ByVal KeyName As String, ByVal Description As String, ByVal KeyValue As String)
        _KeyName = KeyName
        _Description = Description
        _KeyValue = KeyValue
    End Sub
    Public Property KeyName() As String
        Get
            Return _KeyName

        End Get
        Set(ByVal value As String)
            _KeyName = value

        End Set
    End Property

    Public Property Description() As String
        Get
            Return _Description

        End Get
        Set(ByVal value As String)
            _Description = value

        End Set
    End Property

    Public Property KeyValue() As String
        Get
            Return _KeyValue

        End Get
        Set(ByVal value As String)
            _KeyValue = value

        End Set
    End Property

End Class
