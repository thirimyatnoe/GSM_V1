Public Class RepairReturnHeaderInfo

#Region "Private Property"
    Private _ReturnRepairID As String
    Private _RepairID As String
    Private _ReturnDate As DateTime
    Private _AllReturnTotalAmount As Integer
    Private _AllReturnAddOrSub As Integer
    Private _ReturnDiscountAmount As Integer
    Private _ReturnPaidAmount As Integer
    Private _Remark As String
    Private _LastModifiedLoginUserName As String
    Private _LastModifiedDate As Date
    Private _IsPaid As Boolean
    Private _AdvanceAmount As Integer
    Private _BalanceAmount As Integer
    Private _StaffID As String
    Private _CustomerID As String
    Private _LocationID As String

#End Region

#Region "Properties "

    Public Property ReturnRepairID() As String
        Get
            ReturnRepairID = _ReturnRepairID
        End Get
        Set(ByVal value As String)
            _ReturnRepairID = value
        End Set
    End Property
    Public Property RepairID() As String
        Get
            RepairID = _RepairID
        End Get
        Set(ByVal value As String)
            _RepairID = value
        End Set
    End Property

    Public Property ReturnDate() As DateTime
        Get
            ReturnDate = _ReturnDate
        End Get
        Set(ByVal value As DateTime)
            _ReturnDate = value
        End Set
    End Property
    Public Property AllReturnTotalAmount() As Integer
        Get
            AllReturnTotalAmount = _AllReturnTotalAmount
        End Get
        Set(ByVal value As Integer)
            _AllReturnTotalAmount = value
        End Set
    End Property

    Public Property AllReturnAddOrSub() As Integer
        Get
            AllReturnAddOrSub = _AllReturnAddOrSub
        End Get
        Set(ByVal value As Integer)
            _AllReturnAddOrSub = value
        End Set
    End Property

    Public Property ReturnDiscountAmount() As Integer
        Get
            ReturnDiscountAmount = _ReturnDiscountAmount
        End Get
        Set(ByVal value As Integer)
            _ReturnDiscountAmount = value
        End Set
    End Property
    Public Property ReturnPaidAmount() As Integer
        Get
            ReturnPaidAmount = _ReturnPaidAmount
        End Get
        Set(ByVal value As Integer)
            _ReturnPaidAmount = value
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
    Public Property IsPaid() As Boolean
        Get
            IsPaid = _IsPaid
        End Get
        Set(ByVal value As Boolean)
            _IsPaid = value
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

    Public Property AdvanceAmount() As Integer
        Get
            AdvanceAmount = _AdvanceAmount
        End Get
        Set(ByVal value As Integer)
            _AdvanceAmount = value
        End Set
    End Property

    Public Property BalanceAmount() As Integer
        Get
            BalanceAmount = _BalanceAmount
        End Get
        Set(ByVal value As Integer)
            _BalanceAmount = value
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

    Public Property CustomerID() As String
        Get
            CustomerID = _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
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

#End Region
End Class
