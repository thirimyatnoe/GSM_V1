Public Class ConsignmentSaleInfo
#Region "Private Property"
    Private _ConsignmentSaleID As String
    Private _ConsignDate As Date
    Private _WholesaleInvoiceID As String
    Private _ItemNameID As String
    Private _GoldQualityID As String
    Private _StaffID As String
    Private _CustomerID As String
    Private _Remark As String
    Private _NetAmount As Integer
    Private _PaidAmount As Integer

    Private _AddOrSub As Integer
    Private _Discount As Integer
    Private _LastModifiedLoginUserName As String
    Private _LastModifiedDate As Date
    Private _LocationID As String
    Private _PurchaseHeaderID As String
    Private _PurchaseAmount As Integer
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
    Public Property ConsignmentSaleID() As String
        Get
            ConsignmentSaleID = _ConsignmentSaleID
        End Get
        Set(ByVal value As String)
            _ConsignmentSaleID = value
        End Set
    End Property
    Public Property ConsignDate() As Date
        Get
            ConsignDate = _ConsignDate
        End Get
        Set(ByVal value As Date)
            _ConsignDate = value
        End Set
    End Property
    Public Property WholesaleInvoiceID() As String
        Get
            WholesaleInvoiceID = _WholesaleInvoiceID
        End Get
        Set(ByVal value As String)
            _WholesaleInvoiceID = value
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
    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
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
    Public Property PaidAmount() As Integer
        Get
            PaidAmount = _PaidAmount
        End Get
        Set(ByVal value As Integer)
            _PaidAmount = value
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
    Public Property Discount() As Integer
        Get
            Discount = _Discount
        End Get
        Set(ByVal value As Integer)
            _Discount = value
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
