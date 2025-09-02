Public Class SalesVolumeDetailInfo
#Region "Private Property"
    Private _SalesVolumeDetailID As String
    Private _SalesVolumeID As String
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _ItemNameID As String
    Private _Length As String
    Private _Width As String
    Private _ItemCategoryID As String
    Private _GoldQualityID As String
    Private _SalesRate As Integer
    Private _ItemK As Integer
    Private _ItemP As Integer
    Private _ItemY As Integer
    Private _ItemC As Decimal
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal
    Private _WasteK As Integer
    Private _WasteP As Integer
    Private _WasteY As Integer
    Private _WasteC As Decimal
    Private _WasteTK As Decimal
    Private _WasteTG As Decimal
    Private _TotalK As Integer
    Private _TotalP As Integer
    Private _TotalY As Integer
    Private _TotalC As Decimal
    Private _TotalTK As Decimal
    Private _TotalTG As Decimal
    Private _GoldPrice As Integer
    Private _LocationID As String
    Private _FixPrice As Integer
    Private _IsFixPrice As Boolean
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _QTY As Integer
    Private _DesignCharges As Integer
    Private _DesignChargesRate As Integer

#End Region

#Region "Properties "
    Public Property SalesVolumeDetailID() As String
        Get
            SalesVolumeDetailID = _SalesVolumeDetailID
        End Get
        Set(ByVal value As String)
            _SalesVolumeDetailID = value
        End Set
    End Property

    Public Property SalesVolumeID() As String
        Get
            SalesVolumeID = _SalesVolumeID
        End Get
        Set(ByVal value As String)
            _SalesVolumeID = value
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
    Public Property ItemNameID() As String
        Get
            ItemNameID = _ItemNameID
        End Get
        Set(ByVal value As String)
            _ItemNameID = value
        End Set
    End Property
    Public Property Length() As String
        Get
            Length = _Length
        End Get
        Set(ByVal value As String)
            _Length = value
        End Set
    End Property
   
    Public Property ItemCategoryID() As String
        Get
            ItemCategoryID = _ItemCategoryID
        End Get
        Set(ByVal value As String)
            _ItemCategoryID = value
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
   
    Public Property WasteK() As Integer
        Get
            WasteK = _WasteK
        End Get
        Set(ByVal value As Integer)
            _WasteK = value
        End Set
    End Property
    Public Property WasteP() As Integer
        Get
            WasteP = _WasteP
        End Get
        Set(ByVal value As Integer)
            _WasteP = value
        End Set
    End Property
    Public Property WasteY() As Integer
        Get
            WasteY = _WasteY
        End Get
        Set(ByVal value As Integer)
            _WasteY = value
        End Set
    End Property
    Public Property WasteC() As Decimal
        Get
            WasteC = _WasteC
        End Get
        Set(ByVal value As Decimal)
            _WasteC = value
        End Set
    End Property
    Public Property WasteTK() As Decimal
        Get
            WasteTK = _WasteTK
        End Get
        Set(ByVal value As Decimal)
            _WasteTK = value
        End Set
    End Property
    Public Property WasteTG() As Decimal
        Get
            WasteTG = _WasteTG
        End Get
        Set(ByVal value As Decimal)
            _WasteTG = value
        End Set
    End Property
    Public Property TotalK() As Integer
        Get
            TotalK = _TotalK
        End Get
        Set(ByVal value As Integer)
            _TotalK = value
        End Set
    End Property
    Public Property TotalP() As Integer
        Get
            TotalP = _TotalP
        End Get
        Set(ByVal value As Integer)
            _TotalP = value
        End Set
    End Property
    Public Property TotalY() As Integer
        Get
            TotalY = _TotalY
        End Get
        Set(ByVal value As Integer)
            _TotalY = value
        End Set
    End Property
    Public Property TotalC() As Decimal
        Get
            TotalC = _TotalC
        End Get
        Set(ByVal value As Decimal)
            _TotalC = value
        End Set
    End Property
    Public Property TotalTK() As Decimal
        Get
            TotalTK = _TotalTK
        End Get
        Set(ByVal value As Decimal)
            _TotalTK = value
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
    Public Property GoldPrice() As Integer
        Get
            GoldPrice = _GoldPrice
        End Get
        Set(ByVal value As Integer)
            _GoldPrice = value
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
    Public Property Width() As String
        Get
            Width = _Width
        End Get
        Set(ByVal value As String)
            _Width = value
        End Set
    End Property
    Public Property ItemK() As Integer
        Get
            ItemK = _ItemK
        End Get
        Set(ByVal value As Integer)
            _ItemK = value
        End Set
    End Property
    Public Property ItemP() As Integer
        Get
            ItemP = _ItemP
        End Get
        Set(ByVal value As Integer)
            _ItemP = value
        End Set
    End Property
    Public Property ItemY() As Decimal
        Get
            ItemY = _ItemY
        End Get
        Set(ByVal value As Decimal)
            _ItemY = value
        End Set
    End Property
    Public Property ItemC() As Decimal
        Get
            ItemC = _ItemC
        End Get
        Set(ByVal value As Decimal)
            _ItemC = value
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
#End Region
End Class
