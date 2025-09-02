Public Class ItemNameInfo
#Region "Private Property"
    Private _ItemNameID As String
    Private _ItemCategoryID As String
    Private _ItemName As String
    Private _ItemPhoto As Object
    Private _LocationID As String

#End Region

#Region "Properties "
    Public Property ItemNameID() As String
        Get
            ItemNameID = _ItemNameID
        End Get
        Set(ByVal value As String)
            _ItemNameID = value
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
    Public Property ItemName() As String
        Get
            ItemName = _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
        End Set
    End Property
    Public Property ItemPhoto() As Object
        Get
            ItemPhoto = _ItemPhoto
        End Get
        Set(ByVal value As Object)
            _ItemPhoto = value
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
