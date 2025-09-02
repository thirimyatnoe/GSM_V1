Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace ConsignmentSale
    Public Class ConsignmentSaleDA
        Implements IConsignmentSaleDA



#Region "Private Consignment Sale"

        Private DB As Database
        Private Shared ReadOnly _instance As IConsignmentSaleDA = New ConsignmentSaleDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IConsignmentSaleDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteConsignmentSale(ByVal ConsignmentSaleID As String) As Boolean Implements IConsignmentSaleDA.DeleteConsignmentSale
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "UPDATE tbl_ConsignmentSale SET IsDelete =CONVERT(bit,1),IsUpload= CONVERT(bit,0),JC_IsUpload=CONVERT(bit,0) WHERE  ConsignmentSaleID= @ConsignmentSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ConsignmentSaleID)
           
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

        Public Function DeleteConsignmentSaleItem(ByVal ConsignmentSaleItemID As String) As Boolean Implements IConsignmentSaleDA.DeleteConsignmentSaleItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_ConsignmentSaleItem WHERE  ConsignmentSaleItemID= @ConsignmentSaleItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleItemID", DbType.String, ConsignmentSaleItemID)
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

        Public Function GetAllConsignmentSale() As System.Data.DataTable Implements IConsignmentSaleDA.GetAllConsignmentSale
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ConsignmentSaleID,convert(varchar(10),ConsignDate,105) as ConsignDate,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,NetAmount,AddOrSub,Discount,PaidAmount,PurchaseHeaderID,PurchaseAmount " & _
                                  " From tbl_ConsignmentSale CS INNER join tbl_Staff S On CS.StaffID=S.StaffID INNER Join tbl_Customer C on CS.CustomerID=C.CustomerID WHERE CS.IsDelete=0 AND S.IsDelete=0 AND C.IsDelete=0 Order by ConsignDate desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetConsignmentSaleByID(ByVal ConsignmentSaleID As String) As CommonInfo.ConsignmentSaleInfo Implements IConsignmentSaleDA.GetConsignmentSaleByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New ConsignmentSaleInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_ConsignmentSale WHERE ConsignmentSaleID= @ConsignmentSaleID AND IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ConsignmentSaleID)
                drResult = DB.ExecuteReader(DBComm)

                If drResult.Read() Then
                    With obj
                        .ConsignmentSaleID = drResult("ConsignmentSaleID")
                        .ConsignDate = drResult("ConsignDate")
                        .WholesaleInvoiceID = drResult("WholesaleInvoiceID")
                        .StaffID = drResult("StaffID")
                        .CustomerID = drResult("CustomerID")
                        .NetAmount = drResult("NetAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .Discount = drResult("Discount")
                        .Remark = drResult("Remark")
                        .PaidAmount = drResult("PaidAmount")
                        .PurchaseHeaderID = drResult("PurchaseHeaderID")
                        .PurchaseAmount = drResult("PurchaseAmount")
                        .MemberID = drResult("MemberID")
                        .MemberCode = drResult("MemberCode")
                        .MemberName = drResult("MemberName")
                        .RedeemID = drResult("RedeemID")
                        .RedeemPoint = drResult("RedeemPoint")
                        .RedeemValue = drResult("RedeemValue")
                        .TopupPoint = drResult("TopupPoint")
                        .TopupValue = drResult("TopupValue")
                        .MemberDiscountAmt = drResult("MemberDiscountAmt")
                        .MemberDis = drResult("MemberDis")
                        .InvoiceStatus = drResult("InvoiceStatus")
                        .IsRedeemInvoice = drResult("IsRedeemInvoice")
                        .TransactionID = IIf(IsDBNull(drResult("TransactionID")), "", drResult("TransactionID"))
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetConsignmentSaleItem(ByVal ConsignmentSaleItemID As String) As System.Data.DataTable Implements IConsignmentSaleDA.GetConsignmentSaleItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * " & _
                                " From tbl_ConsignmentSaleItem where ConsignmentSaleItemID=@ConsignmentSaleItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleItemID", DbType.String, ConsignmentSaleItemID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetConsignmentSaleItemByID(ByVal ConsignmentSaleID As String) As System.Data.DataTable Implements IConsignmentSaleDA.GetConsignmentSaleItemByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  WI.ConsignmentSaleItemID,WI.ForSaleID as [ForSaleID],WI.ItemNameID,WI.GoldQualityID,I.ItemName,GQ.GoldQuality,WI.ItemCode,WI.IsReturn as Pay,WI.IsSale as Sale,WI.ItemTG as Gram,WI.SalesRate ,WI.GoldPrice as Amount,WI.ItemTG,WI.ItemTK,WI.GoldTK,WI.GoldTG,WI.WasteTK,WI.WasteTG,WI.GemsTK,WI.GemsTG,WI.GoldPrice,WI.FixPrice,  " & _
                                "CAST((WI.ItemTK-WI.GemsTK) AS INT) AS GoldK,CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                "CAST(((((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16)-CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                "CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                "CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                "CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                "CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY, " & _
                                "CAST(WI.GemsTK AS INT) AS GemsK,CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                "CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY  " & _
                                " From tbl_ConsignmentSaleItem WI " & _
                                "left join tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                "left join tbl_GoldQuality GQ on WI.GoldQualityID=GQ.GoldQualityID " & _
                                " where ConsignmentSaleID=@ConsignmentSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ConsignmentSaleID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertConsignmentSale(ByVal Obj As CommonInfo.ConsignmentSaleInfo) As Boolean Implements IConsignmentSaleDA.InsertConsignmentSale
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ConsignmentSale ( ConsignmentSaleID,ConsignDate,WholesaleInvoiceID,StaffID,CustomerID,Remark,NetAmount,AddOrSub,Discount,PaidAmount,LastModifiedLoginUserName,LastModifiedDate,LocationID,IsUpload,IsDelete,JC_IsUpload,PurchaseHeaderID,PurchaseAmount,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus)"
                strCommandText += " Values (@ConsignmentSaleID,@ConsignDate,@WholesaleInvoiceID,@StaffID,@CustomerID,@Remark,@NetAmount,@AddOrSub,@Discount,@PaidAmount,@LastModifiedLoginUserName,getDate(),@LocationID,CONVERT(bit,0),CONVERT(bit,0),CONVERT(bit,0),@PurchaseHeaderID,@PurchaseAmount,@MemberID,@MemberName,@MemberCode,@RedeemID,@TopupPoint,@TopupValue,@RedeemPoint,@RedeemValue,@IsRedeemInvoice,@MemberDis,@MemberDiscountAmt,@InvoiceStatus)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, Obj.ConsignmentSaleID)
                DB.AddInParameter(DBComm, "@ConsignDate", DbType.DateTime, Obj.ConsignDate)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceID", DbType.String, Obj.WholesaleInvoiceID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int64, Obj.NetAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, Obj.Discount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID) 'Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)
                DB.AddInParameter(DBComm, "@MemberID", DbType.String, Obj.MemberID)
                DB.AddInParameter(DBComm, "@MemberName", DbType.String, Obj.MemberName)
                DB.AddInParameter(DBComm, "@MemberCode", DbType.String, Obj.MemberCode)
                DB.AddInParameter(DBComm, "@RedeemID", DbType.String, Obj.RedeemID)
                DB.AddInParameter(DBComm, "@TopupPoint", DbType.Int32, Obj.TopupPoint)
                DB.AddInParameter(DBComm, "@TopupValue", DbType.Int32, Obj.TopupValue)
                DB.AddInParameter(DBComm, "@RedeemPoint", DbType.Int32, Obj.RedeemPoint)
                DB.AddInParameter(DBComm, "@RedeemValue", DbType.Int32, Obj.RedeemValue)
                DB.AddInParameter(DBComm, "@IsRedeemInvoice", DbType.Boolean, Obj.IsRedeemInvoice)
                DB.AddInParameter(DBComm, "@MemberDis", DbType.Decimal, Obj.MemberDis)
                DB.AddInParameter(DBComm, "@MemberDiscountAmt", DbType.Int64, Obj.MemberDiscountAmt)
                DB.AddInParameter(DBComm, "@InvoiceStatus", DbType.Int32, Obj.InvoiceStatus)

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

        Public Function InsertConsignmentSaleItem(ByVal ObjItem As CommonInfo.ConsignmentSaleItemInfo) As Boolean Implements IConsignmentSaleDA.InsertConsignmentSaleItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ConsignmentSaleItem ( ConsignmentSaleItemID,ConsignmentSaleID,ForSaleID,ItemNameID,GoldQualityID,ItemCode,IsReturn,IsSale,SalesRate,ItemTK,ItemTG,GemsTK,GemsTG,WasteTK,WasteTG,GoldTK,GoldTG,GoldPrice,FixPrice)"
                strCommandText += " Values (@ConsignmentSaleItemID,@ConsignmentSaleID,@ForSaleID,@ItemNameID,@GoldQualityID,@ItemCode,@IsReturn,@IsSale,@SalesRate,@ItemTK,@ItemTG,@GemsTK,@GemsTG,@WasteTK,@WasteTG,@GoldTK,@GoldTG,@GoldPrice,@FixPrice)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleItemID", DbType.String, ObjItem.ConsignmentSaleItemID)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ObjItem.ConsignmentSaleID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ObjItem.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ObjItem.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, ObjItem.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ObjItem.ItemCode)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, ObjItem.IsReturn)
                DB.AddInParameter(DBComm, "@IsSale", DbType.Boolean, ObjItem.IsSale)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, ObjItem.SalesRate)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, ObjItem.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, ObjItem.ItemTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjItem.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjItem.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, ObjItem.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, ObjItem.WasteTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, ObjItem.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, ObjItem.GoldTG)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int32, ObjItem.GoldPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int32, ObjItem.FixPrice)

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
        Public Function UpdateConsignmentSale(ByVal Obj As CommonInfo.ConsignmentSaleInfo) As Boolean Implements IConsignmentSaleDA.UpdateConsignmentSale
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ConsignmentSale set  ConsignmentSaleID= @ConsignmentSaleID , ConsignDate= @ConsignDate , WholesaleInvoiceID= @WholesaleInvoiceID, StaffID= @StaffID , CustomerID= @CustomerID , Remark= @Remark, NetAmount= @NetAmount ,PaidAmount=@PaidAmount, AddOrSub= @AddOrSub , Discount= @Discount , LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= getDate() , LocationID= @LocationID ,IsUpload= CONVERT(bit,0),JC_IsUpload=CONVERT(bit,0),PurchaseHeaderID=@PurchaseHeaderID,PurchaseAmount=@PurchaseAmount,MemberID=@MemberID,MemberName=@MemberName,MemberCode=@MemberCode,RedeemID=@RedeemID,TopupPoint=@TopupPoint,TopupValue=@TopupValue,RedeemPoint=@RedeemPoint,RedeemValue=@RedeemValue,IsRedeemInvoice=@IsRedeemInvoice,MemberDis=@MemberDis,MemberDiscountAmt=@MemberDiscountAmt,InvoiceStatus=@InvoiceStatus"
                strCommandText += " where ConsignmentSaleID= @ConsignmentSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, Obj.ConsignmentSaleID)
                DB.AddInParameter(DBComm, "@ConsignDate", DbType.DateTime, Obj.ConsignDate)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceID", DbType.String, Obj.WholesaleInvoiceID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int64, Obj.NetAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, Obj.Discount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID) 'Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)
                DB.AddInParameter(DBComm, "@MemberID", DbType.String, Obj.MemberID)
                DB.AddInParameter(DBComm, "@MemberName", DbType.String, Obj.MemberName)
                DB.AddInParameter(DBComm, "@MemberCode", DbType.String, Obj.MemberCode)
                DB.AddInParameter(DBComm, "@RedeemID", DbType.String, Obj.RedeemID)
                DB.AddInParameter(DBComm, "@TopupPoint", DbType.Int32, Obj.TopupPoint)
                DB.AddInParameter(DBComm, "@TopupValue", DbType.Int32, Obj.TopupValue)
                DB.AddInParameter(DBComm, "@RedeemPoint", DbType.Int32, Obj.RedeemPoint)
                DB.AddInParameter(DBComm, "@RedeemValue", DbType.Int32, Obj.RedeemValue)
                DB.AddInParameter(DBComm, "@IsRedeemInvoice", DbType.Boolean, Obj.IsRedeemInvoice)
                DB.AddInParameter(DBComm, "@MemberDis", DbType.Decimal, Obj.MemberDis)
                DB.AddInParameter(DBComm, "@MemberDiscountAmt", DbType.Int64, Obj.MemberDiscountAmt)
                DB.AddInParameter(DBComm, "@InvoiceStatus", DbType.Int32, Obj.InvoiceStatus)


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

        Public Function UpdateConsignmentSaleItem(ByVal ObjItem As CommonInfo.ConsignmentSaleItemInfo) As Boolean Implements IConsignmentSaleDA.UpdateConsignmentSaleItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ConsignmentSaleItem set  ConsignmentSaleID= @ConsignmentSaleID , ForSaleID= @ForSaleID , ItemCode= @ItemCode, ItemNameID= @ItemNameID , GoldQualityID= @GoldQualityID,ItemTG=@ItemTG, ItemTK= @ItemTK , GemsTK= @GemsTK , GemsTG= @GemsTG , WasteTK= @WasteTK , WasteTG= @WasteTG ,GoldTK= @GoldTK,GoldTG=@GoldTG,SalesRate=@SalesRate,GoldPrice=@GoldPrice,FixPrice=@FixPrice"
                strCommandText += " where ConsignmentSaleItemID= @ConsignmentSaleItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ObjItem.ConsignmentSaleID)
                DB.AddInParameter(DBComm, "@ForsaleID", DbType.String, ObjItem.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ObjItem.ItemCode)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ObjItem.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, ObjItem.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, ObjItem.ItemTG)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, ObjItem.ItemTK)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjItem.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjItem.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, ObjItem.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, ObjItem.WasteTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, ObjItem.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, ObjItem.GoldTG) 'Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, ObjItem.SalesRate)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, ObjItem.GoldPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, ObjItem.FixPrice)
                DB.AddInParameter(DBComm, "@ConsignmentSaleItemID", DbType.String, ObjItem.ConsignmentSaleItemID)


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

        Public Function GetBarcodeByConsignmentSaleID(ByVal argstr As String, Optional ByVal cristr As String = "", Optional ByVal ConsignmentSaleID As String = "") As CommonInfo.ConsignmentSaleItemInfo Implements IConsignmentSaleDA.GetBarcodeByConsignmentSaleID
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim drResult As IDataReader
            Dim objCSItem As New ConsignmentSaleItemInfo
            Dim strWhere As String

            If argstr <> "" Then
                strWhere = " WHERE IsExit=1 and ConsignmentSaleID='" & ConsignmentSaleID & "  ' " & cristr & " AND C.ItemCode NOT IN (" & argstr & ")"

            Else
                strWhere = " WHERE IsExit=1 and ConsignmentSaleID='" & ConsignmentSaleID & "  '  " & cristr

            End If
            Try

                strCommandText = " select *  from tbl_ConsignmentSaleItem C left join  tbl_ForSale F on C.ForSaleID=F.ForSaleID  " & strWhere


                DBComm = DB.GetSqlStringCommand(strCommandText)
                ''DB.AddInParameter(DBComm, "@LocationID", DbType.String, argLocationID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objCSItem
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemNameID = drResult("ItemNameID")
                        .GoldQualityID = drResult("GoldQualityID")
                        .IsReturn = drResult("IsReturn")
                        .IsSale = drResult("IsSale")
                        .ItemTG = drResult("ItemTG")
                        .SalesRate = drResult("SalesRate")
                        .ItemTK = drResult("ItemTK")
                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GoldPrice = drResult("GoldPrice")
                        .FixPrice = drResult("FixPrice")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objCSItem
        End Function

        Public Function GetBarcodeDataByConsignmentSaleID(ByVal argstr As String, Optional ByVal ConsignmentSaleID As String = "") As System.Data.DataTable Implements IConsignmentSaleDA.GetBarcodeDataByConsignmentSaleID
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim dtResult As DataTable

            Dim strWhere As String

            If argstr <> "" Then
                strWhere = " WHERE IsExit=1 and ConsignmentSaleID='" & ConsignmentSaleID & "'" & " AND C.ItemCode NOT IN (" & argstr & ")"

            Else
                strWhere = " WHERE IsExit=1 and C.IsReturn=0 and ConsignmentSaleID='" & ConsignmentSaleID & "'"

            End If
            Try

                strCommandText = " select C.ConsignmentSaleItemID,C.ConsignmentSaleID,C.ForSaleID,C.ItemNameID,C.GoldQualityID,C.ItemCode,C.IsReturn,C.IsSale,C.SalesRate,C.ItemTK,C.ItemTG,C.GemsTK,C.GemsTG,C.WasteTK,C.WasteTG,C.GoldTK,C.GoldTG," & _
                                " CAST((F.ItemTK-F.GemsTK) AS INT) AS GoldK, " & _
                                " CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                                " CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                                " CAST((((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC, " & _
                                " CAST(F.ItemTK AS INT) AS ItemK, CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                                " CAST(F.GemsTK AS INT) AS GemsK,CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT) AS GemsP,CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,CAST(((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,F.GemsTK,F.GemsTG," & _
                                " CAST(F.WasteTK AS INT) AS WasteK, CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT) AS WasteP,CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,CAST(((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC ," & _
                                " C.GoldPrice,C.FixPrice from tbl_ConsignmentSaleItem C left join  tbl_ForSale F on C.ForSaleID=F.ForSaleID  " & strWhere


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetItemCodeByConsignmentSaleID(Optional ByVal ConsignmentSaleID As String = "") As System.Data.DataTable Implements IConsignmentSaleDA.GetItemCodeByConsignmentSaleID
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim dtResult As DataTable

            Try

                'strCommandText = " select C.ConsignmentSaleItemID,C.ConsignmentSaleID,C.ForSaleID,C.ItemNameID,C.GoldQualityID,C.ItemCode,C.IsReturn,C.IsSale,C.SalesRate,C.ItemTK,C.ItemTG,C.GemsTK,C.GemsTG,C.WasteTK,C.WasteTG,C.GoldTK,C.GoldTG," & _
                '                " CAST((F.ItemTK-F.GemsTK) AS INT) AS GoldK, " & _
                '                " CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                '                " CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                '                " CAST((((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC, " & _
                '                " CAST(F.ItemTK AS INT) AS ItemK, CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                '                " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                '                " CAST(F.GemsTK AS INT) AS GemsK,CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT) AS GemsP,CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,CAST(((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,F.GemsTK,F.GemsTG," & _
                '                " CAST(F.WasteTK AS INT) AS WasteK, CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT) AS WasteP,CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,CAST(((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC ," & _
                '                " C.GoldPrice,C.FixPrice from tbl_ConsignmentSaleItem C left join  tbl_ForSale F on C.ForSaleID=F.ForSaleID   IsExit=1 and C.IsReturn=0 and ConsignmentSaleID='" & ConsignmentSaleID & "'"

                strCommandText = "select  ROW_NUMBER() OVER (ORDER BY WI.ForSaleID) AS SNo,ConsignmentSaleItemID as ConsignmentItemID,ConsignmentSaleID as ConsignmentID,'' as WholeSaleReturnID,'' as WholeSaleReturnItemID, " & _
                                "WI.ForSaleID as [ForSaleID],WI.ItemCode,WI.ItemTG as Gram, " & _
                                "WI.SalesRate,WI.ItemTK ,WI.ItemTG, WI.GemsTK , WI.GemsTG , WI.WasteTK , WI.WasteTG ,WI.GoldTK,WI.GoldTG,WI.GoldPrice as Amount,WI.GoldPrice,WI.FixPrice,0 as Pay,1 as Sale,WI.IsSale as IsSale,WI.IsReturn as IsReturn,'' as IsShowForReturn, " & _
                                "CAST((WI.ItemTK-WI.GemsTK) AS INT) AS GoldK,CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                "CAST(((((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16)-CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                "CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                "CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                "CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                "CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY, " & _
                                "CAST(WI.GemsTK AS INT) AS GemsK,CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                "CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY , " & _
                                "GQ.GoldQualityID,GQ.GoldQuality,GQ.IsGramRate,N.ItemName,WI.ItemNameID from tbl_ConsignmentSaleItem WI " & _
                                "LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=F.ItemCategoryID " & _
                                "LEFT JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID  LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                "where WI.ConsignmentSaleID=@ConsignmentSaleID and IsExit=1 and WI.IsReturn=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ConsignmentSaleID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function



        Public Function GetConsignmentSaleReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IConsignmentSaleDA.GetConsignmentSaleReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "SELECT WI.ConsignmentSaleItemID, WI.ConsignmentSaleID , WI.ForSaleID, WI.ItemCode,WI.IsReturn,WI.IsSale ,WI.ItemTG,W.MemberID,W.MemberCode,W.MemberName,W.RedeemValue,W.RedeemPoint,W.RedeemID,W.TopupPoint,W.TopupValue,W.MemberDis,W.MemberDiscountAmt,w.TransactionID,W.InvoiceStatus,W.IsRedeemInvoice, " & _
                                 " WI.SalesRate,f.IsDiamond,Case when (IsDiamond='1') then WI.FixPrice when (IsDiamond='0') then WI.GoldPrice End as Amount,W.ConsignDate, W.WholeSaleInvoiceID, Staff, CustomerName,W.NetAmount,W.CustomerID, W.AddOrSub,  W.Discount,W.PaidAmount," & _
                                 " CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,   " & _
                                 " CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,     " & _
                                 " CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP,   " & _
                                 " CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                 " CAST(WI.GoldTK AS INT) AS GoldK, CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT) AS GoldP,   " & _
                                 " CAST((((WI.GoldTK-CAST(WI.GoldTK AS INT))*16)-CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                 " CAST(WI.GemsTK AS INT) AS GemsK, CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP,   " & _
                                 " CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY, " & _
                                 " WI.WasteTK AS [@WasteTK], WI.WasteTG AS [@WasteTG], CONVERT(VARCHAR, CAST(WI.WasteTG AS DECIMAL(18,3)))  as WasteTG,   " & _
                                 " CONVERT(VARCHAR,CAST(WI.WasteTK AS INT)) AS WasteK,  CONVERT(VARCHAR,CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))  AS WasteP,  " & _
                                 " CONVERT(VARCHAR,CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2))) AS WasteY,   " & _
                                 " ItemCategory, OriginalCode, WI.GoldPrice, WI.FixPrice, GQ.GoldQuality " & _
                                 " FROM tbl_ConsignmentSaleItem WI  LEFT JOIN tbl_ConsignmentSale W ON W.ConsignmentSaleID=WI.ConsignmentSaleID    " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID   LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID  LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID  LEFT JOIN tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID " & _
                                 " WHERE W.IsDelete=0 AND W.ConsignDate  BETWEEN @FromDate And @ToDate " & GetFilterString

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

        Public Function GetConsignmentSaleReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IConsignmentSaleDA.GetConsignmentSaleReportAmount
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = " select sum(netamount) as WSNetAmount,customerid as WSCustomerID from  " & _
                                 " (Select distinct W.CustomerID, w.wholesaleinvoiceid,W.Netamount  FROM tbl_ConsignmentSaleItem WI  " & _
                                 " LEFT JOIN tbl_ConsignmentSale W ON W.ConsignmentSaleID=WI.ConsignmentSaleID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID " & _
                                 " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID  WHERE W.IsDelete=0 " & _
                                 " AND W.ConsignDate  BETWEEN @FromDate and @ToDate " & GetFilterString & " ) as tmp group by customerid Order by CustomerID asc"

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

        Public Function GetConsignmentSalePrint(ByVal ConsignmentSaleID As String) As System.Data.DataTable Implements IConsignmentSaleDA.GetConsignmentSalePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  C.ConsignmentSaleID, ConsignDate, Staff,  CustomerName,CustomerCode,CustomerTel, Address,NetAmount, AddOrSub, Discount,PaidAmount,   " & _
                                "ConsignmentSaleItemID,C.PurchaseHeaderID,C.PurchaseAmount,I.ForSaleID,N.ItemName,I.ItemCode,IsReturn as Pay,IsSale as Sale,I.ItemTK ,I.ItemTG, I.GemsTK , I.GemsTG , I.WasteTK , " & _
                                "I.WasteTG ,I.GoldTK,I.GoldTG,C.MemberID,C.MemberCode,C.MemberName,C.RedeemValue,C.RedeemPoint,C.RedeemID,C.TopupPoint,C.TopupValue,C.MemberDis,C.MemberDiscountAmt,C.TransactionID,C.InvoiceStatus,C.IsRedeemInvoice,0 as PointBalance, " & _
                                "I.SalesRate, I.GoldPrice,IC.ItemCategory , " & _
                                "CAST((I.ItemTK-I.GemsTK) AS INT) AS GoldK,CAST(((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                "CAST(((((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16)-CAST(((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                "CAST(I.WasteTK AS INT) AS WasteK, CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                "CAST((((I.WasteTK-CAST(I.WasteTK AS INT))*16)-CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY, " & _
                                "CAST(I.ItemTK AS INT) AS ItemK, CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                "CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY, " & _
                                "CAST(I.GemsTK AS INT) AS GemsK,CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                "CAST((((I.GemsTK-CAST(I.GemsTK AS INT))*16)-CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY , " & _
                                " CAST((I.ItemTK-I.GemsTK)+I.WasteTK AS INT) AS TotalK,  " & _
                                " CAST(((I.ItemTK-I.GemsTK)+I.WasteTK-CAST((I.ItemTK-I.GemsTK)+I.WasteTK AS INT))*16 AS INT) AS TotalP,   " & _
                                " CAST(((((I.ItemTK-I.GemsTK)+I.WasteTK-CAST((I.ItemTK-I.GemsTK)+I.WasteTK AS INT))*16)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK-CAST((I.ItemTK-I.GemsTK)+I.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS TotalY, " & _
                                "Location, GoldQuality,Q.IsGramRate,IsDiamond From tbl_ConsignmentSaleItem I LEFT JOIN tbl_ConsignmentSale C ON C.ConsignmentSaleID=I.ConsignmentSaleID " & _
                                "LEFT JOIN tbl_ForSale F ON I.ForSaleID=F.ForSaleID LEFT JOIN tbl_ItemCategory IC ON F.ItemCategoryID=IC.ItemCategoryID " & _
                                "LEFT JOIN tbl_ItemName N ON I.ItemNameID=N.ItemNameID   " & _
                                "LEFT JOIN tbl_Staff S ON C.StaffID=S.StaffID LEFT JOIN tbl_Customer CU ON CU.CustomerID=C.CustomerID  " & _
                                "LEFT JOIN tbl_Location L ON L.LocationID=C.LocationID  " & _
                                "LEFT JOIN tbl_GoldQuality Q ON Q.GoldQualityID=F.GoldQualityID  " & _
                                "where C.IsDelete = 0 And   I.ConsignmentSaleID=@ConsignmentSaleID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ConsignmentSaleID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements IConsignmentSaleDA.UpdateRedeemID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_ConsignmentSale SET RedeemID=@RedeemID WHERE  ConsignmentSaleID= @ConsignmentSaleID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, SaleInvoiceID)
                DB.AddInParameter(DBComm, "@RedeemID", DbType.String, RedeemID)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Update RedeemID for this invoice. ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean Implements IConsignmentSaleDA.UpdateTransactionID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_ConsignmentSale SET TransactionID=@TransactionID WHERE  ConsignmentSaleID= @ConsignmentSaleID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, SaleInvoiceID)
                DB.AddInParameter(DBComm, "@TransactionID", DbType.String, TransactionID)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Update transactionID for this invoice. ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
    End Class
End Namespace

