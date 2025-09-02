Imports CommonInfo
Imports BusinessRule

Public Class frm_BarcodeSetting
    Implements IFormProcess
    Private _BarcodeSettingController As Barcodesetting.IBarcodeSettingController = Factory.Instance.CreateBarcodeSettingController

    Private Sub frm_BarcodeSetting_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtLabelWidth.Focus()
    End Sub

    Private Sub frm_BarcodeSetting_Load(sender As Object, e As EventArgs) Handles Me.Load
        '_Heading.Text = "Barcode Setting"
        MyBase.btnNew.Visible = False
        MyBase.btnDelete.Visible = False
        chkGemPrice.Enabled = False
        LoadBarcodeSetting()
    End Sub
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
                chkGemWgt.Checked = dr.Item("IsIncludeGemWgt")
                chkGemPrice.Checked = dr.Item("IsIncludeGemPrice")
            Next
        End If
    End Sub
    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew

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
                .IsIncludeGemWgt = chkGemWgt.Checked
                .IsIncludeGemPrice = chkGemPrice.Checked
            End With
            Result = _BarcodeSettingController.SaveBarcodeSetting(info)
            Return Result
        Else
            Return Result
        End If

    End Function
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
        chkGemWgt.Checked = False
        chkGemPrice.Checked = False
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
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("BarcodeSetting")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub chkGemWgt_CheckedChanged(sender As Object, e As EventArgs) Handles chkGemWgt.CheckedChanged
        If chkGemWgt.Checked Then
            chkGemPrice.Enabled = True
        Else
            chkGemPrice.Enabled = False
            chkGemPrice.Checked = False
        End If
    End Sub


End Class
