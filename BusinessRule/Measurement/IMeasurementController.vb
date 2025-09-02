Imports CommonInfo
Namespace Measurement
    Public Interface IMeasurementController
        Function DeleteMeasurement() As Boolean
        Function SaveMeasurement(ByVal info As MeasurementInfo) As Boolean
        Function GetAllMeasurement() As DataTable
    End Interface
End Namespace
