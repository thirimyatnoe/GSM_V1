Public Class GoldSmithInfo
#Region "Private Property"
    Private _GoldSmithID As String
    Private _GoldSmithCode As String
    Private _Name As String
    Private _Address As String
    Private _Phone As String
    Private _Remark As String
    Private _IsInactive As Boolean
    Private _LocationID As String
#End Region
#Region "Properties "
    Public Property GoldSmithID() As String
        Get
            GoldSmithID = _GoldSmithID
        End Get
        Set(ByVal value As String)
            _GoldSmithID = value
        End Set
    End Property
    Public Property GoldSmithCode() As String
        Get
            GoldSmithCode = _GoldSmithCode
        End Get
        Set(ByVal value As String)
            _GoldSmithCode = value
        End Set
    End Property
    Public Property Name() As String
        Get
            Name = _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property Address() As String
        Get
            Address = _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property
    Public Property Phone() As String
        Get
            Phone = _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
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
    Public Property IsInactive() As Boolean
        Get
            IsInactive = _IsInactive
        End Get
        Set(ByVal value As Boolean)
            _IsInactive = value
        End Set
    End Property
  
#End Region
End Class
