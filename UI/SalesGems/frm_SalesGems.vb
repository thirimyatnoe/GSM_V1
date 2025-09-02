Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
'Imports System.String
Imports System.Windows
Imports Operational.AppConfiguration
Imports ThoughtWorks.QRCode.Codec

Public Class frm_SalesGems

    Implements IFormProcess
    Private _SaleGemsID As String
    Private _SaleGemsItemID As String
    Private _StaffID As String
    Private _LocationID As String
    Private _GemsCategoryID As String
    Private GemTK As Decimal = 0.0
    Private GemTG As Decimal = 0.0
    Private GemTW As Decimal = 0.0
    Private TotalTK As Decimal = 0.0
    Private TotalTW As Decimal = 0.0
    Private _TotalTG As Decimal = 0.0
    Private _TotalGemsTK As Decimal = 0.0
    Private _TotalGemsTG As Decimal = 0.0
    Private _CustomerID As String = ""
    Private _CurrentQTY As Integer = 0
    Private _OldQTY As Integer = 0
    Private _IsAllowDiscount As Boolean = False
    Private _IsCustomerName As Boolean = False
    Private _IsCustomerCode As Boolean = False
    Private _dtOtherCash As DataTable
    Private _PurchaseHeaderID As String = ""

    Private _dtSaleGemsItem As New DataTable
    Private _Staff As Staff.IStaffController = Factory.Instance.CreateStaffController
    Private _ConverterController As Converter.IConverterController = Factory.Instance.CreateConverterController
    Private _GemsCategoryController As GemsCategory.IGemsCategoryController = Factory.Instance.CreateGemsCategoryController
    Private _SaleGems As SaleGems.ISaleGemsController = Factory.Instance.CreateSaleGemsController
    Private _CustomerController As BusinessRule.Customer.ICustomerController = Factory.Instance.CreatecustomerController
    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Dim IsCheck As Boolean = False ' For Gems
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

    'Private Sub frm_SalesGems_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    '    GetStaffByLocation(_LocationID)
    'End Sub
    Private Sub frm_SaleGems_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "ကျောက်ဝယ်ခြင်း"
        'MyBase.lblLogInUserName.Text = Global_CurrentUser
        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        MyBase.addGridDataErrorHandlers(grdCategory)
        GetStaffByLocation()
        ClearData()
        _LocationID = Global_CurrentLocationID
        Me.KeyPreview = True
    End Sub

    Private Sub frm_SaleGems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        dt = objGeneralController.CheckExitVoucherForCashReceipt("tbl_CashReceipt", "AND VoucherNo='" + _SaleGemsID + "' AND Type='SalesGems'")
        If dt.Rows.Count() > 0 Then
            MsgBox("This VoucherNo is Use in CashReceipt!", MsgBoxStyle.Information, "")
            Return False
            Exit Function
        End If

        If _SaleGems.DeleteSaleGems(_SaleGemsID) Then
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
        If IsFillData() Then
            Dim objSaleGems As New SaleGemsInfo
            objSaleGems = GetDataSaleGems()
            If _SaleGems.SaveSaleGems(objSaleGems, _dtSaleGemsItem, _dtOtherCash) Then
                _SaleGemsID = objSaleGems.SaleGemsID
                If (MsgBox("Do You Want To Save And Print Sale Voucher", MsgBoxStyle.OkCancel, AppName) = MsgBoxResult.Ok) Then
                    'QRCode
                    Dim objQRCode As QRCodeEncoder = New QRCodeEncoder()
                    objQRCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
                    objQRCode.QRCodeScale = 3
                    objQRCode.QRCodeVersion = 7
                    objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H

                    Dim frmPrint As New frm_ReportViewer
                    Dim dt As New DataTable
                    Dim dtOtherCash As New DataTable
                    Dim dtPurchaseGems As New DataTable
                    dt = _SaleGems.GetSaleGemsPrint(_SaleGemsID)
                    dtPurchaseGems = _ObjPurchaseController.GetAllPurchaseGemsPrint(_SaleGemsID)


                    dtOtherCash = _SaleGems.GetOtherCashDataByVoucherNo(_SaleGemsID)
                    frmPrint.PrintSaleGems(dt, dtPurchaseGems, dtOtherCash)
                    For Each dr As DataRow In dt.Rows
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
                   

                    ClearData()
                    frmPrint.WindowState = FormWindowState.Maximized
                    frmPrint.Show()
                    ClearData()
                Else
                    ClearData()
                    Return True
                End If
            Else
                Return False
            End If
        End If
    End Function

    Private Function IsFillData() As Boolean
        If _dtSaleGemsItem.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If


        If _dtSaleGemsItem.Rows.Count <= 0 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        If grdCategory.Rows.Count <= 1 Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If

        For Each drDetail As DataRow In _dtSaleGemsItem.Rows
            If drDetail.RowState <> DataRowState.Deleted Then
                If IsDBNull(drDetail("GemsCategoryID")) Then
                    MsgBox("Please Select Gem Category!", MsgBoxStyle.Information, AppName)
                    grdCategory.Focus()
                    Return False
                End If
            End If
        Next

        If cboStaff.SelectedIndex = -1 Then
            MsgBox("Select Staff !", MsgBoxStyle.Information, AppName)
            cboStaff.Focus()
            Return False
        End If

        If _CustomerID = "" Then
            MsgBox("Please Select Customer !", MsgBoxStyle.Information, AppName)
            txtCustomerCode.Focus()
            Return False
        End If
        Return True
    End Function

    Public Function GetDataSaleGems() As SaleGemsInfo
        Dim objSaleGems = New SaleGemsInfo
        With objSaleGems
            .SaleGemsID = _SaleGemsID
            .SDate = dtpSaleDate.Value
            .staffID = cboStaff.SelectedValue
            .CustomerID = _CustomerID
            .Address = txtAddress.Text
            .TotalAmount = txtTotalAmt.Text
            .AddOrSub = txtAddOrSub.Text
            .PaidAmount = txtPaidAmt.Text
            .DiscountAmount = txtDiscountAmt.Text
            .Remark = IIf(txtRemark.Text = "", "-", txtRemark.Text)
            .PromotionDiscount = IIf(txtPromotionDis.Text = "", 0, txtPromotionDis.Text)
            .PurchaseHeaderID = IIf(txtPurchaseVoucherNo.Text = "", "", txtPurchaseVoucherNo.Text)
            .PurchaseAmount = IIf(txtPurchaseAmount.Text = "", 0, txtPurchaseAmount.Text)
            '.LastModifiedLoginUserName = lblLogInUserName.Text
            .IsOtherCash = chkOtherCash.Checked
            .OtherCashAmount = IIf(txtOtherCashAmount.Text = "", 0, txtOtherCashAmount.Text)
        End With

        Return objSaleGems

    End Function

    Public Sub showSaleGemsData(ByVal objSGems As CommonInfo.SaleGemsInfo)
        Dim objCustomer As New CustomerInfo
        With objSGems
            dtpSaleDate.Value = .SDate
            txtSalesGemID.Text = .SaleGemsID
            cboStaff.SelectedValue = .StaffID
            cboStaff.Text = _Staff.GetStaff(.StaffID).Staff
            _CustomerID = .CustomerID
            objCustomer = _CustomerController.GetCustomerByID(_CustomerID)
            txtCustomerCode.Text = objCustomer.CustomerCode
            txtCustomer.Text = objCustomer.CustomerName
            txtAddress.Text = objCustomer.CustomerAddress
            txtTotalAmt.Text = Format(Val(.TotalAmount), "###,##0.##")
            txtPromotionDis.Text = Format(Val(.PromotionDiscount), "###,##0.##")
            txtPromotionAmt.Text = Format(Val(.PromotionAmount), "###,##0.##")
            'txtOtherCashAmount.Text = Format(Val(.OtherCashAmount), "###,##0.##")
            txtPurchaseVoucherNo.Text = .PurchaseHeaderID
            _PurchaseHeaderID = .PurchaseHeaderID
            txtPurchaseAmount.Text = .PurchaseAmount
            _IsAllowDiscount = True
            txtDiscountAmt.Text = Format(Val(.DiscountAmount), "###,##0.##")
            txtAddOrSub.Text = Format(Val(.AddOrSub), "###,##0.##")
            txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text) + CLng(txtAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text))), "###,##0.##")
            txtPaidAmt.Text = Format(.PaidAmount, "###,##0.##")
            txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")

            If _PurchaseHeaderID <> "" Then
               
                txtDifferentAmount.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPurchaseAmount.Text)), "###,##0.##")
                txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            Else
                txtDifferentAmount.Text = "0"
                txtPurchaseAmount.Text = "0"
                txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If


            txtGemsTG.Text = Format(.GemsTG, "0.000")
            txtGemsK.Text = .GemsK
            txtGemsP.Text = .GemsP
            If Global_DecimalFormat = 1 Then
                txtGemsY.Text = Format(.GemsY + .GemsC, "0.0")
            Else
                txtGemsY.Text = Format(.GemsY + .GemsC, "0.00")
            End If
            _TotalGemsTG = .GemsTG
            txtRemark.Text = .Remark
            _IsAllowDiscount = False
            chkOtherCash.Checked = .IsOtherCash
            txtOtherCashAmount.Text = Format(Val(.OtherCashAmount), "###,##0.##")
        End With
    End Sub
    Private Sub SaleGemGenerateFormat()

        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.SalesGem.ToString)

        If objGenerateFormat.GenerateFormatID <> 0 Then
            txtSalesGemID.Text = objGeneralController.GetGenerateKeyForFormat(objGenerateFormat, dtpSaleDate.Value)
        Else
            MsgBox("Please Fill the format at Generate Format Form", MsgBoxStyle.OkOnly, AppName)
        End If

    End Sub

    Public Sub ClearData()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _SaleGemsID = "0"
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        SaleGemGenerateFormat()
        dtpSaleDate.Value = Now
        'cboStaff.SelectedValue = -1
        'cboStaff.Text = ""
        txtCustomerCode.Text = ""
        txtCustomer.Text = ""
        txtAddress.Text = ""
        txtRemark.Text = ""
        _CustomerID = ""
        _PurchaseHeaderID = ""
        txtPurchaseAmount.Text = "0"
        txtPurchaseVoucherNo.Text = "0"
        chkOtherCash.Checked = False
        If Global_UserLevel = "Administrator" Then
            txtPromotionDis.ReadOnly = False
            txtPromotionDis.BackColor = Color.White
        Else
            txtPromotionDis.ReadOnly = True
            txtPromotionDis.BackColor = Color.Linen
        End If

        txtDiscountAmt.Text = "0"
        txtPaidAmt.Text = "0"
        txtPromotionAmt.Text = "0"
        txtPromotionDis.Text = "0"
        txtBalanceAmt.Text = "0"
        txtTotalAmt.Text = "0"
        txtNetAmt.Text = "0"
        txtBalanceAmt.Text = "0"
        txtAddOrSub.Text = "0"
        _IsAllowDiscount = False
        txtOtherCashAmount.Text = "0"
        Dim dcOtherCash As New DataColumn
        _dtOtherCash = New DataTable
        _dtOtherCash.Columns.Add("RecordCashID", System.Type.GetType("System.String"))
        _dtOtherCash.Columns.Add("CashTypeID", System.Type.GetType("System.String"))
        _dtOtherCash.Columns.Add("ExchangeRate", System.Type.GetType("System.Int32"))
        _dtOtherCash.Columns.Add("Amount", System.Type.GetType("System.Int32"))
        _dtOtherCash.Columns.Add("Total", System.Type.GetType("System.Int64"))

        Dim dc As DataColumn
        _dtSaleGemsItem = New DataTable
        _dtSaleGemsItem.Columns.Add("SaleGemsItemID", System.Type.GetType("System.String"))
        _dtSaleGemsItem.Columns.Add("SaleGemsID", System.Type.GetType("System.String"))
        _dtSaleGemsItem.Columns.Add("GemsCategoryID", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "GemsK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtSaleGemsItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtSaleGemsItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GemsY"
        dc.DataType = System.Type.GetType("System.Decimal")
        If Global_DecimalFormat = 1 Then
            dc.DefaultValue = "0.0"
        Else
            dc.DefaultValue = "0.00"
        End If
        _dtSaleGemsItem.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "YOrCOrG"
        dc.DataType = System.Type.GetType("System.String")
        dc.DefaultValue = 0
        _dtSaleGemsItem.Columns.Add(dc)

        _dtSaleGemsItem.Columns.Add("Qty", System.Type.GetType("System.Int32"))
        _dtSaleGemsItem.Columns.Add("GemsName", System.Type.GetType("System.String"))
        _dtSaleGemsItem.Columns.Add("FixType", System.Type.GetType("System.String"))
        _dtSaleGemsItem.Columns.Add("GemsTK", System.Type.GetType("System.Decimal"))
        _dtSaleGemsItem.Columns.Add("GemsTG", System.Type.GetType("System.Decimal"))
        _dtSaleGemsItem.Columns.Add("SaleRate", System.Type.GetType("System.Int64"))

        dc = New DataColumn
        dc.ColumnName = "Amount"
        dc.DataType = System.Type.GetType("System.Int64")
        dc.DefaultValue = 0
        _dtSaleGemsItem.Columns.Add(dc)

        _dtSaleGemsItem.Columns.Add("GemsTW", System.Type.GetType("System.Decimal"))
        _dtSaleGemsItem.Columns.Add("IsReturn", System.Type.GetType("System.Boolean"))

        grdCategory.AutoGenerateColumns = False
        grdCategory.DataSource = _dtSaleGemsItem
        FormatGridSaleGems()


        GemTK = 0.0
        GemTW = 0.0
        GemTG = 0.0
        TotalTK = 0.0
        TotalTW = 0.0

        txtGemsTG.Text = "0.000"
        txtGemsK.Text = "0"
        txtGemsP.Text = "0"
        txtGemsY.Text = "0"
        _CustomerID = ""
        _OldQTY = 0
        _CurrentQTY = 0
        txtPurchaseVoucherNo.Text = ""
        txtPurchaseAmount.Text = ""
        txtDifferentAmount.Text = "0"
        btnOtherCash.Visible = False

    End Sub
#Region "FormatItemGrid"


    Public Sub FormatGridSaleGems()
        With grdCategory
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)

            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "SaleGemsItemID"
            dclID.DataPropertyName = "SaleGemsItemID"
            dclID.Name = "SaleGemsItemID"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcID As New DataGridViewTextBoxColumn()
            dcID.HeaderText = "SaleGemsID"
            dcID.DataPropertyName = "SaleGemsID"
            dcID.Name = "SaleGemsID"
            dcID.Visible = False
            .Columns.Add(dcID)

            Dim dcName As New DataGridViewComboBoxColumn()
            dcName.HeaderText = "Category"
            dcName.DataPropertyName = "GemsCategoryID"
            dcName.Name = "GemsCategoryID"
            dcName.DataSource = _GemsCategoryController.GetAllGemsCategoryForGridCombo
            dcName.DisplayMember = "GemsCategory"
            dcName.ValueMember = "@GemsCategoryID"
            dcName.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcName.Width = 150
            dcName.Visible = True
            .Columns.Add(dcName)




            Dim dc8 As New DataGridViewTextBoxColumn()
            dc8.HeaderText = "Description"
            dc8.DataPropertyName = "GemsName"
            dc8.Name = "GemsName"
            dc8.Width = 200
            dc8.Visible = True
            dc8.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dc8.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc8)

            'Dim dcClarity As New DataGridViewTextBoxColumn()
            'dcClarity.HeaderText = "Clarity"
            'dcClarity.DataPropertyName = "Clarity"
            'dcClarity.Name = "Clarity"
            'dcClarity.Width = 150
            'dcClarity.Visible = False
            'dcClarity.DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
            'dcClarity.SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns.Add(dcClarity)

            'Dim dcSizeMM As New DataGridViewTextBoxColumn()
            'dcSizeMM.HeaderText = "SizeMM"
            'dcSizeMM.DataPropertyName = "SizeMM"
            'dcSizeMM.Name = "SizeMM"
            'dcSizeMM.Width = 150
            'dcSizeMM.Visible = False
            'dcSizeMM.DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
            'dcSizeMM.SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns.Add(dcSizeMM)



            Dim dc6 As New DataGridViewTextBoxColumn()
            dc6.HeaderText = "RBP"
            dc6.DataPropertyName = "YOrCOrG"
            dc6.Name = "YOrCOrG"
            dc6.Width = 100
            dc6.Visible = True
            dc6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc6.SortMode = DataGridViewColumnSortMode.NotSortable
            dc6.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc6)

            Dim dc As New DataGridViewTextBoxColumn()
            dc.HeaderText = "K"
            dc.DataPropertyName = "GemsK"
            dc.Name = "GemsK"
            dc.Width = 35
            dc.Visible = True
            dc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc.SortMode = DataGridViewColumnSortMode.NotSortable
            dc.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc)


            Dim dc3 As New DataGridViewTextBoxColumn()
            dc3.HeaderText = "P"
            dc3.DataPropertyName = "GemsP"
            dc3.Name = "GemsP"
            dc3.Width = 35
            dc3.Visible = True
            dc3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc3.SortMode = DataGridViewColumnSortMode.NotSortable
            dc3.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc3)

            Dim dc4 As New DataGridViewTextBoxColumn()
            dc4.HeaderText = "Y"
            dc4.DataPropertyName = "GemsY"
            dc4.Name = "GemsY"
            dc4.Width = 35
            dc4.Visible = True
            If Global_DecimalFormat = 1 Then
                dc4.DefaultCellStyle.Format = "0.0"
            Else
                dc4.DefaultCellStyle.Format = "0.00"
            End If
            dc4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc4.SortMode = DataGridViewColumnSortMode.NotSortable
            dc4.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc4)


            Dim dc7 As New DataGridViewTextBoxColumn()
            dc7.HeaderText = "QTY"
            dc7.DataPropertyName = "Qty"
            dc7.Name = "Qty"
            dc7.Width = 50
            dc7.Visible = True
            dc7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dc7.SortMode = DataGridViewColumnSortMode.NotSortable
            dc7.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc7)

            Dim dc9 As New DataGridViewComboBoxColumn()
            dc9.HeaderText = "Fix"
            dc9.DataPropertyName = "FixType"
            dc9.Name = "FixType"
            dc9.Width = 95
            dc9.Visible = True
            dc9.Items.AddRange(New String() {"Fix", "ByWeight", "ByQty"})
            dc9.SortMode = DataGridViewColumnSortMode.NotSortable
            dc9.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dc9)

            Dim dcUP As New DataGridViewTextBoxColumn()
            dcUP.HeaderText = "UnitPrice"
            dcUP.DataPropertyName = "SaleRate"
            dcUP.Name = "SaleRate"
            dcUP.Width = 90
            dcUP.Visible = True
            dcUP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dcUP.SortMode = DataGridViewColumnSortMode.NotSortable
            dcUP.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dcUP)

            Dim dcAmt As New DataGridViewTextBoxColumn()
            dcAmt.HeaderText = "Amount"
            dcAmt.DataPropertyName = "Amount"
            dcAmt.Name = "Amount"
            dcAmt.Width = 90
            dcAmt.Visible = True
            dcAmt.ReadOnly = True
            dcAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dcAmt.DefaultCellStyle.Format = "###,##0.##"
            dcAmt.SortMode = DataGridViewColumnSortMode.NotSortable
            dcAmt.DefaultCellStyle.Font = New Font("Tahoma", 8.25)
            .Columns.Add(dcAmt)

            Dim dc10 As New DataGridViewTextBoxColumn()
            dc10.HeaderText = "GemsTW"
            dc10.DataPropertyName = "GemsTW"
            dc10.Name = "GemsTW"
            dc10.Visible = False
            dc10.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc10)


            Dim dc14 As New DataGridViewTextBoxColumn()
            dc14.HeaderText = "GemsTK"
            dc14.DataPropertyName = "GemsTK"
            dc14.Name = "GemsTK"
            dc14.Visible = False
            dc14.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc14)

            Dim dc16 As New DataGridViewTextBoxColumn()
            dc16.HeaderText = "GemsTG"
            dc16.DataPropertyName = "GemsTG"
            dc16.Name = "GemsTG"
            dc16.Visible = False
            dc16.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc16)

            Dim dc17 As New DataGridViewCheckBoxColumn()
            dc17.HeaderText = "IsReturn"
            dc17.DataPropertyName = "IsReturn"
            dc17.Name = "IsReturn"
            dc17.Visible = True
            dc17.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dc17)

        End With
    End Sub
