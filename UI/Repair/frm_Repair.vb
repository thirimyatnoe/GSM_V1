Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms

Public Class frm_Repair
    Implements IFormProcess

    Dim _LocationID As String
    Private _RepairID As String = ""
    Private _RepairDetailID As String = ""
    Private _CustomerID As String = ""
    Private itemid As String = ""
    Private ItemCategoryPrefix As String = ""
    Private _IsGram As Boolean = False
    Private _Prefix As Boolean = False
    Private _IsUpdate As Boolean = False
    Private _BarcodeNo As String = ""
    Private _IsExit As Boolean = False
    Private _dtRepairDetail As DataTable

    Dim _ItemTG As Decimal = 0.0
    Dim _ItemTK As Decimal = 0.0
    Dim numberformat As Integer

    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False

    Private _RepairController As BusinessRule.Repair.IRepairController = Factory.Instance.CreateRepairController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _StaffController As BusinessRule.Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _ItemCategoryController As BusinessRule.ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As BusinessRule.GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemNameController As BusinessRule.ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _GeneralController As BusinessRule.General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private _CurrentController As BusinessRule.CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private objConverterController As BusinessRule.Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _objSalesItemInvoiceController As BusinessRule.SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController

#Region " Auto Completion ComboBox "
    Private Sub AutoCompleteCombo_KeyUp(ByVal cbo As ComboBox, ByVal e As KeyEventArgs)
        Dim sTypedText As String
        Dim iFoundIndex As Integer
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        'Allow select keys without Autocompleting

        Select Case e.KeyCode
            Case Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.Down
                Return
        End Select

        'Get the Typed Text and Find it in the list

        sTypedText = cbo.Text
        iFoundIndex = cbo.FindString(sTypedText)

        'If we found the Typed Text in the list then Autocomplete

        If iFoundIndex >= 0 Then

            'Get the Item from the list (Return Type depends if Datasource was bound 

            ' or List Created)

            oFoundItem = cbo.Items(iFoundIndex)

            'Use the ListControl.GetItemText to resolve the Name in case the Combo 

            ' was Data bound

            sFoundText = cbo.GetItemText(oFoundItem)

            'Append then found text to the typed text to preserve case

            sAppendText = sFoundText.Substring(sTypedText.Length)
            cbo.Text = sTypedText & sAppendText

            'Select the Appended Text

            If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
            If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length

        End If

    End Sub

    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox)
        Dim iFoundIndex As Integer

        iFoundIndex = cbo.FindStringExact(cbo.Text)

        cbo.SelectedIndex = iFoundIndex

    End Sub
    Private Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox, ByVal e As String)
        'sya 2/10/2008
        Dim sTypedText As String = ""
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        Dim iFoundIndex As Integer
        iFoundIndex = cbo.FindStringExact(cbo.Text)

        If iFoundIndex = -1 Then
            sTypedText = cbo.Text
            iFoundIndex = cbo.FindString(sTypedText)

            If iFoundIndex = -1 Then
                If sTypedText.IndexOf(" ") < 0 Then
                    iFoundIndex = -1
                Else
                    sTypedText = sTypedText.Remove(sTypedText.IndexOf(" "))
                    iFoundIndex = cbo.FindString(sTypedText)
                End If
            End If

            If iFoundIndex >= 0 Then
                oFoundItem = cbo.Items(iFoundIndex)
                sFoundText = cbo.GetItemText(oFoundItem)
                sAppendText = sFoundText.Substring(sTypedText.Length)
                cbo.Text = sTypedText & sAppendText

                If cbo.Text.Length <> sTypedText.Length Then cbo.SelectionStart = sTypedText.Length
                If sAppendText.Length <> 0 Then cbo.SelectionLength = sAppendText.Length
            End If
        Else
            cbo.SelectedIndex = iFoundIndex
        End If

        If iFoundIndex = -1 Then
            For i As Integer = 0 To cbo.Items.Count - 1
                oFoundItem = cbo.Items(i)
                sFoundText = cbo.GetItemText(oFoundItem)

                If sFoundText.IndexOf(" ") < 0 Then
                    iFoundIndex = -1
                Else
                    sFoundText = sFoundText.Remove(sFoundText.IndexOf(" "))
                    iFoundIndex = cbo.FindString(sFoundText)
                End If

                If sTypedText.Contains(sFoundText) = True Then
                    iFoundIndex = i
                    cbo.SelectedIndex = i
                    Exit For
                Else
                    cbo.Text = ""
                    cbo.SelectedIndex = -1
                End If
            Next
        End If
    End Sub
