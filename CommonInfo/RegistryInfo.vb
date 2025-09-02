Public Class RegistryInfo
    Protected _CustomerReferenceNo As String = ""
    Protected _LicensedEmployee As Long = 0
    Protected _LicensedDay As Long = 0
    Protected _SerialKey As String = ""
    Protected _ServerName As String = ""
    Protected _LicensedStatus As String = ""
    Protected _Company As String = ""
    Protected _Timezone As String = ""

    Public Property LicensedStatus() As String
        Get
            Return _LicensedStatus
        End Get
        Set(ByVal value As String)
            _LicensedStatus = value
        End Set
    End Property

    Public Property LicensedDay() As Long
        Get
            Return _LicensedDay
        End Get
        Set(ByVal value As Long)
            _LicensedDay = value
        End Set
    End Property

    Public Property CustomerReferenceNo() As String
        Get
            Return _CustomerReferenceNo
        End Get
        Set(ByVal value As String)
            _CustomerReferenceNo = value
        End Set
    End Property

    Public Property LicensedEmployee() As Long
        Get
            Return _LicensedEmployee
        End Get
        Set(ByVal value As Long)
            _LicensedEmployee = value
        End Set
    End Property

    Public Property SerialKey() As String
        Get
            Return _SerialKey
        End Get
        Set(ByVal value As String)
            _SerialKey = value
        End Set
    End Property

    Public Property ServerName() As String
        Get
            Return _ServerName
        End Get
        Set(ByVal value As String)
            _ServerName = value
        End Set
    End Property
    Public Property Company() As String
        Get
            Return _Company
        End Get
        Set(ByVal value As String)
            _Company = value
        End Set
    End Property

    Public Property Timezone() As String
        Get
            Return _Timezone
        End Get
        Set(ByVal value As String)
            _Timezone = value
        End Set
    End Property
End Class
