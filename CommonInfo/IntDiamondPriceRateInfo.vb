Public Class IntDiamondPriceRateInfo
#Region "Private Property"
    Private _DefineID As String
    Private _DefineDateTime As Date
    Private _CaratFrom As Decimal
    Private _CaratTo As Decimal
    Private _PriceRate As Double
    Private _PercentReturn As Integer
    Private _PercentDirectChange As Integer
    Private _WholeSaleRate As Double
    Private _PurchaseRate As Double
#End Region

#Region "Properties "
    Public Property DefineID() As String
        Get
            DefineID = _DefineID
        End Get
        Set(ByVal value As String)
            _DefineID = value
        End Set
    End Property
    Public Property DefineDateTime() As Date
        Get
            DefineDateTime = _DefineDateTime
        End Get
        Set(ByVal value As Date)
            _DefineDateTime = value
        End Set
    End Property
    Public Property CaratFrom() As Decimal
        Get
            CaratFrom = _CaratFrom
        End Get
        Set(ByVal value As Decimal)
            _CaratFrom = value
        End Set
    End Property
    Public Property CaratTo() As Decimal
        Get
            CaratTo = _CaratTo
        End Get
        Set(ByVal value As Decimal)
            _CaratTo = value
        End Set
    End Property
    Public Property PriceRate() As Double
        Get
            PriceRate = _PriceRate
        End Get
        Set(ByVal value As Double)
            _PriceRate = value
        End Set
    End Property
    Public Property PercentReturn() As Integer
        Get
            PercentReturn = _PercentReturn
        End Get
        Set(ByVal value As Integer)
            _PercentReturn = value
        End Set
    End Property
    Public Property PercentDirectChange() As Integer
        Get
            PercentDirectChange = _PercentDirectChange
        End Get
        Set(ByVal value As Integer)
            _PercentDirectChange = value
        End Set
    End Property
    Public Property WholeSaleRate() As Double
        Get
            WholeSaleRate = _WholeSaleRate
        End Get
        Set(ByVal value As Double)
            _WholeSaleRate = value
        End Set
    End Property

    Public Property PurchaseRate() As Double
        Get
            PurchaseRate = _PurchaseRate
        End Get
        Set(ByVal value As Double)
            _PurchaseRate = value
        End Set
    End Property


#End Region
End Class
