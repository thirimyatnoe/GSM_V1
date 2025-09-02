Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace WholeSaleInvoice
    Public Class WholeSaleInvoiceDA
        Implements IWholeSaleInvoiceDA

#Region "Private WholeSaleInvoice"

        Private DB As Database
        Private Shared ReadOnly _instance As IWholeSaleInvoiceDA = New WholeSaleInvoiceDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IWholeSaleInvoiceDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteWholeSaleInvoice(ByVal WholeSaleInvoiceID As String) As Boolean Implements IWholeSaleInvoiceDA.DeleteWholeSaleInvoice
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_WholesaleInvoice SET IsDelete =CONVERT(bit,1),IsUpload= CONVERT(bit,0),JC_IsUpload= CONVERT(bit,0),LastModifiedDate=GetDate() WHERE  WholeSaleInvoiceID= @WholeSaleInvoiceID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholeSaleInvoiceID)
             
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

        Public Function DeleteWholeSaleInvoiceItem(ByVal WholeSaleInvoiceItemID As String) As Boolean Implements IWholeSaleInvoiceDA.DeleteWholeSaleInvoiceItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_WholesaleInvoiceItem WHERE  WholeSaleInvoiceItemID= @WholeSaleInvoiceItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceItemID", DbType.String, WholeSaleInvoiceItemID)
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

        Public Function GetAllWholeSaleInvoice() As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetAllWholeSaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT WholesaleInvoiceID,convert(varchar(10),WDate,105) as WholeSaleDate,Case when PayType='0' then 'Credit' when PayType='1' then 'Pay' when PayType='2' then 'Cash'  End as PayType ,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,NetAmount,Discount,AddOrSub,PaidAmount " & _
                                  " From tbl_WholesaleInvoice WS Inner join tbl_Staff S On WS.StaffID=S.StaffID INNER Join tbl_Customer C on WS.CustomerID=C.CustomerID WHERE WS.IsDelete=0 and C.IsDelete=0 AND S.IsDelete=0 Order by WDate desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetWSInvoiceAndCSInvoice() As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWSInvoiceAndCSInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                'strCommandText = "  SELECT '-' as [ConsignmentSaleID],WholesaleInvoiceID,convert(varchar(10),WDate,103) as WholeSaleDate,WS.WDate as [@Wdate],Case when PayType='0' then 'Sale' when PayType='1' then 'Pay' when PayType='2' then 'Sale' when PayType='3' then 'Sale' End as PayType ,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,NetAmount,AddOrSub,Discount,PaidAmount    " & _
                '    " From tbl_WholesaleInvoice WS Left join tbl_Staff S On WS.StaffID=S.StaffID Left Join tbl_Customer C on WS.CustomerID=C.CustomerID " & _
                '    " where  WS.IsDelete=0 AND S.IsDelete=0 AND C.IsDelete=0  Union All " & _
                '    " SELECT ConsignmentSaleID as [ConsignmentSaleID],WholesaleInvoiceID as [WholesaleInvoiceID],convert(varchar(10),ConsignDate,105) as WholeSaleDate,WS.ConsignDate as [@Wdate],'Consignment' as PayType ,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,NetAmount ,AddOrSub,Discount,PaidAmount " & _
                '    " From tbl_ConsignmentSale WS Left join tbl_Staff S On WS.StaffID=S.StaffID Left Join tbl_Customer C on WS.CustomerID=C.CustomerID where WS.IsDelete=0 Order By [@Wdate] desc "

                strCommandText = "  SELECT '-' as [ConsignmentSaleID],WholesaleInvoiceID,convert(varchar(10),WDate,103) as WholeSaleDate,WS.WDate as [@Wdate],Case when PayType='0' then 'Sale' when PayType='1' then 'Pay' when PayType='2' then 'Sale' when PayType='3' then 'Sale' End as PayType ,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,NetAmount,AddOrSub,Discount,PaidAmount    " & _
                    " From tbl_WholesaleInvoice WS Left join tbl_Staff S On WS.StaffID=S.StaffID Left Join tbl_Customer C on WS.CustomerID=C.CustomerID " & _
                    " where  WS.IsDelete=0 AND S.IsDelete=0 AND C.IsDelete=0 " & _
                    " And WS.WholesaleInvoiceID not in (Select WholesaleInvoiceid from tbl_ConsignmentSale Where isDelete=0 ) " & _
                    " and ws.wholesaleinvoiceid in (select wholesaleinvoiceid from tbl_wholesaleinvoiceitem where isreturn=0 ) order by WDate asc "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetWSInvoice() As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWSInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "  SELECT '-' as [ConsignmentSaleID],WholesaleInvoiceID,convert(varchar(10),WDate,103) as WholeSaleDate,WS.WDate as [@Wdate],Case when PayType='0' then 'Sale' when PayType='1' then 'Pay' when PayType='2' then 'Sale' when PayType='3' then 'Sale' End as PayType ,S.StaffID as [@StaffID],S.Staff,C.CustomerID as [@CustomerID],C.CustomerCode,C.CustomerName,NetAmount,AddOrSub,Discount,PaidAmount    " & _
                    " From tbl_WholesaleInvoice WS Left join tbl_Staff S On WS.StaffID=S.StaffID Left Join tbl_Customer C on WS.CustomerID=C.CustomerID " & _
                    " where  WS.IsDelete=0 AND S.IsDelete=0 AND C.IsDelete=0 Order By [@Wdate] desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetWholeSaleInvoiceByID(ByVal WholeSaleInvoiceID As String) As CommonInfo.WholeSaleInvoiceInfo Implements IWholeSaleInvoiceDA.GetWholeSaleInvoiceByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New WholeSaleInvoiceInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_WholesaleInvoice WHERE WholeSaleInvoiceID= @WholeSaleInvoiceID AND IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholeSaleInvoiceID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .WholesaleInvoiceID = drResult("WholesaleInvoiceID")
                        .WDate = drResult("WDate")
                        .StaffID = drResult("StaffID")
                        .CustomerID = drResult("CustomerID")
                        .NetAmount = drResult("NetAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .Discount = drResult("Discount")
                        .PaidAmount = drResult("PaidAmount")
                        .DueDate = drResult("DueDate")
                        .PayType = drResult("PayType")
                        .Discount = drResult("Discount")
                        .Remark = drResult("Remark")
                        .DesignCharges = drResult("TotalDesignCharges")
                        .DisPercent = drResult("DisPercent")
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

        Public Function GetWholeSaleInvoiceItem(ByVal WholesaleInvoiceItemID As String) As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSaleInvoiceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * " & _
                                " From tbl_WholesaleInvoiceItem where WholeSaleInvoiceID=@WholeSaleInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholesaleInvoiceItemID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetWholeSaleInvoiceItemByID(ByVal WholesaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSaleInvoiceItemByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  ROW_NUMBER() OVER (ORDER BY D.WholesaleInvoiceItemID) AS SNo,D.WholesaleInvoiceItemID,D.ForSaleID as [@ForSaleID], " & _
                                 "D.ItemCode as BarcodeNo,IsReturn as Pay,IsSale as Sale,D.ItemTG as Gram,SalesRate as Rate,D.GoldPrice as Amount , " & _
                                 "D.SalesRate, D.ItemTK ,D.ItemTG, D.GemsTK , " & _
                                 "D.GemsTG , D.WasteTK , D.WasteTG ,D.GoldTK,D.GoldTG, D.GoldPrice , D.FixPrice ,CAST((D.ItemTK-D.GemsTK) AS INT) AS GoldK,CAST(((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                 "CAST(((((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16)-CAST(((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                 "CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 "CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                "CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                "CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,CAST(D.GemsTK AS INT) AS GemsK,CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS GemsP,CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY ,GQ.GoldQualityID,GQ.GoldQuality ,N.ItemName,D.ItemNameID,D.GoldQualityID,D.DesignCharges,D.DesignChargesRate,D.ItemDisPercent as DisPercent,D.ItemDisAmount as DisAmount,D.GemsPrice " & _
                                "From tbl_WholesaleInvoiceItem D   LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID " & _
                                "LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=F.ItemCategoryID " & _
                                "LEFT JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID  " & _
                                "LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID   where WholeSaleInvoiceID=@WholeSaleInvoiceID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholesaleInvoiceID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertWholeSaleInvoice(ByVal obj As CommonInfo.WholeSaleInvoiceInfo) As Boolean Implements IWholeSaleInvoiceDA.InsertWholeSaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_WholesaleInvoice ( WholesaleInvoiceID,WDate,StaffID,CustomerID,NetAmount,AddOrSub,Discount,PaidAmount,DueDate,PayType,LastModifiedLoginUserName,LastModifiedDate,LocationID,IsUpload,IsDelete,JC_IsUpload,Remark,TotalDesignCharges,DisPercent,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus)"
                strCommandText += " Values (@WholesaleInvoiceID,@WDate,@StaffID,@CustomerID,@NetAmount,@AddOrSub,@Discount,@PaidAmount,@DueDate,@PayType,@LastModifiedLoginUserName,@LastModifiedDate,@LocationID,CONVERT(bit,0),CONVERT(bit,0),CONVERT(bit,0),@Remark,@TotalDesignCharges,@DisPercent,@MemberID,@MemberName,@MemberCode,@RedeemID,@TopupPoint,@TopupValue,@RedeemPoint,@RedeemValue,@IsRedeemInvoice,@MemberDis,@MemberDiscountAmt,@InvoiceStatus)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceID", DbType.String, obj.WholesaleInvoiceID)
                DB.AddInParameter(DBComm, "@WDate", DbType.DateTime, obj.WDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, obj.CustomerID)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int32, obj.NetAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, obj.AddOrSub)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, obj.Discount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, obj.DueDate)
                DB.AddInParameter(DBComm, "@PayType", DbType.Int32, obj.PayType)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@TotalDesignCharges", DbType.String, obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DisPercent", DbType.String, obj.DisPercent)
                DB.AddInParameter(DBComm, "@MemberID", DbType.String, obj.MemberID)
                DB.AddInParameter(DBComm, "@MemberName", DbType.String, obj.MemberName)
                DB.AddInParameter(DBComm, "@MemberCode", DbType.String, obj.MemberCode)
                DB.AddInParameter(DBComm, "@RedeemID", DbType.String, obj.RedeemID)
                DB.AddInParameter(DBComm, "@TopupPoint", DbType.Int32, obj.TopupPoint)
                DB.AddInParameter(DBComm, "@TopupValue", DbType.Int32, obj.TopupValue)
                DB.AddInParameter(DBComm, "@RedeemPoint", DbType.Int32, obj.RedeemPoint)
                DB.AddInParameter(DBComm, "@RedeemValue", DbType.Int32, obj.RedeemValue)
                DB.AddInParameter(DBComm, "@IsRedeemInvoice", DbType.Boolean, obj.IsRedeemInvoice)
                DB.AddInParameter(DBComm, "@MemberDis", DbType.Decimal, obj.MemberDis)
                DB.AddInParameter(DBComm, "@MemberDiscountAmt", DbType.Int64, obj.MemberDiscountAmt)
                DB.AddInParameter(DBComm, "@InvoiceStatus", DbType.Int32, obj.InvoiceStatus)
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

        Public Function InsertWholeSaleInvoiceItem(ByVal obj As CommonInfo.WholeSaleInvoiceItemInfo) As Boolean Implements IWholeSaleInvoiceDA.InsertWholeSaleInvoiceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_WholesaleInvoiceItem ( WholesaleInvoiceItemID,WholesaleInvoiceID,ForSaleID,ItemNameID,GoldQualityID,ItemCode,IsReturn,IsSale,SalesRate,ItemTK,ItemTG,GemsTK,GemsTG,WasteTK,WasteTG,GoldTK,GoldTG,GoldPrice,FixPrice,DesignCharges,DesignChargesRate,ItemDisPercent,ItemDisAmount,GemsPrice)"
                strCommandText += " Values (@WholesaleInvoiceItemID,@WholesaleInvoiceID,@ForSaleID,@ItemNameID,@GoldQualityID,@ItemCode,@IsReturn,@IsSale,@SalesRate,@ItemTK,@ItemTG,@GemsTK,@GemsTG,@WasteTK,@WasteTG,@GoldTK,@GoldTG,@GoldPrice,@FixPrice,@DesignCharges,@DesignChargesRate,@ItemDisPercent,@ItemDisAmount,@GemsPrice)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceItemID", DbType.String, obj.WholesaleInvoiceItemID)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceID", DbType.String, obj.WholesaleInvoiceID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, obj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, obj.IsReturn)
                DB.AddInParameter(DBComm, "@IsSale", DbType.Boolean, obj.IsSale)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int32, obj.SalesRate)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, obj.ItemTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, obj.GoldTG)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int32, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int32, obj.FixPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int32, obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int32, obj.DesignChargesRate)
                DB.AddInParameter(DBComm, "@ItemDisPercent", DbType.Int32, obj.ItemDisPercent)
                DB.AddInParameter(DBComm, "@ItemDisAmount", DbType.Int32, obj.ItemDisAmount)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int32, obj.GemsPrice)

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

        Public Function UpdateWholeSaleInvoice(ByVal obj As CommonInfo.WholeSaleInvoiceInfo) As Boolean Implements IWholeSaleInvoiceDA.UpdateWholeSaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_WholesaleInvoice set  WholesaleInvoiceID= @WholesaleInvoiceID , WDate= @WDate , StaffID= @StaffID , CustomerID= @CustomerID , NetAmount= @NetAmount , AddOrSub= @AddOrSub , Discount= @Discount , PaidAmount= @PaidAmount , DueDate= @DueDate , PayType= @PayType , LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate , LocationID= @LocationID ,IsUpload= CONVERT(bit,0),JC_IsUpload= CONVERT(bit,0),Remark=@Remark,DisPercent=@DisPercent,MemberID=@MemberID,MemberName=@MemberName,MemberCode=@MemberCode,RedeemID=@RedeemID,TopupPoint=@TopupPoint,TopupValue=@TopupValue,RedeemPoint=@RedeemPoint,RedeemValue=@RedeemValue,IsRedeemInvoice=@IsRedeemInvoice,MemberDis=@MemberDis,MemberDiscountAmt=@MemberDiscountAmt,InvoiceStatus=@InvoiceStatus "
                strCommandText += " where WholesaleInvoiceID= @WholesaleInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceID", DbType.String, obj.WholesaleInvoiceID)
                DB.AddInParameter(DBComm, "@WDate", DbType.DateTime, obj.WDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, obj.CustomerID)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int32, obj.NetAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, obj.AddOrSub)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, obj.Discount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, obj.DueDate)
                DB.AddInParameter(DBComm, "@PayType", DbType.Int32, obj.PayType)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "DisPercent", DbType.Int32, obj.DisPercent)
                DB.AddInParameter(DBComm, "@MemberID", DbType.String, obj.MemberID)
                DB.AddInParameter(DBComm, "@MemberName", DbType.String, obj.MemberName)
                DB.AddInParameter(DBComm, "@MemberCode", DbType.String, obj.MemberCode)
                DB.AddInParameter(DBComm, "@RedeemID", DbType.String, obj.RedeemID)
                DB.AddInParameter(DBComm, "@TopupPoint", DbType.Int32, obj.TopupPoint)
                DB.AddInParameter(DBComm, "@TopupValue", DbType.Int32, obj.TopupValue)
                DB.AddInParameter(DBComm, "@RedeemPoint", DbType.Int32, obj.RedeemPoint)
                DB.AddInParameter(DBComm, "@RedeemValue", DbType.Int32, obj.RedeemValue)
                DB.AddInParameter(DBComm, "@IsRedeemInvoice", DbType.Boolean, obj.IsRedeemInvoice)
                DB.AddInParameter(DBComm, "@MemberDis", DbType.Decimal, obj.MemberDis)
                DB.AddInParameter(DBComm, "@MemberDiscountAmt", DbType.Int64, obj.MemberDiscountAmt)
                DB.AddInParameter(DBComm, "@InvoiceStatus", DbType.Int32, obj.InvoiceStatus)

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

        Public Function UpdateWholeSaleInvoiceItem(ByVal obj As CommonInfo.WholeSaleInvoiceItemInfo) As Boolean Implements IWholeSaleInvoiceDA.UpdateWholeSaleInvoiceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_WholesaleInvoiceItem set  WholesaleInvoiceItemID= @WholesaleInvoiceItemID , WholesaleInvoiceID= @WholesaleInvoiceID , ForSaleID= @ForSaleID ,ItemNameID=@ItemNameID,GoldQualityID=@GoldQualityID, Itemcode= @Itemcode , IsReturn= @IsReturn , IsSale= @IsSale  , SalesRate= @SalesRate,ItemTK=@ItemTK,ItemTG=@ItemTG,GemsTK=GemsTK,GemsTG=@GemsTG,WasteTK=@WasteTK,WasteTG=@WasteTG , GoldTK=@GoldTK,GoldTG=@GoldTG,GoldPrice=@GoldPrice,FixPrice=@FixPrice,DesignCharges=@DesignCharges,DesignChargesRate=@DesignChargesRate,ItemDisPercent=@ItemDisPercent,ItemDisAmount=@ItemDisAmount,GemsPrice=@GemsPrice "
                strCommandText += " where WholesaleInvoiceItemID= @WholesaleInvoiceItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceItemID", DbType.String, obj.WholesaleInvoiceItemID)
                DB.AddInParameter(DBComm, "@WholesaleInvoiceID", DbType.String, obj.WholesaleInvoiceID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, obj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, obj.IsReturn)
                DB.AddInParameter(DBComm, "@IsSale", DbType.Boolean, obj.IsSale)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int32, obj.SalesRate)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, obj.ItemTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, obj.GoldTG)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int32, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int32, obj.FixPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int32, obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int32, obj.DesignChargesRate)
                DB.AddInParameter(DBComm, "@ItemDisPercent", DbType.Int32, obj.ItemDisPercent)
                DB.AddInParameter(DBComm, "@ItemDisAmount", DbType.Int32, obj.ItemDisAmount)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int32, obj.GemsPrice)
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
        Public Function GetBarcodeByWholeSaleID(ByVal argstr As String, Optional ByVal cristr As String = "", Optional ByVal WholeSaleID As String = "") As CommonInfo.WholeSaleInvoiceItemInfo Implements IWholeSaleInvoiceDA.GetBarcodeByWholeSaleID
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim drResult As IDataReader
            Dim objWSItem As New WholeSaleInvoiceItemInfo
            Dim strWhere As String

            If argstr <> "" Then
                strWhere = " WHERE isReturn=0 And  WholeSaleInvoiceID='" & WholeSaleID & "  ' " & cristr & " AND ItemCode NOT IN (" & argstr & ")"

            Else
                strWhere = " WHERE isReturn=0 And  WholeSaleInvoiceID='" & WholeSaleID & "  '  " & cristr
            End If
            Try

                strCommandText = " select * from tbl_WholeSaleInvoiceItem " & strWhere

                DBComm = DB.GetSqlStringCommand(strCommandText)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objWSItem
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemNameID = drResult("ItemNameID")
                        .GoldQualityID = drResult("GoldQualityID")
                        .SalesRate = drResult("SalesRate")
                        .ItemTK = drResult("ItemTK")
                        .ItemTG = drResult("ItemTG")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .GoldPrice = drResult("GoldPrice")
                        .FixPrice = drResult("FixPrice")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objWSItem
        End Function

        Public Function GetBarcodeDataByWholeSaleID(ByVal argstr As String, Optional ByVal WholeSaleID As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetBarcodeDataByWholeSaleID
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim dtResult As DataTable

            Dim strWhere As String

            If argstr <> "" Then
                strWhere = " WHERE WholeSaleInvoiceID='" & WholeSaleID & "'" & " AND ItemCode NOT IN (" & argstr & ")"

            Else
                strWhere = " WHERE IsReturn=0 And WholeSaleInvoiceID='" & WholeSaleID & "'"
            End If
            Try

                strCommandText = " select WholesaleInvoiceID,ItemCode,WholesaleInvoiceItemID,SalesRate,ForSaleID,ItemNameID,GoldQualityID,IsReturn,IsSale,ItemTK,ItemTG,GemsTK,GemsTG,WasteTK,WasteTG,GoldTK,GoldTG," & _
                                " CAST((ItemTK-GemsTK) AS INT) AS GoldK, " & _
                                " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                                " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                                " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC, " & _
                                " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                                " CAST(GemsTK AS INT) AS GemsK,CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,GemsTK,GemsTG," & _
                                " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC, " & _
                                " GoldPrice,FixPrice,0 as Pay,1 as Sale,'' as SNo  from tbl_WholeSaleInvoiceItem " & strWhere
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholeSaleID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function
        Public Function GetItemCodeByWholeSaleID(Optional ByVal WholeSaleID As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetItemCodeByWholeSaleID
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim dtResult As DataTable

            Try

                strCommandText = "select ROW_NUMBER() OVER (ORDER BY WI.ForSaleID) AS SNo,'' as WholeSaleReturnID,'' as WholeSaleReturnItemID,'' as ConsignmentItemID,'' as ConsignmentID, " & _
                                 "WI.ForSaleID as [ForSaleID],WI.ItemCode,WI.WholeSaleInvoiceID,WI.ItemTG as Gram, " & _
                                 "WI.SalesRate,WI.ItemTK ,WI.ItemTG, WI.GemsTK , WI.GemsTG , WI.WasteTK , WI.WasteTG ,WI.GoldTK,WI.GoldTG,WI.GoldPrice as Amount,WI.GoldPrice,WI.FixPrice,0 as Pay,1 as Sale,'' as IsSale,'' as IsReturn,'' as IsShowForReturn, " & _
                                 "CAST((WI.ItemTK-WI.GemsTK) AS INT) AS GoldK,CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                 "CAST(((((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16)-CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                 "CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                 "CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                 "CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                 "CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY, " & _
                                 "CAST(WI.GemsTK AS INT) AS GemsK,CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                 "CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY , " & _
                                 "GQ.GoldQualityID,GQ.GoldQuality,GQ.IsGramRate,N.ItemName,WI.ItemNameID,WI.GoldQualityID  from tbl_WholesaleInvoiceItem WI " & _
                                 "LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=F.ItemCategoryID " & _
                                 "LEFT JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID  LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                 "where WI.IsReturn=0 and WI.WholeSaleInvoiceID=@WholesaleInvoiceID  and WI.ItemCode Not In " & _
                                 "(select SI.ItemCode from tbl_ConsignmentSale S  left join tbl_ConsignmentSaleItem SI on S.ConsignmentSaleID=SI.ConsignmentSaleID " & _
                                 " where S.WholesaleInvoiceID=@WholesaleInvoiceID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholeSaleID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetBarcodeDataByWholesaleInvoiceID(ByVal WholesaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetBarcodeDataByWholesaleInvoiceID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select  ROW_NUMBER() OVER (ORDER BY WI.ForSaleID) AS SNo,'' as ConsignmentItemID,'' as ConsignmentID, " & _
                                "WI.ForSaleID as [@ForSaleID],WI.ItemCode,WI.WholeSaleInvoiceID,WI.ItemTG as Gram, " & _
                                "WI.SalesRate,WI.ItemTK ,WI.ItemTG, WI.GemsTK , WI.GemsTG , WI.WasteTK , WI.WasteTG ,WI.GoldTK,WI.GoldTG,WI.GoldPrice as Amount,WI.GoldPrice,WI.FixPrice,0 as Pay,1 as Sale, " & _
                                "CAST((WI.ItemTK-WI.GemsTK) AS INT) AS GoldK,CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                "CAST(((((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16)-CAST(((WI.ItemTK-WI.GemsTK)-CAST((WI.ItemTK-WI.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                "CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                "CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                "CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                "CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY, " & _
                                "CAST(WI.GemsTK AS INT) AS GemsK,CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                "CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY , " & _
                                "GQ.GoldQualityID,GQ.GoldQuality ,N.ItemName,WI.ItemNameID,WI.GoldQualityID  from tbl_WholesaleInvoiceItem WI " & _
                                "LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=F.ItemCategoryID " & _
                                "LEFT JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID  LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                "where WI.WholeSaleInvoiceID=@WholesaleInvoiceID  and WI.ItemCode Not In " & _
                                "(select WR.ItemCode from tbl_WholesaleReturn W  left join tbl_WholesaleReturnItem WR on W.WholesaleReturnID=WR.WholesaleReturnID " & _
                                " where W.WholesaleInvoiceID=@WholesaleInvoiceID and W.isDelete=0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholesaleInvoiceID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllWholesaleInvoiceByConsignmentType() As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetAllWholesaleInvoiceByConsignmentType
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT WS.WholesaleInvoiceID,convert(varchar(10),WS.WDate,105) as WholesaleDate,S.StaffID as [@StaffID],S.Staff, " & _
                                " C.CustomerID as [@CustomerID], C.CustomerCode,C.CustomerName,WS.NetAmount,WS.AddOrSub,WS.Discount,WS.PaidAmount " & _
                                " From tbl_WholesaleInvoice WS " & _
                                " INNER join tbl_Staff S On WS.StaffID=S.StaffID  INNER Join tbl_Customer C on WS.CustomerID=C.CustomerID  " & _
                                " where WS.IsDelete=0 AND S.IsDelete=0 AND C.IsDelete=0 AND WS.PayType='1' and " & _
                                " WS.WholesaleInvoiceID Not In (select WholesaleInvoiceID  from tbl_ConsignmentSale where IsDelete=0) " & _
                                " and ws.wholesaleinvoiceid in (select wholesaleinvoiceid from tbl_wholesaleinvoiceitem where isreturn=0 ) Order by WS.WDate asc"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSaleInvoiceForCommissionReport(ByVal WDate As Date, ByVal CustomerID As String, ByVal PayType As Integer, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSaleInvoiceForCommissionReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If cristr <> "" Then
                    strCommandText = "select W.WholeSaleInvoiceID,W.WDate,W.TotalPayment,W.AddOrSub,W.PaidAmount,C.CustomerCode, " & _
                        " C.CustomerName,S.Staff from tbl_WholeSaleInvoice W Left join tbl_Customer C on W.CustomerID=C.CustomerID " & _
                        " left join tbl_Staff S on W.StaffID=S.StaffID " & _
                        " where W.PayType = @PayType and  Month(WDate) = Month(@WDate) "

                Else
                    strCommandText = "select W.WholeSaleInvoiceID,W.WDate,W.TotalPayment,W.AddOrSub,W.PaidAmount,C.CustomerCode, " & _
                      " C.CustomerName,S.Staff from tbl_WholeSaleInvoice W Left join tbl_Customer C on W.CustomerID=C.CustomerID " & _
                      " left join tbl_Staff S on W.StaffID=S.StaffID " & _
                      " where W.PayType = @PayType and W.CustomerID=@CustomerID and Month(WDate) = Month(@WDate) "

                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WDate", DbType.Date, WDate)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, CustomerID)
                DB.AddInParameter(DBComm, "@PayType", DbType.Int32, PayType)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSalePrint(ByVal WholesaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSalePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  W.WholeSaleInvoiceID, WDate, W.StaffID, Staff, W.CustomerID,  C.CustomerName,C.CustomerCode, C.CustomerAddress,C.CustomerTel,NetAmount, " & _
                                    "AddOrSub,Discount, PaidAmount, DueDate,N.ItemName,W.Remark,  " & _
                                    "Case PayType When 0 Then 'Credit' When 1 Then 'Consignment' When 2 Then 'Cash'  END As PayType, " & _
                                    "WholesaleInvoiceItemID,D.ForSaleID,D.ItemCode,IsReturn as Pay,IsSale as Sale,D.ItemTG,SalesRate, " & _
                                    "IC.ItemCategory,GQ.GoldQuality,GQ.GoldQualityID,IsGramRate ,IsDiamond,D.ItemTK , D.GemsTK , D.GemsTG , D.WasteTK , D.WasteTG,D.GoldTG, D.GoldPrice , " & _
                                    "D.FixPrice ,CAST((D.ItemTK-D.GemsTK) AS INT) AS GoldK,CAST(((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                                    "CAST(((((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16)-CAST(((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  " & _
                                    "CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                    "CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                    "CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                    "CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY, " & _
                                    "CAST(D.GemsTK AS INT) AS GemsK,CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                    "CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY ," & _
                                    "CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT) AS TotalK,  CAST(((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16 AS INT) AS TotalP,  " & _
                                    " CAST(((((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16)-CAST(((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,D.DesignCharges,D.DesignChargesRate,W.DisPercent as DisPercent,D.ItemDisPercent,D.ItemDisAmount,F.Length,F.Width,W.MemberID,W.MemberCode,W.MemberName,W.RedeemPoint,W.RedeemValue,W.RedeemID,W.TopupPoint,W.TopupValue,W.IsRedeemInvoice,W.TransactionID,W.MemberDis,W.MemberDiscountAmt,W.InvoiceStatus,0 as PointBalance " & _
                                    "From tbl_WholesaleInvoiceItem D LEFT JOIN tbl_WholeSaleInvoice W ON W.WholesaleInvoiceID=D.WholesaleInvoiceID " & _
                                    "LEFT JOIN tbl_ForSale F ON D.ForSaleID=F.ForSaleID LEFT JOIN tbl_ItemCategory IC ON F.ItemCategoryID=IC.ItemCategoryID  " & _
                                    "LEFT JOIN tbl_ItemName N ON D.ItemNameID=N.ItemNameID  LEFT JOIN tbl_GoldQuality GQ ON F.GoldQualityID=GQ.GoldQualityID  LEFT JOIN tbl_Staff S ON W.StaffID=S.StaffID " & _
                                    "LEFT JOIN tbl_Customer C ON W.CustomerID=C.CustomerID  LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID  " & _
                                    " where W.WholeSaleInvoiceID=@WholeSaleInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, WholesaleInvoiceID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSaleReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSaleReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "SELECT WI.WholesaleInvoiceItemID, WI.WholesaleInvoiceID As WholeSaleInvoiceID, WI.ForSaleID, WI.ItemCode,WI.IsReturn as Pay," & _
                                 " WI.IsSale as Sale ,WI.ItemTG, WI.SalesRate,   W.WDate, Staff, CustomerName,W.CustomerID,I.ItemNameID,I.ItemName,  W.NetAmount, W.AddOrSub, " & _
                                 " W.PaidAmount,W.Discount, Case PayType When 0 Then 'Credit' When 1 Then 'Consignment' When 2 Then 'Cash' When 3 Then 'Bank' END As PayType, " & _
                                 " Location, ItemCategory, OriginalCode , " & _
                                 " CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                 " CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,    " & _
                                 " CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 " CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " CAST(WI.GoldTK AS INT) AS GoldK, CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT) AS GoldP,  " & _
                                 " CAST((((WI.GoldTK-CAST(WI.GoldTK AS INT))*16)-CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY, " & _
                                 " CAST(WI.GemsTK AS INT) AS GemsK, CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY, WI.WasteTK AS [@WasteTK], WI.WasteTG AS [@WasteTG], CONVERT(VARCHAR, CAST(WI.WasteTG AS DECIMAL(18,3))) " & _
                                 " as WasteTG,   CONVERT(VARCHAR,CAST(WI.WasteTK AS INT)) AS WasteK,  CONVERT(VARCHAR,CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT)) " & _
                                 " AS WasteP,  CONVERT(VARCHAR,CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2))) AS WasteY,GQ.GoldQuality ,WI.GoldPrice,WI.FixPrice,W.NetAmount as TotalPayment,(W.AddOrSub+W.Discount) as Discount,W.MemberDiscountAmt,W.MemberDis,W.RedeemValue,W.RedeemPoint,W.RedeemID,W.TopupPoint,W.TopupValue,W.TransactionID,W.InvoiceStatus,W.MemberId,W.MemberCode,W.MemberName " & _
                                 " FROM tbl_WholesaleInvoiceItem WI " & _
                                 " LEFT JOIN tbl_WholesaleInvoice W ON W.WholesaleInvoiceID=WI.WholesaleInvoiceID   " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID    " & _
                                 " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID    " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemName I on I.ItemNameID=WI.ItemNameID  " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID  " & _
                                 " WHERE W.IsDelete=0 AND W.WDate BETWEEN @FromDate And @ToDate " & GetFilterString

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
        Public Function GetWholeSaleReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSaleReportAmount
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = " select sum(netamount) as WSNetAmount,customerid as WSCustomerID from " & _
                                 " (Select distinct W.CustomerID, w.wholesaleinvoiceid,W.Netamount" & _
                                 " FROM tbl_WholesaleInvoice W " & _
                                 " LEFT JOIN tbl_WholesaleInvoiceItem WI ON W.WholesaleInvoiceID=WI.WholesaleInvoiceID   " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID    " & _
                                 " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID    " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemName I on I.ItemNameID=WI.ItemNameID  " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID  " & _
                                 " WHERE W.IsDelete=0 AND W.WDate BETWEEN @FromDate And @ToDate " & GetFilterString & " ) as tmp group by customerid Order by CustomerID asc"

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

        Public Function GetConsignBalanceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetConsignBalanceReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                'strCommandText = "select  WS.WDate,WS.WholesaleInvoiceID,WI.ForSaleID,WS.LocationID,WI.BarcodeNo,WI.TotalG as Gram,I.ItemCategoryID, " & _
                '                "WI.PaidRate as Rate,Cast(WI.TotalG*WI.PaidRate as Int) as Amount,L.Location,C.CustomerName,I.ItemCategory From tbl_WholesaleInvoice WS " & _
                '                "Inner Join  tbl_WholesaleInvoiceItem WI  on WS.WholesaleInvoiceID=WI.WholesaleInvoiceID " & _
                '                "Left Join tbl_Location L on WS.LocationID=L.LocationID  " & _
                '                "Left Join tbl_Customer C on WS.CustomerID=C.CustomerID  " & _
                '                "Left Join tbl_ForSale F on WI.ForSaleID=F.ForSaleID  " & _
                '                "Left Join tbl_ItemCategory I On F.ItemCategoryID=I.ItemCategoryID " & _
                '                "WHERE WS.IsDelete=0 AND WS.WDate BETWEEN '" & FromDate & " 00:00:00' AND '" & ToDate & " 23:59:59'" & GetFilterString & _
                '                "AND WS.WholesaleInvoiceID Not In (select WholesaleInvoiceID  from tbl_ConsignmentSale where IsDelete=0)  " & _
                '                "and WS.IsDelete=0 AND WS.PayType='1'  and WI.BarcodeNo Not In  " & _
                '                "(select WR.BarcodeNo from tbl_WholesaleReturn W  left join tbl_WholesaleReturnItem WR " & _
                '                " on W.WholesaleReturnID=WR.WholesaleReturnID where W.IsDelete=0) "

                strCommandText = "SELECT WI.WholesaleInvoiceItemID, WI.WholesaleInvoiceID As WholeSaleInvoiceID, WI.ForSaleID, WI.ItemCode,WI.IsReturn as Pay," & _
                                 " WI.IsSale as Sale ,WI.ItemTG, WI.SalesRate,   W.WDate, Staff, CustomerName,W.CustomerID,I.ItemNameID,I.ItemName,  W.NetAmount, W.AddOrSub, " & _
                                 " W.PaidAmount,W.Discount, Case PayType When 0 Then 'Credit' When 1 Then 'Consignment' When 2 Then 'Cash' When 3 Then 'Bank' END As PayType, " & _
                                 " Location, ItemCategory, OriginalCode , " & _
                                 " CAST(WI.ItemTK AS INT) AS ItemK, CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                 " CAST((((WI.ItemTK-CAST(WI.ItemTK AS INT))*16)-CAST((WI.ItemTK-CAST(WI.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,    " & _
                                 " CAST(WI.WasteTK AS INT) AS WasteK, CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 " CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " CAST(WI.GoldTK AS INT) AS GoldK, CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT) AS GoldP,  " & _
                                 " CAST((((WI.GoldTK-CAST(WI.GoldTK AS INT))*16)-CAST((WI.GoldTK-CAST(WI.GoldTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY, " & _
                                 " CAST(WI.GemsTK AS INT) AS GemsK, CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((WI.GemsTK-CAST(WI.GemsTK AS INT))*16)-CAST((WI.GemsTK-CAST(WI.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GemsY, WI.WasteTK AS [@WasteTK], WI.WasteTG AS [@WasteTG], CONVERT(VARCHAR, CAST(WI.WasteTG AS DECIMAL(18,3))) " & _
                                 " as WasteTG,   CONVERT(VARCHAR,CAST(WI.WasteTK AS INT)) AS WasteK,  CONVERT(VARCHAR,CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT)) " & _
                                 " AS WasteP,  CONVERT(VARCHAR,CAST((((WI.WasteTK-CAST(WI.WasteTK AS INT))*16)-CAST((WI.WasteTK-CAST(WI.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2))) AS WasteY,GQ.GoldQuality ,WI.GoldPrice,WI.FixPrice,W.NetAmount as TotalPayment " & _
                                 " FROM tbl_WholesaleInvoiceItem WI " & _
                                 " LEFT JOIN tbl_WholesaleInvoice W ON W.WholesaleInvoiceID=WI.WholesaleInvoiceID   " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID    " & _
                                 " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID    " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemName I on I.ItemNameID=WI.ItemNameID  " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID  " & _
                                 " WHERE W.IsDelete=0 AND W.WDate BETWEEN @FromDate And @ToDate " & GetFilterString & _
                                 " AND W.WholesaleInvoiceID Not In (select WholesaleInvoiceID  from tbl_ConsignmentSale where IsDelete=0) And " & _
                                 " WI.isreturn=0 "



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
        Public Function GetConsignBalanceReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetConsignBalanceReportAmount
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                'strCommandText = "select  WS.WDate,WS.WholesaleInvoiceID,WI.ForSaleID,WS.LocationID,WI.BarcodeNo,WI.TotalG as Gram,I.ItemCategoryID, " & _
                '                "WI.PaidRate as Rate,Cast(WI.TotalG*WI.PaidRate as Int) as Amount,L.Location,C.CustomerName,I.ItemCategory From tbl_WholesaleInvoice WS " & _
                '                "Inner Join  tbl_WholesaleInvoiceItem WI  on WS.WholesaleInvoiceID=WI.WholesaleInvoiceID " & _
                '                "Left Join tbl_Location L on WS.LocationID=L.LocationID  " & _
                '                "Left Join tbl_Customer C on WS.CustomerID=C.CustomerID  " & _
                '                "Left Join tbl_ForSale F on WI.ForSaleID=F.ForSaleID  " & _
                '                "Left Join tbl_ItemCategory I On F.ItemCategoryID=I.ItemCategoryID " & _
                '                "WHERE WS.IsDelete=0 AND WS.WDate BETWEEN '" & FromDate & " 00:00:00' AND '" & ToDate & " 23:59:59'" & GetFilterString & _
                '                "AND WS.WholesaleInvoiceID Not In (select WholesaleInvoiceID  from tbl_ConsignmentSale where IsDelete=0)  " & _
                '                "and WS.IsDelete=0 AND WS.PayType='1'  and WI.BarcodeNo Not In  " & _
                '                "(select WR.BarcodeNo from tbl_WholesaleReturn W  left join tbl_WholesaleReturnItem WR " & _
                '                " on W.WholesaleReturnID=WR.WholesaleReturnID where W.IsDelete=0) "

                strCommandText = " select sum(NetAmount) as WSNetAmount,customerid as WSCustomerID from  " & _
                                 " (Select distinct W.CustomerID, w.wholesaleinvoiceid,W.NetAmount " & _
                                 " FROM tbl_WholesaleInvoiceItem WI " & _
                                 " LEFT JOIN tbl_WholesaleInvoice W ON W.WholesaleInvoiceID=WI.WholesaleInvoiceID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID " & _
                                 " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID " & _
                                 " LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemName I on I.ItemNameID=WI.ItemNameID " & _
                                 " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID   WHERE W.IsDelete=0 AND W.WDate BETWEEN @FromDate And @ToDate " & GetFilterString & _
                                 " AND W.WholesaleInvoiceID Not In (select WholesaleInvoiceID  from tbl_ConsignmentSale where IsDelete=0) And " & _
                                 " WI.ItemCode Not In  (select WR.Itemcode from tbl_WholesaleReturn W " & _
                                 " left join tbl_WholesaleReturnItem WR  on W.WholesaleReturnID=WR.WholesaleReturnID where W.IsDelete=0) ) as tmp group by customerid Order by CustomerID asc"



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
        Public Function GetWholeSaleTotalPaidAmtReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal WSType As String = "", Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSaleTotalPaidAmtReport
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                If WSType = "WholeSaleInvoice" Then
                    strCommandText = "SELECT Distinct(W.WholesaleInvoiceID),W.NetAmount as TotalPayment,W.AddOrSub,W.PaidAmount,((W.NetAmount- (W.Discount+W.AddOrSub))) as NetAmount,(W.AddOrSub+W.Discount) As Discount  " & _
                                     " FROM tbl_WholesaleInvoiceItem WI LEFT JOIN tbl_WholesaleInvoice W ON W.WholesaleInvoiceID=WI.WholesaleInvoiceID   " & _
                                     " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID " & _
                                     " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID " & _
                                     " LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                     " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID " & _
                                     " LEFT JOIN tbl_GoldQuality GQ on GQ.GoldQualityID=WI.GoldQualityID " & _
                                     " LEFT JOIN tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                     " WHERE W.IsDelete=0 AND W.WDate BETWEEN @FromDate And @ToDate " & GetFilterString

                ElseIf WSType = "WholeSaleReturn" Then
                    strCommandText = " SELECT Distinct(W.WholesaleReturnID),W.SaleReturnAmount,W.TotalAmount as TotalPayment, W.PaidAmount, " & _
                                     " (W.TotalAmount-W.Discount) as NetAmount FROM tbl_WholesaleReturnItem WI " & _
                                     " LEFT JOIN tbl_WholesaleReturn W ON W.WholesaleReturnID=WI.WholesaleReturnID LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID " & _
                                     " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID    " & _
                                     " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID " & _
                                     " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                     " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID " & _
                                     " LEFT JOIN tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                     " WHERE W.IsDelete=0 AND W.WReturnDate BETWEEN @FromDate And @ToDate " & GetFilterString

                ElseIf WSType = "ConsignmentSale" Then
                    strCommandText = " SELECT Distinct(W.ConsignmentSaleID), W.PaidAmount, W.NetAmount as TotalPayment, (W.NetAmount- (W.Discount+W.AddOrSub)) as NetAmount  " & _
                                     " FROM tbl_ConsignmentSaleItem WI  LEFT JOIN tbl_ConsignmentSale W ON W.ConsignmentSaleID=WI.ConsignmentSaleID   " & _
                                     " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID  " & _
                                     " LEFT JOIN tbl_Staff ST ON W.StaffID=ST.StaffID " & _
                                     " LEFT JOIN tbl_Location L ON L.LocationID=W.LocationID LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                     " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=WI.GoldQualityID " & _
                                     " LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID " & _
                                     " LEFT JOIN tbl_ItemName I on WI.ItemNameID=I.ItemNameID " & _
                                     " WHERE W.IsDelete=0 AND W.ConsignDate BETWEEN @FromDate And @ToDate " & GetFilterString

                End If

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

        Public Function GetWholeSaleSummaryReportByCustomer(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleInvoiceDA.GetWholeSaleSummaryReportByCustomer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "SELECT convert(varchar(10),W.WDate,103) as GroupDate, W.WDate, Case when (WI.IsReturn=1)and (WI.IsSale=1) then 'SR'when (WI.IsReturn=1)and (WI.IsSale=0) then 'PR' when (WI.IsSale=1) AND (WI.IsReturn=0) then 'S' when (WI.IsSale=0) AND WI.IsReturn=0 then 'P' end as SaleOrPay, " & _
                                " PaidRate, COUNT(WI.BarcodeNo) AS QTY, SUM(WI.TotalG) AS TotalG, SUM(WI.PaidRate*WI.TotalG) AS ItemAmount,C.ItemCategory, F.ItemCategoryID, W.CustomerID, CustomerCode, CustomerName,Month(W.WDate) AS PaidMonth,Year(W.WDate)As PaidYear, F.ItemGroupID, ItemGroupName " & _
                                " FROM tbl_WholesaleInvoiceItem WI LEFT JOIN tbl_WholesaleInvoice W ON W.WholesaleInvoiceID=WI.WholesaleInvoiceID  " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=WI.ForSaleID " & _
                                "  LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                " LEFT JOIN tbl_ItemGroup I ON I.ItemGroupID=F.ItemGroupID " & _
                                "  LEFT JOIN tbl_Customer CU on CU.CustomerID=W.CustomerID " & _
                                " WHERE W.IsDelete=0 AND W.WDate BETWEEN @FromDate And @ToDate " & GetFilterString & _
                                " Group By Case when (WI.IsReturn=1)and (WI.IsSale=1) then 'SR'when (WI.IsReturn=1)and (WI.IsSale=0) then 'PR' when (WI.IsSale=1) AND (WI.IsReturn=0) then 'S' when (WI.IsSale=0) AND WI.IsReturn=0 then 'P' end, WDate, PaidRate, C.ItemCategory, F.ItemCategoryID, W.CustomerID, CustomerCode, CustomerName,Month(W.WDate),Year(W.WDate), F.ItemGroupID, ItemGroupName "

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

        Public Function UpdateWholeSalesItem(ByVal Obj As CommonInfo.WholeSaleInvoiceItemInfo) As Boolean Implements IWholeSaleInvoiceDA.UpdateWholeSalesItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try


                '  strCommandText = "UPDATE tbl_WholeSaleInvoiceItem SET IsReturn=@IsReturn " & _
                '" WHERE ForSaleID=@ForSaleID "

                strCommandText = "update tbl_Wholesaleinvoiceitem  set isReturn=@IsReturn  " & _
                                 " From tbl_Wholesaleinvoiceitem A " & _
                                 " inner  join tbl_Wholesalereturn B on A.Wholesaleinvoiceid=B.Wholesaleinvoiceid " & _
                                 " inner join tbl_Wholesalereturnitem C on B.Wholesalereturnid=C.wholesalereturnid and A.forsaleid=C.forsaleid  " & _
                                 " where C.wholesalereturnitemid=@wholesalereturnitemid And A.ForSaleID=@ForSaleID "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
                DB.AddInParameter(DBComm, "@wholesalereturnitemid", DbType.String, Obj.WholesaleInvoiceID)

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
        Public Function UpdateSalesItem(ByVal Obj As CommonInfo.WholeSaleInvoiceItemInfo) As Boolean Implements IWholeSaleInvoiceDA.UpdateSalesItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try


                strCommandText = "UPDATE tbl_WholeSaleInvoiceItem SET IsSale=@IsSale " & _
              " WHERE ForSaleID=@ForSaleID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsSale", DbType.Boolean, Obj.IsSale)

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
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements IWholeSaleInvoiceDA.UpdateRedeemID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_WholeSaleInvoice SET RedeemID=@RedeemID WHERE  WholeSaleInvoiceID= @WholeSaleInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, SaleInvoiceID)
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

        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean Implements IWholeSaleInvoiceDA.UpdateTransactionID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_WholeSaleInvoice SET TransactionID=@TransactionID WHERE  WholeSaleInvoiceID= @WholeSaleInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WholeSaleInvoiceID", DbType.String, SaleInvoiceID)
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
