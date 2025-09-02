Imports CommonInfo

Namespace General
    Public Interface IGeneralDA
        Function GetGenerateKey(ByVal KeyType As String, ByVal GenerateFormat As String, ByVal GenerateOn As String, ByVal GenerateDate As Date, ByVal Prefix As String, ByVal Postfix As String) As String
        Function GenerateKey(ByVal KeyType As String, ByVal GenerateFormat As String, ByVal GenerateOn As String, ByVal GenerateDate As Date, ByVal Prefix As String, ByVal Postfix As String) As String
        Function CheckRecordsExistOrNot(ByVal table_1 As String, ByVal table_2 As String, ByVal IDName As String, ByVal argID As String) As DataTable
        Function GetMaxID(ByVal tbName As String, ByVal FName As String, Optional ByVal CriStr As String = "") As Integer
        Function GetGenerateKeyForFormat(ByVal objgenerate As GenerateFormatInfo, Optional ByVal SaleDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String
        Function GenerateKeyForFormat(ByVal objgenerate As GenerateFormatInfo, Optional ByVal SaleDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String
        Function CheckExitVoucherForCashReceipt(ByVal tbName As String, ByVal CriStr As String) As DataTable
        Function UpdateGenerateKeyForFormat(objgenerate As GenerateFormatInfo, Optional FromDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal MaxID As Integer = 0) As Boolean
        Function GenerateKeyForMortgageCode(ByVal objgenerate As GenerateFormatInfo, Optional ByVal SaleDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String
    End Interface
End Namespace
