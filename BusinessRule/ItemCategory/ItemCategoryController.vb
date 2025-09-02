Imports DataAccess.ItemCategory
Namespace ItemCategory
    Public Class ItemCategoryController
        Implements IItemCategoryController


#Region "Private Members"

        Private _objItemCategoryDA As IItemCategoryDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IItemCategoryController = New ItemCategoryController

#End Region

#Region "Constructors"

        Private Sub New()
            _objItemCategoryDA = DataAccess.Factory.Instance.CreateItemCategoryDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IItemCategoryController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteItemCategory(ByVal ItemCategoryID As String) As Boolean Implements IItemCategoryController.DeleteItemCategory
            If _objItemCategoryDA.DeleteItemCategory(ItemCategoryID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.ItemCategory.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                          ItemCategoryID, _
                                          "Delete Item Category")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllItemCategory() As System.Data.DataTable Implements IItemCategoryController.GetAllItemCategory
            Return _objItemCategoryDA.GetAllItemCategory()
        End Function
        Public Function GetAllItemCategoryByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IItemCategoryController.GetAllItemCategoryByLocation
            Return _objItemCategoryDA.GetAllItemCategoryByLocation(LocationID)
        End Function

        Public Function GetBalance(ByVal CategoryID As String) As System.Data.DataTable Implements IItemCategoryController.GetBalance
            Return _objItemCategoryDA.GetBalance(CategoryID)
        End Function
        Public Function GetItemCategory(ByVal ItemCategoryID As String) As CommonInfo.ItemCategoryInfo Implements IItemCategoryController.GetItemCategory
            Return _objItemCategoryDA.GetItemCategory(ItemCategoryID)
        End Function

        Public Function InsertItemCategory(ByVal obj As CommonInfo.ItemCategoryInfo) As Boolean Implements IItemCategoryController.InsertItemCategory
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            If obj.ItemCategoryID = "0" Then
                obj.ItemCategoryID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.ItemCategory, CommonInfo.EnumSetting.GenerateKeyType.ItemCategory.ToString, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.ItemCategory.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                          obj.ItemCategoryID, _
                                          "Insert Item Category")
                Return _objItemCategoryDA.InsertItemCategory(obj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.ItemCategory.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                          obj.ItemCategoryID, _
                                          "Update Item Category")
                Return _objItemCategoryDA.UpdateItemCategory(obj)
            End If
        End Function

        Public Function GetItemCategoryName(ByVal ItemCategory As String) As CommonInfo.ItemCategoryInfo Implements IItemCategoryController.GetItemCategoryName
            Return _objItemCategoryDA.GetItemCategoryName(ItemCategory)
        End Function

        Public Function GetAllItemCategoryFromSearchBox() As System.Data.DataTable Implements IItemCategoryController.GetAllItemCategoryFromSearchBox
            Return _objItemCategoryDA.GetAllItemCategoryFromSearchBox()
        End Function

        Public Function HasItemCategory(ByVal ItemCategoryName As String, ByVal Prefix As String) As DataTable Implements IItemCategoryController.HasItemCategory
            Return _objItemCategoryDA.HasItemCategory(ItemCategoryName, Prefix)
        End Function

        Public Function HasItemCategoryForUpdate(ByVal ItemCategoryName As String, ByVal OldItemCategory As String, ByVal OldPrefix As String, ByVal OldTax As Integer) As System.Data.DataTable Implements IItemCategoryController.HasItemCategoryForUpdate
            Return _objItemCategoryDA.HasItemCategoryForUpdate(ItemCategoryName, OldItemCategory, OldPrefix, OldTax)
        End Function

        Public Function HasPrefixForUpdate(ByVal prefix As String, ByVal OldPrefix As String) As System.Data.DataTable Implements IItemCategoryController.HasPrefixForUpdate
            Return _objItemCategoryDA.HasPrefixForUpdate(prefix, OldPrefix)
        End Function

        Public Function HasPrefixForUpdateUseItemCode(ByVal ItemCategoryId As String) As System.Data.DataTable Implements IItemCategoryController.HasPrefixForUpdateUseItemCode
            Return _objItemCategoryDA.HasPrefixForUpdateUseItemCode(ItemCategoryId)
        End Function

        Public Function GetrptItemCategory() As DataTable Implements IItemCategoryController.GetrptItemCategory
            Return _objItemCategoryDA.GetrptItemCategory()
        End Function

        Public Function HasItemCategoryAndPrefix(ByVal ItemCategory As String, ByVal Prefix As String, ByVal ItemCategoryID As String) As System.Data.DataTable Implements IItemCategoryController.HasItemCategoryAndPrefix
            Return _objItemCategoryDA.HasItemCategoryAndPrefix(ItemCategory, Prefix, ItemCategoryID)
        End Function


    End Class
End Namespace

