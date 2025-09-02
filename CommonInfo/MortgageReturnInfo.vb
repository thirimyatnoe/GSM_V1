Public Class MortgageReturnInfo
#Region "Private Property"
    Private _MortgageReturnID As String
    Private _MortgageInvoiceID As String
    Private _MortgageCode As String
    Private _ReturnDate As Date
    Private _PaidAmount As Integer
    Private _InterestAmount As Integer
    Private _AddOrSub As Integer
    Private _TotalAmount As Integer
    Private _BranchID As Integer
    Private _IsInterest As Boolean
    Private _FromDate As Date
    Private _ToDate As Date
    Private _Remark As String
    Private _isDelete As Boolean
    Private _ReturnAmount As Integer
   
#End Region
#Region "Properties "
    Public Property MortgageReturnID() As String
        Get
            MortgageReturnID = _MortgageReturnID
        End Get
        Set(ByVal value As String)
            _MortgageReturnID = value
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

    Public Property MortgageCode() As String
        Get
            MortgageCode = _MortgageCode
        End Get
        Set(ByVal value As String)
            _MortgageCode = value
        End Set
    End Property

    Public Property ReturnDate() As Date
        Get
            ReturnDate = _ReturnDate
        End Get
        Set(ByVal value As Date)
            _ReturnDate = value
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

    Public Property TotalAmount() As Integer
        Get
            TotalAmount = _TotalAmount
        End Get
        Set(ByVal value As Integer)
            _TotalAmount = value
        End Set
    End Property

    Public Property AddOrSub() As Integer
        Get
            AddOrSub = _AddOrSub
        End Get
        Set(ByVal value As Integer)
            _AddOrSub = value
        End Set
    End Property

    Public Property BranchID() As Integer
        Get
            BranchID = _BranchID
        End Get
        Set(ByVal value As Integer)
            _BranchID = value
        End Set
    End Property

    Public Property IsInterest As Boolean
        Get
            IsInterest = _IsInterest
        End Get
        Set(ByVal value As Boolean)
            _IsInterest = value
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
    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
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
    Public Property ReturnAmount() As Integer
        Get
            ReturnAmount = _ReturnAmount
        End Get
        Set(ByVal value As Integer)
            _ReturnAmount = value
        End Set
    End Property
    'test

#End Region
End Class
