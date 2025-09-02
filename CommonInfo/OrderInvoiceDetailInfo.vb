Public Class OrderInvoiceDetailInfo
#Region "Private Property"
    Private _OrderInvoiceDetailID As String
    Private _OrderReturnHeaderID As String
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _SalesRate As Integer
    Private _GoldPrice As Integer
    Private _GemsPrice As Integer
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _QTY As Integer
    Private _ItemAmount As Integer

    Private _ItemTG As Decimal
    Private _ItemTK As Decimal
    Private _ItemK As Integer
    Private _Itemp As Integer
    Private _ItemY As Decimal
    Private _ItemC As Decimal

    Private _WasteK As Integer
    Private _WasteP As Integer
    Private _WasteY As Decimal
    Private _WasteC As Decimal
    Private _WasteTK As Decimal
    Private _WasteTG As Decimal

    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Decimal
    Private _GemsC As Decimal

    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Decimal
    Private _GoldC As Decimal

    Private _TotalTK As Decimal
    Private _TotalTG As Decimal
    Private _TotalK As Integer
    Private _TotalP As Integer
    Private _TotalY As Integer
    Private _TotalC As Decimal

    Private _IsOriginalFixedPrice As Boolean
    Private _OriginalFixedPrice As Integer
    Private _IsOriginalPriceGram As Boolean
    Private _OriginalPriceGram As Integer
    Private _OriginalPriceTK As Integer
    Private _OriginalGemsPrice As Integer
    Private _OriginalOtherPrice As Integer
    Private _PurchaseWasteTK As Decimal
    Private _PurchaseWasteTG As Decimal
    Private _IsReturn As Boolean
    Private _ItemTaxPer As Decimal
    Private _ItemTax As Integer


#End Region

#Region "Properties "
    Public Property OrderInvoiceDetailID() As String
        Get
            OrderInvoiceDetailID = _OrderInvoiceDetailID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceDetailID = value
        End Set
    End Property
    Public Property OrderReturnHeaderID() As String
        Get
            OrderReturnHeaderID = _OrderReturnHeaderID
        End Get
        Set(ByVal value As String)
            _OrderReturnHeaderID = value
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
    Public Property QTY() As Integer
        Get
            QTY = _QTY
        End Get
        Set(ByVal value As Integer)
            _QTY = value
        End Set
    End Property
    Public Property ItemAmount() As Integer
        Get
            ItemAmount = _ItemAmount
        End Get
        Set(ByVal value As Integer)
            _ItemAmount = value
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

    Public Property SalesRate() As Integer
        Get
            SalesRate = _SalesRate
        End Get
        Set(ByVal value As Integer)
            _SalesRate = value
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
    Public Property GemsPrice() As Integer
        Get
            GemsPrice = _GemsPrice
        End Get
        Set(ByVal value As Integer)
            _GemsPrice = value
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

    Public Property ItemTG() As Decimal
        Get
            ItemTG = _ItemTG
        End Get
        Set(ByVal value As Decimal)
            _ItemTG = value
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

    Public Property ItemK() As Decimal
        Get
            ItemK = _ItemK
        End Get
        Set(ByVal value As Decimal)
            _ItemK = value
        End Set
    End Property
    Public Property ItemP() As Decimal
        Get
            ItemP = _Itemp
        End Get
        Set(ByVal value As Decimal)
            _Itemp = value
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
    Public Property WasteY() As Decimal
        Get
            WasteY = _WasteY
        End Get
        Set(ByVal value As Decimal)
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

    Public Property GemsTG() As Decimal
        Get
            GemsTG = _GemsTG
        End Get
        Set(ByVal value As Decimal)
            _GemsTG = value
        End Set
    End Property
    Public Property GemsTK() As Decimal
        Get
            GemsTK = _GemsTK
        End Get
        Set(ByVal value As Decimal)
            _GemsTK = value
        End Set
    End Property
    Public Property GemsK() As Integer
        Get
            GemsK = _GemsK
        End Get
        Set(ByVal value As Integer)
            _GemsK = value
        End Set
    End Property
    Public Property GemsP() As Integer
        Get
            GemsP = _GemsP
        End Get
        Set(ByVal value As Integer)
            _GemsP = value
        End Set
    End Property
    Public Property GemsY() As Decimal
        Get
            GemsY = _GemsY
        End Get
        Set(ByVal value As Decimal)
            _GemsY = value
        End Set
    End Property
    Public Property GemsC() As Decimal
        Get
            GemsC = _GemsC
        End Get
        Set(ByVal value As Decimal)
            _GemsC = value
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
    Public Property GoldTK() As Decimal
        Get
            GoldTK = _GoldTK
        End Get
        Set(ByVal value As Decimal)
            _GoldTK = value
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
    Public Property GoldY() As Decimal
        Get
            GoldY = _GoldY
        End Get
        Set(ByVal value As Decimal)
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

    Public Property TotalTG() As Decimal
        Get
            TotalTG = _TotalTG
        End Get
        Set(ByVal value As Decimal)
            _TotalTG = value
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
    Public Property TotalY() As Decimal
        Get
            TotalY = _TotalY
        End Get
        Set(ByVal value As Decimal)
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
    Public Property IsOriginalFixedPrice() As Boolean
        Get
            IsOriginalFixedPrice = _IsOriginalFixedPrice
        End Get
        Set(ByVal value As Boolean)
            _IsOriginalFixedPrice = value
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
    Public Property IsOriginalPriceGram() As Boolean
        Get
            IsOriginalPriceGram = _IsOriginalPriceGram
        End Get
        Set(ByVal value As Boolean)
            _IsOriginalPriceGram = value
        End Set
    End Property
    Public Property OriginalPriceGram() As Integer
        Get
            OriginalPriceGram = _OriginalPriceGram
        End Get
        Set(ByVal value As Integer)
            _OriginalPriceGram = value
        End Set
    End Property
    Public Property OriginalPriceTK() As Integer
        Get
            OriginalPriceTK = _OriginalPriceTK
        End Get
        Set(ByVal value As Integer)
            _OriginalPriceTK = value
        End Set
    End Property
    Public Property OriginalGemsPrice() As Integer
        Get
            OriginalGemsPrice = _OriginalGemsPrice
        End Get
        Set(ByVal value As Integer)
            _OriginalGemsPrice = value
        End Set
    End Property
    Public Property OriginalOtherPrice() As Integer
        Get
            OriginalOtherPrice = _OriginalOtherPrice
        End Get
        Set(ByVal value As Integer)
            _OriginalOtherPrice = value
        End Set
    End Property

    Public Property PurchaseWasteTG() As Decimal
        Get
            PurchaseWasteTG = _PurchaseWasteTG
        End Get
        Set(ByVal value As Decimal)
            _PurchaseWasteTG = value
        End Set
    End Property
    Public Property PurchaseWasteTK() As Decimal
        Get
            PurchaseWasteTK = _PurchaseWasteTK
        End Get
        Set(ByVal value As Decimal)
            _PurchaseWasteTK = value
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
    Public Property ItemTaxPer() As Decimal
        Get
            ItemTaxPer = _ItemTaxPer
        End Get
        Set(ByVal value As Decimal)
            _ItemTaxPer = value
        End Set
    End Property
    Public Property ItemTax() As Integer
        Get
            ItemTax = _ItemTax
        End Get
        Set(ByVal value As Integer)
            _ItemTax = value
        End Set
    End Property

#End Region
End Class
