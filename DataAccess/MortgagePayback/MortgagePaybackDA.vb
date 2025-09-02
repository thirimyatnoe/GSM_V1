Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace MortgagePayback
    Public Class MortgagePaybackDA
        Implements IMortgagePaybackDA

#Region "Private MortgagePayback"

        Private DB As Database
        Private Shared ReadOnly _instance As IMortgagePaybackDA = New MortgagePaybackDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgagePaybackDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteMortgagePayback(ByVal MortgagePaybackID As String) As Boolean Implements IMortgagePaybackDA.DeleteMortgagePayback
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_MortgagePayback set isDelete=1 WHERE  MortgagePaybackID=@MortgagePaybackID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, MortgagePaybackID)
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

        Public Function GetMortgagePayback(ByVal MortgagePaybackID As String) As CommonInfo.MortgagePaybackInfo Implements IMortgagePaybackDA.GetMortgagePayback
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.MortgagePaybackInfo
            Try
                strCommandText = " SELECT  MortgagePaybackID,MortgageInvoiceID as [@MortgageInvoiceID],FromDate,ToDate,PaybackAmount,PaidAmount,InterestAmt,PaybackDate,Remark,DiscountAmount FROM tbl_MortgagePayback WHERE MortgagePaybackID=@MortgagePaybackID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, MortgagePaybackID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .MortgagePaybackID = drResult("MortgagePaybackID")
                        .MortgageInvoiceID = drResult("@MortgageInvoiceID")
                        .FromDate = drResult("FromDate")
                        .ToDate = drResult("ToDate")
                        .PaybackAmount = drResult("PaybackAmount")
                        .PaidAmount = drResult("PaidAmount")
                        .PaybackDate = drResult("PaybackDate")
                        .Remark = drResult("Remark")
                        .InterestAmt = drResult("InterestAmt")
                        .DiscountAmount = drResult("DiscountAmount")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertMortgagePayback(ByVal Obj As CommonInfo.MortgagePaybackInfo) As Boolean Implements IMortgagePaybackDA.InsertMortgagePayback
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_MortgagePayback ( MortgagePaybackID,MortgageInvoiceID,FromDate,ToDate,PaybackAmount,PaidAmount,PaybackDate,DiscountAmount,InterestAmt,LastModifiedLoginUserName,LastModifiedDate,LocationID,Remark,IsUpload,TotalAmount,IsDelete)"
                strCommandText += " Values (@MortgagePaybackID,@MortgageInvoiceID,@FromDate,@ToDate,@PaybackAmount,@PaidAmount,@PaybackDate, @DiscountAmount,@InterestAmt, @LastModifiedLoginUserName,GetDate(),@LocationID,@Remark,0,@TotalAmount,@IsDelete)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, Obj.MortgagePaybackID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, Obj.FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, Obj.ToDate)
                DB.AddInParameter(DBComm, "@PaybackAmount", DbType.Int64, Obj.PaybackAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PaybackDate", DbType.DateTime, Obj.PaybackDate)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@InterestAmt", DbType.Int64, Obj.InterestAmt)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@IsDelete", DbType.Boolean, Obj.IsDelete)

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

        Public Function UpdateMortgagePayback(ByVal Obj As CommonInfo.MortgagePaybackInfo) As Boolean Implements IMortgagePaybackDA.UpdateMortgagePayback
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_MortgagePayback set MortgageInvoiceID= @MortgageInvoiceID , FromDate= @FromDate , ToDate= @ToDate ,PaybackDate=@PaybackDate, PaybackAmount= @PaybackAmount , PaidAmount= @PaidAmount, DiscountAmount=@DiscountAmount,InterestAmt=@InterestAmt, LastModifiedLoginUserName=@LastModifiedLoginUserName,LastModifiedDate=@LastModifiedDate,Remark=@Remark,TotalAmount=@TotalAmount,IsDelete=@IsDelete "
                strCommandText += " where  MortgagePaybackID= @MortgagePaybackID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, Obj.MortgagePaybackID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, Obj.FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, Obj.ToDate)
                DB.AddInParameter(DBComm, "@PaybackAmount", DbType.Int64, Obj.PaybackAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PaybackDate", DbType.Date, Obj.PaybackDate)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@InterestAmt", DbType.Int64, Obj.InterestAmt)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@IsDelete", DbType.Boolean, Obj.IsDelete)

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

        Public Function GetMortgagePaybackDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackDA.GetMortgagePaybackDataTable
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select MortgagePaybackID, MortgageInvoiceID, FromDate, ToDate, (PaybackAmount-DiscountAmount) AS PaybackAmount, PaidAmount,InterestAmt, LocationID, PaybackDate,Remark From tbl_MortgagePayback Where MortgageInvoiceID = '" & MortgageInvoiceID & "' And isDelete=0 Order by MortgageInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllMortgagePaybackList() As System.Data.DataTable Implements IMortgagePaybackDA.GetAllMortgagePaybackList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, H.MortgageInvoiceID, CONVERT(VARCHAR(10),H.ReceiveDate,105) AS ReceiveDate, CONVERT(VARCHAR(10),I.FromDate,105) AS FromDate, CONVERT(VARCHAR(10),I.ToDate,105) AS ToDate, S.Staff, " & _
                " PaybackRate, I.PaybackAmount, I.PaidAmount,I.InterestAmt" & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgagePayback I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
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

        Public Function GetAllMortgagePaybackFromSearchBox() As System.Data.DataTable Implements IMortgagePaybackDA.GetAllMortgagePaybackFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, I.MortgagePaybackID,H.MortgageInvoiceID, CONVERT(VARCHAR(10),H.ReceiveDate,105) AS ReceiveDate, CONVERT(VARCHAR(10),I.FromDate,105) AS FromDate, CONVERT(VARCHAR(10),I.ToDate,105) AS ToDate, CONVERT(VARCHAR(10),I.PaybackDate,105) AS PaybackDate,S.Name as [Name_], " & _
                " H.PaybackRate, I.PaybackAmount, I.PaidAmount,I.InterestAmt" & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgagePayback I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
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

        Public Function GetMortgagePaybackPrint(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackDA.GetMortgagePaybackPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.MortgageInvoiceID, H.MortgagePaybackID, PaybackDate,DATEADD(MONTH,M.InterestPeriod,PaybackDate) as DueDate, H.LocationID, Location,ToDate, " & _
                                 "M.TotalAmount,H.TotalAmount As NetAmount,H.InterestAmt,H.PaybackAmount, FromDate, H.PaidAmount,H.DiscountAmount,M.CustomerID,Cus.CustomerName,Cus.CustomerAddress as CustomerAddress,Cus.CustomerTel,H.Remark,H.InterestAmt,Cast(M.InterestRate as Int) as MortgageRate FROM tbl_MortgagePayback H " & _
                                 "INNER JOIN tbl_MortgageInvoice M on H.MortgageInvoiceID=M.MortgageInvoiceID " & _
                                 "INNER JOIN tbl_Customer Cus On M.CustomerID=Cus.CustomerID " & _
                                 "Inner JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                 " WHERE H.MortgagePaybackID= @MortgageInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function UpdateMortgageInvoiceByPayback(ByVal Obj As CommonInfo.MortgageInvoiceInfo, ByVal MortgagePaybackID As String) As Boolean Implements IMortgagePaybackDA.UpdateMortgageInvoiceByPayback
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                'strCommandText = "UPDATE tbl_MortgageInvoice SET IsPayBack=@IsPayBack,PaybackAmt=@PaybackAmt,PaybackInterestAmt=@PaybackInterestAmt " & _
                '                 " WHERE MortgageInvoiceID=@MortgageInvoiceID "

                strCommandText = "UPDATE tbl_MortgageInvoice SET IsPayBack=@IsPayBack,PaybackAmt=M.PaybackAmt-P.PaidAmount,PaybackInterestAmt=M.PaybackInterestAmt-P.InterestAmt " & _
                                 " From tbl_MortgageInvoice M Left Join (select PaidAmount,InterestAmt,MortgageInvoiceID From tbl_MortgagePayback where MortgagePaybackID=@MortgagePaybackID) P on M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                 " WHERE M.MortgageInvoiceID=@MortgageInvoiceID "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, MortgagePaybackID)
                DB.AddInParameter(DBComm, "@IsPayBack", DbType.Boolean, Obj.IsPayback)
                'DB.AddInParameter(DBComm, "@PaybackAmt", DbType.Int32, Obj.PaybackAmt)
                'DB.AddInParameter(DBComm, "@PaybackInterestAmt", DbType.Int32, Obj.PaybackInterestAmt)

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

        Public Function GetMortgagePaybackFromInterest(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackDA.GetMortgagePaybackFromInterest
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dt As New DataTable
            Try
                strCommandText = "   SELECT  M.MortgageInvoiceID as [@MortgageInvoiceID],M.MortgageInvoiceCode, ReceiveDate,M.MortgageStaff,M.InterestRate,C.CustomerName,C.CustomerAddress,M.TotalAmount,(select Max(ToDate) from tbl_MortgageInterest where MortgageInvoiceID=@MortgageInvoiceID) as [InterestDate]  FROM tbl_MortgageInvoice M  INNER JOIN tbl_Customer C On M.CustomerID=C.CustomerID WHERE (M.MortgageInvoiceID= @MortgageInvoiceID)  and (M.ReceiveDate <=GetDate()) and M.ReceiveDate<=(select Max(ToDate) from tbl_MortgageInterest  "
                strCommandText += " where MortgageInvoiceID=@MortgageInvoiceID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dt = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dt
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgagePaybackDate(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackDA.GetMortgagePaybackDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Max(ToDate) as Date from tbl_MortgagePayback where MortgageInvoiceID=@MortgageInvoiceID And isDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllMortgagePayback() As System.Data.DataTable Implements IMortgagePaybackDA.GetAllMortgagePayback
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT MortgagePaybackID,convert(varchar(10),P.PaybackDate,105) as PaybackDate,P.MortgageInvoiceID,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,P.PaidAmount,PaybackAmount,DiscountAmount   " & _
                                  " From tbl_MortgagePayback P INNER JOIN tbl_MortgageInvoice M On M.MortgageInvoiceID=P.MortgageInvoiceID INNER join tbl_Staff S On M.MortgageStaff=S.StaffID INNER Join tbl_Customer C on M.CustomerID=C.CustomerID WHERE M.IsDelete=0 and C.IsDelete=0 AND S.IsDelete=0 And P.IsDelete=0  Order by P.PaybackDate desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgagePaybackByID(ByVal MortgagePaybackID As String) As CommonInfo.MortgagePaybackInfo Implements IMortgagePaybackDA.GetMortgagePaybackByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New MortgagePaybackInfo
            Try
                strCommandText = "SELECT M.MortgageInvoiceID,M.MortgageInvoiceCode,M.ReceiveDate,M.MortgageStaff,M.InterestRate,M.CustomerID, " & _
                               " P.TotalAmount as TotalAmount, " & _
                               " M.TotalQTY , M.Remark,M.IsReturn,M.LocationID,M.ReturnDate, " & _
                               " M.InterestAmount,M.NetAmount,M.AddOrSub,M.PaidAmount,M.RRemark,M.InterestPeriod,M.IsPayback,IsNull(P.PaidAmount,0) as PaybackAmt,IsNull(M.PaybackInterestAmt,0) as PaybackInterestAmt,M.NRC as NRC, " & _
                               " P.FromDate,P.ToDate,P.PaidAmount,P.InterestAmt FROM tbl_MortgageInvoice M " & _
                               " Left JOIN tbl_MortgagePayback P ON M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                               " WHERE P.MortgagePaybackID= @MortgagePaybackID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, MortgagePaybackID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ToDate = drResult("ToDate")
                        .FromDate = drResult("FromDate")
                        .PaidAmount = drResult("PaidAmount")
                        .InterestAmt = drResult("InterestAmt")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetMortgagePaybackItem(ByVal MortgagePaybackID As String) As System.Data.DataTable Implements IMortgagePaybackDA.GetMortgagePaybackItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dt As New DataTable
            Try
                'strCommandText = "SELECT I.MortgageItemID, H.MortgageInvoiceID, I.GoldQualityID, GoldQuality, I.MortgageRate, I.ItemCategoryID, ItemCategory, ItemName, QTY, " & _
                '" CAST(GoldTK AS INT) AS GoldK," & _
                '" CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                '" CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'8.0'AS DECIMAL(18,2)) AS GoldY," & _
                '" CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
                '" GoldTK, GoldTG, (H.TotalAmount-(IsNull(P.PaidAmount,0)-IsNull(P.InterestAmt,0))) as Amount, IsDone,DonePercent,ItemNameID,H.IsPayback" & _
                '" FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                '" LEFT JOIN tbl_MortgagePayback P ON H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                '" LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                '" LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                '" WHERE H.MortgageInvoiceID=@MortgageInvoiceID ORDER BY I.MortgageItemID"
                strCommandText = "SELECT I.MortgagePaybackItemID,I.MortgageItemID, H.MortgageInvoiceID, I.GoldQualityID, GoldQuality, I.MortgageRate, I.ItemCategoryID,ItemCategory as [ItemCategory_], ItemName as [ItemName_],I.ItemCategoryID, " & _
               " CAST(GoldTK AS INT) AS GoldK," & _
               " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
               " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'8.0'AS DECIMAL(18,2)) AS GoldY," & _
               " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
               " GoldTK, GoldTG,I.NetAmount as Amount,I.Amount as PaybackAmount, IsDone,DonePercent,ItemName,H.FromDate,H.ToDate" & _
               " FROM tbl_MortgagePayback H INNER JOIN tbl_MortgagePaybackItem I ON H.MortgagePaybackID=I.MortgagePaybackID " & _
               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
               " WHERE H.MortgagePaybackID=@MortgagePaybackID  ORDER BY I.MortgageItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, MortgagePaybackID)
                dt = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dt
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function InsertMortgagePaybackItem(ByVal Obj As CommonInfo.MortgagePaybackItemInfo) As Boolean Implements IMortgagePaybackDA.InsertMortgagePaybackItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_MortgagePaybackItem( MortgagePaybackItemID,MortgagePaybackID,MortgageItemID,GoldQualityID,ItemName,GoldTK,GoldTG,Amount,MortgageRate,IsDone,DonePercent,ItemCategoryID,NetAmount)"
                strCommandText += " Values (@MortgagePaybackItemID,@MortgagePaybackID,@MortgageItemID,@GoldQualityID,@ItemName,@GoldTK,@GoldTG, @Amount,@MortgageRate, @IsDone,@DonePercent,@ItemCategoryID,@NetAmount)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackItemID", DbType.String, Obj.MortgagePaybackItemID)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, Obj.MortgagePaybackID)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, Obj.MortgageItemID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, Obj.ItemName)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, Obj.Amount)
                DB.AddInParameter(DBComm, "@MortgageRate", DbType.Int32, Obj.MortgageRate)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, Obj.IsDone)
                DB.AddInParameter(DBComm, "@DonePercent", DbType.Int32, Obj.DonePercent)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int32, Obj.NetAmount)


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
        Public Function DeleteMortgagePaybackItem(ByVal MortgagePaybackItemID As String) As Boolean Implements IMortgagePaybackDA.DeleteMortgagePaybackItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_MortgagePaybackItem WHERE  MortgageItemID= @MortgagePaybackItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackItemID", DbType.String, MortgagePaybackItemID)

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
        Public Function GetMortgagePaybackByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As System.Data.DataTable Implements IMortgagePaybackDA.GetMortgagePaybackByDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select *  from tbl_MortgagePayback where MortgageInvoiceID=@MortgageInvoiceID and ToDate>@TDate"
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
        Public Function GetMortgagePaybackItemPrint(ByVal MortgagePaybackID As String) As System.Data.DataTable Implements IMortgagePaybackDA.GetMortgagePaybackItemPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT MP.MortgagepaybackId,H.MortgageInvoiceID, H.MortgageInvoiceCode, ReceiveDate,DATEADD(MONTH,InterestPeriod,ReceiveDate)as DueDate, H.LocationID, Location, " & _
                                 " S.Staff as MortgageStaff, InterestRate,InterestPeriod, Cus.CustomerName, Cus.CustomerAddress,H.TotalAmount, TotalQTY, H.Remark, IsReturn, " & _
                                  " ReturnDate,InterestAmount, MP.NetAmount, AddOrSub, H.PaidAmount, RRemark,  MP.GoldQualityID, GoldQuality, MP.ItemCategoryID, ItemCategory, MP.ItemName, MP.Amount ,  CAST(MP.GoldTK AS INT) AS GoldK, CAST((MP.GoldTK-CAST(MP.GoldTK AS INT))*16 AS INT) AS GoldP, CAST((((MP.GoldTK-CAST(MP.GoldTK AS INT))*16)" & _
                                  " -CAST((MP.GoldTK-CAST(MP.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, MP.GoldTK,(H.TotalAmount*(InterestRate/100)) as InterestPayment, " & _
                                  " ((H.TotalAmount/InterestRate)*InterestRate) as TotalInterestPayment,'' as Photo,MP.GoldTG,MP.GoldTK   FROM tbl_MortgageInvoice H " & _
                                  " Left Join tbl_mortgagepayback P on H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                  " Inner Join tbl_mortgagepaybackItem MP on P.MortgagePaybackID=MP.MortgagePaybackID " & _
                                  " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff   " & _
                                  " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                                  " LEFT JOIN tbl_Customer Cus ON H.CustomerID=Cus.CustomerID " & _
                                  " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=MP.GoldQualityID " & _
                                  " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=MP.ItemCategoryID  WHERE MP.MortgagepaybackID=@MortgagePaybackID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, MortgagePaybackID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function UpdateMortgagePaybackItem(ByVal Obj As CommonInfo.MortgagePaybackItemInfo) As Boolean Implements IMortgagePaybackDA.UpdateMortgagePaybackItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update  tbl_MortgagePaybackItem set MortgagePaybackID=@MortgagePaybackID,MortgageItemID=@MortgageItemID,GoldQualityID=@GoldQualityID,ItemName=@ItemName,GoldTK=@GoldTK,GoldTG=@GoldTG,Amount=@Amount,MortgageRate=@MortgageRate,IsDone=@IsDone,DonePercent=@DonePercent,NetAmount=@NetAmount "
                strCommandText += " Where MortgagePaybackItemID=@MortgagePaybackItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgagePaybackItemID", DbType.String, Obj.MortgagePaybackItemID)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, Obj.MortgagePaybackID)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, Obj.MortgageItemID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, Obj.ItemName)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, Obj.Amount)
                DB.AddInParameter(DBComm, "@MortgageRate", DbType.Int32, Obj.MortgageRate)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, Obj.IsDone)
                DB.AddInParameter(DBComm, "@DonePercent", DbType.Int32, Obj.DonePercent)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int32, Obj.NetAmount)


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
    End Class
End Namespace

