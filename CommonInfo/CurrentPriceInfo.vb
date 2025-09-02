Public Class CurrentPriceInfo
#Region "Private Property"
    Private _DefineID As String
    Private _DefineDateTime As Date
    Private _GoldQualityID As String
    Private _SalesRate As Long
    Private _PurchaseRate As Long
    Private _ExchangeRate As Long
    Private _Remark As String
    Private _PercentPurchaseRate As Integer
    Private _PercentExchangeRate As Integer
    Private _PercentDamageRate As Integer
    Private _LocationID As String

#End Region
#Region "Properties "
    Public Property DefineID() As String
        Get
            DefineID = _DefineID
        End Get
        Set(ByVal value As String)
            _DefineID = value
        End Set
    End Property
    Public Property DefineDateTime() As Date
        Get
            DefineDateTime = _DefineDateTime
        End Get
        Set(ByVal value As Date)
            _DefineDateTime = value
        End Set
    End Property
    Public Property GoldQualityID() As String
        Get
            GoldQualityID = _GoldQualityID
        End Get
        Set(ByVal value As String)
            _GoldQualityID = value
        End Set
    End Property
    Public Property SalesRate() As Long
        Get
            SalesRate = _SalesRate
        End Get
        Set(ByVal value As Long)
            _SalesRate = value
        End Set
    End Property
    Public Property PurchaseRate() As Long
        Get
            PurchaseRate = _PurchaseRate
        End Get
        Set(ByVal value As Long)
            _PurchaseRate = value
        End Set
    End Property
    Public Property ExchangeRate() As Long
        Get
            ExchangeRate = _ExchangeRate
        End Get
        Set(ByVal value As Long)
            _ExchangeRate = value
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
    Public Property PercentPurchaseRate() As Integer
        Get
            PercentPurchaseRate = _PercentPurchaseRate
        End Get
        Set(ByVal value As Integer)
            _PercentPurchaseRate = value
        End Set
    End Property
    Public Property PercentExchangeRate() As Integer
        Get
            PercentExchangeRate = _PercentExchangeRate
        End Get
        Set(ByVal value As Integer)
            _PercentExchangeRate = value
        End Set
    End Property
    Public Property PercentDamageRate() As Integer
        Get
            PercentDamageRate = _PercentDamageRate
        End Get
        Set(ByVal value As Integer)
            _PercentDamageRate = value
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

#End Region
End Class
