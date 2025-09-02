Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Supplier
    Public Class SupplierDA
        Implements ISupplierDA

#Region "Private Location"

        Private DB As Database
        Private Shared ReadOnly _instance As ISupplierDA = New SupplierDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISupplierDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteSupplier(ByVal SupplierID As String) As Boolean Implements ISupplierDA.DeleteSupplier
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_Supplier Set IsDelete=1 WHERE  SupplierID= @SupplierID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, SupplierID)
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

        Public Function InsertSupplier(ByVal obj As CommonInfo.SupplierInfo) As Boolean Implements ISupplierDA.InsertSupplier
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_Supplier ( SupplierID, SupplierCode, SupplierName, SupplierAddress, Email, Website, PhoneNo, Remark,IsDelete,IsUpload,LocationID,LastModifiedDate)"
                strCommandText += " Values (@SupplierID, @SupplierCode, @SupplierName, @SupplierAddress, @Email, @Website, @PhoneNo, @Remark,0,0,@LocationID,getdate())"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, obj.SupplierID)
                DB.AddInParameter(DBComm, "@SupplierCode", DbType.String, obj.SupplierCode)
                DB.AddInParameter(DBComm, "@SupplierName", DbType.String, obj.SupplierName)
                DB.AddInParameter(DBComm, "@SupplierAddress", DbType.String, obj.SupplierAddress)
                DB.AddInParameter(DBComm, "@PhoneNo", DbType.String, obj.PhoneNo)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@Email", DbType.String, obj.Email)
                DB.AddInParameter(DBComm, "@Website", DbType.String, obj.Website)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, obj.LocationID)
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

        Public Function UpdateSupplier(ByVal obj As CommonInfo.SupplierInfo) As Boolean Implements ISupplierDA.UpdateSupplier
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_Supplier set  SupplierID= @SupplierID , SupplierCode= @SupplierCode , SupplierName= @SupplierName , SupplierAddress= @SupplierAddress , PhoneNo= @PhoneNo , Remark= @Remark , Email= @Email , Website=@Website,LocationID=@LocationID,LastModifiedDate=getdate()"
                strCommandText += " where SupplierID= @SupplierID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, obj.SupplierID)
                DB.AddInParameter(DBComm, "@SupplierCode", DbType.String, obj.SupplierCode)
                DB.AddInParameter(DBComm, "@SupplierName", DbType.String, obj.SupplierName)
                DB.AddInParameter(DBComm, "@SupplierAddress", DbType.String, obj.SupplierAddress)
                DB.AddInParameter(DBComm, "@PhoneNo", DbType.String, obj.PhoneNo)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@Email", DbType.String, obj.Email)
                DB.AddInParameter(DBComm, "@Website", DbType.String, obj.Website)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, obj.LocationID)
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

        Public Function GetAllSupplier() As System.Data.DataTable Implements ISupplierDA.GetAllSupplier
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select SupplierID as [@SupplierID],SupplierCode,SupplierName as [SupplierName_],SupplierAddress as [SupplierAddress_], PhoneNo, Email, Website, Remark  as [Remark_] from tbl_Supplier where IsDelete = 0 Order By SupplierID DESC "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSupplierByID(ByVal SupplierID As String) As CommonInfo.SupplierInfo Implements ISupplierDA.GetSupplierByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSupplier As New SupplierInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_Supplier WHERE SupplierID = @SupplierID and IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, SupplierID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSupplier
                        .SupplierID = drResult("SupplierID")
                        .SupplierCode = drResult("SupplierCode")
                        .SupplierName = drResult("SupplierName")
                        .SupplierAddress = drResult("SupplierAddress")
                        .PhoneNo = drResult("PhoneNo")
                        .Remark = drResult("Remark")
                        .Email = drResult("Email")
                        .Website = drResult("Website")
                        .LocationID = drResult("LocationID")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objSupplier
        End Function

        Public Function GetSupplierDataByCode(ByVal SupplierCode As String) As CommonInfo.SupplierInfo Implements ISupplierDA.GetSupplierDataByCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSupplier As New SupplierInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_Supplier WHERE SupplierCode = @SupplierCode and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SupplierCode", DbType.String, SupplierCode)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSupplier
                        .SupplierID = drResult("SupplierID")
                        .SupplierCode = drResult("SupplierCode")
                        .SupplierName = drResult("SupplierName")
                        .SupplierAddress = drResult("SupplierAddress")
                        .PhoneNo = drResult("PhoneNo")
                        .Remark = drResult("Remark")
                        .Email = drResult("Email")
                        .Website = drResult("Website")
                        .LocationID = drResult("LocationID")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objSupplier
        End Function

        Public Function GetAllSupplierList() As System.Data.DataTable Implements ISupplierDA.GetAllSupplierList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT SupplierID AS [@SupplierID],SupplierCode,SupplierName as [SupplierName_],SupplierAddress AS [Address_], PhoneNo, Email, Website,Remark AS [Remark_] from tbl_Supplier where IsDelete=0 Order by SupplierID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllSupplierListByLocation(ByVal LocationID As String) As System.Data.DataTable Implements ISupplierDA.GetAllSupplierListByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT SupplierID AS [@SupplierID],SupplierCode,SupplierName as [SupplierName_],SupplierAddress AS [Address_], PhoneNo, Email, Website,Remark AS [Remark_] from tbl_Supplier where IsDelete=0 and LocationID=@LocationID  Order by SupplierID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

    End Class
End Namespace
