Public Class SalesItemInfo
#Region "Private Property"
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _ItemName As String
    Private _ItemNameID As String
    Private _Length As String
    Private _GoldQualityID As String
    Private _ItemCategoryID As String
    Private _ItemCategory As String
    Private _GemsCategory As String
    Private _LocationID As String
    Private _GivenDate As Date
    Private _WReturnDate As Date


    Private _EnterDate As Date
    Private _GoodItemID As String
    Private _PayType As String

    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Decimal
    Private _GoldC As Decimal

    '*********
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    '************
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Decimal
    Private _GemsC As Decimal
    '*********
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    '************
    Private _WasteK As Integer
    Private _WasteP As Integer
    Private _WasteY As Decimal
    Private _WasteC As Decimal
    '*********
    Private _WasteTK As Decimal
    Private _WasteTG As Decimal
    '************
    Private _ItemK As Integer
    Private _ItemP As Integer
    Private _ItemY As Decimal
    Private _ItemC As Decimal


    '*********
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal

    '************
    Private _TotalK As Integer
    Private _TotalP As Integer
    Private _TotalY As Decimal
    Private _TotalC As Decimal
    '*********
    Private _TotalTK As Decimal
    Private _TotalTG As Decimal
    '************
    Private _IsExit As Boolean
    Private _ExitDate As Date

    '************
    'Add new field (GoldSmithID,Width) at 26.05.2012
    Private _GoldSmithID As String
    Private _Width As String
    Private _Remark As String

    Private _IsFixPrice As Boolean
    Private _FixPrice As Integer

    Private _OriginalPrice As Integer
    'Private _OriginalGemsPrice As Integer
    Private _GemsAmount As Integer

    'Add new field For TheLady
    Private _DesignCharges As Integer
    Private _PlatingCharges As Integer
    Private _MountingCharges As Integer
    Private _WhiteCharges As Integer
    Private _IsOriginalFixedPrice As Boolean
    Private _OriginalFixedPrice As Integer
    Private _IsOriginalPriceGram As Boolean
    Private _OriginalPriceGram As Integer
    Private _OriginalPriceTK As Integer
    Private _OriginalGemsPrice As Integer
    Private _OriginalOtherPrice As Integer
    Private _IsClosed As Boolean
    Private _IsOrder As Boolean
    Private _Photo As String
    Private _SellingPrice As String
    Private _OrderInvoiceID As String
    Private _IsVolume As Boolean
    Private _QTY As Integer
    Private _StaffID As String
    Private _LossQTY As Integer
    Private _LossItemTK As Decimal
    Private _LossItemTG As Decimal
    Private _TotalGemPrice As Integer
    Private _PurchaseWasteTK As Decimal
    Private _PurchaseWasteTG As Decimal
    Private _PurchaseWasteK As Integer
    Private _PurchaseWasteP As Integer
    Private _PurchaseWasteY As Decimal
    Private _PurchaseWasteC As Decimal
    Private _OrderReceiveDetailID As String
    Private _OldOrderReceiveDetailID As String
    Private _OldIsOrder As Boolean
    Private _GoldSmith As String
    Private _IsDiamond As Boolean
    Private _OriginalCode As String
    Private _PriceCode As String
    Private _GoldQuality As String
    Private _LastModifiedLoginUserName As String
    Private _LastModifiedDate As Date
    Private _Color As String
    Private _SupplierID As String
    Private _SupplierVou As String

    Private _HForSaleID As String
    Private _HItemCode As String
    Private _HItemName As String
    Private _HItemNameID As String
    Private _HLength As String
    Private _HGoldQualityID As String
    Private _HItemCategoryID As String
    Private _HItemCategory As String
    Private _HLocationID As String
    Private _HGivenDate As Date


    Private _HEnterDate As Date
    Private _HGoodItemID As String
    Private _HPayType As String

    Private _HGoldK As Integer
    Private _HGoldP As Integer
    Private _HGoldY As Decimal
    Private _HGoldC As Decimal

    '*********
    Private _HGoldTK As Decimal
    Private _HGoldTG As Decimal
    '************
    Private _HGemsK As Integer
    Private _HGemsP As Integer
    Private _HGemsY As Decimal
    Private _HGemsC As Decimal
    '*********
    Private _HGemsTK As Decimal
    Private _HGemsTG As Decimal
    '************
    Private _HWasteK As Integer
    Private _HWasteP As Integer
    Private _HWasteY As Decimal
    Private _HWasteC As Decimal
    '*********
    Private _HWasteTK As Decimal
    Private _HWasteTG As Decimal
    '************
    Private _HItemK As Integer
    Private _HItemP As Integer
    Private _HItemY As Decimal
    Private _HItemC As Decimal


    '*********
    Private _HItemTK As Decimal
    Private _HItemTG As Decimal

    '************
    Private _HTotalK As Integer
    Private _HTotalP As Integer
    Private _HTotalY As Decimal
    Private _HTotalC As Decimal
    '*********
    Private _HTotalTK As Decimal
    Private _HTotalTG As Decimal
    '************
    Private _HIsExit As Boolean
    Private _HExitDate As Date

    '************
    'Add new field (GoldSmithID,Width) at 26.05.2012
    Private _HGoldSmithID As String
    Private _HWidth As String
    Private _HRemark As String

    Private _HIsFixPrice As Boolean
    Private _HFixPrice As Integer

    Private _HOriginalPrice As Integer
    'Private _HriginalGemsPrice As Integer
    Private _HGemsAmount As Integer

    'Add new field For TheLady
    Private _HDesignCharges As Integer
    Private _HPlatingCharges As Integer
    Private _HMountingCharges As Integer
    Private _HWhiteCharges As Integer
    Private _HIsOriginalFixedPrice As Boolean
    Private _HOriginalFixedPrice As Integer
    Private _HIsOriginalPriceGram As Boolean
    Private _HOriginalPriceGram As Integer
    Private _HOriginalPriceTK As Integer
    Private _HOriginalGemsPrice As Integer
    Private _HOriginalOtherPrice As Integer
    Private _HIsClosed As Boolean
    Private _HIsOrder As Boolean
    Private _HPhoto As String
    Private _HSellingPrice As String
    Private _HOrderInvoiceID As String
    Private _HIsVolume As Boolean
    Private _HQTY As Integer
    Private _HStaffID As String
    Private _HLossQTY As Integer
    Private _HLossItemTK As Decimal
    Private _HLossItemTG As Decimal
    Private _HTotalGemPrice As Integer
    Private _HPurchaseWasteTK As Decimal
    Private _HPurchaseWasteTG As Decimal
    Private _HPurchaseWasteK As Integer
    Private _HPurchaseWasteP As Integer
    Private _HPurchaseWasteY As Decimal
    Private _HPurchaseWasteC As Decimal
    Private _HOrderReceiveDetailID As String
    Private _HOldOrderReceiveDetailID As String
    Private _HOldIsOrder As Boolean
    Private _HGoldSmith As String
    Private _HIsDiamond As Boolean
    Private _HOriginalCode As String
    Private _HPriceCode As String
    Private _HGoldQuality As String
    Private _HLastModifiedLoginUserName As String
    Private _HLastModifiedDate As Date
    Private _HColor As String
    Private _HSupplierID As String
    Private _HSupplierVou As String
    Private _IsSolidVolume As Boolean
    Private _SellingRate As Integer
    Private _IsCheck As Boolean
    Private _WSFixPrice As Integer
    Private _IsLooseDiamond As Boolean
    Private _SDGemsCategoryID As String
    Private _Shape As String
    Private _Clarity As String
    Private _SDGemsName As String
    Private _OriginalPriceCarat As Integer
    Private _SDGemsTW As Decimal
    Private _SDYOrCOrG As String
    Private _IsOriginalPriceCarat As Boolean
    Private _TotalCost As Integer

