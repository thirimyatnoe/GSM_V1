Imports CommonInfo
Imports BusinessRule
Imports Operational.AppConfiguration
Public Class frm_GenerateFomat
    Implements IFormProcess

    Private _GenerateFormatController As BusinessRule.GenerateFormat.IGenerateFormatController = Factory.Instance.CreateGenerateFormatController
    Dim GenerateFormatID As Integer = 0
    Dim OrgPrefix As System.Drawing.Point
    Dim OrgGroup As System.Drawing.Point


    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If GenerateFormatID = 0 Then
            MsgBox("Please select you want to delete data", MsgBoxStyle.Information, AppName)
            Exit Function

        Else
            'If MsgBox("Are you sure to delete?", MsgBoxStyle.YesNo, AppName) = MsgBoxResult.Yes Then
            If _GenerateFormatController.DeleteGenerateFormat(GenerateFormatID) Then
                MsgBox("Delete Successfully", MsgBoxStyle.Information, AppName)
                Clear()
            End If
            'End If
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() = False Then Exit Function
        If _GenerateFormatController.Save_GenerateFormat(GetData()) Then

            MsgBox("Save Successfully", MsgBoxStyle.Information, AppName)
            cboDateFormat1.DataSource = _GenerateFormatController.GetDateFormatData().DefaultView()

            cboDateFormat2.DataSource = _GenerateFormatController.GetSecondDateFormatData().DefaultView()
            Clear()
        End If
    End Function

    Private Sub frm_GenerateFomat_Load(sender As Object, e As EventArgs) Handles Me.Load
        OrgPrefix = optFirst.Location
        OrgGroup = GpFormat.Location
        cboTableType.Text = ""
        'GetCombo()
        Clear()
        'MyBase._Heading.Text = "Generate Format"
        'optFirst.Visible = False
        'optLast.Visible = False
        'optNot.Visible = False
        'optFirst.Checked = False
        'optLast.Checked = False
        'optNot.Checked = False

        'GpFormat.Location = OrgPrefix
    End Sub

#Region " Methods"
    Private Sub Clear()
        txtPrefix.Text = ""
        txtPrefix2.Text = ""
        cboNumberCount.Text = ""
        cboDateFormat1.Text = ""
        cboDateFormat2.Text = ""
        cboDateFormat1.SelectedIndex = -1
        cboDateFormat2.SelectedIndex = -1
        txtResult.Text = ""
        txtGenerateFormat.Text = ""
        cboTableType.SelectedIndex = 0

        optFirst.Visible = False
        optLast.Visible = False
        optNot.Visible = False
        optFirst.Checked = False
        optLast.Checked = False
        optNot.Checked = False
        chkIsGenerate.Checked = False
        GenerateFormatID = 0

    End Sub
    Private Function GetData() As CommonInfo.GenerateFormatInfo
        Dim objGenerateFormat As New CommonInfo.GenerateFormatInfo
        With objGenerateFormat
            .GenerateFormatID = GenerateFormatID

            .GenerateFormat = cboTableType.Text
            .Prefix = CStr(txtPrefix.Text)
            .Prefix2 = CStr(txtPrefix2.Text)
            .FormatDate1 = CStr(cboDateFormat1.Text)
            .FormatDate2 = CStr(cboDateFormat2.Text)
            .NumberCount = CStr(cboNumberCount.Text)

            If cboTableType.Text = EnumSetting.TableType.Barcode.ToString() Or cboTableType.Text = EnumSetting.TableType.DiamondBarcode.ToString() Then
                If optFirst.Checked Then
                    .PrefixPlace = EnumSetting.PrefixPlace.First.ToString()
                ElseIf optLast.Checked Then
                    .PrefixPlace = EnumSetting.PrefixPlace.Last.ToString()
                Else
                    .PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString()
                End If
                .IsGenerate = chkIsGenerate.Checked
            Else
                .PrefixPlace = ""
                .IsGenerate = False
            End If


        End With
        Return objGenerateFormat

    End Function
    Private Function IsFillData() As Boolean

        If cboTableType.Text = "" Then
            MsgBox("Please Fill Generate Format", MsgBoxStyle.Information, AppName)
            IsFillData = False
            Exit Function
        Else
            IsFillData = True
        End If


        Return IsFillData
    End Function

    'Public Sub GetCombo()

    '    cboTableType.DisplayMember = "TableType"
    '    cboTableType.ValueMember = "TableType"
    '    cboTableType.DataSource = {"PurchaseRowMaterialItem", "SalesGem", "SalesInvoice", "SalesInvoice_Volume", "OrderInvoice", "Barcode"}
    '    cboTableType.SelectedValue = -1
    'End Sub

    Private Sub ShowResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrefix.TextChanged, txtPrefix2.TextChanged, cboDateFormat1.TextChanged, cboDateFormat2.TextChanged, cboNumberCount.TextChanged
        Dim Dateformat1 As String = ""
        Dim Dateformat2 As String = ""

        If cboDateFormat1.Text <> "" Then
            Try
                Dateformat1 = Format(System.DateTime.Now.Date, cboDateFormat1.Text)
            Catch ex As Exception
                Dateformat1 = ""
            End Try
        End If
        If cboDateFormat2.Text <> "" Then
            Try
                Dateformat2 = Format(System.DateTime.Now.Date, cboDateFormat2.Text)
            Catch ex As Exception
                Dateformat2 = ""
            End Try
        End If

        txtResult.Text = txtPrefix.Text.Trim & IIf(Dateformat1 <> "", Dateformat1, "") & IIf(Dateformat2 <> "", Dateformat2, "") & txtPrefix2.Text.Trim & cboNumberCount.Text.Trim
    End Sub

    Private Sub ShowData(ByVal GenerateFormatObj As CommonInfo.GenerateFormatInfo)
        With GenerateFormatObj

            txtGenerateFormat.Text = .GenerateFormat
            txtPrefix.Text = .Prefix
            txtPrefix2.Text = .Prefix2
            cboDateFormat1.Text = .FormatDate1
            cboDateFormat2.Text = .FormatDate2
            cboNumberCount.Text = .NumberCount
        End With
    End Sub

#End Region

#Region "Button Click"
    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim DataItem As DataRow
        Dim dtGenerate As New DataTable
        Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
        dtGenerate = _GenerateFormatController.GetGenerateFormatList()
        DataItem = DirectCast(SearchData.FindFast(dtGenerate, "GenerateFormat List"), DataRow)
        If DataItem IsNot Nothing Then
            GenerateFormatID = DataItem.Item("GenerateFormatID")
            objGenerateFormat = _GenerateFormatController.GetGenerateFormat(GenerateFormatID)
            ShowData(objGenerateFormat)

        End If
    End Sub
#End Region


    Private Sub cboTableType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTableType.SelectedValueChanged
        If cboTableType.Text = EnumSetting.TableType.Barcode.ToString() Then
            optFirst.Visible = True
            optFirst.Text = "ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကိုရှေ့ဆုံးတွင်ထားပါ။"
            optLast.Visible = True
            optLast.Text = "ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကိုနောက်ဆုံးတွင်ထားပါ။"
            optNot.Visible = True
            optNot.Text = "ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကို Barcode တွင်မထည့်ပါ။"
            txtPrefix.Enabled = False
            lblPrefix.Enabled = False
            optFirst.Location = OrgPrefix
            GpFormat.Location = OrgGroup
            chkIsGenerate.Visible = True
        ElseIf cboTableType.Text = EnumSetting.TableType.DiamondBarcode.ToString() Then
            optFirst.Visible = True
            optFirst.Text = "စိန်ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကိုရှေ့ဆုံးတွင်ထားပါ။"
            optLast.Visible = True
            optLast.Text = "စိန်ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကိုနောက်ဆုံးတွင်ထားပါ။"
            optNot.Visible = True
            optNot.Text = "စိန်ပစ္စည်းအမျိုးအစား၏အတိုကောက် (prefix) ကို Barcode တွင်မထည့်ပါ။"
            txtPrefix.Enabled = False
            lblPrefix.Enabled = False
            optFirst.Location = OrgPrefix
            GpFormat.Location = OrgGroup
            chkIsGenerate.Visible = True
        ElseIf cboTableType.Text = ".....Selected....." Then
            optFirst.Visible = False
            optLast.Visible = False
            optNot.Visible = False
            optFirst.Checked = False
            optLast.Checked = False
            optNot.Checked = False
            txtPrefix.Enabled = True
            lblPrefix.Enabled = True
            GpFormat.Location = OrgPrefix
            chkIsGenerate.Visible = False
            chkIsGenerate.Checked = False
        Else
            optFirst.Visible = False
            optLast.Visible = False
            optNot.Visible = False
            optFirst.Checked = False
            optLast.Checked = False
            optNot.Checked = False
            txtPrefix.Enabled = True
            lblPrefix.Enabled = True
            GpFormat.Location = OrgPrefix
            chkIsGenerate.Visible = False
            chkIsGenerate.Checked = False
        End If

        If cboTableType.Text IsNot Nothing Then
            Dim objGF As New CommonInfo.GenerateFormatInfo
            objGF = _GenerateFormatController.GetGenerateFormatByFormat(cboTableType.Text)
            If objGF.GenerateFormat IsNot Nothing Then
                With objGF
                    GenerateFormatID = .GenerateFormatID

                    If .PrefixPlace = CommonInfo.EnumSetting.PrefixPlace.First.ToString() Then
                        optFirst.Checked = True
                    ElseIf .PrefixPlace = CommonInfo.EnumSetting.PrefixPlace.Last.ToString() Then
                        optLast.Checked = True
                    ElseIf .PrefixPlace = CommonInfo.EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                        optNot.Checked = True

                    End If
                    txtPrefix.Text = .Prefix
                    txtPrefix2.Text = .Prefix2
                    txtPrefix2.Text = .Prefix2
                    cboDateFormat1.Text = .FormatDate1
                    cboDateFormat2.Text = .FormatDate2
                    cboNumberCount.Text = .NumberCount
                    chkIsGenerate.Checked = .IsGenerate
                    'cboTableType.SelectedValue = .GenerateFormat
                End With
            Else
                txtPrefix.Text = ""
                txtPrefix2.Text = ""
                cboNumberCount.Text = ""
                cboDateFormat1.Text = ""
                cboDateFormat2.Text = ""
                cboDateFormat1.SelectedIndex = -1
                cboDateFormat2.SelectedIndex = -1
                txtResult.Text = ""
                GenerateFormatID = 0
                chkIsGenerate.Checked = False
            End If
        End If
    End Sub

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("GenerateFormat")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class
