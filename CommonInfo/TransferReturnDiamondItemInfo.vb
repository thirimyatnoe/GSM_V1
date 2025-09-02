Public Class TransferReturnDiamondItemInfo
#Region "Private Property"
    Private _TransferReturnItemID As String
    Private _TransferReturnID As String
    Private _ForSaleID As String
    Private _OriginalFixedPrice As Integer
    Private _OriginalPriceCarat As Integer
    Private _PriceCode As String
    Private _FixPrice As Integer

#End Region

#Region "Properties "
    Public Property TransferReturnItemID() As String
        Get
            TransferReturnItemID = _TransferReturnItemID
        End Get
        Set(ByVal value As String)
            _TransferReturnItemID = value
        End Set
    End Property
    Public Property TransferReturnID() As String
        Get
            TransferReturnID = _TransferReturnID
        End Get
        Set(ByVal value As String)
            _TransferReturnID = value
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
    Public Property OriginalFixedPrice() As Integer
        Get
            OriginalFixedPrice = _OriginalFixedPrice
        End Get
        Set(ByVal value As Integer)
            _OriginalFixedPrice = value
        End Set
    End Property
    Public Property OriginalPriceCarat() As Integer
        Get
            OriginalPriceCarat = _OriginalPriceCarat
        End Get
        Set(ByVal value As Integer)
            _OriginalPriceCarat = value
        End Set
    End Property

    Public Property PriceCode() As String
        Get
            PriceCode = _PriceCode
        End Get
        Set(ByVal value As String)
            _PriceCode = value
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


#End Region
End Class
