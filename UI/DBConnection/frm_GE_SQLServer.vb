Imports BusinessRule.DBConnection
Imports Operational.AppConfiguration

Public Class frm_GE_SQLServer
    Dim objDBConnection As New DBConnection
    Private SQLDBName As String = "globalgold"
    Dim pTrustedConnection As Boolean
    Private ScriptSqlFileName As String = Application.StartupPath & "\Script\InstallSqlScript.sql"
    Dim pValue As String
    Public Connect As Boolean = False

    Private Sub rbtWindow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtWindow.CheckedChanged
        If rbtWindow.Checked = True Then
            gbSQL.Enabled = False
            pTrustedConnection = True

        Else
            gbSQL.Enabled = True
            pTrustedConnection = False

        End If

    End Sub

    Private Sub rbtSQL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtSQL.CheckedChanged
        If rbtSQL.Checked = True Then
            gbSQL.Enabled = True
            pTrustedConnection = False
        Else
            gbSQL.Enabled = False
            pTrustedConnection = True
        End If

    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStatus As Integer
        Dim ansCreateDB = vbNo

        If txtServer.Text.Trim = "" Then
            MessageBox.Show("Please enter Server Name.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            '0=DataBase Server OK, 1=Server Not Found, 2=Database Not Found
            sqlStatus = objDBConnection.CheckSQLDatabase(SQLDBName, txtServer.Text.Trim, txtUserName.Text, txtPassword.Text, pTrustedConnection)
            If sqlStatus = 1 Then  ' Server not found
                MsgBox("SQL Server not found!", MsgBoxStyle.Critical, "Server Connection")
                Exit Sub

            ElseIf sqlStatus = 2 Then   ' Database not found
                Me.Cursor = Cursors.WaitCursor
                If objDBConnection.InstallNewDatabase(ScriptSqlFileName, SQLDBName, txtServer.Text.Trim, txtUserName.Text, txtPassword.Text, pTrustedConnection, txtFilePath.Text.Trim) = False Then
                    Me.Cursor = Cursors.Arrow
                    MsgBox("Install Database Fail!", MsgBoxStyle.Critical, "Server Connection")
                    Exit Sub
                End If
                Me.Cursor = Cursors.Arrow
            Else      ' Server and Database Exists
                Dim ComfirmForm As New frm_GE_Confirm
                ansCreateDB = ComfirmForm.OpenForm
                ComfirmForm.Dispose()
                If ansCreateDB = 1 Then   ' Overwrite existing
                    Me.Cursor = Cursors.WaitCursor
                    objDBConnection.DeleteDatabase(SQLDBName, txtServer.Text.Trim, txtUserName.Text, txtPassword.Text, pTrustedConnection)
                    If objDBConnection.InstallNewDatabase(ScriptSqlFileName, SQLDBName, txtServer.Text.Trim, txtUserName.Text, txtPassword.Text, pTrustedConnection, txtFilePath.Text.Trim) = False Then
                        Me.Cursor = Cursors.Arrow
                        MsgBox("Install Database Fail!", MsgBoxStyle.Critical, "Server Connection")
                        Exit Sub
                    End If
                    Me.Cursor = Cursors.Arrow
                ElseIf ansCreateDB = 2 Then   ' Use Existing Database
                    If objDBConnection.IsValidSQLDataStructure(SQLDBName, txtServer.Text.Trim, txtUserName.Text, txtPassword.Text, pTrustedConnection) = False Then
                        ' Invalid Database structure
                        MsgBox("Invalid database structure!", MsgBoxStyle.Critical, "Server Connection")
                        Exit Sub
                    End If
                ElseIf ansCreateDB = 3 Then   ' Cancel
                    Exit Sub
                End If
            End If

            pValue = objDBConnection.CreateConnectionString(txtServer.Text.Trim, SQLDBName, txtUserName.Text, txtPassword.Text, pTrustedConnection)

            AppConfiguration.AddConfiguration("MsSql", "System.Data.SqlClient", pValue)
            AppConfiguration.EncryptConfiguration() 'sya 10/11/2008
            Connect = True
            MessageBox.Show("Server setting was configured successfully.  You have to restart application in order to change server settings", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Application.Restart()
            End

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, AppName)
            Connect = False
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Connect = False
        Me.Close()
    End Sub

    Private Sub txtServer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServer.Validated
        If txtServer.Text.Trim = "" Then
            MessageBox.Show("You have to type Server Name", "Invalid Servername", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtServer.Focus()
            Exit Sub
        End If
    End Sub

   
End Class