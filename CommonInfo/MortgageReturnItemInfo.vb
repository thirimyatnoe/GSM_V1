Public Class MortgageReturnItemInfo
#Region "Private Property"
    Private _MortgageReturnItemID As String
    Private _MortgageReturnID As String
    Private _MortgageItemID As String
    Private _GoldQualityID As String
    Private _ItemCategoryID As String
    Private _ItemName As String
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _Amount As Integer
    Private _IsDone As Boolean
    Private _DonePercent As Integer



#End Region

#Region "Properties "
    Public Property MortgageReturnItemID() As String
        Get
            MortgageReturnItemID = _MortgageReturnItemID
        End Get
        Set(ByVal value As String)
            _MortgageReturnItemID = value
        End Set
    End Property
    Public Property MortgageReturnID() As String
        Get
            MortgageReturnID = _MortgageReturnID
        End Get
        Set(ByVal value As String)
            _MortgageReturnID = value
        End Set
    End Property
    Public Property MortgageItemID() As String
        Get
            MortgageItemID = _MortgageItemID
        End Get
        Set(ByVal value As String)
            _MortgageItemID = value
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

    Public Property IsDone() As Boolean
        Get
            IsDone = _IsDone
        End Get
        Set(ByVal value As Boolean)
            _IsDone = value
        End Set
    End Property

    Public Property DonePercent() As Integer
        Get
            DonePercent = _DonePercent
        End Get
        Set(ByVal value As Integer)
            _DonePercent = value
        End Set
    End Property

#End Region
End Class
