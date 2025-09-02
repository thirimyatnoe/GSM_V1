Imports DataAccess.PurchaseGems
Imports CommonInfo
Namespace PurchaseGems
    Public Class PurchaseGemsController
        Implements IPurchaseGemsController
#Region "Private Members"

        Private _objPurchaseGemsDA As IPurchaseGemsDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IPurchaseGemsController = New PurchaseGemsController

#End Region

#Region "Constructors"

        Private Sub New()
            _objPurchaseGemsDA = DataAccess.Factory.Instance.CreatePurchaseGemsDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IPurchaseGemsController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeletePurchaseGems(ByVal PurchaseGemsID As String) As Boolean Implements IPurchaseGemsController.DeletePurchaseGems
            If _objPurchaseGemsDA.DeletePurchaseGems(PurchaseGemsID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseGems.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       PurchaseGemsID, _
                                                       "Delete Purchase Gems")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllPurchaseGems() As System.Data.DataTable Implements IPurchaseGemsController.GetAllPurchaseGems
            Return _objPurchaseGemsDA.GetAllPurchaseGems()
        End Function

        Public Function GetPurchaseGems(ByVal PurchaseGemsID As String) As CommonInfo.PurchaseGemsInfo Implements IPurchaseGemsController.GetPurchaseGems
            Return _objPurchaseGemsDA.GetPurchaseGems(PurchaseGemsID)
        End Function

        Public Function SavePurchaseGems(ByVal PurchaseGemsObj As CommonInfo.PurchaseGemsInfo, ByVal _dtPurchaseGemsItem As System.Data.DataTable) As Boolean Implements IPurchaseGemsController.SavePurchaseGems
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            If PurchaseGemsObj.PurchaseGemsID = "" Then
                PurchaseGemsObj.PurchaseGemsID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.PurchaseGems, CommonInfo.EnumSetting.GenerateKeyType.PurchaseGems.ToString, PurchaseGemsObj.PDate)
                bolRet = _objPurchaseGemsDA.InsertPurchaseGems(PurchaseGemsObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseGems.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                       PurchaseGemsObj.PurchaseGemsID, _
                                                       "Insert Purchase Gems")
            Else
                bolRet = _objPurchaseGemsDA.UpdatePurchaseGems(PurchaseGemsObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.PurchaseGems.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                       PurchaseGemsObj.PurchaseGemsID, _
                                                       "Update Purchase Gems")
            End If

            If bolRet Then
                For Each dr As DataRow In _dtPurchaseGemsItem.Rows
                    Dim objPurchaseItemInfo As New PurchaseGemsItemInfo
                    If dr.RowState = DataRowState.Added Then
                        With objPurchaseItemInfo
                            .PurchaseGemsID = PurchaseGemsObj.PurchaseGemsID
                            .PurchaseGemsItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseGemsItem, "PurchaseGemsItemID", PurchaseGemsObj.PDate)
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = IIf(IsDBNull(dr.Item("GemsName")) = True, "-", dr.Item("GemsName"))
                            .Clarity = IIf(IsDBNull(dr.Item("Clarity")) = True, "-", dr.Item("Clarity"))
                            .SizeMM = IIf(IsDBNull(dr.Item("SizeMM")) = True, "-", dr.Item("SizeMM"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, 0, dr.Item("YOrCOrG"))
                            .GemTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
                            .QTY = IIf(IsDBNull(dr.Item("QTY")) = True, 0, dr.Item("QTY"))
                            If (dr.Item("FixType") = "Fix") Then
                                .FixType = 1
                            ElseIf (dr.Item("FixType") = "ByWeight") Then
                                .FixType = 2
                            Else
                                .FixType = 3
                            End If
                            .PurchaseRate = dr.Item("PurchaseRate")
                            .Amount = dr.Item("Amount")
                        End With
                        bolRet = _objPurchaseGemsDA.InsertPurchaseGemsItem(objPurchaseItemInfo)
                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objPurchaseItemInfo
                            .PurchaseGemsID = PurchaseGemsObj.PurchaseGemsID
                            .PurchaseGemsItemID = dr.Item("PurchaseGemsItemID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = IIf(IsDBNull(dr.Item("GemsName")) = True, "-", dr.Item("GemsName"))
                            .Clarity = IIf(IsDBNull(dr.Item("Clarity")) = True, "-", dr.Item("Clarity"))
                            .SizeMM = IIf(IsDBNull(dr.Item("SizeMM")) = True, "-", dr.Item("SizeMM"))
                                                        .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))

                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, 0, dr.Item("YOrCOrG"))
                            .GemTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
                            .QTY = IIf(IsDBNull(dr.Item("QTY")) = True, 0, dr.Item("QTY"))
                            If (dr.Item("FixType") = "Fix") Then
                                .FixType = 1
                            ElseIf (dr.Item("FixType") = "ByWeight") Then
                                .FixType = 2
                            Else
                                .FixType = 3
                            End If

                            .PurchaseRate = dr.Item("PurchaseRate")
                            .Amount = dr.Item("Amount")
                        End With
                        bolRet = _objPurchaseGemsDA.UpdatePurchaseGemsItem(objPurchaseItemInfo)
                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objPurchaseGemsDA.DeletePurchaseGemsItem(CStr(dr.Item("PurchaseGemsItemID", DataRowVersion.Original)))
                    End If
                Next
            End If

            Return bolRet
        End Function

        Public Function GetPurchaseGemsItem(ByVal PurchaseGemsID As String) As System.Data.DataTable Implements IPurchaseGemsController.GetPurchaseGemsItem
            Return _objPurchaseGemsDA.GetPurchaseGemsItem(PurchaseGemsID)
        End Function
        Public Function GetPurchaseGemsReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IPurchaseGemsController.GetPurchaseGemsReport
            Return _objPurchaseGemsDA.GetPurchaseGemsReport(FromDate, ToDate, criStr)
        End Function

        Public Function GetAllPurchaseGem() As System.Data.DataTable Implements IPurchaseGemsController.GetAllPurchaseGem
            Return _objPurchaseGemsDA.GetAllPurchaseGem()
        End Function
        Public Function GetPurchaseGemsPrint(ByVal PurchaseGemsID As String) As System.Data.DataTable Implements IPurchaseGemsController.GetPurchaseGemsPrint
            Return _objPurchaseGemsDA.GetPurchaseGemsPrint(PurchaseGemsID)
        End Function

        Public Function InsertPurchaseGemsUserID(ByVal PurchaseGemsID As String, ByVal UserID As String) As Boolean Implements IPurchaseGemsController.InsertPurchaseGemsUserID
            Return _objPurchaseGemsDA.InsertPurchaseGemsUserID(PurchaseGemsID, UserID)
        End Function
    End Class
End Namespace


