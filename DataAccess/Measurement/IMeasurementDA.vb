Imports CommonInfo
Namespace Measurement

    Public Interface IMeasurementDA
        Function InsertMeasurement(ByVal info As MeasurementInfo) As Boolean
        Function DeleteMeasurement() As Boolean
        Function GetAllMeasurement() As DataTable
    End Interface
End Namespace
