Public Class MortgageInterestInfo
#Region "Private Property"
    Private _MortgageInterestID As String
    Private _MortgageInvoiceID As String
    Private _FromDate As Date
    Private _ToDate As Date
    Private _InterestAmount As Integer
    Private _PaidAmount As Integer
    Private _InterestPaidDate As Date
    Private _DiscountAmount As Integer
    Private _Remark As String
    Private _InterestMonth As Integer
#End Region

#Region "Properties "
    Public Property MortgageInterestID() As String
        Get
            MortgageInterestID = _MortgageInterestID
        End Get
        Set(ByVal value As String)
            _MortgageInterestID = value
        End Set
    End Property
    Public Property MortgageInvoiceID() As String
        Get
            MortgageInvoiceID = _MortgageInvoiceID
        End Get
        Set(ByVal value As String)
            _MortgageInvoiceID = value
        End Set
    End Property
    Public Property FromDate() As Date
        Get
            FromDate = _FromDate
        End Get
        Set(ByVal value As Date)
            _FromDate = value
        End Set
    End Property
    Public Property ToDate() As Date
        Get
            ToDate = _ToDate
        End Get
        Set(ByVal value As Date)
            _ToDate = value
        End Set
    End Property
    Public Property InterestAmount() As Integer
        Get
            InterestAmount = _InterestAmount
        End Get
        Set(ByVal value As Integer)
            _InterestAmount = value
        End Set
    End Property
    Public Property PaidAmount() As Integer
        Get
            PaidAmount = _PaidAmount
        End Get
        Set(ByVal value As Integer)
            _PaidAmount = value
        End Set
    End Property
    Public Property InterestPaidDate() As Date
        Get
            InterestPaidDate = _InterestPaidDate
        End Get
        Set(ByVal value As Date)
            _InterestPaidDate = value
        End Set
    End Property

    Public Property DiscountAmount() As Integer
        Get
            DiscountAmount = _DiscountAmount
        End Get
        Set(ByVal value As Integer)
            _DiscountAmount = value
        End Set
    End Property

    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
    Public Property InterestMonth() As String
        Get
            InterestMonth = _InterestMonth
        End Get
        Set(ByVal value As String)
            _InterestMonth = value
        End Set
    End Property

#End Region
End Class
