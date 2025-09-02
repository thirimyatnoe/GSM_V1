Public Class GenerateFormatInfo
#Region "Private Property"

    Private _GenerateFormatID As Integer
    Private _GenerateFormat As String
    Private _Prefix As String
    Private _FormatDate1 As String
    Private _FormatDate2 As String
    Private _Prefix2 As String
    Private _NumberCount As String
    Private _PrefixPlace As String
    Private _IsGenerate As Boolean
#End Region

#Region "Properties"
    
    Public Property GenerateFormatID() As Integer
        Get
            GenerateFormatID = _GenerateFormatID
        End Get
        Set(ByVal value As Integer)
            _GenerateFormatID = value
        End Set
    End Property

    Public Property GenerateFormat() As String
        Get
            GenerateFormat = _GenerateFormat
        End Get
        Set(ByVal value As String)
            _GenerateFormat = value
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

   

    Public Property FormatDate1() As String
        Get
            FormatDate1 = _FormatDate1
        End Get
        Set(ByVal value As String)
            _FormatDate1 = value
        End Set
    End Property

    Public Property FormatDate2() As String
        Get
            FormatDate2 = _FormatDate2
        End Get
        Set(ByVal value As String)
            _FormatDate2 = value
        End Set
    End Property

    Public Property Prefix2() As String
        Get
            Prefix2 = _Prefix2
        End Get
        Set(ByVal value As String)
            _Prefix2 = value
        End Set
    End Property

    Public Property NumberCount() As String
        Get
            NumberCount = _NumberCount
        End Get
        Set(ByVal value As String)
            _NumberCount = value
        End Set
    End Property

    Public Property PrefixPlace() As String
        Get
            PrefixPlace = _PrefixPlace
        End Get
        Set(value As String)
            _PrefixPlace = value
        End Set
    End Property
    Public Property IsGenerate() As Boolean
        Get
            IsGenerate = _IsGenerate
        End Get
        Set(value As Boolean)
            _IsGenerate = value
        End Set
    End Property

  

#End Region
End Class
