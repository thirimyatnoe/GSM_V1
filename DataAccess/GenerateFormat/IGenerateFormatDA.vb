Imports CommonInfo
Namespace GenerateFormat
    Public Interface IGenerateFormatDA
        Function Insert_GenerateFormat(ByVal objGenerateFormat As CommonInfo.GenerateFormatInfo) As Boolean
        Function Get_GenerateFormatByID(ByVal GenerateFormatID As Integer) As GenerateFormatInfo
        Function GetDateFormat() As DataTable
        Function GetSecondDateFormat() As DataTable
        Function Get_GenerateFormat() As DataTable
        Function Delete_GenerateFormat(ByVal GenerateFormatID As Integer) As Boolean
        Function GetGenerateFormatByFormat(ByVal GenerateFormat As String) As GenerateFormatInfo
    End Interface

End Namespace

