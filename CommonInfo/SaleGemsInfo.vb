Public Class SaleGemsInfo
#Region "Private Property"
    Private _SaleGemsID As String
    Private _SDate As Date
    Private _StaffID As String
    Private _CustomerID As String
    Private _Customer As String
    Private _Address As String
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _PaidAmount As Integer
    Private _Remark As String
    Private _PromotionDiscount As Integer
    Private _PromotionAmount As Integer
    Private _DiscountAmount As Integer
    Private _GemsTG As Decimal
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _NetAmount As Integer
    Private _PurchaseHeaderID As String
    Private _PurchaseAmount As Integer
    Private _IsOtherCash As Boolean
    Private _OtherCashAmount As Integer

#End Region

#Region "Properties "
    Public Property SaleGemsID() As String
        Get
            SaleGemsID = _SaleGemsID
        End Get
        Set(ByVal value As String)
            _SaleGemsID = value
        End Set
    End Property
    Public Property SDate() As Date
        Get
            SDate = _SDate
        End Get
        Set(ByVal value As Date)
            _SDate = value
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
    Public Property Customer() As String
        Get
            Customer = _Customer
        End Get
        Set(ByVal value As String)
            _Customer = value
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
    Public Property PaidAmount() As Integer
        Get
            PaidAmount = _PaidAmount
        End Get
        Set(ByVal value As Integer)
            _PaidAmount = value
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

    Public Property PromotionAmount() As Integer
        Get
            PromotionAmount = _PromotionAmount
        End Get
        Set(ByVal value As Integer)
            _PromotionAmount = value
        End Set
    End Property
    Public Property PromotionDiscount() As Integer
        Get
            PromotionDiscount = _PromotionDiscount
        End Get
        Set(ByVal value As Integer)
            _PromotionDiscount = value
        End Set
    End Property
    Public Property GemsTG() As Decimal
        Get
            GemsTG = _GemsTG
        End Get
        Set(ByVal value As Decimal)
            _GemsTG = value
        End Set
    End Property
    Public Property GemsK() As Integer
        Get
            GemsK = _GemsK
        End Get
        Set(ByVal value As Integer)
            _GemsK = value
        End Set
    End Property
    Public Property GemsP() As Integer
        Get
            GemsP = _GemsP
        End Get
        Set(ByVal value As Integer)
            _GemsP = value
        End Set
    End Property
    Public Property GemsY() As Integer
        Get
            GemsY = _GemsY
        End Get
        Set(ByVal value As Integer)
            _GemsY = value
        End Set
    End Property
    Public Property GemsC() As Decimal
        Get
            GemsC = _GemsC
        End Get
        Set(ByVal value As Decimal)
            _GemsC = value
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

    Public Property NetAmount() As Integer
        Get
            NetAmount = _NetAmount
        End Get
        Set(ByVal value As Integer)
            _NetAmount = value
        End Set
    End Property

    Public Property PurchaseHeaderID() As String
        Get
            PurchaseHeaderID = _PurchaseHeaderID
        End Get
        Set(ByVal value As String)
            _PurchaseHeaderID = value
        End Set
    End Property
    Public Property PurchaseAmount() As Integer
        Get
            PurchaseAmount = _PurchaseAmount
        End Get
        Set(ByVal value As Integer)
            _PurchaseAmount = value
        End Set
    End Property

    Public Property IsOtherCash() As Boolean
        Get
            IsOtherCash = _IsOtherCash
        End Get
        Set(ByVal value As Boolean)
            _IsOtherCash = value
        End Set
    End Property
    Public Property OtherCashAmount() As Integer
        Get
            OtherCashAmount = _OtherCashAmount
        End Get
        Set(ByVal value As Integer)
            _OtherCashAmount = value
        End Set
    End Property

#End Region
End Class
