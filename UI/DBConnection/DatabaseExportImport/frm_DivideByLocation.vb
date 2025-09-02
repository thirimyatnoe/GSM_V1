Imports System.Data
Imports BusinessRule
Imports CommonInfo
Public Class frm_DivideByLocation
    Dim ObjBranchController As Location.LocationController = Factory.Instance.CreateLocationController
    Dim objDataExportImport As DatabaseExportImport.IDatabaseExportImportController = Factory.Instance.CreateDatabaseExportImportController
#Region " Private Methods "
    Private Sub Fill_cboBranch()
        cboBranch.DataSource = ObjBranchController.GetAllLocation()
        cboBranch.DisplayMember = "Location_"
        cboBranch.ValueMember = "@LocationID"
    End Sub
#End Region
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If txtPath.Text.Trim = "" Then
            MsgBox("Please select export path.", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        If objDataExportImport.DivideByLocation(txtPath.Text.Trim, cboBranch.SelectedValue) = False Then
            Me.Cursor = Cursors.Default
            MsgBox("Export Database Fail!", MsgBoxStyle.Exclamation, AppName)
            Exit Sub
        Else
            MsgBox("Export Successful.", MsgBoxStyle.Information, AppName)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPath.Click
        Dim tmpFileName As String

        If cboBranch.SelectedValue <> -1 Then
            tmpFileName = "DivideForLocation_" & Replace(cboBranch.Text, " ", "_")
            Save.FileName = tmpFileName
            Save.Filter = "Export Files (*.xml;)|*.xml;"
            Save.ShowDialog()
            txtPath.Text = Save.FileName
        End If
    End Sub

    Private Sub frm_DivideByLocation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Fill_cboBranch()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class