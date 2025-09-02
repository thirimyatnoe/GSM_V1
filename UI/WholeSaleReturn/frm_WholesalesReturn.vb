Imports BusinessRule
Imports CommonInfo
Public Class frm_WholesalesReturn
    Implements IFormProcess

#Region "Declaration"
    Private _WSReturnID As String
    Private _WSIReturnItemID As String
    Private _WSInvoiceID As String
    Private _CSInvoiceID As String
    Private _CustomerID As String
    Private _TotalAmt As Long = 0
    'Private _WSDate As String = ""
    Private _totaldate As Long = 0

    Private _dtWSReturnItem As New DataTable
    Private _dtWSaleITem As New DataTable
    Private _PayType As Integer = 0
    Private _WSType As String = ""
    Dim IsWholeSaleInvoice As Boolean = True
    Private _IsUpdate As Boolean = False
    Dim numberformat As Integer
    Dim TempTK As Decimal = 0.0
    Dim _GemsTK As Decimal = 0.0
    Dim _GemsTG As Decimal = 0.0
    Dim _WasteTK As Decimal = 0.0
    Dim _WasteTG As Decimal = 0.0
    Dim _ItemTK As Decimal = 0.0
    Dim _ItemTG As Decimal = 0.0
    Dim _GoldTK As Decimal = 0.0
    Dim _GoldTG As Decimal = 0.0
    Private _IsGram As Boolean = False
    Private objWSaleInvoiceController As WholeSaleInvoice.IWholeSaleInvoiceController = Factory.Instance.CreateWholeSaleInvoiceController

    Private objConsignmentSaleController As ConsignmentSale.IConsignmentSaleController = Factory.Instance.CreateConsignmentSaleController
    Private objWSaleReturnController As WholeSaleReturn.IWholeSaleReturnController = Factory.Instance.CreateWholeSaleReturnController
    Private objCustomerController As Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private objStaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private objStockController As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private objCurrentController As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private objConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemNameController As ItemName.IItemNameController = Factory.Instance.CreateItemName

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

    Private Sub frm_WholesalesReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MyBase._Heading.Text = "လက္ကားေရာင္း/ျပန္ပို႔"
        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        ClearData()
        FormatGrid()
        GetComBo()
        If (lblLogInUserName.Text = "Administrator") Then
            txtDiscountDis.Enabled = True
        Else
            txtDiscountDis.Enabled = False
        End If
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If objWSaleReturnController.DeleteWholeSaleReturn(_WSReturnID) Then
            ClearData()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        ClearData()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objWSReturn As New WholeSaleReturnInfo
        If IsFillData() Then
            objWSReturn = GetData()
            If objWSaleReturnController.SaveWholeSaleReturn(objWSReturn, _dtWSReturnItem) Then

                _WSReturnID = objWSReturn.WholesaleReturnID
                If (MsgBox("Do You Want To Save And Print Return Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                    Dim frmPrint As New frm_ReportViewer
                    Dim UserInfo As New UserInfo
                    Dim dt As New DataTable

                    dt = objWSaleReturnController.GetWholeSaleReturnPrint(_WSReturnID)
                    ClearData()
                    frmPrint.PrintWholeSaleReturn(dt)
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
    End Function

#Region " Private Methods "
    Public Sub FormatGrid()
        With grdItems
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Zawgyi-one", 9.25)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "WSReturnItemID"
                .DataPropertyName = "WSReturnItemID"
                .Name = "WSReturnItemID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "WSReturnID"
                .DataPropertyName = "WSReturnID"
                .Name = "WSReturnID"
                .Visible = False
            End With
            .Columns.Add(dcID)

            Dim dcForSaleID As New DataGridViewTextBoxColumn
            With dcForSaleID
                .HeaderText = "ForSaleID"
                .DataPropertyName = "ForSaleID"
                .Name = "ForSaleID"
                .Width = 0
                .Visible = False
            End With
            .Columns.Add(dcForSaleID)

            Dim dcItemNameID As New DataGridViewTextBoxColumn()
            dcItemNameID.HeaderText = "ItemNameID"
            dcItemNameID.DataPropertyName = "ItemNameID"
            dcItemNameID.Name = "ItemNameID"
            dcItemNameID.Width = 0
            dcItemNameID.Visible = False
            dcItemNameID.DefaultCellStyle.Font = New Font("Myanmar3", 8.5)
            dcItemNameID.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcItemNameID)

            Dim dcGQID As New DataGridViewTextBoxColumn()
            dcGQID.HeaderText = "GoldQualityID"
            dcGQID.DataPropertyName = "GoldQualityID"
            dcGQID.Name = "GoldQualityID"
            dcGQID.Width = 0
            dcGQID.Visible = False
            dcGQID.DefaultCellStyle.Font = New Font("Myanmar3", 8.5)
            dcGQID.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcGQID)


            Dim dcSNO As New DataGridViewTextBoxColumn
            With dcSNO
                .HeaderText = "No"
                .DataPropertyName = "SNo"
                .Name = "No"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 30
                .Visible = True
            End With
            .Columns.Add(dcSNO)

            Dim dcDesign As New DataGridViewTextBoxColumn
            With dcDesign
                .HeaderText = "Barcode No."
                .DataPropertyName = "ItemCode"
                .Name = "ItemCode"
                .Width = 145
                .Visible = True
            End With
            .Columns.Add(dcDesign)

            Dim dcItemName As New DataGridViewTextBoxColumn()
            dcItemName.HeaderText = "ItemName"
            dcItemName.DataPropertyName = "ItemName"
            dcItemName.Name = "ItemName"
            dcItemName.Width = 140
            dcItemName.Visible = True
            dcItemName.DefaultCellStyle.Font = New Font("Myanmar3", 8.5)
            dcItemName.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcItemName)

            Dim dcBtn As New DataGridViewButtonColumn
            dcBtn.HeaderText = "..."
            dcBtn.Name = "SearchButton"
            dcBtn.Visible = True
            dcBtn.Width = 25
            dcBtn.Resizable = DataGridViewTriState.False
            dcBtn.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcBtn)

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
                .HeaderText = "ေစ်းႏႈန္း"
                .DataPropertyName = "SalesRate"
                .Name = "SalesRate"
                .Width = 80
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
                .Width = 80
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
            .Columns.Add(dcFixPrice)

            Dim dcAmt As New DataGridViewTextBoxColumn()
            With dcAmt
                .HeaderText = "က်သင့္ေငြ"
                .DataPropertyName = "Amount"
                .Name = "Amount"
                .Width = 110
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcAmt)

            Dim dcPay As New DataGridViewCheckBoxColumn
            With dcPay
                .HeaderText = "SR"
                .DataPropertyName = "IsSale"
                .Name = "IsSale"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcPay)

            Dim dcSale As New DataGridViewCheckBoxColumn
            With dcSale
                .HeaderText = "IsReturn"
                .DataPropertyName = "IsReturn"
                .Name = "IsReturn"
                '.Width = 35
                .Visible = False
                '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcSale)

            Dim dcShow As New DataGridViewCheckBoxColumn
            With dcShow
                .HeaderText = "PR"
                .DataPropertyName = "IsShowForReturn"
                .Name = "IsShowForReturn"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcShow)

        End With
    End Sub
    Public Sub ClearData()

        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        SearchWholeSale.Focus()
        'txtWSReturnID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.WholeSaleReturn, EnumSetting.GenerateKeyType.WholeSaleReturn.ToString, dtpReturnDate.Value)
        _WSReturnID = "0"
        dtpReturnDate.Value = Now
        WholeSaleReturnItemInvoiceGenerateFormat()

        _CSInvoiceID = ""

        _PayType = 0
        _WSType = ""
        txtWholeSalesID.Text = ""

        cboStaff.SelectedValue = -1
        cboStaff.Text = ""

        _CustomerID = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtRemark.Text = ""

        txtTotalAmt.Text = "0"
        txtSaleAmt.Text = "0"
        txtSaleReturnAmt.Text = "0"
        txtNetAmt.Text = "0"
        txtAddSub.Text = "0"
        txtPaidAmt.Text = "0"
        txtBalanceAmt.Text = "0"


        lblPayType.Text = "Pay Type"
        optSale.Checked = False
        optSaleReturn.Checked = False
        optPayReturn.Checked = False

        Dim dc As New DataColumn
        _dtWSReturnItem = New DataTable
        _dtWSReturnItem.Columns.Add("SNo", System.Type.GetType("System.Int64"))
        _dtWSReturnItem.Columns.Add("WSReturnItemID", System.Type.GetType("System.String"))
        _dtWSReturnItem.Columns.Add("WSReturnID", System.Type.GetType("System.String"))
        _dtWSReturnItem.Columns.Add("ForSaleID", System.Type.GetType("System.String"))
        _dtWSReturnItem.Columns.Add("ItemCode", System.Type.GetType("System.String"))
        _dtWSReturnItem.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtWSReturnItem.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtWSReturnItem.Columns.Add("ItemNameID", System.Type.GetType("System.String"))
        _dtWSReturnItem.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))

        _dtWSReturnItem.Columns.Add("ItemTK", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("ItemTG", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("GoldTK", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("GoldTG", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("WasteTK", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("WasteTG", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("ItemK", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("ItemP", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("ItemY", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("WasteK", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("WasteP", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("WasteY", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("GoldK", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("GoldP", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("GoldY", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("GemsK", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("GemsP", System.Type.GetType("System.Int16"))
        _dtWSReturnItem.Columns.Add("GemsY", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("Gram", System.Type.GetType("System.Decimal"))
        _dtWSReturnItem.Columns.Add("SalesRate", System.Type.GetType("System.Int64"))
        _dtWSReturnItem.Columns.Add("FixPrice", System.Type.GetType("System.Int64"))
        _dtWSReturnItem.Columns.Add("Amount", System.Type.GetType("System.Int64"))
        _dtWSReturnItem.Columns.Add("IsSale", System.Type.GetType("System.Boolean"))
        _dtWSReturnItem.Columns.Add("IsReturn", System.Type.GetType("System.Boolean"))
        _dtWSReturnItem.Columns.Add("IsShowForReturn", System.Type.GetType("System.Boolean"))


        grdItems.AutoGenerateColumns = False
        grdItems.ReadOnly = False
        grdItems.DataSource = _dtWSReturnItem

        FormatGrid()
        _TotalAmt = 0
        _totaldate = 0
        lblTotalDate.Text = "-"
        lblWDate.Text = "-"
        txtDiscountDis.Text = "0"
        txtDiscountAmt.Text = "0"
        txtCommisionDis.Text = "0"
    End Sub
    Private Sub WholeSaleReturnItemInvoiceGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.WholeSaleReturnStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtWSReturnID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpReturnDate.Value)
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

        If _dtWSReturnItem.Rows.Count < 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        'If _totaldate <= 31 Then
        '    If CLng(txtTotalAmt.Text) > CLng(CLng(_TotalAmt / 100) * 30) Then
        '        MsgBox("Your Return Amount is Greater Than limit Amount", MsgBoxStyle.Information, AppName)
        '        Return False
        '    End If
        'End If

        'If _totaldate <= 31 Then
        '    If _WSType <> "Pay" Then
        '        If CLng(txtTotalAmt.Text) > CLng(CLng(_TotalAmt / 100) * 30) Then
        '            MsgBox("Your Return Amount is Greater Than limit Amount", MsgBoxStyle.Information, AppName)
        '            grdItems.Focus()

        '            Exit Function
        '        End If
        '    End If

        'End If


        Return True
    End Function
    Public Function GetData() As CommonInfo.WholeSaleReturnInfo
        Dim objWSaleReturnInfo As New CommonInfo.WholeSaleReturnInfo
        With objWSaleReturnInfo
            .WholesaleReturnID = IIf(_WSReturnID = "", "-", _WSReturnID)
            .ConsignmentSaleID = IIf(_CSInvoiceID = "", "-", _CSInvoiceID)
            .WReturnDate = dtpReturnDate.Value
            .WholeSaleInvoiceID = txtWholeSalesID.Text
            .StaffID = cboStaff.SelectedValue
            .CustomerID = _CustomerID
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .SaleAmount = IIf(txtSaleAmt.Text = "", 0, txtSaleAmt.Text)
            .SaleReturnAmount = IIf(txtSaleReturnAmt.Text = "", 0, txtSaleReturnAmt.Text)
            .TotalAmount = IIf(txtTotalAmt.Text = "", 0, txtTotalAmt.Text)
            .AddOrSub = IIf(txtAddSub.Text = "", 0, txtAddSub.Text)
            .PaidAmount = IIf(txtPaidAmt.Text = "", 0, txtPaidAmt.Text)
            .Discount = IIf(txtDiscountAmt.Text = "", 0, txtDiscountAmt.Text)
        End With
        Return objWSaleReturnInfo
    End Function
    Private Sub GetComBo()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = objStaffController.GetStaffList().DefaultView
    End Sub
    Private Sub CalculateGridTotalAmount()
        Dim grdTotalAmt As Long = 0
        If (grdItems) IsNot Nothing Then
            If grdItems.RowCount > -1 Then
                For i As Integer = 0 To grdItems.RowCount - 1
                    grdTotalAmt += CLng(Val(grdItems.Rows(i).Cells("Amount").FormattedValue))
                Next
                If optPayReturn.Checked Then
                    txtTotalAmt.Text = CStr(grdTotalAmt)
                    txtSaleAmt.Text = "0"
                    txtSaleReturnAmt.Text = "0"
                    txtNetAmt.Text = "0"
                    txtAddSub.Text = "0"
                    txtPaidAmt.Text = "0"
                    txtBalanceAmt.Text = "0"
                ElseIf optSale.Checked Then
                    txtSaleAmt.Text = CStr(grdTotalAmt)
                    txtSaleReturnAmt.Text = "0"
                    txtTotalAmt.Text = CStr(grdTotalAmt)

                    txtNetAmt.Text = CStr(txtTotalAmt.Text)
                    txtAddSub.Text = "0"
                    txtPaidAmt.Text = CStr(txtNetAmt.Text)
                    txtBalanceAmt.Text = "0"
                Else
                    txtSaleAmt.Text = "0"
                    txtSaleReturnAmt.Text = CStr(grdTotalAmt)
                    txtTotalAmt.Text = CStr(grdTotalAmt)


                End If

                'Dim CurrentMonth As Integer
                'CurrentMonth = Now.Month
                'If (CurrentMonth = 1 Or 3 Or 5 Or 7 Or 8 Or 10 Or 12) Then
                '    MsgBox("So Sad", MsgBoxStyle.Information, AppName)
                '    Exit Sub
                'ElseIf (CurrentMonth = 4 Or 6 Or 9 Or 11) Then

                'Else
                '    If (Now.Year / 4 = 0) Then

                '    Else

                '    End If
                'End If


                'If _totaldate <= 31 Then
                '    If _WSType <> "Pay" Then
                '        If CLng(txtTotalAmt.Text) > CLng(CLng(_TotalAmt / 100) * 30) Then
                '            MsgBox("Your Return Amount is Greater Than limit Amount", MsgBoxStyle.Information, AppName)
                '            grdItems.Focus()

                '            Exit Sub
                '        End If
                '    End If

                'End If



            End If
        End If
        _dtWSReturnItem = grdItems.DataSource
    End Sub
    Private Function GetExistedItemscode(ByVal _CurrentRowIndex As Integer) As String
        GetExistedItemscode = ""
        If (_dtWSReturnItem) IsNot Nothing Then
            For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                If _dtWSReturnItem.Rows(i).RowState <> DataRowState.Deleted And i <> _CurrentRowIndex Then
                    GetExistedItemscode += "'" & _dtWSReturnItem.Rows(i).Item("ItemCode") & "',"
                End If
            Next
        End If

        Return GetExistedItemscode.Trim(",")
    End Function
    Private Sub ShowWholeSalesReturnData(ByVal objWSalesReturn As WholeSaleReturnInfo)
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        Dim ItemCategory As New ItemCategoryInfo
        Dim objWSInvoiceInfo As WholeSaleInvoiceInfo
        Dim GoldQuality As New GoldQualityInfo
        'from Stock
        With objWSalesReturn
            txtWSReturnID.Text = _WSReturnID
            dtpReturnDate.Value = .WReturnDate
            _WSInvoiceID = .WholeSaleInvoiceID
            _CSInvoiceID = .ConsignmentSaleID
            txtWholeSalesID.Text = .WholeSaleInvoiceID

            If _CSInvoiceID = "-" Then
                objWSInvoiceInfo = objWSaleInvoiceController.GetWholeSaleInvoiceByID(.WholeSaleInvoiceID)
                With objWSInvoiceInfo
                    _PayType = .PayType
                    '_TotalAmt = .TotalPayment
                    lblWDate.Text = Format(CDate(.WDate), "dd-MM-yyyy")
                    _totaldate = DateDiff(DateInterval.Day, CDate(.WDate), dtpReturnDate.Value.Date)
                    lblTotalDate.Text = CStr(_totaldate) + " days"

                End With

                If (_PayType = 1) Then
                    lblPayType.Text = "Pay"

                ElseIf (_PayType = 0) Then
                    lblPayType.Text = "Sale"

                ElseIf (_PayType = 2) Then
                    lblPayType.Text = "Sale"

                ElseIf (_PayType = 3) Then
                    lblPayType.Text = "Sale"
                End If

                IsWholeSaleInvoice = True
            Else
                If (_PayType = 1) Then
                    lblPayType.Text = "Consignment"
                End If
                IsWholeSaleInvoice = False
            End If


            cboStaff.SelectedValue = .StaffID
            _CustomerID = .CustomerID
            ShowCustomer(_CustomerID)
            txtRemark.Text = .Remark
            txtSaleAmt.Text = .SaleAmount
            txtSaleReturnAmt.Text = .SaleReturnAmount
            txtTotalAmt.Text = .TotalAmount
            txtAddSub.Text = .AddOrSub
            txtDiscountAmt.Text = .Discount
            'txtDiscountDis.Text = .DiscountPercent
            txtNetAmt.Text = CStr((CLng(txtTotalAmt.Text) - CLng(txtDiscountAmt.Text)) + CLng(txtAddSub.Text))
            txtPaidAmt.Text = .PaidAmount


        End With

    End Sub
    Private Sub ShowCustomer(ByVal CustomerID As String)
        Dim dtCustomer As New DataTable
        Dim objCustomer As New CustomerInfo
        If CustomerID <> "" Then
            objCustomer = objCustomerController.GetCustomerByID(CustomerID)
            txtCustomerCode.Text = objCustomer.CustomerCode
            txtCustomerName.Text = objCustomer.CustomerName

        End If
    End Sub


#End Region

#Region "Click"


    Private Sub btnWholeSaleID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWholeSaleID.Click

        Dim DataItem As DataRow
        Dim dtWholeSale As New DataTable
        Dim objCustomer As New CustomerInfo
        Dim objWSale As New WholeSaleInvoiceInfo
        Dim objCSale As New ConsignmentSaleInfo
        If chkView.Checked = True Then
            dtWholeSale = objWSaleInvoiceController.GetWSInvoice()
        Else
            dtWholeSale = objWSaleInvoiceController.GetWSInvoiceAndCSInvoice()
        End If

        ' dtWholeSale = objWSaleInvoiceController.GetWSInvoiceAndCSInvoice()
        DataItem = DirectCast(SearchData.FindFast(dtWholeSale, "WholeSale Invoice List"), DataRow)
        If DataItem IsNot Nothing Then
            If DataItem.Item("ConsignmentSaleID").ToString = "-" Then
                _WSInvoiceID = DataItem.Item("WholeSaleInvoiceID").ToString
                txtWholeSalesID.Text = _WSInvoiceID
                IsWholeSaleInvoice = True
                _WSType = DataItem.Item("PayType").ToString


                _totaldate = DateDiff(DateInterval.Day, CDate(DataItem.Item("@WDate")), dtpReturnDate.Value.Date)

                lblTotalDate.Text = CStr(_totaldate) + " days"
                lblWDate.Text = Format(CDate(DataItem.Item("@WDate")), "dd-MM-yyyy")
                _TotalAmt = DataItem.Item("NetAmount")
                txtDiscountDis.Text = DataItem.Item("Discount")
                dtWholeSale = objWSaleInvoiceController.GetWholeSaleInvoiceItemByID(_WSInvoiceID)
                objWSale = objWSaleInvoiceController.GetWholeSaleInvoiceByID(_WSInvoiceID)

                _CustomerID = objWSale.CustomerID
                ShowCustomer(_CustomerID)
                ' _dtWSReturnItem = objWSaleInvoiceController.GetItemCodeByWholeSaleID(_WSInvoiceID)
                If (objWSale.PayType = 1) Then
                    lblPayType.Text = "Pay"
                    optPayReturn.Checked = True

                ElseIf (objWSale.PayType = 0) Then
                    lblPayType.Text = "Sale"
                    optSaleReturn.Checked = True

                ElseIf (objWSale.PayType = 2) Then
                    lblPayType.Text = "Sale"
                    optSaleReturn.Checked = True

                ElseIf (objWSale.PayType = 3) Then
                    lblPayType.Text = "Sale"
                    optSaleReturn.Checked = True

                End If


                If (optPayReturn.Checked) Then
                    If grdItems.Rows.Count <> -1 Then
                        If _dtWSReturnItem.Rows.Count > 0 Then
                            For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                                _dtWSReturnItem.Rows(i).Item("IsSale") = False
                                _dtWSReturnItem.Rows(i).Item("IsReturn") = True
                                _dtWSReturnItem.Rows(i).Item("IsShowForReturn") = True

                            Next
                        End If
                    End If
                ElseIf (optSale.Checked) Then
                    If grdItems.Rows.Count <> -1 Then
                        If _dtWSReturnItem.Rows.Count > 0 Then
                            For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                                _dtWSReturnItem.Rows(i).Item("IsSale") = True
                                _dtWSReturnItem.Rows(i).Item("IsReturn") = False

                            Next
                        End If
                    End If
                ElseIf (optSaleReturn.Checked) Then
                    If grdItems.Rows.Count <> -1 Then
                        If _dtWSReturnItem.Rows.Count > 0 Then
                            For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                                _dtWSReturnItem.Rows(i).Item("Pay") = True
                                _dtWSReturnItem.Rows(i).Item("Sale") = True
                                _dtWSReturnItem.Rows(i).Item("IsShowForReturn") = False
                            Next
                        End If
                    End If

                End If

            Else
                _WSInvoiceID = DataItem.Item("WholeSaleInvoiceID").ToString
                txtWholeSalesID.Text = _WSInvoiceID
                IsWholeSaleInvoice = False

                _CSInvoiceID = DataItem.Item("ConsignmentSaleID").ToString
                dtWholeSale = objConsignmentSaleController.GetConsignmentSaleItemByID(_CSInvoiceID)
                objCSale = objConsignmentSaleController.GetConsignmentSaleByID(_CSInvoiceID)
                _TotalAmt = objCSale.NetAmount
                _CustomerID = objCSale.CustomerID
                ShowCustomer(_CustomerID)
                lblPayType.Text = "Consignment"
                optSaleReturn.Checked = True
                '_dtWSReturnItem = objConsignmentSaleController.GetItemCodeByConsignmentSaleID(_CSInvoiceID)

                If (optPayReturn.Checked) Then
                    If grdItems.Rows.Count <> -1 Then
                        If _dtWSReturnItem.Rows.Count > 0 Then
                            For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                                _dtWSReturnItem.Rows(i).Item("IsSale") = False
                                _dtWSReturnItem.Rows(i).Item("IsReturn") = True
                                _dtWSReturnItem.Rows(i).Item("IsShowForReturn") = True

                            Next
                        End If
                    End If
                ElseIf (optSale.Checked) Then
                    If grdItems.Rows.Count <> -1 Then
                        If _dtWSReturnItem.Rows.Count > 0 Then
                            For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                                _dtWSReturnItem.Rows(i).Item("IsSale") = True
                                _dtWSReturnItem.Rows(i).Item("IsReturn") = False

                            Next
                        End If
                    End If
                ElseIf (optSaleReturn.Checked) Then
                    If grdItems.Rows.Count <> -1 Then
                        If _dtWSReturnItem.Rows.Count > 0 Then
                            For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                                _dtWSReturnItem.Rows(i).Item("Pay") = True
                                _dtWSReturnItem.Rows(i).Item("Sale") = True
                                _dtWSReturnItem.Rows(i).Item("IsShowForReturn") = False
                            Next
                        End If
                    End If

                End If


            End If
            'grdItems.DataSource = _dtWSReturnItem
            'CalculateGridTotalAmount()
        End If

    End Sub
    Private Sub SearchWholeSale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchWholeSale.Click
        btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objWSReturn As New WholeSaleReturnInfo

        _IsUpdate = False
        dt = objWSaleReturnController.GetAllWholeSaleReturn()
        DataItem = DirectCast(SearchData.FindFast(dt, "WholeSales Return List"), DataRow)
        If DataItem IsNot Nothing Then
            _WSReturnID = DataItem.Item("WholeSaleReturnID").ToString()
            objWSReturn = objWSaleReturnController.GetWholeSaleReturnByID(_WSReturnID)
            'ShowWholeSalesInvoiceData(objWSInvoice)

            _dtWSReturnItem = objWSaleReturnController.GetWholeSaleReturnItemByID(_WSReturnID)
            grdItems.DataSource = _dtWSReturnItem

            If _dtWSReturnItem IsNot Nothing Then
                If _dtWSReturnItem.Rows.Count > 0 Then
                    If _dtWSReturnItem.Rows(0).Item("IsReturn") = False And _dtWSReturnItem.Rows(0).Item("IsSale") = True Then
                        optSale.Checked = True
                    ElseIf _dtWSReturnItem.Rows(0).Item("IsReturn") = True And _dtWSReturnItem.Rows(0).Item("IsSale") = True Then
                        optSaleReturn.Checked = True
                    ElseIf _dtWSReturnItem.Rows(0).Item("IsReturn") = True And _dtWSReturnItem.Rows(0).Item("IsSale") = False Then
                        optPayReturn.Checked = True
                    End If
                End If

            End If

            ShowWholeSalesReturnData(objWSReturn)
            grdItems.DataSource = _dtWSReturnItem
            CalculateGridTotalAmount()
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

    'Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
    '    Dim DataItem As DataRow
    '    Dim dtCustomer As New DataTable
    '    Dim objCustomer As New CustomerInfo
    '    dtCustomer = objCustomerController.GetAllCustomerList()
    '    DataItem = DirectCast(Search.FindFast(dtCustomer, "Customer List"), DataRow)
    '    If DataItem IsNot Nothing Then
    '        _CustomerID = DataItem.Item("@CustomerID").ToString
    '        txtCustomerCode.Text = DataItem.Item("CustomerCode").ToString
    '        txtCustomerName.Text = DataItem.Item("CustomerName").ToString

    '    End If
    'End Sub

#End Region

    Private Sub grdItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellContentClick
        If e.ColumnIndex <> 8 Then Exit Sub


        Dim DataItem As DataRow
        Dim dt As New DataTable
        If txtWholeSalesID.Text <> "" Then
            If IsWholeSaleInvoice = True Then
                dt = objWSaleInvoiceController.GetBarcodeDataByWholeSaleID(GetExistedItemscode(e.RowIndex), _WSInvoiceID)
                'dt = _dtWSReturnItem

            Else
                dt = objConsignmentSaleController.GetBarcodeDataByConsignmentSaleID(GetExistedItemscode(e.RowIndex), _CSInvoiceID)


            End If
            DataItem = DirectCast(SearchData.FindFast(dt, "WholeSaleReturn List"), DataRow)
            Dim _CurrentPriceInfo As CurrentPriceInfo
            If DataItem IsNot Nothing Then
                If grdItems.CurrentRow.IsNewRow Then
                    _IsUpdate = False
                    Dim dr As DataRow = _dtWSReturnItem.NewRow
                    dr.Item("SNo") = e.RowIndex + 1
                    dr.Item("ForSaleID") = DataItem("ForSaleID")
                    dr.Item("ItemCode") = DataItem("ItemCode")
                    dr.Item("Gram") = Format(DataItem("ItemTG"), "#0.000")
                    dr.Item("SalesRate") = DataItem("SalesRate")
                    dr.Item("ItemNameID") = DataItem("ItemNameID")
                    dr.Item("GoldQualityID") = DataItem("GoldQualityID")
                    dr.Item("ItemName") = _ItemNameController.GetItemName(DataItem("ItemNameID")).ItemName
                    dr.Item("GoldQuality") = _GoldQualityController.GetGoldQuality(DataItem("GoldQualityID")).GoldQuality
                    _IsGram = _GoldQualityController.GetGoldQuality(DataItem("GoldQualityID")).IsGramRate
                    dr.Item("ItemTG") = DataItem("ItemTG")
                    dr.Item("ItemTK") = DataItem("ItemTK")
                    dr.Item("GoldTG") = DataItem("GoldTG")
                    dr.Item("GoldTK") = DataItem("GoldTK")
                    dr.Item("WasteTG") = DataItem("WasteTG")
                    dr.Item("WasteTK") = DataItem("WasteTK")
                    dr.Item("GemsTG") = DataItem("GemsTG")
                    dr.Item("GemsTK") = DataItem("GemsTK")
                    'Waste
                    _WasteTK = DataItem("WasteTK")
                    _WasteTG = DataItem("WasteTG")
                    Dim WasteWeight As New CommonInfo.GoldWeightInfo
                    WasteWeight.GoldTK = _WasteTK
                    WasteWeight = objConverterController.ConvertGoldTKToKPYC(WasteWeight)
                    dr.Item("WasteK") = CStr(WasteWeight.WeightK)
                    dr.Item("WasteP") = CStr(WasteWeight.WeightP)
                    If numberformat = 1 Then
                        dr.Item("WasteY") = Format(WasteWeight.WeightY + WasteWeight.WeightC, "0.0")
                    Else
                        dr.Item("WasteY") = Format(WasteWeight.WeightY + WasteWeight.WeightC, "0.00")
                    End If
                    'Item
                    _ItemTK = DataItem("ItemTK")
                    _ItemTG = DataItem("ItemTG")
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
                    'Gold
                    _GoldTK = DataItem("GoldTK")
                    _GoldTG = DataItem("GoldTG")
                    Dim GoldWeight As New CommonInfo.GoldWeightInfo
                    GoldWeight.GoldTK = _GoldTK
                    ItemWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                    dr.Item("GoldK") = CStr(GoldWeight.WeightK)
                    dr.Item("GoldP") = CStr(GoldWeight.WeightP)
                    If numberformat = 1 Then
                        dr.Item("GoldY") = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
                    Else
                        dr.Item("GoldY") = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
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
                    dr.Item("FixPrice") = DataItem("FixPrice")


                    If (dr.Item("FixPrice")) > 0 Then
                        dr.Item("Amount") = dr.Item("FixPrice")
                    Else
                        'If _IsGram = False Then
                        '    'Calculate Price
                        '    dr.Item("Amount") = (TempTK * (dr.Item("SalesRate")))
                        'Else
                        '    dr.Item("Amount") = ((dr.Item("GoldTG")) + (dr.Item("WasteTG"))) * (dr.Item("SalesRate"))
                        'End If

                        dr.Item("Amount") = DataItem("GoldPrice")
                    End If

                    If (optPayReturn.Checked) Then
                        dr.Item("IsSale") = False
                        dr.Item("IsReturn") = True
                        dr.Item("IsShowForReturn") = True
                    ElseIf (optSale.Checked) Then
                        dr.Item("IsSale") = True
                        dr.Item("IsReturn") = False

                    ElseIf (optSaleReturn.Checked) Then
                        dr.Item("IsSale") = True
                        dr.Item("IsReturn") = True
                        dr.Item("IsShowForReturn") = False
                    End If
                    _dtWSReturnItem.Rows.Add(dr)
                    grdItems.DataSource = _dtWSReturnItem
                Else
                    With grdItems
                        grdItems.DataSource = _dtWSReturnItem
                        Dim dr As DataRow = _dtWSReturnItem.NewRow
                        .CurrentRow.Cells("No").Value = e.RowIndex + 1
                        .CurrentRow.Cells("ForSaleID").Value = DataItem("ForSaleID")
                        .CurrentRow.Cells("ItemCode").Value = DataItem("ItemCode")
                        _IsGram = _GoldQualityController.GetGoldQuality(DataItem("GoldQualityID")).IsGramRate
                        _CurrentPriceInfo = objCurrentController.GetCurrentPriceByGoldID(DataItem("GoldQualityID"))
                        .CurrentRow.Cells("SalesRate").Value = _CurrentPriceInfo.SalesRate
                        .CurrentRow.Cells("Gram").Value = Format(DataItem("ItemTG"), "#0.000")
                        .CurrentRow.Cells("ItemNameID").Value = DataItem("ItemNameID")
                        .CurrentRow.Cells("ItemName").Value = _ItemNameController.GetItemName(DataItem("ItemNameID")).ItemName
                        .CurrentRow.Cells("GoldQuality").Value = _GoldQualityController.GetGoldQuality(DataItem("GoldQualityID")).GoldQuality
                        .CurrentRow.Cells("GoldQualityID").Value = DataItem("GoldQualityID")
                        .CurrentRow.Cells("ItemTG").Value = DataItem("ItemTG")
                        .CurrentRow.Cells("ItemTK").Value = DataItem("ItemTK")
                        .CurrentRow.Cells("GoldTG").Value = DataItem("GoldTG")
                        .CurrentRow.Cells("GoldTK").Value = DataItem("GoldTK")
                        .CurrentRow.Cells("WasteTG").Value = DataItem("WasteTG")
                        .CurrentRow.Cells("WasteTK").Value = DataItem("WasteTK")
                        .CurrentRow.Cells("GemsTG").Value = DataItem("GemsTG")
                        .CurrentRow.Cells("GemsTK").Value = DataItem("GemsTK")
                        .CurrentRow.Cells("FixPrice").Value = DataItem("FixPrice")
                        '.CurrentRow.Cells("SalesRate").Value = _CurrentPriceInfo.SalesRate
                        .CurrentRow.Cells("SalesRate").Value = DataItem("SalesRate")
                        If Not IsDBNull(.CurrentRow.Cells("SalesRate").Value) Then
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

                        If (.CurrentRow.Cells("FixPrice").Value) > 0 Then
                            .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value
                        Else
                            ' If _IsGram = False Then
                            'Calculate Price
                            '.CurrentRow.Cells("Amount").Value = (TempTK * (.CurrentRow.Cells("SalesRate").Value))
                            'Else
                            '.CurrentRow.Cells("Amount").Value = ((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * (.CurrentRow.Cells("SalesRate").Value)
                            .CurrentRow.Cells("Amount").Value = DataItem("GoldPrice")
                            'End If
                        End If
                If (optPayReturn.Checked) Then
                    .CurrentRow.Cells("IsSale").Value = False
                    .CurrentRow.Cells("IsReturn").Value = True
                    .CurrentRow.Cells("IsShowForReturn").Value = True
                ElseIf (optSale.Checked) Then
                    .CurrentRow.Cells("IsSale").Value = True
                    .CurrentRow.Cells("IsReturn").Value = False

                ElseIf (optSaleReturn.Checked) Then
                    .CurrentRow.Cells("IsSale").Value = True
                    .CurrentRow.Cells("IsReturn").Value = True
                    .CurrentRow.Cells("IsShowForReturn").Value = False
                End If


                    End With

                End If
            End If

            CalculateGridTotalAmount()

        End If
    End Sub


    Private Sub grdItems_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellValidated
        Dim _BarCode As String
        Dim dt As New DataTable
        If grdItems.IsCurrentCellInEditMode = False Then Exit Sub

        If e.RowIndex <> -1 Then
            If txtWholeSalesID.Text <> "" Then
                If optSale.Checked <> False Or optSaleReturn.Checked <> False Or optPayReturn.Checked <> False Then
                    If grdItems.Columns(e.ColumnIndex).Name = "ItemCode" Then
                        If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("ItemCode").Value) Then
                            'If _PayType <> 1 Then

                            Dim objWSInfo As WholeSaleInvoiceItemInfo
                            Dim objCSInfo As ConsignmentSaleItemInfo

                            If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("ItemCode").Value) Then
                                _BarCode = CStr(grdItems.CurrentRow.Cells("ItemCode").Value)
                                If IsWholeSaleInvoice = True Then
                                    objWSInfo = objWSaleInvoiceController.GetBarcodeByWholeSaleID(GetExistedItemscode(e.RowIndex), "  and ItemCode='" & _BarCode & "'", _WSInvoiceID)
                                    If (objWSInfo.ItemCode = "") Then
                                        MsgBox("Invalid Barcode No !", MsgBoxStyle.Information, AppName)
                                        grdItems.CurrentRow.Cells("ItemCode").Value = ""
                                        _IsUpdate = True
                                        Dim dr As DataRow = _dtWSReturnItem.NewRow
                                        dr.Item("SNo") = e.RowIndex + 1

                                    Else
                                        With grdItems
                                            Dim dr As DataRow = _dtWSReturnItem.NewRow
                                            .CurrentRow.Cells("No").Value = e.RowIndex + 1

                                            _IsUpdate = False
                                            .CurrentRow.Cells("ForSaleID").Value = objWSInfo.ForSaleID
                                            .CurrentRow.Cells("ItemCode").Value = objWSInfo.ItemCode
                                            .CurrentRow.Cells("Gram").Value = Format(objWSInfo.ItemTG, "#0.000")
                                            .CurrentRow.Cells("SalesRate").Value = CInt(objWSInfo.SalesRate)
                                            .CurrentRow.Cells("ItemNameID").Value = objWSInfo.ItemNameID
                                            .CurrentRow.Cells("GoldQualityID").Value = objWSInfo.GoldQualityID
                                            .CurrentRow.Cells("ItemName").Value = _ItemNameController.GetItemName(objWSInfo.ItemNameID).ItemName
                                            .CurrentRow.Cells("GoldQuality").Value = _GoldQualityController.GetGoldQuality(objWSInfo.GoldQualityID).GoldQuality
                                            _IsGram = _GoldQualityController.GetGoldQuality(objWSInfo.GoldQualityID).IsGramRate
                                            .CurrentRow.Cells("ItemTG").Value = objWSInfo.ItemTG
                                            .CurrentRow.Cells("ItemTK").Value = objWSInfo.ItemTK
                                            .CurrentRow.Cells("GoldTG").Value = objWSInfo.GoldTG
                                            .CurrentRow.Cells("GoldTK").Value = objWSInfo.GoldTK
                                            .CurrentRow.Cells("WasteTG").Value = objWSInfo.WasteTG
                                            .CurrentRow.Cells("WasteTK").Value = objWSInfo.WasteTK
                                            .CurrentRow.Cells("GemsTG").Value = objWSInfo.GemsTG
                                            .CurrentRow.Cells("GemsTK").Value = objWSInfo.GemsTK

                                            'Waste
                                            _WasteTK = objWSInfo.WasteTK
                                            _WasteTG = objWSInfo.WasteTG
                                            Dim WasteWeight As New CommonInfo.GoldWeightInfo
                                            WasteWeight.GoldTK = _WasteTK
                                            WasteWeight = objConverterController.ConvertGoldTKToKPYC(WasteWeight)
                                            .CurrentRow.Cells("WasteK").Value = CStr(WasteWeight.WeightK)
                                            .CurrentRow.Cells("WasteP").Value = CStr(WasteWeight.WeightP)
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("WasteY").Value = Format(WasteWeight.WeightY + WasteWeight.WeightC, "0.0")
                                            Else
                                                .CurrentRow.Cells("WasteY").Value = Format(WasteWeight.WeightY + WasteWeight.WeightC, "0.00")
                                            End If

                                            'Item
                                            _ItemTK = objWSInfo.ItemTK
                                            _ItemTG = objWSInfo.ItemTG
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
                                            'Gold
                                            _GoldTK = objWSInfo.GoldTK
                                            _GoldTG = objWSInfo.GoldTG
                                            Dim GoldWeight As New CommonInfo.GoldWeightInfo
                                            GoldWeight.GoldTK = _GoldTK
                                            ItemWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                                            .CurrentRow.Cells("GoldK").Value = CStr(GoldWeight.WeightK)
                                            .CurrentRow.Cells("GoldP").Value = CStr(GoldWeight.WeightP)
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("GoldY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
                                            Else
                                                .CurrentRow.Cells("GoldY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
                                            End If


                                            'Gem
                                            _GemsTK = objWSInfo.GemsTK
                                            _GemsTG = objWSInfo.GemsTG
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

                                            .CurrentRow.Cells("FixPrice").Value = objWSInfo.FixPrice
                                            If (.CurrentRow.Cells("FixPrice").Value) > 0 Then
                                                .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value
                                            Else
                                                'If _IsGram = False Then
                                                '    'Calculate Price
                                                '    .CurrentRow.Cells("Amount").Value = (TempTK * (.CurrentRow.Cells("SalesRate").Value))
                                                'Else
                                                '    .CurrentRow.Cells("Amount").Value = objWSInfo.GoldPrice '((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * (.CurrentRow.Cells("SalesRate").Value)
                                                'End If
                                                .CurrentRow.Cells("Amount").Value = objWSInfo.GoldPrice
                                            End If
                                            If (optPayReturn.Checked) Then
                                                .CurrentRow.Cells("IsSale").Value = False
                                                .CurrentRow.Cells("IsReturn").Value = True
                                                .CurrentRow.Cells("IsShowForReturn").Value = True
                                            ElseIf (optSale.Checked) Then
                                                .CurrentRow.Cells("IsSale").Value = True
                                                .CurrentRow.Cells("IsReturn").Value = False
                                            ElseIf (optSaleReturn.Checked) Then
                                                .CurrentRow.Cells("IsSale").Value = True
                                                .CurrentRow.Cells("IsReturn").Value = True
                                                .CurrentRow.Cells("IsShowForReturn").Value = False
                                            End If
                                            Dim i As Integer = 1
                                            For Each drNo As DataRow In _dtWSReturnItem.Rows
                                                If drNo.RowState <> DataRowState.Deleted Then
                                                    drNo.Item("SNo") = i
                                                    i = i + 1
                                                End If
                                            Next

                                        End With
                                    End If
                                Else ' IsWholeSaleInvoice =false
                                    objCSInfo = objConsignmentSaleController.GetBarcodeByConsignmentSaleID(GetExistedItemscode(e.RowIndex), "  and C.ItemCode='" & _BarCode & "'", _CSInvoiceID)
                                    If (objCSInfo.ItemCode = "") Then
                                        MsgBox("Invalid Barcode No !", MsgBoxStyle.Information, AppName)
                                        grdItems.CurrentRow.Cells("ItemCode").Value = ""
                                        _IsUpdate = True
                                    Else
                                        With grdItems
                                            _IsUpdate = False
                                            .CurrentRow.Cells("ForSaleID").Value = objCSInfo.ForSaleID
                                            .CurrentRow.Cells("ItemCode").Value = objCSInfo.ItemCode
                                            .CurrentRow.Cells("ItemTG").Value = Format(objCSInfo.ItemTG, "#0.000")
                                            .CurrentRow.Cells("Gram").Value = Format(objCSInfo.ItemTG, "#0.000")
                                            .CurrentRow.Cells("SalesRate").Value = CInt(objCSInfo.SalesRate)
                                            .CurrentRow.Cells("ItemNameID").Value = objCSInfo.ItemNameID
                                            .CurrentRow.Cells("GoldQualityID").Value = objCSInfo.GoldQualityID
                                            .CurrentRow.Cells("ItemName").Value = _ItemNameController.GetItemName(objCSInfo.ItemNameID).ItemName
                                            .CurrentRow.Cells("GoldQuality").Value = _GoldQualityController.GetGoldQuality(objCSInfo.GoldQualityID).GoldQuality
                                            _IsGram = _GoldQualityController.GetGoldQuality(objCSInfo.GoldQualityID).IsGramRate
                                            .CurrentRow.Cells("ItemTG").Value = objCSInfo.ItemTG
                                            .CurrentRow.Cells("ItemTK").Value = objCSInfo.ItemTK

                                            .CurrentRow.Cells("GoldTG").Value = objCSInfo.GoldTG
                                            .CurrentRow.Cells("GoldTK").Value = objCSInfo.GoldTK
                                            .CurrentRow.Cells("WasteTG").Value = objCSInfo.WasteTG
                                            .CurrentRow.Cells("WasteTK").Value = objCSInfo.WasteTK
                                            .CurrentRow.Cells("GemsTG").Value = objCSInfo.GemsTG
                                            .CurrentRow.Cells("GemsTK").Value = objCSInfo.GemsTK

                                            'Waste
                                            _WasteTK = objCSInfo.WasteTK
                                            _WasteTG = objCSInfo.WasteTG
                                            Dim WasteWeight As New CommonInfo.GoldWeightInfo
                                            WasteWeight.GoldTK = _WasteTK
                                            WasteWeight = objConverterController.ConvertGoldTKToKPYC(WasteWeight)
                                            .CurrentRow.Cells("WasteK").Value = CStr(WasteWeight.WeightK)
                                            .CurrentRow.Cells("WasteP").Value = CStr(WasteWeight.WeightP)
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("WasteY").Value = Format(WasteWeight.WeightY + WasteWeight.WeightC, "0.0")
                                            Else
                                                .CurrentRow.Cells("WasteY").Value = Format(WasteWeight.WeightY + WasteWeight.WeightC, "0.00")
                                            End If

                                            'Item
                                            _ItemTK = objCSInfo.ItemTK
                                            _ItemTG = objCSInfo.ItemTG
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

                                            'Gold
                                            _GoldTK = objCSInfo.GoldTK
                                            _GoldTG = objCSInfo.GoldTG
                                            Dim GoldWeight As New CommonInfo.GoldWeightInfo
                                            GoldWeight.GoldTK = _GoldTK
                                            ItemWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
                                            .CurrentRow.Cells("GoldK").Value = CStr(GoldWeight.WeightK)
                                            .CurrentRow.Cells("GoldP").Value = CStr(GoldWeight.WeightP)
                                            If numberformat = 1 Then
                                                .CurrentRow.Cells("GoldY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
                                            Else
                                                .CurrentRow.Cells("GoldY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
                                            End If


                                            'Gem
                                            _GemsTK = objCSInfo.GemsTK
                                            _GemsTG = objCSInfo.GemsTG
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
                                            .CurrentRow.Cells("FixPrice").Value = objCSInfo.FixPrice
                                            If (.CurrentRow.Cells("FixPrice").Value) > 0 Then
                                                .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("FixPrice").Value
                                            Else
                                                'If Not IsDBNull(.CurrentRow.Cells("SalesRate").Value) Then
                                                '    If _IsGram = False Then
                                                '        'Calculate Price
                                                '        .CurrentRow.Cells("Amount").Value = (TempTK * (.CurrentRow.Cells("SalesRate").Value))
                                                '    Else
                                                '        .CurrentRow.Cells("Amount").Value = ((.CurrentRow.Cells("GoldTG").Value) + (.CurrentRow.Cells("WasteTG").Value)) * (.CurrentRow.Cells("SalesRate").Value)
                                                '    End If
                                                'End If
                                                .CurrentRow.Cells("Amount").Value = objCSInfo.GoldPrice
                                            End If

                                            'If Not IsDBNull(.CurrentRow.Cells("SalesRate").Value) Then
                                            '    '.CurrentRow.Cells("Amount").Value = CInt((.CurrentRow.Cells("Gram").Value) * (.CurrentRow.Cells("SalesRate").Value))
                                            '    If txtCommisionDis.Text <> 0 Then
                                            '        .CurrentRow.Cells("Amount").Value = CInt((.CurrentRow.Cells("Gram").Value) * (.CurrentRow.Cells("SalesRate").Value))
                                            '        .CurrentRow.Cells("Amount").Value = .CurrentRow.Cells("Amount").Value - (CInt(.CurrentRow.Cells("Amount").Value) * CInt(txtCommisionDis.Text) / 100)
                                            '    Else

                                            '        .CurrentRow.Cells("Amount").Value = CInt((.CurrentRow.Cells("Gram").Value) * (.CurrentRow.Cells("SalesRate").Value))
                                            '    End If

                                            'End If
                                            If (optPayReturn.Checked) Then
                                                .CurrentRow.Cells("IsSale").Value = False
                                                .CurrentRow.Cells("IsReturn").Value = True
                                                .CurrentRow.Cells("IsShowForReturn").Value = True
                                            ElseIf (optSale.Checked) Then
                                                .CurrentRow.Cells("IsSale").Value = True
                                                .CurrentRow.Cells("IsReturn").Value = False
                                            ElseIf (optSaleReturn.Checked) Then
                                                .CurrentRow.Cells("IsSale").Value = True
                                                .CurrentRow.Cells("IsReturn").Value = True
                                                .CurrentRow.Cells("IsShowForReturn").Value = False
                                            End If
                                            Dim i As Integer = 1

                                            For Each drNo As DataRow In _dtWSReturnItem.Rows
                                                If drNo.RowState <> DataRowState.Deleted Then
                                                    drNo.Item("SNo") = i
                                                    i = i + 1
                                                End If
                                            Next

                                        End With
                                    End If
                                End If ' IsWholeSaleInvoice=true

                                'End If
                            End If
                            'End If
                            'Next
                        End If
                    ElseIf grdItems.Columns(e.ColumnIndex).Name = "SalesRate" Then
                        If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("SalesRate").Value) Then
                            _IsGram = _GoldQualityController.GetGoldQuality(grdItems.CurrentRow.Cells("GoldQualityID").Value).IsGramRate
                            If (grdItems.CurrentRow.Cells("FixPrice").Value) > 0 Then
                                grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value
                            Else
                                If _IsGram = False Then
                                    'Calculate Price
                                    Dim Gold As New CommonInfo.GoldWeightInfo
                                    Dim _GoldPrice As Integer = 0
                                    Dim _TotalGoldPrice As Integer = 0
                                    Gold.WeightK = CInt(CInt(grdItems.CurrentRow.Cells("GoldK").Value) + CInt(grdItems.CurrentRow.Cells("WasteK").Value))
                                    Gold.WeightP = CInt(grdItems.CurrentRow.Cells("GoldP").Value) + CInt(grdItems.CurrentRow.Cells("WasteP").Value)
                                    Gold.WeightY = System.Decimal.Truncate(CDec(grdItems.CurrentRow.Cells("GoldY").Value) + CDec(grdItems.CurrentRow.Cells("WasteY").Value))
                                    Gold.WeightC = (CDec(grdItems.CurrentRow.Cells("GoldY").Value) + CDec(grdItems.CurrentRow.Cells("WasteY").Value)) - Gold.WeightY
                                    Gold.GoldTK = objConverterController.ConvertKPYCToGoldTK(Gold)
                                    TempTK = Gold.GoldTK

                                    grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("SalesRate").Value))
                                Else
                                    grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("SalesRate").Value)
                                End If
                            End If
                        End If

                    ElseIf grdItems.Columns(e.ColumnIndex).Name = "FixPrice" Then
                        If Not IsDBNull(grdItems.Rows(e.RowIndex).Cells("FixPrice").Value) Then
                            If (grdItems.CurrentRow.Cells("FixPrice").Value) > 0 Then
                                grdItems.CurrentRow.Cells("Amount").Value = grdItems.CurrentRow.Cells("FixPrice").Value
                            Else
                                If _IsGram = False Then
                                    'Calculate Price
                                    grdItems.CurrentRow.Cells("Amount").Value = (TempTK * (grdItems.CurrentRow.Cells("SalesRate").Value))
                                Else
                                    grdItems.CurrentRow.Cells("Amount").Value = ((grdItems.CurrentRow.Cells("GoldTG").Value) + (grdItems.CurrentRow.Cells("WasteTG").Value)) * (grdItems.CurrentRow.Cells("SalesRate").Value)
                                End If
                            End If

                        End If

                    End If

                    'End If
                End If
            End If
        Else
            MsgBox("Please choose payment type!", MsgBoxStyle.Information, "Data Require!")
            grdItems.CurrentRow.Cells("BarcodeNo").Value = ""
        End If


        CalculateGridTotalAmount()

    End Sub
    Private Sub grdItems_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdItems.RowsRemoved
        CalculateGridTotalAmount()
    End Sub
#Region "TextChange"

    Private Sub txtTotalAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAmt.TextChanged
        'If txtDiscountDis.Text = "" Then txtDiscountDis.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtAddSub.Text = "" Then txtAddSub.Text = "0"

        'If CInt(txtDiscountDis.Text) > 0 Then
        '    txtDiscountAmt.Text = CStr(CLng(txtTotalAmt.Text) * (CLng(txtDiscountDis.Text) / 100))
        'End If


        If txtTotalAmt.Text = "0" Then
            txtNetAmt.Text = 0
        Else
            txtNetAmt.Text = CStr((CLng(txtTotalAmt.Text) - CLng(txtDiscountAmt.Text)))

        End If

        'If txtTotalAmt.Text = "0" Then
        '    txtNetAmt.Text = 0
        'Else
        '    txtNetAmt.Text = CStr((CLng(txtTotalAmt.Text) + CLng(txtAddSub.Text)))
        '    '_TotalAmount = txtTotalAmt.Text
        'End If
    End Sub

    Private Sub txtNetAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtNetAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmt.TextChanged
        If txtNetAmt.Text = "" Then
            txtNetAmt.Text = 0
        End If
        If txtNetAmt.Text <> "" And txtTotalAmt.Text <> "" Then
            'txtAddSub.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtTotalAmt.Text))
            'txtPaidAmt.Text = txtNetAmt.Text
            txtAddSub.Text = CStr(CLng(CLng(txtNetAmt.Text) - CLng(txtTotalAmt.Text)) + CLng(txtDiscountAmt.Text))
            txtPaidAmt.Text = txtNetAmt.Text
        End If
    End Sub

    Private Sub txtPaidAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPaidAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaidAmt.TextChanged
        If txtPaidAmt.Text = "" Then
            txtPaidAmt.Text = 0
        Else
            If txtBalanceAmt.Text = "" Then
                txtBalanceAmt.Text = 0
            Else
                txtBalanceAmt.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text))
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



    Private Sub txtExchangeRate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPaidUSDAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtRefundKyats_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub



    Private Sub txtDiscountDis_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscountDis.TextChanged
        If txtDiscountDis.Text = "" Then txtDiscountDis.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"

        If txtDiscountDis.Text <> "" Then
            CalculateDisAmt()
        End If
    End Sub
    Private Sub CalculateDisAmt()
        If txtDiscountDis.Text = "" Then txtDiscountDis.Text = "0"

        If CInt(txtDiscountDis.Text) >= 0 Then
            txtDiscountAmt.Text = (CLng(txtTotalAmt.Text) * CDec(txtDiscountDis.Text)) / 100
            txtNetAmt.Text = CStr(CLng(txtTotalAmt.Text) - CLng(txtDiscountAmt.Text))
        End If

    End Sub


    Private Sub CalculateFinalAmount()
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"

        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"

        txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text) + CLng(txtAddSub.Text)) - (CLng(txtDiscountAmt.Text))), "###,##0.##")
        If Global_IsCash = True Then
            txtPaidAmt.Text = Format(Val(CLng(txtNetAmt.Text)), "###,##0.##")
        End If
        txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")

    End Sub




    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim UserInfo As New UserInfo
        Dim dt As New DataTable

        dt = objWSaleReturnController.GetWholeSaleReturnPrint(_WSReturnID)

        frmPrint.PrintWholeSaleReturn(dt)
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub


    Private Sub txtDiscountAmt_TextChanged(sender As Object, e As EventArgs) Handles txtDiscountAmt.TextChanged
        'CalculateFinalAmount()
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If CInt(txtDiscountAmt.Text) >= 0 Then

            txtNetAmt.Text = CStr(CLng(txtTotalAmt.Text) - CLng(txtDiscountAmt.Text))
        End If
    End Sub

    Private Sub optSaleReturn_CheckedChanged(sender As Object, e As EventArgs) Handles optSaleReturn.CheckedChanged
        If (optSaleReturn.Checked) Then
            If grdItems.Rows.Count <> -1 Then
                If _dtWSReturnItem.Rows.Count > 0 Then
                    For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                        _dtWSReturnItem.Rows(i).Item("IsSale") = True
                        _dtWSReturnItem.Rows(i).Item("IsReturn") = True
                    Next
                End If
            End If
        ElseIf (optPayReturn.Checked) Then
            If grdItems.Rows.Count <> -1 Then
                If _dtWSReturnItem.Rows.Count > 0 Then
                    For i As Integer = 0 To _dtWSReturnItem.Rows.Count - 1
                        _dtWSReturnItem.Rows(i).Item("IsSale") = True
                        _dtWSReturnItem.Rows(i).Item("IsReturn") = False
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("WholeSaleReturn")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class
