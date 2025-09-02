'Imports CommonInfo
'Imports Microsoft.Practices.EnterpriseLibrary.Common
'Imports Microsoft.Practices.EnterpriseLibrary.Data
'Imports System.Data.Common
'Namespace PurchaseInvoice
'    Public Class PurchaseInvoiceDA
'        Implements IPurchaseInvoiceDA

'#Region "Private PurchaseInvoice"

'        Private DB As Database
'        Private Shared ReadOnly _instance As IPurchaseInvoiceDA = New PurchaseInvoiceDA

'#End Region

'#Region "Constructors"

'        Private Sub New()
'            DB = DatabaseFactory.CreateDatabase()
'        End Sub

'#End Region

'#Region "Public Properties"

'        Public Shared ReadOnly Property Instance() As IPurchaseInvoiceDA
'            Get
'                Return _instance
'            End Get
'        End Property

'#End Region

'        Public Function DeletePurchaseInvoice(ByVal PurchaseInvoiceID As String, Optional ByVal SaleInvoiceID As String = "") As Boolean Implements IPurchaseInvoiceDA.DeletePurchaseInvoice
'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "DELETE FROM tbl_PurchaseInvoice WHERE  PurchaseInvoiceID= @PurchaseInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, PurchaseInvoiceID)
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

'        Public Function InsertPurchaseInvoice(ByVal Obj As CommonInfo.PurchaseInvoiceInfo) As Boolean Implements IPurchaseInvoiceDA.InsertPurchaseInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Insert into tbl_PurchaseInvoice ( PurchaseInvoiceID,PDate,StaffID,CustomerID,Address,ItemCategoryID,ItemName,Length,Qty,GoldQualityID,SubGoldQualityID, PurchaseRate,TotalAmount,AddOrSub,PaidAmount,GoldPrice,GemsPrice,Remark,LastModifiedDate,LastModifiedLoginUserName,LocationID,IsExchange,FromShopOrCustomer,OldSaleInvoiceID,OldSaleAmount,GoldTK,GoldTG,GemsTK,GemsTG,TotalTK,TotalTG,IsDamage)"
'                strCommandText += " Values (@PurchaseInvoiceID,@PDate,@StaffID,@CustomerID,@Address,@ItemCategoryID,@ItemName,@Length,@Qty,@GoldQualityID,@SubGoldQualityID,@PurchaseRate,@TotalAmount,@AddOrSub,@PaidAmount,@GoldPrice,@GemsPrice,@Remark,@LastModifiedDate,@LastModifiedLoginUserName,@LocationID,@IsExchange,@FromShopOrCustomer,@OldSaleInvoiceID,@OldSaleAmount,@GoldTK,@GoldTG,@GemsTK,@GemsTG,@TotalTK,@TotalTG,@IsDamage)"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, Obj.PurchaseInvoiceID)
'                DB.AddInParameter(DBComm, "@PDate", DbType.Date, Obj.PDate)
'                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
'                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
'                DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
'                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
'                DB.AddInParameter(DBComm, "@ItemName", DbType.String, Obj.ItemName)
'                DB.AddInParameter(DBComm, "@Length", DbType.String, Obj.Length)
'                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
'                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
'                DB.AddInParameter(DBComm, "@SubGoldQualityID", DbType.String, Obj.SubGoldQualityID)
'                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int32, Obj.PurchaseRate)
'                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, Obj.TotalAmount)
'                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
'                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, Obj.PaidAmount)
'                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int32, Obj.GoldPrice)
'                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int32, Obj.GemsPrice)
'                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
'                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
'                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
'                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
'                DB.AddInParameter(DBComm, "@IsExchange", DbType.Boolean, Obj.IsExchange)
'                DB.AddInParameter(DBComm, "@FromShopOrCustomer", DbType.Boolean, Obj.FromShopOrCustomer)
'                DB.AddInParameter(DBComm, "@OldSaleInvoiceID", DbType.String, Obj.OldSaleInvoiceID)
'                DB.AddInParameter(DBComm, "@OldSaleAmount", DbType.Int32, Obj.OldSaleAmount)
'                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
'                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
'                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, Obj.TotalTK)
'                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, Obj.TotalTG)
'                DB.AddInParameter(DBComm, "@IsDamage", DbType.Boolean, Obj.IsDamage)
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

