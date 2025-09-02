Public Class SalesSolidGoldInfo
#Region "Private Property"
    Private _SaleSolidGoldID As String
    Private _SaleDate As String
    Private _StaffID As String
    Private _Customer As String
    Private _Address As String
    Private _LocationID As String
    Private _CounterID As String
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _GoldQualityID As String
    Private _ItemCategoryID As String
    Private _ItemName As String
    Private _SalesRate As Integer
    Private _DoneRate As Integer
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Integer
    Private _GoldC As Decimal
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _CurrentGoldK As Integer
    Private _CurrentGoldP As Integer
    Private _CurrentGoldY As Integer
    Private _CurrentGoldC As Decimal
    Private _CurrentGoldTK As Decimal
    Private _CurrentGoldTG As Decimal
    Private _SaleGoldK As Integer
    Private _SaleGoldP As Integer
    Private _SaleGoldY As Integer
    Private _SaleGoldC As Decimal
    Private _SaleGoldTK As Decimal
    Private _SaleGoldTG As Decimal
    Private _LeftGoldK As Integer
    Private _LeftGoldP As Integer
    Private _LeftGoldY As Integer
    Private _LeftGoldC As Decimal
    Private _LeftGoldTK As Decimal
    Private _LeftGoldTG As Decimal
    Private _TotalPayment As Integer
    Private _AddOrSub As Integer
    Private _DiscountAmount As Integer
    Private _PaidAmount As Integer
    Private _Remark As String

#End Region

#Region "Properties "
    Public Property SaleSolidGoldID() As String
        Get
            SaleSolidGoldID = _SaleSolidGoldID
        End Get
        Set(ByVal value As String)
            _SaleSolidGoldID = value
        End Set
    End Property
    Public Property SaleDate() As String
        Get
            SaleDate = _SaleDate
        End Get
        Set(ByVal value As String)
            _SaleDate = value
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
    Public Property ItemName() As String
        Get
            ItemName = _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
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
    Public Property DoneRate() As Integer
        Get
            DoneRate = _DoneRate
        End Get
        Set(ByVal value As Integer)
            _DoneRate = value
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
    Public Property CurrentGoldK() As Integer
        Get
            CurrentGoldK = _CurrentGoldK
        End Get
        Set(ByVal value As Integer)
            _CurrentGoldK = value
        End Set
    End Property
    Public Property CurrentGoldP() As Integer
        Get
            CurrentGoldP = _CurrentGoldP
        End Get
        Set(ByVal value As Integer)
            _CurrentGoldP = value
        End Set
    End Property
    Public Property CurrentGoldY() As Integer
        Get
            CurrentGoldY = _CurrentGoldY
        End Get
        Set(ByVal value As Integer)
            _CurrentGoldY = value
        End Set
    End Property
    Public Property CurrentGoldC() As Decimal
        Get
            CurrentGoldC = _CurrentGoldC
        End Get
        Set(ByVal value As Decimal)
            _CurrentGoldC = value
        End Set
    End Property
    Public Property CurrentGoldTK() As Decimal
        Get
            CurrentGoldTK = _CurrentGoldTK
        End Get
        Set(ByVal value As Decimal)
            _CurrentGoldTK = value
        End Set
    End Property
    Public Property CurrentGoldTG() As Decimal
        Get
            CurrentGoldTG = _CurrentGoldTG
        End Get
        Set(ByVal value As Decimal)
            _CurrentGoldTG = value
        End Set
    End Property
    Public Property SaleGoldK() As Integer
        Get
            SaleGoldK = _SaleGoldK
        End Get
        Set(ByVal value As Integer)
            _SaleGoldK = value
        End Set
    End Property
    Public Property SaleGoldP() As Integer
        Get
            SaleGoldP = _SaleGoldP
        End Get
        Set(ByVal value As Integer)
            _SaleGoldP = value
        End Set
    End Property
    Public Property SaleGoldY() As Integer
        Get
            SaleGoldY = _SaleGoldY
        End Get
        Set(ByVal value As Integer)
            _SaleGoldY = value
        End Set
    End Property
    Public Property SaleGoldC() As Decimal
        Get
            SaleGoldC = _SaleGoldC
        End Get
        Set(ByVal value As Decimal)
            _SaleGoldC = value
        End Set
    End Property
    Public Property SaleGoldTK() As Decimal
        Get
            SaleGoldTK = _SaleGoldTK
        End Get
        Set(ByVal value As Decimal)
            _SaleGoldTK = value
        End Set
    End Property
    Public Property SaleGoldTG() As Decimal
        Get
            SaleGoldTG = _SaleGoldTG
        End Get
        Set(ByVal value As Decimal)
            _SaleGoldTG = value
        End Set
    End Property
    Public Property LeftGoldK() As Integer
        Get
            LeftGoldK = _LeftGoldK
        End Get
        Set(ByVal value As Integer)
            _LeftGoldK = value
        End Set
    End Property
    Public Property LeftGoldP() As Integer
        Get
            LeftGoldP = _LeftGoldP
        End Get
        Set(ByVal value As Integer)
            _LeftGoldP = value
        End Set
    End Property
    Public Property LeftGoldY() As Integer
        Get
            LeftGoldY = _LeftGoldY
        End Get
        Set(ByVal value As Integer)
            _LeftGoldY = value
        End Set
    End Property
    Public Property LeftGoldC() As Decimal
        Get
            LeftGoldC = _LeftGoldC
        End Get
        Set(ByVal value As Decimal)
            _LeftGoldC = value
        End Set
    End Property
    Public Property LeftGoldTK() As Decimal
        Get
            LeftGoldTK = _LeftGoldTK
        End Get
        Set(ByVal value As Decimal)
            _LeftGoldTK = value
        End Set
    End Property
    Public Property LeftGoldTG() As Decimal
        Get
            LeftGoldTG = _LeftGoldTG
        End Get
        Set(ByVal value As Decimal)
            _LeftGoldTG = value
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

    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property

#End Region
End Class
