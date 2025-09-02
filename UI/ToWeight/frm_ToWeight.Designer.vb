<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ToWeight
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me._Heading = New System.Windows.Forms.Label()
        Me.optTG = New System.Windows.Forms.RadioButton()
        Me.optTK = New System.Windows.Forms.RadioButton()
        Me.lblGetWeight = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtToWeight = New System.Windows.Forms.TextBox()
        Me.txtGoldC = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtGoldY = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtGoldP = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtGoldK = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblToWeight = New System.Windows.Forms.Label()
        Me.txtGetWeight = New System.Windows.Forms.TextBox()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.BackgroundImage = Global.UI.My.Resources.Resources.titlebar1
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnOK)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 170)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(279, 43)
        Me.Panel2.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(158, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(70, 25)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(82, 8)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(70, 25)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.UI.My.Resources.Resources.titlebar
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me._Heading)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(279, 43)
        Me.Panel1.TabIndex = 5
        '
        '_Heading
        '
        Me._Heading.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._Heading.BackColor = System.Drawing.Color.Transparent
        Me._Heading.Font = New System.Drawing.Font("Myanmar3", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Heading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me._Heading.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me._Heading.Location = New System.Drawing.Point(3, 0)
        Me._Heading.Name = "_Heading"
        Me._Heading.Size = New System.Drawing.Size(273, 41)
        Me._Heading.TabIndex = 0
        Me._Heading.Text = "အလေးချိန် ချိန်ရန်"
        Me._Heading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optTG
        '
        Me.optTG.AutoSize = True
        Me.optTG.BackColor = System.Drawing.Color.Transparent
        Me.optTG.Checked = True
        Me.optTG.Font = New System.Drawing.Font("Myanmar3", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTG.ForeColor = System.Drawing.Color.Blue
        Me.optTG.Location = New System.Drawing.Point(83, 55)
        Me.optTG.Name = "optTG"
        Me.optTG.Size = New System.Drawing.Size(54, 23)
        Me.optTG.TabIndex = 3
        Me.optTG.TabStop = True
        Me.optTG.Text = "ဂရမ်"
        Me.optTG.UseVisualStyleBackColor = False
        '
        'optTK
        '
        Me.optTK.AutoSize = True
        Me.optTK.BackColor = System.Drawing.Color.Transparent
        Me.optTK.Font = New System.Drawing.Font("Myanmar3", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTK.ForeColor = System.Drawing.Color.Blue
        Me.optTK.Location = New System.Drawing.Point(154, 55)
        Me.optTK.Name = "optTK"
        Me.optTK.Size = New System.Drawing.Size(54, 23)
        Me.optTK.TabIndex = 4
        Me.optTK.TabStop = True
        Me.optTK.Text = "ကျပ်"
        Me.optTK.UseVisualStyleBackColor = False
        '
        'lblGetWeight
        '
        Me.lblGetWeight.AutoSize = True
        Me.lblGetWeight.BackColor = System.Drawing.Color.Transparent
        Me.lblGetWeight.Font = New System.Drawing.Font("Myanmar3", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblGetWeight.ForeColor = System.Drawing.Color.Blue
        Me.lblGetWeight.Location = New System.Drawing.Point(42, 86)
        Me.lblGetWeight.Name = "lblGetWeight"
        Me.lblGetWeight.Size = New System.Drawing.Size(36, 19)
        Me.lblGetWeight.TabIndex = 9
        Me.lblGetWeight.Text = "ဂရမ်"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Zawgyi-One", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(5, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 21)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "အေလးခ်ိန္"
        '
        'txtToWeight
        '
        Me.txtToWeight.BackColor = System.Drawing.Color.Linen
        Me.txtToWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToWeight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToWeight.Location = New System.Drawing.Point(83, 112)
        Me.txtToWeight.Name = "txtToWeight"
        Me.txtToWeight.ReadOnly = True
        Me.txtToWeight.Size = New System.Drawing.Size(100, 22)
        Me.txtToWeight.TabIndex = 1
        Me.txtToWeight.TabStop = False
        Me.txtToWeight.Text = "0.0"
        Me.txtToWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtGoldC
        '
        Me.txtGoldC.BackColor = System.Drawing.Color.Linen
        Me.txtGoldC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldC.Location = New System.Drawing.Point(227, 138)
        Me.txtGoldC.MaxLength = 3
        Me.txtGoldC.Name = "txtGoldC"
        Me.txtGoldC.ReadOnly = True
        Me.txtGoldC.Size = New System.Drawing.Size(29, 22)
        Me.txtGoldC.TabIndex = 372
        Me.txtGoldC.TabStop = False
        Me.txtGoldC.Text = "0.00"
        Me.txtGoldC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(259, 141)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(14, 13)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "C"
        '
        'txtGoldY
        '
        Me.txtGoldY.BackColor = System.Drawing.Color.Linen
        Me.txtGoldY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldY.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldY.Location = New System.Drawing.Point(178, 138)
        Me.txtGoldY.MaxLength = 1
        Me.txtGoldY.Name = "txtGoldY"
        Me.txtGoldY.ReadOnly = True
        Me.txtGoldY.Size = New System.Drawing.Size(29, 22)
        Me.txtGoldY.TabIndex = 371
        Me.txtGoldY.TabStop = False
        Me.txtGoldY.Text = "0"
        Me.txtGoldY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(207, 140)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(14, 13)
        Me.Label19.TabIndex = 8
        Me.Label19.Text = "Y"
        '
        'txtGoldP
        '
        Me.txtGoldP.BackColor = System.Drawing.Color.Linen
        Me.txtGoldP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldP.Location = New System.Drawing.Point(131, 138)
        Me.txtGoldP.MaxLength = 2
        Me.txtGoldP.Name = "txtGoldP"
        Me.txtGoldP.ReadOnly = True
        Me.txtGoldP.Size = New System.Drawing.Size(29, 22)
        Me.txtGoldP.TabIndex = 370
        Me.txtGoldP.TabStop = False
        Me.txtGoldP.Text = "0"
        Me.txtGoldP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(158, 140)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(14, 13)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "P"
        '
        'txtGoldK
        '
        Me.txtGoldK.BackColor = System.Drawing.Color.Linen
        Me.txtGoldK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldK.Location = New System.Drawing.Point(83, 138)
        Me.txtGoldK.MaxLength = 3
        Me.txtGoldK.Name = "txtGoldK"
        Me.txtGoldK.ReadOnly = True
        Me.txtGoldK.Size = New System.Drawing.Size(29, 22)
        Me.txtGoldK.TabIndex = 369
        Me.txtGoldK.TabStop = False
        Me.txtGoldK.Text = "0"
        Me.txtGoldK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(111, 140)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(14, 13)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "K"
        '
        'lblToWeight
        '
        Me.lblToWeight.AutoSize = True
        Me.lblToWeight.BackColor = System.Drawing.Color.Transparent
        Me.lblToWeight.Font = New System.Drawing.Font("Myanmar3", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblToWeight.ForeColor = System.Drawing.Color.Blue
        Me.lblToWeight.Location = New System.Drawing.Point(187, 112)
        Me.lblToWeight.Name = "lblToWeight"
        Me.lblToWeight.Size = New System.Drawing.Size(36, 19)
        Me.lblToWeight.TabIndex = 377
        Me.lblToWeight.Text = "ဂရမ်"
        '
        'txtGetWeight
        '
        Me.txtGetWeight.BackColor = System.Drawing.Color.Linen
        Me.txtGetWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGetWeight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGetWeight.Location = New System.Drawing.Point(83, 85)
        Me.txtGetWeight.MaxLength = 1000
        Me.txtGetWeight.Name = "txtGetWeight"
        Me.txtGetWeight.Size = New System.Drawing.Size(100, 22)
        Me.txtGetWeight.TabIndex = 0
        Me.txtGetWeight.Text = "0.0"
        Me.txtGetWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frm_ToWeight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.UI.My.Resources.Resources.background
        Me.ClientSize = New System.Drawing.Size(279, 213)
        Me.Controls.Add(Me.txtGetWeight)
        Me.Controls.Add(Me.lblToWeight)
        Me.Controls.Add(Me.txtGoldC)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtGoldY)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtGoldP)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtGoldK)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtToWeight)
        Me.Controls.Add(Me.lblGetWeight)
        Me.Controls.Add(Me.optTK)
        Me.Controls.Add(Me.optTG)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_ToWeight"
        Me.Text = "To Weight"
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents _Heading As System.Windows.Forms.Label
    Friend WithEvents optTG As System.Windows.Forms.RadioButton
    Friend WithEvents optTK As System.Windows.Forms.RadioButton
    Friend WithEvents lblGetWeight As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtToWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldC As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtGoldY As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtGoldP As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtGoldK As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblToWeight As System.Windows.Forms.Label
    Friend WithEvents txtGetWeight As System.Windows.Forms.TextBox
End Class
