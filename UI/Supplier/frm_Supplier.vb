Imports BusinessRule
Imports CommonInfo
Imports System.IO

Public Class frm_Supplier
    Implements IFormProcess
#Region " Private Properties "
    Private _SupplierID As String
    Private _SupplierCode As String
    Private _SupplierController As Supplier.ISupplierController = Factory.Instance.CreateSupplierController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _IsUpdate As Boolean = False
#End Region
#Region " IFormProcess "
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

        If _SupplierController.DeleteSupplier(_SupplierID) Then
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
        Dim objSupplierInfo As CommonInfo.SupplierInfo
        If IsFillData() Then
            objSupplierInfo = Get_Data()
            _SupplierID = _SupplierController.SaveSupplier(objSupplierInfo)
            If _SupplierID <> "" Then
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

    Private Sub frm_Supplier_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyBase.MaximizeBox = False
        Clear()
    End Sub
    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        txtAddress.Clear()
        txtName.Clear()
        txtPhone.Clear()
        txtRemark.Clear()
        txtEmail.Clear()
        txtWebsite.Clear()

        txtSupplierID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.Supplier, EnumSetting.GenerateKeyType.Supplier.ToString, Now)
        txtSupplierCode.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.SupplierCode, EnumSetting.GenerateKeyType.SupplierCode, Now)
        _SupplierID = ""
        _SupplierCode = ""
        _IsUpdate = False
        txtName.Focus()
    End Sub
    Private Function Get_Data() As CommonInfo.SupplierInfo
        Dim objSupplierInfo As New CommonInfo.SupplierInfo
        With objSupplierInfo
            .SupplierID = _SupplierID
            .SupplierCode = _SupplierCode
            .SupplierName = txtName.Text
            .SupplierAddress = IIf(txtAddress.Text = "", "-", txtAddress.Text)
            .PhoneNo = IIf(txtPhone.Text = "", "-", txtPhone.Text)
            .Email = IIf(txtEmail.Text = "", "-", txtEmail.Text)
            .Website = IIf(txtWebsite.Text = "", "-", txtWebsite.Text)
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .LocationID = Global_CurrentLocationID
        End With
        Return objSupplierInfo
    End Function

    Private Function IsFillData() As Boolean

        If txtName.Text = "" Then
            MessageBox.Show("Please fill data in  Supplier Name textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Return False
        End If
        If txtAddress.Text = "" Then
            MessageBox.Show("Please fill data in  Supplier Address textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAddress.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub Show_Data(ByVal objSupplierInfo As SupplierInfo)
        If Not IsNothing(objSupplierInfo) Then
            With objSupplierInfo
                txtSupplierID.Text = .SupplierID
                txtSupplierCode.Text = .SupplierCode
                txtName.Text = .SupplierName
                txtAddress.Text = .SupplierAddress
                txtPhone.Text = .PhoneNo
                txtEmail.Text = .Email
                txtWebsite.Text = .Website
                txtRemark.Text = .Remark
            End With
        End If
    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Dim DataItem As DataRow
        Dim dtGS As New DataTable
        Dim tmpObjSupplierInfo As New SupplierInfo

        dtGS = _SupplierController.GetAllSupplier()
        DataItem = DirectCast(SearchData.FindFast(dtGS, "Supplier List"), DataRow)

        If DataItem IsNot Nothing Then
            _SupplierID = DataItem.Item("@SupplierID").ToString()
            _SupplierCode = DataItem.Item("SupplierCode").ToString()
            tmpObjSupplierInfo = _SupplierController.GetSupplierByID(_SupplierID)
            Show_Data(tmpObjSupplierInfo)
            _IsUpdate = True
            txtSupplierCode.Enabled = True
            btnDelete.Enabled = True
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        End If
    End Sub

    Private Sub txtSupplierCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplierCode.TextChanged
        Dim objSupplier As SupplierInfo
        If txtSupplierCode.Text <> "" Then
            objSupplier = _SupplierController.GetSupplierDataByCode(txtSupplierCode.Text)
            If objSupplier.SupplierID <> "" Then
                _SupplierID = objSupplier.SupplierID
                objSupplier = _SupplierController.GetSupplierByID(_SupplierID)
                Show_Data(objSupplier)
                txtSupplierCode.Enabled = True
                btnDelete.Enabled = True
                btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
            End If
        Else
            Show_Data(Nothing)
        End If
    End Sub
    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("Supplier")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class

