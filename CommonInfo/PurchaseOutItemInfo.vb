Public Class PurchaseOutItemInfo
#Region "Private Property"
    Private _PurchaseOutID As String
    Private _OutDate As Date
    Private _StaffID As String
    Private _PurchaseHeaderID As String
    Private _Remark As String
#End Region

#Region "Properties "
    Public Property PurchaseOutID() As String
        Get
            PurchaseOutID = _PurchaseOutID
        End Get
        Set(ByVal value As String)
            _PurchaseOutID = value
        End Set
    End Property
    Public Property OutDate() As Date
        Get
            OutDate = _OutDate
        End Get
        Set(ByVal value As Date)
            _OutDate = value
        End Set
    End Property
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
        End Set
    End Property
    Public Property PurchaseHeaderID() As String
        Get
            PurchaseHeaderID = _PurchaseHeaderID
        End Get
        Set(ByVal value As String)
            _PurchaseHeaderID = value
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
#End Region
End Class
