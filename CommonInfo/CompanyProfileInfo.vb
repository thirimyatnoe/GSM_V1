Public Class CompanyProfileInfo
#Region "Private Methods"
    Private _CompanyID As String
    Private _CompanyName As String
    Private _CompanyLogo As Object
    Private _Telephone As String
    Private _Email As String
    Private _Address As String
    Private _WebSite As String
    Private _Fax As String
    Private _IsHeadOffice As Boolean
    Private _ShortText As String

#End Region

#Region "Properties"

    Public Property ShortText() As String
        Get
            Return _ShortText
        End Get
        Set(ByVal value As String)
            _ShortText = value
        End Set
    End Property
    Public Property CompanyID() As String
        Get
            Return _CompanyID
        End Get
        Set(ByVal value As String)
            _CompanyID = value
        End Set
    End Property
    Public Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
        Set(ByVal value As String)
            _CompanyName = value
        End Set
    End Property

    Public Property CompanyLogo() As Object
        Get
            Return _CompanyLogo

        End Get
        Set(ByVal value As Object)
            _CompanyLogo = value
        End Set
    End Property

    Public Property Telephone() As String
        Get
            Return _Telephone

        End Get
        Set(ByVal value As String)
            _Telephone = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email

        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address

        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property
    Public Property WebSite() As String
        Get
            Return _WebSite

        End Get
        Set(ByVal value As String)
            _WebSite = value
        End Set
    End Property

    Public Property Fax() As String
        Get
            Return _Fax
        End Get
        Set(ByVal value As String)
            _Fax = value
        End Set
    End Property

    Public Property HO() As Boolean
        Get
            Return _IsHeadOffice
        End Get
        Set(ByVal value As Boolean)
            _IsHeadOffice = value
        End Set
    End Property
#End Region
End Class
