Public Class RepairReturnDetailInfo

#Region "Private Property"
    Private _ReturnRepairDetailID As String
    Private _ReturnRepairID As String
    Private _RepairDetailID As String
    Private _ChangeSaleRate As Integer
    Private _ReturnItemTK As Decimal
    Private _ReturnItemTG As Decimal
    Private _ReturnGoldTK As Decimal
    Private _ReturnGoldTG As Decimal
    Private _OrgGoldTK As Decimal
    Private _OrgGoldTG As Decimal
    Private _OrgGemTK As Decimal
    Private _OrgGemTG As Decimal
    Private _ReturnGemTK As Decimal
    Private _ReturnGemTG As Decimal
    Private _WasteTK As Decimal
    Private _WasteTG As Decimal
    Private _DesignCharges As Integer
    Private _PlatingCharges As Integer
    Private _MountingCharges As Integer
    Private _WhiteCharges As Integer
    Private _ReturnGoldPrice As Integer
    Private _ReturnGemPrice As Integer
    Private _ReturnTotalAmount As Integer
    Private _ReturnAddOrSub As Integer
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal
    Private _ItemAmount As Integer
#End Region

#Region "Properties "

    Public Property ReturnRepairDetailID() As String
        Get
            ReturnRepairDetailID = _ReturnRepairDetailID
        End Get
        Set(ByVal value As String)
            _ReturnRepairDetailID = value
        End Set
    End Property
    Public Property ReturnRepairID() As String
        Get
            ReturnRepairID = _ReturnRepairID
        End Get
        Set(ByVal value As String)
            _ReturnRepairID = value
        End Set
    End Property

    Public Property RepairDetailID() As String
        Get
            RepairDetailID = _RepairDetailID
        End Get
        Set(ByVal value As String)
            _RepairDetailID = value
        End Set
    End Property

    Public Property ChangeSaleRate() As Integer
        Get
            ChangeSaleRate = _ChangeSaleRate
        End Get
        Set(ByVal value As Integer)
            _ChangeSaleRate = value
        End Set
    End Property




    Public Property ReturnItemTK() As Decimal
        Get
            ReturnItemTK = _ReturnItemTK
        End Get
        Set(ByVal value As Decimal)
            _ReturnItemTK = value
        End Set
    End Property

    Public Property ReturnItemTG() As Decimal
        Get
            ReturnItemTG = _ReturnItemTG
        End Get
        Set(ByVal value As Decimal)
            _ReturnItemTG = value
        End Set
    End Property

    Public Property ReturnGoldTK() As Decimal
        Get
            ReturnGoldTK = _ReturnGoldTK
        End Get
        Set(ByVal value As Decimal)
            _ReturnGoldTK = value
        End Set
    End Property

    Public Property ReturnGoldTG() As Decimal
        Get
            ReturnGoldTG = _ReturnGoldTG
        End Get
        Set(ByVal value As Decimal)
            _ReturnGoldTG = value
        End Set
    End Property
    Public Property OrgGoldTK() As Decimal
        Get
            OrgGoldTK = _OrgGoldTK
        End Get
        Set(ByVal value As Decimal)
            _OrgGoldTK = value
        End Set
    End Property

    Public Property OrgGoldTG() As Decimal
        Get
            OrgGoldTG = _OrgGoldTG
        End Get
        Set(ByVal value As Decimal)
            _OrgGoldTG = value
        End Set
    End Property
    Public Property OrgGemTK() As Decimal
        Get
            OrgGemTK = _OrgGemTK
        End Get
        Set(ByVal value As Decimal)
            _OrgGemTK = value
        End Set
    End Property

    Public Property OrgGemTG() As Decimal
        Get
            OrgGemTG = _OrgGemTG
        End Get
        Set(ByVal value As Decimal)
            _OrgGemTG = value
        End Set
    End Property
    Public Property ReturnGemTK() As Decimal
        Get
            ReturnGemTK = _ReturnGemTK
        End Get
        Set(ByVal value As Decimal)
            _ReturnGemTK = value
        End Set
    End Property

    Public Property ReturnGemTG() As Decimal
        Get
            ReturnGemTG = _ReturnGemTG
        End Get
        Set(ByVal value As Decimal)
            _ReturnGemTG = value
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

    Public Property DesignCharges() As Integer
        Get
            DesignCharges = _DesignCharges
        End Get
        Set(ByVal value As Integer)
            _DesignCharges = value
        End Set
    End Property
    Public Property WhiteCharges() As Integer
        Get
            WhiteCharges = _WhiteCharges
        End Get
        Set(ByVal value As Integer)
            _WhiteCharges = value
        End Set
    End Property
    Public Property PlatingCharges() As Integer
        Get
            PlatingCharges = _PlatingCharges
        End Get
        Set(ByVal value As Integer)
            _PlatingCharges = value
        End Set
    End Property

    Public Property MountingCharges() As Integer
        Get
            MountingCharges = _MountingCharges
        End Get
        Set(ByVal value As Integer)
            _MountingCharges = value
        End Set
    End Property
    Public Property ReturnGoldPrice() As Integer
        Get
            ReturnGoldPrice = _ReturnGoldPrice
        End Get
        Set(ByVal value As Integer)
            _ReturnGoldPrice = value
        End Set
    End Property

    Public Property ReturnGemPrice() As Integer
        Get
            ReturnGemPrice = _ReturnGemPrice
        End Get
        Set(ByVal value As Integer)
            _ReturnGemPrice = value
        End Set
    End Property
    Public Property ReturnTotalAmount() As Integer
        Get
            ReturnTotalAmount = _ReturnTotalAmount
        End Get
        Set(ByVal value As Integer)
            _ReturnTotalAmount = value
        End Set
    End Property

    Public Property ReturnAddOrSub() As Integer
        Get
            ReturnAddOrSub = _ReturnAddOrSub
        End Get
        Set(ByVal value As Integer)
            _ReturnAddOrSub = value
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
    Public Property ItemAmount() As Integer
        Get
            ItemAmount = _ItemAmount
        End Get
        Set(ByVal value As Integer)
            _ItemAmount = value
        End Set
    End Property
#End Region
End Class
