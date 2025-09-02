Public Class LocationInfo
    Private _LocationID As String
    Private _Location As String
    Private _Address As String
    Private _Phone As String
    Private _Remark15 As String
    Private _RemarkDone As String
    Private _CurrentLocationID As String
    Private _IsHeadOffice As Boolean

    Public Property LocationID() As String
        Get
            Return _LocationID

        End Get
        Set(ByVal value As String)
            _LocationID = value

        End Set
    End Property

    Public Property Location() As String
        Get
            Return _Location

        End Get
        Set(ByVal value As String)
            _Location = value

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

    Public Property Phone() As String
        Get
            Return _Phone

        End Get
        Set(ByVal value As String)
            _Phone = value

        End Set
    End Property
    Public Property Remark15() As String
        Get
            Return _Remark15

        End Get
        Set(ByVal value As String)
            _Remark15 = value

        End Set
    End Property

    Public Property RemarkDone() As String
        Get
            Return _RemarkDone

        End Get
        Set(ByVal value As String)
            _RemarkDone = value

        End Set
    End Property
    Public Property CurrentLocationID() As String
        Get
            Return _CurrentLocationID

        End Get
        Set(ByVal value As String)
            _CurrentLocationID = value

        End Set
    End Property
    Public Property IsHeadOffice() As Boolean
        Get
            Return _IsHeadOffice

        End Get
        Set(ByVal value As Boolean)
            _IsHeadOffice = value

        End Set
    End Property

    End Class
