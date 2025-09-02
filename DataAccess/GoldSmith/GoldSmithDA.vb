Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace GoldSmith
    Public Class GoldSmithDA
        Implements IGoldSmithDA

#Region "Private Location"

        Private DB As Database
        Private Shared ReadOnly _instance As IGoldSmithDA = New GoldSmithDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGoldSmithDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteGoldSmith(GoldSmithID As String) As Boolean Implements IGoldSmithDA.DeleteGoldSmith
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_GoldSmith SET IsDelete=1 WHERE  GoldSmithID= @GoldSmithID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, GoldSmithID)
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

        Public Function GetAllGoldSmith() As DataTable Implements IGoldSmithDA.GetAllGoldSmith
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select IsInactive as [$Inactive],GoldSmithID as [@GoldSmithID],GoldSmithCode,Name as [Name_],Address as [Address_],Phone,Remark  as [Remark_] from tbl_GoldSmith where IsDelete=0 Order By GoldSmithID DESC "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetDefaultGoldSmith() As DataTable Implements IGoldSmithDA.GetDefaultGoldSmith
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select GoldSmithID as [@GoldSmithID],GoldSmithCode,Name as [Name_] from tbl_GoldSmith where Name='DEFAULT'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetGoldSmith() As DataTable Implements IGoldSmithDA.GetGoldSmith

        End Function

        Public Function GetGoldSmithByID(GoldSmithID As String) As GoldSmithInfo Implements IGoldSmithDA.GetGoldSmithByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGoldSmith As New GoldSmithInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_GoldSmith WHERE GoldSmithID = @GoldSmithID and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, GoldSmithID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGoldSmith
                        .GoldSmithID = drResult("GoldSmithID")
                        .GoldSmithCode = drResult("GoldSmithCode")
                        .Name = drResult("Name")
                        .Address = drResult("Address")
                        .Phone = drResult("Phone")
                        .Remark = drResult("Remark")
                        .IsInactive = drResult("IsInactive")

                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGoldSmith
        End Function

        Public Function GetGoldSmithCode(_GoldSmithCode As String) As GoldSmithInfo Implements IGoldSmithDA.GetGoldSmithCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New GoldSmithInfo
            Try
                strCommandText = "   SELECT  * FROM tbl_GoldSmith WHERE GoldSmithCode= @GoldSmithCode And IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldSmithCode", DbType.String, _GoldSmithCode)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .GoldSmithID = drResult("GoldSmithID")
                        .GoldSmithCode = drResult("GoldSmithCode")
                        .Name = drResult("Name")
                        .Address = drResult("Address")
                        .Phone = drResult("Phone")
                        .Remark = drResult("Remark")
                        .IsInactive = drResult("IsInactive")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertGoldSmith(GoldSmithObj As GoldSmithInfo) As Boolean Implements IGoldSmithDA.InsertGoldSmith
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_GoldSmith ( GoldSmithID,GoldSmithCode,Name,Address,Phone,Remark,IsInactive,IsDelete,LastModifiedDate,IsUpload,LocationID)"
                strCommandText += " Values (@GoldSmithID,@GoldSmithCode,@GoldSmithName,@GoldSmithAddress,@Phone,@Remark,@IsInactive,0,getDate(),0,@LocationID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, GoldSmithObj.GoldSmithID)
                DB.AddInParameter(DBComm, "@GoldSmithCode", DbType.String, GoldSmithObj.GoldSmithCode)
                DB.AddInParameter(DBComm, "@GoldSmithName", DbType.String, GoldSmithObj.Name)
                DB.AddInParameter(DBComm, "@GoldSmithAddress", DbType.String, GoldSmithObj.Address)
                DB.AddInParameter(DBComm, "@Phone", DbType.String, GoldSmithObj.Phone)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, GoldSmithObj.Remark)
                DB.AddInParameter(DBComm, "@IsInactive", DbType.Boolean, GoldSmithObj.IsInactive)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)

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

        Public Function UpdateGoldSmith(GoldSmithObj As GoldSmithInfo) As Boolean Implements IGoldSmithDA.UpdateGoldSmith
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_GoldSmith set  GoldSmithID= @GoldSmithID , GoldSmithCode= @GoldSmithCode , Name= @Name , Address= @Address , Phone= @Phone , Remark= @Remark , IsInactive= @IsInactive ,LastModifiedDate=getDate(),IsUpload=0,LocationID=@LocationID "
                strCommandText += " where GoldSmithID= @GoldSmithID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, GoldSmithObj.GoldSmithID)
                DB.AddInParameter(DBComm, "@GoldSmithCode", DbType.String, GoldSmithObj.GoldSmithCode)
                DB.AddInParameter(DBComm, "@Name", DbType.String, GoldSmithObj.Name)
                DB.AddInParameter(DBComm, "@Address", DbType.String, GoldSmithObj.Address)
                DB.AddInParameter(DBComm, "@Phone", DbType.String, GoldSmithObj.Phone)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, GoldSmithObj.Remark)
                DB.AddInParameter(DBComm, "@IsInactive", DbType.Boolean, GoldSmithObj.IsInactive)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)

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
        Public Function GetAllGoldSmithList() As System.Data.DataTable Implements IGoldSmithDA.GetAllGoldSmithList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT GoldSmithID AS [@GoldSmithID],GoldSmithCode,Name as [Name_],Address AS [Address_], Phone,Remark AS [Remark_] from tbl_GoldSmith where IsDelete=0 Order by GoldSmithID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllGoldSmithListByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IGoldSmithDA.GetAllGoldSmithListByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT GoldSmithID AS [@GoldSmithID],GoldSmithCode,Name as [Name_],Address AS [Address_], Phone,Remark AS [Remark_] from tbl_GoldSmith where IsDelete=0 and LocationID=@LocationID Order by GoldSmithID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetGoldSmithNameListByGoldSmithID(ByVal GoldSmithID As String) As System.Data.DataTable Implements IGoldSmithDA.GetGoldSmithNameListByGoldSmithID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT GoldSmithID, Name as [Name_]" & _
                " FROM tbl_GoldSmith WHERE GoldSmithID=@GoldSmithID and IsDelete=0" & _
                " ORDER BY GoldSmithID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, GoldSmithID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetGoldSmithNameListByStock(ByVal GoldSmithID As String) As System.Data.DataTable Implements IGoldSmithDA.GetGoldSmithNameListByStock
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * from tbl_ForSale " & _
                " WHERE GoldSmithID=@GoldSmithID and IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, GoldSmithID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

