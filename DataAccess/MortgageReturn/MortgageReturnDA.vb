Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace MortgageReturn
    Public Class MortgageReturnDA
        Implements IMortgageReturnDA

#Region "Private MortgageReturn"

        Private DB As Database
        Private Shared ReadOnly _instance As IMortgageReturnDA = New MortgageReturnDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgageReturnDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteMortgageReturn(ByVal MortgageReturnID As String) As Boolean Implements IMortgageReturnDA.DeleteMortgageReturn
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_MortgageReturn set isDelete=1 WHERE  MortgageReturnID=@MortgageReturnID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, MortgageReturnID)
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

        Public Function GetMortgageReturn(ByVal MortgageReturnID As String) As CommonInfo.MortgageReturnInfo Implements IMortgageReturnDA.GetMortgageReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.MortgageReturnInfo
            Try
                strCommandText = " SELECT  MortgageReturnID,MortgageInvoiceID as [@MortgageInvoiceID],FromDate,ToDate,PaybackAmount,PaidAmount,InterestAmt,PaybackDate,Remark,DiscountAmount FROM tbl_MortgageReturn WHERE MortgageReturnID=@MortgageReturnID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, MortgageReturnID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .MortgageReturnID = drResult("MortgageReturnID")
                        .MortgageInvoiceID = drResult("@MortgageInvoiceID")
                        .FromDate = drResult("FromDate")
                        .ToDate = drResult("ToDate")
                        .PaidAmount = drResult("PaidAmount")
                        .ReturnDate = drResult("ReturnDate")
                        .Remark = drResult("Remark")
                        .InterestAmount = drResult("InterestAmount")
                        .AddOrSub = drResult("AddOrSub")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertMortgageReturn(ByVal Obj As CommonInfo.MortgageReturnInfo) As Boolean Implements IMortgageReturnDA.InsertMortgageReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_MortgageReturn ( MortgageReturnID,MortgageInvoiceID,FromDate,ToDate,ReturnAmount,PaidAmount,InterestAmount,AddOrSub,LastLoginUserName,LastModifiedDate,LocationID,Remark,IsUpload,TotalAmount,IsDelete,ReturnDate)"
                strCommandText += " Values (@MortgageReturnID,@MortgageInvoiceID,@FromDate,@ToDate,@ReturnAmount,@PaidAmount,@InterestAmount, @AddOrSub, @LastLoginUserName,GetDate(),@LocationID,@Remark,0,@TotalAmount,@IsDelete,@ReturnDate)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, Obj.MortgageReturnID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, Obj.FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, Obj.ToDate)
                DB.AddInParameter(DBComm, "@ReturnAmount", DbType.Int64, Obj.ReturnAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.DateTime, Obj.ReturnDate)
                DB.AddInParameter(DBComm, "@LastLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@InterestAmount", DbType.Int64, Obj.InterestAmount)
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

        Public Function UpdateMortgageReturn(ByVal Obj As CommonInfo.MortgageReturnInfo) As Boolean Implements IMortgageReturnDA.UpdateMortgageReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_MortgageReturn set MortgageInvoiceID= @MortgageInvoiceID , FromDate= @FromDate , ToDate= @ToDate ,ReturnDate=@ReturnDate, ReturnAmount= @ReturnAmount , PaidAmount= @PaidAmount, AddOrSub=@AddOrSub,InterestAmount=@InterestAmount, LastLoginUserName=@LastLoginUserName,LastModifiedDate=@LastModifiedDate,Remark=@Remark,TotalAmount=@TotalAmount,IsDelete=@IsDelete "
                strCommandText += " where  MortgageReturnID= @MortgageReturnID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, Obj.MortgageReturnID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, Obj.FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, Obj.ToDate)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@ReturnAmount", DbType.Int64, Obj.ReturnAmount)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.Date, Obj.ReturnDate)
                DB.AddInParameter(DBComm, "@LastLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@InterestAmount", DbType.Int64, Obj.InterestAmount)
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

        Public Function GetMortgageReturnDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnDataTable
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select MortgageReturnID, MortgageInvoiceID, FromDate, ToDate, (PaybackAmount-DiscountAmount) AS PaybackAmount, PaidAmount,InterestAmt, LocationID, PaybackDate,Remark From tbl_MortgageReturn Where MortgageInvoiceID = '" & MortgageInvoiceID & "' Order by MortgageInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllMortgageReturnList() As System.Data.DataTable Implements IMortgageReturnDA.GetAllMortgageReturnList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, H.MortgageInvoiceID, CONVERT(VARCHAR(10),H.ReceiveDate,105) AS ReceiveDate, CONVERT(VARCHAR(10),I.FromDate,105) AS FromDate, CONVERT(VARCHAR(10),I.ToDate,105) AS ToDate, S.Staff, " & _
                " PaybackRate, I.PaybackAmount, I.PaidAmount,I.InterestAmt" & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageReturn I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
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

        Public Function GetAllMortgageReturnFromSearchBox() As System.Data.DataTable Implements IMortgageReturnDA.GetAllMortgageReturnFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, I.MortgageReturnID,H.MortgageInvoiceID, CONVERT(VARCHAR(10),H.ReceiveDate,105) AS ReceiveDate, CONVERT(VARCHAR(10),I.FromDate,105) AS FromDate, CONVERT(VARCHAR(10),I.ToDate,105) AS ToDate, CONVERT(VARCHAR(10),I.PaybackDate,105) AS PaybackDate,S.Name as [Name_], " & _
                " H.PaybackRate, I.PaybackAmount, I.PaidAmount,I.InterestAmt" & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageReturn I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
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

        'Public Function GetMortgageReturnPrint(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnPrint
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Dim dtResult As DataTable
        '    Try
        '        strCommandText = "SELECT H.MortgageInvoiceID, H.MortgageReturnID, PaybackDate,DATEADD(MONTH,M.InterestPeriod,PaybackDate) as DueDate, H.LocationID, Location,ToDate, " & _
        '                         "M.TotalAmount,H.InterestAmt,H.PaybackAmount, FromDate, H.PaidAmount,H.DiscountAmount,M.CustomerID,Cus.CustomerName,Cus.CustomerAddress as CustomerAddress,Cus.CustomerTel,H.Remark,H.InterestAmt,Cast(M.InterestRate as Int) as MortgageRate FROM tbl_MortgageReturn H " & _
        '                         "INNER JOIN tbl_MortgageInvoice M on H.MortgageInvoiceID=M.MortgageInvoiceID " & _
        '                         "INNER JOIN tbl_Customer Cus On M.CustomerID=Cus.CustomerID " & _
        '                         "Inner JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
        '                         " WHERE H.MortgageInvoiceID= @MortgageInvoiceID"

        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
        '        Return dtResult
        '    Catch ex As Exception
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
        '        Return New DataTable
        '    End Try
        'End Function


        Public Function GetMortgageReturnPrint(ByVal MortgageRetrunID As String) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT distinct H.MortgageInvoiceID, M.MortgageInvoiceCode,H.MortgageReturnID, H.LocationID, Location, S.Staff as " & _
                                 " MortgageStaff, Cus.CustomerName, Cus.CustomerAddress,H.FromDate,H.ToDate,RI.Amount,H.PaidAmount,H.ReturnDate,H.InterestAmount,H.ReturnAmount,H.AddOrSub,Cus.CustomerTel, " & _
                                 " I.MortgageRate,I.GoldTK,I.GoldTG ,I.MortgageItemCode,RI.ItemName,G.GoldQualityID,G.GoldQuality,C.ItemCategoryID,C.ItemCategory," & _
                                 " I.Amount as LoanAmount,  CAST(I.GoldTK AS INT) AS GoldK, CAST((I.GoldTK-CAST(I.GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST((((I.GoldTK-CAST(I.GoldTK AS INT))*16)-CAST((I.GoldTK-CAST(I.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY  FROM tbl_MortgageReturn H " & _
                                 " INNER JOIN tbl_MortgageReturnItem RI on H.MortgageReturnID=RI.MortgageReturnID " & _
                                 " INNER JOIN tbl_MortgageInvoice M on H.MortgageInvoiceID=M.MortgageInvoiceID " & _
                                 " INNER JOIN tbl_MortgageInvoiceItem I ON I.MortgageItemID=RI.MortgageItemID  " & _
                                 " LEFT JOIN tbl_Staff S ON S.StaffID=M.MortgageStaff " & _
                                 " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                                 " LEFT JOIN tbl_Customer Cus ON M.CustomerID=Cus.CustomerID  " & _
                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=RI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=RI.ItemCategoryID WHERE H.MortgageReturnID='" & MortgageRetrunID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageRetrunID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function UpdateMortgageInvoiceByPayback(ByVal Obj As CommonInfo.MortgageInvoiceInfo, ByVal MortgageReturnID As String) As Boolean Implements IMortgageReturnDA.UpdateMortgageInvoiceByPayback
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                'strCommandText = "UPDATE tbl_MortgageInvoice SET IsPayBack=@IsPayBack,PaybackAmt=@PaybackAmt,PaybackInterestAmt=@PaybackInterestAmt " & _
                '                 " WHERE MortgageInvoiceID=@MortgageInvoiceID "

                strCommandText = "UPDATE tbl_MortgageInvoice SET IsPayBack=@IsPayBack,PaybackAmt=M.PaybackAmt-P.PaidAmount,PaybackInterestAmt=M.PaybackInterestAmt-P.InterestAmt " & _
                                 " From tbl_MortgageInvoice M Left Join (select PaidAmount,InterestAmt,MortgageInvoiceID From tbl_MortgageReturn where MortgageReturnID=@MortgageReturnID) P on M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                 " WHERE M.MortgageInvoiceID=@MortgageInvoiceID "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, Obj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, MortgageReturnID)
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

        Public Function GetMortgageReturnFromInterest(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnFromInterest
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
        Public Function GetMortgageReturnDate(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Max(ToDate) as Date from tbl_MortgageReturn where MortgageInvoiceID=@MortgageInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllMortgageReturn() As System.Data.DataTable Implements IMortgageReturnDA.GetAllMortgageReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT MortgageReturnID,convert(varchar(10),R.ReturnDate,105) as ReturnDate,R.MortgageInvoiceID,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,R.PaidAmount,R.ReturnAmount,R.AddOrSub   " & _
                                  " From tbl_MortgageReturn R INNER JOIN tbl_MortgageInvoice M On M.MortgageInvoiceID=R.MortgageInvoiceID INNER join tbl_Staff S On M.MortgageStaff=S.StaffID INNER Join tbl_Customer C on M.CustomerID=C.CustomerID WHERE M.IsDelete=0 and C.IsDelete=0 AND S.IsDelete=0 And R.IsDelete=0  Order by R.ReturnDate desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgageReturnByID(ByVal MortgageReturnID As String) As CommonInfo.MortgageReturnInfo Implements IMortgageReturnDA.GetMortgageReturnByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New MortgageReturnInfo
            Try
                strCommandText = "SELECT M.MortgageInvoiceID,M.MortgageInvoiceCode,M.ReceiveDate,M.MortgageStaff,M.InterestRate,M.CustomerID, " & _
                               " P.TotalAmount as TotalAmount, " & _
                               " M.TotalQTY , M.Remark,M.IsReturn,M.LocationID,P.ReturnDate, " & _
                               " P.InterestAmount,M.NetAmount,P.AddOrSub,M.RRemark,M.InterestPeriod,IsNull(P.PaidAmount,0) as PaidAmount, " & _
                               " P.FromDate,P.ToDate,P.ReturnDate FROM tbl_MortgageInvoice M " & _
                               " Left JOIN tbl_MortgageReturn P ON M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                               " WHERE P.MortgageReturnID= @MortgageReturnID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, MortgageReturnID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ToDate = drResult("ToDate")
                        .FromDate = drResult("FromDate")
                        .PaidAmount = drResult("PaidAmount")
                        .InterestAmount = drResult("InterestAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .ReturnDate = drResult("ReturnDate")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetMortgageReturnItem(ByVal MortgageReturnID As String) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnItem
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
                '" LEFT JOIN tbl_MortgageReturn P ON H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                '" LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                '" LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                '" WHERE H.MortgageInvoiceID=@MortgageInvoiceID ORDER BY I.MortgageItemID"
                strCommandText = "SELECT I.MortgageReturnItemID,I.MortgageItemID, H.MortgageInvoiceID, I.GoldQualityID, GoldQuality, I.ItemCategoryID,ItemCategory as [ItemCategory_], ItemName as [ItemName_],I.ItemCategoryID, " & _
               " CAST(GoldTK AS INT) AS GoldK," & _
               " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
               " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'8.0'AS DECIMAL(18,2)) AS GoldY," & _
               " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
               " GoldTK, GoldTG,I.Amount as Amount, IsDone,DonePercent,ItemName,H.FromDate,H.ToDate" & _
               " FROM tbl_MortgageReturn H INNER JOIN tbl_MortgageReturnItem I ON H.MortgageReturnID=I.MortgageReturnID " & _
               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
               " WHERE H.MortgageReturnID=@MortgageReturnID  ORDER BY I.MortgageItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, MortgageReturnID)
                dt = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dt
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function InsertMortgageReturnItem(ByVal Obj As CommonInfo.MortgageReturnItemInfo) As Boolean Implements IMortgageReturnDA.InsertMortgageReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_MortgageReturnItem( MortgageReturnItemID,MortgageReturnID,MortgageItemID,GoldQualityID,ItemName,GoldTK,GoldTG,Amount,IsDone,DonePercent,ItemCategoryID)"
                strCommandText += " Values (@MortgageReturnItemID,@MortgageReturnID,@MortgageItemID,@GoldQualityID,@ItemName,@GoldTK,@GoldTG, @Amount, @IsDone,@DonePercent,@ItemCategoryID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnItemID", DbType.String, Obj.MortgageReturnItemID)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, Obj.MortgageReturnID)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, Obj.MortgageItemID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, Obj.ItemName)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, Obj.Amount)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, Obj.IsDone)
                DB.AddInParameter(DBComm, "@DonePercent", DbType.Int32, Obj.DonePercent)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)


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
        Public Function DeleteMortgageReturnItem(ByVal MortgageReturnItemID As String) As Boolean Implements IMortgageReturnDA.DeleteMortgageReturnItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_MortgageReturnItem WHERE  MortgageReturnItemID= @MortgageReturnItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnItemID", DbType.String, MortgageReturnItemID)

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
        Public Function GetMortgageReturnByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnByDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select *  from tbl_MortgageReturn where MortgageInvoiceID=@MortgageInvoiceID and ToDate>@TDate"
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
        Public Function GetMortgageReturnItemPrint(ByVal MortgageReturnID As String) As System.Data.DataTable Implements IMortgageReturnDA.GetMortgageReturnItemPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT MP.MortgageReturnId,H.MortgageInvoiceID, H.MortgageInvoiceCode, ReceiveDate,DATEADD(MONTH,InterestPeriod,ReceiveDate)as DueDate, H.LocationID, Location, " & _
                                 " S.Staff as MortgageStaff, InterestRate,InterestPeriod, Cus.CustomerName, Cus.CustomerAddress,H.TotalAmount, TotalQTY, H.Remark, IsReturn, " & _
                                  " ReturnDate,InterestAmount, NetAmount, AddOrSub, H.PaidAmount, RRemark,  MP.GoldQualityID, GoldQuality, MP.ItemCategoryID, ItemCategory, MP.ItemName, MP.Amount,  CAST(MP.GoldTK AS INT) AS GoldK, CAST((MP.GoldTK-CAST(MP.GoldTK AS INT))*16 AS INT) AS GoldP, CAST((((MP.GoldTK-CAST(MP.GoldTK AS INT))*16)" & _
                                  " -CAST((MP.GoldTK-CAST(MP.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, MP.GoldTK,(H.TotalAmount*(InterestRate/100)) as InterestPayment, " & _
                                  " ((H.TotalAmount/InterestRate)*InterestRate) as TotalInterestPayment,'' as Photo,MP.GoldTG,MP.GoldTK   FROM tbl_MortgageInvoice H " & _
                                  " Left Join tbl_MortgageReturn P on H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                  " Inner Join tbl_MortgageReturnItem MP on P.MortgageReturnID=MP.MortgageReturnID " & _
                                  " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff   " & _
                                  " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                                  " LEFT JOIN tbl_Customer Cus ON H.CustomerID=Cus.CustomerID " & _
                                  " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=MP.GoldQualityID " & _
                                  " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=MP.ItemCategoryID  WHERE MP.MortgageReturnID=@MortgageReturnID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, MortgageReturnID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function UpdateMortgageReturnItem(ByVal Obj As CommonInfo.MortgageReturnItemInfo) As Boolean Implements IMortgageReturnDA.UpdateMortgageReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update  tbl_MortgageReturnItem set MortgageReturnID=@MortgageReturnID,MortgageItemID=@MortgageItemID,GoldQualityID=@GoldQualityID,ItemName=@ItemName,GoldTK=@GoldTK,GoldTG=@GoldTG,Amount=@Amount,IsDone=@IsDone,DonePercent=@DonePercent,ItemCategoryID=@ItemCategoryID "
                strCommandText += " Where MortgageReturnItemID=@MortgageReturnItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageReturnItemID", DbType.String, Obj.MortgageReturnItemID)
                DB.AddInParameter(DBComm, "@MortgageReturnID", DbType.String, Obj.MortgageReturnID)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, Obj.MortgageItemID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, Obj.ItemName)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, Obj.Amount)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, Obj.IsDone)
                DB.AddInParameter(DBComm, "@DonePercent", DbType.Int32, Obj.DonePercent)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)


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

