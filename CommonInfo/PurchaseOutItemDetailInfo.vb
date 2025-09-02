Public Class PurchaseOutItemDetailInfo
#Region "Private Property"
    Private _PurchaseOutDetailID As String
    Private _PurchaseOutID As String
    Private _PurchaseDetailID As String
    Private _ItemCategoryID As String
    Private _ItemNameID As String
    Private _DivideType As String
    Private _GemsCategoryID As String
    Private _GemsName As String
    Private _PurchaseGemID As String
    Private _QTY As Integer
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    Private _GemTW As Decimal
    Private _YOrCOrG As String

#End Region

#Region "Properties "
    Public Property PurchaseOutDetailID() As String
        Get
            PurchaseOutDetailID = _PurchaseOutDetailID
        End Get
        Set(ByVal value As String)
            _PurchaseOutDetailID = value
        End Set
    End Property
    Public Property PurchaseOutID() As String
        Get
            PurchaseOutID = _PurchaseOutID
        End Get
        Set(ByVal value As String)
            _PurchaseOutID = value
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
    Public Property DivideType() As String
        Get
            DivideType = _DivideType
        End Get
        Set(ByVal value As String)
            _DivideType = value
        End Set
    End Property
    Public Property PurchaseGemID() As String
        Get
            PurchaseGemID = _PurchaseGemID
        End Get
        Set(ByVal value As String)
            _PurchaseGemID = value
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

    Public Property QTY() As Integer
        Get
            QTY = _QTY
        End Get
        Set(ByVal value As Integer)
            _QTY = value
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
#End Region
End Class
