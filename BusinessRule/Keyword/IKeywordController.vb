Imports CommonInfo

Namespace Keyword
    Public Interface IKeywordController
        Function SaveKeyword(ByRef KeywordHeaderObj As KeywordHeaderInfo, ByVal KeyWordsItems As DataTable) As Boolean
        Function DeleteKeyword(ByVal KeywordHeaderID As String) As Boolean
        Function GetKeywordHeader(ByVal KeywordHeaderID As String) As KeywordHeaderInfo
        Function GetKeywordHeaderIDByName(ByVal KeywordName As String) As DataTable
        Function GetKeywordItems(ByVal KeywordHeaderID As String) As DataTable
        Function GetKeywordHeaderList() As DataTable
        Function GetKeywordItemsByHeaderName(ByVal KeywordName As String) As DataTable
        Function GetKeywordItemsName() As DataTable
        Function GetKeywordItemByItemID(ByVal ItemID As String) As KeywordItemInfo
    End Interface
End Namespace