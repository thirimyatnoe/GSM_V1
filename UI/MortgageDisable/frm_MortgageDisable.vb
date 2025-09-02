Imports BusinessRule
Imports CommonInfo

Public Class frm_MortgageDisable
    Implements IFormProcess
#Region " Variables Declaration "


    Private _dt As DataTable
    Private _dtItem As DataTable

    Private FindFast_Caption As String
    Private GridSizes As New Size
    Private DefaultSizes As New Size
    Private Const m_ButIn As String = "<<<"
    Private Const m_ButOut As String = ">>>"



    Dim _MortgageInvoice As MortgageInvoice.IMortgageInvoiceController = Factory.Instance.CreateMortgageInvoiceController
    Dim _MortgageInterest As MortgageInterest.IMortgageInterestController = Factory.Instance.CreateMortgageInterestController



   

#End Region
    Private Sub frm_MortgageDisable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblLocationName.Text = Global_CurrentLocationName
        btnNew.Visible = False
        btnDelete.Visible = False
        Clear()
        grdItem.Visible = False
        grd.SendToBack()
        'AddEventHandalers(Me)
        GridSizes = grd.Size
        DefaultSizes = grd.Size
        btnClick.Text = m_ButIn
        btnClick.Image = ImageList1.Images(1)
         MoveButton(grd.Size.Width, grd.Height + 59)

    End Sub
#Region " IFormProcess "
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave

        If _MortgageInvoice.SaveMortgageDisable(dtpDate.Value, _dt) Then
            Clear()
            Return True
        Else
            Return False
        End If

    End Function
