Public Class KeywordItemInfo

    Private _ItemID As Integer
    Private _KeywordID As Integer
    Private _ItemName As String

    Public Property ItemID() As Integer
        Get
            Return _ItemID
        End Get
        Set(ByVal value As Integer)
            _ItemID = value
        End Set
    End Property

    Public Property KeywordID() As Integer
        Get
            Return _KeywordID
        End Get
        Set(ByVal value As Integer)
            _KeywordID = value
        End Set
    End Property

    Public Property ItemName() As String
        Get
            Return _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
        End Set
    End Property
End Class
