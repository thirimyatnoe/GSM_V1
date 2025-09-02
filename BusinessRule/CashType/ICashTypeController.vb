Imports CommonInfo
Namespace CashType
    Public Interface ICashTypeController
        Function GetCashTypeList() As System.Data.DataTable
        Function SaveCashType(ByVal _dtCash As System.Data.DataTable) As Boolean
        Function GetCashTypeDataByCashType(ByVal CashType As String, Optional CashTypeID As String = "") As DataTable
    End Interface
End Namespace

