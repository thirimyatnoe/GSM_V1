Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
Public Class frm_PurchaseRow
    Implements IFormProcess

#Region "Declaration"
    Private _PurchaseHeaderID As String
    'Private _SaleInvoiceHeaderID As String
    Private _PurchaseDetailID As String
    Private _SaleInvoiceDetailID As String = ""
    Private _ConsignmentSaleItemID As String = ""
    Private _SaleGemsItemID As String = ""
    Private _ForSaleID As String
    Private _StaffID As String
    Private _ItemCategoryID As String
    Private _GoldQualityID As String
    Private _CustomerID As String
    Private _CurrentPurRate As Integer = 0
    Private _CurrentSaleRate As Integer = 0
    Private _DCurrentSaleRate As Integer = 0
    Private _SaleRate As Integer = 0
    Private _SGoldPrice As Integer = 0
    Private _SGemsPrice As Integer = 0
    Private _STotalCharges As Integer = 0

    Private _IsUpdate As Boolean = False
    Private _IsRowDelete As Boolean = False
    Private _IsRowUpdate As Boolean = False
    Private _IsPercentage As Boolean = False
    Private _IsGram As Boolean = False
    Private _IsFixPrice As Boolean = False
    Private _GemQTY As Integer = 0

    Private _TotalTK As Decimal = 0
    Private _TotalTG As Decimal = 0
    Private _WasteTK As Decimal = 0
    Private _WasteTG As Decimal = 0
    Private _PWasteTK As Decimal = 0
    Private _PWasteTG As Decimal = 0
    Private _GoldTK As Decimal = 0
    Private _GoldTG As Decimal = 0
    Private _TotalGemTK As Decimal = 0
    Private _TotalGemTG As Decimal = 0
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    Private GemTW As Decimal
    Private GemTK As Decimal
    Private GemTG As Decimal
    Private _TotalAmount As Integer = 0

    Private _dtDetail As New DataTable
    Private _dtPurGem As New DataTable
    Private _dtStone As New DataTable

    Private _IsOrder As Boolean
    Private _IsGem As Boolean
    Private _IsLooseDiamond As Boolean
    Private _DisWasteTG As Decimal = 0
    Dim itemid As String
    Dim Photo As String
    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
    Private _WeightType As String = ""
    Private _DRBP As String
    Private _ItemTK As Decimal = 0.0
    Private _ItemTG As Decimal = 0.0
    Private _Carat As Decimal = 0.0
    Private _GemsTW As Decimal = 0.0
    Private _DSaleRate As Integer = 0
    Private _SDiamondPrice As Integer = 0
    Private _SDTotalCharges As Integer = 0
    Private _SaleLooseDiamondDetailID As String


    Private objPurItemController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private objSaleItemInvoiceController As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private objSaleGemsController As SaleGems.ISaleGemsController = Factory.Instance.CreateSaleGemsController

    Private objCustomerController As Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private objItemCatController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private objItemNameController As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private objGoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private objGemsCatController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _CustomerController As Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private _OrderInvoiceController As OrderInvoice.IOrderInvoiceController = Factory.Instance.CreateOrderInvoiceController
    Private _SalesItemCon As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _ConverterCon As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GetKeyword As BusinessRule.Keyword.IKeywordController = Factory.Instance.CreateKeyWordController
    Private _GemsCat As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _CurrentController As InternationalDiamond.IIntDiamondPriceRateController = Factory.Instance.CreateIntDiamondController
    Private objCurrentPriceController As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private objSaleLooseDiamondController As SaleLooseDiamond.ISaleLooseDiamondController = Factory.Instance.CreateSaleLooseDiamondController