'        Public Function UpdatePurchaseInvoice(ByVal Obj As CommonInfo.PurchaseInvoiceInfo) As Boolean Implements IPurchaseInvoiceDA.UpdatePurchaseInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Update tbl_PurchaseInvoice set PDate= @PDate , StaffID= @StaffID , CustomerID= @CustomerID , Address= @Address , ItemCategoryID= @ItemCategoryID , ItemName= @ItemName , Length= @Length , Qty= @Qty , GoldQualityID= @GoldQualityID , SubGoldQualityID= @SubGoldQualityID, PurchaseRate= @PurchaseRate ,TotalAmount= @TotalAmount , AddOrSub= @AddOrSub , PaidAmount= @PaidAmount , GoldPrice= @GoldPrice , GemsPrice= @GemsPrice , Remark= @Remark ,LocationID=@LocationID, LastModifiedDate = @LastModifiedDate , LastModifiedLoginUserName =@LastModifiedLoginUserName ,IsExchange=@IsExchange,FromShopOrCustomer=@FromShopOrCustomer,OldSaleInvoiceID=@OldSaleInvoiceID, OldSaleAmount= @OldSaleAmount,GoldTK=@GoldTK,GoldTG=@GoldTG,GemsTK=@GemsTK,GemsTG=@GemsTG,TotalTK=@TotalTK,TotalTG=@TotalTG,IsDamage=@IsDamage"
'                strCommandText += " where PurchaseInvoiceID= @PurchaseInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, Obj.PurchaseInvoiceID)
'                DB.AddInParameter(DBComm, "@PDate", DbType.Date, Obj.PDate)
'                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
'                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
'                DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
'                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
'                DB.AddInParameter(DBComm, "@ItemName", DbType.String, Obj.ItemName)
'                DB.AddInParameter(DBComm, "@Length", DbType.String, Obj.Length)
'                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
'                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
'                DB.AddInParameter(DBComm, "@SubGoldQualityID", DbType.String, Obj.SubGoldQualityID)
'                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int32, Obj.PurchaseRate)
'                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, Obj.TotalAmount)
'                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
'                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, Obj.PaidAmount)
'                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int32, Obj.GoldPrice)
'                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int32, Obj.GemsPrice)
'                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
'                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
'                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
'                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
'                DB.AddInParameter(DBComm, "@IsExchange", DbType.Boolean, Obj.IsExchange)
'                DB.AddInParameter(DBComm, "@FromShopOrCustomer", DbType.Boolean, Obj.FromShopOrCustomer)
'                DB.AddInParameter(DBComm, "@OldSaleInvoiceID", DbType.String, Obj.OldSaleInvoiceID)
'                DB.AddInParameter(DBComm, "@OldSaleAmount", DbType.Int32, Obj.OldSaleAmount)
'                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
'                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
'                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, Obj.TotalTK)
'                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, Obj.TotalTG)
'                DB.AddInParameter(DBComm, "@IsDamage", DbType.Boolean, Obj.IsDamage)
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
'        Public Function GetPurchaseInvoice(ByVal PurchaseInvoiceID As String) As CommonInfo.PurchaseInvoiceInfo Implements IPurchaseInvoiceDA.GetPurchaseInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim drResult As IDataReader
'            Dim obj As New CommonInfo.PurchaseInvoiceInfo
'            Try
'                strCommandText = " SELECT PurchaseInvoiceID, PDate, StaffID, CustomerID, Address, ItemCategoryID, ItemName, Length, Qty, GoldQualityID ,  PurchaseRate, TotalAmount, AddOrSub, PaidAmount, GoldPrice, GemsPrice, Remark, LocationID, IsExchange, FromShopOrCustomer, OldSaleInvoiceID, OldSaleAmount, IsDamage, " & _
'                                " GoldTK, GoldTG, GemsTK, GemsTG, TotalTK, TotalTG,   " & _
'                                " CAST(GoldTK AS INT) AS GoldK, " & _
'                                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY, " & _
'                                " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
'                                " CAST(GemsTK AS INT) AS GemsK," & _
'                                " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                                " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY," & _
'                                " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8)-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC," & _
'                                " CAST(TotalTK AS INT) AS TotalK," & _
'                                " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                                " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT) AS TotalY," & _
'                                " CAST(((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8)-CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC " & _
'                                " FROM tbl_PurchaseInvoice WHERE PurchaseInvoiceID= @PurchaseInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, PurchaseInvoiceID)
'                drResult = DB.ExecuteReader(DBComm)
'                If drResult.Read() Then
'                    With obj
'                        .PurchaseInvoiceID = drResult("PurchaseInvoiceID")
'                        .PDate = drResult("PDate")
'                        .StaffID = drResult("StaffID")
'                        .CustomerID = drResult("CustomerID")
'                        .Address = drResult("Address")
'                        .ItemCategoryID = drResult("ItemCategoryID")
'                        .ItemName = drResult("ItemName")
'                        .Length = drResult("Length")
'                        .Qty = drResult("Qty")
'                        .GoldQualityID = drResult("GoldQualityID")
'                        '.SubGoldQualityID = drResult("SubGoldQualityID")
'                        .PurchaseRate = drResult("PurchaseRate")
'                        .GoldK = drResult("GoldK")
'                        .GoldP = drResult("GoldP")
'                        .GoldY = drResult("GoldY")
'                        .GoldC = drResult("GoldC")
'                        .GemsK = drResult("GemsK")
'                        .GemsP = drResult("GemsP")
'                        .GemsY = drResult("GemsY")
'                        .GemsC = drResult("GemsC")
'                        .TotalK = drResult("TotalK")
'                        .TotalP = drResult("TotalP")
'                        .TotalY = drResult("TotalY")
'                        .TotalC = drResult("TotalC")
'                        .TotalAmount = drResult("TotalAmount")
'                        .AddOrSub = drResult("AddOrSub")
'                        .PaidAmount = drResult("PaidAmount")
'                        .GoldPrice = drResult("GoldPrice")
'                        .GemsPrice = drResult("GemsPrice")
'                        .Remark = IIf(IsDBNull(drResult.Item("Remark")) = True, "", drResult.Item("Remark"))
'                        .IsExchange = drResult("IsExchange")
'                        .IsDamage = drResult("IsDamage")
'                        .FromShopOrCustomer = drResult("FromShopOrCustomer")
'                        .OldSaleInvoiceID = drResult("OldSaleInvoiceID")
'                        .OldSaleAmount = drResult("OldSaleAmount")
'                        .GoldTK = drResult("GoldTK")
'                        .GoldTG = drResult("GoldTG")
'                        .GemsTK = drResult("GemsTK")
'                        .GemsTG = drResult("GemsTG")
'                        .TotalTK = drResult("TotalTK")
'                        .TotalTG = drResult("TotalTG")
'                    End With
'                End If
'                drResult.Close()
'            Catch ex As Exception
'                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
'            End Try
'            Return obj
'        End Function

