Public Class UserLevelInfo
    Protected m_intSysID As String
    Protected m_strUserLevel As String
    Protected m_strDescription As String
    Protected m_strUserType As String
    Protected m_strRemark As String
    Protected m_intAdministrator As Integer

    Public Property SysID() As String
        Get
            SysID = m_intSysID
        End Get
        Set(ByVal argSysID As String)
            m_intSysID = argSysID
        End Set
    End Property

    Public Property UserLevel() As String
        Get
            UserLevel = m_strUserLevel
        End Get
        Set(ByVal argUserLevel As String)
            m_strUserLevel = argUserLevel
        End Set
    End Property

    Public Property Description() As String
        Get
            Description = m_strDescription
        End Get
        Set(ByVal argDescription As String)
            m_strDescription = argDescription
        End Set
    End Property

    Public Property UserType() As String
        Get
            UserType = m_strUserType
        End Get
        Set(ByVal argvalue As String)
            m_strUserType = argvalue
        End Set
    End Property

    Public Property Remark() As String
        Get
            Remark = m_strRemark
        End Get
        Set(ByVal argRemark As String)
            m_strRemark = argRemark
        End Set
    End Property

    Public Property Administrator() As Integer
        Get
            Administrator = m_intAdministrator
        End Get
        Set(ByVal argIsAdministrator As Integer)
            m_intAdministrator = argIsAdministrator
        End Set
    End Property

End Class

