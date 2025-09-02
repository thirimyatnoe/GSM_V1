Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports System
Imports Microsoft.Win32

Namespace Register

    Public Class GWTRegistry
        'Inherits RegistryInfo


        'Private Const KeyName As String = "SOFTWARE\\ABS\\ABSTA"
        'Private Const SubKeyName As String = "Key"
        'Private Const SubKeyTried As String = "Tried"
        'Private Const SubOrganization As String = "Organization"
        'Private Const SoftWareName As String = "Registers"
        'Private Const SubSaveKey As String = "KeySave"

        'Private _Registry As RegistryKey
        'Private _DACrypto As New DACrypto

        'Public Function CheckRegistry() As Boolean
        '    Try
        '        _Registry = Registry.LocalMachine.OpenSubKey(KeyName)

        '        If _Registry Is Nothing Then
        '            Return False
        '        Else
        '            RegistryRead()
        '            Return True
        '        End If
        '    Catch ex As Exception
        '        Return False
        '    End Try

        'End Function

        'Public Function IsTry() As Boolean
        '    Return Me._Tried
        'End Function

        'Public Function RegistryCreate() As Boolean
        '    Try
        '        _Registry = Registry.LocalMachine.CreateSubKey(KeyName, RegistryKeyPermissionCheck.Default)
        '        _Registry = _Registry.CreateSubKey(SoftWareName)
        '        If _Registry IsNot Nothing Then
        '            _Registry.SetValue(SubKeyName, _DACrypto.Encrypt(Me._Key))
        '            _Registry.SetValue(SubKeyTried, Me._Tried)
        '            _Registry.SetValue(SubOrganization, Me._Organization)
        '            _Registry.SetValue(SubSaveKey, Me._SaveKey)
        '        End If
        '        Return True
        '    Catch ex As Exception
        '        Return False
        '    End Try

        'End Function

        'Public Function RegistryRead() As Boolean
        '    Try
        '        _Registry = Registry.LocalMachine.OpenSubKey(KeyName, RegistryKeyPermissionCheck.Default)
        '        _Registry = _Registry.OpenSubKey(SoftWareName, RegistryKeyPermissionCheck.Default)
        '        If _Registry IsNot Nothing Then
        '            Me._Key = _DACrypto.Decrypt(_Registry.GetValue(SubKeyName, ""))
        '            Me._Tried = _Registry.GetValue(SubKeyTried, False)
        '            Me._Organization = _Registry.GetValue(SubOrganization, "")
        '            Me._SaveKey = _Registry.GetValue(SubSaveKey, False)
        '            Return True
        '        Else
        '            Return False
        '        End If

        '    Catch ex As Exception
        '        Return False
        '    End Try
        'End Function
    End Class

End Namespace