'        Public Function DeletePurchaseInvoiceItem(ByVal PurchaseInvoiceGemsItemID As String) As Boolean Implements IPurchaseInvoiceDA.DeletePurchaseInvoiceItem
'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "DELETE FROM tbl_PurchaseInvoiceGemsItem WHERE  PurchaseInvoiceGemsItemID= @PurchaseInvoiceGemsItemID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceGemsItemID", DbType.String, PurchaseInvoiceGemsItemID)
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

'        Public Function GetPurchaseInvoiceItem(ByVal PurchaseInvoiceID As String) As System.Data.DataTable Implements IPurchaseInvoiceDA.GetPurchaseInvoiceItem
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                'strCommandText = "Select * From tbl_PurchaseInvoiceGemsItem Where PurchaseInvoiceID ='" & PurchaseInvoiceID & "' Order by PurchaseInvoiceID"
'                strCommandText = "Select PurchaseInvoiceGemsItemID,PurchaseInvoiceID,GemsCategoryID as [@GemsCategoryID],GemsName,GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,PurchaseRate,Amount,CASE FixType WHEN 1 Then 'Fix' WHEN 2 Then 'ByWeight' WHEN 3 Then 'ByQty' end as FixType, "
'                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
'                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
'                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY,"
'                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8)-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC "
'                strCommandText += " From tbl_PurchaseInvoiceGemsItem Where PurchaseInvoiceID = '" & PurchaseInvoiceID & "' Order By PurchaseInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function InsertPurchaseInvoiceItem(ByVal Obj As CommonInfo.PurchaseInvoiceGemsItemInfo) As Boolean Implements IPurchaseInvoiceDA.InsertPurchaseInvoiceItem
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Insert into tbl_PurchaseInvoiceGemsItem ( PurchaseInvoiceGemsItemID,PurchaseInvoiceID,GemsCategoryID,GemsName,GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,PurchaseRate,FixType,Amount)"
'                strCommandText += " Values (@PurchaseInvoiceGemsItemID,@PurchaseInvoiceID,@GemsCategoryID,@GemsName,@GemsTK,@GemsTG,@YOrCOrG,@GemsTW,@Qty,@PurchaseRate,@FixType,@Amount)"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceGemsItemID", DbType.String, Obj.PurchaseInvoiceGemsItemID)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, Obj.PurchaseInvoiceID)
'                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
'                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
'                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, Obj.GemsK)
'                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, Obj.GemsP)
'                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, Obj.GemsY)
'                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, Obj.GemsC)

'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)


'                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
'                'DB.AddInParameter(DBComm, "@GemY", DbType.Int32, Obj.GemY)
'                'DB.AddInParameter(DBComm, "@GemBCG", DbType.Decimal, Obj.GemBCG)
'                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemTW)
'                ' DB.AddInParameter(DBComm, "@GemTK", DbType.Decimal, Obj.GemTK)
'                'DB.AddInParameter(DBComm, "@GemP", DbType.Decimal, Obj.GemP)
'                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
'                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int32, Obj.PurchaseRate)
'                DB.AddInParameter(DBComm, "@FixType", DbType.Int32, Obj.FixType)
'                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, Obj.Amount)

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

'        Public Function UpdatePurchaseInvoiceItem(ByVal Obj As CommonInfo.PurchaseInvoiceGemsItemInfo) As Boolean Implements IPurchaseInvoiceDA.UpdatePurchaseInvoiceItem
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Update tbl_PurchaseInvoiceGemsItem set PurchaseInvoiceID= @PurchaseInvoiceID , GemsCategoryID= @GemsCategoryID , GemsName= @GemsName , GemsTK=@GemsTK,GemsTG=@GemsTG, YOrCOrG= @YOrCOrG ,GemsTW= @GemsTW ,Qty= @Qty , PurchaseRate= @PurchaseRate , FixType= @FixType , Amount= @Amount "
'                strCommandText += " where PurchaseInvoiceGemsItemID= @PurchaseInvoiceGemsItemID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceGemsItemID", DbType.String, Obj.PurchaseInvoiceGemsItemID)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, Obj.PurchaseInvoiceID)
'                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
'                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
'                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, Obj.GemsK)
'                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, Obj.GemsP)
'                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, Obj.GemsY)
'                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, Obj.GemsC)


'                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
'                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)

