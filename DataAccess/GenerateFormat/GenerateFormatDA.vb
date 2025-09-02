Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace GenerateFormat
    Public Class GenerateFormatDA
        Implements IGenerateFormatDA

#Region "Private Member"
        Private DB As Database
        Private Shared ReadOnly _instance As IGenerateFormatDA = New GenerateFormatDA
#End Region
#Region "Constructor"
        Private Sub New()
            DB = DatabaseFactory.CreateDatabase
        End Sub
#End Region

#Region "Public Property"
        Public Shared ReadOnly Property Instance() As IGenerateFormatDA
            Get
                Return _instance
            End Get
        End Property
#End Region

        Public Function Insert_GenerateFormat(ByVal objGenerateFormat As CommonInfo.GenerateFormatInfo) As Boolean Implements IGenerateFormatDA.Insert_GenerateFormat
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "IF Exists (select * from tbl_GenerateFormat where GenerateFormatID=@GenerateFormatID) Update tbl_GenerateFormat set GenerateFormat=@GenerateFormat,Prefix=@Prefix,FormatDate1=@FormatDate1,FormatDate2=@FormatDate2,Prefix2=@Prefix2,NumberCount=@NumberCount,PrefixPlace=@PrefixPlace, IsGenerate=@IsGenerate  where GenerateFormatID=@GenerateFormatID Else "
                strCommandText += " Insert into tbl_GenerateFormat(GenerateFormatID,GenerateFormat,Prefix,FormatDate1,FormatDate2,Prefix2,NumberCount,PrefixPlace, IsGenerate) values (@GenerateFormatID,@GenerateFormat,@Prefix,@FormatDate1,@FormatDate2,@Prefix2,@NumberCount,@PrefixPlace,@IsGenerate)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GenerateFormatID", DbType.Int32, objGenerateFormat.GenerateFormatID)
                DB.AddInParameter(DBComm, "@GenerateFormat", DbType.String, objGenerateFormat.GenerateFormat)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, objGenerateFormat.Prefix)
                DB.AddInParameter(DBComm, "@FormatDate1", DbType.String, objGenerateFormat.FormatDate1)
                DB.AddInParameter(DBComm, "@FormatDate2", DbType.String, objGenerateFormat.FormatDate2)
                DB.AddInParameter(DBComm, "@Prefix2", DbType.String, objGenerateFormat.Prefix2)
                DB.AddInParameter(DBComm, "@NumberCount", DbType.String, objGenerateFormat.NumberCount)
                DB.AddInParameter(DBComm, "@PrefixPlace", DbType.String, objGenerateFormat.PrefixPlace)
                DB.AddInParameter(DBComm, "@IsGenerate", DbType.Boolean, objGenerateFormat.IsGenerate)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
        End Function

        Public Function GetDateFormat() As System.Data.DataTable Implements IGenerateFormatDA.GetDateFormat
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select distinct(FormatDate1) from tbl_GenerateFormat"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSecondDateFormat() As System.Data.DataTable Implements IGenerateFormatDA.GetSecondDateFormat
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select distinct(FormatDate2) from tbl_GenerateFormat"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function Get_GenerateFormat() As System.Data.DataTable Implements IGenerateFormatDA.Get_GenerateFormat
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select GenerateFormatID,GenerateFormat from tbl_GenerateFormat "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function Get_GenerateFormatByID(ByVal GenerateFormatID As Integer) As CommonInfo.GenerateFormatInfo Implements IGenerateFormatDA.Get_GenerateFormatByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGenerate As New CommonInfo.GenerateFormatInfo
            Try
                strCommandText = "Select * from tbl_GenerateFormat where GenerateFormatID = @GenerateFormatID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GenerateFormatID", DbType.Int32, GenerateFormatID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGenerate
                        .GenerateFormatID = drResult("GenerateFormatID")
                        .GenerateFormat = drResult("GenerateFormat")
                        .Prefix = drResult("Prefix")
                        .FormatDate1 = drResult("FormatDate1")
                        .FormatDate2 = drResult("FormatDate2")
                        .Prefix2 = drResult("Prefix2")
                        .NumberCount = drResult("NumberCount")
                        .PrefixPlace = drResult("PrefixPlace")

                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGenerate
        End Function

        Public Function Delete_GenerateFormat(ByVal GenerateFormatID As Integer) As Boolean Implements IGenerateFormatDA.Delete_GenerateFormat
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_GenerateFormat WHERE GenerateFormatID= @GenerateFormatID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@GenerateFormatID", DbType.Int64, GenerateFormatID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetGenerateFormatByFormat(GenerateFormat As String) As GenerateFormatInfo Implements IGenerateFormatDA.GetGenerateFormatByFormat
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGenerate As New CommonInfo.GenerateFormatInfo
            Try
                strCommandText = "Select * from tbl_GenerateFormat where GenerateFormat = @GenerateFormat "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GenerateFormat", DbType.String, GenerateFormat)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGenerate
                        .GenerateFormatID = drResult("GenerateFormatID")
                        .GenerateFormat = drResult("GenerateFormat")
                        .Prefix = drResult("Prefix")
                        .FormatDate1 = drResult("FormatDate1")
                        .FormatDate2 = drResult("FormatDate2")
                        .Prefix2 = drResult("Prefix2")
                        .NumberCount = drResult("NumberCount")
                        .PrefixPlace = drResult("PrefixPlace")
                        .IsGenerate = drResult("IsGenerate")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGenerate
        End Function
    End Class
End Namespace

