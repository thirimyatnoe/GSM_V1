Imports BusinessRule
Imports CommonInfo
Imports System.Drawing.Printing

Public Class frm_CurrentPriceForGold
    Inherits frm_Base
    Implements IFormProcess
    Dim _IsUpdate As Boolean
    Dim _dtGoldPrice As DataTable

    Private _PriceController As CurrentPrice.CurrentPriceController = Factory.Instance.CreateCurrentPriceController
    Private _GoldQualityController As GoldQuality.GoldQualityController = Factory.Instance.CreateGoldQualityController
    Private _objGlobalSettingCon As GlobalSetting.IGlobalSettingController = Factory.Instance.CreateGlobalSettingController
#Region " IFormProcess Events "

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If _PriceController.DeleteGoldPriceData(_dtGoldPrice) Then
            Call Clear()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Call Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            If _PriceController.SaveGoldPriceData(_dtGoldPrice, dtpDate.Value) Then
                Call Clear()
                Return True
            Else
                Return False
            End If
        End If
    End Function

#End Region

#Region " Private Methods "

    Private Function IsFillData() As Boolean
        If grdGoldPrice.Rows.Count <= 0 Then
            MessageBox.Show("Please fill data in table!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If Not MyBase.IsFill_AtLeastOneRowInGrid(_dtGoldPrice) Then Return False

        For j As Integer = 0 To grdGoldPrice.RowCount - 1
            If IsDBNull(grdGoldPrice.Rows(j).Cells("SalesRate").Value) Then
                MessageBox.Show("Please Fill SaleRate in table!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf grdGoldPrice.Rows(j).Cells("SalesRate").Value = 0 Then
                MessageBox.Show("Please Fill SaleRate in table!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        dtpDate.Value = Date.Now
        txtSolidGoldRate.Text = ""
        btnDelete.Enabled = False
        _IsUpdate = False

        _dtGoldPrice = New DataTable
        _dtGoldPrice.Columns.Add("DefineID", System.Type.GetType("System.String"))
        _dtGoldPrice.Columns.Add("GoldQualityID", System.Type.GetType("System.String"))
        _dtGoldPrice.Columns.Add("GoldQuality", System.Type.GetType("System.String"))
        _dtGoldPrice.Columns.Add("SalesRate", System.Type.GetType("System.Int64"))
        _dtGoldPrice.Columns.Add("PurchaseRate", System.Type.GetType("System.Int64"))
        _dtGoldPrice.Columns.Add("PercentPurchaseRate", System.Type.GetType("System.Int32"))
        _dtGoldPrice.Columns.Add("ExchangeRate", System.Type.GetType("System.Int64"))
        _dtGoldPrice.Columns.Add("PercentExchangeRate", System.Type.GetType("System.Int32"))
        _dtGoldPrice.Columns.Add("PercentDamageRate", System.Type.GetType("System.Int32"))
        _dtGoldPrice.Columns.Add("Remark", System.Type.GetType("System.String"))
        grdGoldPrice.AutoGenerateColumns = False
        grdGoldPrice.DataSource = _dtGoldPrice
        FormatItemGrid()
    End Sub

    Private Sub FormatItemGrid()
        With grdGoldPrice
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9.5)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "DefineID"
            dclID.DataPropertyName = "DefineID"
            dclID.Name = "DefineID"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcGID As New DataGridViewTextBoxColumn()
            dcGID.HeaderText = "GoldQualityID"
            dcGID.DataPropertyName = "GoldQualityID"
            dcGID.Name = "GoldQualityID"
            dcGID.Visible = False
            .Columns.Add(dcGID)

            Dim dcGName As New DataGridViewTextBoxColumn()
            dcGName.HeaderText = "ရွှေရည်"
            dcGName.DataPropertyName = "GoldQuality"
            dcGName.Name = "GoldQuality"
            dcGName.Width = 140
            dcGName.Visible = True
            dcGName.ReadOnly = True
            dcGName.DefaultCellStyle.Font = New Font("Myanmar3", 8.5)
            dcGName.SortMode = DataGridViewColumnSortMode.NotSortable
            dcGName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns.Add(dcGName)

        
            Dim dcSaleRate As New DataGridViewTextBoxColumn()
            dcSaleRate.HeaderText = "ရောင်းစျေး"
            dcSaleRate.DataPropertyName = "SalesRate"
            dcSaleRate.Name = "SalesRate"
            dcSaleRate.Width = 85
            dcSaleRate.Visible = True
            dcSaleRate.DefaultCellStyle.Font = New Font("Tahoma", 9.5)
            dcSaleRate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcSaleRate.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcSaleRate)

            Dim dcExchangeRate As New DataGridViewTextBoxColumn()
            dcExchangeRate.HeaderText = "လဲစျေး(နှုန်း)"
            dcExchangeRate.DataPropertyName = "ExchangeRate"
            dcExchangeRate.Name = "ExchangeRate"
            dcExchangeRate.Width = 85
            dcExchangeRate.Visible = True
            dcExchangeRate.DefaultCellStyle.Font = New Font("Tahoma", 9.5)
            dcExchangeRate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcExchangeRate.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcExchangeRate)

            Dim dcExchangePercent As New DataGridViewTextBoxColumn()
            dcExchangePercent.HeaderText = "လဲစျေး(%)"
            dcExchangePercent.DataPropertyName = "PercentExchangeRate"
            dcExchangePercent.Name = "PercentExchangeRate"
            dcExchangePercent.Width = 85
            dcExchangePercent.Visible = True
            dcExchangePercent.DefaultCellStyle.Font = New Font("Tahoma", 9.5)
            dcExchangePercent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcExchangePercent.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcExchangePercent)

            Dim dcPurchaseRate As New DataGridViewTextBoxColumn()
            dcPurchaseRate.HeaderText = "ဝယ်စျေး(နှုန်း)"
            dcPurchaseRate.DataPropertyName = "PurchaseRate"
            dcPurchaseRate.Name = "PurchaseRate"
            dcPurchaseRate.Width = 90
            dcPurchaseRate.Visible = True
            dcPurchaseRate.DefaultCellStyle.Font = New Font("Tahoma", 9.5)
            dcPurchaseRate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcPurchaseRate.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcPurchaseRate)

            Dim dcPurchasePercent As New DataGridViewTextBoxColumn()
            dcPurchasePercent.HeaderText = "ဝယ်စျေး(%)"
            dcPurchasePercent.DataPropertyName = "PercentPurchaseRate"
            dcPurchasePercent.Name = "PercentPurchaseRate"
            dcPurchasePercent.Width = 85
            dcPurchasePercent.Visible = True
            dcPurchasePercent.DefaultCellStyle.Font = New Font("Tahoma", 9.5)
            dcPurchasePercent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcPurchasePercent.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcPurchasePercent)

            Dim dcDamagePercent As New DataGridViewTextBoxColumn()
            dcDamagePercent.HeaderText = "ပျက်စျေး(%)"
            dcDamagePercent.DataPropertyName = "PercentDamageRate"
            dcDamagePercent.Name = "PercentDamageRate"
            dcDamagePercent.Width = 85
            dcDamagePercent.Visible = True
            dcDamagePercent.DefaultCellStyle.Font = New Font("Tahoma", 9.5)
            dcDamagePercent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcDamagePercent.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcDamagePercent)

            Dim dcRemark As New DataGridViewTextBoxColumn()
            dcRemark.HeaderText = "မှတ်ချက်"
            dcRemark.DataPropertyName = "Remark"
            dcRemark.Name = "Remark"
            dcRemark.Width = 250
            dcRemark.Visible = True
            dcRemark.DefaultCellStyle.Font = New Font("Myanmar3", 8.5)
            dcRemark.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dcRemark.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcRemark)
        End With
    End Sub
