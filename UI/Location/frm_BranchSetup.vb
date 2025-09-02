Imports CommonInfo
Imports BusinessRule
Public Class frm_BranchSetup
    Inherits frm_Base
    Implements IFormProcess
    Dim _IsUpdate As Boolean = False
    Dim _OldBranchName As String
    Dim dt_Branch As New DataTable

    Private _BranchController As Branch.IBranchController = Factory.Instance.CreateBranchController
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        If _OldBranchName <> "" Then
            If _BranchController.DeleteBranch(_OldBranchName) Then
                LoadBranch()
                Clear()
            End If
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        If IsFillData() Then
            If _IsUpdate = False Then
                If _BranchController.InsertBranch(txtBranchName.Text) Then
                    LoadBranch()
                    Clear()
                End If
            Else
                If _BranchController.UpdateBranch(txtBranchName.Text, _OldBranchName) Then
                    LoadBranch()
                    Clear()
                End If
            End If
        End If
    End Function

    Private Function IsFillData() As Boolean

        If txtBranchName.Text = "" Then
            MessageBox.Show("Please Fill Branch Name!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtBranchName.Focus()
            Return False
        End If

        If _IsUpdate = False Then
            If _BranchController.HashBranch(txtBranchName.Text) = False Then
                MsgBox("Branch is duplicated! Save with another one!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtBranchName.Focus()
                Return False
            End If
        Else
            If _BranchController.HashBranch(txtBranchName.Text, _OldBranchName) = False Then
                MsgBox("Branch is duplicated! Save with another one!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtBranchName.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub frm_BranchSetup_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadBranch()
        Clear()
        Me.KeyPreview = True
    End Sub

    Private Sub LoadBranch()
        dt_Branch = _BranchController.GetAllBranchList()
        If Not IsNothing(dt_Branch) Then
            lstBranch.Items.Clear()
            For i As Integer = 0 To dt_Branch.Rows.Count - 1
                lstBranch.Items.Add(dt_Branch.Rows(i).Item("BranchName"))
            Next
        End If
    End Sub
    Private Sub Clear()
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        _IsUpdate = False
        txtBranchName.Text = ""
        lstBranch.SelectedIndex = -1
        btnDelete.Enabled = False
        txtBranchName.Focus()
    End Sub

    Private Sub lstBranch_Click(sender As Object, e As EventArgs) Handles lstBranch.Click
        If lstBranch.Items.Count > 0 And lstBranch.SelectedIndex < lstBranch.Items.Count And lstBranch.SelectedIndex >= 0 Then
            _OldBranchName = CStr(dt_Branch.DefaultView.Item(lstBranch.SelectedIndex).Item("BranchName"))
            txtBranchName.Text = _OldBranchName
            _IsUpdate = True
            MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Update)
            btnDelete.Enabled = True
        End If
    End Sub
End Class
