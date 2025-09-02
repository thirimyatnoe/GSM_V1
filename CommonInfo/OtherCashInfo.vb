Public Class OtherCashInfo
#Region "Private Property"
    Private _RecordCashID As String
    Private _VoucherNo As String
    Private _CashTypeID As String
    Private _ExchangeRate As Integer
    Private _Amount As Integer
#End Region

#Region "Properties "
    Public Property RecordCashID() As String
        Get
            Return _RecordCashID

        End Get
        Set(ByVal value As String)
            _RecordCashID = value
        End Set
    End Property

    Public Property VoucherNo() As String
        Get
            Return _VoucherNo

        End Get
        Set(ByVal value As String)
            _VoucherNo = value
        End Set
    End Property

    Public Property CashTypeID() As String
        Get
            Return _CashTypeID

        End Get
        Set(ByVal value As String)
            _CashTypeID = value
        End Set
    End Property

    Public Property ExchangeRate() As Integer
        Get
            Return _ExchangeRate

        End Get
        Set(ByVal value As Integer)
            _ExchangeRate = value
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
#End Region

End Class
