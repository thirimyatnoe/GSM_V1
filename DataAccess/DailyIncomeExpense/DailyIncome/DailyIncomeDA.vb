Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports CommonInfo

Namespace DailyIncome
    Friend Class DailyIncomeDA
        Implements IDailyIncomeDA

#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As DailyIncomeDA = New DailyIncomeDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDailyIncomeDA
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteDailyIncomeHeader(ByVal DailyIncomeHeaderID As String) As Boolean Implements IDailyIncomeDA.DeleteDailyIncomeHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "Update tbl_DailyIncome SET IsDelete=1 "
                strCommandText += " WHERE DailyIncomeID = @DailyIncomeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyIncomeID", DbType.String, DailyIncomeHeaderID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return False
            End Try
        End Function

        Public Function DeleteDailyIncomeItem(ByVal ItemID As String) As Boolean Implements IDailyIncomeDA.DeleteDailyIncomeItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_DailyIncomeItem "
                strCommandText += " WHERE DailyIncomeItemID = @ItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ItemID", DbType.String, ItemID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return False
            End Try
        End Function

        Public Function GetDailyIncomeHeader(ByVal DailyIncomeHeaderID As String) As CommonInfo.DailyIncomeInfo Implements IDailyIncomeDA.GetDailyIncomeHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objDailyIncome As New DailyIncomeInfo

            Try
                strCommandText = "  SELECT DailyIncomeID, IncomeDate, Remark, TotalAmount,IsBank "
                strCommandText += " FROM tbl_DailyIncome "
                strCommandText += " WHERE DailyIncomeID = @HeaderID AND IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@HeaderID", DbType.String, DailyIncomeHeaderID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objDailyIncome
                        .DailyIncomeID = drResult("DailyIncomeID")
                        .IncomeDate = drResult("IncomeDate")
                        .Remark = drResult("Remark")
                        .TotalAmount = drResult("TotalAmount")
                        .IsBank = drResult("IsBank")
                    End With
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
            End Try

            Return objDailyIncome
        End Function

        Public Function GetDailyIncomeHeaderList() As System.Data.DataTable Implements IDailyIncomeDA.GetDailyIncomeHeaderList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "SELECT DailyIncomeID, CONVERT(VARCHAR(10),IncomeDate,105) as IncomeDate, Remark as [Remark_], TotalAmount,IsBank "
                strCommandText += " FROM tbl_DailyIncome WHERE IsDelete=0  ORDER BY DailyIncomeID ASC "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetDailyIncomeItems(ByVal DailyIncomeHeaderID As Object) As System.Data.DataTable Implements IDailyIncomeDA.GetDailyIncomeItems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = " SELECT  D_I.DailyIncomeItemID,D_I.DailyIncomeID,D.IncomeDate,D_I.Remark,D_I.Description,D_I.QTY,D_I.UnitPrice,D_I.Amount,D.IsBank "
                strCommandText += " FROM  tbl_DailyIncomeItem D_I left JOIN  tbl_DailyIncome D ON D.DailyIncomeID = D_I.DailyIncomeID "
                strCommandText += " WHERE D.DailyIncomeID=@HeaderID  AND D.IsDelete=0  ORDER BY D.DailyIncomeID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@HeaderID", DbType.String, DailyIncomeHeaderID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertDailyIncomeHeader(ByVal DailyIncomeHeaderObj As CommonInfo.DailyIncomeInfo) As Boolean Implements IDailyIncomeDA.InsertDailyIncomeHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " INSERT INTO tbl_DailyIncome "
                strCommandText += " (DailyIncomeID, IncomeDate, Remark, TotalAmount,LocationID,LastModifiedDate,LastModifiedLoginUserName,IsDelete,IsSync,IsBank) "
                strCommandText += " Values( @DailyIncomeID, @IncomeDate, @Remark, @TotalAmount,@LocationID,getdate(),@LastModifiedLoginUserName,0,0,@IsBank) "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyIncomeID", DbType.String, DailyIncomeHeaderObj.DailyIncomeID)
                DB.AddInParameter(DBComm, "@IncomeDate", DbType.Date, DailyIncomeHeaderObj.IncomeDate)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Decimal, DailyIncomeHeaderObj.TotalAmount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyIncomeHeaderObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsBank", DbType.Boolean, DailyIncomeHeaderObj.IsBank)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return False
            End Try
        End Function

        Public Function InsertDailyIncomeItem(ByVal DailyIncomeItemObj As CommonInfo.DailyIncomeItemInfo) As Boolean Implements IDailyIncomeDA.InsertDailyIncomeItem
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "INSERT INTO tbl_DailyIncomeItem "
                strCommandText += " (DailyIncomeItemID, DailyIncomeID,Description,QTY,UnitPrice,Amount,Remark,LastModifiedDate,LastModifiedLoginUserName) "
                strCommandText += " VALUES ( @DailyIncomeItemID, @DailyIncomeID,@Description, @QTY,@UnitPrice, @Amount,@Remark,getdate(),@LastModifiedLoginUserName) "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyIncomeItemID", DbType.String, DailyIncomeItemObj.DailyIncomeItemID)
                DB.AddInParameter(DBComm, "@DailyIncomeID", DbType.String, DailyIncomeItemObj.DailyIncomeID)
                DB.AddInParameter(DBComm, "@Description", DbType.String, DailyIncomeItemObj.Description)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, DailyIncomeItemObj.QTY)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int32, DailyIncomeItemObj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, DailyIncomeItemObj.Amount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyIncomeItemObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return False
            End Try
        End Function

        Public Function UpdateDailyIncomeHeader(ByVal DailyIncomeHeaderObj As CommonInfo.DailyIncomeInfo) As Boolean Implements IDailyIncomeDA.UpdateDailyIncomeHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " UPDATE tbl_DailyIncome "
                strCommandText += " SET IncomeDate=@IncomeDate, Remark=@Remark, TotalAmount=@TotalAmount,LocationID=@LocationID,LastModifiedDate=getdate(),LastModifiedLoginUserName=@LastModifiedLoginUserName,IsBank=@IsBank "
                strCommandText += " WHERE DailyIncomeID=@DailyIncomeID"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyIncomeID", DbType.String, DailyIncomeHeaderObj.DailyIncomeID)
                DB.AddInParameter(DBComm, "@IncomeDate", DbType.Date, DailyIncomeHeaderObj.IncomeDate)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Decimal, DailyIncomeHeaderObj.TotalAmount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyIncomeHeaderObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsBank", DbType.Boolean, DailyIncomeHeaderObj.IsBank)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return False
            End Try
        End Function

        Public Function UpdateDailyIncomeItem(ByVal DailyIncomeItemObj As CommonInfo.DailyIncomeItemInfo) As Boolean Implements IDailyIncomeDA.UpdateDailyIncomeItem
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = " UPDATE tbl_DailyIncomeItem "
                strCommandText += " SET DailyIncomeID=@DailyIncomeID,QTY=@QTY,UnitPrice=@UnitPrice, Amount=@Amount,Description=@Description,Remark=@Remark,LastModifiedDate=getdate(),LastModifiedLoginUserName=@LastModifiedLoginUserName "
                strCommandText += " WHERE DailyIncomeItemID=@DailyIncomeItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyIncomeItemID", DbType.String, DailyIncomeItemObj.DailyIncomeItemID)
                DB.AddInParameter(DBComm, "@DailyIncomeID", DbType.String, DailyIncomeItemObj.DailyIncomeID)
                DB.AddInParameter(DBComm, "@Description", DbType.String, DailyIncomeItemObj.Description)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, DailyIncomeItemObj.QTY)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int32, DailyIncomeItemObj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, DailyIncomeItemObj.Amount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyIncomeItemObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return False
            End Try
        End Function

        Public Function GetDailyIncomeExpenseReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal CriStr1 As String, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IDailyIncomeDA.GetDailyIncomeExpenseReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If criStr = "ByExpense" Then
                    strCommandText = " SELECT H.DailyExpenseID AS VoucherID, I.DailyExpenseItemID AS DetailID, H.ExpenseDate AS TDate, Description, QTY, UnitPrice, Amount, I.Remark, H.Remark AS HRemark ,H.IsBank  " & _
                     " FROM tbl_DailyExpense H INNER JOIN tbl_DailyExpenseItem I ON H.DailyExpenseID=I.DailyExpenseID " & _
                     " WHERE H.IsDelete=0 AND H.ExpenseDate BETWEEN @FromDate And @ToDate " & CriStr1
                Else
                    strCommandText = " SELECT  H.DailyIncomeID AS VoucherID, I.DailyIncomeItemID AS DetailID, H.IncomeDate AS TDate, Description, QTY, UnitPrice, Amount, I.Remark, H.Remark AS HRemark,H.IsBank " & _
                    " FROM tbl_DailyIncome H INNER JOIN tbl_DailyIncomeItem I ON H.DailyIncomeID=I.DailyIncomeID " & _
                    " WHERE H.IsDelete=0 AND H.IncomeDate BETWEEN @FromDate And @ToDate " & CriStr1
                End If


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace
