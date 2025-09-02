Imports CommonInfo
Imports BusinessRule
Imports System.Drawing.FontStyle
Public Class frm_VoucherSetting
    Implements IFormProcess
    Dim OpenFileDia As New OpenFileDialog
    Dim PName As String
    Dim DefaultPhoto As String
    Dim _FirstColor As String
    Dim _LastColor As String
    Dim _FirstFontColor As String
    Dim _SecondFontColor As String
    Dim _ThirdFontColor As String
    'Dim fontDlg As New FontDialog
    Dim fonttitle As New FontDialog
    Dim fontSecond As New FontDialog
    Dim fontAddress As New FontDialog
    Private objVoucherSettingController As VoucherSetting.IVoucherSettingController = Factory.Instance.CreateVoucherSettingController
    Private objSalesInvoiceController As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew

    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim info As VoucherSettingInfo
        Dim Result As Boolean = False
        objVoucherSettingController.DeleteVoucherSetting()
        'If txtGtoKarat.Text <> "" And txtKaratToYati.Text <> "" And txtYatiToB.Text <> "" And txtBToSate.Text <> "" And txtKyattharToGram.Text <> "" Then
        info = New VoucherSettingInfo
        With info
            If txtFirstTitle.Text <> "" Then
                .TitleType = "FirstTitle"
                .Title = txtFirstTitle.Text
                .FontName = txtFirstTitle.Font.Name
                .Bold = txtFirstTitle.Font.Bold
                .Italic = txtFirstTitle.Font.Italic
                ' _FirstFontColor = txtFirstTitle.ForeColor.Name.ToString
                .FontColor = IIf(_FirstFontColor = "", "Black", _FirstFontColor)
                .FontSize = txtFirstTitle.Font.Size.ToString()
                .FontR = txtFirstTitle.ForeColor.R
                .FontG = txtFirstTitle.ForeColor.G
                .FontB = txtFirstTitle.ForeColor.B
                .Photo = PName
            Else
                .TitleType = "FirstTitle"
                .Title = ""
                .FontName = "Zawgyi-One"
                .Bold = False
                .Italic = False
                ' _FirstFontColor = txtFirstTitle.ForeColor.Name.ToString
                .FontColor = "Black"
                .FontSize = "8.0"
                .FontR = "0"
                .FontG = "0"
                .FontB = "0"
                .Photo = PName
            End If
           
        End With
        Result = objVoucherSettingController.SaveVoucherSetting(info)
        info = New VoucherSettingInfo
        With info
            If txtSecondTitle.Text <> "" Then
                .TitleType = "SecondTitle"
                .Title = txtSecondTitle.Text
                .FontName = txtSecondTitle.Font.Name
                .Bold = txtSecondTitle.Font.Bold
                .Italic = txtSecondTitle.Font.Italic
                .FontSize = txtSecondTitle.Font.Size.ToString()
                ' _SecondFontColor = txtFirstTitle.ForeColor.Name.ToString
                .FontColor = IIf(_SecondFontColor = "", "Black", _SecondFontColor)
                .FontR = txtSecondTitle.ForeColor.R
                .FontG = txtSecondTitle.ForeColor.G
                .FontB = txtSecondTitle.ForeColor.B
                .Photo = PName
            Else
                .TitleType = "SecondTitle"
                .Title = ""
                .FontName = "Zawgyi-One"
                .Bold = False
                .Italic = False
                ' _FirstFontColor = txtFirstTitle.ForeColor.Name.ToString
                .FontColor = "Black"
                .FontSize = "8.0"
                .FontR = "0"
                .FontG = "0"
                .FontB = "0"
                .Photo = PName
            End If
           
        End With
        Result = objVoucherSettingController.SaveVoucherSetting(info)
        info = New VoucherSettingInfo
        With info
            If txtAddress.Text <> "" Then
                .TitleType = "ThirdTitle"
                .Title = txtAddress.Text
                .FontName = txtAddress.Font.Name
                .Bold = txtAddress.Font.Bold
                .Italic = txtAddress.Font.Italic
                .FontSize = txtAddress.Font.Size.ToString()
                '_ThirdFontColor = txtFirstTitle.ForeColor.Name.ToString
                .FontColor = IIf(_ThirdFontColor = "", "Black", _ThirdFontColor)
                '.FontColor = _ThirdFontColor
                .FontR = txtAddress.ForeColor.R
                .FontG = txtAddress.ForeColor.G
                .FontB = txtAddress.ForeColor.B
                .Photo = PName
            Else
                .TitleType = "ThirdTitle"
                .Title = ""
                .FontName = "Zawgyi-One"
                .Bold = False
                .Italic = False
                ' _FirstFontColor = txtFirstTitle.ForeColor.Name.ToString
                .FontColor = "Black"
                .FontSize = "8.0"
                .FontR = "0"
                .FontG = "0"
                .FontB = "0"
                .Photo = PName
            End If
            
        End With
        Result = objVoucherSettingController.SaveVoucherSetting(info)
        Return Result
        'Else
        'Return Result
        'End If
    End Function

    Private Sub frm_VoucherSetting_Load(sender As Object, e As EventArgs) Handles Me.Load

        ' MyBase._Heading.Text = "Voucher Header Setting"
        MyBase.btnNew.Visible = False
        MyBase.btnDelete.Visible = False
        lblItemImage.Image = Nothing
        PName = ""
        btnAdd.Text = "Add"
        lblPhoto.Visible = True
        txtFirstTitle.TextAlign = HorizontalAlignment.Center
        txtSecondTitle.TextAlign = HorizontalAlignment.Center
        txtAddress.TextAlign = HorizontalAlignment.Left
        LoadVoucherSetting()
    End Sub
    Private Sub LoadVoucherSetting()
        Dim dt As New DataTable
        Dim objVoucherSetting As New VoucherSettingInfo
        dt = objVoucherSettingController.GetAllVoucherSetting()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.NewRow
                dr = dt.Rows(i)
                If dr.Item("TitleType").ToString() = "FirstTitle" Then
                    If dr.Item("Title").ToString = "" Then
                        txtFirstTitle.ForeColor = Color.Black
                        txtFirstTitle.Font = New System.Drawing.Font("Zawgyi-One", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        txtFirstTitle.Text = ""
                        _FirstFontColor = "Black"
                    Else
                        txtFirstTitle.ForeColor = Color.FromArgb(dr.Item("FontR"), dr.Item("FontG"), dr.Item("FontB"))
                        'txtFirstTitle.Text = dr.Item("Title")
                        _FirstFontColor = dr.Item("FontColor").ToString
                        If dr.Item("Italic") = False And dr.Item("Bold") = True Then
                            txtFirstTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        ElseIf dr.Item("Bold") = False And dr.Item("Italic") = True Then
                            txtFirstTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        ElseIf dr.Item("Bold") And dr.Item("Italic") Then
                            txtFirstTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                            txtFirstTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Bold Or Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                        Else
                            txtFirstTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        End If
                        txtFirstTitle.Text = dr.Item("Title")
                    End If

                    If dr.Item("Photo") <> "" Then
                        lblItemImage.Image = System.Drawing.Image.FromFile(Global_PhotoPath + "\" + dr.Item("Photo"))
                        PName = dr.Item("Photo")
                        btnAdd.Text = "Remove"
                        lblPhoto.Visible = False
                    Else
                        lblItemImage.Image = Nothing
                        PName = ""
                        btnAdd.Text = "Add"
                        lblPhoto.Visible = True
                    End If

                ElseIf dr.Item("TitleType").ToString() = "SecondTitle" Then
                    If dr.Item("Title") = "" Then
                        txtSecondTitle.ForeColor = Color.Black
                        txtSecondTitle.Font = New System.Drawing.Font("Zawgyi-One", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        txtSecondTitle.Text = ""
                        _SecondFontColor = "Black"
                    Else
                        txtSecondTitle.ForeColor = Color.FromArgb(dr.Item("FontR"), dr.Item("FontG"), dr.Item("FontB"))
                        'txtSecondTitle.Text = dr.Item("Title")
                        _SecondFontColor = dr.Item("FontColor").ToString
                        If dr.Item("Bold") = True And dr.Item("Italic") = False Then
                            txtSecondTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        ElseIf dr.Item("Italic") = True And dr.Item("Bold") = False Then
                            txtSecondTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        ElseIf dr.Item("Italic") And dr.Item("Bold") Then
                            txtSecondTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                            txtSecondTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Bold Or Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        Else
                            txtSecondTitle.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        End If
                        txtSecondTitle.Text = dr.Item("Title")
                    End If

                ElseIf dr.Item("TitleType").ToString() = "ThirdTitle" Then
                    If dr.Item("Title") = "" Then
                        txtAddress.ForeColor = Color.Black
                        txtAddress.Font = New System.Drawing.Font("Zawgyi-One", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        txtAddress.Text = ""
                        _ThirdFontColor = "Black"
                    Else
                        txtAddress.ForeColor = Color.FromArgb(dr.Item("FontR"), dr.Item("FontG"), dr.Item("FontB"))
                        'txtAddress.Text = dr.Item("Title")
                        _ThirdFontColor = dr.Item("FontColor").ToString
                        If dr.Item("Bold") = True And dr.Item("Italic") = False Then
                            txtAddress.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        ElseIf dr.Item("Italic") = True And dr.Item("Bold") = False Then
                            txtAddress.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        ElseIf dr.Item("Italic") And dr.Item("Bold") Then
                            txtAddress.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                            txtAddress.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Bold Or Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        Else
                            txtAddress.Font = New System.Drawing.Font(dr.Item("FontName").ToString, CSng(dr.Item("FontSize")), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                        End If
                        txtAddress.Text = dr.Item("Title")
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub btnfont_Click(sender As Object, e As EventArgs) Handles btnfont.Click
        'fonttitle.Reset()
        'fonttitle.FontMustExist = True
        fonttitle.MaxSize = 40
        fonttitle.MinSize = 8
        'fonttitle.ShowEffects = False
        fonttitle.ShowColor = True
        fonttitle.ShowApply = True
        fonttitle.Font = New Font(txtFirstTitle.Font, txtFirstTitle.Font.Style)
        ' fonttitle.Color = txtAddress.ForeColor.Name.ToString
        If Not _FirstFontColor Is Nothing Then
            fonttitle.Color = Color.FromName(_FirstFontColor)
        End If
        'fonttitle.Color = txtFirstTitle.ForeColor
        ' fonttitle.ShowHelp = True

        Try
            fonttitle.ShowDialog()

            If Windows.Forms.DialogResult.Cancel = True Then
                Exit Sub
            End If
            ' If Windows.Forms.DialogResult.OK = True Then
            txtFirstTitle.Font = fonttitle.Font
            txtFirstTitle.ForeColor = fonttitle.Color
            _FirstFontColor = txtFirstTitle.ForeColor.Name.ToString
            ' End If

        Catch ex As Exception
            MsgBox("Please Select Other Font!This font can not support for this system.", MsgBoxStyle.Information, "JewelleryShop Management System")
        End Try

        txtResult.Font = txtFirstTitle.Font
        txtResult.ForeColor = txtFirstTitle.ForeColor
    End Sub

    Private Sub btnSecondTitle_Click(sender As Object, e As EventArgs) Handles btnSecondTitle.Click
        'fonttitle.Reset()
        'fonttitle.FontMustExist = True
        fontSecond.MaxSize = 40
        fontSecond.MinSize = 8
        'fonttitle.ShowEffects = False
        fontSecond.ShowColor = True
        fonttitle.ShowApply = True
        'fonttitle.ShowEffects = False
        'fonttitle.ShowHelp = True
        fontSecond.Font = New Font(txtSecondTitle.Font, txtSecondTitle.Font.Style)
        If Not _SecondFontColor Is Nothing Then
            fontSecond.Color = Color.FromName(_SecondFontColor)
        End If

        Try
            fontSecond.ShowDialog()
            If Windows.Forms.DialogResult.Cancel = True Then
                Exit Sub
            End If
            txtSecondTitle.Font = fontSecond.Font
            txtSecondTitle.ForeColor = fontSecond.Color
            _SecondFontColor = txtSecondTitle.ForeColor.Name.ToString
        Catch ex As Exception
            MsgBox("Please Select Other Font!This font can not support for this system.", MsgBoxStyle.Information, "JewelleryShop Management System")
        End Try

        ''fontSecond.ShowColor = True
        ' ''fontSecond.ShowApply = True
        ' ''fontSecond.ShowEffects = True
        ''fontSecond.ShowHelp = True

        ''fontSecond.ShowDialog()

        ''fontSecond.MaxSize = 40
        ''fontSecond.MinSize = 8

        ''txtSecondTitle.Font = fontSecond.Font
        ''txtSecondTitle.ForeColor = fontSecond.Color

        txtResult2.Font = txtSecondTitle.Font
        txtResult2.ForeColor = txtSecondTitle.ForeColor

    End Sub

    Private Sub btnAddress_Click(sender As Object, e As EventArgs) Handles btnAddress.Click
        'fonttitle.Reset()
        'fonttitle.FontMustExist = True
        fontAddress.MaxSize = 40
        fontAddress.MinSize = 8
        'fonttitle.ShowEffects = False
        fontAddress.ShowColor = True
        fontAddress.ShowApply = True
        'fonttitle.ShowEffects = False
        'fonttitle.ShowHelp = True
        fontAddress.Font = New Font(txtAddress.Font, txtAddress.Font.Style)
        If Not _ThirdFontColor Is Nothing Then
            fontAddress.Color = Color.FromName(_ThirdFontColor)
        End If

        Try
            fontAddress.ShowDialog()
            If Windows.Forms.DialogResult.Cancel = True Then
                Exit Sub
            End If
            txtAddress.Font = fontAddress.Font
            txtAddress.ForeColor = fontAddress.Color
             _ThirdFontColor = txtAddress.ForeColor.Name.ToString
        Catch ex As Exception
            MsgBox("Please Select Other Font!This font can not support for this system.", MsgBoxStyle.Information, "JewelleryShop Management System")
        End Try


        ''fontAddress.ShowColor = True
        ' ''fontAddress.ShowApply = True
        ' ''fontAddress.ShowEffects = True
        ''fontAddress.ShowHelp = True

        ''fontAddress.ShowDialog()

        ''fontAddress.MaxSize = 40
        ''fontAddress.MinSize = 8

        ''txtAddress.Font = fontAddress.Font
        ''txtAddress.ForeColor = fontAddress.Color

        txtResult3.Font = txtAddress.Font
        txtResult3.ForeColor = txtAddress.ForeColor

    End Sub

    Private Sub txtFirstTitle_TextChanged(sender As Object, e As EventArgs) Handles txtFirstTitle.TextChanged
        If txtFirstTitle.Text <> "" Then
            'txtCustomer.Font = fonttitle.Font
            'txtCustomer.ForeColor = fonttitle.Color
            txtResult.Text = txtFirstTitle.Text
            txtResult.Font = txtFirstTitle.Font
            txtResult.ForeColor = txtFirstTitle.ForeColor
            txtResult.TextAlign = HorizontalAlignment.Center
        Else
            ' txtFirstTitle.Text = ""
            txtResult.Text = ""
            'txtFirstTitle.ForeColor = Color.Black
            'txtFirstTitle.Font = New System.Drawing.Font("Zawgyi-One", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'txtFirstTitle.Text = ""
            '_FirstFontColor = "Black"
        End If
    End Sub

    Private Sub txtSecondTitle_TextChanged(sender As Object, e As EventArgs) Handles txtSecondTitle.TextChanged
        If txtSecondTitle.Text <> "" Then

            txtResult2.Text = txtSecondTitle.Text
            txtResult2.Font = txtSecondTitle.Font
            txtResult2.ForeColor = txtSecondTitle.ForeColor
            txtResult2.TextAlign = HorizontalAlignment.Center
        Else
            'txtSecondTitle.Text = ""
            txtResult2.Text = ""
            'txtSecondTitle.ForeColor = Color.Black
            'txtSecondTitle.Font = New System.Drawing.Font("Zawgyi-One", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'txtSecondTitle.Text = ""
            '_SecondFontColor = "Black"
        End If
    End Sub


    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged
        If txtAddress.Text <> "" Then

            txtResult3.Text = txtAddress.Text
            txtResult3.Font = txtAddress.Font
            txtResult3.ForeColor = txtAddress.ForeColor
            txtResult3.TextAlign = HorizontalAlignment.Left
        Else
            ' txtAddress.Text = ""
            txtResult3.Text = ""
            'txtAddress.ForeColor = Color.Black
            'txtAddress.Font = New System.Drawing.Font("Zawgyi-One", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'txtAddress.Text = ""
            '_ThirdFontColor = "Black"
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        If btnAdd.Text = "Add" Then
            If Global_PhotoPath <> "" Then

                OpenFileDia.Filter = "Image (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png;"
                OpenFileDia.FileName = Global_PhotoPath + "\"
                OpenFileDia.InitialDirectory = OpenFileDia.FileName
                OpenFileDia.ShowDialog()
                If OpenFileDia.FileName <> "" Then
                    If OpenFileDia.InitialDirectory = OpenFileDia.FileName Then
                        lblItemImage.Image = Nothing
                        btnAdd.Text = "Add"
                        lblPhoto.Visible = True
                        PName = ""
                        Exit Sub
                    End If
                    lblItemImage.Image = System.Drawing.Image.FromFile(OpenFileDia.FileName)
                    PName = OpenFileDia.FileName.Substring(Global_PhotoPath.Length + 1)
                    btnAdd.Text = "Remove"

                End If
                lblPhoto.Visible = False
            End If
        Else
            lblItemImage.Image = Nothing
            btnAdd.Text = "Add"
            lblPhoto.Visible = True
            PName = ""
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmPrint As New frm_ReportViewer
        Dim dt As New DataTable
        dt = objSalesInvoiceController.GetSalesInvoicePrint("")
        frmPrint.PrintPreview(dt)
        frmPrint.WindowState = FormWindowState.Maximized
        frmPrint.Show()
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("Vouchersetting")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class
