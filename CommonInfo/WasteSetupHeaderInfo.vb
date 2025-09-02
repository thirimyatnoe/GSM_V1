Public Class WasteSetupHeaderInfo
#Region "Private Property"
    Private _WasteSetupHeaderID As String
    Private _ItemCategoryID As String
    Private _ItemNameID As String
    Private _LocationID As String

#End Region
#Region "Properties "
    Public Property WasteSetupHeaderID() As String
        Get
            WasteSetupHeaderID = _WasteSetupHeaderID
        End Get
        Set(ByVal value As String)
            _WasteSetupHeaderID = value
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
    Public Property ItemNameID() As String
        Get
            ItemNameID = _ItemNameID
        End Get
        Set(ByVal value As String)
            _ItemNameID = value
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
