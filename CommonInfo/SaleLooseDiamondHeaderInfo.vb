Public Class SaleLooseDiamondHeaderInfo
#Region "Private Property"
    Private _SaleLooseDiamondID As String
    Private _SaleDate As Date
    Private _StaffID As String
    Private _CustomerID As String
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _DiscountAmount As Integer
    Private _PaidAmount As Integer
    Private _Remark As String
    Private _PromotionDiscount As Integer
    Private _PromotionAmount As Integer
    Private _LastModifiedLoginUserName As String
    Private _LastModifiedDate As Date
    Private _LocationID As String
    Private _PurchaseHeaderID As String
    Private _PurchaseAmount As Integer
    Private _IsOtherCash As Boolean
    Private _OtherCashAmount As Integer
    Private _AllTaxAmt As Integer
    Private _SRTaxPer As Decimal
    Private _SRTaxAmt As Integer

    Private _MemberID As String
    Private _MemberName As String
    Private _MemberCode As String
    Private _RedeemID As String
    Private _TopupPoint As Integer
    Private _TopupValue As Integer
    Private _RedeemPoint As Integer
    Private _RedeemValue As Integer
    Private _IsRedeemInvoice As Boolean
    Private _DiscountAmt As Integer
    Private _ProcessFees As Integer
    Private _MemberDis As Decimal
    Private _MemberDiscountAmt As Integer
    Private _InvoiceStatus As Integer
    Private _OppurtunityType As String
    Private _Token As String
    Private _TransactionID As String

#End Region

#Region "Properties "
    Public Property SaleLooseDiamondID() As String
        Get
            SaleLooseDiamondID = _SaleLooseDiamondID
        End Get
        Set(ByVal value As String)
            _SaleLooseDiamondID = value
        End Set
    End Property
    Public Property SaleDate() As Date
        Get
            SaleDate = _SaleDate
        End Get
        Set(ByVal value As Date)
            _SaleDate = value
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

    Public Property DiscountAmount() As Integer
        Get
            DiscountAmount = _DiscountAmount
        End Get
        Set(ByVal value As Integer)
            _DiscountAmount = value
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

    Public Property LastModifiedLoginUserName() As String
        Get
            LastModifiedLoginUserName = _LastModifiedLoginUserName
        End Get
        Set(ByVal value As String)
            _LastModifiedLoginUserName = value
        End Set
    End Property
    Public Property LastModifiedDate() As Date
        Get
            LastModifiedDate = _LastModifiedDate
        End Get
        Set(ByVal value As Date)
            _LastModifiedDate = value
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

    Public Property PromotionDiscount() As Decimal
        Get
            PromotionDiscount = _PromotionDiscount
        End Get
        Set(ByVal value As Decimal)
            _PromotionDiscount = value
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

    Public Property AllTaxAmt() As Integer
        Get
            AllTaxAmt = _AllTaxAmt
        End Get
        Set(ByVal value As Integer)
            _AllTaxAmt = value
        End Set
    End Property

    Public Property SRTaxPer() As Decimal
        Get
            SRTaxPer = _SRTaxPer
        End Get
        Set(ByVal value As Decimal)
            _SRTaxPer = value
        End Set
    End Property


    Public Property SRTaxAmt() As Integer
        Get
            SRTaxAmt = _SRTaxAmt
        End Get
        Set(ByVal value As Integer)
            _SRTaxAmt = value
        End Set
    End Property
    Public Property MemberID() As String
        Get
            MemberID = _MemberID
        End Get
        Set(ByVal value As String)
            _MemberID = value
        End Set
    End Property
    Public Property MemberName() As String
        Get
            MemberName = _MemberName
        End Get
        Set(ByVal value As String)
            _MemberName = value
        End Set
    End Property
    Public Property MemberCode() As String
        Get
            MemberCode = _MemberCode
        End Get
        Set(ByVal value As String)
            _MemberCode = value
        End Set
    End Property
    Public Property RedeemID() As String
        Get
            RedeemID = _RedeemID
        End Get
        Set(ByVal value As String)
            _RedeemID = value
        End Set
    End Property
    Public Property TopupPoint() As Integer
        Get
            TopupPoint = _TopupPoint
        End Get
        Set(ByVal value As Integer)
            _TopupPoint = value
        End Set
    End Property
    Public Property TopupValue() As Integer
        Get
            TopupValue = _TopupValue
        End Get
        Set(ByVal value As Integer)
            _TopupValue = value
        End Set
    End Property
    Public Property RedeemPoint() As Integer
        Get
            RedeemPoint = _RedeemPoint
        End Get
        Set(ByVal value As Integer)
            _RedeemPoint = value
        End Set
    End Property
    Public Property RedeemValue() As Integer
        Get
            RedeemValue = _RedeemValue
        End Get
        Set(ByVal value As Integer)
            _RedeemValue = value
        End Set
    End Property

    Public Property IsRedeemInvoice() As Boolean
        Get
            IsRedeemInvoice = _IsRedeemInvoice
        End Get
        Set(ByVal value As Boolean)
            _IsRedeemInvoice = value
        End Set
    End Property

    Public Property DiscountAmt() As Integer
        Get
            DiscountAmt = _DiscountAmt
        End Get
        Set(ByVal value As Integer)
            _DiscountAmt = value
        End Set
    End Property
    Public Property ProcessFees() As Integer
        Get
            ProcessFees = _ProcessFees
        End Get
        Set(ByVal value As Integer)
            _ProcessFees = value
        End Set
    End Property
    Public Property MemberDiscountAmt() As Integer
        Get
            MemberDiscountAmt = _MemberDiscountAmt
        End Get
        Set(ByVal value As Integer)
            _MemberDiscountAmt = value
        End Set
    End Property
    Public Property InvoiceStatus() As Integer
        Get
            InvoiceStatus = _InvoiceStatus
        End Get
        Set(ByVal value As Integer)
            _InvoiceStatus = value
        End Set
    End Property

    Public Property OppurtunityType() As String
        Get
            OppurtunityType = _OppurtunityType
        End Get
        Set(ByVal value As String)
            _OppurtunityType = value
        End Set
    End Property
    Public Property MemberDis() As Decimal
        Get
            MemberDis = _MemberDis
        End Get
        Set(ByVal value As Decimal)
            _MemberDis = value
        End Set
    End Property
    Public Property TransactionID() As String
        Get
            TransactionID = _TransactionID
        End Get
        Set(ByVal value As String)
            _TransactionID = value
        End Set
    End Property
    Public Property Token() As String
        Get
            Token = _Token
        End Get
        Set(ByVal value As String)
            _Token = value
        End Set
    End Property
#End Region
End Class


