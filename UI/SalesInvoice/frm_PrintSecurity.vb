Imports System.IO
Imports BusinessRule
Imports CommonInfo
Imports BusinessRule.UserManagement
Public Class frm_PrintSecurity
#Region "Variable"

    Public _UserInfo As New UserInfo
    Private _LoginController As New LogIn
    Public Const AppName = "Stock Control System"
    Public LoginSuccess As Boolean

    Public CurrentUser As String
    Public CurrentuserLevel As String
    Public UserID As String
    Private dtUserInfo As New DataTable

#End Region

    Private Sub Cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmdsave.Click
        If TxtPassword.Text <> "" Then
            Try
                ' CurrentUser = CboUserList.SelectedValue
                Global_CurrentUser = CurrentUser
                UserID = _LoginController.ValidateUserForPrintSecurity(TxtPassword.Text)

                If UserID = "" Then
                    LoginSuccess = False
                    MsgBox("Wrong password!", MsgBoxStyle.Exclamation, "Print Security")
                    TxtPassword.Focus()
                Else
                    With _UserInfo
                        .UserID = UserID
                    End With
                    Me.Close()
                End If
            Catch ex As Exception
                LoginSuccess = False
                MsgBox(ex.ToString)
            End Try


            
        End If
       
    End Sub
    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_PrintSecurity_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If UserID <> "" Then
            _UserInfo = New UserInfo
            _UserInfo.UserID = UserID
        End If
        Me.Close()
    End Sub
    Private Sub frm_PrintSecurity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TxtPassword.Select()
        TxtPassword.Focus()
    End Sub

    Private Sub TxtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPassword.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class