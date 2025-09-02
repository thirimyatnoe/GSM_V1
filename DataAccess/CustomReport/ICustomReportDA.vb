Imports CommonInfo

Namespace CustomReport

    Public Interface ICustomReportDA
        Function InsertCustomReport(ByVal CustomReportObj As CustomReportInfo) As Boolean
        Function UpdateCustomReport(ByVal CustomReportObj As CustomReportInfo) As Boolean
        Function DeleteCustomReport(ByVal CustomReportID As String) As Boolean
        Function GetCustomReport(ByVal CustomReportID As String) As CustomReportInfo
        Function GetCustomReportByStr(ByVal cristr As String) As DataTable

    End Interface

End Namespace
