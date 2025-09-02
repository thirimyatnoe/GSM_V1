Imports DataAccess.CustomReport
Namespace CustomReport
    Public Class CustomReportController
        Implements ICustomReportController

#Region "Private Members"

        Private _objCustomReportDA As ICustomReportDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ICustomReportController = New CustomReportController

#End Region

#Region "Constructors"

        Private Sub New()
            _objCustomReportDA = DataAccess.Factory.Instance.CreateCustomReportDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICustomReportController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteCustomReport(CustomReportID As String) As Boolean Implements ICustomReportController.DeleteCustomReport
            If _objCustomReportDA.DeleteCustomReport(CustomReportID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                DateTime.Now, _
                Global_UserID, _
                CommonInfo.EnumSetting.GenerateKeyType.CustomReport.ToString, _
                CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                CustomReportID, _
                "Delete Custom Report")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetCustomReport(CustomReportID As String) As CommonInfo.CustomReportInfo Implements ICustomReportController.GetCustomReport
            Return _objCustomReportDA.GetCustomReport(CustomReportID)
        End Function

        Public Function GetCustomReportByStr(cristr As String) As DataTable Implements ICustomReportController.GetCustomReportByStr
            Return _objCustomReportDA.GetCustomReportByStr(cristr)
        End Function

        Public Function SaveCustomReport(ByRef CustomReportObj As CommonInfo.CustomReportInfo) As Boolean Implements ICustomReportController.SaveCustomReport
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            If CustomReportObj.ReportID = "" Then

                CustomReportObj.ReportID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.CustomReport, "CustomReport", Now.Date)

                If _objCustomReportDA.InsertCustomReport(CustomReportObj) Then

                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                       DateTime.Now, _
                       Global_UserID, _
                       CommonInfo.EnumSetting.GenerateKeyType.CustomReport.ToString, _
                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                       CustomReportObj.ReportID, _
                       "Insert Custom Report")
                    Return True
                End If

            Else
                If _objCustomReportDA.UpdateCustomReport(CustomReportObj) Then

                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                      DateTime.Now, _
                      Global_UserID, _
                      CommonInfo.EnumSetting.GenerateKeyType.CustomReport.ToString, _
                      CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                      CustomReportObj.ReportID, _
                      "Update Custom Report")
                    Return True
                End If
            End If
        End Function
    End Class
End Namespace

