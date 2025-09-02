Public Class MortgageInvoiceItemInfo
#Region "Private Property"
    Private _MortgageItemID As String
    Private _MortgageInvoiceID As String
    Private _GoldQualityID As String
    Private _ItemCategoryID As String
    Private _ItemName As String
    Private _ItemNameID As String

    Private _QTY As Integer
    Private _GoldK As Integer
    Private _GoldP As Integer
    Private _GoldY As Integer
    Private _GoldC As Decimal
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _Amount As Integer
    Private _MortgageRate As Integer
    Private _IsDone As Boolean
    Private _SaleRate As Integer
    Private _DonePercent As String
    Private _IsPayback As Boolean
    Private _MortgageItemCode As String

#End Region

#Region "Properties "
    Public Property MortgageItemID() As String
        Get
            MortgageItemID = _MortgageItemID
        End Get
        Set(ByVal value As String)
            _MortgageItemID = value
        End Set
    End Property
    Public Property MortgageInvoiceID() As String
        Get
            MortgageInvoiceID = _MortgageInvoiceID
        End Get
        Set(ByVal value As String)
            _MortgageInvoiceID = value
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
    Public Property ItemNameID() As String
        Get
            ItemNameID = _ItemNameID
        End Get
        Set(ByVal value As String)
            _ItemNameID = value
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
    Public Property Amount() As Integer
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
        End Set
    End Property
    Public Property MortgageRate() As Integer
        Get
            MortgageRate = _MortgageRate
        End Get
        Set(ByVal value As Integer)
            _MortgageRate = value
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
    Public Property SaleRate() As Integer
        Get
            SaleRate = _SaleRate
        End Get
        Set(ByVal value As Integer)
            _SaleRate = value
        End Set
    End Property
    Public Property DonePercent() As String
        Get
            DonePercent = _DonePercent
        End Get
        Set(ByVal value As String)
            _DonePercent = value
        End Set
    End Property
    Public Property IsPayback() As Boolean
        Get
            IsPayback = _IsPayback
        End Get
        Set(ByVal value As Boolean)
            _IsPayback = value
        End Set
    End Property
    Public Property MortgageItemCode() As String
        Get
            MortgageItemCode = _MortgageItemCode
        End Get
        Set(ByVal value As String)
            _MortgageItemCode = value
        End Set
    End Property
#End Region
End Class
