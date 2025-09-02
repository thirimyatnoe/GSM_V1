Public Class CustomerInfo
#Region "Private Property"
    Private _CustomerID As String
    Private _CustomerCode As String
    Private _CustomerName As String
    Private _CustomerAddress As String
    Private _CustomerTel As String
    Private _Remark As String
    Private _IsInactive As Boolean
    Private _DOB As Date
    Private _LocationID As String
    Private _NRC As String
    Private _MemberCode As String
#End Region

#Region "Properties "
    Public Property CustomerID() As String
        Get
            CustomerID = _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property
    Public Property CustomerCode() As String
        Get
            CustomerCode = _CustomerCode
        End Get
        Set(ByVal value As String)
            _CustomerCode = value
        End Set
    End Property
    Public Property CustomerName() As String
        Get
            CustomerName = _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property
    Public Property CustomerAddress() As String
        Get
            CustomerAddress = _CustomerAddress
        End Get
        Set(ByVal value As String)
            _CustomerAddress = value
        End Set
    End Property
    Public Property CustomerTel() As String
        Get
            CustomerTel = _CustomerTel
        End Get
        Set(ByVal value As String)
            _CustomerTel = value
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
    Public Property DOB() As Date
        Get
            DOB = _DOB
        End Get
        Set(ByVal value As Date)
            _DOB = value
        End Set
    End Property
    Public Property LocationID() As String
        Get
            LocationID = _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property
    Public Property NRC() As String
        Get
            NRC = _NRC
        End Get
        Set(ByVal value As String)
            _NRC = value
        End Set
    End Property
    Public Property MemberCode() As String
        Get
            MemberCode = _MemberCode
        End Get
        Set(ByVal value As String)
            _MemberCode = value
        End Set
    End Property

#End Region
End Class
