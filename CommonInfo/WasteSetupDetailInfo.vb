
Public Class WasteSetupDetailInfo
#Region "Private Property"
    Private _WasteSetupHeaderID As String
    Private _WasteSetupDetailID As String
    Private _GoldQualityID As String

    Private _MinNetWeightTK As Decimal
    Private _MinNetWeightTG As Decimal

    Private _MaxNetWeightTK As Decimal
    Private _MaxNetWeightTG As Decimal

    Private _MinWeightTKForSale As Decimal
    Private _MinWeightTGForSale As Decimal

    Private _MinWeightPForSale As Integer
    Private _MinWeightYForSale As Decimal
    Private _MinWeightCForSale As Decimal
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
    Public Property WasteSetupDetailID() As String
        Get
            WasteSetupDetailID = _WasteSetupDetailID
        End Get
        Set(ByVal value As String)
            _WasteSetupDetailID = value
        End Set
    End Property

    Public Property GoldQualityID() As String
        Get
            GoldQualityID = _GoldQualityID
        End Get
        Set(ByVal value As String)
            _GoldQualityID = value
        End Set
    End Property
    Public Property MinNetWeightTK() As Decimal
        Get
            MinNetWeightTK = _MinNetWeightTK
        End Get
        Set(ByVal value As Decimal)
            _MinNetWeightTK = value
        End Set
    End Property
    Public Property MinNetWeightTG() As Decimal
        Get
            MinNetWeightTG = _MinNetWeightTG
        End Get
        Set(ByVal value As Decimal)
            _MinNetWeightTG = value
        End Set
    End Property
    Public Property MaxNetWeightTK() As Decimal
        Get
            MaxNetWeightTK = _MaxNetWeightTK
        End Get
        Set(ByVal value As Decimal)
            _MaxNetWeightTK = value
        End Set
    End Property
    Public Property MaxNetWeightTG() As Decimal
        Get
            MaxNetWeightTG = _MaxNetWeightTG
        End Get
        Set(ByVal value As Decimal)
            _MaxNetWeightTG = value
        End Set
    End Property
    Public Property MinWeightTKForSale() As Decimal
        Get
            MinWeightTKForSale = _MinWeightTKForSale
        End Get
        Set(ByVal value As Decimal)
            _MinWeightTKForSale = value
        End Set
    End Property
    Public Property MinWeightTGForSale() As Decimal
        Get
            MinWeightTGForSale = _MinWeightTGForSale
        End Get
        Set(ByVal value As Decimal)
            _MinWeightTGForSale = value
        End Set
    End Property

    Public Property MinWeightPForSale() As Integer
        Get
            MinWeightPForSale = _MinWeightPForSale
        End Get
        Set(ByVal value As Integer)
            _MinWeightPForSale = value
        End Set
    End Property
    Public Property MinWeightYForSale() As Decimal
        Get
            MinWeightYForSale = _MinWeightYForSale
        End Get
        Set(ByVal value As Decimal)
            _MinWeightYForSale = value
        End Set
    End Property
    Public Property MinWeightCForSale() As Decimal
        Get
            MinWeightCForSale = _MinWeightCForSale
        End Get
        Set(ByVal value As Decimal)
            _MinWeightCForSale = value
        End Set
    End Property
#End Region
End Class
