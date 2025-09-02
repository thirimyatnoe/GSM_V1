Public Class ReturnAdvanceInfo
#Region "Private Property"
    Private _ReturnAdvanceID As String
    Private _ReturnAdvanceDate As Date
    Private _StaffID As String
    Private _CustomerID As String
    Private _Customer As String
    Private _Address As String
    Private _TotalAmount As Integer
    Private _Remark As String
    Private _Discount As Integer
    Private _NetAmount As Integer
    Private _TotalTG As Decimal

#End Region

#Region "Properties "
    Public Property ReturnAdvanceID() As String
        Get
            ReturnAdvanceID = _ReturnAdvanceID
        End Get
        Set(ByVal value As String)
            _ReturnAdvanceID = value
        End Set
    End Property
    Public Property ReturnAdvanceDate() As Date
        Get
            ReturnAdvanceDate = _ReturnAdvanceDate
        End Get
        Set(ByVal value As Date)
            _ReturnAdvanceDate = value
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
    Public Property Discount() As Integer
        Get
            Discount = _Discount
        End Get
        Set(ByVal value As Integer)
            _Discount = value
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
    Public Property TotalTG() As Decimal
        Get
            TotalTG = _TotalTG
        End Get
        Set(ByVal value As Decimal)
            _TotalTG = value
        End Set
    End Property

#End Region
End Class
