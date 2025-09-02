Option Explicit On
Imports System
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports CommonInfo
Imports BusinessRule

Public Class frm_CompanyProfile
    Implements IFormProcess

    Dim OpenFileDia As New OpenFileDialog
    Dim key As String = "CurrentCompanyID"
    Dim value As String
    Private _My_Own_Settings As New MyCompanyIDSetting
    Dim CurrentCompanyID As String = "CurrentCompanyID"
    Private _CompanyProfileID As String = ""
    Private _CompanyProfileController As CompanyProfile.ICompanyProfileController = Factory.Instance.CreateCompanyProfileController

    Private Sub frm_CompanyProfile_Load(sender As Object, e As EventArgs) Handles Me.Load
        'MyBase._Heading.Text = "User Profile Setup"
        _My_Own_Settings.DocumentName = Application.ExecutablePath & ".config"
        btnNew.Visible = False
        btnDelete.Visible = False
        _CompanyProfileID = _My_Own_Settings.MyGet("CurrentCompanyID").Trim
        onlyprofileshow()
    End Sub

    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete

    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        Clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objCompanyProfileInfo As CommonInfo.CompanyProfileInfo
        If IsFillData() Then
            objCompanyProfileInfo = Get_Data()

            If _CompanyProfileController.SaveCompanyProfile(objCompanyProfileInfo) Then
                _CompanyProfileID = objCompanyProfileInfo.CompanyID

                value = _CompanyProfileID
                If _My_Own_Settings.MyPut("CurrentCompanyID", value) <> "ERROR" Then
                    MsgBox("Saving is successfully!", MsgBoxStyle.Information, "Complete")
                End If
                onlyprofileshow()
            Else
                Return False
            End If
        End If
    End Function

#Region "Private Methods"
    Private Function IsFillData() As Boolean
        If txtCompanyName.Text = "" Or txtAddress.Text = "" Then
            MsgBox("Please Fill Data!", MsgBoxStyle.Information, AppName)
            Return False
        End If
        Return True
    End Function

    Private Sub onlyprofileshow()

        Dim objcompanyprofile As CommonInfo.CompanyProfileInfo
        objcompanyprofile = _CompanyProfileController.GetCompanyProfile(_CompanyProfileID)
        ShowData(objcompanyprofile)
    End Sub

    Private Sub ShowData(ByVal CompanyProfileObj As CommonInfo.CompanyProfileInfo)

        With CompanyProfileObj

            txtCompanyName.Text = .CompanyName
            If txtCompanyName.Text = "" Then
                txtCompanyName.Enabled = True
            Else
                txtCompanyName.Enabled = False
            End If

            txtPhone.Text = .Telephone
            txtEmail.Text = .Email
            txtAddress.Text = .Address
            txtWebsite.Text = .WebSite
            txtFax.Text = .Fax

            Dim bytBLOBData As Byte() = IIf(IsDBNull(.CompanyLogo), Nothing, .CompanyLogo)

            If Not IsNothing(bytBLOBData) Then
                Dim ms As New IO.MemoryStream(bytBLOBData)
                lblLogo.Image = System.Drawing.Image.FromStream(ms)
                btnAdd.Text = "Remove"
                lblPhoto.Visible = False
            Else
                lblLogo.Image = Nothing
                btnAdd.Text = "Add"
                lblPhoto.Visible = True
            End If

        End With

    End Sub

    Private Sub Clear()

        _CompanyProfileID = ""
        txtCompanyName.Clear()
        txtPhone.Clear()
        txtEmail.Clear()
        txtAddress.Clear()
        txtWebSite.Clear()
        txtFax.Clear()

        lblLogo.Image = Nothing
        lblPhoto.Visible = True
        txtCompanyName.Focus()
        btnAdd.Text = "Add"

    End Sub

    Private Function Get_Data() As CommonInfo.CompanyProfileInfo
        Dim objCustInfo As New CommonInfo.CompanyProfileInfo

        With objCustInfo
            .CompanyID = _CompanyProfileID
            .CompanyName = txtCompanyName.Text

            .Telephone = IIf(IsDBNull(txtPhone.Text), "-", txtPhone.Text)
            .Email = IIf(IsDBNull(txtEmail.Text), "-", txtEmail.Text)
            .Address = txtAddress.Text
            .WebSite = IIf(IsDBNull(txtWebsite.Text), "-", txtWebsite.Text)
            .Fax = IIf(IsDBNull(txtFax.Text), "-", txtFax.Text)

            If Not IsNothing(lblLogo.Image) Then
                'Save image from PictureBox into MemoryStream object.
                Dim ms As New IO.MemoryStream
                lblLogo.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                'Read from MemoryStream into Byte array.
                Dim bytBLOBData As Byte() = ms.GetBuffer
                'Byte[] bytBLOBData = new Byte[ms.Length];
                ms.Position = 0
                ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length))

                .CompanyLogo = bytBLOBData
            Else

                .CompanyLogo = Nothing
            End If
            .HO = True

        End With

        Return objCustInfo

    End Function
#End Region

