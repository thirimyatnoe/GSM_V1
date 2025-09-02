Imports DataAccess.ItemName
Namespace ItemName
    Public Class ItemNameController
        Implements IItemNameController

#Region "Private Members"

        Private _objItemNameDA As IItemNameDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IItemNameController = New ItemNameController

#End Region

#Region "Constructors"

        Private Sub New()
            _objItemNameDA = DataAccess.Factory.Instance.CreateItemName

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IItemNameController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function GetItemName(ByVal ItemNameID As String) As CommonInfo.ItemNameInfo Implements IItemNameController.GetItemName
            Return _objItemNameDA.GetItemName(ItemNameID)
        End Function

        Public Function GetItemNameList() As System.Data.DataTable Implements IItemNameController.GetItemNameList
            Return _objItemNameDA.GetItemNameList()
        End Function

        Public Function InsertItemName(ByVal obj As CommonInfo.ItemNameInfo, ByVal _dtItemName As System.Data.DataTable) As Boolean Implements IItemNameController.InsertItemName
            Dim dr As DataRow
            Dim index As Integer = 1
            Dim objItemName As CommonInfo.ItemNameInfo
            Dim objGeneralController As General.IGeneralController
            Dim bolRet As Boolean = False
            objGeneralController = Factory.Instance.CreateGeneralController

            For Each dr In _dtItemName.Rows
                objItemName = New CommonInfo.ItemNameInfo
                If dr.RowState = DataRowState.Deleted Then

                    Dim dt As New DataTable
                    dt = objGeneralController.CheckRecordsExistOrNot("tbl_ForSale", "tbl_RepairDetail", "ItemNameID", dr("ItemNameID", DataRowVersion.Original))
                    If dt.Rows.Count() > 0 Then
                        If CInt(dt.Rows(0).Item("Res")) > 0 Then
                            MsgBox("You can not delete at row " & index & ".This Item Name which has Transaction Records!", MsgBoxStyle.Information, "")
                            Return False
                            Exit Function
                        End If

                    End If

                    dt = objGeneralController.CheckRecordsExistOrNot("tbl_OrderReceiveDetail", "tbl_PurchaseDetail", "ItemNameID", dr("ItemNameID", DataRowVersion.Original))
                    If dt.Rows.Count() > 0 Then
                        If CInt(dt.Rows(0).Item("Res")) > 0 Then
                            MsgBox("You can not delete at row " & index & ".This Item Name which has Transaction Records!", MsgBoxStyle.Information, "")
                            Return False
                            Exit Function
                        End If

                    End If


                    _objItemNameDA.DeleteItemName(dr("ItemNameID", DataRowVersion.Original))
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.ItemName.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       dr("ItemNameID", DataRowVersion.Original), _
                                       "Delete Item Name")

                ElseIf dr.RowState = DataRowState.Added Then
                    If Not IsDBNull(dr("ItemName_")) Then
                        objItemName.ItemNameID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.ItemName, "ItemNameID", Now)
                        objItemName.ItemName = dr("ItemName_")
                        objItemName.ItemCategoryID = obj.ItemCategoryID
                        'objItemName.ItemPhoto = dr("ItemPhoto")
                        Dim dtItemName As New DataTable
                        dtItemName = _objItemNameDA.GetItemNameByItemName(objItemName.ItemName, objItemName.ItemCategoryID)
                        If dtItemName.Rows.Count > 0 Then
                            MsgBox("New Item Name  is duplicated at row " & index & " Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                            Return False
                            Exit Function
                        Else
                            _objItemNameDA.InsertItemName(objItemName)
                            _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                               DateTime.Now, _
                                               Global_UserID, _
                                               CommonInfo.EnumSetting.GenerateKeyType.ItemName.ToString, _
                                               CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                               objItemName.ItemNameID, _
                                               "Insert Item Name")
                        End If
                       
                    End If

                ElseIf dr.RowState = DataRowState.Modified Then
                    If Not IsDBNull(dr("ItemName_")) Then
                        objItemName.ItemNameID = dr("ItemNameID")
                        objItemName.ItemCategoryID = obj.ItemCategoryID
                        objItemName.ItemName = dr("ItemName_")
                        'objItemName.ItemPhoto = dr("ItemPhoto")
                        Dim dtItem As New DataTable
                        Dim dtItemCode As New DataTable
                        dtItem = _objItemNameDA.GetItemNameByIDForUpdate(objItemName.ItemNameID, objItemName.ItemName, objItemName.ItemCategoryID)
                        ' dtItemCode = _objItemNameDA.HasPrefixForUpdateUseItemCode(objItemName.ItemNameID)


                        If dtItem.Rows.Count = 0 Then
                            ' If dtItemCode.Rows.Count = 0 Then
                            _objItemNameDA.UpdateItemName(objItemName)
                            _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                              DateTime.Now, _
                                              Global_UserID, _
                                              CommonInfo.EnumSetting.GenerateKeyType.ItemName.ToString, _
                                              CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                              objItemName.ItemNameID, _
                                              "Update Item Name")
                            'Else
                            '    MsgBox("Item Name  had used ItemCode.So it can't update Prefix!", MsgBoxStyle.Information, "Data Duplicated !")
                            '    Return False
                            '    Exit Function
                            'End If
                        Else
                            MsgBox("Item Name  is duplicated! at row " & index & " Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                            Return False
                            Exit Function
                        End If
                    End If

                End If
                index = index + 1
            Next
            Return True
        End Function

        Public Function GetItemNameID(ByVal ItemName As String) As CommonInfo.ItemNameInfo Implements IItemNameController.GetItemNameID
            Return _objItemNameDA.GetItemNameID(ItemName)
        End Function

        Public Function GetItemNameListByItemCategory(ByVal ItemCategoryID As String) As System.Data.DataTable Implements IItemNameController.GetItemNameListByItemCategory
            Return _objItemNameDA.GetItemNameListByItemCategory(ItemCategoryID)
        End Function

        Public Function GetItemID(ByVal ItemID As String) As System.Data.DataTable Implements IItemNameController.GetItemID
            Return _objItemNameDA.GetItemID(ItemID)
        End Function

        Public Function GetItemNamePhoto(ByVal ItemNameID As String) As CommonInfo.ItemNameInfo Implements IItemNameController.GetItemNamePhoto
            Return _objItemNameDA.GetItemNamePhoto(ItemNameID)
        End Function
        Public Function GetItemNameByCategory(ByVal ItemCategoryID As String) As CommonInfo.ItemNameInfo Implements IItemNameController.GetItemNameByCategory
            Return _objItemNameDA.GetItemNameByCategory(ItemCategoryID)
        End Function

        Public Function GetItemPhoto(ByVal ItemNameID As String) As CommonInfo.ItemNameInfo Implements IItemNameController.GetItemPhoto
            Return _objItemNameDA.GetItemPhoto(ItemNameID)
        End Function

        Public Function GetrptItemName() As DataTable Implements IItemNameController.GetrptItemName
            Return _objItemNameDA.GetrptItemName()
        End Function

        Public Function GetAllItemName() As DataTable Implements IItemNameController.GetAllItemName
            Return _objItemNameDA.GetAllItemName()
        End Function
    End Class
End Namespace

