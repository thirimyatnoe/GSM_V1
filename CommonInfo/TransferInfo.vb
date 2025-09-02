Public Class TransferInfo
#Region "Private Property"
    Private _TransferID As String
    Private _TransferDate As DateTime
    Private _LocationID As String
    Private _StaffID As String
    Private _Remark As String
    Private _IsConfirm As Boolean
    Private _CurrentLocationID As String
#End Region

#Region "Properties "
    Public Property TransferID() As String
        Get
            TransferID = _TransferID
        End Get
        Set(ByVal value As String)
            _TransferID = value
        End Set
    End Property
    Public Property TransferDate() As DateTime
        Get
            TransferDate = _TransferDate
        End Get
        Set(ByVal value As DateTime)
            _TransferDate = value
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
    Public Property IsConfirm() As Boolean
        Get
            IsConfirm = _IsConfirm
        End Get
        Set(ByVal value As Boolean)
            _IsConfirm = value
        End Set
    End Property
    Public Property CurrentLocationID() As String
        Get
            CurrentLocationID = _CurrentLocationID
        End Get
        Set(ByVal value As String)
            _CurrentLocationID = value
        End Set
    End Property

#End Region
End Class