#Region "Photo Add Button Click"
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim PictureBox1 As New PictureBox()

        If btnAdd.Text = "Add" Then
            OpenFileDia.Filter = "Image (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png;"
            '"Image (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif;"
            OpenFileDia.FileName = Global_PhotoPath + "\"
            OpenFileDia.InitialDirectory = OpenFileDia.FileName
            OpenFileDia.ShowDialog()
            If OpenFileDia.FileName <> "" Then
                If OpenFileDia.InitialDirectory = OpenFileDia.FileName Then
                    lblLogo.Image = Nothing
                    btnAdd.Text = "Add"
                    lblPhoto.Visible = True
                    Exit Sub
                End If
                lblLogo.Image = System.Drawing.Image.FromFile(OpenFileDia.FileName)
                btnAdd.Text = "Remove"
                lblPhoto.Visible = False
            Else
                lblLogo.Image = Nothing
                btnAdd.Text = "Add"
                lblPhoto.Visible = True
            End If
        Else
            lblLogo.Image = Nothing
            btnAdd.Text = "Add"
            lblPhoto.Visible = True
        End If
    End Sub
#End Region

    Public Class MyCompanyIDSetting

#Region "Variables"
        Private MyDocument As String
#End Region

#Region "Properties"
        Public Property DocumentName() As String
            Get
                Return MyDocument
            End Get
            Set(ByVal Value As String)
                MyDocument = Value
            End Set
        End Property
#End Region

#Region "Methods"

        Public Shared FileExtension As String = "bak"

        Public Shared Function SaveAsFile() As String
            Dim fileD As New SaveFileDialog
            fileD.Filter = "BackUp Files | *." + FileExtension
            If fileD.ShowDialog() = DialogResult.OK Then
                Return fileD.FileName
            Else
                Return ""
            End If
        End Function

        Public Shared Function OpenFile() As String
            Dim fileD As New OpenFileDialog
            fileD.Filter = "BackUp Files | *." + FileExtension
            If fileD.ShowDialog() = DialogResult.OK Then
                Return fileD.FileName
            Else
                Return ""
            End If
        End Function


        Public Function MyPut(ByVal MyKey As String, ByVal MyValue As String) As String
            Dim XmlDocument As New XmlDocument
            Dim XmlNode As XmlNode
            Dim XmlRoot As XmlNode
            Dim XmlKey As XmlNode
            Dim XmlValue As XmlNode

            If MyDocument.Length = 0 Then
                Return "ERROR"
                Exit Function
            End If

            MyPut = MyValue
            XmlDocument.Load(MyDocument)

            XmlNode = XmlDocument.DocumentElement.SelectSingleNode("/configuration/appSettings/add[@key=""" & MyKey & """]")

            If XmlNode Is Nothing Then
                '
                '   The node does not exist, let's create it
                '
                XmlNode = XmlDocument.CreateNode(XmlNodeType.Element, "add", "")
                '
                '   Adding the Key attribute to the node, keep in mind, Xml tokens are case sensitive
                '   We should use 'key' instead of 'Key'
                '
                XmlKey = XmlDocument.CreateNode(XmlNodeType.Attribute, "key", "")
                XmlKey.Value = MyKey
                XmlNode.Attributes.SetNamedItem(XmlKey)
                '
                '   Adding the key value, once again, remember that Xml tokens are case sensitive
                '
                XmlValue = XmlDocument.CreateNode(XmlNodeType.Attribute, "value", "")
                XmlValue.Value = MyValue
                XmlNode.Attributes.SetNamedItem(XmlValue)
                '
                '   Add the new node to the root
                '
                XmlRoot = XmlDocument.DocumentElement.SelectSingleNode("/configuration/appSettings")
                If Not XmlRoot Is Nothing Then
                    XmlRoot.AppendChild(XmlNode)
                Else
                    '
                    '   If not exist appSettings element, create it first and put node
                    '
                    Dim XmlElement As XmlElement = XmlDocument.CreateElement("appSettings")
                    XmlDocument.DocumentElement.AppendChild(XmlElement)
                    XmlElement.AppendChild(XmlNode)
                    'XmlRoot = XmlDocument.DocumentElement.SelectSingleNode("/configuration/appSettings")
                    'If Not XmlRoot Is Nothing Then
                    '    XmlRoot.AppendChild(XmlNode)
                    'End If
                    'MyPut = "ERROR"
                End If
            Else
                '
                '   The node exist, save the new value
                '
                XmlNode.Attributes.GetNamedItem("value").Value = MyValue

            End If
            XmlDocument.Save(MyDocument)
            XmlDocument = Nothing

            Return MyPut

        End Function

        Public Function MyGet(ByVal MyKey As String) As String
            Dim XmlDocument As New XmlDocument
            Dim XmlNode As XmlNode
            'Dim XmlRoot As XmlNode
            'Dim XmlKey As XmlNode
            'Dim XmlValue As XmlNode

            If MyDocument.Length = 0 Then
                Return ""
                Exit Function
            End If

            XmlDocument.Load(MyDocument)
            XmlNode = XmlDocument.DocumentElement.SelectSingleNode("/configuration/appSettings/add[@key=""" & MyKey & """]")

            If XmlNode Is Nothing Then
                '
                '   The node does not exist, let's create it
                '
                MyGet = ""

            Else
                '
                '   The node exist, save the new value
                '
                MyGet = XmlNode.Attributes.GetNamedItem("value").Value

            End If
            XmlDocument.Save(MyDocument)
            XmlDocument = Nothing

            Return MyGet

        End Function

#End Region

    End Class

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("CompanyProfile")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub

End Class
