Public Class PurchaseDetailInfo
#Region "Private Property"
    Private _PurchaseDetailID As String
    Private _PurchaseHeaderID As String
    Private _SaleInvoiceDetailID As String
    Private _ConsignmentSaleItemID As String
    Private _SaleGemsItemID As String
    Private _ForSaleID As String
    Private _BarcodeNo As String
    Private _OldSaleAmount As Integer
    Private _ItemCategoryID As String
    Private _ItemNameID As String
    Private _GoldQualityID As String
    Private _CurrentPrice As Integer
    Private _Length As String
    Private _Width As String
    Private _QTY As Integer
    Private _IsFixPrice As Boolean
    Private _IsDamage As Boolean
    Private _IsChange As Boolean
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _IsClose As Boolean
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Integer
    Private _GoldC As Decimal
    Private _TotalGemTK As Decimal
    Private _TotalGemTG As Decimal
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _TotalTK As Decimal
    Private _TotalTG As Decimal
    Private _TotalK As Integer
    Private _TotalP As Integer
    Private _TotalY As Integer
    Private _TotalC As Decimal
    Private _FixType As String
    Private _GemTW As Decimal
    Private _YOrCOrG As String
    Private _ItemName As String
    Private _GoldPrice As Integer
    Private _GemsPrice As Integer
    Private _WasteTK As Decimal
    Private _WasteTG As Decimal
    Private _PWasteTK As Decimal
    Private _PWasteTG As Decimal
    Private _PurchaseWastePercent As Integer
    Private _SaleRate As Integer
    Private _PurchaseDiscountAmount As Integer
    Private _IsDone As Boolean
    Private _DoneAmount As Integer
    Private _IsSalePercent As Boolean
    Private _SalePercent As Integer
    Private _SalePercentAmount As Integer
    Private _AddSub As Integer
    Private _IsShop As Boolean
    Private _IsOrder As Boolean
    Private _PurchaseFromSupplierItemID As String
    Private _PurchaseFromSupplierID As String
    Private _OriginalCode As String
    Private _GramWeight As Decimal
    Private _Rate As Decimal
    Private _Amount As Decimal
    Private _IsReject As Boolean
    Private _SaleLooseDiamondDetailID As String
    Private _PGemsCategoryID As String
    Private _PGemsName As String
    Private _Color As String
    Private _Shape As String
    Private _Clarity As String



#End Region

