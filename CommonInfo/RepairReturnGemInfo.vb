Public Class RepairReturnGemInfo
#Region "Private Property"
    Private _ReturnRepairGemID As String
    Private _ReturnRepairDetailID As String
    Private _GemsCategoryID As String
    Private _Description As String
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal
    Private _GemsTK As Decimal
    Private _YOrCOrG As String
    Private _GemsTW As Decimal
    Private _GemsTG As Decimal
    Private _QTY As Integer
    Private _UnitPrice As Integer
    Private _Type As String
    Private _Amount As Integer
    Private _IsNewGems As Boolean
#End Region



#Region "Properties "
    Public Property ReturnRepairGemID() As String
        Get
            ReturnRepairGemID = _ReturnRepairGemID
        End Get
        Set(ByVal value As String)
            _ReturnRepairGemID = value
        End Set
    End Property
    Public Property ReturnRepairDetailID() As String
        Get
            ReturnRepairDetailID = _ReturnRepairDetailID
        End Get
        Set(ByVal value As String)
            _ReturnRepairDetailID = value
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
    Public Property Description() As String
        Get
            Description = _Description
        End Get
        Set(ByVal value As String)
            _Description = value
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

    Public Property QTY() As Integer
        Get
            QTY = _Qty
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
    Public Property IsNewGems() As Boolean
        Get
            IsNewGems = _IsNewGems
        End Get
        Set(ByVal value As Boolean)
            _IsNewGems = value
        End Set
    End Property

#End Region

End Class
