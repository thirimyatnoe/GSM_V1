Public Class ItemCategoryInfo
    Private _ItemCategoryID As String
    Private _ItemCategory As String
    Private _Prefix As String
    Private _ItemTaxPer As Decimal
    Private _TaxPer As Integer
    Public Property ItemCategoryID() As String
        Get
            Return _ItemCategoryID

        End Get
        Set(ByVal value As String)
            _ItemCategoryID = value

        End Set
    End Property

    Public Property ItemCategory() As String
        Get
            Return _ItemCategory

        End Get
        Set(ByVal value As String)
            _ItemCategory = value

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

    Public Property ItemTaxPer() As Decimal
        Get
            Return _ItemTaxPer

        End Get
        Set(ByVal value As Decimal)
            _ItemTaxPer = value

        End Set
    End Property
    Public Property TaxPer() As Integer
        Get
            Return _TaxPer

        End Get
        Set(ByVal value As Integer)
            _TaxPer = value

        End Set
    End Property
End Class
