Imports CommonInfo
Imports BusinessRule
Imports System.IO

Public Class frm_PhotoPath
    Implements IFormProcess

    Dim _OldPhotoPath As String
    Private _PhotoPathController As PhotoPath.IPhotoPathController = Factory.Instance.CreatePhotoPathController
    'Private _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
    'Public _OneFinger As Boolean
    'Dim dt As New DataTable
    'Dim tmpFileName As String = ""
    'Dim stri As String


    Private Sub frm_PhotoPath_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnNew.Visible = False
        btnDelete.Visible = False
        txtPhotoPath.Clear()
        txtPhotoPath.Text = _PhotoPathController.GetPhotoPathByID().PhotoPath
        _OldPhotoPath = txtPhotoPath.Text
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If _PhotoPathController.DeletePhotoPath() Then
            ProcessNew()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        ' _PhotoPathID = "0"
        'lblPhotoPathID.Text = _GeneralCon.GetGenerateKey(EnumSetting.GenerateKeyType.PhotoPath, "PhotoPath", Now)
        txtPhotoPath.Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            If _PhotoPathController.SavePhotoPath(GetData(), _OldPhotoPath) > 0 Then
                'txtPhotoPath.Text = ""
                Return True
            Else
                Return False

            End If
        End If
    End Function
    Private Function IsFillData() As Boolean
        If Not MyBase.IsTextBoxFill(txtPhotoPath) Then
            MsgBox("Enter Photo Path !", MsgBoxStyle.Information, AppName)
            txtPhotoPath.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function GetData() As CommonInfo.PhotoPathInfo
        Dim objPhotoPathinfo As New PhotoPathInfo
        With objPhotoPathinfo
            '.PhotoPathID = _PhotoPathID
            .PhotoPath = txtPhotoPath.Text.Trim
        End With
        Return objPhotoPathinfo
    End Function

    'Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    Dim DataItem As DataRow = DirectCast(Search.FindFast(_PhotoPathController.GetPhotoPathList(), "Photo Data List"), DataRow)

    '    If DataItem IsNot Nothing Then
    '        '_PhotoPathID = DataItem("@PhotoPathID")
    '        'ShowData(_PhotoPathController.GetPhotoPathByID(_PhotoPathID))
    '    End If
    'End Sub
    'Private Sub ShowData(ByVal objInfo As CommonInfo.PhotoPathInfo)
    '    With objInfo
    '        lblPhotoPathID.Text = .PhotoPathID
    '        txtPhotoPath.Text = .PhotoPath
    '    End With

    'End Sub

End Class
