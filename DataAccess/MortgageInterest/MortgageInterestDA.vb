Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace MortgageInterest
    Public Class MortgageInterestDA
        Implements IMortgageInterestDA

#Region "Private MortgageInterest"

        Private DB As Database
        Private Shared ReadOnly _instance As IMortgageInterestDA = New MortgageInterestDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgageInterestDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteMortgageInterest(ByVal MortgageInvoiceID As String) As Boolean Implements IMortgageInterestDA.DeleteMortgageInterest
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_MortgageInterest set IsDelete=1 WHERE  MortgageInvoiceID=@MortgageInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
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

        Public Function GetMortgageInterest(ByVal MortgageInterestID As String) As CommonInfo.MortgageInterestInfo Implements IMortgageInterestDA.GetMortgageInterest
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.MortgageInterestInfo
            Try
                strCommandText = " SELECT  MortgageInterestID,MortgageInvoiceID as [@MortgageInvoiceID],FromDate,ToDate,InterestAmount,PaidAmount,InterestPaidDate,Remark FROM tbl_MortgageInterest WHERE MortgageInterestID=@MortgageInterestID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInterestID", DbType.String, MortgageInterestID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .MortgageInterestID = drResult("MortgageInterestID")
                        .MortgageInvoiceID = drResult("@MortgageInvoiceID")
                        .FromDate = drResult("FromDate")
                        .ToDate = drResult("ToDate")
                        .InterestAmount = drResult("InterestAmount")
                        .PaidAmount = drResult("PaidAmount")
                        .InterestPaidDate = drResult("InterestPaidDate")
                        .Remark = drResult("Remark")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertMortgageInterest(ByVal Obj As CommonInfo.MortgageInterestInfo) As Boolean Implements IMortgageInterestDA.InsertMortgageInterest
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_MortgageInterest ( MortgageInterestID,MortgageInvoiceID,FromDate,ToDate,InterestAmount,PaidAmount,InterestPaidDate,DiscountAmount,LastModifiedLoginUserName,LastModifiedDate,LocationID,Remark,IsUpload,IsDelete,InterestMonth)"
                strCommandText += " Values (@MortgageInterestID,@MortgageInvoiceID,@FromDate,@ToDate,@InterestAmount,@PaidAmount,@InterestPaidDate, @DiscountAmount, @LastModifiedLoginUserName,GetDate(),@LocationID,@Remark,0,0,@InterestMonth)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInterestID", DbType.String, Obj.MortgageInterestID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, Obj.FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, Obj.ToDate)
                DB.AddInParameter(DBComm, "@InterestAmount", DbType.Int64, Obj.InterestAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@InterestPaidDate", DbType.DateTime, Obj.InterestPaidDate)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@InterestMonth", DbType.Int32, Obj.InterestMonth)
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

        Public Function UpdateMortgageInterest(ByVal Obj As CommonInfo.MortgageInterestInfo) As Boolean Implements IMortgageInterestDA.UpdateMortgageInterest
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_MortgageInterest set MortgageInvoiceID= @MortgageInvoiceID , FromDate= @FromDate , ToDate= @ToDate ,InterestPaidDate=@InterestPaidDate, InterestAmount= @InterestAmount , PaidAmount= @PaidAmount, DiscountAmount=@DiscountAmount, LastModifiedLoginUserName=@LastModifiedLoginUserName,LastModifiedDate=@LastModifiedDate,Remark=@Remark,InterestMonth=@InterestMonth "
                strCommandText += " where  MortgageInterestID= @MortgageInterestID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInterestID", DbType.String, Obj.MortgageInterestID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, Obj.FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, Obj.ToDate)
                DB.AddInParameter(DBComm, "@InterestAmount", DbType.Int64, Obj.InterestAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@InterestPaidDate", DbType.Date, Obj.InterestPaidDate)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@InterestMonth", DbType.Int32, Obj.InterestMonth)

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

        Public Function GetMortgageInterestDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestDA.GetMortgageInterestDataTable
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select MortgageInterestID, MortgageInvoiceID, FromDate, ToDate, (InterestAmount-DiscountAmount) AS InterestAmount, PaidAmount, LocationID, InterestPaidDate,Remark From tbl_MortgageInterest Where IsDelete=0 and MortgageInvoiceID = '" & MortgageInvoiceID & "' Order by MortgageInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInterestHistoryDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestDA.GetMortgageInterestHistoryDataTable
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select MortgageInterestID, MortgageInvoiceID, FromDate, ToDate, (InterestAmount-DiscountAmount) AS InterestAmount, PaidAmount, LocationID, InterestPaidDate,'Interest' as  Type, Remark From tbl_MortgageInterest Where IsDelete=0 and MortgageInvoiceID = '" & MortgageInvoiceID & "'" & _
                                " UNION ALL " & _
                                " Select MortgagePaybackID, MortgageInvoiceID, FromDate, ToDate, PaidAmount AS InterestAmount, PaidAmount, LocationID, " & _
                                " PaybackDate,'Payback' as Type,Remark From tbl_MortgagePayback Where IsDelete=0 and MortgageInvoiceID = '" & MortgageInvoiceID & "' Order by MortgageInvoiceID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInterestDate(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestDA.GetMortgageInterestDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Max(ToDate) as Date from tbl_MortgageInterest where MortgageInvoiceID=@MortgageInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllMortgageInterestList() As System.Data.DataTable Implements IMortgageInterestDA.GetAllMortgageInterestList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, H.MortgageInvoiceID, CONVERT(VARCHAR(10),H.ReceiveDate,105) AS ReceiveDate, CONVERT(VARCHAR(10),I.FromDate,105) AS FromDate, CONVERT(VARCHAR(10),I.ToDate,105) AS ToDate, S.Staff, " & _
                " InterestRate, I.InterestAmount, I.PaidAmount" & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInterest I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " LEFT JOIN tbl_Location L ON H.LocationID=L.LocationID " & _
                " LEFT JOIN tbl_Staff S ON H.MortgageStaff=S.StaffID " & _
                " ORDER BY H.LocationID, H.MortgageInvoiceID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllMortgageInterestFromSearchBox() As System.Data.DataTable Implements IMortgageInterestDA.GetAllMortgageInterestFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, I.MortgageInterestID,H.MortgageInvoiceID, CONVERT(VARCHAR(10),H.ReceiveDate,105) AS ReceiveDate, CONVERT(VARCHAR(10),I.FromDate,105) AS FromDate, CONVERT(VARCHAR(10),I.ToDate,105) AS ToDate, CONVERT(VARCHAR(10),I.InterestPaidDate,105) AS InterestPaidDate,S.Name as [Name_], " & _
                " H.InterestRate, I.InterestAmount, I.PaidAmount" & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInterest I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " LEFT JOIN tbl_Location L ON H.LocationID=L.LocationID " & _
                " LEFT JOIN tbl_StaffByLocation S ON H.MortgageStaff=S.SaleStaffID " & _
                " ORDER BY H.LocationID, H.MortgageInvoiceID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInterestPrint(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestDA.GetMortgageInterestPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.MortgageInvoiceID, H.MortgageInterestID, InterestPaidDate,DATEADD(MONTH,M.InterestPeriod,InterestPaidDate) as DueDate, H.LocationID, Location,ToDate, " & _
                                 "H.InterestAmount, FromDate, H.PaidAmount,H.DiscountAmount,M.CustomerID,Cus.CustomerName,Cus.CustomerAddress as CustomerAddress,Cus.CustomerTel,H.Remark,Cast(M.InterestRate as Int) as MortgageRate FROM tbl_MortgageInterest H " & _
                                 "INNER JOIN tbl_MortgageInvoice M on H.MortgageInvoiceID=M.MortgageInvoiceID " & _
                                 "INNER JOIN tbl_Customer Cus On M.CustomerID=Cus.CustomerID " & _
                                 "Inner JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                 " WHERE H.IsDelete=0 and H.MortgageInvoiceID= @MortgageInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgageInterestByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As System.Data.DataTable Implements IMortgageInterestDA.GetMortgageInterestByDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select *  from tbl_MortgageInterest where MortgageInvoiceID=@MortgageInvoiceID and ToDate>@TDate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@TDate", DbType.String, TDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

