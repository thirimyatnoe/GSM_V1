Public Class VoucherSettingInfo
#Region "Private Property"
    Private _TitleType As String
    Private _Title As String
    Private _FontName As String
    Private _Bold As Boolean
    Private _Italic As Boolean
    Private _FontSize As Integer
    Private _FontColor As String
    Private _FontR As Integer
    Private _FontG As Integer
    Private _FontB As Integer
    Private _Photo As String
#End Region

#Region "Properties "
    Public Property TitleType() As String
        Get
            TitleType = _TitleType
        End Get
        Set(ByVal value As String)
            _TitleType = value
        End Set
    End Property
    Public Property Title() As String
        Get
            Title = _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property
    Public Property FontName() As String
        Get
            FontName = _FontName
        End Get
        Set(ByVal value As String)
            _FontName = value
        End Set
    End Property
    Public Property Bold() As Boolean
        Get
            Bold = _Bold
        End Get
        Set(ByVal value As Boolean)
            _Bold = value
        End Set
    End Property

    Public Property Italic() As Boolean
        Get
            Italic = _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value
        End Set
    End Property
    Public Property FontSize() As Integer
        Get
            FontSize = _FontSize
        End Get
        Set(ByVal value As Integer)
            _FontSize = value
        End Set
    End Property

    Public Property FontColor() As String
        Get
            FontColor = _FontColor
        End Get
        Set(ByVal value As String)
            _FontColor = value
        End Set
    End Property
    Public Property Photo() As String
        Get
            Photo = _Photo
        End Get
        Set(ByVal value As String)
            _Photo = value
        End Set
    End Property
    Public Property FontR() As Byte
        Get
            FontR = _FontR
        End Get
        Set(ByVal value As Byte)
            _FontR = value
        End Set
    End Property
    Public Property FontG() As Byte
        Get
            FontG = _FontG
        End Get
        Set(ByVal value As Byte)
            _FontG = value
        End Set
    End Property
    Public Property FontB() As Byte
        Get
            FontB = _FontB
        End Get
        Set(ByVal value As Byte)
            _FontB = value
        End Set
    End Property
#End Region
End Class
