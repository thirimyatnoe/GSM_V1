Public Class RepairDetailInfo
#Region "Private Property"
    Private _RepairDetailID As String
    Private _RepairID As String
    Private _IsFromShop As Boolean
    Private _BarcodeNo As String
    Private _ItemCategoryID As String
    Private _ItemNameID As String
    Private _GoldQualityID As String
    Private _LengthOrWidth As String
    Private _CurrentPrice As Integer
    Private _Design As String
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal
    Private _IsExit As Boolean
    Private _DetailRemark As String
    Private _OrgItemK As Decimal
    Private _OrgItemP As Decimal
    Private _OrgItemY As Decimal

#End Region

#Region "Properties "
    Public Property RepairDetailID() As String
        Get
            RepairDetailID = _RepairDetailID
        End Get
        Set(ByVal value As String)
            _RepairDetailID = value
        End Set
    End Property

    Public Property RepairID() As String
        Get
            RepairID = _RepairID
        End Get
        Set(ByVal value As String)
            _RepairID = value
        End Set
    End Property

    Public Property IsFromShop() As Boolean
        Get
            IsFromShop = _IsFromShop
        End Get
        Set(ByVal value As Boolean)
            _IsFromShop = value
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

    Public Property GoldQualityID() As String
        Get
            GoldQualityID = _GoldQualityID
        End Get
        Set(ByVal value As String)
            _GoldQualityID = value
        End Set
    End Property

    Public Property LengthOrWidth() As String
        Get
            LengthOrWidth = _LengthOrWidth
        End Get
        Set(ByVal value As String)
            _LengthOrWidth = value
        End Set
    End Property

    Public Property CurrentPrice() As Integer
        Get
            CurrentPrice = _CurrentPrice
        End Get
        Set(ByVal value As Integer)
            _CurrentPrice = value
        End Set
    End Property

    Public Property Design() As String
        Get
            Design = _Design
        End Get
        Set(ByVal value As String)
            _Design = value
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
    Public Property OrgItemK() As Decimal
        Get
            OrgItemK = _OrgItemK
        End Get
        Set(ByVal value As Decimal)
            _OrgItemK = value
        End Set
    End Property
    Public Property OrgItemP() As Decimal
        Get
            OrgItemP = _OrgItemP
        End Get
        Set(ByVal value As Decimal)
            _OrgItemP = value
        End Set
    End Property
    Public Property OrgItemY() As Decimal
        Get
            OrgItemY = _OrgItemY
        End Get
        Set(ByVal value As Decimal)
            _OrgItemY = value
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
    Public Property DetailRemark() As String
        Get
            DetailRemark = _DetailRemark
        End Get
        Set(ByVal value As String)
            _DetailRemark = value
        End Set
    End Property
#End Region

End Class
