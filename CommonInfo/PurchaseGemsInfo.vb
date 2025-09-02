Public Class PurchaseGemsInfo
#Region "Private Property"
    Private _PurchaseGemsID As String
    Private _PDate As Date
    Private _StaffID As String
    Private _Customer As String
    Private _Address As String
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _PaidAmount As Integer
    Private _Remark As String
#End Region

#Region "Properties "
    Public Property PurchaseGemsID() As String
        Get
            PurchaseGemsID = _PurchaseGemsID
        End Get
        Set(ByVal value As String)
            _PurchaseGemsID = value
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
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
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
#End Region
End Class
