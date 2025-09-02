Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Namespace Cryptography


    Public Class DACrypto
        Implements IDisposable

        Dim Encode As Encoding = Encoding.Default
        Dim MDS5 As System.Security.Cryptography.MD5
        Dim encryptStream As CryptoStream
        Dim rijndael As RijndaelManaged
        Dim rijndaelEncrypt As ICryptoTransform
        Dim memStream As MemoryStream
        Private _KEY As Byte()
        Private _IV As Byte()
        Private Buffer As Array
        Private First As Array

        'Private variables and objects
        Private bytKey() As Byte
        Private bytIV() As Byte
        Private bytInput() As Byte

        Private objTripleDES As TripleDESCryptoServiceProvider
        Private objOutputStream As MemoryStream
        Private regKey As String = "SOFTWARE\ABS\ABSTA\Database"
        Private disposed As Boolean = False

        Public Sub New()
            bytKey = System.Text.Encoding.UTF8.GetBytes("W7359p9KRcFoALFWH6hLUIyO")
            bytIV = System.Text.Encoding.UTF8.GetBytes("Vmeb2I1e=")

            'Instantiate a new instance of the TripleDESCryptoServiceProvider class
            objTripleDES = New TripleDESCryptoServiceProvider
        End Sub

        ' IDisposable
        Private Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then
                    ' TODO: put code to dispose managed resources
                End If

                'Clean up
                objTripleDES = Nothing
            End If
            Me.disposed = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overrides Sub Finalize()
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(False)
            MyBase.Finalize()
        End Sub
