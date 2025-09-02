Imports System.Runtime.InteropServices
Imports System
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic

Namespace Register

    Public Class MD5
        Private Const BLOCK_SIZE_BYTES As Integer = 16
        Private Buffer As Array
        Private First As Array
        Private _CompanyName As String
        Private _Organization As String
        Private _KEY As Byte()
        Private _IV As Byte()
        Dim Encode As Encoding = Encoding.Default
        Dim MDS5 As System.Security.Cryptography.MD5
        Dim encryptStream As CryptoStream
        Dim rijndael As RijndaelManaged
        Dim rijndaelEncrypt As ICryptoTransform
        Dim memStream As MemoryStream

        Public WriteOnly Property CompanyName()
            Set(ByVal value)
                _CompanyName = value
            End Set
        End Property

        Public WriteOnly Property Organization()
            Set(ByVal value)
                _Organization = value
            End Set
        End Property

        Private Function DisplayArray(ByVal arr As Array) As Array
            Dim loopX As Integer
            Dim X As Integer = 0
            Dim Rarr As Byte() = Nothing
            ReDim Rarr(arr.Length)
            For loopX = arr.Length - 1 To 0 Step -1
                Rarr(X) = arr(loopX)
                X += 1
            Next loopX
            Return Rarr
        End Function


        Private Function GenerateKey() As String
            Dim Rstring As String
            Dim Name As String = Me._CompanyName & "-@#^&"
            PRIVATE_KEY(Name)
            MDS5 = Nothing
            MDS5 = System.Security.Cryptography.MD5.Create(Name)
            First = Encode.GetBytes(Name)
            Buffer = DisplayArray(First)
            memStream = New MemoryStream
            Try
                If Name.Length > 0 Then
                    rijndael = New RijndaelManaged()
                    rijndael.Key = Me._KEY
                    rijndael.IV = Me._IV
                    rijndaelEncrypt = rijndael.CreateEncryptor
                    encryptStream = New CryptoStream(memStream, rijndaelEncrypt, CryptoStreamMode.Write)

                    encryptStream.Write(Buffer, 0, Buffer.Length)
                    encryptStream.FlushFinalBlock()
                    Rstring = Convert.ToBase64String(memStream.ToArray())
                    Rstring = Rstring.Replace("+", "")
                    Rstring = Rstring.Replace("/", "")
                    Rstring = Rstring.Replace("-", "")
                    Rstring = Rstring.Replace("=", "")

                    If Rstring.Length > 20 Then
                        Rstring = Rstring.Substring(0, 20).ToUpper
                    End If
                Else
                    Rstring = ""
                End If

            Finally
                If rijndael IsNot Nothing Then rijndael.Clear()
                If rijndaelEncrypt IsNot Nothing Then rijndaelEncrypt.Dispose()
                If memStream IsNot Nothing Then memStream.Close()
            End Try
            Return Rstring.ToUpper
        End Function

        Private Sub PRIVATE_KEY(ByVal SecretPhrase As String)
            ReDim Me._KEY(23)
            ReDim Me._IV(15)
            Dim bytePhrase As Byte() = Encoding.UTF32.GetBytes(SecretPhrase)
            Dim SHA512 As New SHA512Managed
            SHA512.ComputeHash(bytePhrase)
            Dim RESULT As Byte() = SHA512.Hash
            For loops As Integer = 0 To 23
                Me._KEY(loops) = RESULT(loops)
            Next
            For loops As Integer = 24 To 15
                Me._IV(loops - 24) = RESULT(loops)
            Next
        End Sub

        Public Overridable Function KeyGenerate() As String
            Dim RString As String = ""
            RString = Me.GenerateKey
            If RString.Length > 0 Then RString = RString.Substring(0, 5) + "-" + RString.Substring(5, 5) + "-" + RString.Substring(10, 5) + "-" + RString.Substring(15, 5)
            Return RString
        End Function

        Public Function CHECK_GENERATE_KEY(ByVal Key As String) As Boolean
            Return (GenerateKey() = Key)
        End Function

        Public Overridable Function GenerateKey(ByVal KEY As String) As String
            Dim Rstring As String
            PRIVATE_KEY(KEY)
            MDS5 = Nothing
            MDS5 = System.Security.Cryptography.MD5.Create(KEY)
            First = Encode.GetBytes(KEY)
            Buffer = DisplayArray(First)
            memStream = New MemoryStream
            Try
                If KEY.Length > 0 Then
                    rijndael = New RijndaelManaged()
                    rijndael.Key = Me._KEY
                    rijndael.IV = Me._IV
                    rijndaelEncrypt = rijndael.CreateEncryptor
                    encryptStream = New CryptoStream(memStream, rijndaelEncrypt, CryptoStreamMode.Write)

                    encryptStream.Write(Buffer, 0, Buffer.Length)
                    encryptStream.FlushFinalBlock()
                    Rstring = Convert.ToBase64String(memStream.ToArray())
                    Rstring = Rstring.Replace("+", "")
                    Rstring = Rstring.Replace("/", "")
                    Rstring = Rstring.Replace("-", "")
                    Rstring = Rstring.Replace("=", "")

                    If Rstring.Length > 20 Then
                        Rstring = Rstring.Substring(0, 20).ToUpper
                    End If
                Else
                    Rstring = ""
                End If

            Finally
                If rijndael IsNot Nothing Then rijndael.Clear()
                If rijndaelEncrypt IsNot Nothing Then rijndaelEncrypt.Dispose()
                If memStream IsNot Nothing Then memStream.Close()
            End Try
            Return Rstring.ToUpper
        End Function

        Public Function Check_Encrypt_Key(ByVal Key As String) As Boolean
            Return (GenerateKey(GenerateKey().ToString) = Key)
        End Function

        Public Function EncryptKey() As String
            Dim RString As String = ""
            RString = GenerateKey(GenerateKey())
            If RString.Length > 0 Then RString = RString.Substring(0, 5) + "-" + RString.Substring(5, 5) + "-" + RString.Substring(10, 5) + "-" + RString.Substring(15, 5)
            Return RString
        End Function
    End Class


End Namespace