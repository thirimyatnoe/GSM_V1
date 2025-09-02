Public Class OrderReturnHeader
#Region "Private Property"
    Private _OrderReturnHeaderID As String
    Private _ReturnDate As DateTime
    Private _DueDate As DateTime
    Private _OrderInvoiceID As String
    Private _IsPayGold As Boolean
    Private _AllTotalAmount As Integer
    Private _AllAddOrSub As Integer
    Private _FromGoldAmount As Integer
    Private _StaffID As String
    Private _IsAddGold As Boolean
    Private _Remark As String
    Private _DiscountAmount As Integer
    Private _BalanceAmount As Integer
    Private _PaidAmount As Integer
    Private _AdvanceAmount As Integer
    Private _AllTaxAmt As Integer
    Private _AddGoldTaxPer As Decimal
    Private _AddGoldTax As Integer

#End Region
#Region "Properties "
    Public Property OrderReturnHeaderID() As String
        Get
            OrderReturnHeaderID = _OrderReturnHeaderID
        End Get
        Set(ByVal value As String)
            _OrderReturnHeaderID = value
        End Set
    End Property
    Public Property ReturnDate() As DateTime
        Get
            ReturnDate = _ReturnDate
        End Get
        Set(ByVal value As DateTime)
            _ReturnDate = value
        End Set
    End Property
    Public Property DueDate() As DateTime
        Get
            DueDate = _DueDate
        End Get
        Set(ByVal value As DateTime)
            _DueDate = value
        End Set
    End Property
    Public Property OrderInvoiceID() As String
        Get
            OrderInvoiceID = _OrderInvoiceID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceID = value
        End Set
    End Property
    Public Property IsPayGold() As Boolean
        Get
            IsPayGold = _IsPayGold
        End Get
        Set(ByVal value As Boolean)
            _IsPayGold = value
        End Set
    End Property
    Public Property AllTotalAmount() As Integer
        Get
            AllTotalAmount = _AllTotalAmount
        End Get
        Set(ByVal value As Integer)
            _AllTotalAmount = value
        End Set
    End Property
    Public Property AllAddOrSub() As Integer
        Get
            AllAddOrSub = _AllAddOrSub
        End Get
        Set(ByVal value As Integer)
            _AllAddOrSub = value
        End Set
    End Property
    Public Property FromGoldAmount() As Integer
        Get
            FromGoldAmount = _FromGoldAmount
        End Get
        Set(ByVal value As Integer)
            _FromGoldAmount = value
        End Set
    End Property
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
        End Set
    End Property
    Public Property IsAddGold() As Boolean
        Get
            IsAddGold = _IsAddGold
        End Get
        Set(ByVal value As Boolean)
            _IsAddGold = value
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
    Public Property DiscountAmount() As Integer
        Get
            DiscountAmount = _DiscountAmount
        End Get
        Set(ByVal value As Integer)
            _DiscountAmount = value
        End Set
    End Property
  
    Public Property BalanceAmount() As Integer
        Get
            BalanceAmount = _BalanceAmount
        End Get
        Set(ByVal value As Integer)
            _BalanceAmount = value
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

    Public Property AdvanceAmount() As Integer
        Get
            AdvanceAmount = _AdvanceAmount
        End Get
        Set(ByVal value As Integer)
            _AdvanceAmount = value
        End Set
    End Property
    Public Property AllTaxAmt() As Integer
        Get
            AllTaxAmt = _AllTaxAmt
        End Get
        Set(ByVal value As Integer)
            _AllTaxAmt = value
        End Set
    End Property

    Public Property AddGoldTaxPer() As Decimal
        Get
            AddGoldTaxPer = _AddGoldTaxPer
        End Get
        Set(ByVal value As Decimal)
            _AddGoldTaxPer = value
        End Set
    End Property
    Public Property AddGoldTax() As Integer
        Get
            AddGoldTax = _AddGoldTax
        End Get
        Set(ByVal value As Integer)
            _AddGoldTax = value
        End Set
    End Property
#End Region

End Class