#End Region

        Public Function Encrypt(ByVal strToEncrypt As String) As String
            Try
                'Convert the input string to a byte array
                Dim bytInput() As Byte = Encoding.UTF8.GetBytes(strToEncrypt)

                'Instantiate a new instance of the MemoryStream class
                Using objOutputStream As New MemoryStream
                    'Encrypt the byte array
                    Dim objCryptoStream As New CryptoStream(objOutputStream, objTripleDES.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write)
                    objCryptoStream.Write(bytInput, 0, bytInput.Length)
                    objCryptoStream.FlushFinalBlock()

                    'Return the byte array as a Base64 string
                    Encrypt = Convert.ToBase64String(objOutputStream.ToArray())
                End Using

            Catch ExceptionErr As Exception
                Throw New System.Exception(ExceptionErr.Message, _
                    ExceptionErr.InnerException)
            End Try
        End Function

        Public Function Decrypt(ByVal strToDecrypt As String) As String
            Try
                'Convert the input string to a byte array
                Dim inputByteArray() As Byte = Convert.FromBase64String(strToDecrypt)

                'Instantiate a new instance of the MemoryStream class
                Using objOutputStream As New MemoryStream
                    'Decrypt the byte array
                    Dim objCryptoStream As New CryptoStream(objOutputStream, objTripleDES.CreateDecryptor(bytKey, bytIV), CryptoStreamMode.Write)
                    objCryptoStream.Write(inputByteArray, 0, inputByteArray.Length)
                    objCryptoStream.FlushFinalBlock()

                    'Return the byte array as a string
                    Decrypt = Encoding.UTF8.GetString(objOutputStream.ToArray())
                End Using

            Catch ExceptionErr As Exception
                Throw New System.Exception(ExceptionErr.Message, _
                    ExceptionErr.InnerException)
            End Try
        End Function

        Public Function ReadReg() As String
            'Declare variables
            Dim objReg As Microsoft.Win32.RegistryKey

            Try
                objReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKey, False)
                ReadReg = Decrypt(objReg.GetValue("DBString"))

            Catch ExceptionErr As Exception
                ReadReg = ""
            Finally
                objReg = Nothing
            End Try

        End Function

        Public Function WriteReg(ByVal MyValue As String) As String
            Dim objReg As Microsoft.Win32.RegistryKey

            Try
                'Try to open the key with write permissions
                objReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKey, True)

                If objReg Is Nothing Then
                    'Create the registry key
                    objReg = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(regKey)
                End If

                objReg.SetValue("DBString", Encrypt(MyValue))

                'Close the registry key
                objReg.Close()

                WriteReg = MyValue
            Catch SecurityExceptionErr As Security.SecurityException
                WriteReg = "ERROR"
            Finally
                objReg = Nothing
            End Try
            Return WriteReg

        End Function

        Public Function EncryptKey(ByVal Key As String) As String
            Dim RString As String = ""
            Dim RefKey As String = ""
            Dim LocalKey As String = ""
            Dim LocalKey2 As String = ""

            'RefKey = Key.Substring(0, 15)
            'LocalKey = Key.Substring(15, 5)
            'RefKey = Key.Substring(0, Key.Length - 10)
            'LocalKey = Key.Substring(RefKey.Length, 5)
            'LocalKey = EncryptCharToHex(LocalKey)

            'LocalKey2 = Key.Substring(RefKey.Length + 5, 5)
            'LocalKey2 = EncryptCharToHex(LocalKey2)

            RString = GenerateKey(Key)
            Return RString
        End Function

        Public Function EncryptKeyMulti(ByVal Key As String) As String
            Dim RString As String = ""
            Dim RefKey As String = ""
            Dim LocalKey As String = ""
            Dim LocalKey2 As String = ""

            RefKey = Key.Substring(0, 15)
            'LocalKey = Key.Substring(15, 5)
            RefKey = Key.Substring(0, Key.Length - 10)
            LocalKey = Key.Substring(RefKey.Length, 5)
            LocalKey = EncryptCharToHex(LocalKey)

            LocalKey2 = Key.Substring(RefKey.Length + 5, 5)
            LocalKey2 = EncryptCharToHex(LocalKey2)

            RString = GenerateKey(RefKey & LocalKey & LocalKey2)
            Return RString
        End Function

        Public Function GetBase36(ByVal Base10Value As Long) As String
            Dim basestr As String = "A32B1C0DEFGHIJKLMNOPQRSTUVWXYZ456789"
            Dim retstr As String = ""
            Dim tmp As Long = 0
            Dim modval As Long = 36

            While Base10Value > 0
                tmp = Base10Value Mod modval
                retstr = basestr.Chars(tmp / (modval / 36)) + retstr
                modval *= 36
                Base10Value -= tmp
            End While
            Return retstr
        End Function

        Public Function GetBase10(ByVal Base36Value As String) As Long
            Dim basestr As String = "A32B1C0DEFGHIJKLMNOPQRSTUVWXYZ456789"
            Dim ret As Long = 0
            Dim i As Integer

            For i = 0 To Base36Value.Length - 1
                ret = ret + basestr.IndexOf(Base36Value.Chars(i)) * System.Math.Pow(36, Base36Value.Length - (i + 1))
            Next
            Return ret
        End Function

        Public Function EncryptHexToChar(ByVal Key As String) As String
            Dim RString As String = ""
            RString = Convert.ToChar(Convert.ToInt16(Key.Substring(0, 1), 16) + 65).ToString & _
                                    Convert.ToChar(Convert.ToInt16(Key.Substring(1, 1), 16) + 65).ToString & _
                                    Convert.ToChar(Convert.ToInt16(Key.Substring(2, 1), 16) + 65).ToString & _
                                    Convert.ToChar(Convert.ToInt16(Key.Substring(3, 1), 16) + 65).ToString & _
                                    Convert.ToChar(Convert.ToInt16(Key.Substring(4, 1), 16) + 65).ToString
            Return RString
        End Function

        Public Function EncryptCharToHex(ByVal Key As String) As String
            Dim RString As String = ""
            RString = CStr(Hex(Convert.ToInt64(CChar(Key.Substring(0, 1))) - 65)) & _
                        CStr(Hex(Convert.ToInt64(CChar(Key.Substring(1, 1))) - 65)) & _
                        CStr(Hex(Convert.ToInt64(CChar(Key.Substring(2, 1))) - 65)) & _
                        CStr(Hex(Convert.ToInt64(CChar(Key.Substring(3, 1))) - 65)) & _
                        CStr(Hex(Convert.ToInt64(CChar(Key.Substring(4, 1))) - 65))



            Return RString
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

        Private Function GenerateKey(ByVal CreateKey As String) As String
            Dim Rstring As String
            Dim Name As String = CreateKey '& "-@#^&"
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

                    'If Rstring.Length > 20 Then
                    '    Rstring = Rstring.Substring(0, 15).ToUpper
                    'End If
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

        Public Function KeyGenerateSingle(ByVal CreateKey As String) As String
            Dim RString As String = ""

            RString = Me.GenerateKey(CreateKey)
            If RString.Length > 0 Then RString = RString.Substring(0, 5) + "-" + RString.Substring(5, 5) + "-" + RString.Substring(10, 5) + "-" + RString.Substring(15, 5)

            Return RString
        End Function
        Public Function KeyGenerate(ByVal CreateKey As String) As String
            Dim RString As String = ""
            Dim EncryptKey As String = ""
            Dim LocalKey As String = ""
            Dim LocalKey2 As String = ""

            'EncryptKey = CreateKey.Substring(0, 20)
            'LocalKey = CreateKey.Substring(21, 5)
            EncryptKey = CreateKey.Substring(0, CreateKey.Length - 12)
            LocalKey = CreateKey.Substring(EncryptKey.Length + 1, 5)
            LocalKey = EncryptHexToChar(LocalKey)

            LocalKey2 = CreateKey.Substring(EncryptKey.Length + 6 + 1, 5)
            LocalKey2 = EncryptHexToChar(LocalKey2)

            RString = Me.GenerateKey(EncryptKey)
            If RString.Length > 0 Then RString = RString.Substring(0, 5) + "-" + RString.Substring(5, 5) + "-" + RString.Substring(10, 5) + "-" + LocalKey + "-" + LocalKey2

            Return RString
        End Function


    End Class

End Namespace