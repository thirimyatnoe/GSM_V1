Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Staff
    Public Class StaffDA
        Implements IStaffDA

#Region "Private Staff"

        Private DB As Database
        Private Shared ReadOnly _instance As IStaffDA = New StaffDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IStaffDA
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function GetStaffList() As System.Data.DataTable Implements IStaffDA.GetStaffList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT StaffID,Staff AS [Staff_] from tbl_Staff where IsDelete=0 Order by StaffID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetStaffListByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IStaffDA.GetStaffListByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT StaffID,Staff AS [Staff_] from tbl_Staff where IsDelete=0 and LocationID=@LocationID Order by StaffID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function InsertStaff(ByVal StaffObj As CommonInfo.StaffInfo) As Boolean Implements IStaffDA.InsertStaff
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_Staff ( StaffID,Staff,IsDelete,IsUpload,LocationID,LastModifiedDate)"
                strCommandText += " Values (@StaffID,@Staff,0,0,@LocationID,getdate())"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, StaffObj.StaffID)
                DB.AddInParameter(DBComm, "@Staff", DbType.String, StaffObj.Staff)
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

        Public Function DeleteStaff(ByVal StaffID As String) As Boolean Implements IStaffDA.DeleteStaff
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_Staff Set IsDelete=1 WHERE  StaffID= @StaffID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, StaffID)
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

        Public Function UpdateStaff(ByVal StaffObj As CommonInfo.StaffInfo) As Boolean Implements IStaffDA.UpdateStaff
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_Staff set  Staff=@Staff ,LocationID=@LocationID,LastModifiedDate=getdate()"
                strCommandText += " where StaffID= @StaffID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, StaffObj.StaffID)
                DB.AddInParameter(DBComm, "@Staff", DbType.String, StaffObj.Staff)
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

        Public Function GetStaff(ByVal StaffID As String) As CommonInfo.StaffInfo Implements IStaffDA.GetStaff
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objStaffInfo As New StaffInfo
            Try
                strCommandText = " select StaffID  ,Staff from tbl_Staff where StaffID= @StaffID and IsDelete = 0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, StaffID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objStaffInfo
                        .StaffID = drResult("StaffID")
                        .Staff = drResult("Staff")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objStaffInfo
        End Function

        Public Function GetrptStaff() As DataTable Implements IStaffDA.GetrptStaff
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT StaffID, Staff FROM tbl_Staff where IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetStaffDataByStaff(ByVal Staff As String, Optional StaffID As String = "") As DataTable Implements IStaffDA.GetStaffDataByStaff
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim criStr As String = ""
            If StaffID = "" Then
                criStr = " and Staff=@Staff"
            Else
                criStr = " and Staff=@Staff AND StaffID<>@StaffID "
            End If
            Try
                strCommandText = "SELECT * FROM tbl_Staff where IsDelete = 0" & criStr
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, StaffID)
                DB.AddInParameter(DBComm, "@Staff", DbType.String, Staff)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

