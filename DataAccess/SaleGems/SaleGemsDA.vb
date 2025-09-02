Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace SaleGems
    Public Class SaleGemsDA
        Implements ISaleGemsDA
#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As ISaleGemsDA = New SaleGemsDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISaleGemsDA
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteSaleGems(ByVal SaleGemsID As String) As Boolean Implements ISaleGemsDA.DeleteSaleGems
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_SaleGems Set IsDelete=1 WHERE SaleGemsID = @SaleGemsID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsID)
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

        Public Function DeleteSaleGemsItem(ByVal SaleGemsItemID As String) As Boolean Implements ISaleGemsDA.DeleteSaleGemsItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SaleGemsItem WHERE SaleGemsItemID= @SaleGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsItemID", DbType.String, SaleGemsItemID)
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

        Public Function GetSaleGems(ByVal SaleGemsID As String) As CommonInfo.SaleGemsInfo Implements ISaleGemsDA.GetSaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SaleGemsInfo
            Try
                strCommandText = " Select SaleGemsID as [@SaleGemsID],SDate,StaffID as [@StaffID],CustomerID,TotalAmount,(TotalAmount*PromotionDiscount)/100 as PromotionAmount,"
                strCommandText += "  ((TotalAmount + AddOrSub)-(DiscountAmount+((TotalAmount*PromotionDiscount)/100))) As NetAmount, AddOrSub,PaidAmount,Remark,DiscountAmount,PromotionDiscount,IsOtherCash,OtherCashAmount,PurchaseHeaderID,PurchaseAmount from tbl_SaleGems  WHERE SaleGemsID= @SaleGemsID and IsDelete=0 Order by SaleGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .SaleGemsID = drResult("@SaleGemsID")
                        .SDate = drResult("SDate")
                        .StaffID = drResult("@StaffID")
                        .CustomerID = drResult("CustomerID")
                        '.Address = drResult("Address")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .PaidAmount = drResult("PaidAmount")
                        .Remark = drResult("Remark")
                        .DiscountAmount = drResult("DiscountAmount")
                        .PromotionDiscount = drResult("PromotionDiscount")
                        .PromotionAmount = drResult("PromotionAmount")
                        .NetAmount = drResult("NetAmount")
                        .IsOtherCash = drResult("IsOtherCash")
                        .OtherCashAmount = drResult("OtherCashAmount")
                        .PurchaseHeaderID = drResult("PurchaseHeaderID")
                        .PurchaseAmount = drResult("PurchaseAmount")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetSaleGemsItem(ByVal SaleGemsID As String) As System.Data.DataTable Implements ISaleGemsDA.GetSaleGemsItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT SaleGemsItemID,SaleGemsID,GemsCategoryID,GemsName,Clarity,SizeMM,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, "
                strCommandText += " GemsTK, GemsTG, YOrCOrG, GemsTW, QTY, CASE FixType WHEN 1 THEN 'Fix' WHEN 2 THEN 'ByWeight' WHEN 3 THEN 'ByQty' END AS FixType,SaleRate,Amount,IsReturn "
                strCommandText += " From tbl_SaleGemsItem Where SaleGemsID = '" & SaleGemsID & "' Order By SaleGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertSaleGems(ByVal SaleGemsObj As CommonInfo.SaleGemsInfo) As Boolean Implements ISaleGemsDA.InsertSaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_SaleGems ( SaleGemsID,SDate,StaffID,CustomerID,TotalAmount,AddOrSub,PaidAmount,DiscountAmount,PromotionDiscount,Remark,LastModifiedLoginUserName,LastModifiedDate,LocationID,PurchaseHeaderID,PurchaseAmount,IsOtherCash,OtherCashAmount,IsDelete,IsSync)"
                strCommandText += " Values (@SaleGemsID,@SDate,@StaffID,@CustomerID,@TotalAmount,@AddOrSub,@PaidAmount,@DiscountAmount,@PromotionDiscount,@Remark,@LastModifiedLoginUserName,@LastModifiedDate,@LocationID,@PurchaseHeaderID,@PurchaseAmount,@IsOtherCash,@OtherCashAmount,0,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsObj.SaleGemsID)
                DB.AddInParameter(DBComm, "@SDate", DbType.DateTime, SaleGemsObj.SDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, SaleGemsObj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, SaleGemsObj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, SaleGemsObj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, SaleGemsObj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, SaleGemsObj.PaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, SaleGemsObj.Remark)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int32, SaleGemsObj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PromotionDiscount", DbType.Int32, SaleGemsObj.PromotionDiscount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, SaleGemsObj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, SaleGemsObj.PurchaseAmount)
                DB.AddInParameter(DBComm, "@IsOtherCash", DbType.Boolean, SaleGemsObj.IsOtherCash)
                DB.AddInParameter(DBComm, "@OtherCashAmount", DbType.Int64, SaleGemsObj.OtherCashAmount)

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

        Public Function InsertSaleGemsItem(ByVal ObjSaleGemsItem As CommonInfo.SaleGemsItemInfo) As Boolean Implements ISaleGemsDA.InsertSaleGemsItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_SaleGemsItem ( SaleGemsItemID,SaleGemsID,GemsCategoryID,GemsName,Clarity,SizeMM,GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,FixType,SaleRate,Amount,IsReturn)"
                strCommandText += " Values (@SaleGemsItemID,@SaleGemsID,@GemsCategoryID,@GemsName,@Clarity,@SizeMM,@GemsTK,@GemsTG,@YOrCOrG,@GemsTW,@Qty,@FixType,@SaleRate,@Amount,@IsReturn)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsItemID", DbType.String, ObjSaleGemsItem.SaleGemsItemID)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, ObjSaleGemsItem.SaleGemsID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, ObjSaleGemsItem.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, ObjSaleGemsItem.GemsName)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, ObjSaleGemsItem.Clarity)
                DB.AddInParameter(DBComm, "@SizeMM", DbType.String, ObjSaleGemsItem.SizeMM)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjSaleGemsItem.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjSaleGemsItem.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjSaleGemsItem.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, ObjSaleGemsItem.GemTW)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, ObjSaleGemsItem.Qty)
                DB.AddInParameter(DBComm, "@FixType", DbType.Int32, ObjSaleGemsItem.FixType)
                DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, ObjSaleGemsItem.SaleRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, ObjSaleGemsItem.Amount)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, ObjSaleGemsItem.IsReturn)

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

        Public Function UpdateSaleGems(ByVal SaleGemsObj As CommonInfo.SaleGemsInfo) As Boolean Implements ISaleGemsDA.UpdateSaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleGems set  SDate= @SDate , StaffID= @StaffID , CustomerID= @CustomerID , TotalAmount= @TotalAmount , AddOrSub= @AddOrSub , PaidAmount= @PaidAmount ,DiscountAmount=@DiscountAmount,PromotionDiscount=@PromotionDiscount, Remark= @Remark ,LocationID=@LocationID, LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate,PurchaseHeaderID=@PurchaseHeaderID,PurchaseAmount=@PurchaseAmount,IsOtherCash=@IsOtherCash,OtherCashAmount=@OtherCashAmount,IsSync=0 "
                strCommandText += " where SaleGemsID= @SaleGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsObj.SaleGemsID)
                DB.AddInParameter(DBComm, "@SDate", DbType.DateTime, SaleGemsObj.SDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, SaleGemsObj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, SaleGemsObj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, SaleGemsObj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, SaleGemsObj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, SaleGemsObj.PaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, SaleGemsObj.Remark)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int32, SaleGemsObj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PromotionDiscount", DbType.Int32, SaleGemsObj.PromotionDiscount)
                DB.AddInParameter(DBComm, "@PromotionAmount", DbType.Int32, SaleGemsObj.PromotionAmount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, SaleGemsObj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, SaleGemsObj.PurchaseAmount)
                DB.AddInParameter(DBComm, "@IsOtherCash", DbType.Boolean, SaleGemsObj.IsOtherCash)
                DB.AddInParameter(DBComm, "@OtherCashAmount", DbType.Int64, SaleGemsObj.OtherCashAmount)

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

        Public Function UpdateSaleGemsItem(ByVal Obj As CommonInfo.SaleGemsItemInfo) As Boolean Implements ISaleGemsDA.UpdateSaleGemsItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleGemsItem set SaleGemsID= @SaleGemsID , GemsCategoryID= @GemsCategoryID , GemsName= @GemsName, Clarity=@Clarity,SizeMM=@SizeMM,GemsTK=@GemsTK, GemsTG=@GemsTG,YOrCOrG= @YOrCOrG , GemsTW= @GemsTW , Qty = @Qty , FixType= @FixType , SaleRate= @SaleRate , Amount= @Amount "
                strCommandText += " where SaleGemsItemID= @SaleGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsItemID", DbType.String, Obj.SaleGemsItemID)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, Obj.SaleGemsID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, Obj.Clarity)
                DB.AddInParameter(DBComm, "@SizeMM", DbType.String, Obj.SizeMM)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemTW)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
                DB.AddInParameter(DBComm, "@FixType", DbType.Int32, Obj.FixType)
                DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, Obj.SaleRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)

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


        Public Function GetAllSaleGems() As System.Data.DataTable Implements ISaleGemsDA.GetAllSaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "Select S.SaleGemsID as VoucherNo ,convert(varchar(10),S.SDate,105) As SaleDate,S.StaffID As [@StaffID],ST.Staff as [Staff_],S.CustomerID as [@CustomerID], C.CustomerName As [Customer_], S.PaidAmount,S.TotalAmount, S.DiscountAmount, S.PromotionDiscount,S.SDate as [@SDate],S.PurchaseHeaderID "
                strCommandText += " From tbl_SaleGems S Left Join tbl_Staff ST On S.StaffID=ST.StaffID LEFT JOIN tbl_Customer C On C.CustomerID=S.CustomerID where S.IsDelete =0 Order by [@SDate] desc,SaleGemsID desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllSaleGemsForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsDA.GetAllSaleGemsForRpt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "Select S.SaleGemsID,S.SDate,S.StaffID As [@StaffID],ST.Staff,S.CustomerID, C.CustomerName As Customer, C.CustomerAddress As Address, S.TotalAmount,S.Remark,S.AddOrSub,S.PaidAmount,L.Location "
                strCommandText += " From tbl_SaleGems S Left Join tbl_Staff ST On S.StaffID=ST.StaffID LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID Left Join tbl_Location L on S.LocationID = L.LocationID "
                strCommandText += " Where 1=1 and S.IsDelete=0 And S.SDate between @FromDate And @ToDate " & cristr & " Order by S.SDate"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleGemsItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsDA.GetSaleGemsItemForRpt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "Select S.SaleGemsID,G.GemsCategoryID,S.SDate,S.CustomerID, Cu.CustomerName As Customer, Cu.CustomerAddress As Address, S.StaffID, F.Staff, S.TotalAmount,S.AddOrSub,S.PaidAmount,S.Remark,S.DiscountAmount,S.PromotionDiscount," &
                                " ((S.TotalAmount+S.AddOrSub)-(((S.TotalAmount*PromotionDiscount)/100)+S.DiscountAmount)) As NetAmount,((S.TotalAmount*PromotionDiscount)/100) As PromotionAmount," &
                                " C.GemsCategory,G.GemsName,G.GemsTK,CAST((G.GemsTG) as DECIMAL(18,3)) AS GemsTG,G.YOrCOrG,G.GemsTW,G.Qty," &
                                " CASE G.FixType  WHEN 1 Then 'Fix' WHEN 2 Then 'ByWeight' WHEN 3 Then 'ByQty' end as FixType,G.SaleRate,G.Amount,F.Staff," &
                                " CAST(GemsTK AS INT) AS GemsK, CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," &
                                " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, S.SDate AS [@SDate] From tbl_SaleGems S" &
                                " Inner Join tbl_SaleGemsItem G on G.SaleGemsID=S.SaleGemsID Inner Join tbl_GemsCategory C on C.GemsCategoryID=G.GemsCategoryID" &
                                " LEFT JOIN tbl_Customer Cu ON Cu.CustomerID=S.CustomerID " &
                                " Inner Join tbl_Staff F on F.StaffID=S.StaffID" &
                                " Where 1=1 AND S.IsDelete=0 AND S.SDate BETWEEN @FromDate And @ToDate " & cristr & " Order by [@SDate] DESC, S.SaleGemsID, SaleGemsItemID ASC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllSaleGem() As System.Data.DataTable Implements ISaleGemsDA.GetAllSaleGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "Select S.SaleGemsID ,convert(varchar(10),S.SDate,105) As SaleDate,S.StaffID As [@StaffID],ST.Name as [Name_], S.CustomerID AS [@CustomerID], C.CustomerName as [Customer_], S.PaidAmount,S.TotalAmount,S.Remark as [Remark_] "
                strCommandText += " From tbl_SaleGems S Left Join tbl_StaffByLocation ST On S.StaffID=ST.SaleStaffID LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID where S.IsDelete=0 Order by SaleGemsID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSaleGemsPrint(ByVal SaleGemsID As String) As System.Data.DataTable Implements ISaleGemsDA.GetSaleGemsPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.SaleGemsID, SDate, H.StaffID, S.Staff as Staff, H.CustomerID,C.CustomerName AS Customer, C.CustomerAddress AS Address,C.CustomerTel,H.TotalAmount,H.AddOrSub,GI.SaleRate, H.PaidAmount,H.LastModifiedLoginUserName, " & _
                " L.Location,GI.GemsCategoryID,I.GemsCategory, GI.Qty, GI.YOrCOrG, GI.GemsName, GI.Clarity,GI.SizeMM,GI.GemsTK, GI.GemsTG, GI.GemsTW, GI.FixType, GI.SaleRate, GI.Amount,  " & _
                " CAST(GI.GemsTK AS INT) AS GemsK, " & _
                " CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                " CAST((((GI.GemsTK-CAST(GI.GemsTK AS INT))*16)-CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, " & _
                " U.UserName,H.DiscountAmount,H.PromotionDiscount,(H.TotalAmount*PromotionDiscount)/100 As PromotionAmount,H.IsOtherCash,H.OtherCashAmount,'' as QRCode,H.PurchaseHeaderID,H.PurchaseAmount  " & _
                " FROM tbl_SaleGems H " & _
                " LEFT JOIN tbl_SaleGemsItem GI ON GI.SaleGemsID=H.SaleGemsID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                " LEFT JOIN tbl_GemsCategory I ON I.GemsCategoryID=GI.GemsCategoryID " & _
                " LEFT JOIN tb_GE_SystemUser U ON H.UserID=U.UserID " & _
                " WHERE H.SaleGemsID= @SaleGemsID and H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertSaleGemsUserID(ByVal SaleGemsID As String, ByVal UserID As String) As Boolean Implements ISaleGemsDA.InsertSaleGemsUserID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleGems set   UserID= @UserID "
                strCommandText += " where SaleGemsID= @SaleGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsID)
                DB.AddInParameter(DBComm, "@UserID", DbType.String, UserID)
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
        Public Function GetSaleGemsReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsDA.GetSaleGemsReportForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "Select  Distinct(S.SaleGemsID), S.TotalAmount, S.PaidAmount, " & _
                                 " (S.TotalAmount+S.AddOrSub)-(S.DiscountAmount+((S.TotalAmount*S.PromotionDiscount)/100)) As NetAmount" & _
                                " From tbl_SaleGems S" & _
                                " Inner Join tbl_SaleGemsItem G on G.SaleGemsID=S.SaleGemsID Inner Join tbl_GemsCategory C on C.GemsCategoryID=G.GemsCategoryID" & _
                                " Inner Join tbl_Staff F on F.StaffID=S.StaffID " & _
                                " Where 1=1 and S.IsDelete=0 And S.SDate BETWEEN @FromDate And @ToDate " & cristr
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleGemsBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As CommonInfo.SaleGemsItemInfo Implements ISaleGemsDA.GetSaleGemsBalanceStockByGemsCategoryID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SaleGemsItemInfo
            Try
                strCommandText = "Select M.GemsCategoryID, M.GemsCategory, Sum(M.TotalQTY) AS TotalQTY, Sum(M.SaleQTY) As SaleQTY, Sum(M.TotalGemsTK) AS TotalGemsTK, Sum(M.TotalGemsTG) AS TotalGemsTG," & _
                               " Sum(M.SaleGemsTK) AS SaleGemsTK, Sum(M.SaleGemsTG) AS SaleGemsTG, SUM(M.TotalQTY-M.SaleQTY) AS BalanceQTY, SUM(M.TotalGemsTK-M.SaleGemsTK) As BalanceGemsTK, SUM(M.TotalGemsTG-M.SaleGemsTG) As BalanceGemsTG, " & _
                               " CAST(SUM(M.TotalGemsTK-M.SaleGemsTK) AS INT) AS BalanceGemsK," & _
                               " CAST((SUM(M.TotalGemsTK-M.SaleGemsTK)-CAST(SUM(M.TotalGemsTK-M.SaleGemsTK) AS INT))*16 AS INT) AS BalanceGemsP," & _
                               " CAST((((SUM(M.TotalGemsTK-M.SaleGemsTK)-CAST(SUM(M.TotalGemsTK-M.SaleGemsTK) AS INT))*16)-CAST((SUM(M.TotalGemsTK-M.SaleGemsTK)-CAST(SUM(M.TotalGemsTK-M.SaleGemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS BalanceGemsY " & _
                               " FROM (select PD.GemsCategoryID, G.GemsCategory, PD.QTY AS TotalQTY, 0 AS SaleQTY, PD.GemsTK AS TotalGemsTK, " & _
                               " PD.GemsTG AS TotalGemsTG, 0 AS SaleGemsTK, 0 AS SaleGemsTG from tbl_PurchaseOutItemDetail PD " & _
                               " LEFT JOIN tbl_GemsCategory G On G.GemsCategoryID=PD.GemsCategoryID Where DivideType='Stock' AND PD.GemsCategoryID=@GemsCategoryID " & _
                               " UNION ALL" & _
                               " select PD.GemsCategoryID, G.GemsCategory, 0 AS TotalQTY, PD.Qty AS SaleQTY, 0 AS TotalGemsTK, 0 AS TotalGemsTG," & _
                               " PD.GemsTK AS SaleGemsTK, PD.GemsTG AS SaleTG   " & _
                               " from tbl_SaleGemsItem PD LEFT JOIN tbl_GemsCategory G On G.GemsCategoryID=PD.GemsCategoryID Where PD.GemsCategoryID=@GemsCategoryID and PD.IsDelete=0 ) AS M GROUP BY  M.GemsCategoryID, M.GemsCategory "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, GemsCategoryID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .GemsCategory = drResult("GemsCategory")
                        .BalanceQTY = drResult("BalanceQTY")
                        .BalanceGemsTG = drResult("BalanceGemsTG")
                        .BalanceGemsTK = drResult("BalanceGemsTK")
                        .BalanceGemsK = drResult("BalanceGemsK")
                        .BalanceGemsP = drResult("BalanceGemsP")
                        .BalanceGemsY = drResult("BalanceGemsY")
                        .Qty = drResult("SaleQTY")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Private Function DiscountAmount() As Object
            Throw New NotImplementedException
        End Function

        Private Function PromotionDiscount() As Object
            Throw New NotImplementedException
        End Function
        Public Function GetAllSaleGemsVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsDA.GetAllSaleGemsVoucherPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.SaleGemsID, SDate, S.Staff as Staff, H.CustomerID, C.CustomerName AS Customer, C.CustomerAddress As Address, H.TotalAmount,H.AddOrSub,GI.SaleRate, H.PaidAmount,H.LastModifiedLoginUserName, " & _
                " L.Location, I.GemsCategory, GI.Qty, GI.YOrCOrG, GI.GemsName, GI.Clarity,GI.SizeMM,GI.GemsTK, GI.GemsTG, GI.GemsTW, GI.FixType, GI.SaleRate, GI.Amount,  " & _
                " CAST(GI.GemsTK AS INT) AS GemsK, " & _
                " CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                " CAST((((GI.GemsTK-CAST(GI.GemsTK AS INT))*16)-CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, " & _
                " U.UserName,H.DiscountAmount,H.PromotionDiscount,(H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, SDate AS [@SDate]  " & _
                " FROM tbl_SaleGems H " & _
                " LEFT JOIN tbl_SaleGemsItem GI ON GI.SaleGemsID=H.SaleGemsID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                " LEFT JOIN tbl_GemsCategory I ON I.GemsCategoryID=GI.GemsCategoryID " & _
                " LEFT JOIN tb_GE_SystemUser U ON H.UserID=U.UserID " & _
                 " WHERE SDate BETWEEN @FromDate AND @ToDate and H.IsDelete=0  " & cristr & " ORDER BY [@SDate] DESC, H.SaleGemsID ASC"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleGemsItemBySaleGemsItemID(ByVal SaleGemsItemID As String) As System.Data.DataTable Implements ISaleGemsDA.GetSaleGemsItemBySaleGemsItemID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT SaleGemsItemID,SaleGemsID,GemsCategoryID,GemsName,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, "
                strCommandText += " GemsTK, GemsTG, YOrCOrG, GemsTW, QTY, CASE FixType WHEN 1 THEN 'Fix' WHEN 2 THEN 'ByWeight' WHEN 3 THEN 'ByQty' END AS FixType,SaleRate,Amount "
                strCommandText += " From tbl_SaleGemsItem Where SaleGemsItemID = '" & SaleGemsItemID & "' Order By SaleGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleGemsDataByCustomerIDAndItemCode(CustomerID As String, Optional argForSaleIDStr As String = "") As DataTable Implements ISaleGemsDA.GetSaleGemsDataByCustomerIDAndItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String = ""

            If CustomerID <> "" Then
                strWhere = argForSaleIDStr + " AND H.CustomerID='" & CustomerID & "' "
            Else
                strWhere = argForSaleIDStr
            End If
            Try
                strCommandText = " SELECT S.SaleGemsID as VoucherNo ,convert(varchar(10),H.SDate,105) As SaleDate,H.StaffID As [@StaffID],ST.Staff as [Staff_], " & _
                                 "H.CustomerID as [@CustomerID], C.CustomerName As [Customer_], H.PaidAmount,CASE WHEN (select count(SaleGemsItemID) " & _
                                " from tbl_SaleGemsItem where SaleGemsID=H.SaleGemsID AND IsReturn=0)=1 THEN CONVERT(varchar,((H.TotalAmount +H.AddOrSub)-H.DiscountAmount)) ELSE CONVERT(varchar,(S.Amount)) END as TotalAmount, H.DiscountAmount, H.PromotionDiscount, " & _
                                 "H.SDate as [@SDate] ,'' as [@SaleInvoiceDetailID],SaleGemsItemID,S.SaleGemsID,S.GemsCategoryID,GemsName,Clarity,SizeMM, CAST(S.GemsTK AS INT) AS GemsK, " & _
                                 "CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 "CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,3)) AS GemsY,  " & _
                                 "GemsTK, GemsTG, YOrCOrG, GemsTW, QTY, CASE FixType WHEN 1 THEN 'Fix' WHEN 2 THEN 'ByWeight' WHEN 3 THEN 'ByQty' END AS FixType,SaleRate,Amount,S.IsReturn  " & _
                                 "FROM tbl_SaleGemsItem S  LEFT JOIN tbl_SaleGems H ON H.SaleGemsID=S.SaleGemsID " & _
                                 "Left Join tbl_Staff ST On H.StaffID=ST.StaffID " & _
                                 "LEFT JOIN tbl_Customer C On C.CustomerID=H.CustomerID  " & _
                                 "LEFT JOIN tbl_GemsCategory G ON S.GemsCategoryID=G.GemsCategoryID " & _
                                 "where S.IsReturn=0 AND H.IsDelete=0 " & strWhere & " ORDER BY [@SDate]  DESC, VoucherNo DESC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        'OtherCash For Chein TMN 07022019
        Public Function InsertRecordCash(ByVal Obj As CommonInfo.OtherCashInfo) As Boolean Implements ISaleGemsDA.InsertRecordCash
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_RecordOtherCash(RecordCashID, VoucherNo, CashTypeID, ExchangeRate, Amount)"
                strCommandText += " VALUES(@RecordCashID, @VoucherNo, @CashTypeID, @ExchangeRate, @Amount)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RecordCashID", DbType.String, Obj.RecordCashID)
                DB.AddInParameter(DBComm, "@VoucherNo", DbType.String, Obj.VoucherNo)
                DB.AddInParameter(DBComm, "@CashTypeID", DbType.String, Obj.CashTypeID)
                DB.AddInParameter(DBComm, "@ExchangeRate", DbType.Int32, Obj.ExchangeRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)

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
        Public Function UpdateRecordCash(ByVal Obj As CommonInfo.OtherCashInfo) As Boolean Implements ISaleGemsDA.UpdateRecordCash
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "UPDATE tbl_RecordOtherCash SET VoucherNo=@VoucherNo, CashTypeID=@CashTypeID, ExchangeRate=@ExchangeRate, Amount=@Amount "
                strCommandText += " WHERE RecordCashID=@RecordCashID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RecordCashID", DbType.String, Obj.RecordCashID)
                DB.AddInParameter(DBComm, "@VoucherNo", DbType.String, Obj.VoucherNo)
                DB.AddInParameter(DBComm, "@CashTypeID", DbType.String, Obj.CashTypeID)
                DB.AddInParameter(DBComm, "@ExchangeRate", DbType.Int32, Obj.ExchangeRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)

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

        Public Function DeleteRecordCash(ByVal RecordCashID As String) As Boolean Implements ISaleGemsDA.DeleteRecordCash
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_RecordOtherCash WHERE  RecordCashID= @RecordCashID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RecordCashID", DbType.String, RecordCashID)
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

        Public Function GetOtherCashDataByVoucherNo(ByVal SaleGemsID As String) As DataTable Implements ISaleGemsDA.GetOtherCashDataByVoucherNo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT RecordCashID, VoucherNo, R.CashTypeID, C.CashType, ExchangeRate, Amount, (ExchangeRate*Amount) AS Total FROM tbl_RecordOtherCash R LEFT JOIN tbl_CashType C ON R.CashTypeID=C.CashTypeID WHERE  VoucherNo=@VoucherNo "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@VoucherNo", DbType.String, SaleGemsID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

