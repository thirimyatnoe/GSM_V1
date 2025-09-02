Public Class CashTypeInfo
#Region "Private Property"
    Private _CashTypeID As String
    Private _CashType As String
    Private _LocationID As String

#End Region
#Region "Properties"
    Public Property CashTypeID() As String
        Get
            Return _CashTypeID

        End Get
        Set(ByVal value As String)
            _CashTypeID = value
        End Set
    End Property
    Public Property CashType() As String
        Get
            Return _CashType

        End Get
        Set(ByVal value As String)
            _CashType = value
        End Set
    End Property
    Public Property LocationID() As String
        Get
            Return _LocationID

        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property

#End Region

End Class
