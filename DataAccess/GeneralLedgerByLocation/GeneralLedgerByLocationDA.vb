Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace GeneralLedgerByLocation
    Public Class GeneralLedgerByLocationDA
        Implements IGeneralLedgerByLocationDA

#Region "Private GeneralLedgerByLocation"
        Private DB As Database
        Private Shared ReadOnly _instance As IGeneralLedgerByLocationDA = New GeneralLedgerByLocationDA
#End Region
#Region "Constructors"
        Private Sub New()
            DB = DatabaseFactory.CreateDatabase
        End Sub
#End Region
#Region "Public Properties"
        Public Shared ReadOnly Property Instance() As IGeneralLedgerByLocationDA
            Get
                Return _instance
            End Get
        End Property
#End Region

        Public Function GetGeneralLedgerByLocationByDateFromTransaction(ByVal cristr As String, ByVal ForDate As Date, ByVal ToDate As Date) As System.Data.DataTable Implements IGeneralLedgerByLocationDA.GetGeneralLedgerByLocationByDateFromTransaction
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try


                strCommandText = "SELECT 'Opening-Cash' AS Title,'' as MyanTitle, 0 AS DebitAmount, 0 AS CreditAmount, 'In' AS Type UNION ALL" & _
                   " SELECT 'Sale-Invoice' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader where IsDelete=0 AND SaleDate = @ForDate AND Substring(SaleInvoiceHeaderID,1,2)=@LocationID UNION ALL" & _
                   " SELECT 'Sale-Volume' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SalesVolume where IsDelete=0 AND SaleDate = @ForDate AND LocationID=@LocationID UNION ALL" & _
                   " SELECT 'Order-Advance' AS Title,'' as MyanTitle,ISNULL(SUM(AdvanceAmount+SecondAdvanceAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type FROM tbl_OrderInvoice WHERE IsDelete=0 AND OrderDate=@ForDate AND LocationID=@LocationID UNION ALL" & _
                   " SELECT 'Order-Paid' AS Title,'' as MyanTitle, ISNULL(SUM(AllTotalAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type FROM tbl_OrderReturnHeader WHERE IsDelete=0 AND IsRetrieved=1 AND OrderRetrieveDate=@ForDate AND LocationID=@LocationID UNION ALL" & _
                   " SELECT 'Daily-Income' AS Title, '' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome WHERE IsDelete=0 AND IncomeDate=@ForDate AND LocationID=@LocationID UNION ALL" & _
                   " SELECT 'Purchase-Row-Material' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(AllTotalAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader WHERE IsDelete=0 AND PurchaseDate between @FromDate And @ToDate" & " AND Substring(PurchaseHeaderID,1,2)=@LocationID UNION ALL" & _
                   " SELECT 'Daily-Expense' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(TotalAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_DailyExpense WHERE IsDelete=0 AND ExpenseDate=@ForDate AND LocationID=@LocationID  UNION ALL" & _
                   " SELECT 'Saving-Cash' AS Title,'' as MyanTitle, 0 AS DebitAmount, 0 AS CreditAmount, 'Out' AS Type"



                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, cristr)
                DB.AddInParameter(DBComm, "@ForDate", DbType.Date, ForDate)
                'DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function DeleteGeneralLedgerForLocationByDate(ByVal LocationID As String, ByVal GLDate As Date) As Boolean Implements IGeneralLedgerByLocationDA.DeleteGeneralLedgerForLocationByDate
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_GeneralLedgerByLocation WHERE  LocationID=@LocationID AND Convert(Date,GLDate)=@GLDate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                DB.AddInParameter(DBComm, "@GLDate", DbType.Date, GLDate)
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

        Public Function InsertGeneralLedgerForLocationByDate(ByVal LocationID As String, ByVal GLDate As DateTime, ByVal StaffID As String, ByVal Title As String, ByVal DebitAmount As Long, ByVal CreditAmount As Long, ByVal Type As String, ByVal MyanTitle As String, ByVal LastModifiedDate As Date, ByVal GLByLocationID As String) As Boolean Implements IGeneralLedgerByLocationDA.InsertGeneralLedgerForLocationByDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_GeneralLedgerByLocation (LocationID, GLDate, Title, DebitAmount, CreditAmount, Type,MyanTitle,LastModifiedDate,GLByLocationID)"
                strCommandText += " Values (@LocationID, @GLDate,  @Title, @DebitAmount, @CreditAmount, @Type,@MyanTitle,@LastModifiedDate,@GLByLocationID)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                DB.AddInParameter(DBComm, "@GLDate", DbType.Date, GLDate)
                ' DB.AddInParameter(DBComm, "@StaffID", DbType.String, StaffID)
                DB.AddInParameter(DBComm, "@Title", DbType.String, Title)
                DB.AddInParameter(DBComm, "@DebitAmount", DbType.Int64, DebitAmount)
                DB.AddInParameter(DBComm, "@CreditAmount", DbType.Int64, CreditAmount)
                DB.AddInParameter(DBComm, "@Type", DbType.String, Type)
                DB.AddInParameter(DBComm, "@MyanTitle", DbType.String, MyanTitle)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, LastModifiedDate)
                DB.AddInParameter(DBComm, "@GLByLocationID", DbType.String, GLByLocationID)

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

        Public Function GetGeneralLedgerByLocationByDate(ByVal LocationID As String, ByVal ForDate As Date) As System.Data.DataTable Implements IGeneralLedgerByLocationDA.GetGeneralLedgerByLocationByDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = "SELECT H.LocationID, GLDate, Title, DebitAmount, CreditAmount, Type, Location,MyanTitle  FROM tbl_GeneralLedgerByLocation H INNER JOIN tbl_Location L ON H.LocationID=L.LocationID WHERE H.LocationID=@LocationID AND GLDate=@ForDate"
                strCommandText = " SELECT N'မတည်ငွေ' AS Title,'' as MyanTitle, DebitAmount, 0 AS CreditAmount, 'In' AS Type from tbl_GeneralLedgerbyLocation" & _
                                 " where GLDate = @ForDate and Title ='မတည်ငွေ' UNION ALL" & _
                                 " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader where IsDelete=0 AND SaleDate = @ForDate UNION ALL" & _
                                 " SELECT N'ပစ္စည်းရောင်းခြင်း(Volume)မှ ရငွေ'  AS Title,'' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SalesVolume where IsDelete=0 AND SaleDate = @ForDate  UNION ALL" & _
                                 " SELECT  N'အပ်ထည်လက်ခံစရံငွေ'AS Title, '' as MyanTitle,ISNULL(SUM(AdvanceAmount+SecondAdvanceAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type FROM tbl_OrderInvoice  WHERE IsDelete=0 AND OrderDate=@ForDate UNION ALL " & _
                                 " SELECT N'အပ်ထည်ရွေးငွေ' AS Title,'' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type FROM tbl_OrderReturnHeader WHERE IsDelete=0 AND ReturnDate=@ForDate  UNION ALL " & _
                                 " SELECT  N'အခြား ဝင်ငွေ' AS Title,'' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome WHERE IsDelete=0 AND IncomeDate=@ForDate UNION ALL" & _
                                 " SELECT  N'အကြွေးပေးငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PayAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_CashReceipt where IsDelete=0 AND PayDate = @ForDate AND Type<>'PurchaseInvoice' UNION ALL" & _
                                 " SELECT N'ပစ္စည်း အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader WHERE IsGem=0 AND IsDelete=0 AND IsChange=0 AND PurchaseDate  between @FromDate And @ToDate " & " UNION ALL" & _
                                 " SELECT N'စိန်/ကျောက် အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader WHERE IsGem=1 AND IsDelete=0 AND PurchaseDate between @FromDate And @ToDate " & " UNION ALL" & _
                                 " SELECT N'နေ့စဉ် အသုံးစရိတ်' AS Title, '' as MyanTitle, 0 AS DebitAmount, ISNULL(SUM(TotalAmount),0) AS CreditAmount,'Out' AS Type FROM tbl_DailyExpense WHERE ExpenseDate=@ForDate IsDelete=0 AND UNION ALL " & _
                                 " SELECT N'အကြွေးပေးငွေ' AS Title, '' as MyanTitle, 0 AS DebitAmount, ISNULL(SUM(PayAmount),0) AS CreditAmount, 'Out' AS Type From tbl_CashReceipt where IsDelete=0 AND PayDate = @ForDate AND Type='PurchaseInvoice'  UNION ALL " & _
                                 " SELECT N'အပ်ငွေ' AS Title,'' as MyanTitle, 0 AS DebitAmount, CreditAmount, 'Out' AS Type from tbl_GeneralLedgerbyLocation" & _
                                 " where GLDate = @ForDate and Title ='အပ်ငွေ'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ForDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ForDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetGeneralLedgerByLocation(ByVal ForDate As Date, ByVal CriStr As String, ByVal LocationID As String) As DataTable Implements IGeneralLedgerByLocationDA.GetGeneralLedgerByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim dtOption As DataTable

            Try
                'If IsBank = False Then
                '    cristr = " AND Title<> N'အကြွေးဆပ်မှ ရငွေ(Bank)' AND Title<> N'အခြား ဝင်ငွေ(Bank)' AND Title<> N'နေ့စဉ် အသုံးစရိတ်(Bank)' "
                'Else
                '    cristr = " AND Title<> N'အကြွေးဆပ်မှ ရငွေ(Cash)' AND Title<> N'အခြား ဝင်ငွေ(Cash)' AND Title<> N'နေ့စဉ် အသုံးစရိတ်(Cash)' "
                'End If
                strCommandText = "SELECT *  From tbl_GeneralLedgerByLocation Where GLDate Between @FromDate And @ToDate And LocationID='" & LocationID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@ForDate", DbType.Date, ForDate)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ForDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ForDate.Date & " 23:59:59"))
                dtOption = DB.ExecuteDataSet(DBComm).Tables(0)

                If dtOption.Rows.Count > 0 Then
                    strCommandText = " SELECT * FROM (SELECT N'မတည်ငွေ' AS Title,'' as MyanTitle,ISNULL(Sum(DebitAmount),0) AS DebitAmount, 0 AS CreditAmount," & _
                            " 'In' AS Type  From tbl_GeneralLedgerByLocation As H Where GLDate Between @FromDate And @ToDate And Title=N'မတည်ငွေ' And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' AND PaidAmount>0 AND IsAdvance=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(WholeSale)' AS Title, '' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_WholeSaleInvoice As H" & _
                            " where IsDelete = 0 And WDate      BETWEEN @FromDate And @ToDate AND   PaidAmount>0  And (PayType='0' OR PayType='2' OR PayType='3')  And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(ConsignmentSale)' AS Title, '' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_ConsignmentSale As H  where IsDelete=0 AND  PaidAmount>0 AND ConsignDate " & _
                            " BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းလဲခြင်းမှ ရငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) " & _
                            " AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount>0 AND IsAdvance=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'စရံအရောင်းမှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(AllAdvanceAmount),0) as DebitAmount," & _
                            " 0 as CreditAmount, 'In' AS Type from tbl_SaleInvoiceHeader As H where IsDelete=0 AND EntryAdvanceDate BETWEEN @FromDate And @ToDate AND IsAdvance =1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စရံအရောင်းပြန်ရွေးမှ ရငွေ' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PaidAmount>0 AND IsAdvance=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်း(Volume)မှ ရငွေ' AS Title,'' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID = '' AND PaidAmount>0  And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'ပစ္စည်းလဲခြင်း(Volume)မှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(PaidAmount),0) as DebitAmount, " & _
                            " 0 as CreditAmount, 'In' AS Type  from tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်း(LooseDiamond)မှ ရငွေ' AS Title,'' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID = '' AND PaidAmount>0  And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'ပစ္စည်းလဲခြင်း(LooseDiamond)မှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(PaidAmount),0) as DebitAmount, " & _
                            " 0 as CreditAmount, 'In' AS Type  from tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/ကျောက်ရောင်းခြင်းမှ ရငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SaleGems As H where IsDelete=0 AND SDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပ်ထည်လက်ခံစရံငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(M.DebitAmount),0) AS DebitAmount,0 AS CreditAmount, " & _
                            " 'In' AS Type FROM  (SELECT ISNULL(SUM(AdvanceAmount),0) AS DebitAmount FROM tbl_OrderInvoice As H" & _
                            " WHERE IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT ISNULL(SUM(SecondAdvanceAmount),0) AS DebitAmount FROM tbl_OrderInvoice As H" & _
                            " WHERE SecondAdvanceDate BETWEEN @FromDate And @ToDate AND IsDelete=0 And H.LocationID='" & LocationID & "' ) AS M " & _
                            " UNION ALL  " & _
                            " SELECT N'အပ်ထည်ရွေးမှ ရငွေ' AS Title,'' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_OrderReturnHeader As H WHERE IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And PaidAmount >0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " select N'ပြင်ထည်လက်ခံစရံငွေ' as Title,'' as MyanTitle,isnull(sum(AdvanceRepairAmount),0) as DebitAmount,0 AS CreditAmount, 'In' AS Type" & _
                            " from tbl_RepairHeader As H where IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " select N'ပြင်ထည်ရွေးမှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(ReturnPaidAmount),0) as DebitAmount,0 AS CreditAmount, 'In' AS Type" & _
                            " from tbl_ReturnRepairHeader As H where IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And ReturnPaidAmount >0 And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " SELECT N'အကြွေးဆပ်မှ ရငွေ(Bank)' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PayAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @FromDate And @ToDate AND Type<>'PurchaseInvoice' AND PayAmount>0 AND IsBank=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                             " SELECT N'အကြွေးဆပ်မှ ရငွေ(Cash)' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PayAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @FromDate And @ToDate AND Type<>'PurchaseInvoice' AND PayAmount>0 AND IsBank=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            "  SELECT N'ReturnAdvance' AS Title, '' as MyanTitle,ISNULL(SUM(NetAmount),0) AS DebitAmount,  0 AS CreditAmount, 'In' AS Type From tbl_ReturnAdvance As H where IsDelete=0 AND ReturnAdvanceDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အခြား ဝင်ငွေ(Cash)' AS Title,'' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome As H WHERE IncomeDate BETWEEN @FromDate And @ToDate AND IsDelete=0 AND IsBank=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အခြား ဝင်ငွေ(Bank)' AS Title,'' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome As H WHERE IncomeDate BETWEEN @FromDate And @ToDate AND IsDelete=0 AND IsBank=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " Select N'အပေါင်ပစ္စည်းအတိုးသတ်ခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            "'In' AS Type From tbl_MortgageInterest As H " & _
                            " where IsDelete=0 AND InterestPaidDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းအရင်းဆပ်ခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            " 'In' AS Type From tbl_MortgagePayback As H " & _
                            " where PaybackDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                             " UNION ALL " & _
                            " Select N'အပေါင်ပစ္စည်းရွေးခြင်းမှ ရငွေ' as Title, '' as MyanTitle,isnull(sum(MR.DebitAmount),0) as DebitAmount, 0 AS CreditAmount,'In' AS Type " & _
                            " FROM  (SELECT isnull(sum(PaidAmount),0) as DebitAmount from tbl_MortgageReturn As H " & _
                           " where IsDelete=0  AND ReturnDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                           " UNION ALL " & _
                           " SELECT isnull(sum(PaidAmount),0) as DebitAmount from tbl_MortgageInvoice As H " & _
                           " where IsDelete=0  AND ReturnDate BETWEEN @FromDate And @ToDate And IsReturn=1 And H.LocationID='" & LocationID & "') AS MR " & _
                           " Union All " & _
                            " SELECT N'လဲခြင်းမှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SaleInvoiceHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 AND IsOtherCash=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " select N'စရံအရောင်း(Cancel)မှ အမ်းငွေ' as Title,'' as MyanTitle,0 AS DebitAmount,isnull(sum(AllAdvanceAmount+PaidAmount+PurchaseAmount+OtherCashAmount),0) as CreditAmount,'Out' as Type from tbl_SaleInvoiceHeader As H " & _
                            " where CancelDate BETWEEN @FromDate And @ToDate " & _
                            " And IsAdvance=1 AND IsCancel=1 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " select N'နိုင်ငံခြားငွေမှ အမ်းငွေ' as Title,'' as MyanTitle,0 AS DebitAmount,ISNULL(SUM(0-PaidAmount),0) AS CreditAmount,'Out' as Type from tbl_SaleInvoiceHeader As H " & _
                            " where SaleDate BETWEEN @FromDate And @ToDate And IsOtherCash=1 AND PaidAmount<0 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " SELECT N'လဲခြင်း(Volume)မှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " SELECT N'လဲခြင်း(LooseDiamond)မှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်း အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H " & _
                            " WHERE IsDelete=0 AND IsGem=0 AND IsLooseDiamond=0 AND IsChange=0 " & _
                            " AND PurchaseDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/ကျောက် အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H WHERE IsGem=1 " & _
                            " AND IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/အကြွေ အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H WHERE IsLooseDiamond=1 " & _
                            " AND IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'အော်ဒါရွေးမှ အမ်းငွေ' as Title, '' as MyanTitle,0 as DebitAmount,isnull(sum(0-PaidAmount),0) as CreditAmount," & _
                            " 'Out' AS Type from tbl_OrderReturnHeader As H Left Join tbl_OrderInvoice O ON H.OrderInvoiceID = O.OrderInvoiceID " & _
                            " where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate And H.PaidAmount < 0 And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " select N'ပြင်ထည်ရွေးမှ အမ်းငွေ' as Title, '' as MyanTitle,0 as DebitAmount,isnull(sum(0-ReturnPaidAmount),0) as CreditAmount, " & _
                            " 'Out' AS Type from tbl_ReturnRepairHeader R Left Join tbl_RepairHeader H On R.RepairID = H.RepairID " & _
                            " where R.IsDelete=0 AND R.ReturnDate BETWEEN @FromDate And @ToDate and R.ReturnPaidAmount < 0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'နေ့စဉ် အသုံးစရိတ်(Cash)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(TotalAmount),0) AS CreditAmount,'Out' AS Type FROM tbl_DailyExpense As H WHERE IsDelete=0 AND IsBank=0 And ExpenseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'နေ့စဉ် အသုံးစရိတ်(Bank)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(TotalAmount),0) AS CreditAmount,'Out' AS Type FROM tbl_DailyExpense As H WHERE IsDelete=0 AND IsBank=1 AND ExpenseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းပြန်သွင်းခြင်းမှ ထွက်ငွေ(WholeSale Return)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(PaidAmount),0)  AS CreditAmount," & _
                            "'Out' AS Type From tbl_WholesaleReturn As H " & _
                            " where  SaleReturnAmount> 0 AND IsDelete=0 AND WReturnDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းလက်ခံခြင်းမှ ချေးငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount,  ISNULL(SUM(TotalAmount),0) AS CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_MortgageInvoice As H WHERE IsDelete=0 and ReceiveDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အကြွေးပေးငွေ' AS Title, '' as MyanTitle," & _
                            " 0 AS DebitAmount, ISNULL(SUM(0-PayAmount),0) AS CreditAmount, 'Out' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @FromDate And @ToDate AND Type<>'PurchaseInvoice' AND PayAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'အပ်ငွေ' AS Title,'' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(CreditAmount),0) AS CreditAmount," & _
                            " 'Out' AS Type   From tbl_GeneralLedgerByLocation As H Where GLDate BETWEEN @FromDate And @ToDate AND Title=N'အပ်ငွေ' And H.LocationID='" & LocationID & "') AS M Where( M.DebitAmount<>0 OR M.CreditAmount<>0 Or Title=N'မတည်ငွေ' Or  Title=N'အပ်ငွေ') " & CriStr

                    'In WholeSaleReturn Sale Only Data is in OutAmt,In WholeSale Cash only in Amount

                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ForDate.Date & " 00:00:00"))
                    DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ForDate.Date & " 23:59:59"))
                    dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                    Return dtResult
                Else
                    strCommandText = " SELECT * FROM ( SELECT N'မတည်ငွေ' AS Title,'' as MyanTitle,(SUM(DebitAmount)-SUM(CreditAmount)) as DebitAmount,0 AS CreditAmount, 'In' AS Type FROM (" & _
                             " SELECT N'မတည်ငွေ' AS Title,'' as MyanTitle,ISNULL(Sum(DebitAmount),0) AS DebitAmount, 0 AS CreditAmount," & _
                            " 'In' AS Type  From tbl_GeneralLedgerByLocation As H Where GLDate Between @DFromDate And @DToDate And Title=N'မတည်ငွေ' And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID='' AND PaidAmount>0 AND IsAdvance=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(WholeSale)' AS Title, '' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_WholeSaleInvoice As H" & _
                            " where IsDelete = 0 And WDate      BETWEEN @DFromDate And @DToDate AND   PaidAmount>0  And (PayType='0' OR PayType='2' OR PayType='3')  And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(ConsignmentSale)' AS Title, '' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_ConsignmentSale As H  where IsDelete=0 AND  PaidAmount>0 AND ConsignDate " & _
                            " BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းလဲခြင်းမှ ရငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) " & _
                            " AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID<>'' AND PaidAmount>0 AND IsAdvance=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'စရံအရောင်းမှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(AllAdvanceAmount),0) as DebitAmount," & _
                            " 0 as CreditAmount, 'In' AS Type from tbl_SaleInvoiceHeader As H where IsDelete=0 AND EntryAdvanceDate BETWEEN @DFromDate And @DToDate AND IsAdvance =1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စရံအရောင်းပြန်ရွေးမှ ရငွေ' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PaidAmount>0 AND IsAdvance=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်း(Volume)မှ ရငွေ' AS Title,'' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID = '' AND PaidAmount>0  And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'ပစ္စည်းလဲခြင်း(Volume)မှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(PaidAmount),0) as DebitAmount, " & _
                            " 0 as CreditAmount, 'In' AS Type  from tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်း(LooseDiamond)မှ ရငွေ' AS Title,'' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID = '' AND PaidAmount>0  And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'ပစ္စည်းလဲခြင်း(LooseDiamond)မှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(PaidAmount),0) as DebitAmount, " & _
                            " 0 as CreditAmount, 'In' AS Type  from tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/ကျောက်ရောင်းခြင်းမှ ရငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SaleGems As H where IsDelete=0 AND SDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပ်ထည်လက်ခံစရံငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(M.DebitAmount),0) AS DebitAmount,0 AS CreditAmount, " & _
                            " 'In' AS Type FROM  (SELECT ISNULL(SUM(AdvanceAmount),0) AS DebitAmount FROM tbl_OrderInvoice As H" & _
                            " WHERE IsDelete=0 AND OrderDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT ISNULL(SUM(SecondAdvanceAmount),0) AS DebitAmount FROM tbl_OrderInvoice As H" & _
                            " WHERE SecondAdvanceDate BETWEEN @DFromDate And @DToDate AND IsDelete=0 And H.LocationID='" & LocationID & "' ) AS M " & _
                            " UNION ALL  " & _
                            " SELECT N'အပ်ထည်ရွေးမှ ရငွေ' AS Title,'' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_OrderReturnHeader As H WHERE IsDelete=0 AND ReturnDate BETWEEN @DFromDate And @DToDate And PaidAmount >0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " select N'ပြင်ထည်လက်ခံစရံငွေ' as Title,'' as MyanTitle,isnull(sum(AdvanceRepairAmount),0) as DebitAmount,0 AS CreditAmount, 'In' AS Type" & _
                            " from tbl_RepairHeader As H where IsDelete=0 AND RepairDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " select N'ပြင်ထည်ရွေးမှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(ReturnPaidAmount),0) as DebitAmount,0 AS CreditAmount, 'In' AS Type" & _
                            " from tbl_ReturnRepairHeader As H where IsDelete=0 AND ReturnDate BETWEEN @DFromDate And @DToDate And ReturnPaidAmount >0 And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " SELECT N'အကြွေးဆပ်မှ ရငွေ(Bank)' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PayAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @DFromDate And @DToDate AND Type<>'PurchaseInvoice' AND PayAmount>0 AND IsBank=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                             " SELECT N'အကြွေးဆပ်မှ ရငွေ(Cash)' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PayAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @DFromDate And @DToDate AND Type<>'PurchaseInvoice' AND PayAmount>0 AND IsBank=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            "  SELECT N'ReturnAdvance' AS Title, '' as MyanTitle,ISNULL(SUM(NetAmount),0) AS DebitAmount,  0 AS CreditAmount, 'In' AS Type From tbl_ReturnAdvance As H where IsDelete=0 AND ReturnAdvanceDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အခြား ဝင်ငွေ(Cash)' AS Title,'' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome As H WHERE IncomeDate BETWEEN @DFromDate And @DToDate AND IsDelete=0 AND IsBank=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အခြား ဝင်ငွေ(Bank)' AS Title,'' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome As H WHERE IncomeDate BETWEEN @DFromDate And @DToDate AND IsDelete=0 AND IsBank=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " Select N'အပေါင်ပစ္စည်းအတိုးသတ်ခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            "'In' AS Type From tbl_MortgageInterest As H " & _
                            " where IsDelete=0 AND InterestPaidDate  BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းအရင်းဆပ်ခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            " 'In' AS Type From tbl_MortgagePayback As H " & _
                            " where PaybackDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                             " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းရွေးခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            "'In' AS Type From tbl_MortgageReturn As H " & _
                            " where IsDelete=0 AND ReturnDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'လဲခြင်းမှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SaleInvoiceHeader As H where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 AND IsOtherCash=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " select N'စရံအရောင်း(Cancel)မှ အမ်းငွေ' as Title,'' as MyanTitle,0 AS DebitAmount,isnull(sum(AllAdvanceAmount+PaidAmount+PurchaseAmount+OtherCashAmount),0) as CreditAmount,'Out' as Type from tbl_SaleInvoiceHeader As H " & _
                            " where CancelDate BETWEEN @DFromDate And @DToDate " & _
                            " And IsAdvance=1 AND IsCancel=1 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " select N'နိုင်ငံခြားငွေမှ အမ်းငွေ' as Title,'' as MyanTitle,0 AS DebitAmount,ISNULL(SUM(0-PaidAmount),0) AS CreditAmount,'Out' as Type from tbl_SaleInvoiceHeader As H " & _
                            " where SaleDate BETWEEN @DFromDate And @DToDate And IsOtherCash=1 AND PaidAmount<0 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " SELECT N'လဲခြင်း(Volume)မှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " SELECT N'လဲခြင်း(LooseDiamond)မှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @DFromDate And @DToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်း အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H " & _
                            " WHERE IsDelete=0 AND IsGem=0 AND  IsLooseDiamond=0 AND IsChange=0 " & _
                            " AND PurchaseDate  BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/ကျောက် အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H WHERE IsGem=1 " & _
                            " AND IsDelete=0 AND PurchaseDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/အကြွေ အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H WHERE IsLooseDiamond=1 " & _
                            " AND IsDelete=0 AND PurchaseDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'အော်ဒါရွေးမှ အမ်းငွေ' as Title, '' as MyanTitle,0 as DebitAmount,isnull(sum(0-PaidAmount),0) as CreditAmount," & _
                            " 'Out' AS Type from tbl_OrderReturnHeader As H Left Join tbl_OrderInvoice O ON H.OrderInvoiceID = O.OrderInvoiceID " & _
                            " where H.IsDelete=0 AND H.ReturnDate BETWEEN @DFromDate And @DToDate And H.PaidAmount < 0 And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " select N'ပြင်ထည်ရွေးမှ အမ်းငွေ' as Title, '' as MyanTitle,0 as DebitAmount,isnull(sum(0-ReturnPaidAmount),0) as CreditAmount, " & _
                            " 'Out' AS Type from tbl_ReturnRepairHeader R Left Join tbl_RepairHeader H On R.RepairID = H.RepairID " & _
                            " where R.IsDelete=0 AND R.ReturnDate BETWEEN @DFromDate And @DToDate and R.ReturnPaidAmount < 0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'နေ့စဉ် အသုံးစရိတ်(Cash)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(TotalAmount),0) AS CreditAmount,'Out' AS Type FROM tbl_DailyExpense As H WHERE IsDelete=0 AND IsBank=0 And ExpenseDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'နေ့စဉ် အသုံးစရိတ်(Bank)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(TotalAmount),0) AS CreditAmount,'Out' AS Type FROM tbl_DailyExpense As H WHERE IsDelete=0 AND IsBank=1 AND ExpenseDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းပြန်သွင်းခြင်းမှ ထွက်ငွေ(WholeSale Return)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(PaidAmount),0)  AS CreditAmount," & _
                            "'Out' AS Type From tbl_WholesaleReturn As H " & _
                            " where  SaleReturnAmount> 0 AND IsDelete=0 AND WReturnDate  BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းလက်ခံခြင်းမှ ချေးငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount,  ISNULL(SUM(TotalAmount),0) AS CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_MortgageInvoice As H WHERE IsDelete=0 and ReceiveDate BETWEEN @DFromDate And @DToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အကြွေးပေးငွေ' AS Title, '' as MyanTitle," & _
                            " 0 AS DebitAmount, ISNULL(SUM(0-PayAmount),0) AS CreditAmount, 'Out' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @DFromDate And @DToDate AND Type<>'PurchaseInvoice' AND PayAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'အပ်ငွေ' AS Title,'' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(CreditAmount),0) AS CreditAmount," & _
                            " 'Out' AS Type   From tbl_GeneralLedgerByLocation As H Where GLDate BETWEEN @DFromDate And @DToDate AND Title=N'အပ်ငွေ' And H.LocationID='" & LocationID & "') AS M  " & CriStr & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' AND PaidAmount>0 AND IsAdvance=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(WholeSale)' AS Title, '' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_WholeSaleInvoice As H" & _
                            " where IsDelete = 0 And WDate      BETWEEN @FromDate And @ToDate AND   PaidAmount>0  And (PayType='0' OR PayType='2' OR PayType='3')  And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(ConsignmentSale)' AS Title, '' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_ConsignmentSale As H  where IsDelete=0 AND  PaidAmount>0 AND ConsignDate " & _
                            " BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းလဲခြင်းမှ ရငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) " & _
                            " AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount>0 AND IsAdvance=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'စရံအရောင်းမှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(AllAdvanceAmount),0) as DebitAmount," & _
                            " 0 as CreditAmount, 'In' AS Type from tbl_SaleInvoiceHeader As H where IsDelete=0 AND EntryAdvanceDate BETWEEN @FromDate And @ToDate AND IsAdvance =1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စရံအရောင်းပြန်ရွေးမှ ရငွေ' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_SaleInvoiceHeader As H " & _
                            " where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PaidAmount>0 AND IsAdvance=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်း(Volume)မှ ရငွေ' AS Title,'' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID = '' AND PaidAmount>0  And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'ပစ္စည်းလဲခြင်း(Volume)မှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(PaidAmount),0) as DebitAmount, " & _
                            " 0 as CreditAmount, 'In' AS Type  from tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းရောင်းခြင်း(LooseDiamond)မှ ရငွေ' AS Title,'' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID = '' AND PaidAmount>0  And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'ပစ္စည်းလဲခြင်း(LooseDiamond)မှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(PaidAmount),0) as DebitAmount, " & _
                            " 0 as CreditAmount, 'In' AS Type  from tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/ကျောက်ရောင်းခြင်းမှ ရငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type From tbl_SaleGems As H where IsDelete=0 AND SDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပ်ထည်လက်ခံစရံငွေ' AS Title, '' as MyanTitle,ISNULL(SUM(M.DebitAmount),0) AS DebitAmount,0 AS CreditAmount, " & _
                            " 'In' AS Type FROM  (SELECT ISNULL(SUM(AdvanceAmount),0) AS DebitAmount FROM tbl_OrderInvoice As H" & _
                            " WHERE IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT ISNULL(SUM(SecondAdvanceAmount),0) AS DebitAmount FROM tbl_OrderInvoice As H" & _
                            " WHERE SecondAdvanceDate BETWEEN @FromDate And @ToDate AND IsDelete=0 And H.LocationID='" & LocationID & "' ) AS M " & _
                            " UNION ALL  " & _
                            " SELECT N'အပ်ထည်ရွေးမှ ရငွေ' AS Title,'' as MyanTitle, ISNULL(SUM(PaidAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_OrderReturnHeader As H WHERE IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And PaidAmount >0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " select N'ပြင်ထည်လက်ခံစရံငွေ' as Title,'' as MyanTitle,isnull(sum(AdvanceRepairAmount),0) as DebitAmount,0 AS CreditAmount, 'In' AS Type" & _
                            " from tbl_RepairHeader As H where IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " select N'ပြင်ထည်ရွေးမှ ရငွေ' as Title,'' as MyanTitle,isnull(sum(ReturnPaidAmount),0) as DebitAmount,0 AS CreditAmount, 'In' AS Type" & _
                            " from tbl_ReturnRepairHeader As H where IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And ReturnPaidAmount >0 And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " SELECT N'အကြွေးဆပ်မှ ရငွေ(Bank)' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PayAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @FromDate And @ToDate AND Type<>'PurchaseInvoice' AND PayAmount>0 AND IsBank=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                             " SELECT N'အကြွေးဆပ်မှ ရငွေ(Cash)' AS Title, '' as MyanTitle," & _
                            " ISNULL(SUM(PayAmount),0) AS DebitAmount, 0 AS CreditAmount, 'In' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @FromDate And @ToDate AND Type<>'PurchaseInvoice' AND PayAmount>0 AND IsBank=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            "  SELECT N'ReturnAdvance' AS Title, '' as MyanTitle,ISNULL(SUM(NetAmount),0) AS DebitAmount,  0 AS CreditAmount, 'In' AS Type From tbl_ReturnAdvance As H where IsDelete=0 AND ReturnAdvanceDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အခြား ဝင်ငွေ(Cash)' AS Title,'' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome As H WHERE IncomeDate BETWEEN @FromDate And @ToDate AND IsDelete=0 AND IsBank=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အခြား ဝင်ငွေ(Bank)' AS Title,'' as MyanTitle,ISNULL(SUM(TotalAmount),0) AS DebitAmount, " & _
                            " 0 AS CreditAmount, 'In' AS Type FROM tbl_DailyIncome As H WHERE IncomeDate BETWEEN @FromDate And @ToDate AND IsDelete=0 AND IsBank=1 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " Select N'အပေါင်ပစ္စည်းအတိုးသတ်ခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            "'In' AS Type From tbl_MortgageInterest As H " & _
                            " where IsDelete=0 AND InterestPaidDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းအရင်းဆပ်ခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            " 'In' AS Type From tbl_MortgagePayback As H " & _
                            " where PaybackDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                             " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းရွေးခြင်းမှ ရငွေ' AS Title, '' as MyanTitle, " & _
                            " ISNULL(SUM(PaidAmount),0) AS DebitAmount, 0 AS CreditAmount, " & _
                            "'In' AS Type From tbl_MortgageReturn As H " & _
                            " where IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'လဲခြင်းမှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SaleInvoiceHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 AND IsOtherCash=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " select N'စရံအရောင်း(Cancel)မှ အမ်းငွေ' as Title,'' as MyanTitle,0 AS DebitAmount,isnull(sum(AllAdvanceAmount+PaidAmount+PurchaseAmount+OtherCashAmount),0) as CreditAmount,'Out' as Type from tbl_SaleInvoiceHeader As H " & _
                            " where CancelDate BETWEEN @FromDate And @ToDate " & _
                            " And IsAdvance=1 AND IsCancel=1 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " select N'နိုင်ငံခြားငွေမှ အမ်းငွေ' as Title,'' as MyanTitle,0 AS DebitAmount,ISNULL(SUM(0-PaidAmount),0) AS CreditAmount,'Out' as Type from tbl_SaleInvoiceHeader As H " & _
                            " where SaleDate BETWEEN @FromDate And @ToDate And IsOtherCash=1 AND PaidAmount<0 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " SELECT N'လဲခြင်း(Volume)မှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL" & _
                             " SELECT N'လဲခြင်း(LooseDiamond)မှ အမ်းငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(0-PaidAmount),0) AS  CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်း အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H " & _
                            " WHERE IsDelete=0 AND IsGem=0 AND IsLooseDiamond=0 AND IsChange=0 " & _
                            " AND PurchaseDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/ကျောက် အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H WHERE IsGem=1 " & _
                            " AND IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'စိန်/အကြွေ အဝယ်ငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount, " & _
                            " ISNULL(SUM(AllPaidAmount),0) AS CreditAmount, 'Out' AS Type FROM tbl_PurchaseHeader As H WHERE IsLooseDiamond=1 " & _
                            " AND IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " Union All " & _
                            " Select N'အော်ဒါရွေးမှ အမ်းငွေ' as Title, '' as MyanTitle,0 as DebitAmount,isnull(sum(0-PaidAmount),0) as CreditAmount," & _
                            " 'Out' AS Type from tbl_OrderReturnHeader As H Left Join tbl_OrderInvoice O ON H.OrderInvoiceID = O.OrderInvoiceID " & _
                            " where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate And H.PaidAmount < 0 And H.LocationID='" & LocationID & "'" & _
                            " Union All" & _
                            " select N'ပြင်ထည်ရွေးမှ အမ်းငွေ' as Title, '' as MyanTitle,0 as DebitAmount,isnull(sum(0-ReturnPaidAmount),0) as CreditAmount, " & _
                            " 'Out' AS Type from tbl_ReturnRepairHeader R Left Join tbl_RepairHeader H On R.RepairID = H.RepairID " & _
                            " where R.IsDelete=0 AND R.ReturnDate BETWEEN @FromDate And @ToDate and R.ReturnPaidAmount < 0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'နေ့စဉ် အသုံးစရိတ်(Cash)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(TotalAmount),0) AS CreditAmount,'Out' AS Type FROM tbl_DailyExpense As H WHERE IsDelete=0 AND IsBank=0 And ExpenseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'နေ့စဉ် အသုံးစရိတ်(Bank)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(TotalAmount),0) AS CreditAmount,'Out' AS Type FROM tbl_DailyExpense As H WHERE IsDelete=0 AND IsBank=1 AND ExpenseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'ပစ္စည်းပြန်သွင်းခြင်းမှ ထွက်ငွေ(WholeSale Return)' AS Title, '' as MyanTitle, 0 AS DebitAmount, " & _
                            " ISNULL(SUM(PaidAmount),0)  AS CreditAmount," & _
                            "'Out' AS Type From tbl_WholesaleReturn As H " & _
                            " where  SaleReturnAmount> 0 AND IsDelete=0 AND WReturnDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပေါင်ပစ္စည်းလက်ခံခြင်းမှ ချေးငွေ' AS Title, '' as MyanTitle,0 AS DebitAmount,  ISNULL(SUM(TotalAmount),0) AS CreditAmount, " & _
                            " 'Out' AS Type FROM tbl_MortgageInvoice As H WHERE IsDelete=0 and ReceiveDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အကြွေးပေးငွေ' AS Title, '' as MyanTitle," & _
                            " 0 AS DebitAmount, ISNULL(SUM(0-PayAmount),0) AS CreditAmount, 'Out' AS Type From tbl_CashReceipt As H " & _
                            " where IsDelete=0 AND PayDate BETWEEN @FromDate And @ToDate AND Type<>'PurchaseInvoice' AND PayAmount<0 And H.LocationID='" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'အပ်ငွေ' AS Title,'' as MyanTitle,0 AS DebitAmount, ISNULL(SUM(CreditAmount),0) AS CreditAmount," & _
                            " 'Out' AS Type   From tbl_GeneralLedgerByLocation As H Where GLDate BETWEEN @FromDate And @ToDate AND Title=N'အပ်ငွေ' And H.LocationID='" & LocationID & "') AS M Where( M.DebitAmount<>0 OR M.CreditAmount<>0 Or Title=N'မတည်ငွေ' Or  Title=N'အပ်ငွေ') " & CriStr

                    'In WholeSaleReturn Sale Only Data is in OutAmt,In WholeSale Cash only in Amount

                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@DFromDate", DbType.DateTime, CDate(DateAdd(DateInterval.Day, -1, ForDate) & " 00:00:00"))
                    DB.AddInParameter(DBComm, "@DToDate", DbType.DateTime, CDate(DateAdd(DateInterval.Day, -1, ForDate) & " 23:59:59"))
                    DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ForDate.Date & " 00:00:00"))
                    DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ForDate.Date & " 23:59:59"))
                    dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                    Return dtResult
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllCashInCashOutReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal CriStr As String, ByVal LocationID As String) As System.Data.DataSet Implements IGeneralLedgerByLocationDA.GetAllCashInCashOutReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataSet
            Try



                strCommandText = " SELECT * FROM  (Select N'မတည်ငွေ' as InTitle, isnull(sum(DebitAmount),0) as InAmount,'' as OutTitle,0 as OutAmount" & _
                       " from tbl_GeneralLedgerByLocation H  where GLDate BETWEEN @FromDate And @ToDate AND Title=N'မတည်ငွေ' And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ' as InTitle,isnull(sum(M.DebitAmount),0) as InAmount," & _
                       " '' as OutTitle,0 as OutAmount FROM " & _
                       " (SELECT isnull(sum(PaidAmount),0) as DebitAmount from tbl_SaleInvoiceHeader As H where IsDelete=0  AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' AND PaidAmount>0 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " select isnull(sum(PaidAmount),0) as DebitAmount " & _
                       " from tbl_SaleInvoiceHeader As H  where SaleDate BETWEEN @FromDate And @ToDate" & _
                       " AND PurchaseHeaderID<>'' AND PaidAmount>0 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select isnull(sum(AllAdvanceAmount),0) as DebitAmount " & _
                       " from tbl_SaleInvoiceHeader As H where EntryAdvanceDate BETWEEN @FromDate And @ToDate AND IsAdvance =1 AND IsDelete=0 And H.LocationID='" & LocationID & "') AS M" & _
                       " Union All" & _
                       " Select N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(WholeSale)' as InTitle,isnull(sum(PaidAmount),0) as InAmount," & _
                       " '' as OutTitle,0 as OutAmount FROM " & _
                       "  tbl_WholeSaleInvoice As H where IsDelete=0  AND  PaidAmount>0  And (PayType='0' OR PayType='2' OR PayType='3') AND WDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'ပစ္စည်းရောင်းခြင်းမှ ရငွေ(ConsignmentSale)' as InTitle,isnull(sum(PaidAmount),0) as InAmount," & _
                       " '' as OutTitle,0 as OutAmount FROM " & _
                       "  tbl_ConsignmentSale As H where IsDelete=0 AND PaidAmount>0  AND ConsignDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'ပစ္စည်းရောင်းခြင်း(Volume)မှ ရငွေ' as InTitle,isnull(sum(B.DebitAmount),0) as InAmount, '' as OutTitle, 0 as OutAmount  FROM " & _
                       " (SELECT isnull(sum(PaidAmount),0) as DebitAmount FROM tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' And H.LocationID='" & LocationID & "'" & _
                        " Union All" & _
                       " Select isnull(sum(PaidAmount),0) as DebitAmount from tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "') AS B" & _
                       " Union All" & _
                        " Select N'ပစ္စည်းရောင်းခြင်း(LooseDiamond)မှ ရငွေ' as InTitle,isnull(sum(L.DebitAmount),0) as InAmount, '' as OutTitle, 0 as OutAmount  FROM " & _
                       " (SELECT isnull(sum(PaidAmount),0) as DebitAmount FROM tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' And H.LocationID='" & LocationID & "'" & _
                        " Union All" & _
                       " Select isnull(sum(PaidAmount),0) as DebitAmount from tbl_SaleLooseDiamondHeader As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID <> '' And PaidAmount>0 And H.LocationID='" & LocationID & "') AS L" & _
                       " Union All" & _
                       " select N'စိန်၊ကျောက်ရောင်းခြင်းမှ ရငွေ' as InTitle,isnull(sum(PaidAmount),0) as InAmount,'' as OutTitle,0 as OutAmount" & _
                       " from tbl_SaleGems As H where IsDelete=0 AND SDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'အပ်ထည်လက်ခံစရံငွေ' as InTitle, isnull(sum(M.DebitAmount),0) as InAmount, '' as OutTitle, 0 as OutAmount" & _
                       " FROM  (Select isnull(sum(AdvanceAmount),0) as DebitAmount from tbl_OrderInvoice As H where IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " UNION ALL" & _
                       " Select isnull(sum(SecondAdvanceAmount),0) as DebitAmount from tbl_OrderInvoice As H where IsDelete=0 AND SecondAdvanceDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "') As M" & _
                       " Union All" & _
                       " Select N'အပ်ထည်ရွေးမှ ရငွေ' as InTitle,isnull(sum(PaidAmount),0) as InAmount,'' as OutTitle," & _
                       " 0 as OutAmount from tbl_OrderReturnHeader As H where IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And PaidAmount > 0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " select N'ပြင်ထည်လက်ခံစရံငွေ' as InTitle,isnull(sum(AdvanceRepairAmount),0) as InAmount,'' as OutTitle,0 as OutAmount" & _
                       " from tbl_RepairHeader As H where IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " select N'ပြင်ထည်ရွေးမှ ရငွေ' as InTitle,isnull(sum(ReturnPaidAmount),0) as InAmount,'' as OutTitle,0 as OutAmount" & _
                       " from tbl_ReturnRepairHeader As H where IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And ReturnPaidAmount >0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'အကြွေးဆပ်မှ ရငွေ' as InTitle,isnull(sum(PayAmount),0) as InAmount, '' as OutTitle," & _
                       " 0 as OutAmount from tbl_CashReceipt As H where IsDelete=0 AND Type<>'PurchaseInvoice' and PayDate BETWEEN @FromDate And @ToDate AND PayAmount>0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " select N'Return Advance' as InTitle,isnull(sum(NetAmount),0) as InAmount,'' as OutTitle,0 as OutAmount" & _
                       " from tbl_ReturnAdvance As H where IsDelete=0 AND ReturnAdvanceDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All " & _
                       " Select N'အပေါင်ပစ္စည်းရွေးခြင်းမှ ရငွေ' as InTitle,isnull(sum(MR.DebitAmount),0) as InAmount, " & _
                       " '' as OutTitle,0 as OutAmount FROM  (SELECT isnull(sum(PaidAmount),0) as DebitAmount from tbl_MortgageReturn As H " & _
                       " where IsDelete=0  AND ReturnDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " UNION ALL " & _
                       " SELECT isnull(sum(PaidAmount),0) as DebitAmount from tbl_MortgageInvoice As H " & _
                       " where IsDelete=0  AND ReturnDate BETWEEN @FromDate And @ToDate And IsReturn=1 And H.LocationID='" & LocationID & "') AS MR " & _
                       " Union All " & _
                       " Select N'အပေါင်ပစ္စည်းအတိုးသတ်ခြင်းမှ ရငွေ' as InTitle,isnull(sum(MI.DebitAmount),0) as InAmount,  " & _
                       " '' as OutTitle,0 as OutAmount FROM  (SELECT isnull(sum(PaidAmount),0) as DebitAmount from tbl_MortgageInterest As H  " & _
                       " where IsDelete=0  AND InterestPaidDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "')  AS MI " & _
                       " Union All " & _
                       " Select N'အပေါင်ပစ္စည်းအရင်းဆပ်ခြင်းမှ ရငွေ' as InTitle,isnull(sum(MP.DebitAmount),0) as InAmount, " & _
                       " '' as OutTitle,0 as OutAmount FROM  (SELECT isnull(sum(PaidAmount),0) as DebitAmount from tbl_MortgagePayback As H " & _
                       " where  PaybackDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "') AS MP " & _
                       " Union All " & _
                       " Select N'အခြား ဝင်ငွေ' as InTitle,isnull(sum(TotalAmount),0) as InAmount,'' as OutTitle,0 as OutAmount" & _
                       " from tbl_DailyIncome As H where  IsDelete=0  " & CriStr & " AND IncomeDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "') AS A" & _
                       " SELECT * FROM  " & _
                       " (select N'အမ်းငွေ' as Title,isnull(sum(C.CreditAmount),0) as Amount FROM " & _
                       " (SELECT isnull(sum(0-PaidAmount),0) as CreditAmount from tbl_SaleInvoiceHeader As H  where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PaidAmount<0 AND IsOtherCash=0 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " select isnull(sum(AllAdvanceAmount+PaidAmount+PurchaseAmount+OtherCashAmount),0) as CreditAmount from tbl_SaleInvoiceHeader As H" & _
                       " where CancelDate BETWEEN @FromDate And @ToDate " & _
                       " And IsAdvance=1 AnD IsCancel=1 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                       " UNION ALL" & _
                      " SELECT isnull(sum(0-PaidAmount),0) as CreditAmount from tbl_SaleInvoiceHeader As H  where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PaidAmount<0 AND IsOtherCash=1 AND IsDelete=0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                      " select isnull(sum(0-PaidAmount),0) as CreditAmount" & _
                       " from tbl_SalesVolume As H where IsDelete=0 AND SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select isnull(sum(0-PaidAmount),0) as CreditAmount" & _
                       " from tbl_OrderReturnHeader H Left Join tbl_OrderInvoice O ON H.OrderInvoiceID = O.OrderInvoiceID" & _
                       " where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate And H.PaidAmount < 0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " select isnull(sum(0-ReturnPaidAmount),0) as Amount" & _
                       " from tbl_ReturnRepairHeader R Left Join tbl_RepairHeader H On R.RepairID = H.RepairID" & _
                       " where R.IsDelete=0 AND R.ReturnDate BETWEEN @FromDate And @ToDate and R.ReturnPaidAmount < 0 And H.LocationID='" & LocationID & "') AS C" & _
                        " Union All" & _
                       " Select N'အကြွေးပေးငွေ' as InTitle,isnull(sum(0-PayAmount),0) as Amount " & _
                       " from tbl_CashReceipt As H  where IsDelete=0 AND Type<>'PurchaseInvoice' and PayDate BETWEEN @FromDate And @ToDate AND PayAmount<0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " select N'ပစ္စည်း အဝယ်ငွေ' as Title,isnull(sum(AllPaidAmount),0) as Amount from tbl_PurchaseHeader As H " & _
                       " where IsDelete=0 AND IsGem=0 AND IsLooseDiamond=0 AND PurchaseDate  BETWEEN @FromDate And @ToDate" & _
                       " AND IsChange=0 And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'စိန်/ကျောက် အဝယ်ငွေ' as Title,isnull(sum(AllPaidAmount),0) as Amount from tbl_PurchaseHeader As H " & _
                       " where IsDelete=0 AND IsGem=1 AND PurchaseDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'စိန်အကြွေ အဝယ်ငွေ' as Title,isnull(sum(AllPaidAmount),0) as Amount from tbl_PurchaseHeader As H " & _
                       " where IsDelete=0 AND IsLooseDiamond=1 AND PurchaseDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All" & _
                       " Select N'နေ့စဉ် အသုံးစရိတ်' as Title,isnull(sum(TotalAmount),0) as Amount" & _
                       " from tbl_DailyExpense  As H where IsDelete=0  " & CriStr & " AND ExpenseDate BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union all " & _
                       " Select N'ပစ္စည်းပြန်သွင်းခြင်းမှ ထွက်ငွေ(WholeSale Return)' as Title,isnull(sum(PaidAmount),0) as Amount from tbl_WholesaleReturn As H " & _
                       " where IsDelete=0 AND   SaleReturnAmount> 0 AND  WReturnDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union all " & _
                       " Select N'အပေါင်ပစ္စည်းလက်ခံခြင်းမှ ချေးငွေ' as Title,isnull(sum(TotalAmount),0) as Amount from tbl_MortgageInvoice As H " & _
                       " where IsDelete=0 AND  ReceiveDate  BETWEEN @FromDate And @ToDate And H.LocationID='" & LocationID & "'" & _
                       " Union All " & _
                       " Select N'အပ်ငွေ' as Title,ISNULL(SUM(CreditAmount),0) as Amount  from tbl_GeneralLedgerByLocation As H where" & _
                       " GLDate BETWEEN @FromDate And @ToDate AND Title=N'အပ်ငွေ' And H.LocationID='" & LocationID & "') AS B"

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

        Public Function GetDailyTransactonByLocation(ForDate As Date, LocationID As String, ByVal CriStr As String) As DataTable Implements IGeneralLedgerByLocationDA.GetDailyTransactonByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = " SELECT * FROM  (Select N'မတည်ငွေ(Cash In)' as Title,'In' As Type, '-' As VoucherNo," & _
                '            " convert(varchar(10),GLDate,105) as Date,  '-' As Customer, sum(H.DebitAmount) As PaidAmount,0 As DiscountAmount," & _
                '            " H.LocationID, B.Location,'' AS  Remark, sum(H.DebitAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_GeneralLedgerByLocation H  LEFT JOIN tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where GLDate=@ForDate AND Title=N'မတည်ငွေ' And H.LocationID= '" & LocationID & "'" & _
                '            " Group By GLDate, H.LocationID, B.Location" & _
                '            " UNION ALL" & _
                '            " Select N'အရောင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 and H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' AND PaidAmount>=0 AND IsAdvance=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " Select N'WholeSale အရောင်း(Cash In)' as Title, 'In' As Type, WholeSaleInvoiceID As VoucherNo," & _
                '            " convert(varchar(10),WDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.Discount) As DiscountAmount, H.LocationID, B.Location ,'' as Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_WholeSaleInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND  PaidAmount>0  And (PayType='0' OR PayType='2' OR PayType='3') and H.WDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " Select N'ConsignmentSale အရောင်း(Cash In)' as Title, 'In' As Type, ConsignmentSaleID As VoucherNo," & _
                '            " convert(varchar(10),ConsignDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.Discount) As DiscountAmount, H.LocationID, B.Location ,'' as Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_ConsignmentSale H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.ConsignDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'Return Advance' as Title, 'In' As Type, ReturnAdvanceID As VoucherNo," & _
                '            "convert(varchar(10),ReturnAdvanceDate,105) as Date, C.CustomerName As Customer, H.NetAmount  As PaidAmount," & _
                '            " (H.Discount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.NetAmount) AS CashInAmount," & _
                '            "  0 As CashOutAmount  from tbl_ReturnAdvance H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID   " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 and H.ReturnAdvanceDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'လဲခြင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.SaleDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " AND PurchaseHeaderID<>'' AND PaidAmount>=0 AND IsAdvance=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL " & _
                '            " SELECT N'လဲခြင်းမှအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 AND IsOtherCash=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '             " Select  N'အပေါင်ရွေးခြင်းမှရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                '            "convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                '            "(H.AddOrSub) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount, " & _
                '            "0 As CashOutAmount  from tbl_MortgageReturn H LEFT JOIN tbl_MortgageInvoice M ON H.MortgageInvoiceID=M.MortgageInvoiceID LEFT JOIN tbl_Customer C ON C.CustomerID=M.CustomerID " & _
                '            "Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and H.ReturnDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL " & _
                '            " Select N'အပေါင်အတိုးသတ်ခြင်းမှရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),InterestPaidDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                '            " (H.InterestAmount-H.PaidAmount) As DiscountAmount, H.LocationID, B.Location ,S.Remark, (H.PaidAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_MortgageInterest H  Left Join tbl_MortgageInvoice S " & _
                '            " On H.MortgageInvoiceID = S.MortgageInvoiceID LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and H.InterestPaidDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL " & _
                '            " Select  N'အပေါင်ပစ္စည်းအရင်းဆပ်ခြင်းမှ ရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),PaybackDate,105) as Date, C.CustomerName As Customer, (H.PaybackAmt) As PaidAmount, " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaybackAmt) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_MortgageInvoice H " & _
                '            " Left Join tbl_MortgagePayback P On H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and P.PaybackDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL " & _
                '            " SELECT N'အပေါင်လက်ခံခြင်းမှချေးငွေ(Cash Out)' as Title, 'Out' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                '           " convert(varchar(10),ReceiveDate,105) as Date, C.CustomerName As Customer, " & _
                '           " (H.TotalAmount) As PaidAmount, 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '           " (H.TotalAmount) As CashOutAmount from tbl_MortgageInvoice H LEFT JOIN tbl_Customer C  " & _
                '           " ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '           " where H.ReceiveDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL " & _
                '            " SELECT N'OtherCashအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.SaleDate BETWEEN @FromDate And @ToDate AND IsOtherCash=1 AND PaidAmount<0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'စရံအရောင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, " & _
                '            " C.CustomerName As Customer, (H.AllAdvanceAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount,  " & _
                '            " H.LocationID, B.Location,H.Remark, (H.AllAdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.EntryAdvanceDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'စရံအရောင်းပြန်ရွေး(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, " & _
                '            " C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount,  " & _
                '            " H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.SaleDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND PaidAmount>0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'စရံအရောင်းအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, " & _
                '            " (H.AllAdvanceAmount+H.PaidAmount+H.PurchaseAmount+H.OtherCashAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount," & _
                '            " H.LocationID, B.Location,H.Remark, 0 AS CashInAmount," & _
                '            " (H.AllAdvanceAmount+H.PaidAmount+H.PurchaseAmount+H.OtherCashAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H " & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.CancelDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND IsCancel=1 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'အရောင်းမှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleInvoiceHeader S On H.VoucherNo = S.SaleInvoiceHeaderID LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.PayDate=@ForDate AND Type='SalesInvoice' AnD H.PayAmount>0 AND H.IsDelete=0 and S.IsDelete=0 " & CriStr & _
                '            " UNION ALL" & _
                '            " SELECT N'အရောင်းမှအကြွေးပေးငွေ(Cash Out)' as Title, 'Out' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (0-H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, (0-H.PayAmount) As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleInvoiceHeader S On H.VoucherNo = S.SaleInvoiceHeaderID LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.PayDate=@ForDate AND Type='SalesInvoice' AnD H.PayAmount<0 AND H.IsDelete=0 " & CriStr & _
                '            " UNION ALL" & _
                '             " Select N'အရောင်းVolume (Cash In)' as Title, 'In' As Type, SalesVolumeID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate AND PaidAmount>=0 AND PurchaseHeaderID='' And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'လဲခြင်းVolume (Cash In)' as Title, 'In' As Type, SalesVolumeID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & _
                '            " AND PurchaseHeaderID<>'' AND PaidAmount>=0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'လဲခြင်းVolume အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SalesVolumeID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '            " (0-H.PaidAmount) As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>''  " & _
                '            " AND PaidAmount<0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " Select N'အရောင်း(Volume)မှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SalesVolume S On H.VoucherNo = S.SalesVolumeID LEFT JOIN tbl_Customer C" & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.PayDate=@ForDate AND Type='SalesInvoiceVolume'" & CriStr & _
                '            " union all" & _
                '            " SELECT N'ကျောက်အရောင်း(Cash In)' as Title, 'In' As Type, SaleGemsID As VoucherNo, " & _
                '            " convert(varchar(10),SDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_SaleGems H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.SDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'ကျောက်ရောင်းမှ အကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleGems S On H.VoucherNo = S.SaleGemsID" & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.PayDate=@ForDate AND Type='SalesGems' " & CriStr & _
                '            " UNION ALL" & _
                '            " SELECT N'အဝယ်(Cash Out)' as Title, 'Out' As Type, H.PurchaseHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),PurchaseDate,105) as Date, C.CustomerName As Customer, (H.AllPaidAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, (H.AllPaidAmount) As CashOutAmount  " & _
                '            " from tbl_PurchaseHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.PurchaseDate BETWEEN @FromDate And @ToDate AND H.IsChange=0 " & _
                '            " and H.IsGem = '0' And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'ကျောက်အဝယ်(Cash Out)' as Title, 'Out' As Type, H.PurchaseHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),PurchaseDate,105) as Date, C.CustomerName As Customer, (H.AllPaidAmount) As PaidAmount, " & _
                '            "  '0' As DiscountAmount,  H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, " & _
                '            " (H.AllPaidAmount) As CashOutAmount  from tbl_PurchaseHeader H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND  H.PurchaseDate BETWEEN @FromDate And @ToDate And H.IsGem ='1' And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'ပထမအော်ဒါစရံ(Cash In)' as Title, 'In' As Type, H.OrderInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),OrderDate,105) as Date, C.CustomerName As Customer, (H.AdvanceAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.AdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_OrderInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.OrderDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'ဒုတိယအော်ဒါစရံ(Cash In)' as Title, 'In' As Type, H.OrderInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),SecondAdvanceDate,105) as Date, C.CustomerName As Customer, (H.SecondAdvanceAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.SecondAdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_OrderInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.SecondAdvanceDate=@ForDate And SecondAdvanceAmount <> 0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'အော်ဒါရွေး (Cash In)' as Title, 'In' As Type, " & _
                '            " R.OrderInvoiceID As VoucherNo, convert(varchar(10),ReturnDate,105) as Date, C.CustomerName As Customer, " & _
                '            " (H.PaidAmount) As PaidAmount, H.DiscountAmount As DiscountAmount,  H.LocationID, B.Location,H.Remark, " & _
                '            " (H.PaidAmount) AS CashInAmount, 0 As CashOutAmount  from tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice R " & _
                '            " ON R.OrderInvoiceID=H.OrderInvoiceID  LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & _
                '            " and H.PaidAmount >= 0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'အော်ဒါရွေးအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_OrderReturnHeader S LEFT JOIN tbl_OrderInvoice R ON R.OrderInvoiceID=S.OrderInvoiceID" & _
                '            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.OrderInvoiceID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  where H.IsDelete=0 AND S.IsDelete=0 AND H.PayDate=@ForDate" & _
                '            " AND Type='OrderInvoice' " & CriStr & _
                '            " UNION ALL  " & _
                '            " SELECT N'အော်ဒါရွေးမှအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, H.OrderInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),ReturnDate,105) as Date, C.CustomerName As Customer, (0-R.PaidAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  R.LocationID, B.Location,R.Remark, 0 AS CashInAmount, (0-R.PaidAmount) As CashOutAmount  " & _
                '            " from tbl_OrderReturnHeader R LEFT JOIN tbl_OrderInvoice H ON H.OrderInvoiceID=R.OrderInvoiceID " & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID   " & _
                '            " where R.IsDelete=0 AND R.ReturnDate BETWEEN @FromDate And @ToDate " & _
                '            " AND R.PaidAmount < 0 And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'ပြင်ထည်စရံ(Cash In)' as Title, 'In' As Type, H.RepairID As VoucherNo, convert(varchar(10)," & _
                '            " RepairDate,105) as Date, C.CustomerName As Customer, (H.AdvanceRepairAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                '            " H.LocationID, B.Location,H.Remark, (H.AdvanceRepairAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_RepairHeader H  LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.RepairDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT N'ပြန်ထည်ရွေး(Cash In)' as Title, 'In' As Type, R.RepairID As VoucherNo, " & _
                '            " convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (H.ReturnPaidAmount) As PaidAmount, " & _
                '            " ReturnDiscountAmount As DiscountAmount,  R.LocationID, B.Location,H.Remark, (H.ReturnPaidAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID  " & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID  Left Join tbl_Location B ON B.LocationID=R.LocationID  " & _
                '            " where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & _
                '            " and H.ReturnPaidAmount>=0  And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " SELECT 'ပြန်ထည်ရွေးမှ အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, R.RepairID As VoucherNo, " & _
                '            " convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (0-H.ReturnPaidAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  R.LocationID, B.Location,R.Remark, 0 AS CashInAmount, (0-H.ReturnPaidAmount) As CashOutAmount  " & _
                '            " from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID Left Join tbl_Location B ON B.LocationID=R.LocationID   " & _
                '            " where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate " & _
                '            " AND H.ReturnPaidAmount<0 And H.LocationID= '" & LocationID & "'" & _
                '             " UNION ALL " & _
                '            " SELECT N'ပြင်ထည်ရွေးမှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo, " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_ReturnRepairHeader S LEFT JOIN tbl_RepairHeader R ON R.RepairID=S.RepairID " & _
                '            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.RepairID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where S.IsDelete=0 AND H.PayDate=@ForDate AND Type='RepairReturn'" & CriStr & _
                '            " UNION ALL" & _
                '            " SELECT N'အခြားဝင်ငွေ(Cash In)' as Title, 'In' As Type, '-' As VoucherNo, " & _
                '            " convert(varchar(10),H.IncomeDate,105) as Date, H.Remark As Customer, (H.TotalAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location, H.Remark, (H.TotalAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_DailyIncome H  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.IsDelete=0 AND H.IncomeDate Between @FromDate And @ToDate  And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'အသုံးစရိတ်(Cash Out)' as Title, 'Out' As Type, '-' As VoucherNo, " & _
                '            " convert(varchar(10),H.ExpenseDate,105) as Date, H.Remark As Customer, (H.TotalAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                '            " H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, (H.TotalAmount) As CashOutAmount  from tbl_DailyExpense H  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.ExpenseDate Between @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'WholesaleReturn(Cash Out)' as Title, 'Out' As Type, WholesaleReturnID As VoucherNo, " & _
                '            " convert(varchar(10),H.WReturnDate,105) as Date, H.Remark As Customer, (H.PaidAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                '            " H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, (H.PaidAmount) As CashOutAmount  from tbl_WholesaleReturn H  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0  AND SaleReturnAmount> 0  AND H.WReturnDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                '            " UNION ALL" & _
                '            " Select N'အပ်ငွေ(Cash Out)' as Title,'Out' As Type, '-' As VoucherNo, convert(varchar(10),GLDate,105) as Date, " & _
                '            " '-' As Customer, sum(H.CreditAmount) As PaidAmount,0 As DiscountAmount,  H.LocationID, B.Location,'' AS  Remark, " & _
                '            " 0 as CashInAmount,sum(H.CreditAmount) AS CashOutAmount from tbl_GeneralLedgerByLocation H " & _
                '            " LEFT JOIN tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where GLDate=@ForDate AND Title=N'အပ်ငွေ' And H.LocationID= '" & LocationID & "'" & _
                '            " Group By GLDate,  H.LocationID, B.Location) as M "

                strCommandText = " SELECT * FROM  (Select N'မတည်ငွေ(Cash In)' as Title,'In' As Type, '-' As VoucherNo," & _
                            " convert(varchar(10),GLDate,105) as Date,  '-' As Customer, sum(H.DebitAmount) As PaidAmount,0 As DiscountAmount," & _
                            " H.LocationID, B.Location,'' AS  Remark, sum(H.DebitAmount) AS CashInAmount, 0 As CashOutAmount" & _
                            " from tbl_GeneralLedgerByLocation H  LEFT JOIN tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where GLDate Between  @FromDate And @ToDate AND Title=N'မတည်ငွေ' And H.LocationID= '" & LocationID & "'" & _
                            " Group By GLDate, H.LocationID, B.Location" & _
                            " UNION ALL" & _
                            " Select N'အရောင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.DiscountAmount) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 and H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' AND PaidAmount>=0 AND IsAdvance=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'WholeSale အရောင်း(Cash In)' as Title, 'In' As Type, WholeSaleInvoiceID As VoucherNo," & _
                            " convert(varchar(10),WDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.Discount) As DiscountAmount, H.LocationID, B.Location ,'' as Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_WholeSaleInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND  PaidAmount>0  And (PayType='0' OR PayType='2' OR PayType='3') and H.WDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'ConsignmentSale အရောင်း(Cash In)' as Title, 'In' As Type, ConsignmentSaleID As VoucherNo," & _
                            " convert(varchar(10),ConsignDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.Discount) As DiscountAmount, H.LocationID, B.Location ,'' as Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_ConsignmentSale H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.ConsignDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'Return Advance' as Title, 'In' As Type, ReturnAdvanceID As VoucherNo," & _
                            "convert(varchar(10),ReturnAdvanceDate,105) as Date, C.CustomerName As Customer, H.NetAmount  As PaidAmount," & _
                            " (H.Discount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.NetAmount) AS CashInAmount," & _
                            "  0 As CashOutAmount  from tbl_ReturnAdvance H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID   " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 and H.ReturnAdvanceDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'လဲခြင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.SaleDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " AND PurchaseHeaderID<>'' AND PaidAmount>=0 AND IsAdvance=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'လဲခြင်းမှအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 AND IsOtherCash=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                             " Select  N'အပေါင်ရွေးခြင်းမှရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                            "convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                            "(H.AddOrSub) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount, " & _
                            "0 As CashOutAmount  from tbl_MortgageReturn H LEFT JOIN tbl_MortgageInvoice M ON H.MortgageInvoiceID=M.MortgageInvoiceID LEFT JOIN tbl_Customer C ON C.CustomerID=M.CustomerID " & _
                            "Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and H.ReturnDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                             " Select  N'အပေါင်ရွေးခြင်းမှရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                            "convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                            "(H.AddOrSub) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount, " & _
                            "0 As CashOutAmount  from tbl_MortgageInvoice H  LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                            "Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 And H.IsReturn=1 and H.ReturnDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                            " Select N'အပေါင်အတိုးသတ်ခြင်းမှရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                            " convert(varchar(10),InterestPaidDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                            " (H.InterestAmount-H.PaidAmount) As DiscountAmount, H.LocationID, B.Location ,S.Remark, (H.PaidAmount) AS CashInAmount, " & _
                            " 0 As CashOutAmount  from tbl_MortgageInterest H  Left Join tbl_MortgageInvoice S " & _
                            " On H.MortgageInvoiceID = S.MortgageInvoiceID LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and H.InterestPaidDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                            " Select  N'အပေါင်ပစ္စည်းအရင်းဆပ်ခြင်းမှ ရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                            " convert(varchar(10),PaybackDate,105) as Date, C.CustomerName As Customer, (H.PaybackAmt) As PaidAmount, " & _
                            " 0 As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaybackAmt) AS CashInAmount, " & _
                            " 0 As CashOutAmount  from tbl_MortgageInvoice H " & _
                            " Left Join tbl_MortgagePayback P On H.MortgageInvoiceID=P.MortgageInvoiceID " & _
                            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and P.PaybackDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'အပေါင်လက်ခံခြင်းမှချေးငွေ(Cash Out)' as Title, 'Out' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                           " convert(varchar(10),ReceiveDate,105) as Date, C.CustomerName As Customer, " & _
                           " (H.TotalAmount) As PaidAmount, 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                           " (H.TotalAmount) As CashOutAmount from tbl_MortgageInvoice H LEFT JOIN tbl_Customer C  " & _
                           " ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                           " where H.ReceiveDate BETWEEN  @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                            " SELECT N'OtherCashအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.SaleDate BETWEEN @FromDate And @ToDate AND IsOtherCash=1 AND PaidAmount<0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'OtherCash(LooseDiamond)အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleLooseDiamondID As VoucherNo, " & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleLooseDiamondHeader H LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.SaleDate BETWEEN @FromDate And @ToDate AND IsOtherCash=1 AND PaidAmount<0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'စရံအရောင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                            " convert(varchar(10),SaleDate,105) as Date, " & _
                            " C.CustomerName As Customer, (H.AllAdvanceAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount,  " & _
                            " H.LocationID, B.Location,H.Remark, (H.AllAdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.EntryAdvanceDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'စရံအရောင်းပြန်ရွေး(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                            " convert(varchar(10),SaleDate,105) as Date, " & _
                            " C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount,  " & _
                            " H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.SaleDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND PaidAmount>0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'စရံအရောင်းအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, " & _
                            " (H.AllAdvanceAmount+H.PaidAmount+H.PurchaseAmount+H.OtherCashAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount," & _
                            " H.LocationID, B.Location,H.Remark, 0 AS CashInAmount," & _
                            " (H.AllAdvanceAmount+H.PaidAmount+H.PurchaseAmount+H.OtherCashAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H " & _
                            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.CancelDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND IsCancel=1 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'အရောင်းမှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                            " from tbl_CashReceipt H Left Join tbl_SaleInvoiceHeader S On H.VoucherNo = S.SaleInvoiceHeaderID LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.PayDate Between  @FromDate And @ToDate AND Type='SalesInvoice' AnD H.PayAmount>0 AND H.IsDelete=0 and S.IsDelete=0 " & CriStr & _
                            " UNION ALL" & _
                            " SELECT N'WholeSaleမှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                            " from tbl_CashReceipt H Left Join tbl_WholeSaleInvoice S On H.VoucherNo = S.WholeSaleInvoiceID LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.PayDate Between  @FromDate And @ToDate AND Type='WholeSalesInvoice' AnD H.PayAmount>0 AND H.IsDelete=0 and S.IsDelete=0 " & CriStr & _
                            " UNION ALL" & _
                            " SELECT N'အရောင်းမှအကြွေးပေးငွေ(Cash Out)' as Title, 'Out' As Type, VoucherNo As VoucherNo," & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (0-H.PayAmount) As PaidAmount," & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, (0-H.PayAmount) As CashOutAmount" & _
                            " from tbl_CashReceipt H Left Join tbl_SaleInvoiceHeader S On H.VoucherNo = S.SaleInvoiceHeaderID LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.PayDate Between  @FromDate And @ToDate AND Type='SalesInvoice' AnD H.PayAmount<0 AND H.IsDelete=0 " & CriStr & _
                            " UNION ALL" & _
                             " Select N'အရောင်းVolume (Cash In)' as Title, 'In' As Type, SalesVolumeID As VoucherNo," & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.DiscountAmount) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate AND PaidAmount>=0 AND PurchaseHeaderID='' And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'လဲခြင်းVolume (Cash In)' as Title, 'In' As Type, SalesVolumeID As VoucherNo," & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & _
                            " AND PurchaseHeaderID<>'' AND PaidAmount>=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'လဲခြင်းVolume အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SalesVolumeID As VoucherNo, " & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                            " (0-H.PaidAmount) As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>''  " & _
                            " AND PaidAmount<0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'အရောင်း(Volume)မှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                            " from tbl_CashReceipt H Left Join tbl_SalesVolume S On H.VoucherNo = S.SalesVolumeID LEFT JOIN tbl_Customer C" & _
                            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.PayDate Between  @FromDate And @ToDate AND Type='SalesInvoiceVolume'" & CriStr & _
                            " union all" & _
                            " Select N'အရောင်းLooseDiamond(Cash In)' as Title, 'In' As Type, SaleLooseDiamondID As VoucherNo," & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.DiscountAmount) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_SaleLooseDiamondHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 and H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' AND PaidAmount>=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                             " SELECT N'လဲခြင်းLooseDiamond(Cash In)' as Title, 'In' As Type, SaleLooseDiamondID As VoucherNo," & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                            " 0 As CashOutAmount  from tbl_SaleLooseDiamondHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.SaleDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " AND PurchaseHeaderID<>'' AND PaidAmount>=0 AND H.IsDelete=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL " & _
                             " SELECT N'လဲခြင်းLooseDiamond အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleLooseDiamondID As VoucherNo, " & _
                            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleLooseDiamondHeader H LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>''  " & _
                            " AND PaidAmount<0 And IsOtherCash=0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'အရောင်း(LooseDiamond)မှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                            " from tbl_CashReceipt H Left Join tbl_SaleLooseDiamondHeader S On H.VoucherNo = S.SaleLooseDiamondID LEFT JOIN tbl_Customer C" & _
                            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.PayDate Between  @FromDate And @ToDate AND Type='SaleLooseDiamond'" & CriStr & _
                            " union all" & _
                             " SELECT N'စိန်အကြွေအဝယ်(Cash Out)' as Title, 'Out' As Type, H.PurchaseHeaderID As VoucherNo, " & _
                            " convert(varchar(10),PurchaseDate,105) as Date, C.CustomerName As Customer, (H.AllPaidAmount) As PaidAmount, " & _
                            "  '0' As DiscountAmount,  H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, " & _
                            " (H.AllPaidAmount) As CashOutAmount  from tbl_PurchaseHeader H LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND  H.PurchaseDate BETWEEN @FromDate And @ToDate And H.IsLooseDiamond ='1' And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'ကျောက်အရောင်း(Cash In)' as Title, 'In' As Type, SaleGemsID As VoucherNo, " & _
                            " convert(varchar(10),SDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount, " & _
                            " 0 As CashOutAmount  from tbl_SaleGems H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.SDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'ကျောက်ရောင်းမှ အကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_CashReceipt H Left Join tbl_SaleGems S On H.VoucherNo = S.SaleGemsID" & _
                            " LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.PayDate Between  @FromDate And @ToDate AND Type='SalesGems' " & CriStr & _
                            " UNION ALL" & _
                            " SELECT N'အဝယ်(Cash Out)' as Title, 'Out' As Type, H.PurchaseHeaderID As VoucherNo, " & _
                            " convert(varchar(10),PurchaseDate,105) as Date, C.CustomerName As Customer, (H.AllPaidAmount) As PaidAmount, " & _
                            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, (H.AllPaidAmount) As CashOutAmount  " & _
                            " from tbl_PurchaseHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.PurchaseDate BETWEEN @FromDate And @ToDate AND H.IsChange=0 " & _
                            " and H.IsGem = '0' And H.IsLooseDiamond='0' And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'ကျောက်အဝယ်(Cash Out)' as Title, 'Out' As Type, H.PurchaseHeaderID As VoucherNo, " & _
                            " convert(varchar(10),PurchaseDate,105) as Date, C.CustomerName As Customer, (H.AllPaidAmount) As PaidAmount, " & _
                            "  '0' As DiscountAmount,  H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, " & _
                            " (H.AllPaidAmount) As CashOutAmount  from tbl_PurchaseHeader H LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND  H.PurchaseDate BETWEEN @FromDate And @ToDate And H.IsGem ='1' And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'ပထမအော်ဒါစရံ(Cash In)' as Title, 'In' As Type, H.OrderInvoiceID As VoucherNo, " & _
                            " convert(varchar(10),OrderDate,105) as Date, C.CustomerName As Customer, (H.AdvanceAmount) As PaidAmount, " & _
                            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.AdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_OrderInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.OrderDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'ဒုတိယအော်ဒါစရံ(Cash In)' as Title, 'In' As Type, H.OrderInvoiceID As VoucherNo, " & _
                            " convert(varchar(10),SecondAdvanceDate,105) as Date, C.CustomerName As Customer, (H.SecondAdvanceAmount) As PaidAmount, " & _
                            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.SecondAdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_OrderInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.SecondAdvanceDate Between  @FromDate And @ToDate And SecondAdvanceAmount <> 0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'အော်ဒါရွေး (Cash In)' as Title, 'In' As Type, " & _
                            " R.OrderInvoiceID As VoucherNo, convert(varchar(10),ReturnDate,105) as Date, C.CustomerName As Customer, " & _
                            " (H.PaidAmount) As PaidAmount, H.DiscountAmount As DiscountAmount,  H.LocationID, B.Location,H.Remark, " & _
                            " (H.PaidAmount) AS CashInAmount, 0 As CashOutAmount  from tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice R " & _
                            " ON R.OrderInvoiceID=H.OrderInvoiceID  LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & _
                            " and H.PaidAmount >= 0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'အော်ဒါရွေးအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_OrderReturnHeader S LEFT JOIN tbl_OrderInvoice R ON R.OrderInvoiceID=S.OrderInvoiceID" & _
                            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.OrderInvoiceID  LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=R.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  where H.IsDelete=0 AND S.IsDelete=0 AND H.PayDate  Between @FromDate And @ToDate" & _
                            " AND Type='OrderInvoice' " & CriStr & _
                            " UNION ALL  " & _
                            " SELECT N'အော်ဒါရွေးမှအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, H.OrderInvoiceID As VoucherNo, " & _
                            " convert(varchar(10),ReturnDate,105) as Date, C.CustomerName As Customer, (0-R.PaidAmount) As PaidAmount, " & _
                            " 0 As DiscountAmount,  R.LocationID, B.Location,R.Remark, 0 AS CashInAmount, (0-R.PaidAmount) As CashOutAmount  " & _
                            " from tbl_OrderReturnHeader R LEFT JOIN tbl_OrderInvoice H ON H.OrderInvoiceID=R.OrderInvoiceID " & _
                            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID   " & _
                            " where R.IsDelete=0 AND R.ReturnDate BETWEEN @FromDate And @ToDate " & _
                            " AND R.PaidAmount < 0 And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'ပြင်ထည်စရံ(Cash In)' as Title, 'In' As Type, H.RepairID As VoucherNo, convert(varchar(10)," & _
                            " RepairDate,105) as Date, C.CustomerName As Customer, (H.AdvanceRepairAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                            " H.LocationID, B.Location,H.Remark, (H.AdvanceRepairAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_RepairHeader H  LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.RepairDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT N'ပြန်ထည်ရွေး(Cash In)' as Title, 'In' As Type, R.RepairID As VoucherNo, " & _
                            " convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (H.ReturnPaidAmount) As PaidAmount, " & _
                            " ReturnDiscountAmount As DiscountAmount,  R.LocationID, B.Location,H.Remark, (H.ReturnPaidAmount) AS CashInAmount, " & _
                            " 0 As CashOutAmount  from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID  " & _
                            " LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID  Left Join tbl_Location B ON B.LocationID=R.LocationID  " & _
                            " where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & _
                            " and H.ReturnPaidAmount>=0  And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " SELECT 'ပြန်ထည်ရွေးမှ အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, R.RepairID As VoucherNo, " & _
                            " convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (0-H.ReturnPaidAmount) As PaidAmount, " & _
                            " 0 As DiscountAmount,  R.LocationID, B.Location,R.Remark, 0 AS CashInAmount, (0-H.ReturnPaidAmount) As CashOutAmount  " & _
                            " from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID  LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=R.CustomerID Left Join tbl_Location B ON B.LocationID=R.LocationID   " & _
                            " where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate " & _
                            " AND H.ReturnPaidAmount<0 And H.LocationID= '" & LocationID & "'" & _
                             " UNION ALL " & _
                            " SELECT N'ပြင်ထည်ရွေးမှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo, " & _
                            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                            " from tbl_ReturnRepairHeader S LEFT JOIN tbl_RepairHeader R ON R.RepairID=S.RepairID " & _
                            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.RepairID  LEFT JOIN tbl_Customer C " & _
                            " ON C.CustomerID=R.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where S.IsDelete=0 AND H.PayDate Between  @FromDate And @ToDate AND Type='RepairReturn'" & CriStr & _
                            " UNION ALL" & _
                            " SELECT N'အခြားဝင်ငွေ(Cash In)' as Title, 'In' As Type, '-' As VoucherNo, " & _
                            " convert(varchar(10),H.IncomeDate,105) as Date, H.Remark As Customer, (H.TotalAmount) As PaidAmount, " & _
                            " 0 As DiscountAmount,  H.LocationID, B.Location, H.Remark, (H.TotalAmount) AS CashInAmount, " & _
                            " 0 As CashOutAmount  from tbl_DailyIncome H  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where H.IsDelete=0 AND H.IncomeDate Between @FromDate And @ToDate  And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'အသုံးစရိတ်(Cash Out)' as Title, 'Out' As Type, '-' As VoucherNo, " & _
                            " convert(varchar(10),H.ExpenseDate,105) as Date, H.Remark As Customer, (H.TotalAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                            " H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, (H.TotalAmount) As CashOutAmount  from tbl_DailyExpense H  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0 AND H.ExpenseDate Between @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL  " & _
                            " SELECT N'WholesaleReturn(Cash Out)' as Title, 'Out' As Type, WholesaleReturnID As VoucherNo, " & _
                            " convert(varchar(10),H.WReturnDate,105) as Date, H.Remark As Customer, (H.PaidAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                            " H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, (H.PaidAmount) As CashOutAmount  from tbl_WholesaleReturn H  " & _
                            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                            " where H.IsDelete=0  AND SaleReturnAmount> 0  AND H.WReturnDate BETWEEN @FromDate And @ToDate And H.LocationID= '" & LocationID & "'" & _
                            " UNION ALL" & _
                            " Select N'အပ်ငွေ(Cash Out)' as Title,'Out' As Type, '-' As VoucherNo, convert(varchar(10),GLDate,105) as Date, " & _
                            " '-' As Customer, sum(H.CreditAmount) As PaidAmount,0 As DiscountAmount,  H.LocationID, B.Location,'' AS  Remark, " & _
                            " 0 as CashInAmount,sum(H.CreditAmount) AS CashOutAmount from tbl_GeneralLedgerByLocation H " & _
                            " LEFT JOIN tbl_Location B ON B.LocationID=H.LocationID  " & _
                            " where GLDate Between  @FromDate And @ToDate AND Title=N'အပ်ငွေ' And H.LocationID= '" & LocationID & "'" & _
                            " Group By GLDate,  H.LocationID, B.Location) as M order by VoucherNo asc"




                'If IsBank = False Then

                'Else
                '    strCommandText = " SELECT * FROM  (Select N'မတည်ငွေ(Cash In)' as Title,'In' As Type, '-' As VoucherNo," & _
                '            " convert(varchar(10),GLDate,105) as Date,  '-' As Customer, sum(H.DebitAmount) As PaidAmount,0 As DiscountAmount," & _
                '            " H.LocationID, B.Location,'' AS  Remark, sum(H.DebitAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_GeneralLedgerByLocation H  LEFT JOIN tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where GLDate=@ForDate AND Title=N'မတည်ငွေ' " & _
                '            " Group By GLDate, H.LocationID, B.Location" & _
                '            " UNION ALL " & _
                '            " Select N'အရောင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID='' AND PaidAmount>=0 AND IsAdvance=0 AND H.IsDelete=0 " & _
                '            " UNION ALL " & _
                '            " Select N'WholeSale အရောင်း(Cash In)' as Title, 'In' As Type, WholeSaleInvoiceID As VoucherNo," & _
                '            " convert(varchar(10),WDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.Discount) As DiscountAmount, H.LocationID, B.Location ,'' as Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_WholeSaleInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND  PaidAmount>0  And (PayType='0' OR PayType='2' OR PayType='3') and H.WDate BETWEEN @FromDate And @ToDate " & _
                '            " UNION ALL " & _
                '            " Select N'ConsignmentSale အရောင်း(Cash In)' as Title, 'In' As Type, ConsignmentSaleID As VoucherNo," & _
                '            " convert(varchar(10),ConsignDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.Discount) As DiscountAmount, H.LocationID, B.Location ,'' as Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_ConsignmentSale H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.ConsignDate BETWEEN @FromDate And @ToDate " & _
                '            " UNION ALL" & _
                '            " SELECT N'Return Advance' as Title, 'In' As Type, ReturnAdvanceID As VoucherNo," & _
                '            "convert(varchar(10),ReturnAdvanceDate,105) as Date, C.CustomerName As Customer, H.NetAmount  As PaidAmount," & _
                '            " (H.Discount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.NetAmount) AS CashInAmount," & _
                '            "  0 As CashOutAmount  from tbl_ReturnAdvance H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID   " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 and H.ReturnAdvanceDate BETWEEN @FromDate And @ToDate" & _
                '            " UNION ALL" & _
                '            " SELECT N'လဲခြင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.SaleDate BETWEEN @FromDate And @ToDate" & _
                '            " AND PurchaseHeaderID<>'' AND PaidAmount>=0 AND IsAdvance=0 AND H.IsDelete=0 " & _
                '            " UNION ALL" & _
                '            " SELECT N'လဲခြင်းမှအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>'' AND PaidAmount<0 AND IsOtherCash=0 AND H.IsDelete=0 " & _
                '            " UNION ALL  " & _
                '            " Select  N'အပေါင်ရွေးခြင်းမှရငွေ(Cash In)' as Title, 'In' As Type, MortgageInvoiceID As VoucherNo, " & _
                '            "convert(varchar(10),ReturnDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                '            "(H.AddOrSub) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount, " & _
                '            "0 As CashOutAmount  from tbl_MortgageInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                '            "Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and H.ReturnDate BETWEEN  @FromDate And @ToDate " & _
                '            " UNION ALL " & _
                '            " Select N'အပေါင်အတိုးသတ်ခြင်းမှရငွေ(Cash In)' as Title, 'In' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),InterestPaidDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                '            " (H.InterestAmount-H.PaidAmount) As DiscountAmount, H.LocationID, B.Location ,S.Remark, (H.PaidAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_MortgageInterest H  Left Join tbl_MortgageInvoice S " & _
                '            " On H.MortgageInvoiceID = S.MortgageInvoiceID LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 and H.InterestPaidDate BETWEEN  @FromDate And @ToDate " & _
                '            " UNION ALL " & _
                '            " SELECT N'အပေါင်လက်ခံခြင်းမှချေးငွေ(Cash Out)' as Title, 'Out' As Type, H.MortgageInvoiceID As VoucherNo, " & _
                '           " convert(varchar(10),ReceiveDate,105) as Date, C.CustomerName As Customer, " & _
                '           " (H.PaidAmount) As PaidAmount, 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '           " (H.PaidAmount) As CashOutAmount from tbl_MortgageInvoice H LEFT JOIN tbl_Customer C  " & _
                '           " ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '           " where H.ReceiveDate BETWEEN  @FromDate And @ToDate " & _
                '            " UNION ALL " & _
                '            " SELECT N'OtherCashအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '            " (0-H.PaidAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.SaleDate BETWEEN @FromDate And @ToDate AND IsOtherCash=1 AND PaidAmount<0 AND H.IsDelete=0 " & _
                '            " UNION ALL  " & _
                '            " SELECT N'စရံအရောင်း(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, " & _
                '            " C.CustomerName As Customer, (H.AllAdvanceAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount,  " & _
                '            " H.LocationID, B.Location,H.Remark, (H.AllAdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.EntryAdvanceDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND H.IsDelete=0 " & _
                '            " UNION ALL" & _
                '            " SELECT N'စရံအရောင်းပြန်ရွေး(Cash In)' as Title, 'In' As Type, SaleInvoiceHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, " & _
                '            " C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount,  " & _
                '            " H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_SaleInvoiceHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.SaleDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND PaidAmount>0 AND H.IsDelete=0 " & _
                '            " UNION ALL" & _
                '            " SELECT N'စရံအရောင်းအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SaleInvoiceHeaderID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, " & _
                '            " (H.AllAdvanceAmount+H.PaidAmount+H.PurchaseAmount+H.OtherCashAmount) As PaidAmount, (H.DiscountAmount) As DiscountAmount," & _
                '            " H.LocationID, B.Location,H.Remark, 0 AS CashInAmount," & _
                '            " (H.AllAdvanceAmount+H.PaidAmount+H.PurchaseAmount+H.OtherCashAmount) As CashOutAmount  from tbl_SaleInvoiceHeader H " & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.CancelDate BETWEEN @FromDate And @ToDate AND H.IsAdvance=1 AND IsCancel=1 AND H.IsDelete=0" & _
                '            " UNION ALL" & _
                '            " SELECT N'အရောင်းမှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleInvoiceHeader S On H.VoucherNo = S.SaleInvoiceHeaderID LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.PayDate=@ForDate AND Type='SalesInvoice' AnD H.PayAmount>0 AND H.IsDelete=0 and H.IsBank=0 " & _
                '             " UNION ALL" & _
                '             " SELECT N'အရောင်းမှအကြွေးရငွေ(Bank)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleInvoiceHeader S On H.VoucherNo = S.SaleInvoiceHeaderID LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.PayDate=@ForDate AND Type='SalesInvoice' AnD H.PayAmount>0 AND H.IsDelete=0 and H.IsBank=1 And S.IsDelete=0 And C.IsDelete=0 And B.IsDelete=0 " & _
                '            " UNION ALL" & _
                '            " SELECT N'အရောင်းမှအကြွေးပေးငွေ(Cash Out)' as Title, 'Out' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (0-H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, (0-H.PayAmount) As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleInvoiceHeader S On H.VoucherNo = S.SaleInvoiceHeaderID LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.PayDate=@ForDate AND Type='SalesInvoice' AnD H.PayAmount<0 AND H.IsDelete=0 and S.IsDelete=0 and H.IsBank=1" & _
                '            " UNION ALL" & _
                '             " Select N'အရောင်းVolume (Cash In)' as Title, 'In' As Type, SalesVolumeID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount, H.LocationID, B.Location ,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate AND PaidAmount>=0 AND PurchaseHeaderID='' " & _
                '            " UNION ALL" & _
                '            " SELECT N'လဲခြင်းVolume (Cash In)' as Title, 'In' As Type, SalesVolumeID As VoucherNo," & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount," & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount," & _
                '            " 0 As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID" & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & _
                '            " AND PurchaseHeaderID<>'' AND PaidAmount>=0 " & _
                '            " UNION ALL" & _
                '            " SELECT N'လဲခြင်းVolume အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, SalesVolumeID As VoucherNo, " & _
                '            " convert(varchar(10),SaleDate,105) as Date, C.CustomerName As Customer, (0-H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, " & _
                '            " (0-H.PaidAmount) As CashOutAmount  from tbl_SalesVolume H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate AND PurchaseHeaderID<>''  " & _
                '            " AND PaidAmount<0 " & _
                '            " UNION ALL" & _
                '            " Select N'အရောင်း(Volume)မှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SalesVolume S On H.VoucherNo = S.SalesVolumeID LEFT JOIN tbl_Customer C" & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND S.IsDelete=0 AND H.PayDate=@ForDate AND Type='SalesInvoiceVolume' AND IsBank=0 " & _
                '            " union all" & _
                '            " Select N'အရောင်း(Volume)မှအကြွေးရငွေ(Bank)' as Title, 'In' As Type, VoucherNo As VoucherNo," & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount," & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount" & _
                '            " from tbl_CashReceipt H Left Join tbl_SalesVolume S On H.VoucherNo = S.SalesVolumeID LEFT JOIN tbl_Customer C" & _
                '            " ON C.CustomerID=S.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND S.IsDelete=0 AND H.PayDate=@ForDate AND Type='SalesInvoiceVolume' AND IsBank=1 " & _
                '            " union all" & _
                '            " SELECT N'ကျောက်အရောင်း(Cash In)' as Title, 'In' As Type, SaleGemsID As VoucherNo, " & _
                '            " convert(varchar(10),SDate,105) as Date, C.CustomerName As Customer, (H.PaidAmount) As PaidAmount, " & _
                '            " (H.DiscountAmount) As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.PaidAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_SaleGems H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.SDate BETWEEN @FromDate And @ToDate" & _
                '            " UNION ALL  " & _
                '            " SELECT N'ကျောက်ရောင်းမှ အကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleGems S On H.VoucherNo = S.SaleGemsID" & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND S.IsDelete=0 AND H.PayDate=@ForDate AND Type='SalesGems' AND IsBank=0 " & _
                '             " UNION ALL  " & _
                '            " SELECT N'ကျောက်ရောင်းမှ အကြွေးရငွေ(Bank)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_CashReceipt H Left Join tbl_SaleGems S On H.VoucherNo = S.SaleGemsID" & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 And S.IsDelete=0 AND H.PayDate=@ForDate AND Type='SalesGems' AND IsBank=1 " & _
                '            " UNION ALL" & _
                '            " SELECT N'အဝယ်(Cash Out)' as Title, 'Out' As Type, H.PurchaseHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),PurchaseDate,105) as Date, C.CustomerName As Customer, (H.AllPaidAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, 0 AS CashInAmount, (H.AllPaidAmount) As CashOutAmount  " & _
                '            " from tbl_PurchaseHeader H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.PurchaseDate BETWEEN @FromDate And @ToDate AND H.IsChange=0 " & _
                '            " and H.IsGem = '0'" & _
                '            " UNION ALL  " & _
                '            " SELECT N'ကျောက်အဝယ်(Cash Out)' as Title, 'Out' As Type, H.PurchaseHeaderID As VoucherNo, " & _
                '            " convert(varchar(10),PurchaseDate,105) as Date, C.CustomerName As Customer, (H.AllPaidAmount) As PaidAmount, " & _
                '            "  '0' As DiscountAmount,  H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, " & _
                '            " (H.AllPaidAmount) As CashOutAmount  from tbl_PurchaseHeader H LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND  H.PurchaseDate BETWEEN @FromDate And @ToDate And H.IsGem ='1' " & _
                '            " UNION ALL" & _
                '            " SELECT N'ပထမအော်ဒါစရံ(Cash In)' as Title, 'In' As Type, H.OrderInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),OrderDate,105) as Date, C.CustomerName As Customer, (H.AdvanceAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.AdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_OrderInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.OrderDate BETWEEN @FromDate And @ToDate " & _
                '            " UNION ALL" & _
                '            " SELECT N'ဒုတိယအော်ဒါစရံ(Cash In)' as Title, 'In' As Type, H.OrderInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),SecondAdvanceDate,105) as Date, C.CustomerName As Customer, (H.SecondAdvanceAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location,H.Remark, (H.SecondAdvanceAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_OrderInvoice H LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.SecondAdvanceDate=@ForDate And SecondAdvanceAmount <> 0" & _
                '            " UNION ALL" & _
                '            " SELECT N'အော်ဒါရွေး (Cash In)' as Title, 'In' As Type, " & _
                '            " R.OrderInvoiceID As VoucherNo, convert(varchar(10),ReturnDate,105) as Date, C.CustomerName As Customer, " & _
                '            " (H.PaidAmount) As PaidAmount, H.DiscountAmount As DiscountAmount,  H.LocationID, B.Location,H.Remark, " & _
                '            " (H.PaidAmount) AS CashInAmount, 0 As CashOutAmount  from tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice R " & _
                '            " ON R.OrderInvoiceID=H.OrderInvoiceID  LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND C.IsDelete=0 AND R.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & _
                '            " and H.PaidAmount >= 0" & _
                '            " UNION ALL  " & _
                '            " SELECT N'အော်ဒါရွေးအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_OrderReturnHeader S LEFT JOIN tbl_OrderInvoice R ON R.OrderInvoiceID=S.OrderInvoiceID" & _
                '            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.OrderInvoiceID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  where H.IsDelete=0 AND H.PayDate=@ForDate" & _
                '            " AND Type='OrderInvoice' AND IsBank=0 " & _
                '            " UNION ALL  " & _
                '            " SELECT N'အော်ဒါရွေးအကြွေးရငွေ(Bank)' as Title, 'In' As Type, VoucherNo As VoucherNo,  " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_OrderReturnHeader S LEFT JOIN tbl_OrderInvoice R ON R.OrderInvoiceID=S.OrderInvoiceID" & _
                '            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.OrderInvoiceID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID  where S.IsDelete=0 AND R.IsDelete=0 AND H.IsDelete=0 AND H.PayDate=@ForDate" & _
                '            " AND Type='OrderInvoice' AND IsBank=1 " & _
                '            " UNION ALL  " & _
                '            " SELECT N'အော်ဒါရွေးမှအမ်းငွေ(Cash Out)' as Title, 'Out' As Type, H.OrderInvoiceID As VoucherNo, " & _
                '            " convert(varchar(10),ReturnDate,105) as Date, C.CustomerName As Customer, (0-R.PaidAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  R.LocationID, B.Location,R.Remark, 0 AS CashInAmount, (0-R.PaidAmount) As CashOutAmount  " & _
                '            " from tbl_OrderReturnHeader R LEFT JOIN tbl_OrderInvoice H ON H.OrderInvoiceID=R.OrderInvoiceID " & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID Left Join tbl_Location B ON B.LocationID=H.LocationID   " & _
                '            " where R.IsDelete=0 AND H.IsDelete=0 AND R.ReturnDate BETWEEN @FromDate And @ToDate " & _
                '            " AND R.PaidAmount < 0" & _
                '            " UNION ALL  " & _
                '            " SELECT N'ပြင်ထည်စရံ(Cash In)' as Title, 'In' As Type, H.RepairID As VoucherNo, convert(varchar(10)," & _
                '            " RepairDate,105) as Date, C.CustomerName As Customer, (H.AdvanceRepairAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                '            " H.LocationID, B.Location,H.Remark, (H.AdvanceRepairAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_RepairHeader H  LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID where H.IsDelete=0 AND H.RepairDate BETWEEN @FromDate And @ToDate" & _
                '            " UNION ALL" & _
                '            " SELECT N'ပြန်ထည်ရွေး(Cash In)' as Title, 'In' As Type, R.RepairID As VoucherNo, " & _
                '            " convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (H.ReturnPaidAmount) As PaidAmount, " & _
                '            " ReturnDiscountAmount As DiscountAmount,  R.LocationID, B.Location,H.Remark, (H.ReturnPaidAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID  " & _
                '            " LEFT JOIN tbl_Customer C ON C.CustomerID=R.CustomerID  Left Join tbl_Location B ON B.LocationID=R.LocationID  " & _
                '            " where H.IsDelete=0 AND R.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & _
                '            " and H.ReturnPaidAmount>=0  " & _
                '            " UNION ALL" & _
                '            " SELECT 'ပြန်ထည်ရွေးမှ အမ်းငွေ(Cash Out)' as Title, 'Out' As Type, R.RepairID As VoucherNo, " & _
                '            " convert(varchar(10),H.ReturnDate,105) as Date, C.CustomerName As Customer, (0-H.ReturnPaidAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  R.LocationID, B.Location,R.Remark, 0 AS CashInAmount, (0-H.ReturnPaidAmount) As CashOutAmount  " & _
                '            " from tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON R.RepairID=H.RepairID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID Left Join tbl_Location B ON B.LocationID=R.LocationID   " & _
                '            " where H.IsDelete=0 AND R.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate " & _
                '            " AND H.ReturnPaidAmount<0 " & _
                '             " UNION ALL " & _
                '            " SELECT N'ပြင်ထည်ရွေးမှအကြွေးရငွေ(Cash In)' as Title, 'In' As Type, VoucherNo As VoucherNo, " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_ReturnRepairHeader S LEFT JOIN tbl_RepairHeader R ON R.RepairID=S.RepairID " & _
                '            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.RepairID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where S.IsDelete=0 AND R.IsDelete=0 AND H.IsDelete=0 AND H.PayDate=@ForDate AND Type='RepairReturn' and IsBank=0  " & _
                '            " UNION ALL " & _
                '            " SELECT N'ပြင်ထည်ရွေးမှအကြွေးရငွေ(Bank)' as Title, 'In' As Type, VoucherNo As VoucherNo, " & _
                '            " convert(varchar(10),PayDate,105) as Date, C.CustomerName As Customer, (H.PayAmount) As PaidAmount,  " & _
                '            " 0 As DiscountAmount, H.LocationID, B.Location,H.Remark, (H.PayAmount) AS CashInAmount, 0 As CashOutAmount  " & _
                '            " from tbl_ReturnRepairHeader S LEFT JOIN tbl_RepairHeader R ON R.RepairID=S.RepairID " & _
                '            " LEFT JOIN tbl_CashReceipt H  On H.VoucherNo = R.RepairID  LEFT JOIN tbl_Customer C " & _
                '            " ON C.CustomerID=R.CustomerID  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where S.IsDelete=0 AND H.IsDelete=0 AND H.PayDate=@ForDate AND Type='RepairReturn' and IsBank=1  " & _
                '            " UNION ALL" & _
                '            " SELECT N'အခြားဝင်ငွေ(Cash In)' as Title, 'In' As Type, '-' As VoucherNo, " & _
                '            " convert(varchar(10),H.IncomeDate,105) as Date, H.Remark As Customer, (H.TotalAmount) As PaidAmount, " & _
                '            " 0 As DiscountAmount,  H.LocationID, B.Location, H.Remark, (H.TotalAmount) AS CashInAmount, " & _
                '            " 0 As CashOutAmount  from tbl_DailyIncome H  Left Join tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where H.IsDelete=0 AND H.IncomeDate=@ForDate" & _
                '            " UNION ALL  " & _
                '            " SELECT N'အသုံးစရိတ်(Cash Out)' as Title, 'Out' As Type, '-' As VoucherNo, " & _
                '            " convert(varchar(10),H.ExpenseDate,105) as Date, H.Remark As Customer, (H.TotalAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                '            " H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, (H.TotalAmount) As CashOutAmount  from tbl_DailyExpense H  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0 AND H.ExpenseDate=@ForDate" & _
                '            " UNION ALL  " & _
                '            " SELECT N'WholesaleReturn(Cash Out)' as Title, 'Out' As Type, WholesaleReturnID As VoucherNo, " & _
                '            " convert(varchar(10),H.WReturnDate,105) as Date, H.Remark As Customer, (H.PaidAmount) As PaidAmount, 0 As DiscountAmount,  " & _
                '            " H.LocationID, B.Location, H.Remark, 0 AS CashInAmount, (H.PaidAmount) As CashOutAmount  from tbl_WholesaleReturn H  " & _
                '            " Left Join tbl_Location B ON B.LocationID=H.LocationID" & _
                '            " where H.IsDelete=0  AND SaleReturnAmount> 0  AND H.WReturnDate BETWEEN @FromDate And @ToDate " & _
                '            " UNION ALL" & _
                '            " Select N'အပ်ငွေ(Cash Out)' as Title,'Out' As Type, '-' As VoucherNo, convert(varchar(10),GLDate,105) as Date, " & _
                '            " '-' As Customer, sum(H.CreditAmount) As PaidAmount,0 As DiscountAmount,  H.LocationID, B.Location,'' AS  Remark, " & _
                '            " 0 as CashInAmount,sum(H.CreditAmount) AS CashOutAmount from tbl_GeneralLedgerByLocation H " & _
                '            " LEFT JOIN tbl_Location B ON B.LocationID=H.LocationID  " & _
                '            " where GLDate=@ForDate AND Title=N'အပ်ငွေ' " & _
                '            " Group By GLDate,  H.LocationID, B.Location) as M "
                'End If


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ForDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ForDate.Date & " 23:59:59"))
                DB.AddInParameter(DBComm, "@ForDate", DbType.DateTime, CDate(ForDate.Date))
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)  '
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllOtherCashDataByGeneralLedger(ByVal ForDate As Date) As DataTable Implements IGeneralLedgerByLocationDA.GetAllOtherCashDataByGeneralLedger
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT R.CashTypeID, C.CashType, R.ExchangeRate, sum(R.Amount) AS Amount, sum(R.ExchangeRate*R.Amount) AS TotalAmount FROM tbl_RecordOtherCash R INNER JOIN tbl_SaleInvoiceHeader H ON H.SaleInvoiceHeaderID=R.VoucherNo " & _
                                 " INNER JOIN tbl_CashType C ON C.CashTypeID=R.CashTypeID " & _
                                 " where H.SaleDate BETWEEN @FromDate And @ToDate AND H.IsDelete=0 AND C.IsDelete=0 GROUP BY R.CashTypeID, C.CashType, R.ExchangeRate " & _
                                 " union all " & _
                                 " SELECT RS.CashTypeID, CS.CashType, RS.ExchangeRate, sum(RS.Amount) AS Amount, sum(RS.ExchangeRate*RS.Amount) AS TotalAmount " & _
                                 " FROM tbl_RecordOtherCash RS INNER JOIN tbl_SaleGems G ON G.SaleGemsID=RS.VoucherNo  " & _
                                 " INNER JOIN tbl_CashType CS ON CS.CashTypeID=RS.CashTypeID  " & _
                                 " where G.SDate BETWEEN @FromDate And @ToDate  AND G.IsDelete=0 AND CS.IsDelete=0 GROUP BY RS.CashTypeID, CS.CashType, RS.ExchangeRate  "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ForDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ForDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllRecordOtherCashDataByDate(ByVal ForDate As Date) As System.Data.DataTable Implements IGeneralLedgerByLocationDA.GetAllRecordOtherCashDataByDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT R.RecordCashID, R.VoucherNo, R.CashTypeID, R.ExchangeRate, R.Amount, (R.ExchangeRate*R.Amount) AS TotalAmount, C.CashType, R.VoucherNo, convert(varchar(10),H.SaleDate,105) as Date,   " &
                                 " S.CustomerName As Customer, (H.DiscountAmount) As DiscountAmount, H.Remark " & _
                                 " FROM tbl_RecordOtherCash R INNER JOIN tbl_SaleInvoiceHeader H ON H.SaleInvoiceHeaderID=R.VoucherNo " & _
                                 " LEFT JOIN tbl_Customer S ON S.CustomerID=H.CustomerID " & _
                                 " INNER JOIN tbl_CashType C ON C.CashTypeID=R.CashTypeID " & _
                                 " where H.SaleDate BETWEEN @FromDate And @ToDate AND H.IsDelete=0  " & _
                                 " Union all " & _
                                 " SELECT RS.RecordCashID, RS.VoucherNo,Rs.CashTypeID, RS.ExchangeRate, (RS.Amount) AS Amount, (RS.ExchangeRate*RS.Amount) AS TotalAmount, " & _
                                 " CS.CashType, RS.VoucherNo, convert(varchar(10),G.SDate,105) as Date,    SS.CustomerName As Customer, (G.DiscountAmount) " & _
                                 " As DiscountAmount, G.Remark FROM tbl_RecordOtherCash RS INNER JOIN tbl_SaleGems G ON G.SaleGemsID=RS.VoucherNo  " & _
                                 " INNER JOIN tbl_Customer SS ON SS.CustomerID=G.CustomerID " & _
                                 "INNER JOIN tbl_CashType CS ON CS.CashTypeID=RS.CashTypeID  " & _
                                 "where G.SDate Between @FromDate And @ToDate AND G.IsDelete=0  " & _
                                 " Union all " & _
                                 " SELECT RS.RecordCashID, RS.VoucherNo,Rs.CashTypeID, RS.ExchangeRate, (RS.Amount) AS Amount, (RS.ExchangeRate*RS.Amount) AS TotalAmount, " & _
                                 " CS.CashType, RS.VoucherNo, convert(varchar(10),G.SaleDate,105) as Date,    SS.CustomerName As Customer, (G.DiscountAmount) " & _
                                 " As DiscountAmount, G.Remark FROM tbl_RecordOtherCash RS INNER JOIN tbl_SaleLooseDiamondHeader G ON G.SaleLooseDiamondID=RS.VoucherNo  " & _
                                 " INNER JOIN tbl_Customer SS ON SS.CustomerID=G.CustomerID " & _
                                 "INNER JOIN tbl_CashType CS ON CS.CashTypeID=RS.CashTypeID  " & _
                                 "where G.SaleDate Between @FromDate And @ToDate AND G.IsDelete=0  "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ForDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ForDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllRecordOtherCashData(ByVal FromDate As Date, ByVal ToDate As Date) As System.Data.DataTable Implements IGeneralLedgerByLocationDA.GetAllRecordOtherCashData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT R.RecordCashID, R.VoucherNo, R.CashTypeID, R.ExchangeRate, R.Amount, (R.ExchangeRate*R.Amount) AS TotalAmount, C.CashType FROM tbl_RecordOtherCash R INNER JOIN tbl_SaleInvoiceHeader H ON H.SaleInvoiceHeaderID=R.VoucherNo " & _
                                 " INNER JOIN tbl_CashType C ON C.CashTypeID=R.CashTypeID " & _
                                 " where H.SaleDate BETWEEN @FromDate And @ToDate AND H.IsDelete=0 AND C.IsDelete=0 " & _
                                 " Union all " & _
                                 " SELECT RS.RecordCashID, RS.VoucherNo,Rs.CashTypeID, RS.ExchangeRate, (RS.Amount) AS Amount, (RS.ExchangeRate*RS.Amount) AS TotalAmount, " & _
                                 " CS.CashType FROM tbl_RecordOtherCash RS INNER JOIN tbl_SaleGems G ON G.SaleGemsID=RS.VoucherNo  " & _
                                 " INNER JOIN tbl_CashType CS ON CS.CashTypeID=RS.CashTypeID  " & _
                                 " where G.SDate BETWEEN @FromDate And @ToDate " & _
                                 " AND G.IsDelete=0 AND CS.IsDelete=0 "

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

        Public Function GetAllCustomerTransaction(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IGeneralLedgerByLocationDA.GetAllCustomerTransaction
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark " & _
                                " from (SELECT (select TOP 1 CASE WHEN GQ.IsGramRate=1 THEN 'PT' WHEN F.IsDiamond=1 THEN 'Diamond' ELSE '' END  from tbl_SaleInvoiceDetail SD inner join tbl_ForSale F ON SD.ForSaleID=F.ForSaleID inner join tbl_GoldQuality GQ ON F.GoldQualityID=GQ.GoldQualityID where SD.SaleInvoiceHeaderID=H.SaleInvoiceHeaderID) AS Title, " & _
                                " SaleInvoiceHeaderID AS VoucherNo, H.SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, PurchaseAmount AS ChangeAmount," & _
                                " CASE WHEN H.PaidAmount<0 THEN (AllAdvanceAmount+OtherCashAmount) ELSE (H.PaidAmount+AllAdvanceAmount+OtherCashAmount) END AS CashAmount, " & _
                                " CASE WHEN (IsAdvance=1 AND IsCancel=1) THEN 0 ELSE (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount+H.PurchaseAmount+H.AllAdvanceAmount+H.OtherCashAmount)) END AS NewCredit, " & _
                                "  0 As CashCredit, 0 AS BankCredit, H.Remark  From tbl_SaleInvoiceHeader H inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate " & cristr & _
                                "  Union all" & _
                                " SELECT (select TOP 1 CASE WHEN GQ.IsGramRate=1 THEN 'PT' WHEN F.IsDiamond=1 THEN 'Diamond' ELSE '' END  from tbl_SaleInvoiceDetail SD inner join tbl_ForSale F ON SD.ForSaleID=F.ForSaleID inner join tbl_GoldQuality GQ ON F.GoldQualityID=GQ.GoldQualityID where SD.SaleInvoiceHeaderID=H.SaleInvoiceHeaderID) AS Title, " & _
                                " SaleInvoiceHeaderID AS VoucherNo, H.SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, H.Remark " & _
                                " From tbl_SaleInvoiceHeader H INNER join tbl_CashReceipt CR ON H.SaleInvoiceHeaderID=CR.VoucherNo  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesInvoice' " & cristr & ") As M group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark" & _
                                " union all " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , " & _
                                " Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, " & _
                                " Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark  from (SELECT 'WholeSalesInvoice' AS Title,WholeSaleInvoiceID AS VoucherNo, H.WDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                                " ((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, 0 AS ChangeAmount, H.PaidAmount  AS CashAmount, (((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount))  AS NewCredit, " & _
                                " 0 As CashCredit, 0 AS BankCredit,'' as Remark  From tbl_WholesaleInvoice H " & _
                                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  " & _
                                " where H.PayType<>1 and H.IsDelete=0 AND H.WDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " Union all " & _
                                " SELECT 'WholeSalesInvoice' AS Title, WholeSaleInvoiceID AS VoucherNo, H.WDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                " 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  " & _
                                " CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, '' as Remark  From tbl_WholesaleInvoice H " & _
                                " INNER join tbl_CashReceipt CR ON H.WholesaleInvoiceID=CR.VoucherNo  inner join tbl_Customer C " & _
                                " ON H.CustomerID=C.CustomerID  where H.PayType<>1 and H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='WholeSalesInvoice'" & cristr & " ) As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark  " & _
                                " Union All " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark  " & _
                                " from ( SELECT 'ConsignmentSaleInvoice' AS Title, ConsignmentSaleID AS VoucherNo, H.ConsignDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount,  0 AS ChangeAmount, H.PaidAmount  AS CashAmount, (((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount))  AS NewCredit, 0 As CashCredit, 0 AS BankCredit,'' as Remark  " & _
                                " From tbl_ConsignmentSale H inner join tbl_Customer C ON H.CustomerID=C.CustomerID  " & _
                                " where H.IsDelete=0 AND H.ConsignDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " Union all " & _
                                " SELECT 'ConsignmentSaleInvoice' AS Title, ConsignmentSaleID AS VoucherNo, H.ConsignDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, '' as Remark  From tbl_ConsignmentSale H " & _
                                " INNER join tbl_CashReceipt CR ON H.ConsignmentSaleID=CR.VoucherNo  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='ConsignmentSaleInvoice' " & cristr & " ) As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark  " & _
                                " UNION ALL " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount, sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, sum(NewCredit) As NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark from ( SELECT 'Other' AS Title, SaleGemsID AS VoucherNo, H.SDate AS SaleDate, C.CustomerCode, " & _
                                " C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount,  H.PaidAmount AS CashAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-H.PaidAmount-H.PurchaseAmount) AS NewCredit, 0 As CashCredit, 0 AS BankCredit, H.Remark " & _
                                " From tbl_SaleGems H inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.SDate BETWEEN @FromDate And @ToDate " & cristr & _
                                "  Union all" & _
                                " SELECT 'Other' AS Title, SaleGemsID AS VoucherNo, H.SDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, H.Remark  " & _
                                " From tbl_SaleGems H INNER join  tbl_CashReceipt CR ON CR.VoucherNo=H.SaleGemsID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesGems' " & cristr & _
                                " Union All " & _
                                " SELECT 'SaleLooseDiamond' AS Title, SaleLooseDiamondID AS VoucherNo, H.SaleDate AS SaleDate, C.CustomerCode, " & _
                                " C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount,  H.PaidAmount AS CashAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-H.PaidAmount-H.PurchaseAmount-H.OtherCashAmount) AS NewCredit, 0 As CashCredit, 0 AS BankCredit, H.Remark " & _
                                " From tbl_SaleLooseDiamondHeader H inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate " & cristr & _
                                "  Union all" & _
                                " SELECT 'SaleLooseDiamond' AS Title, SaleLooseDiamondID AS VoucherNo, H.SaleDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, H.Remark  " & _
                                " From tbl_SaleLooseDiamondHeader H INNER join  tbl_CashReceipt CR ON CR.VoucherNo=H.SaleLooseDiamondID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SaleLooseDiamond' " & cristr & _
                                " Union All " & _
                                " SELECT 'SalesVolume' AS Title, SalesVolumeID AS VoucherNo, H.SaleDate AS SaleDate, C.CustomerCode, " & _
                                " C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount,  H.PaidAmount AS CashAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-H.PaidAmount-H.PurchaseAmount) AS NewCredit, 0 As CashCredit, 0 AS BankCredit, H.Remark " & _
                                " From tbl_SalesVolume H inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate " & cristr & _
                                "  Union all" & _
                                " SELECT 'SalesVolume' AS Title, SalesVolumeID AS VoucherNo, H.SaleDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, H.Remark  " & _
                                " From tbl_SalesVolume H INNER join  tbl_CashReceipt CR ON CR.VoucherNo=H.SalesVolumeID inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesInvoiceVolume' " & cristr & _
                                " Union All " & _
                                "SELECT 'ReturnAdvance' AS Title, ReturnAdvanceID AS VoucherNo, H.ReturnAdvanceDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, H.NetAmount AS SaleAmount, 0 AS ChangeAmount,  H.NetAmount AS CashAmount, 0 AS NewCredit, 0  As CashCredit,0 As BankCredit, H.Remark   From tbl_ReturnAdvance H inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 and H.ReturnAdvanceDate BETWEEN @FromDate And @ToDate " & cristr & _
                                ") As M group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark " & _
                                " UNION ALL " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, " & _
                                " Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark   from ( SELECT 'OrderInvoice' AS Title, OrderReturnHeaderID AS VoucherNo," & _
                                " H.ReturnDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.AllTotalAmount+H.AllAddOrSub)-(H.DiscountAmount)-isnull((I.AdvanceAmount+SecondAdvanceAmount),0)) AS SaleAmount,  0 AS ChangeAmount," & _
                                " (H.PaidAmount)  AS CashAmount,  (((H.AllTotalAmount+H.AllAddOrSub)-(H.DiscountAmount))-(H.PaidAmount+(I.AdvanceAmount+I.SecondAdvanceAmount)))  AS NewCredit, " & _
                                " 0 As CashCredit, 0 AS BankCredit,'' as Remark   From tbl_OrderReturnHeader H  Left join tbl_OrderInvoice I on H.OrderInvoiceID=I.OrderInvoiceID " & _
                                " inner join tbl_Customer C ON I.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " Union all " & _
                                " SELECT 'OrderInvoice' AS Title, OrderReturnHeaderID AS VoucherNo, H.ReturnDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                                " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 " & _
                                " THEN CR.PayAmount ELSE 0  END As BankCredit, '' as Remark  From tbl_OrderReturnHeader H   left join tbl_OrderInvoice I on H.OrderInvoiceID=I.OrderInvoiceID  " & _
                                " INNER join tbl_CashReceipt CR ON I.OrderInvoiceID=CR.VoucherNo   " & _
                                " inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate " & _
                                " and CR.Type='OrderInvoice' " & cristr & ") As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark" & _
                                " UNION ALL " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, " & _
                                " Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark   from ( SELECT 'OrderInvoice' AS Title, OrderInvoiceID AS VoucherNo," & _
                                " H.OrderDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,(H.AdvanceAmount+H.SecondAdvanceAmount)  AS SaleAmount,  0 AS ChangeAmount," & _
                                " (H.AdvanceAmount+H.SecondAdvanceAmount)  AS CashAmount, 0  AS NewCredit, 0 As CashCredit, 0 AS BankCredit,'' as Remark " & _
                                " From tbl_OrderInvoice  H " & _
                                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.OrderDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " ) As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark "

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
        Public Function GetAllCustomerReceipt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IGeneralLedgerByLocationDA.GetAllCustomerReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark " & _
                                " from (SELECT (select TOP 1 CASE WHEN GQ.IsGramRate=1 THEN 'PT' WHEN F.IsDiamond=1 THEN 'Diamond' ELSE '' END  from tbl_SaleInvoiceDetail SD inner join tbl_ForSale F ON SD.ForSaleID=F.ForSaleID inner join tbl_GoldQuality GQ ON F.GoldQualityID=GQ.GoldQualityID where SD.SaleInvoiceHeaderID=I.SaleInvoiceHeaderID) AS Title, " & _
                                " SaleInvoiceHeaderID AS VoucherNo, I.SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((I.TotalAmount+I.AddOrSub)-(((I.TotalAmount*PromotionDiscount)/100)+I.DiscountAmount)) AS SaleAmount, PurchaseAmount AS ChangeAmount," & _
                                " CASE WHEN I.PaidAmount<0 THEN (AllAdvanceAmount+OtherCashAmount) ELSE (I.PaidAmount+AllAdvanceAmount+OtherCashAmount) END AS CashAmount, " & _
                                " CASE WHEN (IsAdvance=1 AND IsCancel=1) THEN 0 ELSE (((I.TotalAmount+I.AddOrSub)-(((I.TotalAmount*PromotionDiscount)/100)+I.DiscountAmount))-(I.PaidAmount+I.PurchaseAmount+I.AllAdvanceAmount+I.OtherCashAmount)) END AS NewCredit, " & _
                                "  0 As CashCredit, 0 AS BankCredit, I.Remark  From tbl_SaleInvoiceHeader I inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where I.IsDelete=0 AND I.SaleDate BETWEEN @FromDate And @ToDate " & cristr & _
                                "  Union all" & _
                                " SELECT (select TOP 1 CASE WHEN GQ.IsGramRate=1 THEN 'PT' WHEN F.IsDiamond=1 THEN 'Diamond' ELSE '' END  from tbl_SaleInvoiceDetail SD inner join tbl_ForSale F ON SD.ForSaleID=F.ForSaleID inner join tbl_GoldQuality GQ ON F.GoldQualityID=GQ.GoldQualityID where SD.SaleInvoiceHeaderID=I.SaleInvoiceHeaderID) AS Title, " & _
                                " SaleInvoiceHeaderID AS VoucherNo,I.SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, I.Remark " & _
                                " From tbl_SaleInvoiceHeader I INNER join tbl_CashReceipt CR ON I.SaleInvoiceHeaderID=CR.VoucherNo  inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where I.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesInvoice' " & cristr & ") As M group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark" & _
                                "  union all " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , " & _
                                " Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, " & _
                                " Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark  from (SELECT 'WholeSalesInvoice' AS Title,WholeSaleInvoiceID AS VoucherNo, I.WDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                                " ((I.NetAmount+I.AddOrSub)-(I.Discount)) AS SaleAmount, 0 AS ChangeAmount, I.PaidAmount  AS CashAmount, (((I.NetAmount+I.AddOrSub)-(I.Discount))-(I.PaidAmount))  AS NewCredit, " & _
                                " 0 As CashCredit, 0 AS BankCredit,'' as Remark  From tbl_WholesaleInvoice I " & _
                                " inner join tbl_Customer C ON I.CustomerID=C.CustomerID  " & _
                                " where I.PayType<>1 and I.IsDelete=0 AND I.WDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " Union all " & _
                                " SELECT 'WholeSalesInvoice' AS Title, WholeSaleInvoiceID AS VoucherNo, I.WDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                " 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  " & _
                                " CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, '' as Remark  From tbl_WholesaleInvoice I " & _
                                " INNER join tbl_CashReceipt CR ON I.WholesaleInvoiceID=CR.VoucherNo  inner join tbl_Customer C " & _
                                " ON I.CustomerID=C.CustomerID  where I.PayType<>1 and I.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='WholeSalesInvoice' " & _
                                " ) As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark  " & _
                                " UNION ALL " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark  " & _
                                " from ( SELECT 'ConsignmentSaleInvoice' AS Title, ConsignmentSaleID AS VoucherNo, I.ConsignDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((I.NetAmount+I.AddOrSub)-(I.Discount)) AS SaleAmount,  0 AS ChangeAmount, I.PaidAmount  AS CashAmount, (((I.NetAmount+I.AddOrSub)-(I.Discount))-(I.PaidAmount))  AS NewCredit, 0 As CashCredit, 0 AS BankCredit,'' as Remark  " & _
                                " From tbl_ConsignmentSale I inner join tbl_Customer C ON I.CustomerID=C.CustomerID  " & _
                                " where I.IsDelete=0 AND I.ConsignDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " Union all " & _
                                " SELECT 'ConsignmentSaleInvoice' AS Title, ConsignmentSaleID AS VoucherNo, I.ConsignDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, '' as Remark  From tbl_ConsignmentSale I " & _
                                " INNER join tbl_CashReceipt CR ON I.ConsignmentSaleID=CR.VoucherNo  inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where I.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='ConsignmentSaleInvoice' " & cristr & " ) As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark  " & _
                                " UNION ALL " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount, sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, sum(NewCredit) As NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark from ( SELECT 'Other' AS Title, SaleGemsID AS VoucherNo, I.SDate AS SaleDate, C.CustomerCode, " & _
                                " C.CustomerName, C.CustomerAddress, ((I.TotalAmount+I.AddOrSub)-(((I.TotalAmount*PromotionDiscount)/100)+I.DiscountAmount)) AS SaleAmount, 0 AS ChangeAmount,  I.PaidAmount AS CashAmount, (((I.TotalAmount+I.AddOrSub)-(((I.TotalAmount*PromotionDiscount)/100)+I.DiscountAmount))-I.PaidAmount) AS NewCredit, 0 As CashCredit, 0 AS BankCredit, I.Remark " & _
                                " From tbl_SaleGems I inner join tbl_Customer C ON I.CustomerID=C.CustomerID   where I.IsDelete=0 AND I.SDate BETWEEN @FromDate And @ToDate " & cristr & _
                                "  Union all" & _
                                " SELECT 'Other' AS Title, SaleGemsID AS VoucherNo, I.SDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, I.Remark  " & _
                                " From tbl_SaleGems I INNER join  tbl_CashReceipt CR ON CR.VoucherNo=I.SaleGemsID  inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where I.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesGems' " & cristr & _
                                " Union All " & _
                                 " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount, sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, sum(NewCredit) As NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark from ( SELECT 'Other' AS Title, SaleLooseDiamondID AS VoucherNo, I.SDate AS SaleDate, C.CustomerCode, " & _
                                " C.CustomerName, C.CustomerAddress, ((I.TotalAmount+I.AddOrSub)-(((I.TotalAmount*PromotionDiscount)/100)+I.DiscountAmount)) AS SaleAmount, 0 AS ChangeAmount,  I.PaidAmount+I.OtherCashAmount AS CashAmount, (((I.TotalAmount+I.AddOrSub)-(((I.TotalAmount*PromotionDiscount)/100)+I.DiscountAmount))-(I.PaidAmount+I.OtherCashAmount)) AS NewCredit, 0 As CashCredit, 0 AS BankCredit, I.Remark " & _
                                " From tbl_SaleLooseDiamondHeader I inner join tbl_Customer C ON I.CustomerID=C.CustomerID   where I.IsDelete=0 AND I.SDate BETWEEN @FromDate And @ToDate " & cristr & _
                                "  Union all" & _
                                " SELECT 'Other' AS Title, SaleLooseDiamondID AS VoucherNo, I.SDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 END As BankCredit, I.Remark  " & _
                                " From tbl_SaleLooseDiamondHeader I INNER join  tbl_CashReceipt CR ON CR.VoucherNo=I.SaleLooseDiamondID  inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where I.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SaleLooseDiamond' " & cristr & _
                                " Union All " & _
                                " SELECT 'ReturnAdvance' AS Title, ReturnAdvanceID AS VoucherNo, I.ReturnAdvanceDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, I.NetAmount AS SaleAmount, 0 AS ChangeAmount,  I.NetAmount AS CashAmount, 0 AS NewCredit, 0  As CashCredit,0 As BankCredit, I.Remark   From tbl_ReturnAdvance I inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where I.IsDelete=0 and I.ReturnAdvanceDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " ) As M group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark " & _
                                " UNION ALL " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount," & _
                                " Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark   from ( SELECT 'OrderInvoice' AS Title, OrderReturnHeaderID AS VoucherNo, " & _
                                " H.ReturnDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.AllTotalAmount+H.AllAddOrSub)-(H.DiscountAmount)) AS SaleAmount,  0 AS ChangeAmount,(H.PaidAmount)  AS CashAmount, " & _
                                " (((H.AllTotalAmount+H.AllAddOrSub)-(H.DiscountAmount))-(H.PaidAmount+(I.AdvanceAmount+I.SecondAdvanceAmount)))  AS NewCredit, 0 As CashCredit, 0 AS BankCredit,'' as Remark   From tbl_OrderReturnHeader  H " & _
                                " left join tbl_OrderInvoice I on H.OrderInvoiceID=I.OrderInvoiceID " & _
                                " inner join tbl_Customer C ON I.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " Union all  " & _
                                " SELECT 'OrderInvoice' AS Title, OrderReturnHeaderID AS VoucherNo, H.ReturnDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                " 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,  CASE CR.IsBank When 0 THEN CR.PayAmount ELSE 0 END As CashCredit,  CASE CR.IsBank When 1 THEN CR.PayAmount ELSE 0 " & _
                                " END As BankCredit, '' as Remark  From tbl_OrderReturnHeader H  " & _
                                " left join tbl_OrderInvoice I on H.OrderInvoiceID=I.OrderInvoiceID " & _
                                " INNER join tbl_CashReceipt CR ON H.OrderInvoiceID=CR.VoucherNo  " & _
                                " inner join tbl_Customer C ON I.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='OrderInvoice' " & cristr & _
                                " ) As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark  " & _
                                " UNION ALL " & _
                                " select Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, " & _
                                " Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit, Sum(BankCredit) aS BankCredit, Remark   from ( SELECT 'OrderInvoice' AS Title, OrderInvoiceID AS VoucherNo," & _
                                " I.OrderDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, (I.AllTotalAmount+I.AllAddOrSub) AS SaleAmount,  0 AS ChangeAmount," & _
                                " (I.AdvanceAmount+I.SecondAdvanceAmount)  AS CashAmount,  ((I.AllTotalAmount+I.AllAddOrSub)-(I.AdvanceAmount+I.SecondAdvanceAmount))  AS NewCredit, 0 As CashCredit, 0 AS BankCredit,'' as Remark " & _
                                " From tbl_OrderInvoice  I " & _
                                " inner join tbl_Customer C ON I.CustomerID=C.CustomerID   where I.IsDelete=0 And  I.OrderDate BETWEEN @FromDate And @ToDate " & cristr & _
                                " ) As M  group by Title, VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress, Remark "

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

        Public Function GetAllCashTransaction(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal Type As String = "", Optional ByVal GoldQualityID As String = "", Optional ByVal CustomerCode As String = "", Optional ByVal LocationID As String = "", Optional ByVal CustomerID As String = "", Optional ByVal str As String = "") As System.Data.DataTable Implements IGeneralLedgerByLocationDA.GetAllCashTransaction
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim strDateCommand As String = ""
            Dim VarDate As Date = FromDate
            Dim TDate As Date = FromDate
            Dim ChangeDate As String
            Dim StrDateCommand2 As String = ""
            Dim dtOpening As DataTable
            Dim dtTransaction As DataTable
            Dim OpeningDate As Date

            Try
                Select Case Type
                    Case "All"


                        'strCommandText = "  Select SaleDate,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(CreditAmount) as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from (" & _
                        '                 "  Select SaleDate,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(NewCredit) as CreditAmount,Sum(CashCredit) as ReceiveAmount from (" & _
                        '                 " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                 " Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit  from ( " & _
                        '                 " select distinct H.SaleInvoiceHeaderID AS VoucherNo, Convert(Date,H.SaleDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub) " & _
                        '                 " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, PurchaseAmount AS ChangeAmount, CASE WHEN H.PaidAmount<0 THEN " & _
                        '                 " (AllAdvanceAmount+OtherCashAmount) ELSE (H.PaidAmount+AllAdvanceAmount+OtherCashAmount) END AS CashAmount,0 As NewCredit, 0 As CashCredit, H.Remark  From tbl_SaleInvoiceHeader H " & _
                        '                 " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                        '                 " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                 " Union all " & _
                        '                 " select distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  " & _
                        '                 " 0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SaleInvoiceHeader H " & _
                        '                 " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                        '                 " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                        '                 " INNER join tbl_CashReceipt CR ON H.SaleInvoiceHeaderID=CR.VoucherNo  " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate " & _
                        '                 " AND @ToDate and CR.Type='SalesInvoice' " & cristr & ") As M group by  VoucherNo, SaleDate, CustomerCode, CustomerName,CustomerAddress " & _
                        '                 " union all  " & _
                        '                 " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                 " Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit  from ( " & _
                        '                 " select distinct H.SaleLooseDiamondID AS VoucherNo, Convert(Date,H.SaleDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub) " & _
                        '                 " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, PurchaseAmount AS ChangeAmount, CASE WHEN H.PaidAmount<0 THEN " & _
                        '                 " (OtherCashAmount) ELSE (H.PaidAmount+OtherCashAmount) END AS CashAmount,0 As NewCredit, 0 As CashCredit, H.Remark  From tbl_SaleLooseDiamondHeader H " & _
                        '                 " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID " & _
                        '                 " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                 " Union all " & _
                        '                 " select distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  " & _
                        '                 " 0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SaleLooseDiamondHeader H " & _
                        '                 " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID " & _
                        '                 " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                        '                 " INNER join tbl_CashReceipt CR ON H.SaleLooseDiamondID=CR.VoucherNo  " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate " & _
                        '                 " AND @ToDate and CR.Type='SaleLooseDiamond' " & cristr & ") As M group by  VoucherNo, SaleDate,CustomerCode, CustomerName,CustomerAddress " & _
                        '                 " union all  " & _
                        '                 " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,  " & _
                        '                 " Sum(ChangeAmount) AS ChangeAmount, Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit,  Sum(CashCredit) As CashCredit  from (" & _
                        '                 " SELECT distinct 'WholeSalesInvoice' AS Title,H.WholeSaleInvoiceID AS VoucherNo, Convert(Date,H.WDate) as SaleDate, C.CustomerCode, C.CustomerName, " & _
                        '                 " C.CustomerAddress,  ((H.NetAmount+H.AddOrSub)-(H.Discount)) AS SaleAmount, 0 AS ChangeAmount, H.PaidAmount  AS CashAmount,0  AS NewCredit,  0 As CashCredit,'' as Remark  From tbl_WholesaleInvoice H  " & _
                        '                 " Inner Join tbl_WholeSaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.PayType<>1 and H.IsDelete=0 AND H.WDate BETWEEN @FromDate " & _
                        '                 " AND @ToDate " & cristr & _
                        '                 " Union all  " & _
                        '                 " SELECT distinct 'WholeSalesInvoice' AS Title, CR.CashReceiptID AS VoucherNo, Convert(DAte,CR.PayDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress," & _
                        '                 " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_WholesaleInvoice H  " & _
                        '                 " Inner Join tbl_WholeSaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID " & _
                        '                 " INNER join tbl_CashReceipt CR ON H.WholesaleInvoiceID=CR.VoucherNo inner join tbl_Customer C  ON H.CustomerID=C.CustomerID  where H.PayType<>1 and H.IsDelete=0 " & _
                        '                 " AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='WholeSalesInvoice' " & cristr & ") As M " & _
                        '                 " group by  VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress" & _
                        '                 " Union All  " & _
                        '                 " select   SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                 " Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from ( " & _
                        '                 " SELECT distinct 'ConsignmentSaleInvoice' AS Title, H.ConsignmentSaleID AS VoucherNo, Convert(Date,H.ConsignDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                        '                 " ((H.NetAmount+H.AddOrSub)-(H.Discount)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount, H.PaidAmount  AS CashAmount, 0  AS NewCredit, 0 As CashCredit,'' as Remark   From tbl_ConsignmentSale H " & _
                        '                 " Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.ConsignDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                 " Union all  " & _
                        '                 " SELECT distinct 'ConsignmentSaleInvoice' AS Title, CR.CashReceiptID AS VoucherNo,Convert(Date,CR.PayDate) as SaleDate, C.CustomerCode, " & _
                        '                 " C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount " & _
                        '                 " As CashCredit,  '' as Remark  From tbl_ConsignmentSale H  " & _
                        '                 " Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                        '                 " INNER join tbl_CashReceipt CR ON H.ConsignmentSaleID=CR.VoucherNo  " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate " & _
                        '                 " AND @ToDate and CR.Type='ConsignmentSaleInvoice' " & cristr & " ) As M  group by  VoucherNo, SaleDate, CustomerCode, " & _
                        '                 " CustomerName, CustomerAddress  " & _
                        '                 " UNION ALL " & _
                        '                 " select  SaleDate,CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount, sum(ChangeAmount) AS ChangeAmount, " & _
                        '                 " Sum(CashAmount) AS CashAmount, sum(NewCredit) As NewCredit, Sum(CashCredit) As CashCredit from ( " & _
                        '                 " SELECT distinct H.SaleGemsID AS VoucherNo, Convert(Date,H.SDate) AS SaleDate,C.CustomerCode,  C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub)" & _
                        '                 " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount,  H.PaidAmount AS CashAmount, " & _
                        '                 " 0 AS NewCredit, 0 As CashCredit, H.Remark  From tbl_SaleGems H " & _
                        '                 " Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.SDate BETWEEN @FromDate And @ToDate And H.CustomerID='" & CustomerID & "' And H.LocationID='" & LocationID & "' " & _
                        '                 "  Union all " & _
                        '                 " SELECT  distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                        '                 " 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CR.PayAmount As CashCredit, H.Remark   From tbl_SaleGems H " & _
                        '                 " Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                        '                 " INNER join  tbl_CashReceipt CR ON CR.VoucherNo=H.SaleGemsID" & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate " & _
                        '                 " AND @ToDate and CR.Type='SalesGems' And H.CustomerID='" & CustomerID & "' And H.LocationID='" & LocationID & "' " & _
                        '                 " Union All " & _
                        '                 " SELECT distinct ReturnAdvanceID AS VoucherNo, Convert(Date,H.ReturnAdvanceDate) AS SaleDate,C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                        '                 " H.NetAmount AS SaleAmount, 0 AS ChangeAmount,  H.NetAmount AS CashAmount, 0 AS NewCredit, 0  As CashCredit, H.Remark   " & _
                        '                 " From tbl_ReturnAdvance H " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 and H.ReturnAdvanceDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " And H.CustomerID='" & CustomerID & "' " & _
                        '                 " ) As M group by  VoucherNo, SaleDate, CustomerCode, CustomerName, CustomerAddress " & _
                        '                 " UNION ALL " & _
                        '                 " select   SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                 " Sum(CashAmount) AS CashAmount,  Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from ( " & _
                        '                 " SELECT distinct 'OrderInvoice' AS Title, OrderReturnHeaderID AS VoucherNo, Convert(Date,I.ReturnDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                        '                 " ((I.AllTotalAmount+I.AllAddOrSub)-(I.DiscountAmount)-isnull((H.AdvanceAmount+SecondAdvanceAmount),0)) AS SaleAmount,  0 AS ChangeAmount, (I.PaidAmount)  " & _
                        '                 " AS CashAmount,  0  AS NewCredit,  " & _
                        '                 " 0 As CashCredit,'' as Remark   From tbl_OrderReturnHeader  I  " & _
                        '                 " left join tbl_OrderInvoice H on H.OrderInvoiceID=I.OrderInvoiceID  " & _
                        '                 " Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isDelete=0 AND I.ReturnDate BETWEEN @FromDate " & _
                        '                 " AND @ToDate " & cristr & _
                        '                 " Union all " & _
                        '                 " SELECT distinct 'OrderInvoice' AS Title, CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,  " & _
                        '                 " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_OrderReturnHeader I  " & _
                        '                 " left join tbl_OrderInvoice H on H.OrderInvoiceID=I.OrderInvoiceID   " & _
                        '                 " Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID " & _
                        '                 " INNER join tbl_CashReceipt CR ON H.OrderInvoiceID=CR.VoucherNo" & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  AND CR.IsDelete=0 and CR.PayDate " & _
                        '                 " BETWEEN @FromDate And @ToDate  and CR.Type='OrderInvoice'" & cristr & " ) As M  group by  VoucherNo, SaleDate, " & _
                        '                 " CustomerCode, CustomerName, CustomerAddress " & _
                        '                 " UNION ALL " & _
                        '                 " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                 " Sum(CashAmount) AS CashAmount,  Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from ( " & _
                        '                 " SELECT distinct 'OrderInvoice' AS Title, H.OrderInvoiceID AS VoucherNo, Convert(Date,H.OrderDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,(H.AdvanceAmount" & _
                        '                 " +H.SecondAdvanceAmount)  AS SaleAmount,  0 AS ChangeAmount, (H.AdvanceAmount+H.SecondAdvanceAmount)  AS CashAmount, 0  AS NewCredit, 0 As CashCredit, " & _
                        '                 " '' as Remark  From tbl_OrderInvoice  H  " & _
                        '                 " Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.OrderDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                 "  ) As M  group by SaleDate, CustomerCode, CustomerName, CustomerAddress " & _
                        '                 " UNION ALL" & _
                        '                " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                " Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit  from (" & _
                        '                " select distinct H.SalesVolumeID AS VoucherNo, Convert(Date,H.SaleDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub)" & _
                        '                " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, PurchaseAmount AS ChangeAmount,H.PaidAmount AS CashAmount,0 As NewCredit, 0 As CashCredit, H.Remark  From tbl_SalesVolume  H " & _
                        '                " Inner Join tbl_SalesVolumeDetail D on H.SalesVolumeID=D.SalesVolumeID" & _
                        '                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                " Union all " & _
                        '                " select distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  " & _
                        '                " 0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SalesVolume H " & _
                        '                " Inner Join tbl_SalesVolumeDetail D on H.SalesVolumeID=D.SalesVolumeID" & _
                        '                " INNER join tbl_CashReceipt CR ON H.SalesVolumeID=CR.VoucherNo  " & _
                        '                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate " & _
                        '                " AND @ToDate and CR.Type='SalesInvoiceVolume'" & cristr & ") As M group by  VoucherNo, SaleDate, CustomerCode, CustomerName,CustomerAddress " & _
                        '                " UNION ALL" & _
                        '                " select   SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                " Sum(CashAmount) AS CashAmount,  Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from ( " & _
                        '                " SELECT distinct 'RepairInvoice' AS Title, ReturnRepairID AS VoucherNo, Convert(Date,I.ReturnDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                        '                " ((I.AllReturnTotalAmount+I.AllReturnAddOrSub)-(I.ReturnDiscountAmount)-isnull((H.AdvanceRepairAmount),0)) AS SaleAmount,  0 AS ChangeAmount, (I.ReturnPaidAmount)  " & _
                        '                "  AS CashAmount,  0  AS NewCredit,  " & _
                        '                "  0 As CashCredit,'' as Remark   From tbl_ReturnRepairHeader  I  " & _
                        '                " Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID " & _
                        '                 " left join tbl_RepairHeader H on H.RepairID=I.RepairID  " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isDelete=0 AND I.ReturnDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                 " Union all  " & _
                        '                 " SELECT distinct 'RepairInvoice' AS Title, CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,  " & _
                        '                 " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_ReturnRepairHeader I   " & _
                        '                 " Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID " & _
                        '                " left join tbl_RepairHeader H on H.RepairID=I.RepairID   " & _
                        '                "  INNER join tbl_CashReceipt CR ON H.RepairID=CR.VoucherNo    " & _
                        '                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  AND CR.IsDelete=0 and CR.PayDate " & _
                        '                "  BETWEEN @FromDate And @ToDate  and CR.Type='RepairReturn'" & cristr & " ) As M  group by  VoucherNo, SaleDate, " & _
                        '                "  CustomerCode, CustomerName, CustomerAddress " & _
                        '                " UNION ALL  " & _
                        '                "  select distinct SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                "  Sum(CashAmount) AS CashAmount,  Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from ( " & _
                        '                "  SELECT 'RepairInvoice' AS Title, H.RepairID AS VoucherNo, Convert(Date,H.RepairDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,(H.AdvanceRepairAmount)  AS SaleAmount, " & _
                        '                " 0 AS ChangeAmount, (H.AdvanceRepairAmount)  AS CashAmount, 0  AS NewCredit, 0 As CashCredit, " & _
                        '                " '' as Remark  From tbl_RepairHeader  H  " & _
                        '                " Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID " & _
                        '                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.RepairDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                " ) As M  group by SaleDate, CustomerCode, CustomerName, CustomerAddress " & _
                        '                " ) as F group By SaleDate,CustomerCode ) As T  Where 1=1  group By SaleDate Order By SaleDate desc "

                        strCommandText = "Select SaleDate,VoucherNo,SaleAmount, ChangeAmount, CashAmount, VCredit, CreditAmount,ReceiveAmount from (" & _
                                        "Select SaleDate,Title,VoucherNo,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(SaleAmount-CashAmount-ChangeAmount) as VCredit,Sum(CreditAmount) as CreditAmount,Sum(ReceiveAmount) " & _
                                        " as ReceiveAmount from (  " & _
                                        " Select SaleDate,Title,VoucherNo,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(NewCredit) as " & _
                                        " CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " select distinct H.SaleInvoiceHeaderID AS VoucherNo,'SaleInvoice' as Title, H.SaleDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount" & _
                                        " +H.AddOrSub)  -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, PurchaseAmount AS ChangeAmount, CASE WHEN H.PaidAmount<0 THEN  " & _
                                        " (AllAdvanceAmount+OtherCashAmount) ELSE (H.PaidAmount+AllAdvanceAmount+OtherCashAmount) END AS CashAmount,0 As NewCredit, 0 As CashCredit, H.Remark  " & _
                                        " From tbl_SaleInvoiceHeader H  Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID  Inner Join tbl_forsale D " & _
                                        " On D.ForSaleID=F.ForSaleID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate   " & cristr & _
                                        " union all   " & _
                                        " select distinct  H.SaleLooseDiamondID AS VoucherNo,'SaleLooseDiamond' as Title,H.SaleDate as SaleDate, " & _
                                        " C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub)  -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, " & _
                                        " PurchaseAmount AS ChangeAmount, CASE WHEN H.PaidAmount<0 THEN  (OtherCashAmount) ELSE (H.PaidAmount+OtherCashAmount) END AS CashAmount,0 As NewCredit, 0 As " & _
                                        " CashCredit, H.Remark  From tbl_SaleLooseDiamondHeader H  Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID  " & _
                                        " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate " & _
                                        " BETWEEN @FromDate And @ToDate   " & cristr & _
                                        " union all   " & _
                                        " SELECT distinct H.WholeSaleInvoiceID AS VoucherNo, 'WholeSalesInvoice' AS Title, H.WDate as SaleDate, C.CustomerCode, C.CustomerName,  " & _
                                        " C.CustomerAddress,  ((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, 0 AS ChangeAmount, H.PaidAmount  AS CashAmount,0  AS NewCredit,  0 As CashCredit,'' as " & _
                                        " Remark  From tbl_WholesaleInvoice H   Inner Join tbl_WholeSaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID  inner join tbl_Customer C " & _
                                        " ON H.CustomerID=C.CustomerID   where H.PayType<>1 and H.IsDelete=0 AND H.WDate BETWEEN @FromDate And @ToDate " & cristr & _
                                        " Union All   " & _
                                        " SELECT distinct  H.ConsignmentSaleID AS VoucherNo,'ConsignmentSaleInvoice' AS Title, H.ConsignDate as SaleDate, C.CustomerCode, C.CustomerName, " & _
                                        " C.CustomerAddress,  ((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount, H.PaidAmount  AS CashAmount, 0  AS NewCredit, " & _
                                        " 0 As CashCredit,'' as Remark   From tbl_ConsignmentSale H  Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID  " & _
                                        " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.ConsignDate BETWEEN @FromDate And @ToDate" & cristr & _
                                        " UNION ALL   " & _
                                        " SELECT distinct H.SaleGemsID AS VoucherNo,'SaleGem' as Title, H.SDate AS SaleDate,C.CustomerCode,  C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub) " & _
                                        " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount,  H.PaidAmount AS CashAmount,  0 AS NewCredit, 0 As " & _
                                        " CashCredit, H.Remark  From tbl_SaleGems H  Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID   " & _
                                        " where H.IsDelete=0 AND H.SDate BETWEEN @FromDate And @ToDate " & cristr & _
                                        " Union All  " & _
                                        " SELECT distinct  ReturnAdvanceID AS VoucherNo,'ReturnAdvance' as Title, H.ReturnAdvanceDate AS SaleDate,C.CustomerCode, C.CustomerName, C.CustomerAddress,  H.NetAmount AS " & _
                                        " SaleAmount, 0 AS ChangeAmount,  H.NetAmount AS CashAmount, 0 AS NewCredit, 0  As CashCredit, H.Remark    From tbl_ReturnAdvance H  inner join tbl_Customer C ON " & _
                                        " H.CustomerID=C.CustomerID  where H.IsDelete=0 and H.ReturnAdvanceDate BETWEEN @FromDate And @ToDate  " & cristr & _
                                        " UNION ALL  " & _
                                        " SELECT distinct  OrderReturnHeaderID AS VoucherNo,'OrderInvoice' AS Title, " & _
                                        " I.ReturnDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,  ((I.AllTotalAmount+I.AllAddOrSub)-(I.DiscountAmount)" & _
                                        " -isnull((H.AdvanceAmount+SecondAdvanceAmount),0)) AS SaleAmount,  0 AS ChangeAmount, (I.PaidAmount)   AS CashAmount,  0  AS NewCredit,   0 As CashCredit," & _
                                        " '' as Remark   From tbl_OrderReturnHeader  I   left join tbl_OrderInvoice H on H.OrderInvoiceID=I.OrderInvoiceID   Inner Join tbl_OrderReceiveDetail D " & _
                                        " on H.OrderInvoiceID=D.OrderInvoiceID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isDelete=0 AND I.ReturnDate " & _
                                        " BETWEEN @FromDate And @ToDate  " & cristr & _
                                        " UNION ALL  " & _
                                        " SELECT distinct  H.OrderInvoiceID AS VoucherNo,'OrderInvoice' AS Title,H.OrderDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress," & _
                                        " (H.AdvanceAmount +H.SecondAdvanceAmount)  AS SaleAmount,  0 AS ChangeAmount, (H.AdvanceAmount+H.SecondAdvanceAmount)  AS CashAmount, 0  AS NewCredit, " & _
                                        " 0 As CashCredit,  '' as Remark  From tbl_OrderInvoice  H   Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID  " & _
                                        " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.OrderDate BETWEEN @FromDate And @ToDate   " & cristr & _
                                        " UNION ALL " & _
                                        " select distinct  H.SalesVolumeID AS VoucherNo, 'SaleVolume' as Title, H.SaleDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub) " & _
                                        " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, PurchaseAmount AS ChangeAmount,H.PaidAmount AS CashAmount,0 As NewCredit, 0 As " & _
                                        " CashCredit, H.Remark  From tbl_SalesVolume  H  Inner Join tbl_SalesVolumeDetail D on H.SalesVolumeID=D.SalesVolumeID inner join tbl_Customer C " & _
                                        " ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate  " & cristr & _
                                        " UNION ALL " & _
                                        " SELECT distinct  ReturnRepairID AS VoucherNo,'RepairInvoice' AS Title, " & _
                                        " I.ReturnDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,  ((I.AllReturnTotalAmount+I.AllReturnAddOrSub)" & _
                                        " -(I.ReturnDiscountAmount)-isnull((H.AdvanceRepairAmount),0)) AS SaleAmount,  0 AS ChangeAmount, (I.ReturnPaidAmount)    AS CashAmount,  0  AS NewCredit,    " & _
                                        " 0 As CashCredit,'' as Remark   From tbl_ReturnRepairHeader  I   Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID  left join tbl_RepairHeader H " & _
                                        " on H.RepairID=I.RepairID   inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isDelete=0 AND I.ReturnDate " & _
                                        " BETWEEN @FromDate And @ToDate   " & cristr & _
                                        " Union all   " & _
                                        " SELECT  H.RepairID AS VoucherNo,'RepairInvoice' AS Title," & _
                                        " H.RepairDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,(H.AdvanceRepairAmount)  AS SaleAmount,  0 AS ChangeAmount, (H.AdvanceRepairAmount)  " & _
                                        " AS CashAmount, 0  AS NewCredit, 0 As CashCredit,  '' as Remark  From tbl_RepairHeader  H   Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID  " & _
                                        " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.RepairDate BETWEEN @FromDate And @ToDate   " & cristr & _
                                        "  ) As M  group by SaleDate,Title, CustomerCode,VoucherNo, CustomerName, CustomerAddress  ) As T  " & _
                                        " Where 1=1  group By SaleDate,Title,VoucherNo " & _
                                        " UNION ALL" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime),'-' as Title,'-' As VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( select distinct 'SaleInvoice' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType, CR.PayDate As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                        " 0 AS ChangeAmount,   0 AS CashAmount, 0 AS NewCredit, CR.PayAmount CashCredit, H.Remark  From tbl_SaleInvoiceHeader H  Inner Join tbl_SaleInvoiceDetail F " & _
                                        " On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID  Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID  INNER join tbl_CashReceipt CR " & _
                                        " ON H.SaleInvoiceHeaderID=CR.VoucherNo   inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate " & _
                                        " BETWEEN @FromDate And @ToDate and CR.Type='SalesInvoice'  " & cristr & _
                                        " Union all  " & _
                                        " select distinct 'SaleLooseDiamond' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType, CR.PayDate As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                        " 0 AS ChangeAmount,   0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SaleLooseDiamondHeader H  " & _
                                        " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID  Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID  " & _
                                        " INNER join tbl_CashReceipt CR ON H.SaleLooseDiamondID=CR.VoucherNo   inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 " & _
                                        " AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SaleLooseDiamond'  " & cristr & _
                                        " Union all   " & _
                                        " SELECT distinct 'WholeSalesInvoice' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType, CR.PayDate as SaleDate, C.CustomerCode, C.CustomerName, " & _
                                        " C.CustomerAddress, 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_WholesaleInvoice H   " & _
                                        " Inner Join tbl_WholeSaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID  INNER join tbl_CashReceipt CR ON H.WholesaleInvoiceID=CR.VoucherNo " & _
                                        " inner join tbl_Customer C  ON H.CustomerID=C.CustomerID  where H.PayType<>1 and H.IsDelete=0  AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate " & _
                                        " and CR.Type='WholeSalesInvoice'  " & cristr & _
                                        " Union all   " & _
                                        " SELECT distinct 'ConsignmentSaleInvoice' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType,CR.PayDate as SaleDate, C.CustomerCode,  C.CustomerName, " & _
                                        " C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount  As CashCredit,  '' as Remark  From tbl_ConsignmentSale H   " & _
                                        " Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID  INNER join tbl_CashReceipt CR ON H.ConsignmentSaleID=CR.VoucherNo   " & _
                                        " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate " & _
                                        " and CR.Type='ConsignmentSaleInvoice'  " & cristr & _
                                        " Union all  " & _
                                        " SELECT  distinct 'SaleGem' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType, CR.PayDate AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount,  " & _
                                        " 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CR.PayAmount As CashCredit, H.Remark   From tbl_SaleGems H  Inner Join tbl_SaleGemsItem D " & _
                                        " On H.SaleGemsID=D.SaleGemsID  INNER join  tbl_CashReceipt CR ON CR.VoucherNo=H.SaleGemsID inner join tbl_Customer C ON H.CustomerID=C.CustomerID  " & _
                                        " where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesGems' " & cristr & _
                                        " Union all  " & _
                                        " SELECT distinct 'OrderInvoice' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType, CR.PayDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,   " & _
                                        " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_OrderReturnHeader I   " & _
                                        " left join tbl_OrderInvoice H on H.OrderInvoiceID=I.OrderInvoiceID    Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID  " & _
                                        " INNER join tbl_CashReceipt CR ON H.OrderInvoiceID=CR.VoucherNo inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  " & _
                                        " AND CR.IsDelete=0 and CR.PayDate  BETWEEN @FromDate And @ToDate  and CR.Type='OrderInvoice' " & cristr & _
                                        " Union all  " & _
                                        " select distinct 'SaleVolume' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType, CR.PayDate As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount," & _
                                        " 0 AS ChangeAmount,   0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SalesVolume H  Inner Join tbl_SalesVolumeDetail D " & _
                                        " on H.SalesVolumeID=D.SalesVolumeID INNER join tbl_CashReceipt CR ON H.SalesVolumeID=CR.VoucherNo   inner join tbl_Customer C ON H.CustomerID=C.CustomerID  " & _
                                        " where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesInvoiceVolume' " & cristr & _
                                        " UNION ALL" & _
                                        " SELECT distinct 'RepairInvoice' AS Title, CR.CashReceiptID AS VoucherNo,'CashReceipt' as TType, CR.PayDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,   " & _
                                        " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_ReturnRepairHeader I    " & _
                                        " Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID  left join tbl_RepairHeader H on H.RepairID=I.RepairID     INNER join tbl_CashReceipt CR " & _
                                        " ON H.RepairID=CR.VoucherNo     inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  AND CR.IsDelete=0 and CR.PayDate   " & _
                                        " BETWEEN @FromDate And @ToDate  and CR.Type='RepairReturn' " & cristr & ") as M group By Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) ) As F order by SaleDate "

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        'Dim OpeningDate As Date
                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)

                        If dtTransaction.Rows.Count > 0 Then
                            For k As Integer = 0 To dtTransaction.Rows.Count - dtTransaction.Rows.Count
                                ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                                OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                                strDateCommand = "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From (" & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit " & _
                                                  " From ( SELECT  distinct H.OrderInvoiceID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,(((I.AllTotalAmount+I.AllAddOrSub)-(I.DiscountAmount))-(I.PaidAmount " & _
                                                  " +(H.AdvanceAmount+H.SecondAdvanceAmount)))  AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_OrderReturnHeader  I  " & _
                                                  " Left Join tbl_OrderInvoice H On H.OrderInvoiceID=I.OrderInvoiceID " & _
                                                  " Inner Join tbl_OrderReceiveDetail D On I.OrderInvoiceID=D.OrderInvoiceID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  I.ReturnDate < @SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_OrderInvoice H  left Join tbl_CashReceipt  I  On H.OrderInvoiceID=I.VoucherNo " & _
                                                  " Inner Join tbl_OrderReturnHeader R On H.OrderInvoiceID=R.OrderInvoiceID " & _
                                                  " Inner Join tbl_OrderReceiveDetail D On H.OrderInvoiceID=D.OrderInvoiceID  " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And R.ReturnDate < @SFromDate " & cristr & _
                                                  " And I.Type='OrderInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                                                  " UNION ALL " & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From ( " & _
                                                  " SELECT  distinct H.SaleInvoiceHeaderID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount+H.PurchaseAmount+H.AllAdvanceAmount" & _
                                                  " +H.OtherCashAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SaleInvoiceHeader H  " & _
                                                  " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SaleDate <@SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_SaleInvoiceHeader H  left Join tbl_CashReceipt  I  On H.SaleInvoiceHeaderID=I.VoucherNo " & _
                                                  " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SaleDate <@SFromDate " & cristr & _
                                                  "  And I.Type='SalesInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                                                   " UNION ALL " & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From ( " & _
                                                  " SELECT  distinct H.SaleLooseDiamondID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount+H.PurchaseAmount" & _
                                                  " +H.OtherCashAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SaleLooseDiamondHeader H  " & _
                                                  " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SaleDate <@SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_SaleLooseDiamondHeader H  left Join tbl_CashReceipt  I  On H.SaleLooseDiamondID=I.VoucherNo " & _
                                                  " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SaleDate <@SFromDate " & cristr & _
                                                  "  And I.Type='SaleLooseDiamond' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                                                  " UNION ALL" & _
                                                  " select  SaleDate , CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From (" & _
                                                  " SELECT  distinct H.WholeSaleinvoiceID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.NetAmount+H.AddOrSub) -(H.Discount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_WholesaleInvoice H  " & _
                                                  " left Join tbl_WholesaleInvoiceItem D On D.WholesaleInvoiceID=H.WholesaleInvoiceID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And H.PayType<>1 And  H.WDate <@SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_WholesaleInvoice H  left Join tbl_CashReceipt  I  On H.WholeSaleInvoiceID=I.VoucherNo " & _
                                                  " Inner Join tbl_WholesaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.WDate <@SFromDate " & cristr & _
                                                  " And I.Type='WholeSaleInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                                                  " UNION ALL" & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From ( " & _
                                                  " SELECT distinct H.ConsignmentSaleID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) -(H.PaidAmount+H.PurchaseAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_ConsignmentSale H  " & _
                                                  " left Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.ConsignDate < @SFromDate " & cristr & _
                                                  " UNION ALL" & _
                                                  " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_ConsignmentSale H  left Join tbl_CashReceipt  I  On H.ConsignmentSaleID=I.VoucherNo " & _
                                                  " Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate And H.ConsignDate <@SFromDate " & cristr & _
                                                  " And I.Type='ConsignmentSaleInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                                                  " UNION ALL" & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From (" & _
                                                  " SELECT distinct H.SaleGemsID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-H.PaidAmount-H.PurchaseAmount) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SaleGems H  " & _
                                                  " left Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SDate <@SFromDate  And H.LocationID=" & LocationID & " And H.CustomerID='" & CustomerID & "' " & _
                                                  " UNION ALL " & _
                                                  " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_SaleGems H  left Join tbl_CashReceipt  I  On H.SaleGemsID=I.VoucherNo " & _
                                                  " Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SDate <@SFromDate  And H.LocationID=" & LocationID & " And H.CustomerID='" & CustomerID & "' " & _
                                                  " And I.Type='SalesGems' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                                                  " UNION ALL" & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From (" & _
                                                  " SELECT distinct H.SalesVolumeID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-H.PaidAmount-H.PurchaseAmount) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SalesVolume H  " & _
                                                  " left Join tbl_SalesVolumeDetail D On H.SalesVolumeID=D.SalesVolumeID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SaleDate <@SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_SalesVolume H  left Join tbl_CashReceipt  I  On H.SalesVolumeID=I.VoucherNo " & _
                                                  " Inner Join tbl_SalesVolumeDetail D On H.SalesVolumeID=D.SalesVolumeID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SaleDate <@SFromDate " & cristr & _
                                                  " And I.Type='SalesInvoiceVolume' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                                                  " UNION ALL" & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From (" & _
                                                  " SELECT  distinct I.RepairID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,(((I.AllReturnTotalAmount+I.AllReturnAddOrSub)-(I.ReturnDiscountAmount))-(I.ReturnPaidAmount" & _
                                                  " +(H.AdvanceRepairAmount)))  AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_ReturnRepairHeader  I  " & _
                                                  " Left Join tbl_RepairHeader  H On I.RepairID=H.RepairID " & _
                                                  " Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where I.IsDelete=0 And  I.ReturnDate <@SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_RepairHeader H  left Join tbl_CashReceipt  I  On H.RepairID=I.VoucherNo " & _
                                                  " Inner Join tbl_ReturnRepairHeader R On H.RepairID=R.RepairID " & _
                                                  " Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And R.ReturnDate <@SFromDate " & cristr & _
                                                  " And I.Type='RepairReturn' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress ) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "

                                DBComm = DB.GetSqlStringCommand(strDateCommand)
                                DBComm.CommandTimeout = 0
                                DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                                DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                                dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)

                                If dtOpening.Rows.Count > 0 Then
                                    dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")
                                Else
                                    dtTransaction.Rows(k).Item("CreditAmount") = 0
                                End If


                            Next
                        End If

                    Case "OrderInvoice"

                        strCommandText = " Select SaleDate,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(CreditAmount) as CreditAmount,Sum(ReceiveAmount) " & _
                                        " as ReceiveAmount from (  " & _
                                        " Select SaleDate,Title,VoucherNo,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(SaleAmount-CashAmount-ChangeAmount) as VCredit,Sum(NewCredit) as CreditAmount,Sum(CashCredit) as " & _
                                        " ReceiveAmount from ( select   SaleDate,Title,VoucherNo, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount,  " & _
                                        " Sum(CashAmount) AS CashAmount,  Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from (  " & _
                                        " SELECT 'OrderInvoice' AS Title, OrderReturnHeaderID AS VoucherNo, Convert(Date,I.ReturnDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,  " & _
                                        " ((I.AllTotalAmount+I.AllAddOrSub)-(I.DiscountAmount)-isnull((H.AdvanceAmount+SecondAdvanceAmount),0)) AS SaleAmount,  0 AS ChangeAmount, (I.PaidAmount)   " & _
                                        " AS CashAmount,  0  AS NewCredit,   0 As CashCredit,'' as Remark   From tbl_OrderReturnHeader  I   left join tbl_OrderInvoice H on H.OrderInvoiceID=I.OrderInvoiceID" & _
                                        " Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 " & _
                                        " And I.isDelete=0 AND I.ReturnDate BETWEEN @FromDate And @ToDate  " & cristr & _
                                        " UNION ALL  " & _
                                        " SELECT 'OrderInvoice' AS Title, H.OrderInvoiceID AS VoucherNo, Convert(Date,H.OrderDate) as " & _
                                        " SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,(H.AdvanceAmount +H.SecondAdvanceAmount)  AS SaleAmount,  0 AS ChangeAmount, " & _
                                        " (H.AdvanceAmount+H.SecondAdvanceAmount)  AS CashAmount, 0  AS NewCredit, 0 As CashCredit,  '' as Remark  From tbl_OrderInvoice  H   " & _
                                        " Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 " & _
                                        " And  H.OrderDate BETWEEN @FromDate And @ToDate  " & cristr & " ) As M  group by SaleDate, CustomerCode, " & _
                                        " CustomerName, CustomerAddress,Title,Voucherno  ) as F group By SaleDate,CustomerCode,Title,VoucherNo " & _
                                        " UNION ALL" & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate ,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from (" & _
                                        " SELECT distinct 'OrderInvoice' AS Title,CR.CashReceiptID AS VoucherNo,CR.PayDate as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,   " & _
                                        " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_OrderReturnHeader I   " & _
                                        " left join tbl_OrderInvoice H on H.OrderInvoiceID=I.OrderInvoiceID    Inner Join tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID  " & _
                                        " INNER join tbl_CashReceipt CR ON H.OrderInvoiceID=CR.VoucherNo inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  " & _
                                        " AND CR.IsDelete=0 and CR.PayDate  BETWEEN @FromDate And @ToDate  and CR.Type='OrderInvoice' " & cristr & " ) as M Group By Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime)" & _
                                        " ) as M   group by  VoucherNo, SaleDate ) As T  Where 1=1  group By SaleDate Order By SaleDate desc  "

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)
                        If dtTransaction.Rows.Count > 0 Then
                            For k As Integer = 0 To dtTransaction.Rows.Count - 1
                                ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                                OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                                strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From (" & _
                                                  "select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit " & _
                                                  " From ( SELECT  distinct H.OrderInvoiceID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,(((I.AllTotalAmount+I.AllAddOrSub)-(I.DiscountAmount))-(I.PaidAmount " & _
                                                  " +(H.AdvanceAmount+H.SecondAdvanceAmount)))  AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_OrderReturnHeader  I  " & _
                                                  " Left Join tbl_OrderInvoice H On H.OrderInvoiceID=I.OrderInvoiceID " & _
                                                  " Inner Join tbl_OrderReceiveDetail D On I.OrderInvoiceID=D.OrderInvoiceID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  I.ReturnDate <@SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_OrderInvoice H  left Join tbl_CashReceipt  I  On H.OrderInvoiceID=I.VoucherNo " & _
                                                  " Inner Join tbl_OrderReturnHeader R On H.OrderInvoiceID=R.OrderInvoiceID " & _
                                                  " Inner Join tbl_OrderReceiveDetail D On H.OrderInvoiceID=D.OrderInvoiceID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And R.ReturnDate <@SFromDate " & cristr & _
                                                  " And I.Type='OrderInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                                DBComm = DB.GetSqlStringCommand(strDateCommand)
                                DBComm.CommandTimeout = 0
                                DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                                DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                                dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)

                                If dtOpening.Rows.Count > 0 Then
                                    dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")
                                Else
                                    dtTransaction.Rows(k).Item("CreditAmount") = 0
                                End If

                            Next
                        End If

                    Case "RepairReturn"

                        'strCommandText = "  Select SaleDate,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(CreditAmount) as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from (" & _
                        '                 "  Select SaleDate,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(NewCredit) as CreditAmount,Sum(CashCredit) as ReceiveAmount from (" & _
                        '                " select   SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                " Sum(CashAmount) AS CashAmount,  Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from ( " & _
                        '                " SELECT 'RepairInvoice' AS Title, ReturnRepairID AS VoucherNo, Convert(Date,I.ReturnDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                        '                " ((I.AllReturnTotalAmount+I.AllReturnAddOrSub)-(I.ReturnDiscountAmount)-isnull((H.AdvanceRepairAmount),0)) AS SaleAmount,  0 AS ChangeAmount, (I.ReturnPaidAmount)  " & _
                        '                "  AS CashAmount,  0  AS NewCredit,  " & _
                        '                "  0 As CashCredit,'' as Remark   From tbl_ReturnRepairHeader  I  " & _
                        '                " Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID " & _
                        '                 " left join tbl_RepairHeader H on H.RepairID=I.RepairID  " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isDelete=0 AND I.ReturnDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                 " Union all  " & _
                        '                 " SELECT distinct 'RepairInvoice' AS Title,CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,  " & _
                        '                 " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_ReturnRepairHeader I   " & _
                        '                 " Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID " & _
                        '                " left join tbl_RepairHeader H on H.RepairID=I.RepairID   " & _
                        '                "  INNER join tbl_CashReceipt CR ON H.RepairID=CR.VoucherNo    " & _
                        '                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  AND CR.IsDelete=0 and CR.PayDate " & _
                        '                "  BETWEEN @FromDate And @ToDate  and CR.Type='RepairReturn'" & cristr & " ) As M  group by  VoucherNo, SaleDate, " & _
                        '                "  CustomerCode, CustomerName, CustomerAddress " & _
                        '                " UNION ALL  " & _
                        '                "  select  SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                "  Sum(CashAmount) AS CashAmount,  Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit   from ( " & _
                        '                "  SELECT 'RepairInvoice' AS Title, H.RepairID AS VoucherNo, Convert(Date,H.RepairDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,(H.AdvanceRepairAmount)  AS SaleAmount, " & _
                        '                " 0 AS ChangeAmount, (H.AdvanceRepairAmount)  AS CashAmount, 0  AS NewCredit, 0 As CashCredit, " & _
                        '                " '' as Remark  From tbl_RepairHeader  H  " & _
                        '                " Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID " & _
                        '                " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.RepairDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                " ) As M  group by SaleDate, CustomerCode, CustomerName, CustomerAddress " & _
                        '                " ) as F group By SaleDate,CustomerCode ) As T  Where 1=1  group By SaleDate Order By SaleDate desc "

                        strCommandText = "Select SaleDate,Title,VoucherNo, SaleAmount, ChangeAmount,CashAmount,VCredit, CreditAmount, ReceiveAmount from (  " & _
                                        " select   SaleDate,Title,VoucherNo, sum(SaleAmount) AS SaleAmount ,Sum(ChangeAmount) AS ChangeAmount," & _
                                        " Sum(CashAmount) AS CashAmount,Sum(SaleAmount-CashAmount-ChangeAmount) as VCredit,  Sum(NewCredit) AS CreditAmount, Sum(CashCredit) As ReceiveAmount   from ( " & _
                                        " SELECT 'RepairInvoice' AS Title, ReturnRepairID AS VoucherNo, Convert(Date,I.ReturnDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress," & _
                                        " ((I.AllReturnTotalAmount+I.AllReturnAddOrSub)-(I.ReturnDiscountAmount)-isnull((H.AdvanceRepairAmount),0)) AS SaleAmount,  0 AS ChangeAmount, (I.ReturnPaidAmount)  " & _
                                        " AS CashAmount,  0  AS NewCredit,    0 As CashCredit,'' as Remark   From tbl_ReturnRepairHeader  I   Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID  " & _
                                        " left join tbl_RepairHeader H on H.RepairID=I.RepairID   inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isDelete=0 " & _
                                        " AND I.ReturnDate BETWEEN @FromDate And @ToDate " & cristr & _
                                        " Union all   " & _
                                        " SELECT 'RepairInvoice' AS Title, H.RepairID AS VoucherNo, Convert(Date,H.RepairDate) as " & _
                                        " SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,(H.AdvanceRepairAmount)  AS SaleAmount,  0 AS ChangeAmount, (H.AdvanceRepairAmount)  AS CashAmount, 0  " & _
                                        " AS NewCredit, 0 As CashCredit,  '' as Remark  From tbl_RepairHeader  H   Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID  inner join tbl_Customer C " & _
                                        " ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.RepairDate BETWEEN @FromDate And @ToDate " & cristr & _
                                        "  ) As M  group by SaleDate,Title,VoucherNo" & _
                                        " UNION ALL" & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " SELECT distinct 'RepairInvoice' AS Title,CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress,   " & _
                                        " 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_ReturnRepairHeader I    " & _
                                        " Inner Join tbl_RepairDetail D On I.RepairID=D.RepairID  left join tbl_RepairHeader H on H.RepairID=I.RepairID     " & _
                                        " INNER join tbl_CashReceipt CR ON H.RepairID=CR.VoucherNo     inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 And I.isDelete=0  " & _
                                        " AND CR.IsDelete=0 and CR.PayDate   BETWEEN @FromDate And @ToDate  and CR.Type='RepairReturn' " & cristr & " ) as M " & _
                                        " group By VoucherNo, Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime), CustomerCode, CustomerName,CustomerAddress " & _
                                        " ) as M   group by  VoucherNo, SaleDate ) As M ORDER By SaleDate"

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)

                        For k As Integer = 0 To dtTransaction.Rows.Count - 1
                            ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                            OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                            strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From (" & _
                                              " UNION ALL select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                              " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                              " From (" & _
                                              " SELECT  distinct I.RepairID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,(((I.AllReturnTotalAmount+I.AllReturnAddOrSub)-(I.ReturnDiscountAmount))-(I.ReturnPaidAmount" & _
                                              " +(H.AdvanceRepairAmount)))  AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_ReturnRepairHeader  I  " & _
                                              " Left Join tbl_RepairHeader  H On I.RepairID=H.RepairID " & _
                                              " Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID " & _
                                              " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where I.IsDelete=0 And  I.ReturnDate <@SFromDate " & cristr & _
                                              " UNION ALL " & _
                                              " SELECT distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                              " From tbl_RepairHeader H  left Join tbl_CashReceipt  I  On H.RepairID=I.VoucherNo " & _
                                              " Inner Join tbl_ReturnRepairHeader R On H.RepairID=R.RepairID " & _
                                              " Inner Join tbl_RepairDetail D On H.RepairID=D.RepairID " & _
                                              " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And R.ReturnDate <@SFromDate " & cristr & _
                                              " And I.Type='RepairReturn' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                            DBComm = DB.GetSqlStringCommand(strDateCommand)
                            DBComm.CommandTimeout = 0
                            DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                            DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                            dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)

                            dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")

                        Next

                    Case "WholeSaleInvoice"

                        strCommandText = "Select SaleDate,Title,VoucherNo, SaleAmount, ChangeAmount,CashAmount,VCredit, CreditAmount, ReceiveAmount from ( " & _
                                        " select SaleDate,Title,VoucherNo, SaleAmount AS SaleAmount ,ChangeAmount AS ChangeAmount,CashAmount AS CashAmount,SaleAmount-CashAmount-ChangeAmount as VCredit,NewCredit AS CreditAmount,CashCredit As ReceiveAmount   from  ( " & _
                                        " Select distinct 'WholeSalesInvoice' AS Title,H.WholeSaleInvoiceID AS VoucherNo, Convert(Date,H.WDate) as SaleDate, C.CustomerCode, C.CustomerName,  " & _
                                        " C.CustomerAddress,  ((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, 0 AS ChangeAmount, H.PaidAmount  AS CashAmount,0  AS NewCredit,  0 As CashCredit," & _
                                        " '' as Remark  From tbl_WholesaleInvoice H   Inner Join tbl_WholeSaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID  " & _
                                        " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.PayType<>1 and H.IsDelete=0 AND H.WDate BETWEEN @FromDate And @ToDate " & cristr & _
                                        " ) as M" & _
                                        " Union all   " & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " SELECT distinct 'WholeSalesInvoice' AS Title, CR.CashReceiptID AS VoucherNo, Convert(DAte,CR.PayDate) as SaleDate, C.CustomerCode, C.CustomerName, " & _
                                        " C.CustomerAddress, 0 AS SaleAmount,  0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount As CashCredit, '' as Remark  From tbl_WholesaleInvoice H   " & _
                                        " Inner Join tbl_WholeSaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID  INNER join tbl_CashReceipt CR ON H.WholesaleInvoiceID=CR.VoucherNo " & _
                                        " inner join tbl_Customer C  ON H.CustomerID=C.CustomerID  where H.PayType<>1 and H.IsDelete=0  AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate " & _
                                        " and CR.Type='WholeSalesInvoice'  " & cristr & " ) As M  " & _
                                        " group by  VoucherNo, Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime), CustomerCode, CustomerName, CustomerAddress  ) as M   group by  VoucherNo, SaleDate ) as F Order By SaleDate"

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)

                        If dtTransaction.Rows.Count > 0 Then
                            For k As Integer = 0 To dtTransaction.Rows.Count - 1
                                ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                                OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                                strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From ( " & _
                                                  " select  SaleDate , CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From (" & _
                                                  " SELECT  distinct H.WholeSaleinvoiceID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.NetAmount+H.AddOrSub) -(H.Discount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_WholesaleInvoice H  " & _
                                                  " left Join tbl_WholesaleInvoiceItem D On D.WholesaleInvoiceID=H.WholesaleInvoiceID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And H.PayType<>1 And  H.WDate <@SFromDate " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_WholesaleInvoice H  left Join tbl_CashReceipt  I  On H.WholeSaleInvoiceID=I.VoucherNo " & _
                                                  " Inner Join tbl_WholesaleInvoiceItem D On H.WholeSaleInvoiceID=D.WholeSaleInvoiceID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.WDate <@SFromDate " & cristr & _
                                                  " And I.Type='WholeSaleInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress ) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                                DBComm = DB.GetSqlStringCommand(strDateCommand)
                                DBComm.CommandTimeout = 0
                                DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                                DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                                dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)
                                If dtOpening.Rows.Count > 0 Then
                                    dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")
                                Else
                                    dtTransaction.Rows(k).Item("CreditAmount") = 0
                                End If

                            Next
                        End If
                    Case "SaleInvoice"


                        'strCommandText = "  Select SaleDate,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(CreditAmount) as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from (" & _
                        '                 "  Select SaleDate,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(NewCredit) as CreditAmount,Sum(CashCredit) as ReceiveAmount from (" & _
                        '                 " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, sum(SaleAmount) AS SaleAmount , Sum(ChangeAmount) AS ChangeAmount, " & _
                        '                 " Sum(CashAmount) AS CashAmount, Sum(NewCredit) AS NewCredit, Sum(CashCredit) As CashCredit  from ( " & _
                        '                 " select distinct H.SaleInvoiceHeaderID AS VoucherNo, Convert(Date,H.SaleDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub) " & _
                        '                 " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, PurchaseAmount AS ChangeAmount, CASE WHEN H.PaidAmount<0 THEN " & _
                        '                 " (AllAdvanceAmount+OtherCashAmount) ELSE (H.PaidAmount+AllAdvanceAmount+OtherCashAmount) END AS CashAmount,0 As NewCredit, 0 As CashCredit, H.Remark  From tbl_SaleInvoiceHeader H " & _
                        '                 " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                        '                 " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate  " & cristr & _
                        '                 " Union all " & _
                        '                 " select distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  " & _
                        '                 " 0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SaleInvoiceHeader H " & _
                        '                 " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                        '                 " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                        '                 " INNER join tbl_CashReceipt CR ON H.SaleInvoiceHeaderID=CR.VoucherNo  " & _
                        '                 " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate " & _
                        '                 " AND @ToDate and CR.Type='SalesInvoice' " & cristr & ") As M group by  VoucherNo, SaleDate, CustomerCode, CustomerName,CustomerAddress " & _
                        '                 " ) as F group By SaleDate,CustomerCode ) As T  Where 1=1  group By SaleDate Order By SaleDate desc "

                        strCommandText = "Select SaleDate,Title,VoucherNo, SaleAmount, ChangeAmount,CashAmount,VCredit, CreditAmount, ReceiveAmount from (  " & _
                                        " Select SaleDate,Title,VoucherNo,Sum(SaleAmount) as SaleAmount,Sum(ChangeAmount) as ChangeAmount,Sum(CashAmount) As CashAmount,Sum(SaleAmount-CashAmount-ChangeAmount) as VCredit,Sum(NewCredit) as CreditAmount,Sum(CashCredit) as " & _
                                        " ReceiveAmount from ( " & _
                                        " Select distinct 'SaleInvoice' as title,H.SaleInvoiceHeaderID AS VoucherNo, Convert(Date,H.SaleDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                                        " ((H.TotalAmount+H.AddOrSub)  -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, PurchaseAmount AS ChangeAmount, CASE WHEN H.PaidAmount<0 " & _
                                        " THEN  (AllAdvanceAmount+OtherCashAmount) ELSE (H.PaidAmount+AllAdvanceAmount+OtherCashAmount) END AS CashAmount,0 As NewCredit, 0 As CashCredit, H.Remark  " & _
                                        " From tbl_SaleInvoiceHeader H  Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID  " & _
                                        " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate " & _
                                        " BETWEEN @FromDate And @ToDate   " & cristr & " ) as M group By SaleDate,VoucherNo,Title, CustomerCode, CustomerName, CustomerAddress" & _
                                        " Union all  " & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " select distinct CR.CashReceiptID AS VoucherNo, CR.PayDate As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                        " 0 AS ChangeAmount,   0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SaleInvoiceHeader H  " & _
                                        " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID  Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID  " & _
                                        " INNER join tbl_CashReceipt CR ON H.SaleInvoiceHeaderID=CR.VoucherNo   inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 " & _
                                        " AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesInvoice'  " & cristr & " ) As M " & _
                                        " group by  VoucherNo, Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime), CustomerCode, CustomerName,CustomerAddress) as M   group by  VoucherNo, SaleDate " & _
                                        " ) as F order by SaleDate "

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)

                        If dtTransaction.Rows.Count > 0 Then
                            For k As Integer = 0 To dtTransaction.Rows.Count - 1
                                ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                                OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                                strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From ( " & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From ( " & _
                                                  " SELECT  distinct H.SaleInvoiceHeaderID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount+H.PurchaseAmount+H.AllAdvanceAmount" & _
                                                  " +H.OtherCashAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SaleInvoiceHeader H  " & _
                                                  " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SaleDate < @SFromDate  " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_SaleInvoiceHeader H  left Join tbl_CashReceipt  I  On H.SaleInvoiceHeaderID=I.VoucherNo " & _
                                                  " Inner Join tbl_SaleInvoiceDetail F On H.SaleInvoiceHeaderID=F.SaleInvoiceHeaderID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SaleDate <@SFromDate " & cristr & _
                                                  "  And I.Type='SalesInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress ) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                                DBComm = DB.GetSqlStringCommand(strDateCommand)
                                DBComm.CommandTimeout = 0
                                DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                                DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                                dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)
                                If dtOpening.Rows.Count > 0 Then
                                    dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")
                                Else
                                    dtTransaction.Rows(k).Item("CreditAmount") = 0
                                End If

                            Next
                        End If

                    Case "SaleGems"
                        'While (VarDate <= TDate)
                        '    ChangeDate = Format(VarDate.Date, "yyyy-MM-dd")
                        '    strDateCommand += " UNION ALL select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                        '                      " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                        '                      " From (" & _
                        '                      " SELECT distinct H.SaleGemsID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                        '                      " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-H.PaidAmount-H.PurchaseAmount) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SaleGems H  " & _
                        '                      " left Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                        '                      " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SDate <= @LFromDate" & i & " And H.LocationID=" & LocationID & " And H.CustomerID='" & CustomerID & "' " & _
                        '                      " UNION ALL " & _
                        '                      " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                        '                      " From tbl_SaleGems H  left Join tbl_CashReceipt  I  On H.SaleGemsID=I.VoucherNo " & _
                        '                      " Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                        '                      " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate" & i & " And H.SDate <= @LFromDate" & i & " And H.LocationID=" & LocationID & " And H.CustomerID='" & CustomerID & "' " & _
                        '                      " And I.Type='SalesGems' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                        '                      " Union All " & _
                        '                      " select '" & ChangeDate & "' as SaleDate," & CustomerCode & " as CustomerCode,'' as customerName,'' CustomerAddress,0 As SaleAmont,0 As ChangeAmount,0 As CashAmount,0 As NewCredit,0 as CashCredit "
                        '    i += 1
                        '    VarDate = DateAdd(DateInterval.Day, 1, VarDate)
                        'End While
                        strCommandText = "  Select SaleDate,Title,VoucherNo, SaleAmount, ChangeAmount,CashAmount,VCredit, CreditAmount, ReceiveAmount from (   select  SaleDate,Title,VoucherNo,sum(SaleAmount) AS SaleAmount, " & _
                                        " sum(ChangeAmount) AS ChangeAmount,  Sum(CashAmount) AS CashAmount,Sum(SaleAmount-CashAmount-ChangeAmount) as VCredit, sum(NewCredit) As CreditAmount, Sum(CashCredit) As ReceiveAmount from (  " & _
                                        " SELECT distinct H.SaleGemsID AS VoucherNo,'SaleGems' as Title, Convert(Date,H.SDate) AS SaleDate, C.CustomerCode,  C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub) " & _
                                        " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) AS SaleAmount, H.PurchaseAmount AS ChangeAmount,  H.PaidAmount AS CashAmount,  0 AS NewCredit, 0 As " & _
                                        " CashCredit, H.Remark  From tbl_SaleGems H  Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID   " & _
                                        " where H.IsDelete=0 AND H.SDate BETWEEN @FromDate And @ToDate " & cristr & " ) as M group by SaleDate,Title,VoucherNo" & _
                                        " Union all  " & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " SELECT distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) AS SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount,  " & _
                                        " 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit, CR.PayAmount As CashCredit, H.Remark   From tbl_SaleGems H  " & _
                                        " Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID  INNER join  tbl_CashReceipt CR ON CR.VoucherNo=H.SaleGemsID " & _
                                        " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesGems' " & _
                                        " " & cristr & "  ) As M  group by Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime), CustomerCode, CustomerName, CustomerAddress " & _
                                        " ) as M   group by  VoucherNo, SaleDate)as M Order By SaleDate "

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)

                        If dtTransaction.Rows.Count > 0 Then
                            For k As Integer = 0 To dtTransaction.Rows.Count - 1
                                ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                                OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                                strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From ( " & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From (" & _
                                                  " SELECT distinct H.SaleGemsID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-H.PaidAmount-H.PurchaseAmount) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SaleGems H  " & _
                                                  " left Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SDate <@SFromDate And H.LocationID=" & LocationID & " And H.CustomerID='" & CustomerID & "' " & _
                                                  " UNION ALL " & _
                                                  " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_SaleGems H  left Join tbl_CashReceipt  I  On H.SaleGemsID=I.VoucherNo " & _
                                                  " Inner Join tbl_SaleGemsItem D On H.SaleGemsID=D.SaleGemsID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SDate <@SFromDateAnd H.LocationID=" & LocationID & " And H.CustomerID='" & CustomerID & "' " & _
                                                  " And I.Type='SalesGems' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress ) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                                DBComm = DB.GetSqlStringCommand(strDateCommand)
                                DBComm.CommandTimeout = 0
                                DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                                DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                                dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)

                                If dtOpening.Rows.Count > 0 Then
                                    dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")
                                Else
                                    dtTransaction.Rows(k).Item("CreditAmount") = 0
                                End If

                            Next
                        End If
                    Case "SalesVolume"
                        'While (VarDate <= TDate)
                        '    ChangeDate = Format(VarDate.Date, "yyyy-MM-dd")
                        '    strDateCommand += " UNION ALL select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                        '                      " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                        '                      " From (" & _
                        '                      " SELECT distinct H.SalesVolumeID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                        '                      " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-H.PaidAmount-H.PurchaseAmount) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SalesVolume H  " & _
                        '                      " left Join tbl_SalesVolumeDetail D On H.SalesVolumeID=D.SalesVolumeID " & _
                        '                      " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SaleDate <= @LFromDate" & i & " " & cristr & _
                        '                      " UNION ALL " & _
                        '                      " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                        '                      " From tbl_SalesVolume H  left Join tbl_CashReceipt  I  On H.SalesVolumeID=I.VoucherNo " & _
                        '                      " Inner Join tbl_SalesVolumeDetail D On H.SalesVolumeID=D.SalesVolumeID " & _
                        '                      " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate" & i & " And H.SaleDate <= @LFromDate" & i & " " & cristr & _
                        '                      " And I.Type='SalesInvoiceVolume' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                        '                      " Union All " & _
                        '                      " select '" & ChangeDate & "' as SaleDate," & CustomerCode & " as CustomerCode,'' as customerName,'' CustomerAddress,0 As SaleAmont,0 As ChangeAmount,0 As CashAmount,0 As NewCredit,0 as CashCredit "
                        '    i += 1
                        '    VarDate = DateAdd(DateInterval.Day, 1, VarDate)
                        'End While
                        strCommandText = " Select SaleDate,Title,VoucherNo, SaleAmount, ChangeAmount,CashAmount,VCredit, CreditAmount, ReceiveAmount from (  " & _
                                        " select SaleDate,Title,VoucherNo, SaleAmount AS SaleAmount ,ChangeAmount AS ChangeAmount,CashAmount AS CashAmount,SaleAmount-CashAmount-ChangeAmount as VCredit,NewCredit AS CreditAmount,CashCredit As ReceiveAmount   from  ( " & _
                                        " select H.SalesVolumeID AS VoucherNo,'SaleVolume' as Title, Convert(Date,H.SaleDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, ((H.TotalAmount+H.AddOrSub) " & _
                                        " -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, isnull(PurchaseAmount,0) AS ChangeAmount,H.PaidAmount AS CashAmount,0 As NewCredit, 0 As " & _
                                        " CashCredit  From tbl_SalesVolume  H  Inner Join tbl_SalesVolumeDetail D on H.SalesVolumeID=D.SalesVolumeID " & _
                                        " inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate  " & cristr & _
                                        " ) as M " & _
                                        " Union all  " & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " select distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                        " 0 AS ChangeAmount,   0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SalesVolume H  Inner Join tbl_SalesVolumeDetail D on " & _
                                        " H.SalesVolumeID=D.SalesVolumeID INNER join tbl_CashReceipt CR ON H.SalesVolumeID=CR.VoucherNo   inner join tbl_Customer C ON H.CustomerID=C.CustomerID  " & _
                                        " where H.IsDelete=0 AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SalesInvoiceVolume'  " & cristr & _
                                        " ) As M group by  VoucherNo, Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime), CustomerCode, CustomerName,CustomerAddress  ) as M   group by  VoucherNo, SaleDate) As " & _
                                        " T  Order By SaleDate  "

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)

                        For k As Integer = 0 To dtTransaction.Rows.Count - 1
                            ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                            OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                            strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From ( " & _
                                              " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                              " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                              " From (" & _
                                              " SELECT distinct H.SalesVolumeID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                              " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-H.PaidAmount-H.PurchaseAmount) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SalesVolume H  " & _
                                              " left Join tbl_SalesVolumeDetail D On H.SalesVolumeID=D.SalesVolumeID " & _
                                              " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SaleDate <@SFromDate  " & cristr & _
                                              " UNION ALL " & _
                                              " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                              " From tbl_SalesVolume H  left Join tbl_CashReceipt  I  On H.SalesVolumeID=I.VoucherNo " & _
                                              " Inner Join tbl_SalesVolumeDetail D On H.SalesVolumeID=D.SalesVolumeID " & _
                                              " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SaleDate <@SFromDate " & cristr & _
                                              " And I.Type='SalesInvoiceVolume' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress ) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                            DBComm = DB.GetSqlStringCommand(strDateCommand)
                            DBComm.CommandTimeout = 0
                            DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                            DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                            dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)

                            dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")

                        Next

                    Case "ConsignmentSaleInvoice"
                        'While (VarDate <= TDate)
                        '    ChangeDate = Format(VarDate.Date, "yyyy-MM-dd")
                        '    strDateCommand += " UNION ALL select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                        '                      " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                        '                      " From ( " & _
                        '                      " SELECT distinct H.ConsignmentSaleID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                        '                      " (((H.NetAmount+H.AddOrSub)-(H.Discount)) -(H.PaidAmount+H.PurchaseAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_ConsignmentSale H  " & _
                        '                      " left Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                        '                      " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.ConsignDate <= @LFromDate" & i & " " & cristr & _
                        '                      " UNION ALL" & _
                        '                      " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                        '                      " From tbl_ConsignmentSale H  left Join tbl_CashReceipt  I  On H.ConsignmentSaleID=I.VoucherNo " & _
                        '                      " Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                        '                      " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate" & i & " And H.ConsignDate <= @LFromDate" & i & " " & cristr & _
                        '                      " And I.Type='ConsignmentSaleInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress" & _
                        '                      " Union All " & _
                        '                      " select '" & ChangeDate & "' as SaleDate," & CustomerCode & " as CustomerCode,'' as customerName,'' CustomerAddress,0 As SaleAmont,0 As ChangeAmount,0 As CashAmount,0 As NewCredit,0 as CashCredit "
                        '    i += 1
                        '    VarDate = DateAdd(DateInterval.Day, 1, VarDate)
                        'End While
                        strCommandText = " Select SaleDate,Title,VoucherNo, SaleAmount, ChangeAmount,CashAmount,VCredit, CreditAmount, ReceiveAmount from (  " & _
                                        " select SaleDate,Title,VoucherNo, SaleAmount AS SaleAmount ,ChangeAmount AS ChangeAmount,CashAmount AS CashAmount,SaleAmount-CashAmount-ChangeAmount as VCredit,NewCredit AS CreditAmount,CashCredit As ReceiveAmount   from  ( " & _
                                        " Select distinct 'ConsignmentSaleInvoice' AS Title, H.ConsignmentSaleID AS VoucherNo, Convert(Date,H.ConsignDate) as SaleDate, C.CustomerCode, C.CustomerName, " & _
                                        " C.CustomerAddress,  ((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount,  H.PurchaseAmount AS ChangeAmount, H.PaidAmount  AS CashAmount, 0  AS NewCredit, 0 As " & _
                                        " CashCredit,'' as Remark   From tbl_ConsignmentSale H  Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID  inner join tbl_Customer C " & _
                                        " ON H.CustomerID=C.CustomerID   where H.IsDelete=0 AND H.ConsignDate BETWEEN @FromDate And @ToDate  " & cristr & _
                                        "  ) as M" & _
                                        " Union all   " & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " SELECT 'ConsignmentSaleInvoice' AS Title, H.ConsignmentSaleID AS VoucherNo, Convert(Date,CR.PayDate) as " & _
                                        " SaleDate, C.CustomerCode,  C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, 0 AS ChangeAmount,  0 AS CashAmount, 0 AS NewCredit,CR.PayAmount  As CashCredit " & _
                                        " From tbl_ConsignmentSale H   Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID  " & _
                                        " INNER join tbl_CashReceipt CR ON H.ConsignmentSaleID=CR.VoucherNo   inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 " & _
                                        " AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='ConsignmentSaleInvoice'  " & cristr & _
                                        " ) As M  group by  VoucherNo, Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime), CustomerCode,  CustomerName, CustomerAddress ) as M   group by  VoucherNo, SaleDate  ) as F Order By SaleDate"

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)

                        For k As Integer = 0 To dtTransaction.Rows.Count - 1
                            ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                            OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                            strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From ( " & _
                                              " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                              " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                              " From ( " & _
                                              " SELECT distinct H.ConsignmentSaleID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                              " (((H.NetAmount+H.AddOrSub)-(H.Discount+H.RedeemValue+H.MemberDiscountAmt)) -(H.PaidAmount+H.PurchaseAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_ConsignmentSale H  " & _
                                              " left Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                                              " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.ConsignDate <@SFromDate " & cristr & _
                                              " UNION ALL" & _
                                              " SELECT  distinct I.CashReceiptID,'" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                              " From tbl_ConsignmentSale H  left Join tbl_CashReceipt  I  On H.ConsignmentSaleID=I.VoucherNo " & _
                                              " Inner Join tbl_ConsignmentSaleItem D On H.ConsignmentSaleID=D.ConsignmentSaleID " & _
                                              " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.ConsignDate <@SFromDate " & cristr & _
                                              " And I.Type='ConsignmentSaleInvoice' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress ) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                            DBComm = DB.GetSqlStringCommand(strDateCommand)
                            DBComm.CommandTimeout = 0
                            DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                            DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                            dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)

                            dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")

                        Next

                    Case "SaleLooseDiamond"

                        strCommandText = "Select SaleDate,Title,VoucherNo, SaleAmount, ChangeAmount,CashAmount,VCredit, CreditAmount, ReceiveAmount from (" & _
                                         " select SaleDate,Title,VoucherNo, SaleAmount AS SaleAmount ,ChangeAmount AS ChangeAmount,CashAmount AS CashAmount,SaleAmount-CashAmount-ChangeAmount as VCredit,NewCredit AS CreditAmount,CashCredit As ReceiveAmount   from  ( " & _
                                        " select distinct H.SaleLooseDiamondID AS VoucherNo,'SaleLooseDiamond' as Title, Convert(Date,H.SaleDate) as SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, " & _
                                        " ((H.TotalAmount+H.AddOrSub)  -(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt)) AS SaleAmount, PurchaseAmount AS ChangeAmount, " & _
                                        " CASE WHEN H.PaidAmount<0 THEN  (OtherCashAmount) ELSE (H.PaidAmount+OtherCashAmount) END AS CashAmount,0 As NewCredit, 0 As CashCredit, H.Remark  " & _
                                        " From tbl_SaleLooseDiamondHeader H  Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID  " & _
                                        " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID  inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 AND H.SaleDate " & _
                                        " BETWEEN @FromDate And @ToDate   " & cristr & " ) as M" & _
                                        " Union all" & _
                                        " Select SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount," & _
                                        " 0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(ReceiveAmount) as ReceiveAmount from(" & _
                                        " Select Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime) as SaleDate,'-' as Title,'-' as VoucherNo,0 as SaleAmount,0 as ChangeAmount,0 as CashAmount,0 as VCredit,0 as CreditAmount,Sum(CashCredit) as ReceiveAmount from ( " & _
                                        " select distinct CR.CashReceiptID AS VoucherNo, Convert(Date,CR.PayDate) As SaleDate, C.CustomerCode, C.CustomerName, C.CustomerAddress, 0 AS SaleAmount, " & _
                                        " 0 AS ChangeAmount,   0 AS CashAmount, 0 AS NewCredit,  CR.PayAmount CashCredit, H.Remark  From tbl_SaleLooseDiamondHeader H  " & _
                                        " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID  Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID  " & _
                                        " INNER join tbl_CashReceipt CR ON H.SaleLooseDiamondID=CR.VoucherNo   inner join tbl_Customer C ON H.CustomerID=C.CustomerID  where H.IsDelete=0 " & _
                                        " AND CR.IsDelete=0 and CR.PayDate BETWEEN @FromDate And @ToDate and CR.Type='SaleLooseDiamond' " & cristr & _
                                        " ) As M group by  VoucherNo, Cast((Convert(Date,SaleDate)) as datetime)+ Cast('23:10:00' as datetime), CustomerCode, CustomerName,CustomerAddress ) as M   group by  VoucherNo, SaleDate  ) as F Order By SaleDate "

                        DBComm = DB.GetSqlStringCommand(strCommandText)
                        DBComm.CommandTimeout = 0
                        DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                        DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                        dtTransaction = DB.ExecuteDataSet(DBComm).Tables(0)
                        If dtTransaction.Rows.Count > 0 Then
                            For k As Integer = 0 To dtTransaction.Rows.Count - 1
                                ChangeDate = Format(dtTransaction.Rows(k).Item("SaleDate"), "yyyy-MM-dd")
                                OpeningDate = dtTransaction.Rows(k).Item("SaleDate")

                                strDateCommand += "select SaleDate,CustomerCode,CustomerName,CustomerAddress,Sum(NewCredit) as NewCredit From ( " & _
                                                  " select  SaleDate, CustomerCode, CustomerName, CustomerAddress, 0 AS SaleAmount ,0 AS ChangeAmount, " & _
                                                  " 0 AS CashAmount,  Sum(AllCredit-PayAmount) AS NewCredit, 0 As CashCredit" & _
                                                  " From ( " & _
                                                  " SELECT  distinct H.SaleLooseDiamondID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark," & _
                                                  " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-(H.PaidAmount+H.PurchaseAmount" & _
                                                  " +H.OtherCashAmount)) AS AllCredit,0 As PayAmount,0 As NewCredit,0 as CashCredit From tbl_SaleLooseDiamondHeader H  " & _
                                                  " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " inner join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And  H.SaleDate <@SFromDate  " & cristr & _
                                                  " UNION ALL " & _
                                                  " SELECT distinct I.CashReceiptID, '" & ChangeDate & "' as SaleDate, C.CustomerCode,CustomerName,CustomerAddress,0 aS SaleAmount,0 As ChangeAmount,0 As CashAmount,'' as Remark,0  AS AllCredit,PayAmount as PayAmount ,0 As NewCredit,0 as CashCredit" & _
                                                  " From tbl_SaleLooseDiamondHeader H  left Join tbl_CashReceipt  I  On H.SaleLooseDiamondID=I.VoucherNo " & _
                                                  " Inner Join tbl_SaleLooseDiamondDetail F On H.SaleLooseDiamondID=F.SaleLooseDiamondID " & _
                                                  " Inner Join tbl_forsale D On D.ForSaleID=F.ForSaleID " & _
                                                  " left join tbl_Customer C ON H.CustomerID=C.CustomerID   where H.IsDelete=0 And I.isdelete=0 And I.PayDate< @SFromDate  And H.SaleDate <@SFromDate " & cristr & _
                                                  "  And I.Type='SaleLooseDiamond' ) As M group By SaleDate,CustomerCode,CustomerName,CustomerAddress ) As N group By SaleDate,CustomerCode,CustomerName,CustomerAddress "


                                DBComm = DB.GetSqlStringCommand(strDateCommand)
                                DBComm.CommandTimeout = 0
                                DB.AddInParameter(DBComm, "@SFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 00:00:00"))
                                DB.AddInParameter(DBComm, "@LFromDate", DbType.DateTime, CDate(OpeningDate.Date & " 23:59:59"))
                                dtOpening = DB.ExecuteDataSet(DBComm).Tables(0)
                                If dtOpening.Rows.Count > 0 Then
                                    dtTransaction.Rows(k).Item("CreditAmount") = dtOpening.Rows(0).Item("NewCredit")
                                Else
                                    dtTransaction.Rows(k).Item("CreditAmount") = 0
                                End If

                            Next
                        End If
                End Select

                Dim iterateIndex As Integer = 0
                Dim newDataTable As DataTable = dtTransaction.Copy
                If str = "Nill" Then
                    For Each row As DataRow In newDataTable.Rows
                        If row("CreditAmount") - row("ReceiveAmount") <> 0 Then
                            dtTransaction.Rows.RemoveAt(iterateIndex)
                        Else
                            iterateIndex += 1
                        End If
                    Next
                ElseIf str = "Not Nill" Then
                    For Each row As DataRow In newDataTable.Rows
                        If row("CreditAmount") - row("ReceiveAmount") = 0 Then
                            dtTransaction.Rows.RemoveAt(iterateIndex)
                        Else
                            iterateIndex += 1
                        End If
                    Next
                End If

                Return dtTransaction
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

    End Class
End Namespace
