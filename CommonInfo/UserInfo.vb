
Public Class UserInfo
    Protected m_intSysID As Integer
    Protected m_strUserID As String
    Protected m_strUserName As String
    Protected m_strPassword As String
    Protected m_strUserLevelID As Integer
    Protected m_strRemark As String

    Public Property SysID() As Integer
        Get
            SysID = m_intSysID
        End Get
        Set(ByVal argSysID As Integer)
            m_intSysID = argSysID
        End Set
    End Property

    Public Property UserID() As String
        Get
            UserID = m_strUserID
        End Get
        Set(ByVal argUserID As String)
            m_strUserID = argUserID
        End Set
    End Property

    Public Property UserName() As String
        Get
            UserName = m_strUserName
        End Get
        Set(ByVal argUserName As String)
            m_strUserName = argUserName
        End Set
    End Property
    Public Property UserPassword() As String
        Get
            UserPassword = m_strPassword
        End Get
        Set(ByVal argUserPassword As String)
            m_strPassword = argUserPassword
        End Set
    End Property

    Public Property UserLevelID() As Integer
        Get
            UserLevelID = m_strUserLevelID
        End Get
        Set(ByVal argUserLevelID As Integer)
            m_strUserLevelID = argUserLevelID
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
End Class

