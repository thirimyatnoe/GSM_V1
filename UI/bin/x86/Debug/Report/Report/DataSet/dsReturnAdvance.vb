Partial Class dsReturnAdvance
    Partial Class ReturnAdvanceDataTable

        Private Sub ReturnAdvanceDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.AddressColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
