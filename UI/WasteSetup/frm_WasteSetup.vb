'TMN 06/07/2017

Imports BusinessRule
Imports CommonInfo
Imports Microsoft.Reporting.WinForms
Imports System.Windows
Imports Operational.AppConfiguration
Public Class frm_WasteSetup

    Implements IFormProcess
    Private _LocationID As String
    Private _WasteSetupHeaderID As String
    Dim _ItemNameHeaderID As String
    Dim _ItemCategoryID As String
    Dim _MinTKForSale As Decimal
    Dim _MinTGForSale As Decimal
    Dim _MaxTKForGST As Decimal
    Dim _MaxTGForGST As Decimal
    Dim numberformat As Integer
    Dim _MinNetWeightTK As Decimal
    Dim _MinNetWeightTG As Decimal
    Dim _MaxNetWeightTK As Decimal
    Dim _MaxNetWeightTG As Decimal
    Dim _IsNew As Boolean = False

    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _WasteSetup As WasteSetup.IWasteSetupController = Factory.Instance.CreateWasteSetup
    Private _ItemName As ItemName.IItemNameController = Factory.Instance.CreateItemName
    Private _GoldQuality As GoldQuality.IGoldQualityController = Factory.Instance.CreateGoldQualityController

    Private _ItemCategory As ItemCategory.IItemCategoryController = Factory.Instance.CreateItemCategoryController
    Private _ConverterCon As Converter.IConverterController = Factory.Instance.CreateConverterController

    Private _dtWasteSetupDetail As DataTable


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