#End Region

#Region "Properties "
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
    Public Property GoldQuality() As String
        Get
            GoldQuality = _GoldQuality
        End Get
        Set(ByVal value As String)
            _GoldQuality = value
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
    Public Property GoldQualityID() As String
        Get
            GoldQualityID = _GoldQualityID
        End Get
        Set(ByVal value As String)
            _GoldQualityID = value
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
    Public Property ItemCategory() As String
        Get
            ItemCategory = _ItemCategory
        End Get
        Set(ByVal value As String)
            _ItemCategory = value
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


    Public Property GivenDate() As Date
        Get
            GivenDate = _GivenDate
        End Get
        Set(ByVal value As Date)
            _GivenDate = value
        End Set
    End Property

    Public Property WReturnDate() As Date
        Get
            WReturnDate = _WReturnDate
        End Get
        Set(ByVal value As Date)
            _WReturnDate = value
        End Set
    End Property
    Public Property EnterDate() As Date
        Get
            EnterDate = _EnterDate
        End Get
        Set(ByVal value As Date)
            _EnterDate = value
        End Set
    End Property
    Public Property GoodItemID() As String
        Get
            GoodItemID = _GoodItemID
        End Get
        Set(ByVal value As String)
            _GoodItemID = value
        End Set
    End Property

    Public Property PayType() As String
        Get
            PayType = _PayType
        End Get
        Set(ByVal value As String)
            _PayType = value
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
    Public Property IsExit() As Boolean
        Get
            IsExit = _IsExit
        End Get
        Set(ByVal value As Boolean)
            _IsExit = value
        End Set
    End Property
    Public Property ExitDate() As Date
        Get
            ExitDate = _ExitDate
        End Get
        Set(ByVal value As Date)
            _ExitDate = value
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
    Public Property Width() As String
        Get
            Width = _Width
        End Get
        Set(ByVal value As String)
            _Width = value
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
    Public Property FixPrice() As Integer
        Get
            FixPrice = _FixPrice
        End Get
        Set(ByVal value As Integer)
            _FixPrice = value
        End Set
    End Property
    Public Property OriginalPrice() As Integer
        Get
            OriginalPrice = _OriginalPrice
        End Get
        Set(ByVal value As Integer)
            _OriginalPrice = value
        End Set
    End Property
    Public Property GemsAmount() As Integer
        Get
            GemsAmount = _GemsAmount
        End Get
        Set(ByVal value As Integer)
            _GemsAmount = value
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
    'TheLady
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
    Public Property Photo() As String
        Get
            Photo = _Photo
        End Get
        Set(ByVal value As String)
            _Photo = value
        End Set
    End Property
    Public Property SellingPrice() As String
        Get
            SellingPrice = _SellingPrice
        End Get
        Set(ByVal value As String)
            _SellingPrice = value
        End Set
    End Property

    Public Property IsClosed() As Boolean
        Get
            IsClosed = _IsClosed
        End Get
        Set(ByVal value As Boolean)
            _IsClosed = value
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

    Public Property OrderInvoiceID() As String
        Get
            OrderInvoiceID = _OrderInvoiceID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceID = value
        End Set
    End Property

    Public Property IsVolume() As Boolean
        Get
            IsVolume = _IsVolume
        End Get
        Set(ByVal value As Boolean)
            _IsVolume = value
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


    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
        End Set
    End Property

    Public Property LossQTY() As Integer
        Get
            LossQTY = _LossQTY
        End Get
        Set(ByVal value As Integer)
            _LossQTY = value
        End Set
    End Property
    Public Property LossItemTK() As Decimal
        Get
            LossItemTK = _LossItemTK
        End Get
        Set(ByVal value As Decimal)
            _LossItemTK = value
        End Set
    End Property
    Public Property LossItemTG() As Decimal
        Get
            LossItemTG = _LossItemTG
        End Get
        Set(ByVal value As Decimal)
            _LossItemTG = value
        End Set
    End Property
    Public Property TotalGemPrice() As Integer
        Get
            TotalGemPrice = _TotalGemPrice
        End Get
        Set(value As Integer)
            _TotalGemPrice = value
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

    Public Property PurchaseWasteK() As Integer
        Get
            PurchaseWasteK = _PurchaseWasteK
        End Get
        Set(ByVal value As Integer)
            _PurchaseWasteK = value
        End Set
    End Property
    Public Property PurchaseWasteP() As Integer
        Get
            PurchaseWasteP = _PurchaseWasteP
        End Get
        Set(ByVal value As Integer)
            _PurchaseWasteP = value
        End Set
    End Property
    Public Property PurchaseWasteY() As Decimal
        Get
            PurchaseWasteY = _PurchaseWasteY
        End Get
        Set(ByVal value As Decimal)
            _PurchaseWasteY = value
        End Set
    End Property
    Public Property PurchaseWasteC() As Decimal
        Get
            PurchaseWasteC = _PurchaseWasteC
        End Get
        Set(ByVal value As Decimal)
            _PurchaseWasteC = value
        End Set
    End Property
    Public Property OrderReceiveDetailID() As String
        Get
            OrderReceiveDetailID = _OrderReceiveDetailID
        End Get
        Set(ByVal value As String)
            _OrderReceiveDetailID = value
        End Set
    End Property
    Public Property OldOrderReceiveDetailID() As String
        Get
            OldOrderReceiveDetailID = _OldOrderReceiveDetailID
        End Get
        Set(ByVal value As String)
            _OldOrderReceiveDetailID = value
        End Set
    End Property

    Public Property OldIsOrder() As Boolean
        Get
            OldIsOrder = _OldIsOrder
        End Get
        Set(ByVal value As Boolean)
            _OldIsOrder = value
        End Set
    End Property

    Public Property GoldSmith() As String
        Get
            GoldSmith = _GoldSmith
        End Get
        Set(ByVal value As String)
            _GoldSmith = value
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
    Public Property OriginalCode() As String
        Get
            OriginalCode = _OriginalCode
        End Get
        Set(ByVal value As String)
            _OriginalCode = value
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
    Public Property LastModifiedLoginUserName() As String
        Get
            LastModifiedLoginUserName = _LastModifiedLoginUserName
        End Get
        Set(ByVal value As String)
            _LastModifiedLoginUserName = value
        End Set
    End Property


    Public Property LastModifiedDate() As Date
        Get
            LastModifiedDate = _LastModifiedDate
        End Get
        Set(ByVal value As Date)
            _LastModifiedDate = value
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
    Public Property SupplierID() As String
        Get
            SupplierID = _SupplierID
        End Get
        Set(ByVal value As String)
            _SupplierID = value
        End Set
    End Property
    Public Property SupplierVou() As String
        Get
            SupplierVou = _SupplierVou
        End Get
        Set(ByVal value As String)
            _SupplierVou = value
        End Set
    End Property

    'For History Data----
    Public Property HForSaleID() As String
        Get
            HForSaleID = _HForSaleID
        End Get
        Set(ByVal value As String)
            _HForSaleID = value
        End Set
    End Property

    Public Property HItemCode() As String
        Get
            HItemCode = _HItemCode
        End Get
        Set(ByVal value As String)
            _HItemCode = value
        End Set
    End Property
    Public Property HItemName() As String
        Get
            HItemName = _HItemName
        End Get
        Set(ByVal value As String)
            _HItemName = value
        End Set
    End Property
    Public Property HGoldQuality() As String
        Get
            HGoldQuality = _HGoldQuality
        End Get
        Set(ByVal value As String)
            _HGoldQuality = value
        End Set
    End Property
    Public Property HItemNameID() As String
        Get
            HItemNameID = _HItemNameID
        End Get
        Set(ByVal value As String)
            _HItemNameID = value
        End Set
    End Property

    Public Property HLength() As String
        Get
            HLength = _HLength
        End Get
        Set(ByVal value As String)
            _HLength = value
        End Set
    End Property
    Public Property HGoldQualityID() As String
        Get
            HGoldQualityID = _HGoldQualityID
        End Get
        Set(ByVal value As String)
            _HGoldQualityID = value
        End Set
    End Property
    Public Property HItemCategoryID() As String
        Get
            HItemCategoryID = _HItemCategoryID
        End Get
        Set(ByVal value As String)
            _HItemCategoryID = value
        End Set
    End Property
    Public Property HItemCategory() As String
        Get
            HItemCategory = _HItemCategory
        End Get
        Set(ByVal value As String)
            _HItemCategory = value
        End Set
    End Property
    Public Property HLocationID() As String
        Get
            HLocationID = _HLocationID
        End Get
        Set(ByVal value As String)
            _HLocationID = value
        End Set
    End Property


    Public Property HGivenDate() As Date
        Get
            HGivenDate = _HGivenDate
        End Get
        Set(ByVal value As Date)
            _HGivenDate = value
        End Set
    End Property
    Public Property HEnterDate() As Date
        Get
            HEnterDate = _HEnterDate
        End Get
        Set(ByVal value As Date)
            _HEnterDate = value
        End Set
    End Property
    Public Property HGoodItemID() As String
        Get
            HGoodItemID = _HGoodItemID
        End Get
        Set(ByVal value As String)
            _HGoodItemID = value
        End Set
    End Property

    Public Property HPayType() As String
        Get
            HPayType = _HPayType
        End Get
        Set(ByVal value As String)
            _HPayType = value
        End Set
    End Property
    Public Property HGoldK() As Integer
        Get
            HGoldK = _HGoldK
        End Get
        Set(ByVal value As Integer)
            _HGoldK = value
        End Set
    End Property
    Public Property HGoldP() As Integer
        Get
            HGoldP = _HGoldP
        End Get
        Set(ByVal value As Integer)
            _HGoldP = value
        End Set
    End Property
    Public Property HGoldY() As Decimal
        Get
            HGoldY = _HGoldY
        End Get
        Set(ByVal value As Decimal)
            _HGoldY = value
        End Set
    End Property
    Public Property HGoldC() As Decimal
        Get
            HGoldC = _HGoldC
        End Get
        Set(ByVal value As Decimal)
            _HGoldC = value
        End Set
    End Property
    Public Property HGoldTK() As Decimal
        Get
            HGoldTK = _HGoldTK
        End Get
        Set(ByVal value As Decimal)
            _HGoldTK = value
        End Set
    End Property
    Public Property HGoldTG() As Decimal
        Get
            HGoldTG = _HGoldTG
        End Get
        Set(ByVal value As Decimal)
            _HGoldTG = value
        End Set
    End Property
    Public Property HGemsK() As Integer
        Get
            HGemsK = _HGemsK
        End Get
        Set(ByVal value As Integer)
            _HGemsK = value
        End Set
    End Property
    Public Property HGemsP() As Integer
        Get
            HGemsP = _HGemsP
        End Get
        Set(ByVal value As Integer)
            _HGemsP = value
        End Set
    End Property
    Public Property HGemsY() As Decimal
        Get
            HGemsY = _HGemsY
        End Get
        Set(ByVal value As Decimal)
            _HGemsY = value
        End Set
    End Property
    Public Property HGemsC() As Decimal
        Get
            HGemsC = _HGemsC
        End Get
        Set(ByVal value As Decimal)
            _HGemsC = value
        End Set
    End Property
    Public Property HGemsTK() As Decimal
        Get
            HGemsTK = _HGemsTK
        End Get
        Set(ByVal value As Decimal)
            _HGemsTK = value
        End Set
    End Property
    Public Property HGemsTG() As Decimal
        Get
            HGemsTG = _HGemsTG
        End Get
        Set(ByVal value As Decimal)
            _HGemsTG = value
        End Set
    End Property
    Public Property HWasteK() As Integer
        Get
            HWasteK = _HWasteK
        End Get
        Set(ByVal value As Integer)
            _HWasteK = value
        End Set
    End Property
    Public Property HWasteP() As Integer
        Get
            HWasteP = _HWasteP
        End Get
        Set(ByVal value As Integer)
            _HWasteP = value
        End Set
    End Property
    Public Property HWasteY() As Decimal
        Get
            HWasteY = _HWasteY
        End Get
        Set(ByVal value As Decimal)
            _HWasteY = value
        End Set
    End Property
    Public Property HWasteC() As Decimal
        Get
            HWasteC = _HWasteC
        End Get
        Set(ByVal value As Decimal)
            _HWasteC = value
        End Set
    End Property
    Public Property HWasteTK() As Decimal
        Get
            HWasteTK = _HWasteTK
        End Get
        Set(ByVal value As Decimal)
            _HWasteTK = value
        End Set
    End Property
    Public Property HWasteTG() As Decimal
        Get
            HWasteTG = _HWasteTG
        End Get
        Set(ByVal value As Decimal)
            _HWasteTG = value
        End Set
    End Property
    Public Property HItemK() As Integer
        Get
            HItemK = _HItemK
        End Get
        Set(ByVal value As Integer)
            _HItemK = value
        End Set
    End Property
    Public Property HItemP() As Integer
        Get
            HItemP = _HItemP
        End Get
        Set(ByVal value As Integer)
            _HItemP = value
        End Set
    End Property
    Public Property HItemY() As Decimal
        Get
            HItemY = _HItemY
        End Get
        Set(ByVal value As Decimal)
            _HItemY = value
        End Set
    End Property
    Public Property HItemC() As Decimal
        Get
            HItemC = _HItemC
        End Get
        Set(ByVal value As Decimal)
            _HItemC = value
        End Set
    End Property
    Public Property HItemTK() As Decimal
        Get
            HItemTK = _HItemTK
        End Get
        Set(ByVal value As Decimal)
            _HItemTK = value
        End Set
    End Property
    Public Property HItemTG() As Decimal
        Get
            HItemTG = _HItemTG
        End Get
        Set(ByVal value As Decimal)
            _HItemTG = value
        End Set
    End Property
    Public Property HTotalK() As Integer
        Get
            HTotalK = _HTotalK
        End Get
        Set(ByVal value As Integer)
            _HTotalK = value
        End Set
    End Property
    Public Property HTotalP() As Integer
        Get
            HTotalP = _HTotalP
        End Get
        Set(ByVal value As Integer)
            _HTotalP = value
        End Set
    End Property
    Public Property HTotalY() As Decimal
        Get
            HTotalY = _HTotalY
        End Get
        Set(ByVal value As Decimal)
            _HTotalY = value
        End Set
    End Property
    Public Property HTotalC() As Decimal
        Get
            HTotalC = _HTotalC
        End Get
        Set(ByVal value As Decimal)
            _HTotalC = value
        End Set
    End Property
    Public Property HTotalTK() As Decimal
        Get
            HTotalTK = _HTotalTK
        End Get
        Set(ByVal value As Decimal)
            _HTotalTK = value
        End Set
    End Property
    Public Property HTotalTG() As Decimal
        Get
            HTotalTG = _HTotalTG
        End Get
        Set(ByVal value As Decimal)
            _HTotalTG = value
        End Set
    End Property
    
    Public Property HGoldSmithID() As String
        Get
            HGoldSmithID = _HGoldSmithID
        End Get
        Set(ByVal value As String)
            _HGoldSmithID = value
        End Set
    End Property
    Public Property HWidth() As String
        Get
            HWidth = _HWidth
        End Get
        Set(ByVal value As String)
            _HWidth = value
        End Set
    End Property
    Public Property HIsFixPrice() As Boolean
        Get
            HIsFixPrice = _HIsFixPrice
        End Get
        Set(ByVal value As Boolean)
            _HIsFixPrice = value
        End Set
    End Property
    Public Property HFixPrice() As Integer
        Get
            HFixPrice = _HFixPrice
        End Get
        Set(ByVal value As Integer)
            _HFixPrice = value
        End Set
    End Property
    Public Property HOriginalPrice() As Integer
        Get
            HOriginalPrice = _HOriginalPrice
        End Get
        Set(ByVal value As Integer)
            _HOriginalPrice = value
        End Set
    End Property
    Public Property HGemsAmount() As Integer
        Get
            HGemsAmount = _HGemsAmount
        End Get
        Set(ByVal value As Integer)
            _HGemsAmount = value
        End Set
    End Property
    Public Property HRemark() As String
        Get
            HRemark = _HRemark
        End Get
        Set(ByVal value As String)
            _HRemark = value
        End Set
    End Property
    'TheLady
    Public Property HDesignCharges() As Integer
        Get
            HDesignCharges = _HDesignCharges
        End Get
        Set(ByVal value As Integer)
            _HDesignCharges = value
        End Set
    End Property
    Public Property HPlatingCharges() As Integer
        Get
            HPlatingCharges = _HPlatingCharges
        End Get
        Set(ByVal value As Integer)
            _HPlatingCharges = value
        End Set
    End Property
    Public Property HMountingCharges() As Integer
        Get
            HMountingCharges = _HMountingCharges
        End Get
        Set(ByVal value As Integer)
            _HMountingCharges = value
        End Set
    End Property
    Public Property HWhiteCharges() As Integer
        Get
            HWhiteCharges = _HWhiteCharges
        End Get
        Set(ByVal value As Integer)
            _HWhiteCharges = value
        End Set
    End Property
    Public Property HIsOriginalFixedPrice() As Boolean
        Get
            HIsOriginalFixedPrice = _HIsOriginalFixedPrice
        End Get
        Set(ByVal value As Boolean)
            _HIsOriginalFixedPrice = value
        End Set
    End Property
    Public Property HOriginalFixedPrice() As Integer
        Get
            HOriginalFixedPrice = _HOriginalFixedPrice
        End Get
        Set(ByVal value As Integer)
            _HOriginalFixedPrice = value
        End Set
    End Property
    Public Property HIsOriginalPriceGram() As Boolean
        Get
            HIsOriginalPriceGram = _HIsOriginalPriceGram
        End Get
        Set(ByVal value As Boolean)
            _HIsOriginalPriceGram = value
        End Set
    End Property
    Public Property HOriginalPriceGram() As Integer
        Get
            HOriginalPriceGram = _HOriginalPriceGram
        End Get
        Set(ByVal value As Integer)
            _HOriginalPriceGram = value
        End Set
    End Property
    Public Property HOriginalPriceTK() As Integer
        Get
            HOriginalPriceTK = _HOriginalPriceTK
        End Get
        Set(ByVal value As Integer)
            _HOriginalPriceTK = value
        End Set
    End Property
    Public Property HOriginalGemsPrice() As Integer
        Get
            HOriginalGemsPrice = _HOriginalGemsPrice
        End Get
        Set(ByVal value As Integer)
            _HOriginalGemsPrice = value
        End Set
    End Property
    Public Property HOriginalOtherPrice() As Integer
        Get
            HOriginalOtherPrice = _HOriginalOtherPrice
        End Get
        Set(ByVal value As Integer)
            _HOriginalOtherPrice = value
        End Set
    End Property
    Public Property HPhoto() As String
        Get
            HPhoto = _HPhoto
        End Get
        Set(ByVal value As String)
            _HPhoto = value
        End Set
    End Property
    Public Property HSellingPrice() As String
        Get
            HSellingPrice = _HSellingPrice
        End Get
        Set(ByVal value As String)
            _HSellingPrice = value
        End Set
    End Property

    Public Property HIsClosed() As Boolean
        Get
            HIsClosed = _HIsClosed
        End Get
        Set(ByVal value As Boolean)
            _HIsClosed = value
        End Set
    End Property

    Public Property HIsOrder() As Boolean
        Get
            HIsOrder = _HIsOrder
        End Get
        Set(ByVal value As Boolean)
            _HIsOrder = value
        End Set
    End Property

    Public Property HOrderInvoiceID() As String
        Get
            HOrderInvoiceID = _HOrderInvoiceID
        End Get
        Set(ByVal value As String)
            _HOrderInvoiceID = value
        End Set
    End Property

    Public Property HIsVolume() As Boolean
        Get
            HIsVolume = _HIsVolume
        End Get
        Set(ByVal value As Boolean)
            _HIsVolume = value
        End Set
    End Property

    Public Property HQTY() As Integer
        Get
            HQTY = _HQTY
        End Get
        Set(ByVal value As Integer)
            _HQTY = value
        End Set
    End Property


    Public Property HStaffID() As String
        Get
            HStaffID = _HStaffID
        End Get
        Set(ByVal value As String)
            _HStaffID = value
        End Set
    End Property

    Public Property HLossQTY() As Integer
        Get
            HLossQTY = _HLossQTY
        End Get
        Set(ByVal value As Integer)
            _HLossQTY = value
        End Set
    End Property
    Public Property HLossItemTK() As Decimal
        Get
            HLossItemTK = _HLossItemTK
        End Get
        Set(ByVal value As Decimal)
            _HLossItemTK = value
        End Set
    End Property
    Public Property HLossItemTG() As Decimal
        Get
            HLossItemTG = _HLossItemTG
        End Get
        Set(ByVal value As Decimal)
            _HLossItemTG = value
        End Set
    End Property
    Public Property HTotalGemPrice() As Integer
        Get
            HTotalGemPrice = _HTotalGemPrice
        End Get
        Set(value As Integer)
            _HTotalGemPrice = value
        End Set
    End Property

    Public Property HPurchaseWasteTG() As Decimal
        Get
            HPurchaseWasteTG = _HPurchaseWasteTG
        End Get
        Set(ByVal value As Decimal)
            _HPurchaseWasteTG = value
        End Set
    End Property
    Public Property HPurchaseWasteTK() As Decimal
        Get
            HPurchaseWasteTK = _HPurchaseWasteTK
        End Get
        Set(ByVal value As Decimal)
            _HPurchaseWasteTK = value
        End Set
    End Property

    Public Property HPurchaseWasteK() As Integer
        Get
            HPurchaseWasteK = _HPurchaseWasteK
        End Get
        Set(ByVal value As Integer)
            _HPurchaseWasteK = value
        End Set
    End Property
    Public Property HPurchaseWasteP() As Integer
        Get
            HPurchaseWasteP = _HPurchaseWasteP
        End Get
        Set(ByVal value As Integer)
            _HPurchaseWasteP = value
        End Set
    End Property
    Public Property HPurchaseWasteY() As Decimal
        Get
            HPurchaseWasteY = _HPurchaseWasteY
        End Get
        Set(ByVal value As Decimal)
            _HPurchaseWasteY = value
        End Set
    End Property
    Public Property HPurchaseWasteC() As Decimal
        Get
            HPurchaseWasteC = _HPurchaseWasteC
        End Get
        Set(ByVal value As Decimal)
            _HPurchaseWasteC = value
        End Set
    End Property
    Public Property HOrderReceiveDetailID() As String
        Get
            HOrderReceiveDetailID = _HOrderReceiveDetailID
        End Get
        Set(ByVal value As String)
            _HOrderReceiveDetailID = value
        End Set
    End Property
    Public Property HOldOrderReceiveDetailID() As String
        Get
            HOldOrderReceiveDetailID = _HOldOrderReceiveDetailID
        End Get
        Set(ByVal value As String)
            _HOldOrderReceiveDetailID = value
        End Set
    End Property

    Public Property HOldIsOrder() As Boolean
        Get
            HOldIsOrder = _HOldIsOrder
        End Get
        Set(ByVal value As Boolean)
            _HOldIsOrder = value
        End Set
    End Property

    Public Property HGoldSmith() As String
        Get
            HGoldSmith = _HGoldSmith
        End Get
        Set(ByVal value As String)
            _HGoldSmith = value
        End Set
    End Property

    Public Property HIsDiamond() As Boolean
        Get
            HIsDiamond = _HIsDiamond
        End Get
        Set(ByVal value As Boolean)
            _HIsDiamond = value
        End Set
    End Property
    Public Property HOriginalCode() As String
        Get
            HOriginalCode = _HOriginalCode
        End Get
        Set(ByVal value As String)
            _HOriginalCode = value
        End Set
    End Property

    Public Property HPriceCode() As String
        Get
            HPriceCode = _HPriceCode
        End Get
        Set(ByVal value As String)
            _HPriceCode = value
        End Set
    End Property
   

    Public Property HColor() As String
        Get
            HColor = _HColor
        End Get
        Set(ByVal value As String)
            _HColor = value
        End Set
    End Property
    Public Property HSupplierID() As String
        Get
            HSupplierID = _HSupplierID
        End Get
        Set(ByVal value As String)
            _HSupplierID = value
        End Set
    End Property
    Public Property HSupplierVou() As String
        Get
            HSupplierVou = _HSupplierVou
        End Get
        Set(ByVal value As String)
            _HSupplierVou = value
        End Set
    End Property
    Public Property IsSolidVolume() As Boolean
        Get
            IsSolidVolume = _IsSolidVolume
        End Get
        Set(ByVal value As Boolean)
            _IsSolidVolume = value
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
    Public Property IsCheck() As Boolean
        Get
            IsCheck = _IsCheck
        End Get
        Set(ByVal value As Boolean)
            _IsCheck = value
        End Set
    End Property
    Public Property WSFixPrice() As Integer
        Get
            WSFixPrice = _WSFixPrice
        End Get
        Set(ByVal value As Integer)
            _WSFixPrice = value
        End Set
    End Property
    Public Property IsLooseDiamond() As Boolean
        Get
            IsLooseDiamond = _IsLooseDiamond
        End Get
        Set(ByVal value As Boolean)
            _IsLooseDiamond = value
        End Set
    End Property
    Public Property SDGemsCategoryID() As String
        Get
            SDGemsCategoryID = _SDGemsCategoryID
        End Get
        Set(ByVal value As String)
            _SDGemsCategoryID = value
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
    Public Property SDGemsName() As String
        Get
            SDGemsName = _SDGemsName
        End Get
        Set(ByVal value As String)
            _SDGemsName = value
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
    Public Property SDGemsTW() As Decimal
        Get
            SDGemsTW = _SDGemsTW
        End Get
        Set(ByVal value As Decimal)
            _SDGemsTW = value
        End Set
    End Property
    Public Property SDYOrCOrG() As String
        Get
            SDYOrCOrG = _SDYOrCOrG
        End Get
        Set(ByVal value As String)
            _SDYOrCOrG = value
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
    Public Property GemsCategory() As String
        Get
            GemsCategory = _GemsCategory
        End Get
        Set(ByVal value As String)
            _GemsCategory = value
        End Set
    End Property
    Public Property TotalCost() As Integer
        Get
            TotalCost = _TotalCost
        End Get
        Set(ByVal value As Integer)
            _TotalCost = value
        End Set
    End Property
#End Region
End Class
