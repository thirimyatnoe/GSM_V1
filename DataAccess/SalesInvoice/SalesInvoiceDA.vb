'Imports CommonInfo
'Imports Microsoft.Practices.EnterpriseLibrary.Common
'Imports Microsoft.Practices.EnterpriseLibrary.Data
'Imports System.Data.Common
'Namespace SalesInvoice
'    Public Class SalesInvoiceDA
'        Implements ISalesInvoiceDA

'#Region "Private SalesInvoice"

'        Private DB As Database
'        Private Shared ReadOnly _instance As ISalesInvoiceDA = New SalesInvoiceDA

'#End Region

'#Region "Constructors"

'        Private Sub New()
'            DB = DatabaseFactory.CreateDatabase()
'        End Sub

'#End Region

'#Region "Public Properties"

'        Public Shared ReadOnly Property Instance() As ISalesInvoiceDA
'            Get
'                Return _instance
'            End Get
'        End Property

'#End Region

'        Public Function DeleteSalesInvoice(ByVal SaleInvoiceID As String) As Boolean Implements ISalesInvoiceDA.DeleteSalesInvoice
'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "DELETE FROM tbl_SaleInvoice WHERE  SaleInvoiceID= @SaleInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SaleInvoiceID)
'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox("Cannot Delete ", MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function DeleteSalesInvoiceItem(ByVal SaleInvoiceItemID As String) As Boolean Implements ISalesInvoiceDA.DeleteSalesInvoiceItem

'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "DELETE FROM tbl_SaleInvoiceGemsItem WHERE  SaleInvoiceItemID= @SaleInvoiceItemID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceItemID", DbType.String, SaleInvoiceItemID)
'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox("Cannot Delete ", MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function GetAllSalesInvoice() As System.Data.DataTable Implements ISalesInvoiceDA.GetAllSalesInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " select convert(varchar(10),SDate,105) as SaleDate,SaleInvoiceID,IsSalesReturn as [$IsSalesReturn],tbl_SaleInvoice.StaffID as [@StaffID],S.Staff as [Name_],tbl_SaleInvoice.CustomerID,C.CustomerName as [Customer_],ItemCode,ItemName as [ItemName_],Length,tbl_SaleInvoice.ItemCategoryID as [@ItemCategoryID],I.ItemCategory as [ItemCategory_],tbl_SaleInvoice.GoldQualityID as [@GoldQualityID],G.GoldQuality,SalesRate,GoldPrice,GemsPrice,DesignCharges,tbl_SaleInvoice.Remark as [Remark_] from tbl_SaleInvoice left join tbl_ItemCategory I on tbl_SaleInvoice.ItemCategoryID=I.ItemCategoryID left join tbl_GoldQuality G on tbl_SaleInvoice.GoldQualityID=G.GoldQualityID left join tbl_Staff S on tbl_SaleInvoice.StaffID=S.StaffID left join tbl_Customer C on tbl_SaleInvoice.CustomerID=C.CustomerID order by SDate "

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function
'        Public Function GetSalesInvoice(ByVal SaleInvoiceID As String) As CommonInfo.SalesInvoiceDetailInfo Implements ISalesInvoiceDA.GetSalesInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim drResult As IDataReader
'            Dim objSalesInvoice As New SalesInvoiceDetailInfo
'            Try
'                strCommandText = " SELECT  SaleInvoiceID,SDate,StaffID,CustomerID,ForSaleID,ItemCode,ItemCategoryID,GoldQualityID,ItemName,Length,Width,SalesRate,GoldTK,GoldTG,GemsTK,GemsTG,WasteTK,WasteTG,TotalTK,TotalTG,TotalPayment,AddOrSub,DiscountAmount,PaidAmount,GoldPrice,GemsPrice,PlatingCharges,MountingCharges,WhiteCharges,DesignCharges,Remark,  "
'                strCommandText += " CAST(GoldTK AS INT) AS GoldK,"
'                strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
'                strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
'                strCommandText += " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC,"
'                strCommandText += " GoldTK, GoldTG,"
'                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
'                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
'                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
'                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8)-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC,"
'                strCommandText += " GemsTK,GemsTG,"
'                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
'                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
'                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS INT) AS WasteY,"
'                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8)-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS WasteC,"
'                strCommandText += " WasteTK,WasteTG,"
'                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
'                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
'                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*8 AS INT) AS ItemY,"
'                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*8)-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS ItemC,"
'                strCommandText += " ItemTK,ItemTG,"
'                strCommandText += " CAST(TotalTK AS INT) AS TotalK,"
'                strCommandText += " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP,"
'                strCommandText += " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT) AS TotalY,"
'                strCommandText += " CAST(((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8)-CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC,TotalTK,TotalTG "
'                strCommandText += "  FROM tbl_SaleInvoice WHERE SaleInvoiceID= @SaleInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SaleInvoiceID)
'                drResult = DB.ExecuteReader(DBComm)
'                If drResult.Read() Then
'                    With objSalesInvoice
'                        '.CounterID = drResult("CounterID")
'                        .ForSaleID = drResult("ForSaleID")
'                        .SaleInvoiceDetailID = drResult("SaleInvoiceID")
'                        ' = drResult("SDate")
'                        '.StaffID = drResult("StaffID")
'                        '.CustomerID = drResult("CustomerID")
'                        .ItemCode = drResult("ItemCode")
'                        .ItemName = drResult("ItemName")
'                        .Length = IIf(IsDBNull(drResult("Length")), "-", drResult("Length"))
'                        .Width = IIf(IsDBNull(drResult("Width")), "-", drResult("Width"))
'                        .ItemCategoryID = drResult("ItemCategoryID")
'                        .GoldQualityID = drResult("GoldQualityID")
'                        .SalesRate = drResult("SalesRate")

'                        .ItemTG = drResult("ItemTG")
'                        .ItemTK = drResult("ItemTK")
'                        .ItemK = drResult("ItemK")
'                        .ItemP = drResult("ItemP")
'                        .ItemY = drResult("ItemY")
'                        .ItemC = Math.Round(drResult("ItemC"), 1)

'                        .GoldTG = drResult("GoldTG")
'                        .GoldTK = drResult("GoldTK")
'                        .GoldK = drResult("GoldK")
'                        .GoldP = drResult("GoldP")
'                        '.GoldY = Format(drResult("GoldY") + drResult("GoldC"), "0.0")
'                        .GoldY = drResult("GoldY")
'                        .GoldC = Format(drResult("GoldC"), "0.0")

'                        .GemsTG = drResult("GemsTG")
'                        .GemsTK = drResult("GemsTK")
'                        .GemsK = drResult("GemsK")
'                        .GemsP = drResult("GemsP")
'                        .GemsY = drResult("GemsY")
'                        .GemsC = Format(drResult("GemsC"), "0.0")

'                        .WasteK = drResult("WasteK")
'                        .WasteP = drResult("WasteP")
'                        .WasteY = drResult("WasteY")
'                        .WasteC = Format(drResult("WasteC"), "0.0")

'                        .TotalTG = drResult("TotalTG")
'                        .TotalTK = drResult("TotalTK")
'                        .TotalK = drResult("TotalK")
'                        .TotalP = drResult("TotalP")
'                        .TotalY = drResult("TotalY")
'                        .TotalC = Format(drResult("TotalC"), "0.0")

'                        .TotalPayment = drResult("TotalPayment")
'                        .AddOrSub = drResult("AddOrSub")
'                        .DiscountAmount = drResult("DiscountAmount")
'                        .PaidAmount = drResult("PaidAmount")
'                        .GoldPrice = drResult("GoldPrice")
'                        .GemsPrice = drResult("GemsPrice")
'                        .DesignCharges = drResult("DesignCharges")
'                        .MountingCharges = drResult("MountingCharges")
'                        .PlatingCharges = drResult("PlatingCharges")
'                        .WhiteCharges = drResult("WhiteCharges")
'                        .Remark = IIf(IsDBNull(drResult("Remark")), "-", drResult("Remark"))
'                        '.GemsTK = drResult("GemsTK")
'                        '.GemsTG = drResult("GemsTG")
'                        '.GoldTK = drResult("GoldTK")
'                        '.GoldTG = drResult("GoldTG")
'                        '.TotalTK = drResult("TotalTK")
'                        '.TotalTG = drResult("TotalTG")
'                        .WasteTK = drResult("WasteTK")
'                        .WasteTG = drResult("WasteTG")
'                    End With
'                End If
'                drResult.Close()
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'            End Try
'            Return objSalesInvoice
'        End Function

