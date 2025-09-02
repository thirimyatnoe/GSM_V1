Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace WholeSaleReturn
    Public Class WholeSaleReturnDA
        Implements IWholeSaleReturnDA




#Region "Private WholeSaleReturn"

        Private DB As Database
        Private Shared ReadOnly _instance As IWholeSaleReturnDA = New WholeSaleReturnDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IWholeSaleReturnDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteWholeSaleReturn(ByVal WholeSaleReturnID As String) As Boolean Implements IWholeSaleReturnDA.DeleteWholeSaleReturn
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_WholesaleReturn SET IsDelete =CONVERT(bit,1),IsUpload= CONVERT(bit,0) WHERE  WholesaleReturnID= @WholesaleReturnID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleReturnID", DbType.String, WholeSaleReturnID)
          
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

        Public Function DeleteWholeSaleReturnItem(ByVal WholeSaleReturnItemID As String) As Boolean Implements IWholeSaleReturnDA.DeleteWholeSaleReturnItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_WholesaleReturnItem WHERE  WholeSaleReturnItemID= @WholeSaleReturnItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleReturnItemID", DbType.String, WholeSaleReturnItemID)
     
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

        Public Function GetAllWholeSaleReturn() As System.Data.DataTable Implements IWholeSaleReturnDA.GetAllWholeSaleReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT WholesaleReturnID,convert(varchar(10),WS.WReturnDate,105) as ReturnDate,WS.WholesaleInvoiceID,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,SaleReturnAmount,TotalAmount,AddOrSub,PaidAmount   " & _
                                  " From tbl_WholesaleReturn WS INNER join tbl_Staff S On WS.StaffID=S.StaffID INNER Join tbl_Customer C on WS.CustomerID=C.CustomerID WHERE WS.IsDelete=0 and C.IsDelete=0 AND S.IsDelete=0 Order by WS.WReturnDate desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSaleReturnItem(ByVal WholeSaleReturnItemID As String) As System.Data.DataTable Implements IWholeSaleReturnDA.GetWholeSaleReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * " & _
                                " From tbl_WholesaleReturnItem Where WholesaleReturnID='" & WholeSaleReturnItemID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetWholeSaleReturnByWSID(ByVal WholeSaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleReturnDA.GetWholeSaleReturnByWSID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * " & _
                                " From tbl_WholesaleReturn Where WholesaleInvoiceID='" & WholeSaleInvoiceID & "' And IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSaleReturnItemByID(ByVal WholeSaleReturnID As String) As System.Data.DataTable Implements IWholeSaleReturnDA.GetWholeSaleReturnItemByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'CommssionDis Calculation for Amount 
                strCommandText = " SELECT   ROW_NUMBER() OVER (ORDER BY I.WholeSaleReturnItemID) AS SNo,I.WholeSaleReturnItemID  as [WSReturnItemID],I.WholeSaleReturnID as [WSReturnID],I.ForSaleID,I.ItemCode,I.GoldQualityID,I.ItemNameID,N.ItemName,GQ.GoldQuality,I.ItemTG as Gram, " & _
                                    "I.IsReturn,I.IsSale,Case when (IsSale='1' and IsReturn='1') then 'False' " & _
                                    "when (IsSale='0' and IsReturn='1') then 'True' when (IsSale='1' and IsReturn='0') " & _
                                    "then 'False' End as IsShowForReturn,SalesRate,I.ItemTG, I.GoldPrice as Amount , I.ItemTK as ItemTK,CAST((I.ItemTK-I.GemsTK) AS INT)  as GoldTK,I.itemTG As ItemTG,CAST((I.ItemTG-I.GemsTG) As INT) AS GoldTG," & _
                                    "CAST((I.ItemTK-I.GemsTK) AS INT) AS GoldK,CAST(((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                    "CAST(((((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16)-CAST(((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                    "CAST(I.wasteTG AS INT) As WasteTG,CAST(I.WasteTK As INT) AS WasteTK,CAST(I.GemsTG As INT) AS GemsTG, CAST(I.GemsTK As INT) AS GemsTK," & _
                                    "CAST(I.WasteTK AS INT) AS WasteK, CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                    "CAST((((I.WasteTK-CAST(I.WasteTK AS INT))*16)-CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                    "CAST(I.ItemTK AS INT) AS ItemK, CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                    "CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,CAST(I.GemsTK AS INT) AS GemsK,CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT) AS GemsP,CAST((((I.GemsTK-CAST(I.GemsTK AS INT))*16)-CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY,I.GoldPrice,I.FixPrice  " & _
                                    " From tbl_WholesaleReturnItem I " & _
                                    "INNER JOIN tbl_WholesaleReturn H  ON I.WholesaleReturnID=H.WholesaleReturnID  " & _
                                    "INNER JOIN tbl_ItemName N On I.ItemNameID=N.ItemNameID  " & _
                                    "INNER JOIN tbl_GoldQuality GQ On I.GoldQualityID=GQ.GoldQualityID " & _
                                    "where I.WholeSaleReturnID=@WholeSaleReturnID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleReturnID", DbType.String, WholeSaleReturnID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
           
        End Function

        Public Function GetWholeSaleReturnByID(ByVal WholeSaleReturnID As String) As CommonInfo.WholeSaleReturnInfo Implements IWholeSaleReturnDA.GetWholeSaleReturnByID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New WholeSaleReturnInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_WholesaleReturn WHERE WholeSaleReturnID= @WholeSaleReturnID AND IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleReturnID", DbType.String, WholeSaleReturnID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .WholesaleReturnID = drResult("WholesaleReturnID")
                        .WReturnDate = drResult("WReturnDate")
                        .WholeSaleInvoiceID = drResult("WholeSaleInvoiceID")
                        .ConsignmentSaleID = drResult("ConsignmentSaleID")
                        .StaffID = drResult("StaffID")
                        .CustomerID = drResult("CustomerID")
                        .Remark = drResult("Remark")
                        .SaleAmount = drResult("SaleAmount")
                        .SaleReturnAmount = drResult("SaleReturnAmount")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .PaidAmount = drResult("PaidAmount")
                        .Discount = drResult("Discount")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertWholeSaleReturn(ByVal obj As CommonInfo.WholeSaleReturnInfo) As Boolean Implements IWholeSaleReturnDA.InsertWholeSaleReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_WholesaleReturn ( WholesaleReturnID,WReturnDate,WholeSaleInvoiceID,ConsignmentSaleID,StaffID,CustomerID,Remark,SaleAmount,SaleReturnAmount,TotalAmount,AddOrSub,PaidAmount,Discount,LastModifiedLoginUserName,LastModifiedDate,LocationID,IsUpload,IsDelete)"
                strCommandText += " Values (@WholesaleReturnID,@WReturnDate,@WholeSaleInvoiceID,@ConsignmentSaleID,@StaffID,@CustomerID,@Remark,@SaleAmount,@SaleReturnAmount,@TotalAmount,@AddOrSub,@PaidAmount,@Discount,@LastModifiedLoginUserName,@LastModifiedDate,@LocationID,CONVERT(bit,0),CONVERT(bit,0))"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleReturnID", DbType.String, obj.WholesaleReturnID)
                DB.AddInParameter(DBComm, "@WReturnDate", DbType.DateTime, obj.WReturnDate)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, obj.WholeSaleInvoiceID)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, obj.ConsignmentSaleID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, obj.CustomerID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@SaleAmount", DbType.Int32, obj.SaleAmount)
                DB.AddInParameter(DBComm, "@SaleReturnAmount", DbType.Int32, obj.SaleReturnAmount)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, obj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, obj.Discount)

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

        Public Function InsertWholeSaleReturnItem(ByVal obj As CommonInfo.WholeSaleReturnItemInfo) As Boolean Implements IWholeSaleReturnDA.InsertWholeSaleReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_WholesaleReturnItem ( WholesaleReturnItemID,WholesaleReturnID,ForSaleID,ItemNameID,GoldQualityID,ItemCode,IsReturn,IsSale,SalesRate,ItemTG,ItemTK,GoldTG,GoldTK,GemsTG,GemsTK,WasteTG,WasteTK,GoldPrice,FixPrice)"
                strCommandText += " Values (@WholesaleReturnItemID,@WholesaleReturnID,@ForSaleID,@ItemNameID,@GoldQualityID,@ItemCode,@IsReturn,@IsSale,@SalesRate,@ItemTG,@ItemTK,@GoldTG,@GoldTK,@GemsTG,@GemsTK,@WasteTG,@WasteTK,@GoldPrice,@FixPrice)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleReturnItemID", DbType.String, obj.WholesaleReturnItemID)
                DB.AddInParameter(DBComm, "@WholesaleReturnID", DbType.String, obj.WholesaleReturnID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, obj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, obj.IsReturn)
                DB.AddInParameter(DBComm, "@IsSale", DbType.Boolean, obj.IsSale)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int32, obj.SalesRate)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, obj.ItemTG)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, obj.ItemTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, obj.GoldTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, obj.GoldTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int32, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int32, obj.FixPrice)

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

        Public Function UpdateWholeSaleReturn(ByVal obj As CommonInfo.WholeSaleReturnInfo) As Boolean Implements IWholeSaleReturnDA.UpdateWholeSaleReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_WholesaleReturn set  WholesaleReturnID= @WholesaleReturnID , WReturnDate= @WReturnDate ,WholeSaleInvoiceID=@WholeSaleInvoiceID, ConsignmentSaleID=@ConsignmentSaleID,StaffID= @StaffID , CustomerID= @CustomerID , Remark= @Remark , SaleAmount= @SaleAmount , SaleReturnAmount= @SaleReturnAmount , TotalAmount= @TotalAmount ,Discount=@Discount, AddOrSub= @AddOrSub , PaidAmount= @PaidAmount ,LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate , LocationID= @LocationID ,IsUpload= CONVERT(bit,0) "
                strCommandText += " where WholesaleReturnID= @WholesaleReturnID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleReturnID", DbType.String, Obj.WholesaleReturnID)
                DB.AddInParameter(DBComm, "@WReturnDate", DbType.DateTime, obj.WReturnDate)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, obj.WholeSaleInvoiceID)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, obj.ConsignmentSaleID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@SaleAmount", DbType.Int32, Obj.SaleAmount)
                DB.AddInParameter(DBComm, "@SaleReturnAmount", DbType.Int32, Obj.SaleReturnAmount)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, obj.TotalAmount)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, obj.Discount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
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

        Public Function UpdateWholeSaleReturnItem(ByVal obj As CommonInfo.WholeSaleReturnItemInfo) As Boolean Implements IWholeSaleReturnDA.UpdateWholeSaleReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_WholesaleReturnItem set  WholesaleReturnItemID= @WholesaleReturnItemID , WholesaleReturnID= @WholesaleReturnID , ForSaleID= @ForSaleID , ItemCode= @ItemCode , IsReturn= @IsReturn , IsSale= @IsSale , SalesRate= @SalesRate,ItemTK=@ItemTK,ItemTG=@ItemTG,GemsTK=GemsTK,WasteTK=@WasteTK,WasteTG=@WasteTG , GoldTK=@GoldTK,GoldTG=@GoldTG,GoldPrice=@GoldPrice,FixPrice=@FixPrice  "
                strCommandText += " where WholesaleReturnItemID= @WholesaleReturnItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleReturnItemID", DbType.String, obj.WholesaleReturnItemID)
                DB.AddInParameter(DBComm, "@WholesaleReturnID", DbType.String, obj.WholesaleReturnID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, obj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, obj.IsReturn)
                DB.AddInParameter(DBComm, "@IsSale", DbType.Boolean, obj.IsSale)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int32, obj.SalesRate)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, obj.ItemTG)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, obj.ItemTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, obj.GoldTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, obj.GoldTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int32, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int32, obj.FixPrice)

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

        Public Function GetWholeSaleReturnPrint(ByVal WholeSaleReturnID As String) As System.Data.DataTable Implements IWholeSaleReturnDA.GetWholeSaleReturnPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  W.WholeSaleReturnID, W.WReturnDate, S.Staff, C.CustomerName,C.CustomerCode, C.CustomerAddress,C.CustomerTel, SaleAmount, SaleReturnAmount, TotalAmount as NetAmount, AddOrSub, " & _
                                   " PaidAmount, W.Discount, ConsignmentSaleID, WI.WholeSaleReturnItemID ,ItemCategory,N.ItemName, " & _
                                   " WI.ForSaleID,WI.ItemCode,WI.ItemTG as [Gram],IsReturn,IsSale,WI.ItemTG,GQ.IsGramRate, " & _
                                   " Case when (IsSale='1' and IsReturn='1') then 'False' when (IsSale='0' and IsReturn='1') then 'True' " & _
                                   " when (IsSale='1' and IsReturn='0') then 'False' End as IsShowForReturn,SalesRate, WI.GoldPrice,WI.FixPrice , " & _
                                   " CAST((WI.ItemTK-WI.GemsTK) AS INT) AS GoldK, " & _
                                   " CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                   " CAST(((((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16)-CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                   " CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                   " CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                   " CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY, " & _
                                   " CAST(WI.GemsTK AS INT) AS GemsK,CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                   " CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY ," & _
                                   " CAST((WI.ItemTK-WI.GemsTK)+WI.WasteTK AS INT) AS TotalK,  " & _
                                   " CAST(((WI.ItemTK-WI.GemsTK)+WI.WasteTK-CAST((WI.ItemTK-WI.GemsTK)+WI.WasteTK AS INT))*16 AS INT) AS TotalP,   " & _
                                   " CAST(((((WI.ItemTK-WI.GemsTK)+WI.WasteTK-CAST((WI.ItemTK-WI.GemsTK)+WI.WasteTK AS INT))*16)-CAST(((WI.ItemTK-WI.GemsTK)+WI.WasteTK-CAST((WI.ItemTK-WI.GemsTK)+WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS TotalY " & _
                                   " From tbl_WholesaleReturnItem WI LEFT JOIN tbl_WholeSaleReturn W ON W.WholeSaleReturnID=WI.WholeSaleReturnID  " & _
                                   " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID LEFT JOIN tbl_ItemName N ON WI.ItemNameID=N.ItemNameID  " & _
                                   " LEFT JOIN tbl_GoldQuality GQ On F.GoldQualityID=GQ.GoldQualityID " & _
                                   " LEFT JOIN tbl_ItemCategory IC ON IC.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_Staff S ON S.StaffID=W.StaffID " & _
                                   " LEFT JOIN tbl_Customer C ON C.CustomerID=W.CustomerID  LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID " & _
                                   " where W.WholeSaleReturnID=@WholeSaleReturnID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleReturnID", DbType.String, WholeSaleReturnID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSaleReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleReturnDA.GetWholeSaleReturnReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = " SELECT WI.WholesaleReturnItemID AS WholeSaleReturnItemID, WI.WholesaleReturnID As WholeSaleReturnID, WI.ForSaleID, WI.ItemCode, " & _
                                 " WI.IsReturn,WI.IsSale ,WI.ItemTG, WI.SalesRate,   W.WReturnDate, Staff, CustomerName,W.CustomerID, W.SaleAmount, W.SaleReturnAmount, " & _
                                 " W.TotalAmount, W.AddOrSub,     W.PaidAmount, Location,W.LocationID, W.ConsignmentSaleID, ItemCategory, " & _
                                 " OriginalCode,Discount, " & _
                                 " CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                 " CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,   " & _
                                 " CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 " CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " CAST(WI.GoldTK AS INT) AS GoldK, CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT) AS GoldP,  " & _
                                 " CAST((((WI.GoldTK-CAST(WI.GoldTK AS INT))*16)-CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY," & _
                                 " CAST(WI.GemsTK AS INT) AS GemsK, CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY,WI.GoldPrice,WI.FixPrice,GQ.GoldQuality,W.TotalAmount as TotalPayment   FROM tbl_WholesaleReturnItem WI " & _
                                 " LEFT JOIN tbl_WholesaleReturn W ON W.WholesaleReturnID=WI.WholesaleReturnID   " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID   " & _
                                 " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID    " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID  " & _
                                 " WHERE W.IsDelete=0 AND W.WReturnDate  BETWEEN @FromDate And @ToDate " & GetFilterString

                DBComm = DB.GetSqlStringCommand(strCommandText)
                ' DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
                'DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSaleReturnReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleReturnDA.GetWholeSaleReturnReportAmount
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = " select sum(TotalAmount) as WSNetAmount,customerid as WSCustomerID from  " & _
                                 " (Select distinct W.CustomerID, w.wholesaleinvoiceid,W.TotalAmount " & _
                                 " FROM tbl_WholesaleReturnItem WI " & _
                                 " LEFT JOIN tbl_WholesaleReturn W ON W.WholesaleReturnID=WI.WholesaleReturnID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID " & _
                                 " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID " & _
                                 " LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID   WHERE W.IsDelete=0 " & _
                                 " AND W.WReturnDate  BETWEEN @FromDate and @ToDate " & GetFilterString & ") as tmp group by customerid Order by CustomerID asc"

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