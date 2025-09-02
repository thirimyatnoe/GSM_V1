Imports BusinessRule.EventLogs
Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports NPOI.HSSF.UserModel
Public Class frm_GE_EventLogs
    Private _EventLogController As New EventLogsController
    Dim dt As DataTable
    Dim strFileName As String
    Dim SFD As New SaveFileDialog()

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        'dt = _EventLogController.ReadLog(dtpFromDate.Value.Date, dtpToDate.Value.Date, IIf(chkSource.Checked, cboSource.SelectedValue, ""), "")
        Dim AffectedID As String = ""
        Dim strAffected As String = ""
        If chkAffectedID.Checked Then
            AffectedID = _EventLogController.GetForAffectedIDByBarcodeNo(txtAffectedID.Text.Trim)
            If AffectedID <> "" Then
                strAffected = " AND Source='BarcodeNo' AND (AffectedID='" & txtAffectedID.Text.Trim & "' Or CASE WHEN CHARINDEX ( ',',AffectedID)>0 THEN Substring(AffectedID,CHARINDEX ( ',',AffectedID)+2,13) ELSE AffectedID END = '" & AffectedID & "')"
            Else
                strAffected = " And AffectedID='" & txtAffectedID.Text.Trim & "'"
            End If
        End If

        dt = _EventLogController.ReadLog(dtpFromDate.Value.Date, dtpToDate.Value.Date, "", IIf(chkSource.Checked, cboSource.SelectedItem, ""), strAffected, IIf(chkAction.Checked, cboAction.SelectedItem, ""))
        grd.DataSource = dt
    End Sub

    Private Sub frm_GE_EventLogs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormatGrid()
        chkSource.Checked = False
        cboSource.Enabled = False
        chkAction.Checked = False
        cboAction.Enabled = False
        txtAffectedID.Text = ""
        txtAffectedID.Enabled = False
        chkAffectedID.Checked = False
    End Sub
    Private Sub FormatGrid()
        With grd
            .AutoGenerateColumns = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .Columns.Clear()
            Dim dclID As New DataGridViewTextBoxColumn()
            dclID.HeaderText = "LogType"
            dclID.DataPropertyName = "LogType"
            dclID.Name = "LogType"
            dclID.Visible = False
            .Columns.Add(dclID)

            Dim dcPID As New DataGridViewTextBoxColumn()
            dcPID.HeaderText = "LogDateTime"
            dcPID.DataPropertyName = "LogDateTime"
            dcPID.Name = "LogDateTime"
            dcPID.DefaultCellStyle.Format = "dd-MM-yyyy hh:mm:ss tt"
            dcPID.Width = 180
            dcPID.Visible = True
            .Columns.Add(dcPID)

            Dim dcID As New DataGridViewTextBoxColumn()
            dcID.HeaderText = "LogInUser"
            dcID.DataPropertyName = "LogInUser"
            dcID.Name = "LogInUser"
            dcID.Width = 100
            dcID.Visible = True
            .Columns.Add(dcID)

            Dim dc As New DataGridViewTextBoxColumn()
            dc.HeaderText = "Source"
            dc.DataPropertyName = "Source"
            dc.Name = "Source"
            dc.Width = 150
            dc.Visible = True
            .Columns.Add(dc)


            Dim dc2 As New DataGridViewTextBoxColumn()
            dc2.HeaderText = "DataChange"
            dc2.DataPropertyName = "DataChange"
            dc2.Name = "DataChange"
            dc2.Width = 75
            dc2.Visible = True
            .Columns.Add(dc2)

            Dim dc3 As New DataGridViewTextBoxColumn()
            dc3.HeaderText = "AffectedID"
            dc3.DataPropertyName = "AffectedID"
            dc3.Name = "AffectedID"
            dc3.Width = 200
            dc3.Visible = True
            .Columns.Add(dc3)

            'Dim dc5 As New DataGridViewTextBoxColumn()
            'dc5.HeaderText = "BarcodeNo"
            'dc5.DataPropertyName = "ItemCode"
            'dc5.Name = "ItemCode"
            'dc5.Width = 150
            'dc5.Visible = True
            '.Columns.Add(dc5)

            Dim dc4 As New DataGridViewTextBoxColumn()
            dc4.HeaderText = "LogMessage"
            dc4.DataPropertyName = "LogMessage"
            dc4.Name = "LogMessage"
            dc4.Width = 600
            dc4.Visible = True
            .Columns.Add(dc4)

        End With
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        _EventLogController.ClearAllLogNow()
    End Sub

    Private Sub chkSource_CheckedChanged(sender As Object, e As EventArgs) Handles chkSource.CheckedChanged
        If chkSource.Checked Then
            cboSource.Enabled = True
        Else
            cboSource.Enabled = False
        End If
    End Sub


    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("EventLog")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

    Private Sub chkAction_CheckedChanged(sender As Object, e As EventArgs) Handles chkAction.CheckedChanged
        If chkAction.Checked Then
            cboAction.Enabled = True
        Else
            cboAction.Enabled = False
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        'ExportToExcel()
        If dt.Rows.Count > 0 Then
            Dim SaveFileDialog1 As New SaveFileDialog()
            SaveFileDialog1.Filter = "Excel Files |*.xls;*.xlsx"
            SaveFileDialog1.FileName = "StockImportFile"
            SaveFileDialog1.ShowDialog()
            Dim tmppath As String = ""
            tmppath = SaveFileDialog1.FileName.ToString()

            If tmppath <> "" Then
                If ExportExcel(tmppath) Then
                    MessageBox.Show("Export Successful")
                Else
                    MessageBox.Show("There is a problem exprot.")
                End If
            End If
        Else
            MessageBox.Show("There is no record.")
        End If

    End Sub
    Public Function ExportExcel(ByVal ExcelFileName As String) As Boolean


        Try
            Dim hssfworkbook As New HSSFWorkbook
            Dim sheet1 As HSSFSheet = hssfworkbook.CreateSheet("Sheet1")
            Dim headrow As HSSFRow = sheet1.CreateRow(0)
            Dim format As HSSFDataFormat = hssfworkbook.CreateDataFormat()
            Dim font As HSSFFont = hssfworkbook.CreateFont()
            font.FontName = "ZawGyi-One"
            font.FontHeight = 190

            Dim styleString As HSSFCellStyle = hssfworkbook.CreateCellStyle()
            Dim styleDate As HSSFCellStyle = hssfworkbook.CreateCellStyle()
            styleString.Alignment = HSSFCellStyle.ALIGN_LEFT
            styleString.DataFormat = HSSFDataFormat.GetBuiltinFormat("text")

            headrow.CreateCell(0).SetCellValue("LogType")
            sheet1.SetDefaultColumnStyle(0, styleString)

            headrow.CreateCell(1).SetCellValue("LogDateTime")
            sheet1.SetDefaultColumnStyle(1, styleString)

            headrow.CreateCell(2).SetCellValue("LogInUser")
            sheet1.SetDefaultColumnStyle(2, styleString)

            headrow.CreateCell(3).SetCellValue("Source")
            sheet1.SetDefaultColumnStyle(3, styleString)

            headrow.CreateCell(4).SetCellValue("DataChange")
            sheet1.SetDefaultColumnStyle(4, styleString)

            headrow.CreateCell(5).SetCellValue("AffectedID")
            sheet1.SetDefaultColumnStyle(5, styleString)

            headrow.CreateCell(6).SetCellValue("LogMessage")
            sheet1.SetDefaultColumnStyle(6, styleString)

            Dim j As Integer
            Dim i As Integer
            j = 1  'Start datarow
            For row_Index As Integer = 0 To dt.Rows.Count - 1
                Dim row As New HSSFRow
                row = sheet1.CreateRow(j)

                For i = 0 To dt.Columns.Count - 1
                    Dim cell_create As HSSFCell = row.CreateCell(i)

                    If Not IsDBNull(dt.Rows(row_Index).Item(i)) Then 'And row_Index <> dt.Rows.Count - 1
                        cell_create.SetCellValue(CStr(dt.Rows(row_Index).Item(i)))
                        cell_create.CellStyle = styleString
                    End If

                    cell_create.CellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER
                    cell_create.CellStyle.SetFont(font)

                Next
                j += 1
            Next
            Dim file As New FileStream(ExcelFileName, FileMode.Create)
            hssfworkbook.Write(file)
            file.Close()

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Private Sub ExportToExcel()
        ' Creating a Excel object. 
        'Dim excel As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()
        'Dim workbook As Microsoft.Office.Interop.Excel._Workbook = excel.Workbooks.Add(Type.Missing)
        'Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing

        'Try

        '    worksheet = workbook.ActiveSheet

        '    worksheet.Name = "ExportedFromDatGrid"

        '    Dim cellRowIndex As Integer = 1
        '    Dim cellColumnIndex As Integer = 1

        '    For i As Integer = 1 To grd.Columns.Count
        '        worksheet.Cells(1, i) = grd.Columns(i - 1).HeaderText
        '    Next



        '    ' storing Each row and column value to excel sheet
        '    For i As Integer = 0 To grd.Rows.Count - 2
        '        For j As Integer = 0 To grd.Columns.Count - 1
        '            worksheet.Cells(i + 2, j + 1) = grd.Rows(i).Cells(j).Value.ToString()
        '        Next
        '    Next

        '    'Getting the location and file name of the excel to save from user. 
        '    Dim saveDialog As New SaveFileDialog()
        '    saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        '    saveDialog.FilterIndex = 2

        '    If saveDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '        workbook.SaveAs(saveDialog.FileName)
        '        MessageBox.Show("Export Successful")
        '    End If
        'Catch ex As System.Exception
        '    MessageBox.Show(ex.Message)
        'Finally
        '    excel.Quit()
        '    workbook = Nothing
        '    excel = Nothing
        'End Try

    End Sub

    Private Sub chkAffectedID_CheckedChanged(sender As Object, e As EventArgs) Handles chkAffectedID.CheckedChanged
        If chkAffectedID.Checked Then
            txtAffectedID.Enabled = True
        Else
            txtAffectedID.Text = ""
            txtAffectedID.Enabled = False
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class