'        Public Function GetSalesInvoiceItem(ByVal SaleInvoiceID As String) As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoiceItem

'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = "Select SaleInvoiceItemID,SaleInvoiceID,GemsCategoryID as [@GemsCategoryID],GemsName,GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,Type,UnitPrice,Amount ,GemsTK ,GemsRemark "
'                strCommandText += " from tbl_SaleInvoiceGemsItem Where SaleInvoiceID ='" & SaleInvoiceID & "' Order By SaleInvoiceItemID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try

'        End Function

'        Public Function InsertSalesInvoice(ByVal SalesInvoiceObj As CommonInfo.SalesInvoiceDetailInfo) As Boolean Implements ISalesInvoiceDA.InsertSalesInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Insert into tbl_SaleInvoice ( SaleInvoiceID,SDate,StaffID,CustomerID,ForSaleID,ItemCode,ItemName,Length,Width,SalesRate,GoldTK,GoldTG,GemsTK,GemsTG,WasteTK,WasteTG,TotalTK,TotalTG,TotalPayment,AddOrSub,DiscountAmount,PaidAmount,GoldPrice,GemsPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,Remark,LastModifiedLoginUserName,LastModifiedDate,LocationID,ItemCategoryID,GoldQualityID,IsSalesReturn,DoneRate,ItemTG,ItemTK)"
'                strCommandText += " Values (@SaleInvoiceID,@SDate,@StaffID,@CustomerID,@ForSaleID,@ItemCode,@ItemName,@Length,@Width,@SalesRate,@GoldTK,@GoldTG,@GemsTK,@GemsTG,@WasteTK,@WasteTG,@TotalTK,@TotalTG,@TotalPayment,@AddOrSub,@DiscountAmount,@PaidAmount,@GoldPrice,@GemsPrice,@DesignCharges,@PlatingCharges,@MountingCharges,@WhiteCharges,@Remark,@LastModifiedLoginUserName,@LastModifiedDate,@LocationID,@ItemCategoryID,@GoldQualityID,@IsSalesReturn,@DoneRate,@ItemTG,@ItemTK)"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                'DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceObj.SaleInvoiceID)
'                'DB.AddInParameter(DBComm, "@SDate", DbType.Date, SalesInvoiceObj.SDate)
'                'DB.AddInParameter(DBComm, "@StaffID", DbType.String, SalesInvoiceObj.StaffID)
'                'DB.AddInParameter(DBComm, "@CustomerID", DbType.String, SalesInvoiceObj.CustomerID)
'                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, SalesInvoiceObj.ForSaleID)
'                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, SalesInvoiceObj.ItemCode)
'                DB.AddInParameter(DBComm, "@ItemName", DbType.String, SalesInvoiceObj.ItemName)
'                DB.AddInParameter(DBComm, "@Length", DbType.String, SalesInvoiceObj.Length)
'                DB.AddInParameter(DBComm, "@Width", DbType.String, SalesInvoiceObj.Width)
'                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, SalesInvoiceObj.SalesRate)
'                'DB.AddInParameter(DBComm, "@GoldK", DbType.Int32, SalesInvoiceObj.GoldK)
'                'DB.AddInParameter(DBComm, "@GoldP", DbType.Int32, SalesInvoiceObj.GoldP)
'                'DB.AddInParameter(DBComm, "@GoldY", DbType.Int32, SalesInvoiceObj.GoldY)
'                'DB.AddInParameter(DBComm, "@GoldC", DbType.Decimal, SalesInvoiceObj.GoldC)
'                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, SalesInvoiceObj.GoldTK)
'                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, SalesInvoiceObj.GoldTG)
'                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, SalesInvoiceObj.GemsK)
'                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, SalesInvoiceObj.GemsP)
'                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, SalesInvoiceObj.GemsY)
'                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, SalesInvoiceObj.GemsC)
'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, SalesInvoiceObj.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, SalesInvoiceObj.GemsTG)
'                'DB.AddInParameter(DBComm, "@WasteK", DbType.Int32, SalesInvoiceObj.WasteK)
'                'DB.AddInParameter(DBComm, "@WasteP", DbType.Int32, SalesInvoiceObj.WasteP)
'                'DB.AddInParameter(DBComm, "@WasteY", DbType.Int32, SalesInvoiceObj.WasteY)
'                'DB.AddInParameter(DBComm, "@WasteC", DbType.Decimal, SalesInvoiceObj.WasteC)
'                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, SalesInvoiceObj.WasteTK)
'                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, SalesInvoiceObj.WasteTG)
'                'DB.AddInParameter(DBComm, "@TotalK", DbType.Int32, SalesInvoiceObj.TotalK)
'                'DB.AddInParameter(DBComm, "@TotalP", DbType.Int32, SalesInvoiceObj.TotalP)
'                'DB.AddInParameter(DBComm, "@TotalY", DbType.Int32, SalesInvoiceObj.TotalY)
'                'DB.AddInParameter(DBComm, "@TotalC", DbType.Decimal, SalesInvoiceObj.TotalC)
'                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, SalesInvoiceObj.TotalTK)
'                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, SalesInvoiceObj.TotalTG)
'                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, SalesInvoiceObj.ItemTK)
'                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, SalesInvoiceObj.ItemTG)
'                DB.AddInParameter(DBComm, "@TotalPayment", DbType.Int64, SalesInvoiceObj.TotalPayment)
'                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, SalesInvoiceObj.AddOrSub)
'                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, SalesInvoiceObj.DiscountAmount)
'                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, SalesInvoiceObj.PaidAmount)
'                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, SalesInvoiceObj.GoldPrice)
'                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, SalesInvoiceObj.GemsPrice)
'                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, SalesInvoiceObj.DesignCharges)
'                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, SalesInvoiceObj.PlatingCharges)
'                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, SalesInvoiceObj.MountingCharges)
'                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, SalesInvoiceObj.WhiteCharges)
'                DB.AddInParameter(DBComm, "@Remark", DbType.String, SalesInvoiceObj.Remark)
'                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
'                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
'                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
'                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, SalesInvoiceObj.ItemCategoryID)
'                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, SalesInvoiceObj.GoldQualityID)
'                DB.AddInParameter(DBComm, "@IsSalesReturn", DbType.Int32, SalesInvoiceObj.IsSalesReturn)
'                'DB.AddInParameter(DBComm, "@CounterID", DbType.String, SalesInvoiceObj.CounterID)
'                DB.AddInParameter(DBComm, "@DoneRate", DbType.Int64, SalesInvoiceObj.DoneRate)


'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function InsertSalesInvoiceItem(ByVal ObjSalesInvoiceItem As CommonInfo.SaleInvoiceGemsItemInfo) As Boolean Implements ISalesInvoiceDA.InsertSalesInvoiceItem
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "INSERT INTO tbl_SaleInvoiceGemsItem ( SaleInvoiceItemID,SaleInvoiceID,GemsCategoryID,GemsName,YOrCOrG,GemsTW,GemsTK,GemsTG,Qty,UnitPrice,Type,Amount,GemsRemark)"
'                strCommandText += " VALUES(@SaleInvoiceItemID,@SaleInvoiceID,@GemsCategoryID,@GemsName,@YOrCOrG,@GemsTW,@GemsTK,@GemsTG,@Qty,@UnitPrice,@Type,@Amount,@GemsRemark)"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceItemID", DbType.String, ObjSalesInvoiceItem.SaleInvoiceItemID)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, ObjSalesInvoiceItem.SaleInvoiceID)
'                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, ObjSalesInvoiceItem.GemsCategoryID)
'                DB.AddInParameter(DBComm, "@GemsName", DbType.String, ObjSalesInvoiceItem.GemsName)
'                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int16, ObjSalesInvoiceItem.GemsK)
'                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int16, ObjSalesInvoiceItem.GemsP)
'                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int16, ObjSalesInvoiceItem.GemsY)
'                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, ObjSalesInvoiceItem.GemsC)
'                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjSalesInvoiceItem.YOrCOrG)
'                'DB.AddInParameter(DBComm, "@GemY", DbType.Int16, ObjSalesInvoiceItem.GemY)
'                'DB.AddInParameter(DBComm, "@GemBCG", DbType.Decimal, ObjSalesInvoiceItem.GemBCG)
'                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, ObjSalesInvoiceItem.GemTW)
'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjSalesInvoiceItem.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjSalesInvoiceItem.GemsTG)
'                DB.AddInParameter(DBComm, "@Qty", DbType.Int16, ObjSalesInvoiceItem.Qty)
'                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, ObjSalesInvoiceItem.UnitPrice)
'                DB.AddInParameter(DBComm, "@Type", DbType.String, ObjSalesInvoiceItem.Type)

