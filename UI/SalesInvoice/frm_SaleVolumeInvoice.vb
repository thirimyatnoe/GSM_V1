Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
'Imports System.String
'Imports System.Windows
Imports System.Text
Imports System.Net
Imports Operational.AppConfiguration
Public Class frm_SaleVolumeInvoice
    Implements IFormProcess

    Private _SalesVolumeID As String = ""
    Private _SalesVolumeDetailID As String = ""
    Private _StaffID As String = ""
    Private _ForSaleID As String = ""
    Private _BarcodeNo As String = ""
    Private _ItemCode As String = ""
    Private _CustomerID As String = ""
    Private _GoldQualityID As String = ""
    Private _ItemCategoryID As String = ""
    Private _ItemNameID As String = ""
    Private _IsGram As Boolean = False
    Private _dtSalesInvoiceItem As DataTable
    Private _LocationID As String = ""
    Private PName As String = ""
    Private _FixPrice As Integer = 0
    Private _GoldPrice As Integer = 0
    Private _IsUpdate As Boolean = False
    Private _PurchaseHeaderID As String = ""
    Private _IsAllowDiscount As Boolean = False

    Dim _GoldTK As Decimal = 0
    Dim _GoldTG As Decimal = 0

    Dim _WasteTK As Decimal = 0.0
    Dim _WasteTG As Decimal = 0.0

    Dim _ItemTK As Decimal = 0.0
    Dim _ItemTG As Decimal = 0.0

    Dim _TotalTK As Decimal = 0.0
    Dim _TotalTG As Decimal = 0.0

    Dim _SaleItemTK As Decimal = 0.0
    Dim _SaleItemTG As Decimal = 0.0

    Dim _GoldTKForSale As Decimal = 0
    Dim _GoldTGForSale As Decimal = 0

    Dim _GemsTKForSale As Decimal = 0.0
    Dim _GemsTGForSale As Decimal = 0.0

    Dim _WasteTKForSale As Decimal = 0.0
    Dim _WasteTGForSale As Decimal = 0.0

    Dim _TotalTKForSale As Decimal = 0.0
    Dim _TotalTGForSale As Decimal = 0.0


    Dim _ItemTKForSale As Decimal = 0.0
    Dim _ItemTGForSale As Decimal = 0.0

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
    ' Dim _OldTopupValue As Integer=0
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
    Private _ItemNameCon As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private objGemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private objSalesVolumeController As SalesVolume.ISalesVolumeController = Factory.Instance.CreateSalesVolumeController
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _SalesItemController As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _CurrentController As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _ObjPurchaseController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController

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
    Private Sub frm_SaleVolumeInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MyBase._Heading.Text = "ရောင်းခြင်း(Volume)"
        'MyBase.lblLogInUserName.Text = Global_CurrentUser
        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        numberformat = Global_DecimalFormat
        _LocationID = Global_CurrentLocationID
        GetcboStaff()
        Clear()
        Me.KeyPreview = True
        ' MyBase.addGridDataErrorHandlers(grdSaleCategory)
    End Sub
    Private Sub frm_SaleVolumeInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        dt = objGeneralController.CheckExitVoucherForCashReceipt("tbl_CashReceipt", "AND VoucherNo='" + _SalesVolumeID + "' AND Type='SalesInvoiceVolume'")
        If dt.Rows.Count() > 0 Then
            MsgBox("This VoucherNo is Use in CashReceipt!", MsgBoxStyle.Information, "")
            Return False
            Exit Function
        End If

        'If objSalesVolumeController.DeleteSalesVolume(GetDataSalesVolume()) Then
        '    Clear()
        '    btnDelete.Enabled = False
        '    btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
        '    Return True
        'Else
        '    Return False
        'End If

        Dim objSalesVolume As CommonInfo.SalesVolumeHeaderInfo
        objSalesVolume = GetDataSalesVolume()
        If objSalesVolumeController.DeleteSalesVolume(GetDataSalesVolume()) Then

            If Global_IsUseMember = True Then

                If OppurtunityType <> "Item" Then

                    ''To Filter With RedeemID

                    If IsRedeemInvoice = True Then
                        UpdateRedeem(objSalesVolume, 2)
                    Else
                        AddRedeem(objSalesVolume, 2)
                    End If
                End If

                SaveSaleMemberCard(objSalesVolume, 2)
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
            Dim objSalesVolume As New SalesVolumeHeaderInfo

            objSalesVolume = GetDataSalesVolume()
            If Global_IsUseMember = False Or txtMemberCode.Text = "" Then 'Not use member
                If objSalesVolumeController.SaveSalesVolume(objSalesVolume, _dtItemBarcode) Then
                    _SalesVolumeID = objSalesVolume.SalesVolumeID
                    If (MsgBox("Do You Want To Save And Print SaleVolume Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                        Dim frmPrint As New frm_ReportViewer
                        Dim dt As New DataTable
                        Dim obj As New CommonInfo.SalesVolumeHeaderInfo
                        Dim _PurchaseHeaderID As String
                        Dim dtPurchase As New DataTable

                        obj = objSalesVolumeController.GetSaleVolumeByID(_SalesVolumeID)
                        _PurchaseHeaderID = obj.PurchaseHeaderID

                        If _PurchaseHeaderID = "" Then
                            dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

                            If dt.Rows.Count() = 0 Then
                                MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                                Exit Function
                            Else
                                For Each dr As DataRow In dt.Rows
                                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                    _GoldQualityID = dr.Item("GoldQualityID")
                                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                Next
                            End If
                        Else
                            dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

                            If dt.Rows.Count() = 0 Then
                                MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                                Exit Function
                            Else
                                For Each dr As DataRow In dt.Rows
                                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                    _GoldQualityID = dr.Item("GoldQualityID")
                                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                Next
                            End If
                            dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleVolume(_SalesVolumeID)
                        End If

                        frmPrint.PrintSaleVolume(dt, dtPurchase)
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
                    If objSalesVolumeController.SaveSalesVolume(objSalesVolume, _dtItemBarcode) Then
                        _SalesVolumeID = objSalesVolume.SalesVolumeID

                        'Can be use For Oppurnity Type Amount,Discount only
                        If OppurtunityType <> "Item" Then
                            If RedeemID <> "" And btnSave.Text <> "Update" Then  'RedeemIsuse 1
                                If CheckForInternetConnection() = True Then
                                    UpdateRedeem(objSalesVolume, 0)

                                End If
                            Else

                                If btnSave.Text <> "Update" Then
                                    If CheckForInternetConnection() = True Then
                                        AddRedeem(objSalesVolume, 0)
                                    End If

                                Else
                                    If CheckForInternetConnection() = True Then
                                        AddRedeem(objSalesVolume, 1)

                                    End If

                                End If
                            End If
                        End If

                        If IsMaximumPoint() = True Then
                            If btnSave.Text <> "Update" Then
                                SaveSaleMemberCard(objSalesVolume, 0)
                            Else
                                SaveSaleMemberCard(objSalesVolume, 1)
                            End If
                        End If

                        If (MsgBox("Do You Want To Save And Print SaleVolume Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                            Dim frmPrint As New frm_ReportViewer
                            Dim dt As New DataTable
                            Dim obj As New CommonInfo.SalesVolumeHeaderInfo
                            Dim _PurchaseHeaderID As String
                            Dim dtPurchase As New DataTable

                            obj = objSalesVolumeController.GetSaleVolumeByID(_SalesVolumeID)
                            _PurchaseHeaderID = obj.PurchaseHeaderID

                            If _PurchaseHeaderID = "" Then
                                dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        _GoldQualityID = dr.Item("GoldQualityID")
                                        '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                    Next
                                End If
                            Else
                                dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        _GoldQualityID = dr.Item("GoldQualityID")
                                        '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                    Next
                                End If
                                dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleVolume(_SalesVolumeID)
                            End If

                            UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _SalesVolumeID)
                            dt.Rows(0).Item("PointBalance") = VoucherPointBalance
                            frmPrint.PrintSaleVolume(dt, dtPurchase)
                            frmPrint.WindowState = FormWindowState.Maximized
                            frmPrint.Show()
                            Clear()
                        Else
                            UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _SalesVolumeID)
                            Clear()
                            Return True
                        End If
                    End If
                Else 'Member သုံးပြီးလိုင်းမရဘူး Yes= လိုင်းမရပေမယ့်သိမ်းမယ်
                    If objSalesVolumeController.SaveSalesVolume(objSalesVolume, _dtItemBarcode) Then
                        _SalesVolumeID = objSalesVolume.SalesVolumeID
                        If (MsgBox("Do You Want To Save And Print SaleVolume Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                            Dim frmPrint As New frm_ReportViewer
                            Dim dt As New DataTable
                            Dim obj As New CommonInfo.SalesVolumeHeaderInfo
                            Dim _PurchaseHeaderID As String
                            Dim dtPurchase As New DataTable

                            obj = objSalesVolumeController.GetSaleVolumeByID(_SalesVolumeID)
                            _PurchaseHeaderID = obj.PurchaseHeaderID

                            If _PurchaseHeaderID = "" Then
                                dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        _GoldQualityID = dr.Item("GoldQualityID")
                                        '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                    Next
                                End If
                            Else
                                dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        _GoldQualityID = dr.Item("GoldQualityID")
                                        '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                                    Next
                                End If
                                dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleVolume(_SalesVolumeID)
                            End If
                            frmPrint.PrintSaleVolume(dt, dtPurchase)
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

        'If objSalesVolumeController.SaveSalesVolume(objSalesVolume, _dtItemBarcode) Then
        '    _SalesVolumeID = objSalesVolume.SalesVolumeID
        '    If (MsgBox("Do You Want To Save And Print SaleVolume Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
        '        Dim frmPrint As New frm_ReportViewer
        '        Dim dt As New DataTable
        '        Dim obj As New CommonInfo.SalesVolumeHeaderInfo
        '        Dim _PurchaseHeaderID As String
        '        Dim dtPurchase As New DataTable

        '        obj = objSalesVolumeController.GetSaleVolumeByID(_SalesVolumeID)
        '        _PurchaseHeaderID = obj.PurchaseHeaderID

        '        If _PurchaseHeaderID = "" Then
        '            dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

        '            If dt.Rows.Count() = 0 Then
        '                MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
        '                Exit Function
        '            Else
        '                For Each dr As DataRow In dt.Rows
        '                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
        '                    _GoldQualityID = dr.Item("GoldQualityID")
        '                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
        '                Next
        '            End If
        '        Else
        '            dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

        '            If dt.Rows.Count() = 0 Then
        '                MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
        '                Exit Function
        '            Else
        '                For Each dr As DataRow In dt.Rows
        '                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
        '                    _GoldQualityID = dr.Item("GoldQualityID")
        '                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
        '                Next
        '            End If
        '            dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleVolume(_SalesVolumeID)
        '        End If

        '        frmPrint.PrintSaleVolume(dt, dtPurchase)
        '        frmPrint.WindowState = FormWindowState.Maximized
        '        frmPrint.Show()
        '        Clear()
        '    Else
        '        Clear()
        '        Return True
        '    End If
        'Else
        '    Return False
        'End If

    End Function

    Private Function GetDataSalesVolume() As SalesVolumeHeaderInfo
        GetDataSalesVolume = New SalesVolumeHeaderInfo
        With GetDataSalesVolume
            .SalesVolumeID = _SalesVolumeID
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
            .IsSolidVolume = ChkIsSolidVolume.Checked
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
            txtCurrentPrice.BackColor = Color.White
        Else
            txtCurrentPrice.ReadOnly = True
            txtCurrentPrice.BackColor = Color.Linen
        End If
        txtCurrentPrice.Text = 0
        txtItemCategory.Text = ""
        txtItemName.Text = ""
        txtGoldQuality.Text = ""
        txtLength.Text = ""
        txtItemK.Text = "0"
        txtItemP.Text = "0"
        txtItemY.Text = "0.0"
        txtItemTK.Text = "0.0"
        txtItemTG.Text = "0.0"

        txtWasteK.Text = "0"
        txtWasteP.Text = "0"
        txtWasteY.Text = "0.0"
        txtWasteTK.Text = "0.0"
        txtWasteTG.Text = "0.0"

        txtTotalK.Text = "0"
        txtTotalP.Text = "0"
        txtTotalY.Text = "0.0"
        txtTotalTK.Text = "0.0"
        txtTotalTG.Text = "0.0"

        txtItemK.ReadOnly = True
        txtItemP.ReadOnly = True
        txtItemY.ReadOnly = True
        txtItemTG.ReadOnly = True

        txtItemK.BackColor = Color.Linen
        txtItemP.BackColor = Color.Linen
        txtItemY.BackColor = Color.Linen
        txtItemTG.BackColor = Color.Linen
        txtWasteK.ReadOnly = True
        txtWasteP.ReadOnly = True
        txtWasteY.ReadOnly = True
        txtWasteTG.ReadOnly = True

        txtWasteK.BackColor = Color.Linen
        txtWasteP.BackColor = Color.Linen
        txtWasteY.BackColor = Color.Linen
        txtWasteTG.BackColor = Color.Linen

        _ItemTG = "0"
        _ItemTK = "0"
        _TotalTG = "0"
        _TotalTK = "0"
        _SaleItemTG = "0"
        _SaleItemTK = "0"
        _WasteTG = "0"
        _WasteTK = "0"

        chkIsFixPrice.Checked = False
        txtGoldPrice.Text = "0"
        txtSaleFixPrice.Text = "0"
        txtQty.Text = "0"
        txtTotalAmt.Text = "0"
        txtNetAmt.Text = ""
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
        _IsGram = False
        _QTY = 0
        _SalesVolumeDetailID = ""
        txtDesignCharges.Text = "0"
        txtDesignRate.Text = "0"
    End Sub
    Private Sub SaleVolumeGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.SaleVolumeStock.ToString)
        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtSalesVolumeID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpSaleDate.Value)
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
        End If

    End Sub
    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _SalesVolumeID = "0"
        SaleVolumeGenerateFormat()
        _LocationID = ""
        dtpSaleDate.Value = Now
        cboStaff.SelectedIndex = -1
        _CustomerID = ""
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtRemark.Text = ""
        txtAllTotalAmt.Text = 0
        txtAllAddOrSub.Text = 0
        txtAllNetAmt.Text = 0
        txtDiscountAmt.Text = 0
        txtValue.Text = 0
        txtPoint.Text = 0
        txtPaidAmt.Text = 0
        txtPromotionAmt.Text = 0
        txtPromotionDis.Text = 0
        txtBalanceAmt.Text = ""
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
            txtPromotionDis.BackColor = Color.White
        Else
            txtPromotionDis.ReadOnly = True
            txtPromotionDis.BackColor = Color.Linen
        End If

        txtPoint.Enabled = False
        txtValue.Enabled = False
        grpMember.Visible = False
        RedeemID = ""
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
                    txtMemberCode.BackColor = Color.Linen
                Else
                    txtMemberCode.ReadOnly = False
                    txtMemberCode.BackColor = Color.White
                End If
            Else
                MsgBox("Please Check Internet Connection!")
            End If

        Else
            txtMemberCode.Visible = False
            txtMemberID.Visible = False
            txtMemberName.Visible = False
            txtMemberCode.BackColor = Color.Linen
        End If



        RedeemID = IIf(RedeemID = "", "", RedeemID)
        IsRedeemInvoice = False
        ' txtPoint.Text = IIf(txtPoint.Text = "", 0, txtPoint.Text)
        DiscountType = ""
        'txtDiscountAmt.Text = 0
        lblRedeemItem.Visible = False
        RedeemSuccess = False
        TopUpSuccess = False
        txtBalanceAmt.Text = 0
        _TransactionID = ""
        _RedeemID = ""
        TempRedeemID = ""
        RedeemID = ""
        InvoiceStatus = 0
        _IsUpdateHeader = False
        _isMaximum = False

        Dim dc As New DataColumn
        _dtItemBarcode = New DataTable
        _dtItemBarcode.Columns.Add("SalesVolumeDetailID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("SalesVolumeID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ForSaleID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemCode", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemCategoryID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemNameID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemCategory", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("Length", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("QTY", System.Type.GetType("System.Int64"))

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

        dc = New DataColumn
        dc.ColumnName = "WasteTG"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtItemBarcode.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "WasteTK"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtItemBarcode.Columns.Add(dc)

        _dtItemBarcode.Columns.Add("SalesRate", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("GoldPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsFixPrice", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("FixPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("TotalAmount", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("AddOrSub", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("NetAmount", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("DesignCharges", System.Type.GetType("System.Int32"))
        _dtItemBarcode.Columns.Add("DesignChargesRate", System.Type.GetType("System.Int32"))


        grdDetail.AutoGenerateColumns = False
        grdDetail.ReadOnly = True
        grdDetail.DataSource = _dtItemBarcode

        FormatGridItemDetail()
        txtBarcodeNo.Text = ""
        ClearItemCode()
        ChkIsSolidVolume.Checked = False
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
                .HeaderText = "SalesVolumeDetailID"
                .DataPropertyName = "SalesVolumeDetailID"
                .Name = "SalesVolumeDetailID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "SalesVolumeID"
                .DataPropertyName = "SalesVolumeID"
                .Name = "SalesVolumeID"
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
                .Width = 95
                .Visible = True
            End With
            .Columns.Add(dcDia)

            Dim dcItemTG As New DataGridViewTextBoxColumn
            With dcItemTG
                .HeaderText = "Item(G)"
                .DataPropertyName = "ItemTG"
                .Name = "ItemTG"
                .Width = 60
                .Visible = False
                .DefaultCellStyle.Format = "0.000"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcItemTG)

            Dim dcItemTK As New DataGridViewTextBoxColumn
            With dcItemTK
                .HeaderText = "ItemTK"
                .DataPropertyName = "ItemTK"
                .Name = "ItemTK"
                .Visible = False
            End With
            .Columns.Add(dcItemTK)

            Dim dcWasteTG As New DataGridViewTextBoxColumn
            With dcWasteTG
                .HeaderText = "Waste(G)"
                .DataPropertyName = "WasteTG"
                .Name = "WasteTG"
                .Visible = False
                .Width = 60
                .DefaultCellStyle.Format = "0.000"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcWasteTG)

            Dim dcWasteTK As New DataGridViewTextBoxColumn
            With dcWasteTK
                .HeaderText = "WasteTK"
                .DataPropertyName = "WasteTK"
                .Name = "WasteTK"
                .Visible = False
            End With
            .Columns.Add(dcWasteTK)

            Dim dcItemCategoryID As New DataGridViewTextBoxColumn
            With dcItemCategoryID
                .HeaderText = "ItemCategoryID"
                .DataPropertyName = "ItemCategoryID"
                .Name = "ItemCategoryID"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcItemCategoryID)

            Dim dcItemCategory As New DataGridViewTextBoxColumn
            With dcItemCategory
                .HeaderText = "ItemCategory"
                .DataPropertyName = "ItemCategory"
                .Name = "ItemCategory"
                .Width = 105
                .Visible = True
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcItemCategory)

            Dim dcItemNameID As New DataGridViewTextBoxColumn
            With dcItemNameID
                .HeaderText = "ItemNameID"
                .DataPropertyName = "ItemNameID"
                .Name = "ItemNameID"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcItemNameID)

            Dim dcItemName As New DataGridViewTextBoxColumn
            With dcItemName
                .HeaderText = "ItemName"
                .DataPropertyName = "ItemName"
                .Name = "ItemName"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcItemName)

            Dim dcGoldQualityID As New DataGridViewTextBoxColumn
            With dcGoldQualityID
                .HeaderText = "GoldQualityID"
                .DataPropertyName = "GoldQualityID"
                .Name = "GoldQualityID"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcGoldQualityID)

            Dim dcGoldQuality As New DataGridViewTextBoxColumn
            With dcGoldQuality
                .HeaderText = "GoldQuality"
                .DataPropertyName = "GoldQuality"
                .Name = "GoldQuality"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcGoldQuality)

            Dim dcLength As New DataGridViewTextBoxColumn
            With dcLength
                .HeaderText = "Length"
                .DataPropertyName = "Length"
                .Name = "Length"
                .Width = 100
                .Visible = False
            End With
            .Columns.Add(dcLength)

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

            Dim dcQTY As New DataGridViewTextBoxColumn()
            With dcQTY
                .HeaderText = "QTY"
                .DataPropertyName = "QTY"
                .Name = "QTY"
                .Width = 40
                If ChkIsSolidVolume.Checked Then
                    .Visible = False
                Else
                    .Visible = True
                End If
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcQTY)

            Dim dcGoldFee As New DataGridViewTextBoxColumn
            With dcGoldFee
                .HeaderText = "GoldPrice"
                .DataPropertyName = "GoldPrice"
                .Name = "GoldPrice"
                .Visible = False
            End With
            .Columns.Add(dcGoldFee)

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
        End With
    End Sub

    Private Sub ShowCurrentPrice(ByVal objCurrentPrice As CurrentPriceInfo)
        txtCurrentPrice.Text = objCurrentPrice.SalesRate
    End Sub

    Private Sub SearchSale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchSale.Click
        btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objSaleVolumeHeader As New SalesVolumeHeaderInfo


        dt = objSalesVolumeController.GetAllSalesVolume()
        DataItem = DirectCast(SearchData.FindFast(dt, "Sale Invoice Item List"), DataRow)

        If DataItem IsNot Nothing Then
            ' _IsGemInDB = True
            _SalesVolumeID = DataItem.Item("VoucherNo").ToString()
            objSaleVolumeHeader = objSalesVolumeController.GetSalesVolumeHeaderByID(_SalesVolumeID)
            _IsSearch = True
            _IsUpdateHeader = True
            _dtItemBarcode.Rows.Clear()
            _dtItemBarcode = objSalesVolumeController.GetSalesVolumeDetailByID(_SalesVolumeID)
            grdDetail.DataSource = _dtItemBarcode
            ShowSaleDiaBarcodeData(objSaleVolumeHeader)
            btnDelete.Enabled = True
        Else
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString

        End If
    End Sub
    Private Sub ShowSaleDiaBarcodeData(ByVal objSaleHeader As SalesVolumeHeaderInfo)

        With objSaleHeader
            dtpSaleDate.Value = .SaleDate
            txtSalesVolumeID.Text = .SalesVolumeID
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
            txtDiscountAmt.Text = .DiscountAmount
            txtPromotionDis.Text = .PromotionDiscount
            txtPromotionAmt.Text = CLng(CLng(txtAllTotalAmt.Text) * (CLng(txtPromotionDis.Text) / 100))
            txtMemberDis.Text = .MemberDis
            txtMemberDisAmt.Text = .MemberDiscountAmt
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text)))
            _PurchaseHeaderID = .PurchaseHeaderID
            txtPurchaseVoucherNo.Text = .PurchaseHeaderID
            txtPurchaseAmount.Text = .PurchaseAmount
            txtPaidAmt.Text = .PaidAmount

            If _PurchaseHeaderID <> "" Then
                txtDifferentAmount.Text = CLng(txtAllNetAmt.Text) - CLng(txtPurchaseAmount.Text)
                txtBalanceAmt.Text = CStr(CLng(txtDifferentAmount.Text) - CLng(txtPaidAmt.Text))
            Else
                txtDifferentAmount.Text = "0"
                txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text))
            End If
            txtRemark.Text = .Remark
            _IsAllowDiscount = False
            ChkIsSolidVolume.Checked = .IsSolidVolume
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


    Public Function GoldQualityIsGramRate() As Boolean
        Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
        GoldQualityInfo = _GoldQualityController.GetGoldQuality(_GoldQualityID)
        Return GoldQualityInfo.IsGramRate
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim obj As New CommonInfo.SalesVolumeHeaderInfo
        Dim _PurchaseHeaderID As String
        Dim dtPurchase As New DataTable

        obj = objSalesVolumeController.GetSaleVolumeByID(_SalesVolumeID)
        _PurchaseHeaderID = obj.PurchaseHeaderID

        If _PurchaseHeaderID = "" Then
            dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

            If dt.Rows.Count() = 0 Then
                MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                    _GoldQualityID = dr.Item("GoldQualityID")
                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                Next
            End If
        Else
            dt = objSalesVolumeController.GetSalesVolumePrint(_SalesVolumeID)

            If dt.Rows.Count() = 0 Then
                MsgBox("Please Select Sale Volume Voucher First!", MsgBoxStyle.Information, AppName)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                    _GoldQualityID = dr.Item("GoldQualityID")
                    '_IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
                Next
            End If
            dtPurchase = _ObjPurchaseController.GetAllPurchasePrintForSaleVolume(_SalesVolumeID)
        End If
        dt.Rows(0).Item("PointBalance") = VoucherPointBalance
        frmPrint.PrintSaleVolume(dt, dtPurchase)
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
                If Global_IsMemberCustomer = True Then
                    txtMemberCode.Text = DataItem.Item("MemberCode").ToString
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
            If ChkIsSolidVolume.Checked = True Then
                objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo, " And F.IsExit = '0' AND F.IsOrder='0' AND IsSolidVolume='1' ")
            Else
                objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo, " And F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='1' ")
            End If
            ShowForSaleBarcodeData(objSItem)
        Else
            ClearItemCode()
        End If
    End Sub
    Private Sub ShowForSaleBarcodeData(ByVal objSItem As SalesItemInfo)
        Dim objCurrentPrice As New CurrentPriceInfo
        Dim GoldQualityInfo As New GoldQualityInfo

        With objSItem
            _ForSaleID = .ForSaleID

            _ItemCategoryID = .ItemCategoryID
            txtItemCategory.Text = _ItemCategoryController.GetItemCategory(_ItemCategoryID).ItemCategory
            _ItemNameID = .ItemNameID

            txtLength.Text = .Length
            txtItemName.Text = .ItemName
            _GoldQualityID = .GoldQualityID
            txtGoldQuality.Text = _GoldQualityController.GetGoldQuality(_GoldQualityID).GoldQuality
            _IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
            _ItemTK = Math.Round(.LossItemTK, 3)
            _ItemTG = Math.Round(.LossItemTG, 3)

            GoldQualityForTextChange()
            _QTY = .LossQTY
            If _SalesVolumeDetailID = "" Then
                If _ForSaleID = "" Then
                    lblWeight.Text = ""
                Else
                    If ChkIsSolidVolume.Checked = True Then
                        lblWeight.Text = CStr(_ItemTG) + " TG  -  " + CStr(_ItemTK) + " TK" + CStr(IIf(.IsFixPrice = False, "", "  -  ")) + CStr(IIf(.IsFixPrice = False, "", .FixPrice))
                    Else
                        lblWeight.Text = CStr(.LossQTY) + "  -  " + CStr(_ItemTG) + " TG  -  " + CStr(_ItemTK) + " TK" + CStr(IIf(.IsFixPrice = False, "", "  -  ")) + CStr(IIf(.IsFixPrice = False, "", .FixPrice))
                    End If
                    End If
            Else
                    lblWeight.Text = ""
            End If

            objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(_GoldQualityID)
            txtCurrentPrice.Text = objCurrentPrice.SalesRate
            _IsGram = _GoldQualityController.GetGoldQuality(.GoldQualityID).IsGramRate

            If _ForSaleID = "" Then
                lblIsGram.Text = ""
            Else
                If _IsGram Then
                    lblIsGram.Text = "၁ ဂရမ်စျေး"
                    txtDesignRate.Enabled = True
                    txtDesignRate.Text = "0"
                Else
                    lblIsGram.Text = "၁ ကျပ်သားစျေး"
                    txtDesignRate.Enabled = False
                    txtDesignRate.Text = "0"
                End If
            End If
            chkIsFixPrice.Checked = .IsFixPrice

            If (.IsFixPrice = True) Then
                isFixPrice = True
                txtGoldPrice.Text = "0"
                lblDonePrice.Visible = True
                txtSaleFixPrice.Visible = True
                txtSaleFixPrice.Text = .FixPrice
            Else
                isFixPrice = False
                lblDonePrice.Visible = False
                txtSaleFixPrice.Visible = False
            End If
        End With

    End Sub

    Private Sub GoldQualityForTextChange()
        If _IsGram = True Then
            txtItemK.ReadOnly = True
            txtItemP.ReadOnly = True
            txtItemY.ReadOnly = True
            txtItemTG.ReadOnly = False
            txtItemTG.TabIndex = 14
            txtWasteTG.TabIndex = 15
            txtItemK.TabStop = False
            txtItemP.TabStop = False
            txtItemY.TabStop = False
            txtWasteK.TabStop = False
            txtWasteP.TabStop = False
            txtWasteY.TabStop = False
            txtItemTG.TabStop = True
            txtWasteTG.TabStop = True

            txtItemK.BackColor = Color.Linen
            txtItemP.BackColor = Color.Linen
            txtItemY.BackColor = Color.Linen
            txtItemTG.BackColor = Color.White


            txtWasteK.ReadOnly = True
            txtWasteP.ReadOnly = True
            txtWasteY.ReadOnly = True
            txtWasteTG.ReadOnly = False


            txtWasteK.BackColor = Color.Linen
            txtWasteP.BackColor = Color.Linen
            txtWasteY.BackColor = Color.Linen
            txtWasteTG.BackColor = Color.White
        Else
            txtItemK.ReadOnly = False
            txtItemP.ReadOnly = False
            txtItemY.ReadOnly = False
            txtItemTG.ReadOnly = True
            txtItemK.TabIndex = 14
            txtItemP.TabIndex = 15
            txtItemY.TabIndex = 16
            txtWasteK.TabIndex = 17
            txtWasteP.TabIndex = 18
            txtWasteY.TabIndex = 19
            txtItemTG.TabStop = False
            txtWasteTG.TabStop = False
            txtItemK.TabStop = True
            txtItemP.TabStop = True
            txtItemY.TabStop = True
            txtWasteK.TabStop = True
            txtWasteP.TabStop = True
            txtWasteY.TabStop = True

            txtItemK.BackColor = Color.White
            txtItemP.BackColor = Color.White
            txtItemY.BackColor = Color.White
            txtItemTG.BackColor = Color.Linen

            txtWasteK.ReadOnly = False
            txtWasteP.ReadOnly = False
            txtWasteY.ReadOnly = False
            txtWasteTG.ReadOnly = True

            txtWasteK.BackColor = Color.White
            txtWasteP.BackColor = Color.White
            txtWasteY.BackColor = Color.White
            txtWasteTG.BackColor = Color.Linen
        End If
    End Sub

    Private Sub txtItemTG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtItemTG_TextChanged(sender As Object, e As EventArgs) Handles txtItemTG.TextChanged
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.0"

        If Val(txtItemTG.Text.Trim) >= 0.0 Then
            If _IsGram = True Then
                CalculateItemWeightForGram()
            End If
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub
    Private Sub CalculateItemWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemK.Text = "" Then txtItemK.Text = "0"
        If txtItemP.Text = "" Then txtItemP.Text = "0"
        If txtItemY.Text = "" Then txtItemY.Text = "0.00"

        If (Val(txtItemK.Text) > 0 Or Val(txtItemP.Text) > 0 Or Val(txtItemY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtItemK.Text)
            GoldWeight.WeightP = CInt(txtItemP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtItemY.Text)
            GoldWeight.WeightC = CDec(txtItemY.Text) - GoldWeight.WeightY
            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _SaleItemTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
            _SaleItemTG = GoldWeight.Gram
            txtItemTG.Text = Math.Round(_SaleItemTG, 3)
            txtItemTK.Text = Math.Round(_SaleItemTK, 3)
        Else
            _SaleItemTG = 0.0
            _SaleItemTK = 0.0
            'txtItemK.Text = "0"
            'txtItemP.Text = "0"
            'txtItemY.Text = "0.0"
            txtItemTG.Text = "0.0"
            txtItemTK.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateItemWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.0"

        If Val(txtItemTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtItemTG.Text)
            _SaleItemTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _SaleItemTK = GoldWeight.GoldTK

            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtItemTK.Text = Math.Round(_SaleItemTK, 3)
        ElseIf Val(txtItemTG.Text) > 0 And _WeightType = "Gram" Then
            GoldWeight.Gram = Format(Math.Round(CDec(txtItemTG.Text), 3), "0.000")
            _ItemTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _ItemTK = GoldWeight.GoldTK

            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtItemTK.Text = Format(_ItemTK, "0.000")
        Else
            _SaleItemTG = 0.0
            _SaleItemTK = 0.0

            txtItemK.Text = "0"
            txtItemP.Text = "0"
            txtItemY.Text = "0.0"
            txtItemTK.Text = "0.0"
            'txtItemTG.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateTotalWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If CStr(_SaleItemTG) <> "" Or CStr(_WasteTG) <> "" Then
            If _SaleItemTG <> 0.0 Or _WasteTG <> 0.0 Then

                Dim ItemWeight As New CommonInfo.GoldWeightInfo
                Dim WasteWeight As New CommonInfo.GoldWeightInfo
                Dim TotalWeight As New CommonInfo.GoldWeightInfo

                ItemWeight.WeightK = CDec(txtItemK.Text)
                ItemWeight.WeightP = CDec(txtItemP.Text)
                ItemWeight.WeightY = CDec(txtItemY.Text)

                WasteWeight.WeightK = CDec(txtWasteK.Text)
                WasteWeight.WeightP = CDec(txtWasteP.Text)
                WasteWeight.WeightY = CDec(txtWasteY.Text)

                weightY = ItemWeight.WeightY + WasteWeight.WeightY
                If weightY >= Global_PToY Then
                    weightP = 1
                    weightY = weightY - Global_PToY
                End If

                weightP += ItemWeight.WeightP + WasteWeight.WeightP
                If weightP >= 16 Then
                    weightK = 1
                    weightP = weightP - 16
                End If

                weightK += ItemWeight.WeightK + WasteWeight.WeightK

                TotalWeight.WeightY = weightY
                TotalWeight.WeightP = weightP
                TotalWeight.WeightK = weightK

                txtTotalK.Text = Format(TotalWeight.WeightK, "0")
                txtTotalP.Text = Format(TotalWeight.WeightP, "0")
                If numberformat = 1 Then
                    txtTotalY.Text = Math.Round(TotalWeight.WeightY, 1)
                Else
                    txtTotalY.Text = Math.Round(TotalWeight.WeightY, 2)
                End If
                'txtTotalY.Text = Math.Round(TotalWeight.WeightY, 1)

                TotalWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(TotalWeight)
                _TotalTK = TotalWeight.GoldTK
                TotalWeight.Gram = TotalWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
                _TotalTG = TotalWeight.Gram

                txtTotalTG.Text = Format(CDec(txtItemTG.Text) + CDec(txtWasteTG.Text), "0.000")
                txtTotalTK.Text = Format(CDec(txtItemTK.Text) + CDec(txtWasteTK.Text), "0.000")
            Else
                _TotalTG = 0.0
                _TotalTG = 0.0

                txtTotalTG.Text = "0.0"
                txtTotalTK.Text = "0.0"
                txtTotalK.Text = "0"
                txtTotalP.Text = "0"
                txtTotalY.Text = "0.00"
            End If
        End If
    End Sub

    Private Sub txtItemK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub
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


    Private Sub txtItemK_TextChanged(sender As Object, e As EventArgs) Handles txtItemK.TextChanged
        If txtItemK.Text = "" Then txtItemK.Text = "0"

        If Val(txtItemK.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()

            End If
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtItemP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub

    Private Sub txtItemP_TextChanged(sender As Object, e As EventArgs) Handles txtItemP.TextChanged
        If txtItemP.Text = "" Then txtItemP.Text = "0"
        If Val(txtItemP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtItemP.Text = 0
            txtItemP.SelectAll()
        End If

        If Val(txtItemP.Text.Trim) >= 0 And _IsGram = False Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtItemY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
        _WeightType = "Kyat"
    End Sub

    Private Sub txtItemY_TextChanged(sender As Object, e As EventArgs) Handles txtItemY.TextChanged
        If txtItemY.Text = "" Then txtItemY.Text = "0.0"
        If Val(txtItemY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtItemY.Text = "0.0"
            txtItemY.SelectAll()
        End If

        If Val(txtItemY.Text.Trim) >= 0 And _IsGram = False Then
            If (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtWasteTG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtWasteTG_TextChanged(sender As Object, e As EventArgs) Handles txtWasteTG.TextChanged
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0.0"

        If Val(txtWasteTG.Text.Trim) >= 0.0 And _IsGram = True Then
            CalculateWasteWeightForGram()
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub CalculateWasteWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0.0"

        If Val(txtWasteTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtWasteTG.Text)
            _WasteTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _WasteTK = GoldWeight.GoldTK

            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteK.Text = CStr(GoldWeight.WeightK)
            txtWasteP.Text = CStr(GoldWeight.WeightP)
            txtWasteY.Text = CStr(Math.Round(GoldWeight.WeightY + GoldWeight.WeightC, 1))
            txtWasteTK.Text = Math.Round(_WasteTK, 3)
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0

            txtWasteK.Text = "0"
            txtWasteP.Text = "0"
            txtWasteY.Text = "0.0"
            txtWasteTG.Text = "0.0"
            txtWasteTK.Text = "0.0"
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

            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _WasteTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
            _WasteTG = GoldWeight.Gram
            txtWasteTG.Text = Math.Round(_WasteTG, 3)
            txtWasteTK.Text = Math.Round(_WasteTK, 3)
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0

            txtWasteK.Text = "0"
            txtWasteP.Text = "0"
            txtWasteY.Text = "0.0"
            txtWasteTG.Text = "0.0"
            txtWasteTK.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateGoldPrice()
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"
        If txtSaleFixPrice.Text = "" Then txtSaleFixPrice.Text = "0"
        If txtQty.Text = "" Then txtQty.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        Dim _DesignCharges As Integer = 0

        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        GoldWeight.WeightK = CInt(txtTotalK.Text)
        GoldWeight.WeightP = CInt(txtTotalP.Text)
        GoldWeight.WeightY = System.Decimal.Truncate(txtTotalY.Text)
        GoldWeight.WeightC = CDec(txtTotalY.Text) - GoldWeight.WeightY
        GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
        _TotalTK = GoldWeight.GoldTK

        If (chkIsFixPrice.Checked = True) Then
            txtGoldPrice.Text = "0"
            txtTotalAmt.Text = CStr((CInt(txtSaleFixPrice.Text) * CInt(txtQty.Text)) + CLng(txtDesignCharges.Text))
        Else
            If _IsGram = False Then
                _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * _TotalTK)
                _DesignCharges = txtDesignCharges.Text
            Else
                _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * CDec(txtTotalTG.Text))
                _DesignCharges = CStr(CLng(txtDesignRate.Text) * (CDec(txtTotalTG.Text)))
            End If
            txtGoldPrice.Text = CLng(_GoldPrice)
            txtTotalAmt.Text = CStr(CLng(txtGoldPrice.Text) + CLng(txtDesignCharges.Text))
            txtDesignCharges.Text = Format(_DesignCharges, "###,##0.##")
        End If
    End Sub

    Private Sub txtWasteK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtWasteK_TextChanged(sender As Object, e As EventArgs) Handles txtWasteK.TextChanged
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"

        If Val(txtWasteK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteWeightForKPY()
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtWasteP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
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
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtWasteY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtWasteY_TextChanged(sender As Object, e As EventArgs) Handles txtWasteY.TextChanged
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.00"

        If Val(txtWasteY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtWasteY.Text = 0
            txtWasteY.SelectAll()
        End If
        If Val(txtWasteY.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteWeightForKPY()
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
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
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            If numberformat = 1 Then
                txtItemY.Text = Math.Round(GoldWeight.WeightY + GoldWeight.WeightC, 1)
            Else
                txtItemY.Text = Math.Round(GoldWeight.WeightY + GoldWeight.WeightC, 2)
            End If
            'txtItemY.Text = Math.Round(GoldWeight.WeightY + GoldWeight.WeightC, 1)
        ElseIf _IsGram = False And _WeightType = "Gram" Then
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            'If txtVItemTG.Text = "" Then txtVItemTG.Text = "0.0"
            'If Val(txtVItemTG.Text.Trim) >= 0.0 Then
            'If _VIsGram = True Then
            CalculateItemWeightForGram()
            'End If
            'End If
        Else
            txtItemTG.Text = Math.Round(GoldWeight.Gram, 3)
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtBarcodeNo.Focus()
        txtBarcodeNo.Text = ""
        ClearItemCode()
    End Sub

    Private Sub txtGoldPrice_TextChanged(sender As Object, e As EventArgs) Handles txtSaleFixPrice.TextChanged
        If txtSaleFixPrice.Text = "" Then txtSaleFixPrice.Text = "0"
        If txtSaleFixPrice.Text.Trim >= 0 Then
            CalculateGoldPrice()
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
            If ChkIsSolidVolume.Checked = False Then
                Dim dt As DataTable
                dt = objSalesVolumeController.GetSaleVolumeDetailByDetailID(_SalesVolumeDetailID)
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
            Else
                Dim dtLossWeight As DataTable
                dtLossWeight = objSalesVolumeController.GetSaleVolumeDetailByDetailID(_SalesVolumeDetailID)
                If dtLossWeight.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLossWeight.Rows
                        _OldTG = dr.Item("ItemTG")
                    Next
                Else
                    _OldTG = 0
                End If

                If CDec(txtItemTG.Text) > _ItemTG + _OldTG Or txtItemTG.Text = "0" Then
                    MsgBox("Please Check Weight!", MsgBoxStyle.Information, "Data Require!")
                    Exit Sub
                End If
            End If
        Else
            If ChkIsSolidVolume.Checked = False Then
                If txtQty.Text > _QTY Or txtQty.Text = "0" Then
                    MsgBox("Please Check Quantity!", MsgBoxStyle.Information, "Data Require!")
                    Exit Sub
                End If
            Else
                If CDec(txtItemTG.Text) > _ItemTG Or txtItemTG.Text = "0" Then
                    MsgBox("Please Check Weight!", MsgBoxStyle.Information, "Data Require!")
                    Exit Sub
                End If
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

        If _SaleItemTG = "0" Or _SaleItemTG <= 0 Then
            MsgBox("Please Enter  Item Weight!", MsgBoxStyle.Information, "Data Require!")
            Exit Sub
        End If

        If _IsUpdate Then
            UpdateItem(_SalesVolumeDetailID)
            txtBarcodeNo.Text = ""
            ClearItemCode()
        Else

            If btnAdd.Text = "Add" Then
                _SalesVolumeDetailID = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SalesVolumeDetail, EnumSetting.GenerateKeyType.SalesVolumeDetail.ToString, dtpSaleDate.Value)
                InsertItem(_SalesVolumeDetailID)
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
    Public Sub InsertItem(ByVal _SalesVolumeDetailID As String)
        Dim drItem As DataRow

        drItem = _dtItemBarcode.NewRow
        drItem.Item("SalesVolumeDetailID") = _SalesVolumeDetailID
        drItem.Item("SalesVolumeID") = _SalesVolumeID
        drItem.Item("ForSaleID") = _ForSaleID
        drItem.Item("ItemCode") = txtBarcodeNo.Text
        drItem.Item("ItemCategoryID") = _ItemCategoryID
        drItem.Item("ItemNameID") = _ItemNameID
        drItem.Item("GoldQualityID") = _GoldQualityID
        drItem.Item("ItemCategory") = txtItemCategory.Text
        drItem.Item("ItemName") = txtItemName.Text
        drItem.Item("GoldQuality") = txtGoldQuality.Text
        drItem.Item("Length") = txtLength.Text
        drItem.Item("ItemTG") = _SaleItemTG
        drItem.Item("ItemTK") = _SaleItemTK
        drItem.Item("WasteTG") = _WasteTG
        drItem.Item("WasteTK") = _WasteTK
        drItem.Item("SalesRate") = txtCurrentPrice.Text
        drItem.Item("GoldPrice") = txtGoldPrice.Text
        drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
        drItem.Item("FixPrice") = txtSaleFixPrice.Text
        drItem.Item("TotalAmount") = txtTotalAmt.Text
        drItem.Item("AddOrSub") = txtAddSub.Text
        drItem.Item("NetAmount") = txtNetAmt.Text
        drItem.Item("QTY") = txtQty.Text
        drItem.Item("DesignCharges") = CInt(txtDesignCharges.Text)
        drItem.Item("DesignChargesRate") = txtDesignRate.Text
        _dtItemBarcode.Rows.Add(drItem)
        grdDetail.DataSource = _dtItemBarcode

        'Dim drDiamond As DataRow
        'For Each dr As DataRow In _dtSalesInvoiceItem.Rows
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

    Public Sub UpdateItem(ByVal _SalesVolumeDetailID As String)
        Dim drItem As DataRow
        drItem = _dtItemBarcode.Rows(grdDetail.CurrentRow.Index)

        If Not IsNothing(drItem) Then
            drItem.Item("SalesVolumeDetailID") = _SalesVolumeDetailID
            drItem.Item("SalesVolumeID") = _SalesVolumeID
            drItem.Item("ForSaleID") = _ForSaleID
            drItem.Item("ItemCode") = txtBarcodeNo.Text
            drItem.Item("ItemCategoryID") = _ItemCategoryID
            drItem.Item("ItemNameID") = _ItemNameID
            drItem.Item("GoldQualityID") = _GoldQualityID
            drItem.Item("ItemCategory") = txtItemCategory.Text
            drItem.Item("ItemName") = txtItemName.Text
            drItem.Item("GoldQuality") = txtGoldQuality.Text
            drItem.Item("Length") = txtLength.Text
            drItem.Item("ItemTG") = _SaleItemTG
            drItem.Item("ItemTK") = _SaleItemTK
            drItem.Item("WasteTG") = _WasteTG
            drItem.Item("WasteTK") = _WasteTK
            drItem.Item("SalesRate") = txtCurrentPrice.Text
            drItem.Item("GoldPrice") = txtGoldPrice.Text
            drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
            drItem.Item("FixPrice") = txtSaleFixPrice.Text
            drItem.Item("TotalAmount") = txtTotalAmt.Text
            drItem.Item("AddOrSub") = txtAddSub.Text
            drItem.Item("QTY") = txtQty.Text
            drItem.Item("NetAmount") = txtNetAmt.Text
            drItem.Item("DesignCharges") = CInt(txtDesignCharges.Text)
            drItem.Item("DesignChargesRate") = CInt(txtDesignRate.Text)
            grdDetail.DataSource = _dtItemBarcode

        End If
    End Sub

    Private Sub grdDetail_Click(sender As Object, e As EventArgs) Handles grdDetail.Click
        Dim GoldWeight As New GoldWeightInfo
        Dim objSItem As New SalesItemInfo
        Dim objCurrentPrice As New CurrentPriceInfo
        Dim GoldQualityInfo As New GoldQualityInfo

        If grdDetail.RowCount = 0 Then
            _IsUpdate = False
            Exit Sub
        End If

        With grdDetail
            _SalesVolumeDetailID = .CurrentRow.Cells("SalesVolumeDetailID").Value
            _SalesVolumeID = .CurrentRow.Cells("SalesVolumeID").Value
            _ForSaleID = .CurrentRow.Cells("ForSaleID").Value
            txtBarcodeNo.Text = .CurrentRow.Cells("ItemCode").Value

            _ItemCategoryID = .CurrentRow.Cells("ItemCategoryID").Value
            txtItemCategory.Text = _ItemCategoryController.GetItemCategory(_ItemCategoryID).ItemCategory
            _ItemNameID = .CurrentRow.Cells("ItemNameID").Value

            txtLength.Text = .CurrentRow.Cells("Length").Value
            txtItemName.Text = _ItemNameCon.GetItemName(_ItemNameID).ItemName
            _GoldQualityID = .CurrentRow.Cells("GoldQualityID").Value
            txtGoldQuality.Text = _GoldQualityController.GetGoldQuality(_GoldQualityID).GoldQuality
            _IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate
            GoldQualityForTextChange()

            objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(_GoldQualityID)
            txtCurrentPrice.Text = .CurrentRow.Cells("SalesRate").Value
            ' ShowCurrentPrice(objCurrentPrice)
            GoldQualityInfo = _GoldQualityController.GetGoldQuality(_GoldQualityID)
            _ForSaleID = .CurrentRow.Cells("ForSaleID").Value
            If _ForSaleID = "" Then
                txtCurrentPrice.Text = 0
                lblIsGram.Text = ""
            Else

                If GoldQualityInfo.IsGramRate Then
                    lblIsGram.Text = "၁ ဂရမ်စျေး"
                    txtDesignRate.Text = .CurrentRow.Cells("DesignChargesRate").Value
                    txtDesignRate.Enabled = True
                Else
                    lblIsGram.Text = "၁ ကျပ်သားစျေး"
                    txtDesignRate.Text = "0"
                    txtDesignRate.Enabled = False

                End If
            End If
            chkIsFixPrice.Checked = .CurrentRow.Cells("IsFixPrice").Value
            txtQty.Text = .CurrentRow.Cells("QTY").Value

            If (.CurrentRow.Cells("IsFixPrice").Value = True) Then
                isFixPrice = True
                txtGoldPrice.Text = "0"
                lblDonePrice.Visible = True
                txtSaleFixPrice.Visible = True
                txtSaleFixPrice.Text = .CurrentRow.Cells("FixPrice").Value
            Else
                isFixPrice = False
                lblDonePrice.Visible = False
                txtSaleFixPrice.Visible = False
                txtGoldPrice.Text = .CurrentRow.Cells("GoldPrice").Value
            End If


            'GoldWeight.Gram = CDec(grdDetail.CurrentRow.Cells("ItemTG").Value)
            '_SaleItemTG = GoldWeight.Gram
            'GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            '_SaleItemTK = GoldWeight.GoldTK

            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("ItemTK").Value)
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

            txtItemTG.Text = Format(grdDetail.CurrentRow.Cells("ItemTG").Value, "0.000")
            txtItemTK.Text = Format(grdDetail.CurrentRow.Cells("ItemTK").Value, "0.000")
            _SaleItemTK = CDec(grdDetail.CurrentRow.Cells("ItemTK").Value)
            _SaleItemTG = CDec(grdDetail.CurrentRow.Cells("ItemTG").Value)

            'GoldWeight.Gram = CDec(grdDetail.CurrentRow.Cells("WasteTG").Value)
            '_WasteTG = GoldWeight.Gram
            'GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            '_WasteTK = GoldWeight.GoldTK

            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("WasteTK").Value)
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteK.Text = CStr(GoldWeight.WeightK)
            txtWasteP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtWasteTK.Text = Format(grdDetail.CurrentRow.Cells("WasteTK").Value, "0.000")
            txtWasteTG.Text = Format(grdDetail.CurrentRow.Cells("WasteTG").Value, "0.000")
            _WasteTK = CDec(grdDetail.CurrentRow.Cells("WasteTK").Value)
            _WasteTG = CDec(grdDetail.CurrentRow.Cells("WasteTG").Value)
            CalculateTotalWeight()

            txtTotalAmt.Text = .CurrentRow.Cells("TotalAmount").Value
            txtAddSub.Text = .CurrentRow.Cells("AddOrSub").Value
            txtNetAmt.Text = .CurrentRow.Cells("NetAmount").Value
            txtDesignCharges.Text = .CurrentRow.Cells("DesignCharges").Value
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
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtMemberDis.Text = "" Then txtMemberDis.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"
        If CInt(txtMemberDis.Text) > 0 Then
            txtMemberDisAmt.Text = CStr(CLng(txtAllTotalAmt.Text) * (CLng(txtMemberDis.Text) / 100))
        End If
        txtAllAddOrSub.Text = CStr((CLng(txtAllNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text)) - CLng(txtAllTotalAmt.Text))
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
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub SearchItem_Click(sender As Object, e As EventArgs) Handles SearchItem.Click
        Dim DataItem As DataRow
        Dim dtSale As New DataTable
        Dim objSItem As CommonInfo.SalesItemInfo

        dtSale = _SalesItemController.GetForSalesVolumeItemForSaleInvoice(GetExistedItems(), ChkIsSolidVolume.Checked)
        DataItem = DirectCast(SearchData.FindFast(dtSale, "Sales Volume Item List"), DataRow)

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

    Private Sub Label46_Click(sender As Object, e As EventArgs) Handles Label46.Click

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
        openhelp("SaleVolumeInvoice")
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
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"

        If _PurchaseHeaderID <> "" Then
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text)))
            txtDifferentAmount.Text = CLng(txtAllNetAmt.Text) - CLng(txtPurchaseAmount.Text)
            txtPaidAmt.Text = txtDifferentAmount.Text
            txtBalanceAmt.Text = CStr(CLng(txtDifferentAmount.Text) - CLng(txtPaidAmt.Text))
        Else
            txtAllNetAmt.Text = CStr((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text)))
            txtPaidAmt.Text = txtAllNetAmt.Text
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtPaidAmt.Text))
        End If
    End Sub

    Private Sub btnSearchPurchase_Click(sender As Object, e As EventArgs) Handles btnSearchPurchase.Click
        Dim DataItem As DataRow
        Dim dtPurchase As New DataTable
        dtPurchase = _ObjPurchaseController.GetAllPuchaseHeaderDataBySaleInvoice(_SalesVolumeID, "SaleVolume")
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
            ObjPurchase = _ObjPurchaseController.GetPurchaseHeaderDataBySaleInvoice(_PurchaseHeaderID, _SalesVolumeID, "SaleVolume")
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
        CalculateGoldPrice()
    End Sub
    Private Sub txtDesignCharges_TextChanged(sender As Object, e As EventArgs) Handles txtDesignCharges.TextChanged
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        CalculateGoldPrice()
    End Sub
    Private Sub txtDesignRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesignRate.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtCurrentPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtDesignRate_TextChanged(sender As Object, e As EventArgs) Handles txtDesignRate.TextChanged
        Dim _DesignCharges As Integer = 0
        If _IsGram = True Then
            _DesignCharges = CStr(CLng(txtDesignRate.Text) * (CDec(txtTotalTG.Text)))
        End If
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
    Private Sub chkIsSolidVolume_CheckedChanged(sender As Object, e As EventArgs) Handles ChkIsSolidVolume.CheckedChanged
        'If _IsSearch = False Then
        '    If grdDetail.RowCount > 0 Then
        '        If ChkIsSolidVolume.Checked = True Then
        '            If MsgBox("Do you want to discard selected item? ", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.Yes Then
        '                Clear()
        '                txtQty.Text = "0"
        '                txtQty.Enabled = False
        '            Else
        '                ChkIsSolidVolume.Checked = False
        '                Exit Sub
        '            End If
        '        Else
        '            If MsgBox("Do you want to discard selected item? ", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.Yes Then
        '                Clear()
        '                txtQty.Text = "0"
        '                txtQty.Enabled = True
        '            Else
        '                ChkIsSolidVolume.Checked = True
        '                Exit Sub
        '            End If

        '        End If
        '    Else
        '        If ChkIsSolidVolume.Checked Then
        '            txtQty.Text = "0"
        '            txtQty.Enabled = False
        '        Else
        '            txtQty.Enabled = True
        '        End If
        '    End If
        'Else
        '    If ChkIsSolidVolume.Checked Then
        '        txtQty.Text = "0"
        '        txtQty.Enabled = False
        '    Else
        '        txtQty.Enabled = True
        '    End If
        'End If

        FormatGridItemDetail()

        If ChkIsSolidVolume.Checked Then
            txtQty.Text = "0"
            txtQty.Enabled = False
        Else
            txtQty.Enabled = True
        End If

    End Sub
    Public Sub ChkIsSolidVolume_Click(Sender As Object, e As System.EventArgs) Handles ChkIsSolidVolume.Click
        If grdDetail.RowCount > 0 Then
            If ChkIsSolidVolume.Checked = True Then
                If MsgBox("Do you want to discard selected item? ", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.Yes Then
                    Clear()
                    txtQty.Text = "0"
                    'txtQty.Enabled = False
                Else
                    ChkIsSolidVolume.Checked = False
                    Exit Sub
                End If
            Else
                If MsgBox("Do you want to discard selected item? ", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.Yes Then
                    Clear()
                    txtQty.Text = "0"
                    txtQty.Enabled = True
                Else
                    ChkIsSolidVolume.Checked = True
                    Exit Sub
                End If

            End If
        Else
            If ChkIsSolidVolume.Checked Then
                txtQty.Text = "0"
                txtQty.Enabled = False
            Else
                txtQty.Enabled = True
            End If
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

        ' dtMember = Await WebService.MemberCard.GetMemberByMemberCode(MCode, Global_CompanyName, Token)
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

        'dtRedeemInfo = Await WebService.MemberCard.GetRedeemInfoByMemberCode(MCode, Global_CompanyName, Token)

        If dtRedeemInfo.Rows(0).Item("RedeemID") <> "" Then
            Try
                TempRedeemID = dtRedeemInfo.Rows(0).Item("RedeemID")
                RedeemID = TempRedeemID.Replace("|", ",")
                IsRedeemInvoice = True
                RedeemPoint = dtRedeemInfo.Rows(0).Item("RedeemPoint")
                RedeemValue = dtRedeemInfo.Rows(0).Item("RedeemValue")
                txtPoint.Text = Format(RedeemPoint, "###,##0.##")
                'txtPoint.Enabled = False
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
        If txtAllTotalAmt.Text = "0" Then
            txtMemberDisAmt.Text = 0
        Else
            txtMemberDisAmt.Text = CStr(CLng(txtAllTotalAmt.Text) * (CLng(txtMemberDis.Text) / 100))
        End If
    End Sub

    Private Sub txtMemberDisAmt_TextChanged(sender As Object, e As EventArgs) Handles txtMemberDisAmt.TextChanged
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
            If ((Val((TopupPoint)) + Val(PointBalance)) > Val(MaxPoint)) Then

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


        'dtRedeemID = Await WebService.MemberCard.GetRedeemIDByMemberCode(MCode, Global_CompanyName, Token, InvoiceID)

        If (dtRedeemID.Rows(0).Item("RedeemID") <> "") Then

            Try

                TempRedeemID = dtRedeemID.Rows(0).Item("RedeemID")
                RedeemID = TempRedeemID.Replace("|", ",")

                _RedeemID = dtRedeemID.Rows(0).Item("RedeemID")
                RedeemBool = objSalesVolumeController.UpdateRedeemID(RedeemID, InvoiceID)
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



    Private Async Sub SaveSaleMemberCard(ByVal objSaleInvoice As CommonInfo.SalesVolumeHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean
        'Dim Result As String

        'Dim Result As String
        '  RegName + MemberCode + GwtMember@2020

        'Dim s As String = "GWT1234501320000002GwtMember@2020"
        'Dim s1 As String = Convert.ToBase64String(Encoding.Unicode.GetBytes(s))
        'Dim s2 As String = Encoding.Unicode.GetString(Convert.FromBase64String(s1))

        'Result = Await WebService.MemberCard.SaveSaleMemberCardForVolume(objSaleInvoice, Status, Global_CompanyName)
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

    Private Async Sub UpdateRedeem(ByVal objSaleInvoice As CommonInfo.SalesVolumeHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean

        ' Result = Await WebService.MemberCard.UpdateRedeemForVolume(objSaleInvoice, Status)
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
    Private Async Sub AddRedeem(ByVal objSaleInvoice As CommonInfo.SalesVolumeHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean


        'Result = Await WebService.MemberCard.AddRedeemForVolume(objSaleInvoice, Status)
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

        'dtMember = Await WebService.MemberCard.GetMemberByMemberCode(MCode, Global_CompanyName, Token)

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
        If txtAllTotalAmt.Text = "0" Then
            txtAllNetAmt.Text = 0
        Else
            txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
            txtAllAddOrSub.Text = Format(Val((CLng(txtAllNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + +CLng(txtValue.Text)) - CLng(txtAllTotalAmt.Text)), "###,##0.##")
        End If
    End Sub
    Public Sub UpdateRedeemAndTransID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)
        If _isMaximum = True Then
            GetTransactionID(MCode, Global_CompanyName, Token, _SalesVolumeID)
        End If

        GetRedeemID(MCode, Global_CompanyName, Token, _SalesVolumeID)

    End Sub
    Public Async Sub GetTransactionID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)

        'dtTransactionID = Await WebService.MemberCard.GetTransactionIDByMemberCode(MCode, Global_CompanyName, Token, InvoiceID)

        If (dtTransactionID.Rows(0).Item("TransactionID") <> "") Then

            Try
                _TransactionID = dtTransactionID.Rows(0).Item("TransactionID")

                TransBool = objSalesVolumeController.UpdateTransactionID(_TransactionID, InvoiceID)
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

