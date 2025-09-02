Imports CommonInfo
Namespace GoldSmith
    Public Interface IGoldSmithDA
        Function InsertGoldSmith(ByVal GoldSmithObj As GoldSmithInfo) As Boolean
        Function UpdateGoldSmith(ByVal GoldSmithObj As GoldSmithInfo) As Boolean
        Function DeleteGoldSmith(ByVal GoldSmithID As String) As Boolean
        Function GetAllGoldSmith() As DataTable
        Function GetGoldSmithByID(ByVal GoldSmithID As String) As GoldSmithInfo
        Function GetGoldSmithCode(ByVal _GoldSmithCode As String) As GoldSmithInfo
        Function GetGoldSmith() As DataTable
        Function GetAllGoldSmithList() As DataTable
        Function GetAllGoldSmithListByLocation(ByVal LocationID As String) As DataTable

        Function GetGoldSmithNameListByGoldSmithID(ByVal GoldSmithID As String) As DataTable
        Function GetGoldSmithNameListByStock(ByVal GoldSmithID As String) As DataTable
        Function GetDefaultGoldSmith() As DataTable
    End Interface
End Namespace

