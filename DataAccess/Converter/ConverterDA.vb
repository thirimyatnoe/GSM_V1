Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Converter
    Public Class ConverterDA
        Implements IConverterDA

#Region "Private Converter"

        Private DB As Database
        Private Shared ReadOnly _instance As IConverterDA = New ConverterDA

#End Region
#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region
#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IConverterDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetMeasurement(ByVal argFromMeasurement As String, ByVal argToMeasurement As String) As Decimal Implements IConverterDA.GetMeasurement
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGoldQuality As New GoldQualityInfo
            Try
                strCommandText = "SELECT Equivalent from tbl_Measurement " & _
                                " Where FromMeasurement=@FromMeasurement and ToMeasurement=@ToMeasurement"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromMeasurement", DbType.String, argFromMeasurement)
                DB.AddInParameter(DBComm, "@ToMeasurement", DbType.String, argToMeasurement)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    Return drResult("Equivalent")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management")
            End Try
        End Function
    End Class

End Namespace
