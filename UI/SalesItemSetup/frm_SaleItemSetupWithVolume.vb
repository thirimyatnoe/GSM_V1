Imports BusinessRule
Imports CommonInfo
Imports System.Decimal


Public Class frm_SaleItemSetupWithVolume
    Implements IFormProcess
    Dim OpenFileDia As New OpenFileDialog
    Dim PName As String
    Dim DefaultPhoto As String
    Private _OrderReceiveDetailID As String
    Private _ForSaleID As String
    Private _IsUpdate As Boolean = False
    Private _GoldTK As Decimal
    Private _GoldTG As Decimal
    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    Private _TotalGemsTK As Decimal
    Private _TotalGemsTG As Decimal
    Private _WasteTK As Decimal
    Private _WasteTG As Decimal
    Private _PWasteTK As Decimal
    Private _PWasteTG As Decimal
    Private _ItemTK As Decimal
    Private _ItemTG As Decimal
    Private _TotalTK As Decimal
    Private _TotalTG As Decimal
    Private _TotalNoWasteTK As Decimal
    Private _TotalNoWasteTG As Decimal
    Private _IsExit As Boolean = False
    Private _dtForSaleGems As DataTable
    Private _VItemTK As Decimal
    Private _VItemTG As Decimal
    Private _GrdGemPrice As Integer = 0
    Private _IsPurchase As Boolean = False
    Private ItemCategoryPrefix As String = ""
    Private _IsGram As Boolean = False
    Private _VIsGram As Boolean = False
    Private _Prefix As String = ""
    Private _VPrefix As String = ""
    Private _DPrefix As String = ""
    Private _GrdGemQTY As Integer = 0
    Private _Totalct As Decimal = 0
    Private _OldOrderReceiveDetailID As String = ""
    Private _OldIsOrder As Boolean = False
    Private _LocationID As String = ""
    Private _GetItemCode As String = ""
    Private itemid As String = ""
    Private vitemid As String = ""
    Private ditemid As String = ""
    Private VPName As String
    Private DPName As String
    Private _OldDate As Date
    Private _GoldPrice As Integer = 0
    Private _OrgGoldPrice As Integer = 0
    Private _Color As String = ""
    Private _DItemTG As Decimal
    Private _DItemTK As Decimal
    Private _SDGemsTW As Decimal
    Private _DRBP As String
    Private _DCarat As String
    Dim numberformat As Integer
    Dim _Carat As Decimal = 0.0
    Dim _ItemNameID As String = ""
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _ConverterCon As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _SalesItemCon As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _GoldQCon As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCat As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _Supplier As Supplier.ISupplierController = Factory.Instance.CreateSupplierController
    Private _GoldSmith As GoldSmith.IGoldSmithController = Factory.Instance.CreateGoldSmithController
    Private _LocationCon As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ItemNameCon As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _GemsCat As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _OrderInvoiceController As OrderInvoice.IOrderInvoiceController = Factory.Instance.CreateOrderInvoiceController
    Private _StaffCon As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _SalesVolumeCon As SalesVolume.ISalesVolumeController = Factory.Instance.CreateSalesVolumeController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private _WasteSetup As WasteSetup.IWasteSetupController = Factory.Instance.CreateWasteSetup
    Private _GetKeyword As BusinessRule.Keyword.IKeywordController = Factory.Instance.CreateKeyWordController
    Private _CurrentController As InternationalDiamond.IIntDiamondPriceRateController = Factory.Instance.CreateIntDiamondController
    Dim _WeightType As String = ""
    Dim frm As New frm_ToWeight
    Dim H_obj As New CommonInfo.SalesItemInfo

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

