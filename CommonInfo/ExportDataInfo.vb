
Public Class ExportDataInfo

    Private _ExportID As Integer
    Private _LocationID As String
    Private _OtherLocationID As String
    Private _LocationName As String
    Private _OtherLocationName As String
    Private _TransactionType As String
    Private _AllUse As Boolean
    Private _ModifiedDate As Date
    Private _CompanyName As String
    Private _ToMail As String
    Private _CCMail As String

    Public Property ExportID() As Integer
        Get
            Return _ExportID

        End Get
        Set(ByVal value As Integer)
            _ExportID = value

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
    Public Property OtherLocationID() As String
        Get
            Return _OtherLocationID

        End Get
        Set(ByVal value As String)
            _OtherLocationID = value

        End Set
    End Property

    Public Property LocationName() As String
        Get
            Return _LocationName

        End Get
        Set(ByVal value As String)
            _LocationName = value

        End Set
    End Property
    Public Property OtherLocationName() As String
        Get
            Return _OtherLocationName

        End Get
        Set(ByVal value As String)
            _OtherLocationName = value

        End Set
    End Property
    Public Property TransactionType() As String
        Get
            Return _TransactionType

        End Get
        Set(ByVal value As String)
            _TransactionType = value

        End Set
    End Property
    Public Property AllUse() As Boolean
        Get
            Return _AllUse

        End Get
        Set(ByVal value As Boolean)
            _AllUse = value

        End Set
    End Property
    Public Property ModifiedDate() As Date
        Get
            Return _ModifiedDate

        End Get
        Set(ByVal value As Date)
            _ModifiedDate = value

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
    Public Property ToMail() As String
        Get
            Return _ToMail

        End Get
        Set(ByVal value As String)
            _ToMail = value

        End Set
    End Property
    Public Property CCMail() As String
        Get
            Return _CCMail

        End Get
        Set(ByVal value As String)
            _CCMail = value

        End Set
    End Property
End Class
