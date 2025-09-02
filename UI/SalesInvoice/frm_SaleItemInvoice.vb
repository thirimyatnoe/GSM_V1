Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports ThoughtWorks.QRCode.Codec
Imports System.Drawing
Imports System.Text
Imports System.Net
Imports Operational.AppConfiguration



Public Class frm_SaleItemInvoice
    Implements IFormProcess

    Private _SalesInvoiceID As String = "0"
    Private _SalesInvoiceDetailID As String = ""
    Private _StaffID As String = ""
    Private _ForSaleID As String = ""
    Private _ItemCodeStr As String = ""
    Private _OldItemCode As String = ""

    Private _CustomerID As String = ""
    Private _IsGram As Boolean = False
    Private _dtSalesInvoiceItem As DataTable
    Private _LocationID As String = ""
    Private PName As String = ""
    Private _FixPrice As Integer = 0
    Private _IsUpdate As Boolean = False
    Private _BarcodeNo As String = ""
    Private _PurchaseHeaderID As String = ""
    Private _IsAllUpdate As Boolean = False

    Dim _GoldTK As Decimal = 0
    Dim _GoldTG As Decimal = 0
    Dim _GemsTK As Decimal = 0.0
    Dim _GemsTG As Decimal = 0.0
    Dim _WasteTK As Decimal = 0.0
    Dim _WasteTG As Decimal = 0.0
    Dim _TotalTK As Decimal = 0.0
    Dim _TotalTG As Decimal = 0.0
    Dim _ItemTK As Decimal = 0.0
    Dim _ItemTG As Decimal = 0.0
    Dim _NetTaxAmt As Integer = 0
    Dim _IsSaleReturn As Boolean = False
    Dim _OldTotalItemTG As Decimal
    Dim _OldTotalWasteTG As Decimal

    Dim isFixPrice As Boolean = False
    Private _dtItemBarcode As DataTable
    Private _dtAllDiamond As DataTable
    Private _IsGemInDB As Boolean = False
    Private _IsRowDelete As Boolean = False


    Private IsOriginalFixedPrice As Boolean = False
    Private _OriginalFixedPrice As Long = 0
    Private IsOriginalPriceGram As Boolean = False
    Private _OriginalPriceGram As Long = 0
    Private _OriginalPriceTK As Long = 0
    Private _OriginalGemsPrice As Long = 0
    Private _OriginalOtherPrice As Long = 0
    Private _PurchaseWasteTK As Decimal = 0.0
    Private _PurchaseWasteTG As Decimal = 0.0

    Private _Color As String = ""
    Private _AllPaidAmount As Long = 0
    Private _IsAllowDiscount As Boolean = False
    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
    Private _dtOtherCash As DataTable
    Private grdSaleCategoryTaxAmt As Integer = 0

    Private _AllGrdAmt As Integer = 0

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



    Dim PointBalance As Integer = 0
    Dim Status As Boolean = False
    Dim IsRedeemInvoice As Boolean = False
    Dim DiscountType As String = ""
    Dim OppurtunityType As String = ""

    Dim MemDiscountAmount As Integer = 0
    Dim DiscountPercent As Decimal = 0
    Dim ServiceURL As String = ""

    Dim VoucherPointBalance As Integer = 0
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
    Dim _IsMaximum As Boolean = False

    Dim dtMember As New DataTable()
    Dim dtRedeemID As New DataTable()
    Dim dtTransactionID As New DataTable()

    Private objLocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemNameCon As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private objGemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private objSalesInvoiceController As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _SalesItemController As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _CurrentController As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _ObjPurchaseController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private _CurrentCon As InternationalDiamond.IIntDiamondPriceRateController = Factory.Instance.CreateIntDiamondController
    Dim _WeightType As String = ""
    Dim _dtItemData_His As New DataTable
    Dim strCri As String
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

    Dim numberformat As Integer



    Private Sub frm_SaleItemInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frm_SaleItemInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        numberformat = Global_DecimalFormat

        lblLogInUser.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        _LocationID = Global_CurrentLocationID
        GetcboStaff()
        Clear()
        MyBase.addGridDataErrorHandlers(grdSaleCategory)
        Me.KeyPreview = True
    End Sub
    Private Sub GetcboStaff()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = objStaffController.GetStaffList().DefaultView
        cboStaff.SelectedIndex = -1
    End Sub
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

        For Each dr As DataRow In _dtItemBarcode.Rows
            Dim dtsale As New DataTable
            If dr.RowState <> DataRowState.Deleted Then
                dtsale = objGeneralController.CheckExitVoucherForCashReceipt("tbl_PurchaseDetail PD LEFT JOIN tbl_PurchaseHeader H ON PD.PurchaseHeaderID=H.PurchaseHeaderID", "AND SaleInvoiceDetailID='" + dr.Item("SaleInvoiceDetailID") + "' AND PD.IsShop=1 AND PD.IsOrder=0 AND H.IsDelete=0")
            Else
                dtsale = objGeneralController.CheckExitVoucherForCashReceipt("tbl_PurchaseDetail PD LEFT JOIN tbl_PurchaseHeader H ON PD.PurchaseHeaderID=H.PurchaseHeaderID", "AND SaleInvoiceDetailID='" + CStr(dr.Item("SaleInvoiceDetailID", DataRowVersion.Original)) + "' AND PD.IsShop=1 AND PD.IsOrder=0 AND H.IsDelete=0")
            End If
            If dtsale.Rows.Count() > 0 Then
                MsgBox("Can not delete this Voucher because of Use in SaleReturn!", MsgBoxStyle.Information, "")
                Return False
                Exit Function
            End If
        Next

        Dim dt As New DataTable
        dt = objGeneralController.CheckExitVoucherForCashReceipt("tbl_CashReceipt", "AND VoucherNo='" + _SalesInvoiceID + "' AND Type='SalesInvoice' AND IsDelete=0")
        If dt.Rows.Count() > 0 Then
            MsgBox("Can not delete this Voucher because of Use in Cashreceipt!", MsgBoxStyle.Information, "")
            Return False
            Exit Function
        End If

        Dim objSaleInvoice As CommonInfo.SaleInvoiceHeaderInfo
        objSaleInvoice = GetDataSalesInvoice()
        If objSalesInvoiceController.DeleteSalesInvoice(GetDataSalesInvoice()) Then

            If Global_IsUseMember = True Then
              
                If OppurtunityType <> "Item" Then

                    ''To Filter With RedeemID

                    If IsRedeemInvoice = True Then
                        UpdateRedeem(objSaleInvoice, 2)
                    Else
                        AddRedeem(objSaleInvoice, 2)
                    End If
                End If

                SaveSaleMemberCard(objSaleInvoice, 2)
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
        ' GetcboStaff()
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            Dim objSalesInvoice As New SaleInvoiceHeaderInfo
            objSalesInvoice = GetDataSalesInvoice()
            If _ItemCodeStr <> "" Then
                _dtItemData_His = _SalesItemController.GetForSaleDataByItemCode(_ItemCodeStr)
            End If
            '  _dtItemData_His = _SalesItemController.GetForSaleDataByItemCode(_ItemCodeStr)
            If Global_IsUseMember = False Or txtMemberCode.Text = "" Then
                If objSalesInvoiceController.SaveSalesInvoice(objSalesInvoice, _dtItemBarcode, _dtAllDiamond, _dtOtherCash, _dtItemData_His) Then
                    _SalesInvoiceID = objSalesInvoice.SaleInvoiceHeaderID
                    If chkCancel.Checked = False Then
                        If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                            Dim frmPrint As New frm_ReportViewer
                            Dim dt As New DataTable
                            Dim dtItem As New DataTable
                            Dim dtGem As New DataTable
                            Dim dtPurchase As New DataTable
                            Dim dtVoucherTax As New DataTable
                            Dim _PurchaseHeaderID As String
                            Dim obj As New CommonInfo.SaleInvoiceHeaderInfo
                            Dim dtOtherCash As New DataTable
                            'QRCode
                            Dim objQRCode As QRCodeEncoder = New QRCodeEncoder()
                            objQRCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
                            objQRCode.QRCodeScale = 3
                            objQRCode.QRCodeVersion = 7
                            objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H

                            obj = objSalesInvoiceController.GetSaleInvoiceHeaderByID(_SalesInvoiceID)
                            _PurchaseHeaderID = obj.PurchaseHeaderID
                            If _PurchaseHeaderID = "" Then
                                dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        'Generate QRCode
                                        Dim imgImage As Image
                                        Dim objBitmap As Bitmap
                                        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                                        imgImage = objQRCode.Encode(Global_QRCode)
                                        objBitmap = New Bitmap(imgImage)
                                        objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                        Dim bytBLOBData As Byte() = ms.ToArray()
                                        Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                                        dr.Item("QrCode") = sIMGBASE64

                                    Next
                                End If
                                dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
                            Else
                                dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
                                If dt.Rows.Count() = 0 Then
                                    MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                                    Exit Function
                                Else
                                    For Each dr As DataRow In dt.Rows
                                        dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                        'Generate QRCode
                                        Dim imgImage As Image
                                        Dim objBitmap As Bitmap
                                        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                                        imgImage = objQRCode.Encode(Global_QRCode)
                                        objBitmap = New Bitmap(imgImage)
                                        objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                        Dim bytBLOBData As Byte() = ms.ToArray()
                                        Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                                        dr.Item("QrCode") = sIMGBASE64

                                    Next
                                End If
                                dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
                                dtPurchase = _ObjPurchaseController.GetAllPurchasePrint(_SalesInvoiceID)
                            End If
                            dtGem = objSalesInvoiceController.GetForSaleGem(_SalesInvoiceID)
                            dtOtherCash = objSalesInvoiceController.GetOtherCashDataByVoucherNo(_SalesInvoiceID)
                            dtVoucherTax = objSalesInvoiceController.GetSalesInvoiceTaxVoucher(_SalesInvoiceID)
                            If dtGem.Rows.Count > 0 Then
                                frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                frmPrint.WindowState = FormWindowState.Maximized
                                frmPrint.Show()
                            Else
                                If dtItem.Rows.Count = 1 Then
                                    frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                    frmPrint.WindowState = FormWindowState.Maximized
                                    frmPrint.Show()
                                Else
                                    frmPrint.SaleItemNamePrint(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                    frmPrint.WindowState = FormWindowState.Maximized
                                    frmPrint.Show()
                                End If

                            End If
                            Clear()
                        Else
                            Clear()
                            Return True
                        End If

                    Else
                        Clear()
                        Return True
                    End If
                Else
                    Return False
                End If
            ElseIf Global_IsUseMember = True Then ' use Member
                If CheckForInternetConnection() = True Then
                    If objSalesInvoiceController.SaveSalesInvoice(objSalesInvoice, _dtItemBarcode, _dtAllDiamond, _dtOtherCash, _dtItemData_His) Then
                        _SalesInvoiceID = objSalesInvoice.SaleInvoiceHeaderID

                        'Can be use For Oppurnity Type Amount,Discount only
                        If OppurtunityType <> "Item" Then
                            If RedeemID <> "" And btnSave.Text <> "Update" Then  'RedeemIsuse 1
                                If CheckForInternetConnection() = True Then
                                    UpdateRedeem(objSalesInvoice, 0)




                                End If


                            Else

                                If btnSave.Text <> "Update" Then
                                    If CheckForInternetConnection() = True Then
                                        AddRedeem(objSalesInvoice, 0)
                                    End If

                                Else
                                    If CheckForInternetConnection() = True Then
                                        AddRedeem(objSalesInvoice, 1)

                                    End If

                                End If
                            End If
                        End If

                        If IsMaximumPoint() = True Then
                            If btnSave.Text <> "Update" Then
                                SaveSaleMemberCard(objSalesInvoice, 0)
                            Else
                                SaveSaleMemberCard(objSalesInvoice, 1)
                            End If
                        End If
                        
                        If chkCancel.Checked = False Then
                            If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                                Dim frmPrint As New frm_ReportViewer
                                Dim dt As New DataTable
                                Dim dtItem As New DataTable
                                Dim dtGem As New DataTable
                                Dim dtPurchase As New DataTable
                                Dim dtVoucherTax As New DataTable
                                Dim _PurchaseHeaderID As String
                                Dim obj As New CommonInfo.SaleInvoiceHeaderInfo
                                Dim dtOtherCash As New DataTable

                                'QRCode
                                Dim objQRCode As QRCodeEncoder = New QRCodeEncoder()
                                objQRCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
                                objQRCode.QRCodeScale = 3
                                objQRCode.QRCodeVersion = 7
                                objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H

                                obj = objSalesInvoiceController.GetSaleInvoiceHeaderByID(_SalesInvoiceID)
                                _PurchaseHeaderID = obj.PurchaseHeaderID
                                If _PurchaseHeaderID = "" Then
                                    dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
                                    If dt.Rows.Count() = 0 Then
                                        MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                                        Exit Function
                                    Else
                                        For Each dr As DataRow In dt.Rows
                                            dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                            'Generate QRCode
                                            Dim imgImage As Image
                                            Dim objBitmap As Bitmap
                                            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                                            imgImage = objQRCode.Encode(Global_QRCode)
                                            objBitmap = New Bitmap(imgImage)
                                            objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                            Dim bytBLOBData As Byte() = ms.ToArray()
                                            Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                                            dr.Item("QrCode") = sIMGBASE64

                                        Next
                                    End If
                                    dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
                                Else
                                    dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
                                    If dt.Rows.Count() = 0 Then
                                        MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                                        Exit Function
                                    Else
                                        For Each dr As DataRow In dt.Rows
                                            dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                            'Generate QRCode
                                            Dim imgImage As Image
                                            Dim objBitmap As Bitmap
                                            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                                            imgImage = objQRCode.Encode(Global_QRCode)
                                            objBitmap = New Bitmap(imgImage)
                                            objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                            Dim bytBLOBData As Byte() = ms.ToArray()
                                            Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                                            dr.Item("QrCode") = sIMGBASE64

                                        Next
                                    End If
                                    dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
                                    dtPurchase = _ObjPurchaseController.GetAllPurchasePrint(_SalesInvoiceID)
                                End If
                                dtGem = objSalesInvoiceController.GetForSaleGem(_SalesInvoiceID)
                                dtOtherCash = objSalesInvoiceController.GetOtherCashDataByVoucherNo(_SalesInvoiceID)
                                dtVoucherTax = objSalesInvoiceController.GetSalesInvoiceTaxVoucher(_SalesInvoiceID)


                                UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _SalesInvoiceID)

                                dt.Rows(0).Item("PointBalance") = VoucherPointBalance
                                If dtGem.Rows.Count > 0 Then
                                    frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                    frmPrint.WindowState = FormWindowState.Maximized
                                    frmPrint.Show()
                                Else
                                    If dtItem.Rows.Count = 1 Then
                                        frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                        frmPrint.WindowState = FormWindowState.Maximized
                                        frmPrint.Show()
                                    Else
                                        frmPrint.SaleItemNamePrint(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                        frmPrint.WindowState = FormWindowState.Maximized
                                        frmPrint.Show()
                                    End If

                                End If
                                Clear()
                            Else

                                UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _SalesInvoiceID)

                                Clear()
                                Return True
                            End If

                        Else
                            Clear()
                            Return True
                        End If
                    End If

                Else 'Member သုံးပြီးလိုင်းမရဘူး Yes= လိုင်းမရပေမယ့်သိမ်းမယ်
                    If (MessageBox.Show("Point balance can't be update.Please Check internet Connection.Do you want to save voucher.", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)) = DialogResult.Yes Then
                        If objSalesInvoiceController.SaveSalesInvoice(objSalesInvoice, _dtItemBarcode, _dtAllDiamond, _dtOtherCash, _dtItemData_His) Then
                            _SalesInvoiceID = objSalesInvoice.SaleInvoiceHeaderID
                            If chkCancel.Checked = False Then
                                If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                                    Dim frmPrint As New frm_ReportViewer
                                    Dim dt As New DataTable
                                    Dim dtItem As New DataTable
                                    Dim dtGem As New DataTable
                                    Dim dtPurchase As New DataTable
                                    Dim dtVoucherTax As New DataTable
                                    Dim _PurchaseHeaderID As String
                                    Dim obj As New CommonInfo.SaleInvoiceHeaderInfo
                                    Dim dtOtherCash As New DataTable
                                    'QRCode
                                    Dim objQRCode As QRCodeEncoder = New QRCodeEncoder()
                                    objQRCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
                                    objQRCode.QRCodeScale = 3
                                    objQRCode.QRCodeVersion = 7
                                    objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H

                                    obj = objSalesInvoiceController.GetSaleInvoiceHeaderByID(_SalesInvoiceID)
                                    _PurchaseHeaderID = obj.PurchaseHeaderID
                                    If _PurchaseHeaderID = "" Then
                                        dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
                                        If dt.Rows.Count() = 0 Then
                                            MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                                            Exit Function
                                        Else
                                            For Each dr As DataRow In dt.Rows
                                                dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                                'Generate QRCode
                                                Dim imgImage As Image
                                                Dim objBitmap As Bitmap
                                                Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                                                imgImage = objQRCode.Encode(Global_QRCode)
                                                objBitmap = New Bitmap(imgImage)
                                                objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                                Dim bytBLOBData As Byte() = ms.ToArray()
                                                Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                                                dr.Item("QrCode") = sIMGBASE64

                                            Next
                                        End If
                                        dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
                                    Else
                                        dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
                                        If dt.Rows.Count() = 0 Then
                                            MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                                            Exit Function
                                        Else
                                            For Each dr As DataRow In dt.Rows
                                                dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                                                'Generate QRCode
                                                Dim imgImage As Image
                                                Dim objBitmap As Bitmap
                                                Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                                                imgImage = objQRCode.Encode(Global_QRCode)
                                                objBitmap = New Bitmap(imgImage)
                                                objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                                Dim bytBLOBData As Byte() = ms.ToArray()
                                                Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                                                dr.Item("QrCode") = sIMGBASE64

                                            Next
                                        End If
                                        dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
                                        dtPurchase = _ObjPurchaseController.GetAllPurchasePrint(_SalesInvoiceID)
                                    End If
                                    dtGem = objSalesInvoiceController.GetForSaleGem(_SalesInvoiceID)
                                    dtOtherCash = objSalesInvoiceController.GetOtherCashDataByVoucherNo(_SalesInvoiceID)
                                    dtVoucherTax = objSalesInvoiceController.GetSalesInvoiceTaxVoucher(_SalesInvoiceID)
                                    If dtGem.Rows.Count > 0 Then
                                        frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                        frmPrint.WindowState = FormWindowState.Maximized
                                        frmPrint.Show()
                                    Else
                                        If dtItem.Rows.Count = 1 Then
                                            frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                            frmPrint.WindowState = FormWindowState.Maximized
                                            frmPrint.Show()
                                        Else
                                            frmPrint.SaleItemNamePrint(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                                            frmPrint.WindowState = FormWindowState.Maximized
                                            frmPrint.Show()
                                        End If

                                    End If
                                    Clear()
                                Else
                                    Clear()
                                    Return True
                                End If

                            Else
                                Clear()
                                Return True
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                End If
            End If
                End If
    End Function
    Public Sub UpdateRedeemAndTransID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)
        If _IsMaximum = True Then
            GetTransactionID(MCode, Global_CompanyName, Token, _SalesInvoiceID)
        End If
        GetRedeemID(MCode, Global_CompanyName, Token, _SalesInvoiceID)

    End Sub
    Public Async Sub GetTransactionID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)

        'dtTransactionID = Await WebService.MemberCard.GetTransactionIDByMemberCode(MCode, Global_CompanyName, Token, InvoiceID)

        If (dtTransactionID.Rows(0).Item("TransactionID") <> "") Then

            Try
                _TransactionID = dtTransactionID.Rows(0).Item("TransactionID")

                TransBool = objSalesInvoiceController.UpdateTransactionID(_TransactionID, InvoiceID)
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
    Private Function GetDataSalesInvoice() As SaleInvoiceHeaderInfo
        GetDataSalesInvoice = New SaleInvoiceHeaderInfo
        With GetDataSalesInvoice
            .SaleInvoiceHeaderID = _SalesInvoiceID
            .StaffID = cboStaff.SelectedValue
            .SaleDate = dtpSaleDate.Value
            .CustomerID = _CustomerID
            .TotalAmount = txtAllTotalAmt.Text
            .AddOrSub = IIf(txtAllAddOrSub.Text = "", 0, txtAllAddOrSub.Text)
            .DiscountAmount = txtDiscountAmt.Text
            .PaidAmount = IIf(txtPaidAmt.Text = "" Or txtPaidAmt.Text = "-", 0, txtPaidAmt.Text)
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .PromotionDiscount = IIf(txtPromotionDis.Text = "", 0, txtPromotionDis.Text)
            .PurchaseHeaderID = _PurchaseHeaderID
            .PurchaseAmount = IIf(txtPurchaseAmount.Text = "", 0, txtPurchaseAmount.Text)
            .IsAdvance = chkAdvance.Checked
            .EntryAdvanceDate = dtpAdvanceDate.Value
            .AllAdvanceAmount = IIf(txtAdvanceAmt.Text = "", 0, txtAdvanceAmt.Text)
            .IsCancel = chkCancel.Checked
            .CancelDate = dtpCancelDate.Value
            .IsOtherCash = chkOtherCash.Checked
            .OtherCashAmount = IIf(txtOtherCashAmount.Text = "", 0, txtOtherCashAmount.Text)
            .AllTaxAmt = CInt(txtAllTaxAmt.Text)
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
    Private Function IsFillData() As Boolean
        If cboStaff.SelectedIndex = -1 Then
            MsgBox("Select Staff !", MsgBoxStyle.Information, AppName)
            cboStaff.Focus()
            Return False
        End If

        If _CustomerID = "" Then
            MsgBox("Please Select Valid Customer !", MsgBoxStyle.Information, AppName)
            txtCustomerCode.Focus()
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

        If chkAdvance.Checked And Val(txtAdvanceAmt.Text) = 0 Then
            MsgBox("Please Fill AdvanceAmount!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        If chkAdvance.Checked And Val(txtPaidAmt.Text) > 0 And chkCancel.Checked = False Then
            If MsgBox("Are you sure you want to save this Voucher with this Sale Date?", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.No Then
                Return False
                dtpSaleDate.Focus()
            End If
        End If

        If chkOtherCash.Checked And (txtOtherCashAmount.Text = "0" Or txtOtherCashAmount.Text = "") Then
            MsgBox("Please Check OtherCash!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        If (txtRemark.Text.Length > 255) Then
            MsgBox("Please Check Remark is too long!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        If _SalesInvoiceID <> "0" Then
            For Each dr As DataRow In _dtItemBarcode.Rows
                Dim dtsale As New DataTable
                If dr.RowState = DataRowState.Deleted Then
                    dtsale = objGeneralController.CheckExitVoucherForCashReceipt("tbl_PurchaseDetail", "AND SaleInvoiceDetailID='" + CStr(dr.Item("SaleInvoiceDetailID", DataRowVersion.Original)) + "' AND IsShop=1 AND IsOrder=0")
                    If dtsale.Rows.Count() > 0 Then
                        MsgBox("Can not delete row because of Use in SaleReturn!", MsgBoxStyle.Information, "")
                        Return False
                    End If


                Else
                    If CBool(dr.Item("IsSaleReturn")) = False Then
                        Dim dtSR As New DataTable
                        dtSR = objGeneralController.CheckExitVoucherForCashReceipt("tbl_SaleInvoiceDetail SD inner join tbl_ForSale F ON SD.ForSaleID=F.ForSaleID", "AND SD.SaleInvoiceHeaderID<>'" + _SalesInvoiceID + "' AND IsExit=1")
                        If dtsale.Rows.Count() > 0 Then
                            MsgBox("Can not Sale because of using other voucher!", MsgBoxStyle.Information, "")
                            Return False
                        End If
                    End If
                End If
            Next
        End If


        Return True
    End Function

    Private Sub ClearItemCode()

        'If Global_UserLevel = "Administrator" Then
        '    LnkTotalNoWaste.Enabled = True
        'Else
        '    LnkTotalNoWaste.Enabled = False
        'End If
        txtCurrentPrice.ReadOnly = False
        txtCurrentPrice.BackColor = Color.White
        txtBarcodeNo.ReadOnly = False
        _ForSaleID = ""
        _BarcodeNo = ""
        _IsSaleReturn = 0

        _SalesInvoiceDetailID = ""
        txtCurrentPrice.Text = 0
        txtItemCategory.Text = ""
        txtItemName.Text = ""
        txtGoldQuality.Text = ""
        txtColor.Text = ""
        txtLength.Text = ""
        lblPercent.Text = ""

        txtTaxPer.Text = "0"
        txtTaxAmt.Text = "0"
        txtGemTax.Text = "0"
        txtAllTaxAmt.Text = "0"

        _IsGram = False

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

        txtGoldK.Text = "0"
        txtGoldP.Text = "0"
        txtGoldY.Text = "0.0"
        txtGoldTK.Text = "0.0"
        txtGoldTG.Text = "0.0"

        txtGemKForSale.Text = "0"
        txtGemPForSale.Text = "0"
        txtGemYForSale.Text = "0.00"
        txtGemTGForSale.Text = "0.0"
        txtGemTKForSale.Text = "0.0"

        _ItemTG = 0
        _ItemTK = 0

        _WasteTG = 0
        _WasteTK = 0

        _GemsTG = 0
        _GemsTK = 0

        _GoldTK = 0
        _GoldTG = 0
        lblItemGram.Text = ""

        txtWhiteCharges.Text = "0"
        txtPlatingCharges.Text = "0"
        txtMountingCharges.Text = "0"
        txtDesignCharges.Text = "0"
        txtDesignRate.Text = "0"


        chkIsFixPrice.Checked = False

        txtGoldPrice.Text = "0"
        txtGemsPrice.Text = "0"
        txtTotalAmt.Text = "0"
        txtNetAmt.Text = ""
        txtTaxPer.Text = "0"
        txtTaxAmt.Text = "0"
        txtAllTaxAmt.Text = "0"
        txtSellingRate.Text = "0"
        txtSellingAmt.Text = "0"

        txtAddSub.Text = 0
        btnAdd.Text = "Add"
        If Global_LogoPhoto <> "" Then
            If Directory.Exists(Global_PhotoPath) Then
                Try
                    lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                    PName = Global_LogoPhoto
                    lblPhoto.Visible = False
                Catch ex As Exception
                    lblItemImage.Image = Nothing
                    lblPhoto.Visible = True
                    PName = ""
                End Try
            End If
        Else

            lblItemImage.Image = Nothing
            lblPhoto.Visible = True
            PName = ""
        End If

        lblOriginalCode.Text = ""
        lblPriceCode.Text = ""
        chkIsDiamond.Checked = False

        _IsUpdate = False
        isFixPrice = False
        _FixPrice = 0
        _IsGemInDB = False

        Dim dc As DataColumn

        _dtSalesInvoiceItem = New DataTable
        _dtSalesInvoiceItem.Columns.Add("SalesInvoiceGemItemID", System.Type.GetType("System.String"))
        _dtSalesInvoiceItem.Columns.Add("@SaleInvoiceDetailID", System.Type.GetType("System.String"))
        _dtSalesInvoiceItem.Columns.Add("@GemsCategoryID", System.Type.GetType("System.String"))
        _dtSalesInvoiceItem.Columns.Add("GemsCategory", System.Type.GetType("System.String"))
        _dtSalesInvoiceItem.Columns.Add("GemsName", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "GemsK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtSalesInvoiceItem.Columns.Add(dc)



        dc = New DataColumn
        dc.ColumnName = "GemsP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtSalesInvoiceItem.Columns.Add(dc)


        dc = New DataColumn
        dc.ColumnName = "GemsY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0
        _dtSalesInvoiceItem.Columns.Add(dc)


        dc = New DataColumn
        dc.ColumnName = "GemsC"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0
        _dtSalesInvoiceItem.Columns.Add(dc)

        _dtSalesInvoiceItem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtSalesInvoiceItem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtSalesInvoiceItem.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))
        _dtSalesInvoiceItem.Columns.Add("GemsTW", System.Type.GetType("System.Decimal"))
        _dtSalesInvoiceItem.Columns.Add("Qty", System.Type.GetType("System.Int16"))
        _dtSalesInvoiceItem.Columns.Add("UnitPrice", System.Type.GetType("System.Int64"))
        _dtSalesInvoiceItem.Columns.Add("Type", System.Type.GetType("System.String"))
        _dtSalesInvoiceItem.Columns.Add("GemTaxPer", System.Type.GetType("System.Decimal"))
        _dtSalesInvoiceItem.Columns.Add("GemTax", System.Type.GetType("System.Int16"))

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtSalesInvoiceItem.Columns.Add(dc)
        _dtSalesInvoiceItem.Columns.Add("GemsRemark", System.Type.GetType("System.String"))

        grdSaleCategory.AutoGenerateColumns = False
        grdSaleCategory.ReadOnly = False
        grdSaleCategory.DataSource = _dtSalesInvoiceItem
        FormatSalesInvoiceItem()
    End Sub
    Private Sub SaleItemInvoiceGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.SaleStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtSaleInvoiceHeaderID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpSaleDate.Value)
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
        End If
    End Sub

    Public Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _SalesInvoiceID = "0"
        SaleItemInvoiceGenerateFormat()
        _LocationID = ""
        dtpSaleDate.Value = Now
        'cboStaff.SelectedIndex = -1
        _PurchaseHeaderID = ""
        txtPurchaseVoucherNo.Text = ""
        _CustomerID = ""
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtRemark.Clear()
        MemDiscountAmount = 0
        DiscountPercent = 0
        txtMemberDis.Text = "0"
        txtMemberDisAmt.Text = "0"
        txtValue.Text = 0
        txtAllTotalAmt.Text = 0

        txtAllNetAmt.Text = 0
        txtPoint.Text = 0
        txtAllAddOrSub.Text = 0

        'txtSRTaxPer.Enabled = False
        'txtSRTaxAmt.Enabled = False
        'lblPer.Enabled = False 
        'lblSRTax.Enabled = False

        txtSRTaxPer.Text = ""
        txtSRTaxAmt.Text = 0

        txtTaxPer.Text = 0
        txtTaxAmt.Text = 0
        txtGemTax.Text = 0
        txtAllTaxAmt.Text = 0

        txtDiscountAmt.Text = 0
        txtPromotionAmt.Text = 0
        txtPromotionDis.Text = 0
        txtDifferentAmount.Text = 0
        txtPurchaseAmount.Text = 0
        If Global_UserLevel = "Administrator" Then
            txtPromotionDis.ReadOnly = False
            txtPromotionDis.BackColor = Color.White
        Else
            txtPromotionDis.ReadOnly = True
            txtPromotionDis.BackColor = Color.Linen
        End If
        chkAdvance.Checked = False
        dtpAdvanceDate.Value = Now
        dtpAdvanceDate.Visible = False
        txtAdvanceAmt.Text = "0"
        txtAdvanceAmt.Visible = False
        chkCancel.Checked = False
        chkCancel.Visible = False
        lblCancelDate.Visible = False
        dtpCancelDate.Value = Now
        dtpCancelDate.Visible = False
        _AllPaidAmount = 0
        _IsAllUpdate = False
        _IsAllowDiscount = False
        _IsCustomerName = False
        _IsCustomerCode = False
        txtTotalItemG.Text = "0.000"
        txtTotalItemK.Text = "0"
        txtTotalItemP.Text = "0"
        txtTotalItemY.Text = "0.0"
        txtTotalQTY.Text = "0"

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
        _IsMaximum = False

        Dim dc As New DataColumn
        _dtItemBarcode = New DataTable
        _dtItemBarcode.Columns.Add("SaleInvoiceDetailID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("SaleInvoiceHeaderID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("SNo", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("ForSaleID", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemCode", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemCategory", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("GoldQuality", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "Gram"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtItemBarcode.Columns.Add(dc)

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

        dc = New DataColumn
        dc.ColumnName = "GemsTK"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtItemBarcode.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsTG"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.0"
        _dtItemBarcode.Columns.Add(dc)

        _dtItemBarcode.Columns.Add("SalesRate", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("GoldPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("GemsPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsFixPrice", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("TotalAmount", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("AddOrSub", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("NetAmount", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsOriginalFixedPrice", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("OriginalFixedPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsOriginalPriceGram", System.Type.GetType("System.Boolean"))
        _dtItemBarcode.Columns.Add("OriginalPriceGram", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("OriginalPriceTK", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("OriginalGemsPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("OriginalOtherPrice", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("PurchaseWasteTK", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("PurchaseWasteTG", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("OriginalCode", System.Type.GetType("System.String"))
        _dtItemBarcode.Columns.Add("ItemTaxPer", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("ItemTax", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("WhiteCharges", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("PlatingCharges", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("MountingCharges", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("DesignCharges", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("IsSaleReturn", System.Type.GetType("System.Boolean"))

        _dtItemBarcode.Columns.Add("ItemK", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("ItemP", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("ItemY", System.Type.GetType("System.Decimal"))

        _dtItemBarcode.Columns.Add("WasteK", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("WasteP", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("WasteY", System.Type.GetType("System.Decimal"))
        _dtItemBarcode.Columns.Add("DesignChargesRate", System.Type.GetType("System.Int64"))
        _dtItemBarcode.Columns.Add("SellingRate", System.Type.GetType("System.Int32"))
        _dtItemBarcode.Columns.Add("SellingAmt", System.Type.GetType("System.Int64"))


        grdDetail.AutoGenerateColumns = False
        grdDetail.DataSource = _dtItemBarcode
        FormatGridItemDetail()

        _dtAllDiamond = New DataTable
        _dtAllDiamond.Columns.Add("SalesInvoiceGemItemID", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("@SaleInvoiceDetailID", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("@GemsCategoryID", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("GemsCategory", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("GemsName", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "GemsK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtAllDiamond.Columns.Add(dc)



        dc = New DataColumn
        dc.ColumnName = "GemsP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtAllDiamond.Columns.Add(dc)


        dc = New DataColumn
        dc.ColumnName = "GemsY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0
        _dtAllDiamond.Columns.Add(dc)


        dc = New DataColumn
        dc.ColumnName = "GemsC"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0
        _dtAllDiamond.Columns.Add(dc)

        _dtAllDiamond.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtAllDiamond.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtAllDiamond.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("GemsTW", System.Type.GetType("System.Decimal"))
        _dtAllDiamond.Columns.Add("Qty", System.Type.GetType("System.Int16"))
        _dtAllDiamond.Columns.Add("UnitPrice", System.Type.GetType("System.Int64"))
        _dtAllDiamond.Columns.Add("Type", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("GemTaxPer", System.Type.GetType("System.Decimal"))
        _dtAllDiamond.Columns.Add("GemTax", System.Type.GetType("System.Int64"))
        _dtAllDiamond.Columns.Add("SaleByDefinePrice", System.Type.GetType("System.Boolean"))


        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtAllDiamond.Columns.Add(dc)
        _dtAllDiamond.Columns.Add("GemsRemark", System.Type.GetType("System.String"))

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
        txtBarcodeNo.Text = ""
        ClearItemCode()
    End Sub

    Public Sub FormatGridItemDetail()

        With grdDetail
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "SaleInvoiceDetailID"
                .DataPropertyName = "SaleInvoiceDetailID"
                .Name = "SaleInvoiceDetailID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "SaleInvoiceHeaderID"
                .DataPropertyName = "SaleInvoiceHeaderID"
                .Name = "SaleInvoiceHeaderID"
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

            Dim dcSNO As New DataGridViewTextBoxColumn
            With dcSNO
                .HeaderText = "SNo"
                .DataPropertyName = "SNo"
                .Name = "SNo"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 30
                .Visible = True
                .ReadOnly = True
            End With
            .Columns.Add(dcSNO)

            Dim dcDia As New DataGridViewTextBoxColumn
            With dcDia
                .HeaderText = "ItemCode"
                .DataPropertyName = "ItemCode"
                .Name = "ItemCode"
                .Width = 100
                .Visible = True
                .ReadOnly = True
            End With
            .Columns.Add(dcDia)

            Dim dcGram As New DataGridViewTextBoxColumn
            With dcGram
                .HeaderText = "Gram"
                .DataPropertyName = "Gram"
                .Name = "Gram"
                .Width = 60
                .DefaultCellStyle.Format = "0.000"
                .Visible = True
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcGram)


            Dim dcCategory As New DataGridViewTextBoxColumn
            With dcCategory
                .HeaderText = "ItemCategory"
                .DataPropertyName = "ItemCategory"
                .Name = "ItemCategory"
                .Width = 150
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcCategory)

            Dim dcItemName As New DataGridViewTextBoxColumn
            With dcItemName
                .HeaderText = "ItemName"
                .DataPropertyName = "ItemName"
                .Name = "ItemName"
                .Width = 150
                .Visible = True
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcItemName)

            Dim dcGQ As New DataGridViewTextBoxColumn
            With dcGQ
                .HeaderText = "GoldQuality"
                .DataPropertyName = "GoldQuality"
                .Name = "GoldQuality"
                .Width = 130
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
            End With
            .Columns.Add(dcGQ)

            Dim dcItemTG As New DataGridViewTextBoxColumn
            With dcItemTG
                .HeaderText = "ItemTG"
                .DataPropertyName = "ItemTG"
                .Name = "ItemTG"
                .Width = 80
                ' .DefaultCellStyle.Format = "0.000"
                .Visible = False
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
                .Width = 80
                .Visible = False
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

            Dim dcGemTK As New DataGridViewTextBoxColumn
            With dcGemTK
                .HeaderText = "GemsTK"
                .DataPropertyName = "GemsTK"
                .Name = "GemsTK"
                .Visible = False
            End With
            .Columns.Add(dcGemTK)

            Dim dcGemTG As New DataGridViewTextBoxColumn
            With dcGemTG
                .HeaderText = "GemsTG"
                .DataPropertyName = "GemsTG"
                .Name = "GemsTG"
                .Visible = False
            End With
            .Columns.Add(dcGemTG)

            Dim dcCurrent As New DataGridViewTextBoxColumn
            With dcCurrent
                .HeaderText = "Rate"
                .DataPropertyName = "SalesRate"
                .Name = "SalesRate"
                .Width = 80
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .ReadOnly = True
            End With
            .Columns.Add(dcCurrent)

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

            Dim dcItemTaxPer As New DataGridViewTextBoxColumn()
            With dcItemTaxPer
                .HeaderText = "ItemTaxPer"
                .DataPropertyName = "ItemTaxPer"
                .Name = "ItemTaxPer"
                .Visible = False
                .DefaultCellStyle.Format = "0.00"
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcItemTaxPer)

            Dim dcItemTax As New DataGridViewTextBoxColumn()
            With dcItemTax
                .HeaderText = "ItemTax"
                .DataPropertyName = "ItemTax"
                .Name = "ItemTax"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcItemTax)



            Dim dcNetAmount As New DataGridViewTextBoxColumn()
            With dcNetAmount
                .HeaderText = "NetAmount"
                .DataPropertyName = "NetAmount"
                .Name = "NetAmount"
                .Width = 75
                .Visible = True
                .ReadOnly = True
                .DefaultCellStyle.Format = "###,##0.##"
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
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcAdd)

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

            Dim dcIsOriginalPriceGram As New DataGridViewTextBoxColumn()
            With dcIsOriginalPriceGram
                .HeaderText = "IsOriginalPriceGram"
                .DataPropertyName = "IsOriginalPriceGram"
                .Name = "IsOriginalPriceGram"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcIsOriginalPriceGram)

            Dim dcOriginalPriceGram As New DataGridViewTextBoxColumn()
            With dcOriginalPriceGram
                .HeaderText = "OriginalPriceGram"
                .DataPropertyName = "OriginalPriceGram"
                .Name = "OriginalPriceGram"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOriginalPriceGram)

            Dim dcOriginalPriceTK As New DataGridViewTextBoxColumn()
            With dcOriginalPriceTK
                .HeaderText = "OriginalPriceTK"
                .DataPropertyName = "OriginalPriceTK"
                .Name = "OriginalPriceTK"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOriginalPriceTK)

            Dim dcOriginalGemsPrice As New DataGridViewTextBoxColumn()
            With dcOriginalGemsPrice
                .HeaderText = "OriginalGemsPrice"
                .DataPropertyName = "OriginalGemsPrice"
                .Name = "OriginalGemsPrice"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOriginalGemsPrice)

            Dim dcOriginalOtherPrice As New DataGridViewTextBoxColumn()
            With dcOriginalOtherPrice
                .HeaderText = "OriginalOtherPrice"
                .DataPropertyName = "OriginalOtherPrice"
                .Name = "OriginalOtherPrice"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcOriginalOtherPrice)

            Dim dcPurchaseWasteTK As New DataGridViewTextBoxColumn()
            With dcPurchaseWasteTK
                .HeaderText = "PurchaseWasteTK"
                .DataPropertyName = "PurchaseWasteTK"
                .Name = "PurchaseWasteTK"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPurchaseWasteTK)

            Dim dcPurchaseWasteTG As New DataGridViewTextBoxColumn()
            With dcPurchaseWasteTG
                .HeaderText = "PurchaseWasteTG"
                .DataPropertyName = "PurchaseWasteTG"
                .Name = "PurchaseWasteTG"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPurchaseWasteTG)

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
                .HeaderText = "PriceCode"
                .DataPropertyName = "PriceCode"
                .Name = "PriceCode"
                .Visible = False
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

            Dim dcDesignCharges As New DataGridViewTextBoxColumn()
            With dcDesignCharges
                .HeaderText = "DesignCharges"
                .DataPropertyName = "DesignCharges"
                .Name = "DesignCharges"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDesignCharges)

            Dim dcchkIsReturn As New DataGridViewCheckBoxColumn()
            dcchkIsReturn.HeaderText = "IsSaleReturn"
            dcchkIsReturn.DataPropertyName = "IsSaleReturn"
            dcchkIsReturn.Name = "IsSaleReturn"
            dcchkIsReturn.Width = 90
            dcchkIsReturn.Visible = False
            dcchkIsReturn.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcchkIsReturn)


            Dim dcItemK As New DataGridViewTextBoxColumn
            With dcItemK
                .HeaderText = "ItemK"
                .DataPropertyName = "ItemK"
                .Name = "ItemK"
                .Visible = False
            End With
            .Columns.Add(dcItemK)

            Dim dcItemP As New DataGridViewTextBoxColumn
            With dcItemP
                .HeaderText = "ItemP"
                .DataPropertyName = "ItemP"
                .Name = "ItemP"
                .Visible = False
            End With
            .Columns.Add(dcItemP)

            Dim dcItemY As New DataGridViewTextBoxColumn
            With dcItemP
                .HeaderText = "ItemY"
                .DataPropertyName = "ItemY"
                .Name = "ItemY"
                .Visible = False
            End With
            .Columns.Add(dcItemY)

            Dim dcWasteK As New DataGridViewTextBoxColumn
            With dcItemK
                .HeaderText = "WasteK"
                .DataPropertyName = "WasteK"
                .Name = "WasteK"
                .Visible = False
            End With
            .Columns.Add(dcWasteK)

            Dim dcWasteP As New DataGridViewTextBoxColumn
            With dcWasteP
                .HeaderText = "WasteP"
                .DataPropertyName = "WasteP"
                .Name = "WasteP"
                .Visible = False
            End With
            .Columns.Add(dcWasteP)

            Dim dcWasteY As New DataGridViewTextBoxColumn
            With dcWasteY
                .HeaderText = "WasteY"
                .DataPropertyName = "WasteY"
                .Name = "WasteY"
                .Visible = False
            End With
            .Columns.Add(dcWasteY)

            Dim dcDesignChargesRate As New DataGridViewTextBoxColumn()
            With dcDesignChargesRate
                .HeaderText = "DesignChargesRate"
                .DataPropertyName = "DesignChargesRate"
                .Name = "DesignChargesRate"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDesignChargesRate)

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
#Region "FormatItemGrid"

    Private Sub FormatSalesInvoiceItem()
        With grdSaleCategory
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40
            .DefaultCellStyle.Font = New Font("Tahoma", 8.25)

            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "SalesInvoiceGemItemID"
            dclID.DataPropertyName = "SalesInvoiceGemItemID"
            dclID.Name = "SalesInvoiceGemItemID"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcPID As New DataGridViewTextBoxColumn()
            dcPID.HeaderText = "SaleInvoiceDetailID"
            dcPID.DataPropertyName = "SaleInvoiceDetailID"
            dcPID.Name = "SaleInvoiceDetailID"
            dcPID.Visible = False
            .Columns.Add(dcPID)

            Dim dcID As New DataGridViewComboBoxColumn()
            dcID.HeaderText = "Category"
            dcID.DataPropertyName = "@GemsCategoryID"
            dcID.Name = "@GemsCategoryID"
            dcID.DataSource = objGemsCategoryController.GetAllGemsCategoryForGridCombo()
            dcID.DisplayMember = "GemsCategory"
            dcID.ValueMember = "@GemsCategoryID"
            dcID.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcID.Visible = False
            dcID.Width = 150
            dcID.ReadOnly = True
            .Columns.Add(dcID)

            Dim dcCat As New DataGridViewTextBoxColumn()
            dcCat.HeaderText = "Category"
            dcCat.DataPropertyName = "GemsCategory"
            dcCat.Name = "GemsCategory"
            dcCat.Width = 150
            dcCat.Visible = True
            dcCat.ReadOnly = True
            dcCat.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcCat.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcCat)

            Dim dcName As New DataGridViewTextBoxColumn()
            dcName.HeaderText = "Description"
            dcName.DataPropertyName = "GemsName"
            dcName.Name = "GemsName"
            dcName.Width = 150
            dcName.Visible = True
            dcName.ReadOnly = True
            dcName.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcName.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcName)

            Dim dc5 As New DataGridViewTextBoxColumn()
            dc5.HeaderText = "RBP"
            dc5.DataPropertyName = "YOrCOrG"
            dc5.Name = "YOrCOrG"
            dc5.Width = 50
            dc5.Visible = True
            dc5.ReadOnly = True
            dc5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc5.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc5)

            Dim dc As New DataGridViewTextBoxColumn()
            dc.HeaderText = "K"
            dc.DataPropertyName = "GemsK"
            dc.Name = "GemsK"
            dc.Width = 30
            dc.Visible = True
            dc.ReadOnly = True
            dc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc)


            Dim dc2 As New DataGridViewTextBoxColumn()
            dc2.HeaderText = "P"
            dc2.DataPropertyName = "GemsP"
            dc2.Name = "GemsP"
            dc2.Width = 30
            dc2.Visible = True
            dc2.ReadOnly = True
            dc2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc2.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc2)

            Dim dc3 As New DataGridViewTextBoxColumn()
            dc3.HeaderText = "Y"
            dc3.DataPropertyName = "GemsY"
            dc3.Name = "GemsY"
            dc3.Width = 30
            If numberformat = 1 Then
                dc3.DefaultCellStyle.Format = "0.0"
            Else
                dc3.DefaultCellStyle.Format = "0.00"
            End If
            dc3.Visible = True
            dc3.ReadOnly = True
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

            Dim dcTG As New DataGridViewTextBoxColumn()
            dcTG.HeaderText = "GemsTG"
            dcTG.DataPropertyName = "GemsTG"
            dcTG.Name = "GemsTG"
            dcTG.Visible = False
            dcTG.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcTG)


            Dim dc10 As New DataGridViewTextBoxColumn()
            dc10.HeaderText = "GemsTW"
            dc10.DataPropertyName = "GemsTW"
            dc10.Name = "GemsTW"
            dc10.Visible = False
            dc10.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc10)

            Dim dc6 As New DataGridViewTextBoxColumn()
            dc6.HeaderText = "QTY"
            dc6.DataPropertyName = "Qty"
            dc6.Name = "Qty"
            dc6.Width = 50
            dc6.Visible = True
            dc6.ReadOnly = True
            dc6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc6.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc6)

            Dim dc7 As New DataGridViewTextBoxColumn()
            dc7.HeaderText = "Type"
            dc7.DataPropertyName = "Type"
            dc7.Name = "Type"
            dc7.Visible = True
            dc7.ReadOnly = True
            dc7.Width = 80
            .Columns.Add(dc7)

            Dim dc8 As New DataGridViewTextBoxColumn()
            dc8.HeaderText = "UnitPrice"
            dc8.DataPropertyName = "UnitPrice"
            dc8.Name = "UnitPrice"
            dc8.Width = 80
            dc8.Visible = True
            dc8.ReadOnly = False
            dc8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc8.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc8)

            Dim dcGemTaxPer As New DataGridViewTextBoxColumn()
            dcGemTaxPer.HeaderText = "Tax(%)"
            dcGemTaxPer.DataPropertyName = "GemTaxPer"
            dcGemTaxPer.Name = "GemTaxPer"
            dcGemTaxPer.Visible = True
            dcGemTaxPer.ReadOnly = False
            dcGemTaxPer.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dcGemTaxPer.Width = 80
            dcGemTaxPer.DefaultCellStyle.Format = "0.0"
            dcGemTaxPer.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcGemTaxPer)

            Dim dcAllTaxAmt As New DataGridViewTextBoxColumn()
            dcAllTaxAmt.HeaderText = "GemTax"
            dcAllTaxAmt.DataPropertyName = "GemTax"
            dcAllTaxAmt.Name = "GemTax"
            dcAllTaxAmt.Visible = True
            dcAllTaxAmt.ReadOnly = False
            dcAllTaxAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dcAllTaxAmt.Width = 80
            dcAllTaxAmt.DefaultCellStyle.Format = "###,##0"
            dcAllTaxAmt.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcAllTaxAmt)



            Dim dc9 As New DataGridViewTextBoxColumn()
            dc9.HeaderText = "Amount"
            dc9.DataPropertyName = "Amount"
            dc9.Name = "Amount"
            dc9.Visible = True
            dc9.ReadOnly = True
            dc9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dc9.Width = 80
            dc9.DefaultCellStyle.Format = "###,##0"
            dc9.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc9)

            Dim dcGR As New DataGridViewTextBoxColumn()
            With dcGR
                .HeaderText = "GemsRemark"
                .DataPropertyName = "GemsRemark"
                .Name = "GemsRemark"
                .Width = 100
                .Visible = False
                .ReadOnly = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGR)

            Dim dcSaleType As New DataGridViewTextBoxColumn()
            With dcSaleType
                .HeaderText = "SaleByDefinePrice"
                .DataPropertyName = "SaleByDefinePrice"
                .Name = "SaleByDefinePrice"
                .Width = 100
                .Visible = False
                .ReadOnly = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcSaleType)


        End With
    End Sub
