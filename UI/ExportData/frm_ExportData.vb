Imports BusinessRule
Imports CommonInfo
Imports System.IO
Public Class frm_ExportData
    Implements IFormProcess

    Private _ExportID As Integer
    Private _LocationID As String = ""
    Private _OtherLocationID As String = ""
    Private _LocationName As String = ""
    Private _OtherLocaionName As String = ""
    Private _TransactionType As String = ""
    Private _AllUse As Boolean
    Private _ModifiedDate As Date
    Private CompanyProfileID As String = ""
    Private Exportdatatype As String = ""


    Private dtExportData As DataTable
    Private dtLocationData As DataTable
    Private _ExportDataController As ExportData.IExportDataController = Factory.Instance.CreateExportDataController
    Private objExportDataController As ExportData.IExportDataController = Factory.Instance.CreateExportDataController
    Private objLocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private _LocationController As Location.ILocationController = Factory.Instance.CreateLocationController
    Private objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private objDataExportImport As DatabaseExportImport.IDatabaseExportImportController = Factory.Instance.CreateDatabaseExportImportController
    Public FLAG As String = ""

    Private Sub frm_ExportData_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'lblLogInUser.Text = Global_CurrentUser
        'lblCurrentLocationName.Text = Global_CurrentLocationName
        _LocationID = Global_CurrentLocationID
        _LocationName = Global_CurrentLocationName
        Clear()


    End Sub
    Private Sub Clear()

        dtLocationData = New DataTable
        dtLocationData.Columns.Add("LocationID", System.Type.GetType("System.String"))
        dtLocationData.Columns.Add("Location", System.Type.GetType("System.String"))
        dtLocationData.Columns.Add("AllUse", System.Type.GetType("System.Boolean"))
        grdlocation.AutoGenerateColumns = False
        grdlocation.DataSource = dtLocationData
        FormatLocationGrid()

        dtLocationData = _LocationController.GetAllLocationData()
        grdlocation.DataSource = dtLocationData



        dtExportData = New DataTable
        dtExportData.Columns.Add("ExportID", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("LocationID", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("OtherLocationID", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("LocationName", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("OtherLocationName", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("TransactionType", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("AllUse", System.Type.GetType("System.Boolean"))
        dtExportData.Columns.Add("CompanyName", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("ToMail", System.Type.GetType("System.String"))
        dtExportData.Columns.Add("CCMail", System.Type.GetType("System.String"))
        grdExport.AutoGenerateColumns = False
        grdExport.ReadOnly = True
        grdExport.DataSource = dtExportData
        FormatExportDataGrid()


        dtExportData = _ExportDataController.GetAllExportData()
        grdExport.DataSource = dtExportData
        cbotransaction.SelectedIndex = 0
        _ExportID = 0

        btndelete.Enabled = False
        btnSave.Text = "&Save"
        _AllUse = False
        FLAG = "New"

    End Sub


#Region "FormatGrid"

    Private Sub FormatLocationGrid()
        With grdlocation
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40
            .DefaultCellStyle.Font = New Font("Tahoma", 8.25)

            Dim dcLocationID As New DataGridViewTextBoxColumn()
            dcLocationID.HeaderText = "LocationID"
            dcLocationID.DataPropertyName = "LocationID"
            dcLocationID.Name = "LocationID"
            dcLocationID.Visible = False
            .Columns.Add(dcLocationID)

            Dim dcLocation As New DataGridViewTextBoxColumn()
            dcLocation.HeaderText = "Location"
            dcLocation.DataPropertyName = "Location"
            dcLocation.Name = "Location"
            dcLocation.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcLocation.Visible = True
            dcLocation.Width = 150
            dcLocation.ReadOnly = True
            .Columns.Add(dcLocation)

            Dim dcUse As New DataGridViewCheckBoxColumn()
            dcUse.HeaderText = "AllUse"
            dcUse.DataPropertyName = "AllUse"
            dcUse.Name = "AllUse"
            dcUse.Width = 80
            dcUse.Visible = True
            dcUse.ReadOnly = False
            dcUse.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcUse.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcUse)

        End With
    End Sub

    Private Sub FormatExportDataGrid()
        With grdExport
            .Columns.Clear()
            .ColumnHeadersDefaultCellStyle.Font = New Font("Myanmar3", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersHeight = 40
            .DefaultCellStyle.Font = New Font("Tahoma", 8.25)

            Dim dcExportID As New DataGridViewTextBoxColumn()
            dcExportID.HeaderText = "ExportID"
            dcExportID.DataPropertyName = "ExportID"
            dcExportID.Name = "ExportID"
            dcExportID.Visible = False
            .Columns.Add(dcExportID)

            Dim dcLocationID As New DataGridViewTextBoxColumn()
            dcLocationID.HeaderText = "LocationID"
            dcLocationID.DataPropertyName = "LocationID"
            dcLocationID.Name = "LocationID"
            dcLocationID.Width = 150
            dcLocationID.Visible = False
            .Columns.Add(dcLocationID)

            Dim dcOtherLocationID As New DataGridViewTextBoxColumn()
            dcOtherLocationID.HeaderText = "OtherLocationID"
            dcOtherLocationID.DataPropertyName = "OtherLocationID"
            dcOtherLocationID.Name = "OtherLocationID"
            dcOtherLocationID.Width = 150
            dcOtherLocationID.Visible = False
            .Columns.Add(dcOtherLocationID)


            Dim dcLocationName As New DataGridViewTextBoxColumn()
            dcLocationName.HeaderText = "LocationName"
            dcLocationName.DataPropertyName = "LocationName"
            dcLocationName.Name = "LocationName"
            dcLocationName.Width = 150
            dcLocationName.Visible = True
            dcLocationName.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcLocationName.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcLocationName)

            Dim dcOtherLocation As New DataGridViewTextBoxColumn()
            dcOtherLocation.HeaderText = "OtherLocationName"
            dcOtherLocation.DataPropertyName = "OtherLocationName"
            dcOtherLocation.Name = "OtherLocationName"
            dcOtherLocation.Width = 150
            dcOtherLocation.Visible = True
            dcOtherLocation.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcOtherLocation.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcOtherLocation)


            Dim dcTransactionType As New DataGridViewTextBoxColumn()
            dcTransactionType.HeaderText = "TransactionType"
            dcTransactionType.DataPropertyName = "TransactionType"
            dcTransactionType.Name = "TransactionType"
            dcTransactionType.Width = 150
            dcTransactionType.Visible = True
            dcTransactionType.DefaultCellStyle.Font = New Font("Myanmar3", 9)
            dcTransactionType.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(dcTransactionType)

            Dim dcAllUse As New DataGridViewTextBoxColumn()
            dcAllUse.HeaderText = "AllUse"
            dcAllUse.DataPropertyName = "AllUse"
            dcAllUse.Name = "AllUse"
            dcAllUse.Visible = False
            .Columns.Add(dcAllUse)

            Dim dcCompanyName As New DataGridViewTextBoxColumn()
            dcCompanyName.HeaderText = "CompanyName"
            dcCompanyName.DataPropertyName = "CompanyName"
            dcCompanyName.Name = "CompanyName"
            dcCompanyName.Visible = False
            .Columns.Add(dcCompanyName)

            Dim dcToMail As New DataGridViewTextBoxColumn()
            dcToMail.HeaderText = "ToMail"
            dcToMail.DataPropertyName = "ToMail"
            dcToMail.Name = "ToMail"
            dcToMail.Visible = False
            .Columns.Add(dcToMail)

            Dim dcCCMail As New DataGridViewTextBoxColumn()
            dcCCMail.HeaderText = "CCMail"
            dcCCMail.DataPropertyName = "CCMail"
            dcCCMail.Name = "CCMail"
            dcCCMail.Visible = False
            .Columns.Add(dcCCMail)
        End With
    End Sub
#End Region

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        ' Clear_Data()
    End Function


    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim objExportInfo As CommonInfo.ExportDataInfo
        Dim dtExportType As New DataTable
        If cbotransaction.SelectedIndex = 0 Then
            MsgBox("Please Select Transaction Type!", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If

        If (txtToMail.Text.Contains("@")) Then
            If Not (txtToMail.Text.ToLower.Contains(".com")) Then
                MsgBox("Please Check To Mail!", MsgBoxStyle.Information, AppName)
                Exit Sub
            End If
        Else
            MsgBox("Please Check To Mail!", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If

        If (txtCCMail.Text.Contains("@")) Then
            If Not (txtCCMail.Text.ToLower.Contains(".com")) Then
                MsgBox("Please Check CC Mail!", MsgBoxStyle.Information, AppName)
                Exit Sub
            End If
        Else
            MsgBox("Please Check CC Mail!", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If

        If FLAG <> "Update" Then
            dtExportType = objExportDataController.GetAllServiceData(" AND TransactionType ='" & cbotransaction.Text.Trim & "' and ExportID<>'" & _ExportID & "'")
            If dtExportType.Rows.Count > 0 Then
                MsgBox("Duplicate Transaction Type!", MsgBoxStyle.Information, AppName)
                Exit Sub
            End If
        End If


        objExportInfo = GetAllExportData()
        If objExportDataController.InsertExportData(objExportInfo) Then
            MsgBox("Save SucessFully", MsgBoxStyle.Information, AppName)

            Clear()
        Else
            MsgBox("UnSave", MsgBoxStyle.Information, AppName)
            Me.Close()
        End If

    End Sub


    Function GetAllExportData() As CommonInfo.ExportDataInfo
        Dim objExportInfo As New CommonInfo.ExportDataInfo
        Dim strOtherLocationID As String = ""
        Dim strOtherLocation As String = ""
        With objExportInfo
            .ExportID = _ExportID
            .LocationID = _LocationID
            .LocationName = _LocationName
            For Each drLocation As DataRow In dtLocationData.Rows
                If CBool(IIf(IsDBNull(drLocation.Item("AllUse")), 0, drLocation.Item("AllUse"))) = True Then 'CBool(drLocation.Item("AllUse"))

                    strOtherLocationID += drLocation.Item("LocationID").ToString & ","
                    strOtherLocation += drLocation.Item("Location").ToString & ","
                End If
            Next
            If strOtherLocationID.EndsWith(",") Then
                strOtherLocationID = strOtherLocationID.Remove(strOtherLocationID.Length - 1, 1)
            End If
            If strOtherLocation.EndsWith(",") Then
                strOtherLocation = strOtherLocation.Remove(strOtherLocation.Length - 1, 1)
            End If
            .OtherLocationID = strOtherLocationID
            .OtherLocationName = strOtherLocation
            .TransactionType = cbotransaction.Text.Trim
            .AllUse = True
            .CompanyName = txtCompanyName.Text
            .ToMail = txtToMail.Text
            .CCMail = txtCCMail.Text

        End With
        Return objExportInfo

    End Function

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnexport.Click
        Dim TransactionExportDate As DateTime
        Dim NowDate As String = ""
        Dim ExportDataType As String = ""
        Dim bolAll As Boolean = False
        Dim filetpath As String = ""
        Dim Cat As New ADOX.Catalog

        'TransactionExportDate = CDate(InputBox("Insert Export DateTime:", "", "2016-11-25 04:50:30.600"))
        NowDate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)

        If cbotransaction.Text = "" Or cbotransaction.Text = "--Select--" Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Exit Sub
        End If
        ExportDataType = cbotransaction.Text

        Dim FName1 As String = ExportDataType.Trim + Global_CurrentLocationID + "-" + String.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now)
        Dim FName2 As String = FName1.Replace("/", "-")
        Dim FName3 As String = FName2.Replace(":", "-")
        Dim FName As String = FName3.Replace(" ", "-")
        If Not Directory.Exists(My.Application.Info.DirectoryPath & "\ExportServiceFile") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\ExportServiceFile")
            If Directory.Exists(My.Application.Info.DirectoryPath & "\ExportServiceFile") Then
                filetpath = My.Application.Info.DirectoryPath & "\ExportServiceFile\" & FName & ".mdb"
            End If
        Else
            filetpath = My.Application.Info.DirectoryPath & "\ExportServiceFile\" & FName & ".mdb"
        End If

        '  ExportDataType = cbotransaction.SelectedValue
        bolAll = _AllUse

        btnexport.Enabled = True
        btnexport.Select()
        If objDataExportImport.CreateDatabaseForExportData(filetpath, TransactionExportDate, DateTime.Now, Global_CurrentLocationID, ExportDataType, FName, bolAll, Global_CurrentLocationID) = False Then
            MsgBox("Export Database Fails!", MsgBoxStyle.Exclamation, AppName)
            Me.Close()
        Else
            MsgBox("Export Successfully!", MsgBoxStyle.Information, AppName)
        End If



    End Sub


    Private Sub ShowGrdData()
        If grdExport.RowCount = 0 Then
            FLAG = "New"
            Exit Sub
        End If
        Dim lstLocaionID() As String
        Dim strLocaionIDList As String = ""
        Dim strLocaionID As String = ""
        Dim drow() As Data.DataRow = Nothing


        With grdExport
            _ExportID = .CurrentRow.Cells("ExportID").Value
            _LocationName = .CurrentRow.Cells("LocationName").Value
            _LocationID = .CurrentRow.Cells("LocationID").Value
            strLocaionIDList = .CurrentRow.Cells("OtherLocationID").Value
            lstLocaionID = strLocaionIDList.Split(CChar(","))
            For Each dru As DataRow In dtLocationData.Rows
                dru.Item("AllUse") = False
            Next
            For Each strLocaionID In lstLocaionID
                drow = dtLocationData.Select("LocationID ='" & strLocaionID & "'")
                If drow.Length > 0 Then
                    drow(0)("AllUse") = True
                End If
            Next

            grdlocation.DataSource = dtLocationData
            If .CurrentRow.Cells("TransactionType").Value = "Master" Then
                cbotransaction.SelectedIndex = 1
            ElseIf .CurrentRow.Cells("TransactionType").Value = "Stock" Then
                cbotransaction.SelectedIndex = 2
            ElseIf .CurrentRow.Cells("TransactionType").Value = "Current-Price" Then
                cbotransaction.SelectedIndex = 3
            ElseIf .CurrentRow.Cells("TransactionType").Value = "Expense-Income" Then
                cbotransaction.SelectedIndex = 4
            ElseIf .CurrentRow.Cells("TransactionType").Value = "Customer,Supplier,Sale-Person" Then
                cbotransaction.SelectedIndex = 5
            ElseIf .CurrentRow.Cells("TransactionType").Value = "Other-Transaction" Then
                cbotransaction.SelectedIndex = 6
            ElseIf .CurrentRow.Cells("TransactionType").Value = "Transfer-Return" Then
                cbotransaction.SelectedIndex = 7
            ElseIf .CurrentRow.Cells("TransactionType").Value = "Shop-Item" Then
                cbotransaction.SelectedIndex = 8

            End If

            txtCompanyName.Text = .CurrentRow.Cells("CompanyName").Value
            txtToMail.Text = .CurrentRow.Cells("ToMail").Value
            txtCCMail.Text = .CurrentRow.Cells("CCMail").Value

            FLAG = "Update"
            If FLAG <> "New" Then
                btnSave.Enabled = True
                btnSave.Text = "&Update"
                btndelete.Enabled = True
            End If
        End With
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnnew.Click
        Clear()
    End Sub



    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If _ExportDataController.DeleteExportData(_ExportID) Then
            '  Clear()
            btndelete.Enabled = False
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btndelete.Click

        If objExportDataController.DeleteExportData(_ExportID) Then
            MsgBox("Delete Successfully!", MsgBoxStyle.Information)
            Call Clear()
        Else
            MsgBox("Delete Unsuccessfully!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub grdExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles grdExport.Click
        ShowGrdData()
    End Sub

    Private Sub grdExport_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdExport.SelectionChanged
        ShowGrdData()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim ImportDataType As String = cbotransaction.Text
        Dim filetpath As String = My.Application.Info.DirectoryPath & "\ExportServiceFile\"
        Dim AllFileName As String() = Directory.GetFiles(filetpath, "*.mdb")
        Dim FileName As String = ""

        If AllFileName.Length > 0 Then
            For Each FileName In AllFileName
                If objDataExportImport.ImportData(FileName, Global_CurrentLocationID, ImportDataType) = False Then
                    MsgBox("Import Database Fail!", MsgBoxStyle.Exclamation, AppName)
                    Me.Close()
                Else
                    MsgBox("Import Successfully.", MsgBoxStyle.Information, AppName)

                End If
            Next
        End If
        '    ' Me.Dispose()
    End Sub
   
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("ExportData")
    End Sub
End Class