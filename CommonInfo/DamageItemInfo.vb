Public Class DamageItemInfo
#Region "Private Property"
    Private _DamageItemID As String
    Private _DamageID As String
    Private _ForSaleID As String
    Private _IsReAdd As Integer
    Private _Remark As String
    Private _ReAddDate As Date

#End Region

#Region "Properties "
    Public Property DamageItemID() As String
        Get
            DamageItemID = _DamageItemID
        End Get
        Set(ByVal value As String)
            _DamageItemID = value
        End Set
    End Property
    Public Property DamageID() As String
        Get
            DamageID = _DamageID
        End Get
        Set(ByVal value As String)
            _DamageID = value
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
    Public Property IsReAdd() As String
        Get
            IsReAdd = _IsReAdd
        End Get
        Set(ByVal value As String)
            _IsReAdd = value
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

    Public Property ReAddDate() As String
        Get
            ReAddDate = _ReAddDate
        End Get
        Set(ByVal value As String)
            _ReAddDate = value
        End Set
    End Property

#End Region
End Class
