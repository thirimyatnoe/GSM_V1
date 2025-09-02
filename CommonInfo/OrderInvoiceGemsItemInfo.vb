Public Class OrderInvoiceGemsItemInfo
#Region "Private Property"
    Private _OrderInvoiceGemsItemID As String
    Private _OrderReceiveDetailID As String
    Private _OrderInvoiceID As String
    Private _GemsCategoryID As String
    Private _GemsName As String
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _GemsTK As Decimal
    Private _YOrCOrG As String
    'Private _GemY As Integer
    'Private _GemBCG As Decimal
    Private _GemsTW As Decimal
    Private _GemsTG As Decimal
    'Private _GemP As Decimal
    Private _Qty As Integer
    Private _UnitPrice As Integer
    Private _Type As String
    Private _Amount As Integer
    Private _IsCustomerGem As Boolean

#End Region

#Region "Properties "
    Public Property OrderInvoiceGemsItemID() As String
        Get
            OrderInvoiceGemsItemID = _OrderInvoiceGemsItemID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceGemsItemID = value
        End Set
    End Property
    Public Property OrderReceiveDetailID() As String
        Get
            OrderReceiveDetailID = _OrderReceiveDetailID
        End Get
        Set(ByVal value As String)
            _OrderReceiveDetailID = value
        End Set
    End Property

    Public Property OrderInvoiceID() As String
        Get
            OrderInvoiceID = _OrderInvoiceID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceID = value
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
    'Public Property GemY() As Integer
    '    Get
    '        GemY = _GemY
    '    End Get
    '    Set(ByVal value As Integer)
    '        _GemY = value
    '    End Set
    'End Property
    'Public Property GemBCG() As Decimal
    '    Get
    '        GemBCG = _GemBCG
    '    End Get
    '    Set(ByVal value As Decimal)
    '        _GemBCG = value
    '    End Set
    'End Property
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
    'Public Property GemP() As Decimal
    '    Get
    '        GemP = _GemP
    '    End Get
    '    Set(ByVal value As Decimal)
    '        _GemP = value
    '    End Set
    'End Property
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
    Public Property Type() As String
        Get
            Type = _Type
        End Get
        Set(ByVal value As String)
            _Type = value
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
    Public Property IsCustomerGem() As Boolean
        Get
            IsCustomerGem = _IsCustomerGem
        End Get
        Set(ByVal value As Boolean)
            _IsCustomerGem = value
        End Set
    End Property

#End Region
End Class
