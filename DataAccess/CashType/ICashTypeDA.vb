Imports CommonInfo
Namespace CashType
    Public Interface ICashTypeDA
        Function GetCashTypeList() As System.Data.DataTable
        Function DeleteCashType(ByVal CashTypeID As String) As Boolean
        Function InsertCashType(ByVal obj As CommonInfo.CashTypeInfo) As Boolean
        Function UpdateCashType(ByVal obj As CommonInfo.CashTypeInfo) As Boolean
        Function GetCashTypeDataByCashType(ByVal CashType As String, Optional CashTypeID As String = "") As DataTable
    End Interface
End Namespace
