Public Class TransferReturnInfo
#Region "Private Property"
    Private _TransferReturnID As String
    Private _TransferReturnDate As Date
    Private _LocationID As String
    Private _StaffID As String
    Private _Remark As String

#End Region

#Region "Properties "
    Public Property TransferReturnID() As String
        Get
            TransferReturnID = _TransferReturnID
        End Get
        Set(ByVal value As String)
            _TransferReturnID = value
        End Set
    End Property
    Public Property TransferReturnDate() As Date
        Get
            TransferReturnDate = _TransferReturnDate
        End Get
        Set(ByVal value As Date)
            _TransferReturnDate = value
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
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
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

#End Region
End Class
