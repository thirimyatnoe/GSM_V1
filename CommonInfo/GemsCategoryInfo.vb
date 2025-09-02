Public Class GemsCategoryInfo
#Region "Private Property"
    Private _GemsCategoryID As String
    Private _GemsCategory As String
    Private _StoneType As String
    Private _GemTaxPer As Decimal
    Private _LocationID As String
    Private _Prefix As String

#End Region

#Region "Properties "
    Public Property GemsCategoryID() As String
        Get
            GemsCategoryID = _GemsCategoryID
        End Get
        Set(ByVal value As String)
            _GemsCategoryID = value
        End Set
    End Property
    Public Property GemsCategory() As String
        Get
            GemsCategory = _GemsCategory
        End Get
        Set(ByVal value As String)
            _GemsCategory = value
        End Set
    End Property
    Public Property StoneType() As String
        Get
            StoneType = _StoneType
        End Get
        Set(ByVal value As String)
            _StoneType = value
        End Set
    End Property

    Public Property GemTaxPer() As Decimal
        Get
            GemTaxPer = _GemTaxPer
        End Get
        Set(ByVal value As Decimal)
            _GemTaxPer = value
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
    Public Property Prefix() As String
        Get
            Prefix = _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property
#End Region
End Class
