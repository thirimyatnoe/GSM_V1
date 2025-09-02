Imports System.Text
Imports System.Xml
Imports System.Configuration
Imports BusinessRule.DBConnection
Imports Operational.Cryptography
Imports Operational.AppConfiguration

Public Class frm_GE_Server
    Private _AccessPassword As String
    Dim objSettings As New DACrypto
    Dim objDBConnection As New DBConnection

    Private ScriptAccessFileName As String = Application.StartupPath & "\InstallAccessScript.sql"
    Private ScriptSqlFileName As String = Application.StartupPath & "\InstallSqlScript.sql"
    Private AccessDBName As String = Application.StartupPath & "\ABSDB.MDB"
    Private pValue As String
    Public Connect As Boolean = False

    Private Sub cmdExistDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExistDB.Click
        Dim fDBCon As New frm_GE_DBConnection
        fDBCon.isMain = True
        fDBCon.ShowDialog()
        Connect = fDBCon.Connect
        fDBCon.Dispose()
        fDBCon = Nothing
        If Connect = True Then
            Me.Close()
        End If
        'If fDBCon.Connect = False Then
        '    'MsgBox("Could not connect to Database", MsgBoxStyle.Exclamation, AppName)
        '    fDBCon.Dispose()
        '    fDBCon = Nothing
        '    Return False
        'Else
        '    fDBCon.Dispose()
        '    fDBCon = Nothing
        '    Return True
        'End If

        'If ServerConfiguration() = False Then End
    End Sub

    Private Sub cmdSQLDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSQLDB.Click
        Dim fDBCon As New frm_GE_SQLServer
        fDBCon.ShowDialog()
        Connect = fDBCon.Connect
        fDBCon.Dispose()
        fDBCon = Nothing

        If Connect = True Then
            Me.Close()
        End If

        'If fDBCon.Connect = False Then
        '    fDBCon.Dispose()
        '    fDBCon = Nothing
        '    Return False
        'Else
        '    fDBCon.Dispose()
        '    fDBCon = Nothing
        '    Return True
        'End If
        'If SQLServerConfiguration() = True Then End
    End Sub

    Private Sub cmdAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAccess.Click
        Dim AccessPassword As New frm_AccessPasswordConfirm
        AccessPassword.ShowDialog()
        If AccessPassword.IsCancel Then
            Me.Connect = False
            AccessPassword.Dispose()
        Else
            _AccessPassword = AccessPassword.Password
            AccessPassword.Dispose()

            Dim accessStatus As Integer
            Dim ansCreateDB As Integer
            accessStatus = objDBConnection.CheckAccessDatabase(AccessDBName, "Admin", _AccessPassword)
            '0=Access Database OK, 1=File Not Found, 2=Incorrect Password, 3=Invalid Database structure
            If accessStatus <> 1 Then
                Dim ComfirmForm As New frm_GE_Confirm
                '1=Overwrite , 2=Existing Use , 3=Cancel
                ansCreateDB = ComfirmForm.OpenForm
                ComfirmForm.Dispose()
                If ansCreateDB = 1 Then
                    Me.Cursor = Cursors.WaitCursor
                    System.IO.File.Delete(AccessDBName)
                    If objDBConnection.InstallNewAccessDatabase(ScriptAccessFileName, AccessDBName, "Admin", _AccessPassword) = False Then
                        Me.Cursor = Cursors.Arrow
                        MsgBox("Install Database Fail!", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                    Me.Cursor = Cursors.Arrow
                ElseIf ansCreateDB = 2 Then
                    If accessStatus = 2 Then
                        MsgBox("Incorrect password!", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    ElseIf accessStatus = 3 Then
                        MsgBox("Invalid access database!", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If
                ElseIf ansCreateDB = 3 Then
                    Exit Sub
                End If
            Else
                If objDBConnection.InstallNewAccessDatabase(ScriptAccessFileName, AccessDBName, "Admin", _AccessPassword) = False Then
                    MsgBox("Install Database Fail!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            pValue = objDBConnection.CreateAccessConnectionString(AccessDBName, "Admin", _AccessPassword)
            AppConfiguration.AddConfiguration("Access", "System.Data.OleDb", pValue)
            'sya
            MessageBox.Show("Access setting was configured successfully.  You have to restart application in order to change Access settings", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information)
            AppConfiguration.EncryptConfiguration() 'sya 10/11/2008
            Application.Restart()
            Me.Close()
            End
        End If

    End Sub

   

End Class