Imports CommonInfo

Namespace Keyword
    Public Interface IKeywordDA
        Function InsertKeywordHeader(ByVal KeywordHeaderObj As KeywordHeaderInfo) As Boolean
        Function UpdateKeywordHeader(ByVal KeywordHeaderObj As KeywordHeaderInfo) As Boolean
        Function DeleteKeywordHeader(ByVal KeywordHeaderID As Integer) As Boolean
        Function GetKeywordHeader(ByVal KeywordHeaderID As Integer) As KeywordHeaderInfo
        Function GetKeywordHeaderList() As DataTable
        Function GetKeywordHeaderIDByName(ByVal KeywordName As String) As DataTable

        Function InsertKeywordItem(ByVal KeywordItemObj As KeywordItemInfo) As Boolean
        Function UpdateKeywordItem(ByVal KeywordItemObj As KeywordItemInfo) As Boolean
        Function DeleteKeywordItem(ByVal KeywordItemID As Integer) As Boolean
		Function GetKeywordItems(ByVal KeywordHeaderID) As DataTable
        Function GetKeywordItemsByHeaderName(ByVal KeywordName As String) As DataTable
        Function GetKeywordItemsName() As DataTable
        Function GetKeywordItemByItemID(ByVal ItemID As String) As KeywordItemInfo

    End Interface
End Namespace