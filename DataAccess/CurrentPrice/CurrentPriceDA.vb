Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace CurrentPrice
    Public Class CurrentPriceDA
        Implements ICurrentPriceDA

#Region "Private CurrentPrice"

        Private DB As Database
        Private Shared ReadOnly _instance As ICurrentPriceDA = New CurrentPriceDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICurrentPriceDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteCurrentPrice(ByVal DefineID As String) As Boolean Implements ICurrentPriceDA.DeleteCurrentPrice
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_StandardRate Set IsDelete=1 WHERE  DefineID= @DefineID,LastModifiedDate=getdate()"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineID", DbType.String, DefineID)
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

        Public Function InsertCurrentPrice(ByVal CurrentPriceObj As CommonInfo.CurrentPriceInfo) As Boolean Implements ICurrentPriceDA.InsertCurrentPrice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_StandardRate ( DefineID,DefineDateTime,GoldQualityID,SalesRate,PurchaseRate,ExchangeRate,Remark,PercentPurchaseRate,PercentExchangeRate,PercentDamageRate,IsDelete,IsUpload,LocationID,LastModifiedDate)"
                strCommandText += " Values (@DefineID,@DefineDateTime,@GoldQualityID,@SalesRate,@PurchaseRate,@ExchangeRate,@Remark,@PercentPurchaseRate,@PercentExchangeRate,@PercentDamageRate,0,0,@LocationID,getdate())"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineID", DbType.String, CurrentPriceObj.DefineID)
                DB.AddInParameter(DBComm, "@DefineDateTime", DbType.DateTime, CurrentPriceObj.DefineDateTime)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, CurrentPriceObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, CurrentPriceObj.SalesRate)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int64, CurrentPriceObj.PurchaseRate)
                DB.AddInParameter(DBComm, "@ExchangeRate", DbType.Int64, CurrentPriceObj.ExchangeRate)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, CurrentPriceObj.Remark)
                DB.AddInParameter(DBComm, "@PercentPurchaseRate", DbType.Int32, CurrentPriceObj.PercentPurchaseRate)
                DB.AddInParameter(DBComm, "@PercentExchangeRate", DbType.Int32, CurrentPriceObj.PercentExchangeRate)
                DB.AddInParameter(DBComm, "@PercentDamageRate", DbType.Int32, CurrentPriceObj.PercentDamageRate)
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

        Public Function UpdateCurrentPrice(ByVal CurrentPriceObj As CommonInfo.CurrentPriceInfo) As Boolean Implements ICurrentPriceDA.UpdateCurrentPrice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_StandardRate set DefineDateTime= @DefineDateTime , GoldQualityID= @GoldQualityID , SalesRate= @SalesRate , PurchaseRate= @PurchaseRate , ExchangeRate= @ExchangeRate , Remark= @Remark, PercentPurchaseRate= @PercentPurchaseRate,PercentExchangeRate= @PercentExchangeRate,PercentDamageRate= @PercentDamageRate,LocationID=@LocationID "
                strCommandText += " where DefineID= @DefineID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineID", DbType.String, CurrentPriceObj.DefineID)
                DB.AddInParameter(DBComm, "@DefineDateTime", DbType.DateTime, CurrentPriceObj.DefineDateTime)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, CurrentPriceObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, CurrentPriceObj.SalesRate)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int64, CurrentPriceObj.PurchaseRate)
                DB.AddInParameter(DBComm, "@ExchangeRate", DbType.Int64, CurrentPriceObj.ExchangeRate)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, CurrentPriceObj.Remark)
                DB.AddInParameter(DBComm, "@PercentPurchaseRate", DbType.Int32, CurrentPriceObj.PercentPurchaseRate)
                DB.AddInParameter(DBComm, "@PercentExchangeRate", DbType.Int32, CurrentPriceObj.PercentExchangeRate)
                DB.AddInParameter(DBComm, "@PercentDamageRate", DbType.Int32, CurrentPriceObj.PercentDamageRate)
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

        Public Function GetAllCurrentPrice() As System.Data.DataTable Implements ICurrentPriceDA.GetAllCurrentPrice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT S.DefineID AS [@DefineID],convert(varchar(20),S.DefineDateTime,113) as [Current Date               ],S.GoldQualityID as [@GoldQualityID],G.GoldQuality,S.SalesRate,S.PurchaseRate,S.ExchangeRate,S.PercentPurchaseRate,S.PercentExchangeRate,S.PercentDamageRate,S.DefineDateTime as [@DefineDateTime] From tbl_StandardRate S Left join tbl_GoldQuality  G on S.GoldQualityID=G.GoldQualityID WHERE G.IsGramRate=1 and S.IsDelete=0 Order by [@DefineDateTime] DESC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetCurrentPrice(ByVal DefineID As String) As CommonInfo.CurrentPriceInfo Implements ICurrentPriceDA.GetCurrentPrice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objCurrentPrice As New CurrentPriceInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_StandardRate WHERE DefineID = @DefineID and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineID", DbType.String, DefineID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objCurrentPrice
                        .DefineID = drResult("DefineID")
                        .DefineDateTime = drResult("DefineDateTime")
                        .GoldQualityID = drResult("GoldQualityID")
                        .SalesRate = drResult("SalesRate")
                        .PurchaseRate = drResult("PurchaseRate")
                        .ExchangeRate = drResult("ExchangeRate")
                        .Remark = drResult("Remark")
                        .PercentPurchaseRate = drResult("PercentPurchaseRate")
                        .PercentExchangeRate = drResult("PercentExchangeRate")
                        .PercentDamageRate = drResult("PercentDamageRate")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objCurrentPrice
        End Function

        Public Function GetCurrentPriceByGoldID(ByVal GoldQualityID As String) As CommonInfo.CurrentPriceInfo Implements ICurrentPriceDA.GetCurrentPriceByGoldID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objCurrentPrice As New CurrentPriceInfo
            Try

                strCommandText = "SELECT * FROM tbl_StandardRate WHERE  "
                strCommandText += "DefineDateTime = (select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID = @GoldQualityID) AND GoldQualityID = @GoldQualityID and IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objCurrentPrice
                        '.DefineID = drResult("DefineID")
                        .DefineDateTime = drResult("DefineDateTime")
                        .GoldQualityID = drResult("GoldQualityID")
                        .SalesRate = drResult("SalesRate")
                        .PurchaseRate = drResult("PurchaseRate")
                        .ExchangeRate = drResult("ExchangeRate")
                        .Remark = drResult("Remark")
                        .PercentPurchaseRate = drResult("PercentPurchaseRate")
                        .PercentExchangeRate = drResult("PercentExchangeRate")
                        .PercentDamageRate = drResult("PercentDamageRate")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objCurrentPrice
        End Function

        Public Function GetAllCurrentPriceForPreview(Optional cristr As String = "") As System.Data.DataTable Implements ICurrentPriceDA.GetAllCurrentPriceForPreview
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT S.*,Convert(varchar,S.DefineDatetime,105) As DefineDate, Substring(Convert(varchar,S.DefineDatetime),12,8) as Time,Convert(varchar,S.DefineDatetime,105) as Date,G.GoldQuality, S.DefineDateTime as [@DefineDateTime] From tbl_StandardRate S Left join tbl_GoldQuality  G on S.GoldQualityID=G.GoldQualityID WHERE 1=1 and S.IsDelete=0" & cristr & " Order by [@DefineDateTime] DESC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllGoldPriceListByDateTime() As System.Data.DataTable Implements ICurrentPriceDA.GetAllGoldPriceListByDateTime
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                ' convert(varchar(20),S.DefineDateTime,113) as DefineDateTime
                strCommandText = "SELECT DefineID AS [@DefineID], Convert(varchar,S.DefineDatetime,105) + Substring(Convert(varchar,S.DefineDatetime),12,8) as [Define Date               ],  G.GoldQuality, S.SalesRate, S.PurchaseRate, S.ExchangeRate, S.Remark, S.PercentPurchaseRate, S.PercentExchangeRate, S.PercentDamageRate, S.DefineDateTime as [@DefineDateTime] From tbl_StandardRate S Left join tbl_GoldQuality  G on S.GoldQualityID=G.GoldQualityID Where IsGramRate = 0 AND IsSolidGold=1  AND S.IsDelete= 0 Order by [@DefineDateTime] DESC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetGoldPriceDataByDateTime(ByVal DefineDate As Date) As System.Data.DataTable Implements ICurrentPriceDA.GetGoldPriceDataByDateTime
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT DefineID, S.GoldQualityID, G.GoldQuality, CASE WHEN S.SalesRate <= 0 THEN '' ELSE CAST(S.SalesRate as VARCHAR(10)) END AS SalesRate,  CASE WHEN PurchaseRate <= 0 THEN '' ELSE CAST(PurchaseRate as VARCHAR(10)) END AS PurchaseRate, CASE WHEN PercentPurchaseRate <= 0 THEN '' ELSE CAST(PercentPurchaseRate as VARCHAR(10)) END AS PercentPurchaseRate , CASE WHEN ExchangeRate <= 0 THEN '' ELSE CAST(ExchangeRate as VARCHAR(10)) END AS ExchangeRate, CASE WHEN PercentExchangeRate <= 0 THEN '' ELSE CAST(PercentExchangeRate as VARCHAR(10)) END AS PercentExchangeRate, CASE WHEN PercentDamageRate <= 0 THEN '' ELSE CAST(PercentDamageRate as VARCHAR(10)) END AS PercentDamageRate, S.Remark, CASE G.DividedBy WHEN 0 THEN 1 ELSE DividedBy END AS DividedBy, G.MultiplyBy, G.IsSolidGold" & _
                                 " From tbl_StandardRate S LEFT JOIN tbl_GoldQuality G ON S.GoldQualityID=G.GoldQualityID WHERE IsGramRate=0 AND DefineDateTime=@DefineDate And S.IsDelete=0 Order by GoldQualityID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineDate", DbType.DateTime, DefineDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSolidGoldPrice() As System.Data.DataTable Implements ICurrentPriceDA.GetSolidGoldPrice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select distinct salesrate from tbl_StandardRate where definedatetime=(select max(definedatetime) from tbl_standardrate " & _
                                " where GoldQualityID in (select GoldQualityID from tbl_GoldQuality where IsSolidGold=1))"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

