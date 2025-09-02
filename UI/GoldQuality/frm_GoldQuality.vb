Imports CommonInfo
Imports BusinessRule
Public Class frm_GoldQuality
    Implements IFormProcess
    Private _GoldQualityID As String = ""
    Private _IsUpdate As Boolean = False
    Dim OldGoldQuality As String
    Dim OldPrefix As String
    Dim dtResult As New DataTable

    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController

    Private Sub frm_GoldQuality_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtGoldQuality.Focus()
    End Sub
    Private Sub frm_GoldQuality_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "ရွှေရည်အမျိုးအစား ထည့်ခြင်း"
        MyBase.MaximizeBox = False
        Clear_Data()
    End Sub
#Region "Private Methods"
    Private Sub Clear_Data()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _GoldQualityID = "0"
        txtGoldQualityID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.GoldQuality, EnumSetting.GenerateKeyType.GoldQuality.ToString, Now)
        txtGoldQuality.Text = ""
        txtPrefix.Text = ""
        OldGoldQuality = ""
        OldPrefix = ""
        txtGoldQuality.Focus()
        rdbKPY.Checked = True
        _IsUpdate = False
        txtDividedBy.Text = ""
        txtMultiplyBy.Text = ""
        txtResult.Text = ""
        chkSolidGold.Checked = False
        If _GoldQualityController.CheckIsExitSolidGoldInGoldQuality() Then
            chkSolidGold.Enabled = False
        Else
            chkSolidGold.Enabled = True
        End If
        CboBarStatus.Text = "Default"
    End Sub
    Private Function Get_Data() As CommonInfo.GoldQualityInfo
        Dim objGoldQualityInfo As New CommonInfo.GoldQualityInfo
        With objGoldQualityInfo
            .GoldQualityID = _GoldQualityID
            .GoldQuality = txtGoldQuality.Text
            .Prefix = txtPrefix.Text
            If rdbGram.Checked Then
                .IsGramRate = True
            Else
                .IsGramRate = False
            End If
            .DividedBy = IIf(txtDividedBy.Text = "", 0, txtDividedBy.Text)
            .MultiplyBy = IIf(txtMultiplyBy.Text = "", 0, txtMultiplyBy.Text)
            .IsSolidGold = chkSolidGold.Checked

            If CboBarStatus.Text = "Show KPY" Then
                .BarcodeStatus = 1
            ElseIf CboBarStatus.Text = "Show Gram" Then
                .BarcodeStatus = 2
            Else
                .BarcodeStatus = 0
            End If
            End With
        Return objGoldQualityInfo
    End Function
    Private Sub Show_Data(ByVal GoldQualityObj As CommonInfo.GoldQualityInfo)
        With GoldQualityObj
            txtGoldQualityID.Text = .GoldQualityID
            txtGoldQuality.Text = .GoldQuality
            txtPrefix.Text = .Prefix
            OldGoldQuality = .GoldQuality
            OldPrefix = .Prefix
            If .IsGramRate = True Then
                rdbGram.Checked = True
                rdbKPY.Checked = False
                txtMultiplyBy.Text = ""
                txtDividedBy.Text = ""
                txtResult.Text = ""
                grpCalculatePrice.Enabled = False
            Else
                rdbGram.Checked = False
                rdbKPY.Checked = True
                grpCalculatePrice.Enabled = True
            End If

            chkSolidGold.Checked = .IsSolidGold
            If chkSolidGold.Checked Then
                chkSolidGold.Enabled = True
            End If

            If .MultiplyBy > 0 Then
                txtMultiplyBy.Text = Val(.MultiplyBy)
            Else
                txtMultiplyBy.Text = ""
            End If

            If .DividedBy > 0 Then
                txtDividedBy.Text = Val(.DividedBy)
            Else
                txtDividedBy.Text = ""
            End If

            If .DividedBy > 0 Then
                If .MultiplyBy > 0 Then
                    txtResult.Text = "(" & "အခေါက်စျေး/" & Val(.DividedBy) & ") *" & Val(.MultiplyBy)
                Else
                    txtResult.Text = "အခေါက်စျေး/" & Val(.DividedBy)
                End If
            Else
                If .MultiplyBy > 0 Then
                    txtResult.Text = "အခေါက်စျေး*" & Val(.MultiplyBy)
                Else
                    txtResult.Text = ""
                End If
            End If
           
            If .BarcodeStatus = 1 Then
                CboBarStatus.Text = "Show KPY"
            ElseIf .BarcodeStatus = 2 Then
                CboBarStatus.Text = "Show Gram"
            Else
                CboBarStatus.Text = "Default"
            End If
        End With
    End Sub

    Private Function IsFillData() As Boolean

        If txtGoldQuality.Text = "" Then
            MessageBox.Show("Please fill data in  Gold Quality Name textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtGoldQuality.Focus()
            Return False
        End If

        If chkSolidGold.Checked Then
            If _IsUpdate Then
                If _GoldQualityController.CheckIsExitSolidGoldInGoldQuality(_GoldQualityID) Then
                    MsgBox("Cannot Allow To Save Duplicate SolidGold!", MsgBoxStyle.Information, "Data Duplicated !")
                    chkSolidGold.Focus()
                    Return False
                End If
            Else
                If _GoldQualityController.CheckIsExitSolidGoldInGoldQuality() Then
                    MsgBox("Cannot Allow To Save Duplicate SolidGold!", MsgBoxStyle.Information, "Data Duplicated !")
                    chkSolidGold.Focus()
                    Return False
                End If
            End If
        End If

        If _IsUpdate = True Then
            Dim dt As New DataTable
            Dim dtPrefix As New DataTable
            dt = _GoldQualityController.HasGoldQualityForUpdate(txtGoldQuality.Text, _GoldQualityID)

            If dt.Rows.Count > 0 Then
                MsgBox("GoldQuality  is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                txtGoldQuality.Select()
                Return False
            End If

            dtPrefix = _GoldQualityController.HasPrefixForUpdate(txtPrefix.Text, _GoldQualityID)
            If dtPrefix.Rows.Count > 0 Then
                MsgBox(" Prefix is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                txtPrefix.Select()
                Return False
            End If
        Else

            dtResult = _GoldQualityController.HasGoldQuality(txtGoldQuality.Text, txtPrefix.Text)
            If dtResult.Rows.Count > 0 Then
                MsgBox("GoldQuality or Prefix is duplicated! Save with another one!", MsgBoxStyle.Information, "Data Duplicated !")
                Return False
            End If
        End If

        If chkSolidGold.Checked And rdbGram.Checked Then
            MsgBox("SolidGold Can not Allow Gram-Rate!", MsgBoxStyle.Information, "Invalid Data !")
            Return False
        End If

        Return True
    End Function
#End Region

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

        Dim dt As New DataTable
        dt = objGeneralController.CheckRecordsExistOrNot("tbl_OrderInvoice", "", "PayGoldQualityID", _GoldQualityID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Gold Quality which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If

        dt = objGeneralController.CheckRecordsExistOrNot("tbl_ForSale", "tbl_RepairDetail", "GoldQualityID", _GoldQualityID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Item Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If

        dt = objGeneralController.CheckRecordsExistOrNot("tbl_OrderReceiveDetail", "tbl_PurchaseDetail", "GoldQualityID", _GoldQualityID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Item Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If

        dt = objGeneralController.CheckRecordsExistOrNot("tbl_SalesVolumeDetail", "", "GoldQualityID", _GoldQualityID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Item Category which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If
        End If

        If _GoldQualityController.DeleteGoldQuality(_GoldQualityID) Then
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
        Dim objGoldQualityInfo As CommonInfo.GoldQualityInfo

        If IsFillData() Then
            objGoldQualityInfo = Get_Data()
            If _GoldQualityController.InsertGoldQuality(objGoldQualityInfo) Then
                Clear_Data()
                Return True
            Else
                Return False
            End If
        End If
      
    End Function
    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        Dim dtGold As New DataTable
        Dim DataItem As DataRow
        Dim objGoldQualityInfo As New GoldQualityInfo

        dtGold = _GoldQualityController.GetAllGoldQuality()
        DataItem = DirectCast(SearchData.FindFast(dtGold, "GoldQuality List"), DataRow)
        If DataItem IsNot Nothing Then
            _GoldQualityID = DataItem.Item("@GoldQualityID").ToString()
            objGoldQualityInfo = _GoldQualityController.GetGoldQuality(_GoldQualityID)
            Show_Data(objGoldQualityInfo)
            _IsUpdate = True
            btnDelete.Enabled = True
        Else
            _IsUpdate = False
            btnSave.Text = "Save"
            btnDelete.Enabled = False

        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("GoldQuality")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtMultiplyBy_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMultiplyBy.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtDividedBy_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDividedBy.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub chkSolidGold_CheckedChanged(sender As Object, e As EventArgs) Handles chkSolidGold.CheckedChanged
        If chkSolidGold.Checked Then
            txtMultiplyBy.Text = ""
            txtDividedBy.Text = ""
            rdbKPY.Checked = True
            rdbGram.Enabled = False
            txtResult.Text = ""
            grpCalculatePrice.Enabled = False
        Else
            grpCalculatePrice.Enabled = True
            rdbGram.Enabled = True
        End If
    End Sub

    Private Sub txtDividedBy_TextChanged(sender As Object, e As EventArgs) Handles txtDividedBy.TextChanged
        ShowResultData()
    End Sub

    Private Sub txtMultiplyBy_TextChanged(sender As Object, e As EventArgs) Handles txtMultiplyBy.TextChanged
        ShowResultData()
    End Sub

    Private Sub ShowResultData()
        If txtDividedBy.Text <> "" And txtDividedBy.Text <> "0" Then
            If txtMultiplyBy.Text <> "" And txtMultiplyBy.Text <> "" Then
                txtResult.Text = "(" & "အခေါက်စျေး/" & CDec(txtDividedBy.Text) & ") *" & CDec(txtMultiplyBy.Text)
            Else
                txtResult.Text = "အခေါက်စျေး/" & CDec(txtDividedBy.Text)
            End If
        Else
            If txtMultiplyBy.Text <> "" And txtMultiplyBy.Text <> "" Then
                txtResult.Text = "အခေါက်စျေး*" & CDec(txtMultiplyBy.Text)
            Else
                txtResult.Text = ""
            End If
        End If
    End Sub

    Private Sub rdbGram_CheckedChanged(sender As Object, e As EventArgs) Handles rdbGram.CheckedChanged
        If rdbGram.Checked Then
            txtMultiplyBy.Text = ""
            txtDividedBy.Text = ""
            txtResult.Text = ""
            grpCalculatePrice.Enabled = False
        Else
            grpCalculatePrice.Enabled = True
        End If
    End Sub
End Class