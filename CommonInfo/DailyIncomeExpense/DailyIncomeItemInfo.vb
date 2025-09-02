Public Class DailyIncomeItemInfo

#Region "Private Property"

    Private _DailyIncomeItemID As String
    Private _DailyIncomeID As String
    Private _IncomeID As String
    Private _QTY As Integer
    Private _UnitPrice As Integer
    Private _Amount As Integer
    Private _Description As String
    Private _Remark As String
#End Region

#Region "Properties"

    Public Property DailyIncomeItemID() As String
        Get
            Return _DailyIncomeItemID
        End Get
        Set(ByVal value As String)
            _DailyIncomeItemID = value
        End Set
    End Property

    Public Property DailyIncomeID() As String
        Get
            Return _DailyIncomeID
        End Get
        Set(ByVal value As String)
            _DailyIncomeID = value
        End Set
    End Property

    Public Property IncomeID() As String
        Get
            Return _IncomeID
        End Get
        Set(ByVal value As String)
            _IncomeID = value
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
    Public Property Amount() As Integer
        Get
            Return _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
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