#Region "Properties "
    Public Property PurchaseDetailID() As String
        Get
            PurchaseDetailID = _PurchaseDetailID
        End Get
        Set(ByVal value As String)
            _PurchaseDetailID = value
        End Set
    End Property
    Public Property PurchaseHeaderID() As String
        Get
            PurchaseHeaderID = _PurchaseHeaderID
        End Get
        Set(ByVal value As String)
            _PurchaseHeaderID = value
        End Set
    End Property

    Public Property SaleInvoiceDetailID() As String
        Get
            SaleInvoiceDetailID = _SaleInvoiceDetailID
        End Get
        Set(ByVal value As String)
            _SaleInvoiceDetailID = value
        End Set
    End Property
    Public Property SaleGemsItemID() As String
        Get
            SaleGemsItemID = _SaleGemsItemID
        End Get
        Set(ByVal value As String)
            _SaleGemsItemID = value
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
    Public Property BarcodeNo() As String
        Get
            BarcodeNo = _BarcodeNo
        End Get
        Set(ByVal value As String)
            _BarcodeNo = value
        End Set
    End Property
    Public Property OldSaleAmount() As Integer
        Get
            OldSaleAmount = _OldSaleAmount
        End Get
        Set(ByVal value As Integer)
            _OldSaleAmount = value
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

    Public Property ItemName() As String
        Get
            ItemName = _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
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
    Public Property CurrentPrice() As Integer
        Get
            CurrentPrice = _CurrentPrice
        End Get
        Set(ByVal value As Integer)
            _CurrentPrice = value
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
    Public Property QTY() As Integer
        Get
            QTY = _QTY
        End Get
        Set(ByVal value As Integer)
            _QTY = value
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
    Public Property IsDamage() As Boolean
        Get
            IsDamage = _IsDamage
        End Get
        Set(ByVal value As Boolean)
            _IsDamage = value
        End Set
    End Property
    Public Property IsChange() As Boolean
        Get
            IsChange = _IsChange
        End Get
        Set(ByVal value As Boolean)
            _IsChange = value
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
    Public Property AddOrSub() As Integer
        Get
            AddOrSub = _AddOrSub
        End Get
        Set(ByVal value As Integer)
            _AddOrSub = value
        End Set
    End Property
    Public Property IsClose() As Boolean
        Get
            IsClose = _IsClose
        End Get
        Set(ByVal value As Boolean)
            _IsClose = value
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
    Public Property FixType() As String
        Get
            FixType = _FixType
        End Get
        Set(ByVal value As String)
            _FixType = value
        End Set
    End Property
    Public Property GemTW() As Decimal
        Get
            GemTW = _GemTW
        End Get
        Set(ByVal value As Decimal)
            _GemTW = value
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

    Public Property PurchaseWastePercent() As Integer
        Get
            PurchaseWastePercent = _PurchaseWastePercent
        End Get
        Set(ByVal value As Integer)
            _PurchaseWastePercent = value
        End Set
    End Property

    Public Property SaleRate() As Integer
        Get
            SaleRate = _SaleRate
        End Get
        Set(ByVal value As Integer)
            _SaleRate = value
        End Set
    End Property

    Public Property PurchaseDiscountAmount() As Integer
        Get
            PurchaseDiscountAmount = _PurchaseDiscountAmount
        End Get
        Set(ByVal value As Integer)
            _PurchaseDiscountAmount = value
        End Set
    End Property

    Public Property PWasteTK() As Decimal
        Get
            PWasteTK = _PWasteTK
        End Get
        Set(ByVal value As Decimal)
            _PWasteTK = value
        End Set
    End Property
    Public Property PWasteTG() As Decimal
        Get
            PWasteTG = _PWasteTG
        End Get
        Set(ByVal value As Decimal)
            _PWasteTG = value
        End Set
    End Property

    Public Property IsDone() As Boolean
        Get
            IsDone = _IsDone
        End Get
        Set(ByVal value As Boolean)
            _IsDone = value
        End Set
    End Property
    Public Property DoneAmount() As Integer
        Get
            DoneAmount = _DoneAmount
        End Get
        Set(ByVal value As Integer)
            _DoneAmount = value
        End Set
    End Property
    Public Property IsSalePercent() As Boolean
        Get
            IsSalePercent = _IsSalePercent
        End Get
        Set(ByVal value As Boolean)
            _IsSalePercent = value
        End Set
    End Property

    Public Property SalePercent() As Integer
        Get
            SalePercent = _SalePercent
        End Get
        Set(ByVal value As Integer)
            _SalePercent = value
        End Set
    End Property
    Public Property SalePercentAmount() As Integer
        Get
            SalePercentAmount = _SalePercentAmount
        End Get
        Set(ByVal value As Integer)
            _SalePercentAmount = value
        End Set
    End Property
    Public Property AddSub() As Integer
        Get
            AddSub = _AddSub
        End Get
        Set(ByVal value As Integer)
            _AddSub = value
        End Set
    End Property

    Public Property IsOrder() As Boolean
        Get
            IsOrder = _IsOrder
        End Get
        Set(ByVal value As Boolean)
            _IsOrder = value
        End Set
    End Property

    Public Property IsShop() As Boolean
        Get
            IsShop = _IsShop
        End Get
        Set(ByVal value As Boolean)
            _IsShop = value
        End Set
    End Property

    Public Property PurchaseFromSupplierItemID() As String
        Get
            PurchaseFromSupplierItemID = _PurchaseFromSupplierItemID
        End Get
        Set(ByVal value As String)
            _PurchaseFromSupplierItemID = value
        End Set
    End Property

    Public Property PurchaseFromSupplierID() As String
        Get
            PurchaseFromSupplierID = _PurchaseFromSupplierID
        End Get
        Set(ByVal value As String)
            _PurchaseFromSupplierID = value
        End Set
    End Property
    Public Property OriginalCode() As String
        Get
            OriginalCode = _OriginalCode
        End Get
        Set(ByVal value As String)
            _OriginalCode = value
        End Set
    End Property

    Public Property GramWeight() As Decimal
        Get
            GramWeight = _GramWeight
        End Get
        Set(ByVal value As Decimal)
            _GramWeight = value
        End Set
    End Property
    Public Property Rate() As Decimal
        Get
            Rate = _Rate
        End Get
        Set(ByVal value As Decimal)
            _Rate = value
        End Set
    End Property

    Public Property Amount() As Decimal
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property

    Public Property IsReject() As Boolean
        Get
            IsReject = _IsReject
        End Get
        Set(ByVal value As Boolean)
            _IsReject = value
        End Set
    End Property
    Public Property ConsignmentSaleItemID() As String
        Get
            ConsignmentSaleItemID = _ConsignmentSaleItemID
        End Get
        Set(ByVal value As String)
            _ConsignmentSaleItemID = value
        End Set
    End Property
    Public Property SaleLooseDiamondDetailID() As String
        Get
            SaleLooseDiamondDetailID = _SaleLooseDiamondDetailID
        End Get
        Set(ByVal value As String)
            _SaleLooseDiamondDetailID = value
        End Set
    End Property
    Public Property PGemsCategoryID() As String
        Get
            PGemsCategoryID = _PGemsCategoryID
        End Get
        Set(ByVal value As String)
            _PGemsCategoryID = value
        End Set
    End Property
    Public Property PGemsName() As String
        Get
            PGemsName = _PGemsName
        End Get
        Set(ByVal value As String)
            _PGemsName = value
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
    Public Property Shape() As String
        Get
            Shape = _Shape
        End Get
        Set(ByVal value As String)
            _Shape = value
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

#End Region
End Class
