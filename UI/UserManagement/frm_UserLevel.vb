Option Explicit On 
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BusinessRule
Imports BusinessRule.UserManagement
Imports CommonInfo

Public Class frm_UserLevel
    Inherits System.Windows.Forms.Form

#Region "Variables"
    Private _objUserLevelController As New UserLevel
    Public Const AppName = "Sale Management System"

    Dim Status As String
    Dim UserLevelID As Integer
    Dim ReturnFlg As Boolean
    Dim UserLevelInfo As New UserLevelInfo

    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserLevel As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
#End Region


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUserLevel = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Location = New System.Drawing.Point(83, 42)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(194, 21)
        Me.txtDescription.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 16)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Description  "
        '
        'txtUserLevel
        '
        Me.txtUserLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserLevel.Location = New System.Drawing.Point(83, 12)
        Me.txtUserLevel.Name = "txtUserLevel"
        Me.txtUserLevel.Size = New System.Drawing.Size(196, 21)
        Me.txtUserLevel.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 16)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "UserLevel  "
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 16)
        Me.Label4.TabIndex = 92
        Me.Label4.Text = "Remark "
        '
        'txtRemark
        '
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Location = New System.Drawing.Point(83, 74)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(238, 21)
        Me.txtRemark.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.SteelBlue
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(153, 111)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 24)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(244, 110)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 24)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frm_UserLevel
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.Silver
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(333, 145)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUserLevel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frm_UserLevel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "User Level Setup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Sub CleanUp()
        txtUserLevel.Text = ""
        txtDescription.Text = ""
        txtRemark.Text = ""
        With UserLevelInfo
            .SysID = 0
            .UserLevel = ""
            .UserType = 0
            .Remark = ""
        End With
    End Sub

    Public Function NewUserLevel() As Boolean
        UserLevelID = 0
        Me.ShowDialog()
        Return True
    End Function

    Public Function EditUserLevel(ByVal parUserLevelInfo As UserLevelInfo) As Boolean
        UserLevelID = parUserLevelInfo.SysID
        txtUserLevel.Text = parUserLevelInfo.UserLevel
        txtDescription.Text = parUserLevelInfo.Description
        txtRemark.Text = parUserLevelInfo.Remark

        Me.ShowDialog()

        Return True
    End Function


    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            Get_Data()
            If ValidateData() = False Then Exit Sub

            If _objUserLevelController.SaveUserLevel(UserLevelInfo) Then
                MessageBox.Show("Successfully Save", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ReturnFlg = True
                Me.Dispose()
                Me.Close()
            Else
                MessageBox.Show("User Level already exists!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ReturnFlg = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            ReturnFlg = False
        End Try
    End Sub

    Private Sub Get_Data()
        With UserLevelInfo
            .SysID = UserLevelID
            .UserLevel = txtUserLevel.Text.Trim
            .Description = txtDescription.Text.Trim
            .Remark = txtRemark.Text.Trim
        End With
    End Sub

    Private Function ValidateData() As Boolean

        If txtUserLevel.Text.Trim = "" Then
            MsgBox("Please Fill UserLevel", MsgBoxStyle.Information)
            txtUserLevel.Focus()
            ValidateData = False
            Exit Function
        End If

        If txtDescription.Text.Trim = "" Then
            MsgBox("Please Fill UserName", MsgBoxStyle.Information)
            txtDescription.Focus()
            ValidateData = False
            Exit Function
        End If

        ValidateData = True
    End Function


End Class
