Public Class OrderReceiveDetailInfo
#Region "Private Property"
    Private _OrderReceiveDetailID As String
    Private _OrderInvoiceID As String
    Private _ItemCategoryID As String
    Private _ItemNameID As String
    Private _GoldQualityID As String
    Private _GoldSmithID As String
    Private _OrderRate As Integer
    Private _Length As String
    Private _Width As String
    Private _GoldPrice As Integer
    Private _GemPrice As Integer
    Private _DesignCharges As Integer
    Private _ItemName As Integer
    Private _PlatingFee As Integer
    Private _WhiteCharges As Integer
    Private _MountingFee As Integer
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer

    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Integer
    Private _GoldC As Decimal

    Private _WasteTK As Decimal
    Private _WasteTG As Decimal
    Private _WasteK As Integer
    Private _WasteP As Integer
    Private _WasteY As Integer
    Private _WasteC As Decimal

    Private _TotalGemTK As Decimal
    Private _TotalGemTG As Decimal
    Private _TotalGemK As Integer
    Private _TotalGemP As Integer
    Private _TotalGemY As Integer
    Private _TotalGemC As Decimal

    Private _IsBarcodeNo As Boolean
    Private _Design As String
    Private _IsDiamond As Boolean
#End Region

#Region "Properties "
    Public Property OrderReceiveDetailID() As String
        Get
            OrderReceiveDetailID = _OrderReceiveDetailID
        End Get
        Set(ByVal value As String)
            _OrderReceiveDetailID = value
        End Set
    End Property

    Public Property OrderInvoiceID() As String
        Get
            OrderInvoiceID = _OrderInvoiceID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceID = value
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

    Public Property ItemNameID() As String
        Get
            ItemNameID = _ItemNameID
        End Get
        Set(ByVal value As String)
            _ItemNameID = value
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
    Public Property OrderRate() As Integer
        Get
            OrderRate = _OrderRate
        End Get
        Set(ByVal value As Integer)
            _OrderRate = value
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
    Public Property Width() As String
        Get
            Width = _Width
        End Get
        Set(ByVal value As String)
            _Width = value
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
    Public Property GemPrice() As Integer
        Get
            GemPrice = _GemPrice
        End Get
        Set(ByVal value As Integer)
            _GemPrice = value
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
    Public Property WhiteCharges() As Integer
        Get
            WhiteCharges = _WhiteCharges
        End Get
        Set(ByVal value As Integer)
            _WhiteCharges = value
        End Set
    End Property
    Public Property MountingFee() As Integer
        Get
            MountingFee = _MountingFee
        End Get
        Set(ByVal value As Integer)
            _MountingFee = value
        End Set
    End Property

    Public Property PlatingFee() As Integer
        Get
            PlatingFee = _PlatingFee
        End Get
        Set(ByVal value As Integer)
            _PlatingFee = value
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
    Public Property GoldK() As Integer
        Get
            GoldK = _GoldK
        End Get
        Set(ByVal value As Integer)
            _GoldK = value
        End Set
    End Property
    Public Property GoldP() As Integer
        Get
            GoldP = _GoldP
        End Get
        Set(ByVal value As Integer)
            _GoldP = value
        End Set
    End Property
    Public Property GoldY() As Integer
        Get
            GoldY = _GoldY
        End Get
        Set(ByVal value As Integer)
            _GoldY = value
        End Set
    End Property
    Public Property GoldC() As Decimal
        Get
            GoldC = _GoldC
        End Get
        Set(ByVal value As Decimal)
            _GoldC = value
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

    Public Property TotalGemTK() As Decimal
        Get
            TotalGemTK = _TotalGemTK
        End Get
        Set(ByVal value As Decimal)
            _TotalGemTK = value
        End Set
    End Property
    Public Property TotalGemTG() As Decimal
        Get
            TotalGemTG = _TotalGemTG
        End Get
        Set(ByVal value As Decimal)
            _TotalGemTG = value
        End Set
    End Property
    Public Property TotalGemK() As Integer
        Get
            TotalGemK = _TotalGemK
        End Get
        Set(ByVal value As Integer)
            _TotalGemK = value
        End Set
    End Property
    Public Property TotalGemP() As Integer
        Get
            TotalGemP = _TotalGemP
        End Get
        Set(ByVal value As Integer)
            _TotalGemP = value
        End Set
    End Property
    Public Property TotalGemY() As Integer
        Get
            TotalGemY = _TotalGemY
        End Get
        Set(ByVal value As Integer)
            _TotalGemY = value
        End Set
    End Property
    Public Property TotalGemC() As Decimal
        Get
            TotalGemC = _TotalGemC
        End Get
        Set(ByVal value As Decimal)
            _TotalGemC = value
        End Set
    End Property
    Public Property IsBarcodeNo() As Boolean
        Get
            IsBarcodeNo = _IsBarcodeNo
        End Get
        Set(ByVal value As Boolean)
            _IsBarcodeNo = value
        End Set
    End Property
    Public Property Design() As String
        Get
            Design = _Design
        End Get
        Set(ByVal value As String)
            _Design = value
        End Set
    End Property

    Public Property IsDiamond() As Boolean
        Get
            IsDiamond = _IsDiamond
        End Get
        Set(ByVal value As Boolean)
            _IsDiamond = value
        End Set
    End Property
    Public Property GoldSmithID() As String
        Get
            GoldSmithID = _GoldSmithID
        End Get
        Set(ByVal value As String)
            _GoldSmithID = value
        End Set
    End Property

#End Region
End Class
