Public Class CheckStockItemInfo
#Region "Private Property"
    Private _CheckStockItemID As String
    Private _MBarcodeNo As String
    Private _MItemCategoryID As String
    Private _CheckStockID As String
    Private _MGoldTG As Decimal


#End Region

#Region "Properties "
    Public Property CheckStockItemID() As String
        Get
            CheckStockItemID = _CheckStockItemID
        End Get
        Set(ByVal value As String)
            _CheckStockItemID = value
        End Set
    End Property
    
        Public Property MBarcodeNo() As String
        Get
            MBarcodeNo = _MBarcodeNo
        End Get
        Set(ByVal value As String)
            _MBarcodeNo = value
        End Set
    End Property
    Public Property CheckStockID() As String
        Get
            CheckStockID = _CheckStockID
        End Get
        Set(ByVal value As String)
            _CheckStockID = value
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
    Public Property MItemCategoryID() As String
        Get
            MItemCategoryID = _MItemCategoryID
        End Get
        Set(ByVal value As String)
            _MItemCategoryID = value
        End Set
    End Property
    'test
  
   

#End Region
End Class
