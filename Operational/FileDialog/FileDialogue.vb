Imports System.Windows.Forms

Namespace FileDialog

    Public Class FileDialogue
        Public Shared FileExtension As String = "bak"

        Public Shared Function SaveAsFile() As String
            Dim fileD As New SaveFileDialog
            fileD.Filter = "BackUp Files | *." + FileExtension
            If fileD.ShowDialog() = DialogResult.OK Then
                Return fileD.FileName
            Else
                Return ""
            End If
        End Function

        Public Shared Function OpenFile() As String
            Dim fileD As New OpenFileDialog
            fileD.Filter = "BackUp Files | *." + FileExtension
            If fileD.ShowDialog() = DialogResult.OK Then
                Return fileD.FileName
            Else
                Return ""
            End If
        End Function

    End Class

End Namespace