#End Region

    'Public Sub GetCombo()
    '    cboSalesStaff.DisplayMember = "Staff_"
    '    cboSalesStaff.ValueMember = "StaffID"
    '    cboSalesStaff.DataSource = _Staff.GetStaffList().DefaultView
    'End Sub

    Private Sub GetStaffByLocation()
        cboStaff.DisplayMember = "Staff_"
        cboStaff.ValueMember = "StaffID"
        cboStaff.DataSource = _Staff.GetStaffList().DefaultView
        cboStaff.SelectedIndex = -1

    End Sub

    Private Sub grdCategory_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCategory.CellValidated

        If grdCategory.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then
            Select Case grdCategory.Columns(e.ColumnIndex).Name

                Case "SaleRate", "FixType", "Qty", "GemsK", "GemsP", "GemsY", "YOrCOrG"

                    If Not IsDBNull(grdCategory.Rows(e.RowIndex).Cells("FixType").Value) Then

                        If grdCategory.Rows(e.RowIndex).Cells("FixType").Value = "Fix" Then

                            If Not IsDBNull(grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value) Then
                                grdCategory.Rows(e.RowIndex).Cells("Amount").Value = grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value

                            End If

                        ElseIf grdCategory.Rows(e.RowIndex).Cells("FixType").Value = "ByWeight" Then
                            Dim _Type As Boolean = False
                            If Not (IsDBNull(grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value)) Then
                                If (IsDBNull(grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value)) Then
                                    _Type = True
                                ElseIf grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0" Then
                                    _Type = True
                                Else
                                    _Type = False
                                End If

                                If _Type = False Then
                                    grdCategory.Rows(e.RowIndex).Cells("Amount").Value = IIf(IsDBNull(grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value) = True, 0, grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value) * CLng(grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value)
                                Else
                                    grdCategory.Rows(e.RowIndex).Cells("Amount").Value = IIf(IsDBNull(grdCategory.Rows(e.RowIndex).Cells("GemsTK").Value) = True, 0, grdCategory.Rows(e.RowIndex).Cells("GemsTK").Value) * CLng(grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value)
                                End If

                            End If

                        Else
                            If Not IsDBNull(grdCategory.Rows(e.RowIndex).Cells("Qty").Value) Then

                                If Not IsDBNull(grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value) Then
                                    grdCategory.Rows(e.RowIndex).Cells("Amount").Value = CLng(grdCategory.Rows(e.RowIndex).Cells("SaleRate").Value) * CInt(grdCategory.Rows(e.RowIndex).Cells("Qty").Value)
                                End If
                            Else
                                MsgBox("Pls Fill Quatity", MsgBoxStyle.Information, Me.Text)


                            End If
                        End If
                    End If
            End Select
        End If
        CalculategrdTotalAmount()
    End Sub


    Private Sub grdCategory_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCategory.CellValueChanged
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        Dim ItemInfo As New CommonInfo.SaleGemsItemInfo
        If grdCategory.IsCurrentCellInEditMode = False Then Exit Sub

        If (e.RowIndex <> -1) Then



            If (grdCategory.Columns(e.ColumnIndex).Name = "GemsK" Or grdCategory.Columns(e.ColumnIndex).Name = "GemsP" Or grdCategory.Columns(e.ColumnIndex).Name = "GemsY") Then 'F
                With grdCategory
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("GemsK").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("GemsP").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("GemsY").Value) Then

                        If .Rows(e.RowIndex).Cells("GemsK").FormattedValue = "" Then
                            .Rows(e.RowIndex).Cells("GemsK").Value() = "0"
                        End If

                        If .Rows(e.RowIndex).Cells("GemsP").FormattedValue = "" Then
                            .Rows(e.RowIndex).Cells("GemsP").Value() = "0"
                        End If

                        If .Rows(e.RowIndex).Cells("GemsY").FormattedValue = "" Then
                            If Global_DecimalFormat = 1 Then
                                .Rows(e.RowIndex).Cells("GemsY").Value() = "0.0"
                            Else
                                .Rows(e.RowIndex).Cells("GemsY").Value() = "0.00"
                            End If
                        End If

                        GoldWeight.WeightK = CInt(Val(grdCategory.Rows(e.RowIndex).Cells("GemsK").FormattedValue))

                        If CInt(Val(grdCategory.Rows(e.RowIndex).Cells("GemsP").FormattedValue)) >= 16 Then
                            MsgBox("GemP should not be greater than 15", MsgBoxStyle.Information, AppName)
                            .Rows(e.RowIndex).Cells("GemsP").Value() = "0"
                        End If
                        GoldWeight.WeightP = CInt(Val(grdCategory.Rows(e.RowIndex).Cells("GemsP").FormattedValue))

                        If CDec(grdCategory.Rows(e.RowIndex).Cells("GemsY").FormattedValue) >= Global_PToY Then
                            MsgBox("GemY should not be greater than" & (Global_PToY - 0.1), MsgBoxStyle.Information, AppName)
                            .Rows(e.RowIndex).Cells("GemsY").Value() = "0"
                        End If

                        GoldWeight.WeightY = System.Decimal.Truncate(Val(grdCategory.Rows(e.RowIndex).Cells("GemsY").FormattedValue))
                        GoldWeight.WeightC = CDec(Val(grdCategory.Rows(e.RowIndex).Cells("GemsY").FormattedValue)) - GoldWeight.WeightY

                        GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
                        GemTK = GoldWeight.GoldTK
                        GoldWeight.Gram = GemTK * Global_KyatToGram
                        GemTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("GemsTG").Value() = GemTG
                        .Rows(e.RowIndex).Cells("GemsTK").Value() = GemTK
                    End If
                End With

            End If 'end Gemskpyc



            If grdCategory.Columns(e.ColumnIndex).Name = "YOrCOrG" Then  'For GemsWeight Yati,B,Karat

                If Not IsDBNull(grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value) Then
                    Dim equivalent As Decimal
                    Dim VarWeight As String
                    Dim VarWeightY As Integer
                    Dim VarWeightBCG As Decimal
                    Dim VarWeightP As Decimal
                    Dim TP As Decimal
                    Dim TY As Decimal
                    Dim TC As Decimal

                    Dim IsValid As Boolean
                    VarWeight = CStr(grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value)
                    'If VarWeight = "0" Then
                    '    Exit Sub
                    'End If

                    If Not VarWeight.EndsWith("ct") And Not VarWeight.EndsWith("G") And Not VarWeight.EndsWith("R") And Not VarWeight.EndsWith("B") And Not VarWeight.EndsWith("P") And Not VarWeight.ToString = "0" Then
                        MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                        grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                        grdCategory.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                        grdCategory.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                        If Global_DecimalFormat = 1 Then
                            grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                        Else
                            grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = "0.00"
                        End If

                        'ElseIf VarWeight.StartsWith("ct") Or VarWeight.EndsWith("G") Or VarWeight.StartsWith("R") Or VarWeight.StartsWith("B") Or VarWeight.StartsWith("P") Then
                        '    MsgBox("Please enter unit of Gems weight!", MsgBoxStyle.Information, "Data Require")
                        '    grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                        '    grdCategory.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                        '    grdCategory.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                        '    grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                    Else
                        If VarWeight.EndsWith("ct") Then
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 2)) Then
                                VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 2))
                                ''  grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG * 1.1) ' Notes: For Karat,multiply 1.1 
                                TC = CStr(VarWeightBCG)
                                If Global_IsCarat = 0 Or Global_IsCarat = 2 Then
                                    grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG)
                                Else
                                    grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG * 1.1)
                                End If
                                IsValid = True
                            Else
                                IsValid = False
                            End If

                        ElseIf VarWeight.EndsWith("G") Then
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                                VarWeightBCG = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                                If Global_IsCarat = 0 Or Global_IsCarat = 2 Then
                                    grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG)
                                Else
                                    grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = CStr(VarWeightBCG)
                                End If
                                IsValid = True
                            Else
                                IsValid = False
                            End If

                        ElseIf VarWeight.EndsWith("R") Then
                            If IsNumeric(LSet(VarWeight, Len(VarWeight) - 1)) Then
                                If VarWeight.IndexOf(".") = -1 Then
                                    VarWeightY = CInt(LSet(VarWeight, Len(VarWeight) - 1))
                                    '  grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightY
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = VarWeightY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightY
                                    End If
                                    IsValid = True
                                Else
                                    VarWeight = CDec(LSet(VarWeight, Len(VarWeight) - 1))
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = VarWeight / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeight
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
                                        ''  grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                        equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                        Else
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                    ''  grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = VarWeightBCG
                                    equivalent = Global_YatiToB '_ConverterCon.GetMeasurement("Yati", "B")
                                    TY = VarWeightY + (VarWeightBCG / equivalent)
                                    equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                    TC = TY / equivalent
                                    If Global_IsCarat = 2 Then
                                        grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                    Else
                                        grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                        '' grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                        equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                        Else
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                        ''grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                        equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                        Else
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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
                                        '' grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
                                        equivalent = Global_KaratToYati '_ConverterCon.GetMeasurement("Karat", "Yati")
                                        TC = TY / equivalent
                                        If Global_IsCarat = 2 Then
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TC
                                        Else
                                            grdCategory.Rows(e.RowIndex).Cells("GemsTW").Value = TY
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

                        If Not IsValid And grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value <> "0" Then
                            MsgBox("Stone Weight is Invalid!", MsgBoxStyle.Information, "Invalid Data")
                            grdCategory.Rows(e.RowIndex).Cells("YOrCOrG").Value = "0"
                            grdCategory.Rows(e.RowIndex).Cells("GemsK").Value = "0"
                            grdCategory.Rows(e.RowIndex).Cells("GemsP").Value = "0"
                            If Global_DecimalFormat = 1 Then
                                grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                            Else
                                grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = "0.0"
                            End If

                        End If

                        If VarWeight.EndsWith("G") Then
                            equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
                            GoldWeight.GoldTK = VarWeightBCG / equivalent
                            _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
                            grdCategory.Rows(e.RowIndex).Cells("GemsK").Value = GoldWeight.WeightK
                            grdCategory.Rows(e.RowIndex).Cells("GemsP").Value = GoldWeight.WeightP
                            If Global_DecimalFormat = 1 Then
                                grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.0")
                            Else
                                grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.00")
                            End If
                            grdCategory.Rows(e.RowIndex).Cells("GemsTK").Value = VarWeightBCG / equivalent
                            grdCategory.Rows(e.RowIndex).Cells("GemsTG").Value = VarWeightBCG
                        Else
                            equivalent = Global_GramToKarat '_ConverterCon.GetMeasurement("Gram", "Karat")
                            Dim gram As Decimal = TC / equivalent
                            grdCategory.Rows(e.RowIndex).Cells("GemsTG").Value = gram
                            equivalent = Global_KyatToGram '_ConverterCon.GetMeasurement("Kyat", "Gram")
                            grdCategory.Rows(e.RowIndex).Cells("GemsTK").Value = gram / equivalent
                            GoldWeight.GoldTK = gram / equivalent
                            _ConverterController.ConvertGoldTKToKPYC(GoldWeight)
                            grdCategory.Rows(e.RowIndex).Cells("GemsK").Value = GoldWeight.WeightK
                            grdCategory.Rows(e.RowIndex).Cells("GemsP").Value = GoldWeight.WeightP
                            If Global_DecimalFormat = 1 Then
                                grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.0")
                            Else
                                grdCategory.Rows(e.RowIndex).Cells("GemsY").Value = Format(CDec(GoldWeight.WeightY + GoldWeight.WeightC), "0.00")
                            End If
                        End If


                    End If
                End If
                'If (e.RowIndex <> -1) Then
                CalculategrdCategory()
                CalculategrdTotalAmount()
            End If

        End If
    End Sub
    'Private Sub CalculategrdCategory(Optional ByVal _IsRetrieve As Boolean = False)
    'Dim GoldWeight As New CommonInfo.GoldWeightInfo

    'txtGemsK.Text = "0"
    'txtGemsP.Text = "0"
    'txtGemsY.Text = "0"
    'txtGemsC.Text = "0.0"

    'For i As Integer = 0 To grdCategory.RowCount - 1
    '    If Not grdCategory.Rows(i).IsNewRow Then
    '        GoldWeight.WeightK += CDec(Val(grdCategory.Rows(i).Cells("GemsK").FormattedValue))
    '        GoldWeight.WeightP += CDec(Val(grdCategory.Rows(i).Cells("GemsP").FormattedValue))
    '        GoldWeight.WeightY += CDec(Val(grdCategory.Rows(i).Cells("GemsY").FormattedValue))
    '        GoldWeight.WeightC += CDec(Val(grdCategory.Rows(i).Cells("GemsC").FormattedValue))

    '    End If

    '    GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
    '    _TotalTK = GoldWeight.GoldTK
    '    GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
    '    _TotalTG = GoldWeight.Gram


    'Dim Gold1 As New CommonInfo.GoldWeightInfo
    'Gold1.GoldTK = _GemsTK
    'Gold1 = _ConverterController.ConvertGoldTKToKPYC(Gold1)
    'For i As Integer = 0 To grdCategory.RowCount - 1
    '    If Not grdCategory.Rows(i).IsNewRow Then
    '        grdCategory.Rows(i).Cells("GemsK").Value += Gold1.WeightK
    '        grdCategory.Rows(i).Cells("GemsP").Value += Gold1.WeightP
    '        grdCategory.Rows(i).Cells("GemsY").Value += Gold1.WeightY
    '        grdCategory.Rows(i).Cells("GemsC").Value += Gold1.WeightC

    '    End If
    'Next
    '    _dtSaleGemsItem = grdCategory.DataSource



    'Next


    'Dim GoldWeight As New CommonInfo.GoldWeightInfo
    '    For i As Integer = 0 To grdCategory.RowCount - 1
    '        If Not grdCategory.Rows(i).IsNewRow Then
    '            GoldWeight.WeightK += CDec(Val(grdCategory.Rows(i).Cells("GemsK").FormattedValue))
    '            GoldWeight.WeightP += CDec(Val(grdCategory.Rows(i).Cells("GemsP").FormattedValue))
    '            GoldWeight.WeightY += CDec(Val(grdCategory.Rows(i).Cells("GemsY").FormattedValue))
    '            GoldWeight.WeightC += CDec(Val(grdCategory.Rows(i).Cells("GemsC").FormattedValue))
    '        End If
    '        GoldWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(GoldWeight)
    '        TotalTK = GoldWeight.GoldTK
    '        GoldWeight.Gram = GoldWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
    '        TotalTW = GoldWeight.Gram
    'Dim Gold As New CommonInfo.GoldWeightInfo
    '        Gold.GoldTK = TotalTK
    '        Gold = _ConverterController.ConvertGoldTKToKPYC(Gold)
    '        txtGemsK.Text = Gold.WeightK
    '        txtGemsP.Text = Gold.WeightP
    '        txtGemsY.Text = Gold.WeightY
    '        txtGemsC.Text = Format(Gold.WeightC, "0.0")
    '    Next



    'End Sub
    Private Sub CalculategrdCategory()
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        _TotalGemsTK = 0
        _TotalGemsTG = 0

        Dim GemWeight As New CommonInfo.GoldWeightInfo
        Dim TotalWeight As New CommonInfo.GoldWeightInfo
        Dim weightY As Decimal = 0
        Dim weightP As Integer = 0
        Dim weightK As Integer = 0
        For i As Integer = 0 To grdCategory.RowCount - 1
            If Not grdCategory.Rows(i).IsNewRow Then
                GemWeight.WeightK = CInt(Val(grdCategory.Rows(i).Cells("GemsK").FormattedValue))
                GemWeight.WeightP = CInt(Val(grdCategory.Rows(i).Cells("GemsP").FormattedValue))
                GemWeight.WeightY = CDec(Val(grdCategory.Rows(i).Cells("GemsY").FormattedValue))

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

                _TotalGemsTG += CDec(Val(grdCategory.Rows(i).Cells("GemsTG").FormattedValue))
            End If
        Next

        TotalWeight.WeightY = weightY
        TotalWeight.WeightP = weightP
        TotalWeight.WeightK = weightK

        txtGemsK.Text = Format(TotalWeight.WeightK, "0")
        txtGemsP.Text = Format(TotalWeight.WeightP, "0")
        If Global_DecimalFormat = 1 Then
            txtGemsY.Text = Format(TotalWeight.WeightY, "0.0")
        Else
            txtGemsY.Text = Format(TotalWeight.WeightY, "0.00")
        End If


        TotalWeight.GoldTK = _ConverterController.ConvertKPYCToGoldTK(TotalWeight)
        _TotalGemsTK = TotalWeight.GoldTK

        'TotalWeight.Gram = TotalWeight.GoldTK * (_ConverterController.GetMeasurement("Kyat", "Gram"))
        '_TotalGemsTG = TotalWeight.Gram

        txtGemsTG.Text = Format(_TotalGemsTG, "0.000")
        'Format(_TotalGemsTG, "0.000")
        _dtSaleGemsItem = grdCategory.DataSource
    End Sub
    Private Sub CalculategrdTotalAmount()

        Dim totalamt As Decimal = 0
        If grdCategory.RowCount - 1 = 0 Then
            txtTotalAmt.Text = "0"
        End If
        For i As Integer = 0 To grdCategory.RowCount - 1
            If Not grdCategory.Rows(i).IsNewRow Then
                If grdCategory.Rows(i).Cells("Amount").FormattedValue <> "" Then
                    totalamt += Val(CLng(grdCategory.Rows(i).Cells("Amount").FormattedValue))
                End If
            End If
        Next
        txtTotalAmt.Text = Format(Val(totalamt), "###,##0.##")
        txtNetAmt.Text = Format(Val(totalamt), "###,##0.##")

        txtAddOrSub.Text = "0"
        txtDiscountAmt.Text = "0"
        txtPromotionDis.Text = "0"
        txtPromotionAmt.Text = "0"
        CalculateFinalAmount()
    End Sub

    Private Sub txtNetAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtNetAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmt.TextChanged
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtAddOrSub.Text = "" Then txtAddOrSub.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"

        txtAddOrSub.Text = Format(Val((CLng(txtNetAmt.Text) + CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text)) - CLng(txtTotalAmt.Text)), "###,##0.##")
        If Global_IsCash Then
            txtPaidAmt.Text = Format(Val(CLng(txtNetAmt.Text)), "###,##0.##")
        End If
        txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPaidAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtPaidAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub btnSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchButton.Click
        Dim DataItem As DataRow
        Dim dtSaleGems As New DataTable
        Dim dtSaleGemsItem As New DataTable
        Dim objSaleGems As New SaleGemsInfo
        Dim objSaleGemsItem As New SaleGemsItemInfo
        Dim GoldWeight As New CommonInfo.GoldWeightInfo
        dtSaleGems = _SaleGems.GetAllSaleGems()
        DataItem = DirectCast(SearchData.FindFast(dtSaleGems, "SaleGems List"), DataRow)
        If DataItem IsNot Nothing Then
            _SaleGemsID = DataItem.Item("VoucherNo").ToString()
            objSaleGems = _SaleGems.GetSaleGems(_SaleGemsID)
            _dtOtherCash = _SaleGems.GetOtherCashDataByVoucherNo(_SaleGemsID)

            showSaleGemsData(objSaleGems)
            _dtSaleGemsItem = _SaleGems.GetSaleGemsItem(_SaleGemsID)
            grdCategory.DataSource = _dtSaleGemsItem


            CalculategrdCategory()
            MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        Dim dtPurchaseGems As New DataTable

        Dim dtOtherCash As New DataTable
        'QRCode
        Dim objQRCode As QRCodeEncoder = New QRCodeEncoder()
        objQRCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        objQRCode.QRCodeScale = 3
        objQRCode.QRCodeVersion = 7
        objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H
        dt = _SaleGems.GetSaleGemsPrint(_SaleGemsID)
        dtPurchaseGems = _ObjPurchaseController.GetAllPurchaseGemsPrint(_SaleGemsID)

        For Each dr As DataRow In dt.Rows
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

        dtOtherCash = _SaleGems.GetOtherCashDataByVoucherNo(_SaleGemsID)
        frmPrint.PrintSaleGems(dt, dtPurchaseGems, dtOtherCash)
        ''frmPrint.PrintSaleGemsVoucher(dt)
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub
    Private Sub txtTotalAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAmt.TextChanged
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"

        txtAddOrSub.Text = "0"
        txtDiscountAmt.Text = "0"
        txtPromotionDis.Text = "0"
        txtPromotionAmt.Text = "0"
        CalculateFinalAmount()
    End Sub

    Private Sub grdCategory_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdCategory.RowsRemoved
        If grdCategory.RowCount > 0 Then
            CalculategrdCategory()
            CalculategrdTotalAmount()
        End If
    End Sub

    Private Sub cboStaff_Click(sender As Object, e As EventArgs) Handles cboStaff.Click
        GetStaffByLocation()
    End Sub

    Private Sub cboSalesStaff_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStaff.KeyUp
        AutoCompleteCombo_KeyUp(cboStaff, e)
    End Sub

    Private Sub cboSalesStaff_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.Leave
        AutoCompleteCombo_Leave(cboStaff, "")
    End Sub

    Private Sub txtDiscountAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscountAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtDiscountAmt_TextChanged(sender As Object, e As EventArgs) Handles txtDiscountAmt.TextChanged
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
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtPurchaseAmount.Text = "" Then txtPurchaseAmount.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"
        If _PurchaseHeaderID <> "" Then
            txtDifferentAmount.Text = Format(Val(CLng(txtNetAmt.Text) - CLng(txtPurchaseAmount.Text)), "###,##0.##")
            If Global_IsCash = True Then
                txtPaidAmt.Text = Format(Val((CLng(txtDifferentAmount.Text)) - CLng(txtOtherCashAmount.Text)), "###,##0.##")
            Else
                txtPaidAmt.Text = "0"
            End If

            txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text)) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")

        Else
            txtNetAmt.Text = Format(Val((CLng(txtTotalAmt.Text) + CLng(txtAddOrSub.Text)) - (CLng(txtDiscountAmt.Text) + CLng(txtPromotionAmt.Text))), "###,##0.##")
            If Global_IsCash = True Then
                txtPaidAmt.Text = Format(Val((CLng(txtNetAmt.Text) - CLng(txtOtherCashAmount.Text))), "###,##0.##")
            End If
            txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtOtherCashAmount.Text))), "###,##0.##")
        End If

    End Sub
    Private Sub CalculateBalance()
        If txtNetAmt.Text <> "" And txtPaidAmt.Text <> "" And txtDiscountAmt.Text <> "" And txtPromotionAmt.Text <> "" Then
            txtBalanceAmt.Text = CStr(CLng(txtNetAmt.Text) - (CLng(txtPaidAmt.Text) + CLng(txtPromotionAmt.Text) + CLng(txtDiscountAmt.Text)))
        End If
    End Sub

    Private Sub txtPromotionAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPromotionAmt.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPromotionDis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPromotionDis.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub

    Private Sub txtPromotionDis_TextChanged(sender As Object, e As EventArgs) Handles txtPromotionDis.TextChanged
        If txtPromotionDis.Text = "" Then txtPromotionDis.Text = "0"
        If txtTotalAmt.Text = "" Then txtTotalAmt.Text = "0"
        If txtAddOrSub.Text = "" Then txtAddOrSub.Text = "0"
        If txtDiscountAmt.Text = "" Then txtDiscountAmt.Text = "0"
        If txtPromotionAmt.Text = "" Then txtPromotionAmt.Text = "0"

        txtPromotionAmt.Text = Format(Val(CLng(txtTotalAmt.Text) * (CLng(txtPromotionDis.Text) / 100)), "###,##0.##")
        CalculateFinalAmount()
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

    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsK") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsP") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsY") Then
        '    If IsDBNull(grdCategory.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdCategory.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdCategory.CurrentRow.Cells("GemsY").FormattedValue) Then
        '        If grdCategory.CurrentRow.Cells("YOrCOrG").FormattedValue <> "0" Then
        '            grdCategory.CurrentRow.Cells("YOrCOrG").Value = "0"
        '        End If
        '    End If
        'End If

        If grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsK") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsP") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("Qty") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("SaleRate") Then
            If IsDBNull(grdCategory.CurrentRow.Cells("GemsK").FormattedValue) = False Or IsDBNull(grdCategory.CurrentRow.Cells("GemsP").FormattedValue) = False Or IsDBNull(grdCategory.CurrentRow.Cells("Qty").FormattedValue) = False Or IsDBNull(grdCategory.CurrentRow.Cells("SaleRate").FormattedValue) = False Then
                If InStr(Chr(8), e.KeyChar) > 0 Then
                    Exit Sub
                End If
                If InStr("1234567890" & Chr(13) & Keys.Delete, e.KeyChar) > 0 Then
                    Exit Sub
                Else
                    e.Handled = True
                End If
            End If
        ElseIf grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsY") Then
            If IsDBNull(grdCategory.CurrentRow.Cells("GemsY").FormattedValue) = False Then
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
    Private Sub grdCategory_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCategory.EditingControlShowing
        If grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("GemsCategoryID") Or grdCategory.CurrentCell Is grdCategory.CurrentRow.Cells("FixType") Then
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
        openhelp("SalesGems")
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

    Private Sub dtpSaleDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSaleDate.ValueChanged
        SaleGemGenerateFormat()
    End Sub

    Private Sub txtTotalAmt_Validated(sender As Object, e As EventArgs) Handles txtTotalAmt.Validated
        txtTotalAmt.Text = Format(Val(CLng(txtTotalAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtDiscountAmt_Validated(sender As Object, e As EventArgs) Handles txtDiscountAmt.Validated
        txtDiscountAmt.Text = Format(Val(CLng(txtDiscountAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtNetAmt_Validated(sender As Object, e As EventArgs) Handles txtNetAmt.Validated
        txtNetAmt.Text = Format(Val(CLng(txtNetAmt.Text)), "###,##0.##")
    End Sub

    Private Sub txtPaidAmt_Validated(sender As Object, e As EventArgs)
        txtPaidAmt.Text = Format(Val(CLng(txtPaidAmt.Text)), "###,##0.##")
    End Sub


    Private Sub btnSearchPurchase_Click(sender As Object, e As EventArgs) Handles btnSearchPurchase.Click

        Dim DataItem As DataRow
        Dim dtPurchase As New DataTable
        dtPurchase = _ObjPurchaseController.GetAllPuchaseHeaderDataBySaleGems(_SaleGemsID)
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

    Private Sub btnOtherCash_Click(sender As Object, e As EventArgs) Handles btnOtherCash.Click
        Dim frm As New frm_OtherCash
        frm._dtOtherCash = _dtOtherCash
        frm.ShowDialog()
        _dtOtherCash = frm._dtOtherCash
        CalculateOtherCashAmount()
    End Sub
    Private Sub CalculateOtherCashAmount()
        Dim _OtherCashAmount As Integer = 0
        If _dtOtherCash.Rows.Count() > 0 Then
            For Each drRecord As DataRow In _dtOtherCash.Rows
                If drRecord.RowState <> DataRowState.Deleted Then
                    _OtherCashAmount += CLng(drRecord.Item("Total"))
                End If
            Next
        End If
        txtOtherCashAmount.Text = Format(Val(_OtherCashAmount), "###,##0.##")
        CalculateFinalAmount()
    End Sub

    Private Sub chkOtherCash_CheckedChanged(sender As Object, e As EventArgs) Handles chkOtherCash.CheckedChanged
        If chkOtherCash.Checked Then
            btnOtherCash.Visible = True
        Else
            btnOtherCash.Visible = False
        End If
    End Sub

  
  
    Private Sub txtPaidAmt_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmt.TextChanged
        If txtNetAmt.Text = "" Then txtNetAmt.Text = "0"
        If txtPaidAmt.Text = "" Then txtPaidAmt.Text = "0"
        If txtDifferentAmount.Text = "" Then txtDifferentAmount.Text = "0"
        If txtOtherCashAmount.Text = "" Then txtOtherCashAmount.Text = "0"

        Dim PaidAmount As Integer
        If txtPaidAmt.Text = "-" Then
            PaidAmount = 0
        Else
            PaidAmount = CLng(txtPaidAmt.Text)
        End If

        If _PurchaseHeaderID <> "" Then
            txtBalanceAmt.Text = Format(Val((CLng(txtDifferentAmount.Text)) - (PaidAmount + CLng(txtOtherCashAmount.Text))), "###,##0.##")

        Else
            txtBalanceAmt.Text = Format(Val(CLng(txtNetAmt.Text) - (CLng(txtPaidAmt.Text))), "###,##0.##")
        End If
    End Sub
End Class