#Region "Implements"

    Private Sub frm_SalesItemSetup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "ရောင်းရန်ပစ္စည်းသတ်မှတ်ခြင်း"

        lblLogInUserName.Text = Global_CurrentUser
        chkisNormal.Checked = True
        cboItemCategory.Enabled = True
        cboItemName.Enabled = True
        cboSupplier.Enabled = True
        cboVItemCategory.Enabled = True
        cboVItemName.Enabled = True
        numberformat = Global_DecimalFormat

        If IsDBNull(Global_CurrentLocationID) Or Global_CurrentLocationID Is Nothing Then
            MsgBox("Please Define Location Setup First", MsgBoxStyle.Information, "TMS")
            Dim frm As New frm_Location
            frm.ShowDialog()
        Else
            _LocationID = Global_CurrentLocationID
        End If

        GetLocationAndGoldQCombo()
        GetLocationAndGoldQComboByVolume()
        GetItemCategory()
        GetDItemCategory()
        GetItemCategoryByVolume()
        GetStaffCombo()
        LoadCombos()
        LoadShapeCombos()
        LoadColorCombos()
        LoadClarityCombos()
        GetSupplier()
        GetDSupplier()
        GetGoldSmith()
        AllClear()
        cboStaff.Focus()
        chkByFixedPrice.Checked = Global_GIsFixPrice
    End Sub


    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If _SalesItemCon.DeleteForSale(txtStockID.Text) Then
            AllClear()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        AllClear()
    End Function



    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objForSale As New CommonInfo.SalesItemInfo
        Dim objItemCat As New CommonInfo.ItemCategoryInfo
        Dim ObjGemsCat As New CommonInfo.GemsCategoryInfo
        Dim Prefix As String = ""


        If IsFillData() Then
            objForSale = GetDataForSale()
            If chkIsVolume.Checked Then
                objItemCat = _ItemCat.GetItemCategory(cboVItemCategory.SelectedValue)
            ElseIf chkIsLooseDiamond.Checked Then
                ObjGemsCat = _GemsCat.GetGemsCategory(cboDCategory.SelectedValue)
                objItemCat.Prefix = ObjGemsCat.Prefix
            Else
                objItemCat = _ItemCat.GetItemCategory(cboItemCategory.SelectedValue)
            End If

            If _SalesItemCon.SaveForSaleHeader(H_obj, objForSale, objItemCat, _dtForSaleGems, _GetItemCode) Then
                If chkIsVolume.Checked Then
                    If MsgBox("ItemCode  " & txtVItemCode.Text & " is successlly save.Do u want to see this ItemCode? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        ShowVolumeBarcodeData(txtVItemCode.Text)
                    End If
                ElseIf chkIsLooseDiamond.Checked Then
                    If MsgBox("ItemCode  " & txtDBarcode.Text & " is successlly save.Do u want to see this ItemCode? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        ShowDiamondBarcodeData(txtDBarcode.Text)
                    End If
                Else
                    If MsgBox("ItemCode  " & txtItemCode.Text & " is successlly save.Do u want to see this ItemCode? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        ShowBarcodeData(txtItemCode.Text, txtTotalTG.Text, txtLength.Text, Prefix)
                    End If
                End If


                If Global_IsSpeedEntry = True Then
                    SpeedEntryClear()
                    'txtOriginalCode.Focus()
                Else
                    ItemClear()
                    txtOriginalCode.Focus()
                End If
                '
                Return True

            Else
                Return False
            End If

        End If
    End Function

#End Region

#Region "Private Sub"
    Private Sub chkChangedForOriginal()
        If chkIsOriginalFixedPrice.Checked = False Then
            txtOriginalFixedPrice.Text = "0"
            txtOriginalFixedPrice.Enabled = False
        Else
            txtOriginalFixedPrice.Text = "0"
            txtOriginalFixedPrice.Enabled = True
        End If

        If chkIsOriginalPriceGram.Checked = False Then
            txtOriginalPriceGram.Text = "0"
            txtOriginalPriceTK.Text = "0"
            txtOriginalGemsPrice.Text = "0"
            txtOriginalOtherPrice.Text = "0"
        Else
            txtOriginalPriceGram.Text = "0"
            txtOriginalPriceTK.Text = "0"
            txtOriginalGemsPrice.Text = "0"
            txtOriginalOtherPrice.Text = "0"
        End If
    End Sub

    Private Sub ShowPhoto()
        ''** Show Item Photo
        Dim ItemNameInfo As CommonInfo.ItemNameInfo
        ItemNameInfo = _ItemNameCon.GetItemPhoto(cboItemName.SelectedValue)
        Dim bytBLOBData As Byte() = IIf(IsDBNull(ItemNameInfo.ItemPhoto), Nothing, ItemNameInfo.ItemPhoto)

        If Not IsNothing(bytBLOBData) Then
            Dim ms As New IO.MemoryStream(bytBLOBData)
            lblItemImage.Image = System.Drawing.Image.FromStream(ms)
            lblPhoto.Visible = False

        Else
            lblItemImage.Image = Nothing
            lblPhoto.Visible = True

        End If

    End Sub
    Private Sub GetLocationAndGoldQCombo()
        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DataSource = _GoldQCon.GetAllGoldQuality().DefaultView
        cboGoldQuality.SelectedIndex = -1
    End Sub

    Private Sub GetLocationAndGoldQComboByVolume()

        cboVGoldQuality.DisplayMember = "GoldQuality"
        cboVGoldQuality.ValueMember = "@GoldQualityID"
        cboVGoldQuality.DataSource = _GoldQCon.GetAllGoldQuality().DefaultView
        cboVGoldQuality.SelectedIndex = -1
    End Sub

    Private Sub GetSupplier()
        cboSupplier.DisplayMember = "SupplierName_"
        cboSupplier.ValueMember = "@SupplierID"
        cboSupplier.DataSource = _Supplier.GetAllSupplierList().DefaultView
        cboSupplier.SelectedIndex = -1
    End Sub
    Private Sub GetDSupplier()
        cboDSupplier.DisplayMember = "SupplierName_"
        cboDSupplier.ValueMember = "@SupplierID"
        cboDSupplier.DataSource = _Supplier.GetAllSupplierList().DefaultView
        cboDSupplier.SelectedIndex = -1
    End Sub
    Private Sub GetGoldSmith()
        cboGoldSmith.DisplayMember = "Name_"
        cboGoldSmith.ValueMember = "@GoldSmithID"
        cboGoldSmith.DataSource = _GoldSmith.GetAllGoldSmithList().DefaultView
        'cboGoldSmith.SelectedValue = "DEFAULT"

    End Sub
    Private Sub GetItemCategory()
        cboItemCategory.DisplayMember = "ItemCategory_"
        cboItemCategory.ValueMember = "@ItemCategoryID"
        cboItemCategory.DataSource = _ItemCat.GetAllItemCategory().DefaultView
        cboItemCategory.SelectedIndex = -1
    End Sub
    Private Sub GetDItemCategory()
        cboDCategory.DisplayMember = "GemsCategory"
        cboDCategory.ValueMember = "@GemsCategoryID"
        cboDCategory.DataSource = _GemsCat.GetAllGemsCategoryForGridCombo().DefaultView
        cboDCategory.SelectedIndex = -1
    End Sub

    Private Sub GetItemCategoryByVolume()
        cboVItemCategory.DisplayMember = "ItemCategory_"
        cboVItemCategory.ValueMember = "@ItemCategoryID"
        cboVItemCategory.DataSource = _ItemCat.GetAllItemCategory().DefaultView
        cboVItemCategory.SelectedIndex = -1
    End Sub

    Private Sub GetStaffCombo()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _StaffCon.GetStaffList().DefaultView
    End Sub

    Private Sub RefreshItemNameCbo(ByVal ItemID As String)
        Dim dt As New DataTable
        dt = _ItemNameCon.GetItemNameListByItemCategory(ItemID)
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
            ' cboItemName.SelectedIndex = -1
        End If
    End Sub

    Private Sub SaleItemCodeGenerateFormat()
        _GetItemCode = ""
        Dim ItemCatInfo As New CommonInfo.ItemCategoryInfo
        Dim GemsItemCatInfo As New CommonInfo.GemsCategoryInfo

        If chkIsVolume.Checked Then
            ItemCatInfo = _ItemCat.GetItemCategory(cboVItemCategory.SelectedValue)
            ItemCategoryPrefix = ItemCatInfo.Prefix
        ElseIf chkIsLooseDiamond.Checked Then
            GemsItemCatInfo = _GemsCat.GetGemsCategory(cboDCategory.SelectedValue)
            ItemCategoryPrefix = GemsItemCatInfo.Prefix
        Else
            ItemCatInfo = _ItemCat.GetItemCategory(cboItemCategory.SelectedValue)
            ItemCategoryPrefix = ItemCatInfo.Prefix
        End If


        If chkIsVolume.Checked Then
            If cboVItemCategory.SelectedValue IsNot Nothing Then

                If cboVItemCategory.SelectedValue <> "-1" Then

                    Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

                    objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.Barcode.ToString)

                    If objGenerateFormat.GenerateFormat IsNot Nothing Then

                        If objGenerateFormat.Prefix = "" And objGenerateFormat.FormatDate1 = "" And objGenerateFormat.FormatDate2 = "" And objGenerateFormat.Prefix2 = "" And objGenerateFormat.NumberCount = "" And objGenerateFormat.PrefixPlace = "NotPrefix" Then
                            txtItemCode.Text = ""
                        Else
                            If objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                                txtVItemCode.Text = "V" + ItemCategoryPrefix + objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, "V" + ItemCategoryPrefix)

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                                txtVItemCode.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, "V" + ItemCategoryPrefix) + "V" + ItemCategoryPrefix

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                                txtVItemCode.Text = "V" + objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, "V")
                            End If
                        End If
                    End If
                End If
            Else
                txtVItemCode.Text = ""

            End If
            _GetItemCode = txtVItemCode.Text

        ElseIf chkIsLooseDiamond.Checked Then

            If cboDCategory.SelectedValue IsNot Nothing Then

                If cboDCategory.SelectedValue <> "-1" Then

                    Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

                    objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.DiamondBarcode.ToString)

                    If objGenerateFormat.GenerateFormat IsNot Nothing Then

                        If objGenerateFormat.Prefix = "" And objGenerateFormat.FormatDate1 = "" And objGenerateFormat.FormatDate2 = "" And objGenerateFormat.Prefix2 = "" And objGenerateFormat.NumberCount = "" And objGenerateFormat.PrefixPlace = "NotPrefix" Then
                            txtItemCode.Text = ""
                        Else
                            If objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                                txtDBarcode.Text = ItemCategoryPrefix + objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, ItemCategoryPrefix)

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                                txtDBarcode.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, ItemCategoryPrefix) + ItemCategoryPrefix

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                                txtDBarcode.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, "")
                            End If
                        End If
                    End If
                End If
            Else
                txtDBarcode.Text = ""

            End If

            _GetItemCode = txtDBarcode.Text

        Else
            If cboItemCategory.SelectedValue IsNot Nothing Then

                If cboItemCategory.SelectedValue <> "-1" Then

                    Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

                    objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.Barcode.ToString)

                    If objGenerateFormat.GenerateFormat IsNot Nothing Then

                        If objGenerateFormat.Prefix = "" And objGenerateFormat.FormatDate1 = "" And objGenerateFormat.FormatDate2 = "" And objGenerateFormat.Prefix2 = "" And objGenerateFormat.NumberCount = "" And objGenerateFormat.PrefixPlace = "NotPrefix" Then
                            txtItemCode.Text = ""
                        Else
                            If objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                                txtItemCode.Text = ItemCategoryPrefix + objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, ItemCategoryPrefix, _ForSaleID)

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                                txtItemCode.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, ItemCategoryPrefix, _ForSaleID) + ItemCategoryPrefix

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                                txtItemCode.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpGivenDate.Value.Date, "", _ForSaleID)

                            End If
                        End If
                    Else
                        txtItemCode.Text = ""
                    End If

                End If
            Else
                txtItemCode.Text = ""
            End If
            _GetItemCode = txtItemCode.Text
        End If
    End Sub


    Private Sub CalculateItemWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.000"

        If chkIsVolume.Checked = False Then
            If Val(txtItemTG.Text) > 0 And _IsGram = True Then
                'GoldWeight.Gram = CDec(txtItemTG.Text)
                GoldWeight.Gram = Format(Math.Round(CDec(txtItemTG.Text), 3), "0.000")
                _ItemTG = GoldWeight.Gram
                GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                _ItemTK = GoldWeight.GoldTK

                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtItemK.Text = CStr(GoldWeight.WeightK)
                txtItemP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtItemTK.Text = Format(_ItemTK, "0.000")
            ElseIf Val(txtItemTG.Text) > 0 And _WeightType = "Gram" Then
                GoldWeight.Gram = Format(Math.Round(CDec(txtItemTG.Text), 3), "0.000")
                _ItemTG = GoldWeight.Gram
                GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                _ItemTK = GoldWeight.GoldTK

                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
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
                txtItemY.Text = "0.00"
                txtItemTK.Text = "0.0"





            End If
        Else
            If Val(txtVItemTG.Text) > 0 And _VIsGram = True Then
                GoldWeight.Gram = CDec(txtVItemTG.Text)
                _VItemTG = GoldWeight.Gram
                GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                _VItemTK = GoldWeight.GoldTK

                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtVItemK.Text = CStr(GoldWeight.WeightK)
                txtVItemP.Text = CStr(GoldWeight.WeightP)
                txtVItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtVItemTK.Text = Format(_VItemTK, "0.000")
            ElseIf Val(txtVItemTG.Text) > 0 And _WeightType = "Gram" Then
                GoldWeight.Gram = Format(Math.Round(CDec(txtVItemTG.Text), 3), "0.000")
                _VItemTG = GoldWeight.Gram
                GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                _VItemTK = GoldWeight.GoldTK

                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtVItemK.Text = CStr(GoldWeight.WeightK)
                txtVItemP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtVItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtVItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtVItemTK.Text = Format(_ItemTK, "0.000")
            Else
                _VItemTG = 0.0
                _VItemTK = 0.0

                txtVItemK.Text = "0"
                txtVItemP.Text = "0"
                txtVItemY.Text = "0.0"
                txtVItemTK.Text = "0.0"
            End If
        End If
    End Sub
    Private Sub CalculateItemWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtItemK.Text = "" Then txtItemK.Text = "0"
        If txtItemP.Text = "" Then txtItemP.Text = "0"
        If txtItemY.Text = "" Then txtItemY.Text = "0.00"
        If txtVItemK.Text = "" Then txtVItemK.Text = "0"
        If txtVItemP.Text = "" Then txtVItemP.Text = "0"
        If txtVItemY.Text = "" Then txtVItemY.Text = "0.00"

        If chkIsVolume.Checked = False Then
            If (Val(txtItemK.Text) > 0 Or Val(txtItemP.Text) > 0 Or Val(txtItemY.Text) > 0) And _IsGram = False Then
                GoldWeight.WeightK = CInt(txtItemK.Text)
                GoldWeight.WeightP = CInt(txtItemP.Text)
                If numberformat = 1 Then
                    GoldWeight.WeightY = System.Decimal.Truncate(txtItemY.Text)
                Else
                    GoldWeight.WeightY = Format(CDec(txtItemY.Text), "0.00")
                End If
                'GoldWeight.WeightY = System.Decimal.Truncate(txtItemY.Text)
                GoldWeight.WeightC = CDec(txtItemY.Text) - GoldWeight.WeightY
                GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                _ItemTK = GoldWeight.GoldTK
                GoldWeight.Gram = _ItemTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
                _ItemTG = GoldWeight.Gram
                txtItemTG.Text = Format(Math.Round(_ItemTG, 4), "0.000")
                txtItemTK.Text = Format(_ItemTK, "0.000")
            Else
                _ItemTG = 0.0
                _ItemTK = 0.0
                txtItemTG.Text = "0.000"
                txtItemTK.Text = "0.0"
                'txtItemK.Text = "0"
                'txtItemP.Text = "0"
                'txtItemY.Text = "0.0"
            End If
        Else
            If (Val(txtVItemK.Text) > 0 Or Val(txtVItemP.Text) > 0 Or Val(txtVItemY.Text) > 0) And _VIsGram = False Then
                GoldWeight.WeightK = CInt(txtVItemK.Text)
                GoldWeight.WeightP = CInt(txtVItemP.Text)
                GoldWeight.WeightY = System.Decimal.Truncate(txtVItemY.Text)
                GoldWeight.WeightC = CDec(txtVItemY.Text) - GoldWeight.WeightY
                GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                _VItemTK = GoldWeight.GoldTK
                GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
                _VItemTG = GoldWeight.Gram
                txtVItemTG.Text = Format(_VItemTG, "0.000")
                txtVItemTK.Text = Format(_VItemTK, "0.000")
            Else
                _VItemTG = 0.0
                _VItemTK = 0.0
                txtVItemTG.Text = "0.00"
                txtVItemTK.Text = "0.00"
                'txtVItemK.Text = "0"
                'txtVItemP.Text = "0"
                'txtVItemY.Text = "0.0" 
            End If
        End If
    End Sub
    Private Sub CalculateWasteWeightForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0.0"

        If Val(txtWasteTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtWasteTG.Text)
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteK.Text = CStr(GoldWeight.WeightK)
            txtWasteP.Text = CStr(GoldWeight.WeightP)
            txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            _WasteTG = GoldWeight.Gram
            _WasteTK = GoldWeight.GoldTK
            txtWasteTK.Text = Format(_WasteTK, "0.000")

            If _ForSaleID = "0" Then
                GoldWeight.Gram = CDec(txtWasteTG.Text)
                _PWasteTG = GoldWeight.Gram

                GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                _PWasteTK = GoldWeight.GoldTK

                txtPWasteTG.Text = Format(_PWasteTG, "0.000")
                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtPWasteK.Text = CStr(GoldWeight.WeightK)
                txtPWasteP.Text = CStr(GoldWeight.WeightP)
                txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            End If
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0

            txtWasteK.Text = "0"
            txtWasteP.Text = "0"
            txtWasteY.Text = "0.0"
            txtWasteTK.Text = "0.0"

            If _PWasteTK = 0 Then
                _PWasteTG = 0.0
                _PWasteTK = 0.0

                txtPWasteK.Text = "0"
                txtPWasteP.Text = "0"
                txtPWasteY.Text = "0.0"
            End If
        End If
    End Sub

    Private Sub CalculatePurchaseWasteWeightForGram()
        If txtPWasteTG.Text = "" Then txtPWasteTG.Text = "0.0"

        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If Val(txtPWasteTG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtPWasteTG.Text)
            _PWasteTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _PWasteTK = GoldWeight.GoldTK

            GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            txtPWasteK.Text = CStr(GoldWeight.WeightK)
            txtPWasteP.Text = CStr(GoldWeight.WeightP)
            txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

        Else
            _PWasteTG = 0.0
            _PWasteTK = 0.0

            txtPWasteK.Text = "0"
            txtPWasteP.Text = "0"
            txtPWasteY.Text = "0.0"
        End If

        CalculateOriginalGoldPrice()
        CalculateOriginalPrice()
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

            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _WasteTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _WasteTG = GoldWeight.Gram

            txtWasteTG.Text = Format(_WasteTG, "0.000")
            txtWasteTK.Text = Format(_WasteTK, "0.000")
            If _ForSaleID = "0" Then
                _PWasteTK = _WasteTK
                _PWasteTG = _WasteTG

                GoldWeight.GoldTK = _PWasteTK
                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtPWasteK.Text = CStr(GoldWeight.WeightK)
                txtPWasteP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtPWasteTG.Text = Format(_PWasteTG, "0.000")

                CalculateOriginalGoldPrice()
                CalculateOriginalPrice()
            End If
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0
            txtWasteTG.Text = "0.0"
            txtWasteTK.Text = "0.0"

            If _PWasteTK = 0 Then
                _PWasteTG = 0.0
                _PWasteTK = 0.0
                txtPWasteTG.Text = "0.0"
                txtPWasteK.Text = "0"
                txtPWasteP.Text = "0"
                txtPWasteY.Text = "0.0"

                CalculateOriginalGoldPrice()
                CalculateOriginalPrice()
            End If
        End If
    End Sub

    Private Sub CalculatePurchaseWasteWeightForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtPWasteK.Text = "" Then txtPWasteK.Text = "0"
        If txtPWasteP.Text = "" Then txtPWasteP.Text = "0"
        If txtPWasteY.Text = "" Then txtPWasteY.Text = "0.0"

        If (Val(txtPWasteK.Text) > 0 Or Val(txtPWasteP.Text) > 0 Or Val(txtPWasteY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtPWasteK.Text)
            GoldWeight.WeightP = CInt(txtPWasteP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(txtPWasteY.Text)
            GoldWeight.WeightC = CDec(txtPWasteY.Text) - GoldWeight.WeightY

            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _PWasteTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _PWasteTG = GoldWeight.Gram
            txtPWasteTG.Text = Format(_PWasteTG, "0.000")
        Else
            _PWasteTG = 0.0
            _PWasteTK = 0.0
            'txtPWasteK.Text = "0"
            'txtPWasteP.Text = "0"
            'txtPWasteY.Text = "0.0"
            txtPWasteTG.Text = "0.0"
        End If

        CalculateOriginalGoldPrice()
        CalculateOriginalPrice()
    End Sub
    Private Sub CalculateTotalWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If CStr(_ItemTG) <> "" Or CStr(_WasteTG) <> "" Then
            If _ItemTG <> 0.0 Or _WasteTG <> 0.0 Then
                Dim ItemWeight As New CommonInfo.GoldWeightInfo
                Dim WasteWeight As New CommonInfo.GoldWeightInfo
                Dim TotalWeight As New CommonInfo.GoldWeightInfo

                ItemWeight.WeightK = CDec(txtItemK.Text)
                ItemWeight.WeightP = CDec(txtItemP.Text)

                If numberformat = 1 Then
                    ItemWeight.WeightY = Format(CDec(txtItemY.Text), "0.0")
                Else
                    ItemWeight.WeightY = Format(CDec(txtItemY.Text), "0.00")

                End If
                'ItemWeight.WeightY = CDec(txtItemY.Text)

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


                TotalWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(TotalWeight)
                _TotalTK = TotalWeight.GoldTK

                'TotalWeight.Gram = TotalWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
                '_TotalTG = TotalWeight.Gram

                _TotalTG = CDec(txtItemTG.Text) + CDec(txtWasteTG.Text)
                txtTotalTG.Text = Format(Format(Math.Round(CDec(txtItemTG.Text), 4), "0.000") + CDec(txtWasteTG.Text), "0.000")
                txtTotalTK.Text = Format(CDec(txtItemTK.Text) + CDec(txtWasteTK.Text), "0.000")
            Else
                _TotalTK = 0.0
                _TotalTG = 0.0

                txtTotalK.Text = "0"
                txtTotalP.Text = "0"
                txtTotalY.Text = "0.00"
                txtTotalTG.Text = "0.0"
                txtTotalTK.Text = "0.0"
            End If
        End If
    End Sub
    Private Sub CalculateGoldWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If _ItemTG > 0.0 Or _TotalGemsTG > 0.0 Then
            Dim ItemWeight As New CommonInfo.GoldWeightInfo
            Dim GemWeight As New CommonInfo.GoldWeightInfo
            Dim GoldWeight As New CommonInfo.GoldWeightInfo

            ItemWeight.WeightK = CDec(txtItemK.Text)
            ItemWeight.WeightP = CDec(txtItemP.Text)
            ItemWeight.WeightY = CDec(txtItemY.Text)

            GemWeight.WeightK = CDec(txtGemK.Text)
            GemWeight.WeightP = CDec(txtGemP.Text)
            GemWeight.WeightY = CDec(txtGemY.Text)

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


            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _GoldTK = GoldWeight.GoldTK

            'GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            '_GoldTG = GoldWeight.Gram

            _GoldTG = CDec(txtItemTG.Text) - CDec(txtGemTG.Text)
            txtGoldTG.Text = Format(CDec(txtItemTG.Text) - CDec(txtGemTG.Text), "0.000")
            txtGoldTK.Text = Format(CDec(txtItemTK.Text) - CDec(txtGemsTK.Text), "0.000")
        Else
            _GoldTG = 0.0
            _GoldTK = 0.0

            txtGoldK.Text = "0"
            txtGoldP.Text = "0"
            txtGoldY.Text = "0.0"
            txtGoldTG.Text = "0.0"
            txtGoldTK.Text = "0.0"
        End If
        CalculateOriginalGoldPrice()
        CalculateOriginalPrice()
    End Sub
    Private Sub CalculateOriginalGoldPrice()
        If txtOriginalPriceTK.Text = "" Then txtOriginalPriceTK.Text = "0"
        If txtOriginalPriceGram.Text = "" Then txtOriginalPriceGram.Text = "0"
        Dim TempTK As Decimal = 0.0
        Dim OrgTempTK As Decimal = 0.0

        If txtPWasteK.Text = "" Then txtPWasteK.Text = "0"
        If txtPWasteP.Text = "" Then txtPWasteP.Text = "0"
        If txtPWasteY.Text = "" Then txtPWasteY.Text = "0"
        If txtPWasteTG.Text = "" Then txtPWasteTG.Text = "0"
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0"
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0"
        If txtGoldK.Text = "" Then txtGoldK.Text = "0"
        If txtGoldP.Text = "" Then txtGoldP.Text = "0"
        If txtGoldY.Text = "" Then txtGoldY.Text = "0"
        If txtGoldTG.Text = "" Then txtGoldTG.Text = "0"
        If txtOriginalGemsPrice.Text = "" Then txtOriginalGemsPrice.Text = "0"
        If txtOriginalOtherPrice.Text = "" Then txtOriginalOtherPrice.Text = "0"

        If radKPrice.Checked Then
            Dim GoldWeight As New CommonInfo.GoldWeightInfo
            GoldWeight.WeightK = CInt(txtGoldK.Text) + CInt(txtPWasteK.Text)
            GoldWeight.WeightP = CInt(txtGoldP.Text) + CInt(txtPWasteP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtPWasteY.Text))
            GoldWeight.WeightC = (CDec(txtGoldY.Text) + CDec(txtPWasteY.Text)) - GoldWeight.WeightY
            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            TempTK = GoldWeight.GoldTK
            _GoldPrice = CStr(CLng(txtOriginalPriceTK.Text) * TempTK)

            GoldWeight.WeightK = CInt(txtGoldK.Text) + CInt(txtWasteK.Text)
            GoldWeight.WeightP = CInt(txtGoldP.Text) + CInt(txtWasteP.Text)
            GoldWeight.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtWasteY.Text))
            GoldWeight.WeightC = (CDec(txtGoldY.Text) + CDec(txtWasteY.Text)) - GoldWeight.WeightY
            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            OrgTempTK = GoldWeight.GoldTK
            _OrgGoldPrice = CStr(CLng(txtOriginalPriceTK.Text) * OrgTempTK)

        Else
            _GoldPrice = CStr(CLng(txtOriginalPriceGram.Text) * (CDec(txtGoldTG.Text) + CDec(txtPWasteTG.Text)))
            _OrgGoldPrice = CStr(CLng(txtOriginalPriceGram.Text) * (CDec(txtGoldTG.Text) + CDec(txtWasteTG.Text)))
        End If
    End Sub
    Private Sub CalculateOriginalPrice()
        If txtOriginalGemsPrice.Text = "" Then txtOriginalGemsPrice.Text = "0"
        If txtOriginalOtherPrice.Text = "" Then txtOriginalOtherPrice.Text = "0"
        If txtOriginalFixedPrice.Text = "" Then txtOriginalFixedPrice.Text = "0"
        If txtSellingRate.Text = "" Then txtSellingRate.Text = "0"

        If chkIsOriginalFixedPrice.Checked Then
            txtPriceCode.Text = txtOriginalFixedPrice.Text
            txtTotalCost.Text = txtOriginalFixedPrice.Text
        ElseIf chkIsOriginalPriceGram.Checked Then
            txtPriceCode.Text = CStr(_OrgGoldPrice + CLng(txtOriginalGemsPrice.Text))
            txtTotalCost.Text = CStr(_GoldPrice + CLng(txtOriginalGemsPrice.Text) + CLng(txtOriginalOtherPrice.Text))
        Else
            txtPriceCode.Text = "0"
            txtTotalCost.Text = "0"
        End If

        If Global_GIsFixPrice Then
            chkByFixedPrice.Checked = Global_GIsFixPrice
            'If CLng(txtSellingRate.Text) > 0 Then
            '    txtSaleFixPrice.Text = CLng(txtTotalCost.Text) * (CLng(txtSellingRate.Text) / 100)
            '    txtWSFixPrice.Text = CLng(txtTotalCost.Text) * (CLng(txtSellingRate.Text) / 100)
            'Else
            txtSaleFixPrice.Text = txtTotalCost.Text
            txtWSFixPrice.Text = txtTotalCost.Text
        End If
        CalculateSellingAmt()

    End Sub
    Private Sub CalculateDOriginalPrice()

        If chkDOriginalFixPrice.Checked Then
            txtDOriginalPrice.Text = txtDOriginalFixPrice.Text
        ElseIf chkOriginalPriceCarat.Checked Then
            txtDOriginalPrice.Text = CStr(CLng(_DCarat * CLng(txtOriginalPriceCarat.Text)))
        Else
            txtDOriginalPrice.Text = ""
        End If
    End Sub
    Private Sub CalculateGrdGems()

        _GrdGemPrice = 0
        _GrdGemQTY = 0
        _Totalct = 0.0
        _TotalGemsTG = 0
        _TotalGemsTK = 0

        Dim GemWeight As New CommonInfo.GoldWeightInfo
        Dim TotalWeight As New CommonInfo.GoldWeightInfo
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0
        txtGemTG.Text = "0.0"

        For i As Integer = 0 To grdGems.RowCount - 1
            If Not grdGems.Rows(i).IsNewRow Then
                _GrdGemPrice += CInt(Val(grdGems.Rows(i).Cells("Amount").FormattedValue))
                _GrdGemQTY += CInt(Val(grdGems.Rows(i).Cells("Qty").FormattedValue))
                _TotalGemsTG += CDec(Val(grdGems.Rows(i).Cells("GemsTG").FormattedValue))

                GemWeight.WeightK = CInt(Val(grdGems.Rows(i).Cells("GemsK").FormattedValue))
                GemWeight.WeightP = CInt(Val(grdGems.Rows(i).Cells("GemsP").FormattedValue))
                GemWeight.WeightY = CDec(Val(grdGems.Rows(i).Cells("GemsY").FormattedValue))

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

        txtGemK.Text = Format(TotalWeight.WeightK, "0")
        txtGemP.Text = Format(TotalWeight.WeightP, "0")
        If numberformat = 1 Then
            txtGemY.Text = Format(TotalWeight.WeightY, "0.0")
        Else
            txtGemY.Text = Format(TotalWeight.WeightY, "0.00")
        End If

        TotalWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(TotalWeight)
        _TotalGemsTK = TotalWeight.GoldTK



        txtGemTG.Text = Format(_TotalGemsTG, "0.000")
        txtGemsTK.Text = Format(_TotalGemsTK, "0.000")
        _dtForSaleGems = grdGems.DataSource
    End Sub
    Private Sub GoldQualityForTextChange()
        If chkIsVolume.Checked Then
            If cboVGoldQuality.Text <> "" Then
                Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
                GoldQualityInfo = _GoldQCon.GetGoldQuality(cboVGoldQuality.SelectedValue)

                _VIsGram = GoldQualityInfo.IsGramRate
                _VPrefix = GoldQualityInfo.Prefix
                If chkVIsOriginalPriceGram.Checked = True Then
                    If _VIsGram = True Then
                        radVGramPrice.Enabled = True
                        radVGramPrice.Checked = True
                        txtVOriginalPriceKyat.Enabled = False
                        txtVOriginalPriceKyat.Text = "0"
                        txtVOriginalPriceGram.Enabled = True
                        radVKPrice.Enabled = False
                        txtVOriginalPriceKyat.BackColor = Color.Gainsboro
                        txtVOriginalPriceGram.BackColor = Color.White
                    Else
                        radVKPrice.Enabled = True
                        radVKPrice.Checked = True
                        radVGramPrice.Enabled = False
                        txtVOriginalPriceGram.Enabled = False
                        txtVOriginalPriceGram.Text = "0"
                        txtVOriginalPriceKyat.Enabled = True
                        txtVOriginalPriceKyat.Enabled = True
                        txtVOriginalPriceGram.Enabled = False
                        txtVOriginalPriceKyat.BackColor = Color.White
                        txtVOriginalPriceGram.BackColor = Color.Gainsboro
                    End If
                End If

                If _VIsGram = True Then
                    txtVItemK.ReadOnly = True
                    txtVItemP.ReadOnly = True
                    txtVItemY.ReadOnly = True
                    txtVItemTG.ReadOnly = False

                    txtVItemK.BackColor = Color.Linen
                    txtVItemP.BackColor = Color.Linen
                    txtVItemY.BackColor = Color.Linen
                    txtVItemTG.BackColor = Color.White
                    txtVItemTG.TabStop = True
                    txtVItemK.TabStop = False
                    txtVItemP.TabStop = False
                    txtVItemY.TabStop = False
                    LnkTotalWeight.TabIndex = 9
                    txtVItemTG.TabIndex = 10

                Else
                    txtVItemK.ReadOnly = False
                    txtVItemP.ReadOnly = False
                    txtVItemY.ReadOnly = False
                    txtVItemTG.ReadOnly = True
                    txtVItemTG.TabStop = False

                    txtVItemK.TabStop = True
                    txtVItemP.TabStop = True
                    txtVItemY.TabStop = True

                    txtVItemK.BackColor = Color.White
                    txtVItemP.BackColor = Color.White
                    txtVItemY.BackColor = Color.White
                    txtVItemTG.BackColor = Color.Linen
                    LnkTotalWeight.TabIndex = 9
                    txtVItemK.TabIndex = 10
                    txtVItemP.TabIndex = 11
                    txtVItemY.TabIndex = 12
                End If
            Else
                _VIsGram = False
                _VPrefix = ""
                txtVItemTG.BackColor = Color.Linen
                txtVItemK.BackColor = Color.Linen
                txtVItemP.BackColor = Color.Linen
                txtVItemY.BackColor = Color.Linen
                txtVItemTG.ReadOnly = True
                txtVItemK.ReadOnly = True
                txtVItemP.ReadOnly = True
                txtVItemY.ReadOnly = True
            End If

        Else
            If cboGoldQuality.Text <> "" Then
                Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
                GoldQualityInfo = _GoldQCon.GetGoldQuality(cboGoldQuality.SelectedValue)
                _IsGram = GoldQualityInfo.IsGramRate
                _Prefix = GoldQualityInfo.Prefix
                If chkIsOriginalPriceGram.Checked = True Then
                    If _IsGram = True Then
                        radGramPrice.Enabled = True
                        radKPrice.Enabled = False
                        radGramPrice.Checked = True
                        txtOriginalPriceGram.Enabled = True
                        txtOriginalPriceGram.BackColor = Color.White

                        radKPrice.Checked = False
                        txtOriginalPriceTK.Enabled = False
                        txtOriginalPriceTK.Text = "0"
                        txtOriginalPriceGram.Text = "0"
                        txtOriginalPriceTK.BackColor = Color.Gainsboro
                    Else
                        radGramPrice.Enabled = False
                        radKPrice.Enabled = True
                        radKPrice.Checked = True
                        txtOriginalPriceTK.Enabled = True
                        txtOriginalPriceTK.BackColor = Color.White

                        radGramPrice.Checked = False
                        txtOriginalPriceGram.Enabled = False
                        txtOriginalPriceGram.Text = "0"
                        txtOriginalPriceTK.Text = "0"
                        txtOriginalPriceGram.BackColor = Color.Gainsboro
                    End If
                End If

                If _IsGram = True Then
                    txtItemK.ReadOnly = True
                    txtItemP.ReadOnly = True
                    txtItemY.ReadOnly = True
                    txtItemTG.ReadOnly = False
                    txtItemTG.TabIndex = 1
                    txtWasteTG.TabIndex = 2
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

                    txtPWasteK.ReadOnly = True
                    txtPWasteP.ReadOnly = True
                    txtPWasteY.ReadOnly = True
                    txtPWasteTG.ReadOnly = False

                    txtPWasteK.BackColor = Color.Linen
                    txtPWasteP.BackColor = Color.Linen
                    txtPWasteY.BackColor = Color.Linen
                    txtPWasteTG.BackColor = Color.White
                    txtPWasteK.TabStop = False
                    txtPWasteP.TabStop = False
                    txtPWasteY.TabStop = False
                    txtPWasteTG.TabStop = True

                    'txtPriceCode.Enabled = True
                    'lblPriceCode.Enabled = True
                    'txtPriceCode.BackColor = Color.White
                Else
                    txtItemK.ReadOnly = False
                    txtItemP.ReadOnly = False
                    txtItemY.ReadOnly = False
                    'txtItemTG.ReadOnly = True
                    txtItemK.TabIndex = 1
                    txtItemP.TabIndex = 2
                    txtItemY.TabIndex = 3
                    txtWasteK.TabIndex = 4
                    txtWasteP.TabIndex = 5
                    txtWasteY.TabIndex = 6
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

                    txtPWasteK.ReadOnly = False
                    txtPWasteP.ReadOnly = False
                    txtPWasteY.ReadOnly = False
                    txtPWasteTG.ReadOnly = True

                    txtPWasteK.TabStop = True
                    txtPWasteP.TabStop = True
                    txtPWasteY.TabStop = True
                    txtPWasteTG.TabStop = False

                    txtPWasteK.BackColor = Color.White
                    txtPWasteP.BackColor = Color.White
                    txtPWasteY.BackColor = Color.White
                    txtPWasteTG.BackColor = Color.Linen

                    'txtPriceCode.Enabled = False
                    'lblPriceCode.Enabled = False
                    'txtPriceCode.BackColor = Color.Linen

                    'If chkIsDiamond.Checked Then
                    '    txtPriceCode.Enabled = True
                    '    lblPriceCode.Enabled = True
                    '    txtPriceCode.BackColor = Color.White
                    'End If
                End If
            Else
                _IsGram = False
                _Prefix = ""

                ' txtItemTG.ReadOnly = True
                txtItemK.ReadOnly = True
                txtItemP.ReadOnly = True
                txtItemY.ReadOnly = True

                txtItemTG.BackColor = Color.Linen
                txtItemK.BackColor = Color.Linen
                txtItemP.BackColor = Color.Linen
                txtItemY.BackColor = Color.Linen

                txtWasteTG.ReadOnly = True
                txtWasteTK.ReadOnly = True
                txtWasteK.ReadOnly = True
                txtWasteP.ReadOnly = True
                txtWasteY.ReadOnly = True

                txtWasteTG.BackColor = Color.Linen
                txtWasteTK.BackColor = Color.Linen
                txtWasteK.BackColor = Color.Linen
                txtWasteP.BackColor = Color.Linen
                txtWasteY.BackColor = Color.Linen

                txtPWasteTG.ReadOnly = True
                txtPWasteK.ReadOnly = True
                txtPWasteP.ReadOnly = True
                txtPWasteY.ReadOnly = True

                txtPWasteTG.BackColor = Color.Linen
                txtPWasteK.BackColor = Color.Linen
                txtPWasteP.BackColor = Color.Linen
                txtPWasteY.BackColor = Color.Linen

                'txtPriceCode.Enabled = False
                'lblPriceCode.Enabled = False
                'txtPriceCode.BackColor = Color.Linen
            End If

        End If
    End Sub
    Private Sub AllClear()
        _IsUpdate = False
        cboGoldQuality.SelectedIndex = -1
        cboVGoldQuality.SelectedIndex = -1
        chkIsOrder.Checked = False
        btnOrderVoucherSearch.Visible = False
        cboItemCategory.SelectedIndex = -1
        cboVItemCategory.SelectedIndex = -1
        cboSupplier.SelectedIndex = -1
        cboGoldSmith.SelectedIndex = -1

        cboStaff.SelectedIndex = -1
        cboGoldQuality.SelectedIndex = -1
        cboVGoldQuality.SelectedIndex = -1
        cboItemName.SelectedIndex = -1
        cboVItemName.SelectedIndex = -1
        txtVItemCode.Text = ""
        txtItemCode.Text = ""
        radGramPrice.Enabled = False
        radKPrice.Enabled = False
        radGramPrice.Checked = True
        chkIsDiamond.Checked = False
        chkIsOriginalFixedPrice.Checked = False
        txtOriginalFixedPrice.Enabled = False
        txtOriginalFixedPrice.BackColor = Color.Gainsboro
        txtOriginalFixedPrice.Text = "0"
        chkDOriginalFixPrice.Checked = False
        txtDOriginalFixPrice.Enabled = False
        txtDOriginalFixPrice.BackColor = Color.Gainsboro
        txtDOriginalFixPrice.Text = "0"
        chkDFixPrice.Checked = False

        'txtDOriginalPrice.Enabled = False
        'txtDOriginalPrice.BackColor = Color.Gainsboro
        'txtDOriginalPrice.Text = "0"

        txtColor.Text = ""
        txtSupplierVou.Text = ""

        ChkIsSolidVolume.Checked = False
        'chkisNormal.Enabled = True
        'chkIsLooseDiamond.Enabled = True
        'chkIsVolume.Enabled = True

        chkIsOriginalPriceGram.Checked = False
        txtOriginalPriceGram.Enabled = False
        txtOriginalPriceTK.Enabled = False
        txtOriginalGemsPrice.Enabled = False

        txtOriginalPriceGram.BackColor = Color.Gainsboro
        txtOriginalPriceTK.BackColor = Color.Gainsboro
        txtOriginalGemsPrice.BackColor = Color.Gainsboro
        txtOriginalOtherPrice.BackColor = Color.Gainsboro

        txtOriginalPriceGram.Text = "0"
        txtOriginalPriceTK.Text = "0"
        txtOriginalGemsPrice.Text = "0"
        txtOriginalOtherPrice.Text = "0"
        txtSellingRate.Text = "0"

        txtDOriginalFixPrice.Text = "0"
        txtDOriginalFixPrice.BackColor = Color.Gainsboro
        txtDOriginalFixPrice.Enabled = False
        chkDOriginalFixPrice.Checked = False
        txtOriginalPriceCarat.Text = "0"
        txtOriginalPriceCarat.BackColor = Color.Gainsboro
        txtOriginalPriceCarat.Enabled = False
        chkOriginalPriceCarat.Checked = False

        cboDCategory.SelectedIndex = -1

        cboDColor.SelectedIndex = -1
        cboDShape.SelectedIndex = -1
        cboDClarity.SelectedIndex = -1
        txtDescription.Text = ""
        txtRBP.Text = "0"
        txtDGram.Text = "0.0"
        txtDDesignCharges.Text = "0"
        txtDPlatingCharges.Text = "0"
        txtDMountingCharges.Text = "0"
        txtDWhiteCharges.Text = "0"
        txtDFixPrice.Text = "0"
        chkDFixPrice.Checked = False
        txtDOriginalPrice.Text = "0"
        txtDSellingRate.Text = "0"
        cboDSupplier.SelectedIndex = -1
        txtDVoucherNo.Text = ""
        lblDPhoto.Text = ""
        btnDAdd.Text = "Add"
        lblDItemImage.Image = Nothing

        txtDFixPrice.Enabled = False
        txtDFixPrice.BackColor = Color.Gainsboro
        If Global_CurrentUser = "Administrator" Then
            txtItemCode.ReadOnly = False
            txtVItemCode.ReadOnly = False
            txtDBarcode.ReadOnly = False

        Else
            If Global_IsAllowStock Then
                txtItemCode.ReadOnly = False
                txtVItemCode.ReadOnly = False
                txtDBarcode.ReadOnly = False
            Else
                txtItemCode.ReadOnly = True
                txtVItemCode.ReadOnly = True
                txtDBarcode.ReadOnly = True
            End If
        End If


        ItemClear()
    End Sub
    Private Sub ItemClear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _ForSaleID = "0"
        _IsUpdate = False
        txtStockID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.ForSale, EnumSetting.GenerateKeyType.ForSale.ToString, Now)
        _GetItemCode = ""
        _IsExit = False

        If chkIsVolume.Checked = True Then
            vitemid = cboVItemCategory.SelectedValue
            SaleItemCodeGenerateFormat()
        ElseIf chkIsLooseDiamond.Checked = True Then
            ditemid = cboDCategory.SelectedValue
            SaleItemCodeGenerateFormat()
        Else
            itemid = cboItemCategory.SelectedValue
            SaleItemCodeGenerateFormat()
        End If

        _OldOrderReceiveDetailID = ""
        _OldIsOrder = False
        dtpGivenDate.Value = Now
        'GoldQualityForTextChange()
        _OldDate = Now.Date
        txtWidth.Text = ""
        txtLength.Text = ""
        txtColor.Text = ""
        txtSellingPrice.Text = "0"
        txtItemTG.Text = "0.000"
        txtItemTK.Text = "0.0"
        txtItemK.Text = "0"
        txtItemP.Text = "0"
        txtItemY.Text = "0.00"
        txtVWidth.Text = ""
        txtVLength.Text = ""
        txtVSellingPrice.Text = "-"
        txtVItemTG.Text = "0.0"
        txtVItemTK.Text = "0.0"
        txtVItemK.Text = "0"
        txtVItemP.Text = "0"
        txtVItemY.Text = "0.00"

        txtWasteTG.Text = "0.0"
        txtWasteTK.Text = "0.0"
        txtWasteK.Text = "0"
        txtWasteP.Text = "0"
        txtWasteY.Text = "0.0"

        txtPWasteTG.Text = "0.0"
        txtPWasteK.Text = "0"
        txtPWasteP.Text = "0"
        txtPWasteY.Text = "0.0"

        txtTotalTG.Text = "0.0"
        txtTotalTK.Text = "0.0"
        txtTotalK.Text = "0"
        txtTotalP.Text = "0"
        txtTotalY.Text = "0.0"

        txtGoldTG.Text = "0.0"
        txtGoldTK.Text = "0.0"
        txtGoldK.Text = "0"
        txtGoldP.Text = "0"
        txtGoldY.Text = "0.0"

        txtGemTG.Text = "0.0"
        txtGemsTK.Text = "0.0"
        txtGemK.Text = "0"
        txtGemP.Text = "0"
        txtGemY.Text = "0.00"
        _GemsTK = 0
        _GemsTG = 0

        chkByFixedPrice.Checked = False
        txtSaleFixPrice.Text = "0"
        txtSaleFixPrice.Enabled = False
        txtSaleFixPrice.BackColor = Color.Gainsboro

        txtWSFixPrice.Text = "0"
        txtWSFixPrice.Enabled = False
        txtWSFixPrice.BackColor = Color.Gainsboro
        _GoldPrice = 0
        _OrgGoldPrice = 0
        If chkIsOriginalFixedPrice.Checked Then
            txtOriginalFixedPrice.Text = "0"
        End If
        If chkIsOriginalPriceGram.Checked Then
            txtOriginalGemsPrice.Text = "0"
            txtOriginalOtherPrice.Text = "0"
        End If


        txtDesignCharges.Text = "0"
        txtPlatingCharges.Text = "0"
        txtMountingCharges.Text = "0"
        txtWhiteCharges.Text = "0"
        _TotalGemsTG = 0
        _TotalGemsTK = 0
        _GoldTG = 0
        _GoldTK = 0
        _WasteTG = 0
        _WasteTK = 0
        _ItemTG = 0
        _ItemTK = 0
        _TotalTG = 0
        _TotalTK = 0
        _PWasteTG = 0
        _PWasteTK = 0
        btnSave.Text = "&Save"
        txtGoldSmith.Text = ""
        txtRemark.Text = ""

        If Global_LogoPhoto <> "" Then
            Try
                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                PName = Global_LogoPhoto
                btnAdd.Text = "Add"
                lblPhoto.Visible = False
            Catch ex As Exception
                'MsgBox(ex.Message + " does not exist.")
                lblItemImage.Image = Nothing
                lblPhoto.Visible = True
                PName = ""
                btnAdd.Text = "Add"
            End Try
        Else

            lblItemImage.Image = Nothing
            lblPhoto.Visible = True
            PName = ""
            btnAdd.Text = "Add"
        End If

        chkIsClosed.Checked = False
        chkIsClosed.Enabled = False
        chkIsVolume.Enabled = True
        chkIsLooseDiamond.Enabled = True
        chkisNormal.Enabled = True
        If chkIsVolume.Checked = True Then
            tabStock.TabPages.Remove(TabStockItem)
            tabStock.TabPages.Remove(TabLooseDiamond)
        ElseIf chkIsLooseDiamond.Checked = True Then
            tabStock.TabPages.Remove(TabStockItem)
            tabStock.TabPages.Remove(TabVolumeStock)
        Else
            'tabStock.TabPages.Item(0).Hide()
            tabStock.TabPages.Remove(TabVolumeStock)
            tabStock.TabPages.Remove(TabLooseDiamond)
            tabStock.SelectedIndex = 0
        End If
        chkVByFixPrice.Checked = False
        txtVFixedPrice.Text = "0"
        txtVFixedPrice.Enabled = False

        chkVIsOriginalFixedPrice.Checked = False
        txtVOriginalFixPrice.Enabled = False
        txtVOriginalFixPrice.BackColor = Color.Gainsboro
        txtVOriginalFixPrice.Text = "0"

        txtVOriginalPriceGram.Enabled = False
        txtVOriginalPriceKyat.Enabled = False
        txtVOriginalGemPrice.Enabled = False
        txtVOriginalOtherPrice.Enabled = False

        txtVOriginalPriceGram.BackColor = Color.Gainsboro
        txtVOriginalPriceKyat.BackColor = Color.Gainsboro
        txtVOriginalGemPrice.BackColor = Color.Gainsboro
        txtVOriginalOtherPrice.BackColor = Color.Gainsboro

        txtVOriginalPriceGram.Text = "0"
        txtVOriginalPriceKyat.Text = "0"
        txtVOriginalGemPrice.Text = "0"
        txtVOriginalOtherPrice.Text = "0"
        radVGramPrice.Enabled = True
        radVKPrice.Enabled = True
        radVGramPrice.Checked = True
        chkVByFixPrice.Checked = False
        txtVFixedPrice.BackColor = Color.Gainsboro
        txtOriginalOtherPrice.BackColor = Color.Gainsboro

        If Global_LogoPhoto <> "" Then
            Try
                lblVItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                VPName = Global_LogoPhoto
                btnVAdd.Text = "Add"
                lblVPhoto.Visible = False
            Catch ex As Exception
                'MsgBox(ex.Message + " does not exist.")
                lblVItemImage.Image = Nothing
                lblVPhoto.Visible = True
                VPName = ""
                btnVAdd.Text = "Add"
            End Try
        Else
            lblVItemImage.Image = Nothing
            lblVPhoto.Visible = True
            VPName = ""
            btnVAdd.Text = "Add"
        End If
        _VItemTG = 0
        _VItemTK = 0
        cboSupplier.SelectedIndex = -1
        cboGoldSmith.SelectedValue = "DEFAULT"
        chkVIsOriginalFixedPrice.Enabled = True
        txtQTY.Text = "0"
        _IsPurchase = False
        tabStock.Enabled = True
        dtpGivenDate.Enabled = True
        cboStaff.Enabled = True
        _Totalct = 0
        lblCustomer.Text = ""
        lblOrderInvoiceiD.Text = ""
        lblOrderDate.Text = ""
        lblOrderInvoiceiD.Visible = False
        lblOrderDate.Visible = False
        lblCustomer.Visible = False
        chkIsOrder.Checked = False
        btnOrderVoucherSearch.Visible = False
        chkIsDiamond.Enabled = True
        'chkIsDiamond.Checked = False
        txtOriginalCode.Text = ""
        txtDOCode.Text = ""
        txtPriceCode.Text = ""
        txtTotalCost.Text = ""
        txtSupplierVou.Text = ""
        txtColor.Text = ""
        txtSellingRate.Text = "0"
        cboColor.SelectedIndex = -1
        cboDColor.SelectedIndex = -1
        cboDShape.SelectedIndex = -1
        cboDClarity.SelectedIndex = -1
        chkOriginalPriceCarat.Checked = False
        txtDescription.Text = ""
        txtOriginalPriceCarat.Text = 0
        txtRBP.Text = "0"
        txtDGram.Text = "0.0"
        txtDDesignCharges.Text = "0"
        txtDPlatingCharges.Text = "0"
        txtDMountingCharges.Text = "0"
        txtDWhiteCharges.Text = "0"
        txtDFixPrice.Text = "0"
        chkDFixPrice.Checked = False
        txtDOriginalPrice.Text = "0"
        txtDSellingRate.Text = "0"
        cboDSupplier.SelectedIndex = -1
        txtDVoucherNo.Text = ""
        lblDPhoto.Text = ""
        btnDAdd.Text = "Add"
        txtDQty.Text = 0
        lblDItemImage.Image = Nothing
        ClearGrdForSaleGems()
    End Sub
    Private Sub SpeedEntryClear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _ForSaleID = "0"
        _IsUpdate = False
        txtStockID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.ForSale, EnumSetting.GenerateKeyType.ForSale.ToString, Now)
        _GetItemCode = ""
        _IsExit = False
        txtOriginalGemsPrice.Text = 0
        'If chkIsVolume.Checked = False Then
        '    itemid = cboItemCategory.SelectedValue
        '    SaleItemCodeGenerateFormat()
        'Else
        '    vitemid = cboVItemCategory.SelectedValue
        '    SaleItemCodeGenerateFormat()
        'End If

        If chkIsVolume.Checked = True Then
            vitemid = cboVItemCategory.SelectedValue
            SaleItemCodeGenerateFormat()
        ElseIf chkIsLooseDiamond.Checked = True Then
            ditemid = cboDCategory.SelectedValue
            SaleItemCodeGenerateFormat()
        Else
            itemid = cboItemCategory.SelectedValue
            SaleItemCodeGenerateFormat()
        End If

        _OldOrderReceiveDetailID = ""
        _OldIsOrder = False
        dtpGivenDate.Value = Now
        'GoldQualityForTextChange()
        _OldDate = Now.Date


        txtSellingPrice.Text = "0"
        _GoldTG = 0
        _GoldTK = 0
        _ItemTG = 0
        _ItemTK = 0
        txtItemTG.Text = "0.000"
        txtItemTK.Text = "0.0"
        txtItemK.Text = "0"
        txtItemP.Text = "0"
        txtItemY.Text = "0.00"

        CalculateTotalWeight()

        CalculateGoldWeight()
        _TotalTG = 0
        _TotalTK = 0

        '  CalculateItemWeightForGram()


        'txtTotalK.Text = "0"
        'txtTotalP.Text = "0"
        'txtTotalY.Text = "0.0"
        'txtTotalTG.Text = "0.0"
        'txtTotalTK.Text = "0.0"




        'txtVWidth.Text = ""
        'txtVLength.Text = ""
        'txtVSellingPrice.Text = "-"
        'txtVItemTG.Text = "0.0"
        'txtVItemTK.Text = "0.0"
        'txtVItemK.Text = "0"
        'txtVItemP.Text = "0"
        'txtVItemY.Text = "0.0"


        txtGoldTG.Text = "0.0"
        txtGoldTK.Text = "0.0"
        txtGoldK.Text = "0"
        txtGoldP.Text = "0"
        txtGoldY.Text = "0.0"

        txtGemTG.Text = "0.0"
        txtGemsTK.Text = "0.0"
        txtGemK.Text = "0"
        txtGemP.Text = "0"
        txtGemY.Text = "0.00"
        _GemsTK = 0
        _GemsTG = 0
        _DItemTG = 0
        _DItemTK = 0
        _DRBP = "0"

        txtWasteK.Text = "0"
        txtWasteP.Text = "0"
        txtWasteY.Text = "0.0"
        txtWasteTG.Text = "0.0"
        txtWasteTK.Text = "0.0"

        txtPWasteK.Text = "0"
        txtPWasteP.Text = "0"
        txtPWasteY.Text = "0.0"
        txtPWasteTG.Text = "0.0"


        chkByFixedPrice.Checked = False
        txtSaleFixPrice.Text = "0"
        txtSaleFixPrice.Enabled = False
        txtSaleFixPrice.BackColor = Color.Gainsboro

        chkDFixPrice.Checked = False
        txtDFixPrice.Text = "0"
        txtDFixPrice.Enabled = False
        txtDFixPrice.BackColor = Color.Gainsboro

        txtRBP.Text = "0"
        txtDGram.Text = "0"

        txtWSFixPrice.Text = "0"
        txtWSFixPrice.Enabled = False
        txtWSFixPrice.BackColor = Color.Gainsboro

        If chkIsOriginalFixedPrice.Checked Then
            txtOriginalFixedPrice.Text = "0"
        End If
        If chkIsOriginalPriceGram.Checked Then
            txtOriginalGemsPrice.Text = "0"
            '  txtOriginalOtherPrice.Text = "0"
        End If

        _TotalGemsTG = 0
        _TotalGemsTK = 0

        '_WasteTG = 0
        '_WasteTK = 0

        '_TotalTG = 0
        '_TotalTK = 0
        '_PWasteTG = 0
        '_PWasteTK = 0
        btnSave.Text = "&Save"


        If Global_LogoPhoto <> "" Then
            Try
                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                PName = Global_LogoPhoto
                btnAdd.Text = "Add"
                lblPhoto.Visible = False
            Catch ex As Exception
                'MsgBox(ex.Message + " does not exist.")
                lblItemImage.Image = Nothing
                lblPhoto.Visible = True
                PName = ""
                btnAdd.Text = "Add"
            End Try
        Else

            lblItemImage.Image = Nothing
            lblPhoto.Visible = True
            PName = ""
            btnAdd.Text = "Add"
        End If

        chkIsClosed.Checked = False
        chkIsClosed.Enabled = False
        chkIsVolume.Enabled = True
        If chkIsVolume.Checked = True Then
            tabStock.TabPages.Remove(TabStockItem)
        Else
            tabStock.TabPages.Item(0).Hide()
            tabStock.SelectedIndex = 0
            tabStock.TabPages.Remove(TabVolumeStock)
        End If
        chkVByFixPrice.Checked = False
        txtVFixedPrice.Text = "0"
        txtVFixedPrice.Enabled = False

        chkVIsOriginalFixedPrice.Checked = False
        txtVOriginalFixPrice.Enabled = False
        txtVOriginalFixPrice.BackColor = Color.Gainsboro
        txtVOriginalFixPrice.Text = "0"

        txtVOriginalPriceGram.Enabled = False
        txtVOriginalPriceKyat.Enabled = False
        txtVOriginalGemPrice.Enabled = False
        txtVOriginalOtherPrice.Enabled = False

        txtVOriginalPriceGram.BackColor = Color.Gainsboro
        txtVOriginalPriceKyat.BackColor = Color.Gainsboro
        txtVOriginalGemPrice.BackColor = Color.Gainsboro
        txtVOriginalOtherPrice.BackColor = Color.Gainsboro

        txtVOriginalPriceGram.Text = "0"
        txtVOriginalPriceKyat.Text = "0"
        txtVOriginalGemPrice.Text = "0"
        txtVOriginalOtherPrice.Text = "0"
        radVGramPrice.Enabled = True
        radVKPrice.Enabled = True
        radVGramPrice.Checked = True
        chkVByFixPrice.Checked = False
        txtVFixedPrice.BackColor = Color.Gainsboro

        If Global_LogoPhoto <> "" Then
            Try
                lblVItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                VPName = Global_LogoPhoto
                btnVAdd.Text = "Add"
                lblVPhoto.Visible = False
            Catch ex As Exception
                'MsgBox(ex.Message + " does not exist.")
                lblVItemImage.Image = Nothing
                lblVPhoto.Visible = True
                VPName = ""
                btnVAdd.Text = "Add"
            End Try
        Else
            lblVItemImage.Image = Nothing
            lblVPhoto.Visible = True
            VPName = ""
            btnVAdd.Text = "Add"
        End If
        _VItemTG = 0
        _VItemTK = 0

        chkVIsOriginalFixedPrice.Enabled = True
        txtQTY.Text = "0"
        _IsPurchase = False
        tabStock.Enabled = True
        dtpGivenDate.Enabled = True
        cboStaff.Enabled = True
        _Totalct = 0
        lblCustomer.Text = ""
        lblOrderInvoiceiD.Text = ""
        lblOrderDate.Text = ""
        lblOrderInvoiceiD.Visible = False
        lblOrderDate.Visible = False
        lblCustomer.Visible = False
        chkIsOrder.Checked = False
        btnOrderVoucherSearch.Visible = False
        chkIsDiamond.Enabled = True
        'chkIsDiamond.Checked = False
        'txtOriginalCode.Text = ""
        txtPriceCode.Text = ""
        txtTotalCost.Text = ""
        _GoldPrice = 0
        _OrgGoldPrice = 0
        ClearGrdForSaleGems()
    End Sub
    Private Sub ClearGrdForSaleGems()
        Dim dcGem As DataColumn
        Dim dcQty As DataColumn

        _dtForSaleGems = New DataTable

        _dtForSaleGems.Columns.Add("ForSaleGemsItemID", System.Type.GetType("System.String"))
        _dtForSaleGems.Columns.Add("ForSaleID", System.Type.GetType("System.String"))
        _dtForSaleGems.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtForSaleGems.Columns.Add("GemsName", System.Type.GetType("System.String"))
        _dtForSaleGems.Columns.Add("SaleByDefinePrice", System.Type.GetType("System.Boolean"))

        dcGem = New DataColumn
        dcGem.ColumnName = "YOrCOrG"
        dcGem.DataType = System.Type.GetType("System.String")
        dcGem.DefaultValue = 0
        _dtForSaleGems.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsK"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0

        _dtForSaleGems.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsP"
        dcGem.DataType = System.Type.GetType("System.Int16")
        dcGem.DefaultValue = 0
        _dtForSaleGems.Columns.Add(dcGem)

        dcGem = New DataColumn
        dcGem.ColumnName = "GemsY"
        dcGem.DataType = System.Type.GetType("System.Decimal")
        If numberformat = 1 Then
            dcGem.DefaultValue = "0.0"
        Else
            dcGem.DefaultValue = "0.00"
        End If

        _dtForSaleGems.Columns.Add(dcGem)


        _dtForSaleGems.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtForSaleGems.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))


        _dtForSaleGems.Columns.Add("GemsTW", System.Type.GetType("System.Decimal"))
        '_dtForSaleGems.Columns.Add("Qty", System.Type.GetType("System.Int32"))
        dcQty = New DataColumn
        dcQty.ColumnName = "Qty"
        dcQty.DataType = System.Type.GetType("System.Int16")
        dcQty.DefaultValue = 0
        _dtForSaleGems.Columns.Add(dcQty)

        _dtForSaleGems.Columns.Add("Type", System.Type.GetType("System.String"))
        _dtForSaleGems.Columns.Add("UnitPrice", System.Type.GetType("System.Int64"))
        _dtForSaleGems.Columns.Add("Amount", System.Type.GetType("System.Int64"))
        _dtForSaleGems.Columns.Add("GemsRemark", System.Type.GetType("System.String"))

        grdGems.AutoGenerateColumns = False
        grdGems.DataSource = _dtForSaleGems
        FormatGrdForSalesItemGems()
    End Sub
    Private Sub FormatGrdForSalesItemGems()
        With grdGems
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcGemsID As New DataGridViewTextBoxColumn()
            With dcGemsID
                .HeaderText = "ForSaleGemsItemID"
                .DataPropertyName = "ForSaleGemsItemID"
                .Name = "ForSaleGemsItemID"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGemsID)

            Dim dcForSaleID As New DataGridViewTextBoxColumn()
            With dcForSaleID
                .HeaderText = "ForSaleID"
                .DataPropertyName = "ForSaleID"
                .Name = "ForSaleID"
                .Width = 40
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcForSaleID)

            Dim dcDPrice As New DataGridViewCheckBoxColumn()
            With dcDPrice
                .HeaderText = "SaleByDefinePrice"
                .DataPropertyName = "SaleByDefinePrice"
                .Name = "SaleByDefinePrice"
                .Width = 110
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDPrice)

            Dim dcName As New DataGridViewComboBoxColumn()
            With dcName
                .HeaderText = "Category"
                .DataPropertyName = "GemsCategoryID"
                .Name = "GemsCategoryID"
                .DataSource = _GemsCat.GetAllGemsCategoryForGridCombo
                .DisplayMember = "GemsCategory"
                .ValueMember = "@GemsCategoryID"
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Width = 120
                .Visible = True
            End With
            .Columns.Add(dcName)

            Dim dcGN As New DataGridViewTextBoxColumn()
            With dcGN
                .HeaderText = "Description"
                .DataPropertyName = "GemsName"
                .Name = "GemsName"
                .Width = 110
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Font = New Font("Myanmar3", 9)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGN)

            Dim dcYorCorG As New DataGridViewTextBoxColumn()
            With dcYorCorG
                .HeaderText = "RBP"
                .DataPropertyName = "YOrCOrG"
                .Name = "YOrCOrG"
                .Width = 50
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
                'If Global_IsCarat = 2 Then
                '    .ReadOnly = True
                'End If
            End With
            .Columns.Add(dcYorCorG)

            'Dim dcCarat As New DataGridViewTextBoxColumn()
            'With dcCarat
            '    .HeaderText = "Carat"
            '    .DataPropertyName = "Carat"
            '    .Name = "Carat"
            '    .Width = 50
            '    .Visible = True
            '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '    .SortMode = DataGridViewColumnSortMode.NotSortable
            '    .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            '    If Global_IsCarat = 1 Then
            '        .ReadOnly = True
            '    End If
            'End With
            '.Columns.Add(dcCarat)

            Dim dcGK As New DataGridViewTextBoxColumn()
            With dcGK
                .HeaderText = "K"
                .DataPropertyName = "GemsK"
                .Name = "GemsK"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
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
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcGP)

            Dim dcGY As New DataGridViewTextBoxColumn()
            With dcGY
                .HeaderText = "Y"
                .DataPropertyName = "GemsY"
                .Name = "GemsY"
                .Width = 40
                .Visible = True
                If numberformat = 1 Then
                    .DefaultCellStyle.Format = "0.0"
                    .MaxInputLength = 3
                Else
                    .DefaultCellStyle.Format = "0.00"
                    .MaxInputLength = 4
                End If

                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcGY)

            Dim dcGTK As New DataGridViewTextBoxColumn()
            With dcGTK
                .HeaderText = "GemsTK"
                .DataPropertyName = "GemsTK"
                .Name = "GemsTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGTK)

            Dim dcGTG As New DataGridViewTextBoxColumn()
            With dcGTG
                .HeaderText = "GemsTG"
                .DataPropertyName = "GemsTG"
                .Name = "GemsTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGTG)


            Dim dcGemTW As New DataGridViewTextBoxColumn()
            With dcGemTW
                .HeaderText = "GemsTW"
                .DataPropertyName = "GemsTW"
                .Name = "GemsTW"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGemTW)

            Dim dcQty As New DataGridViewTextBoxColumn()
            With dcQty
                .HeaderText = "Qty"
                .DataPropertyName = "Qty"
                .Name = "Qty"
                .Width = 40
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcQty)

            Dim dctype As New DataGridViewComboBoxColumn()
            With dctype
                .HeaderText = "Fix"
                .DataPropertyName = "Type"
                .Name = "Type"
                .Width = 80
                .Visible = True
                .DefaultCellStyle.Format = ""
                .Items.AddRange(New String() {"Fix", "ByWeight", "ByQty"})
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dctype)

            Dim dcUnitPrice As New DataGridViewTextBoxColumn()
            With dcUnitPrice
                .HeaderText = "UnitPrice"
                .DataPropertyName = "UnitPrice"
                .Name = "UnitPrice"
                .Width = 95
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            End With
            .Columns.Add(dcUnitPrice)

            Dim dcAmount As New DataGridViewTextBoxColumn()
            With dcAmount
                .HeaderText = "Amount"
                .DataPropertyName = "Amount"
                .Name = "Amount"
                .Width = 95
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
                .ReadOnly = True
            End With
            .Columns.Add(dcAmount)

            Dim dcGR As New DataGridViewTextBoxColumn()
            With dcGR
                .HeaderText = "Original Amt"
                .DataPropertyName = "GemsRemark"
                .Name = "GemsRemark"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Font = New Font("Tahoma", 8.25)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGR)
        End With
    End Sub
    Private Function IsFillData() As Boolean

        If chkIsClosed.Checked Then
            Return True
        End If

        If cboStaff.SelectedIndex = -1 Then
            MsgBox("Please Select Staff!", MsgBoxStyle.Information, AppName)
            cboStaff.Focus()
            Return False
        End If

        If chkByFixedPrice.Checked Then
            If txtSaleFixPrice.Text = "0" Then
                MsgBox("Please Check Your Sale Fix Price Amount!", MsgBoxStyle.Information, AppName)
                txtSaleFixPrice.Focus()
                Return False
            End If

            If txtWSFixPrice.Text = "0" Then
                MsgBox("Please Check Your WholeSale Fix Price Amount!", MsgBoxStyle.Information, AppName)
                txtWSFixPrice.Focus()
                Return False
            End If
        End If
        'If cboGoldSmith.SelectedIndex = -1 Then
        '    cboGoldSmith.SelectedText = "DEFAULT"
        '    cboGoldSmith.ValueMember = "20180601-0006"
        'End If



        If chkIsVolume.Checked = False And chkIsLooseDiamond.Checked = False Then
            If cboItemCategory.SelectedIndex = -1 Then
                MsgBox("Please Select ItemCategory!", MsgBoxStyle.Information, AppName)
                cboItemCategory.Focus()
                Return False
            End If

            If cboItemName.SelectedIndex = -1 Then
                MsgBox("Please Select Item Name!", MsgBoxStyle.Information, AppName)
                cboItemName.Focus()
                Return False
            End If

            If cboGoldQuality.SelectedIndex = -1 Then
                MsgBox("Please Select GoldQuality!", MsgBoxStyle.Information, AppName)
                cboGoldQuality.Focus()
                Return False
            End If

            If _IsGram = True Then
                If _ItemTG = 0.0 Then
                    MsgBox("Please Enter Item Gram!", MsgBoxStyle.Information, AppName)
                    txtItemTG.Focus()
                    Return False
                End If
            Else
                If _ItemTK = 0.0 Then
                    MsgBox("Please Enter Item Weight!", MsgBoxStyle.Information, AppName)
                    txtItemK.Focus()
                    Return False
                End If
            End If


            If chkIsOrder.Checked = True Then
                If _OrderReceiveDetailID = "" Then
                    MsgBox("Please Select Order Voucher!", MsgBoxStyle.Information, AppName)
                    btnOrderVoucherSearch.Focus()
                    Return False
                End If
            End If

            If txtItemCode.Text.ToString.Trim = "" Then
                MsgBox("Please Enter BarcodeNo!", MsgBoxStyle.Information, AppName)
                txtItemCode.Focus()
                Return False
            End If


            If _ForSaleID <> "0" Then
                If numberformat = 1 Then
                    Dim obj As New CommonInfo.SalesItemInfo
                    obj = _SalesItemCon.GetForSaleInfo(_ForSaleID)
                    With obj
                        If obj.IsClosed = False Then
                            If obj.IsExit = True Then
                                MsgBox("This BarcodeNo is Already Sale!", MsgBoxStyle.Information, AppName)
                                Return False
                            End If
                        End If
                    End With

                Else
                    Dim obj As New CommonInfo.SalesItemInfo
                    obj = _SalesItemCon.GetForSaleInfoY(_ForSaleID)
                    With obj
                        If obj.IsClosed = False Then
                            If obj.IsExit = True Then
                                MsgBox("This BarcodeNo is Already Sale!", MsgBoxStyle.Information, AppName)
                                Return False
                            End If
                        End If
                    End With
                End If

            End If

            ' for Chein
            If Global_IsCash = False Then

                If txtOriginalFixedPrice.Text = "0" And txtOriginalPriceGram.Text = "0" And txtOriginalPriceTK.Text = "0" Then
                    MsgBox("Please Fill Purchase Price!", MsgBoxStyle.Information, AppName)
                    Return False
                End If

                If IsNumeric(txtPriceCode.Text) Then
                    If CInt(txtPriceCode.Text) > 0 And CInt(txtSaleFixPrice.Text) > 0 Then
                        If CInt(txtPriceCode.Text) > CInt(txtSaleFixPrice.Text) Then
                            MsgBox("Origianl Price is greater than Sale FixPrice!", MsgBoxStyle.Information, AppName)
                            Return False
                        End If
                    End If

                    If CInt(txtPriceCode.Text) > 0 And CInt(txtWSFixPrice.Text) > 0 Then
                        If CInt(txtPriceCode.Text) > CInt(txtWSFixPrice.Text) Then
                            MsgBox("Origianl Price is greater than Whole Sale FixPrice!", MsgBoxStyle.Information, AppName)
                            Return False
                        End If
                    End If

                End If

            End If


            'Add new for reuse barcode at 26.3.2014 11:13 am

            Dim dtItem As DataTable
            If Global_IsReuseBarcode Then
                dtItem = _SalesItemCon.GetSaleItemDataByItemCodeAndForSaleID(txtItemCode.Text, " AND IsExit=0 And ForSaleID <>'" + _ForSaleID + "'")
            Else
                dtItem = _SalesItemCon.GetSaleItemDataByItemCodeAndForSaleID(txtItemCode.Text, " AND ForSaleID <>'" + _ForSaleID + "'")
            End If

            If dtItem.Rows.Count > 0 Then
                MsgBox("Duplicate BarcodeNo!", MsgBoxStyle.Information, AppName)
                txtItemCode.Select()
                Return False
            End If
        ElseIf chkIsLooseDiamond.Checked = True Then
            If cboDCategory.SelectedIndex = -1 Then
                MsgBox("Please Select GemsCategory!", MsgBoxStyle.Information, AppName)
                cboDCategory.Focus()
                Return False
            End If
            If IIf(txtDQty.Text = "", 0, txtDQty.Text) <= 0 Then
                MsgBox("Please Enter Qty!", MsgBoxStyle.Information, AppName)
                txtDQty.Focus()
                Return False
            End If
            If txtDGram.Text = 0.0 Then
                MsgBox("Please Enter RPB/Carat!", MsgBoxStyle.Information, AppName)
                txtRBP.Focus()
                Return False
            End If
        Else
            If cboVItemCategory.SelectedIndex = -1 Then
                MsgBox("Please Select ItemCategory!", MsgBoxStyle.Information, AppName)
                cboVItemCategory.Focus()
                Return False
            End If

            If cboVItemName.SelectedIndex = -1 Then
                MsgBox("Please Select Item Name!", MsgBoxStyle.Information, AppName)
                cboVItemName.Focus()
                Return False
            End If

            If cboVGoldQuality.SelectedIndex = -1 Then
                MsgBox("Please Select GoldQuality!", MsgBoxStyle.Information, AppName)
                cboVGoldQuality.Focus()
                Return False
            End If

            If txtVItemTG.Text = 0.0 Then
                MsgBox("Please Enter Item Weight!", MsgBoxStyle.Information, AppName)
                txtVItemTG.Focus()
                Return False
            End If
            If ChkIsSolidVolume.Checked = False Then
                If txtQTY.Text = "0" Then
                    MsgBox("Please Enter Quantity!", MsgBoxStyle.Information, AppName)
                    txtQTY.Focus()
                    Return False
                End If
            End If
            If txtVItemCode.Text = "" Then
                MsgBox("Please Enter BarcodeNo!", MsgBoxStyle.Information, AppName)
                txtVItemCode.Focus()
                Return False
            End If

            'Add new for reuse barcode at 29.3.2014 9:49 am
            If chkIsVolume.Checked = False Then
                Dim dtVItem As DataTable
                dtVItem = _SalesItemCon.GetSaleItemDataByItemCodeAndForSaleID(txtItemCode.Text, " AND ForSaleID <>'" + _ForSaleID + "'")
                If dtVItem.Rows.Count > 0 Then
                    MsgBox("Duplicate BarcodeNo!", MsgBoxStyle.Information, AppName)
                    txtVItemCode.Focus()
                    Return False
                End If
            End If

        End If
        For Each dr As DataRow In _dtForSaleGems.Rows
            If dr.RowState <> DataRowState.Deleted Then
                If IsDBNull(dr.Item("GemsCategoryID")) Then
                    MsgBox("Please Fill in GemsCategory !", MsgBoxStyle.Information, AppName)
                    Return False
                End If
            End If
        Next
        Return True
    End Function
    Private Sub ShowData_ForSale(ByVal argID As String)
        Dim obj As New CommonInfo.SalesItemInfo

        Dim ObjOrder As New CommonInfo.OrderInvoiceInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim dt As New DataTable
        obj = _SalesItemCon.GetForSaleInfo(argID)
        H_obj = _SalesItemCon.GetForSaleInfo_History(argID)

        With obj
            If (.IsVolume = False And .IsSolidVolume = False And .IsLooseDiamond = False) Then
                chkIsVolume.Checked = .IsVolume
                ChkIsSolidVolume.Checked = .IsSolidVolume
                chkIsLooseDiamond.Checked = .IsLooseDiamond
                chkisNormal.Checked = True
                chkIsLooseDiamond.Enabled = False
                _IsUpdate = True
                _ForSaleID = .ForSaleID
                txtStockID.Text = .ForSaleID
                dtpGivenDate.Value = .GivenDate
                _OldDate = .GivenDate.Date
                cboStaff.SelectedValue = .StaffID

                _ItemNameID = .ItemNameID
                cboItemCategory.SelectedValue = .ItemCategoryID
                cboItemName.SelectedValue = .ItemNameID
                'GetItemName(.ItemNameID)

                cboGoldQuality.SelectedValue = .GoldQualityID
                cboSupplier.SelectedValue = IIf(.SupplierID = "", "0", .SupplierID)
                cboGoldSmith.SelectedValue = IIf(.GoldSmithID = "", "0", .GoldSmithID)
                txtSupplierVou.Text = IIf(.SupplierVou = "", "", .SupplierVou)
                If cboGoldQuality.Text <> "" Then
                    Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
                    GoldQualityInfo = _GoldQCon.GetGoldQuality(cboGoldQuality.SelectedValue)
                    _IsGram = GoldQualityInfo.IsGramRate
                    _Prefix = GoldQualityInfo.Prefix
                    GoldQualityForTextChange()
                End If

                txtItemCode.Text = .ItemCode
                txtLength.Text = .Length
                txtSellingPrice.Text = .SellingPrice
                txtRemark.Text = .Remark
                txtGoldSmith.Text = .GoldSmith

                txtColor.Text = .Color
                cboColor.SelectedValue = .Color
                txtItemK.Text = .ItemK
                txtItemP.Text = .ItemP
                If numberformat = 1 Then
                    txtItemY.Text = Format(.ItemY, "0.0")
                Else
                    txtItemY.Text = Format(.ItemY, "0.00")
                End If
                'txtItemY.Text = Format(.ItemY, "0.0")
                'txtItemTG.Text = Format(Math.Round(.ItemTG, 4), "0.000")
                Dim TT As String = ""
                txtItemTG.Text = Format(Math.Round(.ItemTG, 3), "0.000").ToString
                txtItemTK.Text = Format(.ItemTK, "0.000")
                TT = CStr(Format(Math.Round(.ItemTG, 3), "0.000"))
                _ItemTG = .ItemTG
                txtItemTG.Text = TT
                'txtItemTG.Text = txtItemTG.Tag.ToString()

                _ItemTK = .ItemTK
                txtItemTG.Text = Format(_ItemTG, "0.000")

                txtWasteK.Text = .WasteK
                txtWasteP.Text = .WasteP
                If numberformat = 1 Then
                    txtWasteY.Text = Format(.WasteY, "0.0")
                Else
                    txtWasteY.Text = Format(.WasteY, "0.00")
                End If
                'txtWasteY.Text = Format(.WasteY, "0.0")
                txtWasteTG.Text = Format(.WasteTG, "0.000")
                txtWasteTK.Text = Format(.WasteTK, "0.000")
                _WasteTG = .WasteTG
                _WasteTK = .WasteTK

                txtPWasteK.Text = .PurchaseWasteK
                txtPWasteP.Text = .PurchaseWasteP
                If numberformat = 1 Then
                    txtPWasteY.Text = Format(.PurchaseWasteY, "0.0")
                Else
                    txtPWasteY.Text = Format(.PurchaseWasteY, "0.00")
                End If
                'txtPWasteY.Text = Format(.PurchaseWasteY, "0.0")
                txtPWasteTG.Text = Format(.PurchaseWasteTG, "0.000")
                _PWasteTG = .PurchaseWasteTG
                _PWasteTK = .PurchaseWasteTK

                txtGemK.Text = .GemsK
                txtGemP.Text = .GemsP
                If numberformat = 1 Then
                    txtGemY.Text = Format(.GemsY, "0.0")
                Else
                    txtGemY.Text = Format(.GemsY, "0.00")
                End If
                txtGemTG.Text = Format(.GemsTG, "0.000")
                txtGemsTK.Text = Format(.GemsTK, "0.000")
                _TotalGemsTG = .GemsTG
                _TotalGemsTK = .GemsTK

                CalculateGoldWeight()
                CalculateTotalWeight()


                txtWidth.Text = .Width
                txtDesignCharges.Text = .DesignCharges
                txtMountingCharges.Text = .MountingCharges
                txtPlatingCharges.Text = .PlatingCharges
                txtWhiteCharges.Text = .WhiteCharges
                chkIsVolume.Enabled = False

                If .IsFixPrice = 0 Then
                    chkByFixedPrice.Checked = False
                    txtSaleFixPrice.Text = "0"
                    txtSaleFixPrice.Enabled = False
                    txtWSFixPrice.Text = "0"
                    txtWSFixPrice.Enabled = False
                Else
                    chkByFixedPrice.Checked = True
                    txtSaleFixPrice.Text = .FixPrice
                    txtSaleFixPrice.Enabled = True

                    txtWSFixPrice.Text = .WSFixPrice
                    txtWSFixPrice.Enabled = True
                End If

                If .IsOriginalFixedPrice = True Then
                    txtOriginalFixedPrice.Text = .OriginalFixedPrice
                    txtOriginalFixedPrice.Enabled = True
                    txtOriginalFixedPrice.BackColor = Color.White
                Else
                    txtOriginalFixedPrice.Text = "0"
                    txtOriginalFixedPrice.Enabled = False
                    txtOriginalFixedPrice.BackColor = Color.Gainsboro
                End If
                chkIsOriginalFixedPrice.Checked = .IsOriginalFixedPrice
                If .IsOriginalPriceGram = True Then
                    chkIsOriginalPriceGram.Checked = .IsOriginalPriceGram
                    If .OriginalPriceGram = 0 Then
                        radKPrice.Enabled = True
                        radKPrice.Checked = True
                        txtOriginalPriceTK.Text = .OriginalPriceTK
                        txtOriginalGemsPrice.Text = .OriginalGemsPrice
                        txtOriginalPriceTK.BackColor = Color.White
                        txtOriginalPriceTK.Enabled = True

                        radGramPrice.Checked = False
                        radGramPrice.Enabled = False
                        txtOriginalGemsPrice.Enabled = False
                        txtOriginalGemsPrice.BackColor = Color.Gainsboro
                    Else
                        radGramPrice.Enabled = True
                        radGramPrice.Checked = True
                        txtOriginalPriceGram.Text = .OriginalPriceGram
                        txtOriginalPriceGram.BackColor = Color.White
                        txtOriginalPriceGram.Enabled = True

                        radKPrice.Checked = False
                        radKPrice.Enabled = False
                        txtOriginalPriceTK.Enabled = False
                        txtOriginalPriceTK.BackColor = Color.Gainsboro
                    End If

                    txtOriginalGemsPrice.Text = .OriginalGemsPrice
                    txtOriginalOtherPrice.Text = .OriginalOtherPrice
                    txtOriginalGemsPrice.BackColor = Color.White
                    txtOriginalOtherPrice.BackColor = Color.Gainsboro

                    txtOriginalGemsPrice.ReadOnly = False
                    txtOriginalGemsPrice.Enabled = True

                End If

                If .Photo <> "" Then
                    Try
                        lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + .Photo)
                        PName = .Photo
                        btnAdd.Text = "Remove"
                        lblPhoto.Visible = False
                    Catch ex As Exception
                        lblItemImage.Image = Nothing
                        PName = ""
                    End Try

                End If
                _IsExit = .IsExit
                If (_IsExit) Then
                    'btnDelete.Enabled = False
                    'chkIsClosed.Enabled = False
                    'chkIsOrder.Enabled = False
                    'btnOrderVoucherSearch.Enabled = False
                Else
                    btnDelete.Enabled = True
                    If obj.IsOrder Then
                        chkIsClosed.Enabled = False
                    Else
                        chkIsClosed.Enabled = True
                    End If
                End If
                chkIsOrder.Checked = .IsOrder
                chkIsClosed.Checked = .IsClosed
                If (.IsClosed = True) Then
                    'btnSave.Enabled = False
                    'btnDelete.Enabled = False
                    'chkIsOrder.Enabled = False
                    'tabStock.Enabled = False
                    'dtpGivenDate.Enabled = False
                    'cboStaff.Enabled = False
                    'btnNew.Enabled = True
                    chkIsClosed.Enabled = True
                End If

                _OldIsOrder = .IsOrder
                If (.IsOrder = True) Then
                    _OldOrderReceiveDetailID = .OrderReceiveDetailID
                    btnOrderVoucherSearch.Visible = True

                    If chkIsOrder.Enabled = False Then
                        btnOrderVoucherSearch.Enabled = False
                    Else
                        btnOrderVoucherSearch.Enabled = True
                    End If

                    lblOrderInvoiceiD.Visible = True
                    lblOrderDate.Visible = True
                    lblCustomer.Visible = True
                    _OrderReceiveDetailID = .OrderReceiveDetailID
                    ObjOrder = _OrderInvoiceController.GetOrderInvoiceInfoByDetailID(_OrderReceiveDetailID)
                    lblOrderInvoiceiD.Text = ObjOrder.OrderInvoiceID
                    lblOrderDate.Text = Format(ObjOrder.OrderDate, "dd-MM-yyyy")
                    lblCustomer.Text = ObjOrder.CustomerName
                    cboGoldQuality.Enabled = False
                    cboItemCategory.Enabled = False
                    cboItemName.Enabled = False
                    chkIsDiamond.Enabled = False
                Else
                    btnOrderVoucherSearch.Visible = False
                    lblOrderInvoiceiD.Visible = False
                    lblOrderDate.Visible = False
                    lblCustomer.Visible = False
                    cboGoldQuality.Enabled = True
                    chkIsDiamond.Enabled = True
                End If
                chkIsDiamond.Checked = .IsDiamond
                txtOriginalCode.Text = .OriginalCode
                txtPriceCode.Text = .PriceCode
                txtTotalCost.Text = .TotalCost
                txtSellingRate.Text = .SellingRate
                txtWSFixPrice.Text = .WSFixPrice
                chkIsLooseDiamond.Enabled = False
                chkIsVolume.Enabled = False
            ElseIf .IsLooseDiamond = True Then
                chkisNormal.Checked = False
                chkIsVolume.Checked = .IsVolume
                chkIsLooseDiamond.Checked = True
                chkisNormal.Enabled = False
                chkIsVolume.Enabled = False
                If .IsFixPrice Then
                    chkDFixPrice.Checked = True
                    txtDFixPrice.Text = .FixPrice
                    txtDFixPrice.Enabled = True
                    txtDFixPrice.BackColor = Color.White
                Else
                    chkDFixPrice.Checked = False
                    txtDFixPrice.Text = 0
                    txtDFixPrice.Enabled = False
                    txtDFixPrice.BackColor = Color.Gainsboro
                End If
                _IsUpdate = True
                _ForSaleID = .ForSaleID
                txtStockID.Text = .ForSaleID

                dtpGivenDate.Value = .GivenDate
                _OldDate = .GivenDate.Date
                txtDQty.Text = .LossQTY
                cboStaff.SelectedValue = .StaffID
                txtRemark.Text = .Remark
                cboDCategory.SelectedValue = .SDGemsCategoryID
                'Call RefreshItemNameCboByVolume(.ItemCategoryID)
                txtDOCode.Text = .OriginalCode
                txtDescription.Text = .SDGemsName
                cboDShape.SelectedValue = .Shape
                cboDClarity.SelectedValue = .Clarity
                cboDColor.SelectedValue = .Color
                txtDBarcode.Text = .ItemCode

                txtDGram.Text = Format(.LossItemTG, "0.000")
                _DItemTK = .LossItemTK
                _DItemTG = .LossItemTG
                txtRBP.Text = .SDYOrCOrG

                If .IsOriginalFixedPrice = True Then

                    txtDOriginalFixPrice.Text = .OriginalFixedPrice
                    txtDOriginalFixPrice.Enabled = True
                    txtDOriginalFixPrice.BackColor = Color.White
                Else
                    txtDOriginalFixPrice.Text = "0"
                    txtDOriginalFixPrice.Enabled = False
                    txtDOriginalFixPrice.BackColor = Color.Gainsboro
                End If
                chkDOriginalFixPrice.Checked = .IsOriginalFixedPrice

                If .IsOriginalPriceCarat = True Then
                    chkOriginalPriceCarat.Checked = .IsOriginalPriceCarat
                    txtOriginalPriceCarat.Text = .OriginalPriceCarat
                    txtOriginalPriceCarat.Enabled = True
                    txtOriginalPriceCarat.BackColor = Color.White
                Else
                    chkOriginalPriceCarat.Checked = .IsOriginalPriceCarat
                    txtOriginalPriceCarat.Text = 0
                    txtOriginalPriceCarat.Enabled = False
                    txtOriginalPriceCarat.BackColor = Color.Gainsboro
                End If

                If .Photo <> "" Then
                    Try
                        lblVItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + .Photo)
                        VPName = .Photo
                        btnVAdd.Text = "Remove"
                        lblVPhoto.Visible = False
                    Catch ex As Exception
                        lblItemImage.Image = Nothing
                        PName = ""
                    End Try
                End If
                _IsExit = .IsExit
                If (_IsExit) Then
                    'chkIsClosed.Enabled = False
                    chkIsOrder.Enabled = False
                    btnOrderVoucherSearch.Enabled = False
                Else
                    chkIsClosed.Enabled = True
                End If

                chkIsOrder.Checked = .IsOrder
                chkIsClosed.Checked = .IsClosed
                If (.IsClosed = True) Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    chkIsOrder.Enabled = False
                End If
                'chkIsClosed.Enabled = False

                _OldIsOrder = .IsOrder
                If (.IsOrder = True) Then
                    _OldOrderReceiveDetailID = .OrderReceiveDetailID
                    btnOrderVoucherSearch.Visible = True
                    If chkIsOrder.Enabled = False Then
                        btnOrderVoucherSearch.Enabled = False
                    Else
                        btnOrderVoucherSearch.Enabled = True
                    End If

                    lblOrderInvoiceiD.Visible = True
                    lblOrderDate.Visible = True
                    _OrderReceiveDetailID = .OrderReceiveDetailID

                    ObjOrder = _OrderInvoiceController.GetOrderInvoiceInfoByDetailID(_OrderReceiveDetailID)
                    lblOrderInvoiceiD.Text = ObjOrder.OrderInvoiceID
                    lblOrderDate.Text = Format(ObjOrder.OrderDate, "dd-MM-yyyy")
                    lblCustomer.Text = ObjOrder.CustomerName
                    cboGoldQuality.Enabled = False
                Else
                    btnOrderVoucherSearch.Visible = False
                    lblOrderInvoiceiD.Visible = False
                    lblOrderDate.Visible = False
                    cboGoldQuality.Enabled = True
                End If
                txtDDesignCharges.Text = .DesignCharges
                txtDMountingCharges.Text = .MountingCharges
                txtDPlatingCharges.Text = .PlatingCharges
                txtDWhiteCharges.Text = .WhiteCharges
                cboDSupplier.SelectedValue = .SupplierID
                txtDSellingRate.Text = .SellingRate
                txtDVoucherNo.Text = .SupplierVou
                txtDOriginalPrice.Text = .PriceCode
                Dim GemsItemCatInfo As CommonInfo.GemsCategoryInfo
                GemsItemCatInfo = _GemsCat.GetGemsCategory(cboDCategory.SelectedValue)
                _DPrefix = GemsItemCatInfo.Prefix
                _DCarat = _DItemTG * Global_GramToKarat

            Else
                Dim dtVolume As DataTable
                If .IsSolidVolume = True Then
                    ChkIsSolidVolume.Checked = .IsSolidVolume
                    chkIsVolume.Checked = .IsSolidVolume
                Else
                    ChkIsSolidVolume.Checked = .IsSolidVolume
                    chkIsVolume.Checked = .IsVolume
                End If
                chkIsLooseDiamond.Checked = .IsLooseDiamond
                chkisNormal.Checked = False
                chkisNormal.Enabled = False
                chkIsLooseDiamond.Enabled = False
                _IsUpdate = True
                _ForSaleID = .ForSaleID
                txtStockID.Text = .ForSaleID
                dtpGivenDate.Value = .GivenDate
                _OldDate = .GivenDate.Date
                txtQTY.Text = .LossQTY
                cboStaff.SelectedValue = .StaffID
                txtRemark.Text = .Remark
                cboVItemCategory.SelectedValue = .ItemCategoryID
                Call RefreshItemNameCboByVolume(.ItemCategoryID)
                cboVGoldQuality.SelectedValue = .GoldQualityID
                cboVItemName.SelectedValue = .ItemNameID

                If cboVGoldQuality.Text <> "" Then
                    Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
                    GoldQualityInfo = _GoldQCon.GetGoldQuality(cboVGoldQuality.SelectedValue)
                    _VIsGram = GoldQualityInfo.IsGramRate
                    GoldQualityForTextChange()
                End If

                txtVItemCode.Text = .ItemCode
                txtVLength.Text = .Length
                txtVSellingPrice.Text = .SellingPrice

                txtVItemTG.Text = Format(.LossItemTG, "0.000")
                txtVItemTK.Text = Format(.LossItemTK, "0.000")
                If txtVItemTK.Text <> "0" Then
                    GoldWeight.GoldTK = .LossItemTK
                    GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                    txtVItemK.Text = CStr(GoldWeight.WeightK)
                    txtVItemP.Text = CStr(GoldWeight.WeightP)
                    If numberformat = 1 Then
                        txtVItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                    Else
                        txtVItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                    End If
                    'txtVItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                End If

                _VItemTG = .LossItemTG
                _VItemTK = .LossItemTK

                txtVWidth.Text = .Width

                If .IsFixPrice = 0 Then
                    chkVByFixPrice.Checked = False
                    txtVFixedPrice.Text = "0"
                    txtVFixedPrice.Enabled = False
                Else
                    chkVByFixPrice.Checked = True
                    txtVFixedPrice.Text = .FixPrice
                    txtVFixedPrice.Enabled = True

                End If

                If .IsOriginalFixedPrice = True Then

                    txtVOriginalFixPrice.Text = .OriginalFixedPrice
                    txtVOriginalFixPrice.Enabled = True
                    txtVOriginalFixPrice.BackColor = Color.White
                Else
                    txtVOriginalFixPrice.Text = "0"
                    txtVOriginalFixPrice.Enabled = False
                    txtVOriginalFixPrice.BackColor = Color.Gainsboro
                End If
                chkVIsOriginalFixedPrice.Checked = .IsOriginalFixedPrice

                If .IsOriginalPriceGram = True Then
                    chkVIsOriginalPriceGram.Checked = .IsOriginalPriceGram
                    If .OriginalPriceGram = 0 Then
                        radVKPrice.Enabled = True
                        radVKPrice.Checked = True
                        txtVOriginalPriceKyat.Text = .OriginalPriceTK
                        txtVOriginalPriceKyat.BackColor = Color.White
                        'txtOriginalPriceTK.ReadOnly = False
                        txtVOriginalPriceKyat.Enabled = True

                        radVGramPrice.Checked = False
                        radVGramPrice.Enabled = False
                        ' txtOriginalGemsPrice.ReadOnly = True
                        txtVOriginalGemPrice.Enabled = False
                        txtVOriginalGemPrice.BackColor = Color.Gainsboro
                    Else
                        radVGramPrice.Enabled = True
                        radVGramPrice.Checked = True
                        txtVOriginalPriceGram.Text = .OriginalPriceGram

                        txtVOriginalPriceGram.BackColor = Color.White
                        'txtOriginalPriceGram.ReadOnly = False
                        txtVOriginalPriceGram.Enabled = True

                        radVKPrice.Checked = False
                        radVKPrice.Enabled = False
                        'txtOriginalPriceTK.ReadOnly = True
                        txtVOriginalPriceKyat.Enabled = False
                        txtVOriginalPriceKyat.BackColor = Color.Gainsboro
                    End If

                    txtVOriginalGemPrice.Text = .OriginalGemsPrice
                    txtVOriginalOtherPrice.Text = .OriginalOtherPrice

                    txtVOriginalGemPrice.BackColor = Color.White
                    txtVOriginalOtherPrice.BackColor = Color.White

                    txtVOriginalGemPrice.ReadOnly = False
                    txtVOriginalOtherPrice.ReadOnly = False
                    txtVOriginalGemPrice.Enabled = True
                    txtVOriginalOtherPrice.Enabled = True

                End If

                If .Photo <> "" Then
                    Try
                        lblVItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + .Photo)
                        VPName = .Photo
                        btnVAdd.Text = "Remove"
                        lblVPhoto.Visible = False
                    Catch ex As Exception
                        lblItemImage.Image = Nothing
                        PName = ""
                    End Try
                End If
                _IsExit = .IsExit
                If (_IsExit) Then
                    'chkIsClosed.Enabled = False
                    chkIsOrder.Enabled = False
                    btnOrderVoucherSearch.Enabled = False
                Else
                    chkIsClosed.Enabled = True
                End If
                chkIsOrder.Checked = .IsOrder
                chkIsClosed.Checked = .IsClosed
                If (.IsClosed = True) Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    chkIsOrder.Enabled = False
                End If
                'chkIsClosed.Enabled = False

                _OldIsOrder = .IsOrder
                If (.IsOrder = True) Then
                    _OldOrderReceiveDetailID = .OrderReceiveDetailID
                    btnOrderVoucherSearch.Visible = True
                    If chkIsOrder.Enabled = False Then
                        btnOrderVoucherSearch.Enabled = False
                    Else
                        btnOrderVoucherSearch.Enabled = True
                    End If

                    lblOrderInvoiceiD.Visible = True
                    lblOrderDate.Visible = True
                    _OrderReceiveDetailID = .OrderReceiveDetailID

                    ObjOrder = _OrderInvoiceController.GetOrderInvoiceInfoByDetailID(_OrderReceiveDetailID)
                    lblOrderInvoiceiD.Text = ObjOrder.OrderInvoiceID
                    lblOrderDate.Text = Format(ObjOrder.OrderDate, "dd-MM-yyyy")
                    lblCustomer.Text = ObjOrder.CustomerName
                    cboGoldQuality.Enabled = False
                Else
                    btnOrderVoucherSearch.Visible = False
                    lblOrderInvoiceiD.Visible = False
                    lblOrderDate.Visible = False
                    cboGoldQuality.Enabled = True
                End If

                dtVolume = _SalesVolumeCon.GetSalesVolumeDateByForSaleID(txtStockID.Text)
                If dtVolume.Rows.Count > 0 Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                End If

            End If
        End With
    End Sub
    Private Sub GetItemName(ByVal ItemNameID As String)
        cboItemName.Text = _ItemNameCon.GetItemName(ItemNameID).ItemName
    End Sub
    Private Function GetDataForSale() As CommonInfo.SalesItemInfo
        Dim objForSale As New CommonInfo.SalesItemInfo
        If chkIsVolume.Checked = False And chkIsLooseDiamond.Checked = False Then
            With objForSale
                .ForSaleID = _ForSaleID
                .StaffID = cboStaff.SelectedValue
                .GivenDate = dtpGivenDate.Value
                .ItemCode = txtItemCode.Text
                .ItemNameID = cboItemName.SelectedValue
                .Length = IIf(txtLength.Text = "", "-", txtLength.Text)
                .GoldQualityID = cboGoldQuality.SelectedValue
                .ItemCategoryID = cboItemCategory.SelectedValue
                .SupplierID = IIf(cboSupplier.SelectedValue = "", "0", cboSupplier.SelectedValue)
                .SupplierVou = IIf(txtSupplierVou.Text = "", "", txtSupplierVou.Text)
                .GoldSmithID = IIf(cboGoldSmith.SelectedValue = "", "0", cboGoldSmith.SelectedValue)

                .LocationID = _LocationID
                .GoldTK = _GoldTK
                .GoldTG = _GoldTG
                .GemsTK = _TotalGemsTK
                .GemsTG = _TotalGemsTG
                .WasteTK = _WasteTK
                .WasteTG = _WasteTG
                .ItemTK = _ItemTK
                '.ItemTG = _ItemTG
                .ItemTG = txtItemTG.Text

                .TotalTK = _TotalTK
                .TotalTG = _TotalTG
                .PurchaseWasteTG = _PWasteTG
                .PurchaseWasteTK = _PWasteTK
                .Width = IIf(txtWidth.Text = "", "-", txtWidth.Text)
                .IsExit = _IsExit
                .SellingPrice = IIf(txtSellingPrice.Text = "", "-", txtSellingPrice.Text)
                .DesignCharges = IIf(txtDesignCharges.Text = "", "0", txtDesignCharges.Text)
                .PlatingCharges = IIf(txtPlatingCharges.Text = "", "0", txtPlatingCharges.Text)
                .MountingCharges = IIf(txtMountingCharges.Text = "", "0", txtMountingCharges.Text)
                .WhiteCharges = IIf(txtWhiteCharges.Text = "", "0", txtWhiteCharges.Text)
                .IsFixPrice = chkByFixedPrice.Checked
                .FixPrice = IIf(txtSaleFixPrice.Text = "", "0", txtSaleFixPrice.Text)
                .IsOriginalFixedPrice = chkIsOriginalFixedPrice.Checked
                .OriginalFixedPrice = IIf(txtOriginalFixedPrice.Text = "", "0", txtOriginalFixedPrice.Text)
                .IsOriginalPriceGram = chkIsOriginalPriceGram.Checked
                .OriginalPriceGram = IIf(txtOriginalPriceGram.Text = "", "0", txtOriginalPriceGram.Text)
                .OriginalPriceTK = IIf(txtOriginalPriceTK.Text = "", "0", txtOriginalPriceTK.Text)
                .OriginalGemsPrice = IIf(txtOriginalGemsPrice.Text = "", "0", txtOriginalGemsPrice.Text)
                .OriginalOtherPrice = IIf(txtOriginalOtherPrice.Text = "", "0", txtOriginalOtherPrice.Text)
                .OriginalPrice = 0
                .Photo = PName
                .IsClosed = chkIsClosed.Checked
                .IsOrder = chkIsOrder.Checked
                .OldIsOrder = _OldIsOrder
                .OldOrderReceiveDetailID = _OldOrderReceiveDetailID
                .Color = IIf(txtColor.Text = "", IIf(cboColor.SelectedValue = "", "", cboColor.SelectedValue), txtColor.Text)
                If chkIsOrder.Checked Then
                    .OrderReceiveDetailID = _OrderReceiveDetailID
                Else
                    .OrderReceiveDetailID = ""
                End If
                .QTY = IIf(txtQTY.Text = "", "0", txtQTY.Text)
                .IsVolume = chkIsVolume.Checked
                .LossItemTK = "0"
                .LossItemTG = "0"
                .LossQTY = "0"
                .TotalGemPrice = _GrdGemPrice
                .GoldSmith = txtGoldSmith.Text
                .Remark = txtRemark.Text
                .IsDiamond = chkIsDiamond.Checked
                .OriginalCode = txtOriginalCode.Text
                .PriceCode = txtPriceCode.Text
                .TotalCost = txtTotalCost.Text

                .ItemK = txtItemK.Text
                .ItemP = txtItemP.Text
                .ItemY = txtItemY.Text

                .WasteK = txtWasteK.Text
                .WasteP = txtWasteP.Text
                .WasteY = txtWasteY.Text
                .SellingRate = txtSellingRate.Text
                .WSFixPrice = txtWSFixPrice.Text

                .Shape = ""
                .Clarity = ""
                .SDGemsName = ""
                .SDYOrCOrG = 0
                .SDGemsTW = 0.0
                .IsLooseDiamond = 0
                .OriginalPriceCarat = 0
                .IsOriginalPriceCarat = 0
                .SDGemsCategoryID = ""
            End With
        ElseIf chkIsLooseDiamond.Checked = True Then
            With objForSale
                .ForSaleID = _ForSaleID
                .GivenDate = dtpGivenDate.Value
                .ItemCode = txtDBarcode.Text
                .StaffID = cboStaff.SelectedValue
                .SDGemsCategoryID = cboDCategory.SelectedValue
                .ItemCategoryID = ""
                .ItemNameID = ""
                .LocationID = _LocationID
                .GoldTK = 0
                .GoldTG = 0
                .GemsTK = 0
                .GemsTG = 0
                .WasteTK = 0
                .WasteTG = 0
                .PurchaseWasteTG = 0
                .PurchaseWasteTK = 0
                .ItemTK = _DItemTK
                .ItemTG = _DItemTG
                .TotalTK = 0
                .TotalTG = 0
                .Width = ""
                .IsExit = _IsExit
                .SellingPrice = IIf(txtVSellingPrice.Text = "", "-", txtVSellingPrice.Text)
                .DesignCharges = txtDDesignCharges.Text
                .PlatingCharges = txtDPlatingCharges.Text
                .MountingCharges = txtDMountingCharges.Text
                .WhiteCharges = txtWhiteCharges.Text
                .IsFixPrice = chkDFixPrice.Checked
                .FixPrice = IIf(txtDFixPrice.Text = "", "0", txtDFixPrice.Text)
                .IsOriginalFixedPrice = chkDOriginalFixPrice.Checked
                .OriginalFixedPrice = IIf(txtDOriginalFixPrice.Text = "", "0", txtDOriginalFixPrice.Text)
                .IsOriginalPriceGram = 0
                .OriginalPriceGram = 0
                .OriginalPriceTK = 0
                .OriginalGemsPrice = 0
                .OriginalOtherPrice = 0
                .SupplierID = IIf(cboDSupplier.SelectedValue = "", "0", cboDSupplier.SelectedValue)
                .SupplierVou = IIf(txtDVoucherNo.Text = "", "", txtDVoucherNo.Text)
                .GoldSmithID = ""

                .OriginalPrice = 0
                .Photo = IIf(DPName = "", " ", DPName)
                .IsClosed = chkIsClosed.Checked
                .IsOrder = chkIsOrder.Checked
                .OldIsOrder = _OldIsOrder
                .OldOrderReceiveDetailID = _OldOrderReceiveDetailID
                If chkIsOrder.Checked Then
                    .OrderReceiveDetailID = _OrderReceiveDetailID
                Else
                    .OrderReceiveDetailID = ""
                End If
                .QTY = IIf(txtDQty.Text = "", "0", txtDQty.Text)
                .Length = "-"
                .GoldQualityID = ""
                .IsVolume = 0
                .IsSolidVolume = 0
                .LossItemTK = _DItemTK
                .LossItemTG = _DItemTG
                .LossQTY = IIf(txtDQty.Text = "", "0", txtDQty.Text)
                .GoldSmith = ""
                .Remark = txtRemark.Text
                .OriginalCode = IIf(IsDBNull(txtDOCode.Text), "", txtDOCode.Text)
                .PriceCode = IIf(IsDBNull(txtDOriginalPrice.Text), "", txtDOriginalPrice.Text)
                .Color = IIf(IsDBNull(cboDColor.SelectedValue), "", cboDColor.SelectedValue)
                .Shape = IIf(IsDBNull(cboDShape.SelectedValue), "", cboDShape.SelectedValue)
                .Clarity = IIf(IsDBNull(cboDClarity.SelectedValue), "", cboDClarity.SelectedValue)
                .SDGemsName = IIf(IsDBNull(txtDescription.Text), "", txtDescription.Text)
                .SDYOrCOrG = _DRBP
                .SDGemsTW = _SDGemsTW
                .WSFixPrice = 0
                .IsLooseDiamond = chkIsLooseDiamond.Checked
                .OriginalPriceCarat = IIf(IsDBNull(txtOriginalPriceCarat.Text), 0, txtOriginalPriceCarat.Text)
                .IsOriginalPriceCarat = chkOriginalPriceCarat.Checked
                .SellingRate = txtDSellingRate.Text
                .TotalCost = 0

                '.QTY = txtDQty.Text
            End With
        Else
            With objForSale
                .ForSaleID = _ForSaleID
                .GivenDate = dtpGivenDate.Value
                .ItemCode = txtVItemCode.Text
                .StaffID = cboStaff.SelectedValue
                .ItemNameID = cboVItemName.SelectedValue
                .Length = IIf(txtVLength.Text = "", "-", txtVLength.Text)
                .GoldQualityID = cboVGoldQuality.SelectedValue
                .ItemCategoryID = cboVItemCategory.SelectedValue
                .LocationID = _LocationID
                .GoldTK = 0
                .GoldTG = 0
                .GemsTK = 0
                .GemsTG = 0
                .WasteTK = 0
                .WasteTG = 0
                .PurchaseWasteTG = 0
                .PurchaseWasteTK = 0
                .ItemTK = _VItemTK
                .ItemTG = _VItemTG
                .TotalTK = 0
                .TotalTG = 0
                .Width = IIf(txtVWidth.Text = "", "-", txtVWidth.Text)
                .IsExit = _IsExit
                .SellingPrice = IIf(txtVSellingPrice.Text = "", "-", txtVSellingPrice.Text)
                .DesignCharges = "0"
                .PlatingCharges = "0"
                .MountingCharges = "0"
                .WhiteCharges = "0"
                .IsFixPrice = chkVByFixPrice.Checked
                .FixPrice = IIf(txtVFixedPrice.Text = "", "0", txtVFixedPrice.Text)
                .IsOriginalFixedPrice = chkVIsOriginalFixedPrice.Checked
                .OriginalFixedPrice = IIf(txtVOriginalFixPrice.Text = "", "0", txtVOriginalFixPrice.Text)
                .IsOriginalPriceGram = chkVIsOriginalPriceGram.Checked
                .OriginalPriceGram = IIf(txtVOriginalPriceGram.Text = "", "0", txtVOriginalPriceGram.Text)
                .OriginalPriceTK = IIf(txtVOriginalPriceKyat.Text = "", "0", txtVOriginalPriceKyat.Text)
                .OriginalGemsPrice = IIf(txtVOriginalGemPrice.Text = "", "0", txtVOriginalGemPrice.Text)
                .OriginalOtherPrice = IIf(txtVOriginalOtherPrice.Text = "", "0", txtVOriginalOtherPrice.Text)
                .SupplierID = IIf(cboSupplier.SelectedValue = "", "0", cboSupplier.SelectedValue)
                .SupplierVou = IIf(txtSupplierVou.Text = "", "", txtSupplierVou.Text)
                .GoldSmithID = IIf(cboGoldSmith.SelectedValue = "", "0", cboGoldSmith.SelectedValue)

                .OriginalPrice = 0
                .Photo = VPName
                .IsClosed = chkIsClosed.Checked
                .IsOrder = chkIsOrder.Checked
                .OldIsOrder = _OldIsOrder
                .OldOrderReceiveDetailID = _OldOrderReceiveDetailID
                If chkIsOrder.Checked Then
                    .OrderReceiveDetailID = _OrderReceiveDetailID
                Else
                    .OrderReceiveDetailID = ""
                End If
                .QTY = IIf(txtQTY.Text = "", "0", txtQTY.Text)
                If ChkIsSolidVolume.Checked = True Then
                    .IsVolume = 0
                    .IsSolidVolume = ChkIsSolidVolume.Checked
                Else
                    .IsVolume = chkIsVolume.Checked
                    .IsSolidVolume = 0
                End If
                .LossItemTK = _VItemTK
                .LossItemTG = _VItemTG
                .LossQTY = IIf(txtQTY.Text = "", "0", txtQTY.Text)
                .GoldSmith = ""
                .Remark = txtRemark.Text
                .OriginalCode = IIf(IsDBNull(txtOriginalCode.Text), "", txtOriginalCode.Text)
                .PriceCode = IIf(IsDBNull(txtPriceCode.Text), "", txtPriceCode.Text)
                .Color = IIf(IsDBNull(txtColor.Text), "", txtColor.Text)
                .Shape = ""
                .Clarity = ""
                .SDGemsName = ""
                .SDYOrCOrG = 0
                .SDGemsTW = 0.0
                .WSFixPrice = 0
                .IsLooseDiamond = 0
                .OriginalPriceCarat = 0
                .IsOriginalPriceCarat = 0
                .SDGemsCategoryID = ""
                .WSFixPrice = 0
                .TotalCost = IIf(txtTotalCost.Text = "", "0", txtTotalCost.Text)
            End With

        End If

        Return objForSale
    End Function

    Private Sub ShowVolumeBarcodeData(ByVal VItemCode As String)

        If txtVItemCode.Text <> "" Then
            Dim frm As New FShowBarcodeItemData
            frm.IsVolume = chkIsVolume.Checked
            frm.VBarcodeNo = txtVItemCode.Text
            frm.VGoldGram = txtVItemTG.Text
            frm.VGoldTK = txtVItemTK.Text
            frm.VQTY = txtQTY.Text
            frm.VGoldWgt = txtVItemK.Text + "-" + txtVItemP.Text + "-" + CStr(CDec(txtVItemY.Text))
            frm.VPrefix = _VPrefix
            frm.VIsGram = _VIsGram
            frm.VLength = txtVLength.Text
            If chkVByFixPrice.Checked Then
                frm.VFixPrice = txtVFixedPrice.Text
            Else
                frm.VFixPrice = "0"
            End If
            frm.ShowDialog()
        End If
    End Sub

    Private Sub ShowDiamondBarcodeData(ByVal DItemCode As String)

        If txtDBarcode.Text <> "" Then
            Dim frm As New FShowBarcodeItemData
            frm.IsLooseDiamond = chkIsLooseDiamond.Checked
            frm.DBarcodeNo = txtDBarcode.Text
            frm.DGemsGram = txtDGram.Text
            frm.DQty = txtDQty.Text
            frm.DWgt = txtRBP.Text
            frm.DPrefix = _DPrefix
            frm.DColor = cboDColor.SelectedValue
            frm.DShape = cboDShape.SelectedValue
            frm.DClarity = cboDClarity.SelectedValue
            If chkDFixPrice.Checked Then
                frm.DFixPrice = txtDFixPrice.Text
            Else
                frm.DFixPrice = "0"
            End If
            frm.ShowDialog()
        End If
    End Sub

    Private Sub ShowBarcodeData(ByVal ItemCode As String, ByVal Gram As String, ByVal Length As String, ByVal Prefix As String)

        If txtItemCode.Text <> "" Then
            Dim frm As New FShowBarcodeItemData
            frm.dtStoneData = _dtForSaleGems


            frm.BarcodeData = txtGoldK.Text + "-" + txtGoldP.Text + "-" + CStr(CDec(txtGoldY.Text))
            frm.ItemData = txtItemK.Text + "-" + txtItemP.Text + "-" + CStr(CDec(txtItemY.Text))
            frm.GemKPY = txtGemK.Text + "-" + txtGemP.Text + "-" + CStr(CDec(txtGemY.Text))
            If CDec(txtWasteTG.Text) > 0.0 Then
                frm.WasteWgt = txtWasteK.Text + "-" + txtWasteP.Text + "-" + CStr(CDec(txtWasteY.Text))
                frm.StrWasteWg = IIf(txtWasteK.Text = 0, "", txtWasteK.Text + "K ") + IIf(txtWasteP.Text = 0, "", txtWasteP.Text + "P ") + CStr(IIf(CDec(txtWasteY.Text) = 0, "", CStr(CDec(txtWasteY.Text)) + "Y"))
            Else
                frm.WasteWgt = ""
            End If


            frm.WasteWgt = txtWasteK.Text + "-" + txtWasteP.Text + "-" + CStr(CDec(txtWasteY.Text))
            frm.StrPWaste = IIf(txtPWasteK.Text = 0, "", txtPWasteK.Text + "K ") + IIf(txtPWasteP.Text = 0, "", txtPWasteP.Text + "P ") + CStr(IIf(CDec(txtPWasteY.Text) = 0, "", CStr(CDec(txtPWasteY.Text)) + "Y"))

            frm.StrItemWg = IIf(txtItemK.Text = 0, "", txtItemK.Text + "K ") + IIf(txtItemP.Text = 0, "", txtItemP.Text + "P ") + CStr(IIf(CDec(txtItemY.Text) = 0, "", CStr(CDec(txtItemY.Text)) + "Y"))
            frm.StrGemsWg = IIf(txtGemK.Text = 0, "", txtGemK.Text + "K ") + IIf(txtGemP.Text = 0, "", txtGemP.Text + "P ") + CStr(IIf(CDec(txtGemY.Text) = 0, "", CStr(CDec(txtGemY.Text)) + "Y"))
            frm.StrGoldWg = IIf(txtGoldK.Text = 0, "", txtGoldK.Text + "K ") + IIf(txtGoldP.Text = 0, "", txtGoldP.Text + "P ") + CStr(IIf(CDec(txtGoldY.Text) = 0, "", CStr(CDec(txtGoldY.Text)) + "Y"))

            'Me.txtItemCode.Font = New Font("Times New Roman", 12)
            frm.BarcodeNo = txtItemCode.Text
            frm.GoldQ = cboGoldQuality.Text
            frm.Prefix = _Prefix

            If txtLength.Text <> "-" And txtLength.Text <> "" Then
                frm.Length = txtLength.Text
            End If

            If txtWidth.Text <> "-" And txtWidth.Text <> "" Then
                frm.Length += txtWidth.Text
            End If

            frm.IsGram = _IsGram
            frm.IsDiamond = chkIsDiamond.Checked

            frm.GemGram = Math.Round(Val(txtGemTG.Text), 3)
            frm.GoldGramWgt = Math.Round(Val(txtGoldTG.Text), 3)
            frm.WasteGramWgt = Math.Round(Val(txtWasteTG.Text), 3)
            frm.ItemGramData = Math.Round(Val(txtItemTG.Text), 3)
            frm.TotalGemPrice = _GrdGemPrice
            frm.TotalGemQTY = _GrdGemQTY
            frm.Totalct = _Totalct
            frm.OriginalCode = txtOriginalCode.Text
            frm.GoldSmith = txtGoldSmith.Text
            frm.Color = txtColor.Text
            frm.GoldQualityID = Me.cboGoldQuality.SelectedValue
            If IsNumeric(txtPriceCode.Text) Then
                frm.PriceCode = Format(Val(txtPriceCode.Text), "###,##0.##")
            Else
                frm.PriceCode = txtPriceCode.Text
            End If

            If chkByFixedPrice.Checked Then
                frm.FixPrice = Format(Val(txtSaleFixPrice.Text), "###,##0.##")
            Else
                frm.FixPrice = 0
            End If
            If chkDouble.Checked Then
                frm.IsDouble = True
            Else
                frm.IsDouble = False
            End If
            frm.OrgGPrice = txtOriginalPriceGram.Text
            frm.OrgFPrice = txtOriginalFixedPrice.Text
            frm.IsFixed = chkByFixedPrice.Checked

            Dim weightY As Decimal = 0
            Dim weightP As Integer = 0
            Dim weightK As Integer = 0

            If CStr(_GoldTG) <> "" Or CStr(_WasteTG) <> "" Then
                If _GoldTG <> 0.0 Or _WasteTG <> 0.0 Then
                    Dim GoldWeight As New CommonInfo.GoldWeightInfo
                    Dim WasteWeight As New CommonInfo.GoldWeightInfo
                    Dim TGoldWeight As New CommonInfo.GoldWeightInfo

                    GoldWeight.WeightK = CDec(txtGoldK.Text)
                    GoldWeight.WeightP = CDec(txtGoldP.Text)
                    GoldWeight.WeightY = CDec(txtGoldY.Text)

                    WasteWeight.WeightK = CDec(txtWasteK.Text)
                    WasteWeight.WeightP = CDec(txtWasteP.Text)
                    WasteWeight.WeightY = CDec(txtWasteY.Text)

                    weightY = GoldWeight.WeightY + WasteWeight.WeightY
                    If weightY >= Global_PToY Then
                        weightP = 1
                        weightY = weightY - Global_PToY
                    End If

                    weightP += GoldWeight.WeightP + WasteWeight.WeightP
                    If weightP >= 16 Then
                        weightK = 1
                        weightP = weightP - 16
                    End If

                    weightK += GoldWeight.WeightK + WasteWeight.WeightK

                    TGoldWeight.WeightY = weightY
                    TGoldWeight.WeightP = weightP
                    TGoldWeight.WeightK = weightK
                    If Global_DecimalFormat = 1 Then
                        frm.TGoldWg = Format(TGoldWeight.WeightK, "0") + "-" + Format(TGoldWeight.WeightP, "0") + "-" + Format(TGoldWeight.WeightY, "0.0")
                    ElseIf Global_DecimalFormat = 2 Then
                        frm.TGoldWg = Format(TGoldWeight.WeightK, "0") + "-" + Format(TGoldWeight.WeightP, "0") + "-" + Format(TGoldWeight.WeightY, "0.00")
                    ElseIf Global_DecimalFormat = 3 Then
                        frm.TGoldWg = Format(TGoldWeight.WeightK, "0") + "-" + Format(TGoldWeight.WeightP, "0") + "-" + Format(TGoldWeight.WeightY, "0.000")
                    ElseIf Global_DecimalFormat = 4 Then
                        frm.TGoldWg = Format(TGoldWeight.WeightK, "0") + "-" + Format(TGoldWeight.WeightP, "0") + "-" + Format(TGoldWeight.WeightY, "0.0000")
                    Else
                        frm.TGoldWg = Format(TGoldWeight.WeightK, "0") + "-" + Format(TGoldWeight.WeightP, "0") + "-" + Format(TGoldWeight.WeightY, "0.0")
                    End If
                    ' frm.TGoldWg = Format(TGoldWeight.WeightK, "0") + "-" + Format(TGoldWeight.WeightP, "0") + "-" + Format(TGoldWeight.WeightY, "0.0")
                    frm.TGoldGram = CStr(CDec(txtGoldTG.Text) + CDec(txtWasteTG.Text))
                End If
            End If


            frm.ShowDialog()
        End If
    End Sub


