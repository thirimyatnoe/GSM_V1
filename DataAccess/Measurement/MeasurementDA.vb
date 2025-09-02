Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.data
Imports System.Data.Common
Namespace Measurement
    Public Class MeasurementDA
        Implements IMeasurementDA

#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As IMeasurementDA = New MeasurementDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMeasurementDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function GetAllMeasurement() As System.Data.DataTable Implements IMeasurementDA.GetAllMeasurement
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * FROM tbl_Measurement"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertMeasurement(ByVal info As MeasurementInfo) As Boolean Implements IMeasurementDA.InsertMeasurement
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " INSERT INTO tbl_Measurement VALUES (@FromMeasurement,@ToMeasurement,@Equivalent)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromMeasurement", DbType.String, info.FromMeasurement)
                DB.AddInParameter(DBComm, "@ToMeasurement", DbType.String, info.ToMeasurement)
                DB.AddInParameter(DBComm, "@Equivalent", DbType.Decimal, info.Equivalent)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function DeleteMeasurement() As Boolean Implements IMeasurementDA.DeleteMeasurement
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_Measurement"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Delete ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
    End Class

End Namespace