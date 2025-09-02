Public Class RepairHeaderInfo

#Region "Private Property"
    Private _RepairID As String
    Private _RepairDate As DateTime
    Private _ReturnDate As Date
    Private _CustomerID As String
    Private _StaffID As String
    Private _Remark As String
    Private _AdvanceRepairAmount As Integer
    Private _DueDate As Date
    Private _IsAllReturn As Boolean
    Private _LastModifiedLoginUserName As String
    Private _LastModifiedDate As Date
#End Region

#Region "Properties "
    Public Property RepairID() As String
        Get
            RepairID = _RepairID
        End Get
        Set(ByVal value As String)
            _RepairID = value
        End Set
    End Property
    Public Property RepairDate() As DateTime
        Get
            RepairDate = _RepairDate
        End Get
        Set(ByVal value As DateTime)
            _RepairDate = value
        End Set
    End Property
    Public Property ReturnDate() As Date
        Get
            ReturnDate = _ReturnDate
        End Get
        Set(ByVal value As Date)
            _ReturnDate = value
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

    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
        End Set
    End Property

    Public Property AdvanceRepairAmount() As Integer
        Get
            AdvanceRepairAmount = _AdvanceRepairAmount
        End Get
        Set(ByVal value As Integer)
            _AdvanceRepairAmount = value
        End Set
    End Property
    Public Property DueDate() As Date
        Get
            DueDate = _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property

    Public Property IsAllReturn() As Boolean
        Get
            IsAllReturn = _IsAllReturn
        End Get
        Set(ByVal value As Boolean)
            _IsAllReturn = value
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

#End Region

End Class
