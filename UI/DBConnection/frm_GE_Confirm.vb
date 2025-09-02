Public Class frm_GE_Confirm
    Private intReturnvalue As Integer = 3

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        intReturnvalue = IIf(rbtOverwrite.Checked = True, 1, 2)
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        intReturnvalue = 3
        Me.Close()
    End Sub

    Public Function OpenForm() As Integer
        Me.ShowDialog()
        Return intReturnvalue
    End Function
End Class