'                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
'                'DB.AddInParameter(DBComm, "@GemY", DbType.Int32, Obj.GemY)
'                'DB.AddInParameter(DBComm, "@GemBCG", DbType.Decimal, Obj.GemBCG)
'                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemTW)
'                'DB.AddInParameter(DBComm, "@GemTK", DbType.Decimal, Obj.GemTK)
'                'DB.AddInParameter(DBComm, "@GemP", DbType.Decimal, Obj.GemP)
'                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
'                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int32, Obj.PurchaseRate)
'                DB.AddInParameter(DBComm, "@FixType", DbType.Int32, Obj.FixType)
'                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, Obj.Amount)

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
'        Public Function GetAllPurchaseInvoice() As System.Data.DataTable Implements IPurchaseInvoiceDA.GetAllPurchaseInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = "SELECT P.PurchaseInvoiceID ,P.OldSaleInvoiceID AS [@OldSaleInvoiceID],convert(varchar(10),P.PDate,105) As PurchaseDate,G.GoldQuality,I.ItemCategory AS [ItemCategory_],P.ItemName AS [ItemName_],P.Qty,P.StaffID as [@StaffID],S.Staff AS [Staff_],C.CustomerName AS [Customer_],P.Address AS [Address_], P.Remark AS [Remark_], P.ItemCategoryID As [@ItemCategoryID],"
'                strCommandText += "P.Length,P.GoldQualityID As [@GoldQualityID] "
'                strCommandText += "FROM tbl_PurchaseInvoice P Left JOIN tbl_ItemCategory I ON P.ItemCategoryID = I.ItemCategoryID Left JOIN "
'                strCommandText += "tbl_GoldQuality G ON P.GoldQualityID = G.GoldQualityID Left JOIN tbl_Staff S On P.StaffID = S.StaffID  Left JOIN tbl_Customer C On P.CustomerID =C.CustomerID Order by PurchaseInvoiceID"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function GetAllPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceDA.GetAllPurchaseInvoiceReport
'            ''Dim strCommandText As String
'            ''Dim DBComm As DbCommand
'            ''Dim dtResult As DataTable
'            ''Try

'            ''    strCommandText = "SELECT P.PurchaseInvoiceID as [@PurchaseInvoiceID],convert(varchar(10),P.PDate,105) As [PDate],P.StaffID as [@StaffID],S.Staff,P.Customer,I.ItemCategory,G.GoldQuality,P.ItemCategoryID As [@ItemCategoryID],P.PurchaseRate,"
'            ''    strCommandText += "P.ItemName,P.Length,P.Qty,P.GoldQualityID As [@GoldQualityID],P.GoldK,P.GoldP,P.GoldY,P.GoldC,P.GemsK,P.GemsP,P.GemsY,P.GemsC,P.TotalK,P.TotalP,P.TotalY,P.TotalC,P.GoldPrice,P.GemsPrice,P.Remark,P.LocationID,L.Location, P.TotalAmount,P.PaidAmount, "
'            ''    strCommandText += "P.GoldTK,P.GemsTK as TotalGemsTK,P.TotalTK FROM tbl_PurchaseInvoice P Left JOIN tbl_ItemCategory I ON P.ItemCategoryID = I.ItemCategoryID Left JOIN "
'            ''    strCommandText += "tbl_GoldQuality G ON P.GoldQualityID = G.GoldQualityID Left JOIN tbl_Staff S On P.StaffID = S.StaffID Left JOIN tbl_Location L On P.LocationID = L.LocationID WHERE 1=1 And P.PDate between '" & FromDate & " 00:00:00' and '" & ToDate & " 23:59:59'" & cristr & " Order By P.PDate"

'            ''    DBComm = DB.GetSqlStringCommand(strCommandText)
'            ''    DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate.Date)
'            ''    DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'            ''    dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'            ''    Return dtResult
'            ''Catch ex As Exception
'            ''    MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'            Return New DataTable
'            ''End Try
'        End Function

'        Public Function GetPurchasePercent(ByVal GoldQualityID As String) As Integer Implements IPurchaseInvoiceDA.GetPurchasePercent
'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "SELECT PercentPurchaseRate FROM tbl_StandardRate WHERE  "
'                strCommandText += "DefineDateTime = (select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID = @GoldQualityID)"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
'                GetPurchasePercent = CStr(DB.ExecuteScalar(DBComm))
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function GetExchangePercent(ByVal GoldQualityID As String) As Integer Implements IPurchaseInvoiceDA.GetExchangePercent
'            Try
'                Dim strCommandText As String
'                Dim DBComm As DbCommand
'                strCommandText = "SELECT PercentExchangeRate FROM tbl_StandardRate WHERE  "
'                strCommandText += "DefineDateTime = (select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID = @GoldQualityID)"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
'                GetExchangePercent = CStr(DB.ExecuteScalar(DBComm))
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return False
'            End Try
'        End Function

'        Public Function GetAllPurchaseInvoiceForDetailRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceDA.GetAllPurchaseInvoiceForDetailRpt
'            ''Dim strCommandText As String
'            ''Dim DBComm As DbCommand
'            ''Dim dtResult As DataTable
'            ''Try

'            ''    strCommandText = "SELECT P.PurchaseInvoiceID as [@PurchaseInvoiceID],P.OldSaleInvoiceID,convert(varchar(10),P.PDate,105) As [PDate],P.StaffID as [@StaffID],S.Staff,P.Customer,I.ItemCategory,G.GoldQuality,P.ItemCategoryID As [@ItemCategoryID],P.PurchaseRate,"
'            ''    strCommandText += "P.ItemName,P.Length,P.Qty,P.GoldQualityID As [@GoldQualityID],P.GoldK,P.GoldP,P.GoldY,P.GoldC,P.GemsK,P.GemsP,P.GemsY,P.GemsC,P.TotalK,P.TotalP,P.TotalY,P.TotalC,P.GoldPrice,P.GemsPrice,P.Remark,P.LocationID,L.Location, P.TotalAmount,P.PaidAmount, "
'            ''    strCommandText += "(P.GoldTK+SS.WasteTK) as GoldTK,P.GemsTK as TotalGemsTK,P.TotalTK FROM tbl_PurchaseInvoice P Left Join tbl_SaleInvoice SS On P.OldSaleInvoiceID=SS.SaleInvoiceID Left JOIN tbl_ItemCategory I ON P.ItemCategoryID = I.ItemCategoryID Left JOIN "
'            ''    strCommandText += "tbl_GoldQuality G ON P.GoldQualityID = G.GoldQualityID Left JOIN tbl_Staff S On P.StaffID = S.StaffID Left JOIN tbl_Location L On P.LocationID = L.LocationID WHERE 1=1 And P.PDate between '" & FromDate & " 00:00:00' and '" & ToDate & " 23:59:59'" & cristr & " Order By P.PDate"

