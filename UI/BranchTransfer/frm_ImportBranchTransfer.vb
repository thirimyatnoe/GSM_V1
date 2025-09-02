Imports BusinessRule
Public Class frm_ImportBranchTransfer

    Dim objDataExportImport As DatabaseExportImport.IDatabaseExportImportController = Factory.Instance.CreateDatabaseExportImportController



    Private Sub btnPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPath.Click
        open.Filter = "Import Files (*.xml;)|*.xml;"
        open.ShowDialog()
        txtPath.Text = open.FileName

    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If txtPath.Text.Trim = "" Then
            MsgBox("Please select file path.", MsgBoxStyle.Information, "Gold Smith Management System")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Dim strFileName As String = open.FileName
        Dim IsStock As Boolean = False
        Dim IsNewStock As Boolean = False

      
        If objDataExportImport.ImportDatabaseFromTransfer(txtPath.Text.Trim) = False Then
            MsgBox("Import Database Fail!", MsgBoxStyle.Exclamation, "Gold Smith Management System")
        Else
            MsgBox("Import Successful.", MsgBoxStyle.Information, "Gold Smith Management System")
        End If
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

  
End Class