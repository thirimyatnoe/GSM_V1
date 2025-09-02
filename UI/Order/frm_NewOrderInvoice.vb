Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms

Public Class frm_NewOrderInvoice
    Implements IFormProcess

    Private _OrderInvoiceID As String = ""
    Private _GoldQuality As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _StaffController As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _GoldQualityController As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _ItemCategoryController As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _GemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _ConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _OrderInvoiceController As OrderInvoice.IOrderInvoiceController = Factory.Instance.CreateOrderInvoiceController
    Private _CurrentController As CurrentPrice.ICurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private _LocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _ConverterCon As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _CustomerController As Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _SalesItemCon As SalesItem.ISalesItemController = Factory.Instance.CreateSalesItemController
    Private _GoldSmith As GoldSmith.IGoldSmithController = Factory.Instance.CreateGoldSmithController

    Dim _dtOrderInvoiceItem As DataTable
    Private _OrderReceiveDetailID As String
    Private _OrderGemID As String
    Private _IsGram As Boolean = False
    Private _dtAllDiamond As DataTable
    Private _dtDetail As New DataTable
    Private _IsPayGram As Boolean = False
    Private _IsRowDelete As Boolean = False
    Private _IsRowUpdate As Boolean = False
    Dim itemid As String
    Dim _CustomerID As String = ""
    Dim _Customercode As String = ""
    Dim _LocationID As String
    Private _AllTotalAmount As Long = 0
    Private _IsUpdate As Boolean = False

    Dim _PayGoldTK As Decimal = 0.0
    Dim _PayGoldTG As Decimal = 0.0

    Dim _EstimateGoldTK As Decimal = 0.0
    Dim _EstimateGoldTG As Decimal = 0.0

    Dim _WasteTK As Decimal = 0.0
    Dim _WasteTG As Decimal = 0.0

    Dim _GemsTK As Decimal = 0.0
    Dim _GemsTG As Decimal = 0.0

    Dim _TotalTK As Decimal = 0.0
    Dim _TotalTG As Decimal = 0.0

    Dim _grdGemTK As Decimal = 0.0
    Dim _grdGemTG As Decimal = 0.0

    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
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

