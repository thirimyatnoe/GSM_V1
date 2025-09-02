Imports BusinessRule
Imports CommonInfo
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frm_GoldSmith
    Implements IFormProcess
#Region " Private Properties "
    Private _GoldSmithID As String
    Private _GoldSmithCode As String
    Private _dtGoldSmithItem As DataTable
    Private _IsGoldSmithItemUpdate As Boolean = False
    Private _ConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GoldSmithController As GoldSmith.IGoldSmithController = Factory.Instance.CreateGoldSmithController
    Private _GoldQ As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _IsUpdate As Boolean = False
    Dim GoldTK As Decimal
#End Region
#Region " IFormProcess "
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

        Dim dt As New DataTable
        dt = _GoldSmithController.GetGoldSmithNameListByStock(_GoldSmithID)

        If dt.Rows.Count() > 0 Then

            MsgBox("You can not delete this GoldSmith which has Stock Transaction Records!", MsgBoxStyle.Information, Me.Text)
            Exit Function


        End If

        If _GoldSmithController.DeleteGoldSmith(_GoldSmithID) Then
            Clear()
            btnDelete.Enabled = False
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
        btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objGoldSmithInfo As CommonInfo.GoldSmithInfo
        If IsFillData() Then
            objGoldSmithInfo = Get_Data()
            _GoldSmithID = _GoldSmithController.SaveGoldSmith(objGoldSmithInfo)
            If _GoldSmithID <> "" Then
                Clear()
                Return True
            Else
                Return False
            End If
        End If
    End Function
#End Region
#Region " Private Methods "

#End Region

    Private Sub frm_GoldSmith_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyBase.MaximizeBox = False
        Clear()
    End Sub
    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        txtAddress.Clear()
        txtName.Clear()
        txtRemark.Clear()
        txtPhone.Clear()
        txtGoldSmithID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.GoldSmith, EnumSetting.GenerateKeyType.GoldSmith.ToString, Now)
        txtGoldSmithCode.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.GoldSmithCode, EnumSetting.GenerateKeyType.GoldSmithCode, Now)
        _GoldSmithID = ""
        _GoldSmithCode = ""
        _IsUpdate = False
        txtName.Focus()
    End Sub
    Private Function Get_Data() As CommonInfo.GoldSmithInfo
        Dim objGoldSmithInfo As New CommonInfo.GoldSmithInfo
        With objGoldSmithInfo
            .GoldSmithID = _GoldSmithID
            .GoldSmithCode = _GoldSmithCode
            .Name = txtName.Text
            .Address = IIf(txtAddress.Text = "", "-", txtAddress.Text)
            .Phone = IIf(txtPhone.Text = "", "-", txtPhone.Text)
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .IsInactive = chkInactive.Checked
        End With
        Return objGoldSmithInfo
    End Function

    Private Function IsFillData() As Boolean

        If txtName.Text = "" Then
            MessageBox.Show("Please fill data in  GoldSmith Name textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Return False
        End If

        'If txtAddress.Text = "" Then
        '    MessageBox.Show("Please fill data in  GoldSmith Address textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    txtAddress.Focus()
        '    Return False
        'End If
        Return True
    End Function

    Private Sub Show_Data(ByVal objGoldSmithInfo As GoldSmithInfo)
        If Not IsNothing(objGoldSmithInfo) Then


            With objGoldSmithInfo
                txtGoldSmithID.Text = .GoldSmithID
                txtGoldSmithCode.Text = .GoldSmithCode
                txtName.Text = .Name
                txtAddress.Text = .Address
                txtPhone.Text = .Phone
                txtRemark.Text = .Remark
                chkInactive.Checked = IIf(CBool(.IsInactive) = True, True, False)

            End With


        Else
            txtGoldSmithID.Text = "Auto Generate"
            _GoldSmithCode = ""
            _GoldSmithID = ""
            txtName.Text = ""
            txtAddress.Text = ""
            txtPhone.Text = ""
            txtRemark.Text = ""
            txtPhone.Text = ""
            chkInactive.Checked = False
        End If
    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Dim DataItem As DataRow
        Dim dtGS As New DataTable
        Dim tmpObjGoldSmithInfo As New GoldSmithInfo

        dtGS = _GoldSmithController.GetAllGoldSmith()
        DataItem = DirectCast(SearchData.FindFast(dtGS, "GoldSmith List"), DataRow)

        If DataItem IsNot Nothing Then
            _GoldSmithID = DataItem.Item("@GoldSmithID").ToString()
            _GoldSmithCode = DataItem.Item("GoldSmithCode").ToString()
            tmpObjGoldSmithInfo = _GoldSmithController.GetGoldSmithByID(_GoldSmithID)
            Show_Data(tmpObjGoldSmithInfo)
            _IsUpdate = True
            txtGoldSmithCode.Enabled = True
            btnDelete.Enabled = True
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        End If
    End Sub

    Private Sub txtGoldSmithCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGoldSmithCode.TextChanged, txtGoldSmithCode.TextChanged
        Dim objGoldSmith As GoldSmithInfo
        If txtGoldSmithCode.Text <> "" Then
            objGoldSmith = _GoldSmithController.GetGoldSmithCode(txtGoldSmithCode.Text)
            If objGoldSmith.GoldSmithID <> "" Then
                _GoldSmithID = objGoldSmith.GoldSmithID
                objGoldSmith = _GoldSmithController.GetGoldSmithByID(_GoldSmithID)
                Show_Data(objGoldSmith)
                txtGoldSmithCode.Enabled = True
                btnDelete.Enabled = True
                btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
            End If
        Else
            Show_Data(Nothing)
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("GoldSmith")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class
