Public Class DailyExpenseInfo

#Region "Private Property"

    Private _DailyExpenseID As String
    Private _ExpenseDate As Date
    Private _Remark As String
    Private _TotalAmount As Long
    Private _LocationID As String
    Private _ReturnAmount As Long
    Private _IsBank As Boolean

#End Region

#Region "Properties"

    Public Property DailyExpenseID() As String
        Get
            Return _DailyExpenseID
        End Get
        Set(ByVal value As String)
            _DailyExpenseID = value
        End Set
    End Property

    Public Property ExpenseDate() As Date
        Get
            Return _ExpenseDate
        End Get
        Set(ByVal value As Date)
            _ExpenseDate = value
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

    Public Property TotalAmount() As Long
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Long)
            _TotalAmount = value
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
    Public Property ReturnAmount() As Long
        Get
            Return _ReturnAmount
        End Get
        Set(ByVal value As Long)
            _ReturnAmount = value
        End Set
    End Property

    Public Property IsBank() As Boolean
        Get
            Return _IsBank
        End Get
        Set(ByVal value As Boolean)
            _IsBank = value
        End Set
    End Property
#End Region

End Class
