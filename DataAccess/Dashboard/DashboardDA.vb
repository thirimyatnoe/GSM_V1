Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Dashboard
    Public Class DashboardDA
        Implements IDashboardDA

#Region "Private Location"

        Private Shared ReadOnly _instance As IDashboardDA = New DashboardDA
        Private DB As Database


#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDashboardDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetAllCashAndCredit(Optional ByVal SaleType As String = "", Optional ByVal Cristr As String = "", Optional ByVal DateType As String = "") As System.Data.DataTable Implements IDashboardDA.GetAllCashAndCredit
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If DateType = "Day " Then
                    If SaleType = "Sale" Then
                        strCommandText = "select Sum(PaidAmount) as SaleCashAmount,CONCAT(Day(SaleDate), '/',Convert(char(3),SaleDate,0)) As SaleDate,Month(SaleDate) as MSaleDate,Year(SaleDate),Sum((((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+RedeemValue+MemberDiscountAmt))" & _
                                         " -(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount))) as SaleCreditAmount " & _
                                         " from tbl_saleinvoiceheader where saleDate Between " & Cristr & " and isDelete=0 And  LocationId=" & Global_CurrentLocationID & " group by Day(SaleDate),Month(SaleDate),Year(SaleDate),SaleDate order by SaleDate asc "
                    Else
                        strCommandText = "select Sum(PaidAmount) as SaleCashAmount,CONCAT(Day(WDate), '/',Convert(char(3),WDate,0)) As SaleDate,Month(WDate) as MSaleDate,Year(WDate),Sum(NetAmount+AddOrSub-Discount-RedeemValue-MemberDiscountAmt-PaidAmount) as SaleCreditAmount " & _
                                         " from tbl_WholesaleInvoice where WDate Between " & Cristr & " and isDelete=0 And PayType=2 And LocationId=" & Global_CurrentLocationID & "  group by Day(WDate),Month(WDate),Year(WDate),WDate order by SaleDate asc "
                    End If
                ElseIf DateType = "Month" Then
                    If SaleType = "Sale" Then
                        strCommandText = "select Sum(PaidAmount) as SaleCashAmount,CONCAT(Month(saledate), '/',Year(saledate)) As SaleDate,Month(saledate) as MSaleDate,Year(saledate),Sum(TotalAmount+AddOrSub-DiscountAmount-RedeemValue-MemberDiscountAmt-PromotionDiscount-PurchaseAmount-AllAdvanceAmount-AllTaxAmt-PaidAmount) as SaleCreditAmount " & _
                                         " from tbl_saleinvoiceheader where saleDate Between " & Cristr & " and isDelete=0 And LocationId=" & Global_CurrentLocationID & "  group by Month(saledate),Year(saledate) order by Year(SaleDate) asc "
                    Else
                        strCommandText = "select Sum(PaidAmount) as SaleCashAmount,CONCAT(Month(WDate), '/',Year(WDate)) As SaleDate,Month(WDate) as MSaleDate,Year(WDate),Sum(NetAmount+AddOrSub-Discount-RedeemValue-MemberDiscountAmt-PaidAmount) as SaleCreditAmount " & _
                                         " from tbl_WholesaleInvoice where WDate Between " & Cristr & " and And PayType=2 isDelete=0  And LocationId=" & Global_CurrentLocationID & "  group by Month(WDate),Year(WDate) order by SaleDate asc "
                    End If
                ElseIf DateType = "Year" Then
                    If SaleType = "Sale" Then
                        strCommandText = "select Sum(PaidAmount) as SaleCashAmount,Year(saledate) As SaleDate,Sum(TotalAmount+AddOrSub-DiscountAmount-RedeemValue-MemberDiscountAmt-PromotionDiscount-PurchaseAmount-AllAdvanceAmount-AllTaxAmt-PaidAmount) as SaleCreditAmount " & _
                                         " from tbl_saleinvoiceheader where saleDate Between " & Cristr & " and isDelete=0 And LocationId=" & Global_CurrentLocationID & "  group by Year(saledate) order by SaleDate asc "
                    Else
                        strCommandText = "select Sum(PaidAmount) as SaleCashAmount,Year(WDate) As SaleDate,Sum(NetAmount+AddOrSub-Discount-RedeemValue-MemberDiscountAmt-PaidAmount) as SaleCreditAmount " & _
                                         " from tbl_WholesaleInvoice where WDate Between " & Cristr & " and isDelete=0 And PayType=2  And LocationId=" & Global_CurrentLocationID & "  group by Year(WDate) order by SaleDate asc "
                    End If
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllSaleByDate(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Type As String) As System.Data.DataTable Implements IDashboardDA.GetAllSaleByDate
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                    If Type = "Sale" Then
                    strCommandText = " select sum(D.TotalAmount+D.AddOrSub) as SaleAmount,'Diamond' as StockType,Sum(D.ItemTG) as TotalGram from tbl_SaleInvoiceHeader H " & _
                                     " join tbl_SaleInvoiceDetail D on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                     " join tbl_forSale F on F.ForSaleID=D.ForSaleID where H.isdelete=0 and H.LocationID=" & Global_CurrentLocationID & " and F.isdiamond=1" & _
                                     " and H.saleDate Between @FromDate and @ToDate " & _
                                     " Union All " & _
                                     " select sum(D.TotalAmount+D.AddOrSub) as SaleAmount,'Platinum' as StockType,Sum(D.ItemTG) as TotalGram from tbl_SaleInvoiceHeader H " & _
                                     " join tbl_SaleInvoiceDetail D on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                     " join tbl_forSale F on F.ForSaleID=D.ForSaleID " & _
                                     " join tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID  where H.isdelete=0 and H.LocationID=" & Global_CurrentLocationID & " and G.IsGramRate=1 " & _
                                     " and H.saleDate Between @FromDate and @ToDate " & _
                                     " Union All " & _
                                     " select sum(D.TotalAmount+D.AddOrSub) as SaleAmount,'Gold' as StockType,Sum(D.ItemTG) as TotalGram from tbl_SaleInvoiceHeader H " & _
                                     " join tbl_SaleInvoiceDetail D on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                     " join tbl_forSale F on F.ForSaleID=D.ForSaleID " & _
                                     " join tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID  where H.isdelete=0 and H.LocationID=" & Global_CurrentLocationID & " and G.IsGramRate=0 and F.IsDiamond=0 " & _
                                     " and H.saleDate Between @FromDate and @ToDate "
                    Else
                    strCommandText = " select sum(H.NetAmount) as SaleAmount,'Diamond' as StockType,Sum(D.ItemTG) as TotalGram from tbl_WholeSaleInvoice H " & _
                                 " join tbl_WholesaleInvoiceItem D on H.WholeSaleInvoiceID=D.WholeSaleInvoiceID " & _
                                 " join tbl_forSale F on F.ForSaleID=D.ForSaleID where H.isdelete=0 and H.LocationID=" & Global_CurrentLocationID & " and F.isdiamond=1" & _
                                  " and H.WDate Between @FromDate and @ToDate " & _
                                 " Union All " & _
                                 " select sum(H.NetAmount) as SaleAmount,'Platinum' as StockType,Sum(D.ItemTG) as TotalGram from tbl_WholeSaleInvoice H " & _
                                 " join tbl_WholesaleInvoiceItem D on H.WholeSaleInvoiceID=D.WholeSaleInvoiceID " & _
                                 " join tbl_forSale F on F.ForSaleID=D.ForSaleID " & _
                                 " join tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID  where H.isdelete=0 and H.LocationID=" & Global_CurrentLocationID & " and G.IsGramRate=1 " & _
                                 " and H.WDate Between @FromDate and @ToDate " & _
                                 " Union All " & _
                                 " select sum(H.NetAmount) as SaleAmount,'Gold' as StockType,Sum(D.ItemTG) as TotalGram from tbl_WholeSaleInvoice H " & _
                                 " join tbl_WholesaleInvoiceItem D on H.WholeSaleInvoiceID=D.WholeSaleInvoiceID " & _
                                 " join tbl_forSale F on F.ForSaleID=D.ForSaleID " & _
                                 " join tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID  where H.isdelete=0 and H.LocationID=" & Global_CurrentLocationID & " and G.IsGramRate=0 and F.IsDiamond=0 " & _
                                 " And H.WDate Between @FromDate and @ToDate "
                End If

                'If Type = "Sale" Then
                '    strCommandText = "select sum(TotalAmount) as SaleAmount,L.Location as Location From tbl_SaleInvoiceHeader H " & _
                '                    " Join tbl_Location L on H.LocationID=L.LocationID " & _
                '                    " where saleDate Between '" & FromDate & " 00:00:00' and '" & ToDate & " 23:59:59 ' group by L.Location "
                'Else
                '    strCommandText = "select sum(NetAmount) as SaleAmount,L.Location as Location From tbl_WholesaleInvoice H " & _
                '                    " Join tbl_Location L on H.LocationID=L.LocationID " & _
                '                    " where WDate Between '" & FromDate & " 00:00:00' and '" & ToDate & " 23:59:59 ' group by L.Location "
                'End If
               
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
        Public Function GetAllStockBalance(Optional ByVal SortingType As String = "", Optional ByVal BalanceType As String = "") As System.Data.DataTable Implements IDashboardDA.GetAllStockBalance
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If BalanceType = "Stock Balance" Then
                    If SortingType = "Min" Then
                        strCommandText = "select count(forsaleid) as StockBalance,isnull(I.ItemCategory,GC.GemsCategory) as ItemCategory from tbl_forsale H  " & _
                                        " Left Join tbl_ItemCategory I on H.ItemCategoryId=I.ItemCategoryID " & _
                                        " Left Join tbl_Staff S on H.StaffID=S.StaffID " & _
                                        " left Join tbl_GemsCategory GC On H.SDGemsCategoryID=GC.GemsCategoryID  " & _
                                        " Left Join tbl_ItemName N on H.ItemNameID=N.ItemNameID " & _
                                        " Left Join tbl_GoldQuality G on H.GoldQualityID=G.GoldQualityID " & _
                                        " Left Join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where isexit=0 and H.IsClosed=0 and H.isdelete=0  And H.LocationID=" & Global_CurrentLocationID & _
                                        " group by I.ItemCategory,GC.GemsCategory order by StockBalance asc"
                    Else
                        strCommandText = "select count(forsaleid) as StockBalance,isnull(I.ItemCategory,GC.GemsCategory) as ItemCategory from tbl_forsale H  " & _
                                        " Left Join tbl_ItemCategory I on H.ItemCategoryId=I.ItemCategoryID " & _
                                        " Left Join tbl_Staff S on H.StaffID=S.StaffID " & _
                                        " left Join tbl_GemsCategory GC On H.SDGemsCategoryID=GC.GemsCategoryID  " & _
                                        " Left Join tbl_ItemName N on H.ItemNameID=N.ItemNameID " & _
                                        " Left Join tbl_GoldQuality G on H.GoldQualityID=G.GoldQualityID " & _
                                        " Left Join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where isexit=0 and H.IsClosed=0 and H.isdelete=0  And H.LocationID=" & Global_CurrentLocationID & _
                                        " group by I.ItemCategory,GC.GemsCategory order by StockBalance desc"
                    End If
                Else
                    If SortingType = "Min" Then
                        strCommandText = " Select sum(DebtAmount) AS CreditAmount,CustomerId,Customer as CustomerName from ( Select * from " & _
                                         " (select I.CustomerID, C.CustomerName as Customer,(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) " & _
                                         " -(PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount " & _
                                         " +OtherCashAmount)) as DebtAmount  from tbl_SaleInvoiceHeader I   left join tbl_Customer C on I.CustomerID=C.CustomerID  , " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 group by VoucherNo ) as Debt " & _
                                         " where I.SaleInvoiceHeaderID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  (((TotalAmount+AddOrSub) " & _
                                         " -(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+payAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) > 0  " & _
                                        " And I.LocationID =" & Global_CurrentLocationID & _
                                        " union all" & _
                                         " select I.CustomerID, C.CustomerName as Customer, (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100) " & _
                                         " +DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount  from tbl_SaleInvoiceHeader I " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND (((TotalAmount+AddOrSub)" & _
                                         " -(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) <>0 AND IsCancel=0  " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " and SaleInvoiceHeaderID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 ))as E " & _
                                         " UNION ALL  " & _
                                         " Select * from ( " & _
                                         " select  I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount  from tbl_WholeSaleInvoice I " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  ,   (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='WholeSalesInvoice' and IsDelete=0  group by VoucherNo ) as Debt  where I.WholeSaleInvoiceID = Debt.VoucherNo AND I.IsDelete=0  " & _
                                         " And C.IsDelete=0 And   (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) <> 0   And I.LocationID =" & Global_CurrentLocationID & _
                                         " union all select I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) as DebtAmount   from tbl_WholeSaleInvoice I   " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND  (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount))  " & _
                                         " <>0  And I.LocationID =" & Global_CurrentLocationID & " AND PayType='2'  and WholeSaleInvoiceID Not In  (Select VoucherNo from tbl_CashReceipt  " & _
                                         " Where Type='WholeSalesInvoice'  and IsDelete=0 ))as W  UNION ALL " & _
                                         " Select * from ( select I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount   " & _
                                         " from tbl_ConsignmentSale I  left join  tbl_Customer C on I.CustomerID=C.CustomerID  ,   (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='ConsignmentSaleInvoice' and IsDelete=0  group by VoucherNo ) as Debt  where I.ConsignmentSaleID = Debt.VoucherNo  " & _
                                         " AND I.IsDelete=0 And C.IsDelete=0 And   (((NetAmount + AddOrSub) - (Discount+I.RedeemValue+I.MemberDiscountAmt)) - (PaidAmount)) <> 0   And I.LocationID = " & Global_CurrentLocationID & _
                                         " union all select  I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) as DebtAmount   from tbl_ConsignmentSale I   " & _
                                         " left join tbl_Customer C  on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND  (((NetAmount + AddOrSub) - (Discount+I.RedeemValue+I.MemberDiscountAmt)) - (PaidAmount)) <> 0   " & _
                                         " And I.LocationID =" & Global_CurrentLocationID & " and ConsignmentSaleID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='ConsignmentSaleInvoice'  " & _
                                         " and IsDelete=0 ))as S   UNION ALL  Select * from (  select I.CustomerID, C.CustomerName as Customer,((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) " & _
                                         " -(H.PaidAmount+PayAmount+H.FromGoldAmount)) AS DebtAmount  from tbl_OrderReturnHeader H  LEFT JOIN tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID LEFT JOIN tbl_Location ON tbl_Location.LocationID=H.LocationID , (select VoucherNo,sum(PayAmount) as PayAmount  " & _
                                         " from tbl_CashReceipt Where Type='OrderInvoice'  and IsDelete=0 group by VoucherNo ) as Debt 	    where H.OrderInvoiceID = Debt.VoucherNo  " & _
                                         " And ((((H.AllTotalAmount + H.AllAddOrSub) - H.DiscountAmount)-H.AdvanceAmount) - (H.PaidAmount + H.FromGoldAmount))<>0 and IsRetrieved = 1   " & _
                                         " And I.LocationID =" & Global_CurrentLocationID & " AND H.IsDelete=0 And I.IsDelete=0 And C.IsDelete=0 AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID)  " & _
                                         " FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID AND R.IsDelete=0)  " & _
                                         " UNION ALL  select I.CustomerID,  C.CustomerName as Customer,((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) " & _
                                         " -(H.PaidAmount+H.FromGoldAmount)) AS DebtAmount  from tbl_OrderReturnHeader H  LEFT JOIN  tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  LEFT JOIN tbl_Location  ON tbl_Location.LocationID=H.LocationID  where ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) " & _
                                         " -(H.PaidAmount+H.FromGoldAmount))<>0   And I.LocationID =" & Global_CurrentLocationID & " and IsRetrieved = 1 and H.OrderInvoiceID  " & _
                                         " Not In (Select VoucherNo from tbl_CashReceipt Where Type='OrderInvoice' and IsDelete=0  ) AND H.IsDelete=0 And I.IsDelete=0 And C.IsDelete=0  " & _
                                         " And H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID  " & _
                                         " AND R.IsDelete=0)) AS A  UNION ALL  Select * from ( select I.CustomerID, C.CustomerName as Customer,((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount)) " & _
                                         " -(PaidAmount+PayAmount+isnull(I.PurchaseAmount,0)) as DebtAmount  from tbl_SaleGems 	I  LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID   " & _
                                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID , (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='SalesGems' and IsDelete=0  group by VoucherNo ) as Debt  where I.IsDelete=0 And C.IsDelete=0 And I.SaleGemsID=Debt.VoucherNo   and ((TotalAmount+AddOrSub) " & _
                                         " -((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+isnull(I.PurchaseAmount,0))<>0  And I.LocationID = " & Global_CurrentLocationID & "  " & _
                                         " union all  select I.CustomerID,  C.CustomerName as Customer, ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount " & _
                                         " -isnull(I.PurchaseAmount,0) as DebtAmount from tbl_SaleGems I LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID   " & _
                                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID  where I.IsDelete=0 and C.IsDelete=0 And tbl_Location.IsDelete=0  " & _
                                         " and ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0)<>0   " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " and SaleGemsID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesGems' and IsDelete=0 )) AS B   " & _
                                         " UNION ALL  Select * from ( select I.CustomerID, C.CustomerName as Customer,(((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) " & _
                                         " -(H.ReturnPaidAmount+H.AdvanceAmount+PayAmount)) as DebtAmount   from tbl_ReturnRepairHeader H  LEFT JOIN tbl_RepairHeader I ON I.RepairID=H.RepairID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID , (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='RepairReturn' and IsDelete=0  group by VoucherNo ) as Debt 	    where H.RepairID = Debt.VoucherNo  " & _
                                         " And (((H.AllReturnTotalAmount + H.AllReturnAddOrSub) - H.ReturnDiscountAmount) - (H.ReturnPaidAmount + H.AdvanceAmount)) <>0  " & _
                                         " and IsAllReturn = 1  And I.LocationID = " & Global_CurrentLocationID & " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID)  " & _
                                         " FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID AND IsDelete=0 )   " & _
                                         " UNION ALL select I.CustomerID,  C.CustomerName as Customer,(((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) " & _
                                         " -(H.ReturnPaidAmount+H.AdvanceAmount)) as DebtAmount  from tbl_ReturnRepairHeader H  LEFT JOIN  tbl_RepairHeader I ON I.RepairID=H.RepairID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID   where H.IsDelete=0 AND I.IsDelete=0 And C.IsDelete=0  " & _
                                         " And (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) <>0    " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " and IsAllReturn = 1 and H.RepairID Not In (Select VoucherNo from tbl_CashReceipt Where Type='RepairReturn'  " & _
                                         " and IsDelete=0  ) AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID  " & _
                                         " and IsDelete=0))as C UNION ALL  Select * from ( select  I.CustomerID, C.CustomerName as Customer,(((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) " & _
                                         " -(PaidAmount+PayAmount))  as DebtAmount  from tbl_SalesVolume I  left join tbl_Customer C on I.CustomerID=C.CustomerID  , (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='SalesInvoiceVolume' and IsDelete=0  group by VoucherNo ) as Debt where I.SalesVolumeID=Debt.VoucherNo and I.IsDelete=0  " & _
                                         " and C.IsDelete=0  and (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) <>0    " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " union all select I.CustomerID, C.CustomerName as Customer,(((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-PaidAmount) as DebtAmount  " & _
                                         " from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 and C.IsDelete=0  " & _
                                         " And (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-PaidAmount)<>0  And I.LocationID = " & Global_CurrentLocationID & " " & _
                                         " and SalesVolumeID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoiceVolume' And IsDelete=0 ))as D)as M  WHERE 1=1 and DebtAmount>0 group by CustomerId,Customer " & _
                                        "  order by CreditAmount ASC"
                    Else
                        strCommandText = " Select sum(DebtAmount) AS CreditAmount,CustomerId,Customer as CustomerName from ( Select * from " & _
                                         " (select I.CustomerID, C.CustomerName as Customer,(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) " & _
                                         " -(PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount " & _
                                         " +OtherCashAmount)) as DebtAmount  from tbl_SaleInvoiceHeader I   left join tbl_Customer C on I.CustomerID=C.CustomerID  , " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 group by VoucherNo ) as Debt " & _
                                         " where I.SaleInvoiceHeaderID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  (((TotalAmount+AddOrSub) " & _
                                         " -(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+payAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) > 0  " & _
                                        " And I.LocationID =" & Global_CurrentLocationID & _
                                        " union all" & _
                                         " select I.CustomerID, C.CustomerName as Customer, (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100) " & _
                                         " +DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount  from tbl_SaleInvoiceHeader I " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND (((TotalAmount+AddOrSub)" & _
                                         " -(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) <>0 AND IsCancel=0  " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " and SaleInvoiceHeaderID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 ))as E " & _
                                         " UNION ALL  " & _
                                         " Select * from ( " & _
                                         " select  I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount  from tbl_WholeSaleInvoice I " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  ,   (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='WholeSalesInvoice' and IsDelete=0  group by VoucherNo ) as Debt  where I.WholeSaleInvoiceID = Debt.VoucherNo AND I.IsDelete=0  " & _
                                         " And C.IsDelete=0 And   (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) <> 0   And I.LocationID =" & Global_CurrentLocationID & _
                                         " union all select I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) as DebtAmount   from tbl_WholeSaleInvoice I   " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND  (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount))  " & _
                                         " <>0  And I.LocationID =" & Global_CurrentLocationID & " AND PayType='2'  and WholeSaleInvoiceID Not In  (Select VoucherNo from tbl_CashReceipt  " & _
                                         " Where Type='WholeSalesInvoice'  and IsDelete=0 ))as W  UNION ALL " & _
                                         " Select * from ( select I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount   " & _
                                         " from tbl_ConsignmentSale I  left join  tbl_Customer C on I.CustomerID=C.CustomerID  ,   (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='ConsignmentSaleInvoice' and IsDelete=0  group by VoucherNo ) as Debt  where I.ConsignmentSaleID = Debt.VoucherNo  " & _
                                         " AND I.IsDelete=0 And C.IsDelete=0 And   (((NetAmount + AddOrSub) - (Discount+I.RedeemValue+I.MemberDiscountAmt)) - (PaidAmount)) <> 0   And I.LocationID = " & Global_CurrentLocationID & _
                                         " union all select  I.CustomerID, C.CustomerName as Customer, (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) as DebtAmount   from tbl_ConsignmentSale I   " & _
                                         " left join tbl_Customer C  on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND  (((NetAmount + AddOrSub) - (Discount+I.RedeemValue+I.MemberDiscountAmt)) - (PaidAmount)) <> 0   " & _
                                         " And I.LocationID =" & Global_CurrentLocationID & " and ConsignmentSaleID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='ConsignmentSaleInvoice'  " & _
                                         " and IsDelete=0 ))as S   UNION ALL  Select * from (  select I.CustomerID, C.CustomerName as Customer,((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) " & _
                                         " -(H.PaidAmount+PayAmount+H.FromGoldAmount)) AS DebtAmount  from tbl_OrderReturnHeader H  LEFT JOIN tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID LEFT JOIN tbl_Location ON tbl_Location.LocationID=H.LocationID , (select VoucherNo,sum(PayAmount) as PayAmount  " & _
                                         " from tbl_CashReceipt Where Type='OrderInvoice'  and IsDelete=0 group by VoucherNo ) as Debt 	    where H.OrderInvoiceID = Debt.VoucherNo  " & _
                                         " And ((((H.AllTotalAmount + H.AllAddOrSub) - H.DiscountAmount)-H.AdvanceAmount) - (H.PaidAmount + H.FromGoldAmount))<>0 and IsRetrieved = 1   " & _
                                         " And I.LocationID =" & Global_CurrentLocationID & " AND H.IsDelete=0 And I.IsDelete=0 And C.IsDelete=0 AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID)  " & _
                                         " FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID AND R.IsDelete=0)  " & _
                                         " UNION ALL  select I.CustomerID,  C.CustomerName as Customer,((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) " & _
                                         " -(H.PaidAmount+H.FromGoldAmount)) AS DebtAmount  from tbl_OrderReturnHeader H  LEFT JOIN  tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  LEFT JOIN tbl_Location  ON tbl_Location.LocationID=H.LocationID  where ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) " & _
                                         " -(H.PaidAmount+H.FromGoldAmount))<>0   And I.LocationID =" & Global_CurrentLocationID & " and IsRetrieved = 1 and H.OrderInvoiceID  " & _
                                         " Not In (Select VoucherNo from tbl_CashReceipt Where Type='OrderInvoice' and IsDelete=0  ) AND H.IsDelete=0 And I.IsDelete=0 And C.IsDelete=0  " & _
                                         " And H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID  " & _
                                         " AND R.IsDelete=0)) AS A  UNION ALL  Select * from ( select I.CustomerID, C.CustomerName as Customer,((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount)) " & _
                                         " -(PaidAmount+PayAmount+isnull(I.PurchaseAmount,0)) as DebtAmount  from tbl_SaleGems 	I  LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID   " & _
                                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID , (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='SalesGems' and IsDelete=0  group by VoucherNo ) as Debt  where I.IsDelete=0 And C.IsDelete=0 And I.SaleGemsID=Debt.VoucherNo   and ((TotalAmount+AddOrSub) " & _
                                         " -((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+isnull(I.PurchaseAmount,0))<>0  And I.LocationID = " & Global_CurrentLocationID & "  " & _
                                         " union all  select I.CustomerID,  C.CustomerName as Customer, ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount " & _
                                         " -isnull(I.PurchaseAmount,0) as DebtAmount from tbl_SaleGems I LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID   " & _
                                         " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID  where I.IsDelete=0 and C.IsDelete=0 And tbl_Location.IsDelete=0  " & _
                                         " and ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0)<>0   " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " and SaleGemsID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesGems' and IsDelete=0 )) AS B   " & _
                                         " UNION ALL  Select * from ( select I.CustomerID, C.CustomerName as Customer,(((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) " & _
                                         " -(H.ReturnPaidAmount+H.AdvanceAmount+PayAmount)) as DebtAmount   from tbl_ReturnRepairHeader H  LEFT JOIN tbl_RepairHeader I ON I.RepairID=H.RepairID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID , (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='RepairReturn' and IsDelete=0  group by VoucherNo ) as Debt 	    where H.RepairID = Debt.VoucherNo  " & _
                                         " And (((H.AllReturnTotalAmount + H.AllReturnAddOrSub) - H.ReturnDiscountAmount) - (H.ReturnPaidAmount + H.AdvanceAmount)) <>0  " & _
                                         " and IsAllReturn = 1  And I.LocationID = " & Global_CurrentLocationID & " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID)  " & _
                                         " FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID AND IsDelete=0 )   " & _
                                         " UNION ALL select I.CustomerID,  C.CustomerName as Customer,(((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) " & _
                                         " -(H.ReturnPaidAmount+H.AdvanceAmount)) as DebtAmount  from tbl_ReturnRepairHeader H  LEFT JOIN  tbl_RepairHeader I ON I.RepairID=H.RepairID  " & _
                                         " Left Join tbl_Customer C on C.CustomerID=I.CustomerID   where H.IsDelete=0 AND I.IsDelete=0 And C.IsDelete=0  " & _
                                         " And (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) <>0    " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " and IsAllReturn = 1 and H.RepairID Not In (Select VoucherNo from tbl_CashReceipt Where Type='RepairReturn'  " & _
                                         " and IsDelete=0  ) AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID  " & _
                                         " and IsDelete=0))as C UNION ALL  Select * from ( select  I.CustomerID, C.CustomerName as Customer,(((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) " & _
                                         " -(PaidAmount+PayAmount))  as DebtAmount  from tbl_SalesVolume I  left join tbl_Customer C on I.CustomerID=C.CustomerID  , (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt  " & _
                                         " Where Type='SalesInvoiceVolume' and IsDelete=0  group by VoucherNo ) as Debt where I.SalesVolumeID=Debt.VoucherNo and I.IsDelete=0  " & _
                                         " and C.IsDelete=0  and (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) <>0    " & _
                                         " And I.LocationID = " & Global_CurrentLocationID & " union all select I.CustomerID, C.CustomerName as Customer,(((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-PaidAmount) as DebtAmount  " & _
                                         " from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 and C.IsDelete=0  " & _
                                         " And (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-PaidAmount)<>0  And I.LocationID = " & Global_CurrentLocationID & " " & _
                                         " and SalesVolumeID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoiceVolume' And IsDelete=0 ))as D)as M  WHERE 1=1 and DebtAmount>0 group by CustomerId,Customer " & _
                                        "  order by CreditAmount Desc"
                    End If
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllSaleByCategory(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Type As String, Optional ByVal ItemType As String = "") As System.Data.DataTable Implements IDashboardDA.GetAllSaleByCategory
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If ItemType = "Item Category" Then
                    If Type = "Sale" Then
                        strCommandText = "select top 10 sum(H.TotalAmount) as SaleAmount,I.ItemCategory From tbl_SaleInvoiceHeader H " & _
                                        " join tbl_SaleInvoiceDetail D on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                        " Join tbl_Forsale F on D.Forsaleid=F.ForSaleID " & _
                                        " join tbl_ItemCategory I on F.ItemCategoryID=I.ItemCategoryID  Where H.isdelete=0  " & _
                                        " And saleDate Between @FromDate And @ToDate and H.isDelete=0 and H.LocationId=" & Global_CurrentLocationID & " group by I.ItemCategory order by SaleAmount desc "
                    Else
                        strCommandText = "select top 10 sum(H.NetAmount) as SaleAmount,I.ItemCategory From tbl_WholeSaleInvoice H " & _
                                        " join tbl_WholesaleInvoiceItem D on H.WholesaleinvoiceID=D.WholesaleinvoiceID " & _
                                        " Join tbl_Forsale F on D.Forsaleid=F.ForSaleID " & _
                                        " join tbl_ItemCategory I on F.ItemCategoryID=I.ItemCategoryID  Where H.isdelete=0  " & _
                                        " And WDate Between @FromDate And @ToDate and H.isDelete=0 and H.LocationId=" & Global_CurrentLocationID & " group by I.ItemCategory order by SaleAmount desc "
                    End If
                Else
                    If Type = "Sale" Then
                        strCommandText = "select top 10 sum(H.TotalAmount) as SaleAmount,I.ItemName From tbl_SaleInvoiceHeader H " & _
                                        " join tbl_SaleInvoiceDetail D on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                        " Join tbl_Forsale F on D.Forsaleid=F.ForSaleID " & _
                                        " join tbl_ItemName I on F.ItemNameID=I.ItemNameID  Where H.isdelete=0  " & _
                                        " And saleDate Between @FromDate And @ToDate and H.isDelete=0 and H.LocationId=" & Global_CurrentLocationID & " group by I.ItemName order by SaleAmount desc "
                    Else
                        strCommandText = "select top 10 sum(H.NetAmount) as SaleAmount,I.ItemName From tbl_WholeSaleInvoice H " & _
                                        " join tbl_WholesaleInvoiceItem D on H.WholesaleinvoiceID=D.WholesaleinvoiceID " & _
                                        " Join tbl_Forsale F on D.Forsaleid=F.ForSaleID " & _
                                        " join tbl_ItemName I on F.ItemNameID=I.ItemNameID  Where H.isdelete=0  " & _
                                        " And WDate Between @FromDate And @ToDate and H.isDelete=0 and H.LocationId=" & Global_CurrentLocationID & " group by I.ItemName order by SaleAmount desc "
                    End If
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
        Public Function GetAllSaleByLocation(Optional ByVal SaleType As String = "", Optional ByVal Cristr As String = "", Optional ByVal DateType As String = "") As System.Data.DataTable Implements IDashboardDA.GetAllSaleByLocation
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If DateType = "Day " Then
                    If SaleType = "Sale" Then
                        strCommandText = "select sum(totalamount) as SaleAmount,CONCAT(Day(SaleDate), '/',Convert(char(3),SaleDate,0)) As SaleDate,Month(SaleDate) as MSaleDate,Year(SaleDate),L.Location as Location From tbl_SaleInvoiceHeader H " & _
                                        " join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where saleDate Between " & Cristr & " group by L.Location,Day(SaleDate),Month(SaleDate),Year(SaleDate),SaleDate order by Month(SaleDate) asc "
                    Else
                        strCommandText = "select sum(NetAmount) as SaleAmount,CONCAT(Day(WDate), '/',Convert(char(3),WDate,0)) As SaleDate,Month(WDate) as MSaleDate,Year(WDate),L.Location as Location From tbl_WholesaleInvoice H " & _
                                        " join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where WDate Between " & Cristr & " group by L.Location,Day(WDate),Month(WDate),Year(WDate),WDate order by Month(WDate) asc "
                    End If
                ElseIf DateType = "Month" Then
                    If SaleType = "Sale" Then
                        strCommandText = "select sum(totalamount) as SaleAmount,CONCAT(Month(SaleDate), '/',Year(SaleDate)) As SaleDate,Month(SaleDate) as MSaleDate,Year(SaleDate),L.Location as Location From tbl_SaleInvoiceHeader H " & _
                                        " join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where saleDate Between " & Cristr & " group by L.Location,Month(SaleDate ),Year(SaleDate) order by Year(SaleDate),Month(SaleDate) asc "
                    Else
                        strCommandText = "select sum(NetAmount) as SaleAmount,CONCAT(Month(WDate), '/',Year(WDate)) As SaleDate,Month(WDate) as MSaleDate,Year(WDate),L.Location as Location From tbl_WholesaleInvoice H " & _
                                        " join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where WDate Between " & Cristr & " group by L.Location,Month(WDate),Year(WDate)  order by Year(WDate),Month(WDate) asc "
                    End If
                ElseIf DateType = "Year" Then
                    If SaleType = "Sale" Then
                        strCommandText = "select sum(totalamount) as SaleAmount,Year(SaleDate) as SaleDate,L.Location as Location From tbl_SaleInvoiceHeader H " & _
                                        " join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where saleDate Between " & Cristr & " group by L.Location,Year(SaleDate ) order by Year(SaleDate) asc "
                    Else
                        strCommandText = "select sum(NetAmount) as SaleAmount,Year(WDate) as SaleDate,L.Location as Location From tbl_WholesaleInvoice H " & _
                                        " join tbl_Location L on H.LocationID=L.LocationID " & _
                                        " where WDate Between " & Cristr & " group by L.Location,Year(WDate) order by Year(WDate) asc "
                    End If
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllCredit() As System.Data.DataTable Implements IDashboardDA.GetAllCredit
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select * from (Select * from (select VoucherNo,  SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address, " & _
                                         " ((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount, (PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as PaidAmount,PayAmount,   " & _
                                         " (PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as TotalPaidAmount,(((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleInvoiceHeader I   left join tbl_Customer C on I.CustomerID=C.CustomerID  , " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 group by VoucherNo ) as Debt " & _
                                         " where I.SaleInvoiceHeaderID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) <> 0  And I.LocationID=" & Global_CurrentLocationID & " union all " & _
                                         " select SaleInvoiceHeaderID as VoucherNo, SaleDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                         " ((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount, (PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as PaidAmount, 0 as PayAmount, " & _
                                         " (PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount) as TotalPaidAmount,   (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) as DebtAmount " & _
                                         " from tbl_SaleInvoiceHeader I  left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                                         " where I.IsDelete=0 AND C.IsDelete=0 AND (((TotalAmount+AddOrSub)-(((TotalAmount*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PurchaseAmount+AllAdvanceAmount+OtherCashAmount)) <>0 AND IsCancel=0 And I.LocationID=" & Global_CurrentLocationID & " and SaleInvoiceHeaderID Not In " & _
                                         " (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoice' and IsDelete=0 ))as E " & _
                                         " UNION ALL" & _
                                         "  Select * from (select VoucherNo,  WDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,  ((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount, (PaidAmount) as PaidAmount,PayAmount, " & _
                                         " (PaidAmount+PayAmount) as TotalPaidAmount, " & _
                                         " (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount  from tbl_WholeSaleInvoice I   " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  ,  " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='WholeSalesInvoice' and IsDelete=0 " & _
                                         " group by VoucherNo ) as Debt  where I.WholeSaleInvoiceID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  " & _
                                         " (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) <> 0 And I.LocationID=" & Global_CurrentLocationID & " union all " & _
                                         " select WholeSaleInvoiceID as VoucherNo, WDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,   ((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount, " & _
                                         " (PaidAmount) as PaidAmount, 0 as PayAmount,  " & _
                                         " (PaidAmount) as TotalPaidAmount,   " & _
                                         " (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) as DebtAmount  " & _
                                         " from tbl_WholeSaleInvoice I  left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND " & _
                                         " (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) <>0 And I.LocationID=" & Global_CurrentLocationID & " AND PayType='2' " & _
                                         " and WholeSaleInvoiceID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='WholeSalesInvoice'  and IsDelete=0 ))as W " & _
                                         " UNION ALL " & _
                                         " Select * from (select VoucherNo,  ConsignDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,  ((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount, (PaidAmount) as PaidAmount,PayAmount,    " & _
                                         " (PaidAmount+PayAmount) as TotalPaidAmount, " & _
                                         " (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount)) as DebtAmount  from tbl_ConsignmentSale I   " & _
                                         " left join tbl_Customer C on I.CustomerID=C.CustomerID  ,  " & _
                                         " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='ConsignmentSaleInvoice' and IsDelete=0 " & _
                                         " group by VoucherNo ) as Debt  where I.ConsignmentSaleID = Debt.VoucherNo AND I.IsDelete=0 And C.IsDelete=0 And  " & _
                                         " (((NetAmount + AddOrSub) - (Discount+I.RedeemValue+I.MemberDiscountAmt)) - (PaidAmount)) <> 0  And I.LocationID=" & Global_CurrentLocationID & " union all " & _
                                         " select ConsignmentSaleID as VoucherNo, ConsignDate as PDate, I.CustomerID, C.CustomerName as Customer, " & _
                                         " C.CustomerAddress as Address,   ((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount, " & _
                                         " (PaidAmount) as PaidAmount, 0 as PayAmount,  " & _
                                         " (PaidAmount) as TotalPaidAmount,   " & _
                                         " (((NetAmount+AddOrSub)-(Discount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) as DebtAmount  " & _
                                         " from tbl_ConsignmentSale I  left join tbl_Customer C on I.CustomerID=C.CustomerID  where I.IsDelete=0 AND C.IsDelete=0 AND " & _
                                         " (((NetAmount + AddOrSub) - (Discount+I.RedeemValue+I.MemberDiscountAmt)) - (PaidAmount)) <> 0 And I.LocationID=" & Global_CurrentLocationID & " and ConsignmentSaleID Not In  (Select VoucherNo from tbl_CashReceipt Where Type='ConsignmentSaleInvoice' and IsDelete=0 ))as S  " & _
                                        " UNION ALL " & _
                                        " Select * from (select H.OrderInvoiceID AS VoucherNo, H.ReturnDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " (((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) as NetAmount, H.PaidAmount+H.FromGoldAmount AS PaidAmount, PayAmount," & _
                                        " H.PaidAmount+PayAmount+H.FromGoldAmount AS TotalPaidAmount, ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+PayAmount+H.FromGoldAmount)) AS DebtAmount " & _
                                        " from tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  LEFT JOIN tbl_Location ON tbl_Location.LocationID=H.LocationID ," & _
                                        " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='OrderInvoice'  and IsDelete=0 group by VoucherNo ) as Debt 	   " & _
                                        " where H.OrderInvoiceID = Debt.VoucherNo And ((((H.AllTotalAmount + H.AllAddOrSub) - H.DiscountAmount)-H.AdvanceAmount) - (H.PaidAmount + H.FromGoldAmount))<>0 and IsRetrieved = 1 And I.LocationID=" & Global_CurrentLocationID & _
                                        " AND H.IsDelete=0 And I.IsDelete=0 And C.IsDelete=0 AND H.OrderReturnHeaderID=(SELECT MAX(OrderReturnHeaderID) FROM tbl_OrderReturnHeader R WHERE R.OrderInvoiceID=H.OrderInvoiceID AND R.IsDelete=0) " & _
                                        " UNION ALL " & _
                                        " select H.OrderInvoiceID as VoucherNo, H.ReturnDate as PDate, I.CustomerID,  C.CustomerName as Customer," & _
                                        " C.CustomerAddress as Address, (((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount) as NetAmount, (H.PaidAmount+H.FromGoldAmount) as PaidAmount, " & _
                                        " 0 as PayAmount, (H.PaidAmount+H.FromGoldAmount) as TotalPaidAmount,   " & _
                                        " ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+H.FromGoldAmount)) AS DebtAmount " & _
                                        " from tbl_OrderReturnHeader H LEFT JOIN  tbl_OrderInvoice I ON I.OrderInvoiceID=H.OrderInvoiceID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  LEFT JOIN tbl_Location " & _
                                        " ON tbl_Location.LocationID=H.LocationID  where ((((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount)-H.AdvanceAmount)-(H.PaidAmount+H.FromGoldAmount))<>0 And I.LocationID=" & Global_CurrentLocationID & _
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
                                        " and ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-(PaidAmount+isnull(I.PurchaseAmount,0))<>0 And I.LocationID=" & Global_CurrentLocationID & _
                                        " union all " & _
                                        " select SaleGemsID as VoucherNo, I.SDate AS PDate, I.CustomerID, " & _
                                        " C.CustomerName as Customer,C.CustomerAddress as Address,  " & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount)) as NetAmount,PaidAmount,0 as PayAmount, PaidAmount as TotalPaidAmount,  " & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0) as DebtAmount from tbl_SaleGems I " & _
                                        " LEFT JOIN tbl_Customer C ON C.CustomerID=I.CustomerID " & _
                                        " LEFT JOIN tbl_Location ON tbl_Location.LocationID=I.LocationID " & _
                                        " where I.IsDelete=0 and C.IsDelete=0 And tbl_Location.IsDelete=0 and ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount))-PaidAmount-isnull(I.PurchaseAmount,0)<>0 And I.LocationID=" & Global_CurrentLocationID & "and SaleGemsID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesGems' and IsDelete=0 )) AS B " & _
                                        " UNION ALL " & _
                                        " Select * from (select H.RepairID AS VoucherNo, H.ReturnDate as PDate, I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-H.AdvanceAmount) as NetAmount, H.ReturnPaidAmount AS PaidAmount, PayAmount," & _
                                        " (H.ReturnPaidAmount + PayAmount) as TotalPaidAmount, " & _
                                        " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount+PayAmount)) as DebtAmount  " & _
                                        " from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader I ON I.RepairID=H.RepairID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID ," & _
                                        " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='RepairReturn' and IsDelete=0  group by VoucherNo ) as Debt 	   " & _
                                        " where H.RepairID = Debt.VoucherNo And (((H.AllReturnTotalAmount + H.AllReturnAddOrSub) - H.ReturnDiscountAmount) - (H.ReturnPaidAmount + H.AdvanceAmount)) <>0 and IsAllReturn = 1 And I.LocationID=" & Global_CurrentLocationID & _
                                        " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID AND IsDelete=0 ) " & _
                                        " UNION ALL " & _
                                        " select H.RepairID as VoucherNo, H.ReturnDate as PDate, I.CustomerID,  C.CustomerName as Customer," & _
                                        " C.CustomerAddress as Address, (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-H.AdvanceAmount) as NetAmount,  H.ReturnPaidAmount AS PaidAmount, " & _
                                        " 0 as PayAmount, H.ReturnPaidAmount AS TotalPaidAmount,   " & _
                                        " (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) as DebtAmount " & _
                                        " from tbl_ReturnRepairHeader H LEFT JOIN  tbl_RepairHeader I ON I.RepairID=H.RepairID" & _
                                        " Left Join tbl_Customer C on C.CustomerID=I.CustomerID  " & _
                                        " where H.IsDelete=0 AND I.IsDelete=0 And C.IsDelete=0 And (((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount)-(H.ReturnPaidAmount+H.AdvanceAmount)) <>0 And I.LocationID=" & Global_CurrentLocationID & _
                                        " and IsAllReturn = 1 and H.RepairID Not In (Select VoucherNo from tbl_CashReceipt Where Type='RepairReturn' and IsDelete=0  )" & _
                                        " AND H.ReturnRepairID=(SELECT MAX(ReturnRepairID) FROM tbl_ReturnRepairHeader R WHERE R.RepairID=H.RepairID and IsDelete=0))as C  " & _
                                        " UNION ALL " & _
                                        " Select * from (select VoucherNo, SaleDate as PDate , I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount,PaidAmount,PayAmount, " & _
                                        " PaidAmount+PayAmount as TotalPaidAmount,(((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount+PayAmount))  as DebtAmount " & _
                                        " from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID  ," & _
                                        " (select VoucherNo,sum(PayAmount) as PayAmount from tbl_CashReceipt Where Type='SalesInvoiceVolume' and IsDelete=0  group by VoucherNo ) as Debt where I.SalesVolumeID=Debt.VoucherNo and I.IsDelete=0 and C.IsDelete=0 " & _
                                        " and (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-(PaidAmount)) <>0 And I.LocationID=" & Global_CurrentLocationID & " union all " & _
                                        " select SalesVolumeID as VoucherNo, SaleDate as PDate , I.CustomerID, C.CustomerName as Customer,C.CustomerAddress as Address," & _
                                        " ((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt)) as NetAmount,PaidAmount,0 as PayAmount,PaidAmount as TotalPaidAmount, " & _
                                        " (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-PaidAmount) as DebtAmount from tbl_SalesVolume I left join tbl_Customer C on I.CustomerID=C.CustomerID " & _
                                        " where I.IsDelete=0 and C.IsDelete=0 And (((TotalAmount+AddOrSub)-((((TotalAmount+AddOrSub)*PromotionDiscount)/100)+DiscountAmount+I.RedeemValue+I.MemberDiscountAmt))-PaidAmount)<>0 And I.LocationID=" & Global_CurrentLocationID & " and SalesVolumeID Not In (Select VoucherNo from tbl_CashReceipt Where Type='SalesInvoiceVolume' And IsDelete=0 ))as D)as M  WHERE 1=1 order by VoucherNo"
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
