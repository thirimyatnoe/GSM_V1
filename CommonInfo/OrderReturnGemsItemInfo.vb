Public Class OrderReturnGemsItemInfo
#Region "Private Property"
    Private _OrderReturnGemID As String
    Private _OrderInvoiceDetailID As String
    Private _GemsCategoryID As String
    Private _GemsName As String
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _GemsTK As Decimal
    Private _YOrCOrG As String
    Private _GemsTW As Decimal
    Private _GemsTG As Decimal
    Private _Qty As Integer
    Private _UnitPrice As Integer
    Private _SaleType As String
    Private _Amount As Integer
    Private _GemsRemark As String
    Private _GemTaxPer As Decimal
    Private _GemTax As Integer

#End Region

#Region "Properties "
    Public Property OrderReturnGemID() As String
        Get
            OrderReturnGemID = _OrderReturnGemID
        End Get
        Set(ByVal value As String)
            _OrderReturnGemID = value
        End Set
    End Property
    Public Property OrderInvoiceDetailID() As String
        Get
            OrderInvoiceDetailID = _OrderInvoiceDetailID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceDetailID = value
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
    Public Property YOrCOrG() As String
        Get
            YOrCOrG = _YOrCOrG
        End Get
        Set(ByVal value As String)
            _YOrCOrG = value
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
    Public Property GemsTG() As Decimal
        Get
            GemsTG = _GemsTG
        End Get
        Set(ByVal value As Decimal)
            _GemsTG = value
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
    Public Property UnitPrice() As Integer
        Get
            UnitPrice = _UnitPrice
        End Get
        Set(ByVal value As Integer)
            _UnitPrice = value
        End Set
    End Property
    Public Property SaleType() As String
        Get
            SaleType = _SaleType
        End Get
        Set(ByVal value As String)
            _SaleType = value
        End Set
    End Property
    Public Property Amount() As Integer
        Get
            Amount = _Amount
        End Get
        Set(ByVal value As Integer)
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
