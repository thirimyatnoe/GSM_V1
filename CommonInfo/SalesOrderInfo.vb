Public Class SalesOrderInfo
#Region "Private Property"
    Private _SaleOrderID As String
    Private _OrderDate As Date
    Private _IsReturn As Boolean
    Private _OrderRetrieveDate As Date
    Private _StaffID As String
    Private _Customer As String
    Private _Address As String
    Private _ItemCategoryID As String
    Private _GoldQualityID As String
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _ItemName As String
    Private _Length As String
    Private _Width As String
    Private _SalesRate As Integer
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
    Private _TotalPayment As Integer
    Private _AddOrSub As Integer
    Private _AdvanceAmount As Integer
    Private _PaidAmount As Integer
    Private _DiscountAmount As Integer
    Private _GoldPrice As Integer
    Private _GemsPrice As Integer
    Private _DesignCharges As Integer
    Private _Remark As String
    Private _LocationID As String
    Private _CounterID As String
    Private _IsSalesReturn As Integer
#End Region

#Region "Properties "
    Public Property SaleOrderID() As String
        Get
            SaleOrderID = _SaleOrderID
        End Get
        Set(ByVal value As String)
            _SaleOrderID = value
        End Set
    End Property
    Public Property OrderDate() As Date
        Get
            OrderDate = _OrderDate
        End Get
        Set(ByVal value As Date)
            _OrderDate = value
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
    Public Property OrderRetrieveDate() As Date
        Get
            OrderRetrieveDate = _OrderRetrieveDate
        End Get
        Set(ByVal value As Date)
            _OrderRetrieveDate = value
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
    Public Property GoldQualityID() As String
        Get
            GoldQualityID = _GoldQualityID
        End Get
        Set(ByVal value As String)
            _GoldQualityID = value
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
    Public Property Width() As String
        Get
            Width = _Width
        End Get
        Set(ByVal value As String)
            _Width = value
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
    Public Property AddOrSub() As Integer
        Get
            AddOrSub = _AddOrSub
        End Get
        Set(ByVal value As Integer)
            _AddOrSub = value
        End Set
    End Property
    Public Property AdvanceAmount() As Integer
        Get
            AdvanceAmount = _AdvanceAmount
        End Get
        Set(ByVal value As Integer)
            _AdvanceAmount = value
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
    Public Property DiscountAmount() As Integer
        Get
            DiscountAmount = _DiscountAmount
        End Get
        Set(ByVal value As Integer)
            _DiscountAmount = value
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
    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
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
    Public Property CounterID() As String
        Get
            CounterID = _CounterID
        End Get
        Set(ByVal value As String)
            _CounterID = value
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

#End Region
End Class