#End Region

#Region "Grid"


    Private Sub grdGems_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGems.CellValidated

        If grdGems.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdGems.Columns(e.ColumnIndex).Name

                Case "UnitPrice", "Type", "Qty", "GemsK", "GemsP", "GemsY", "YOrCOrG", "GemsRemark"
                    If Not IsDBNull(grdGems.Rows(e.RowIndex).Cells("Type").Value) Then

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
                            If Not IsDBNull(grdGems.Rows(e.RowIndex).Cells("Qty").Value) Then

                                If Not IsDBNull(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                                    grdGems.Rows(e.RowIndex).Cells("Amount").Value = CLng(grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value) * CInt(grdGems.Rows(e.RowIndex).Cells("Qty").Value)
                                End If
                            Else
                                MsgBox("Pls Fill Quatity", MsgBoxStyle.Information, Me.Text)

                            End If
                        End If
                    End If
            End Select
        End If
        CalculategrAlldTotalAmount()
    End Sub
    Private Sub CalculategrAlldTotalAmount()
        Dim _OriginalAmount As Integer = 0
        If chkIsOriginalPriceGram.Checked Then
            For j As Integer = 0 To grdGems.RowCount - 1
                If IIf(IsDBNull(grdGems.Rows(j).Cells("Amount").Value), 0, grdGems.Rows(j).Cells("Amount").Value) <> 0.0 Then
                    If IsNumeric(grdGems.Rows(j).Cells("Amount").FormattedValue) Then
                        _OriginalAmount += CDec(grdGems.Rows(j).Cells("Amount").FormattedValue)
                    End If
                End If
            Next
            txtOriginalGemsPrice.Text = _OriginalAmount
        Else
            txtOriginalGemsPrice.Text = "0"
        End If
    End Sub

    Private Sub grdGems_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGems.CellValueChanged
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If grdGems.IsCurrentCellInEditMode = False Then Exit Sub
        If (e.RowIndex <> -1) Then
            If (grdGems.Columns(e.ColumnIndex).Name = "GemsK" Or grdGems.Columns(e.ColumnIndex).Name = "GemsP" Or grdGems.Columns(e.ColumnIndex).Name = "GemsY") Then 'F

                With grdGems
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("GemsK").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("GemsP").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("GemsY").Value) Then

                        If .Rows(e.RowIndex).Cells("GemsK").FormattedValue = "" Then
                            .Rows(e.RowIndex).Cells("GemsK").Value() = "0"
                        End If

                        If .Rows(e.RowIndex).Cells("GemsP").FormattedValue = "" Then
                            .Rows(e.RowIndex).Cells("GemsP").Value() = "0"
                        End If

                        If .Rows(e.RowIndex).Cells("GemsY").FormattedValue = "" Then
                            If numberformat = 1 Then
                                .Rows(e.RowIndex).Cells("GemsY").Value() = "0.0"
                            Else
                                .Rows(e.RowIndex).Cells("GemsY").Value() = "0.00"
                            End If
                        End If

                        GoldWeight.WeightK = CInt(Val(grdGems.Rows(e.RowIndex).Cells("GemsK").FormattedValue))

                        If CInt(Val(grdGems.Rows(e.RowIndex).Cells("GemsP").FormattedValue)) >= 16 Then
                            MsgBox("GemP should not be greater than 15", MsgBoxStyle.Information, AppName)
                            .Rows(e.RowIndex).Cells("GemsP").Value() = "0"
                        End If
                        GoldWeight.WeightP = CInt(Val(grdGems.Rows(e.RowIndex).Cells("GemsP").FormattedValue))

                        If CDec(grdGems.Rows(e.RowIndex).Cells("GemsY").FormattedValue) >= Global_PToY Then
                            MsgBox("GemY should not be greater than" & (Global_PToY - 0.1), MsgBoxStyle.Information, AppName)
                            If numberformat = 1 Then
                                .Rows(e.RowIndex).Cells("GemsY").Value() = "0.0"
                            Else
                                .Rows(e.RowIndex).Cells("GemsY").Value() = "0.00"
                            End If
                        End If

                        GoldWeight.WeightY = System.Decimal.Truncate(Val(grdGems.Rows(e.RowIndex).Cells("GemsY").FormattedValue))
                        GoldWeight.WeightC = CDec(Val(grdGems.Rows(e.RowIndex).Cells("GemsY").FormattedValue)) - GoldWeight.WeightY

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _GemsTK = GoldWeight.GoldTK
                        GoldWeight.Gram = _GemsTK * Global_KyatToGram
                        _GemsTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("GemsTG").Value() = _GemsTG
                        .Rows(e.RowIndex).Cells("GemsTK").Value() = _GemsTK
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
                    Exit Sub
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
                    If numberformat = 1 Then
                        grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                    Else
                        grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.00"
                    End If

                Else
                    If VarWeight.EndsWith("ct") Then
                        If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                            VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                            ' Notes: For Karat,multiply 1.1 
                            TC = CStr(VarWeightBCG)
                            If Global_IsCarat = 0 Or Global_IsCarat = 2 Then 'If Global_IsCarat = True Then
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
                        If numberformat = 1 Then
                            grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                        Else
                            grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.00"
                        End If
                    End If

                    equivalent = Global_GramToKarat '_ConverterCon.GetMeasurement("Gram", "Karat")
                    Dim gram As Decimal = TC / equivalent
                    equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
                    GoldWeight.GoldTK = gram / equivalent
                    _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                    grdGems.Rows(e.RowIndex).Cells("GemsK").Value = GoldWeight.WeightK
                    grdGems.Rows(e.RowIndex).Cells("GemsP").Value = GoldWeight.WeightP
                    If numberformat = 1 Then
                        grdGems.Rows(e.RowIndex).Cells("GemsY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
                    Else
                        grdGems.Rows(e.RowIndex).Cells("GemsY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
                    End If
                    grdGems.Rows(e.RowIndex).Cells("GemsTK").Value = gram / equivalent
                    grdGems.Rows(e.RowIndex).Cells("GemsTG").Value = gram
                    _Carat = Format(gram * Global_GramToKarat, "0.0")

                    'Get Unit Price
                    Dim objCurrent As New CommonInfo.IntDiamondPriceRateInfo
                    objCurrent = _CurrentController.GetIntDiamondData(_Carat)
                    With objCurrent
                        grdGems.Rows(e.RowIndex).Cells("UnitPrice").Value = .PurchaseRate
                    End With

                End If
                'For Carat 
                'ElseIf grdGems.Columns(e.ColumnIndex).Name = "Carat" Then
                '    Dim equivalent As Decimal
                '    Dim VarWeight As String
                '    Dim VarWeightY As Integer
                '    Dim VarWeightBCG As Decimal
                '    Dim VarWeightP As Decimal
                '    Dim TP As Decimal
                '    Dim TY As Decimal
                '    Dim TC As Decimal

                '    Dim IsValid As Boolean
                '    If IsDBNull(grdGems.Rows(e.RowIndex).Cells("Carat").Value) Then
                '        Exit Sub
                '    End If

                '    VarWeight = CStr(grdGems.Rows(e.RowIndex).Cells("Carat").Value)

                '    If Not VarWeight.EndsWith("ct") And Not VarWeight.ToString = "0" Then
                '        MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                '        grdGems.Rows(e.RowIndex).Cells("Carat").Value = "0"
                '        grdGems.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                '        grdGems.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                '        If numberformat = 1 Then
                '            grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                '        Else
                '            grdGems.Rows(e.RowIndex).Cells("GemsY").Value = "0.00"
                '        End If

                '    Else
                '        If VarWeight.EndsWith("ct") Then
                '            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                '                VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                '                ' Notes: For Karat,multiply 1.1 
                '                TC = CStr(VarWeightBCG)
                '                If Global_IsCarat = 0 Or Global_IsCarat = 2 Then 'If Global_IsCarat = True Then
                '                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG)
                '                Else
                '                    grdGems.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG * 1.1)
                '                End If
                '                IsValid = True
                '            Else
                '                IsValid = False
                '            End If
                '        End If

                '    End If
                '    equivalent = Global_GramToKarat '_ConverterCon.GetMeasurement("Gram", "Karat")
                '    Dim gram As Decimal = TC / equivalent
                '    equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
                '    GoldWeight.GoldTK = gram / equivalent
                '    _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                '    grdGems.Rows(e.RowIndex).Cells("GemsK").Value = GoldWeight.WeightK
                '    grdGems.Rows(e.RowIndex).Cells("GemsP").Value = GoldWeight.WeightP
                '    If numberformat = 1 Then
                '        grdGems.Rows(e.RowIndex).Cells("GemsY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
                '    Else
                '        grdGems.Rows(e.RowIndex).Cells("GemsY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
                '    End If
                '    grdGems.Rows(e.RowIndex).Cells("GemsTK").Value = gram / equivalent
                '    grdGems.Rows(e.RowIndex).Cells("GemsTG").Value = gram
                '    Dim TR As String
                '    Dim TB As String
                '    Dim TTP As String
                '    'TR = Truncate((gram * Global_GramToKarat) * Global_KaratToYati)
                '    'TB = Truncate((((gram * Global_GramToKarat) * Global_KaratToYati) * Global_YatiToB) - Truncate(((gram * Global_KaratToYati) * Global_KaratToYati) * Global_YatiToB))
                '    'TTP = Truncate(((((gram * Global_GramToKarat) * Global_KaratToYati) * Global_YatiToB) * Global_BToP) - Truncate((((gram * Global_GramToKarat) * Global_KaratToYati) * Global_YatiToB) * Global_BToP))

                '    TR = Truncate(VarWeightBCG * Global_KaratToYati)
                '    TB = Truncate((((VarWeightBCG * Global_KaratToYati) - Truncate(VarWeightBCG * Global_KaratToYati)) * Global_YatiToB))
                '    TTP = Truncate((((((VarWeightBCG * Global_KaratToYati) - Truncate(VarWeightBCG * Global_KaratToYati)) * Global_YatiToB)) - Truncate((((VarWeightBCG * Global_KaratToYati) - Truncate(VarWeightBCG * Global_KaratToYati)) * Global_YatiToB))) * Global_BToP)

                '    grdGems.Rows(e.RowIndex).Cells("YOrCOrG").Value = IIf(TR <= 0, "", TR & "R") & IIf(TB <= 0, "", TB & "B") & IIf(TTP <= 0, "", TTP & "P")


            End If



            CalculateGrdGems()
            CalculateGoldWeight()
            'CalculateTotalWeight()
        End If
    End Sub

    Private Sub grdGems_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdGems.RowsRemoved
        If grdGems.RowCount > 0 Then
            CalculateGrdGems()
            CalculateTotalWeight()
            CalculateGoldWeight()
            CalculategrAlldTotalAmount()
        End If
    End Sub

#End Region

#Region "KeyPress"

    Private Sub txtWasteTG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWasteTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtWasteK_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWasteK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtWasteP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWasteP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtWasteY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWasteY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtItemTG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemTG.KeyPress
        _WeightType = "Gram"
        MyBase.ValidateNumeric(sender, e, True)
        If Asc(e.KeyChar) = Keys.Enter Then
            btnNew.Focus()
        End If
    End Sub
    Private Sub txtItemK_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub
    Private Sub txtItemP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub
    Private Sub txtItemY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
        _WeightType = "Kyat"
    End Sub

    Private Sub cboGoldQuality_Click(sender As Object, e As EventArgs) Handles cboGoldQuality.Click
        If cboGoldQuality.SelectedIndex = -1 Then
            GetLocationAndGoldQCombo()
        End If
    End Sub

    Private Sub cboGoldQuality_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQuality.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQuality, e)
    End Sub

    Private Sub cboGoldQuality_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQuality.Leave
        AutoCompleteCombo_Leave(cboGoldQuality, "")
    End Sub

    Private Sub cboItemCategory_Click(sender As Object, e As EventArgs) Handles cboItemCategory.Click
        If cboItemCategory.SelectedIndex = -1 Then
            GetItemCategory()
        End If

    End Sub

    Private Sub cboDCategory_Click(sender As Object, e As EventArgs) Handles cboDCategory.Click
        If cboDCategory.SelectedIndex = -1 Then
            GetDItemCategory()
        End If

    End Sub

    'Private Sub cboLocation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboLocation.KeyUp
    '    AutoCompleteCombo_KeyUp(cboLocation, e)
    'End Sub
    'Private Sub cboLocation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.Leave
    '    AutoCompleteCombo_Leave(cboLocation, "")
    'End Sub
    Private Sub cboItemCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCategory, e)
    End Sub
    Private Sub cboItemCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCategory.Leave
        AutoCompleteCombo_Leave(cboItemCategory, "")
    End Sub
    Private Sub cboDCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboDCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboDCategory, e)
    End Sub
    Private Sub cboDCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDCategory.Leave
        AutoCompleteCombo_Leave(cboDCategory, "")
    End Sub
    Private Sub cboItemName_Click(sender As Object, e As EventArgs) Handles cboItemName.Click
        If cboItemName.SelectedIndex = -1 Then
            cboItemName.DisplayMember = "ItemName_"
            cboItemName.ValueMember = "ItemNameID"
            cboItemName.DataSource = _ItemNameCon.GetItemNameListByItemCategory(itemid).DefaultView

        End If
        ' RefreshItemNameCbo(itemid)

    End Sub
    Private Sub cboItemName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        AutoCompleteCombo_KeyUp(cboItemName, e)
    End Sub
    Private Sub cboItemName_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        AutoCompleteCombo_Leave(cboItemName, "")
    End Sub
    Private Sub txtVItemTG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVItemTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtVItemK_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVItemK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub
    Private Sub txtVItemP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVItemP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
        _WeightType = "Kyat"
    End Sub
    Private Sub txtVItemY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVItemY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
        _WeightType = "Kyat"
    End Sub

    Private Sub txtQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTY.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub cboVGoldQuality_Click(sender As Object, e As EventArgs) Handles cboVGoldQuality.Click
        GetLocationAndGoldQComboByVolume()
    End Sub

    Private Sub cboVGoldQuality_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboVGoldQuality.KeyUp
        AutoCompleteCombo_KeyUp(cboVGoldQuality, e)
    End Sub

    Private Sub cboVGoldQuality_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboVGoldQuality.Leave
        AutoCompleteCombo_Leave(cboVGoldQuality, "")
    End Sub

    Private Sub cboVItemCategory_Click(sender As Object, e As EventArgs) Handles cboVItemCategory.Click
        GetItemCategoryByVolume()
    End Sub
    Private Sub cboVItemCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboVItemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboVItemCategory, e)
    End Sub

    Private Sub cboVItemName_Click(sender As Object, e As EventArgs) Handles cboVItemName.Click
        cboVItemName.DisplayMember = "ItemName_"
        cboVItemName.ValueMember = "ItemNameID"
        cboVItemName.DataSource = _ItemNameCon.GetItemNameListByItemCategory(vitemid).DefaultView
        'cboVItemName.SelectedIndex = 0
    End Sub

    'Private Sub cboVItemCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboVItemCategory.Leave
    '    cboVItemCategory.SelectedIndex = -1
    '    AutoCompleteCombo_Leave(cboVItemCategory, "")

    'End Sub
    Private Sub cboVItemName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboVItemName.KeyUp
        AutoCompleteCombo_KeyUp(cboVItemName, e)
    End Sub
    Private Sub cboVItemName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboVItemName.Leave
        AutoCompleteCombo_Leave(cboVItemName, "")
    End Sub

    Private Sub cboStaff_Click(sender As Object, e As EventArgs) Handles cboStaff.Click
        GetStaffCombo()
    End Sub

    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub

    Private Sub txtPWasteTG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPWasteTG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtPWasteK_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPWasteK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtPWasteP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPWasteP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtPWasteY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPWasteY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
