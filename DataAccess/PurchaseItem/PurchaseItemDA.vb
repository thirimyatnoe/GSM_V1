Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Operational.AppConfiguration
Imports System.Data.Common
Namespace PurchaseItem
    Public Class PurchaseItemDA
        Implements IPurchaseItemDA

#Region "Private Purchase Item"

        Private DB As Database
        Private Shared ReadOnly _instance As IPurchaseItemDA = New PurchaseItemDA
        Private IsUpload As Integer = AppConfiguration.ReadAppSettings("IsUpload")
#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IPurchaseItemDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeletePuchasDetail(PurchaseDetailID As String) As Boolean Implements IPurchaseItemDA.DeletePuchasDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_PurchaseDetail WHERE  PurchaseDetailID= @PurchaseDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseDetailID", DbType.String, PurchaseDetailID)
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

        Public Function DeletePuchaseGems(PurchaseGemID As String) As Boolean Implements IPurchaseItemDA.DeletePuchaseGems
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_PurchaseGem where PurchaseGemID=@PurchaseGemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemID", DbType.String, PurchaseGemID)
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

        Public Function DeletePuchaseGemsByDetailID(PurchaseDetailID As String) As Boolean Implements IPurchaseItemDA.DeletePuchaseGemsByDetailID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_PurchaseGem where PurchaseDetailID=@PurchaseDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseDetailID", DbType.String, PurchaseDetailID)
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

        Public Function DeletePuchaseHeader(PurchaseHeaderID As String) As Boolean Implements IPurchaseItemDA.DeletePuchaseHeader

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_PurchaseHeader SET IsDelete=1,LastModifiedDate=GetDate()  WHERE  PurchaseHeaderID= @PurchaseHeaderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
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

        Public Function GetAllPuchaseHeader() As DataTable Implements IPurchaseItemDA.GetAllPuchaseHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select PH.IsChange AS [$IsChange], C.CustomerName AS [CustomerName_], PH.PurchaseHeaderID AS VoucherNo,Convert(varchar(10),PurchaseDate,105) as PurchaseDate,  S.Staff AS [Staff_], PH.Remark AS [Remark_], AllTotalAmount, AllAddOrSub,AllPaidAmount,PH.PurchaseDate as [@PDate]"
                strCommandText += "   from tbl_PurchaseHeader PH INNER join tbl_Staff S on PH.StaffID=S.StaffID INNER join tbl_Customer C On C.CustomerID=PH.CustomerID and PH.IsDelete=0 order by [@PDate] desc,VoucherNo desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseDetailGemByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseDetailGemByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select PG.PurchaseGemID,PG.PurchaseDetailID,PG.GemsCategoryID,PG.GemsName,PG.GemsTK,PG.GemsTG,"
                strCommandText += " CAST(PG.GemsTK AS INT) AS GemsK, "
                strCommandText += " CAST((PG.GemsTK-CAST(PG.GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((PG.GemsTK-CAST(PG.GemsTK AS INT))*16)-CAST((PG.GemsTK-CAST(PG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, "
                strCommandText += " PG.YOrCOrG, PG.GemTW, PG.QTY, PG.FixType, PG.PurchaseRate, PG.Amount, PG.Discount "
                strCommandText += " from tbl_PurchaseGem PG left join tbl_PurchaseDetail PD on PG.PurchaseDetailID=PD.PurchaseDetailID "
                strCommandText += " where PD.PurchaseHeaderID=@PurchaseHeaderID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseDetailByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseDetailByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select  ROW_NUMBER() OVER (ORDER BY PD.PurchaseDetailID) AS SNo, PD.PurchaseDetailID,PD.PurchaseHeaderID, PD.SaleInvoiceDetailID, PD.ForSaleID,PD.BarcodeNo,PD.OldSaleAmount,PD.ItemCategoryID, '' As GemsCategory, C.ItemCategory As ItemCategory, PD.ItemNameID,PD.GoldQualityID,G.GoldQuality,PD.CurrentPrice, IsNull(CD.ConsignmentSaleItemID,'') AS ConsignmentSaleItemID, "
                strCommandText += " PD.SaleRate, IsNull(SD.IsFixPrice,0) AS IsFixPrice, PD.TotalTK,PD.TotalTG,PD.GoldTK,PD.GoldTG,PD.TotalGemTK,PD.TotalGemTG, PD.WasteTK, PD.WasteTG, PD.PWasteTK, PD.PWasteTG, "
                strCommandText += " PD.Length,PD.QTY,PD.IsDamage,PD.IsChange,PD.TotalAmount, YOrCOrG, GemTW, FixType,Case PH.IsGem WHEN 1 THEN PD.ItemName   ELSE '' END AS GemsName, Case PH.IsGem WHEN 0 THEN I.ItemName   ELSE '' END AS ItemName, IsClose, "
                strCommandText += " PD.GoldPrice, PD.GemsPrice , PD.IsDone, PD.DoneAmount, PD.IsSalePercent, PD.SalePercent, PD.SalePercentAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub) AS NetAmount, PD.IsOrder, PD.IsShop, IsNull(F.Photo,'') AS Photo , IsNull(F.OriginalCode,'') AS OriginalCode, IsNull(F.IsDiamond,0) AS IsDiamond,'' as SaleGemsItemID,'' as SaleLooseDiamondDetailID,'' as PGemsCategoryID,'' as PGemsName,'' Color,'' as Shape,'' as Clarity "
                strCommandText += " from tbl_PurchaseDetail PD left join tbl_PurchaseHeader PH on PD.PurchaseHeaderID=PH.PurchaseHeaderID left join tbl_Itemcategory C on PD.ItemCategoryID=C.ItemCategoryID left join tbl_ItemName I On I.ItemNameID=PD.ItemNameID left join tbl_GoldQuality G on PD.GoldQualityID=G.GoldQualityID "
                strCommandText += " LEFT JOIN tbl_SaleInvoiceDetail SD ON PD.SaleInvoiceDetailID=SD.SaleInvoiceDetailID "
                strCommandText += " LEFT JOIN tbl_ConsignmentSaleItem CD ON CD.ConsignmentSaleItemID=PD.ConsignmentSaleItemID "
                strCommandText += " LEFT JOIN tbl_ForSale F ON F.ForSaleID=PD.ForSaleID "
                strCommandText += " where PD.PurchaseHeaderID=@PurchaseHeaderID And PH.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseGemByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseGemByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select  ROW_NUMBER() OVER (ORDER BY PD.PurchaseDetailID) AS SNo, PD.PurchaseDetailID,PD.PurchaseHeaderID, PD.SaleInvoiceDetailID, PD.ForSaleID,PD.BarcodeNo,PD.OldSaleAmount,PD.ItemCategoryID, GC.GemsCategory As GemsCategory, '' As ItemCategory, PD.ItemNameID,PD.GoldQualityID,G.GoldQuality,PD.CurrentPrice, "
                strCommandText += " PD.SaleRate, IsNull(SD.IsFixPrice,0) AS IsFixPrice, PD.TotalTK,PD.TotalTG,PD.GoldTK,PD.GoldTG,PD.TotalGemTK,PD.TotalGemTG, PD.WasteTK, PD.WasteTG, PD.PWasteTK, PD.PWasteTG, "
                strCommandText += " PD.Length,PD.QTY,PD.IsDamage,PD.IsChange,PD.TotalAmount, YOrCOrG, GemTW, FixType,Case PH.IsGem WHEN 1 THEN PD.ItemName   ELSE '' END AS GemsName, Case PH.IsGem WHEN 0 THEN I.ItemName   ELSE '' END AS ItemName, IsClose, "
                strCommandText += " PD.GoldPrice, PD.GemsPrice , PD.IsDone, PD.DoneAmount, PD.IsSalePercent, PD.SalePercent, PD.SalePercentAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub) AS NetAmount, PD.IsOrder, PD.IsShop, IsNull(F.Photo,'') AS Photo , IsNull(F.OriginalCode,'') AS OriginalCode, IsNull(F.IsDiamond,0) AS IsDiamond,pd.SaleGemsItemID,PD.ConsignmentSaleItemID,PD.PGemsCategoryID,PD.PGemsName,PD.Color,PD.Shape,PD.Clarity,PD.SaleLooseDiamondDetailID  "
                strCommandText += " from tbl_PurchaseDetail PD left join tbl_PurchaseHeader PH on PD.PurchaseHeaderID=PH.PurchaseHeaderID left join tbl_ItemName I On I.ItemNameID=PD.ItemNameID left join tbl_GoldQuality G on PD.GoldQualityID=G.GoldQualityID "
                strCommandText += " LEFT JOIN tbl_SaleInvoiceDetail SD ON PD.SaleInvoiceDetailID=SD.SaleInvoiceDetailID "
                strCommandText += " LEFT JOIN tbl_ForSale F ON F.ForSaleID=PD.ForSaleID "
                strCommandText += " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=PD.ItemCategoryID "
                strCommandText += " where PD.PurchaseHeaderID=@PurchaseHeaderID and PH.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetPurchaseDiamondByID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseDiamondByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select  ROW_NUMBER() OVER (ORDER BY PD.PurchaseDetailID) AS SNo, PD.PurchaseDetailID,PD.PurchaseHeaderID, PD.SaleInvoiceDetailID, PD.ForSaleID,PD.BarcodeNo,PD.OldSaleAmount,PD.ItemCategoryID, GC.GemsCategory As GemsCategory,PD.PGemsName,PD.Color,PD.Shape,PD.Clarity, '' As ItemCategory, PD.ItemNameID,PD.GoldQualityID,G.GoldQuality,PD.CurrentPrice,PD.PGemsCategoryID, "
                strCommandText += " PD.SaleRate, IsNull(SD.IsFixPrice,0) AS IsFixPrice, PD.TotalTK,PD.TotalTG,PD.GoldTK,PD.GoldTG,PD.TotalGemTK,PD.TotalGemTG, PD.WasteTK, PD.WasteTG, PD.PWasteTK, PD.PWasteTG, "
                strCommandText += " PD.Length,PD.QTY,PD.IsDamage,PD.IsChange,PD.TotalAmount, PD.YOrCOrG, GemTW, FixType,Case PH.IsGem WHEN 1 THEN PD.ItemName   ELSE '' END AS GemsName, Case PH.IsGem WHEN 0 THEN I.ItemName   ELSE '' END AS ItemName, IsClose, "
                strCommandText += " PD.GoldPrice, PD.GemsPrice , PD.IsDone, PD.DoneAmount, PD.IsSalePercent, PD.SalePercent, PD.SalePercentAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub) AS NetAmount, PD.IsOrder, PD.IsShop, IsNull(F.Photo,'') AS Photo , IsNull(F.OriginalCode,'') AS OriginalCode, IsNull(F.IsDiamond,0) AS IsDiamond,pd.SaleGemsItemID,PD.ConsignmentSaleItemID,PD.SaleLooseDiamondDetailID  "
                strCommandText += " from tbl_PurchaseDetail PD left join tbl_PurchaseHeader PH on PD.PurchaseHeaderID=PH.PurchaseHeaderID left join tbl_ItemName I On I.ItemNameID=PD.ItemNameID left join tbl_GoldQuality G on PD.GoldQualityID=G.GoldQualityID "
                strCommandText += " LEFT JOIN tbl_SaleLooseDiamondDetail SD ON PD.SaleLooseDiamondDetailID=SD.SaleLooseDiamondDetailID "
                strCommandText += " LEFT JOIN tbl_ForSale F ON F.ForSaleID=PD.ForSaleID "
                strCommandText += " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=PD.PGemsCategoryID "
                strCommandText += " where PD.PurchaseHeaderID=@PurchaseHeaderID and PH.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetPurchaseHeaderByID(PurchaseHeaderID As String) As PurchaseHeaderInfo Implements IPurchaseItemDA.GetPurchaseHeaderByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.PurchaseHeaderInfo
            Try
                strCommandText = " SELECT  *, (AllTotalAmount+AllAddOrSub) As AllNetAmount   FROM tbl_PurchaseHeader  WHERE PurchaseHeaderID= @PurchaseHeaderID And IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .PurchaseHeaderID = drResult("PurchaseHeaderID")
                        .PurchaseDate = drResult("PurchaseDate")
                        .CustomerID = drResult("CustomerID")
                        .Address = drResult("Address")
                        .StaffID = drResult("StaffID")
                        .Remark = drResult("Remark")
                        .AllTotalAmount = drResult("AllTotalAmount")
                        .AllAddOrSub = drResult("AllAddOrSub")
                        .AllPaidAmount = drResult("AllPaidAmount")
                        .IsGem = drResult("IsGem")
                        .GoldPrice = drResult("GoldPrice")
                        .GemsPrice = drResult("GemsPrice")
                        .LocationID = drResult("LocationID")
                        .AllNetAmount = drResult("AllNetAmount")
                        .IsChange = drResult("IsChange")
                        .IsLooseDiamond = drResult("IsLooseDiamond")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertPuchaseDetail(ObjItem As PurchaseDetailInfo) As Boolean Implements IPurchaseItemDA.InsertPuchaseDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_PurchaseDetail ( PurchaseDetailID,PurchaseHeaderID, SaleInvoiceDetailID,SaleGemsItemID, ForSaleID,BarcodeNo,OldSaleAmount,ItemCategoryID,ItemNameID,GoldQualityID,CurrentPrice,TotalTK,TotalTG,GoldTK,GoldTG,TotalGemTK,TotalGemTG,Length,QTY,IsDamage,IsChange,TotalAmount, IsClose, YOrCOrG, GemTW, FixType, ItemName, GoldPrice, GemsPrice, WasteTK, WasteTG,  PWasteTK, PWasteTG, SaleRate, IsDone, DoneAmount, IsSalePercent, SalePercent, SalePercentAmount, AddSub, IsShop, IsOrder,ConsignmentSaleItemID,SaleLooseDiamondDetailID,PGemsCategoryID,PGemsName,Color,Shape,Clarity)"
                strCommandText += " Values (@PurchaseDetailID,@PurchaseHeaderID, @SaleInvoiceDetailID,@SaleGemsItemID, @ForSaleID, @BarcodeNo, @OldSaleAmount, @ItemCategoryID, @ItemNameID, @GoldQualityID, @CurrentPrice, @TotalTK, @TotalTG, @GoldTK, @GoldTG, @TotalGemTK, @TotalGemTG, @Length, @QTY, @IsDamage, @IsChange, @TotalAmount, @IsClose, @YOrCOrG, @GemTW, @FixType, @ItemName, @GoldPrice, @GemsPrice, @WasteTK, @WasteTG,  @PWasteTK, @PWasteTG, @SaleRate, @IsDone, @DoneAmount, @IsSalePercent, @SalePercent, @SalePercentAmount, @AddSub , @IsShop, @IsOrder,@ConsignmentSaleItemID,@SaleLooseDiamondDetailID,@PGemsCategoryID,@PGemsName,@Color,@Shape,@Clarity)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseDetailID", DbType.String, ObjItem.PurchaseDetailID)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, ObjItem.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, ObjItem.SaleInvoiceDetailID)
                DB.AddInParameter(DBComm, "@SaleGemsItemID", DbType.String, ObjItem.SaleGemsItemID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ObjItem.ForSaleID)
                DB.AddInParameter(DBComm, "@BarcodeNo", DbType.String, ObjItem.BarcodeNo)
                DB.AddInParameter(DBComm, "@OldSaleAmount", DbType.Int64, ObjItem.OldSaleAmount)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ObjItem.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ObjItem.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, ObjItem.GoldQualityID)
                DB.AddInParameter(DBComm, "@CurrentPrice", DbType.Int64, ObjItem.CurrentPrice)
                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, ObjItem.TotalTK)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, ObjItem.TotalTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, ObjItem.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, ObjItem.GoldTG)
                DB.AddInParameter(DBComm, "@TotalGemTK", DbType.Decimal, ObjItem.TotalGemTK)
                DB.AddInParameter(DBComm, "@TotalGemTG", DbType.Decimal, ObjItem.TotalGemTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, ObjItem.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, ObjItem.WasteTG)
                DB.AddInParameter(DBComm, "@PWasteTK", DbType.Decimal, ObjItem.PWasteTK)
                DB.AddInParameter(DBComm, "@PWasteTG", DbType.Decimal, ObjItem.PWasteTG)
                DB.AddInParameter(DBComm, "@Length", DbType.String, ObjItem.Length)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjItem.QTY)
                DB.AddInParameter(DBComm, "@IsDamage", DbType.Boolean, ObjItem.IsDamage)
                DB.AddInParameter(DBComm, "@IsChange", DbType.Boolean, ObjItem.IsChange)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, ObjItem.TotalAmount)
                DB.AddInParameter(DBComm, "@IsClose", DbType.Boolean, ObjItem.IsClose)
                DB.AddInParameter(DBComm, "@GemTW", DbType.Decimal, ObjItem.GemTW)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjItem.YOrCOrG)
                DB.AddInParameter(DBComm, "@FixType", DbType.String, ObjItem.FixType)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ObjItem.ItemName)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, ObjItem.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, ObjItem.GemsPrice)
                DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, ObjItem.SaleRate)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, ObjItem.IsDone)
                DB.AddInParameter(DBComm, "@DoneAmount", DbType.Int64, ObjItem.DoneAmount)
                DB.AddInParameter(DBComm, "@IsSalePercent", DbType.Boolean, ObjItem.IsSalePercent)
                DB.AddInParameter(DBComm, "@SalePercent", DbType.Int64, ObjItem.SalePercent)
                DB.AddInParameter(DBComm, "@SalePercentAmount", DbType.Int64, ObjItem.SalePercentAmount)
                DB.AddInParameter(DBComm, "@AddSub", DbType.Int64, ObjItem.AddSub)
                DB.AddInParameter(DBComm, "@IsOrder", DbType.Boolean, ObjItem.IsOrder)
                DB.AddInParameter(DBComm, "@IsShop", DbType.Boolean, ObjItem.IsShop)
                DB.AddInParameter(DBComm, "@ConsignmentSaleItemID", DbType.String, ObjItem.ConsignmentSaleItemID)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondDetailID", DbType.String, ObjItem.SaleLooseDiamondDetailID)
                DB.AddInParameter(DBComm, "@PGemsCategoryID", DbType.String, ObjItem.PGemsCategoryID)
                DB.AddInParameter(DBComm, "@PGemsName", DbType.String, ObjItem.PGemsName)
                DB.AddInParameter(DBComm, "@Color", DbType.String, ObjItem.Color)
                DB.AddInParameter(DBComm, "@Shape", DbType.String, ObjItem.Shape)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, ObjItem.Clarity)

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

        Public Function InsertPuchaseHeader(Obj As PurchaseHeaderInfo) As Boolean Implements IPurchaseItemDA.InsertPuchaseHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_PurchaseHeader ( PurchaseHeaderID,PurchaseDate,StaffID,CustomerID,Address,Remark,AllTotalAmount,AllAddOrSub,AllPaidAmount,GoldPrice,GemsPrice,IsGem,LocationID, IsChange,IsDelete,IsSync,IsUpload,LastModifiedLoginUserName,LastModifiedDate,IsLooseDiamond)"
                strCommandText += " Values (@PurchaseHeaderID,@PurchaseDate,@StaffID,@CustomerID,@Address,@Remark,@AllTotalAmount,@AllAddOrSub,@AllPaidAmount,@GoldPrice,@GemsPrice,@IsGem,@LocationID, @IsChange,0,0,0,@LastModifiedLoginUserName,getDate(),@IsLooseDiamond)"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseDate", DbType.DateTime, Obj.PurchaseDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@AllTotalAmount", DbType.Int64, Obj.AllTotalAmount)
                DB.AddInParameter(DBComm, "@AllAddOrSub", DbType.Int32, Obj.AllAddOrSub)
                DB.AddInParameter(DBComm, "@AllPaidAmount", DbType.Int64, Obj.AllPaidAmount)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                DB.AddInParameter(DBComm, "@IsGem", DbType.Boolean, Obj.IsGem)
                DB.AddInParameter(DBComm, "@IsChange", DbType.Boolean, Obj.IsChange)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsLooseDiamond", DbType.Boolean, Obj.IsLooseDiamond)

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

        Public Function InsertPuchasGems(ObjGems As PurchaseGemInfo) As Boolean Implements IPurchaseItemDA.InsertPuchasGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_PurchaseGem ( PurchaseGemID,PurchaseDetailID,GemsCategoryID,GemsName,GemsTK,GemsTG,YOrCOrG,GemTW,QTY,FixType, PurchaseRate, Amount, Discount)"
                strCommandText += " Values (@PurchaseGemID,@PurchaseDetailID,@GemsCategoryID,@GemsName,@GemsTK,@GemsTG,@YOrCOrG,@GemTW,@QTY,@FixType, @PurchaseRate, @Amount, @Discount)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemID", DbType.String, ObjGems.PurchaseGemID)
                DB.AddInParameter(DBComm, "@PurchaseDetailID", DbType.String, ObjGems.PurchaseDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, ObjGems.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, ObjGems.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjGems.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjGems.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjGems.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemTW", DbType.Decimal, ObjGems.GemTW)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjGems.QTY)
                DB.AddInParameter(DBComm, "@FixType", DbType.String, ObjGems.FixType)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int64, ObjGems.PurchaseRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, ObjGems.Amount)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, ObjGems.Discount)

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

        Public Function UpdatePuchaseDetail(ObjItem As PurchaseDetailInfo) As Boolean Implements IPurchaseItemDA.UpdatePuchaseDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_PurchaseDetail set PurchaseHeaderID=@PurchaseHeaderID, SaleInvoiceDetailID=@SaleInvoiceDetailID,SaleGemsItemID=@SaleGemsItemID, ForSaleID=@ForSaleID, BarcodeNo=@BarcodeNo, OldSaleAmount=@OldSaleAmount, ItemCategoryID=@ItemCategoryID, ItemNameID=@ItemNameID, GoldQualityID=@GoldQualityID, CurrentPrice=@CurrentPrice, TotalTK=@TotalTK, TotalTG=@TotalTG, GoldTK=@GoldTK, GoldTG=@GoldTG, TotalGemTK=@TotalGemTK, TotalGemTG=@TotalGemTG, Length=@Length, QTY=@QTY, IsDamage=@IsDamage, IsChange=@IsChange, TotalAmount=@TotalAmount, IsClose=@IsClose, YOrCOrG=@YOrCOrG, GemTW=@GemTW, FixType=@FixType, ItemName=@ItemName, GoldPrice=@GoldPrice, GemsPrice=@GemsPrice, WasteTK=@WasteTK, WasteTG=@WasteTG,  PWasteTK=@PWasteTK, PWasteTG=@PWasteTG, SaleRate=@SaleRate, IsDone=@IsDone, DoneAmount=@DoneAmount, IsSalePercent=@IsSalePercent, SalePercent=@SalePercent, SalePercentAmount=@SalePercentAmount, AddSub=@AddSub, IsOrder=@IsOrder, IsShop=@IsShop,ConsignmentSaleItemID=@ConsignmentSaleItemID,SaleLooseDiamondDetailID=@SaleLooseDiamondDetailID,PGemsCategoryID=@PGemsCategoryID,PGemsName=@PGemsName,Color=@Color,Shape=@Shape,Clarity=@Clarity "
                strCommandText += " Where PurchaseDetailID = @PurchaseDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseDetailID", DbType.String, ObjItem.PurchaseDetailID)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, ObjItem.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@SaleGemsItemID", DbType.String, ObjItem.SaleGemsItemID)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, ObjItem.SaleInvoiceDetailID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ObjItem.ForSaleID)
                DB.AddInParameter(DBComm, "@BarcodeNo", DbType.String, ObjItem.BarcodeNo)
                DB.AddInParameter(DBComm, "@OldSaleAmount", DbType.Int64, ObjItem.OldSaleAmount)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ObjItem.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ObjItem.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, ObjItem.GoldQualityID)
                DB.AddInParameter(DBComm, "@CurrentPrice", DbType.Int64, ObjItem.CurrentPrice)
                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, ObjItem.TotalTK)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, ObjItem.TotalTG)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, ObjItem.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, ObjItem.GoldTG)
                DB.AddInParameter(DBComm, "@TotalGemTK", DbType.Decimal, ObjItem.TotalGemTK)
                DB.AddInParameter(DBComm, "@TotalGemTG", DbType.Decimal, ObjItem.TotalGemTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, ObjItem.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, ObjItem.WasteTG)
                DB.AddInParameter(DBComm, "@PWasteTK", DbType.Decimal, ObjItem.PWasteTK)
                DB.AddInParameter(DBComm, "@PWasteTG", DbType.Decimal, ObjItem.PWasteTG)
                DB.AddInParameter(DBComm, "@Length", DbType.String, ObjItem.Length)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjItem.QTY)
                DB.AddInParameter(DBComm, "@IsDamage", DbType.Boolean, ObjItem.IsDamage)
                DB.AddInParameter(DBComm, "@IsChange", DbType.Boolean, ObjItem.IsChange)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, ObjItem.TotalAmount)
                DB.AddInParameter(DBComm, "@IsClose", DbType.Boolean, ObjItem.IsClose)
                DB.AddInParameter(DBComm, "@GemTW", DbType.Decimal, ObjItem.GemTW)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjItem.YOrCOrG)
                DB.AddInParameter(DBComm, "@FixType", DbType.String, ObjItem.FixType)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ObjItem.ItemName)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, ObjItem.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, ObjItem.GemsPrice)
                DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, ObjItem.SaleRate)
                DB.AddInParameter(DBComm, "@IsDone", DbType.Boolean, ObjItem.IsDone)
                DB.AddInParameter(DBComm, "@DoneAmount", DbType.Int64, ObjItem.DoneAmount)
                DB.AddInParameter(DBComm, "@IsSalePercent", DbType.Boolean, ObjItem.IsSalePercent)
                DB.AddInParameter(DBComm, "@SalePercent", DbType.Int64, ObjItem.SalePercent)
                DB.AddInParameter(DBComm, "@SalePercentAmount", DbType.Int64, ObjItem.SalePercentAmount)
                DB.AddInParameter(DBComm, "@AddSub", DbType.Int64, ObjItem.AddSub)
                DB.AddInParameter(DBComm, "@IsOrder", DbType.Boolean, ObjItem.IsOrder)
                DB.AddInParameter(DBComm, "@IsShop", DbType.Boolean, ObjItem.IsShop)
                DB.AddInParameter(DBComm, "@ConsignmentSaleItemID", DbType.String, ObjItem.ConsignmentSaleItemID)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondDetailID", DbType.String, ObjItem.SaleLooseDiamondDetailID)
                DB.AddInParameter(DBComm, "@PGemsCategoryID", DbType.String, ObjItem.PGemsCategoryID)
                DB.AddInParameter(DBComm, "@PGemsName", DbType.String, ObjItem.PGemsName)
                DB.AddInParameter(DBComm, "@Color", DbType.String, ObjItem.Color)
                DB.AddInParameter(DBComm, "@Shape", DbType.String, ObjItem.Shape)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, ObjItem.Clarity)
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

        Public Function UpdatePuchaseHeader(Obj As PurchaseHeaderInfo) As Boolean Implements IPurchaseItemDA.UpdatePuchaseHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                If IsUpload = "1" Then
                    strCommandText = "Update tbl_PurchaseHeader set PurchaseHeaderID = @PurchaseHeaderID,PurchaseDate = @PurchaseDate,StaffID = @StaffID,CustomerID = @CustomerID,Address = @Address,Remark = @Remark, AllTotalAmount = @AllTotalAmount,AllAddOrSub = @AllAddOrSub,AllPaidAmount = @AllPaidAmount,GoldPrice = @GoldPrice,GemsPrice = @GemsPrice,IsGem = @IsGem,LocationID = @LocationID, IsChange=@IsChange, LastModifiedLoginUserName = @LastModifiedLoginUserName,IsUpload=0,LastModifiedDate = getDate(),IsLooseDiamond=@IsLooseDiamond"
                    strCommandText += " Where PurchaseHeaderID = @PurchaseHeaderID"

                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                    DB.AddInParameter(DBComm, "@PurchaseDate", DbType.DateTime, Obj.PurchaseDate)
                    DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                    DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                    DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
                    DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                    DB.AddInParameter(DBComm, "@AllTotalAmount", DbType.Int64, Obj.AllTotalAmount)
                    DB.AddInParameter(DBComm, "@AllAddOrSub", DbType.Int32, Obj.AllAddOrSub)
                    DB.AddInParameter(DBComm, "@AllPaidAmount", DbType.Int64, Obj.AllPaidAmount)
                    DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                    DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                    DB.AddInParameter(DBComm, "@IsGem", DbType.Boolean, Obj.IsGem)
                    DB.AddInParameter(DBComm, "@IsChange", DbType.Boolean, Obj.IsChange)
                    DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                    DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                    DB.AddInParameter(DBComm, "@IsLooseDiamond", DbType.Boolean, Obj.IsLooseDiamond)

                Else
                    strCommandText = "Update tbl_PurchaseHeader set PurchaseHeaderID = @PurchaseHeaderID,PurchaseDate = @PurchaseDate,StaffID = @StaffID,CustomerID = @CustomerID,Address = @Address,Remark = @Remark, AllTotalAmount = @AllTotalAmount,AllAddOrSub = @AllAddOrSub,AllPaidAmount = @AllPaidAmount,GoldPrice = @GoldPrice,GemsPrice = @GemsPrice,IsGem = @IsGem,LocationID = @LocationID, IsChange=@IsChange, LastModifiedLoginUserName = @LastModifiedLoginUserName,IsUpload=0,LastModifiedDate = getDate(),IsLooseDiamond=@IsLooseDiamond"
                    strCommandText += " Where PurchaseHeaderID = @PurchaseHeaderID"

                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                    DB.AddInParameter(DBComm, "@PurchaseDate", DbType.DateTime, Obj.PurchaseDate)
                    DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                    DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                    DB.AddInParameter(DBComm, "@Address", DbType.String, Obj.Address)
                    DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                    DB.AddInParameter(DBComm, "@AllTotalAmount", DbType.Int64, Obj.AllTotalAmount)
                    DB.AddInParameter(DBComm, "@AllAddOrSub", DbType.Int32, Obj.AllAddOrSub)
                    DB.AddInParameter(DBComm, "@AllPaidAmount", DbType.Int64, Obj.AllPaidAmount)
                    DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                    DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                    DB.AddInParameter(DBComm, "@IsGem", DbType.Boolean, Obj.IsGem)
                    DB.AddInParameter(DBComm, "@IsChange", DbType.Boolean, Obj.IsChange)
                    DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                    DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                    DB.AddInParameter(DBComm, "@IsLooseDiamond", DbType.Boolean, Obj.IsLooseDiamond)

                End If
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

        Public Function UpdatePuchasGems(ObjGems As PurchaseGemInfo) As Boolean Implements IPurchaseItemDA.UpdatePuchasGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "update tbl_PurchaseGem set PurchaseGemID = @PurchaseGemID,PurchaseDetailID = @PurchaseDetailID,GemsCategoryID = @GemsCategoryID,GemsName = @GemsName,GemsTK = @GemsTK,GemsTG = @GemsTG,YOrCOrG = @YOrCOrG,GemTW = @GemTW,QTY = @QTY,FixType = @,FixType,PurchaseRate = @PurchaseRate,Amount = @Amount, Discount=@Discount "
                strCommandText += " where PurchaseGemID = @PurchaseGemID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemID", DbType.String, ObjGems.PurchaseGemID)
                DB.AddInParameter(DBComm, "@PurchaseDetailID", DbType.String, ObjGems.PurchaseDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, ObjGems.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, ObjGems.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjGems.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjGems.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjGems.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemTW", DbType.Decimal, ObjGems.GemTW)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjGems.QTY)
                DB.AddInParameter(DBComm, "@FixType", DbType.String, ObjGems.FixType)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Int64, ObjGems.PurchaseRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, ObjGems.Amount)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, ObjGems.Discount)
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

        Public Function GetPurchaseGemDataByPurchaseGemID(PurchaseGemID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseGemDataByPurchaseGemID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select * from tbl_PurchaseGem"
                strCommandText += " where PurchaseGemID=@PurchaseGemID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemID", DbType.String, PurchaseGemID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseInvoiceForBarcodeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceForBarcodeReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT P.PurchaseHeaderID, P.PurchaseDate, P.StaffID, S.Staff, P.CustomerID, L.CustomerCode, L.CustomerName, P.Remark, P.AllTotalAmount, P.AllAddOrSub, (P.AllTotalAmount-P.AllAddOrSub) AS AllNetAmount, 0 As ItemTotalAmount , " & _
                " P.AllPaidAmount, P.GoldPrice AS TotalGoldPrice, P.GemsPrice AS TotalGemsPrice, P.IsGem, P.LocationID, P.IsChange, " & _
                " PD.PurchaseDetailID, PD.ForSaleID, PD.BarcodeNo, PD.OldSaleAmount, PD.ItemCategoryID, C.ItemCategory, PD.ItemNameID, N.ItemName, PD.IsShop, PD.IsOrder, " & _
                " PD.GoldQualityID, G.GoldQuality, PD.CurrentPrice, PD.TotalTK, CAST((PD.TotalTG) AS DECIMAL(18,3)) as TotalTG, CAST((PD.GoldTG) AS DECIMAL(18,3)) as GoldTG, PD.GoldTK, PD.TotalGemTK, CAST((PD.TotalGemTG) AS DECIMAL(18,3)) AS TotalGemTG, " & _
                " PD.Length, PD.QTY, PD.IsDamage, PD.IsChange AS DetailIsChange, PD.TotalAmount,(PD.TotalAmount-PD.AddSub) AS NetAmount, PD.IsClose, PD.GoldPrice, PD.GemsPrice, " & _
                " CAST(PD.GoldTK AS INT) AS GoldK," & _
                " CAST((PD.GoldTK-CAST(PD.GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((PD.GoldTK-CAST(PD.GoldTK AS INT))*16)-CAST((PD.GoldTK-CAST(PD.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GoldY," & _
                " CAST(PD.TotalGemTK AS INT) AS TotalGemsK, " & _
                " CAST((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16 AS INT) AS TotalGemsP," & _
                " CAST((((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16)-CAST((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemsY," & _
                " CAST(PD.TotalTK AS INT) AS TotalK, " & _
                " CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT) AS TotalP," & _
                " CAST((((PD.TotalTK-CAST(PD.TotalTK AS INT))*16)-CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalY," & _
                 " CAST(PD.PWasteTK AS INT) AS PWasteK," & _
                " CAST((PD.PWasteTK-CAST(PD.PWasteTK AS INT))*16 AS INT) AS PWasteP," & _
                " CAST((((PD.PWasteTK-CAST(PD.PWasteTK AS INT))*16)-CAST((PD.PWasteTK-CAST(PD.PWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS PWasteY," & _
                " PG.PurchaseGemID, PG.GemsCategoryID, GC.GemsCategory, PG.GemsName, PG.GemsTK, PG.GemsTG, PG.YOrCOrG, PG.GemTW, PG.QTY As GemsQTY, PG.FixType, PG.PurchaseRate, PG.Amount, PG.Discount, " & _
                " GC.GemsCategory AS DetailGemsCategory, PD.ItemName AS DetailGemsName, PD.YOrCOrG AS DetailYOrCOrG, PD.FixType AS DetailFixType, PD.WasteTK, PD.WasteTG, PD.PWasteTK, PD.PWasteTG, PD.SaleRate, PD.IsDone, PD.DoneAmount, PD.IsSalePercent, PD.SalePercent, PD.SalePercentAmount, PD.AddSub, P.PurchaseDate AS [@PDate],ROW_NUMBER() OVER (PARTITION BY PD.PurchaseDetailID order by PD.purchaseDetailid desc) AS Position " & _
                " FROM tbl_PurchaseDetail PD LEFT JOIN tbl_PurchaseHeader P ON P.PurchaseHeaderID=PD.PurchaseHeaderID " & _
                " LEFT JOIN tbl_PurchaseGem PG ON PD.PurchaseDetailID=PG.PurchaseDetailID " & _
                "  LEFT JOIN tbl_Customer L ON P.CustomerID=L.CustomerID " & _
                " LEFT JOIN tbl_GoldQuality G ON PD.GoldQualityID=G.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory C ON PD.ItemCategoryID=C.ItemCategoryID" & _
                " LEFT JOIN tbl_ItemName N  ON PD.ItemNameID=N.ItemNameID " & _
                " LEFT JOIN tbl_Staff S ON P.StaffID=S.StaffID " & _
                " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=PG.GemsCategoryID " & _
                " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate and @ToDate " & cristr & " ORDER BY [@PDate] DESC, P.PurchaseHeaderID ASC"
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

        Public Function GetPurchaseInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select PD.PurchaseDetailID,PD.BarcodeNo,PD.TotalTK, PD.TotalTG, PD.PWasteTK, PD.PWasteTG, PD.GoldQualityID," & _
                                 " PD.ItemCategoryID,PD.QTY,P.PurchaseDate,P.IsGem,P.IsChange,PD.CurrentPrice,I.ItemName, PD.TotalAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub)  AS NetAmount," & _
                                 " CAST(TotalTK AS INT) AS TotalK, " & _
                                 " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP," & _
                                 " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalY," & _
                                 " P.PurchaseHeaderID,S.Staff,C.CustomerName As Customer,G.GoldQuality,IC.ItemCategory, (TotalAmount-AddSub) AS NetAmount,0 As PurchasePaidAmount,P.AllTotalAmount,P.AllAddOrSub,PD.BarcodeNo, PD.IsOrder, PD.IsShop  " & _
                                 " From tbl_PurchaseDetail PD Left Join tbl_PurchaseHeader P on P.PurchaseHeaderID=PD.PurchaseHeaderID   " & _
                                 " Left Join tbl_GoldQuality G on G.GoldQualityID=PD.GoldQualityID Left Join tbl_Customer C on C.CustomerID=P.CustomerID " & _
                                 " Left Join tbl_Staff S on S.StaffID=P.StaffID Left Join tbl_ItemName I on I.ItemNameID=PD.ItemNameID  " & _
                                 " Left Join tbl_ItemCategory IC on IC.ItemcategoryID=PD.ItemCategoryID" & _
                                 " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate " & cristr & ""

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

        Public Function GetPurchaseInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As CommonInfo.PurchaseDetailInfo Implements IPurchaseItemDA.GetPurchaseInvoiceDetailForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New PurchaseDetailInfo
            Try
                strCommandText = " select Sum(PD.QTY) As QTY, Sum(CAST((PD.GoldTG) AS DECIMAL(18,3))) as GoldTG, Sum(PD.GoldTK) AS GoldTK, Sum(CAST((PD.TotalTG) AS DECIMAL(18,3))) AS TotalTG, Sum(PD.TotalTK) As TotalTK, " & _
                                " Sum(PD.TotalGemTK) AS TotalGemTK, Sum(CAST((PD.TotalGemTG) AS DECIMAL(18,3))) AS TotalGemTG, Sum(CAST((PD.PWasteTG) AS DECIMAL(18,3))) AS PWasteTG, Sum(PD.PWasteTK) As PWasteTK,  " & _
                                " sum(PD.TotalAmount-PD.AddSub) As TotalAmount " & _
                                " FROM tbl_PurchaseDetail PD LEFT JOIN tbl_PurchaseHeader P ON P.PurchaseHeaderID=PD.PurchaseHeaderID " & _
                               "  LEFT JOIN tbl_Customer L ON P.CustomerID=L.CustomerID " & _
                                " LEFT JOIN tbl_GoldQuality G ON PD.GoldQualityID=G.GoldQualityID " & _
                                " LEFT JOIN tbl_ItemCategory C ON PD.ItemCategoryID=C.ItemCategoryID" & _
                                " LEFT JOIN tbl_ItemName N  ON PD.ItemNameID=N.ItemNameID " & _
                                " LEFT JOIN tbl_Staff S ON P.StaffID=S.StaffID " & _
                                " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate " & criStr & ""

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .QTY = IIf(IsDBNull(drResult("QTY")), 0, drResult("QTY"))
                        .GoldTG = IIf(IsDBNull(drResult("GoldTG")), 0, drResult("GoldTG"))
                        .GoldTK = IIf(IsDBNull(drResult("GoldTK")), 0, drResult("GoldTK"))
                        .TotalGemTK = IIf(IsDBNull(drResult("TotalGemTK")), 0, drResult("TotalGemTK"))
                        .TotalGemTG = IIf(IsDBNull(drResult("TotalGemTG")), 0, drResult("TotalGemTG"))
                        .TotalTG = IIf(IsDBNull(drResult("TotalTG")), 0, drResult("TotalTG"))
                        .TotalTK = IIf(IsDBNull(drResult("TotalTK")), 0, drResult("TotalTK"))
                        .TotalAmount = IIf(IsDBNull(drResult("TotalAmount")), 0, drResult("TotalAmount"))
                        .PWasteTG = IIf(IsDBNull(drResult("PWasteTG")), 0, drResult("PWasteTG"))
                        .PWasteTK = IIf(IsDBNull(drResult("PWasteTK")), 0, drResult("PWasteTK"))
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetPurchaseInvoiceReportForTotalAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceReportForTotalAmount
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Distinct(P.PurchaseHeaderID), P.AllTotalAmount AS TotalAmount, P.AllAddOrSub AS AddOrSub, (P.AllTotalAmount-P.AllAddOrSub) As NetAmount," & _
                                 "  P.AllPaidAmount  AS PaidAmount, ((P.AllTotalAmount-P.AllAddOrSub)-P.AllPaidAmount) As BalanceAmount " & _
                                " FROM tbl_PurchaseDetail PD LEFT JOIN tbl_PurchaseHeader P ON P.PurchaseHeaderID=PD.PurchaseHeaderID " & _
                                "  LEFT JOIN tbl_Customer L ON P.CustomerID=L.CustomerID " & _
                                " LEFT JOIN tbl_GoldQuality G ON PD.GoldQualityID=G.GoldQualityID " & _
                                " LEFT JOIN tbl_ItemCategory C ON PD.ItemCategoryID=C.ItemCategoryID" & _
                                " LEFT JOIN tbl_ItemName N  ON PD.ItemNameID=N.ItemNameID " & _
                                " LEFT JOIN tbl_Staff S ON P.StaffID=S.StaffID " & _
                                " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate " & criStr

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
        Public Function GetPurchaseInvoicePrint(ByVal PurchaseHeaderID As String) As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoicePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT G.PurchaseGemID, G.GemsCategoryID, GC.GemsCategory, G.GemsName, " & _
                                " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY,G.GemsTK, G.GemsTG, G.YOrCOrG, G.GemTW, G.QTY AS GemQTY , G.FixType, G.PurchaseRate, " & _
                                " G.Amount As GemsAmount, G.Discount, D.PurchaseDetailID, D.ForSaleID, D.BarcodeNo, D.ItemNameID, I.ItemName, D.GoldQualityID, " & _
                                " GQ.GoldQuality, GQ.IsGramRate,D.ItemCategoryID, C.ItemCategory, D.OldSaleAmount, D.CurrentPrice, D.Length, D.QTY, D.IsDamage, D.IsChange, D.TotalAmount AS ItemTotalAmount, D.AddSub AS ItemAddSub, (D.TotalAmount-D.AddSub) AS ItemNetAmount," & _
                                " D.GoldPrice, D.GemsPrice,  D.SaleRate, D.IsDone, D.DoneAmount, D.IsSalePercent, D.SalePercent, D.SalePercentAmount, D.IsShop, D.IsOrder, " & _
                                " D.TotalTK, D.TotalTG, D.GoldTK, D.GoldTG, D.TotalGemTK, D.TotalGemTG, D.WasteTK,D.WasteTG, D.PWasteTK, D.PWasteTG, " & _
                                " CAST(D.TotalTK AS INT) AS TotalK, " & _
                                " CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT) AS TotalP, " & _
                                " CAST((((D.TotalTK-CAST(D.TotalTK AS INT))*16)-CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY, " & _
                                " CAST(D.GoldTK AS INT) AS GoldK," & _
                                " CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                " CAST((((D.GoldTK-CAST(D.GoldTK AS INT))*16)-CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                                " CAST(D.WasteTK AS INT) AS WasteK," & _
                                " CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                " CAST(D.PWasteTK AS INT) AS PWasteK," & _
                                " CAST((D.PWasteTK-CAST(D.PWasteTK AS INT))*16 AS INT) AS PWasteP, " & _
                                " CAST((((D.PWasteTK-CAST(D.PWasteTK AS INT))*16)-CAST((D.PWasteTK-CAST(D.PWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PWasteY, " & _
                                " CAST(D.TotalGemTK AS INT) AS TotalGemK," & _
                                " CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP,  CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemY, " & _
                                " H.PurchaseHeaderID, H.PurchaseDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress, Cus.CustomerTel, H.StaffID, S.Staff, H.Remark, H.AllTotalAmount, H.AllAddOrSub, " & _
                                " (H.AllTotalAmount-H.AllAddOrSub) As AllNetAmount, H.AllPaidAmount, ((H.AllTotalAmount-H.AllAddOrSub)-H.AllPaidAmount) AS BalanceAmount, H.GoldPrice AS TotalGoldPrice, H.GemsPrice As TotalGemsPrice, H.IsChange " & _
                                " FROM tbl_PurchaseDetail D LEFT JOIN tbl_PurchaseGem G ON G.PurchaseDetailID=D.PurchaseDetailID" & _
                                " LEFT JOIN  tbl_PurchaseHeader H  ON H.PurchaseHeaderID=D.PurchaseHeaderID " & _
                                " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID" & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=D.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                " ON GQ.GoldQualityID=D.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=D.ItemCategoryID" & _
                                " WHERE H.PurchaseHeaderID=@PurchaseHeaderID AND H.IsGem=0 And H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseInvoiceOnlyGemPrint(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceOnlyGemPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select PD.PurchaseDetailID, PH.PurchaseHeaderID,PD.ItemCategoryID As GemsCategoryID,PD.CurrentPrice,PD.QTY,PD.YOrCOrG,PD.ItemName," & _
                                " PD.TotalGemTK,PD.TotalGemTG,Cast(PD.TotalGemTK As INT)As GemsK,Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))*16 As INT) As GemsP," & _
                                " Cast((((PD.TotalGemTK-Cast(PD.TotalGemTK As Int))* 16)-Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))* 16 As INT))*'" & Global_PToY & "' AS Decimal(18,3))AS GemsY," & _
                                " PD.TotalAmount As ItemTotalAmount, I.GemsCategory AS ItemCategory,PH.AllTotalAmount,PH.AllAddOrSub,PH.AllPaidAmount," & _
                                " PH.PurchaseDate,C.CustomerName,C.CustomerTel,PH.Address As CustomerAddress, PH.StaffID, Staff " & _
                                " From tbl_PurchaseDetail PD " & _
                                " LEFT JOIN tbl_PurchaseHeader PH on PH.PurchaseHeaderID=PD.PurchaseHeaderID " & _
                                " LEFT JOIN tbl_Customer C on C.CustomerID=PH.CustomerID Left Join tbl_GemsCategory I on I.GemsCategoryID=PD.ItemCategoryID " & _
                                " LEFT JOIN tbl_Staff ST ON ST.StaffID=PH.StaffID" & _
                                " WHERE PH.PurchaseHeaderID=@PurchaseHeaderID and IsGem=1 and PH.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetPurchaseInvoiceLooseDiamondPrint(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceLooseDiamondPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select PD.PurchaseDetailID, PH.PurchaseHeaderID,PD.PGemsCategoryID As GemsCategoryID,PD.CurrentPrice,PD.QTY,PD.YOrCOrG,PD.PGemsName as GemsName," & _
                                " PD.TotalGemTK,PD.TotalGemTG,Cast(PD.TotalGemTK As INT)As GemsK,Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))*16 As INT) As GemsP," & _
                                " Cast((((PD.TotalGemTK-Cast(PD.TotalGemTK As Int))* 16)-Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))* 16 As INT))*'" & Global_PToY & "' AS Decimal(18,3))AS GemsY," & _
                                " PD.TotalAmount As ItemTotalAmount, I.GemsCategory AS ItemCategory,PH.AllTotalAmount,PH.AllAddOrSub,PH.AllPaidAmount," & _
                                " PH.PurchaseDate,C.CustomerName,C.CustomerTel,PH.Address As CustomerAddress, PH.StaffID, Staff,PD.Color,PD.Shape,PD.Clarity " & _
                                " From tbl_PurchaseDetail PD " & _
                                " LEFT JOIN tbl_PurchaseHeader PH on PH.PurchaseHeaderID=PD.PurchaseHeaderID " & _
                                " LEFT JOIN tbl_Customer C on C.CustomerID=PH.CustomerID Left Join tbl_GemsCategory I on I.GemsCategoryID=PD.PGemsCategoryID " & _
                                " LEFT JOIN tbl_Staff ST ON ST.StaffID=PH.StaffID" & _
                                " WHERE PH.PurchaseHeaderID=@PurchaseHeaderID and IsLooseDiamond=1 and PH.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllPurchaseInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IPurchaseItemDA.GetAllPurchaseInvoiceVoucherPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT G.PurchaseGemID, G.GemsCategoryID, GC.GemsCategory, G.GemsName, " & _
                              " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                              " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY,G.GemsTK, G.GemsTG, G.YOrCOrG, G.GemTW, G.QTY AS GemQTY , G.FixType, G.PurchaseRate, " & _
                              " G.Amount As GemsAmount, D.PurchaseDetailID, D.ForSaleID, D.BarcodeNo, D.ItemNameID, I.ItemName, D.Length, D.GoldQualityID, " & _
                              " GQ.GoldQuality, D.ItemCategoryID, C.ItemCategory, D.QTY, D.GoldPrice, D.GemsPrice, D.IsFixPrice, D.IsDamage, D.IsChange, D.CurrentPrice, Convert(varchar(10),D.CurrentPrice,105) as StrCurrentPrice, " & _
                              " D.GoldPrice, D.GemsPrice, D.TotalAmount AS ItemTotalAmount, D.TotalTK, CAST((D.TotalTG) AS DECIMAL(18,3)) as TotalTG, D.GoldTK, CAST((D.GoldTG) AS DECIMAL(18,3)) as GoldTG, D.TotalGemTK, CAST((D.TotalGemTG) AS DECIMAL(18,3)) as TotalGemTG, " & _
                              " CAST(D.TotalTK AS INT) AS TotalK," & _
                              " CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT) AS TotalP, " & _
                              " CAST((((D.TotalTK-CAST(D.TotalTK AS INT))*16)-CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY, " & _
                              " CAST(D.GoldTK AS INT) AS GoldK," & _
                              " CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT) AS GoldP,  CAST((((D.GoldTK-CAST(D.GoldTK AS INT))*16)-CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GoldY, " & _
                              " CAST(D.TotalGemTK AS INT) AS TotalGemK," & _
                              " CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP,  CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalGemY, " & _
                              " H.PurchaseHeaderID, H.PurchaseDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, H.AllTotalAmount AS TotalAmount,H.AllAddOrSub AS AddOrSub, " & _
                              " (H.AllTotalAmount-H.AllAddOrSub) As NetAmount, H.AllPaidAmount As PaidAmount, H.GoldPrice AS TotalGoldPrice, H.GemsPrice As TotalGemsPrice, F.IsFixPrice As ForSaleFixPrice, D.OldSaleAmount, D.WasteTK, D.WasteTG, D.PurchaseWastePercent, D.SaleRate, D.PurchaseDiscountAmount, H.PurchaseDate AS [@PDate] " & _
                              " FROM tbl_PurchaseDetail D LEFT JOIN tbl_PurchaseGem G ON G.PurchaseDetailID=D.PurchaseDetailID" & _
                              " LEFT JOIN  tbl_PurchaseHeader H  ON H.PurchaseHeaderID=D.PurchaseHeaderID " & _
                              " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID" & _
                              " LEFT JOIN tbl_ItemName I ON I.ItemNameID=D.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                              " ON GQ.GoldQualityID=D.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                              " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=D.ItemCategoryID" & _
                              " WHERE PurchaseDate BETWEEN @FromDate AND @ToDate AND IsGem=0 AND H.IsDelete=0 " & criStr & " ORDER BY [@PDate] DESC, H.PurchaseHeaderID ASC"

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


        Public Function GetAllPurchaseGemsVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As DataTable Implements IPurchaseItemDA.GetAllPurchaseGemsVoucherPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select PD.PurchaseDetailID, PH.PurchaseHeaderID,PD.ItemCategoryID As GemsCategoryID,PD.CurrentPrice,PD.QTY,PD.YOrCOrG,PD.ItemName," & _
                                " PD.TotalGemTK,PD.TotalGemTG,Cast(PD.TotalGemTK As INT)As GemsK,Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))*16 As INT) As GemsP," & _
                                " Cast((((PD.TotalGemTK-Cast(PD.TotalGemTK As Int))* 16)-Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))* 16 As INT))*'" & Global_PToY & "' AS Decimal(18,3))AS GemsY," & _
                                " PD.TotalAmount,  I.GemsCategory as ItemCategory,PH.AllTotalAmount,PH.AllAddOrSub,PH.AllPaidAmount," & _
                                " PH.PurchaseDate,C.CustomerName,PH.Address, PH.PurchaseDate As [@PDate] " & _
                                " From tbl_PurchaseDetail PD " & _
                                " LEFT JOIN tbl_PurchaseHeader PH on PH.PurchaseHeaderID=PD.PurchaseHeaderID " & _
                                " LEFT JOIN tbl_Customer C on C.CustomerID=PH.CustomerID  Left Join tbl_GemsCategory I on I.GemsCategoryID=PD.ItemCategoryID " & _
                                " WHERE PH.IsDelete=0 AND PurchaseDate BETWEEN @FromDate AND @ToDate and IsGem=1" & criStr & " ORDER BY [@PDate] DESC, PH.PurchaseHeaderID ASC"

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

        Public Function GetPurchaseInvoiceDetailPrint(ByVal PurchaseHeaderID As String) As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceDetailPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT D.PurchaseDetailID, D.ForSaleID, D.BarcodeNo, D.ItemNameID, I.ItemName, D.Length, D.GoldQualityID, " & _
                                  " GQ.GoldQuality, GQ.IsGramRate,D.ItemCategoryID, C.ItemCategory, D.OldSaleAmount, D.CurrentPrice, D.Length, D.QTY, D.IsDamage, D.IsChange, D.TotalAmount AS ItemTotalAmount, D.AddSub AS ItemAddSub, (D.TotalAmount-D.AddSub) AS ItemNetAmount," & _
                                  " D.GoldPrice, D.GemsPrice,  D.SaleRate, D.IsDone, D.DoneAmount, D.IsSalePercent, D.SalePercent, D.SalePercentAmount, " & _
                                  " D.GoldPrice, D.GemsPrice, D.TotalAmount AS ItemTotalAmount, D.TotalTK, D.TotalTG, D.GoldTK, D.GoldTG, D.TotalGemTK, D.TotalGemTG, D.IsOrder, D.IsShop, " & _
                                  " CAST(D.TotalTK AS INT) AS TotalK," & _
                                  " CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT) AS TotalP, " & _
                                  " CAST((((D.TotalTK-CAST(D.TotalTK AS INT))*16)-CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY, " & _
                                  " CAST(D.GoldTK AS INT) AS GoldK," & _
                                  " CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                  " CAST((((D.GoldTK-CAST(D.GoldTK AS INT))*16)-CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                                  " CAST(D.WasteTK AS INT) AS WasteK," & _
                                  " CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                  " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                  " CAST(D.PWasteTK AS INT) AS PWasteK," & _
                                  " CAST((D.PWasteTK-CAST(D.PWasteTK AS INT))*16 AS INT) AS PWasteP, " & _
                                  " CAST((((D.PWasteTK-CAST(D.PWasteTK AS INT))*16)-CAST((D.PWasteTK-CAST(D.PWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PWasteY, " & _
                                  " CAST(D.TotalGemTK AS INT) AS TotalGemK," & _
                                  " CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP,  CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemY, " & _
                                  " H.PurchaseHeaderID, H.PurchaseDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, H.AllTotalAmount, H.AllAddOrSub, " & _
                                  " (H.AllTotalAmount-H.AllAddOrSub) As AllNetAmount, H.AllPaidAmount, ((H.AllTotalAmount-H.AllAddOrSub)-H.AllPaidAmount) AS BalanceAmount, H.GoldPrice AS TotalGoldPrice, H.GemsPrice As TotalGemsPrice, H.IsChange " & _
                                  " FROM tbl_PurchaseDetail D " & _
                                  " LEFT JOIN  tbl_PurchaseHeader H  ON H.PurchaseHeaderID=D.PurchaseHeaderID " & _
                                  " LEFT JOIN tbl_ItemName I ON I.ItemNameID=D.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                  " ON GQ.GoldQualityID=D.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                  " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=D.ItemCategoryID" & _
                                  " WHERE H.PurchaseHeaderID=@PurchaseHeaderID AND H.IsGem=0 And H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllPuchaseHeaderDataBySaleInvoice(ByVal SaleInvoiceHeaderID As String, ByVal Type As String) As DataTable Implements IPurchaseItemDA.GetAllPuchaseHeaderDataBySaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If Type = "SaleInvoice" Then
                    strCommandText = " select  PH.PurchaseHeaderID AS VoucherNo,Convert(varchar(10),PurchaseDate,105) as PurchaseDate, C.CustomerName AS [Customer_],  AllTotalAmount, AllAddOrSub, (AllTotalAmount-AllAddOrSub) As AllNetAmount, AllPaidAmount, S.Staff AS [Staff_], PH.Remark AS [Remark_], PH.PurchaseDate as [@PDate]"
                    strCommandText += "   from tbl_PurchaseHeader PH left join tbl_Staff S on PH.StaffID=S.StaffID Left join tbl_Customer C On C.CustomerID=PH.CustomerID  WHERE IsGem=0 AND IsChange=1 AND PH.IsDelete=0 AND PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SaleInvoiceHeader S WHERE S.PurchaseHeaderID<>'' AND S.SaleInvoiceHeaderID<>@SaleInvoiceHeaderID AND IsDelete=0) AND PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SalesVolume SV WHERE SV.PurchaseHeaderID<>'') order by [@PDate] desc, PH.PurchaseHeaderID desc "
                ElseIf Type = "SaleLooseDiamond" Then
                    strCommandText = " select  PH.PurchaseHeaderID AS VoucherNo,Convert(varchar(10),PurchaseDate,105) as PurchaseDate, C.CustomerName AS [Customer_],  AllTotalAmount, AllAddOrSub, (AllTotalAmount-AllAddOrSub) As AllNetAmount, AllPaidAmount, S.Staff AS [Staff_], PH.Remark AS [Remark_], PH.PurchaseDate as [@PDate]"
                    strCommandText += "   from tbl_PurchaseHeader PH left join tbl_Staff S on PH.StaffID=S.StaffID Left join tbl_Customer C On C.CustomerID=PH.CustomerID  WHERE IsGem=0 AND IsLooseDiamond=1 AND IsChange=1 AND PH.IsDelete=0 And PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SaleInvoiceHeader S WHERE S.PurchaseHeaderID<>'' AND IsDelete=0) AND PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SalesVolume SV WHERE SV.PurchaseHeaderID<>'' AND SV.SalesVolumeID<>@SaleInvoiceHeaderID) order by [@PDate] desc, PH.PurchaseHeaderID desc "
                Else
                    strCommandText = " select  PH.PurchaseHeaderID AS VoucherNo,Convert(varchar(10),PurchaseDate,105) as PurchaseDate, C.CustomerName AS [Customer_],  AllTotalAmount, AllAddOrSub, (AllTotalAmount-AllAddOrSub) As AllNetAmount, AllPaidAmount, S.Staff AS [Staff_], PH.Remark AS [Remark_], PH.PurchaseDate as [@PDate]"
                    strCommandText += "   from tbl_PurchaseHeader PH left join tbl_Staff S on PH.StaffID=S.StaffID Left join tbl_Customer C On C.CustomerID=PH.CustomerID  WHERE IsGem=0  AND IsChange=1 AND PH.IsDelete=0 And PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SaleInvoiceHeader S WHERE S.PurchaseHeaderID<>'' AND IsDelete=0) AND PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SalesVolume SV WHERE SV.PurchaseHeaderID<>'' AND SV.SalesVolumeID<>@SaleInvoiceHeaderID) AND PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SaleLooseDiamondHeader SH WHERE SH.PurchaseHeaderID<>'' AND SVH.SaleLooseDiamondID<>@SaleInvoiceHeaderID) order by [@PDate] desc, PH.PurchaseHeaderID desc "
                End If


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllPurchaseHeaderDataByConsignmentSaleInvoice(ByVal ConsignmentSaleID As String, ByVal Type As String) As DataTable Implements IPurchaseItemDA.GetAllPurchaseHeaderDataByConsignmentSaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If Type = "ConsignmentSaleInvoice" Then
                    strCommandText = " select  PH.PurchaseHeaderID AS VoucherNo,Convert(varchar(10),PurchaseDate,105) as PurchaseDate, C.CustomerName AS [Customer_],  AllTotalAmount, AllAddOrSub, (AllTotalAmount-AllAddOrSub) As AllNetAmount, AllPaidAmount, S.Staff AS [Staff_], PH.Remark AS [Remark_], PH.PurchaseDate as [@PDate]"
                    strCommandText += "   from tbl_PurchaseHeader PH left join tbl_Staff S on PH.StaffID=S.StaffID Left join tbl_Customer C On C.CustomerID=PH.CustomerID  WHERE IsGem=0 AND IsChange=1 AND PH.IsDelete=0 AND PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_ConsignmentSale S WHERE S.PurchaseHeaderID<>'' AND S.ConsignmentSaleID<>@ConsignmentSaleID AND IsDelete=0) order by [@PDate] desc, PH.PurchaseHeaderID desc "
                Else
                    strCommandText = " select  PH.PurchaseHeaderID AS VoucherNo,Convert(varchar(10),PurchaseDate,105) as PurchaseDate, C.CustomerName AS [Customer_],  AllTotalAmount, AllAddOrSub, (AllTotalAmount-AllAddOrSub) As AllNetAmount, AllPaidAmount, S.Staff AS [Staff_], PH.Remark AS [Remark_], PH.PurchaseDate as [@PDate]"
                    strCommandText += "   from tbl_PurchaseHeader PH left join tbl_Staff S on PH.StaffID=S.StaffID Left join tbl_Customer C On C.CustomerID=PH.CustomerID  WHERE IsGem=0 AND IsChange=1 AND PH.IsDelete=0 And PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_ConsignmentSale S WHERE S.PurchaseHeaderID<>'' AND IsDelete=0) order by [@PDate] desc, PH.PurchaseHeaderID desc "
                End If


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ConsignmentSaleID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllPuchaseHeaderDataBySaleGems(ByVal SaleGemsID As String) As DataTable Implements IPurchaseItemDA.GetAllPuchaseHeaderDataBySaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "  select  PH.PurchaseHeaderID AS VoucherNo,Convert(varchar(10),PurchaseDate,105) as PurchaseDate, C.CustomerName AS [Customer_],  " & _
                                 " AllTotalAmount, AllAddOrSub, (AllTotalAmount-AllAddOrSub) As AllNetAmount, AllPaidAmount, " & _
                                 " S.Staff AS [Staff_], PH.Remark AS [Remark_], PH.PurchaseDate as [@PDate]   from tbl_PurchaseHeader PH " & _
                                 " left join tbl_Staff S on PH.StaffID=S.StaffID Left join tbl_Customer C On C.CustomerID=PH.CustomerID  " & _
                                 " WHERE IsGem = 1 And PH.IsDelete = 0 And PurchaseHeaderID " & _
                                 " NOT IN(SELECT PurchaseHeaderID FROM tbl_SaleGems S WHERE S.PurchaseHeaderID<>'' AND S.SaleGemsID<>@SaleGemsID AND IsDelete=0) " & _
                                 " AND PurchaseHeaderID NOT IN(SELECT PurchaseHeaderID FROM tbl_SalesVolume SV WHERE SV.PurchaseHeaderID<>'') " & _
                                 " order by [@PDate] desc, PH.PurchaseHeaderID desc "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllPurchasePrint(ByVal SaleInvoiceHeaderID As String) As DataTable Implements IPurchaseItemDA.GetAllPurchasePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select Distinct D.PurchaseDetailID,D.ForSaleID,D.BarcodeNo,D.OldSaleAmount,D.ItemCategoryID,D.ItemNameID,D.GoldQualityID,D.CurrentPrice, " & _
                                 " D.GoldTK,D.GoldTG,CAST(D.GoldTK AS INT) AS GoldK, CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST((((D.GoldTK-CAST(D.GoldTK AS INT))*16)-CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GoldY,  " & _
                                 " (D.GoldTK+D.TotalGemTK) As ItemTK,(D.GoldTG+D.TotalGemTG) As ItemTG, " & _
                                 " CAST((D.GoldTK+D.TotalGemTK) AS INT) AS ItemK, CAST(((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST(((((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16)-CAST(((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY, " & _
                                 " D.TotalTK,D.TotaltG,CAST(D.TotalTK AS INT) AS TotalK, CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT) AS TotalP, " & _
                                 " CAST((((D.TotalTK-CAST(D.TotalTK AS INT))*16)-CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalY, " & _
                                 " D.WasteTK,D.WasteTG, CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                 " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS WasteY, " & _
                                 " D.TotalGemTK,D.TotalGemTG,CAST(D.TotalGemTK AS INT) AS TotalGemK, CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP, " & _
                                 " CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemY, " & _
                                 " D.Length,D.QTY,D.GoldPrice,D.GemsPrice,D.SaleRate, D.TotalAmount AS ItemAmount, " & _
                                 " I.ItemCategory,N.ItemName,Q.GoldQuality,Q.IsGramRate,SH.SaleInvoiceHeaderID,H.AllTotalAmount,H.AllAddOrSub,H.AllPaidAmount, " & _
                                 " G.PurchaseGemID,G.GemsCategoryID,G.GemsName,G.GemsTK,G.GemsTG,G.YOrCOrG,G.GemTW,G.QTY As GemsQTY,G.FixType,G.PurchaseRate,G.Amount,G.IsOutGem, " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, SH.PurchaseHeaderID,D.IsShop,H.IsChange,H.Remark " & _
                                 " From tbl_PurchaseDetail D  LEFT JOIN tbl_PurchaseGem G on G.PurchaseDetailID=D.PurchaseDetailID  " & _
                                 " LEFT JOIN tbl_SaleInvoiceHeader SH on SH.PurchaseHeaderID=D.PurchaseHeaderID  INNER JOIN tbl_PurchaseHeader H on H.PurchaseHeaderID=SH.PurchaseHeaderID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID " & _
                                 " LEFT JOIN tbl_GoldQuality Q on Q.GoldQualityID=D.GoldQualityID WHERE  SH.SaleInvoiceHeaderID= @SaleInvoiceHeaderID AND SH.IsDelete=0 and H.IsDelete=0 And H.IsGem=0 ORDER BY D.PurchaseDetailID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllPurchasePrintByConsignmentSale(ByVal ConsignmentSaleID As String) As DataTable Implements IPurchaseItemDA.GetAllPurchasePrintByConsignmentSale
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select Distinct D.PurchaseDetailID,D.ForSaleID,D.BarcodeNo,D.OldSaleAmount,D.ItemCategoryID," & _
                                 "D.ItemNameID,D.GoldQualityID,D.CurrentPrice,  D.GoldTK,D.GoldTG,CAST(D.GoldTK AS INT) AS GoldK, " & _
                                 "CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT) AS GoldP,  " & _
                                 "CAST((((D.GoldTK-CAST(D.GoldTK AS INT))*16)-CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS GoldY,  " & _
                                 "(D.GoldTK+D.TotalGemTK) As ItemTK,(D.GoldTG+D.TotalGemTG) As ItemTG,  CAST((D.GoldTK+D.TotalGemTK) AS INT) AS ItemK, " & _
                                 "CAST(((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16 AS INT) AS ItemP,  " & _
                                 "CAST(((((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16)-CAST(((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS ItemY,  " & _
                                 "D.TotalTK,D.TotaltG,CAST(D.TotalTK AS INT) AS TotalK, CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT) AS TotalP,  " & _
                                 "CAST((((D.TotalTK-CAST(D.TotalTK AS INT))*16)-CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS TotalY,  " & _
                                 "D.WasteTK,D.WasteTG, CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 "CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS WasteY,  " & _
                                 "D.TotalGemTK,D.TotalGemTG,CAST(D.TotalGemTK AS INT) AS TotalGemK, CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP,  " & _
                                 "CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS TotalGemY,  " & _
                                 "D.Length,D.QTY,D.GoldPrice,D.GemsPrice,D.SaleRate, D.TotalAmount AS ItemAmount,  I.ItemCategory,N.ItemName,Q.GoldQuality, " & _
                                 "Q.IsGramRate,SH.ConsignmentSaleID,H.AllTotalAmount,H.AllAddOrSub,H.AllPaidAmount,  G.PurchaseGemID,G.GemsCategoryID,G.GemsName, " & _
                                 "G.GemsTK,G.GemsTG,G.YOrCOrG,G.GemTW,G.QTY As GemsQTY,G.FixType,G.PurchaseRate,G.Amount,G.IsOutGem,  " & _
                                 "CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 "CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS GemsY, " & _
                                 "SH.PurchaseHeaderID,D.IsShop,H.IsChange,H.Remark  From tbl_PurchaseDetail D  " & _
                                 "LEFT JOIN tbl_PurchaseGem G on G.PurchaseDetailID=D.PurchaseDetailID   " & _
                                 "LEFT JOIN tbl_ConsignmentSale SH on SH.PurchaseHeaderID=D.PurchaseHeaderID  " & _
                                 "INNER JOIN tbl_PurchaseHeader H on H.PurchaseHeaderID=SH.PurchaseHeaderID  " & _
                                 "LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID  " & _
                                 "LEFT JOIN tbl_GoldQuality Q on Q.GoldQualityID=D.GoldQualityID WHERE  SH.ConsignmentSaleID= @ConsignmentSaleID " & _
                                 "AND SH.IsDelete=0 and H.IsDelete=0 And H.IsGem=0 ORDER BY D.PurchaseDetailID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleID", DbType.String, ConsignmentSaleID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetAllPurchaseGemsPrint(ByVal SaleGemsID As String) As DataTable Implements IPurchaseItemDA.GetAllPurchaseGemsPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select Distinct PD.PurchaseDetailID, PH.PurchaseHeaderID,PD.ItemCategoryID As GemsCategoryID,PD.CurrentPrice,PD.QTY,PD.YOrCOrG,PD.ItemName, " & _
                                 "PD.TotalGemTK,PD.TotalGemTG,Cast(PD.TotalGemTK As INT)As GemsK,Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))*16 As INT) As GemsP, Cast((((PD.TotalGemTK-Cast(PD.TotalGemTK As Int))* 16)-Cast((PD.TotalGemTK-Cast(PD.TotalGemTK As INT))* 16 As INT))*'8.0' AS Decimal(18,3))AS GemsY, PD.TotalAmount As ItemTotalAmount, I.GemsCategory AS ItemCategory,PH.AllTotalAmount,PH.AllAddOrSub, " & _
                                 "PH.AllPaidAmount, PH.PurchaseDate,C.CustomerName,C.CustomerTel,PH.Address As CustomerAddress, PH.StaffID, Staff,I.GemsCategory,I.GemsCategoryID  " & _
                                 "From tbl_PurchaseDetail PD  LEFT JOIN tbl_PurchaseHeader PH on PH.PurchaseHeaderID=PD.PurchaseHeaderID  " & _
                                 "LEFT JOIN tbl_Customer C on C.CustomerID=PH.CustomerID Left Join tbl_GemsCategory I on I.GemsCategoryID=PD.ItemCategoryID  " & _
                                 "LEFT JOIN tbl_Staff ST ON ST.StaffID=PH.StaffID " & _
                                 "LEFT JOIN tbl_SaleGems SH on SH.PurchaseHeaderID=PD.PurchaseHeaderID  " & _
                                 "INNER JOIN tbl_PurchaseHeader H on H.PurchaseHeaderID=SH.PurchaseHeaderID  " & _
                                 "LEFT JOIN tbl_ItemName N on N.ItemNameID=PD.ItemNameID  " & _
                                 "WHERE  SH.SaleGemsID= @SaleGemsID " & _
                                 "AND SH.IsDelete=0 and H.IsDelete=0 And H.IsGem=1 ORDER BY PD.PurchaseDetailID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsID", DbType.String, SaleGemsID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseGemsItemByDetailID(PurchaseDetailID As String) As DataTable Implements IPurchaseItemDA.GetPurchaseGemsItemByDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select PG.PurchaseGemID,PG.PurchaseDetailID,PG.GemsCategoryID,PG.GemsName,PG.GemsTK,PG.GemsTG,"
                strCommandText += " CAST(PG.GemsTK AS INT) AS GemsK, "
                strCommandText += " CAST((PG.GemsTK-CAST(PG.GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((PG.GemsTK-CAST(PG.GemsTK AS INT))*16)-CAST((PG.GemsTK-CAST(PG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, "
                strCommandText += " PG.YOrCOrG, PG.GemTW, PG.QTY, PG.FixType, PG.PurchaseRate, PG.Amount "
                strCommandText += " from tbl_PurchaseGem PG "
                strCommandText += " where PG.PurchaseDetailID=@PurchaseDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseDetailID", DbType.String, PurchaseDetailID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function CheckIsUseInSaleInvoiceHeader(PurchaseHeaderID As String) As Boolean Implements IPurchaseItemDA.CheckIsUseInSaleInvoiceHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from tbl_SaleInvoiceHeader where PurchaseHeaderID='" & PurchaseHeaderID & "' AND IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                If dtResult.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetPurchaseHeaderDataBySaleInvoice(PurchaseHeaderID As String, SaleInvoiceHeaderID As String, ByVal Type As String) As PurchaseHeaderInfo Implements IPurchaseItemDA.GetPurchaseHeaderDataBySaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.PurchaseHeaderInfo
            Try
                If Type = "SaleInvoice" Then
                    strCommandText = " SELECT  *, (AllTotalAmount-AllAddOrSub) As AllNetAmount   FROM tbl_PurchaseHeader  WHERE PurchaseHeaderID= @PurchaseHeaderID AND IsChange=1 AND IsDelete=0 AND @PurchaseHeaderID NOT IN (SELECT PurchaseHeaderID FROM tbl_SaleInvoiceHeader WHERE SaleInvoiceHeaderID<>@SaleInvoiceHeaderID AND IsDelete=0) AND @PurchaseHeaderID NOT IN (SELECT PurchaseHeaderID FROM tbl_SalesVolume) "
                Else
                    strCommandText = " SELECT  *, (AllTotalAmount-AllAddOrSub) As AllNetAmount   FROM tbl_PurchaseHeader  WHERE PurchaseHeaderID= @PurchaseHeaderID AND IsChange=1 AND IsDelete=0 AND @PurchaseHeaderID NOT IN (SELECT PurchaseHeaderID FROM tbl_SalesVolume WHERE SalesVolumeID<>@SaleInvoiceHeaderID) AND @PurchaseHeaderID NOT IN (SELECT PurchaseHeaderID FROM tbl_SaleInvoiceHeader where IsDelete=0) "
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .PurchaseHeaderID = drResult("PurchaseHeaderID")
                        .PurchaseDate = drResult("PurchaseDate")
                        .CustomerID = drResult("CustomerID")
                        .Address = drResult("Address")
                        .StaffID = drResult("StaffID")
                        .Remark = drResult("Remark")
                        .AllTotalAmount = drResult("AllTotalAmount")
                        .AllAddOrSub = drResult("AllAddOrSub")
                        .AllPaidAmount = drResult("AllPaidAmount")
                        .IsGem = drResult("IsGem")
                        .GoldPrice = drResult("GoldPrice")
                        .GemsPrice = drResult("GemsPrice")
                        .LocationID = drResult("LocationID")
                        .AllNetAmount = drResult("AllNetAmount")
                        .IsChange = drResult("IsChange")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetSaleInvoiceDataByHeaderID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetSaleInvoiceDataByHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT SH.PurchaseHeaderID, SH.PurchaseAmount, PH.IsChange  from tbl_SaleInvoiceHeader SH INNER JOIN tbl_PurchaseHeader PH ON SH.PurchaseHeaderID=PH.PurchaseHeaderID where SH.PurchaseHeaderID=@PurchaseHeaderID AND PH.IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateSaleInvoiceDataByPurchaseHeaderID(ByVal Obj As CommonInfo.SaleInvoiceHeaderInfo) As Boolean Implements IPurchaseItemDA.UpdateSaleInvoiceDataByPurchaseHeaderID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                If Obj.PurchaseAmount = 0 Then
                    strCommandText = "Update tbl_SaleInvoiceHeader set PurchaseAmount = 0, PurchaseHeaderID='' WHERE  PurchaseHeaderID= @PurchaseHeaderID AND IsDelete=0"
                Else
                    strCommandText = "Update tbl_SaleInvoiceHeader set PurchaseAmount = @PurchaseAmount WHERE  PurchaseHeaderID= @PurchaseHeaderID AND IsDelete=0"
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)
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

        Public Function GetSaleVolumeInvoiceDataByHeaderID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetSaleVolumeInvoiceDataByHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT SV.PurchaseHeaderID, SV.PurchaseAmount, PH.IsChange  from tbl_SalesVolume SV INNER JOIN tbl_PurchaseHeader PH ON SV.PurchaseHeaderID=PH.PurchaseHeaderID where SV.PurchaseHeaderID=@PurchaseHeaderID and PH.IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSaleLooseDiamondInvoiceDataByHeaderID(PurchaseHeaderID As String) As DataTable Implements IPurchaseItemDA.GetSaleLooseDiamondInvoiceDataByHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT SH.PurchaseHeaderID, SH.PurchaseAmount, PH.IsChange  from tbl_SaleLooseDiamondHeader SH INNER JOIN tbl_PurchaseHeader PH ON SH.PurchaseHeaderID=PH.PurchaseHeaderID where SH.PurchaseHeaderID=@PurchaseHeaderID and PH.IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, PurchaseHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateSaleVolumeDataByPurchaseHeaderID(ByVal Obj As CommonInfo.SalesVolumeHeaderInfo) As Boolean Implements IPurchaseItemDA.UpdateSaleVolumeDataByPurchaseHeaderID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                If Obj.PurchaseAmount = 0 Then
                    strCommandText = "Update tbl_SalesVolume set PurchaseAmount = 0, PurchaseHeaderID='' WHERE  PurchaseHeaderID= @PurchaseHeaderID"
                Else
                    strCommandText = "Update tbl_SalesVolume set PurchaseAmount = @PurchaseAmount WHERE  PurchaseHeaderID= @PurchaseHeaderID"
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)
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
        Public Function UpdateSaleLooseDiamondDataByPurchaseHeaderID(ByVal Obj As CommonInfo.SaleLooseDiamondHeaderInfo) As Boolean Implements IPurchaseItemDA.UpdateSaleLooseDiamondDataByPurchaseHeaderID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                If Obj.PurchaseAmount = 0 Then
                    strCommandText = "Update tbl_SaleLooseDiamondHeader set PurchaseAmount = 0, PurchaseHeaderID='' WHERE  PurchaseHeaderID= @PurchaseHeaderID"
                Else
                    strCommandText = "Update tbl_SaleLooseDiamondHeader set PurchaseAmount = @PurchaseAmount WHERE  PurchaseHeaderID= @PurchaseHeaderID"
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Update ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
        Public Function CheckIsUseInSaleVolumeHeader(PurchaseHeaderID As String) As Boolean Implements IPurchaseItemDA.CheckIsUseInSaleVolumeHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from tbl_SalesVolume where PurchaseHeaderID='" & PurchaseHeaderID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                If dtResult.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
        Public Function GetAllPurchasePrintForSaleVolume(ByVal SalesVolumeID As String) As DataTable Implements IPurchaseItemDA.GetAllPurchasePrintForSaleVolume
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select Distinct D.PurchaseDetailID,D.ForSaleID,D.BarcodeNo,D.OldSaleAmount,D.ItemCategoryID,D.ItemNameID,D.GoldQualityID,D.CurrentPrice, " & _
                                 " D.GoldTK,D.GoldTG,CAST(D.GoldTK AS INT) AS GoldK, CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST((((D.GoldTK-CAST(D.GoldTK AS INT))*16)-CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GoldY,  " & _
                                 " (D.GoldTK+D.TotalGemTK) As ItemTK,(D.GoldTG+D.TotalGemTG) As ItemTG, " & _
                                 " CAST((D.GoldTK+D.TotalGemTK) AS INT) AS ItemK, CAST(((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST(((((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16)-CAST(((D.GoldTK+D.TotalGemTK)-CAST((D.GoldTK+D.TotalGemTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY, " & _
                                 " D.TotalTK,D.TotaltG,CAST(D.TotalTK AS INT) AS TotalK, CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT) AS TotalP, " & _
                                 " CAST((((D.TotalTK-CAST(D.TotalTK AS INT))*16)-CAST((D.TotalTK-CAST(D.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalY, " & _
                                 " D.WasteTK,D.WasteTG, CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                 " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS WasteY, " & _
                                 " D.TotalGemTK,D.TotalGemTG,CAST(D.TotalGemTK AS INT) AS TotalGemK, CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP, " & _
                                 " CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemY, " & _
                                 " D.Length,D.QTY,D.GoldPrice,D.GemsPrice,D.SaleRate, " & _
                                 " I.ItemCategory,N.ItemName,Q.GoldQuality,Q.IsGramRate,SV.SalesVolumeID,H.AllTotalAmount,H.AllAddOrSub,H.AllPaidAmount, " & _
                                 " G.PurchaseGemID,G.GemsCategoryID,G.GemsName,G.GemsTK,G.GemsTG,G.YOrCOrG,G.GemTW,G.QTY As GemsQTY,G.FixType,G.PurchaseRate,G.Amount,G.IsOutGem, " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, SV.PurchaseHeaderID " & _
                                 " From tbl_PurchaseDetail D  LEFT JOIN tbl_PurchaseGem G on G.PurchaseDetailID=D.PurchaseDetailID  " & _
                                 " LEFT JOIN tbl_SalesVolume SV on SV.PurchaseHeaderID=D.PurchaseHeaderID  INNER JOIN tbl_PurchaseHeader H on H.PurchaseHeaderID=SV.PurchaseHeaderID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID " & _
                                 " LEFT JOIN tbl_GoldQuality Q on Q.GoldQualityID=D.GoldQualityID WHERE  SV.SalesVolumeID= @SalesVolumeID AND H.IsDelete=0 ORDER BY D.PurchaseDetailID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SalesVolumeID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseInvoiceSummayReportByDate(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceSummayReportByDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select PD.PurchaseDetailID, PD.ForSaleID, PD.BarcodeNo, PD.TotalTK, PD.TotalTG, PD.PWasteTK, PD.PWasteTG, PD.GoldQualityID, G.GoldQuality, IC.ItemCategory," & _
                                 " PD.ItemCategoryID, PD.ItemNameID, I.ItemName, PD.QTY, PD.IsShop, P.PurchaseDate, P.IsGem, P.IsChange, PD.CurrentPrice, PD.TotalAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub)  AS ItemNetAmount," & _
                                 " P.PurchaseHeaderID,S.Staff,C.CustomerName As Customer, (AllTotalAmount-AllAddOrSub) AS NetAmount,0 As PurchasePaidAmount,P.AllTotalAmount, P.AllAddOrSub, 0 AS TotalNetAmount, 0 AS TotalAddOrSub   " & _
                                 " From tbl_PurchaseDetail PD Left Join tbl_PurchaseHeader P on P.PurchaseHeaderID=PD.PurchaseHeaderID   " & _
                                 " Left Join tbl_GoldQuality G on G.GoldQualityID=PD.GoldQualityID Left Join tbl_Customer C on C.CustomerID=P.CustomerID " & _
                                 " Left Join tbl_Staff S on S.StaffID=P.StaffID Left Join tbl_ItemName I on I.ItemNameID=PD.ItemNameID  " & _
                                 " Left Join tbl_ItemCategory IC on IC.ItemcategoryID=PD.ItemCategoryID" & _
                                 " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate and @ToDate " & cristr & "" & " Order by P.PurchaseHeaderID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DBComm.CommandTimeout = 0
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseInvoiceDailyTransactionReport(ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceDailyTransactionReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select PD.PurchaseDetailID,PD.BarcodeNo," & _
                       " PD.GoldQualityID,  PD.ItemCategoryID,PD.ItemNameID,PD.Qty as QTY,Convert(varchar(10),P.PurchaseDate,105) As PurchaseDate, " & _
                       " P.PurchaseDate As [@PDate] ,P.AllTotalAmount,P.AllAddOrSub,P.AllPaidAmount,PD.TotalTK,PD.TotalTG,CAST(PD.TotalTK AS INT) AS TotalK,  " & _
                       " CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT) AS TotalP,CAST((((PD.TotalTK-CAST(PD.TotalTK AS INT))*16)-CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalY, " & _
                       " P.PurchaseHeaderID,C.CustomerName As Customer,G.GoldQuality,IsNull(IC.ItemCategory,'') as ItemCategory,(PD.TotalAmount-PD.AddSub) AS TotalAmount,IsNull(I.ItemName,'') as ItemName," & _
                       " PD.CurrentPrice, 0 As PurchasePaidAmount,P.IsChange,ST.Staff  From tbl_PurchaseDetail PD Left Join tbl_PurchaseHeader P " & _
                       " on P.PurchaseHeaderID=PD.PurchaseHeaderID     Left Join tbl_GoldQuality G on G.GoldQualityID=PD.GoldQualityID " & _
                       " Left Join tbl_Customer C on C.CustomerID=P.CustomerID  LEFT JOIN tbl_Staff ST On ST.StaffID=P.StaffID Left Join tbl_ItemName I on I.ItemNameID=PD.ItemNameID" & _
                       " Left Join tbl_ItemCategory IC on IC.ItemcategoryID=PD.ItemCategoryID  " & _
                       " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate and @ToDate " & cristr & _
                       " Order by  [@PDate] DESC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(ToDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetPurchaseInvoiceGemReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceGemReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select  P.PurchaseHeaderID,P.PurchaseDate,P.IsGem, PD.PurchaseDetailID,PD.ItemCategoryID aS GemsCategoryID, G.GemsCategory, PD.ItemName As GemsName,PD.TotalGemTK,CAST((PD.TotalGemTG) AS DECIMAL(18,3)) as TotalGemTG,PD.YOrCOrG,PD.QTY, P.PurchaseDate AS [@PDate], PD.TotalAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub) AS NetAmount,ST.Staff " & _
                                 " From tbl_PurchaseHeader P Left Join tbl_PurchaseDetail PD on PD.PurchaseHeaderID=P.PurchaseHeaderID Left Join tbl_GemsCategory G on G.GemsCategoryID=PD.ItemCategoryID LEFT JOIN tbl_Staff ST ON ST.StaffID=P.StaffID" & _
                                 " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate and @ToDate " & cristr & " ORDER BY [@PDate] DESC"

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
        Public Function GetPurchaseInvoiceLooseDiamondReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceLooseDiamondReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select  P.PurchaseHeaderID,P.PurchaseDate,P.IsLooseDiamond, PD.PurchaseDetailID,PD.PGemsCategoryID as GemsCategoryID, G.GemsCategory, PD.PGemsName As GemsName,PD.TotalGemTK,CAST((PD.TotalGemTG) AS DECIMAL(18,3)) as TotalGemTG,PD.YOrCOrG,PD.QTY, P.PurchaseDate AS [@PDate], PD.TotalAmount, PD.AddSub, (PD.TotalAmount-PD.AddSub) AS NetAmount,ST.Staff " & _
                                 " From tbl_PurchaseHeader P Left Join tbl_PurchaseDetail PD on PD.PurchaseHeaderID=P.PurchaseHeaderID Left Join tbl_GemsCategory G on G.GemsCategoryID=PD.PGemsCategoryID LEFT JOIN tbl_Staff ST ON ST.StaffID=P.StaffID" & _
                                 " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate and @ToDate " & cristr & " ORDER BY [@PDate] DESC"

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

        Public Function InsertPurchaseFromSupplier(ByVal Obj As CommonInfo.PurchaseHeaderInfo) As Boolean Implements IPurchaseItemDA.InsertPurchaseFromSupplier
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_PurchaseFromSupplier ( PurchaseFromSupplierID,PDate,StaffID,SupplierID,Remark,Voucher,ExchangeRate,TotalAmount,AddOrSub,PaidAmount,DiscountRate,Expense,CommissionRate,PayType,DueDate,LocationID,LastModifiedLoginUserName,LastModifiedDate,IsDelete,IsSync)"
                strCommandText += " Values (@PurchaseFromSupplierID,@PDate,@StaffID,@SupplierID,@Remark,@Voucher,@ExchangeRate,@TotalAmount,@AddOrSub,@PaidAmount,@DiscountRate,@Expense,@CommissionRate,@PayType,@DueDate,@LocationID,@LastModifiedLoginUserName,@LastModifiedDate,0,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierID", DbType.String, Obj.PurchaseFromSupplierID)
                DB.AddInParameter(DBComm, "@PDate", DbType.Date, Obj.PDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, Obj.SupplierID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@Voucher", DbType.String, Obj.Voucher)
                DB.AddInParameter(DBComm, "@ExchangeRate", DbType.Int64, Obj.ExchangeRate)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountRate", DbType.Int32, Obj.DiscountRate)
                DB.AddInParameter(DBComm, "@Expense", DbType.Int64, Obj.Expense)
                DB.AddInParameter(DBComm, "@CommissionRate", DbType.Int32, Obj.CommissionRate)
                DB.AddInParameter(DBComm, "@PayType", DbType.Int32, Obj.PayType)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, Obj.DueDate)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                'DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)

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

        Public Function InsertPurchaseFromSupplierItem(ByVal ObjItem As CommonInfo.PurchaseDetailInfo) As Boolean Implements IPurchaseItemDA.InsertPurchaseFromSupplierItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_PurchaseFromSupplierItem ( PurchaseFromSupplierItemID,PurchaseFromSupplierID,OriginalCode,GramWeight,QTY,Rate,Amount,IsReject)"
                strCommandText += " Values (@PurchaseFromSupplierItemID,@PurchaseFromSupplierID,@OriginalCode,@GramWeight,@QTY,@Rate,@Amount,@IsReject)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierItemID", DbType.String, ObjItem.PurchaseFromSupplierItemID)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierID", DbType.String, ObjItem.PurchaseFromSupplierID)
                DB.AddInParameter(DBComm, "@OriginalCode", DbType.String, ObjItem.OriginalCode)
                DB.AddInParameter(DBComm, "@GramWeight", DbType.Decimal, ObjItem.GramWeight)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjItem.QTY)
                DB.AddInParameter(DBComm, "@Rate", DbType.Decimal, ObjItem.Rate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Decimal, ObjItem.Amount)
                DB.AddInParameter(DBComm, "@IsReject", DbType.Boolean, False)

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

        Public Function UpdatePurchaseFromSupplier(ByVal Obj As CommonInfo.PurchaseHeaderInfo) As Boolean Implements IPurchaseItemDA.UpdatePurchaseFromSupplier
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_PurchaseFromSupplier set  PurchaseFromSupplierID= @PurchaseFromSupplierID , PDate= @PDate , StaffID= @StaffID , SupplierID= @SupplierID , Remark= @Remark , Voucher= @Voucher , ExchangeRate= @ExchangeRate , TotalAmount= @TotalAmount , AddOrSub= @AddOrSub , PaidAmount= @PaidAmount , DiscountRate= @DiscountRate , Expense= @Expense , CommissionRate= @CommissionRate , PayType= @PayType , DueDate= @DueDate , LocationID= @LocationID ,  LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate,IsSync=0 "
                strCommandText += " where PurchaseFromSupplierID= @PurchaseFromSupplierID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierID", DbType.String, Obj.PurchaseFromSupplierID)
                DB.AddInParameter(DBComm, "@PDate", DbType.Date, Obj.PDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, Obj.SupplierID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@Voucher", DbType.String, Obj.Voucher)
                DB.AddInParameter(DBComm, "@ExchangeRate", DbType.Int64, Obj.ExchangeRate)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@DiscountRate", DbType.Int32, Obj.DiscountRate)
                DB.AddInParameter(DBComm, "@Expense", DbType.Int64, Obj.Expense)
                DB.AddInParameter(DBComm, "@CommissionRate", DbType.Int32, Obj.CommissionRate)
                DB.AddInParameter(DBComm, "@PayType", DbType.Int32, Obj.PayType)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, Obj.DueDate)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                'DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)

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

        Public Function UpdatePurchaseFromSupplierItem(ByVal ObjItem As CommonInfo.PurchaseDetailInfo) As Boolean Implements IPurchaseItemDA.UpdatePurchaseFromSupplierItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_PurchaseFromSupplierItem set  PurchaseFromSupplierItemID= @PurchaseFromSupplierItemID , PurchaseFromSupplierID= @PurchaseFromSupplierID , OriginalCode= @OriginalCode , GramWeight= @GramWeight , QTY= @QTY , Rate= @Rate , Amount= @Amount "
                strCommandText += " where PurchaseFromSupplierItemID= @PurchaseFromSupplierItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierItemID", DbType.String, ObjItem.PurchaseFromSupplierItemID)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierID", DbType.String, ObjItem.PurchaseFromSupplierID)
                DB.AddInParameter(DBComm, "@OriginalCode", DbType.String, ObjItem.OriginalCode)
                DB.AddInParameter(DBComm, "@GramWeight", DbType.Decimal, ObjItem.GramWeight)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, ObjItem.QTY)
                DB.AddInParameter(DBComm, "@Rate", DbType.Decimal, ObjItem.Rate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Decimal, ObjItem.Amount)

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

        Public Function DeletePurchaseFromSupplierItem(ByVal PurchaseFromSupplierItemID As String) As Boolean Implements IPurchaseItemDA.DeletePurchaseFromSupplierItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_PurchaseFromSupplierItem WHERE  PurchaseFromSupplierItemID= @PurchaseFromSupplierItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierItemID", DbType.String, PurchaseFromSupplierItemID)
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

        Public Function DeletePurchaseFromSupplier(ByVal PurchaseFromSupplierID As String) As Boolean Implements IPurchaseItemDA.DeletePurchaseFromSupplier
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "Update tbl_PurchaseFromSupplier SET IsDelete= 1 WHERE  PurchaseFromSupplierID= @PurchaseFromSupplierID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierID", DbType.String, PurchaseFromSupplierID)
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

        Public Function GetAllPurchaseFromSupplier() As System.Data.DataTable Implements IPurchaseItemDA.GetAllPurchaseFromSupplier
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT P.PurchaseFromSupplierID ,convert(varchar(10),P.PDate,105) As PurchaseDate,S.Staff AS [Staff_],P.SupplierID as [@SupplierID],SU.SupplierName AS [SupplierName_],P.TotalAmount,P.PaidAmount,P.DiscountRate "
                strCommandText += "FROM tbl_PurchaseFromSupplier P Left Join tbl_Staff S On P.StaffID=S.StaffID Left Join tbl_Supplier SU On P.SupplierID = SU.SupplierID WHERE P.IsDelete=0 order by P.PDate desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseFromSupplier(ByVal PurchaseFromSupplierID As String) As CommonInfo.PurchaseHeaderInfo Implements IPurchaseItemDA.GetPurchaseFromSupplier
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.PurchaseHeaderInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_PurchaseFromSupplier WHERE PurchaseFromSupplierID= @PurchaseFromSupplierID And IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierID", DbType.String, PurchaseFromSupplierID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .PurchaseFromSupplierID = drResult("PurchaseFromSupplierID")
                        .PDate = drResult("PDate")
                        .StaffID = drResult("StaffID")
                        .SupplierID = drResult("SupplierID")
                        .Remark = drResult("Remark")
                        .Voucher = drResult("Voucher")
                        .ExchangeRate = drResult("ExchangeRate")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .PaidAmount = drResult("PaidAmount")
                        .DiscountRate = drResult("DiscountRate")
                        .Expense = drResult("Expense")
                        .CommissionRate = drResult("CommissionRate")
                        .PayType = drResult("PayType")
                        .DueDate = drResult("DueDate")
                        .LocationID = drResult("LocationID")
                        '.IsReturn = drResult("IsReturn")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetPurchaseFromSupplierItem(ByVal PurchaseFromSupplierID As String) As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseFromSupplierItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT PurchaseFromSupplierItemID , PurchaseFromSupplierID, OriginalCode,GramWeight as GWeight,QTY,Rate,Amount, IsReject "
                strCommandText += "FROM tbl_PurchaseFromSupplierItem P Where PurchaseFromSupplierID = @PurchaseFromSupplierID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseFromSupplierID", DbType.String, PurchaseFromSupplierID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseInvoiceFromSupplierReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceFromSupplierReport
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select H.PurchaseFromSupplierID,H.PDate,S.Staff,H.SupplierID,U.SupplierName,H.Remark, H.Voucher, H.ExchangeRate," & _
                            " (((H.TotalAmount + H.AddOrSub) * H.DiscountRate) /100) as DisAmount,(((H.TotalAmount + H.AddOrSub) * H.CommissionRate) / 100) as ComAmount," & _
                            " H.TotalAmount, ((H.TotalAmount+H.AddOrSub)-((((H.TotalAmount + H.AddOrSub) * H.CommissionRate) / 100)+ (((H.TotalAmount + H.AddOrSub) * H.DiscountRate) /100))) as NetAmount,H.AddorSub, H.PaidAmount, H.DiscountRate, H.Expense, H.CommissionRate, H.PayType, H.DueDate," & _
                            " I.PurchaseFromSupplierItemID, I.OriginalCode, I.GramWeight, I.QTY, I.Rate, I.Amount, I.IsReject, 0 As AllNetAmount, 0 AS AllPaidAmount" & _
                            " from tbl_PurchaseFromSupplier H Left Join tbl_PurchaseFromSupplierItem I" & _
                            " On H.PurchaseFromSupplierID=I.PurchaseFromSupplierID " & _
                            " Left Join tbl_Staff S On H.StaffID = S.StaffID Left Join tbl_Supplier U On H.SupplierID = U.SupplierID" & _
                            " Where H.PDate Between '" & FromDate & "' And '" & ToDate & "' " & GetFilterString & "And H.IsDelete=0 ORDER BY H.PDate ASC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)

                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllPurchasePrintForSaleLooseDiamond(ByVal SaleLooseDiamondID As String) As DataTable Implements IPurchaseItemDA.GetAllPurchasePrintForSaleLooseDiamond
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select Distinct D.PurchaseDetailID,D.ForSaleID,D.BarcodeNo,D.OldSaleAmount,D.PGemsCategoryID,D.PGemsName as GemsName,D.CurrentPrice,I.GemsCategory, " & _
                                 " D.TotalGemTK,D.TotalGemTG,CAST(D.TotalGemTK AS INT) AS TotalGemK, CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP, " & _
                                 " CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemY, " & _
                                 " D.Length,D.QTY,D.GoldPrice,D.GemsPrice,D.SaleRate,D.Color,D.Shape,D.Clarity, " & _
                                 " S.SaleLooseDiamondID,H.AllTotalAmount,H.AllAddOrSub,H.AllPaidAmount, " & _
                                 "  S.PurchaseHeaderID " & _
                                 " From tbl_PurchaseDetail D   " & _
                                 " LEFT JOIN tbl_SaleLooseDiamondHeader S on S.PurchaseHeaderID=D.PurchaseHeaderID  INNER JOIN tbl_PurchaseHeader H on H.PurchaseHeaderID=S.PurchaseHeaderID " & _
                                 " LEFT JOIN tbl_GemsCategory I on I.GemsCategoryID=D.PGemsCategoryID  " & _
                                 " WHERE  S.SaleLooseDiamondID= @SaleLooseDiamondID AND H.IsDelete=0 ORDER BY D.PurchaseDetailID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleLooseDiamondID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetPurchaseInvoiceForLooseDiamondReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IPurchaseItemDA.GetPurchaseInvoiceForLooseDiamondReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT P.PurchaseHeaderID, P.PurchaseDate, P.StaffID, S.Staff, P.CustomerID, L.CustomerCode, L.CustomerName, P.Remark, P.AllTotalAmount, P.AllAddOrSub, (P.AllTotalAmount-P.AllAddOrSub) AS AllNetAmount, 0 As ItemTotalAmount , " & _
                " P.AllPaidAmount, P.GoldPrice AS TotalGoldPrice, P.GemsPrice AS TotalGemsPrice, P.LocationID, P.IsChange, " & _
                " PD.PurchaseDetailID, PD.ForSaleID, PD.BarcodeNo, PD.OldSaleAmount, PD.ItemCategoryID,PD.IsShop, PD.IsOrder, " & _
                " PD.GoldQualityID,PD.CurrentPrice, PD.TotalTK, CAST((PD.TotalTG) AS DECIMAL(18,3)) as TotalTG, CAST((PD.GoldTG) AS DECIMAL(18,3)) as GoldTG, PD.GoldTK, PD.TotalGemTK, CAST((PD.TotalGemTG) AS DECIMAL(18,3)) AS TotalGemTG, " & _
                " PD.Length, PD.QTY, PD.IsDamage, PD.IsChange AS DetailIsChange, PD.TotalAmount,(PD.TotalAmount-PD.AddSub) AS NetAmount, PD.IsClose, PD.GoldPrice, PD.GemsPrice, " & _
                " CAST(PD.GoldTK AS INT) AS GoldK," & _
                " CAST((PD.GoldTK-CAST(PD.GoldTK AS INT))*16 AS INT) AS GoldP," & _
                " CAST((((PD.GoldTK-CAST(PD.GoldTK AS INT))*16)-CAST((PD.GoldTK-CAST(PD.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GoldY," & _
                " CAST(PD.TotalGemTK AS INT) AS TotalGemsK, " & _
                " CAST((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16 AS INT) AS TotalGemsP," & _
                " CAST((((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16)-CAST((PD.TotalGemTK-CAST(PD.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemsY," & _
                " CAST(PD.TotalTK AS INT) AS TotalK, " & _
                " CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT) AS TotalP," & _
                " CAST((((PD.TotalTK-CAST(PD.TotalTK AS INT))*16)-CAST((PD.TotalTK-CAST(PD.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalY," & _
                 " CAST(PD.PWasteTK AS INT) AS PWasteK," & _
                " CAST((PD.PWasteTK-CAST(PD.PWasteTK AS INT))*16 AS INT) AS PWasteP," & _
                " CAST((((PD.PWasteTK-CAST(PD.PWasteTK AS INT))*16)-CAST((PD.PWasteTK-CAST(PD.PWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS PWasteY," & _
                " GC.GemsCategory, PD.PGemsCategoryID as GemsCategoryID,PD.PGemsName as GemsName,PD.Color,PD.Shape,PD.Clarity,PD.SaleLooseDiamondDetailID, " & _
                " GC.GemsCategory AS DetailGemsCategory, PD.ItemName AS DetailGemsName, PD.YOrCOrG, PD.FixType AS DetailFixType, PD.WasteTK, PD.WasteTG, PD.PWasteTK, PD.PWasteTG, PD.SaleRate, PD.IsDone, PD.DoneAmount, PD.IsSalePercent, PD.SalePercent, PD.SalePercentAmount, PD.AddSub, P.PurchaseDate AS [@PDate],ROW_NUMBER() OVER (PARTITION BY PD.PurchaseDetailID order by PD.purchaseDetailid desc) AS Position " & _
                " FROM tbl_PurchaseDetail PD LEFT JOIN tbl_PurchaseHeader P ON P.PurchaseHeaderID=PD.PurchaseHeaderID " & _
                " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=PD.PGemsCategoryID " & _
                 "  LEFT JOIN tbl_Customer L ON P.CustomerID=L.CustomerID " & _
                " LEFT JOIN tbl_Staff S ON P.StaffID=S.StaffID " & _
                " WHERE P.IsDelete=0 AND PurchaseDate BETWEEN @FromDate and @ToDate " & cristr & " ORDER BY [@PDate] DESC, P.PurchaseHeaderID ASC"
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

    End Class
End Namespace

