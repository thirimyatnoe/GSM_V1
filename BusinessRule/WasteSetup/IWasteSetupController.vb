Imports CommonInfo
Namespace WasteSetup
    Public Interface IWasteSetupController
        Function InsertWasteSetup(ByVal WasteSetupObj As WasteSetupHeaderInfo, ByVal _dtWasteSetupDetail As DataTable) As Boolean
        Function GetWasteSetup() As DataTable
        Function UpdateWasteSetup(ByVal WasteSetupObj As ItemNameInfo) As Boolean
        Function DeleteWasteSetup(ByVal WasteSetupHeaderID As String) As Boolean
        Function GetWasteSetupHeaderID(ByVal WasteSetupHeaderID As String) As WasteSetupHeaderInfo

        Function DeleteWasteSetupDetail(ByVal WasteSetupDetailID As String) As Boolean
        Function InsertWasteSetupDetail(ByVal WasteSetupDetailObj As WasteSetupDetailInfo) As Boolean
        Function UpdateWasteSetupDetail(ByVal ObjDetail As WasteSetupDetailInfo) As Boolean
        Function GetWasteSetupDetail(ByVal WasteSetupHeaderID As String) As DataTable
        Function GetWasetSetupInfoByStockWeight(ByVal ItemTK As Decimal, ByVal ItemCategoryID As String, ByVal ItemNameID As String, ByVal GoldQualityID As String) As WasteSetupDetailInfo
    End Interface
End Namespace