#End Region

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If _RepairController.DeleteRepairReceive(_RepairID) Then
            Clear()
            btnDelete.Enabled = False
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
            Return True
        Else
            MessageBox.Show("Can't Delete this Repair Voucher!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            Dim objRepair As New RepairHeaderInfo
            objRepair = GetDataRepair()
            If _RepairController.SaveRepairReceive(objRepair, _dtRepairDetail) Then
                _RepairID = objRepair.RepairID
                If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                    Dim frmPrint As New frm_ReportViewer
                    Dim dt As New DataTable
                    Dim dtGem As New DataTable
                    dt = _RepairController.GetRepairReceiveForVoucher(_RepairID)
                    If dt.Rows.Count > 0 Then
                        frmPrint.RepairReceiveVoucher(dt)
                        frmPrint.WindowState = FormWindowState.Maximized
                        frmPrint.Show()
                    End If
                    Clear()
                Else
                    Clear()
                    Return True
                End If
            Else
                Return False
            End If
        End If
    End Function

    Private Function IsFillData() As Boolean

        If cboStaff.SelectedIndex = -1 Then
            MsgBox("Select Staff !", MsgBoxStyle.Information, AppName)
            cboStaff.Focus()
            Return False
        End If


        If _CustomerID = "" Then
            MsgBox("Please Select Customer !", MsgBoxStyle.Information, AppName)
            btnCustomer.Focus()
            Return False
        End If

        If grdDetail.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        If _dtRepairDetail.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        Return True
    End Function
    Private Function GetDataRepair() As RepairHeaderInfo
        GetDataRepair = New RepairHeaderInfo
        With GetDataRepair
            .RepairID = _RepairID
            .RepairDate = dtpRepairDate.Value
            .DueDate = dtpDueDate.Value
            .StaffID = cboStaff.SelectedValue
            .CustomerID = _CustomerID
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .AdvanceRepairAmount = IIf(txtAdvanceAmount.Text = "", 0, txtAdvanceAmount.Text)
        End With
    End Function

    Private Sub frm_Repair_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        numberformat = Global_DecimalFormat
        _LocationID = Global_CurrentLocationID
        GetcboStaff()
        GetItemCategory()
        GetGoldQuality()
        Clear()
        Me.KeyPreview = True
    End Sub

    Private Sub frm_Repair_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                MyBase.btnNew.PerformClick()
            Case Keys.F2
                MyBase.btnSave.PerformClick()
            Case Keys.F3
                If btnDelete.Enabled = True Then
                    MyBase.btnDelete.PerformClick()
                End If
            Case Keys.F4
                MyBase.btnClose.PerformClick()
            Case Keys.F5
                btnPrint.PerformClick()
        End Select
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm As New frm_CustomerShow
        Dim ObjCustomer As CustomerInfo
        If _CustomerID <> "" Then
            ObjCustomer = _CustomerController.GetCustomerByID(_CustomerID)
            With ObjCustomer
                frm._CustomerID = _CustomerID
                frm._CustomerCode = .CustomerCode
                frm.txtCustomerID.Text = _CustomerID
                frm.txtCustomerCode.Text = .CustomerCode
                frm.txtName.Text = .CustomerName
                frm.txtAddress.Text = .CustomerAddress
                frm.txtPhone.Text = .CustomerTel
                frm.txtRemark.Text = .Remark
                frm.chkInactive.Checked = IIf(CBool(.IsInactive) = True, True, False)
                frm._IsUpdate = True
            End With
        End If

        frm.ShowDialog()
        If frm._CustomerID <> "" Then
            _CustomerID = frm._CustomerID
            ObjCustomer = _CustomerController.GetCustomerByID(_CustomerID)
            txtCustomerCode.Text = ObjCustomer.CustomerCode
            txtCustomer.Text = ObjCustomer.CustomerName
            txtAddress.Text = ObjCustomer.CustomerAddress
        End If
    End Sub

    Private Sub btnCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomer.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow

        dt = _CustomerController.GetAllCustomerAutoCompleteData()
        DataItem = DirectCast(SearchData.FindFast(dt, "Customer List"), DataRow)
        If DataItem IsNot Nothing Then
            If DataItem("$Inactive") = False Then
                _CustomerID = DataItem("@CustomerID")
                txtCustomerCode.Text = DataItem("CustomerCode")
                txtCustomer.Text = DataItem("CustomerName_")
                txtAddress.Text = DataItem("CustomerAddress_")
            Else
                MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
                Exit Sub
            End If

        End If
    End Sub
#Region "GetCbo"
    Private Sub GetcboStaff()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _StaffController.GetStaffList().DefaultView
        cboStaff.SelectedIndex = -1
    End Sub
    Private Sub GetItemCategory()
        cboItemCategory.DisplayMember = "ItemCategory_"
        cboItemCategory.ValueMember = "@ItemCategoryID"
        cboItemCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView
        cboItemCategory.SelectedIndex = -1
    End Sub
    Private Sub GetGoldQuality()
        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView
        cboGoldQuality.SelectedIndex = -1
    End Sub
    Private Sub RefreshItemNameCbo(ByVal ItemID As String)
        Dim dt As New DataTable
        dt = _ItemNameController.GetItemNameListByItemCategory(ItemID)
        If dt.Rows.Count > 0 Then
            cboItemName.DataSource = dt.DefaultView
            cboItemName.DisplayMember = "ItemName_"
            cboItemName.ValueMember = "ItemNameID"
            cboItemName.SelectedIndex = 0
        Else
            dt.Rows.Clear()
            cboItemName.DataSource = dt.DefaultView
            cboItemName.DisplayMember = "ItemName_"
            cboItemName.ValueMember = "ItemNameID"
            cboItemName.Text = ""
            cboItemName.SelectedIndex = -1
        End If
    End Sub

#End Region

    Private Sub cboGoldQuality_Click(sender As Object, e As EventArgs) Handles cboGoldQuality.Click
        GetGoldQuality()
    End Sub

    Private Sub cboGoldQuality_KeyUp(sender As Object, e As KeyEventArgs) Handles cboGoldQuality.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQuality, e)
    End Sub

    Private Sub cboItemCategory_Click(sender As Object, e As EventArgs) Handles cboItemCategory.Click
        GetItemCategory()
    End Sub

    Private Sub cboItemCategory_KeyUp(sender As Object, e As KeyEventArgs) Handles cboItemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCategory, e)
    End Sub

    Private Sub cboItemName_Click(sender As Object, e As EventArgs) Handles cboItemName.Click
        cboItemName.DisplayMember = "ItemName_"
        cboItemName.ValueMember = "ItemNameID"
        cboItemName.DataSource = _ItemNameController.GetItemNameListByItemCategory(itemid).DefaultView
    End Sub

    Private Sub cboItemName_KeyUp(sender As Object, e As KeyEventArgs) Handles cboItemName.KeyUp
        AutoCompleteCombo_KeyUp(cboItemName, e)
    End Sub

    Private Sub cboStaff_Click(sender As Object, e As EventArgs) Handles cboStaff.Click
        GetcboStaff()
    End Sub

    Private Sub cboStaff_KeyUp(sender As Object, e As KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboItemCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboItemCategory.SelectedValueChanged
        itemid = cboItemCategory.SelectedValue
        RefreshItemNameCbo(itemid)

        If cboItemCategory.SelectedIndex > -1 Then
            txtDetailRemark.Text = ""
        End If
    End Sub

    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _RepairID = "0"
        RepairItemGenerateFormat()
        dtpRepairDate.Text = Now
        cboStaff.SelectedValue = -1
        _CustomerID = ""
        txtCustomer.Text = ""
        txtCustomerCode.Text = ""
        txtAddress.Text = ""
        txtRemark.Text = ""
        dtpDueDate.Text = Now.Date
        txtAdvanceAmount.Text = "0"
        Dim dc As New DataColumn
        _dtRepairDetail = New DataTable

        _dtRepairDetail.Columns.Add("RepairDetailID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("RepairID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("BarcodeNo", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("ItemCategoryID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("ItemNameID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("CurrentPrice", System.Type.GetType("System.Int32"))
        _dtRepairDetail.Columns.Add("LengthOrWidth", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("ItemTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("ItemTG", System.Type.GetType("System.Decimal"))

        dc = New DataColumn
        dc.ColumnName = "ItemK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0

        _dtRepairDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "ItemP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtRepairDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "ItemY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtRepairDetail.Columns.Add(dc)

        _dtRepairDetail.Columns.Add("DetailRemark", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("Design", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("IsFromShop", System.Type.GetType("System.Boolean"))
        _dtRepairDetail.Columns.Add("IsExit", System.Type.GetType("System.Boolean"))

        grdDetail.AutoGenerateColumns = False
        grdDetail.ReadOnly = True
        grdDetail.DataSource = _dtRepairDetail
        FormatGridItemDetail()

        chkIsFromShop.Checked = False
        lblBarcodeNo.Visible = False
        txtBarcodeNo.Visible = False
        btnSearchItem.Visible = False
        txtBarcodeNo.Text = ""
        ClearDetail()
    End Sub
    Private Sub RepairItemGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.RepairStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtRepairHeaderID.Text = _GeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpRepairDate.Value)
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
        End If


    End Sub

    Private Sub cboGoldQuality_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboGoldQuality.SelectedValueChanged
        Dim objCurrentPrice As New CommonInfo.CurrentPriceInfo
        If cboGoldQuality.Text <> "" Then
            Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
            GoldQualityInfo = _GoldQualityController.GetGoldQuality(cboGoldQuality.SelectedValue)
            If GoldQualityInfo.IsGramRate Then
                lblPercent.Text = "၁ ဂရမ်စျေး"
            Else
                lblPercent.Text = "၁ ကျပ်သာစျေး"
            End If
            _IsGram = GoldQualityInfo.IsGramRate
            GoldQualityForTextChange()
            objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(cboGoldQuality.SelectedValue)
            txtCurrentPrice.Text = objCurrentPrice.SalesRate.ToString
        Else
            txtItemTG.BackColor = Color.Linen
            txtItemK.BackColor = Color.Linen
            txtItemP.BackColor = Color.Linen
            txtItemY.BackColor = Color.Linen
            txtItemK.ReadOnly = True
            txtItemP.ReadOnly = True
            txtItemY.ReadOnly = True
            txtItemTG.ReadOnly = True
            lblPercent.Text = ""
            _IsGram = False
            txtCurrentPrice.Text = "0"
        End If
    End Sub

    Private Sub GoldQualityForTextChange()

        If _IsGram = True Then
            txtItemK.ReadOnly = True
            txtItemP.ReadOnly = True
            txtItemY.ReadOnly = True
            txtItemTG.ReadOnly = False

            txtItemTG.TabStop = True
            txtItemK.TabStop = False
            txtItemP.TabStop = False
            txtItemY.TabStop = False

            txtItemK.BackColor = Color.Linen
            txtItemP.BackColor = Color.Linen
            txtItemY.BackColor = Color.Linen
            txtItemTG.BackColor = Color.White
        Else
            txtItemK.ReadOnly = False
            txtItemP.ReadOnly = False
            txtItemY.ReadOnly = False
            txtItemTG.ReadOnly = True

            txtItemTG.TabStop = False
            txtItemK.TabStop = True
            txtItemP.TabStop = True
            txtItemY.TabStop = True

            txtItemK.BackColor = Color.White
            txtItemP.BackColor = Color.White
            txtItemY.BackColor = Color.White
            txtItemTG.BackColor = Color.Linen
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If chkIsFromShop.Checked And txtBarcodeNo.Text = "" Then
            MsgBox("Please Fill BarcodeNo!", MsgBoxStyle.Information, "Data Require!")
            txtBarcodeNo.Select()
            Exit Sub
        End If

        If chkIsFromShop.Checked And _BarcodeNo = "" Then
            MsgBox("Invalid BarcodeNo!", MsgBoxStyle.Information, "Invalid Data!")
            txtBarcodeNo.Select()
            Exit Sub
        End If

        If txtBarcodeNo.Text <> "" And _dtRepairDetail.Rows.Count > 0 And _IsUpdate = False Then
            For Each dr As DataRow In _dtRepairDetail.Rows
                If dr.RowState <> DataRowState.Deleted Then
                    If dr("BarcodeNo") = txtBarcodeNo.Text Then
                        MsgBox("Duplicate BarcodeNo!", MsgBoxStyle.Information, "Duplicate Data!")
                        Exit Sub
                    End If
                End If
            Next
        End If

        If txtDetailRemark.Text = "" Then
            If (cboItemCategory.Text = "" Or cboItemName.Text = "") Then
                MsgBox("Please Fill Combo!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If
        End If

        If cboGoldQuality.Text = "" Then
            MsgBox("Please Fill GoldQuality!", MsgBoxStyle.Information, "Data Require!")
            cboGoldQuality.Focus()
            Exit Sub
        End If


        If Val(txtCurrentPrice.Text) = 0 Then
            MsgBox("Please Fill Current Price!", MsgBoxStyle.Information, "Data Require!")
            txtCurrentPrice.Focus()
            Exit Sub
        End If
        If _IsGram = True Then
            If _ItemTG = 0 Then
                MsgBox("Please Enter Item Gram!", MsgBoxStyle.Information, "Data Require!")
                txtItemTG.Focus()
                Exit Sub
            End If
        Else
            If _ItemTK = 0 Then
                MsgBox("Please Enter Item Weight!", MsgBoxStyle.Information, "Data Require!")
                txtItemK.Focus()
                Exit Sub
            End If
        End If
       

       

        If _IsUpdate Then
            Dim dtItem As New DataTable
            UpdateItem(_RepairDetailID)
            txtBarcodeNo.Text = ""
            ClearDetail()
        Else
            If btnAdd.Text = "Add" Then
                _RepairDetailID = _GeneralController.GenerateKey(EnumSetting.GenerateKeyType.RepairDetail, EnumSetting.GenerateKeyType.RepairDetail.ToString, dtpRepairDate.Value)
                InsertItem(_RepairDetailID)
            End If
            txtBarcodeNo.Text = ""
            ClearDetail()
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        chkIsFromShop.Checked = False
        lblBarcodeNo.Visible = False
        txtBarcodeNo.Visible = False
        btnSearchItem.Visible = False
        txtBarcodeNo.Text = ""
        ClearDetail()
    End Sub

    Private Sub ClearDetail()
        If Global_UserLevel = "Administrator" Then
            txtCurrentPrice.ReadOnly = False
            txtCurrentPrice.BackColor = Color.White
        Else
            txtCurrentPrice.ReadOnly = True
            txtCurrentPrice.BackColor = Color.Linen
        End If

        _BarcodeNo = ""
        cboItemCategory.SelectedValue = -1
        cboItemName.SelectedValue = -1
        cboGoldQuality.SelectedValue = -1
        txtCurrentPrice.Text = ""
        lblPercent.Text = ""
        _IsGram = False
        txtLength.Text = ""
        txtItemK.Text = "0"
        txtItemP.Text = "0"
        txtItemY.Text = "0.0"
        txtItemTG.Text = "0.0"
        txtItemTK.Text = "0.0"
        _ItemTK = 0.0
        _ItemTG = 0.0
        txtDetailRemark.Text = ""
        txtDesign.Text = ""

        txtItemK.ReadOnly = True
        txtItemP.ReadOnly = True
        txtItemY.ReadOnly = True
        txtItemTG.ReadOnly = True

        txtItemK.BackColor = Color.Linen
        txtItemP.BackColor = Color.Linen
        txtItemY.BackColor = Color.Linen
        txtItemTG.BackColor = Color.Linen
        _IsExit = False
        _IsUpdate = False
        btnAdd.Text = "Add"
    End Sub
    Public Sub InsertItem(ByVal _RepairDetailID As String)
        Dim drItem As DataRow

        drItem = _dtRepairDetail.NewRow
        drItem.Item("RepairDetailID") = _RepairDetailID
        drItem.Item("RepairID") = _RepairID
        drItem.Item("BarcodeNo") = IIf(txtBarcodeNo.Text = "", "-", txtBarcodeNo.Text)
        drItem.Item("ItemCategoryID") = IIf(cboItemCategory.Text = "", "00", cboItemCategory.SelectedValue)
        drItem.Item("ItemNameID") = IIf(cboItemName.Text = "", "00", cboItemName.SelectedValue)

        If txtDetailRemark.Text = "" Then
            drItem.Item("ItemName") = _ItemNameController.GetItemName(cboItemName.SelectedValue).ItemName
        Else
            drItem.Item("ItemName") = IIf(txtDetailRemark.Text = "", "-", txtDetailRemark.Text)
        End If
        drItem.Item("GoldQuality") = cboGoldQuality.Text
        drItem.Item("GoldQualityID") = cboGoldQuality.SelectedValue
        drItem.Item("LengthOrWidth") = IIf(txtLength.Text = "", "-", txtLength.Text)
        drItem.Item("ItemTG") = _ItemTG
        drItem.Item("ItemTK") = _ItemTK
        drItem.Item("ItemK") = txtItemK.Text
        drItem.Item("ItemP") = txtItemP.Text
        drItem.Item("ItemY") = txtItemY.Text
        drItem.Item("CurrentPrice") = IIf(txtCurrentPrice.Text = "", 0, txtCurrentPrice.Text)
        drItem.Item("DetailRemark") = txtDetailRemark.Text
        drItem.Item("Design") = txtDesign.Text
        drItem.Item("IsFromShop") = chkIsFromShop.Checked
        drItem.Item("IsExit") = False
        _dtRepairDetail.Rows.Add(drItem)
        grdDetail.DataSource = _dtRepairDetail

    End Sub
    Public Sub UpdateItem(ByVal _RepairDetailID As String)
        Dim drItem As DataRow
        drItem = _dtRepairDetail.Rows(grdDetail.CurrentRow.Index)

        If Not IsNothing(drItem) Then
            drItem.Item("RepairDetailID") = _RepairDetailID
            drItem.Item("RepairID") = _RepairID
            drItem.Item("BarcodeNo") = IIf(txtBarcodeNo.Text = "", "-", txtBarcodeNo.Text)
            drItem.Item("ItemCategoryID") = IIf(cboItemCategory.Text = "", "00", cboItemCategory.SelectedValue)
            drItem.Item("ItemNameID") = IIf(cboItemName.Text = "", "00", cboItemName.SelectedValue)

            If txtDetailRemark.Text = "" Then
                drItem.Item("ItemName") = _ItemNameController.GetItemName(cboItemName.SelectedValue).ItemName
            Else
                drItem.Item("ItemName") = IIf(txtDetailRemark.Text = "", "-", txtDetailRemark.Text)
            End If
            drItem.Item("GoldQuality") = cboGoldQuality.Text
            drItem.Item("GoldQualityID") = cboGoldQuality.SelectedValue
            drItem.Item("LengthOrWidth") = IIf(txtLength.Text = "", "-", txtLength.Text)
            drItem.Item("ItemTG") = _ItemTG
            drItem.Item("ItemTK") = _ItemTK
            drItem.Item("ItemK") = txtItemK.Text
            drItem.Item("ItemP") = txtItemP.Text
            drItem.Item("ItemY") = txtItemY.Text
            drItem.Item("CurrentPrice") = IIf(txtCurrentPrice.Text = "", 0, txtCurrentPrice.Text)
            drItem.Item("DetailRemark") = txtDetailRemark.Text
            drItem.Item("Design") = txtDesign.Text
            drItem.Item("IsFromShop") = chkIsFromShop.Checked
            drItem.Item("IsExit") = _IsExit

            grdDetail.DataSource = _dtRepairDetail
        End If

    End Sub

    Public Sub FormatGridItemDetail()

        With grdDetail

            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "RepairDetailID"
                .DataPropertyName = "RepairDetailID"
                .Name = "RepairDetailID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "RepairID"
                .DataPropertyName = "RepairID"
                .Name = "RepairID"
                .Visible = False
            End With
            .Columns.Add(dcID)

            Dim dcDia As New DataGridViewTextBoxColumn
            With dcDia
                .HeaderText = "BarcodeNo"
                .DataPropertyName = "BarcodeNo"
                .Name = "BarcodeNo"
                .Width = 100
                .Visible = True
            End With
            .Columns.Add(dcDia)

            Dim dcCategoryID As New DataGridViewTextBoxColumn
            With dcCategoryID
                .HeaderText = "ItemCategoryID"
                .DataPropertyName = "ItemCategoryID"
                .Name = "ItemCategoryID"
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcCategoryID)


            Dim dcItemNameID As New DataGridViewTextBoxColumn
            With dcItemNameID
                .HeaderText = "ItemNameID"
                .DataPropertyName = "ItemNameID"
                .Name = "ItemNameID"
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcItemNameID)

            Dim dcItemName As New DataGridViewTextBoxColumn
            With dcItemName
                .HeaderText = "ItemName"
                .DataPropertyName = "ItemName"
                .Name = "ItemName"
                .Width = 150
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcItemName)

            Dim dcGQID As New DataGridViewTextBoxColumn
            With dcGQID
                .HeaderText = "GoldQualityID"
                .DataPropertyName = "GoldQualityID"
                .Name = "GoldQualityID"
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcGQID)

            Dim dcGQ As New DataGridViewTextBoxColumn
            With dcGQ
                .HeaderText = "GoldQuality"
                .DataPropertyName = "GoldQuality"
                .Name = "GoldQuality"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcGQ)


            Dim dcItemTK As New DataGridViewTextBoxColumn
            With dcItemTK
                .HeaderText = "ItemTK"
                .DataPropertyName = "ItemTK"
                .Name = "ItemTK"
                .Visible = False
            End With
            .Columns.Add(dcItemTK)

            Dim dcCurrentPirce As New DataGridViewTextBoxColumn
            With dcCurrentPirce
                .HeaderText = "Price"
                .DataPropertyName = "CurrentPrice"
                .Name = "CurrentPrice"
                .Width = 80
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcCurrentPirce)

            Dim dclength As New DataGridViewTextBoxColumn
            With dclength
                .HeaderText = "Length"
                .DataPropertyName = "LengthOrWidth"
                .Name = "LengthOrWidth"
                .Width = 80
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dclength)

            Dim dcItemTG As New DataGridViewTextBoxColumn
            With dcItemTG
                .HeaderText = "Gram"
                .DataPropertyName = "ItemTG"
                .Name = "ItemTG"
                .DefaultCellStyle.Format = "0.000"
                .Width = 80
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcItemTG)


            Dim dcItemK As New DataGridViewTextBoxColumn()
            With dcItemK
                .HeaderText = "K"
                .DataPropertyName = "ItemK"
                .Name = "ItemK"
                .Width = 40
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcItemK)

            Dim dcItemP As New DataGridViewTextBoxColumn()
            With dcItemP
                .HeaderText = "P"
                .DataPropertyName = "ItemP"
                .Name = "ItemP"
                .Width = 40
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcItemP)

            Dim dcItemY As New DataGridViewTextBoxColumn()
            With dcItemY
                .HeaderText = "Y"
                .DataPropertyName = "ItemY"
                .Name = "ItemY"
                .Width = 40
                .Visible = True
                .DefaultCellStyle.Format = "0.00"
                .MaxInputLength = 3
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcItemY)



            Dim dcDetailRemark As New DataGridViewTextBoxColumn
            With dcDetailRemark
                .HeaderText = "DetailRemark"
                .DataPropertyName = "DetailRemark"
                .Name = "DetailRemark"
                .Width = 100
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcDetailRemark)

            Dim dcDesign As New DataGridViewTextBoxColumn
            With dcDesign
                .HeaderText = "Design"
                .DataPropertyName = "Design"
                .Name = "Design"
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcDesign)

            Dim dcIsFromShop As New DataGridViewTextBoxColumn
            With dcIsFromShop
                .HeaderText = "IsFromShop"
                .DataPropertyName = "IsFromShop"
                .Name = "IsFromShop"
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            .Columns.Add(dcIsFromShop)


            Dim dcIsExit As New DataGridViewTextBoxColumn
            With dcIsExit
                .HeaderText = "IsExit"
                .DataPropertyName = "IsExit"
                .Name = "IsExit"
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            .Columns.Add(dcIsExit)
        End With
    End Sub

    Private Sub btnSearchItem_Click(sender As Object, e As EventArgs) Handles btnSearchItem.Click

        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objSaleItemHeader As New SaleInvoiceHeaderInfo
        Dim objCurrentPrice As New CurrentPriceInfo

        If _CustomerID = "" Then
            MsgBox("Please Select Customer", MsgBoxStyle.Information, AppName)
            btnCustomer.Focus()
        Else
            dt = _objSalesItemInvoiceController.GetSaleInvoiceForRepair(" AND M.[@CustomerID]='" & _CustomerID & "'", GetExistedItems())
            DataItem = DirectCast(SearchData.FindFast(dt, "Sales Item List"), DataRow)
            If DataItem IsNot Nothing Then
                _BarcodeNo = DataItem.Item("BarcodeNo")
                txtBarcodeNo.Text = _BarcodeNo
                cboItemCategory.SelectedValue = DataItem.Item("@ItemCategoryID")
                cboItemName.SelectedValue = DataItem.Item("@ItemNameID")
                cboGoldQuality.SelectedValue = DataItem.Item("@GoldQualityID")
                _IsGram = _GoldQualityController.GetGoldQuality(cboGoldQuality.SelectedValue).IsGramRate
                GoldQualityForTextChange()
                If _IsGram Then
                    lblPercent.Text = "၁ ဂရမ်စျေး"
                Else
                    lblPercent.Text = "၁ ကျပ်သားစျေး"
                End If

                objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(cboGoldQuality.SelectedValue)
                txtCurrentPrice.Text = objCurrentPrice.SalesRate
                If DataItem.Item("Length_") = "-" Then
                    If DataItem.Item("Width_") <> "-" Then
                        txtLength.Text = DataItem.Item("Width_")
                    Else
                        txtLength.Text = "-"
                    End If
                Else
                    If DataItem.Item("Width_") = "-" Then
                        txtLength.Text = DataItem.Item("Length_")
                    Else
                        txtLength.Text = DataItem.Item("Length_") & "\" & DataItem.Item("Width_")
                    End If
                End If

                txtItemK.Text = DataItem.Item("ItemK")
                txtItemP.Text = DataItem.Item("ItemP")
                txtItemY.Text = DataItem.Item("ItemY")
                txtItemTG.Text = Format(DataItem.Item("@ItemTG"), "0.000")
                _ItemTG = DataItem.Item("@ItemTG")
                _ItemTK = DataItem.Item("@ItemTK")
            End If
        End If

        'If (txtCustomerCode.Text <> "" And txtCustomer.Text <> "") Then
        '    dt = _objSalesItemInvoiceController.GetSaleInvoiceForRepair(" AND M.CustomerCode='" & txtCustomerCode.Text & "'", GetExistedItems())
        'Else
        '    dt = _objSalesItemInvoiceController.GetSaleInvoiceForRepair("", GetExistedItems())
        'End If
    End Sub

    Private Function GetExistedItems() As String
        GetExistedItems = ""
        For i As Integer = 0 To _dtRepairDetail.Rows.Count - 1
            If _dtRepairDetail.Rows(i).RowState <> DataRowState.Deleted Then
                GetExistedItems += "'" & _dtRepairDetail.Rows(i).Item("BarcodeNo") & "',"
            End If
        Next
        Return GetExistedItems.Trim(",")
    End Function

    Private Sub LnkTotalNoWaste_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkTotalNoWaste.LinkClicked
        Dim frm As New frm_ToWeight
        Dim GoldWeight As New GoldWeightInfo
        frm.ShowDialog()
        GoldWeight = frm._GoldWeightInfo

        If _IsGram = False Then
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
            Else
                txtItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
            End If
            'txtItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
        Else
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
        End If
    End Sub
    Private Sub CalculateItemWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemK.Text = "" Then txtItemK.Text = "0"
        If txtItemP.Text = "" Then txtItemP.Text = "0"
        If txtItemY.Text = "" Then txtItemY.Text = "0.0"

        If (Val(txtItemK.Text) > 0 Or Val(txtItemP.Text) > 0 Or Val(txtItemY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtItemK.Text)
            GoldWeight.WeightP = CInt(txtItemP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtItemY.Text)
            GoldWeight.WeightC = CDec(txtItemY.Text) - GoldWeight.WeightY
            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _ItemTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
            _ItemTG = GoldWeight.Gram
            txtItemTG.Text = Format(_ItemTG, "0.000")
        Else
            _ItemTG = 0.0
            _ItemTK = 0.0
            txtItemTG.Text = "0.0"
            'txtItemK.Text = "0"
            'txtItemP.Text = "0"
            'txtItemY.Text = "0.0"
        End If
    End Sub

    Private Sub txtItemK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtItemK_TextChanged(sender As Object, e As EventArgs) Handles txtItemK.TextChanged
        If txtItemK.Text = "" Then txtItemK.Text = "0"

        If Val(txtItemK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateItemWeightForKPY()
        End If
    End Sub

    Private Sub txtItemP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtItemP_TextChanged(sender As Object, e As EventArgs) Handles txtItemP.TextChanged
        If txtItemP.Text = "" Then txtItemP.Text = "0"
        If Val(txtItemP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtItemP.Text = 0
            txtItemP.SelectAll()
        End If

        If Val(txtItemP.Text.Trim) >= 0 And _IsGram = False Then
            CalculateItemWeightForKPY()
        End If
    End Sub

    Private Sub txtItemY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtItemY_TextChanged(sender As Object, e As EventArgs) Handles txtItemY.TextChanged
        If txtItemY.Text = "" Then txtItemY.Text = "0.0"
        If Val(txtItemY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtItemY.Text = 0.0
            txtItemY.SelectAll()
        End If

        If Val(txtItemY.Text.Trim) >= 0.0 And _IsGram = False Then
            CalculateItemWeightForKPY()
        End If
    End Sub

    Private Sub grdDetail_Click(sender As Object, e As EventArgs) Handles grdDetail.Click
        Dim GoldWeight As New GoldWeightInfo
        Dim objSItem As New OrderReceiveDetailInfo
        If grdDetail.RowCount = 0 Then
            _IsUpdate = False
            Exit Sub
        End If
        btnAdd.Text = "Update"
        _IsUpdate = True
        With grdDetail
            _RepairDetailID = .CurrentRow.Cells("RepairDetailID").Value

            If .CurrentRow.Cells("BarcodeNo").Value <> "-" Then
                _BarcodeNo = .CurrentRow.Cells("BarcodeNo").Value
                txtBarcodeNo.Text = .CurrentRow.Cells("BarcodeNo").Value
            Else
                txtBarcodeNo.Text = ""
                _BarcodeNo = ""
                lblBarcodeNo.Visible = False
                txtBarcodeNo.Visible = False
                btnSearchItem.Visible = False
            End If

            chkIsFromShop.Checked = .CurrentRow.Cells("IsFromShop").Value
            cboItemCategory.SelectedValue = .CurrentRow.Cells("ItemCategoryID").Value
            cboItemName.SelectedValue = .CurrentRow.Cells("ItemNameID").Value
            cboGoldQuality.SelectedValue = .CurrentRow.Cells("GoldQualityID").Value
            _IsGram = _GoldQualityController.GetGoldQuality(cboGoldQuality.SelectedValue).IsGramRate
            GoldQualityForTextChange()
            If _IsGram Then
                lblPercent.Text = "၁ ဂရမ်စျေး"
            Else
                lblPercent.Text = "၁ ကျပ်သားစျေး"
            End If
            txtLength.Text = .CurrentRow.Cells("lengthOrWidth").Value
            txtCurrentPrice.Text = .CurrentRow.Cells("CurrentPrice").Value
            txtDetailRemark.Text = .CurrentRow.Cells("DetailRemark").Value
            txtDesign.Text = .CurrentRow.Cells("Design").Value
            _ItemTG = CDec(grdDetail.CurrentRow.Cells("ItemTG").Value)
            _ItemTK = CDec(grdDetail.CurrentRow.Cells("ItemTK").Value)
            GoldWeight.GoldTK = _ItemTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtItemTG.Text = Format(_ItemTG, "0.000")
            _IsExit = .CurrentRow.Cells("IsExit").Value
        End With

    End Sub

    Private Sub btnSearchRepairID_Click(sender As Object, e As EventArgs) Handles btnSearchRepairID.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objRepairHeader As New RepairHeaderInfo

        dt = _RepairController.GetAllRepairHeader()
        DataItem = DirectCast(SearchData.FindFast(dt, "Repair Receive List"), DataRow)

        If DataItem IsNot Nothing Then
            _RepairID = DataItem.Item("@RepairID").ToString()
            objRepairHeader = _RepairController.GetRepairHeaderInfo(_RepairID)

            _dtRepairDetail.Clear()
            _dtRepairDetail = _RepairController.GetRepairReceiveDetail(_RepairID)
            grdDetail.DataSource = _dtRepairDetail

            ShowRepairHeaderInfo(objRepairHeader)
            btnDelete.Enabled = True
        Else
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString
        End If
    End Sub

    Private Sub chkIsFromShop_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsFromShop.CheckedChanged
        If chkIsFromShop.Checked = True Then
            lblBarcodeNo.Visible = True
            txtBarcodeNo.Visible = True
            btnSearchItem.Visible = True
        Else
            txtBarcodeNo.Text = ""
            lblBarcodeNo.Visible = False
            txtBarcodeNo.Visible = False
            btnSearchItem.Visible = False
        End If
    End Sub

    Private Sub ShowRepairHeaderInfo(ByVal objRepairHeader As RepairHeaderInfo)
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        With objRepairHeader
            dtpRepairDate.Value = .RepairDate
            txtRepairHeaderID.Text = .RepairID
            dtpDueDate.Value = .DueDate
            cboStaff.SelectedValue = .StaffID
            _CustomerID = .CustomerID
            txtCustomerCode.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerCode
            txtCustomer.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerName
            txtAddress.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerAddress
            txtAdvanceAmount.Text = .AdvanceRepairAmount
            txtRemark.Text = .Remark
        End With
    End Sub


    Private Sub txtBarcodeNo_TextChanged(sender As Object, e As EventArgs) Handles txtBarcodeNo.TextChanged
        Dim objSaleDetail As New SalesInvoiceDetailInfo
        Dim objCurrentPrice As New CurrentPriceInfo

        If txtBarcodeNo.Text <> "" Then
            _BarcodeNo = txtBarcodeNo.Text()
            If _CustomerID <> "" Then
                objSaleDetail = _objSalesItemInvoiceController.GetSaleInvoiceObjDataForRepair(" AND M.ItemCode='" & _BarcodeNo & "' AND M.CustomerID='" & _CustomerID & "'")
            Else
                objSaleDetail = _objSalesItemInvoiceController.GetSaleInvoiceObjDataForRepair(" AND M.ItemCode='" & _BarcodeNo & "'")
            End If

            If objSaleDetail.ItemCode IsNot Nothing Then
                With objSaleDetail
                    _BarcodeNo = .ItemCode
                    txtBarcodeNo.Text = .ItemCode
                    cboItemCategory.SelectedValue = .ItemCategoryID
                    cboItemName.SelectedValue = .ItemNameID
                    cboGoldQuality.SelectedValue = .GoldQualityID
                    _IsGram = _GoldQualityController.GetGoldQuality(.GoldQualityID).IsGramRate

                    If .Length = "-" Then
                        If .Width <> "-" Then
                            txtLength.Text = .Width
                        Else
                            txtLength.Text = "-"
                        End If
                    Else
                        If .Width = "-" Then
                            txtLength.Text = .Length
                        Else
                            txtLength.Text = .Length & "\" & .Width
                        End If
                    End If
                    If _IsGram Then
                        lblPercent.Text = "၁ ဂရမ်စျေး"
                    Else
                        lblPercent.Text = "၁ ကျပ်သားစျေး"
                    End If
                    GoldQualityForTextChange()
                    objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(cboGoldQuality.SelectedValue)
                    txtCurrentPrice.Text = objCurrentPrice.SalesRate
                    txtItemK.Text = .ItemK
                    txtItemP.Text = .ItemP
                    txtItemY.Text = .ItemY
                    txtItemTG.Text = Format(.ItemTG, "0.000")
                    _ItemTG = .ItemTG
                    _ItemTK = .ItemTK
                End With
            Else
                ClearDetail()
            End If
        Else
            ClearDetail()
        End If

    End Sub

    Private Sub txtItemTG_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemTG.TextChanged
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.0"

        If Val(txtItemTG.Text.Trim) >= 0.0 And _IsGram = True Then
            CalculateItemWeightForGram()
        End If
    End Sub

    Private Sub CalculateItemWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.0"

        If Val(txtItemTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtItemTG.Text)
            _ItemTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _ItemTK = GoldWeight.GoldTK

            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        Else
            _ItemTG = 0.0
            _ItemTK = 0.0
            txtItemK.Text = "0"
            txtItemP.Text = "0"
            txtItemY.Text = "0.0"
            'txtItemTG.Text = "0.0"
        End If
    End Sub

    Private Sub txtDetailRemark_TextChanged(sender As Object, e As EventArgs) Handles txtDetailRemark.TextChanged
        If txtDetailRemark.Text <> "" Then
            cboItemCategory.SelectedIndex = -1
            cboItemName.SelectedIndex = -1
        End If
    End Sub

    Private Sub txtCustomerCode_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerCode.TextChanged
        Dim objCustomer As CustomerInfo
        If _IsCustomerName = False Then
            _IsCustomerCode = True
            If txtCustomerCode.Text <> "" Then
                objCustomer = _CustomerController.GetCustomerCode(txtCustomerCode.Text)
                If objCustomer.CustomerID <> "" Then
                    If objCustomer.IsInactive = 0 Then
                        With objCustomer
                            _CustomerID = .CustomerID
                            txtCustomerCode.Text = .CustomerCode
                            txtCustomer.Text = .CustomerName
                            txtAddress.Text = .CustomerAddress
                        End With
                    Else
                        MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                        _CustomerID = ""
                        txtCustomer.Text = ""
                        txtAddress.Text = ""
                        Exit Sub
                    End If
                    txtCustomerCode.Enabled = True
                Else
                    _CustomerID = ""
                    txtCustomer.Text = ""
                    txtAddress.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
            End If
            _IsCustomerCode = False
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim dtGem As New DataTable

        dt = _RepairController.GetRepairReceiveForVoucher(_RepairID)
        If dt.Rows.Count > 0 Then
            frmPrint.RepairReceiveVoucher(dt)
            frmPrint.WindowState = FormWindowState.Maximized
            frmPrint.Show()
        Else
            MsgBox("Please Select Repair Voucher First!", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If
    End Sub
    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("RepairReceive")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtCustomer.TextChanged
        If _IsCustomerCode = False Then
            Dim dt As New DataTable
            Dim DataCollection As New AutoCompleteStringCollection()
            _IsCustomerName = True
            If txtCustomer.Text <> "" Then
                dt = _CustomerController.GetCustomerDataByCustomerName(txtCustomer.Text.Trim)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0).Item("IsInactive") = False) Then
                        _CustomerID = dt.Rows(0).Item("CustomerID")
                        txtCustomerCode.Text = dt.Rows(0).Item("CustomerCode")
                        txtCustomer.Text = dt.Rows(0).Item("CustomerName")
                        txtAddress.Text = dt.Rows(0).Item("CustomerAddress")
                    Else
                        _CustomerID = ""
                        txtCustomerCode.Text = ""
                        txtAddress.Text = ""
                    End If
                Else
                    _CustomerID = ""
                    txtCustomerCode.Text = ""
                    txtAddress.Text = ""
                End If
            Else
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtAddress.Text = ""
            End If
            _IsCustomerName = False
        End If
    End Sub

    Private Sub dtpRepairDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpRepairDate.ValueChanged
        RepairItemGenerateFormat()
    End Sub

    Private Sub grdDetail_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdDetail.RowsRemoved
        ClearDetail()
    End Sub


End Class