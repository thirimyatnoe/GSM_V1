Partial Class dsDamage
    Partial Class DamageDataTable

        Private Sub DamageDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.columnDDate.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
