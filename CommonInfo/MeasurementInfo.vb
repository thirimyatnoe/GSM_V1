Public Class MeasurementInfo
#Region "Private Property"
    Private _FromMeasurement As String
    Private _ToMeasurement As String
    Private _Equivalent As Decimal

#End Region

#Region "Properties "
    Public Property FromMeasurement() As String
        Get
            FromMeasurement = _FromMeasurement
        End Get
        Set(ByVal value As String)
            _FromMeasurement = value
        End Set
    End Property
    Public Property ToMeasurement() As String
        Get
            ToMeasurement = _ToMeasurement
        End Get
        Set(ByVal value As String)
            _ToMeasurement = value
        End Set
    End Property
    Public Property Equivalent() As Decimal
        Get
            Equivalent = _Equivalent
        End Get
        Set(ByVal value As Decimal)
            _Equivalent = value
        End Set
    End Property

#End Region
End Class
