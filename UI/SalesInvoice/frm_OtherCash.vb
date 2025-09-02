Imports BusinessRule
Imports CommonInfo

Public Class frm_OtherCash

    Public _dtOtherCash As New DataTable
    Public ObjOtherCash As New OtherCashInfo
    Private _CashTypeController As CashType.CashTypeController = Factory.Instance.CreateCashTypeController

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frm_RecordOtherCash_Load(sender As Object, e As EventArgs) Handles Me.Load
        grdOtherCash.AutoGenerateColumns = False
        FormatGrdForOtherCash()
        grdOtherCash.DataSource = _dtOtherCash
    End Sub
    Private Sub FormatGrdForOtherCash()
        With grdOtherCash
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9.5)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "RecordCashID"
                .DataPropertyName = "RecordCashID"
                .Name = "RecordCashID"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcID)

            Dim dcName As New DataGridViewComboBoxColumn()
            With dcName
                .HeaderText = "CashType"
                .DataPropertyName = "CashTypeID"
                .Name = "CashTypeID"
                .DataSource = _CashTypeController.GetCashTypeList()
                .DisplayMember = "CashType"
                .ValueMember = "CashTypeID"
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Width = 150
            End With
            .Columns.Add(dcName)

            Dim dc1 As New DataGridViewTextBoxColumn()
            With dc1
                .HeaderText = "ExchangeRate"
                .DataPropertyName = "ExchangeRate"
                .Name = "ExchangeRate"
                .Width = 110
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dc1)

            Dim dc2 As New DataGridViewTextBoxColumn()
            With dc2
                .HeaderText = "Amount"
                .DataPropertyName = "Amount"
                .Name = "Amount"
                .Width = 95
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dc2)

            Dim dc3 As New DataGridViewTextBoxColumn()
            With dc3
                .HeaderText = "Total"
                .DataPropertyName = "Total"
                .Name = "Total"
                .Width = 95
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .DefaultCellStyle.Format = "###,##0.##"
                .ReadOnly = True
            End With
            .Columns.Add(dc3)
        End With
    End Sub

    Private Sub grdOtherCash_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles grdOtherCash.CellValidated
        If grdOtherCash.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdOtherCash.Columns(e.ColumnIndex).Name
                Case "ExchangeRate", "Amount"
                    With grdOtherCash
                        If Not IsDBNull(.Rows(e.RowIndex).Cells("ExchangeRate").Value) And Not IsDBNull(.Rows(e.RowIndex).Cells("Amount").Value) Then
                            .Rows(e.RowIndex).Cells("Total").Value = CInt(.Rows(e.RowIndex).Cells("ExchangeRate").Value) * CInt(.Rows(e.RowIndex).Cells("Amount").Value)
                        Else
                            .Rows(e.RowIndex).Cells("Total").Value = 0
                        End If
                    End With
            End Select
        End If
    End Sub
End Class