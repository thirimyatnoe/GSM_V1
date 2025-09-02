Imports System.Data
Imports BusinessRule
Imports CommonInfo

Public Class frm_ExportDatabaseForBranch
    Dim objLocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Dim objDataExportImport As DatabaseExportImport.IDatabaseExportImportController = Factory.Instance.CreateDatabaseExportImportController
    Dim dtUser As DataTable
    Dim argLocationID As Integer
#Region " Private Methods "
    Private Sub Fill_cboBranch()
        cboBranch.DataSource = objLocationController.GetAllLocationList
        cboBranch.DisplayMember = "Location"
        cboBranch.ValueMember = "@LocationID"
    End Sub
#End Region
    Private Sub frm_ExportDatabaseForBranch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Fill_cboBranch()
        dtpFrom.Value = Now
        dtpTo.Value = Now
    End Sub
    Private Sub btnPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPath.Click
        Dim tmpFileName As String

        If cboBranch.SelectedValue <> -1 Then
            ' ''If dtpFrom.Value.Date <> dtpTo.Value.Date Then
            ' ''    tmpFileName = Replace(cboBranch.Text, " ", "_") & dtpFrom.Value.ToString("yyyyMMdd") & "TO" & dtpTo.Value.ToString("yyyyMMdd")
            ' ''Else
            ' ''    tmpFileName = Replace(cboBranch.Text, " ", "_") & dtpFrom.Value.ToString("yyyyMMdd")
            ' ''End If

            tmpFileName = Replace(cboBranch.Text, " ", "_") & "Stock_" & IIf(optNewStock.Checked, "New_", "Edit_") & dtpFrom.Value.ToString("yyyyMMdd")

            Save.FileName = tmpFileName
            Save.Filter = "Export Files (*.xml;)|*.xml;"
            Save.ShowDialog()
            txtPath.Text = Save.FileName
        End If
    End Sub
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'Export Branch
        Dim BranchID As Integer = cboBranch.SelectedValue

        If txtPath.Text.Trim = "" Then
            MsgBox("Please select export path.", MsgBoxStyle.Information, "Gold Smith Management System")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        If objDataExportImport.ExportDataBaseForBranch(txtPath.Text.Trim, BranchID, dtpFrom.Value, dtpTo.Value) = False Then
            '' If objDataExportImport.ExportDataBaseForBranch(txtPath.Text.Trim, cboBranch.SelectedValue, dtpFrom.Value, dtpTo.Value) Then
            Me.Cursor = Cursors.Default
            MsgBox("Export Database Fail!", MsgBoxStyle.Exclamation, "Gold Smith Management System")
            Exit Sub
        Else
            MsgBox("Export Successful.", MsgBoxStyle.Information, "Gold Smith Management System")
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class