#End Region

#Region "Text Changed"

    Private Sub txtWasteTG_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWasteTG.TextChanged
        If txtWasteTG.Text = "" Then txtWasteTG.Text = "0.0"
        If Val(txtWasteTG.Text.Trim) >= 0.0 Then
            If _IsGram = True Then
                CalculateWasteWeightForGram()
            End If
            CalculateTotalWeight()
            'CalculateGrdGems()
            'CalculateGoldWeight()
            CalculateOriginalGoldPrice()
            CalculateOriginalPrice()
        End If
    End Sub
    Private Sub txtSellingRate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSellingRate.TextChanged
        CalculateSellingAmt()
    End Sub
    Private Sub CalculateSellingAmt()
        If txtSaleFixPrice.Text = "" Then txtSaleFixPrice.Text = 0
        If txtWSFixPrice.Text = "" Then txtWSFixPrice.Text = 0
        If txtSellingRate.Text = "" Then txtSellingRate.Text = 0
        If CLng(txtSellingRate.Text) > 0 Then
            If Global_GIsFixPrice Then
                chkByFixedPrice.Checked = Global_GIsFixPrice
            End If

            If Global_GIsFixPrice Then
                If CLng(txtSellingRate.Text) > 0 Then
                    txtSaleFixPrice.Text = CLng(CLng(txtTotalCost.Text) + CLng(txtTotalCost.Text) * (CLng(txtSellingRate.Text) / 100))
                    txtWSFixPrice.Text = CLng(CLng(txtTotalCost.Text) + CLng(txtTotalCost.Text) * (CLng(txtSellingRate.Text) / 100))
                Else
                    txtSaleFixPrice.Text = txtTotalCost.Text
                    txtWSFixPrice.Text = txtTotalCost.Text
                End If
            End If
        End If
    End Sub
    Private Sub txtWasteK_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWasteK.TextChanged
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"

        If Val(txtWasteK.Text.Trim) >= 0 Then
            If _IsGram = False Then
                CalculateWasteWeightForKPY()
            End If
            CalculateTotalWeight()
            'CalculateGrdGems()
            'CalculateGoldWeight()
        End If
    End Sub

    Private Sub txtWasteP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWasteP.TextChanged
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
            'CalculateGrdGems()
            'CalculateGoldWeight()
        End If
    End Sub

    Private Sub txtWasteY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWasteY.TextChanged
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
            'CalculateGrdGems()
            'CalculateGoldWeight()
        End If
    End Sub



    Private Sub txtItemTG_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemTG.TextChanged
        If txtItemTG.Text = "" Then txtItemTG.Text = "0.000"
        If Val(txtItemTG.Text.Trim) >= 0.0 Then
            If _IsGram = True Then
                CalculateItemWeightForGram()
            End If

            CalculateTotalWeight()
            CalculateGoldWeight()
        End If
    End Sub

    Private Sub txtItemK_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtItemK.TextChanged
        If txtItemK.Text = "" Then txtItemK.Text = "0"

        If Val(txtItemK.Text.Trim) >= 0 Then
            If _VIsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If

            ShowWasteData()
            CalculateTotalWeight()
            CalculateGoldWeight()
        End If
    End Sub

    Private Sub txtItemP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtItemP.TextChanged
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
            ShowWasteData()
            CalculateTotalWeight()
            CalculateGoldWeight()
        End If
    End Sub
    Private Sub txtItemY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtItemY.TextChanged

        If txtItemY.Text = "" Then txtItemY.Text = "0.00"

        If Val(txtItemY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtItemY.Text = "0.00"
            txtItemY.SelectAll()
        End If

        If Val(txtItemY.Text.Trim) >= 0 Then
            If _VIsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
            ShowWasteData()
            CalculateTotalWeight()
            CalculateGoldWeight()
        End If
    End Sub

#End Region

#Region "Selected changed"

    Private Sub cboItemCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboItemCategory.SelectedIndexChanged

    End Sub


    Private Sub cboItemCategory_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCategory.SelectedValueChanged
        itemid = cboItemCategory.SelectedValue
        'If _IsUpdate = False Then
        RefreshItemNameCbo(itemid)
        'End If

        If itemid IsNot Nothing Then
            Dim GoldQualityID As String

            If cboItemCategory.Text.Contains("White Gold") Then
                GoldQualityID = _GoldQCon.GetAllGoldQualityByItemCategory("18K").GoldQualityID


                If GoldQualityID IsNot Nothing Then
                    cboGoldQuality.SelectedValue = GoldQualityID
                End If
            ElseIf cboItemCategory.Text.Contains("Gems") Or cboItemCategory.Text.Contains("Diamond") Then
                GoldQualityID = _GoldQCon.GetAllGoldQualityByItemCategory("21K").GoldQualityID
                If GoldQualityID IsNot Nothing Then
                    cboGoldQuality.SelectedValue = GoldQualityID
                End If
            End If
        End If
        SaleItemCodeGenerateFormat()
        ShowWasteData()
    End Sub

    Private Sub dtpGivenDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpGivenDate.ValueChanged
        If _IsUpdate = False Then
            SaleItemCodeGenerateFormat()
        Else
            If dtpGivenDate.Value.Date <> _OldDate Then
                SaleItemCodeGenerateFormat()
            End If
        End If
    End Sub

    Private Sub cboGoldQuality_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQuality.SelectedValueChanged
        GoldQualityForTextChange()
        ShowWasteData()
    End Sub
#End Region

#Region "Click"
    Private Sub btnForSaleHeader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        AllClear()
        Dim DataItem As DataRow
        Dim dtForSale As New DataTable
        dtForSale = _SalesItemCon.GetAllForSaleHeader(" Where ((IsExit = '0') Or (IsExit='1' And IsClosed='1'))")
        DataItem = DirectCast(SearchData.FindFast(dtForSale, "ForSale Header List"), DataRow)
        If DataItem IsNot Nothing Then
            _IsUpdate = True
            _ForSaleID = DataItem.Item("@ForSaleID").ToString()
            ShowData_ForSale(_ForSaleID)
            If chkIsVolume.Checked = False And chkIsLooseDiamond.Checked = False Then
                _dtForSaleGems = _SalesItemCon.GetForSaleGems(_ForSaleID)
                grdGems.DataSource = _dtForSaleGems
                CalculateGrdGems()
            End If
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()

            If Global_CurrentUser = "Administrator" Then
                dtpGivenDate.Enabled = True
                btnSave.Enabled = True
                btnDelete.Enabled = True
                txtItemCode.ReadOnly = False
                txtVItemCode.ReadOnly = False
            Else
                If Global_IsAllowStock Then
                    dtpGivenDate.Enabled = True
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    txtItemCode.ReadOnly = False
                    txtVItemCode.ReadOnly = False
                Else
                    dtpGivenDate.Enabled = False
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    txtItemCode.ReadOnly = True
                    txtVItemCode.ReadOnly = True
                End If
            End If


        End If
    End Sub

    Private Sub LnkTotalNoWaste_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LnkTotalNoWaste.LinkClicked
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
                txtItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
            Else
                txtItemY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
            End If
            ' txtItemTG.Text = Format(GoldWeight.Gram, "0.000") 'newTaisin
            txtWasteK.Focus()
        ElseIf _IsGram = False And _WeightType = "Gram" Then
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            If txtVItemTG.Text = "" Then txtVItemTG.Text = "0.0"
            If Val(txtVItemTG.Text.Trim) >= 0.0 Then
                'If _VIsGram = True Then
                CalculateItemWeightForGram()
                'End If
            End If
        Else
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            txtItemTG.Focus()
        End If

    End Sub

    Private Sub chkByFixedPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkByFixedPrice.Click
        If chkByFixedPrice.Checked = False Then
            txtSaleFixPrice.Enabled = False
            txtSaleFixPrice.Text = 0
            txtWSFixPrice.Enabled = False
            txtWSFixPrice.Text = 0
        Else
            txtSaleFixPrice.Enabled = True
            txtWSFixPrice.Enabled = True
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        If _ForSaleID <> "0" Then
            If txtItemCode.Text <> "" Then
                ShowBarcodeData(txtItemCode.Text.Trim, txtTotalTG.Text.Trim, txtLength.Text.Trim, _Prefix)
            End If
            If txtVItemCode.Text <> "" Then
                If chkIsVolume.Checked Then
                    ShowVolumeBarcodeData(txtVItemCode.Text.Trim)
                End If
            End If
            If txtDBarcode.Text <> "" Then
                If chkIsLooseDiamond.Checked Then
                    ShowDiamondBarcodeData(txtDBarcode.Text.Trim)
                End If
            End If
        Else
            MsgBox("Please Select ItemCode!", MsgBoxStyle.Information, AppName)
        End If

    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        If btnAdd.Text = "Add" Then
            If Global_PhotoPath <> "" Then

                OpenFileDia.Filter = "Image (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png;"
                '"Image (*.jpg;)|*.jpg;"
                OpenFileDia.FileName = Global_PhotoPath + "\"
                OpenFileDia.InitialDirectory = OpenFileDia.FileName
                OpenFileDia.ShowDialog()
                If OpenFileDia.FileName <> "" Then
                    If OpenFileDia.InitialDirectory = OpenFileDia.FileName Then
                        'lblItemImage.Image = Nothing
                        'btnAdd.Text = "Add"
                        'lblPhoto.Visible = True
                        'PName = ""
                        If Global_LogoPhoto <> "" Then
                            lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                            PName = Global_LogoPhoto
                            btnAdd.Text = "Add"
                            lblPhoto.Visible = False
                        Else

                            lblItemImage.Image = Nothing
                            lblPhoto.Visible = True
                            PName = ""
                            btnAdd.Text = "Add"
                        End If
                        Exit Sub

                    End If
                    lblItemImage.Image = System.Drawing.Image.FromFile(OpenFileDia.FileName)
                    PName = OpenFileDia.FileName.Substring(Global_PhotoPath.Length + 1)
                    btnAdd.Text = "Remove"
                End If
                lblPhoto.Visible = False
            End If
        Else
            If Global_LogoPhoto <> "" Then
                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                PName = Global_LogoPhoto
                btnAdd.Text = "Add"
                lblPhoto.Visible = False
            Else

                lblItemImage.Image = Nothing
                lblPhoto.Visible = True
                PName = ""
                'DefaultPhoto = "ri.jpg"
                btnAdd.Text = "Add"
            End If
        End If
    End Sub
#End Region

    Private Sub chkByFixedPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkByFixedPrice.CheckedChanged
        If chkByFixedPrice.Checked = True Then
            GBFix.Enabled = True
            txtSaleFixPrice.BackColor = Color.White
            txtSaleFixPrice.Enabled = True
            txtWSFixPrice.BackColor = Color.White
            txtWSFixPrice.Enabled = True
        Else
            GBFix.Enabled = False
            txtSaleFixPrice.Enabled = False
            txtSaleFixPrice.BackColor = Color.Gainsboro
            txtSaleFixPrice.Text = "0"
            txtWSFixPrice.Enabled = False
            txtWSFixPrice.BackColor = Color.Gainsboro
            txtWSFixPrice.Text = "0"
        End If
    End Sub
    Private Sub chkDFixPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkDFixPrice.CheckedChanged
        If chkDFixPrice.Checked = True Then
            txtDFixPrice.BackColor = Color.White
            txtDFixPrice.Enabled = True
        Else
            txtDFixPrice.Enabled = False
            txtDFixPrice.BackColor = Color.Gainsboro
            txtDFixPrice.Text = "0"
        End If
    End Sub
    Private Sub chkIsOrder_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsOrder.CheckedChanged

        If chkIsOrder.Checked Then
            If _IsUpdate = True Then
                chkIsClosed.Enabled = False
            End If
            btnOrderVoucherSearch.Visible = True
            btnOrderVoucherSearch.Enabled = True
            lblOrderInvoiceiD.Visible = True
            lblOrderDate.Visible = True
            chkIsVolume.Checked = False
            chkIsVolume.Visible = False
            chkIsLooseDiamond.Checked = False
            chkIsLooseDiamond.Visible = False
            lblCustomer.Visible = True
        Else
            If _IsUpdate = True Then
                chkIsClosed.Enabled = True
            End If
            _OrderReceiveDetailID = ""
            lblOrderInvoiceiD.Text = ""
            lblOrderDate.Text = ""
            lblCustomer.Text = ""

            btnOrderVoucherSearch.Visible = False
            lblOrderInvoiceiD.Visible = False
            lblOrderDate.Visible = False
            lblCustomer.Visible = False
            cboItemCategory.Enabled = True
            cboItemName.Enabled = True
            cboGoldQuality.Enabled = True
            If txtItemCode.Text = "" Then
                cboGoldQuality.SelectedIndex = -1
            End If
            chkIsVolume.Visible = True
            chkIsLooseDiamond.Visible = True
        End If
    End Sub

    Private Sub btnOrderVoucherSearch_Click(sender As Object, e As EventArgs) Handles btnOrderVoucherSearch.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objCust As New CommonInfo.CustomerInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        dt = _OrderInvoiceController.GetAllOrderReceive()
        DataItem = DirectCast(SearchData.FindFast(dt, "Order Detail List"), DataRow)
        If DataItem IsNot Nothing Then
            lblOrderInvoiceiD.Visible = True
            lblOrderDate.Visible = True
            lblCustomer.Visible = True
            _OrderReceiveDetailID = DataItem("@OrderReceiveDetailID")
            lblOrderInvoiceiD.Text = DataItem("VoucherNo")
            lblOrderDate.Text = DataItem("OrderDate").ToString()
            lblCustomer.Text = DataItem("Customer_").ToString
            cboItemCategory.Enabled = False

            cboItemName.Enabled = False
            cboGoldQuality.Enabled = False
            chkIsDiamond.Checked = DataItem("$IsDiamond")
            chkIsDiamond.Enabled = False
            If _IsUpdate = False Then
                cboItemCategory.SelectedValue = DataItem("@ItemCategoryID").ToString()
                cboGoldQuality.SelectedValue = DataItem("@GoldQualityID").ToString()
                cboItemName.SelectedValue = DataItem("@ItemNameID").ToString()
                cboGoldSmith.SelectedValue = DataItem("GoldSmithID").ToString()
                txtPlatingCharges.Text = DataItem("@PlatingFee").ToString()
                txtWhiteCharges.Text = DataItem("@WhiteCharges").ToString()
                txtDesignCharges.Text = DataItem("@DesignCharges").ToString()
                txtMountingCharges.Text = DataItem("@MountingFee").ToString()

                cboItemCategory.Enabled = False
                cboItemName.Enabled = False
                cboGoldQuality.Enabled = False
                txtLength.Text = DataItem("Length_").ToString()
                txtWidth.Text = DataItem("Width_").ToString()
                _ItemTG = DataItem("@ItemTG")
                _ItemTK = DataItem("@ItemTK")
                txtItemTG.Text = Format(DataItem("@ItemTG"), "0.000")
                txtItemTK.Text = Format(DataItem("@ItemTK"), "0.000")
                GoldWeight.GoldTK = _ItemTK
                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtItemK.Text = CStr(GoldWeight.WeightK)
                txtItemP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

                Else
                    txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtItemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                _WasteTG = DataItem("@WasteTG")
                _WasteTK = DataItem("@WasteTK")
                GoldWeight.GoldTK = _WasteTK
                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtWasteK.Text = CStr(GoldWeight.WeightK)
                txtWasteP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                txtWasteTG.Text = Format(_WasteTG, "0.000")
                txtWasteTK.Text = Format(_WasteTK, "0.000")
                _GoldTG = DataItem("@GoldTG")
                _GoldTK = DataItem("@GoldTK")
                txtGoldTG.Text = Format(_GoldTG, "0.000")
                txtGoldTK.Text = Format(_GoldTK, "0.000")
                GoldWeight.GoldTK = _GoldTK
                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtGoldK.Text = CStr(GoldWeight.WeightK)
                txtGoldP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If
                'txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                _PWasteTG = DataItem("@WasteTG")
                _PWasteTK = DataItem("@WasteTK")
                txtPWasteTG.Text = Format(_PWasteTG, "0.000")
                GoldWeight.GoldTK = _PWasteTK
                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtPWasteK.Text = CStr(GoldWeight.WeightK)
                txtPWasteP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))

                End If
                'txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                'Gem
                _TotalGemsTG = DataItem("@TotalGemTG")
                _TotalGemsTK = DataItem("@TotalGemTK")
                txtGemTG.Text = Format(_TotalGemsTG, "0.000")
                txtGemsTK.Text = Format(_TotalGemsTK, "0.000")
                GoldWeight.GoldTK = _TotalGemsTK
                GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                txtGemK.Text = CStr(GoldWeight.WeightK)
                txtGemP.Text = CStr(GoldWeight.WeightP)
                If numberformat = 1 Then
                    txtGemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
                Else
                    txtGemY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
                End If

                CalculateTotalWeight()
                _dtForSaleGems = _OrderInvoiceController.GetOrderGemsByReceive(_OrderReceiveDetailID)
                grdGems.DataSource = _dtForSaleGems
                CalculateGrdGems()
            Else
                If (cboItemCategory.SelectedValue IsNot Nothing And cboItemCategory.SelectedValue <> DataItem("@ItemCategoryID").ToString()) Then
                    If MsgBox("OrderItem's Category and ItemCode's Category is Not Same.", MsgBoxStyle.OkOnly) = MsgBoxResult.Ok Then

                    End If
                End If
            End If

        End If
    End Sub


    'Private Sub chkIsVolume_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsVolume.CheckedChanged
    '    If (chkIsVolume.Checked Or ChkIsSolidVolume.Checked) Then
    '        tabStock.TabPages.Remove(TabStockItem)
    '        tabStock.TabPages.Add(TabVolumeStock)
    '        chkIsClosed.Checked = False
    '        chkIsClosed.Enabled = False
    '        GetItemCategoryByVolume()
    '        cboVGoldQuality.SelectedIndex = -1
    '        chkIsOrder.Checked = False
    '        chkIsOrder.Visible = False
    '        cboStaff.TabIndex = 9
    '        chkIsDiamond.Enabled = False
    '    Else
    '        tabStock.TabPages.Remove(TabVolumeStock)
    '        tabStock.TabPages.Add(TabStockItem)
    '        chkIsClosed.Enabled = False
    '        GetItemCategory()
    '        cboGoldQuality.SelectedIndex = -1
    '        chkIsOrder.Visible = True
    '        chkIsDiamond.Enabled = True
    '    End If
    '    AllClear()
    'End Sub
    Private Sub chkIsSolidVolume_CheckedChanged(sender As Object, e As EventArgs) Handles ChkIsSolidVolume.CheckedChanged
        If ChkIsSolidVolume.Checked Then
            txtQTY.Text = "0"
            txtQTY.Enabled = False
            chkVByFixPrice.Enabled = False
        Else
            txtQTY.Enabled = True
            chkVByFixPrice.Enabled = True
        End If
    End Sub

    Private Sub cboVItemCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboVItemCategory.SelectedValueChanged
        vitemid = cboVItemCategory.SelectedValue
        RefreshItemNameCboByVolume(vitemid)
        SaleItemCodeGenerateFormat()
    End Sub
    Private Sub RefreshItemNameCboByVolume(ByVal ItemID As String)
        Dim dt As New DataTable
        dt = _ItemNameCon.GetItemNameListByItemCategory(ItemID)
        If dt.Rows.Count > 0 Then
            cboVItemName.DataSource = dt.DefaultView
            cboVItemName.DisplayMember = "ItemName_"
            cboVItemName.ValueMember = "ItemNameID"
            'cboVItemName.SelectedIndex = 0
        Else
            dt.Rows.Clear()
            cboVItemName.DataSource = dt.DefaultView
            cboVItemName.DisplayMember = "ItemName_"
            cboVItemName.ValueMember = "ItemNameID"
            cboVItemName.Text = ""
            cboVItemName.SelectedIndex = -1
        End If
    End Sub
    Private Sub cboVGoldQuality_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboVGoldQuality.SelectedValueChanged
        GoldQualityForTextChange()
    End Sub

    Private Sub LnkTotalWeight_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkTotalWeight.LinkClicked
        Dim frm As New frm_ToWeight
        Dim GoldWeight As New GoldWeightInfo
        frm.ShowDialog()
        _WeightType = frm._OptType
        GoldWeight = frm._GoldWeightInfo
        '_VItemTG = GoldWeight.Gram
        '_VItemTK = GoldWeight.GoldTK

        If _VIsGram Then
            txtVItemTG.Text = Format(GoldWeight.Gram, "0.000")
        ElseIf _VIsGram = False And _WeightType = "Kyat" Then
            txtVItemTG.Text = Format(GoldWeight.Gram, "0.000")
            txtVItemK.Text = CStr(GoldWeight.WeightK)
            txtVItemP.Text = CStr(GoldWeight.WeightP)
            txtItemTG.Text = Format(GoldWeight.Gram, "0.000")
            If numberformat = 1 Then
                txtVItemY.Text = Math.Round(GoldWeight.WeightY + GoldWeight.WeightC, 1)
            Else
                txtVItemY.Text = Math.Round(GoldWeight.WeightY + GoldWeight.WeightC, 2)
            End If
            'txtVItemY.Text = Math.Round(GoldWeight.WeightY + GoldWeight.WeightC, 1)
        ElseIf _VIsGram = False And _WeightType = "Gram" Then
            txtVItemTG.Text = Format(GoldWeight.Gram, "0.000")
            If txtVItemTG.Text = "" Then txtVItemTG.Text = "0.0"
            If Val(txtVItemTG.Text.Trim) >= 0.0 Then
                'If _VIsGram = True Then
                CalculateItemWeightForGram()
                'End If
            End If
        End If
    End Sub

    Private Sub chkVByFixPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkVByFixPrice.CheckedChanged
        If chkVByFixPrice.Checked = True Then
            txtVFixedPrice.BackColor = Color.White
            txtVFixedPrice.Enabled = True
            chkVIsOriginalPriceGram.Enabled = False
            radVGramPrice.Enabled = False
            radVKPrice.Enabled = False
        Else
            txtVFixedPrice.Enabled = False
            txtVFixedPrice.BackColor = Color.Gainsboro
            txtVFixedPrice.Text = "0"
            chkVIsOriginalPriceGram.Enabled = True
            radVGramPrice.Enabled = True
            radVKPrice.Enabled = True
        End If
    End Sub

    Private Sub chkVIsOriginalFixedPrice_Click(sender As Object, e As EventArgs) Handles chkVIsOriginalFixedPrice.Click
        If chkVIsOriginalFixedPrice.Checked = False Then
            txtVOriginalFixPrice.Enabled = False
            txtVOriginalFixPrice.BackColor = Color.Gainsboro
            txtVOriginalFixPrice.Text = "0"
            CalculategrAlldTotalAmount()
        Else
            txtVOriginalFixPrice.Enabled = True
            txtVOriginalFixPrice.BackColor = Color.White
            chkVIsOriginalPriceGram.Checked = False
            txtVOriginalPriceGram.Enabled = False
            txtVOriginalPriceKyat.Enabled = False
            txtVOriginalGemPrice.Enabled = False
            txtVOriginalOtherPrice.Enabled = False

            txtVOriginalPriceGram.BackColor = Color.Gainsboro
            txtVOriginalPriceKyat.BackColor = Color.Gainsboro
            txtVOriginalGemPrice.BackColor = Color.Gainsboro
            txtVOriginalOtherPrice.BackColor = Color.Gainsboro

            txtVOriginalPriceGram.Text = "0"
            txtVOriginalPriceKyat.Text = "0"
            txtVOriginalGemPrice.Text = "0"
            txtVOriginalOtherPrice.Text = "0"
            radVGramPrice.Checked = True
            radVGramPrice.Enabled = False
            radVKPrice.Enabled = False
        End If
    End Sub

    Private Sub chkVIsOriginalPriceGram_Click(sender As Object, e As EventArgs) Handles chkVIsOriginalPriceGram.Click
        If chkVIsOriginalPriceGram.Checked = False Then
            txtVOriginalPriceGram.Enabled = False
            txtVOriginalPriceKyat.Enabled = False
            txtVOriginalGemPrice.Enabled = False
            txtVOriginalOtherPrice.Enabled = False

            txtVOriginalPriceGram.BackColor = Color.Gainsboro
            txtVOriginalPriceKyat.BackColor = Color.Gainsboro
            txtVOriginalGemPrice.BackColor = Color.Gainsboro
            txtVOriginalOtherPrice.BackColor = Color.Gainsboro

            txtVOriginalPriceGram.Text = "0"
            txtVOriginalPriceKyat.Text = "0"
            txtVOriginalGemPrice.Text = "0"
            txtVOriginalOtherPrice.Text = "0"
            radVGramPrice.Checked = True
            radVGramPrice.Enabled = True
            radVKPrice.Enabled = True

        Else
            If cboVGoldQuality.Text <> "" Then
                Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
                GoldQualityInfo = _GoldQCon.GetGoldQuality(cboVGoldQuality.SelectedValue)

                _VIsGram = GoldQualityInfo.IsGramRate

                If _VIsGram = True Then
                    radVGramPrice.Enabled = True
                    radVGramPrice.Checked = True
                    txtVOriginalPriceKyat.Enabled = False
                    txtVOriginalPriceKyat.Text = "0"
                    txtVOriginalPriceGram.Enabled = True
                    radVKPrice.Enabled = False
                    txtVOriginalPriceKyat.BackColor = Color.Gainsboro
                    txtVOriginalPriceGram.BackColor = Color.White
                Else
                    radVKPrice.Enabled = True
                    radVKPrice.Checked = True
                    radVGramPrice.Enabled = False
                    txtVOriginalPriceGram.Enabled = False
                    txtVOriginalPriceGram.Text = "0"
                    txtVOriginalPriceKyat.Enabled = True
                    txtVOriginalPriceKyat.Enabled = True
                    txtVOriginalPriceGram.Enabled = False
                    txtVOriginalPriceKyat.BackColor = Color.White
                    txtVOriginalPriceGram.BackColor = Color.Gainsboro

                End If
            End If

            txtVOriginalGemPrice.Enabled = True
            txtVOriginalOtherPrice.Enabled = True
            txtVOriginalGemPrice.BackColor = Color.White
            txtVOriginalOtherPrice.BackColor = Color.White
            chkVIsOriginalFixedPrice.Checked = False
            txtVOriginalFixPrice.Enabled = False
            txtVOriginalFixPrice.BackColor = Color.Gainsboro
            txtVOriginalFixPrice.Text = "0"

            txtVOriginalPriceGram.Text = "0"
            txtVOriginalPriceKyat.Text = "0"
            txtVOriginalGemPrice.Text = "0"
            txtVOriginalOtherPrice.Text = "0"
            CalculategrAlldTotalAmount()
        End If

    End Sub

    Private Sub btnVAdd_Click(sender As Object, e As EventArgs) Handles btnVAdd.Click

        If btnVAdd.Text = "Add" Then
            If Global_PhotoPath <> "" Then

                OpenFileDia.Filter = "Image (*.jpg;)|*.jpg;"
                OpenFileDia.FileName = Global_PhotoPath + "\"
                OpenFileDia.InitialDirectory = OpenFileDia.FileName
                OpenFileDia.ShowDialog()
                If OpenFileDia.FileName <> "" Then
                    If OpenFileDia.InitialDirectory = OpenFileDia.FileName Then
                        If Global_LogoPhoto <> "" Then
                            lblVItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                            VPName = Global_LogoPhoto
                            btnVAdd.Text = "Add"
                            lblVPhoto.Visible = False
                        Else
                            lblVItemImage.Image = Nothing
                            btnVAdd.Text = "Add"
                            lblVPhoto.Visible = True
                            VPName = ""
                        End If
                        Exit Sub
                    End If
                    lblVItemImage.Image = System.Drawing.Image.FromFile(OpenFileDia.FileName)
                    VPName = OpenFileDia.FileName.Substring(Global_PhotoPath.Length + 1)
                    btnVAdd.Text = "Remove"

                End If
                lblVPhoto.Visible = False
            End If
        Else
            If Global_LogoPhoto <> "" Then
                lblVItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                VPName = Global_LogoPhoto
                btnVAdd.Text = "Add"
                lblVPhoto.Visible = False
            Else
                lblVItemImage.Image = Nothing
                btnVAdd.Text = "Add"
                lblVPhoto.Visible = True
                VPName = ""
            End If
        End If
    End Sub

    Private Sub txtVItemK_TextChanged(sender As Object, e As EventArgs) Handles txtVItemK.TextChanged
        If txtVItemK.Text = "" Then txtVItemK.Text = "0"

        If Val(txtVItemK.Text.Trim) >= 0 Then
            If _IsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
        End If
    End Sub

    Private Sub txtVItemP_TextChanged(sender As Object, e As EventArgs) Handles txtVItemP.TextChanged
        If txtVItemP.Text = "" Then txtVItemP.Text = "0"

        If Val(txtVItemP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtVItemP.Text = 0
            txtVItemP.SelectAll()
        End If

        If Val(txtVItemP.Text.Trim) >= 0 Then
            If _VIsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
        End If
    End Sub

    Private Sub txtVItemY_TextChanged(sender As Object, e As EventArgs) Handles txtVItemY.TextChanged
        If txtVItemY.Text = "" Then txtVItemY.Text = "0.00"

        If Val(txtVItemY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtVItemY.Text = "0.0"
            txtVItemY.SelectAll()
        End If

        If Val(txtVItemY.Text.Trim) >= 0 Then
            If _VIsGram = False And (_WeightType = "" Or _WeightType = "Kyat") Then
                CalculateItemWeightForKPY()
            End If
        End If
    End Sub

    Private Sub txtVItemTG_TextChanged(sender As Object, e As EventArgs) Handles txtVItemTG.TextChanged
        If txtVItemTG.Text = "" Then txtVItemTG.Text = "0.0"

        If Val(txtVItemTG.Text.Trim) >= 0.0 Then
            If _VIsGram = True Then
                CalculateItemWeightForGram()
            End If
        End If
    End Sub

    'Private Sub txtSaleFixPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSaleFixPrice.KeyPress
    '    MyBase.ValidateNumeric(sender, e, False)
    'End Sub
    'Private Sub txtWSFixPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWSFixPrice.KeyPress
    '    MyBase.ValidateNumeric(sender, e, False)
    'End Sub
    Private Sub txtSellingPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSellingPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDesignCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesignCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtMountingCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMountingCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPlatingCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlatingCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtWhiteCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWhiteCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtOriginalFixedPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOriginalFixedPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtOriginalPriceTK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOriginalPriceTK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtOriginalPriceGram_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOriginalPriceGram.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtOriginalGemsPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOriginalGemsPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtOriginalOtherPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOriginalOtherPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtVFixedPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVFixedPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtVSellingPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVSellingPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtVOriginalFixPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVOriginalFixPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtVOriginalPriceGram_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVOriginalPriceGram.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtVOriginalPriceKyat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVOriginalPriceKyat.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtVOriginalGemPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVOriginalGemPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtVOriginalOtherPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVOriginalOtherPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPWasteTG_TextChanged(sender As Object, e As EventArgs) Handles txtPWasteTG.TextChanged
        If txtPWasteTG.Text = "" Then txtPWasteTG.Text = "0.0"

        If Val(txtPWasteTG.Text.Trim) >= 0.0 Then
            If _IsGram = True Then
                CalculatePurchaseWasteWeightForGram()
            End If
        End If
    End Sub

    Private Sub txtPWasteK_TextChanged(sender As Object, e As EventArgs) Handles txtPWasteK.TextChanged
        If txtPWasteK.Text = "" Then txtPWasteK.Text = "0"

        If Val(txtPWasteK.Text.Trim) >= 0 Then
            If _IsGram = False Then
                CalculatePurchaseWasteWeightForKPY()
            End If
        End If
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
                CalculatePurchaseWasteWeightForKPY()
            End If
        End If
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
                CalculatePurchaseWasteWeightForKPY()
            End If
        End If
    End Sub

    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If grdGems.CurrentCell Is grdGems.CurrentRow.Cells("GemsK") Or grdGems.CurrentCell Is grdGems.CurrentRow.Cells("GemsP") Or grdGems.CurrentCell Is grdGems.CurrentRow.Cells("GemsY") Then
        '    If IsDBNull(grdGems.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdGems.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdGems.CurrentRow.Cells("GemsY").FormattedValue) Then
        '        If grdGems.CurrentRow.Cells("YOrCOrG").FormattedValue <> "0" Then
        '            grdGems.CurrentRow.Cells("YOrCOrG").Value = "0"
        '        End If
        '    End If
        'End If

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

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("SaleItemSetupWithVolume")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub radGramPrice_CheckedChanged(sender As Object, e As EventArgs) Handles radGramPrice.CheckedChanged
        If radGramPrice.Checked Then
            txtOriginalPriceGram.Enabled = True
            txtOriginalPriceGram.BackColor = Color.White
            txtOriginalPriceGram.Text = "0"
            radKPrice.Checked = False
            txtOriginalPriceTK.Enabled = False
            txtOriginalPriceTK.Text = "0"
            txtOriginalPriceTK.BackColor = Color.Gainsboro
        End If
        CalculateOriginalGoldPrice()
        CalculateOriginalPrice()
    End Sub

    Private Sub radKPrice_CheckedChanged(sender As Object, e As EventArgs) Handles radKPrice.CheckedChanged
        If radKPrice.Checked Then
            txtOriginalPriceGram.Enabled = False
            txtOriginalPriceGram.BackColor = Color.Gainsboro
            radGramPrice.Checked = False
            txtOriginalPriceGram.Text = "0"
            txtOriginalPriceTK.Enabled = True
            txtOriginalPriceTK.Text = "0"
            txtOriginalPriceTK.BackColor = Color.White
        End If
        CalculateOriginalGoldPrice()
        CalculateOriginalPrice()
    End Sub

    Private Sub frm_SaleItemSetupWithVolume_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub chkIsDiamond_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsDiamond.CheckedChanged
        If chkIsDiamond.Checked Then
            'txtPriceCode.Enabled = True
            'lblPriceCode.Enabled = True
            'txtPriceCode.BackColor = Color.White
            chkIsVolume.Enabled = False
        Else

            'If _IsGram = True Then
            '    txtPriceCode.Enabled = True
            '    lblPriceCode.Enabled = True
            '    txtPriceCode.BackColor = Color.White
            'Else
            '    txtPriceCode.Enabled = False
            '    lblPriceCode.Enabled = False
            '    txtPriceCode.BackColor = Color.Linen
            'End If
            chkIsVolume.Enabled = True

        End If
    End Sub

    Private Sub txtOriginalPriceTK_TextChanged(sender As Object, e As EventArgs) Handles txtOriginalPriceTK.TextChanged
        CalculateOriginalGoldPrice()
        CalculateOriginalPrice()
    End Sub

    Private Sub chkIsOriginalPriceGram_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsOriginalPriceGram.CheckedChanged
        If chkIsOriginalPriceGram.Checked = False Then
            radGramPrice.Checked = True
            radGramPrice.Enabled = False
            radKPrice.Enabled = False

            txtOriginalPriceGram.Enabled = False
            txtOriginalPriceTK.Enabled = False
            txtOriginalGemsPrice.Enabled = False

            txtOriginalPriceGram.BackColor = Color.Gainsboro
            txtOriginalPriceTK.BackColor = Color.Gainsboro
            txtOriginalGemsPrice.BackColor = Color.Gainsboro
            txtOriginalOtherPrice.BackColor = Color.Gainsboro

            txtOriginalPriceGram.Text = "0"
            txtOriginalPriceTK.Text = "0"
            txtOriginalGemsPrice.Text = "0"
            'txtOriginalOtherPrice.Text = "0"
        Else
            If cboGoldQuality.Text <> "" Then
                Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
                GoldQualityInfo = _GoldQCon.GetGoldQuality(cboGoldQuality.SelectedValue)
                _IsGram = GoldQualityInfo.IsGramRate
                If _IsGram = True Then
                    radGramPrice.Enabled = True
                    radKPrice.Enabled = False
                    radGramPrice.Checked = True
                    txtOriginalPriceTK.Enabled = False
                    txtOriginalPriceTK.Text = "0"
                    txtOriginalPriceGram.Enabled = True
                    txtOriginalPriceTK.BackColor = Color.Gainsboro
                    txtOriginalPriceGram.BackColor = Color.White
                Else
                    radKPrice.Enabled = True
                    radGramPrice.Enabled = False
                    radKPrice.Checked = True
                    txtOriginalPriceGram.Text = "0"
                    txtOriginalPriceTK.Enabled = True
                    txtOriginalPriceTK.Enabled = True
                    txtOriginalPriceGram.Enabled = False

                    txtOriginalPriceTK.BackColor = Color.White
                    txtOriginalPriceGram.BackColor = Color.Gainsboro
                End If
            Else
                radGramPrice.Enabled = False
                radKPrice.Enabled = False
                txtOriginalPriceTK.Enabled = False
                txtOriginalPriceTK.Text = "0"
                txtOriginalPriceGram.Enabled = False
                txtOriginalPriceGram.Text = "0"
                txtOriginalPriceTK.BackColor = Color.Gainsboro
                txtOriginalPriceGram.BackColor = Color.Gainsboro
            End If

            txtOriginalGemsPrice.Enabled = True

            txtOriginalGemsPrice.BackColor = Color.White
            txtOriginalOtherPrice.BackColor = Color.Gainsboro
            chkIsOriginalFixedPrice.Checked = False
            txtOriginalFixedPrice.Enabled = False
            txtOriginalFixedPrice.BackColor = Color.Gainsboro
            txtOriginalFixedPrice.Text = "0"

            txtOriginalPriceGram.Text = "0"
            txtOriginalPriceTK.Text = "0"
            txtOriginalGemsPrice.Text = "0"
            'txtOriginalOtherPrice.Text = "0"
        End If
        CalculategrAlldTotalAmount()
    End Sub

    Private Sub txtOriginalPriceGram_TextChanged(sender As Object, e As EventArgs) Handles txtOriginalPriceGram.TextChanged
        CalculateOriginalGoldPrice()
        CalculateOriginalPrice()
    End Sub

    Private Sub chkIsOriginalFixedPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsOriginalFixedPrice.CheckedChanged
        If chkIsOriginalFixedPrice.Checked = False Then
            txtOriginalFixedPrice.Enabled = False
            txtOriginalFixedPrice.BackColor = Color.Gainsboro
            txtOriginalFixedPrice.Text = "0"
        Else
            txtOriginalFixedPrice.Enabled = True
            txtOriginalFixedPrice.BackColor = Color.White
            chkIsOriginalPriceGram.Checked = False
            txtOriginalPriceGram.Enabled = False
            txtOriginalPriceTK.Enabled = False
            txtOriginalGemsPrice.Enabled = False

            txtOriginalPriceGram.BackColor = Color.Gainsboro
            txtOriginalPriceTK.BackColor = Color.Gainsboro
            txtOriginalGemsPrice.BackColor = Color.Gainsboro
            txtOriginalOtherPrice.BackColor = Color.Gainsboro

            txtOriginalPriceGram.Text = "0"
            txtOriginalPriceTK.Text = "0"
            txtOriginalGemsPrice.Text = "0"
            txtOriginalOtherPrice.Text = "0"
            radGramPrice.Checked = True
            radGramPrice.Enabled = False
            radKPrice.Enabled = False
        End If
    End Sub
    Private Sub chkOriginalPriceCarat_CheckedChanged(sender As Object, e As EventArgs) Handles chkOriginalPriceCarat.CheckedChanged
        If chkOriginalPriceCarat.Checked = False Then
            txtOriginalPriceCarat.Enabled = False
            txtOriginalPriceCarat.BackColor = Color.Gainsboro
            txtOriginalPriceCarat.Text = "0"
        Else
            txtOriginalPriceCarat.Enabled = True
            txtOriginalPriceCarat.BackColor = Color.White

            chkDOriginalFixPrice.Checked = False
            txtDOriginalFixPrice.Enabled = False
            txtDOriginalFixPrice.BackColor = Color.Gainsboro
            txtDOriginalFixPrice.Text = "0"
        End If
    End Sub
    Private Sub chkDOriginalFixedPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkDOriginalFixPrice.CheckedChanged
        If chkDOriginalFixPrice.Checked = False Then
            txtDOriginalFixPrice.Enabled = False
            txtDOriginalFixPrice.BackColor = Color.Gainsboro
            txtDOriginalFixPrice.Text = "0"
        Else
            txtDOriginalFixPrice.Enabled = True
            txtDOriginalFixPrice.BackColor = Color.White

            chkOriginalPriceCarat.Checked = False
            txtOriginalPriceCarat.Enabled = False
            txtOriginalPriceCarat.BackColor = Color.Gainsboro
            txtOriginalPriceCarat.Text = "0"
        End If
    End Sub

    Private Sub txtOriginalFixedPrice_TextChanged(sender As Object, e As EventArgs) Handles txtOriginalFixedPrice.TextChanged
        CalculateOriginalPrice()
    End Sub

    Private Sub txtOriginalGemsPrice_TextChanged(sender As Object, e As EventArgs) Handles txtOriginalGemsPrice.TextChanged
        CalculateOriginalPrice()
    End Sub

    Private Sub txtOriginalOtherPrice_TextChanged(sender As Object, e As EventArgs) Handles txtOriginalOtherPrice.TextChanged
        CalculateOriginalPrice()
    End Sub

    Private Sub txtGoldSmith_Validated(sender As Object, e As EventArgs) Handles txtGoldSmith.Validated
        LnkTotalWeight.Focus()
    End Sub

    Private Sub txtSaleFixPrice_TextChanged(sender As Object, e As EventArgs) Handles txtSaleFixPrice.TextChanged
        If txtSaleFixPrice.Text = "" Then txtSaleFixPrice.Text = "0"
        ' CalculateSellingAmt()
    End Sub
    Private Sub txtWSFixPrice_TextChanged(sender As Object, e As EventArgs) Handles txtWSFixPrice.TextChanged
        If txtWSFixPrice.Text = "" Then txtWSFixPrice.Text = "0"
        'CalculateSellingAmt()
    End Sub

    Private Sub LnkCategory_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkCategory.LinkClicked
        Dim frm As New frm_ItemCategory
        frm.ShowDialog()
    End Sub

    Private Sub LnkItemName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkItemName.LinkClicked
        Dim frm As New frm_ItemName
        frm.ShowDialog()
    End Sub

    Private Sub LnkGoldQuality_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkGoldQuality.LinkClicked
        Dim frm As New frm_GoldQuality
        frm.ShowDialog()
    End Sub
    Private Sub ShowWasteData()
        Dim objWasteSetupDetail As New CommonInfo.WasteSetupDetailInfo

        objWasteSetupDetail = _WasteSetup.GetWasetSetupInfoByStockWeight(_ItemTK, cboItemCategory.SelectedValue, cboItemName.SelectedValue, cboGoldQuality.SelectedValue)

        If Not IsNothing(objWasteSetupDetail.WasteSetupHeaderID) Then
            With objWasteSetupDetail
                txtWasteP.Text = .MinWeightPForSale
                If numberformat = 1 Then
                    txtWasteY.Text = Format(.MinWeightYForSale, "0.0")
                Else
                    txtWasteY.Text = Format(.MinWeightYForSale, "0.00")
                End If
                'txtWasteY.Text = Format(.MinWeightYForSale, "0.0")
                txtWasteTG.Text = Format(.MinWeightTGForSale, "0.000")
                _WasteTK = .MinWeightTKForSale
                _WasteTG = .MinWeightTGForSale
            End With
        End If
    End Sub

    'Private Sub cboItemName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboItemName.SelectedIndexChanged
    '    If _IsUpdate = True Then
    '        cboItemName.SelectedValue = _ItemNameID
    '    End If
    '    'RefreshItemNameCbo()
    'End Sub

    Private Sub cboItemName_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboItemName.SelectedValueChanged
        'If _IsUpdate = True Then
        '    cboItemName.SelectedValue = _ItemNameID
        'End If
        Dim ID As String
        ID = cboItemName.SelectedValue
        ShowWasteData()

    End Sub

    Private Sub lnkSupplier_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkSupplier.LinkClicked
        Dim frm As New frm_Supplier
        frm.ShowDialog()
    End Sub

    Private Sub cboSupplier_Click(sender As Object, e As EventArgs) Handles cboSupplier.Click
        GetSupplier()
    End Sub



    Private Sub cboSupplier_KeyUp(sender As Object, e As KeyEventArgs) Handles cboSupplier.KeyUp
        AutoCompleteCombo_KeyUp(cboSupplier, e)
    End Sub

    Private Sub cboSupplier_Leave(sender As Object, e As EventArgs) Handles cboSupplier.Leave
        AutoCompleteCombo_Leave(cboSupplier, "")

    End Sub


    Private Sub cboGoldSmith_Click(sender As Object, e As EventArgs) Handles cboGoldSmith.Click
        GetGoldSmith()
    End Sub

    Private Sub cboGoldSmith_KeyUp(sender As Object, e As KeyEventArgs) Handles cboGoldSmith.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldSmith, e)
    End Sub

    Private Sub cboGoldSmith_Leave(sender As Object, e As EventArgs) Handles cboGoldSmith.Leave
        AutoCompleteCombo_Leave(cboGoldSmith, "")
    End Sub
    Private Sub lnkColor_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkColor.LinkClicked
        Dim frmkeyword As New frm_Keyword
        frmkeyword._KeywordName = "Color"
        frmkeyword.ShowDialog()
        LoadCombos()
        'If cboPercent.SelectedIndex = -1 Then
        '    MsgBox("စစ္ေဆးရန္ဓ", MsgBoxStyle.Information, AppName)
        'End If
    End Sub
    Private Sub LoadCombos()
        cboColor.DisplayMember = "ItemName"
        cboColor.ValueMember = "ItemName"
        cboColor.DataSource = _GetKeyword.GetKeywordItemsByHeaderName("Color")
    End Sub

    Private Sub chkIsVolume_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsVolume.CheckedChanged
        If chkIsVolume.Checked Then
            tabStock.TabPages.Remove(TabStockItem)
            tabStock.TabPages.Remove(TabLooseDiamond)
            tabStock.TabPages.Add(TabVolumeStock)
            chkIsClosed.Checked = False
            chkIsClosed.Enabled = False
            GetItemCategoryByVolume()
            cboVGoldQuality.SelectedIndex = -1
            chkIsOrder.Checked = False
            chkIsOrder.Visible = False
            cboStaff.TabIndex = 9
            chkIsDiamond.Enabled = False
        Else
            tabStock.TabPages.Remove(TabVolumeStock)
            tabStock.TabPages.Add(TabStockItem)
            chkIsClosed.Enabled = False
            GetItemCategory()
            cboGoldQuality.SelectedIndex = -1
            chkIsOrder.Visible = True
            chkIsDiamond.Enabled = True
        End If
        AllClear()
    End Sub
    Private Sub LnkDCategory_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkDCategory.LinkClicked
        Dim frm As New frm_GemsCategory
        frm.ShowDialog()
    End Sub
    Private Sub lnkDColor_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkDColor.LinkClicked
        Dim frmkeyword As New frm_Keyword
        frmkeyword._KeywordName = "Color"
        frmkeyword.ShowDialog()
        LoadColorCombos()
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
    Private Sub lnkDSupplier_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkDSupplier.LinkClicked
        Dim frm As New frm_Supplier
        frm.ShowDialog()
    End Sub
    Private Sub btnDAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDAdd.Click

        If btnAdd.Text = "Add" Then
            If Global_PhotoPath <> "" Then

                OpenFileDia.Filter = "Image (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png;"
                '"Image (*.jpg;)|*.jpg;"
                OpenFileDia.FileName = Global_PhotoPath + "\"
                OpenFileDia.InitialDirectory = OpenFileDia.FileName
                OpenFileDia.ShowDialog()
                If OpenFileDia.FileName <> "" Then
                    If OpenFileDia.InitialDirectory = OpenFileDia.FileName Then
                        'lblItemImage.Image = Nothing
                        'btnAdd.Text = "Add"
                        'lblPhoto.Visible = True
                        'PName = ""
                        If Global_LogoPhoto <> "" Then
                            lblDItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                            DPName = Global_LogoPhoto
                            btnDAdd.Text = "Add"
                            lblDPhoto.Visible = False
                        Else

                            lblDItemImage.Image = Nothing
                            lblDPhoto.Visible = True
                            DPName = ""
                            btnDAdd.Text = "Add"
                        End If
                        Exit Sub

                    End If
                    lblDItemImage.Image = System.Drawing.Image.FromFile(OpenFileDia.FileName)
                    DPName = OpenFileDia.FileName.Substring(Global_PhotoPath.Length + 1)
                    btnDAdd.Text = "Remove"
                End If
                lblDPhoto.Visible = False
            End If
        Else
            If Global_LogoPhoto <> "" Then
                lblDItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                DPName = Global_LogoPhoto
                btnDAdd.Text = "Add"
                lblDPhoto.Visible = False
            Else

                lblDItemImage.Image = Nothing
                lblDPhoto.Visible = True
                DPName = ""
                'DefaultPhoto = "ri.jpg"
                btnDAdd.Text = "Add"
            End If
        End If
    End Sub
    Private Sub chkIsLooseDiamond_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsLooseDiamond.CheckedChanged
        If (chkIsLooseDiamond.Checked) Then
            tabStock.TabPages.Remove(TabStockItem)
            tabStock.TabPages.Remove(TabVolumeStock)
            tabStock.TabPages.Add(TabLooseDiamond)
            chkIsClosed.Checked = False
            chkIsClosed.Enabled = True
            GetDItemCategory()
            cboDCategory.SelectedIndex = -1
            'cboVGoldQuality.SelectedIndex = -1
            chkIsDiamond.Checked = False
            chkIsDiamond.Visible = False
            chkIsOrder.Checked = False
            chkIsOrder.Visible = False
            cboStaff.TabIndex = 9

        Else
            tabStock.TabPages.Remove(TabLooseDiamond)
            'tabStock.TabPages.Add(TabStockItem)
            tabStock.TabPages.Add(TabStockItem)
            'chkIsClosed.Enabled = False
            GetItemCategory()
            cboGoldQuality.SelectedIndex = -1
            chkIsOrder.Visible = True
            chkIsDiamond.Enabled = True
            chkIsDiamond.Visible = True
        End If
        AllClear()
    End Sub
    'Private Sub chkisNormal_CheckedChanged(sender As Object, e As EventArgs) Handles chkisNormal.CheckedChanged
    '    If (chkisNormal.Checked) Then
    '        tabStock.TabPages.Remove(TabVolumeStock)
    '        tabStock.TabPages.Remove(TabLooseDiamond)
    '        tabStock.TabPages.Add(TabStockItem)
    '        chkIsClosed.Checked = False
    '        chkIsClosed.Enabled = True
    '        GetItemCategory()
    '        cboItemCategory.SelectedIndex = -1
    '        'cboVGoldQuality.SelectedIndex = -1
    '        chkIsDiamond.Checked = False
    '        chkIsDiamond.Visible = True
    '        chkIsOrder.Checked = False
    '        chkIsOrder.Visible = True
    '        cboStaff.TabIndex = 9

    '    Else
    '        tabStock.TabPages.Remove(TabStockItem)
    '        cboGoldQuality.SelectedIndex = -1
    '        chkIsOrder.Visible = False
    '        chkIsDiamond.Enabled = False
    '        chkIsDiamond.Visible = False
    '    End If
    '    AllClear()
    'End Sub

    Private Sub cboDCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDCategory.SelectedIndexChanged
        ditemid = cboDCategory.SelectedValue
        SaleItemCodeGenerateFormat()
        _DPrefix = ItemCategoryPrefix
    End Sub

    Private Sub txtRBP_LostFocus(sender As Object, e As EventArgs) Handles txtRBP.LostFocus
        'For GemsWeight Yati,B,Karat
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
            _DRBP = "0"
            _DItemTG = "0.0"
            _DItemTK = "0.0"
            _SDGemsTW = "0.0"

        Else
            If VarWeight.EndsWith("ct") Then
                If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                    VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                    ' Notes: For Karat,multiply 1.1 
                    TC = CStr(VarWeightBCG)
                    If Global_IsCarat = 0 Or Global_IsCarat = 2 Then 'If Global_IsCarat = True Then
                        _SDGemsTW = CDec(VarWeightBCG)
                    Else
                        _SDGemsTW = CDec(VarWeightBCG * 1.1)
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
                            _SDGemsTW = TC
                        Else
                            _SDGemsTW = VarWeightY
                        End If
                        IsValid = True
                    Else
                        VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                        equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                        TC = VarWeight / equivalent
                        If Global_IsCarat = 2 Then
                            _SDGemsTW = TC
                        Else
                            _SDGemsTW = VarWeight
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
                                _SDGemsTW = TC
                            Else
                                _SDGemsTW = TY
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
                            _SDGemsTW = TC
                        Else
                            _SDGemsTW = TY
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
                                _SDGemsTW = TC
                            Else
                                _SDGemsTW = TY
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
                                _SDGemsTW = TC
                            Else
                                _SDGemsTW = TY
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
                                _SDGemsTW = TC
                            Else
                                _SDGemsTW = TY
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
                _DItemTG = "0.0"
                _DItemTK = "0.0"
                _SDGemsTW = "0.0"
            End If

            equivalent = Global_GramToKarat '_ConverterCon.GetMeasurement("Gram", "Karat")
            Dim gram As Decimal = TC / equivalent
            equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
            GoldWeight.GoldTK = gram / equivalent
            _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            _DItemTK = gram / equivalent
            _DItemTG = gram
            _DCarat = gram * Global_GramToKarat
            _DRBP = txtRBP.Text
            txtDGram.Text = Format(_DItemTG, "0.000")
        End If
        CalculateDOriginalPrice()

    End Sub

    Private Sub txtDOriginalFixPrice_TextChanged(sender As Object, e As EventArgs) Handles txtDOriginalFixPrice.TextChanged
        If txtDOriginalFixPrice.Text = "" Then txtDOriginalFixPrice.Text = "0"
        CalculateDOriginalPrice()
    End Sub

    Private Sub txtOriginalPriceCarat_TextChanged(sender As Object, e As EventArgs) Handles txtOriginalPriceCarat.TextChanged
        If txtOriginalPriceCarat.Text = "" Then txtOriginalPriceCarat.Text = "0"
        CalculateDOriginalPrice()
    End Sub
    Private Sub txtDOriginalPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDOriginalPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDesignCharges_TextChanged(sender As Object, e As EventArgs) Handles txtDesignCharges.TextChanged
        CalculateOtherCharges()
    End Sub
    Private Sub CalculateOtherCharges()
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = 0
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = 0
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = 0
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = 0

        txtOriginalOtherPrice.Text = CInt(txtDesignCharges.Text) + CInt(txtWhiteCharges.Text) + CInt(txtMountingCharges.Text) + CInt(txtPlatingCharges.Text)
    End Sub

    Private Sub txtPlatingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtPlatingCharges.TextChanged
        CalculateOtherCharges()
    End Sub

    Private Sub txtMountingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtMountingCharges.TextChanged
        CalculateOtherCharges()
    End Sub

    Private Sub txtWhiteCharges_TextChanged(sender As Object, e As EventArgs) Handles txtWhiteCharges.TextChanged
        CalculateOtherCharges()
    End Sub
End Class
