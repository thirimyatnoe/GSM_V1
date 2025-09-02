Public Class CashReceiptInfo
#Region "Private Property"
    Private _CashReceiptID As String
    Private _LocationID As String
    Private _VoucherNo As String
    Private _PayDate As Date
    Private _PayAmount As Integer
    Private _Remark As String
    Private _Type As String
    Private _IsBank As Boolean
    Private _LastModifiedLoginUserName As String
    Private _LastModifiedDate As Date
    Private _RAdvanceID As String

#End Region

#Region "Properties "
    Public Property CashReceiptID() As String
        Get
            CashReceiptID = _CashReceiptID
        End Get
        Set(ByVal value As String)
            _CashReceiptID = value
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
    Public Property VoucherNo() As String
        Get
            VoucherNo = _VoucherNo
        End Get
        Set(ByVal value As String)
            _VoucherNo = value
        End Set
    End Property
    Public Property PayDate() As Date
        Get
            PayDate = _PayDate
        End Get
        Set(ByVal value As Date)
            _PayDate = value
        End Set
    End Property
    Public Property PayAmount() As Integer
        Get
            PayAmount = _PayAmount
        End Get
        Set(ByVal value As Integer)
            _PayAmount = value
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
    Public Property Type() As String
        Get
            Type = _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property
    Public Property IsBank() As Boolean
        Get
            IsBank = _IsBank
        End Get
        Set(ByVal value As Boolean)
            _IsBank = value
        End Set
    End Property
    Public Property LastModifiedLoginUserName() As String
        Get
            LastModifiedLoginUserName = _LastModifiedLoginUserName
        End Get
        Set(ByVal value As String)
            _LastModifiedLoginUserName = value
        End Set
    End Property
    Public Property LastModifiedDate() As Date
        Get
            LastModifiedDate = _LastModifiedDate
        End Get
        Set(ByVal value As Date)
            _LastModifiedDate = value
        End Set
    End Property
    Public Property RAdvanceID() As String
        Get
            RAdvanceID = _RAdvanceID
        End Get
        Set(ByVal value As String)
            _RAdvanceID = value
        End Set
    End Property

#End Region
End Class
