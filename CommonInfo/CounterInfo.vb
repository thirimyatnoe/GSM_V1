Public Class CounterInfo
#Region "Private Property"
    Private _CounterID As String
    Private _CounterNo As String
    Private _Counter As String
    Private _LocationID As String
    Private _IsOrderCounter As Boolean
#End Region
#Region "Properties "
    Public Property CounterID() As String
        Get
            Return _CounterID

        End Get
        Set(ByVal value As String)
            _CounterID = value
        End Set
    End Property
    Public Property CounterNO() As String
        Get
            Return _CounterNo

        End Get
        Set(ByVal value As String)
            _CounterNo = value
        End Set
    End Property
    Public Property Counter() As String
        Get
            Return _Counter

        End Get
        Set(ByVal value As String)
            _Counter = value
        End Set
    End Property
    Public Property LocationID() As String
        Get
            Return _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property
    Public Property IsOrderCounter() As Boolean
        Get
            Return _IsOrderCounter
        End Get
        Set(ByVal value As Boolean)
            _IsOrderCounter = value
        End Set
    End Property
#End Region
End Class
