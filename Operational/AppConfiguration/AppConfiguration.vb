Imports System.Configuration
Namespace AppConfiguration
    Public Class AppConfiguration
        Private Shared config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Private Shared Connection As ConnectionStringSettings
        Private Shared connections As ConnectionStringSettingsCollection

        Public Shared Sub AddConfiguration(ByVal Name As String, ByVal ProviderName As String, ByVal ConnectionString As String)
            Try
                Dim sect As ConfigurationSection


                config.ConnectionStrings.ConnectionStrings.Clear()

                If config.ConnectionStrings.ConnectionStrings(Name) IsNot Nothing Then
                    AppConfiguration.DeleteConfiguration(config.ConnectionStrings.ConnectionStrings(Name))
                End If
                Connection = New ConnectionStringSettings
                Connection.Name = "DataAccess" 'Name '"DataAccessQuickStart"
                Connection.ProviderName = ProviderName
                Connection.ConnectionString = ConnectionString
                config.ConnectionStrings.ConnectionStrings.Add(Connection)

                sect = config.GetSection("connectionStrings")
                If Not sect Is Nothing Then
                    If sect.IsReadOnly = False Then

                        sect.SectionInformation.ForceSave = True

                    End If
                End If

                config.Save(ConfigurationSaveMode.Full)
            Catch ex As ConfigurationErrorsException

            End Try
        End Sub

        Public Shared Function ReadAppSettings(ByVal Name As String) As String
            Dim test As String = ""
            If IsDBNull(config.AppSettings.Settings(Name).Value) Then
                test = ""
            Else
                test = config.AppSettings.Settings(Name).Value
            End If
            Return test
        End Function
        Public Shared Function ReadConnectionString() As String
            Dim _Name As String = ""
            If config.HasFile Then
                connections = config.ConnectionStrings.ConnectionStrings
                Dim conEnum As IEnumerator = connections.GetEnumerator()
                While conEnum.MoveNext
                    Connection = conEnum.Current
                    _Name = Connection.ConnectionString
                End While
            End If
            Return _Name
        End Function

        Public Shared Function ReadProviderName() As String
            Dim _Name As String = ""
            If config.HasFile Then
                connections = config.ConnectionStrings.ConnectionStrings
                Dim conEnum As IEnumerator = connections.GetEnumerator()
                While conEnum.MoveNext
                    Connection = conEnum.Current
                    _Name = Connection.ProviderName
                End While
            End If
            Return _Name
        End Function

        Public Shared Sub EncryptConfiguration()
            Dim sect As ConfigurationSection
            Dim conf As Configuration
            conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            sect = conf.GetSection("connectionStrings")
            If Not sect Is Nothing Then
                If sect.IsReadOnly = False Then
                    sect.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
                    sect.SectionInformation.ForceSave = True
                    conf.Save(ConfigurationSaveMode.Full)
                End If
            End If
        End Sub

        Private Shared Sub DeleteConfiguration(ByVal Name As ConnectionStringSettings)
            config.ConnectionStrings.ConnectionStrings.Remove(Name)
            config.Save(ConfigurationSaveMode.Full)
        End Sub

        Public Shared Function AddAppSettings(ByVal Name As String, ByVal value As String) As Boolean

            If config.AppSettings.Settings(Name) IsNot Nothing Then
                RemoveAppSettings(Name)
            End If
            config.AppSettings.Settings.Add(Name, value)
            config.Save(ConfigurationSaveMode.Full)
            AddAppSettings = True

        End Function
        Private Shared Sub RemoveAppSettings(ByVal Name As String)
            config.AppSettings.Settings.Remove(Name)
            config.Save(ConfigurationSaveMode.Full)
        End Sub
    End Class
End Namespace