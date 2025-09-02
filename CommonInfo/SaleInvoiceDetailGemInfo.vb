Public Class SaleInvoiceDetailGemInfo
#Region "Private Property"
    Private _SalesInvoiceGemItemID As String
    Private _SalesInvoiceDetailID As String
    Private _GemsCategoryID As String
    Private _GemsName As String
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    '**********
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    '**********
    Private _YOrCOrG As String
    Private _GemY As Integer
    Private _GemBCG As Decimal
    Private _GemsTW As Decimal
    Private _GemTK As Decimal
    Private _GemP As Decimal
    Private _Qty As Integer
    Private _FixType As String
    Private _UnitPrice As Integer
    Private _Amount As String
    Private _GemsRemark As String
    Private _Type As String
    Private _GemTaxPer As Decimal
    Private _GemTax As Integer
#End Region

#Region "Properties "
    Public Property SalesInvoiceGemItemID() As String
        Get
            SalesInvoiceGemItemID = _SalesInvoiceGemItemID
        End Get
        Set(ByVal value As String)
            _SalesInvoiceGemItemID = value
        End Set
    End Property
    Public Property SalesInvoiceDetailID() As String
        Get
            SalesInvoiceDetailID = _SalesInvoiceDetailID
        End Get
        Set(ByVal value As String)
            _SalesInvoiceDetailID = value
        End Set
    End Property
    Public Property GemsCategoryID() As String
        Get
            GemsCategoryID = _GemsCategoryID
        End Get
        Set(ByVal value As String)
            _GemsCategoryID = value
        End Set
    End Property
    Public Property GemsName() As String
        Get
            GemsName = _GemsName
        End Get
        Set(ByVal value As String)
            _GemsName = value
        End Set
    End Property
    Public Property GemsK() As Integer
        Get
            GemsK = _GemsK
        End Get
        Set(ByVal value As Integer)
            _GemsK = value
        End Set
    End Property
    Public Property GemsP() As Integer
        Get
            GemsP = _GemsP
        End Get
        Set(ByVal value As Integer)
            _GemsP = value
        End Set
    End Property
    Public Property GemsY() As Integer
        Get
            GemsY = _GemsY
        End Get
        Set(ByVal value As Integer)
            _GemsY = value
        End Set
    End Property
    Public Property GemsC() As Decimal
        Get
            GemsC = _GemsC
        End Get
        Set(ByVal value As Decimal)
            _GemsC = value
        End Set
    End Property
    Public Property GemsTK() As Decimal
        Get
            GemsTK = _GemsTK
        End Get
        Set(ByVal value As Decimal)
            _GemsTK = value
        End Set
    End Property
    Public Property GemsTG() As Decimal
        Get
            GemsTG = _GemsTG
        End Get
        Set(ByVal value As Decimal)
            _GemsTG = value
        End Set
    End Property
    Public Property YOrCOrG() As String
        Get
            YOrCOrG = _YOrCOrG
        End Get
        Set(ByVal value As String)
            _YOrCOrG = value
        End Set
    End Property
    Public Property GemY() As Integer
        Get
            GemY = _GemY
        End Get
        Set(ByVal value As Integer)
            _GemY = value
        End Set
    End Property
    Public Property GemBCG() As Decimal
        Get
            GemBCG = _GemBCG
        End Get
        Set(ByVal value As Decimal)
            _GemBCG = value
        End Set
    End Property
    Public Property GemsTW() As Decimal
        Get
            GemsTW = _GemsTW
        End Get
        Set(ByVal value As Decimal)
            _GemsTW = value
        End Set
    End Property
    Public Property GemTK() As Decimal
        Get
            GemTK = _GemTK
        End Get
        Set(ByVal value As Decimal)
            _GemTK = value
        End Set
    End Property
    Public Property GemP() As Decimal
        Get
            GemP = _GemP
        End Get
        Set(ByVal value As Decimal)
            _GemP = value
        End Set
    End Property
    Public Property Qty() As Integer
        Get
            Qty = _Qty
        End Get
        Set(ByVal value As Integer)
            _Qty = value
        End Set
    End Property
    Public Property FixType() As String
        Get
            FixType = _FixType
        End Get
        Set(ByVal value As String)
            _FixType = value
        End Set
    End Property
    Public Property UnitPrice() As Integer
        Get
            UnitPrice = _UnitPrice
        End Get
        Set(ByVal value As Integer)
            _UnitPrice = value
        End Set
    End Property
    Public Property Amount() As String
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As String)
            _Amount = value
        End Set
    End Property
    Public Property GemsRemark() As String
        Get
            GemsRemark = _GemsRemark
        End Get
        Set(ByVal value As String)
            _GemsRemark = value
        End Set
    End Property
    Public Property Type() As String
        Get
            Type = _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property
    Public Property GemTaxPer() As Decimal
        Get
            GemTaxPer = _GemTaxPer
        End Get
        Set(ByVal value As Decimal)
            _GemTaxPer = value
        End Set
    End Property
    Public Property GemTax() As Integer
        Get
            GemTax = _GemTax
        End Get
        Set(ByVal value As Integer)
            _GemTax = value
        End Set
    End Property
#End Region
End Class