'                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, ObjSalesInvoiceItem.Amount)
'                DB.AddInParameter(DBComm, "@GemsRemark", DbType.String, ObjSalesInvoiceItem.GemsRemark)
'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function UpdateSalesInvoice(ByVal SalesInvoiceObj As CommonInfo.SalesInvoiceDetailInfo) As Boolean Implements ISalesInvoiceDA.UpdateSalesInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Update tbl_SaleInvoice set   SDate= @SDate , StaffID= @StaffID , CustomerID= @CustomerID ,ForSaleID=@ForSaleID, ItemCode= @ItemCode , ItemName= @ItemName , Length= @Length , SalesRate= @SalesRate , GoldTK= @GoldTK , GoldTG= @GoldTG , GemsTK= @GemsTK , GemsTG= @GemsTG ,WasteTK= @WasteTK , WasteTG= @WasteTG ,TotalTK= @TotalTK , TotalTG= @TotalTG , TotalPayment= @TotalPayment , AddOrSub= @AddOrSub , DiscountAmount= @DiscountAmount, PaidAmount= @PaidAmount , GoldPrice= @GoldPrice , GemsPrice= @GemsPrice , DesignCharges= @DesignCharges , PlatingCharges= @PlatingCharges , MountingCharges= @MountingCharges , WhiteCharges= @WhiteCharges ,Remark= @Remark , LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate, ItemCategoryID= @ItemCategoryID , GoldQualityID= @GoldQualityID , IsSalesReturn=@IsSalesReturn ,ItemTK=@ItemTK,ItemTG=@ItemTG "
'                strCommandText += " where SaleInvoiceID= @SaleInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                'DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceObj.SaleInvoiceID)
'                'DB.AddInParameter(DBComm, "@SDate", DbType.Date, SalesInvoiceObj.SDate)
'                'DB.AddInParameter(DBComm, "@StaffID", DbType.String, SalesInvoiceObj.StaffID)
'                'DB.AddInParameter(DBComm, "@CustomerID", DbType.String, SalesInvoiceObj.CustomerID)

'                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, SalesInvoiceObj.ForSaleID)
'                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, SalesInvoiceObj.ItemCode)
'                DB.AddInParameter(DBComm, "@ItemName", DbType.String, SalesInvoiceObj.ItemName)
'                DB.AddInParameter(DBComm, "@Length", DbType.String, SalesInvoiceObj.Length)
'                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, SalesInvoiceObj.SalesRate)
'                'DB.AddInParameter(DBComm, "@GoldK", DbType.Int32, SalesInvoiceObj.GoldK)
'                'DB.AddInParameter(DBComm, "@GoldP", DbType.Int32, SalesInvoiceObj.GoldP)
'                'DB.AddInParameter(DBComm, "@GoldY", DbType.Int32, SalesInvoiceObj.GoldY)
'                'DB.AddInParameter(DBComm, "@GoldC", DbType.Decimal, SalesInvoiceObj.GoldC)
'                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, SalesInvoiceObj.GoldTK)
'                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, SalesInvoiceObj.GoldTG)
'                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, SalesInvoiceObj.GemsK)
'                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, SalesInvoiceObj.GemsP)
'                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, SalesInvoiceObj.GemsY)
'                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, SalesInvoiceObj.GemsC)
'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, SalesInvoiceObj.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, SalesInvoiceObj.GemsTG)
'                'DB.AddInParameter(DBComm, "@WasteK", DbType.Int32, SalesInvoiceObj.WasteK)
'                'DB.AddInParameter(DBComm, "@WasteP", DbType.Int32, SalesInvoiceObj.WasteP)
'                'DB.AddInParameter(DBComm, "@WasteY", DbType.Int32, SalesInvoiceObj.WasteY)
'                'DB.AddInParameter(DBComm, "@WasteC", DbType.Decimal, SalesInvoiceObj.WasteC)
'                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, SalesInvoiceObj.WasteTK)
'                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, SalesInvoiceObj.WasteTG)
'                'DB.AddInParameter(DBComm, "@TotalK", DbType.Int32, SalesInvoiceObj.TotalK)
'                'DB.AddInParameter(DBComm, "@TotalP", DbType.Int32, SalesInvoiceObj.TotalP)
'                'DB.AddInParameter(DBComm, "@TotalY", DbType.Int32, SalesInvoiceObj.TotalY)
'                'DB.AddInParameter(DBComm, "@TotalC", DbType.Decimal, SalesInvoiceObj.TotalC)
'                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, SalesInvoiceObj.TotalTK)
'                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, SalesInvoiceObj.TotalTG)
'                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, SalesInvoiceObj.ItemTK)
'                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, SalesInvoiceObj.ItemTG)
'                DB.AddInParameter(DBComm, "@TotalPayment", DbType.Int64, SalesInvoiceObj.TotalPayment)
'                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, SalesInvoiceObj.AddOrSub)
'                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, SalesInvoiceObj.DiscountAmount)
'                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, SalesInvoiceObj.PaidAmount)
'                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, SalesInvoiceObj.GoldPrice)
'                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, SalesInvoiceObj.GemsPrice)
'                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, SalesInvoiceObj.DesignCharges)
'                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, SalesInvoiceObj.PlatingCharges)
'                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, SalesInvoiceObj.MountingCharges)
'                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, SalesInvoiceObj.WhiteCharges)
'                DB.AddInParameter(DBComm, "@Remark", DbType.String, SalesInvoiceObj.Remark)
'                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
'                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
'                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
'                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, SalesInvoiceObj.ItemCategoryID)
'                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, SalesInvoiceObj.GoldQualityID)
'                DB.AddInParameter(DBComm, "@IsSalesReturn", DbType.Int32, SalesInvoiceObj.IsSalesReturn)
'                DB.AddInParameter(DBComm, "@DoneRate", DbType.Int64, SalesInvoiceObj.DoneRate)
'                'DB.AddInParameter(DBComm, "@CounterID", DbType.String, SalesInvoiceObj.CounterID)

'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function UpdateSalesInvoiceItem(ByVal ObjSalesInvoiceItem As CommonInfo.SaleInvoiceGemsItemInfo) As Boolean Implements ISalesInvoiceDA.UpdateSalesInvoiceItem
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Update tbl_SaleInvoiceGemsItem set SaleInvoiceID= @SaleInvoiceID , GemsCategoryID= @GemsCategoryID , GemsName= @GemsName , GemsTK= @GemsTK , GemsTG= @GemsTG , YOrCOrG= @YOrCOrG , GemsTW= @GemsTW ,  Qty= @Qty ,  Type= @Type , UnitPrice= @UnitPrice , Amount= @Amount ,GemsRemark=@GemsRemark "
'                strCommandText += " where SaleInvoiceItemID= @SaleInvoiceItemID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceItemID", DbType.String, ObjSalesInvoiceItem.SaleInvoiceItemID)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, ObjSalesInvoiceItem.SaleInvoiceID)
'                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, ObjSalesInvoiceItem.GemsCategoryID)
'                DB.AddInParameter(DBComm, "@GemsName", DbType.String, ObjSalesInvoiceItem.GemsName)

'                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjSalesInvoiceItem.YOrCOrG)

'                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, ObjSalesInvoiceItem.GemTW)
'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjSalesInvoiceItem.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjSalesInvoiceItem.GemsTG)

