Namespace GeneralLedgerByLocation
    Public Interface IGeneralLedgerByLocationDA
        Function GetGeneralLedgerByLocationByDateFromTransaction(ByVal cristr As String, ByVal ForDate As Date, ByVal ToDate As Date) As DataTable
        Function GetGeneralLedgerByLocationByDate(ByVal LocationID As String, ByVal ForDate As Date) As DataTable
        Function InsertGeneralLedgerForLocationByDate(ByVal LocationID As String, ByVal GLDate As DateTime, ByVal StaffID As String, ByVal Title As String, ByVal DebitAmount As Long, ByVal CreditAmount As Long, ByVal Type As String, ByVal MyanTitle As String, ModifiedDate As Date, GLByLocationID As String) As Boolean
        Function DeleteGeneralLedgerForLocationByDate(ByVal LocationID As String, ByVal GLDate As Date) As Boolean
        Function GetGeneralLedgerByLocation(ByVal ForDate As Date, ByVal CriStr As String, ByVal LocationID As String) As DataTable
        Function GetAllCashInCashOutReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal CriStr As String, ByVal LocationID As String) As DataSet
        Function GetDailyTransactonByLocation(ByVal ForDate As Date, ByVal LocationID As String, ByVal CriStr As String) As System.Data.DataTable
        Function GetAllOtherCashDataByGeneralLedger(ByVal ForDate As Date) As DataTable
        Function GetAllRecordOtherCashDataByDate(ByVal ForDate As Date) As DataTable
        Function GetAllRecordOtherCashData(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Function GetAllCustomerTransaction(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetAllCustomerReceipt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetAllCashTransaction(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal Type As String = "", Optional ByVal GoldQualityID As String = "", Optional ByVal CustomerCode As String = "", Optional ByVal LocationID As String = "", Optional ByVal CustomerID As String = "", Optional ByVal str As String = "") As DataTable
    End Interface
End Namespace
