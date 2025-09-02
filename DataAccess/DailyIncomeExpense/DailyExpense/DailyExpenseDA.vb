Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports CommonInfo

Namespace DailyExpense
    Friend Class DailyExpenseDA
        Implements IDailyExpenseDA


#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As DailyExpenseDA = New DailyExpenseDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDailyExpenseDA
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteDailyExpenseHeader(ByVal DailyExpenseHeaderID As String) As Boolean Implements IDailyExpenseDA.DeleteDailyExpenseHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "UPDATE tbl_DailyExpense SET IsDelete=1"
                strCommandText += " WHERE DailyExpenseID = @DailyExpenseID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyExpenseID", DbType.String, DailyExpenseHeaderID)

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

        Public Function DeleteDailyExpenseItem(ByVal ItemID As String) As Boolean Implements IDailyExpenseDA.DeleteDailyExpenseItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_DailyExpenseItem "
                strCommandText += " WHERE DailyExpenseItemID = @ItemID"
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

        Public Function GetDailyExpenseHeader(ByVal DailyExpenseHeaderID As String) As CommonInfo.DailyExpenseInfo Implements IDailyExpenseDA.GetDailyExpenseHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objDailyExpense As New DailyExpenseInfo

            Try
                strCommandText = "  SELECT DailyExpenseID , ExpenseDate, Remark, TotalAmount, ReturnAmount,IsBank "
                strCommandText += " FROM tbl_DailyExpense "
                strCommandText += " WHERE DailyExpenseID = @HeaderID AND IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@HeaderID", DbType.String, DailyExpenseHeaderID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objDailyExpense
                        .DailyExpenseID = drResult("DailyExpenseID")
                        .ExpenseDate = drResult("ExpenseDate")
                        .Remark = drResult("Remark")
                        .TotalAmount = drResult("TotalAmount")
                        .ReturnAmount = drResult("ReturnAmount")
                        .IsBank = drResult("IsBank")

                    End With
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
            End Try
            Return objDailyExpense
        End Function

        Public Function GetDailyExpenseHeaderList() As System.Data.DataTable Implements IDailyExpenseDA.GetDailyExpenseHeaderList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "SELECT DailyExpenseID, CONVERT(VARCHAR(10),ExpenseDate,105) as ExpenseDate, Remark as [Remark_], TotalAmount, ReturnAmount,IsBank "
                strCommandText += " FROM tbl_DailyExpense WHERE IsDelete=0 ORDER BY DailyExpenseID Asc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetDailyExpenseItems(ByVal DailyExpenseHeaderID As Object) As System.Data.DataTable Implements IDailyExpenseDA.GetDailyExpenseItems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = " SELECT R_I.DailyExpenseItemID, R_I.DailyExpenseID, R_I.Description, R_I.QTY, R_I.UnitPrice, R_I.Amount,R_I.Remark,R_H.IsBank "
                strCommandText += " FROM tbl_DailyExpense R_H INNER JOIN  tbl_DailyExpenseItem R_I ON R_H.DailyExpenseID = R_I.DailyExpenseID "
                strCommandText += " WHERE R_I.DailyExpenseID=@HeaderID AND R_H.IsDelete=0 ORDER BY R_I.DailyExpenseItemID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@HeaderID", DbType.String, DailyExpenseHeaderID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Cash In-Out Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertDailyExpenseHeader(ByVal DailyExpenseHeaderObj As CommonInfo.DailyExpenseInfo) As Boolean Implements IDailyExpenseDA.InsertDailyExpenseHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " INSERT INTO tbl_DailyExpense "
                strCommandText += " (DailyExpenseID, ExpenseDate, Remark, TotalAmount, LocationID,ReturnAmount,LastModifiedLoginUserName, LastModifiedDate,IsDelete,IsSync,IsBank) "
                strCommandText += " Values( @DailyExpenseID, @ExpenseDate, @Remark, @TotalAmount, @LocationID,@ReturnAmount,@LastModifiedLoginUserName, getdate(),0,0,@IsBank) "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DailyExpenseID", DbType.String, DailyExpenseHeaderObj.DailyExpenseID)
                DB.AddInParameter(DBComm, "@ExpenseDate", DbType.Date, DailyExpenseHeaderObj.ExpenseDate)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyExpenseHeaderObj.Remark)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Decimal, DailyExpenseHeaderObj.TotalAmount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@ReturnAmount", DbType.Decimal, DailyExpenseHeaderObj.ReturnAmount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsBank", DbType.Boolean, DailyExpenseHeaderObj.IsBank)

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

        Public Function InsertDailyExpenseItem(ByVal DailyExpenseItemObj As CommonInfo.DailyExpenseItemInfo) As Boolean Implements IDailyExpenseDA.InsertDailyExpenseItem
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "INSERT INTO tbl_DailyExpenseItem "
                strCommandText += " (DailyExpenseItemID, DailyExpenseID, Description, QTY, UnitPrice, Amount,Remark,LastModifiedDate,LastModifiedLoginUserName) "
                strCommandText += " VALUES ( @DailyExpenseItemID, @DailyExpenseID, @Description, @QTY, @UnitPrice, @Amount,@Remark,getdate(),@LastModifiedLoginUserName) "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyExpenseItemID", DbType.String, DailyExpenseItemObj.DailyExpenseItemID)
                DB.AddInParameter(DBComm, "@DailyExpenseID", DbType.String, DailyExpenseItemObj.DailyExpenseID)
                DB.AddInParameter(DBComm, "@Description", DbType.String, DailyExpenseItemObj.Description)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, DailyExpenseItemObj.QTY)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int32, DailyExpenseItemObj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, DailyExpenseItemObj.Amount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyExpenseItemObj.Remark)
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

        Public Function UpdateDailyExpenseHeader(ByVal DailyExpenseHeaderObj As CommonInfo.DailyExpenseInfo) As Boolean Implements IDailyExpenseDA.UpdateDailyExpenseHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " UPDATE tbl_DailyExpense "
                strCommandText += " SET ExpenseDate=@ExpenseDate, Remark=@Remark, TotalAmount=@TotalAmount,LocationID=@LocationID,ReturnAmount=@ReturnAmount,LastModifiedDate=getdate(),LastModifiedLoginUserName=@LastModifiedLoginUserName,IsBank=@IsBank "
                strCommandText += " WHERE DailyExpenseID=@DailyExpenseID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DailyExpenseID", DbType.String, DailyExpenseHeaderObj.DailyExpenseID)
                DB.AddInParameter(DBComm, "@ExpenseDate", DbType.Date, DailyExpenseHeaderObj.ExpenseDate)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyExpenseHeaderObj.Remark)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Decimal, DailyExpenseHeaderObj.TotalAmount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@ReturnAmount", DbType.Decimal, DailyExpenseHeaderObj.ReturnAmount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsBank", DbType.Boolean, DailyExpenseHeaderObj.IsBank)

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

        Public Function UpdateDailyExpenseItem(ByVal DailyExpenseItemObj As CommonInfo.DailyExpenseItemInfo) As Boolean Implements IDailyExpenseDA.UpdateDailyExpenseItem
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = " UPDATE tbl_DailyExpenseItem "
                strCommandText += " SET DailyExpenseID=@DailyExpenseID, Description=@Description,QTY=@QTY, UnitPrice=@UnitPrice, Amount=@Amount,Remark=@Remark,LastModifiedDate=getdate(),LastModifiedLoginUserName=@LastModifiedLoginUserName "
                strCommandText += " WHERE DailyExpenseItemID=@DailyExpenseItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@DailyExpenseItemID", DbType.String, DailyExpenseItemObj.DailyExpenseItemID)
                DB.AddInParameter(DBComm, "@DailyExpenseID", DbType.String, DailyExpenseItemObj.DailyExpenseID)
                DB.AddInParameter(DBComm, "@Description", DbType.String, DailyExpenseItemObj.Description)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, DailyExpenseItemObj.QTY)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int32, DailyExpenseItemObj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, DailyExpenseItemObj.Amount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DailyExpenseItemObj.Remark)
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

    End Class
End Namespace