#End Region

    Private Sub CalculateTotalAmount()

        If txtGoldPrice.Text = "" Then txtGoldPrice.Text = "0"
        If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
        If txtSellingAmt.Text = "" Then txtSellingAmt.Text = "0"

        If isFixPrice = False Then
            txtTotalAmt.Text = Format(CLng(txtGoldPrice.Text) + CInt(txtGemsPrice.Text) + CInt(txtSellingAmt.Text), "###,##0.##")
        Else
            txtTotalAmt.Text = Format(CLng(_FixPrice) + CInt(txtSellingAmt.Text), "###,##0.##")
        End If

        txtNetAmt.Text = Format(CLng(txtTotalAmt.Text), "###,##0.##")
        ' txtAddSub.Text = "0"
        txtAddSub.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtTotalAmt.Text)), "###,##0.##")
    End Sub

    Private Sub CalculategrdTotalAmount()
        Dim grdtotalAmt As Integer = 0.0
        Dim grdGemTax As Integer = 0.0

        If chkIsFixPrice.Checked = False Then
            For i As Integer = 0 To grdSaleCategory.RowCount - 1
                If Not grdSaleCategory.Rows(i).IsNewRow Then
                    grdtotalAmt += CInt(grdSaleCategory.Rows(i).Cells("Amount").FormattedValue)
                    grdGemTax += CInt(grdSaleCategory.Rows(i).Cells("GemTax").FormattedValue)
                End If
            Next
            txtGemsPrice.Text = Format(Val(grdtotalAmt), "###,##0.##")
            txtGemTax.Text = Format(Val(grdGemTax), "###,##0.##")
        Else
            txtGemsPrice.Text = "0"
            txtGemTax.Text = "0"
        End If
    End Sub

    Private Sub SearchSale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchSale.Click

        txtSRTaxPer.Enabled = True
        txtSRTaxAmt.Enabled = True
        lblPer.Enabled = True
        lblSRTax.Enabled = True

        btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objSaleItemHeader As New SaleInvoiceHeaderInfo


        dt = objSalesInvoiceController.GetAllSalesInvoice()
        DataItem = DirectCast(SearchData.FindFast(dt, "Sale Invoice Item List"), DataRow)

        If DataItem IsNot Nothing Then
            _IsGemInDB = True
            _IsUpdateHeader = True
            _SalesInvoiceID = DataItem.Item("VoucherNo").ToString()
            'InvoiceStatus = DataItem.Item("InvoiceStatus")
            'IsRedeemInvoice = DataItem.Item("IsRedeemInvoice")
            objSaleItemHeader = objSalesInvoiceController.GetSaleInvoiceHeaderByID(_SalesInvoiceID)

            _dtItemBarcode.Rows.Clear()
            _dtSalesInvoiceItem.Rows.Clear()
            _dtAllDiamond.Rows.Clear()

            _dtItemBarcode = objSalesInvoiceController.GetSalesInvoiceDetailByID(_SalesInvoiceID)
            grdDetail.DataSource = _dtItemBarcode

            grdDetail.Columns("IsSaleReturn").Visible = True

            _dtAllDiamond = objSalesInvoiceController.GetSaleInvoiceDetailGemByHeaderID(_SalesInvoiceID)
            Dim dtTestStone1 As New DataTable
            dtTestStone1 = _dtAllDiamond
            CalculategrAlldTotalWeight()
            _dtOtherCash = objSalesInvoiceController.GetOtherCashDataByVoucherNo(_SalesInvoiceID)
            ShowSaleDiaBarcodeData(objSaleItemHeader)
            btnDelete.Enabled = True
            If Global_CurrentUser = "Administrator" Then
                dtpSaleDate.Enabled = True
                btnSave.Enabled = True
                btnDelete.Enabled = True
            Else
                If Global_IsAllowSale Then
                    dtpSaleDate.Enabled = True
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                Else
                    dtpSaleDate.Enabled = False
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                End If
            End If


        Else
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString
        End If
    End Sub
    Private Sub ShowSaleDiaBarcodeData(ByVal objSaleHeader As SaleInvoiceHeaderInfo)
        With objSaleHeader
            dtpSaleDate.Value = .SaleDate
            txtSaleInvoiceHeaderID.Text = .SaleInvoiceHeaderID
            cboStaff.SelectedValue = .StaffID
            _CustomerID = .CustomerID
            txtCustomerCode.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerCode
            txtCustomer.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerName
            txtAddress.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerAddress

            chkAdvance.Checked = .IsAdvance
            If chkAdvance.Checked Then
                dtpAdvanceDate.Visible = True
                txtAdvanceAmt.Visible = True
                chkCancel.Visible = True
                lblCancelDate.Visible = True
                dtpCancelDate.Visible = True
                dtpAdvanceDate.Value = .EntryAdvanceDate
                txtAdvanceAmt.Text = Format(Val(.AllAdvanceAmount), "###,##0.##")
            End If

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

            txtAllTaxAmt.Text = Format(Val(.AllTaxAmt), "###,##0.##")
            txtAllTotalAmt.Text = Format(Val(.TotalAmount), "###,##0.##")
            txtAllAddOrSub.Text = Format(Val(.AddOrSub), "###,##0.##")
            txtPromotionDis.Text = .PromotionDiscount
            txtPromotionAmt.Text = Format(Val(CLng(txtAllTotalAmt.Text) * (CLng(txtPromotionDis.Text) / 100)), "###,##0.##")
            _IsAllowDiscount = True
            txtMemberDis.Text = .MemberDis
            txtMemberDisAmt.Text = .MemberDiscountAmt

            txtDiscountAmt.Text = Format(Val(.DiscountAmount), "###,##0.##")
            txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
            _PurchaseHeaderID = .PurchaseHeaderID
            txtPurchaseVoucherNo.Text = .PurchaseHeaderID
            txtPurchaseAmount.Text = Format(Val(.PurchaseAmount), "###,##0.##")


            chkOtherCash.Checked = .IsOtherCash
            txtSRTaxPer.Text = .SRTaxPer
            txtSRTaxAmt.Text = Format(Val(.SRTaxAmt), "###,##0.##")
            txtPaidAmt.Text = Format(Val(.PaidAmount), "###,##0.##")
            _AllPaidAmount = .PaidAmount

            'If chkOtherCash.Checked Then
            '    btnOtherCash.Visible = True
            'Else
            '    btnOtherCash.Visible = False
            'End If
            'txtOtherCashAmount.Text = Format(Val(.OtherCashAmount), "###,##0.##")

            If _PurchaseHeaderID <> "" Then
                txtSRTaxPer.Enabled = True
                txtSRTaxAmt.Enabled = True
                lblPer.Enabled = True
                lblSRTax.Enabled = True
                txtDifferentAmount.Text = Format(Val(CLng(txtAllNetAmt.Text) - CLng(txtPurchaseAmount.Text)), "###,##0.##")
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtAdvanceAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            Else
                txtDifferentAmount.Text = "0"
                txtPurchaseAmount.Text = "0"
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtAdvanceAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If

            'txtDiscountAmt.Text = Format(Val(.DiscountAmount), "###,##0.##")
            'txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text))), "###,##0.##")
            '_PurchaseHeaderID = .PurchaseHeaderID
            'txtPurchaseVoucherNo.Text = .PurchaseHeaderID
            'txtPurchaseAmount.Text = Format(Val(.PurchaseAmount), "###,##0.##")

            'txtPaidAmt.Text = Format(Val(.PaidAmount) - Val(.TotalAmount), "###,##0.##")
            '_AllPaidAmount = .PaidAmount
            'chkOtherCash.Checked = .IsOtherCash
            'txtSRTaxPer.Text = .SRTaxPer
            'txtSRTaxAmt.Text = Format(Val(.SRTaxAmt), "###,##0.##")

            If chkOtherCash.Checked Then
                btnOtherCash.Visible = True
            Else
                btnOtherCash.Visible = False
            End If
            txtOtherCashAmount.Text = Format(Val(.OtherCashAmount), "###,##0.##")
            chkCancel.Checked = .IsCancel
            If chkCancel.Checked Then
                dtpCancelDate.Value = .CancelDate
            End If
            txtRemark.Text = .Remark
            _IsAllUpdate = True
            _IsAllowDiscount = False

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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim dtItem As New DataTable
        Dim dtGem As New DataTable
        Dim dtOtherCash As New DataTable
        Dim dtPurchase As New DataTable
        Dim dtVoucherTax As New DataTable

        'QRCode
        Dim objQRCode As QRCodeEncoder = New QRCodeEncoder()
        objQRCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        objQRCode.QRCodeScale = 3
        objQRCode.QRCodeVersion = 7
        objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H

        Dim _PurchaseHeaderID As String
        Dim obj As New CommonInfo.SaleInvoiceHeaderInfo

        obj = objSalesInvoiceController.GetSaleInvoiceHeaderByID(_SalesInvoiceID)
        _PurchaseHeaderID = obj.PurchaseHeaderID

        If _PurchaseHeaderID = "" Then

            dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
            If dt.Rows.Count() = 0 Then
                MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                    'Generate QRCode
                    Dim imgImage As Image
                    Dim objBitmap As Bitmap
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                    imgImage = objQRCode.Encode(Global_QRCode)
                    objBitmap = New Bitmap(imgImage)
                    objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    Dim bytBLOBData As Byte() = ms.ToArray()
                    Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                    dr.Item("QrCode") = sIMGBASE64

                Next

            End If
            dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
        Else
            dt = objSalesInvoiceController.GetSalesInvoicePrint(_SalesInvoiceID)
            If dt.Rows.Count() = 0 Then
                MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
                Exit Sub

            Else
                For Each dr As DataRow In dt.Rows
                    dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
                    'Generate QRCode
                    Dim imgImage As Image
                    Dim objBitmap As Bitmap
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

                    imgImage = objQRCode.Encode(Global_QRCode)
                    objBitmap = New Bitmap(imgImage)
                    objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    Dim bytBLOBData As Byte() = ms.ToArray()
                    Dim sIMGBASE64 As String = Convert.ToBase64String(bytBLOBData)
                    dr.Item("QrCode") = sIMGBASE64

                Next
            End If
            dtItem = objSalesInvoiceController.GetSalesInvoiceDetailPrintByID(_SalesInvoiceID)
            dtPurchase = _ObjPurchaseController.GetAllPurchasePrint(_SalesInvoiceID)
        End If

        dtGem = objSalesInvoiceController.GetForSaleGem(_SalesInvoiceID)
        dtOtherCash = objSalesInvoiceController.GetOtherCashDataByVoucherNo(_SalesInvoiceID)
        dtVoucherTax = objSalesInvoiceController.GetSalesInvoiceTaxVoucher(_SalesInvoiceID)


        If dtGem.Rows.Count > 0 Then
            dt.Rows(0).Item("PointBalance") = VoucherPointBalance
            frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
            frmPrint.WindowState = FormWindowState.Maximized
            frmPrint.Show()
        Else
            'frmPrint.SaleItemNamePrint(dt, dtItem, dtPurchase, dtOtherCash, chkPhoto.Checked)
            'frmPrint.WindowState = FormWindowState.Maximized
            'frmPrint.Show()
            dt.Rows(0).Item("PointBalance") = VoucherPointBalance
            If dtItem.Rows.Count = 1 Then
                frmPrint.PrintD(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                frmPrint.WindowState = FormWindowState.Maximized
                frmPrint.Show()
            Else
                frmPrint.SaleItemNamePrint(dt, dtItem, dtPurchase, dtOtherCash, dtVoucherTax, chkPhoto.Checked)
                frmPrint.WindowState = FormWindowState.Maximized
                frmPrint.Show()
            End If
        End If
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
        MyBase.ValidateNumericAllowMinus(sender, e, True)
    End Sub

    Private Sub txtDiscountAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscountAmt.TextChanged
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtSRTaxAmt.Text = "" Then txtSRTaxAmt.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"
        If IsAllowAddOrSub() = False Then
            txtDiscountAmt.Text = "0"
        End If

        Dim DiscountAmount As Integer
        If txtDiscountAmt.Text = "-" Then
            DiscountAmount = 0
        Else
            DiscountAmount = CLng(txtDiscountAmt.Text)
        End If

        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If Global_IsCash = False Then
            txtPaidAmt.Text = CLng(txtPaidAmt.Text)

        End If

        txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (DiscountAmount + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
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

    Private Sub CalculateFinalAmount()
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtPurchaseAmount.Text = "" Then txtPurchaseAmount.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
        If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"
        'If txtSRTaxAmt.Text = "" Then txtSRTaxAmt.Text = "0"

        If _PurchaseHeaderID <> "" Then
            txtDifferentAmount.Text = Format(Val(CLng(txtAllNetAmt.Text) - CLng(txtPurchaseAmount.Text)), "###,##0.##")
            txtSRTaxAmt.Text = Format(CInt(CInt(txtDifferentAmount.Text) * CDec(txtSRTaxPer.Text) / 100), "###,##0.##")

            If chkAdvance.Checked Then
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
            Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If
        Else
            txtSRTaxAmt.Text = Format(CInt(CInt(txtAllNetAmt.Text) * CDec(txtSRTaxPer.Text) / 100), "###,##0.##")

            If chkAdvance.Checked Then
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
            Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If
        End If
    End Sub

    Private Sub txtPaidAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPaidAmt.KeyPress
        MyBase.ValidateNumericAllowMinus(sender, e, True)
    End Sub

    Private Sub txtPaidAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaidAmt.TextChanged
        If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtAdvanceAmt.Text = "" Then txtAdvanceAmt.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
        If txtBalanceAmt.Text = "" Then txtBalanceAmt.Text = "0"
        If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"

        Dim PaidAmount As Integer
        If txtPaidAmt.Text = "-" Then
            PaidAmount = 0
        Else
            PaidAmount = CLng(txtPaidAmt.Text)
        End If
        If chkAdvance.Checked Then
            If _PurchaseHeaderID <> "" Then
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + +CLng(txtSRTaxAmt.Text)) - (PaidAmount + CLng(txtAdvanceAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            Else
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CLng(txtSRTaxAmt.Text) - (PaidAmount + CLng(txtAdvanceAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If
        Else
            If _PurchaseHeaderID <> "" Then
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (PaidAmount + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            Else
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CLng(txtSRTaxAmt.Text) - (PaidAmount + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If
        End If
        ' CalculateFinalAmount()
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
    Private Sub txtCustomerCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.TextChanged
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
        Dim objCurrent As New CommonInfo.IntDiamondPriceRateInfo
        Dim _Carat As Decimal = 0.0
        Dim Amt As Long = 0
        If txtBarcodeNo.Text <> "" Then
            _BarcodeNo = txtBarcodeNo.Text
            objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo, " And F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='0' AND IsClosed='0'")

            ShowForSaleBarcodeData(objSItem)
            _dtSalesInvoiceItem = _SalesItemController.GetSalesItemGems(_ForSaleID)
            If _dtSalesInvoiceItem.Rows.Count > 0 Then
                For k As Integer = 0 To _dtSalesInvoiceItem.Rows.Count - 1
                    If _dtSalesInvoiceItem.Rows(k).Item("SaleByDefinePrice") = True Then
                        _Carat = Format(CDec(_dtSalesInvoiceItem.Rows(k).Item("GemsTG") * Global_GramToKarat), 0.0)
                        objCurrent = _CurrentCon.GetIntDiamondData(_Carat)
                        With objCurrent
                            _dtSalesInvoiceItem.Rows(k).Item("UnitPrice") = .PriceRate
                            Amt = CLng(_Carat * .PriceRate)
                            If _dtSalesInvoiceItem.Rows(k).Item("Type") = "Fix" Then
                                _dtSalesInvoiceItem.Rows(k).Item("Amount") = _dtSalesInvoiceItem.Rows(k).Item("UnitPrice")
                            ElseIf _dtSalesInvoiceItem.Rows(k).Item("Type") = "ByQty" Then

                                _dtSalesInvoiceItem.Rows(k).Item("Amount") = .PriceRate * _dtSalesInvoiceItem.Rows(k).Item("Qty")
                            Else
                                _dtSalesInvoiceItem.Rows(k).Item("Amount") = CLng(Amt + (Amt * (_dtSalesInvoiceItem.Rows(k).Item("GemTaxPer")) / 100))
                            End If

                        End With
                    End If

                Next
                'For Each dr As DataRow In _dtSalesInvoiceItem.Rows
                '    Dim IsTrue As Boolean? = dr.Field(Of Boolean?)("SaleByDefinePrice")
                '    If IsTrue Then
                '        _Carat = Format(CDec((dr.Item("GemsTG")) * Global_GramToKarat), 0.0)
                '        objCurrent = _CurrentCon.GetIntDiamondData(_Carat)
                '        With objCurrent
                '            dr.Item("UnitPrice") = .PriceRate
                '            Amt = CLng(_Carat * .PurchaseRate)
                '            dr.Item("Amount") = CLng(Amt + (Amt * (dr.Item("GemTaxPer")) / 100))
                '        End With
                '    End If

                'Next
            End If
            grdSaleCategory.DataSource = _dtSalesInvoiceItem
            CalculategrdTotalAmount()
            CalculateTotalAmount()


        Else
            ClearItemCode()
        End If

    End Sub
    Private Sub ShowForSaleBarcodeData(ByVal objSItem As SalesItemInfo)
        Dim objCurrentPrice As New CurrentPriceInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo

        With objSItem
            _ForSaleID = .ForSaleID
            txtBarcodeNo.Text = _BarcodeNo
            txtItemCategory.Text = _ItemCategoryController.GetItemCategory(.ItemCategoryID).ItemCategory
            txtItemName.Text = .ItemName
            txtGoldQuality.Text = _GoldQualityController.GetGoldQuality(.GoldQualityID).GoldQuality
            txtTaxPer.Text = _ItemCategoryController.GetItemCategory(.ItemCategoryID).ItemTaxPer


            _IsGram = _GoldQualityController.GetGoldQuality(.GoldQualityID).IsGramRate
            txtColor.Text = .Color
            If _ForSaleID IsNot Nothing Then
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
                    txtDesignRate.Enabled = True
                    txtDesignRate.Text = "0"
                Else
                    lblPercent.Text = "၁ ကျပ်သားစျေး"
                    txtDesignRate.Enabled = False
                    txtDesignRate.Text = "0"
                End If
                'If Global_UserLevel = "Administrator" Then
                GoldQualityForTextChange()

                'End If
            Else
                lblPercent.Text = ""
                txtLength.Text = ""
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
            End If

            objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(.GoldQualityID)
            txtCurrentPrice.Text = objCurrentPrice.SalesRate

            If .GemsTG > 0 Then
                GoldWeight.GoldTK = .GemsTK
                GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtGemKForSale.Text = CStr(GoldWeight.WeightK)
                txtGemPForSale.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtGemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtGemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                txtGemTKForSale.Text = Format(.GemsTK, "0.000")
                txtGemTGForSale.Text = Format(.GemsTG, "0.000")
            Else
                txtGemKForSale.Text = "0"
                txtGemPForSale.Text = "0"
                txtGemYForSale.Text = "0.0"
                txtGemTKForSale.Text = "0.0"
                txtGemTGForSale.Text = "0.0"
            End If
            _GemsTK = .GemsTK
            _GemsTG = .GemsTG

            If .ItemTG > 0 Then
                GoldWeight.GoldTK = .ItemTK
                GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtItemK.Text = CStr(GoldWeight.WeightK)
                txtItemP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtItemTG.Text = Format(.ItemTG, "0.000")
                txtItemTK.Text = Format(.ItemTK, "0.000")
                lblItemGram.Text = Format(.ItemTG, "0.00") + " gram"
            Else
                txtItemK.Text = "0"
                txtItemP.Text = "0"
                txtItemY.Text = "0.00"
                txtItemTG.Text = "0.0"
                txtItemTK.Text = "0.0"
                lblItemGram.Text = ""
            End If

            _ItemTK = .ItemTK
            _ItemTG = .ItemTG


            If .WasteTK > 0 Then
                GoldWeight.GoldTK = .WasteTK
                GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                txtWasteK.Text = CStr(GoldWeight.WeightK)
                txtWasteP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtWasteTK.Text = Format(.WasteTK, "0.000")
                txtWasteTG.Text = Format(.WasteTG, "0.000")
            Else
                txtWasteK.Text = "0"
                txtWasteP.Text = "0"
                txtWasteY.Text = "0.00"
                txtWasteTK.Text = "0.0"
                txtWasteTG.Text = "0.0"
            End If
            _WasteTK = .WasteTK
            _WasteTG = .WasteTG

            CalculateTotalWeight()
            CalculateGoldWeight()

            txtWhiteCharges.Text = Format(Val(.WhiteCharges), "###,##0.##")
            txtPlatingCharges.Text = Format(Val(.PlatingCharges), "###,##0.##")
            txtMountingCharges.Text = Format(Val(.MountingCharges), "###,##0.##")
            txtDesignCharges.Text = Format(Val(.DesignCharges), "###,##0.##")
            chkIsFixPrice.Checked = Format(Val(.IsFixPrice), "###,##0.##")

            Dim TempTK As Decimal = 0.0
            Dim Gold As New CommonInfo.GoldWeightInfo
            Dim _GoldPrice As Integer = 0
            Dim _TotalGoldPrice As Integer = 0
            Gold.WeightK = CInt(txtGoldK.Text) + CInt(txtWasteK.Text)
            Gold.WeightP = CInt(txtGoldP.Text) + CInt(txtWasteP.Text)
            Gold.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtWasteY.Text))
            Gold.WeightC = (CDec(txtGoldY.Text) + CDec(txtWasteY.Text)) - Gold.WeightY

            Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
            TempTK = Gold.GoldTK

            If (.IsFixPrice = True) Then
                isFixPrice = True
                _FixPrice = .FixPrice
                txtGoldPrice.Text = "0"
                txtGemsPrice.Text = "0"
                txtGold.Text = "0"

                txtTaxAmt.Text = Format(CInt((_FixPrice * CInt(txtTaxPer.Text)) / 100), "###,##0.##")
                txtTotalAmt.Text = Format(_FixPrice + CInt(txtTaxAmt.Text), "###,##0.##")

            Else
                isFixPrice = False
                If _IsGram = False Then
                    _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * TempTK)
                Else
                    _GoldPrice = CStr((CLng(txtCurrentPrice.Text) * (CDec(txtGoldTG.Text)) + CDec(txtWasteTG.Text)))
                End If

                txtGold.Text = Format(_GoldPrice, "###,##0.##")

                _TotalGoldPrice = _GoldPrice + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
                txtTaxAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtTaxPer.Text)) / 100), "###,##0.##")
                txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
                txtTotalAmt.Text = Format(Val(CLng(txtGoldPrice.Text) + CLng(txtGemsPrice.Text)), "###,##0.##")
            End If

            If .Photo <> "" Then
                Try
                    lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + .Photo)
                    PName = .Photo
                    lblPhoto.Visible = False
                Catch
                    lblItemImage.Image = Nothing
                    PName = ""
                    lblPhoto.Visible = True
                End Try

            Else
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
            IsOriginalFixedPrice = .IsOriginalFixedPrice
            _OriginalFixedPrice = .OriginalFixedPrice
            IsOriginalPriceGram = .IsOriginalPriceGram
            _OriginalPriceGram = .OriginalPriceGram
            _OriginalPriceTK = .OriginalPriceTK
            _OriginalGemsPrice = .OriginalGemsPrice
            _OriginalOtherPrice = .OriginalOtherPrice
            _PurchaseWasteTK = .PurchaseWasteTK
            _PurchaseWasteTG = .PurchaseWasteTG

            lblOriginalCode.Visible = True
            lblPriceCode.Visible = True
            lblOriginalCode.Text = .OriginalCode
            lblPriceCode.Text = .PriceCode
            chkIsDiamond.Checked = .IsDiamond

            txtSellingRate.Text = .SellingRate

        End With


    End Sub

    Private Sub GoldQualityForTextChange()
        'If Global_UserLevel = "Administrator" Then
        If _IsGram = True Then
            txtItemK.ReadOnly = True
            txtItemP.ReadOnly = True
            txtItemY.ReadOnly = True
            txtItemTG.ReadOnly = False


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
        'ElseIf (Global_IsAllowUpdate = True) Then
        'If _IsGram = True Then
        '    txtItemK.ReadOnly = True
        '    txtItemP.ReadOnly = True
        '    txtItemY.ReadOnly = True
        '    txtItemTG.ReadOnly = False


        '    txtItemK.BackColor = Color.Linen
        '    txtItemP.BackColor = Color.Linen
        '    txtItemY.BackColor = Color.Linen
        '    txtItemTG.BackColor = Color.White


        '    txtWasteK.ReadOnly = True
        '    txtWasteP.ReadOnly = True
        '    txtWasteY.ReadOnly = True
        '    txtWasteTG.ReadOnly = False


        '    txtWasteK.BackColor = Color.Linen
        '    txtWasteP.BackColor = Color.Linen
        '    txtWasteY.BackColor = Color.Linen
        '    txtWasteTG.BackColor = Color.White

        'Else
        '    txtItemK.ReadOnly = False
        '    txtItemP.ReadOnly = False
        '    txtItemY.ReadOnly = False
        '    txtItemTG.ReadOnly = True


        '    txtItemK.BackColor = Color.White
        '    txtItemP.BackColor = Color.White
        '    txtItemY.BackColor = Color.White
        '    txtItemTG.BackColor = Color.Linen

        '    txtWasteK.ReadOnly = False
        '    txtWasteP.ReadOnly = False
        '    txtWasteY.ReadOnly = False
        '    txtWasteTG.ReadOnly = True

        '    txtWasteK.BackColor = Color.White
        '    txtWasteP.BackColor = Color.White
        '    txtWasteY.BackColor = Color.White
        '    txtWasteTG.BackColor = Color.Linen

        'End If

        ' End If

    End Sub

    Private Sub txtItemTG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtItemTG_TextChanged(sender As Object, e As EventArgs) Handles txtItemTG.TextChanged
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.0"

        If Val(txtItemTG.Text) >= 0 And _IsGram = True Then
            CalculateItemWeightForGram()
        End If
        _ItemTG = txtItemTG.Text
        CalculateTotalWeight()
        CalculateGoldWeight()
        CalculateGoldPrice()
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
            txtItemTK.Text = Format(_ItemTK, "0.000")
        Else
            _ItemTG = 0.0
            _ItemTK = 0.0

            txtItemTG.Text = "0.0"
            txtItemTK.Text = "0.0"
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
            txtItemTK.Text = Format(_ItemTK, "0.000")
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
            _ItemTG = 0.0
            _ItemTK = 0.0
            txtItemK.Text = "0"
            txtItemP.Text = "0"
            txtItemY.Text = "0.0"
            txtItemTK.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateTotalWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0
        If _ItemTG > 0.0 Or _WasteTG > 0.0 Then
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
                txtTotalY.Text = Format(TotalWeight.WeightY, "0.0")
            Else
                txtTotalY.Text = Format(TotalWeight.WeightY, "0.00")
            End If
            'txtTotalY.Text = Format(TotalWeight.WeightY, "0.0")

            TotalWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(TotalWeight)
            _TotalTK = TotalWeight.GoldTK

            _TotalTG = CDec(txtItemTG.Text) + CDec(txtWasteTG.Text)
            txtTotalTG.Text = Format(CDec(txtItemTG.Text) + CDec(txtWasteTG.Text), "0.000")
            txtTotalTK.Text = Format(CDec(txtItemTK.Text) + CDec(txtWasteTK.Text), "0.000")
        Else
            _TotalTK = 0.0
            _TotalTG = 0.0

            txtTotalK.Text = "0"
            txtTotalP.Text = "0"
            txtTotalY.Text = "0"
            txtTotalTG.Text = "0.0"
            txtTotalTK.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateGoldWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If _ItemTG > 0.0 Or _GemsTG > 0.0 Then
            Dim ItemWeight As New CommonInfo.GoldWeightInfo
            Dim GemWeight As New CommonInfo.GoldWeightInfo
            Dim GoldWeight As New CommonInfo.GoldWeightInfo

            ItemWeight.WeightK = CDec(txtItemK.Text)
            ItemWeight.WeightP = CDec(txtItemP.Text)
            ItemWeight.WeightY = CDec(txtItemY.Text)

            GemWeight.WeightK = CDec(txtGemKForSale.Text)
            GemWeight.WeightP = CDec(txtGemPForSale.Text)
            GemWeight.WeightY = CDec(txtGemYForSale.Text)

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
            If numberformat = 1 Then
                txtGoldY.Text = Format(GoldWeight.WeightY, "0.0")
            Else
                txtGoldY.Text = Format(GoldWeight.WeightY, "0.00")
            End If
            'txtGoldY.Text = Format(GoldWeight.WeightY, "0.0")

            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _GoldTK = GoldWeight.GoldTK

            _GoldTG = CDec(txtItemTG.Text) - CDec(txtGemTGForSale.Text)
            txtGoldTG.Text = Format(CDec(txtItemTG.Text) - CDec(txtGemTGForSale.Text), "0.000")
            txtGoldTK.Text = Format(CDec(txtItemTK.Text) - CDec(txtGemTKForSale.Text), "0.000")
        Else
            _GoldTG = 0.0
            _GoldTK = 0.0

            txtGoldK.Text = "0"
            txtGoldP.Text = "0"
            txtGoldY.Text = "0"
            txtGoldTG.Text = "0.0"
            txtGoldTK.Text = "0.0"
        End If

    End Sub

    Private Sub txtItemK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub


    Private Sub txtItemK_TextChanged(sender As Object, e As EventArgs) Handles txtItemK.TextChanged
        If txtItemK.Text = "" Then txtItemK.Text = "0"

        If Val(txtItemK.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
        End If

        CalculateTotalWeight()
        CalculateGoldWeight()
        CalculateGoldPrice()
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

        If Val(txtItemP.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
            'CalculateItemWeightForKPY()
        End If

        CalculateTotalWeight()
        CalculateGoldWeight()
        CalculateGoldPrice()

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

        If Val(txtItemY.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
            'CalculateItemWeightForKPY()
        End If

        CalculateTotalWeight()
        CalculateGoldWeight()
        CalculateGoldPrice()
    End Sub

    Private Sub txtWasteTG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub


    Private Sub txtWasteTG_TextChanged(sender As Object, e As EventArgs) Handles txtWasteTG.TextChanged
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0.0"

        If Val(txtWasteTG.Text.Trim) >= 0 And _IsGram = True Then
            CalculateWasteWeightForGram()
        End If

        CalculateTotalWeight()
        CalculateGoldPrice()
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
            txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtWasteTK.Text = Format(_WasteTK, "0.000")
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0
            txtWasteK.Text = "0"
            txtWasteP.Text = "0"
            txtWasteY.Text = "0.0"
            txtWasteTK.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateWasteWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.00"

        If (Val(txtWasteK.Text) > 0 Or Val(txtWasteP.Text) > 0 Or Val(txtWasteY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtWasteK.Text)
            GoldWeight.WeightP = CInt(txtWasteP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtWasteY.Text)
            GoldWeight.WeightC = CDec(txtWasteY.Text) - GoldWeight.WeightY

            GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
            _WasteTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (objConverterController.GetMeasurement("Kyat", "Gram"))
            _WasteTG = GoldWeight.Gram
            txtWasteTG.Text = Format(_WasteTG, "0.000")
            txtWasteTK.Text = Format(_WasteTK, "0.000")
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0

            'txtWasteK.Text = "0"
            'txtWasteP.Text = "0"
            'txtWasteY.Text = "0.0"
            txtWasteTG.Text = "0.0"
            txtWasteTK.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateGoldPrice()
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"
        Dim TempTK As Decimal = 0.0
        Dim _GoldPrice As Integer = 0
        Dim _TotalGoldPrice As Integer = 0
        Dim _DesignCharges As Integer = 0
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0"
        If txtTaxPer.Text = "" Then txtTaxPer.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtGold.Text = "" Then txtGold.Text = "0"


        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        GoldWeight.WeightK = CInt(txtGoldK.Text) + CInt(txtWasteK.Text)
        GoldWeight.WeightP = CInt(txtGoldP.Text) + CInt(txtWasteP.Text)
        GoldWeight.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtWasteY.Text))
        GoldWeight.WeightC = (CDec(txtGoldY.Text) + CDec(txtWasteY.Text)) - GoldWeight.WeightY
        GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
        TempTK = GoldWeight.GoldTK

        If (isFixPrice = False) Then
            If _IsGram = False Then
                _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * TempTK)
                _DesignCharges = txtDesignCharges.Text
            Else
                _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * (CDec(txtGoldTG.Text) + CDec(txtWasteTG.Text)))
                _DesignCharges = CStr(CLng(txtDesignRate.Text) * (CDec(txtGoldTG.Text) + CDec(txtWasteTG.Text)))
            End If
        End If
        txtGold.Text = Format(_GoldPrice, "###,##0.##")

        _TotalGoldPrice = _GoldPrice + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
        txtTaxAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtTaxPer.Text)) / 100), "###,##0.##")
        If Global_GIsFixPrice Then
            txtSellingAmt.Text = "0"
        Else
            txtSellingAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtSellingRate.Text)) / 100), "###,##0.##")
        End If
        txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
        txtDesignCharges.Text = Format(_DesignCharges, "###,##0.##")
        CalculateTotalAmount()
    End Sub

    Private Sub txtWasteK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWasteK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtWasteK_TextChanged(sender As Object, e As EventArgs) Handles txtWasteK.TextChanged
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"

        If Val(txtWasteK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteWeightForKPY()
        End If

        CalculateTotalWeight()
        CalculateGoldPrice()
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

        If Val(txtWasteP.Text.Trim) >= 0 Then
            If _IsGram = False Then
                CalculateWasteWeightForKPY()
            End If

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
            txtWasteY.Text = "0.0"
            txtWasteY.SelectAll()
        End If

        If Val(txtWasteY.Text.Trim) >= 0 Then
            If _IsGram = False Then
                CalculateWasteWeightForKPY()
            End If

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
        'If _IsGram = False Then
        If _IsGram = False And frm._OptType = "Kyat" Then
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            txtItemK.Text = CStr(GoldWeight.WeightK)
            txtItemP.Text = CStr(GoldWeight.WeightP)
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            txtItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
        ElseIf _IsGram = False And _WeightType = "Gram" Then
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")

            If Val(txtItemTG.Text.Trim) >= 0.0 Then
                'If _VIsGram = True Then
                CalculateItemWeightForGram()
                'End If
            End If
        Else
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
        End If
        CalculateTotalWeight()
        CalculateGoldWeight()
        CalculateGoldPrice()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtBarcodeNo.Text = ""
        ClearItemCode()
        txtBarcodeNo.Focus()
    End Sub


    Private Sub grdSaleCategory_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles grdSaleCategory.CellValidated
        If grdSaleCategory.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdSaleCategory.Columns(e.ColumnIndex).Name

                Case "UnitPrice", "GemTaxPer"
                    Dim GemAmount As Integer = 0
                    If Not IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("Type").Value) Then
                        If IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                            grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value = 0
                        End If

                        If grdSaleCategory.Rows(e.RowIndex).Cells("Type").Value = "Fix" Then
                            If Not IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                                GemAmount = CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value)
                                grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value = CInt(GemAmount * (grdSaleCategory.Rows(e.RowIndex).Cells("GemTaxPer").Value / 100))
                                grdSaleCategory.Rows(e.RowIndex).Cells("Amount").Value = GemAmount + CInt(grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value)

                            End If

                        ElseIf grdSaleCategory.Rows(e.RowIndex).Cells("Type").Value = "ByWeight" Then
                            Dim _Type As Boolean = False
                            If Not (IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value)) Then
                                If (IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value)) Then
                                    _Type = True
                                ElseIf grdSaleCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0" Then
                                    _Type = True
                                Else
                                    _Type = False
                                End If

                                If _Type = False Then
                                    GemAmount = CInt(IIf(IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("GemsTW").Value) = True, 0, grdSaleCategory.Rows(e.RowIndex).Cells("GemsTW").Value) * CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value))
                                Else
                                    GemAmount = CInt(IIf(IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("GemsTK").Value) = True, 0, grdSaleCategory.Rows(e.RowIndex).Cells("GemsTK").Value) * CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value))
                                End If
                                grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value = CInt(GemAmount * (grdSaleCategory.Rows(e.RowIndex).Cells("GemTaxPer").Value / 100))
                                grdSaleCategory.Rows(e.RowIndex).Cells("Amount").Value = GemAmount + CInt(grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value)
                            End If

                        Else
                            If Not IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                                GemAmount = CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) * CInt(grdSaleCategory.Rows(e.RowIndex).Cells("Qty").Value)
                                grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value = CInt(GemAmount * (grdSaleCategory.Rows(e.RowIndex).Cells("GemTaxPer").Value / 100))
                                grdSaleCategory.Rows(e.RowIndex).Cells("Amount").Value = GemAmount + CInt(grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value)
                            End If
                        End If
                    End If
                Case "GemTax"
                    If Not IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("Type").Value) Then
                        Dim Amount As Integer = 0
                        If IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                            grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value = 0
                        End If

                        If grdSaleCategory.Rows(e.RowIndex).Cells("Type").Value = "Fix" Then
                            If Not IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                                Amount = CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value)
                                grdSaleCategory.Rows(e.RowIndex).Cells("Amount").Value = CInt(Amount + grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value)

                            End If

                        ElseIf grdSaleCategory.Rows(e.RowIndex).Cells("Type").Value = "ByWeight" Then
                            Dim _Type As Boolean = False
                            If Not (IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value)) Then
                                If (IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value)) Then
                                    _Type = True
                                ElseIf grdSaleCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0" Then
                                    _Type = True
                                Else
                                    _Type = False
                                End If

                                If _Type = False Then
                                    Amount = CInt(IIf(IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("GemsTW").Value) = True, 0, grdSaleCategory.Rows(e.RowIndex).Cells("GemsTW").Value) * CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value))
                                Else
                                    Amount = CInt(IIf(IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("GemsTK").Value) = True, 0, grdSaleCategory.Rows(e.RowIndex).Cells("GemsTK").Value) * CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value))
                                End If
                                grdSaleCategory.Rows(e.RowIndex).Cells("Amount").Value = CInt(Amount + grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value)
                            End If

                        Else
                            If Not IsDBNull(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                                Amount = CInt(grdSaleCategory.Rows(e.RowIndex).Cells("UnitPrice").Value) * CInt(grdSaleCategory.Rows(e.RowIndex).Cells("Qty").Value)
                                grdSaleCategory.Rows(e.RowIndex).Cells("Amount").Value = CInt(Amount + grdSaleCategory.Rows(e.RowIndex).Cells("GemTax").Value)
                            End If
                        End If
                    End If

            End Select


        End If
        CalculategrdTotalAmount()
        CalculateTotalAmount()

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

        If (txtNetAmt.Text <> "") And (txtTotalAmt.Text <> "") Then
            txtAddSub.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtTotalAmt.Text)), "###,##0.##")
        End If

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtBarcodeNo.Text = "" Then
            MsgBox("Please Enter ItemCode!", MsgBoxStyle.Information, "Data Require!")
            txtBarcodeNo.Focus()
            Exit Sub
        End If

        If _ForSaleID Is Nothing Then
            MsgBox("Invalid ItemCode!", MsgBoxStyle.Information, "Data Invalid!")
            txtBarcodeNo.Focus()
            Exit Sub
        End If

        If _IsGram Then
            If _ItemTG = 0 Then
                MsgBox("Please Enter Item Weight!", MsgBoxStyle.Information, "Data Require!")
                txtItemTG.Select()
                Exit Sub
            End If
        Else
            If _ItemTK = 0 Then
                MsgBox("Please Enter Item Weight!", MsgBoxStyle.Information, "Data Require!")
                txtItemK.Select()
                Exit Sub
            End If
        End If


        If (chkIsFixPrice.Checked = False) And (txtCurrentPrice.Text = "" Or txtCurrentPrice.Text = "0") Then
            MsgBox("Please Fill Current Price!", MsgBoxStyle.Information, "Data Require!")
            txtCurrentPrice.Select()
            Exit Sub
        End If

        If _ForSaleID = "" Then
            MsgBox("Invalid ItemCode!", MsgBoxStyle.Information, "Data Require!")
            txtBarcodeNo.Focus()
            Exit Sub
        End If

        If _dtItemBarcode.Rows.Count > 0 And _IsUpdate = False Then
            For Each dr As DataRow In _dtItemBarcode.Rows
                If dr.RowState <> DataRowState.Deleted Then
                    If dr("ForSaleID") = _ForSaleID Then
                        MsgBox("Duplicate BarcodeNo!", MsgBoxStyle.Information, "Duplicate Data!")
                        txtBarcodeNo.Select()
                        Exit Sub
                    End If
                End If
            Next
        End If

        If Global_UserLevel <> "Administrator" Then
            If _OldTotalItemTG <> _ItemTG Then
                If (_ItemTG > (_OldTotalItemTG + Global_AllowEditWeight)) Or (_ItemTG < (_OldTotalItemTG - Global_AllowEditWeight)) Then
                    MsgBox("Your editing item weight is exceeding than limit weight! Please login and edit with Administrator Account", MsgBoxStyle.Information, AppName)
                    'If _OldTotalItemTG > 0 Then
                    '    GoldWeight.GoldTK = _ItemTK
                    '    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    '    txtItemK.Text = CStr(GoldWeight.WeightK)
                    '    txtItemP.Text = CStr(GoldWeight.WeightP)
                    '    If numberformat = 1 Then
                    '        txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    '    Else
                    '        txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                    '    End If
                    '    'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    '    txtItemTG.Text = Format(_ItemTG, "0.000")
                    '    txtItemTK.Text = Format(_ItemTK, "0.000")
                    '    lblItemGram.Text = Format(_ItemTG, "0.00") + " gram"
                    'Else
                    '    txtItemK.Text = "0"
                    '    txtItemP.Text = "0"
                    '    txtItemY.Text = "0.00"
                    '    txtItemTG.Text = "0.0"
                    '    txtItemTK.Text = "0.0"
                    '    lblItemGram.Text = ""
                    'End If
                    Exit Sub
                End If
            End If
            If _OldTotalWasteTG <> _WasteTG Then
                If (_WasteTG > (_OldTotalWasteTG + Global_AllowEditWeight)) Or (_WasteTG < (_OldTotalWasteTG - Global_AllowEditWeight)) Then
                    MsgBox("Your editing waste weight is exceeding than limit weight! Please login and edit with Administrator Account", MsgBoxStyle.Information, AppName)
                    'If _OldTotalWasteTG > 0 Then
                    '    GoldWeight.GoldTK = _WasteTK
                    '    GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    '    txtWasteK.Text = CStr(GoldWeight.WeightK)
                    '    txtWasteP.Text = CStr(GoldWeight.WeightP)
                    '    If numberformat = 1 Then
                    '        txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    '    Else
                    '        txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                    '    End If
                    '    'txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    '    txtWasteTK.Text = Format(_WasteTK, "0.000")
                    '    txtWasteTG.Text = Format(_WasteTG, "0.000")
                    'Else
                    '    txtWasteK.Text = "0"
                    '    txtWasteP.Text = "0"
                    '    txtWasteY.Text = "0.00"
                    '    txtWasteTK.Text = "0.0"
                    '    txtWasteTG.Text = "0.0"
                    'End If
                    Exit Sub
                End If
            End If
        End If

        If _IsUpdate Then
            UpdateItem(_SalesInvoiceDetailID, _dtSalesInvoiceItem)
            If _dtItemBarcode.Rows.Count > 1 Then

                GetStringID(_dtItemBarcode, "ItemCode", "")
            Else
                GetStringID(_dtItemBarcode, "ItemCode", "")

            End If
            _ItemCodeStr = " F.ItemCode  In (" & strCri & ")"
        Else
            If btnAdd.Text = "Add" Then

                _SalesInvoiceDetailID = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SaleInvoiceDetail, EnumSetting.GenerateKeyType.SaleInvoiceDetail.ToString, dtpSaleDate.Value)
                InsertItem(_SalesInvoiceDetailID, _dtSalesInvoiceItem)
                If _dtItemBarcode.Rows.Count > 1 Then

                    GetStringID(_dtItemBarcode, "ItemCode", "")
                Else
                    GetStringID(_dtItemBarcode, "ItemCode", "")

                End If
                _ItemCodeStr = " F.ItemCode  In (" & strCri & ")"
            End If
        End If




        txtBarcodeNo.Text = ""
        txtBarcodeNo.Focus()



        ClearItemCode()


        CalculategrAlldTotalAmount()
        CalculategrAlldTotalWeight()

    End Sub
    Private Sub GetStringID(ByVal dt As DataTable, ByVal FieldName As String, ByVal ColumnName As String)
        strCri = ReturnStringCri(dt, FieldName, "")
    End Sub
    Private Function ReturnStringCri(ByVal dt As DataTable, ByVal Name As String, Optional ByVal OtherFieldName As String = "") As String
        Dim strCust As String = ""
        If OtherFieldName <> "" Then
            For Each row As DataRow In dt.Rows
                strCust += " [" & row(Name).ToString & "]=N'" & row(OtherFieldName).ToString & "'" & " AND"
            Next
            If strCust.EndsWith("AND") Then
                strCust = strCust.Remove(strCust.Length - 3, 3)
            End If
        Else
            For Each row As DataRow In dt.Rows
                strCust += "'" & row(Name).ToString & "'" & ","
            Next
            If strCust <> "" Then
                strCust = Mid(strCust, 1, strCust.Length - 1)
            End If
        End If

        Return strCust
    End Function

    Private Sub CalculategrAlldTotalAmount()
        Dim _AllTotalAmount As Long = 0
        Dim _AllAllTaxAmt As Long
        ' Dim _AllAddorSub As Long
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"

        For j As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(j).IsNewRow Then
                If CBool(grdDetail.Rows(j).Cells("IsSaleReturn").Value) = False Then
                    _AllTotalAmount += CLng(grdDetail.Rows(j).Cells("NetAmount").FormattedValue)
                    _AllAllTaxAmt += CLng(grdDetail.Rows(j).Cells("ItemTax").FormattedValue)
                    '_AllAddorSub += CLng(grdDetail.Rows(j).Cells("AddOrSub").FormattedValue)
                End If
            End If
        Next

        For Each dr As DataRow In _dtAllDiamond.Rows
            If dr.RowState <> DataRowState.Deleted Then
                _AllAllTaxAmt += dr.Item("GemTax")
            End If
        Next

        txtAllTotalAmt.Text = Format(Val(_AllTotalAmount), "###,##0.##")
        txtAllTaxAmt.Text = Format(Val(_AllAllTaxAmt), "###,##0.##")
        'txtAllAddOrSub.Text = Format(Val(_AllAddorSub), "###,##0.##")
        ' txtAllAddOrSub.Text = "0"
        txtDiscountAmt.Text = "0"
        txtPromotionDis.Text = "0"
        txtPromotionAmt.Text = "0"

        txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
        CalculateFinalAmount()
    End Sub
    Private Sub CalculategrAlldTotalWeight()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim _TotalTK As Decimal = 0
        Dim _TotalTG As Decimal = 0
        Dim _TotalQTY As Integer = 0
        For j As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(j).IsNewRow Then
                If CBool(grdDetail.Rows(j).Cells("IsSaleReturn").Value) = False Then
                    _TotalTG += CDec(grdDetail.Rows(j).Cells("ItemTG").FormattedValue)
                    _TotalTK += CDec(grdDetail.Rows(j).Cells("ItemTK").FormattedValue)
                    _TotalQTY += 1
                End If
            End If
        Next

        GoldWeight.GoldTK = _TotalTK
        GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
        txtTotalItemK.Text = CStr(GoldWeight.WeightK)
        txtTotalItemP.Text = CStr(GoldWeight.WeightP)
        If numberformat = 1 Then
            txtTotalItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        Else
            txtTotalItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
        End If
        'txtTotalItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        txtTotalItemG.Text = Format(_TotalTG, "0.000")
        txtTotalQTY.Text = _TotalQTY
    End Sub
    Public Sub InsertItem(ByVal _SaleInvoiceDetailID As String, ByVal _dtSalesInvoiceItem As DataTable)
        Dim drItem As DataRow

        drItem = _dtItemBarcode.NewRow
        drItem.Item("SaleInvoiceDetailID") = _SaleInvoiceDetailID
        drItem.Item("SaleInvoiceHeaderID") = _SalesInvoiceID
        drItem.Item("ForSaleID") = _ForSaleID
        drItem.Item("ItemCode") = txtBarcodeNo.Text
        drItem.Item("ItemCategory") = txtItemCategory.Text
        drItem.Item("ItemName") = txtItemName.Text
        drItem.Item("GoldQuality") = txtGoldQuality.Text
        drItem.Item("Gram") = _ItemTG
        drItem.Item("ItemTG") = _ItemTG
        drItem.Item("ItemTK") = _ItemTK
        drItem.Item("WasteTG") = _WasteTG
        drItem.Item("WasteTK") = _WasteTK
        drItem.Item("GemsTK") = _GemsTK
        drItem.Item("GemsTG") = _GemsTG
        drItem.Item("SalesRate") = CInt(txtCurrentPrice.Text)
        drItem.Item("GoldPrice") = CInt(txtGoldPrice.Text)
        drItem.Item("GemsPrice") = CInt(txtGemsPrice.Text)
        drItem.Item("WhiteCharges") = CInt(IIf(txtWhiteCharges.Text = "", 0, txtWhiteCharges.Text))
        drItem.Item("PlatingCharges") = CInt(IIf(txtPlatingCharges.Text = "", 0, txtPlatingCharges.Text))
        drItem.Item("MountingCharges") = CInt(IIf(txtMountingCharges.Text = "", 0, txtMountingCharges.Text))
        drItem.Item("DesignCharges") = CInt(IIf(txtDesignCharges.Text = "", 0, txtDesignCharges.Text))
        drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
        drItem.Item("TotalAmount") = CInt(txtTotalAmt.Text)
        drItem.Item("AddOrSub") = CInt(txtAddSub.Text)
        drItem.Item("NetAmount") = CInt(txtNetAmt.Text)
        drItem.Item("IsOriginalFixedPrice") = IsOriginalFixedPrice
        drItem.Item("OriginalFixedPrice") = _OriginalFixedPrice
        drItem.Item("IsOriginalPriceGram") = IsOriginalPriceGram
        drItem.Item("OriginalPriceGram") = _OriginalPriceGram
        drItem.Item("OriginalPriceTK") = _OriginalPriceTK
        drItem.Item("OriginalGemsPrice") = _OriginalGemsPrice
        drItem.Item("OriginalOtherPrice") = _OriginalOtherPrice
        drItem.Item("PurchaseWasteTK") = _PurchaseWasteTK
        drItem.Item("PurchaseWasteTG") = _PurchaseWasteTG
        drItem.Item("ItemTaxPer") = Format(CDec(IIf(txtTaxPer.Text = "", 0, txtTaxPer.Text)), "###,##0.##")
        drItem.Item("ItemTax") = CInt(IIf(txtTaxAmt.Text = "", 0, txtTaxAmt.Text))
        drItem.Item("IsSaleReturn") = _IsSaleReturn

        drItem.Item("ItemK") = txtItemK.Text
        drItem.Item("ItemP") = txtItemP.Text
        drItem.Item("ItemY") = txtItemY.Text
        drItem.Item("WasteK") = txtWasteK.Text
        drItem.Item("WasteP") = txtWasteP.Text
        drItem.Item("WasteY") = txtWasteY.Text
        drItem.Item("DesignChargesRate") = CInt(IIf(txtDesignRate.Text = "", 0, txtDesignRate.Text))
        drItem.Item("SellingRate") = CInt(IIf(txtSellingRate.Text = "", 0, txtSellingRate.Text))
        drItem.Item("SellingAmt") = CInt(IIf(txtSellingAmt.Text = "", 0, txtSellingAmt.Text))

        _dtItemBarcode.Rows.Add(drItem)
        grdDetail.DataSource = _dtItemBarcode
        Dim i As Integer = 1
        For Each drNo As DataRow In _dtItemBarcode.Rows
            If drNo.RowState <> DataRowState.Deleted Then
                drNo.Item("SNo") = i
                i = i + 1
            End If
        Next

        Dim drDiamond As DataRow
        For Each dr As DataRow In _dtSalesInvoiceItem.Rows
            drDiamond = _dtAllDiamond.NewRow()
            drDiamond("SalesInvoiceGemItemID") = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SaleInvoiceGemItem, CommonInfo.EnumSetting.GenerateKeyType.SaleInvoiceGemItem.ToString, Now.Date)
            drDiamond("@SaleInvoiceDetailID") = _SaleInvoiceDetailID
            drDiamond("GemsCategory") = dr("GemsCategory")
            drDiamond("@GemsCategoryID") = dr("@GemsCategoryID")
            drDiamond("GemsName") = dr("GemsName")
            drDiamond("GemsK") = dr("GemsK")
            drDiamond("GemsP") = dr("GemsP")
            drDiamond("GemsY") = dr("GemsY")
            drDiamond("GemsTK") = dr("GemsTK")
            drDiamond("GemsTG") = dr("GemsTG")
            drDiamond("YOrCOrG") = dr("YOrCOrG")
            drDiamond("GemsTW") = dr("GemsTW")
            drDiamond("Qty") = dr("Qty")
            drDiamond("UnitPrice") = dr("UnitPrice")
            drDiamond("Type") = dr("Type")
            drDiamond("Amount") = dr("Amount")
            drDiamond("GemsRemark") = dr("GemsRemark")
            drDiamond("GemTaxPer") = dr("GemTaxPer")
            drDiamond("GemTax") = dr("GemTax")

            _dtAllDiamond.Rows.Add(drDiamond)
        Next
    End Sub

    Public Sub UpdateItem(ByVal _SaleInvoiceDetailID As String, ByVal _dtSalesInvoiceItem As DataTable)
        Dim drItem As DataRow
        drItem = _dtItemBarcode.Rows(grdDetail.CurrentRow.Index)

        If Not IsNothing(drItem) Then
            drItem.Item("SaleInvoiceDetailID") = _SaleInvoiceDetailID
            drItem.Item("SaleInvoiceHeaderID") = _SalesInvoiceID
            drItem.Item("ForSaleID") = _ForSaleID
            drItem.Item("ItemCode") = txtBarcodeNo.Text
            drItem.Item("ItemCategory") = txtItemCategory.Text
            drItem.Item("ItemName") = txtItemName.Text
            drItem.Item("GoldQuality") = txtGoldQuality.Text
            drItem.Item("Gram") = _ItemTG
            drItem.Item("ItemTG") = _ItemTG
            drItem.Item("ItemTK") = _ItemTK
            drItem.Item("WasteTG") = _WasteTG
            drItem.Item("WasteTK") = _WasteTK
            drItem.Item("GemsTK") = _GemsTK
            drItem.Item("GemsTG") = _GemsTG
            drItem.Item("SalesRate") = CInt(txtCurrentPrice.Text)
            drItem.Item("GoldPrice") = CInt(txtGoldPrice.Text)
            drItem.Item("GemsPrice") = CInt(txtGemsPrice.Text)
            drItem.Item("WhiteCharges") = CInt(IIf(txtWhiteCharges.Text = "", 0, txtWhiteCharges.Text))
            drItem.Item("PlatingCharges") = CInt(IIf(txtPlatingCharges.Text = "", 0, txtPlatingCharges.Text))
            drItem.Item("MountingCharges") = CInt(IIf(txtMountingCharges.Text = "", 0, txtMountingCharges.Text))
            drItem.Item("DesignCharges") = CInt(IIf(txtDesignCharges.Text = "", 0, txtDesignCharges.Text))
            drItem.Item("IsFixPrice") = chkIsFixPrice.Checked
            drItem.Item("TotalAmount") = CInt(txtTotalAmt.Text)
            drItem.Item("AddOrSub") = CInt(txtAddSub.Text)
            drItem.Item("NetAmount") = CInt(txtNetAmt.Text)
            drItem.Item("IsOriginalFixedPrice") = IsOriginalFixedPrice
            drItem.Item("OriginalFixedPrice") = _OriginalFixedPrice
            drItem.Item("IsOriginalPriceGram") = IsOriginalPriceGram
            drItem.Item("OriginalPriceGram") = _OriginalPriceGram
            drItem.Item("OriginalPriceTK") = _OriginalPriceTK
            drItem.Item("OriginalGemsPrice") = _OriginalGemsPrice
            drItem.Item("OriginalOtherPrice") = _OriginalOtherPrice
            drItem.Item("PurchaseWasteTK") = _PurchaseWasteTK
            drItem.Item("PurchaseWasteTG") = _PurchaseWasteTG
            drItem.Item("ItemTaxPer") = Format(CDec(IIf(txtTaxPer.Text = "", 0, txtTaxPer.Text)), "###,##0.##")
            drItem.Item("ItemTax") = CInt(IIf(txtTaxAmt.Text = "", 0, txtTaxAmt.Text))
            drItem.Item("IsSaleReturn") = _IsSaleReturn

            drItem.Item("ItemK") = txtItemK.Text
            drItem.Item("ItemP") = txtItemP.Text
            drItem.Item("ItemY") = txtItemY.Text
            drItem.Item("WasteK") = txtWasteK.Text
            drItem.Item("WasteP") = txtWasteP.Text
            drItem.Item("WasteY") = txtWasteY.Text
            drItem.Item("DesignChargesRate") = CInt(IIf(txtDesignRate.Text = "", 0, txtDesignRate.Text))
            drItem.Item("SellingRate") = CInt(IIf(txtSellingRate.Text = "", 0, txtSellingRate.Text))
            drItem.Item("SellingAmt") = CInt(IIf(txtSellingAmt.Text = "", 0, txtSellingAmt.Text))


            _dtSalesInvoiceItem = grdSaleCategory.DataSource

            Dim n As Integer = 1
            For Each drNo As DataRow In _dtItemBarcode.Rows
                If drNo.RowState <> DataRowState.Deleted Then
                    drNo.Item("SNo") = n
                    n = n + 1
                End If
            Next

            Dim j As Integer = 0
            If _dtSalesInvoiceItem.Rows.Count > 0 Then  '   if Gems Update , check dtstone. if dtstone has Gems, delete gemsid .
                For i As Integer = 0 To _dtSalesInvoiceItem.Rows.Count - 1
                    While j < _dtAllDiamond.Rows.Count
                        Dim row As DataRow
                        row = _dtAllDiamond.Rows(j)
                        If Not IsDBNull(_dtSalesInvoiceItem.Rows(i).Item("@SaleInvoiceDetailID")) Then
                            If row.Item("@SaleInvoiceDetailID") = _SaleInvoiceDetailID Then
                                _IsRowDelete = True
                            Else
                                _IsRowDelete = False
                            End If
                            If _IsRowDelete Then
                                _dtAllDiamond.Rows.Remove(row)
                            Else
                                j = j + 1
                            End If
                        Else
                            j = j + 1
                        End If
                    End While
                Next
            Else   ' dtPDiaItemgems no row , but dtstone has another gems id.It gemsid is deleted
                If _dtAllDiamond.Rows.Count > 0 Then
                    While j < _dtAllDiamond.Rows.Count
                        Dim row As DataRow
                        row = _dtAllDiamond.Rows(j)
                        If row.Item("@SaleInvoiceDetailID") = _SaleInvoiceDetailID Then
                            _dtAllDiamond.Rows.Remove(row)
                        Else
                            j = j + 1
                        End If
                    End While

                End If
            End If


            Dim drPItemDetailStone As DataRow
            If _dtAllDiamond.Rows.Count <> 0 Then
                For i As Integer = 0 To _dtAllDiamond.Rows.Count - 1
                    If _dtAllDiamond.Rows(i).Item("@SaleInvoiceDetailID") = _SaleInvoiceDetailID Then
                        For Each drvPItemDetailStone As DataRow In _dtSalesInvoiceItem.Rows
                            If Not IsDBNull(drvPItemDetailStone("@SaleInvoiceDetailID")) And drvPItemDetailStone("@SaleInvoiceDetailID") <> "" Then
                                If _dtAllDiamond.Rows(i).Item("@SaleInvoiceDetailID") = _SaleInvoiceDetailID And _IsRowDelete <> True Then
                                    If _dtAllDiamond.Rows(i).Item("SalesInvoiceGemItemID") = drvPItemDetailStone("SalesInvoiceGemItemID") Then
                                        drvPItemDetailStone.BeginEdit()
                                        _dtAllDiamond.Rows(i).Item("SalesInvoiceGemItemID") = drvPItemDetailStone("SalesInvoiceGemItemID")
                                        _dtAllDiamond.Rows(i).Item("@SaleInvoiceDetailID") = _SaleInvoiceDetailID
                                        _dtAllDiamond.Rows(i).Item("GemsCategory") = drvPItemDetailStone("GemsCategory")
                                        _dtAllDiamond.Rows(i).Item("@GemsCategoryID") = drvPItemDetailStone("@GemsCategoryID")
                                        _dtAllDiamond.Rows(i).Item("GemsName") = drvPItemDetailStone("GemsName")
                                        _dtAllDiamond.Rows(i).Item("GemsK") = drvPItemDetailStone("GemsK")
                                        _dtAllDiamond.Rows(i).Item("GemsP") = drvPItemDetailStone("GemsP")
                                        _dtAllDiamond.Rows(i).Item("GemsY") = drvPItemDetailStone("GemsY")
                                        _dtAllDiamond.Rows(i).Item("GemsTK") = drvPItemDetailStone("GemsTK")
                                        _dtAllDiamond.Rows(i).Item("GemsTG") = drvPItemDetailStone("GemsTG")
                                        _dtAllDiamond.Rows(i).Item("YOrCOrG") = drvPItemDetailStone("YOrCOrG")
                                        _dtAllDiamond.Rows(i).Item("GemsTW") = drvPItemDetailStone("GemsTW")
                                        _dtAllDiamond.Rows(i).Item("Qty") = drvPItemDetailStone("Qty")
                                        _dtAllDiamond.Rows(i).Item("UnitPrice") = drvPItemDetailStone("UnitPrice")
                                        _dtAllDiamond.Rows(i).Item("Type") = drvPItemDetailStone("Type")
                                        _dtAllDiamond.Rows(i).Item("GemTaxPer") = drvPItemDetailStone("GemTaxPer")
                                        _dtAllDiamond.Rows(i).Item("GemTax") = drvPItemDetailStone("GemTax")
                                        _dtAllDiamond.Rows(i).Item("Amount") = drvPItemDetailStone("Amount")

                                        _dtAllDiamond.Rows(i).Item("GemsRemark") = drvPItemDetailStone("GemsRemark")
                                        drvPItemDetailStone.EndEdit()
                                        i += 1

                                    End If
                                End If
                            Else
                                drPItemDetailStone = _dtAllDiamond.NewRow()
                                If drvPItemDetailStone("SalesInvoiceGemItemID") = "" Then
                                    drPItemDetailStone("SalesInvoiceGemItemID") = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SaleInvoiceGemItem, CommonInfo.EnumSetting.GenerateKeyType.SaleInvoiceGemItem.ToString, Now.Date)
                                Else
                                    drPItemDetailStone("SalesInvoiceGemItemID") = drvPItemDetailStone("SalesInvoiceGemItemID")
                                End If
                                drPItemDetailStone("@SaleInvoiceDetailID") = _SaleInvoiceDetailID
                                drPItemDetailStone("GemsCategory") = drvPItemDetailStone("GemsCategory")
                                drPItemDetailStone("@GemsCategoryID") = drvPItemDetailStone("@GemsCategoryID")
                                drPItemDetailStone("GemsName") = drvPItemDetailStone("GemsName")
                                drPItemDetailStone("GemsK") = drvPItemDetailStone("GemsK")
                                drPItemDetailStone("GemsP") = drvPItemDetailStone("GemsP")
                                drPItemDetailStone("GemsY") = drvPItemDetailStone("GemsY")
                                drPItemDetailStone("GemsTK") = drvPItemDetailStone("GemsTK")
                                drPItemDetailStone("GemsTG") = drvPItemDetailStone("GemsTG")
                                drPItemDetailStone("YOrCOrG") = drvPItemDetailStone("YOrCOrG")
                                drPItemDetailStone("GemsTW") = drvPItemDetailStone("GemsTW")
                                drPItemDetailStone("Qty") = drvPItemDetailStone("Qty")
                                drPItemDetailStone("UnitPrice") = drvPItemDetailStone("UnitPrice")
                                drPItemDetailStone("Type") = drvPItemDetailStone("Type")
                                drPItemDetailStone("GemTaxPer") = drvPItemDetailStone("GemTaxPer")
                                drPItemDetailStone("GemTax") = drvPItemDetailStone("GemTax")
                                drPItemDetailStone("Amount") = drvPItemDetailStone("Amount")
                                drPItemDetailStone("GemsRemark") = drvPItemDetailStone("GemsRemark")

                                _dtAllDiamond.Rows.Add(drPItemDetailStone)
                                i += 1
                            End If
                        Next
                        _dtSalesInvoiceItem.DefaultView.RowFilter = ""
                        _dtSalesInvoiceItem.DefaultView.Sort = "@SaleInvoiceDetailID"

                    End If
                Next

            Else '''''' if _dtAllDiamond.Row.Count=0
                For Each drGems As DataRow In _dtSalesInvoiceItem.Rows
                    drPItemDetailStone = _dtAllDiamond.NewRow()
                    If drGems("SalesInvoiceGemItemID") = "" Then
                        drPItemDetailStone("SalesInvoiceGemItemID") = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SaleInvoiceGemItem, CommonInfo.EnumSetting.GenerateKeyType.SaleInvoiceGemItem.ToString, Now.Date)
                    Else
                        drPItemDetailStone("SalesInvoiceGemItemID") = drGems("SalesInvoiceGemItemID")
                    End If
                    drPItemDetailStone("@SaleInvoiceDetailID") = _SaleInvoiceDetailID
                    drPItemDetailStone("@GemsCategoryID") = drGems("@GemsCategoryID")
                    drPItemDetailStone("GemsCategory") = drGems("GemsCategory")
                    drPItemDetailStone("GemsName") = drGems("GemsName")
                    drPItemDetailStone("GemsK") = drGems("GemsK")
                    drPItemDetailStone("GemsP") = drGems("GemsP")
                    drPItemDetailStone("GemsY") = drGems("GemsY")
                    drPItemDetailStone("GemsTK") = drGems("GemsTK")
                    drPItemDetailStone("GemsTG") = drGems("GemsTG")
                    drPItemDetailStone("YOrCOrG") = drGems("YOrCOrG")
                    drPItemDetailStone("GemsTW") = drGems("GemsTW")
                    drPItemDetailStone("Qty") = drGems("Qty")
                    drPItemDetailStone("UnitPrice") = drGems("UnitPrice")
                    drPItemDetailStone("Type") = drGems("Type")
                    drPItemDetailStone("Amount") = drGems("Amount")
                    drPItemDetailStone("GemTaxPer") = drGems("GemTaxPer")
                    drPItemDetailStone("GemTax") = drGems("GemTax")
                    drPItemDetailStone("GemsRemark") = drGems("GemsRemark")
                    _dtAllDiamond.Rows.Add(drPItemDetailStone)

                Next

            End If
            Dim drFind As Boolean = False

            If _dtAllDiamond.Rows.Count <> 0 Then
                For i As Integer = 0 To _dtAllDiamond.Rows.Count - 1
                    If _dtAllDiamond.Rows(i).Item("@SaleInvoiceDetailID") = _SaleInvoiceDetailID Then
                        drFind = True
                    Else
                        drFind = False
                    End If

                Next
                If drFind = False Then
                    For Each drGems As DataRow In _dtSalesInvoiceItem.Rows
                        drPItemDetailStone = _dtAllDiamond.NewRow()
                        If drGems("SalesInvoiceGemItemID") = "" Then
                            drPItemDetailStone("SalesInvoiceGemItemID") = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.SaleInvoiceGemItem, CommonInfo.EnumSetting.GenerateKeyType.SaleInvoiceGemItem.ToString, Now.Date)
                        Else
                            drPItemDetailStone("SalesInvoiceGemItemID") = drGems("SalesInvoiceGemItemID")
                        End If
                        drPItemDetailStone("@SaleInvoiceDetailID") = _SaleInvoiceDetailID
                        drPItemDetailStone("@GemsCategoryID") = drGems("@GemsCategoryID")
                        drPItemDetailStone("GemsCategory") = drGems("GemsCategory")
                        drPItemDetailStone("GemsName") = drGems("GemsName")
                        drPItemDetailStone("GemsK") = drGems("GemsK")
                        drPItemDetailStone("GemsP") = drGems("GemsP")
                        drPItemDetailStone("GemsY") = drGems("GemsY")
                        drPItemDetailStone("GemsTK") = drGems("GemsTK")
                        drPItemDetailStone("GemsTG") = drGems("GemsTG")
                        drPItemDetailStone("YOrCOrG") = drGems("YOrCOrG")
                        drPItemDetailStone("GemsTW") = drGems("GemsTW")
                        drPItemDetailStone("Qty") = drGems("Qty")
                        drPItemDetailStone("UnitPrice") = drGems("UnitPrice")
                        drPItemDetailStone("Type") = drGems("Type")
                        drPItemDetailStone("GemTaxPer") = drGems("GemTaxPer")
                        drPItemDetailStone("GemTax") = drGems("GemTax")
                        drPItemDetailStone("Amount") = drGems("Amount")
                        drPItemDetailStone("GemsRemark") = drGems("GemsRemark")
                        _dtAllDiamond.Rows.Add(drPItemDetailStone)

                    Next
                End If
            End If
        End If
    End Sub

    Private Sub grdDetail_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetail.CellContentClick
        If (e.RowIndex <> -1) Then
            Select Case grdDetail.Columns(e.ColumnIndex).Name
                Case "IsSaleReturn"
                    If Not IsDBNull(grdDetail.Rows(e.RowIndex).Cells("IsSaleReturn").Value) Then
                        If grdDetail.Rows(e.RowIndex).Cells("IsSaleReturn").Value = True Then
                            grdDetail.Rows(e.RowIndex).Cells("IsSaleReturn").Value = False

                        ElseIf grdDetail.Rows(e.RowIndex).Cells("IsSaleReturn").Value = False Then
                            grdDetail.Rows(e.RowIndex).Cells("IsSaleReturn").Value = True
                        End If
                    End If
            End Select
        End If
        CalculategrAlldTotalAmount()
        CalculategrAlldTotalWeight()
    End Sub



    Private Sub grdDetail_Click(sender As Object, e As EventArgs) Handles grdDetail.Click
        Dim GoldWeight As New GoldWeightInfo
        Dim objSItem As New SalesItemInfo

        If grdDetail.RowCount = 0 Then
            _IsUpdate = False
            Exit Sub
        End If

        With grdDetail
            _SalesInvoiceDetailID = .CurrentRow.Cells("SaleInvoiceDetailID").Value
            _ForSaleID = .CurrentRow.Cells("ForSaleID").Value

            objSItem = _SalesItemController.GetForSaleItemInfo(_ForSaleID, "")
            With objSItem
                txtBarcodeNo.Text = .ItemCode
                _BarcodeNo = .ItemCode
                _ForSaleID = .ForSaleID
                chkIsDiamond.Checked = .IsDiamond
                txtItemCategory.Text = _ItemCategoryController.GetItemCategory(.ItemCategoryID).ItemCategory
                txtItemName.Text = .ItemName
                txtGoldQuality.Text = _GoldQualityController.GetGoldQuality(.GoldQualityID).GoldQuality
                _IsGram = _GoldQualityController.GetGoldQuality(.GoldQualityID).IsGramRate

                If _ForSaleID IsNot Nothing Then
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
                        txtDesignRate.Enabled = True
                    Else
                        lblPercent.Text = "၁ ကျပ်သားစျေး"
                        txtDesignRate.Enabled = False
                    End If

                    If Global_UserLevel = "Administrator" Then
                        GoldQualityForTextChange()
                    End If
                    txtSellingRate.Text = .SellingRate
                    ' txtSellingAmt.Text = .SellingAmt
                Else
                    lblPercent.Text = ""
                    txtLength.Text = ""
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
                End If

                txtWhiteCharges.Text = Format(Val(grdDetail.CurrentRow.Cells("WhiteCharges").Value), "###,##0.##")
                txtPlatingCharges.Text = Format(Val(grdDetail.CurrentRow.Cells("PlatingCharges").Value), "###,##0.##")
                txtMountingCharges.Text = Format(Val(grdDetail.CurrentRow.Cells("MountingCharges").Value), "###,##0.##")
                txtDesignCharges.Text = Format(Val(grdDetail.CurrentRow.Cells("DesignCharges").Value), "###,##0.##")
                txtDesignRate.Text = Format(Val(grdDetail.CurrentRow.Cells("DesignChargesRate").Value), "###,##0.##")
                isFixPrice = .IsFixPrice
                _FixPrice = .FixPrice
                If .Photo <> "" Then
                    Try
                        lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + .Photo)
                        PName = .Photo
                        lblPhoto.Visible = False
                    Catch ex As Exception
                        lblItemImage.Image = Nothing
                        lblPhoto.Visible = True
                    End Try
                Else
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
            End With
            _IsSaleReturn = CBool(grdDetail.CurrentRow.Cells("IsSaleReturn").Value)

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
            txtItemTG.Text = Format(CDec(grdDetail.CurrentRow.Cells("ItemTG").Value), "0.000")
            txtItemTK.Text = Format(CDec(grdDetail.CurrentRow.Cells("ItemTK").Value), "0.000")
            _ItemTK = CDec(grdDetail.CurrentRow.Cells("ItemTK").Value)
            _ItemTG = CDec(grdDetail.CurrentRow.Cells("ItemTG").Value)
            lblItemGram.Text = Format(CDec(grdDetail.CurrentRow.Cells("ItemTG").Value), "0.00") + " gram"

            _WasteTK = CDec(grdDetail.CurrentRow.Cells("WasteTK").Value)
            GoldWeight.GoldTK = _WasteTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteK.Text = CStr(GoldWeight.WeightK)
            txtWasteP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtWasteTK.Text = Format(CDec(grdDetail.CurrentRow.Cells("WasteTK").Value), "0.000")
            txtWasteTG.Text = Format(CDec(grdDetail.CurrentRow.Cells("WasteTG").Value), "0.000")
            _WasteTK = CDec(grdDetail.CurrentRow.Cells("WasteTK").Value)
            _WasteTG = CDec(grdDetail.CurrentRow.Cells("WasteTG").Value)

            _GemsTK = CDec(grdDetail.CurrentRow.Cells("GemsTK").Value)
            GoldWeight.GoldTK = _GemsTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtGemKForSale.Text = CStr(GoldWeight.WeightK)
            txtGemPForSale.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtGemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtGemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If

            txtGemTKForSale.Text = Format(CDec(grdDetail.CurrentRow.Cells("GemsTK").Value), "0.000")
            txtGemTGForSale.Text = Format(CDec(grdDetail.CurrentRow.Cells("GemsTG").Value), "0.000")
            _GemsTK = CDec(grdDetail.CurrentRow.Cells("GemsTK").Value)
            _GemsTG = CDec(grdDetail.CurrentRow.Cells("GemsTG").Value)


            CalculateTotalWeight()
            CalculateGoldWeight()


            chkIsFixPrice.Checked = Format(Val(.CurrentRow.Cells("IsFixPrice").Value), "###,##0.##")


            txtCurrentPrice.Text = Format(Val(.CurrentRow.Cells("SalesRate").Value), "###,##0.##")

            txtGoldPrice.Text = Format(Val(.CurrentRow.Cells("GoldPrice").Value), "###,##0.##")
            If isFixPrice = False Then
                txtGold.Text = Format((Val(grdDetail.CurrentRow.Cells("GoldPrice").Value)) - (Val(grdDetail.CurrentRow.Cells("ItemTax").Value) + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)), "###,##0.##")
            Else
                txtGold.Text = Format(Val(.CurrentRow.Cells("GoldPrice").Value), "###,##0.##")
            End If
            txtGemsPrice.Text = Format(Val(.CurrentRow.Cells("GemsPrice").Value), "###,##0.##")

            txtTaxPer.Text = Format(.CurrentRow.Cells("ItemTaxPer").Value, "###,##0.##")
            txtTaxAmt.Text = Format(.CurrentRow.Cells("ItemTax").Value, "###,##0.##")
            txtTotalAmt.Text = Format(Val(.CurrentRow.Cells("TotalAmount").Value), "###,##0.##")
            txtTotalAmt.Text = Format(Val(.CurrentRow.Cells("TotalAmount").Value), "###,##0.##")
            txtAddSub.Text = Format(Val(.CurrentRow.Cells("AddOrSub").Value), "###,##0.##")
            txtNetAmt.Text = Format(Val(.CurrentRow.Cells("NetAmount").Value), "###,##0.##")

            IsOriginalFixedPrice = .CurrentRow.Cells("IsOriginalFixedPrice").Value
            _OriginalFixedPrice = .CurrentRow.Cells("OriginalFixedPrice").Value
            IsOriginalPriceGram = .CurrentRow.Cells("IsOriginalPriceGram").Value
            _OriginalPriceGram = .CurrentRow.Cells("OriginalPriceGram").Value
            _OriginalPriceTK = .CurrentRow.Cells("OriginalPriceTK").Value
            _OriginalGemsPrice = .CurrentRow.Cells("OriginalGemsPrice").Value
            _OriginalOtherPrice = .CurrentRow.Cells("OriginalOtherPrice").Value
            _PurchaseWasteTK = .CurrentRow.Cells("PurchaseWasteTK").Value
            _PurchaseWasteTG = .CurrentRow.Cells("PurchaseWasteTG").Value
            lblOriginalCode.Visible = True
            lblPriceCode.Visible = True
            lblOriginalCode.Text = IIf(IsDBNull(.CurrentRow.Cells("OriginalCode").Value), "", .CurrentRow.Cells("OriginalCode").Value)
            lblPriceCode.Text = IIf(IsDBNull(.CurrentRow.Cells("PriceCode").Value), "", .CurrentRow.Cells("PriceCode").Value)
            txtSellingRate.Text = Format(Val(grdDetail.CurrentRow.Cells("SellingRate").Value), "###,##0.##")
            txtSellingAmt.Text = Format(Val(grdDetail.CurrentRow.Cells("SellingAmt").Value), "###,##0.##")
        End With

        _dtSalesInvoiceItem.Rows.Clear()

        If _dtAllDiamond.Rows.Count Then

            For i As Integer = 0 To _dtAllDiamond.Rows.Count - 1
                If Not IsDBNull(_dtAllDiamond.Rows(i).Item("@SaleInvoiceDetailID")) Then

                    If _dtAllDiamond.Rows(i).Item("@SaleInvoiceDetailID") = _SalesInvoiceDetailID Then
                        Dim drItem As DataRow
                        drItem = _dtSalesInvoiceItem.NewRow
                        drItem("SalesInvoiceGemItemID") = _dtAllDiamond.Rows(i).Item("SalesInvoiceGemItemID")
                        drItem("@SaleInvoiceDetailID") = _dtAllDiamond.Rows(i).Item("@SaleInvoiceDetailID")
                        drItem("GemsCategory") = _dtAllDiamond.Rows(i).Item("GemsCategory")
                        drItem("@GemsCategoryID") = _dtAllDiamond.Rows(i).Item("@GemsCategoryID")
                        drItem("GemsName") = _dtAllDiamond.Rows(i).Item("GemsName")
                        drItem("GemsK") = _dtAllDiamond.Rows(i).Item("GemsK")
                        drItem("GemsP") = _dtAllDiamond.Rows(i).Item("GemsP")
                        drItem("GemsY") = _dtAllDiamond.Rows(i).Item("GemsY")
                        drItem("GemsTK") = _dtAllDiamond.Rows(i).Item("GemsTK")
                        drItem("GemsTG") = _dtAllDiamond.Rows(i).Item("GemsTG")
                        drItem("YOrCOrG") = _dtAllDiamond.Rows(i).Item("YOrCOrG")
                        drItem("GemsTW") = _dtAllDiamond.Rows(i).Item("GemsTW")
                        drItem("Qty") = _dtAllDiamond.Rows(i).Item("Qty")
                        drItem("UnitPrice") = _dtAllDiamond.Rows(i).Item("UnitPrice")
                        drItem("Type") = _dtAllDiamond.Rows(i).Item("Type")
                        drItem("Amount") = _dtAllDiamond.Rows(i).Item("Amount")
                        drItem("GemsRemark") = _dtAllDiamond.Rows(i).Item("GemsRemark")
                        drItem("GemTaxPer") = _dtAllDiamond.Rows(i).Item("GemTaxPer")
                        drItem("GemTax") = _dtAllDiamond.Rows(i).Item("GemTax")

                        _dtSalesInvoiceItem.Rows.Add(drItem)
                    End If
                End If

            Next
            grdSaleCategory.DataSource = _dtSalesInvoiceItem
        End If

        CalculategrdTotalAmount()

        btnAdd.Text = "Update"
        _IsUpdate = True

    End Sub


    Private Sub txtAllNetAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAllNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtAllNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAllNetAmt.TextChanged
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtSRTaxAmt.Text = "" Then txtSRTaxAmt.Text = "0"
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If txtPurchaseAmount.Text = "" Then txtPurchaseAmount.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
        If txtMemberDis.Text = "" Then txtMemberDis.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"

        
        If Global_IsCash = False Then
            txtPaidAmt.Text = CLng(txtPaidAmt.Text)
        Else
            txtPaidAmt.Text = Format(((CInt(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text)) - CInt(txtPaidAmt.Text)), "###,##0.##")
        End If
        Dim DiscountAmount As Integer
        If txtDiscountAmt.Text = "-" Then
            DiscountAmount = 0
        Else
            DiscountAmount = CLng(txtDiscountAmt.Text)
        End If

        If CInt(txtMemberDis.Text) > 0 Then
            txtMemberDisAmt.Text = CStr(CLng(txtAllTotalAmt.Text) * (CLng(txtMemberDis.Text) / 100))
        End If

        txtAllAddOrSub.Text = Format(Val((CLng(txtAllNetAmt.Text) + DiscountAmount + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + +CLng(txtValue.Text)) - CLng(txtAllTotalAmt.Text)), "###,##0.##")

        CalculateFinalAmount()

    End Sub

    Private Sub txtPromotionDis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPromotionDis.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtPromotionDis_TextChanged(sender As Object, e As EventArgs) Handles txtPromotionDis.TextChanged
        If txtPromotionDis.Text = "" Then txtPromotionDis.Text = "0"
        If txtAllTotalAmt.Text = "" Then txtAllTotalAmt.Text = "0"
        If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"
        If Global_IsCash = False Then
            txtPaidAmt.Text = "0"
        End If
        txtPromotionAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text)) * (CLng(txtPromotionDis.Text) / 100)), "###,##0.##")

        txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")


        CalculateFinalAmount()
    End Sub

    Private Sub txtPromotionAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPromotionAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub grdDetail_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdDetail.RowsRemoved

        Dim row As DataRow
        Dim j As Integer = _dtAllDiamond.Rows.Count() - 1
        While j >= 0
            row = _dtAllDiamond.Rows(j)
            If row.Item("@SaleInvoiceDetailID") = _SalesInvoiceDetailID Then
                _dtAllDiamond.Rows.Remove(row)
            End If
            j = j - 1
        End While

        For i As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(i).IsNewRow Then
                grdDetail.Rows(i).Cells("SNo").Value = i + 1
            End If
        Next

        txtBarcodeNo.Text = ""
        ClearItemCode()
        CalculategrAlldTotalAmount()
        CalculategrAlldTotalWeight()
    End Sub

    Private Sub SearchItem_Click(sender As Object, e As EventArgs) Handles SearchItem.Click
        Dim DataItem As DataRow
        Dim dtSale As New DataTable
        Dim objSItem As CommonInfo.SalesItemInfo
        Dim objCurrent As New CommonInfo.IntDiamondPriceRateInfo
        Dim _Carat As Decimal = 0.0
        Dim Amt As Long = 0

        dtSale = _SalesItemController.GetForSalesItemForSaleInvoice(GetExistedItems())
        DataItem = DirectCast(SearchData.FindFast(dtSale, "SalesItem List"), DataRow)
        If DataItem IsNot Nothing Then
            _ForSaleID = DataItem.Item("@ForSaleID").ToString()
            _BarcodeNo = DataItem.Item("ItemCode")
            objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo)
            '_dtItemData_His = _SalesItemController.GetForSaleDataByItemCode(_BarcodeNo)
            'Dim dc As New DataColumn
            '_dtItemData_His = New DataTable
            '_dtItemData_His.Columns.Add("ForSaleID", System.Type.GetType("System.String"))
            '_dtItemData_His.Columns.Add("ItemCode", System.Type.GetType("System.String"))
            '_dtItemData_His.Columns.Add("ItemTG", System.Type.GetType("System.Deicimal"))
            '_dtItemData_His.Columns.Add("WasteTG", System.Type.GetType("System.Deicimal"))


            ShowForSaleBarcodeData(objSItem)
            _dtSalesInvoiceItem = _SalesItemController.GetSalesItemGems(_ForSaleID)

            _dtSalesInvoiceItem = _SalesItemController.GetSalesItemGems(_ForSaleID)
            If _dtSalesInvoiceItem.Rows.Count > 0 Then
                For k As Integer = 0 To _dtSalesInvoiceItem.Rows.Count - 1
                    If _dtSalesInvoiceItem.Rows(k).Item("SaleByDefinePrice") = True Then
                        _Carat = Format(CDec(_dtSalesInvoiceItem.Rows(k).Item("GemsTG") * Global_GramToKarat), 0.0)
                        objCurrent = _CurrentCon.GetIntDiamondData(_Carat)
                        With objCurrent
                            _dtSalesInvoiceItem.Rows(k).Item("UnitPrice") = .PriceRate
                            Amt = CLng(_Carat * .PriceRate)
                            If _dtSalesInvoiceItem.Rows(k).Item("Type") = "Fix" Then
                                _dtSalesInvoiceItem.Rows(k).Item("Amount") = _dtSalesInvoiceItem.Rows(k).Item("UnitPrice")
                            ElseIf _dtSalesInvoiceItem.Rows(k).Item("Type") = "ByQty" Then

                                _dtSalesInvoiceItem.Rows(k).Item("Amount") = .PriceRate * _dtSalesInvoiceItem.Rows(k).Item("Qty")
                            Else
                                _dtSalesInvoiceItem.Rows(k).Item("Amount") = CLng(Amt + (Amt * (_dtSalesInvoiceItem.Rows(k).Item("GemTaxPer")) / 100))
                            End If

                        End With
                    End If

                Next
            End If

            grdSaleCategory.DataSource = _dtSalesInvoiceItem
            CalculategrdTotalAmount()
            CalculateTotalAmount()
            txtBarcodeNo.Select()
            _OldTotalItemTG = CDec(DataItem.Item("GoldTG").ToString)
            _OldTotalWasteTG = CDec(DataItem.Item("WasteTG").ToString)
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

    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If grdSaleCategory.CurrentCell Is grdSaleCategory.CurrentRow.Cells("UnitPrice") Then
            If IsDBNull(grdSaleCategory.CurrentRow.Cells("UnitPrice").FormattedValue) = False Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If

        End If


    End Sub

    Private Sub grdGems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSaleCategory.EditingControlShowing
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
        openhelp("SaleItemInvoice")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub txtCurrentPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtDesignRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesignRate.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtTaxAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTaxAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtDesignRate_TextChanged(sender As Object, e As EventArgs) Handles txtDesignRate.TextChanged
        Dim _DesignCharges As Integer = 0
        If _IsGram = True Then
            _DesignCharges = CStr(CLng(txtDesignRate.Text) * (CDec(txtGoldTG.Text) + CDec(txtWasteTG.Text)))
        End If
        txtDesignCharges.Text = Format(_DesignCharges, "###,##0.##")
    End Sub
    Private Sub txtCurrentPrice_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentPrice.TextChanged
        Dim TempTK As Decimal = 0.0
        Dim Gold As New CommonInfo.GoldWeightInfo
        Dim _GoldPrice As Integer = 0
        Dim _TotalGoldPrice As Integer = 0
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtTaxPer.Text = "" Then txtTaxPer.Text = "0"

        If (_WasteTG > 0 Or _GoldTG > 0) And isFixPrice = False Then
            Gold.WeightK = CInt(txtGoldK.Text) + CInt(txtWasteK.Text)
            Gold.WeightP = CInt(txtGoldP.Text) + CInt(txtWasteP.Text)
            Gold.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtWasteY.Text))
            Gold.WeightC = (CDec(txtGoldY.Text) + CDec(txtWasteY.Text)) - Gold.WeightY
            Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
            TempTK = Gold.GoldTK
            If _IsGram = False Then
                _GoldPrice = CStr((CLng(txtCurrentPrice.Text) * TempTK))
            Else
                _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * (CDec(txtGoldTG.Text) + CDec(txtWasteTG.Text)))
            End If

            txtGold.Text = Format(_GoldPrice, "###,##0.##")
            _TotalGoldPrice = _GoldPrice + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
            txtTaxAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtTaxPer.Text)) / 100), "###,##0.##")
            txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
        End If

        CalculateTotalAmount()

    End Sub

    Private Sub txtPurchaseVoucherNo_TextChanged(sender As Object, e As EventArgs) Handles txtPurchaseVoucherNo.TextChanged


        Dim ObjPurchase As New CommonInfo.PurchaseHeaderInfo
        If txtPurchaseVoucherNo.Text <> "" Then
            _PurchaseHeaderID = txtPurchaseVoucherNo.Text.Trim
            ObjPurchase = _ObjPurchaseController.GetPurchaseHeaderDataBySaleInvoice(_PurchaseHeaderID, _SalesInvoiceID, "SaleInvoice")
            If ObjPurchase.PurchaseHeaderID <> "" Then
                txtPurchaseAmount.Text = Format(Val(ObjPurchase.AllNetAmount), "###,##0.##")
            Else
                _PurchaseHeaderID = ""
                txtPurchaseAmount.Text = "0"
                txtDifferentAmount.Text = "0"
                'txtSRTaxPer.Text = "0"
                txtSRTaxAmt.Text = "0"
                txtSRTaxPer.Enabled = True
                txtSRTaxAmt.Enabled = True
                lblPer.Enabled = True
                lblSRTax.Enabled = True
            End If
        Else

            _PurchaseHeaderID = ""
            txtPurchaseAmount.Text = "0"
            txtDifferentAmount.Text = "0"
            'txtSRTaxPer.Text = "0"
            txtSRTaxAmt.Text = "0"
            txtSRTaxPer.Enabled = True
            txtSRTaxAmt.Enabled = True
            lblPer.Enabled = True
            lblSRTax.Enabled = True
        End If
        If Global_IsCash = False Then
            txtPaidAmt.Text = "0"
        End If
        'CalculateSRTaxAmt()
        CalculateFinalAmount()
    End Sub

    Private Sub btnSearchPurchase_Click(sender As Object, e As EventArgs) Handles btnSearchPurchase.Click

        txtSRTaxPer.Enabled = True
        txtSRTaxAmt.Enabled = True
        lblPer.Enabled = True
        lblSRTax.Enabled = True

        Dim DataItem As DataRow
        Dim dtPurchase As New DataTable
        dtPurchase = _ObjPurchaseController.GetAllPuchaseHeaderDataBySaleInvoice(_SalesInvoiceID, "SaleInvoice")
        DataItem = DirectCast(SearchData.FindFast(dtPurchase, "PurchaseHeader List"), DataRow)
        If DataItem IsNot Nothing Then
            _PurchaseHeaderID = DataItem.Item("VoucherNo").ToString()
            txtPurchaseVoucherNo.Text = _PurchaseHeaderID
            txtPurchaseAmount.Text = Format(Val(DataItem.Item("AllNetAmount")), "###,##0.##")
            If Global_IsCash = True Then
                txtPaidAmt.Text = "0"
            End If
            CalculateFinalAmount()
        End If
    End Sub

    Private Sub txtPurchaseAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPurchaseAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPurchaseAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPurchaseAmount.TextChanged
        CalculateFinalAmount()
    End Sub

    Private Sub chkAdvance_CheckedChanged(sender As Object, e As EventArgs) Handles chkAdvance.CheckedChanged
        If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"

        If chkAdvance.Checked Then
            txtAdvanceAmt.Visible = True
            dtpAdvanceDate.Visible = True

        Else
            txtAdvanceAmt.Text = "0"
            dtpAdvanceDate.Value = Now
            txtAdvanceAmt.Visible = False
            dtpAdvanceDate.Visible = False

        End If

        CalculateFinalAmount()
    End Sub

    Private Sub txtAdvanceAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAdvanceAmt.TextChanged
        If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtAdvanceAmt.Text = "" Then txtAdvanceAmt.Text = "0"


        CalculateFinalAmount()
    End Sub
    Private Sub txtAdvanceAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAdvanceAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
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
        SaleItemInvoiceGenerateFormat()
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

    Private Sub txtNetAmt_Validated(sender As Object, e As EventArgs) Handles txtNetAmt.Validated
        txtNetAmt.Text = Format(Val(CInt(txtNetAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtDiscountAmt_Validated(sender As Object, e As EventArgs) Handles txtDiscountAmt.Validated
        txtDiscountAmt.Text = Format(Val(CInt(txtDiscountAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtAllAddOrSub_Validated(sender As Object, e As EventArgs) Handles txtAllAddOrSub.Validated
        txtAllAddOrSub.Text = Format(Val(CInt(txtAllAddOrSub.Text)), "###,##0.##")
    End Sub

    Private Sub txtAdvanceAmt_Validated(sender As Object, e As EventArgs) Handles txtAdvanceAmt.Validated
        txtAdvanceAmt.Text = Format(Val(CInt(txtAdvanceAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtPaidAmt_Validated(sender As Object, e As EventArgs) Handles txtPaidAmt.Validated
        txtPaidAmt.Text = Format(Val(CInt(txtPaidAmt.Text)), "###,##0.##")
    End Sub
    Private Sub txtTaxPer_Validated(sender As Object, e As EventArgs) Handles txtPaidAmt.Validated
        txtTaxPer.Text = Format(Val(txtTaxPer.Text), "###,##0.##")
    End Sub

    Private Sub txtTaxAmt_Validated(sender As Object, e As EventArgs) Handles txtTaxAmt.Validated
        txtTaxAmt.Text = Format(Val(CInt(txtTaxAmt.Text)), "###,##0.##")
    End Sub

    Private Sub btnEntry_Click(sender As Object, e As EventArgs) Handles btnEntry.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objSaleItemHeader As New SaleInvoiceHeaderInfo

        dt = objSalesInvoiceController.GetAllSalesInvoiceDataByItemCode()
        DataItem = DirectCast(SearchData.FindFast(dt, "Sale Invoice Item List"), DataRow)

        If DataItem IsNot Nothing Then
            _IsGemInDB = True
            _SalesInvoiceID = DataItem.Item("VoucherNo").ToString()
            objSaleItemHeader = objSalesInvoiceController.GetSaleInvoiceHeaderByID(_SalesInvoiceID)

            _dtItemBarcode.Rows.Clear()
            _dtSalesInvoiceItem.Rows.Clear()
            _dtAllDiamond.Rows.Clear()

            _dtItemBarcode = objSalesInvoiceController.GetSalesInvoiceDetailByID(_SalesInvoiceID)
            grdDetail.DataSource = _dtItemBarcode

            For j As Integer = 0 To grdDetail.RowCount - 1
                If Not grdDetail.Rows(j).IsNewRow Then
                    If grdDetail.Rows(j).Cells("SaleInvoiceDetailID").Value = DataItem.Item("@SaleInvoiceDetailID") Then
                        grdDetail.Rows(j).Selected = True
                        Exit For
                    Else
                        grdDetail.Rows(j).Selected = False
                    End If
                End If
            Next

            _dtAllDiamond = objSalesInvoiceController.GetSaleInvoiceDetailGemByHeaderID(_SalesInvoiceID)
            Dim dtTestStone1 As New DataTable
            dtTestStone1 = _dtAllDiamond
            CalculategrAlldTotalWeight()
            _dtOtherCash = objSalesInvoiceController.GetOtherCashDataByVoucherNo(_SalesInvoiceID)
            ShowSaleDiaBarcodeData(objSaleItemHeader)
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
            btnDelete.Enabled = True
        Else
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString
        End If
    End Sub
    Private Sub txtBarcodeNo_Validated(sender As Object, e As EventArgs) Handles txtBarcodeNo.Validated
        Dim dtSaleItem As New DataTable
        Dim objSItem As SalesItemInfo

        If txtBarcodeNo.Text <> "" And _ForSaleID Is Nothing Then
            _BarcodeNo = txtBarcodeNo.Text
            objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo, "")

            If IsNothing(objSItem.ForSaleID) Then
                MsgBox("Invalid Barcode!", MsgBoxStyle.Information, AppName)
                txtBarcodeNo.Text = ""
                txtBarcodeNo.Focus()
            ElseIf objSItem.IsExit = True Then
                MsgBox("Barcode is Sold Out!", MsgBoxStyle.Information, AppName)
                txtBarcodeNo.Text = ""
                txtBarcodeNo.Focus()
            End If
        End If
    End Sub
    Private Sub txtTaxPer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTaxPer.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub


    Public Sub CalculateSRTaxAmt()

        If txtPurchaseVoucherNo.Text = "" Then
            If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"
            If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
            If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
            If txtAdvanceAmt.Text = "" Then txtAdvanceAmt.Text = "0"
            '  If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
            If txtAllAddOrSub.Text = "" Then txtAllAddOrSub.Text = "0"
            If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
            If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
            If txtValue.Text = "" Then txtValue.Text = "0"

            If Global_IsCash = False Then
                txtPaidAmt.Text = "0"
            End If

            txtSRTaxAmt.Text = Format(CInt(CInt(txtAllNetAmt.Text) * CDec(txtSRTaxPer.Text) / 100), "###,##0.##")
            If chkAdvance.Checked Then
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(CInt(txtAllTotalAmt.Text) + CInt(txtSRTaxAmt.Text), "###,##0.##")
                End If
                txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")

                txtBalanceAmt.Text = Format(CInt(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CInt(txtPaidAmt.Text), "###,##0.##")

            Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(CInt(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text), "###,##0.##")
                End If
                txtAllNetAmt.Text = Format(Val((CLng(txtAllTotalAmt.Text) + CLng(txtAllAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
                txtBalanceAmt.Text = Format(CInt(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CInt(txtPaidAmt.Text), "###,##0.##")
            End If

        Else
            If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"
            If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
            If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
            If txtAdvanceAmt.Text = "" Then txtAdvanceAmt.Text = "0"
            ' If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
            If Global_IsCash = False Then
                txtPaidAmt.Text = "0"
            End If

            txtSRTaxAmt.Text = Format(CInt(CInt(txtDifferentAmount.Text) * CDec(txtSRTaxPer.Text) / 100), "###,##0.##")
            If chkAdvance.Checked Then
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
            Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If

        End If


    End Sub
    Private Sub txtSRTaxPer_TextChanged(sender As Object, e As EventArgs) Handles txtSRTaxPer.TextChanged
        If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"
        CalculateFinalAmount()
    End Sub

    Private Sub txtSRTaxAmt_TextChanged(sender As Object, e As EventArgs) Handles txtSRTaxAmt.TextChanged
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        If txtPurchaseAmount.Text = "" Then txtPurchaseAmount.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
        If txtSRTaxPer.Text = "" Then txtSRTaxPer.Text = "0"

        If _PurchaseHeaderID <> "" Then

            If chkAdvance.Checked Then
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CLng(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
            Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text) + CInt(txtSRTaxAmt.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If
        Else
            If chkAdvance.Checked Then
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text) + CLng(txtAdvanceAmt.Text))), "###,##0.##")
            Else
                If Global_IsCash = True Then
                    txtPaidAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
                End If
                txtBalanceAmt.Text = Format(Val(CLng(txtAllNetAmt.Text) + CInt(txtSRTaxAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If
        End If
    End Sub
    Public Sub CalculateTaxAmt()
        Dim _TotalGoldPrice As Integer = 0
        If txtTaxPer.Text = "" Then txtTaxPer.Text = "0.0"
        If txtGold.Text = "" Then txtGold.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"


        If isFixPrice = False Then

            _TotalGoldPrice = CInt(txtGold.Text) + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
            txtTaxAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtTaxPer.Text)) / 100), "###,##0.##")
            txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
            CalculateTotalAmount()
        Else
            txtTaxAmt.Text = Format(CInt((_FixPrice * CDec(txtTaxPer.Text)) / 100), "###,##0.##")
            txtTotalAmt.Text = Format(_FixPrice + CInt(txtTaxAmt.Text), "###,##0.##")
            txtNetAmt.Text = Format(CLng(txtTotalAmt.Text), "###,##0.##")
            txtAddSub.Text = "0"
        End If

    End Sub
    Public Sub CalculateSellingAmt()
        Dim _TotalGoldPrice As Integer = 0
        If txtSellingRate.Text = "" Then txtSellingRate.Text = "0.0"
        If txtGold.Text = "" Then txtGold.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"

        If Global_GIsFixPrice = False Then
            If isFixPrice = False Then

                _TotalGoldPrice = CInt(txtGold.Text) + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
                txtSellingAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtSellingRate.Text)) / 100), "###,##0.##")
                'txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
                txtSellingAmt.Enabled = True
                txtSellingRate.Enabled = True
                CalculateTotalAmount()
            Else
                txtSellingAmt.Text = Format(CInt((_FixPrice * CDec(txtSellingRate.Text)) / 100), "###,##0.##")
                txtTotalAmt.Text = Format(_FixPrice + CInt(txtSellingAmt.Text) + CInt(txtTaxAmt.Text), "###,##0.##")
                txtNetAmt.Text = Format(CLng(txtTotalAmt.Text), "###,##0.##")
                txtSellingAmt.Enabled = True
                txtSellingRate.Enabled = True
                txtAddSub.Text = "0"
            End If
        Else
            txtSellingAmt.Text = 0
            txtSellingAmt.Enabled = False
            txtSellingRate.Enabled = False
        End If
    End Sub
    Private Sub txtSellingRate_TextChanged(sender As Object, e As EventArgs) Handles txtSellingRate.TextChanged
        If txtSellingRate.Text = "" Then txtSellingRate.Text = "0"
        CalculateSellingAmt()
    End Sub
    Private Sub txtTaxPer_TextChanged(sender As Object, e As EventArgs) Handles txtTaxPer.TextChanged
        If txtTaxPer.Text = "" Then txtTaxPer.Text = "0"
        CalculateTaxAmt()
    End Sub
    Private Sub txtTaxAmt_TextChanged(sender As Object, e As EventArgs) Handles txtTaxAmt.TextChanged
        Dim _TotalGoldPrice As Integer = 0

        If txtGold.Text = "" Then txtGold.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If isFixPrice = False Then
            _TotalGoldPrice = CInt(txtGold.Text) + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
            txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
            CalculateTotalAmount()
        Else
            txtTotalAmt.Text = Format(_FixPrice + CInt(txtTaxAmt.Text) + CInt(txtSellingAmt.Text), "###,##0.##")
            txtNetAmt.Text = Format(CLng(txtTotalAmt.Text), "###,##0.##")
            txtAddSub.Text = "0"
        End If
    End Sub
    Private Sub txtSellingAmt_TextChanged(sender As Object, e As EventArgs) Handles txtSellingAmt.TextChanged
        Dim _TotalGoldPrice As Integer = 0

        If txtGold.Text = "" Then txtGold.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If isFixPrice = False Then
            _TotalGoldPrice = CInt(txtGold.Text) + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
            txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
            CalculateTotalAmount()
        Else
            txtTotalAmt.Text = Format(_FixPrice + CInt(txtTaxAmt.Text) + CInt(txtSellingAmt.Text), "###,##0.##")
            txtNetAmt.Text = Format(CLng(txtTotalAmt.Text), "###,##0.##")
            txtAddSub.Text = "0"
        End If
    End Sub
    Private Sub CalculateGoldAmount()
        Dim _TotalGoldPrice As Integer = 0
        If txtGold.Text = "" Then txtGold.Text = "0"
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If txtTaxAmt.Text = "" Then txtTaxAmt.Text = "0"
        If txtTaxPer.Text = "" Then txtTaxPer.Text = "0"
        If txtSellingRate.Text = "" Then txtSellingRate.Text = "0"

        If isFixPrice = False Then
            _TotalGoldPrice = CInt(txtGold.Text) + CInt(txtWhiteCharges.Text) + CInt(txtPlatingCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtDesignCharges.Text)
            txtTaxAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtTaxPer.Text)) / 100), "###,##0.##")
            If Global_GIsFixPrice = False Then
                txtSellingAmt.Text = Format(CInt((_TotalGoldPrice * CDec(txtSellingRate.Text)) / 100), "###,##0.##")
            Else
                txtSellingAmt.Text = 0
            End If
            txtGoldPrice.Text = Format(_TotalGoldPrice + CInt(txtTaxAmt.Text), "###,##0.##")
            CalculateTotalAmount()
        End If
    End Sub

    Private Sub txtWhiteCharges_TextChanged(sender As Object, e As EventArgs) Handles txtWhiteCharges.TextChanged
        CalculateGoldAmount()
    End Sub

    Private Sub txtPlatingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtPlatingCharges.TextChanged
        CalculateGoldAmount()
    End Sub

    Private Sub txtMountingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtMountingCharges.TextChanged
        CalculateGoldAmount()
    End Sub

    Private Sub txtDesignCharges_TextChanged(sender As Object, e As EventArgs) Handles txtDesignCharges.TextChanged
        CalculateGoldAmount()
    End Sub

    Private Sub txtWhiteCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWhiteCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtPlatingCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlatingCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtMountingCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMountingCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtDesignCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesignCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtWhiteCharges_Validated(sender As Object, e As EventArgs) Handles txtWhiteCharges.Validated
        txtWhiteCharges.Text = Format(Val(CInt(txtWhiteCharges.Text)), "###,##0.##")
    End Sub

    Private Sub txtPlatingCharges_Validated(sender As Object, e As EventArgs) Handles txtPlatingCharges.Validated
        txtPlatingCharges.Text = Format(Val(CInt(txtPlatingCharges.Text)), "###,##0.##")
    End Sub

    Private Sub txtMountingCharges_Validated(sender As Object, e As EventArgs) Handles txtMountingCharges.Validated
        txtMountingCharges.Text = Format(Val(CInt(txtMountingCharges.Text)), "###,##0.##")
    End Sub

    Private Sub txtDesignCharges_Validated(sender As Object, e As EventArgs) Handles txtDesignCharges.Validated
        txtDesignCharges.Text = Format(Val(CInt(txtDesignCharges.Text)), "###,##0.##")
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

        'dtRedeemInfo = Await WebService.MemberCard.GetRedeemInfoByMemberCode(MCode, Global_CompanyName, Token)

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
                _IsMaximum = False
                Return False
                Exit Function
            End If
        ElseIf _IsUpdateHeader = True Then
            If ((Val((TopupPoint)) + Val(PointBalance) - Val(RedeemPoint - _OldRedeemTopupPoint)) > Val(MaxPoint)) Then

                MsgBox("You Reach Maximum Point.Please Redeem it!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                _IsMaximum = False
                Return False
                Exit Function
            End If
        ElseIf IsRedeemInvoice = False Then
            If ((Val((TopupPoint)) + Val(PointBalance) - Val(RedeemPoint)) > Val(MaxPoint)) Then

                MsgBox("You Reach Maximum Point.Please Redeem it!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                _IsMaximum = False
                Return False
                Exit Function
            End If
        Else
            If ((Val((TopupPoint)) + Val(PointBalance) - Val(RedeemPoint)) > Val(MaxPoint)) Then

                MsgBox("You Reach Maximum Point.Please Redeem it!", MsgBoxStyle.Information, AppName)
                RedeemPoint = 0
                txtPoint.Text = RedeemPoint
                RedeemValue = 0
                _IsMaximum = False
                Return False
                Exit Function
            End If
        End If

        _IsMaximum = True
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
                RedeemBool = objSalesInvoiceController.UpdateRedeemID(RedeemID, InvoiceID)
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



    Private Async Sub SaveSaleMemberCard(ByVal objSaleInvoice As CommonInfo.SaleInvoiceHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean
        'Dim Result As String

        'Dim Result As String
        '  RegName + MemberCode + GwtMember@2020

        'Dim s As String = "GWT1234501320000002GwtMember@2020"
        'Dim s1 As String = Convert.ToBase64String(Encoding.Unicode.GetBytes(s))
        'Dim s2 As String = Encoding.Unicode.GetString(Convert.FromBase64String(s1))

        'Result = Await WebService.MemberCard.SaveSaleMemberCard(objSaleInvoice, Status, Global_CompanyName)
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

    Private Async Sub UpdateRedeem(ByVal objSaleInvoice As CommonInfo.SaleInvoiceHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean

        ' Result = Await WebService.MemberCard.UpdateRedeem(objSaleInvoice, Status)
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
    Private Async Sub AddRedeem(ByVal objSaleInvoice As CommonInfo.SaleInvoiceHeaderInfo, ByVal Status As Integer)
        Dim Result As Boolean


        ' Result = Await WebService.MemberCard.AddRedeem(objSaleInvoice, Status)
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
End Class


