Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace SalesOrder
    Public Class SalesOrderDA
        Implements ISalesOrderDA

#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As ISalesOrderDA = New SalesOrderDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesOrderDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteSalesOrder(ByVal SalesOrderID As String) As Boolean Implements ISalesOrderDA.DeleteSalesOrder
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SalesOrder WHERE  SaleOrderID= @SalesOrderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesOrderID", DbType.String, SalesOrderID)
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

        Public Function DeleteSalesOrderItem(ByVal SalesOrderItemID As String) As Boolean Implements ISalesOrderDA.DeleteSalesOrderItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SalesOrderGemsItem WHERE  SaleOrderItemID= @SalesOrderItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesOrderItemID", DbType.String, SalesOrderItemID)
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

        Public Function GetAllSalesOrder(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesOrderDA.GetAllSalesOrder
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select ItemCode,ForSaleID as [@ForSaleID],SaleOrderID,convert(varchar(10),OrderDate,105) as OrderDate,convert(varchar(10),OrderRetrieveDate,105)as OrderRetrieveDate,IsReturn as [$IsReturn],ItemName as [ItemName_],I.ItemCategory as [@ItemCategory],G.GoldQuality,tbl_SalesOrder.StaffID as [@StaffID],S.Name as [Name_],Customer as [Customer_],IsSalesReturn as [$IsSalesReturn] "
                strCommandText += " from tbl_SalesOrder left join tbl_ItemCategory I on tbl_SalesOrder.ItemCategoryID=I.ItemCategoryID left join tbl_GoldQuality G on tbl_SalesOrder.GoldQualityID=G.GoldQualityID left join tbl_StaffByLocation S on tbl_SalesOrder.StaffID=S.SaleStaffID " & cristr & " order by ItemCode"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSalesOrder(ByVal SalesOrderID As String) As CommonInfo.SalesOrderInfo Implements ISalesOrderDA.GetSalesOrder
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSalesOrder As New SalesOrderInfo
            Try
                strCommandText = " SELECT  *,"
                strCommandText += " CAST(GoldTK AS INT) AS GoldK,  "
                strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
                strCommandText += " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8)-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC, "
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,  "
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS INT) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8)-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS WasteC, "
                strCommandText += " CAST(TotalTK AS INT) AS TotalK, "
                strCommandText += " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT) AS TotalY,"
                strCommandText += " CAST(((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8)-CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC "
                strCommandText += " FROM tbl_SalesOrder WHERE SaleOrderID= @SalesOrderID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesOrderID", DbType.String, SalesOrderID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSalesOrder
                        .ForSaleID = drResult("ForSaleID")
                        .SaleOrderID = drResult("SaleOrderID")
                        .CounterID = drResult("CounterID")
                        .OrderDate = drResult("OrderDate")
                        .StaffID = drResult("StaffID")
                        .Customer = drResult("Customer")
                        .Address = drResult("Address")
                        .ItemCode = drResult("ItemCode")
                        .ItemName = drResult("ItemName")
                        .Length = IIf(IsDBNull(drResult("Length")), "-", drResult("Length"))

                        .Width = IIf(IsDBNull(drResult("Width")), "-", drResult("Width"))

                        .ItemCategoryID = drResult("ItemCategoryID")
                        .GoldQualityID = drResult("GoldQualityID")
                        .SalesRate = drResult("SalesRate")
                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .GoldC = Format(drResult("GoldC"), "0.0")
                        .GemsK = drResult("GemsK")
                        .GemsP = drResult("GemsP")
                        .GemsY = drResult("GemsY")
                        .GemsC = Format(drResult("GemsC"), "0.0")
                        .WasteK = drResult("WasteK")
                        .WasteP = drResult("WasteP")
                        .WasteY = drResult("WasteY")
                        .WasteC = Format(drResult("WasteC"), "0.0")
                        .TotalK = drResult("TotalK")
                        .TotalP = drResult("TotalP")
                        .TotalY = drResult("TotalY")
                        .TotalC = Format(drResult("TotalC"), "0.0")
                        .TotalPayment = drResult("TotalPayment")
                        .AddOrSub = drResult("AddOrSub")
                        .AdvanceAmount = drResult("AdvanceAmount")
                        .PaidAmount = drResult("PaidAmount")
                        .DiscountAmount = drResult("DiscountAmount")
                        .GoldPrice = drResult("GoldPrice")
                        .GemsPrice = drResult("GemsPrice")
                        .DesignCharges = drResult("DesignCharges")
                        .Remark = IIf(IsDBNull(drResult("Remark")), "-", drResult("Remark"))
                        .IsReturn = drResult("IsReturn")
                        .OrderRetrieveDate = drResult("OrderRetrieveDate")
                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .TotalTK = drResult("TotalTK")
                        .TotalTG = drResult("TotalTG")
                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objSalesOrder
        End Function

        Public Function GetSalesOrderItem(ByVal SalesOrderID As String) As System.Data.DataTable Implements ISalesOrderDA.GetSalesOrderItem

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select SaleOrderItemID,SaleOrderID As [@SaleOrderID],GemsCategoryID as [@GemsCategoryID],GemsName,GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,UnitPrice,Type,Amount,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8)-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC "
                strCommandText += " from tbl_SalesOrderGemsItem Where SaleOrderID ='" & SalesOrderID & "' Order By SaleOrderItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function InsertSalesOrder(ByVal obj As CommonInfo.SalesOrderInfo) As Boolean Implements ISalesOrderDA.InsertSalesOrder
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_SalesOrder ( SaleOrderID,OrderDate,IsReturn,OrderRetrieveDate,StaffID,Customer,Address,ItemCategoryID,GoldQualityID,ForSaleID,ItemCode,ItemName,Length,Width,SalesRate,GoldTK,GoldTG,GemsTK,GemsTG,WasteTK,WasteTG,TotalTK,TotalTG,TotalPayment,AddOrSub,AdvanceAmount,PaidAmount,DiscountAmount,GoldPrice,GemsPrice,DesignCharges,Remark,LastModifiedLoginUserName,LastModifiedDate,LocationID,CounterID,IsSalesReturn)"
                strCommandText += " Values (@SaleOrderID,@OrderDate,@IsReturn,@OrderRetrieveDate,@StaffID,@Customer,@Address,@ItemCategoryID,@GoldQualityID,@ForSaleID,@ItemCode,@ItemName,@Length,@Width,@SalesRate,@GoldTK,@GoldTG,@GemsTK,@GemsTG,@WasteTK,@WasteTG,@TotalTK,@TotalTG,@TotalPayment,@AddOrSub,@AdvanceAmount,@PaidAmount,@DiscountAmount,@GoldPrice,@GemsPrice,@DesignCharges,@Remark,@LastModifiedLoginUserName,GetDate(),@LocationID,@CounterID,@IsSalesReturn)"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@SaleOrderID", DbType.String, obj.SaleOrderID)
                DB.AddInParameter(DBComm, "@OrderDate", DbType.Date, obj.OrderDate)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, obj.IsReturn)
                DB.AddInParameter(DBComm, "@OrderRetrieveDate", DbType.Date, obj.OrderRetrieveDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@Customer", DbType.String, obj.Customer)
                DB.AddInParameter(DBComm, "@Address", DbType.String, obj.Address)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, obj.ItemName)
                DB.AddInParameter(DBComm, "@Length", DbType.String, obj.Length)
                DB.AddInParameter(DBComm, "@Width", DbType.String, obj.Width)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, obj.SalesRate)
                ''DB.AddInParameter(DBComm, "@GoldK", DbType.Int32, obj.GoldK)
                ''DB.AddInParameter(DBComm, "@GoldP", DbType.Int32, obj.GoldP)
                ''DB.AddInParameter(DBComm, "@GoldY", DbType.Int32, obj.GoldY)
                ''DB.AddInParameter(DBComm, "@GoldC", DbType.Decimal, obj.GoldC)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, obj.GoldTG)
                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, obj.GemsK)
                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, obj.GemsP)
                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, obj.GemsY)
                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, obj.GemsC)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                'DB.AddInParameter(DBComm, "@WasteK", DbType.Int32, obj.WasteK)
                'DB.AddInParameter(DBComm, "@WasteP", DbType.Int32, obj.WasteP)
                'DB.AddInParameter(DBComm, "@WasteY", DbType.Int32, obj.WasteY)
                'DB.AddInParameter(DBComm, "@WasteC", DbType.Decimal, obj.WasteC)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                'DB.AddInParameter(DBComm, "@TotalK", DbType.Int32, obj.TotalK)
                'DB.AddInParameter(DBComm, "@TotalP", DbType.Int32, obj.TotalP)
                'DB.AddInParameter(DBComm, "@TotalY", DbType.Int32, obj.TotalY)
                'DB.AddInParameter(DBComm, "@TotalC", DbType.Decimal, obj.TotalC)
                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, obj.TotalTK)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, obj.TotalTG)
                DB.AddInParameter(DBComm, "@TotalPayment", DbType.Int64, obj.TotalPayment)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, obj.AddOrSub)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int64, obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, obj.GemsPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, obj.DesignCharges)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@CounterID", DbType.String, obj.CounterID)
                DB.AddInParameter(DBComm, "@IsSalesReturn", DbType.Int32, obj.IsSalesReturn)

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

        Public Function InsertSalesOrderItem(ByVal obj As CommonInfo.SalesOrderGemsItemInfo) As Boolean Implements ISalesOrderDA.InsertSalesOrderItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_SalesOrderGemsItem ( SaleOrderItemID,SaleOrderID,GemsCategoryID,GemsName,GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,UnitPrice,Type,Amount)"
                strCommandText += " Values (@SaleOrderItemID,@SaleOrderID,@GemsCategoryID,@GemsName,@GemsTK,@GemsTG,@YOrCOrG,@GemsTW,@Qty,@UnitPrice,@Type,@Amount)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleOrderItemID", DbType.String, obj.SaleOrderItemID)
                DB.AddInParameter(DBComm, "@SaleOrderID", DbType.String, obj.SaleOrderID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)

                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, obj.GemsK)
                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, obj.GemsP)
                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, obj.GemsY)
                'DB.AddInParameter(DBComm, "@GemsC", DbType.String, obj.GemsC)

                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, obj.GemsTW)

                'DB.AddInParameter(DBComm, "@GemY", DbType.Int32, obj.GemY)
                'DB.AddInParameter(DBComm, "@GemBCG", DbType.String, obj.GemBCG)
                'DB.AddInParameter(DBComm, "@GemP", DbType.String, obj.GemP)

                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, obj.Qty)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Type", DbType.String, obj.Type)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, obj.Amount)


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

        Public Function UpdateSalesOrder(ByVal obj As CommonInfo.SalesOrderInfo) As Boolean Implements ISalesOrderDA.UpdateSalesOrder
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SalesOrder set  OrderDate= @OrderDate , IsReturn= @IsReturn , OrderRetrieveDate= @OrderRetrieveDate , StaffID= @StaffID , Customer= @Customer , Address= @Address , ItemCategoryID= @ItemCategoryID , GoldQualityID= @GoldQualityID ,ForSaleID= @ForSaleID , ItemCode= @ItemCode , ItemName= @ItemName , Length= @Length , SalesRate= @SalesRate , GoldTK= @GoldTK , GoldTG= @GoldTG, GemsTK= @GemsTK , GemsTG= @GemsTG, WasteTK= @WasteTK , WasteTG= @WasteTG, TotalTK= @TotalTK , TotalTG= @TotalTG , TotalPayment= @TotalPayment , AddOrSub= @AddOrSub , AdvanceAmount= @AdvanceAmount, PaidAmount= @PaidAmount , DiscountAmount= @DiscountAmount, GoldPrice= @GoldPrice , GemsPrice= @GemsPrice , DesignCharges= @DesignCharges , Remark= @Remark , LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate=GetDate() , LocationID= @LocationID ,CounterID=@CounterID, IsSalesReturn= @IsSalesReturn "
                strCommandText += " where SaleOrderID= @SaleOrderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleOrderID", DbType.String, obj.SaleOrderID)
                DB.AddInParameter(DBComm, "@OrderDate", DbType.Date, obj.OrderDate)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, obj.IsReturn)
                DB.AddInParameter(DBComm, "@OrderRetrieveDate", DbType.Date, obj.OrderRetrieveDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@Customer", DbType.String, obj.Customer)
                DB.AddInParameter(DBComm, "@Address", DbType.String, obj.Address)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, obj.ItemName)
                DB.AddInParameter(DBComm, "@Length", DbType.String, obj.Length)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, obj.SalesRate)
                ''DB.AddInParameter(DBComm, "@GoldK", DbType.Int32, obj.GoldK)
                ''DB.AddInParameter(DBComm, "@GoldP", DbType.Int32, obj.GoldP)
                ''DB.AddInParameter(DBComm, "@GoldY", DbType.Int32, obj.GoldY)
                ''DB.AddInParameter(DBComm, "@GoldC", DbType.Decimal, obj.GoldC)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, obj.GoldTG)
                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, obj.GemsK)
                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, obj.GemsP)
                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, obj.GemsY)
                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, obj.GemsC)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                'DB.AddInParameter(DBComm, "@WasteK", DbType.Int32, obj.WasteK)
                'DB.AddInParameter(DBComm, "@WasteP", DbType.Int32, obj.WasteP)
                'DB.AddInParameter(DBComm, "@WasteY", DbType.Int32, obj.WasteY)
                'DB.AddInParameter(DBComm, "@WasteC", DbType.Decimal, obj.WasteC)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                'DB.AddInParameter(DBComm, "@TotalK", DbType.Int32, obj.TotalK)
                'DB.AddInParameter(DBComm, "@TotalP", DbType.Int32, obj.TotalP)
                'DB.AddInParameter(DBComm, "@TotalY", DbType.Int32, obj.TotalY)
                'DB.AddInParameter(DBComm, "@TotalC", DbType.Decimal, obj.TotalC)
                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, obj.TotalTK)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, obj.TotalTG)
                DB.AddInParameter(DBComm, "@TotalPayment", DbType.Int64, obj.TotalPayment)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, obj.AddOrSub)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int64, obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, obj.GemsPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, obj.DesignCharges)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@CounterID", DbType.String, obj.CounterID)
                DB.AddInParameter(DBComm, "@IsSalesReturn", DbType.Int32, obj.IsSalesReturn)
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

        Public Function UpdateSalesOrderItem(ByVal obj As CommonInfo.SalesOrderGemsItemInfo) As Boolean Implements ISalesOrderDA.UpdateSalesOrderItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SalesOrderGemsItem set  SaleOrderItemID= @SaleOrderItemID , SaleOrderID= @SaleOrderID , GemsCategoryID= @GemsCategoryID , GemsName= @GemsName , GemsTK= @GemsTK , GemsTG= @GemsTG , YOrCOrG= @YOrCOrG , GemsTW= @GemsTW , Qty= @Qty , UnitPrice= @UnitPrice , Type= @Type , Amount= @Amount "
                strCommandText += " where SaleOrderItemID= @SaleOrderItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleOrderItemID", DbType.String, obj.SaleOrderItemID)
                DB.AddInParameter(DBComm, "@SaleOrderID", DbType.String, obj.SaleOrderID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)

                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, obj.GemsK)
                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, obj.GemsP)
                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, obj.GemsY)
                'DB.AddInParameter(DBComm, "@GemsC", DbType.String, obj.GemsC)

                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, obj.GemsTW)

                'DB.AddInParameter(DBComm, "@GemY", DbType.Int32, obj.GemY)
                'DB.AddInParameter(DBComm, "@GemBCG", DbType.String, obj.GemBCG)
                'DB.AddInParameter(DBComm, "@GemP", DbType.String, obj.GemP)

                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, obj.Qty)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Type", DbType.String, obj.Type)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, obj.Amount)

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

        Public Function GetSalesOrderForReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesOrderDA.GetSalesOrderForReport
            Dim strCommandText As String
            Dim strWhereClause As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            If IsReturn = True Then
                strWhereClause = " WHERE H.IsReturn=1 AND H.OrderRetrieveDate BETWEEN @FromDate AND @ToDate " & cristr
            Else
                strWhereClause = " WHERE H.IsReturn=0 And H.OrderDate BETWEEN @FromDate AND @ToDate " & cristr
            End If
            Try
                strCommandText = "SELECT H.IsReturn,H.OrderDate, H.OrderRetrieveDate, H.LocationID, Location, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, S.Staff as Staff, " & _
               " H.ForSaleID, H.ItemCode, H.ItemName, H.Length, SalesRate, GoldPrice, GemsPrice, DesignCharges, TotalPayment, AddOrSub, PaidAmount, AdvanceAmount, DiscountAmount, " & _
               " CAST(GoldTK AS INT) AS GoldK," & _
               " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
               " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
               " CAST(GemsTK AS INT) AS GemsK, " & _
               " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," & _
               " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
               " CAST(WasteTK AS INT) AS WasteK," & _
               " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP," & _
               " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY," & _
               " CAST(TotalTK AS INT) AS TotalK, " & _
               " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP," & _
               " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY, " & _
               " CAST((GoldTK+GemsTK) AS INT) AS TotalNoWasteK, " & _
               " CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT) AS TotalNoWasteP," & _
               " CAST(((((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16)-CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalNoWasteY," & _
               " GoldTK, GemsTK, WasteTK, TotalTK, GoldTK+GemsTK AS TotalNoWasteTK " & _
               " FROM tbl_SalesOrder H " & _
               " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
               " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID " & _
               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID " & strWhereClause
                ''" LEFT JOIN tbl_Counter T ON T.CounterID=H.CounterID " & _

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

        Public Function GetSalesOrderPrint(ByVal SaleOrderID As String) As System.Data.DataTable Implements ISalesOrderDA.GetSalesOrderPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.OrderDate, H.OrderRetrieveDate, H.Customer, H.Address, H.LocationID, Location, H.CounterID, Counter, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, S.Name as Staff,  " & _
                " H.ForSaleID, H.ItemCode, H.ItemName, H.Length, SalesRate, GoldPrice, GemsPrice, DesignCharges, TotalPayment, AddOrSub, PaidAmount, AdvanceAmount, DiscountAmount, L.Remark15, L.RemarkDone,      " & _
                " CAST(GoldTK AS INT) AS GoldK, " & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, " & _
                " CAST(GemsTK AS INT) AS GemsK,  " & _
                " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY, " & _
                " CAST(WasteTK AS INT) AS WasteK, " & _
                " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY, " & _
                " CAST(TotalTK AS INT) AS TotalK,  " & _
                " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP, " & _
                " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY,  " & _
                " CAST((GoldTK+GemsTK) AS INT) AS TotalNoWasteK,  " & _
                " CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT) AS TotalNoWasteP, " & _
                " CAST(((((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16)-CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalNoWasteY, " & _
                " GoldTK, GemsTK, WasteTK, TotalTK, GoldTK+GemsTK AS TotalNoWasteTK  " & _
                " FROM tbl_SalesOrder H  " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                " LEFT JOIN tbl_Counter T ON T.CounterID=H.CounterID  " & _
                " LEFT JOIN tbl_StaffByLocation S ON S.SaleStaffID=H.StaffID  " & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                " WHERE H.SaleOrderID=@SaleOrderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleOrderID", DbType.String, SaleOrderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllSaleOrderFromSearchBox() As System.Data.DataTable Implements ISalesOrderDA.GetAllSaleOrderFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = " select O.SaleOrderID,O.IsReturn as [$IsReturn],convert(varchar(10),O.OrderDate,105) as OrderDate,convert(varchar(10),O.OrderRetrieveDate,105)as OrderRetrieveDate,O.StaffID as [@StaffID],S.Staff as [Staff_],O.Customer as [Customer_],O.ItemCode,O.ItemName as [ItemName_],G.GoldQuality,I.ItemCategory as [ItemCategory_],O.IsSalesReturn as [$IsSalesReturn] "
                'strCommandText += " from tbl_SalesOrder O left join tbl_ItemCategory I on O.ItemCategoryID=I.ItemCategoryID left join tbl_GoldQuality G on O.GoldQualityID=G.GoldQualityID left join tbl_Staff S on O.StaffID=S.StaffID"

                strCommandText = "select O.SaleOrderID,O.IsReturn as [$IsReturn],convert(varchar(10),O.OrderDate,105) as OrderDate," & _
                " convert(varchar(10),O.OrderRetrieveDate,105)as OrderRetrieveDate,O.StaffID as [@StaffID],S.Name as [Name_]," & _
                " O.Customer as [Customer_],O.ItemCode,O.ItemName as [ItemName_],G.GoldQuality,I.ItemCategory as [ItemCategory_]," & _
                " O.Length,O.SalesRate,O.GoldPrice,O.GemsPrice,O.DesignCharges,O.TotalPayment,O.AdvanceAmount,O.PaidAmount,O.IsSalesReturn as [$IsSalesReturn],C.Counter,L.Location," & _
                " CAST(GoldTK AS INT) AS GoldK," & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
                " CAST(GemsTK AS INT) AS GemsK, " & _
                " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," & _
                " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
                " CAST(WasteTK AS INT) AS WasteK," & _
                " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP," & _
                " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY," & _
                " CAST(TotalTK AS INT) AS TotalK," & _
                " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP, " & _
                " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY" & _
                " from tbl_SalesOrder O left join tbl_ItemCategory I on O.ItemCategoryID=I.ItemCategoryID " & _
                " left join tbl_GoldQuality G on O.GoldQualityID=G.GoldQualityID left join tbl_StaffByLocation S on O.StaffID=S.SaleStaffID" & _
                " left join tbl_Counter C on O.CounterID=C.CounterID left join tbl_Location L on O.LocationID=L.LocationID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateSalesOrderReturnToCompile(ByVal Obj As CommonInfo.SalesOrderInfo) As Boolean Implements ISalesOrderDA.UpdateSalesOrderReturnToCompile
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SalesOrder set  OrderDate= @OrderDate , IsReturn= @IsReturn , OrderRetrieveDate= @OrderRetrieveDate , StaffID= @StaffID , Customer= @Customer , Address= @Address , ItemCategoryID= @ItemCategoryID , GoldQualityID= @GoldQualityID ,ForSaleID= @ForSaleID , ItemCode= @ItemCode , ItemName= @ItemName , Length= @Length , SalesRate= @SalesRate , GoldTK= @GoldTK , GoldTG= @GoldTG, GemsTK= @GemsTK , GemsTG= @GemsTG, WasteTK= @WasteTK , WasteTG= @WasteTG, TotalTK= @TotalTK , TotalTG= @TotalTG , TotalPayment= @TotalPayment , AddOrSub= @AddOrSub , AdvanceAmount= @AdvanceAmount, PaidAmount= @PaidAmount , DiscountAmount= @DiscountAmount, GoldPrice= @GoldPrice , GemsPrice= @GemsPrice , DesignCharges= @DesignCharges , Remark= @Remark , LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate=GetDate() , LocationID= @LocationID ,CounterID=@CounterID, IsSalesReturn= @IsSalesReturn "
                strCommandText += " where SaleOrderID= @SaleOrderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleOrderID", DbType.String, Obj.SaleOrderID)
                DB.AddInParameter(DBComm, "@OrderDate", DbType.Date, Obj.OrderDate)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
                DB.AddInParameter(DBComm, "@OrderRetrieveDate", DbType.Date, Obj.OrderRetrieveDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@Customer", DbType.String, Obj.Customer)
                DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, Obj.ItemName)
                DB.AddInParameter(DBComm, "@Length", DbType.String, Obj.Length)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, Obj.SalesRate)

                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
              
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, Obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, Obj.WasteTG)

                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, Obj.TotalTK)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, Obj.TotalTG)
                DB.AddInParameter(DBComm, "@TotalPayment", DbType.Int64, Obj.TotalPayment)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int64, Obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                DB.AddInParameter(DBComm, "@CounterID", DbType.String, Obj.CounterID)
                DB.AddInParameter(DBComm, "@IsSalesReturn", DbType.Int32, Obj.IsSalesReturn)
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

