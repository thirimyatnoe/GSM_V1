Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace PhotoPath
    Friend Class PhotoPathDA
        Implements IPhotoPathDA




#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As IPhotoPathDA = New PhotoPathDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IPhotoPathDA
            Get
                Return _instance
            End Get
        End Property

#End Region



        Public Function DeletePhotoPath() As Boolean Implements IPhotoPathDA.DeletePhotoPath
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_PhotoPath"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                'DB.AddInParameter(DBComm, "@PhotoPathID", DbType.String, PhotoPathID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Data Management System")
            End Try
        End Function

        Public Function GetPhotoPathByID() As CommonInfo.PhotoPathInfo Implements IPhotoPathDA.GetPhotoPathByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objPhotoPath As New PhotoPathInfo

            Try

                strCommandText = "SELECT PhotoPath FROM tbl_PhotoPath "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@PhotoPathID", DbType.String, PhotoPathID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objPhotoPath
                        '.PhotoPathID = drResult("PhotoPathID")
                        .PhotoPath = drResult("PhotoPath")
                        '.OneFinger = drResult("OneFinger")
                    End With
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Data Management System")
            End Try

            Return objPhotoPath
        End Function

        Public Function GetPhotoPathList() As System.Data.DataTable Implements IPhotoPathDA.GetPhotoPathList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "SELECT PhotoPath FROM tbl_PhotoPath "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Data Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertPhotoPath(ByVal PhtotoPathobj As CommonInfo.PhotoPathInfo) As Boolean Implements IPhotoPathDA.InsertPhotoPath
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = "INSERT INTO tbl_PhotoPath(PhotoPath) VALUES(@PhotoPath)"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                'DB.AddInParameter(DBComm, "@PhotoPathID", DbType.String, PhtotoPathobj.PhotoPathID)
                DB.AddInParameter(DBComm, "@PhotoPath", DbType.String, PhtotoPathobj.PhotoPath)
                'DB.AddInParameter(DBComm, "@OneFinger", DbType.Boolean, PhtotoPathobj.OneFinger)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Data Management System")
            End Try
        End Function

        Public Function UpdatePhotoPath(ByVal PhtotoPathobj As CommonInfo.PhotoPathInfo) As Boolean Implements IPhotoPathDA.UpdatePhotoPath
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = " UPDATE tbl_PhotoPath SET PhotoPath = @PhotoPath"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                'DB.AddInParameter(DBComm, "@PhotoPathID", DbType.String, PhtotoPathobj.PhotoPathID)
                DB.AddInParameter(DBComm, "@PhotoPath", DbType.String, PhtotoPathobj.PhotoPath)
                'DB.AddInParameter(DBComm, "@OneFinger", DbType.Boolean, PhtotoPathobj.OneFinger)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Data Management System")
            End Try
        End Function

        Public Function GetPhotoPathForOneFinger(ByVal OneFinger As Integer) As String Implements IPhotoPathDA.GetPhotoPathForOneFinger
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objPhotoPath As String = ""

            Try

                strCommandText = "  SELECT PhotoPath FROM tbl_PhotoPath WHERE OneFinger = @OneFinger "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OneFinger", DbType.Int32, OneFinger)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    objPhotoPath = drResult("PhotoPath")
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Data Management System")
            End Try
            Return objPhotoPath

        End Function
    End Class
End Namespace