#Region "Private Methods"
    Private Sub GetCombo()
        cboItemCategory.DisplayMember = "ItemCategory_"
        cboItemCategory.ValueMember = "@ItemCategoryID"
        cboItemCategory.DataSource = _ItemCategory.GetAllItemCategory().DefaultView
    End Sub
    Private Sub cboItemCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemCategory.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCategory, e)
    End Sub

    Private Sub cboItemCategory_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemCategory.Leave
        AutoCompleteCombo_Leave(cboItemCategory, "")
    End Sub
    Private Sub cboItemName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboItemName.KeyUp
        AutoCompleteCombo_KeyUp(cboItemCategory, e)
    End Sub

    Private Sub cboItemName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItemName.Leave
        AutoCompleteCombo_Leave(cboItemCategory, "")
    End Sub
    Private Function IsFillData() As Boolean
        If cboItemCategory.SelectedIndex = -1 Then
            MsgBox("Please Fill Item Category", MsgBoxStyle.Information, AppName)
            Return False
        End If
        If cboItemName.SelectedIndex = -1 Then
            MsgBox("Please Fill Item Name", MsgBoxStyle.Information, AppName)
            Return False
        End If
        Return True
    End Function
    Private Function GetData() As CommonInfo.WasteSetupHeaderInfo
        Dim objWasteSetup As New CommonInfo.WasteSetupHeaderInfo
        With objWasteSetup
            .WasteSetupHeaderID = _WasteSetupHeaderID
            .ItemCategoryID = cboItemCategory.SelectedValue
            .ItemNameID = cboItemName.SelectedValue
        End With
        Return objWasteSetup
    End Function
    Private Sub ClearData()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)

        _IsNew = False
        _WasteSetupHeaderID = ""
        _ItemCategoryID = ""
        txtWasteSetupHeaderID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.WasteSetUp, EnumSetting.GenerateKeyType.WasteSetUp.ToString, Now)

        cboItemCategory.Enabled = True
        cboItemCategory.SelectedValue = -1

        cboItemName.Text = ""

        Dim dc As DataColumn

        _dtWasteSetupDetail = New DataTable
        _dtWasteSetupDetail.Columns.Add("WasteSetupHeaderID", System.Type.GetType("System.String"))
        _dtWasteSetupDetail.Columns.Add("WasteSetupDetailID", System.Type.GetType("System.String"))
        _dtWasteSetupDetail.Columns.Add("@GoldQualityID", System.Type.GetType("System.String"))

        dc = New DataColumn
        dc.ColumnName = "MinNetWeightK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtWasteSetupDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MinNetWeightP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtWasteSetupDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MinNetWeightY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = 0.0
        _dtWasteSetupDetail.Columns.Add(dc)


        _dtWasteSetupDetail.Columns.Add("MinNetWeightTK", System.Type.GetType("System.Decimal"))
        _dtWasteSetupDetail.Columns.Add("MinNetWeightTG", System.Type.GetType("System.Decimal"))

        dc = New DataColumn
        dc.ColumnName = "MaxNetWeightK"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtWasteSetupDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MaxNetWeightP"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtWasteSetupDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MaxNetWeightY"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.00"
        _dtWasteSetupDetail.Columns.Add(dc)


        _dtWasteSetupDetail.Columns.Add("MaxNetWeightTK", System.Type.GetType("System.Decimal"))
        _dtWasteSetupDetail.Columns.Add("MaxNetWeightTG", System.Type.GetType("System.Decimal"))

        dc = New DataColumn
        dc.ColumnName = "MinWeightPForSale"
        dc.DataType = System.Type.GetType("System.Int16")
        dc.DefaultValue = 0
        _dtWasteSetupDetail.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MinWeightYForSale"
        dc.DataType = System.Type.GetType("System.Decimal")
        dc.DefaultValue = "0.00"
        _dtWasteSetupDetail.Columns.Add(dc)




        _dtWasteSetupDetail.Columns.Add("MinWeightTKForSale", System.Type.GetType("System.Decimal"))
        _dtWasteSetupDetail.Columns.Add("MinWeightTGForSale", System.Type.GetType("System.Decimal"))


        grdWasteDetail.AutoGenerateColumns = False
        grdWasteDetail.ReadOnly = False
        grdWasteDetail.DataSource = _dtWasteSetupDetail
        FormatItemNameDetailGrid()

    End Sub

    Private Sub FormatItemNameDetailGrid()

        With grdWasteDetail

            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Zawgyi-one", 9.25)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40

            Dim dcDetailID As New DataGridViewTextBoxColumn()
            dcDetailID.HeaderText = "WasteSetupDetailID"
            dcDetailID.DataPropertyName = "WasteSetupDetailID"
            dcDetailID.Name = "WasteSetupDetailID"
            dcDetailID.Visible = False
            .Columns.Add(dcDetailID)

            Dim dcHeaderID As New DataGridViewTextBoxColumn()
            dcHeaderID.HeaderText = "WasteSetupHeaderID"
            dcHeaderID.DataPropertyName = "WasteSetupHeaderID"
            dcHeaderID.Name = "WasteSetupHeaderID"
            dcHeaderID.Visible = False
            .Columns.Add(dcHeaderID)

            Dim dcGQ As New DataGridViewComboBoxColumn()
            With dcGQ
                .HeaderText = "ေရႊရည္"
                .DataPropertyName = "@GoldQualityID"
                .Name = "GoldQualityID"
                .DataSource = _GoldQuality.GetAllGoldQuality()
                .DisplayMember = "GoldQuality"
                .ValueMember = "@GoldQualityID"
                .DefaultCellStyle.Font = New Font("Zawgyi-One", 8.25)
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
            End With
            .Columns.Add(dcGQ)

            Dim dcMinK As New DataGridViewTextBoxColumn()
            With dcMinK
                .HeaderText = "MinK"
                .DataPropertyName = "MinNetWeightK"
                .Name = "MinNetWeightK"
                .Width = 70
                .Visible = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.BackColor = Color.LightSalmon
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMinK)

            Dim dcMinP As New DataGridViewTextBoxColumn()
            dcMinP.HeaderText = "MinP"
            dcMinP.DataPropertyName = "MinNetWeightP"
            dcMinP.Name = "MinNetWeightP"
            dcMinP.Width = 70
            dcMinP.Visible = True
            dcMinP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcMinP.DefaultCellStyle.BackColor = Color.LightSalmon
            dcMinP.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcMinP)

            Dim dcMinY As New DataGridViewTextBoxColumn()
            dcMinY.HeaderText = "MinY"
            dcMinY.DataPropertyName = "MinNetWeightY"
            dcMinY.Name = "MinNetWeightY"
            dcMinY.Width = 70
            dcMinY.Visible = True
            .DefaultCellStyle.Format = "0.00"
            dcMinY.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcMinY.DefaultCellStyle.BackColor = Color.LightSalmon
            dcMinY.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcMinY)

            Dim dcMinNetTK As New DataGridViewTextBoxColumn()
            With dcMinNetTK
                .HeaderText = "MinNetWeightTK"
                .DataPropertyName = "MinNetWeightTK"
                .Name = "MinNetWeightTK"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMinNetTK)

            Dim dcMinNetTG As New DataGridViewTextBoxColumn()
            With dcMinNetTG
                .HeaderText = "MinNetWeightTG"
                .DataPropertyName = "MinNetWeightTG"
                .Name = "MinNetWeightTG"
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMinNetTG)

            Dim dcMaxK As New DataGridViewTextBoxColumn()
            dcMaxK.HeaderText = "MaxK"
            dcMaxK.DataPropertyName = "MaxNetWeightK"
            dcMaxK.Name = "MaxNetWeightK"
            dcMaxK.Width = 70
            dcMaxK.Visible = True
            dcMaxK.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcMaxK.DefaultCellStyle.BackColor = Color.LightSalmon
            dcMaxK.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcMaxK)

            Dim dcMaxP As New DataGridViewTextBoxColumn()
            dcMaxP.HeaderText = "MaxP"
            dcMaxP.DataPropertyName = "MaxNetWeightP"
            dcMaxP.Name = "MaxNetWeightP"
            dcMaxP.Visible = True
            dcMaxP.Width = 70
            dcMaxP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcMaxP.DefaultCellStyle.BackColor = Color.LightSalmon
            dcMaxP.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcMaxP)

            Dim dcMaxY As New DataGridViewTextBoxColumn()
            dcMaxY.HeaderText = "MaxY"
            dcMaxY.DataPropertyName = "MaxNetWeightY"
            dcMaxY.Name = "MaxNetWeightY"
            dcMaxY.Visible = True
            dcMaxY.Width = 70
            .DefaultCellStyle.Format = "0.00"
            dcMaxY.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dcMaxY.DefaultCellStyle.BackColor = Color.LightSalmon
            dcMaxY.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcMaxY)


            Dim dcMaxNetTK As New DataGridViewTextBoxColumn()
            With dcMaxNetTK
                .HeaderText = "MaxNetWeightTK"
                .DataPropertyName = "MaxNetWeightTK"
                .Name = "MaxNetWeightTK"
                .Visible = False
                .Width = 70
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMaxNetTK)

            Dim dcMaxNetTG As New DataGridViewTextBoxColumn()
            With dcMaxNetTG
                .HeaderText = "MaxNetWeightTG"
                .DataPropertyName = "MaxNetWeightTG"
                .Name = "MaxNetWeightTG"
                .Visible = False
                .Width = 70
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMaxNetTG)

            Dim dcminPForSale As New DataGridViewTextBoxColumn()
            With dcminPForSale
                .HeaderText = "SP"
                .DataPropertyName = "MinWeightPForSale"
                .Name = "MinWeightPForSale"
                .Visible = True
                .Width = 70
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.BackColor = Color.Honeydew
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcminPForSale)

            Dim dcminYForSale As New DataGridViewTextBoxColumn()
            With dcminYForSale
                .HeaderText = "SY"
                .DataPropertyName = "MinWeightYForSale"
                .Name = "MinWeightYForSale"
                .Visible = True
                .DefaultCellStyle.Format = "0.00"
                .Width = 70
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.BackColor = Color.Honeydew
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcminYForSale)


            Dim dcMinWeightTK As New DataGridViewTextBoxColumn()
            With dcMinWeightTK
                .HeaderText = "MinWeightTKForSale"
                .DataPropertyName = "MinWeightTKForSale"
                .Name = "MinWeightTKForSale"
                .Visible = False
                .Width = 70
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMinWeightTK)

            Dim dcMinWeightTG As New DataGridViewTextBoxColumn()
            With dcMinWeightTG
                .HeaderText = "MinWeightTGForSale"
                .DataPropertyName = "MinWeightTGForSale"
                .Name = "MinWeightTGForSale"
                .Visible = False
                .Width = 70
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(dcMinWeightTG)
        End With
    End Sub
   
    Private Sub ShowWasteSetupData(ByVal Obj As CommonInfo.WasteSetupHeaderInfo)
        With Obj
            txtWasteSetupHeaderID.Text = .WasteSetupHeaderID
            cboItemCategory.SelectedValue = .ItemCategoryID
            cboItemName.SelectedValue = .ItemNameID

        End With
    End Sub

