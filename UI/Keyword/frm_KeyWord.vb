Imports UI
Imports BusinessRule

Public Class frm_Keyword
    Private _HeaderID As Integer = 0
    Dim _HeaderName As String
    Public _KeywordName As String = ""
    Dim _dtHeaderID As DataTable
    Private _dtKeywordItems As DataTable
    Private _KeywordController As Keyword.IKeywordController = Factory.Instance.CreateKeyWordController


    Private Sub Clear()
        _HeaderID = 0

        _dtKeywordItems = New DataTable
        _dtKeywordItems.Columns.Add("ItemID", System.Type.GetType("System.Int32"))
        _dtKeywordItems.Columns.Add("ItemName", System.Type.GetType("System.String"))

        grdKeyword.AutoGenerateColumns = False
        grdKeyword.DataSource = _dtKeywordItems
        FormatItemGrid()



    End Sub

    Private Sub FormatItemGrid()
        With grdKeyword
            .Columns.Clear()

            Dim dcID As New DataGridViewTextBoxColumn()
            dcID.HeaderText = "ItemID"
            dcID.DataPropertyName = "ItemID"
            dcID.Name = "ItemID"
            dcID.Visible = False
            .Columns.Add(dcID)

            Dim dcName As New DataGridViewTextBoxColumn()
            dcName.HeaderText = "ItemName"
            dcName.DataPropertyName = "ItemName"
            dcName.Name = "ItemName"
            dcName.DefaultCellStyle.Font = New Font("Myanmar3", 9.25)
            dcName.Width = 200
            .Columns.Add(dcName)


        End With
    End Sub

    Private Sub frm_KeyWord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Clear()
        Dim dtKeyWordList As New DataTable
        dtKeyWordList = _KeywordController.GetKeywordHeaderList
        cbokeywordname.DataSource = dtKeyWordList
        cbokeywordname.SelectedIndex = -1
        cbokeywordname.Focus()
    End Sub

    Private Function Get_Data() As CommonInfo.KeywordHeaderInfo
        Dim objKeywordHeaderInfo As New CommonInfo.KeywordHeaderInfo
        With objKeywordHeaderInfo
            .KeywordID = _HeaderID
            .KeywordName = cbokeywordname.Text
        End With
        Return objKeywordHeaderInfo
    End Function


    Private Sub cbokeywordname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbokeywordname.SelectedIndexChanged
        _HeaderName = cbokeywordname.Text
        _dtHeaderID = _KeywordController.GetKeywordHeaderIDByName(_HeaderName)

        If _dtHeaderID.Rows.Count > 0 Then
            _HeaderID = _dtHeaderID.Rows(0).Item(0)
           
            _dtKeywordItems = _KeywordController.GetKeywordItems(_HeaderID)

            grdKeyword.DataSource = _dtKeywordItems
        Else
            Call Clear()
        End If


        grdKeyword.Focus()


    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim objKeywordHeaderInfo As New CommonInfo.KeywordHeaderInfo

        '_dtHeaderID = _KeywordController.GetKeywordHeaderIDByName(_KeywordName)
        If _dtHeaderID.Rows.Count > 0 Then
            _HeaderID = _dtHeaderID.Rows(0).Item(0)
        End If

        objKeywordHeaderInfo = Get_Data()
        If cbokeywordname.Text = "" Then
            MsgBox("Please Select KeywordName", MsgBoxStyle.Information, Me.Text)
            cbokeywordname.Focus()
        ElseIf _KeywordController.SaveKeyword(objKeywordHeaderInfo, _dtKeywordItems) Then
            _HeaderID = objKeywordHeaderInfo.KeywordID

            _dtKeywordItems = _KeywordController.GetKeywordItems(_HeaderID)
            grdKeyword.DataSource = _dtKeywordItems

            MessageBox.Show("Save Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else

        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    Private Sub grdKeyword_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdKeyword.CellValidating
        With grdKeyword
            Select Case .Columns(e.ColumnIndex).Name.ToString
                Case ("ItemName")
                    If .CurrentRow.IsNewRow Then
                        Exit Sub
                    End If

                    If e.FormattedValue = "" Then
                        MsgBox("Enter Item Name !", MsgBoxStyle.Information, Me.Text)
                        e.Cancel = True
                    End If


            End Select
        End With
    End Sub

  
    Private Sub grdKeyword_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles grdKeyword.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then
            If Not grdKeyword.CurrentRow Is Nothing AndAlso Not grdKeyword.CurrentRow.IsNewRow AndAlso grdKeyword.CurrentRow.Index = 0 AndAlso IsDBNull(grdKeyword.Item("ItemName", 0).Value) Then
                grdKeyword.CancelEdit()
                grdKeyword.Rows.Remove(grdKeyword.CurrentRow)
            End If
        End If
    End Sub
End Class