#Region " Load ComboBoxes "
    Private Sub GetcboStaff()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _StaffController.GetStaffList().DefaultView
        cboStaff.SelectedIndex = -1
    End Sub
    Private Sub GetcboGoldQuality()
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView
        cboGoldQuality.SelectedIndex = -1
    End Sub
    Private Sub GetAllGoldQuality()
        cboGoldQuality.ValueMember = "@GoldQualityID"
        cboGoldQuality.DisplayMember = "GoldQuality"
        cboGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView
    End Sub
    Private Sub GetcboPayGoldQuality()
        cboPayGoldQuality.ValueMember = "@GoldQualityID"
        cboPayGoldQuality.DisplayMember = "GoldQuality"
        cboPayGoldQuality.DataSource = _GoldQualityController.GetAllGoldQuality().DefaultView
        cboPayGoldQuality.SelectedIndex = -1
    End Sub
    Public Sub GetcboItemCategory()
        cboItemCategory.DisplayMember = "ItemCategory_"
        cboItemCategory.ValueMember = "@ItemCategoryID"
        cboItemCategory.DataSource = _ItemCategoryController.GetAllItemCategory().DefaultView
        cboItemCategory.SelectedIndex = -1
    End Sub

    Private Sub RefreshItemNameCbo(ByVal ItemID As String)
        Dim dt As New DataTable
        dt = _ItemName.GetItemNameListByItemCategory(ItemID)
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

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        Dim dtItem As DataTable
        For Each dr As DataRow In _dtDetail.Rows
            Dim _ORDetailID As String = ""
            _ORDetailID = dr.Item("OrderReceiveDetailID")
            dtItem = _SalesItemCon.GetSaleItemDataByOrderReceiveDetailID(_ORDetailID)
            If dtItem.Rows.Count > 0 Then
                MsgBox("Can not Delete this Voucher.This Item is Already Barcode!", MsgBoxStyle.Information, AppName)
                Return False
            End If
        Next

        If _OrderInvoiceController.DeleteOrderInvoice(_OrderInvoiceID) Then
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
        If IsFillData() Then
            Dim objOrderInvoice As New OrderInvoiceInfo
            objOrderInvoice = GetDataOrderInvoice()
            If _OrderInvoiceController.SaveOrderInvoiceReceive(objOrderInvoice, _dtDetail, _dtAllDiamond) Then
                _OrderInvoiceID = objOrderInvoice.OrderInvoiceID
                If chkCancel.Checked = False Then
                    If (MsgBox("Do You Want To Save And Print Order Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                        Dim frmPrint As New frm_ReportViewer
                        Dim dt As New DataTable
                        Dim dtGem As New DataTable
                        dt = _OrderInvoiceController.GetOrderReceivePrint(_OrderInvoiceID)
                        dtGem = _OrderInvoiceController.GetOrderForItemName(_OrderInvoiceID)
                        If dtGem.Rows.Count > 0 Then
                            frmPrint.OrderReceiveVoucherPrint(dt)
                            frmPrint.WindowState = FormWindowState.Maximized
                            frmPrint.Show()
                        Else
                            frmPrint.OrderReceiveItemName(dt)
                            frmPrint.WindowState = FormWindowState.Maximized
                            frmPrint.Show()
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
        End If

    End Function

    Private Sub SearchOrder_Click(sender As Object, e As EventArgs) Handles SearchOrder.Click
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objOrderInvoice As New OrderInvoiceInfo

        dt = _OrderInvoiceController.GetAllOrderReceiveHeader()
        DataItem = DirectCast(SearchData.FindFast(dt, "Order Receive List"), DataRow)

        If DataItem IsNot Nothing Then
            _OrderInvoiceID = DataItem.Item("VoucherNo").ToString()
            objOrderInvoice = _OrderInvoiceController.GetOrderInvoiceHeaderID(_OrderInvoiceID)

            _dtDetail.Rows.Clear()
            _dtOrderInvoiceItem.Rows.Clear()
            _dtAllDiamond.Rows.Clear()

            _dtDetail = _OrderInvoiceController.GetOrderReceiveDetail(_OrderInvoiceID)
            grdDetail.DataSource = _dtDetail

            _dtAllDiamond = _OrderInvoiceController.GetOrderInvoiceGemsItemHeaderID(_OrderInvoiceID)

            Dim dtTestStone1 As New DataTable
            dtTestStone1 = _dtAllDiamond

            ShowOrderInvoiceData(objOrderInvoice)

            btnDelete.Enabled = True
        Else
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString

        End If

    End Sub
#End Region

    Private Sub GetGoldSmith()
        cboGoldSmith.DisplayMember = "Name_"
        cboGoldSmith.ValueMember = "@GoldSmithID"
        cboGoldSmith.DataSource = _GoldSmith.GetAllGoldSmithList().DefaultView
        'cboGoldSmith.SelectedValue = "DEFAULT"

    End Sub


#Region " Private Methods "

    Private Sub OrderInvoiceGenerateFormat()
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo

        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.OrderStock.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtOrderInvoiceID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpOrderDate.Value)
        Else
            MsgBox("Please Fill the format for this form at Generate Format Form", MsgBoxStyle.Information, AppName)
        End If
    End Sub

    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _OrderInvoiceID = ""
        _CustomerID = ""
        _Customercode = ""
        OrderInvoiceGenerateFormat()
        dtpOrderDate.Value = Now
        dtpDueDate.Value = Now.Date
        cboStaff.SelectedIndex = -1
        cboGoldSmith.SelectedIndex = -1
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtCustomerCode.Text = ""
        _IsPayGram = False

        chkCancel.Checked = False
        chkCancel.Enabled = False
        _PayGoldTG = 0.0
        _PayGoldTK = 0.0
        cboPayGoldQuality.SelectedIndex = -1
        txtPayGoldG.Text = "0.0"
        txtPayGoldK.Text = "0"
        txtPayGoldP.Text = "0"
        txtPayGoldY.Text = "0.0"

        txtPayGoldK.ReadOnly = True
        txtPayGoldP.ReadOnly = True
        txtPayGoldY.ReadOnly = True
        txtPayGoldG.ReadOnly = True

        txtPayGoldG.TabStop = False
        txtPayGoldK.TabStop = False
        txtPayGoldP.TabStop = False
        txtPayGoldY.TabStop = False

        txtPayGoldK.BackColor = Color.Linen
        txtPayGoldP.BackColor = Color.Linen
        txtPayGoldY.BackColor = Color.Linen
        txtPayGoldG.BackColor = Color.Linen

        txtAllTotalAmt.Text = 0
        txtAllNetAmt.Text = 0
        txtAllAddSub.Text = 0
        txtAdvanceAmount.Text = 0
        dtpSecondAdvanceDate.Value = Now.Date
        txtSecondAdvanceAmount.Text = "0"
        txtBalanceAmt.Text = 0
        txtRemark.Clear()

        Dim dc As New DataColumn
        _dtDetail = New DataTable

        _dtDetail.Columns.Add("OrderInvoiceID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("OrderReceiveDetailID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ItemCategoryID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GoldSmithID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ItemCategory", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ItemNameID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("ItemName", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("OrderRate", System.Type.GetType("System.Int32"))
        _dtDetail.Columns.Add("Length", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("Width", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("GoldTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("GoldTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("WasteTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("WasteTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("TotalGemTK", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("TotalGemTG", System.Type.GetType("System.Decimal"))
        _dtDetail.Columns.Add("GoldPrice", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("GemPrice", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("DesignCharges", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("PlatingFee", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("WhiteCharges", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("MountingFee", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("TotalAmount", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("NetAmount", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("AddOrSub", System.Type.GetType("System.Int64"))
        _dtDetail.Columns.Add("Design", System.Type.GetType("System.String"))
        _dtDetail.Columns.Add("IsDiamond", System.Type.GetType("System.Boolean"))

        grdDetail.AutoGenerateColumns = False
        grdDetail.ReadOnly = True
        grdDetail.DataSource = _dtDetail

        FormatgrdDetail()

        txtPayGoldK.ReadOnly = True
        txtPayGoldP.ReadOnly = True
        txtPayGoldY.ReadOnly = True
        txtPayGoldG.ReadOnly = True

        txtPayGoldK.BackColor = Color.Linen
        txtPayGoldP.BackColor = Color.Linen
        txtPayGoldY.BackColor = Color.Linen
        txtPayGoldG.BackColor = Color.Linen


        _dtAllDiamond = New DataTable

        _dtAllDiamond.Columns.Add("OrderInvoiceGemsItemID", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("OrderReceiveDetailID", System.Type.GetType("System.String"))
        _dtAllDiamond.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
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
        If numberformat = 1 Then
            dc.DefaultValue = "0.0"
        Else
            dc.DefaultValue = "0.00"
        End If

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
        _dtAllDiamond.Columns.Add("IsCustomerGem", System.Type.GetType("System.Boolean"))

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtAllDiamond.Columns.Add(dc)
        ClearDetail()
    End Sub

    Private Sub FormatOrderInvoiceItem()
        With grdOrderGemsItem
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)

            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "OrderInvoiceGemsItemID"
            dclID.DataPropertyName = "OrderInvoiceGemsItemID"
            dclID.Name = "OrderInvoiceGemsItemID"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcPID As New DataGridViewTextBoxColumn()
            dcPID.HeaderText = "OrderInvoiceID"
            dcPID.DataPropertyName = "OrderInvoiceID"
            dcPID.Name = "OrderInvoiceID"
            dcPID.Visible = False
            .Columns.Add(dcPID)

            Dim dcOID As New DataGridViewTextBoxColumn()
            dcOID.HeaderText = "OrderReceiveDetailID"
            dcOID.DataPropertyName = "OrderReceiveDetailID"
            dcOID.Name = "OrderReceiveDetailID"
            dcOID.Visible = False
            .Columns.Add(dcOID)

            Dim dcID As New DataGridViewComboBoxColumn()
            dcID.HeaderText = "Category"
            dcID.DataPropertyName = "GemsCategoryID"
            dcID.Name = "GemsCategoryID"
            dcID.DataSource = _GemsCategoryController.GetAllGemsCategoryForGridCombo()
            dcID.DisplayMember = "GemsCategory"
            dcID.ValueMember = "@GemsCategoryID"
            dcID.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dcID.Width = 130
            dcID.Visible = True
            .Columns.Add(dcID)

            Dim dcName As New DataGridViewTextBoxColumn()
            dcName.HeaderText = "Description"
            dcName.DataPropertyName = "GemsName"
            dcName.Name = "GemsName"
            dcName.Width = 120
            dcName.Visible = True
            dcName.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcName.SortMode = DataGridViewColumnSortMode.NotSortable
            dcName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns.Add(dcName)

            Dim dc5 As New DataGridViewTextBoxColumn()
            dc5.HeaderText = "RBP"
            dc5.DataPropertyName = "YOrCOrG"
            dc5.Name = "YOrCOrG"
            dc5.Width = 58
            dc5.Visible = True
            dc5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc5.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc5)

            Dim dcGK As New DataGridViewTextBoxColumn()
            With dcGK
                .HeaderText = "K"
                .DataPropertyName = "GemsK"
                .Name = "GemsK"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Format = "0"
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
                If numberformat = 1 Then
                    .DefaultCellStyle.Format = "0.0"
                Else
                    .DefaultCellStyle.Format = "0.00"
                End If
                .MaxInputLength = 3
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcGY)
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
            dc6.DataPropertyName = "Qty"
            dc6.Name = "Qty"
            dc6.Width = 50
            dc6.Visible = True
            dc6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc6.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc6)

            Dim dc7 As New DataGridViewComboBoxColumn()
            dc7.HeaderText = "Type"
            dc7.DataPropertyName = "Type"
            dc7.Name = "Type"
            dc7.Visible = True
            dc7.Items.AddRange(New String() {"Fix", "ByWeight", "ByQty"})
            dc7.SortMode = DataGridViewColumnSortMode.NotSortable
            dc7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dc7.Width = 80
            .Columns.Add(dc7)

            Dim dc8 As New DataGridViewTextBoxColumn()
            dc8.HeaderText = "UnitPrice"
            dc8.DataPropertyName = "UnitPrice"
            dc8.Name = "UnitPrice"
            dc8.Width = 80
            dc8.Visible = True
            dc8.DefaultCellStyle.Format = "0"
            dc8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dc8.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc8)

            Dim dc9 As New DataGridViewTextBoxColumn()
            dc9.HeaderText = "Amount"
            dc9.DataPropertyName = "Amount"
            dc9.Name = "Amount"
            dc9.Visible = True
            dc9.ReadOnly = True
            dc9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dc9.Width = 80
            dc9.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc9)


            Dim dcchkShopGems As New DataGridViewCheckBoxColumn()
            dcchkShopGems.HeaderText = "CustomerGem"
            dcchkShopGems.DataPropertyName = "IsCustomerGem"
            dcchkShopGems.Name = "IsCustomerGem"
            dcchkShopGems.Width = 90
            dcchkShopGems.Visible = True
            dcchkShopGems.DefaultCellStyle.Font = New Font("Myanmar3", 10)
            dcchkShopGems.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcchkShopGems)
        End With
    End Sub

    Private Sub FormatgrdDetail()
        With grdDetail
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "OrderInvoiceID"
                .DataPropertyName = "OrderInvoiceID"
                .Name = "OrderInvoiceID"
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "OrderReceiveDetailID"
                .DataPropertyName = "OrderReceiveDetailID"
                .Name = "OrderReceiveDetailID"
                .Visible = False
            End With
            .Columns.Add(dcID)

            Dim dcItemCatID As New DataGridViewTextBoxColumn
            With dcItemCatID
                .HeaderText = "ItemCategoryID"
                .DataPropertyName = "ItemCategoryID"
                .Name = "ItemCategoryID"
                .Visible = False
            End With
            .Columns.Add(dcItemCatID)

            Dim dcGoldSmithID As New DataGridViewTextBoxColumn
            With dcGoldSmithID
                .HeaderText = "GoldSmithID"
                .DataPropertyName = "GoldSmithID"
                .Name = "GoldSmithID"
                .Visible = False
            End With
            .Columns.Add(dcGoldSmithID)

            Dim dcItemCategory As New DataGridViewTextBoxColumn
            With dcItemCategory
                .HeaderText = "ItemCategory"
                .DataPropertyName = "ItemCategory"
                .Name = "ItemCategory"
                .Width = 125
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = True
            End With
            .Columns.Add(dcItemCategory)

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
                .Width = 125
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = True
            End With
            .Columns.Add(dcItemName)

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
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = True
            End With
            .Columns.Add(dcGoldQuality)


            Dim dcCurrentPrice As New DataGridViewTextBoxColumn
            With dcCurrentPrice
                .HeaderText = "Rate"
                .DataPropertyName = "OrderRate"
                .Name = "OrderRate"
                .Width = 80
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
            End With
            .Columns.Add(dcCurrentPrice)

            Dim dcLength As New DataGridViewTextBoxColumn()
            With dcLength
                .HeaderText = "Length"
                .DataPropertyName = "Length"
                .Name = "Length"
                .Visible = False
            End With
            .Columns.Add(dcLength)

            Dim dcWidth As New DataGridViewTextBoxColumn()
            With dcWidth
                .HeaderText = "Width"
                .DataPropertyName = "Width"
                .Name = "Width"
                .Visible = False
            End With
            .Columns.Add(dcWidth)

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
                .HeaderText = "GemPrice"
                .DataPropertyName = "GemPrice"
                .Name = "GemPrice"
                .Visible = False
            End With
            .Columns.Add(dcDiamond)

            Dim dcPlatingFee As New DataGridViewTextBoxColumn
            With dcPlatingFee
                .HeaderText = "PlatingFee"
                .DataPropertyName = "PlatingFee"
                .Name = "PlatingFee"
                .Visible = False
            End With
            .Columns.Add(dcPlatingFee)

            Dim dcWhiteCharges As New DataGridViewTextBoxColumn
            With dcWhiteCharges
                .HeaderText = "WhiteCharges"
                .DataPropertyName = "WhiteCharges"
                .Name = "WhiteCharges"
                .Visible = False
            End With
            .Columns.Add(dcWhiteCharges)

            Dim dcMountingCharges As New DataGridViewTextBoxColumn
            With dcMountingCharges
                .HeaderText = "MountingCharges"
                .DataPropertyName = "MountingFee"
                .Name = "MountingFee"
                .Visible = False
            End With
            .Columns.Add(dcMountingCharges)

            Dim dcDesighCharges As New DataGridViewTextBoxColumn
            With dcDesighCharges
                .HeaderText = "DesignCharges"
                .DataPropertyName = "DesignCharges"
                .Name = "DesignCharges"
                .Visible = False
            End With
            .Columns.Add(dcDesighCharges)

            Dim dcEstimateGoldTK As New DataGridViewTextBoxColumn
            With dcEstimateGoldTK
                .HeaderText = "GoldTK"
                .DataPropertyName = "GoldTK"
                .Name = "GoldTK"
                .DefaultCellStyle.Format = "0.000"
                .Width = 90
                .Visible = False
            End With
            .Columns.Add(dcEstimateGoldTK)

            Dim dcEstimateGoldTG As New DataGridViewTextBoxColumn()
            With dcEstimateGoldTG
                .HeaderText = "GoldTG"
                .DataPropertyName = "GoldTG"
                .Name = "GoldTG"
                .DefaultCellStyle.Format = "0.000"
                .Width = 80
                .Visible = False
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcEstimateGoldTG)

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
                .HeaderText = "TotalGemTG"
                .DataPropertyName = "TotalGemTG"
                .Name = "TotalGemTG"
                .Visible = False
            End With
            .Columns.Add(dcGemTG)


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

            Dim dcNetAmount As New DataGridViewTextBoxColumn()
            With dcNetAmount
                .HeaderText = "NetAmount"
                .DataPropertyName = "NetAmount"
                .Name = "NetAmount"
                .Width = 80
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Visible = True
            End With
            .Columns.Add(dcNetAmount)


            Dim dcAddSub As New DataGridViewTextBoxColumn()
            With dcAddSub
                .HeaderText = "AddOrSub"
                .DataPropertyName = "AddOrSub"
                .Name = "AddOrSub"
                .Width = 90
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Visible = False
            End With
            .Columns.Add(dcAddSub)


            Dim dcBtn As New DataGridViewButtonColumn
            dcBtn.HeaderText = "..."
            dcBtn.Name = "AddStock"
            dcBtn.Visible = True
            dcBtn.UseColumnTextForButtonValue = True
            dcBtn.Text = "Add"
            dcBtn.Width = 35
            dcBtn.Resizable = DataGridViewTriState.False
            dcBtn.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcBtn)

            Dim dcDesign As New DataGridViewTextBoxColumn()
            With dcDesign
                .HeaderText = "Design"
                .DataPropertyName = "Design"
                .Name = "Design"
                .Visible = False
            End With
            .Columns.Add(dcDesign)

            Dim dcIsDiamond As New DataGridViewTextBoxColumn
            With dcIsDiamond
                .HeaderText = "IsDiamond"
                .DataPropertyName = "IsDiamond"
                .Name = "IsDiamond"
                .Visible = False
            End With
            .Columns.Add(dcIsDiamond)
        End With
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

        If _dtDetail.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        If _OrderInvoiceID <> "" Then
            For Each dr As DataRow In _dtDetail.Rows
                If dr.RowState = DataRowState.Deleted Then
                    Dim _ORDetailID As String = ""
                    Dim dtItem As New DataTable
                    _ORDetailID = dr.Item("OrderReceiveDetailID", DataRowVersion.Original)
                    dtItem = _SalesItemCon.GetSaleItemDataByOrderReceiveDetailID(_ORDetailID)
                    If dtItem.Rows.Count > 0 Then
                        MsgBox("Order Item Is Not Remove.This Item is Already Barcode!", MsgBoxStyle.Information, AppName)
                        Return False
                    End If
                End If
            Next
        End If
        Return True
    End Function
    Private Function GetDataOrderInvoice() As OrderInvoiceInfo
        GetDataOrderInvoice = New OrderInvoiceInfo
        With GetDataOrderInvoice
            .OrderInvoiceID = _OrderInvoiceID
            .OrderDate = dtpOrderDate.Value
            .DueDate = dtpDueDate.Value
            .StaffID = cboStaff.SelectedValue
            .CustomerID = _CustomerID
            .IsCancel = chkCancel.Checked
            If _PayGoldTK = 0 Then
                .PayGoldQualityID = "00"
            Else
                .PayGoldQualityID = cboPayGoldQuality.SelectedValue
            End If
            .PayGoldTK = _PayGoldTK
            .PayGoldTG = _PayGoldTG

            .AllTotalAmount = txtAllTotalAmt.Text
            .AllAddOrSub = txtAllAddSub.Text
            .AdvanceAmount = txtAdvanceAmount.Text
            If CInt(txtSecondAdvanceAmount.Text) > 0 Then
                .SecondAdvanceDate = dtpSecondAdvanceDate.Value
            Else
                .SecondAdvanceDate = Now.Date
            End If
            .SecondAdvanceAmount = txtSecondAdvanceAmount.Text
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
        End With
    End Function
    Private Sub ShowOrderInvoiceData(ByVal objOrderInvoice As OrderInvoiceInfo)

        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        Dim Weight As New GoldWeightInfo

        With objOrderInvoice
            dtpOrderDate.Value = .OrderDate
            txtOrderInvoiceID.Text = .OrderInvoiceID
            dtpDueDate.Value = .DueDate
            cboStaff.SelectedValue = .StaffID
            _CustomerID = .CustomerID
            txtCustomerCode.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerCode
            txtCustomer.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerName
            txtAddress.Text = _CustomerController.GetCustomerByID(_CustomerID).CustomerAddress
            chkCancel.Enabled = True
            chkCancel.Checked = .IsCancel
            cboPayGoldQuality.SelectedValue = .PayGoldQualityID
            _PayGoldTK = .PayGoldTK
            _PayGoldTG = .PayGoldTG
            Weight.GoldTK = _PayGoldTK
            Weight = _ConverterController.ConvertGoldTKToKPYC(Weight)
            txtPayGoldK.Text = CStr(Weight.WeightK)
            txtPayGoldP.Text = CStr(Weight.WeightP)
            If numberformat = 1 Then
                txtPayGoldY.Text = CStr(Format(Weight.WeightY + Weight.WeightC, "0.0"))
            Else
                txtPayGoldY.Text = CStr(Format(Weight.WeightY + Weight.WeightC, "0.00"))
            End If
            'txtPayGoldY.Text = CStr(Format(Weight.WeightY + Weight.WeightC, "0.0"))
            txtPayGoldG.Text = Format(.PayGoldTG, "0.000")
            txtRemark.Text = .Remark

            txtAllTotalAmt.Text = .AllTotalAmount
            txtAllAddSub.Text = .AllAddOrSub
            txtAllNetAmt.Text = CStr(CLng(txtAllTotalAmt.Text) + CLng(txtAllAddSub.Text))
            txtAdvanceAmount.Text = .AdvanceAmount
            If .SecondAdvanceAmount > 0 Then
                dtpSecondAdvanceDate.Value = .SecondAdvanceDate
            Else
                dtpSecondAdvanceDate.Value = Now.Date
            End If
            txtSecondAdvanceAmount.Text = .SecondAdvanceAmount
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(txtAdvanceAmount.Text) + CLng(txtSecondAdvanceAmount.Text)))
        End With
    End Sub

    'Private Sub ShowOrderInvoiceDetail(ByVal objOrderInvoice As OrderReceiveDetailInfo)
    '    ' MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
    '    Dim GoldWeight As New CommonInfo.GoldWeightInfo

    '    With objOrderInvoice
    '        cboItemCategory.SelectedValue = .ItemCategoryID
    '        cboItemName.SelectedValue = .ItemNameID
    '        cboGoldQuality.SelectedValue = .GoldQualityID
    '        txtWidth.Text = .Width
    '        txtLength.Text = .Length
    '        txtGemsPrice.Text = .GemPrice
    '        txtGoldPrice.Text = .GoldPrice
    '        txtDesignCharges.Text = .DesignCharges
    '        txtPlatingCharges.Text = .PlatingFee
    '        txtMountingCharges.Text = .MountingFee
    '        txtWhiteCharges.Text = .WhiteCharges

    '        GoldWeight.GoldTK = .GoldTK
    '        GoldWeight = objConverterController.ConvertGoldTKToKPYC(GoldWeight)
    '        txtItemForSaleK.Text = CStr(GoldWeight.WeightK)
    '        txtItemPForSale.Text = CStr(GoldWeight.WeightP)
    '        txtItemYForSale.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

    '        txtItemTGForSale.Text = Format(.ItemTG, "0.000")
    '        txtItemTKForSale.Text = Format(.ItemTK, "0.000")
    '        '  End If
    '        _ItemTKForSale = .ItemTK
    '        _ItemTGForSale = .ItemTG


    '        txtGoldK.Text = .GoldK
    '        txtGoldP.Text = .GoldP
    '        txtGoldY.Text = Format(.GoldY + .GoldC, "0.0")

    '        _GoldTK = .GoldTK
    '        _GoldTG = .GoldTG
    '        txtGoldG.Text = Format(.GoldTG, "0.000")

    '        txtWasteK.Text = .WasteK
    '        txtWasteP.Text = .WasteP
    '        txtWasteY.Text = Format(.WasteY + .WasteC, "0.0")

    '        _WasteTK = .WasteTK
    '        _WasteTG = .WasteTG
    '        txtWasteG.Text = Format(.GoldTG, "0.000")

    '        txtGemsK.Text = .TotalGemK
    '        txtGemsP.Text = .TotalGemP
    '        txtGemsY.Text = Format(.TotalGemY + .TotalGemC, "0.0")

    '        _TotalGemsTG = .TotalGemTG
    '        _TotalGemsTK = .TotalGemTK
    '        txtTotalG.Text = Format(.TotalGemTG, "0.000")

    '        txtTotalAmt.Text = .TotalAmount
    '        txtAddSub.Text = .AddOrSub
    '        txtNetAmt.Text = CStr(CLng(txtTotalAmt.Text) + CLng(txtAddSub.Text))

    '    End With


    'End Sub
#End Region

    Private Sub frm_NewOrderInvoice_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblLogInUserName.Text = Global_CurrentUser
        numberformat = Global_DecimalFormat
        lblCurrentLocationName.Text = Global_CurrentLocationName
        _LocationID = Global_CurrentLocationID
        GetcboGoldQuality()
        GetcboPayGoldQuality()
        GetcboStaff()
        GetcboItemCategory()
        GetGoldSmith()
        Clear()
        Me.KeyPreview = True
    End Sub

    Private Sub frm_NewOrderInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

#Region " Calculation"

    Private Sub CalculateGoldPrice()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim TempTK As Decimal = 0.0
        If txtCurrentPrice.Text = "" Then txtCurrentPrice.Text = "0"
        If txtGoldK.Text = "" Then txtGoldK.Text = "0"
        If txtGoldP.Text = "" Then txtGoldP.Text = "0"
        If txtGoldY.Text = "" Then txtGoldY.Text = "0.0"
        If txtGoldG.Text = "" Then txtGoldG.Text = "0.0"
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.0"
        If txtWasteG.Text = "" Then txtWasteK.Text = "0"

        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.0"
        If txtWasteG.Text = "" Then txtWasteG.Text = "0.0"

        GoldWeight.WeightK = CInt(txtGoldK.Text) + CInt(txtWasteK.Text)
        GoldWeight.WeightP = CInt(txtGoldP.Text) + CInt(txtWasteP.Text)
        GoldWeight.WeightY = System.Decimal.Truncate(CDec(txtGoldY.Text) + CDec(txtWasteY.Text))
        GoldWeight.WeightC = (CDec(txtGoldY.Text) + CDec(txtWasteY.Text)) - GoldWeight.WeightY
        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
        TempTK = GoldWeight.GoldTK

        If _IsGram = False Then
            txtGoldPrice.Text = CInt(TempTK * CLng(Val(txtCurrentPrice.Text)))
        Else
            txtGoldPrice.Text = CInt(CLng(txtCurrentPrice.Text) * (CDec(txtGoldG.Text) + CDec(txtWasteG.Text)))
        End If
        CalculateTotalAmount()
    End Sub
    Private Sub CalculateTotalAmount()
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        If txtGoldPrice.Text = "" Then txtGoldPrice.Text = "0"
        If txtGemsPrice.Text = "" Then txtGemsPrice.Text = "0"
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        If txtMountingCharges.Text = "" Then txtMountingCharges.Text = "0"
        If txtWhiteCharges.Text = "" Then txtWhiteCharges.Text = "0"

        txtTotalAmt.Text = CStr(CLng(txtGoldPrice.Text) + CLng(txtGemsPrice.Text)) + CLng(txtDesignCharges.Text) + CLng(txtPlatingCharges.Text) + CLng(txtMountingCharges.Text) + CLng(txtWhiteCharges.Text)
        txtNetAmt.Text = txtTotalAmt.Text
        txtAddSub.Text = "0"
    End Sub
    Private Sub CalculategrdTotalAmount()

        Dim grdtotalAmt As Long = 0
        For i As Integer = 0 To grdOrderGemsItem.RowCount - 1
            If Not grdOrderGemsItem.Rows(i).IsNewRow Then
                grdtotalAmt += CDec(Val(grdOrderGemsItem.Rows(i).Cells("Amount").FormattedValue))
            End If
        Next
        txtGemsPrice.Text = CStr(grdtotalAmt)
        CalculateTotalAmount()
    End Sub
    Private Sub CalculategrAlldTotalAmount()
        _AllTotalAmount = 0

        For j As Integer = 0 To grdDetail.RowCount - 1
            If Not grdDetail.Rows(j).IsNewRow Then
                _AllTotalAmount += CDec(grdDetail.Rows(j).Cells("NetAmount").FormattedValue)
            End If
        Next
        txtAllTotalAmt.Text = _AllTotalAmount

    End Sub
    Private Sub CalculateAllDetailTotalAmount()
        Dim grdTotalAmt As Long = 0

        For i As Integer = 0 To grdDetail.RowCount - 1
            grdTotalAmt += CLng(Val(grdDetail.Rows(i).Cells("NetAmount").FormattedValue))
        Next
        txtAllTotalAmt.Text = grdTotalAmt
        txtAllNetAmt.Text = txtAllTotalAmt.Text
        txtAllAddSub.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalAmt.Text))
        txtAdvanceAmount.Text = txtAllNetAmt.Text
        txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(Val(txtAdvanceAmount.Text)) + CLng(Val(txtSecondAdvanceAmount.Text))))
    End Sub

    Private Sub CalculateEstimateForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtGoldG.Text = "" Then txtGoldG.Text = "0.0"

        If Val(txtGoldG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtGoldG.Text)
            _EstimateGoldTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _EstimateGoldTK = GoldWeight.GoldTK

            GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            txtGoldK.Text = CStr(GoldWeight.WeightK)
            txtGoldP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

        Else
            _EstimateGoldTG = 0.0
            _EstimateGoldTK = 0.0

            txtGoldK.Text = "0"
            txtGoldP.Text = "0"
            txtGoldY.Text = "0.0"
            'txtGoldG.Text = "0.0"
        End If
    End Sub
    Private Sub CalculatePayForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtPayGoldG.Text = "" Then txtPayGoldG.Text = "0.0"

        If Val(txtPayGoldG.Text) > 0 And _IsPayGram = True Then
            GoldWeight.Gram = CDec(txtPayGoldG.Text)
            _PayGoldTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _PayGoldTK = GoldWeight.GoldTK

            GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            txtPayGoldK.Text = CStr(GoldWeight.WeightK)
            txtPayGoldP.Text = CStr(GoldWeight.WeightP)
            txtPayGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
        Else
            _PayGoldTG = 0.0
            _PayGoldTK = 0.0

            txtPayGoldK.Text = "0"
            txtPayGoldP.Text = "0"
            txtPayGoldY.Text = "0.0"
            'txtPayGoldG.Text = "0.0"
        End If
    End Sub
    Private Sub CalculatePayForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtPayGoldK.Text = "" Then txtPayGoldK.Text = "0"
        If txtPayGoldP.Text = "" Then txtPayGoldP.Text = "0"
        If txtPayGoldY.Text = "" Then txtPayGoldY.Text = "0.0"

        If (Val(txtPayGoldK.Text) > 0 Or Val(txtPayGoldP.Text) > 0 Or Val(txtPayGoldY.Text) > 0) And _IsPayGram = False Then
            GoldWeight.WeightK = CInt(txtPayGoldK.Text)
            GoldWeight.WeightP = CInt(txtPayGoldP.Text)
            GoldWeight.WeightY = CDec(txtPayGoldY.Text)
            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _PayGoldTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _PayGoldTG = GoldWeight.Gram
            txtPayGoldG.Text = Format(_PayGoldTG, "0.000")
        Else
            _PayGoldTG = 0.0
            _PayGoldTK = 0.0
            txtPayGoldG.Text = "0.0"

            'txtPayGoldK.Text = "0"
            'txtPayGoldP.Text = "0"
            'txtPayGoldY.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateWasteForKPY()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"
        If txtWasteP.Text = "" Then txtWasteP.Text = "0"
        If txtWasteY.Text = "" Then txtWasteY.Text = "0.0"

        If (Val(txtWasteK.Text) > 0 Or Val(txtWasteP.Text) > 0 Or Val(txtWasteY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtWasteK.Text)
            GoldWeight.WeightP = CInt(txtWasteP.Text)
            GoldWeight.WeightY = CDec(txtWasteY.Text)
            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _WasteTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _WasteTG = GoldWeight.Gram
            txtWasteG.Text = Format(_WasteTG, "0.000")
        Else
            _WasteTG = 0.0
            _WasteTK = 0.0
            txtWasteG.Text = "0.0"
            'txtWasteK.Text = "0"
            'txtWasteP.Text = "0"
            'txtWasteY.Text = "0.0"
        End If
    End Sub

    Private Sub CalculateWasteForGram()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If txtWasteG.Text = "" Then txtWasteG.Text = "0.0"

        If Val(txtWasteG.Text) > 0 And _IsGram = True Then
            GoldWeight.Gram = CDec(txtWasteG.Text)
            _WasteTG = GoldWeight.Gram
            GoldWeight.GoldTK = GoldWeight.Gram / Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _WasteTK = GoldWeight.GoldTK

            GoldWeight = _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
            txtWasteK.Text = CStr(GoldWeight.WeightK)
            txtWasteP.Text = CStr(GoldWeight.WeightP)
            txtWasteY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))

        Else
            _WasteTG = 0.0
            _WasteTK = 0.0

            txtWasteK.Text = "0"
            txtWasteP.Text = "0"
            txtWasteY.Text = "0.0"
            'txtWasteG.Text = "0.0"
        End If
    End Sub
    Private Sub CalculateEstimateForKPY()
        If txtGoldK.Text = "" Then txtGoldK.Text = "0"
        If txtGoldP.Text = "" Then txtGoldP.Text = "0"
        If txtGoldY.Text = "" Then txtGoldY.Text = "0.0"

        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        If (Val(txtGoldK.Text) > 0 Or Val(txtGoldP.Text) > 0 Or Val(txtGoldY.Text) > 0) And _IsGram = False Then
            GoldWeight.WeightK = CInt(txtGoldK.Text)
            GoldWeight.WeightP = CInt(txtGoldP.Text)
            GoldWeight.WeightY = CDec(txtGoldY.Text)
            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _EstimateGoldTK = GoldWeight.GoldTK
            GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            _EstimateGoldTG = GoldWeight.Gram
            txtGoldG.Text = Format(_EstimateGoldTG, "0.000")
        Else
            _EstimateGoldTG = 0.0
            _EstimateGoldTK = 0.0
            txtGoldG.Text = "0.0"
            'txtGoldK.Text = "0"
            'txtGoldP.Text = "0"
            'txtGoldY.Text = "0.0"
        End If
    End Sub

    Private Sub CalculategrdOrderGemsItemGemsItem()
        Dim GemWeight As New CommonInfo.GoldWeightInfo
        Dim TotalWeight As New CommonInfo.GoldWeightInfo
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        For i As Integer = 0 To grdOrderGemsItem.RowCount - 1
            If Not grdOrderGemsItem.Rows(i).IsNewRow Then
                GemWeight.WeightK = CInt(Val(grdOrderGemsItem.Rows(i).Cells("GemsK").FormattedValue))
                GemWeight.WeightP = CInt(Val(grdOrderGemsItem.Rows(i).Cells("GemsP").FormattedValue))
                GemWeight.WeightY = CDec(Val(grdOrderGemsItem.Rows(i).Cells("GemsY").FormattedValue))

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
        If numberformat = 1 Then
            txtGemsY.Text = Format(TotalWeight.WeightY, "0.0")
        Else
            txtGemsY.Text = Format(TotalWeight.WeightY, "0.00")
        End If

        TotalWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(TotalWeight)
        _GemsTK = TotalWeight.GoldTK
        TotalWeight.Gram = TotalWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
        _GemsTG = TotalWeight.Gram
        If _GemsTG <> 0 Then
            txtGemsG.Text = Format(_GemsTG, "0.000")
        Else
            txtGemsG.Text = "0.0"
        End If
    End Sub

    Private Sub CalculateTotalWeight()
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0

        If _EstimateGoldTG <> 0.0 Or _GemsTG <> 0.0 Then
            Dim GoldWeight As New CommonInfo.GoldWeightInfo
            Dim GemWeight As New CommonInfo.GoldWeightInfo
            Dim TotalWeight As New CommonInfo.GoldWeightInfo
            GoldWeight.WeightK = CInt(txtGoldK.Text)
            GoldWeight.WeightP = CInt(txtGoldP.Text)
            GoldWeight.WeightY = CDec(txtGoldY.Text)

            GemWeight.WeightK = CInt(txtGemsK.Text)
            GemWeight.WeightP = CInt(txtGemsP.Text)
            GemWeight.WeightY = CDec(txtGemsY.Text)

            weightY = GoldWeight.WeightY + GemWeight.WeightY
            If weightY >= Global_PToY Then
                weightP = 1
                weightY = weightY - Global_PToY
            End If

            weightP += GoldWeight.WeightP + GemWeight.WeightP
            If weightP >= 16 Then
                weightK = 1
                weightP = weightP - 16
            End If

            weightK += GoldWeight.WeightK + GemWeight.WeightK

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

            GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
            _TotalTK = GoldWeight.GoldTK
            'GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterCon.GetMeasurement("Kyat", "Gram"))
            '_TotalTG = GoldWeight.Gram

            _TotalTG = CDec(txtGoldG.Text) + CDec(txtGemsG.Text)
            txtTotalG.Text = Format((CDec(txtGoldG.Text) + CDec(txtGemsG.Text)), "0.000")
        Else
            _TotalTG = 0.0
            _TotalTG = 0.0
            txtTotalK.Text = "0"
            txtTotalP.Text = "0"
            txtTotalY.Text = "0.0"
            txtTotalG.Text = "0.0"
        End If
    End Sub

