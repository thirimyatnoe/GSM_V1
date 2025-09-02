Imports BusinessRule
Public Class frm_ExportDatabaseForHO
    Dim objDataExportImport As DatabaseExportImport.IDatabaseExportImportController = Factory.Instance.CreateDatabaseExportImportController
    Dim objLocation As Location.ILocationController = Factory.Instance.CreateLocationController
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Me.Cursor = Cursors.WaitCursor
        If objDataExportImport.ExportDatabaseForHO(txtPath.Text.Trim, dtpForMonth.Value.Date) = False Then
            MsgBox("Export Database Fail!", MsgBoxStyle.Exclamation, "Gold Smith Management System")
        Else
            MsgBox("Export Successful.", MsgBoxStyle.Information, "Gold Smith Management System")
        End If
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub btnPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPath.Click
        Save.FileName = objLocation.GetLocationByID(objLocation.GetCurrentLocationID).Location & dtpForMonth.Value.ToString("yyyyMMdd")
        Save.Filter = "Export Files (*.xml;)|*.xml;"
        Save.ShowDialog()
        txtPath.Text = Save.FileName
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class