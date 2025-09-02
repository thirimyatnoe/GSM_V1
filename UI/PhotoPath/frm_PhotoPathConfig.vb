Option Explicit On
Imports System
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Public Class frm_PhotoPathConfig
    Implements IFormProcess
    Dim key As String = "PhotoPath"
    Dim value As String
    Private _My_Own_Settings As New MyPhotoPathSetting
    Dim PhotoPath As String = "PhotoPath"

    Private Sub PhotoPathSetUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MyBase._Heading.Text = "PhotoPath Setup"
        MyBase.MaximizeBox = False
        _My_Own_Settings.DocumentName = Application.ExecutablePath & ".config"

        txtPhotoPath.Text = _My_Own_Settings.MyGet("PhotoPath").Trim 'For PhotoPath By ATK 22-3-2013
        btnNew.Visible = False
        btnDelete.Visible = False
    End Sub
    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
       
    End Function
    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave

        If txtPhotoPath.Text <> "" Then
            value = txtPhotoPath.Text
            If _My_Own_Settings.MyPut("PhotoPath", value) <> "ERROR" Then
                MsgBox("Saving is successfully!", MsgBoxStyle.Information, "Complete")
            End If

        Else
            MsgBox("Saving is successfully!", MsgBoxStyle.Information, "Complete")
        End If

    End Function
    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        txtPhotoPath.Text = ""
    End Function

    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("PhotoPath")
    End Sub
    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub
End Class
Public Class MyPhotoPathSetting

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
