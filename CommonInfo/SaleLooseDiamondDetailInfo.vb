Public Class SaleLooseDiamondDetailInfo
#Region "Private Property"
    Private _SaleLooseDiamondDetailID As String
    Private _SaleLooseDiamondID As String
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _GemsName As String
    Private _GemsCategoryID As String
    Private _Shape As String
    Private _Color As String
    Private _Clarity As String
    Private _SalesRate As Integer
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal
    Private _GemsPrice As Integer
    Private _LocationID As String
    Private _FixPrice As Integer
    Private _IsFixPrice As Boolean
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _QTY As Integer
    Private _DesignCharges As Integer
    Private _DesignChargesRate As Integer
    Private _PlatingCharges As Integer
    Private _MountingCharges As Integer
    Private _WhiteCharges As Integer
    Private _IsSaleReturn As Boolean
    Private _SellingRate As Integer
    Private _SellingAmt As Integer
    Private _IsOriginalFixedPrice As Boolean
    Private _OriginalFixedPrice As Integer
    Private _IsOriginalPriceCarat As Boolean
    Private _OriginalPriceCarat As Integer
    Private _GemsTW As Decimal
    Private _YOrCOrG As String
    Private _IsReturn As Boolean

#End Region

#Region "Properties "
    Public Property SaleLooseDiamondDetailID() As String
        Get
            SaleLooseDiamondDetailID = _SaleLooseDiamondDetailID
        End Get
        Set(ByVal value As String)
            _SaleLooseDiamondDetailID = value
        End Set
    End Property

    Public Property SaleLooseDiamondID() As String
        Get
            SaleLooseDiamondID = _SaleLooseDiamondID
        End Get
        Set(ByVal value As String)
            _SaleLooseDiamondID = value
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
    Public Property ItemCode() As String
        Get
            ItemCode = _ItemCode
        End Get
        Set(ByVal value As String)
            _ItemCode = value
        End Set
    End Property
    Public Property GemsName() As String
        Get
            GemsName = _GemsName
        End Get
        Set(ByVal value As String)
            _GemsName = value
        End Set
    End Property
    Public Property GemsCategoryID() As String
        Get
            GemsCategoryID = _GemsCategoryID
        End Get
        Set(ByVal value As String)
            _GemsCategoryID = value
        End Set
    End Property
    Public Property Shape() As String
        Get
            Shape = _Shape
        End Get
        Set(ByVal value As String)
            _Shape = value
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
    Public Property GemsPrice() As Integer
        Get
            GemsPrice = _GemsPrice
        End Get
        Set(ByVal value As Integer)
            _GemsPrice = value
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
    Public Property FixPrice() As Integer
        Get
            FixPrice = _FixPrice
        End Get
        Set(ByVal value As Integer)
            _FixPrice = value
        End Set
    End Property
    Public Property ItemTK() As Decimal
        Get
            ItemTK = _ItemTK
        End Get
        Set(ByVal value As Decimal)
            _ItemTK = value
        End Set
    End Property
    Public Property ItemTG() As Decimal
        Get
            ItemTG = _ItemTG
        End Get
        Set(ByVal value As Decimal)
            _ItemTG = value
        End Set
    End Property

    Public Property IsFixPrice() As Boolean
        Get
            IsFixPrice = _IsFixPrice
        End Get
        Set(ByVal value As Boolean)
            _IsFixPrice = value
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

    Public Property QTY() As Integer
        Get
            QTY = _QTY
        End Get
        Set(ByVal value As Integer)
            _QTY = value
        End Set
    End Property
    Public Property DesignCharges() As Integer
        Get
            DesignCharges = _DesignCharges
        End Get
        Set(ByVal value As Integer)
            _DesignCharges = value
        End Set
    End Property
    Public Property DesignChargesRate() As Integer
        Get
            DesignChargesRate = _DesignChargesRate
        End Get
        Set(ByVal value As Integer)
            _DesignChargesRate = value
        End Set
    End Property
    'Public Property DesignCharges() As Integer
    '    Get
    '        DesignCharges = _DesignCharges
    '    End Get
    '    Set(ByVal value As Integer)
    '        _DesignCharges = value
    '    End Set
    'End Property
    Public Property PlatingCharges() As Integer
        Get
            PlatingCharges = _PlatingCharges
        End Get
        Set(ByVal value As Integer)
            _PlatingCharges = value
        End Set
    End Property
    Public Property MountingCharges() As Integer
        Get
            MountingCharges = _MountingCharges
        End Get
        Set(ByVal value As Integer)
            _MountingCharges = value
        End Set
    End Property
    Public Property WhiteCharges() As Integer
        Get
            WhiteCharges = _WhiteCharges
        End Get
        Set(ByVal value As Integer)
            _WhiteCharges = value
        End Set
    End Property
    Public Property IsSaleReturn() As Boolean
        Get
            IsSaleReturn = _IsSaleReturn
        End Get
        Set(ByVal value As Boolean)
            _IsSaleReturn = value
        End Set
    End Property
    Public Property IsOriginalFixedPrice() As Boolean
        Get
            IsOriginalFixedPrice = _IsOriginalFixedPrice
        End Get
        Set(ByVal value As Boolean)
            _IsOriginalFixedPrice = value
        End Set
    End Property
    Public Property OriginalFixedPrice() As Boolean
        Get
            OriginalFixedPrice = _OriginalFixedPrice
        End Get
        Set(ByVal value As Boolean)
            _OriginalFixedPrice = value
        End Set
    End Property
    Public Property IsOriginalPriceCarat() As Boolean
        Get
            IsOriginalPriceCarat = _IsOriginalPriceCarat
        End Get
        Set(ByVal value As Boolean)
            _IsOriginalPriceCarat = value
        End Set
    End Property
    Public Property OriginalPriceCarat() As Boolean
        Get
            OriginalPriceCarat = _OriginalPriceCarat
        End Get
        Set(ByVal value As Boolean)
            _OriginalPriceCarat = value
        End Set
    End Property
    Public Property SellingRate() As Integer
        Get
            SellingRate = _SellingRate
        End Get
        Set(ByVal value As Integer)
            _SellingRate = value
        End Set
    End Property
    Public Property SellingAmt() As Integer
        Get
            SellingAmt = _SellingAmt
        End Get
        Set(ByVal value As Integer)
            _SellingAmt = value
        End Set
    End Property
    Public Property Color() As String
        Get
            Color = _Color
        End Get
        Set(ByVal value As String)
            _Color = value
        End Set
    End Property
    Public Property Clarity() As String
        Get
            Clarity = _Clarity
        End Get
        Set(ByVal value As String)
            _Clarity = value
        End Set
    End Property
    Public Property GemsTW() As Decimal
        Get
            GemsTW = _GemsTW
        End Get
        Set(ByVal value As Decimal)
            _GemsTW = value
        End Set
    End Property
    Public Property YOrCOrG() As String
        Get
            YOrCOrG = _YOrCOrG
        End Get
        Set(ByVal value As String)
            _YOrCOrG = value
        End Set
    End Property
    Public Property IsReturn() As Boolean
        Get
            IsReturn = _IsReturn
        End Get
        Set(ByVal value As Boolean)
            _IsReturn = value
        End Set
    End Property
#End Region
End Class
