Public Class CustomReportInfo

#Region "Private Property"
    Private _ReportID As String
    Private _ReportName As String
    Private _ReportCode As String
    Private _CriCustomer As Boolean
    Private _CriGoldQuality As Boolean
    Private _CriItemCategory As Boolean
    Private _CriItemName As Boolean
    Private _CriFromDate As Boolean
    Private _CriToDate As Boolean
    Private _CriGemsCategory As Boolean
    Private _CriStaff As Boolean
#End Region
    
#Region "Properties"
    Public Property ReportID() As String
        Get
            ReportID = _ReportID
        End Get
        Set(ByVal value As String)
            _ReportID = value
        End Set
    End Property
    Public Property ReportName() As String
        Get
            ReportName = _ReportName
        End Get
        Set(ByVal value As String)
            _ReportName = value
        End Set
    End Property
    Public Property ReportCode() As String
        Get
            ReportCode = _ReportCode
        End Get
        Set(ByVal value As String)
            _ReportCode = value
        End Set
    End Property
    Public Property CriCustomer() As Boolean
        Get
            CriCustomer = _CriCustomer
        End Get
        Set(ByVal value As Boolean)
            _CriCustomer = value
        End Set
    End Property

    Public Property CriGoldQuality() As Boolean
        Get
            CriGoldQuality = _CriGoldQuality
        End Get
        Set(ByVal value As Boolean)
            _CriGoldQuality = value
        End Set
    End Property

    Public Property CriItemCategory() As Boolean
        Get
            CriItemCategory = _CriItemCategory
        End Get
        Set(ByVal value As Boolean)
            _CriItemCategory = value
        End Set
    End Property

    Public Property CriItemName() As Boolean
        Get
            CriItemName = _CriItemName
        End Get
        Set(ByVal value As Boolean)
            _CriItemName = value
        End Set
    End Property

    Public Property CriFromDate() As Boolean
        Get
            CriFromDate = _CriFromDate
        End Get
        Set(ByVal value As Boolean)
            _CriFromDate = value
        End Set
    End Property
    Public Property CriToDate() As Boolean
        Get
            CriToDate = _CriToDate
        End Get
        Set(ByVal value As Boolean)
            _CriToDate = value
        End Set
    End Property

    Public Property CriGemsCategory() As Boolean
        Get
            CriGemsCategory = _CriGemsCategory
        End Get
        Set(ByVal value As Boolean)
            _CriGemsCategory = value
        End Set
    End Property

    Public Property CriStaff() As Boolean
        Get
            CriStaff = _CriStaff
        End Get
        Set(ByVal value As Boolean)
            _CriStaff = value
        End Set
    End Property
#End Region
   
End Class