#End Region


    Private Sub LnkPayGold_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LnkPayGold.LinkClicked
        Dim frm As New frm_ToWeight
        Dim GoldWeight As New GoldWeightInfo
        frm.ShowDialog()
        GoldWeight = frm._GoldWeightInfo
        If _IsPayGram = False Then
            txtPayGoldK.Text = CStr(GoldWeight.WeightK)
            txtPayGoldP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtPayGoldY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
            Else
                txtPayGoldY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
            End If
            'txtPayGoldY.Text = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
        Else
            txtPayGoldG.Text = Format(GoldWeight.Gram, "0.000")
        End If
        '_PayGoldTG = GoldWeight.Gram
        '_PayGoldTK = GoldWeight.GoldTK
    End Sub
    'Private Sub btnPrintDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim frmPrint As New frm_ReportViewer
    '    Dim dt As New DataTable
    '    dt = _OrderInvoiceController.GetOrderInvoicePrint(_OrderInvoiceID)
    '    frmPrint.PrintOrderDone(dt)
    '    frmPrint.WindowState = FormWindowState.Maximized
    '    frmPrint.Show()
    'End Sub
    Private Sub chkChangedForCancel()
        If chkCancel.Checked = True Then
            dtpOrderDate.Enabled = False
            cboStaff.Enabled = False
            btnCustomer.Enabled = False
            txtCustomerCode.Enabled = False
            txtCustomer.Enabled = False
            txtAddress.Enabled = False
            txtRemark.Enabled = False
            cboItemName.Enabled = False
            txtLength.Enabled = False
            txtWidth.Enabled = False
            'txtQty.Enabled = False
            cboGoldQuality.Enabled = False
            txtCurrentPrice.Enabled = False
            cboPayGoldQuality.Enabled = False
            txtPayGoldG.Enabled = False
            txtGoldG.Enabled = False
            txtGemsG.Enabled = False
            txtWasteG.Enabled = False
            txtTotalG.Enabled = False

            txtPayGoldK.Enabled = False
            txtPayGoldP.Enabled = False
            txtPayGoldY.Enabled = False

            txtGoldK.Enabled = False
            txtGoldP.Enabled = False
            txtGoldY.Enabled = False

            txtGemsK.Enabled = False
            txtGemsP.Enabled = False
            txtGemsY.Enabled = False
            txtGemsC.Enabled = False

            txtWasteK.Enabled = False
            txtWasteP.Enabled = False
            txtWasteY.Enabled = False
            txtWasteC.Enabled = False

            txtTotalK.Enabled = False
            txtTotalP.Enabled = False
            txtTotalY.Enabled = False
            txtTotalC.Enabled = False
            txtGoldPrice.Enabled = False
            txtGemsPrice.Enabled = False
            txtDesignCharges.Enabled = False
            txtAllNetAmt.Enabled = False
            txtAllAddSub.Enabled = False
            txtAllTotalAmt.Enabled = False
            txtAdvanceAmount.Enabled = False
            dtpSecondAdvanceDate.Enabled = False
            txtSecondAdvanceAmount.Enabled = False
            txtBalanceAmt.Enabled = False
            LnkPayGold.Enabled = False
            'LnkWaste.Enabled = False
            'LnkEstimateGold.Enabled = False
            grdOrderGemsItem.Enabled = False
            btnPrint.Enabled = False

        Else
            dtpOrderDate.Enabled = True
            cboStaff.Enabled = True
            btnCustomer.Enabled = True
            txtCustomerCode.Enabled = True
            txtCustomer.Enabled = True
            txtAddress.Enabled = True
            txtRemark.Enabled = True
            cboItemName.Enabled = True
            txtLength.Enabled = True
            txtWidth.Enabled = True
            'txtQty.Enabled = True
            cboGoldQuality.Enabled = True
            txtCurrentPrice.Enabled = True
            cboPayGoldQuality.Enabled = True
            txtPayGoldG.Enabled = True
            txtGoldG.Enabled = True
            txtGemsG.Enabled = True
            txtWasteG.Enabled = True
            txtTotalG.Enabled = True

            txtPayGoldK.Enabled = True
            txtPayGoldP.Enabled = True
            txtPayGoldY.Enabled = True

            txtGoldK.Enabled = True
            txtGoldP.Enabled = True
            txtGoldY.Enabled = True

            txtGemsK.Enabled = True
            txtGemsP.Enabled = True
            txtGemsY.Enabled = True
            txtGemsC.Enabled = True

            txtWasteK.Enabled = True
            txtWasteP.Enabled = True
            txtWasteY.Enabled = True
            txtWasteC.Enabled = True

            txtTotalK.Enabled = True
            txtTotalP.Enabled = True
            txtTotalY.Enabled = True
            txtTotalC.Enabled = True
            txtGoldPrice.Enabled = True
            txtGemsPrice.Enabled = True
            txtDesignCharges.Enabled = True
            txtAllNetAmt.Enabled = True
            txtAllAddSub.Enabled = True
            txtAllTotalAmt.Enabled = True
            txtAdvanceAmount.Enabled = True
            dtpSecondAdvanceDate.Enabled = True
            txtSecondAdvanceAmount.Enabled = True
            txtBalanceAmt.Enabled = True
            LnkPayGold.Enabled = True
            'LnkWaste.Enabled = True
            'LnkEstimateGold.Enabled = True
            grdOrderGemsItem.Enabled = True
            btnPrint.Enabled = True
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm As New frm_CustomerShow
        Dim ObjCustomer As CustomerInfo
        If _CustomerID <> "0" Then
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
    Private Sub cboStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub
    Private Sub cboStaff_Click(sender As Object, e As EventArgs) Handles cboStaff.Click
        GetcboStaff()
    End Sub

    Private Sub cboGoldQuality_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboGoldQuality.SelectedValueChanged
        Dim objCurrentPrice As New CommonInfo.CurrentPriceInfo
        If cboGoldQuality.Text <> "" Then
            Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
            GoldQualityInfo = _GoldQuality.GetGoldQuality(cboGoldQuality.SelectedValue)
            If GoldQualityInfo.IsGramRate Then
                lblIsGram.Text = "၁ ဂရမ်စျေး"
            Else
                lblIsGram.Text = "၁ ကျပ်သာစျေး"
            End If
            _IsGram = GoldQualityInfo.IsGramRate
            GoldQualityForTextChange()
            objCurrentPrice = _CurrentController.GetCurrentPriceByGoldID(cboGoldQuality.SelectedValue)
            txtCurrentPrice.Text = objCurrentPrice.SalesRate.ToString
        Else
            txtGoldK.ReadOnly = True
            txtGoldP.ReadOnly = True
            txtGoldY.ReadOnly = True
            txtGoldG.ReadOnly = True

            txtGoldG.BackColor = Color.Linen
            txtGoldK.BackColor = Color.Linen
            txtGoldP.BackColor = Color.Linen
            txtGoldY.BackColor = Color.Linen

            txtWasteG.BackColor = Color.Linen
            txtWasteK.BackColor = Color.Linen
            txtWasteP.BackColor = Color.Linen
            txtWasteY.BackColor = Color.Linen

            txtWasteK.ReadOnly = True
            txtWasteP.ReadOnly = True
            txtWasteY.ReadOnly = True
            txtWasteG.ReadOnly = True
            txtCurrentPrice.Text = "0"
            lblIsGram.Text = ""
            _IsGram = False
        End If
        CalculateGoldPrice()
    End Sub

    Private Sub txtCustomerCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub cboPayGoldQuality_Click(sender As Object, e As EventArgs) Handles cboPayGoldQuality.Click
        GetcboPayGoldQuality()
    End Sub

    Private Sub cboPayGoldQuality_KeyUp(sender As Object, e As KeyEventArgs) Handles cboPayGoldQuality.KeyUp
        AutoCompleteCombo_KeyUp(cboPayGoldQuality, e)
    End Sub

    Private Sub cboPayGoldQuality_Leave(sender As Object, e As EventArgs) Handles cboPayGoldQuality.Leave
        AutoCompleteCombo_Leave(cboPayGoldQuality, "")
    End Sub

    Private Sub cboPayGoldQuality_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboPayGoldQuality.SelectedValueChanged
        Dim objCurrentPrice As New CommonInfo.CurrentPriceInfo
        If cboPayGoldQuality.Text <> "" Then
            Dim GoldQualityInfo As New CommonInfo.GoldQualityInfo
            GoldQualityInfo = _GoldQuality.GetGoldQuality(cboPayGoldQuality.SelectedValue)
            _IsPayGram = GoldQualityInfo.IsGramRate
            PayGoldQualityForTextChange()
        Else
            txtPayGoldK.ReadOnly = True
            txtPayGoldP.ReadOnly = True
            txtPayGoldY.ReadOnly = True
            txtPayGoldG.ReadOnly = True

            txtPayGoldG.TabStop = False
            txtPayGoldK.TabStop = False
            txtPayGoldP.TabStop = False
            txtPayGoldY.TabStop = False

            txtPayGoldK.BackColor = Color.Linen
            txtPayGoldP.BackColor = Color.Linen
            txtPayGoldY.BackColor = Color.Linen
            txtPayGoldG.BackColor = Color.Linen
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim dtGem As New DataTable
        dt = _OrderInvoiceController.GetOrderReceivePrint(_OrderInvoiceID)
        dtGem = _OrderInvoiceController.GetOrderForItemName(_OrderInvoiceID)

        If dtGem.Rows.Count > 0 Then
            frmPrint.OrderReceiveVoucherPrint(dt)
            frmPrint.WindowState = FormWindowState.Maximized
            frmPrint.Show()
        Else
            If dt.Rows.Count = 1 Then
                frmPrint.OrderReceiveVoucherPrint(dt)
                frmPrint.WindowState = FormWindowState.Maximized
                frmPrint.Show()
            Else
                frmPrint.OrderReceiveItemName(dt)
                frmPrint.WindowState = FormWindowState.Maximized
                frmPrint.Show()
            End If

        End If
    End Sub

    Private Sub grdOrderGemsItem_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderGemsItem.CellValidated

        If grdOrderGemsItem.IsCurrentCellInEditMode = False Then Exit Sub
        If (e.RowIndex <> -1) Then
            Select Case grdOrderGemsItem.Columns(e.ColumnIndex).Name
                Case "UnitPrice", "Type", "Qty", "GemsK", "GemsP", "GemsY", "YOrCOrG"

                    If grdOrderGemsItem.Rows(e.RowIndex).Cells("IsCustomerGem").Value = False Then
                        If IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value) Then
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value = "0"
                        End If

                        If IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("Qty").Value) Then
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("Qty").Value = "0"
                        End If

                        If Not IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("Type").Value) Then
                            If grdOrderGemsItem.Rows(e.RowIndex).Cells("Type").Value = "Fix" Then
                                grdOrderGemsItem.Rows(e.RowIndex).Cells("Amount").Value = grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value

                            ElseIf grdOrderGemsItem.Rows(e.RowIndex).Cells("Type").Value = "ByWeight" Then
                                Dim _Type As Boolean = False
                                If (IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value)) Then
                                    _Type = True
                                ElseIf grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0" Then
                                    _Type = True
                                Else
                                    _Type = False
                                End If

                                If _Type = False Then
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("Amount").Value = IIf(IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value) = True, 0, grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value) * CLng(grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value)
                                Else
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("Amount").Value = IIf(IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTK").Value) = True, 0, grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTK").Value) * CLng(grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value)
                                End If

                            ElseIf grdOrderGemsItem.Rows(e.RowIndex).Cells("Type").Value = "ByQty" Then
                                grdOrderGemsItem.Rows(e.RowIndex).Cells("Amount").Value = CLng(grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value) * CInt(grdOrderGemsItem.Rows(e.RowIndex).Cells("Qty").Value)
                            End If
                        Else
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("Amount").Value = "0"
                        End If
                    Else
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value = "0"
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("Amount").Value = "0"
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("Type").Value = ""
                    End If
            End Select
            CalculategrdTotalAmount()
        End If
    End Sub

    Private Sub grdOrderGemsItem_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderGemsItem.CellValueChanged
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim grdOrderGemsItemTK As Decimal = 0.0
        Dim grdOrderGemsItemTG As Decimal = 0.0
        If grdOrderGemsItem.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then

            If (grdOrderGemsItem.Columns(e.ColumnIndex).Name = "GemsK" Or grdOrderGemsItem.Columns(e.ColumnIndex).Name = "GemsP" Or grdOrderGemsItem.Columns(e.ColumnIndex).Name = "GemsY") Then

                With grdOrderGemsItem
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

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _grdGemTK = GoldWeight.GoldTK
                        GoldWeight.Gram = _grdGemTK * Global_KyatToGram
                        _grdGemTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("GemsTG").Value() = _grdGemTG
                        .Rows(e.RowIndex).Cells("GemsTK").Value() = _grdGemTK
                    End If
                End With
            End If 'end Gemskpyc

            If grdOrderGemsItem.Columns(e.ColumnIndex).Name = "YOrCOrG" Then  'For GemsWeight Yati,B,Karat

                Dim equivalent As Decimal
                Dim VarWeight As String
                Dim VarWeightY As Integer
                Dim VarWeightBCG As Decimal
                Dim VarWeightP As Decimal
                Dim TP As Decimal
                Dim TY As Decimal
                Dim TC As Decimal

                Dim IsValid As Boolean
                If IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value) Then
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                End If

                VarWeight = CStr(grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value)
                If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
                    MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"

                Else
                    If VarWeight.EndsWith("ct") Then
                        If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                            VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                            '' grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG * 1.1) ' Notes: For Karat,multiply 1.1 
                            TC = CStr(VarWeightBCG)
                            If Global_IsCarat = 0 Or Global_IsCarat = 2 Then
                                grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG)
                            Else
                                grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG * 1.1)
                            End If

                            IsValid = True
                        Else
                            IsValid = False
                        End If

                    ElseIf VarWeight.EndsWith("R") Then
                        If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                            If VarWeight.IndexOf(".") = -1 Then
                                VarWeightY = CInt(LSet(VarWeight, Len(VarWeight) - 1))
                                '' grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightY
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = VarWeightY / equivalent
                                If Global_IsCarat = 2 Then
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                Else
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightY
                                End If
                                IsValid = True
                            Else
                                VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = VarWeight / equivalent
                                If Global_IsCarat = 2 Then ' If Global_IsCarat = True Then
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                Else
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeight
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
                                    '' grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                '' grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightBCG
                                equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                TY = VarWeightY + (VarWeightBCG / equivalent)
                                equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                TC = TY / equivalent
                                If Global_IsCarat = 2 Then
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                Else
                                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                    ''grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                    '' grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                    ''grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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

                    If Not IsValid And grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value <> "0" Then
                        MsgBox("Stone Weight is Invalid!", MsgBoxStyle.Information, "Invalid Data")
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                    End If

                    equivalent = Global_GramToKarat '_ConverterCon.GetMeasurement("Gram", "Karat")
                    Dim gram As Decimal = TC / equivalent
                    equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
                    GoldWeight.GoldTK = gram / equivalent
                    _ConverterCon.ConvertGoldTKToKPYC(GoldWeight)
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsK").Value = GoldWeight.WeightK
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsP").Value = GoldWeight.WeightP
                    If numberformat = 1 Then
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0")
                    Else
                        grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsY").Value = Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00")
                    End If
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTK").Value = gram / equivalent
                    grdOrderGemsItem.Rows(e.RowIndex).Cells("GemsTG").Value = gram
                End If
            End If
        End If
        CalculategrdOrderGemsItemGemsItem()
        CalculateTotalWeight()
    End Sub

    Private Sub grdOrderGemsItem_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdOrderGemsItem.RowsRemoved
        If (grdOrderGemsItem.RowCount > 0) Then
            CalculategrdOrderGemsItemGemsItem()
            CalculategrdTotalAmount()
            CalculateTotalWeight()
            'CalculatePayGoldWeight()
        End If
    End Sub
    Private Sub grdOrderGemsItem_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdOrderGemsItem.CellContentClick

        If (e.RowIndex <> -1) Then
            Select Case grdOrderGemsItem.Columns(e.ColumnIndex).Name
                Case "IsCustomerGem"
                    If Not IsDBNull(grdOrderGemsItem.Rows(e.RowIndex).Cells("IsCustomerGem").Value) Then
                        If grdOrderGemsItem.Rows(e.RowIndex).Cells("IsCustomerGem").Value = True Then
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("IsCustomerGem").Value = False

                        ElseIf grdOrderGemsItem.Rows(e.RowIndex).Cells("IsCustomerGem").Value = False Then
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("IsCustomerGem").Value = True
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("Type").Value = ""
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("UnitPrice").Value = "0"
                            grdOrderGemsItem.Rows(e.RowIndex).Cells("Amount").Value = "0"
                        End If
                    End If
            End Select
        End If
        '_dtOrderInvoiceItem = grdOrderGemsItem.DataSource
        CalculategrdTotalAmount()
    End Sub
