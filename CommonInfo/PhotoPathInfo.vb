Public Class PhotoPathInfo

    Private _PhotoPathID As String
    Private _PhotoPath As String
    'Private _OneFinger As Boolean


    Public Property PhotoPathID() As String
        Get
            Return _PhotoPathID
        End Get
        Set(ByVal value As String)
            _PhotoPathID = value
        End Set
    End Property

    Public Property PhotoPath() As String
        Get
            Return _PhotoPath
        End Get
        Set(ByVal value As String)
            _PhotoPath = value
        End Set
    End Property
    'Public Property OneFinger() As Boolean
    '    Get
    '        Return _OneFinger
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _OneFinger = value
    '    End Set
    'End Property


End Class