#End Region
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

    Dim NumberFormat As Integer

    Private Sub frm_PurchaseItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblLogInUserName.Text = Global_CurrentUser
        NumberFormat = Global_DecimalFormat

        lblCurrentLocationName.Text = Global_CurrentLocationName
        GetCombo()
        LoadCombos()
        LoadShapeCombos()
        LoadColorCombos()
        LoadClarityCombos()
        GetDItemCategory()
        ClearData()
        txtItemCode.Text = ""
        ClearDetail()
        FormatGridDetail()
        FormatGridGems()
        Me.KeyPreview = True
    End Sub

    Private Sub frm_PurchaseItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If objPurItemController.DeletePuchaseHeader(GetData()) Then
            chkOnlyGem.Enabled = True
            chkOnlyGem.Checked = False
            txtItemCode.Text = ""
            ClearDetail()
            ClearData()
            Return True
        Else
            MessageBox.Show("Can't Delete this Transaction!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        'chkOnlyGem.Enabled = True
        'chkOnlyGem.Checked = False


        txtItemCode.Text = ""
        cboPurchaseStaff.SelectedValue = -1
        ClearData()
        ClearDetail()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objPurchase As New PurchaseHeaderInfo
        If IsFillData() Then
            objPurchase = GetData()
            If objPurItemController.SavePuchaseItem(objPurchase, _dtDetail, _dtStone) Then
                If objPurchase.IsGem = False And objPurchase.IsLooseDiamond = False Then
                    _PurchaseHeaderID = objPurchase.PurchaseHeaderID
                    If (MsgBox("Do You Want To Save And Print Purchase Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                        Dim frmPrint As New frm_ReportViewer
                        Dim dt As New DataTable
                        Dim dtItem As New DataTable
                        dt = objPurItemController.GetPurchaseInvoicePrint(_PurchaseHeaderID)
                        dtItem = objPurItemController.GetPurchaseInvoiceDetailPrint(_PurchaseHeaderID)
                        frmPrint.PrintPurchaseInvoice(dt, dtItem)
                        frmPrint.WindowState = FormWindowState.Maximized
                        frmPrint.Show()
                        chkOnlyGem.Enabled = True
                        chkOnlyGem.Checked = False
                        txtItemCode.Text = ""
                        ClearData()
                        ClearDetail()
                    Else
                        chkOnlyGem.Enabled = True
                        chkOnlyGem.Checked = False
                        txtItemCode.Text = ""
                        ClearData()
                        ClearDetail()
                        Return True
                    End If
                ElseIf objPurchase.IsLooseDiamond = True Then
                    _PurchaseHeaderID = objPurchase.PurchaseHeaderID
                    If (MsgBox("Do You Want To Save And Print Purchase Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                        Dim frmPrint As New frm_ReportViewer
                        Dim dt As New DataTable
                        dt = objPurItemController.GetPurchaseInvoiceLooseDiamondPrint(_PurchaseHeaderID)
                        frmPrint.PrintPurchaseInvoiceforLooseDiamond(dt)
                        frmPrint.WindowState = FormWindowState.Maximized
                        frmPrint.Show()
                        chkDiamond.Enabled = True
                        chkDiamond.Checked = False
                        ClearData()
                        ClearDetail()
                    Else
                        chkDiamond.Enabled = True
                        chkDiamond.Checked = False
                        ClearData()
                        ClearDetail()
                        Return True
                    End If
            Else
                _PurchaseHeaderID = objPurchase.PurchaseHeaderID
                If (MsgBox("Do You Want To Save And Print Purchase Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                    Dim frmPrint As New frm_ReportViewer
                    Dim dt As New DataTable
                    dt = objPurItemController.GetPurchaseInvoiceOnlyGemPrint(_PurchaseHeaderID)
                    frmPrint.PrintPurchaseInvoiceforOnlyGem(dt)
                    frmPrint.WindowState = FormWindowState.Maximized
                    frmPrint.Show()
                    chkOnlyGem.Enabled = True
                    chkOnlyGem.Checked = False
                    ClearData()
                    ClearDetail()
                Else
                    chkOnlyGem.Enabled = True
                    chkOnlyGem.Checked = False
                    ClearData()
                    ClearDetail()
                    Return True
                End If
            End If
            Else
                Return False
            End If
        End If
    End Function

#Region "Methods"

    Public Function IsFillData() As Boolean

        If cboPurchaseStaff.SelectedIndex = -1 Then
            MsgBox("Please Select Staff !", MsgBoxStyle.Information, AppName)
            cboPurchaseStaff.Focus()
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

        If _dtDetail.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        Return True
    End Function

    Public Sub GetStaffCombo()
        cboPurchaseStaff.DisplayMember = "Staff_"
        cboPurchaseStaff.ValueMember = "StaffID"
        cboPurchaseStaff.DataSource = objStaffController.GetStaffList().DefaultView
        cboPurchaseStaff.SelectedIndex = -1
    End Sub
    Public Sub GetItemCategoryCombo()
        cboItemCategory.DisplayMember = "ItemCategory_"
        cboItemCategory.ValueMember = "@ItemCategoryID"
        cboItemCategory.DataSource = objItemCatController.GetAllItemCategory().DefaultView
        cboItemCategory.SelectedIndex = -1
    End Sub
    Public Sub GetGemsCategoryCombo()
        cboGemCategory.DisplayMember = "GemsCategory"
        cboGemCategory.ValueMember = "@GemsCategoryID"
        cboGemCategory.DataSource = objGemsCatController.GetAllGemsCategoryForGridCombo().DefaultView
        cboGemCategory.SelectedIndex = -1
    End Sub
    Public Sub GetGoldQualityCombo()
        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = objGoldQualityController.GetAllGoldQuality.DefaultView
        cboGoldQuality.SelectedIndex = -1
    End Sub
    Public Sub GetItemNameCombo()
        cboItemName.DisplayMember = "ItemName_"
        cboItemName.ValueMember = "ItemNameID"
        cboItemName.DataSource = objItemNameController.GetItemNameListByItemCategory(cboItemCategory.SelectedValue).DefaultView
        'cboItemName.SelectedIndex = 0
    End Sub
    Public Sub GetCombo()
        cboPurchaseStaff.DisplayMember = "Staff_"
        cboPurchaseStaff.ValueMember = "StaffID"
        cboPurchaseStaff.DataSource = objStaffController.GetStaffList().DefaultView
        cboPurchaseStaff.SelectedIndex = -1

        cboItemCategory.DisplayMember = "ItemCategory_"
        cboItemCategory.ValueMember = "@ItemCategoryID"
        cboItemCategory.DataSource = objItemCatController.GetAllItemCategory().DefaultView
        cboItemCategory.SelectedIndex = -1

        cboGemCategory.DisplayMember = "GemsCategory"
        cboGemCategory.ValueMember = "@GemsCategoryID"
        cboGemCategory.DataSource = objGemsCatController.GetAllGemsCategoryForGridCombo().DefaultView
        cboGemCategory.SelectedIndex = -1

        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = objGoldQualityController.GetAllGoldQuality.DefaultView
        cboGoldQuality.SelectedIndex = -1
    End Sub

    Private Sub RefreshItemNameCbo(ByVal ItemID As String)
        Dim dt As DataTable
        dt = objItemNameController.GetItemNameListByItemCategory(ItemID)
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

    Public Function GetData() As CommonInfo.PurchaseHeaderInfo
        Dim objPurchaseHeader As New CommonInfo.PurchaseHeaderInfo
        With objPurchaseHeader
            .PurchaseHeaderID = _PurchaseHeaderID
            .PurchaseDate = dtpPurchaseDate.Value
            .CustomerID = _CustomerID
            .StaffID = cboPurchaseStaff.SelectedValue
            .Address = txtAddress.Text
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .LocationID = Global_CurrentLocationID
            .GoldPrice = CLng(txtGoldPrice.Text)
            .GemsPrice = CLng(txtGemsPrice.Text)
            .IsGem = chkOnlyGem.Checked
            .AllTotalAmount = CLng(txtAllTotalAmt.Text)
            .AllAddOrSub = CInt(txtAddSub.Text)
            .AllPaidAmount = CLng(txtPaidAmt.Text)
            .IsChange = chkIsChange.Checked
            .IsLooseDiamond = chkDiamond.Checked
            

        End With
        Return objPurchaseHeader
    End Function
    Private Sub PurchaseInvoiceGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.PurchaseStock.ToString)
        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtPurchaseItemID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpPurchaseDate.Value)
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
        End If
    End Sub

    Public Sub ClearData()

        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        PurchaseInvoiceGenerateFormat()
        _PurchaseHeaderID = "0"
        dtpPurchaseDate.Value = Now

        _CustomerID = ""
        chkOnlyGem.Enabled = True
        'cboPurchaseStaff.SelectedValue = -1

        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""

        txtAllTotalAmt.Text = "0"
        txtAllNetAmt.Text = "0"
        txtAddSub.Text = "0"
        txtPaidAmt.Text = "0"
        txtBalanceAmt.Text = "0"

        txtRemark.Text = ""
        chkIsChange.Checked = False

        Dim dc As New DataColumn
        _dtDetail = New DataTable
        _dtDetail.Columns.Add("PurchaseDetailID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("PurchaseHeaderID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("SaleInvoiceDetailID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ConsignmentSaleItemID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("SaleGemsItemID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("SNo", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("ForSaleID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("BarcodeNo", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("OldSaleAmount", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("ItemCategoryID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ItemCategory", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GemsCategory", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ItemNameID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GemsName", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("CurrentPrice", System.Type.GetType("System.Int32"))
        _dtDetail.Columns.Add("SaleRate", System.Type.GetType("System.Int32"))
        _dtDetail.Columns.Add("TotalTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("TotalTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("GoldTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("GoldTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("WasteTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("WasteTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("PWasteTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("PWasteTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("TotalGemTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("TotalGemTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("Length", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("QTY", System.Type.GetType("System.Int16"))
        _dtDetail.Columns.Add("IsDamage", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("IsChange", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("TotalAmount", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("FixType", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GemTW", System.Type.GetType("System.Decimal"))
        '_dtDetail.Columns.Add("IsClose", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("GoldPrice", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("GemsPrice", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("IsFixPrice", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("IsDone", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("DoneAmount", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("IsSalePercent", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("SalePercent", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("SalePercentAmount", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("AddSub", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("NetAmount", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("IsShop", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("IsOrder", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("Photo", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("OriginalCode", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("IsDiamond", System.Type.GetType("System.Boolean"))
        _dtDetail.Columns.Add("SaleLooseDiamondDetailID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("PGemsCategoryID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("PGemsName", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("Color", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("Shape", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("Clarity", System.Type.GetType("System.String"))

        grdDetail.AutoGenerateColumns = False
        grdDetail.ReadOnly = True
        grdDetail.DataSource = _dtDetail

        FormatGridDetail()
        If chkOnlyGem.Checked = True Or chkDiamond.Checked = True Then
            grdDetail.Columns("GemsCategory").Visible = True
            grdDetail.Columns("GemsName").Visible = True
            grdDetail.Columns("TotalGemTG").Visible = True
            grdDetail.Columns("BarcodeNo").Visible = False
            grdDetail.Columns("ItemName").Visible = False
            grdDetail.Columns("TotalTG").Visible = False
        Else
            grdDetail.Columns("GemsCategory").Visible = False
            grdDetail.Columns("GemsName").Visible = False
            grdDetail.Columns("TotalGemTG").Visible = False
            grdDetail.Columns("BarcodeNo").Visible = True
            grdDetail.Columns("ItemName").Visible = True
            grdDetail.Columns("TotalTG").Visible = True
        End If


        Dim dcstone As New DataColumn
        Dim dcGem As DataColumn
        Dim dcQTY As DataColumn
        _dtStone = New DataTable

        _dtStone.Columns.Add("PurchaseGemID", System.Type.GetType("System.String"))
        _dtStone.Columns.Add("PurchaseDetailID", System.Type.GetType("System.String"))
        _dtStone.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtStone.Columns.Add("GemsName", System.Type.GetType("System.String"))
        _dtStone.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtStone.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtStone.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsK"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0

        _dtStone.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsP"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0
        _dtStone.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsY"
        dcGem.DataType = System.Type.GetType("System.Decimal")
        dcGem.DefaultValue = "0.0"
        _dtStone.Columns.Add(dcGem)

        _dtStone.Columns.Add("GemTW", System.Type.GetType("System.Decimal"))
        dcQTY = New DataColumn
        dcQTY.ColumnName = "QTY"
        dcQTY.DataType = System.Type.GetType("System.Int16")
        dcQTY.DefaultValue = 0
        _dtStone.Columns.Add(dcQTY)


        _dtStone.Columns.Add("FixType", System.Type.GetType("System.String"))
        _dtStone.Columns.Add("Discount", System.Type.GetType("System.Int64"))
        _dtStone.Columns.Add("PurchaseRate", System.Type.GetType("System.Int64"))

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtStone.Columns.Add(dc)

        'If tabStock.SelectedTab.Name = "TabGem" Then
        '    tabStock.SelectedIndex = 0
        '    tabStock.TabPages.Remove(TabGem)
        '    tabStock.TabPages.Add(TabStockItem)
        'Else
        '    tabStock.SelectedIndex = 0
        '    tabStock.TabPages.Remove(TabGem)
        'End If
        _IsGem = False

        If Global_CurrentUser = "Administrator" Then
            txtCurPrice.ReadOnly = False
        Else
            If Global_IsAllowSaleReturn Then
                txtCurPrice.ReadOnly = False
            Else
                txtCurPrice.ReadOnly = True
            End If
        End If
        chkNormal.Checked = True
        chkNormal.Enabled = True
        chkOnlyGem.Enabled = True
        chkDiamond.Enabled = True
        cboItemCategory.SelectedIndex = -1
        cboGoldQuality.SelectedIndex = -1
    End Sub

    Public Sub ClearForSaleVoucher()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _PurchaseHeaderID = "0"
        _CustomerID = ""
        dtpPurchaseDate.Value = Now
        cboPurchaseStaff.SelectedValue = -1
        cboPurchaseStaff.Text = ""
        _StaffID = ""
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtAllTotalAmt.Text = "0"
        txtAllNetAmt.Text = "0"
        txtAddSub.Text = "0"
        txtPaidAmt.Text = "0"
        txtBalanceAmt.Text = "0"
        txtRemark.Text = ""
        _IsGem = False
    End Sub
    Private Sub ClearDetail()
        If chkOnlyGem.Checked Then
            ClearGem()
        Else
            ClearStock()
        End If

        If chkOnlyGem.Checked = True Then
            tabStock.TabPages.Remove(TabStockItem)
            tabStock.TabPages.Remove(TabLooseDiamond)
        ElseIf chkDiamond.Checked = True Then
            tabStock.TabPages.Remove(TabStockItem)
            tabStock.TabPages.Remove(TabGem)
        Else
            'tabStock.TabPages.Item(0).Hide()
            tabStock.TabPages.Remove(TabGem)
            tabStock.TabPages.Remove(TabLooseDiamond)
            tabStock.TabPages.Remove(TabStockItem)
            tabStock.TabPages.Add(TabStockItem)
            tabStock.SelectedIndex = 0
        End If
        cboItemCategory.SelectedIndex = -1
        cboGoldQuality.SelectedIndex = -1
    End Sub
    Private Sub ClearGem()
        GemTK = 0
        GemTG = 0
        GemTW = 0

        cboGemCategory.SelectedIndex = -1
        txtGemName.Text = ""
        radRBP.Checked = True
        txtRBP.Text = "0"
        txtGemK.Text = "0"
        txtGemP.Text = "0"
        txtGemY.Text = "0.0"
        txtGemQTY.Text = "0"
        cboFixType.SelectedIndex = -1
        txtUnitPrice.Text = "0"
        txtTotalAmount.Text = "0"
        btnGemAdd.Text = "Add"
        _IsUpdate = False
    End Sub
    Private Sub ClearStock()
        If Global_UserLevel = "Administrator" Then
            txtSaleAmount.BackColor = Color.White
            txtSaleAmount.ReadOnly = False
            txtCurPrice.BackColor = Color.White
            txtCurPrice.ReadOnly = False
        Else
            txtSaleAmount.BackColor = Color.AntiqueWhite
            txtSaleAmount.ReadOnly = True
            txtCurPrice.BackColor = Color.AntiqueWhite
            txtCurPrice.ReadOnly = True
        End If
        chkPurchasePercent.Checked = False
        chkPurchasePercent.Enabled = False
        txtPurchasePercent.Text = "0"
        txtPurchasePercent.Enabled = False
        txtPurchcasePercentAmount.Enabled = False
        txtPurchcasePercentAmount.Text = "0"
        txtSaleAmount.Text = 0

        _PurchaseDetailID = ""
        _SaleInvoiceDetailID = ""
        _IsGram = False
        _IsPercentage = False
        _ForSaleID = ""
        _CurrentSaleRate = 0
        _IsOrder = False
        _SaleRate = 0
        _SGoldPrice = 0
        _SGemsPrice = 0
        _STotalCharges = 0
        lblOrgSaleRate.Text = ""
        lblOrgSaleRate.Visible = False

        chkShopGold.Checked = False
        lblOriginalCode.Visible = False
        lblOriginalCode.Text = ""
       
        lblSaleAmount.Visible = False
        txtItemCode.Visible = False
        txtSaleAmount.Visible = False
        btnBarcodeSearch.Visible = False
        chkIsFixPrice.Checked = False
        chkIsFixPrice.Visible = False
        chkIsFixPrice.Enabled = False
        chkIsDiamond.Visible = False
        

        GemTK = 0
        GemTG = 0
        GemTW = 0

        _GoldTK = 0
        _GoldTG = 0

        _TotalGemTK = 0
        _TotalGemTG = 0

        _TotalTK = 0
        _TotalTG = 0


        _WasteTK = 0
        _WasteTG = 0

        _PWasteTK = 0
        _PWasteTG = 0
        _DisWasteTG = 0
        _GemQTY = 0
        itemid = ""
        _IsFixPrice = False
        cboItemCategory.SelectedValue = -1
        cboItemName.SelectedValue = -1
        cboGoldQuality.SelectedValue = -1
        txtCurPrice.Text = "0"
        txtWidthLength.Text = ""
        txtQty.Text = "0"
        chkIsDamage.Checked = False
        chkIsExchange.Checked = False
        txtGoldK.Text = "0"
        txtGoldP.Text = "0"
        txtGoldY.Text = "0.0"
        txtGoldG.Text = "0.0"

        txtGemsK.Text = "0"
        txtGemsP.Text = "0"
        txtGemsY.Text = "0.0"
        txtGemsG.Text = "0.0"

        txtItemK.Text = "0"
        txtItemP.Text = "0"
        txtItemY.Text = "0.0"
        txtItemTG.Text = "0.0"

        txtSaleWasteK.Text = "0"
        txtSaleWasteP.Text = "0"
        txtSaleWasteY.Text = "0.0"
        txtSaleWasteG.Text = "0.0"

        txtPWasteK.Text = "0"
        txtPWasteP.Text = "0"
        txtPWasteY.Text = "0.0"
        txtPWasteG.Text = "0.0"

        txtItemTG.BackColor = Color.Linen
        txtItemK.BackColor = Color.Linen
        txtItemP.BackColor = Color.Linen
        txtItemY.BackColor = Color.Linen

        txtItemTG.ReadOnly = True
        txtItemK.ReadOnly = True
        txtItemP.ReadOnly = True
        txtItemY.ReadOnly = True

        txtPWasteK.BackColor = Color.Linen
        txtPWasteP.BackColor = Color.Linen
        txtPWasteY.BackColor = Color.Linen
        txtPWasteG.BackColor = Color.Linen

        txtPWasteK.ReadOnly = True
        txtPWasteP.ReadOnly = True
        txtPWasteY.ReadOnly = True
        txtPWasteG.ReadOnly = True

        chkDone.Checked = False
        txtDoneAmount.Text = "0"
        txtGoldPrice.Text = "0"
        txtGemsPrice.Text = "0"
        txtTotalAmt.Text = "0"
        chkIsDiamond.Checked = False

        btnAdd.Text = "Add"
        _IsUpdate = False
        Photo = ""
        If Global_LogoPhoto <> "" Then
            Try
                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                lblPhoto.Visible = False
            Catch ex As Exception
                lblItemImage.Image = Nothing
                lblPhoto.Visible = True
            End Try
        Else
            lblItemImage.Image = Nothing
            lblPhoto.Visible = True
        End If

        Dim dc As New DataColumn
        Dim dcGem As DataColumn
        Dim dcQTY As New DataColumn
        _dtPurGem = New DataTable

        _dtPurGem.Columns.Add("PurchaseGemID", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("PurchaseDetailID", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("GemsName", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtPurGem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtPurGem.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsK"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0
        _dtPurGem.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsP"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0
        _dtPurGem.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsY"
        dcGem.DataType = System.Type.GetType("System.Decimal")
        dcGem.DefaultValue = "0.0"
        _dtPurGem.Columns.Add(dcGem)

        _dtPurGem.Columns.Add("GemTW", System.Type.GetType("System.Decimal"))
        '_dtPurGem.Columns.Add("QTY", System.Type.GetType("System.Int16"))

        dcQTY = New DataColumn
        dcQTY.ColumnName = "QTY"
        dcQTY.DataType = System.Type.GetType("System.Int16")
        dcQTY.DefaultValue = 0
        _dtPurGem.Columns.Add(dcQTY)

        _dtPurGem.Columns.Add("FixType", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("Discount", System.Type.GetType("System.Int64"))
        _dtPurGem.Columns.Add("PurchaseRate", System.Type.GetType("System.Int64"))

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtPurGem.Columns.Add(dc)

        _dtPurGem.Columns.Add("IsRowDelete", System.Type.GetType("System.String"))

        grdGem.AutoGenerateColumns = False
        grdGem.DataSource = _dtPurGem
        FormatGridGems()

        txtDiscountWasteAmount.Text = "0"
        lblPercent.Text = ""
        _CurrentSaleRate = 0
        txtSaleRate.Text = "0"
        LblSalePercent.Text = ""
        txtDoneAmount.Enabled = False
        txtItemAddOrSub.Text = "0"
        txtNetAmount.Text = "0"

        chkDIsFixPrice.Visible = False
        chkDIsFixPrice.Checked = False
        chkShopDiamond.Checked = False
        txtDItemCode.Text = ""
        cboDCategory.SelectedValue = -1
        cboDColor.SelectedValue = -1
        cboDShape.SelectedValue = -1
        cboDClarity.SelectedValue = -1
        txtDescription.Text = "-"
        txtDQty.Text = 0
        txtDCurPrice.Text = 0
        chkDIsChange.Checked = False
        lblDOrgSaleRate.Visible = False
        lblDOrgSaleRate.Text = 0
        lblDSaleAmount.Visible = False
        lblDSaleAmount.Visible = False
        txtDSaleAmount.Text = 0
        txtDSaleAmount.Visible = False
        lblDOriginalCode.Visible = False
        txtDRBP.Text = 0
        txtDGram.Text = 0
        txtDiamondPrice.Text = 0
        chkDIsDone.Checked = False
        txtDDonePrice.Text = 0
        chkDPurchasePercent.Checked = False
        txtDPurchasePercent.Text = 0
        txtDPercentPrice.Text = 0
        txtDTotalAmount.Text = 0
        txtDAddOrSub.Text = 0
        txtDNetAmount.Text = 0
        _DCurrentSaleRate = 0
        _DSaleRate = 0
        _SDiamondPrice = 0
        _SDTotalCharges = 0
        _SaleLooseDiamondDetailID = 0
        cboItemCategory.SelectedIndex = -1
        cboGoldQuality.SelectedIndex = -1
    End Sub
    Private Sub ClearBarcodeData()
        If Global_UserLevel = "Administrator" Then
            txtSaleAmount.BackColor = Color.White
            txtSaleAmount.ReadOnly = False
            txtCurPrice.BackColor = Color.White
            txtCurPrice.ReadOnly = False
        Else
            txtSaleAmount.BackColor = Color.AntiqueWhite
            txtSaleAmount.ReadOnly = True
            txtCurPrice.BackColor = Color.AntiqueWhite
            txtCurPrice.ReadOnly = True
        End If
        chkPurchasePercent.Checked = False
        chkPurchasePercent.Enabled = False
        txtPurchasePercent.Text = "0"
        txtPurchasePercent.Enabled = False
        txtPurchcasePercentAmount.Enabled = False
        txtPurchcasePercentAmount.Text = "0"
        txtSaleAmount.Text = 0

        _PurchaseDetailID = ""
        _SaleInvoiceDetailID = ""
        _IsGram = False
        _IsPercentage = False
        _ForSaleID = ""
        _CurrentSaleRate = 0

        GemTK = 0
        GemTG = 0
        GemTW = 0

        _GoldTK = 0
        _GoldTG = 0

        _TotalGemTK = 0
        _TotalGemTG = 0

        _TotalTK = 0
        _TotalTG = 0


        _WasteTK = 0
        _WasteTG = 0

        _PWasteTK = 0
        _PWasteTG = 0
        _DisWasteTG = 0
        _GemQTY = 0
        itemid = ""
        _IsFixPrice = False
        cboItemCategory.SelectedValue = -1
        cboItemName.SelectedValue = -1
        cboGoldQuality.SelectedValue = -1
        txtCurPrice.Text = "0"
        txtWidthLength.Text = ""
        txtQty.Text = "0"
        chkIsDamage.Checked = False
        chkIsExchange.Checked = False
        txtGoldK.Text = "0"
        txtGoldP.Text = "0"
        txtGoldY.Text = "0.0"
        txtGoldG.Text = "0.0"

        txtGemsK.Text = "0"
        txtGemsP.Text = "0"
        txtGemsY.Text = "0.0"
        txtGemsG.Text = "0.0"

        txtItemK.Text = "0"
        txtItemP.Text = "0"
        txtItemY.Text = "0.0"
        txtItemTG.Text = "0.0"

        txtSaleWasteK.Text = "0"
        txtSaleWasteP.Text = "0"
        txtSaleWasteY.Text = "0.0"
        txtSaleWasteG.Text = "0.0"

        txtPWasteK.Text = "0"
        txtPWasteP.Text = "0"
        txtPWasteY.Text = "0.0"
        txtPWasteG.Text = "0.0"

        txtItemTG.BackColor = Color.Linen
        txtItemK.BackColor = Color.Linen
        txtItemP.BackColor = Color.Linen
        txtItemY.BackColor = Color.Linen

        txtItemTG.ReadOnly = True
        txtItemK.ReadOnly = True
        txtItemP.ReadOnly = True
        txtItemY.ReadOnly = True

        txtPWasteK.BackColor = Color.Linen
        txtPWasteP.BackColor = Color.Linen
        txtPWasteY.BackColor = Color.Linen
        txtPWasteG.BackColor = Color.Linen

        txtPWasteK.ReadOnly = True
        txtPWasteP.ReadOnly = True
        txtPWasteY.ReadOnly = True
        txtPWasteG.ReadOnly = True

        chkDone.Checked = False
        txtDoneAmount.Text = "0"
        txtGoldPrice.Text = "0"
        txtGemsPrice.Text = "0"
        txtTotalAmt.Text = "0"
        chkIsDiamond.Checked = False

        lblOriginalCode.Text = ""
        lblOrgSaleRate.Text = ""
        _SaleRate = 0
        _SGoldPrice = 0
        _SGemsPrice = 0
        _STotalCharges = 0

        btnAdd.Text = "Add"
        btnDAdd.Text = "Add"
        _IsUpdate = False
        Photo = ""
        If Global_LogoPhoto <> "" Then
            Try
                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                lblPhoto.Visible = False
            Catch ex As Exception
                lblItemImage.Image = Nothing
                lblPhoto.Visible = True
            End Try
        Else
            lblItemImage.Image = Nothing
            lblPhoto.Visible = True
        End If

        Dim dc As New DataColumn
        Dim dcGem As DataColumn
        Dim dcQTY As New DataColumn
        _dtPurGem = New DataTable

        _dtPurGem.Columns.Add("PurchaseGemID", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("PurchaseDetailID", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("GemsName", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtPurGem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtPurGem.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsK"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0
        _dtPurGem.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsP"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0
        _dtPurGem.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsY"
        dcGem.DataType = System.Type.GetType("System.Decimal")
        dcGem.DefaultValue = "0.0"
        _dtPurGem.Columns.Add(dcGem)

        _dtPurGem.Columns.Add("GemTW", System.Type.GetType("System.Decimal"))
        '_dtPurGem.Columns.Add("QTY", System.Type.GetType("System.Int16"))

        dcQTY = New DataColumn
        dcQTY.ColumnName = "QTY"
        dcQTY.DataType = System.Type.GetType("System.Int16")
        dcQTY.DefaultValue = 0
        _dtPurGem.Columns.Add(dcQTY)

        _dtPurGem.Columns.Add("FixType", System.Type.GetType("System.String"))
        _dtPurGem.Columns.Add("Discount", System.Type.GetType("System.Int64"))
        _dtPurGem.Columns.Add("PurchaseRate", System.Type.GetType("System.Int64"))

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtPurGem.Columns.Add(dc)

        _dtPurGem.Columns.Add("IsRowDelete", System.Type.GetType("System.String"))

        grdGem.AutoGenerateColumns = False
        grdGem.DataSource = _dtPurGem
        FormatGridGems()

        txtDiscountWasteAmount.Text = "0"
        lblPercent.Text = ""
        _CurrentSaleRate = 0
        txtSaleRate.Text = "0"
        LblSalePercent.Text = ""
        txtDoneAmount.Enabled = False
        txtItemAddOrSub.Text = "0"
        txtNetAmount.Text = "0"

        chkDIsFixPrice.Visible = False
        chkDIsFixPrice.Checked = False
        chkShopDiamond.Checked = False
        txtDItemCode.Text = ""
        cboDCategory.SelectedValue = -1
        cboDColor.SelectedValue = -1
        cboDShape.SelectedValue = -1
        cboDClarity.SelectedValue = -1
        txtDescription.Text = "-"
        txtDQty.Text = 0
        txtDCurPrice.Text = 0
        chkDIsChange.Checked = False
        lblDOrgSaleRate.Visible = False
        lblDOrgSaleRate.Text = 0
        lblDSaleAmount.Visible = False
        txtDSaleAmount.Text = 0
        txtDSaleAmount.Visible = False
        lblDOriginalCode.Visible = False
        txtDRBP.Text = 0
        txtDGram.Text = 0
        txtDiamondPrice.Text = 0
        chkDIsDone.Checked = False
        txtDDonePrice.Text = 0
        chkDPurchasePercent.Checked = False
        txtDPurchasePercent.Text = 0
        txtDPercentPrice.Text = 0
        txtDTotalAmount.Text = 0
        txtDAddOrSub.Text = 0
        txtDNetAmount.Text = 0
        _DCurrentSaleRate = 0
        _DSaleRate = 0
        _SDiamondPrice = 0
        _SDTotalCharges = 0
        _SaleLooseDiamondDetailID = 0
    End Sub

    Public Sub FormatGridDetail()
        With grdDetail
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "PurchaseDetailID"
                .DataPropertyName = "PurchaseDetailID"
                .Name = "PurchaseDetailID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "PurchaseHeaderID"
                .DataPropertyName = "PurchaseHeaderID"
                .Name = "PurchaseHeaderID"
                .Visible = False
            End With
            .Columns.Add(dcID)

            Dim dcSaleDetailID As New DataGridViewTextBoxColumn()
            With dcSaleDetailID
                .HeaderText = "SaleInvoiceDetailID"
                .DataPropertyName = "SaleInvoiceDetailID"
                .Name = "SaleInvoiceDetailID"
                .Visible = False
            End With
            .Columns.Add(dcSaleDetailID)

            Dim dcConsignmentSaleItemID As New DataGridViewTextBoxColumn()
            With dcConsignmentSaleItemID
                .HeaderText = "ConsignmentSaleItemID"
                .DataPropertyName = "ConsignmentSaleItemID"
                .Name = "ConsignmentSaleItemID"
                .Visible = False
            End With


            Dim dcSaleGemsItemID As New DataGridViewTextBoxColumn()
            With dcSaleGemsItemID
                .HeaderText = "SaleGemsItemID"
                .DataPropertyName = "SaleGemsItemID"
                .Name = "SaleGemsItemID"
                .Visible = False
            End With
            .Columns.Add(dcSaleGemsItemID)

            Dim dcSaleLooseDiamondDetailID As New DataGridViewTextBoxColumn()
            With dcSaleLooseDiamondDetailID
                .HeaderText = "SaleLooseDiamondDetailID"
                .DataPropertyName = "SaleLooseDiamondDetailID"
                .Name = "SaleLooseDiamondDetailID"
                .Visible = False
            End With
            .Columns.Add(dcSaleLooseDiamondDetailID)

            Dim dcFID As New DataGridViewTextBoxColumn()
            With dcFID
                .HeaderText = "ForSaleID"
                .DataPropertyName = "ForSaleID"
                .Name = "ForSaleID"
                .Visible = False
            End With
            .Columns.Add(dcFID)



            Dim dcSaleAmount As New DataGridViewTextBoxColumn
            With dcSaleAmount
                .HeaderText = "OldSaleAmount"
                .DataPropertyName = "OldSaleAmount"
                .Name = "OldSaleAmount"
                .Visible = False
            End With
            .Columns.Add(dcSaleAmount)


            Dim dcItemCatID As New DataGridViewTextBoxColumn
            With dcItemCatID
                .HeaderText = "ItemCategoryID"
                .DataPropertyName = "ItemCategoryID"
                .Name = "ItemCategoryID"
                .Visible = False
            End With
            .Columns.Add(dcItemCatID)

            Dim dcSNO As New DataGridViewTextBoxColumn
            With dcSNO
                .HeaderText = "SNo"
                .DataPropertyName = "SNo"
                .Name = "SNo"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 30
                .Visible = True
            End With

            .Columns.Add(dcSNO)
            Dim dcBarNo As New DataGridViewTextBoxColumn
            With dcBarNo
                .HeaderText = "BarcodeNo"
                .DataPropertyName = "BarcodeNo"
                .Name = "BarcodeNo"
                .Visible = True
                .Width = 130
            End With
            .Columns.Add(dcBarNo)


            Dim dcItemCategory As New DataGridViewTextBoxColumn
            With dcItemCategory
                .HeaderText = "ItemCategory"
                .DataPropertyName = "ItemCategory"
                .Name = "ItemCategory"
                .Width = 150
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = False
            End With
            .Columns.Add(dcItemCategory)

            Dim dcGemsCategory As New DataGridViewTextBoxColumn
            With dcGemsCategory
                .HeaderText = "GemsCategory"
                .DataPropertyName = "GemsCategory"
                .Name = "GemsCategory"
                .Width = 200
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = False
            End With
            .Columns.Add(dcGemsCategory)

            Dim dcItemNameID As New DataGridViewTextBoxColumn
            With dcItemNameID
                .HeaderText = "ItemNameID"
                .DataPropertyName = "ItemNameID"
                .Name = "ItemNameID"
                .Visible = False
            End With
            .Columns.Add(dcItemNameID)

            Dim dcItemName As New DataGridViewTextBoxColumn
            With dcItemName
                .HeaderText = "ItemName"
                .DataPropertyName = "ItemName"
                .Name = "ItemName"
                .Width = 180
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = True
            End With
            .Columns.Add(dcItemName)

            Dim dcGemsName As New DataGridViewTextBoxColumn
            With dcGemsName
                .HeaderText = "Description"
                .DataPropertyName = "GemsName"
                .Name = "GemsName"
                .Width = 186
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = False
            End With
            .Columns.Add(dcGemsName)

            Dim dcGoldQualityID As New DataGridViewTextBoxColumn
            With dcGoldQualityID
                .HeaderText = "GoldQualityID"
                .DataPropertyName = "GoldQualityID"
                .Name = "GoldQualityID"
                .Visible = False
            End With
            .Columns.Add(dcGoldQualityID)

            Dim dcGoldQuality As New DataGridViewTextBoxColumn
            With dcGoldQuality
                .HeaderText = "GoldQuality"
                .DataPropertyName = "GoldQuality"
                .Name = "GoldQuality"
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = False
            End With
            .Columns.Add(dcGoldQuality)

            Dim dcCurrentPrice As New DataGridViewTextBoxColumn
            With dcCurrentPrice
                .HeaderText = "CurrentPrice"
                .DataPropertyName = "CurrentPrice"
                .Name = "CurrentPrice"
                .Visible = False
                .DefaultCellStyle.Format = "0"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcCurrentPrice)

            Dim dcSaleRate As New DataGridViewTextBoxColumn
            With dcSaleRate
                .HeaderText = "SaleRate"
                .DataPropertyName = "SaleRate"
                .Name = "SaleRate"
                .Visible = False
                .DefaultCellStyle.Format = "0"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcSaleRate)

            Dim dcTotalTK As New DataGridViewTextBoxColumn
            With dcTotalTK
                .HeaderText = "TotalTK"
                .DataPropertyName = "TotalTK"
                .Name = "TotalTK"
                .Width = 90
                .Visible = False
            End With
            .Columns.Add(dcTotalTK)

            Dim dcTotalTG As New DataGridViewTextBoxColumn()
            With dcTotalTG
                .HeaderText = "Item(G)"
                .DataPropertyName = "TotalTG"
                .Name = "TotalTG"
                .DefaultCellStyle.Format = "0.000"
                .Width = 80
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcTotalTG)

            Dim dcGoldTK As New DataGridViewTextBoxColumn()
            With dcGoldTK
                .HeaderText = "GoldTK"
                .DataPropertyName = "GoldTK"
                .Name = "GoldTK"
                .Visible = False
            End With
            .Columns.Add(dcGoldTK)

            Dim dcGoldTG As New DataGridViewTextBoxColumn()
            With dcGoldTG
                .HeaderText = "GoldTG"
                .DataPropertyName = "GoldTG"
                .Name = "GoldTG"
                .Visible = False
            End With
            .Columns.Add(dcGoldTG)

            Dim dcGemTK As New DataGridViewTextBoxColumn()
            With dcGemTK
                .HeaderText = "TotalGemTK"
                .DataPropertyName = "TotalGemTK"
                .Name = "TotalGemTK"
                .Visible = False
            End With
            .Columns.Add(dcGemTK)

            Dim dcGemTG As New DataGridViewTextBoxColumn()
            With dcGemTG
                .HeaderText = "Gem(G)"
                .DataPropertyName = "TotalGemTG"
                .Name = "TotalGemTG"
                .Visible = False
                .Width = 80
                .DefaultCellStyle.Format = "0.000"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcGemTG)

            Dim dcWasteTK As New DataGridViewTextBoxColumn()
            With dcWasteTK
                .HeaderText = "WasteTK"
                .DataPropertyName = "WasteTK"
                .Name = "WasteTK"
                .Visible = False
            End With
            .Columns.Add(dcWasteTK)

            Dim dcWasteTG As New DataGridViewTextBoxColumn()
            With dcWasteTG
                .HeaderText = "WasteTG"
                .DataPropertyName = "WasteTG"
                .Name = "WasteTG"
                .Visible = False
            End With
            .Columns.Add(dcWasteTG)

            Dim dcPWasteTK As New DataGridViewTextBoxColumn()
            With dcPWasteTK
                .HeaderText = "PWasteTK"
                .DataPropertyName = "PWasteTK"
                .Name = "PWasteTK"
                .Visible = False
            End With
            .Columns.Add(dcPWasteTK)

            Dim dcPWasteTG As New DataGridViewTextBoxColumn()
            With dcPWasteTG
                .HeaderText = "PWasteTG"
                .DataPropertyName = "PWasteTG"
                .Name = "PWasteTG"
                .Visible = False
            End With
            .Columns.Add(dcPWasteTG)

            Dim dcLength As New DataGridViewTextBoxColumn()
            With dcLength
                .HeaderText = "Length"
                .DataPropertyName = "Length"
                .Name = "Length"
                .Visible = False
            End With
            .Columns.Add(dcLength)

            Dim dcQTY As New DataGridViewTextBoxColumn()
            With dcQTY
                .HeaderText = "QTY"
                .DataPropertyName = "QTY"
                .Name = "QTY"
                .Width = 48
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcQTY)

            Dim dcIsFixPrice As New DataGridViewTextBoxColumn
            With dcIsFixPrice
                .HeaderText = "IsFixPrice"
                .DataPropertyName = "IsFixPrice"
                .Name = "IsFixPrice"
                .Visible = False
            End With
            .Columns.Add(dcIsFixPrice)

            Dim dcIsDone As New DataGridViewTextBoxColumn()
            With dcIsDone
                .HeaderText = "IsDone"
                .DataPropertyName = "IsDone"
                .Name = "IsDone"
                .Visible = False
            End With
            .Columns.Add(dcIsDone)

            Dim dcDoneAmount As New DataGridViewTextBoxColumn()
            With dcDoneAmount
                .HeaderText = "DoneAmount"
                .DataPropertyName = "DoneAmount"
                .Name = "DoneAmount"
                .Visible = False
            End With
            .Columns.Add(dcDoneAmount)

            Dim dcDmg As New DataGridViewTextBoxColumn()
            With dcDmg
                .HeaderText = "IsDamage"
                .DataPropertyName = "IsDamage"
                .Name = "IsDamage"
                .Visible = False
            End With
            .Columns.Add(dcDmg)

            Dim dcChange As New DataGridViewTextBoxColumn()
            With dcChange
                .HeaderText = "IsChange"
                .DataPropertyName = "IsChange"
                .Name = "IsChange"
                .Visible = False
            End With
            .Columns.Add(dcChange)

            Dim dcTotalAmount As New DataGridViewTextBoxColumn()
            With dcTotalAmount
                .HeaderText = "TotalAmount"
                .DataPropertyName = "TotalAmount"
                .Name = "TotalAmount"
                .Width = 90
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Visible = False
            End With
            .Columns.Add(dcTotalAmount)

            Dim dc5 As New DataGridViewTextBoxColumn()
            dc5.HeaderText = "RBP"
            dc5.DataPropertyName = "YOrCOrG"
            dc5.Name = "YOrCOrG"
            dc5.Width = 55
            dc5.Visible = False
            dc5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc5.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc5)

            Dim dc7 As New DataGridViewComboBoxColumn()
            dc7.HeaderText = "Fix"
            dc7.DataPropertyName = "FixType"
            dc7.Name = "FixType"
            dc7.Width = 80
            dc7.Visible = False
            .Columns.Add(dc7)

            Dim dc10 As New DataGridViewTextBoxColumn()
            dc10.HeaderText = "GemTW"
            dc10.DataPropertyName = "GemTW"
            dc10.Name = "GemTW"
            dc10.Visible = False
            dc10.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc10)

            'Dim dcClose As New DataGridViewTextBoxColumn()
            'With dcClose
            '    .HeaderText = "IsClose"
            '    .DataPropertyName = "IsClose"
            '    .Name = "IsClose"
            '    .Visible = False
            'End With
            '.Columns.Add(dcClose)

            Dim dcGoldFee As New DataGridViewTextBoxColumn
            With dcGoldFee
                .HeaderText = "GoldPrice"
                .DataPropertyName = "GoldPrice"
                .Name = "GoldPrice"
                .Visible = False
            End With
            .Columns.Add(dcGoldFee)

            Dim dcDiamond As New DataGridViewTextBoxColumn
            With dcDiamond
                .HeaderText = "GemsPrice"
                .DataPropertyName = "GemsPrice"
                .Name = "GemsPrice"
                .Visible = False
            End With
            .Columns.Add(dcDiamond)

            Dim dcIsSalePercent As New DataGridViewTextBoxColumn
            With dcIsSalePercent
                .HeaderText = "IsSalePercent"
                .DataPropertyName = "IsSalePercent"
                .Name = "IsSalePercent"
                .Visible = False
            End With
            .Columns.Add(dcIsSalePercent)

            Dim dcSalePercent As New DataGridViewTextBoxColumn
            With dcSalePercent
                .HeaderText = "SalePercent"
                .DataPropertyName = "SalePercent"
                .Name = "SalePercent"
                .Visible = False
            End With
            .Columns.Add(dcSalePercent)

            Dim dcSalePercentAmount As New DataGridViewTextBoxColumn
            With dcSalePercentAmount
                .HeaderText = "SalePercentAmount"
                .DataPropertyName = "SalePercentAmount"
                .Name = "SalePercentAmount"
                .Visible = False
            End With
            .Columns.Add(dcSalePercentAmount)

            Dim dcAddSub As New DataGridViewTextBoxColumn
            With dcAddSub
                .HeaderText = "AddSub"
                .DataPropertyName = "AddSub"
                .Name = "AddSub"
                .Visible = False
            End With
            .Columns.Add(dcAddSub)

            Dim dcNetAmount As New DataGridViewTextBoxColumn()
            With dcNetAmount
                .HeaderText = "NetAmount"
                .DataPropertyName = "NetAmount"
                .Name = "NetAmount"
                .Width = 80
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "###,##0.##"
                .Visible = True
            End With
            .Columns.Add(dcNetAmount)

            Dim dcIsShop As New DataGridViewCheckBoxColumn()
            With dcIsShop
                .HeaderText = "Shop"
                .DataPropertyName = "IsShop"
                .Name = "IsShop"
                .Width = 40
                .Visible = True
            End With
            .Columns.Add(dcIsShop)

            Dim dcIsOrder As New DataGridViewTextBoxColumn()
            With dcIsOrder
                .HeaderText = "IsOrder"
                .DataPropertyName = "IsOrder"
                .Name = "IsOrder"
                .Visible = False
            End With
            .Columns.Add(dcIsOrder)

            Dim dcPhoto As New DataGridViewTextBoxColumn()
            With dcPhoto
                .HeaderText = "Photo"
                .DataPropertyName = "Photo"
                .Name = "Photo"
                .Visible = False
            End With
            .Columns.Add(dcPhoto)

            Dim dcOrgCode As New DataGridViewTextBoxColumn()
            With dcOrgCode
                .HeaderText = "OriginalCode"
                .DataPropertyName = "OriginalCode"
                .Name = "OriginalCode"
                .Visible = False
            End With
            .Columns.Add(dcOrgCode)


            Dim dcIsDiamond As New DataGridViewTextBoxColumn()
            With dcIsDiamond
                .HeaderText = "IsDiamond"
                .DataPropertyName = "IsDiamond"
                .Name = "IsDiamond"
                .Visible = False
            End With
            .Columns.Add(dcIsDiamond)

            Dim dcPGemsName As New DataGridViewTextBoxColumn
            With dcPGemsName
                .HeaderText = "PGemsName"
                .DataPropertyName = "PGemsName"
                .Name = "PGemsName"
                .Visible = False
            End With
            .Columns.Add(dcPGemsName)

            Dim dcColor As New DataGridViewTextBoxColumn
            With dcColor
                .HeaderText = "Color"
                .DataPropertyName = "Color"
                .Name = "Color"
                .Visible = False
            End With
            .Columns.Add(dcColor)

            Dim dcShape As New DataGridViewTextBoxColumn
            With dcShape
                .HeaderText = "Shape"
                .DataPropertyName = "Shape"
                .Name = "Shape"
                .Visible = False
            End With
            .Columns.Add(dcShape)

            Dim dcClarity As New DataGridViewTextBoxColumn
            With dcClarity
                .HeaderText = "Clarity"
                .DataPropertyName = "Clarity"
                .Name = "Clarity"
                .Visible = False
            End With
            .Columns.Add(dcClarity)
            Dim dcPGemsCategoryID As New DataGridViewTextBoxColumn
            With dcPGemsCategoryID
                .HeaderText = "PGemsCategoryID"
                .DataPropertyName = "PGemsCategoryID"
                .Name = "PGemsCategoryID"
                .Visible = False
            End With
            .Columns.Add(dcPGemsCategoryID)
        End With
    End Sub
    Public Sub FormatGridGems()
        With grdGem
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40
            .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "PurchaseGemID"
                .DataPropertyName = "PurchaseGemID"
                .Name = "PurchaseGemID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "PurchaseDetailID"
                .DataPropertyName = "PurchaseDetailID"
                .Name = "PurchaseDetailID"
                .Visible = False
            End With
            .Columns.Add(dcID)

            Dim dcCategory As New DataGridViewComboBoxColumn()
            With dcCategory
                .HeaderText = "Category"
                .DataPropertyName = "GemsCategoryID"
                .Name = "GemsCategoryID"
                .DataSource = objGemsCatController.GetAllGemsCategoryForGridCombo()
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .DisplayMember = "GemsCategory"
                .ValueMember = "@GemsCategoryID"
                .Width = 150
                .Visible = True
            End With
            .Columns.Add(dcCategory)

            Dim dcName As New DataGridViewTextBoxColumn()
            dcName.HeaderText = "Description"
            dcName.DataPropertyName = "GemsName"
            dcName.Name = "GemsName"
            dcName.Width = 120
            dcName.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcName.Visible = True

            dcName.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcName)


            Dim dc5 As New DataGridViewTextBoxColumn()
            dc5.HeaderText = "RBP"
            dc5.DataPropertyName = "YOrCOrG"
            dc5.Name = "YOrCOrG"
            dc5.Width = 56
            dc5.Visible = True
            dc5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc5.SortMode = DataGridViewColumnSortMode.NotSortable
            'dc5.DefaultCellStyle.Font = New Font("Tahoma", 9)
            .Columns.Add(dc5)

            Dim dcGK As New DataGridViewTextBoxColumn()
            With dcGK
                .HeaderText = "K"
                .DataPropertyName = "GemsK"
                .Name = "GemsK"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGK)

            Dim dcGP As New DataGridViewTextBoxColumn()
            With dcGP
                .HeaderText = "P"
                .DataPropertyName = "GemsP"
                .Name = "GemsP"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGP)

            Dim dcGY As New DataGridViewTextBoxColumn()
            With dcGY
                .HeaderText = "Y"
                .DataPropertyName = "GemsY"
                .Name = "GemsY"
                .Width = 40
                .Visible = True
                .DefaultCellStyle.Format = "0.0"
                .MaxInputLength = 3
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGY)

            Dim dc6 As New DataGridViewTextBoxColumn()
            dc6.HeaderText = "QTY"
            dc6.DataPropertyName = "QTY"
            dc6.Name = "QTY"
            dc6.Width = 50
            dc6.Visible = True
            dc6.DefaultCellStyle.Format = "0"
            dc6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc6.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc6)

            Dim dc7 As New DataGridViewComboBoxColumn()
            dc7.HeaderText = "Fix"
            dc7.DataPropertyName = "FixType"
            dc7.Name = "FixType"
            dc7.Items.AddRange(New String() {"Fix", "ByWeight", "ByQty"})
            dc7.Width = 80
            dc7.Visible = True
            .Columns.Add(dc7)

            Dim dcDis As New DataGridViewTextBoxColumn()
            dcDis.HeaderText = "Discount"
            dcDis.DataPropertyName = "Discount"
            dcDis.Name = "Discount"
            dcDis.Width = 70
            dcDis.Visible = True
            dcDis.DefaultCellStyle.Format = "0"
            dcDis.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcDis.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcDis)

            Dim dc8 As New DataGridViewTextBoxColumn()
            dc8.HeaderText = "UnitPrice"
            dc8.DataPropertyName = "PurchaseRate"
            dc8.Name = "PurchaseRate"
            dc8.Width = 80
            dc8.Visible = True
            dc8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dc8.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc8)

            Dim dc9 As New DataGridViewTextBoxColumn()
            dc9.HeaderText = "Amount"
            dc9.DataPropertyName = "Amount"
            dc9.Name = "Amount"
            dc9.Visible = True
            dc9.Width = 80
            dc9.ReadOnly = True
            dc9.DefaultCellStyle.Format = "###,##0.##"
            dc9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dc9.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc9)

            Dim dc10 As New DataGridViewTextBoxColumn()
            dc10.HeaderText = "GemTW"
            dc10.DataPropertyName = "GemTW"
            dc10.Name = "GemTW"
            dc10.Visible = False
            dc10.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc10)

            Dim dc11 As New DataGridViewTextBoxColumn()
            dc11.HeaderText = "GemsTK"
            dc11.DataPropertyName = "GemsTK"
            dc11.Name = "GemsTK"
            dc11.Visible = False
            dc11.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc11)

            Dim dc13 As New DataGridViewTextBoxColumn()
            dc13.HeaderText = "GemsTG"
            dc13.DataPropertyName = "GemsTG"
            dc13.Name = "GemsTG"
            dc13.Visible = False
            dc13.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc13)
        End With
    End Sub

    Public Sub InsertItem(ByVal _PurchaseDetailID As String, ByVal _dtPurGem As DataTable)
        Dim drItem As DataRow
        If chkOnlyGem.Checked = False And chkDiamond.Checked = False Then
            drItem = _dtDetail.NewRow
            drItem.Item("PurchaseDetailID") = _PurchaseDetailID
            drItem.Item("PurchaseHeaderID") = _PurchaseHeaderID
            drItem.Item("SaleInvoiceDetailID") = _SaleInvoiceDetailID
            drItem.Item("ConsignmentSaleItemID") = _ConsignmentSaleItemID
            drItem.Item("SaleLooseDiamondDetailID") = ""
            drItem.Item("SaleGemsItemID") = ""
            drItem.Item("ForSaleID") = _ForSaleID
            drItem.Item("BarcodeNo") = IIf(IsDBNull(txtItemCode.Text), "-", txtItemCode.Text)
            drItem.Item("OldSaleAmount") = IIf(IsDBNull(txtSaleAmount.Text), "0", CInt(txtSaleAmount.Text))
            drItem.Item("ItemCategoryID") = cboItemCategory.SelectedValue
            drItem.Item("ItemCategory") = cboItemCategory.Text
            drItem.Item("ItemNameID") = cboItemName.SelectedValue
            drItem.Item("ItemName") = cboItemName.Text
            drItem.Item("CurrentPrice") = IIf(txtCurPrice.Text = "", 0, txtCurPrice.Text)
            drItem.Item("SaleRate") = _CurrentSaleRate
            drItem.Item("Length") = IIf(txtWidthLength.Text = "", "-", txtWidthLength.Text)
            drItem.Item("QTY") = txtQty.Text
            drItem.Item("GoldQualityID") = cboGoldQuality.SelectedValue
            drItem.Item("GoldQuality") = cboGoldQuality.Text
            drItem.Item("GemsCategory") = ""
            drItem.Item("GemsName") = ""
            drItem.Item("IsDamage") = chkIsDamage.Checked
            drItem.Item("IsChange") = chkIsExchange.Checked
            drItem.Item("TotalTK") = _TotalTK
            drItem.Item("TotalTG") = _TotalTG
            drItem.Item("GoldTK") = _GoldTK
            drItem.Item("GoldTG") = _GoldTG
            drItem.Item("TotalGemTK") = _TotalGemTK
            drItem.Item("TotalGemTG") = _TotalGemTG
            drItem.Item("WasteTK") = _WasteTK
            drItem.Item("WasteTG") = _WasteTG
            drItem.Item("PWasteTK") = _PWasteTK
            drItem.Item("PWasteTG") = _PWasteTG
            drItem.Item("TotalAmount") = IIf(txtTotalAmt.Text = "", 0, CInt(txtTotalAmt.Text))
            drItem.Item("YOrCOrG") = "-"
            drItem.Item("FixType") = "-"
            drItem.Item("GemTW") = "0"
            'drItem.Item("IsClose") = chkIsClose.Checked
            drItem.Item("GoldPrice") = IIf(txtGoldPrice.Text = "", 0, CInt(txtGoldPrice.Text))
            drItem.Item("GemsPrice") = IIf(txtGemsPrice.Text = "", 0, CInt(txtGemsPrice.Text))
            drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
            drItem.Item("IsDone") = chkDone.Checked
            drItem.Item("DoneAmount") = IIf(txtDoneAmount.Text = "", 0, CInt(txtDoneAmount.Text))
            drItem.Item("IsSalePercent") = chkPurchasePercent.Checked
            drItem.Item("SalePercent") = IIf(txtPurchasePercent.Text = "", 0, txtPurchasePercent.Text)
            drItem.Item("SalePercentAmount") = IIf(txtPurchcasePercentAmount.Text = "", 0, CInt(txtPurchcasePercentAmount.Text))
            drItem.Item("AddSub") = IIf(txtItemAddOrSub.Text = "", 0, CInt(txtItemAddOrSub.Text))
            drItem.Item("NetAmount") = IIf(txtNetAmount.Text = "", 0, CInt(txtNetAmount.Text))
            drItem.Item("IsShop") = chkShopGold.Checked
            drItem.Item("IsOrder") = _IsOrder
            drItem.Item("Photo") = Photo
            drItem.Item("IsDiamond") = chkIsDiamond.Checked
            drItem.Item("OriginalCode") = IIf(IsDBNull(lblOriginalCode.Text), "", lblOriginalCode.Text)
            drItem.Item("PGemsCategoryID") = ""
            drItem.Item("PGemsName") = ""
            drItem.Item("Color") = ""
            drItem.Item("Shape") = ""
            drItem.Item("Clarity") = ""

            _dtDetail.Rows.Add(drItem)
            grdDetail.DataSource = _dtDetail

            Dim i As Integer = 1
            For Each drNo As DataRow In _dtDetail.Rows
                If drNo.RowState <> DataRowState.Deleted Then
                    drNo.Item("SNo") = i
                    i = i + 1
                End If
            Next

            Dim drDetailStone As DataRow
            For Each drGems As DataRow In _dtPurGem.Rows
                If drGems.RowState <> DataRowState.Deleted Then
                    drDetailStone = _dtStone.NewRow()
                    drDetailStone("PurchaseGemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseGem, CommonInfo.EnumSetting.GenerateKeyType.PurchaseGem.ToString, dtpPurchaseDate.Value)
                    drDetailStone("PurchaseDetailID") = _PurchaseDetailID
                    drDetailStone("GemsCategoryID") = IIf(IsDBNull(drGems("GemsCategoryID")), "", drGems("GemsCategoryID"))
                    drDetailStone("GemsName") = IIf(IsDBNull(drGems("GemsName")), "-", drGems("GemsName"))
                    '
                    drDetailStone("GemsK") = drGems("GemsK")
                    drDetailStone("GemsP") = drGems("GemsP")
                    drDetailStone("GemsY") = drGems("GemsY")
                    '
                    drDetailStone("GemsTK") = drGems("GemsTK")
                    drDetailStone("GemsTG") = drGems("GemsTG")
                    drDetailStone("YOrCOrG") = IIf(IsDBNull(drGems("YOrCOrG")), "0", drGems("YOrCOrG"))
                    drDetailStone("GemTW") = drGems("GemTW")
                    drDetailStone("QTY") = IIf(IsDBNull(drGems("QTY")), 0, drGems("QTY"))
                    drDetailStone("FixType") = IIf(IsDBNull(drGems("FixType")), "", drGems("FixType"))
                    drDetailStone("Discount") = IIf(IsDBNull(drGems("Discount")), 0, drGems("Discount"))
                    drDetailStone("PurchaseRate") = IIf(IsDBNull(drGems("PurchaseRate")), 0, drGems("PurchaseRate"))
                    drDetailStone("Amount") = IIf(IsDBNull(drGems("Amount")), 0, drGems("Amount"))
                    _dtStone.Rows.Add(drDetailStone)
                End If
            Next
        ElseIf chkDiamond.Checked Then
            drItem = _dtDetail.NewRow
            drItem.Item("PurchaseDetailID") = _PurchaseDetailID
            drItem.Item("PurchaseHeaderID") = _PurchaseHeaderID
            drItem.Item("SaleInvoiceDetailID") = ""
            drItem.Item("SaleGemsItemID") = ""
            drItem.Item("ConsignmentSaleItemID") = ""
            drItem.Item("SaleLooseDiamondDetailID") = _SaleLooseDiamondDetailID
            drItem.Item("ForSaleID") = _ForSaleID
            drItem.Item("BarcodeNo") = IIf(txtDItemCode.Text = "", "-", txtDItemCode.Text)
            drItem.Item("OldSaleAmount") = IIf(IsDBNull(txtDSaleAmount.Text), "0", CInt(txtDSaleAmount.Text))
            drItem.Item("PGemsCategoryID") = cboDCategory.SelectedValue
            drItem.Item("GemsCategory") = cboDCategory.Text
            drItem.Item("PGemsName") = IIf(txtDescription.Text = "", "-", txtDescription.Text)
            drItem.Item("Color") = IIf(cboDColor.SelectedValue = "", "", cboDColor.SelectedValue)
            drItem.Item("Shape") = IIf(cboDShape.SelectedValue = "", "-", cboDShape.SelectedValue)
            drItem.Item("Clarity") = IIf(cboDClarity.SelectedValue = "", "-", cboDClarity.SelectedValue)
            drItem.Item("CurrentPrice") = IIf(txtDCurPrice.Text = "", 0, txtDCurPrice.Text)
            drItem.Item("SaleRate") = _DCurrentSaleRate
            drItem.Item("Length") = ""
            drItem.Item("QTY") = txtDQty.Text
            drItem.Item("GoldQualityID") = "00"
            drItem.Item("GoldQuality") = "_"
            drItem.Item("IsDamage") = chkIsDamage.Checked
            drItem.Item("IsChange") = chkDIsChange.Checked
            drItem.Item("TotalTK") = 0.0
            drItem.Item("TotalTG") = 0.0
            drItem.Item("GoldTK") = 0.0
            drItem.Item("GoldTG") = 0.0
            drItem.Item("TotalGemTK") = _ItemTK
            drItem.Item("TotalGemTG") = _ItemTG
            drItem.Item("WasteTK") = 0.0
            drItem.Item("WasteTG") = 0.0
            drItem.Item("PWasteTK") = 0.0
            drItem.Item("PWasteTG") = 0.0
            drItem.Item("TotalAmount") = IIf(txtDTotalAmount.Text = "", 0, CInt(txtDTotalAmount.Text))
            drItem.Item("YOrCOrG") = IIf(txtDRBP.Text = "0", "", txtDRBP.Text)
            drItem.Item("FixType") = "-"
            drItem.Item("GemTW") = _GemsTW
            drItem.Item("GoldPrice") = 0
            drItem.Item("GemsPrice") = IIf(txtDiamondPrice.Text = "", 0, CInt(txtDiamondPrice.Text))
            drItem.Item("IsFixPrice") = chkDIsFixPrice.Checked
            drItem.Item("IsDone") = chkDIsDone.Checked
            drItem.Item("DoneAmount") = IIf(txtDDonePrice.Text = "", 0, CInt(txtDDonePrice.Text))
            drItem.Item("IsSalePercent") = chkDPurchasePercent.Checked
            drItem.Item("SalePercent") = IIf(txtDPurchasePercent.Text = "", 0, txtDPurchasePercent.Text)
            drItem.Item("SalePercentAmount") = IIf(txtDPercentPrice.Text = "", 0, CInt(txtDPercentPrice.Text))
            drItem.Item("AddSub") = IIf(txtDAddOrSub.Text = "", 0, CInt(txtDAddOrSub.Text))
            drItem.Item("NetAmount") = IIf(txtDNetAmount.Text = "", 0, CInt(txtDNetAmount.Text))
            drItem.Item("IsShop") = chkShopDiamond.Checked
            drItem.Item("IsOrder") = False
            drItem.Item("Photo") = Photo
            drItem.Item("OriginalCode") = IIf(IsDBNull(lblDOriginalCode.Text), "", lblDOriginalCode.Text)
            drItem.Item("IsDiamond") = chkIsDiamond.Checked

            _dtDetail.Rows.Add(drItem)
            grdDetail.DataSource = _dtDetail

            Dim i As Integer = 1
            For Each drNo As DataRow In _dtDetail.Rows
                If drNo.RowState <> DataRowState.Deleted Then
                    drNo.Item("SNo") = i
                    i = i + 1
                End If
            Next

            ' grdDetail.DataSource = _dtDetail
            
        Else
            drItem = _dtDetail.NewRow
            drItem.Item("PurchaseDetailID") = _PurchaseDetailID
            drItem.Item("PurchaseHeaderID") = _PurchaseHeaderID
            drItem.Item("SaleInvoiceDetailID") = ""
            drItem.Item("ConsignmentSaleItemID") = _ConsignmentSaleItemID
            drItem.Item("SaleGemsItemID") = _SaleGemsItemID
            drItem.Item("ForSaleID") = ""
            drItem.Item("BarcodeNo") = ""
            drItem.Item("OldSaleAmount") = "0"
            drItem.Item("ItemCategoryID") = cboGemCategory.SelectedValue
            drItem.Item("GemsCategory") = cboGemCategory.Text
            drItem.Item("ItemNameID") = ""
            drItem.Item("GemsName") = IIf(txtGemName.Text = "", "-", txtGemName.Text)
            drItem.Item("CurrentPrice") = IIf(txtUnitPrice.Text = "", 0, txtUnitPrice.Text)
            drItem.Item("SaleRate") = "0"
            drItem.Item("Length") = "_"
            drItem.Item("QTY") = txtGemQTY.Text
            drItem.Item("GoldQualityID") = ""
            drItem.Item("GoldQuality") = ""
            drItem.Item("ItemCategory") = ""
            drItem.Item("ItemName") = ""
            drItem.Item("IsDamage") = False
            drItem.Item("IsChange") = False
            drItem.Item("TotalTK") = 0.0
            drItem.Item("TotalTG") = 0.0
            drItem.Item("GoldTK") = 0.0
            drItem.Item("GoldTG") = 0.0
            drItem.Item("TotalGemTK") = GemTK
            drItem.Item("TotalGemTG") = GemTG
            drItem.Item("WasteTK") = 0.0
            drItem.Item("WasteTG") = 0.0
            drItem.Item("PWasteTK") = 0.0
            drItem.Item("PWasteTG") = 0.0
            drItem.Item("TotalAmount") = IIf(IsDBNull(txtTotalAmount.Text), "0", txtTotalAmount.Text)
            drItem.Item("YOrCOrG") = IIf(txtRBP.Text = "", "0", txtRBP.Text)
            drItem.Item("FixType") = cboFixType.Text
            drItem.Item("GemTW") = GemTW
            'drItem.Item("IsClose") = False
            drItem.Item("GoldPrice") = "0"
            drItem.Item("GemsPrice") = "0"
            drItem.Item("IsFixPrice") = False
            drItem.Item("IsDone") = False
            drItem.Item("DoneAmount") = 0
            drItem.Item("IsSalePercent") = False
            drItem.Item("SalePercent") = 0
            drItem.Item("SalePercentAmount") = 0
            drItem.Item("AddSub") = IIf(IsDBNull(txtGemAddSub.Text), "0", txtGemAddSub.Text)
            drItem.Item("NetAmount") = IIf(IsDBNull(txtGemNetAmount.Text), "0", txtGemNetAmount.Text)
            drItem.Item("IsShop") = False
            drItem.Item("IsOrder") = False
            drItem.Item("Photo") = ""
            drItem.Item("SaleLooseDiamondDetailID") = ""
            drItem.Item("PGemsCategoryID") = ""
            drItem.Item("PGemsName") = ""
            drItem.Item("Color") = ""
            drItem.Item("Shape") = ""
            drItem.Item("Clarity") = ""
            Dim i As Integer = 1
            For Each drNo As DataRow In _dtDetail.Rows
                If drNo.RowState <> DataRowState.Deleted Then
                    drNo.Item("SNo") = i
                    i = i + 1
                End If
            Next

            grdDetail.DataSource = _dtDetail
            _dtDetail.Rows.Add(drItem)
            grdDetail.DataSource = _dtDetail
            End If

    End Sub

    Public Sub UpdateItem(ByVal _PDiaItemDetailID As String, ByVal _dtPurGem As DataTable)
        Dim drItem As DataRow
        drItem = _dtDetail.Rows(grdDetail.CurrentRow.Index)

        If chkOnlyGem.Checked = False And chkDiamond.Checked = False Then
            If Not IsNothing(drItem) Then
                drItem.Item("PurchaseDetailID") = _PurchaseDetailID
                drItem.Item("PurchaseHeaderID") = _PurchaseHeaderID
                drItem.Item("SaleInvoiceDetailID") = _SaleInvoiceDetailID
                drItem.Item("ConsignmentSaleItemID") = _ConsignmentSaleItemID
                drItem.Item("SaleGemsItemID") = ""
                drItem.Item("ForSaleID") = _ForSaleID
                drItem.Item("BarcodeNo") = IIf(IsDBNull(txtItemCode.Text), "-", txtItemCode.Text)
                drItem.Item("OldSaleAmount") = IIf(IsDBNull(txtSaleAmount.Text), "0", CInt(txtSaleAmount.Text))
                drItem.Item("ItemCategoryID") = cboItemCategory.SelectedValue
                drItem.Item("ItemCategory") = cboItemCategory.Text
                drItem.Item("ItemNameID") = cboItemName.SelectedValue
                drItem.Item("ItemName") = cboItemName.Text
                drItem.Item("CurrentPrice") = IIf(txtCurPrice.Text = "", 0, txtCurPrice.Text)
                drItem.Item("SaleRate") = _CurrentSaleRate
                drItem.Item("Length") = IIf(txtWidthLength.Text = "", "-", txtWidthLength.Text)
                drItem.Item("QTY") = txtQty.Text
                drItem.Item("GoldQualityID") = cboGoldQuality.SelectedValue
                drItem.Item("GoldQuality") = cboGoldQuality.Text
                drItem.Item("GemsCategory") = ""
                drItem.Item("GemsName") = ""
                drItem.Item("IsDamage") = chkIsDamage.Checked
                drItem.Item("IsChange") = chkIsExchange.Checked
                drItem.Item("TotalTK") = _TotalTK
                drItem.Item("TotalTG") = _TotalTG
                drItem.Item("GoldTK") = _GoldTK
                drItem.Item("GoldTG") = _GoldTG
                drItem.Item("TotalGemTK") = _TotalGemTK
                drItem.Item("TotalGemTG") = _TotalGemTG
                drItem.Item("WasteTK") = _WasteTK
                drItem.Item("WasteTG") = _WasteTG
                drItem.Item("PWasteTK") = _PWasteTK
                drItem.Item("PWasteTG") = _PWasteTG
                drItem.Item("TotalAmount") = IIf(txtTotalAmt.Text = "", 0, CInt(txtTotalAmt.Text))
                drItem.Item("YOrCOrG") = "-"
                drItem.Item("FixType") = "-"
                drItem.Item("GemTW") = "0"
                'drItem.Item("IsClose") = chkIsClose.Checked
                drItem.Item("GoldPrice") = IIf(txtGoldPrice.Text = "", 0, CInt(txtGoldPrice.Text))
                drItem.Item("GemsPrice") = IIf(txtGemsPrice.Text = "", 0, CInt(txtGemsPrice.Text))
                drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
                drItem.Item("IsDone") = chkDone.Checked
                drItem.Item("DoneAmount") = IIf(txtDoneAmount.Text = "", 0, CInt(txtDoneAmount.Text))
                drItem.Item("IsSalePercent") = chkPurchasePercent.Checked
                drItem.Item("SalePercent") = IIf(txtPurchasePercent.Text = "", 0, txtPurchasePercent.Text)
                drItem.Item("SalePercentAmount") = IIf(txtPurchcasePercentAmount.Text = "", 0, CInt(txtPurchcasePercentAmount.Text))
                drItem.Item("AddSub") = IIf(txtItemAddOrSub.Text = "", 0, CInt(txtItemAddOrSub.Text))
                drItem.Item("NetAmount") = IIf(txtNetAmount.Text = "", 0, CInt(txtNetAmount.Text))
                drItem.Item("IsShop") = chkShopGold.Checked
                drItem.Item("IsOrder") = _IsOrder
                drItem.Item("Photo") = Photo
                drItem.Item("OriginalCode") = IIf(IsDBNull(lblOriginalCode.Text), "", lblOriginalCode.Text)
                drItem.Item("IsDiamond") = chkIsDiamond.Checked
                drItem.Item("SaleLooseDiamondDetailID") = ""
                drItem.Item("PGemsCategoryID") = ""
                drItem.Item("PGemsName") = ""
                drItem.Item("Color") = ""
                drItem.Item("Shape") = ""
                drItem.Item("Clarity") = ""
                grdDetail.DataSource = _dtDetail
                _dtPurGem = grdGem.DataSource

                Dim n As Integer = 1
                For Each drNo As DataRow In _dtDetail.Rows
                    If drNo.RowState <> DataRowState.Deleted Then
                        drNo.Item("SNo") = n
                        n = n + 1
                    End If
                Next

                Dim j As Integer = 0
                If _dtPurGem.Rows.Count > 0 Then  '   if Gems Update , check dtstone. if dtstone has Gems, delete gemsid .
                    For i As Integer = 0 To _dtPurGem.Rows.Count - 1
                        While j < _dtStone.Rows.Count
                            Dim row As DataRow
                            row = _dtStone.Rows(j)
                            If Not IsDBNull(_dtPurGem.Rows(i).Item("PurchaseDetailID")) Then
                                If row.Item("PurchaseDetailID") = _dtPurGem.Rows(i).Item("PurchaseDetailID") Then
                                    _IsRowDelete = True
                                Else
                                    _IsRowDelete = False
                                End If
                                If _IsRowDelete Then
                                    _dtStone.Rows.Remove(row)
                                Else
                                    j = j + 1
                                End If
                            Else
                                j = j + 1
                            End If
                        End While
                    Next
                Else   ' dtPDiaItemgems no row , but dtstone has another gems id.It gemsid is deleted
                    If _dtStone.Rows.Count > 0 Then
                        While j < _dtStone.Rows.Count
                            Dim row As DataRow
                            row = _dtStone.Rows(j)
                            If row.Item("PurchaseDetailID") = _PurchaseDetailID Then
                                _dtStone.Rows.Remove(row)
                            Else
                                j = j + 1
                            End If
                        End While

                    End If
                End If

                Dim drDetailStone As DataRow
                If _dtStone.Rows.Count <> 0 Then
                    For i As Integer = 0 To _dtStone.Rows.Count - 1
                        If _dtStone.Rows(i).Item("PurchaseDetailID") = _PurchaseDetailID Then
                            For Each drvDetailStone As DataRow In _dtPurGem.Rows
                                If Not IsDBNull(drvDetailStone("PurchaseDetailID")) Then
                                    If _dtStone.Rows(i).Item("PurchaseDetailID") = _PurchaseDetailID And _IsRowDelete <> True Then
                                        If _dtStone.Rows(i).Item("PurchaseGemID") = drvDetailStone("PurchaseGemID") Then
                                            drvDetailStone.BeginEdit()
                                            _dtStone.Rows(i).Item("PurchaseDetailID") = _PurchaseDetailID
                                            _dtStone.Rows(i).Item("GemsCategoryID") = IIf(IsDBNull(drvDetailStone("GemsCategoryID")), "", drvDetailStone("GemsCategoryID"))
                                            _dtStone.Rows(i).Item("GemsName") = IIf(IsDBNull(drvDetailStone("GemsName")), "-", drvDetailStone("GemsName"))
                                            _dtStone.Rows(i).Item("GemsTK") = drvDetailStone("GemsTK")
                                            _dtStone.Rows(i).Item("GemsTG") = drvDetailStone("GemsTG")
                                            _dtStone.Rows(i).Item("YOrCOrG") = IIf(IsDBNull(drvDetailStone("YOrCOrG")), "0", drvDetailStone("YOrCOrG"))                                            '
                                            _dtStone.Rows(i).Item("GemsK") = drvDetailStone("GemsK")
                                            _dtStone.Rows(i).Item("GemsP") = drvDetailStone("GemsP")
                                            _dtStone.Rows(i).Item("GemsY") = drvDetailStone("GemsY")                                            '
                                            _dtStone.Rows(i).Item("GemTW") = drvDetailStone("GemTW")
                                            _dtStone.Rows(i).Item("QTY") = IIf(IsDBNull(drvDetailStone("QTY")), 0, drvDetailStone("QTY"))
                                            _dtStone.Rows(i).Item("FixType") = IIf(IsDBNull(drvDetailStone("FixType")), "", drvDetailStone("FixType"))
                                            _dtStone.Rows(i).Item("Discount") = IIf(IsDBNull(drvDetailStone("Discount")), 0, drvDetailStone("Discount"))
                                            _dtStone.Rows(i).Item("PurchaseRate") = IIf(IsDBNull(drvDetailStone("PurchaseRate")), 0, drvDetailStone("PurchaseRate"))
                                            _dtStone.Rows(i).Item("Amount") = IIf(IsDBNull(drvDetailStone("Amount")), 0, drvDetailStone("Amount"))
                                            drvDetailStone.EndEdit()
                                            i += 1
                                        End If
                                    End If
                                Else
                                    drDetailStone = _dtStone.NewRow()
                                    If IsDBNull(drDetailStone("PurchaseGemID")) Then
                                        drDetailStone("PurchaseGemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseGem, CommonInfo.EnumSetting.GenerateKeyType.PurchaseGem.ToString, dtpPurchaseDate.Value)
                                    Else
                                        drDetailStone("PurchaseGemID") = drvDetailStone("PurchaseGemID")
                                    End If
                                    drDetailStone("PurchaseDetailID") = _PurchaseDetailID
                                    drDetailStone("GemsCategoryID") = IIf(IsDBNull(drvDetailStone("GemsCategoryID")), "", drvDetailStone("GemsName"))
                                    drDetailStone("GemsName") = IIf(IsDBNull(drvDetailStone("GemsName")), "-", drvDetailStone("GemsName"))
                                    drDetailStone("GemsTK") = drvDetailStone("GemsTK")
                                    drDetailStone("GemsTG") = drvDetailStone("GemsTG")
                                    drDetailStone("YOrCOrG") = IIf(IsDBNull(drvDetailStone("YOrCOrG")), "0", drvDetailStone("YOrCOrG"))                                    '
                                    drDetailStone("GemsK") = drvDetailStone("GemsK")
                                    drDetailStone("GemsP") = drvDetailStone("GemsP")
                                    drDetailStone("GemsY") = drvDetailStone("GemsY")                                    '
                                    drDetailStone("GemTW") = drvDetailStone("GemTW")
                                    drDetailStone("QTY") = IIf(IsDBNull(drvDetailStone("QTY")), 0, drvDetailStone("QTY"))
                                    drDetailStone("FixType") = IIf(IsDBNull(drvDetailStone("FixType")), "", drvDetailStone("FixType"))
                                    drDetailStone("Discount") = IIf(IsDBNull(drvDetailStone("Discount")), 0, drvDetailStone("Discount"))
                                    drDetailStone("PurchaseRate") = IIf(IsDBNull(drvDetailStone("PurchaseRate")), 0, drvDetailStone("PurchaseRate"))
                                    drDetailStone("Amount") = IIf(IsDBNull(drvDetailStone("Amount")), 0, drvDetailStone("Amount"))
                                    _dtStone.Rows.Add(drDetailStone)
                                    i += 1
                                End If


                            Next
                            _dtPurGem.DefaultView.RowFilter = ""
                            _dtPurGem.DefaultView.Sort = "PurchaseDetailID"

                        End If
                    Next


                Else '''''' if _dtStone.Row.Count=0
                    For Each drGems As DataRow In _dtPurGem.Rows
                        drDetailStone = _dtStone.NewRow()

                        If IsDBNull(drDetailStone("PurchaseGemID")) Then
                            drDetailStone("PurchaseGemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseGem, CommonInfo.EnumSetting.GenerateKeyType.PurchaseGem.ToString, dtpPurchaseDate.Value)
                        Else
                            drDetailStone("PurchaseGemID") = drGems("PurchaseGemID")
                        End If
                        drDetailStone("PurchaseDetailID") = _PurchaseDetailID
                        drDetailStone("GemsCategoryID") = IIf(IsDBNull(drGems("GemsCategoryID")), "", drGems("GemsCategoryID"))
                        drDetailStone("GemsName") = IIf(IsDBNull(drGems("GemsName")), "-", drGems("GemsName"))
                        drDetailStone("GemsTK") = drGems("GemsTK")
                        drDetailStone("GemsTG") = drGems("GemsTG")
                        drDetailStone("YOrCOrG") = IIf(IsDBNull(drGems("YOrCOrG")), "0", drGems("YOrCOrG"))                        '
                        drDetailStone("GemsK") = drGems("GemsK")
                        drDetailStone("GemsP") = drGems("GemsP")
                        drDetailStone("GemsY") = drGems("GemsY")                        '
                        drDetailStone("GemTW") = drGems("GemTW")
                        drDetailStone("QTY") = IIf(IsDBNull(drGems("QTY")), 0, drGems("QTY"))
                        drDetailStone("FixType") = IIf(IsDBNull(drGems("FixType")), "", drGems("FixType"))
                        drDetailStone("Discount") = IIf(IsDBNull(drGems("Discount")), 0, drGems("Discount"))
                        drDetailStone("PurchaseRate") = IIf(IsDBNull(drGems("PurchaseRate")), 0, drGems("PurchaseRate"))
                        drDetailStone("Amount") = IIf(IsDBNull(drGems("Amount")), 0, drGems("Amount"))

                        _dtStone.Rows.Add(drDetailStone)

                    Next

                End If



                Dim drFind As Boolean = False

                If _dtStone.Rows.Count <> 0 Then
                    For i As Integer = 0 To _dtStone.Rows.Count - 1
                        If _dtStone.Rows(i).Item("PurchaseDetailID") = _PurchaseDetailID Then
                            drFind = True
                        Else
                            drFind = False
                        End If

                    Next
                    If drFind = False Then
                        For Each drGems As DataRow In _dtPurGem.Rows
                            drDetailStone = _dtStone.NewRow()
                            If IsDBNull(drDetailStone("PurchaseGemID")) Then
                                drDetailStone("PurchaseGemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseGem, CommonInfo.EnumSetting.GenerateKeyType.PurchaseGem.ToString, dtpPurchaseDate.Value)
                            Else
                                drDetailStone("PurchaseGemID") = drGems("PurchaseGemID")
                            End If
                            drDetailStone("PurchaseDetailID") = _PurchaseDetailID
                            drDetailStone("GemsCategoryID") = IIf(IsDBNull(drGems("GemsCategoryID")), "", drGems("GemsCategoryID"))
                            drDetailStone("GemsName") = IIf(IsDBNull(drGems("GemsName")), "-", drGems("GemsName"))
                            drDetailStone("GemsTK") = drGems("GemsTK")
                            drDetailStone("GemsTG") = drGems("GemsTG")
                            drDetailStone("YOrCOrG") = IIf(IsDBNull(drGems("YOrCOrG")), "0", drGems("YOrCOrG"))                            '
                            drDetailStone("GemsK") = drGems("GemsK")
                            drDetailStone("GemsP") = drGems("GemsP")
                            drDetailStone("GemsY") = drGems("GemsY")                            '
                            drDetailStone("GemTW") = drGems("GemTW")
                            drDetailStone("QTY") = IIf(IsDBNull(drGems("QTY")), 0, drGems("QTY"))
                            drDetailStone("FixType") = IIf(IsDBNull(drGems("FixType")), "", drGems("FixType"))
                            drDetailStone("Discount") = IIf(IsDBNull(drGems("Discount")), 0, drGems("Discount"))
                            drDetailStone("PurchaseRate") = IIf(IsDBNull(drGems("PurchaseRate")), 0, drGems("PurchaseRate"))
                            drDetailStone("Amount") = IIf(IsDBNull(drGems("Amount")), 0, drGems("Amount"))

                            _dtStone.Rows.Add(drDetailStone)

                        Next
                    End If
                End If

            End If
        ElseIf chkDiamond.Checked Then
            If Not IsNothing(drItem) Then
                drItem.Item("PurchaseDetailID") = _PurchaseDetailID
                drItem.Item("PurchaseHeaderID") = _PurchaseHeaderID
                drItem.Item("SaleInvoiceDetailID") = ""
                drItem.Item("SaleGemsItemID") = ""
                drItem.Item("ConsignmentSaleItemID") = ""
                drItem.Item("SaleLooseDiamondDetailID") = _SaleLooseDiamondDetailID
                drItem.Item("ForSaleID") = _ForSaleID
                drItem.Item("BarcodeNo") = IIf(txtDItemCode.Text = "", "-", txtDItemCode.Text)
                drItem.Item("OldSaleAmount") = IIf(IsDBNull(txtDSaleAmount.Text), "0", CInt(txtDSaleAmount.Text))
                drItem.Item("PGemsCategoryID") = cboDCategory.SelectedValue
                drItem.Item("GemsCategory") = cboDCategory.Text
                drItem.Item("PGemsName") = IIf(txtDescription.Text = "", "-", txtDescription.Text)
                drItem.Item("Color") = IIf(cboDColor.SelectedValue = "", "", cboDColor.SelectedValue)
                drItem.Item("Shape") = IIf(cboDShape.SelectedValue = "", "-", cboDShape.SelectedValue)
                drItem.Item("Clarity") = IIf(cboDClarity.SelectedValue = "", "-", cboDClarity.SelectedValue)
                drItem.Item("CurrentPrice") = IIf(txtDCurPrice.Text = "", 0, txtDCurPrice.Text)
                drItem.Item("SaleRate") = _DCurrentSaleRate
                drItem.Item("Length") = ""
                drItem.Item("QTY") = txtDQty.Text
                drItem.Item("GoldQualityID") = "00"
                drItem.Item("GoldQuality") = "_"
                drItem.Item("IsDamage") = chkIsDamage.Checked
                drItem.Item("IsChange") = chkDIsChange.Checked
                drItem.Item("TotalTK") = 0.0
                drItem.Item("TotalTG") = 0.0
                drItem.Item("GoldTK") = 0.0
                drItem.Item("GoldTG") = 0.0
                drItem.Item("TotalGemTK") = _ItemTK
                drItem.Item("TotalGemTG") = _ItemTG
                drItem.Item("WasteTK") = 0.0
                drItem.Item("WasteTG") = 0.0
                drItem.Item("PWasteTK") = 0.0
                drItem.Item("PWasteTG") = 0.0
                drItem.Item("TotalAmount") = IIf(txtDTotalAmount.Text = "", 0, CInt(txtDTotalAmount.Text))
                drItem.Item("YOrCOrG") = IIf(txtDRBP.Text = "0", "", txtDRBP.Text)
                drItem.Item("FixType") = "-"
                drItem.Item("GemTW") = _GemsTW
                drItem.Item("GoldPrice") = 0
                drItem.Item("GemsPrice") = IIf(txtDiamondPrice.Text = "", 0, CInt(txtDiamondPrice.Text))
                drItem.Item("IsFixPrice") = chkDIsFixPrice.Checked
                drItem.Item("IsDone") = chkDIsDone.Checked
                drItem.Item("DoneAmount") = IIf(txtDDonePrice.Text = "", 0, CInt(txtDDonePrice.Text))
                drItem.Item("IsSalePercent") = chkDPurchasePercent.Checked
                drItem.Item("SalePercent") = IIf(txtDPurchasePercent.Text = "", 0, txtDPurchasePercent.Text)
                drItem.Item("SalePercentAmount") = IIf(txtDPercentPrice.Text = "", 0, CInt(txtDPercentPrice.Text))
                drItem.Item("AddSub") = IIf(txtDAddOrSub.Text = "", 0, CInt(txtDAddOrSub.Text))
                drItem.Item("NetAmount") = IIf(txtDNetAmount.Text = "", 0, CInt(txtDNetAmount.Text))
                drItem.Item("IsShop") = chkShopDiamond.Checked
                drItem.Item("IsOrder") = False
                drItem.Item("Photo") = Photo
                drItem.Item("OriginalCode") = IIf(IsDBNull(lblDOriginalCode.Text), "", lblDOriginalCode.Text)
                drItem.Item("IsDiamond") = chkIsDiamond.Checked

                
                Dim i As Integer = 1
                For Each drNo As DataRow In _dtDetail.Rows
                    If drNo.RowState <> DataRowState.Deleted Then
                        drNo.Item("SNo") = i
                        i = i + 1
                    End If
                Next

                grdDetail.DataSource = _dtDetail
            End If
        Else
            If Not IsNothing(drItem) Then
                drItem.Item("PurchaseDetailID") = _PurchaseDetailID
                drItem.Item("PurchaseHeaderID") = _PurchaseHeaderID
                drItem.Item("SaleInvoiceDetailID") = ""
                drItem.Item("SaleGemsItemID") = _SaleGemsItemID
                drItem.Item("ForSaleID") = ""
                drItem.Item("BarcodeNo") = ""
                drItem.Item("OldSaleAmount") = "0"
                drItem.Item("ItemCategoryID") = cboGemCategory.SelectedValue
                drItem.Item("GemsCategory") = cboGemCategory.Text
                drItem.Item("ItemNameID") = "00"
                drItem.Item("GemsName") = IIf(txtGemName.Text = "", "-", txtGemName.Text)
                drItem.Item("CurrentPrice") = IIf(txtUnitPrice.Text = "", 0, txtUnitPrice.Text)
                drItem.Item("SaleRate") = "0"
                drItem.Item("Length") = "_"
                drItem.Item("QTY") = txtGemQTY.Text
                drItem.Item("GoldQualityID") = "00"
                drItem.Item("GoldQuality") = "_"
                drItem.Item("ItemCategory") = ""
                drItem.Item("ItemName") = ""
                drItem.Item("IsDamage") = False
                drItem.Item("IsChange") = False
                drItem.Item("TotalTK") = 0.0
                drItem.Item("TotalTG") = 0.0
                drItem.Item("GoldTK") = 0.0
                drItem.Item("GoldTG") = 0.0
                drItem.Item("TotalGemTK") = GemTK
                drItem.Item("TotalGemTG") = GemTG
                drItem.Item("WasteTK") = 0.0
                drItem.Item("WasteTG") = 0.0
                drItem.Item("PWasteTK") = 0.0
                drItem.Item("PWasteTG") = 0.0
                drItem.Item("TotalAmount") = IIf(IsDBNull(txtTotalAmount.Text), "0", txtTotalAmount.Text)
                drItem.Item("YOrCOrG") = IIf(txtRBP.Text = "", "0", txtRBP.Text)
                drItem.Item("FixType") = cboFixType.Text
                drItem.Item("GemTW") = GemTW
                drItem.Item("GoldPrice") = "0"
                drItem.Item("GemsPrice") = "0"
                drItem.Item("IsFixPrice") = False
                drItem.Item("IsDone") = False
                drItem.Item("DoneAmount") = 0
                drItem.Item("IsSalePercent") = False
                drItem.Item("SalePercent") = 0
                drItem.Item("SalePercentAmount") = 0
                drItem.Item("AddSub") = IIf(IsDBNull(txtGemAddSub.Text), "0", txtGemAddSub.Text)
                drItem.Item("NetAmount") = IIf(IsDBNull(txtGemNetAmount.Text), "0", txtGemNetAmount.Text)
                drItem.Item("IsShop") = False
                drItem.Item("IsOrder") = False
                drItem.Item("Photo") = ""
                drItem.Item("SaleLooseDiamondDetailID") = ""
                drItem.Item("PGemsCategoryID") = ""
                drItem.Item("PGemsName") = ""
                drItem.Item("Color") = ""
                drItem.Item("Shape") = ""
                drItem.Item("Clarity") = ""
                Dim i As Integer = 1
                For Each drNo As DataRow In _dtDetail.Rows
                    If drNo.RowState <> DataRowState.Deleted Then
                        drNo.Item("SNo") = i
                        i = i + 1
                    End If
                Next

                grdDetail.DataSource = _dtDetail
            End If
        End If

    End Sub



    Private Sub GetCurrentPrice()
        Dim objCurrent As New CommonInfo.CurrentPriceInfo
        objCurrent = objCurrentPriceController.GetCurrentPriceByGoldID(cboGoldQuality.SelectedValue)
        With objCurrent
            _CurrentSaleRate = .SalesRate
            txtSaleRate.Text = _CurrentSaleRate

            If chkIsDamage.Checked Then
                _IsPercentage = True
                txtCurPrice.Text = .PercentDamageRate
                lblPercent.Text = "%"
            Else

                If chkIsExchange.Checked = True Then
                    If .ExchangeRate = 0 Then
                        _IsPercentage = True
                        txtCurPrice.Text = .PercentExchangeRate
                        lblPercent.Text = "%"
                    Else
                        _IsPercentage = False
                        txtCurPrice.Text = .ExchangeRate
                        If _IsGram Then
                            lblPercent.Text = "၁ ဂရမ်စျေး"
                        Else
                            lblPercent.Text = "၁ ကျပ်သားစျေး"
                        End If
                    End If
                Else
                    If .PurchaseRate = 0 Then
                        _IsPercentage = True
                        txtCurPrice.Text = .PercentPurchaseRate
                        lblPercent.Text = "%"
                    Else
                        _IsPercentage = False
                        txtCurPrice.Text = .PurchaseRate
                        If _IsGram Then
                            lblPercent.Text = "၁ ဂရမ်စျေး"
                        Else
                            lblPercent.Text = "၁ ကျပ်သားစျေး"
                        End If
                    End If
                End If
            End If

            If Global_CurrentUser = "Administrator" Then
                txtCurPrice.ReadOnly = False
            Else
                If Global_IsAllowSaleReturn Then
                    txtCurPrice.ReadOnly = False
                Else
                    txtCurPrice.ReadOnly = True
                End If
            End If
        End With
    End Sub

    Private Sub CalculateTotalAmount()
        If txtPurchcasePercentAmount.Text = "" Then txtPurchcasePercentAmount.Text = "0"
        If txtGoldPrice.Text = "" Then txtGoldPrice.Text = "0"
        If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
        If txtDoneAmount.Text = "" Then txtDoneAmount.Text = "0"
        If txtSaleAmount.Text = "" Then txtSaleAmount.Text = "0"

        If chkDone.Checked Then
            txtTotalAmt.Text = txtDoneAmount.Text
        ElseIf chkPurchasePercent.Checked Then
            txtTotalAmt.Text = Format(Val(CLng(txtSaleAmount.Text) - CLng(txtPurchcasePercentAmount.Text)), "###,##0.##")
        Else
            txtTotalAmt.Text = Format(Val(CLng(txtGoldPrice.Text) + CLng(txtGemsPrice.Text)), "###,##0.##")
        End If
        txtItemAddOrSub.Text = "0"
        txtNetAmount.Text = Format(Val(CLng(txtTotalAmt.Text)), "###,##0.##")
    End Sub

    Private Sub CalculategrdGem()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        _GemQTY = 0
        If txtGemsG.Text = "0.0" Then
            _TotalGemTK = 0
            _TotalGemTG = 0
        End If

        txtGemsG.Text = "0.0"
        Dim GemWeight As New CommonInfo.GoldWeightInfo
        Dim TotalWeight As New CommonInfo.GoldWeightInfo
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        For i As Integer = 0 To grdGem.RowCount - 1
            If Not grdGem.Rows(i).IsNewRow Then

                _GemQTY += CInt(Val(grdGem.Rows(i).Cells("QTY").FormattedValue))
                GemWeight.WeightK = CInt(Val(grdGem.Rows(i).Cells("GemsK").FormattedValue))
                GemWeight.WeightP = CInt(Val(grdGem.Rows(i).Cells("GemsP").FormattedValue))
                GemWeight.WeightY = CDec(Val(grdGem.Rows(i).Cells("GemsY").FormattedValue))

                weightY += GemWeight.WeightY
                If weightY >= Global_PToY Then
                    weightP += 1
                    weightY = weightY - Global_PToY
                End If

                weightP += GemWeight.WeightP
                If weightP >= 16 Then
                    weightK += 1
                    weightP = weightP - 16
                End If

                weightK += GemWeight.WeightK
            End If
        Next

        TotalWeight.WeightY = weightY
        TotalWeight.WeightP = weightP
        TotalWeight.WeightK = weightK

        txtGemsK.Text = Format(TotalWeight.WeightK, "0")
        txtGemsP.Text = Format(TotalWeight.WeightP, "0")
        txtGemsY.Text = Format(TotalWeight.WeightY, "0.0")

        TotalWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(TotalWeight)
        _TotalGemTK = TotalWeight.GoldTK
        TotalWeight.Gram = TotalWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
        _TotalGemTG = TotalWeight.Gram
        txtGemsG.Text = Format(_TotalGemTG, "0.000")
        _dtPurGem = grdGem.DataSource

        'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
        'txtGemsK.Text = CStr(GoldWeight.WeightK)
        'txtGemsP.Text = CStr(GoldWeight.WeightP)
        'txtGemsY.Text = CStr(Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.0"))
    End Sub

    Private Sub CalculateGoldWeight()
        If chkOnlyGem.Checked Then
            _GoldTG = 0.0
            _GoldTK = 0.0

            txtGoldK.Text = "0"
            txtGoldP.Text = "0"
            txtGoldY.Text = "0.0"
        Else
            Dim weightY As Decimal = 0
            Dim weightP As Integer = 0
            Dim weightK As Integer = 0
            If _TotalTG <> 0.0 Or _TotalGemTG <> 0.0 Then
                Dim ItemWeight As New CommonInfo.GoldWeightInfo
                Dim GemWeight As New CommonInfo.GoldWeightInfo
                Dim GoldWeight As New CommonInfo.GoldWeightInfo

                ItemWeight.WeightK = CDec(txtItemK.Text)
                ItemWeight.WeightP = CDec(txtItemP.Text)
                ItemWeight.WeightY = CDec(txtItemY.Text)

                GemWeight.WeightK = CDec(txtGemsK.Text)
                GemWeight.WeightP = CDec(txtGemsP.Text)
                GemWeight.WeightY = CDec(txtGemsY.Text)

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

                txtGoldK.Text = Format(GoldWeight.WeightK, "0")
                txtGoldP.Text = Format(GoldWeight.WeightP, "0")
                txtGoldY.Text = Format(GoldWeight.WeightY, "0.0")

                GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
                _GoldTK = GoldWeight.GoldTK
                GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
                _GoldTG = GoldWeight.Gram
                txtGoldG.Text = Format(CDec(txtItemTG.Text) - CDec(txtGemsG.Text), "0.000")
            Else
                _GoldTK = 0.0
                _GoldTG = 0.0

                txtGoldG.Text = "0.0"
                txtGoldK.Text = "0"
                txtGoldP.Text = "0"
                txtGoldY.Text = "0.0"
            End If

            If _ForSaleID <> "" And chkIsFixPrice.Checked = False Then
                CalculateSaleGoldAmount()
            End If
        End If

    End Sub
    Private Sub CalculateSaleGoldAmount()

        Dim TempTK As Decimal = 0.0
        If txtSaleWasteK.Text = "" Then txtSaleWasteK.Text = "0"
        If txtSaleWasteP.Text = "" Then txtSaleWasteP.Text = "0"
        If txtSaleWasteY.Text = "" Then txtSaleWasteY.Text = "0"
        If txtSaleWasteG.Text = "" Then txtSaleWasteG.Text = "0"
        If txtGoldK.Text = "" Then txtGoldK.Text = "0"
        If txtGoldP.Text = "" Then txtGoldP.Text = "0"
        If txtGoldY.Text = "" Then txtGoldY.Text = "0"
        If txtGoldG.Text = "" Then txtGoldG.Text = "0"


        If _IsGram = False Then
            Dim GoldWeight As New CommonInfo.GoldWeightInfo
            GoldWeight.WeightK = CInt(txtGoldK.Text) + CInt(txtSaleWasteK.Text)
            GoldWeight.WeightP = CInt(txtGoldP.Text) + CInt(txtSaleWasteP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtSaleWasteY.Text))
            GoldWeight.WeightC = (CDec(txtGoldY.Text) + CDec(txtSaleWasteY.Text)) - GoldWeight.WeightY
            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            TempTK = GoldWeight.GoldTK
            _SGoldPrice = CStr(_SaleRate * TempTK)
        Else
            _SGoldPrice = CStr(_SaleRate * (CDec(txtGoldG.Text) + CDec(txtSaleWasteG.Text)))
        End If
        CalculateSaleTotalAmount()
    End Sub
    Private Sub CalculateSaleTotalAmount()
        txtSaleAmount.Text = Format(Val(_SGoldPrice + _SGemsPrice + _STotalCharges), "###,##0.##")
    End Sub
    Private Sub CalculateGoldAmount()
        If txtCurPrice.Text = "" Then txtCurPrice.Text = "0"
        Dim TempTK As Decimal = 0.0
        Dim _GoldPrice As Integer = 0
        If txtPWasteK.Text = "" Then txtPWasteK.Text = "0"
        If txtPWasteP.Text = "" Then txtPWasteP.Text = "0"
        If txtPWasteY.Text = "" Then txtPWasteY.Text = "0.0"
        If txtPWasteG.Text = "" Then txtPWasteG.Text = "0.0"

        If txtGoldK.Text = "" Then txtGoldK.Text = "0"
        If txtGoldP.Text = "" Then txtGoldP.Text = "0"
        If txtGoldY.Text = "" Then txtGoldY.Text = "0.0"
        If txtGoldG.Text = "" Then txtGoldG.Text = "0.0"

        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        GoldWeight.WeightK = CInt(txtGoldK.Text) + CInt(txtPWasteK.Text)
        GoldWeight.WeightP = CInt(txtGoldP.Text) + CInt(txtPWasteP.Text)
        GoldWeight.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtPWasteY.Text))
        GoldWeight.WeightC = (CDec(txtGoldY.Text) + CDec(txtPWasteY.Text)) - GoldWeight.WeightY
        GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
        TempTK = GoldWeight.GoldTK

        If (chkDone.Checked = False And chkPurchasePercent.Checked = False) Then
            If _IsPercentage Then
                If _IsGram = False Then
                    _GoldPrice = (_CurrentSaleRate * TempTK) - ((_CurrentSaleRate * TempTK) * (txtCurPrice.Text) / 100)
                Else
                    _GoldPrice = (_CurrentSaleRate * (CDec(txtGoldG.Text) + CDec(txtPWasteG.Text))) - ((_CurrentSaleRate * (CDec(txtGoldG.Text) + CDec(txtPWasteG.Text))) * (txtCurPrice.Text) / 100)
                End If
            Else
                If _IsGram = False Then
                    _GoldPrice = CStr(CLng(txtCurPrice.Text) * TempTK)
                Else
                    _GoldPrice = CStr(CLng(txtCurPrice.Text) * (CDec(txtGoldG.Text) + CDec(txtPWasteG.Text)))
                End If
            End If
            txtGoldPrice.Text = Format(Val(_GoldPrice), "###,##0.##")
        Else
            txtGoldPrice.Text = "0"
        End If
        CalculateTotalAmount()
    End Sub

    Private Sub CalculategrdTotalAmount()
        Dim grdtotalAmt As Decimal = 0
        Dim tmpAmount As Decimal = 0.0
        If (chkDone.Checked = False And chkPurchasePercent.Checked = False) Then
            For i As Integer = 0 To grdGem.RowCount - 1
                If Not grdGem.Rows(i).IsNewRow Then
                    If grdGem.Rows(i).Cells("Amount").FormattedValue <> "" Then
                        grdtotalAmt += CLng(grdGem.Rows(i).Cells("Amount").FormattedValue)
                    End If
                End If
            Next
            txtGemsPrice.Text = Format(Val(grdtotalAmt), "###,##0.##")
        Else
            txtGemsPrice.Text = "0"
        End If


        If chkIsFixPrice.Checked = False And _ForSaleID <> "" Then
            _SGemsPrice = 0
            For i As Integer = 0 To grdGem.RowCount - 1
                If Not grdGem.Rows(i).IsNewRow Then
                    _SGemsPrice += CLng(grdGem.Rows(i).Cells("Amount").FormattedValue)
                End If
            Next
            CalculateSaleTotalAmount()
        End If

        CalculateTotalAmount()
    End Sub

    Private Sub CalculateAllDetailTotalAmount()
        Dim grdTotalAmt As Long = 0
        If _dtDetail.Rows.Count > 0 Then
            For i As Integer = 0 To grdDetail.RowCount - 1
                grdTotalAmt += CLng(grdDetail.Rows(i).Cells("NetAmount").FormattedValue)
            Next
            txtAllTotalAmt.Text = Format(Val(grdTotalAmt), "###,##0.##")
            txtAllNetAmt.Text = Format(Val(CLng(txtAllTotalAmt.Text)), "###,##0.##")
            txtAddSub.Text = "0"
            txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text)), "###,##0.##")
            txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
        End If
    End Sub

    Private Sub ShowPHeaderData(ByVal obj As CommonInfo.PurchaseHeaderInfo)
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        Dim objCustomer As New CustomerInfo
        Dim GoldQuality As New GoldQualityInfo

        With obj
            chkOnlyGem.Checked = .IsGem
            chkDiamond.Checked = .IsLooseDiamond
            dtpPurchaseDate.Value = .PurchaseDate
            _PurchaseHeaderID = .PurchaseHeaderID
            txtPurchaseItemID.Text = .PurchaseHeaderID
            _CustomerID = .CustomerID
            _IsOrder = .IsOrder
            If chkOnlyGem.Checked Then
                chkShopGold.Checked = False
                chkIsChange.Checked = False
                chkShopGold.Enabled = False
                chkIsChange.Enabled = False
                tabStock.SelectedIndex = 1
                ''If tabStock.SelectedTab.Name = "TabStockItem" Then
                'tabStock.SelectedIndex = 0
                tabStock.TabPages.Remove(TabStockItem)
                tabStock.TabPages.Remove(TabLooseDiamond)
                tabStock.TabPages.Add(TabGem)
                chkNormal.Enabled = False
                chkDiamond.Enabled = False
                'End If
            ElseIf chkDiamond.Checked Then
                chkIsChange.Checked = False
                'chkIsChange.Visible = False
                'chkIsChange.Enabled = False
                tabStock.SelectedIndex = 0
                tabStock.TabPages.Remove(TabStockItem)
                tabStock.TabPages.Remove(TabGem)
                tabStock.TabPages.Add(TabLooseDiamond)
                chkNormal.Enabled = False
                chkOnlyGem.Enabled = False
            Else
                chkShopGold.Enabled = True
                chkIsChange.Enabled = True
                chkIsChange.Visible = True
                chkNormal.Enabled = True
                chkNormal.Checked = True
                    tabStock.SelectedIndex = 0
                tabStock.TabPages.Remove(TabGem)
                tabStock.TabPages.Remove(TabLooseDiamond)
                tabStock.TabPages.Remove(TabStockItem)
                tabStock.TabPages.Add(TabStockItem)
                chkOnlyGem.Enabled = False
                chkDiamond.Enabled = False

            End If

            chkIsChange.Checked = .IsChange
            chkShopGold.Checked = .IsShop
            If chkShopGold.Checked Then
                chkOnlyGem.Enabled = False
            End If
            If chkIsChange.Checked Then
                chkOnlyGem.Enabled = False
            End If

            objCustomer = objCustomerController.GetCustomerByID(_CustomerID)
            txtCustomerCode.Text = objCustomer.CustomerCode
            txtCustomer.Text = objCustomer.CustomerName
            txtAddress.Text = .Address
            _StaffID = .StaffID
            cboPurchaseStaff.SelectedValue = _StaffID
            txtRemark.Text = .Remark
            'txtAllTotalAmt.Text = Format(Val(.AllTotalAmount), "###,##0.##")
            Dim grdtotalAmt As Long = 0
            For i As Integer = 0 To grdDetail.RowCount - 1
                grdtotalAmt += CLng(grdDetail.Rows(i).Cells("NetAmount").FormattedValue)
            Next
            txtAllTotalAmt.Text = Format(Val(grdtotalAmt), "###,##0.##")
            txtAddSub.Text = Format(Val(.AllAddOrSub), "###,##0.##")
            txtAllNetAmt.Text = Format(Val(CLng(txtAllTotalAmt.Text) - CLng(txtAddSub.Text)), "###,##0.##")
            txtPaidAmt.Text = Format(Val(.AllPaidAmount), "###,##0.##")
            txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
        End With

    End Sub

#End Region

#Region "Button Click And Link Click"

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtItemCode.Text = ""
        ClearDetail()
    End Sub
    Private Sub btndClear_Click(sender As Object, e As EventArgs) Handles btnDClear.Click
        txtDItemCode.Text = ""
        ClearDetail()
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

    Private Sub btnSearchButton_Click(sender As Object, e As EventArgs) Handles btnSearchButton.Click
        btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString
        Dim dt As New DataTable
        Dim dtrow As DataRow
        Dim objPHeader As New CommonInfo.PurchaseHeaderInfo
        txtItemCode.Text = ""
        ClearDetail()
        _IsUpdate = False
        dt = objPurItemController.GetAllPuchaseHeader()
        dtrow = DirectCast(SearchData.FindFast(dt, "Purchase Row Material List"), DataRow)
        If dtrow IsNot Nothing Then
            _PurchaseHeaderID = dtrow.Item("VoucherNo").ToString
            objPHeader = objPurItemController.GetPurchaseHeaderByID(_PurchaseHeaderID)
            _dtDetail.Rows.Clear()
            _dtPurGem.Rows.Clear()
            _dtStone.Rows.Clear()
            _IsGem = objPHeader.IsGem
            _IsLooseDiamond = objPHeader.IsLooseDiamond

            If _IsGem = True Then
                _dtDetail = objPurItemController.GetPurchaseGemByID(_PurchaseHeaderID)
            ElseIf _IsLooseDiamond = True Then
                _dtDetail = objPurItemController.GetPurchaseDiamondByID(_PurchaseHeaderID)
            Else
                _dtDetail = objPurItemController.GetPurchaseDetailByID(_PurchaseHeaderID)
            End If


            If _IsGem = True Or _IsLooseDiamond = True Then
                grdDetail.Columns("GemsCategory").Visible = True
                grdDetail.Columns("GemsName").Visible = True
                grdDetail.Columns("TotalGemTG").Visible = True
                grdDetail.Columns("BarcodeNo").Visible = False
                grdDetail.Columns("ItemName").Visible = False
                grdDetail.Columns("TotalTG").Visible = False
            Else
                grdDetail.Columns("GemsCategory").Visible = False
                grdDetail.Columns("GemsName").Visible = False
                grdDetail.Columns("TotalGemTG").Visible = False
                grdDetail.Columns("BarcodeNo").Visible = True
                grdDetail.Columns("ItemName").Visible = True
                grdDetail.Columns("TotalTG").Visible = True
            End If
            grdDetail.DataSource = _dtDetail
            _dtStone = objPurItemController.GetPurchaseDetailGemByID(_PurchaseHeaderID)
            'CalculategrdTotalAmount()
            'CalculateAllDetailTotalAmount()

            CalculategrAlldTotalWeight()
            ShowPHeaderData(objPHeader)
            btnDelete.Enabled = True
            If Global_CurrentUser = "Administrator" Then
                dtpPurchaseDate.Enabled = True
                btnSave.Enabled = True
                btnDelete.Enabled = True
                txtCurPrice.ReadOnly = False
            Else
                If Global_IsAllowSaleReturn Then
                    dtpPurchaseDate.Enabled = True
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    txtCurPrice.ReadOnly = False
                Else
                    dtpPurchaseDate.Enabled = False
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    txtCurPrice.ReadOnly = True
                End If
            End If
            grdDetail.DataSource = _dtDetail
        Else
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString

        End If
    End Sub

    'Private Sub btnSaleVoucherSearch_Click(sender As Object, e As EventArgs) Handles btnSaleVoucherSearch.Click
    '    Dim dt As New DataTable
    '    Dim DataItem As DataRow
    '    Dim objCust As New CommonInfo.CustomerInfo

    '    dt = objSaleItemInvoiceController.GetAllSalesInvoiceForPurchase(Global_IsReuseBarcode)
    '    DataItem = DirectCast(Search.FindFast(dt, "Sale Invoice List"), DataRow)
    '    If DataItem IsNot Nothing Then
    '        If _SaleInvoiceHeaderID <> DataItem("@SaleInvoiceHeaderID") Then
    '            ClearForSaleVoucher()
    '            _dtDetail.Rows.Clear()
    '            _dtStone.Rows.Clear()
    '            grdDetail.AutoGenerateColumns = False
    '            grdDetail.ReadOnly = True
    '            grdDetail.DataSource = _dtDetail
    '            ClearDetail()
    '        End If
    '        _SaleInvoiceHeaderID = DataItem("@SaleInvoiceHeaderID")
    '        _SaleVoucherNo = DataItem("VoucherNo")
    '        lblSaleInvoiceHeaderID.Text = _SaleVoucherNo
    '        lblSaleDate.Text = DataItem("SaleDate").ToString()
    '        _CustomerID = DataItem("@CustomerID")
    '        _IsOrder = DataItem("$IsOrder")
    '        objCust = objCustomerController.GetCustomerByID(_CustomerID)
    '        With objCust
    '            txtCustomerCode.Text = .CustomerCode
    '            txtCustomer.Text = .CustomerName
    '            txtAddress.Text = .CustomerAddress
    '        End With
    '    End If
    'End Sub

    Private Sub btnBarcodeSearch_Click(sender As Object, e As EventArgs) Handles btnBarcodeSearch.Click
        Dim dtItemCode As New DataTable
        Dim DataItem As DataRow
        Dim cri As String = ""
        If _CustomerID = "" Then
            MsgBox("Please Select Customer", MsgBoxStyle.Information, AppName)
            btnCustomer.Focus()
        Else
            If GetExistedItems() <> "" Then
                cri = " AND M.[@ForSaleID] NOT IN (" & GetExistedItems() & ") "
            End If
            dtItemCode = objSaleItemInvoiceController.GetSalesInvoiceDataByCustomerIDAndItemCode(_CustomerID, cri)
            DataItem = DirectCast(SearchData.FindFast(dtItemCode, "Sale Invoice List"), DataRow)
            If DataItem IsNot Nothing Then
                Dim GoldWeight As New CommonInfo.GoldWeightInfo
                Dim objGQInfo As New CommonInfo.GoldQualityInfo

                txtItemCode.Text = DataItem("ItemCode")
                lblOriginalCode.Text = DataItem("@OriginalCode")
                lblOrgSaleRate.Text = Format(Val(DataItem("@SalesRate")), "###,##0.##")
                _SaleRate = DataItem("@SalesRate")
                _SGoldPrice = DataItem("@GoldPrice")
                _SGemsPrice = DataItem("@GemsPrice")
                _STotalCharges = DataItem("@TotalCharges")

                _SaleInvoiceDetailID = DataItem("@SaleInvoiceDetailID")
                _ConsignmentSaleItemID = DataItem("@ConsignmentSaleItemID")
                _IsOrder = DataItem("@IsOrder")
                _ForSaleID = DataItem("@ForSaleID")
                cboItemCategory.SelectedValue = DataItem("@ItemCategoryID")
                cboItemName.SelectedValue = DataItem("@ItemNameID")
                If DataItem("Length") <> "" Then
                    txtWidthLength.Text = DataItem("Length")
                Else
                    txtWidthLength.Text = "-"
                End If

                txtQty.Text = 1
                _IsFixPrice = DataItem("@IsFixPrice")
                chkIsFixPrice.Checked = DataItem("@IsFixPrice")
                cboGoldQuality.SelectedValue = DataItem("@GoldQualityID")
                objGQInfo = objGoldQualityController.GetGoldQuality(cboGoldQuality.SelectedValue)
                With objGQInfo
                    _IsGram = .IsGramRate
                End With
                GoldQualityForTextChange()
                GetCurrentPrice()


                'GoldWeight.GoldTK = _TotalTK
                'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtItemK.Text = CStr(DataItem("ItemK"))
                txtItemP.Text = CStr(DataItem("ItemP"))
                txtItemY.Text = CStr(DataItem("ItemY"))
                txtItemTG.Text = Format(DataItem("@ItemTG"), "0.000")
                _TotalTK = DataItem("@ItemTK")
                _TotalTG = DataItem("@ItemTG")


                'GoldWeight.GoldTK = _TotalGemTK
                'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtGemsK.Text = CStr(DataItem("GemsK"))
                txtGemsP.Text = CStr(DataItem("GemsP"))
                txtGemsY.Text = CStr(DataItem("GemsY"))
                txtGemsG.Text = Format(DataItem("@GemsTG"), "0.000")
                _TotalGemTK = DataItem("@GemsTK")
                _TotalGemTG = DataItem("@GemsTG")

                'GoldWeight.GoldTK = _WasteTK
                'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtSaleWasteK.Text = CStr(DataItem("WasteK"))
                txtSaleWasteP.Text = CStr(DataItem("WasteP"))
                txtSaleWasteY.Text = CStr(DataItem("WasteY"))
                txtSaleWasteG.Text = Format(DataItem("@WasteTG"), "0.000")
                _WasteTK = DataItem("@WasteTK")
                _WasteTG = DataItem("@WasteTG")

                'GoldWeight.GoldTK = _GoldTK
                'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtGoldK.Text = CStr(DataItem("GoldK"))
                txtGoldP.Text = CStr(DataItem("GoldP"))
                txtGoldY.Text = CStr(DataItem("GoldY"))
                txtGoldG.Text = Format(DataItem("@GoldTG"), "0.000")
                _GoldTK = DataItem("@GoldTK")
                _GoldTG = DataItem("@GoldTG")
                chkIsDiamond.Checked = DataItem("@IsDiamond")

                If DataItem("@Photo") <> "" Then
                    Try
                        lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + DataItem("@Photo"))
                        lblPhoto.Visible = False
                        Photo = DataItem("@Photo")
                    Catch ex As Exception
                        lblItemImage.Image = Nothing
                        lblPhoto.Visible = True
                        Photo = ""
                    End Try
                Else
                    Photo = ""
                    If Global_LogoPhoto <> "" Then
                        Try
                            lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                            lblPhoto.Visible = False
                        Catch ex As Exception
                            lblItemImage.Image = Nothing
                            lblPhoto.Visible = True
                        End Try
                    Else
                        lblItemImage.Image = Nothing
                        lblPhoto.Visible = True
                    End If
                End If

                If Val(txtSaleAmount.Text) > 0 Then
                    chkPurchasePercent.Enabled = True
                End If
                CalculateGoldAmount()

                If _IsOrder Then
                    _dtPurGem = _OrderInvoiceController.GetOrderInvoiceGemDataByOrderDetailID(_SaleInvoiceDetailID)
                Else
                    _dtPurGem = objSaleItemInvoiceController.GetSalesInvoiceGemDataBySaleDetailID(_SaleInvoiceDetailID)
                End If
                grdGem.DataSource = _dtPurGem
                CalculategrdTotalAmount()

                If DataItem("@IsFixPrice") Then
                    txtSaleAmount.Text = Format(Val(DataItem("@ItemNetAmount")), "###,##0.##")
                Else
                    txtSaleAmount.Text = Format(Val(DataItem("@GoldPrice") + Val(DataItem("@AddOrSub"))), "###,##0.##")
                End If

            Else
                txtItemCode.Text = ""
                ClearBarcodeData()
            End If
        End If

    End Sub
    Private Function GetExistedItems() As String
        GetExistedItems = ""
        For i As Integer = 0 To _dtDetail.Rows.Count - 1
            If _dtDetail.Rows(i).RowState <> DataRowState.Deleted Then
                If _dtDetail.Rows(i).Item("ForSaleID") <> "" Then
                    GetExistedItems += "'" & _dtDetail.Rows(i).Item("ForSaleID") & "',"
                End If
            End If
        Next
        Return GetExistedItems.Trim(",")
    End Function
    Private Function GetExistedGemItems() As String
        GetExistedGemItems = ""
        For i As Integer = 0 To _dtDetail.Rows.Count - 1
            If _dtDetail.Rows(i).RowState <> DataRowState.Deleted Then
                If _dtDetail.Rows(i).Item("SaleGemsItemID") <> "" Then
                    GetExistedGemItems += "'" & _dtDetail.Rows(i).Item("SaleGemsItemID") & "',"
                End If
            End If
        Next
        Return GetExistedGemItems.Trim(",")
    End Function


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If chkOnlyGem.Checked = False Then
            If cboItemCategory.Text = "" Then
                MsgBox("Please Fill Combo!", MsgBoxStyle.Information, "Data Require!")
                cboItemCategory.Focus()
                Exit Sub
            End If

            If cboItemName.Text = "" Then
                MsgBox("Please Fill Combo!", MsgBoxStyle.Information, "Data Require!")
                cboItemName.Focus()
                Exit Sub
            End If
            If cboGoldQuality.Text = "" Then
                MsgBox("Please Fill Combo!", MsgBoxStyle.Information, "Data Require!")
                cboGoldQuality.Focus()
                Exit Sub
            End If

            If Val(txtItemTG.Text) = 0.0 Then
                MsgBox("Please Check GoldWeigh!", MsgBoxStyle.Information, "Data Require!")
                LnkTotalNoWaste.Focus()
                Exit Sub
            End If
            If txtQty.Text = "0" Then
                MsgBox("Please Enter Quantity!", MsgBoxStyle.Information, "Data Require!")
                txtQty.Focus()
                Exit Sub
            End If
            If chkShopGold.Checked And txtItemCode.Text = "" Then
                MsgBox("Please Select Valid Barcode No!", MsgBoxStyle.Information, "Data Require!")
                btnBarcodeSearch.Focus()
                Exit Sub
            End If

            If chkDone.Checked And Val(txtDoneAmount.Text) = 0 Then
                MsgBox("Please Check Done Amount!", MsgBoxStyle.Information, "Data Require!")
                txtDoneAmount.Focus()
                Exit Sub
            End If

            If chkPurchasePercent.Checked And Val(txtPurchasePercent.Text) = 0 Then
                MsgBox("Please Check Percentate!", MsgBoxStyle.Information, "Data Require!")
                txtPurchasePercent.Focus()
                Exit Sub
            End If

            If Val(txtTotalAmt.Text) = 0 Then
                MsgBox("Please Check Amount!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If

            If _dtDetail.Rows.Count > 0 And _IsUpdate = False And txtItemCode.Text <> "" Then
                For Each dr As DataRow In _dtDetail.Rows
                    If dr.RowState <> DataRowState.Deleted Then
                        If dr("BarcodeNo") <> "" Then
                            If dr("BarcodeNo") = txtItemCode.Text Then
                                MsgBox("Duplicate BarcodeNo!", MsgBoxStyle.Information, "Duplicate Data!")
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            End If
        Else
            If cboGemCategory.SelectedIndex = -1 Then
                MsgBox("Please Fill Combo!", MsgBoxStyle.Information, "Data Require!")
                cboGemCategory.Focus()
                Exit Sub
            End If
            If radRBP.Checked And Val(txtRBP.Text) = 0 Then
                MsgBox("Please Enter Gem Weight!", MsgBoxStyle.Information, "Data Require!")
                txtRBP.Focus()
                Exit Sub
            End If
            If radKyat.Checked And GemTK = 0.0 Then
                MsgBox("Please Enter Gem Weight!", MsgBoxStyle.Information, "Data Require!")
                txtGemK.Focus()
                Exit Sub
            End If

            If Val(txtGemQTY.Text) = 0 Then
                MsgBox("Please Enter Gem QTY!", MsgBoxStyle.Information, "Data Require!")
                txtGemK.Focus()
                Exit Sub
            End If

            If cboFixType.Text = "" Then
                MsgBox("Please Enter Type!", MsgBoxStyle.Information, "Data Require!")
                cboFixType.Focus()
                Exit Sub
            End If

            If Val(txtUnitPrice.Text) = 0 Then
                MsgBox("Please Enter Purchase Rate!", MsgBoxStyle.Information, "Data Require!")
                txtUnitPrice.Focus()
                Exit Sub
            End If

            If Val(txtTotalAmount.Text) = 0 Then
                MsgBox("Please Check Amount!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If
        End If


        If _IsUpdate Then
            UpdateItem(_PurchaseDetailID, _dtPurGem)
        Else

            If btnAdd.Text = "Add" Then
                _PurchaseDetailID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseDetail, EnumSetting.GenerateKeyType.PurchaseDetail.ToString, dtpPurchaseDate.Value)
                InsertItem(_PurchaseDetailID, _dtPurGem)
            End If
        End If
        CalculateAllDetailTotalAmount()
        CalculategrAlldTotalWeight()
        txtItemCode.Text = ""
        ClearBarcodeData()
    End Sub

    Private Sub CalculategrAlldTotalWeight()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim _TotalTK As Decimal = 0
        Dim _TotalTG As Decimal = 0
        Dim _TotalQTY As Integer = 0
        For j As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(j).IsNewRow Then
                _TotalTG += CDec(grdDetail.Rows(j).Cells("TotalTG").FormattedValue)
                _TotalTK += CDec(grdDetail.Rows(j).Cells("TotalTK").FormattedValue)
                _TotalQTY += 1
            End If
        Next

        GoldWeight.GoldTK = _TotalTK
        GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
        txtTotalItemK.Text = CStr(GoldWeight.WeightK)
        txtTotalItemP.Text = CStr(GoldWeight.WeightP)
        txtTotalItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        txtTotalItemG.Text = Format(_TotalTG, "0.000")
        txtTotalQTY.Text = _TotalQTY
    End Sub
    Private Sub LnkTotalNoWaste_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkTotalNoWaste.LinkClicked
        Dim frm As New frm_ToWeight
        Dim GoldWeight As New GoldWeightInfo
        frm.ShowDialog()
        _WeightType = frm._OptType
        GoldWeight = frm._GoldWeightInfo
        If _IsGram = False And frm._OptType = "Kyat" Then
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            txtItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
        ElseIf _IsGram = False And _WeightType = "Gram" Then
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            CalculateTotalWeightForGram()
        Else
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
        End If
    End Sub
#End Region

#Region "Checked Change"

    Private Sub chkShopGold_CheckedChanged(sender As Object, e As EventArgs) Handles chkShopGold.CheckedChanged
        If chkShopGold.Checked Then
            chkIsFixPrice.Visible = True
            lblSaleAmount.Visible = True
            txtItemCode.Visible = True
            txtSaleAmount.Visible = True
            btnBarcodeSearch.Visible = True
            chkIsDiamond.Visible = True
            lblOriginalCode.Visible = True
            lblOrgSaleRate.Visible = True
        Else
            txtItemCode.Text = ""
            ClearStock()
        End If

    End Sub

#End Region

#Region "Key Press and Key Up and cbo changed"

    Private Sub cboGoldQuality_Click(sender As Object, e As EventArgs) Handles cboGoldQuality.Click
        GetGoldQualityCombo()
    End Sub

    Private Sub cboGoldQuality_KeyUp(sender As Object, e As KeyEventArgs) Handles cboGoldQuality.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQuality, e)
    End Sub

    Private Sub cboGoldQuality_Leave(sender As Object, e As EventArgs) Handles cboGoldQuality.Leave
        AutoCompleteCombo_Leave(cboGoldQuality, "")
    End Sub

    Private Sub cboItemCategory_Click(sender As Object, e As EventArgs) Handles cboItemCategory.Click
        GetItemCategoryCombo()
    End Sub

    Private Sub cboItemCategory_KeyUp(sender As Object, e As KeyEventArgs) Handles cboItemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCategory, e)
    End Sub

    Private Sub cboItemCategory_Leave(sender As Object, e As EventArgs) Handles cboItemCategory.Leave
        AutoCompleteCombo_Leave(cboItemCategory, "")
    End Sub

    Private Sub cboItemName_Click(sender As Object, e As EventArgs) Handles cboItemName.Click
        GetItemNameCombo()
    End Sub

    Private Sub cboItemName_KeyUp(sender As Object, e As KeyEventArgs) Handles cboItemName.KeyUp
        AutoCompleteCombo_KeyUp(cboItemName, e)
    End Sub

    Private Sub cboItemName_Leave(sender As Object, e As EventArgs) Handles cboItemName.Leave
        AutoCompleteCombo_Leave(cboItemName, "")
    End Sub

    Private Sub cboPurchaseStaff_Click(sender As Object, e As EventArgs) Handles cboPurchaseStaff.Click
        GetStaffCombo()
    End Sub

    Private Sub cboPurchaseStaff_KeyUp(sender As Object, e As KeyEventArgs) Handles cboPurchaseStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboPurchaseStaff, e)
    End Sub

    Private Sub cboPurchaseStaff_Leave(sender As Object, e As EventArgs) Handles cboPurchaseStaff.Leave
        AutoCompleteCombo_Leave(cboPurchaseStaff, "")
    End Sub

    Private Sub cboGemCategory_Click(sender As Object, e As EventArgs) Handles cboGemCategory.Click
        GetGemsCategoryCombo()
    End Sub

    Private Sub cboGemCategory_KeyUp(sender As Object, e As KeyEventArgs) Handles cboGemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboGemCategory, e)
    End Sub
    Private Sub cboGemCategory_Leave(sender As Object, e As EventArgs) Handles cboGemCategory.Leave
        AutoCompleteCombo_Leave(cboGemCategory, "")
    End Sub
    Private Sub cboFixType_KeyUp(sender As Object, e As KeyEventArgs) Handles cboFixType.KeyUp
        AutoCompleteCombo_KeyUp(cboFixType, e)
    End Sub
    Private Sub cboFixType_Leave(sender As Object, e As EventArgs) Handles cboFixType.Leave
        AutoCompleteCombo_Leave(cboFixType, "")
    End Sub

    Private Sub cboGoldQuality_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboGoldQuality.SelectedValueChanged
        Dim objGQInfo As New CommonInfo.GoldQualityInfo
        If cboGoldQuality.Text <> "" Then
            objGQInfo = objGoldQualityController.GetGoldQuality(cboGoldQuality.SelectedValue)
            With objGQInfo
                _IsGram = .IsGramRate
            End With
            GetCurrentPrice()
        Else
            _IsGram = False
            _IsPercentage = False
            txtCurPrice.Text = "0"
            lblPercent.Text = ""
            _CurrentSaleRate = 0
        End If
        GoldQualityForTextChange()
        CalculateGoldAmount()
    End Sub
    Private Sub GoldQualityForTextChange()
        If cboGoldQuality.Text <> "" Then
            If _IsGram = True Then
                txtItemK.ReadOnly = True
                txtItemP.ReadOnly = True
                txtItemY.ReadOnly = True
                txtItemTG.ReadOnly = False

                txtItemTG.TabIndex = 1
                txtItemK.TabStop = False
                txtItemP.TabStop = False
                txtItemY.TabStop = False
                txtItemTG.TabStop = True

                txtItemK.BackColor = Color.Linen
                txtItemP.BackColor = Color.Linen
                txtItemY.BackColor = Color.Linen
                txtItemTG.BackColor = Color.White

                If Global_UserLevel = "Administrator" Then
                    txtPWasteK.ReadOnly = True
                    txtPWasteP.ReadOnly = True
                    txtPWasteY.ReadOnly = True
                    txtPWasteG.ReadOnly = False

                    txtPWasteG.TabIndex = 2
                    txtPWasteK.TabStop = False
                    txtPWasteP.TabStop = False
                    txtPWasteY.TabStop = False
                    txtPWasteG.TabStop = True

                    txtPWasteK.BackColor = Color.Linen
                    txtPWasteP.BackColor = Color.Linen
                    txtPWasteY.BackColor = Color.Linen
                    txtPWasteG.BackColor = Color.White
                End If

            Else
                txtItemK.ReadOnly = False
                txtItemP.ReadOnly = False
                txtItemY.ReadOnly = False
                txtItemTG.ReadOnly = True
                txtItemK.TabIndex = 1
                txtItemP.TabIndex = 2
                txtItemY.TabIndex = 3
                txtItemK.TabStop = True
                txtItemP.TabStop = True
                txtItemY.TabStop = True
                txtItemTG.TabStop = False

                txtItemK.BackColor = Color.White
                txtItemP.BackColor = Color.White
                txtItemY.BackColor = Color.White
                txtItemTG.BackColor = Color.Linen

                If Global_UserLevel = "Administrator" Then
                    txtPWasteK.ReadOnly = False
                    txtPWasteP.ReadOnly = False
                    txtPWasteY.ReadOnly = False
                    txtPWasteG.ReadOnly = True

                    txtPWasteK.TabIndex = 4
                    txtPWasteP.TabIndex = 5
                    txtPWasteY.TabIndex = 6

                    txtPWasteK.TabStop = True
                    txtPWasteP.TabStop = True
                    txtPWasteY.TabStop = True
                    txtPWasteG.TabStop = False

                    txtPWasteK.BackColor = Color.White
                    txtPWasteP.BackColor = Color.White
                    txtPWasteY.BackColor = Color.White
                    txtPWasteG.BackColor = Color.Linen
                End If
            End If
        Else
            txtItemK.ReadOnly = True
            txtItemP.ReadOnly = True
            txtItemY.ReadOnly = True
            txtItemTG.ReadOnly = True

            txtItemK.BackColor = Color.Linen
            txtItemP.BackColor = Color.Linen
            txtItemY.BackColor = Color.Linen
            txtItemTG.BackColor = Color.Linen

            txtPWasteK.ReadOnly = True
            txtPWasteP.ReadOnly = True
            txtPWasteY.ReadOnly = True
            txtPWasteG.ReadOnly = True

            txtPWasteK.BackColor = Color.Linen
            txtPWasteP.BackColor = Color.Linen
            txtPWasteY.BackColor = Color.Linen
            txtPWasteG.BackColor = Color.Linen
        End If

    End Sub
    Private Sub chkIsDamage_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsDamage.CheckedChanged
        If chkIsDamage.Checked Then
            chkIsDamage.Checked = True
            chkIsExchange.Checked = False
        Else
            chkIsDamage.Checked = False
        End If
        GetCurrentPrice()
        CalculateGoldAmount()
    End Sub

    Private Sub chkIsExchange_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsExchange.CheckedChanged
        If chkIsExchange.Checked Then
            chkIsDamage.Checked = False
            chkIsExchange.Checked = True
        Else
            chkIsExchange.Checked = False
        End If
        GetCurrentPrice()
        CalculateGoldAmount()
    End Sub

    Private Sub cboItemCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboItemCategory.SelectedValueChanged
        itemid = cboItemCategory.SelectedValue
        RefreshItemNameCbo(itemid)
    End Sub



    Private Sub chkOnlyGem_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyGem.CheckedChanged
        Dim objPHeader As New CommonInfo.PurchaseHeaderInfo
        If chkOnlyGem.Checked Then
            If _PurchaseHeaderID = "0" Then
                tabStock.SelectedIndex = 1
                tabStock.TabPages.Remove(TabStockItem)
                tabStock.TabPages.Add(TabGem)
                chkIsChange.Checked = False
                chkIsChange.Enabled = False
                _dtDetail.Rows.Clear()
                '_dtStone.Rows.Clear()
                grdDetail.AutoGenerateColumns = False
                grdDetail.ReadOnly = True
                grdDetail.DataSource = _dtDetail
                grdDetail.Columns("GemsCategory").Visible = True
                grdDetail.Columns("GemsName").Visible = True
                grdDetail.Columns("TotalGemTG").Visible = True
                grdDetail.Columns("BarcodeNo").Visible = False
                grdDetail.Columns("ItemName").Visible = False
                grdDetail.Columns("TotalTG").Visible = False
                grdDetail.Columns("IsShop").Visible = False
                txtSaleGemsID.Visible = False
                btnSaleGemSearch.Visible = False
            End If

        Else
            If _PurchaseHeaderID = "0" Then
                tabStock.SelectedIndex = 0
                tabStock.TabPages.Remove(TabGem)
                tabStock.TabPages.Add(TabStockItem)
                chkIsChange.Enabled = True
                _dtDetail.Rows.Clear()
                '_dtStone.Rows.Clear()
                grdDetail.AutoGenerateColumns = False
                grdDetail.ReadOnly = True
                grdDetail.DataSource = _dtDetail
                grdDetail.Columns("GemsCategory").Visible = False
                grdDetail.Columns("GemsName").Visible = False
                grdDetail.Columns("TotalGemTG").Visible = False
                grdDetail.Columns("BarcodeNo").Visible = True
                grdDetail.Columns("ItemName").Visible = True
                grdDetail.Columns("TotalTG").Visible = True
                grdDetail.Columns("IsShop").Visible = True
            End If

            End If
            txtItemCode.Text = ""
            ClearDetail()
    End Sub

#End Region

#Region "Text Changed"

    'Private Sub txtGoldPrice_TextChanged(sender As Object, e As EventArgs) Handles txtGoldPrice.TextChanged
    '    CalculateTotalAmount()
    'End Sub

    'Private Sub txtGemsPrice_TextChanged(sender As Object, e As EventArgs) Handles txtGemsPrice.TextChanged
    '    CalculateTotalAmount()
    'End Sub

    Private Sub txtDoneAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDoneAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDoneAmount_TextChanged(sender As Object, e As EventArgs) Handles txtDoneAmount.TextChanged
        If txtDoneAmount.Text = "" Then txtDoneAmount.Text = "0"
        CalculateTotalAmount()
    End Sub

    Private Sub txtAllTotalAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAllTotalAmt.TextChanged
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = 0
        txtAllNetAmt.Text = txtAllTotalAmt.Text
    End Sub

    Private Sub txtAllNetAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAllNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    'Private Sub txtAllNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAllNetAmt.TextChanged
    '    If (txtAllNetAmt.Text = "") Then
    '        txtAllNetAmt.Text = "0"
    '    End If
    '    If (txtAllNetAmt.Text <> "") And (txtAllTotalAmt.Text <> "") Then
    '        txtAddSub.Text = CStr(CLng(txtAllTotalAmt.Text) - CLng(txtAllNetAmt.Text))
    '        txtPaidAmt.Text = txtAllNetAmt.Text
    '    End If
    'End Sub

    Private Sub txtPaidAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPaidAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPaidAmt_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmt.TextChanged
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtPaidAmt.Text <> "" Then
            txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
        End If
    End Sub

    Private Sub txtItemCode_TextChanged(sender As Object, e As EventArgs) Handles txtItemCode.TextChanged

        If _CustomerID = "" Then
            MsgBox("Please Select Customer", MsgBoxStyle.Information, AppName)
        Else
            If _PurchaseDetailID = "" Then
                Dim dt As DataTable
                Dim cri As String = " And M.ItemCode = '" + txtItemCode.Text + "'"
                dt = objSaleItemInvoiceController.GetSalesInvoiceDataByCustomerIDAndItemCode(_CustomerID, cri)

                If dt.Rows.Count() > 0 Then
                    Dim GoldWeight As New CommonInfo.GoldWeightInfo
                    Dim objGQInfo As New CommonInfo.GoldQualityInfo

                    _SaleInvoiceDetailID = dt.Rows(0).Item("@SaleInvoiceDetailID")
                    _IsOrder = dt.Rows(0).Item("@IsOrder")
                    _ForSaleID = dt.Rows(0).Item("@ForSaleID")
                    txtItemCode.Text = dt.Rows(0).Item("ItemCode")
                    txtSaleAmount.Text = Format(Val(dt.Rows(0).Item("@TotalAmount")), "###,##0.##")
                    cboItemCategory.SelectedValue = dt.Rows(0).Item("@ItemCategoryID")
                    cboItemName.SelectedValue = dt.Rows(0).Item("@ItemNameID")
                    If dt.Rows(0).Item("Length") <> "" Then
                        txtWidthLength.Text = dt.Rows(0).Item("Length")
                    Else
                        txtWidthLength.Text = "-"
                    End If
                    txtQty.Text = 1
                    _IsFixPrice = dt.Rows(0).Item("@IsFixPrice")
                    chkIsFixPrice.Checked = dt.Rows(0).Item("@IsFixPrice")
                    cboGoldQuality.SelectedValue = dt.Rows(0).Item("@GoldQualityID")
                    objGQInfo = objGoldQualityController.GetGoldQuality(cboGoldQuality.SelectedValue)
                    With objGQInfo
                        _IsGram = .IsGramRate
                    End With
                    GoldQualityForTextChange()
                    GetCurrentPrice()

                    lblOriginalCode.Visible = True
                    lblOriginalCode.Text = IIf(IsDBNull(dt.Rows(0).Item("@OriginalCode")), "", dt.Rows(0).Item("@OriginalCode"))
                    lblOrgSaleRate.Text = Format(Val(dt.Rows(0).Item("@SalesRate")), "###,##0.##")
                    _SaleRate = dt.Rows(0).Item("@SalesRate")
                    _SGoldPrice = dt.Rows(0).Item("@GoldPrice")
                    _SGemsPrice = dt.Rows(0).Item("@GemsPrice")
                    _STotalCharges = dt.Rows(0).Item("@TotalCharges")

                    'GoldWeight.GoldTK = _TotalTK
                    'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtItemK.Text = CStr(dt.Rows(0).Item("ItemK"))
                    txtItemP.Text = CStr(dt.Rows(0).Item("ItemP"))
                    txtItemY.Text = CStr(dt.Rows(0).Item("ItemY"))
                    txtItemTG.Text = Format(dt.Rows(0).Item("@ItemTG"), "0.000")
                    _TotalTK = dt.Rows(0).Item("@ItemTK")
                    _TotalTG = dt.Rows(0).Item("@ItemTG")

                    'GoldWeight.GoldTK = _TotalGemTK
                    'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtGemsK.Text = CStr(dt.Rows(0).Item("GemsK"))
                    txtGemsP.Text = CStr(dt.Rows(0).Item("GemsP"))
                    txtGemsY.Text = CStr(dt.Rows(0).Item("GemsY"))
                    txtGemsG.Text = Format(dt.Rows(0).Item("@GemsTG"), "0.000")
                    _TotalGemTK = dt.Rows(0).Item("@GemsTK")
                    _TotalGemTG = dt.Rows(0).Item("@GemsTG")

                    'GoldWeight.GoldTK = _WasteTK
                    'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtSaleWasteK.Text = CStr(dt.Rows(0).Item("WasteK"))
                    txtSaleWasteP.Text = CStr(dt.Rows(0).Item("WasteP"))
                    txtSaleWasteY.Text = CStr(dt.Rows(0).Item("WasteY"))
                    txtSaleWasteG.Text = Format(dt.Rows(0).Item("@WasteTG"), "0.000")
                    _WasteTK = dt.Rows(0).Item("@WasteTK")
                    _WasteTG = dt.Rows(0).Item("@WasteTG")

                    'GoldWeight.GoldTK = _GoldTK
                    'GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtGoldK.Text = CStr(dt.Rows(0).Item("GoldK"))
                    txtGoldP.Text = CStr(dt.Rows(0).Item("GoldP"))
                    txtGoldY.Text = CStr(dt.Rows(0).Item("GoldY"))
                    txtGoldG.Text = Format(dt.Rows(0).Item("@GoldTG"), "0.000")
                    _GoldTK = dt.Rows(0).Item("@GoldTK")
                    _GoldTG = dt.Rows(0).Item("@GoldTG")
                    chkIsDiamond.Checked = dt.Rows(0).Item("@IsDiamond")
                    If dt.Rows(0).Item("@Photo") <> "" Then
                        Try
                            lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + dt.Rows(0).Item("@Photo"))
                            lblPhoto.Visible = False
                            Photo = dt.Rows(0).Item("@Photo")
                        Catch ex As Exception
                            lblItemImage.Image = Nothing
                            lblPhoto.Visible = True
                            Photo = ""
                        End Try
                    Else
                        Photo = ""
                        If Global_LogoPhoto <> "" Then
                            Try
                                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                                lblPhoto.Visible = False
                            Catch ex As Exception
                                lblItemImage.Image = Nothing
                                lblPhoto.Visible = True
                            End Try
                        Else
                            lblItemImage.Image = Nothing
                            lblPhoto.Visible = True
                        End If
                    End If

                    If Val(txtSaleAmount.Text) > 0 Then
                        chkPurchasePercent.Enabled = True
                    End If
                    CalculateGoldAmount()

                    If _IsOrder Then
                        _dtPurGem = _OrderInvoiceController.GetOrderInvoiceGemDataByOrderDetailID(_SaleInvoiceDetailID)
                    Else
                        _dtPurGem = objSaleItemInvoiceController.GetSalesInvoiceGemDataBySaleDetailID(_SaleInvoiceDetailID)
                    End If
                    grdGem.DataSource = _dtPurGem
                    CalculategrdTotalAmount()
                Else
                    ClearBarcodeData()
                End If
            End If
        End If
    End Sub

#End Region

#Region " Grid Events"
    Private Sub grdGem_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles grdGem.CellValidated
        If grdGem.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdGem.Columns(e.ColumnIndex).Name
                Case "PurchaseRate", "FixType", "QTY", "GemsK", "GemsP", "GemsY", "YOrCOrG", "Discount"

                    Dim Rate As Integer = 0
                    Dim Amount As Integer = 0
                    If (IsDBNull(grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value)) Then
                        grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value = 0
                    End If

                    If (IsDBNull(grdGem.Rows(e.RowIndex).Cells("Discount").Value)) Then
                        grdGem.Rows(e.RowIndex).Cells("Discount").Value = 0
                    End If

                    If Not IsDBNull(grdGem.Rows(e.RowIndex).Cells("FixType").Value) Then
                        If grdGem.Rows(e.RowIndex).Cells("FixType").Value = "Fix" Then
                            If Not (IsDBNull(grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value)) Then
                                If grdGem.Rows(e.RowIndex).Cells("Discount").Value > 0 Then
                                    grdGem.Rows(e.RowIndex).Cells("Amount").Value = grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value - ((grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value * grdGem.Rows(e.RowIndex).Cells("Discount").Value) / 100)
                                Else
                                    grdGem.Rows(e.RowIndex).Cells("Amount").Value = grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value
                                End If
                            Else
                                grdGem.Rows(e.RowIndex).Cells("Amount").Value = 0
                            End If
                        ElseIf grdGem.Rows(e.RowIndex).Cells("FixType").Value = "ByWeight" Then
                            Dim _Type As Boolean = False
                            Dim _Amount As Integer = 0

                            If Not (IsDBNull(grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value)) Then
                                If (IsDBNull(grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value)) Then
                                    _Type = True
                                ElseIf grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0" Then
                                    _Type = True
                                Else
                                    _Type = False
                                End If

                                If _Type = False Then
                                    _Amount = IIf(IsDBNull(grdGem.Rows(e.RowIndex).Cells("GemTW").Value) = True, 0, grdGem.Rows(e.RowIndex).Cells("GemTW").Value) * CLng(grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value)
                                    If grdGem.Rows(e.RowIndex).Cells("Discount").Value > 0 Then
                                        grdGem.Rows(e.RowIndex).Cells("Amount").Value = _Amount - ((_Amount * grdGem.Rows(e.RowIndex).Cells("Discount").Value) / 100)
                                    Else
                                        grdGem.Rows(e.RowIndex).Cells("Amount").Value = _Amount
                                    End If
                                Else
                                    _Amount = IIf(IsDBNull(grdGem.Rows(e.RowIndex).Cells("GemsTK").Value) = True, 0, grdGem.Rows(e.RowIndex).Cells("GemsTK").Value) * CLng(grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value)
                                    If grdGem.Rows(e.RowIndex).Cells("Discount").Value > 0 Then
                                        grdGem.Rows(e.RowIndex).Cells("Amount").Value = _Amount - ((_Amount * grdGem.Rows(e.RowIndex).Cells("Discount").Value) / 100)
                                    Else
                                        grdGem.Rows(e.RowIndex).Cells("Amount").Value = _Amount
                                    End If
                                End If
                            Else
                                grdGem.Rows(e.RowIndex).Cells("Amount").Value = 0
                            End If
                        Else

                            Dim _Amount As Integer = 0
                            If Not (IsDBNull(grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value)) Then
                                _Amount = CInt(IIf(IsDBNull(grdGem.Rows(e.RowIndex).Cells("QTY").Value) = True, 0, grdGem.Rows(e.RowIndex).Cells("QTY").Value) * CLng(grdGem.Rows(e.RowIndex).Cells("PurchaseRate").Value))
                                If grdGem.Rows(e.RowIndex).Cells("Discount").Value > 0 Then
                                    grdGem.Rows(e.RowIndex).Cells("Amount").Value = _Amount - ((_Amount * grdGem.Rows(e.RowIndex).Cells("Discount").Value) / 100)
                                Else
                                    grdGem.Rows(e.RowIndex).Cells("Amount").Value = _Amount
                                End If
                            Else
                                grdGem.Rows(e.RowIndex).Cells("Amount").Value = 0
                            End If
                        End If
                    End If

            End Select
            CalculategrdTotalAmount()
            CalculateTotalAmount()
        End If
    End Sub

    Private Sub grdGem_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdGem.CellValueChanged
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If grdGem.IsCurrentCellInEditMode = False Then Exit Sub
        If (e.RowIndex <> -1) Then
            If (grdGem.Columns(e.ColumnIndex).Name = "GemsK" Or grdGem.Columns(e.ColumnIndex).Name = "GemsP" Or grdGem.Columns(e.ColumnIndex).Name = "GemsY") Then 'F
                With grdGem
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

                        GoldWeight.WeightK = CInt(Val(grdGem.Rows(e.RowIndex).Cells("GemsK").FormattedValue))

                        If CInt(Val(grdGem.Rows(e.RowIndex).Cells("GemsP").FormattedValue)) >= 16 Then
                            MsgBox("GemP should not be greater than 15", MsgBoxStyle.Information, AppName)
                            .Rows(e.RowIndex).Cells("GemsP").Value() = "0"
                        End If
                        GoldWeight.WeightP = CInt(Val(grdGem.Rows(e.RowIndex).Cells("GemsP").FormattedValue))

                        If CDec(grdGem.Rows(e.RowIndex).Cells("GemsY").FormattedValue) >= Global_PToY Then
                            MsgBox("GemY should not be greater than" & (Global_PToY - 0.1), MsgBoxStyle.Information, AppName)
                            .Rows(e.RowIndex).Cells("GemsY").Value() = "0"
                        End If

                        GoldWeight.WeightY = System.Decimal.Truncate(Val(grdGem.Rows(e.RowIndex).Cells("GemsY").FormattedValue))
                        GoldWeight.WeightC = CDec(Val(grdGem.Rows(e.RowIndex).Cells("GemsY").FormattedValue)) - GoldWeight.WeightY

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _GemsTK = GoldWeight.GoldTK
                        GoldWeight.Gram = _GemsTK * Global_KyatToGram
                        _GemsTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("GemsTG").Value() = _GemsTG
                        .Rows(e.RowIndex).Cells("GemsTK").Value() = _GemsTK
                    End If
                End With

            End If 'end Gemskpyc

            If grdGem.Columns(e.ColumnIndex).Name = "YOrCOrG" Then  'For GemsWeight Yati,B,Karat
                Dim equivalent As Decimal
                Dim VarWeight As String
                Dim VarWeightY As Integer
                Dim VarWeightBCG As Decimal
                Dim VarWeightP As Decimal
                Dim TP As Decimal
                Dim TY As Decimal
                Dim TC As Decimal


                Dim IsValid As Boolean
                If IsDBNull(grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value) = True Then
                    grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                End If

                VarWeight = CStr(grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value)
                If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
                    MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                    grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                    grdGem.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                    grdGem.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                    grdGem.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"

                Else
                    If VarWeight.EndsWith("ct") Then
                        If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                            VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                            TC = CStr(VarWeightBCG)
                            If Global_IsCarat = 0 Or Global_IsCarat = 2 Then
                                grdGem.Rows(e.RowIndex).Cells("GemTW").Value = CStr(VarWeightBCG)
                            Else
                                grdGem.Rows(e.RowIndex).Cells("GemTW").Value = CStr(VarWeightBCG * 1.1)
                            End If
                            IsValid = True
                        Else
                            IsValid = False
                        End If

                    ElseIf VarWeight.EndsWith("R") Then
                        If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                            If VarWeight.IndexOf(".") = -1 Then
                                VarWeightY = CInt(LSet(VarWeight, Len(VarWeight) - 1))
                                'grdGem.Rows(e.RowIndex).Cells("GemTW").Value = VarWeightY
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = VarWeightY / equivalent
                                If Global_IsCarat = 2 Then
                                    grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TC
                                Else
                                    grdGem.Rows(e.RowIndex).Cells("GemTW").Value = VarWeightY
                                End If
                                IsValid = True
                            Else
                                VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = VarWeight / equivalent
                                If Global_IsCarat = 2 Then
                                    grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TC
                                Else
                                    grdGem.Rows(e.RowIndex).Cells("GemTW").Value = VarWeight
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
                                    'grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TC
                                    Else
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
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
                                'grdGem.Rows(e.RowIndex).Cells("GemTW").Value = VarWeightBCG
                                equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                TY = VarWeightY + (VarWeightBCG / equivalent)
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = TY / equivalent
                                If Global_IsCarat = 2 Then
                                    grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TC
                                Else
                                    grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
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
                                    ' grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TC
                                    Else
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
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
                                    'grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TC
                                    Else
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
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
                                    'grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TC
                                    Else
                                        grdGem.Rows(e.RowIndex).Cells("GemTW").Value = TY
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


                    If Not IsValid And grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value <> "0" Then
                        MsgBox("Stone Weight is Invalid!", MsgBoxStyle.Information, "Invalid Data")
                        grdGem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                        grdGem.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                        grdGem.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                        grdGem.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                    End If

                    equivalent = Global_GramToKarat '_ConverterController.GetMeasurement("Gram", "Karat")
                    Dim gram As Decimal = TC / equivalent
                    grdGem.Rows(e.RowIndex).Cells("GemsTG").Value = gram
                    equivalent = Global_KyatToGram '_ConverterController.GetMeasurement("Kyat", "Gram")
                    grdGem.Rows(e.RowIndex).Cells("GemsTK").Value = gram / equivalent
                    '
                    GoldWeight.GoldTK = gram / equivalent
                    objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    grdGem.Rows(e.RowIndex).Cells("GemsK").Value = GoldWeight.WeightK
                    grdGem.Rows(e.RowIndex).Cells("GemsP").Value = GoldWeight.WeightP
                    grdGem.Rows(e.RowIndex).Cells("GemsY").Value = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.0")
                End If
            End If
            CalculategrdGem()
            CalculateGoldWeight()
            CalculateGoldAmount()
        End If
    End Sub

    Private Sub grdDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetail.CellClick
        'Dim objPHeader As CommonInfo.PurchaseHeaderInfo
        Dim GoldWeight As New GoldWeightInfo

        If grdDetail.RowCount = 0 Then
            _IsUpdate = False
            Exit Sub
        End If
        If e.RowIndex <> -1 Then
            If grdDetail.Rows.Count > -1 Then

                If chkOnlyGem.Checked = False And chkDiamond.Checked = False Then
                    If grdDetail.Rows(e.RowIndex).Cells("ItemCategory").Value = "" Then
                        txtItemCode.Text = ""
                        ClearStock()
                        Exit Sub
                    End If

                    _PurchaseDetailID = grdDetail.Rows(e.RowIndex).Cells("PurchaseDetailID").Value
                    chkShopGold.Checked = grdDetail.Rows(e.RowIndex).Cells("IsShop").Value
                    lblOriginalCode.Visible = True
                    lblOriginalCode.Text = IIf(IsDBNull(grdDetail.Rows(e.RowIndex).Cells("OriginalCode").Value), "", grdDetail.Rows(e.RowIndex).Cells("OriginalCode").Value)

                    If chkShopGold.Checked Then
                        chkIsFixPrice.Visible = True
                        lblSaleAmount.Visible = True
                        txtItemCode.Visible = True
                        txtSaleAmount.Visible = True
                        btnBarcodeSearch.Visible = True
                        chkIsDiamond.Visible = True
                        lblOrgSaleRate.Visible = True
                        lblOriginalCode.Visible = True
                    Else
                        chkIsFixPrice.Visible = False
                        lblSaleAmount.Visible = False
                        txtItemCode.Visible = False
                        txtSaleAmount.Visible = False
                        btnBarcodeSearch.Visible = False
                        chkIsDiamond.Visible = False
                        lblOrgSaleRate.Visible = False
                        lblOriginalCode.Visible = False
                    End If

                    _SaleInvoiceDetailID = grdDetail.Rows(e.RowIndex).Cells("SaleInvoiceDetailID").Value
                    '_ConsignmentSaleItemID = grdDetail.Rows(e.RowIndex).Cells("ConsignmentSaleItemID").Value
                    _ForSaleID = grdDetail.Rows(e.RowIndex).Cells("ForSaleID").Value
                    _IsOrder = grdDetail.Rows(e.RowIndex).Cells("IsOrder").Value
                    txtItemCode.Text = IIf(grdDetail.Rows(e.RowIndex).Cells("BarcodeNo").Value = "-", "", grdDetail.Rows(e.RowIndex).Cells("BarcodeNo").Value)
                    chkIsFixPrice.Checked = grdDetail.Rows(e.RowIndex).Cells("IsFixPrice").Value
                    Dim dtItem As DataTable = objSaleItemInvoiceController.GetSalesInvoiceDataSaleDetailID("AND M.[@SaleInvoiceDetailID]='" & _SaleInvoiceDetailID & "'")
                    If dtItem.Rows.Count > 0 Then
                        _SaleRate = dtItem.Rows(0).Item("SalesRate")
                        lblOrgSaleRate.Text = _SaleRate
                        _STotalCharges = dtItem.Rows(0).Item("TotalCharges")
                    End If

                    cboItemCategory.SelectedIndex = -1
                    cboItemCategory.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("ItemCategoryID").Value
                    cboItemName.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("ItemNameID").Value
                    cboGoldQuality.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("GoldQualityID").Value
                    txtWidthLength.Text = grdDetail.Rows(e.RowIndex).Cells("Length").Value
                    txtQty.Text = grdDetail.Rows(e.RowIndex).Cells("QTY").Value

                    GoldWeight.GoldTK = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalTK").Value)
                    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtItemK.Text = CStr(GoldWeight.WeightK)
                    txtItemP.Text = CStr(GoldWeight.WeightP)
                    If NumberFormat = 1 Then
                        txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    Else
                        txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                    End If
                    'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    txtItemTG.Text = Format(grdDetail.Rows(e.RowIndex).Cells("TotalTG").Value, "0.000")
                    _TotalTK = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalTK").Value)
                    _TotalTG = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalTG").Value)

                    GoldWeight.GoldTK = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTK").Value)
                    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtGemsK.Text = CStr(GoldWeight.WeightK)
                    txtGemsP.Text = CStr(GoldWeight.WeightP)
                    txtGemsY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    txtGemsG.Text = Format(grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value, "0.000")
                    _TotalGemTK = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTK").Value)
                    _TotalGemTG = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value)

                    GoldWeight.GoldTK = CDec(grdDetail.Rows(e.RowIndex).Cells("WasteTK").Value)
                    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtSaleWasteK.Text = CStr(GoldWeight.WeightK)
                    txtSaleWasteP.Text = CStr(GoldWeight.WeightP)
                    If NumberFormat = 1 Then
                        txtSaleWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    Else
                        txtSaleWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                    End If
                    'txtSaleWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    txtSaleWasteG.Text = Format(grdDetail.Rows(e.RowIndex).Cells("WasteTG").Value, "0.000")
                    _WasteTK = CDec(grdDetail.Rows(e.RowIndex).Cells("WasteTK").Value)
                    _WasteTG = CDec(grdDetail.Rows(e.RowIndex).Cells("WasteTG").Value)

                    GoldWeight.GoldTK = CDec(grdDetail.Rows(e.RowIndex).Cells("PWasteTK").Value)
                    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtPWasteK.Text = CStr(GoldWeight.WeightK)
                    txtPWasteP.Text = CStr(GoldWeight.WeightP)
                    If NumberFormat = 1 Then
                        txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    Else
                        txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                    End If
                    'txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    txtPWasteG.Text = Format(grdDetail.Rows(e.RowIndex).Cells("PWasteTG").Value, "0.000")
                    _PWasteTK = CDec(grdDetail.Rows(e.RowIndex).Cells("PWasteTK").Value)
                    _PWasteTG = CDec(grdDetail.Rows(e.RowIndex).Cells("PWasteTG").Value)

                    GoldWeight.GoldTK = CDec(grdDetail.Rows(e.RowIndex).Cells("GoldTK").Value)
                    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtGoldK.Text = CStr(GoldWeight.WeightK)
                    txtGoldP.Text = CStr(GoldWeight.WeightP)
                    If NumberFormat = 1 Then
                        txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    Else
                        txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                    End If
                    'txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    txtGoldG.Text = Format(CDec(txtItemTG.Text) - CDec(txtGemsG.Text), "0.000")
                    _GoldTK = CDec(grdDetail.Rows(e.RowIndex).Cells("GoldTK").Value)
                    _GoldTG = CDec(grdDetail.Rows(e.RowIndex).Cells("GoldTG").Value)

                    chkIsDamage.Checked = grdDetail.Rows(e.RowIndex).Cells("IsDamage").Value
                    chkIsExchange.Checked = grdDetail.Rows(e.RowIndex).Cells("IsChange").Value
                    _CurrentSaleRate = grdDetail.Rows(e.RowIndex).Cells("SaleRate").Value
                    txtSaleRate.Text = grdDetail.Rows(e.RowIndex).Cells("SaleRate").Value
                    txtCurPrice.Text = grdDetail.Rows(e.RowIndex).Cells("CurrentPrice").Value
                    txtSaleAmount.Text = Format(Val(grdDetail.Rows(e.RowIndex).Cells("OldSaleAmount").Value), "###,##0.##")
                    If Val(txtSaleAmount.Text) > 0 Then
                        chkPurchasePercent.Enabled = True
                    End If
                    chkDone.Checked = grdDetail.Rows(e.RowIndex).Cells("IsDone").Value
                    If chkDone.Checked Then
                        txtDoneAmount.Enabled = True
                    Else
                        txtDoneAmount.Enabled = False
                    End If
                    txtDoneAmount.Text = Format(Val(grdDetail.Rows(e.RowIndex).Cells("DoneAmount").Value), "###,##0.##")
                    chkPurchasePercent.Checked = grdDetail.Rows(e.RowIndex).Cells("IsSalePercent").Value
                    If chkPurchasePercent.Checked Then
                        txtPurchasePercent.Enabled = True
                        txtPurchcasePercentAmount.Enabled = True
                    Else
                        txtPurchasePercent.Enabled = False
                        txtPurchcasePercentAmount.Enabled = False
                    End If
                    '_ConsignmentSaleItemID = grdDetail.Rows(e.RowIndex).Cells("ConsignmentSaleItemID").Value
                    txtPurchasePercent.Text = grdDetail.Rows(e.RowIndex).Cells("SalePercent").Value
                    txtPurchcasePercentAmount.Text = Format(Val(grdDetail.Rows(e.RowIndex).Cells("SalePercentAmount").Value), "###,##0.##")
                    txtGoldPrice.Text = Format(Val(grdDetail.CurrentRow.Cells("GoldPrice").Value), "###,##0.##")
                    txtGemsPrice.Text = Format(Val(grdDetail.CurrentRow.Cells("GemsPrice").Value), "###,##0.##")
                    txtTotalAmt.Text = Format(Val(grdDetail.CurrentRow.Cells("TotalAmount").Value), "###,##0.##")
                    txtItemAddOrSub.Text = Format(Val(grdDetail.CurrentRow.Cells("AddSub").Value), "###,##0.##")
                    txtNetAmount.Text = Format(Val(CLng(txtTotalAmt.Text) - CLng(txtItemAddOrSub.Text)), "###,##0.##")
                    chkIsDiamond.Checked = grdDetail.CurrentRow.Cells("IsDiamond").Value

                    If grdDetail.CurrentRow.Cells("Photo").Value <> "" Then
                        Try
                            lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + grdDetail.CurrentRow.Cells("Photo").Value)
                            lblPhoto.Visible = False
                            Photo = grdDetail.CurrentRow.Cells("Photo").Value
                        Catch ex As Exception
                            lblItemImage.Image = Nothing
                            lblPhoto.Visible = True
                            Photo = ""
                        End Try
                    Else
                        Photo = ""
                        If Global_LogoPhoto <> "" Then
                            Try
                                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                                lblPhoto.Visible = False
                            Catch ex As Exception
                                lblItemImage.Image = Nothing
                                lblPhoto.Visible = True
                            End Try
                        Else
                            lblItemImage.Image = Nothing
                            lblPhoto.Visible = True
                        End If
                    End If

                    _dtPurGem.Rows.Clear()
                    If _dtStone.Rows.Count Then
                        For i As Integer = 0 To _dtStone.Rows.Count - 1
                            If Not IsDBNull(_dtStone.Rows(i).Item("PurchaseDetailID")) Then
                                If _dtStone.Rows(i).Item("PurchaseDetailID") = _PurchaseDetailID Then
                                    Dim drItem As DataRow
                                    drItem = _dtPurGem.NewRow
                                    drItem("PurchaseGemID") = _dtStone.Rows(i).Item("PurchaseGemID")
                                    drItem("PurchaseDetailID") = _dtStone.Rows(i).Item("PurchaseDetailID")
                                    drItem("GemsCategoryID") = _dtStone.Rows(i).Item("GemsCategoryID")
                                    drItem("GemsName") = _dtStone.Rows(i).Item("GemsName")
                                    drItem("GemsTK") = _dtStone.Rows(i).Item("GemsTK")
                                    drItem("GemsTG") = _dtStone.Rows(i).Item("GemsTG")
                                    drItem("YOrCOrG") = _dtStone.Rows(i).Item("YOrCOrG")                                    '
                                    drItem("GemsK") = _dtStone.Rows(i).Item("GemsK")
                                    drItem("GemsP") = _dtStone.Rows(i).Item("GemsP")
                                    drItem("GemsY") = _dtStone.Rows(i).Item("GemsY")                                    '
                                    drItem("GemTW") = _dtStone.Rows(i).Item("GemTW")
                                    drItem("QTY") = _dtStone.Rows(i).Item("QTY")
                                    drItem("FixType") = _dtStone.Rows(i).Item("FixType")
                                    drItem("Discount") = _dtStone.Rows(i).Item("Discount")
                                    drItem("PurchaseRate") = _dtStone.Rows(i).Item("PurchaseRate")
                                    drItem("Amount") = _dtStone.Rows(i).Item("Amount")
                                    _dtPurGem.Rows.Add(drItem)
                                End If
                            End If
                        Next
                        grdGem.DataSource = _dtPurGem
                        If _ForSaleID <> "" And chkIsFixPrice.Checked = False Then
                            CalculateSaleGoldAmount()
                            CalculategrdTotalAmount()
                        End If
                    End If
                    btnAdd.Text = "Update"
                    _IsUpdate = True
                ElseIf chkDiamond.Checked = True Then
                    If grdDetail.Rows(e.RowIndex).Cells("GemsCategory").Value = "" Then
                        txtDItemCode.Text = ""
                        ClearStock()
                        Exit Sub
                    End If

                    _PurchaseDetailID = grdDetail.Rows(e.RowIndex).Cells("PurchaseDetailID").Value
                    chkShopDiamond.Checked = grdDetail.Rows(e.RowIndex).Cells("IsShop").Value
                    lblDOriginalCode.Visible = True
                    lblDOriginalCode.Text = IIf(IsDBNull(grdDetail.Rows(e.RowIndex).Cells("OriginalCode").Value), "", grdDetail.Rows(e.RowIndex).Cells("OriginalCode").Value)

                    If chkShopDiamond.Checked Then
                        chkDIsFixPrice.Visible = True
                        lblDSaleAmount.Visible = True
                        txtDItemCode.Visible = True
                        txtDSaleAmount.Visible = True
                        btnDBarcodeSearch.Visible = True
                        'chkIsDiamond.Visible = True
                        lblDOrgSaleRate.Visible = True
                        lblDOriginalCode.Visible = True
                    Else
                        chkDIsFixPrice.Visible = False
                        lblDSaleAmount.Visible = False
                        txtDItemCode.Visible = False
                        txtDSaleAmount.Visible = False
                        btnDBarcodeSearch.Visible = False
                        'chkdIsDiamond.Visible = False
                        lblDOrgSaleRate.Visible = False
                        lblDOriginalCode.Visible = False
                    End If

                    _SaleLooseDiamondDetailID = grdDetail.Rows(e.RowIndex).Cells("SaleLooseDiamondDetailID").Value
                    '_ConsignmentSaleItemID = grdDetail.Rows(e.RowIndex).Cells("ConsignmentSaleItemID").Value
                    _ForSaleID = grdDetail.Rows(e.RowIndex).Cells("ForSaleID").Value
                    _IsOrder = False
                    txtDItemCode.Text = IIf(grdDetail.Rows(e.RowIndex).Cells("BarcodeNo").Value = "-", "", grdDetail.Rows(e.RowIndex).Cells("BarcodeNo").Value)
                    chkDIsFixPrice.Checked = grdDetail.Rows(e.RowIndex).Cells("IsFixPrice").Value
                    Dim dtItem As DataTable = objSaleLooseDiamondController.GetSaleLooseDiamondDetailByDetailID(_SaleLooseDiamondDetailID)
                    If dtItem.Rows.Count > 0 Then
                        _SaleRate = dtItem.Rows(0).Item("SalesRate")
                        lblDOrgSaleRate.Text = _SaleRate
                        _STotalCharges = dtItem.Rows(0).Item("DesignCharges") + dtItem.Rows(0).Item("WhiteCharges") + dtItem.Rows(0).Item("PlatingCharges") + dtItem.Rows(0).Item("MountingCharges")
                    End If

                    cboDCategory.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("PGemsCategoryID").Value
                    txtDescription.Text = grdDetail.Rows(e.RowIndex).Cells("PGemsName").Value
                    cboDColor.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("Color").Value
                    cboDShape.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("Shape").Value
                    cboDClarity.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("Clarity").Value
                    txtDQty.Text = grdDetail.Rows(e.RowIndex).Cells("QTY").Value
                    txtDRBP.Text = grdDetail.Rows(e.RowIndex).Cells("YOrCOrG").Value
                    txtDGram.Text = Format(grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value, "0.000")
                    _ItemTK = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTK").Value)
                    _ItemTG = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value)

                    GoldWeight.GoldTK = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTK").Value)
                    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtGemsK.Text = CStr(GoldWeight.WeightK)
                    txtGemsP.Text = CStr(GoldWeight.WeightP)
                    txtGemsY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    txtGemsG.Text = Format(grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value, "0.000")
                    _TotalGemTK = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTK").Value)
                    _TotalGemTG = CDec(grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value)

                    chkIsDamage.Checked = False
                    chkDIsChange.Checked = grdDetail.Rows(e.RowIndex).Cells("IsChange").Value
                    _DCurrentSaleRate = grdDetail.Rows(e.RowIndex).Cells("SaleRate").Value
                    txtDSaleRate.Text = grdDetail.Rows(e.RowIndex).Cells("SaleRate").Value
                    txtDCurPrice.Text = grdDetail.Rows(e.RowIndex).Cells("CurrentPrice").Value
                    txtDSaleAmount.Text = Format(Val(grdDetail.Rows(e.RowIndex).Cells("OldSaleAmount").Value), "###,##0.##")
                    If Val(txtDSaleAmount.Text) > 0 Then
                        chkDPurchasePercent.Enabled = True
                    End If
                    chkDIsDone.Checked = grdDetail.Rows(e.RowIndex).Cells("IsDone").Value
                    If chkDIsDone.Checked Then
                        txtDDonePrice.Enabled = True
                    Else
                        txtDDonePrice.Enabled = False
                    End If
                    txtDDonePrice.Text = Format(Val(grdDetail.Rows(e.RowIndex).Cells("DoneAmount").Value), "###,##0.##")
                    chkDPurchasePercent.Checked = grdDetail.Rows(e.RowIndex).Cells("IsSalePercent").Value
                    If chkDPurchasePercent.Checked Then
                        txtDPurchasePercent.Enabled = True
                        txtDPercentPrice.Enabled = True
                    Else
                        txtDPurchasePercent.Enabled = False
                        txtDPercentPrice.Enabled = False
                    End If
                    '_ConsignmentSaleItemID = grdDetail.Rows(e.RowIndex).Cells("ConsignmentSaleItemID").Value
                    txtDPurchasePercent.Text = grdDetail.Rows(e.RowIndex).Cells("SalePercent").Value
                    txtDPercentPrice.Text = Format(Val(grdDetail.Rows(e.RowIndex).Cells("SalePercentAmount").Value), "###,##0.##")
                    txtDiamondPrice.Text = Format(Val(grdDetail.CurrentRow.Cells("GemsPrice").Value), "###,##0.##")
                    txtDTotalAmount.Text = Format(Val(grdDetail.CurrentRow.Cells("TotalAmount").Value), "###,##0.##")
                    txtDAddOrSub.Text = Format(Val(grdDetail.CurrentRow.Cells("AddSub").Value), "###,##0.##")
                    txtDNetAmount.Text = Format(Val(CLng(txtDTotalAmount.Text) - CLng(txtDAddOrSub.Text)), "###,##0.##")
                    chkIsDiamond.Checked = False

                    If grdDetail.CurrentRow.Cells("Photo").Value <> "" Then
                        Try
                            lblDItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + grdDetail.CurrentRow.Cells("Photo").Value)
                            lblDPhoto.Visible = False
                            Photo = grdDetail.CurrentRow.Cells("Photo").Value
                        Catch ex As Exception
                            lblDItemImage.Image = Nothing
                            lblDPhoto.Visible = True
                            Photo = ""
                        End Try
                    Else
                        Photo = ""
                        If Global_LogoPhoto <> "" Then
                            Try
                                lblDItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                                lblDPhoto.Visible = False
                            Catch ex As Exception
                                lblDItemImage.Image = Nothing
                                lblDPhoto.Visible = True
                            End Try
                        Else
                            lblDItemImage.Image = Nothing
                            lblDPhoto.Visible = True
                        End If
                    End If
                    _Carat = _ItemTG * Global_GramToKarat
                    btnDAdd.Text = "Update"
                    _IsUpdate = True
                Else
                    _PurchaseDetailID = grdDetail.Rows(e.RowIndex).Cells("PurchaseDetailID").Value
                    _PurchaseHeaderID = grdDetail.Rows(e.RowIndex).Cells("PurchaseHeaderID").Value
                    If grdDetail.Rows(e.RowIndex).Cells("GemsCategory").Value = "" Then
                        ClearGem()
                        Exit Sub
                    End If

                    cboGemCategory.SelectedValue = grdDetail.Rows(e.RowIndex).Cells("ItemCategoryID").Value
                    cboGemCategory.Text = grdDetail.Rows(e.RowIndex).Cells("GemsCategory").Value
                    txtGemName.Text = grdDetail.Rows(e.RowIndex).Cells("GemsName").Value

                    txtGemQTY.Text = grdDetail.Rows(e.RowIndex).Cells("QTY").Value
                    txtUnitPrice.Text = grdDetail.Rows(e.RowIndex).Cells("CurrentPrice").Value
                    If grdDetail.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0" Then
                        radKyat.Checked = True
                        radRBP.Checked = False
                    Else
                        radKyat.Checked = False
                        radRBP.Checked = True
                    End If
                    txtRBP.Text = grdDetail.Rows(e.RowIndex).Cells("YOrCOrG").Value
                    GemTK = grdDetail.Rows(e.RowIndex).Cells("TotalGemTK").Value
                    GemTG = grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value
                    GemTW = grdDetail.Rows(e.RowIndex).Cells("GemTW").Value

                    GoldWeight.GoldTK = GemTK
                    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    txtGemK.Text = CStr(GoldWeight.WeightK)
                    txtGemP.Text = CStr(GoldWeight.WeightP)
                    txtGemY.Text = CStr(GoldWeight.WeightY + Math.Round(GoldWeight.WeightC, 1))
                    txtGemG.Text = Format(grdDetail.Rows(e.RowIndex).Cells("TotalGemTG").Value, "0.000")

                    cboFixType.Text = grdDetail.Rows(e.RowIndex).Cells("FixType").Value
                    txtTotalAmount.Text = grdDetail.Rows(e.RowIndex).Cells("TotalAmount").Value
                    txtGemAddSub.Text = grdDetail.CurrentRow.Cells("AddSub").Value
                    txtGemNetAmount.Text = CStr(CLng(txtTotalAmount.Text) - CLng(txtGemAddSub.Text))
                    btnGemAdd.Text = "Update"
                    _IsUpdate = True
                End If
            End If
        End If
    End Sub

    Private Sub grdDetail_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdDetail.RowsRemoved
        Dim row As DataRow
        Dim j As Integer = _dtStone.Rows.Count() - 1
        While j >= 0
            row = _dtStone.Rows(j)
            If row.Item("PurchaseDetailID") = _PurchaseDetailID Then
                _dtStone.Rows.Remove(row)
            End If
            j = j - 1
        End While

        For i As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(i).IsNewRow Then
                grdDetail.Rows(i).Cells("SNo").Value = i + 1
            End If
        Next

        txtItemCode.Text = ""
        ClearDetail()
        CalculategrdTotalAmount()
        CalculateAllDetailTotalAmount()
        CalculategrAlldTotalWeight()
    End Sub

#End Region
    Private Sub radRBP_CheckedChanged(sender As Object, e As EventArgs) Handles radRBP.CheckedChanged
        If radRBP.Checked Then
            txtGemK.Text = "0"
            txtGemP.Text = "0"
            txtGemY.Text = "0.0"
            txtRBP.ReadOnly = False
            txtRBP.BackColor = Color.White
            txtGemK.ReadOnly = True
            txtGemP.ReadOnly = True
            txtGemY.ReadOnly = True
            txtGemK.BackColor = Color.Linen
            txtGemP.BackColor = Color.Linen
            txtGemY.BackColor = Color.Linen
            radKyat.TabStop = True
            txtRBP.TabIndex = "1"
            radKyat.TabIndex = "2"
            txtGemK.TabStop = False
            txtGemP.TabStop = False
            txtGemY.TabStop = False
        End If
        CalculateGemAmount()
    End Sub

    Private Sub radKyat_CheckedChanged(sender As Object, e As EventArgs) Handles radKyat.CheckedChanged
        If radKyat.Checked Then
            txtRBP.Text = "0"
            txtGemK.Text = "0"
            txtGemP.Text = "0"
            txtGemY.Text = "0.0"
            txtRBP.ReadOnly = True
            txtRBP.BackColor = Color.Linen
            txtGemK.ReadOnly = False
            txtGemP.ReadOnly = False
            txtGemY.ReadOnly = False
            txtGemK.BackColor = Color.White
            txtGemP.BackColor = Color.White
            txtGemY.BackColor = Color.White
            txtGemK.TabStop = True
            txtGemP.TabStop = True
            txtGemY.TabStop = True
            txtGemK.TabIndex = "2"
            txtGemP.TabIndex = "3"
            txtGemY.TabIndex = "4"
        End If
        CalculateGemAmount()
    End Sub




    Private Sub txtRBP_Validated(sender As Object, e As EventArgs) Handles txtRBP.Validated
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim equivalent As Decimal
        Dim VarWeight As String
        Dim VarWeightY As Integer
        Dim VarWeightBCG As Decimal
        Dim VarWeightP As Decimal
        Dim TP As Decimal
        Dim TY As Decimal
        Dim TC As Decimal


        Dim IsValid As Boolean
        If txtRBP.Text <> "" Then
            VarWeight = txtRBP.Text
            'If VarWeight = "0" Then
            '    Exit Sub
            'End If
            If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
                MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                txtRBP.Text = "0"
                txtGemK.Text = "0"
                txtGemP.Text = "0"
                txtGemY.Text = "0.0"
                txtRBP.Focus()
                'ElseIf VarWeight.StartsWith("ct") Or VarWeight.EndsWith("G") Or VarWeight.StartsWith("R") Or VarWeight.StartsWith("B") Or VarWeight.StartsWith("P") Then
                '    MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                '    txtRBP.Text = "0"
                '    txtGemK.Text = "0"
                '    txtGemP.Text = "0"
                '    txtGemY.Text = "0.0"
                '    txtRBP.Focus()
            Else
                If VarWeight.EndsWith("ct") Then
                    If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                        VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                        'GemTW = CStr(VarWeightBCG * 1.1)    'CStr(VarWeightBCG) ' Notes: For Karat,multiply 1.1 
                        'TC = CStr(VarWeightBCG)
                        TC = CStr(VarWeightBCG)
                        If Global_IsCarat = 0 Or Global_IsCarat = 2 Then
                            GemTW = CStr(VarWeightBCG)
                        Else
                            GemTW = CStr(VarWeightBCG * 1.1)
                        End If
                        IsValid = True
                    Else
                        IsValid = False
                    End If

                ElseIf VarWeight.EndsWith("R") Then
                    If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                        If VarWeight.IndexOf(".") = -1 Then
                            VarWeightY = CInt(LSet(VarWeight, Len(VarWeight) - 1))
                            'GemTW = VarWeightY
                            equivalent = Global_KaratToYati ' _ConverterController.GetMeasurement("Karat", "Yati")
                            TC = VarWeightY / equivalent
                            If Global_IsCarat = 2 Then
                                GemTW = TC
                            Else
                                GemTW = VarWeightY
                            End If
                            IsValid = True
                        Else
                            VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                            'GemTW = VarWeight
                            equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                            TC = VarWeight / equivalent
                            If Global_IsCarat = 2 Then
                                GemTW = TC
                            Else
                                GemTW = VarWeight
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
                                equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                TY = VarWeightY + (VarWeightBCG / equivalent)
                                'GemTW = TY
                                equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                TC = TY / equivalent
                                If Global_IsCarat = 2 Then
                                    GemTW = TC
                                Else
                                    GemTW = TY
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
                            'grdGem.Rows(e.RowIndex).Cells("GemTW").Value = VarWeightBCG
                            equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")

                            TY = VarWeightY + (VarWeightBCG / equivalent)
                            ' GemTW = TY
                            equivalent = Global_KaratToYati ' _ConverterController.GetMeasurement("Karat", "Yati")
                            TC = TY / equivalent
                            If Global_IsCarat = 2 Then
                                GemTW = TC
                            Else
                                GemTW = TY
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
                                equivalent = Global_BToP '_ConverterController.GetMeasurement("B", "P")
                                TP = VarWeightBCG + (VarWeightP / equivalent)
                                equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                TY = VarWeightY + (TP / equivalent)
                                'GemTW = TY
                                equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                TC = TY / equivalent
                                If Global_IsCarat = 2 Then
                                    GemTW = TC
                                Else
                                    GemTW = TY
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
                                equivalent = Global_BToP '_ConverterController.GetMeasurement("B", "P")
                                TP = VarWeightBCG + (VarWeightP / equivalent)
                                equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                TY = VarWeightY + (TP / equivalent)
                                'GemTW = TY
                                equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                TC = TY / equivalent
                                If Global_IsCarat = 2 Then
                                    GemTW = TC
                                Else
                                    GemTW = TY
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
                                equivalent = Global_BToP '_ConverterController.GetMeasurement("B", "P")
                                TP = VarWeightBCG + (VarWeightP / equivalent)
                                equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                TY = VarWeightY + (TP / equivalent)
                                'GemTW = TY
                                equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                TC = TY / equivalent
                                If Global_IsCarat = 2 Then
                                    GemTW = TC
                                Else
                                    GemTW = TY
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

                If Not IsValid And txtRBP.Text <> "0" Then
                    MsgBox("Stone Weight is Invalid!", MsgBoxStyle.Information, "Invalid Data")
                    txtRBP.Text = "0"
                    txtGemK.Text = "0"
                    txtGemP.Text = "0"
                    txtGemY.Text = "0.0"
                    txtRBP.Focus()
                End If

                equivalent = Global_GramToKarat '_ConverterController.GetMeasurement("Gram", "Karat")
                Dim gram As Decimal = TC / equivalent
                GemTG = gram
                equivalent = Global_KyatToGram '_ConverterController.GetMeasurement("Kyat", "Gram")
                GemTK = gram / equivalent
                '
                GoldWeight.GoldTK = gram / equivalent
                objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtGemK.Text = GoldWeight.WeightK
                txtGemP.Text = GoldWeight.WeightP
                txtGemY.Text = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.0")
                txtGemG.Text = Format(gram, "0.000")
            End If
        End If
        CalculateGemAmount()
    End Sub

    Private Sub txtGemQTY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGemQTY.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtUnitPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUnitPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtUnitPrice_TextChanged(sender As Object, e As EventArgs) Handles txtUnitPrice.TextChanged
        CalculateGemAmount()
    End Sub
    Private Sub CalculateGemAmount()
        Dim _Type As Boolean = False
        If txtUnitPrice.Text = "" Then txtUnitPrice.Text = "0"
        If txtGemQTY.Text = "" Then txtGemQTY.Text = "0"

        If cboFixType.Text <> "" Or cboFixType.SelectedIndex <> -1 Then
            If cboFixType.Text = "Fix" Then
                txtTotalAmount.Text = CInt(txtUnitPrice.Text)
            ElseIf cboFixType.Text = "ByWeight" Then

                If (txtRBP.Text = "0") Then
                    _Type = True
                Else
                    _Type = False
                End If

                If _Type = False Then
                    txtTotalAmount.Text = CInt(GemTW * txtUnitPrice.Text)
                Else
                    txtTotalAmount.Text = CInt(GemTK * txtUnitPrice.Text)
                End If
            ElseIf cboFixType.Text = "ByQty" Then
                txtTotalAmount.Text = CInt(txtGemQTY.Text * txtUnitPrice.Text)
            End If
        Else
            txtTotalAmount.Text = "0"
        End If
        txtGemAddSub.Text = "0"
        txtGemNetAmount.Text = txtTotalAmount.Text
    End Sub

    Private Sub txtGemQTY_TextChanged(sender As Object, e As EventArgs) Handles txtGemQTY.TextChanged
        CalculateGemAmount()
    End Sub

    Private Sub cboFixType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFixType.SelectedValueChanged
        CalculateGemAmount()
    End Sub

    Private Sub txtGemY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGemY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtGemY_TextChanged(sender As Object, e As EventArgs) Handles txtGemY.TextChanged
        If txtGemY.Text = "" Then txtGemY.Text = "0.0"

        If Val(txtGemY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtGemY.Text = "0.0"
            txtGemY.SelectAll()
        End If

        If Val(txtGemY.Text.Trim) >= 0 Then
            CalculateItemWeightForKPY()
        End If
        CalculateGemAmount()
    End Sub

    Private Sub txtGemP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGemP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtGemP_TextChanged(sender As Object, e As EventArgs) Handles txtGemP.TextChanged
        If txtGemP.Text = "" Then txtGemP.Text = "0"
        If Val(txtGemP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtGemP.Text = 0
            txtGemP.SelectAll()
        End If
        If Val(txtGemP.Text.Trim) >= 0 Then
            CalculateItemWeightForKPY()
        End If
        CalculateGemAmount()
    End Sub

    Private Sub txtGemK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub


    Private Sub txtGemK_TextChanged(sender As Object, e As EventArgs) Handles txtGemK.TextChanged
        If txtGemK.Text = "" Then txtGemK.Text = "0"

        If Val(txtGemK.Text.Trim) >= 0 Then
            CalculateItemWeightForKPY()
        End If
        CalculateGemAmount()
    End Sub
    Private Sub CalculateItemWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtGemK.Text <> "" And txtGemP.Text <> "" And txtGemY.Text <> "" Then

            GoldWeight.WeightK = CInt(txtGemK.Text)
            GoldWeight.WeightP = CInt(txtGemP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtGemY.Text)
            GoldWeight.WeightC = CDec(txtGemY.Text) - GoldWeight.WeightY
            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
            GemTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
            GemTG = GoldWeight.Gram
            txtGemG.Text = Format(GemTG, "0.000")
        Else
            GemTK = 0.0
            GemTG = 0.0
            'txtGemK.Text = "0"
            'txtGemP.Text = "0"
            'txtGemY.Text = "0"
        End If
    End Sub

    Private Sub btnGemClear_Click(sender As Object, e As EventArgs) Handles btnGemClear.Click
        txtItemCode.Text = ""
        ClearDetail()
    End Sub

    Private Sub btnGemAdd_Click(sender As Object, e As EventArgs) Handles btnGemAdd.Click
        If cboGemCategory.Text = "" Then
            MsgBox("Please Select Gem Category!", MsgBoxStyle.Information, "Data Require!")
            cboGemCategory.Focus()
            Exit Sub
        End If
        If radRBP.Checked Then
            If txtRBP.Text = "0" Then
                MsgBox("Please Check Gem Weight!", MsgBoxStyle.Information, "Data Require!")
                txtRBP.Focus()
                Exit Sub
            End If
        End If

        If radKyat.Checked Then
            If GemTK = 0 Then
                MsgBox("Please Check Gem Weight!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If
        End If

        If txtGemQTY.Text = "0" Then
            MsgBox("Please Check Gem QTY!", MsgBoxStyle.Information, "Data Require!")
            txtGemQTY.Focus()
            Exit Sub
        End If

        If txtUnitPrice.Text = "0" Then
            MsgBox("Please Check Gem Price!", MsgBoxStyle.Information, "Data Require!")
            txtUnitPrice.Focus()
            Exit Sub
        End If

        If cboFixType.Text = "" Then
            MsgBox("Please Check Gem Type!", MsgBoxStyle.Information, "Data Require!")
            cboFixType.Focus()
            Exit Sub
        End If

        If _IsUpdate Then
            UpdateItem(_PurchaseDetailID, _dtPurGem)
            CalculateAllDetailTotalAmount()
            txtItemCode.Text = ""
            ClearDetail()
        Else
            If btnGemAdd.Text = "Add" Then
                _PurchaseDetailID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseDetail, EnumSetting.GenerateKeyType.PurchaseDetail.ToString, dtpPurchaseDate.Value)
                InsertItem(_PurchaseDetailID, _dtPurGem)
                CalculateAllDetailTotalAmount()
            End If
            txtItemCode.Text = ""
            ClearDetail()
        End If
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtItemK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub

    Private Sub txtItemP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub

    Private Sub txtItemY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
        _WeightType = "Kyat"
    End Sub

    Private Sub txtCustomerCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
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
        Dim dtItem As New DataTable

        If chkOnlyGem.Checked = False And chkDiamond.Checked = False Then
            dt = objPurItemController.GetPurchaseInvoicePrint(_PurchaseHeaderID)
            dtItem = objPurItemController.GetPurchaseInvoiceDetailPrint(_PurchaseHeaderID)
            frmPrint.PrintPurchaseInvoice(dt, dtItem)
        ElseIf chkDiamond.Checked Then
            dt = objPurItemController.GetPurchaseInvoiceLooseDiamondPrint(_PurchaseHeaderID)
            frmPrint.PrintPurchaseInvoiceforOnlyGem(dt)
        Else
            dt = objPurItemController.GetPurchaseInvoiceOnlyGemPrint(_PurchaseHeaderID)
            frmPrint.PrintPurchaseInvoiceforLooseDiamond(dt)
        End If
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub

    Private Sub txtPPercent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPPercent.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub


    Private Sub txtPPercent_TextChanged(sender As Object, e As EventArgs) Handles txtPPercent.TextChanged
        If txtPWasteG.Text = "" Then txtPWasteG.Text = "0"
        If txtSaleWasteG.Text = "" Then txtSaleWasteG.Text = "0"
        If txtPPercent.Text = "" Then txtPPercent.Text = "0"

        txtPWasteG.Text = Format(Val(txtSaleWasteG.Text) * Val(CDec(txtPPercent.Text) / 100), "0.###")
        _DisWasteTG = _WasteTG * Val(CDec(txtPPercent.Text) / 100)
        CalculateDiscountWasteWeight()
    End Sub

    Private Sub CalculateDiscountWasteWeight()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        GoldWeight.Gram = _DisWasteTG
        _PWasteTG = GoldWeight.Gram
        GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
        _PWasteTK = GoldWeight.GoldTK

        GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
        txtPWasteK.Text = CStr(GoldWeight.WeightK)
        txtPWasteP.Text = CStr(GoldWeight.WeightP)
        txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
    End Sub

    'Private Sub CalculateWasteAmount()
    '    If txtPWasteG.Text = "" Then txtPWasteG.Text = "0"
    '    Dim GoldWeight As New CommonInfo.GoldWeightInfo
    '    Dim _PurWasteTK As Decimal = 0.0
    '    If txtCurPrice.Text = "" Then txtCurPrice.Text = "0"
    '    If txtPWasteK.Text = "" Then txtPWasteK.Text = "0"
    '    If txtPWasteP.Text = "" Then txtPWasteP.Text = "0"
    '    If txtPWasteY.Text = "" Then txtPWasteY.Text = "0.0"
    '    If txtPWasteG.Text = "" Then txtPWasteG.Text = "0.0"

    '    If _IsFixPrice = False Then
    '        GoldWeight.WeightK = CInt(txtPWasteK.Text)
    '        GoldWeight.WeightP = CInt(txtPWasteP.Text)
    '        GoldWeight.WeightY = System.Decimal.Truncate(txtPWasteY.Text)
    '        GoldWeight.WeightC = CDec(txtPWasteY.Text) - GoldWeight.WeightY
    '        GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
    '        _PurWasteTK = GoldWeight.GoldTK
    '        If _IsGram = True Then
    '            If _IsPercentage = True Then
    '                txtDiscountWasteAmount.Text = CStr(CLng(CDec(txtPWasteG.Text) * CLng(_CurrentSaleRate)))
    '            Else
    '                txtDiscountWasteAmount.Text = CStr(CLng(CDec(txtPWasteG.Text) * CLng(txtCurPrice.Text)))
    '            End If
    '        Else
    '            If _IsPercentage = True Then
    '                txtDiscountWasteAmount.Text = CStr(CLng(_PurWasteTK * CLng(_CurrentSaleRate)))
    '            Else
    '                txtDiscountWasteAmount.Text = CStr(CLng(_PurWasteTK * CLng(txtCurPrice.Text)))
    '            End If
    '        End If
    '    Else
    '        txtDiscountWasteAmount.Text = "0"
    '    End If
    '    CalculateTotalAmount()
    'End Sub

    Private Sub txtSaleRate_TextChanged(sender As Object, e As EventArgs) Handles txtSaleRate.TextChanged
        If txtSaleRate.Text <> "0" Then
            If txtSaleRate.Text.Length <= 3 Then
                LblSalePercent.Text = "%"
            Else
                If _IsGram Then
                    LblSalePercent.Text = "၁ ဂရမ်စျေး"
                Else
                    LblSalePercent.Text = "၁ ကျပ်သားစျေး"
                End If
            End If
        End If
    End Sub

    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'If grdGem.CurrentCell Is grdGem.CurrentRow.Cells("GemsK") Or grdGem.CurrentCell Is grdGem.CurrentRow.Cells("GemsP") Or grdGem.CurrentCell Is grdGem.CurrentRow.Cells("GemsY") Then
        '    If IsDBNull(grdGem.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdGem.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdGem.CurrentRow.Cells("GemsY").FormattedValue) Then
        '        If grdGem.CurrentRow.Cells("YOrCOrG").FormattedValue <> "0" Then
        '            grdGem.CurrentRow.Cells("YOrCOrG").Value = "0"
        '        End If
        '    End If
        'End If

        If grdGem.CurrentCell Is grdGem.CurrentRow.Cells("GemsK") Or grdGem.CurrentCell Is grdGem.CurrentRow.Cells("GemsP") Or grdGem.CurrentCell Is grdGem.CurrentRow.Cells("QTY") Or grdGem.CurrentCell Is grdGem.CurrentRow.Cells("PurchaseRate") Then
            If IsDBNull(grdGem.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdGem.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdGem.CurrentRow.Cells("QTY").FormattedValue) = False Or IsDBNull(grdGem.CurrentRow.Cells("PurchaseRate").FormattedValue) = False Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If

        ElseIf grdGem.CurrentCell Is grdGem.CurrentRow.Cells("GemsY") Then
            If IsDBNull(grdGem.CurrentRow.Cells("GemsY").FormattedValue) = False Then
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
    Private Sub grdGem_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdGem.EditingControlShowing
        If grdGem.CurrentCell Is grdGem.CurrentRow.Cells("GemsCategoryID") Or grdGem.CurrentCell Is grdGem.CurrentRow.Cells("FixType") Then
            Exit Sub
        End If

        Dim txtbox As TextBox = CType(e.Control, TextBox)
        If Not (txtbox Is Nothing) Then
            AddHandler txtbox.KeyPress, AddressOf txtBox_KeyPress
        End If
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

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("PurchaseStock")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtItemTG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtItemTG_TextChanged(sender As Object, e As EventArgs) Handles txtItemTG.TextChanged
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.0"
        If Val(txtItemTG.Text.Trim) >= 0 Then
            If _IsGram = True Then
                CalculateTotalWeightForGram()
            End If
            CalculateGoldWeight()
            CalculateGoldAmount()
        End If
    End Sub
    Private Sub CalculateTotalWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemK.Text = "" Then txtItemK.Text = "0"
        If txtItemP.Text = "" Then txtItemP.Text = "0"
        If txtItemY.Text = "" Then txtItemY.Text = "0.00"

        If (Val(txtItemK.Text) > 0 Or Val(txtItemP.Text) > 0 Or Val(txtItemY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtItemK.Text)
            GoldWeight.WeightP = CInt(txtItemP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtItemY.Text)
            GoldWeight.WeightC = CDec(txtItemY.Text) - GoldWeight.WeightY
            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _TotalTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _TotalTG = GoldWeight.Gram
            txtItemTG.Text = Format(_TotalTG, "0.000")
        Else
            _TotalTG = 0.0
            _TotalTK = 0.0
            txtItemTG.Text = "0.0"
            'txtItemK.Text = "0"
            'txtItemP.Text = "0"
            'txtItemY.Text = "0.0"
        End If
    End Sub

    Private Sub CalculateTotalWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.0"

        If Val(txtItemTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtItemTG.Text)
            _TotalTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _TotalTK = GoldWeight.GoldTK

            GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        ElseIf Val(txtItemTG.Text) > 0 And _WeightType = "Gram" Then
            GoldWeight.Gram = Format(Math.Round(CDec(txtItemTG.Text), 3), "0.000")
            _TotalTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _TotalTK = GoldWeight.GoldTK

            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            If NumberFormat = 1 Then
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
        Else
            _TotalTG = 0.0
            _TotalTK = 0.0

            txtItemK.Text = "0"
            txtItemP.Text = "0"
            txtItemY.Text = "0.0"
            'txtItemTG.Text = "0.0"
        End If
    End Sub


    Private Sub txtItemK_TextChanged(sender As Object, e As EventArgs) Handles txtItemK.TextChanged
        If txtItemK.Text = "" Then txtItemK.Text = "0"

        If Val(txtItemK.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateTotalWeightForKPY()
            End If
            CalculateGoldWeight()
            CalculateGoldAmount()

        End If
    End Sub

    Private Sub txtItemP_TextChanged(sender As Object, e As EventArgs) Handles txtItemP.TextChanged
        If txtItemP.Text = "" Then txtItemP.Text = "0"

        If Val(txtItemP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtItemP.Text = 0
            txtItemP.SelectAll()
        End If

        If Val(txtItemP.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateTotalWeightForKPY()
            End If
            CalculateGoldWeight()
            CalculateGoldAmount()
        End If
    End Sub

    Private Sub txtItemY_TextChanged(sender As Object, e As EventArgs) Handles txtItemY.TextChanged
        If txtItemY.Text = "" Then txtItemY.Text = "0.0"

        If Val(txtItemY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtItemY.Text = "0.0"
            txtItemY.SelectAll()
        End If
        If Val(txtItemY.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateTotalWeightForKPY()
            End If
            CalculateGoldWeight()
            CalculateGoldAmount()
        End If
    End Sub

    Private Sub grdGem_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdGem.RowsRemoved
        If grdGem.RowCount > 0 Then
            CalculategrdTotalAmount()
            CalculategrdGem()
            CalculateGoldWeight()
            CalculateGoldAmount()
        End If

    End Sub

    Private Sub chkIsChange_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsChange.CheckedChanged
        If chkIsChange.Checked Then
            chkOnlyGem.Checked = False
            chkOnlyGem.Enabled = False
        Else
            If _PurchaseHeaderID = "0" And chkShopGold.Checked = False Then
                chkOnlyGem.Enabled = True
            Else
                chkOnlyGem.Enabled = False
            End If

        End If
    End Sub

    Private Sub chkDone_CheckedChanged(sender As Object, e As EventArgs) Handles chkDone.CheckedChanged
        If chkDone.Checked Then
            txtGoldPrice.Text = "0"
            txtGemsPrice.Text = "0"
            txtDoneAmount.Enabled = True
            txtDoneAmount.Text = Format(Val(CLng(txtSaleAmount.Text)), "###,##0.##")

            If Val(txtSaleAmount.Text) > 0 Then
                chkPurchasePercent.Checked = False
                txtPurchasePercent.Text = "0"
                txtPurchasePercent.Enabled = False
                txtPurchcasePercentAmount.Text = "0"
            End If
        Else
            txtDoneAmount.Text = "0"
            txtDoneAmount.Enabled = False
            CalculateGoldAmount()
            CalculategrdTotalAmount()
        End If
        CalculateTotalAmount()
    End Sub

    Private Sub txtCurPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtCurPrice_TextChanged(sender As Object, e As EventArgs) Handles txtCurPrice.TextChanged
        If txtCurPrice.Text = "" Then txtCurPrice.Text = "0"
        CalculateGoldAmount()
        CalculateTotalAmount()
    End Sub

    Private Sub txtSaleAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSaleAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtPurchasePercent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPurchasePercent.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub chkPurchasePercent_CheckedChanged(sender As Object, e As EventArgs) Handles chkPurchasePercent.CheckedChanged
        If chkPurchasePercent.Checked Then
            txtGoldPrice.Text = "0"
            txtGemsPrice.Text = "0"
            txtPurchasePercent.Enabled = True
            txtPurchcasePercentAmount.Enabled = True
            chkDone.Checked = False
            txtDoneAmount.Enabled = False
            txtDoneAmount.Text = "0"
        Else
            txtPurchasePercent.Text = "0"
            txtPurchasePercent.Enabled = False
            txtPurchcasePercentAmount.Text = "0"
            txtPurchcasePercentAmount.Enabled = False
            CalculateGoldAmount()
            CalculategrdTotalAmount()
        End If
        CalculateTotalAmount()
    End Sub

    Private Sub txtPurchasePercent_TextChanged(sender As Object, e As EventArgs) Handles txtPurchasePercent.TextChanged
        If txtPurchasePercent.Text = "" Then txtPurchasePercent.Text = "0"
        If txtSaleAmount.Text = "" Then txtSaleAmount.Text = "0"

        If Val(txtPurchasePercent.Text) > 0 Then
            txtPurchcasePercentAmount.Text = Format(Val((CLng(txtSaleAmount.Text) * txtPurchasePercent.Text) / 100), "###,##0.##")
        Else
            txtPurchcasePercentAmount.Text = 0
        End If

        CalculateTotalAmount()
    End Sub

    Private Sub txtSaleAmount_TextChanged(sender As Object, e As EventArgs) Handles txtSaleAmount.TextChanged
        If txtSaleAmount.Text = "" Then txtSaleAmount.Text = "0"

        If chkPurchasePercent.Checked Then
            If txtPurchasePercent.Text = "" Then txtPurchasePercent.Text = "0"
            If Val(txtPurchasePercent.Text) > 0 Then
                txtPurchcasePercentAmount.Text = Format(Val((CLng(txtSaleAmount.Text) * txtPurchasePercent.Text) / 100), "###,##0.##")
            Else
                txtPurchcasePercentAmount.Text = "0"
            End If
        End If

        If chkDone.Checked Then
            txtDoneAmount.Text = Format(Val(CLng(txtSaleAmount.Text)), "###,##0.##")
        End If
        CalculateTotalAmount()
    End Sub

    'Private Sub txtItemAddOrSub_TextChanged(sender As Object, e As EventArgs) Handles txtItemAddOrSub.TextChanged
    '    If txtTotalAmt.Text = "" Then txtTotalAmt.Text = 0
    '    If txtItemAddOrSub.Text = "" Then txtItemAddOrSub.Text = 0
    '    txtNetAmount.Text = Format(Val(CLng(txtTotalAmt.Text) - CLng(txtItemAddOrSub.Text)), "###,##0.##")
    'End Sub

    Private Sub CalculateWasteWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtPWasteG.Text = "" Then txtPWasteG.Text = "0.0"

        If Val(txtPWasteG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtPWasteG.Text)
            _PWasteTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _PWasteTK = GoldWeight.GoldTK

            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtPWasteK.Text = CStr(GoldWeight.WeightK)
            txtPWasteP.Text = CStr(GoldWeight.WeightP)
            txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        Else
            _PWasteTG = 0.0
            _PWasteTK = 0.0

            txtPWasteK.Text = "0"
            txtPWasteP.Text = "0"
            txtPWasteY.Text = "0.0"
            'txtPWasteG.Text = "0.0"
        End If
    End Sub

    Private Sub CalculateWasteWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtPWasteK.Text = "" Then txtPWasteK.Text = "0"
        If txtPWasteP.Text = "" Then txtPWasteP.Text = "0"
        If txtPWasteY.Text = "" Then txtPWasteY.Text = "0.0"

        If (Val(txtPWasteK.Text) > 0 Or Val(txtPWasteP.Text) > 0 Or Val(txtPWasteY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtPWasteK.Text)
            GoldWeight.WeightP = CInt(txtPWasteP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtPWasteY.Text)
            GoldWeight.WeightC = CDec(txtPWasteY.Text) - GoldWeight.WeightY

            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _PWasteTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
            _PWasteTG = GoldWeight.Gram
            txtPWasteG.Text = Format(_PWasteTG, "0.000")
        Else
            _PWasteTG = 0.0
            _PWasteTK = 0.0

            txtPWasteG.Text = "0.0"
            'txtPWasteK.Text = "0"
            'txtPWasteP.Text = "0"
            'txtPWasteY.Text = "0.0"
        End If
    End Sub

    Private Sub txtPWasteK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPWasteK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPWasteK_TextChanged(sender As Object, e As EventArgs) Handles txtPWasteK.TextChanged
        If txtPWasteK.Text = "" Then txtPWasteK.Text = "0"

        If Val(txtPWasteK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteWeightForKPY()
        End If
        CalculateGoldAmount()
    End Sub

    Private Sub txtPWasteP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPWasteP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPWasteP_TextChanged(sender As Object, e As EventArgs) Handles txtPWasteP.TextChanged
        If txtPWasteP.Text = "" Then txtPWasteP.Text = "0"

        If Val(txtPWasteP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtPWasteP.Text = 0
            txtPWasteP.SelectAll()
        End If

        If Val(txtPWasteP.Text.Trim) >= 0 Then
            If _IsGram = False Then
                CalculateWasteWeightForKPY()
            End If
            CalculateGoldAmount()
        End If

    End Sub

    Private Sub txtPWasteY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPWasteY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtPWasteY_TextChanged(sender As Object, e As EventArgs) Handles txtPWasteY.TextChanged
        If txtPWasteY.Text = "" Then txtPWasteY.Text = "0.0"

        If Val(txtPWasteY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtPWasteY.Text = "0.0"
            txtPWasteY.SelectAll()
        End If

        If Val(txtPWasteY.Text.Trim) >= 0 Then
            If _IsGram = False Then
                CalculateWasteWeightForKPY()
            End If
            CalculateGoldAmount()
        End If
    End Sub

    Private Sub txtAddSub_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddSub.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPWasteG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPWasteG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtPWasteG_TextChanged(sender As Object, e As EventArgs) Handles txtPWasteG.TextChanged
        If txtPWasteG.Text = "" Then txtPWasteG.Text = "0.0"

        If Val(txtPWasteG.Text) >= 0 And _IsGram = True Then
            CalculateWasteWeightForGram()
        End If
        CalculateGoldAmount()
    End Sub

    'Private Sub txtGemAddSub_TextChanged(sender As Object, e As EventArgs) Handles txtGemAddSub.TextChanged
    '    If txtTotalAmount.Text = "" Then txtTotalAmount.Text = 0
    '    If txtGemAddSub.Text = "" Then txtGemAddSub.Text = 0
    '    txtGemNetAmount.Text = CStr(CLng(txtTotalAmount.Text) - CLng(txtGemAddSub.Text))
    'End Sub

    'Private Sub txtAddSub_TextChanged(sender As Object, e As EventArgs) Handles txtAddSub.TextChanged
    '    If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
    '    If txtAddSub.Text = "" Then txtAddSub.Text = "0"
    '    txtAllNetAmt.Text = CStr(CLng(txtAllTotalAmt.Text) - CLng(txtAddSub.Text))
    '    txtPaidAmt.Text = txtAllNetAmt.Text
    '    txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text))
    'End Sub

    Private Sub txtNetAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNetAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtNetAmount_TextChanged(sender As Object, e As EventArgs) Handles txtNetAmount.TextChanged
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtNetAmount.Text = "" Then txtNetAmount.Text = "0"

        txtItemAddOrSub.Text = Format(Val(CLng(txtTotalAmt.Text) - CLng(txtNetAmount.Text)), "###,##0.##")
    End Sub

    Private Sub txtAllNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAllNetAmt.TextChanged
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"

        txtAddSub.Text = Format(Val(CLng(txtAllTotalAmt.Text) - CLng(txtAllNetAmt.Text)), "###,##0.##")
        txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text)), "###,##0.##")
        txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtGemNetAmount_TextChanged(sender As Object, e As EventArgs) Handles txtGemNetAmount.TextChanged
        If txtTotalAmount.Text = "" Then txtTotalAmount.Text = "0"
        If txtGemNetAmount.Text = "" Then txtGemNetAmount.Text = "0"

        txtGemAddSub.Text = CLng(txtTotalAmount.Text) - CLng(txtGemNetAmount.Text)
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

    Private Sub dtpPurchaseDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpPurchaseDate.ValueChanged
        PurchaseInvoiceGenerateFormat()
    End Sub

    Private Sub txtSaleAmount_Validated(sender As Object, e As EventArgs) Handles txtSaleAmount.Validated
        txtSaleAmount.Text = Format(Val(CInt(txtSaleAmount.Text)), "###,##0.##")
    End Sub

    Private Sub txtDoneAmount_Validated(sender As Object, e As EventArgs) Handles txtDoneAmount.Validated
        txtDoneAmount.Text = Format(Val(CInt(txtDoneAmount.Text)), "###,##0.##")
    End Sub

    Private Sub txtNetAmount_Validated(sender As Object, e As EventArgs) Handles txtNetAmount.Validated
        txtNetAmount.Text = Format(Val(CInt(txtNetAmount.Text)), "###,##0.##")
    End Sub

    Private Sub txtAllNetAmt_Validated(sender As Object, e As EventArgs) Handles txtAllNetAmt.Validated
        txtAllNetAmt.Text = Format(Val(CInt(txtAllNetAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtPaidAmt_Validated(sender As Object, e As EventArgs) Handles txtPaidAmt.Validated
        txtPaidAmt.Text = Format(Val(CInt(txtPaidAmt.Text)), "###,##0.##")
    End Sub





    Private Sub chkShopGems_CheckedChanged(sender As Object, e As EventArgs) Handles chkShopGems.CheckedChanged
        If chkShopGems.Checked Then
            txtSaleGemsID.Visible = True
            btnSaleGemSearch.Visible = True
        Else
            txtSaleGemsID.Visible = False
            btnSaleGemSearch.Visible = False

            txtSaleGemsID.Text = ""
            ClearStock()
        End If
    End Sub

    Private Sub btnSaleGemSearch_Click(sender As Object, e As EventArgs) Handles btnSaleGemSearch.Click
        Dim dtGemItemCode As New DataTable
        Dim DataItem As DataRow
        Dim cri As String = ""
        If _CustomerID = "" Then
            MsgBox("Please Select Customer", MsgBoxStyle.Information, AppName)
            btnCustomer.Focus()
        Else
            If GetExistedGemItems() <> "" Then
                cri = " AND S.SaleGemsItemID NOT IN (" & GetExistedGemItems() & ") "
            End If
            dtGemItemCode = objSaleGemsController.GetSaleGemsDataByCustomerIDAndItemCode(_CustomerID, cri)
            DataItem = DirectCast(SearchData.FindFast(dtGemItemCode, "Sale Invoice List"), DataRow)
            If DataItem IsNot Nothing Then
                Dim GoldWeight As New CommonInfo.GoldWeightInfo
                Dim objGQInfo As New CommonInfo.GoldQualityInfo

                txtSaleGemsID.Text = DataItem("SaleGemsItemID")
                _SaleGemsItemID = DataItem("SaleGemsItemID")
                cboGemCategory.SelectedValue = DataItem("GemsCategoryID")


                txtGemQTY.Text = DataItem("QTY")
                txtGemName.Text = DataItem("GemsName")
                cboFixType.SelectedText = DataItem("FixType")
                txtUnitPrice.Text = DataItem("SaleRate")
                txtTotalAmount.Text = DataItem("TotalAmount")
                txtGemAddSub.Text = DataItem("DiscountAmount")
                txtGemNetAmount.Text = DataItem("PaidAmount")
                txtRBP.Text = DataItem("YOrCOrG")
                ''Calculate GemWeight TMN
                Dim equivalent As Decimal
                Dim VarWeight As String
                Dim VarWeightY As Integer
                Dim VarWeightBCG As Decimal
                Dim VarWeightP As Decimal
                Dim TP As Decimal
                Dim TY As Decimal
                Dim TC As Decimal

                Dim IsValid As Boolean
                If txtRBP.Text <> "" Then
                    VarWeight = txtRBP.Text

                    If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
                        MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                        txtRBP.Text = "0"
                        txtGemK.Text = "0"
                        txtGemP.Text = "0"
                        txtGemY.Text = "0.0"
                        txtRBP.Focus()

                    Else
                        If VarWeight.EndsWith("ct") Then
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                                VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                                TC = CStr(VarWeightBCG)
                                If Global_IsCarat = 0 Or Global_IsCarat = 2 Then
                                    GemTW = CStr(VarWeightBCG)
                                Else
                                    GemTW = CStr(VarWeightBCG * 1.1)
                                End If
                                IsValid = True
                            Else
                                IsValid = False
                            End If

                        ElseIf VarWeight.EndsWith("R") Then
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                                If VarWeight.IndexOf(".") = -1 Then
                                    VarWeightY = CInt(LSet(VarWeight, Len(VarWeight) - 1))
                                    equivalent = Global_KaratToYati ' _ConverterController.GetMeasurement("Karat", "Yati")
                                    TC = VarWeightY / equivalent
                                    If Global_IsCarat = 2 Then
                                        GemTW = TC
                                    Else
                                        GemTW = VarWeightY
                                    End If
                                    IsValid = True
                                Else
                                    VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                                    equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                    TC = VarWeight / equivalent
                                    If Global_IsCarat = 2 Then
                                        GemTW = TC
                                    Else
                                        GemTW = VarWeight
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
                                        equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                        TY = VarWeightY + (VarWeightBCG / equivalent)
                                        'GemTW = TY
                                        equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            GemTW = TC
                                        Else
                                            GemTW = TY
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
                                    'grdGem.Rows(e.RowIndex).Cells("GemTW").Value = VarWeightBCG
                                    equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")

                                    TY = VarWeightY + (VarWeightBCG / equivalent)
                                    ' GemTW = TY
                                    equivalent = Global_KaratToYati ' _ConverterController.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        GemTW = TC
                                    Else
                                        GemTW = TY
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
                                        equivalent = Global_BToP '_ConverterController.GetMeasurement("B", "P")
                                        TP = VarWeightBCG + (VarWeightP / equivalent)
                                        equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                        TY = VarWeightY + (TP / equivalent)
                                        'GemTW = TY
                                        equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            GemTW = TC
                                        Else
                                            GemTW = TY
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
                                        equivalent = Global_BToP '_ConverterController.GetMeasurement("B", "P")
                                        TP = VarWeightBCG + (VarWeightP / equivalent)
                                        equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                        TY = VarWeightY + (TP / equivalent)
                                        'GemTW = TY
                                        equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            GemTW = TC
                                        Else
                                            GemTW = TY
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
                                        equivalent = Global_BToP '_ConverterController.GetMeasurement("B", "P")
                                        TP = VarWeightBCG + (VarWeightP / equivalent)
                                        equivalent = Global_YatiToB '_ConverterController.GetMeasurement("Yati", "B")
                                        TY = VarWeightY + (TP / equivalent)
                                        'GemTW = TY
                                        equivalent = Global_KaratToYati '_ConverterController.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            GemTW = TC
                                        Else
                                            GemTW = TY
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
                        equivalent = Global_GramToKarat '_ConverterController.GetMeasurement("Gram", "Karat")
                        Dim gram As Decimal = TC / equivalent
                        GemTG = gram
                        equivalent = Global_KyatToGram '_ConverterController.GetMeasurement("Kyat", "Gram")
                        GemTK = gram / equivalent
                        '
                        GoldWeight.GoldTK = gram / equivalent
                        objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                        txtGemK.Text = GoldWeight.WeightK
                        txtGemP.Text = GoldWeight.WeightP
                        txtGemY.Text = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.0")
                        txtGemG.Text = Format(gram, "0.000")

                        '_IsFixPrice = DataItem("@IsFixPrice")
                        'chkIsFixPrice.Checked = DataItem("@IsFixPrice")
                        'cboGoldQuality.SelectedValue = DataItem("@GoldQualityID")
                        'objGQInfo = objGoldQualityController.GetGoldQuality(cboGoldQuality.SelectedValue)
                        'With objGQInfo
                        '    _IsGram = .IsGramRate
                        'End With
                        'GoldQualityForTextChange()
                        'GetCurrentPrice()


                        ''GoldWeight.GoldTK = _TotalTK
                        ''GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                        'txtItemK.Text = CStr(DataItem("ItemK"))
                        'txtItemP.Text = CStr(DataItem("ItemP"))
                        'txtItemY.Text = CStr(DataItem("ItemY"))
                        'txtItemTG.Text = Format(DataItem("@ItemTG"), "0.000")
                        '_TotalTK = DataItem("@ItemTK")
                        '_TotalTG = DataItem("@ItemTG")

                        ''GoldWeight.GoldTK = _WasteTK
                        ''GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                        'txtSaleWasteK.Text = CStr(DataItem("WasteK"))
                        'txtSaleWasteP.Text = CStr(DataItem("WasteP"))
                        'txtSaleWasteY.Text = CStr(DataItem("WasteY"))
                        'txtSaleWasteG.Text = Format(DataItem("@WasteTG"), "0.000")
                        '_WasteTK = DataItem("@WasteTK")
                        '_WasteTG = DataItem("@WasteTG")

                        ''GoldWeight.GoldTK = _GoldTK
                        ''GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                        'txtGoldK.Text = CStr(DataItem("GoldK"))
                        'txtGoldP.Text = CStr(DataItem("GoldP"))
                        'txtGoldY.Text = CStr(DataItem("GoldY"))
                        'txtGoldG.Text = Format(DataItem("@GoldTG"), "0.000")
                        '_GoldTK = DataItem("@GoldTK")
                        '_GoldTG = DataItem("@GoldTG")
                        'chkIsDiamond.Checked = DataItem("@IsDiamond")

                        'If DataItem("@Photo") <> "" Then
                        '    Try
                        '        lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + DataItem("@Photo"))
                        '        lblPhoto.Visible = False
                        '        Photo = DataItem("@Photo")
                        '    Catch ex As Exception
                        '        lblItemImage.Image = Nothing
                        '        lblPhoto.Visible = True
                        '        Photo = ""
                        '    End Try
                        'Else
                        '    Photo = ""
                        '    If Global_LogoPhoto <> "" Then
                        '        Try
                        '            lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                        '            lblPhoto.Visible = False
                        '        Catch ex As Exception
                        '            lblItemImage.Image = Nothing
                        '            lblPhoto.Visible = True
                        '        End Try
                        '    Else
                        '        lblItemImage.Image = Nothing
                        '        lblPhoto.Visible = True
                        '    End If
                        'End If

                        'If Val(txtSaleAmount.Text) > 0 Then
                        '    chkPurchasePercent.Enabled = True
                        'End If
                        'CalculateGoldAmount()

                        'If _IsOrder Then
                        '    _dtPurGem = _OrderInvoiceController.GetOrderInvoiceGemDataByOrderDetailID(_SaleInvoiceDetailID)
                        'Else
                        '    _dtPurGem = objSaleItemInvoiceController.GetSalesInvoiceGemDataBySaleDetailID(_SaleInvoiceDetailID)
                        'End If
                        'grdGem.DataSource = _dtPurGem
                        'CalculategrdTotalAmount()
                        'txtSaleAmount.Text = Format(Val(DataItem("@TotalAmount")), "###,##0.##")
                    End If

                Else
                    txtItemCode.Text = ""
                    ClearBarcodeData()
                End If
            End If

        End If
    End Sub

    Private Sub LnkDCategory_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkDCategory.LinkClicked
        Dim frm As New frm_GemsCategory
        frm.ShowDialog()
    End Sub
    Private Sub lnkDColor_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkDColor.LinkClicked
        Dim frmkeyword As New frm_Keyword
        frmkeyword._KeywordName = "Color"
        frmkeyword.ShowDialog()
        LoadCombos()
    End Sub
    Private Sub lnkShape_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkShape.LinkClicked
        Dim frmkeyword As New frm_Keyword
        frmkeyword._KeywordName = "Shape"
        frmkeyword.ShowDialog()
        LoadShapeCombos()
    End Sub
    Private Sub LoadShapeCombos()
        cboDShape.DisplayMember = "ItemName"
        cboDShape.ValueMember = "ItemName"
        cboDShape.DataSource = _GetKeyword.GetKeywordItemsByHeaderName("Shape")
    End Sub
    Private Sub LoadColorCombos()
        cboDColor.DisplayMember = "ItemName"
        cboDColor.ValueMember = "ItemName"
        cboDColor.DataSource = _GetKeyword.GetKeywordItemsByHeaderName("Color")
    End Sub
    Private Sub lnkClarity_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkClarity.LinkClicked
        Dim frmkeyword As New frm_Keyword
        frmkeyword._KeywordName = "Clarity"
        frmkeyword.ShowDialog()
        LoadClarityCombos()
    End Sub
    Private Sub LoadClarityCombos()
        cboDClarity.DisplayMember = "ItemName"
        cboDClarity.ValueMember = "ItemName"
        cboDClarity.DataSource = _GetKeyword.GetKeywordItemsByHeaderName("Clarity")
    End Sub
    Private Sub LoadCombos()
        cboDColor.DisplayMember = "ItemName"
        cboDColor.ValueMember = "ItemName"
        cboDColor.DataSource = _GetKeyword.GetKeywordItemsByHeaderName("Color")
    End Sub
    Private Sub GetDItemCategory()
        cboDCategory.DisplayMember = "GemsCategory"
        cboDCategory.ValueMember = "@GemsCategoryID"
        cboDCategory.DataSource = _GemsCat.GetAllGemsCategoryForGridCombo().DefaultView
        cboDCategory.SelectedIndex = -1
    End Sub
    Private Sub cboDCategory_Click(sender As Object, e As EventArgs) Handles cboDCategory.Click
        If cboDCategory.SelectedIndex = -1 Then
            GetDItemCategory()
        End If

    End Sub
    Private Sub cboDCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboDCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboDCategory, e)
    End Sub
    Private Sub cboDCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDCategory.Leave
        AutoCompleteCombo_Leave(cboDCategory, "")
    End Sub
    Private Sub chkShopDiamond_CheckedChanged(sender As Object, e As EventArgs) Handles chkShopDiamond.CheckedChanged
        If chkShopDiamond.Checked Then
            chkDIsFixPrice.Visible = True
            lblDSaleAmount.Visible = True
            txtDItemCode.Visible = True
            txtDSaleAmount.Visible = True
            btnDBarcodeSearch.Visible = True
            lblDOriginalCode.Visible = True
            lblDOrgSaleRate.Visible = True
        Else
            chkDIsFixPrice.Visible = False
            lblDSaleAmount.Visible = False
            txtDItemCode.Visible = False
            txtDSaleAmount.Visible = False
            btnDBarcodeSearch.Visible = False
            lblDOriginalCode.Visible = False
            lblDOrgSaleRate.Visible = False
            txtDItemCode.Text = ""
            ClearStock()
        End If

    End Sub
    Private Sub chkDIsChange_CheckedChanged(sender As Object, e As EventArgs) Handles chkDIsChange.CheckedChanged
        If chkDIsChange.Checked Then
            chkDIsChange.Checked = True
        Else
            chkDIsChange.Checked = False
        End If
        GetDCurrentPrice()
        CalculateGemsAmount()
    End Sub
    Private Sub GetDCurrentPrice()
        Dim objCurrent As New CommonInfo.IntDiamondPriceRateInfo
        objCurrent = _CurrentController.GetIntDiamondData(_Carat)
        With objCurrent
            _DCurrentSaleRate = .PriceRate
            txtDSaleRate.Text = _DCurrentSaleRate

            If chkDIsChange.Checked = True Then

                txtDCurPrice.Text = .PercentDirectChange
            Else
                txtDCurPrice.Text = .PercentReturn
            End If


            If Global_CurrentUser = "Administrator" Then
                txtDCurPrice.ReadOnly = False
            Else
                If Global_IsAllowSaleReturn Then
                    txtDCurPrice.ReadOnly = False
                Else
                    txtDCurPrice.ReadOnly = True
                End If
            End If
        End With
    End Sub
    Private Sub txtDRBP_LostFocus(sender As Object, e As EventArgs) Handles txtDRBP.LostFocus
        'For GemsWeight Yati,B,Karat
        Dim objCurrentPrice As New IntDiamondPriceRateInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim equivalent As Decimal
        Dim VarWeight As String
        Dim VarWeightY As Integer
        Dim VarWeightBCG As Decimal
        Dim VarWeightP As Decimal
        Dim TP As Decimal
        Dim TY As Decimal
        Dim TC As Decimal

        Dim IsValid As Boolean
        If txtDRBP.Text = "0" Then
            Exit Sub
        End If

        VarWeight = CStr(txtDRBP.Text)
        'If VarWeight = "0" Then
        '    Exit Sub
        'End If

        If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
            MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
            txtRBP.Text = "0"
            txtDGram.Text = "0"
            _DRBP = "0"
            _ItemTG = "0.0"
            _ItemTK = "0.0"
            _GemsTW = "0.0"

        Else
            If VarWeight.EndsWith("ct") Then
                If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                    VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                    ' Notes: For Karat,multiply 1.1 
                    TC = CStr(VarWeightBCG)
                    If Global_IsCarat = 0 Or Global_IsCarat = 2 Then 'If Global_IsCarat = True Then
                        _GemsTW = CDec(VarWeightBCG)
                    Else
                        _GemsTW = CDec(VarWeightBCG * 1.1)
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
                            _GemsTW = TC
                        Else
                            _GemsTW = VarWeightY
                        End If
                        IsValid = True
                    Else
                        VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                        equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                        TC = VarWeight / equivalent
                        If Global_IsCarat = 2 Then
                            _GemsTW = TC
                        Else
                            _GemsTW = VarWeight
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
                                _GemsTW = TC
                            Else
                                _GemsTW = TY
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
                            _GemsTW = TC
                        Else
                            _GemsTW = TY
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
                                _GemsTW = TC
                            Else
                                _GemsTW = TY
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
                                _GemsTW = TC
                            Else
                                _GemsTW = TY
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
                            equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                            TC = TY / equivalent
                            If Global_IsCarat = 2 Then
                                _GemsTW = TC
                            Else
                                _GemsTW = TY
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


            If Not IsValid And txtRBP.Text <> "0" Then
                MsgBox("Stone Weight is Invalid!", MsgBoxStyle.Information, "Invalid Data")
                txtRBP.Text = "0"
                _DRBP = "0"
                txtDGram.Text = "0.0"
                _ItemTG = "0.0"
                _ItemTK = "0.0"
                _GemsTW = "0.0"
            End If

            equivalent = Global_GramToKarat '_ConverterCon.GetMeasurement("Gram", "Karat")
            Dim gram As Decimal = TC / equivalent
            equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
            GoldWeight.GoldTK = gram / equivalent
            _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            _ItemTK = gram / equivalent
            _ItemTG = gram
            _Carat = gram * Global_GramToKarat
            _DRBP = txtDRBP.Text
            txtDGram.Text = Format(_ItemTG, "0.000")
        End If

        
        GetDCurrentPrice()
        CalculateGemsAmount()

    End Sub
    Private Sub CalculateGemsAmount()
        If txtDCurPrice.Text = "" Then txtDCurPrice.Text = "0"

        Dim _GemsPrice As Integer = 0

        If (chkDIsDone.Checked = False And chkDPurchasePercent.Checked = False) Then

            _GemsPrice = (_DCurrentSaleRate * _Carat) - ((_DCurrentSaleRate * _Carat) * (txtDCurPrice.Text) / 100)
            txtDiamondPrice.Text = Format(Val(_GemsPrice), "###,##0.##")
        Else
            txtDiamondPrice.Text = "0"
        End If
        CalculateDTotalAmount()
    End Sub
    Private Sub CalculateDTotalAmount()
        If txtDPercentPrice.Text = "" Then txtDPercentPrice.Text = "0"
        If txtDiamondPrice.Text = "" Then txtDiamondPrice.Text = "0"
        If txtDDonePrice.Text = "" Then txtDDonePrice.Text = "0"
        If txtDSaleAmount.Text = "" Then txtDSaleAmount.Text = "0"

        If chkDIsDone.Checked Then
            txtDTotalAmount.Text = txtDDonePrice.Text
        ElseIf chkDPurchasePercent.Checked Then
            txtDTotalAmount.Text = Format(Val(CLng(txtDSaleAmount.Text) - CLng(txtDPercentPrice.Text)), "###,##0.##")
        Else
            txtDTotalAmount.Text = Format(Val(CLng(txtDiamondPrice.Text)), "###,##0.##")
        End If
        txtDAddOrSub.Text = "0"
        txtDNetAmount.Text = Format(Val(CLng(txtDTotalAmount.Text)), "###,##0.##")
    End Sub
    Private Sub chkDIsDone_CheckedChanged(sender As Object, e As EventArgs) Handles chkDIsDone.CheckedChanged
        If chkDIsDone.Checked Then
            txtDiamondPrice.Text = "0"
            txtDDonePrice.Enabled = True
            txtDDonePrice.Text = Format(Val(CLng(txtDSaleAmount.Text)), "###,##0.##")

            If Val(txtDSaleAmount.Text) > 0 Then
                chkDPurchasePercent.Checked = False
                txtDPurchasePercent.Text = "0"
                txtDPurchasePercent.Enabled = False
                txtDPercentPrice.Text = "0"
            End If
        Else
            txtDDonePrice.Text = "0"
            txtDDonePrice.Enabled = False
            CalculateGemsAmount()
        End If
        CalculateDTotalAmount()
    End Sub
    Private Sub chkDPurchasePercent_CheckedChanged(sender As Object, e As EventArgs) Handles chkDPurchasePercent.CheckedChanged
        If chkDPurchasePercent.Checked Then
            txtDiamondPrice.Text = "0"
            txtDPurchasePercent.Enabled = True
            txtDPercentPrice.Enabled = True
            chkDIsDone.Checked = False
            txtDDonePrice.Enabled = False
            txtDDonePrice.Text = "0"
        Else
            txtDPurchasePercent.Text = "0"
            txtDPurchasePercent.Enabled = False
            txtDPercentPrice.Text = "0"
            txtDPercentPrice.Enabled = False
            CalculateGemsAmount()
        End If
        CalculateDTotalAmount()
    End Sub

    Private Sub txtDPurchasePercent_TextChanged(sender As Object, e As EventArgs) Handles txtDPurchasePercent.TextChanged
        If txtDPurchasePercent.Text = "" Then txtDPurchasePercent.Text = "0"
        If txtDSaleAmount.Text = "" Then txtDSaleAmount.Text = "0"

        If Val(txtDPurchasePercent.Text) > 0 Then
            txtDPercentPrice.Text = Format(Val((CLng(txtDSaleAmount.Text) * txtDPurchasePercent.Text) / 100), "###,##0.##")
        Else
            txtDPercentPrice.Text = 0
        End If

        CalculateDTotalAmount()
    End Sub

    Private Sub txtDSaleAmount_TextChanged(sender As Object, e As EventArgs) Handles txtDSaleAmount.TextChanged
        If txtDSaleAmount.Text = "" Then txtDSaleAmount.Text = "0"

        If chkDPurchasePercent.Checked Then
            If txtDPurchasePercent.Text = "" Then txtDPurchasePercent.Text = "0"
            If Val(txtDPurchasePercent.Text) > 0 Then
                txtDPercentPrice.Text = Format(Val((CLng(txtDSaleAmount.Text) * txtDPurchasePercent.Text) / 100), "###,##0.##")
            Else
                txtDPercentPrice.Text = "0"
            End If
        End If

        If chkDIsDone.Checked Then
            txtDDonePrice.Text = Format(Val(CLng(txtDSaleAmount.Text)), "###,##0.##")
        End If
        CalculateDTotalAmount()
    End Sub
    Private Sub txtDNetAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDNetAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDNetAmount_TextChanged(sender As Object, e As EventArgs) Handles txtDNetAmount.TextChanged
        If txtDTotalAmount.Text = "" Then txtDTotalAmount.Text = "0"
        If txtDNetAmount.Text = "" Then txtDNetAmount.Text = "0"

        txtDAddOrSub.Text = Format(Val(CLng(txtDTotalAmount.Text) - CLng(txtDNetAmount.Text)), "###,##0.##")
    End Sub
    Private Sub btnDBarcodeSearch_Click(sender As Object, e As EventArgs) Handles btnDBarcodeSearch.Click
        Dim dtItemCode As New DataTable
        Dim DataItem As DataRow
        Dim cri As String = ""
        If _CustomerID = "" Then
            MsgBox("Please Select Customer", MsgBoxStyle.Information, AppName)
            btnCustomer.Focus()
        Else
            If GetExistedItems() <> "" Then
                cri = " AND S.ForSaleID NOT IN (" & GetExistedItems() & ") "
            End If
            dtItemCode = objSaleLooseDiamondController.GetSaleLooseDiamondDataByCustomerIDAndItemCode(_CustomerID, cri)
            DataItem = DirectCast(SearchData.FindFast(dtItemCode, "Sale Loose Diamond List"), DataRow)
            If DataItem IsNot Nothing Then

                txtDItemCode.Text = DataItem("ItemCode")
                lblDOriginalCode.Text = DataItem("@OriginalCode")
                lblDOrgSaleRate.Text = Format(Val(DataItem("@SalesRate")), "###,##0.##")
                _DSaleRate = DataItem("@SalesRate")
                _SDiamondPrice = DataItem("@GemsPrice")
                _SDTotalCharges = DataItem("@TotalCharges")

                _SaleLooseDiamondDetailID = DataItem("@SaleLooseDiamondDetailID")
                _ForSaleID = DataItem("@ForSaleID")
                cboDCategory.SelectedValue = DataItem("@GemsCategoryID")
                cboDColor.SelectedValue = DataItem("Color")
                cboDShape.SelectedValue = DataItem("Shape")
                cboDClarity.SelectedValue = DataItem("Clarity")
                txtDescription.Text = DataItem("GemsName")

                txtQty.Text = 1
                _IsFixPrice = DataItem("@IsFixPrice")
                chkDIsFixPrice.Checked = DataItem("@IsFixPrice")
                txtDQty.Text = DataItem("Qty")

                txtDGram.Text = Format(DataItem("@ItemTG"), "0.000")
                _ItemTK = DataItem("@ItemTK")
                _ItemTG = DataItem("@ItemTG")
                txtDRBP.Text = DataItem("YOrCOrG")
                _Carat = _ItemTG * Global_GramToKarat
                GetDCurrentPrice()

                

                If DataItem("@Photo") <> "" Then
                    Try
                        lblDItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + DataItem("@Photo"))
                        lblDPhoto.Visible = False
                        Photo = DataItem("@Photo")
                    Catch ex As Exception
                        lblDItemImage.Image = Nothing
                        lblDPhoto.Visible = True
                        Photo = ""
                    End Try
                Else
                    Photo = ""
                    If Global_LogoPhoto <> "" Then
                        Try
                            lblDItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                            lblDPhoto.Visible = False
                        Catch ex As Exception
                            lblDItemImage.Image = Nothing
                            lblDPhoto.Visible = True
                        End Try
                    Else
                        lblDItemImage.Image = Nothing
                        lblDPhoto.Visible = True
                    End If
                End If

                If Val(txtDSaleAmount.Text) > 0 Then
                    chkDPurchasePercent.Enabled = True
                End If
                CalculateGemsAmount()

                If DataItem("@IsFixPrice") Then
                    txtDSaleAmount.Text = Format(Val(DataItem("@ItemNetAmount")), "###,##0.##")
                Else
                    txtDSaleAmount.Text = Format(Val(DataItem("@GemsPrice") + Val(DataItem("@AddOrSub"))), "###,##0.##")
                End If

            Else
                txtDItemCode.Text = ""
                ClearBarcodeData()
            End If
        End If

    End Sub
    Private Sub chkDiamond_CheckedChanged(sender As Object, e As EventArgs) Handles chkDiamond.CheckedChanged
        Dim objPHeader As New CommonInfo.PurchaseHeaderInfo
        If chkDiamond.Checked Then
            If _PurchaseHeaderID = "0" Then
                tabStock.SelectedIndex = 2
                tabStock.TabPages.Remove(TabStockItem)
                tabStock.TabPages.Remove(TabGem)
                tabStock.TabPages.Add(TabLooseDiamond)
                chkIsChange.Checked = False
                'chkIsChange.Visible = False
                '_dtDetail.Rows.Clear()
                _dtStone.Rows.Clear()
                grdDetail.AutoGenerateColumns = False
                grdDetail.ReadOnly = True
                grdDetail.DataSource = _dtDetail
                grdDetail.Columns("GemsCategory").Visible = True
                grdDetail.Columns("GemsName").Visible = True
                grdDetail.Columns("TotalGemTG").Visible = True
                grdDetail.Columns("BarcodeNo").Visible = False
                grdDetail.Columns("ItemName").Visible = False
                grdDetail.Columns("TotalTG").Visible = False
                grdDetail.Columns("IsShop").Visible = False
                txtDItemCode.Visible = False
                btnDBarcodeSearch.Visible = False
            End If

        Else
            If _PurchaseHeaderID = "0" Then
                tabStock.TabPages.Remove(TabLooseDiamond)
                tabStock.TabPages.Add(TabStockItem)
                chkIsChange.Enabled = True
                chkIsChange.Visible = True
                '_dtDetail.Rows.Clear()
                _dtStone.Rows.Clear()
                grdDetail.AutoGenerateColumns = False
                grdDetail.ReadOnly = True
                grdDetail.DataSource = _dtDetail
                grdDetail.Columns("GemsCategory").Visible = False
                grdDetail.Columns("GemsName").Visible = False
                grdDetail.Columns("TotalGemTG").Visible = False
                grdDetail.Columns("BarcodeNo").Visible = True
                grdDetail.Columns("ItemName").Visible = True
                grdDetail.Columns("TotalTG").Visible = True
                grdDetail.Columns("IsShop").Visible = True
            End If

            End If
            txtDItemCode.Text = ""
            ClearDetail()
    End Sub
    Private Sub btnDAdd_Click(sender As Object, e As EventArgs) Handles btnDAdd.Click
        If chkDiamond.Checked = True Then
            If cboDCategory.Text = "" Then
                MsgBox("Please Fill Gem Category!", MsgBoxStyle.Information, "Data Require!")
                cboDCategory.Focus()
                Exit Sub
            End If

            If Val(txtDGram.Text) = 0.0 Then
                MsgBox("Please Check Gem Weight!", MsgBoxStyle.Information, "Data Require!")
                LnkTotalNoWaste.Focus()
                Exit Sub
            End If
            If txtDQty.Text = "0" Then
                MsgBox("Please Enter Quantity!", MsgBoxStyle.Information, "Data Require!")
                txtDQty.Focus()
                Exit Sub
            End If
            If chkShopDiamond.Checked And txtDItemCode.Text = "" Then
                MsgBox("Please Select Valid Barcode No!", MsgBoxStyle.Information, "Data Require!")
                btnDBarcodeSearch.Focus()
                Exit Sub
            End If

            If chkDIsDone.Checked And Val(txtDDonePrice.Text) = 0 Then
                MsgBox("Please Check Done Amount!", MsgBoxStyle.Information, "Data Require!")
                txtDDonePrice.Focus()
                Exit Sub
            End If

            If chkDPurchasePercent.Checked And Val(txtDPurchasePercent.Text) = 0 Then
                MsgBox("Please Check Percentage!", MsgBoxStyle.Information, "Data Require!")
                txtDPurchasePercent.Focus()
                Exit Sub
            End If

            If Val(txtDTotalAmount.Text) = 0 Then
                MsgBox("Please Check Amount!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If

            If _dtDetail.Rows.Count > 0 And _IsUpdate = False And txtDItemCode.Text <> "" Then
                For Each dr As DataRow In _dtDetail.Rows
                    If dr.RowState <> DataRowState.Deleted Then
                        If dr("BarcodeNo") <> "" Then
                            If dr("BarcodeNo") = txtDItemCode.Text Then
                                MsgBox("Duplicate BarcodeNo!", MsgBoxStyle.Information, "Duplicate Data!")
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            End If

        End If


        If _IsUpdate Then
            UpdateItem(_PurchaseDetailID, _dtPurGem)
        Else

            If btnAdd.Text = "Add" Then
                _PurchaseDetailID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.PurchaseDetail, EnumSetting.GenerateKeyType.PurchaseDetail.ToString, dtpPurchaseDate.Value)
                InsertItem(_PurchaseDetailID, _dtPurGem)
            End If
        End If
        CalculateAllDetailTotalAmount()
        CalculategrAlldTotalWeight()
        txtItemCode.Text = ""
        ClearBarcodeData()
    End Sub
End Class