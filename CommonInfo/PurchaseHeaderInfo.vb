Public Class PurchaseHeaderInfo
#Region "Private Property"
    Private _PurchaseHeaderID As String
    Private _PurchaseDate As Date
    Private _StaffID As String
    Private _CustomerID As String
    Private _Address As String
    Private _Remark As String
    Private _IsShop As Boolean
    Private _SaleInvoiceHeaderID As String
    Private _OldSaleAmount As Long
    Private _AllTotalAmount As Integer
    Private _AllAddOrSub As Integer
    Private _AllPaidAmount As Integer
    Private _LocationID As String
    Private _GoldPrice As Integer
    Private _GemsPrice As Integer
    Private _IsGem As Boolean
    Private _IsOut As Boolean
    Private _IsOrder As Boolean
    Private _AllNetAmount As Long
    Private _IsChange As Boolean
    Private _PurchaseFromSupplierID As String
    Private _PDate As Date
    Private _SupplierID As String
    Private _Voucher As String
    Private _ExchangeRate As Integer
    Private _TotalAmount As Decimal
    Private _AddOrSub As Integer
    Private _PaidAmount As Integer
    Private _DiscountRate As Integer
    Private _Expense As Integer
    Private _CommissionRate As Integer
    Private _PayType As Integer
    Private _DueDate As Date
    Private _IsLooseDiamond As Boolean
#End Region

#Region "Properties "
    Public Property PurchaseHeaderID() As String
        Get
            PurchaseHeaderID = _PurchaseHeaderID
        End Get
        Set(ByVal value As String)
            _PurchaseHeaderID = value
        End Set
    End Property
    Public Property PurchaseDate() As Date
        Get
            PurchaseDate = _PurchaseDate
        End Get
        Set(ByVal value As Date)
            _PurchaseDate = value
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
    Public Property CustomerID() As String
        Get
            CustomerID = _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property
    Public Property Address() As String
        Get
            Address = _Address
        End Get
        Set(ByVal value As String)
            _Address = value
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
    Public Property IsShop() As Boolean
        Get
            IsShop = _IsShop
        End Get
        Set(ByVal value As Boolean)
            _IsShop = value
        End Set
    End Property
    Public Property SaleInvoiceHeaderID() As String
        Get
            SaleInvoiceHeaderID = _SaleInvoiceHeaderID
        End Get
        Set(ByVal value As String)
            _SaleInvoiceHeaderID = value
        End Set
    End Property
    Public Property OldSaleAmount() As Long
        Get
            OldSaleAmount = _OldSaleAmount
        End Get
        Set(ByVal value As Long)
            _OldSaleAmount = value
        End Set
    End Property
   
    Public Property AllTotalAmount() As Long
        Get
            AllTotalAmount = _AllTotalAmount
        End Get
        Set(ByVal value As Long)
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
    Public Property AllPaidAmount() As Long
        Get
            AllPaidAmount = _AllPaidAmount
        End Get
        Set(ByVal value As Long)
            _AllPaidAmount = value
        End Set
    End Property
    Public Property GoldPrice() As Long
        Get
            GoldPrice = _GoldPrice
        End Get
        Set(ByVal value As Long)
            _GoldPrice = value
        End Set
    End Property
    Public Property GemsPrice() As Long
        Get
            GemsPrice = _GemsPrice
        End Get
        Set(ByVal value As Long)
            _GemsPrice = value
        End Set
    End Property
  
    Public Property IsGem() As Boolean
        Get
            IsGem = _IsGem
        End Get
        Set(ByVal value As Boolean)
            _IsGem = value
        End Set
    End Property
    Public Property IsLooseDiamond() As Boolean
        Get
            IsLooseDiamond = _IsLooseDiamond
        End Get
        Set(ByVal value As Boolean)
            _IsLooseDiamond = value
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
    Public Property IsOut() As Boolean
        Get
            IsOut = _IsOut
        End Get
        Set(ByVal value As Boolean)
            _IsOut = value
        End Set
    End Property

    Public Property IsOrder() As Boolean
        Get
            IsOrder = _IsOrder
        End Get
        Set(ByVal value As Boolean)
            _IsOrder = value
        End Set
    End Property

    Public Property AllNetAmount() As Long
        Get
            AllNetAmount = _AllNetAmount
        End Get
        Set(ByVal value As Long)
            _AllNetAmount = value
        End Set
    End Property

    Public Property IsChange() As Boolean
        Get
            IsChange = _IsChange
        End Get
        Set(ByVal value As Boolean)
            _IsChange = value
        End Set
    End Property

    Public Property PurchaseFromSupplierID() As String
        Get
            PurchaseFromSupplierID = _PurchaseFromSupplierID
        End Get
        Set(ByVal value As String)
            _PurchaseFromSupplierID = value
        End Set
    End Property

    Public Property PDate() As Date
        Get
            PDate = _PDate
        End Get
        Set(ByVal value As Date)
            _PDate = value
        End Set
    End Property
    Public Property SupplierID() As String
        Get
            SupplierID = _SupplierID
        End Get
        Set(ByVal value As String)
            _SupplierID = value
        End Set
    End Property

    Public Property Voucher() As String
        Get
            Voucher = _Voucher
        End Get
        Set(ByVal value As String)
            _Voucher = value
        End Set
    End Property
    Public Property ExchangeRate() As Integer
        Get
            ExchangeRate = _ExchangeRate
        End Get
        Set(ByVal value As Integer)
            _ExchangeRate = value
        End Set
    End Property

    Public Property TotalAmount() As Decimal
        Get
            TotalAmount = _TotalAmount
        End Get
        Set(ByVal value As Decimal)
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

    Public Property PaidAmount() As Integer
        Get
            PaidAmount = _PaidAmount
        End Get
        Set(ByVal value As Integer)
            _PaidAmount = value
        End Set
    End Property
    Public Property DiscountRate() As Integer
        Get
            DiscountRate = _DiscountRate
        End Get
        Set(ByVal value As Integer)
            _DiscountRate = value
        End Set
    End Property

    Public Property Expense() As Integer
        Get
            Expense = _Expense
        End Get
        Set(ByVal value As Integer)
            _Expense = value
        End Set
    End Property
    Public Property CommissionRate() As Integer
        Get
            CommissionRate = _CommissionRate
        End Get
        Set(ByVal value As Integer)
            _CommissionRate = value
        End Set
    End Property

    Public Property PayType() As Integer
        Get
            PayType = _PayType
        End Get
        Set(ByVal value As Integer)
            _PayType = value
        End Set
    End Property
    Public Property DueDate() As Date
        Get
            DueDate = _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property

#End Region
End Class
