Imports System
Imports System.Text
Imports System.Runtime.InteropServices


Namespace Volume

    Public Class GetVol
        <DllImport("kernel32.dll")> _
        Private Shared Function GetVolumeInformation(ByVal PathName As String, ByVal VolumeNameBuffer As StringBuilder, ByVal VolumeNameSize As UInt32, ByRef VolumeSerialNumber As UInt32, ByRef MaximumComponentLength As UInt32, ByRef FileSystemFlags As UInt32, _
         ByVal FileSystemNameBuffer As StringBuilder, ByVal FileSystemNameSize As UInt32) As Long
        End Function
        ''' <summary>
        ''' Get Volume Serial Number as string
        ''' </summary>
        ''' <param name="strDriveLetter">Single letter (e.g., "C")</param>
        ''' <returns>string representation of Volume Serial Number</returns>
        Public Function GetVolumeSerial(ByVal strDriveLetter As String) As String
            Dim serNum As UInteger = 0
            Dim maxCompLen As UInteger = 0
            Dim VolLabel As New StringBuilder(256)
            ' Label
            Dim VolFlags As New UInt32()
            Dim FSName As New StringBuilder(256)
            ' File System Name
            strDriveLetter += ":\"
            'Dim Ret As Long = GetVolumeInformation(strDriveLetter, VolLabel, DirectCast(VolLabel.Capacity, UInt32), serNum, maxCompLen, VolFlags, FSName, DirectCast(FSName.Capacity, UInt32))

            Dim Ret As Long = GetVolumeInformation(strDriveLetter, VolLabel, VolLabel.Capacity, serNum, maxCompLen, VolFlags, FSName, FSName.Capacity)
            Dim str As String = Hex(Convert.ToString(serNum))
            Return str
            'Dim str As String = serNum
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
            'ElseIf str.Length = 8 Then
            '    Return str.Insert(4, "-")
            'Else
            '    'str = 12345678
            '    Dim i As Integer = str.Length - 4
            '    str = str.Insert(str.Length - 4, "-")
            '    Return str.Insert(i - 4, "-")
            'End If
        End Function
    End Class
End Namespace
