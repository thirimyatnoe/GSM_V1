Public Class DamageInfo
#Region "Private Property"
    Private _DamageID As String
    Private _DDate As Date
    Private _CustomerID As String
    Private _Remark As String
    Private _LocationID As String
    Private _SaleStaffID As String
    Private _ForSaleID As String
#End Region
#Region "Properties "
    Public Property DamageID() As String
        Get
            DamageID = _DamageID
        End Get
        Set(ByVal value As String)
            _DamageID = value
        End Set
    End Property
    Public Property ForSaleID() As String
        Get
            ForSaleID = _ForSaleID
        End Get
        Set(ByVal value As String)
            _ForSaleID = value
        End Set
    End Property
    Public Property DDate() As Date
        Get
            DDate = _DDate
        End Get
        Set(ByVal value As Date)
            _DDate = value
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

    Public Property LocationID() As String
        Get
            LocationID = _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
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
    Public Property SaleStaffID() As String
        Get
            SaleStaffID = _SaleStaffID
        End Get
        Set(ByVal value As String)
            _SaleStaffID = value
        End Set
    End Property
#End Region
End Class
