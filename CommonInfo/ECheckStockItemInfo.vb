Public Class ECheckStockItemInfo
#Region "Private Property"
    Private _ECheckStockItemID As String
    Private _CheckStockID As String
    Private _Weight As Decimal
    Private _EBarcodeNo As String
   

#End Region

#Region "Properties "
    Public Property ECheckStockItemID() As String
        Get
            ECheckStockItemID = _ECheckStockItemID
        End Get
        Set(ByVal value As String)
            _ECheckStockItemID = value
        End Set
    End Property
    

    Public Property EBarcodeNo() As String
        Get
            EBarcodeNo = _EBarcodeNo
        End Get
        Set(ByVal value As String)
            _EBarcodeNo = value
        End Set
    End Property
    Public Property Weight() As Decimal
        Get
            Weight = _Weight
        End Get
        Set(ByVal value As Decimal)
            _Weight = value
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
    'test

#End Region
End Class