#End Region
#Region " Private Methods "
    Private Sub Clear()

        _dt = New DataTable
        _dt.Columns.Add("MortgageInvoiceID", System.Type.GetType("System.String"))
        _dt.Columns.Add("MortgageInvoiceCode", System.Type.GetType("System.String"))
        _dt.Columns.Add("ReceiveDate", System.Type.GetType("System.DateTime"))
        _dt.Columns.Add("Staff", System.Type.GetType("System.String"))
        _dt.Columns.Add("TotalAmount", System.Type.GetType("System.Int32"))
        _dt.Columns.Add("TotalQTY", System.Type.GetType("System.Int32"))
        _dt.Columns.Add("IsDisable", System.Type.GetType("System.Boolean"))

        grd.AutoGenerateColumns = False
        grd.DataSource = _dt

        FormatGrid()

        _dtItem = New DataTable
        _dtItem.Columns.Add("MortgageItemID", System.Type.GetType("System.String"))
        _dtItem.Columns.Add("ItemCategory_", System.Type.GetType("System.String"))
        _dtItem.Columns.Add("ItemName_", System.Type.GetType("System.String"))
        _dtItem.Columns.Add("QTY", System.Type.GetType("System.Int32"))
        _dtItem.Columns.Add("GoldK", System.Type.GetType("System.Int32"))
        _dtItem.Columns.Add("GoldP", System.Type.GetType("System.Int32"))
        _dtItem.Columns.Add("GoldY", System.Type.GetType("System.Int32"))
        _dtItem.Columns.Add("GoldC", System.Type.GetType("System.Int32"))
        _dtItem.Columns.Add("Amount", System.Type.GetType("System.Int32"))


        grdItem.AutoGenerateColumns = False
        grdItem.DataSource = _dtItem

        FormatGrid_Item()
        If Global_IsUsedSettingPeriod Then
            lblPeriod.Visible = False
            txtPeriod.Visible = False
        Else
            lblPeriod.Visible = True
            txtPeriod.Visible = True
        End If
    End Sub
    Private Sub FormatGrid()
        Dim colWidth As Integer = 0
        With grd
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9.25)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "MortgageInvoiceID"
                .DataPropertyName = "MortgageInvoiceID"
                .Name = "MortgageInvoiceID"
                .Width = 150
                .Visible = False
            End With
            .Columns.Add(dcID)
            colWidth = colWidth + dcID.Width

            Dim dccode As New DataGridViewTextBoxColumn()
            With dccode
                .HeaderText = "Code"
                .DataPropertyName = "MortgageInvoiceCode"
                .Name = "MortgageInvoiceCode"
                .Width = 75
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dccode)
            colWidth = colWidth + dccode.Width

            Dim dcDate As New DataGridViewTextBoxColumn()
            With dcDate
                .HeaderText = "နေ့စွဲ"
                .DataPropertyName = "ReceiveDate"
                .Name = "ReceiveDate"
                .Width = 100
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcDate)
            colWidth = colWidth + dcDate.Width
            Dim dcStaff As New DataGridViewTextBoxColumn()
            With dcStaff
                .HeaderText = "ပေါင်သူအမည်"
                .DataPropertyName = "CustomerName"
                .Name = "CustomerName"
                .Width = 150
                .ReadOnly = True
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcStaff)

            Dim dcTotalAmt As New DataGridViewTextBoxColumn()
            With dcTotalAmt
                .HeaderText = "ကျသင့်ငွေ"
                .DataPropertyName = "TotalAmount"
                .Name = "TotalAmount"
                .Width = 90
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcTotalAmt)
            colWidth = colWidth + dcTotalAmt.Width
            Dim dcTotalQTY As New DataGridViewTextBoxColumn()
            With dcTotalQTY
                .HeaderText = "ရတ"
                .DataPropertyName = "TotalQTY"
                .Name = "TotalQTY"
                .Width = 75
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcTotalQTY)
            colWidth = colWidth + dcTotalQTY.Width
            Dim dcDisable As New DataGridViewCheckBoxColumn
            With dcDisable
                .HeaderText = "အပေါင်ဆုံး"
                .DataPropertyName = "IsDisable"
                .Name = "IsDisable"
                .Width = 75
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcDisable)
            colWidth = colWidth + dcDisable.Width
            .Width = colWidth
        End With
    End Sub
    Private Sub FormatGrid_Item()
        Dim colWidth As Integer = 0
        With grdItem
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9.25)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40
            Dim dcItemID As New DataGridViewTextBoxColumn()
            With dcItemID
                .HeaderText = "MortgageItemID"
                .DataPropertyName = "MortgageItemID"
                .Name = "MortgageItemID"
                .Width = 150
                .Visible = False
            End With
            .Columns.Add(dcItemID)

            Dim dcID As New DataGridViewTextBoxColumn()
            With dcID
                .HeaderText = "MortgageInvoiceID"
                .DataPropertyName = "MortgageInvoiceID"
                .Name = "MortgageInvoiceID"
                .Width = 150
                .Visible = False
            End With
            .Columns.Add(dcID)

            Dim dcItemCategory As New DataGridViewTextBoxColumn()
            With dcItemCategory
                .HeaderText = "အမျိုးအစား"
                .DataPropertyName = "ItemCategory"
                .Name = "ItemCategory"
                .Width = 100
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .Visible = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcItemCategory)
            colWidth = colWidth + dcItemCategory.Width
            Dim dcItemName As New DataGridViewTextBoxColumn()
            With dcItemName
                .HeaderText = "အမျိုးအမည်"
                .DataPropertyName = "ItemName"
                .Name = "ItemName"
                .Width = 100
                .Visible = True
                .DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
                .SortMode = DataGridViewColumnSortMode.NotSortable

            End With
            .Columns.Add(dcItemName)
            colWidth = colWidth + dcItemName.Width
            Dim dcQTY As New DataGridViewTextBoxColumn()
            With dcQTY
                .HeaderText = "ရတ"
                .DataPropertyName = "QTY"
                .Name = "QTY"
                .Width = 35
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(dcQTY)
            colWidth = colWidth + dcQTY.Width
            Dim dcK As New DataGridViewTextBoxColumn()
            With dcK
                .HeaderText = "K"
                .DataPropertyName = "GoldK"
                .Name = "GoldK"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcK)
            colWidth = colWidth + dcK.Width
            Dim dcP As New DataGridViewTextBoxColumn
            With dcP
                .HeaderText = "P"
                .DataPropertyName = "GoldP"
                .Name = "GoldP"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcP)
            colWidth = colWidth + dcP.Width

            Dim dcY As New DataGridViewTextBoxColumn
            With dcY
                .HeaderText = "Y"
                .DataPropertyName = "GoldY"
                .Name = "GoldY"
                .Width = 30
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcY)
            colWidth = colWidth + dcY.Width
            Dim dcC As New DataGridViewTextBoxColumn
            With dcC
                .HeaderText = "C"
                .DataPropertyName = "GoldP"
                .Name = "GoldP"
                .Width = 35
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcC)
            colWidth = colWidth + dcC.Width
            Dim dcAmount As New DataGridViewTextBoxColumn
            With dcAmount
                .HeaderText = "ပေးငွေ"
                .DataPropertyName = "Amount"
                .Name = "Amount"
                .Width = 60
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcAmount)
            colWidth = colWidth + dcAmount.Width
            .Width = colWidth + 300
        End With
    End Sub
