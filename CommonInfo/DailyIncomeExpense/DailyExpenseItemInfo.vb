Public Class DailyExpenseItemInfo

#Region "Private Property"

    Private _DailyExpenseItemID As String
    Private _DailyExpenseID As String
    Private _Description As String
    Private _QTY As Integer
    Private _UnitPrice As Integer
    Private _Amount As Long
    Private _Remark As String
#End Region

#Region "Properties"

    Public Property DailyExpenseItemID() As String
        Get
            Return _DailyExpenseItemID
        End Get
        Set(ByVal value As String)
            _DailyExpenseItemID = value
        End Set
    End Property

    Public Property DailyExpenseID() As String
        Get
            Return _DailyExpenseID
        End Get
        Set(ByVal value As String)
            _DailyExpenseID = value
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
    Public Property QTY() As Integer
        Get
            Return _QTY
        End Get
        Set(ByVal value As Integer)
            _QTY = value
        End Set
    End Property
    Public Property UnitPrice() As Integer
        Get
            Return _UnitPrice
        End Get
        Set(ByVal value As Integer)
            _UnitPrice = value
        End Set
    End Property
    Public Property Amount() As Long
        Get
            Return _Amount
        End Get
        Set(ByVal value As Long)
            _Amount = value
        End Set
    End Property
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
#End Region

End Class
