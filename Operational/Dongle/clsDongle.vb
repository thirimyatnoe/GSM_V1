Imports System.Management
Imports System.IO
Public Class clsDongle

    Dim EncryptKey As String = ""
    Dim strGetKey As String = ""
    Dim strSerialKey As String = ""
    Dim strNumberKey As String = ""
    Dim strGlobalKey As String = ""
    Dim strEncrypt As String = ""
    Dim strGenerateKey As String = ""
    Friend Const gtBACKSLASH As String = "\"
    Public Function GetVolume() As Boolean
        'Dim gv As New Volume.GetVol()
        Dim objCrypto As New Cryptography.DACrypto()
        Dim mykey As String = ""
        Dim FLAG As Boolean = False
        With My.Computer.FileSystem
            For i As Integer = 0 To .Drives.Count - 1
                'sb.ClearText()
                Dim str As String = ""

                str = .Drives(i).DriveType.ToString
                If str = "Fixed" Then
                    str = "Local Disk"
                Else
                    str = "Removable Disk"
                End If
                If str = "Removable Disk" Then
                    Try
                        mykey = RemoveTrailingChar(.Drives(i).Name, gtBACKSLASH)
                        Dim gv As New Volume.GetVol()
                        strGetKey = gv.GetVolumeSerial(mykey.Substring(0, 1))
                        strNumberKey = GetSerialNumber(mykey)
                        strGlobalKey = "globalwavetechnology"
                        strEncrypt = objCrypto.Encrypt(strGetKey.Trim & strNumberKey.Trim & strGlobalKey.Trim)

                        strGenerateKey = GetHex(strEncrypt)

                        If IO.File.Exists(.Drives(i).Name & "license.txt") Then
                            EncryptKey = IO.File.ReadAllText(.Drives(i).Name & "license.txt")
                        End If


                        If EncryptKey = strGenerateKey.Trim Then
                            FLAG = True
                            Exit For
                        Else
                            FLAG = False
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Next


        End With


        If FLAG = True Then
            'MsgBox("Successfully Found", MsgBoxStyle.Information, "Dongle")
            Return True
        Else
            ' MsgBox("Not Successfully Found", MsgBoxStyle.Information, "Dongle")
            Return False
        End If
    End Function
    Public Function RemoveTrailingChar(ByVal InString As String, _
                                            ByVal TrailingChar As String) As String
        '---------------------------------------------------------------------------------
        ' Remove one or more occurrences of one or more trailing characters from a string
        '---------------------------------------------------------------------------------
        '     Date          Developer                      Code Change
        '  ---------- -------------------- -----------------------------------------------
        '  12/16/2007 G Gilbert            Original code
        '---------------------------------------------------------------------------------

        Do While InString.Length >= TrailingChar.Length _
        AndAlso InString.EndsWith(TrailingChar)
            Select Case InString.Length
                Case TrailingChar.Length
                    InString = ""
                Case Else
                    InString = InString.Substring(0, InString.Length - TrailingChar.Length)
            End Select
        Loop
        Return InString

    End Function
    Public Function GetSerialNumber(ByVal DriveLetter As String) As String
        Dim wmi_ld, wmi_dp, wmi_dd As ManagementObject
        Dim temp, parts(), ans As String

        ans = ""
        ' get the Logical Disk for that drive letter
        wmi_ld = New ManagementObject("Win32_LogicalDisk.DeviceID='" & _
         DriveLetter.TrimEnd("\"c) & "'")
        ' get the associated DiskPartition 
        For Each wmi_dp In wmi_ld.GetRelated("Win32_DiskPartition")
            ' get the associated DiskDrive
            For Each wmi_dd In wmi_dp.GetRelated("Win32_DiskDrive")
                ' There is a bug in WinVista that corrupts some of the fields
                ' of the Win32_DiskDrive class if you instantiate the class via
                ' its primary key (as in the example above) and the device is
                ' a USB disk. Oh well... so we have go thru this extra step
                Dim wmi As New ManagementClass("Win32_DiskDrive")
                ' loop thru all of the instances. This is silly, we shouldn't
                ' have to loop thru them all, when we know which one we want.
                For Each obj As ManagementObject In wmi.GetInstances
                    ' do the DeviceID fields match?
                    If obj("DeviceID").ToString = wmi_dd("DeviceID").ToString Then
                        ' the serial number is embedded in the PnPDeviceID
                        temp = obj("PnPDeviceID").ToString
                        If Not temp.StartsWith("USBSTOR") Then
                            Throw New ApplicationException(DriveLetter & " doesn't appear to be USB Device")
                        End If
                        parts = temp.Split("\&".ToCharArray)
                        ' The serial number should be the next to the last element
                        ans = parts(parts.Length - 2)
                    End If
                Next
            Next
        Next

        Return ans
    End Function

    Public Function GetHex(ByVal strDriveLetter As String) As String
        'Dim key As Long = CType(strDriveLetter, Long)
        'Dim str As String = Hex(strDriveLetter)
        Dim str As String = strDriveLetter
        str = str.Replace("+", "")
        str = str.Replace("=", "")
        str = str.Remove(20)        '40
        str = UCase(str)
        Dim i As Integer = str.Length / 5
        Dim y As Integer = 5
        For x As Integer = 0 To i - 1
            If y < 23 Then   '43
                str = str.Insert(y, "-")
                y = y + 6
            End If

        Next
        Return str

        'If str.Length <= 4 Then
        '    Return str
        'ElseIf str.Length = 5 Then
        '    str = "000" & str
        '    Return str.Insert(4, "-")
        'ElseIf str.Length = 6 Then
        '    str = "00" & str
        '    Return str.Insert(4, "-")
        'ElseIf str.Length = 7 Then
        '    str = "0" & str
        '    Return str.Insert(4, "-")
        'ElseIf str.Length = 56 Then


        '    'Return str.Insert(5, "-")
        '    'GetHex(str)
        '    'Else
        '    '    'str = 12345678
        '    '    Dim i As Integer = str.Length - 4
        '    '    str = str.Insert(str.Length - 4, "-")
        '    '    Return str.Insert(i - 4, "-")
        'End If
    End Function
End Class
