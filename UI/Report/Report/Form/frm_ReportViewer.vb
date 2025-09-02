Imports Microsoft.Reporting.WinForms
Imports BusinessRule
Imports System.Configuration
Imports Operational
Imports CommonInfo
Imports System.IO

Public Class frm_ReportViewer
    Dim dt As DataTable
    Private Shared config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

    'Private _ReportDA As SalesInvoice.ISalesInvoiceController = Factory.Instance.CreateSaleInvoiceController
    Private _LocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _GoldSmithController As GoldSmith.IGoldSmithController = Factory.Instance.CreateGoldSmithController
    Private _VoucherSettingCon As VoucherSetting.IVoucherSettingController = Factory.Instance.CreateVoucherSettingController
    Private objSalesInvoiceController As SalesItemInvoice.ISalesItemInvoiceController = Factory.Instance.CreateSaleItemInvoiceController
    Private _SettingController As Setting.ISettingController = Factory.Instance.CreateSettingController


    Public Sub frm_SaleInvoice_Print1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub Print15(ByVal dt As DataTable)
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleInvoice_Print15.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))

        'Update For Print Remark SakyarShweYee (10.10.12)
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter


        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
   

    Public Sub PrintPL(ByVal dt As DataTable)
        Dim report As LocalReport = New LocalReport()
        With report
            rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleInvoicePrintPT.rdlc"
            rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
            rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))
            rpt_SaleInvoice_Print1.RefreshReport()
        End With
    End Sub

    Public Sub PrintD(ByVal dt As DataTable, ByVal dtDetail As DataTable, ByVal dtPurchase As DataTable, ByVal dtOtherCash As DataTable, ByVal dtVoucherTax As DataTable, ByVal IsPhoto As Boolean)
        Dim PhotoPath As String = ""
        Dim StrBarcode As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String = ""
        Dim _SaleInvoiceDetailID As String = ""
        Dim _TotalGemTG As Decimal = 0.0
  
            If IsPhoto = True Then
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleInvoice_Print15WithPhoto.rdl") Then
                    rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleInvoice_Print15WithPhoto.rdl"
                Else
                    rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleInvoice_Print15.rdl"
                End If
            Else
                rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleInvoice_Print15.rdl"
            End If


        For i As Integer = 0 To dt.Rows.Count - 1
            If Not IsDBNull(dt.Rows(i).Item("SalesInvoiceGemItemID")) Then
                If dt.Rows(i).Item("SaleInvoiceDetailID") = _SaleInvoiceDetailID Then
                    dt.Rows(i).Item("ItemK") = 0
                    dt.Rows(i).Item("ItemP") = 0
                    dt.Rows(i).Item("ItemY") = 0
                    dt.Rows(i).Item("ItemTK") = 0
                    dt.Rows(i).Item("ItemTG") = 0
                    dt.Rows(i).Item("WasteK") = 0
                    dt.Rows(i).Item("WasteP") = 0
                    dt.Rows(i).Item("WasteY") = 0
                    dt.Rows(i).Item("WasteTK") = 0
                    dt.Rows(i).Item("WasteTG") = 0
                    dt.Rows(i).Item("TotalCharges") = 0
                    dt.Rows(i).Item("ItemNetAmount") = 0
                    dt.Rows(i).Item("GoldPrice") = 0
                    dt.Rows(i).Item("GemsPrice") = 0
                End If

            End If
            _SaleInvoiceDetailID = dt.Rows(i).Item("SaleInvoiceDetailID")
        Next

        For i As Integer = 0 To dt.Rows.Count - 1
            If Not IsDBNull(dt.Rows(i).Item("SalesInvoiceGemItemID")) Then
                _TotalGemTG += dt.Rows(i).Item("GemsTG")
            End If
        Next

        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_dtSaleInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_dtPurchaseIsChange", dtPurchase))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOtherCash_dtOtherCash", dtOtherCash))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_dtVoucherTax", dtVoucherTax))

        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim ItemTG As Decimal = 0
        Dim QTY As Decimal = 0
        If dtDetail.Rows.Count > 0 Then
            For Each dr As DataRow In dtDetail.Rows
                ItemTG += dr.Item("ItemTG")
                QTY += 1
            Next
        End If

        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", "Total Quantity :      " & QTY & "      Total Weight :      " & Format(ItemTG, "0.000") & " grs")
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As DataTable
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter
        Dim GoldSmith(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String
        Dim TotalGemTG(0) As ReportParameter

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        If dt.Rows.Count > 0 Then
            'GoldSmith(0) = New ReportParameter("GoldSmith", "")
            'rpt_SaleInvoice_Print1.LocalReport.SetParameters(GoldSmith)
            'Else
            For Each dr As DataRow In dt.Rows
                GoldSmith(0) = New ReportParameter("GoldSmith", _GoldSmithController.GetGoldSmithByID(dr.Item("goldsmithID")).Name)
                rpt_SaleInvoice_Print1.LocalReport.SetParameters(GoldSmith)
            Next

        End If

        TotalGemTG(0) = New ReportParameter("TotalGemTG", _TotalGemTG)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(TotalGemTG)

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        Dim G_PToY(0) As ReportParameter

        If File.Exists(My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleInvoice_Print15WithPhoto.rdl") Then
            G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
            rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)

        End If


        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub SaleItemNamePrint(ByVal dt As DataTable, ByVal dtDetail As DataTable, ByVal dtPurchase As DataTable, ByVal dtOtherCash As DataTable, ByVal dtVoucherTax As DataTable, ByVal IsShowPhoto As Boolean)
        Dim PhotoPath As String = ""
        Dim StrBarcode As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String = ""

        If IsShowPhoto = True Then
            rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SalePrintByItemNameWithPhoto.rdl"
        Else
            rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SalePrintByItemName.rdl"
        End If

        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_dtSaleInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoioce_PurchaseInvoice", dtPurchase))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOtherCash_dtOtherCash", dtOtherCash))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_dtVoucherTax", dtVoucherTax))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim ItemTG As Decimal = 0
        Dim QTY As Decimal = 0
        If dtDetail.Rows.Count > 0 Then
            For Each dr As DataRow In dtDetail.Rows
                ItemTG += dr.Item("ItemTG")
                QTY += 1
            Next
        End If


        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", "Total Quantity :      " & QTY & "      Total Weight :      " & Format(ItemTG, "0.000") & " grs")
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As DataTable
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        Dim G_PToY(0) As ReportParameter

        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()

    End Sub

    Public Sub PrintSaleOrder15(ByVal dt As DataTable)
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleOrder_Print15.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSalesOrderInvoice_dtSalesOrderInvoice", dt))
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintSaleOrderDone(ByVal dt As DataTable)
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleOrder_PrintDone.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSalesOrderInvoice_dtSalesOrderInvoice", dt))
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintOrder15(ByVal dt As DataTable, ByVal dtDetail As DataTable, ByVal dtVoucherTax As DataTable)
        Dim PhotoPath As String = ""
        Dim StrBarcode As String = ""
        Dim TotalGoldTK As Decimal = 0.0
        Dim TotalGoldTG As Decimal = 0.0
        Dim AddGoldTK As Decimal = 0.0
        Dim AddGoldTG As Decimal = 0.0

        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_OrderInvoice_Print15.rdl"


        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_dtOrderReturn", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_OrderInvoice", dtDetail))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_dtVoucherTax", dtVoucherTax))

        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True
        Dim PhotoOne(0) As ReportParameter
        PhotoPath = Global_PhotoPath & "\"
        PhotoOne(0) = New ReportParameter("PhotoOne", PhotoPath)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(PhotoOne)
        rpt_SaleInvoice_Print1.RefreshReport()

        If dtDetail.Rows.Count > 0 Then
            For Each dr As DataRow In dtDetail.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("ItemCode") & " , " & dr.Item("ItemName") & " , " & Math.Round(dr.Item("TotalTG"), 3) & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & " , " & dr.Item("SalesRate") & " (" & dr.Item("GoldQuality") & ")  , " & dr.Item("ItemNetAmount") & "  /  "
                TotalGoldTK += dr.Item("GoldTK") + dr.Item("WasteTK")
                TotalGoldTG += Math.Round(dr.Item("GoldTG"), 3) + Math.Round(dr.Item("WasteTG"), 3)
            Next

            AddGoldTK = (TotalGoldTK - dtDetail.Rows(0).Item("PayGoldTK"))
            AddGoldTG = (TotalGoldTG - Math.Round(dtDetail.Rows(0).Item("PayGoldTG"), 3))
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        Dim GoldTK(0) As ReportParameter
        Dim GoldTG(0) As ReportParameter
        Dim AddSubGoldTK(0) As ReportParameter
        Dim AddSubGoldTG(0) As ReportParameter

        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        GoldTK(0) = New ReportParameter("GoldTK", TotalGoldTK)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(GoldTK)

        GoldTG(0) = New ReportParameter("GoldTG", TotalGoldTG)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(GoldTG)

        AddSubGoldTK(0) = New ReportParameter("AddSubGoldTK", AddGoldTK)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(AddSubGoldTK)

        AddSubGoldTG(0) = New ReportParameter("AddSubGoldTG", AddGoldTG)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(AddSubGoldTG)

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub OrderReturnItemName(ByVal dt As DataTable, ByVal dtDetail As DataTable, ByVal dtVoucherTax As DataTable)
        Dim PhotoPath As String = ""
        Dim StrBarcode As String = ""
        Dim TotalGoldTK As Decimal = 0.0
        Dim TotalGoldTG As Decimal = 0.0
        Dim AddGoldTK As Decimal = 0.0
        Dim AddGoldTG As Decimal = 0.0

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderReturnByItemName.rdlc"

        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_OrderReturnByItemName.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_dtOrderReturn", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_OrderInvoice", dtDetail))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceDataSet_dtVoucherTax", dtVoucherTax))

        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim PhotoOne(0) As ReportParameter
        PhotoPath = Global_PhotoPath & "\"
        PhotoOne(0) = New ReportParameter("PhotoOne", PhotoPath)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(PhotoOne)

        rpt_SaleInvoice_Print1.RefreshReport()
        If dtDetail.Rows.Count > 0 Then
            For Each dr As DataRow In dtDetail.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("ItemCode") & " , " & dr.Item("ItemName") & " , " & Math.Round(dr.Item("TotalTG"), 3) & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & " , " & dr.Item("SalesRate") & " (" & dr.Item("GoldQuality") & ")  , " & dr.Item("ItemNetAmount") & "  /  "
                TotalGoldTK += dr.Item("GoldTK") + dr.Item("WasteTK")
                TotalGoldTG += Math.Round(dr.Item("GoldTG"), 3) + Math.Round(dr.Item("WasteTG"), 3)
            Next

            AddGoldTK = (TotalGoldTK - dtDetail.Rows(0).Item("PayGoldTK"))
            AddGoldTG = (TotalGoldTG - Math.Round(dtDetail.Rows(0).Item("PayGoldTG"), 3))
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        Dim GoldTK(0) As ReportParameter
        Dim GoldTG(0) As ReportParameter
        Dim AddSubGoldTK(0) As ReportParameter
        Dim AddSubGoldTG(0) As ReportParameter

        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        GoldTK(0) = New ReportParameter("GoldTK", TotalGoldTK)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(GoldTK)

        GoldTG(0) = New ReportParameter("GoldTG", TotalGoldTG)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(GoldTG)

        AddSubGoldTK(0) = New ReportParameter("AddSubGoldTK", AddGoldTK)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(AddSubGoldTK)

        AddSubGoldTG(0) = New ReportParameter("AddSubGoldTG", AddGoldTG)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(AddSubGoldTG)

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        Dim G_PToY(0) As ReportParameter

        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintOrder(ByVal dt As DataTable)
        Dim StrBarcode As String = ""
        Dim PhotoPath As String = ""

        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap

        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String = ""

        ReportName = config.AppSettings.Settings("OrderA4ReportName").Value()

        If ReportName <> "" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & ReportName
        Else
            filepath = My.Application.Info.DirectoryPath & "\Report\RDLC\ rpt_OrderInvoice_Print.rdl"
        End If


        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderInvoice_Print.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderInvoice_Print.rdl"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_OrderInvoice_Print.rdl"

        rpt_SaleInvoice_Print1.LocalReport.ReportPath = filepath
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceItem_OrderInvoiceItem", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True
        ' rpt_SaleInvoice_Print1.RefreshReport()

        ''If dt.Rows.Count > 0 Then
        ''    For Each dr As DataRow In dt.Rows
        ''        '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
        ''        StrBarcode &= " " & dr.Item("ItemName") & " / " & dr.Item("QTY") & " / " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & " / " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & " / " & dr.Item("OrderRate") & " / " & dr.Item("ItemNetAmount") & "  ,  "
        ''    Next
        ''    If StrBarcode.Length > 0 Then
        ''        StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
        ''    End If
        ''Else
        ''    StrBarcode = ""
        ''End If
        ''Dim StrItemCode(0) As ReportParameter
        ''StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        ''rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub


    Public Sub PrintOrderDone(ByVal dt As DataTable)
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderInvoice_PrintDone.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoice_OrderInvoice", dt))
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintGeneralLedgerByLocation(ByVal dt As DataTable, ByVal title As String, ByVal dtOtherCash As DataTable)
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_GeneralLedgerByLocation.rdlc"
        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_GeneralLedgerByLocation.rdl"
        'rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_GeneralLedgerByLocation.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsGeneralLedgerByLocation_dtGeneralLedgerByLocation", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOtherCash_dtOtherCash", dtOtherCash))
        Dim ShowOtherCash(0) As ReportParameter
        Dim IsShow As Boolean
        If dtOtherCash.Rows.Count > 0 Then
            IsShow = True
        Else
            IsShow = False
        End If
        ShowOtherCash(0) = New ReportParameter("ShowOtherCash", IsShow)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(ShowOtherCash)

        Dim CTitle(0) As ReportParameter
        CTitle(0) = New ReportParameter("CTitle", title)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(CTitle)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintMortgageInvoice(ByVal dt As DataTable)
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

        If File.Exists(My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgageInvoice_Voucher.rdl") Then
            rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgageInvoice_Voucher.rdl"
        End If

        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_Mortgage", dt))
  

        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        'Dim Global_InterestRate As ReportParameter
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "-"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If



        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintMortgageInterestInvoice(ByVal dt As DataTable, ByVal dtMortgage As DataTable)
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

        If File.Exists(My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgageInterest_Voucher.rdl") Then
            rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgageInterest_Voucher.rdl"
        End If

        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsMortgageInvoice_MortgageInterest", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_Mortgage", dtMortgage))
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintMortgagePaybackInvoice(ByVal dt As DataTable, ByVal dtMortgage As DataTable)
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

        If File.Exists(My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgagePayback_Voucher.rdl") Then
            rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgagePayback_Voucher.rdl"
        End If

        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_MortgagePayback", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_Mortgage", dtMortgage))
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintMortgageReturn(ByVal dt As DataTable, ByVal dtHistory As DataTable)
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

        If File.Exists(My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgageReturn_Voucher.rdl") Then
            rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_MortgageReturn_Voucher.rdl"
        End If

        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsMortgage_MortgageReturn", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsHistory_MortgageHistory", dtHistory))
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintWholeSale(ByVal dt As DataTable)

        Dim StrBarcode As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String = ""
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_WholeSalePrint.rdl"


        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSaleInvoice_dtWholeSaleInvoice", dt))
       
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim ItemTG As Decimal = 0
        Dim QTY As Decimal = 0
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ItemTG += dr.Item("ItemTG")
                QTY += 1
            Next
        End If


        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", "Total Quantity :      " & QTY & "      Total Weight :      " & Format(ItemTG, "0.000") & " grs")
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As DataTable
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()


    End Sub



    Public Sub PrintPurchaseInvoice(ByVal dt As DataTable, ByVal dtDetail As DataTable)
        Dim PhotoPath As String = ""
        Dim Photo(0) As ReportParameter
        Dim StrBarcode As String = ""

        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap

        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String = ""
        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseInvoice_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_PurchaseInvoice_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_dtPurchaseItem", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        'PhotoPath = Global_PhotoPath & "\"
        'Photo(0) = New ReportParameter("Photo", PhotoPath)
        'rpt_SaleInvoice_Print1.LocalReport.SetParameters(Photo)

        'If dtDetail.Rows.Count > 0 Then
        '    For Each dr As DataRow In dtDetail.Rows
        '        StrBarcode &= IIf(dr.Item("BarcodeNo") = "", "", dr.Item("BarcodeNo")) & IIf(dr.Item("BarcodeNo") = "", "", " , ") & dr.Item("ItemName") & " , " & dr.Item("QTY") & " , " & Math.Round(dr.Item("TotalTG"), 3) & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & " , " & IIf(Len(dr.Item("StrCurrentPrice")) < 3, dr.Item("CurrentPrice") & "%", dr.Item("CurrentPrice")) & IIf(dr.Item("ForSaleFixPrice") = False, "", dr.Item("OldSaleAmount")) & " (" & dr.Item("GoldQuality") & ")  ,  " & IIf(dr.Item("PurchaseWastePercent") > 0, dr.Item("PurchaseDiscountAmount"), "") & IIf(dr.Item("PurchaseWastePercent") > 0, " (၀ယ်လျော့)  ,   ", "") & dr.Item("ItemTotalAmount") & "  /  "
        '    Next
        '    If StrBarcode.Length > 0 Then
        '        StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
        '    End If
        'Else
        '    StrBarcode = ""
        'End If

        'Dim StrItemCode(0) As ReportParameter
        'StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        'rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintPurchaseInvoiceforOnlyGem(ByVal dt As DataTable)
        Dim StrBarcode As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap

        'FileMap = New ExeConfigurationFileMap
        'FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        'config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        'Dim ReportName As String = ""

        'ReportName = config.AppSettings.Settings("PurchaseGemA4ReportName").Value()

        'If ReportName <> "" Then
        '    filepath = My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & ReportName
        'Else
        '    filepath = My.Application.Info.DirectoryPath & "\Report\RDLC\ rpt_PurchaseGems_Print.rdl"
        'End If

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdl"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_PurchaseGems_Print.rdl"

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_PurchaseGems_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("ItemCategory") & " / " & dr.Item("GemsName") & " / " & dr.Item("QTY") & " / " & dr.Item("YOrCOrG") & " / " & Math.Round(dr.Item("TotalGemTG"), 3) & "(g)" & " / " & dr.Item("GemsK") & "-" & dr.Item("GemsP") & "-" & Math.Round(dr.Item("GemsY"), 1) & " / " & dr.Item("CurrentPrice") & " / " & dr.Item("ItemTotalAmount") & "  ,  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)

                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintPurchaseInvoiceforLooseDiamond(ByVal dt As DataTable)
        Dim StrBarcode As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap

        'FileMap = New ExeConfigurationFileMap
        'FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        'config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        'Dim ReportName As String = ""

        'ReportName = config.AppSettings.Settings("PurchaseGemA4ReportName").Value()

        'If ReportName <> "" Then
        '    filepath = My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & ReportName
        'Else
        '    filepath = My.Application.Info.DirectoryPath & "\Report\RDLC\ rpt_PurchaseGems_Print.rdl"
        'End If

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdl"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_PurchaseGems_Print.rdl"

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_PurchaseLooseDiamond_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("ItemCategory") & " / " & dr.Item("GemsName") & " / " & dr.Item("QTY") & " / " & dr.Item("YOrCOrG") & " / " & Math.Round(dr.Item("TotalGemTG"), 3) & "(g)" & " / " & dr.Item("GemsK") & "-" & dr.Item("GemsP") & "-" & Math.Round(dr.Item("GemsY"), 1) & " / " & dr.Item("CurrentPrice") & " / " & dr.Item("ItemTotalAmount") & "  ,  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)

                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintWholeSaleReturn(ByVal dt As DataTable)


        Dim StrBarcode As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String = ""
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_WholeSaleReturnPrint.rdl"


        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsWholeSaleReturn_WholeSaleReturn", dt))

        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True


        Dim ItemTG As Decimal = 0
        Dim QTY As Decimal = 0
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ItemTG += dr.Item("ItemTG")
                QTY += 1
            Next
        End If


        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", "Total Quantity :      " & QTY & "      Total Weight :      " & Format(ItemTG, "0.000") & " grs")
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As DataTable
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()

    End Sub
    Public Sub PrintConsignmentSaleWG(ByVal dt As DataTable, ByVal dtPurchase As DataTable)

        Dim StrBarcode As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String = ""
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_ConsignmentSaleForWGVou.rdl"


        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsConsignmentSale_ConsignmentSale", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_dtPurchaseIsChange", dtPurchase))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True


        Dim ItemTG As Decimal = 0
        Dim QTY As Decimal = 0
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ItemTG += dr.Item("ItemTG")
                QTY += 1
            Next
        End If


        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", "Total Quantity :      " & QTY & "      Total Weight :      " & Format(ItemTG, "0.000") & " grs")
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As DataTable
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If


        Dim Address(0) As ReportParameter

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()

    End Sub



    Public Sub PrintSaleGems(ByVal dt As DataTable, ByVal dtPurchase As DataTable, ByVal dtOtherCash As DataTable)
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim StrBarcode As String = ""

        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleGems_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleGems_SaleGems", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchase_dtPurchase", dtPurchase))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOtherCash_dtOtherCash", dtOtherCash))

        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                StrBarcode &= " " & dr.Item("GemsCategory") & " / " & dr.Item("GemsName") & " / " & dr.Item("Qty") & " / " & dr.Item("YOrCOrG") & " / " & Math.Round(dr.Item("GemsTG"), 3) & "(g)" & " / " & dr.Item("GemsK") & "-" & dr.Item("GemsP") & "-" & Math.Round(dr.Item("GemsY"), 1) & " / " & dr.Item("SaleRate") & " / " & dr.Item("Amount") & "  ,  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If

        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintReturnAdvance(ByVal dt As DataTable)
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim StrBarcode As String = ""

        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_ReturnAdvance_Print .rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsReturnAdvance_ReturnAdvance", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                StrBarcode &= " " & " / " & dr.Item("SaleRate") & " / " & dr.Item("Amount") & "  ,  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If

        'Dim StrItemCode(0) As ReportParameter
        'StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        'rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        'Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        'rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        'RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        'rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintPurchaseGems(ByVal dt As DataTable)
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter

        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseGems_dsPurchaseGems", dt))

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintSaleVolume(ByVal dt As DataTable, ByVal dtPurchase As DataTable)
        Dim PhotoPath As String = ""
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim StrBarcode As String = ""
        'Dim filepath As String = ""
        'Dim FileMap As ExeConfigurationFileMap


        'FileMap = New ExeConfigurationFileMap
        'FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        'config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        'Dim ReportName As String = ""
        'ReportName = config.AppSettings.Settings("SaleVolumeA4ReportName").Value()
        'If ReportName <> "" Then
        '    filepath = My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & ReportName
        'Else
        '    filepath = My.Application.Info.DirectoryPath & "\Report\RDLC\ rpt_SaleVolumeInvoice_Print.rdl"
        'End If

        ''rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleVolumeInvoice_Print.rdl"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleVolumeInvoice_Print.rdl"
        'rpt_SaleInvoice_Print1.LocalReport.ReportPath = filepath

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleVolumeInvoice_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleVolumeInvoice_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSalesVolumeInvoice_SalesVolumeInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_dtPurchaseIsChange", dtPurchase))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True
        'rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True
        ' rpt_SaleInvoice_Print1.RefreshReport()

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("ItemCode") & " , " & dr.Item("ItemName") & " , " & dr.Item("QTY") & " , " & Math.Round(dr.Item("TotalTG"), 3) & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & " , " & dr.Item("SalesRate") & " (" & dr.Item("GoldQuality") & ") , " & dr.Item("ItemNetAmount") & "  /  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintPreview(ByVal dt As DataTable)
        Dim PhotoPath As String = ""
        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PrintPreview.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PrintPreview.rdl"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_PrintPreview.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True


        Dim dtItem As New DataTable
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Dim PhotoOne(0) As ReportParameter
        PhotoPath = Global_PhotoPath + "\"
        PhotoOne(0) = New ReportParameter("PhotoOne", PhotoPath)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(PhotoOne)

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintStandardRate(ByVal dt As DataTable)
        Dim report As LocalReport = New LocalReport()
        With report
            '  rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_CurrentPrice.rdlc"
            rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_CurrentPrice.rdl"
            rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_CurrentPrice.rdl"
            rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
            rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsCurrentPrice_CurrentPrice", dt))
            rpt_SaleInvoice_Print1.RefreshReport()
        End With
    End Sub
    Public Sub PrintCashReceipt(ByVal dt As DataTable)
        Dim PhotoPath As String = ""
        Dim filepath As String = ""
        Dim FileMap As ExeConfigurationFileMap
        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_CashReceipt_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("CashReceipt", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim dtItem As DataTable
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)


        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()

    End Sub

    Public Sub PrintSale(ByVal dt As DataTable, ByVal dtDetail As DataTable)
        Dim PhotoPath As String = ""
        Dim StrBarcode As String = ""
        Dim StrNo As String = ""
        Dim i As Integer = 0

        ''Dim printtype As String = ""
        ''Dim tempfpath As String = ""
        ''Dim filepath As String = ""
        ''Dim tmpstring As String()

        ''If GetSetting("PrinterType") Then
        ''    printtype = "A4"
        ''Else
        ''    printtype = "bydmx"
        ''End If

        ''Dim FileMap As ExeConfigurationFileMap

        ''FileMap = New ExeConfigurationFileMap
        ''FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        ''config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        ''Dim ReportName As String = ""

        ''If printtype = "A4" Then
        ''    ReportName = config.AppSettings.Settings("A4ReportName").Value()

        ''ElseIf printtype = "A4V" Then
        ''    ReportName = config.AppSettings.Settings("A4VReportName").Value()

        ''ElseIf printtype = "bydmx" Then
        ''    ReportName = config.AppSettings.Settings("DMReportName").Value()

        ''End If

        ''tmpstring = GetRDLCFileName(printtype, "transaction")
        ''For Each tempfpath In tmpstring
        ''    If tempfpath = ReportName Then
        ''        filepath = tempfpath
        ''    End If
        ''Next

        ''If printtype = "A4" Then

        ''    filepath = My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & filepath
        ''Else
        ''    filepath = My.Application.Info.DirectoryPath & "\Report\RDLC\ rpt_SaleInvoice_Print.rdl"
        ''End If


        If dtDetail.Rows.Count() = 0 Then
            MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
            Exit Sub
        Else
            For Each dr As DataRow In dtDetail.Rows
                dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
            Next
        End If

        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleInvoice_Print.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleInvoice_Print15.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dtDetail))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim PhotoOne(0) As ReportParameter
        PhotoPath = Global_PhotoPath & "\"
        PhotoOne(0) = New ReportParameter("PhotoOne", PhotoPath)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(PhotoOne)


        If dtDetail.Rows.Count > 0 Then
            If dtDetail.Rows.Count > 0 Then
                For Each dr As DataRow In dtDetail.Rows
                    StrBarcode &= " " & dr.Item("ItemCode") & Chr(13) & Chr(10)
                    i += 1
                    StrNo &= i & Chr(13) & Chr(10)
                Next
                'If StrBarcode.Length > 0 Then
                '    StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 1)
                'End If
            End If
        Else
            StrBarcode = ""
        End If
        ''rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleInvoice_Print.rdlc"

        ''rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        ''rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))

        Dim StrItemCode(0) As ReportParameter
        Dim StrNumber(0) As ReportParameter
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        StrNumber(0) = New ReportParameter("StrNumber", StrNo)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrNumber)

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Private Function Getsetting(ByVal KeyName As String)
        Dim objsetting As SettingInfo
        objsetting = _SettingController.GetKeyvalue(KeyName)
        If objsetting.KeyValue = "0" Then
            Return False
        ElseIf objsetting.KeyValue = "1" Then
            Return True
        Else
            Return objsetting.KeyValue
        End If

    End Function
    Public Sub PrintOrderReturn(ByVal dt As DataTable, ByVal dtDetail As DataTable)
        Dim PhotoPath As String = ""
        Dim StrBarcode As String = ""
        Dim StrNo As String = ""
        Dim i As Integer = 0

        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderInvoiceReturn_Print.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_OrderInvoiceReturn_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoice_OrderInvoice", dtDetail))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dtDetail.Rows.Count() = 0 Then
            MsgBox("Please Select Sale Voucher First!", MsgBoxStyle.Information, AppName)
            Exit Sub
        Else
            For Each dr As DataRow In dtDetail.Rows
                dr.Item("Photo") = Global_PhotoPath + "\" + dr.Item("Photo")
            Next
        End If

        If dtDetail.Rows.Count > 0 Then
            If dtDetail.Rows.Count > 0 Then
                For Each dr As DataRow In dtDetail.Rows
                    StrBarcode &= " " & dr.Item("ItemCode") & Chr(13) & Chr(10)
                    i += 1
                    StrNo &= i & Chr(13) & Chr(10)
                Next
                'If StrBarcode.Length > 0 Then
                '    StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
                'End If
            End If
        Else
            StrBarcode = ""
        End If

        Dim StrItemCode(0) As ReportParameter
        Dim StrNumber(0) As ReportParameter
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        StrNumber(0) = New ReportParameter("StrNumber", StrNo)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrNumber)

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintOrderReceive(ByVal dt As DataTable)
        Dim StrBarcode As String = ""

        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderReceive_Print_.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_OrderInvoice_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoice_OrderInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True


        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub OrderReceiveVoucherPrint(ByVal dt As DataTable)
        'Dim StrBarcode As String = ""

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderReceiveVoucher_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_OrderReceiveVoucher_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsOrderReceiveItem_OrderReceive", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub OrderReceiveItemName(ByVal dt As DataTable)
        'Dim StrBarcode As String = ""

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_OrderReceiveItemName_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_OrderReceiveItemName_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("OrderReceiveItem_dsOrderReceiveItem", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True


        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        Dim G_PToY(0) As ReportParameter
        G_PToY(0) = New ReportParameter("G_PToY", Global_PToY)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(G_PToY)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintPurchase(ByVal dt As DataTable, ByVal dtDetail As DataTable)
        Dim PhotoPath As String = ""
        Dim Photo(0) As ReportParameter
        Dim StrBarcode As String = ""
        Dim StrNo As String = ""
        Dim i As Integer = 0

        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseTRSS_Print.rdlc"


        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_PurchaseInvoice_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dtDetail))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dtDetail.Rows.Count > 0 Then
            If dtDetail.Rows.Count > 0 Then
                For Each dr As DataRow In dtDetail.Rows
                    StrBarcode &= " " & dr.Item("BarcodeNo") & Chr(13) & Chr(10)
                    i += 1
                    StrNo &= i & Chr(13) & Chr(10)
                Next
                If StrBarcode.Length > 0 Then
                    StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 1)
                End If
            End If
        Else
            StrBarcode = ""
        End If

        Dim StrItemCode(0) As ReportParameter
        Dim StrNumber(0) As ReportParameter
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter

        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        StrNumber(0) = New ReportParameter("StrNumber", StrNo)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrNumber)

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintPurchaseGemPrint(ByVal dt As DataTable)
        Dim StrBarcode As String = ""
        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGems_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_PurchaseGemsTRSS_Print.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_PurchaseGems_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("ItemCategory") & " / " & dr.Item("ItemName") & " / " & dr.Item("QTY") & " / " & dr.Item("YOrCOrG") & " / " & Math.Round(dr.Item("TotalGemTG"), 3) & "(g)" & " / " & dr.Item("GemsK") & "-" & dr.Item("GemsP") & "-" & Math.Round(dr.Item("GemsY"), 1) & " / " & dr.Item("CurrentPrice") & " / " & dr.Item("TotalAmount") & "  ,  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)


        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub PrintSaleGemsVoucher(ByVal dt As DataTable)
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim StrBarcode As String = ""
        ' rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleGems_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleGemsTRSS_Print.rdlc"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleGems_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleGems_SaleGems", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("GemsCategory") & " / " & dr.Item("GemsName") & " / " & dr.Item("Qty") & " / " & dr.Item("YOrCOrG") & " / " & Math.Round(dr.Item("GemsTG"), 3) & "(g)" & " / " & dr.Item("GemsK") & "-" & dr.Item("GemsP") & "-" & Math.Round(dr.Item("GemsY"), 1) & " / " & dr.Item("SaleRate") & " / " & dr.Item("Amount") & "  ,  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)



        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub RepairReceiveVoucher(ByVal dt As DataTable)
        'Dim StrBarcode As String = ""

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_RepairReceive_Voucher.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_RepairReceive_Voucher.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsRepairReceive_RepairVoucher", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True


        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub RepairReturnVoucher(ByVal dt As DataTable)
        'Dim StrBarcode As String = ""

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_RepairReturnVoucher.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_RepairReturnVoucher.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsRepairReturn_dtRepairReturn", dt))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True

        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim dtItem As DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If
        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)

        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintCheckStock(ByVal dt As DataTable, ByVal dtM As DataTable, ByVal dtE As DataTable)
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_CheckStockForWG.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()

        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsCheckStock_CheckStock", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsCheckStock_MCheckStock", dtM))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsCheckStock_ECheckStock", dtE))

        'Update For Print Remark SakyarShweYee (10.10.12)
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim Address(0) As ReportParameter

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        Address(0) = New ReportParameter("Address", _LocationController.GetLocationByID(Global_CurrentLocationID).Address)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Address)


        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub

    Public Sub IntDiamondPriceRate(ByVal dt As DataTable)
        rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_IntDiamondPriceRate.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsIntDiamondPrice_IntDiamondPrice", dt))
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
    Public Sub PrintSaleLooseDiamond(ByVal dt As DataTable, ByVal dtPurchase As DataTable)
        Dim PhotoPath As String = ""
        Dim Remark15(0) As ReportParameter
        Dim RemarkDone(0) As ReportParameter
        Dim StrBarcode As String = ""
        'Dim filepath As String = ""
        'Dim FileMap As ExeConfigurationFileMap


        'FileMap = New ExeConfigurationFileMap
        'FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        'config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        'Dim ReportName As String = ""
        'ReportName = config.AppSettings.Settings("SaleVolumeA4ReportName").Value()
        'If ReportName <> "" Then
        '    filepath = My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & ReportName
        'Else
        '    filepath = My.Application.Info.DirectoryPath & "\Report\RDLC\ rpt_SaleVolumeInvoice_Print.rdl"
        'End If

        ''rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleVolumeInvoice_Print.rdl"
        ''rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\RDLC\rpt_SaleVolumeInvoice_Print.rdl"
        'rpt_SaleInvoice_Print1.LocalReport.ReportPath = filepath

        'rpt_SaleInvoice_Print1.LocalReport.ReportEmbeddedResource = "UI.rpt_SaleVolumeInvoice_Print.rdlc"
        rpt_SaleInvoice_Print1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\VoucherRDL\rpt_SaleLooseDiamondInvoice_Print.rdl"
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Clear()
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsSaleLooseDiamondInvoice_SaleLooseDiamondInvoice", dt))
        rpt_SaleInvoice_Print1.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_dtPurchaseIsChange", dtPurchase))
        rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True
        'rpt_SaleInvoice_Print1.LocalReport.EnableExternalImages = True
        ' rpt_SaleInvoice_Print1.RefreshReport()

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                StrBarcode &= " " & dr.Item("ItemCode") & " , " & dr.Item("GemsName") & " , " & dr.Item("QTY") & " , " & Math.Round(dr.Item("ItemTG"), 3) & " (g)" & " , " & dr.Item("SalesRate") & "  /  "
            Next
            If StrBarcode.Length > 0 Then
                StrBarcode = StrBarcode.Substring(0, Len(StrBarcode) - 3)
            End If
        Else
            StrBarcode = ""
        End If
        Dim StrItemCode(0) As ReportParameter
        StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(StrItemCode)

        Dim dtItem As New DataTable
        Dim FirstName(0) As ReportParameter
        Dim FirstTitle(0) As ReportParameter
        Dim FirstBold(0) As ReportParameter
        Dim FirstItalic(0) As ReportParameter
        Dim FirstFontSize(0) As ReportParameter
        Dim FirstFontColor(0) As ReportParameter
        Dim FirstLogoPhoto(0) As ReportParameter

        Dim Title2 As String
        Dim Name2 As String
        Dim Bold2 As Boolean
        Dim Italic2 As Boolean
        Dim FontSize2 As Integer
        Dim FontColor2 As String
        Dim LogoPhoto2 As String
        Dim SecondName(0) As ReportParameter
        Dim SecondTitle(0) As ReportParameter
        Dim SecondBold(0) As ReportParameter
        Dim SecondItalic(0) As ReportParameter
        Dim SecondFontSize(0) As ReportParameter
        Dim SecondFontColor(0) As ReportParameter
        Dim SecondLogoPhoto(0) As ReportParameter

        Dim Title3 As String
        Dim Name3 As String
        Dim Bold3 As Boolean
        Dim Italic3 As Boolean
        Dim FontSize3 As Integer
        Dim FontColor3 As String
        Dim LogoPhoto3 As String

        Dim ThirdName(0) As ReportParameter
        Dim ThirdTitle(0) As ReportParameter
        Dim ThirdBold(0) As ReportParameter
        Dim ThirdItalic(0) As ReportParameter
        Dim ThirdFontSize(0) As ReportParameter
        Dim ThirdFontColor(0) As ReportParameter
        Dim ThirdLogoPhoto(0) As ReportParameter

        Dim Title1 As String
        Dim Name1 As String
        Dim Bold1 As Boolean
        Dim Italic1 As Boolean
        Dim FontSize1 As Integer
        Dim FontColor1 As String
        Dim LogoPhoto1 As String

        dtItem = _VoucherSettingCon.GetAllVoucherSettingByVoucher
        If dtItem.Rows.Count() > 0 Then
            For Each dr As DataRow In dtItem.Rows
                If dr.Item("TitleType") = "FirstTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title1 = "_"
                    Else
                        Title1 = dr.Item("Title").ToString.Trim
                    End If

                    Name1 = dr.Item("FontName")
                    Bold1 = dr.Item("Bold")
                    Italic1 = dr.Item("Italic")
                    FontSize1 = dr.Item("FontSize")
                    FontColor1 = dr.Item("FontColor").ToString
                    LogoPhoto1 = Global_PhotoPath + "\" + dr.Item("Photo")

                    FirstTitle(0) = New ReportParameter("FirstTitle", Title1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstTitle)

                    FirstName(0) = New ReportParameter("FirstName", Name1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstName)

                    FirstBold(0) = New ReportParameter("FirstBold", Bold1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstBold)

                    FirstItalic(0) = New ReportParameter("FirstItalic", Italic1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstItalic)

                    FirstFontSize(0) = New ReportParameter("FirstFontSize", FontSize1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontSize)

                    FirstFontColor(0) = New ReportParameter("FirstFontColor", FontColor1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstFontColor)

                    FirstLogoPhoto(0) = New ReportParameter("FirstLogoPhoto", LogoPhoto1)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(FirstLogoPhoto)

                ElseIf dr.Item("TitleType") = "SecondTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title2 = "_"
                    Else
                        Title2 = dr.Item("Title").ToString.Trim
                    End If
                    Name2 = dr.Item("FontName")
                    Bold2 = dr.Item("Bold")
                    Italic2 = dr.Item("Italic")
                    FontSize2 = dr.Item("FontSize")
                    FontColor2 = dr.Item("FontColor")
                    LogoPhoto2 = dr.Item("Photo")

                    SecondTitle(0) = New ReportParameter("SecondTitle", Title2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondTitle)

                    SecondName(0) = New ReportParameter("SecondName", Name2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondName)

                    SecondBold(0) = New ReportParameter("SecondBold", Bold2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondBold)

                    SecondItalic(0) = New ReportParameter("SecondItalic", Italic2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondItalic)

                    SecondFontSize(0) = New ReportParameter("SecondFontSize", FontSize2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontSize)

                    SecondFontColor(0) = New ReportParameter("SecondFontColor", FontColor2)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondFontColor)

                    'SecondLogoPhoto(0) = New ReportParameter("SecondLogoPhoto", LogoPhoto2)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(SecondLogoPhoto)

                ElseIf dr.Item("TitleType") = "ThirdTitle" Then
                    If dr.Item("Title").ToString.Trim = "" Then
                        Title3 = "_"
                    Else
                        Title3 = dr.Item("Title").ToString.Trim
                    End If
                    Name3 = dr.Item("FontName")
                    Bold3 = dr.Item("Bold")
                    Italic3 = dr.Item("Italic")
                    FontSize3 = dr.Item("FontSize")
                    FontColor3 = dr.Item("FontColor")
                    LogoPhoto3 = dr.Item("Photo")

                    ThirdTitle(0) = New ReportParameter("ThirdTitle", Title3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdTitle)

                    ThirdName(0) = New ReportParameter("ThirdName", Name3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdName)

                    ThirdBold(0) = New ReportParameter("ThirdBold", Bold3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdBold)

                    ThirdItalic(0) = New ReportParameter("ThirdItalic", Italic3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdItalic)

                    ThirdFontSize(0) = New ReportParameter("ThirdFontSize", FontSize3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontSize)

                    ThirdFontColor(0) = New ReportParameter("ThirdFontColor", FontColor3)
                    rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdFontColor)

                    'ThirdLogoPhoto(0) = New ReportParameter("ThirdLogoPhoto", LogoPhoto3)
                    'rpt_SaleInvoice_Print1.LocalReport.SetParameters(ThirdLogoPhoto)
                End If
            Next
        Else
            Title1 = ""
            Bold1 = False
            Italic1 = False
            FontSize1 = 0
            FontColor1 = ""
            LogoPhoto1 = ""
        End If

        Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(Remark15)

        RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
        rpt_SaleInvoice_Print1.LocalReport.SetParameters(RemarkDone)
        rpt_SaleInvoice_Print1.RefreshReport()
    End Sub
End Class