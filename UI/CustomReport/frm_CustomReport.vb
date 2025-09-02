Imports BusinessRule
Imports CommonInfo
Imports System.Text
Public Class frm_CustomReport
    Implements IFormProcess

    Public _ReportID As String
    Public _ReportName As String

    Public drcustomreport As DataRow
    Private _CustomReportController As CustomReport.ICustomReportController = Factory.Instance.CreateCustomReportController

    Private Sub frm_CustomReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Clear()
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objCustomInfo As CommonInfo.CustomReportInfo
        If IsFillData() = True Then
            objCustomInfo = Get_Data()

            If _CustomReportController.SaveCustomReport(objCustomInfo) Then
                Clear()
                Return True
            Else
                Return False
            End If

        End If
    End Function

#Region "Private Methods"

    Private Sub Clear()
        _ReportID = ""
        txtReportName.Text = ""
        txtReportCode.Text = ""

        Dim coll As CheckedListBox.ObjectCollection = chkCheckListBox.Items

        For i As Int32 = 0 To coll.Count - 1
            Dim isChecked As Boolean = False
            If chkCheckListBox.GetItemChecked(i) = True Then
                chkCheckListBox.SetItemChecked(i, False)
            End If

        Next
    End Sub

    Private Function IsFillData() As Boolean
        Dim myFile As New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\Report\CustomRDLC\" & txtReportName.Text & ".rdlc")
        If myFile.Exists = False Then
            MsgBox("Please insert Report file Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        Return True
    End Function

    Private Function Get_Data() As CommonInfo.CustomReportInfo
        Dim objCustInfo As New CommonInfo.CustomReportInfo

        With objCustInfo
            .ReportID = _ReportID

            .ReportName = txtReportName.Text.Trim
            .ReportCode = txtReportCode.Text.Trim

            Dim coll As CheckedListBox.ObjectCollection = chkCheckListBox.Items
            For i As Int32 = 0 To coll.Count - 1
                If chkCheckListBox.GetItemChecked(i) = True Then

                    Select Case coll(i)
                        Case "ItemCategory"
                            .CriItemCategory = True
                        Case "ItemName"
                            .CriItemName = True
                        Case "GoldQuality"
                            .CriGoldQuality = True
                        Case "Customer"
                            .CriCustomer = True
                        Case "GemsCategory"
                            .CriGemsCategory = True
                        Case "FromDate"
                            .CriFromDate = True
                        Case "ToDate"
                            .CriToDate = True
                        Case "Staff"
                            .CriStaff = True

                    End Select
                Else
                    Select Case coll(i)
                        Case "ItemCategory"
                            .CriItemCategory = False
                        Case "ItemName"
                            .CriItemName = False
                        Case "GoldQuality"
                            .CriGoldQuality = False
                        Case "Customer"
                            .CriCustomer = False
                        Case "GemsCategory"
                            .CriGemsCategory = False
                        Case "FromDate"
                            .CriFromDate = False
                        Case "ToDate"
                            .CriToDate = False
                        Case "Staff"
                            .CriStaff = False
                    End Select
                End If
            Next
        End With
        Return objCustInfo
    End Function
#End Region

    Private Sub chkCheckListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chkCheckListBox.ItemCheck
        Dim coll As CheckedListBox.ObjectCollection = chkCheckListBox.Items
        Dim sb As StringBuilder = New StringBuilder()

        For i As Int32 = 0 To coll.Count - 1
            Dim isChecked As Boolean = False
            If i = e.Index Then
                If e.NewValue = CheckState.Checked Then
                    isChecked = True
                Else
                    isChecked = False
                End If
            Else
                isChecked = chkCheckListBox.GetItemChecked(i)
            End If
            If isChecked Then
                sb.Append(coll(i)).Append(", ")
            End If
        Next

        Dim res As String
        If sb.Length > 0 Then
            res = sb.ToString(0, sb.Length - 2)
        Else
            res = "<none>"
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim dtStock As New DataTable
        Dim dr As DataRow

        dtStock = _CustomReportController.GetCustomReportByStr("")
        dr = DirectCast(SearchData.FindFast(dtStock, "Custom Report List"), DataRow)

        If dr IsNot Nothing Then
            _ReportID = dr("ReportID")
            txtReportCode.Text = dr("ReportCode")
            txtReportName.Text = dr("ReportName")

            Dim coll As CheckedListBox.ObjectCollection = chkCheckListBox.Items


            For i As Int32 = 0 To coll.Count - 1
                Select Case coll.Item(i)

                    Case "GoldQuality"
                        If IIf(IsDBNull(dr("CriGoldQuality")), False, dr("CriGoldQuality")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)

                        End If

                    Case "ItemCategory"
                        If IIf(IsDBNull(dr("CriItemCategory")), False, dr("CriItemCategory")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)

                        End If
                    Case "ItemName"
                        If IIf(IsDBNull(dr("CriItemName")), False, dr("CriItemName")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)

                        End If
                    Case "Customer"
                        If IIf(IsDBNull(dr("CriCustomer")), False, dr("CriCustomer")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)

                        End If
                    Case "GemsCategory"
                        If IIf(IsDBNull(dr("CriGemsCategory")), False, dr("CriGemsCategory")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)

                        End If
                    Case "FromDate"
                        If IIf(IsDBNull(dr("CriFromDate")), False, dr("CriFromDate")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)

                        End If
                    Case "ToDate"
                        If IIf(IsDBNull(dr("CriToDate")), False, dr("CriToDate")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)

                        End If

                    Case "Staff"
                        If IIf(IsDBNull(dr("CriStaff")), False, dr("CriStaff")) = True Then
                            chkCheckListBox.SetItemChecked(i, True)
                        End If
                End Select

            Next
        End If
    End Sub
End Class
