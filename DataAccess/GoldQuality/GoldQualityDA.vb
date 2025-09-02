Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace GoldQuality
    Public Class GoldQualityDA
        Implements IGoldQualityDA

#Region "Private GoldQuality"

        Private DB As Database
        Private Shared ReadOnly _instance As IGoldQualityDA = New GoldQualityDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGoldQualityDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function InsertGoldQuality(ByVal Obj As CommonInfo.GoldQualityInfo) As Boolean Implements IGoldQualityDA.InsertGoldQuality
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_GoldQuality (GoldQualityID,GoldQuality,Prefix,IsGramRate, MultiplyBy, DividedBy, IsSolidGold,IsDelete,IsUpload,LocationID,LastModifiedDate,BarcodeStatus)"
                strCommandText += " Values (@GoldQualityID,@GoldQuality,@Prefix,@IsGramRate, @MultiplyBy, @DividedBy, @IsSolidGold,0,0,@LocationID,getdate(),@BarcodeStatus)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@GoldQuality", DbType.String, Obj.GoldQuality)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, Obj.Prefix)
                DB.AddInParameter(DBComm, "@IsGramRate", DbType.Boolean, Obj.IsGramRate)
                DB.AddInParameter(DBComm, "@MultiplyBy", DbType.Decimal, Obj.MultiplyBy)
                DB.AddInParameter(DBComm, "@DividedBy", DbType.Decimal, Obj.DividedBy)
                DB.AddInParameter(DBComm, "@IsSolidGold", DbType.Boolean, Obj.IsSolidGold)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@BarcodeStatus", DbType.Int32, Obj.BarcodeStatus)
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

        Public Function UpdateGoldQuality(ByVal Obj As CommonInfo.GoldQualityInfo) As Boolean Implements IGoldQualityDA.UpdateGoldQuality
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_GoldQuality set  GoldQualityID= @GoldQualityID , GoldQuality= @GoldQuality , Prefix= @Prefix  ,IsGramRate=@IsGramRate, MultiplyBy=@MultiplyBy, DividedBy=@DividedBy, IsSolidGold=@IsSolidGold,LocationID=@LocationID,IsUpload=0,LastModifiedDate=getdate(),BarcodeStatus=@BarcodeStatus"
                strCommandText += " where GoldQualityID= @GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@GoldQuality", DbType.String, Obj.GoldQuality)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, Obj.Prefix)
                DB.AddInParameter(DBComm, "@IsGramRate", DbType.Boolean, Obj.IsGramRate)
                DB.AddInParameter(DBComm, "@MultiplyBy", DbType.Decimal, Obj.MultiplyBy)
                DB.AddInParameter(DBComm, "@DividedBy", DbType.Decimal, Obj.DividedBy)
                DB.AddInParameter(DBComm, "@IsSolidGold", DbType.Boolean, Obj.IsSolidGold)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.String, Now.Date)
                DB.AddInParameter(DBComm, "@BarcodeStatus", DbType.Int32, Obj.BarcodeStatus)
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
        Public Function DeleteGoldQuality(ByVal GoldQualityID As String) As Boolean Implements IGoldQualityDA.DeleteGoldQuality
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_GoldQuality SET IsDelete=1 WHERE  GoldQualityID= @GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
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


        Public Function GetGoldQuality(ByVal GoldQualityID As String) As CommonInfo.GoldQualityInfo Implements IGoldQualityDA.GetGoldQuality
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGoldQuality As New GoldQualityInfo
            Try
                strCommandText = "  SELECT * FROM tbl_GoldQuality "
                strCommandText += "  WHERE GoldQualityID = @GoldQualityID And IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGoldQuality
                        .GoldQualityID = drResult("GoldQualityID")
                        .GoldQuality = drResult("GoldQuality")
                        .Prefix = drResult("Prefix")
                        .IsGramRate = drResult("IsGramRate")
                        .MultiplyBy = drResult("MultiplyBy")
                        .DividedBy = drResult("DividedBy")
                        .IsSolidGold = drResult("IsSolidGold")
                        .BarcodeStatus = drResult("BarcodeStatus")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGoldQuality
        End Function

        Public Function GetAllGoldQuality() As System.Data.DataTable Implements IGoldQualityDA.GetAllGoldQuality
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT GoldQualityID AS [@GoldQualityID],GoldQuality,Prefix, MultiplyBy, DividedBy, IsGramRate as [$IsGramRate], IsSolidGold as [$IsSolidGold],BarcodeStatus From tbl_GoldQuality where IsDelete = 0 Order by GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllGoldQualityByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IGoldQualityDA.GetAllGoldQualityByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT GoldQualityID AS [@GoldQualityID],GoldQuality,Prefix, MultiplyBy, DividedBy, IsGramRate as [$IsGramRate], IsSolidGold as [$IsSolidGold],BarcodeStatus From tbl_GoldQuality where IsDelete = 0 and LocationID=@LocationID Order by GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetAllGoldQualityFromSearchBox() As System.Data.DataTable Implements IGoldQualityDA.GetAllGoldQualityFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT GoldQualityID,GoldQuality,Prefix From tbl_GoldQuality where IsDelete=0 Order by GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetrptGoldQuality() As DataTable Implements IGoldQualityDA.GetrptGoldQuality
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT GoldQualityID, GoldQuality, Prefix, IsGramRate FROM tbl_GoldQuality where IsDelete= 0 ORDER BY IsGramRate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasGoldQuality(ByVal GoldQualityName As String, ByVal Prefix As String) As DataTable Implements IGoldQualityDA.HasGoldQuality
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_GoldQuality  "
                strCommandText += " where (GoldQuality= @GoldQuality and Prefix=@Prefix) or GoldQuality= @GoldQuality or Prefix=@Prefix And IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQuality", DbType.String, GoldQualityName)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, Prefix)


                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function HasGoldQualityForUpdate(ByVal GoldQualityName As String, ByVal GoldQualityID As String) As System.Data.DataTable Implements IGoldQualityDA.HasGoldQualityForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_GoldQuality where (GoldQuality= @GoldQuality ) "
                strCommandText += " and IsDelete=0 and GoldQualityID <> '" & GoldQualityID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQuality", DbType.String, GoldQualityName)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasPrefixForUpdate(ByVal prefix As String, ByVal GoldQualityID As String) As System.Data.DataTable Implements IGoldQualityDA.HasPrefixForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_GoldQuality where IsDelete= 0"
                strCommandText += " and (Prefix= @Prefix )   and GoldQualityID<>'" & GoldQualityID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, prefix)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasPrefixForUpdateUseItemCode(ByVal GoldQualityId As String) As System.Data.DataTable Implements IGoldQualityDA.HasPrefixForUpdateUseItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "  select * from  tbl_GoldQuality  C inner join tbl_ForSale  F on C.GoldQualityID=F.GoldQualityID "
                strCommandText += " where C.IsDelete=0 and C.GoldQualityID=" & GoldQualityId & ""
                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function HasGoldQualityAndPrefix(ByVal GoldQualityName As String, ByVal Prefix As String, ByVal GoldQualityID As String) As DataTable Implements IGoldQualityDA.HasGoldQualityAndPrefix
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_GoldQuality  "
                strCommandText += " where  GoldQuality= @GoldQuality AND  Prefix=@Prefix AND GoldQualityID=@GoldQualityID AND IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQuality", DbType.String, GoldQualityName)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, Prefix)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)


                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function CheckIsExitSolidGoldInGoldQuality(Optional GoldQualityID As String = "") As Boolean Implements IGoldQualityDA.CheckIsExitSolidGoldInGoldQuality
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If GoldQualityID = "" Then
                    strCommandText = "select * from tbl_GoldQuality where IsSolidGold=1 and IsDelete=0"
                Else

                    strCommandText = "select * from tbl_GoldQuality where IsSolidGold=1 AND GoldQualityID<>@GoldQualityID and IsDelete=0"
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                If dtResult.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
        Public Function GetAllGoldQualityForWhiteGold() As System.Data.DataTable Implements IGoldQualityDA.GetAllGoldQualityForWhiteGold
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT GoldQualityID AS [@GoldQualityID],GoldQuality From tbl_GoldQuality WHERE IsGramRate=1 and IsDelete = 0 Order by GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllGoldQualityDataForGoldPrice() As System.Data.DataTable Implements IGoldQualityDA.GetAllGoldQualityDataForGoldPrice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT '0' AS DefineID, GoldQualityID, GoldQuality, '' AS SalesRate, '' AS PurchaseRate, '' AS PercentPurchaseRate, '' AS ExchangeRate, '' AS PercentExchangeRate, '' AS PercentDamageRate, '' AS Remark, CASE DividedBy WHEN 0 THEN 1 ELSE DividedBy END AS DividedBy,  CASE MultiplyBy WHEN 0 THEN 1 ELSE MultiplyBy END AS MultiplyBy, IsSolidGold  From tbl_GoldQuality WHERE IsGramRate=0 and IsDelete = 0 Order by GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllGoldQualityByItemCategory(ByVal ItemCategory As String) As CommonInfo.GoldQualityInfo Implements IGoldQualityDA.GetAllGoldQualityByItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGoldQuality As New GoldQualityInfo
            Try
                strCommandText = "  SELECT * FROM tbl_GoldQuality "
                strCommandText += "  WHERE GoldQuality LIKE @ItemCategory and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategory", DbType.String, "%" + ItemCategory + "%")

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGoldQuality
                        .GoldQualityID = drResult("GoldQualityID")
                        .GoldQuality = drResult("GoldQuality")
                        .Prefix = drResult("Prefix")
                        .IsGramRate = drResult("IsGramRate")
                        .MultiplyBy = drResult("MultiplyBy")
                        .DividedBy = drResult("DividedBy")
                        .IsSolidGold = drResult("IsSolidGold")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGoldQuality
        End Function

    End Class
End Namespace

