Imports CommonInfo
Imports BusinessRule
Public Class frm_ItemCategory

    Implements IFormProcess
    Dim dtResult As New DataTable
    Dim IsSave As String = ""
    Dim OldItemCategory As String
    Dim OldPrefix As String
    Dim OldTax As Integer


    Private _ItemCategoryID As String = ""
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController


#Region "Private Methods"
    Private Sub Clear_Data()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _ItemCategoryID = "0"

        txtItemCategoryID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.ItemCategory, EnumSetting.GenerateKeyType.ItemCategory.ToString, Now)
        txtItemCategory.Text = ""
        ' txtItemCategory.Font = New Font("Myanmar3", 9.5)
        txtPrefix.Text = ""
        OldItemCategory = ""
        OldPrefix = ""
        txtTax.Text = ""


        IsSave = "Save"
        txtItemCategory.Focus()
    End Sub
    Private Function Get_Data() As CommonInfo.ItemCategoryInfo
        Dim objItemCategoryInfo As New CommonInfo.ItemCategoryInfo
        With objItemCategoryInfo
            .ItemCategoryID = _ItemCategoryID
            .ItemCategory = txtItemCategory.Text
            .Prefix = txtPrefix.Text
            .ItemTaxPer = Val(txtTax.Text)
        End With
        Return objItemCategoryInfo
    End Function
    Private Sub Show_Data(ByVal ItemCategoryObj As CommonInfo.ItemCategoryInfo)
        With ItemCategoryObj
            txtItemCategoryID.Text = .ItemCategoryID
            txtItemCategory.Text = .ItemCategory
            txtPrefix.Text = .Prefix
            OldItemCategory = .ItemCategory
            OldPrefix = .Prefix
            txtTax.Text = Format(.ItemTaxPer, "###,##0.##")

        End With
    End Sub
#End Region

    Private Sub frm_ItemCategory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtItemCategory.Focus()
    End Sub
    Private Sub frm_ItemCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "ပစ္စည်းအမျိုးအစား ထည့်ခြင်း"
        MyBase.MaximizeBox = False
        Clear_Data()
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

        Dim dt As New DataTable
        dt = objGeneralController.CheckRecordsExistOrNot("tbl_ForSale", "tbl_RepairDetail", "ItemCategoryID", _ItemCategoryID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Item Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If

        dt = objGeneralController.CheckRecordsExistOrNot("tbl_OrderReceiveDetail", "tbl_PurchaseDetail", "ItemCategoryID", _ItemCategoryID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Item Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If

        dt = objGeneralController.CheckRecordsExistOrNot("tbl_SalesVolumeDetail", "", "ItemCategoryID", _ItemCategoryID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Item Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If

        If _ItemCategoryController.DeleteItemCategory(_ItemCategoryID) = True Then
            Clear_Data()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear_Data()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            If IsSave = "Update" Then
                Dim dt As New DataTable
                Dim dtPrefix As New DataTable
                'Dim dtitemCode As New DataTable
                Dim dtItemCat As New DataTable
                Dim dtItem As New DataTable

                Dim objItemCategoryInfo As CommonInfo.ItemCategoryInfo

                objItemCategoryInfo = Get_Data()
                dtPrefix = _ItemCategoryController.HasPrefixForUpdate(txtPrefix.Text, OldPrefix)
                dt = _ItemCategoryController.HasItemCategoryForUpdate(txtItemCategory.Text, OldItemCategory, OldPrefix, OldTax)
                ''dtitemCode = _ItemCategoryController.HasPrefixForUpdateUseItemCode(_ItemCategoryID)

                If dtPrefix.Rows.Count = 0 Then
                    If dt.Rows.Count = 0 Then
                        If _ItemCategoryController.InsertItemCategory(objItemCategoryInfo) Then
                            Clear_Data()
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        MsgBox("Item Category  is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                    End If
                Else
                    MsgBox(" Prefix is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                End If

            Else
                dtResult = _ItemCategoryController.HasItemCategory(txtItemCategory.Text, txtPrefix.Text)
                If dtResult.Rows.Count = 0 Then
                    Dim objItemCategoryInfo As CommonInfo.ItemCategoryInfo
                    objItemCategoryInfo = Get_Data()
                    If _ItemCategoryController.InsertItemCategory(objItemCategoryInfo) Then
                        Clear_Data()
                        Return True
                    Else
                        Return False
                    End If
                Else
                    MsgBox("Item Category or Prefix is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                End If
            End If
        End If
    End Function

    Private Sub SearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)

        Dim dtCategory As New DataTable
        Dim DataItem As DataRow
        Dim objItemCategory As CommonInfo.ItemCategoryInfo
        dtCategory = _ItemCategoryController.GetAllItemCategory()
        DataItem = DirectCast(SearchData.FindFast(dtCategory, "ItemCategory List"), DataRow)
        If DataItem IsNot Nothing Then
            _ItemCategoryID = DataItem.Item("@ItemCategoryID").ToString()
            objItemCategory = _ItemCategoryController.GetItemCategory(_ItemCategoryID)

            Show_Data(objItemCategory)
            IsSave = "Update"
            btnDelete.Enabled = True
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()

        Else
            IsSave = "Save"
            btnDelete.Enabled = False
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
        End If
    End Sub

    Private Function IsFillData() As Boolean
        If txtItemCategory.Text = "" Then
            MessageBox.Show("Please fill data in textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtItemCategory.Focus()
            Return False
        ElseIf txtPrefix.Text = "" Then
            MessageBox.Show("Please fill data in textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPrefix.Focus()
            Return False
        ElseIf txtTax.Text = "" Then
            MessageBox.Show("Please fill data in textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtTax.Focus()
            Return False

        End If
        Return True
    End Function

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("ItemCategory")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTax.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub


 
    Private Sub txtTax_TextChanged(sender As Object, e As EventArgs) Handles txtTax.TextChanged

    End Sub
End Class