Public Class frm_TermsAndConditions

    Private Sub frm_TermsAndConditions_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnOK.Focus()

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub
End Class