Imports CommonInfo
Imports BusinessRule
Public Class frm_NewBarcodeSetting
    Implements IFormProcess
    Private _BarcodeSettingController As Barcodesetting.IBarcodeSettingController = Factory.Instance.CreateBarcodeSettingController

    Private Sub frm_NewBarcodeSetting_Load(sender As Object, e As EventArgs) Handles Me.Load
        '_Heading.Text = "Barcode Setting"
        MyBase.btnNew.Visible = False
        MyBase.btnDelete.Visible = False
        chkGemPrice.Enabled = False
        lblEng.Visible = False
        lblMM.Visible = False
        txtEngName.Visible = False
        txtMMName.Visible = False
        LoadBarcodeSetting()

    End Sub
    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()

    End Function
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

    End Function
    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim info As BarcodePrinterInfo
        Dim Result As Boolean = False
        _BarcodeSettingController.DeleteBarcodeSetting()
        If txtLabelWidth.Text <> "" And txtLabelHeight.Text <> "" And txtBarcodeHeight.Text <> "" And txtBarcodeName.Text <> "" And txtBarcodeNarrow.Text <> "" And txtBarcodeWide.Text <> "" And txtBarcodeX.Text <> "" And txtBarcodeY.Text <> "" Then
            If chkIsLocationName.Checked = True Then
                If txtMMName.Text = "" And txtEngName.Text = "" Then
                    MsgBox("Please Fill Location Name ", MsgBoxStyle.Information, AppName)
                    Exit Function
                End If
            End If

            info = New BarcodePrinterInfo
            With info
                .PaperWidth = txtLabelWidth.Text.Trim
                .PaperHeight = txtLabelHeight.Text.Trim
                .X = txtBarcodeX.Text.Trim
                .Y = txtBarcodeY.Text.Trim
                .Height = txtBarcodeHeight.Text.Trim
                .Narrow = txtBarcodeNarrow.Text.Trim
                .Wide = txtBarcodeWide.Text.Trim
                .PrinterName = txtBarcodeName.Text.Trim
                .IsLocationName = chkIsLocationName.Checked
                .MMName = IIf(IsDBNull(txtMMName.Text), "", txtMMName.Text)
                .EngName = IIf(IsDBNull(txtEngName.Text), "", txtEngName.Text)
                .RightPositionX = txtRightPositionX.Text.Trim
                .IsIncludeGemWgt = chkSummary.Checked
                .IsIncludeGemPrice = chkGemPrice.Checked

                .IsFixGemQTY = chkGemQTY.Checked
                .IsFixGemWeight = chkGemWeight.Checked
                .IsFixGold = chkGoldWeight.Checked
                .IsLength = chkLength.Checked
                .IsPrefix = chkPrefix.Checked
                .IsAllDetail = chkDetail.Checked
                .IsFixItem = chkItemWeight.Checked
                .IsOriginalCode = chkOriginalCode.Checked
                .IsPriceCode = chkPriceCode.Checked
                .IsDescription = chkDescription.Checked
                .IsWaste = chkWaste.Checked
                .IsShowGram = chkShowGram.Checked
                .IsShowGW = chkShowGW.Checked
                .IsFixPrice = chkFixPrice.Checked
                .LeftFontSize = txtLeft.Text
                .RightFontSize = txtRight.Text
            End With
            Result = _BarcodeSettingController.SaveBarcodeSetting(info)
            Return Result
        Else
            Return Result
        End If
    End Function

    Private Sub LoadBarcodeSetting()
        Dim dt As New DataTable
        Dim objMeasurement As New MeasurementInfo
        dt = _BarcodeSettingController.GetBarcode()
        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.NewRow
                dr = dt.Rows(i)
                txtLabelWidth.Text = dr.Item("PaperWidth")
                txtLabelHeight.Text = dr.Item("PaperHeight")
                txtBarcodeX.Text = dr.Item("X")
                txtBarcodeY.Text = dr.Item("Y")
                txtBarcodeHeight.Text = dr.Item("Height")
                txtBarcodeNarrow.Text = dr.Item("Narrow")
                txtBarcodeWide.Text = dr.Item("Wide")
                txtBarcodeName.Text = dr.Item("PrinterName")
                chkIsLocationName.Checked = dr.Item("IsLocationName")
                If chkIsLocationName.Checked Then
                    txtEngName.Text = dr.Item("EngName")
                    txtMMName.Text = dr.Item("MMName")
                End If
                txtRightPositionX.Text = dr.Item("RightPositionX")
                chkSummary.Checked = dr.Item("IsIncludeGemWgt")
                chkGemPrice.Checked = dr.Item("IsIncludeGemPrice")
                chkGemQTY.Checked = dr.Item("IsFixGemQTY")
                chkGemWeight.Checked = dr.Item("IsFixGemWeight")
                chkGoldWeight.Checked = dr.Item("IsFixGold")
                chkItemWeight.Checked = dr.Item("IsFixItem")
                chkLength.Checked = dr.Item("IsLength")
                chkPrefix.Checked = dr.Item("IsPrefix")
                chkDetail.Checked = dr.Item("IsAllDetail")
                chkOriginalCode.Checked = dr.Item("IsOriginalCode")
                chkPriceCode.Checked = dr.Item("IsPriceCode")
                chkWaste.Checked = dr.Item("IsWaste")
                chkDescription.Checked = dr.Item("IsDescription")
                chkShowGram.Checked = dr.Item("IsGram")
                chkShowGW.Checked = dr.Item("IsShowGW")
                chkFixPrice.Checked = dr.Item("IsFixPrice")
                txtLeft.Text = dr.Item("LeftFontSize")
                txtRight.Text = dr.Item("RightFontSize")
            Next
        End If
    End Sub

    Private Sub Clear()
        txtLabelWidth.Text = 0
        txtLabelHeight.Text = 0
        txtBarcodeHeight.Text = 0
        txtBarcodeName.Text = 0
        txtBarcodeNarrow.Text = 0
        txtBarcodeWide.Text = 0
        txtBarcodeX.Text = 0
        txtBarcodeY.Text = 0
        chkIsLocationName.Checked = False
        lblEng.Visible = False
        lblMM.Visible = False
        txtEngName.Visible = False
        txtMMName.Visible = False
        txtRightPositionX.Text = 0
        chkItemWeight.Checked = False
        chkGoldWeight.Checked = False
        chkGemQTY.Checked = False
        chkGemWeight.Checked = False
        chkSummary.Checked = False
        chkDetail.Checked = False
        chkGemPrice.Checked = False
        chkSummary.Checked = False
        chkGemPrice.Checked = False
        chkOriginalCode.Checked = False
        chkPriceCode.Checked = False
        chkDescription.Checked = False
        chkWaste.Checked = False
        chkShowGram.Checked = False
        chkShowGW.Checked = False
        chkFixPrice.Checked = False
    End Sub
    Private Sub txtBarcodeHeight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcodeHeight.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtBarcodeNarrow_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcodeNarrow.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtBarcodeWide_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcodeWide.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtBarcodeX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcodeX.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtBarcodeY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcodeY.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtLabelHeight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabelHeight.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtLabelWidth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabelWidth.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub txtRightPositionX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRightPositionX.KeyPress
        MyBase.ValidateNumeric(sender, e, True)
    End Sub

    Private Sub chkIsLocationName_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsLocationName.CheckedChanged
        If chkIsLocationName.Checked Then
            lblEng.Visible = True
            lblMM.Visible = True
            txtEngName.Visible = True
            txtMMName.Visible = True
            txtEngName.Text = ""
            txtMMName.Text = ""
        Else
            lblEng.Visible = False
            lblMM.Visible = False
            txtEngName.Visible = False
            txtMMName.Visible = False
        End If
        If chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\12\OR01"
            lblDLocation.Text = "Shop\18K\12\OR01"
            lblDLocation2.Text = "Shop\18K\12\OR01"
            lblDiaLocation.Text = "Shop\Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K\12"
            lblDLocation.Text = "Shop\18K\12"
            lblDLocation2.Text = "Shop\18K\12"
            lblDiaLocation.Text = "Shop\Dia\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\12\OR01"
            lblDLocation.Text = "18K\12\OR01"
            lblDLocation2.Text = "18K\12\OR01"
            lblDiaLocation.Text = "Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\OR01"
            lblDLocation.Text = "Shop\18K\OR01"
            lblDLocation2.Text = "Shop\18K\OR01"
            lblDiaLocation.Text = "Shop\Dia\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\12\OR01"
            lblDLocation.Text = "Shop\12\OR01"
            lblDLocation2.Text = "Shop\12\OR01"
            lblDiaLocation.Text = "Shop\12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\OR01"
            lblDLocation.Text = "Shop\OR01"
            lblDLocation2.Text = "Shop\OR01"
            lblDiaLocation.Text = "Shop\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "12\OR01"
            lblDLocation.Text = "12\OR01"
            lblDLocation2.Text = "12\OR01"
            lblDiaLocation.Text = "12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\12"
            lblDLocation.Text = "Shop\12"
            lblDLocation2.Text = "Shop\12"
            lblDiaLocation.Text = "Shop\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\OR01"
            lblDLocation.Text = "18K\OR01"
            lblDLocation2.Text = "18K\OR01"
            lblDiaLocation.Text = "Dia\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K"
            lblDLocation.Text = "Shop\18K"
            lblDLocation2.Text = "Shop\18K"
            lblDiaLocation.Text = "Shop\Dia"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K\12"
            lblDLocation.Text = "18K\12"
            lblDLocation2.Text = "18K\12"
            lblDiaLocation.Text = "Dia\12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = ""
            lblDLocation.Text = ""
            lblDLocation2.Text = ""
            lblDiaLocation.Text = ""
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K"
            lblDLocation.Text = "18K"
            lblDLocation2.Text = "18K"
            lblDiaLocation.Text = "Dia"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "12"
            lblDLocation.Text = "12"
            lblDLocation2.Text = "12"
            lblDiaLocation.Text = "12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop"
            lblDLocation.Text = "Shop"
            lblDLocation2.Text = "Shop"
            lblDiaLocation.Text = "Shop"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "OR01"
            lblDLocation.Text = "OR01"
            lblDLocation2.Text = "OR01"
            lblDiaLocation.Text = "OR01"
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("BarcodeSetting")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub chkSummary_CheckedChanged(sender As Object, e As EventArgs) Handles chkSummary.CheckedChanged, chkDetail.CheckedChanged
        If chkSummary.Checked Or chkDetail.Checked Then
            chkGemPrice.Enabled = True
        Else
            chkGemPrice.Enabled = False
            chkGemPrice.Checked = False
        End If

        If chkDetail.Checked Then
            chkSummary.Enabled = False
            chkSummary.Checked = False
            chkGemPrice.Text = "အထည်တွင်ပါရှိသောကျောက်များ၏စျေးကို ပြပါမည်။"
            chkDescription.Enabled = True
        Else
            chkSummary.Enabled = True
            chkGemPrice.Text = "အထည်တွင်ပါရှိသောကျောက်များ၏စျေးကို ပြပါမည်။"
            chkDescription.Checked = False
            chkDescription.Enabled = False
        End If

        If chkSummary.Checked Then
            chkDetail.Enabled = False
            chkDetail.Checked = False
            chkGemPrice.Text = "အထည်တွင်ပါရှိသောကျောက်များ၏ စုစုပေါင်းအရေအတွက်/စုစုပေါင်းစျေးကို ပြပါမည်။"
        Else
            chkDetail.Enabled = True
            chkGemPrice.Text = "အထည်တွင်ပါရှိသောကျောက်များ၏စျေးကို ပြပါမည်။"
        End If
        ShowBarcodeForGem()
    End Sub

    Private Sub chkPrefix_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrefix.CheckedChanged

        If chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\12\OR01"
            lblDLocation.Text = "Shop\18K\12\OR01"
            lblDLocation2.Text = "Shop\18K\12\OR01"
            lblDiaLocation.Text = "Shop\Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K\12"
            lblDLocation.Text = "Shop\18K\12"
            lblDLocation2.Text = "Shop\18K\12"
            lblDiaLocation.Text = "Shop\Dia\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\12\OR01"
            lblDLocation.Text = "18K\12\OR01"
            lblDLocation2.Text = "18K\12\OR01"
            lblDiaLocation.Text = "Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\OR01"
            lblDLocation.Text = "Shop\18K\OR01"
            lblDLocation2.Text = "Shop\18K\OR01"
            lblDiaLocation.Text = "Shop\Dia\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\12\OR01"
            lblDLocation.Text = "Shop\12\OR01"
            lblDLocation2.Text = "Shop\12\OR01"
            lblDiaLocation.Text = "Shop\12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\OR01"
            lblDLocation.Text = "Shop\OR01"
            lblDLocation2.Text = "Shop\OR01"
            lblDiaLocation.Text = "Shop\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "12\OR01"
            lblDLocation.Text = "12\OR01"
            lblDLocation2.Text = "12\OR01"
            lblDiaLocation.Text = "12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\12"
            lblDLocation.Text = "Shop\12"
            lblDLocation2.Text = "Shop\12"
            lblDiaLocation.Text = "Shop\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\OR01"
            lblDLocation.Text = "18K\OR01"
            lblDLocation2.Text = "18K\OR01"
            lblDiaLocation.Text = "Dia\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K"
            lblDLocation.Text = "Shop\18K"
            lblDLocation2.Text = "Shop\18K"
            lblDiaLocation.Text = "Shop\Dia"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K\12"
            lblDLocation.Text = "18K\12"
            lblDLocation2.Text = "18K\12"
            lblDiaLocation.Text = "Dia\12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = ""
            lblDLocation.Text = ""
            lblDLocation2.Text = ""
            lblDiaLocation.Text = ""
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K"
            lblDLocation.Text = "18K"
            lblDLocation2.Text = "18K"
            lblDiaLocation.Text = "Dia"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "12"
            lblDLocation.Text = "12"
            lblDLocation2.Text = "12"
            lblDiaLocation.Text = "12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop"
            lblDLocation.Text = "Shop"
            lblDLocation2.Text = "Shop"
            lblDiaLocation.Text = "Shop"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "OR01"
            lblDLocation.Text = "OR01"
            lblDLocation2.Text = "OR01"
            lblDiaLocation.Text = "OR01"
        End If

    End Sub
    Private Sub chkLength_CheckedChanged(sender As Object, e As EventArgs) Handles chkLength.CheckedChanged
        If chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\12\OR01"
            lblDLocation.Text = "Shop\18K\12\OR01"
            lblDLocation2.Text = "Shop\18K\12\OR01"
            lblDiaLocation.Text = "Shop\Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K\12"
            lblDLocation.Text = "Shop\18K\12"
            lblDLocation2.Text = "Shop\18K\12"
            lblDiaLocation.Text = "Shop\Dia\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\12\OR01"
            lblDLocation.Text = "18K\12\OR01"
            lblDLocation2.Text = "18K\12\OR01"
            lblDiaLocation.Text = "Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\OR01"
            lblDLocation.Text = "Shop\18K\OR01"
            lblDLocation2.Text = "Shop\18K\OR01"
            lblDiaLocation.Text = "Shop\Dia\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\12\OR01"
            lblDLocation.Text = "Shop\12\OR01"
            lblDLocation2.Text = "Shop\12\OR01"
            lblDiaLocation.Text = "Shop\12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\OR01"
            lblDLocation.Text = "Shop\OR01"
            lblDLocation2.Text = "Shop\OR01"
            lblDiaLocation.Text = "Shop\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "12\OR01"
            lblDLocation.Text = "12\OR01"
            lblDLocation2.Text = "12\OR01"
            lblDiaLocation.Text = "12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\12"
            lblDLocation.Text = "Shop\12"
            lblDLocation2.Text = "Shop\12"
            lblDiaLocation.Text = "Shop\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\OR01"
            lblDLocation.Text = "18K\OR01"
            lblDLocation2.Text = "18K\OR01"
            lblDiaLocation.Text = "Dia\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K"
            lblDLocation.Text = "Shop\18K"
            lblDLocation2.Text = "Shop\18K"
            lblDiaLocation.Text = "Shop\Dia"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K\12"
            lblDLocation.Text = "18K\12"
            lblDLocation2.Text = "18K\12"
            lblDiaLocation.Text = "Dia\12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = ""
            lblDLocation.Text = ""
            lblDLocation2.Text = ""
            lblDiaLocation.Text = ""
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K"
            lblDLocation.Text = "18K"
            lblDLocation2.Text = "18K"
            lblDiaLocation.Text = "Dia"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "12"
            lblDLocation.Text = "12"
            lblDLocation2.Text = "12"
            lblDiaLocation.Text = "12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop"
            lblDLocation.Text = "Shop"
            lblDLocation2.Text = "Shop"
            lblDiaLocation.Text = "Shop"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "OR01"
            lblDLocation.Text = "OR01"
            lblDLocation2.Text = "OR01"
            lblDiaLocation.Text = "OR01"
        End If
    End Sub
    Private Sub chkoriginalcode_CheckedChanged(sender As Object, e As EventArgs) Handles chkOriginalCode.CheckedChanged
        If chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\12\OR01"
            lblDLocation.Text = "Shop\18K\12\OR01"
            lblDLocation2.Text = "Shop\18K\12\OR01"
            lblDiaLocation.Text = "Shop\Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K\12"
            lblDLocation.Text = "Shop\18K\12"
            lblDLocation2.Text = "Shop\18K\12"
            lblDiaLocation.Text = "Shop\Dia\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\12\OR01"
            lblDLocation.Text = "18K\12\OR01"
            lblDLocation2.Text = "18K\12\OR01"
            lblDiaLocation.Text = "Dia\12\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\18K\OR01"
            lblDLocation.Text = "Shop\18K\OR01"
            lblDLocation2.Text = "Shop\18K\OR01"
            lblDiaLocation.Text = "Shop\Dia\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\12\OR01"
            lblDLocation.Text = "Shop\12\OR01"
            lblDLocation2.Text = "Shop\12\OR01"
            lblDiaLocation.Text = "Shop\12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "Shop\OR01"
            lblDLocation.Text = "Shop\OR01"
            lblDLocation2.Text = "Shop\OR01"
            lblDiaLocation.Text = "Shop\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "12\OR01"
            lblDLocation.Text = "12\OR01"
            lblDLocation2.Text = "12\OR01"
            lblDiaLocation.Text = "12\OR01"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\12"
            lblDLocation.Text = "Shop\12"
            lblDLocation2.Text = "Shop\12"
            lblDiaLocation.Text = "Shop\12"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "18K\OR01"
            lblDLocation.Text = "18K\OR01"
            lblDLocation2.Text = "18K\OR01"
            lblDiaLocation.Text = "Dia\OR01"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop\18K"
            lblDLocation.Text = "Shop\18K"
            lblDLocation2.Text = "Shop\18K"
            lblDiaLocation.Text = "Shop\Dia"
        ElseIf chkPrefix.Checked = True And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K\12"
            lblDLocation.Text = "18K\12"
            lblDLocation2.Text = "18K\12"
            lblDiaLocation.Text = "Dia\12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = ""
            lblDLocation.Text = ""
            lblDLocation2.Text = ""
            lblDiaLocation.Text = ""
        ElseIf chkPrefix.Checked = True And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "18K"
            lblDLocation.Text = "18K"
            lblDLocation2.Text = "18K"
            lblDiaLocation.Text = "Dia"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = True And chkIsLocationName.Checked = False And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "12"
            lblDLocation.Text = "12"
            lblDLocation2.Text = "12"
            lblDiaLocation.Text = "12"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = True And chkOriginalCode.Checked = False Then
            'For single
            lblSLocation.Text = "Shop"
            lblDLocation.Text = "Shop"
            lblDLocation2.Text = "Shop"
            lblDiaLocation.Text = "Shop"
        ElseIf chkPrefix.Checked = False And chkLength.Checked = False And chkIsLocationName.Checked = False And chkOriginalCode.Checked = True Then
            'For single
            lblSLocation.Text = "OR01"
            lblDLocation.Text = "OR01"
            lblDLocation2.Text = "OR01"
            lblDiaLocation.Text = "OR01"
        End If
    End Sub
    Private Sub chkItemWeight_checkedchanged(sendar As Object, e As EventArgs) Handles chkItemWeight.CheckedChanged
        If chkItemWeight.Checked = True And chkWaste.Checked = True Then
            txtItemWeight.Visible = True
            txtWasteWeight.Visible = True
            'txtDiaItemWeight.Text = "G+D>3g W>0g"
            ShowBarcodeForGem()
        ElseIf chkItemWeight.Checked = True And chkWaste.Checked = False Then
            txtItemWeight.Visible = True
            txtWasteWeight.Visible = False
            'txtDiaItemWeight.Text = "G+D>3g "
            ShowBarcodeForGem()
        ElseIf chkItemWeight.Checked = False And chkWaste.Checked = True Then
            txtItemWeight.Visible = False
            txtWasteWeight.Visible = True
            'txtDiaItemWeight.Text = "W>0g "
            ShowBarcodeForGem()
        Else
            txtItemWeight.Visible = False
            txtWasteWeight.Visible = False
            txtDiaItemWeight.Text = ""
            ShowBarcodeForGem()
        End If
    End Sub
    Private Sub chkFixPrice_checkedchanged(sender As Object, e As EventArgs) Handles chkFixPrice.CheckedChanged
        If chkFixPrice.Checked = True And chkPriceCode.Checked = True Then
            txtFPrice.Text = "(500,000) (400,000)"
            txtDFPrice1.Text = "(500,000) (400,000)"
            txtDFPrice2.Text = "(500,000) (400,000)"
            txtDFPrice3.Text = "(500,000) (400,000)"
        ElseIf chkFixPrice.Checked = True And chkPriceCode.Checked = False Then
            txtFPrice.Text = "(500,000)"
            txtDFPrice1.Text = "(500,000)"
            txtDFPrice2.Text = "(500,000)"
            txtDFPrice3.Text = "(500,000)"
        ElseIf chkFixPrice.Checked = False And chkPriceCode.Checked = True Then
            txtFPrice.Text = "(400,000)"
            txtDFPrice1.Text = "(400,000)"
            txtDFPrice2.Text = "(400,000)"
            txtDFPrice3.Text = "(400,000)"
        Else
            txtFPrice.Text = ""
            txtDFPrice1.Text = ""
            txtDFPrice2.Text = ""
            txtDFPrice3.Text = ""
        End If
    End Sub
    Private Sub chkPriceCode_Checkedchanged(sender As Object, e As EventArgs) Handles chkPriceCode.CheckedChanged
        If chkFixPrice.Checked = True And chkPriceCode.Checked = True Then
            txtFPrice.Text = "(500,000) (400,000)"
            txtDFPrice1.Text = "(500,000) (400,000)"
            txtDFPrice2.Text = "(500,000) (400,000)"
            txtDFPrice3.Text = "(500,000) (400,000)"
        ElseIf chkFixPrice.Checked = True And chkPriceCode.Checked = False Then
            txtFPrice.Text = "(500,000)"
            txtDFPrice1.Text = "(500,000)"
            txtDFPrice2.Text = "(500,000)"
            txtDFPrice3.Text = "(500,000)"
        ElseIf chkFixPrice.Checked = False And chkPriceCode.Checked = True Then
            txtFPrice.Text = "(400,000)"
            txtDFPrice1.Text = "(400,000)"
            txtDFPrice2.Text = "(400,000)"
            txtDFPrice3.Text = "(400,000)"
        Else
            txtFPrice.Text = ""
            txtDFPrice1.Text = ""
            txtDFPrice2.Text = ""
            txtDFPrice3.Text = ""
        End If
    End Sub
    Private Sub chkWaste_Checkedchanged(sender As Object, e As EventArgs) Handles chkWaste.CheckedChanged
        If chkItemWeight.Checked = True And chkWaste.Checked = True Then
            txtItemWeight.Visible = True
            txtWasteWeight.Visible = True
            txtDiaItemWeight.Text = "G+D>3g W>0g"
        ElseIf chkItemWeight.Checked = True And chkWaste.Checked = False Then
            txtItemWeight.Visible = True
            txtWasteWeight.Visible = False
            txtDiaItemWeight.Text = "G+D>3g "
        ElseIf chkItemWeight.Checked = False And chkWaste.Checked = True Then
            txtItemWeight.Visible = False
            txtWasteWeight.Visible = True
            txtDiaItemWeight.Text = "W>0g "
        Else
            txtItemWeight.Visible = False
            txtWasteWeight.Visible = False
            txtDiaItemWeight.Visible = False
        End If
    End Sub
    Private Sub chkGoldWeight_Checkedchanged(sender As Object, e As EventArgs) Handles chkGoldWeight.CheckedChanged
        ShowBarcodeForGem()
    End Sub
    Private Sub chkGemWeight_Checkedchanged(sender As Object, e As EventArgs) Handles chkGemWeight.CheckedChanged
        'If chkGemWeight.Checked = True And chkGemQTY.Checked = False And chkItemWeight.Checked = False Then
        '    txtDiaItemWeight.Text = "D>0.2"
        'ElseIf chkGemWeight.Checked = True And chkGemQTY.Checked = True And chkItemWeight.Checked = False Then
        '    txtDiaItemWeight.Text = "D>2p, 0.2"
        'ElseIf chkGemWeight.Checked = False And chkGemQTY.Checked = True And chkItemWeight.Checked = False Then
        '    txtDiaItemWeight.Text = "D>2p"
        '    ' ElseIf chkGemWeight.Checked = True And chkGemQTY.Checked = True And chkItemWeight.Checked = True Then
        '    'txtDiaWeight.Text = ""
        'End If

        'If chkGemWeight.Checked = True Then
        '    If chkGemQTY.Checked = True Then
        '        If chkItemWeight.Checked = True Then
        '            If chkWaste.Checked = True Then
        '                txtDiaItemWeight.Text = "G+D>3g W>0g"
        '            Else
        '                txtDiaItemWeight.Text = "G+D>3g "
        '            End If
        '            If chkGoldWeight.Checked = True Then
        '                txtDiaWeight.Text = "G>2.962g D>0.332g"
        '            Else
        '                txtDiaWeight.Text = ""
        '            End If
        '            If chkDetail.Checked = True Then
        '                If chkGemPrice.Checked = True Then
        '                    txtDiaDetail1.Text = "7p,0.32ct,50000"
        '                    txtDiaDetail2.Text = "12p,0.99ct,40000"
        '                Else
        '                    txtDiaDetail1.Text = "7p,0.32ct"
        '                    txtDiaDetail2.Text = "12p,0.99ct"
        '                End If
        '            Else
        '                txtDiaDetail1.Text = ""
        '                txtDiaDetail2.Text = ""

        '            End If
        '        Else
        '            If chkGoldWeight.Checked = True Then
        '                txtDiaItemWeight.Text = "G>2.962"
        '            Else
        '                txtDiaItemWeight.Text = ""
        '            End If
        '        End If
        '        'txtDiaItemWeight.Text = "D>2p, 0.2"
        '    Else
        '        txtDiaItemWeight.Text = "D>0.2"
        '    End If
        'Else
        '    txtDiaItemWeight.Text = "D>2p"
        'End If
        ShowBarcodeForGem()
    End Sub
    Private Sub chkGemQty_Checkedchanged(sender As Object, e As EventArgs) Handles chkGemQTY.CheckedChanged
        ShowBarcodeForGem()
    End Sub
    Private Sub chkDetail_Checkedchanged(sender As Object, e As EventArgs) Handles chkDetail.CheckedChanged
        ShowBarcodeForGem()
    End Sub
    Private Sub chkGemPrice_Checkedchanged(sender As Object, e As EventArgs) Handles chkGemPrice.CheckedChanged
        ShowBarcodeForGem()
    End Sub

    Private Sub ShowBarcodeForGem()
        If chkItemWeight.Checked = True Then
            If chkWaste.Checked = True Then
                txtDiaItemWeight.Text = "G+D>5 W>0"
            Else
                txtDiaItemWeight.Text = "G+D>5"
            End If
        Else
            If chkWaste.Checked = True Then
                txtDiaItemWeight.Text = "W> 0"
            Else
                txtDiaItemWeight.Text = ""
            End If
        End If
        If chkGoldWeight.Checked = True Then
            txtDiaWeight.Text = "G>3"
        Else
            txtDiaWeight.Text = ""
        End If
        If chkDetail.Checked = False Then
            If chkGemWeight.Checked = True And chkGemQTY.Checked = True Then
                If chkGemPrice.Checked = True Then
                    txtDiaDetail1.Text = "D>2p, 0.2,30,000"
                    txtDiaDetail2.Text = "D>1p, 0.2,40,000"
                Else
                    txtDiaDetail1.Text = "D>2p, 0.2"
                    txtDiaDetail2.Text = "D>1p, 0.2"
                End If
            ElseIf chkGemWeight.Checked = True And chkGemQTY.Checked = False Then
                If chkGemPrice.Checked = True Then
                    txtDiaDetail1.Text = "D> 0.2,30,000"
                    txtDiaDetail2.Text = "D> 0.2,40,000"
                Else
                    txtDiaDetail1.Text = "D> 0.2"
                    txtDiaDetail2.Text = "D> 0.2"
                End If

            ElseIf chkGemWeight.Checked = False And chkGemQTY.Checked = True Then
                If chkGemPrice.Checked = True Then
                    txtDiaDetail1.Text = "D> 2p,30,000"
                    txtDiaDetail2.Text = "D> 2p,40,000"
                Else
                    txtDiaDetail1.Text = "D> 2p"
                    txtDiaDetail2.Text = "D> 2p"
                End If
            Else
                txtDiaDetail1.Text = ""
                txtDiaDetail2.Text = ""
            End If
        Else
            If chkGemPrice.Checked = True Then
                txtDiaDetail1.Text = "2p, 1R2B, 50000"
                txtDiaDetail2.Text = "1p, 2R2B, 40000"
            Else
                txtDiaDetail1.Text = "2p, 1R2B"
                txtDiaDetail2.Text = "1p, 2R2B"
            End If
        End If

        If chkSummary.Checked = True Then
            txtDiaDetail1.Text = "D>3p,0.4"
            txtDiaDetail2.Text = ""
        End If

    End Sub
End Class
