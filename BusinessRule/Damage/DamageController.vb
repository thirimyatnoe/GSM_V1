Imports DataAccess.Damage
Imports DataAccess.SalesItem
Imports CommonInfo
Namespace Damage
    Public Class DamageController
        Implements IDamageController

#Region "Private Members"

        Private _objDamageDA As IDamageDA
        Private _objSaleItemDA As ISalesItemDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IDamageController = New DamageController

#End Region

#Region "Constructors"

        Private Sub New()
            _objDamageDA = DataAccess.Factory.Instance.CreateDamageDA
            _objSaleItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDamageController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate
            End With
            _objSaleItemDA.UpdateSaleItemIsExit(objSaleItem)
        End Sub
        Public Function DeleteDamage(ByVal DamageID As String) As Boolean Implements IDamageController.DeleteDamage
            Dim tmpdt As New DataTable
            tmpdt = _objDamageDA.GetForSaleIDByDamageID(DamageID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                Next
            End If
            If _objDamageDA.DeleteDamage(DamageID) Then

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Damage.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       DamageID, _
                                       "Delete Damage")
                Return True
            Else
                Return False
            End If

        End Function

        Public Function GetAllDamage() As System.Data.DataTable Implements IDamageController.GetAllDamage
            Return _objDamageDA.GetAllDamage()
        End Function
        Public Function GetDamage(ByVal DamageID As String) As CommonInfo.DamageInfo Implements IDamageController.GetDamage
            Return _objDamageDA.GetDamage(DamageID)
        End Function

        Public Function SaveDamage(ByVal DamageObj As CommonInfo.DamageInfo, ByVal _dtDamageItem As System.Data.DataTable) As Boolean Implements IDamageController.SaveDamage
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            Dim bolItemRet As Boolean = False
            If DamageObj.DamageID = "" Then
                DamageObj.DamageID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.Damage, CommonInfo.EnumSetting.GenerateKeyType.Damage.ToString, DamageObj.DDate)
                bolRet = _objDamageDA.InsertDamage(DamageObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Damage.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       DamageObj.DamageID, _
                                       "Insert Damage")
            Else
                bolRet = _objDamageDA.UpdateDamage(DamageObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Damage.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       DamageObj.DamageID, _
                                       "Update Damage")


                Dim tmpdt As New DataTable
                tmpdt = _objDamageDA.GetForSaleIDByDamageID(DamageObj.DamageID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                    Next
                End If
            End If

            If bolRet Then
                For Each dr As DataRow In _dtDamageItem.Rows
                    Dim objDmgItem As New DamageItemInfo
                    Dim objForSale As New SalesItemInfo

                    '''''ForSale IsExit=1 and ExitDate=DDate

                    ''If dr.RowState <> DataRowState.Deleted Then
                    ''    With objForSale
                    ''        .ForSaleID = dr.Item("ForSaleID")
                    ''        .IsExit = 1
                    ''        .ExitDate = DamageObj.DDate
                    ''        .LocationID = Global_CurrentLocationID
                    ''        .CounterID = DamageObj.CounterID
                    ''    End With
                    ''    bolItemRet = _objSaleItemDA.UpdateSaleItemIsExit(objForSale)
                    ''End If

                    '''''

                    If dr.RowState = DataRowState.Added Then
                        With objDmgItem
                            .DamageID = DamageObj.DamageID
                            .DamageItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.DamageItem, EnumSetting.GenerateKeyType.DamageItem.ToString, DamageObj.DDate)
                            .ForSaleID = dr.Item("ForSaleID")
                        End With
                        bolItemRet = _objDamageDA.InsertDamageItem(objDmgItem)
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, DamageObj.DDate)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objDmgItem
                            .DamageID = DamageObj.DamageID
                            .DamageItemID = dr.Item("DamageItemID")
                            .ForSaleID = dr.Item("ForSaleID")
                        End With
                        bolItemRet = _objDamageDA.UpdateDamageItem(objDmgItem)
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, DamageObj.DDate)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, DamageObj.DDate)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolItemRet = _objDamageDA.DeleteDamageItem(CStr(dr.Item("DamageItemID", DataRowVersion.Original)))
                    End If
                Next


            End If

            Return bolRet
        End Function

        Public Function SaveReadded(ByVal DamageObj As CommonInfo.DamageItemInfo, ByVal _dtReaddedItem As System.Data.DataTable) As Boolean Implements IDamageController.SaveReadded
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            Dim bolItemRet As Boolean = False
            ''If DamageObj.DamageID = "" Then
            ''    DamageObj.DamageID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.Damage, CommonInfo.EnumSetting.GenerateKeyType.Damage.ToString, DamageObj.DDate)
            ''    bolRet = _objDamageDA.InsertDamage(DamageObj)
            ''    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
            ''                           DateTime.Now, _
            ''                           Global_UserID, _
            ''                           CommonInfo.EnumSetting.GenerateKeyType.Damage.ToString, _
            ''                           CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
            ''                           DamageObj.DamageID, _
            ''                           "Insert Damage")
            ''Else
            ''bolRet = _objDamageDA.UpdateReadded(DamageObj)
            ''_objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
            ''                       DateTime.Now, _
            ''                       Global_UserID, _
            ''                       CommonInfo.EnumSetting.GenerateKeyType.Damage.ToString, _
            ''                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
            ''                       DamageObj.DamageID, _
            ''                       "Update Readded")


            Dim tmpdt As New DataTable
            tmpdt = _objDamageDA.GetForSaleIDByDamageID(DamageObj.DamageID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                Next
            End If
            'End If

            'If bolRet Then
            If Not _dtReaddedItem Is Nothing Then
                For Each dr As DataRow In _dtReaddedItem.Rows
                    Dim objDmgItem As New DamageItemInfo
                    Dim objForSale As New SalesItemInfo

                    '''''ForSale IsExit=1 and ExitDate=DDate

                    ''If dr.RowState <> DataRowState.Deleted Then
                    ''    With objForSale
                    ''        .ForSaleID = dr.Item("ForSaleID")
                    ''        .IsExit = 1
                    ''        .ExitDate = DamageObj.DDate
                    ''        .LocationID = Global_CurrentLocationID
                    ''        .CounterID = DamageObj.CounterID
                    ''    End With
                    ''    bolItemRet = _objSaleItemDA.UpdateSaleItemIsExit(objForSale)
                    ''End If

                    '''''

                    If dr.RowState = DataRowState.Added Then
                        If IsDBNull(dr.Item("ForSaleID")) Then

                        Else
                            With objDmgItem
                                .ReAddDate = DamageObj.ReAddDate
                                '.DamageItemID = dr.Item("DamageItemID")
                                .ForSaleID = dr.Item("ForSaleID")
                                .Remark = dr.Item("Remark")
                                '.IsReAdd = 1
                            End With
                            bolItemRet = _objDamageDA.UpdateReaddedItem(objDmgItem)
                            UpdateSalesItemByIsExit(dr.Item("ForSaleID"), False, DamageObj.ReAddDate)
                        End If
                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objDmgItem
                            ''.DamageID = DamageObj.DamageID
                            ''.DamageItemID = dr.Item("DamageItemID")
                            ''.ForSaleID = dr.Item("ForSaleID")
                            '.DamageID = DamageObj.DamageID
                            .ReAddDate = DamageObj.ReAddDate
                            .ForSaleID = dr.Item("ForSaleID")
                            .Remark = dr.Item("Remark")
                            '.IsReAdd = 1
                        End With
                        bolItemRet = _objDamageDA.UpdateReaddedItem(objDmgItem)
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, DamageObj.ReAddDate)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, DamageObj.ReAddDate)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolItemRet = _objDamageDA.DeleteDamageItem(CStr(dr.Item("DamageItemID", DataRowVersion.Original)))
                    End If
                Next


            End If

            Return bolItemRet
        End Function
        Public Function GetDamageItem(ByVal DamageID As String) As System.Data.DataTable Implements IDamageController.GetDamageItem
            Return _objDamageDA.GetDamageItem(DamageID)
        End Function
        Public Function GetDamageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IDamageController.GetDamageReport
            Return _objDamageDA.GetDamageReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetDamageSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As System.Data.DataTable Implements IDamageController.GetDamageSummaryByGoldQualityAndItemCategory
            Return _objDamageDA.GetDamageSummaryByGoldQualityAndItemCategory(ForDate, GoldQualityID, ItemCategoryID)
        End Function

        Public Function GetAllDamageFromSearchBox() As System.Data.DataTable Implements IDamageController.GetAllDamageFromSearchBox
            Return _objDamageDA.GetAllDamageFromSearchBox()
        End Function
    End Class

End Namespace
