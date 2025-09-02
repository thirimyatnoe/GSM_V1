Imports DataAccess.Location
Namespace Location
    Public Class LocationController
        Implements ILocationController

#Region "Private Members"

        Private _objLocationDA As ILocationDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ILocationController = New LocationController

#End Region

#Region "Constructors"

        Public Sub New()
            _objLocationDA = DataAccess.Factory.Instance.CreateLocationDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ILocationController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function InsertLocation(ByVal obj As CommonInfo.LocationInfo) As Boolean Implements ILocationController.InsertLocation
             Dim objGeneralController As General.IGeneralController
            Dim bolRet As Boolean
            objGeneralController = Factory.Instance.CreateGeneralController
            If obj.LocationID = "0" Then
                obj.LocationID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.Location, CommonInfo.EnumSetting.GenerateKeyType.Location.ToString, Now)
                bolRet = _objLocationDA.InsertLocation(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                       DateTime.Now, _
                       Global_UserID, _
                       CommonInfo.EnumSetting.GenerateKeyType.Location.ToString, _
                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                       obj.LocationID, _
                       "Insert Location")
            Else
                bolRet = _objLocationDA.UpdateLocation(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                       DateTime.Now, _
                       Global_UserID, _
                       CommonInfo.EnumSetting.GenerateKeyType.Location.ToString, _
                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                       obj.LocationID, _
                       "Update Location")
            End If
            Return bolRet
        End Function
      
        Public Function GetSubCounter(ByVal LocationID As String, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ILocationController.GetSubCounter
            Return _objLocationDA.GetSubCounter(LocationID, cristr)
        End Function
        Public Function DeleteLocation(ByVal LocationID As String) As Boolean Implements ILocationController.DeleteLocation
            If _objLocationDA.DeleteLocation(LocationID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Location.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       LocationID, _
                                       "Delete Location")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function DeleteCurrentLocation() As Boolean Implements ILocationController.DeleteCurrentLocation
            Return _objLocationDA.DeleteCurrentLocation()
        End Function

        Public Function GetCurrentLocationID() As String Implements ILocationController.GetCurrentLocationID
            Return _objLocationDA.GetCurrentLocationID()
        End Function

        Public Function SaveCurrentLocation(ByVal LocationID As String) As Boolean Implements ILocationController.SaveCurrentLocation
            Return _objLocationDA.SaveCurrentLocation(LocationID)
        End Function

        Public Function GetLocationByID(ByVal LocationID As String) As CommonInfo.LocationInfo Implements ILocationController.GetLocationByID
            Return _objLocationDA.GetLocationByID(LocationID)
        End Function

        Public Function GetLocationID(Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ILocationController.GetLocationID
            Return _objLocationDA.GetLocationID(LocationID)
        End Function

        Public Function GetCounterList(Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ILocationController.GetCounterList
            Return _objLocationDA.GetCounterList(LocationID)
        End Function

        Public Function CountIsOrderCountByLocationID(ByVal LocationID As String) As Integer Implements ILocationController.CountIsOrderCountByLocationID
            Return _objLocationDA.CountIsOrderCountByLocationID(LocationID)
        End Function

        Public Function GetCounterByCounterID(ByVal LocationID As String, Optional ByVal cristr As String = "") As String Implements ILocationController.GetCounterByCounterID
            Return _objLocationDA.GetCounterByCounterID(LocationID, cristr)
        End Function

        Public Function GetLocationIDByLocName(ByVal Location As String) As CommonInfo.LocationInfo Implements ILocationController.GetLocationIDByLocName
            Return _objLocationDA.GetLocationIDByLocName(Location)
        End Function
        Public Function GetAllLocation() As System.Data.DataTable Implements ILocationController.GetAllLocation
            Return _objLocationDA.GetAllLocation()
        End Function
        Public Function GetAllLocationData() As System.Data.DataTable Implements ILocationController.GetAllLocationData
            Return _objLocationDA.GetAllLocationData()
        End Function

        Public Function GetAllLocationExportData() As System.Data.DataTable Implements ILocationController.GetAllLocationExportData
            Return _objLocationDA.GetAllLocationExportData()
        End Function
        Public Function GetAllLocationList() As System.Data.DataTable Implements ILocationController.GetAllLocationList
            Return _objLocationDA.GetAllLocationList()
        End Function

        Public Function CheckIsExitHeadOfficeInLocation(Optional LocationID As String = "") As System.Data.DataTable Implements ILocationController.CheckIsExitHeadOfficeInLocation
            Return _objLocationDA.CheckIsExitHeadOfficeInLocation(LocationID)
        End Function


        Public Function CheckTransferInfo() As CommonInfo.GlobalSettingInfo Implements ILocationController.CheckTransferInfo
            Return _objLocationDA.CheckTransferInfo()
        End Function
        Public Function GetCompanyProfileList(Optional str As String = "") As DataTable Implements ILocationController.GetCompanyProfileList
            Return _objLocationDA.GetCompanyProfileList(str)
        End Function
    End Class
End Namespace

