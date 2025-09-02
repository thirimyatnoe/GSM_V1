Public Class BranchInfo
    Private _BranchID As String
    Private _Branch As String
    Private _Address As String
    Private _Phone As String
    Private _Remark15 As String
    Private _RemarkDone As String


    Public Property BranchID() As String
        Get
            Return _BranchID

        End Get
        Set(ByVal value As String)
            _BranchID = value

        End Set
    End Property

    Public Property Branch() As String
        Get
            Return _Branch

        End Get
        Set(ByVal value As String)
            _Branch = value

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
End Class
