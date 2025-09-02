Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace MortgageInvoice
    Public Class MortgageInvoiceDA
        Implements IMortgageInvoiceDA

#Region "Private MortgageInvoice"

        Private DB As Database
        Private Shared ReadOnly _instance As IMortgageInvoiceDA = New MortgageInvoiceDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgageInvoiceDA
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteMortgageInvoiceHeader(ByVal MortgageInvoiceHeaderID As String) As Boolean Implements IMortgageInvoiceDA.DeleteMortgageInvoiceHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_MortgageInvoice Set IsDelete=1  WHERE  MortgageInvoiceID= @MortgageInvoiceHeaderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceHeaderID", DbType.String, MortgageInvoiceHeaderID)
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

        Public Function GetMortgageInvoice(ByVal MortgageInvoiceHeaderID As String) As CommonInfo.MortgageInvoiceInfo Implements IMortgageInvoiceDA.GetMortgageInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New MortgageInvoiceInfo
            Try
                strCommandText = "SELECT M.MortgageInvoiceID,M.MortgageInvoiceCode,M.ReceiveDate,M.MortgageStaff,M.InterestRate,M.CustomerID, " & _
                               " (M.TotalAmount-(IsNull(P.PaidAmount,0)-isNull(P.InterestAmt,0))-isnull(R.ReturnAmount,0)-isnull(P.DiscountAmount,0)) as TotalAmount, " & _
                               "  M.TotalQTY , M.Remark,M.IsReturn,M.LocationID,M.ReturnDate, " & _
                               " M.InterestAmount,M.NetAmount,M.AddOrSub,M.PaidAmount,M.RRemark,M.InterestPeriod,M.IsPayback,IsNull(P.PaidAmount,0) as PaybackAmt,IsNull(M.PaybackInterestAmt,0) as PaybackInterestAmt FROM tbl_MortgageInvoice M " & _
                               " Left JOIN (select MortgageInvoiceID,sum(PaidAmount) as PaidAmount,sum(InterestAmt) as InterestAmt,sum(PaidAmount) as PaybackAmt,sum(DiscountAmount) as DiscountAmount from tbl_MortgagePayback where isDelete=0 group by MortgageInvoiceID ) P ON M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                               " Left JOIN (select MortgageInvoiceID,sum(TotalAmount) " & _
                               " as ReturnAmount,sum(InterestAmount) as InterestAmt,sum(PaidAmount) as PaybackAmt from tbl_MortgageReturn where isDelete=0 " & _
                               " group by MortgageInvoiceID ) R ON M.MortgageInvoiceID=R.MortgageInvoiceID  " & _
                               " WHERE M.MortgageInvoiceID= @MortgageInvoiceHeaderID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceHeaderID", DbType.String, MortgageInvoiceHeaderID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .MortgageInvoiceID = drResult("MortgageInvoiceID")
                        .MortgageInvoiceCode = drResult("MortgageInvoiceCode")
                        .ReceiveDate = drResult("ReceiveDate")
                        .MortgageStaff = IIf(IsDBNull(drResult("MortgageStaff")), "", drResult("MortgageStaff"))
                        .InterestRate = drResult("InterestRate")
                        .CustomerID = drResult("CustomerID")
                        .TotalAmount = drResult("TotalAmount")
                        .TotalQTY = drResult("TotalQTY")
                        .Remark = drResult("Remark")
                        .IsReturn = drResult("IsReturn")
                        .LocationID = drResult("LocationID")
                        If .IsReturn = True Then
                            .ReturnDate = drResult("ReturnDate")
                            .InterestAmount = drResult("InterestAmount")
                            .NetAmount = drResult("NetAmount")
                            .AddOrSub = drResult("AddOrSub")
                            .PaidAmount = drResult("PaidAmount")
                            .RRemark = drResult("RRemark")
                        End If
                        .PaybackAmt = drResult("PaybackAmt")
                        .PaybackInterestAmt = drResult("PaybackInterestAmt")
                        .InterestPeriod = drResult("InterestPeriod")
                        .IsPayback = drResult("IsPayback")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
        Public Function GetMortgageInvoiceForPaybackUpdate(ByVal MortgageInvoiceHeaderID As String, ByVal MortgagePaybackID As String) As CommonInfo.MortgageInvoiceInfo Implements IMortgageInvoiceDA.GetMortgageInvoiceForPaybackUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New MortgageInvoiceInfo
            Try
                strCommandText = "SELECT M.MortgageInvoiceID,M.MortgageInvoiceCode,M.ReceiveDate,M.MortgageStaff,M.InterestRate,M.CustomerID, " & _
                               " (M.TotalAmount-(IsNull(P.PaidAmount,0))-isnull(P.DiscountAmount,0)) as TotalAmount, " & _
                               "  M.TotalQTY , M.Remark,M.IsReturn,M.LocationID,M.ReturnDate, " & _
                               " M.InterestAmount,M.NetAmount,M.AddOrSub,M.PaidAmount,M.RRemark,M.InterestPeriod,M.IsPayback,IsNull(P.PaidAmount,0) as PaybackAmt,IsNull(M.PaybackInterestAmt,0) as PaybackInterestAmt FROM tbl_MortgageInvoice M " & _
                               " Left JOIN (select P.MortgageInvoiceID,sum(P.PaidAmount) as PaidAmount,sum(P.DiscountAmount) as DiscountAmount,sum(InterestAmt) as InterestAmt,sum(P.PaidAmount) as PaybackAmt from tbl_MortgagePayback P " & _
                               " Inner Join tbl_MortgageInvoice M on M.MortgageInvoiceID=P.MortgageInvoiceID where M.IsDelete=0 and P.isDelete=0 and P.MortgagePaybackID<>@MortgagePaybackID and P.MortgageInvoiceID=@MortgageInvoiceHeaderID group by P.MortgageInvoiceID ) P ON M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                               " WHERE M.MortgageInvoiceID= @MortgageInvoiceHeaderID and M.IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceHeaderID", DbType.String, MortgageInvoiceHeaderID)
                DB.AddInParameter(DBComm, "@MortgagePaybackID", DbType.String, MortgagePaybackID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .MortgageInvoiceID = drResult("MortgageInvoiceID")
                        .MortgageInvoiceCode = drResult("MortgageInvoiceCode")
                        .ReceiveDate = drResult("ReceiveDate")
                        .MortgageStaff = IIf(IsDBNull(drResult("MortgageStaff")), "", drResult("MortgageStaff"))
                        .InterestRate = drResult("InterestRate")
                        .CustomerID = drResult("CustomerID")
                        .TotalAmount = drResult("TotalAmount")
                        .TotalQTY = drResult("TotalQTY")
                        .Remark = drResult("Remark")
                        .IsReturn = drResult("IsReturn")
                        .LocationID = drResult("LocationID")
                        If .IsReturn = True Then
                            .ReturnDate = drResult("ReturnDate")
                            .InterestAmount = drResult("InterestAmount")
                            .NetAmount = drResult("NetAmount")
                            .AddOrSub = drResult("AddOrSub")
                            .PaidAmount = drResult("PaidAmount")
                            .RRemark = drResult("RRemark")
                        End If
                        .PaybackAmt = drResult("PaybackAmt")
                        .PaybackInterestAmt = drResult("PaybackInterestAmt")
                        .InterestPeriod = drResult("InterestPeriod")
                        .IsPayback = drResult("IsPayback")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertMortgageInvoiceHeader(ByVal MortgageInvoiceHeaderObj As CommonInfo.MortgageInvoiceInfo) As Boolean Implements IMortgageInvoiceDA.InsertMortgageInvoiceHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
               
                strCommandText = "Insert into tbl_MortgageInvoice ( MortgageInvoiceID,ReceiveDate,MortgageStaff,InterestRate,CustomerID,TotalAmount,TotalQTY,Remark,IsReturn,InterestPeriod,IsPayback,LastModifiedLoginUserName,LastModifiedDate,LocationID,MortgageInvoiceCode,IsUpload,IsDelete)"
                strCommandText += " Values (@MortgageInvoiceID,@ReceiveDate,@MortgageStaff,@InterestRate,@CustomerID,@TotalAmount,@TotalQTY,@Remark,@IsReturn,@InterestPeriod,@IsPayback,@LastModifiedLoginUserName,@LastModifiedDate,@LocationID,@MortgageInvoiceCode,0,0)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceHeaderObj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@ReceiveDate", DbType.Date, MortgageInvoiceHeaderObj.ReceiveDate)
                DB.AddInParameter(DBComm, "@MortgageStaff", DbType.String, MortgageInvoiceHeaderObj.MortgageStaff)
                DB.AddInParameter(DBComm, "@InterestRate", DbType.Decimal, MortgageInvoiceHeaderObj.InterestRate)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, MortgageInvoiceHeaderObj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, MortgageInvoiceHeaderObj.TotalAmount)
                DB.AddInParameter(DBComm, "@TotalQTY", DbType.Int32, MortgageInvoiceHeaderObj.TotalQTY)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, MortgageInvoiceHeaderObj.Remark)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, MortgageInvoiceHeaderObj.IsReturn)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceCode", DbType.String, MortgageInvoiceHeaderObj.MortgageInvoiceCode)
                DB.AddInParameter(DBComm, "@InterestPeriod", DbType.Int32, MortgageInvoiceHeaderObj.InterestPeriod)
                DB.AddInParameter(DBComm, "@IsPayback", DbType.Boolean, MortgageInvoiceHeaderObj.IsPayback)

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

        Public Function UpdateMortgageInvoiceHeader(ByVal MortgageInvoiceHeaderObj As CommonInfo.MortgageInvoiceInfo) As Boolean Implements IMortgageInvoiceDA.UpdateMortgageInvoiceHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                
                strCommandText = "Update tbl_MortgageInvoice set   ReceiveDate= @ReceiveDate , MortgageStaff= @MortgageStaff , InterestRate= @InterestRate , CustomerID= @CustomerID, TotalAmount= @TotalAmount , TotalQTY= @TotalQTY , Remark= @Remark , IsReturn= @IsReturn ,InterestPeriod=@InterestPeriod,IsPayback=@IsPayback, LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate, MortgageInvoiceCode=@MortgageInvoiceCode,IsUpload=0  "
                strCommandText += " where MortgageInvoiceID= @MortgageInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceHeaderObj.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@ReceiveDate", DbType.Date, MortgageInvoiceHeaderObj.ReceiveDate)
                DB.AddInParameter(DBComm, "@MortgageStaff", DbType.String, MortgageInvoiceHeaderObj.MortgageStaff)
                DB.AddInParameter(DBComm, "@InterestRate", DbType.Decimal, MortgageInvoiceHeaderObj.InterestRate)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, MortgageInvoiceHeaderObj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, MortgageInvoiceHeaderObj.TotalAmount)
                DB.AddInParameter(DBComm, "@TotalQTY", DbType.Int32, MortgageInvoiceHeaderObj.TotalQTY)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, MortgageInvoiceHeaderObj.Remark)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, MortgageInvoiceHeaderObj.IsReturn)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
                DB.AddInParameter(DBComm, "@MortgageInvoiceCode", DbType.String, MortgageInvoiceHeaderObj.MortgageInvoiceCode)
                DB.AddInParameter(DBComm, "@InterestPeriod", DbType.Int32, MortgageInvoiceHeaderObj.InterestPeriod)
                DB.AddInParameter(DBComm, "@IsPayback", DbType.Boolean, MortgageInvoiceHeaderObj.IsPayback)

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

        Public Function DeleteMortgageInvoiceItem(ByVal MortgageItemID As String) As Boolean Implements IMortgageInvoiceDA.DeleteMortgageInvoiceItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_MortgageInvoiceItem WHERE  MortgageItemID= @MortgageItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, MortgageItemID)
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

        Public Function GetMortgageInvoiceItem(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoiceItem
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
                strCommandText = "SELECT I.MortgageItemID, H.MortgageInvoiceID, I.GoldQualityID, GoldQuality, I.MortgageRate, I.ItemCategoryID, ItemCategory, ItemName as [ItemName_] , QTY, " & _
               " CAST(GoldTK AS INT) AS GoldK," & _
               " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
               " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'8.0'AS DECIMAL(18,2)) AS GoldY," & _
               " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
               " GoldTK, GoldTG,(I.Amount-isnull(P.PaidAmount,0)) as Amount, IsDone,DonePercent,ItemNameID,H.IsPayback,I.IsPayback as ItemPayback,I.MortgageItemCode,0 As PaybackAmount" & _
               " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
               " left join (" & _
               " select sum(I.Amount) as PaidAmount,sum(InterestAmt) as InterestAmt,sum(P.PaidAmount) as PaybackAmt,I.MortgageItemID " & _
               " from tbl_MortgagePayback P  " & _
               " Inner Join tbl_MortgageInvoice M on M.MortgageInvoiceID=P.MortgageInvoiceID " & _
               " Inner join tbl_mortgagepaybackitem I on P.Mortgagepaybackid=I.mortgagePaybackID where M.IsDelete=0 and P.isDelete=0 " & _
               " and P.MortgageInvoiceID=@MortgageInvoiceID group by I.MortgageItemID ) P on I.MortgageItemID=P.MortgageItemId " & _
               " WHERE H.MortgageInvoiceID=@MortgageInvoiceID  and I.MortgageItemID not in (select MortgageItemID from tbl_MortgageReturnItem I " & _
               " Inner Join tbl_MortgageReturn R on I.MortgageReturnID=R.MortgageReturnID where R.isDelete=0) ORDER BY I.MortgageItemID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dt = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dt
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllMortgageReturnByMortgageInvoice(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetAllMortgageReturnByMortgageInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dt As New DataTable
            Try
                strCommandText = "Select * From tbl_MortgageReturn Where MortgageInvoiceID=@MortgageInvoiceID And isDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dt = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dt
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInvoiceReceiveItem(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoiceReceiveItem
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
                strCommandText = "SELECT I.MortgageItemID, H.MortgageInvoiceID, I.GoldQualityID, GoldQuality, I.MortgageRate, I.ItemCategoryID, ItemCategory, ItemName as [ItemName] , QTY, " & _
               " CAST(GoldTK AS INT) AS GoldK," & _
               " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
               " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'8.0'AS DECIMAL(18,2)) AS GoldY," & _
               " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
               " GoldTK, GoldTG,I.Amount as Amount, IsDone,DonePercent,ItemNameID,H.IsPayback,I.IsPayback as ItemPayback,I.MortgageItemCode" & _
               " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
               " WHERE H.MortgageInvoiceID=@MortgageInvoiceID  ORDER BY I.MortgageItemID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dt = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dt
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertMortgageInvoiceItem(ByVal ObjMortgageInvoiceItem As CommonInfo.MortgageInvoiceItemInfo) As Boolean Implements IMortgageInvoiceDA.InsertMortgageInvoiceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_MortgageInvoiceItem ( MortgageItemID,MortgageInvoiceID,GoldQualityID,ItemCategoryID,ItemNameID,ItemName,QTY,GoldTK,GoldTG,Amount,MortgageRate,IsDone,DonePercent,isPayback,MortgageItemCode)"
                strCommandText += " Values (@MortgageItemID,@MortgageInvoiceID,@GoldQualityID,@ItemCategoryID,@ItemNameID,@ItemName,@QTY,@GoldTK,@GoldTG,@Amount,@MortgageRate,@IsDone,@DonePercent,@isPayback,@MortgageItemCode)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, ObjMortgageInvoiceItem.MortgageItemID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, ObjMortgageInvoiceItem.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, ObjMortgageInvoiceItem.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ObjMortgageInvoiceItem.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ObjMortgageInvoiceItem.ItemNameID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ObjMortgageInvoiceItem.ItemName)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjMortgageInvoiceItem.QTY)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, ObjMortgageInvoiceItem.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, ObjMortgageInvoiceItem.GoldTG)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, ObjMortgageInvoiceItem.Amount)
                DB.AddInParameter(DBComm, "@MortgageRate", DbType.Int64, ObjMortgageInvoiceItem.MortgageRate)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, ObjMortgageInvoiceItem.IsDone)
                'DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, ObjMortgageInvoiceItem.SaleRate)
                DB.AddInParameter(DBComm, "@DonePercent", DbType.String, ObjMortgageInvoiceItem.DonePercent)
                DB.AddInParameter(DBComm, "@isPayback", DbType.Boolean, ObjMortgageInvoiceItem.IsPayback)
                DB.AddInParameter(DBComm, "@MortgageItemCode", DbType.String, ObjMortgageInvoiceItem.MortgageItemCode)

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

        Public Function UpdateMortgageInvoiceItem(ByVal ObjMortgageInvoiceItem As CommonInfo.MortgageInvoiceItemInfo) As Boolean Implements IMortgageInvoiceDA.UpdateMortgageInvoiceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_MortgageInvoiceItem set   MortgageInvoiceID= @MortgageInvoiceID , GoldQualityID= @GoldQualityID ,ItemCategoryID=@ItemCategoryID, ItemName= @ItemName, ItemNameID= @ItemNameID  , QTY= @QTY , GoldTK= @GoldTK , GoldTG= @GoldTG , Amount= @Amount , MortgageRate=@MortgageRate, IsDone = @IsDone, DonePercent = @DonePercent,isPayback=@isPayback,MortgageItemCode=@MortgageItemCode "
                strCommandText += " where MortgageItemID= @MortgageItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, ObjMortgageInvoiceItem.MortgageItemID)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, ObjMortgageInvoiceItem.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, ObjMortgageInvoiceItem.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ObjMortgageInvoiceItem.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ObjMortgageInvoiceItem.ItemName)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ObjMortgageInvoiceItem.ItemNameID)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjMortgageInvoiceItem.QTY)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, ObjMortgageInvoiceItem.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, ObjMortgageInvoiceItem.GoldTG)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, ObjMortgageInvoiceItem.Amount)
                DB.AddInParameter(DBComm, "@MortgageRate", DbType.Int64, ObjMortgageInvoiceItem.MortgageRate)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, ObjMortgageInvoiceItem.IsDone)
                'DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, ObjMortgageInvoiceItem.SaleRate)
                DB.AddInParameter(DBComm, "@DonePercent", DbType.String, ObjMortgageInvoiceItem.DonePercent)
                DB.AddInParameter(DBComm, "@isPayback", DbType.Boolean, ObjMortgageInvoiceItem.IsPayback)
                DB.AddInParameter(DBComm, "@MortgageItemCode", DbType.String, ObjMortgageInvoiceItem.MortgageItemCode)

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

        Public Function GetAllMortgageInvoice() As System.Data.DataTable Implements IMortgageInvoiceDA.GetAllMortgageInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select IsReturn as [$IsReturn],IsDisable as [$IsDisable],M.MortgageInvoiceID as [@MortgageInvoiceID], MortgageInvoiceCode, " & _
                                 "convert(varchar(10),ReceiveDate,105) as [ReceiveDate],MortgageStaff as [@MortgageStaff],S.Staff as [Staff_], " & _
                                 "InterestRate,C.CustomerName AS [CustomerName_],C.CustomerAddress AS [Address_], " & _
                                 " M.TotalAmount as TotalAmount,TotalQTY,M.Remark AS [Remark_] " & _
                                 "From tbl_MortgageInvoice  M " & _
                                 "left join tbl_Staff S on S.StaffID=M.MortgageStaff " & _
                                 "Left join tbl_Customer C on C.CustomerID=M.CustomerID Where  IsReturn=0 and M.IsDelete=0  Order by ReceiveDate desc, " & _
                                 "SubString(M.MortgageInvoiceCode,5,5) asc "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateMortgageReturnHeader(ByVal MortgageReturnHeaderObj As CommonInfo.MortgageInvoiceInfo) As Boolean Implements IMortgageInvoiceDA.UpdateMortgageReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                'strCommandText = "Update tbl_MortgageInvoice set   ReceiveDate= @ReceiveDate , MortgageStaff= @MortgageStaff , InterestRate= @InterestRate , CustomerName= @CustomerName , Address= @Address , TotalAmount= @TotalAmount , TotalQTY= @TotalQTY , Remark= @Remark , IsReturn= @IsReturn , ReturnDate= @ReturnDate , InterestAmount= @InterestAmount , NetAmount= @NetAmount , AddOrSub= @AddOrSub , PaidAmount= @PaidAmount , RRemark= @RRemark "
                'strCommandText += " where MortgageInvoiceID= @MortgageInvoiceID"
                strCommandText = "Update tbl_MortgageInvoice set  IsReturn= @IsReturn, ReturnDate= @ReturnDate , InterestAmount= @InterestAmount , NetAmount= @NetAmount , AddOrSub= @AddOrSub , PaidAmount= @PaidAmount , RRemark= @RRemark,IsUpload=0   "
                strCommandText += " where MortgageInvoiceID= @MortgageInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageReturnHeaderObj.MortgageInvoiceID)
                'DB.AddInParameter(DBComm, "@ReceiveDate", DbType.Date, MortgageInvoiceHeaderObj.ReceiveDate)
                'DB.AddInParameter(DBComm, "@MortgageStaff", DbType.String, MortgageInvoiceHeaderObj.MortgageStaff)
                'DB.AddInParameter(DBComm, "@InterestRate", DbType.Decimal, MortgageInvoiceHeaderObj.InterestRate)
                'DB.AddInParameter(DBComm, "@CustomerName", DbType.String, MortgageInvoiceHeaderObj.CustomerName)
                'DB.AddInParameter(DBComm, "@Address", DbType.String, MortgageInvoiceHeaderObj.Address)
                'DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, MortgageInvoiceHeaderObj.TotalAmount)
                'DB.AddInParameter(DBComm, "@TotalQTY", DbType.Int32, MortgageInvoiceHeaderObj.TotalQTY)
                'DB.AddInParameter(DBComm, "@Remark", DbType.String, MortgageInvoiceHeaderObj.Remark)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, MortgageReturnHeaderObj.IsReturn)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.Date, MortgageReturnHeaderObj.ReturnDate)
                DB.AddInParameter(DBComm, "@InterestAmount", DbType.Int32, MortgageReturnHeaderObj.InterestAmount)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int32, MortgageReturnHeaderObj.NetAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, MortgageReturnHeaderObj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, MortgageReturnHeaderObj.PaidAmount)
                DB.AddInParameter(DBComm, "@RRemark", DbType.String, MortgageReturnHeaderObj.RRemark)

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

        Public Function UpdateMortgagePaybackHeader(ByVal MortgagePaybackHeaderObj As CommonInfo.MortgageInvoiceInfo) As Boolean Implements IMortgageInvoiceDA.UpdateMortgagePaybackHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "update tbl_MortgageInvoice set PaybackInterestAmt=P.InterestAmt ,PaybackAmt=P.PaidAmount,isPayback=1 " & _
                                " from tbl_MortgageInvoice M " & _
                                " join (select sum(PaidAmount) as PaidAmount,Sum(InterestAmt) as InterestAmt,MortgageInvoiceID " & _
                                " From tbl_MortgagePayback where isDelete=0 and MortgageInvoiceID=@MortgageInvoiceID group by MortgageInvoiceID ) as P on M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                " where M.MortgageInvoiceID= @MortgageInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgagePaybackHeaderObj.MortgageInvoiceID)
             
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
        Public Function UpdateMortgageInvoiceItemPayback(ByVal objMortgageItem As MortgageInvoiceItemInfo) As Boolean Implements IMortgageInvoiceDA.UpdateMortgageInvoiceItemPayback
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                'For Each dr As DataRow In dtMortgageInvoiceItem.Rows
                strCommandText = "Update tbl_MortgageInvoiceItem set  IsPayback=@IsPayback "
                strCommandText += " where MortgageItemID= @MortgageItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@IsPayback", DbType.String, objMortgageItem.IsPayback)
                DB.AddInParameter(DBComm, "@MortgageItemID", DbType.String, objMortgageItem.MortgageItemID)

                DB.ExecuteNonQuery(DBComm)

                ' Next
                Return True
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetMortgageReturnFromInterest(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageReturnFromInterest
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


        Public Function GetMortgageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, H.MortgageInvoiceID, H.MortgageInvoiceCode,ReceiveDate, S.Staff, Cus.CustomerName, Cus.CustomerAddress, InterestRate, TotalAmount, TotalQTY, H.Remark, " & _
                " I.GoldQualityID, GoldQuality, I.ItemCategoryID, ItemCategory, ItemName, Case QTY When 0 Then 1 else QTY end as QTY, GoldTK, Amount, MortgageRate, " & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY,CAST((H.TotalAmount*(H.InterestRate/100))AS INT) as InterestPayment ,CAST(((H.TotalAmount*(H.InterestRate/100))*H.InterestPeriod)AS INT) as TotalInterestPayment,Case isnull(R.MortgageitemID,'')" & _
                " When '' Then '-' Else N'ထုတ်ပြီး' End As IsPayback " & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " LEFT JOIN tbl_Customer Cus ON Cus.CustomerID=H.CustomerID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff" & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                " LEFT JOIN (Select MortgageItemID,R.MortgageInvoiceID from tbl_MortgageReturnItem RI" & _
                " Inner Join tbl_MortgageReturn R on RI.MortgageReturnID=R.MortgageReturnID Where R.isDelete=0) as R on I.MortgageItemID=R.MortgageItemID" & _
                " WHERE H.IsDelete=0 And ReceiveDate BETWEEN @FromDate And @ToDate " & cristr & " Order by H.MortgageInvoiceCode"
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

        Public Function GetAllMortgageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetAllMortgageReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, H.MortgageInvoiceID, H.MortgageInvoiceCode,ReceiveDate, S.Staff, Cus.CustomerName, Cus.CustomerAddress, InterestRate, TotalAmount, TotalQTY, H.Remark, " & _
                " I.GoldQualityID, GoldQuality, I.ItemCategoryID, ItemCategory, ItemName, QTY, GoldTK, Amount, MortgageRate, Cast(((MortgageRate*GoldTK)-((MortgageRate*GoldTK)*(I.DonePercent)/100))as INT) as EstimateAmount, " & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY,CAST((H.TotalAmount*(H.InterestRate/100))AS INT) as InterestPayment ,CAST(((H.TotalAmount*(H.InterestRate/100))*H.InterestPeriod)AS INT) as TotalInterestPayment,Case isnull(R.MortgageitemID,'') When '' Then '-' Else N'ထုတ်ပြီး' End As IsPayback  " & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " LEFT JOIN tbl_Customer Cus ON Cus.CustomerID=H.CustomerID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff" & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                " LEFT JOIN (Select MortgageItemID,R.MortgageInvoiceID from tbl_MortgageReturnItem RI" & _
                " Inner Join tbl_MortgageReturn R on RI.MortgageReturnID=R.MortgageReturnID Where R.isDelete=0) as R on I.MortgageItemID=R.MortgageItemID" & _
                " WHERE H.IsDelete=0 And ReceiveDate BETWEEN @FromDate And @ToDate " & cristr & " Order by H.MortgageInvoiceCode"
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

        Public Function GetMortgagePaybackReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgagePaybackReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT distinct L.Location, H.MortgageInvoiceID,M.MortgageItemID, H.MortgageInvoiceCode,ReceiveDate, S.Staff, Cus.CustomerName,I.Qty, " & _
                                  " Cus.CustomerAddress, InterestRate,M.MortgageRate,I.Amount as TotalAmount, TotalQTY, H.Remark,  M.GoldQualityID, GoldQuality, M.ItemCategoryID, ItemCategory, " & _
                                  " M.ItemName, M.GoldTK, M.Amount, CAST(M.GoldTK AS INT) AS GoldK, CAST((M.GoldTK-CAST(M.GoldTK AS INT))*16 AS INT)" & _
                                  " AS GoldP, CAST((((M.GoldTK-CAST(M.GoldTK AS INT))*16)-CAST((M.GoldTK-CAST(M.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
                                  " CAST((H.TotalAmount*(H.InterestRate/100))AS INT) as InterestPayment ,CAST(((H.TotalAmount*(H.InterestRate/100))*H.InterestPeriod)AS " & _
                                  " INT) as TotalInterestPayment,M.Amount As ItemPayback,P.DiscountAmount,P.MortgagePaybackID,M.MortgagePaybackItemID,P.PaidAmount as PaybackAmt,P.TotalAmount As TotalPayback,  P.DiscountAmount as PaybackBalanceAmt,CAST((P.DiscountAmount" & _
                                  " *(H.InterestRate/100))AS INT) as NewInterestPayment,CAST(((P.DiscountAmount*(H.InterestRate/100))*H.InterestPeriod)AS INT) as " & _
                                  " NewTotalInterestPayment     FROM tbl_MortgageInvoice H  " & _
                                  " Left Join tbl_MortgagePayback P On H.MortgageInvoiceID=P.MortgageInvoiceID  " & _
                                  " left Join tbl_MortgagePaybackItem M on P.MortgagePaybackID=M.MortgagePaybackID  " & _
                                  " left join tbl_Mortgageinvoiceitem I on H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                                  " LEFT JOIN tbl_Customer Cus ON Cus.CustomerID=H.CustomerID  " & _
                                  " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                  " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff " & _
                                  " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=M.GoldQualityID  " & _
                                  " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=M.ItemCategoryID  WHERE M.Amount>0 and " & _
                                  " H.IsDelete=0 and P.PaybackDate BETWEEN @FromDate and @ToDate " & cristr & " Order by H.MortgageInvoiceCode"
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

        Public Function GetMortgagePaybackTotalReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgagePaybackTotalReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT MortgagePaybackID,MortgageInvoiceID,PaybackAmount,PaidAmount,DiscountAmount,TotalAmount   FROM tbl_MortgagePayback H  " & _
                                  " Where H.IsDelete=0 and H.PaybackDate BETWEEN @FromDate and @ToDate " & cristr
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

        Public Function GetMortgageReturnReportNew(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageReturnReportNew
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT distinct L.Location, H.MortgageInvoiceID,RI.MortgageItemID, H.MortgageInvoiceCode,R.ReturnDate, S.Staff, C.CustomerName, C.CustomerAddress, " & _
                                  " InterestRate,RI.Amount as Amount,H.TotalAmount as TotalAmount, R.InterestAmount, NetAmount, R.AddOrSub, R.PaidAmount, TotalQTY, H.Remark, " & _
                                  " RRemark,  RI.ItemName, IC.ItemCategory,0 as Qty,RI.GoldTK, CAST(RI.GoldTK AS INT) AS GoldK, CAST((RI.GoldTK-CAST(RI.GoldTK AS INT))*16 AS INT) AS " & _
                                  " GoldP, CAST((((RI.GoldTK-CAST(RI.GoldTK AS INT))*16)-CAST((RI.GoldTK-CAST(RI.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY   " & _
                                  " FROM tbl_MortgageReturn R " & _
                                  " LEFT JOIN tbl_MortgageReturnItem RI on R.MortgageReturnID=RI.MortgageReturnID " & _
                                  " LEFT JOIN tbl_MortgageInvoice H ON R.MortgageInvoiceID=H.MortgageInvoiceID " & _
                                  " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                                  " LEFT JOIN tbl_ItemCategory IC ON RI.ItemCategoryID=IC.ItemCategoryID  " & _
                                  " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                  " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff  WHERE R.isDelete=0 and H.isDelete=0 And R.ReturnDate " & _
                                  " BETWEEN @FromDate and @ToDate " & cristr & " Order by H.MortgageInvoiceCode"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, True)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgageReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageReturnReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT L.Location, H.MortgageInvoiceID,I.MortgageItemID, H.MortgageInvoiceCode,ReturnDate, S.Staff, C.CustomerName, C.CustomerAddress, InterestRate,I.Amount as Amount,TotalAmount as TotalAmount, InterestAmount, NetAmount, AddOrSub, PaidAmount, TotalQTY, H.Remark, RRemark, " & _
                " I.ItemName, IC.ItemCategory,I.QTY,I.GoldTK," & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY  " & _
                " FROM tbl_MortgageInvoice H LEFT JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                " LEFT JOIN tbl_ItemCategory IC ON I.ItemCategoryID=IC.ItemCategoryID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff " & _
                " WHERE IsReturn=@IsReturn AND ReturnDate BETWEEN @FromDate And @ToDate " & cristr & " Order by H.MortgageInvoiceCode"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, True)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgageInterestReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataSet Implements IMortgageInvoiceDA.GetMortgageInterestReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataSet
            Try
                strCommandText = "SELECT L.Location, H.MortgageInvoiceID, H.MortgageInvoiceCode, H.ReceiveDate, S.Staff, C.CustomerName,C.CustomerAddress, H.InterestRate, H.TotalAmount, MI.InterestPaidDate, MI.FromDate, MI.ToDate, MI.InterestAmount, MI.PaidAmount, MI.DiscountAmount, " & _
                " IC.ItemCategory, I.ItemName,I.QTY,I.GoldTK," & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, " & _
                " IsNull(P.PaidAmount,0) as PaybackAmt, IsNull(P.DiscountAmount,0) as PaybackBalanceAmt " & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I On H.MortgageInvoiceID=I.MortgageInvoiceID INNER JOIN tbl_MortgageInterest MI ON H.MortgageInvoiceID=MI.MortgageInvoiceID " & _
                " Left Join tbl_MortgagePayback P On H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff " & _
                " LEFT JOIN tbl_ItemCategory IC ON I.ItemCategoryID=IC.ItemCategoryID " & _
                " WHERE MI.IsDelete=0 and MI.InterestPaidDate BETWEEN @FromDate and @ToDate " & cristr & " group by L.Location, H.MortgageInvoiceID, H.MortgageInvoiceCode, H.ReceiveDate, S.Staff, H.CustomerID,C.CustomerName,C.CustomerAddress, H.InterestRate, H.TotalAmount, MI.InterestPaidDate, MI.FromDate, MI.ToDate, MI.InterestAmount, MI.PaidAmount, MI.DiscountAmount, IC.ItemCategory,I.ItemName,I.QTY,I.GoldTK,P.PaidAmount,P.DiscountAmount order by H.MortgageInvoiceCode" & _
                " Select TotalAmount,IsNull(PaybackAmt,0) as PaybackAmt,H.MortgageInvoiceCode,MI.InterestAmount from tbl_MortgageInvoice H " & _
                " Left Join tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID  Left Join tbl_MortgageInterest MI " & _
                " On H.MortgageInvoiceID=MI.MortgageInvoiceID " & _
                " WHERE MI.IsDelete=0 AND MI.InterestPaidDate BETWEEN @FromDate and @ToDate " & cristr & _
                " group by TotalAmount,PaybackAmt,MortgageInvoiceCode,MI.InterestAmount "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                dtResult = DB.ExecuteDataSet(DBComm)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataSet
            End Try
        End Function

        Public Function GetMortgageCustomerHistoryReport(Optional ByVal cristr As String = "") As System.Data.DataSet Implements IMortgageInvoiceDA.GetMortgageCustomerHistoryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataSet
            Try
                strCommandText = " SELECT  H.MortgageInvoiceID, H.MortgageInvoiceCode, H.ReceiveDate, S.Staff, C.CustomerName, " & _
                                 "H.InterestRate,G.GoldQuality,I.MortgageRate,IC.ItemCategory, I.ItemName,I.QTY,I.GoldTK, CAST(GoldTK AS INT) AS GoldK, " & _
                                 "CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                 "CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, " & _
                                 "H.ReturnDate, Sum(H.TotalAmount) as TotalAmount, " & _
                                 "(IsNull(Sum(H.InterestAmount),0)+IsNull(Sum(MI.PaidAmount),0)) as InterestAmount, " & _
                                 "IsNull(Sum(H.NetAmount),0) as NetAmount,IsNull(Sum(H.AddOrSub),0) as AddorSub, " & _
                                 "(IsNull(Sum(H.PaidAmount),0)+ IsNull(Sum(P.PaidAmount),0)) as PaidAmount,  " & _
                                 "IsNull(Sum(P.DiscountAmount),0) as PaybackBalanceAmt " & _
                                 "FROM tbl_MortgageInvoice H " & _
                                 "INNER JOIN tbl_MortgageInterest MI ON H.MortgageInvoiceID=MI.MortgageInvoiceID " & _
                                 "INNER JOIN tbl_MortgageInvoiceItem I On H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                                 "INNER JOIN tbl_GoldQuality G On I.GoldQualityID=G.GoldQualityID " & _
                                 "Inner Join tbl_MortgagePayback P On H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                 "LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                                 "LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff  LEFT JOIN tbl_ItemCategory IC ON I.ItemCategoryID=IC.ItemCategoryID  " & _
                                 "WHERE H.IsDelete=0 and MI.IsDelete=0 " & cristr & _
                                 " Group By  H.MortgageInvoiceID, H.MortgageInvoiceCode, H.ReceiveDate, S.Staff, C.CustomerName, " & _
                                 "H.InterestRate,IC.ItemCategory, I.ItemName,I.QTY,I.GoldTK,  H.ReturnDate,G.GoldQuality,I.MortgageRate " & _
                                 "SELECT L.Location,S.Staff,C.CustomerName,H.InterestRate, H.MortgageInvoiceID, H.MortgageInvoiceCode, H.ReceiveDate,H.TotalAmount, MI.InterestPaidDate, MI.FromDate, MI.ToDate, " & _
                                 "MI.InterestAmount, IsNull(H.InterestAmount,0) as ReturnInterestAmount,MI.PaidAmount, MI.DiscountAmount FROM tbl_MortgageInvoice H " & _
                                 "INNER JOIN tbl_MortgageInvoiceItem I On H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                                 "INNER JOIN tbl_MortgageInterest MI ON H.MortgageInvoiceID=MI.MortgageInvoiceID " & _
                                 "LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                 "LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff  LEFT JOIN tbl_ItemCategory IC ON I.ItemCategoryID=IC.ItemCategoryID  " & _
                                 "WHERE MI.IsDelete=0 " & cristr & _
                                 " group by L.Location,S.Staff,C.CustomerName,H.InterestRate, H.MortgageInvoiceID, " & _
                                 "H.MortgageInvoiceCode, H.ReceiveDate,H.TotalAmount,MI.InterestPaidDate, MI.FromDate, MI.ToDate, MI.InterestAmount, MI.PaidAmount, MI.DiscountAmount,H.InterestAmount  " & _
                                 "SELECT L.Location,S.Staff,C.CustomerName, H.MortgageInvoiceID, H.MortgageInvoiceCode, P.PaybackDate, P.FromDate, P.ToDate,IsNull(Sum(H.TotalAmount),0) as TotalAmount, " & _
                                 "IsNull((IsNull(Sum(H.NetAmount),0)+(IsNull(Sum(P.PaybackAmount),0)-IsNull(Sum(P.DiscountAmount),0))),0) as NetAmount,IsNull(Sum(P.PaidAmount),0) as PaybackAmount , IsNull(Sum(H.PaidAmount),0) as PaidAmount,IsNull((IsNull(Sum(P.PaidAmount),0)+IsNull(Sum(H.PaidAmount),0)),0) as TotalPaidAmount, " & _
                                 "IsNull(H.AddOrSub,0) as AddOrSub FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I On H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                                 "INNER JOIN tbl_MortgagePayback P ON H.MortgageInvoiceID=P.MortgageInvoiceID  " & _
                                 "LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                 "LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff  LEFT JOIN tbl_ItemCategory IC ON I.ItemCategoryID=IC.ItemCategoryID  " & _
                                 "Where H.IsDelete=0 " & cristr & _
                                 " group by L.Location,S.Staff,C.CustomerName, H.MortgageInvoiceID,H.MortgageInvoiceCode,P.PaybackDate, P.FromDate,H.TotalAmount, P.ToDate,P.PaybackAmount,H.NetAmount, P.InterestAmt, P.PaidAmount,H.AddOrSub,P.DiscountAmount "



                DBComm = DB.GetSqlStringCommand(strCommandText)
              

                dtResult = DB.ExecuteDataSet(DBComm)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataSet
            End Try
        End Function

        Public Function GetMortgageDisable(ByVal InterestPeriod As Integer) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageDisable
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = "  SELECT MortgageInvoiceID, MortgageInvoiceCode, CONVERT(VARCHAR(10),ReceiveDate,105) AS ReceiveDate, " & _
                ' "CustomerName, TotalAmount , TotalQTY, IsDisable FROM tbl_MortgageInvoice M INNER JOIN tbl_Customer C " & _
                ' "On M.CustomerID=C.CustomerID " & _
                '"WHERE   MortgageInvoiceID IN (SELECT MortgageInvoiceID FROM (SELECT CASE WHEN ToDate IS NULL THEN ReceiveDate " & _
                ' "ELSE ToDate END AS MDate, M.MortgageInvoiceID " & _
                ' "FROM (SELECT MAX(I.ToDate) AS ToDate, R.ReceiveDate, R.MortgageInvoiceID FROM tbl_MortgageInterest I " & _
                ' "RIGHT JOIN tbl_MortgageInvoice R ON I.MortgageInvoiceID=R.MortgageInvoiceID " & _
                '"GROUP BY  R.MortgageInvoiceID, R.ReceiveDate) AS M) AS MAIN " & _
                '  "WHERE DATEADD(m,@InterestPeriod,MDate) < GetDate()) And M.InterestPeriod=" & InterestPeriod & " order by ReceiveDate "

                strCommandText = "  SELECT MortgageInvoiceID, MortgageInvoiceCode, CONVERT(VARCHAR(10),ReceiveDate,105) AS ReceiveDate, " & _
                                " CustomerName, TotalAmount , TotalQTY, IsDisable FROM tbl_MortgageInvoice M INNER JOIN tbl_Customer C " & _
                                " On M.CustomerID=C.CustomerID " & _
                                " WHERE   MortgageInvoiceID IN (SELECT MortgageInvoiceID FROM (SELECT CASE WHEN ToDate IS NULL THEN ReceiveDate " & _
                                " ELSE ToDate END AS MDate, M.MortgageInvoiceID " & _
                                " FROM (SELECT distinct case when  isnull(MAX(I.ToDate),r.receivedate)>isnull(max(B.todate),r.receivedate)  then I.todate else b.todate end AS ToDate, R.ReceiveDate, R.MortgageInvoiceID FROM tbl_MortgageInterest I " & _
                                " RIGHT JOIN tbl_MortgageInvoice R ON I.MortgageInvoiceID=R.MortgageInvoiceID " & _
                                " left join tbl_MortgagePayback B on r.MortgageInvoiceid=b.mortgageinvoiceid" & _
                                " GROUP BY  R.MortgageInvoiceID, R.ReceiveDate,i.todate,B.ToDate) AS M) AS MAIN " & _
                                " WHERE DATEADD(m,@InterestPeriod,MDate) < GetDate()) And M.InterestPeriod=" & InterestPeriod & " order by ReceiveDate "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "InterestPeriod", DbType.Int32, InterestPeriod)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateMortgageDisable(ByVal ObjMortgageInvoice As CommonInfo.MortgageInvoiceInfo) As Boolean Implements IMortgageInvoiceDA.UpdateMortgageDisable
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_MortgageInvoice set   IsDisable=@IsDisable , DisableDate=@DisableDate "
                strCommandText += " where MortgageInvoiceID= @MortgageInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, ObjMortgageInvoice.MortgageInvoiceID)
                DB.AddInParameter(DBComm, "@IsDisable", DbType.Boolean, ObjMortgageInvoice.IsDisable)
                DB.AddInParameter(DBComm, "@DisableDate", DbType.Date, ObjMortgageInvoice.DisableDate)

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

        Public Function GetMortgageDisableSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageDisableSummaryByGoldQualityAndItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  IsNull(SUM(I.QTY),0) AS QTY, IsNull(SUM(I.GoldTK),0)AS GoldTK  " & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " WHERE I.GoldQualityID=@GoldQualityID AND I.ItemCategoryID=@ItemCategoryID AND H.DisableDate=@ForDate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
                DB.AddInParameter(DBComm, "@ForDate", DbType.Date, ForDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllMortgageInvoiceFromSearchBox() As System.Data.DataTable Implements IMortgageInvoiceDA.GetAllMortgageInvoiceFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select MortgageInvoiceID, MortgageInvoiceCode,IsReturn as [$IsReturn],convert(varchar(10),ReceiveDate,105) as ReceiveDate,MortgageStaff as [@MortgageStaff],S.Staff as [Staff],L.Location,InterestRate,CustomerName AS [CustomerName_],tbl_MortgageInvoice.Address AS [Address_],TotalAmount,TotalQTY,Remark AS [Remark_] From tbl_MortgageInvoice  left join tbl_Staff S on S.StaffID=tbl_MortgageInvoice .MortgageStaff Left Join tbl_Location L on tbl_MortgageInvoice.LocationID=L.LocationID Order by tbl_MortgageInvoice .MortgageInvoiceID  "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInvoiceItemFromSearchBox(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoiceItemFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dt As New DataTable
            Try
                strCommandText = "SELECT I.MortgageItemID, H.MortgageInvoiceID, GoldQuality, MortgageRate, ItemCategory as [ItemCategory_], ItemName as [ItemName_], QTY, " & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS GoldY," & _
                " GoldTK, GoldTG, Amount" & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                " WHERE H.MortgageInvoiceID=@MortgageInvoiceID ORDER BY I.MortgageItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dt = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dt
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInvoicePrint(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoicePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.MortgageInvoiceID, H.MortgageInvoiceCode, ReceiveDate,DATEADD(MONTH,InterestPeriod,ReceiveDate)as DueDate, H.LocationID, Location, S.Staff as MortgageStaff, InterestRate,InterestPeriod, Cus.CustomerName, Cus.CustomerAddress,TotalAmount, TotalQTY, H.Remark, IsReturn, ReturnDate,InterestAmount, NetAmount, AddOrSub, PaidAmount, RRemark, " & _
                " I.GoldQualityID, GoldQuality, I.ItemCategoryID, ItemCategory, ItemName, QTY, I.Amount-isnull(P.PaybackAmount,0) as Amount, " & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, GoldTK,(H.TotalAmount*(InterestRate/100)) as InterestPayment,((H.TotalAmount/InterestRate)*InterestRate) as TotalInterestPayment,'' as Photo,GoldTG,GoldTK,I.MortgageItemCode,I.GoldTK as TotalTK,I.GoldTG as TotalTG " & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff  " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Customer Cus ON H.CustomerID=Cus.CustomerID " & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                " LEFT JOIN (Select MortgageItemID,Sum(Amount) as PaybackAmount From tbl_MortgagePaybackItem PI Inner Join tbl_MortgagePayback P on PI.MortgagePaybackID=P.MortgagePaybackID " & _
                " where P.isDelete=0 And P.MortgageInvoiceID=@MortgageInvoiceID group by MortgageItemID) as P on I.MortgageItemID=P.MortgageItemID " & _
                " WHERE H.MortgageInvoiceID= @MortgageInvoiceID and I.MortgageItemID " & _
                " not in (select MortgageItemID From tbl_MortgageReturnItem I Inner Join tbl_MortgageReturn R on I.MortgageReturnID=R.MortgageReturnID " & _
                " where R.isDelete=0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInvoiceByMortgageCode(ByVal MortgageInvoiceCode As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoiceByMortgageCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select IsDisable,MortgageInvoiceID as [@MortgageInvoiceID], MortgageInvoiceCode,ReceiveDate,MortgageStaff as [@MortgageStaff],S.Staff as [Staff_],InterestRate,CustomerName AS [CustomerName_],Address AS [Address_],TotalAmount,TotalQTY From tbl_MortgageInvoice  left join tbl_Staff S on S.StaffID=tbl_MortgageInvoice .MortgageStaff where IsReturn=0 And MortgageInvoiceCode = '" & MortgageInvoiceCode & "' Order by tbl_MortgageInvoice .MortgageInvoiceCode  "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInvoiceDisableReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoiceDisableReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.IsDisable,H.DisableDate,L.Location, H.MortgageInvoiceID, H.MortgageInvoiceCode,ReceiveDate, S.Staff,Cus.CustomerName, Cus.CustomerAddress, InterestRate, TotalAmount, TotalQTY, H.Remark, " & _
                " I.GoldQualityID, GoldQuality, I.ItemCategoryID, ItemCategory, ItemName, QTY, GoldTK, Amount,I.MortgageRate, " & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY " & _
                " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                " INNER JOIN tbl_Customer Cus on H.CustomerID=Cus.CustomerID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.MortgageStaff" & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                " WHERE DisableDate BETWEEN @FromDate AND @ToDate " & cristr & " Order by H.MortgageInvoiceCode"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInvoiceExcludeInMortgageItems() As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoiceExcludeInMortgageItems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select M.IsDisable as [$IsDisable],M.MortgageInvoiceID as [@MortgageInvoiceID], M.MortgageInvoiceCode, " & _
                        " convert(varchar(10),M.ReceiveDate,105) as [ReceiveDate],S.Staff as [Staff_]," & _
                        " M.InterestRate,M.CustomerName AS [CustomerName_],M.Address AS [Address_],M.TotalAmount,M.TotalQTY," & _
                        " M.Remark AS [Remark_],M.IsReturn as [$IsReturn] From tbl_MortgageInvoice M " & _
                        " left join tbl_Staff S on S.StaffID=M.MortgageStaff where IsReturn=0 " & _
                        " and M.MortgageInvoiceID Not In (select M.MortgageInvoiceID from tbl_MortgageInvoice M right Join tbl_MortgageItems I On M.MortgageInvoiceID=I.MortgageInvoiceID)" & _
                        " Order by M.MortgageInvoiceCode"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateMortgageInvoiceRepay(ByVal MortgageInvoiceID As String) As Boolean Implements IMortgageInvoiceDA.UpdateMortgageInvoiceRepay
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_MortgageInvoice set IsRepayByHeadOffice=@IsRepayByHeadOffice "
                strCommandText += " where MortgageInvoiceID= @MortgageInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@IsRepayByHeadOffice", DbType.Boolean, True)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)
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

        Public Function GetAllMortgageInvoiceByIsRepayHeadOffice() As System.Data.DataTable Implements IMortgageInvoiceDA.GetAllMortgageInvoiceByIsRepayHeadOffice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "  Select distinct IsReturn as [$IsReturn],M.IsPayback as [$IsPayback],IsDisable as [$IsDisable],M.MortgageInvoiceID as [@MortgageInvoiceID], " & _
                                 " MortgageInvoiceCode, convert(varchar(10),ReceiveDate,105) as [ReceiveDate],MortgageStaff as [@MortgageStaff], " & _
                                  "S.Staff as [Staff_],InterestRate,C.CustomerName AS [CustomerName_],C.CustomerAddress AS [Address_], " & _
                                  "(M.TotalAmount-(IsNull(P.PaidAmount,0)-IsNull(P.InterestAmt,0))) as TotalAmount,TotalQTY,M.Remark AS [Remark_] From tbl_MortgageInvoice M  " & _
                                  " left Join tbl_mortgageinvoiceItem I on M.MortgageInvoiceID=I.MortgageInvoiceID " & _
                                  "left JOIN (select MortgageInvoiceID,sum(PaidAmount) as PaidAmount,sum(InterestAmt) as InterestAmt from tbl_MortgagePayback group by MortgageInvoiceID) as P ON M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                  "left join tbl_Customer C on M.CustomerID=C.CustomerID " & _
                                  "left join tbl_Staff S on S.StaffID=M.MortgageStaff where M.IsDelete=0 and M.isReturn=0 " & _
                                  "And I.MortgageItemID not in (select mortgageitemID from tbl_MortgageReturnItem I " & _
                                  "Inner Join tbl_MortgageReturn R on I.MortgageReturnId=R.MortgageReturnID Where R.isDelete=0 )" & _
                                  "Order by [ReceiveDate] desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllMortgageInvoiceByReturn() As System.Data.DataTable Implements IMortgageInvoiceDA.GetAllMortgageInvoiceByReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "  Select distinct IsReturn as [$IsReturn],M.IsPayback as [$IsPayback],IsDisable as [$IsDisable],M.MortgageInvoiceID as [@MortgageInvoiceID], " & _
                                 " MortgageInvoiceCode, convert(varchar(10),ReceiveDate,105) as [ReceiveDate],MortgageStaff as [@MortgageStaff], " & _
                                  "S.Staff as [Staff_],InterestRate,C.CustomerName AS [CustomerName_],C.CustomerAddress AS [Address_], " & _
                                  "(M.TotalAmount-(IsNull(P.PaidAmount,0)-IsNull(P.InterestAmt,0))) as TotalAmount,TotalQTY,M.Remark AS [Remark_] From tbl_MortgageInvoice M  " & _
                                  "left JOIN (select MortgageInvoiceID,sum(PaidAmount) as PaidAmount,sum(InterestAmt) as InterestAmt from tbl_MortgagePayback group by MortgageInvoiceID) as P ON M.MortgageInvoiceID=P.MortgageInvoiceID " & _
                                  "left join tbl_Customer C on M.CustomerID=C.CustomerID " & _
                                  "left join tbl_Staff S on S.StaffID=M.MortgageStaff Left Join tbl_MortgageInvoiceItem I on M.MortgageInvoiceID=I.MortgageInvoiceID where M.IsDelete=0 And I.MortgageItemID not in " & _
                                  " (select MortgageItemID from tbl_MortgageReturn R " & _
                                  " Inner Join tbl_MortgageReturnItem I On R.MortgageReturnID=I.MortgageReturnID Where R.IsDelete=0)  " & _
                                  "Order by M.MortgageInvoiceCode desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMortgageInvoiceByMortgageCodeOnIsRepayHO(ByVal MortgageInvoiceCode As String) As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageInvoiceByMortgageCodeOnIsRepayHO
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select IsDisable,MortgageInvoiceID as [@MortgageInvoiceID], MortgageInvoiceCode,ReceiveDate,MortgageStaff as [@MortgageStaff],S.Staff as [Staff_]," & _
                    "InterestRate,C.CustomerName AS [CustomerName_],C.CustomerAddress AS [Address_],TotalAmount,TotalQTY From tbl_MortgageInvoice M    left join tbl_Customer C on  M.CustomerID=C.CustomerID  left join tbl_Staff S on S.StaffID=M.MortgageStaff where MortgageInvoiceCode = '" & MortgageInvoiceCode & "' Order by M.MortgageInvoiceCode  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgageDataByMortgageInvoiceID(ByVal argstr As String, Optional ByVal MortgageInvoiceID As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageDataByMortgageInvoiceID
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim dtResult As DataTable

            Dim strWhere As String

            If argstr <> "" Then
                strWhere = " WHERE I.MortgageItemID not in(select MortgageItemID from tbl_MortgageReturnItem RI " & _
                            " Inner Join tbl_mortgageReturn R on R.MortgageReturnID=RI.MortgageReturnID  where R.MortgageInvoiceID=@MortgageInvoiceID and R.isDelete=0) " & _
                            " And I.MortgageInvoiceID='" & MortgageInvoiceID & "'" & " AND I.MortgageItemID NOT IN (" & argstr & ")"

            Else
                strWhere = " WHERE I.MortgageItemID not in(select MortgageItemID from tbl_MortgageReturnItem RI " & _
                            " Inner Join tbl_mortgageReturn R on R.MortgageReturnID=RI.MortgageReturnID  where R.MortgageInvoiceID=@MortgageInvoiceID and R.isDelete=0) " & _
                            " And I.MortgageInvoiceID='" & MortgageInvoiceID & "'"
            End If
            Try

                strCommandText = "SELECT I.MortgageItemID, H.MortgageInvoiceID, I.GoldQualityID as [@GoldQualityID], GoldQuality, ItemCategory, I.MortgageRate, I.ItemCategoryID as [@ItemCategoryID], ItemName, QTY, " & _
                   " CAST(GoldTK AS INT) AS GoldK," & _
                   " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                   " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'8.0'AS DECIMAL(18,2)) AS GoldY," & _
                   " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
                   " GoldTK as [@GoldTK], GoldTG as [@GoldTG],(I.Amount-isnull(P.PaidAmount,0)) as Amount, IsDone,DonePercent,ItemNameID as [@ItemNameID],H.IsPayback as [@IsPayback],I.IsPayback as [@ItemPayback]" & _
                   " FROM tbl_MortgageInvoice H INNER JOIN tbl_MortgageInvoiceItem I ON H.MortgageInvoiceID=I.MortgageInvoiceID " & _
                   " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                   " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=I.ItemCategoryID " & _
                   " left join ( select sum(I.Amount) as PaidAmount,sum(InterestAmt) as InterestAmt,sum(P.PaidAmount) as PaybackAmt,I.MortgageItemID  " & _
                   " from tbl_MortgagePayback P " & _
                   " Inner Join tbl_MortgageInvoice M on M.MortgageInvoiceID=P.MortgageInvoiceID  " & _
                   " Inner join tbl_mortgagepaybackitem I on P.Mortgagepaybackid=I.mortgagePaybackID where M.IsDelete=0 and P.isDelete=0  " & _
                   " and P.MortgageInvoiceID=@MortgageInvoiceID group by I.MortgageItemID ) P on I.MortgageItemID=P.MortgageItemId " & strWhere & " ORDER BY I.MortgageItemID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@MortgageInvoiceID", DbType.String, MortgageInvoiceID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetMortgageReturnReportSum(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageReturnReportSum
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT TotalAmount,InterestAmount,PaidAmount,AddOrSub " & _
                " FROM tbl_MortgageInvoice H " & _
                " WHERE IsReturn=@IsReturn AND ReturnDate BETWEEN @FromDate And @ToDate " & cristr & " And IsDelete=0 Order by H.MortgageInvoiceCode"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, True)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMortgageReturnReportSumNew(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceDA.GetMortgageReturnReportSumNew
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT R.TotalAmount,R.InterestAmount,R.PaidAmount,R.AddOrSub " & _
                " FROM tbl_MortgageReturn R " & _
                " INNER JOIN tbl_MortgageInvoice H ON R.MortgageInvoiceID=H.MortgageInvoiceID " & _
                " WHERE R.ReturnDate BETWEEN @FromDate and @ToDate " & cristr & " And R.IsDelete=0 Order by R.MortgageInvoiceID"
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

