Imports BusinessRule
Imports CommonInfo
Public Class frm_CashReceiptOnCredit
    Implements IFormProcess

    Private CashReceiptID As String = "0"
    Private _CashReceiptID As String
    'Private _PurchaseGemsID As String
    'Private _PurchaseInvoiceID As String
    Private _PurchaseHeaderID As String
    Private _SaleInvoiceHeaderID As String
    Private _WholeSaleInvoiceID As String
    Private _ConsignmentSaleID As String
    Private _SaleLooseDiamondID As String
    Private _VoucherNo As String
    'Private _SalesGemsID As String
    'Private _SalesInvoiceID As String
    'Private _WholeSalesInvoiceID As String
    Private _OrderReturnHeaderID As String = ""
    Private _RepairReturnHeaderID As String = ""
    Private _CustomerID As String = ""
    Private _SalesGemsID As String = ""
    Private _SalesVolumeID As String = ""
    Private _ReturnAdvanceID As String

    Private dtCashReceipt As New DataTable
    Private _CashReceiptController As BusinessRule.CashReceipt.ICashReceiptController = Factory.Instance.CreateCashReceiptController
    Private _SalesGemsController As BusinessRule.SaleGems.ISaleGemsController = Factory.Instance.CreateSaleGemsController
    Private _OrderInvoiceController As BusinessRule.OrderInvoice.IOrderInvoiceController = Factory.Instance.CreateOrderInvoiceController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _PurchaseHeaderController As PurchaseItem.IPurchaseItemController = Factory.Instance.CreatePurchaseItemController
    Private _SaleInvoiceHeaderController As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private _WholesaleInvoiceController As WholeSaleInvoice.IWholeSaleInvoiceController = Factory.Instance.CreateWholeSaleInvoiceController
    Private _ConsignmentSaleController As ConsignmentSale.IConsignmentSaleController = Factory.Instance.CreateConsignmentSaleController

    Private _SaleVolumeController As BusinessRule.SalesVolume.ISalesVolumeController = Factory.Instance.CreateSalesVolumeController
    Private _RepairReturnController As BusinessRule.RepairReturn.IRepairReturnController = Factory.Instance.CreateRepairReturnController
    Private _ReturnAdvance As ReturnAdvance.IReturnAdvanceController = Factory.Instance.CreateReturnAdvanceController
    Private _SaleLooseDiamondHeaderController As SaleLooseDiamond.ISaleLooseDiamondController = Factory.Instance.CreateSaleLooseDiamondController

    Dim _IsUpdate As Boolean = False
    Dim TotalPayAmount As Integer
    Dim PaidAmount As Integer
    Dim Type As String = ""
    Dim VoucherNo As String
    Dim argType As String


    Private Sub frm_CashReceiptOnCredit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "အကြွေးပေး/ရ စာရင်းထည့်ခြင်း"
        lblCurrentLocationName.Text = Global_CurrentLocationName
        lblLogInUserName.Text = Global_UserID
        rbtSaleInvoice.Checked = True
        'lblCustomer.Text = "ရောင်းသူ"
        chkIsCash.Checked = False
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
    Private Sub btnRAdvance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRAdvance.Click
        Dim DataItem As DataRow
        Dim dtReturnAdvance As New DataTable
        Dim objReturnAdvance As New ReturnAdvanceInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        dtReturnAdvance = _ReturnAdvance.GetAllReturnAdvanceInCashReceipt()
        DataItem = DirectCast(SearchData.FindFast(dtReturnAdvance, "ReturnAdvance List"), DataRow)
        If DataItem IsNot Nothing Then
            _ReturnAdvanceID = DataItem.Item("VoucherNo").ToString()
            objReturnAdvance = _ReturnAdvance.GetReturnAdvance(_ReturnAdvanceID)
            showReturnAdvanceData(objReturnAdvance)
        End If
    End Sub
    Public Sub showReturnAdvanceData(ByVal objReturnAdvance As CommonInfo.ReturnAdvanceInfo)
        With objReturnAdvance
            txtPayAmt.Text = .TotalAmount
        End With
    End Sub
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        ''If _CashReceiptController.DeleteCashReceipt(txtVoucherNo.Text) Then
        ''    Clear()

        ''    Return True
        ''Else
        ''    Return False
        ''End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        chkIsCash.Checked = False
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If dtCashReceipt.Rows.Count > 0 Then
            If _VoucherNo <> "" Then
                If _CashReceiptController.SaveCashReceipt(_VoucherNo, Type, dtCashReceipt) Then
                    If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                        Dim frmPrint As New frm_ReportViewer
                        Dim dt As New DataTable
                        Dim PaidAmount As Integer = 0
                        dt = _CashReceiptController.GetCashReceiptforPrint(_VoucherNo, Type)
                        If dt.Rows.Count > 0 Then
                            PaidAmount = dt.Rows(0).Item("PaidAmount")

                            For i As Integer = 0 To dt.Rows.Count - 1
                                PaidAmount = PaidAmount + dt.Rows(i).Item("PayAmount")
                            Next
                            dt.Rows(0).Item("AllPaidAmount") = PaidAmount
                        End If
                        frmPrint.PrintCashReceipt(dt)
                        chkIsCash.Checked = False
                        Clear()
                        frmPrint.WindowState = FormWindowState.Maximized
                        frmPrint.Show()
                    Else
                        chkIsCash.Checked = False
                        Clear()
                        Return True
                    End If

                Else
                    Return False
                End If
            End If
        Else
            MsgBox("Please Insert Data !", MsgBoxStyle.Information, AppName)
            txtPayAmt.Focus()
            ' Clear()
        End If
    End Function

    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        SearchButton.Focus()

        _ReturnAdvanceID = ""
        _CashReceiptID = ""
        _PurchaseHeaderID = ""
        _SaleInvoiceHeaderID = ""
        _OrderReturnHeaderID = ""
        _SalesGemsID = ""
        _SalesVolumeID = ""
        _VoucherNo = ""
        txtVoucherNo.Text = ""
        txtDate.Text = ""
        txtCustomer.Text = ""
        txtTotalAmt.Text = "0"
        txtPaidAmt.Text = "0"
        txtBalanceAmt.Text = "0"
        PaidAmount = 0
        Type = ""
        dtCashReceipt = New DataTable
        dtCashReceipt.Columns.Add("@CashReceiptID", System.Type.GetType("System.String"))
        dtCashReceipt.Columns.Add("ReceiptDate", System.Type.GetType("System.DateTime"))
        dtCashReceipt.Columns.Add("Amount", System.Type.GetType("System.Int64"))
        dtCashReceipt.Columns.Add("Remark", System.Type.GetType("System.String"))
        dtCashReceipt.Columns.Add("IsBank", System.Type.GetType("System.Boolean"))
        dtCashReceipt.Columns.Add("VoucherNo", System.Type.GetType("System.String"))


        grdCashReceipt.AutoGenerateColumns = False
        grdCashReceipt.ReadOnly = True
        grdCashReceipt.DataSource = dtCashReceipt

        FormatGridCashReceipt()
        ClearItem()

    End Sub
    Private Sub ClearItem()
        _IsUpdate = False
        dtpPayDate.Value = Now
        txtPayAmt.Text = ""
        txtRemark.Clear()
        btnAdd.Text = "&Add"
        TotalPayAmount = 0
        chkIsBank.Checked = False
        'PaidAmount = 0
    End Sub
    Private Sub FormatGridCashReceipt()
        With grdCashReceipt
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcID As New DataGridViewTextBoxColumn()
            dcID.HeaderText = "CashReceiptID"
            dcID.DataPropertyName = "@CashReceiptID"
            dcID.Name = "CashReceiptID"
            dcID.Visible = False
            .Columns.Add(dcID)

            Dim dc1 As New DataGridViewTextBoxColumn()
            dc1.HeaderText = "ReceiptDate"
            dc1.DataPropertyName = "ReceiptDate"
            dc1.Name = "ReceiptDate"
            dc1.Width = 100
            dc1.Visible = True
            dc1.SortMode = DataGridViewColumnSortMode.NotSortable
            dc1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            dc1.DefaultCellStyle.Format = "dd-MM-yyyy"
            .Columns.Add(dc1)


            Dim dc2 As New DataGridViewTextBoxColumn()
            dc2.HeaderText = "Amount"
            dc2.DataPropertyName = "Amount"
            dc2.Name = "Amount"
            dc2.Width = 102
            dc2.Visible = True
            'dc2.DefaultCellStyle.Format = "###,##0.##"
            dc2.SortMode = DataGridViewColumnSortMode.NotSortable
            dc2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            .Columns.Add(dc2)

            Dim dc3 As New DataGridViewTextBoxColumn()
            dc3.HeaderText = "Remark"
            dc3.DataPropertyName = "Remark"
            dc3.Name = "Remark"
            dc3.Width = 150
            dc3.Visible = True
            dc3.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dc3.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc3)

            Dim dcIsBank As New DataGridViewTextBoxColumn()
            With dcIsBank
                .HeaderText = "IsBank"
                .DataPropertyName = "IsBank"
                .Name = "IsBank"
                .Visible = False
            End With
            .Columns.Add(dcIsBank)

            Dim dc4 As New DataGridViewTextBoxColumn()
            dc4.HeaderText = "VoucherNo"
            dc4.DataPropertyName = "VoucherNo"
            dc4.Name = "VoucherNo"
            dc4.Width = 150
            dc4.Visible = True
            dc4.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dc4.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc4)
        End With
    End Sub

    Private Sub grdCashReceipt_Click(sender As Object, e As EventArgs) Handles grdCashReceipt.Click
        If (grdCashReceipt.RowCount = 0) Then
            _IsUpdate = False
            Exit Sub
        End If
        If IsDBNull(grdCashReceipt.CurrentRow.Cells("CashReceiptID").Value) = False Then
            ' If _IsUpdate = True Then
            With grdCashReceipt
                _CashReceiptID = IIf(IsDBNull(.CurrentRow.Cells("CashReceiptID").Value), "-", .CurrentRow.Cells("CashReceiptID").Value)
                dtpPayDate.Value = .CurrentRow.Cells("ReceiptDate").Value
                txtPayAmt.Text = IIf(IsDBNull(.CurrentRow.Cells("Amount").Value), 0, .CurrentRow.Cells("Amount").Value)
                txtRemark.Text = IIf(IsDBNull(.CurrentRow.Cells("Remark").Value), "-", .CurrentRow.Cells("Remark").Value)
                chkIsBank.Checked = .CurrentRow.Cells("IsBank").Value
                _ReturnAdvanceID = IIf(IsDBNull(.CurrentRow.Cells("VoucherNo").Value), "-", .CurrentRow.Cells("VoucherNo").Value)
            End With

            btnAdd.Text = "&Update"
            _IsUpdate = True
        Else
            ClearItem()
        End If
    End Sub

    Private Sub grdCashReceipt_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdCashReceipt.RowsRemoved
        ClearItem()
        'If grdCashReceipt.RowCount > 0 Then
        CalculateTotalPayAmount()
        txtPaidAmt.Text = PaidAmount + CStr(TotalPayAmount)
        'End If

    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        If (txtVoucherNo.Text = "") Then
            MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Else
            MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        End If
        Clear()
        Dim dt As New DataTable
        Dim DataItem As DataRow
        Dim objCashReceipt As New CashReceiptInfo
        Dim objPurHeader As New PurchaseHeaderInfo
        Dim objSaleHeader As New SaleInvoiceHeaderInfo
        Dim objWholeSaleHeader As New WholeSaleInvoiceInfo
        Dim objConsignmentSale As New ConsignmentSaleInfo
        Dim objOrderReturn As New OrderReturnHeader
        Dim objRepairReturn As New RepairReturnHeaderInfo
        Dim objCustomer As New CustomerInfo
        Dim objSaleGems As New SaleGemsInfo
        Dim objSalesVolume As New SalesVolumeHeaderInfo
        Dim objSaleLooseDiamond As New SaleLooseDiamondHeaderInfo
        PaidAmount = 0
        TotalPayAmount = 0
        If rbtSaleInvoice.Checked Then
            dt = _CashReceiptController.GetSaleInvoiceHeaderCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(SearchData.FindFast(dt, "Sale Invoice List"), DataRow)
            If DataItem IsNot Nothing Then
                TotalPayAmount = 0
                _SaleInvoiceHeaderID = DataItem.Item("VoucherNo").ToString()
                objSaleHeader = _SaleInvoiceHeaderController.GetSaleInvoiceHeaderByID(_SaleInvoiceHeaderID)
                With objSaleHeader
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.SalesInvoice.ToString
                    txtVoucherNo.Text = _SaleInvoiceHeaderID
                    _VoucherNo = _SaleInvoiceHeaderID
                    txtDate.Text = Format(.SaleDate, "dd-MM-yyyy")
                    _CustomerID = .CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = (.TotalAmount + .AddOrSub + .AllTaxAmt) - (.PromotionAmount + .DiscountAmount + .RedeemValue + .MemberDiscountAmt)
                    txtPaidAmt.Text = .PaidAmount + .PurchaseAmount + .AllAdvanceAmount + .OtherCashAmount
                    PaidAmount = .PaidAmount + .PurchaseAmount + .AllAdvanceAmount + .OtherCashAmount
                    dtCashReceipt = _CashReceiptController.GetCashReceipt(txtVoucherNo.Text, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        ElseIf rbtWholeSaleInvoice.Checked Then
            dt = _CashReceiptController.GetWholeSaleInvoiceHeaderCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(SearchData.FindFast(dt, "WholeSale Invoice List"), DataRow)
            If DataItem IsNot Nothing Then
                TotalPayAmount = 0
                _WholeSaleInvoiceID = DataItem.Item("VoucherNo").ToString()
                objWholeSaleHeader = _WholesaleInvoiceController.GetWholeSaleInvoiceByID(_WholeSaleInvoiceID)
                With objWholeSaleHeader
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.WholeSalesInvoice.ToString
                    txtVoucherNo.Text = _WholeSaleInvoiceID
                    _VoucherNo = _WholeSaleInvoiceID
                    txtDate.Text = Format(.WDate, "dd-MM-yyyy")
                    _CustomerID = .CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = (.NetAmount + .AddOrSub) - (.Discount + .RedeemValue + .MemberDiscountAmt)
                    txtPaidAmt.Text = .PaidAmount
                    PaidAmount = .PaidAmount
                    dtCashReceipt = _CashReceiptController.GetCashReceipt(txtVoucherNo.Text, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        ElseIf rbtConsignmentSale.Checked Then
            dt = _CashReceiptController.GetConsignmentSaleCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(SearchData.FindFast(dt, "ConsignmentSale Invoice List"), DataRow)
            If DataItem IsNot Nothing Then
                TotalPayAmount = 0
                _ConsignmentSaleID = DataItem.Item("VoucherNo").ToString()
                objConsignmentSale = _ConsignmentSaleController.GetConsignmentSaleByID(_ConsignmentSaleID)
                With objConsignmentSale
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.ConsignmentSaleInvoice.ToString
                    txtVoucherNo.Text = _ConsignmentSaleID
                    _VoucherNo = _ConsignmentSaleID
                    txtDate.Text = Format(.ConsignDate, "dd-MM-yyyy")
                    _CustomerID = .CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = (.NetAmount + .AddOrSub) - (.Discount + .RedeemValue + .MemberDiscountAmt)
                    txtPaidAmt.Text = .PaidAmount
                    PaidAmount = .PaidAmount
                    dtCashReceipt = _CashReceiptController.GetCashReceipt(txtVoucherNo.Text, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        ElseIf rbtOrder.Checked Then
            dt = _CashReceiptController.GetOrderCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(Search.FindFast(dt, "CashReceipt List"), DataRow)
            If DataItem IsNot Nothing Then
                TotalPayAmount = 0
                _OrderReturnHeaderID = DataItem.Item("@OrderReturnHeaderID")
                objOrderReturn = _OrderInvoiceController.GetOrderInvoiceReturnHeader(_OrderReturnHeaderID)
                With objOrderReturn
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.OrderInvoice.ToString
                    txtVoucherNo.Text = .OrderInvoiceID
                    _VoucherNo = .OrderInvoiceID
                    txtDate.Text = Format(.ReturnDate, "dd-MM-yyyy")
                    _CustomerID = _OrderInvoiceController.GetOrderInvoice(.OrderInvoiceID).CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = DataItem.Item("NetAmount")
                    txtPaidAmt.Text = (.PaidAmount + .FromGoldAmount + .AdvanceAmount)
                    PaidAmount = (.PaidAmount + .FromGoldAmount + .AdvanceAmount)

                    dtCashReceipt = _CashReceiptController.GetCashReceipt(_VoucherNo, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        ElseIf rbtRepairReturn.Checked Then
            dt = _CashReceiptController.GetReturnRepairStockCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(SearchData.FindFast(dt, "CashReceipt List"), DataRow)
            If DataItem IsNot Nothing Then
                TotalPayAmount = 0
                _RepairReturnHeaderID = DataItem.Item("@ReturnRepairID")
                objRepairReturn = _RepairReturnController.GetRepairReturnHeaderInfoByID(_RepairReturnHeaderID)
                With objRepairReturn
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.RepairReturn.ToString
                    txtVoucherNo.Text = .RepairID
                    _VoucherNo = .RepairID
                    txtDate.Text = Format(.ReturnDate, "dd-MM-yyyy")
                    _CustomerID = .CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = DataItem.Item("NetAmount")
                    txtPaidAmt.Text = (.ReturnPaidAmount + .AdvanceAmount)
                    PaidAmount = (.ReturnPaidAmount + .AdvanceAmount)
                    dtCashReceipt = _CashReceiptController.GetCashReceipt(_VoucherNo, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        ElseIf rbtSalesGems.Checked Then
            dt = _CashReceiptController.GetSaleGemsCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(SearchData.FindFast(dt, "CashReceipt List"), DataRow)
            If DataItem IsNot Nothing Then
                TotalPayAmount = 0
                _SalesGemsID = DataItem.Item("VoucherNo").ToString()
                objSaleGems = _SalesGemsController.GetSaleGems(_SalesGemsID)
                With objSaleGems
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.SalesGems.ToString
                    txtVoucherNo.Text = _SalesGemsID
                    _VoucherNo = _SalesGemsID
                    txtDate.Text = Format(.SDate, "dd-MM-yyyy")
                    'txtCustomer.Text = .Customer
                    _CustomerID = .CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = (.TotalAmount + .AddOrSub) - (.PromotionAmount + .DiscountAmount + .PurchaseAmount)
                    txtPaidAmt.Text = .PaidAmount
                    PaidAmount = .PaidAmount
                    dtCashReceipt = _CashReceiptController.GetCashReceipt(txtVoucherNo.Text, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        ElseIf rbtSaleInvoiceVolume.Checked Then
            dt = _CashReceiptController.GetSaleInvoiceVolumeCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(SearchData.FindFast(dt, "Sale Volume List"), DataRow)
            If DataItem IsNot Nothing Then

                TotalPayAmount = 0
                _SalesVolumeID = DataItem.Item("VoucherNo").ToString()
                objSalesVolume = _SaleVolumeController.GetSalesVolumeHeaderByID(_SalesVolumeID)
                With objSalesVolume
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.SalesInvoiceVolume.ToString
                    txtVoucherNo.Text = _SalesVolumeID
                    _VoucherNo = _SalesVolumeID
                    txtDate.Text = Format(.SaleDate, "dd-MM-yyyy")
                    _CustomerID = .CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = (.TotalAmount + .AddOrSub) - (.PromotionAmount + .DiscountAmount + .RedeemValue + .MemberDiscountAmt)
                    txtPaidAmt.Text = .PaidAmount
                    PaidAmount = .PaidAmount

                    dtCashReceipt = _CashReceiptController.GetCashReceipt(txtVoucherNo.Text, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        ElseIf RadLooseDiamond.Checked Then
            dt = _CashReceiptController.GetSaleLooseDiamondHeaderCashReceipt(chkIsCash.Checked)
            DataItem = DirectCast(SearchData.FindFast(dt, "Sale Loose Diamond List"), DataRow)
            If DataItem IsNot Nothing Then
                TotalPayAmount = 0
                _SaleLooseDiamondID = DataItem.Item("VoucherNo").ToString()
                objSaleLooseDiamond = _SaleLooseDiamondHeaderController.GetSaleLooseDiamondHeaderByID(_SaleLooseDiamondID)
                With objSaleLooseDiamond
                    Type = CommonInfo.EnumSetting.CardReceiptOnCredit.SaleLooseDiamond.ToString
                    txtVoucherNo.Text = _SaleLooseDiamondID
                    _VoucherNo = _SaleLooseDiamondID
                    txtDate.Text = Format(.SaleDate, "dd-MM-yyyy")
                    _CustomerID = .CustomerID
                    txtCustomer.Text = DataItem.Item("Customer_")
                    txtTotalAmt.Text = (.TotalAmount + .AddOrSub + .AllTaxAmt) - (.PromotionAmount + .DiscountAmount + .RedeemValue + .MemberDiscountAmt)
                    txtPaidAmt.Text = .PaidAmount + .PurchaseAmount + .OtherCashAmount
                    PaidAmount = .PaidAmount + .PurchaseAmount + .OtherCashAmount
                    dtCashReceipt = _CashReceiptController.GetCashReceipt(txtVoucherNo.Text, Type)
                    If Not IsDBNull(dtCashReceipt) Then
                        If dtCashReceipt.Rows.Count <> 0 Then
                            grdCashReceipt.DataSource = dtCashReceipt
                            CalculateTotalPayAmount()
                        End If
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End With
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim drCashReceipt As DataRow
        If txtVoucherNo.Text <> "" Then
            If IsFillData() Then
                If _IsUpdate Then
                    drCashReceipt = dtCashReceipt.Rows(grdCashReceipt.CurrentRow.Index)
                    If txtPayAmt.Text <> "" Then
                        drCashReceipt.Item("@CashReceiptID") = _CashReceiptID
                        drCashReceipt.Item("ReceiptDate") = dtpPayDate.Value
                        drCashReceipt.Item("Amount") = txtPayAmt.Text
                        drCashReceipt.Item("Remark") = IIf(txtRemark.Text = "", "-", txtRemark.Text)
                        drCashReceipt.Item("IsBank") = chkIsBank.Checked
                        drCashReceipt.Item("VoucherNo") = IIf(_ReturnAdvanceID = "", "-", _ReturnAdvanceID)
                        grdCashReceipt.DataSource = dtCashReceipt
                        CalculateTotalPayAmount()
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                    'End If
                Else
                    drCashReceipt = dtCashReceipt.NewRow
                    If txtPayAmt.Text <> "" Then
                        ''_CashReceiptID = _GeneralCon.GetGenerateKey(EnumSetting.GenerateKeyType.CashReceiptOnCredit, EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString, dtpPayDate.Value)

                        drCashReceipt.Item("@CashReceiptID") = _CashReceiptID
                        drCashReceipt.Item("ReceiptDate") = dtpPayDate.Value
                        drCashReceipt.Item("Amount") = txtPayAmt.Text
                        drCashReceipt.Item("Remark") = IIf(txtRemark.Text = "", "-", txtRemark.Text)
                        drCashReceipt.Item("IsBank") = chkIsBank.Checked
                        drCashReceipt.Item("VoucherNo") = IIf(_ReturnAdvanceID = "", "-", _ReturnAdvanceID)
                        dtCashReceipt.Rows.Add(drCashReceipt)
                        grdCashReceipt.DataSource = dtCashReceipt
                        CalculateTotalPayAmount()
                    End If
                    txtPaidAmt.Text = PaidAmount + TotalPayAmount
                End If
                ClearItem()
            End If
        End If
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearItem()
    End Sub
    Private Function IsFillData() As Boolean
        Dim RAdvanceCount As Integer = 0
        Dim RAdID As String = ""
        If txtPayAmt.Text = "" Then
            MsgBox("Select Pay Amount !", MsgBoxStyle.Information, AppName)
            txtPayAmt.Focus()
            Return False
        End If

        If _IsUpdate = False Then
            If dtCashReceipt.Rows.Count > 0 Then
                For i As Integer = 0 To dtCashReceipt.Rows.Count - 1
                    RAdID = IIf(IsDBNull(dtCashReceipt.Rows(i).Item("VoucherNo")), "-", dtCashReceipt.Rows(i).Item("VoucherNo"))
                    If RAdID = "" Then RAdID = "-"
                    If (RAdID <> "-") Then
                        If _ReturnAdvanceID = RAdID Then
                            RAdvanceCount += 1
                        End If
                    End If

                Next
            End If
        End If
        If RAdvanceCount > 0 Then
            MsgBox("Already exist ReturnAdvance Voucher!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        Return True
    End Function

    Private Sub CalculateTotalPayAmount()
        TotalPayAmount = 0
        For i As Integer = 0 To grdCashReceipt.RowCount - 1
            If Not grdCashReceipt.Rows(i).IsNewRow Then
                TotalPayAmount += CDec(Val(grdCashReceipt.Rows(i).Cells("Amount").FormattedValue))
            End If
        Next
    End Sub

    Private Sub txtPaidAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaidAmt.TextChanged
        If txtTotalAmt.Text <> "" And txtPaidAmt.Text <> "" Then
            txtBalanceAmt.Text = CLng(txtTotalAmt.Text) - CLng(txtPaidAmt.Text)
        Else
            txtBalanceAmt.Text = "0"
        End If
    End Sub
    Private Sub txtPayAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPayAmt.KeyPress
        MyBase.ValidateNumericAllowMinus(sender, e, True)
        _ReturnAdvanceID = "-"
    End Sub

    Private Sub txtTotalAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAmt.TextChanged
        If txtTotalAmt.Text <> "" And txtPaidAmt.Text <> "" Then
            txtBalanceAmt.Text = CLng(txtTotalAmt.Text) - CLng(txtPaidAmt.Text)
        Else
            txtBalanceAmt.Text = "0"
        End If
    End Sub

    Private Sub rbtPurchaseInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles rbtRepairReturn.CheckedChanged
        If rbtRepairReturn.Checked Then
            chkIsCash.Checked = False
            lblCustomer.Text = "ရောင်းသူ"
        Else
            lblCustomer.Text = "   ဝယ်သူ"
        End If
    End Sub

    Private Sub rbtSaleInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles rbtSaleInvoice.CheckedChanged
        If rbtSaleInvoice.Checked Then
            chkIsCash.Checked = False
            lblCustomer.Text = "   ဝယ်သူ"
        Else
            lblCustomer.Text = "ရောင်းသူ"
        End If
    End Sub

    Private Sub rbtOrder_CheckedChanged(sender As Object, e As EventArgs) Handles rbtOrder.CheckedChanged
        If rbtOrder.Checked Then
            chkIsCash.Checked = False
            lblCustomer.Text = "   ဝယ်သူ"
        Else
            lblCustomer.Text = "ရောင်းသူ"
        End If
    End Sub

    Private Sub rbtSalesGems_CheckedChanged(sender As Object, e As EventArgs) Handles rbtSalesGems.CheckedChanged
        If rbtSalesGems.Checked Then
            chkIsCash.Checked = False
            lblCustomer.Text = "   ဝယ်သူ"
        Else
            lblCustomer.Text = "ရောင်းသူ"
        End If

    End Sub

    Private Sub rbtSaleInvoiceVolume_CheckedChanged(sender As Object, e As EventArgs) Handles rbtSaleInvoiceVolume.CheckedChanged
        If rbtSaleInvoiceVolume.Checked Then
            chkIsCash.Checked = False
            lblCustomer.Text = "   ဝယ်သူ"
        Else
            lblCustomer.Text = "ရောင်းသူ"
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("CashReceiptonCredit")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim PaidAmount As Integer = 0
        dt = _CashReceiptController.GetCashReceiptforPrint(txtVoucherNo.Text, Type)
        If dt.Rows.Count > 0 Then
            PaidAmount = dt.Rows(0).Item("PaidAmount")
            For i As Integer = 0 To dt.Rows.Count - 1
                PaidAmount = PaidAmount + dt.Rows(i).Item("PayAmount")
            Next
            dt.Rows(0).Item("AllPaidAmount") = PaidAmount
        End If
        frmPrint.PrintCashReceipt(dt)
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub


    Private Sub rbtWholeSaleInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles rbtWholeSaleInvoice.CheckedChanged
        If rbtWholeSaleInvoice.Checked Then
            chkIsCash.Checked = False
            lblCustomer.Text = "   ဝယ်သူ"
        Else
            lblCustomer.Text = "ရောင်းသူ"
        End If

    End Sub

    Private Sub rbtConsignmentSale_CheckedChanged(sender As Object, e As EventArgs) Handles rbtConsignmentSale.CheckedChanged
        If rbtConsignmentSale.Checked Then
            chkIsCash.Checked = False
            lblCustomer.Text = "   ဝယ်သူ"
        Else
            lblCustomer.Text = "ရောင်းသူ"
        End If
    End Sub

End Class
