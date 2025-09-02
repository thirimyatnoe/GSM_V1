Public Class WholeSaleReturnInfo
#Region "Private Property"
    Private _WholesaleReturnID As String
    Private _WReturnDate As Date
    Private _WholesaleInvoiceID As String
    Private _ConsignmentSaleID As String
    Private _StaffID As String
    Private _CustomerID As String
    Private _Remark As String
    Private _SaleAmount As Integer
    Private _SaleReturnAmount As Integer
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _PaidAmount As Integer
    Private _LastModifiedLoginUserName As String
    Private _LastModifiedDate As Date
    Private _LocationID As String
    Private _Discount As Integer

#End Region

#Region "Properties "
    Public Property WholesaleReturnID() As String
        Get
            WholesaleReturnID = _WholesaleReturnID
        End Get
        Set(ByVal value As String)
            _WholesaleReturnID = value
        End Set
    End Property
    Public Property WReturnDate() As Date
        Get
            WReturnDate = _WReturnDate
        End Get
        Set(ByVal value As Date)
            _WReturnDate = value
        End Set
    End Property
    Public Property ConsignmentSaleID() As String
        Get
            ConsignmentSaleID = _ConsignmentSaleID
        End Get
        Set(ByVal value As String)
            _ConsignmentSaleID = value
        End Set
    End Property
    Public Property WholeSaleInvoiceID() As String
        Get
            WholeSaleInvoiceID = _WholesaleInvoiceID
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
    Public Property SaleAmount() As Integer
        Get
            SaleAmount = _SaleAmount
        End Get
        Set(ByVal value As Integer)
            _SaleAmount = value
        End Set
    End Property
    Public Property SaleReturnAmount() As Integer
        Get
            SaleReturnAmount = _SaleReturnAmount
        End Get
        Set(ByVal value As Integer)
            _SaleReturnAmount = value
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
    Public Property Discount() As Integer
        Get
            Discount = _Discount
        End Get
        Set(ByVal value As Integer)
            _Discount = value
        End Set
    End Property
   
#End Region
End Class