#Region "TextChange"

    Private Sub GoldQualityForTextChange()

        If _IsGram = True Then
            txtGoldK.ReadOnly = True
            txtGoldP.ReadOnly = True
            txtGoldY.ReadOnly = True
            txtGoldG.ReadOnly = False

            txtWasteG.TabStop = True
            txtGoldG.TabStop = True

            txtWasteK.TabStop = False
            txtWasteP.TabStop = False
            txtWasteY.TabStop = False
            txtGoldK.TabStop = False
            txtGoldP.TabStop = False
            txtGoldY.TabStop = False

            txtGoldK.BackColor = Color.Linen
            txtGoldP.BackColor = Color.Linen
            txtGoldY.BackColor = Color.Linen
            txtGoldG.BackColor = Color.White

            txtWasteK.ReadOnly = True
            txtWasteP.ReadOnly = True
            txtWasteY.ReadOnly = True
            txtWasteG.ReadOnly = False

            txtWasteK.BackColor = Color.Linen
            txtWasteP.BackColor = Color.Linen
            txtWasteY.BackColor = Color.Linen
            txtWasteG.BackColor = Color.White

        Else
            txtGoldK.ReadOnly = False
            txtGoldP.ReadOnly = False
            txtGoldY.ReadOnly = False
            txtGoldG.ReadOnly = True

            txtWasteG.TabStop = False
            txtGoldG.TabStop = False
            txtWasteK.TabStop = True
            txtWasteP.TabStop = True
            txtWasteY.TabStop = True
            txtGoldK.TabStop = True
            txtGoldP.TabStop = True
            txtGoldY.TabStop = True
            txtGoldK.BackColor = Color.White
            txtGoldP.BackColor = Color.White
            txtGoldY.BackColor = Color.White
            txtGoldG.BackColor = Color.Linen

            txtWasteK.ReadOnly = False
            txtWasteP.ReadOnly = False
            txtWasteY.ReadOnly = False
            txtWasteG.ReadOnly = True

            txtWasteK.BackColor = Color.White
            txtWasteP.BackColor = Color.White
            txtWasteY.BackColor = Color.White
            txtWasteG.BackColor = Color.Linen
        End If
    End Sub

    Private Sub PayGoldQualityForTextChange()

        If _IsPayGram = True Then
            txtPayGoldK.ReadOnly = True
            txtPayGoldP.ReadOnly = True
            txtPayGoldY.ReadOnly = True
            txtPayGoldG.ReadOnly = False
            txtPayGoldG.TabStop = True
            txtPayGoldK.TabStop = False
            txtPayGoldP.TabStop = False
            txtPayGoldY.TabStop = False
            txtPayGoldK.BackColor = Color.Linen
            txtPayGoldP.BackColor = Color.Linen
            txtPayGoldY.BackColor = Color.Linen
            txtPayGoldG.BackColor = Color.White
        Else
            txtPayGoldK.ReadOnly = False
            txtPayGoldP.ReadOnly = False
            txtPayGoldY.ReadOnly = False
            txtPayGoldG.ReadOnly = True

            txtPayGoldG.TabStop = False
            txtPayGoldK.TabStop = True
            txtPayGoldP.TabStop = True
            txtPayGoldY.TabStop = True

            txtPayGoldK.BackColor = Color.White
            txtPayGoldP.BackColor = Color.White
            txtPayGoldY.BackColor = Color.White
            txtPayGoldG.BackColor = Color.Linen
        End If
    End Sub

    Private Sub txtSecondAdvanceAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSecondAdvanceAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtSecondAdvanceAmount_TextChanged(sender As Object, e As EventArgs) Handles txtSecondAdvanceAmount.TextChanged
        If txtAdvanceAmount.Text = "" Then txtAdvanceAmount.Text = "0"
        If txtSecondAdvanceAmount.Text = "" Then txtSecondAdvanceAmount.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"
        txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(Val(txtAdvanceAmount.Text)) + CLng(Val(txtSecondAdvanceAmount.Text))))
    End Sub

    Private Sub txtAdvanceAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAdvanceAmount.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtAdvanceAmount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdvanceAmount.TextChanged
        If txtAdvanceAmount.Text = "" Then txtAdvanceAmount.Text = "0"
        If txtSecondAdvanceAmount.Text = "" Then txtSecondAdvanceAmount.Text = "0"
        If txtAllNetAmt.Text = "" Then txtAllNetAmt.Text = "0"

        txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(Val(txtAdvanceAmount.Text)) + CLng(Val(txtSecondAdvanceAmount.Text))))
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

    Private Sub txtTotalAmt_TextChanged(sender As Object, e As EventArgs) Handles txtTotalAmt.TextChanged
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        txtNetAmt.Text = txtTotalAmt.Text

    End Sub
    Private Sub txtNetAmt_TextChanged(sender As Object, e As EventArgs) Handles txtNetAmt.TextChanged

        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"

        If (txtNetAmt.Text <> "") And (txtTotalAmt.Text <> "") Then
            txtAddSub.Text = CStr(CLng(txtNetAmt.Text) - CLng(txtTotalAmt.Text))
        End If


    End Sub

    Private Sub txtAllNetAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAllNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtAllNetAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAllNetAmt.TextChanged
        If txtAdvanceAmount.Text = "" Then txtAdvanceAmount.Text = "0"
        If txtSecondAdvanceAmount.Text = "" Then txtSecondAdvanceAmount.Text = "0"

        If txtAllNetAmt.Text = "" Then
            txtAllNetAmt.Text = "0"
            txtAdvanceAmount.Text = "0"
            txtSecondAdvanceAmount.Text = "0"
        End If
        If (txtAllNetAmt.Text <> "") And (txtAllTotalAmt.Text <> "") Then
            txtAllAddSub.Text = CStr(CLng(txtAllNetAmt.Text) - CLng(txtAllTotalAmt.Text))

            txtAdvanceAmount.Text = txtAllNetAmt.Text
            txtBalanceAmt.Text = CStr(CLng(txtAllNetAmt.Text) - (CLng(Val(txtAdvanceAmount.Text)) + CLng(Val(txtSecondAdvanceAmount.Text))))
        End If
    End Sub
    Private Sub txtDesignCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesignCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtNetAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtDesignCharges_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDesignCharges.TextChanged
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
        CalculateTotalAmount()
    End Sub
    Private Sub txtPayGoldG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPayGoldG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtPayGoldK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPayGoldK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtPayGoldP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPayGoldP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPayGoldY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPayGoldY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtGoldK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGoldK.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtGoldP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGoldP.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
    Private Sub txtGoldY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGoldY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtGoldG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGoldG.KeyPress
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
    Private Sub txtWasteG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWasteG.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub
    Private Sub txtPayGoldG_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayGoldG.TextChanged
        If txtPayGoldG.Text = "" Then txtPayGoldG.Text = "0.0"
        If Val(txtPayGoldG.Text.Trim) >= 0.0 And _IsPayGram = True Then
            CalculatePayForGram()
        End If
    End Sub

    Private Sub txtGoldG_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldG.TextChanged
        If txtGoldG.Text = "" Then txtGoldG.Text = "0.0"

        If Val(txtGoldG.Text.Trim) >= 0 And _IsGram = True Then
            CalculateEstimateForGram()
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtWasteG_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWasteG.TextChanged
        If txtWasteG.Text = "" Then txtWasteG.Text = "0.0"

        If Val(txtWasteG.Text.Trim) >= 0 And _IsGram = True Then
            CalculateWasteForGram()
            CalculateGoldPrice()
        End If
    End Sub
    Private Sub txtPayGoldK_TextChanged(sender As Object, e As EventArgs) Handles txtPayGoldK.TextChanged
        If txtPayGoldK.Text = "" Then txtPayGoldK.Text = "0"

        If Val(txtPayGoldK.Text.Trim) >= 0 And _IsPayGram = False Then
            CalculatePayForKPY()
        End If
    End Sub

    Private Sub txtPayGoldP_TextChanged(sender As Object, e As EventArgs) Handles txtPayGoldP.TextChanged
        If txtPayGoldP.Text = "" Then txtPayGoldP.Text = "0"

        If Val(txtPayGoldP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtPayGoldP.Text = 0
            txtPayGoldP.SelectAll()
        End If

        If _IsPayGram = False And Val(txtPayGoldP.Text.Trim) >= 0 Then
            CalculatePayForKPY()
        End If
    End Sub


    Private Sub txtPayGoldY_TextChanged(sender As Object, e As EventArgs) Handles txtPayGoldY.TextChanged
        If txtPayGoldY.Text = "" Then txtPayGoldY.Text = "0.0"

        If Val(txtPayGoldY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtPayGoldY.Text = "0.0"
            txtPayGoldY.SelectAll()
        End If

        If _IsPayGram = False And Val(txtPayGoldY.Text.Trim) >= 0 Then
            CalculatePayForKPY()
        End If
    End Sub
    Private Sub txtGoldK_TextChanged(sender As Object, e As EventArgs) Handles txtGoldK.TextChanged
        If txtGoldK.Text = "" Then txtGoldK.Text = "0"

        If Val(txtGoldK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateEstimateForKPY()
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub

    Private Sub txtGoldP_TextChanged(sender As Object, e As EventArgs) Handles txtGoldP.TextChanged
        If txtGoldP.Text = "" Then txtGoldP.Text = "0"

        If Val(txtGoldP.Text) >= 16 Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtGoldP.Text = 0
            txtGoldP.SelectAll()
        End If

        If Val(txtGoldP.Text.Trim) >= 0 And _IsGram = False Then
            CalculateEstimateForKPY()
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub


    Private Sub txtGoldY_TextChanged(sender As Object, e As EventArgs) Handles txtGoldY.TextChanged
        If txtGoldY.Text = "" Then txtGoldY.Text = "0.0"

        If Val(txtGoldY.Text) >= Global_PToY Then
            MsgBox("Invaild Weight", MsgBoxStyle.Information, AppName)
            txtGoldY.Text = "0.0"
            txtGoldY.SelectAll()
        End If

        If Val(txtGoldY.Text.Trim) >= 0 And _IsGram = False Then
            CalculateEstimateForKPY()
            CalculateTotalWeight()
            CalculateGoldPrice()
        End If
    End Sub
    Private Sub txtWasteK_TextChanged(sender As Object, e As EventArgs) Handles txtWasteK.TextChanged
        If txtWasteK.Text = "" Then txtWasteK.Text = "0"

        If Val(txtWasteK.Text.Trim) >= 0 And _IsGram = False Then
            CalculateWasteForKPY()
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
            CalculateWasteForKPY()
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
            CalculateWasteForKPY()
            CalculateGoldPrice()
        End If
    End Sub

#End Region

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If cboItemCategory.Text = "" Then
            MsgBox("Please Fill ItemCategory!", MsgBoxStyle.Information, "Data Require!")
            cboItemCategory.Focus()
            Exit Sub
        End If

        If cboItemName.Text = "" Then
            MsgBox("Please Fill ItemName!", MsgBoxStyle.Information, "Data Require!")
            cboItemName.Focus()
            Exit Sub
        End If

        If cboGoldQuality.Text = "" Then
            MsgBox("Please Fill GoldQuality!", MsgBoxStyle.Information, "Data Require!")
            cboGoldQuality.Focus()
            Exit Sub
        End If
        If txtCurrentPrice.Text = "" Or Val(txtCurrentPrice.Text) = 0 Then
            MsgBox("Please Fill Order Rate!", MsgBoxStyle.Information, "Data Require!")
            txtCurrentPrice.Focus()
            Exit Sub
        End If

        If cboGoldSmith.Text = "" Then
            MsgBox("Please Fill GoldSmith!", MsgBoxStyle.Information, "Data Require!")
            cboGoldSmith.Focus()
            Exit Sub
        End If

        If _EstimateGoldTK = 0 Then
            MsgBox("Please Fill Weight!", MsgBoxStyle.Information, "Data Require!")
            If _IsGram = True Then
                txtGoldG.Focus()
            Else
                txtGoldK.Focus()
            End If
            Exit Sub
        End If

        If _IsUpdate Then
            Dim dtItem As New DataTable
            If _OrderReceiveDetailID <> "" Then
                dtItem = _SalesItemCon.GetSaleItemDataByOrderReceiveDetailID(_OrderReceiveDetailID)
                If dtItem.Rows.Count > 0 Then
                    MsgBox("This Item Is Alreadey Barcode!", MsgBoxStyle.Information, AppName)
                    Exit Sub
                End If
            End If
            UpdateItem(_OrderReceiveDetailID, _dtOrderInvoiceItem)
        Else
            _OrderReceiveDetailID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OrderReceiveDetail, EnumSetting.GenerateKeyType.OrderReceiveDetail.ToString, dtpOrderDate.Value)
            InsertItem(_OrderReceiveDetailID, _dtOrderInvoiceItem)
        End If

        CalculateAllDetailTotalAmount()
        ClearDetail()
    End Sub
    Public Sub InsertItem(ByVal _OrderReceiveDetailID As String, ByVal _dtOrderInvoiceItem As DataTable)
        Dim drItem As DataRow

        drItem = _dtDetail.NewRow
        drItem.Item("OrderReceiveDetailID") = _OrderReceiveDetailID
        drItem.Item("OrderInvoiceID") = _OrderInvoiceID
        drItem.Item("ItemCategoryID") = cboItemCategory.SelectedValue
        drItem.Item("GoldSmithID") = cboGoldSmith.SelectedValue
        drItem.Item("ItemCategory") = cboItemCategory.Text
        drItem.Item("ItemNameID") = cboItemName.SelectedValue
        drItem.Item("ItemName") = cboItemName.Text
        drItem.Item("OrderRate") = IIf(txtCurrentPrice.Text = "", 0, txtCurrentPrice.Text)
        drItem.Item("Length") = IIf(txtLength.Text = "", "-", txtLength.Text)
        drItem.Item("Width") = IIf(txtWidth.Text = "", "-", txtWidth.Text)
        drItem.Item("GoldQualityID") = cboGoldQuality.SelectedValue
        drItem.Item("GoldQuality") = cboGoldQuality.Text
        drItem.Item("GoldTK") = _EstimateGoldTK
        drItem.Item("GoldTG") = _EstimateGoldTG
        drItem.Item("TotalGemTK") = _GemsTK
        drItem.Item("TotalGemTG") = _GemsTG
        drItem.Item("WasteTK") = _WasteTK
        drItem.Item("WasteTG") = _WasteTG
        drItem.Item("GoldPrice") = txtGoldPrice.Text
        drItem.Item("GemPrice") = txtGemsPrice.Text
        drItem.Item("PlatingFee") = txtPlatingCharges.Text
        drItem.Item("MountingFee") = txtMountingCharges.Text
        drItem.Item("DesignCharges") = txtDesignCharges.Text
        drItem.Item("WhiteCharges") = txtWhiteCharges.Text
        drItem.Item("TotalAmount") = txtTotalAmt.Text
        drItem.Item("NetAmount") = txtNetAmt.Text
        drItem.Item("AddOrSub") = txtAddSub.Text
        drItem.Item("Design") = txtDesign.Text
        drItem.Item("IsDiamond") = chkIsDiamond.Checked

        _dtDetail.Rows.Add(drItem)
        grdDetail.DataSource = _dtDetail

        Dim drDiamond As DataRow
        For Each dr As DataRow In _dtOrderInvoiceItem.Rows
            drDiamond = _dtAllDiamond.NewRow()
            drDiamond("OrderInvoiceGemsItemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OrderInvoiceGemsItem, CommonInfo.EnumSetting.GenerateKeyType.OrderInvoiceGemsItem.ToString, dtpOrderDate.Value)
            drDiamond("OrderReceiveDetailID") = _OrderReceiveDetailID
            drDiamond("GemsCategoryID") = IIf(IsDBNull(dr("GemsCategoryID")), "", dr("GemsCategoryID"))
            drDiamond("GemsName") = IIf(IsDBNull(dr("GemsName")), "-", dr("GemsName"))
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
            drDiamond("IsCustomerGem") = dr("IsCustomerGem")
            _dtAllDiamond.Rows.Add(drDiamond)
        Next
    End Sub
    Public Sub UpdateItem(ByVal _OrderReceiveDetailID As String, ByVal _dtOrderInvoiceItem As DataTable)
        Dim drItem As DataRow
        drItem = _dtDetail.Rows(grdDetail.CurrentRow.Index)

        If Not IsNothing(drItem) Then
            drItem.Item("OrderReceiveDetailID") = _OrderReceiveDetailID
            drItem.Item("OrderInvoiceID") = _OrderInvoiceID
            drItem.Item("ItemCategoryID") = cboItemCategory.SelectedValue
            drItem.Item("GoldSmithID") = cboGoldSmith.SelectedValue
            drItem.Item("ItemCategory") = cboItemCategory.Text
            drItem.Item("ItemNameID") = cboItemName.SelectedValue
            drItem.Item("ItemName") = cboItemName.Text
            drItem.Item("OrderRate") = IIf(txtCurrentPrice.Text = "", 0, txtCurrentPrice.Text)
            drItem.Item("Length") = IIf(txtLength.Text = "", "-", txtLength.Text)
            drItem.Item("Width") = IIf(txtWidth.Text = "", "-", txtWidth.Text)
            drItem.Item("GoldQualityID") = cboGoldQuality.SelectedValue
            drItem.Item("GoldQuality") = cboGoldQuality.Text
            drItem.Item("GoldTK") = _EstimateGoldTK
            drItem.Item("GoldTG") = _EstimateGoldTG
            drItem.Item("TotalGemTK") = _GemsTK
            drItem.Item("TotalGemTG") = _GemsTG
            drItem.Item("WasteTK") = _WasteTK
            drItem.Item("WasteTG") = _WasteTG
            drItem.Item("GoldPrice") = txtGoldPrice.Text
            drItem.Item("GemPrice") = txtGemsPrice.Text
            drItem.Item("PlatingFee") = txtPlatingCharges.Text
            drItem.Item("MountingFee") = txtMountingCharges.Text
            drItem.Item("DesignCharges") = txtDesignCharges.Text
            drItem.Item("WhiteCharges") = txtWhiteCharges.Text
            drItem.Item("TotalAmount") = txtTotalAmt.Text
            drItem.Item("NetAmount") = txtNetAmt.Text
            drItem.Item("AddOrSub") = txtAddSub.Text
            drItem.Item("Design") = txtDesign.Text
            drItem.Item("IsDiamond") = chkIsDiamond.Checked

            grdDetail.DataSource = _dtDetail

            _dtOrderInvoiceItem = grdOrderGemsItem.DataSource

            Dim j As Integer = 0
            If _dtOrderInvoiceItem.Rows.Count > 0 Then  '   if Gems Update , check dtstone. if dtstone has Gems, delete gemsid .
                For i As Integer = 0 To _dtOrderInvoiceItem.Rows.Count - 1
                    While j < _dtAllDiamond.Rows.Count
                        Dim row As DataRow
                        row = _dtAllDiamond.Rows(j)

                        If Not IsDBNull(_dtOrderInvoiceItem.Rows(i).Item("OrderReceiveDetailID")) Then
                            If row.Item("OrderReceiveDetailID") = _OrderReceiveDetailID Then
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
                        If row.Item("OrderReceiveDetailID") = _OrderReceiveDetailID Then
                            _dtAllDiamond.Rows.Remove(row)
                        Else
                            j = j + 1
                        End If
                    End While

                End If
            End If

            ''Dim dtTestStone As New DataTable
            ''dtTestStone = _dtAllDiamond
            ''Dim dtTestPDiaGems As New DataTable
            ''dtTestPDiaGems = _dtPDiaItemGems

            Dim drPItemDetailStone As DataRow
            If _dtAllDiamond.Rows.Count <> 0 Then
                For i As Integer = 0 To _dtAllDiamond.Rows.Count - 1
                    If _dtAllDiamond.Rows(i).Item("OrderReceiveDetailID") = _OrderReceiveDetailID Then
                        For Each drvPItemDetailStone As DataRow In _dtOrderInvoiceItem.Rows
                            If Not IsDBNull(drvPItemDetailStone("OrderReceiveDetailID")) And drvPItemDetailStone("OrderReceiveDetailID") <> "" Then
                                If _dtAllDiamond.Rows(i).Item("OrderReceiveDetailID") = _OrderReceiveDetailID And _IsRowDelete <> True Then
                                    If _dtAllDiamond.Rows(i).Item("OrderInvoiceGemsItemID") = drvPItemDetailStone("OrderInvoiceGemsItemID") Then
                                        drvPItemDetailStone.BeginEdit()
                                        _dtAllDiamond.Rows(i).Item("OrderReceiveDetailID") = _OrderReceiveDetailID
                                        _dtAllDiamond.Rows(i).Item("GemsCategoryID") = IIf(IsDBNull(drvPItemDetailStone("GemsCategoryID")), "", drvPItemDetailStone("GemsCategoryID"))
                                        _dtAllDiamond.Rows(i).Item("GemsName") = IIf(IsDBNull(drvPItemDetailStone("GemsName")), "-", drvPItemDetailStone("GemsName"))
                                        _dtAllDiamond.Rows(i).Item("GemsTK") = drvPItemDetailStone("GemsTK")
                                        _dtAllDiamond.Rows(i).Item("GemsTG") = drvPItemDetailStone("GemsTG")
                                        _dtAllDiamond.Rows(i).Item("YOrCOrG") = IIf(IsDBNull(drvPItemDetailStone("YOrCOrG")), "0", drvPItemDetailStone("YOrCOrG"))                                        '
                                        _dtAllDiamond.Rows(i).Item("GemsK") = drvPItemDetailStone("GemsK")
                                        _dtAllDiamond.Rows(i).Item("GemsP") = drvPItemDetailStone("GemsP")
                                        _dtAllDiamond.Rows(i).Item("GemsY") = drvPItemDetailStone("GemsY")                                        '
                                        _dtAllDiamond.Rows(i).Item("GemsTW") = drvPItemDetailStone("GemsTW")
                                        _dtAllDiamond.Rows(i).Item("Qty") = drvPItemDetailStone("Qty")
                                        _dtAllDiamond.Rows(i).Item("UnitPrice") = drvPItemDetailStone("UnitPrice")
                                        _dtAllDiamond.Rows(i).Item("Type") = drvPItemDetailStone("Type")
                                        _dtAllDiamond.Rows(i).Item("Amount") = drvPItemDetailStone("Amount")
                                        _dtAllDiamond.Rows(i).Item("IsCustomerGem") = drvPItemDetailStone("IsCustomerGem")
                                        drvPItemDetailStone.EndEdit()
                                        i += 1

                                    End If
                                End If
                            Else
                                drPItemDetailStone = _dtAllDiamond.NewRow()
                                If IsDBNull(drvPItemDetailStone("OrderInvoiceGemsItemID")) Then
                                    drPItemDetailStone("OrderInvoiceGemsItemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OrderInvoiceGemsItem, CommonInfo.EnumSetting.GenerateKeyType.OrderInvoiceGemsItem.ToString, dtpOrderDate.Value)
                                Else
                                    drPItemDetailStone("OrderInvoiceGemsItemID") = drvPItemDetailStone("OrderInvoiceGemsItemID")
                                End If
                                drPItemDetailStone("OrderReceiveDetailID") = _OrderReceiveDetailID
                                drPItemDetailStone("GemsCategoryID") = IIf(IsDBNull(drvPItemDetailStone("GemsCategoryID")), "", drvPItemDetailStone("GemsCategoryID"))
                                drPItemDetailStone("GemsName") = IIf(IsDBNull(drvPItemDetailStone("GemsName")), "-", drvPItemDetailStone("GemsName"))
                                drPItemDetailStone("GemsTK") = drvPItemDetailStone("GemsTK")
                                drPItemDetailStone("GemsTG") = drvPItemDetailStone("GemsTG")
                                drPItemDetailStone("YOrCOrG") = IIf(IsDBNull(drvPItemDetailStone("YOrCOrG")), "0", drvPItemDetailStone("YOrCOrG"))                                '
                                drPItemDetailStone("GemsK") = drvPItemDetailStone("GemsK")
                                drPItemDetailStone("GemsP") = drvPItemDetailStone("GemsP")
                                drPItemDetailStone("GemsY") = drvPItemDetailStone("GemsY")
                                drPItemDetailStone("GemsTW") = drvPItemDetailStone("GemsTW")
                                drPItemDetailStone("Qty") = drvPItemDetailStone("Qty")
                                drPItemDetailStone("Type") = drvPItemDetailStone("Type")
                                drPItemDetailStone("UnitPrice") = drvPItemDetailStone("UnitPrice")
                                drPItemDetailStone("Amount") = drvPItemDetailStone("Amount")
                                drPItemDetailStone("IsCustomerGem") = drvPItemDetailStone("IsCustomerGem")
                                _dtAllDiamond.Rows.Add(drPItemDetailStone)
                                i += 1
                            End If
                        Next
                        _dtOrderInvoiceItem.DefaultView.RowFilter = ""
                        _dtOrderInvoiceItem.DefaultView.Sort = "OrderReceiveDetailID"

                    End If
                Next

            Else '''''' if _dtAllDiamond.Row.Count=0
                For Each drGems As DataRow In _dtOrderInvoiceItem.Rows
                    drPItemDetailStone = _dtAllDiamond.NewRow()
                    If IsDBNull(drGems("OrderInvoiceGemsItemID")) Then
                        drPItemDetailStone("OrderInvoiceGemsItemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OrderInvoiceGemsItem, CommonInfo.EnumSetting.GenerateKeyType.OrderInvoiceGemsItem.ToString, dtpOrderDate.Value)
                    Else
                        drPItemDetailStone("OrderInvoiceGemsItemID") = drGems("OrderInvoiceGemsItemID")
                    End If

                    drPItemDetailStone("OrderReceiveDetailID") = _OrderReceiveDetailID
                    drPItemDetailStone("GemsCategoryID") = IIf(IsDBNull(drGems("GemsCategoryID")), "", drGems("GemsCategoryID"))
                    drPItemDetailStone("GemsName") = IIf(IsDBNull(drGems("GemsName")), "-", drGems("GemsName"))
                    drPItemDetailStone("GemsTK") = drGems("GemsTK")
                    drPItemDetailStone("GemsTG") = drGems("GemsTG")
                    drPItemDetailStone("YOrCOrG") = IIf(IsDBNull(drGems("YOrCOrG")), "0", drGems("YOrCOrG"))                    '
                    drPItemDetailStone("GemsK") = drGems("GemsK")
                    drPItemDetailStone("GemsP") = drGems("GemsP")
                    drPItemDetailStone("GemsY") = drGems("GemsY")
                    drPItemDetailStone("GemsTW") = drGems("GemsTW")
                    drPItemDetailStone("Qty") = drGems("Qty")
                    drPItemDetailStone("Type") = drGems("Type")
                    drPItemDetailStone("UnitPrice") = drGems("UnitPrice")
                    drPItemDetailStone("Amount") = drGems("Amount")
                    drPItemDetailStone("IsCustomerGem") = drGems("IsCustomerGem")
                    _dtAllDiamond.Rows.Add(drPItemDetailStone)
                Next

            End If
            Dim drFind As Boolean = False

            If _dtAllDiamond.Rows.Count <> 0 Then
                For i As Integer = 0 To _dtAllDiamond.Rows.Count - 1
                    If _dtAllDiamond.Rows(i).Item("OrderReceiveDetailID") = _OrderReceiveDetailID Then
                        drFind = True
                    Else
                        drFind = False
                    End If
                Next

                If drFind = False Then
                    For Each drGems As DataRow In _dtOrderInvoiceItem.Rows
                        drPItemDetailStone = _dtAllDiamond.NewRow()
                        If IsDBNull(drGems("OrderInvoiceGemsItemID")) Then
                            drPItemDetailStone("OrderInvoiceGemsItemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OrderInvoiceGemsItem, CommonInfo.EnumSetting.GenerateKeyType.OrderInvoiceGemsItem.ToString, dtpOrderDate.Value)
                        Else
                            drPItemDetailStone("OrderInvoiceGemsItemID") = drGems("OrderInvoiceGemsItemID")
                        End If
                        drPItemDetailStone("OrderReceiveDetailID") = _OrderReceiveDetailID
                        drPItemDetailStone("GemsCategoryID") = IIf(IsDBNull(drGems("GemsCategoryID")), "", drGems("GemsCategoryID"))
                        drPItemDetailStone("GemsName") = IIf(IsDBNull(drGems("GemsName")), "-", drGems("GemsName"))
                        drPItemDetailStone("GemsTK") = drGems("GemsTK")
                        drPItemDetailStone("GemsTG") = drGems("GemsTG")
                        drPItemDetailStone("YOrCOrG") = IIf(IsDBNull(drGems("YOrCOrG")), "0", drGems("YOrCOrG"))                    '
                        drPItemDetailStone("GemsK") = drGems("GemsK")
                        drPItemDetailStone("GemsP") = drGems("GemsP")
                        drPItemDetailStone("GemsY") = drGems("GemsY")
                        drPItemDetailStone("GemsTW") = drGems("GemsTW")
                        drPItemDetailStone("Qty") = drGems("Qty")
                        drPItemDetailStone("Type") = drGems("Type")
                        drPItemDetailStone("UnitPrice") = drGems("UnitPrice")
                        drPItemDetailStone("Amount") = drGems("Amount")
                        drPItemDetailStone("IsCustomerGem") = drGems("IsCustomerGem")
                        _dtAllDiamond.Rows.Add(drPItemDetailStone)
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
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
        _OrderReceiveDetailID = ""
        cboItemCategory.SelectedIndex = -1
        cboItemName.SelectedIndex = -1
        cboGoldQuality.SelectedIndex = -1
        cboGoldSmith.SelectedIndex = -1

        txtLength.Text = ""
        txtWidth.Text = ""
        txtCurrentPrice.Text = "0"
        _IsGram = False
        lblIsGram.Text = ""

        _TotalTG = 0
        _TotalTK = 0
        _EstimateGoldTG = 0
        _EstimateGoldTK = 0
        _GemsTK = 0
        _GemsTK = 0
        _WasteTG = 0
        _WasteTK = 0

        _grdGemTK = 0
        _grdGemTG = 0

        txtGoldK.Text = "0"
        txtGoldP.Text = "0"
        txtGoldY.Text = "0.0"
        txtGoldG.Text = "0.0"

        txtGoldK.ReadOnly = True
        txtGoldP.ReadOnly = True
        txtGoldY.ReadOnly = True
        txtGoldG.ReadOnly = True

        txtGoldG.BackColor = Color.Linen
        txtGoldK.BackColor = Color.Linen
        txtGoldP.BackColor = Color.Linen
        txtGoldY.BackColor = Color.Linen

        txtWasteK.Text = "0"
        txtWasteP.Text = "0"
        txtWasteY.Text = "0.0"
        txtWasteG.Text = "0.0"

        txtWasteG.BackColor = Color.Linen
        txtWasteK.BackColor = Color.Linen
        txtWasteP.BackColor = Color.Linen
        txtWasteY.BackColor = Color.Linen

        txtWasteK.ReadOnly = True
        txtWasteP.ReadOnly = True
        txtWasteY.ReadOnly = True
        txtWasteG.ReadOnly = True

        txtGemsK.Text = "0"
        txtGemsP.Text = "0"
        txtGemsY.Text = "0.00"
        txtGemsG.Text = "0.0"

        txtTotalK.Text = "0"
        txtTotalP.Text = "0"
        txtTotalY.Text = "0.0"
        txtTotalG.Text = "0.0"

        txtGoldPrice.Text = "0"
        txtGemsPrice.Text = "0"
        txtDesignCharges.Text = "0"
        txtPlatingCharges.Text = "0"
        txtWhiteCharges.Text = "0"
        txtMountingCharges.Text = "0"

        txtTotalAmt.Text = "0"
        txtNetAmt.Text = "0"
        txtAddSub.Text = "0"

        txtGemsG.BackColor = Color.Linen
        txtGemsK.BackColor = Color.Linen
        txtGemsP.BackColor = Color.Linen
        txtGemsY.BackColor = Color.Linen

        txtTotalG.BackColor = Color.Linen
        txtTotalK.BackColor = Color.Linen
        txtTotalP.BackColor = Color.Linen
        txtTotalY.BackColor = Color.Linen
        btnAdd.Text = "Add"
        _IsUpdate = False
        txtDesign.Text = ""
        chkIsDiamond.Checked = False
        _dtOrderInvoiceItem = New DataTable
        FormatItemTable(_dtOrderInvoiceItem)
        grdOrderGemsItem.AutoGenerateColumns = False
        grdOrderGemsItem.ReadOnly = False
        grdOrderGemsItem.DataSource = _dtOrderInvoiceItem
        FormatOrderInvoiceItem()
    End Sub

    Private Sub FormatItemTable(_dtOrderInvoiceItem)
        Dim dc As DataColumn
        _dtOrderInvoiceItem.Columns.Add("OrderInvoiceGemsItemID", System.Type.GetType("System.String"))
        _dtOrderInvoiceItem.Columns.Add("OrderReceiveDetailID", System.Type.GetType("System.String"))
        _dtOrderInvoiceItem.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))
        _dtOrderInvoiceItem.Columns.Add("GemsName", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "GemsK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtOrderInvoiceItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtOrderInvoiceItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsY"
        dc.DataType = System.Type.GetType("System.Decimal")
        If numberformat = 1 Then
            dc.DefaultValue = "0.0"
        Else
            dc.DefaultValue = "0.00"
        End If
        _dtOrderInvoiceItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsC"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0
        _dtOrderInvoiceItem.Columns.Add(dc)

        _dtOrderInvoiceItem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtOrderInvoiceItem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtOrderInvoiceItem.Columns.Add("YOrCOrG", System.Type.GetType("System.String"))
        _dtOrderInvoiceItem.Columns.Add("GemsTW", System.Type.GetType("System.Decimal"))
        _dtOrderInvoiceItem.Columns.Add("Qty", System.Type.GetType("System.Int32"))

        dc = New DataColumn
        dc.ColumnName = "Type"
        dc.DataType = System.Type.GetType("System.String")
        _dtOrderInvoiceItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "IsCustomerGem"
        dc.DataType = System.Type.GetType("System.Boolean")
        dc.DefaultValue = False
        _dtOrderInvoiceItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "UnitPrice"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtOrderInvoiceItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtOrderInvoiceItem.Columns.Add(dc)
    End Sub
    Private Sub cboItemCategory_Click(sender As Object, e As EventArgs) Handles cboItemCategory.Click
        GetcboItemCategory()
    End Sub
    Private Sub cboItemCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCategory, e)
    End Sub
    Private Sub cboItemCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCategory.Leave
        AutoCompleteCombo_Leave(cboItemCategory, "")
    End Sub
    Private Sub cboItemCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboItemCategory.SelectedValueChanged
        itemid = cboItemCategory.SelectedValue
        RefreshItemNameCbo(itemid)
    End Sub
    Private Sub cboGoldQuality_Click(sender As Object, e As EventArgs) Handles cboGoldQuality.Click
        GetcboGoldQuality()
    End Sub

    Private Sub cboGoldQuality_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGoldQuality.KeyUp
        AutoCompleteCombo_KeyUp(cboGoldQuality, e)
    End Sub

    Private Sub cboGoldQuality_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGoldQuality.Leave
        AutoCompleteCombo_Leave(cboGoldQuality, "")
    End Sub
    Private Sub cboItemName_Click(sender As Object, e As EventArgs) Handles cboItemName.Click
        cboItemName.DisplayMember = "ItemName_"
        cboItemName.ValueMember = "ItemNameID"
        cboItemName.DataSource = _ItemName.GetItemNameListByItemCategory(itemid).DefaultView
    End Sub
    Private Sub cboItemName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemName.KeyUp
        AutoCompleteCombo_KeyUp(cboItemName, e)
    End Sub
    Private Sub cboItemName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemName.Leave
        AutoCompleteCombo_Leave(cboItemName, "")
    End Sub
    Private Sub grdDetail_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetail.CellContentClick

        If e.ColumnIndex = 27 Then
            With grdDetail
                Dim drItem As DataRow
                Dim OldOrderReceiveDetailID As String
                OldOrderReceiveDetailID = .CurrentRow.Cells("OrderReceiveDetailID").Value
                _OrderReceiveDetailID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OrderReceiveDetail, EnumSetting.GenerateKeyType.OrderReceiveDetail.ToString, dtpOrderDate.Value)

                drItem = _dtDetail.NewRow
                drItem.Item("OrderReceiveDetailID") = _OrderReceiveDetailID
                drItem.Item("OrderInvoiceID") = _OrderInvoiceID
                drItem.Item("ItemCategoryID") = .CurrentRow.Cells("ItemCategoryID").Value
                drItem.Item("ItemCategory") = .CurrentRow.Cells("ItemCategory").Value
                drItem.Item("ItemNameID") = .CurrentRow.Cells("ItemNameID").Value
                drItem.Item("GoldSmithID") = .CurrentRow.Cells("GoldSmithID").Value
                drItem.Item("ItemName") = .CurrentRow.Cells("ItemName").Value
                drItem.Item("OrderRate") = .CurrentRow.Cells("OrderRate").Value
                drItem.Item("Length") = .CurrentRow.Cells("Length").Value
                drItem.Item("Width") = .CurrentRow.Cells("Width").Value
                drItem.Item("GoldQualityID") = .CurrentRow.Cells("GoldQualityID").Value
                drItem.Item("GoldQuality") = .CurrentRow.Cells("GoldQuality").Value
                drItem.Item("GoldTK") = .CurrentRow.Cells("GoldTK").Value
                drItem.Item("GoldTG") = .CurrentRow.Cells("GoldTG").Value
                drItem.Item("TotalGemTK") = .CurrentRow.Cells("TotalGemTK").Value
                drItem.Item("TotalGemTG") = .CurrentRow.Cells("TotalGemTG").Value
                drItem.Item("WasteTK") = .CurrentRow.Cells("WasteTK").Value
                drItem.Item("WasteTG") = .CurrentRow.Cells("WasteTG").Value
                drItem.Item("GoldPrice") = .CurrentRow.Cells("GoldPrice").Value
                drItem.Item("GemPrice") = .CurrentRow.Cells("GemPrice").Value
                drItem.Item("PlatingFee") = .CurrentRow.Cells("PlatingFee").Value
                drItem.Item("MountingFee") = .CurrentRow.Cells("MountingFee").Value
                drItem.Item("DesignCharges") = .CurrentRow.Cells("DesignCharges").Value
                drItem.Item("WhiteCharges") = .CurrentRow.Cells("WhiteCharges").Value
                drItem.Item("TotalAmount") = .CurrentRow.Cells("TotalAmount").Value
                drItem.Item("NetAmount") = .CurrentRow.Cells("NetAmount").Value
                drItem.Item("AddOrSub") = .CurrentRow.Cells("AddOrSub").Value
                drItem.Item("Design") = .CurrentRow.Cells("Design").Value
                drItem.Item("IsDiamond") = .CurrentRow.Cells("IsDiamond").Value
                _dtDetail.Rows.Add(drItem)
                grdDetail.DataSource = _dtDetail

                Dim _dtTemp As New DataTable
                FormatItemTable(_dtTemp)

                Dim drDiamond As DataRow
                For Each dr As DataRow In _dtAllDiamond.Rows
                    If (OldOrderReceiveDetailID = dr("OrderReceiveDetailID")) Then
                        drDiamond = _dtTemp.NewRow()
                        drDiamond("OrderInvoiceGemsItemID") = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OrderInvoiceGemsItem, CommonInfo.EnumSetting.GenerateKeyType.OrderInvoiceGemsItem.ToString, dtpOrderDate.Value)
                        drDiamond("OrderReceiveDetailID") = _OrderReceiveDetailID
                        drDiamond("GemsCategoryID") = dr("GemsCategoryID")
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
                        drDiamond("IsCustomerGem") = dr("IsCustomerGem")
                        _dtTemp.Rows.Add(drDiamond)
                    End If
                Next

                Dim drGem As DataRow
                For Each DataRow As DataRow In _dtTemp.Rows
                    drGem = _dtAllDiamond.NewRow()
                    drGem("OrderInvoiceGemsItemID") = DataRow("OrderInvoiceGemsItemID")
                    drGem("OrderReceiveDetailID") = DataRow("OrderReceiveDetailID")
                    drGem("GemsCategoryID") = DataRow("GemsCategoryID")
                    drGem("GemsName") = DataRow("GemsName")
                    drGem("GemsK") = DataRow("GemsK")
                    drGem("GemsP") = DataRow("GemsP")
                    drGem("GemsY") = DataRow("GemsY")
                    drGem("GemsTK") = DataRow("GemsTK")
                    drGem("GemsTG") = DataRow("GemsTG")
                    drGem("YOrCOrG") = DataRow("YOrCOrG")
                    drGem("GemsTW") = DataRow("GemsTW")
                    drGem("Qty") = DataRow("Qty")
                    drGem("UnitPrice") = DataRow("UnitPrice")
                    drGem("Type") = DataRow("Type")
                    drGem("Amount") = DataRow("Amount")
                    drGem("IsCustomerGem") = DataRow("IsCustomerGem")
                    _dtAllDiamond.Rows.Add(drGem)
                Next
                _dtAllDiamond.AcceptChanges()

                CalculateAllDetailTotalAmount()
                ClearDetail()
            End With
        End If
    End Sub

    Private Sub grdDetail_Click(sender As Object, e As EventArgs) Handles grdDetail.Click
        Dim GoldWeight As New GoldWeightInfo
        Dim objSItem As New OrderReceiveDetailInfo

        If grdDetail.RowCount = 0 Then
            _IsUpdate = False
            Exit Sub
        End If

        With grdDetail
            _OrderReceiveDetailID = .CurrentRow.Cells("OrderReceiveDetailID").Value
            _OrderInvoiceID = .CurrentRow.Cells("OrderInvoiceID").Value

            cboItemCategory.SelectedValue = .CurrentRow.Cells("ItemCategoryID").Value
            cboItemName.SelectedValue = .CurrentRow.Cells("ItemNameID").Value
            cboGoldSmith.SelectedValue = .CurrentRow.Cells("GoldSmithID").Value
            cboGoldQuality.SelectedValue = .CurrentRow.Cells("GoldQualityID").Value
            txtWidth.Text = .CurrentRow.Cells("Width").Value
            txtLength.Text = .CurrentRow.Cells("length").Value
            txtCurrentPrice.Text = .CurrentRow.Cells("OrderRate").Value
            txtGoldPrice.Text = .CurrentRow.Cells("GoldPrice").Value
            txtGemsPrice.Text = .CurrentRow.Cells("GemPrice").Value
            txtTotalAmt.Text = .CurrentRow.Cells("TotalAmount").Value
            txtAddSub.Text = .CurrentRow.Cells("AddOrSub").Value
            txtNetAmt.Text = .CurrentRow.Cells("NetAmount").Value
            txtDesignCharges.Text = .CurrentRow.Cells("DesignCharges").Value
            txtWhiteCharges.Text = .CurrentRow.Cells("WhiteCharges").Value
            txtPlatingCharges.Text = .CurrentRow.Cells("PlatingFee").Value
            txtMountingCharges.Text = .CurrentRow.Cells("MountingFee").Value
            txtDesign.Text = grdDetail.CurrentRow.Cells("Design").Value
            chkIsDiamond.Checked = .CurrentRow.Cells("IsDiamond").Value

            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("GoldTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtGoldK.Text = CStr(GoldWeight.WeightK)
            txtGoldP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            'txtGoldY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            txtGoldG.Text = Format(grdDetail.CurrentRow.Cells("GoldTG").Value, "0.000")
            _EstimateGoldTG = CDec(grdDetail.CurrentRow.Cells("GoldTG").Value)
            _EstimateGoldTK = CDec(grdDetail.CurrentRow.Cells("GoldTK").Value)



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
            txtWasteG.Text = Format(grdDetail.CurrentRow.Cells("WasteTG").Value, "0.000")
            _WasteTG = CDec(grdDetail.CurrentRow.Cells("WasteTG").Value)
            _WasteTK = CDec(grdDetail.CurrentRow.Cells("WasteTK").Value)


            GoldWeight.GoldTK = CDec(grdDetail.CurrentRow.Cells("TotalGemTK").Value)
            GoldWeight = _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
            txtGemsK.Text = CStr(GoldWeight.WeightK)
            txtGemsP.Text = CStr(GoldWeight.WeightP)
            If numberformat = 1 Then
                txtGemsY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.0"))
            Else
                txtGemsY.Text = CStr(Format(GoldWeight.WeightY + GoldWeight.WeightC, "0.00"))
            End If
            txtGemsG.Text = Format(grdDetail.CurrentRow.Cells("TotalGemTG").Value, "0.000")
            _GemsTK = CDec(grdDetail.CurrentRow.Cells("TotalGemTK").Value)
            _GemsTG = CDec(grdDetail.CurrentRow.Cells("TotalGemTG").Value)

            CalculateTotalWeight()
        End With

        _dtOrderInvoiceItem.Rows.Clear()
        If _dtAllDiamond.Rows.Count Then
            For i As Integer = 0 To _dtAllDiamond.Rows.Count - 1
                If Not IsDBNull(_dtAllDiamond.Rows(i).Item("OrderReceiveDetailID")) Then
                    If _dtAllDiamond.Rows(i).Item("OrderReceiveDetailID") = _OrderReceiveDetailID Then
                        Dim drItem As DataRow
                        drItem = _dtOrderInvoiceItem.NewRow
                        drItem("OrderInvoiceGemsItemID") = _dtAllDiamond.Rows(i).Item("OrderInvoiceGemsItemID")
                        drItem("OrderReceiveDetailID") = _dtAllDiamond.Rows(i).Item("OrderReceiveDetailID")
                        drItem("GemsCategoryID") = _dtAllDiamond.Rows(i).Item("GemsCategoryID")
                        drItem("GemsName") = _dtAllDiamond.Rows(i).Item("GemsName")
                        drItem("GemsK") = _dtAllDiamond.Rows(i).Item("GemsK")
                        drItem("GemsP") = _dtAllDiamond.Rows(i).Item("GemsP")
                        drItem("GemsY") = _dtAllDiamond.Rows(i).Item("GemsY")
                        drItem("GemsTK") = _dtAllDiamond.Rows(i).Item("GemsTK")
                        drItem("GemsTG") = _dtAllDiamond.Rows(i).Item("GemsTG")
                        drItem("YOrCOrG") = _dtAllDiamond.Rows(i).Item("YOrCOrG")
                        drItem("GemsTW") = _dtAllDiamond.Rows(i).Item("GemsTW")
                        drItem("Qty") = _dtAllDiamond.Rows(i).Item("Qty")
                        drItem("IsCustomerGem") = _dtAllDiamond.Rows(i).Item("IsCustomerGem")
                        drItem("UnitPrice") = _dtAllDiamond.Rows(i).Item("UnitPrice")
                        drItem("Type") = _dtAllDiamond.Rows(i).Item("Type")
                        drItem("Amount") = _dtAllDiamond.Rows(i).Item("Amount")
                        _dtOrderInvoiceItem.Rows.Add(drItem)
                    End If
                End If
            Next
            grdOrderGemsItem.DataSource = _dtOrderInvoiceItem
        End If
        btnAdd.Text = "Update"
        _IsUpdate = True
    End Sub

    Private Sub txtPlatingCharges_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlatingCharges.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub


    Private Sub txtPlatingCharges_TextChanged(sender As Object, e As EventArgs) Handles txtPlatingCharges.TextChanged
        If txtPlatingCharges.Text = "" Then txtPlatingCharges.Text = "0"
        CalculateTotalAmount()
    End Sub

    Private Sub txtDesignCharges_TextChanged_1(sender As Object, e As EventArgs) Handles txtDesignCharges.TextChanged
        If txtDesignCharges.Text = "" Then txtDesignCharges.Text = "0"
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

    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("GemsK") Or grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("GemsP") Or grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("GemsY") Then
        '    If IsDBNull(grdOrderGemsItem.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdOrderGemsItem.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdOrderGemsItem.CurrentRow.Cells("GemsY").FormattedValue) Then
        '        If grdOrderGemsItem.CurrentRow.Cells("YOrCOrG").FormattedValue <> "0" Then
        '            grdOrderGemsItem.CurrentRow.Cells("YOrCOrG").Value = "0"
        '        End If
        '    End If
        'End If

        If grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("GemsK") Or grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("GemsP") Or grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("Qty") Or grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("UnitPrice") Then
            If IsDBNull(grdOrderGemsItem.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdOrderGemsItem.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdOrderGemsItem.CurrentRow.Cells("Qty").FormattedValue) = False Or IsDBNull(grdOrderGemsItem.CurrentRow.Cells("UnitPrice").FormattedValue) = False Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If
        ElseIf grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("GemsY") Then
            If IsDBNull(grdOrderGemsItem.CurrentRow.Cells("GemsY").FormattedValue) = False Then
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
    Private Sub grdOrderGemsItem_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdOrderGemsItem.EditingControlShowing
        If grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("GemsCategoryID") Or grdOrderGemsItem.CurrentCell Is grdOrderGemsItem.CurrentRow.Cells("Type") Then
            Exit Sub
        End If

        Dim txtbox As TextBox = CType(e.Control, TextBox)
        If Not (txtbox Is Nothing) Then
            AddHandler txtbox.KeyPress, AddressOf txtBox_KeyPress
        End If
    End Sub

    Private Sub txtCurrentPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentPrice.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtCurrentPrice_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentPrice.TextChanged
        CalculateGoldPrice()
    End Sub

    Private Sub grdDetail_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles grdDetail.RowsRemoved
        Dim row As DataRow
        Dim j As Integer = _dtAllDiamond.Rows.Count() - 1
        While j >= 0
            row = _dtAllDiamond.Rows(j)
            If row.Item("OrderReceiveDetailID") = _OrderReceiveDetailID Then
                _dtAllDiamond.Rows.Remove(row)
            End If
            j = j - 1
        End While

        CalculateAllDetailTotalAmount()
        ClearDetail()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("OrderInvoice")
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

    Private Sub dtpOrderDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpOrderDate.ValueChanged
        OrderInvoiceGenerateFormat()
    End Sub

   
    Private Sub cboGoldSmith_Click(sender As Object, e As EventArgs) Handles cboGoldSmith.Click
        GetGoldSmith()
    End Sub
End Class