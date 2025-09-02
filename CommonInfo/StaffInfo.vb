Public Class StaffInfo
#Region "Private Property"
    Private _StaffID As String
    Private _Staff As String
    Private _LocationID As String


#End Region

#Region "Properties "
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
        End Set
    End Property
    Public Property Staff() As String
        Get
            Staff = _Staff
        End Get
        Set(ByVal value As String)
            _Staff = value
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
