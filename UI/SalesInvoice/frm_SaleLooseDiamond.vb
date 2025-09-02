Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
Imports System.Drawing
Imports System.String
Imports System.Windows
Imports System.Int64
Imports System.Decimal
Imports Operational.AppConfiguration
Imports System.Text
Imports System.Net
Public Class frm_SaleLooseDiamond
    Implements IFormProcess

    Private _SaleLooseDiamondID As String = ""
    Private _SaleLooseDiamondDetailID As String = ""
    Private _StaffID As String = ""
    Private _ForSaleID As String = ""
    Private _BarcodeNo As String = ""
    Private _ItemCode As String = ""
    Private _CustomerID As String = ""
    Private _GemsCategoryID As String = ""
    Private _dtSaleLooseDiamondItem As DataTable
    Private _LocationID As String = ""
    Private PName As String = ""
    Private _FixPrice As Integer = 0
    Private _GemsPrice As Integer = 0
    Private _IsUpdate As Boolean = False
    Private _PurchaseHeaderID As String = ""
    Private _IsAllowDiscount As Boolean = False

    Dim _ItemTK As Decimal = 0.0
    Dim _ItemTG As Decimal = 0.0

    Dim isFixPrice As Boolean = False
    Dim numberformat As Integer


    Private _dtItemBarcode As DataTable
    Private _dtAllDiamond As DataTable
    Private _IsGemInDB As Boolean = False
    Private _IsRowDelete As Boolean = False
    Private _AllTotalAmount As Long = 0
    Private _QTY As Integer = 0
    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
    Private _IsSearch As Boolean = False
    Private _WeightType As String = ""
    Private _RBP As String = ""
    Private _Carat As Decimal = "0.0"
    Private _GemsTW As Decimal = "0.0"
    Private _IsSaleReturn As Boolean

    Private IsOriginalFixedPrice As Boolean = False
    Private _OriginalFixedPrice As Long = 0
    Private IsOriginalPriceCarat As Boolean = False
    Private _OriginalPriceCarat As Long = 0
    Private _dtOtherCash As DataTable

    Dim MCode As String = ""
    Dim Token As String = ""
    Dim Temp As String = ""
    Dim IsUseRedeem As Boolean = False
    Dim MaxPoint As Integer = 0
    Dim RedeemPoint As Integer = 0
    Dim TopupPoint As Integer = 0
    Dim TempRedeemID As String = ""
    Dim RedeemID As String = ""

    Dim RedeemTopupPoint As Integer = 0
    Dim RedeemTopupvalue As Integer = 0
    Dim RedeemValue As Integer = 0
    Dim TopupValue As Integer = 0
    Dim PointConfiguration As Integer = 1
    Dim AmountConfiguration As Integer = 1
    Dim OpportunityPoint As Integer = 1
    Dim OpportunityValue As Integer = 1

    Dim MemberRedeemPoint As Integer = 0
    Dim MemberRedeemValue As Integer = 0
    Dim VoucherPointBalance As Integer = 0


    Dim PointBalance As Integer = 0
    Dim Status As Boolean = False
    Dim IsRedeemInvoice As Boolean = False
    Dim DiscountType As String = ""
    Dim OppurtunityType As String = ""

    Dim MemDiscountAmount As Integer = 0
    Dim DiscountPercent As Decimal = 0
    Dim ServiceURL As String = ""


    Dim RedeemSuccess As Boolean
    Dim TopUpSuccess As Boolean
    Dim InvoiceStatus As Integer = 0
    Dim _TransactionID As String
    Dim _RedeemID As String
    Dim _OldTopupPoint As Integer = 0
    Dim _OldRedeemTopupPoint As Integer = 0


    Public TransBool As Boolean
    Public RedeemBool As Boolean
    Dim _IsUpdateHeader As Boolean
    Dim _isMaximum As Boolean = False
    Dim dtMember As New DataTable()
    Dim dtRedeemID As New DataTable()
    Dim dtTransactionID As New DataTable()

    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private objLocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    'Private _ItemNameCon As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private objGemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private objSaleLooseDiamondController As SaleLooseDiamond.ISaleLooseDiamondController = Factory.Instance.CreateSaleLooseDiamondController
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _SalesItemController As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _CurrentController As InternationalDiamond.IIntDiamondPriceRateController = Factory.Instance.CreateIntDiamondController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _ObjPurchaseController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private _ConverterCon As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GemsCat As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private objSalesInvoiceController As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController

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

    'Private Sub frm_SaleVolumeInvoice_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    '    GetcboStaff()
    'End Sub
    Private Sub frm_SaleLooseDiamond_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MyBase._Heading.Text = "ရောင်းခြင်း(Volume)"
        'MyBase.lblLogInUserName.Text = Global_CurrentUser
        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        numberformat = Global_DecimalFormat
        _LocationID = Global_CurrentLocationID
        GetcboStaff()
        Clear()
        FormatGridItemDetail()
        Me.KeyPreview = True
        ' MyBase.addGridDataErrorHandlers(grdSaleCategory)
    End Sub
    Private Sub frm_SaleLooseDiamond_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

        Dim dt As New DataTable
        dt = objGeneralController.CheckExitVoucherForCashReceipt("tbl_CashReceipt", "AND VoucherNo='" + _SaleLooseDiamondID + "' AND Type='SaleLooseDiamond'")
        If dt.Rows.Count() > 0 Then
            MsgBox("This VoucherNo is Use in CashReceipt!", MsgBoxStyle.Information, "")
            Return False
            Exit Function
        End If

        'If objSaleLooseDiamondController.DeleteSaleLooseDiamond(GetDataSaleLooseDiamond()) Then
        '    Clear()
        '    btnDelete.Enabled = False
        '    btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
        '    Return True
        'Else
        '    Return False
        'End If

        Dim objSaleLooseDiamond As CommonInfo.SaleLooseDiamondHeaderInfo
        objSaleLooseDiamond = GetDataSaleLooseDiamond()
        If objSaleLooseDiamondController.DeleteSaleLooseDiamond(GetDataSaleLooseDiamond()) Then

            If Global_IsUseMember = True Then

                If OppurtunityType <> "Item" Then

                    ''To Filter With RedeemID

                    If IsRedeemInvoice = True Then
                        UpdateRedeem(objSaleLooseDiamond, 2)
                    Else
                        AddRedeem(objSaleLooseDiamond, 2)
                    End If
                End If

                SaveSaleMemberCard(objSaleLooseDiamond, 2)
            End If
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
        'GetcboStaff()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            Dim objSaleLooseDiamond As New SaleLooseDiamondHeaderInfo

            objSaleLooseDiamond = GetDataSaleLooseDiamond()
            If Global_IsUseMember = False Or txtMemberCode.Text = "" Then ' no use member 
                If objSaleLooseDiamondController.SaveSaleLooseDiamondHeader(objSaleLooseDiamond, _dtItemBarcode, _dtOtherCash) Then
                    _SaleLooseDiamondID = objSaleLooseDiamond.SaleLooseDiamondID
                    If (MsgBox("Do You Want To Save And Print SaleLooseDiamond Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                        Dim frmPrint As New frm_ReportViewer
                        Dim dt As New DataTable
                        Dim obj As New CommonInfo.SaleLooseDiamondHeaderInfo
                        Dim _PurchaseHeaderID As String
                        Dim dtPurchase As New DataTable

                        obj = objSaleLooseDiamondController.GetSaleLooseDiamondByID(_SaleLooseDiamondID)
                        _PurchaseHeaderID = obj.PurchaseHeaderID

                        If _PurchaseHeaderID = "" Then
                            dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

                            If dt.Rows.Count() = 0 Then
                                MsgBox("Please Select Sale LooseDiamond Voucher First!", MsgBoxStyle.Information, AppName)
                                Exit Function
                            Else
                                For Each dr As DataRow In dt.Rows
                                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                Next
                            End If
                        Else
                            dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

                            If dt.Rows.Count() = 0 Then
                                MsgBox("Please Select Sale Loose Diamond Voucher First!", MsgBoxStyle.Information, AppName)
                                Exit Function
                            Else
                                For Each dr As DataRow In dt.Rows
                                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                    '_GoldQualityID = dr.Item("GoldQualityID")
                                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                Next
                            End If
                            dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleLooseDiamond(_SaleLooseDiamondID)
                        End If

                        frmPrint.PrintSaleLooseDiamond(dt, dtPurchase)
                        frmPrint.WindowState = FormWindowState.Maximized
                        frmPrint.Show()
                        Clear()
                    Else
                        Clear()
                        Return True
                    End If
                Else
                    Return False
                End If
            ElseIf Global_IsUseMember = True Then ' Use Member
                If CheckForInternetConnection() = True Then
                    If objSaleLooseDiamondController.SaveSaleLooseDiamondHeader(objSaleLooseDiamond, _dtItemBarcode, _dtOtherCash) Then
                        _SaleLooseDiamondID = objSaleLooseDiamond.SaleLooseDiamondID

                        'Can be use For Oppurnity Type Amount,Discount only
                        If OppurtunityType <> "Item" Then
                            If RedeemID <> "" And btnSave.Text <> "Update" Then  'RedeemIsuse 1
                                If CheckForInternetConnection() = True Then
                                    UpdateRedeem(objSaleLooseDiamond, 0)

                                End If
                            Else

                                If btnSave.Text <> "Update" Then
                                    If CheckForInternetConnection() = True Then
                                        AddRedeem(objSaleLooseDiamond, 0)
                                    End If

                                Else
                                    If CheckForInternetConnection() = True Then
                                        AddRedeem(objSaleLooseDiamond, 1)

                                    End If

                                End If
                            End If
                        End If

                        If IsMaximumPoint() = True Then
                            If btnSave.Text <> "Update" Then
                                SaveSaleMemberCard(objSaleLooseDiamond, 0)
                            Else
                                SaveSaleMemberCard(objSaleLooseDiamond, 1)
                            End If
                        End If

                        If (MsgBox("Do You Want To Save And Print SaleLooseDiamond Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                            Dim frmPrint As New frm_ReportViewer
                            Dim dt As New DataTable
                            Dim obj As New CommonInfo.SaleLooseDiamondHeaderInfo
                            Dim _PurchaseHeaderID As String
                            Dim dtPurchase As New DataTable

                            obj = objSaleLooseDiamondController.GetSaleLooseDiamondByID(_SaleLooseDiamondID)
                            _PurchaseHeaderID = obj.PurchaseHeaderID

                            If _PurchaseHeaderID = "" Then
                                dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Loose Diamond Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                    Next
                                End If
                            Else
                                dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale LooseDiamond Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                    Next
                                End If
                                dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleLooseDiamond(_SaleLooseDiamondID)
                            End If

                            UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _SaleLooseDiamondID)
                            dt.Rows(0).Item("PointBalance") = VoucherPointBalance ' ForVoucherPointBalance
                            frmPrint.PrintSaleLooseDiamond(dt, dtPurchase)
                            frmPrint.WindowState = FormWindowState.Maximized
                            frmPrint.Show()
                            Clear()
                        Else
                            UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _SaleLooseDiamondID)
                            Clear()
                            Return True
                        End If
                    End If
                Else 'Member သုံးပြီးလိုင်းမရဘူး Yes= လိုင်းမရပေမယ့်သိမ်းမယ်
                    If objSaleLooseDiamondController.SaveSaleLooseDiamondHeader(objSaleLooseDiamond, _dtItemBarcode, _dtOtherCash) Then
                        _SaleLooseDiamondID = objSaleLooseDiamond.SaleLooseDiamondID
                        If (MsgBox("Do You Want To Save And Print SaleLooseDiamond Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                            Dim frmPrint As New frm_ReportViewer
                            Dim dt As New DataTable
                            Dim obj As New CommonInfo.SaleLooseDiamondHeaderInfo
                            Dim _PurchaseHeaderID As String
                            Dim dtPurchase As New DataTable

                            obj = objSaleLooseDiamondController.GetSaleLooseDiamondByID(_SaleLooseDiamondID)
                            _PurchaseHeaderID = obj.PurchaseHeaderID

                            If _PurchaseHeaderID = "" Then
                                dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale LooseDiamond Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                    Next
                                End If
                            Else
                                dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Loose Diamond Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        '_GoldQualityID = dr.Item("GoldQualityID")
                                        '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                    Next
                                End If
                                dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleLooseDiamond(_SaleLooseDiamondID)
                            End If

                            frmPrint.PrintSaleLooseDiamond(dt, dtPurchase)
                            frmPrint.WindowState = FormWindowState.Maximized
                            frmPrint.Show()
                            Clear()
                        Else
                            Clear()
                            Return True
                        End If
                    Else
                        Return False
                    End If
                End If
            End If
        End If


    End Function

    Private Function GetDataSaleLooseDiamond() As SaleLooseDiamondHeaderInfo
        GetDataSaleLooseDiamond = New SaleLooseDiamondHeaderInfo
        With GetDataSaleLooseDiamond
            .SaleLooseDiamondID = _SaleLooseDiamondID
            .StaffID = cboStaff.SelectedValue
            .SaleDate = dtpSaleDate.Value
            .CustomerID = _CustomerID
            .TotalAmount = txtAllTotalAmt.Text
            .AddOrSub = txtAllAddOrSub.Text
            .DiscountAmount = txtDiscountAmt.Text
            .PaidAmount = txtPaidAmt.Text
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .PromotionDiscount = IIf(txtPromotionDis.Text = "", 0, txtPromotionDis.Text)
            .PurchaseHeaderID = _PurchaseHeaderID
            .PurchaseAmount = IIf(txtPurchaseAmount.Text = "", 0, txtPurchaseAmount.Text)
            .IsOtherCash = chkOtherCash.Checked
            .OtherCashAmount = IIf(txtOtherCashAmount.Text = "", 0, txtOtherCashAmount.Text)
            .SRTaxPer = IIf(txtSRTaxPer.Text = "", 0, txtSRTaxPer.Text)
            .SRTaxAmt = CInt(txtSRTaxAmt.Text)
            .MemberDis = IIf(txtMemberDis.Text = "", 0, txtMemberDis.Text)
            .MemberDiscountAmt = IIf(txtMemberDisAmt.Text = "", 0, txtMemberDisAmt.Text)
            .MemberID = IIf(txtMemberID.Text = "", "", txtMemberID.Text)
            .MemberName = IIf(txtMemberName.Text = "", "", txtMemberName.Text)
            .MemberCode = IIf(txtMemberCode.Text = "", "", txtMemberCode.Text)
            TempRedeemID = CStr(RedeemID)
            .RedeemID = TempRedeemID.Replace("|", ",")

            .RedeemPoint = RedeemPoint
            .RedeemValue = RedeemValue

            If _IsUpdateHeader = False And .RedeemID <> "" And Global_IsUseMember = True Then
                .IsRedeemInvoice = True
            End If

            If _IsUpdateHeader = True Then
                .InvoiceStatus = InvoiceStatus
                .IsRedeemInvoice = IsRedeemInvoice
                .RedeemValue = RedeemValue

            End If

            If OppurtunityType = "Amount" Then 'OppurtunityType Amount 0,Discount 1 Item 2
                .InvoiceStatus = 0
            ElseIf OppurtunityType = "Discount" Then
                .InvoiceStatus = 1

            ElseIf OppurtunityType = "Item" Then
                .IsRedeemInvoice = True
                ' IsRedeemInvoice = True
                .InvoiceStatus = 2
            Else
                .InvoiceStatus = 0
            End If



            If Global_IsUseMember = True Then

                TopupPoint = CInt(CInt(PointConfiguration) * CInt(CInt(CInt(txtAllNetAmt.Text) / CInt(AmountConfiguration))))
                TopupValue = CInt(TopupPoint) * CInt(AmountConfiguration)

            End If


            .TopupPoint = TopupPoint
            .TopupValue = TopupValue


            .Token = Token
        End With
    End Function
    Private Sub GetcboStaff()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = objStaffController.GetStaffList().DefaultView
        cboStaff.SelectedIndex = -1
    End Sub
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

        If _dtItemBarcode.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        Return True
    End Function

    Private Sub ClearItemCode()
        If Global_UserLevel = "Administrator" Then
            txtCurrentPrice.ReadOnly = False
            txtCurrentPrice.BackColor = Drawing.Color.White
        Else
            txtCurrentPrice.ReadOnly = True
            txtCurrentPrice.BackColor = Drawing.Color.Linen
        End If
        txtCurrentPrice.Text = 0
        txtGemsCategory.Text = ""
        txtOriginalCode.Text = ""
        txtShape.Text = ""
        txtColor.Text = ""

        chkIsFixPrice.Checked = False
        txtGemsPrice.Text = "0"
        txtSaleFixPrice.Text = "0"
        txtQty.Text = "0"
        txtTotalAmt.Text = "0"
        txtNetAmt.Text = "0"
        txtAddSub.Text = 0
        txtSaleFixPrice.Text = "0"
        btnAdd.Text = "Add"
        _IsUpdate = False
        isFixPrice = False
        _FixPrice = 0
        lblDonePrice.Visible = False
        txtSaleFixPrice.Visible = False
        txtSaleFixPrice.Text = "0"
        lblWeight.Text = ""
        lblIsGram.Text = ""
        _QTY = 0
        _SaleLooseDiamondDetailID = ""
        txtDesignCharges.Text = "0"
        txtDesignRate.Text = "0"
        _IsSaleReturn = 0
        txtClarity.Text = ""
        txtRBP.Text = "0"
        _RBP = 0
        _Carat = 0
        _ItemTG = 0.0
        _ItemTK = 0.0
        txtDGram.Text = "0.0"
        txtSellingRate.Text = "0"
        txtSellingAmt.Text = "0"
        txtDesignRate.Text = "0"
        txtWhiteCharges.Text = "0"
        txtMountingCharges.Text = "0"
        txtDesignCharges.Text = 0
        txtPlatingCharges.Text = 0
        IsOriginalFixedPrice = 0
        _OriginalFixedPrice = 0
        IsOriginalPriceCarat = 0
        _OriginalPriceCarat = 0
    End Sub
    Private Sub SaleVolumeGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.SaleLooseDiamond.ToString)
        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtSaleLooseDiamondID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpSaleDate.Value)
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
        End If

    End Sub
    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _SaleLooseDiamondID = "0"
        SaleVolumeGenerateFormat()
        _LocationID = ""
        dtpSaleDate.Value = Now
        cboStaff.SelectedIndex = -1
        _CustomerID = ""
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtRemark.Text = ""
        MemDiscountAmount = 0
        DiscountPercent = 0
        txtAllTotalAmt.Text = 0
        txtAllAddOrSub.Text = 0
        txtAllNetAmt.Text = 0
        txtDiscountAmt.Text = 0
        txtPaidAmt.Text = 0
        txtPromotionAmt.Text = 0
        txtPromotionDis.Text = 0
        txtMemberDis.Text = "0"
        txtMemberDisAmt.Text = "0"
        txtValue.Text = 0
        txtPoint.Text = 0
        txtBalanceAmt.Text = "0"
        _PurchaseHeaderID = ""
        txtPurchaseVoucherNo.Text = ""
        txtPurchaseAmount.Text = ""
        txtDifferentAmount.Text = 0
        txtPurchaseAmount.Text = 0
        txtDesignCharges.Text = 0
        _IsAllowDiscount = False
        _IsSearch = False
        If Global_UserLevel = "Administrator" Then
            txtPromotionDis.ReadOnly = False
            txtPromotionDis.BackColor = Drawing.Color.White
        Else
            txtPromotionDis.ReadOnly = True
            txtPromotionDis.BackColor = Drawing.Color.Linen
        End If

        txtPoint.Enabled = False
        txtValue.Enabled = False
        grpMember.Visible = False
        MaxPoint = 0
        TopupPoint = 0
        TopupValue = 0
        RedeemPoint = 0
        RedeemValue = 0
        txtMemberCode.Text = ""
        txtMemberID.Text = ""
        txtMemberName.Text = ""

        If Global_IsUseMember = True Then
            If CheckForInternetConnection() = True Then
                txtMemberCode.Visible = True
                txtMemberID.Visible = True
                txtMemberName.Visible = True
                If Global_IsMemberCustomer Then
                    txtMemberCode.ReadOnly = True
                    txtMemberCode.BackColor = Drawing.Color.Linen
                Else
                    txtMemberCode.ReadOnly = False
                    txtMemberCode.BackColor = Drawing.Color.White
                End If
            Else
                MsgBox("Please Check Internet Connection!")
            End If

        Else
            txtMemberCode.Visible = False
            txtMemberID.Visible = False
            txtMemberName.Visible = False
            txtMemberCode.BackColor = Drawing.Color.Linen
        End If

        IsRedeemInvoice = False
        ' txtPoint.Text = IIf(txtPoint.Text = "", 0, txtPoint.Text)
        DiscountType = ""
        'txtDiscountAmt.Text = 0
        lblRedeemItem.Visible = False
        RedeemSuccess = False
        TopUpSuccess = False
        txtBalanceAmt.Text = 0
        _TransactionID = ""
        InvoiceStatus = 0
        _IsUpdateHeader = False
        _isMaximum = False
        Dim dc As New DataColumn
        _dtItemBarcode = New DataTable
        _dtItemBarcode.Columns.Add("SaleLooseDiamondDetailID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("SaleLooseDiamondID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ForSaleID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemCode", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("GemsCategory", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("GemsName", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("OriginalCode", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("Shape", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("Clarity", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("Color", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("QTY", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("GemsTW", System.Type.GetType("System.Decimal"))

        dc = New DataColumn
        dc.ColumnName = "ItemTG"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtItemBarcode.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "ItemTK"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtItemBarcode.Columns.Add(dc)


        _dtItemBarcode.Columns.Add("SalesRate", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("GemsPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsFixPrice", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("FixPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("TotalAmount", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("AddOrSub", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("NetAmount", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("DesignCharges", System.Type.GetType("System.Int32"))
        _dtItemBarcode.Columns.Add("DesignChargesRate", System.Type.GetType("System.Int32"))
        _dtItemBarcode.Columns.Add("WhiteCharges", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("PlatingCharges", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("MountingCharges", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsSaleReturn", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("SellingRate", System.Type.GetType("System.Int32"))
        _dtItemBarcode.Columns.Add("SellingAmt", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsOriginalFixedPrice", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("OriginalFixedPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsOriginalPriceCarat", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("OriginalPriceCarat", System.Type.GetType("System.Int64"))


        grdDetail.AutoGenerateColumns = False
        grdDetail.ReadOnly = True
        grdDetail.DataSource = _dtItemBarcode

        FormatGridItemDetail()
        txtBarcodeNo.Text = ""

        Dim dcOtherCash As New DataColumn
        _dtOtherCash = New DataTable
        _dtOtherCash.Columns.Add("RecordCashID", System.Type.GetType("System.String"))
        _dtOtherCash.Columns.Add("CashTypeID", System.Type.GetType("System.String"))
        _dtOtherCash.Columns.Add("ExchangeRate", System.Type.GetType("System.Int32"))
        _dtOtherCash.Columns.Add("Amount", System.Type.GetType("System.Int32"))
        _dtOtherCash.Columns.Add("Total", System.Type.GetType("System.Int64"))
        chkOtherCash.Checked = False
        btnOtherCash.Visible = False
        txtOtherCashAmount.Text = 0
        txtPaidAmt.Text = 0
        txtBalanceAmt.Text = 0

        ClearItemCode()
        RedeemID = ""
        _RedeemID = ""
        TempRedeemID = ""
        VoucherPointBalance = 0

    End Sub

    Public Sub FormatGridItemDetail()

        With grdDetail
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40
            .DefaultCellStyle.Font = New Font("Tahoma", 8.25)

            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "SaleLooseDiamondDetailID"
                .DataPropertyName = "SaleLooseDiamondDetailID"
                .Name = "SaleLooseDiamondDetailID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "SaleLooseDiamondID"
                .DataPropertyName = "SaleLooseDiamondID"
                .Name = "SaleLooseDiamondID"
                .Visible = False
            End With
            .Columns.Add(dcID)

            Dim dcForSale As New DataGridViewTextBoxColumn
            With dcForSale
                .HeaderText = "ForSaleID"
                .DataPropertyName = "ForSaleID"
                .Name = "ForSaleID"
                .Visible = False
            End With
            .Columns.Add(dcForSale)

            Dim dcDia As New DataGridViewTextBoxColumn
            With dcDia
                .HeaderText = "ItemCode"
                .DataPropertyName = "ItemCode"
                .Name = "ItemCode"
                .Width = 120
                .Visible = True
            End With
            .Columns.Add(dcDia)

            Dim dcGemsCategory As New DataGridViewTextBoxColumn
            With dcGemsCategory
                .HeaderText = "GemsCategory"
                .DataPropertyName = "GemsCategory"
                .Name = "GemsCategory"
                .Width = 100
                .Visible = True
            End With
            .Columns.Add(dcGemsCategory)

            Dim dcYOrCOrG As New DataGridViewTextBoxColumn
            With dcYOrCOrG
                .HeaderText = "RBP"
                .DataPropertyName = "YOrCOrG"
                .Name = "YOrCOrG"
                .Width = 100
                .Visible = True
            End With
            .Columns.Add(dcYOrCOrG)

            Dim dcItemTG As New DataGridViewTextBoxColumn
            With dcItemTG
                .HeaderText = "Item(G)"
                .DataPropertyName = "ItemTG"
                .Name = "ItemTG"
                .Width = 60
                .Visible = True
                .DefaultCellStyle.Format = "0.000"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcItemTG)

            Dim dcQTY As New DataGridViewTextBoxColumn()
            With dcQTY
                .HeaderText = "QTY"
                .DataPropertyName = "QTY"
                .Name = "QTY"
                .Width = 40
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcQTY)

            Dim dcNetAmount As New DataGridViewTextBoxColumn()
            With dcNetAmount
                .HeaderText = "NetAmount"
                .DataPropertyName = "NetAmount"
                .Name = "NetAmount"
                .Width = 75
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcNetAmount)

            Dim dcGemsCategoryID As New DataGridViewTextBoxColumn
            With dcGemsCategoryID
                .HeaderText = "GemsCategoryID"
                .DataPropertyName = "GemsCategoryID"
                .Name = "GemsCategoryID"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcGemsCategoryID)

            Dim dcGemsName As New DataGridViewTextBoxColumn
            With dcGemsName
                .HeaderText = "GemsName"
                .DataPropertyName = "GemsName"
                .Name = "GemsName"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcGemsName)

           

            Dim dcItemTK As New DataGridViewTextBoxColumn
            With dcItemTK
                .HeaderText = "ItemTK"
                .DataPropertyName = "ItemTK"
                .Name = "ItemTK"
                .Visible = False
            End With
            .Columns.Add(dcItemTK)

            Dim dcColor As New DataGridViewTextBoxColumn
            With dcColor
                .HeaderText = "Color"
                .DataPropertyName = "Color"
                .Name = "Color"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcColor)

            Dim dcShape As New DataGridViewTextBoxColumn
            With dcShape
                .HeaderText = "Shape"
                .DataPropertyName = "Shape"
                .Name = "Shape"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcShape)

            Dim dcClarity As New DataGridViewTextBoxColumn
            With dcClarity
                .HeaderText = "Clarity"
                .DataPropertyName = "Clarity"
                .Name = "Clarity"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcClarity)

            Dim dcCurrent As New DataGridViewTextBoxColumn
            With dcCurrent
                .HeaderText = "Rate"
                .DataPropertyName = "SalesRate"
                .Name = "SalesRate"
                .Visible = False
                .Width = 70
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcCurrent)

            Dim dcGemsFee As New DataGridViewTextBoxColumn
            With dcGemsFee
                .HeaderText = "GemsPrice"
                .DataPropertyName = "GemsPrice"
                .Name = "GemsPrice"
                .Visible = False
            End With
            .Columns.Add(dcGemsFee)

            Dim dcIsFixPrice As New DataGridViewTextBoxColumn()
            With dcIsFixPrice
                .HeaderText = "IsFixPrice"
                .DataPropertyName = "IsFixPrice"
                .Name = "IsFixPrice"
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcIsFixPrice)

            Dim dcFixPrice As New DataGridViewTextBoxColumn()
            With dcFixPrice
                .HeaderText = "FixPrice"
                .DataPropertyName = "FixPrice"
                .Name = "FixPrice"
                .Width = 100
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcFixPrice)

            Dim dcIsOriginalFixedPrice As New DataGridViewTextBoxColumn()
            With dcIsOriginalFixedPrice
                .HeaderText = "IsOriginalFixedPrice"
                .DataPropertyName = "IsOriginalFixedPrice"
                .Name = "IsOriginalFixedPrice"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcIsOriginalFixedPrice)

            Dim dcOriginalFixedPrice As New DataGridViewTextBoxColumn()
            With dcOriginalFixedPrice
                .HeaderText = "OriginalFixedPrice"
                .DataPropertyName = "OriginalFixedPrice"
                .Name = "OriginalFixedPrice"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOriginalFixedPrice)

            Dim dcIsOriginalPriceCarat As New DataGridViewTextBoxColumn()
            With dcIsOriginalPriceCarat
                .HeaderText = "IsOriginalPriceCarat"
                .DataPropertyName = "IsOriginalPriceCarat"
                .Name = "IsOriginalPriceCarat"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcIsOriginalPriceCarat)

            Dim dcOriginalPriceCarat As New DataGridViewTextBoxColumn()
            With dcOriginalPriceCarat
                .HeaderText = "OriginalPriceCarat"
                .DataPropertyName = "OriginalPriceCarat"
                .Name = "OriginalPriceCarat"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOriginalPriceCarat)

            Dim dcPrice As New DataGridViewTextBoxColumn()
            With dcPrice
                .HeaderText = "TotalAmount"
                .DataPropertyName = "TotalAmount"
                .Name = "TotalAmount"
                .Width = 75
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPrice)

            Dim dcAdd As New DataGridViewTextBoxColumn()
            With dcAdd
                .HeaderText = "AddOrSub"
                .DataPropertyName = "AddOrSub"
                .Name = "AddOrSub"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcAdd)

            Dim dcDesignCharges As New DataGridViewTextBoxColumn()
            With dcDesignCharges
                .HeaderText = "DesignCharges"
                .DataPropertyName = "DesignCharges"
                .Name = "DesignCharges"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDesignCharges)

            Dim dcDesignChargesRate As New DataGridViewTextBoxColumn()
            With dcDesignChargesRate
                .HeaderText = "DesignChargesRate"
                .DataPropertyName = "DesignChargesRate"
                .Name = "DesignChargesRate"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDesignChargesRate)

            Dim dcOrgCode As New DataGridViewTextBoxColumn()
            With dcOrgCode
                .HeaderText = "OriginalCode"
                .DataPropertyName = "OriginalCode"
                .Name = "OriginalCode"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOrgCode)

            Dim dcPriceCode As New DataGridViewTextBoxColumn()
            With dcPriceCode
                .HeaderText = "GemsTW"
                .DataPropertyName = "GemsTW"
                .Name = "GemsTW"
                .Visible = False
                .DefaultCellStyle.Format = "0.000"
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPriceCode)

            Dim dcWhiteCharges As New DataGridViewTextBoxColumn()
            With dcWhiteCharges
                .HeaderText = "WhiteCharges"
                .DataPropertyName = "WhiteCharges"
                .Name = "WhiteCharges"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcWhiteCharges)

            Dim dcPlatingCharges As New DataGridViewTextBoxColumn()
            With dcPlatingCharges
                .HeaderText = "PlatingCharges"
                .DataPropertyName = "PlatingCharges"
                .Name = "PlatingCharges"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPlatingCharges)

            Dim dcMountingCharges As New DataGridViewTextBoxColumn()
            With dcMountingCharges
                .HeaderText = "MountingCharges"
                .DataPropertyName = "MountingCharges"
                .Name = "MountingCharges"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMountingCharges)

            Dim dcchkIsReturn As New DataGridViewCheckBoxColumn()
            dcchkIsReturn.HeaderText = "IsSaleReturn"
            dcchkIsReturn.DataPropertyName = "IsSaleReturn"
            dcchkIsReturn.Name = "IsSaleReturn"
            dcchkIsReturn.Width = 90
            dcchkIsReturn.Visible = False
            dcchkIsReturn.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcchkIsReturn)

            Dim dcSellingRate As New DataGridViewTextBoxColumn()
            With dcSellingRate
                .HeaderText = "SellingRate"
                .DataPropertyName = "SellingRate"
                .Name = "SellingRate"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcSellingRate)

            Dim dcSellingAmt As New DataGridViewTextBoxColumn()
            With dcSellingAmt
                .HeaderText = "SellingAmt"
                .DataPropertyName = "SellingAmt"
                .Name = "SellingAmt"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcSellingAmt)

        End With


    End Sub

    Private Sub ShowCurrentPrice(ByVal objCurrentPrice As CurrentPriceInfo)
        txtCurrentPrice.Text = objCurrentPrice.SalesRate
    End Sub

    Private Sub SearchSale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchSale.Click
        btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objSaleLooseDiamondHeader As New SaleLooseDiamondHeaderInfo


        dt = objSaleLooseDiamondController.GetAllSaleLooseDiamond()
        DataItem = DirectCast(SearchData.FindFast(dt, "Sale Loose Diamond Item List"), DataRow)

        If DataItem IsNot Nothing Then
            ' _IsGemInDB = True
            _SaleLooseDiamondID = DataItem.Item("VoucherNo").ToString()
            objSaleLooseDiamondHeader = objSaleLooseDiamondController.GetSaleLooseDiamondHeaderByID(_SaleLooseDiamondID)
            _IsSearch = True
            _IsUpdateHeader = True
            _dtItemBarcode.Rows.Clear()
            _dtItemBarcode = objSaleLooseDiamondController.GetSaleLooseDiamondDetailByID(_SaleLooseDiamondID)
            grdDetail.DataSource = _dtItemBarcode
            _dtOtherCash = objSalesInvoiceController.GetOtherCashDataByVoucherNo(_SaleLooseDiamondID)
            ShowSaleDiaBarcodeData(objSaleLooseDiamondHeader)
            btnDelete.Enabled = True
        Else
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString
        End If
    End Sub
    Private Sub ShowSaleDiaBarcodeData(ByVal objSaleHeader As SaleLooseDiamondHeaderInfo)

        With objSaleHeader
            dtpSaleDate.Value = .SaleDate
            txtSaleLooseDiamondID.Text = .SaleLooseDiamondID
            cboStaff.SelectedValue = .StaffID
            _CustomerID = .CustomerID
            txtCustomerCode.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerCode
            txtCustomer.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerName
            txtAddress.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerAddress

            'For Member
            If Global_IsUseMember = True Then
                RedeemID = CStr(.RedeemID)
                ' RedeemID = TempRedeemID.Replace(",", "|")

                txtPoint.Text = .RedeemPoint
                RedeemPoint = .RedeemPoint
                txtValue.Text = .RedeemValue
                RedeemValue = .RedeemValue
                TopupPoint = .TopupPoint
                TopupValue = .TopupValue
                _OldRedeemTopupPoint = .RedeemPoint
                _OldTopupPoint = .TopupPoint
                _TransactionID = .TransactionID
            Else
                RedeemID = ""
                txtPoint.Text = 0
                txtValue.Text = 0
                RedeemPoint = 0
                RedeemValue = 0
                TopupPoint = 0
                TopupValue = 0
                _OldRedeemTopupPoint = 0
                _OldTopupPoint = 0
            End If
            InvoiceStatus = .InvoiceStatus
            If InvoiceStatus = 1 Then

                txtValue.Text = 0
            Else
                txtValue.Text = .RedeemValue


            End If
            txtAllTotalAmt.Text = .TotalAmount
            txtAllAddOrSub.Text = .AddOrSub
            _IsAllowDiscount = True
            txtMemberDis.Text = .MemberDis
            txtMemberDisAmt.Text = .MemberDiscountAmt
            txtDiscountAmt.Text = .DiscountAmount
            txtPromotionDis.Text = .PromotionDiscount
            txtPromotionAmt.Text = CLng(CLng(txtAllTotalAmt.Text) * (CLng(txtPromotionDis.Text) / 100))
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text)))
            _PurchaseHeaderID = .PurchaseHeaderID
            txtPurchaseVoucherNo.Text = .PurchaseHeaderID
            txtPurchaseAmount.Text = .PurchaseAmount
            txtPaidAmt.Text = .PaidAmount

            If _PurchaseHeaderID <> "" Then
                txtSRTaxPer.Enabled = True
                txtSRTaxAmt.Enabled = True
                lblPer.Enabled = True
                lblSRTax.Enabled = True
                txtDifferentAmount.Text = Format(Val(CLng(txtAllNetAmt.Text) - CLng(txtPurchaseAmount.Text)), "###,##0.##")
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            Else
                txtDifferentAmount.Text = "0"
                txtPurchaseAmount.Text = "0"
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If


            txtRemark.Text = .Remark
            _IsAllowDiscount = False

            chkOtherCash.Checked = .IsOtherCash
            txtSRTaxPer.Text = .SRTaxPer
            txtSRTaxAmt.Text = Format(Val(.SRTaxAmt), "###,##0.##")
            txtPaidAmt.Text = Format(Val(.PaidAmount), "###,##0.##")

            If chkOtherCash.Checked Then
                btnOtherCash.Visible = True
            Else
                btnOtherCash.Visible = False
            End If
            txtOtherCashAmount.Text = Format(Val(.OtherCashAmount), "###,##0.##")

            'For Member
            txtMemberCode.Text = IIf(.MemberCode = "", "", .MemberCode)
            If Global_IsUseMember = True And txtMemberCode.Text <> "" Then

                txtMemberCode.Text = .MemberCode
                MCode = .MemberCode
                txtMemberID.Text = .MemberID
                txtMemberName.Text = .MemberName

                GetPointBalanceByMemberID(MCode)

            Else
                txtMemberCode.Text = ""
                MCode = ""
                txtMemberID.Text = ""
                txtMemberName.Text = ""

            End If
            IsRedeemInvoice = .IsRedeemInvoice
            'For Oppurnity Type Item must  be Redeem Invoice
            If RedeemPoint = 0 And RedeemValue = 0 And IsRedeemInvoice = True Then
                lblRedeemItem.Visible = True
            End If
        End With
    End Sub


    'Public Function GoldQualityIsGramRate() As Boolean
    '    Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
    '    GoldQualityInfo = _GoldQualityController.GetGoldQuality(_GoldQualityID)
    '    Return GoldQualityInfo.IsGramRate
    'End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim obj As New CommonInfo.SaleLooseDiamondHeaderInfo
        Dim _PurchaseHeaderID As String
        Dim dtPurchase As New DataTable

        obj = objSaleLooseDiamondController.GetSaleLooseDiamondByID(_SaleLooseDiamondID)
        _PurchaseHeaderID = obj.PurchaseHeaderID

        If _PurchaseHeaderID = "" Then
            dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

            If dt.Rows.Count() = 0 Then
                MsgBox("Please Select Sale Loose Diamond Voucher First!", MsgBoxStyle.Information, AppName)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                    '_GoldQualityID = dr.Item("GoldQualityID")
                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                Next
            End If
        Else
            dt = objSaleLooseDiamondController.GetSaleLooseDiamondPrint(_SaleLooseDiamondID)

            If dt.Rows.Count() = 0 Then
                MsgBox("Please Select Sale Loose Diamond Voucher First!", MsgBoxStyle.Information, AppName)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                    '_GoldQualityID = dr.Item("GoldQualityID")
                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                Next
            End If
            dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleLooseDiamond(_SaleLooseDiamondID)
        End If
        dt.Rows(0).Item("PointBalance") = VoucherPointBalance
        frmPrint.PrintSaleLooseDiamond(dt, dtPurchase)
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub

    Private Sub cboStaff_Click(sender As Object, e As EventArgs) Handles cboStaff.Click
        GetcboStaff()
    End Sub
    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub

    Private Sub txtDiscountAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscountAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDiscountAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscountAmt.TextChanged
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If IsAllowAddOrSub() = False Then
            txtDiscountAmt.Text = "0"
        End If
        CalculateFinalAmount()
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

    Private Sub CalculateBalance()
        If txtAllNetAmt.Text <> "" And txtPaidAmt.Text <> "" And txtDiscountAmt.Text <> "" Then
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtDiscountAmt.Text)))
        End If

    End Sub

    Private Sub txtPaidAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPaidAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPaidAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaidAmt.TextChanged
        If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"

        If _PurchaseHeaderID <> "" Then
            txtBalanceAmt.Text = CStr(CLng(txtDifferentAmount.Text) - CLng(txtPaidAmt.Text))
        Else
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text))
        End If
    End Sub

    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
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
                If Global_IsMemberCustomer Then
                    txtMemberCode.Text = DataItem("MemberCode")
                End If
            Else
                MsgBox("This Customer is Inactive!", MsgBoxStyle.Information, AppName)
                _CustomerID = ""
                txtCustomerCode.Text = ""
                txtCustomer.Text = ""
                txtAddress.Text = ""
                txtMemberCode.Text = ""
                Exit Sub
            End If

        End If
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


    Private Sub txtBarcodeNo_TextChanged(sender As Object, e As EventArgs) Handles txtBarcodeNo.TextChanged
        Dim dtSaleItem As New DataTable
        Dim objSItem As SalesItemInfo
        If txtBarcodeNo.Text <> "" Then
            _BarcodeNo = txtBarcodeNo.Text
            'If ChkIsSolidVolume.Checked = True Then
            objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo, " And F.IsExit = '0' AND F.IsOrder='0' AND IsLooseDiamond='1' ")
            'Else
            '    objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo, " And F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='1' ")
            'End If
            ShowForSaleBarcodeData(objSItem)
        Else
            ClearItemCode()
        End If
    End Sub
    Private Sub ShowForSaleBarcodeData(ByVal objSItem As SalesItemInfo)
        Dim objCurrentPrice As New IntDiamondPriceRateInfo
        Dim GoldQualityInfo As New GoldQualityInfo

        With objSItem
            _ForSaleID = .ForSaleID

            _GemsCategoryID = .SDGemsCategoryID
            'txtGemsCategory.Text = _ItemCategoryController.GetItemCategory(_GemsCategoryID).ItemCategory
            txtGemsCategory.Text = _GemsCat.GetGemsCategory(_GemsCategoryID).GemsCategory
            txtDescription.Text = .SDGemsName

            txtColor.Text = .Color
            txtShape.Text = .Shape
            txtClarity.Text = .Clarity
            txtOriginalCode.Text = .OriginalCode
            _ItemTK = Math.Round(.LossItemTK, 3)
            _ItemTG = Math.Round(.LossItemTG, 3)
            _RBP = .SDYOrCOrG
            'txtRBP.Text = .SDYOrCOrG
            'txtDGram.Text = Format(_ItemTG, "0.000")
            '_Carat = Format(CDec(_ItemTG) * Global_GramToKarat, "0.0")
            _GemsTW = .SDGemsTW
            'GoldQualityForTextChange()
            _QTY = .LossQTY
            If _SaleLooseDiamondDetailID = "" Then
                If _ForSaleID = "" Then
                    lblWeight.Text = ""
                Else
                    'If ChkIsSolidVolume.Checked = True Then
                    'lblWeight.Text = CStr(_ItemTG) + " TG  -  " + CStr(_ItemTK) + " TK" + CStr(IIf(.IsFixPrice = False, "", "  -  ")) + CStr(IIf(.IsFixPrice = False, "", .FixPrice))
                    'Else
                    lblWeight.Text = CStr(.LossQTY) + "  -  " + CStr(_ItemTG) + " TG  -  " + CStr(_ItemTK) + " TK" + CStr(IIf(.IsFixPrice = False, "", "  -  ")) + CStr(IIf(.IsFixPrice = False, "", .FixPrice))
                    'End If
                End If
            Else
                lblWeight.Text = ""
            End If

            objCurrentPrice = _CurrentController.GetIntDiamondData(_Carat)
            txtCurrentPrice.Text = objCurrentPrice.PriceRate
            
            chkIsFixPrice.Checked = .IsFixPrice

            If (.IsFixPrice = True) Then
                isFixPrice = True
                txtGemsPrice.Text = "0"
                lblDonePrice.Visible = True
                txtSaleFixPrice.Visible = True
                txtSaleFixPrice.Text = .FixPrice
            Else
                isFixPrice = False
                lblDonePrice.Visible = False
                txtSaleFixPrice.Visible = False
            End If
            txtWhiteCharges.Text = Format(Val(.WhiteCharges), "###,##0.##")
            txtPlatingCharges.Text = Format(Val(.PlatingCharges), "###,##0.##")
            txtMountingCharges.Text = Format(Val(.MountingCharges), "###,##0.##")
            txtDesignCharges.Text = Format(Val(.DesignCharges), "###,##0.##")
            chkIsFixPrice.Checked = Format(Val(.IsFixPrice), "###,##0.##")

            IsOriginalFixedPrice = .IsOriginalFixedPrice
            _OriginalFixedPrice = .OriginalFixedPrice
            IsOriginalPriceCarat = .IsOriginalPriceCarat
            _OriginalPriceCarat = .OriginalPriceCarat

        End With

    End Sub

    'Private Sub GoldQualityForTextChange()
    '    If _IsGram = True Then
    '        txtItemK.ReadOnly = True
    '        txtItemP.ReadOnly = True
    '        txtItemY.ReadOnly = True
    '        txtItemTG.ReadOnly = False
    '        txtItemTG.TabIndex = 14
    '        txtWasteTG.TabIndex = 15
    '        txtItemK.TabStop = False
    '        txtItemP.TabStop = False
    '        txtItemY.TabStop = False
    '        txtWasteK.TabStop = False
    '        txtWasteP.TabStop = False
    '        txtWasteY.TabStop = False
    '        txtItemTG.TabStop = True
    '        txtWasteTG.TabStop = True

    '        txtItemK.BackColor = Color.Linen
    '        txtItemP.BackColor = Color.Linen
    '        txtItemY.BackColor = Color.Linen
    '        txtItemTG.BackColor = Color.White


    '        txtWasteK.ReadOnly = True
    '        txtWasteP.ReadOnly = True
    '        txtWasteY.ReadOnly = True
    '        txtWasteTG.ReadOnly = False


    '        txtWasteK.BackColor = Color.Linen
    '        txtWasteP.BackColor = Color.Linen
    '        txtWasteY.BackColor = Color.Linen
    '        txtWasteTG.BackColor = Color.White
    '    Else
    '        txtItemK.ReadOnly = False
    '        txtItemP.ReadOnly = False
    '        txtItemY.ReadOnly = False
    '        txtItemTG.ReadOnly = True
    '        txtItemK.TabIndex = 14
    '        txtItemP.TabIndex = 15
    '        txtItemY.TabIndex = 16
    '        txtWasteK.TabIndex = 17
    '        txtWasteP.TabIndex = 18
    '        txtWasteY.TabIndex = 19
    '        txtItemTG.TabStop = False
    '        txtWasteTG.TabStop = False
    '        txtItemK.TabStop = True
    '        txtItemP.TabStop = True
    '        txtItemY.TabStop = True
    '        txtWasteK.TabStop = True
    '        txtWasteP.TabStop = True
    '        txtWasteY.TabStop = True

    '        txtItemK.BackColor = Color.White
    '        txtItemP.BackColor = Color.White
    '        txtItemY.BackColor = Color.White
    '        txtItemTG.BackColor = Color.Linen

    '        txtWasteK.ReadOnly = False
    '        txtWasteP.ReadOnly = False
    '        txtWasteY.ReadOnly = False
    '        txtWasteTG.ReadOnly = True

    '        txtWasteK.BackColor = Color.White
    '        txtWasteP.BackColor = Color.White
    '        txtWasteY.BackColor = Color.White
    '        txtWasteTG.BackColor = Color.Linen
    '    End If
    'End Sub

    'Private Sub txtRBP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRBP.KeyPress
    '    MyBase.ValidateNumeric(sender, e, True)
    'End Sub
    Private Sub txtRBP_LostFocus(sender As Object, e As EventArgs) Handles txtRBP.LostFocus
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
        If txtRBP.Text = "0" Then
            Exit Sub
        End If

        VarWeight = CStr(txtRBP.Text)
        'If VarWeight = "0" Then
        '    Exit Sub
        'End If

        If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
            MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
            txtRBP.Text = "0"
            txtDGram.Text = "0"
            _RBP = "0"
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
                _RBP = "0"
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
            _RBP = txtRBP.Text
            '_ItemTG = Format(Math.Round(CDec(gram), 3), "0.000")
            txtDGram.Text = Format(Math.Round(CDec(_ItemTG), 3), "0.000")

        End If

        objCurrentPrice = _CurrentController.GetIntDiamondData(_Carat)
        txtCurrentPrice.Text = objCurrentPrice.PriceRate

        CalculateGemsPrice()

    End Sub
    'Private Sub txtRBP_TextChanged(sender As Object, e As EventArgs) Handles txtRBP.TextChanged
    '    If txtRBP.Text = "" Then txtRBP.Text = "0"

    '    If Val(txtItemTG.Text.Trim) >= 0.0 Then
    '        If _IsGram = True Then
    '            CalculateItemWeightForGram()
    '        End If
    '        CalculateTotalWeight()
    '        CalculateGemsPrice()
    '    End If
    'End Sub
    'Private Sub CalculateGoldWeight()
    '    Dim weightY As Decimal = 0
    '    Dim weightP As Integer = 0
    '    Dim weightK As Integer = 0

    '    If CStr(_ItemTG) <> "" Or CStr(_GemsTG) <> "" Then

    '        If _ItemTG <> 0.0 Or _GemsTG <> 0.0 Then
    '            Dim ItemWeight As New CommonInfo.GoldWeightInfo
    '            Dim GemWeight As New CommonInfo.GoldWeightInfo
    '            Dim GoldWeight As New CommonInfo.GoldWeightInfo

    '            ItemWeight.WeightK = CDec(txtItemK.Text)
    '            ItemWeight.WeightP = CDec(txtItemP.Text)
    '            ItemWeight.WeightY = CDec(txtItemY.Text)

    '            GemWeight.WeightK = CDec(txtGemKForSale.Text)
    '            GemWeight.WeightP = CDec(txtGemPForSale.Text)
    '            GemWeight.WeightY = CDec(txtGemYForSale.Text)

    '            If ItemWeight.WeightY < GemWeight.WeightY Then
    '                weightY = 8 + ItemWeight.WeightY - GemWeight.WeightY
    '                ItemWeight.WeightP = ItemWeight.WeightP - 1
    '            Else
    '                weightY = ItemWeight.WeightY - GemWeight.WeightY
    '            End If

    '            If ItemWeight.WeightP < GemWeight.WeightP Then
    '                weightP = 16 + ItemWeight.WeightP - GemWeight.WeightP
    '                ItemWeight.WeightK = ItemWeight.WeightK - 1
    '            Else
    '                weightP = ItemWeight.WeightP - GemWeight.WeightP
    '            End If

    '            weightK = ItemWeight.WeightK - GemWeight.WeightK

    '            GoldWeight.WeightY = weightY
    '            GoldWeight.WeightP = weightP
    '            GoldWeight.WeightK = weightK

    '            txtGoldK.Text = Format(GoldWeight.WeightK, "0")
    '            txtGoldP.Text = Format(GoldWeight.WeightP, "0")
    '            txtGoldY.Text = Math.Round(GoldWeight.WeightY, 1)

    '            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
    '            _GoldTK = GoldWeight.GoldTK
    '            GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
    '            _GoldTG = GoldWeight.Gram
    '            txtGoldTG.Text = Math.Round(_ItemTG - _GemsTG, 3)
    '            txtGoldTK.Text = Math.Round(_ItemTK - _GemsTK, 3)
    '            _GoldTK = _ItemTK - _GemsTK
    '            _GoldTG = _ItemTG - _GemsTG

    '        Else
    '            _GoldTG = 0.0
    '            _GoldTK = 0.0

    '            txtGoldK.Text = "0"
    '            txtGoldP.Text = "0"
    '            txtGoldY.Text = "0"
    '        End If
    '    End If

    'End Sub

    Private Sub CalculateGemsPrice()
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"
        If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
        If txtQty.Text = "" Then txtQty.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtDesignRate.Text = "" Then txtDesignRate.Text = "0"
        If txtSellingAmt.Text = "" Then txtSellingAmt.Text = "0"
        Dim _DesignCharges As Integer = 0

        If (chkIsFixPrice.Checked = True) Then
            txtGemsPrice.Text = "0"
            txtTotalAmt.Text = CStr((CInt(txtSaleFixPrice.Text) * CInt(txtQty.Text)) + CLng(txtDesignCharges.Text) + CLng(txtPlatingCharges.Text) + CLng(txtMountingCharges.Text) + CLng(txtWhiteCharges.Text))
        Else
            _GemsPrice = CLng(txtCurrentPrice.Text) * CDec(_Carat)
            ' _DesignCharges = CStr(CLng(txtDesignRate.Text) * (CDec(_ItemTG)))
            txtGemsPrice.Text = CLng(_GemsPrice)
            txtTotalAmt.Text = CStr(CLng(txtGemsPrice.Text) + CLng(txtDesignCharges.Text) + CLng(txtPlatingCharges.Text) + CLng(txtMountingCharges.Text) + CLng(txtWhiteCharges.Text) + CLng(txtSellingAmt.Text))
            'txtDesignCharges.Text = Format(_DesignCharges, "###,##0.##")
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtBarcodeNo.Focus()
        txtBarcodeNo.Text = ""
        ClearItemCode()
    End Sub

    Private Sub txtGoldPrice_TextChanged(sender As Object, e As EventArgs)
        If txtSaleFixPrice.Text = "" Then txtSaleFixPrice.Text = "0"
        If txtSaleFixPrice.Text.Trim >= 0 Then
            CalculateGemsPrice()
        End If
    End Sub

    Private Sub txtTotalAmt_TextChanged(sender As Object, e As EventArgs) Handles txtTotalAmt.TextChanged
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        'CalculateTotalAmount()
        txtNetAmt.Text = txtTotalAmt.Text
    End Sub

    Private Sub txtNetAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtNetAmt.TextChanged
        If txtNetAmt.Text = "" Then
            txtNetAmt.Text = "0"
        End If
        If txtTotalAmt.Text = "" Then
            txtTotalAmt.Text = "0"
        End If

        'If txtNetAmt.Text = "0" Then
        '    txtTotalAmt.Text = "0"
        'Else
        If (txtNetAmt.Text <> "") And (txtTotalAmt.Text <> "") Then
            txtAddSub.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtTotalAmt.Text))
        End If
        'End If

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim _OldQTY As Integer = 0
        Dim _OldTG As Decimal = 0.0
        _IsSearch = False
        If txtBarcodeNo.Text = "" Then
            MsgBox("Please Enter ItemCode!", MsgBoxStyle.Information, "Data Require!")
            Exit Sub
        End If
        If _IsUpdate Then
            Dim dt As DataTable
            dt = objSaleLooseDiamondController.GetSaleLooseDiamondDetailByDetailID(_SaleLooseDiamondDetailID)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    _OldQTY = dr.Item("QTY")
                Next
            Else
                _OldQTY = 0
            End If

            If txtQty.Text > _QTY + _OldQTY Or txtQty.Text = "0" Then
                MsgBox("Please Check Quantity!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If

            Dim dtLossWeight As DataTable
            dtLossWeight = objSaleLooseDiamondController.GetSaleLooseDiamondDetailByDetailID(_SaleLooseDiamondDetailID)
            If dtLossWeight.Rows.Count > 0 Then
                For Each dr As DataRow In dtLossWeight.Rows
                    _OldTG = dr.Item("ItemTG")
                Next
            Else
                _OldTG = 0
            End If

            If txtDGram.Text > _ItemTG + _OldTG Or txtDGram.Text = "0" Then
                MsgBox("Please Check Weight!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If

        Else
            If txtQty.Text > _QTY Or txtQty.Text = "0" Then
                MsgBox("Please Check Quantity!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If
        End If

            If _dtItemBarcode.Rows.Count > 0 And _IsUpdate = False Then
                For Each dr As DataRow In _dtItemBarcode.Rows
                    If dr.RowState <> DataRowState.Deleted Then
                        If dr("ItemCode") = txtBarcodeNo.Text Then
                            MsgBox("Duplicate BarcodeNo!", MsgBoxStyle.Information, "Duplicate Data!")
                            Exit Sub
                        End If
                    End If
                Next
            End If

            If _ItemTG = "0.0" Or _ItemTG <= 0 Then
                MsgBox("Please Enter  Item Weight!", MsgBoxStyle.Information, "Data Require!")
                Exit Sub
            End If

            If _IsUpdate Then
                UpdateItem(_SaleLooseDiamondDetailID)
                txtBarcodeNo.Text = ""
                ClearItemCode()
            Else

                If btnAdd.Text = "Add" Then
                    _SaleLooseDiamondDetailID = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SalesVolumeDetail, EnumSetting.GenerateKeyType.SalesVolumeDetail.ToString, dtpSaleDate.Value)
                    InsertItem(_SaleLooseDiamondDetailID)
                End If
                txtBarcodeNo.Text = ""
                ClearItemCode()
            End If

            CalculategrAlldTotalAmount()

    End Sub
    Private Sub CalculategrAlldTotalAmount()
        _AllTotalAmount = 0

        For j As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(j).IsNewRow Then
                _AllTotalAmount += CDec(grdDetail.Rows(j).Cells("NetAmount").FormattedValue)
            End If
        Next
        txtAllTotalAmt.Text = _AllTotalAmount
        txtAllAddOrSub.Text = "0"
        txtDiscountAmt.Text = "0"
        txtPromotionDis.Text = "0"
        txtPromotionAmt.Text = "0"
        CalculateFinalAmount()
    End Sub
    Public Sub InsertItem(ByVal _SaleLooseDiamondDetailID As String)
        Dim drItem As DataRow

        drItem = _dtItemBarcode.NewRow
        drItem.Item("SaleLooseDiamondDetailID") = _SaleLooseDiamondDetailID
        drItem.Item("SaleLooseDiamondID") = _SaleLooseDiamondID
        drItem.Item("ForSaleID") = _ForSaleID
        drItem.Item("ItemCode") = txtBarcodeNo.Text
        drItem.Item("GemsCategoryID") = _GemsCategoryID
        drItem.Item("GemsName") = txtDescription.Text
        drItem.Item("GemsCategory") = txtGemsCategory.Text
        drItem.Item("OriginalCode") = txtOriginalCode.Text
        drItem.Item("Shape") = txtShape.Text
        drItem.Item("Color") = txtColor.Text
        drItem.Item("Clarity") = txtClarity.Text
        drItem.Item("ItemTG") = _ItemTG
        drItem.Item("ItemTK") = _ItemTK
        drItem.Item("SalesRate") = txtCurrentPrice.Text
        drItem.Item("GemsPrice") = txtGemsPrice.Text
        drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
        drItem.Item("FixPrice") = txtSaleFixPrice.Text
        drItem.Item("TotalAmount") = txtTotalAmt.Text
        drItem.Item("AddOrSub") = txtAddSub.Text
        drItem.Item("NetAmount") = txtNetAmt.Text
        drItem.Item("QTY") = txtQty.Text
        drItem.Item("DesignCharges") = CInt(txtDesignCharges.Text)
        drItem.Item("DesignChargesRate") = txtDesignRate.Text
        drItem.Item("PlatingCharges") = txtPlatingCharges.Text
        drItem.Item("WhiteCharges") = txtWhiteCharges.Text
        drItem.Item("MountingCharges") = txtMountingCharges.Text
        drItem.Item("SellingRate") = IIf(txtSellingRate.Text = "", 0, txtSellingRate.Text)
        drItem.Item("SellingAmt") = IIf(txtSellingAmt.Text = "", 0, txtSellingAmt.Text)
        drItem.Item("GemsTW") = _GemsTW
        drItem.Item("YOrCOrG") = txtRBP.Text
        drItem.Item("IsOriginalFixedPrice") = IsOriginalFixedPrice
        drItem.Item("OriginalFixedPrice") = _OriginalFixedPrice
        drItem.Item("IsOriginalPriceCarat") = IsOriginalPriceCarat
        drItem.Item("OriginalPriceCarat") = _OriginalPriceCarat
        drItem.Item("IsSaleReturn") = _IsSaleReturn
        drItem.Item("GemsTW") = _GemsTW

        _dtItemBarcode.Rows.Add(drItem)
        grdDetail.DataSource = _dtItemBarcode

        'Dim drDiamond As DataRow
        'For Each dr As DataRow In _dtSaleLooseDiamondItem.Rows
        '    drDiamond = _dtAllDiamond.NewRow()
        '    drDiamond("SalesInvoiceGemItemID") = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SaleInvoiceGemItem, CommonInfo.EnumSetting.GenerateKeyType.SaleInvoiceGemItem.ToString, Now.Date)
        '    drDiamond("@SaleInvoiceDetailID") = _SaleInvoiceDetailID
        '    drDiamond("@GemsCategoryID") = dr("@GemsCategoryID")
        '    drDiamond("GemsName") = dr("GemsName")
        '    drDiamond("GemsK") = dr("GemsK")
        '    drDiamond("GemsP") = dr("GemsP")
        '    drDiamond("GemsY") = dr("GemsY")
        '    drDiamond("GemsTK") = dr("GemsTK")
        '    drDiamond("GemsTG") = dr("GemsTG")
        '    drDiamond("YOrCOrG") = dr("YOrCOrG")
        '    drDiamond("GemsTW") = dr("GemsTW")
        '    drDiamond("Qty") = dr("Qty")
        '    drDiamond("UnitPrice") = dr("UnitPrice")
        '    drDiamond("Type") = dr("Type")
        '    drDiamond("Amount") = dr("Amount")
        '    drDiamond("GemsRemark") = dr("GemsRemark")

        '    _dtAllDiamond.Rows.Add(drDiamond)

    End Sub

    Public Sub UpdateItem(ByVal _SaleLooseDiamondDetailID As String)
        Dim drItem As DataRow
        drItem = _dtItemBarcode.Rows(grdDetail.CurrentRow.Index)

        If Not IsNothing(drItem) Then
            drItem.Item("SaleLooseDiamondDetailID") = _SaleLooseDiamondDetailID
            drItem.Item("SaleLooseDiamondID") = _SaleLooseDiamondID
            drItem.Item("ForSaleID") = _ForSaleID
            drItem.Item("ItemCode") = txtBarcodeNo.Text
            drItem.Item("GemsCategoryID") = _GemsCategoryID
            drItem.Item("GemsName") = IIf(txtDescription.Text = "", "-", txtDescription.Text)
            drItem.Item("GemsCategory") = txtGemsCategory.Text
            drItem.Item("OriginalCode") = txtOriginalCode.Text
            drItem.Item("Shape") = txtShape.Text
            drItem.Item("Color") = txtColor.Text
            drItem.Item("Clarity") = txtClarity.Text
            drItem.Item("ItemTG") = _ItemTG
            drItem.Item("ItemTK") = _ItemTK
            drItem.Item("SalesRate") = txtCurrentPrice.Text
            drItem.Item("GemsPrice") = txtGemsPrice.Text
            drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
            drItem.Item("FixPrice") = txtSaleFixPrice.Text
            drItem.Item("TotalAmount") = txtTotalAmt.Text
            drItem.Item("AddOrSub") = txtAddSub.Text
            drItem.Item("NetAmount") = txtNetAmt.Text
            drItem.Item("QTY") = txtQty.Text
            drItem.Item("DesignCharges") = CInt(txtDesignCharges.Text)
            drItem.Item("DesignChargesRate") = txtDesignRate.Text
            drItem.Item("PlatingCharges") = txtPlatingCharges.Text
            drItem.Item("WhiteCharges") = txtWhiteCharges.Text
            drItem.Item("MountingCharges") = txtMountingCharges.Text
            drItem.Item("SellingRate") = IIf(txtSellingRate.Text = "", 0, txtSellingRate.Text)
            drItem.Item("SellingAmt") = IIf(txtSellingAmt.Text = "", 0, txtSellingAmt.Text)
            drItem.Item("GemsTW") = _GemsTW
            drItem.Item("YOrCOrG") = txtRBP.Text
            drItem.Item("IsOriginalFixedPrice") = IsOriginalFixedPrice
            drItem.Item("OriginalFixedPrice") = _OriginalFixedPrice
            drItem.Item("IsOriginalPriceCarat") = IsOriginalPriceCarat
            drItem.Item("OriginalPriceCarat") = _OriginalPriceCarat
            drItem.Item("IsSaleReturn") = _IsSaleReturn
            drItem.Item("GemsTW") = _GemsTW
            grdDetail.DataSource = _dtItemBarcode

        End If
    End Sub

    Private Sub grdDetail_Click(sender As Object, e As EventArgs) Handles grdDetail.Click
        Dim GoldWeight As New GoldWeightInfo
        Dim objSItem As New SalesItemInfo
        Dim objCurrentPrice As New IntDiamondPriceRateInfo
        Dim GoldQualityInfo As New GoldQualityInfo

        If grdDetail.RowCount = 0 Then
            _IsUpdate = False
            Exit Sub
        End If

        With grdDetail
            _SaleLooseDiamondDetailID = .CurrentRow.Cells("SaleLooseDiamondDetailID").Value
            _SaleLooseDiamondID = .CurrentRow.Cells("SaleLooseDiamondID").Value
            _ForSaleID = .CurrentRow.Cells("ForSaleID").Value
            txtBarcodeNo.Text = .CurrentRow.Cells("ItemCode").Value
            _GemsCategoryID = .CurrentRow.Cells("GemsCategoryID").Value
            txtGemsCategory.Text = _ItemCategoryController.GetItemCategory(_GemsCategoryID).ItemCategory
            txtDescription.Text = .CurrentRow.Cells("GemsName").Value

            txtColor.Text = .CurrentRow.Cells("Color").Value
            txtOriginalCode.Text = IIf(IsDBNull(.CurrentRow.Cells("OriginalCode").Value), "-", .CurrentRow.Cells("OriginalCode").Value)
            txtShape.Text = .CurrentRow.Cells("Shape").Value
            txtClarity.Text = .CurrentRow.Cells("Clarity").Value

            objCurrentPrice = _CurrentController.GetIntDiamondData(_Carat)
            txtCurrentPrice.Text = .CurrentRow.Cells("SalesRate").Value
            ' ShowCurrentPrice(objCurrentPrice)
            _ForSaleID = .CurrentRow.Cells("ForSaleID").Value
            If _ForSaleID = "" Then
                txtCurrentPrice.Text = 0
                lblIsGram.Text = ""
            End If
            chkIsFixPrice.Checked = .CurrentRow.Cells("IsFixPrice").Value
            txtQty.Text = .CurrentRow.Cells("QTY").Value

            If (.CurrentRow.Cells("IsFixPrice").Value = True) Then
                isFixPrice = True
                txtGemsPrice.Text = "0"
                lblDonePrice.Visible = True
                txtSaleFixPrice.Visible = True
                txtSaleFixPrice.Text = .CurrentRow.Cells("FixPrice").Value
            Else
                isFixPrice = False
                lblDonePrice.Visible = False
                txtSaleFixPrice.Visible = False
                txtGemsPrice.Text = .CurrentRow.Cells("GemsPrice").Value
            End If


            'GoldWeight.Gram = CDec(grdDetail.CurrentRow.Cells("ItemTG").Value)
            '_SaleItemTG = GoldWeight.Gram
            'GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            '_SaleItemTK = GoldWeight.GoldTK

            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("ItemTK").Value)
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)

            'txtDGram.Text = Format(CDec(grdDetail.CurrentRow.Cells("ItemTG").Value), "0.000")
            txtRBP.Text = .CurrentRow.Cells("YOrCOrG").Value
            _ItemTG = grdDetail.CurrentRow.Cells("ItemTG").Value
            txtDGram.Text = Format(Math.Round(CDec(_ItemTG), 3), "0.000")
            _ItemTK = grdDetail.CurrentRow.Cells("ItemTK").Value



            txtTotalAmt.Text = .CurrentRow.Cells("TotalAmount").Value
            txtAddSub.Text = .CurrentRow.Cells("AddOrSub").Value
            txtNetAmt.Text = .CurrentRow.Cells("NetAmount").Value
            txtDesignCharges.Text = .CurrentRow.Cells("DesignCharges").Value
            txtWhiteCharges.Text = .CurrentRow.Cells("WhiteCharges").Value
            txtPlatingCharges.Text = .CurrentRow.Cells("PlatingCharges").Value
            txtMountingCharges.Text = .CurrentRow.Cells("MountingCharges").Value
            txtDesignRate.Text = .CurrentRow.Cells("DesignChargesRate").Value
            txtSellingRate.Text = .CurrentRow.Cells("SellingRate").Value
            txtSellingAmt.Text = .CurrentRow.Cells("SellingAmt").Value
        End With
        btnAdd.Text = "Update"
        _IsUpdate = True
    End Sub


    Private Sub txtAllNetAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAllNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtAllNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAllNetAmt.TextChanged
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtMemberDis.Text = "" Then txtMemberDis.Text = "0"

        If CInt(txtMemberDis.Text) > 0 Then
            txtMemberDisAmt.Text = CStr(CLng(txtAllTotalAmt.Text) * (CLng(txtMemberDis.Text) / 100))
        End If
        txtAllAddOrSub.Text = CStr((CLng(txtAllNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtValue.Text) + CLng(txtMemberDisAmt.Text)) - CLng(txtAllTotalAmt.Text))
        CalculateFinalAmount()
    End Sub

    Private Sub txtPromotionDis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPromotionDis.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPromotionDis_TextChanged(sender As Object, e As EventArgs) Handles txtPromotionDis.TextChanged
        If txtPromotionDis.Text = "" Then txtPromotionDis.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        txtPromotionAmt.Text = CLng(CLng(txtAllTotalAmt.Text) * (CLng(txtPromotionDis.Text) / 100))
        CalculateFinalAmount()
    End Sub

    Private Sub txtPromotionAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPromotionAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPromotionAmt_TextChanged(sender As Object, e As EventArgs) Handles txtPromotionAmt.TextChanged
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
        If (txtPromotionAmt.Text <> "0") Or (txtAllNetAmt.Text <> "0") Then
            txtPaidAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(txtPromotionAmt.Text) + CLng(txtDiscountAmt.Text)))
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtDiscountAmt.Text)))
        End If
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        If txtQty.Text = "" Then txtQty.Text = "0"
        If txtQty.Text <> "" Then
            CalculateGemsPrice()
        End If
    End Sub

    Private Sub SearchItem_Click(sender As Object, e As EventArgs) Handles SearchItem.Click
        Dim DataItem As DataRow
        Dim dtSale As New DataTable
        Dim objSItem As CommonInfo.SalesItemInfo

        dtSale = _SalesItemController.GetForSalesItemForSaleLooseDiamond(GetExistedItems())
        DataItem = DirectCast(SearchData.FindFast(dtSale, "Sales LooseDiamond Item List"), DataRow)

        If DataItem IsNot Nothing Then
            _ForSaleID = DataItem.Item("@ForSaleID").ToString()
            _BarcodeNo = DataItem.Item("ItemCode")
            txtBarcodeNo.Text = DataItem.Item("ItemCode")

            objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo)
            _IsSearch = True
            ShowForSaleBarcodeData(objSItem)
        End If
    End Sub

    Private Function GetExistedItems() As String
        GetExistedItems = ""
        For i As Integer = 0 To _dtItemBarcode.Rows.Count - 1
            If _dtItemBarcode.Rows(i).RowState <> DataRowState.Deleted Then
                GetExistedItems += "'" & _dtItemBarcode.Rows(i).Item("ItemCode") & "',"
            End If
        Next
        Return GetExistedItems.Trim(",")
    End Function

    Private Sub grdDetail_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdDetail.RowsRemoved
        txtBarcodeNo.Text = ""
        ClearItemCode()
        CalculategrAlldTotalAmount()
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
        openhelp("SaleInvoiceLooseDiamond")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub CalculateFinalAmount()
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If txtPurchaseAmount.Text = "" Then txtPurchaseAmount.Text = "0"
        If txtSRTaxAmt.Text = "" Then txtSRTaxAmt.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"

        If _PurchaseHeaderID <> "" Then
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text)))
            txtDifferentAmount.Text = CLng(txtAllNetAmt.Text) - CLng(txtPurchaseAmount.Text)
            txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
            txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
        Else
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text)))
            txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
            txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
        End If
    End Sub

    Private Sub btnSearchPurchase_Click(sender As Object, e As EventArgs) Handles btnSearchPurchase.Click
        Dim DataItem As DataRow
        Dim dtPurchase As New DataTable
        dtPurchase = _ObjPurchaseController.GetAllPuchaseHeaderDataBySaleInvoice(_SaleLooseDiamondID, "SaleLooseDiamond")
        DataItem = DirectCast(SearchData.FindFast(dtPurchase, "PurchaseHeader List"), DataRow)
        If DataItem IsNot Nothing Then
            _PurchaseHeaderID = DataItem.Item("VoucherNo").ToString()
            txtPurchaseVoucherNo.Text = _PurchaseHeaderID
            txtPurchaseAmount.Text = DataItem.Item("AllNetAmount")
            CalculateFinalAmount()
        End If
    End Sub

    Private Sub txtPurchaseAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPurchaseAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPurchaseAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPurchaseAmount.TextChanged
        CalculateFinalAmount()
    End Sub

    Private Sub txtPurchaseVoucherNo_TextChanged(sender As Object, e As EventArgs) Handles txtPurchaseVoucherNo.TextChanged
        Dim ObjPurchase As New CommonInfo.PurchaseHeaderInfo
        If txtPurchaseVoucherNo.Text <> "" Then
            _PurchaseHeaderID = txtPurchaseVoucherNo.Text.Trim
            ObjPurchase = _ObjPurchaseController.GetPurchaseHeaderDataBySaleInvoice(_PurchaseHeaderID, _SaleLooseDiamondID, "SaleVolume")
            If ObjPurchase.PurchaseHeaderID <> "" Then
                txtPurchaseAmount.Text = ObjPurchase.AllNetAmount
            Else
                _PurchaseHeaderID = ""
                txtPurchaseAmount.Text = "0"
                txtDifferentAmount.Text = "0"
            End If
        Else
            _PurchaseHeaderID = ""
            txtPurchaseAmount.Text = "0"
            txtDifferentAmount.Text = "0"
        End If
        CalculateFinalAmount()
    End Sub

    Private Sub txtCurrentPrice_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentPrice.TextChanged
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"
        CalculateGemsPrice()
    End Sub
    Private Sub txtDesignCharges_TextChanged(sender As Object, e As EventArgs) Handles txtDesignCharges.TextChanged
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        CalculateGemsPrice()
    End Sub
    Private Sub txtDesignRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesignRate.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtCurrentPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtDesignRate_TextChanged(sender As Object, e As EventArgs) Handles txtDesignRate.TextChanged
        Dim _DesignCharges As Integer = 0
        If txtDGram.Text = "" Then txtDGram.Text = "0.0"
        _DesignCharges = CStr(CLng(txtDesignRate.Text) * (CDec(txtDGram.Text)))
        txtDesignCharges.Text = Format(_DesignCharges, "###,##0.##")
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

    Private Sub dtpSaleDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSaleDate.ValueChanged
        SaleVolumeGenerateFormat()
    End Sub


    Private Sub txtWhiteCharges_TextChanged(sender As Object, e As EventArgs) Handles txtWhiteCharges.TextChanged
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = 0
        Call CalculateGemsPrice()
    End Sub

    Private Sub txtPlatingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtPlatingCharges.TextChanged
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = 0
        Call CalculateGemsPrice()
    End Sub

    Private Sub txtMountingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtMountingCharges.TextChanged
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = 0
        Call CalculateGemsPrice()
    End Sub
    Private Sub btnOtherCash_Click(sender As Object, e As EventArgs) Handles btnOtherCash.Click
        Dim frm As New frm_OtherCash
        frm._dtOtherCash = _dtOtherCash
        frm.ShowDialog()
        _dtOtherCash = frm._dtOtherCash
        CalculateOtherCashAmount()
    End Sub

    Private Sub chkOtherCash_CheckedChanged(sender As Object, e As EventArgs) Handles chkOtherCash.CheckedChanged
        If chkOtherCash.Checked Then
            btnOtherCash.Visible = True
        Else
            btnOtherCash.Visible = False
        End If
    End Sub
    Private Sub CalculateOtherCashAmount()
        Dim _OtherCashAmount As Integer = 0
        If _dtOtherCash.Rows.Count() > 0 Then
            For Each drRecord As DataRow In _dtOtherCash.Rows
                If drRecord.RowState <> DataRowState.Deleted Then
                    _OtherCashAmount += CLng(IIf(IsDBNull(drRecord.Item("Total")), 0, drRecord.Item("Total")))
                End If
            Next
        End If
        txtOtherCashAmount.Text = Format(Val(_OtherCashAmount), "###,##0.##")
        CalculateFinalAmount()
    End Sub
    Public Sub CalculateSRTaxAmt()

        If txtPurchaseVoucherNo.Text = "" Then
            If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"
            If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
            If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
            '  If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
            If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
            If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
            If txtValue.Text = "" Then txtValue.Text = "0"
            If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"

            If Global_IsCash = False Then
                txtPaidAmt.Text = "0"
            End If

            txtSRTaxAmt.Text = Format(CInt(CInt(txtAllNetAmt.Text) * CDec(txtSRTaxPer.Text) / 100), "###,##0.##")
            'If chkAdvance.Checked Then
            '    If Global_IsCash = True Then
            '        txtPaidAmt.Text = Format(CInt(txtAllTotalAmt.Text) + CInt(txtSRTaxAmt.Text), "###,##0.##")
            '    End If
            '    txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text))), "###,##0.##")

            '    txtBalanceAmt.Text = Format(CInt(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CInt(txtPaidAmt.Text), "###,##0.##")

            'Else
            If Global_IsCash = True Then
                txtPaidAmt.Text = Format(CInt(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text), "###,##0.##")
            End If
            txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
            txtBalanceAmt.Text = Format(CInt(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CInt(txtPaidAmt.Text), "###,##0.##")
            'End If

        Else
            If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"
            If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
            If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
            ' If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
            If Global_IsCash = False Then
                txtPaidAmt.Text = "0"
            End If

            txtSRTaxAmt.Text = Format(CInt(CInt(txtDifferentAmount.Text) * CDec(txtSRTaxPer.Text) / 100), "###,##0.##")
            'If chkAdvance.Checked Then
            '    If Global_IsCash = True Then
            '        txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
            '    End If
            '    txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
            'Else
            If Global_IsCash = True Then
                txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
            End If
            txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            'End If

        End If


    End Sub
    Private Sub txtSRTaxPer_TextChanged(sender As Object, e As EventArgs) Handles txtSRTaxPer.TextChanged
        If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"
        CalculateFinalAmount()
    End Sub
    Private Sub txtSellingRate_TextChanged(sender As Object, e As EventArgs) Handles txtSellingRate.TextChanged
        If txtSellingRate.Text = "" Then txtSellingRate.Text = "0"
        CalculateSellingAmt()
    End Sub
    Public Sub CalculateSellingAmt()
        Dim _TotalGoldPrice As Integer = 0
        If txtSellingRate.Text = "" Then txtSellingRate.Text = "0.0"
        If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"


        If isFixPrice = False Then

            _TotalGoldPrice = CInt(txtGemsPrice.Text) + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
            txtSellingAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtSellingRate.Text)) / 100), "###,##0.##")
            txtGemsPrice.Text = Format(_TotalGoldPrice, "###,##0.##")
            CalculateGemsPrice()
        Else
            txtSellingAmt.Text = Format(CInt((_FixPrice * CDec(txtSellingRate.Text)) / 100), "###,##0.##")
            txtTotalAmt.Text = Format(_FixPrice + CInt(txtSellingAmt.Text), "###,##0.##")
            txtNetAmt.Text = Format(CLng(txtTotalAmt.Text), "###,##0.##")
            txtAddSub.Text = "0"
        End If
    End Sub
    Private Sub txtSRTaxAmt_TextChanged(sender As Object, e As EventArgs) Handles txtSRTaxAmt.TextChanged
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtPurchaseAmount.Text = "" Then txtPurchaseAmount.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
        If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"

            If _PurchaseHeaderID <> "" Then

                'If chkAdvance.Checked Then
                '    If Global_IsCash = True Then
                '        txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                '    End If
                '    txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                'Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
                'End If
            Else
                'If chkAdvance.Checked Then
                '    If Global_IsCash = True Then
                '        txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                '    End If
                '    txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                'Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
                'End If
            End If
    End Sub
    Private Async Sub GetMemberID(ByVal MemberCode As String)
        'Dim dtMember As DataTable
        Dim dtMember As New DataTable()
        MCode = txtMemberCode.Text


        Temp = Global_CompanyName + txtMemberCode.Text + Global_MemberConstant
        Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(Temp))

        Dim s2 As String = Encoding.UTF8.GetString(Convert.FromBase64String(Token))
        Dim dtColumn As String = ""
        Dim ExpireDate As DateTime

        'dtMember = Await WebService.MemberCard.GetMemberByMemberCode(MCode, Global_CompanyName, Token)
        'dtMember.Rows(0).Item("OpportunityValue") = 1
        If (dtMember.Rows(0).Item("MemberID") <> "") Then
            'IsUseRedeem = dtMember.Rows(0).Item("IsUseRedeem")
            MaxPoint = dtMember.Rows(0).Item("MaximumPoint")
            PointConfiguration = dtMember.Rows(0).Item("PointConfiguration")
            AmountConfiguration = dtMember.Rows(0).Item("AmountConfiguration")
            OpportunityPoint = dtMember.Rows(0).Item("OpportunityPoint")
            OpportunityValue = dtMember.Rows(0).Item("OpportunityValue")

            PointBalance = dtMember.Rows(0).Item("PointBalance")
            OppurtunityType = dtMember.Rows(0).Item("OpportunityType")
            DiscountType = dtMember.Rows(0).Item("DiscountType")
            MemDiscountAmount = dtMember.Rows(0).Item("Discount")
            DiscountPercent = dtMember.Rows(0).Item("Discount")
            ExpireDate = dtMember.Rows(0).Item("ExpireDate")
            dtpExpireDate.Value = ExpireDate
            MemberRedeemPoint = dtMember.Rows(0).Item("RedeemPoint")
            MemberRedeemValue = dtMember.Rows(0).Item("RedeemAmount")
            Try
                btnRedeem.Visible = True
                btnRedeemClear.Visible = True

                grpMember.Visible = True
                grpMember.Text = "Member Redeem Info"
                txtPoint.Enabled = False
                txtValue.Enabled = False
                btnRedeem.Visible = True
                btnRedeemClear.Visible = True
                lblPointBalance.Text = PointBalance

                If OppurtunityType = "Item" Then

                    lblPoint.Visible = False
                    lblPointBalance.Visible = False
                    lblRedeemItem.Visible = True
                    btnRedeem.Visible = False
                    btnRedeemClear.Visible = False
                    InvoiceStatus = 1
                ElseIf OppurtunityType = "Amount" Then
                    lblPoint.Visible = True
                    lblPointBalance.Visible = True
                    PointBalance = dtMember.Rows(0).Item("PointBalance")
                    lblRedeemItem.Visible = False


                Else
                    lblRedeemItem.Visible = False

                End If


                'If (IsUseRedeem = True) Then
                '    'lblRedeemPoint.Visible = True
                '    'txtRedeemPoint.Visible = True
                '    'txtRedeemValue.Visible = True
                '    'btnRedeem.Visible = True
                '    'btnRedeemClear.Visible = True
                '    grpMember.Visible = True
                '    grpMember.Text = "Member Redeem Info"
                '    txtPoint.Enabled = False
                '    txtValue.Enabled = False
                '    If OppurtunityType = "Item" Then
                '        lblRedeemItem.Visible = True
                '        btnRedeem.Visible = False
                '        btnRedeemClear.Visible = False
                '    Else
                '        lblRedeemItem.Visible = False

                '        btnRedeem.Visible = True
                '        btnRedeemClear.Visible = True
                '    End If

                '    lblPoint.Visible = False
                '    lblPointBalance.Visible = False




                '    'For Discount Type =>Amount,Percent
                '    If DiscountType = "Amount" Then
                '        txtDiscountAmt.Text = Format(Val(CInt(DiscountAmount)), "###,##0.##")
                '    Else
                '        txtMemberDis.Text = CDec(DiscountPercent)

                '    End If

                'Else
                '    grpMember.Visible = True
                '    grpMember.Text = "Member Point Info"
                '    txtPoint.Enabled = True
                '    txtValue.Enabled = False
                '    btnRedeem.Visible = False
                '    btnRedeemClear.Visible = False
                '    lblRedeemItem.Visible = False

                '    PointBalance = dtMember.Rows(0).Item("PointBalance")
                '    txtPoint.Text = 0
                '    lblPoint.Visible = True
                '    lblPointBalance.Visible = True
                '    lblPointBalance.Text = PointBalance
                If DiscountType = "Amount" Then
                    txtDiscountAmt.Text = Format(Val(CInt(MemDiscountAmount)), "###,##0.##")
                Else
                    'If OppurtunityType = "Discount" Then
                    '    txtMemberDis.Text = CDec(DiscountPercent) + CDec(OpportunityValue)

                    'Else
                    '    txtMemberDis.Text = CDec(DiscountPercent)
                    'End If
                    txtMemberDis.Text = CDec(DiscountPercent)

                End If

                ' End If
                txtMemberName.Text = dtMember.Rows(0).Item("MemberName")
                txtMemberID.Text = dtMember.Rows(0).Item("MemberID")
            Catch ex As Exception
                MsgBox("There is no record for this Member No.", MsgBoxStyle.Information, "Member Management System")


            End Try
        Else
            If (dtMember.Rows(0).Item("ErrorMessage") = "Fail") Then
                MsgBox("There is no token for this Member No.", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtMember.Rows(0).Item("ErrorMessage") = "Unauthorized token") Then
                MsgBox("There member isn't exit in this Company.", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtMember.Rows(0).Item("ErrorMessage") = "Not Found") Then
                MsgBox("There member isn't exit in this Company.", MsgBoxStyle.Information, "Member Management System")
                'ElseIf (dtMember.Rows(0).Item("OpportunityValue") = "") Then
                '    MsgBox("This Member has no oppurtunity.", MsgBoxStyle.Information, "Member Management System")

            End If
        End If


    End Sub
    Private Sub btnRedeem_Click(sender As Object, e As EventArgs) Handles btnRedeem.Click
        If CheckForInternetConnection() = True Then
            ''For oppurnity Type =Item,Amount,percent

            GetRedeemInfo(txtMemberCode.Text)

        Else
            MsgBox("Please Check Internet Connection!")
        End If



    End Sub
    Private Async Sub GetRedeemInfo(ByVal MemberCode As String)
        Dim dtRedeemInfo As DataTable
        Dim MCode As String = txtMemberCode.Text

        'Dim temp As String = Global_RegName + txtMemberCode.Text + Global_MemberConstant
        'Dim Token As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(temp))
        'Dim s2 As String = Encoding.Unicode.GetString(Convert.FromBase64String(Token))

        'Dim s1 As String = "RD01-21052020-00001|RD01-21052020-00002|RD01-22052020-00001|RD01-22052020-00004"
        'Dim s3 As String = s1.Replace("|", ",")

        ' dtRedeemInfo = Await WebService.MemberCard.GetRedeemInfoByMemberCode(MCode, Global_CompanyName, Token)

        If dtRedeemInfo.Rows(0).Item("RedeemID") <> "" Then
            Try
                TempRedeemID = dtRedeemInfo.Rows(0).Item("RedeemID")
                RedeemID = TempRedeemID.Replace("|", ",")
                IsRedeemInvoice = True
                RedeemPoint = dtRedeemInfo.Rows(0).Item("RedeemPoint")
                RedeemValue = dtRedeemInfo.Rows(0).Item("RedeemValue")
                txtPoint.Text = Format(RedeemPoint, "###,##0.##")
                txtPoint.Enabled = False
                If OppurtunityType = "Amount" Then
                    InvoiceStatus = 0
                    txtValue.Text = Format(RedeemValue, "###,##0.##")
                ElseIf OppurtunityType = "Discount" Then
                    InvoiceStatus = 1
                    txtMemberDis.Text = CDec(DiscountPercent) + CDec(RedeemValue)
                    'txtValue.Text = Format(RedeemValue, "###,##0.##")
                Else
                    InvoiceStatus = 2

                End If


            Catch ex As Exception
                MsgBox("There is no record for this Member No.", MsgBoxStyle.Information, "Gold Smith Member System")


            End Try
        ElseIf dtRedeemInfo.Rows(0).Item("RedeemID") = "" Then
            'InvoiceStatus = 2
            'txtPoint.Enabled = True
            txtPoint.Enabled = True
            IsRedeemInvoice = False
            RedeemID = ""
        Else
            If (dtRedeemInfo.Rows(0).Item("ErrorMessage") = "Fail") Then
                MsgBox("There is no Redeem for this Member No.", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtRedeemInfo.Rows(0).Item("ErrorMessage") = "Unauthorized token") Then
                MsgBox("There member isn't exit in this Company.", MsgBoxStyle.Information, "Member Management System")


            End If

        End If


    End Sub
    Private Sub txtMemberCode_TextChanged(sender As Object, e As EventArgs) Handles txtMemberCode.TextChanged
        Dim QREncodeString As String = ""
        If Global_IsUseMember = True And _IsUpdateHeader = False And Global_IsMemberCustomer = True Then
            If Global_IsMemberCustomer = True Then
                If CheckForInternetConnection() = True Then
                    If txtMemberCode.Text <> "" Then
                        GetMemberID(txtMemberCode.Text)
                    End If

                Else
                    MsgBox("Please Check Internet Connection!")
                End If


            Else
                If txtMemberCode.Text <> "" Then

                    GetMemberID(txtMemberCode.Text)
                End If
            End If


        End If


    End Sub
    Private Sub txtMemberCode_LostFocus(sender As Object, e As EventArgs) Handles txtMemberCode.LostFocus
        Dim QREncodeString As String = ""
        If Global_IsUseMember = True And _IsUpdateHeader = False Then
            If Global_IsMemberCustomer = True Then
                If CheckForInternetConnection() = True Then
                    If txtMemberCode.Text <> "" Then
                        GetMemberID(txtMemberCode.Text)
                    End If

                Else
                    MsgBox("Please Check Internet Connection!")
                End If


            Else
                If txtMemberCode.Text <> "" Then

                    GetMemberID(txtMemberCode.Text)
                End If
            End If


        End If
    End Sub
    Private Sub btnRedeemClear_Click(sender As Object, e As EventArgs) Handles btnRedeemClear.Click
        RedeemID = ""
        RedeemPoint = 0
        RedeemValue = 0
        txtPoint.Text = 0
        txtValue.Text = 0
        InvoiceStatus = 0

    End Sub

    Private Sub txtPoint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPoint.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub


    Private Sub txtPoint_TextChanged(sender As Object, e As EventArgs) Handles txtPoint.TextChanged
        If txtPoint.Text = "" Then txtPoint.Text = "0"
        If IsRedeemInvoice = False Then
            If PointBalance > 0 Then
                RedeemPoint = CInt(txtPoint.Text)
                If IsLimitPoint() = True Then

                    If OpportunityPoint > 0 And OppurtunityType <> "Discount" Then
                        RedeemValue = CInt(CInt(OpportunityValue) / CInt(OpportunityPoint)) * CInt(txtPoint.Text)
                        txtValue.Text = Val(RedeemValue)
                    Else
                        If MemberRedeemPoint > 0 And OppurtunityType <> "Discount" Then
                            RedeemValue = CInt(CInt(MemberRedeemValue) / CInt(MemberRedeemPoint)) * CInt(txtPoint.Text)
                            txtValue.Text = Val(RedeemValue)
                        Else
                            If OppurtunityType = "Discount" Then
                                txtValue.Text = 0


                            Else
                                MsgBox("There is no redeem value.", MsgBoxStyle.Information, "Gold Smith Management System")
                            End If

                        End If


                    End If

                End If
            End If
        End If

    End Sub
    Private Sub txtMemberDis_TextChanged(sender As Object, e As EventArgs) Handles txtMemberDis.TextChanged
        If txtMemberDis.Text = "" Then txtMemberDis.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtAllTotalAmt.Text = "0" Then
            txtMemberDisAmt.Text = 0
        Else
            txtMemberDisAmt.Text = CStr(CLng(txtAllTotalAmt.Text) * (CLng(txtMemberDis.Text) / 100))
        End If
    End Sub

    Private Sub txtMemberDisAmt_TextChanged(sender As Object, e As EventArgs) Handles txtMemberDisAmt.TextChanged
        If txtValue.Text = "" Then txtValue.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtAllTotalAmt.Text = "0" Then
            txtAllNetAmt.Text = 0
        Else
            'txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text)) - (CLng(txtPromotionAmt.Text) + CLng(txtValue.Text) + CLng(txtDiscountAmt.Text) + CLng(txtMemberDisAmt.Text))), "###,##0.##")
            'txtAddOrSub.Text = CStr((CLng(txtNetAmt.Text)) - ((CLng(txtTotalAmt.Text) + CLng(txtExpenseAmt.Text)) - (CLng(txtCommissionAmt.Text) + CLng(txtPromotionAmt.Text))))

            txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0")
        End If

    End Sub
    Private Function IsMaximumPoint() As Boolean

        If _TransactionID <> "" Then ' Update for Save Point Voucher
            If ((Val((TopupPoint - _OldTopupPoint)) + Val(PointBalance) - Val(RedeemPoint - _OldRedeemTopupPoint)) > Val(MaxPoint)) Then

                MsgBox("You Reach Maximum Point.Please Redeem it!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                _isMaximum = False
                Return False
                Exit Function
            End If
        ElseIf _IsUpdateHeader = True Then
            If ((Val((TopupPoint)) + Val(PointBalance) - Val(RedeemPoint - _OldRedeemTopupPoint)) > Val(MaxPoint)) Then

                MsgBox("You Reach Maximum Point.Please Redeem it!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                _isMaximum = False
                Return False
                Exit Function
            End If
        ElseIf IsRedeemInvoice = False Then
            If ((Val((TopupPoint)) + Val(PointBalance) - Val(RedeemPoint)) > Val(MaxPoint)) Then

                MsgBox("You Reach Maximum Point.Please Redeem it!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                _isMaximum = False
                Return False
                Exit Function
            End If
        Else
            If ((Val((TopupPoint)) + Val(PointBalance) - Val(RedeemPoint)) > Val(MaxPoint)) Then

                MsgBox("You Reach Maximum Point.Please Redeem it!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                _isMaximum = False
                Return False
                Exit Function
            End If
        End If
        _isMaximum = True
        Return True

    End Function
    Private Function IsLimitPoint() As Boolean
        If MCode <> "" Then
            If (Val(RedeemPoint) > Val(PointBalance)) Then

                MsgBox("Please check point balance!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                Return False

            End If
        End If


        Return True
    End Function
    Public Shared Function CheckForInternetConnection() As Boolean
        Dim ServiceURL As String = AppConfiguration.ReadAppSettings("URL")
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead(serviceURL)
                    Return True
                End Using
            End Using

        Catch

            Return False

        End Try
    End Function

    Public Async Sub GetRedeemID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)



        'Dim dtRedeemID As New DataTable()
        MCode = txtMemberCode.Text


        Dim temp As String = Global_CompanyName + txtMemberCode.Text + Global_MemberConstant
        Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(temp))
        Dim s2 As String = Encoding.Unicode.GetString(Convert.FromBase64String(Token))
        Dim dtColumn As String = ""


        ' dtRedeemID = Await WebService.MemberCard.GetRedeemIDByMemberCode(MCode, Global_CompanyName, Token, InvoiceID)

        If (dtRedeemID.Rows(0).Item("RedeemID") <> "") Then

            Try

                TempRedeemID = dtRedeemID.Rows(0).Item("RedeemID")
                RedeemID = TempRedeemID.Replace("|", ",")

                _RedeemID = dtRedeemID.Rows(0).Item("RedeemID")
                RedeemBool = objSaleLooseDiamondController.UpdateRedeemID(RedeemID, InvoiceID)
                RedeemID = ""
                _RedeemID = ""

            Catch ex As Exception
                MsgBox("There is no record for this Member No.", MsgBoxStyle.Information, "Member Management System")
            End Try
        Else
            If (dtRedeemID.Rows(0).Item("ErrorMessage") = "Fail") Then
                MsgBox("There is no Redeem ID for this Member No.", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtRedeemID.Rows(0).Item("ErrorMessage") = "Unauthorized token") Then
                MsgBox("There member isn't exit in this Company.", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtRedeemID.Rows(0).Item("ErrorMessage") = "Not Found") Then
                MsgBox("There member isn't exit in this Company.", MsgBoxStyle.Information, "Member Management System")


            End If
        End If

    End Sub



    Private Async Sub SaveSaleMemberCard(ByVal objSaleInvoice As CommonInfo.SaleLooseDiamondHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean
        'Dim Result As String

        'Dim Result As String
        '  RegName + MemberCode + GwtMember@2020

        'Dim s As String = "GWT1234501320000002GwtMember@2020"
        'Dim s1 As String = Convert.ToBase64String(Encoding.Unicode.GetBytes(s))
        'Dim s2 As String = Encoding.Unicode.GetString(Convert.FromBase64String(s1))

        'Result = Await WebService.MemberCard.SaveSaleMemberCardForDiamond(objSaleInvoice, Status, Global_CompanyName)
        If Result = False Then
            MsgBox("Add Point in server fail.", AppName)
            'If (MessageBox.Show("Point balance can't be update.Do you want to save voucher.", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)) = DialogResult.Yes Then
            '    Result = True


            'Else
            '    Result = False

            'End If
        ElseIf Result = True And Status = 0 Then 'For Voucher Point Balance
            VoucherPointBalance = PointBalance + objSaleInvoice.TopupPoint - objSaleInvoice.RedeemPoint
        ElseIf Result = True And Status = 1 Then
            VoucherPointBalance = PointBalance + objSaleInvoice.TopupPoint - _OldTopupPoint + (_OldRedeemTopupPoint - objSaleInvoice.RedeemPoint)
        End If
        'End If
    End Sub

    Private Async Sub UpdateRedeem(ByVal objSaleInvoice As CommonInfo.SaleLooseDiamondHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean

        ' Result = Await WebService.MemberCard.UpdateRedeemForDiamond(objSaleInvoice, Status)
        If Result = False Then

            MsgBox("Update Redeem in server fail.", AppName)
            'Exit Sub
            'If (MessageBox.Show("Point balance can't be update.Do you want to save voucher.", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)) = DialogResult.Yes Then
            '    Result = True


            'Else
            '    Result = False

            'End If

        End If

    End Sub
    Private Async Sub AddRedeem(ByVal objSaleInvoice As CommonInfo.SaleLooseDiamondHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean


        ' Result = Await WebService.MemberCard.AddRedeemForDiamond(objSaleInvoice, Status)
        If Result = False Then
            'Exit Sub
            MessageBox.Show("Add Redeem in server fail.", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)


            'If (MessageBox.Show("Point balance can't be update.Do you want to save voucher.", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)) = DialogResult.Yes Then
            '    Result = True


            'Else
            '    Result = False

            'End If
            'Else
            '    RedeemSuccess = True
            ' MsgBox("Update Redeem in server fail.")

        End If
    End Sub
    Private Async Sub GetPointBalanceByMemberID(ByVal MemberCode As String)
        Dim dtMember As DataTable
        MCode = txtMemberCode.Text
        'Dim CompanyReferenceNo As String = "GWT"
        ' Dim Token As String = "R1dUMTIzNDUwMTMyMDAwMDAwMkd3dE1lbWJlckAyMDIw"

        Dim temp As String = Global_CompanyName + txtMemberCode.Text + Global_MemberConstant
        Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(temp))
        Dim s2 As String = Encoding.Unicode.GetString(Convert.FromBase64String(Token))

        ' dtMember = Await WebService.MemberCard.GetMemberByMemberCode(MCode, Global_CompanyName, Token)

        If dtMember.Rows.Count > 0 Then
            'IsUseRedeem = dtMember.Rows(0).Item("IsUseRedeem")
            PointBalance = dtMember.Rows(0).Item("PointBalance")
            VoucherPointBalance = PointBalance
            MaxPoint = dtMember.Rows(0).Item("MaximumPoint")
            PointConfiguration = dtMember.Rows(0).Item("PointConfiguration")
            AmountConfiguration = dtMember.Rows(0).Item("AmountConfiguration")

            Try
                If IsRedeemInvoice = True Then
                    grpMember.Visible = True
                    grpMember.Text = "Member Redeem Info"
                    txtPoint.Enabled = False
                    txtValue.Enabled = False
                    lblPoint.Visible = False
                    lblPointBalance.Visible = False
                    If _IsUpdateHeader = False Then

                        btnRedeem.Visible = True
                        btnRedeemClear.Visible = True
                    Else
                        btnRedeem.Visible = False
                        btnRedeemClear.Visible = False
                    End If

                Else

                    grpMember.Visible = True
                    grpMember.Text = "Member Point Info"
                    txtPoint.Enabled = True
                    txtValue.Enabled = False
                    btnRedeem.Visible = False
                    btnRedeemClear.Visible = False
                    PointBalance = dtMember.Rows(0).Item("PointBalance")

                    lblPoint.Visible = True
                    lblPointBalance.Visible = True
                    lblPointBalance.Text = PointBalance


                End If
                txtMemberName.Text = dtMember.Rows(0).Item("MemberName")
                txtMemberID.Text = dtMember.Rows(0).Item("MemberID")
            Catch ex As Exception
                MsgBox("There is no record for this Member No.", MsgBoxStyle.Information, "Gold Smith Management")

            End Try


        End If


    End Sub
    Private Function ContainColumn(ByVal columnName As String, ByVal table As DataTable) As Boolean
        Dim columns As DataColumnCollection = table.Columns
        If columns.Contains(columnName) Then

            MsgBox("There is no record for this Member No.", MsgBoxStyle.Information, "Member Management System")
            Return False
        End If
        Return True
    End Function
    Private Sub txtValue_TextChanged(sender As Object, e As EventArgs) Handles txtValue.TextChanged
        If txtValue.Text = "" Then txtValue.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtAllTotalAmt.Text = "0" Then
            txtAllNetAmt.Text = 0
        Else
            txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
            txtAllAddOrSub.Text = Format(Val((CLng(txtAllNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + +CLng(txtValue.Text)) - CLng(txtAllTotalAmt.Text)), "###,##0.##")
        End If
    End Sub
    Public Sub UpdateRedeemAndTransID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)
        If _isMaximum = True Then
            GetTransactionID(MCode, Global_CompanyName, Token, _SaleLooseDiamondID)
        End If
        GetRedeemID(MCode, Global_CompanyName, Token, _SaleLooseDiamondID)

    End Sub
    Public Async Sub GetTransactionID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)

        ' dtTransactionID = Await WebService.MemberCard.GetTransactionIDByMemberCode(MCode, Global_CompanyName, Token, InvoiceID)

        If (dtTransactionID.Rows(0).Item("TransactionID") <> "") Then

            Try
                _TransactionID = dtTransactionID.Rows(0).Item("TransactionID")

                TransBool = objSaleLooseDiamondController.UpdateTransactionID(_TransactionID, InvoiceID)
            Catch ex As Exception
                MsgBox("There is no record for this Member No.", MsgBoxStyle.Information, "Member Management System")
            End Try
        Else
            If (dtTransactionID.Rows(0).Item("ErrorMessage") = "Fail") Then
                MsgBox("There is no Transactionid for this Member No.", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtTransactionID.Rows(0).Item("ErrorMessage") = "Unauthorized token") Then
                MsgBox("Unauthorized Token.", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtTransactionID.Rows(0).Item("ErrorMessage") = "Not Found") Then
                MsgBox("Not Found", MsgBoxStyle.Information, "Member Management System")
            ElseIf (dtTransactionID.Rows(0).Item("OpportunityValue") = "") Then
                MsgBox("This Member has no oppurtunity.", MsgBoxStyle.Information, "Member Management System")

            End If
        End If
    End Sub

End Class

