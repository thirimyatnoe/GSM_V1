Public Class frm_AccessPasswordConfirm

    Public Password As String
    Public IsCancel As Boolean = True

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If txtPassword.Text = txtConfirmPassword.Text Then
            Password = txtPassword.Text
            IsCancel = False
            Me.Close()
        Else
            MsgBox("Password and Confirm Password must be same.", MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class