Imports CommonInfo
Namespace MortgageInvoice

    Public Interface IMortgageInvoiceController
        Function DeleteMortgageInvoiceHeader(ByVal MortgageInvoiceID As String) As Boolean
        Function GetMortgageInvoice(ByVal MortgageInvoiceID As String) As MortgageInvoiceInfo
        Function GetMortgageReturnFromInterest(ByVal MortgageInvoiceID As String) As DataTable
        Function GetAllMortgageInvoice() As DataTable
        Function GetMortgageInvoicePrint(ByVal MortgageInvoiceID As String) As DataTable
        Function SaveMortgageInvoice(ByVal MortgageInvoiceObj As MortgageInvoiceInfo, ByVal _dtMortgageInvoiceItem As DataTable) As Boolean

        Function GetMortgageInvoiceItem(ByVal _MortgageInvoiceItemID As String) As DataTable
        Function GetMortgageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetAllMortgageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable

        Function GetMortgagePaybackReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetMortgagePaybackTotalReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable

        Function GetMortgageReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetMortgageInterestReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataSet
        Function GetMortgageCustomerHistoryReport(Optional ByVal cristr As String = "") As DataSet

        Function GetMortgageDisableSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As DataTable

        Function GetMortgageDisable(ByVal InterestPeriod As Integer) As DataTable
        Function SaveMortgageDisable(ByVal DisableDate As Date, ByVal _dtMortgageDisable As DataTable) As Boolean
        Function GetAllMortgageInvoiceFromSearchBox() As DataTable
        Function GetMortgageInvoiceItemFromSearchBox(ByVal MortgageInvoiceID As String) As DataTable
        Function GetMortgageInvoiceByMortgageCode(ByVal MortgageInvoiceCode As String) As DataTable
        Function GetMortgageInvoiceDisableReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        'add new Fun: 11.07.2012
        Function GetMortgageInvoiceExcludeInMortgageItems() As DataTable
        'add new Function 26.09.2012
        Function GetAllMortgageInvoiceByIsRepayHeadOffice() As DataTable
        Function GetMortgageInvoiceByMortgageCodeOnIsRepayHO(ByVal MortgageInvoiceCode As String) As DataTable
        Function GetMortgageDataByMortgageInvoiceID(ByVal argstr As String, Optional ByVal MortgageInvoiceID As String = "") As DataTable
        Function GetMortgageInvoiceForPaybackUpdate(ByVal MortgageInvoiceID As String, ByVal MortgagePaybackID As String) As MortgageInvoiceInfo
        Function GetAllMortgageInvoiceByReturn() As DataTable
        Function GetMortgageReturnReportSum(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetMortgageInvoiceReceiveItem(ByVal _MortgageInvoiceItemID As String) As DataTable
        Function GetAllMortgageReturnByMortgageInvoice(ByVal _MortgageInvoiceID As String) As DataTable
        Function GetMortgageReturnReportNew(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable
        Function GetMortgageReturnReportSumNew(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As DataTable

    End Interface

End Namespace
