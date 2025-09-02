Public Class PurchaseGemInfo
#Region "Private Property"
    Private _PurchaseGemID As String
    Private _PurchaseDetailID As String
    Private _GemsCategoryID As String
    Private _GemsName As String
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
    Private _GemP As Decimal
    Private _QTY As Integer
    Private _PurchaseRate As Decimal
    Private _FixType As String
    Private _Amount As Integer
    Private _IsOutGem As Boolean
    Private _Discount As Integer
#End Region

#Region "Properties "
    Public Property PurchaseGemID() As String
        Get
            PurchaseGemID = _PurchaseGemID
        End Get
        Set(ByVal value As String)
            _PurchaseGemID = value
        End Set
    End Property
    Public Property PurchaseDetailID() As String
        Get
            PurchaseDetailID = _PurchaseDetailID
        End Get
        Set(ByVal value As String)
            _PurchaseDetailID = value
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
    Public Property GemsName() As String
        Get
            GemsName = _GemsName
        End Get
        Set(ByVal value As String)
            _GemsName = value
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
    Public Property GemP() As Decimal
        Get
            GemP = _GemP
        End Get
        Set(ByVal value As Decimal)
            _GemP = value
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
    Public Property PurchaseRate() As Decimal
        Get
            PurchaseRate = _PurchaseRate
        End Get
        Set(ByVal value As Decimal)
            _PurchaseRate = value
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
    Public Property Amount() As Integer
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
        End Set
    End Property
    Public Property IsOutGem() As Boolean
        Get
            IsOutGem = _IsOutGem
        End Get
        Set(ByVal value As Boolean)
            _IsOutGem = value
        End Set
    End Property
    Public Property Discount() As Integer
        Get
            Discount = _Discount
        End Get
        Set(ByVal value As Integer)
            _Discount = value
        End Set
    End Property
#End Region
End Class
