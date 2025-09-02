Imports DataAccess.ExportData


Namespace ExportData
    Public Class ExportDataController
        Implements IExportDataController
#Region "Private Members"

        Private _objExportDataDA As IExportDataDA

        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IExportDataController = New ExportDataController

#End Region

#Region "Constructors"

        Public Sub New()
            _objExportDataDA = DataAccess.Factory.Instance.CreateExportDataDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IExportDataController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteExportData(ExportID As Integer) As Boolean Implements IExportDataController.DeleteExportData
            If _objExportDataDA.DeleteExportData(ExportID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.ExportData.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                            ExportID, _
                            "Delete Export Data")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetExportData() As DataTable Implements IExportDataController.GetExportData
            Return _objExportDataDA.GetAllExportData()
        End Function
        Public Function GetAllServiceData(Optional ByVal cristr As String = "") As System.Data.DataTable Implements IExportDataController.GetAllServiceData
            Return _objExportDataDA.GetAllServiceData(cristr)
        End Function


        Public Function GetAllExportData() As DataTable Implements IExportDataController.GetAllExportData
            Return _objExportDataDA.GetAllExportData()
        End Function
        Public Function InsertExportData(ExportDataObj As CommonInfo.ExportDataInfo) As Boolean Implements IExportDataController.InsertExportData
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            If ExportDataObj.ExportID = "0" Then
                Return _objExportDataDA.InsertExportData(ExportDataObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.ExportData.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                            ExportDataObj.ExportID, _
                            "Insert Export Data")

            Else
                Return _objExportDataDA.UpdateExportData(ExportDataObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.ExportData.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                            ExportDataObj.ExportID, _
                            "Update Export Data")

            End If
        End Function
        Public Function UpdateExportData(ExportDataObj As CommonInfo.ExportDataInfo) As Boolean Implements IExportDataController.UpdateExportData
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            If ExportDataObj.ExportID = "0" Then
                ExportDataObj.ExportID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.ExportData, CommonInfo.EnumSetting.GenerateKeyType.ExportData.ToString, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.ExportData.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                            ExportDataObj.ExportID, _
                            "Update Export Data")
                Return _objExportDataDA.UpdateExportData(ExportDataObj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                            DateTime.Now, _
                            Global_UserID, _
                            CommonInfo.EnumSetting.GenerateKeyType.ExportData.ToString, _
                            CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                            ExportDataObj.ExportID, _
                            "Update Export Data")
                Return _objExportDataDA.UpdateExportData(ExportDataObj)
            End If
        End Function
    End Class


End Namespace


