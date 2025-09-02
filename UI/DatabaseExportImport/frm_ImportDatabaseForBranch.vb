Imports BusinessRule
Public Class frm_ImportDatabaseForBranch

    Private ScriptFileName As String = Application.StartupPath & "\DivideByLocation.sql"
    Dim objDataExportImport As DatabaseExportImport.IDatabaseExportImportController = Factory.Instance.CreateDatabaseExportImportController

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If txtPath.Text.Trim = "" Then
            MsgBox("Please select file path.", MsgBoxStyle.Information, "Gold Smith Management System")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
       
        Dim strFileName As String = open.FileName
        Dim IsStock As Boolean = False
        Dim IsNewStock As Boolean = False
        Dim IsDivided As Boolean = False


        If InStr(strFileName, "Divide", CompareMethod.Text) > 0 Then
            IsDivided = True


            If objDataExportImport.ImportDatabaseFromHO(txtPath.Text.Trim, IsStock, IsNewStock, IsDivided, ScriptFileName) = False Then
                MsgBox("Import Database Fail!", MsgBoxStyle.Exclamation, AppName)
            Else
                MsgBox("Import Successful.", MsgBoxStyle.Information, AppName)
            End If

        ElseIf InStr(strFileName, "Stock", CompareMethod.Text) > 0 Then
            IsStock = True
            If optNewStock.Checked Then
                IsNewStock = True
            Else
                IsNewStock = False
            End If

            If objDataExportImport.ImportDatabaseFromHO(txtPath.Text.Trim, IsStock, IsNewStock, IsDivided, ScriptFileName) = False Then
                MsgBox("Import Database Fail!", MsgBoxStyle.Exclamation, AppName)
            Else
                MsgBox("Import Successful.", MsgBoxStyle.Information, AppName)
            End If
        Else
            MsgBox("File is incorrect. Please contact your Head Office!", MsgBoxStyle.Exclamation, AppName)
        End If



        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub btnPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPath.Click
        open.Filter = "Import Files (*.xml;)|*.xml;"
        open.ShowDialog()
        txtPath.Text = open.FileName
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class