'            ''    DBComm = DB.GetSqlStringCommand(strCommandText)
'            ''    DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'            ''    DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'            ''    dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'            ''    Return dtResult
'            ''Catch ex As Exception
'            ''    MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'            Return New DataTable
'            ''End Try
'        End Function

'        Public Function GetPurchaseInvoiceSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceDA.GetPurchaseInvoiceSummaryReport
'            ''Dim strCommandText As String
'            ''Dim DBComm As DbCommand
'            ''Dim dtResult As DataTable
'            ''Try

'            ''    strCommandText = "select P.ItemCategoryID,I.ItemCategory,sum(P.Qty) as Qty,P.GoldQualityID,G.GoldQuality,Sum(P.GoldTK) as GoldTK,P.LocationID,L.Location "
'            ''    strCommandText += "From tbl_PurchaseInvoice P Left Join tbl_ItemCategory I On P.ItemCategoryID=I.ItemCategoryID "
'            ''    strCommandText += " Left Join tbl_GoldQuality G On P.GoldQualityID=G.GoldQualityID"
'            ''    strCommandText += " Left Join tbl_Location L On P.LocationID = L.LocationID"
'            ''    strCommandText += " where 1=1 and P.PDate between '" & FromDate & " 00:00:00' and '" & ToDate & " 23:59:59'" & cristr & ""
'            ''    strCommandText += " group by P.ItemCategoryID,I.ItemCategory,P.GoldQualityID,G.GoldQuality,P.LocationID,L.Location"

'            ''    DBComm = DB.GetSqlStringCommand(strCommandText)
'            ''    DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
'            ''    DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
'            ''    dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'            ''    Return dtResult
'            ''Catch ex As Exception
'            ''    MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'            Return New DataTable
'            ''End Try
'        End Function

'        Public Function GetPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceDA.GetPurchaseInvoiceReport
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                'strCommandText = "SELECT PurchaseInvoiceID,PDate,L.CustomerName as Customer, P.GoldQualityID, G.GoldQuality, P.ItemCategoryID, C.ItemCategory, S.Staff,  P.Address as Address, ItemName, Length, QTY,P.GoldTG, P.GemsTG, P.TotalTG, " & _
'                '" CAST(GoldTK AS INT) AS GoldK," & _
'                '" CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                '" CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
'                '" CAST(GemsTK AS INT) AS GemsK, " & _
'                '" CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                '" CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
'                '" CAST(TotalTK AS INT) AS TotalK, " & _
'                '" CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                '" CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY," & _
'                '" GoldTK, GemsTK, TotalTK, PurchaseRate, GoldPrice, GemsPrice, TotalAmount, P.Remark " & _
'                '" FROM tbl_PurchaseInvoice P LEFT JOIN tbl_Customer L ON P.CustomerID=L.CustomerID " & _
'                '" LEFT JOIN tbl_GoldQuality G ON P.GoldQualityID=G.GoldQualityID " & _
'                '" LEFT JOIN tbl_ItemCategory C ON P.ItemCategoryID=C.ItemCategoryID" & _
'                '" LEFT JOIN tbl_Staff S ON P.StaffID=S.StaffID " & _
'                '" WHERE PDate BETWEEN @FromDate AND @ToDate " & cristr & ""

