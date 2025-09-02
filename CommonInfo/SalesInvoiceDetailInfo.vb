Public Class SalesInvoiceDetailInfo
#Region "Private Property"
    Private _SaleInvoiceDetailID As String
    Private _SaleInvoiceHeaderID As String
    Private _SaleInvoiceID As String
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _ItemName As String
    Private _Length As String
    Private _Width As String
    Private _ItemCategoryID As String
    Private _GoldQualityID As String
    Private _SalesRate As Integer
    Private _ItemK As Integer
    Private _ItemP As Integer
    Private _ItemY As Decimal

    Private _ItemC As Decimal
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Integer
    Private _GoldC As Decimal
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
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
    Private _GemsPrice As Integer
    Private _DesignCharges As Integer
    Private _PlatingCharges As Integer
    Private _MountingCharges As Integer
    Private _WhiteCharges As Integer
    Private _IsSalesReturn As Integer
    Private _LocationID As String
    Private _DoneRate As Integer
    Private _IsFixPrice As Boolean
    Private _ItemAmount As Integer
    Private _AddOrSub As Integer
    Private _TotalPayment As Integer
    Private _DiscountAmount As Integer
    Private _PaidAmount As Integer
    Private _Remark As String
    Private _SDate As Date
    Private _CustomerID As String
    Private _StaffID As String
    Private _TotalAmount As Long
    Private _QTY As Integer
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
    Private _ItemNameID As String
    Private _ItemTaxPer As Decimal
    Private _ItemTax As Integer
    Private _IsSaleReturn As Boolean
    Private _DesignChargesRate As Integer
    Private _SellingRate As Integer
    Private _SellingAmt As Integer
#End Region

#Region "Properties "
    Public Property SaleInvoiceDetailID() As String
        Get
            SaleInvoiceDetailID = _SaleInvoiceDetailID
        End Get
        Set(ByVal value As String)
            _SaleInvoiceDetailID = value
        End Set
    End Property
    Public Property SaleInvoiceHeaderID() As String
        Get
            SaleInvoiceHeaderID = _SaleInvoiceHeaderID
        End Get
        Set(ByVal value As String)
            _SaleInvoiceHeaderID = value
        End Set
    End Property

    Public Property SaleInvoiceID() As String
        Get
            SaleInvoiceID = _SaleInvoiceID
        End Get
        Set(ByVal value As String)
            _SaleInvoiceID = value
        End Set
    End Property
    Public Property SDate() As Date
        Get
            SDate = _SDate
        End Get
        Set(ByVal value As Date)
            _SDate = value
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
    Public Property ItemName() As String
        Get
            ItemName = _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
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
    Public Property GemsY() As Integer
        Get
            GemsY = _GemsY
        End Get
        Set(ByVal value As Integer)
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
    Public Property GemsTK() As Decimal
        Get
            GemsTK = _GemsTK
        End Get
        Set(ByVal value As Decimal)
            _GemsTK = value
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
    Public Property TotalPayment() As Integer
        Get
            TotalPayment = _TotalPayment
        End Get
        Set(ByVal value As Integer)
            _TotalPayment = value
        End Set
    End Property

    Public Property DiscountAmount() As Integer
        Get
            DiscountAmount = _DiscountAmount
        End Get
        Set(ByVal value As Integer)
            _DiscountAmount = value
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
    Public Property DesignCharges() As Integer
        Get
            DesignCharges = _DesignCharges
        End Get
        Set(ByVal value As Integer)
            _DesignCharges = value
        End Set
    End Property
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
    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
    Public Property IsSalesReturn() As Integer
        Get
            IsSalesReturn = _IsSalesReturn
        End Get
        Set(ByVal value As Integer)
            _IsSalesReturn = value
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
    Public Property DoneRate() As Integer
        Get
            DoneRate = _DoneRate
        End Get
        Set(ByVal value As Integer)
            _DoneRate = value
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

    Public Property ItemAmount() As Integer
        Get
            ItemAmount = _ItemAmount
        End Get
        Set(ByVal value As Integer)
            _ItemAmount = value
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
    Public Property TotalAmount() As Long
        Get
            TotalAmount = _TotalAmount
        End Get
        Set(ByVal value As Long)
            _TotalAmount = value
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

    Public Property ItemNameID() As String
        Get
            ItemNameID = _ItemNameID
        End Get
        Set(ByVal value As String)
            _ItemNameID = value
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
    Public Property IsSaleReturn() As Boolean
        Get
            IsSaleReturn = _IsSaleReturn
        End Get
        Set(ByVal value As Boolean)
            _IsSaleReturn = value
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
#End Region
End Class
