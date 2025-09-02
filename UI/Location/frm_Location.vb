Imports CommonInfo
Imports BusinessRule
Imports Operational.AppConfiguration
Imports System.Configuration

Public Class frm_Location
    Implements IFormProcess

    Dim key As String = "CurrentLocationID"
    Dim value As String
    Private _My_Own_Settings As New MyCurrentLocationSetting
    Private _LocationID As String = ""
    Private _objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
    Private _LocationController As Location.ILocationController = Factory.Instance.CreateLocationController



#Region "Private Methods"
    Private Sub clear()
        Dim dtHeadLocation As New DataTable
        MyBase.ChangeButtonEnableStage(CommonInfo.EnumSetting.ButtonEnableStage.Save)
        Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
        txtLocationID.Text = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.Location, EnumSetting.GenerateKeyType.Location.ToString, Now)
        _LocationID = "0"
        txtLocation.Text = ""
        txtAddress.Text = ""
        txtPhone.Text = ""
        txtRemark15.Text = ""
        txtRemarkDone.Text = ""
        txtLocation.Font = New Font("Myanmar3", 9.5)
        txtAddress.Font = New Font("Myanmar3", 9.5)
        txtPhone.Font = New Font("Myanmar3", 9.5)
        txtRemark15.Font = New Font("Myanmar3", 9.5)
        txtRemarkDone.Font = New Font("Myanmar3", 9.5)
        btnSave.Text = "&Save"
        btnSetCurrentLocation.Enabled = False
        SearchButton.Focus()
        chkIsHead.Checked = False
        dtHeadLocation = _LocationController.CheckIsExitHeadOfficeInLocation()
        If dtHeadLocation.Rows.Count > 0 Then
            chkIsHead.Enabled = False
        Else
            chkIsHead.Enabled = True
        End If
    End Sub
    Private Function Get_Data() As CommonInfo.LocationInfo
        Dim objLocationInfo As New CommonInfo.LocationInfo
        With objLocationInfo
            .LocationID = _LocationID
            .Location = IIf(txtLocation.Text = "", " ", txtLocation.Text)
            .Address = IIf(txtAddress.Text = "", "-", txtAddress.Text)
            .Phone = IIf(txtPhone.Text = "", "-", txtPhone.Text)
            .Remark15 = IIf(txtRemark15.Text = "", " ", txtRemark15.Text)
            .RemarkDone = IIf(txtRemarkDone.Text = "", " ", txtRemarkDone.Text)
            .CurrentLocationID = Global_CurrentLocationID
            .IsHeadOffice = chkIsHead.Checked
        End With
        Return objLocationInfo
    End Function
    Private Sub Show_Data(ByVal LocationObj As CommonInfo.LocationInfo)
        With LocationObj
            txtLocationID.Text = .LocationID
            txtLocation.Text = .Location
            txtAddress.Text = .Address
            txtPhone.Text = .Phone
            txtRemark15.Text = .Remark15
            txtRemarkDone.Text = .RemarkDone
            chkIsHead.Checked = .IsHeadOffice
            If chkIsHead.Checked Then
                chkIsHead.Enabled = True
            End If
        End With
    End Sub

#End Region

    Private Sub frm_Location_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        '  SearchButton.Focus()
    End Sub
    Private Sub frm_Location_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    
        MyBase.MaximizeBox = False
        _My_Own_Settings.DocumentName = Application.ExecutablePath & ".config"

        MyBase.btnNew.Visible = True
        MyBase.btnDelete.Visible = True
        _LocationID = Global_CurrentLocationID

        txtLocation.Font = New Font("Myanmar3", 9.5)
        txtAddress.Font = New Font("Myanmar3", 9.5)
        txtPhone.Font = New Font("Myanmar3", 9.5)
        txtRemark15.Font = New Font("Myanmar3", 9.5)

        txtRemarkDone.Font = New Font("Myanmar3", 9.5)
        ' LoadLocation()
        clear()


     
    End Sub


    Public Function ProcessDelete() As Boolean Implements IFormProcess.ProcessDelete
        Dim dt As New DataTable
        dt = _objGeneralController.CheckRecordsExistOrNot("tbl_PurchaseHeader", "tbl_SaleInvoiceHeader", "LocationID", _LocationID)

        If dt.Rows.Count() > 0 Then

            If CInt(dt.Rows(0).Item("Res")) > 0 Then
                MsgBox("You can not delete this Location  which has Transaction Records!", MsgBoxStyle.Information, Me.Text)
                Exit Function
            End If

        End If

        If _LocationController.DeleteLocation(_LocationID) Then
            clear()
            btnDelete.Enabled = False
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Save.ToString()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessNew() As Boolean Implements IFormProcess.ProcessNew
        clear()
    End Function

    Public Function ProcessSave() As Boolean Implements IFormProcess.ProcessSave
        Dim objLocationInfo As CommonInfo.LocationInfo

        If IsFillData() Then
            objLocationInfo = Get_Data()
            _LocationID = _LocationController.InsertLocation(objLocationInfo)

            If _LocationID <> "" Then
                clear()
                Return True
            Else
                Return False
            End If
        End If

    End Function
    Private Function IsFillData() As Boolean
        If txtLocation.Text = "" Then
            MessageBox.Show("Please fill data in  Shop Name textbox!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtLocation.Focus()
            Return False
        End If

        If txtAddress.Text = "" Then
            MessageBox.Show("Please fill data in Address!", "Data Not Fill", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAddress.Focus()
            Return False
        End If



        Return True
    End Function

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Dim DataItem As DataRow
        Dim dtLocation As New DataTable
        Dim objLocationInfo As New LocationInfo

        dtLocation = _LocationController.GetAllLocationList()
        DataItem = DirectCast(Search.FindFast(dtLocation, "Location List"), DataRow)
     


        If DataItem IsNot Nothing Then
            _LocationID = DataItem.Item("@LocationID").ToString()
            objLocationInfo = _LocationController.GetLocationByID(_LocationID)
            Show_Data(objLocationInfo)
            If _LocationID = Global_CurrentLocationID Then
                btnSetCurrentLocation.Enabled = False
            Else
                btnSetCurrentLocation.Enabled = True
            End If
            btnDelete.Enabled = True
            btnSave.Text = CommonInfo.EnumSetting.ButtonEnableStage.Update.ToString()
        End If
    End Sub

    Public Sub openhelp(ByVal name As String)
        Dim frm As New frm_Help
        frm.Open(name)
    End Sub


    Private Sub btnHelpbook_Click(sender As Object, e As EventArgs) Handles btnHelpbook.Click
        openhelp("Location")
    End Sub

    Private Sub btnSetCurrentLocation_Click(sender As Object, e As EventArgs) Handles btnSetCurrentLocation.Click
        If txtLocationID.Text <> "" Then
            value = txtLocationID.Text

            If _My_Own_Settings.MyPut("CurrentLocationID", value) <> "ERROR" Then
                MsgBox("Saving is successfully!", MsgBoxStyle.Information, "Complete")
            End If
        Else
            MsgBox("Saving is successfully!", MsgBoxStyle.Information, "Complete")
        End If



    End Sub


End Class
Public Class MyCurrentLocationSetting

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