Public Class MortgagePaybackInfo
#Region "Private Property"
    Private _MortgagePaybackID As String
    Private _MortgageInvoiceID As String
    Private _FromDate As Date
    Private _ToDate As Date
    Private _PaybackAmount As Integer
    Private _PaidAmount As Integer
    Private _PaybackDate As Date
    Private _DiscountAmount As Integer
    Private _Remark As String
    Private _InterestAmt As Integer
    Private _TotalAmount As Integer
    Private _IsDelete As Boolean

#End Region

#Region "Properties "
    Public Property MortgagePaybackID() As String
        Get
            MortgagePaybackID = _MortgagePaybackID
        End Get
        Set(ByVal value As String)
            _MortgagePaybackID = value
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
    Public Property PaybackAmount() As Integer
        Get
            PaybackAmount = _PaybackAmount
        End Get
        Set(ByVal value As Integer)
            _PaybackAmount = value
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
    Public Property PaybackDate() As Date
        Get
            PaybackDate = _PaybackDate
        End Get
        Set(ByVal value As Date)
            _PaybackDate = value
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

    Public Property InterestAmt() As Integer
        Get
            InterestAmt = _InterestAmt
        End Get
        Set(ByVal value As Integer)
            _InterestAmt = value
        End Set
    End Property

    Public Property TotalAmount() As Integer
        Get
            TotalAmount = _TotalAmount
        End Get
        Set(ByVal value As Integer)
            _TotalAmount = value
        End Set
    End Property
    Public Property IsDelete() As Boolean
        Get
            IsDelete = _IsDelete
        End Get
        Set(ByVal value As Boolean)
            _IsDelete = value
        End Set
    End Property


#End Region
End Class