'                strCommandText = " Select PD.PurchaseDetailID,PD.BarcodeNo,PD.TotalTK,PD.TotalTG,PD.GoldQualityID," & _
'                                 " PD.ItemCategoryID,PD.Qty,P.PurchaseDate,P.IsShop,P.IsGem,P.IsChange, " & _
'                                 " CAST(TotalTK AS INT) AS TotalK, " & _
'                                 " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                                 " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS TotalY," & _
'                                 " P.PurchaseHeaderID,S.Staff,C.CustomerName As Customer,G.GoldQuality,IC.ItemCategory,PD.CurrentPirce, 0 As PurchasePaidAmount,P.AllTotalAmount,P.AllAddOrSub,P.AllPaidAmount " & _
'                                 " From tbl_PurchaseDetail PD Left Join tbl_PurchaseHeader P on P.PurchaseHeaderID=PD.PurchaseHeaderID   " & _
'                                 " Left Join tbl_GoldQuality G on G.GoldQualityID=PD.GoldQualityID Left Join tbl_Customer C on C.CustomerID=P.CustomerID " & _
'                                 " Left Join tbl_Staff S on S.StaffID=P.StaffID Left Join tbl_ItemName I on I.ItemNameID=PD.ItemNameID  " & _
'                                 " Left Join tbl_ItemCategory IC on IC.ItemcategoryID=PD.ItemCategoryID" & _
'                                 " WHERE PurchaseDate BETWEEN '" & FromDate & " 00:00:00' AND '" & ToDate & " 23:59:59' " & cristr & ""

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate.Date)
'                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate.Date)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function
'        Public Function GetPurchaseInvoiceForBarcodeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseInvoiceDA.GetPurchaseInvoiceForBarcodeReport
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " SELECT PurchaseInvoiceID, PDate,L.CustomerName as Customer, P.GoldQualityID, G.GoldQuality, P.ItemCategoryID, C.ItemCategory, S.Staff,  P.Address as Address, P.ItemName, P.Length, P.QTY,P.GoldTG, P.GemsTG, P.TotalTG, " & _
'                                 " CAST(P.GoldTK AS INT) AS GoldK," & _
'                                 " CAST((P.GoldTK-CAST(P.GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                                 " CAST((((P.GoldTK-CAST(P.GoldTK AS INT))*16)-CAST((P.GoldTK-CAST(P.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
'                                 " CAST(P.GemsTK AS INT) AS GemsK, " & _
'                                 " CAST((P.GemsTK-CAST(P.GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                                 " CAST((((P.GemsTK-CAST(P.GemsTK AS INT))*16)-CAST((P.GemsTK-CAST(P.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
'                                 " CAST(P.TotalTK AS INT) AS TotalK, " & _
'                                 " CAST((P.TotalTK-CAST(P.TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                                 " CAST((((P.TotalTK-CAST(P.TotalTK AS INT))*16)-CAST((P.TotalTK-CAST(P.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY," & _
'                                 " P.GoldTK, P.GemsTK, P.TotalTK, PurchaseRate, P.GoldPrice, P.GemsPrice, P.TotalAmount, P.Remark, PDate AS [@PDate] " & _
'                                 " FROM tbl_PurchaseDetail PD LEFT JOIN tbl_PurchaseHeader P ON P.PurchaseHeaderID=PD.PurchaseHeaderID " & _
'                                 " LEFT JOIN tbl_PurchaseGemsItem PG ON " & _
'                                 "  LEFT JOIN tbl_Customer L ON P.CustomerID=L.CustomerID " & _
'                                 " LEFT JOIN tbl_GoldQuality G ON P.GoldQualityID=G.GoldQualityID " & _
'                                 " LEFT JOIN tbl_ItemCategory C ON P.ItemCategoryID=C.ItemCategoryID" & _
'                                 " LEFT JOIN tbl_Staff S ON P.StaffID=S.StaffID " & _
'                                 " Left Join tbl_SaleInvoice SI on P.OldSaleInvoiceID=SI.SaleInvoiceID " & _
'                                 " WHERE PDate BETWEEN '" & FromDate & " 00:00:00' AND '" & ToDate & " 23:59:59' " & cristr & " ORDER BY [@PDate] DESC"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function
'        Public Function GetPurchaseSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As System.Data.DataTable Implements IPurchaseInvoiceDA.GetPurchaseSummaryByGoldQualityAndItemCategory
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = " SELECT  IsNull(SUM(P.QTY),0) AS QTY, IsNull(SUM(P.TotalTK),0)AS TotalTK  " & _
'                                 " FROM tbl_PurchaseInvoice P   " & _
'                                 " WHERE P.ItemCategoryID=@ItemCategoryID AND P.GoldQualityID=@GoldQualityID AND PDate=@ForDate"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
'                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
'                DB.AddInParameter(DBComm, "@ForDate", DbType.Date, ForDate)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)

'                Return dtResult

'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function GetPurchaseInvoicePrint(ByVal PurchaseInvoiceID As String) As System.Data.DataTable Implements IPurchaseInvoiceDA.GetPurchaseInvoicePrint
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = "SELECT H.PurchaseInvoiceID, PDate,H.StaffID,S.Staff, C.CustomerName as Customer, H.Address, ItemCategoryID,GC.GemsCategory,HG.GemsName,HG.YOrCOrG,HG.Qty as GemsQty,ItemName, Length, H.GoldQualityID ,G.GoldQuality, SubGoldQualityID, H.PurchaseRate, TotalAmount, AddOrSub, PaidAmount, GoldPrice, GemsPrice, H.Remark, IsExchange, FromShopOrCustomer,  OldSaleAmount, IsDamage, " & _
'                                 " H.GoldTK, H.GoldTG, H.GemsTK, H.GemsTG, H.TotalTK, H.TotalTG,'' as Photo,  " & _
'                                 " CAST(H.GoldTK AS INT) AS GoldK, " & _
'                                 " CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                                 " CAST((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, " & _
'                                 " CAST(((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8)-CAST((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
'                                 " CAST(H.GemsTK AS INT) AS GemsK," & _
'                                 " CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                                 " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
'                                 " CAST(((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8)-CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC," & _
'                                 " CAST(H.TotalTK AS INT) AS TotalK," & _
'                                 " CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                                 " CAST((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY," & _
'                                 " CAST(((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8)-CAST((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC  " & _
'                                 " FROM tbl_PurchaseInvoice H " & _
'                                 " left join tbl_PurchaseInvoiceGemsItem HG on H.PurchaseInvoiceID=HG.PurchaseInvoiceID " & _
'                                 " left Join tbl_GemsCategory GC on HG.GemsCategoryID=GC.GemsCategoryID " & _
'                                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
'                                 " LEFT JOIN tbl_Customer  C ON H.CustomerID=C.CustomerID  " & _
'                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
'                                 " WHERE H.PurchaseInvoiceID= @PurchaseInvoiceID"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, PurchaseInvoiceID)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function


'        Public Function InsertPurchaseInvoiceUserID(ByVal PurchaseInvoiceID As String, ByVal UserID As String) As Boolean Implements IPurchaseInvoiceDA.InsertPurchaseInvoiceUserID
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Try
'                strCommandText = "Update tbl_PurchaseInvoice set   UserID= @UserID "
'                strCommandText += " where PurchaseInvoiceID= @PurchaseInvoiceID"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, PurchaseInvoiceID)
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

