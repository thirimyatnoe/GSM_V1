Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace InternationalDiamond
    Public Class IntDiamondPriceRateDA
        Implements IIntDiamondPriceRateDA

#Region "Private InternationalDiamond"

        Private DB As Database
        Private Shared ReadOnly _instance As IIntDiamondPriceRateDA = New IntDiamondPriceRateDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IIntDiamondPriceRateDA
            Get
                Return _instance
            End Get
        End Property

#End Region



        Public Function DeleteIntDiamond(ByVal DefineID As String) As Boolean Implements IIntDiamondPriceRateDA.DeleteIntDiamond
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_DiamondPriceRate SET IsDelete =CONVERT(bit,1),IsUpload= CONVERT(bit,0) WHERE  DefineID= @DefineID"

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

        Public Function GetIntDiamondByIntDiamondID(ByVal DefineID As String) As CommonInfo.IntDiamondPriceRateInfo Implements IIntDiamondPriceRateDA.GetIntDiamondByIntDiamondID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New IntDiamondPriceRateInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_DiamondPriceRate WHERE DefineID= @DefineID AND IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineID", DbType.String, DefineID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .DefineID = drResult("DefineID")
                        .DefineDateTime = drResult("DefineDateTime")
                        .CaratFrom = drResult("CaratFrom")
                        .CaratTo = drResult("CaratTo")
                        .PriceRate = drResult("PriceRate")
                        .PercentDirectChange = drResult("PercentDirectChange")
                        .PercentReturn = drResult("PercentReturn")
                        .WholeSaleRate = drResult("WholeSaleRate")
                        .PurchaseRate = drResult("PurchaseRate")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetIntDiamondData(ByVal Carat As Decimal) As CommonInfo.IntDiamondPriceRateInfo Implements IIntDiamondPriceRateDA.GetIntDiamondData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New IntDiamondPriceRateInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_DiamondPriceRate WHERE DefineDateTime=(SELECT Max(DefineDateTime) FROM tbl_DiamondPriceRate WHERE @Carat Between CaratFrom AND CaratTo ) AND IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@DefineDate", DbType.DateTime, DefineDate)
                'DB.AddInParameter(DBComm, "@Shape", DbType.String, Shape)
                DB.AddInParameter(DBComm, "@Carat", DbType.Decimal, Carat)
                'DB.AddInParameter(DBComm, "@Color", DbType.String, Color)
                'DB.AddInParameter(DBComm, "@Clarity", DbType.String, Clarity)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .DefineID = drResult("DefineID")
                        .DefineDateTime = drResult("DefineDateTime")
                        .CaratFrom = drResult("CaratFrom")
                        .CaratTo = drResult("CaratTo")
                        .PriceRate = drResult("PriceRate")
                        .PercentDirectChange = drResult("PercentDirectChange")
                        .PercentReturn = drResult("PercentReturn")
                        .WholeSaleRate = drResult("WholeSaleRate")
                        .PurchaseRate = drResult("PurchaseRate")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetIntDiamondList(ByVal argShape As String, ByVal argCaratFrom As Double, ByVal argCaratTo As Double) As System.Data.DataTable Implements IIntDiamondPriceRateDA.GetIntDiamondList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If argCaratTo < 0.3 Then
                    strCommandText = "SELECT Color, SUM(CASE Clarity WHEN 'IF-VVS' THEN PriceRate ELSE 0 END) AS 'IF-VVS', " & _
                        " SUM(CASE Clarity WHEN 'VS' THEN PriceRate ELSE 0 END) AS 'VS' FROM tbl_DiamondPriceRate " & _
                        " WHERE Shape=@Shape AND CaratFrom=@CaratFrom AND CaratTo=@CaratTo AND IsDelete=0 " & _
                        " GROUP BY DefineDateTime, Color"
                Else
                    strCommandText = "SELECT Color, SUM(CASE Clarity WHEN 'IF' THEN PriceRate ELSE 0 END) AS 'IF'," & _
                        " SUM(CASE Clarity WHEN 'VVS1' THEN PriceRate ELSE 0 END) AS 'VVS1', " & _
                        " SUM(CASE Clarity WHEN 'VVS2' THEN PriceRate ELSE 0 END) AS 'VVS2', " & _
                        " SUM(CASE Clarity WHEN 'VS1' THEN PriceRate ELSE 0 END) AS 'VS1'," & _
                        " SUM(CASE Clarity WHEN 'VS2' THEN PriceRate ELSE 0 END) AS 'VS2' FROM tbl_DiamondPriceRate " & _
                        " WHERE Shape=@Shape AND CaratFrom=@CaratFrom AND CaratTo=@CaratTo AND IsDelete=0 " & _
                        " GROUP BY DefineDateTime, Color"
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@Shape", DbType.String, argShape)
                DB.AddInParameter(DBComm, "@CaratFrom", DbType.Decimal, argCaratFrom)
                DB.AddInParameter(DBComm, "@CaratTo", DbType.Decimal, argCaratTo)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertIntDiamond(ByVal obj As CommonInfo.IntDiamondPriceRateInfo) As Boolean Implements IIntDiamondPriceRateDA.InsertIntDiamond
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_DiamondPriceRate ( DefineID,DefineDateTime,CaratFrom,CaratTo,PriceRate,PercentDirectChange,PercentReturn,WholeSaleRate,IsDelete,LastModifiedDate,LastModifiedUserName,PurchaseRate)"
                strCommandText += " Values (@DefineID,@DefineDateTime,@CaratFrom,@CaratTo,@PriceRate,@PercentDirectChange,@PercentReturn,@WholeSaleRate,0,getDate(),@LastModifiedUserName,@PurchaseRate)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineID", DbType.String, obj.DefineID)
                DB.AddInParameter(DBComm, "@DefineDateTime", DbType.Date, obj.DefineDateTime)
                DB.AddInParameter(DBComm, "@CaratFrom", DbType.Decimal, obj.CaratFrom)
                DB.AddInParameter(DBComm, "@CaratTo", DbType.Decimal, obj.CaratTo)
                DB.AddInParameter(DBComm, "@PriceRate", DbType.Decimal, obj.PriceRate)
                DB.AddInParameter(DBComm, "@PercentDirectChange", DbType.Int32, obj.PercentDirectChange)
                DB.AddInParameter(DBComm, "@PercentReturn", DbType.Int32, obj.PercentReturn)
                DB.AddInParameter(DBComm, "@WholeSaleRate", DbType.Decimal, obj.WholeSaleRate)
                DB.AddInParameter(DBComm, "@LastModifiedUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Decimal, obj.PurchaseRate)

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

        Public Function UpdateIntDiamond(ByVal obj As CommonInfo.IntDiamondPriceRateInfo) As Boolean Implements IIntDiamondPriceRateDA.UpdateIntDiamond
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_DiamondPriceRate set  DefineID= @DefineID , DefineDateTime= @DefineDateTime , CaratFrom= @CaratFrom , CaratTo= @CaratTo ,PriceRate= @PriceRate,PercentDirectChange= @PercentDirectChange,PercentReturn= @PercentReturn,WholeSaleRate=@WholeSaleRate,LastModifiedDate=getDate(),LastModifiedUserName=@LastModifiedUserName,PurchaseRate=@PurchaseRate "
                strCommandText += " where DefineID= @DefineID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DefineID", DbType.String, obj.DefineID)
                DB.AddInParameter(DBComm, "@DefineDateTime", DbType.Date, obj.DefineDateTime)
                DB.AddInParameter(DBComm, "@CaratFrom", DbType.Decimal, obj.CaratFrom)
                DB.AddInParameter(DBComm, "@CaratTo", DbType.Decimal, obj.CaratTo)
                DB.AddInParameter(DBComm, "@PriceRate", DbType.Decimal, obj.PriceRate)
                DB.AddInParameter(DBComm, "@PercentDirectChange", DbType.Int32, obj.PercentDirectChange)
                DB.AddInParameter(DBComm, "@PercentReturn", DbType.Int32, obj.PercentReturn)
                DB.AddInParameter(DBComm, "@WholeSaleRate", DbType.Decimal, obj.WholeSaleRate)
                DB.AddInParameter(DBComm, "@LastModifiedUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Decimal, obj.PurchaseRate)

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

        Public Function GetSaleReturnPercentByMaxDate(Optional ByVal cristr As String = "") As CommonInfo.IntDiamondPriceRateInfo Implements IIntDiamondPriceRateDA.GetSaleReturnPercentByMaxDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New IntDiamondPriceRateInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_DiamondPriceRate WHERE DefineDateTime=(SELECT Max(DefineDateTime) FROM tbl_DiamondPriceRate) AND IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        
                        .PercentDirectChange = drResult("PercentDirectChange")
                        .PercentReturn = drResult("PercentReturn")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetIntDiamondListForView() As System.Data.DataTable Implements IIntDiamondPriceRateDA.GetIntDiamondListForView
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT * FROM tbl_DiamondPriceRate WHERE IsDelete=0 order by DefineDateTime desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllDiamondPrice() As System.Data.DataTable Implements IIntDiamondPriceRateDA.GetAllDiamondPrice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT DefineID AS [@DefineID],Convert(varchar,DefineDatetime,105) + Substring(Convert(varchar,DefineDatetime),12,8) as [Current Date],CaratFrom,CaratTo,PriceRate,WholeSaleRate,PercentDirectChange,PercentReturn From tbl_DiamondPriceRate WHERE IsDelete=0 Order by [@DefineID] DESC"

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

