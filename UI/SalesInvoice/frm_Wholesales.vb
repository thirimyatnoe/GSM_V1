Imports BusinessRule
Imports CommonInfo
Imports Operational.AppConfiguration
Imports System.Text
Imports System.Net
Public Class frm_Wholesales
    Implements IFormProcess


#Region "Declaration"
    Private _WSInvoieID As String
    Private _WSInvoieItemID As String
    Private _SaleInvoiceID As String
    Private _CustomerID As String

    Private _IsGram As Boolean = False
    Dim _GemsTK As Decimal = 0.0
    Dim _GemsTG As Decimal = 0.0
    Dim _WasteTK As Decimal = 0.0
    Dim _WasteTG As Decimal = 0.0
    Dim _ItemTK As Decimal = 0.0
    Dim _ItemTG As Decimal = 0.0
    Dim _GoldTK As Decimal = 0.0
    Dim _GoldTG As Decimal = 0.0


    Private _dtWSInvoiceItem As New DataTable

    Private _IsUpdate As Boolean = False
    Dim numberformat As Integer
    Dim TempTK As Decimal = 0.0
    Private OrgNetAmt As Long = 0

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
    Dim _IsMaximum As Boolean = False

    Dim dtMember As New DataTable()
    Dim dtRedeemID As New DataTable()
    Dim dtTransactionID As New DataTable()
    Private _CustType As String = ""
    Private _DealerPayAmount As Integer = 0
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private objWSaleInvoiceController As WholeSaleInvoice.IWholeSaleInvoiceController = Factory.Instance.CreateWholeSaleInvoiceController
    Private objCustomerController As Customer.ICustomerController = Factory.Instance.CreateCustomerController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private objSalesItemController As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private objCurrentController As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objWholeSaleReturnController As BusinessRule.WholeSaleReturn.IWholeSaleReturnController = Factory.Instance.CreateWholeSaleReturnController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _SalesItemController As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _CurrentCon As InternationalDiamond.IIntDiamondPriceRateController = Factory.Instance.CreateIntDiamondController
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
    Private Sub frm_Wholesales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "လက္ကားအေရာင္း"
        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        ClearData()
        FormatGrid()
        GetComBo()
    End Sub
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        'If objWSaleInvoiceController.DeleteWholeSaleInvoice(_WSInvoieID) Then
        '    ClearData()
        '    Return True
        'Else
        '    Return False
        'End If

        Dim objWS As CommonInfo.WholeSaleInvoiceInfo
        objWS = GetData()
        If objWSaleInvoiceController.DeleteWholeSaleInvoice(_WSInvoieID) Then

            If Global_IsUseMember = True Then

                If OppurtunityType <> "Item" Then

                    ''To Filter With RedeemID

                    If IsRedeemInvoice = True Then
                        UpdateRedeem(objWS, 2)
                    Else
                        AddRedeem(objWS, 2)
                    End If
                End If

                SaveSaleMemberCard(objWS, 2)
            End If
            ClearData()
            btnDelete.Enabled = False
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        ClearData()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objWSInvoice As New WholeSaleInvoiceInfo
        Dim tmpdt As New DataTable
        If IsFillData() Then
            objWSInvoice = GetData()
            If Global_IsUseMember = False Or txtMemberCode.Text = "" Then ' no use member 
                tmpdt = objWholeSaleReturnController.GetWholeSaleReturnByWSID(objWSInvoice.WholesaleInvoiceID)
                If tmpdt.Rows.Count > 0 Then
                    MsgBox("This Voucher already have WholeSale Return. Can't Update!", MsgBoxStyle.OkCancel, AppName)
                Else
                    If objWSaleInvoiceController.SaveWholeSaleInvoice(objWSInvoice, _dtWSInvoiceItem) Then
                        _WSInvoieID = objWSInvoice.WholesaleInvoiceID
                        If (MsgBox("Do You Want To Save And Print WholeSale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                            Dim frmPrint As New frm_ReportViewer
                            Dim UserInfo As New UserInfo
                            Dim dt As New DataTable
                            dt = objWSaleInvoiceController.GetWholeSalePrint(_WSInvoieID)
                            frmPrint.PrintWholeSale(dt)
                            ClearData()
                            frmPrint.WindowState = FormWindowState.Maximized
                            frmPrint.Show()

                        Else
                            ClearData()
                            Return True
                        End If
                    Else
                        Return False
                    End If
                End If

            ElseIf Global_IsUseMember = True Then ' Use Member
                If CheckForInternetConnection() = True Then
                    tmpdt = objWholeSaleReturnController.GetWholeSaleReturnByWSID(objWSInvoice.WholesaleInvoiceID)
                    If tmpdt.Rows.Count > 0 Then
                        MsgBox("This Voucher already have WholeSale Return. Can't Update!", MsgBoxStyle.OkCancel, AppName)
                    Else
                        If objWSaleInvoiceController.SaveWholeSaleInvoice(objWSInvoice, _dtWSInvoiceItem) Then
                            _WSInvoieID = objWSInvoice.WholesaleInvoiceID

                            'Can be use For Oppurnity Type Amount,Discount only
                            If OppurtunityType <> "Item" Then
                                If RedeemID <> "" And _IsUpdateHeader = False Then  'RedeemIsuse 1
                                    If CheckForInternetConnection() = True Then
                                        UpdateRedeem(objWSInvoice, 0)

                                    End If
                                Else

                                    If _IsUpdateHeader = False Then
                                        If CheckForInternetConnection() = True Then
                                            AddRedeem(objWSInvoice, 0)
                                        End If

                                    Else
                                        If CheckForInternetConnection() = True Then
                                            AddRedeem(objWSInvoice, 1)

                                        End If

                                    End If
                                End If
                            End If

                            If IsMaximumPoint() = True Then
                                If _IsUpdateHeader = False Then
                                    SaveSaleMemberCard(objWSInvoice, 0)
                                Else
                                    SaveSaleMemberCard(objWSInvoice, 1)
                                End If
                            End If

                            If (MsgBox("Do You Want To Save And Print WholeSale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                                Dim frmPrint As New frm_ReportViewer
                                Dim UserInfo As New UserInfo
                                Dim dt As New DataTable
                                dt = objWSaleInvoiceController.GetWholeSalePrint(_WSInvoieID)

                                UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _WSInvoieID)
                                dt.Rows(0).Item("PointBalance") = VoucherPointBalance
                                frmPrint.PrintWholeSale(dt)
                                ClearData()
                                frmPrint.WindowState = FormWindowState.Maximized
                                frmPrint.Show()

                            Else

                                UpdateRedeemAndTransID(MCode, Global_CompanyName, Token, _WSInvoieID)

                                ClearData()
                                Return True
                            End If
                        End If
                    End If
                Else 'Member သုံးပြီးလိုင်းမရဘူး Yes= လိုင်းမရပေမယ့်သိမ်းမယ်
                    tmpdt = objWholeSaleReturnController.GetWholeSaleReturnByWSID(objWSInvoice.WholesaleInvoiceID)
                    If tmpdt.Rows.Count > 0 Then
                        MsgBox("This Voucher already have WholeSale Return. Can't Update!", MsgBoxStyle.OkCancel, AppName)
                    Else
                        If objWSaleInvoiceController.SaveWholeSaleInvoice(objWSInvoice, _dtWSInvoiceItem) Then
                            _WSInvoieID = objWSInvoice.WholesaleInvoiceID
                            If (MsgBox("Do You Want To Save And Print WholeSale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                                Dim frmPrint As New frm_ReportViewer
                                Dim UserInfo As New UserInfo
                                Dim dt As New DataTable
                                dt = objWSaleInvoiceController.GetWholeSalePrint(_WSInvoieID)
                                frmPrint.PrintWholeSale(dt)
                                ClearData()
                                frmPrint.WindowState = FormWindowState.Maximized
                                frmPrint.Show()

                            Else
                                ClearData()
                                Return True
                            End If
                        Else
                            Return False
                        End If
                    End If
                End If
            End If
            End If
    End Function
    Private Sub CalculategrAlldTotalWeight()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim _TotalTK As Decimal = 0
        Dim _TotalTG As Decimal = 0
        Dim _TotalQTY As Integer = 0
        For j As Integer = 0 To grdItems.RowCount - 1
            If Not grdItems.Rows(j).IsNewRow Then
                'If CBool(grdItems.Rows(j).Cells("IsSaleReturn").Value) = False Then
                _TotalTG += CDec(IIf(grdItems.Rows(j).Cells("ItemTG").FormattedValue = "", 0, grdItems.Rows(j).Cells("ItemTG").FormattedValue))
                _TotalTK += CDec(IIf(grdItems.Rows(j).Cells("ItemTK").FormattedValue = "", 0, grdItems.Rows(j).Cells("ItemTK").FormattedValue))
                _TotalQTY += 1
                'End If
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
#Region " Private Methods "
    Public Sub FormatGrid()
        With grdItems
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9.25)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "WholesalesItemID"
                .DataPropertyName = "WholesalesItemID"
                .Name = "WholesalesItemID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcFroSaleID As New DataGridViewTextBoxColumn
            With dcFroSaleID
                .HeaderText = "ForSaleID"
                .DataPropertyName = "@ForSaleID"
                .Name = "@ForSaleID"
                .Width = 0
                .Visible = False
            End With
            .Columns.Add(dcFroSaleID)

            Dim dcItemNameID As New DataGridViewTextBoxColumn
            With dcItemNameID
                .HeaderText = "ItemNameID"
                .DataPropertyName = "ItemNameID"
                .Name = "ItemNameID"
                .Width = 0
                .Visible = False
            End With
            .Columns.Add(dcItemNameID)
            Dim dcGoldQualityID As New DataGridViewTextBoxColumn
            With dcGoldQualityID
                .HeaderText = "GoldQualityID"
                .DataPropertyName = "GoldQualityID"
                .Name = "GoldQualityID"
                .Width = 0
                .Visible = False
            End With
            .Columns.Add(dcGoldQualityID)

             Dim dcSNO As New DataGridViewTextBoxColumn
            With dcSNO
                .HeaderText = "No"
                .DataPropertyName = "SNo"
                .Name = "No"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 40
                .Visible = True
            End With
            .Columns.Add(dcSNO)

            Dim dcDesign As New DataGridViewTextBoxColumn
            With dcDesign
                .HeaderText = "Barcode No"
                .DataPropertyName = "BarcodeNo"
                .Name = "BarcodeNo"
                .Width = 110
                .Visible = True
            End With
            .Columns.Add(dcDesign)

            Dim dcBtn As New DataGridViewButtonColumn
            dcBtn.HeaderText = "..."
            dcBtn.Name = "SearchButton"
            dcBtn.Visible = True
            dcBtn.Width = 25
            dcBtn.Resizable = DataGridViewTriState.False
            dcBtn.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcBtn)

            Dim dcItemName As New DataGridViewTextBoxColumn()
            dcItemName.HeaderText = "ItemName"
            dcItemName.DataPropertyName = "ItemName"
            dcItemName.Name = "ItemName"
            dcItemName.Width = 150
            dcItemName.Visible = True
            dcItemName.DefaultCellStyle.Font = New Font("Myanmar3", 8.5)
            dcItemName.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcItemName)

            Dim dcGQ As New DataGridViewTextBoxColumn()
            dcGQ.HeaderText = "GoldQuality"
            dcGQ.DataPropertyName = "GoldQuality"
            dcGQ.Name = "GoldQuality"
            dcGQ.Width = 90
            dcGQ.Visible = True
            dcGQ.DefaultCellStyle.Font = New Font("Myanmar3", 8.5)
            dcGQ.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcGQ)

           
            Dim dcWeight As New DataGridViewTextBoxColumn()
            With dcWeight
                .HeaderText = "G"
                .DataPropertyName = "Gram"
                .Name = "Gram"
                .Width = 40
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "0.00"
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcWeight)

            Dim dcItemTK As New DataGridViewTextBoxColumn()
            With dcItemTK
                .HeaderText = "ItemTK"
                .DataPropertyName = "ItemTK"
                .Name = "ItemTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcItemTK)

            Dim dcItemTG As New DataGridViewTextBoxColumn()
            With dcItemTG
                .HeaderText = "ItemTG"
                .DataPropertyName = "ItemTG"
                .Name = "ItemTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcItemTG)

            Dim dcItemK As New DataGridViewTextBoxColumn()
            With dcItemK
                .HeaderText = "K"
                .DataPropertyName = "ItemK"
                .Name = "ItemK"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcItemK)

            Dim dcItemP As New DataGridViewTextBoxColumn()
            With dcItemP
                .HeaderText = "P"
                .DataPropertyName = "ItemP"
                .Name = "ItemP"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcItemP)

            Dim dcItemY As New DataGridViewTextBoxColumn()
            With dcItemY
                .HeaderText = "Y"
                .DataPropertyName = "ItemY"
                .Name = "ItemY"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcItemY)


            Dim dcWasteTK As New DataGridViewTextBoxColumn()
            With dcWasteTK
                .HeaderText = "WasteTK"
                .DataPropertyName = "WasteTK"
                .Name = "WasteTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcWasteTK)

            Dim dcWasteTG As New DataGridViewTextBoxColumn()
            With dcWasteTG
                .HeaderText = "WasteTG"
                .DataPropertyName = "WasteTG"
                .Name = "WasteTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcWasteTG)

            Dim dcWasteK As New DataGridViewTextBoxColumn()
            With dcWasteK
                .HeaderText = "K"
                .DataPropertyName = "WasteK"
                .Name = "WasteK"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcWasteK)

            Dim dcWasteP As New DataGridViewTextBoxColumn()
            With dcWasteP
                .HeaderText = "P"
                .DataPropertyName = "WasteP"
                .Name = "WasteP"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcWasteP)

            Dim dcWasteY As New DataGridViewTextBoxColumn()
            With dcWasteY
                .HeaderText = "Y"
                .DataPropertyName = "WasteY"
                .Name = "WasteY"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcWasteY)


            Dim dcGemsTK As New DataGridViewTextBoxColumn()
            With dcGemsTK
                .HeaderText = "GemsTK"
                .DataPropertyName = "GemsTK"
                .Name = "GemsTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGemsTK)

            Dim dcGemsTG As New DataGridViewTextBoxColumn()
            With dcGemsTG
                .HeaderText = "GemsTG"
                .DataPropertyName = "GemsTG"
                .Name = "GemsTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGemsTG)

            Dim dcGemsK As New DataGridViewTextBoxColumn()
            With dcGemsK
                .HeaderText = "K"
                .DataPropertyName = "GemsK"
                .Name = "GemsK"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcGemsK)

            Dim dcGemsP As New DataGridViewTextBoxColumn()
            With dcGemsP
                .HeaderText = "P"
                .DataPropertyName = "GemsP"
                .Name = "GemsP"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcGemsP)

            Dim dcGemsY As New DataGridViewTextBoxColumn()
            With dcGemsY
                .HeaderText = "Y"
                .DataPropertyName = "GemsY"
                .Name = "GemsY"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcGemsY)


            Dim dcGoldTK As New DataGridViewTextBoxColumn()
            With dcGoldTK
                .HeaderText = "GoldTK"
                .DataPropertyName = "GoldTK"
                .Name = "GoldTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGoldTK)

            Dim dcGoldTG As New DataGridViewTextBoxColumn()
            With dcGoldTG
                .HeaderText = "GoldTG"
                .DataPropertyName = "GoldTG"
                .Name = "GoldTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGoldTG)

            Dim dcGoldK As New DataGridViewTextBoxColumn()
            With dcGoldK
                .HeaderText = "K"
                .DataPropertyName = "GoldK"
                .Name = "GoldK"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcGoldK)

            Dim dcGoldP As New DataGridViewTextBoxColumn()
            With dcGoldP
                .HeaderText = "P"
                .DataPropertyName = "GoldP"
                .Name = "GoldP"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcGoldP)

            Dim dcGoldY As New DataGridViewTextBoxColumn()
            With dcGoldY
                .HeaderText = "Y"
                .DataPropertyName = "GoldY"
                .Name = "GoldY"
                .Visible = True
                .Width = 40
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcGoldY)


            Dim dcSaleRate As New DataGridViewTextBoxColumn()
            With dcSaleRate
                .HeaderText = "နှုန်း"
                .DataPropertyName = "Rate"
                .Name = "Rate"
                .Width = 60
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcSaleRate)

            Dim dcFixPrice As New DataGridViewTextBoxColumn()
            With dcFixPrice
                .HeaderText = "FixPrice"
                .DataPropertyName = "FixPrice"
                .Name = "FixPrice"
                .Visible = True
                .Width = 70
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcFixPrice)

            Dim dcDesignChargesRate As New DataGridViewTextBoxColumn()
            With dcDesignChargesRate
                .HeaderText = "လက်ခနှုန်း"
                .DataPropertyName = "DesignChargesRate"
                .Name = "DesignChargesRate"
                .Width = 60
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDesignChargesRate)

            Dim dcDesignCharges As New DataGridViewTextBoxColumn()
            With dcDesignCharges
                .HeaderText = "လက်ခ"
                .DataPropertyName = "DesignCharges"
                .Name = "DesignCharges"
                .Width = 60
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDesignCharges)

            Dim dcdisPercent As New DataGridViewTextBoxColumn()
            With dcdisPercent
                .HeaderText = "Discount (%)"
                .DataPropertyName = "DisPercent"
                .Name = "DisPercent"
                .Width = 60
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcdisPercent)

            Dim dcdisAmount As New DataGridViewTextBoxColumn()
            With dcdisAmount
                .HeaderText = "Discount Amount"
                .DataPropertyName = "disAmount"
                .Name = "disAmount"
                .Width = 100
                .Visible = True
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcdisAmount)

            Dim dcGemsPrice As New DataGridViewTextBoxColumn()
            With dcGemsPrice
                .HeaderText = "ကျောက်ဖိုး"
                .DataPropertyName = "GemsPrice"
                .Name = "GemsPrice"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGemsPrice)

            Dim dcAmt As New DataGridViewTextBoxColumn()
            With dcAmt
                .HeaderText = "ကျသင့်ငွေ"
                .DataPropertyName = "Amount"
                .Name = "Amount"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcAmt)

            Dim dcPay As New DataGridViewCheckBoxColumn
            With dcPay
                .HeaderText = "P"
                .DataPropertyName = "Pay"
                .Name = "Pay"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPay)

            Dim dcSale As New DataGridViewCheckBoxColumn
            With dcSale
                .HeaderText = "S"
                .DataPropertyName = "Sale"
                .Name = "Sale"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcSale)

        End With
    End Sub
    Public Sub ClearData()

        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        SearchWholeSale.Focus()
        WholeSaleItemInvoiceGenerateFormat()
        dtpWholeSaleInvoice.Value = Now


        'dtpWholeSaleInvoice.Value = Now
        'txtWSInvoiceID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.WholeSaleInvoice, EnumSetting.GenerateKeyType.WholeSaleInvoice.ToString, dtpWholeSaleInvoice.Value)
        _WSInvoieID = "0"
        numberformat = Global_DecimalFormat
        dtpDueDate.Value = Now.Date

        cboStaff.SelectedValue = -1
        cboStaff.Text = ""

        _CustomerID = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtAddress.Text = ""

        OrgNetAmt = 0
        txtTotalAmt.Text = "0"
        txtDiscountAmt.Text = "0"
        txtDiscountAmt.Enabled = False
        txtMemberDis.Text = "0"
        txtMemberDisAmt.Text = "0"
        txtValue.Text = 0
        txtPoint.Text = 0
        txtNetAmt.Text = "0"
        txtAddOrSubAmt.Text = ""
        txtPaidAmt.Text = ""
        txtRemark.Text = ""
        txtDisPercent.Text = "0"

        'optCash.Checked = True
        optConsignment.Checked = True

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
        VoucherPointBalance = 0

        Dim dc As New DataColumn
        _dtWSInvoiceItem = New DataTable

        _dtWSInvoiceItem.Columns.Add("SNo", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("WholeSaleInvoiceItemID", System.Type.GetType("System.String"))
        _dtWSInvoiceItem.Columns.Add("@ForSaleID", System.Type.GetType("System.String"))
        _dtWSInvoiceItem.Columns.Add("BarcodeNo", System.Type.GetType("System.String"))
        _dtWSInvoiceItem.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtWSInvoiceItem.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtWSInvoiceItem.Columns.Add("ItemNameID", System.Type.GetType("System.String"))
        _dtWSInvoiceItem.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))
        _dtWSInvoiceItem.Columns.Add("Gram", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("ItemTK", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("ItemTG", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("GoldTK", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("GoldTG", System.Type.GetType("System.Decimal"))


        _dtWSInvoiceItem.Columns.Add("WasteTK", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("WasteTG", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("ItemK", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("ItemP", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("ItemY", System.Type.GetType("System.Decimal"))

        _dtWSInvoiceItem.Columns.Add("WasteK", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("WasteP", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("WasteY", System.Type.GetType("System.Decimal"))

        _dtWSInvoiceItem.Columns.Add("GoldK", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("GoldP", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("GoldY", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("GemsK", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("GemsP", System.Type.GetType("System.Int16"))
        _dtWSInvoiceItem.Columns.Add("GemsY", System.Type.GetType("System.Decimal"))
        _dtWSInvoiceItem.Columns.Add("Rate", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("FixPrice", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("Amount", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("Pay", System.Type.GetType("System.Boolean"))
        _dtWSInvoiceItem.Columns.Add("Sale", System.Type.GetType("System.Boolean"))
        _dtWSInvoiceItem.Columns.Add("DesignCharges", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("DesignChargesRate", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("disPercent", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("disAmount", System.Type.GetType("System.Int64"))
        _dtWSInvoiceItem.Columns.Add("GemsPrice", System.Type.GetType("System.Int64"))

        grdItems.AutoGenerateColumns = False
        grdItems.ReadOnly = False
        grdItems.DataSource = _dtWSInvoiceItem

        TempTK = 0.0
        txtDesignCharges.Text = "0"
        FormatGrid()

    End Sub
    Private Sub WholeSaleItemInvoiceGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.WholeSaleStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtWSInvoiceID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpWholeSaleInvoice.Value)
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
        End If
    End Sub
    Public Function IsFillData() As Boolean

        If cboStaff.SelectedIndex = -1 Then
            MsgBox("Select Staff !", MsgBoxStyle.Information, AppName)
            cboStaff.Focus()
            Return False
        End If

        If txtCustomerCode.Text = "" Then
            MsgBox("Select Customer !", MsgBoxStyle.Information, AppName)
            txtCustomerCode.Focus()
            Return False
        End If

        If _dtWSInvoiceItem.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        Return True
    End Function
    Public Function GetData() As CommonInfo.WholeSaleInvoiceInfo
        Dim objWSaleInfo As New CommonInfo.WholeSaleInvoiceInfo
        With objWSaleInfo
            .WholesaleInvoiceID = _WSInvoieID
            .WDate = dtpWholeSaleInvoice.Value

            .StaffID = cboStaff.SelectedValue
            .CustomerID = _CustomerID

            .NetAmount = IIf(txtTotalAmt.Text = "", 0, txtTotalAmt.Text)
            .AddOrSub = IIf(txtAddOrSubAmt.Text = "", 0, txtAddOrSubAmt.Text)
            .Discount = IIf(txtDiscountAmt.Text = "", 0, txtDiscountAmt.Text)
            .PaidAmount = IIf(txtPaidAmt.Text = "", 0, txtPaidAmt.Text)
            .DueDate = dtpDueDate.Value
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)

            If (optCredit.Checked) Then
                .PayType = 0
            ElseIf (optConsignment.Checked) Then
                .PayType = 1
            ElseIf (optCash.Checked) Then
                .PayType = 2

            End If
            .DesignCharges = txtDesignCharges.Text
            .DisPercent = txtDisPercent.Text
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

                TopupPoint = CInt(CInt(PointConfiguration) * CInt(CInt(CInt(txtNetAmt.Text) / CInt(AmountConfiguration))))
                TopupValue = CInt(TopupPoint) * CInt(AmountConfiguration)

            End If


            .TopupPoint = TopupPoint
            .TopupValue = TopupValue


            .Token = Token
        End With
        Return objWSaleInfo
    End Function
    Private Sub GetComBo()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = objStaffController.GetStaffList().DefaultView
    End Sub
    Private Sub ShowBarcodeData(ByVal Barcodeobj As CommonInfo.SalesItemInfo)
        With Barcodeobj
            'txtGoodCode.Text = .GoodItemCode
            '_ItemGroupID = .ItemGroupID
            'Dim _ItemGroupInfo As CommonInfo.ItemGroupInfo
            '_ItemGroupInfo = objItemGroupCon.GetItemGroup(_ItemGroupID)
            'txtItemGroup.Text = _ItemGroupInfo.ItemGroupName

            '_GoldQualityID = .GoldQualityID
            'Dim _GoldQuaInfo As CommonInfo.GoldQualityInfo
            'Dim _CurrentPriceInfo As CommonInfo.CurrentPriceInfo
            '_GoldQuaInfo = objGoldQCon.GetGoldQuality(_GoldQualityID)
            'txtGoldQuality.Text = _GoldQuaInfo.GoldQuality

            '_CurrentPriceInfo = _CurrentController.GetCurrentPriceByGoldID(_GoldQualityID)
            'txtCurrentPrice.Text = _CurrentPriceInfo.SalesRate

            '_ColorCodeID = .ColorCodeID
            'Dim _ColorInfo As CommonInfo.ItemColorInfo
            '_ColorInfo = objColorCon.GetItemColorByID(_ColorCodeID)
            'txtColorCode.Text = _ColorInfo.ColorName


            'txtSize.Text = .ItemSize
            'txtGoldGram.Text = Format(.GoldTG, "0.000")
            'CalculateItemGoldAmount()


        End With

    End Sub
    Private Sub OptionCheckChange()
        If (optCredit.Checked) Or (optCash.Checked) Then
            If grdItems.Rows.Count <> -1 Then
                If _dtWSInvoiceItem.Rows.Count > 0 Then
                    For i As Integer = 0 To _dtWSInvoiceItem.Rows.Count - 1
                        _dtWSInvoiceItem.Rows(i).Item("Pay") = False
                        _dtWSInvoiceItem.Rows(i).Item("Sale") = True
                    Next
                End If
            End If
            If (Global_IsUseMember = True And txtMemberCode.Text <> "") Then
                btnRedeem.Enabled = True
                txtMemberDis.Enabled = True
                'txtMemberDisAmt.Text = "0.0"
                txtMemberDisAmt.Enabled = True
                GetMemberID(txtMemberCode.Text)
            End If
        ElseIf (optConsignment.Checked) Then
            If _dtWSInvoiceItem Is Nothing Then
            Else
                If grdItems.Rows.Count <> -1 Then
                    If _dtWSInvoiceItem.Rows.Count > 0 Then
                        For i As Integer = 0 To _dtWSInvoiceItem.Rows.Count - 1
                            _dtWSInvoiceItem.Rows(i).Item("Pay") = True
                            _dtWSInvoiceItem.Rows(i).Item("Sale") = False
                        Next
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ForOptConsignment()
        If optConsignment.Checked = True Then
            OrgNetAmt = 0
            txtDiscountAmt.Text = "0"
            txtNetAmt.Text = "0"
            txtAddOrSubAmt.Text = ""
            txtPaidAmt.Text = ""
            txtDisPercent.Text = ""
            btnRedeem.Enabled = False
            txtMemberDis.Enabled = False
            txtMemberDis.Text = "0.0"
            txtMemberDisAmt.Enabled = False
            txtValue.Enabled = False
            txtValue.Text = "0"
            txtPoint.Enabled = False
            txtPoint.Text = "0"
        Else
            CalculateGridTotalAmount()
            CalculategrAlldTotalWeight()
        End If
    End Sub
    Private Sub CalculateGridTotalAmount()
        Dim grdTotalAmt As Long = 0
        Dim grdDesignCharges As Long = 0
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If _dtWSInvoiceItem.Rows.Count <> -1 Then
            For i As Integer = 0 To grdItems.RowCount - 1
                grdTotalAmt += CLng(Val(grdItems.Rows(i).Cells("Amount").FormattedValue))
                grdDesignCharges += CLng(Val(grdItems.Rows(i).Cells("DesignCharges").FormattedValue))
            Next

            txtTotalAmt.Text = Format(Val(CLng(grdTotalAmt)), "###,##0.##")
            txtDesignCharges.Text = Format(Val(grdDesignCharges), "###,##0.##")

            txtNetAmt.Text = CStr(txtTotalAmt.Text)
            CalculateCommDisAmt()
            'CalculateDisAmt()
            'If (_CustType = EnumSetting.SaleType.Dealer.ToString()) Then
            '    If (grdTotalAmt > CLng(_DealerPayAmount)) Then
            '        MsgBox("Please Check Your Total Amount", MsgBoxStyle.Information, AppName)
            '        Exit Sub
            '    End If
            'End If
        End If
        _dtWSInvoiceItem = grdItems.DataSource
    End Sub
    Private Sub CalculateCommDisAmt()
        If txtValue.Text = "" Then txtValue.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If optConsignment.Checked = False Then
            If txtTotalAmt.Text <> "" Then

                txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtValue.Text) + CLng(txtMemberDisAmt.Text))), "###,##0.##")
            End If
        Else
            ForOptConsignment()
        End If

    End Sub
    Private Sub txtDesignCharges_TextChanged(sender As Object, e As EventArgs) Handles txtDesignCharges.TextChanged
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        'CalculateGridTotalAmount()
    End Sub
    'Private Sub CalculateDisAmt()
    '    If txtDiscountDis.Text = "" Then txtDiscountDis.Text = "0"
    '    If optConsignment.Checked = False Then
    '        If CDec(txtDiscountDis.Text) >= 0 Then
    '            txtDiscountAmt.Text = (CLng(txtTotalAmt.Text) * CDec(txtDiscountDis.Text)) / 100
    '            txtNetAmt.Text = CStr((CLng(txtTotalAmt.Text)) - (CLng(txtDiscountAmt.Text)))
    '        End If
    '    Else
    '        ForOptConsignment()
    '    End If
    'End Sub
    Private Function GetExistedItemscode(ByVal _CurrentRowIndex As Integer) As String
        GetExistedItemscode = ""
        For i As Integer = 0 To _dtWSInvoiceItem.Rows.Count - 1
            If _dtWSInvoiceItem.Rows(i).RowState <> DataRowState.Deleted And i <> _CurrentRowIndex Then
                GetExistedItemscode += "'" & _dtWSInvoiceItem.Rows(i).Item("BarcodeNo") & "',"
            End If
        Next
        Return GetExistedItemscode.Trim(",")
    End Function
    Private Sub ShowWholeSalesInvoiceData(ByVal objWSalesInvoice As WholeSaleInvoiceInfo)
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        Dim ItemCategory As New ItemCategoryInfo
        Dim GoldQuality As New GoldQualityInfo
        Dim objCust As New CustomerInfo
        Dim objCust1 As New CustomerInfo
        'from Stock
        With objWSalesInvoice
            txtWSInvoiceID.Text = .WholesaleInvoiceID
            dtpWholeSaleInvoice.Value = .WDate
            cboStaff.SelectedValue = .StaffID

            _CustomerID = .CustomerID
            objCust1 = objCustomerController.GetCustomerByID(_CustomerID)
            With objCust1
                txtCustomerCode.Text = .CustomerCode
                txtCustomerName.Text = .CustomerName
                '_DealerPayAmount = .CreditAmount
            End With

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

            If (.PayType = 0) Then
                optCredit.Checked = True
            ElseIf (.PayType = 1) Then
                optConsignment.Checked = True
            ElseIf (.PayType = 2) Then
                optCash.Checked = True
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


            txtTotalAmt.Text = Format(Val(.NetAmount), "###,##0.##")
            txtMemberDis.Text = .MemberDis
            txtMemberDisAmt.Text = .MemberDiscountAmt
            txtDiscountAmt.Text = Format(Val(.Discount), "###,##0.##")
            txtAddOrSubAmt.Text = Format(Val(.AddOrSub), "###,##0.##")
            txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text) + CLng(txtAddOrSubAmt.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtValue.Text) + CLng(txtMemberDisAmt.Text))), "###,##0.##")
            txtPaidAmt.Text = Format(.PaidAmount, "###,##0.##")
            txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
            txtRemark.Text = .Remark

            dtpDueDate.Value = .DueDate
            txtDesignCharges.Text = .DesignCharges
            txtDisPercent.Text = .DisPercent

        End With

    End Sub

#End Region

    Private Sub grdItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellContentClick
        If e.ColumnIndex <> 6 Then Exit Sub
        Dim objCusInfo As New CurrentPriceInfo
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim dtWSItem As New DataTable

        Dim objCurrent As New CommonInfo.IntDiamondPriceRateInfo
        Dim _Carat As Decimal = 0.0

        _IsUpdate = False
        dt = objSalesItemController.GetForSaleItemListForWholesales(GetExistedItems(e.RowIndex))
        DataItem = DirectCast(SearchData.FindFast(dt, "Transfer List"), DataRow)
        Dim _CurrentPriceInfo As CurrentPriceInfo


        If DataItem IsNot Nothing Then
            If grdItems.CurrentRow.IsNewRow Then
                _IsUpdate = True
                Dim dr As DataRow = _dtWSInvoiceItem.NewRow
                dr.Item("SNo") = e.RowIndex + 1
               
                dr.Item("@ForSaleID") = DataItem("@ForSaleID")
                dr.Item("BarcodeNo") = DataItem("ItemCode")
                dr.Item("ItemName") = DataItem("ItemName_")
                dr.Item("ItemNameID") = DataItem("ItemNameID")
                dr.Item("GoldQualityID") = DataItem("@GoldQualityID")
                _IsGram = _GoldQualityController.GetGoldQuality(DataItem("@GoldQualityID")).IsGramRate
                dr.Item("GoldQuality") = DataItem("GoldQuality_")
                _CurrentPriceInfo = objCurrentController.GetCurrentPriceByGoldID(DataItem("@GoldQualityID"))
                dr.Item("Rate") = _CurrentPriceInfo.SalesRate
                dr.Item("Gram") = Format(DataItem("@ItemTG"), "#0.000")
                dr.Item("ItemTG") = DataItem("@ItemTG")
                dr.Item("ItemTK") = DataItem("@ItemTK")
                dr.Item("GoldTG") = DataItem("GoldTG")
                dr.Item("GoldTK") = DataItem("GoldTK")
                dr.Item("WasteTG") = DataItem("WasteTG")
                dr.Item("WasteTK") = DataItem("WasteTK")
                dr.Item("GemsTG") = DataItem("GemsTG")
                dr.Item("GemsTK") = DataItem("GemsTK")
                If Not IsDBNull(dr.Item("Rate")) Then
                    _GoldTK = dr.Item("GoldTK")
                    _GoldTG = dr.Item("GoldTG")
                    _WasteTK = dr.Item("WasteTK")
                    _WasteTG = dr.Item("WasteTG")
                    dr.Item("GoldK") = DataItem("GoldK")
                    dr.Item("GoldP") = DataItem("GoldP")
                    If numberformat = 1 Then
                        dr.Item("GoldY") = Format(DataItem("GoldY"), "0.0")
                    Else
                        dr.Item("GoldY") = Format(DataItem("GoldY"), "0.00")
                    End If
                    dr.Item("WasteK") = DataItem("WasteK")
                    dr.Item("WasteP") = DataItem("WasteP")
                    If numberformat = 1 Then
                        dr.Item("WasteY") = Format(DataItem("WasteY"), "0.0")
                    Else
                        dr.Item("WasteY") = Format(DataItem("WasteY"), "0.00")
                    End If

                    'Gold+waste

                    Dim Gold As New CommonInfo.GoldWeightInfo
                    Dim _GoldPrice As Integer = 0
                    Dim _TotalGoldPrice As Integer = 0
                    Gold.WeightK = CInt(CInt(DataItem("GoldK")) + CInt(DataItem("WasteK")))
                    Gold.WeightP = CInt(DataItem("GoldP")) + CInt(DataItem("WasteP"))
                    Gold.WeightY = System.Decimal.Truncate(CDec(DataItem("GoldY")) + CDec(DataItem("WasteY")))
                    Gold.WeightC = (CDec(DataItem("GoldY")) + CDec(DataItem("WasteY"))) - Gold.WeightY
                    Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
                    TempTK = Gold.GoldTK
                    'Gem
                    _GemsTK = dr.Item("GemsTK")
                    _GemsTG = dr.Item("GemsTG")
                    Dim GemWeight As New CommonInfo.GoldWeightInfo
                    GemWeight.GoldTK = _GemsTK
                    GemWeight = objConverterController.ConvertGoldTKToKPYC(GemWeight)
                    dr.Item("GemsK") = CStr(GemWeight.WeightK)
                    dr.Item("GemsP") = CStr(GemWeight.WeightP)
                    If numberformat = 1 Then
                        dr.Item("GemsY") = Format(GemWeight.WeightY + GemWeight.WeightC, "0.0")
                    Else
                        dr.Item("GemsY") = Format(GemWeight.WeightY + GemWeight.WeightC, "0.00")
                    End If

                    'Item
                    _ItemTK = dr.Item("ItemTK")
                    _ItemTG = dr.Item("ItemTG")
                    Dim ItemWeight As New CommonInfo.GoldWeightInfo
                    ItemWeight.GoldTK = _ItemTK
                    ItemWeight = objConverterController.ConvertGoldTKToKPYC(ItemWeight)
                    dr.Item("ItemK") = CStr(ItemWeight.WeightK)
                    dr.Item("ItemP") = CStr(ItemWeight.WeightP)
                    If numberformat = 1 Then
                        dr.Item("ItemY") = Format(ItemWeight.WeightY + ItemWeight.WeightC, "0.0")
                    Else
                        dr.Item("ItemY") = Format(ItemWeight.WeightY + ItemWeight.WeightC, "0.00")
                    End If

                End If
                ' GemsPrice
                Dim amt As Long = 0
                Dim GemsPrice As Long = 0
                dtWSItem = _SalesItemController.GetSalesItemGems(DataItem("@ForSaleID"))
                If dtWSItem.Rows.Count > 0 Then
                    For k As Integer = 0 To dtWSItem.Rows.Count - 1
                        If dtWSItem.Rows(k).Item("SaleByDefinePrice") = True Then
                            _Carat = Format(CDec(dtWSItem.Rows(k).Item("GemsTG") * Global_GramToKarat), 0.0)
                            objCurrent = _CurrentCon.GetIntDiamondData(_Carat)
                            With objCurrent
                                'dtWSInvoiceItem.Rows(k).Item("UnitPrice") = .PriceRate
                                amt = CLng(_Carat * .WholeSaleRate)
                                If dtWSItem.Rows(k).Item("Type") = "Fix" Then
                                    GemsPrice += .WholeSaleRate
                                ElseIf dtWSItem.Rows(k).Item("Type") = "ByQty" Then

                                    GemsPrice += (.WholeSaleRate * dtWSItem.Rows(k).Item("Qty")) * ((.WholeSaleRate * dtWSItem.Rows(k).Item("Qty")) * (dtWSItem.Rows(k).Item("GemTaxPer")) / 100)
                                Else
                                    GemsPrice += CLng(amt + (amt * (dtWSItem.Rows(k).Item("GemTaxPer")) / 100))
                                End If

                            End With
                        Else
                            GemsPrice += dtWSItem.Rows(k).Item("Amount")
                        End If

                    Next
                End If

                dr.Item("GemsPrice") = GemsPrice
                dr.Item("FixPrice") = DataItem("FixPrice")
                If (dr.Item("FixPrice")) > 0 Then
                    dr.Item("Amount") = DataItem("FixPrice")
                Else
                    If _IsGram = False Then
                        'Calculate Price
                        dr.Item("Amount") = (TempTK * (dr.Item("Rate"))) + GemsPrice
                    Else
                        dr.Item("Amount") = ((dr.Item("GoldTG")) + (dr.Item("WasteTG"))) * (dr.Item("Rate")) + GemsPrice
                    End If
                End If
                If (optCredit.Checked) Then
                    dr.Item("Pay") = False
                    dr.Item("Sale") = True
                ElseIf (optConsignment.Checked) Then
                    dr.Item("Pay") = True
                    dr.Item("Sale") = False
                ElseIf (optCash.Checked) Then
                    dr.Item("Pay") = False
                    dr.Item("Sale") = True
                End If
                dr.Item("DesignCharges") = DataItem("DesignCharges")
                dr.Item("DesignChargesRate") = DataItem("DesignChargesRate")
                dr.Item("disPercent") = DataItem("disPercent")
                dr.Item("disAmount") = DataItem("disAmount")
                _dtWSInvoiceItem.Rows.Add(dr)
                grdItems.DataSource = _dtWSInvoiceItem
            Else
                With grdItems
                    grdItems.DataSource = _dtWSInvoiceItem
                    Dim dr As DataRow = _dtWSInvoiceItem.NewRow
                    .CurrentRow.Cells("No").Value = e.RowIndex + 1
                    .CurrentRow.Cells("@ForSaleID").Value = DataItem("@ForSaleID")
                    .CurrentRow.Cells("BarcodeNo").Value = DataItem("ItemCode")
                    _IsGram = _GoldQualityController.GetGoldQuality(DataItem("@GoldQualityID")).IsGramRate
                    _CurrentPriceInfo = objCurrentController.GetCurrentPriceByGoldID(DataItem("@GoldQualityID"))
                    .CurrentRow.Cells("Rate").Value = _CurrentPriceInfo.SalesRate
                    .CurrentRow.Cells("Gram").Value = Format(DataItem("@ItemTG"), "#0.000")
                    .CurrentRow.Cells("ItemName").Value = DataItem("ItemName_")
                    .CurrentRow.Cells("GoldQuality").Value = _GoldQualityController.GetGoldQuality(DataItem("@GoldQualityID")).GoldQuality
                    .CurrentRow.Cells("ItemNameID").Value = DataItem("ItemNameID")
                    .CurrentRow.Cells("GoldQualityID").Value = DataItem("@GoldQualityID")

                    .CurrentRow.Cells("ItemTG").Value = DataItem("@ItemTG")
                    .CurrentRow.Cells("ItemTK").Value = DataItem("@ItemTK")
                    .CurrentRow.Cells("GoldTG").Value = DataItem("GoldTG")
                    .CurrentRow.Cells("GoldTK").Value = DataItem("GoldTK")
                    .CurrentRow.Cells("WasteTG").Value = DataItem("WasteTG")
                    .CurrentRow.Cells("WasteTK").Value = DataItem("WasteTK")
                    .CurrentRow.Cells("GemsTG").Value = DataItem("GemsTG")
                    .CurrentRow.Cells("GemsTK").Value = DataItem("GemsTK")
                    .CurrentRow.Cells("FixPrice").Value = DataItem("FixPrice")
                    .CurrentRow.Cells("Rate").Value = _CurrentPriceInfo.SalesRate
                    If Not IsDBNull(.CurrentRow.Cells("Rate").Value) Then
                        _GoldTK = .CurrentRow.Cells("GoldTK").Value
                        _GoldTG = .CurrentRow.Cells("GoldTG").Value
                        _WasteTK = .CurrentRow.Cells("WasteTK").Value
                        _WasteTG = .CurrentRow.Cells("WasteTG").Value
                        .CurrentRow.Cells("GoldK").Value = DataItem("GoldK")
                        .CurrentRow.Cells("GoldP").Value = DataItem("GoldP")
                        If numberformat = 1 Then
                            .CurrentRow.Cells("GoldY").Value = Format(DataItem("GoldY"), "0.0")
                        Else
                            .CurrentRow.Cells("GoldY").Value = Format(DataItem("GoldY"), "0.00")
                        End If

                        .CurrentRow.Cells("WasteK").Value = DataItem("WasteK")
                        .CurrentRow.Cells("WasteP").Value = DataItem("WasteP")
                        If numberformat = 1 Then
                            .CurrentRow.Cells("WasteY").Value = Format(DataItem("WasteY"), "0.0")
                        Else
                            .CurrentRow.Cells("WasteY").Value = Format(DataItem("WasteY"), "0.00")
                        End If

                        'Gold+waste

                        Dim Gold As New CommonInfo.GoldWeightInfo
                        Dim _GoldPrice As Integer = 0
                        Dim _TotalGoldPrice As Integer = 0
                        Gold.WeightK = CInt(.CurrentRow.Cells("GoldK").Value) + CInt(.CurrentRow.Cells("WasteK").Value)
                        Gold.WeightP = CInt(.CurrentRow.Cells("GoldP").Value) + CInt(.CurrentRow.Cells("WasteP").Value)
                        Gold.WeightY = System.Decimal.Truncate(CDec(.CurrentRow.Cells("GoldY").Value) + CDec(.CurrentRow.Cells("WasteY").Value))
                        Gold.WeightC = (CDec(.CurrentRow.Cells("GoldY").Value) + CDec(.CurrentRow.Cells("WasteY").Value)) - Gold.WeightY
                        Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
                        TempTK = Gold.GoldTK
                        'Gem
                        _GemsTK = .CurrentRow.Cells("GemsTK").Value
                        _GemsTG = .CurrentRow.Cells("GemsTG").Value
                        Dim GemWeight As New CommonInfo.GoldWeightInfo
                        GemWeight.GoldTK = _GemsTK
                        GemWeight = objConverterController.ConvertGoldTKToKPYC(GemWeight)
                        .CurrentRow.Cells("GemsK").Value = CStr(GemWeight.WeightK)
                        .CurrentRow.Cells("GemsP").Value = CStr(GemWeight.WeightP)
                        If numberformat = 1 Then
                            .CurrentRow.Cells("GemsY").Value = Format(GemWeight.WeightY + GemWeight.WeightC, "0.0")
                        Else
                            .CurrentRow.Cells("GemsY").Value = Format(GemWeight.WeightY + GemWeight.WeightC, "0.00")
                        End If

                        'Item
                        _ItemTK = .CurrentRow.Cells("ItemTK").Value
                        _ItemTG = .CurrentRow.Cells("ItemTG").Value
                        Dim ItemWeight As New CommonInfo.GoldWeightInfo
                        ItemWeight.GoldTK = _ItemTK
                        ItemWeight = objConverterController.ConvertGoldTKToKPYC(ItemWeight)
                        .CurrentRow.Cells("ItemK").Value = CStr(ItemWeight.WeightK)
                        .CurrentRow.Cells("ItemP").Value = CStr(ItemWeight.WeightP)
                        If numberformat = 1 Then
                            .CurrentRow.Cells("ItemY").Value = Format(ItemWeight.WeightY + ItemWeight.WeightC, "0.0")
                        Else
                            .CurrentRow.Cells("ItemY").Value = Format(ItemWeight.WeightY + ItemWeight.WeightC, "0.00")
                        End If

                    End If

                    ' GemsPrice
                    Dim amt As Long = 0
                    Dim GemsPrice As Long = 0
                    dtWSItem = _SalesItemController.GetSalesItemGems(DataItem("@ForSaleID"))
                    If dtWSItem.Rows.Count > 0 Then
                        For k As Integer = 0 To dtWSItem.Rows.Count - 1
                            If dtWSItem.Rows(k).Item("SaleByDefinePrice") = True Then
                                _Carat = Format(CDec(dtWSItem.Rows(k).Item("GemsTG") * Global_GramToKarat), 0.0)
                                objCurrent = _CurrentCon.GetIntDiamondData(_Carat)
                                With objCurrent
                                    'dtWSInvoiceItem.Rows(k).Item("UnitPrice") = .PriceRate
                                    amt = CLng(_Carat * .WholeSaleRate)
                                    If dtWSItem.Rows(k).Item("Type") = "Fix" Then
                                        GemsPrice += .WholeSaleRate
                                    ElseIf dtWSItem.Rows(k).Item("Type") = "ByQty" Then

                                        GemsPrice += (.WholeSaleRate * dtWSItem.Rows(k).Item("Qty")) * ((.WholeSaleRate * dtWSItem.Rows(k).Item("Qty")) * (dtWSItem.Rows(k).Item("GemTaxPer")) / 100)
                                    Else
                                        GemsPrice += CLng(amt + (amt * (dtWSItem.Rows(k).Item("GemTaxPer")) / 100))
                                    End If

                                End With
                            Else
                                GemsPrice += dtWSItem.Rows(k).Item("Amount")
                            End If

                        Next
                    End If

                    .CurrentRow.Cells("GemsPrice").Value = GemsPrice

                    If (.CurrentRow.Cells("FixPrice").Value) > 0 Then
                        .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value
                    Else
                        If _IsGram = False Then
                            'Calculate Price
                            .CurrentRow.Cells("Amount").Value = (TempTK * (.CurrentRow.Cells("Rate").Value)) + GemsPrice
                        Else
                            .CurrentRow.Cells("Amount").Value = ((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * (.CurrentRow.Cells("Rate").Value) + GemsPrice
                        End If
                    End If
                    If (optCredit.Checked) Then
                        .CurrentRow.Cells("Pay").Value = False
                        .CurrentRow.Cells("Sale").Value = True
                    ElseIf (optConsignment.Checked) Then
                        .CurrentRow.Cells("Pay").Value = True
                        .CurrentRow.Cells("Sale").Value = False
                    ElseIf (optCash.Checked) Then
                        .CurrentRow.Cells("Pay").Value = False
                        .CurrentRow.Cells("Sale").Value = True
                    End If
                    .CurrentRow.Cells("DesignCharges").Value = DataItem("DesignCharges")
                    .CurrentRow.Cells("DesignChargesRate").Value = DataItem("DesignChargesRate")
                    .CurrentRow.Cells("disPercent").Value = DataItem("disPercent")
                    .CurrentRow.Cells("disAmount").Value = DataItem("disAmount")
                End With
            End If
            CalculateGridTotalAmount()
            CalculategrAlldTotalWeight()
        End If
    End Sub

    Private Function GetExistedItems(ByVal _CurrentRowIndex As Integer) As String
        GetExistedItems = ""
        For i As Integer = 0 To _dtWSInvoiceItem.Rows.Count - 1
            If _dtWSInvoiceItem.Rows(i).RowState <> DataRowState.Deleted And i <> _CurrentRowIndex Then
                GetExistedItems += "'" & _dtWSInvoiceItem.Rows(i).Item("@ForSaleID") & "',"
            End If
        Next
        Return GetExistedItems.Trim(",")
    End Function


    Private Sub grdItems_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellValidated
        Dim _BarCode As String
        Dim dt As New DataTable


        Dim _CurrentPriceInfo As CommonInfo.CurrentPriceInfo

        Dim dtWSItem As New DataTable
        Dim objCurrent As New CommonInfo.IntDiamondPriceRateInfo
        Dim _Carat As Decimal = 0.0

        If grdItems.IsCurrentCellInEditMode = False Then Exit Sub
        If e.RowIndex <> -1 Then
            If grdItems.Columns(e.ColumnIndex).Name = "BarcodeNo" Then
                If txtCustomerCode.Text = "" Then
                    MsgBox("Please Choose Customer Name First", MsgBoxStyle.Information, AppName)
                    Exit Sub
                End If

                If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("BarcodeNo").Value) Then

                    If (optCredit.Checked) Or (optConsignment.Checked) Or (optCash.Checked) Then

                        Dim objSalesItemInfo As SalesItemInfo

                        If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("BarcodeNo").Value) Then
                            _BarCode = CStr(grdItems.CurrentRow.Cells("BarcodeNo").Value)

                            objSalesItemInfo = objSalesItemController.GetForSaleInfoByWSItemCode(_BarCode, GetExistedItemscode(e.RowIndex))
                            If objSalesItemInfo.ForSaleID <> "" Then
                                If (objSalesItemInfo.ItemCode = "") Then
                                    MsgBox("Invalid Barcode No !", MsgBoxStyle.Information, AppName)
                                    grdItems.CurrentRow.Cells("BarcodeNo").Value = ""
                                    _IsUpdate = True
                                    Dim dr As DataRow = _dtWSInvoiceItem.NewRow
                                    dr.Item("SNo") = e.RowIndex + 1
                                Else
                                    With grdItems
                                        _IsUpdate = False
                                        Dim dr As DataRow = _dtWSInvoiceItem.NewRow
                                        .CurrentRow.Cells("No").Value = e.RowIndex + 1
                                        .CurrentRow.Cells("@ForSaleID").Value = objSalesItemInfo.ForSaleID
                                        .CurrentRow.Cells("BarcodeNo").Value = objSalesItemInfo.ItemCode
                                        _IsGram = _GoldQualityController.GetGoldQuality(objSalesItemInfo.GoldQualityID).IsGramRate
                                        _CurrentPriceInfo = objCurrentController.GetCurrentPriceByGoldID(objSalesItemInfo.GoldQualityID)
                                        .CurrentRow.Cells("Rate").Value = _CurrentPriceInfo.SalesRate
                                        .CurrentRow.Cells("Gram").Value = Format(objSalesItemInfo.ItemTG, "#0.000")
                                        .CurrentRow.Cells("ItemName").Value = objSalesItemInfo.ItemName
                                        .CurrentRow.Cells("GoldQuality").Value = _GoldQualityController.GetGoldQuality(objSalesItemInfo.GoldQualityID).GoldQuality
                                        .CurrentRow.Cells("ItemNameID").Value = objSalesItemInfo.ItemNameID
                                        .CurrentRow.Cells("GoldQualityID").Value = objSalesItemInfo.GoldQualityID

                                        .CurrentRow.Cells("ItemTG").Value = objSalesItemInfo.ItemTG
                                        .CurrentRow.Cells("ItemTK").Value = objSalesItemInfo.ItemTK
                                        .CurrentRow.Cells("GoldTG").Value = objSalesItemInfo.GoldTG
                                        .CurrentRow.Cells("GoldTK").Value = objSalesItemInfo.GoldTK
                                        .CurrentRow.Cells("WasteTG").Value = objSalesItemInfo.WasteTG
                                        .CurrentRow.Cells("WasteTK").Value = objSalesItemInfo.WasteTK
                                        .CurrentRow.Cells("GemsTG").Value = objSalesItemInfo.GemsTG
                                        .CurrentRow.Cells("GemsTK").Value = objSalesItemInfo.GemsTK
                                        .CurrentRow.Cells("FixPrice").Value = CInt(objSalesItemInfo.FixPrice)
                                        .CurrentRow.Cells("Rate").Value = _CurrentPriceInfo.SalesRate
                                        .CurrentRow.Cells("DesignCharges").Value = IIf(IsDBNull(objSalesItemInfo.DesignCharges), 0, objSalesItemInfo.DesignCharges)
                                        .CurrentRow.Cells("DesignChargesRate").Value = IIf(IsDBNull(objSalesItemInfo.DesignCharges), 0, objSalesItemInfo.DesignCharges)
                                        .CurrentRow.Cells("DisPercent").Value = 0
                                        .CurrentRow.Cells("DisAmount").Value = 0
                                        If Not IsDBNull(.CurrentRow.Cells("Rate").Value) Then
                                            _GoldTK = .CurrentRow.Cells("GoldTK").Value
                                            _GoldTG = .CurrentRow.Cells("GoldTG").Value
                                            _WasteTK = .CurrentRow.Cells("WasteTK").Value
                                            _WasteTG = .CurrentRow.Cells("WasteTG").Value
                                            .CurrentRow.Cells("GoldK").Value = objSalesItemInfo.GoldK
                                            .CurrentRow.Cells("GoldP").Value = objSalesItemInfo.GoldP
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("GoldY").Value = Format(objSalesItemInfo.GoldY, "0.0")
                                            Else
                                                .CurrentRow.Cells("GoldY").Value = Format(objSalesItemInfo.GoldY, "0.00")
                                            End If

                                            .CurrentRow.Cells("WasteK").Value = objSalesItemInfo.WasteK
                                            .CurrentRow.Cells("WasteP").Value = objSalesItemInfo.WasteP
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("WasteY").Value = Format(objSalesItemInfo.WasteY, "0.0")
                                            Else
                                                .CurrentRow.Cells("WasteY").Value = Format(objSalesItemInfo.WasteY, "0.00")
                                            End If

                                            'Gold+waste

                                            Dim Gold As New CommonInfo.GoldWeightInfo
                                            Dim _GoldPrice As Integer = 0
                                            Dim _TotalGoldPrice As Integer = 0
                                            Gold.WeightK = CInt(.CurrentRow.Cells("GoldK").Value) + CInt(.CurrentRow.Cells("WasteK").Value)
                                            Gold.WeightP = CInt(.CurrentRow.Cells("GoldP").Value) + CInt(.CurrentRow.Cells("WasteP").Value)
                                            Gold.WeightY = System.Decimal.Truncate(CDec(.CurrentRow.Cells("GoldY").Value) + CDec(.CurrentRow.Cells("WasteY").Value))
                                            Gold.WeightC = (CDec(.CurrentRow.Cells("GoldY").Value) + CDec(.CurrentRow.Cells("WasteY").Value)) - Gold.WeightY
                                            Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
                                            TempTK = Gold.GoldTK
                                            'Gem
                                            _GemsTK = .CurrentRow.Cells("GemsTK").Value
                                            _GemsTG = .CurrentRow.Cells("GemsTG").Value
                                            Dim GemWeight As New CommonInfo.GoldWeightInfo
                                            GemWeight.GoldTK = _GemsTK
                                            GemWeight = objConverterController.ConvertGoldTKToKPYC(GemWeight)
                                            .CurrentRow.Cells("GemsK").Value = CStr(GemWeight.WeightK)
                                            .CurrentRow.Cells("GemsP").Value = CStr(GemWeight.WeightP)
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("GemsY").Value = Format(GemWeight.WeightY + GemWeight.WeightC, "0.0")
                                            Else
                                                .CurrentRow.Cells("GemsY").Value = Format(GemWeight.WeightY + GemWeight.WeightC, "0.00")
                                            End If

                                            'Item
                                            _ItemTK = .CurrentRow.Cells("ItemTK").Value
                                            _ItemTG = .CurrentRow.Cells("ItemTG").Value
                                            Dim ItemWeight As New CommonInfo.GoldWeightInfo
                                            ItemWeight.GoldTK = _ItemTK
                                            ItemWeight = objConverterController.ConvertGoldTKToKPYC(ItemWeight)
                                            .CurrentRow.Cells("ItemK").Value = CStr(ItemWeight.WeightK)
                                            .CurrentRow.Cells("ItemP").Value = CStr(ItemWeight.WeightP)
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("ItemY").Value = Format(ItemWeight.WeightY + ItemWeight.WeightC, "0.0")
                                            Else
                                                .CurrentRow.Cells("ItemY").Value = Format(ItemWeight.WeightY + ItemWeight.WeightC, "0.00")
                                            End If

                                        End If

                                        ' GemsPrice
                                        Dim amt As Long = 0
                                        Dim GemsPrice As Long = 0
                                        dtWSItem = _SalesItemController.GetSalesItemGems(objSalesItemInfo.ForSaleID)
                                        If dtWSItem.Rows.Count > 0 Then
                                            For k As Integer = 0 To dtWSItem.Rows.Count - 1
                                                If dtWSItem.Rows(k).Item("SaleByDefinePrice") = True Then
                                                    _Carat = Format(CDec(dtWSItem.Rows(k).Item("GemsTG") * Global_GramToKarat), 0.0)
                                                    objCurrent = _CurrentCon.GetIntDiamondData(_Carat)
                                                    With objCurrent
                                                        'dtWSInvoiceItem.Rows(k).Item("UnitPrice") = .PriceRate
                                                        amt = CLng(_Carat * .WholeSaleRate)
                                                        If dtWSItem.Rows(k).Item("Type") = "Fix" Then
                                                            GemsPrice += .WholeSaleRate
                                                        ElseIf dtWSItem.Rows(k).Item("Type") = "ByQty" Then

                                                            GemsPrice += (.WholeSaleRate * dtWSItem.Rows(k).Item("Qty")) * ((.WholeSaleRate * dtWSItem.Rows(k).Item("Qty")) * (dtWSItem.Rows(k).Item("GemTaxPer")) / 100)
                                                        Else
                                                            GemsPrice += CLng(amt + (amt * (dtWSItem.Rows(k).Item("GemTaxPer")) / 100))
                                                        End If

                                                    End With
                                                Else
                                                    GemsPrice += dtWSItem.Rows(k).Item("Amount")
                                                End If

                                            Next
                                        End If

                                        .CurrentRow.Cells("GemsPrice").Value = GemsPrice

                                        If (.CurrentRow.Cells("DisPercent").Value) > 0 Then
                                            If (.CurrentRow.Cells("FixPrice").Value) > 0 Then
                                                If _IsGram = True Then
                                                    .CurrentRow.Cells("DisAmount").Value = (.CurrentRow.Cells("FixPrice").Value + IIf(IsDBNull(.CurrentRow.Cells("DesignCharges").Value), 0, .CurrentRow.Cells("DesignCharges").Value)) * (.CurrentRow.Cells("DisPercent").Value / 100)
                                                    .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value + .CurrentRow.Cells("GemsPrice") - .CurrentRow.Cells("DisAmount").Value
                                                Else
                                                    .CurrentRow.Cells("DisAmount").Value = (.CurrentRow.Cells("FixPrice").Value) * (.CurrentRow.Cells("disPercent").Value / 100)
                                                    .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value - .CurrentRow.Cells("DisAmount").Value
                                                End If
                                            Else
                                                If _IsGram = False Then
                                                    'Calculate Price
                                                    .CurrentRow.Cells("DisAmount").Value = ((TempTK * (.CurrentRow.Cells("Rate").Value)) + IIf(IsDBNull(.CurrentRow.Cells("DesignCharges").Value), 0, .CurrentRow.Cells("DesignCharges").Value)) * (.CurrentRow.Cells("DisPercent").Value / 100)
                                                    .CurrentRow.Cells("Amount").Value = (TempTK * (.CurrentRow.Cells("Rate").Value)) + .CurrentRow.Cells("DesignCharges").Value + .CurrentRow.Cells("GemsPrice") - .CurrentRow.Cells("DisAmount").Value
                                                Else
                                                    .CurrentRow.Cells("DesignCharges").Value = ((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * IIf(IsDBNull(.CurrentRow.Cells("DesignChargesRate").Value), 0, .CurrentRow.Cells("DesignChargesRate").Value)
                                                    .CurrentRow.Cells("DisAmount").Value = (((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * (.CurrentRow.Cells("Rate").Value) + IIf(IsDBNull(.CurrentRow.Cells("DesignCharges").Value), 0, .CurrentRow.Cells("DesignCharges").Value)) * (.CurrentRow.Cells("DisPercent").Value / 100)
                                                    .CurrentRow.Cells("Amount").Value = ((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * (.CurrentRow.Cells("Rate").Value) + .CurrentRow.Cells("DesignCharges").Value + .CurrentRow.Cells("GemsPrice") - .CurrentRow.Cells("DisAmount").Value
                                                End If
                                            End If
                                        End If

                                        If (.CurrentRow.Cells("FixPrice").Value) > 0 Then
                                            If _IsGram = True Then
                                                .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value + IIf(IsDBNull(.CurrentRow.Cells("DesignCharges").Value), 0, .CurrentRow.Cells("DesignCharges").Value) - .CurrentRow.Cells("disAmount").Value
                                            Else
                                                .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value - .CurrentRow.Cells("disAmount").Value
                                            End If
                                        Else
                                            If _IsGram = False Then
                                                'Calculate Price
                                                .CurrentRow.Cells("Amount").Value = (TempTK * (.CurrentRow.Cells("Rate").Value)) + .CurrentRow.Cells("GemsPrice").Value + IIf(IsDBNull(.CurrentRow.Cells("DesignCharges").Value), 0, .CurrentRow.Cells("DesignCharges").Value) - .CurrentRow.Cells("disAmount").Value
                                            Else
                                                .CurrentRow.Cells("DesignCharges").Value = ((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * IIf(IsDBNull(.CurrentRow.Cells("DesignChargesRate").Value), 0, .CurrentRow.Cells("DesignChargesRate").Value)
                                                .CurrentRow.Cells("Amount").Value = ((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * (.CurrentRow.Cells("Rate").Value) + IIf(IsDBNull(.CurrentRow.Cells("DesignCharges").Value), 0, .CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(.CurrentRow.Cells("GemsPrice").Value), 0, .CurrentRow.Cells("GemsPrice").Value) - .CurrentRow.Cells("disAmount").Value
                                            End If
                                        End If
                                        If (optCredit.Checked) Then
                                            .CurrentRow.Cells("Pay").Value = False
                                            .CurrentRow.Cells("Sale").Value = True
                                        ElseIf (optConsignment.Checked) Then
                                            .CurrentRow.Cells("Pay").Value = True
                                            .CurrentRow.Cells("Sale").Value = False
                                        ElseIf (optCash.Checked) Then
                                            .CurrentRow.Cells("Pay").Value = False
                                            .CurrentRow.Cells("Sale").Value = True
                                        End If

                                        Dim i As Integer = 1
                                        For Each drNo As DataRow In _dtWSInvoiceItem.Rows
                                            If drNo.RowState <> DataRowState.Deleted Then
                                                drNo.Item("SNo") = i
                                                i = i + 1
                                            End If
                                        Next

                                    End With


                                End If
                            End If

                        End If
                    Else
                        MsgBox("Please choose payment type!", MsgBoxStyle.Information, "Data Require!")
                        grdItems.CurrentRow.Cells("BarcodeNo").Value = ""
                    End If
               

                End If
            ElseIf grdItems.Columns(e.ColumnIndex).Name = "Rate" Then
                If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("Rate").Value) Then
                    _IsGram = _GoldQualityController.GetGoldQuality(grdItems.CurrentRow.Cells("GoldQualityID").Value).IsGramRate
                    If (grdItems.CurrentRow.Cells("FixPrice").Value) > 0 Then
                        grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value
                    Else
                        If _IsGram = False Then

                            Dim Gold As New CommonInfo.GoldWeightInfo
                            Dim _GoldPrice As Integer = 0
                            Dim _TotalGoldPrice As Integer = 0
                            Gold.WeightK = CInt(CInt(grdItems.CurrentRow.Cells("GoldK").Value) + CInt(grdItems.CurrentRow.Cells("WasteK").Value))
                            Gold.WeightP = CInt(grdItems.CurrentRow.Cells("GoldP").Value) + CInt(grdItems.CurrentRow.Cells("WasteP").Value)
                            Gold.WeightY = System.Decimal.Truncate(CDec(grdItems.CurrentRow.Cells("GoldY").Value) + CDec(grdItems.CurrentRow.Cells("WasteY").Value))
                            Gold.WeightC = (CDec(grdItems.CurrentRow.Cells("GoldY").Value) + CDec(grdItems.CurrentRow.Cells("WasteY").Value)) - Gold.WeightY
                            Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
                            TempTK = Gold.GoldTK

                            'Calculate Price
                            grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + grdItems.CurrentRow.Cells("DesignCharges").Value + grdItems.CurrentRow.Cells("GemsPrice").Value
                        Else
                            'grdItems.CurrentRow.Cells("Amount").Value = ((((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * grdItems.CurrentRow.Cells("Rate").Value))
                            grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                        End If
                    End If
                End If
                
            ElseIf grdItems.Columns(e.ColumnIndex).Name = "FixPrice" Then
                If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("FixPrice").Value) Then
                    If (grdItems.CurrentRow.Cells("FixPrice").Value) > 0 Then
                        'grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value
                        grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                        grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                    Else
                        If _IsGram = False Then
                            'Calculate Price
                            'grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + grdItems.CurrentRow.Cells("DesignCharges").Value
                            grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value) * (grdItems.CurrentRow.Cells("disPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                        Else
                            'grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value
                            grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                        End If
                    End If

                End If

            ElseIf grdItems.Columns(e.ColumnIndex).Name = "DesignChargesRate" Then
                _IsGram = _GoldQualityController.GetGoldQuality(grdItems.CurrentRow.Cells("GoldQualityID").Value).IsGramRate
                If _IsGram = True Then
                    If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("DesignChargesRate").Value) Then
                        If (grdItems.CurrentRow.Cells("DesignChargesRate").Value) >= 0 Then
                            If grdItems.CurrentRow.Cells("FixPrice").Value > 0 Then
                                grdItems.CurrentRow.Cells("DesignCharges").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("DesignChargesRate").Value)
                                grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                                grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                            Else
                                grdItems.CurrentRow.Cells("DesignCharges").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("DesignChargesRate").Value)
                                'grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value
                                grdItems.CurrentRow.Cells("DisAmount").Value = (((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                                grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                            End If
                            
                        End If

                        
                    End If
                End If

                ElseIf grdItems.Columns(e.ColumnIndex).Name = "DesignCharges" Then
                    _IsGram = _GoldQualityController.GetGoldQuality(grdItems.CurrentRow.Cells("GoldQualityID").Value).IsGramRate
                    If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("DesignCharges").Value) Then
                        If (grdItems.CurrentRow.Cells("DesignCharges").Value) > 0 Then
                            If _IsGram = False Then
                                'Calculate Price
                                'grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + grdItems.CurrentRow.Cells("DesignCharges").Value
                            grdItems.CurrentRow.Cells("DisAmount").Value = ((TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + grdItems.CurrentRow.Cells("GemsPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                            Else
                                'grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value
                            grdItems.CurrentRow.Cells("DisAmount").Value = (((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                            End If
                        End If

                    End If

                ElseIf grdItems.Columns(e.ColumnIndex).Name = "DisPercent" Then
                If (IIf(IsDBNull(grdItems.CurrentRow.Cells("DisPercent").Value), 0, grdItems.CurrentRow.Cells("DisPercent").Value)) >= 0 Then
                    If (IIf(IsDBNull(grdItems.CurrentRow.Cells("FixPrice").Value), 0, grdItems.CurrentRow.Cells("FixPrice").Value)) > 0 Then
                        If _IsGram = True Then
                            grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                        Else
                            grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value) * (grdItems.CurrentRow.Cells("disPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                        End If
                    Else
                        If _IsGram = False Then
                            'Calculate Price
                            grdItems.CurrentRow.Cells("DisAmount").Value = ((TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                        Else
                            grdItems.CurrentRow.Cells("DesignCharges").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignChargesRate").Value), 0, grdItems.CurrentRow.Cells("DesignChargesRate").Value)
                            grdItems.CurrentRow.Cells("DisAmount").Value = (((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                        End If
                    End If
                End If

            ElseIf grdItems.Columns(e.ColumnIndex).Name = "GemsPrice" Then
                If (IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) >= 0 Then
                    If (IIf(IsDBNull(grdItems.CurrentRow.Cells("FixPrice").Value), 0, grdItems.CurrentRow.Cells("FixPrice").Value)) > 0 Then
                        If _IsGram = True Then
                            grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value + grdItems.CurrentRow.Cells("DesignCharges").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                        Else
                            grdItems.CurrentRow.Cells("DisAmount").Value = (grdItems.CurrentRow.Cells("FixPrice").Value) * (grdItems.CurrentRow.Cells("disPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value - grdItems.CurrentRow.Cells("DisAmount").Value
                        End If
                    Else
                        If _IsGram = False Then
                            'Calculate Price
                            grdItems.CurrentRow.Cells("DisAmount").Value = ((TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("Rate").Value)) + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                        Else
                            grdItems.CurrentRow.Cells("DesignCharges").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignChargesRate").Value), 0, grdItems.CurrentRow.Cells("DesignChargesRate").Value)
                            grdItems.CurrentRow.Cells("DisAmount").Value = (((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("DesignCharges").Value), 0, grdItems.CurrentRow.Cells("DesignCharges").Value) + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value)) * (grdItems.CurrentRow.Cells("DisPercent").Value / 100)
                            grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("Rate").Value) + grdItems.CurrentRow.Cells("DesignCharges").Value + IIf(IsDBNull(grdItems.CurrentRow.Cells("GemsPrice").Value), 0, grdItems.CurrentRow.Cells("GemsPrice").Value) - grdItems.CurrentRow.Cells("DisAmount").Value
                        End If
                    End If
                End If

                End If




        End If
        CalculateGridTotalAmount()
        CalculategrAlldTotalWeight()
        CalculateCommDisAmt()
        'CalculateDisAmt()
        'CalculateExpressAmt()

        _dtWSInvoiceItem = grdItems.DataSource
    End Sub

   
    Private Sub grdItems_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdItems.RowsRemoved
        CalculateGridTotalAmount()
        'CalculateNetAmt()
        CalculateCommDisAmt()
        CalculategrAlldTotalWeight()
        'CalculateDisAmt()
        'CalculateExpressAmt()
    End Sub

#Region "Click and TextChanged"
    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Dim DataItem As DataRow
        Dim dtCustomer As New DataTable
        Dim objCustomer As New CustomerInfo
        dtCustomer = objCustomerController.GetAllCustomer()
        DataItem = DirectCast(SearchData.FindFast(dtCustomer, "Customer List"), DataRow)
        If DataItem IsNot Nothing Then
            _CustomerID = DataItem.Item("@CustomerID").ToString
            txtCustomerCode.Text = DataItem.Item("CustomerCode").ToString
            txtCustomerName.Text = DataItem.Item("CustomerName_").ToString
            txtAddress.Text = DataItem.Item("CustomerAddress_").ToString
            If Global_IsMemberCustomer Then
                txtMemberCode.Text = DataItem.Item("MemberCode").ToString
            End If
        End If
    End Sub

  

    Private Sub optConsignment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optConsignment.CheckedChanged
        OptionCheckChange()
        ForOptConsignment()
    End Sub

    Private Sub optCash_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optCash.CheckedChanged
        OptionCheckChange()
        ForOptConsignment()
    End Sub
    Private Sub optBank_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        OptionCheckChange()
        ForOptConsignment()
    End Sub
    Private Sub optCredit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optCredit.CheckedChanged
        OptionCheckChange()
        ForOptConsignment()
    End Sub

    'Private Sub txtDiscountDis_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscountDis.KeyPress
    '    MyBase.ValidateNumeric(sender, e, False)
    'End Sub
    'Private Sub txtDiscountDis_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtDiscountDis.Text = "" Then txtDiscountDis.Text = "0"
    '    If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
    '    If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
    '    If optConsignment.Checked = False Then
    '        CalculateDisAmt()
    '    Else
    '        ForOptConsignment()
    '    End If
    'End Sub

    'Private Sub txtCommissionDis_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCommissionDis.KeyPress
    '    MyBase.ValidateNumeric(sender, e, False)
    'End Sub

    'Private Sub txtCommissionDis_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtCommissionDis.Text = "" Then txtCommissionDis.Text = "0"
    '    If txtExpenseAmt.Text = "" Then txtExpenseAmt.Text = "0"
    '    If txtCommissionAmt.Text = "" Then txtCommissionAmt.Text = "0"
    '    If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
    '    If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
    '    If optConsignment.Checked = False Then
    '        CalculateCommDisAmt()
    '    Else
    '        ForOptConsignment()
    '    End If
    'End Sub

    Private Sub txtExpenseAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    'Private Sub txtExpenseAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtExpenseAmt.Text = "" Then txtExpenseAmt.Text = 0
    '    If optConsignment.Checked = False Then
    '        If txtTotalAmt.Text <> "" And txtNetAmt.Text <> "" Then
    '            txtNetAmt.Text = CStr((CLng(txtTotalAmt.Text) + CLng(txtExpenseAmt.Text)) - (CLng(txtDiscountAmt.Text)))
    '            'txtAddOrSubAmt.Text = CStr(CLng(txtNetAmt.Text) - (CLng(txtTotalAmt.Text) + CLng(txtExpenseAmt.Text) - CLng(txtCommissionAmt.Text) - CLng(txtDiscountAmt.Text)))
    '            'txtPaidAmt.Text = txtNetAmt.Text
    '        End If
    '        'CalculateExpressAmt()
    '    Else
    '        ForOptConsignment()
    '    End If

    'End Sub
    Private Sub txtTotalAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAmt.TextChanged
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"

       
        txtDisPercent.Text = "0"
        txtAddOrSubAmt.Text = "0"
        txtDiscountAmt.Text = "0"
        CalculateFinalAmount()

    End Sub

    Private Sub txtNetAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtNetAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmt.TextChanged
        If optConsignment.Checked = False Then

            If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
            If txtAddOrSubAmt.Text = "" Then txtAddOrSubAmt.Text = "0"
            If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
            If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
            If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
            If txtValue.Text = "" Then txtValue.Text = "0"
            If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
            If txtMemberDis.Text = "" Then txtMemberDis.Text = "0"

            If CInt(txtMemberDis.Text) > 0 Then
                txtMemberDisAmt.Text = CStr(CLng(txtTotalAmt.Text) * (CLng(txtMemberDis.Text) / 100))
            End If

            txtAddOrSubAmt.Text = Format(Val((CLng(txtNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtValue.Text) + CLng(txtMemberDisAmt.Text)) - CLng(txtTotalAmt.Text)), "###,##0.##")
            If Global_IsCash Then
                txtPaidAmt.Text = Format(Val(CLng(txtNetAmt.Text)), "###,##0.##")
            End If
            txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")

        Else
            ForOptConsignment()
        End If

    End Sub

    Private Sub txtPaidAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaidAmt.TextChanged
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
    End Sub

    Private Sub SearchWholeSale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchWholeSale.Click
        btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objWSInvoice As New WholeSaleInvoiceInfo

        _IsUpdate = False
        dt = objWSaleInvoiceController.GetAllWholeSaleInvoice()
        DataItem = DirectCast(SearchData.FindFast(dt, "WholeSalesInvoice List"), DataRow)
        If DataItem IsNot Nothing Then
            _WSInvoieID = DataItem.Item("WholeSaleInvoiceID").ToString()
            objWSInvoice = objWSaleInvoiceController.GetWholeSaleInvoiceByID(_WSInvoieID)
            'ShowWholeSalesInvoiceData(objWSInvoice)
            _IsUpdateHeader = True
            _dtWSInvoiceItem = objWSaleInvoiceController.GetWholeSaleInvoiceItemByID(_WSInvoieID)
            grdItems.DataSource = _dtWSInvoiceItem
            ShowWholeSalesInvoiceData(objWSInvoice)
            CalculateGridTotalAmount()
            CalculategrAlldTotalWeight()
            'grdItems.DataSource = _dtWSInvoiceItem
            OptionCheckChange()
            'btnDelete.Enabled = True
            If Global_CurrentUser = "Administrator" Then
                btnDelete.Enabled = True
                btnSave.Enabled = True
            Else
                btnDelete.Enabled = False
                btnSave.Enabled = False
            End If


        End If
    End Sub
    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub
#End Region

    'Private Sub chkPaidByUSD_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If (chkPaidByUSD.Checked = True) Then
    '        lblPaidByUSD.Enabled = True
    '        lblExchangeRate.Enabled = True
    '        lblRefundKyat.Enabled = True
    '        txtPaidUSD.Enabled = True
    '        txtExchangeRate.Enabled = True
    '        txtRefundKyats.Enabled = True
    '    Else
    '        lblPaidByUSD.Enabled = False
    '        lblExchangeRate.Enabled = False
    '        lblRefundKyat.Enabled = False
    '        txtPaidUSD.Enabled = False
    '        txtExchangeRate.Enabled = False
    '        txtRefundKyats.Enabled = False
    '        txtPaidUSD.Text = "0"
    '        txtExchangeRate.Text = "0"
    '        txtRefundKyats.Text = "0"
    '    End If
    'End Sub

    Private Sub txtExchangeRate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPaidUSD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtRefundKyats_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub lnkCustomer_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Dim frm As New frm_Customer
        frm.ShowDialog()
    End Sub

    Private Sub lnkBroker_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Dim frm As New frm_Customer
        frm.ShowDialog()
    End Sub

    
    Private Sub txtDiscountAmt_TextChanged(sender As Object, e As EventArgs) Handles txtDiscountAmt.TextChanged
        CalculateFinalAmount()

    End Sub

    Private Sub CalculateFinalAmount()
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtAddOrSubAmt.Text = "" Then txtAddOrSubAmt.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtValue.Text = "" Then txtValue.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"

        txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text) + CLng(txtAddOrSubAmt.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtValue.Text) + CLng(txtMemberDisAmt.Text))), "###,##0.##")
        If Global_IsCash = True Then
            txtPaidAmt.Text = Format(Val(CLng(txtNetAmt.Text)), "###,##0.##")
        End If
        txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")

    End Sub



    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim UserInfo As New UserInfo
        Dim dt As New DataTable

        dt = objWSaleInvoiceController.GetWholeSalePrint(_WSInvoieID)

        dt.Rows(0).Item("PointBalance") = VoucherPointBalance
        frmPrint.PrintWholeSale(dt)
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub

    Private Sub lnkCustomer_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCustomer.LinkClicked
        Dim frm As New frm_CustomerShow
        Dim ObjCustomer As CustomerInfo
        If _CustomerID <> "" Then
            ObjCustomer = objCustomerController.GetCustomerByID(_CustomerID)
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
            ObjCustomer = objCustomerController.GetCustomerByID(_CustomerID)
            txtCustomerCode.Text = ObjCustomer.CustomerCode
            txtCustomerName.Text = ObjCustomer.CustomerName
            txtAddress.Text = ObjCustomer.CustomerAddress
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("WholeSaleInvoice")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub


    Private Sub txtDisPercent_TextChanged(sender As Object, e As EventArgs) Handles txtDisPercent.TextChanged
        If txtDisPercent.Text = "" Then txtDisPercent.Text = "0"
        Dim DAmt As Integer
        DAmt = ((txtTotalAmt.Text)) * (txtDisPercent.Text / 100)
        txtDiscountAmt.Text = Format(CLng(DAmt), "###,##0")
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
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtTotalAmt.Text = "0" Then
            txtMemberDisAmt.Text = 0
        Else
            txtMemberDisAmt.Text = CStr(CLng(txtTotalAmt.Text) * (CLng(txtMemberDis.Text) / 100))
        End If
    End Sub

    Private Sub txtMemberDisAmt_TextChanged(sender As Object, e As EventArgs) Handles txtMemberDisAmt.TextChanged
        If txtValue.Text = "" Then txtValue.Text = "0"
        If txtMemberDisAmt.Text = "" Then txtMemberDisAmt.Text = "0"
        If txtAddOrSubAmt.Text = "" Then txtAddOrSubAmt.Text = "0"
        If txtTotalAmt.Text = "0" Then
            txtNetAmt.Text = 0
        Else
            'txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text)) - (CLng(txtPromotionAmt.Text) + CLng(txtValue.Text) + CLng(txtDiscountAmt.Text) + CLng(txtMemberDisAmt.Text))), "###,##0.##")
            'txtAddOrSub.Text = CStr((CLng(txtNetAmt.Text)) - ((CLng(txtTotalAmt.Text) + CLng(txtExpenseAmt.Text)) - (CLng(txtCommissionAmt.Text) + CLng(txtPromotionAmt.Text))))

            txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text) + CLng(txtAddOrSubAmt.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0")
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


        'dtRedeemID = Await WebService.MemberCard.GetRedeemIDByMemberCode(MCode, Global_CompanyName, Token, InvoiceID)

        If (dtRedeemID.Rows(0).Item("RedeemID") <> "") Then

            Try

                TempRedeemID = dtRedeemID.Rows(0).Item("RedeemID")
                RedeemID = TempRedeemID.Replace("|", ",")

                _RedeemID = dtRedeemID.Rows(0).Item("RedeemID")
                RedeemBool = objWSaleInvoiceController.UpdateRedeemID(RedeemID, InvoiceID)
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



    Private Async Sub SaveSaleMemberCard(ByVal objSaleInvoice As CommonInfo.WholeSaleInvoiceInfo, ByVal Status As Integer)
        Dim Result As Boolean
        'Dim Result As String

        'Dim Result As String
        '  RegName + MemberCode + GwtMember@2020

        'Dim s As String = "GWT1234501320000002GwtMember@2020"
        'Dim s1 As String = Convert.ToBase64String(Encoding.Unicode.GetBytes(s))
        'Dim s2 As String = Encoding.Unicode.GetString(Convert.FromBase64String(s1))

        ' Result = Await WebService.MemberCard.SaveSaleMemberCardForWholeSale(objSaleInvoice, Status, Global_CompanyName)
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

    Private Async Sub UpdateRedeem(ByVal objSaleInvoice As CommonInfo.WholeSaleInvoiceInfo, ByVal Status As Integer)
        Dim Result As Boolean

        ' Result = Await WebService.MemberCard.UpdateRedeemForWholeSale(objSaleInvoice, Status)
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
    Private Async Sub AddRedeem(ByVal objSaleInvoice As CommonInfo.WholeSaleInvoiceInfo, ByVal Status As Integer)
        Dim Result As Boolean


        'Result = Await WebService.MemberCard.AddRedeemForWholeSale(objSaleInvoice, Status)
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
        If txtTotalAmt.Text = "0" Then
            txtNetAmt.Text = 0
        Else
            txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text) + CLng(txtAddOrSubAmt.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtMemberDisAmt.Text) + CLng(txtValue.Text))), "###,##0.##")
            txtAddOrSubAmt.Text = Format(Val((CLng(txtNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtMemberDisAmt.Text) + +CLng(txtValue.Text)) - CLng(txtTotalAmt.Text)), "###,##0.##")
        End If
    End Sub
    Public Sub UpdateRedeemAndTransID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)
        If _IsMaximum = True Then
            GetTransactionID(MCode, Global_CompanyName, Token, _WSInvoieID)
        End If
        GetRedeemID(MCode, Global_CompanyName, Token, _WSInvoieID)

    End Sub
    Public Async Sub GetTransactionID(ByVal MemberCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String, ByVal InvoiceID As String)

        'dtTransactionID = Await WebService.MemberCard.GetTransactionIDByMemberCode(MCode, Global_CompanyName, Token, InvoiceID)

        If (dtTransactionID.Rows(0).Item("TransactionID") <> "") Then

            Try
                _TransactionID = dtTransactionID.Rows(0).Item("TransactionID")

                TransBool = objWSaleInvoiceController.UpdateTransactionID(_TransactionID, InvoiceID)
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