'        Public Function GetSaleInvoiceItemFromPurchaseInvoice(PurchaseInvoiceID As String, SaleInvoice As String) As DataTable Implements IPurchaseInvoiceDA.GetSaleInvoiceItemFromPurchaseInvoice
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = "SELECT H.PurchaseInvoiceID, PDate,F.ItemCode,H.StaffID,S.Staff, C.CustomerName as Customer, H.Address, H.ItemCategoryID,IC.ItemCategory,GC.GemsCategory,SIG.GemsName,SIG.YOrCOrG,SIG.Qty as GemsQty, F.Photo ,H.ItemName, H.Length, H.GoldQualityID ,G.GoldQuality,  H.PurchaseRate, TotalAmount, H.AddOrSub, H.PaidAmount, H.GoldPrice, H.GemsPrice, H.Remark, IsExchange, FromShopOrCustomer, OldSaleInvoiceID, OldSaleAmount, IsDamage, " & _
'                                 " H.GoldTK, H.GoldTG, H.GemsTK, H.GemsTG, H.TotalTK, H.TotalTG,F.Photo,  " & _
'                                 " CAST(H.GoldTK AS INT) AS GoldK, " & _
'                                 " CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT) AS GoldP," & _
'                                 " CAST((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY, " & _
'                                 " CAST(((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8)-CAST((((H.GoldTK-CAST(H.GoldTK AS INT))*16)-CAST((H.GoldTK-CAST(H.GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC," & _
'                                 " CAST(H.GemsTK AS INT) AS GemsK," & _
'                                 " CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP," & _
'                                 " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
'                                 " CAST(((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8)-CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC," & _
'                                 " CAST(H.TotalTK AS INT) AS TotalK," & _
'                                 " CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT) AS TotalP," & _
'                                 " CAST((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY," & _
'                                 " CAST(((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8)-CAST((((H.TotalTK-CAST(H.TotalTK AS INT))*16)-CAST((H.TotalTK-CAST(H.TotalTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS TotalC  " & _
'                                 " FROM tbl_PurchaseInvoice H  " & _
'                                 " left join tbl_PurchaseInvoiceGemsItem HG on H.PurchaseInvoiceID=HG.PurchaseInvoiceID  " & _
'                                 " left Join tbl_SaleInvoice SI on H.OldSaleInvoiceID=SI.SaleInvoiceID " & _
'                                 " left Join tbl_SaleInvoiceGemsItem SIG on SI.SaleInvoiceID=SIG.SaleInvoiceID  " & _
'                                 " left Join tbl_GemsCategory GC on SIG.GemsCategoryID=GC.GemsCategoryID  " & _
'                                 " left join tbl_ForSale F on SI.ItemCode=F.ItemCode " & _
'                                 " left join tbl_ItemCategory IC on H.ItemCategoryID=IC.ItemCategorYID " & _
'                                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   " & _
'                                 " LEFT JOIN tbl_Customer  C ON H.CustomerID=C.CustomerID   " & _
'                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
'                                 " WHERE H.PurchaseInvoiceID= @PurchaseInvoiceID  and H.OldSaleInvoiceID=@SaleInvoice"
'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@PurchaseInvoiceID", DbType.String, PurchaseInvoiceID)
'                DB.AddInParameter(DBComm, "@SaleInvoice", DbType.String, SaleInvoice)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function GetPurchaseInvoiceGemReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseInvoiceDA.GetPurchaseInvoiceGemReport
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                strCommandText = "Select  P.PurchaseHeaderID,P.PurchaseDate,P.IsGem,P.IsShop,PD.PurchaseDetailID,PD.ItemCategoryID aS GemsCategoryID, G.GemsCategory, PD.ItemName As GemsName,PD.TotalGemTK,CAST((PD.TotalGemTG) AS DECIMAL(18,3)) as TotalGemTG,PD.YOrCOrG,PD.QTY, P.PurchaseDate AS [@PDate], PD.TotalAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub) AS NetAmount " & _
'                                 " From tbl_PurchaseHeader P Left Join tbl_PurchaseDetail PD on PD.PurchaseHeaderID=P.PurchaseHeaderID Left Join tbl_GemsCategory G on G.GemsCategoryID=PD.ItemCategoryID" & _
'                                 " WHERE PurchaseDate BETWEEN '" & FromDate & " 00:00:00' AND '" & ToDate & " 23:59:59' " & cristr & " ORDER BY [@PDate] DESC"

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate.Date)
'                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate.Date)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function

'        Public Function GetPurchaseInvoiceDailyTransactionReport(ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseInvoiceDA.GetPurchaseInvoiceDailyTransactionReport
'            Dim strCommandText As String
'            Dim DBComm As DbCommand
'            Dim dtResult As DataTable
'            Try
'                'If (cristr = " And P.IsGem=0 And P.IsChange=0" Or cristr = " And P.IsGem=0 And P.IsChange=1") Then

