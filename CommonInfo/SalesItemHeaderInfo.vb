Public Class SalesItemHeaderInfo
#Region "Private Property"
    Private _ForSaleHeaderID As String
    Private _EntryDate As Date
    Private _MainStoreDivideItemID As String
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _LossGoldTK As Decimal
    Private _LossGoldTG As Decimal
    Private _NetGoldTK As Decimal
    Private _NetGoldTG As Decimal
    '***********************
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Integer
    Private _GoldC As Decimal
    '***********************
    Private _LossGoldK As Integer
    Private _LossGoldP As Integer
    Private _LossGoldY As Integer
    Private _LossGoldC As Decimal
    Private _AllTaxAmt As Integer

#End Region

#Region "Properties "

    Public Property ForSaleHeaderID() As String
        Get
            ForSaleHeaderID = _ForSaleHeaderID
        End Get
        Set(ByVal value As String)
            _ForSaleHeaderID = value
        End Set
    End Property
    Public Property EntryDate() As Date
        Get
            EntryDate = _EntryDate
        End Get
        Set(ByVal value As Date)
            _EntryDate = value
        End Set
    End Property
    Public Property MainStoreDivideItemID() As String
        Get
            MainStoreDivideItemID = _MainStoreDivideItemID
        End Get
        Set(ByVal value As String)
            _MainStoreDivideItemID = value
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
    Public Property LossGoldTK() As Decimal
        Get
            LossGoldTK = _LossGoldTK
        End Get
        Set(ByVal value As Decimal)
            _LossGoldTK = value
        End Set
    End Property
    Public Property LossGoldTG() As Decimal
        Get
            LossGoldTG = _LossGoldTG
        End Get
        Set(ByVal value As Decimal)
            _LossGoldTG = value
        End Set
    End Property
    Public Property NetGoldTK() As Decimal
        Get
            NetGoldTK = _NetGoldTK
        End Get
        Set(ByVal value As Decimal)
            _NetGoldTK = value
        End Set
    End Property
    Public Property NetGoldTG() As Decimal
        Get
            NetGoldTG = _NetGoldTG
        End Get
        Set(ByVal value As Decimal)
            _NetGoldTG = value
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
    Public Property LossGoldK() As Integer
        Get
            LossGoldK = _LossGoldK
        End Get
        Set(ByVal value As Integer)
            _LossGoldK = value
        End Set
    End Property
    Public Property LossGoldP() As Integer
        Get
            LossGoldP = _LossGoldP
        End Get
        Set(ByVal value As Integer)
            _LossGoldP = value
        End Set
    End Property
    Public Property LossGoldY() As Integer
        Get
            LossGoldY = _LossGoldY
        End Get
        Set(ByVal value As Integer)
            _LossGoldY = value
        End Set
    End Property
    Public Property LossGoldC() As Decimal
        Get
            LossGoldC = _LossGoldC
        End Get
        Set(ByVal value As Decimal)
            _LossGoldC = value
        End Set
    End Property

    Public Property AllTaxAmt() As Integer
        Get
            AllTaxAmt = _AllTaxAmt
        End Get
        Set(ByVal value As Integer)
            _AllTaxAmt = value
        End Set
    End Property
#End Region
End Class
