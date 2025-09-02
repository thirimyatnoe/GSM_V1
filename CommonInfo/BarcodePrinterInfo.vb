Public Class BarcodePrinterInfo
    Private _PaperWidth As String
    Private _PaperHeight As String
    Private _Height As String
    Private _PrinterName As String
    Private _Narrow As String
    Private _Wide As String
    Private _X As String
    Private _Y As String
    Private _IsLocationName As Boolean
    Private _EngName As String
    Private _MMName As String
    Private _RightPositionX As String
    Private _IsIncludeGemWgt As Boolean
    Private _IsIncludeGemPrice As Boolean
    Private _IsPrefix As Boolean
    Private _IsLength As Boolean
    Private _IsFixGold As Boolean
    Private _IsFixItem As Boolean
    Private _IsFixGemQTY As Boolean
    Private _IsFixGemWeight As Boolean
    Private _IsAllDetail As Boolean
    Private _IsPriceCode As Boolean
    Private _IsOriginalCode As Boolean
    Private _IsDescription As Boolean
    Private _IsWaste As Boolean
    Private _IsShowGram As Boolean
    Private _IsShowGW As Boolean
    Private _IsFixPrice As Boolean
    Private _LeftFontSize As Integer
    Private _RightFontSize As Integer

    Public Property PaperWidth() As String
        Get
            Return _PaperWidth
        End Get
        Set(ByVal value As String)
            _PaperWidth = value
        End Set
    End Property
    Public Property PaperHeight() As String
        Get
            Return _PaperHeight
        End Get
        Set(ByVal value As String)
            _PaperHeight = value
        End Set
    End Property
    Public Property Height() As String
        Get
            Return _Height
        End Get
        Set(ByVal value As String)
            _Height = value
        End Set
    End Property
    Public Property PrinterName() As String
        Get
            Return _PrinterName
        End Get
        Set(ByVal value As String)
            _PrinterName = value
        End Set
    End Property
    Public Property Narrow() As String
        Get
            Return _Narrow
        End Get
        Set(ByVal value As String)
            _Narrow = value
        End Set
    End Property
    Public Property Wide() As String
        Get
            Return _Wide
        End Get
        Set(ByVal value As String)
            _Wide = value
        End Set
    End Property
    Public Property X() As String
        Get
            Return _X
        End Get
        Set(ByVal value As String)
            _X = value
        End Set
    End Property
    Public Property Y() As String
        Get
            Return _Y
        End Get
        Set(ByVal value As String)
            _Y = value
        End Set
    End Property

    Public Property IsLocationName() As Boolean
        Get
            Return _IsLocationName
        End Get
        Set(ByVal value As Boolean)
            _IsLocationName = value
        End Set
    End Property

    Public Property EngName() As String
        Get
            Return _EngName
        End Get
        Set(ByVal value As String)
            _EngName = value
        End Set
    End Property
    Public Property MMName() As String
        Get
            Return _MMName
        End Get
        Set(ByVal value As String)
            _MMName = value
        End Set
    End Property
    Public Property RightPositionX() As String
        Get
            Return _RightPositionX
        End Get
        Set(ByVal value As String)
            _RightPositionX = value
        End Set
    End Property

    Public Property IsIncludeGemWgt() As Boolean
        Get
            Return _IsIncludeGemWgt
        End Get
        Set(ByVal value As Boolean)
            _IsIncludeGemWgt = value
        End Set
    End Property
    Public Property IsPrefix() As Boolean
        Get
            Return _IsPrefix
        End Get
        Set(ByVal value As Boolean)
            _IsPrefix = value
        End Set
    End Property
    Public Property IsLength() As Boolean
        Get
            Return _IsLength
        End Get
        Set(ByVal value As Boolean)
            _IsLength = value
        End Set
    End Property
    Public Property IsFixGold() As Boolean
        Get
            Return _IsFixGold
        End Get
        Set(ByVal value As Boolean)
            _IsFixGold = value
        End Set
    End Property
    Public Property IsIncludeGemPrice() As Boolean
        Get
            Return _IsIncludeGemPrice
        End Get
        Set(ByVal value As Boolean)
            _IsIncludeGemPrice = value
        End Set
    End Property
    Public Property IsFixItem() As Boolean
        Get
            Return _IsFixItem
        End Get
        Set(ByVal value As Boolean)
            _IsFixItem = value
        End Set
    End Property
    Public Property IsFixGemQTY() As Boolean
        Get
            Return _IsFixGemQTY
        End Get
        Set(ByVal value As Boolean)
            _IsFixGemQTY = value
        End Set
    End Property
    Public Property IsFixGemWeight() As Boolean
        Get
            Return _IsFixGemWeight
        End Get
        Set(ByVal value As Boolean)
            _IsFixGemWeight = value
        End Set
    End Property
    Public Property IsAllDetail() As Boolean
        Get
            Return _IsAllDetail
        End Get
        Set(ByVal value As Boolean)
            _IsAllDetail = value
        End Set
    End Property
    Public Property IsPriceCode() As Boolean
        Get
            Return _IsPriceCode
        End Get
        Set(ByVal value As Boolean)
            _IsPriceCode = value
        End Set
    End Property

    Public Property IsOriginalCode() As Boolean
        Get
            Return _IsOriginalCode
        End Get
        Set(ByVal value As Boolean)
            _IsOriginalCode = value
        End Set
    End Property
    Public Property IsDescription() As Boolean
        Get
            Return _IsDescription
        End Get
        Set(ByVal value As Boolean)
            _IsDescription = value
        End Set
    End Property
    Public Property IsWaste() As Boolean
        Get
            Return _IsWaste
        End Get
        Set(ByVal value As Boolean)
            _IsWaste = value
        End Set
    End Property
    Public Property IsShowGram() As Boolean
        Get
            Return _IsShowGram
        End Get
        Set(ByVal value As Boolean)
            _IsShowGram = value
        End Set
    End Property
    Public Property IsFixPrice() As Boolean
        Get
            Return _IsFixPrice
        End Get
        Set(ByVal value As Boolean)
            _IsFixPrice = value
        End Set
    End Property
    Public Property IsShowGW() As Boolean
        Get
            Return _IsShowGW
        End Get
        Set(ByVal value As Boolean)
            _IsShowGW = value
        End Set
    End Property
    Public Property LeftFontSize() As Integer
        Get
            Return _LeftFontSize
        End Get
        Set(ByVal value As Integer)
            _LeftFontSize = value
        End Set
    End Property
    Public Property RightFontSize() As Integer
        Get
            Return _RightFontSize
        End Get
        Set(ByVal value As Integer)
            _RightFontSize = value
        End Set
    End Property

End Class
