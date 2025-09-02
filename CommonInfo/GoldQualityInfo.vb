Public Class GoldQualityInfo
    Private _GoldQualityID As String
    Private _GoldQuality As String
    Private _Prefix As String
    Private _IsGramRate As Boolean
    Private _DividedBy As Decimal
    Private _MultiplyBy As Decimal
    Private _IsSolidGold As Boolean
    Private _LocationID As String
    Private _BarcodeStatus As Integer

    Public Property GoldQualityID() As String
        Get
            Return _GoldQualityID

        End Get
        Set(ByVal value As String)
            _GoldQualityID = value

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


    Public Property GoldQuality() As String
        Get
            Return _GoldQuality

        End Get
        Set(ByVal value As String)
            _GoldQuality = value

        End Set
    End Property

    Public Property Prefix() As String
        Get
            Return _Prefix

        End Get
        Set(ByVal value As String)
            _Prefix = value

        End Set
    End Property

    Public Property IsGramRate() As Boolean
        Get
            Return _IsGramRate

        End Get
        Set(ByVal value As Boolean)
            _IsGramRate = value

        End Set
    End Property

    Public Property DividedBy() As Decimal
        Get
            Return _DividedBy
        End Get
        Set(ByVal value As Decimal)
            _DividedBy = value
        End Set
    End Property

    Public Property MultiplyBy() As Decimal
        Get
            Return _MultiplyBy
        End Get
        Set(ByVal value As Decimal)
            _MultiplyBy = value
        End Set
    End Property

    Public Property IsSolidGold() As Boolean
        Get
            Return _IsSolidGold
        End Get
        Set(ByVal value As Boolean)
            _IsSolidGold = value
        End Set
    End Property
    Public Property BarcodeStatus() As Integer
        Get
            Return _BarcodeStatus
        End Get
        Set(ByVal value As Integer)
            _BarcodeStatus = value
        End Set
    End Property
End Class
