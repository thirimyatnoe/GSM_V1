Public Interface ITest
    Function GenerateReportDocument(ByVal Customer As String, ByVal GoldQuality As String, ByVal ItemCategory As String, ByVal ItemName As String, ByVal GemsCategory As String, ByVal Staff As String, ByVal FromDate As Date, ByVal ToDate As Date) As DataSet
End Interface