#End Region

#Region "IFormProcess"
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If _WasteSetup.DeleteWasteSetup(_WasteSetupHeaderID) Then
            ClearData()
            btnDelete.Enabled = False
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
            Dim objWasteSetup As New CommonInfo.WasteSetupHeaderInfo
            objWasteSetup = GetData()
            If _WasteSetup.InsertWasteSetup(objWasteSetup, _dtWasteSetupDetail) Then
                ClearData()
                Return True
            Else
                Return False
            End If
        End If
    End Function
#End Region

    Private Sub frm_WasteSetup_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblLogInUserName.Text = Global_CurrentUser
        lblCurrentLocationName.Text = Global_CurrentLocationName
        numberformat = Global_DecimalFormat

        GetCombo()
        MyBase.addGridDataErrorHandlers(grdWasteDetail)
        ClearData()
        _LocationID = Global_CurrentLocationID
        Me.KeyPreview = True
    End Sub

    Private Sub cboItemCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboItemCategory.SelectedValueChanged
        Dim ItemCategoryID As String
        ItemCategoryID = cboItemCategory.SelectedValue
        RefreshItemNameCbo(ItemCategoryID)
    End Sub
    Private Sub RefreshItemNameCbo(ByVal ItemCategoryID As String)
        Dim dt As New DataTable
        dt = _ItemName.GetItemNameListByItemCategory(ItemCategoryID)
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


    Private Sub btnSearchWasteHeaderID_Click(sender As Object, e As EventArgs) Handles btnSearchWasteHeaderID.Click
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)

        Dim dt As New DataTable
        Dim objWasteSetup As New CommonInfo.WasteSetupHeaderInfo
        Dim DataItem As DataRow

        dt = _WasteSetup.GetWasteSetup()

        DataItem = DirectCast(SearchData.FindFast(dt, "ItemCategory List"), DataRow)
        If DataItem IsNot Nothing Then
            _IsNew = True
            _WasteSetupHeaderID = DataItem.Item("WasteSetupHeaderID").ToString()
            objWasteSetup = _WasteSetup.GetWasteSetupHeaderID(_WasteSetupHeaderID)
            ShowWasteSetupData(objWasteSetup)

            _dtWasteSetupDetail = _WasteSetup.GetWasteSetupDetail(_WasteSetupHeaderID)
            If _dtWasteSetupDetail IsNot Nothing Then
                grdWasteDetail.DataSource = _dtWasteSetupDetail

            End If

        End If
        btnDelete.Enabled = True
    End Sub

    Private Sub grdWasteDetail_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdWasteDetail.CellValueChanged
        Dim GoldWeight As New CommonInfo.GoldWeightInfo


        If (grdWasteDetail.Columns(e.ColumnIndex).Name = "MinNetWeightK" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MinNetWeightP" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MinNetWeightY") Then

            If (e.RowIndex <> -1) Then

                With grdWasteDetail
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("MinNetWeightK").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MinNetWeightP").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MinNetWeightY").Value) Then
                        GoldWeight.WeightK = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightK").FormattedValue))
                        GoldWeight.WeightP = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightP").FormattedValue))

                        'GoldWeight.WeightY = CDec(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightY").FormattedValue))

                        GoldWeight.WeightY = CDec(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightY").FormattedValue))

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _MinNetWeightTK = GoldWeight.GoldTK
                        GoldWeight.Gram = GoldWeight.GoldTK * Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                        _MinNetWeightTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("MinNetWeightTK").Value() = _MinNetWeightTK
                        .Rows(e.RowIndex).Cells("MinNetWeightTG").Value() = _MinNetWeightTG

                    End If
                End With

            End If
        ElseIf (grdWasteDetail.Columns(e.ColumnIndex).Name = "MaxNetWeightK" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MaxNetWeightP" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MaxNetWeightY") Then

            If (e.RowIndex <> -1) Then

                With grdWasteDetail
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("MaxNetWeightK").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MaxNetWeightP").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MaxNetWeightY").Value) Then
                        GoldWeight.WeightK = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MaxNetWeightK").FormattedValue))
                        GoldWeight.WeightP = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MaxNetWeightP").FormattedValue))
                        GoldWeight.WeightY = CDec(grdWasteDetail.Rows(e.RowIndex).Cells("MaxNetWeightY").FormattedValue)

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _MaxNetWeightTK = GoldWeight.GoldTK
                        GoldWeight.Gram = GoldWeight.GoldTK * Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                        _MaxNetWeightTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("MaxNetWeightTK").Value() = _MaxNetWeightTK
                        .Rows(e.RowIndex).Cells("MaxNetWeightTG").Value() = _MaxNetWeightTG

                    End If
                End With
            End If

        ElseIf (grdWasteDetail.Columns(e.ColumnIndex).Name = "MinWeightPForSale" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MinWeightYForSale") Then

            If (e.RowIndex <> -1) Then

                With grdWasteDetail
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("MinWeightPForSale").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MinWeightYForSale").Value) Then
                        GoldWeight.WeightK = 0
                        GoldWeight.WeightP = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinWeightPForSale").FormattedValue))
                        GoldWeight.WeightY = CDec(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinWeightYForSale").FormattedValue))

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _MinTKForSale = GoldWeight.GoldTK
                        GoldWeight.Gram = GoldWeight.GoldTK * Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                        _MinTGForSale = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("MinWeightTKForSale").Value() = _MinTKForSale
                        .Rows(e.RowIndex).Cells("MinWeightTGForSale").Value() = _MinTGForSale

                    End If
                End With
            End If
        End If
    End Sub

  





    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
          openhelp("Waste")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub grdWasteDetail_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles grdWasteDetail.CellValidated
        Dim GoldWeight As New CommonInfo.GoldWeightInfo


        If (grdWasteDetail.Columns(e.ColumnIndex).Name = "MinNetWeightK" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MinNetWeightP" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MinNetWeightY") Then

            If (e.RowIndex <> -1) Then

                With grdWasteDetail
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("MinNetWeightK").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MinNetWeightP").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MinNetWeightY").Value) Then
                        GoldWeight.WeightK = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightK").FormattedValue))
                        GoldWeight.WeightP = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightP").FormattedValue))

                        'GoldWeight.WeightY = CDec(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightY").FormattedValue))

                        GoldWeight.WeightY = CDec(grdWasteDetail.Rows(e.RowIndex).Cells("MinNetWeightY").FormattedValue)

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _MinNetWeightTK = GoldWeight.GoldTK
                        GoldWeight.Gram = GoldWeight.GoldTK * Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                        _MinNetWeightTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("MinNetWeightTK").Value() = _MinNetWeightTK
                        .Rows(e.RowIndex).Cells("MinNetWeightTG").Value() = _MinNetWeightTG

                    End If
                End With

            End If
        ElseIf (grdWasteDetail.Columns(e.ColumnIndex).Name = "MaxNetWeightK" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MaxNetWeightP" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MaxNetWeightY") Then

            If (e.RowIndex <> -1) Then

                With grdWasteDetail
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("MaxNetWeightK").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MaxNetWeightP").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MaxNetWeightY").Value) Then
                        GoldWeight.WeightK = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MaxNetWeightK").FormattedValue))
                        GoldWeight.WeightP = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MaxNetWeightP").FormattedValue))
                        GoldWeight.WeightY = CDec(grdWasteDetail.Rows(e.RowIndex).Cells("MaxNetWeightY").FormattedValue)

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _MaxNetWeightTK = GoldWeight.GoldTK
                        GoldWeight.Gram = GoldWeight.GoldTK * Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                        _MaxNetWeightTG = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("MaxNetWeightTK").Value() = _MaxNetWeightTK
                        .Rows(e.RowIndex).Cells("MaxNetWeightTG").Value() = _MaxNetWeightTG

                    End If
                End With
            End If

        ElseIf (grdWasteDetail.Columns(e.ColumnIndex).Name = "MinWeightPForSale" Or grdWasteDetail.Columns(e.ColumnIndex).Name = "MinWeightYForSale") Then

            If (e.RowIndex <> -1) Then

                With grdWasteDetail
                    If Not IsDBNull(.Rows(e.RowIndex).Cells("MinWeightPForSale").Value) Or Not IsDBNull(.Rows(e.RowIndex).Cells("MinWeightYForSale").Value) Then
                        GoldWeight.WeightK = 0
                        GoldWeight.WeightP = CInt(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinWeightPForSale").FormattedValue))
                        GoldWeight.WeightY = CDec(Val(grdWasteDetail.Rows(e.RowIndex).Cells("MinWeightYForSale").FormattedValue))

                        GoldWeight.GoldTK = _ConverterCon.ConvertKPYCToGoldTK(GoldWeight)
                        _MinTKForSale = GoldWeight.GoldTK
                        GoldWeight.Gram = GoldWeight.GoldTK * Global_KyatToGram '(_ConverterCon.GetMeasurement("Kyat", "Gram"))
                        _MinTGForSale = GoldWeight.Gram

                        .Rows(e.RowIndex).Cells("MinWeightTKForSale").Value() = _MinTKForSale
                        .Rows(e.RowIndex).Cells("MinWeightTGForSale").Value() = _MinTGForSale

                    End If
                End With
            End If
        End If
    End Sub
End Class