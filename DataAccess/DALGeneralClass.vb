Public Class DALGeneralClass

    Public Shared Sub Set_Global_UserID(ByVal temp_Global_UserID As String, ByVal temp_CurrentLocationID As String, ByVal temp_Global_IsCash As Boolean)
        Global_UserID = temp_Global_UserID
        Global_CurrentLocationID = temp_CurrentLocationID
        Global_IsCash = temp_Global_IsCash
    End Sub

    Public Shared Sub Set_Global_GoldWeight(ByVal temp_Global_PToY As String)
        Global_PToY = temp_Global_PToY
    End Sub
End Class
