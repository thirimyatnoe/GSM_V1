Imports DataAccess.DBConnection
Imports System.Windows.Forms
'Imports System.ServiceProcess
Imports System.Configuration
Imports System.IO
Imports System.Reflection

Namespace DBConnection
    '''<summary>
    '''Modified Programmer Name = YNPP
    '''Last Modified Date=29-JAN-2008
    '''Use GWT Standard
    '''</summary>
    Public Class DBConnection

        Dim objDBConnectionDAL As New DBConnectionDAL

        Public Function IsServerMachine() As Boolean
            'Dim scServices() As ServiceController
            'Dim scTemp As ServiceController
            'scServices = ServiceController.GetServices()
            'For Each scTemp In scServices
            '    If scTemp.ServiceName = "ABSPTAServiceProcess" Then
            '        Return True
            '    End If
            'Next
            Try
                Dim FileMap As ExeConfigurationFileMap

                FileMap = New ExeConfigurationFileMap
                FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")

                Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                '' If config.AppSettings.Settings.Count = 0 Then Return False
                If config.AppSettings.Settings.Item("ValueCS").ElementInformation.IsPresent Then
                    If config.AppSettings.Settings("ValueCS").Value = "2" Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            


            Catch ex As FileNotFoundException
                Return False
            End Try
        End Function

#Region "MS SQL"

        Public Function IsExistsConfiguration() As Boolean
            Return objDBConnectionDAL.IsExistsConfiguration()
        End Function

        Public Function getDatabase(ByVal ServerName As String, ByVal UserID As String, ByVal Password As String, ByVal TrustedConnection As Boolean) As DataTable
            Return objDBConnectionDAL.getDatabase(ServerName, UserID, Password, TrustedConnection)
        End Function

        'Public Function GetSQLServerList(ByVal IsLocalOnly As Boolean) As DataTable
        '    'Return objDBConnectionDAL.GetSQLServerList(IsLocalOnly)
        'End Function

        Public Function CheckSQLDatabase(ByVal DatabaseName As String, ByVal ServerName As String, ByVal UserID As String, ByVal Password As String, ByVal TrustedConnection As Boolean) As Integer
            '0=DataBase Server OK, 1=Server Not Found, 2=Database Not Found
            Return objDBConnectionDAL.CheckDatabase(DatabaseName, ServerName, UserID, Password, TrustedConnection)
        End Function

        Public Function IsValidSQLDataStructure(ByVal DatabaseName As String, ByVal ServerName As String, ByVal UserID As String, ByVal Password As String, ByVal TrustedConnection As Boolean) As Boolean
            Return objDBConnectionDAL.IsValidSQLDataStructure(DatabaseName, ServerName, UserID, Password, TrustedConnection)
        End Function

        Public Function DeleteDatabase(ByVal DatabaseName As String, ByVal ServerName As String, ByVal UserID As String, ByVal Password As String, ByVal TrustedConnection As Boolean) As Boolean
            Return objDBConnectionDAL.DeleteDatabase(DatabaseName, ServerName, UserID, Password, TrustedConnection)
        End Function

        Public Function InstallNewDatabase(ByVal InstallScriptFile As String, ByVal DatabaseName As String, ByVal ServerName As String, ByVal UserID As String, ByVal Password As String, ByVal TrustedConnection As Boolean, ByVal FilePath As String) As Boolean
            Return objDBConnectionDAL.InstallNewDatabase(InstallScriptFile, DatabaseName, ServerName, UserID, Password, TrustedConnection, FilePath)
        End Function

        Public Function CreateConnectionString(ByVal ServerName As String, ByVal DatabaseName As String, ByVal UserID As String, ByVal Password As String, ByVal TrustedConnection As Boolean) As String
            Return objDBConnectionDAL.CreateConnectionString(ServerName, DatabaseName, UserID, Password, TrustedConnection)
        End Function

        'Public Function BackUpDB(ByVal argConnection As String, ByVal argTempDir As String, ByVal argBackUpFileName As String, ByVal pb As ProgressBar, ByVal frm As Form) As String
        '    ' Return objDBConnectionDAL.BackUpDB(argConnection, argTempDir, argBackUpFileName, pb, frm)
        'End Function

        'Public Function RestoreDB(ByVal argConnection As String, ByVal argTempDir As String, ByVal argBackUpFileName As String, ByVal pb As ProgressBar, ByVal frm As Form) As String
        '    'Return objDBConnectionDAL.RestoreDB(argConnection, argTempDir, argBackUpFileName, pb, frm)
        'End Function

        Public Function CheckExitColumnFieldInTable(ByVal ServerName As String, ByVal DatabaseName As String, ByVal UserID As String, ByVal Password As String, ByVal TrustedConnection As Boolean) As Boolean
            Return objDBConnectionDAL.CheckExitColumnFieldInTable(ServerName, DatabaseName, UserID, Password, TrustedConnection)
        End Function
#End Region

#Region " MS Access"

        Public Function AccessBackup(ByVal sourceFile As String, ByVal destinationFile As String)
            Return objDBConnectionDAL.AccessBackup(sourceFile, destinationFile)
        End Function

        Public Function AccessRestore(ByVal sourceFile As String, ByVal destinationFile As String)
            Return objDBConnectionDAL.AccessRestore(sourceFile, destinationFile)
        End Function

        Public Function InstallNewAccessDatabase(ByVal InstallScriptFile As String, ByVal FileName As String, ByVal UserID As String, ByVal PPassword As String) As Boolean
            'Return objDBConnectionDAL.InstallNewAccessDatabase(InstallScriptFile, FileName, PPassword)
        End Function

        Public Function CheckAccessDatabase(ByVal FileName As String, ByVal PUserID As String, ByVal PPassword As String) As Integer
            '0=Access Database OK, 1=File Not Found, 2=Incorrect Password, 3=Invalid Database structure
            Return objDBConnectionDAL.CheckAccessDatabase(FileName, PPassword)
        End Function

        Public Function CreateAccessConnectionString(ByVal FileName As String, ByVal UserName As String, ByVal PPassword As String) As String
            Return objDBConnectionDAL.CreateAccessConnectionString(FileName, PPassword)
        End Function

        Public Function isExistsDatabase(ByVal PDatabaseName As String, ByVal pServerName As String, ByVal PUserID As String, ByVal PPassword As String, ByVal pTrustedConnection As Boolean) As Boolean
            Return objDBConnectionDAL.isExistsDatabase(PDatabaseName, pServerName, PUserID, PPassword, pTrustedConnection)
        End Function
#End Region

    End Class

End Namespace