#End Region

#Region "Other Form Events"

    Private Sub frm_Sale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "အခြားဝင်ငွေ စာရင်းထည့်ခြင်း"
        Clear()
        Timer1.Interval = 1000
        Timer1.Start()
        MyBase.addGridDataErrorHandlers(grdGoldPrice)
        Me.KeyPreview = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If _IsUpdate = False Then
            dtpDate.Value = dtpDate.Value.AddSeconds(1)
        End If
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
        End Select
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim DataItem As DataRow = DirectCast(SearchData.FindFast(_PriceController.GetAllGoldPriceListByDateTime(), "Gold Price List"), DataRow)
        If DataItem IsNot Nothing Then
            dtpDate.Value = DataItem.Item("@DefineDateTime")
            txtSolidGoldRate.Text = IIf(DataItem.Item("SalesRate") = "0", "", DataItem.Item("SalesRate"))
            _dtGoldPrice = _PriceController.GetGoldPriceDataByDateTime(DataItem.Item("@DefineDateTime"))
            grdGoldPrice.DataSource = _dtGoldPrice
            MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
            btnDelete.Enabled = True
            _IsUpdate = True
        End If
    End Sub
#End Region

#Region "DataGridView Events"


    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If grdGoldPrice.CurrentCell Is grdGoldPrice.CurrentRow.Cells("SalesRate") Or grdGoldPrice.CurrentCell Is grdGoldPrice.CurrentRow.Cells("PurchaseRate") Or grdGoldPrice.CurrentCell Is grdGoldPrice.CurrentRow.Cells("PercentPurchaseRate") Or grdGoldPrice.CurrentCell Is grdGoldPrice.CurrentRow.Cells("ExchangeRate") Or grdGoldPrice.CurrentCell Is grdGoldPrice.CurrentRow.Cells("PercentExchangeRate") Or grdGoldPrice.CurrentCell Is grdGoldPrice.CurrentRow.Cells("PercentDamageRate") Then
            If IsDBNull(grdGoldPrice.CurrentRow.Cells("SalesRate").FormattedValue) = False Or IsDBNull(grdGoldPrice.CurrentRow.Cells("PurchaseRate").FormattedValue) = False Or IsDBNull(grdGoldPrice.CurrentRow.Cells("PercentPurchaseRate").FormattedValue) = False Or IsDBNull(grdGoldPrice.CurrentRow.Cells("ExchangeRate").FormattedValue) = False Or IsDBNull(grdGoldPrice.CurrentRow.Cells("PercentExchangeRate").FormattedValue) = False Or IsDBNull(grdGoldPrice.CurrentRow.Cells("PercentDamageRate").FormattedValue) = False Then
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

    Private Sub grdDailyIncome_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grdGoldPrice.EditingControlShowing
        Dim txtbox As TextBox = CType(e.Control, TextBox)
        If Not (txtbox Is Nothing) Then
            AddHandler txtbox.KeyPress, AddressOf txtBox_KeyPress
        End If
    End Sub

    Private Sub grdDailyIncome_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdGoldPrice.RowsRemoved
        'If grdGoldPrice.Rows.Count = 0 Then Exit Sub
        'MyBase.ShowGridSerialNo(grdGoldPrice)
        'grdGoldPrice.Text = MyBase.CalculateTotalFromGrid(grdGoldPrice, "Amount")
    End Sub

    Private Sub txtSolidGoldRate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSolidGoldRate.KeyPress
        MyBase.ValidateNumeric(sender, e, False)
    End Sub
