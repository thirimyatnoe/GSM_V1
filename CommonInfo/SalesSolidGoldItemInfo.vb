Public Class SalesSolidGoldItemInfo
#Region "Private Property"
    Private _SaleSolidGoldItemID As String
    Private _SaleSolidGoldID As String
    Private _GoldQualityID As String
    Private _SalesRate As Integer
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _Amount As Long
#End Region

#Region "Properties "
    Public Property SaleSolidGoldItemID() As String
        Get
            SaleSolidGoldItemID = _SaleSolidGoldItemID
        End Get
        Set(ByVal value As String)
            _SaleSolidGoldItemID = value
        End Set
    End Property
    Public Property SaleSolidGoldID() As String
        Get
            SaleSolidGoldID = _SaleSolidGoldID
        End Get
        Set(ByVal value As String)
            _SaleSolidGoldID = value
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
    Public Property SalesRate() As Integer
        Get
            SalesRate = _SalesRate
        End Get
        Set(ByVal value As Integer)
            _SalesRate = value
        End Set
    End Property
    Public Property GoldTK() As Decimal
        Get
            GoldTK = _GoldTK
        End Get
        Set(ByVal value As Decimal)
            _GoldTK = value
        End Set
    End Property
    Public Property GoldTG() As Decimal
        Get
            GoldTG = _GoldTG
        End Get
        Set(ByVal value As Decimal)
            _GoldTG = value
        End Set
    End Property
    Public Property Amount() As Integer
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
        End Set
    End Property
#End Region
End Class
