Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace ExportData
    Public Class ExportDataDA
        Implements IExportDataDA


#Region "Private Export Data"
        Private DB As Database
        Private Shared ReadOnly _instance As IExportDataDA = New ExportDataDA
#End Region
#Region "Constructors"
        Public Sub New()
            DB = DatabaseFactory.CreateDatabase
        End Sub
#End Region
#Region "Public Properties"
        Public Shared ReadOnly Property Instance() As IExportDataDA
            Get
                Return _instance
            End Get
        End Property
#End Region


        Public Function DeleteExportData(ByVal ExportID As Integer) As Boolean Implements IExportDataDA.DeleteExportData
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE tbl_ExportData WHERE  ExportID= @ExportID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ExportID", DbType.Int64, ExportID)
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

        Public Function GetAllExportData() As System.Data.DataTable Implements IExportDataDA.GetAllExportData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try


                strCommandText = "SELECT ExportID,LocationID,OtherLocationID,LocationName,OtherLocationName,TransactionType,AllUse,ModifiedDate,CompanyName,ToMail,CCMail From tbl_ExportData where 1=1 Order by ExportID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllServiceData(Optional ByVal cristr As String = "") As System.Data.DataTable Implements IExportDataDA.GetAllServiceData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try


                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_ExportData WHERE 1=1 " & cristr
                DBComm = DB.GetSqlStringCommand(strCommandText)


                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable

            End Try
        End Function



        Public Function InsertExportData(ByVal ExportDataObj As CommonInfo.ExportDataInfo) As Boolean Implements IExportDataDA.InsertExportData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ExportData (LocationID,OtherLocationID, LocationName,OtherLocationName, TransactionType,AllUse,ModifiedDate,CompanyName,ToMail,CCMail)"
                strCommandText += " Values (@LocationID, @OtherLocationID, @LocationName,@OtherLocationName, @TransactionType,@AllUse,GetDate(),@CompanyName,@ToMail,@CCMail)"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, ExportDataObj.LocationID)
                DB.AddInParameter(DBComm, "@OtherLocationID", DbType.String, ExportDataObj.OtherLocationID)
                DB.AddInParameter(DBComm, "@LocationName", DbType.String, ExportDataObj.LocationName)
                DB.AddInParameter(DBComm, "@OtherLocationName", DbType.String, ExportDataObj.OtherLocationName)
                DB.AddInParameter(DBComm, "@TransactionType", DbType.String, ExportDataObj.TransactionType)
                DB.AddInParameter(DBComm, "@AllUse", DbType.Boolean, ExportDataObj.AllUse)
                DB.AddInParameter(DBComm, "@CompanyName", DbType.String, ExportDataObj.CompanyName)
                DB.AddInParameter(DBComm, "@ToMail", DbType.String, ExportDataObj.ToMail)
                DB.AddInParameter(DBComm, "@CCMail", DbType.String, ExportDataObj.CCMail)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function UpdateExportData(ByVal ExportDataObj As CommonInfo.ExportDataInfo) As Boolean Implements IExportDataDA.UpdateExportData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try


                strCommandText = "Update tbl_ExportData set LocationID= @LocationID, OtherLocationID= @OtherLocationID  ,LocationName= @LocationName, OtherLocationName=@OtherLocationName,TransactionType= @TransactionType, AllUse=@AllUse, ModifiedDate= GetDate(),CompanyName=@CompanyName,ToMail=@ToMail,CCMail=@CCMail "
                strCommandText += " where ExportID= @ExportID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ExportID", DbType.String, ExportDataObj.ExportID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, ExportDataObj.LocationID)
                DB.AddInParameter(DBComm, "@OtherLocationID", DbType.String, ExportDataObj.OtherLocationID)
                DB.AddInParameter(DBComm, "@LocationName", DbType.String, ExportDataObj.LocationName)
                DB.AddInParameter(DBComm, "@OtherLocationName", DbType.String, ExportDataObj.OtherLocationName)
                DB.AddInParameter(DBComm, "@TransactionType", DbType.String, ExportDataObj.TransactionType)
                DB.AddInParameter(DBComm, "@AllUse", DbType.Boolean, ExportDataObj.AllUse)
                DB.AddInParameter(DBComm, "@CompanyName", DbType.String, ExportDataObj.CompanyName)
                DB.AddInParameter(DBComm, "@ToMail", DbType.String, ExportDataObj.ToMail)
                DB.AddInParameter(DBComm, "@CCMail", DbType.String, ExportDataObj.CCMail)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
    End Class

End Namespace

