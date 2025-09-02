

Partial Public Class dsSaleGems
    Partial Class SaleGemsDataTable

        Private Sub SaleGemsDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.GemsTKColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