#End Region


    Private Sub grdDailyIncome_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles grdGoldPrice.RowStateChanged

    End Sub

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim objGlobal As New GlobalSettingInfo

        If txtSolidGoldRate.Text = "" Or txtSolidGoldRate.Text = "0" Then
            MsgBox("Please Enter SolidGold Price!", MsgBoxStyle.Information, AppName)
            txtSolidGoldRate.Focus()
        Else
            objGlobal = _objGlobalSettingCon.GetAllGlobalSettingInfo()

            If _IsUpdate = False Then
                _dtGoldPrice = _GoldQualityController.GetAllGoldQualityDataForGoldPrice()
            End If

            For Each dr As DataRow In _dtGoldPrice.Rows
                If CBool(dr("IsSolidGold")) = True Then
                    dr("SalesRate") = CInt(txtSolidGoldRate.Text)
                    dr("ExchangeRate") = CInt(txtSolidGoldRate.Text) - objGlobal.DiffChangeRate
                    dr("PurchaseRate") = CInt(txtSolidGoldRate.Text) - objGlobal.DiffPurchaseRate
                Else
                    Dim SaleRate As Integer
                    Dim Temp As Integer
                    SaleRate = (CInt(txtSolidGoldRate.Text) / dr("DividedBy")) * dr("MultiplyBy")
                    If SaleRate > 0 Then
                        If objGlobal.IsExactPrice = True Then
                            dr("SalesRate") = SaleRate
                        Else
                            Temp = SaleRate Mod 1000
                            If Temp > 0 Then
                                'dr("SalesRate") = (CInt(SaleRate / 1000) * 1000) + 1000
                                dr("SalesRate") = (CInt(SaleRate / 1000) * 1000)
                            Else
                                dr("SalesRate") = SaleRate
                            End If
                        End If

                        dr("ExchangeRate") = CInt(dr("SalesRate")) - objGlobal.DiffChangeRate
                        dr("PurchaseRate") = CInt(dr("SalesRate")) - objGlobal.DiffPurchaseRate

                    End If
                End If
            Next
            grdGoldPrice.DataSource = _dtGoldPrice
        End If
    End Sub

    Private Sub grdGoldPrice_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGoldPrice.CellValidated
        Dim objGlobal As New GlobalSettingInfo
        objGlobal = _objGlobalSettingCon.GetAllGlobalSettingInfo()

        With grdGoldPrice
            If .IsCurrentCellInEditMode = False Then Exit Sub
            Select Case .Columns(e.ColumnIndex).Name.ToString
                Case ("SalesRate")
                    If Not IsDBNull(.CurrentRow.Cells("SalesRate").Value) Then
                        If CInt(.CurrentRow.Cells("SalesRate").Value) > 0 Then
                            If Not IsDBNull(.CurrentRow.Cells("PurchaseRate").Value) Then
                                If .CurrentRow.Cells("PurchaseRate").Value <> "" Then
                                    .CurrentRow.Cells("PurchaseRate").Value = (.CurrentRow.Cells("SalesRate").Value - objGlobal.DiffPurchaseRate)
                                End If
                            End If

                            If Not IsDBNull(.CurrentRow.Cells("ExchangeRate").Value) Then
                                If .CurrentRow.Cells("ExchangeRate").Value <> "" Then
                                    .CurrentRow.Cells("ExchangeRate").Value = (.CurrentRow.Cells("SalesRate").Value - objGlobal.DiffChangeRate)
                                End If
                            End If
                        End If
                    End If
                Case ("PercentPurchaseRate")
                    If Not IsDBNull(.CurrentRow.Cells("PercentPurchaseRate").Value) Then
                        If .CurrentRow.Cells("PercentPurchaseRate").Value <> "" Then
                            If CInt(.CurrentRow.Cells("PercentPurchaseRate").Value) > 0 Then
                                grdGoldPrice.CurrentRow.Cells("PurchaseRate").Value = ""
                            End If
                        End If
                    Else
                        If Not IsDBNull(.CurrentRow.Cells("SalesRate").Value) Then
                            If .CurrentRow.Cells("SalesRate").Value <> "" Then
                                If CInt(.CurrentRow.Cells("SalesRate").Value) > 0 Then
                                    .CurrentRow.Cells("PurchaseRate").Value = (.CurrentRow.Cells("SalesRate").Value - objGlobal.DiffPurchaseRate)
                                End If
                            End If
                        End If
                    End If

                Case ("PercentExchangeRate")
                    If Not IsDBNull(.CurrentRow.Cells("PercentExchangeRate").Value) Then
                        If .CurrentRow.Cells("PercentExchangeRate").Value <> "" Then
                            If CInt(.CurrentRow.Cells("PercentExchangeRate").Value) > 0 Then
                                grdGoldPrice.CurrentRow.Cells("ExchangeRate").Value = ""
                            End If
                        End If
                    Else
                        If Not IsDBNull(.CurrentRow.Cells("SalesRate").Value) Then
                            If .CurrentRow.Cells("SalesRate").Value <> "" Then
                                If CInt(.CurrentRow.Cells("SalesRate").Value) > 0 Then
                                    .CurrentRow.Cells("ExchangeRate").Value = (.CurrentRow.Cells("SalesRate").Value - objGlobal.DiffChangeRate)
                                End If
                            End If
                        End If
                    End If

                Case ("PurchaseRate")
                    If Not IsDBNull(.CurrentRow.Cells("PurchaseRate").Value) Then
                        If .CurrentRow.Cells("PurchaseRate").Value <> "" Then
                            If CInt(.CurrentRow.Cells("PurchaseRate").Value) > 0 Then
                                grdGoldPrice.CurrentRow.Cells("PercentPurchaseRate").Value = ""
                            End If
                        End If
                    End If
                Case ("ExchangeRate")
                    If Not IsDBNull(.CurrentRow.Cells("ExchangeRate").Value) Then
                        If .CurrentRow.Cells("ExchangeRate").Value <> "" Then
                            If CInt(.CurrentRow.Cells("ExchangeRate").Value) > 0 Then
                                grdGoldPrice.CurrentRow.Cells("PercentExchangeRate").Value = ""
                            End If
                        End If
                    End If
            End Select
        End With
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        dt = _PriceController.GetAllCurrentPriceForPreview("AND IsGramRate=0")
        If dt.Rows.Count() = 0 Then
            MsgBox("There's no record", MsgBoxStyle.Information, Me.Text)
        End If

        frmPrint.PrintStandardRate(dt)
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("DefineGoldPrice")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class

