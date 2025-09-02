Public Class PurchaseInvoiceInfo
#Region "Private Property"
    Private _PurchaseInvoiceID As String
    Private _PDate As Date
    Private _StaffID As String
    Private _Customer As String
    Private _CustomerID As String
    Private _Address As String
    Private _ItemCategoryID As String
    Private _ItemName As String
    Private _Length As String
    Private _Qty As Integer
    Private _GoldQualityID As String
    Private _SubGoldQualityID As String
    Private _PurchaseRate As Integer
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Integer
    Private _GoldC As Decimal
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _TotalK As Integer
    Private _TotalP As Integer
    Private _TotalY As Integer
    Private _TotalC As Decimal
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _PaidAmount As Integer
    Private _GoldPrice As Integer
    Private _GemsPrice As Integer
    Private _Remark As String
    Private _IsExchange As Integer
    Private _FromShopOrCustomer As Integer
    Private _OldSaleInvoiceID As String
    Private _OldSaleAmount As Long
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    Private _TotalTK As Decimal
    Private _TotalTG As Decimal
    Private _IsDamage As Integer

#End Region

#Region "Properties "
    Public Property PurchaseInvoiceID() As String
        Get
            PurchaseInvoiceID = _PurchaseInvoiceID
        End Get
        Set(ByVal value As String)
            _PurchaseInvoiceID = value
        End Set
    End Property
    Public Property PDate() As Date
        Get
            PDate = _PDate
        End Get
        Set(ByVal value As Date)
            _PDate = value
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
    Public Property Customer() As String
        Get
            Customer = _Customer
        End Get
        Set(ByVal value As String)
            _Customer = value
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
    Public Property Address() As String
        Get
            Address = _Address
        End Get
        Set(ByVal value As String)
            _Address = value
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
    Public Property Qty() As Integer
        Get
            Qty = _Qty
        End Get
        Set(ByVal value As Integer)
            _Qty = value
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
    Public Property SubGoldQualityID() As String
        Get
            SubGoldQualityID = _SubGoldQualityID
        End Get
        Set(ByVal value As String)
            _SubGoldQualityID = value
        End Set
    End Property
    Public Property PurchaseRate() As Integer
        Get
            PurchaseRate = _PurchaseRate
        End Get
        Set(ByVal value As Integer)
            _PurchaseRate = value
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
    Public Property PaidAmount() As Long
        Get
            PaidAmount = _PaidAmount
        End Get
        Set(ByVal value As Long)
            _PaidAmount = value
        End Set
    End Property
    Public Property GoldPrice() As Long
        Get
            GoldPrice = _GoldPrice
        End Get
        Set(ByVal value As Long)
            _GoldPrice = value
        End Set
    End Property
    Public Property GemsPrice() As Long
        Get
            GemsPrice = _GemsPrice
        End Get
        Set(ByVal value As Long)
            _GemsPrice = value
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
    Public Property IsExchange() As Integer
        Get
            IsExchange = _IsExchange
        End Get
        Set(ByVal value As Integer)
            _IsExchange = value
        End Set
    End Property
    Public Property FromShopOrCustomer() As Integer
        Get
            FromShopOrCustomer = _FromShopOrCustomer
        End Get
        Set(ByVal value As Integer)
            _FromShopOrCustomer = value
        End Set
    End Property
    Public Property OldSaleInvoiceID() As String
        Get
            OldSaleInvoiceID = _OldSaleInvoiceID
        End Get
        Set(ByVal value As String)
            _OldSaleInvoiceID = value
        End Set
    End Property
    Public Property OldSaleAmount() As Long
        Get
            OldSaleAmount = _OldSaleAmount
        End Get
        Set(ByVal value As Long)
            _OldSaleAmount = value
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
    Public Property IsDamage() As Integer
        Get
            IsDamage = _IsDamage
        End Get
        Set(ByVal value As Integer)
            _IsDamage = value
        End Set
    End Property
#End Region
End Class
