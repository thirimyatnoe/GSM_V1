Public Class SaleGemsItemInfo
#Region "Private Property"
    Private _SaleGemsItemID As String
    Private _SaleGemsID As String
    Private _GemsCategoryID As String
    Private _GemsCategory As String
    Private _GemsName As String
    Private _Clarity As String
    Private _SizeMM As String
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    Private _YOrCOrG As String
    Private _GemY As Integer
    Private _GemBCG As Decimal
    Private _GemTW As Decimal
    Private _GemTK As Decimal
    Private _Qty As Integer
    Private _FixType As String
    Private _SaleRate As Integer
    Private _Amount As Integer
    Private _BalanceQTY As Integer
    Private _BalanceGemsTK As Decimal
    Private _BalanceGemsTG As Decimal
    Private _BalanceGemsK As Integer
    Private _BalanceGemsP As Integer
    Private _BalanceGemsY As Decimal
    Private _GemsStockID As String
    Private _BarcodeNo As String
    Private _Carat As Decimal
    Private _WeightR As Integer
    Private _WeightB As Integer
    Private _WeightP As Decimal
    Private _GemsTW As Decimal
    Private _IsReturn As Boolean

#End Region

#Region "Properties "
    Public Property SaleGemsItemID() As String
        Get
            SaleGemsItemID = _SaleGemsItemID
        End Get
        Set(ByVal value As String)
            _SaleGemsItemID = value
        End Set
    End Property
    Public Property SaleGemsID() As String
        Get
            SaleGemsID = _SaleGemsID
        End Get
        Set(ByVal value As String)
            _SaleGemsID = value
        End Set
    End Property
    Public Property GemsCategoryID() As String
        Get
            GemsCategoryID = _GemsCategoryID
        End Get
        Set(ByVal value As String)
            _GemsCategoryID = value
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
    Public Property GemsName() As String
        Get
            GemsName = _GemsName
        End Get
        Set(ByVal value As String)
            _GemsName = value
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
    Public Property SizeMM() As String
        Get
            SizeMM = _SizeMM
        End Get
        Set(ByVal value As String)
            _SizeMM = value
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
    Public Property YOrCOrG() As String
        Get
            YOrCOrG = _YOrCOrG
        End Get
        Set(ByVal value As String)
            _YOrCOrG = value
        End Set
    End Property
    Public Property GemY() As Integer
        Get
            GemY = _GemY
        End Get
        Set(ByVal value As Integer)
            _GemY = value
        End Set
    End Property
    Public Property GemBCG() As Decimal
        Get
            GemBCG = _GemBCG
        End Get
        Set(ByVal value As Decimal)
            _GemBCG = value
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
    Public Property GemTK() As Decimal
        Get
            GemTK = _GemTK
        End Get
        Set(ByVal value As Decimal)
            _GemTK = value
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
    Public Property FixType() As String
        Get
            FixType = _FixType
        End Get
        Set(ByVal value As String)
            _FixType = value
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
    Public Property Amount() As Integer
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
        End Set
    End Property

    Public Property BalanceQTY() As Integer
        Get
            BalanceQTY = _BalanceQTY
        End Get
        Set(ByVal value As Integer)
            _BalanceQTY = value
        End Set
    End Property

    Public Property BalanceGemsTK() As Decimal
        Get
            BalanceGemsTK = _BalanceGemsTK
        End Get
        Set(ByVal value As Decimal)
            _BalanceGemsTK = value
        End Set
    End Property

    Public Property BalanceGemsTG() As Decimal
        Get
            BalanceGemsTG = _BalanceGemsTG
        End Get
        Set(ByVal value As Decimal)
            _BalanceGemsTG = value
        End Set
    End Property

    Public Property BalanceGemsK() As Integer
        Get
            BalanceGemsK = _BalanceGemsK
        End Get
        Set(ByVal value As Integer)
            _BalanceGemsK = value
        End Set
    End Property
    Public Property BalanceGemsP() As Integer
        Get
            BalanceGemsP = _BalanceGemsP
        End Get
        Set(ByVal value As Integer)
            _BalanceGemsP = value
        End Set
    End Property
    Public Property BalanceGemsY() As Decimal
        Get
            BalanceGemsY = _BalanceGemsY
        End Get
        Set(ByVal value As Decimal)
            _BalanceGemsY = value
        End Set
    End Property

    Public Property GemsStockID() As String
        Get
            GemsStockID = _GemsStockID
        End Get
        Set(ByVal value As String)
            _GemsStockID = value
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

    Public Property Carat() As Decimal
        Get
            Carat = _Carat
        End Get
        Set(ByVal value As Decimal)
            _Carat = value
        End Set
    End Property
    Public Property WeightR() As Integer
        Get
            WeightR = _WeightR
        End Get
        Set(ByVal value As Integer)
            _WeightR = value
        End Set
    End Property
    Public Property WeightB() As Integer
        Get
            WeightB = _WeightB
        End Get
        Set(ByVal value As Integer)
            _WeightB = value
        End Set
    End Property
    Public Property WeightP() As Decimal
        Get
            WeightP = _WeightP
        End Get
        Set(ByVal value As Decimal)
            _WeightP = value
        End Set
    End Property
    Public Property GemsTW() As Decimal
        Get
            GemsTW = _GemsTW
        End Get
        Set(ByVal value As Decimal)
            _GemsTW = value
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


#End Region
End Class
