Imports BusinessRule
Imports CommonInfo

Public Class frm_CashType
    Implements IFormProcess
    Private _CashController As CashType.CashTypeController = Factory.Instance.CreateCashTypeController
    Private _dtCash As DataTable

#Region "Private Methods"
    Private Sub Clear()

        btnSave.Text = "&Save"
        FormatCashGrid()

    End Sub
    Private Sub FormatCashGrid()



        _dtCash = New DataTable
        _dtCash.Columns.Add("CashTypeID", System.Type.GetType("System.String"))
        _dtCash.Columns.Add("CashType", System.Type.GetType("System.String"))

        grdCash.AutoGenerateColumns = False
        grdCash.DataSource = _dtCash

        With grdCash
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9.5)

            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "CashTypeID"
            dclID.DataPropertyName = "CashTypeID"
            dclID.Name = "CashTypeID"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcName As New DataGridViewTextBoxColumn()
            dcName.HeaderText = "Cash Type"
            dcName.DataPropertyName = "CashType"
            dcName.Name = "CashType"
            dcName.DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
            dcName.Width = 290
            dcName.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcName)

        End With
    End Sub
    Private Sub ShowCashType()

        _dtCash = _CashController.GetCashTypeList()
        grdCash.DataSource = _dtCash

    End Sub

#End Region

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew

    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then

            If _CashController.SaveCashType(_dtCash) Then
                ShowCashType()
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Private Sub frm_CashType_Load(sender As Object, e As EventArgs) Handles Me.Load
        MyBase.MaximizeBox = False
        btnNew.Visible = False
        btnDelete.Visible = False
        Clear()
        ShowCashType()
    End Sub
    Private Function IsFillData() As Boolean
        If Not MyBase.IsFill_AtLeastOneRowInGrid(_dtCash) Then Return False

        Dim dr As DataRow
        For Each dr In _dtCash.Rows
           
            If dr.RowState = DataRowState.Added Then
                Dim dt As New DataTable
                dt = _CashController.GetCashTypeDataByCashType(dr("CashType"))
                If dt.Rows.Count > 0 Then
                    MsgBox("CashType  is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                    Return False
                End If
            ElseIf dr.RowState = DataRowState.Modified Then
                Dim dtStaff As New DataTable
                dtStaff = _CashController.GetCashTypeDataByCashType(dr("CashType"), dr("CashTypeID"))
                If dtStaff.Rows.Count > 0 Then
                    MsgBox("CashType  is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                    Return False
                End If
            End If
        Next

        Return True
    End Function


    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("CashType")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class

