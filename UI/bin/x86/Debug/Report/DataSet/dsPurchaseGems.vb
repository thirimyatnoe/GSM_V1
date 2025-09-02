

Partial Public Class dsPurchaseGems
    Partial Class dsPurchaseGemsDataTable

        Private Sub dsPurchaseGemsDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.LocationColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
