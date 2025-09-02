Imports System.Windows.Forms
Imports CommonInfo
Imports BusinessRule
Imports System.Drawing.FontStyle
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Imports System.Reflection
Imports System.Data.SqlClient

Public Class frm_VoucherTemplate
    Dim IsSelect As Boolean = False
    Public config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
    '' Private _settingcontroller As Setting.ISettingController = Factory.Instance.CreateSettingController
    Private _LocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private Sub frm_VoucherTemplate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim tmpstr() As String
        Dim FileMap As ExeConfigurationFileMap
        Dim DMReport As String = ""
        RDLCTemplate()
        If optSaleInvoice.Checked Then
            GetRDLCFileName("A4", "")
            tmpstr = GetRDLCFileName("A4", "")
        ElseIf optOrderReceive.Checked Then
            GetRDLCFileName("OrderA4", "")
            tmpstr = GetRDLCFileName("OrderA4", "")
        ElseIf optPurchaseInvoice.Checked Then
            GetRDLCFileName("PurchaseA4", "")
            tmpstr = GetRDLCFileName("PurchaseA4", "")
        ElseIf optPurchaseInvoice.Checked Then
            GetRDLCFileName("SaleGemA4", "")
            tmpstr = GetRDLCFileName("SaleGemA4", "")
        ElseIf optPurchaseInvoice.Checked Then
            GetRDLCFileName("SaleVolumeA4", "")
            tmpstr = GetRDLCFileName("SaleVolumeA4", "")
        ElseIf optPurchaseInvoice.Checked Then
            GetRDLCFileName("OrderReturnA4", "")
            tmpstr = GetRDLCFileName("OrderReturnA4", "")
        ElseIf optPurchaseInvoice.Checked Then
            GetRDLCFileName("PurchaseGemA4", "")
            tmpstr = GetRDLCFileName("PurchaseGemA4", "")
        End If

        If tmpstr IsNot Nothing Then
            If tmpstr.Length > 0 Then
                FileMap = New ExeConfigurationFileMap
                FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                If optSaleInvoice.Checked Then
                    DMReport = config.AppSettings.Settings("A4ReportName").Value
                ElseIf optOrderReceive.Checked Then
                    DMReport = config.AppSettings.Settings("OrderA4ReportName").Value
                ElseIf optPurchaseInvoice.Checked Then
                    DMReport = config.AppSettings.Settings("PurchaseA4ReportName").Value
                ElseIf optSaleGem.Checked Then
                    DMReport = config.AppSettings.Settings("SaleGemA4ReportName").Value
                ElseIf optSaleVolume.Checked Then
                    DMReport = config.AppSettings.Settings("SaleVolumeA4ReportName").Value
                ElseIf optOrderReturn.Checked Then
                    DMReport = config.AppSettings.Settings("OrderReturnA4ReportName").Value
                ElseIf optPurchaseGem.Checked Then
                    DMReport = config.AppSettings.Settings("PurchaseGemA4ReportName").Value
                End If

                cboChangeTemplate.Items.Clear()
                For i As Integer = 0 To tmpstr.Length - 1
                    cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                Next
            End If
        End If

        cboChangeTemplate.Text = DMReport.ToString
        IsSelect = True
        'ShowData()

        Me.RptViewer.RefreshReport()
        'Dim dt As New DataTable
        'Dim dr As DataRow()
        'dt = _settingcontroller.GetKeyName()

        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String
        'dr = dt.Select("KeyName='PrinterType'")
        'If dr.Length > 0 Then
        'If dr(0).Item("KeyValue") = "1" Then

        If optSaleInvoice.Checked Then
            ReportName = config.AppSettings.Settings("A4ReportName").Value()
            Preview("bya4", ReportName)
        ElseIf optOrderReceive.Checked Then
            ReportName = config.AppSettings.Settings("OrderA4ReportName").Value()
            Preview("OrderA4", ReportName)
        ElseIf optPurchaseInvoice.Checked Then
            ReportName = config.AppSettings.Settings("PurchaseA4ReportName").Value()
            Preview("PurchaseA4", ReportName)
        ElseIf optSaleGem.Checked Then
            ReportName = config.AppSettings.Settings("SaleGemA4ReportName").Value()
            Preview("SaleGemA4", ReportName)
        ElseIf optSaleVolume.Checked Then
            ReportName = config.AppSettings.Settings("SaleVolumeA4ReportName").Value()
            Preview("SaleVolumeA4", ReportName)
        ElseIf optOrderReturn.Checked Then
            ReportName = config.AppSettings.Settings("OrderReturnA4ReportName").Value()
            Preview("OrderReturnA4", ReportName)
        ElseIf optPurchaseGem.Checked Then
            ReportName = config.AppSettings.Settings("PurchaseGemA4ReportName").Value()
            Preview("PurchaseGemA4", ReportName)
        End If
        'Else
        'ReportName = config.AppSettings.Settings("A4ReportName").Value()
        'Preview("bya4", ReportName)
        'End If
        'End If

        Me.RptViewer.Refresh()
    End Sub
    'Private Sub ShowData()
    '    Dim dt As New DataTable

    '    dt = _settingcontroller.GetKeyName()
    '    Dim dr As DataRow()

    '    dr = dt.Select("KeyName='AutoPrint'")
    '    If dr.Length > 0 Then
    '        If dr(0).Item("KeyValue") = 1 Then
    '            chkDirectPrint.Checked = True
    '        Else
    '            chkDirectPrint.Checked = False
    '        End If
    '    End If
    'End Sub

    Public Sub RDLCTemplate()
        Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\")
        Dim diar As IO.FileInfo() = di.GetFiles()
        Dim dra As IO.FileInfo
        Dim count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_SaleInvoice_Print15") Then
                count += 1
            End If
        Next
        If count > 1 Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice_Print15")
        End If
        diar = di.GetFiles()
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_SaleInvoice_Print15.rdl") Then
                If dra.Name.Contains("rpt_SaleInvoice_Print15") Or dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            Else
                If dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            End If
        Next
        If count > 1 Then
            Dim Name = "rpt_SaleInvoice_Print15.rdl"
            ' My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice_Print.rdlc", Name.Replace(".rdlc", ""))
        End If

        Dim ORA4 As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\")
        Dim A4T As IO.FileInfo() = di.GetFiles()
        Dim A4TP As IO.FileInfo
        count = 0
        For Each A4TP In A4T
            If A4TP.Name.Contains("rpt_OrderInvoice_Print") Then
                count += 1
            End If
        Next
        If count > 1 Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\rpt_OrderInvoice_Print")
        End If
        A4T = ORA4.GetFiles()
        count = 0
        For Each A4TP In A4T
            If A4TP.Name.Contains("rpt_OrderInvoice_Print.rdl") Then
                If A4TP.Name.Contains("rpt_OrderInvoice_Print") And A4TP.Name.Contains(".rdl") Then
                    count += 1
                End If
            Else
                If A4TP.Name.Contains(".rdl") Then
                    count += 1
                End If
            End If

        Next
        If count > 1 Then
            Dim Name = "rpt_OrderInvoice_Print.rdl"
            '  My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice.rdl", Name.Replace(".rdl", ""))
        End If


        Dim PA4 As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseA4RDL\")
        Dim A4P As IO.FileInfo() = di.GetFiles()
        Dim A4PP As IO.FileInfo
        count = 0
        For Each A4PP In A4P
            If A4PP.Name.Contains("rpt_PurchaseInvoice_Print") Then
                count += 1
            End If
        Next
        If count > 1 Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Report\PurchaseA4RDL\rpt_PurchaseInvoice_Print")
        End If
        A4T = ORA4.GetFiles()
        count = 0
        For Each A4TP In A4T
            If A4TP.Name.Contains("rpt_PurchaseInvoice_Print.rdl") Then
                If A4TP.Name.Contains("rpt_PurchaseInvoice_Print") And A4TP.Name.Contains(".rdl") Then
                    count += 1
                End If
            Else
                If A4TP.Name.Contains(".rdl") Then
                    count += 1
                End If
            End If

        Next
        If count > 1 Then
            Dim Name = "rpt_PurchaseInvoice_Print.rdl"
            '  My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice.rdl", Name.Replace(".rdl", ""))
        End If

        Dim di1 As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\")
        Dim diar1 As IO.FileInfo() = di.GetFiles()
        Dim dra1 As IO.FileInfo
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_SaleGems_Print") Then
                count += 1
            End If
        Next
        If count > 1 Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\rpt_SaleGems_Print")
        End If
        diar = di.GetFiles()
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_SaleGems_Print.rdl") Then
                If dra.Name.Contains("rpt_SaleGems_Print") Or dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            Else
                If dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            End If
        Next
        If count > 1 Then
            Dim Name = "rpt_SaleGems_Print.rdl"
            ' My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice_Print.rdlc", Name.Replace(".rdlc", ""))
        End If


        Dim di2 As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\")
        Dim diar2 As IO.FileInfo() = di.GetFiles()
        Dim dra2 As IO.FileInfo
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_SaleVolumeInvoice_Print") Then
                count += 1
            End If
        Next
        If count > 1 Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\rpt_SaleVolumeInvoice_Print")
        End If
        diar = di.GetFiles()
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_SaleVolumeInvoice_Print.rdl") Then
                If dra.Name.Contains("rpt_SaleVolumeInvoice_Print") Or dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            Else
                If dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            End If
        Next
        If count > 1 Then
            Dim Name = "rpt_SaleVolumeInvoice_Print.rdl"
            ' My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice_Print.rdlc", Name.Replace(".rdlc", ""))
        End If


        Dim di3 As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\")
        Dim diar3 As IO.FileInfo() = di.GetFiles()
        Dim dra3 As IO.FileInfo
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_OrderInvoice_Print15") Then
                count += 1
            End If
        Next
        If count > 1 Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\rpt_OrderInvoice_Print15")
        End If
        diar = di.GetFiles()
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_OrderInvoice_Print15.rdl") Then
                If dra.Name.Contains("rpt_OrderInvoice_Print15") Or dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            Else
                If dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            End If
        Next
        If count > 1 Then
            Dim Name = "rpt_OrderInvoice_Print15.rdl"
            ' My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice_Print.rdlc", Name.Replace(".rdlc", ""))
        End If


        Dim di4 As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\")
        Dim diar4 As IO.FileInfo() = di.GetFiles()
        Dim dra4 As IO.FileInfo
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_PurchaseGems_Print") Then
                count += 1
            End If
        Next
        If count > 1 Then
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\rpt_PurchaseGems_Print")
        End If
        diar = di.GetFiles()
        count = 0
        For Each dra In diar
            If dra.Name.Contains("rpt_PurchaseGems_Print.rdl") Then
                If dra.Name.Contains("rpt_PurchaseGems_Print") Or dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            Else
                If dra.Name.Contains(".rdl") Then
                    count += 1
                End If
            End If
        Next
        If count > 1 Then
            Dim Name = "rpt_PurchaseGems_Print.rdl"
            ' My.Computer.FileSystem.RenameFile(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\rpt_SaleInvoice_Print.rdlc", Name.Replace(".rdlc", ""))
        End If
    End Sub

    Private Function GetRDLCFileName(ByVal Type As String, ByVal process As String) As String()
        Dim sb As New StringBuilder
        Dim str() As String = {}
        '' Dim FileMap As ExeConfigurationFileMap
        Dim DMReport As String = ""

        If Type = "A4" Then
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\") Then
                IO.File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\")
                Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\")
                Dim diar As IO.FileInfo() = di.GetFiles()
                Dim dra As IO.FileInfo

                For Each dra In diar
                    If process = "transaction" Then
                        If dra.Name.Contains(".rdl") Then
                            sb.Append(dra.Name & ",")
                            str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                            '    Return str
                        Else
                            sb.Append(dra.Name & ",")
                        End If
                        'If dra.Name.Contains(".rdlc") Then
                        '    sb.Append(dra.Name.Replace(".rdlc", ","))
                    Else
                        sb.Append(dra.Name & ",")
                    End If
                Next
                If sb.ToString <> "" Then
                    str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                End If
            End If

        ElseIf Type = "OrderA4" Then
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\") Then
                IO.File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\")
                Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\")
                Dim diar As IO.FileInfo() = di.GetFiles()
                Dim dra As IO.FileInfo

                For Each dra In diar
                    If process = "transaction" Then
                        If dra.Name.Contains(".rdl") Then
                            sb.Append(dra.Name & ",")
                            str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                            '    Return str
                        Else
                            sb.Append(dra.Name & ",")
                        End If
                        'If dra.Name.Contains(".rdlc") Then
                        '    sb.Append(dra.Name.Replace(".rdlc", ","))
                    Else
                        sb.Append(dra.Name & ",")
                    End If
                Next
                If sb.ToString <> "" Then
                    str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                End If
            End If
        ElseIf Type = "PurchaseA4" Then
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\") Then
                IO.File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\")
                Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\")
                Dim diar As IO.FileInfo() = di.GetFiles()
                Dim dra As IO.FileInfo

                For Each dra In diar
                    If process = "transaction" Then
                        If dra.Name.Contains(".rdl") Then
                            sb.Append(dra.Name & ",")
                            str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                            '    Return str
                        Else
                            sb.Append(dra.Name & ",")
                        End If
                        'If dra.Name.Contains(".rdlc") Then
                        '    sb.Append(dra.Name.Replace(".rdlc", ","))
                    Else
                        sb.Append(dra.Name & ",")
                    End If
                Next
                If sb.ToString <> "" Then
                    str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                End If
            End If

        ElseIf Type = "SaleGemA4" Then
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\") Then
                IO.File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\")
                Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\")
                Dim diar As IO.FileInfo() = di.GetFiles()
                Dim dra As IO.FileInfo

                For Each dra In diar
                    If process = "transaction" Then
                        If dra.Name.Contains(".rdl") Then
                            sb.Append(dra.Name & ",")
                            str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                            '    Return str
                        Else
                            sb.Append(dra.Name & ",")
                        End If
                        'If dra.Name.Contains(".rdlc") Then
                        '    sb.Append(dra.Name.Replace(".rdlc", ","))
                    Else
                        sb.Append(dra.Name & ",")
                    End If
                Next
                If sb.ToString <> "" Then
                    str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                End If
            End If
        ElseIf Type = "SaleVolumeA4" Then
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\") Then
                IO.File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\")
                Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\")
                Dim diar As IO.FileInfo() = di.GetFiles()
                Dim dra As IO.FileInfo

                For Each dra In diar
                    If process = "transaction" Then
                        If dra.Name.Contains(".rdl") Then
                            sb.Append(dra.Name & ",")
                            str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                            '    Return str
                        Else
                            sb.Append(dra.Name & ",")
                        End If
                        'If dra.Name.Contains(".rdlc") Then
                        '    sb.Append(dra.Name.Replace(".rdlc", ","))
                    Else
                        sb.Append(dra.Name & ",")
                    End If
                Next
                If sb.ToString <> "" Then
                    str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                End If
            End If
        ElseIf Type = "OrderReturnA4" Then
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\") Then
                IO.File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\")
                Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\")
                Dim diar As IO.FileInfo() = di.GetFiles()
                Dim dra As IO.FileInfo

                For Each dra In diar
                    If process = "transaction" Then
                        If dra.Name.Contains(".rdl") Then
                            sb.Append(dra.Name & ",")
                            str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                            '    Return str
                        Else
                            sb.Append(dra.Name & ",")
                        End If
                        'If dra.Name.Contains(".rdlc") Then
                        '    sb.Append(dra.Name.Replace(".rdlc", ","))
                    Else
                        sb.Append(dra.Name & ",")
                    End If
                Next
                If sb.ToString <> "" Then
                    str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                End If
            End If
        ElseIf Type = "PurchaseGemA4" Then
            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\") Then
                IO.File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\")
                Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\")
                Dim diar As IO.FileInfo() = di.GetFiles()
                Dim dra As IO.FileInfo

                For Each dra In diar
                    If process = "transaction" Then
                        If dra.Name.Contains(".rdl") Then
                            sb.Append(dra.Name & ",")
                            str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                            '    Return str
                        Else
                            sb.Append(dra.Name & ",")
                        End If
                        'If dra.Name.Contains(".rdlc") Then
                        '    sb.Append(dra.Name.Replace(".rdlc", ","))
                    Else
                        sb.Append(dra.Name & ",")
                    End If
                Next
                If sb.ToString <> "" Then
                    str = sb.ToString.Substring(0, sb.ToString.LastIndexOf(",")).Split(",")
                End If
            End If
        End If

        Return str
    End Function

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim strFileName As String
        strFileName = InputBox("Enter File Name ", Me.Text)
        If strFileName <> "" Then
            If optSaleInvoice.Checked Then
                If CreateTemplate(strFileName, "A4") Then
                    cboChangeTemplate.Items.AddRange(New String() {strFileName & ".rdl"})
                    ShowTemplate()
                End If
            ElseIf optOrderReceive.Checked Then
                If CreateTemplate(strFileName, "OrderA4") Then
                    cboChangeTemplate.Items.AddRange(New String() {strFileName & ".rdl"})
                    ShowTemplate()
                End If
            ElseIf optPurchaseInvoice.Checked Then
                If CreateTemplate(strFileName, "PurchaseA4") Then
                    cboChangeTemplate.Items.AddRange(New String() {strFileName & ".rdl"})
                    ShowTemplate()
                End If
            ElseIf optSaleGem.Checked Then
                If CreateTemplate(strFileName, "SaleGemA4") Then
                    cboChangeTemplate.Items.AddRange(New String() {strFileName & ".rdl"})
                    ShowTemplate()
                End If
            ElseIf optSaleVolume.Checked Then
                If CreateTemplate(strFileName, "SaleVolumeA4") Then
                    cboChangeTemplate.Items.AddRange(New String() {strFileName & ".rdl"})
                    ShowTemplate()
                End If
            ElseIf optOrderReturn.Checked Then
                If CreateTemplate(strFileName, "OrderReturnA4") Then
                    cboChangeTemplate.Items.AddRange(New String() {strFileName & ".rdl"})
                    ShowTemplate()
                End If
            ElseIf optPurchaseGem.Checked Then
                If CreateTemplate(strFileName, "PurchaseGemA4") Then
                    cboChangeTemplate.Items.AddRange(New String() {strFileName & ".rdl"})
                    ShowTemplate()
                End If
            End If
            

        End If
    End Sub

    Private Sub ShowTemplate()
        RDLCTemplate()

        Me.RptViewer.RefreshReport()

        Dim FileMap As ExeConfigurationFileMap

        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String

        If optSaleInvoice.Checked Then
            ReportName = config.AppSettings.Settings("A4ReportName").Value()
            Preview("bya4", ReportName)
        ElseIf optOrderReceive.Checked Then
            ReportName = config.AppSettings.Settings("OrderA4ReportName").Value()
            Preview("OrderA4", ReportName)
        ElseIf optPurchaseInvoice.Checked Then
            ReportName = config.AppSettings.Settings("PurchaseA4ReportName").Value()
            Preview("PurchaseA4", ReportName)
        ElseIf optSaleGem.Checked Then
            ReportName = config.AppSettings.Settings("SaleGemA4ReportName").Value()
            Preview("SaleGemA4", ReportName)
        ElseIf optSaleVolume.Checked Then
            ReportName = config.AppSettings.Settings("SaleVolumeA4ReportName").Value()
            Preview("SaleVolumeA4", ReportName)
        ElseIf optOrderReturn.Checked Then
            ReportName = config.AppSettings.Settings("OrderReturnA4ReportName").Value()
            Preview("OrderReturnA4", ReportName)
        ElseIf optPurchaseGem.Checked Then
            ReportName = config.AppSettings.Settings("PurchaseGemA4ReportName").Value()
            Preview("PurchaseGemA4", ReportName)
        End If
       
        Me.RptViewer.Refresh()

    End Sub

    Private Sub Preview(ByVal Previewtype As String, ByVal ReportName As String)
        Dim PhotoPath As String = ""
        Dim StrBarcode1 As String = ""

        Me.RptViewer.Reset()
        Dim dt As New DataTable
        Dim dtunit As New DataTable
        Dim filepath As String = ""
        dt.Clear()

        dt = CreateTable()
        Dim i As Integer = 0

        If Previewtype = "bya4" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & ReportName
            'My.Computer.FileSystem.RenameFile(filepath, ReportName & ".rdl")
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & ReportName
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleInvoice_SaleInvoice", dt))
            RptViewer.LocalReport.EnableExternalImages = True

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                    StrBarcode1 &= " " & dr.Item("ItemCode") & " , " & dr.Item("ItemName") & " , " & dr.Item("TotalTG") & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & dr.Item("TotalY") & " , " & dr.Item("SalesRate") & " (" & dr.Item("GoldQuality") & ")  , " & dr.Item("ItemNetAmount") & "  /  "
                Next
                If StrBarcode1.Length > 0 Then
                    StrBarcode1 = StrBarcode1.Substring(0, Len(StrBarcode1) - 3)
                End If
            Else
                StrBarcode1 = ""
            End If


            Dim StrItemCode(0) As ReportParameter
            StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode1)
            RptViewer.LocalReport.SetParameters(StrItemCode)

        ElseIf Previewtype = "OrderA4" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & ReportName
            'My.Computer.FileSystem.RenameFile(filepath, ReportName & ".rdl")
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & ReportName
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoiceItem_OrderInvoiceItem", dt))
            RptViewer.LocalReport.EnableExternalImages = True

            ''If dt.Rows.Count > 0 Then
            ''    For Each dr As DataRow In dt.Rows
            ''        If IsDBNull(dr.Item("ForSaleFixPrice")) Then
            ''            dr.Item("ForSaleFixPrice") = False
            ''        End If
            ''        '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
            ''        StrBarcode1 &= IIf(dr.Item("BarcodeNo") = "", "", dr.Item("BarcodeNo")) & IIf(dr.Item("BarcodeNo") = "", "", " , ") & dr.Item("ItemName") & " , " & dr.Item("QTY") & " , " & Math.Round(dr.Item("TotalTG"), 3) & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & " , " & IIf(Len(dr.Item("StrCurrentPrice")) < 3, dr.Item("CurrentPrice") & "%", dr.Item("CurrentPrice")) & IIf(dr.Item("ForSaleFixPrice") = False, "", dr.Item("OldSaleAmount")) & " (" & dr.Item("GoldQuality") & ")  ,  " & IIf(dr.Item("PurchaseWastePercent") > 0, dr.Item("PurchaseDiscountAmount"), "") & IIf(dr.Item("PurchaseWastePercent") > 0, " (၀ယ်လျော့)  ,   ", "") & dr.Item("ItemTotalAmount") & "  /  "
            ''    Next
            ''    If StrBarcode1.Length > 0 Then
            ''        StrBarcode1 = StrBarcode1.Substring(0, Len(StrBarcode1) - 3)
            ''    End If
            ''Else
            ''    StrBarcode1 = ""
            ''End If
            ''Dim StrItemCode(0) As ReportParameter
            ''StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode1)
            ''RptViewer.LocalReport.SetParameters(StrItemCode)

        ElseIf Previewtype = "PurchaseA4" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & ReportName
            'My.Computer.FileSystem.RenameFile(filepath, ReportName & ".rdl")
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & ReportName
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
            RptViewer.LocalReport.EnableExternalImages = True

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If IsDBNull(dr.Item("ForSaleFixPrice")) Then
                        dr.Item("ForSaleFixPrice") = False
                    End If
                    '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                    StrBarcode1 &= IIf(dr.Item("BarcodeNo") = "", "", dr.Item("BarcodeNo")) & IIf(dr.Item("BarcodeNo") = "", "", " , ") & dr.Item("ItemName") & " , " & dr.Item("QTY") & " , " & dr.Item("TotalTG") & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & dr.Item("TotalY") & " , " & IIf(Len(dr.Item("StrCurrentPrice")) < 3, dr.Item("CurrentPrice") & "%", dr.Item("CurrentPrice")) & IIf(dr.Item("ForSaleFixPrice") = False, "", dr.Item("OldSaleAmount")) & " (" & dr.Item("GoldQuality") & ")  ,  " & IIf(dr.Item("PurchaseWastePercent") > 0, dr.Item("PurchaseDiscountAmount"), "") & IIf(dr.Item("PurchaseWastePercent") > 0, " (၀ယ်လျော့)  ,   ", "") & dr.Item("ItemTotalAmount") & "  /  "
                Next
                If StrBarcode1.Length > 0 Then
                    StrBarcode1 = StrBarcode1.Substring(0, Len(StrBarcode1) - 3)
                End If
            Else
                StrBarcode1 = ""
            End If
            Dim StrItemCode(0) As ReportParameter
            StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode1)
            RptViewer.LocalReport.SetParameters(StrItemCode)

        ElseIf Previewtype = "SaleGemA4" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & ReportName
            'My.Computer.FileSystem.RenameFile(filepath, ReportName & ".rdl")
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & ReportName
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSaleGems_SaleGems", dt))
            RptViewer.LocalReport.EnableExternalImages = True

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                    StrBarcode1 &= " " & dr.Item("GemsCategory") & " / " & dr.Item("GemsName") & " / " & dr.Item("Qty") & " / " & dr.Item("YOrCOrG") & " / " & dr.Item("GemsTG") & "(g)" & " / " & dr.Item("GemsK") & "-" & dr.Item("GemsP") & "-" & dr.Item("GemsY") & " / " & dr.Item("SaleRate") & " / " & dr.Item("Amount") & "  ,  "
                Next
                If StrBarcode1.Length > 0 Then
                    StrBarcode1 = StrBarcode1.Substring(0, Len(StrBarcode1) - 3)
                End If
            Else
                StrBarcode1 = ""
            End If

            Dim StrItemCode(0) As ReportParameter
            StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode1)
            RptViewer.LocalReport.SetParameters(StrItemCode)

        ElseIf Previewtype = "SaleVolumeA4" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & ReportName
            'My.Computer.FileSystem.RenameFile(filepath, ReportName & ".rdl")
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & ReportName
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsSalesVolumeInvoice_SalesVolumeInvoice", dt))
            RptViewer.LocalReport.EnableExternalImages = True

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                    StrBarcode1 &= " " & dr.Item("ItemCode") & " , " & dr.Item("ItemName") & " , " & dr.Item("QTY") & " , " & dr.Item("TotalTG") & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & dr.Item("TotalY") & " , " & dr.Item("SalesRate") & " (" & dr.Item("GoldQuality") & ") , " & dr.Item("ItemNetAmount") & "  /  "
                Next
                If StrBarcode1.Length > 0 Then
                    StrBarcode1 = StrBarcode1.Substring(0, Len(StrBarcode1) - 3)
                End If
            Else
                StrBarcode1 = ""
            End If

            Dim StrItemCode(0) As ReportParameter
            StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode1)
            RptViewer.LocalReport.SetParameters(StrItemCode)
        ElseIf Previewtype = "OrderReturnA4" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & ReportName
            'My.Computer.FileSystem.RenameFile(filepath, ReportName & ".rdl")
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & ReportName
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsOrderInvoice_OrderInvoice", dt))
            RptViewer.LocalReport.EnableExternalImages = True

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                    StrBarcode1 &= " " & dr.Item("ItemCode") & " , " & dr.Item("ItemName") & " , " & dr.Item("TotalTG") & " (g)" & " , " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & dr.Item("TotalY") & " , " & dr.Item("SalesRate") & " (" & dr.Item("GoldQuality") & ")  , " & dr.Item("ItemNetAmount") & "  /  "
                Next
                If StrBarcode1.Length > 0 Then
                    StrBarcode1 = StrBarcode1.Substring(0, Len(StrBarcode1) - 3)
                End If
            Else
                StrBarcode1 = ""
            End If

            Dim StrItemCode(0) As ReportParameter
            StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode1)
            RptViewer.LocalReport.SetParameters(StrItemCode)

        ElseIf Previewtype = "PurchaseGemA4" Then
            filepath = My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & ReportName
            'My.Computer.FileSystem.RenameFile(filepath, ReportName & ".rdl")
            RptViewer.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & ReportName
            RptViewer.LocalReport.DataSources.Clear()
            RptViewer.LocalReport.DataSources.Add(New ReportDataSource("dsPurchaseInvoice_PurchaseInvoice", dt))
            RptViewer.LocalReport.EnableExternalImages = True

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    '  StrBarcode &= " " & dr.Item("ItemCode") & "         " & Math.Round(dr.Item("TotalTG"), 3) & "(g)" & "         " & dr.Item("TotalK") & "-" & dr.Item("TotalP") & "-" & Math.Round(dr.Item("TotalY"), 1) & "         " & dr.Item("SalesRate") & "         " & dr.Item("ItemNetAmount") & "         " & dr.Item("ItemName") & Chr(13) & Chr(10)
                    StrBarcode1 &= " " & dr.Item("ItemCategory") & " / " & dr.Item("ItemName") & " / " & dr.Item("QTY") & " / " & dr.Item("YOrCOrG") & " / " & dr.Item("TotalGemTG") & "(g)" & " / " & dr.Item("GemsK") & "-" & dr.Item("GemsP") & "-" & dr.Item("GemsY") & " / " & dr.Item("CurrentPrice") & " / " & dr.Item("TotalAmount") & "  ,  "
                Next
                If StrBarcode1.Length > 0 Then
                    StrBarcode1 = StrBarcode1.Substring(0, Len(StrBarcode1) - 3)
                End If
            Else
                StrBarcode1 = ""
            End If

            Dim StrItemCode(0) As ReportParameter
            StrItemCode(0) = New ReportParameter("StrItemCode", StrBarcode1)
            RptViewer.LocalReport.SetParameters(StrItemCode)
        End If

        Try

            'Dim StrNumber(0) As ReportParameter
            'StrItemCode(0) = New ReportParameter("StrNumber", "")
            'RptViewer.LocalReport.SetParameters(StrNumber)

            Dim Remark15(0) As ReportParameter
            Dim RemarkDone(0) As ReportParameter

            Remark15(0) = New ReportParameter("Remark15", _LocationController.GetLocationByID(Global_CurrentLocationID).Remark15)
            RptViewer.LocalReport.SetParameters(Remark15)

            RemarkDone(0) = New ReportParameter("RemarkDone", _LocationController.GetLocationByID(Global_CurrentLocationID).RemarkDone)
            RptViewer.LocalReport.SetParameters(RemarkDone)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "TMS")
        End Try
        RptViewer.RefreshReport()

    End Sub

    Private Function CreateTable() As DataTable
        Dim dt As New DataTable
        If optSaleInvoice.Checked Then
            dt.Columns.Add("SalesInvoiceGemItemID")
            dt.Columns.Add("GemsCategoryID")
            dt.Columns.Add("GemsCategory")
            dt.Columns.Add("GemsName")
            dt.Columns.Add("GemsK")
            dt.Columns.Add("GemsP")
            dt.Columns.Add("GemsY")
            dt.Columns.Add("GemsTK")
            dt.Columns.Add("GemsTG")
            dt.Columns.Add("YOrCOrG")
            dt.Columns.Add("GemsTW")
            dt.Columns.Add("Qty")
            dt.Columns.Add("Type")
            dt.Columns.Add("UnitPrice")
            dt.Columns.Add("GemAmount")
            dt.Columns.Add("GemsRemark")

            dt.Columns.Add("SaleInvoiceDetailID")
            dt.Columns.Add("ForSaleID")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemNameID")

            dt.Columns.Add("ItemName")
            dt.Columns.Add("Length")
            dt.Columns.Add("GoldQualityID")
            dt.Columns.Add("GoldQuality")
            dt.Columns.Add("ItemCategoryID")
            dt.Columns.Add("ItemCategory")
            dt.Columns.Add("Width")
            dt.Columns.Add("FixPrice")
            dt.Columns.Add("DesignCharges")
            dt.Columns.Add("PlatingCharges")
            dt.Columns.Add("MountingCharges")
            dt.Columns.Add("WhiteCharges")
            dt.Columns.Add("Photo")
            dt.Columns.Add("SalesRate")
            dt.Columns.Add("GoldPrice")

            dt.Columns.Add("GemsPrice")
            dt.Columns.Add("IsFixPrice")
            dt.Columns.Add("ItemTotalAmount")
            dt.Columns.Add("ItemAddOrSub")
            dt.Columns.Add("ItemNetAmount")
            dt.Columns.Add("ItemTK")
            dt.Columns.Add("ItemTG")
            dt.Columns.Add("TotalGemsTK")
            dt.Columns.Add("TotalGemsTG")
            dt.Columns.Add("WasteTK")
            dt.Columns.Add("WasteTG")
            dt.Columns.Add("TotalTK")
            dt.Columns.Add("TotalTG")
            dt.Columns.Add("GoldTK")
            dt.Columns.Add("GoldTG")
            dt.Columns.Add("ItemK")
            dt.Columns.Add("ItemP")
            dt.Columns.Add("ItemY")
            dt.Columns.Add("TotalGemsK")
            dt.Columns.Add("TotalGemsP")
            dt.Columns.Add("TotalGemsY")

            dt.Columns.Add("WasteK")
            dt.Columns.Add("WasteP")
            dt.Columns.Add("WasteY")
            dt.Columns.Add("TotalK")
            dt.Columns.Add("TotalP")
            dt.Columns.Add("TotalY")
            dt.Columns.Add("GoldK")
            dt.Columns.Add("GoldP")
            dt.Columns.Add("GoldY")
            dt.Columns.Add("SaleInvoiceHeaderID")
            dt.Columns.Add("SaleDate")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("CustomerAddress")

            dt.Columns.Add("StaffID")
            dt.Columns.Add("Staff")
            dt.Columns.Add("Remark")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("AddOrSub")
            dt.Columns.Add("NetAmount")
            dt.Columns.Add("PromotionDiscount")
            dt.Columns.Add("PromotionAmount")
            dt.Columns.Add("DiscountAmount")
            dt.Columns.Add("PaidAmount")
            dt.Columns.Add("BalanceAmount")
            dt.Columns.Add("TotalCharges")


            Dim dr As DataRow
            dr = dt.NewRow
            dr("SalesInvoiceGemItemID") = ""
            dr("GemsCategoryID") = "01"
            dr("GemsCategory") = "ကျောက်စိမ်း"
            dr("GemsName") = "Testing"
            dr("GemsK") = "0"
            dr("GemsP") = "0"
            dr("GemsY") = "0"
            dr("GemsTK") = "0"
            dr("GemsTG") = "0"
            dr("YOrCOrG") = ""
            dr("GemsTW") = "0"
            dr("Qty") = "0"
            dr("Type") = ""
            dr("UnitPrice") = "0"
            dr("GemAmount") = "0"
            dr("GemsRemark") = ""

            dr("SaleInvoiceDetailID") = "0"
            dr("ForSaleID") = "0"
            dr("ItemCode") = "NE2102140001"
            dr("ItemNameID") = "00"
            dr("ItemName") = "ဟိန်းဝေယံကြို:"
            dr("Length") = "10"""
            dr("GoldQualityID") = "00"
            dr("GoldQuality") = "၁၆ ပဲရည်"
            dr("ItemCategoryID") = "00"
            dr("ItemCategory") = "ကြိုး"
            dr("Width") = "-"
            dr("FixPrice") = "0"
            dr("DesignCharges") = 20000
            dr("PlatingCharges") = 0
            dr("MountingCharges") = 0
            dr("WhiteCharges") = 0
            dr("Photo") = ""
            dr("SalesRate") = "640000"
            dr("GoldPrice") = 774000

            dr("GemsPrice") = 0
            dr("IsFixPrice") = False
            dr("ItemTotalAmount") = 794000
            dr("ItemAddOrSub") = 0
            dr("ItemNetAmount") = 794000
            dr("ItemTK") = 0
            dr("ItemTG") = 18.001
            dr("TotalGemsTK") = 0
            dr("TotalGemsTG") = 0
            dr("WasteTK") = 0
            dr("WasteTG") = 2.075
            dr("TotalTK") = 0
            dr("TotalTG") = 20.075
            dr("GoldTK") = 0
            dr("GoldTG") = 18.001
            dr("ItemK") = 1
            dr("ItemP") = 1
            dr("ItemY") = 2.8
            dr("TotalGemsK") = 0
            dr("TotalGemsP") = 0
            dr("TotalGemsY") = 0.0
            dr("WasteK") = 0
            dr("WasteP") = 0
            dr("WasteY") = 0.0
            dr("TotalK") = 3
            dr("TotalP") = 2.8
            dr("TotalY") = 0.0
            dr("GoldK") = 1
            dr("GoldP") = 1
            dr("GoldY") = 2.8
            dr("SaleInvoiceHeaderID") = "SI-28052014-0001"
            dr("SaleDate") = "28-5-2014"
            dr("CustomerID") = "00"
            dr("CustomerName") = "Mg Mg"
            dr("CustomerAddress") = "Yangon"
            dr("StaffID") = "00"
            dr("Staff") = "Ma Ma"
            dr("Remark") = ""
            dr("TotalAmount") = 974000
            dr("AddOrSub") = 0
            dr("NetAmount") = 974000
            dr("PromotionDiscount") = 0
            dr("PromotionAmount") = 0
            dr("DiscountAmount") = 0
            dr("PaidAmount") = 974000
            dr("BalanceAmount") = 0
            dr("TotalCharges") = 20000
            dt.Rows.Add(dr)
        ElseIf optOrderReceive.Checked Then

            dt.Columns.Add("OrderInvoiceID")
            dt.Columns.Add("ItemName")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("CustomerAddress")
            dt.Columns.Add("Width")
            dt.Columns.Add("WidthLength")
            dt.Columns.Add("OrderDate")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("StaffID")
            dt.Columns.Add("Staff")
            dt.Columns.Add("QTY")
            dt.Columns.Add("PayGoldQualityID")
            dt.Columns.Add("PayGoldQuality")
            dt.Columns.Add("Photo")
            dt.Columns.Add("DesignCharges")

            dt.Columns.Add("PayGoldK")
            dt.Columns.Add("PayGoldP")
            dt.Columns.Add("PayGoldY")
            dt.Columns.Add("EstimateGoldK")

            dt.Columns.Add("EstimateGoldP")
            dt.Columns.Add("EstimateGoldY")
            dt.Columns.Add("WasteK")
            dt.Columns.Add("WasteP")
            dt.Columns.Add("WasteY")
            dt.Columns.Add("GemsK")
            dt.Columns.Add("GemsP")
            dt.Columns.Add("GemsY")
            dt.Columns.Add("TotalK")
            dt.Columns.Add("TotalP")
            dt.Columns.Add("TotalY")
            dt.Columns.Add("PayGoldTK")
            dt.Columns.Add("PayGoldTG")
            dt.Columns.Add("EstimateGoldTK")
            dt.Columns.Add("EstimateGoldTG")

            dt.Columns.Add("WasteGoldTK")
            dt.Columns.Add("WasteGoldTG")
            dt.Columns.Add("GemsTK")
            dt.Columns.Add("GemsTG")
            dt.Columns.Add("TotalTK")
            dt.Columns.Add("TotalTG")
            dt.Columns.Add("GemsQTY")
            dt.Columns.Add("YOrCOrG")
            dt.Columns.Add("GemsAmount")
            dt.Columns.Add("OrderInvoiceGemsItemID")
            dt.Columns.Add("GemsCategoryID")
            dt.Columns.Add("GemsCategory")
            dt.Columns.Add("GemsName")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("AddOrSub")
            dt.Columns.Add("AdvanceAmount")
            dt.Columns.Add("SecondAdvanceAmount")
            dt.Columns.Add("ItemCategory")
            dt.Columns.Add("GoldPrice")
            dt.Columns.Add("GemsPrice")

            dt.Columns.Add("GoldQuality")
            dt.Columns.Add("IsShopGems")
            dt.Columns.Add("OrderRate")
            dt.Columns.Add("BalanceAmount")
            dt.Columns.Add("ItemGemsTK")
            dt.Columns.Add("ItemGemsTG")
            dt.Columns.Add("ItemGemsK")
            dt.Columns.Add("ItemGemsP")
            dt.Columns.Add("ItemGemsY")

            Dim dr As DataRow
            dr = dt.NewRow
            dr("OrderInvoiceID") = "OI-012014-0001"
            dr("ItemName") = "စိန်ဘယက်ကြိုး၊ ကျောက်နှစ်တန်း၊ကျောက်ကိုနှစ်မျိုးသုံးမည်။"
            dr("CustomerID") = "20140125-0002"
            dr("CustomerName") = "Ma Ma"
            dr("CustomerAddress") = "Yangon"
            dr("Width") = "_"
            dr("WidthLength") = "15"""
            dr("OrderDate") = "630000"
            dr("DueDate") = "2014-01-30 00:00:00.000"
            dr("StaffID") = "0001"
            dr("Staff") = "Ma Yu"
            dr("QTY") = "2"
            dr("PayGoldQualityID") = "04"
            dr("PayGoldQuality") = "16ပဲရည်"
            dr("Photo") = ""
            dr("DesignCharges") = 10000
            dr("EstimateGoldK") = 2
            dr("EstimateGoldP") = 0
            dr("EstimateGoldY") = 0.0
            dr("WasteK") = 0
            dr("WasteP") = 2
            dr("WasteY") = 0.0
            dr("GemsK") = 0
            dr("GemsP") = 5
            dr("GemsY") = 4.5
            dr("TotalK") = 2
            dr("TotalP") = 5
            dr("TotalY") = 4.5
            dr("PayGoldTK") = 0
            dr("PayGoldTG") = 8.3
            dr("EstimateGoldTK") = 0
            dr("EstimateGoldTG") = 33.2
            dr("WasteGoldTK") = 0
            dr("WasteGoldTG") = 2.075
            dr("GemsTK") = 0
            dr("GemsTG") = 5.771

            dr("TotalTK") = 0
            dr("TotalTG") = 38.971
            dr("GemsQTY") = 20
            dr("YOrCOrG") = "20ct"
            dr("GemsAmount") = 1172500
            dr("OrderInvoiceGemsItemID") = ""
            dr("GemsCategoryID") = ""
            dr("GemsCategory") = ""
            dr("GemsName") = ""
            dr("ItemCode") = 0
            dr("TotalAmount") = 2536250
            dr("AddOrSub") = 0
            dr("AdvanceAmount") = 1000000
            dr("SecondAdvanceAmount") = 500000
            dr("ItemCategory") = "_"
            dr("GoldPrice") = 1338750
            dr("GemsPrice") = 1172500
            dr("GoldQuality") = "16ပဲရည်"
            dr("IsShopGems") = 1
            dr("OrderRate") = 630000
            dr("BalanceAmount") = 0
            dr("ItemGemsTK") = 0
            dr("ItemGemsTG") = 0
            dr("ItemGemsK") = 0
            dr("ItemGemsP") = 9
            dr("ItemGemsY") = 9.6
            dt.Rows.Add(dr)
        ElseIf optPurchaseInvoice.Checked Then
            dt.Columns.Add("PurchaseGemID")
            dt.Columns.Add("GemsCategoryID")
            dt.Columns.Add("GemsCategory")
            dt.Columns.Add("GemsName")
            dt.Columns.Add("GemsK")
            dt.Columns.Add("GemsP")
            dt.Columns.Add("GemsY")
            dt.Columns.Add("GemsTK")
            dt.Columns.Add("GemsTG")
            dt.Columns.Add("YOrCOrG")
            dt.Columns.Add("GemTW")
            dt.Columns.Add("GemQTY")
            dt.Columns.Add("FixType")
            dt.Columns.Add("PurchaseRate")
            dt.Columns.Add("PurchaseDetailID")
            dt.Columns.Add("ForSaleID")

            dt.Columns.Add("BarcodeNo")
            dt.Columns.Add("ItemNameID")
            dt.Columns.Add("ItemName")
            dt.Columns.Add("Length")

            dt.Columns.Add("GoldQualityID")
            dt.Columns.Add("GoldQuality")
            dt.Columns.Add("ItemCategoryID")
            dt.Columns.Add("ItemCategory")
            dt.Columns.Add("QTY")
            dt.Columns.Add("GoldPrice")
            dt.Columns.Add("GemsPrice")
            dt.Columns.Add("IsFixPrice")
            dt.Columns.Add("IsDamage")
            dt.Columns.Add("IsChange")
            dt.Columns.Add("CurrentPrice")
            dt.Columns.Add("StrCurrentPrice")
            dt.Columns.Add("ItemTotalAmount")

            dt.Columns.Add("TotalTK")
            dt.Columns.Add("TotalTG")
            dt.Columns.Add("GoldTK")
            dt.Columns.Add("GoldTG")
            dt.Columns.Add("TotalGemTK")
            dt.Columns.Add("TotalGemTG")
            dt.Columns.Add("TotalK")
            dt.Columns.Add("TotalP")
            dt.Columns.Add("TotalY")

            dt.Columns.Add("GoldK")
            dt.Columns.Add("GoldP")
            dt.Columns.Add("GoldY")
            dt.Columns.Add("TotalGemK")
            dt.Columns.Add("TotalGemP")
            dt.Columns.Add("TotalGemY")
            dt.Columns.Add("PurchaseHeaderID")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("CustomerAddress")
            dt.Columns.Add("StaffID")
            dt.Columns.Add("Staff")
            dt.Columns.Add("Remark")
            dt.Columns.Add("TotalAmount")

            dt.Columns.Add("AddOrSub")
            dt.Columns.Add("NetAmount")
            dt.Columns.Add("PaidAmount")
            dt.Columns.Add("TotalGoldPrice")
            dt.Columns.Add("TotalGemsPrice")
            dt.Columns.Add("IsShop")
            dt.Columns.Add("ForSaleFixPrice")
            dt.Columns.Add("OldSaleAmount")
            dt.Columns.Add("WasteTK")
            dt.Columns.Add("WasteTG")

            dt.Columns.Add("PurchaseWastePercent")
            dt.Columns.Add("SaleRate")
            dt.Columns.Add("PurchaseDiscountAmount")

            Dim dr As DataRow
            dr = dt.NewRow
            dr("PurchaseGemID") = ""
            dr("GemsCategoryID") = ""
            dr("GemsCategory") = ""
            dr("GemsName") = ""
            dr("GemsK") = 0
            dr("GemsP") = 0
            dr("GemsY") = 0.0
            dr("GemsTK") = 0
            dr("GemsTG") = 0
            dr("YOrCOrG") = ""
            dr("GemTW") = 0
            dr("GemQTY") = 0
            dr("FixType") = ""
            dr("PurchaseRate") = 0
            dr("PurchaseDetailID") = "2016060001"
            dr("ForSaleID") = "01"

            dr("BarcodeNo") = "PN2014060001"
            dr("ItemNameID") = ""
            dr("ItemName") = "ပန်းလက်ကောက်"
            dr("Length") = ""
            dr("GoldQualityID") = ""
            dr("GoldQuality") = "15ပဲရည်"
            dr("ItemCategoryID") = ""
            dr("ItemCategory") = "လက်ကောက်"
            dr("QTY") = 1
            dr("GoldPrice") = 650000
            dr("GemsPrice") = 0
            dr("IsFixPrice") = 0
            dr("IsDamage") = 0
            dr("IsChange") = 0
            dr("CurrentPrice") = 650000
            dr("StrCurrentPrice") = 650000
            dr("GoldPrice") = 650000
            dr("GemsPrice") = 0
            dr("ItemTotalAmount") = 650000

            dr("TotalTK") = 1.0
            dr("TotalTG") = 16.6
            dr("GoldTK") = 1.0
            dr("GoldTG") = 16.6
            dr("TotalGemTK") = 0.0
            dr("TotalGemTG") = 0.0
            dr("TotalK") = 1
            dr("TotalP") = 0
            dr("TotalY") = 0.0

            dr("GoldK") = 1
            dr("GoldP") = 0
            dr("GoldY") = 0.0
            dr("TotalGemK") = 0
            dr("TotalGemP") = 0
            dr("TotalGemY") = 0.0
            dr("PurchaseHeaderID") = "2016060001"
            dr("PurchaseDate") = "2014-01-30 00:00:00.000"
            dr("CustomerID") = "01"
            dr("CustomerName") = "Ma Ma"
            dr("CustomerAddress") = "Yangon"
            dr("StaffID") = "01"
            dr("Staff") = "Mya Mya"
            dr("Remark") = ""
            dr("TotalAmount") = 650000
            dr("AddOrSub") = 0
            dr("NetAmount") = 650000
            dr("PaidAmount") = 650000
            dr("TotalGoldPrice") = 650000

            dr("TotalGemsPrice") = 0
            dr("IsShop") = 1
            dr("ForSaleFixPrice") = 0
            dr("OldSaleAmount") = 0
            dr("WasteTK") = 0.0
            dr("WasteTG") = 0.0
            dr("PurchaseWastePercent") = 0
            dr("SaleRate") = 0
            dr("PurchaseDiscountAmount") = 0
            dt.Rows.Add(dr)

        ElseIf optSaleGem.Checked Then
            dt.Columns.Add("SaleGemsID")
            dt.Columns.Add("SDate")
            dt.Columns.Add("StaffID")
            dt.Columns.Add("Staff")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("Customer")
            dt.Columns.Add("Address")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("AddOrSub")
            dt.Columns.Add("SaleRate")
            dt.Columns.Add("PaidAmount")
            dt.Columns.Add("GemsCategory")
            dt.Columns.Add("Qty")
            dt.Columns.Add("YOrCOrG")
            dt.Columns.Add("GemsName")
            dt.Columns.Add("Clarity")

            dt.Columns.Add("SizeMM")
            dt.Columns.Add("GemsTK")
            dt.Columns.Add("GemsTG")
            dt.Columns.Add("GemsTW")

            dt.Columns.Add("FixType")
            dt.Columns.Add("Amount")
            dt.Columns.Add("GemsK")
            dt.Columns.Add("GemsP")
            dt.Columns.Add("GemsY")
            dt.Columns.Add("DiscountAmount")
            dt.Columns.Add("PromotionDiscount")
            dt.Columns.Add("PromotionAmount")

            Dim dr As DataRow
            dr = dt.NewRow
            dr("SaleGemsID") = "SI-2014060001"
            dr("SDate") = "2014-01-30 00:00:00.000"
            dr("StaffID") = "01"
            dr("Staff") = "Ma Ma"
            dr("CustomerID") = "01"
            dr("Customer") = "Ma Ma"
            dr("Address") = "Yangon"
            dr("TotalAmount") = 500000
            dr("AddOrSub") = 0
            dr("SaleRate") = 50000
            dr("PaidAmount") = 500000
            dr("GemsCategory") = "Testing"
            dr("Qty") = 10
            dr("YOrCOrG") = "10ct"
            dr("GemsName") = "ကျောက်စိမ်း"
            dr("Clarity") = ""

            dr("SizeMM") = ""
            dr("GemsTK") = "0.5"
            dr("GemsTG") = "13.3"
            dr("GemsTW") = ""
            dr("FixType") = "By QTY"
            dr("Amount") = 500000
            dr("GemsK") = 0
            dr("GemsP") = 8
            dr("GemsY") = 0.0
            dr("DiscountAmount") = 0
            dr("PromotionDiscount") = 0
            dr("PromotionAmount") = 0
            dt.Rows.Add(dr)

        ElseIf optSaleVolume.Checked Then
            dt.Columns.Add("SalesVolumeDetailID")
            dt.Columns.Add("ForSaleID")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemNameID")
            dt.Columns.Add("ItemName")
            dt.Columns.Add("GoldQualityID")
            dt.Columns.Add("GoldQuality")
            dt.Columns.Add("ItemCategoryID")
            dt.Columns.Add("ItemCategory")
            dt.Columns.Add("Width")
            dt.Columns.Add("Length")
            dt.Columns.Add("FixPrice")
            dt.Columns.Add("IsFixPrice")
            dt.Columns.Add("Photo")
            dt.Columns.Add("SalesRate")
            dt.Columns.Add("ItemTotalAmount")

            dt.Columns.Add("ItemAddOrSub")
            dt.Columns.Add("ItemNetAmount")
            dt.Columns.Add("ItemTK")
            dt.Columns.Add("ItemTG")

            dt.Columns.Add("WasteTK")
            dt.Columns.Add("WasteTG")
            dt.Columns.Add("QTY")
            dt.Columns.Add("GoldPrice")
            dt.Columns.Add("TotalTK")
            dt.Columns.Add("TotalTG")
            dt.Columns.Add("ItemK")
            dt.Columns.Add("ItemP")
            dt.Columns.Add("ItemY")
            dt.Columns.Add("WasteK")
            dt.Columns.Add("WasteP")
            dt.Columns.Add("WasteY")
            dt.Columns.Add("TotalK")
            dt.Columns.Add("TotalP")
            dt.Columns.Add("TotalY")

            dt.Columns.Add("SalesVolumeID")
            dt.Columns.Add("SaleDate")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("CustomerAddress")
            dt.Columns.Add("StaffID")
            dt.Columns.Add("Staff")
            dt.Columns.Add("Remark")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("AddOrSub")
            dt.Columns.Add("NetAmount")
            dt.Columns.Add("PromotionDiscount")
            dt.Columns.Add("PromotionAmount")
            dt.Columns.Add("DiscountAmount")
            dt.Columns.Add("PaidAmount")
            dt.Columns.Add("BalanceAmount")

            Dim dr As DataRow
            dr = dt.NewRow
            dr("SalesVolumeDetailID") = "190620140001"
            dr("ForSaleID") = "20140127-0002"
            dr("ItemCode") = "NE0620140001"
            dr("ItemNameID") = 1
            dr("ItemName") = "တွဲလောင်းနားကပ်(ခရု)"
            dr("GoldQualityID") = 2
            dr("GoldQuality") = "၁၄ ပဲရည်"
            dr("ItemCategoryID") = "02"
            dr("ItemCategory") = "နားကပ်စုံ"
            dr("Width") = ""
            dr("Length") = ""
            dr("FixPrice") = 0
            dr("IsFixPrice") = 0
            dr("Photo") = ""
            dr("SalesRate") = "630000"
            dr("ItemTotalAmount") = 164883

            dr("ItemAddOrSub") = 0
            dr("ItemNetAmount") = 164883
            dr("ItemTK") = 0
            dr("ItemTG") = 4.15
            dr("WasteTK") = 0
            dr("WasteTG") = 0.195
            dr("QTY") = 1
            dr("GoldPrice") = 164883
            dr("TotalTK") = 0
            dr("TotalTG") = 4.345
            dr("ItemK") = 0
            dr("ItemP") = 4
            dr("ItemY") = 0.0
            dr("WasteK") = 0
            dr("WasteP") = 0
            dr("WasteY") = 1.5
            dr("TotalK") = 0
            dr("TotalP") = 4
            dr("TotalY") = 1.5

            dr("SalesVolumeID") = "SV-04032014-001"
            dr("SaleDate") = "2014-01-30 00:00:00.000"
            dr("CustomerID") = "01"
            dr("CustomerName") = "Ma Ma"
            dr("CustomerAddress") = "Yangon"
            dr("StaffID") = "02"
            dr("Staff") = "Ma Thu Zar"
            dr("Remark") = "_"
            dr("TotalAmount") = 164883
            dr("AddOrSub") = 0
            dr("NetAmount") = 164883
            dr("PromotionDiscount") = 0
            dr("PromotionAmount") = 0
            dr("DiscountAmount") = 0
            dr("PaidAmount") = 164883
            dr("BalanceAmount") = 0
            dt.Rows.Add(dr)
        ElseIf optOrderReturn.Checked Then

            dt.Columns.Add("OrderInvoiceDetailGemsID")
            dt.Columns.Add("GemsCategoryID")
            dt.Columns.Add("GemsCategory")
            dt.Columns.Add("GemsName")
            dt.Columns.Add("GemsK")
            dt.Columns.Add("GemsP")
            dt.Columns.Add("GemsY")
            dt.Columns.Add("GemsTK")
            dt.Columns.Add("GemsTG")
            dt.Columns.Add("YOrCOrG")
            dt.Columns.Add("GemsTW")
            dt.Columns.Add("Qty")
            dt.Columns.Add("Type")
            dt.Columns.Add("UnitPrice")
            dt.Columns.Add("GemsAmount")
            dt.Columns.Add("GemsRemark")

            dt.Columns.Add("OrderInvoiceDetailID")
            dt.Columns.Add("ForSaleID")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemNameID")

            dt.Columns.Add("ItemName")
            dt.Columns.Add("Length")
            dt.Columns.Add("GoldQualityID")
            dt.Columns.Add("GoldQuality")
            dt.Columns.Add("ItemCategoryID")
            dt.Columns.Add("ItemCategory")
            dt.Columns.Add("Width")
            dt.Columns.Add("FixPrice")
            dt.Columns.Add("IsFixPrice")
            dt.Columns.Add("DesignCharges")
            dt.Columns.Add("PlatingCharges")
            dt.Columns.Add("MountingCharges")
            dt.Columns.Add("WhiteCharges")
            dt.Columns.Add("Photo")
            dt.Columns.Add("SalesRate")

            dt.Columns.Add("GoldPrice")
            dt.Columns.Add("GemsPrice")
            dt.Columns.Add("ItemTotalAmount")
            dt.Columns.Add("ItemAddOrSub")
            dt.Columns.Add("ItemNetAmount")
            dt.Columns.Add("ItemTK")
            dt.Columns.Add("ItemTG")
            dt.Columns.Add("TotalGemsTK")
            dt.Columns.Add("TotalGemsTG")
            dt.Columns.Add("WasteTK")
            dt.Columns.Add("WasteTG")
            dt.Columns.Add("TotalTK")
            dt.Columns.Add("TotalTG")
            dt.Columns.Add("GoldTK")
            dt.Columns.Add("GoldTG")

            dt.Columns.Add("ItemK")
            dt.Columns.Add("ItemP")
            dt.Columns.Add("ItemY")
            dt.Columns.Add("TotalGemsK")
            dt.Columns.Add("TotalGemsP")
            dt.Columns.Add("TotalGemsY")
            dt.Columns.Add("WasteK")
            dt.Columns.Add("WasteP")
            dt.Columns.Add("WasteY")
            dt.Columns.Add("TotalK")
            dt.Columns.Add("TotalP")
            dt.Columns.Add("TotalY")
            dt.Columns.Add("GoldK")
            dt.Columns.Add("GoldP")
            dt.Columns.Add("GoldY")
            dt.Columns.Add("OrderInvoiceID")

            dt.Columns.Add("OrderDate")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("CustomerAddress")

            dt.Columns.Add("StaffID")
            dt.Columns.Add("Staff")
            dt.Columns.Add("Remark")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("AddOrSub")
            dt.Columns.Add("NetAmount")
            dt.Columns.Add("AdvanceAmount")
            dt.Columns.Add("SecondAdvanceAmount")
            dt.Columns.Add("TotalAdvanceAmount")
            dt.Columns.Add("FromGoldAmount")
            dt.Columns.Add("DiscountAmount")
            dt.Columns.Add("PaidAmount")
            dt.Columns.Add("BalanceAmount")
            dt.Columns.Add("OrderRetrieveDate")
            dt.Columns.Add("TotalCharges")

            Dim dr As DataRow
            dr = dt.NewRow
            dr("OrderInvoiceDetailGemsID") = ""
            dr("GemsCategoryID") = ""
            dr("GemsCategory") = ""
            dr("GemsName") = ""
            dr("GemsK") = 0
            dr("GemsP") = 0
            dr("GemsY") = 0.0
            dr("GemsTK") = 0.0
            dr("GemsTG") = 0.0
            dr("YOrCOrG") = ""
            dr("GemsTW") = 0.0
            dr("Qty") = 0
            dr("Type") = ""
            dr("UnitPrice") = 0
            dr("GemsAmount") = 0
            dr("GemsRemark") = ""

            dr("OrderInvoiceDetailID") = "01-20140204-0002"
            dr("ForSaleID") = "20140204-0002"
            dr("ItemCode") = "NE11031401"
            dr("ItemNameID") = "20140227-0003"
            dr("ItemName") = "တစ်လမ်းမောင်းကြိုး"
            dr("Length") = ""
            dr("GoldQualityID") = "01"
            dr("GoldQuality") = "၁၅ ပဲရည်"
            dr("ItemCategoryID") = "01"
            dr("ItemCategory") = "ကြိုး"
            dr("Width") = ""
            dr("FixPrice") = 0
            dr("IsFixPrice") = 0
            dr("DesignCharges") = 0
            dr("PlatingCharges") = 0
            dr("MountingCharges") = 0
            dr("WhiteCharges") = 0
            dr("Photo") = ""
            dr("SalesRate") = 630000

            dr("GoldPrice") = 669375
            dr("GemsPrice") = 0
            dr("ItemTotalAmount") = 669375
            dr("ItemAddOrSub") = 0
            dr("ItemNetAmount") = 669375
            dr("ItemTK") = 1.0
            dr("ItemTG") = 16.6
            dr("TotalGemsTK") = 0.0
            dr("TotalGemsTG") = 0.0
            dr("WasteTK") = 0.0625
            dr("WasteTG") = 1.0375
            dr("TotalTK") = 1.0625
            dr("TotalTG") = 17.6375
            dr("GoldTK") = 1.0
            dr("GoldTG") = 16.6
            dr("ItemK") = 1

            dr("ItemP") = 0
            dr("ItemY") = 0.0
            dr("TotalGemsK") = 0
            dr("TotalGemsP") = 0
            dr("TotalGemsY") = 0.0
            dr("WasteK") = 0
            dr("WasteP") = 1
            dr("WasteY") = 0.0
            dr("TotalK") = 1
            dr("TotalP") = 1
            dr("TotalY") = 0.0
            dr("GoldK") = 1
            dr("GoldP") = 0
            dr("GoldY") = 0.0
            dr("OrderInvoiceID") = "OI-032014-0001"
            dr("OrderDate") = "2014-03-10 00:00:00.000"

            dr("CustomerID") = "20140222-0001"
            dr("CustomerName") = "Aye Aye"
            dr("CustomerAddress") = "Yangon"
            dr("StaffID") = "02"
            dr("Staff") = "Ma Ma"
            dr("Remark") = ""
            dr("TotalAmount") = 669375
            dr("AddOrSub") = 375
            dr("NetAmount") = 669000
            dr("AdvanceAmount") = 300000
            dr("SecondAdvanceAmount") = 0
            dr("TotalAdvanceAmount") = 300000
            dr("FromGoldAmount") = 0
            dr("DiscountAmount") = 0
            dr("PaidAmount") = 369000
            dr("BalanceAmount") = 0
            dr("OrderRetrieveDate") = "2014-03-20 00:00:00.000"
            dr("TotalCharges") = 0 
            dt.Rows.Add(dr)
        ElseIf optPurchaseGem.Checked Then
            dt.Columns.Add("PurchaseDetailID")
            dt.Columns.Add("PurchaseHeaderID")
            dt.Columns.Add("GemsCategoryID")
            dt.Columns.Add("CurrentPrice")
            dt.Columns.Add("QTY")
            dt.Columns.Add("YOrCOrG")
            dt.Columns.Add("ItemName")
            dt.Columns.Add("TotalGemTK")
            dt.Columns.Add("TotalGemTG")
            dt.Columns.Add("GemsK")
            dt.Columns.Add("GemsP")
            dt.Columns.Add("GemsY")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCategory")
            dt.Columns.Add("AllTotalAmount")
            dt.Columns.Add("AllAddOrSub")

            dt.Columns.Add("AllPaidAmount")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("Address")

            dt.Columns.Add("StaffID")
            dt.Columns.Add("Staff")

            Dim dr As DataRow
            dr = dt.NewRow
            dr("PurchaseDetailID") = "01-20140127-0002"
            dr("PurchaseHeaderID") = "PI-012014-0002"
            dr("GemsCategoryID") = "04"
            dr("CurrentPrice") = 500000
            dr("QTY") = "50"
            dr("YOrCOrG") = "4ct"
            dr("ItemName") = "စိန်လုံးသန့်"
            dr("TotalGemTK") = 0.0484375
            dr("TotalGemTG") = 0.8040625
            dr("GemsK") = 0
            dr("GemsP") = 0
            dr("GemsY") = 6.2
            dr("TotalAmount") = 2200000
            dr("ItemCategory") = "စိန်(၅၀)လုံးစီး"
            dr("AllTotalAmount") = 2200000
            dr("AllAddOrSub") = 0

            dr("AllPaidAmount") = 2200000
            dr("PurchaseDate") = "2014-01-27 00:00:00.000"
            dr("CustomerName") = "Aye Aye Mar"
            dr("Address") = "Yangon"
            dr("StaffID") = "01"
            dr("Staff") = "Ma Ma"
            dt.Rows.Add(dr)
        End If


        Return dt
    End Function


    Private Function CreateTemplate(ByVal FileName As String, ByVal Type As String) As Boolean
        Dim CustomFileName As String = FileName & ".rdl"
        Dim BkFileName As String = ""
        BkFileName = FileName & " - Backup"
        If Type = "A4" Then
            If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template.rdl") Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                If FileName.Contains(".rdl") Then
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName.Substring(0, FileName.LastIndexOf("."))) Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & CustomFileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                Else
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & CustomFileName) Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                End If
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template.rdl") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template.rdl", My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & CustomFileName)
                    'Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & CustomFileName)
                    ''rtew - Backup

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & BkFileName & ".rdl")
                    End If

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & BkFileName & ".rdl")
                    End If
                ElseIf File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template", My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & CustomFileName)

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & BkFileName & ".rdl")
                    End If
                End If
            End If

        ElseIf Type = "OrderA4" Then
            If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\OrderA4Template.rdl") Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                If FileName.Contains(".rdl") Then
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & FileName.Substring(0, FileName.LastIndexOf("."))) Or File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & CustomFileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                Else
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & CustomFileName) Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                End If
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\OrderA4Template.rdl") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\OrderA4Template.rdl", My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & CustomFileName)

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & BkFileName & ".rdl")
                    End If

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & BkFileName & ".rdl")
                    End If
                ElseIf File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\OrderA4Template") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\OrderA4Template", My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & CustomFileName)

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & BkFileName & ".rdl")
                    End If
                End If

            End If

        ElseIf Type = "PurchaseA4" Then
            If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\PurchaseA4Template.rdl") Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                If FileName.Contains(".rdl") Then
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & FileName.Substring(0, FileName.LastIndexOf("."))) Or File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & CustomFileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                Else
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & CustomFileName) Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                End If
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\PurchaseA4Template.rdl") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\PurchaseA4Template.rdl", My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & CustomFileName)

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & BkFileName & ".rdl")
                    End If

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & BkFileName & ".rdl")
                    End If
                ElseIf File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\PurchaseA4Template") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\PurchaseA4Template", My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & CustomFileName)

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & BkFileName & ".rdl")
                    End If
                End If
            End If
        ElseIf Type = "SaleGemA4" Then
            If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\SaleGemA4Template.rdl") Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                If FileName.Contains(".rdl") Then
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & FileName.Substring(0, FileName.LastIndexOf("."))) Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & CustomFileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                Else
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & CustomFileName) Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                End If
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\SaleGemA4Template.rdl") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\SaleGemA4Template.rdl", My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & CustomFileName)

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & BkFileName & ".rdl")
                    End If

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & BkFileName & ".rdl")
                    End If
                ElseIf File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\SaleGemA4Template") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\SaleGemA4Template", My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & CustomFileName)

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & BkFileName & ".rdl")
                    End If
                End If
            End If
        ElseIf Type = "SaleVolumeA4" Then
            If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\SaleVolumeA4Template.rdl") Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                If FileName.Contains(".rdl") Then
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & FileName.Substring(0, FileName.LastIndexOf("."))) Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & CustomFileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                Else
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & CustomFileName) Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                End If
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\SaleVolumeA4Template.rdl") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\SaleVolumeA4Template.rdl", My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & CustomFileName)

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & BkFileName & ".rdl")
                    End If

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & BkFileName & ".rdl")
                    End If
                ElseIf File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\SaleVolumeA4Template") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\SaleVolumeA4Template", My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & CustomFileName)

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & BkFileName & ".rdl")
                    End If
                End If
            End If
        ElseIf Type = "OrderReturnA4" Then
            If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\OrderReturnA4Template.rdl") Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                If FileName.Contains(".rdl") Then
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & FileName.Substring(0, FileName.LastIndexOf("."))) Or File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & CustomFileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                Else
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & CustomFileName) Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                End If
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\OrderReturnA4Template.rdl") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\OrderReturnA4Template.rdl", My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & CustomFileName)

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & BkFileName & ".rdl")
                    End If

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & BkFileName & ".rdl")
                    End If
                ElseIf File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\OrderReturnA4Template") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\OrderReturnA4Template", My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & CustomFileName)

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & BkFileName & ".rdl")
                    End If
                End If
            End If

        ElseIf Type = "PurchaseGemA4" Then
            If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\PurchaseGemA4Template.rdl") Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\A4Template") Then
                If FileName.Contains(".rdl") Then
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & FileName.Substring(0, FileName.LastIndexOf("."))) Or File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & CustomFileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                Else
                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & CustomFileName) Then 'Or File.Exists(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & FileName) Then
                        MsgBox("A file with the name you specified already exits,please specifiy another file name?", MsgBoxStyle.Information, "File Already Exits")
                        Return False
                    End If
                End If
                If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\PurchaseGemA4Template.rdl") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\PurchaseGemA4Template.rdl", My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & CustomFileName)

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & BkFileName & ".rdl")
                    End If

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & BkFileName & ".rdl")
                    End If
                ElseIf File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\PurchaseGemA4Template") Then
                    FileCopy(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\PurchaseGemA4Template", My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & CustomFileName)

                    Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & CustomFileName)
                    p.WaitForExit()

                    If File.Exists(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & BkFileName & ".rdl") Then
                        File.Delete(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & BkFileName & ".rdl")
                    End If
                End If
            End If
        End If
        Return True

    End Function

    Private Sub cboChangeTemplate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboChangeTemplate.SelectedIndexChanged
        Dim tmpstrA4() As String
        If IsSelect = True Then
            If optSaleInvoice.Checked Then
                tmpstrA4 = GetRDLCFileName("A4", "")
                If tmpstrA4 IsNot Nothing Then
                    For i As Integer = 0 To tmpstrA4.Length - 1
                        If tmpstrA4(i) = cboChangeTemplate.Text Then
                            If MsgBox("Are you sure you want to change this Report Item?", MsgBoxStyle.YesNo, "Confirm Change Item") = MsgBoxResult.Yes Then
                                ChangeFileName(tmpstrA4(i), "A4")
                                ShowTemplate()
                            End If
                        End If
                    Next
                End If
            ElseIf optOrderReceive.Checked Then
                tmpstrA4 = GetRDLCFileName("OrderA4", "")
                If tmpstrA4 IsNot Nothing Then
                    For i As Integer = 0 To tmpstrA4.Length - 1
                        If tmpstrA4(i) = cboChangeTemplate.Text Then
                            If MsgBox("Are you sure you want to change this Report Item?", MsgBoxStyle.YesNo, "Confirm Change Item") = MsgBoxResult.Yes Then
                                ChangeFileName(tmpstrA4(i), "OrderA4")
                                ShowTemplate()
                            End If
                        End If
                    Next
                End If
            ElseIf optPurchaseInvoice.Checked Then
                tmpstrA4 = GetRDLCFileName("PurchaseA4", "")
                If tmpstrA4 IsNot Nothing Then
                    For i As Integer = 0 To tmpstrA4.Length - 1
                        If tmpstrA4(i) = cboChangeTemplate.Text Then
                            If MsgBox("Are you sure you want to change this Report Item?", MsgBoxStyle.YesNo, "Confirm Change Item") = MsgBoxResult.Yes Then
                                ChangeFileName(tmpstrA4(i), "PurchaseA4")
                                ShowTemplate()
                            End If
                        End If
                    Next
                End If
            ElseIf optSaleGem.Checked Then
                tmpstrA4 = GetRDLCFileName("SaleGemA4", "")
                If tmpstrA4 IsNot Nothing Then
                    For i As Integer = 0 To tmpstrA4.Length - 1
                        If tmpstrA4(i) = cboChangeTemplate.Text Then
                            If MsgBox("Are you sure you want to change this Report Item?", MsgBoxStyle.YesNo, "Confirm Change Item") = MsgBoxResult.Yes Then
                                ChangeFileName(tmpstrA4(i), "SaleGemA4")
                                ShowTemplate()
                            End If
                        End If
                    Next
                End If
            ElseIf optSaleVolume.Checked Then
                tmpstrA4 = GetRDLCFileName("SaleVolumeA4", "")
                If tmpstrA4 IsNot Nothing Then
                    For i As Integer = 0 To tmpstrA4.Length - 1
                        If tmpstrA4(i) = cboChangeTemplate.Text Then
                            If MsgBox("Are you sure you want to change this Report Item?", MsgBoxStyle.YesNo, "Confirm Change Item") = MsgBoxResult.Yes Then
                                ChangeFileName(tmpstrA4(i), "SaleVolumeA4")
                                ShowTemplate()
                            End If
                        End If
                    Next
                End If
            ElseIf optOrderReturn.Checked Then
                tmpstrA4 = GetRDLCFileName("OrderReturnA4", "")
                If tmpstrA4 IsNot Nothing Then
                    For i As Integer = 0 To tmpstrA4.Length - 1
                        If tmpstrA4(i) = cboChangeTemplate.Text Then
                            If MsgBox("Are you sure you want to change this Report Item?", MsgBoxStyle.YesNo, "Confirm Change Item") = MsgBoxResult.Yes Then
                                ChangeFileName(tmpstrA4(i), "OrderReturnA4")
                                ShowTemplate()
                            End If
                        End If
                    Next
                End If
            ElseIf optPurchaseGem.Checked Then
                tmpstrA4 = GetRDLCFileName("PurchaseGemA4", "")
                If tmpstrA4 IsNot Nothing Then
                    For i As Integer = 0 To tmpstrA4.Length - 1
                        If tmpstrA4(i) = cboChangeTemplate.Text Then
                            If MsgBox("Are you sure you want to change this Report Item?", MsgBoxStyle.YesNo, "Confirm Change Item") = MsgBoxResult.Yes Then
                                ChangeFileName(tmpstrA4(i), "PurchaseGemA4")
                                ShowTemplate()
                            End If
                        End If
                    Next
                End If
            End If
       
        End If
    End Sub

    Private Function ChangeFileName(ByVal FileName As String, ByVal Type As String)
        Dim sb As New StringBuilder
        If Type = "A4" Then
            Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            For Each dra In diar
                If dra.Name = FileName Then
                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("A4ReportName").Value = dra.Name
                    config.Save()
                End If
            Next
        ElseIf Type = "OrderA4" Then
            Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            For Each dra In diar
                If dra.Name = FileName Then
                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("OrderA4ReportName").Value = dra.Name
                    config.Save()
                End If
            Next
        ElseIf Type = "PurchaseA4" Then
            Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            For Each dra In diar
                If dra.Name = FileName Then
                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("PurchaseA4ReportName").Value = dra.Name
                    config.Save()
                End If
            Next

        ElseIf Type = "SaleGemA4" Then
            Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            For Each dra In diar
                If dra.Name = FileName Then
                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("SaleGemA4ReportName").Value = dra.Name
                    config.Save()
                End If
            Next

        ElseIf Type = "SaleVolumeA4" Then
            Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            For Each dra In diar
                If dra.Name = FileName Then
                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("SaleVolumeA4ReportName").Value = dra.Name
                    config.Save()
                End If
            Next

        ElseIf Type = "OrderReturnA4" Then
            Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            For Each dra In diar
                If dra.Name = FileName Then
                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("OrderReturnA4ReportName").Value = dra.Name
                    config.Save()
                End If
            Next

        ElseIf Type = "PurchaseGemA4" Then
            Dim di As New IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\")
            Dim diar As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            For Each dra In diar
                If dra.Name = FileName Then
                    Dim FileMap As ExeConfigurationFileMap

                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    config.AppSettings.Settings("PurchaseGemA4ReportName").Value = dra.Name
                    config.Save()
                End If
            Next
        End If
    End Function

    ''Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    ''    If Global_UserLevelID = 3 Then Exit Sub
    ''    Dim objsettinginfo As CommonInfo.SettingInfo

    ''    'If optSaleInvoice.Checked Then
    ''    objsettinginfo = getsettingdata("PrinterType", "PrinterType", 1)

    ''    'ElseIf optOrderReceive.Checked Then
    ''    'objsettinginfo = getsettingdata("PrinterType", "PrinterType", 2)
    ''    'End If

    ''    _settingcontroller.SaveSetting(objsettinginfo)


    ''    objsettinginfo = getsettingdata(chkDirectPrint.Tag.ToString, chkDirectPrint.Tag.ToString, IIf(chkDirectPrint.Checked, 1, 0))
    ''    _settingcontroller.SaveSetting(objsettinginfo)

    ''    MessageBox.Show("Save Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

    ''    Dim FileMap As ExeConfigurationFileMap
    ''    FileMap = New ExeConfigurationFileMap
    ''    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
    ''    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

    ''    config.AppSettings.Settings("PrinterType").Value = 1

    ''    If optSaleInvoice.Checked Then
    ''        config.AppSettings.Settings("A4ReportName").Value = cboChangeTemplate.Text.Trim
    ''    ElseIf optOrderReceive.Checked Then
    ''        config.AppSettings.Settings("OrderA4ReportName").Value = cboChangeTemplate.Text.Trim
    ''    ElseIf optPurchaseInvoice.Checked Then
    ''        config.AppSettings.Settings("PurchaseA4ReportName").Value = cboChangeTemplate.Text.Trim
    ''    ElseIf optSaleGem.Checked Then
    ''        config.AppSettings.Settings("SaleGemA4ReportName").Value = cboChangeTemplate.Text.Trim
    ''    ElseIf optSaleVolume.Checked Then
    ''        config.AppSettings.Settings("SaleVolumeA4ReportName").Value = cboChangeTemplate.Text.Trim
    ''    ElseIf optOrderReturn.Checked Then
    ''        config.AppSettings.Settings("OrderReturnA4ReportName").Value = cboChangeTemplate.Text.Trim
    ''    ElseIf optPurchaseGem.Checked Then
    ''        config.AppSettings.Settings("PurchaseGemA4ReportName").Value = cboChangeTemplate.Text.Trim
    ''    End If
    ''    'ElseIf optOrderReceive.Checked Then
    ''    '    config.AppSettings.Settings("PrinterType").Value = 2
    ''    config.Save()
    ''End Sub

    Private Function getsettingdata(ByVal KeyName As String, ByVal Description As String, ByVal KeyValue As String) As CommonInfo.SettingInfo
        Dim objgetsinfo As New CommonInfo.SettingInfo("", "", "")
        With objgetsinfo

            .KeyName = KeyName
            .Description = Description
            .KeyValue = KeyValue


        End With
        Return objgetsinfo


    End Function


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim objsetting As New SettingInfo("", "", "")
        'objsetting = _settingcontroller.getdatabykeyname("PrinterType")
        Dim tmpfile As String = ""
        'If objsetting.KeyValue = "1" Then
        If optSaleInvoice.Checked Then
            tmpfile = GetPrinterType("A4")
            If tmpfile <> "A4Template.rdl" Then
                Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & tmpfile)
                p.WaitForExit()
                ShowTemplate()
            Else
                MsgBox("You cannot edit default template,please create another template", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        ElseIf optOrderReceive.Checked Then
            tmpfile = GetPrinterType("OrderA4")
            If tmpfile <> "OrderA4Template.rdl" Then
                Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & tmpfile)
                p.WaitForExit()
                ShowTemplate()
            Else
                MsgBox("You cannot edit default template,please create another template", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        ElseIf optPurchaseInvoice.Checked Then
            tmpfile = GetPrinterType("PurchaseA4")
            If tmpfile <> "PurchaseA4Template.rdl" Then
                Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & tmpfile)
                p.WaitForExit()
                ShowTemplate()
            Else
                MsgBox("You cannot edit default template,please create another template", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

        ElseIf optSaleGem.Checked Then
            tmpfile = GetPrinterType("SaleGemA4")
            If tmpfile <> "SaleGemA4Template.rdl" Then
                Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & tmpfile)
                p.WaitForExit()
                ShowTemplate()
            Else
                MsgBox("You cannot edit default template,please create another template", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

        ElseIf optSaleVolume.Checked Then
            tmpfile = GetPrinterType("SaleVolumeA4")
            If tmpfile <> "SaleVolumeA4Template.rdl" Then
                Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & tmpfile)
                p.WaitForExit()
                ShowTemplate()
            Else
                MsgBox("You cannot edit default template,please create another template", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

        ElseIf optOrderReturn.Checked Then
            tmpfile = GetPrinterType("OrderReturnA4")
            If tmpfile <> "OrderReturnA4Template.rdl" Then
                Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & tmpfile)
                p.WaitForExit()
                ShowTemplate()
            Else
                MsgBox("You cannot edit default template,please create another template", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

        ElseIf optPurchaseGem.Checked Then
            tmpfile = GetPrinterType("PurchaseGemA4")
            If tmpfile <> "PurchaseGemA4Template.rdl" Then
                Dim p As Process = Process.Start(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & tmpfile)
                p.WaitForExit()
                ShowTemplate()
            Else
                MsgBox("You cannot edit default template,please create another template", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        End If

        ' End If
    End Sub

    Private Function GetPrinterType(ByVal Type As String) As String
        Dim tmpstr() As String
        Dim _tmp As String = ""

        Dim FileMap As ExeConfigurationFileMap

        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String
        'If Type = "A4" Then
        If Type = "A4" Then
            ReportName = config.AppSettings.Settings("A4ReportName").Value()
        ElseIf Type = "OrderA4" Then
            ReportName = config.AppSettings.Settings("OrderA4ReportName").Value()
        ElseIf Type = "PurchaseA4" Then
            ReportName = config.AppSettings.Settings("PurchaseA4ReportName").Value()
        ElseIf Type = "SaleGemA4" Then
            ReportName = config.AppSettings.Settings("SaleGemA4ReportName").Value()
        ElseIf Type = "SaleVolumeA4" Then
            ReportName = config.AppSettings.Settings("SaleVolumeA4ReportName").Value()
        ElseIf Type = "OrderReturnA4" Then
            ReportName = config.AppSettings.Settings("OrderReturnA4ReportName").Value()
        ElseIf Type = "PurchaseGemA4" Then
            ReportName = config.AppSettings.Settings("PurchaseGemA4ReportName").Value()
        End If


        'End If

        tmpstr = GetRDLCFileName(Type, "")
        If tmpstr IsNot Nothing Then
            If tmpstr.Length > 0 Then
                For i As Integer = 0 To tmpstr.Length - 1
                    Dim tmpReportName As String = tmpstr(i)
                    If tmpReportName = ReportName Then
                        _tmp = tmpReportName
                    End If
                Next
            End If
        End If
        Return _tmp
    End Function

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ' Dim objsetting As New SettingInfo("", "", "")
        Dim Template As String
        Dim path As String
        'objsetting = _settingcontroller.getdatabykeyname("PrinterType")
        'If objsetting.KeyValue = "1" Then
        If optSaleInvoice.Checked Then
            Template = ConfigurationManager.AppSettings("A4ReportName")
        ElseIf optOrderReceive.Checked Then
            Template = ConfigurationManager.AppSettings("OrderA4ReportName")
        ElseIf optPurchaseInvoice.Checked Then
            Template = ConfigurationManager.AppSettings("PurchaseA4ReportName")
        ElseIf optSaleGem.Checked Then
            Template = ConfigurationManager.AppSettings("SaleGemA4ReportName")
        ElseIf optSaleVolume.Checked Then
            Template = ConfigurationManager.AppSettings("SaleVolumeA4ReportName")
        ElseIf optOrderReturn.Checked Then
            Template = ConfigurationManager.AppSettings("OrderReturnA4ReportName")
        ElseIf optPurchaseGem.Checked Then
            Template = ConfigurationManager.AppSettings("PurchaseGemA4ReportName")
        End If
        'Else
        'Template = ConfigurationManager.AppSettings("DMReportName")
        'End If

        SaveFileDialog.FileName = Template
        SaveFileDialog.Filter = ""
        If SaveFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            path = SaveFileDialog.FileName

            Dim fiFile As New System.IO.FileInfo(path.Trim)
            If fiFile.Exists = True Then
                MsgBox("Please change Export file name!", MsgBoxStyle.Exclamation, "TMS")
                Exit Sub
            Else
                Try
                    ' If objsetting.KeyValue = "1" Then
                    If optSaleInvoice.Checked Then
                        FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & Template, path)
                    ElseIf optOrderReceive.Checked Then
                        FileCopy(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & Template, path)
                    ElseIf optPurchaseInvoice.Checked Then
                        FileCopy(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & Template, path)
                    ElseIf optSaleGem.Checked Then
                        FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & Template, path)
                    ElseIf optSaleVolume.Checked Then
                        FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & Template, path)
                    ElseIf optOrderReturn.Checked Then
                        FileCopy(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & Template, path)
                    ElseIf optPurchaseGem.Checked Then
                        FileCopy(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & Template, path)
                    End If

                    'ElseIf objsetting.KeyValue = "2" Then
                    'FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4VRDL\" & Template, path)

                    'Else
                    'FileCopy(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceDotMatrixRDL\" & Template, path)

                    'End If

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                MsgBox("Export Successfully!", MsgBoxStyle.Information, "TMS")

            End If
        Else
            path = ""
        End If
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim path As String
        OpenFileDialog.FileName = ""
        If OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            path = OpenFileDialog.FileName
            Dim fiFile As New System.IO.FileInfo(path.Trim)
            If fiFile.Exists = True Then
                'Dim objsetting As New SettingInfo("", "", "")
                'objsetting = _settingcontroller.getdatabykeyname("PrinterType")
                ' If objsetting.KeyValue = "1" Then

                If OpenFileDialog.SafeFileName.Contains(".rdl") Then
                    If optSaleInvoice.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & OpenFileDialog.SafeFileName)
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & OpenFileDialog.SafeFileName)
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If
                    ElseIf optOrderReceive.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & OpenFileDialog.SafeFileName)
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & OpenFileDialog.SafeFileName)
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If
                    ElseIf optPurchaseInvoice.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & OpenFileDialog.SafeFileName)
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & OpenFileDialog.SafeFileName)
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optSaleGem.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & OpenFileDialog.SafeFileName)
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & OpenFileDialog.SafeFileName)
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optSaleVolume.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & OpenFileDialog.SafeFileName)
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & OpenFileDialog.SafeFileName)
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optOrderReturn.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & OpenFileDialog.SafeFileName)
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & OpenFileDialog.SafeFileName)
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optPurchaseGem.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & OpenFileDialog.SafeFileName)
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & OpenFileDialog.SafeFileName)
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If
                    End If

                    cboChangeTemplate.Items.AddRange(New String() {OpenFileDialog.SafeFileName})


                Else
                    If optSaleInvoice.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\SaleInvoiceA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If
                    ElseIf optOrderReceive.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\OrderReceiveA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If
                    ElseIf optPurchaseInvoice.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\PurchaseInvoiceA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optSaleGem.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\SaleGemA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optSaleVolume.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\SaleVolumeA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optOrderReturn.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\OrderReturnA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If

                    ElseIf optPurchaseGem.Checked Then
                        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        If myFile.Exists = False Then
                            FileCopy(path, My.Application.Info.DirectoryPath & "\Report\PurchaseGemA4RDL\" & OpenFileDialog.SafeFileName & ".rdl")
                        Else
                            MsgBox("Please change Import file name!", MsgBoxStyle.Exclamation, "TMS")
                            Exit Sub
                        End If
                    End If
                    cboChangeTemplate.Items.AddRange(New String() {OpenFileDialog.SafeFileName & ".rdl"})
                End If
            End If

            MsgBox("Import Template Successfully!", MsgBoxStyle.Information, "TMS")
        End If
        'End If
    End Sub

    Private Sub optOrderReceive_CheckedChanged(sender As Object, e As EventArgs) Handles optOrderReceive.CheckedChanged
        If optOrderReceive.Checked Then
            Dim tmpstr() As String
            Dim FileMap As ExeConfigurationFileMap
            Dim DMReport As String = ""
            RDLCTemplate()      
            GetRDLCFileName("OrderA4", "")
            tmpstr = GetRDLCFileName("OrderA4", "")

            cboChangeTemplate.Items.Clear()
            If tmpstr IsNot Nothing Then
                If tmpstr.Length > 0 Then
                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                    If optSaleInvoice.Checked Then
                        DMReport = config.AppSettings.Settings("A4ReportName").Value
                    Else
                        DMReport = config.AppSettings.Settings("OrderA4ReportName").Value
                    End If
                    For i As Integer = 0 To tmpstr.Length - 1
                        cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                    Next
                End If
            End If
            IsSelect = False
            cboChangeTemplate.Text = DMReport.ToString
            IsSelect = True
            'ShowData()

            Me.RptViewer.RefreshReport()
            'Dim dt As New DataTable
            'Dim dr As DataRow()
            'dt = _settingcontroller.GetKeyName()

            FileMap = New ExeConfigurationFileMap
            FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
            config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
            Dim ReportName As String

            'dr = dt.Select("KeyName='PrinterType'")
            'If dr.Length > 0 Then
            '    If dr(0).Item("KeyValue") = "1" Then
           

            ReportName = config.AppSettings.Settings("OrderA4ReportName").Value()
            Preview("OrderA4", ReportName)
            Me.RptViewer.Refresh()       

        End If
    End Sub

    Private Sub optSaleInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles optSaleInvoice.CheckedChanged
        If optSaleInvoice.Checked And IsSelect = True Then
            Dim tmpstr() As String
            Dim FileMap As ExeConfigurationFileMap
            Dim DMReport As String = ""
            RDLCTemplate()

            GetRDLCFileName("A4", "")
            tmpstr = GetRDLCFileName("A4", "")
            cboChangeTemplate.Items.Clear()
        If tmpstr IsNot Nothing Then
            If tmpstr.Length > 0 Then
                FileMap = New ExeConfigurationFileMap
                FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                If optSaleInvoice.Checked Then
                    DMReport = config.AppSettings.Settings("A4ReportName").Value
                Else
                    DMReport = config.AppSettings.Settings("OrderA4ReportName").Value
                End If
                For i As Integer = 0 To tmpstr.Length - 1
                    cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                Next
            End If
        End If
        IsSelect = False
        cboChangeTemplate.Text = DMReport.ToString
        IsSelect = True
        'ShowData()

            Me.RptViewer.RefreshReport()

            'Dim dt As New DataTable
            'Dim dr As DataRow()
            'dt = _settingcontroller.GetKeyName()

        FileMap = New ExeConfigurationFileMap
        FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
        config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
        Dim ReportName As String
            'If optSaleInvoice.Checked Then
            ReportName = config.AppSettings.Settings("A4ReportName").Value()
            Preview("bya4", ReportName)
            Me.RptViewer.Refresh()
            'ElseIf optOrderReceive.Checked Then
            '    ReportName = config.AppSettings.Settings("OrderA4ReportName").Value()
            '    Preview("OrderA4", ReportName)
            'End If

        End If
    End Sub

    Private Sub optPurchaseInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles optPurchaseInvoice.CheckedChanged
        If optPurchaseInvoice.Checked Then
            Dim tmpstr() As String
            Dim FileMap As ExeConfigurationFileMap
            Dim DMReport As String = ""
            RDLCTemplate()

            GetRDLCFileName("PurchaseA4", "")
            tmpstr = GetRDLCFileName("PurchaseA4", "")

            cboChangeTemplate.Items.Clear()
            If tmpstr IsNot Nothing Then
                If tmpstr.Length > 0 Then
                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
                   
                    DMReport = config.AppSettings.Settings("PurchaseA4ReportName").Value
                    For i As Integer = 0 To tmpstr.Length - 1
                        cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                    Next
                End If
            End If
            IsSelect = False
            cboChangeTemplate.Text = DMReport.ToString
            IsSelect = True
            'ShowData()

            Me.RptViewer.RefreshReport()
            'Dim dt As New DataTable
            'Dim dr As DataRow()
            'dt = _settingcontroller.GetKeyName()

            FileMap = New ExeConfigurationFileMap
            FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
            config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
            Dim ReportName As String
            'dr = dt.Select("KeyName='PrinterType'")
            'If dr.Length > 0 Then
            '    If dr(0).Item("KeyValue") = "1" Then
            ReportName = config.AppSettings.Settings("PurchaseA4ReportName").Value()
            Preview("PurchaseA4", ReportName)
            'Else
            '    ReportName = config.AppSettings.Settings("A4ReportName").Value()
            '    Preview("bya4", ReportName)
            'End If
            '    End If
            Me.RptViewer.Refresh()
        End If
    End Sub

    Private Sub optSaleGem_CheckedChanged(sender As Object, e As EventArgs) Handles optSaleGem.CheckedChanged
        If optSaleGem.Checked Then
            Dim tmpstr() As String
            Dim FileMap As ExeConfigurationFileMap
            Dim DMReport As String = ""
            RDLCTemplate()

            GetRDLCFileName("SaleGemA4", "")
            tmpstr = GetRDLCFileName("SaleGemA4", "")

            cboChangeTemplate.Items.Clear()
        If tmpstr IsNot Nothing Then
            If tmpstr.Length > 0 Then
                FileMap = New ExeConfigurationFileMap
                FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
               
                    DMReport = config.AppSettings.Settings("SaleGemA4ReportName").Value
                    For i As Integer = 0 To tmpstr.Length - 1
                        cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                    Next
                End If
            End If
            IsSelect = False
            cboChangeTemplate.Text = DMReport.ToString
            IsSelect = True
            'ShowData()

            Me.RptViewer.RefreshReport()
            'Dim dt As New DataTable
            'Dim dr As DataRow()
            'dt = _settingcontroller.GetKeyName()

            FileMap = New ExeConfigurationFileMap
            FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
            config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
            Dim ReportName As String
            'dr = dt.Select("KeyName='PrinterType'")
            'If dr.Length > 0 Then
            '    If dr(0).Item("KeyValue") = "1" Then

            ReportName = config.AppSettings.Settings("SaleGemA4ReportName").Value()
            Preview("SaleGemA4", ReportName)
            'Else
            '    ReportName = config.AppSettings.Settings("A4ReportName").Value()
            '    Preview("bya4", ReportName)
            'End If
            '    End If
            Me.RptViewer.Refresh()

        End If
    End Sub

    Private Sub optSaleVolume_CheckedChanged(sender As Object, e As EventArgs) Handles optSaleVolume.CheckedChanged
        If optSaleVolume.Checked Then
            Dim tmpstr() As String
            Dim FileMap As ExeConfigurationFileMap
            Dim DMReport As String = ""
            RDLCTemplate()

            GetRDLCFileName("SaleVolumeA4", "")
            tmpstr = GetRDLCFileName("SaleVolumeA4", "")

            cboChangeTemplate.Items.Clear()
            If tmpstr IsNot Nothing Then
                If tmpstr.Length > 0 Then
                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

                    DMReport = config.AppSettings.Settings("SaleVolumeA4ReportName").Value
                    For i As Integer = 0 To tmpstr.Length - 1
                        cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                    Next
                End If
            End If
            IsSelect = False
            cboChangeTemplate.Text = DMReport.ToString
            IsSelect = True
            'ShowData()

            Me.RptViewer.RefreshReport()
            'Dim dt As New DataTable
            'Dim dr As DataRow()
            'dt = _settingcontroller.GetKeyName()

            FileMap = New ExeConfigurationFileMap
            FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
            config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
            Dim ReportName As String
            'dr = dt.Select("KeyName='PrinterType'")
            'If dr.Length > 0 Then
            '    If dr(0).Item("KeyValue") = "1" Then

            ReportName = config.AppSettings.Settings("SaleVolumeA4ReportName").Value()
            Preview("SaleVolumeA4", ReportName)
            'Else
            '    ReportName = config.AppSettings.Settings("A4ReportName").Value()
            '    Preview("bya4", ReportName)
            'End If
            '    End If
            Me.RptViewer.Refresh()
        End If
    End Sub

    Private Sub optOrderReturn_CheckedChanged(sender As Object, e As EventArgs) Handles optOrderReturn.CheckedChanged
        If optOrderReturn.Checked Then
            Dim tmpstr() As String
            Dim FileMap As ExeConfigurationFileMap
            Dim DMReport As String = ""
            RDLCTemplate()

            GetRDLCFileName("OrderReturnA4", "")
            tmpstr = GetRDLCFileName("OrderReturnA4", "")

            cboChangeTemplate.Items.Clear()
            If tmpstr IsNot Nothing Then
                If tmpstr.Length > 0 Then
                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

                    DMReport = config.AppSettings.Settings("OrderReturnA4ReportName").Value
                    For i As Integer = 0 To tmpstr.Length - 1
                        cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                    Next
                End If
            End If
            IsSelect = False
            cboChangeTemplate.Text = DMReport.ToString
            IsSelect = True
            'ShowData()

            Me.RptViewer.RefreshReport()
            'Dim dt As New DataTable
            'Dim dr As DataRow()
            'dt = _settingcontroller.GetKeyName()

            FileMap = New ExeConfigurationFileMap
            FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
            config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
            Dim ReportName As String
            'dr = dt.Select("KeyName='PrinterType'")
            'If dr.Length > 0 Then
            '    If dr(0).Item("KeyValue") = "1" Then

            ReportName = config.AppSettings.Settings("OrderReturnA4ReportName").Value()
            Preview("OrderReturnA4", ReportName)
            'Else
            '    ReportName = config.AppSettings.Settings("A4ReportName").Value()
            '    Preview("bya4", ReportName)
            'End If
            '    End If
            Me.RptViewer.Refresh()

        End If
    End Sub

    Private Sub optPurchaseGem_CheckedChanged(sender As Object, e As EventArgs) Handles optPurchaseGem.CheckedChanged
        If optPurchaseGem.Checked Then
            Dim tmpstr() As String
            Dim FileMap As ExeConfigurationFileMap
            Dim DMReport As String = ""
            RDLCTemplate()

            GetRDLCFileName("PurchaseGemA4", "")
            tmpstr = GetRDLCFileName("PurchaseGemA4", "")

            cboChangeTemplate.Items.Clear()
            If tmpstr IsNot Nothing Then
                If tmpstr.Length > 0 Then
                    FileMap = New ExeConfigurationFileMap
                    FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
                    Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)

                    DMReport = config.AppSettings.Settings("PurchaseGemA4ReportName").Value
                    For i As Integer = 0 To tmpstr.Length - 1
                        cboChangeTemplate.Items.AddRange(New String() {tmpstr(i)})
                    Next
                End If
            End If
            IsSelect = False
            cboChangeTemplate.Text = DMReport.ToString
            IsSelect = True
            ' ShowData()

            Me.RptViewer.RefreshReport()
            'Dim dt As New DataTable
            'Dim dr As DataRow()
            'dt = _settingcontroller.GetKeyName()

            FileMap = New ExeConfigurationFileMap
            FileMap.ExeConfigFilename = Path.Combine(Application.StartupPath.ToString, "UI.exe.config")
            config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None)
            Dim ReportName As String
            'dr = dt.Select("KeyName='PrinterType'")
            'If dr.Length > 0 Then
            '    If dr(0).Item("KeyValue") = "1" Then

            ReportName = config.AppSettings.Settings("PurchaseGemA4ReportName").Value()
            Preview("PurchaseGemA4", ReportName)
            'Else
            '    ReportName = config.AppSettings.Settings("A4ReportName").Value()
            '    Preview("bya4", ReportName)
            'End If
            '    End If

            Me.RptViewer.Refresh()

        End If
    End Sub
End Class