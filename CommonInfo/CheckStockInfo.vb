Public Class CheckStockInfo
#Region "Private Property"
    Private _CheckStockID As String
    Private _checkdatetime As Date
    Private _ItemCategoryID As String
    Private _QTY As Integer
    Private _GoldTG As Decimal
    Private _MQTY As Integer
    Private _MGoldTG As Decimal
    Private _FQTY As Integer
    Private _FGoldTG As Decimal
    Private _LocationID As String
    Private _Remark As String
    Private _StaffID As String

  


#End Region

#Region "Properties "
    Public Property CheckStockID() As String
        Get
            CheckStockID = _CheckStockID
        End Get
        Set(ByVal value As String)
            _CheckStockID = value
        End Set
    End Property
    Public Property checkdatetime() As Date
        Get
            checkdatetime = _checkdatetime
        End Get
        Set(ByVal value As Date)
            _checkdatetime = value
        End Set
    End Property
    Public Property ItemCategoryID() As String
        Get
            ItemCategoryID = _ItemCategoryID
        End Get
        Set(ByVal value As String)
            _ItemCategoryID = value
        End Set
    End Property
  
    Public Property GoldTG() As Decimal
        Get
            GoldTG = _GoldTG
        End Get
        Set(ByVal value As Decimal)
            _GoldTG = value
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
    
    Public Property MQTY() As Integer
        Get
            MQTY = _MQTY
        End Get
        Set(ByVal value As Integer)
            _MQTY = value
        End Set
    End Property
    Public Property MGoldTG() As Decimal
        Get
            MGoldTG = _MGoldTG
        End Get
        Set(ByVal value As Decimal)
            _MGoldTG = value
        End Set
    End Property
    Public Property FGoldTG() As Decimal
        Get
            FGoldTG = _FGoldTG
        End Get
        Set(ByVal value As Decimal)
            _FGoldTG = value
        End Set
    End Property
    Public Property FQTY() As Integer
        Get
            FQTY = _FQTY
        End Get
        Set(ByVal value As Integer)
            _FQTY = value
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
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
        End Set
    End Property
    Public Property LocationID() As String
        Get
            LocationID = _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property
    'test
#End Region
End Class