'                DB.AddInParameter(DBComm, "@Qty", DbType.Int16, ObjSalesInvoiceItem.Qty)
'                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, ObjSalesInvoiceItem.UnitPrice)
'                DB.AddInParameter(DBComm, "@Type", DbType.String, ObjSalesInvoiceItem.Type)
'                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, ObjSalesInvoiceItem.Amount)
'                DB.AddInParameter(DBComm, "@GemsRemark", DbType.String, ObjSalesInvoiceItem.GemsRemark)
'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function
'        Public Function GetSalesInvoicePrint(ByVal SaleInvoiceID As String) As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoicePrint
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " SELECT H.SaleInvoiceID, SDate, S.Staff as Staff, C.CustomerName as Customer,C.CustomerAddress, C.CustomerTel, H.ForSaleID, H.ItemCode, H.ItemName, H.Length, SalesRate, GoldPrice, GemsPrice,H.DesignCharges, H.Remark, TotalPayment+AddOrSub as TotalPayment, AddOrSub, PaidAmount, DiscountAmount,(TotalPayment+AddOrSub-DiscountAmount-PaidAmount) as BalanceAmount, I.ItemCategory, G.GoldQuality,   H.GoldTG,  " & _
'                                 "  H.TotalTG,GC.GemsCategory,SG.Qty as GemsQty,SG.YOrCOrG ,SG.GemsName, F.Photo,F.DImage,H.PurchaseHeaderID,H.PurchaseAmount,H.AllAdvanceAmount, " & _
'                                 " FROM tbl_SaleInvoice H left join tbl_SaleInvoiceGemsItem SG on H.SaleInvoiceID=SG.SaleInvoiceID left Join tbl_GemsCategory GC on SG.GemsCategoryID=GC.GemsCategoryID  " & _
'                                 " left join tbl_ForSale F on H.ItemCode=F.ItemCode LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=H.ItemCategoryID    " & _
'                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  LEFT JOIN tbl_Customer C ON H.CustomerID=C.CustomerID   " & _
'                                 " WHERE H.SaleInvoiceID= @SaleInvoiceID "

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SaleInvoiceID)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function GetSalesInvoicePrint2(ByVal SaleInvoiceID As String, Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoicePrint2
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " Select tbl_SaleInvoice.SaleInvoiceID as [@SaleInvoiceID],convert(varchar(10),SDate,105) as SDate,tbl_SaleInvoice.StaffID as [@StaffID],S.Staff,Customer,"
'                strCommandText += " tbl_SaleInvoice.Address,ItemCode,ItemName,Length,tbl_SaleInvoice.GoldQualityID,G.GoldQuality,SalesRate,SI.GemsName, "
'                strCommandText += " CAST(GoldTK AS INT) AS GoldK,  "
'                strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
'                strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
'                strCommandText += " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
'                strCommandText += " CAST(tbl_SaleInvoice.GemsTK AS INT) AS TotalGemsK,"
'                strCommandText += " CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT) AS TotalGemsP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT) AS TotalGemsY,"
'                strCommandText += " CAST(((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalGemsC, "
'                strCommandText += " CAST(WasteTK AS INT) AS WasteK,  "
'                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
'                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS INT) AS WasteY,"
'                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8)-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS WasteC, "
'                strCommandText += " CAST(TotalTK AS INT) AS TotalK, "
'                strCommandText += " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP,"
'                strCommandText += " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT) AS TotalY,"
'                strCommandText += " CAST(((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8)-CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC, "
'                strCommandText += " CAST(SI.GemsTK AS INT) AS GemsK,"
'                strCommandText += " CAST((SI.GemsTK-CAST(SI.GemsTK AS INT))*16 AS INT) AS GemsP,"
'                strCommandText += " CAST((((SI.GemsTK-CAST(SI.GemsTK AS INT))*16)-CAST((SI.GemsTK-CAST(SI.GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
'                strCommandText += " CAST(((((SI.GemsTK-CAST(SI.GemsTK AS INT))*16)-CAST((SI.GemsTK-CAST(SI.GemsTK AS INT))*16 AS INT))*8)-CAST((((SI.GemsTK-CAST(SI.GemsTK AS INT))*16)-CAST((SI.GemsTK-CAST(SI.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC, "
'                strCommandText += " SI.YOrCOrG,SI.Qty,TotalPayment,AddOrSub,PaidAmount,Remark,L.Remark1,L.Remark2 from tbl_SaleInvoice Left join tbl_SaleInvoiceGemsItem SI on tbl_SaleInvoice.SaleInvoiceID=SI.SaleInvoiceID left join tbl_Location L on tbl_SaleInvoice.LocationID=L.LocationID  Left join tbl_Staff S on tbl_SaleInvoice.StaffID=S.StaffID left join tbl_GoldQuality G on tbl_SaleInvoice.GoldQualityID=G.GoldQualityID "
'                strCommandText += " where  tbl_SaleInvoice.SaleInvoiceID='" & SaleInvoiceID & "' and tbl_SaleInvoice.LocationID='" & LocationID & "'"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function SetSalesInvoiceToReturn(ByVal SalesInvoiceID As String, Optional ByVal IsReturn As Integer = 0) As Boolean Implements ISalesInvoiceDA.SetSalesInvoiceToReturn
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Update tbl_SaleInvoice set IsSalesReturn=@IsReturn "
'                strCommandText += " where SaleInvoiceID= @SaleInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceID)
'                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, IsReturn)
'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function GetItemCodeBySaleInvoice(ByVal SalesInvoiceID As String) As String Implements ISalesInvoiceDA.GetItemCodeBySaleInvoice
'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "SELECT ItemCode FROM tbl_SaleInvoice WHERE  "
'                strCommandText += " SaleInvoiceID = @SaleInvoiceID"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceID)
'                GetItemCodeBySaleInvoice = CStr(DB.ExecuteScalar(DBComm))
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function
'        Public Function GetForSaleIDBySaleInvoice(ByVal SalesInvoiceID As String) As String Implements ISalesInvoiceDA.GetForSaleIDBySaleInvoice
'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "SELECT ForSaleID FROM tbl_SaleInvoice WHERE  "
'                strCommandText += " SaleInvoiceID = @SaleInvoiceID"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceID)
'                GetForSaleIDBySaleInvoice = CStr(DB.ExecuteScalar(DBComm))
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function GetAllSalesInvoiceDetail(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetAllSalesInvoiceDetail
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                'strCommandText = " select L.Location,C.Counter,convert(varchar(10),SDate,105) as SDate,tbl_SaleInvoice.StaffID as [StaffID],S.Staff,Customer,tbl_SaleInvoice.ItemCode,tbl_SaleInvoice.ItemName,tbl_SaleInvoice.Length,tbl_SaleInvoice.ItemCategoryID,I.ItemCategory,tbl_SaleInvoice.GoldQualityID,G.GoldQuality,tbl_SaleInvoice.SalesRate,"
'                'strCommandText += " tbl_SaleInvoice.GoldTK,tbl_SaleInvoice.GoldTG,"
'                'strCommandText += " CAST(tbl_SaleInvoice.GoldTK AS INT) AS GoldK,  "
'                'strCommandText += " CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT) AS GoldP,"
'                'strCommandText += " CAST((((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
'                'strCommandText += " CAST(((((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
'                'strCommandText += "  tbl_SaleInvoice.GemsTK,tbl_SaleInvoice.GemsTG,"
'                'strCommandText += " CAST(tbl_SaleInvoice.GemsTK AS INT) AS GemsK,"
'                'strCommandText += " CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT) AS GemsP,"
'                'strCommandText += " CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
'                'strCommandText += " CAST(((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC, "
'                'strCommandText += " CAST(tbl_SaleInvoice.WasteTK AS INT) AS WasteK,  "
'                'strCommandText += " CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT) AS WasteP,"
'                'strCommandText += " CAST((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8 AS INT) AS WasteY,"
'                'strCommandText += " CAST(((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS WasteC, "
'                'strCommandText += " tbl_SaleInvoice.WasteTK,tbl_SaleInvoice.WasteTG,"
'                'strCommandText += " CAST(tbl_SaleInvoice.TotalTK AS INT) AS TotalK, "
'                'strCommandText += " CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT) AS TotalP,"
'                'strCommandText += " CAST((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8 AS INT) AS TotalY,"
'                'strCommandText += " CAST(((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC, "
'                'strCommandText += " tbl_SaleInvoice.TotalTK, tbl_SaleInvoice.TotalTG ,  "
'                'strCommandText += " tbl_SaleInvoice.TotalPayment,tbl_SaleInvoice.GoldPrice,tbl_SaleInvoice.GemsPrice,tbl_SaleInvoice.DesignCharges,tbl_SaleInvoice.PaidAmount from tbl_SaleInvoice left join tbl_Staff S on tbl_SaleInvoice.StaffID=S.StaffID left join tbl_Location L on tbl_SaleInvoice.LocationID=L.LocationID left join tbl_ForSale FS on tbl_SaleInvoice.ItemCode=FS.ItemCode left join tbl_Counter C on FS.CounterID=C.CounterID   left join tbl_GoldQuality G on tbl_SaleInvoice.GoldQualityID=G.GoldQualityID left join tbl_ItemCategory I on tbl_SaleInvoice.ItemCategoryID=I.ItemCategoryID"
'                'strCommandText += " where 1=1  and  SDate between @FromDate and @ToDate " & GetFilterString & " order by tbl_SaleInvoice.ItemCategoryID, tbl_SaleInvoice.GoldQualityID  "

'                strCommandText = "SELECT H.ForSaleID,H.SDate, H.LocationID, H.Customer,Location,H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, S.Staff as Staff, " & _
'                                 " H.ItemCode, H.ItemName, H.Length, SalesRate, GoldPrice, GemsPrice, DesignCharges, TotalPayment, AddOrSub, PaidAmount, DiscountAmount,    " & _
'                                 " CAST(GoldTK AS INT) AS GoldK," & _
'                                 " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                                 " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS GoldY," & _
'                                 " CAST(GemsTK AS INT) AS GemsK, " & _
'                                 " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                                 " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS GemsY," & _
'                                 " CAST(WasteTK AS INT) AS WasteK," & _
'                                 " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP," & _
'                                 " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS WasteY," & _
'                                 " CAST(TotalTK AS INT) AS TotalK, " & _
'                                 " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                                 " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS TotalY, " & _
'                                 " CAST((GoldTK+GemsTK) AS INT) AS TotalNoWasteK, " & _
'                                 " CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT) AS TotalNoWasteP," & _
'                                 " CAST(((((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16)-CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS TotalNoWasteY," & _
'                                 " GoldTK, GemsTK, WasteTK, TotalTK, GoldTK+GemsTK AS TotalNoWasteTK " & _
'                                 " FROM tbl_SaleInvoice H " & _
'                                 " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
'                                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
'                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID " & _
'                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID " & _
'                                 " WHERE AddOrSub < '0' And H.SDate BETWEEN @FromDate AND @ToDate " & GetFilterString & " Order by H.ItemCode "
'                '" LEFT JOIN tbl_Counter T ON T.CounterID=H.CounterID " & _
'                '" LEFT JOIN tbl_StaffByLocation S ON S.SaleStaffID=H.StaffID " & _

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        'Public Function GetSalesInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoiceReport
'        '    Dim strCommandText As String
'        '    Dim DBComm As DbCommand
'        '    Dim dtResult As DataTable
'        '    Try
'        '        ' ''strCommandText = "SELECT H.ForSaleID,H.SDate,Cus.CustomerName as Customer,H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, S.Staff as Staff, " & _
'        '        ' ''" H.ItemCode, H.ItemName, H.Length, SalesRate, DoneRate, GoldPrice, GemsPrice, DesignCharges, TotalPayment, AddOrSub, PaidAmount, DiscountAmount,    " & _
'        '        ' ''" CAST(GoldTK AS INT) AS GoldK," & _
'        '        ' ''" CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
'        '        ' ''" CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
'        '        ' ''" CAST(GemsTK AS INT) AS GemsK, " & _
'        '        ' ''" CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," & _
'        '        ' ''" CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
'        '        ' ''" CAST(WasteTK AS INT) AS WasteK," & _
'        '        ' ''" CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP," & _
'        '        ' ''" CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY," & _
'        '        ' ''" CAST(TotalTK AS INT) AS TotalK, " & _
'        '        ' ''" CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP," & _
'        '        ' ''" CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY," 
'        '        ' ''" CAST((GoldTK+GemsTK) AS INT) AS TotalNoWasteK, " & _
'        '        ' ''" CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT) AS TotalNoWasteP," & _
'        '        ' ''" CAST(((((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16)-CAST(((GoldTK+GemsTK)-CAST((GoldTK+GemsTK) AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalNoWasteY," & _
'        '        ' ''" GoldTK, GemsTK, WasteTK, TotalTK, GoldTK+GemsTK AS TotalNoWasteTK , GoldTG, GemsTG, WasteTG, TotalTG, GoldTG+GemsTG AS TotalNoWasteTG " & _
'        '        ' ''" FROM tbl_SaleInvoice H " & _
'        '        ' ''"  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID " & _
'        '        ' ''" LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
'        '        ' ''" LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID " & _
'        '        ' ''" LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID " & _
'        '        ' ''" WHERE H.SDate BETWEEN @FromDate AND @ToDate " & criStr & " Order by H.ItemCode "

'        '        strCommandText = " SELECT G.SalesInvoiceGemItemID, G.GemsCategoryID, GC.GemsCategory, G.GemsName," & _
'        '                         " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
'        '                         " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY,G.GemsTK, G.GemsTG," & _
'        '                         " G.YOrCOrG, G.GemsTW, G.Qty , G.Type, G.UnitPrice, G.Amount As GemAmount, G.GemsRemark,  " & _
'        '                         " D.SaleInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID, " & _
'        '                         " GQ.GoldQuality, F.ItemCategoryID, C.ItemCategory, F.Width, F.FixPrice, F.DesignCharges, F.PlatingCharges, F.MountingCharges, " & _
'        '                         " F.WhiteCharges, F.Photo, D.SalesRate," & _
'        '                         " D.GoldPrice, D.GemsPrice, D.IsFixPrice,D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount," & _
'        '                         " D.ItemTK, D.ItemTG, D.GemsTK As TotalGemsTK, D.GemsTG AS TotalGemsTG, D.WasteTK, D.WasteTG, D.ItemTK+D.WasteTK As TotalTK, D.ItemTG+D.WasteTG As TotalTG, D.ItemTK-D.GemsTK As GoldTK, D.ItemTG-D.GemsTG AS GoldTG, " & _
'        '                         "  CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
'        '                         " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS ItemY, " & _
'        '                         " CAST(D.GemsTK AS INT) AS TotalGemsK,  CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS TotalGemsP, " & _
'        '                         " CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalGemsY, " & _
'        '                         " CAST(D.WasteTK AS INT) AS WasteK,  CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
'        '                         " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY," & _
'        '                         " CAST(D.ItemTK+D.WasteTK AS INT) AS TotalK,  CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT) AS TotalP, " & _
'        '                         " CAST((((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16)-CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY," & _
'        '                         " CAST(D.ItemTK-D.GemsTK AS INT) AS GoldK,  CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT) AS GoldP, " & _
'        '                         " CAST((((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16)-CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
'        '                         " H.SaleInvoiceHeaderID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,S.StaffID, S.Staff, H.Remark, H.TotalAmount,H.AddOrSub,  " & _
'        '                         " (H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, ((H.TotalAmount+H.AddOrSub)*PromotionDiscount)/100 As PromotionAmount," & _
'        '                         " H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub)-((((H.TotalAmount+H.AddOrSub)*PromotionDiscount)/100)+H.DiscountAmount) )-H.PaidAmount) As BalanceAmount" & _
'        '                         " FROM tbl_SaleInvoiceDetail D LEFT JOIN tbl_SalesInvoiceGemItem G ON G.SaleInvoiceDetailID=D.SaleInvoiceDetailiD" & _
'        '                         " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
'        '                         " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
'        '                         " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID" & _
'        '                         " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
'        '                         " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
'        '                         "  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
'        '                         " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
'        '                         " WHERE H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr & " Order by H.SaleInvoiceHeaderID "
'        '        DBComm = DB.GetSqlStringCommand(strCommandText)
'        '        DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'        '        DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'        '        Return dtResult
'        '    Catch ex As Exception
'        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'        '        Return New DataTable
'        '    End Try
'        'End Function

'        Public Function GetSalesInvoiceForOrderDetail(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoiceForOrderDetail
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " Select tbl_SaleInvoice.SaleInvoiceID,convert(varchar(10),tbl_SaleInvoice.SDate,105) as SDate,tbl_SaleInvoice.Customer,tbl_SaleInvoice.Address,ST.Staff,tbl_SaleInvoice.ItemCode,tbl_SaleInvoice.ItemName,tbl_SaleInvoice.Length,"
'                '  strCommandText += " S.GoldK,S.GoldP,S.GoldY,S.GoldC,S.GoldTK,S.GemsK,S.GemsP,S.GemsY,S.GemsC,S.GemsTK,S.WasteK,S.WasteP, "
'                strCommandText += " tbl_SaleInvoiceS.SalesRate,tbl_SaleInvoice.TotalPayment,tbl_SaleInvoice.AddOrSub, "

'                strCommandText += " tbl_SaleInvoice.GoldTK,tbl_SaleInvoice.GoldTG,"
'                strCommandText += " CAST(tbl_SaleInvoice.GoldTK AS INT) AS GoldK,  "
'                strCommandText += " CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT) AS GoldP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
'                strCommandText += " CAST(((((S.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
'                strCommandText += "  tbl_SaleInvoice.GemsTK,tbl_SaleInvoice.GemsTG,"
'                strCommandText += " CAST(tbl_SaleInvoice.GemsTK AS INT) AS GemsK,"
'                strCommandText += " CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT) AS GemsP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
'                strCommandText += " CAST(((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC, "
'                strCommandText += " CAST(tbl_SaleInvoice.WasteTK AS INT) AS WasteK,  "
'                strCommandText += " CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT) AS WasteP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8 AS INT) AS WasteY,"
'                strCommandText += " CAST(((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS WasteC, "
'                strCommandText += " tbl_SaleInvoice.WasteTK,tbl_SaleInvoice.WasteTG,"
'                strCommandText += " CAST(tbl_SaleInvoice.TotalTK AS INT) AS TotalK, "
'                strCommandText += " CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT) AS TotalP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8 AS INT) AS TotalY,"
'                strCommandText += " CAST(((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC, "
'                strCommandText += " tbl_SaleInvoice.TotalTK, tbl_SaleInvoice.TotalTG , "

'                strCommandText += " tbl_SaleInvoice.PaidAmount,tbl_SaleInvoice.Remark,convert(varchar(10),tbl_SaleInvoice.OrderRetrieveDate,105) as OrderRetrieveDate,I.ItemCategory,G.GoldQuality,tbl_SaleInvoice.LocationID,L.Location,F.CounterID,C.Counter from tbl_SaleInvoice  Left Join tbl_Staff ST On tbl_SaleInvoice.StaffID = ST.StaffID "
'                strCommandText += " Left Join tbl_ForSale F On tbl_SaleInvoice.ItemCode=F.ItemCode Left Join tbl_Counter C On F.CounterID=C.CounterID "
'                strCommandText += " Left Join tbl_ItemCategory I On tbl_SaleInvoice.ItemCategoryID = I.ItemCategoryID Left Join tbl_GoldQuality G "
'                strCommandText += " On tbl_SaleInvoice.GoldQualityID = G.GoldQualityID Left Join tbl_Location L On tbl_SaleInvoice.LocationID=L.LocationID where 1=1 and tbl_SaleInvoice.SDate between '" & FromDate & "' and '" & ToDate & "' " & cristr & " Order by tbl_SaleInvoice.SaleInvoiceID"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)

'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function GetSalesInvoiceForOrderSummary(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoiceForOrderSummary
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " Select tbl_SaleInvoice.SaleInvoiceID,convert(varchar(10),tbl_SaleInvoice.SDate,105) as SDate,tbl_SaleInvoice.Customer,ST.Staff,tbl_SaleInvoice.ItemName,tbl_SaleInvoice.Length,"
'                strCommandText += " tbl_SaleInvoice.GoldTK,tbl_SaleInvoice.GoldTG,"
'                strCommandText += " CAST(tbl_SaleInvoice.GoldTK AS INT) AS GoldK,  "
'                strCommandText += " CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT) AS GoldP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
'                strCommandText += " CAST(((((S.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16)-CAST((tbl_SaleInvoice.GoldTK-CAST(tbl_SaleInvoice.GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
'                strCommandText += "  tbl_SaleInvoice.GemsTK,tbl_SaleInvoice.GemsTG,"
'                strCommandText += " CAST(tbl_SaleInvoice.GemsTK AS INT) AS GemsK,"
'                strCommandText += " CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT) AS GemsP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
'                strCommandText += " CAST(((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16)-CAST((tbl_SaleInvoice.GemsTK-CAST(tbl_SaleInvoice.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC, "
'                strCommandText += " CAST(tbl_SaleInvoice.WasteTK AS INT) AS WasteK,  "
'                strCommandText += " CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT) AS WasteP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8 AS INT) AS WasteY,"
'                strCommandText += " CAST(((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16)-CAST((tbl_SaleInvoice.WasteTK-CAST(tbl_SaleInvoice.WasteTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS WasteC, "
'                strCommandText += " tbl_SaleInvoice.WasteTK,tbl_SaleInvoice.WasteTG,"
'                strCommandText += " CAST(tbl_SaleInvoice.TotalTK AS INT) AS TotalK, "
'                strCommandText += " CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT) AS TotalP,"
'                strCommandText += " CAST((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8 AS INT) AS TotalY,"
'                strCommandText += " CAST(((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8)-CAST((((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16)-CAST((tbl_SaleInvoice.TotalTK-CAST(tbl_SaleInvoice.TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC, "
'                strCommandText += " tbl_SaleInvoice.TotalTK, tbl_SaleInvoice.TotalTG , "
'                strCommandText += " tbl_SaleInvoice.TotalPayment,tbl_SaleInvoice.AddOrSub, "
'                strCommandText += " tbl_SaleInvoice.PaidAmount,tbl_SaleInvoice.Remark,I.ItemCategory,G.GoldQuality from tbl_SaleInvoice  Left Join tbl_Staff ST On tbl_SaleInvoice.StaffID = ST.StaffID "
'                strCommandText += " Left Join tbl_ItemCategory I On tbl_SaleInvoice.ItemCategoryID = I.ItemCategoryID Left Join tbl_GoldQuality G "
'                strCommandText += " On tbl_SaleInvoice.GoldQualityID = G.GoldQualityID where 1=1 and tbl_SaleInvoice.SDate between '" & FromDate & "' and '" & ToDate & "' " & cristr & " Order by tbl_SaleInvoice.SaleInvoiceID"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)

'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function GetSaleInvoiceItemFromPurchaseInvoice(ByVal SalesInvoiceID As String) As System.Data.DataTable Implements ISalesInvoiceDA.GetSaleInvoiceItemFromPurchaseInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = "Select SaleInvoiceItemID as [PurchaseInvoiceGemsItemID],SaleInvoiceID as [PurchaseInvoiceID],GemsCategoryID as [@GemsCategoryID],GemsName,GemsTG,YOrCOrG,GemsTW,Qty,UnitPrice as PurchaseRate,CASE Type WHEN 'Fix' Then 'Fix' WHEN 'ByWeight' Then 'ByWeight' WHEN 'ByQty' Then 'ByQty' end as FixType,Amount ,"
'                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
'                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
'                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
'                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8)-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC "
'                strCommandText += " from tbl_SaleInvoiceGemsItem Where SaleInvoiceID ='" & SalesInvoiceID & "' Order By SaleInvoiceItemID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try

'        End Function

'        Public Function GetAllSaleInvoiceFromSearchBox() As System.Data.DataTable Implements ISalesInvoiceDA.GetAllSaleInvoiceFromSearchBox
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " select SaleInvoiceID,convert(varchar(10),SDate,105) as SaleDate,IsSalesReturn as [$IsSalesReturn],tbl_SaleInvoice.StaffID as [@StaffID],S.Name as [Name_],Customer as [Customer_],tbl_SaleInvoice.Address as [Address_],ItemCode,ItemName as [ItemName_],Length,tbl_SaleInvoice.ItemCategoryID as [@ItemCategory],I.ItemCategory as [ItemCategory_],tbl_SaleInvoice.GoldQualityID as [@GoldQualityID],G.GoldQuality,SalesRate,DoneRate,"
'                strCommandText += " CAST(GoldTK AS INT) AS GoldK,"
'                strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
'                strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS GoldY,"
'                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
'                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
'                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS GemsY,"
'                strCommandText += " CAST(WasteTK AS INT) AS WasteK,  "
'                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
'                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS WasteY,"
'                strCommandText += " CAST(TotalTK AS INT) AS TotalK, "
'                strCommandText += " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP,"
'                strCommandText += " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS TotalY,"
'                strCommandText += " GoldPrice,GemsPrice,DesignCharges,TotalPayment,PaidAmount,Remark as [Remark_],L.Location,C.Counter"
'                strCommandText += " from tbl_SaleInvoice left join tbl_ItemCategory I on tbl_SaleInvoice.ItemCategoryID=I.ItemCategoryID left join tbl_GoldQuality G on tbl_SaleInvoice.GoldQualityID=G.GoldQualityID left join tbl_StaffByLocation S on tbl_SaleInvoice.StaffID=S.SaleStaffID"
'                strCommandText += " Left Join tbl_Location L On tbl_SaleInvoice.LocationID=L.LocationID Left Join tbl_Counter C On tbl_SaleInvoice.CounterID=C.CounterID"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function
'        Public Function GetProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetProfitForSaleItem
'            Dim strCommandText As String = ""
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                Select Case argType
'                    Case "SaleStock"
'                        'strCommandText = "(SELECT H.ForSaleID,H.SDate as SDate, H.LocationID, Location,H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, S.Staff as Staff, " & _
'                        '       " H.ItemCode, H.ItemName, H.Length, GoldPrice, GemsPrice,  TotalPayment, AddOrSub, ((TotalPayment+AddOrSub)-DiscountAmount) as TotalNetAmount,PaidAmount, DiscountAmount,   " & _
'                        '     " Case I.IsOriginalFixedPrice when 1 then I.OriginalFixedPrice when 0 then 0 end as OriginalFixedPrice, " & _
'                        ' " I.IsOriginalFixedPrice,I.OriginalFixedPrice,IsOriginalPriceGram,I.OriginalPriceGram,I.OriginalPriceTK,OriginalGemsPrice,OriginalOtherPrice,0 as PurchasePrice,0 as PurchaseRate, " & _
'                        '       " CASE I.IsFixPrice WHEN 1 THEN 0 WHEN 0 THEN SalesRate END as SalesRate," & _
'                        '       " CAST(H.GoldTK AS INT) AS GoldK," & _
'                        '       " CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                        '       " CAST((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
'                        '       " CAST(H.GemsTK AS INT) AS GemsK, " & _
'                        '       " CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                        '       " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
'                        '       " CAST(H.WasteTK AS INT) AS WasteK," & _
'                        '       " CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT) AS WasteP," & _
'                        '       " CAST((((H.WasteTK-CAST(H.WasteTK AS INT))*16)-CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY," & _
'                        '       " CAST(H.TotalTK AS INT) AS TotalK, " & _
'                        '       " CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                        '       " CAST((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY, " & _
'                        '       " CAST((H.GoldTK+H.GemsTK) AS INT) AS TotalNoWasteK, " & _
'                        '       " CAST(((H.GoldTK+H.GemsTK)-CAST((H.GoldTK+H.GemsTK) AS INT))*16 AS INT) AS TotalNoWasteP," & _
'                        '       " CAST(((((H.GoldTK+H.GemsTK)-CAST((H.GoldTK+H.GemsTK) AS INT))*16)-CAST(((H.GoldTK+H.GemsTK)-CAST((H.GoldTK+H.GemsTK) AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalNoWasteY," & _
'                        '       " H.GoldTK, H.GemsTK, H.WasteTK, H.WasteTG, H.TotalTK, H.TotalTG,H.GoldTG, H.GemsTG,H.GoldTG+H.GemsTG AS TotalNoWasteTG, H.GoldTK+H.GemsTK AS TotalNoWasteTK " & _
'                        '       " FROM tbl_SaleInvoice H " & _
'                        '       " LEFT JOIN tbl_ForSale I ON I.ForSaleID=H.ForSaleID " & _
'                        '       " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
'                        '       " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
'                        '       " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID " & _
'                        '       " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID " & _
'                        '       " WHERE H.SDate BETWEEN @FromDate AND @ToDate " & criStr & ") Order by H.ItemCode "

'                        strCommandText = "(SELECT D.ForSaleID,H.SaleDate, I.LocationID, Location,I.GoldQualityID, GoldQuality, I.ItemCategoryID, " & _
'                                         " ItemCategory, S.Staff as Staff,  D.ItemCode, I.ItemNameID, I.Length, D.GoldPrice, D.GemsPrice,  D.TotalAmount, " & _
'                                         " D.AddOrSub, ((H.TotalAmount+H.AddOrSub)-DiscountAmount) as TotalNetAmount,PaidAmount, DiscountAmount," & _
'                                         " H.PromotionDiscount," & _
'                                         " Case H.PromotionDiscount when 0 Then 0 else ((H.TotalAmount+H.AddOrSub) - (((H.TotalAmount+H.AddOrSub)*(H.PromotionDiscount/100))- DiscountAmount)) End as PromotionAmount," & _
'                                         " Case I.IsOriginalFixedPrice when 1 then I.OriginalFixedPrice when 0 then 0 end as OriginalFixedPrice," & _
'                                         " I.IsOriginalFixedPrice,IsOriginalPriceGram,I.OriginalPriceGram,I.OriginalPriceTK," & _
'                                         " OriginalGemsPrice,OriginalOtherPrice,0 as PurchasePrice,0 as PurchaseRate," & _
'                                         " CASE I.IsFixPrice WHEN 1 THEN 0 WHEN 0 THEN SalesRate END as SalesRate," & _
'                                         " CAST(D.ItemTK-D.GemsTK AS INT) AS GoldK,  CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT) AS GoldP," & _
'                                         " CAST((((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16)-CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, CAST(D.GemsTK AS INT) AS GemsK," & _
'                                         " CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS GemsP, CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY, CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY, CAST(D.ItemTK AS INT) AS ItemK,  CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
'                                         " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS ItemY," & _
'                                         " (D.ItemTK - D.GemsTK) as GoldTK, D.GemsTK, D.WasteTK, D.WasteTG, D.ItemTK,D.ItemTG,(D.ItemTG - D.GemsTG) as GoldTG, D.GemsTG" & _
'                                         " FROM tbl_SaleInvoiceHeader H Left Join tbl_SaleInvoiceDetail D On H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID" & _
'                                         " LEFT JOIN tbl_ForSale I ON I.ForSaleID=D.ForSaleID" & _
'                                         " LEFT JOIN tbl_Location L ON L.LocationID=I.LocationID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
'                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID  LEFT JOIN tbl_ItemCategory C " & _
'                                         " ON C.ItemCategoryID=I.ItemCategoryID  WHERE H.SaleDate BETWEEN @FromDate AND @ToDate and I.IsVolume = '0'  " & criStr & ")" & _
'                                         " Order by D.ItemCode "




'                    Case "BalanceStock"
'                        strCommandText = "SELECT H.ForSaleID,H.GivenDate,H.GoldQualityID, G.GoldQuality, H.ItemCategoryID,ItemCategory, " & _
'                                         " H.ItemCode, H.ItemName, H.Length,H.FixPrice,CASE H.IsFixPrice WHEN 1 THEN 0 WHEN 0 THEN (select SalesRate from tbl_StandardRate where DefineDateTime =(select MAX(DefineDateTime) FROM tbl_StandardRate where H.GoldQualityID=tbl_StandardRate.GoldQualityID ))END as SalesRate," & _
'                                         " H.IsOriginalFixedPrice,H.OriginalFixedPrice,IsOriginalPriceGram,H.OriginalPriceGram,H.OriginalPriceTK,OriginalGemsPrice,OriginalOtherPrice,0 as PurchasePrice,0 as PurchaseRate, " & _
'                                         " CASE H.IsFixPrice WHEN 1 THEN H.FixPrice WHEN 0 THEN (CASE G.IsGramRate WHEN 1 THEN (select SalesRate from tbl_StandardRate where DefineDateTime =(select MAX(DefineDateTime) FROM tbl_StandardRate where H.GoldQualityID=tbl_StandardRate.GoldQualityID ))*H.TotalTG WHEN 0 THEN (select SalesRate from tbl_StandardRate where DefineDateTime =(select MAX(DefineDateTime) FROM tbl_StandardRate where H.GoldQualityID=tbl_StandardRate.GoldQualityID ))*H.TotalTK END) END as SalesPrice," & _
'                                         " CAST(H.GoldTK AS INT) AS GoldK," & _
'                                         " CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                                         " CAST((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
'                                         " CAST(H.GemsTK AS INT) AS GemsK, " & _
'                                         " CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                                         " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
'                                         " CAST(H.WasteTK AS INT) AS WasteK," & _
'                                         " CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT) AS WasteP," & _
'                                         " CAST((((H.WasteTK-CAST(H.WasteTK AS INT))*16)-CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY," & _
'                                         " CAST(H.TotalTK AS INT) AS TotalK, " & _
'                                         " CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                                         " CAST((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY, " & _
'                                         " CAST((H.GoldTK+H.GemsTK) AS INT) AS TotalNoWasteK, " & _
'                                         " CAST(((H.GoldTK+H.GemsTK)-CAST((H.GoldTK+H.GemsTK) AS INT))*16 AS INT) AS TotalNoWasteP," & _
'                                         " CAST(((((H.GoldTK+H.GemsTK)-CAST((H.GoldTK+H.GemsTK) AS INT))*16)-CAST(((H.GoldTK+H.GemsTK)-CAST((H.GoldTK+H.GemsTK) AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalNoWasteY," & _
'                                         " H.GoldTK, H.GemsTK, H.WasteTK, H.WasteTG, H.TotalTK, H.TotalTG,H.GoldTG, H.GemsTG,H.GoldTG+H.GemsTG AS TotalNoWasteTG, H.GoldTK+H.GemsTK AS TotalNoWasteTK " & _
'                                         " ,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges " & _
'                                         " FROM tbl_ForSale H " & _
'                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID " & _
'                                         " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID " & _
'                                         " WHERE H.isExit='0' AND H.GivenDate BETWEEN @FromDate AND @ToDate " & criStr & " Order by H.ItemCode "
'                        '" LEFT JOIN tbl_Counter T ON T.CounterID=H.CounterID " & _
'                        '" LEFT JOIN tbl_StaffByLocation S ON S.SaleStaffID=H.StaffID " & _

'                End Select

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function InsertSalesInvoiceUserID(ByVal SalesInvoiceID As String, ByVal UserID As String) As Boolean Implements ISalesInvoiceDA.InsertSalesInvoiceUserID
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Update tbl_SaleInvoice set   UserID= @UserID "
'                strCommandText += " where SaleInvoiceID= @SaleInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceID)
'                DB.AddInParameter(DBComm, "@UserID", DbType.String, UserID)
'                If DB.ExecuteNonQuery(DBComm) > 0 Then
'                    Return True
'                Else
'                    Return False
'                End If
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        'Public Function GetSaleInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As CommonInfo.SalesInvoiceDetailInfo Implements ISalesInvoiceDA.GetSaleInvoiceDetailForTotal
'        '    Dim strCommandText As String
'        '    Dim DBComm As DbCommand
'        '    Dim drResult As IDataReader
'        '    Dim obj As New SalesInvoiceDetailInfo
'        '    Try

'        '        strCommandText = " select Count(D.SaleInvoiceDetailID) As QTY, Sum(D.ItemTG) AS ItemTG,Sum(D.GemsTG) AS GemsTG, Sum(D.WasteTG) AS WasteTG, Sum(D.ItemTG+D.WasteTG) As TotalTG, Sum(D.ItemTG-D.GemsTG) AS GoldTG, " & _
'        '                        " Sum(D.ItemTK) AS ItemTK,Sum(D.GemsTK) AS GemsTK, Sum(D.WasteTK) AS WasteTK, Sum(D.ItemTK+D.WasteTK) As TotalTK, Sum(D.ItemTK-D.GemsTK) AS GoldTK, " & _
'        '                        " sum(D.TotalAmount+D.AddOrSub) As ItemAmount " & _
'        '                        " FROM tbl_SaleInvoiceDetail D " & _
'        '                        " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
'        '                        " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
'        '                        " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
'        '                        " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
'        '                        " left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
'        '                        " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
'        '                        " WHERE H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr
'        '        DBComm = DB.GetSqlStringCommand(strCommandText)
'        '        DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'        '        DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'        '        drResult = DB.ExecuteReader(DBComm)
'        '        If drResult.Read() Then
'        '            With obj
'        '                .QTY = drResult("QTY")
'        '                .ItemTG = drResult("ItemTG")
'        '                .GoldTG = drResult("GoldTG")
'        '                .WasteTG = drResult("WasteTG")
'        '                .GemsTG = drResult("GemsTG")
'        '                .TotalTG = drResult("TotalTG")
'        '                .ItemTK = drResult("ItemTK")
'        '                .GoldTK = drResult("GoldTK")
'        '                .WasteTK = drResult("WasteTK")
'        '                .GemsTK = drResult("GemsTK")
'        '                .TotalTK = drResult("TotalTK")
'        '                .ItemAmount = drResult("ItemAmount")
'        '            End With
'        '        End If
'        '        drResult.Close()
'        '    Catch ex As Exception
'        '        MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'        '    End Try
'        '    Return obj
'        'End Function

'        'Public Function GetSalesInvoiceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoiceReportForTotal
'        '    Dim strCommandText As String
'        '    Dim DBComm As DbCommand
'        '    Dim dtResult As DataTable
'        '    Try
'        '        strCommandText = " select Distinct(H.SaleInvoiceHeaderID), H.TotalAmount,H.AddOrSub,((H.TotalAmount+H.AddOrSub)-((((H.TotalAmount+H.AddOrSub)*PromotionDiscount)/100)+H.DiscountAmount) )As NetAmount, H.PromotionDiscount, ((H.TotalAmount+H.AddOrSub)*PromotionDiscount)/100 As PromotionAmount," & _
'        '                         " H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub)-((((H.TotalAmount+H.AddOrSub)*PromotionDiscount)/100)+H.DiscountAmount) )-H.PaidAmount) As BalanceAmount " & _
'        '                         " FROM tbl_SaleInvoiceDetail D " & _
'        '                         " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
'        '                         " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
'        '                         " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
'        '                         " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
'        '                         " left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
'        '                         " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
'        '                         " WHERE H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr

'        '        DBComm = DB.GetSqlStringCommand(strCommandText)
'        '        DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'        '        DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'        '        Return dtResult
'        '    Catch ex As Exception
'        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'        '        Return New DataTable
'        '    End Try
'        'End Function

'        'Public Function GetSalesInvoiceReportForSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetSalesInvoiceReportForSummaryReport
'        '    Dim strCommandText As String
'        '    Dim DBComm As DbCommand
'        '    Dim dtResult As DataTable
'        '    Try
'        '        strCommandText = " select  D.ForSaleID, D.ItemCode, H.SaleDate, (D.TotalAmount+D.AddOrSub) As NetAmount,  " & _
'        '                         " Case D.IsFixPrice When 1 Then 0 Else D.SalesRate END As SalesRate, D.ItemTK, D.ItemTG, D.GemsTK, D.GemsTG, D.WasteTK, D.WasteTG, D.GemsTK, D.GemsTG, D.GoldPrice, D.GemsPrice, D.IsFixPrice, D.TotalAmount, D.AddOrSub, F.ItemCategoryID, C.ItemCategory, " & _
'        '                         " D.ItemTK+D.WasteTK As TotalTK, D.ItemTG+D.WasteTG As TotalTG, D.ItemTK-D.GemsTK As GoldTK, D.ItemTG-D.GemsTG as GoldTG, " & _
'        '                         " F.ItemNameID, I.ItemName, F.GoldQualityID, GQ.GoldQuality " & _
'        '                         " FROM tbl_SaleInvoiceDetail D " & _
'        '                         " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
'        '                         " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
'        '                         " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
'        '                         " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
'        '                         "  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
'        '                         " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
'        '                         " WHERE H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr

'        '        DBComm = DB.GetSqlStringCommand(strCommandText)
'        '        DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'        '        DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'        '        Return dtResult
'        '    Catch ex As Exception
'        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'        '        Return New DataTable
'        '    End Try
'        'End Function
'        Public Function GetAllSaleInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceDA.GetAllSaleInvoiceVoucherPrint
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " SELECT H.SaleInvoiceID, SDate, S.Staff as Staff, C.CustomerName as Customer,C.CustomerAddress, H.ForSaleID, H.ItemCode, H.ItemName, H.Length, SalesRate, GoldPrice, GemsPrice,H.DesignCharges, H.Remark, TotalPayment+AddOrSub as TotalPayment, AddOrSub, PaidAmount, DiscountAmount,(TotalPayment+AddOrSub-DiscountAmount-PaidAmount) as BalanceAmount, I.ItemCategory, G.GoldQuality,   H.GoldTG,  " & _
'                                 "  H.TotalTG,GC.GemsCategory,SG.Qty as GemsQty,SG.YOrCOrG ,SG.GemsName, F.Photo,F.DImage  " & _
'                                 " FROM tbl_SaleInvoice H left join tbl_SaleInvoiceGemsItem SG on H.SaleInvoiceID=SG.SaleInvoiceID left Join tbl_GemsCategory GC on SG.GemsCategoryID=GC.GemsCategoryID  " & _
'                                 " left join tbl_ForSale F on H.ItemCode=F.ItemCode LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=H.ItemCategoryID    " & _
'                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  LEFT JOIN tbl_Customer C ON H.CustomerID=C.CustomerID   " & _
'                                 " WHERE H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'    End Class
'        end namespace