'                strCommandText = " Select PD.PurchaseDetailID,PD.BarcodeNo," & _
'                       " PD.GoldQualityID,  PD.ItemCategoryID,PD.ItemNameID,PD.Qty as QTY,Convert(varchar(10),P.PurchaseDate,105) As PurchaseDate, " & _
'                       " P.PurchaseDate As [@PDate] ,P.AllTotalAmount,P.AllAddOrSub,P.AllPaidAmount,PD.TotalTK,PD.TotalTG,CAST(PD.TotalTK AS INT) AS TotalK,  " & _
'                       " CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT) AS TotalP,CAST((((PD.TotalTK-CAST(PD.TotalTK AS INT))*16)-CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS TotalY, " & _
'                       " P.PurchaseHeaderID,C.CustomerName As Customer,G.GoldQuality,IsNull(IC.ItemCategory,'') as ItemCategory,(PD.TotalAmount-PD.AddSub) AS TotalAmount,IsNull(I.ItemName,'') as ItemName," & _
'                       " PD.CurrentPrice, 0 As PurchasePaidAmount,P.IsChange  From tbl_PurchaseDetail PD Left Join tbl_PurchaseHeader P " & _
'                       " on P.PurchaseHeaderID=PD.PurchaseHeaderID     Left Join tbl_GoldQuality G on G.GoldQualityID=PD.GoldQualityID " & _
'                       " Left Join tbl_Customer C on C.CustomerID=P.CustomerID   Left Join tbl_ItemName I on I.ItemNameID=PD.ItemNameID" & _
'                       " Left Join tbl_ItemCategory IC on IC.ItemcategoryID=PD.ItemCategoryID  " & _
'                       " WHERE PurchaseDate BETWEEN '" & ToDate & " 00:00:00' AND '" & ToDate & " 23:59:59'" & cristr & _
'                       " Order by  [@PDate] DESC"

'                'Else
'                'strCommandText = " Select PD.PurchaseDetailID,PD.BarcodeNo,'0' as GoldQualityID,  PD.ItemCategoryID,'0' as ItemNameID,PD.Qty as QTY," & _
'                '        " Convert(varchar(10),P.PurchaseDate,105) As PurchaseDate, P.PurchaseDate As [@PDate] ," & _
'                '        " P.AllTotalAmount,P.AllAddOrSub,P.AllPaidAmount,PD.TotalGemTK as TotalTK,PD.TotalGemTG as TotalTG,CAST(PD.TotalGemTK AS INT) AS TotalK," & _
'                '        " CAST((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16 AS INT) AS TotalP," & _
'                '        " CAST((((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16)-CAST((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS TotalY," & _
'                '        " P.PurchaseHeaderID,C.CustomerName As Customer,'' as GoldQuality,IsNull(IC.GemsCategory,'') as ItemCategory," & _
'                '        " (PD.TotalAmount-PD.AddSub) AS TotalAmount,'' as ItemName, PD.CurrentPrice, 0 As PurchasePaidAmount " & _
'                '        " From tbl_PurchaseDetail PD Left Join tbl_PurchaseHeader P  on P.PurchaseHeaderID=PD.PurchaseHeaderID" & _
'                '        " Left Join tbl_Customer C on C.CustomerID=P.CustomerID Left Join tbl_GemsCategory IC on IC.GemsCategoryID=PD.ItemCategoryID" & _
'                '        " WHERE PurchaseDate BETWEEN '" & ToDate & " 00:00:00' AND '" & ToDate & " 23:59:59'" & cristr & _
'                '        " Order by  [@PDate] DESC"

'                'End If

'                DBComm = DB.GetSqlStringCommand(strCommandText)
'                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'                Return dtResult
'            Catch ex As Exception
'                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'                Return New DataTable
'            End Try
'        End Function


'        'Public Function GetPurchaseForIsChange(ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseInvoiceDA.GetPurchaseForIsChange
'        '    Dim strCommandText As String
'        '    Dim DBComm As DbCommand
'        '    Dim dtResult As DataTable
'        '    Try

'        '        strCommandText = " Select PD.PurchaseDetailID,PD.BarcodeNo," & _
'        '               " PD.GoldQualityID,  PD.ItemCategoryID,PD.ItemNameID,PD.Qty as QTY,Convert(varchar(10),P.PurchaseDate,105) As PurchaseDate,PD.IsFixPrice, " & _
'        '               " P.PurchaseDate As [@PDate] ,P.AllTotalAmount,P.AllAddOrSub,P.AllPaidAmount,PD.TotalTK,PD.TotalTG,CAST(PD.TotalTK AS INT) AS TotalK,  " & _
'        '               " CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT) AS TotalP,CAST((((PD.TotalTK-CAST(PD.TotalTK AS INT))*16)-CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,1)) AS TotalY, " & _
'        '               " P.PurchaseHeaderID,C.CustomerName As Customer,G.GoldQuality,IsNull(IC.ItemCategory,'') as ItemCategory,TotalAmount ,IsNull(I.ItemName,'') as ItemName," & _
'        '               " PD.CurrentPrice, 0 As PurchasePaidAmount  From tbl_PurchaseDetail PD Left Join tbl_PurchaseHeader P " & _
'        '               " on P.PurchaseHeaderID=PD.PurchaseHeaderID     Left Join tbl_GoldQuality G on G.GoldQualityID=PD.GoldQualityID " & _
'        '               " Left Join tbl_Customer C on C.CustomerID=P.CustomerID   Left Join tbl_ItemName I on I.ItemNameID=PD.ItemNameID" & _
'        '               " Left Join tbl_ItemCategory IC on IC.ItemcategoryID=PD.ItemCategoryID  " & _
'        '               " WHERE PurchaseDate BETWEEN '" & ToDate & " 00:00:00' AND '" & ToDate & " 23:59:59'" & cristr & _
'        '               " Order by  [@PDate] DESC"

'        '        DBComm = DB.GetSqlStringCommand(strCommandText)
'        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
'        '        Return dtResult
'        '    Catch ex As Exception
'        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
'        '        Return New DataTable
'        '    End Try
'        'End Function
'    End Class
'End Namespace

