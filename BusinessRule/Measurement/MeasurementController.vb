Imports DataAccess.Measurement
Imports CommonInfo
Namespace Measurement
    Public Class MeasurementController
        Implements IMeasurementController

#Region "Private Members"

        Private _objMeasurementDA As IMeasurementDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IMeasurementController = New MeasurementController

#End Region

#Region "Constructors"

        Private Sub New()
            _objMeasurementDA = DataAccess.Factory.Instance.CreateMeasurementDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMeasurementController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetAllMeasurement() As System.Data.DataTable Implements IMeasurementController.GetAllMeasurement
            Return _objMeasurementDA.GetAllMeasurement
        End Function

        Public Function SaveMeasurement(ByVal info As CommonInfo.MeasurementInfo) As Boolean Implements IMeasurementController.SaveMeasurement
            Return _objMeasurementDA.InsertMeasurement(info)
        End Function

        Public Function DeleteMeasurement() As Boolean Implements IMeasurementController.DeleteMeasurement
            Return _objMeasurementDA.DeleteMeasurement()
        End Function
    End Class
End Namespace