Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace CashType
    Public Class CashTypeDA
        Implements ICashTypeDA

#Region "Private CashType"

        Private DB As Database
        Private Shared ReadOnly _instance As ICashTypeDA = New CashTypeDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICashTypeDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetCashTypeList() As System.Data.DataTable Implements ICashTypeDA.GetCashTypeList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CashTypeID,CashType from tbl_CashType Where IsDelete=0  Order by CashTypeID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function DeleteCashType(ByVal CashTypeID As String) As Boolean Implements ICashTypeDA.DeleteCashType
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_CashType Set IsDelete=1 WHERE  CashTypeID= @CashTypeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CashTypeID", DbType.String, CashTypeID)
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
        Public Function InsertCashType(ByVal obj As CommonInfo.CashTypeInfo) As Boolean Implements ICashTypeDA.InsertCashType
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_CashType (CashTypeID, CashType,LocationID,IsDelete,IsUpload)"
                strCommandText += " Values (@CashTypeID, @CashType,@LocationID,0,0)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CashTypeID", DbType.String, obj.CashTypeID)
                DB.AddInParameter(DBComm, "@CashType", DbType.String, obj.CashType)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)

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
        Public Function UpdateCashType(ByVal obj As CommonInfo.CashTypeInfo) As Boolean Implements ICashTypeDA.UpdateCashType
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_CashType set  CashType=@CashType ,LocationID=@LocationID"
                strCommandText += " where CashTypeID= @CashTypeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CashTypeID", DbType.String, obj.CashTypeID)
                DB.AddInParameter(DBComm, "@CashType", DbType.String, obj.CashType)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)

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

        Public Function GetCashTypeDataByCashType(ByVal CashType As String, Optional CashTypeID As String = "") As DataTable Implements ICashTypeDA.GetCashTypeDataByCashType
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim criStr As String = ""
            If CashTypeID = "" Then
                criStr = " Where CashType=@CashType and IsDelete=0 "
            Else
                criStr = " Where CashType=@CashType AND CashTypeID<>@CashTypeID AND IsDelete=0  "
            End If
            Try
                strCommandText = "SELECT * FROM tbl_CashType " & criStr
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CashTypeID", DbType.String, CashTypeID)
                DB.AddInParameter(DBComm, "@CashType", DbType.String, CashType)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