#End Region

   

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim objMortgageInvoice As New MortgageInvoiceInfo
        Dim objMortgageInterest As New MortgageInterestInfo
        If IsFillData() Then
            If Global_IsUsedSettingPeriod Then
                _dt = _MortgageInvoice.GetMortgageDisable(Global_InterestPeriod)
            Else
                _dt = _MortgageInvoice.GetMortgageDisable(CInt(txtPeriod.Text))
            End If
            If (_dt.Rows.Count > 0) Then
                grd.AutoGenerateColumns = False
                grd.DataSource = _dt.DefaultView
                'Call FormatGrid()
                If grd.Rows.Count > 0 Then
                    grd.Rows(0).Selected = True
                End If
            Else
                MsgBox("There is no record.", vbInformation)
            End If
        End If
        grdItem.Visible = False
    End Sub
    Private Function IsFillData() As Boolean
        If Global_IsUsedSettingPeriod = False Then
            If CInt(txtPeriod.Text) <= 0 Then
                MsgBox("Please Enter Mortgage Disable Period !", MsgBoxStyle.Information, AppName)
                txtPeriod.Focus()
                Return False
            End If
        End If
       
        Return True
    End Function

    Private Sub MoveButton(ByVal X As Integer, ByVal Y As Integer)
        Dim ButtonLocation As New Point
        ButtonLocation.X = X
        ButtonLocation.Y = (Y / 2) - (btnClick.Height / 2)
        btnClick.Location = ButtonLocation
        btnClick.BringToFront()
    End Sub
    Private Sub ButtonWithChange(ByVal Width As Integer)
        Dim ButtonSize As New Size
        ButtonSize = btnClick.Size
        ButtonSize.Width = Width
        btnClick.Size = ButtonSize
    End Sub
    Private Sub TreeViewSplitter_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles TreeViewSplitter.SplitterMoved
        If grd.Size.Width <= 335 Then
            btnClick.Text = m_ButOut
            GridSizes.Width = 0
            grd.Size = GridSizes
            MoveButton(grd.Size.Width, grd.Height)
        Else
            GridSizes = grd.Size
            MoveButton(grd.Size.Width, grd.Height)

        End If
    End Sub
    Private Sub TreeViewSplitter_SplitterMoving(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles TreeViewSplitter.SplitterMoving
        If grd.Size.Width <= 335 Then
            Dim NewPoint As New Point(0, 0)
            grd.Location = NewPoint
        End If
    End Sub
    Private Sub btnClick_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClick.Click
        Dim ReadWidth As Integer = 0
        If grd.Size.Width <> 0 Then
            ReadWidth = GridSizes.Width
            GridSizes.Width = 0
            grd.Size = GridSizes
            GridSizes.Width = ReadWidth
            btnClick.Image = ImageList1.Images(1)
            btnClick.Text = m_ButOut
        Else
            If GridSizes.Width <> 0 Then
                grd.Size = GridSizes
            Else
                grd.Size = DefaultSize
                btnClick.Image = ImageList1.Images(1)
            End If
            btnClick.Text = m_ButIn
        End If
        MoveButton(grd.Size.Width, grd.Height)
    End Sub

    Private Sub btnClick_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClick.MouseHover
        btnClick.Cursor = Cursors.Hand
        If grd.Size.Width <> 0 Then
            btnClick.Image = ImageList1.Images(0)
        Else
            btnClick.Image = ImageList1.Images(0)
        End If

        ButtonWithChange(8)
    End Sub

    Private Sub btnClick_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClick.MouseLeave
        ButtonWithChange(8)
        If grd.Size.Width <> 0 Then
            btnClick.Image = ImageList1.Images(1)
        Else
            btnClick.Image = ImageList1.Images(1)
        End If
    End Sub
    Private Sub grd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.Click
        Dim MortgageInvoiceID As String
        If grd.Rows.Count > 0 Then

            MortgageInvoiceID = IIf(IsDBNull(grd.CurrentRow.Cells("MortgageInvoiceID").Value), "", grd.CurrentRow.Cells("MortgageInvoiceID").Value)
            _dtItem = _MortgageInvoice.GetMortgageInvoiceItem(MortgageInvoiceID)

            If _dtItem.Rows.Count > 0 Then
                grdItem.AutoGenerateColumns = False
                grdItem.DataSource = _dtItem.DefaultView
                grdItem.Visible = True
                ' Call grdItem_FormatGrid()
            End If

        End If

    End Sub
    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("MortgageDisable")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class
