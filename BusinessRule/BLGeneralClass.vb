Imports DataAccess

Public Class BLGeneralClass
    Public Shared Sub Set_Global_UserID(ByVal temp_Global_UserID As String, ByVal temp_CurrentLocationID As String, ByVal temp_ReuseBarcode As Boolean, ByVal temp_Global_IsCash As Boolean)
        BLGeneralModule.Global_UserID = temp_Global_UserID
        BLGeneralModule.Global_CurrentLocationID = temp_CurrentLocationID
        BLGeneralModule.Global_IsReuseBarcode = temp_ReuseBarcode
        BLGeneralModule.Global_IsCash = temp_Global_IsCash
        DALGeneralClass.Set_Global_UserID(temp_Global_UserID, temp_CurrentLocationID, temp_Global_IsCash)
    End Sub
    Public Shared Sub Set_Global_GoldWeight(ByVal temp_Global_KyatToGram As Decimal, ByVal temp_Global_GramToKarat As Decimal, ByVal temp_Global_KaratToYati As Decimal, ByVal temp_Global_YatiToB As Decimal, ByVal temp_Global_BToP As Decimal, ByVal temp_Global_PToY As Decimal)
        BLGeneralModule.Global_KyatToGram = temp_Global_KyatToGram
        BLGeneralModule.Global_GramToKarat = temp_Global_GramToKarat
        BLGeneralModule.Global_KaratToYati = temp_Global_KaratToYati
        BLGeneralModule.Global_YatiToB = temp_Global_YatiToB
        BLGeneralModule.Global_BToP = temp_Global_BToP
        BLGeneralModule.Global_BToP = temp_Global_BToP
        BLGeneralModule.Global_PToY = temp_Global_PToY
        DALGeneralClass.Set_Global_GoldWeight(temp_Global_PToY)
    End Sub
End Class
