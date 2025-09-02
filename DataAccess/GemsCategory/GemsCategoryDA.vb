Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace GemsCategory
    Public Class GemsCategoryDA
        Implements IGemsCategoryDA


#Region "Private GemsCategory"
        Private DB As Database
        Private Shared ReadOnly _instance As IGemsCategoryDA = New GemsCategoryDA
#End Region
#Region "Constructors"
        Private Sub New()
            DB = DatabaseFactory.CreateDatabase
        End Sub
#End Region
#Region "Public Properties"
        Public Shared ReadOnly Property Instance() As IGemsCategoryDA
            Get
                Return _instance
            End Get
        End Property
#End Region


        Public Function DeleteGemsCategory(ByVal GemsCategoryID As String) As Boolean Implements IGemsCategoryDA.DeleteGemsCategory
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_GemsCategory SET IsDelete=1 WHERE  GemsCategoryID= @GemsCategoryID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, GemsCategoryID)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Delete ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetAllGemsCategory() As System.Data.DataTable Implements IGemsCategoryDA.GetAllGemsCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT GemsCategoryID AS [@GemsCategoryID],GemsCategory AS [GemsCategory_],GemTaxPer From tbl_GemsCategory where 1=1 and IsDelete=0 " & " Order by GemsCategoryID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetGemsCategory(ByVal GemsCategoryID As String) As CommonInfo.GemsCategoryInfo Implements IGemsCategoryDA.GetGemsCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGemsCategory As New GemsCategoryInfo
            Try
                strCommandText = "SELECT GemsCategoryID,GemsCategory,GemTaxPer,Prefix From tbl_GemsCategory WHERE GemsCategoryID = @GemsCategoryID and IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, GemsCategoryID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGemsCategory
                        .GemsCategoryID = drResult("GemsCategoryID")
                        .GemsCategory = drResult("GemsCategory")
                        .GemTaxPer = drResult("GemTaxPer")
                        .Prefix = drResult("Prefix")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGemsCategory
        End Function

        Public Function InsertGemsCategory(ByVal GemsCategoryObj As CommonInfo.GemsCategoryInfo) As Boolean Implements IGemsCategoryDA.InsertGemsCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                strCommandText = "Insert into tbl_GemsCategory (GemsCategoryID,GemsCategory,GemTaxPer,IsDelete,IsUpload,LocationID,LastModifiedDate,Prefix)"
                strCommandText += " Values (@GemsCategoryID,@GemsCategory,@GemTaxPer,0,0,@LocationID,getdate(),@Prefix)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, GemsCategoryObj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsCategory", DbType.String, GemsCategoryObj.GemsCategory)
                DB.AddInParameter(DBComm, "@GemTaxPer", DbType.String, GemsCategoryObj.GemTaxPer)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, GemsCategoryObj.Prefix)

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

        Public Function UpdateGemsCategory(ByVal GemsCategoryObj As CommonInfo.GemsCategoryInfo) As Boolean Implements IGemsCategoryDA.UpdateGemsCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try


                strCommandText = "Update tbl_GemsCategory set   GemsCategory= @GemsCategory ,GemTaxPer=@GemTaxPer,LocationID=@LocationID,IsUpload=0,LastModifiedDate=getdate(),Prefix=@Prefix"
                strCommandText += " where GemsCategoryID= @GemsCategoryID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, GemsCategoryObj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsCategory", DbType.String, GemsCategoryObj.GemsCategory)
                DB.AddInParameter(DBComm, "@GemTaxPer", DbType.String, GemsCategoryObj.GemTaxPer)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, GemsCategoryObj.Prefix)

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
        Public Function GetAllGemsCategoryForGridCombo() As System.Data.DataTable Implements IGemsCategoryDA.GetAllGemsCategoryForGridCombo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT GemsCategoryID AS [@GemsCategoryID],GemsCategory As GemsCategory,GemTaxPer From tbl_GemsCategory where IsDelete=0 Order by GemsCategory asc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllGemsCategoryFromSearchBox() As System.Data.DataTable Implements IGemsCategoryDA.GetAllGemsCategoryFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT GemsCategoryID,GemsCategory AS [GemsCategory_],GemTaxPer From tbl_GemsCategory where IsDelete=0 Order by GemsCategoryID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetrptGemsCategory() As DataTable Implements IGemsCategoryDA.GetrptGemsCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT GemsCategoryID, GemsCategory, StoneType From tbl_GemsCategory where IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetGemsCategoryByGemsCategory(ByVal GemsCategory As String, Optional ByVal GemsCategoryID As String = "") As DataTable Implements IGemsCategoryDA.GetGemsCategoryByGemsCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim CriStr As String = ""
            If GemsCategoryID = "" Then
                CriStr = " And GemsCategory=@GemsCategory "
            Else
                CriStr = " And GemsCategory=@GemsCategory AND GemsCategoryID<>@GemsCategoryID "
            End If

            Try
                strCommandText = "SELECT * From tbl_GemsCategory where IsDelete=0" & CriStr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsCategory", DbType.String, GemsCategory)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function HasPrefixForUpdate(ByVal prefix As String, ByVal OldPrefix As String) As System.Data.DataTable Implements IGemsCategoryDA.HasPrefixForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_GemsCategory where IsDelete=0 "
                strCommandText += " and (Prefix= @Prefix )   and Prefix<>'" & OldPrefix & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, prefix)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class

End Namespace
