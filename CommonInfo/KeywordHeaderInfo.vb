Public Class KeywordHeaderInfo

    Private _KeywordID As Integer
    Private _KeywordName As String

    Public Property KeywordID() As String
        Get
            Return _KeywordID

        End Get
        Set(ByVal value As String)
            _KeywordID = value

        End Set
    End Property

    Public Property KeywordName() As String
        Get
            Return _KeywordName

        End Get
        Set(ByVal value As String)
            _KeywordName = value

        End Set
    End Property
End Class
