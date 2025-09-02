Public Class ReturnAdvanceItemInfo
#Region "Private Property"
    Private _ReturnAdvanceItemID As String
    Private _ReturnAdvanceID As String
    Private _ItemTG As Decimal
    Private _Qty As Integer

    Private _SaleRate As Integer
    Private _Amount As Integer
    Private _Remark As String
    Private _IsUsed As Boolean

#End Region

#Region "Properties "
    Public Property ReturnAdvanceItemID() As String
        Get
            ReturnAdvanceItemID = _ReturnAdvanceItemID
        End Get
        Set(ByVal value As String)
            _ReturnAdvanceItemID = value
        End Set
    End Property
    Public Property ReturnAdvanceID() As String
        Get
            ReturnAdvanceID = _ReturnAdvanceID
        End Get
        Set(ByVal value As String)
            _ReturnAdvanceID = value
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
    Public Property SaleRate() As Integer
        Get
            SaleRate = _SaleRate
        End Get
        Set(ByVal value As Integer)
            _SaleRate = value
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
    Public Property Amount() As Integer
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
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
    Public Property IsUsed() As Boolean
        Get
            IsUsed = _IsUsed
        End Get
        Set(ByVal value As Boolean)
            _IsUsed = value
        End Set
    End Property
#End Region
End Class
