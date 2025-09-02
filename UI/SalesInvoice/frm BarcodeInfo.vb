Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms

Public Class frm_BarcodeInfo

    Private _ForSaleID As String = ""
    Private _BarcodeNo As String = ""
    Private _GoldQualityID As String = ""
    Private _ItemCategoryID As String = ""
    Private _IsGram As Boolean = False
    Private _dtSalesInvoiceItem As DataTable
    Private PName As String = ""
    Private _GoldPrice As Integer = 0
    Private _FixPrice As Integer = 0

    Dim _GoldTKForSale As Decimal = 0
    Dim _GoldTGForSale As Decimal = 0

    Dim _GemsTKForSale As Decimal = 0.0
    Dim _GemsTGForSale As Decimal = 0.0

    Dim _WasteTKForSale As Decimal = 0.0
    Dim _WasteTGForSale As Decimal = 0.0

    Private _PWasteTK As Decimal = 0.0
    Private _PWasteTG As Decimal = 0.0

    Dim isFixPrice As Boolean = False
    Dim isOriginalFixPrice As Boolean = False
    Private _OriginalFixPrice As Integer = 0

    Private _dtItemBarcode As DataTable
    Private _dtAllDiamond As DataTable
    Private _IsGemInDB As Boolean = False
    Private _PGoldPrice As Integer = 0

    Private objGemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private objSalesInvoiceController As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _SalesItemController As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _CurrentController As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Dim numberformat As Integer



    Private Sub frm_BarcodeInfo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtBarcodeNo.Focus()
        lblIsGram.Text = ""
        lblPercent.Text = ""
    End Sub

    Private Sub frm_BarcodeInfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Clear()
    End Sub
    Private Sub Clear()
        numberformat = Global_DecimalFormat

        txtBarcodeNo.Focus()
        txtBarcodeNo.ReadOnly = False
        txtBarcodeNo.Text = ""
        txtCurrentPrice.Text = "0"
        txtItemCategory.Text = ""
        txtItemName.Text = ""

        txtGoldQuality.Text = ""
        txtLength.Text = ""


        txtItemTGForSale.Text = "0.000"
        txtItemForSaleK.Text = "0"
        txtItemPForSale.Text = "0"
        txtItemYForSale.Text = "0.0"

        txtWasteTGForSale.Text = "0.000"
        txtWasteKForSale.Text = "0"
        txtWastePForSale.Text = "0"
        txtWasteYForSale.Text = "0.0"

        txtGoldTGForSale.Text = "0.000"
        txtGoldKForSale.Text = "0"
        txtGoldPForSale.Text = "0"
        txtGoldYForSale.Text = "0.0"

        txtGemTGForSale.Text = "0.000"
        txtGemKForSale.Text = "0"
        txtGemPForSale.Text = "0"
        txtGemYForSale.Text = "0.0"


        _WasteTKForSale = "0"
        _WasteTGForSale = "0"
        _GemsTGForSale = "0"
        _GemsTKForSale = "0"
        _GoldTKForSale = "0"
        _GoldTGForSale = "0"

        txtWhiteCharges.Text = "0"
        txtPlatingCharges.Text = "0"
        txtMountingCharges.Text = "0"
        txtDesignCharges.Text = "0"
        chkIsFixPrice.Checked = False

        txtGoldPrice.Text = "0"
        txtGemsPrice.Text = "0"
        txtTotalAmt.Text = "0"

        lblIsGram.Text = ""
        lblPercent.Text = ""
        txtPWasteK.Text = "0"
        txtPWasteP.Text = "0"
        txtPWasteY.Text = "0"
        txtPWasteTG.Text = "0.000"

        txtOriginalPrice.Text = "0"
        txtOriginalPriceTK.Text = "0"
        txtOriginalOtherPrice.Text = "0"
        txtOriginalGemsPrice.Text = "0"

        txtPTotAmt.Text = "0"
        txtBalanceAmount.Text = "0"



        If Global_LogoPhoto <> "" Then
            Try
                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
            Catch ex As Exception
                MsgBox(ex.Message + " does not exist.")
                lblItemImage.Image = Nothing
            End Try
        Else
            lblItemImage.Image = Nothing
        End If


        'isFixPrice = False
        '_FixPrice = 0
        _IsGemInDB = False
    End Sub
    Private Sub txtBarcodeNo_TextChanged(sender As Object, e As EventArgs) Handles txtBarcodeNo.TextChanged
        Dim dtSaleItem As New DataTable
        Dim objSItem As SalesItemInfo
        If txtBarcodeNo.Text <> "" Then
            _BarcodeNo = txtBarcodeNo.Text
            objSItem = _SalesItemController.GetForSaleInfoByItemCode(_BarcodeNo, " And F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='0' AND IsClosed='0'")

            ShowForSaleBarcodeData(objSItem)
        Else
            Clear()
        End If

    End Sub
    Private Sub ShowForSaleBarcodeData(ByVal objSItem As SalesItemInfo)
        Dim objCurrentPrice As New CurrentPriceInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo

        With objSItem
            _ForSaleID = .ForSaleID
            txtBarcodeNo.Text = _BarcodeNo
            _ItemCategoryID = .ItemCategoryID
            txtItemCategory.Text = _ItemCategoryController.GetItemCategory(_ItemCategoryID).ItemCategory

            txtLength.Text = .Length
            txtItemName.Text = .ItemName
            _GoldQualityID = .GoldQualityID
            txtGoldQuality.Text = _GoldQualityController.GetGoldQuality(_GoldQualityID).GoldQuality
            _IsGram = _GoldQualityController.GetGoldQuality(_GoldQualityID).IsGramRate

            'If .IsFixPrice Then
            If _ForSaleID <> "" Then
                If _IsGram Then
                    lblPercent.Text = "၁ ဂရမ်စျေး"
                Else
                    lblPercent.Text = "၁ ကျပ်သားစျေး"
                End If
            Else
                lblPercent.Text = ""
            End If
            'End If



            objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(_GoldQualityID)
            ShowCurrentPrice(objCurrentPrice)

            'txtItemForSaleK.Text = .ItemK
            'txtItemPForSale.Text = .ItemP
            'txtItemYForSale.Text = Math.Round(.ItemY + .ItemC, 1)
            'txtItemTGForSale.Text = Math.Round(.ItemTG, 3)
            'txtItemTKForSale.Text = Math.Round(.ItemTK, 3)

            txtItemTGForSale.Text = Format(.ItemTG, "0.000")
            ' If txtItemTKForSale.Text <> "0" Then
            GoldWeight.GoldTK = .ItemTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtItemForSaleK.Text = CStr(GoldWeight.WeightK)
            txtItemPForSale.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtItemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtItemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtItemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            '  End If
            txtWasteTGForSale.Text = Format(.WasteTG, "0.000")

            GoldWeight.GoldTK = .WasteTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteKForSale.Text = CStr(GoldWeight.WeightK)
            txtWastePForSale.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtWasteYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtWasteYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtWasteYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            ' End If

            _WasteTKForSale = .WasteTK
            _WasteTGForSale = .WasteTG

            GoldWeight.GoldTK = .TotalTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)

            txtGoldTGForSale.Text = Format(.GoldTG, "0.000")

            GoldWeight.GoldTK = .GoldTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtGoldKForSale.Text = CStr(GoldWeight.WeightK)
            txtGoldPForSale.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtGoldYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtGoldYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtGoldYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

            _GoldTKForSale = .GoldTK
            _GoldTGForSale = .GoldTG

            txtGemTGForSale.Text = Format(.GemsTG, "0.000")

            GoldWeight.GoldTK = .GemsTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtGemKForSale.Text = CStr(GoldWeight.WeightK)
            txtGemPForSale.Text = CStr(GoldWeight.WeightP)
            txtGemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

            '14/05/2014 **************
            _PWasteTK = .PurchaseWasteTK
            _PWasteTG = .PurchaseWasteTG

            txtPWasteTG.Text = Format(.PurchaseWasteTG, "0.000")

            GoldWeight.GoldTK = .PurchaseWasteTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtPWasteK.Text = CStr(GoldWeight.WeightK)
            txtPWasteP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))

            End If
            'txtPWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

            txtOriginalGemsPrice.Text = .OriginalGemsPrice
            txtOriginalOtherPrice.Text = .OriginalOtherPrice

            '*****************
            txtWhiteCharges.Text = .WhiteCharges
            txtPlatingCharges.Text = .PlatingCharges
            txtMountingCharges.Text = .MountingCharges
            txtDesignCharges.Text = .DesignCharges
            chkIsFixPrice.Checked = .IsFixPrice
            If .OriginalPriceTK <> 0 Then
                txtOriginalPrice.Text = .OriginalPriceTK
            Else
                txtOriginalPrice.Text = .OriginalPriceGram
            End If

            txtOriginalPriceTK.Text = txtOriginalPrice.Text



            GoldWeight.GoldTK = .ItemTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)

            GoldWeight.GoldTK = .WasteTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)

            GoldWeight.GoldTK = .TotalTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)

            GoldWeight.GoldTK = .GoldTK
            GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)

            ''for Sale (14/05/2014)

            Dim TempTK As Decimal = 0.0
            Dim Gold As New CommonInfo.GoldWeightInfo

            Gold.WeightK = CInt(txtGoldKForSale.Text) + CInt(txtWasteKForSale.Text)
            Gold.WeightP = CInt(txtGoldPForSale.Text) + CInt(txtWastePForSale.Text)
            Gold.WeightY = System.Decimal.Truncate(CDec(txtGoldYForSale.Text) + CDec(txtWasteYForSale.Text))
            Gold.WeightC = (CDec(txtGoldYForSale.Text) + CDec(txtWasteYForSale.Text)) - Gold.WeightY
            Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
            TempTK = Gold.GoldTK

            _dtSalesInvoiceItem = _SalesItemController.GetSalesItemGems(_ForSaleID)

            Dim grdtotalAmt As Decimal = 0.0
            If .IsFixPrice = False Then
                For i As Integer = 0 To _dtSalesInvoiceItem.Rows.Count - 1
                    grdtotalAmt += CInt(Val(_dtSalesInvoiceItem.Rows(i).Item("Amount")))
                Next
                txtGemsPrice.Text = CStr(grdtotalAmt)
            Else
                txtGemsPrice.Text = "0"
            End If

            If (.IsFixPrice = True) Then
                isFixPrice = True
                _FixPrice = .FixPrice
                txtGoldPrice.Text = "0"
                txtGemsPrice.Text = "0"
                txtTotalAmt.Text = CStr(_FixPrice)
            Else
                isFixPrice = False
                If GoldQualityIsGramRate() = False Then
                    _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * TempTK)
                Else
                    _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * CDec(_GoldTGForSale + _WasteTGForSale))
                End If
                txtGoldPrice.Text = CLng(_GoldPrice)
                txtTotalAmt.Text = CStr(CLng(txtGoldPrice.Text) + CLng(txtGemsPrice.Text) + CLng(txtWhiteCharges.Text) + CLng(txtPlatingCharges.Text) + CLng(txtMountingCharges.Text) + CLng(txtDesignCharges.Text))
            End If

            ' for Purchase (14/05/2014)

            Dim PTempTK As Decimal = 0.0
            Dim PGold As New CommonInfo.GoldWeightInfo

            Gold.WeightK = CInt(txtGoldKForSale.Text) + CInt(txtPWasteK.Text)
            Gold.WeightP = CInt(txtGoldPForSale.Text) + CInt(txtPWasteP.Text)
            Gold.WeightY = System.Decimal.Truncate(CDec(txtGoldYForSale.Text) + CDec(txtPWasteY.Text))
            Gold.WeightC = (CDec(txtGoldYForSale.Text) + CDec(txtPWasteY.Text)) - Gold.WeightY
            Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
            TempTK = Gold.GoldTK

            If (.IsOriginalFixedPrice = True) Then
                isOriginalFixPrice = True
                _OriginalFixPrice = .OriginalFixedPrice
                txtOriginalGemsPrice.Text = "0"
                txtOriginalOtherPrice.Text = "0"
                txtPTotAmt.Text = CStr(_OriginalFixPrice)
            Else
                isOriginalFixPrice = False
                If GoldQualityIsGramRate() = False Then
                    _PGoldPrice = CStr(CLng(txtOriginalPriceTK.Text) * TempTK)
                Else
                    _PGoldPrice = CStr(CLng(txtOriginalPriceTK.Text) * CDec(_GoldTGForSale + _PWasteTG))
                End If
                txtOriginalPriceTK.Text = CLng(_PGoldPrice)
                txtPTotAmt.Text = CStr(CLng(txtOriginalGemsPrice.Text) + CLng(txtOriginalOtherPrice.Text) + CLng(txtOriginalPriceTK.Text))
            End If

            txtBalanceAmount.Text = CStr(CLng(txtTotalAmt.Text) - CLng(txtPTotAmt.Text))
            '***************************
            If .Photo <> "" Then
                lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + .Photo)
                PName = .Photo
            Else
                If Global_LogoPhoto <> "" Then
                    Try
                        lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + Global_LogoPhoto)
                    Catch ex As Exception
                        MsgBox(ex.Message + " does not exist.")
                        lblItemImage.Image = Nothing
                    End Try
                Else
                    lblItemImage.Image = Nothing
                End If
            End If
        End With

    End Sub
    Public Function GoldQualityIsGramRate() As Boolean
        Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
        GoldQualityInfo = _GoldQualityController.GetGoldQuality(_GoldQualityID)
        Return GoldQualityInfo.IsGramRate
    End Function
    Private Sub ShowCurrentPrice(ByVal objCurrentPrice As CurrentPriceInfo)
        txtCurrentPrice.Text = objCurrentPrice.SalesRate
        If _IsGram = True Then
            lblIsGram.Text = "၁ ဂရမ်စျေး"
        Else
            lblIsGram.Text = "၁ ကျပ်သားစျေး"
        End If
    End Sub

    Private Sub txtGemsPrice_TextChanged(sender As Object, e As EventArgs) Handles txtGemsPrice.TextChanged
        If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
        If txtGemsPrice.Text.Trim >= 0 Then
            CalculateTotalAmount()
        End If
    End Sub


    'Private Sub txtGoldPrice_TextChanged(sender As Object, e As EventArgs) Handles txtGoldPrice.TextChanged
    '    If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
    '    If txtGemsPrice.Text.Trim >= 0 Then
    '        CalculateTotalAmount()
    '    End If
    'End Sub

    Private Sub CalculateTotalAmount()

        ''''### OLD 
        'If isFixPrice = False Then
        '    If (Val(txtGoldPrice.Text.Trim) <> 0 Or Val(txtGemsPrice.Text.Trim) <> 0 Or Val(txtWhiteCharges.Text.Trim) <> 0 Or Val(txtPlatingCharges.Text.Trim) <> 0 Or Val(txtMountingCharges.Text.Trim) <> 0 Or Val(txtDesignCharges.Text.Trim) <> 0) Then
        '        txtTotalAmt.Text = CStr(CLng(txtGoldPrice.Text) + CLng(txtGemsPrice.Text) + CLng(txtWhiteCharges.Text) + CLng(txtPlatingCharges.Text) + CLng(txtMountingCharges.Text) + CLng(txtDesignCharges.Text))
        '        'txtNetAmt.Text = txtTotalAmt.Text
        '    Else
        '        txtTotalAmt.Text = "0"
        '    End If
        'Else
        '    txtTotalAmt.Text = _FixPrice
        'End If
        ''''##
        Dim TempTK As Decimal = 0.0
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"

        Dim GoldWeight As New CommonInfo.GoldWeightInfo

        GoldWeight.WeightK = CInt(txtGoldKForSale.Text) + CInt(txtWasteKForSale.Text)
        GoldWeight.WeightP = CInt(txtGoldPForSale.Text) + CInt(txtWastePForSale.Text)
        GoldWeight.WeightY = System.Decimal.Truncate(CDec(txtGoldYForSale.Text) + CDec(txtWasteYForSale.Text))
        GoldWeight.WeightC = (CDec(txtGoldYForSale.Text) + CDec(txtWasteYForSale.Text)) - GoldWeight.WeightY
        GoldWeight.GoldTK = objConverterController.ConvertKPYCToGoldTK(GoldWeight)
        TempTK = GoldWeight.GoldTK



        If (isFixPrice = True) Then
            txtGoldPrice.Text = "0"
            txtGemsPrice.Text = "0"
            txtTotalAmt.Text = CStr(_FixPrice)
            'txtBalanceAmt.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text))
        Else
            isFixPrice = False
            If txtCurrentPrice.Text <> "0" Then
                If GoldQualityIsGramRate() = False Then
                    _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * TempTK)
                Else
                    _GoldPrice = CStr(CLng(txtCurrentPrice.Text) * (CDec(txtGoldTGForSale.Text) + CDec(txtWasteTGForSale.Text)))
                End If
                txtGoldPrice.Text = CLng(_GoldPrice)
                txtTotalAmt.Text = CStr(CLng(txtGoldPrice.Text) + CLng(txtGemsPrice.Text) + CLng(txtWhiteCharges.Text) + CLng(txtPlatingCharges.Text) + CLng(txtMountingCharges.Text) + CLng(txtDesignCharges.Text))
            End If
        End If

    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("BarcodeInfo")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class