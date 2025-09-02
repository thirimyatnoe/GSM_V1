Imports CommonInfo
Namespace GoldSmith
    Public Interface IGoldSmithController
        Function SaveGoldSmith(ByVal GoldSmithObj As GoldSmithInfo) As String
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

