Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Customer
    Public Class CustomerDA
        Implements ICustomerDA

#Region "Private Location"

        Private DB As Database
        Private Shared ReadOnly _instance As ICustomerDA = New CustomerDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICustomerDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteCustomer(ByVal CustomerID As String) As Boolean Implements ICustomerDA.DeleteCustomer
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_Customer SET IsDelete=1 WHERE  CustomerID= @CustomerID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, CustomerID)
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

        Public Function InsertCustomer(ByVal obj As CommonInfo.CustomerInfo) As Boolean Implements ICustomerDA.InsertCustomer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_Customer ( CustomerID,CustomerCode,CustomerName,CustomerAddress,CustomerTel,Remark,IsInactive,IsDelete,LastModifiedDate,IsUpload,DOB,LocationID,NRC,MemberCode)"
                strCommandText += " Values (@CustomerID,@CustomerCode,@CustomerName,@CustomerAddress,@CustomerTel,@Remark,@IsInactive,0,getDate(),0,@DOB,@LocationID,@NRC,@MemberCode)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, obj.CustomerID)
                DB.AddInParameter(DBComm, "@CustomerCode", DbType.String, obj.CustomerCode)
                DB.AddInParameter(DBComm, "@CustomerName", DbType.String, obj.CustomerName)
                DB.AddInParameter(DBComm, "@CustomerAddress", DbType.String, obj.CustomerAddress)
                DB.AddInParameter(DBComm, "@CustomerTel", DbType.String, obj.CustomerTel)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@IsInactive", DbType.Boolean, obj.IsInactive)
                DB.AddInParameter(DBComm, "@DOB", DbType.Date, obj.DOB)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, obj.LocationID)
                DB.AddInParameter(DBComm, "@NRC", DbType.String, obj.NRC)
                DB.AddInParameter(DBComm, "@MemberCode", DbType.String, obj.MemberCode)
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

        Public Function UpdateCustomer(ByVal obj As CommonInfo.CustomerInfo) As Boolean Implements ICustomerDA.UpdateCustomer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_Customer set  CustomerID= @CustomerID , CustomerCode= @CustomerCode , CustomerName= @CustomerName , CustomerAddress= @CustomerAddress , CustomerTel= @CustomerTel , Remark= @Remark , IsInactive= @IsInactive ,LastModifiedDate=getDate(),DOB=@DOB,LocationID=@LocationID,NRC=@NRC,MemberCode=@MemberCode "
                strCommandText += " where CustomerID= @CustomerID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, obj.CustomerID)
                DB.AddInParameter(DBComm, "@CustomerCode", DbType.String, obj.CustomerCode)
                DB.AddInParameter(DBComm, "@CustomerName", DbType.String, obj.CustomerName)
                DB.AddInParameter(DBComm, "@CustomerAddress", DbType.String, obj.CustomerAddress)
                DB.AddInParameter(DBComm, "@CustomerTel", DbType.String, obj.CustomerTel)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@IsInactive", DbType.Boolean, obj.IsInactive)
                DB.AddInParameter(DBComm, "@DOB", DbType.Date, obj.DOB)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, obj.LocationID)
                DB.AddInParameter(DBComm, "@NRC", DbType.String, obj.NRC)
                DB.AddInParameter(DBComm, "@MemberCode", DbType.String, obj.MemberCode)

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

        Public Function GetAllCustomer() As System.Data.DataTable Implements ICustomerDA.GetAllCustomer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select IsInactive as [$Inactive],CustomerID as [@CustomerID],CustomerCode,MemberCode,CustomerName as [CustomerName_],CustomerAddress as [CustomerAddress_],CustomerTel,Remark  as [Remark_] ,DOB as DOB,NRC as NRC from tbl_Customer where IsDelete=0 Order By CustomerCode DESC "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetCustomerByID(ByVal CustomerID As String) As CommonInfo.CustomerInfo Implements ICustomerDA.GetCustomerByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objCustomer As New CustomerInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_Customer WHERE CustomerID = @CustomerID and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, CustomerID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objCustomer
                        .CustomerID = drResult("CustomerID")
                        .CustomerCode = drResult("CustomerCode")
                        .CustomerName = drResult("CustomerName")
                        .CustomerAddress = drResult("CustomerAddress")
                        .CustomerTel = drResult("CustomerTel")
                        .Remark = drResult("Remark")
                        .IsInactive = drResult("IsInactive")
                        .DOB = drResult("DOB")
                        .NRC = drResult("NRC")
                        .MemberCode = drResult("MemberCode")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objCustomer
        End Function
        Public Function GetCustomerCode(ByVal _CustomerCode As String) As CommonInfo.CustomerInfo Implements ICustomerDA.GetCustomerCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CustomerInfo
            Try
                strCommandText = "   SELECT  * FROM tbl_Customer WHERE CustomerCode= @CustomerCode And IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CustomerCode", DbType.String, _CustomerCode)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .CustomerID = drResult("CustomerID")
                        .CustomerCode = drResult("CustomerCode")
                        .CustomerName = drResult("CustomerName")
                        .CustomerAddress = drResult("CustomerAddress")
                        .CustomerTel = drResult("CustomerTel")
                        .Remark = drResult("Remark")
                        .IsInactive = drResult("IsInactive")
                        .DOB = drResult("DOB")
                        .NRC = drResult("NRC")
                        .MemberCode = drResult("MemberCode")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetCustomer() As DataTable Implements ICustomerDA.GetCustomer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CustomerID, CustomerCode, CustomerName, CustomerAddress, CustomerTel, Remark, IsInactive,DOB FROM tbl_Customer where IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetCustomerDataByCustomerName(ByVal CustomerName As String, Optional CustomerID As String = "") As DataTable Implements ICustomerDA.GetCustomerDataByCustomerName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim criStr As String = ""
            If CustomerID = "" Then
                criStr = " And CustomerName=N'" & CustomerName & "' AND Len(CustomerName)=Len(N'" & CustomerName & "')"
            Else
                criStr = " And CustomerName=N'" & CustomerName & "'  AND Len(CustomerName)=Len(N'" & CustomerName & "') AND CustomerID<>@CustomerID  "
            End If
            Try

                strCommandText = "SELECT * FROM tbl_Customer where IsDelete=0" & criStr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, CustomerID)
                DB.AddInParameter(DBComm, "@CustomerName", DbType.String, CustomerName)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllCustomerAutoCompleteData(Optional ByVal CustomerName As String = "") As System.Data.DataTable Implements ICustomerDA.GetAllCustomerAutoCompleteData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim criStr As String = ""
            If CustomerName <> "" Then
                criStr = " AND CustomerName LIKE @CustomerName"
            End If

            Try
                strCommandText = "select IsInactive as [$Inactive],CustomerID as [@CustomerID],CustomerCode,MemberCode,CustomerName as [CustomerName_],CustomerAddress as [CustomerAddress_],CustomerTel,Remark  as [Remark_],DOB As DOB  from tbl_Customer C WHERE 1=1 And IsDelete=0" & criStr & " Order By CustomerCode desc "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CustomerName", DbType.String, "%" + CustomerName + "%")
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllCustomerForSearch(Optional CustomerName As String = "") As System.Data.DataTable Implements ICustomerDA.GetAllCustomerForSearch
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim criStr As String = ""

            If CustomerName <> "" Then
                criStr = " AND CustomerName=N'" & CustomerName & "' "
            End If
            Try
                strCommandText = "select IsInactive as [$Inactive],CustomerID as [@CustomerID],CustomerCode,CustomerName as [CustomerName_],CustomerAddress as [CustomerAddress_],CustomerTel,Remark  as [Remark_] ,DOB as DOB from tbl_Customer C WHERE 1=1 and IsDelete=0 " & criStr & " Order By CustomerID DESC "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace
