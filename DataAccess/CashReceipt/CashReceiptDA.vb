Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace CashReceipt
    Public Class CashReceiptDA
        Implements ICashReceiptDA

#Region "Private CashReceipt"

        Private DB As Database
        Private Shared ReadOnly _instance As ICashReceiptDA = New CashReceiptDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICashReceiptDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetOrderCashReceipt(ByVal IsCash As Boolean) As System.Data.DataTable Implements ICashReceiptDA.GetOrderCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash = True Then
                    strCommandText = "Select C.CustomerName as [Customer_], H.OrderReturnHeaderID AS [@OrderReturnHeaderID], H.OrderInvoiceID AS VoucherNo,convert(varchar(10),ReturnDate,105) As ReturnDate," & _
                                           " H.AllTotalAmount as [TotalAmount], (((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount)) as [NetAmount], H.AllAddOrSub as [AddOrSub]," & _
                                           " H.DiscountAmount AS [DiscountAmount],  (H.AdvanceAmount+H.FromGoldAmount+H.PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.OrderInvoiceID AND Type='OrderInvoice'),0)) as [PaidAmount], " & _
                                           " (((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount)-(H.AdvanceAmount+H.FromGoldAmount+H.PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.OrderInvoiceID AND Type='OrderInvoice'),0))) as [Balance Amount  ],ReturnDate as [@HideReturnDate] " & _
                                           " From tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice O ON O.OrderInvoiceID=H.OrderInvoiceID LEFT JOIN tbl_Customer C ON C.CustomerID=O.CustomerID " & _
                                           " where ((((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount)-H.AdvanceAmount)-(H.FromGoldAmount+H.PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.OrderInvoiceID AND Type='OrderInvoice' And IsDelete=0),0)))=0 and IsRetrieved=1  " & _
                                           " AND (((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount)-(H.AdvanceAmount+H.FromGoldAmount+H.PaidAmount)) <>0 " & _
                                           " AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID And R.IsDelete=0) " & _
                                           " AND H.IsDelete=0 And O.IsDelete=0 And C.IsDelete=0 " & _
                                           " ORder By [@HideReturnDate] DESC , H.OrderInvoiceID DESC "
                Else
                    strCommandText = "Select C.CustomerName as [Customer_], H.OrderReturnHeaderID AS [@OrderReturnHeaderID], H.OrderInvoiceID AS VoucherNo,convert(varchar(10),ReturnDate,105) As ReturnDate," & _
                                          " H.AllTotalAmount as [TotalAmount], (((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount)) as [NetAmount], H.AllAddOrSub as [AddOrSub]," & _
                                          " H.DiscountAmount AS [DiscountAmount], (H.AdvanceAmount+H.FromGoldAmount+H.PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.OrderInvoiceID AND Type='OrderInvoice'),0)) as [PaidAmount], " & _
                                          " (((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount)-(H.AdvanceAmount+H.FromGoldAmount+H.PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.OrderInvoiceID AND Type='OrderInvoice'),0))) as [Balance Amount  ],ReturnDate as [@HideReturnDate] " & _
                                          " From tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice O ON O.OrderInvoiceID=H.OrderInvoiceID LEFT JOIN tbl_Customer C ON C.CustomerID=O.CustomerID  " & _
                                          " where (((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount)-(H.AdvanceAmount+H.FromGoldAmount+H.PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.OrderInvoiceID AND Type='OrderInvoice' And IsDelete=0),0))) <>0 and IsRetrieved=1  " & _
                                          " AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID And IsDelete=0 ) " & _
                                           " AND H.IsDelete=0 " & _
                                          " ORder By [@HideReturnDate] DESC , H.OrderInvoiceID DESC "

                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseGemsCashReceipt() As System.Data.DataTable Implements ICashReceiptDA.GetPurchaseGemsCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select PurchaseGemsID,convert(varchar(10),PDate,105) As PDate,Customer as [Customer_]," & _
                                " TotalAmount as [TotalAmount],AddOrSub as [AddOrSubAmount],TotalAmount+AddOrSub as [NetAmount],PaidAmount as [PaidAmount]," & _
                                " TotalAmount+AddOrSub-PaidAmount as [Balance Amount  ]" & _
                                " From tbl_PurchaseGems  where (TotalAmount+AddOrSub-PaidAmount)>0 " & _
                                " order by PurchaseGemsID asc "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseInvoiceCashReceipt() As System.Data.DataTable Implements ICashReceiptDA.GetPurchaseInvoiceCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select PurchaseInvoiceID,convert(varchar(10),PDate,103) As PDate,C.CustomerName as [Customer_], " & _
                                " TotalAmount as [TotalAmount],AddOrSub as [AddOrSubAmount],TotalAmount+AddOrSub as [NetAmount],PaidAmount as [PaidAmount]," & _
                                " TotalAmount+AddOrSub-PaidAmount as [Balance Amount  ]" & _
                                " From tbl_PurchaseInvoice P left join tbl_Customer C on P.CustomerID=C.CustomerID where (TotalAmount+AddOrSub-PaidAmount)>0  " & _
                                " order by PurchaseInvoiceID asc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleGemsCashReceipt(ByVal IsCash As Boolean) As System.Data.DataTable Implements ICashReceiptDA.GetSaleGemsCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash Then
                    strCommandText = "Select C.CustomerName as [Customer_], SaleGemsID AS VoucherNo,convert(varchar(10),SDate,105) As SaleDate,SDate as [@SDate], " & _
                               " TotalAmount as [TotalAmount],AddOrSub as [AddOrSubAmount],((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount)) as [NetAmount], " & _
                               " (PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleGemsID AND Type='SalesGems'),0)) as [PaidAmount]," & _
                               " (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleGemsID AND Type='SalesGems'),0))) as [Balance Amount  ]" & _
                               " From tbl_SaleGems S LEFT JOIN tbl_Customer  C ON C.CustomerID=S.CustomerID where (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleGemsID AND Type='SalesGems'),0)))=0 " & _
                               " AND (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount))-PaidAmount)<>0  AND S.IsDelete=0 " & _
                               " order by [@SDate] DESC, SaleGemsID DESC "
                Else
                    strCommandText = "Select C.CustomerName as [Customer_], SaleGemsID AS VoucherNo,convert(varchar(10),SDate,105) As SaleDate, SDate as [@SDate], " & _
                              " TotalAmount as [TotalAmount],AddOrSub as [AddOrSubAmount],((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount)-isnull(S.PurchaseAmount,0)) as [NetAmount], " & _
                              " (PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleGemsID AND Type='SalesGems'),0)) as [PaidAmount]," & _
                              " (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+isnull(S.PurchaseAmount,0)+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleGemsID AND Type='SalesGems'),0))) as [Balance Amount  ],S.PurchaseAmount" & _
                              " From tbl_SaleGems S LEFT JOIN tbl_Customer  C ON C.CustomerID=S.CustomerID where S.isDelete=0 And (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+isnull(S.PurchaseAmount,0)+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleGemsID AND Type='SalesGems' And IsDelete=0),0)))<>0 " & _
                              " order by [@SDate] DESC, SaleGemsID DESC "
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleInvoiceCashReceipt() As System.Data.DataTable Implements ICashReceiptDA.GetSaleInvoiceCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select SaleInvoiceID,convert(varchar(10),SDate,103) As SDate,C.CustomerName as [Customer_], " & _
                                " TotalPayment as [TotalAmount],AddOrSub as [AddOrSubAmount],DiscountAmount,TotalPayment+AddOrSub-DiscountAmount as [NetAmount],PaidAmount as [PaidAmount], " & _
                                " (TotalPayment+AddOrSub-DiscountAmount)-PaidAmount as [Balance Amount  ] " & _
                                 " From tbl_SaleInvoice S left join tbl_Customer C on S.CustomerID=C.CustomerID where ((TotalPayment+AddOrSub-DiscountAmount)-PaidAmount)>0  " & _
                                " order by SaleInvoiceID asc  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWholeSalesCashReceipt() As System.Data.DataTable Implements ICashReceiptDA.GetWholeSalesCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select WholeSaleID,convert(varchar(10),WSDate,103) As WSDate,tbl_Customer.CustomerName as [CustomerName_], " & _
                                " TotalAmount as [TotalAmount],AddOrSub as [AddOrSubAmount],TotalAmount+AddOrSub as [NetAmount],PaidAmount as [PaidAmount]," & _
                                " (TotalAmount+AddOrSub-PaidAmount) as [Balance Amount  ]" & _
                                "  From tbl_WholeSale  Inner join tbl_Customer on tbl_WholeSale.CustomerID=tbl_Customer.CustomerID" & _
                                "  where(TotalAmount + AddOrSub - PaidAmount) > 0 " & _
                                " order by WholeSaleID asc "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetCashReceipt(ByVal VoucherNo As String, Optional ByVal Type As String = "") As System.Data.DataTable Implements ICashReceiptDA.GetCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select CashReceiptID as [@CashReceiptID],PayDate as [ReceiptDate],PayAmount as [Amount],Remark, IsBank ,ReturnAdvanceID As VoucherNo" & _
                           " from tbl_CashReceipt where IsDelete=0 AND VoucherNo='" & VoucherNo & "' and Type ='" & Type & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function DeleteCashReceipt(ByVal CashReceiptID As String) As Boolean Implements ICashReceiptDA.DeleteCashReceipt
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_CashReceipt SET IsDelete=1  WHERE  CashReceiptID= @CashReceiptID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CashReceiptID", DbType.String, CashReceiptID)
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

        Public Function InsertCashReceipt(ByVal obj As CommonInfo.CashReceiptInfo) As Boolean Implements ICashReceiptDA.InsertCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_CashReceipt ( CashReceiptID,VoucherNo,PayDate,PayAmount,Remark,LocationID,Type,IsBank,LastModifiedLoginUserName,LastModifiedDate,IsDelete,IsSync,ReturnAdvanceID)"
                strCommandText += " Values (@CashReceiptID,@VoucherNo,@PayDate,@PayAmount,@Remark,@LocationID,@Type,@IsBank,@LastModifiedLoginUserName,getdate(),0,0,@ReturnAdvanceID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CashReceiptID", DbType.String, obj.CashReceiptID)
                DB.AddInParameter(DBComm, "@VoucherNo", DbType.String, obj.VoucherNo)
                DB.AddInParameter(DBComm, "@PayDate", DbType.Date, obj.PayDate)
                DB.AddInParameter(DBComm, "@PayAmount", DbType.Int32, obj.PayAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@Type", DbType.String, obj.Type)
                DB.AddInParameter(DBComm, "@IsBank", DbType.Boolean, obj.IsBank)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, obj.RAdvanceID)

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

        Public Function UpdateCashReceipt(ByVal obj As CommonInfo.CashReceiptInfo) As Boolean Implements ICashReceiptDA.UpdateCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_CashReceipt set  CashReceiptID= @CashReceiptID , VoucherNo= @VoucherNo , PayDate= @PayDate , PayAmount= @PayAmount , Remark= @Remark ,Type=@Type,LocationID=@LocationID , IsBank=@IsBank, LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate=getdate(),ReturnAdvanceID=@ReturnAdvanceID "
                strCommandText += " where CashReceiptID= @CashReceiptID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CashReceiptID", DbType.String, obj.CashReceiptID)
                DB.AddInParameter(DBComm, "@VoucherNo", DbType.String, obj.VoucherNo)
                DB.AddInParameter(DBComm, "@PayDate", DbType.DateTime, obj.PayDate)
                DB.AddInParameter(DBComm, "@PayAmount", DbType.Int32, obj.PayAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@Type", DbType.String, obj.Type)
                DB.AddInParameter(DBComm, "@IsBank", DbType.Boolean, obj.IsBank)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, obj.RAdvanceID)

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
        Function GetDebtDataByType(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "", Optional ByVal Str As String = "") As DataTable Implements ICashReceiptDA.GetDebtDataByType
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                Select Case argType
                    Case "PurchaseGems"
                        strCommandText = "Select * from (select VoucherNo, PDate," & _
                                 " Customer as Customer,I.Address as Address, " & _
                                  " TotalAmount,AddOrSub,TotalAmount+AddOrSub as NetAmount,PaidAmount,PayAmount,  " & _
                                  " PaidAmount+PayAmount as TotalPaidAmount,TotalAmount+AddOrSub-PaidAmount-PayAmount as DebtAmount" & _
                                  " from tbl_PurchaseGems 	I  " & _
                                  " LEFT JOIN tbl_Location L ON L.LocationID=I.LocationID, " & _
                                  " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='PurchaseGems' and IsDelete=0 group by VoucherNo ) as Debt 	  " & _
                                  " where I.PurchaseGemsID=Debt.VoucherNo  " & _
                                  " and TotalAmount+AddOrSub-PaidAmount-PayAmount<>0 " & criStr & _
                                  " union all " & _
                                  " select PurchaseGemsID as VoucherNo, PDate, " & _
                                  " Customer as Customer,I.Address as Address,  " & _
                                  " TotalAmount,AddOrSub,TotalAmount+AddOrSub as NetAmount,PaidAmount,0 as PayAmount,   PaidAmount as TotalPaidAmount,  " & _
                                  " TotalAmount+AddOrSub-PaidAmount as DebtAmount from tbl_PurchaseGems  I" & _
                                  " LEFT JOIN tbl_Location L ON L.LocationID=I.LocationID " & _
                                  " where TotalAmount+AddOrSub-PaidAmount<>0 " & criStr & "  and PurchaseGemsID Not In (Select VoucherNo from tbl_CashReceipt Where Type='PurchaseGems' and IsDelete=0))as M " & _
                                  "  WHERE 1=1 " & Str & "order by Customer,VoucherNo "

                    Case "SaleGems"
                        strCommandText = "Select * from (select VoucherNo, I.SDate AS PDate , I.CustomerID, " & _
                                 " C.CustomerName as Customer,C.CustomerAddress as Address, " & _
                                  " TotalAmount,AddOrSub,((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount)) as NetAmount,PaidAmount,PayAmount,  " & _
                                  " PaidAmount+PayAmount as TotalPaidAmount,((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+PayAmount+isnull(I.PurchaseAmount,0)) as DebtAmount " & _
                                  " from tbl_SaleGems 	I  LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID " & _
                                  " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID ," & _
                                  " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesGems' and IsDelete=0 group by VoucherNo ) as Debt 	  " & _
                                  " where I.SaleGemsID=Debt.VoucherNo and I.IsDelete=0 " & _
                                  " and ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0)<>0 " & criStr & _
                                  " union all " & _
                                  " select SaleGemsID as VoucherNo, I.SDate AS PDate, I.CustomerID, " & _
                                  " C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                  " TotalAmount,AddOrSub,((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount)) as NetAmount,PaidAmount,0 as PayAmount,   PaidAmount as TotalPaidAmount,  " & _
                                  " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0) as DebtAmount from tbl_SaleGems I " & _
                                  " LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID " & _
                                  " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID " & _
                                  " where ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0)<>0 " & criStr & " and I.IsDelete=0 and SaleGemsID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesGems' and IsDelete=0 ))as M " & _
                                   " WHERE 1=1 " & Str & " order by VoucherNo "
                    Case "PurchaseInvoice"

                        strCommandText = "Select * from (select VoucherNo,PurchaseDate as PDate,I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,   " & _
                                       " AllTotalAmount,AllAddOrSub,AllTotalAmount-AllAddOrSub as NetAmount,AllPaidAmount,PayAmount,AllPaidAmount+PayAmount as TotalPaidAmount, " & _
                                       " AllTotalAmount-AllAddOrSub-AllPaidAmount-PayAmount as DebtAmount  from tbl_PurchaseHeader 	I   left join tbl_Customer C on I.CustomerID=C.CustomerID ,  (select VoucherNo, Sum(PayAmount) as PayAmount " & _
                                       " from tbl_CashReceipt  Where Type='PurchaseInvoice' and IsDelete=0  group by VoucherNo) as Debt where I.PurchaseHeaderID=Debt.VoucherNo and  AllTotalAmount-AllAddOrSub-AllPaidAmount-PayAmount<>0 " & criStr & " union all  " & _
                                       " select PurchaseHeaderID as VoucherNo, PurchaseDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,   AllTotalAmount,AllAddOrSub,AllTotalAmount-AllAddOrSub as NetAmount,AllPaidAmount,0 as PayAmount,  " & _
                                       " AllPaidAmount as TotalPaidAmount,   AllTotalAmount-AllAddOrSub-AllPaidAmount as DebtAmount from tbl_PurchaseHeader I  " & _
                                       " left join tbl_Customer C on I.CustomerID=C.CustomerID   where AllTotalAmount-AllAddOrSub-AllPaidAmount<>0 " & criStr & " and PurchaseHeaderID Not In(Select VoucherNo from tbl_CashReceipt Where Type='PurchaseInvoice' ))as M " & _
                                       " WHERE 1=1  " & Str & " order by VoucherNo "
                    Case "SaleInvoice"
                        strCommandText = "Select * from (select VoucherNo,  SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address, " & _
                                         " TotalAmount,AddOrSub,RedeemValue,MemberdiscountAmt,((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) AS PaidAmount,PayAmount,   " & _
                                         " (PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as TotalPaidAmount,(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleInvoiceHeader I   left join tbl_Customer C on I.CustomerID=C.CustomerID  , " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0  group by VoucherNo ) as Debt " & _
                                         " where I.SaleInvoiceHeaderID = Debt.VoucherNo AND I.IsDelete=0 And  (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) <> 0  " & criStr & " union all " & _
                                         " select SaleInvoiceHeaderID as VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                         " TotalAmount,AddOrSub,RedeemValue,MemberdiscountAmt,((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) AS PaidAmount, 0 as PayAmount, " & _
                                         " (PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as TotalPaidAmount,   (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleInvoiceHeader I  left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                                         " where (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) <>0 AND IsCancel=0 AND I.IsDelete=0 " & criStr & " and SaleInvoiceHeaderID Not In " & _
                                         " (Select VoucherNo from tbl_CashReceipt  Where Type='SalesInvoice' and IsDelete=0 ))as M  WHERE 1=1 " & Str & " order by VoucherNo"
                    Case "WholeSalesInvoice"
                        strCommandText = " Select * from (select VoucherNo,  WDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,  NetAmount as TotalAmount,AddOrSub,RedeemValue,MemberDiscountAmt, " & _
                                         "((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount) AS PaidAmount,PayAmount,    " & _
                                         " (PaidAmount+PayAmount) as TotalPaidAmount, " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount  " & _
                                         " from tbl_WholeSaleInvoice I   left join tbl_Customer C on I.CustomerID=C.CustomerID  ,  " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='WholeSalesInvoice' and IsDelete=0  group by VoucherNo ) as Debt  " & _
                                         " where I.WholeSaleInvoiceID = Debt.VoucherNo And I.IsDelete = 0 And (((NetAmount + AddOrSub) - (+Discount+RedeemValue+MemberDiscountAmt)) - (PaidAmount)) <> 0 " & criStr & " union all " & _
                                         " select WholeSaleInvoiceID as VoucherNo, WDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,   " & _
                                         " NetAmount,AddOrSub,((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount) AS PaidAmount,RedeemValue,MemberDiscountAmt, " & _
                                         "  0 as PayAmount,  (PaidAmount) as TotalPaidAmount,   " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount)) as DebtAmount  " & _
                                         " from tbl_WholeSaleInvoice I  left join tbl_Customer C on I.CustomerID=C.CustomerID  where " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount)) <>0 AND PayType='2'  " & _
                                         " AND I.IsDelete=0 " & criStr & " and WholeSaleInvoiceID Not In  (Select VoucherNo from tbl_CashReceipt  Where Type='WholeSalesInvoice' and IsDelete=0 ))as M  WHERE 1=1  " & Str & " order by VoucherNo"

                    Case "ConsignmentSaleInvoice"
                        strCommandText = " Select * from (select VoucherNo,  ConsignDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,  NetAmount as TotalAmount,AddOrSub,RedeemValue,MemberDiscountAmt, " & _
                                      " ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount+I.PurchaseAmount) AS PaidAmount,PayAmount,     (PaidAmount+PayAmount) as TotalPaidAmount,  " & _
                                      " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount+I.PurchaseAmount)) as DebtAmount   from tbl_ConsignmentSale I   left join tbl_Customer C on I.CustomerID=C.CustomerID  ,   " & _
                                      " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='ConsignmentSaleInvoice' and IsDelete=0  group by VoucherNo ) as Debt   " & _
                                      " where I.ConsignmentSaleID = Debt.VoucherNo And I.IsDelete = 0 And (((NetAmount + AddOrSub) - (+Discount+RedeemValue+MemberDiscountAmt)) - (PaidAmount)) <> 0 " & criStr & " union all " & _
                                      " select ConsignmentSaleID as VoucherNo, ConsignDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,    " & _
                                      " NetAmount,AddOrSub,RedeemValue,MemberDiscountAmt,((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount) AS PaidAmount,   0 as PayAmount,  (PaidAmount) as TotalPaidAmount,    " & _
                                      " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+I.PurchaseAmount)) as DebtAmount   from tbl_ConsignmentSale I  left join tbl_Customer C on I.CustomerID=C.CustomerID  " & _
                                      " where  (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+I.PurchaseAmount)) <>0   AND I.IsDelete=0 " & criStr & " and ConsignmentSaleID Not In  " & _
                                      " (Select VoucherNo from tbl_CashReceipt  Where Type='ConsignmentSaleInvoice' and IsDelete=0 ))as M  WHERE 1=1   " & Str & " order by VoucherNo"


                    Case "OrderInvoice"
                        strCommandText = "Select * from (select H.OrderInvoiceID AS VoucherNo, H.ReturnDate as PDate, O.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                " H.FromGoldAmount,  H.AllTotalAmount, H.AllAddOrSub, H.DiscountAmount,(((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) as NetAmount,  H.PaidAmount+H.FromGoldAmount AS PaidAmount, H.AdvanceAmount, PayAmount," & _
                                " H.PaidAmount+PayAmount+H.FromGoldAmount AS TotalPaidAmount, " & _
                                " ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+PayAmount+H.FromGoldAmount)) AS DebtAmount  " & _
                                " from tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice O ON O.OrderInvoiceID=H.OrderInvoiceID" & _
                                " Left Join tbl_Customer C on C.CustomerID=O.CustomerID  LEFT JOIN tbl_Location ON tbl_Location.LocationID=H.LocationID ," & _
                                " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='OrderInvoice' and IsDelete=0  group by VoucherNo ) as Debt 	   " & _
                                " where H.OrderInvoiceID = Debt.VoucherNo And H.isDelete=0 And ((((H.AllTotalAmount + H.AllAddOrSub) - H.DiscountAmount)-H.AdvanceAmount) - (H.PaidAmount + H.FromGoldAmount))<>0 and IsRetrieved = 1" & _
                                " AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID " & criStr & " ) " & _
                                " UNION ALL " & _
                                " select H.OrderInvoiceID as VoucherNo, H.ReturnDate as PDate, O.CustomerID,  C.CustomerName as Customer," & _
                                " C.CustomerAddress as Address, H.FromGoldAmount,   H.AllTotalAmount, H.AllAddOrSub, H.DiscountAmount, (((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) as NetAmount,(H.PaidAmount+H.FromGoldAmount) AS PaidAmount, " & _
                                " 0 as PayAmount, H.AdvanceAmount, (H.PaidAmount+H.FromGoldAmount) as TotalPaidAmount,   " & _
                                " ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+H.FromGoldAmount)) AS DebtAmount " & _
                                " from tbl_OrderReturnHeader H LEFT JOIN  tbl_OrderInvoice O ON O.OrderInvoiceID=H.OrderInvoiceID" & _
                                " Left Join tbl_Customer C on C.CustomerID=O.CustomerID  LEFT JOIN tbl_Location " & _
                                " ON tbl_Location.LocationID=H.LocationID  where ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+H.FromGoldAmount))<>0  " & _
                                " and IsRetrieved = 1 And H.isDelete=0 and H.OrderInvoiceID Not In (Select VoucherNo from tbl_CashReceipt Where Type='OrderInvoice' and IsDelete=0  )" & _
                                " AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID)" & criStr & " )as M  " & _
                                " WHERE 1=1 " & Str & " order by VoucherNo "

                    Case "RepairReturn"
                        strCommandText = "Select * from (select H.RepairID AS VoucherNo, H.ReturnDate as PDate, O.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                " H.AllReturnTotalAmount AS AllTotalAmount, H.AllReturnAddOrSub As AllAddOrSub, H.ReturnDiscountAmount AS DiscountAmount, (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-H.AdvanceAmount) as NetAmount, H.ReturnPaidAmount AS PaidAmount, PayAmount," & _
                                " (H.ReturnPaidAmount+PayAmount) as TotalPaidAmount, " & _
                                " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount+PayAmount)) as DebtAmount  " & _
                                " from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader O ON O.RepairID=H.RepairID" & _
                                " Left Join tbl_Customer C on C.CustomerID=O.CustomerID ," & _
                                " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='RepairReturn' and IsDelete=0  group by VoucherNo ) as Debt 	   " & _
                                " where H.RepairID = Debt.VoucherNo And H.isDelete=0  And (((H.AllReturnTotalAmount + H.AllReturnAddOrSub) - H.ReturnDiscountAmount) - (H.ReturnPaidAmount + H.AdvanceAmount)) <>0 and IsAllReturn = 1" & _
                                " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID) " & _
                                " UNION ALL " & _
                                " select H.RepairID as VoucherNo, H.ReturnDate as PDate, O.CustomerID,  C.CustomerName as Customer," & _
                                " C.CustomerAddress as Address, H.AllReturnTotalAmount AS AllTotalAmount, H.AllReturnAddOrSub As AllAddOrSub, H.ReturnDiscountAmount AS DiscountAmount, " & _
                                " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-H.AdvanceAmount) as NetAmount, H.ReturnPaidAmount AS PaidAmount, " & _
                                " 0 as PayAmount,H.ReturnPaidAmount AS TotalPaidAmount,   " & _
                                " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) as DebtAmount " & _
                                " from tbl_ReturnRepairHeader H LEFT JOIN  tbl_RepairHeader O ON O.RepairID=H.RepairID" & _
                                " Left Join tbl_Customer C on C.CustomerID=O.CustomerID  " & _
                                " where (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) <>0  " & _
                                " and IsAllReturn = 1 And H.isDelete=0  and H.RepairID Not In (Select VoucherNo from tbl_CashReceipt Where Type='RepairReturn' and IsDelete=0  )" & _
                                " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID))as M  " & _
                                " WHERE 1=1 " & Str & " order by VoucherNo "

                    Case "SalesVolume"
                        strCommandText = "Select * from (select VoucherNo, SaleDate as PDate , I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                            " TotalAmount,AddOrSub,RedeemValue,MemberDiscountAmt, ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount+PurchaseAmount) AS PaidAmount,PayAmount, (PaidAmount+PayAmount+PurchaseAmount) as TotalPaidAmount," & _
                                            "(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount+PurchaseAmount)) as DebtAmount   " & _
                                            " from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID  ," & _
                                            " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoiceVolume' and IsDelete=0 group by VoucherNo ) as Debt where I.SalesVolumeID=Debt.VoucherNo AND I.IsDelete=0 And " & _
                                            "(((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount)) <>0    " & criStr & " union all " & _
                                            " select SalesVolumeID as VoucherNo, SaleDate as PDate , I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,TotalAmount,AddOrSub,RedeemValue,MemberdiscountAmt," & _
                                            "((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount+PurchaseAmount) AS PaidAmount,0 as PayAmount,(PaidAmount+PurchaseAmount) as TotalPaidAmount, " & _
                                            " (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount)) as DebtAmount   " & _
                                            "from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID" & _
                                            " where(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount))<>0  AND I.IsDelete=0 " & criStr & "and SalesVolumeID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoiceVolume' ))as M" & _
                                            "  WHERE 1=1 " & Str & "order by VoucherNo "
                    Case "SaleLooseDiamond"
                        strCommandText = "Select * from (select VoucherNo,  SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address, " & _
                                         " TotalAmount,AddOrSub,RedeemValue,MemberDiscountAmt,((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount+PurchaseAmount+OtherCashAmount) AS PaidAmount,PayAmount,   " & _
                                         " (PaidAmount+PayAmount+PurchaseAmount+OtherCashAmount) as TotalPaidAmount,(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount+PurchaseAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleLooseDiamondHeader I   left join tbl_Customer C on I.CustomerID=C.CustomerID  , " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SaleLooseDiamond' and IsDelete=0  group by VoucherNo ) as Debt " & _
                                         " where I.SaleLooseDiamondID = Debt.VoucherNo AND I.IsDelete=0 And  (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+OtherCashAmount)) <> 0  " & criStr & " union all " & _
                                         " select SaleLooseDiamondID as VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                         " TotalAmount,AddOrSub,RedeemValue,MemberDiscountAmt,((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,(PaidAmount+PurchaseAmount+OtherCashAmount) AS PaidAmount, 0 as PayAmount, " & _
                                         " (PaidAmount+PurchaseAmount+OtherCashAmount) as TotalPaidAmount,   (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleLooseDiamondHeader I  left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                                         " where (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+OtherCashAmount)) <>0 AND I.IsDelete=0 " & criStr & " and SaleLooseDiamondID Not In " & _
                                         " (Select VoucherNo from tbl_CashReceipt  Where Type='SaleLooseDiamond' and IsDelete=0 ))as M  WHERE 1=1 " & Str & " order by VoucherNo"
                    Case "All"
                        strCommandText = " Select * from (Select * from (select VoucherNo,  SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address, " & _
                                         " ((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount, (PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as PaidAmount,PayAmount,   " & _
                                         " (PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as TotalPaidAmount,(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleInvoiceHeader I   left join tbl_Customer C on I.CustomerID=C.CustomerID  , " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 group by VoucherNo ) as Debt " & _
                                         " where I.SaleInvoiceHeaderID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) > 0  " & criStr & " union all " & _
                                         " select SaleInvoiceHeaderID as VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                         " ((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount, (PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as PaidAmount, 0 as PayAmount, " & _
                                         " (PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as TotalPaidAmount,   (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleInvoiceHeader I  left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                                         " where I.IsDelete=0 AND C.IsDelete=0 AND (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) >0 AND IsCancel=0 " & criStr & " and SaleInvoiceHeaderID Not In " & _
                                         " (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 ))as E " & _
                                         " UNION ALL" & _
                                         "  Select * from (select VoucherNo,  WDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,  ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount, (PaidAmount) as PaidAmount,PayAmount, " & _
                                         " (PaidAmount+PayAmount) as TotalPaidAmount, " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount  from tbl_WholeSaleInvoice I   " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  ,  " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='WholeSalesInvoice' and IsDelete=0 " & _
                                         " group by VoucherNo ) as Debt  where I.WholeSaleInvoiceID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount)) > 0  " & criStr & " union all " & _
                                         " select WholeSaleInvoiceID as VoucherNo, WDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,   ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount, " & _
                                         " (PaidAmount) as PaidAmount, 0 as PayAmount,  " & _
                                         " (PaidAmount) as TotalPaidAmount,   " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount)) as DebtAmount  " & _
                                         " from tbl_WholeSaleInvoice I  left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount)) >0 " & criStr & " AND PayType='2' " & _
                                         " and WholeSaleInvoiceID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='WholeSalesInvoice'  and IsDelete=0 ))as W " & _
                                         " UNION ALL " & _
                                         " Select * from (select VoucherNo,  ConsignDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,  ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount, (PaidAmount+I.PurchaseAmount) as PaidAmount,PayAmount,    " & _
                                         " (PaidAmount+PayAmount) as TotalPaidAmount, " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount+I.PurchaseAmount)) as DebtAmount  from tbl_ConsignmentSale I   " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  ,  " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='ConsignmentSaleInvoice' and IsDelete=0 " & _
                                         " group by VoucherNo ) as Debt  where I.ConsignmentSaleID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  " & _
                                         " (((NetAmount + AddOrSub) - (Discount+RedeemValue+MemberDiscountAmt)) - (PaidAmount)) > 0  " & criStr & " union all " & _
                                         " select ConsignmentSaleID as VoucherNo, ConsignDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,   ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as NetAmount, " & _
                                         " (PaidAmount) as PaidAmount, 0 as PayAmount,  " & _
                                         " (PaidAmount) as TotalPaidAmount,   " & _
                                         " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+I.PurchaseAmount)) as DebtAmount  " & _
                                         " from tbl_ConsignmentSale I  left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND " & _
                                         " (((NetAmount + AddOrSub) - (Discount+RedeemValue+MemberDiscountAmt)) - (PaidAmount+I.PurchaseAmount)) > 0 " & criStr & " and ConsignmentSaleID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='ConsignmentSaleInvoice' and IsDelete=0 ))as S  " & _
                                        " UNION ALL " & _
                                        " Select * from (select H.OrderInvoiceID AS VoucherNo, H.ReturnDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " (((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) as NetAmount, H.PaidAmount+H.FromGoldAmount AS PaidAmount, PayAmount," & _
                                        " H.PaidAmount+PayAmount+H.FromGoldAmount AS TotalPaidAmount, ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+PayAmount+H.FromGoldAmount)) AS DebtAmount " & _
                                        " from tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  LEFT JOIN tbl_Location ON tbl_Location.LocationID=H.LocationID ," & _
                                        " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='OrderInvoice'  and IsDelete=0 group by VoucherNo ) as Debt 	   " & _
                                        " where H.OrderInvoiceID = Debt.VoucherNo And ((((H.AllTotalAmount + H.AllAddOrSub) - H.DiscountAmount)-H.AdvanceAmount) - (H.PaidAmount + H.FromGoldAmount))>0 and IsRetrieved = 1 " & criStr & _
                                        " AND H.IsDelete=0 And I.IsDelete=0 And C.IsDelete=0 AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID AND R.IsDelete=0) " & _
                                        " UNION ALL " & _
                                        " select H.OrderInvoiceID as VoucherNo, H.ReturnDate as PDate, I.CustomerID,  C.CustomerName as Customer," & _
                                        " C.CustomerAddress as Address, (((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) as NetAmount, (H.PaidAmount+H.FromGoldAmount) as PaidAmount, " & _
                                        " 0 as PayAmount, (H.PaidAmount+H.FromGoldAmount) as TotalPaidAmount,   " & _
                                        " ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+H.FromGoldAmount)) AS DebtAmount " & _
                                        " from tbl_OrderReturnHeader H LEFT JOIN  tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  LEFT JOIN tbl_Location " & _
                                        " ON tbl_Location.LocationID=H.LocationID  where ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+H.FromGoldAmount))<>0  " & criStr & _
                                        " and IsRetrieved = 1 and H.OrderInvoiceID Not In (Select VoucherNo from tbl_CashReceipt Where Type='OrderInvoice' and IsDelete=0  )" & _
                                        " AND H.IsDelete=0 And I.IsDelete=0 And C.IsDelete=0 And H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID AND R.IsDelete=0)) AS A" & _
                                        " UNION ALL" & _
                                        " Select * from (select VoucherNo, I.SDate AS PDate , I.CustomerID, " & _
                                        " C.CustomerName as Customer,C.CustomerAddress as Address, " & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount)) as NetAmount,PaidAmount,PayAmount,  " & _
                                        " PaidAmount+PayAmount as TotalPaidAmount,((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+PayAmount+isnull(I.PurchaseAmount,0)) as DebtAmount " & _
                                        " from tbl_SaleGems 	I  LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID " & _
                                        " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID ," & _
                                        " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesGems' and IsDelete=0  group by VoucherNo ) as Debt 	  " & _
                                        " where I.IsDelete=0 And C.IsDelete=0 And I.SaleGemsID=Debt.VoucherNo  " & _
                                        " and ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+isnull(I.PurchaseAmount,0))>0 " & criStr & _
                                        " union all " & _
                                        " select SaleGemsID as VoucherNo, I.SDate AS PDate, I.CustomerID, " & _
                                        " C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount)) as NetAmount,PaidAmount,0 as PayAmount, PaidAmount as TotalPaidAmount,  " & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0) as DebtAmount from tbl_SaleGems I " & _
                                        " LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID " & _
                                        " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID " & _
                                        " where I.IsDelete=0 and C.IsDelete=0 And tbl_Location.IsDelete=0 and ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0)>0 " & criStr & "and SaleGemsID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesGems' and IsDelete=0 )) AS B " & _
                                        " UNION ALL " & _
                                        " Select * from (select H.RepairID AS VoucherNo, H.ReturnDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-H.AdvanceAmount) as NetAmount, H.ReturnPaidAmount AS PaidAmount, PayAmount," & _
                                        " (H.ReturnPaidAmount + PayAmount) as TotalPaidAmount, " & _
                                        " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount+PayAmount)) as DebtAmount  " & _
                                        " from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader I ON I.RepairID=H.RepairID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID ," & _
                                        " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='RepairReturn' and IsDelete=0  group by VoucherNo ) as Debt 	   " & _
                                        " where H.RepairID = Debt.VoucherNo And (((H.AllReturnTotalAmount + H.AllReturnAddOrSub) - H.ReturnDiscountAmount) - (H.ReturnPaidAmount + H.AdvanceAmount)) >0 and IsAllReturn = 1 " & criStr & _
                                        " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID AND IsDelete=0 ) " & _
                                        " UNION ALL " & _
                                        " select H.RepairID as VoucherNo, H.ReturnDate as PDate, I.CustomerID,  C.CustomerName as Customer," & _
                                        " C.CustomerAddress as Address, (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-H.AdvanceAmount) as NetAmount,  H.ReturnPaidAmount AS PaidAmount, " & _
                                        " 0 as PayAmount, H.ReturnPaidAmount AS TotalPaidAmount,   " & _
                                        " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) as DebtAmount " & _
                                        " from tbl_ReturnRepairHeader H LEFT JOIN  tbl_RepairHeader I ON I.RepairID=H.RepairID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  " & _
                                        " where H.IsDelete=0 AND I.IsDelete=0 And C.IsDelete=0 And (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) >0  " & criStr & _
                                        " and IsAllReturn = 1 and H.RepairID Not In (Select VoucherNo from tbl_CashReceipt Where Type='RepairReturn' and IsDelete=0  )" & _
                                        " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID and IsDelete=0))as C  " & _
                                        " UNION ALL " & _
                                         " Select * from (select VoucherNo,  SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address, " & _
                                         " ((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount, (PaidAmount+PurchaseAmount+OtherCashAmount) as PaidAmount,PayAmount,   " & _
                                         " (PaidAmount+PayAmount+PurchaseAmount+OtherCashAmount) as TotalPaidAmount,(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount+PurchaseAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleLooseDiamondHeader I   left join tbl_Customer C on I.CustomerID=C.CustomerID  , " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SaleLooseDiamond' and IsDelete=0 group by VoucherNo ) as Debt " & _
                                         " where I.SaleLooseDiamondID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+OtherCashAmount)) > 0  " & criStr & " union all " & _
                                         " select SaleLooseDiamondID as VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                         " ((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount, (PaidAmount+PurchaseAmount+OtherCashAmount) as PaidAmount, 0 as PayAmount, " & _
                                         " (PaidAmount+PurchaseAmount+OtherCashAmount) as TotalPaidAmount,   (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleLooseDiamondHeader I  left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                                         " where I.IsDelete=0 AND C.IsDelete=0 AND (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PurchaseAmount+OtherCashAmount)) >0 " & criStr & " and SaleLooseDiamondID Not In " & _
                                         " (Select VoucherNo from tbl_CashReceipt Where Type='SaleLooseDiamond' and IsDelete=0 ))as L " & _
                                        " UNION ALL " & _
                                        " Select * from (select VoucherNo, SaleDate as PDate , I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,PaidAmount,PayAmount, " & _
                                        " PaidAmount+PayAmount as TotalPaidAmount,(((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount+PayAmount))  as DebtAmount " & _
                                        " from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID  ," & _
                                        " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoiceVolume' and IsDelete=0  group by VoucherNo ) as Debt where I.SalesVolumeID=Debt.VoucherNo and I.IsDelete=0 and C.IsDelete=0 " & _
                                        " and (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-(PaidAmount)) >0  " & criStr & " union all " & _
                                        " select SalesVolumeID as VoucherNo, SaleDate as PDate , I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt)) as NetAmount,PaidAmount,0 as PayAmount,PaidAmount as TotalPaidAmount, " & _
                                        " (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-PaidAmount) as DebtAmount from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                                        " where I.IsDelete=0 and C.IsDelete=0 And (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))-PaidAmount)>0 " & criStr & "and SalesVolumeID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoiceVolume' And IsDelete=0 ))as D)as M  WHERE 1=1 " & Str & " order by VoucherNo"

                End Select
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

        Function GetDebtInDataByType(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "", Optional ByVal Str As String = "") As DataTable Implements ICashReceiptDA.GetDebtInDataByType
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                'If Str = "All" Then
                '    Str = ""
                'Else
                '    Str = "And PayDate>='" & Format(FromDate, "yyyy/MM/dd") & "' AND PayDate<= '" & Format(ToDate, "yyyy/MM/dd") & "'"
                'End If


                Select Case argType
                    Case "PurchaseGems"
                        strCommandText = "SELECT D.VoucherNo, I.PDate, I.CustomerID, I.Customer AS Customer, PayDate, PayAmount, D.Remark  " & _
                        " FROM tbl_CashReceipt D INNER JOIN tbl_PurchaseGems I ON D.VoucherNo=I.PurchaseGemsID " & _
                        " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID " & _
                        " WHERE 1=1   " & criStr & " " & Str & " and Type='PurchaseGems' and D.IsDelete=0 order by Customer,VoucherNo "

                    Case "SaleGems"
                        strCommandText = "SELECT D.VoucherNo, I.SDate As PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_SaleGems I ON D.VoucherNo=I.SaleGemsID " & _
                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID " & _
                         " WHERE 1=1   " & criStr & " " & Str & " and Type='SalesGems' and D.IsDelete=0  Order by Customer,VoucherNo "

                    Case "WholeSalesInvoice"
                        strCommandText = "SELECT D.VoucherNo, WDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark   FROM tbl_CashReceipt D " & _
                                        "INNER JOIN tbl_WholesaleInvoice I ON D.VoucherNo=I.WholeSaleInvoiceID  left join tbl_Customer C on I.CustomerID=C.CustomerID   " & _
                                        "WHERE I.IsDelete=0 " & criStr & " " & Str & " and Type='WholeSalesInvoice'  and D.IsDelete=0 order by Customer,VoucherNo "

                    Case "ConsignmentSaleInvoice"
                        strCommandText = "SELECT D.VoucherNo, ConsignDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark   " & _
                                        " FROM tbl_CashReceipt D INNER JOIN tbl_ConsignmentSale I ON D.VoucherNo=I.ConsignmentSaleID  " & _
                                        " left join tbl_Customer C on I.CustomerID=C.CustomerID   " & _
                                        " WHERE I.IsDelete=0 " & criStr & " " & Str & " and Type='ConsignmentSaleInvoice'  and D.IsDelete=0 order by Customer,VoucherNo "

                    Case "PurchaseInvoice"
                        strCommandText = "SELECT D.VoucherNo, I.PurchaseDate as PDate,  I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_PurchaseHeader I ON D.VoucherNo=I.PurchaseHeaderID " & _
                         " left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                         " WHERE 1=1   " & criStr & " " & Str & " and Type='PurchaseInvoice' and D.IsDelete=0 order by Customer,VoucherNo "

                    Case "SaleInvoice"
                        strCommandText = "SELECT D.VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_SaleInvoiceHeader I ON D.VoucherNo=I.SaleInvoiceHeaderID " & _
                         " left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                         " WHERE I.IsDelete=0   " & criStr & " " & Str & "and Type='SalesInvoice' and D.IsDelete=0 order by Customer,VoucherNo "

                    Case "OrderInvoice"
                        strCommandText = "SELECT D.VoucherNo, H.ReturnDate as PDate, O.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_OrderReturnHeader H ON D.VoucherNo=H.OrderInvoiceID " & _
                         " LEFT JOIN tbl_OrderInvoice O ON H.OrderInvoiceID=O.OrderInvoiceID " & _
                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=H.LocationID " & _
                         " left join tbl_Customer C on O.CustomerID=C.CustomerID " & _
                         " WHERE 1=1   " & criStr & " " & Str & " AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID) and Type='OrderInvoice' and D.IsDelete=0  order by Customer,VoucherNo "

                    Case "RepairReturn"
                        strCommandText = "SELECT H.RepairID AS VoucherNo, H.ReturnDate as PDate, O.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_ReturnRepairHeader H ON D.VoucherNo=H.RepairID " & _
                         " LEFT JOIN tbl_RepairHeader O ON H.RepairID=O.RepairID " & _
                         " left join tbl_Customer C on O.CustomerID=C.CustomerID " & _
                         " WHERE 1=1   " & criStr & " " & Str & " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID) and Type='RepairReturn' and D.IsDelete=0 order by Customer,VoucherNo "
                    Case "SalesVolume"
                        strCommandText = "Select D.VoucherNo,I.SaleDate as PDate, I.CustomerID, C.CustomerName As Customer, PayDate,PayAmount,D.Remark" & _
                                        " From tbl_CashReceipt D Inner Join tbl_SalesVolume I On D.VoucherNo=I.SalesVolumeID left Join tbl_Customer C on I.CustomerID=C.CustomerID" & _
                                        " WHERE 1=1   " & criStr & " " & Str & "and Type='SalesInvoiceVolume' and D.IsDelete=0  order by Customer,VoucherNo "
                    Case "SaleLooseDiamond"
                        strCommandText = "SELECT D.VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_SaleLooseDiamondHeader I ON D.VoucherNo=I.SaleLooseDiamondID " & _
                         " left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                         " WHERE I.IsDelete=0   " & criStr & " " & Str & "and Type='SaleLooseDiamond' and D.IsDelete=0 order by Customer,VoucherNo "

                    Case "All"
                        strCommandText = " Select * from (SELECT D.VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_SaleInvoiceHeader I ON D.VoucherNo=I.SaleInvoiceHeaderID " & _
                         " left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                         " WHERE I.IsDelete=0   " & criStr & " " & Str & "and Type='SalesInvoice' and D.IsDelete=0 " & _
                         " UNION ALL " & _
                         " SELECT D.VoucherNo, WDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark   FROM tbl_CashReceipt D " & _
                         " INNER JOIN tbl_WholesaleInvoice I ON D.VoucherNo=I.WholesaleInvoiceID  left join tbl_Customer C on I.CustomerID=C.CustomerID  WHERE I.PayType<>1 and I.IsDelete=0  " & criStr & " " & Str & "and Type='WholeSalesInvoice' and D.IsDelete=0 " & _
                         " UNION ALL " & _
                         " SELECT D.VoucherNo, ConsignDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark   FROM tbl_CashReceipt D " & _
                         " INNER JOIN tbl_ConsignmentSale I ON D.VoucherNo=I.ConsignmentSaleID  left join tbl_Customer C on I.CustomerID=C.CustomerID  WHERE I.IsDelete=0  " & criStr & " " & Str & "and Type='ConsignmentSaleInvoice' and D.IsDelete=0 " & _
                         " UNION ALL " & _
                         " SELECT D.VoucherNo, H.ReturnDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_OrderReturnHeader H ON D.VoucherNo=H.OrderInvoiceID " & _
                         " LEFT JOIN tbl_OrderInvoice I ON H.OrderInvoiceID=I.OrderInvoiceID " & _
                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=H.LocationID " & _
                         " left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                         " WHERE 1=1   " & criStr & " " & Str & " AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID) and Type='OrderInvoice' and D.IsDelete=0 " & _
                         " UNION ALL " & _
                         " SELECT D.VoucherNo, I.SDate As PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_SaleGems I ON D.VoucherNo=I.SaleGemsID " & _
                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID " & _
                         " WHERE 1=1   " & criStr & " " & Str & " and Type='SalesGems' and D.IsDelete=0 " & _
                         " UNION ALL " & _
                         " SELECT H.RepairID AS VoucherNo, H.ReturnDate as PDate, I.CustomerID, C.CustomerName AS Customer, PayDate, PayAmount, D.Remark  " & _
                         " FROM tbl_CashReceipt D INNER JOIN tbl_ReturnRepairHeader H ON D.VoucherNo=H.RepairID " & _
                         " LEFT JOIN tbl_RepairHeader I ON H.RepairID=I.RepairID " & _
                         " left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                         " WHERE 1=1   " & criStr & " " & Str & " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID) and Type='RepairReturn' and D.IsDelete=0 " & _
                         " UNION ALL " & _
                         " Select D.VoucherNo,I.SaleDate as PDate, I.CustomerID, C.CustomerName As Customer, PayDate,PayAmount,D.Remark" & _
                         " From tbl_CashReceipt D Inner Join tbl_SalesVolume I On D.VoucherNo=I.SalesVolumeID left Join tbl_Customer C on I.CustomerID=C.CustomerID" & _
                         " WHERE 1=1   " & criStr & " " & Str & "and Type='SalesInvoiceVolume' and D.IsDelete=0 ) AS M order by Customer,VoucherNo "

                End Select
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

        Public Function GetPurchaseHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptDA.GetPurchaseHeaderCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash = True Then
                    strCommandText = " Select PurchaseHeaderID AS [@PurchaseHeaderID], PurchaseHeaderID AS VoucherNo, P.IsGem as [$IsGem], convert(varchar(10),PurchaseDate,105) As PurchaseDate, C.CustomerName as [Customer_]," & _
                       " AllTotalAmount,AllAddOrSub,AllTotalAmount-AllAddOrSub as [NetAmount]," & _
                       " (AllPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=PurchaseHeaderID AND Type='PurchaseInvoice' and IsDelete=0 ),0)) as [PaidAmount], " & _
                       " ((AllTotalAmount-AllAddOrSub)-(AllPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=PurchaseHeaderID AND Type='PurchaseInvoice' and IsDelete=0 ),0))) as [BalanceAmount],P.PurchaseDate as [@PDate]" & _
                       " From tbl_PurchaseHeader P left join tbl_Customer C on P.CustomerID=C.CustomerID" & _
                       " WHERE ((AllTotalAmount-AllAddOrSub)-(AllPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=PurchaseHeaderID AND Type='PurchaseInvoice' and IsDelete=0 ),0))) = 0" & _
                       " AND (AllTotalAmount - AllAddOrSub) - AllPaidAmount <> 0" & _
                      " order by [@PDate] desc, PurchaseHeaderID DESC"
                Else
                    strCommandText = " Select PurchaseHeaderID AS [@PurchaseHeaderID], PurchaseHeaderID AS VoucherNo,P.IsGem as [$IsGem],convert(varchar(10),PurchaseDate,105) As PurchaseDate,C.CustomerName as [Customer_]," & _
                      " AllTotalAmount,AllAddOrSub,AllTotalAmount-AllAddOrSub as [NetAmount]," & _
                      " (AllPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=PurchaseHeaderID AND Type='PurchaseInvoice' and IsDelete=0 ),0)) as [PaidAmount], ((AllTotalAmount-AllAddOrSub)-(AllPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=PurchaseHeaderID AND Type='PurchaseInvoice'),0))) as [BalanceAmount],P.PurchaseDate as [@PDate]" & _
                      " From tbl_PurchaseHeader P left join tbl_Customer C on P.CustomerID=C.CustomerID" & _
                      " where ((AllTotalAmount-AllAddOrSub)-(AllPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=PurchaseHeaderID AND Type='PurchaseInvoice' and IsDelete=0 ),0))) <> 0" & _
                      " order by [@PDate] desc, PurchaseHeaderID DESC"
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleInvoiceHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptDA.GetSaleInvoiceHeaderCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash Then
                    strCommandText = "Select C.CustomerName as [Customer_], SaleInvoiceHeaderID AS VoucherNo,convert(varchar(10),SaleDate,105) As SaleDate," & _
                            " TotalAmount,AddOrSub, DiscountAmount, ((TotalAmount*PromotionDiscount)/100) AS PromotionAmount, RedeemValue,MemberDiscountAmt, ((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100))) as [NetAmount]," & _
                            " (PaidAmount+AllAdvanceAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleInvoiceHeaderID AND Type='SalesInvoice' and IsDelete=0 ),0)) as [PaidAmount],(((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+AllAdvanceAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleInvoiceHeaderID AND Type='SalesInvoice'),0))) as [BalanceAmount],SaleDate as [@SDate]" & _
                            " From tbl_SaleInvoiceHeader S left join tbl_Customer C on S.CustomerID=C.CustomerID" & _
                            " where S.IsDelete=0 AND (((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+AllAdvanceAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleInvoiceHeaderID AND Type='SalesInvoice' and IsDelete=0),0))) = 0" & _
                            " AND (((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+AllAdvanceAmount+PurchaseAmount+OtherCashAmount))<>0  AND IsCancel=0 " & _
                            " order by [@SDate] desc, SaleInvoiceHeaderID DESC"
                Else
                    strCommandText = "Select C.CustomerName as [Customer_],SaleInvoiceHeaderID AS VoucherNo,convert(varchar(10),SaleDate,105) As SaleDate," & _
                           " TotalAmount,AddOrSub, DiscountAmount, RedeemValue,MemberDiscountAmt,((TotalAmount*PromotionDiscount)/100) AS PromotionAmount, ((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100))) as [NetAmount]," & _
                           " (PaidAmount+AllAdvanceAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleInvoiceHeaderID AND Type='SalesInvoice' and IsDelete=0),0)) as [PaidAmount],(((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+AllAdvanceAmount+PurchaseAmount+OtherCashAmount+RedeemValue+MemberDiscountAmt+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleInvoiceHeaderID AND Type='SalesInvoice'),0))) as [BalanceAmount],SaleDate as [@SDate]" & _
                           " From tbl_SaleInvoiceHeader S left join tbl_Customer C on S.CustomerID=C.CustomerID" & _
                           " where S.IsDelete=0 AND (((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+AllAdvanceAmount+PurchaseAmount+OtherCashAmount+RedeemValue+MemberDiscountAmt+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleInvoiceHeaderID AND Type='SalesInvoice' and IsDelete=0),0))) <> 0 AND IsCancel=0 " & _
                           " order by [@SDate] desc, SaleInvoiceHeaderID DESC"
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetWholeSaleInvoiceHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptDA.GetWholeSaleInvoiceHeaderCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash Then
                    strCommandText = "select C.CustomerName as [Customer_],W.WholesaleInvoiceID as VoucherNo,Convert(varchar(10),W.WDate,103) as WDate,NetAmount As TotalAmount,AddOrSub,Discount,RedeemValue,MemberDiscountAmt, " & _
                                    " ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as [NetAmount], " & _
                                    " (PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=WholeSaleInvoiceID " & _
                                    " AND Type='WholeSaleInvoice' and IsDelete=0),0)) as [PaidAmount],  " & _
                                    " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=WholeSaleInvoiceID AND Type='WholeSalesInvoice' and IsDelete=0),0))) as [BalanceAmount],  " & _
                                    " WDate as [@WDate] from tbl_WholeSaleInvoice W " & _
                                    " left Join tbl_Customer C on W.CustomerID=C.CustomerID  Where W.IsDelete=0 AND W.PayType='2' AND " & _
                                    " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt  " & _
                                    " WHERE VoucherNo=WholeSaleInvoiceID AND Type='WholeSalesInvoice' and IsDelete=0),0)))=0  AND " & _
                                    " ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)-(PaidAmount))<>0 " & _
                                    " order by [@WDate] desc,WholesaleInvoiceID DESC "

                Else
                    strCommandText = "select C.CustomerName as [Customer_],W.WholesaleInvoiceID as VoucherNo,Convert(varchar(10),W.WDate,103) as WDate,NetAmount As TotalAmount,AddOrSub,Discount,RedeemValue,MemberDiscountAmt," & _
                                    " ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as [NetAmount],(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=WholeSaleInvoiceID AND Type='WholeSaleInvoice' and IsDelete=0),0)) as [PaidAmount], " & _
                                    " (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=WholeSaleInvoiceID AND Type='WholeSalesInvoice' and IsDelete=0),0))) as [BalanceAmount], " & _
                                    " WDate as [@WDate] from tbl_WholeSaleInvoice W left Join tbl_Customer C on W.CustomerID=C.CustomerID " & _
                                    " Where W.IsDelete=0 AND W.PayType='2'  AND (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt " & _
                                    " WHERE VoucherNo=WholeSaleInvoiceID AND Type='WholeSalesInvoice' and IsDelete=0),0)))<>0 " & _
                                    " order by [@WDate] desc,WholesaleInvoiceID DESC"

                End If


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetConsignmentSaleCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptDA.GetConsignmentSaleCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash Then
                    strCommandText = "select C.CustomerName as [Customer_],H.ConsignmentSaleID as VoucherNo,Convert(varchar(10),H.ConsignDate,103) as WDate,NetAmount As TotalAmount,AddOrSub,Discount,RedeemValue,MemberDiscountAmt,  " & _
                                    " ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as [NetAmount],  (PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=ConsignmentSaleID  AND Type='ConsignmentSaleInvoice' " & _
                                    " and IsDelete=0),0)) as [PaidAmount],   (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=ConsignmentSaleID " & _
                                    " AND Type='ConsignmentSaleInvoice' and IsDelete=0),0))) as [BalanceAmount],   ConsignDate as [@ConsignDate] from tbl_ConsignmentSale H  left Join tbl_Customer C on H.CustomerID=C.CustomerID  " & _
                                    " Where H.IsDelete=0 AND  (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt   WHERE VoucherNo=ConsignmentSaleID AND Type='ConsignmentSaleInvoice' " & _
                                    " and IsDelete=0),0)))=0  AND  ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)-(PaidAmount))<>0  order by [@ConsignDate] desc,ConsignmentSaleID DESC "

                Else
                    strCommandText = "select C.CustomerName as [Customer_],H.ConsignmentSaleID as VoucherNo,Convert(varchar(10),H.ConsignDate,103) as ConsignDate,NetAmount As TotalAmount,AddOrSub,Discount,RedeemValue,MemberDiscountAmt, " & _
                                    " ((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt)) as [NetAmount],(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=ConsignmentSaleID AND Type='ConsignmentSaleInvoice' " & _
                                    " and IsDelete=0),0)) as [PaidAmount],  (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=ConsignmentSaleID " & _
                                    " AND Type='ConsignmentSaleInvoice' and IsDelete=0),0))) as [BalanceAmount],  ConsignDate as [@ConsignDate] from tbl_ConsignmentSale H left Join tbl_Customer C on H.CustomerID=C.CustomerID  " & _
                                    " Where H.IsDelete=0 AND (((NetAmount+AddOrSub)-(Discount+RedeemValue+MemberDiscountAmt))-(PaidAmount+IsNull((select Sum(PayAmount) FROM tbl_CashReceipt  WHERE VoucherNo=ConsignmentSaleID AND Type='ConsignmentSaleInvoice' " & _
                                    " and IsDelete=0),0)))<>0  order by [@ConsignDate] desc,ConsignmentSaleID DESC"

                End If


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GerSaleInvoiceVolumeCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptDA.GerSaleInvoiceVolumeCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash = True Then
                    strCommandText = "Select C.CustomerName As [Customer_], SalesVolumeID AS VoucherNo,Convert(varchar(10),SaleDate,105) as SaleDate,TotalAmount As [TotalAmount]," &
                               " AddOrSub As [AddOrSubAmount],DiscountAmount,PromotionDiscount,RedeemValue+MemberDiscountAmt, (PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SalesVolumeID AND Type='SalesInvoiceVolume' and IsDelete=0 ),0)) AS [PaidAmount],((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100))) As [NetAmount]," &
                               " (((TotalAmount+AddOrSub)-(DiscountAmount+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+RedeemValue+MemberDiscountAmt+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SalesVolumeID AND Type='SalesInvoiceVolume' and IsDelete=0 ),0))) as [BalanceAmount],SaleDate as [@SDate]" &
                               " From tbl_SalesVolume S Left Join tbl_Customer C on C.CustomerID=S.CustomerID " &
                               " where (((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+(((TotalAmount+AddOrSub)*PromotionDiscount)/100)))-(PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SalesVolumeID AND Type='SalesInvoiceVolume' and IsDelete=0 ),0))) = 0" & _
                               " AND (((TotalAmount+AddOrSub)-(DiscountAmount+((TotalAmount*PromotionDiscount)/100)))-PaidAmount) <>0 " & _
                               " order by [@SDate] desc, SalesVolumeID DESC"
                Else
                    strCommandText = "Select C.CustomerName As [Customer_], SalesVolumeID AS VoucherNo,Convert(varchar(10),SaleDate,105) as SaleDate,TotalAmount As [TotalAmount]," &
                              " AddOrSub As [AddOrSubAmount],DiscountAmount,PromotionDiscount,RedeemValue+MemberDiscountAmt+ (PaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SalesVolumeID AND Type='SalesInvoiceVolume' and IsDelete=0 ),0)) AS [PaidAmount],((TotalAmount+AddOrSub)-(DiscountAmount+((TotalAmount*PromotionDiscount)/100))) As [NetAmount]," &
                              " (((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+PurchaseAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SalesVolumeID AND Type='SalesInvoiceVolume' and IsDelete=0 ),0))) as [BalanceAmount],SaleDate as [@SDate]" &
                              " From tbl_SalesVolume S LEFT Join tbl_Customer C on C.CustomerID=S.CustomerID " &
                              " where (((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+PurchaseAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SalesVolumeID AND Type='SalesInvoiceVolume' and IsDelete=0 ),0))) <> 0" & _
                              " order by [@SDate] desc, SalesVolumeID DESC"
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetCashReceiptforPrint(ByVal VoucherNo As String, Optional ByVal Type As String = "") As DataTable Implements ICashReceiptDA.GetCashReceiptforPrint
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try


                If Type = "PurchaseInvoice" Then
                    strCommandText = " SELECT  C.CashReceiptID, C.VoucherNo, Convert(varchar(10),C.PayDate,105) as PayDate, P.PurchaseDate AS Date, C.PayAmount,P.AllAddOrSub,(P.AllTotalAmount-P.AllAddOrSub) As NetAmount,C.Remark, " & _
                                     "  P.AllPaidAmount as PaidAmount, 0 AS AllPaidAmount, T.CustomerCode, T.CustomerName As Customer,ReturnAdvanceID FROM tbl_CashReceipt C " & _
                                     " INNER JOIN tbl_PurchaseHeader P ON P.PurchaseHeaderID=C.VoucherNo INNER JOIN tbl_Customer T ON T.CustomerID=P.CustomerID WHERE C.VoucherNo='" & VoucherNo & "' AND C.Type='" & Type & "' and C.IsDelete=0 " & _
                                     " GROUP BY P.AllPaidAmount,AllTotalAmount ,C.PayAmount,C.CashReceiptID, C.VoucherNo,C.Remark,C.PayDate, P.PurchaseDate, T.CustomerCode, T.CustomerName,P.AllAddOrSub,C.ReturnAdvanceID "

                ElseIf Type = "SalesInvoice" Then
                    strCommandText = " SELECT C.CashReceiptID, C.VoucherNo, Convert(varchar(10),C.PayDate, 105) As PayDate, S.SaleDate AS Date, C.PayAmount,S.AddOrSub,((S.TotalAmount+S.AddOrSub)-(S.DiscountAmount+((S.TotalAmount*S.PromotionDiscount)/100))) as NetAmount," & _
                                     " (S.PaidAmount+S.AllAdvanceAmount+S.PurchaseAmount) as PaidAmount, C.Remark,T.CustomerCode, T.CustomerName As Customer,0 AS AllPaidAmount,ReturnAdvanceID FROM tbl_CashReceipt C INNER JOIN tbl_SaleInvoiceHeader S ON S.SaleInvoiceHeaderID= C.VoucherNo " & _
                                     " INNER JOIN tbl_Customer T ON T.CustomerID=S.CustomerID   WHERE S.IsDelete=0 AND C.VoucherNo='" & VoucherNo & "' AND C.Type='" & Type & "' and C.IsDelete=0 " & _
                                     " GROUP BY C.PayAmount,S.TotalAmount,S.PaidAmount, S.AllAdvanceAmount, S.PurchaseAmount, C.CashReceiptID, C.VoucherNo,C.PayDate,  S.SaleDate, C.Remark,T.CustomerCode, T.CustomerName,S.AddOrSub,S.DiscountAmount,S.PromotionDiscount,C.ReturnAdvanceID"

                ElseIf Type = "WholeSalesInvoice" Then
                    strCommandText = "  SELECT C.CashReceiptID, C.VoucherNo, Convert(varchar(10),C.PayDate, 105) As PayDate, S.WDate AS Date, C.PayAmount,S.AddOrSub," & _
                                     " ((S.NetAmount+S.AddOrSub)-(S.Discount)) as NetAmount, " & _
                                     " (S.PaidAmount) as PaidAmount, C.Remark,T.CustomerCode, T.CustomerName As Customer,0 AS AllPaidAmount,ReturnAdvanceID  " & _
                                     " FROM tbl_CashReceipt C INNER JOIN tbl_WholesaleInvoice S ON S.WholesaleInvoiceID= C.VoucherNo  " & _
                                     " INNER JOIN tbl_Customer T ON T.CustomerID=S.CustomerID   WHERE S.IsDelete=0  AND C.VoucherNo='" & VoucherNo & "' AND C.Type='" & Type & "'  and C.IsDelete=0  GROUP BY C.PayAmount,S.NetAmount,S.PaidAmount," & _
                                     " C.CashReceiptID, C.VoucherNo,C.PayDate,  S.WDate, C.Remark,T.CustomerCode, T.CustomerName,S.AddOrSub,S.Discount,C.ReturnAdvanceID "

                ElseIf Type = "SalesGems" Then
                    strCommandText = " SELECT C.CashReceiptID, C.VoucherNo,Convert(varchar(10),C.PayDate, 105) As PayDate, S.SDate AS Date, C.PayAmount,((S.TotalAmount+S.AddOrSub)-((((S.TotalAmount+S.AddOrSub)*S.PromotionDiscount)/100)+S.DiscountAmount))As  NetAmount, S.PaidAmount, C.Remark,T.CustomerCode, T.CustomerName As Customer,0 AS AllPaidAmount,ReturnAdvanceID  " & _
                                     " FROM tbl_CashReceipt C INNER JOIN tbl_SaleGems S ON S.SaleGemsID=C.VoucherNo INNER JOIN tbl_Customer T ON T.CustomerID=S.CustomerID " & _
                                     " WHERE C.VoucherNo='" & VoucherNo & "' AND C.Type='" & Type & "' and C.IsDelete=0 GROUP BY C.PayAmount,S.TotalAmount,S.PaidAmount ,C.CashReceiptID, C.VoucherNo,C.PayDate, S.SDate, C.Remark,T.CustomerCode, T.CustomerName,S.AddOrSub,S.PromotionDiscount,S.DiscountAmount,C.ReturnAdvanceID"

                ElseIf Type = "SalesInvoiceVolume" Then
                    strCommandText = " SELECT C.CashReceiptID, C.VoucherNo, Convert(varchar(10),C.PayDate, 105) As PayDate, S.SaleDate AS Date, C.PayAmount,((S.TotalAmount+S.AddOrSub)-(S.DiscountAmount+(((S.TotalAmount+AddOrSub)*S.PromotionDiscount)/100))) As NetAmount, S.PaidAmount, C.Remark,T.CustomerCode, T.CustomerName As Customer,0 AS AllPaidAmount,ReturnAdvanceID   " & _
                                     " FROM tbl_CashReceipt C INNER JOIN tbl_SalesVolume S ON S.SalesVolumeID=C.VoucherNO INNER JOIN tbl_Customer T ON T.CustomerID=S.CustomerID WHERE C.VoucherNo='" & VoucherNo & "' AND C.Type='" & Type & "' and C.IsDelete=0 " & _
                                     " GROUP BY C.PayAmount,S.TotalAmount,S.PaidAmount ,C.CashReceiptID, C.VoucherNo,C.PayDate, S.SaleDate, C.Remark, T.CustomerCode, T.CustomerName,S.AddOrSub,S.PromotionDiscount,S.DiscountAmount,C.ReturnAdvanceID"

                ElseIf Type = "OrderInvoice" Then
                    strCommandText = "SELECT C.CashReceiptID, C.VoucherNo,Convert(varchar(10),C.PayDate, 105) As PayDate, H.ReturnDate AS Date, C.PayAmount, ((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount) As NetAmount, " & _
                                     " (H.AdvanceAmount+H.PaidAmount+H.FromGoldAmount) As PaidAmount,C.Remark,T.CustomerCode,T.CustomerName As Customer,0 AS AllPaidAmount,ReturnAdvanceID " & _
                                      " From tbl_CashReceipt C INNER JOIN tbl_OrderReturnHeader H  ON C.VoucherNo=H.OrderInvoiceID INNER JOIN tbl_OrderInvoice R ON R.OrderInvoiceID=H.OrderInvoiceID INNER JOIN tbl_Customer T ON T.CustomerID=R.CustomerID " & _
                                     " WHERE C.VoucherNo='" & VoucherNo & "' AND C.Type=' " & Type & "' and C.IsDelete=0 AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader RR WHERE RR.OrderInvoiceID=H.OrderInvoiceID) " & _
                                     " GROUP BY C.PayAmount,H.AllTotalAmount,H.PaidAmount ,C.CashReceiptID,H.DiscountAmount,H.AllAddOrSub, C.VoucherNo,C.PayDate, H.ReturnDate, C.Remark,T.CustomerCode, T.CustomerName,H.AdvanceAmount,H.FromGoldAmount,C.ReturnAdvanceID"

                ElseIf Type = "RepairReturn" Then
                    strCommandText = "SELECT C.CashReceiptID, C.VoucherNo,Convert(varchar(10),C.PayDate, 105) As PayDate, H.ReturnDate AS Date, C.PayAmount, ((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) As NetAmount, " & _
                                     " (H.AdvanceAmount+H.ReturnPaidAmount) As PaidAmount,C.Remark,T.CustomerCode,T.CustomerName As Customer,0 AS AllPaidAmount,ReturnAdvanceID " & _
                                      " From tbl_CashReceipt C INNER JOIN tbl_ReturnRepairHeader H  ON C.VoucherNo=H.RepairID INNER JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID INNER JOIN tbl_Customer T ON T.CustomerID=R.CustomerID " & _
                                     " WHERE C.VoucherNo='" & VoucherNo & "' AND C.Type='" & Type & "'and C.IsDelete=0 AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader RR WHERE RR.RepairID=H.RepairID) " & _
                                     " GROUP BY C.PayAmount,H.AllReturnTotalAmount,H.ReturnPaidAmount ,C.CashReceiptID,H.ReturnDiscountAmount,H.AllReturnAddOrSub, C.VoucherNo,C.PayDate, H.ReturnDate, C.Remark,T.CustomerCode, T.CustomerName,H.AdvanceAmount,C.ReturnAdvanceID"
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetReturnRepairStockCashReceipt(IsCash As Boolean) As DataTable Implements ICashReceiptDA.GetReturnRepairStockCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash = True Then
                    strCommandText = "Select H.ReturnRepairID AS [@ReturnRepairID], C.CustomerName as [Customer_],H.RepairID AS VoucherNo,convert(varchar(10),H.ReturnDate,105) As ReturnDate," & _
                                   " H.AllReturnTotalAmount as [TotalAmount], ((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) as [NetAmount], H.AllReturnAddOrSub as [AddOrSub]," & _
                                   "  H.ReturnDiscountAmount AS [DiscountAmount], (H.AdvanceAmount+H.ReturnPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.RepairID AND Type='RepairReturn' and IsDelete=0 ),0)) as [PaidAmount], " & _
                                   "  (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-ReturnDiscountAmount)-(H.AdvanceAmount+H.ReturnPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.RepairID AND Type='RepairReturn' and IsDelete=0),0))) as [Balance Amount  ], H.ReturnDate as [@HideReturnDate] " & _
                                   " From tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID " & _
                                   " where (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-ReturnDiscountAmount)-(H.AdvanceAmount+H.ReturnPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.RepairID AND Type='RepairReturn' and IsDelete=0 ),0))) =0 and R.IsAllReturn=1  " & _
                                   " AND (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-ReturnDiscountAmount)-(H.AdvanceAmount+H.ReturnPaidAmount)) <>0 " & _
                                   " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader A WHERE A.RepairID=H.RepairID) " & _
                                   " ORDER By [@HideReturnDate] DESC , H.RepairID DESC "
                Else
                    strCommandText = "Select H.ReturnRepairID AS [@ReturnRepairID], C.CustomerName as [Customer_], H.RepairID AS VoucherNo,convert(varchar(10),H.ReturnDate,105) As ReturnDate, " & _
                                     " H.AllReturnTotalAmount as [TotalAmount], ((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) as [NetAmount], H.AllReturnAddOrSub as [AddOrSub]," & _
                                     " H.ReturnDiscountAmount AS [DiscountAmount], (H.AdvanceAmount+H.ReturnPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.RepairID AND Type='RepairReturn' and IsDelete=0 ),0)) as [PaidAmount], " & _
                                     " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-ReturnDiscountAmount)-(H.AdvanceAmount+H.ReturnPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.RepairID AND Type='RepairReturn' and IsDelete=0 ),0))) as [Balance Amount  ], H.ReturnDate as [@HideReturnDate] " & _
                                     " From tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID " & _
                                     " where (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-ReturnDiscountAmount)-(H.AdvanceAmount+H.ReturnPaidAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=H.RepairID AND Type='RepairReturn' and IsDelete=0 ),0))) <>0 and R.IsAllReturn=1  " & _
                                     " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader A WHERE A.RepairID=H.RepairID) " & _
                                     " ORDER By [@HideReturnDate] DESC , H.RepairID DESC "
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSaleLooseDiamondHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptDA.GetSaleLooseDiamondHeaderCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If IsCash Then
                    strCommandText = "Select C.CustomerName as [Customer_], SaleLooseDiamondID AS VoucherNo,convert(varchar(10),SaleDate,105) As SaleDate," & _
                            " TotalAmount,AddOrSub, DiscountAmount,RedeemValue,MemberDiscountAmt, ((TotalAmount*PromotionDiscount)/100) AS PromotionAmount, ((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100))) as [NetAmount]," & _
                            " (PaidAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleLooseDiamondID AND Type='SaleLooseDiamond' and IsDelete=0 ),0)) as [PaidAmount],(((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleLooseDiamondID AND Type='SaleLooseDiamond'),0))) as [BalanceAmount],SaleDate as [@SDate]" & _
                            " From tbl_SaleLooseDiamondHeader S left join tbl_Customer C on S.CustomerID=C.CustomerID" & _
                            " where S.IsDelete=0 AND (((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleLooseDiamondID AND Type='SaleLooseDiamond' and IsDelete=0),0))) = 0" & _
                            " AND (((TotalAmount+AddOrSub)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+PurchaseAmount+OtherCashAmount))<>0 " & _
                            " order by [@SDate] desc, SaleLooseDiamondID DESC"
                Else
                    strCommandText = "Select C.CustomerName as [Customer_],SaleLooseDiamondID AS VoucherNo,convert(varchar(10),SaleDate,105) As SaleDate," & _
                           " TotalAmount,AddOrSub, DiscountAmount,RedeemValue,MemberDiscountAmt, ((TotalAmount*PromotionDiscount)/100) AS PromotionAmount, ((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100))) as [NetAmount]," & _
                           " (PaidAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleLooseDiamondID AND Type='SaleLooseDiamond' and IsDelete=0),0)) as [PaidAmount],(((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleLooseDiamondID AND Type='SaleLooseDiamond'),0))) as [BalanceAmount],SaleDate as [@SDate]" & _
                           " From tbl_SaleLooseDiamondHeader S left join tbl_Customer C on S.CustomerID=C.CustomerID" & _
                           " where S.IsDelete=0 AND (((TotalAmount+AddOrSub+AllTaxAmt)-(DiscountAmount+RedeemValue+MemberDiscountAmt+((TotalAmount*PromotionDiscount)/100)))-(PaidAmount+PurchaseAmount+OtherCashAmount+IsNUll((select Sum(PayAmount) FROM tbl_CashReceipt WHERE VoucherNo=SaleLooseDiamondID AND Type='SaleLooseDiamond' and IsDelete=0),0))) <> 0  " & _
                           " order by [@SDate] desc, SaleLooseDiamondID DESC"
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

