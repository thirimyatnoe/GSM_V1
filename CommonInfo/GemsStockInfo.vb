Public Class GemsStockInfo
#Region "Private Property"
    Private _GemsStockID As String
    Private _GivenDate As Date
    Private _StaffID As String
    Private _GemsCategoryID As String
    Private _GemsCategory As String
    Private _Description As String
    Private _BarcodeNo As String
    Private _CertificateNo As String
    Private _Carat As Decimal
    Private _WeightR As Integer
    Private _WeightB As Integer
    Private _WeightP As Decimal
    Private _QTY As Integer
    Private _GemsTW As Decimal
    Private _Type As String
    Private _SellingPrice As Integer
    Private _Amount As Integer
    Private _Remark As String
    Private _IsExit As Boolean
    Private _BalanceQTY As Integer
    Private _ExitDate As Date
    Private _BalanceWeight As Decimal
  
#End Region

#Region "Properties "
    Public Property GemsStockID() As String
        Get
            GemsStockID = _GemsStockID
        End Get
        Set(ByVal value As String)
            _GemsStockID = value
        End Set
    End Property
    Public Property GivenDate() As Date
        Get
            GivenDate = _GivenDate
        End Get
        Set(ByVal value As Date)
            _GivenDate = value
        End Set
    End Property
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
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
    Public Property GemsCategory() As String
        Get
            GemsCategory = _GemsCategory
        End Get
        Set(ByVal value As String)
            _GemsCategory = value
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
    Public Property BarcodeNo() As String
        Get
            BarcodeNo = _BarcodeNo
        End Get
        Set(ByVal value As String)
            _BarcodeNo = value
        End Set
    End Property
    Public Property CertificateNo() As String
        Get
            CertificateNo = _CertificateNo
        End Get
        Set(ByVal value As String)
            _CertificateNo = value
        End Set
    End Property
    Public Property Carat() As Decimal
        Get
            Carat = _Carat
        End Get
        Set(ByVal value As Decimal)
            _Carat = value
        End Set
    End Property
    Public Property WeightR() As Integer
        Get
            WeightR = _WeightR
        End Get
        Set(ByVal value As Integer)
            _WeightR = value
        End Set
    End Property
    Public Property WeightB() As Integer
        Get
            WeightB = _WeightB
        End Get
        Set(ByVal value As Integer)
            _WeightB = value
        End Set
    End Property
    Public Property WeightP() As Decimal
        Get
            WeightP = _WeightP
        End Get
        Set(ByVal value As Decimal)
            _WeightP = value
        End Set
    End Property
    Public Property QTY() As Integer
        Get
            QTY = _QTY
        End Get
        Set(ByVal value As Integer)
            _QTY = value
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
    Public Property Type() As String
        Get
            Type = _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property
    Public Property SellingPrice() As Integer
        Get
            SellingPrice = _SellingPrice
        End Get
        Set(ByVal value As Integer)
            _SellingPrice = value
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
    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
    Public Property IsExit() As Boolean
        Get
            IsExit = _IsExit
        End Get
        Set(ByVal value As Boolean)
            _IsExit = value
        End Set
    End Property
    Public Property BalanceWeight() As Decimal
        Get
            BalanceWeight = _BalanceWeight
        End Get
        Set(ByVal value As Decimal)
            _BalanceWeight = value
        End Set
    End Property

    Public Property BalanceQTY() As Integer
        Get
            BalanceQTY = _BalanceQTY
        End Get
        Set(ByVal value As Integer)
            _BalanceQTY = value
        End Set
    End Property

    Public Property ExitDate() As Date
        Get
            ExitDate = _ExitDate
        End Get
        Set(ByVal value As Date)
            _ExitDate = value
        End Set
    End Property
    'test
#End Region
End Class
