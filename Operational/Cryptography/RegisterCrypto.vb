Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Namespace Cryptography

    Public Class RegisterCrypto
        Public Shared Function Encrypt(ByVal DataString As String, ByVal KeyString As String) As String
            Dim strResult As String = ""
            Dim objTripleDES As New TripleDESCryptoServiceProvider
            Try
                'Convert the input string to a byte array
                Dim bytInput() As Byte = Encoding.UTF32.GetBytes(DataString)
                Dim SHA512 As New SHA512Managed
                Dim bytIV(15) As Byte
                Dim bytKey(23) As Byte

                SHA512.ComputeHash(bytInput)
                Dim bytResult As Byte() = SHA512.Hash

                For i As Integer = 0 To 23
                    bytKey(i) = bytResult(i)
                Next
                For i As Integer = 0 To 15
                    bytIV(i) = bytResult(i + 24)
                Next

                'Instantiate a new instance of the MemoryStream class
                Using objOutputStream As New MemoryStream
                    'Encrypt the byte array
                    Dim objCryptoStream As New CryptoStream(objOutputStream, objTripleDES.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write)
                    objCryptoStream.Write(bytInput, 0, bytInput.Length)
                    objCryptoStream.FlushFinalBlock()

                    'Return the byte array as a Base64 string
                    strResult = Convert.ToBase64String(objOutputStream.ToArray())
                End Using
                Return strResult
            Catch ExceptionErr As Exception
                Return strResult
            End Try
        End Function
    End Class

End Namespace
