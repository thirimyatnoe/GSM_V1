Public Class SupplierInfo
#Region "Private Property"
    Private _SupplierID As String
    Private _SupplierCode As String
    Private _SupplierName As String
    Private _SupplierAddress As String
    Private _PhoneNo As String
    Private _Email As String
    Private _Website As String
    Private _Remark As String
    Private _LocationID As String

#End Region

#Region "Properties "
    Public Property SupplierID() As String
        Get
            SupplierID = _SupplierID
        End Get
        Set(ByVal value As String)
            _SupplierID = value
        End Set
    End Property
    Public Property SupplierCode() As String
        Get
            SupplierCode = _SupplierCode
        End Get
        Set(ByVal value As String)
            _SupplierCode = value
        End Set
    End Property
    Public Property SupplierName() As String
        Get
            SupplierName = _SupplierName
        End Get
        Set(ByVal value As String)
            _SupplierName = value
        End Set
    End Property
    Public Property SupplierAddress() As String
        Get
            SupplierAddress = _SupplierAddress
        End Get
        Set(ByVal value As String)
            _SupplierAddress = value
        End Set
    End Property
    Public Property PhoneNo() As String
        Get
            PhoneNo = _PhoneNo
        End Get
        Set(ByVal value As String)
            _PhoneNo = value
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

    Public Property Email() As String
        Get
            Email = _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
    Public Property Website() As String
        Get
            Website = _Website
        End Get
        Set(ByVal value As String)
            _Website = value
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
#End Region
End Class
