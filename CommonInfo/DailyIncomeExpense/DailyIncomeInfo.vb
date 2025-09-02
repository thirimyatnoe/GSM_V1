Public Class DailyIncomeInfo

#Region "Private Property"

    Private _DailyIncomeID As String
    Private _IncomeDate As Date
    Private _Remark As String
    Private _TotalAmount As Integer
    Private _LocationID As String
    Private _CashReceiptID As String
    Private _IsBank As Boolean

#End Region

#Region "Properties"

    Public Property DailyIncomeID() As String
        Get
            Return _DailyIncomeID
        End Get
        Set(ByVal value As String)
            _DailyIncomeID = value
        End Set
    End Property

    Public Property IncomeDate() As Date
        Get
            Return _IncomeDate
        End Get
        Set(ByVal value As Date)
            _IncomeDate = value
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

    Public Property TotalAmount() As Integer
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Integer)
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
    Public Property CashReceiptID() As String
        Get
            Return _CashReceiptID
        End Get
        Set(ByVal value As String)
            _CashReceiptID = value
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
