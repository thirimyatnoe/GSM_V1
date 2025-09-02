Imports BusinessRule
Imports CommonInfo
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frm_Customer
    Implements IFormProcess
#Region " Private Properties "
    Private _CustomerID As String
    Private _CustomerCode As String
    'Private _CustomerAdvancedItemID As String
    Private _dtCustomerItem As DataTable
    Private _IsCustomerItemUpdate As Boolean = False
    Private _ConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _CustomerController As Customer.ICustomerController = Factory.Instance.CreateCustomerController
    Private _GoldQ As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _IsUpdate As Boolean = False
    Dim GoldTK As Decimal
#End Region
#Region " IFormProcess "
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

        Dim dt As New DataTable
        Dim dt1 As New DataTable

        dt = objGeneralController.CheckRecordsExistOrNot("tbl_PurchaseHeader", "tbl_SaleInvoiceHeader", "CustomerID", _CustomerID)
        dt1 = objGeneralController.CheckRecordsExistOrNot("tbl_SaleGems", "tbl_OrderInvoice", "CustomerID", _CustomerID)
        If dt.Rows.Count() > 0 Then
            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Customer which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            ElseIf CInt(dt1.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Customer which has Transaction Records!", MsgBoxStyle.Information)
                Exit Function
            End If

        End If

        If _CustomerController.DeleteCustomer(_CustomerID) Then
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
        Dim objCustomerInfo As CommonInfo.CustomerInfo
        If IsFillData() Then
            objCustomerInfo = Get_Data()
            _CustomerID = _CustomerController.SaveCustomer(objCustomerInfo)
            If _CustomerID <> "" Then
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

    Private Sub frm_Customer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyBase.MaximizeBox = False
        Clear()
    End Sub
    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        txtAddress.Clear()
        txtName.Clear()
        txtPhone.Clear()
        txtRemark.Clear()

        txtCustomerID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.Customer, EnumSetting.GenerateKeyType.Customer.ToString, Now)
        txtCustomerCode.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.CustomerCode, EnumSetting.GenerateKeyType.CustomerCode, Now)
        _CustomerID = ""
        _CustomerCode = ""
        _IsUpdate = False
        txtName.Focus()
    End Sub
    Private Function Get_Data() As CommonInfo.CustomerInfo
        Dim objCustomerInfo As New CommonInfo.CustomerInfo
        With objCustomerInfo
            .CustomerID = _CustomerID
            .CustomerCode = _CustomerCode
            .CustomerName = txtName.Text
            .CustomerAddress = IIf(txtAddress.Text = "", "-", txtAddress.Text)
            .CustomerTel = IIf(txtPhone.Text = "", "-", txtPhone.Text)
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .IsInactive = chkInactive.Checked
        End With
        Return objCustomerInfo
    End Function

    Private Function IsFillData() As Boolean

        If txtName.Text = "" Then
            MessageBox.Show("Please fill data in  Customer Name textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Return False
        End If

        If txtAddress.Text = "" Then
            MessageBox.Show("Please fill data in  Customer Address textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAddress.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub Show_Data(ByVal objCustomerInfo As CustomerInfo)
        If Not IsNothing(objCustomerInfo) Then


            With objCustomerInfo
                txtCustomerID.Text = .CustomerID
                txtCustomerCode.Text = .CustomerCode
                txtName.Text = .CustomerName
                txtAddress.Text = .CustomerAddress
                txtPhone.Text = .CustomerTel
                txtRemark.Text = .Remark
                chkInactive.Checked = IIf(CBool(.IsInactive) = True, True, False)

            End With
        Else
            txtCustomerID.Text = "Auto Generate"
            _CustomerCode = ""
            _CustomerID = ""
            txtName.Text = ""
            txtAddress.Text = ""
            txtPhone.Text = ""
            txtRemark.Text = ""
            chkInactive.Checked = False
        End If
    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Dim DataItem As DataRow
        Dim dtGS As New DataTable
        Dim tmpObjCustomerInfo As New CustomerInfo

        dtGS = _CustomerController.GetAllCustomer()
        DataItem = DirectCast(SearchData.FindFast(dtGS, "Customer List"), DataRow)

        If DataItem IsNot Nothing Then
            _CustomerID = DataItem.Item("@CustomerID").ToString()
            _CustomerCode = DataItem.Item("CustomerCode").ToString()
            tmpObjCustomerInfo = _CustomerController.GetCustomerByID(_CustomerID)
            Show_Data(tmpObjCustomerInfo)
            _IsUpdate = True
            txtCustomerCode.Enabled = True
            btnDelete.Enabled = True
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        End If
    End Sub

    Private Sub txtCustomerCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerCode.TextChanged
        Dim objCustomer As CustomerInfo
        If txtCustomerCode.Text <> "" Then
            objCustomer = _CustomerController.GetCustomerCode(txtCustomerCode.Text)
            If objCustomer.CustomerID <> "" Then
                _CustomerID = objCustomer.CustomerID
                objCustomer = _CustomerController.GetCustomerByID(_CustomerID)
                Show_Data(objCustomer)
                txtCustomerCode.Enabled = True
                btnDelete.Enabled = True
                btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
            End If
        Else
            Show_Data(Nothing)
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("Customer")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class
