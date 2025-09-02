Imports BusinessRule
Imports CommonInfo
Public Class frm_GemsCategory
    Implements IFormProcess
    Private _GemsCategoryID As String = ""
    Dim _IsUpdate As Boolean = False
    Private OldPrefix As String = ""

    Private _GemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController

#Region "Private Methods"
    Private Sub Clear_Data()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)

        _GemsCategoryID = "0"
        txtGemsCategoryID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.GemsCategory, EnumSetting.GenerateKeyType.GemsCategory.ToString, Now)
        txtGemsCategory.Text = ""
        _IsUpdate = False
        txtTax.Text = ""
        txtPrefix.Text = ""
        OldPrefix = ""
        txtGemsCategory.Focus()
    End Sub
    Private Function Get_Data() As CommonInfo.GemsCategoryInfo
        Dim objGemsCategoryInfo As New CommonInfo.GemsCategoryInfo
        With objGemsCategoryInfo
            .GemsCategoryID = _GemsCategoryID
            .GemsCategory = txtGemsCategory.Text
            .GemTaxPer = Val(txtTax.Text)
            .Prefix = txtPrefix.Text
        End With
        Return objGemsCategoryInfo
    End Function
    Private Sub Show_Data(ByVal GemsCategoryObj As CommonInfo.GemsCategoryInfo)
        With GemsCategoryObj
            txtGemsCategoryID.Text = .GemsCategoryID
            txtGemsCategory.Text = .GemsCategory
            txtTax.Text = Format(.GemTaxPer, "###,##0.##")
            OldPrefix = .Prefix
            txtPrefix.Text = .Prefix
        End With
    End Sub

    Private Function IsFillData() As Boolean

        If txtGemsCategory.Text = "" Then
            MessageBox.Show("Please fill data in textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtGemsCategory.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

    Private Sub frm_GemsCategory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtGemsCategory.Focus()
    End Sub


    Private Sub frm_GemsCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "ကျောက်အမျိုးအစား ထည့်ခြင်း"
        MyBase.MaximizeBox = False
        Clear_Data()
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

        Dim dt As New DataTable
        dt = objGeneralController.CheckRecordsExistOrNot("tbl_ForSaleGemsItem", "tbl_PurchaseGem", "GemsCategoryID", _GemsCategoryID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Gems Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If
        dt = objGeneralController.CheckRecordsExistOrNot("tbl_OrderReturnGemsItem", "tbl_ReturnRepairGem", "GemsCategoryID", _GemsCategoryID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Gems Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If

        End If

        dt = objGeneralController.CheckRecordsExistOrNot("tbl_SaleGemsItem", "tbl_SalesInvoiceGemItem", "GemsCategoryID", _GemsCategoryID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Gems Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If

        End If

        If _GemsCategoryController.DeleteGemsCategory(_GemsCategoryID) Then
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
        Dim objGemsCategoryInfo As CommonInfo.GemsCategoryInfo
        Dim dtGemCate As New DataTable
        Dim dtPrefix As New DataTable

        If IsFillData() Then
            objGemsCategoryInfo = Get_Data()
            If _IsUpdate = True Then
                Dim dt As New DataTable
                dtPrefix = _GemsCategoryController.HasPrefixForUpdate(txtPrefix.Text, OldPrefix)

                dt = _GemsCategoryController.GetGemsCategoryByGemsCategory(objGemsCategoryInfo.GemsCategory, objGemsCategoryInfo.GemsCategoryID)
                If dtPrefix.Rows.Count = 0 Then
                    If dt.Rows.Count = 0 Then
                        If _GemsCategoryController.InsertGemsCategory(objGemsCategoryInfo) Then
                            Clear_Data()
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        MsgBox("GemsCategory  is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                    End If
                Else
                    MsgBox(" Prefix is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                End If
            Else
                Dim dtGems As New DataTable
                dtGems = _GemsCategoryController.GetGemsCategoryByGemsCategory(objGemsCategoryInfo.GemsCategory)
                If dtGems.Rows.Count = 0 Then
                    If _GemsCategoryController.InsertGemsCategory(objGemsCategoryInfo) Then
                        Clear_Data()
                        Return True
                    Else
                        Return False
                    End If
                Else
                    MsgBox("GemsCategory  is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                End If
            End If




        End If

    End Function

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click

        Dim dtGems As New DataTable
        Dim DataItem As DataRow
        Dim objGemsCategoryInfo As New GemsCategoryInfo
        dtGems = _GemsCategoryController.GetAllGemsCategory()
        DataItem = DirectCast(SearchData.FindFast(dtGems, "GemsCategory List"), DataRow)
        If DataItem IsNot Nothing Then
            _GemsCategoryID = DataItem.Item("@GemsCategoryID").ToString()
            objGemsCategoryInfo = _GemsCategoryController.GetGemsCategory(_GemsCategoryID)
            Show_Data(objGemsCategoryInfo)
            _IsUpdate = True
            MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("GemsCategory")
    End Sub

    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTax.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub



End Class