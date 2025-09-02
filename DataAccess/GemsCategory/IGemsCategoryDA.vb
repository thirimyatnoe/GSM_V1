Imports CommonInfo
Namespace GemsCategory
    Public Interface IGemsCategoryDA
        Function InsertGemsCategory(ByVal GemsCategoryObj As GemsCategoryInfo) As Boolean
        Function UpdateGemsCategory(ByVal GemsCategoryObj As GemsCategoryInfo) As Boolean
        Function DeleteGemsCategory(ByVal GemsCategoryID As String) As Boolean
        Function GetAllGemsCategory() As DataTable
        Function GetAllGemsCategoryForGridCombo() As DataTable
        Function GetGemsCategory(ByVal GemsCategoryID As String) As GemsCategoryInfo
        Function GetAllGemsCategoryFromSearchBox() As DataTable
        Function GetrptGemsCategory() As DataTable
        Function GetGemsCategoryByGemsCategory(ByVal GemsCategory As String, Optional ByVal GemsCategoryID As String = "") As DataTable
        Function HasPrefixForUpdate(ByVal prefix As String, ByVal OldPrefix As String) As DataTable
    End Interface

End Namespace
