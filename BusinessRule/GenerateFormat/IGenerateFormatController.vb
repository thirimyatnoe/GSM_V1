Imports CommonInfo
Namespace GenerateFormat
    Public Interface IGenerateFormatController
        Function GetGenerateFormat(ByVal GenerateFormatID As Integer) As CommonInfo.GenerateFormatInfo
        Function Save_GenerateFormat(ByVal objGenerateFormat As CommonInfo.GenerateFormatInfo) As Boolean
        Function GetDateFormatData() As DataTable
        Function GetSecondDateFormatData() As DataTable
        Function GetGenerateFormatList() As DataTable
        Function DeleteGenerateFormat(ByVal GenerateFormatID As Integer) As Boolean
        Function GetGenerateFormatByFormat(ByVal GenerateFormat As String) As GenerateFormatInfo
    End Interface
End Namespace

