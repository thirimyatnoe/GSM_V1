Public Class GoldWeightInfo
#Region "Private Property"
    Private _Gram As Decimal
    Private _GoldTK As Decimal
    Private _WeightK As Integer
    Private _WeightP As Integer
    Private _WeightY As Decimal
    Private _WeightC As Decimal
#End Region
#Region "Properties "
    Public Property Gram() As Decimal
        Get
            Gram = _Gram
        End Get
        Set(ByVal argGram As Decimal)
            _Gram = argGram
        End Set
    End Property
    Public Property GoldTK() As Decimal
        Get
            GoldTK = _GoldTK
        End Get
        Set(ByVal argGoldTK As Decimal)
            _GoldTK = argGoldTK
        End Set
    End Property
    Public Property WeightK() As Integer
        Get
            WeightK = _WeightK
        End Get
        Set(ByVal argWeightK As Integer)
            _WeightK = argWeightK
        End Set
    End Property
    Public Property WeightP() As Integer
        Get
            WeightP = _WeightP
        End Get
        Set(ByVal argWeightP As Integer)
            _WeightP = argWeightP
        End Set
    End Property
    Public Property WeightY() As Decimal
        Get
            WeightY = _WeightY
        End Get
        Set(ByVal argWeightY As Decimal)
            _WeightY = argWeightY
        End Set
    End Property
    Public Property WeightC() As Decimal
        Get
            WeightC = _WeightC
        End Get
        Set(ByVal argWeightC As Decimal)
            _WeightC = argWeightC
        End Set
    End Property
#End Region
#Region " Initialize "
    Public Sub New()
        _WeightK = 0
        _WeightP = 0
        _WeightY = 0
        _WeightC = 0
        _Gram = 0
        _GoldTK = 0
    End Sub
#End Region
End Class
