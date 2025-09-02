Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports DataAccess.GlobalSetting

Namespace Location
    Public Class LocationDA
        Implements ILocationDA

#Region "Private Location"

        Private DB As Database
        Private Shared ReadOnly _instance As ILocationDA = New LocationDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ILocationDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function InsertLocation(ByVal Obj As CommonInfo.LocationInfo) As Boolean Implements ILocationDA.InsertLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_Location(LocationID,Location,Address,Phone,Remark15,RemarkDone,IsDelete,IsUpload,CurrentLocationID,LastModifiedDate,IsHeadOffice)"
                strCommandText += " Values (@LocationID,@Location,@Address,@Phone,@Remark15,@RemarkDone,0,0,@CurrentLocationID,getDate(),@IsHeadOffice)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                DB.AddInParameter(DBComm, "@Location", DbType.String, Obj.Location)
                DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
                DB.AddInParameter(DBComm, "@Phone", DbType.String, Obj.Phone)
                DB.AddInParameter(DBComm, "@Remark15", DbType.String, Obj.Remark15)
                DB.AddInParameter(DBComm, "@RemarkDone", DbType.String, Obj.RemarkDone)
                DB.AddInParameter(DBComm, "@CurrentLocationID", DbType.String, Obj.CurrentLocationID)
                DB.AddInParameter(DBComm, "@IsHeadOffice", DbType.String, Obj.IsHeadOffice)

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
        Public Function UpdateLocation(ByVal Obj As CommonInfo.LocationInfo) As Boolean Implements ILocationDA.UpdateLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_Location set  LocationID= @LocationID , Location= @Location,Address=@Address,Phone=@Phone,Remark15=@Remark15,RemarkDone=@RemarkDone,CurrentLocationID=@CurrentLocationID,LastModifiedDate=getDate(),IsHeadOffice=@IsHeadOffice "
                strCommandText += " where  LocationID= @LocationID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                DB.AddInParameter(DBComm, "@Location", DbType.String, Obj.Location)

                DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
                DB.AddInParameter(DBComm, "@Phone", DbType.String, Obj.Phone)

                DB.AddInParameter(DBComm, "@Remark15", DbType.String, Obj.Remark15)
                DB.AddInParameter(DBComm, "@RemarkDone", DbType.String, Obj.RemarkDone)
                DB.AddInParameter(DBComm, "@CurrentLocationID", DbType.String, Obj.CurrentLocationID)
                DB.AddInParameter(DBComm, "@IsHeadOffice", DbType.String, Obj.IsHeadOffice)

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
        Public Function DeleteCounter(ByVal LocationID As String, ByVal CounterID As String) As Boolean Implements ILocationDA.DeleteCouter
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_Counter WHERE LocationID = @LocationID AND CounterID = @CounterID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                DB.AddInParameter(DBComm, "@CounterID", DbType.String, CounterID)

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

        Public Function InsertCounter(ByVal CounterObj As CommonInfo.CounterInfo) As Boolean Implements ILocationDA.InsertCounter
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                strCommandText = "INSERT INTO tbl_Counter (LocationID,CounterID,CounterNo,Counter,IsOrderCounter) "
                strCommandText += " VALUES (@LocationID,@CounterID,@CounterNo,@Counter,@IsOrderCounter)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                With CounterObj
                    DB.AddInParameter(DBComm, "@LocationID", DbType.String, .LocationID)
                    DB.AddInParameter(DBComm, "@CounterID", DbType.String, .CounterID)
                    DB.AddInParameter(DBComm, "@CounterNo", DbType.String, .CounterNO)
                    DB.AddInParameter(DBComm, "@Counter", DbType.String, .Counter)
                    DB.AddInParameter(DBComm, "@IsOrderCounter", DbType.Boolean, .IsOrderCounter)
                End With
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

        Public Function UpdateCounter(ByVal CounterObj As CommonInfo.CounterInfo) As Boolean Implements ILocationDA.UpdateCounter
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "  UPDATE tbl_Counter SET  CounterNo=@CounterNo, Counter=@Counter, IsOrderCounter = @IsOrderCounter WHERE  LocationID = @LocationID AND CounterID = @CounterID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                With CounterObj
                    DB.AddInParameter(DBComm, "@LocationID", DbType.String, .LocationID)
                    DB.AddInParameter(DBComm, "@CounterID", DbType.String, .CounterID)
                    DB.AddInParameter(DBComm, "@CounterNo", DbType.String, .CounterNO)
                    DB.AddInParameter(DBComm, "@Counter", DbType.String, .Counter)
                    DB.AddInParameter(DBComm, "@IsOrderCounter", DbType.Boolean, .IsOrderCounter)
                End With
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

        Public Function GetSubCounter(ByVal LocationID As String, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ILocationDA.GetSubCounter
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT  CounterID, CounterNo, Counter, IsOrderCounter FROM tbl_Counter WHERE LocationID=@LocationID " & cristr & " ORDER BY CounterID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetAllLocationList() As System.Data.DataTable Implements ILocationDA.GetAllLocationList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT LocationID AS [@LocationID],Location AS [Location_],Location As Location,Address AS [Address_],Phone AS [Phone_],CurrentLocationID  as [@CurrentLocationID] from tbl_Location Where IsDelete=0 Order by LocationID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function




        Public Function DeleteLocation(ByVal LocationID As String) As Boolean Implements ILocationDA.DeleteLocation
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_Location Set IsDelete=1 WHERE  LocationID= @LocationID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
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

        Public Function DeleteCurrentLocation() As Boolean Implements ILocationDA.DeleteCurrentLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = "Delete From tbl_CurrentLocation"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                Return True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetCurrentLocationID() As String Implements ILocationDA.GetCurrentLocationID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "Select CurrentLocationID from tbl_CurrentLocation"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                If dtResult.Rows(0).Item("CurrentLocationID") = "" Then
                    MsgBox("Please Define Current Location First.", MsgBoxStyle.Critical, "GoldSmith Management System")
                End If

                Return dtResult.Rows(0).Item("CurrentLocationID")

            Catch ex As Exception
                'MsgBox("Please Define Current Location First.", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return ""
            End Try
        End Function

        Public Function SaveCurrentLocation(ByVal LocationID As String) As Boolean Implements ILocationDA.SaveCurrentLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = " Delete from tbl_CurrentLocation "
                DBComm = Nothing
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.ExecuteNonQuery(DBComm)

                strCommandText = ""
                strCommandText += " Insert into tbl_CurrentLocation(CurrentLocationID) "
                strCommandText += " Values (@CurrentLocationID)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CurrentLocationID", DbType.String, LocationID)

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


        Public Function GetLocationByID(ByVal LocationID As String) As CommonInfo.LocationInfo Implements ILocationDA.GetLocationByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objLocation As New LocationInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_Location WHERE LocationID = @LocationID And IsDelete= 0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objLocation
                        .LocationID = drResult("LocationID")
                        .Location = drResult("Location")

                        .Address = drResult("Address")
                        .Phone = drResult("Phone")
                        If CStr(drResult("Remark15")).Trim = "" Then
                            .Remark15 = ""
                        Else
                            .Remark15 = drResult("Remark15").ToString.Trim
                        End If
                        If CStr(drResult("RemarkDone")).Trim = "" Then
                            .RemarkDone = ""
                        Else
                            .RemarkDone = CStr(drResult("RemarkDone")).Trim
                        End If
                        .IsHeadOffice = drResult("IsHeadOffice")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objLocation
        End Function

        Public Function GetLocationID(Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ILocationDA.GetLocationID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "  SELECT *  FROM tbl_Location WHERE LocationID ='" & LocationID & "' " & " And  IsDelete =0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetCounterList(Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ILocationDA.GetCounterList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim cristr As String = ""
            If LocationID <> "" Then
                cristr = " WHERE L.LocationID='" & LocationID & "'"
            End If
            Try
                strCommandText = "SELECT L.LocationID AS [@LocationID],Location, CounterID, CounterNo, Counter " & _
                " FROM tbl_Location L INNER JOIN tbl_Counter C ON L.LocationID=C.LocationID" & cristr & _
                " ORDER BY L.LocationID, C.CounterID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function CountIsOrderCountByLocationID(ByVal LocationID As String) As Integer Implements ILocationDA.CountIsOrderCountByLocationID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "select Count(IsOrderCounter) as NoOfOrderCounter from tbl_Counter where LocationID = @LocationID and IsOrderCounter = '1' "
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)

                CountIsOrderCountByLocationID = CInt(DB.ExecuteScalar(DBComm))


            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetCounterByCounterID(ByVal LocationID As String, Optional ByVal cristr As String = "") As String Implements ILocationDA.GetCounterByCounterID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "select Counter from tbl_Counter where LocationID = @LocationID " & cristr & ""
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)


                GetCounterByCounterID = CStr(DB.ExecuteScalar(DBComm))

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetLocationIDByLocName(ByVal Location As String) As CommonInfo.LocationInfo Implements ILocationDA.GetLocationIDByLocName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objLocation As New LocationInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_Location WHERE Location = @Location And IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@Location", DbType.String, Location)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objLocation
                        .LocationID = drResult("LocationID")
                        .Location = drResult("Location")
                        .Address = drResult("Address")
                        .Phone = drResult("Phone")
                        .Remark15 = drResult("Remark15")
                        .RemarkDone = drResult("RemarkDone")
                        .IsHeadOffice = drResult("IsHeadOffice")

                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objLocation
        End Function
        Public Function GetAllLocation() As System.Data.DataTable Implements ILocationDA.GetAllLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT LocationID AS [@LocationID],Location,Address AS [Address_],Phone AS [Phone_] from tbl_Location where IsDelete=0 Order by LocationID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllLocationData() As System.Data.DataTable Implements ILocationDA.GetAllLocationData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT LocationID,Location,Address,Phone,Remark15,RemarkDone,IsDelete,IsUpload,0 as AllUse from tbl_Location where IsDelete=0 ORDER BY LocationID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllLocationExportData() As System.Data.DataTable Implements ILocationDA.GetAllLocationExportData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT LocationID,Location,Address,Phone,0 as AllUse from tbl_Location where IsDelete=0 ORDER BY LocationID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function CheckIsExitHeadOfficeInLocation(Optional LocationID As String = "") As System.Data.DataTable Implements ILocationDA.CheckIsExitHeadOfficeInLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If LocationID = "" Then
                    strCommandText = "select * from tbl_Location where IsHeadOffice=1 and IsDelete=0"
                Else

                    strCommandText = "select * from tbl_Location where IsHeadOffice=1 AND LocationID=@LocationID and IsDelete=0"
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)

                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
        End Function
        Public Function CheckTransferInfo() As CommonInfo.GlobalSettingInfo Implements ILocationDA.CheckTransferInfo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGlobalInfo As New GlobalSettingInfo
            Try
                strCommandText = " select * from tbl_GlobalSetting "
                DBComm = DB.GetSqlStringCommand(strCommandText)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGlobalInfo
                        .Photo = drResult("Photo")
                        .DatabaseSharePath = drResult("DatabaseSharePath")
                        .IsCarat = drResult("IsCarat")
                        .IsReuseBarcode = drResult("IsReuseBarcode")
                        .AllowDis = drResult("AllowDis")
                        .IsExactPrice = drResult("IsExactPrice")
                        .DiffChangeRate = drResult("DiffChangeRate")
                        .DiffPurchaseRate = drResult("DiffPurchaseRate")
                        .IsSpeedEntry = drResult("IsSpeedEntry")
                        .DecimalFormat = drResult("DecimalFormat")
                        .IsAllowUpdate = drResult("IsAllowUpdate")
                        .InterestRate = drResult("InterestRate")
                        .InterestPeriod = drResult("InterestPeriod")
                        .EnablePaidAmount = drResult("EnablePaidAmount")
                        .IsAllowSaleReturn = drResult("IsAllowSaleReturn")
                        .IsAllowSale = drResult("IsAllowSale")
                        .IsAllowStock = drResult("IsAllowStock")
                        .QRCode = drResult("QRCode")
                        .IsUsedSettingPeriod = drResult("IsUsedSettingPeriod")
                        .AllowEditWeight = drResult("AllowStockWeight")
                        .IsOneMonthCalculation = drResult("IsOneMonthCalculation")
                        .OverDay = drResult("OverDay")
                        .IsHoToBranch = drResult("IsHoToBranch")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGlobalInfo
        End Function
        Public Function GetCompanyProfileList(Optional str As String = "") As DataTable Implements ILocationDA.GetCompanyProfileList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "  SELECT CompanyID, CompanyName, Telephone, Email, Address, WebSite, Fax , HeadOffice " & _
                                    " FROM tbl_CompanyProfile " & str

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewelry Shop Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

