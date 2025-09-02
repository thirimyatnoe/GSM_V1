Public Class MortgageInvoiceInfo
#Region "Private Property"
    Private _MortgageInvoiceID As String
    Private _ReceiveDate As Date
    Private _MortgageStaff As String
    Private _InterestRate As Decimal
    Private _CustomerID As String
    Private _TotalAmount As Integer
    Private _TotalQTY As Integer
    Private _Remark As String
    Private _IsReturn As Boolean
    Private _IsDisable As Boolean
    Private _DisableDate As Date
    Private _ReturnDate As Date
    Private _InterestAmount As Integer
    Private _NetAmount As Integer
    Private _AddOrSub As Integer
    Private _PaidAmount As Integer
    Private _RRemark As String
    Private _LocationID As String
    Private _MortgageInvoiceCode As String
    Private _InterestPeriod As Integer
    Private _PaybackAmt As Integer
    Private _PaybackInterestAmt As Integer
    Private _IsPayback As Boolean
#End Region

#Region "Properties "
    Public Property MortgageInvoiceID() As String
        Get
            MortgageInvoiceID = _MortgageInvoiceID
        End Get
        Set(ByVal value As String)
            _MortgageInvoiceID = value
        End Set
    End Property
    Public Property ReceiveDate() As Date
        Get
            ReceiveDate = _ReceiveDate
        End Get
        Set(ByVal value As Date)
            _ReceiveDate = value
        End Set
    End Property
    Public Property MortgageStaff() As String
        Get
            MortgageStaff = _MortgageStaff
        End Get
        Set(ByVal value As String)
            _MortgageStaff = value
        End Set
    End Property
    Public Property InterestRate() As Decimal
        Get
            InterestRate = _InterestRate
        End Get
        Set(ByVal value As Decimal)
            _InterestRate = value
        End Set
    End Property
    Public Property CustomerID() As String
        Get
            CustomerID = _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
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
    Public Property TotalQTY() As Integer
        Get
            TotalQTY = _TotalQTY
        End Get
        Set(ByVal value As Integer)
            _TotalQTY = value
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
    Public Property IsReturn() As Boolean
        Get
            IsReturn = _IsReturn
        End Get
        Set(ByVal value As Boolean)
            _IsReturn = value
        End Set
    End Property
    Public Property IsDisable() As Boolean
        Get
            IsDisable = _IsDisable
        End Get
        Set(ByVal value As Boolean)
            _IsDisable = value
        End Set
    End Property
    Public Property DisableDate() As Date
        Get
            DisableDate = _DisableDate
        End Get
        Set(ByVal value As Date)
            _DisableDate = value
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
    Public Property InterestAmount() As Integer
        Get
            InterestAmount = _InterestAmount
        End Get
        Set(ByVal value As Integer)
            _InterestAmount = value
        End Set
    End Property
    Public Property NetAmount() As Integer
        Get
            NetAmount = _NetAmount
        End Get
        Set(ByVal value As Integer)
            _NetAmount = value
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
    Public Property PaidAmount() As Integer
        Get
            PaidAmount = _PaidAmount
        End Get
        Set(ByVal value As Integer)
            _PaidAmount = value
        End Set
    End Property
    Public Property RRemark() As String
        Get
            RRemark = _RRemark
        End Get
        Set(ByVal value As String)
            _RRemark = value
        End Set
    End Property
    Public Property LocationID() As String
        Get
            LocationID = _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property
    Public Property MortgageInvoiceCode() As String
        Get
            MortgageInvoiceCode = _MortgageInvoiceCode
        End Get
        Set(ByVal value As String)
            _MortgageInvoiceCode = value
        End Set
    End Property

    Public Property InterestPeriod() As Integer
        Get
            InterestPeriod = _InterestPeriod
        End Get
        Set(ByVal value As Integer)
            _InterestPeriod = value
        End Set
    End Property
    Public Property PaybackAmt() As Integer
        Get
            PaybackAmt = _PaybackAmt
        End Get
        Set(ByVal value As Integer)
            _PaybackAmt = value
        End Set
    End Property
    Public Property PaybackInterestAmt() As Integer
        Get
            PaybackInterestAmt = _PaybackInterestAmt
        End Get
        Set(ByVal value As Integer)
            _PaybackInterestAmt = value
        End Set
    End Property
    Public Property IsPayback() As Boolean
        Get
            IsPayback = _IsPayback
        End Get
        Set(ByVal value As Boolean)
            _IsPayback = value
        End Set
    End Property
#End Region
End Class
