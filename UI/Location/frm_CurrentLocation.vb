Imports BusinessRule
Imports CommonInfo

Public Class frm_CurrentLocation
#Region "Private Members"
    Private _objLocationController As Location.ILocationController = Factory.Instance.CreateLocationController
#End Region

    Private Sub frm_CurrentLocation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Fill_Combox()
    End Sub
    Private Sub Fill_Combox()
        cboLocation.DataSource = _objLocationController.GetAllLocationList
        cboLocation.DisplayMember = "Location_"
        cboLocation.ValueMember = "@LocationID"
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If cboLocation.SelectedIndex <> -1 Then
            If _objLocationController.SaveCurrentLocation(cboLocation.SelectedValue) Then
                Dim infoLocation As CommonInfo.LocationInfo = _objLocationController.GetLocationByID(cboLocation.SelectedValue)
                Global_CurrentLocationID = infoLocation.LocationID
                Global_CurrentLocationName = infoLocation.Location
                MsgBox("Save Successfully.", MsgBoxStyle.Information, Me.Text)
            End If
        Else
            MsgBox("Please Select Current OP!", MsgBoxStyle.Information, "GoldSmith Management System")
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class