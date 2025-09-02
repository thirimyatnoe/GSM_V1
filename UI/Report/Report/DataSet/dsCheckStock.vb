Partial Class DataSet2
    Partial Class dsCheckStockDataTable

        Private Sub dsCheckStockDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ItemCategoryColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
