Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
Public Class frm_RepairReturn
    Implements IFormProcess

    Private _ReturnRepairID As String
    Private _ReturnRepairDetailID As String = ""
    Private _RepairID As String = ""
    Private _RepairDetailID As String = ""
    Private _CustomerID As String = ""
    Private _BarcodeNo As String = ""
    Private _ItemCategoryID = ""
    Private _ItemNameID = ""
    Private _GoldQualityID As String = ""
    Dim _LocationID As String
    Private _GrdGemPrice As Integer = 0
    Private _GrdGemQTY As Integer = 0
    Private _AllTotalAmount As Integer = 0
    Private _IsRowDelete As Boolean = False
    Private _ItemName As String = ""
    Private _StrGoldQuality As String = ""
    Private _LengthWidth As String = ""

    Private _ReturnItemTK As Decimal = "0.0"
    Private _ReturnItemTG As Decimal = "0.0"
    Private _ReturnGemsTK As Decimal = "0.0"
    Private _ReturnGemsTG As Decimal = "0.0"
    Private _ReturnGoldTK As Decimal = "0.0"
    Private _ReturnGoldTG As Decimal = "0.0"
    Private _WasteTK As Decimal = "0.0"
    Private _WasteTG As Decimal = "0.0"

    Private _OrgGemsTK As Decimal = "0.0"
    Private _OrgGemsTG As Decimal = "0.0"
    Private _OrgGoldTK As Decimal = "0.0"
    Private _OrgGoldTG As Decimal = "0.0"
    Private _OrgItemTK As Decimal = "0.0"
    Private _OrgItemTG As Decimal = "0.0"

    Private _DiffGemsTK As Decimal = "0.0"
    Private _DiffGemsTG As Decimal = "0.0"
    Private _DiffGoldTK As Decimal = "0.0"
    Private _DiffGoldTG As Decimal = "0.0"
    Private _DiffItemTK As Decimal = "0.0"
    Private _DiffItemTG As Decimal = "0.0"

    Private _Totalct As Decimal = "0.0"
    Private _IsGram As Boolean = False
    Private _IsUpdate As Boolean = False

    Private _dtRepairBarcode As New DataTable
    Private _dtAllGem As New DataTable

    Private _dtGem As New DataTable
    Private _dtRepairDetail As New DataTable
    Private _IsAllowDiscount As Boolean = False

    Private _RepairReturnController As BusinessRule.RepairReturn.IRepairReturnController = Factory.Instance.CreateRepairReturnController
    Private _RepairController As BusinessRule.Repair.IRepairController = Factory.Instance.CreateRepairController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _StaffController As BusinessRule.Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _GemsCategoryController As BusinessRule.GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _ConverterController As BusinessRule.Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GenerateController As BusinessRule.General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _ItemCategoryController As BusinessRule.ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _GoldQualityController As BusinessRule.GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    'Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    'Private _GeneralController As BusinessRule.General.IGeneralController = Factory.Instance.CreateGeneralController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Dim numberformat As Integer



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

    Private Sub frm_RepairReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblLogInUser.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        numberformat = Global_DecimalFormat
        _LocationID = Global_CurrentLocationID
        GetcboStaff()
        Clear()
        Me.KeyPreview = True
        'ReturnRepairItemGenerateFormat()
    End Sub
    'Private Sub ReturnRepairItemGenerateFormat() 'TMN 14/07/2017
    '    Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

    '    objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.RepairStockReturn.ToString)

    '    If objGenerateFormat.GenerateFormatID <> 0 Then
    '        txtReturnRepairID.Text = _GeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpReturnDate.Value)
    '    Else
    '        MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
    '    End If


    'End Sub
    Private Sub frm_RepairReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub btnRepairReturn_Click(sender As Object, e As EventArgs) Handles btnRepairReturn.Click
        Dim dt As New DataTable
        Dim dtItem As New DataTable
        Dim DataItem As DataRow
        Dim _objRepair As New CommonInfo.RepairHeaderInfo
        dt = _RepairController.GetReturnRepairHeaderForIsPaid()

        DataItem = DirectCast(SearchData.FindFast(dt, "Repair List"), DataRow)
        If DataItem IsNot Nothing Then
            _RepairID = DataItem.Item("RepairID").ToString()
            _objRepair = _RepairController.GetRepairHeaderInfo(_RepairID)
            With _objRepair
                txtRepairVoucherNo.Text = _RepairID
                _CustomerID = .CustomerID
                lblCustomerName.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerName & " (" & _CustomerController.GetCustomerByID(_CustomerID).CustomerCode & ")"
                lblCustomerAddress.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerAddress
                txtRepairDate.Text = Format(.RepairDate, "dd-MM-yyyy")
                txtDueDate.Text = Format(.DueDate, "dd-MM-yyyy")
                dtItem = _RepairController.GetBalaceAmountByReceiveID(_RepairID)
                If dtItem.Rows.Count > 0 Then
                    txtAdvanceRepairAmt.Text = 0 - (dtItem.Rows(0).Item("BalanceAmount"))
                Else
                    txtAdvanceRepairAmt.Text = .AdvanceRepairAmount
                End If
                txtAllTotalPaidAmt.Text = CStr(CLng(txtPaidAmt.Text) + (CLng(txtAdvanceRepairAmt.Text)))
                txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalPaidAmt.Text))
            End With
        End If
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        Dim dt As New DataTable
        dt = _GenerateController.CheckExitVoucherForCashReceipt("tbl_CashReceipt", "AND VoucherNo='" + _RepairID + "' AND Type='RepairReturn'")
        If dt.Rows.Count() > 0 Then
            MsgBox("This VoucherNo is Use in CashReceipt!", MsgBoxStyle.Information, "")
            Return False
            Exit Function
        End If

        If _RepairReturnController.DeleteRepairReturn(_ReturnRepairID, _RepairID) Then
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
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim ObjReturn As New CommonInfo.RepairReturnHeaderInfo
        If IsFillData() Then
            ObjReturn = GetRepairReturnData()
            _ReturnRepairID = _RepairReturnController.SaveRepairReturn(ObjReturn, _dtRepairDetail, _dtAllGem)
            If _ReturnRepairID <> "0" Then
                _RepairID = ObjReturn.RepairID
                If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                    Dim frmPrint As New frm_ReportViewer
                    Dim dt As New DataTable
                    Dim dtGem As New DataTable

                    dt = _RepairReturnController.GetRepairReturnForVoucher(_RepairID)
                    If dt.Rows.Count > 0 Then
                        frmPrint.RepairReturnVoucher(dt)
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

        If grdDetail.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        If Not MyBase.IsFill_AtLeastOneRowInGrid(_dtRepairDetail) Then Return False
        Return True
    End Function
    Private Function GetRepairReturnData() As CommonInfo.RepairReturnHeaderInfo
        Dim objReturn As New CommonInfo.RepairReturnHeaderInfo
        With objReturn
            .ReturnRepairID = _ReturnRepairID
            .RepairID = _RepairID
            .ReturnDate = dtpReturnDate.Value
            .AllReturnTotalAmount = IIf(txtAllTotalAmt.Text = "", 0, txtAllTotalAmt.Text)
            .AllReturnAddOrSub = IIf(txtAllAddOrSub.Text = "", 0, txtAllAddOrSub.Text)
            .ReturnDiscountAmount = IIf(txtDiscountAmt.Text = "", 0, txtDiscountAmt.Text)
            .ReturnPaidAmount = IIf(txtPaidAmt.Text = "", 0, txtPaidAmt.Text)
            .ReturnDiscountAmount = IIf(IsDBNull(txtDiscountAmt.Text), 0, txtDiscountAmt.Text)
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .AdvanceAmount = IIf(txtAdvanceRepairAmt.Text = "", 0, txtAdvanceRepairAmt.Text)
            .BalanceAmount = IIf(txtBalanceAmt.Text = "", 0, txtBalanceAmt.Text)
            .StaffID = cboStaff.SelectedValue
        End With
        Return objReturn
    End Function
    Private Sub GetcboStaff()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _StaffController.GetStaffList().DefaultView
        cboStaff.SelectedIndex = -1
    End Sub
    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub
    Private Sub cboStaff_Click(sender As Object, e As EventArgs) Handles cboStaff.Click
        GetcboStaff()
    End Sub

    Private Sub btnBarcodeNo_Click(sender As Object, e As EventArgs) Handles btnBarcodeNo.Click
        Dim DataItem As DataRow
        Dim dtRepairReturn As New DataTable
        Dim objCurrentPrice As New CurrentPriceInfo
        Dim GoldQualityInfo As New GoldQualityInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo

        If _RepairID <> "" Then
            dtRepairReturn = _RepairController.GetForRepairReturnbyRepairDetail(_RepairID, GetExistedItems())
            DataItem = DirectCast(SearchData.FindFast(dtRepairReturn, "Repair List"), DataRow)
            If DataItem IsNot Nothing Then
                _RepairDetailID = DataItem.Item("@RepairDetailID")
                If CBool(DataItem.Item("IsFromShop")) = True Then
                    txtBarcodeNo.Text = DataItem.Item("BarcodeNo")
                Else
                    txtBarcodeNo.Text = ""
                End If
                _ItemName = DataItem.Item("ItemName_")
                '_ItemName = IIf(IsDBNull(DataItem.Item("ItemName_")) = True, "", DataItem.Item("ItemName_"))
                _StrGoldQuality = DataItem.Item("GoldQuality_")
                _LengthWidth = DataItem.Item("LengthOrWidth_")
                lblItemName.Text = DataItem.Item("ItemName_") & "    " & DataItem.Item("GoldQuality_") & "    " & DataItem.Item("LengthOrWidth_")
                txtCurrentPrice.Text = DataItem.Item("CurrentPrice")
                _GoldQualityID = DataItem.Item("@GoldQualityID")
                _IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                GoldQualityForTextChange()
                If _IsGram Then
                    lblPercent.Text = "၁ ဂရမ်စျေး"
                Else
                    lblPercent.Text = "၁ ကျပ်သားစျေး"
                End If

                _OrgItemTG = CDec(DataItem.Item("@OrgItemTG"))
                _OrgItemTK = CDec(DataItem.Item("@OrgItemTK"))
                GoldWeight.GoldTK = _OrgItemTK
                GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtOItemK.Text = CStr(GoldWeight.WeightK)
                txtOItemP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtOItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtOItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtOItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtOItemTG.Text = Format(_OrgItemTG, "0.000")

                _OrgGoldTG = CDec(DataItem.Item("@OrgItemTG"))
                _OrgGoldTK = CDec(DataItem.Item("@OrgItemTK"))
                GoldWeight.GoldTK = _OrgGoldTK
                GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtOGoldK.Text = CStr(GoldWeight.WeightK)
                txtOGoldP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtOGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtOGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtOGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtOGoldTG.Text = Format(_OrgGoldTG, "0.000")
            End If
        Else
            MsgBox("Please Select Repair Voucher No!", MsgBoxStyle.Information, AppName)
            btnRepairReturn.Focus()
        End If
    End Sub

    Private Sub GoldQualityForTextChange()
        If _IsGram = True Then
            txtRItemK.ReadOnly = True
            txtRItemP.ReadOnly = True
            txtRItemY.ReadOnly = True
            txtRItemTG.ReadOnly = False

            txtRItemTG.TabStop = True
            txtRItemK.TabStop = False
            txtRItemP.TabStop = False
            txtRItemY.TabStop = False

            txtRItemK.BackColor = Color.Linen
            txtRItemP.BackColor = Color.Linen
            txtRItemY.BackColor = Color.Linen
            txtRItemTG.BackColor = Color.White

            txtWasteK.ReadOnly = True
            txtWasteP.ReadOnly = True
            txtWasteY.ReadOnly = True
            txtWasteTG.ReadOnly = False

            txtWasteTG.TabStop = True
            txtWasteK.TabStop = False
            txtWasteP.TabStop = False
            txtWasteY.TabStop = False

            txtWasteK.BackColor = Color.Linen
            txtWasteP.BackColor = Color.Linen
            txtWasteY.BackColor = Color.Linen
            txtWasteTG.BackColor = Color.White
        Else
            txtRItemK.ReadOnly = False
            txtRItemP.ReadOnly = False
            txtRItemY.ReadOnly = False
            txtRItemTG.ReadOnly = True

            txtRItemTG.TabStop = False
            txtRItemK.TabStop = True
            txtRItemP.TabStop = True
            txtRItemY.TabStop = True

            txtRItemK.BackColor = Color.White
            txtRItemP.BackColor = Color.White
            txtRItemY.BackColor = Color.White
            txtRItemTG.BackColor = Color.Linen

            txtWasteK.ReadOnly = False
            txtWasteP.ReadOnly = False
            txtWasteY.ReadOnly = False
            txtWasteTG.ReadOnly = True

            txtWasteTG.TabStop = False
            txtWasteK.TabStop = True
            txtWasteP.TabStop = True
            txtWasteY.TabStop = True

            txtWasteK.BackColor = Color.White
            txtWasteP.BackColor = Color.White
            txtWasteY.BackColor = Color.White
            txtWasteTG.BackColor = Color.Linen
        End If
    End Sub
    Private Function GetExistedItems() As String
        GetExistedItems = ""
        For i As Integer = 0 To _dtRepairDetail.Rows.Count - 1
            If _dtRepairDetail.Rows(i).RowState <> DataRowState.Deleted Then
                GetExistedItems += "'" & _dtRepairDetail.Rows(i).Item("RepairDetailID") & "',"
            End If
        Next
        Return GetExistedItems.Trim(",")
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearDetail()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If _RepairDetailID = "" Then
            MsgBox("Please Select RepairItem!", MsgBoxStyle.Information, "Data Require!")
            btnBarcodeNo.Focus()
            Exit Sub
        End If

        If _ReturnItemTG = 0 Then
            MsgBox("Please Enter Return Weight!", MsgBoxStyle.Information, "Data Require!")
            LnkTotalNoWaste.Focus()
            Exit Sub
        End If

        If _IsUpdate Then
            UpdateItem(_ReturnRepairDetailID, _dtGem)
            ClearDetail()
            CalculateAlldTotalAmount()
        Else
            If btnAdd.Text = "Add" Then
                _ReturnRepairDetailID = _GenerateController.GenerateKey(EnumSetting.GenerateKeyType.RepairReturnDetail, EnumSetting.GenerateKeyType.RepairReturnDetail.ToString, dtpReturnDate.Value)
                InsertItem(_ReturnRepairDetailID, _dtGem)
                ClearDetail()
                CalculateAlldTotalAmount()
            End If
        End If
    End Sub
    Private Sub Clear()
        _RepairID = ""
        _ReturnRepairID = 0
        _CustomerID = ""
        txtRepairVoucherNo.Text = ""
        dtpReturnDate.Text = Now
        'txtReturnRepairID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.RepairStockReturn, EnumSetting.GenerateKeyType.RepairStockReturn.ToString, Now)
        '_ReturnRepairID = CInt(txtReturnRepairID.Text)

        cboStaff.SelectedValue = -1
        lblCustomerAddress.Text = "CustomerAddress"
        lblCustomerName.Text = "CustomerName"
        txtRepairDate.Text = ""
        txtDueDate.Text = ""
        txtRemark.Text = ""
        txtAllTotalAmt.Text = "0"
        txtAllAddOrSub.Text = "0"
        txtDiscountAmt.Text = "0"
        txtAllNetAmt.Text = "0"
        txtAdvanceRepairAmt.Text = "0"
        txtPaidAmt.Text = "0"
        txtAllTotalPaidAmt.Text = "0"
        txtBalanceAmt.Text = "0"
        _IsAllowDiscount = False

        Dim dc As New DataColumn
        _dtRepairDetail = New DataTable

        _dtRepairDetail.Columns.Add("ReturnRepairDetailID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("ReturnRepairID", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("RepairDetailID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("BarcodeNo", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("LengthOrWidth", System.Type.GetType("System.String"))
        _dtRepairDetail.Columns.Add("ChangeSaleRate", System.Type.GetType("System.Int64"))


        dc = New DataColumn
        dc.ColumnName = "RItemK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0

        _dtRepairDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "RItemP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtRepairDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "RItemY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtRepairDetail.Columns.Add(dc)

        _dtRepairDetail.Columns.Add("ReturnItemTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("ReturnItemTG", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("ReturnGoldTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("ReturnGoldTG", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("ReturnGemTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("ReturnGemTG", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("WasteTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("WasteTG", System.Type.GetType("System.Decimal"))

        _dtRepairDetail.Columns.Add("OrgItemTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("OrgItemTG", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("OrgGoldTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("OrgGoldTG", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("OrgGemTK", System.Type.GetType("System.Decimal"))
        _dtRepairDetail.Columns.Add("OrgGemTG", System.Type.GetType("System.Decimal"))


        _dtRepairDetail.Columns.Add("DesignCharges", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("WhiteCharges", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("PlatingCharges", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("MountingCharges", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("ReturnGoldPrice", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("ReturnGemPrice", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("ReturnTotalAmount", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("ReturnAddOrSub", System.Type.GetType("System.Int64"))
        _dtRepairDetail.Columns.Add("ReturnNetAmount", System.Type.GetType("System.Int64"))

        grdDetail.AutoGenerateColumns = False
        grdDetail.ReadOnly = True
        grdDetail.DataSource = _dtRepairDetail

        FormatGridItemDetail()

        _dtAllGem = New DataTable

        _dtAllGem.Columns.Add("ReturnRepairGemID", System.Type.GetType("System.String"))
        _dtAllGem.Columns.Add("ReturnRepairDetailID", System.Type.GetType("System.String"))
        _dtAllGem.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtAllGem.Columns.Add("GemsCategory", System.Type.GetType("System.String"))
        _dtAllGem.Columns.Add("Description", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "GemsK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtAllGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtAllGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0.0
        _dtAllGem.Columns.Add(dc)

        _dtAllGem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtAllGem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtAllGem.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))
        _dtAllGem.Columns.Add("GemsTW", System.Type.GetType("System.Decimal"))
        _dtAllGem.Columns.Add("QTY", System.Type.GetType("System.Int16"))
        _dtAllGem.Columns.Add("UnitPrice", System.Type.GetType("System.Int64"))
        _dtAllGem.Columns.Add("Type", System.Type.GetType("System.String"))
        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtAllGem.Columns.Add(dc)
        _dtAllGem.Columns.Add("IsNewGems", System.Type.GetType("System.Boolean"))
        ClearDetail()
    End Sub
    Public Sub FormatGridItemDetail()
        With grdDetail
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcForSaleID As New DataGridViewTextBoxColumn()
            With dcForSaleID
                .HeaderText = "ReturnRepairDetailID"
                .DataPropertyName = "ReturnRepairDetailID"
                .Name = "ReturnRepairDetailID"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcForSaleID)

            Dim dcReturnDetialID As New DataGridViewTextBoxColumn()
            With dcReturnDetialID
                .HeaderText = "RepairDetailID"
                .DataPropertyName = "RepairDetailID"
                .Name = "RepairDetailID"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcReturnDetialID)

            Dim dcReturnHeaderID As New DataGridViewTextBoxColumn()
            With dcReturnHeaderID
                .HeaderText = "ReturnRepairID"
                .DataPropertyName = "ReturnRepairID"
                .Name = "ReturnRepairID"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcReturnHeaderID)

            Dim dcBarcodeNo As New DataGridViewTextBoxColumn()
            With dcBarcodeNo
                .HeaderText = "BarcodeNo"
                .DataPropertyName = "BarcodeNo"
                .Name = "BarcodeNo"
                .Width = 110
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcBarcodeNo)

            Dim dcItem As New DataGridViewTextBoxColumn
            With dcItem
                .HeaderText = "ItemName"
                .DataPropertyName = "ItemName"
                .Name = "ItemName"
                .Width = 120
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcItem)

            Dim dcGQ As New DataGridViewTextBoxColumn
            With dcGQ
                .HeaderText = "GoldQuality"
                .DataPropertyName = "GoldQuality"
                .Name = "GoldQuality"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcGQ)

            Dim dcGQID As New DataGridViewTextBoxColumn
            With dcGQID
                .HeaderText = "GoldQualityID"
                .DataPropertyName = "GoldQualityID"
                .Name = "GoldQualityID"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcGQID)

            Dim dcCurrentPrice As New DataGridViewTextBoxColumn
            With dcCurrentPrice
                .HeaderText = "CurrentPrice"
                .DataPropertyName = "ChangeSaleRate"
                .Name = "ChangeSaleRate"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcCurrentPrice)

            Dim dcLength As New DataGridViewTextBoxColumn
            With dcLength
                .HeaderText = "LengthOrWidth"
                .DataPropertyName = "LengthOrWidth"
                .Name = "LengthOrWidth"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcLength)



            Dim dcDesign As New DataGridViewTextBoxColumn
            With dcDesign
                .HeaderText = "DesignCharges"
                .DataPropertyName = "DesignCharges"
                .Name = "DesignCharges"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcDesign)

            Dim dcWhite As New DataGridViewTextBoxColumn
            With dcWhite
                .HeaderText = "WhiteCharges"
                .DataPropertyName = "WhiteCharges"
                .Name = "WhiteCharges"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcWhite)

            Dim dcPlating As New DataGridViewTextBoxColumn
            With dcPlating
                .HeaderText = "PlatingCharges"
                .DataPropertyName = "PlatingCharges"
                .Name = "PlatingCharges"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcPlating)

            Dim dcMounting As New DataGridViewTextBoxColumn
            With dcMounting
                .HeaderText = "MountingCharges"
                .DataPropertyName = "MountingCharges"
                .Name = "MountingCharges"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcMounting)

            Dim dcOrgItemTK As New DataGridViewTextBoxColumn()
            With dcOrgItemTK
                .HeaderText = "OrgItemTK"
                .DataPropertyName = "OrgItemTK"
                .Name = "OrgItemTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOrgItemTK)

            Dim dcOrgItemTG As New DataGridViewTextBoxColumn()
            With dcOrgItemTG
                .HeaderText = "OrgItemTG"
                .DataPropertyName = "OrgItemTG"
                .Name = "OrgItemTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOrgItemTG)

            Dim dcGoldTK As New DataGridViewTextBoxColumn()
            With dcGoldTK
                .HeaderText = "OrgGoldTK"
                .DataPropertyName = "OrgGoldTK"
                .Name = "OrgGoldTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGoldTK)

            Dim dcGoldTG As New DataGridViewTextBoxColumn()
            With dcGoldTG
                .HeaderText = "OrgGoldTG"
                .DataPropertyName = "OrgGoldTG"
                .Name = "OrgGoldTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGoldTG)
            Dim dcGTK As New DataGridViewTextBoxColumn()
            With dcGTK
                .HeaderText = "OrgGemTK"
                .DataPropertyName = "OrgGemTK"
                .Name = "OrgGemTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGTK)

            Dim dcTG As New DataGridViewTextBoxColumn()
            With dcTG
                .HeaderText = "OrgGemTG"
                .DataPropertyName = "OrgGemTG"
                .Name = "OrgGemTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcTG)


            Dim dcWTK As New DataGridViewTextBoxColumn()
            With dcWTK
                .HeaderText = "WasteTK"
                .DataPropertyName = "WasteTK"
                .Name = "WasteTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcWTK)

            Dim dcWTG As New DataGridViewTextBoxColumn()
            With dcWTG
                .HeaderText = "WasteTG"
                .DataPropertyName = "WasteTG"
                .Name = "WasteTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcWTG)

            Dim dcItemK As New DataGridViewTextBoxColumn()
            With dcItemK
                .HeaderText = "K"
                .DataPropertyName = "RItemK"
                .Name = "RItemK"
                .Width = 50
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcItemK)

            Dim dcItemP As New DataGridViewTextBoxColumn()
            With dcItemP
                .HeaderText = "P"
                .DataPropertyName = "RItemP"
                .Name = "RItemP"
                .Width = 50
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcItemP)

            Dim dcItemY As New DataGridViewTextBoxColumn()
            With dcItemY
                .HeaderText = "Y"
                .DataPropertyName = "RItemY"
                .Name = "RItemY"
                .Width = 50
                .Visible = True
                .DefaultCellStyle.Format = "0.00"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcItemY)

            Dim dcTTK As New DataGridViewTextBoxColumn()
            With dcTTK
                .HeaderText = "ReturnItemTK"
                .DataPropertyName = "ReturnItemTK"
                .Name = "ReturnItemTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcTTK)

            Dim dcTTG As New DataGridViewTextBoxColumn()
            With dcTTG
                .HeaderText = "ReturnItemTG"
                .DataPropertyName = "ReturnItemTG"
                .Name = "ReturnItemTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcTTG)


            Dim dcrTK As New DataGridViewTextBoxColumn()
            With dcrTK
                .HeaderText = "ReturnGoldTK"
                .DataPropertyName = "ReturnGoldTK"
                .Name = "ReturnGoldTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcrTK)

            Dim dcrTG As New DataGridViewTextBoxColumn()
            With dcrTG
                .HeaderText = "ReturnGoldTG"
                .DataPropertyName = "ReturnGoldTG"
                .Name = "ReturnGoldTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcrTG)

            Dim dcRGTK As New DataGridViewTextBoxColumn()
            With dcRGTK
                .HeaderText = "ReturnGemTK"
                .DataPropertyName = "ReturnGemTK"
                .Name = "ReturnGemTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcRGTK)

            Dim dcRGTG As New DataGridViewTextBoxColumn()
            With dcRGTG
                .HeaderText = "ReturnGemTG"
                .DataPropertyName = "ReturnGemTG"
                .Name = "ReturnGemTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcRGTG)

            Dim dcGoldPrice As New DataGridViewTextBoxColumn
            With dcGoldPrice
                .HeaderText = "ReturnGoldPrice"
                .DataPropertyName = "ReturnGoldPrice"
                .Name = "ReturnGoldPrice"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcGoldPrice)

            Dim dcGemsPrice As New DataGridViewTextBoxColumn
            With dcGemsPrice
                .HeaderText = "ReturnGemPrice"
                .DataPropertyName = "ReturnGemPrice"
                .Name = "ReturnGemPrice"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcGemsPrice)

            Dim dcAddSub As New DataGridViewTextBoxColumn
            With dcAddSub
                .HeaderText = "AddOrSub"
                .DataPropertyName = "ReturnAddOrSub"
                .Name = "ReturnAddOrSub"
                .Width = 105
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcAddSub)

            Dim dcAmount As New DataGridViewTextBoxColumn
            With dcAmount
                .HeaderText = "Amount"
                .DataPropertyName = "ReturnTotalAmount"
                .Name = "ReturnTotalAmount"
                .Width = 113
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcAmount)

            Dim dcNetAmount As New DataGridViewTextBoxColumn
            With dcNetAmount
                .HeaderText = "NetAmount"
                .DataPropertyName = "ReturnNetAmount"
                .Name = "ReturnNetAmount"
                .Width = 90
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcNetAmount)
        End With
    End Sub
    Private Sub FormatRepairGems()
        With grdGems
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "ReturnRepairGemID"
            dclID.DataPropertyName = "ReturnRepairGemID"
            dclID.Name = "ReturnRepairGemID"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcPID As New DataGridViewTextBoxColumn()
            dcPID.HeaderText = "ReturnRepairDetailID"
            dcPID.DataPropertyName = "ReturnRepairDetailID"
            dcPID.Name = "ReturnRepairDetailID"
            dcPID.Visible = False
            .Columns.Add(dcPID)

            Dim dcCat As New DataGridViewComboBoxColumn()
            With dcCat
                .HeaderText = "Category"
                .DataPropertyName = "GemsCategoryID"
                .Name = "GemsCategoryID"
                .DataSource = _GemsCategoryController.GetAllGemsCategoryForGridCombo
                .DisplayMember = "GemsCategory"
                .ValueMember = "@GemsCategoryID"
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Width = 120
                .Visible = True
                .ReadOnly = False
            End With
            .Columns.Add(dcCat)


            Dim dcName As New DataGridViewTextBoxColumn()
            dcName.HeaderText = "Description"
            dcName.DataPropertyName = "Description"
            dcName.Name = "Description"
            dcName.Width = 110
            dcName.Visible = True
            dcName.ReadOnly = False
            dcName.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcName.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcName)

            Dim dcGR As New DataGridViewCheckBoxColumn()
            With dcGR
                .HeaderText = "NewGem"
                .DataPropertyName = "IsNewGems"
                .Name = "IsNewGems"
                .Width = 60
                .Visible = True
                .ReadOnly = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGR)

            Dim dc5 As New DataGridViewTextBoxColumn()
            dc5.HeaderText = "RBP"
            dc5.DataPropertyName = "YOrCOrG"
            dc5.Name = "YOrCOrG"
            dc5.Width = 70
            dc5.Visible = True
            dc5.ReadOnly = False
            dc5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc5.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc5)

            Dim dc As New DataGridViewTextBoxColumn()
            dc.HeaderText = "K"
            dc.DataPropertyName = "GemsK"
            dc.Name = "GemsK"
            dc.Width = 40
            dc.Visible = True
            .ReadOnly = False
            dc.DefaultCellStyle.Format = "0"
            dc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc)

            Dim dc2 As New DataGridViewTextBoxColumn()
            dc2.HeaderText = "P"
            dc2.DataPropertyName = "GemsP"
            dc2.Name = "GemsP"
            dc2.Width = 40
            dc2.Visible = True
            .ReadOnly = False
            dc2.DefaultCellStyle.Format = "0"
            dc2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc2.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc2)

            Dim dc3 As New DataGridViewTextBoxColumn()
            dc3.HeaderText = "Y"
            dc3.DataPropertyName = "GemsY"
            dc3.Name = "GemsY"
            dc3.Width = 40
            dc3.Visible = True
            .ReadOnly = False
            dc3.DefaultCellStyle.Format = "0.0"
            dc3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc3.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc3)

            Dim dc11 As New DataGridViewTextBoxColumn()
            dc11.HeaderText = "GemsTK"
            dc11.DataPropertyName = "GemsTK"
            dc11.Name = "GemsTK"
            dc11.Visible = False
            dc11.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc11)

            Dim dc12 As New DataGridViewTextBoxColumn()
            dc12.HeaderText = "GemsTG"
            dc12.DataPropertyName = "GemsTG"
            dc12.Name = "GemsTG"
            dc12.Visible = False
            dc12.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc12)

            Dim dc10 As New DataGridViewTextBoxColumn()
            dc10.HeaderText = "GemsTW"
            dc10.DataPropertyName = "GemsTW"
            dc10.Name = "GemsTW"
            dc10.Visible = False
            dc10.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc10)

            Dim dc6 As New DataGridViewTextBoxColumn()
            dc6.HeaderText = "QTY"
            dc6.DataPropertyName = "QTY"
            dc6.Name = "QTY"
            dc6.Width = 47
            dc6.Visible = True
            .ReadOnly = False
            dc6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc6.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc6)

            Dim dc7 As New DataGridViewComboBoxColumn()
            dc7.HeaderText = "Type"
            dc7.DataPropertyName = "Type"
            dc7.Name = "Type"
            dc7.Items.AddRange(New String() {"Fix", "ByWeight", "ByQty"})
            dc7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dc7.Visible = True
            dc7.ReadOnly = False
            dc7.Width = 80
            .Columns.Add(dc7)

            Dim dc8 As New DataGridViewTextBoxColumn()
            dc8.HeaderText = "UnitPrice"
            dc8.DataPropertyName = "UnitPrice"
            dc8.Name = "UnitPrice"
            dc8.Width = 80
            dc8.Visible = True
            .ReadOnly = False
            dc8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dc8.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc8)


            Dim dc9 As New DataGridViewTextBoxColumn()
            dc9.HeaderText = "Amount"
            dc9.DataPropertyName = "Amount"
            dc9.Name = "Amount"
            dc9.Visible = True
            .ReadOnly = False
            dc9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dc9.Width = 90
            dc9.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc9)


        End With
    End Sub
    Private Sub ClearDetail()
        If Global_UserLevel = "Administrator" Then
            txtCurrentPrice.ReadOnly = False
            txtCurrentPrice.BackColor = Color.White
        Else
            txtCurrentPrice.ReadOnly = True
            txtCurrentPrice.BackColor = Color.Linen
        End If
        _RepairDetailID = ""
        _ReturnRepairDetailID = ""
        _GoldQualityID = ""
        _IsGram = False
        lblPercent.Text = ""
        txtBarcodeNo.Text = ""
        lblItemName.Text = ""
        _ItemName = ""
        _StrGoldQuality = ""
        _LengthWidth = ""
        txtCurrentPrice.Text = "0"

        txtOItemK.Text = "0"
        txtOItemP.Text = "0"
        txtOItemY.Text = "0.0"
        txtOItemTG.Text = "0.0"
        _OrgItemTK = 0.0
        _OrgItemTG = 0.0

        txtOGoldK.Text = "0"
        txtOGoldP.Text = "0"
        txtOGoldY.Text = "0.0"
        txtOGoldTG.Text = "0.0"
        _OrgGoldTK = 0.0
        _OrgGoldTG = 0.0

        txtOGemK.Text = "0"
        txtOGemP.Text = "0"
        txtOGemY.Text = "0.0"
        txtOGemTG.Text = "0.0"
        _OrgGemsTG = 0.0
        _OrgGemsTK = 0.0

        txtRItemK.Text = "0"
        txtRItemP.Text = "0"
        txtRItemY.Text = "0.0"
        txtRItemTG.Text = "0.0"
        _ReturnItemTG = 0.0
        _ReturnItemTK = 0.0

        txtRItemK.ReadOnly = True
        txtRItemP.ReadOnly = True
        txtRItemY.ReadOnly = True
        txtRItemTG.ReadOnly = True

        txtRItemK.BackColor = Color.Linen
        txtRItemP.BackColor = Color.Linen
        txtRItemY.BackColor = Color.Linen
        txtRItemTG.BackColor = Color.Linen


        txtRGoldK.Text = "0"
        txtRGoldP.Text = "0"
        txtRGoldY.Text = "0.0"
        txtRGoldTG.Text = "0.0"
        _ReturnGoldTG = 0.0
        _ReturnGoldTK = 0.0

        txtRGemK.Text = "0"
        txtRGemP.Text = "0"
        txtRGemY.Text = "0.0"
        txtRGemTG.Text = "0.0"
        _ReturnGemsTG = 0.0
        _ReturnGemsTK = 0.0

        txtWasteK.Text = "0"
        txtWasteP.Text = "0"
        txtWasteY.Text = "0.0"
        txtWasteTG.Text = "0.0"
        _WasteTG = 0.0
        _WasteTK = 0.0

        txtWasteK.ReadOnly = True
        txtWasteP.ReadOnly = True
        txtWasteY.ReadOnly = True
        txtWasteTG.ReadOnly = True

        txtWasteK.BackColor = Color.Linen
        txtWasteP.BackColor = Color.Linen
        txtWasteY.BackColor = Color.Linen
        txtWasteTG.BackColor = Color.Linen

        txtDItemK.Text = "0"
        txtDItemP.Text = "0"
        txtDItemY.Text = "0.0"
        txtDItemTG.Text = "0.0"
        _DiffItemTG = 0.0
        _DiffItemTK = 0.0

        txtDGoldK.Text = "0"
        txtDGoldP.Text = "0"
        txtDGoldY.Text = "0.0"
        txtDGoldTG.Text = "0.0"
        _DiffGoldTG = 0.0
        _DiffGoldTK = 0.0

        txtDGemK.Text = "0"
        txtDGemP.Text = "0"
        txtDGemY.Text = "0.0"
        txtDGemTG.Text = "0.0"
        _DiffGemsTG = 0.0
        _DiffGemsTK = 0.0

        txtDesignCharges.Text = "0"
        txtPlatingCharges.Text = "0"
        txtMountingCharges.Text = "0"
        txtWhiteCharges.Text = "0"

        txtGoldPrice.Text = ""
        txtGemsPrice.Text = "0"
        txtTotalAmt.Text = "0"
        txtAddSub.Text = "0"
        txtNetAmt.Text = "0"
        btnAdd.Text = "Add"
        _IsUpdate = False

        Dim dc As DataColumn
        _dtGem = New DataTable
        _dtGem.Columns.Add("ReturnRepairGemID", System.Type.GetType("System.String"))
        _dtGem.Columns.Add("ReturnRepairDetailID", System.Type.GetType("System.String"))
        _dtGem.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtGem.Columns.Add("GemsCategory", System.Type.GetType("System.String"))
        _dtGem.Columns.Add("Description", System.Type.GetType("System.String"))


        dc = New DataColumn
        dc.ColumnName = "GemsK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0.0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsTK"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0.0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsTG"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0.0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsTW"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0.0
        _dtGem.Columns.Add(dc)

        _dtGem.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))
        _dtGem.Columns.Add("Type", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "QTY"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "UnitPrice"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtGem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "IsNewGems"
        dc.DataType = System.Type.GetType("System.Boolean")
        dc.DefaultValue = False
        _dtGem.Columns.Add(dc)

        grdGems.AutoGenerateColumns = False
        grdGems.ReadOnly = False
        grdGems.DataSource = _dtGem

        FormatRepairGems()
    End Sub
    Private Sub LnkTotalNoWaste_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkTotalNoWaste.LinkClicked
        Dim frm As New frm_ToWeight
        Dim GoldWeight As New GoldWeightInfo
        frm.ShowDialog()
        GoldWeight = frm._GoldWeightInfo

        If _IsGram = False Then
            txtRItemK.Text = CStr(GoldWeight.WeightK)
            txtRItemP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtRItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
            Else
                txtRItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
            End If
            'txtRItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
        Else
            txtRItemTG.Text = Format(GoldWeight.Gram, "0.000")
        End If
        CalculateReturnGoldWeight()
        CalculateDifferentWeight()
        CalculateGoldPrice()
    End Sub

    Private Sub CalculateGrdGemsWeight()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim ReturnGemTG As Decimal = 0.0
        Dim OldGemTG As Decimal = 0.0

        Dim GemWeight As New CommonInfo.GoldWeightInfo
        Dim TotalWeight As New CommonInfo.GoldWeightInfo
        Dim OldGemWeight As New CommonInfo.GoldWeightInfo
        Dim OldTotalWeight As New CommonInfo.GoldWeightInfo

        Dim ReturnweightY As Decimal = 0
        Dim ReturnweightP As Integer = 0
        Dim ReturnweightK As Integer = 0

        Dim OldweightY As Decimal = 0
        Dim OldweightP As Integer = 0
        Dim OldweightK As Integer = 0

        For i As Integer = 0 To grdGems.RowCount - 1
            If Not IsNothing(grdGems.Rows(i).Cells("GemsTG").Value) Then
                ReturnGemTG += CDec(grdGems.Rows(i).Cells("GemsTG").Value)
            End If
            If Not grdGems.Rows(i).IsNewRow Then
                GemWeight.WeightK = CInt(Val(grdGems.Rows(i).Cells("GemsK").FormattedValue))
                GemWeight.WeightP = CInt(Val(grdGems.Rows(i).Cells("GemsP").FormattedValue))
                GemWeight.WeightY = CDec(Val(grdGems.Rows(i).Cells("GemsY").FormattedValue))

                ReturnweightY += GemWeight.WeightY
                If ReturnweightY >= Global_PToY Then
                    ReturnweightP += 1
                    ReturnweightY = ReturnweightY - Global_PToY
                End If

                ReturnweightP += GemWeight.WeightP
                If ReturnweightP >= 16 Then
                    ReturnweightK += 1
                    ReturnweightP = ReturnweightP - 16
                End If
                ReturnweightK += GemWeight.WeightK


                If (CBool(grdGems.Rows(i).Cells("IsNewGems").Value) = False) Then
                    OldGemWeight.WeightK = CInt(Val(grdGems.Rows(i).Cells("GemsK").FormattedValue))
                    OldGemWeight.WeightP = CInt(Val(grdGems.Rows(i).Cells("GemsP").FormattedValue))
                    OldGemWeight.WeightY = CDec(Val(grdGems.Rows(i).Cells("GemsY").FormattedValue))

                    OldweightY += OldGemWeight.WeightY
                    If OldweightY >= Global_PToY Then
                        OldweightP += 1
                        OldweightY = OldweightY - Global_PToY
                    End If

                    OldweightP += OldGemWeight.WeightP
                    If OldweightP >= 16 Then
                        OldweightK += 1
                        OldweightP = OldweightP - 16
                    End If
                    OldweightK += OldGemWeight.WeightK
                End If
            End If
        Next
        '' Return Gem Weight
        TotalWeight.WeightY = ReturnweightY
        TotalWeight.WeightP = ReturnweightP
        TotalWeight.WeightK = ReturnweightK

        txtRGemK.Text = Format(TotalWeight.WeightK, "0")
        txtRGemP.Text = Format(TotalWeight.WeightP, "0")
        txtRGemY.Text = Format(TotalWeight.WeightY, "0.0")

        TotalWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(TotalWeight)
        _ReturnGemsTK = TotalWeight.GoldTK
        TotalWeight.Gram = TotalWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
        _ReturnGemsTG = TotalWeight.Gram
        txtRGemTG.Text = Format(_ReturnGemsTG, "0.000")

        '' Original Gem Weight
        OldTotalWeight.WeightY = OldweightY
        OldTotalWeight.WeightP = OldweightP
        OldTotalWeight.WeightK = OldweightK

        txtOGemK.Text = Format(OldTotalWeight.WeightK, "0")
        txtOGemP.Text = Format(OldTotalWeight.WeightP, "0")
        txtOGemY.Text = Format(OldTotalWeight.WeightY, "0.0")

        OldTotalWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(OldTotalWeight)
        _OrgGemsTK = OldTotalWeight.GoldTK
        OldTotalWeight.Gram = OldTotalWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
        _OrgGemsTG = OldTotalWeight.Gram
        txtOGemTG.Text = Format(_OrgGemsTG, "0.000")

        'If OldGemTG > 0.0 Then
        '    GoldWeight.Gram = OldGemTG
        '    _OrgGemsTG = GoldWeight.Gram
        '    GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
        '    _OrgGemsTK = GoldWeight.GoldTK

        '    GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
        '    txtOGemK.Text = CStr(GoldWeight.WeightK)
        '    txtOGemP.Text = CStr(GoldWeight.WeightP)
        '    txtOGemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        '    txtOGemTG.Text = Format(_OrgGemsTG, "0.000")
        'Else
        '    _OrgGemsTG = 0.0
        '    _OrgGemsTK = 0.0
        '    txtOGemK.Text = "0"
        '    txtOGemP.Text = "0"
        '    txtOGemY.Text = "0.0"
        '    txtOGemTG.Text = "0.0"
        'End If

        CalculateOldGoldWeight()
        CalculateReturnGoldWeight()
        CalculateDifferentWeight()
        CalculateGoldPrice()
    End Sub
    Private Sub CalculateGrdGemsAmount()
        Dim grdtotalAmt As Integer = 0
        For i As Integer = 0 To grdGems.RowCount - 1
            If (CBool(grdGems.Rows(i).Cells("IsNewGems").Value) = True) Then
                grdtotalAmt += CInt(grdGems.Rows(i).Cells("Amount").FormattedValue)
            Else
                grdtotalAmt += 0
            End If
        Next
        txtGemsPrice.Text = CStr(grdtotalAmt)
        CalculateTotalAmount()
    End Sub

    'Private Sub grdGems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdGems.CellContentClick
    '    If (e.RowIndex <> -1) Then
    '        Select Case grdGems.Columns(e.ColumnIndex).Name
    '            Case "IsNewGems"
    '                CalculateGrdGemsWeight()
    '                CalculateGrdGemsAmount()
    '                CalculateOldGoldWeight()
    '                CalculateReturnGoldWeight()
    '                CalculateDifferentWeight()
    '                CalculateGoldPrice()
    '        End Select
    '    End If

    'End Sub

    Private Sub grdGems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdGems.CellContentClick
        If e.ColumnIndex = 4 Then
            If Not IsDBNull(grdGems.Rows(e.RowIndex).Cells("IsNewGems").Value) Then
                If grdGems.Rows(e.RowIndex).Cells("IsNewGems").Value = True Then
                    grdGems.Rows(e.RowIndex).Cells("IsNewGems").Value = False
                ElseIf grdGems.Rows(e.RowIndex).Cells("IsNewGems").Value = False Then
                    grdGems.Rows(e.RowIndex).Cells("IsNewGems").Value = True
                End If

                CalculateGrdGemsWeight()
                CalculateGrdGemsAmount()
                CalculateOldGoldWeight()
                CalculateReturnGoldWeight()
                CalculateDifferentWeight()
                CalculateGoldPrice()
            End If
        End If
    End Sub

    Private Sub grdGems_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles grdGems.CellValidated
        If grdGems.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdGems.Columns(e.ColumnIndex).Name
                Case "UnitPrice", "Type", "QTY", "GemsK", "GemsP", "GemsY", "YOrCOrG"
                    If Not IsDBNull(grdGems.Rows(e.RowIndex).Cells("Type").Value) Then
                        If IsDBNull(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                            grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value = 0
                        End If

                        If grdGems.Rows(e.RowIndex).Cells("Type").Value = "Fix" Then
                            If Not IsDBNull(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                                grdGems.Rows(e.RowIndex).Cells("Amount").Value = grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value
                            End If
                        ElseIf grdGems.Rows(e.RowIndex).Cells("Type").Value = "ByWeight" Then
                            Dim _Type As Boolean = False
                            If Not (IsDBNull(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value)) Then
                                If (IsDBNull(grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value)) Then
                                    _Type = True
                                ElseIf grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0" Then
                                    _Type = True
                                Else
                                    _Type = False
                                End If

                                If _Type = False Then
                                    grdGems.Rows(e.RowIndex).Cells("Amount").Value = IIf(IsDBNull(grdGems.Rows(e.RowIndex).Cells("GemsTW").Value) = True, 0, grdGems.Rows(e.RowIndex).Cells("GemsTW").Value) * CLng(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value)
                                Else
                                    grdGems.Rows(e.RowIndex).Cells("Amount").Value = IIf(IsDBNull(grdGems.Rows(e.RowIndex).Cells("GemsTK").Value) = True, 0, grdGems.Rows(e.RowIndex).Cells("GemsTK").Value) * CLng(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value)
                                End If
                            End If
                        Else
                            If Not IsDBNull(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                                grdGems.Rows(e.RowIndex).Cells("Amount").Value = CLng(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value) * CInt(IIf(IsDBNull(grdGems.Rows(e.RowIndex).Cells("QTY").Value) = True, 0, grdGems.Rows(e.RowIndex).Cells("QTY").Value))
                            End If
                        End If
                    End If
                    CalculateGrdGemsAmount()
            End Select
        End If
    End Sub
    Private Sub CalculategrAlldTotalAmount()
        Dim _AllTotalAmount As Integer = 0

        For j As Integer = 0 To grdGems.RowCount - 1
            If grdGems.Rows(j).Cells("IsNewGems").FormattedValue = True Then
                If grdGems.Rows(j).Cells("Amount").FormattedValue <> "" Then
                    If Not grdGems.Rows(j).IsNewRow Then
                        _AllTotalAmount += CDec(grdGems.Rows(j).Cells("Amount").FormattedValue)
                    End If

                End If
            Else
                _AllTotalAmount += 0
            End If
        Next
        txtGemsPrice.Text = _AllTotalAmount
    End Sub

    Private Sub grdGems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdGems.CellValueChanged
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim grdGemsItemTK As Decimal = 0.0
        Dim grdGemsItemTG As Decimal = 0.0
        If grdGems.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            If (grdGems.Columns(e.ColumnIndex).Name = "GemsK" Or grdGems.Columns(e.ColumnIndex).Name = "GemsP" Or grdGems.Columns(e.ColumnIndex).Name = "GemsY") Then
                With grdGems
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("GemsK").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("GemsP").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("GemsY").Value) Then

                        If .Rows(e.RowIndex).Cells("GemsK").FormattedValue = "" Then
                            .Rows(e.RowIndex).Cells("GemsK").Value() = "0"
                        End If

                        If .Rows(e.RowIndex).Cells("GemsP").FormattedValue = "" Then
                            .Rows(e.RowIndex).Cells("GemsP").Value() = "0"
                        End If

                        If .Rows(e.RowIndex).Cells("GemsY").FormattedValue = "" Then
                            .Rows(e.RowIndex).Cells("GemsY").Value() = "0.0"
                        End If

                        GoldWeight.WeightK = CInt(Val(.Rows(e.RowIndex).Cells("GemsK").FormattedValue))

                        If CInt(Val(.Rows(e.RowIndex).Cells("GemsP").FormattedValue)) >= 16 Then
                            MsgBox("GemP should not be greater than 15", MsgBoxStyle.Information, AppName)
                            .Rows(e.RowIndex).Cells("GemsP").Value() = "0"
                        End If
                        GoldWeight.WeightP = CInt(Val(.Rows(e.RowIndex).Cells("GemsP").FormattedValue))

                        If CDec(.Rows(e.RowIndex).Cells("GemsY").FormattedValue) >= Global_PToY Then
                            MsgBox("GemY should not be greater than" & (Global_PToY - 0.1), MsgBoxStyle.Information, AppName)
                            .Rows(e.RowIndex).Cells("GemsY").Value() = "0"
                        End If

                        GoldWeight.WeightY = System.Decimal.Truncate(Val(.Rows(e.RowIndex).Cells("GemsY").FormattedValue))
                        GoldWeight.WeightC = CDec(Val(.Rows(e.RowIndex).Cells("GemsY").FormattedValue)) - GoldWeight.WeightY

                        GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
                        grdGemsItemTK = GoldWeight.GoldTK
                        GoldWeight.Gram = grdGemsItemTK * Global_KyatToGram
                        grdGemsItemTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("GemsTG").Value() = grdGemsItemTG
                        .Rows(e.RowIndex).Cells("GemsTK").Value() = grdGemsItemTK
                    End If
                End With
            End If

            If grdGems.Columns(e.ColumnIndex).Name = "YOrCOrG" Then  'For GemsWeight Yati,B,Karat
                Dim equivalent As Decimal
                Dim VarWeight As String
                Dim VarWeightY As Integer
                Dim VarWeightBCG As Decimal
                Dim VarWeightP As Decimal
                Dim TP As Decimal
                Dim TY As Decimal
                Dim TC As Decimal

                Dim IsValid As Boolean
                If IsDBNull(grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value) Then
                    grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                End If

                VarWeight = CStr(grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value)
                'If VarWeight = "0" Then
                '    Exit Sub
                'End If

                If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
                    MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                    grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                    grdGems.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                    grdGems.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                    grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"

                Else
                    If VarWeight.EndsWith("ct") Then
                        If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                            VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                            ' Notes: For Karat,multiply 1.1 
                            TC = CStr(VarWeightBCG)
                            If Global_IsCarat = 0 Or Global_IsCarat = 2 Then
                                grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG)
                            Else
                                grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG * 1.1)
                            End If
                            IsValid = True
                        Else
                            IsValid = False
                        End If

                    ElseIf VarWeight.EndsWith("R") Then
                        If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                            If VarWeight.IndexOf(".") = -1 Then
                                VarWeightY = CInt(LSet(VarWeight, Len(VarWeight) - 1))
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = VarWeightY / equivalent
                                If Global_IsCarat = 2 Then
                                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                Else
                                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightY
                                End If
                                IsValid = True
                            Else
                                VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = VarWeight / equivalent
                                If Global_IsCarat = 2 Then
                                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                Else
                                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeight
                                End If
                                IsValid = True
                            End If
                        Else
                            IsValid = False
                        End If
                    ElseIf VarWeight.EndsWith("B") Then '' when Y is existing in string
                        If VarWeight.IndexOf("R") <> -1 Then
                            If IsNumeric(Mid(VarWeight, 1, VarWeight.IndexOf("R"))) And IsNumeric(Mid(VarWeight, VarWeight.IndexOf("R") + 2, Len(VarWeight) - VarWeight.IndexOf("R") - 2)) Then
                                If Mid(VarWeight, 1, VarWeight.IndexOf("R")).IndexOf(".") = -1 Then
                                    VarWeightY = CInt(Mid(VarWeight, 1, VarWeight.IndexOf("R")))
                                    VarWeightBCG = CDec(Mid(VarWeight, VarWeight.IndexOf("R") + 2, Len(VarWeight) - VarWeight.IndexOf("R") - 2))
                                    equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                    TY = VarWeightY + (VarWeightBCG / equivalent)
                                    ' grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    End If
                                    IsValid = True
                                Else
                                    IsValid = False
                                End If
                            Else
                                IsValid = False
                            End If
                        Else ''when Y is not existing in string
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                                VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                                'grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightBCG
                                equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                TY = VarWeightY + (VarWeightBCG / equivalent)
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = TY / equivalent
                                If Global_IsCarat = 2 Then
                                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                Else
                                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                End If
                                IsValid = True
                            Else
                                IsValid = False
                            End If
                        End If
                    ElseIf VarWeight.EndsWith("P") Then
                        If VarWeight.IndexOf("R") <> -1 Then
                            If IsNumeric(Mid(VarWeight, 1, VarWeight.IndexOf("R"))) And IsNumeric(Mid(VarWeight, VarWeight.IndexOf("R") + 2, Len(VarWeight) - VarWeight.IndexOf("R") - 4)) And IsNumeric(Mid(VarWeight, VarWeight.IndexOf("B") + 2, Len(VarWeight) - VarWeight.IndexOf("P"))) Then
                                If Mid(VarWeight, 1, VarWeight.IndexOf("R")).IndexOf(".") = -1 Then
                                    VarWeightY = CInt(Mid(VarWeight, 1, VarWeight.IndexOf("R")))
                                    VarWeightBCG = CDec(Mid(VarWeight, VarWeight.IndexOf("R") + 2, Len(VarWeight) - VarWeight.IndexOf("R") - 4))
                                    VarWeightP = CDec(Mid(VarWeight, VarWeight.IndexOf("B") + 2, Len(VarWeight) - VarWeight.IndexOf("P")))
                                    equivalent = Global_BToP '_ConverterCon.GetMeasurement("B", "P")
                                    TP = VarWeightBCG + (VarWeightP / equivalent)
                                    equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                    TY = VarWeightY + (TP / equivalent)
                                    '' grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    End If
                                    IsValid = True
                                Else
                                    IsValid = False
                                End If
                            Else
                                IsValid = False
                            End If
                        ElseIf VarWeight.IndexOf("B") <> -1 Then
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 3)) Then
                                If VarWeight.IndexOf(".") = -1 Then
                                    VarWeightY = 0
                                    VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 3))
                                    VarWeightP = CDec(Mid(VarWeight, Len(VarWeight) - 1, 1))
                                    equivalent = Global_BToP '_ConverterCon.GetMeasurement("B", "P")
                                    TP = VarWeightBCG + (VarWeightP / equivalent)
                                    equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                    TY = VarWeightY + (TP / equivalent)
                                    '' grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    End If
                                    IsValid = True
                                Else
                                    IsValid = False
                                End If
                            Else
                                IsValid = False
                            End If
                        Else ''eg 7P
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                                If VarWeight.IndexOf(".") = -1 Then
                                    VarWeightY = 0
                                    VarWeightBCG = 0
                                    VarWeightP = CDec(Mid(VarWeight, 1, VarWeight.IndexOf("P")))
                                    equivalent = Global_BToP '_ConverterCon.GetMeasurement("B", "P")
                                    TP = VarWeightBCG + (VarWeightP / equivalent)
                                    equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                    TY = VarWeightY + (TP / equivalent)
                                    ''grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    End If
                                    IsValid = True
                                Else
                                    IsValid = False
                                End If

                            Else
                                IsValid = False
                            End If
                        End If

                    End If

                    If Not IsValid And grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value <> "0" Then
                        MsgBox("Stone Weight is Invalid!", MsgBoxStyle.Information, "Invalid Data")
                        grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                        grdGems.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                        grdGems.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                        grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                    End If

                    equivalent = Global_GramToKarat '_ConverterCon.GetMeasurement("Gram", "Karat")
                    Dim gram As Decimal = TC / equivalent
                    grdGems.Rows(e.RowIndex).Cells("GemsTG").Value = gram
                    equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
                    grdGems.Rows(e.RowIndex).Cells("GemsTK").Value = gram / equivalent
                    GoldWeight.GoldTK = gram / equivalent
                    _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    grdGems.Rows(e.RowIndex).Cells("GemsK").Value = GoldWeight.WeightK
                    grdGems.Rows(e.RowIndex).Cells("GemsP").Value = GoldWeight.WeightP
                    grdGems.Rows(e.RowIndex).Cells("GemsY").Value = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.0")
                End If
            End If
            CalculateGrdGemsWeight()
        End If
    End Sub
    Private Sub CalculateOldGoldWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If _OrgItemTG > 0.0 Or _OrgGemsTG > 0.0 Then
            Dim ItemWeight As New CommonInfo.GoldWeightInfo
            Dim GemWeight As New CommonInfo.GoldWeightInfo
            Dim GoldWeight As New CommonInfo.GoldWeightInfo

            ItemWeight.WeightK = CDec(txtOItemK.Text)
            ItemWeight.WeightP = CDec(txtOItemP.Text)
            ItemWeight.WeightY = CDec(txtOItemY.Text)

            GemWeight.WeightK = CDec(txtOGemK.Text)
            GemWeight.WeightP = CDec(txtOGemP.Text)
            GemWeight.WeightY = CDec(txtOGemY.Text)

            If ItemWeight.WeightY < GemWeight.WeightY Then
                weightY = Global_PToY + ItemWeight.WeightY - GemWeight.WeightY
                ItemWeight.WeightP = ItemWeight.WeightP - 1
            Else
                weightY = ItemWeight.WeightY - GemWeight.WeightY
            End If

            If ItemWeight.WeightP < GemWeight.WeightP Then
                weightP = 16 + ItemWeight.WeightP - GemWeight.WeightP
                ItemWeight.WeightK = ItemWeight.WeightK - 1
            Else
                weightP = ItemWeight.WeightP - GemWeight.WeightP
            End If

            weightK = ItemWeight.WeightK - GemWeight.WeightK

            GoldWeight.WeightY = weightY
            GoldWeight.WeightP = weightP
            GoldWeight.WeightK = weightK

            txtOGoldK.Text = Format(GoldWeight.WeightK, "0")
            txtOGoldP.Text = Format(GoldWeight.WeightP, "0")
            txtOGoldY.Text = Format(GoldWeight.WeightY, "0.0")

            GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _OrgGoldTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
            _OrgGoldTG = GoldWeight.Gram
            txtOGoldTG.Text = Format((CDec(txtOItemTG.Text) - CDec(txtOGemTG.Text)), "0.000")
        Else
            _OrgGoldTG = 0.0
            _OrgGoldTK = 0.0

            txtOGoldK.Text = "0"
            txtOGoldP.Text = "0"
            txtOGoldY.Text = "0.0"
            txtOGoldTG.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateReturnGoldWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If _ReturnItemTG > 0.0 Or _ReturnGemsTG > 0.0 Then
            Dim ItemWeight As New CommonInfo.GoldWeightInfo
            Dim GemWeight As New CommonInfo.GoldWeightInfo
            Dim GoldWeight As New CommonInfo.GoldWeightInfo

            ItemWeight.WeightK = CDec(txtRItemK.Text)
            ItemWeight.WeightP = CDec(txtRItemP.Text)
            ItemWeight.WeightY = CDec(txtRItemY.Text)

            GemWeight.WeightK = CDec(txtRGemK.Text)
            GemWeight.WeightP = CDec(txtRGemP.Text)
            GemWeight.WeightY = CDec(txtRGemY.Text)

            If ItemWeight.WeightY < GemWeight.WeightY Then
                weightY = Global_PToY + ItemWeight.WeightY - GemWeight.WeightY
                ItemWeight.WeightP = ItemWeight.WeightP - 1
            Else
                weightY = ItemWeight.WeightY - GemWeight.WeightY
            End If

            If ItemWeight.WeightP < GemWeight.WeightP Then
                weightP = 16 + ItemWeight.WeightP - GemWeight.WeightP
                ItemWeight.WeightK = ItemWeight.WeightK - 1
            Else
                weightP = ItemWeight.WeightP - GemWeight.WeightP
            End If

            weightK = ItemWeight.WeightK - GemWeight.WeightK

            GoldWeight.WeightY = weightY
            GoldWeight.WeightP = weightP
            GoldWeight.WeightK = weightK

            txtRGoldK.Text = Format(GoldWeight.WeightK, "0")
            txtRGoldP.Text = Format(GoldWeight.WeightP, "0")
            If numberformat = 1 Then
                txtRGoldY.Text = Format(GoldWeight.WeightY, "0.0")
            Else
                txtRGoldY.Text = Format(GoldWeight.WeightY, "0.00")
            End If
            'txtRGoldY.Text = Format(GoldWeight.WeightY, "0.0")

            GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _ReturnGoldTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
            _ReturnGoldTG = GoldWeight.Gram

            txtRGoldTG.Text = Format((CDec(txtRItemTG.Text) - CDec(txtRGemTG.Text)), "0.000")

            '_ReturnGoldTG = Math.Round(_ReturnItemTG, 3) - Math.Round(_ReturnGemsTG, 3)
            'Dim Gold As New CommonInfo.GoldWeightInfo
            'Gold.Gram = _ReturnGoldTG
            'Gold.GoldTK = Gold.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            '_ReturnGoldTK = Gold.GoldTK
            'txtRGoldTG.Text = Format(_ReturnGoldTG, "0.000")

            'Gold = _ConverterController.ConvertGoldTKToKPYC(Gold)
            'txtRGoldK.Text = CStr(Gold.WeightK)
            'txtRGoldP.Text = CStr(Gold.WeightP)
            'txtRGoldY.Text = CStr(Format(CDec(Gold.WeightY + Gold.WeightC), "0.0"))
        Else
            _ReturnGoldTG = 0.0
            _ReturnGoldTK = 0.0

            txtRGoldK.Text = "0"
            txtRGoldP.Text = "0"
            txtRGoldY.Text = "0.0"
            txtRGoldTG.Text = "0.0"
        End If
    End Sub
    Private Sub txtRItemK_TextChanged(sender As Object, e As EventArgs) Handles txtRItemK.TextChanged
        If txtRItemK.Text = "" Then txtRItemK.Text = "0"

        If Val(txtRItemK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateItemWeightForKPY()
            CalculateReturnGoldWeight()
            CalculateDifferentWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtRItemP_TextChanged(sender As Object, e As EventArgs) Handles txtRItemP.TextChanged
        If txtRItemP.Text = "" Then txtRItemP.Text = "0"

        If Val(txtRItemP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtRItemP.Text = 0
            txtRItemP.SelectAll()
        End If

        If Val(txtRItemP.Text.Trim) >= 0 And _IsGram = False Then
            CalculateItemWeightForKPY()
            CalculateReturnGoldWeight()
            CalculateDifferentWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtRItemY_TextChanged(sender As Object, e As EventArgs) Handles txtRItemY.TextChanged
        If txtRItemY.Text = "" Then txtRItemY.Text = "0.0"

        If Val(txtRItemY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtRItemY.Text = "0.0"
            txtRItemY.SelectAll()
        End If

        If Val(txtRItemY.Text.Trim) >= 0 And _IsGram = False Then
            CalculateItemWeightForKPY()
            CalculateReturnGoldWeight()
            CalculateDifferentWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtWasteK_TextChanged(sender As Object, e As EventArgs) Handles txtWasteK.TextChanged
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"

        If Val(txtWasteK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteWeightForKPY()
            CalculateGoldPrice()
        End If
    End Sub
    Private Sub txtWasteP_TextChanged(sender As Object, e As EventArgs) Handles txtWasteP.TextChanged
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"

        If Val(txtWasteP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtWasteP.Text = 0
            txtWasteP.SelectAll()
        End If

        If Val(txtWasteP.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteWeightForKPY()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtWasteY_TextChanged(sender As Object, e As EventArgs) Handles txtWasteY.TextChanged
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.0"

        If Val(txtWasteY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtWasteY.Text = "0.0"
            txtWasteY.SelectAll()
        End If

        If Val(txtWasteY.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteWeightForKPY()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtNetAmt.TextChanged
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        txtAddSub.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtTotalAmt.Text))
    End Sub
    Private Sub CalculateItemWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtRItemK.Text = "" Then txtRItemK.Text = "0"
        If txtRItemP.Text = "" Then txtRItemP.Text = "0"
        If txtRItemY.Text = "" Then txtRItemY.Text = "0.0"


        If (Val(txtRItemK.Text) > 0 Or Val(txtRItemP.Text) > 0 Or Val(txtRItemY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtRItemK.Text)
            GoldWeight.WeightP = CInt(txtRItemP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtRItemY.Text)
            GoldWeight.WeightC = CDec(txtRItemY.Text) - GoldWeight.WeightY
            GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _ReturnItemTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
            _ReturnItemTG = GoldWeight.Gram
            txtRItemTG.Text = Format(_ReturnItemTG, "0.000")
        Else
            _ReturnItemTK = 0.0
            _ReturnItemTG = 0.0
            txtRItemTG.Text = "0.0"
            'txtRItemK.Text = "0"
            'txtRItemP.Text = "0"
            'txtRItemY.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateWasteWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.0"

        If (Val(txtWasteK.Text) > 0 Or Val(txtWasteP.Text) > 0 Or Val(txtWasteY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtWasteK.Text)
            GoldWeight.WeightP = CInt(txtWasteP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtWasteY.Text)
            GoldWeight.WeightC = CDec(txtWasteY.Text) - GoldWeight.WeightY

            GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _WasteTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
            _WasteTG = GoldWeight.Gram
            txtWasteTG.Text = Format(_WasteTG, "0.000")
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0
            txtWasteTG.Text = "0.0"

            'txtWasteK.Text = "0"
            'txtWasteP.Text = "0"
            'txtWasteY.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateTotalAmount()
        If txtGoldPrice.Text = "" Then txtGoldPrice.Text = "0"
        If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"

        txtTotalAmt.Text = CStr(CLng(txtGoldPrice.Text) + CLng(txtGemsPrice.Text) + CLng(txtWhiteCharges.Text) + CLng(txtPlatingCharges.Text) + CLng(txtMountingCharges.Text) + CLng(txtDesignCharges.Text))
        txtNetAmt.Text = txtTotalAmt.Text
        txtAddSub.Text = "0"
    End Sub
    Private Sub CalculateGoldPrice()
        If txtDGoldK.Text = "" Then txtDGoldK.Text = "0"
        If txtDGoldP.Text = "" Then txtDGoldP.Text = "0"
        If txtDGoldY.Text = "" Then txtDGoldY.Text = "0.0"
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.0"
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"

        Dim Result As Decimal = 0
        Dim TempTK As Decimal = 0.0
        Dim Gold As New CommonInfo.GoldWeightInfo
        Dim _GoldPrice As Integer = 0

        Gold.WeightK = CInt(txtDGoldK.Text) + CInt(txtWasteK.Text)
        Gold.WeightP = CInt(txtDGoldP.Text) + CInt(txtWasteP.Text)
        Gold.WeightY = System.Decimal.Truncate(CDec(txtDGoldY.Text) + CDec(txtWasteY.Text))
        Gold.WeightC = (CDec(txtDGoldY.Text) + CDec(txtWasteY.Text)) - Gold.WeightY
        Gold.GoldTK = _ConverterController.ConvertKPYCToGoldTK(Gold)
        TempTK = Gold.GoldTK

        If _IsGram = False Then
            txtGoldPrice.Text = CInt(TempTK * txtCurrentPrice.Text)
        Else
            txtGoldPrice.Text = CInt((CDec(txtDGoldTG.Text) + CDec(txtWasteTG.Text)) * txtCurrentPrice.Text)
        End If
        CalculateTotalAmount()
    End Sub

    Private Sub txtDesignCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesignCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDesignCharges_TextChanged(sender As Object, e As EventArgs) Handles txtDesignCharges.TextChanged
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        CalculateTotalAmount()
    End Sub

    Private Sub txtPlatingCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlatingCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPlatingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtPlatingCharges.TextChanged
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        CalculateTotalAmount()
    End Sub

    Private Sub txtMountingCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMountingCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtMountingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtMountingCharges.TextChanged
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        CalculateTotalAmount()
    End Sub

    Private Sub txtWhiteCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWhiteCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtWhiteCharges_TextChanged(sender As Object, e As EventArgs) Handles txtWhiteCharges.TextChanged
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        CalculateTotalAmount()
    End Sub

    Private Sub CalculateDifferentWeight()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If _ReturnItemTK > 0 Then
            _DiffItemTK = CDec(_ReturnItemTK) - CDec(_OrgItemTK)
            _DiffItemTG = CDec(txtRItemTG.Text) - CDec(txtOItemTG.Text)
            GoldWeight.GoldTK = _DiffItemTK
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtDItemK.Text = CStr(GoldWeight.WeightK)
            txtDItemP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtDItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtDItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtDItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtDItemTG.Text = Format(_DiffItemTG, "0.000")

            _DiffGemsTK = CDec(_ReturnGemsTK) - CDec(_OrgGemsTK)
            _DiffGemsTG = CDec(txtRGemTG.Text) - CDec(txtOGemTG.Text)
            GoldWeight.GoldTK = _DiffGemsTK
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtDGemK.Text = CStr(GoldWeight.WeightK)
            txtDGemP.Text = CStr(GoldWeight.WeightP)
            txtDGemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtDGemTG.Text = Format(_DiffGemsTG, "0.000")

            _DiffGoldTK = CDec(_ReturnGoldTK) - CDec(_OrgGoldTK)
            _DiffGoldTG = CDec(txtRGoldTG.Text) - CDec(txtOGoldTG.Text)
            GoldWeight.GoldTK = _DiffGoldTK
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtDGoldK.Text = CStr(GoldWeight.WeightK)
            txtDGoldP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtDGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtDGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtDGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtDGoldTG.Text = Format(_DiffGoldTG, "0.000")
        Else
            _DiffItemTK = 0.0
            _DiffItemTG = 0.0
            txtDItemK.Text = "0"
            txtDItemP.Text = "0"
            txtDItemY.Text = "0.0"
            txtDItemTG.Text = "0.0"

            _DiffGemsTK = 0.0
            _DiffGemsTG = 0.0
            txtDGemK.Text = "0"
            txtDGemP.Text = "0"
            txtDGemY.Text = "0.0"
            txtDGemTG.Text = "0.0"

            _DiffGoldTK = 0.0
            _DiffGoldTG = 0.0
            txtDGoldK.Text = "0"
            txtDGoldP.Text = "0"
            txtDGoldY.Text = "0.0"
            txtDGoldTG.Text = "0.0"
        End If
    End Sub


    Private Sub grdGems_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdGems.RowsRemoved
        If (grdGems.RowCount > 0) Then
            CalculateOldGoldWeight()
            CalculateReturnGoldWeight()
            CalculateDifferentWeight()
            CalculateGoldPrice()
        End If
    End Sub
    Public Sub InsertItem(ByVal _ReturnRepairDetailID As String, ByVal _dtGem As DataTable)
        Dim drItem As DataRow

        drItem = _dtRepairDetail.NewRow
        drItem.Item("ReturnRepairDetailID") = _ReturnRepairDetailID
        drItem.Item("ReturnRepairID") = _ReturnRepairID
        drItem.Item("RepairDetailID") = _RepairDetailID
        drItem.Item("BarcodeNo") = IIf(txtBarcodeNo.Text = "", "-", txtBarcodeNo.Text)
        drItem.Item("ItemName") = _ItemName
        drItem.Item("GoldQuality") = _StrGoldQuality
        drItem.Item("GoldQualityID") = _GoldQualityID
        drItem.Item("LengthOrWidth") = _LengthWidth
        drItem.Item("ChangeSaleRate") = IIf(txtCurrentPrice.Text = "", 0, txtCurrentPrice.Text)

        drItem.Item("RItemK") = IIf(txtRItemK.Text = "", "0", txtRItemK.Text)
        drItem.Item("RItemP") = IIf(txtRItemP.Text = "", "0", txtRItemP.Text)
        drItem.Item("RItemY") = IIf(txtRItemY.Text = "", "0.0", txtRItemY.Text)

        drItem.Item("ReturnItemTK") = _ReturnItemTK
        drItem.Item("ReturnItemTG") = _ReturnItemTG
        drItem.Item("ReturnGoldTK") = _ReturnGoldTK
        drItem.Item("ReturnGoldTG") = _ReturnGoldTG
        drItem.Item("ReturnGemTK") = _ReturnGemsTK
        drItem.Item("ReturnGemTG") = _ReturnGemsTG
        drItem.Item("WasteTK") = _WasteTK
        drItem.Item("WasteTG") = _WasteTG
        drItem.Item("OrgItemTK") = _OrgItemTK
        drItem.Item("OrgItemTG") = _OrgItemTG
        drItem.Item("OrgGoldTK") = _OrgGoldTK
        drItem.Item("OrgGoldTG") = _OrgGoldTG
        drItem.Item("OrgGemTK") = _OrgGemsTK
        drItem.Item("OrgGemTG") = _OrgGemsTG
        drItem.Item("DesignCharges") = IIf(txtDesignCharges.Text = "", "0", txtDesignCharges.Text)
        drItem.Item("WhiteCharges") = IIf(txtWhiteCharges.Text = "", "0", txtWhiteCharges.Text)
        drItem.Item("PlatingCharges") = IIf(txtPlatingCharges.Text = "", "0", txtPlatingCharges.Text)
        drItem.Item("MountingCharges") = IIf(txtMountingCharges.Text = "", "0", txtMountingCharges.Text)
        drItem.Item("ReturnGoldPrice") = IIf(txtGoldPrice.Text = "", "0", txtGoldPrice.Text)
        drItem.Item("ReturnGemPrice") = IIf(txtGemsPrice.Text = "", "0", txtGemsPrice.Text)
        drItem.Item("ReturnTotalAmount") = IIf(txtTotalAmt.Text = "", "0", txtTotalAmt.Text)
        drItem.Item("ReturnAddOrSub") = IIf(txtAddSub.Text = "", "0", txtAddSub.Text)
        drItem.Item("ReturnNetAmount") = IIf(txtNetAmt.Text = "", "0", txtNetAmt.Text)


        _dtRepairDetail.Rows.Add(drItem)
        grdDetail.DataSource = _dtRepairDetail

        Dim drDiamond As DataRow
        For Each dr As DataRow In _dtGem.Rows
            drDiamond = _dtAllGem.NewRow()
            drDiamond("ReturnRepairGemID") = _GenerateController.GenerateKey(EnumSetting.GenerateKeyType.RepairReturnGemsItem, CommonInfo.EnumSetting.GenerateKeyType.RepairReturnGemsItem.ToString, Now.Date)
            drDiamond("ReturnRepairDetailID") = _ReturnRepairDetailID
            drDiamond("GemsCategoryID") = dr("GemsCategoryID")
            drDiamond("GemsCategory") = dr("GemsCategory")
            drDiamond("Description") = dr("Description")
            drDiamond("GemsK") = dr("GemsK")
            drDiamond("GemsP") = dr("GemsP")
            drDiamond("GemsY") = dr("GemsY")
            drDiamond("GemsTK") = dr("GemsTK")
            drDiamond("GemsTG") = dr("GemsTG")
            drDiamond("YOrCOrG") = dr("YOrCOrG")
            drDiamond("GemsTW") = dr("GemsTW")
            drDiamond("QTY") = dr("QTY")
            drDiamond("UnitPrice") = dr("UnitPrice")
            drDiamond("Type") = dr("Type")
            drDiamond("Amount") = dr("Amount")
            drDiamond("IsNewGems") = dr("IsNewGems")
            _dtAllGem.Rows.Add(drDiamond)
        Next
    End Sub

    Public Sub UpdateItem(ByVal _ReturnRepairDetailID As String, ByVal _dtGem As DataTable)
        Dim drItem As DataRow
        drItem = _dtRepairDetail.Rows(grdDetail.CurrentRow.Index)

        If Not IsNothing(drItem) Then
            drItem.Item("ReturnRepairDetailID") = _ReturnRepairDetailID
            drItem.Item("ReturnRepairID") = _ReturnRepairID
            drItem.Item("RepairDetailID") = _RepairDetailID
            drItem.Item("BarcodeNo") = IIf(txtBarcodeNo.Text = "", "-", txtBarcodeNo.Text)
            drItem.Item("ItemName") = _ItemName
            drItem.Item("GoldQuality") = _StrGoldQuality
            drItem.Item("GoldQualityID") = _GoldQualityID
            drItem.Item("LengthOrWidth") = _LengthWidth
            drItem.Item("ChangeSaleRate") = IIf(txtCurrentPrice.Text = "", 0, txtCurrentPrice.Text)

            drItem.Item("RItemK") = IIf(txtRItemK.Text = "", "0", txtRItemK.Text)
            drItem.Item("RItemP") = IIf(txtRItemP.Text = "", "0", txtRItemP.Text)
            drItem.Item("RItemY") = IIf(txtRItemY.Text = "", "0.0", txtRItemY.Text)

            drItem.Item("ReturnItemTK") = _ReturnItemTK
            drItem.Item("ReturnItemTG") = _ReturnItemTG
            drItem.Item("ReturnGoldTK") = _ReturnGoldTK
            drItem.Item("ReturnGoldTG") = _ReturnGoldTG
            drItem.Item("ReturnGemTK") = _ReturnGemsTK
            drItem.Item("ReturnGemTG") = _ReturnGemsTG
            drItem.Item("WasteTK") = _WasteTK
            drItem.Item("WasteTG") = _WasteTG
            drItem.Item("OrgItemTK") = _OrgItemTK
            drItem.Item("OrgItemTG") = _OrgItemTG
            drItem.Item("OrgGoldTK") = _OrgGoldTK
            drItem.Item("OrgGoldTG") = _OrgGoldTG
            drItem.Item("OrgGemTK") = _OrgGemsTK
            drItem.Item("OrgGemTG") = _OrgGemsTG
            drItem.Item("DesignCharges") = IIf(txtDesignCharges.Text = "", "0", txtDesignCharges.Text)
            drItem.Item("WhiteCharges") = IIf(txtWhiteCharges.Text = "", "0", txtWhiteCharges.Text)
            drItem.Item("PlatingCharges") = IIf(txtPlatingCharges.Text = "", "0", txtPlatingCharges.Text)
            drItem.Item("MountingCharges") = IIf(txtMountingCharges.Text = "", "0", txtMountingCharges.Text)
            drItem.Item("ReturnGoldPrice") = IIf(txtGoldPrice.Text = "", "0", txtGoldPrice.Text)
            drItem.Item("ReturnGemPrice") = IIf(txtGemsPrice.Text = "", "0", txtGemsPrice.Text)
            drItem.Item("ReturnTotalAmount") = IIf(txtTotalAmt.Text = "", "0", txtTotalAmt.Text)
            drItem.Item("ReturnAddOrSub") = IIf(txtAddSub.Text = "", "0", txtAddSub.Text)
            drItem.Item("ReturnNetAmount") = IIf(txtNetAmt.Text = "", "0", txtNetAmt.Text)

            grdDetail.DataSource = _dtRepairDetail
            _dtGem = grdGems.DataSource
            Dim j As Integer = 0
            If _dtGem.Rows.Count > 0 Then  '   if Gems Update , check dtstone. if dtstone has Gems, delete gemsid .
                For i As Integer = 0 To _dtGem.Rows.Count - 1
                    While j < _dtAllGem.Rows.Count
                        Dim row As DataRow
                        row = _dtAllGem.Rows(j)

                        If Not IsDBNull(_dtGem.Rows(i).Item("ReturnRepairDetailID")) Then
                            If row.Item("ReturnRepairDetailID") = _ReturnRepairDetailID Then
                                _IsRowDelete = True
                            Else
                                _IsRowDelete = False
                            End If
                            If _IsRowDelete Then
                                _dtAllGem.Rows.Remove(row)
                            Else
                                j = j + 1
                            End If
                        Else
                            j = j + 1
                        End If
                    End While
                Next
            Else   ' dtPDiaItemgems no row , but dtstone has another gems id.It gemsid is deleted
                If _dtAllGem.Rows.Count > 0 Then
                    While j < _dtAllGem.Rows.Count
                        Dim row As DataRow
                        row = _dtAllGem.Rows(j)
                        If row.Item("ReturnRepairDetailID") = _ReturnRepairDetailID Then
                            _dtAllGem.Rows.Remove(row)
                        Else
                            j = j + 1
                        End If
                    End While

                End If
            End If

            Dim drPItemDetailStone As DataRow
            If _dtAllGem.Rows.Count <> 0 Then
                For i As Integer = 0 To _dtAllGem.Rows.Count - 1
                    If _dtAllGem.Rows(i).Item("ReturnRepairDetailID") = _ReturnRepairDetailID Then
                        For Each drvPItemDetailStone As DataRow In _dtGem.Rows
                            If Not IsDBNull(drvPItemDetailStone("ReturnRepairDetailID")) And drvPItemDetailStone("ReturnRepairDetailID") <> "" Then
                                If _dtAllGem.Rows(i).Item("ReturnRepairDetailID") = _ReturnRepairDetailID And _IsRowDelete <> True Then
                                    If _dtAllGem.Rows(i).Item("ReturnRepairGemID") = drvPItemDetailStone("ReturnRepairGemID") Then
                                        drvPItemDetailStone.BeginEdit()
                                        _dtAllGem.Rows(i).Item("ReturnRepairDetailID") = _ReturnRepairDetailID
                                        _dtAllGem.Rows(i).Item("GemsCategoryID") = drvPItemDetailStone("GemsCategoryID")
                                        _dtAllGem.Rows(i).Item("GemsCategory") = drvPItemDetailStone("GemsCategory")
                                        _dtAllGem.Rows(i).Item("Description") = drvPItemDetailStone("Description")                                                                              '
                                        _dtAllGem.Rows(i).Item("GemsK") = drvPItemDetailStone("GemsK")
                                        _dtAllGem.Rows(i).Item("GemsP") = drvPItemDetailStone("GemsP")
                                        _dtAllGem.Rows(i).Item("GemsY") = drvPItemDetailStone("GemsY")
                                        _dtAllGem.Rows(i).Item("GemsTK") = drvPItemDetailStone("GemsTK")
                                        _dtAllGem.Rows(i).Item("GemsTG") = drvPItemDetailStone("GemsTG")
                                        _dtAllGem.Rows(i).Item("YOrCOrG") = IIf(IsDBNull(drvPItemDetailStone("YOrCOrG")), "", drvPItemDetailStone("YOrCOrG"))
                                        _dtAllGem.Rows(i).Item("GemsTW") = drvPItemDetailStone("GemsTW")
                                        _dtAllGem.Rows(i).Item("QTY") = drvPItemDetailStone("QTY")
                                        _dtAllGem.Rows(i).Item("UnitPrice") = drvPItemDetailStone("UnitPrice")
                                        _dtAllGem.Rows(i).Item("Type") = drvPItemDetailStone("Type")
                                        _dtAllGem.Rows(i).Item("Amount") = drvPItemDetailStone("Amount")
                                        _dtAllGem.Rows(i).Item("IsNewGems") = drvPItemDetailStone("IsNewGems")
                                        drvPItemDetailStone.EndEdit()
                                        i += 1

                                    End If
                                End If
                            Else
                                drPItemDetailStone = _dtAllGem.NewRow()
                                If IsDBNull(drvPItemDetailStone("ReturnRepairGemID")) Then
                                    drPItemDetailStone("ReturnRepairGemID") = _GenerateController.GenerateKey(EnumSetting.GenerateKeyType.RepairReturnGemsItem, CommonInfo.EnumSetting.GenerateKeyType.RepairReturnGemsItem.ToString, Now.Date)
                                Else
                                    drPItemDetailStone("ReturnRepairGemID") = drvPItemDetailStone("ReturnRepairGemID")
                                End If
                                drPItemDetailStone("ReturnRepairDetailID") = _ReturnRepairDetailID
                                drPItemDetailStone("GemsCategoryID") = drvPItemDetailStone("GemsCategoryID")
                                drPItemDetailStone("GemsCategory") = drvPItemDetailStone("GemsCategory")
                                drPItemDetailStone("Description") = drvPItemDetailStone("Description")                                                             '
                                drPItemDetailStone("GemsK") = drvPItemDetailStone("GemsK")
                                drPItemDetailStone("GemsP") = drvPItemDetailStone("GemsP")
                                drPItemDetailStone("GemsY") = drvPItemDetailStone("GemsY")
                                drPItemDetailStone("GemsTK") = drvPItemDetailStone("GemsTK")
                                drPItemDetailStone("GemsTG") = drvPItemDetailStone("GemsTG")
                                drPItemDetailStone("YOrCOrG") = IIf(IsDBNull(drvPItemDetailStone("YOrCOrG")), "", drvPItemDetailStone("YOrCOrG"))
                                drPItemDetailStone("GemsTW") = drvPItemDetailStone("GemsTW")
                                drPItemDetailStone("QTY") = drvPItemDetailStone("QTY")
                                drPItemDetailStone("UnitPrice") = drvPItemDetailStone("UnitPrice")
                                drPItemDetailStone("Type") = drvPItemDetailStone("Type")
                                drPItemDetailStone("Amount") = drvPItemDetailStone("Amount")
                                drPItemDetailStone("IsNewGems") = drvPItemDetailStone("IsNewGems")
                                _dtAllGem.Rows.Add(drPItemDetailStone)
                                i += 1
                            End If
                        Next
                        _dtGem.DefaultView.RowFilter = ""
                        _dtGem.DefaultView.Sort = "ReturnRepairDetailID"

                    End If
                Next

            Else '''''' if _dtAllGem.Row.Count=0
                For Each drGems As DataRow In _dtGem.Rows
                    drPItemDetailStone = _dtAllGem.NewRow()
                    If IsDBNull(drGems("ReturnRepairGemID")) Then
                        drPItemDetailStone("ReturnRepairGemID") = _GenerateController.GenerateKey(EnumSetting.GenerateKeyType.RepairReturnGemsItem, CommonInfo.EnumSetting.GenerateKeyType.RepairReturnGemsItem.ToString, Now.Date)
                    Else
                        drPItemDetailStone("ReturnRepairGemID") = drGems("ReturnRepairGemID")
                    End If
                    drPItemDetailStone("ReturnRepairDetailID") = _ReturnRepairDetailID
                    drPItemDetailStone("GemsCategoryID") = drGems("GemsCategoryID")
                    drPItemDetailStone("GemsCategory") = drGems("GemsCategory")
                    drPItemDetailStone("Description") = drGems("Description")                                                             '
                    drPItemDetailStone("GemsK") = drGems("GemsK")
                    drPItemDetailStone("GemsP") = drGems("GemsP")
                    drPItemDetailStone("GemsY") = drGems("GemsY")
                    drPItemDetailStone("GemsTK") = drGems("GemsTK")
                    drPItemDetailStone("GemsTG") = drGems("GemsTG")
                    drPItemDetailStone("YOrCOrG") = IIf(IsDBNull(drGems("YOrCOrG")), "", drGems("YOrCOrG"))
                    drPItemDetailStone("GemsTW") = drGems("GemsTW")
                    drPItemDetailStone("QTY") = drGems("QTY")
                    drPItemDetailStone("UnitPrice") = drGems("UnitPrice")
                    drPItemDetailStone("Type") = drGems("Type")
                    drPItemDetailStone("Amount") = drGems("Amount")
                    drPItemDetailStone("IsNewGems") = drGems("IsNewGems")
                    _dtAllGem.Rows.Add(drPItemDetailStone)
                Next

            End If
            Dim drFind As Boolean = False

            If _dtAllGem.Rows.Count <> 0 Then
                For i As Integer = 0 To _dtAllGem.Rows.Count - 1
                    If _dtAllGem.Rows(i).Item("ReturnRepairDetailID") = _ReturnRepairDetailID Then
                        drFind = True
                    Else
                        drFind = False
                    End If
                Next

                If drFind = False Then
                    For Each drGems As DataRow In _dtGem.Rows
                        drPItemDetailStone = _dtAllGem.NewRow()
                        If IsDBNull(drGems("ReturnRepairGemID")) Then
                            drPItemDetailStone("ReturnRepairGemID") = _GenerateController.GenerateKey(EnumSetting.GenerateKeyType.RepairReturnGemsItem, CommonInfo.EnumSetting.GenerateKeyType.RepairReturnGemsItem.ToString, Now.Date)
                        Else
                            drPItemDetailStone("ReturnRepairGemID") = drGems("ReturnRepairGemID")
                        End If
                        drPItemDetailStone("ReturnRepairDetailID") = _ReturnRepairDetailID
                        drPItemDetailStone("GemsCategoryID") = drGems("GemsCategoryID")
                        drPItemDetailStone("GemsCategory") = drGems("GemsCategory")
                        drPItemDetailStone("Description") = drGems("Description")                                                             '
                        drPItemDetailStone("GemsK") = drGems("GemsK")
                        drPItemDetailStone("GemsP") = drGems("GemsP")
                        drPItemDetailStone("GemsY") = drGems("GemsY")
                        drPItemDetailStone("GemsTK") = drGems("GemsTK")
                        drPItemDetailStone("GemsTG") = drGems("GemsTG")
                        drPItemDetailStone("YOrCOrG") = IIf(IsDBNull(drGems("YOrCOrG")), "", drGems("YOrCOrG"))
                        drPItemDetailStone("GemsTW") = drGems("GemsTW")
                        drPItemDetailStone("QTY") = drGems("QTY")
                        drPItemDetailStone("UnitPrice") = drGems("UnitPrice")
                        drPItemDetailStone("Type") = drGems("Type")
                        drPItemDetailStone("Amount") = drGems("Amount")
                        drPItemDetailStone("IsNewGems") = drGems("IsNewGems")
                        _dtAllGem.Rows.Add(drPItemDetailStone)
                    Next
                End If
            End If
        End If
    End Sub
    Private Sub CalculateAlldTotalAmount()
        _AllTotalAmount = 0
        For j As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(j).IsNewRow Then
                _AllTotalAmount += CDec(grdDetail.Rows(j).Cells("ReturnNetAmount").FormattedValue)
            End If
        Next

        txtAllTotalAmt.Text = _AllTotalAmount
        txtAllAddOrSub.Text = "0"
        txtDiscountAmt.Text = "0"
        txtAllNetAmt.Text = txtAllTotalAmt.Text
        txtPaidAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(txtAdvanceRepairAmt.Text)))
        txtAllTotalPaidAmt.Text = CStr(CLng(txtPaidAmt.Text) + (CLng(txtAdvanceRepairAmt.Text)))
        txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalPaidAmt.Text))
    End Sub

    Private Sub txtAllNetAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAllNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtAllNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAllNetAmt.TextChanged
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtAdvanceRepairAmt.Text = "" Then txtAdvanceRepairAmt.Text = "0"

        txtAllAddOrSub.Text = CStr((CLng(txtAllNetAmt.Text) + CLng(txtDiscountAmt.Text)) - CLng(txtAllTotalAmt.Text))
        txtPaidAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(txtAdvanceRepairAmt.Text)))
        txtAllTotalPaidAmt.Text = CStr(CLng(txtPaidAmt.Text) + (CLng(txtAdvanceRepairAmt.Text)))
        txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalPaidAmt.Text))
    End Sub

    Private Sub txtDiscountAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscountAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDiscountAmt_TextChanged(sender As Object, e As EventArgs) Handles txtDiscountAmt.TextChanged
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
        If IsAllowAddOrSub() = False Then
            txtDiscountAmt.Text = "0"
        End If

        If txtDiscountAmt.Text <> "0" Then
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - CLng(txtDiscountAmt.Text))
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalPaidAmt.Text))
        Else
            txtAllNetAmt.Text = CStr(CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text))
        End If

        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtAdvanceRepairAmt.Text = "" Then txtAdvanceRepairAmt.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
        txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - CLng(txtDiscountAmt.Text))
        txtPaidAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAdvanceRepairAmt.Text))
        txtAllTotalPaidAmt.Text = CStr(CLng(txtPaidAmt.Text) + (CLng(txtAdvanceRepairAmt.Text)))
        txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalPaidAmt.Text))
    End Sub
    Private Function IsAllowAddOrSub() As Boolean
        If _IsAllowDiscount = False Then
            If Global_UserLevel <> "Administrator" Then
                If Val(txtDiscountAmt.Text) > Global_AllowDis Then
                    MsgBox("Discount Amount Is Not Allow!", MsgBoxStyle.Information, AppName)
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    Private Sub txtPaidAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPaidAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub


    Private Sub txtPaidAmt_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmt.TextChanged
        If txtAdvanceRepairAmt.Text = "" Then txtAdvanceRepairAmt.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        txtAllTotalPaidAmt.Text = CStr(CLng(txtAdvanceRepairAmt.Text) + CLng(txtPaidAmt.Text))
        txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalPaidAmt.Text))
    End Sub

    Private Sub btnReturnRepairSearch_Click(sender As Object, e As EventArgs) Handles btnReturnRepairSearch.Click
        Dim dt As New DataTable
        Dim dtOrderInvoiceCount As New DataTable
        Dim DataItem As DataRow
        Dim objRepairReturn As New RepairReturnHeaderInfo

        dt = _RepairReturnController.GetAllRepairReturnHeader()
        DataItem = DirectCast(SearchData.FindFast(dt, "RepairReturn List"), DataRow)
        If DataItem IsNot Nothing Then
            _ReturnRepairID = DataItem.Item("@ReturnRepairID")
            objRepairReturn = _RepairReturnController.GetRepairReturnHeaderInfoByID(_ReturnRepairID)

            _dtRepairDetail.Rows.Clear()
            _dtGem.Rows.Clear()
            _dtAllGem.Rows.Clear()

            _dtRepairDetail = _RepairReturnController.GetRepairReturnDetailByHeaderID(_ReturnRepairID)
            grdDetail.DataSource = _dtRepairDetail

            _dtAllGem = _RepairReturnController.GetRepairReturnGemDataByHeaderID(_ReturnRepairID)
            Dim dtTestStone1 As New DataTable
            dtTestStone1 = _dtAllGem

            ShowRepairReturnData(objRepairReturn)
            btnDelete.Enabled = True
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString
        End If
    End Sub

    Private Sub ShowRepairReturnData(ByVal objRepairReturn As RepairReturnHeaderInfo)
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        Dim dtBalance As New DataTable
        Dim _objRepair As CommonInfo.RepairHeaderInfo
        With objRepairReturn
            _RepairID = .RepairID
            txtRepairVoucherNo.Text = _RepairID
            dtpReturnDate.Value = .ReturnDate
            cboStaff.SelectedValue = .StaffID
            _objRepair = _RepairController.GetRepairHeaderInfo(_RepairID)
            With _objRepair
                txtRepairVoucherNo.Text = _RepairID
                _CustomerID = .CustomerID
                lblCustomerName.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerName & " (" & _CustomerController.GetCustomerByID(_CustomerID).CustomerCode & ")"
                lblCustomerAddress.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerAddress
                txtRepairDate.Text = Format(.RepairDate, "dd-MM-yyyy")
                txtDueDate.Text = Format(.DueDate, "dd-MM-yyyy")
            End With
            txtRemark.Text = .Remark
            txtAllTotalAmt.Text = .AllReturnTotalAmount
            txtAllAddOrSub.Text = .AllReturnAddOrSub
            _IsAllowDiscount = True
            txtDiscountAmt.Text = .ReturnDiscountAmount
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - CLng(txtDiscountAmt.Text))
            txtAdvanceRepairAmt.Text = IIf(IsDBNull(.AdvanceAmount) = True, 0, .AdvanceAmount)
            txtPaidAmt.Text = .ReturnPaidAmount
            txtAllTotalPaidAmt.Text = CStr(CLng(txtPaidAmt.Text) + (CLng(txtAdvanceRepairAmt.Text)))
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalPaidAmt.Text))
            _IsAllowDiscount = False
        End With
    End Sub
    Private Sub grdDetail_Click(sender As Object, e As EventArgs) Handles grdDetail.Click
        Dim GoldWeight As New GoldWeightInfo
        Dim objRepair As New RepairDetailInfo
        Dim GoldQualityInfo As New GoldQualityInfo

        If grdDetail.RowCount = 0 Then
            _IsUpdate = False
            Exit Sub
        End If

        With grdDetail
            _ReturnRepairDetailID = .CurrentRow.Cells("ReturnRepairDetailID").Value
            _RepairDetailID = .CurrentRow.Cells("RepairDetailID").Value
            If (.CurrentRow.Cells("BarcodeNo").Value = "-") Then
                txtBarcodeNo.Text = ""
            Else
                txtBarcodeNo.Text = .CurrentRow.Cells("BarcodeNo").Value
            End If
            lblItemName.Text = .CurrentRow.Cells("ItemName").Value & "    " & .CurrentRow.Cells("GoldQuality").Value & "    " & .CurrentRow.Cells("LengthOrWidth").Value
            _ItemName = .CurrentRow.Cells("ItemName").Value
            _StrGoldQuality = .CurrentRow.Cells("GoldQuality").Value
            _LengthWidth = .CurrentRow.Cells("LengthOrWidth").Value
            _GoldQualityID = .CurrentRow.Cells("GoldQualityID").Value
            _IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
            GoldQualityForTextChange()
            If _IsGram Then
                lblPercent.Text = "၁ ဂရမ်စျေး"
            Else
                lblPercent.Text = "၁ ကျပ်သားစျေး"
            End If
            txtCurrentPrice.Text = .CurrentRow.Cells("ChangeSaleRate").Value
            
            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("OrgItemTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtOItemK.Text = CStr(GoldWeight.WeightK)
            txtOItemP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtOItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtOItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtOItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtOItemTG.Text = Format(grdDetail.CurrentRow.Cells("OrgItemTG").Value, "0.000")
            _OrgItemTK = CDec(grdDetail.CurrentRow.Cells("OrgItemTK").Value)
            _OrgItemTG = CDec(grdDetail.CurrentRow.Cells("OrgItemTG").Value)
           
            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("OrgGemTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtOGemK.Text = CStr(GoldWeight.WeightK)
            txtOGemP.Text = CStr(GoldWeight.WeightP)
            txtOGemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtOGemTG.Text = Format(CDec(grdDetail.CurrentRow.Cells("OrgGemTG").Value), "0.000")
            _OrgGemsTG = CDec(grdDetail.CurrentRow.Cells("OrgGemTG").Value)
            _OrgGemsTK = CDec(grdDetail.CurrentRow.Cells("OrgGemTK").Value)

            
            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("OrgGoldTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtOGoldK.Text = CStr(GoldWeight.WeightK)
            txtOGoldP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtOGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtOGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtOGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtOGoldTG.Text = Format(grdDetail.CurrentRow.Cells("OrgGoldTG").Value, "0.000")
            _OrgGoldTG = CDec(grdDetail.CurrentRow.Cells("OrgGoldTG").Value)
            _OrgGoldTK = CDec(grdDetail.CurrentRow.Cells("OrgGoldTK").Value)

            
            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("ReturnItemTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtRItemK.Text = CStr(GoldWeight.WeightK)
            txtRItemP.Text = CStr(GoldWeight.WeightP)
            'txtRItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            If numberformat = 1 Then
                txtRItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtRItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            txtRItemTG.Text = Format(grdDetail.CurrentRow.Cells("ReturnItemTG").Value, "0.000")
            _ReturnItemTG = CDec(grdDetail.CurrentRow.Cells("ReturnItemTG").Value)
            _ReturnItemTK = CDec(grdDetail.CurrentRow.Cells("ReturnItemTK").Value)

            
            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("WasteTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteK.Text = CStr(GoldWeight.WeightK)
            txtWasteP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtWasteTG.Text = Format(grdDetail.CurrentRow.Cells("WasteTG").Value, "0.000")
            _WasteTG = CDec(grdDetail.CurrentRow.Cells("WasteTG").Value)
            _WasteTK = CDec(grdDetail.CurrentRow.Cells("WasteTK").Value)

            
            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("ReturnGemTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtRGemK.Text = CStr(GoldWeight.WeightK)
            txtRGemP.Text = CStr(GoldWeight.WeightP)
            txtRGemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtRGemTG.Text = Format(grdDetail.CurrentRow.Cells("ReturnGemTG").Value, "0.000")
            _ReturnGemsTG = CDec(grdDetail.CurrentRow.Cells("ReturnGemTG").Value)
            _ReturnGemsTK = CDec(grdDetail.CurrentRow.Cells("ReturnGemTK").Value)

            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("ReturnGoldTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtRGoldK.Text = CStr(GoldWeight.WeightK)
            txtRGoldP.Text = CStr(GoldWeight.WeightP)
            'txtRGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            If numberformat = 1 Then
                txtRGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtRGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            txtRGoldTG.Text = Format(CDec(txtRItemTG.Text) - CDec(txtRGemTG.Text), "0.000")
            _ReturnGoldTG = CDec(grdDetail.CurrentRow.Cells("ReturnGoldTG").Value)
            _ReturnGoldTK = CDec(grdDetail.CurrentRow.Cells("ReturnGoldTK").Value)

            CalculateDifferentWeight()

            txtDesignCharges.Text = .CurrentRow.Cells("DesignCharges").Value
            txtWhiteCharges.Text = .CurrentRow.Cells("WhiteCharges").Value
            txtPlatingCharges.Text = .CurrentRow.Cells("PlatingCharges").Value
            txtMountingCharges.Text = .CurrentRow.Cells("MountingCharges").Value

            txtGoldPrice.Text = .CurrentRow.Cells("ReturnGoldPrice").Value
            txtGemsPrice.Text = .CurrentRow.Cells("ReturnGemPrice").Value
            txtTotalAmt.Text = .CurrentRow.Cells("ReturnTotalAmount").Value
            txtAddSub.Text = .CurrentRow.Cells("ReturnAddOrSub").Value
            txtNetAmt.Text = .CurrentRow.Cells("ReturnNetAmount").Value
        End With

        _dtGem.Rows.Clear()
        If _dtAllGem.Rows.Count Then
            For i As Integer = 0 To _dtAllGem.Rows.Count - 1
                If Not IsDBNull(_dtAllGem.Rows(i).Item("ReturnRepairDetailID")) Then
                    If _dtAllGem.Rows(i).Item("ReturnRepairDetailID") = _ReturnRepairDetailID Then
                        Dim drItem As DataRow
                        drItem = _dtGem.NewRow
                        drItem("ReturnRepairGemID") = _dtAllGem.Rows(i).Item("ReturnRepairGemID")
                        drItem("ReturnRepairDetailID") = _dtAllGem.Rows(i).Item("ReturnRepairDetailID")
                        drItem("GemsCategoryID") = _dtAllGem.Rows(i).Item("GemsCategoryID")
                        drItem("GemsCategory") = _dtAllGem.Rows(i).Item("GemsCategory")
                        drItem("Description") = _dtAllGem.Rows(i).Item("Description")
                        drItem("GemsK") = _dtAllGem.Rows(i).Item("GemsK")
                        drItem("GemsP") = _dtAllGem.Rows(i).Item("GemsP")
                        drItem("GemsY") = _dtAllGem.Rows(i).Item("GemsY")
                        drItem("GemsTK") = _dtAllGem.Rows(i).Item("GemsTK")
                        drItem("GemsTG") = _dtAllGem.Rows(i).Item("GemsTG")
                        drItem("YOrCOrG") = _dtAllGem.Rows(i).Item("YOrCOrG")
                        drItem("GemsTW") = _dtAllGem.Rows(i).Item("GemsTW")
                        drItem("QTY") = _dtAllGem.Rows(i).Item("QTY")
                        drItem("UnitPrice") = _dtAllGem.Rows(i).Item("UnitPrice")
                        drItem("Type") = _dtAllGem.Rows(i).Item("Type")
                        drItem("Amount") = _dtAllGem.Rows(i).Item("Amount")
                        drItem("IsNewGems") = _dtAllGem.Rows(i).Item("IsNewGems")
                        _dtGem.Rows.Add(drItem)
                    End If
                End If
            Next
            grdGems.DataSource = _dtGem
        End If
        btnAdd.Text = "Update"
        _IsUpdate = True
    End Sub

    Private Sub CalculateWasteWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0.0"

        If Val(txtWasteTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtWasteTG.Text)
            _WasteTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _WasteTK = GoldWeight.GoldTK

            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteK.Text = CStr(GoldWeight.WeightK)
            txtWasteP.Text = CStr(GoldWeight.WeightP)
            txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0
            txtWasteK.Text = "0"
            txtWasteP.Text = "0"
            txtWasteY.Text = "0.0"
            'txtWasteTG.Text = "0.0"
        End If
    End Sub

    Private Sub txtWasteTG_TextChanged(sender As Object, e As EventArgs) Handles txtWasteTG.TextChanged
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0.0"

        If Val(txtWasteTG.Text.Trim) >= 0 And _IsGram = True Then
            CalculateWasteWeightForGram()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtRItemTG_TextChanged(sender As Object, e As EventArgs) Handles txtRItemTG.TextChanged
        If txtRItemTG.Text = "" Then txtRItemTG.Text = "0"

        If Val(txtRItemTG.Text.Trim) >= 0 And _IsGram = True Then
            CalculateItemWeightForGram()
        End If
        CalculateReturnGoldWeight()
        CalculateDifferentWeight()
        CalculateGoldPrice()
    End Sub

    Private Sub CalculateItemWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtRItemTG.Text = "" Then txtRItemTG.Text = "0.0"

        If Val(txtRItemTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtRItemTG.Text)
            _ReturnItemTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _ReturnItemTK = GoldWeight.GoldTK

            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtRItemK.Text = CStr(GoldWeight.WeightK)
            txtRItemP.Text = CStr(GoldWeight.WeightP)
            txtRItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        Else
            _ReturnItemTG = 0.0
            _ReturnItemTK = 0.0
            txtRItemK.Text = "0"
            txtRItemP.Text = "0"
            txtRItemY.Text = "0.0"
            'txtRItemTG.Text = "0.0"
        End If
    End Sub

    Private Sub txtCurrentPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtCurrentPrice_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentPrice.TextChanged
        CalculateGoldPrice()
    End Sub

    Private Sub txtRItemK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRItemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtRItemP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRItemP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtRItemY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRItemY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtRItemTG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRItemTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtWasteK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtWasteP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtWasteY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtWasteTG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub


    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If grdGems.CurrentCell Is grdGems.CurrentRow.Cells("GemsK") Or grdGems.CurrentCell Is grdGems.CurrentRow.Cells("GemsP") Or grdGems.CurrentCell Is grdGems.CurrentRow.Cells("Qty") Or grdGems.CurrentCell Is grdGems.CurrentRow.Cells("UnitPrice") Then
            If IsDBNull(grdGems.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdGems.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdGems.CurrentRow.Cells("Qty").FormattedValue) = False Or IsDBNull(grdGems.CurrentRow.Cells("UnitPrice").FormattedValue) = False Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If
        ElseIf grdGems.CurrentCell Is grdGems.CurrentRow.Cells("GemsY") Then
            If IsDBNull(grdGems.CurrentRow.Cells("GemsY").FormattedValue) = False Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Chr(46) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If
        End If
    End Sub
    Private Sub grdGems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdGems.EditingControlShowing
        If grdGems.CurrentCell Is grdGems.CurrentRow.Cells("GemsCategoryID") Or grdGems.CurrentCell Is grdGems.CurrentRow.Cells("Type") Then
            Exit Sub
        End If

        Dim txtbox As TextBox = CType(e.Control, TextBox)
        If Not (txtbox Is Nothing) Then
            AddHandler txtbox.KeyPress, AddressOf txtBox_KeyPress
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim dtGem As New DataTable

        dt = _RepairReturnController.GetRepairReturnForVoucher(_RepairID)
        If dt.Rows.Count > 0 Then
            frmPrint.RepairReturnVoucher(dt)
            frmPrint.WindowState = FormWindowState.Maximized
            frmPrint.Show()
        Else
            MsgBox("Please Select Repair Voucher First!", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If
    End Sub

    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("RepairReturn")
    End Sub

    Private Sub grdDetail_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdDetail.RowsRemoved
        Dim row As DataRow
        Dim j As Integer = _dtAllGem.Rows.Count() - 1
        While j >= 0
            row = _dtAllGem.Rows(j)
            If row.Item("ReturnRepairDetailID") = _ReturnRepairDetailID Then
                _dtAllGem.Rows.Remove(row)
            End If
            j = j - 1
        End While
        ClearDetail()
        CalculateAlldTotalAmount()
    End Sub


  

End Class