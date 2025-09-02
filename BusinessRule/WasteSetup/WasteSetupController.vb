Imports DataAccess.WasteSetup
Imports CommonInfo
Namespace WasteSetup
    Public Class WasteSetupController
        Implements IWasteSetupController
#Region "Private Members"

        Private _objWasteSetupDA As IWasteSetupDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IWasteSetupController = New WasteSetupController

#End Region

#Region "Constructors"

        Private Sub New()
            _objWasteSetupDA = DataAccess.Factory.Instance.CreateWasteSetup

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IWasteSetupController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteWasteSetup(WasteSetupHeaderID As String) As Boolean Implements IWasteSetupController.DeleteWasteSetup
            If _objWasteSetupDA.DeleteWasteSetup(WasteSetupHeaderID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                      DateTime.Now, _
                                      Global_UserID, _
                                      CommonInfo.EnumSetting.GenerateKeyType.WasteSetUp.ToString, _
                                      CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                      WasteSetupHeaderID, _
                                      "Delete WasteSetup")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function DeleteWasteSetupDetail(WasteSetupDetailID As String) As Boolean Implements IWasteSetupController.DeleteWasteSetupDetail

        End Function

        Public Function GetWasteSetup() As DataTable Implements IWasteSetupController.GetWasteSetup
            Return _objWasteSetupDA.GetWasteSetup()
        End Function

        Public Function GetWasteSetupDetail(WasteSetupHeaderID As String) As DataTable Implements IWasteSetupController.GetWasteSetupDetail
            Return _objWasteSetupDA.GetWasteSetupDetail(WasteSetupHeaderID)
        End Function

        Public Function GetWasteSetupHeaderID(WasteSetupHeaderID As String) As WasteSetupHeaderInfo Implements IWasteSetupController.GetWasteSetupHeaderID
            Return _objWasteSetupDA.GetWasteSetupHeaderID(WasteSetupHeaderID)
        End Function

        Public Function InsertWasteSetup(WasteSetupObj As WasteSetupHeaderInfo, ByVal _dtWasteSetupDetail As DataTable) As Boolean Implements IWasteSetupController.InsertWasteSetup
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False
            Dim objWasteSetupDetail As New CommonInfo.WasteSetupDetailInfo

            If WasteSetupObj.WasteSetupHeaderID = "" Then

                WasteSetupObj.WasteSetupHeaderID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.WasteSetUp, CommonInfo.EnumSetting.GenerateKeyType.WasteSetUp.ToString, Now)
                bolRet = _objWasteSetupDA.InsertWasteSetup(WasteSetupObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                       DateTime.Now, _
                       Global_UserID, _
                       CommonInfo.EnumSetting.GenerateKeyType.ItemName.ToString, _
                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                       WasteSetupObj.WasteSetupHeaderID, _
                       "Insert WasteSetup")
            Else
                bolRet = _objWasteSetupDA.UpdateWasteSetup(WasteSetupObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                       DateTime.Now, _
                       Global_UserID, _
                       CommonInfo.EnumSetting.GenerateKeyType.ItemName.ToString, _
                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                       WasteSetupObj.WasteSetupHeaderID, _
                       "Update WasteSetup")
            End If

            If bolRet Then
                For Each dr As DataRow In _dtWasteSetupDetail.Rows

                    If dr.RowState = DataRowState.Deleted Then
                        _objWasteSetupDA.DeleteWasteSetupDetail(dr("WasteSetupDetailID", DataRowVersion.Original))

                    ElseIf dr.RowState = DataRowState.Added Then
                        With objWasteSetupDetail
                            .WasteSetupDetailID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.WasteSetUpDetail, CommonInfo.EnumSetting.GenerateKeyType.WasteSetUpDetail.ToString(), Now)
                            .WasteSetupHeaderID = WasteSetupObj.WasteSetupHeaderID
                            .GoldQualityID = IIf(IsDBNull(dr.Item("@GoldQualityID")), "01", dr.Item("@GoldQualityID"))
                            .MinNetWeightTK = IIf(IsDBNull(dr.Item("MinNetWeightTK")), 0, dr.Item("MinNetWeightTK"))
                            .MinNetWeightTG = IIf(IsDBNull(dr.Item("MinNetWeightTG")), 0, dr.Item("MinNetWeightTG"))
                            .MaxNetWeightTK = IIf(IsDBNull(dr.Item("MaxNetWeightTK")), 0, dr.Item("MaxNetWeightTK"))
                            .MaxNetWeightTG = IIf(IsDBNull(dr.Item("MaxNetWeightTG")), 0, dr.Item("MaxNetWeightTG"))
                            .MinWeightTKForSale = IIf(IsDBNull(dr.Item("MinWeightTKForSale")), 0, dr.Item("MinWeightTKForSale"))
                            .MinWeightTGForSale = IIf(IsDBNull(dr.Item("MinWeightTGForSale")), 0, dr.Item("MinWeightTGForSale"))
                        End With

                        _objWasteSetupDA.InsertWasteSetupDetail(objWasteSetupDetail)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objWasteSetupDetail
                            .WasteSetupDetailID = dr.Item("WasteSetupDetailID")
                            .WasteSetupHeaderID = WasteSetupObj.WasteSetupHeaderID
                            .GoldQualityID = IIf(IsDBNull(dr.Item("@GoldQualityID")), "01", dr.Item("@GoldQualityID"))
                            .MinNetWeightTK = IIf(IsDBNull(dr.Item("MinNetWeightTK")), 0, dr.Item("MinNetWeightTK"))
                            .MinNetWeightTG = IIf(IsDBNull(dr.Item("MinNetWeightTG")), 0, dr.Item("MinNetWeightTG"))
                            .MaxNetWeightTK = IIf(IsDBNull(dr.Item("MaxNetWeightTK")), 0, dr.Item("MaxNetWeightTK"))
                            .MaxNetWeightTG = IIf(IsDBNull(dr.Item("MaxNetWeightTG")), 0, dr.Item("MaxNetWeightTG"))
                            .MinWeightTKForSale = IIf(IsDBNull(dr.Item("MinWeightTKForSale")), 0, dr.Item("MinWeightTKForSale"))
                            .MinWeightTGForSale = IIf(IsDBNull(dr.Item("MinWeightTGForSale")), 0, dr.Item("MinWeightTGForSale"))
                        End With

                        _objWasteSetupDA.UpdateWasteSetupDetail(objWasteSetupDetail)
                    End If
                Next
            End If

            Return bolRet
        End Function
        Public Function GetWasetSetupInfoByStockWeight(ByVal ItemTK As Decimal, ByVal ItemCategoryID As String, ByVal ItemNameID As String, ByVal GoldQualityID As String) As WasteSetupDetailInfo Implements IWasteSetupController.GetWasetSetupInfoByStockWeight
            Return _objWasteSetupDA.GetWasetSetupInfoByStockWeight(ItemTK, ItemCategoryID, ItemNameID, GoldQualityID)
        End Function
        Public Function InsertWasteSetupDetail(WasteSetupDetailObj As WasteSetupDetailInfo) As Boolean Implements IWasteSetupController.InsertWasteSetupDetail

        End Function

        Public Function UpdateWasteSetup(WasteSetupObj As ItemNameInfo) As Boolean Implements IWasteSetupController.UpdateWasteSetup

        End Function

        Public Function UpdateWasteSetupDetail(ObjDetail As WasteSetupDetailInfo) As Boolean Implements IWasteSetupController.UpdateWasteSetupDetail

        End Function
    End Class

End Namespace
