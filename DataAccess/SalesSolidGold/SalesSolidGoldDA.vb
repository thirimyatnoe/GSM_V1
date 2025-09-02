Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace SalesSolidGold
    Public Class SalesSolidGoldDA
        Implements ISalesSolidGoldDA
#Region "Private SalesSolidGold"

        Private DB As Database
        Private Shared ReadOnly _instance As ISalesSolidGoldDA = New SalesSolidGoldDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesSolidGoldDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteSalesSolidGold(ByVal SaleSolidGoldID As String) As Boolean Implements ISalesSolidGoldDA.DeleteSalesSolidGold
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SaleSolidGold WHERE  SaleSolidGoldID= @SaleSolidGoldID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, SaleSolidGoldID)
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

        Public Function GetAllSalesSolidGold() As System.Data.DataTable Implements ISalesSolidGoldDA.GetAllSalesSolidGold
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select S.SaleSolidGoldID as [@SaleSolidGoldID],convert(varchar(10),S.SaleDate,105) as SaleDate, C.CustomerName, C.CustomerAddress, S.TotalPayment,S.PaidAmount, C.CustomerID AS [@CustomerID]" & _
                        " From tbl_SaleSolidGold S INNER JOIN tbl_Customer C ON S.CustomerID=C.CustomerID  Order by S.SaleDate "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesSolidGold(ByVal SaleSolidGoldID As String) As CommonInfo.SalesSolidGoldInfo Implements ISalesSolidGoldDA.GetSalesSolidGold
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSaleSolid As New SalesSolidGoldInfo
            Try
                strCommandText = " SELECT  *,"
                strCommandText += " CAST(GoldTK AS INT) AS GoldK,  "
                strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
                strCommandText += " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
                strCommandText += " CAST(CurrentGoldTK AS INT) AS CurrentGoldK,"
                strCommandText += " CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT) AS CurrentGoldP,"
                strCommandText += " CAST((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8 AS INT) AS CurrentGoldY,"
                strCommandText += " CAST(((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8)-CAST((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS CurrentGoldC, "
                strCommandText += " CAST(SaleGoldTK AS INT) AS SaleGoldK,  "
                strCommandText += " CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT) AS SaleGoldP,"
                strCommandText += " CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS INT) AS SaleGoldY,"
                strCommandText += " CAST(((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8)-CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS SaleGoldC, "
                strCommandText += " CAST(LeftGoldTK AS INT) AS LeftGoldK, "
                strCommandText += " CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT) AS LeftGoldP,"
                strCommandText += " CAST((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8 AS INT) AS LeftGoldY,"
                strCommandText += " CAST(((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8)-CAST((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS LeftGoldC "
                strCommandText += "  FROM tbl_SaleSolidGold WHERE SaleSolidGoldID= @SaleSolidGoldID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, SaleSolidGoldID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSaleSolid

                        .SaleSolidGoldID = drResult("SaleSolidGoldID")
                        .SaleDate = drResult("SaleDate")
                        .StaffID = drResult("StaffID")
                        .Customer = drResult("Customer")
                        .Address = drResult("Address")

                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemName = drResult("ItemName")
                        .CounterID = drResult("CounterID")
                        .LocationID = drResult("LocationID")

                        .ItemCategoryID = drResult("ItemCategoryID")
                        .GoldQualityID = drResult("GoldQualityID")
                        .SalesRate = drResult("SalesRate")
                        .DoneRate = drResult("DoneRate")

                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .GoldC = Format(drResult("GoldC"), "0.0")

                        .CurrentGoldK = drResult("CurrentGoldK")
                        .CurrentGoldP = drResult("CurrentGoldP")
                        .CurrentGoldY = drResult("CurrentGoldY")
                        .CurrentGoldC = Format(drResult("CurrentGoldC"), "0.0")

                        .SaleGoldK = drResult("SaleGoldK")
                        .SaleGoldP = drResult("SaleGoldP")
                        .SaleGoldY = drResult("SaleGoldY")
                        .SaleGoldC = Format(drResult("SaleGoldC"), "0.0")

                        .LeftGoldK = drResult("LeftGoldK")
                        .LeftGoldP = drResult("LeftGoldP")
                        .LeftGoldY = drResult("LeftGoldY")
                        .LeftGoldC = Format(drResult("LeftGoldC"), "0.0")

                        .TotalPayment = drResult("TotalPayment")
                        .AddOrSub = drResult("AddOrSub")
                        .DiscountAmount = drResult("DiscountAmount")
                        .PaidAmount = drResult("PaidAmount")
                        .Remark = IIf(IsDBNull(drResult("Remark")), "-", drResult("Remark"))

                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .CurrentGoldTK = drResult("CurrentGoldTK")
                        .CurrentGoldTG = drResult("CurrentGoldTG")
                        .SaleGoldTK = drResult("SaleGoldTK")
                        .SaleGoldTG = drResult("SaleGoldTG")
                        .LeftGoldTK = drResult("LeftGoldTK")
                        .LeftGoldTG = drResult("LeftGoldTG")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objSaleSolid
        End Function

        Public Function GetSalesSolidGoldPrint(ByVal SaleSolidGoldID As String) As System.Data.DataTable Implements ISalesSolidGoldDA.GetSalesSolidGoldPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select S.SaleSolidGoldID,S.SaleDate,S.CounterID as [@CounterID],C.Counter,L.Location,G.GoldQuality,I.ItemCategory,S.Customer,S.Address,ST.Name as Staff,S.ForSaleID,S.ItemCode,S.ItemName,S.SalesRate,S.DoneRate,S.TotalPayment,S.PaidAmount,S.AddOrSub,S.DiscountAmount,S.SaleGoldTK, " & _
                        " CAST(SaleGoldTK AS INT) AS SaleGoldK," & _
                        " CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT) AS SaleGoldP," & _
                        " CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS SaleGoldY" & _
                        " From tbl_SaleSolidGold S Left Join tbl_Counter C On S.CounterID=C.CounterID Left Join tbl_Location L On L.LocationID=S.LocationID Left Join tbl_GoldQuality G On S.GoldQualityID=G.GoldQualityID Left Join tbl_ItemCategory I On S.ItemCategoryID=I.ItemCategoryID Left Join tbl_StaffByLocation ST On S.StaffID=ST.SaleStaffID " & _
                        " where S.SaleSolidGoldID=@SaleSolidGoldID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, SaleSolidGoldID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertSalesSolidGold(ByVal SaleSolidGoldObj As CommonInfo.SalesSolidGoldInfo) As Boolean Implements ISalesSolidGoldDA.InsertSalesSolidGold
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_SaleSolidGold ( SaleSolidGoldID,SaleDate,StaffID,Customer,Address,LocationID,CounterID,ForSaleID,ItemCode,GoldQualityID,ItemCategoryID,ItemName,SalesRate,DoneRate,GoldTK,GoldTG,CurrentGoldTK,CurrentGoldTG,SaleGoldTK,SaleGoldTG,LeftGoldTK,LeftGoldTG,TotalPayment,AddOrSub,DiscountAmount,PaidAmount,Remark,LastModifiedLoginUserName,LastModifiedDate)"
                strCommandText += " Values (@SaleSolidGoldID,@SaleDate,@StaffID,@Customer,@Address,@LocationID,@CounterID,@ForSaleID,@ItemCode,@GoldQualityID,@ItemCategoryID,@ItemName,@SalesRate,@DoneRate,@GoldTK,@GoldTG,@CurrentGoldTK,@CurrentGoldTG,@SaleGoldTK,@SaleGoldTG,@LeftGoldTK,@LeftGoldTG,@TotalPayment,@AddOrSub,@DiscountAmount,@PaidAmount,@Remark,@LastModifiedLoginUserName,getDate())"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, SaleSolidGoldObj.SaleSolidGoldID)
                DB.AddInParameter(DBComm, "@SaleDate", DbType.Date, SaleSolidGoldObj.SaleDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, SaleSolidGoldObj.StaffID)
                DB.AddInParameter(DBComm, "@Customer", DbType.String, SaleSolidGoldObj.Customer)
                DB.AddInParameter(DBComm, "@Address", DbType.String, SaleSolidGoldObj.Address)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@CounterID", DbType.String, SaleSolidGoldObj.CounterID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, SaleSolidGoldObj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, SaleSolidGoldObj.ItemCode)

                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, SaleSolidGoldObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, SaleSolidGoldObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, SaleSolidGoldObj.ItemName)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, SaleSolidGoldObj.SalesRate)
                DB.AddInParameter(DBComm, "@DoneRate", DbType.Int64, SaleSolidGoldObj.DoneRate)

                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, SaleSolidGoldObj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, SaleSolidGoldObj.GoldTG)

                DB.AddInParameter(DBComm, "@CurrentGoldTK", DbType.Decimal, SaleSolidGoldObj.CurrentGoldTK)
                DB.AddInParameter(DBComm, "@CurrentGoldTG", DbType.Decimal, SaleSolidGoldObj.CurrentGoldTG)

                DB.AddInParameter(DBComm, "@SaleGoldTK", DbType.Decimal, SaleSolidGoldObj.SaleGoldTK)
                DB.AddInParameter(DBComm, "@SaleGoldTG", DbType.Decimal, SaleSolidGoldObj.SaleGoldTG)

                DB.AddInParameter(DBComm, "@LeftGoldTK", DbType.Decimal, SaleSolidGoldObj.LeftGoldTK)
                DB.AddInParameter(DBComm, "@LeftGoldTG", DbType.Decimal, SaleSolidGoldObj.LeftGoldTG)

                DB.AddInParameter(DBComm, "@TotalPayment", DbType.Int64, SaleSolidGoldObj.TotalPayment)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, SaleSolidGoldObj.AddOrSub)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, SaleSolidGoldObj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, SaleSolidGoldObj.PaidAmount)

                DB.AddInParameter(DBComm, "@Remark", DbType.String, SaleSolidGoldObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)

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

        Public Function UpdateSalesSolidGold(ByVal SaleSolidGoldObj As CommonInfo.SalesSolidGoldInfo) As Boolean Implements ISalesSolidGoldDA.UpdateSalesSolidGold
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleSolidGold set SaleDate=@SaleDate,StaffID=@StaffID,Customer=@Customer,Address=@Address,LocationID=@LocationID,CounterID=@CounterID,ForSaleID=@ForSaleID,ItemCode=@ItemCode,GoldQualityID=@GoldQualityID,ItemCategoryID=@ItemCategoryID,ItemName= @ItemName,SalesRate= @SalesRate,DoneRate= @DoneRate,GoldTK= @GoldTK,GoldTG= @GoldTG,CurrentGoldTK=@CurrentGoldTK,CurrentGoldTG= @CurrentGoldTG,SaleGoldTK= @SaleGoldTK,SaleGoldTG= @SaleGoldTG,LeftGoldTK= @LeftGoldTK,LeftGoldTG= @LeftGoldTG,TotalPayment= @TotalPayment,AddOrSub = @AddOrSub,DiscountAmount= @DiscountAmount,PaidAmount= @PaidAmount,Remark= @Remark,LastModifiedLoginUserName= @LastModifiedLoginUserName,LastModifiedDate= getDate()"
                strCommandText += " where SaleSolidGoldID= @SaleSolidGoldID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, SaleSolidGoldObj.SaleSolidGoldID)
                DB.AddInParameter(DBComm, "@SaleDate", DbType.Date, SaleSolidGoldObj.SaleDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, SaleSolidGoldObj.StaffID)
                DB.AddInParameter(DBComm, "@Customer", DbType.String, SaleSolidGoldObj.Customer)
                DB.AddInParameter(DBComm, "@Address", DbType.String, SaleSolidGoldObj.Address)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@CounterID", DbType.String, SaleSolidGoldObj.CounterID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, SaleSolidGoldObj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, SaleSolidGoldObj.ItemCode)

                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, SaleSolidGoldObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, SaleSolidGoldObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, SaleSolidGoldObj.ItemName)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, SaleSolidGoldObj.SalesRate)
                DB.AddInParameter(DBComm, "@DoneRate", DbType.Int64, SaleSolidGoldObj.DoneRate)

                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, SaleSolidGoldObj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, SaleSolidGoldObj.GoldTG)

                DB.AddInParameter(DBComm, "@CurrentGoldTK", DbType.Decimal, SaleSolidGoldObj.CurrentGoldTK)
                DB.AddInParameter(DBComm, "@CurrentGoldTG", DbType.Decimal, SaleSolidGoldObj.CurrentGoldTG)

                DB.AddInParameter(DBComm, "@SaleGoldTK", DbType.Decimal, SaleSolidGoldObj.SaleGoldTK)
                DB.AddInParameter(DBComm, "@SaleGoldTG", DbType.Decimal, SaleSolidGoldObj.SaleGoldTG)

                DB.AddInParameter(DBComm, "@LeftGoldTK", DbType.Decimal, SaleSolidGoldObj.LeftGoldTK)
                DB.AddInParameter(DBComm, "@LeftGoldTG", DbType.Decimal, SaleSolidGoldObj.LeftGoldTG)

                DB.AddInParameter(DBComm, "@TotalPayment", DbType.Int64, SaleSolidGoldObj.TotalPayment)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, SaleSolidGoldObj.AddOrSub)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, SaleSolidGoldObj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, SaleSolidGoldObj.PaidAmount)

                DB.AddInParameter(DBComm, "@Remark", DbType.String, SaleSolidGoldObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)

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

        'Public Function GetCurrentGoldWgtByForSaleID(ByVal ForSaleID As String) As CommonInfo.SalesSolidGoldInfo Implements ISalesSolidGoldDA.GetCurrentGoldWgtByForSaleID
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Dim drResult As IDataReader
        '    Dim objSaleSolid As New SalesSolidGoldInfo
        '    Try
        '        strCommandText = " SELECT  *,"
        '        strCommandText += " CAST(GoldTK AS INT) AS GoldK,  "
        '        strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
        '        strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
        '        strCommandText += " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
        '        strCommandText += " CAST(CurrentGoldTK AS INT) AS CurrentGoldK,"
        '        strCommandText += " CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT) AS CurrentGoldP,"
        '        strCommandText += " CAST((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8 AS INT) AS CurrentGoldY,"
        '        strCommandText += " CAST(((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8)-CAST((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS CurrentGoldC, "
        '        strCommandText += " CAST(SaleGoldTK AS INT) AS SaleGoldK,  "
        '        strCommandText += " CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT) AS SaleGoldP,"
        '        strCommandText += " CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS INT) AS SaleGoldY,"
        '        strCommandText += " CAST(((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8)-CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS SaleGoldC, "
        '        strCommandText += " CAST(LeftGoldTK AS INT) AS LeftGoldK, "
        '        strCommandText += " CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT) AS LeftGoldP,"
        '        strCommandText += " CAST((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8 AS INT) AS LeftGoldY,"
        '        strCommandText += " CAST(((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8)-CAST((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS LeftGoldC "
        '        strCommandText += "  FROM tbl_SaleSolidGold WHERE ForSaleID= @ForSaleID"
        '        strCommandText += " And SaleSolidGoldID = (select Max(SaleSolidGoldID) from tbl_SaleSolidGold where ForSaleID= @ForSaleID) "

        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
        '        drResult = DB.ExecuteReader(DBComm)
        '        If drResult.Read() Then
        '            With objSaleSolid

        '                .SaleSolidGoldID = drResult("SaleSolidGoldID")
        '                .SaleDate = drResult("SaleDate")
        '                .StaffID = drResult("StaffID")
        '                .Customer = drResult("Customer")
        '                .Address = drResult("Address")

        '                .ForSaleID = drResult("ForSaleID")
        '                .ItemCode = drResult("ItemCode")
        '                .ItemName = drResult("ItemName")
        '                .CounterID = drResult("CounterID")
        '                .LocationID = drResult("LocationID")

        '                .ItemCategoryID = drResult("ItemCategoryID")
        '                .GoldQualityID = drResult("GoldQualityID")
        '                .SalesRate = drResult("SalesRate")
        '                .DoneRate = drResult("DoneRate")

        '                .GoldK = drResult("GoldK")
        '                .GoldP = drResult("GoldP")
        '                .GoldY = drResult("GoldY")
        '                .GoldC = Format(drResult("GoldC"), "0.0")

        '                .CurrentGoldK = drResult("CurrentGoldK")
        '                .CurrentGoldP = drResult("CurrentGoldP")
        '                .CurrentGoldY = drResult("CurrentGoldY")
        '                .CurrentGoldC = Format(drResult("CurrentGoldC"), "0.0")

        '                .SaleGoldK = drResult("SaleGoldK")
        '                .SaleGoldP = drResult("SaleGoldP")
        '                .SaleGoldY = drResult("SaleGoldY")
        '                .SaleGoldC = Format(drResult("SaleGoldC"), "0.0")

        '                .LeftGoldK = drResult("LeftGoldK")
        '                .LeftGoldP = drResult("LeftGoldP")
        '                .LeftGoldY = drResult("LeftGoldY")
        '                .LeftGoldC = Format(drResult("LeftGoldC"), "0.0")

        '                .TotalPayment = drResult("TotalPayment")
        '                .AddOrSub = drResult("AddOrSub")
        '                .DiscountAmount = drResult("DiscountAmount")
        '                .PaidAmount = drResult("PaidAmount")
        '                .Remark = IIf(IsDBNull(drResult("Remark")), "-", drResult("Remark"))

        '                .GoldTK = drResult("GoldTK")
        '                .GoldTG = drResult("GoldTG")
        '                .CurrentGoldTK = drResult("CurrentGoldTK")
        '                .CurrentGoldTG = drResult("CurrentGoldTG")
        '                .SaleGoldTK = drResult("SaleGoldTK")
        '                .SaleGoldTG = drResult("SaleGoldTG")
        '                .LeftGoldTK = drResult("LeftGoldTK")
        '                .LeftGoldTG = drResult("LeftGoldTG")

        '            End With
        '        End If
        '        drResult.Close()
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
        '    End Try
        '    Return objSaleSolid
        'End Function

        Public Function GetLeftGoldWgtByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesSolidGoldDA.GetLeftGoldWgtByForSaleID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT  *,"
                strCommandText += " CAST(GoldTK AS INT) AS GoldK,  "
                strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT) AS GoldY,"
                strCommandText += " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8)-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GoldC, "
                strCommandText += " CAST(CurrentGoldTK AS INT) AS CurrentGoldK,"
                strCommandText += " CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT) AS CurrentGoldP,"
                strCommandText += " CAST((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8 AS INT) AS CurrentGoldY,"
                strCommandText += " CAST(((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8)-CAST((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS CurrentGoldC, "
                strCommandText += " CAST(SaleGoldTK AS INT) AS SaleGoldK,  "
                strCommandText += " CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT) AS SaleGoldP,"
                strCommandText += " CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS INT) AS SaleGoldY,"
                strCommandText += " CAST(((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8)-CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS SaleGoldC, "
                strCommandText += " CAST(LeftGoldTK AS INT) AS LeftGoldK, "
                strCommandText += " CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT) AS LeftGoldP,"
                strCommandText += " CAST((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS LeftGoldY "
                strCommandText += "  FROM tbl_SaleSolidGold WHERE ForSaleID= @ForSaleID"
                strCommandText += " And SaleSolidGoldID = (select Max(SaleSolidGoldID) from tbl_SaleSolidGold where ForSaleID= @ForSaleID) "
                'strCommandText += " CAST(((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8)-CAST((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS LeftGoldC "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesSolidGoldReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesSolidGoldDA.GetSalesSolidGoldReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select S.SaleDate,ST.Name,S.Customer,S.Address,L.Location,CO.Counter,I.ItemCategory,G.GoldQuality,S.ItemCode,S.ItemName,S.LocationID,S.GoldQualityID,S.ItemCategoryID,S.SalesRate,S.DoneRate,STA.Staff," & _
                    " CAST(GoldTK AS INT) AS GoldK, " & _
                    " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                    " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS GoldY," & _
                    " CAST(CurrentGoldTK AS INT) AS CurrentGoldK," & _
                    " CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT) AS CurrentGoldP," & _
                    " CAST((((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16)-CAST((CurrentGoldTK-CAST(CurrentGoldTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS CurrentGoldY," & _
                    " CAST(SaleGoldTK AS INT) AS SaleGoldK," & _
                    " CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT) AS SaleGoldP," & _
                    " CAST((((SaleGoldTK-CAST(SaleGoldTK AS INT))*16)-CAST((SaleGoldTK-CAST(SaleGoldTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS SaleGoldY," & _
                    " CAST(LeftGoldTK AS INT) AS LeftGoldK, " & _
                    " CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT) AS LeftGoldP," & _
                    " CAST((((LeftGoldTK-CAST(LeftGoldTK AS INT))*16)-CAST((LeftGoldTK-CAST(LeftGoldTK AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS LeftGoldY," & _
                    " S.GoldTK, S.CurrentGoldTK, S.SaleGoldTK, S.LeftGoldTK ,S.TotalPayment,S.PaidAmount,S.AddOrSub,S.DiscountAmount " & _
                    " from tbl_SaleSolidGold S Left Join tbl_StaffByLocation ST On S.StaffID = ST.SaleStaffID" & _
                    " Left Join tbl_Location L On S.LocationID=L.LocationID " & _
                    " left Join tbl_Counter CO On S.CounterID=CO.CounterID " & _
                    " left Join tbl_GoldQuality G On S.GoldQualityID=G.GoldQualityID " & _
                    " left Join tbl_ItemCategory I On S.ItemCategoryID=I.ItemCategoryID " & _
                    " left Join tbl_Staff STA On S.StaffID=STA.StaffID " & _
                    " Where SaleDate between @FromDate And @ToDate " & GetFilterString & "" & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetTotalGoldWeightbyDate(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String) As System.Data.DataTable Implements ISalesSolidGoldDA.GetTotalGoldWeightbyDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select S.ForSaleID,S.GoldTK from tbl_SaleSolidGold S " & _
                     " Where S.SaleDate between @FromDate And @ToDate " & GetFilterString & _
                     " group by S.ForSaleID,S.GoldTK "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
                DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesSolidGoldItem(ByVal SaleSolidGoldID As String) As System.Data.DataTable Implements ISalesSolidGoldDA.GetSalesSolidGoldItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT SaleSolidGoldItemID, GoldQualityID, SalesRate, GoldTK, GoldTG, Amount," & _
                " CAST(GoldTK AS INT) AS GoldK, " & _
                " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*7.5 AS Decimal(18,3)) AS GoldY " & _
                " FROM tbl_SaleSolidGoldItem I INNER JOIN tbl_SaleSolidGold H ON I.SaleSolidGoldID=H.SaleSolidGoldID " & _
                " WHERE H.SaleSolidGoldID=@SaleSolidGoldID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, SaleSolidGoldID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function DeleteSalesSolidGoldItem(ByVal SalesSolidGoldItemID As String) As Boolean Implements ISalesSolidGoldDA.DeleteSalesSolidGoldItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SaleSolidGoldItem WHERE  SaleSolidGoldItemID= @SaleSolidGoldItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesSolidGoldItemID", DbType.String, SalesSolidGoldItemID)
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

        Public Function InsertSalesSolidGoldItem(ByVal objItem As CommonInfo.SalesSolidGoldItemInfo) As Boolean Implements ISalesSolidGoldDA.InsertSalesSolidGoldItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_SaleSolidGoldItem (SaleSolidGoldItemID, SaleSolidGoldID, GoldQualityID,SalesRate,GoldTK,GoldTG,Amount)"
                strCommandText += " Values (@SaleSolidGoldItemID, @SaleSolidGoldID, @GoldQualityID, @SalesRate, @GoldTK, @GoldTG, @Amount)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                With objItem
                    DB.AddInParameter(DBComm, "@SaleSolidGoldItemID", DbType.String, .SaleSolidGoldItemID)
                    DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, .SaleSolidGoldID)
                    DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, .GoldQualityID)
                    DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, .SalesRate)
                    DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, .GoldTK)
                    DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, .GoldTG)
                    DB.AddInParameter(DBComm, "@Amount", DbType.Int64, .Amount)
                End With

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

        Public Function UpdateSalesSolidGoldItem(ByVal objItem As CommonInfo.SalesSolidGoldItemInfo) As Boolean Implements ISalesSolidGoldDA.UpdateSalesSolidGoldItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "UPDATE tbl_SaleSolidGoldItem SET SaleSolidGoldID=@SaleSolidGoldID, GoldQualityID=@GoldQualityID,SalesRate=@SalesRate,GoldTK=@GoldTK,GoldTG=@GoldTG,Amount=@Amount "
                strCommandText += " WHERE SaleSolidGoldItemID=@SaleSolidGoldItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                With objItem
                    DB.AddInParameter(DBComm, "@SaleSolidGoldID", DbType.String, .SaleSolidGoldID)
                    DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, .GoldQualityID)
                    DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, .SalesRate)
                    DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, .GoldTK)
                    DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, .GoldTG)
                    DB.AddInParameter(DBComm, "@Amount", DbType.Int64, .Amount)
                    DB.AddInParameter(DBComm, "@SaleSolidGoldItemID", DbType.String, .SaleSolidGoldItemID)
                End With

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

