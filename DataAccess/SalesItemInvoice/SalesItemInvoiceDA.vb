Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Operational.AppConfiguration
Imports System.Data.Common
Namespace SalesItemInvoice
    Public Class SalesItemInvoiceDA
        Implements ISalesItemInvoiceDA

#Region "Private SalesInvoice"

        Private DB As Database
        Private Shared ReadOnly _instance As ISalesItemInvoiceDA = New SalesItemInvoiceDA
        Private IsUpload As Integer = AppConfiguration.ReadAppSettings("IsUpload")
#End Region
#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesItemInvoiceDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function InsertSalesInvoiceHeader(ByVal Obj As CommonInfo.SaleInvoiceHeaderInfo) As Boolean Implements ISalesItemInvoiceDA.InsertSalesInvoiceHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                'strCommandText = "INSERT INTO tbl_SaleInvoiceHeader(SaleInvoiceHeaderID, SaleDate, CustomerID, StaffID, Remark, TotalAmount, AddOrSub, DiscountAmount, PaidAmount, PromotionDiscount, LocationID,  LastModifiedLoginUserName,LastModifiedDate, PurchaseHeaderID, PurchaseAmount, IsAdvance, EntryAdvanceDate, AllAdvanceAmount, IsCancel, CancelDate, IsOtherCash, OtherCashAmount,IsDelete, IsUpload,IsSync,SRTaxAmt, AllTaxAmt,SRTaxPer)"
                'strCommandText += " VALUES(@SaleInvoiceHeaderID, @SaleDate, @CustomerID, @StaffID,@Remark, @TotalAmount, @AddOrSub, @DiscountAmount,@PaidAmount, @PromotionDiscount, @LocationID, @LastModifiedLoginUserName,@LastModifiedDate, @PurchaseHeaderID, @PurchaseAmount, @IsAdvance, @EntryAdvanceDate, @AllAdvanceAmount, @IsCancel, @CancelDate, @IsOtherCash, @OtherCashAmount, 0,0,0, @SRTaxAmt,@AllTaxAmt,@SRTaxPer)"
                strCommandText = "INSERT INTO tbl_SaleInvoiceHeader(SaleInvoiceHeaderID, SaleDate, CustomerID, StaffID, Remark, TotalAmount, AddOrSub, DiscountAmount, PaidAmount, PromotionDiscount, LocationID,  LastModifiedLoginUserName,LastModifiedDate, PurchaseHeaderID, PurchaseAmount, IsAdvance, EntryAdvanceDate, AllAdvanceAmount, IsCancel, CancelDate, IsOtherCash, OtherCashAmount,IsDelete, IsUpload,IsSync,SRTaxAmt, AllTaxAmt,SRTaxPer,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus)"
                strCommandText += " VALUES(@SaleInvoiceHeaderID, @SaleDate, @CustomerID, @StaffID,@Remark, @TotalAmount, @AddOrSub, @DiscountAmount,@PaidAmount, @PromotionDiscount, @LocationID, @LastModifiedLoginUserName,getDate(), @PurchaseHeaderID, @PurchaseAmount, @IsAdvance,getDate(), @AllAdvanceAmount, @IsCancel,getDate(), @IsOtherCash, @OtherCashAmount, 0,0,0, @SRTaxAmt,@AllTaxAmt,@SRTaxPer,@MemberID,@MemberName,@MemberCode,@RedeemID,@TopupPoint,@TopupValue,@RedeemPoint,@RedeemValue,@IsRedeemInvoice,@MemberDis,@MemberDiscountAmt,@InvoiceStatus)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, Obj.SaleInvoiceHeaderID)
                DB.AddInParameter(DBComm, "@SaleDate", DbType.DateTime, Obj.SaleDate)

                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int32, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, Obj.PaidAmount)

                DB.AddInParameter(DBComm, "@PromotionDiscount", DbType.Int32, Obj.PromotionDiscount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                ' DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, System.DateTime.Now)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)


                DB.AddInParameter(DBComm, "@IsAdvance", DbType.String, Obj.IsAdvance)
                DB.AddInParameter(DBComm, "@EntryAdvanceDate", DbType.String, Obj.EntryAdvanceDate)
                DB.AddInParameter(DBComm, "@AllAdvanceAmount", DbType.String, Obj.AllAdvanceAmount)
                DB.AddInParameter(DBComm, "@IsCancel", DbType.Boolean, Obj.IsCancel)
                DB.AddInParameter(DBComm, "@CancelDate", DbType.DateTime, Obj.CancelDate)
                DB.AddInParameter(DBComm, "@IsOtherCash", DbType.Boolean, Obj.IsOtherCash)
                DB.AddInParameter(DBComm, "@OtherCashAmount", DbType.Int64, Obj.OtherCashAmount)
                DB.AddInParameter(DBComm, "@SRTaxAmt", DbType.Int64, Obj.SRTaxAmt)
                DB.AddInParameter(DBComm, "@AllTaxAmt", DbType.Int64, Obj.AllTaxAmt)
                DB.AddInParameter(DBComm, "@SRTaxPer", DbType.Decimal, Obj.SRTaxPer)
                DB.AddInParameter(DBComm, "@MemberID", DbType.String, Obj.MemberID)
                DB.AddInParameter(DBComm, "@MemberName", DbType.String, Obj.MemberName)
                DB.AddInParameter(DBComm, "@MemberCode", DbType.String, Obj.MemberCode)
                DB.AddInParameter(DBComm, "@RedeemID", DbType.String, Obj.RedeemID)
                DB.AddInParameter(DBComm, "@TopupPoint", DbType.Int32, Obj.TopupPoint)
                DB.AddInParameter(DBComm, "@TopupValue", DbType.Int32, Obj.TopupValue)
                DB.AddInParameter(DBComm, "@RedeemPoint", DbType.Int32, Obj.RedeemPoint)
                DB.AddInParameter(DBComm, "@RedeemValue", DbType.Int32, Obj.RedeemValue)
                DB.AddInParameter(DBComm, "@IsRedeemInvoice", DbType.Boolean, Obj.IsRedeemInvoice)
                DB.AddInParameter(DBComm, "@MemberDis", DbType.Decimal, Obj.MemberDis)
                DB.AddInParameter(DBComm, "@MemberDiscountAmt", DbType.Int64, Obj.MemberDiscountAmt)
                DB.AddInParameter(DBComm, "@InvoiceStatus", DbType.Int32, Obj.InvoiceStatus)


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

        Public Function UpdateSalesInvoiceHeader(ByVal Obj As SaleInvoiceHeaderInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateSalesInvoiceHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                If IsUpload = "1" Then

                    strCommandText = "Update tbl_SaleInvoiceHeader set   StaffID=@StaffID, SaleDate=@SaleDate, CustomerID=@CustomerID, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub, DiscountAmount=@DiscountAmount, PaidAmount=@PaidAmount, Remark=@Remark, PromotionDiscount=@PromotionDiscount, LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate, LocationID=@LocationID, PurchaseHeaderID=@PurchaseHeaderID, PurchaseAmount=@PurchaseAmount, IsAdvance=@IsAdvance, EntryAdvanceDate=@EntryAdvanceDate, AllAdvanceAmount=@AllAdvanceAmount, IsCancel=@IsCancel, CancelDate=@CancelDate, IsOtherCash=@IsOtherCash, OtherCashAmount=@OtherCashAmount, IsUpload=0 ,AllTaxAmt=@AllTaxAmt, SRTaxPer=@SRTaxPer, SRTaxAmt=@SRTaxAmt,MemberID=@MemberID,MemberName=@MemberName,MemberCode=@MemberCode,RedeemID=@RedeemID,TopupPoint=@TopupPoint,TopupValue=@TopupValue,RedeemPoint=@RedeemPoint,RedeemValue=@RedeemValue,IsRedeemInvoice=@IsRedeemInvoice,MemberDis=@MemberDis,MemberDiscountAmt=@MemberDiscountAmt,InvoiceStatus=@InvoiceStatus"
                    strCommandText += " where SaleInvoiceHeaderID= @SaleInvoiceHeaderID"
                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, Obj.SaleInvoiceHeaderID)
                    DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                    DB.AddInParameter(DBComm, "@SaleDate", DbType.DateTime, CDate(Obj.SaleDate))
                    DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                    DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, Obj.TotalAmount)
                    DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
                    DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int32, Obj.DiscountAmount)
                    DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, Obj.PaidAmount)
                    DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                    DB.AddInParameter(DBComm, "@PromotionDiscount", DbType.Int32, Obj.PromotionDiscount)
                    DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                    DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)
                    DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                    DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, CDate(Now))
                    DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                    DB.AddInParameter(DBComm, "@IsAdvance", DbType.String, Obj.IsAdvance)
                    DB.AddInParameter(DBComm, "@EntryAdvanceDate", DbType.String, CDate(Obj.EntryAdvanceDate))
                    DB.AddInParameter(DBComm, "@AllAdvanceAmount", DbType.String, Obj.AllAdvanceAmount)
                    DB.AddInParameter(DBComm, "@IsCancel", DbType.String, Obj.IsCancel)
                    DB.AddInParameter(DBComm, "@CancelDate", DbType.String, CDate(Obj.CancelDate))
                    DB.AddInParameter(DBComm, "@IsOtherCash", DbType.Boolean, Obj.IsOtherCash)
                    DB.AddInParameter(DBComm, "@OtherCashAmount", DbType.Int64, Obj.OtherCashAmount)
                    DB.AddInParameter(DBComm, "@AllTaxAmt", DbType.Int64, Obj.AllTaxAmt)
                    DB.AddInParameter(DBComm, "@SRTaxPer", DbType.Decimal, Obj.SRTaxPer)
                    DB.AddInParameter(DBComm, "@SRTaxAmt", DbType.Int64, Obj.SRTaxAmt)
                    DB.AddInParameter(DBComm, "@MemberID", DbType.String, Obj.MemberID)
                    DB.AddInParameter(DBComm, "@MemberName", DbType.String, Obj.MemberName)
                    DB.AddInParameter(DBComm, "@MemberCode", DbType.String, Obj.MemberCode)
                    DB.AddInParameter(DBComm, "@RedeemID", DbType.String, Obj.RedeemID)
                    DB.AddInParameter(DBComm, "@TopupPoint", DbType.Int32, Obj.TopupPoint)
                    DB.AddInParameter(DBComm, "@TopupValue", DbType.Int32, Obj.TopupValue)
                    DB.AddInParameter(DBComm, "@RedeemPoint", DbType.Int32, Obj.RedeemPoint)
                    DB.AddInParameter(DBComm, "@RedeemValue", DbType.Int32, Obj.RedeemValue)
                    DB.AddInParameter(DBComm, "@IsRedeemInvoice", DbType.Boolean, Obj.IsRedeemInvoice)
                    DB.AddInParameter(DBComm, "@MemberDis", DbType.Decimal, Obj.MemberDis)
                    DB.AddInParameter(DBComm, "@MemberDiscountAmt", DbType.Int64, Obj.MemberDiscountAmt)
                    DB.AddInParameter(DBComm, "@InvoiceStatus", DbType.Int32, Obj.InvoiceStatus)

                Else
                    strCommandText = "Update tbl_SaleInvoiceHeader set   StaffID=@StaffID, SaleDate=@SaleDate, CustomerID=@CustomerID, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub, DiscountAmount=@DiscountAmount, PaidAmount=@PaidAmount, Remark=@Remark, PromotionDiscount=@PromotionDiscount, LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= getDate(), LocationID=@LocationID, PurchaseHeaderID=@PurchaseHeaderID, PurchaseAmount=@PurchaseAmount, IsAdvance=@IsAdvance, EntryAdvanceDate=@EntryAdvanceDate, AllAdvanceAmount=@AllAdvanceAmount, IsCancel=@IsCancel, CancelDate=@CancelDate, IsOtherCash=@IsOtherCash, OtherCashAmount=@OtherCashAmount, IsUpload=0 ,AllTaxAmt=@AllTaxAmt, SRTaxPer=@SRTaxPer, SRTaxAmt=@SRTaxAmt,MemberID=@MemberID,MemberName=@MemberName,MemberCode=@MemberCode,RedeemID=@RedeemID,TopupPoint=@TopupPoint,TopupValue=@TopupValue,RedeemPoint=@RedeemPoint,RedeemValue=@RedeemValue,IsRedeemInvoice=@IsRedeemInvoice,MemberDis=@MemberDis,MemberDiscountAmt=@MemberDiscountAmt,InvoiceStatus=@InvoiceStatus"
                    strCommandText += " where SaleInvoiceHeaderID= @SaleInvoiceHeaderID"
                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, Obj.SaleInvoiceHeaderID)
                    DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                    DB.AddInParameter(DBComm, "@SaleDate", DbType.DateTime, CDate(Obj.SaleDate))
                    DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                    DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, Obj.TotalAmount)
                    DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
                    DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int32, Obj.DiscountAmount)
                    DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, Obj.PaidAmount)
                    DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                    DB.AddInParameter(DBComm, "@PromotionDiscount", DbType.Int32, Obj.PromotionDiscount)
                    DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                    DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int32, Obj.PurchaseAmount)
                    DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                    DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, CDate(Now))
                    DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                    DB.AddInParameter(DBComm, "@IsAdvance", DbType.String, Obj.IsAdvance)
                    DB.AddInParameter(DBComm, "@EntryAdvanceDate", DbType.DateTime, CDate(Obj.EntryAdvanceDate))
                    DB.AddInParameter(DBComm, "@AllAdvanceAmount", DbType.String, Obj.AllAdvanceAmount)
                    DB.AddInParameter(DBComm, "@IsCancel", DbType.String, Obj.IsCancel)
                    DB.AddInParameter(DBComm, "@CancelDate", DbType.DateTime, CDate(Obj.CancelDate))
                    DB.AddInParameter(DBComm, "@IsOtherCash", DbType.Boolean, Obj.IsOtherCash)
                    DB.AddInParameter(DBComm, "@OtherCashAmount", DbType.Int64, Obj.OtherCashAmount)
                    DB.AddInParameter(DBComm, "@AllTaxAmt", DbType.Int64, Obj.AllTaxAmt)
                    DB.AddInParameter(DBComm, "@SRTaxPer", DbType.Decimal, Obj.SRTaxPer)
                    DB.AddInParameter(DBComm, "@SRTaxAmt", DbType.Int64, Obj.SRTaxAmt)
                    DB.AddInParameter(DBComm, "@MemberID", DbType.String, Obj.MemberID)
                    DB.AddInParameter(DBComm, "@MemberName", DbType.String, Obj.MemberName)
                    DB.AddInParameter(DBComm, "@MemberCode", DbType.String, Obj.MemberCode)
                    DB.AddInParameter(DBComm, "@RedeemID", DbType.String, Obj.RedeemID)
                    DB.AddInParameter(DBComm, "@TopupPoint", DbType.Int32, Obj.TopupPoint)
                    DB.AddInParameter(DBComm, "@TopupValue", DbType.Int32, Obj.TopupValue)
                    DB.AddInParameter(DBComm, "@RedeemPoint", DbType.Int32, Obj.RedeemPoint)
                    DB.AddInParameter(DBComm, "@RedeemValue", DbType.Int32, Obj.RedeemValue)
                    DB.AddInParameter(DBComm, "@IsRedeemInvoice", DbType.Boolean, Obj.IsRedeemInvoice)
                    DB.AddInParameter(DBComm, "@MemberDis", DbType.Int32, Obj.MemberDis)
                    DB.AddInParameter(DBComm, "@MemberDiscountAmt", DbType.Int32, Obj.MemberDiscountAmt)
                    DB.AddInParameter(DBComm, "@InvoiceStatus", DbType.Int32, Obj.InvoiceStatus)
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

        Public Function DeleteSalesInvoiceHeader(ByVal SaleInvoiceHeaderID As String) As Boolean Implements ISalesItemInvoiceDA.DeleteSalesInvoiceHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_SaleInvoiceHeader SET IsDelete=1 ,LastModifiedDate=GetDate() WHERE  SaleInvoiceHeaderID= @SaleInvoiceHeaderID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
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

        Public Function InsertSaleInvoiceDetail(ByVal Obj As CommonInfo.SalesInvoiceDetailInfo) As Boolean Implements ISalesItemInvoiceDA.InsertSaleInvoiceDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_SaleInvoiceDetail ( SaleInvoiceDetailID, SaleInvoiceHeaderID, ForSaleID, ItemCode, SalesRate, ItemTK, ItemTG, GemsTK, GemsTG, WasteTK, WasteTG, GoldPrice, GemsPrice, IsFixPrice, TotalAmount, AddOrSub, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, OriginalGemsPrice, OriginalOtherPrice, PurchaseWasteTK, PurchaseWasteTG, IsReturn,ItemTaxPer,ItemTax, IsSaleReturn, WhiteCharges, PlatingCharges, MountingCharges, DesignCharges,DesignChargesRate,SellingRate,SellingAmt)"
                strCommandText += " VALUES(@SaleInvoiceDetailID, @SaleInvoiceHeaderID, @ForSaleID, @ItemCode, @SalesRate, @ItemTK, @ItemTG, @GemsTK, @GemsTG, @WasteTK, @WasteTG, @GoldPrice, @GemsPrice, @IsFixPrice, @TotalAmount, @AddOrSub, @IsOriginalFixedPrice, @OriginalFixedPrice, @IsOriginalPriceGram, @OriginalPriceGram, @OriginalPriceTK, @OriginalGemsPrice, @OriginalOtherPrice, @PurchaseWasteTK, @PurchaseWasteTG, @IsReturn,@ItemTaxPer,@ItemTax, @IsSaleReturn, @WhiteCharges, @PlatingCharges, @MountingCharges, @DesignCharges,@DesignChargesRate,@SellingRate,@SellingAmt)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, Obj.SaleInvoiceDetailID)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, Obj.SaleInvoiceHeaderID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, Obj.SalesRate)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, Obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, Obj.WasteTG)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, Obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, Obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceGram", DbType.Boolean, Obj.IsOriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, Obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, Obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, Obj.OriginalGemsPrice)
                DB.AddInParameter(DBComm, "@OriginalOtherPrice", DbType.Int64, Obj.OriginalOtherPrice)
                DB.AddInParameter(DBComm, "@PurchaseWasteTK", DbType.Decimal, Obj.PurchaseWasteTK)
                DB.AddInParameter(DBComm, "@PurchaseWasteTG", DbType.Decimal, Obj.PurchaseWasteTG)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, False)
                DB.AddInParameter(DBComm, "@ItemTaxPer", DbType.Decimal, Obj.ItemTaxPer)
                DB.AddInParameter(DBComm, "@ItemTax", DbType.Int64, Obj.ItemTax)
                DB.AddInParameter(DBComm, "@IsSaleReturn", DbType.Boolean, Obj.IsSaleReturn)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, Obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, Obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, Obj.MountingCharges)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int64, Obj.DesignChargesRate)
                DB.AddInParameter(DBComm, "@SellingRate", DbType.Int32, Obj.SellingRate)
                DB.AddInParameter(DBComm, "@SellingAmt", DbType.Int64, Obj.SellingAmt)
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

        Public Function UpdateSaleInvoiceDetail(ByVal Obj As CommonInfo.SalesInvoiceDetailInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateSaleInvoiceDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleInvoiceDetail set SaleInvoiceHeaderID=@SaleInvoiceHeaderID, ForSaleID=@ForSaleID, ItemCode=@ItemCode, SalesRate=@SalesRate, ItemTK=@ItemTK, ItemTG=@ItemTG, GemsTK=@GemsTK, GemsTG=@GemsTG, WasteTK=@WasteTK, WasteTG=@WasteTG, GoldPrice=@GoldPrice, GemsPrice=@GemsPrice, IsFixPrice=@IsFixPrice, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub, IsOriginalFixedPrice=@IsOriginalFixedPrice, OriginalFixedPrice=@OriginalFixedPrice, IsOriginalPriceGram=@IsOriginalPriceGram, OriginalPriceGram=@OriginalPriceGram, OriginalPriceTK=@OriginalPriceTK, OriginalGemsPrice=@OriginalGemsPrice, OriginalOtherPrice=@OriginalOtherPrice, PurchaseWasteTK=@PurchaseWasteTK, PurchaseWasteTG=@PurchaseWasteTG ,ItemTaxPer=@ItemTaxPer,ItemTax=@ItemTax, IsSaleReturn=@IsSaleReturn,WhiteCharges=@WhiteCharges, PlatingCharges=@PlatingCharges, MountingCharges=@MountingCharges, DesignCharges=@DesignCharges,DesignChargesRate=@DesignChargesRate,SellingRate=@SellingRate,SellingAmt=@SellingAmt"
                strCommandText += " where SaleInvoiceDetailID= @SaleInvoiceDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, Obj.SaleInvoiceDetailID)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, Obj.SaleInvoiceHeaderID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, Obj.SalesRate)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, Obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, Obj.WasteTG)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, Obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, Obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceGram", DbType.Boolean, Obj.IsOriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, Obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, Obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, Obj.OriginalGemsPrice)
                DB.AddInParameter(DBComm, "@OriginalOtherPrice", DbType.Int64, Obj.OriginalOtherPrice)
                DB.AddInParameter(DBComm, "@PurchaseWasteTK", DbType.Decimal, Obj.PurchaseWasteTK)
                DB.AddInParameter(DBComm, "@PurchaseWasteTG", DbType.Decimal, Obj.PurchaseWasteTG)
                DB.AddInParameter(DBComm, "@ItemTaxPer", DbType.Decimal, Obj.ItemTaxPer)
                DB.AddInParameter(DBComm, "@ItemTax", DbType.Int64, Obj.ItemTax)
                DB.AddInParameter(DBComm, "@IsSaleReturn", DbType.Boolean, Obj.IsSaleReturn)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, Obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, Obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, Obj.MountingCharges)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int64, Obj.DesignChargesRate)
                DB.AddInParameter(DBComm, "@SellingRate", DbType.Int32, Obj.SellingRate)
                DB.AddInParameter(DBComm, "@SellingAmt", DbType.Int64, Obj.SellingAmt)
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
        Public Function DeleteSaleInvoiceDetail(ByVal SaleInvoiceDetailID As String) As Boolean Implements ISalesItemInvoiceDA.DeleteSaleInvoiceDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SaleInvoiceDetail WHERE  SaleInvoiceDetailID= @SaleInvoiceDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, SaleInvoiceDetailID)
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

        Public Function InsertSaleInvoiceDetailGem(ByVal Obj As CommonInfo.SaleInvoiceDetailGemInfo) As Boolean Implements ISalesItemInvoiceDA.InsertSaleInvoiceDetailGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_SalesInvoiceGemItem ( SalesInvoiceGemItemID, SaleInvoiceDetailID, GemsCategoryID, GemsName, GemsTK, GemsTG, YOrCOrG, GemsTW, Qty, Type, UnitPrice, Amount, GemsRemark,GemTaxPer,GemTax)"
                strCommandText += " VALUES(@SalesInvoiceGemItemID, @SaleInvoiceDetailID, @GemsCategoryID, @GemsName, @GemsTK, @GemsTG, @YOrCOrG, @GemsTW, @Qty, @Type, @UnitPrice, @Amount, @GemsRemark,@GemTaxPer,@GemTax)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesInvoiceGemItemID", DbType.String, Obj.SalesInvoiceGemItemID)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, Obj.SalesInvoiceDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemsTW)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int16, Obj.Qty)
                DB.AddInParameter(DBComm, "@Type", DbType.String, Obj.Type)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, Obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)
                DB.AddInParameter(DBComm, "@GemsRemark", DbType.String, Obj.GemsRemark)
                DB.AddInParameter(DBComm, "@GemTaxPer", DbType.Decimal, Obj.GemTaxPer)
                DB.AddInParameter(DBComm, "@GemTax", DbType.Int64, Obj.GemTax)
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

        Public Function UpdateSaleInvoiceDetailGem(ByVal Obj As CommonInfo.SaleInvoiceDetailGemInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateSaleInvoiceDetailGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SalesInvoiceGemItem set SaleInvoiceDetailID=@SaleInvoiceDetailID, GemsCategoryID=@GemsCategoryID, GemsName=@GemsName, GemsTK=@GemsTK, GemsTG=@GemsTG, YOrCOrG=@YOrCOrG, GemsTW=@GemsTW, Qty=@Qty, Type=@Type, UnitPrice=@UnitPrice, Amount=@Amount, GemsRemark=@GemsRemark,GemTaxPer=@GemTaxPer,GemTax=@GemTax"
                strCommandText += " where SalesInvoiceGemItemID=@SalesInvoiceGemItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesInvoiceGemItemID", DbType.String, Obj.SalesInvoiceGemItemID)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, Obj.SalesInvoiceDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemsTW)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int16, Obj.Qty)
                DB.AddInParameter(DBComm, "@Type", DbType.String, Obj.Type)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, Obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)
                DB.AddInParameter(DBComm, "@GemsRemark", DbType.String, Obj.GemsRemark)
                DB.AddInParameter(DBComm, "@GemTaxPer", DbType.Decimal, Obj.GemTaxPer)
                DB.AddInParameter(DBComm, "@GemTax", DbType.Int64, Obj.GemTax)

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
        Public Function DeleteSaleInvoiceDetailGem(ByVal SalesInvoiceGemItemID As String) As Boolean Implements ISalesItemInvoiceDA.DeleteSaleInvoiceDetailGem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SalesInvoiceGemItem WHERE  SalesInvoiceGemItemID= @SalesInvoiceGemItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesInvoiceGemItemID", DbType.String, SalesInvoiceGemItemID)
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
        Public Function GetSalesInvoiceDetailByID(ByVal SaleInvoiceHeaderID As String) As System.Data.DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceDetailByID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select ROW_NUMBER() OVER (ORDER BY D.SaleInvoiceDetailID) AS SNo, D.SaleInvoiceDetailID , D.SaleInvoiceHeaderID , D.ForSaleID , D.ItemCode , I.ItemCategory, N.ItemName, GQ.GoldQuality, D.SalesRate , D.ItemTK , D.ItemTG AS Gram , D.ItemTG , D.GemsTK , D.GemsTG , D.WasteTK , D.WasteTG , D.GoldPrice , D.GemsPrice , D.IsFixPrice , D.TotalAmount,  D.AddOrSub, (D.TotalAmount+D.AddOrSub) AS NetAmount, D.IsOriginalFixedPrice, D.OriginalFixedPrice, D.IsOriginalPriceGram, D.OriginalPriceGram, D.OriginalPriceTK, D.OriginalGemsPrice, D.OriginalOtherPrice, D.PurchaseWasteTK, D.PurchaseWasteTG,F.OriginalCode,F.PriceCode,D.ItemTaxPer,D.ItemTax, D.IsSaleReturn, D.WhiteCharges, D.PlatingCharges, D.MountingCharges, D.DesignCharges ,D.DesignChargesRate,D.SellingRate,D.SellingAmt, "
                strCommandText += "CAST((D.ItemTK-D.GemsTK) AS INT) AS GoldK,CAST(((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16 AS INT) AS GoldP, CAST(((((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16)-CAST(((D.ItemTK-D.GemsTK)-CAST((D.ItemTK-D.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY, "
                strCommandText += " CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY "
                strCommandText += " FROM tbl_SaleInvoiceDetail D LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID "
                strCommandText += " LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=F.ItemCategoryID "
                strCommandText += " LEFT JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID  "
                strCommandText += " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID  "
                strCommandText += " Where F.IsDelete=0 and SaleInvoiceHeaderID ='" & SaleInvoiceHeaderID & "' Order By SaleInvoiceDetailID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetSaleInvoiceDetailGemByID(ByVal SaleInvoiceDetailID As String) As System.Data.DataTable Implements ISalesItemInvoiceDA.GetSaleInvoiceDetailGemByID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select SalesInvoiceGemItemID, SaleInvoiceDetailID,GemsCategoryID as [@GemsCategoryID], GemsName, GemsTK, GemsTG, YOrCOrG, GemsTW, Qty, Type, UnitPrice, Amount, GemsTK, GemsRemark,GemTaxPer,GemTax, "
                strCommandText += " CAST(GemsTK AS INT) AS GemsK, "
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY "
                strCommandText += " from tbl_SalesInvoiceGemItem Where SaleInvoiceDetailID ='" & SaleInvoiceDetailID & "' Order By SaleInvoiceDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetAllSalesInvoice() As System.Data.DataTable Implements ISalesItemInvoiceDA.GetAllSalesInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select CustomerName As [Customer_], SaleInvoiceHeaderID  AS VoucherNo, convert(varchar(10),SaleDate,105) as SaleDate, H.CustomerID As [@CustomerID], H.StaffID AS [@StaffID], Staff As [Staff_], H.IsAdvance AS [$IsAdvance], TotalAmount, AddOrSub, DiscountAmount, PaidAmount, H.Remark As [Remark_], H.PromotionDiscount,SaleDate as [@SDate], PurchaseHeaderID, PurchaseAmount, convert(varchar(10),EntryAdvanceDate,105) as AdvanceDate, AllAdvanceAmount AS AdvanceAmount, H.IsOtherCash AS [$IsOtherCash], H.OtherCashAmount ,H.AllTaxAmt,H.SRTaxPer,H.SRTaxAmt,H.MemberDis,H.MemberDiscountAmt,H.InvoiceStatus,H.IsRedeemInvoice "
                strCommandText += " from tbl_SaleInvoiceHeader H left join tbl_Staff S on H.StaffID=S.StaffID left join tbl_Customer C on H.CustomerID=C.CustomerID WHERE H.IsDelete=0 order by [@SDate] desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesInvoicePrint(ByVal SaleInvoiceHeaderID As String) As System.Data.DataTable Implements ISalesItemInvoiceDA.GetSalesInvoicePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT G.SalesInvoiceGemItemID, G.GemsCategoryID, GC.GemsCategory, G.GemsName,F.Color, " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY,G.GemsTK, G.GemsTG, G.YOrCOrG, G.GemsTW, G.Qty , G.Type, G.UnitPrice,G.Amount As GemAmount, G.GemsRemark,   D.SaleInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID,  GQ.GoldQuality,GQ.IsGramRate, F.ItemCategoryID, C.ItemCategory, F.Width, F.FixPrice, case  F.GoldSmithID When '0' Then F.GoldSmith else GS.Name end as GoldSmith, D.DesignCharges, D.PlatingCharges, D.MountingCharges,  D.WhiteCharges, F.Photo, D.SalesRate, D.GoldPrice, D.GemsPrice, D.IsFixPrice,D.TotalAmount AS ItemTotalAmount, " & _
                                 " D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, D.ItemTK, D.ItemTG, D.GemsTK As TotalGemsTK, D.GemsTG AS TotalGemsTG, D.WasteTK, D.WasteTG, (D.ItemTK-D.GemsTK)+D.WasteTK As TotalTK, (D.ItemTG-D.GemsTG)+D.WasteTG As TotalTG, D.ItemTK-D.GemsTK As GoldTK, D.ItemTG-D.GemsTG AS GoldTG,   CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,  CAST(D.GemsTK AS INT) AS TotalGemsK,  CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS TotalGemsP,  CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemsY,  CAST(D.WasteTK AS INT) AS WasteK,  CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT) AS TotalK,  CAST(((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16 AS INT) AS TotalP,  CAST(((((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16)-CAST(((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY, CAST(D.ItemTK-D.GemsTK AS INT) AS GoldK,  CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT) AS GoldP,  CAST((((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16)-CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, H.SaleInvoiceHeaderID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, H.TotalAmount,H.AddOrSub,   (H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub+H.SRTaxAmt)-(((H.TotalAmount*PromotionDiscount)/100) +H.DiscountAmount+H.AllAdvanceAmount+H.PaidAmount+H.OtherCashAmount+H.PurchaseAmount+H.RedeemValue+H.MemberDiscountAmt))) As BalanceAmount,F.Photo, " & _
                                 " (D.DesignCharges+D.PlatingCharges+D.MountingCharges+D.WhiteCharges) AS TotalCharges, Cus.CustomerCode,Cus.DOB, Cus.CustomerTel,H.PurchaseHeaderID,H.PurchaseAmount,H.AllAdvanceAmount As AdvanceAmount,F.IsDiamond, H.IsOtherCash, H.OtherCashAmount, F.OriginalCode,D.ItemTax,D.ItemTaxPer,G.GemTaxPer,G.GemTax AS GemTaxAmount,D.ItemTax AS ItemTaxAmount,H.AllTaxAmt,GQ.IsSolidGold,(D.GoldPrice-D.ItemTax) as Gold, D.TotalAmount As ItemAmount,H.AllTaxAmt,'Sale' As Type, H.SRTaxPer, H.SRTaxAmt, H.LocationID, L.Location, H.LastModifiedLoginUserName,'' as QRCode,F.GoldSmithID,D.DesignChargesRate,case when GQ.IsGramRate=0 then D.WasteTK*D.SalesRate else D.WasteTG*D.SalesRate end as WasteAmount,H.RedeemID,H.TopupPoint,H.TopupValue,H.RedeemPoint,H.RedeemValue,H.IsRedeemInvoice,H.MemberDis,H.MemberDiscountAmt,H.TransactionID,H.InvoiceStatus,0 as PointBalance " & _
                                 " FROM tbl_SaleInvoiceDetail D LEFT JOIN tbl_SalesInvoiceGemItem G ON G.SaleInvoiceDetailID=D.SaleInvoiceDetailiD" & _
                                 " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID" & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID" & _
                                 " LEFT JOIN tbl_GoldSmith GS ON F.GoldSmithID=GS.GoldSmithID " & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                 " ON GQ.GoldQualityID=F.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID" & _
                                 " LEFT JOIN tbl_Location L ON H.LocationID=L.LocationID " & _
                                 " WHERE H.SaleInvoiceHeaderID= @SaleInvoiceHeaderID AND H.IsDelete=0 AND F.IsDelete=0 and D.IsSaleReturn=0 ORDER BY D.SaleInvoiceDetailID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetSaleInvoiceHeaderByID(ByVal SaleInvoiceHeaderID As String) As CommonInfo.SaleInvoiceHeaderInfo Implements ISalesItemInvoiceDA.GetSaleInvoiceHeaderByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSalesInvoice As New SaleInvoiceHeaderInfo
            Try
                strCommandText = " SELECT  SaleInvoiceHeaderID, SaleDate, CustomerID, StaffID , TotalAmount, AddOrSub, DiscountAmount, PaidAmount, Remark, PromotionDiscount, (TotalAmount*PromotionDiscount)/100 As PromotionAmount, PurchaseHeaderID, PurchaseAmount, IsAdvance, EntryAdvanceDate, AllAdvanceAmount, IsCancel, CancelDate, IsOtherCash, OtherCashAmount,AllTaxAmt,SRTaxPer,SRTaxAmt,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus,TransactionID"
                strCommandText += "  FROM tbl_SaleInvoiceHeader WHERE SaleInvoiceHeaderID= @SaleInvoiceHeaderID AND IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSalesInvoice
                        .SaleInvoiceHeaderID = drResult("SaleInvoiceHeaderID")
                        .SaleDate = drResult("SaleDate")
                        .CustomerID = drResult("CustomerID")
                        .StaffID = drResult("StaffID")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .DiscountAmount = drResult("DiscountAmount")
                        .PaidAmount = drResult("PaidAmount")
                        .Remark = drResult("Remark")
                        .PromotionDiscount = drResult("PromotionDiscount")
                        .PromotionAmount = drResult("PromotionAmount")
                        .PurchaseHeaderID = drResult("PurchaseHeaderID")
                        .PurchaseAmount = drResult("PurchaseAmount")
                        .IsAdvance = drResult("IsAdvance")
                        .EntryAdvanceDate = drResult("EntryAdvanceDate")
                        .AllAdvanceAmount = drResult("AllAdvanceAmount")
                        .IsCancel = drResult("IsCancel")
                        .CancelDate = drResult("CancelDate")
                        .IsOtherCash = drResult("IsOtherCash")
                        .OtherCashAmount = drResult("OtherCashAmount")
                        .AllTaxAmt = drResult("AllTaxAmt")
                        .SRTaxPer = drResult("SRTaxPer")
                        .SRTaxAmt = drResult("SRTaxAmt")
                        .MemberID = drResult("MemberID")
                        .MemberCode = drResult("MemberCode")
                        .MemberName = drResult("MemberName")
                        .RedeemID = drResult("RedeemID")
                        .RedeemPoint = drResult("RedeemPoint")
                        .RedeemValue = drResult("RedeemValue")
                        .TopupPoint = drResult("TopupPoint")
                        .TopupValue = drResult("TopupValue")
                        '.ProcessFees = drResult("ProcessFees")
                        .MemberDiscountAmt = drResult("MemberDiscountAmt")
                        .MemberDis = drResult("MemberDis")
                        .InvoiceStatus = drResult("InvoiceStatus")
                        .IsRedeemInvoice = drResult("IsRedeemInvoice")
                        .TransactionID = IIf(IsDBNull(drResult("TransactionID")), "", drResult("TransactionID"))

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objSalesInvoice
        End Function




        Public Function GetSaleInvoiceDetailGemByHeaderID(ByVal SaleInvoiceHeaderID As String) As System.Data.DataTable Implements ISalesItemInvoiceDA.GetSaleInvoiceDetailGemByHeaderID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.SalesInvoiceGemItemID, G.SaleInvoiceDetailID As [@SaleInvoiceDetailID], G.GemsCategoryID as [@GemsCategoryID], GC.GemsCategory,G.GemsName, G.GemsTK, G.GemsTG, YOrCOrG, GemsTW, Qty, Type, UnitPrice, Amount, GemsRemark,G.GemTaxPer,G.GemTax, "
                strCommandText += " CAST(G.GemsTK AS INT) AS GemsK, "
                strCommandText += " CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY "
                strCommandText += " from tbl_SalesInvoiceGemItem G  LEFT JOIN tbl_SaleInvoiceDetail I ON G.SaleInvoiceDetailID=I.SaleInvoiceDetailID "
                strCommandText += " LEFT JOIN tbl_SaleInvoiceHeader H ON H.SaleInvoiceHeaderID=I.SaleInvoiceHeaderID  "
                strCommandText += " LEFT JOIN tbl_GemsCategory GC ON G.GemsCategoryID=GC.GemsCategoryID "
                strCommandText += " Where H.SaleInvoiceHeaderID ='" & SaleInvoiceHeaderID & "' AND H.IsDelete=0  "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceDA.GetProfitForSaleItem
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                Select Case argType

                    Case "SaleStock"
                        strCommandText = " (SELECT D.ForSaleID,convert(varchar(10),H.SaleDate,105) as SaleDate,H.SaleDate as SDate, H.SaleInvoiceHeaderID, H.SaleInvoiceHeaderID AS VoucherNo, H.CustomerID, Cu.CustomerCode, Cu.CustomerName, I.LocationID, Location,I.GoldQualityID, GoldQuality, I.ItemCategoryID, " & _
                                         " ItemCategory, S.Staff as Staff,  D.ItemCode, I.ItemNameID, N.ItemName, I.Length, D.GoldPrice, D.GemsPrice,  D.TotalAmount, " & _
                                         " D.AddOrSub, (D.TotalAmount-D.AddOrSub) AS ItemNetAmount, H.AddOrSub As TotalAddOrSub, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) as TotalNetAmount,PaidAmount, DiscountAmount," & _
                                         " H.PromotionDiscount, H.TotalAmount As AllTotalAmount, " & _
                                         " Case H.PromotionDiscount when 0 Then 0 else ((H.TotalAmount*H.PromotionDiscount)/100) End as PromotionAmount," & _
                                         " Case D.IsOriginalFixedPrice When 1 Then 0 when 0 then CASE D.OriginalPriceGram When 0 Then D.OriginalPriceTK Else D.OriginalPriceGram end end as PurchaseRate,  " & _
                                         " CASE D.IsOriginalFixedPrice WHEN 1 THEN D.OriginalFixedPrice ELSE CASE D.OriginalPriceGram WHEN 0 THEN  CAST(D.OriginalPriceTK * ((((CAST((((((D.ItemTK - D.GemsTK)+D.PurchaseWasteTK)-CAST(((D.ItemTK - D.GemsTK)+D.PurchaseWasteTK) AS INT))*16)-CAST((((D.ItemTK - D.GemsTK)+D.PurchaseWasteTK)-CAST(((D.ItemTK - D.GemsTK)+D.PurchaseWasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) / '" & Global_PToY & "') + CAST((((D.ItemTK - D.GemsTK)+D.PurchaseWasteTK)-CAST(((D.ItemTK - D.GemsTK)+D.PurchaseWasteTK) AS INT))*16 AS INT)) / 16 + CAST(((D.ItemTK - D.GemsTK)+D.PurchaseWasteTK) AS INT)) AS INT) ELSE CAST((D.OriginalPriceGram*CAST((((D.ItemTG - D.GemsTG)+D.PurchaseWasteTG)) AS DECIMAL(18,3))) AS INT) END END AS PurchaseGoldPrice, " & _
                                         " CASE D.IsOriginalFixedPrice WHEN 1 THEN D.OriginalFixedPrice ELSE CASE D.OriginalPriceGram WHEN 0 THEN  CAST(D.OriginalPriceTK * ((((CAST((((((D.ItemTK-D.GemsTK)+D.WasteTK)-CAST(((D.ItemTK-D.GemsTK)+D.WasteTK) AS INT))*16)-CAST((((D.ItemTK-D.GemsTK)+D.WasteTK)-CAST(((D.ItemTK-D.GemsTK)+D.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) / '" & Global_PToY & "') + CAST((((D.ItemTK-D.GemsTK)+D.WasteTK)-CAST(((D.ItemTK-D.GemsTK)+D.WasteTK) AS INT))*16 AS INT)) / 16 + CAST(((D.ItemTK-D.GemsTK)+D.WasteTK) AS INT)) AS INT) ELSE CAST((D.OriginalPriceGram*CAST((((D.ItemTG-D.GemsTG)+D.WasteTG)) AS DECIMAL(18,3))) AS INT) END END AS PurchaseShopWasteGoldPrice," & _
                                         " D.IsOriginalFixedPrice,D.IsOriginalPriceGram,D.OriginalPriceGram,D.OriginalPriceTK," & _
                                         " Case D.IsOriginalFixedPrice when 1 then 0 ELSE D.OriginalGemsPrice END AS OriginalGemsPrice, Case D.IsOriginalFixedPrice when 1 then 0 ELSE D.OriginalOtherPrice END AS OriginalOtherPrice, " & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 WHEN 0 THEN SalesRate END as SalesRate," & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 ELSE (I.DesignCharges+I.PlatingCharges+I.MountingCharges+I.WhiteCharges) END As OtherCharges, " & _
                                         " CAST(D.PurchaseWasteTK AS INT) AS PurchaseWasteK,  CAST((D.PurchaseWasteTK-CAST(D.PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP," & _
                                         " CAST((((D.PurchaseWasteTK-CAST(D.PurchaseWasteTK AS INT))*16)-CAST((D.PurchaseWasteTK-CAST(D.PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PurchaseWasteY, D.PurchaseWasteTK, CAST((D.PurchaseWasteTG) AS DECIMAL(18,3)) as PurchaseWasteTG, " & _
                                         " CAST(D.ItemTK-D.GemsTK AS INT) AS GoldK,  CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT) AS GoldP," & _
                                         " CAST((((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16)-CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, CAST(D.GemsTK AS INT) AS GemsK," & _
                                         " CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS GemsP, CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, CAST(D.ItemTK AS INT) AS ItemK,  CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                         " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                         " (D.ItemTK - D.GemsTK) as GoldTK, D.GemsTK, D.WasteTK, CAST((D.WasteTG) AS DECIMAL(18,3)) as WasteTG, D.ItemTK,CAST((D.ItemTG) AS DECIMAL(18,3)) as ItemTG,(CAST((D.ItemTG) AS DECIMAL(18,3)) - CAST((D.GemsTG) AS DECIMAL(18,3))) as GoldTG, CAST((D.GemsTG) AS DECIMAL(18,3)) as GemsTG, 0 AS TotalSumAmount,I.GoldSmithID,D.AddOrSub as ItemAddOrSub " & _
                                         " FROM tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader H On H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID" & _
                                         " LEFT JOIN tbl_Customer Cu ON H.CustomerID=Cu.CustomerID " & _
                                         " LEFT JOIN tbl_ForSale I ON I.ForSaleID=D.ForSaleID" & _
                                         " LEFT JOIN tbl_Location L ON L.LocationID=I.LocationID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID  LEFT JOIN tbl_ItemCategory C " & _
                                         " ON C.ItemCategoryID=I.ItemCategoryID Left Join tbl_ItemName N On N.ItemNameID = I.ItemNameID WHERE H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate and IsCancel=0 " & criStr & ")" & _
                                         " Order by H.SaleDate desc "
                    Case "OrderStock"
                        strCommandText = "(SELECT D.ForSaleID, convert(varchar(10),H.ReturnDate,105) as SaleDate,H.ReturnDate as SDate,H.OrderInvoiceID AS SaleInvoiceHeaderID, convert(varchar,H.OrderReturnHeaderID) AS VoucherNo, O.CustomerID , Cu.CustomerCode, Cu.CustomerName, H.LocationID, Location,I.GoldQualityID, GoldQuality, I.ItemCategoryID, " & _
                                         " ItemCategory, S.Staff as Staff,  D.ItemCode, I.ItemNameID, N.ItemName, I.Length, D.GoldPrice, D.GemsPrice,  D.TotalAmount, " & _
                                         " D.AddOrSub, (D.TotalAmount-D.AddOrSub) AS ItemNetAmount, H.AllTotalAmount, H.AllAddOrSub As TotalAddOrSub, 0 As PromotionAmount, ((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount) as TotalNetAmount, PaidAmount, DiscountAmount," & _
                                         " Case D.IsOriginalFixedPrice When 1 Then 0 when 0 then CASE D.OriginalPriceGram When 0 Then D.OriginalPriceTK Else D.OriginalPriceGram end end as PurchaseRate,  " & _
                                         " CASE D.IsOriginalFixedPrice WHEN 1 THEN D.OriginalFixedPrice ELSE CASE D.OriginalPriceGram WHEN 0 THEN  CAST(D.OriginalPriceTK * ((((CAST((((((I.ItemTK - I.GemsTK)+D.PurchaseWasteTK)-CAST(((I.ItemTK - I.GemsTK)+D.PurchaseWasteTK) AS INT))*16)-CAST((((I.ItemTK - I.GemsTK)+D.PurchaseWasteTK)-CAST(((I.ItemTK - I.GemsTK)+D.PurchaseWasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) / '" & Global_PToY & "') + CAST((((I.ItemTK - I.GemsTK)+D.PurchaseWasteTK)-CAST(((I.ItemTK - I.GemsTK)+D.PurchaseWasteTK) AS INT))*16 AS INT)) / 16 + CAST(((I.ItemTK - I.GemsTK)+D.PurchaseWasteTK) AS INT)) AS INT) ELSE CAST((D.OriginalPriceGram*CAST((((I.ItemTG - I.GemsTG)+D.PurchaseWasteTG)) AS DECIMAL(18,3))) AS INT) END END AS PurchaseGoldPrice, " & _
                                         " CASE D.IsOriginalFixedPrice WHEN 1 THEN D.OriginalFixedPrice ELSE CASE I.OriginalPriceGram WHEN 0 THEN  CAST(D.OriginalPriceTK * ((((CAST((((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16)-CAST((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) / '" & Global_PToY & "') + CAST((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16 AS INT)) / 16 + CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT)) AS INT) ELSE CAST((I.OriginalPriceGram*CAST((((I.ItemTG-I.GemsTG)+I.WasteTG)) AS DECIMAL(18,3))) AS INT) END END AS PurchaseShopWasteGoldPrice," & _
                                         " D.IsOriginalFixedPrice,D.IsOriginalPriceGram,D.OriginalPriceGram,D.OriginalPriceTK," & _
                                         " Case D.IsOriginalFixedPrice when 1 then 0 ELSE D.OriginalGemsPrice END AS OriginalGemsPrice, Case D.IsOriginalFixedPrice when 1 then 0 ELSE D.OriginalOtherPrice END AS OriginalOtherPrice, " & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 WHEN 0 THEN SalesRate END as SalesRate," & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 ELSE (I.DesignCharges+I.PlatingCharges+I.MountingCharges+I.WhiteCharges) END As OtherCharges, " & _
                                         " CAST(D.PurchaseWasteTK AS INT) AS PurchaseWasteK,  CAST((D.PurchaseWasteTK-CAST(D.PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP," & _
                                         " CAST((((D.PurchaseWasteTK-CAST(D.PurchaseWasteTK AS INT))*16)-CAST((D.PurchaseWasteTK-CAST(D.PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PurchaseWasteY, D.PurchaseWasteTK, CAST((D.PurchaseWasteTG) AS DECIMAL(18,3)) as PurchaseWasteTG, " & _
                                         " CAST(I.ItemTK-I.GemsTK AS INT) AS GoldK,  CAST((I.ItemTK-I.GemsTK-CAST(I.ItemTK-I.GemsTK AS INT))*16 AS INT) AS GoldP," & _
                                         " CAST((((I.ItemTK-I.GemsTK-CAST(I.ItemTK-I.GemsTK AS INT))*16)-CAST((I.ItemTK-I.GemsTK-CAST(I.ItemTK-I.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, CAST(I.GemsTK AS INT) AS GemsK," & _
                                         " CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT) AS GemsP, CAST((((I.GemsTK-CAST(I.GemsTK AS INT))*16)-CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, CAST(I.WasteTK AS INT) AS WasteK, CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT) AS WasteP, CAST((((I.WasteTK-CAST(I.WasteTK AS INT))*16)-CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, CAST(I.ItemTK AS INT) AS ItemK,  CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                         " CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                         " (I.ItemTK - I.GemsTK) as GoldTK, I.GemsTK, I.WasteTK, CAST((I.WasteTG) AS DECIMAL(18,3)) as WasteTG, I.ItemTK,CAST((I.ItemTG) AS DECIMAL(18,3)) as ItemTG,(CAST((I.ItemTG) AS DECIMAL(18,3)) - CAST((I.GemsTG) AS DECIMAL(18,3))) as GoldTG, CAST((I.GemsTG) as DECIMAL(18,3)) as GemsTG, 0 AS TotalSumAmount,I.GoldSmithID" & _
                                         " FROM tbl_OrderReturnDetail D  Left Join tbl_OrderReturnHeader H  On H.OrderReturnHeaderID=D.OrderReturnHeaderID " & _
                                         " LEFT JOIN tbl_OrderInvoice O ON H.OrderInvoiceID=O.OrderInvoiceID LEFT JOIN tbl_Customer Cu ON O.CustomerID=Cu.CustomerID " & _
                                         " LEFT JOIN tbl_ForSale I ON I.ForSaleID=D.ForSaleID" & _
                                         " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID  LEFT JOIN tbl_ItemCategory C " & _
                                         " ON C.ItemCategoryID=I.ItemCategoryID Left Join tbl_ItemName N On N.ItemNameID = I.ItemNameID WHERE H.ReturnDate BETWEEN @FromDate And @ToDate and I.IsVolume = '0' and O.IsDelete=0 and I.IsDelete=0 And H.IsDelete=0 " & criStr & ")" & _
                                         " Order by H.ReturnDate desc "

                    Case "BalanceStock"
                        strCommandText = "(SELECT  I.ForSaleID,I.GivenDate, I.LocationID, Location,I.GoldQualityID, GoldQuality, I.ItemCategoryID," & _
                                         " ItemCategory, I.ItemCode, I.ItemNameID, N.ItemName, I.Length, I.FixPrice, I.IsFixPrice, I.OriginalFixedPrice, " & _
                                         " Case I.IsOriginalFixedPrice When 1 Then 0 when 0 then CASE OriginalPriceGram When 0 Then I.OriginalPriceTK Else I.OriginalPriceGram end end as PurchaseRate,  " & _
                                         " I.IsOriginalFixedPrice, IsOriginalPriceGram, I.OriginalPriceGram, I.OriginalPriceTK, CASE I.IsOriginalFixedPrice WHEN 1 THEN 0 ELSE OriginalGemsPrice END AS OriginalGemsPrice, CASE I.IsOriginalFixedPrice WHEN 1 THEN 0 ELSE OriginalOtherPrice END AS OriginalOtherPrice, " & _
                                         " CASE I.IsOriginalFixedPrice WHEN 1 THEN I.OriginalFixedPrice ELSE CASE OriginalPriceGram WHEN 0 THEN  CAST(I.OriginalPriceTK * ((((CAST((((((I.ItemTK-I.GemsTK)+I.PurchaseWasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.PurchaseWasteTK) AS INT))*16)-CAST((((I.ItemTK-I.GemsTK)+I.PurchaseWasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.PurchaseWasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) / '" & Global_PToY & "') + CAST((((I.ItemTK-I.GemsTK)+I.PurchaseWasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.PurchaseWasteTK) AS INT))*16 AS INT)) / 16 + CAST(((I.ItemTK-I.GemsTK)+I.PurchaseWasteTK) AS INT)) AS INT) ELSE CAST((I.OriginalPriceGram*CAST((((I.ItemTG-I.GemsTG)+I.PurchaseWasteTG)) AS DECIMAL(18,3))) AS INT) END END AS PurchaseGoldPrice, " & _
                                         " CASE I.IsOriginalFixedPrice WHEN 1 THEN I.OriginalFixedPrice ELSE CASE OriginalPriceGram WHEN 0 THEN  CAST(I.OriginalPriceTK * ((((CAST((((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16)-CAST((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) / '" & Global_PToY & "') + CAST((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16 AS INT)) / 16 + CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT)) AS INT) ELSE CAST((I.OriginalPriceGram*CAST((((I.ItemTG-I.GemsTG)+I.WasteTG)) AS DECIMAL(18,3))) AS INT) END END AS PurchaseShopWasteGoldPrice," & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 WHEN 0 THEN IsNull((select SalesRate from tbl_StandardRate S where DefineDateTime =(select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID=I.GoldQualityID) AND S.GoldQualityID=I.GoldQualityID),0) END as SalesRate, G.IsGramRate, I.FixPrice," & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 ELSE (DesignCharges+PlatingCharges+MountingCharges+WhiteCharges) END As OtherCharges, " & _
                                         " CASE I.IsFixPrice WHEN 1 THEN I.FixPrice ELSE CASE G.IsGramRate WHEN 0 THEN  CAST(IsNull((select SalesRate from tbl_StandardRate S where DefineDateTime =(select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID=I.GoldQualityID) AND S.GoldQualityID=I.GoldQualityID),0) * ((((CAST((((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16)-CAST((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) / '" & Global_PToY & "') + CAST((((I.ItemTK-I.GemsTK)+I.WasteTK)-CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT))*16 AS INT)) / 16 + CAST(((I.ItemTK-I.GemsTK)+I.WasteTK) AS INT)) AS INT) ELSE CAST((IsNull((select SalesRate from tbl_StandardRate S where DefineDateTime =(select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID=I.GoldQualityID) AND S.GoldQualityID=I.GoldQualityID),0)*CAST((((I.ItemTG-I.GemsTG)+I.WasteTG)) AS DECIMAL(18,3))) AS INT) END END AS SaleGoldPrice, " & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 ELSE IsNull((SELECT Sum(Amount) FROM tbl_ForSaleGemsItem FG WHERE FG.ForSaleID=I.ForSaleID),0) END AS SaleGemsPrice, " & _
                                         " CAST(I.PurchaseWasteTK AS INT) AS PurchaseWasteK,  CAST((I.PurchaseWasteTK-CAST(I.PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP," & _
                                         " CAST((((I.PurchaseWasteTK-CAST(I.PurchaseWasteTK AS INT))*16)-CAST((I.PurchaseWasteTK-CAST(I.PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PurchaseWasteY, I.PurchaseWasteTK, CAST((I.PurchaseWasteTG) AS DECIMAL(18,3)) as PurchaseWasteTG, " & _
                                         " CAST((I.ItemTK-I.GemsTK) AS INT) AS GoldK," & _
                                         " CAST(((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16 AS INT) AS GoldP," & _
                                         " CAST(((((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16)-CAST(((I.ItemTK-I.GemsTK)-CAST((I.ItemTK-I.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS Decimal(18,2)) AS GoldY," & _
                                         " CAST(I.GemsTK AS INT) AS GemsK, CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT) AS GemsP, CAST((((I.GemsTK-CAST(I.GemsTK AS INT))*16)-CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                                         " CAST(I.WasteTK AS INT) AS WasteK, CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT) AS WasteP, CAST((((I.WasteTK-CAST(I.WasteTK AS INT))*16)-CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                         " CAST(I.ItemTK AS INT) AS ItemK,  CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP,  CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                                         " (I.ItemTK-I.GemsTK) AS GoldTK, I.GemsTK, I.WasteTK, CAST((I.WasteTG) AS DECIMAL(18,3)) as WasteTG, I.ItemTK, CAST((I.ItemTG) AS DECIMAL(18,3)) as ItemTG, (CAST((I.ItemTG) AS DECIMAL(18,3)) - CAST((I.GemsTG) AS DECIMAL(18,3))) as GoldTG, CAST((I.GemsTG) as DECIMAL(18,3)) as GemsTG,I.GoldSmithID" & _
                                         " from tbl_ForSale I  LEFT JOIN tbl_Location L ON L.LocationID=I.LocationID  " & _
                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                                         " LEFT JOIN tbl_ItemCategory C  ON C.ItemCategoryID=I.ItemCategoryID Left Join tbl_ItemName N On N.ItemNameID = I.ItemNameID " & _
                                         " WHERE I.IsDelete=0 And I.GivenDate BETWEEN @FromDate And @ToDate and I.IsVolume = '0' AND I.IsExit='0'" & criStr & ")" & _
                                         " Order by I.ItemCode "
                End Select

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

        Public Function GetSalesInvoiceDataByHeaderIDAndItemCode(SaleInvoiceHeaderID As String, Optional ItemCode As String = "", Optional argForSaleIDStr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceDataByHeaderIDAndItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If argForSaleIDStr <> "" Then
                strWhere = " AND S.ForSaleID NOT IN (" & argForSaleIDStr & ") " & ItemCode
            Else
                strWhere = ItemCode
            End If
            Try
                strCommandText = " select F.IsFixPrice as [$FixPrice],S.SaleInvoiceDetailID as [@SaleInvoiceDetailID],S.ForSaleID as [@ForSaleID],S.ItemCode,I.ItemCategory,N.ItemName,G.GoldQuality,S.SalesRate,(S.TotalAmount +S.AddorSub) as TotalAmount," & _
                                 " S.ItemTK,S.ItemTG," & _
                                 " CAST(S.ItemTK AS INT) AS ItemK," & _
                                 " CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                 " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                 " (S.ItemTK - S.GemsTK) as GoldTK,(S.ItemTG - S.GemsTG) as GoldTG, " & _
                                 " CAST((S.ItemTK - S.GemsTK) AS INT) AS GoldK," & _
                                 " CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT) AS GoldP," & _
                                 " CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY," & _
                                 " S.GemsTK, S.GemsTG, " & _
                                 " CAST(S.GemsTK AS INT) AS GemsK," & _
                                 " CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT) AS GemsP," & _
                                 " CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS  DECIMAL(18,1)) AS GemsY," & _
                                 " S.WasteTK, S.WasteTG, " & _
                                 " CAST(S.WasteTK AS INT) AS WasteK," & _
                                 " CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT) AS WasteP," & _
                                 " CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY," & _
                                 " F.ItemCategoryID as [@ItemCategoryID],F.ItemNameID as [@ItemNameID],F.GoldQualityID as [@GoldQualityID],F.Length,F.IsFixPrice as [@IsFixPrice],F.FixPrice,S.ItemTaxPer,S.ItemTax,S.AllTaxAmt" & _
                                 " FROM tbl_SaleInvoiceDetail S " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=S.ForSaleID " & _
                                 " Left Join tbl_GoldQuality G On G.GoldQualityID = F.GoldQualityID" & _
                                 " Left Join tbl_ItemCategory I On I.ItemCategoryID = F.ItemCategoryID" & _
                                 " Left Join tbl_ItemName N On N.ItemNameID = F.ItemNameID" & _
                                 " where S.SaleInvoiceHeaderID =@SaleInvoiceHeaderID and F.IsDelete=0 and S.IsReturn=0 " & strWhere


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesInvoiceGemDataBySaleDetailID(SaleInvoiceDetailID As String) As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceGemDataBySaleDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select '' as PurchaseGemID,'' as PurchaseDetailID,PG.GemsCategoryID,PG.GemsName,PG.GemsTK,PG.GemsTG,"
                strCommandText += " CAST(PG.GemsTK AS INT) AS GemsK, "
                strCommandText += " CAST((PG.GemsTK-CAST(PG.GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((PG.GemsTK-CAST(PG.GemsTK AS INT))*16)-CAST((PG.GemsTK-CAST(PG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, "
                strCommandText += " PG.YOrCOrG,PG.GemsTW as GemTW,PG.Qty as QTY,Case PG.Type WHEN '_' THEN '' ELSE PG.Type END  as FixType, 0 AS Discount, PG.UnitPrice as PurchaseRate,PG.Amount ,PG.GemTaxPer,PG.GemTax"
                strCommandText += " from tbl_SalesInvoiceGemItem PG"
                strCommandText += " where PG.SaleInvoiceDetailID=@SaleInvoiceDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, SaleInvoiceDetailID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllSalesInvoiceForPurchase(Optional ByVal IsReuseBarcode As Boolean = False) As System.Data.DataTable Implements ISalesItemInvoiceDA.GetAllSalesInvoiceForPurchase
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                If IsReuseBarcode Then

                    strCommandText = " Select SaleInvoiceHeaderID As [VoucherNo], 0 AS [$IsOrder], SaleInvoiceHeaderID As [@SaleInvoiceHeaderID], convert(varchar(10),SaleDate,105) as SaleDate, " & _
                              " H.CustomerID As [@CustomerID], CustomerName As Customer, H.StaffID AS [@StaffID], Staff, TotalAmount, AddOrSub," & _
                              " DiscountAmount, PaidAmount, H.Remark , H.PromotionDiscount, SaleDate as [@SDate] ,H.AllTaxAmt" & _
                              " from tbl_SaleInvoiceHeader H left join tbl_Staff S on H.StaffID=S.StaffID " & _
                              " left join tbl_Customer C on H.CustomerID=C.CustomerID where H.IsDelete=0 order by [@SDate] desc "

                Else

                    strCommandText = "SELECT M.* FROM(select SaleInvoiceHeaderID As [VoucherNo], 0 AS [$IsOrder], SaleInvoiceHeaderID As [@SaleInvoiceHeaderID]," & _
                                    " convert(varchar(10),SaleDate,105) as SaleDate,  H.CustomerID As [@CustomerID], CustomerName As Customer, H.StaffID AS [@StaffID], Staff, " & _
                                    " TotalAmount, AddOrSub, DiscountAmount, PaidAmount, H.Remark , H.PromotionDiscount, SaleDate as [@SDate],H.AllTaxAmt" & _
                                    " from tbl_SaleInvoiceHeader H left join tbl_Staff S on H.StaffID=S.StaffID  left join tbl_Customer C on H.CustomerID=C.CustomerID  " & _
                                    " Where (Select Count(D.SaleInvoiceDetailID) FROM tbl_SaleInvoiceDetail D WHERE D.IsReturn=0 AND D.SaleInvoiceHeaderID=H.SaleInvoiceHeaderID)>0 And IsCancel=0 AND H.IsDelete=0 " & _
                                    " UNION ALL " & _
                                    " select H.OrderInvoiceID As [VoucherNo], 1 AS [$IsOrder], Convert(varchar,OrderReturnHeaderID) As [@SaleInvoiceHeaderID], " & _
                                    " convert(varchar(10),ReturnDate,105) as SaleDate,  " & _
                                    " O.CustomerID As [@CustomerID], CustomerName As Customer, H.StaffID AS [@StaffID], Staff, H.AllTotalAmount As TotalAmount,  H.AllAddOrSub AS AddOrSub, " & _
                                    " 0 AS DiscountAmount, (H.PaidAmount+H.FromGoldAmount+H.AdvanceAmount) AS PaidAmount, H.Remark , 0 As PromotionDiscount, ReturnDate as [@SDate]   from tbl_OrderReturnHeader H LEFT JOIN " & _
                                    " tbl_OrderInvoice O ON O.OrderInvoiceID=H.OrderInvoiceID left join tbl_Staff S " & _
                                    " on H.StaffID=S.StaffID  left join tbl_Customer C on O.CustomerID=C.CustomerID" & _
                                    " Where (Select Count(D.OrderInvoiceDetailID) FROM tbl_OrderReturnDetail D WHERE D.IsReturn=0 AND D.OrderReturnHeaderID=H.OrderReturnHeaderID)>0) As M order by [@SDate] desc "

                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesInvoiceReport(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT H.SaleInvoiceHeaderID,G.SalesInvoiceGemItemID, G.GemsCategoryID, GC.GemsCategory, G.GemsName," & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY,G.GemsTK, CAST((G.GemsTG) AS DECIMAL(18,3)) as GemsTG," & _
                                 " G.YOrCOrG, G.GemsTW, G.Qty , G.Type, G.UnitPrice, G.Amount As GemAmount, G.GemsRemark,  " & _
                                 " D.SaleInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID, " & _
                                 " GQ.GoldQuality, F.ItemCategoryID, C.ItemCategory, F.Width, F.FixPrice, F.DesignCharges, F.PlatingCharges, F.MountingCharges, " & _
                                 " F.WhiteCharges, F.Photo, D.SalesRate,0 AS SumTotalAmount, F.IsDiamond,GQ.IsGramRate as IsGram, " & _
                                 " D.GoldPrice, D.GemsPrice, D.IsFixPrice,D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount," & _
                                 " D.ItemTK, CAST((D.ItemTG) AS DECIMAL(18,3)) As ItemTG, D.GemsTK As TotalGemsTK, CAST((D.GemsTG) AS DECIMAL(18,3)) AS TotalGemsTG, D.WasteTK, CAST((D.WasteTG) AS DECIMAL(18,3)) as WasteTG, D.ItemTK+D.WasteTK As TotalTK, (CAST((D.ItemTG) As Decimal(18,3)) + CAST((D.WasteTG) As DECIMAL(18,3))) As TotalTG, D.ItemTK-D.GemsTK As GoldTK,(CAST((D.ItemTG) As Decimal(18,3)) - CAST((D.GemsTG) As DECIMAL(18,3))) AS GoldTG, " & _
                                 "  CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                                 " CAST(D.GemsTK AS INT) AS TotalGemsK,  CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS TotalGemsP, " & _
                                 " CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemsY, " & _
                                 " CAST(D.WasteTK AS INT) AS WasteK,  CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                 " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY," & _
                                 " CAST(D.ItemTK+D.WasteTK AS INT) AS TotalK,  CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT) AS TotalP, " & _
                                 " CAST((((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16)-CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY," & _
                                 " CAST(D.ItemTK-D.GemsTK AS INT) AS GoldK,  CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST((((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16)-CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY," & _
                                 " CAST(F.PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((F.PurchaseWasteTK-CAST(F.PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,  " & _
                                 " CAST((((F.PurchaseWasteTK-CAST(F.PurchaseWasteTK AS INT))*16)-CAST((F.PurchaseWasteTK-CAST(F.PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY," & _
                                 " CAST(F.WasteTK AS INT) AS ShopWasteK, CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT) AS ShopWasteP," & _
                                 " CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ShopWasteY," & _
                                 " H.SaleInvoiceHeaderID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,S.StaffID, S.Staff, H.Remark, H.TotalAmount,H.AddOrSub,  " & _
                                 " (H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, ((H.TotalAmount*PromotionDiscount)/100) As PromotionAmount, H.DiscountAmount,H.RedeemValue,H.RedeemID,H.TopupPoint,H.TopupValue,H.RedeemPoint,H.MemberDis,H.MemberDiscountAmt,H.TransactionID,H.InvoiceStatus," & _
                                 " (((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount-H.AddOrSub+H.RedeemValue+H.MemberDiscountAmt) AS TotalDiscountAmount, CASE WHEN H.PaidAmount<0 THEN (H.PurchaseAmount+AllAdvanceAmount+OtherCashAmount) ELSE (H.PaidAmount+H.PurchaseAmount+AllAdvanceAmount+OtherCashAmount) END AS TotalPaidAmount, " & _
                                 " CASE WHEN (IsAdvance=1 AND IsCancel=1) THEN 0 ELSE (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-(H.PaidAmount+H.PurchaseAmount+H.AllAdvanceAmount+H.OtherCashAmount+H.RedeemValue+H.MemberDiscountAmt)) END As BalanceAmount, " & _
                                 "  H.PaidAmount, H.IsOtherCash, H.OtherCashAmount, D.IsSaleReturn, " & _
                                 " CASE PurchaseHeaderID WHEN '' THEN 0 ELSE 1 END As IsChange, H.PurchaseHeaderID, H.PurchaseAmount, IsAdvance, H.EntryAdvanceDate, H.AllAdvanceAmount, H.IsCancel, H.CancelDate, H.SaleDate AS [@SaleDate],CASE F.GoldSmithID When '0' Then F.GoldSmith else GG.Name END as GoldSmith,D.ItemTax,G.GemTaxPer,G.GemTax,H.AllTaxAmt, 0 AS CashInAmount, 0 AS CashOutAmount,F.OriginalCode,F.GoldSmithID " & _
                                 " FROM tbl_SaleInvoiceDetail D LEFT JOIN tbl_SalesInvoiceGemItem G ON G.SaleInvoiceDetailID=D.SaleInvoiceDetailiD" & _
                                 " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID" & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                 " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=F.GoldSmithID " & _
                                 "  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " WHERE F.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & Cristr & " Order by  H.SaleInvoiceHeaderID DESC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DBComm.CommandTimeout = 1000
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function





        Public Function GetSalesInvoiceReportForSummaryReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceReportForSummaryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select  D.ForSaleID, D.ItemCode, H.SaleDate as SaleDate, (D.TotalAmount+D.AddOrSub) As NetAmount," & _
                                " CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,  " & _
                                 " CAST(D.WasteTK AS INT) AS WasteK, CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,  " & _
                                "Case D.IsFixPrice When 1 Then 0 Else D.SalesRate END As SalesRate, D.ItemTK, Cast(D.ItemTG AS DECIMAL(18,3)) as ItemTG, " & _
                                 " D.GemsTK, CAST(D.GemsTG as DECIMAL(18,3)) as GemsTG, D.WasteTK, CAST(D.WasteTG as DECIMAL(18,3)) as WasteTG, D.GoldPrice, D.GemsPrice, D.IsFixPrice, D.TotalAmount, D.AddOrSub, " & _
                                " F.ItemCategoryID, C.ItemCategory,  D.ItemTK+D.WasteTK As TotalTK, CAST(D.ItemTG+D.WasteTG as DECIMAL(18,3)) As TotalTG, D.ItemTK-D.GemsTK As GoldTK, " & _
                                " CAST(D.ItemTG-D.GemsTG as DECIMAL(18,3)) as GoldTG,  F.ItemNameID, I.ItemName, F.GoldQualityID, GQ.GoldQuality,H.SaleDate AS [@SDate],D.ItemTax,H.AllTaxAmt ," & _
                                " Isnull((select sum(GemTax) from tbl_SalesInvoiceGemItem where SaleInvoiceDetailID=D.SaleInvoiceDetailID),0) AS GemTaxAmount," & _
                                " D.ItemTax AS ItemTaxAmount, D.TotalAmount As ItemAmount,H.AllTaxAmt,'Sale' As Type" & _
                                " FROM tbl_SaleInvoiceDetail D  LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID  " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID " & _
                                " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID " & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID    LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " WHERE F.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate " & cristr

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

        Public Function GetSalesInvoiceReportForTotal(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceReportForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Distinct(H.SaleInvoiceHeaderID), H.TotalAmount,H.AddOrSub,((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount) )As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount," & _
                                 " H.DiscountAmount, CASE WHEN H.PaidAmount<0 THEN (H.PurchaseAmount+AllAdvanceAmount+OtherCashAmount) ELSE (H.PaidAmount+H.PurchaseAmount+AllAdvanceAmount+OtherCashAmount) END AS PaidAmount, CASE WHEN (IsAdvance=1 AND IsCancel=1) THEN CASE WHEN H.PaidAmount<0 THEN (AllAdvanceAmount+H.PurchaseAmount+H.OtherCashAmount)*(-1) ELSE (H.PaidAmount+AllAdvanceAmount+H.PurchaseAmount+H.OtherCashAmount)*(-1) END ELSE H.PaidAmount END As ChangeAmount, " & _
                                 " CASE WHEN (IsAdvance=1 AND IsCancel=1) THEN CASE WHEN H.PaidAmount<0 THEN (AllAdvanceAmount+H.PurchaseAmount+H.OtherCashAmount) ELSE (H.PaidAmount+AllAdvanceAmount+H.PurchaseAmount+H.OtherCashAmount) END ELSE CASE WHEN H.PaidAmount<0 THEN H.PaidAmount*(-1) ELSE 0 END END As RefundAmount, " & _
                                 " CASE WHEN (IsAdvance=1 AND IsCancel=1) THEN 0 ELSE (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-(H.PaidAmount+H.PurchaseAmount+H.AllAdvanceAmount+H.OtherCashAmount)) END As BalanceAmount,D.ItemTax,H.AllTaxAmt" & _
                                 " FROM tbl_SaleInvoiceDetail D " & _
                                 " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                 " left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " WHERE H.IsDelete=0 AND F.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & cristr

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


        Public Function GetSaleInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As SalesInvoiceDetailInfo Implements ISalesItemInvoiceDA.GetSaleInvoiceDetailForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New SalesInvoiceDetailInfo
            Try

                strCommandText = " select Count(D.SaleInvoiceDetailID) As QTY, Sum(CAST((D.ItemTG) AS DECIMAL(18,3))) AS ItemTG,Sum(CAST((D.GemsTG) AS DECIMAL(18,3))) AS GemsTG, Sum(CAST((D.WasteTG) AS DECIMAL(18,3))) AS WasteTG,(Sum(CAST((D.ItemTG) AS DECIMAL(18,3))) + Sum(CAST((D.WasteTG) AS DECIMAL(18,3)))) As TotalTG, (Sum(CAST((D.ItemTG) AS DECIMAL(18,3))) - Sum(CAST((D.GemsTG) AS DECIMAL(18,3)))) AS GoldTG, " & _
                                " Sum(D.ItemTK) AS ItemTK,Sum(D.GemsTK) AS GemsTK, Sum(D.WasteTK) AS WasteTK, Sum(D.ItemTK+D.WasteTK) As TotalTK, Sum(D.ItemTK-D.GemsTK) AS GoldTK, " & _
                                " sum(D.TotalAmount+D.AddOrSub) As ItemAmount, SUM(D.AddOrSub) AS AddOrSub,sum(D.ItemTax) as ItemTax,sum(H.AllTaxAmt) as AllTaxAmt" & _
                                " FROM tbl_SaleInvoiceDetail D " & _
                                " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
                                " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                " left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                " WHERE H.IsDelete=0 AND F.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & criStr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .QTY = drResult("QTY")
                        .ItemTG = IIf(IsDBNull(drResult("ItemTG")), 0, drResult("ItemTG"))
                        .GoldTG = IIf(IsDBNull(drResult("GoldTG")), 0, drResult("GoldTG"))
                        .WasteTG = IIf(IsDBNull(drResult("WasteTG")), 0, drResult("WasteTG"))
                        .GemsTG = IIf(IsDBNull(drResult("GemsTG")), 0, drResult("GemsTG"))
                        .TotalTG = IIf(IsDBNull(drResult("TotalTG")), 0, drResult("TotalTG"))
                        .ItemTK = IIf(IsDBNull(drResult("ItemTK")), 0, drResult("ItemTK"))
                        .GoldTK = IIf(IsDBNull(drResult("GoldTK")), 0, drResult("GoldTK"))
                        .WasteTK = IIf(IsDBNull(drResult("WasteTK")), 0, drResult("WasteTK"))
                        .GemsTK = IIf(IsDBNull(drResult("GemsTK")), 0, drResult("GemsTK"))
                        .TotalTK = IIf(IsDBNull(drResult("TotalTK")), 0, drResult("TotalTK"))
                        .ItemTax = IIf(IsDBNull(drResult("ItemTax")), 0, drResult("ItemTax"))

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetAllSaleInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceDA.GetAllSaleInvoiceVoucherPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT G.SalesInvoiceGemItemID, G.GemsCategoryID, GC.GemsCategory, G.GemsName, " & _
                                  " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY,G.GemsTK, G.GemsTG, G.YOrCOrG, G.GemsTW, G.Qty , G.Type, G.UnitPrice, G.Amount As GemAmount, G.GemsRemark,   D.SaleInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID,  GQ.GoldQuality, F.ItemCategoryID, C.ItemCategory, F.Width, F.FixPrice, F.DesignCharges, F.PlatingCharges, F.MountingCharges,  F.WhiteCharges, F.Photo, D.SalesRate, D.GoldPrice, D.GemsPrice, D.IsFixPrice,D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, D.ItemTK, CAST((D.ItemTG) AS DECIMAL(18,3)) as ItemTG, D.GemsTK As TotalGemsTK, CAST((D.GemsTG) as DECIMAL(18,3)) AS TotalGemsTG, D.WasteTK, CAST((D.WasteTG) AS DECIMAL(18,3)) as WasteTG, D.ItemTK+D.WasteTK As TotalTK, (CAST((D.ItemTG) AS DECIMAL(18,3)) + CAST((D.WasteTG) AS DECIMAL(18,3))) As TotalTG, D.ItemTK-D.GemsTK As GoldTK, (CAST((D.ItemTG) AS DECIMAL(18,3)) - CAST((D.GemsTG) AS DECIMAL(18,3))) As GoldTG,   CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS ItemY,  CAST(D.GemsTK AS INT) AS TotalGemsK,  CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS TotalGemsP,  CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalGemsY,  CAST(D.WasteTK AS INT) AS WasteK,  CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS WasteY, CAST(D.ItemTK+D.WasteTK AS INT) AS TotalK,  CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT) AS TotalP,  CAST((((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16)-CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY, CAST(D.ItemTK-D.GemsTK AS INT) AS GoldK,  CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT) AS GoldP,  CAST((((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16)-CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GoldY, H.SaleInvoiceHeaderID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, H.TotalAmount,H.AddOrSub,   (H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-H.PaidAmount) As BalanceAmount,F.Photo, H.SaleDate AS [@SDate],D.ItemTaxPer,D.ItemTax,G.GemTaxPer,G.GemTax,D.AllTaxAmt " & _
                                  " FROM tbl_SaleInvoiceDetail D LEFT JOIN tbl_SalesInvoiceGemItem G ON G.SaleInvoiceDetailID=D.SaleInvoiceDetailiD" & _
                                  " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID" & _
                                  " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID" & _
                                  " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                  " ON GQ.GoldQualityID=F.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                  " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID" & _
                                  " WHERE H.IsDelete=0 AND F.IsDelete=0 AND H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr & " ORDER BY [@SDate] DESC, H.SaleInvoiceHeaderID ASC"


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
        Public Function GetSaleInvoiceGemDataBySaleInvoiceGemsItemID(ByVal SalesInvoiceGemItemID As String) As DataTable Implements ISalesItemInvoiceDA.GetSaleInvoiceGemDataBySaleInvoiceGemsItemID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select * from tbl_SalesInvoiceGemItem "
                strCommandText += " where SalesInvoiceGemItemID=@SalesInvoiceGemItemID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesInvoiceGemItemID", DbType.String, SalesInvoiceGemItemID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSalesInvoiceDetailPrintByID(ByVal SaleInvoiceHeaderID As String) As System.Data.DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceDetailPrintByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT D.SaleInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID,  GQ.GoldQuality,GQ.IsGramRate, F.ItemCategoryID, " & _
                                " C.ItemCategory, F.Width, F.FixPrice, D.DesignCharges, D.PlatingCharges, D.MountingCharges,  D.WhiteCharges, F.Photo, D.SalesRate, D.GoldPrice, D.GemsPrice, D.IsFixPrice, " & _
                                " D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, D.ItemTK, D.ItemTG, D.GemsTK As TotalGemsTK, D.GemsTG AS TotalGemsTG, " & _
                                " D.WasteTK, D.WasteTG, (D.ItemTK-D.GemsTK)+D.WasteTK As TotalTK, (D.ItemTG-D.GemsTG)+D.WasteTG As TotalTG, D.ItemTK-D.GemsTK As GoldTK, D.ItemTG-D.GemsTG AS GoldTG,   CAST(D.ItemTK AS INT) AS ItemK, CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,  CAST(D.GemsTK AS INT) AS TotalGemsK,  CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT) AS TotalGemsP, " & _
                                " CAST((((D.GemsTK-CAST(D.GemsTK AS INT))*16)-CAST((D.GemsTK-CAST(D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemsY,  CAST(D.WasteTK AS INT) AS WasteK,  CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT) AS TotalK,  CAST(((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16 AS INT) AS TotalP,  " & _
                                " CAST(((((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16)-CAST(((D.ItemTK-D.GemsTK)+D.WasteTK-CAST((D.ItemTK-D.GemsTK)+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,CAST(D.ItemTK-D.GemsTK AS INT) AS GoldK,  CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT) AS GoldP, " & _
                                " CAST((((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16)-CAST((D.ItemTK-D.GemsTK-CAST(D.ItemTK-D.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, H.SaleInvoiceHeaderID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerCode, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, H.TotalAmount,H.AddOrSub, " & _
                                " (H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, H.DiscountAmount, H.PaidAmount, H.IsOtherCash, H.OtherCashAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-(H.PaidAmount+H.PurchaseAmount+H.AllAdvanceAmount+H.OtherCashAmount)) As BalanceAmount, " & _
                                " (D.DesignCharges+D.PlatingCharges+D.MountingCharges+D.WhiteCharges) AS TotalCharges,H.PurchaseHeaderID,H.PurchaseAmount,H.AllAdvanceAmount  As AdvanceAmount,F.IsDiamond,D.ItemTaxPer,D.ItemTax,H.AllTaxAmt,H.SRTaxPer,H.SRTaxAmt,case when GQ.IsGramRate=0 then D.WasteTK*D.SalesRate else D.WasteTG*D.SalesRate end as WasteAmount " & _
                                " FROM tbl_SaleInvoiceDetail D " & _
                                " LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID" & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID " & _
                                " LEFT JOIN tbl_GoldSmith GS ON F.GoldSmithID=GS.GoldSmithID " & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID   " & _
                                " left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID" & _
                                " WHERE H.SaleInvoiceHeaderID= @SaleInvoiceHeaderID AND H.IsDelete=0 AND F.IsDelete=0 ORDER BY D.SaleInvoiceDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSumProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesItemInvoiceDA.GetSumProfitForSaleItem
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                Select Case argType

                    Case "SaleStock"
                        strCommandText = " (SELECT Distinct(H.SaleInvoiceHeaderID) AS SaleInvoiceHeaderID,convert(varchar(10),H.SaleDate,105) as SaleDate,H.SaleDate as SDate, Count(D.ItemCode) AS QTY, Sum(D.ItemTK) AS ItemTK, Sum(D.ItemTG) AS ItemTG,  " & _
                                         "  H.AddOrSub As TotalAddOrSub, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount)) as TotalNetAmount,PaidAmount, DiscountAmount,D.ItemTaxPer,D.ItemTax," & _
                                         " H.PromotionDiscount," & _
                                         " Case H.PromotionDiscount when 0 Then 0 else ((H.TotalAmount*H.PromotionDiscount)/100) End as PromotionAmount " & _
                                         " FROM tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader H On H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID" & _
                                         " LEFT JOIN tbl_ForSale I ON I.ForSaleID=D.ForSaleID" & _
                                         " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID  LEFT JOIN tbl_ItemCategory C " & _
                                         " ON C.ItemCategoryID=I.ItemCategoryID Left Join tbl_ItemName N On N.ItemNameID = I.ItemNameID WHERE H.IsDelete=0 AND I.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDateAND IsCancel=0 " & criStr & " Group By H.SaleInvoiceHeaderID, H.SaleDate,H.TotalAmount, H.AddOrSub, H.PaidAmount, H.DiscountAmount,H.PromotionDiscount) order by H.SaleDate desc"
                    Case "OrderStock"
                        strCommandText = " (SELECT Distinct(H.OrderInvoiceID) AS SaleInvoiceHeaderID,convert(varchar(10),H.ReturnDate,105) AS SaleDate,H.ReturnDate as SDate, Count(D.ItemCode) AS QTY, Sum(I.ItemTK) AS ItemTK, Sum(I.ItemTG) AS ItemTG,  " & _
                                         "  H.AllAddOrSub As TotalAddOrSub, ((H.AllTotalAmount+H.AllAddOrSub)-DiscountAmount) as TotalNetAmount,PaidAmount, DiscountAmount, 0 As PromotionAmount ," & _
                                         "  0 As PurchasePrice " & _
                                          " FROM tbl_OrderReturnDetail D  Left Join tbl_OrderReturnHeader H  On H.OrderReturnHeaderID=D.OrderReturnHeaderID " & _
                                         " LEFT JOIN tbl_ForSale I ON I.ForSaleID=D.ForSaleID" & _
                                         " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID  LEFT JOIN tbl_ItemCategory C " & _
                                         " ON C.ItemCategoryID=I.ItemCategoryID Left Join tbl_ItemName N On N.ItemNameID = I.ItemNameID WHERE H.IsDelete=0 AND I.IsDelete=0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & criStr & " Group By H.OrderInvoiceID, H.ReturnDate,H.AllTotalAmount, H.AllAddOrSub, H.PaidAmount, H.DiscountAmount) order by H.ReturnDate desc"

                    Case "BalanceStock"
                        strCommandText = "(SELECT  I.TotalGemPrice as GemsPrice,I.ForSaleID,I.GivenDate, I.LocationID, Location,I.GoldQualityID, GoldQuality, I.ItemCategoryID," & _
                                         " ItemCategory, I.ItemCode, I.ItemNameID, N.ItemName, I.Length, I.FixPrice, I.IsFixPrice, I.OriginalFixedPrice, " & _
                                         " Case I.IsOriginalFixedPrice When 1 Then 0 when 0 then CASE OriginalPriceGram When 0 Then I.OriginalPriceTK Else I.OriginalPriceGram end end as PurchaseRate,  " & _
                                         " I.IsOriginalFixedPrice, IsOriginalPriceGram, I.OriginalPriceGram, I.OriginalPriceTK, OriginalGemsPrice, " & _
                                         " OriginalOtherPrice,0 as PurchasePrice,0 as PurchaseRate,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges, G.IsGramRate," & _
                                         " CASE I.IsFixPrice WHEN 1 THEN 0 WHEN 0 THEN R.SalesRate END as SalesRate, I.FixPrice,0 as SalesPrice," & _
                                         " CAST(I.PurchaseWasteTK AS INT) AS PurchaseWasteK,  CAST((I.PurchaseWasteTK-CAST(I.PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP," & _
                                         " CAST((((I.PurchaseWasteTK-CAST(I.PurchaseWasteTK AS INT))*16)-CAST((I.PurchaseWasteTK-CAST(I.PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PurchaseWasteY, I.PurchaseWasteTK, CAST((I.PurchaseWasteTG) AS DECIMAL(18,3)) as PurchaseWasteTG, " & _
                                         " CAST(I.GoldTK AS INT) AS GoldK," & _
                                         " CAST((I.GoldTK-CAST(I.GoldTK AS INT))*16 AS INT) AS GoldP," & _
                                         " CAST((((I.GoldTK-CAST(I.GoldTK AS INT))*16)-CAST((I.GoldTK-CAST(I.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS Decimal(18,2)) AS GoldY," & _
                                         " CAST(I.GemsTK AS INT) AS GemsK, CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT) AS GemsP, CAST((((I.GemsTK-CAST(I.GemsTK AS INT))*16)-CAST((I.GemsTK-CAST(I.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                                         " CAST(I.WasteTK AS INT) AS WasteK, CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT) AS WasteP, CAST((((I.WasteTK-CAST(I.WasteTK AS INT))*16)-CAST((I.WasteTK-CAST(I.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                         " CAST(I.ItemTK AS INT) AS ItemK,  CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP,  CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                                         " I.GoldTK, I.GemsTK, I.WasteTK, CAST((I.WasteTG) AS DECIMAL(18,3)) as WasteTG, I.ItemTK, CAST((I.ItemTG) AS DECIMAL(18,3)) as ItemTG, CAST((I.GoldTG) AS DECIMAL(18,3)) as GoldTG, CAST((I.GemsTG) as DECIMAL(18,3)) as GemsTG" & _
                                         " from tbl_ForSale I  LEFT JOIN tbl_Location L ON L.LocationID=I.LocationID  " & _
                                         " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=I.GoldQualityID " & _
                                         " LEFT JOIN tbl_ItemCategory C  ON C.ItemCategoryID=I.ItemCategoryID Left Join tbl_ItemName N On N.ItemNameID = I.ItemNameID " & _
                                         " WHERE I.IsDelete=0 AND I.GivenDate BETWEEN @FromDate And @ToDateand I.IsVolume = '0' AND I.IsExit='0'" & criStr & " Order by I.ItemCode "

                End Select

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

        Public Function GetForSalePercentageReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal SortBy As String, Optional ByVal cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetForSalePercentageReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                If SortBy = "Asc" Then
                    strCommandText = " Select CONVERT(VARCHAR(10),H.SaleDate,105) as SaleDate,F.ItemCategoryID,I.ItemCategory,Count(D.ItemCode)As ItemCode,D.ItemTaxPer,D.ItemTax" & _
                                " From tbl_SaleInvoiceDetail D LEFT JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                " LEFT JOIN  tbl_ForSale F  on F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID  " & _
                                " WHERE H.IsDelete=0 AND F.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDateAnd F.IsExit=1" & cristr & _
                                " Group By F.ItemCategoryID ,CONVERT(VARCHAR(10),H.SaleDate,105),I.ItemCategory,D.ItemTaxPer,D.ItemTax" & _
                                " order by SubString(CONVERT(VARCHAR(10),H.SaleDate,105),7,4) asc," & _
                                " SubString(CONVERT(VARCHAR(10),H.SaleDate,105),4,2) asc,SubString(CONVERT(VARCHAR(10),H.SaleDate,105),1,2) asc"
                Else

                    strCommandText = " Select CONVERT(VARCHAR(10),H.SaleDate,105) as SaleDate,F.ItemCategoryID,I.ItemCategory,Count(D.ItemCode)As ItemCode,D.ItemTaxPer,D.ItemTax" & _
                                " From tbl_SaleInvoiceDetail D LEFT JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                " LEFT JOIN  tbl_ForSale F  on F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID  " & _
                                " WHERE H.IsDelete=0 AND F.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDateAnd F.IsExit=1" & cristr & _
                                " Group By F.ItemCategoryID ,CONVERT(VARCHAR(10),H.SaleDate,105),I.ItemCategory,D.ItemTaxPer,D.ItemTax" & _
                                " order by SubString(CONVERT(VARCHAR(10),H.SaleDate,105),7,4) desc," & _
                                " SubString(CONVERT(VARCHAR(10),H.SaleDate,105),4,2) desc,SubString(CONVERT(VARCHAR(10),H.SaleDate,105),1,2) desc"
                End If

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

        Public Function GetForSaleGem(SaleInvoiceHeaderID As String) As DataTable Implements ISalesItemInvoiceDA.GetForSaleGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.* From tbl_SalesInvoiceGemItem G LEFT JOIN tbl_SaleInvoiceDetail D on D.SaleInvoiceDetailID=G.SaleInvoiceDetailID " & _
                                 " LEFT JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID Where H.SaleInvoiceHeaderID=@SaleInvoiceHeaderID AND H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateSaleInvoiceDetailByIsReturn(Obj As SalesInvoiceDetailInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateSaleInvoiceDetailByIsReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_SaleInvoiceDetail set IsReturn=@IsReturn "
                strCommandText += " where SaleInvoiceDetailID=@SaleInvoiceDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceDetailID", DbType.String, Obj.SaleInvoiceDetailID)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
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
        Public Function UpdateConsignmentSaleDetailByIsReturn(Obj As ConsignmentSaleItemInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateConsignmentSaleDetailByIsReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_ConsignmentSaleItem set IsReturn=@IsReturn "
                strCommandText += " where ConsignmentSaleItemID=@ConsignmentSaleItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ConsignmentSaleItemID", DbType.String, Obj.ConsignmentSaleItemID)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
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

        Public Function UpdateSaleGemsItemByIsReturn(Obj As SaleGemsItemInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateSaleGemsItemByIsReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_SaleGemsItem set IsReturn=@IsReturn "
                strCommandText += " where SaleGemsItemID=@SaleGemsItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleGemsItemID", DbType.String, Obj.SaleGemsItemID)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
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
        Public Function UpdateSaleLooseDiamondByIsReturn(Obj As SaleLooseDiamondDetailInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateSaleLooseDiamondByIsReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_SaleLooseDiamondDetail set IsReturn=@IsReturn "
                strCommandText += " where SaleLooseDiamondDetailID=@SaleLooseDiamondDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondDetailID", DbType.String, Obj.SaleLooseDiamondDetailID)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, Obj.IsReturn)
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
        Public Function GetMostSaleItemData(FromDate As Date, ToDate As Date, Optional ByVal cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetMostSaleItemData
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " Select F.GoldQualityID,F.ItemCategoryID,F.ItemNameID,I.ItemCategory,N.ItemName,G.GoldQuality, D.ItemTK, D.ItemTG, D.WasteTK, D.WasteTG ,D.ItemTaxPer,D.ItemTax" & _
                                 " From tbl_SaleInvoiceDetail D LEFT JOIN tbl_ForSale F  on F.ForSaleID=D.ForSaleID " & _
                                 " LEFT JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID " & _
                                 " LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID " & _
                                 " Where H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate And IsExit='1' " & cristr & " Order By F.ItemCategoryID ASC"

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

        Public Function GetSaleSummaryReportByDateANDByItemName(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSaleSummaryReportByDateANDByItemName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select H.SaleInvoiceHeaderID, D.SaleInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemCategoryID, I.ItemCategory, F.ItemNameID, N.ItemName, F.GoldQualityID, GQ.GoldQuality, H.SaleInvoiceHeaderID, H.SaleDate, D.ItemTK, CAST((D.ItemTG) AS DECIMAL(18,3)) As ItemTG, D.WasteTK, CAST((D.WasteTG) AS DECIMAL(18,3)) As WasteTG, (D.TotalAmount+D.AddOrSub) AS ItemNetAmount,  " & _
                                 " H.PaidAmount AS NetAmount, ((H.DiscountAmount+((H.TotalAmount*H.PromotionDiscount)/100))-H.AddOrSub) AS AddOrSub, 0 AS TotalNetAmount, 0 AS TotalAddOrSub,D.ItemTaxPer,D.ItemTax " & _
                                " FROM tbl_SaleInvoiceDetail D  LEFT JOIN  tbl_SaleInvoiceHeader H  ON H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID  " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID " & _
                                " LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                " WHERE H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & cristr & " Order by  H.SaleInvoiceHeaderID DESC "

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

        Public Function GetBalanceStockByValue(ByVal cristr As String) As DataTable Implements ISalesItemInvoiceDA.GetBalanceStockByValue
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT F.ForSaleID, F.ItemCode, F.GoldQualityID, G.GoldQuality,  (SELECT SalesRate FROM tbl_StandardRate WHERE GoldQualityID = F.GoldQualityID AND DefineDateTime = (select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID = F.GoldQualityID)) AS SalesRate,  " & _
                                 " (SELECT IsGramRate FROM tbl_GoldQuality WHERE GoldQualityID = F.GoldQualityID) AS IsGram, " & _
                                 " (F.ItemTK-F.GemsTK) AS GoldTK,(F.ItemTG-F.GemsTG) AS GoldTG, F.ItemTK, F.ItemTG, " & _
                                 " CAST((F.ItemTK-F.GemsTK) AS INT) AS GoldK, " & _
                                 " CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                                 " CAST(F.ItemTK AS INT) AS ItemK, " & _
                                 " CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                                 " CASE (SELECT IsGramRate FROM tbl_GoldQuality WHERE GoldQualityID = F.GoldQualityID) WHEN 0 THEN  CAST((SELECT SalesRate FROM tbl_StandardRate WHERE GoldQualityID = F.GoldQualityID AND DefineDateTime = (select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID = F.GoldQualityID))*   (((CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) / '" & Global_PToY & "') + CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT)) / 16 + CAST((F.ItemTK-F.GemsTK) AS INT)) AS INT) " & _
                                 " ElSE CAST(((SELECT SalesRate FROM tbl_StandardRate WHERE GoldQualityID = F.GoldQualityID AND DefineDateTime = (select MAX(DefineDateTime) FROM tbl_StandardRate WHERE GoldQualityID = F.GoldQualityID))* (F.ItemTG-F.GemsTG)) AS INT)  END AS GoldPrice,   " & _
                                 " IsNull((SELECT Sum(Amount) FROM tbl_ForSaleGemsItem FG WHERE FG.ForSaleID=F.ForSaleID),0) AS GemsPrice, (F.DesignCharges+F.PlatingCharges+F.MountingCharges+F.WhiteCharges) AS OtherCharges " & _
                                 " FROM tbl_ForSale F LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=F.GoldQualityID " & _
                                 " WHERE IsExit=0 AND IsVolume=0 ANd F.IsDelete=0 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function SetSalesInvoiceToReturn(ByVal SalesInvoiceID As String, Optional ByVal IsReturn As Integer = 0) As Boolean Implements ISalesItemInvoiceDA.SetSalesInvoiceToReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleInvoice set IsSalesReturn=@IsReturn "
                strCommandText += " where SaleInvoiceID= @SaleInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceID)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, IsReturn)
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
        Public Function GetForSaleIDBySaleInvoice(ByVal SalesInvoiceID As String) As String Implements ISalesItemInvoiceDA.GetForSaleIDBySaleInvoice
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "SELECT ForSaleID FROM tbl_SaleInvoice WHERE  "
                strCommandText += " SaleInvoiceID = @SaleInvoiceID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceID", DbType.String, SalesInvoiceID)
                GetForSaleIDBySaleInvoice = CStr(DB.ExecuteScalar(DBComm))
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function GetSalesInvoiceDataByCustomerIDAndItemCode(CustomerID As String, Optional argForSaleIDStr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceDataByCustomerIDAndItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String = ""

            If CustomerID <> "" Then
                strWhere = argForSaleIDStr + " AND M.[@CustomerID]='" & CustomerID & "' "
            Else
                strWhere = argForSaleIDStr
            End If
            Try
                strCommandText = "SELECT M.* FROM " & _
                                 " (select S.ItemCode,'' as [@ConsignmentSaleItemID], H.SaleInvoiceHeaderID AS VoucherNo, H.SaleInvoiceHeaderID AS [@SaleInvoiceHeaderID], CONVERT(VARCHAR(10),H.SaleDate,105) as Date, I.ItemCategory As [ItemCategory_], N.ItemName AS [ItemName_], CONVERT(VARCHAR,CAST(S.ItemTG AS DECIMAL(18,3))) AS ItemTG, CASE WHEN (select count(SaleInvoiceDetailID) from tbl_SaleInvoiceDetail where SaleInvoiceHeaderID=H.SaleInvoiceHeaderID AND IsReturn=0)=1 THEN CONVERT(varchar,((H.TotalAmount +H.AddOrSub)-H.DiscountAmount)) ELSE CONVERT(varchar,(S.TotalAmount +S.AddorSub)) END as TotalAmount, " & _
                                 " G.GoldQuality AS [GoldQuality_], Convert(VARCHAR,S.SalesRate) As SaleRate, CASE WHEN (select count(SaleInvoiceDetailID) from tbl_SaleInvoiceDetail where SaleInvoiceHeaderID=H.SaleInvoiceHeaderID AND IsReturn=0)=1 THEN CONVERT(varchar,((H.TotalAmount +H.AddOrSub)-H.DiscountAmount)) ELSE CONVERT(varchar,(S.TotalAmount +S.AddorSub)) END as [@TotalAmount], S.SalesRate As [@SalesRate],  H.SaleDate AS [@SDate], H.CustomerID AS [@CustomerID], 0 AS [@IsOrder], S.SaleInvoiceDetailID as [@SaleInvoiceDetailID],'' as [@SaleGemsItemID],S.ForSaleID as [@ForSaleID],(S.TotalAmount +S.AddorSub) as [@ItemNetAmount], " & _
                                 " S.ItemTK [@ItemTK], S.ItemTG AS [@ItemTG]," & _
                                 " CONVERT(VARCHAR,CAST(S.ItemTK AS INT)) AS ItemK," & _
                                 " CONVERT(VARCHAR,CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT)) AS ItemP," & _
                                 " CONVERT(VARCHAR,CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS ItemY," & _
                                 " (S.ItemTK - S.GemsTK) as [@GoldTK], (S.ItemTG - S.GemsTG) as [@GoldTG], CONVERT(VARCHAR,CAST((S.ItemTG - S.GemsTG) AS DECIMAL(18,3))) as GoldTG, " & _
                                 " CONVERT(VARCHAR,CAST((S.ItemTK - S.GemsTK) AS INT)) AS GoldK," & _
                                 " CONVERT(VARCHAR,CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT)) AS GoldP," & _
                                 " CONVERT(VARCHAR,CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS GoldY," & _
                                 " S.GemsTK AS [@GemsTK], S.GemsTG AS [@GemsTG], CONVERT(VARCHAR,CAST(S.GemsTG AS DECIMAL(18,3))) as GemsTG, " & _
                                 " CONVERT(VARCHAR,CAST(S.GemsTK AS INT)) AS GemsK," & _
                                 " CONVERT(VARCHAR,CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT)) AS GemsP," & _
                                 " CONVERT(VARCHAR,CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS  DECIMAL(18,1))) AS GemsY," & _
                                 " S.WasteTK AS [@WasteTK], S.WasteTG AS [@WasteTG], CONVERT(VARCHAR,CAST(S.WasteTG AS DECIMAL(18,3))) AS WasteTG, " & _
                                 " CONVERT(VARCHAR,CAST(S.WasteTK AS INT)) AS WasteK," & _
                                 " CONVERT(VARCHAR,CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT)) AS WasteP," & _
                                 " CONVERT(VARCHAR,CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY," & _
                                 " CASE F.Length WHEN '-' THEN '' ELSE  F.Length END + CASE F.Width WHEN '-' THEN '' ELSE  F.Width END  AS Length , F.ItemCategoryID as [@ItemCategoryID],F.ItemNameID as [@ItemNameID],F.GoldQualityID as [@GoldQualityID],F.IsFixPrice as [@IsFixPrice], F.FixPrice AS [@FixPrice], F.Photo AS [@Photo], F.OriginalCode As [@OriginalCode], F.IsDiamond AS [@IsDiamond], S.GoldPrice AS [@GoldPrice],S.AddOrSub As [@AddOrSub], S.GemsPrice AS [@GemsPrice], (F.DesignCharges+ F.PlatingCharges+F.MountingCharges+F.WhiteCharges) AS [@TotalCharges] " & _
                                 " FROM tbl_SaleInvoiceDetail S " & _
                                 " LEFT JOIN tbl_SaleInvoiceHeader H ON H.SaleInvoiceHeaderID=S.SaleInvoiceHeaderID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=S.ForSaleID " & _
                                 " Left Join tbl_GoldQuality G On G.GoldQualityID = F.GoldQualityID" & _
                                 " Left Join tbl_ItemCategory I On I.ItemCategoryID = F.ItemCategoryID" & _
                                 " Left Join tbl_ItemName N On N.ItemNameID = F.ItemNameID" & _
                                 " where S.IsReturn=0 AND H.IsDelete=0 and F.IsDelete=0 And F.IsExit=1 " & _
                                 " UNION ALL " & _
                                 " select S.ItemCode,S.ConsignmentSaleItemID as [@ConsignmentSaleItemID],  H.ConsignmentSaleID AS VoucherNo, H.WholeSaleInvoiceID AS [@SaleInvoiceHeaderID], " & _
                                 " CONVERT(VARCHAR(10),H.ConsignDate,105) as Date, I.ItemCategory As [ItemCategory_], N.ItemName AS [ItemName_], " & _
                                 " CONVERT(VARCHAR,CAST(S.ItemTG AS DECIMAL(18,3))) AS ItemTG, CASE WHEN (select count(WholesaleInvoiceItemID) " & _
                                 " from tbl_WholeSaleInvoiceItem where WholesaleInvoiceID=H.WholesaleInvoiceID AND IsReturn=0)=1 " & _
                                 " THEN CONVERT(varchar,((H.NetAmount +H.AddOrSub)-H.Discount)) ELSE " & _
                                 " CONVERT(varchar,(H.NetAmount +H.AddorSub)) END as TotalAmount,  G.GoldQuality AS [GoldQuality_], " & _
                                 " Convert(VARCHAR,S.SalesRate) As SaleRate, CASE WHEN (select count(WholesaleInvoiceItemID) from tbl_WholeSaleInvoiceItem  " & _
                                 " where WholesaleInvoiceID=H.WholesaleInvoiceID AND IsReturn=0)=1 THEN " & _
                                 " CONVERT(varchar,((H.NetAmount +H.AddOrSub)-H.Discount)) ELSE CONVERT(varchar,(H.NetAmount +H.AddorSub)) END  as [@TotalAmount], S.SalesRate As [@SalesRate],  H.ConsignDate AS [@SDate], H.CustomerID AS [@CustomerID], " & _
                                 " 0 AS [@IsOrder], '' as [@SaleInvoiceDetailID],'' as [@SaleGemsItemID],S.ForSaleID as [@ForSaleID],(S.FixPrice) as [@ItemNetAmount],  " & _
                                 " S.ItemTK [@ItemTK], S.ItemTG AS [@ItemTG], CONVERT(VARCHAR,CAST(S.ItemTK AS INT)) AS ItemK, " & _
                                 " CONVERT(VARCHAR,CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT)) AS ItemP, " & _
                                 " CONVERT(VARCHAR,CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2))) AS ItemY, (S.ItemTK - S.GemsTK) as [@GoldTK], (S.ItemTG - S.GemsTG) as [@GoldTG], " & _
                                 " CONVERT(VARCHAR,CAST((S.ItemTG - S.GemsTG) AS DECIMAL(18,3))) as GoldTG,  " & _
                                 " CONVERT(VARCHAR,CAST((S.ItemTK - S.GemsTK) AS INT)) AS GoldK, " & _
                                 " CONVERT(VARCHAR,CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT)) AS GoldP, " & _
                                 " CONVERT(VARCHAR,CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)- CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2))) AS GoldY, S.GemsTK AS [@GemsTK], " & _
                                 " S.GemsTG AS [@GemsTG], CONVERT(VARCHAR,CAST(S.GemsTG AS DECIMAL(18,3))) as GemsTG,  " & _
                                 " CONVERT(VARCHAR,CAST(S.GemsTK AS INT)) AS GemsK, CONVERT(VARCHAR,CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT)) AS GemsP, " & _
                                 " CONVERT(VARCHAR,CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'8.0' AS  DECIMAL(18,1))) AS GemsY, S.WasteTK AS [@WasteTK], S.WasteTG AS [@WasteTG], " & _
                                 " CONVERT(VARCHAR,CAST(S.WasteTG AS DECIMAL(18,3))) AS WasteTG,  CONVERT(VARCHAR,CAST(S.WasteTK AS INT)) AS WasteK, " & _
                                 " CONVERT(VARCHAR,CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT)) AS WasteP, " & _
                                 " CONVERT(VARCHAR,CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2))) AS WasteY, CASE F.Length WHEN '-' THEN '' ELSE  F.Length END + CASE F.Width WHEN '-' THEN '' ELSE  F.Width END  AS Length , F.ItemCategoryID as [@ItemCategoryID],F.ItemNameID as [@ItemNameID],F.GoldQualityID as [@GoldQualityID], " & _
                                 " F.IsFixPrice as [@IsFixPrice], F.FixPrice AS [@FixPrice], F.Photo AS [@Photo], F.OriginalCode As [@OriginalCode], " & _
                                 " F.IsDiamond AS [@IsDiamond], S.GoldPrice AS [@GoldPrice],0 As [@AddOrSub], 0 AS [@GemsPrice], " & _
                                 " (F.DesignCharges+ F.PlatingCharges+F.MountingCharges+F.WhiteCharges) AS [@TotalCharges]  FROM tbl_ConsignmentSaleItem S  " & _
                                 " LEFT JOIN tbl_ConsignmentSale H ON H.ConsignmentSaleID=S.ConsignmentSaleID  " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=S.ForSaleID  Left Join tbl_GoldQuality G On G.GoldQualityID = F.GoldQualityID " & _
                                 " Left Join tbl_ItemCategory I On I.ItemCategoryID = F.ItemCategoryID Left Join tbl_ItemName N On N.ItemNameID = F.ItemNameID " & _
                                 " where S.IsReturn=0 AND H.IsDelete=0 and F.IsDelete=0 And F.IsExit=1 " & _
                                 " UNION ALL " & _
                                 " select O.ItemCode, '' As [@ConsignmentSaleItemID],OH.OrderInvoiceID AS VoucherNo, CONVERT(VARCHAR(10),H.OrderReturnHeaderID) As [@SaleInvoiceHeaderID], CONVERT(VARCHAR(10),H.ReturnDate,105) as Date,I.ItemCategory AS [ItemCategory_], N.ItemName As [ItemName_], CONVERT(VARCHAR,CAST(S.ItemTG AS DECIMAL(18,3))) AS ItemTG, CASE WHEN (select count(OrderInvoiceDetailID) from tbl_OrderReturnDetail where OrderReturnHeaderID=H.OrderReturnHeaderID AND IsReturn=0)=1 THEN CONVERT(varchar,((H.AllTotalAmount +H.AllAddOrSub)-H.DiscountAmount)) ELSE CONVERT(varchar,(O.TotalAmount +O.AddOrSub)) END as TotalAmount, G.GoldQuality AS [GoldQuality_]," & _
                                 " Convert(VARCHAR,O.SalesRate) As SaleRate, CASE WHEN (select count(OrderInvoiceDetailID) from tbl_OrderReturnDetail where OrderReturnHeaderID=H.OrderReturnHeaderID AND IsReturn=0)=1 THEN CONVERT(varchar,((H.AllTotalAmount +H.AllAddOrSub)-H.DiscountAmount)) ELSE CONVERT(varchar,(O.TotalAmount +O.AddOrSub)) END as [@TotalAmount],O.SalesRate AS [@SalesRate], H.ReturnDate AS [@SDate], OH.CustomerID AS [@CustomerID], 1 AS [@IsOrder], O.OrderInvoiceDetailID as [@SaleInvoiceDetailID],'' as [@SaleGemsItemID],O.ForSaleID as [@ForSaleID],(O.TotalAmount +O.AddorSub) as [@ItemNetAmount]," & _
                                 " S.ItemTK AS [@ItemTK], S.ItemTG AS [@ItemTG], " & _
                                 " CONVERT(VARCHAR,CAST(S.ItemTK AS INT)) AS ItemK," & _
                                 " CONVERT(VARCHAR,CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT)) AS ItemP," & _
                                 "  CONVERT(VARCHAR,CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS ItemY," & _
                                 " (S.ItemTK - S.GemsTK) as [@GoldTK], (S.ItemTG - S.GemsTG) as [@GoldTG], CONVERT(VARCHAR,CAST((S.ItemTG - S.GemsTG) AS DECIMAL(18,3))) as GoldTG," & _
                                 "  CONVERT(VARCHAR,CAST((S.ItemTK - S.GemsTK) AS INT)) AS GoldK," & _
                                 "  CONVERT(VARCHAR,CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT)) AS GoldP," & _
                                 "  CONVERT(VARCHAR,CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS GoldY," & _
                                 " S.GemsTK AS [@GemsTK], S.GemsTG AS [@GemsTG], CONVERT(VARCHAR, CAST(S.GemsTG AS DECIMAL(18,3))) as GemsTG," & _
                                 "  CONVERT(VARCHAR,CAST(S.GemsTK AS INT)) AS GemsK," & _
                                 "  CONVERT(VARCHAR,CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT)) AS GemsP," & _
                                 "  CONVERT(VARCHAR,CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) AS GemsY," & _
                                 " S.WasteTK AS [@WasteTK], S.WasteTG AS [@WasteTG], CONVERT(VARCHAR, CAST(S.WasteTG AS DECIMAL(18,3))) as WasteTG, " & _
                                 "  CONVERT(VARCHAR,CAST(S.WasteTK AS INT)) AS WasteK," & _
                                 "  CONVERT(VARCHAR,CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT)) AS WasteP," & _
                                 "  CONVERT(VARCHAR,CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY," & _
                                 " CASE S.Length WHEN '-' THEN '' ELSE  S.Length END + CASE S.Width WHEN '-' THEN '' ELSE  S.Width END  AS Length, S.ItemCategoryID as [@ItemCategoryID],S.ItemNameID as [@ItemNameID],S.GoldQualityID as [@GoldQualityID] ,S.IsFixPrice as [@IsFixPrice],S.FixPrice [@FixPrice], S.Photo As [@Photo], S.OriginalCode As [@OriginalCode], S.IsDiamond AS [@IsDiamond], O.GoldPrice AS [@GoldPrice],O.AddOrSub As [@AddOrSub], O.GemsPrice AS [@GemsPrice], (S.DesignCharges+ S.PlatingCharges+S.MountingCharges+S.WhiteCharges) AS [@TotalCharges] from tbl_OrderReturnDetail O" & _
                                 " LEFT JOIN tbl_OrderReturnHeader H ON H.OrderReturnHeaderID=O.OrderReturnHeaderID " & _
                                 " LEFT JOIN tbl_OrderInvoice OH ON OH.OrderInvoiceID=H.OrderInvoiceID " & _
                                 " Left Join tbl_ForSale S on S.ForSaleID = O.ForSaleID " & _
                                 " Left Join tbl_GoldQuality G On G.GoldQualityID = S.GoldQualityID" & _
                                 " Left Join tbl_ItemCategory I On I.ItemCategoryID = S.ItemCategoryID" & _
                                 " Left Join tbl_ItemName N On N.ItemNameID = S.ItemNameID" & _
                                 " where IsReturn=0 AND H.IsDelete=0 And OH.IsDelete=0 AND S.IsDelete=0) AS M WHERE 1=1 " & strWhere & " ORDER BY [@SDate] DESC, VoucherNo DESC"



                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertRecordCash(ByVal Obj As CommonInfo.OtherCashInfo) As Boolean Implements ISalesItemInvoiceDA.InsertRecordCash
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
        Public Function UpdateRecordCash(ByVal Obj As CommonInfo.OtherCashInfo) As Boolean Implements ISalesItemInvoiceDA.UpdateRecordCash
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

        Public Function DeleteRecordCash(ByVal RecordCashID As String) As Boolean Implements ISalesItemInvoiceDA.DeleteRecordCash
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

        Public Function GetOtherCashDataByVoucherNo(ByVal SaleInvoiceHeaderID As String) As DataTable Implements ISalesItemInvoiceDA.GetOtherCashDataByVoucherNo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT RecordCashID, VoucherNo, R.CashTypeID, C.CashType, ExchangeRate, Amount, (ExchangeRate*Amount) AS Total FROM tbl_RecordOtherCash R LEFT JOIN tbl_CashType C ON R.CashTypeID=C.CashTypeID WHERE  VoucherNo=@VoucherNo "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@VoucherNo", DbType.String, SaleInvoiceHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesInvoiceDataSaleDetailID(Optional argForSaleIDStr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceDataSaleDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String = ""
            Try
                strCommandText = "SELECT M.IsDiamond AS [$Diamond] , M.* FROM " & _
                                 " (select H.SaleInvoiceHeaderID AS VoucherNo, H.SaleInvoiceHeaderID AS [@SaleInvoiceHeaderID], CONVERT(VARCHAR(10),H.SaleDate,105) as Date, H.SaleDate AS [@SDate], H.CustomerID AS [@CustomerID], 0 AS [@IsOrder], S.SaleInvoiceDetailID as [@SaleInvoiceDetailID],S.ForSaleID as [@ForSaleID],S.ItemCode,I.ItemCategory,N.ItemName,G.GoldQuality,S.SalesRate,(S.TotalAmount +S.AddorSub) as TotalAmount," & _
                                 " S.ItemTK, S.ItemTG," & _
                                 " CAST(S.ItemTK AS INT) AS ItemK," & _
                                 " CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                 " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                 " (S.ItemTK - S.GemsTK) as GoldTK, (S.ItemTG - S.GemsTG) as GoldTG, " & _
                                 " CAST((S.ItemTK - S.GemsTK) AS INT) AS GoldK," & _
                                 " CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT) AS GoldP," & _
                                 " CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY," & _
                                 " S.GemsTK, S.GemsTG, " & _
                                 " CAST(S.GemsTK AS INT) AS GemsK," & _
                                 " CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT) AS GemsP," & _
                                 " CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS  DECIMAL(18,1)) AS GemsY," & _
                                 " S.WasteTK, S.WasteTG, " & _
                                 " CAST(S.WasteTK AS INT) AS WasteK," & _
                                 " CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT) AS WasteP," & _
                                 " CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY," & _
                                 " F.ItemCategoryID as [@ItemCategoryID],F.ItemNameID as [@ItemNameID],F.GoldQualityID as [@GoldQualityID],F.Length,F.IsFixPrice as [@IsFixPrice],F.FixPrice, F.Photo, F.OriginalCode, F.IsDiamond, S.GoldPrice, S.GemsPrice, (F.DesignCharges+ F.PlatingCharges+F.MountingCharges+F.WhiteCharges) AS TotalCharges" & _
                                 " FROM tbl_SaleInvoiceDetail S " & _
                                 " LEFT JOIN tbl_SaleInvoiceHeader H ON H.SaleInvoiceHeaderID=S.SaleInvoiceHeaderID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=S.ForSaleID " & _
                                 " Left Join tbl_GoldQuality G On G.GoldQualityID = F.GoldQualityID" & _
                                 " Left Join tbl_ItemCategory I On I.ItemCategoryID = F.ItemCategoryID" & _
                                 " Left Join tbl_ItemName N On N.ItemNameID = F.ItemNameID WHERE H.IsDelete=0 and F.IsDelete=0 " & _
                                 " UNION ALL " & _
                                 " select OH.OrderInvoiceID AS VoucherNo, CONVERT(VARCHAR(10),H.OrderReturnHeaderID) As [@SaleInvoiceHeaderID], CONVERT(VARCHAR(10),H.ReturnDate,105) as Date, H.ReturnDate AS [@SDate], OH.CustomerID AS [@CustomerID], 1 AS [@IsOrder], O.OrderInvoiceDetailID as [@SaleInvoiceDetailID],O.ForSaleID as [@ForSaleID],O.ItemCode,I.ItemCategory,N.ItemName,G.GoldQuality,O.SalesRate,(O.TotalAmount +O.AddorSub) as TotalAmount," & _
                                 " S.ItemTK, S.ItemTG, " & _
                                 " CAST(S.ItemTK AS INT) AS ItemK," & _
                                 " CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                 " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                 " (S.ItemTK - S.GemsTK) as GoldTK, (S.ItemTG - S.GemsTG) as GoldTG, " & _
                                 " CAST((S.ItemTK - S.GemsTK) AS INT) AS GoldK," & _
                                 " CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT) AS GoldP," & _
                                 " CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY," & _
                                 " S.GemsTK, S.GemsTG," & _
                                 " CAST(S.GemsTK AS INT) AS GemsK," & _
                                 " CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT) AS GemsP," & _
                                 " CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY," & _
                                 " S.WasteTK, S.WasteTG," & _
                                 " CAST(S.WasteTK AS INT) AS WasteK," & _
                                 " CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT) AS WasteP," & _
                                 " CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY," & _
                                 " S.ItemCategoryID as [@ItemCategoryID],S.ItemNameID as [@ItemNameID],S.GoldQualityID as [@GoldQualityID] ,S.Length, S.IsFixPrice as [@IsFixPrice],S.FixPrice, S.Photo , S.OriginalCode, S.IsDiamond, O.GoldPrice, O.GemsPrice, (S.DesignCharges+ S.PlatingCharges+S.MountingCharges+S.WhiteCharges) AS TotalCharges from tbl_OrderReturnDetail O" & _
                                 " LEFT JOIN tbl_OrderReturnHeader H ON H.OrderReturnHeaderID=O.OrderReturnHeaderID " & _
                                 " LEFT JOIN tbl_OrderInvoice OH ON OH.OrderInvoiceID=H.OrderInvoiceID " & _
                                 " Left Join tbl_ForSale S on S.ForSaleID = O.ForSaleID " & _
                                 " Left Join tbl_GoldQuality G On G.GoldQualityID = S.GoldQualityID" & _
                                 " Left Join tbl_ItemCategory I On I.ItemCategoryID = S.ItemCategoryID" & _
                                 " Left Join tbl_ItemName N On N.ItemNameID = S.ItemNameID where OH.IsDelete=0 AND H.IsDelete=0 And S.IsDelete=0 ) AS M WHERE 1=1 " & strWhere & " ORDER BY [@SDate] DESC, VoucherNo DESC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllSalesInvoiceDataByItemCode() As System.Data.DataTable Implements ISalesItemInvoiceDA.GetAllSalesInvoiceDataByItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select  D.ItemCode, convert(varchar,CAST(D.ItemTG AS DECIMAL(18,3))) as Gram,  D.SaleInvoiceDetailID As [@SaleInvoiceDetailID],  H.SaleInvoiceHeaderID  AS VoucherNo, convert(varchar(10),SaleDate,105) as SaleDate, H.CustomerID As [@CustomerID], CustomerName As Customer, CONVERT(VARCHAR,((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))) AS NetAmount, CONVERT(VARCHAR,H.PaidAmount) AS PaidAmount, H.StaffID AS [@StaffID], Staff, H.IsAdvance AS [$IsAdvance], CONVERT(VARCHAR,H.TotalAmount) As TotalAmount, CONVERT(VARCHAR,H.AddOrSub) AS AddOrSub, CONVERT(VARCHAR,H.DiscountAmount) AS Discount, H.Remark , CONVERT(VARCHAR,H.PromotionDiscount) AS Promotion,SaleDate as [@SDate], PurchaseHeaderID AS SaleReturnVoucher, CONVERT(VARCHAR,PurchaseAmount) AS SaleReturnAmount, convert(varchar(10),EntryAdvanceDate,105) as AdvanceDate, CONVERT(VARCHAR,AllAdvanceAmount) AS AdvanceAmount, H.IsOtherCash AS [$IsOtherCash], CONVERT(VARCHAR,H.OtherCashAmount) as OtherCash ,H.AllTaxAmt"
                strCommandText += " from  tbl_SaleInvoiceDetail D LEFT JOIN tbl_SaleInvoiceHeader H ON D.SaleInvoiceHeaderID=H.SaleInvoiceHeaderID left join tbl_Staff S on H.StaffID=S.StaffID left join tbl_Customer C on H.CustomerID=C.CustomerID WHERE H.IsDelete=0 order by H.SaleInvoiceHeaderID desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSalesInvoiceTaxVoucher(ByVal SaleInvoiceHeaderID As String) As DataTable Implements ISalesItemInvoiceDA.GetSalesInvoiceTaxVoucher
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                'strCommandText = " select ItemTaxPer, sum(ItemTax) AS ItemTaxAmount from(select SD.ItemTaxPer, SD.ItemTax " & _
                '                  " FROM tbl_SaleInvoiceDetail SD INNER JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=SD.SaleInvoiceHeaderID Where H.SaleInvoiceHeaderID=@SaleInvoiceHeaderID " & _
                '                  " union all" & _
                '                  " select SG.GemTaxPer as ItemTaxPer, SG.GemTax as ItemTax" & _
                '                  " FROM tbl_SalesInvoiceGemItem SG INNER JOIN tbl_SaleInvoiceDetail SD ON SG.SaleInvoiceDetailID=SD.SaleInvoiceDetailID INNER JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=SD.SaleInvoiceHeaderID " & _
                '                  " Where H.SaleInvoiceHeaderID=@SaleInvoiceHeaderID) As M group by ItemTaxPer"
                strCommandText = "select ItemTaxPer, sum(ItemTax) AS ItemTaxAmount from (select SD.ItemTaxPer,SD.ItemTax as ItemTax " & _
                                "FROM tbl_SaleInvoiceDetail SD INNER JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=SD.SaleInvoiceHeaderID " & _
                                " union all select SG.GemTaxPer as ItemTaxPer, SG.GemTax as ItemTax FROM tbl_SalesInvoiceGemItem SG " & _
                                "INNER JOIN tbl_SaleInvoiceDetail SD ON SG.SaleInvoiceDetailID=SD.SaleInvoiceDetailID " & _
                                "INNER JOIN tbl_SaleInvoiceHeader SH on SH.SaleInvoiceHeaderID=SD.SaleInvoiceHeaderID " & _
                                "Where SH.SaleInvoiceHeaderID=@SaleInvoiceHeaderID) As M group by ItemTaxPer"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceHeaderID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetTaxSummaryReport(FromDate As Date, ToDate As Date, Optional ByVal cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetTaxSummaryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "Select H.SaleInvoiceHeaderID As VouNo,H.SaleDate As SaleDate,F.ItemCategoryID, C.ItemCategory, D.SaleInvoiceDetailID, " & _
                                 "Isnull((select sum(GemTax) from tbl_SalesInvoiceGemItem where SaleInvoiceDetailID=D.SaleInvoiceDetailID),0) AS GemTaxAmount, " & _
                                 "D.ItemTax AS ItemTaxAmount, D.TotalAmount As ItemAmount,H.AllTaxAmt,H.CustomerID,'Sale' As Type,Cu.CustomerName " & _
                                 "FROM tbl_SaleInvoiceHeader H " & _
                                 "LEFT JOIN tbl_SaleInvoiceDetail D ON D.SaleInvoiceHeaderID=H.SaleInvoiceHeaderID " & _
                                 "LEFT JOIN tbl_SalesInvoiceGemItem G ON G.SaleInvoiceDetailID=D.SaleInvoiceDetailID " & _
                                 "LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 "LEFT JOIN tbl_Customer Cu ON Cu.CustomerID = H.CustomerID " & _
                                 "LEFT JOIN tbl_Location L on L.LocationID = H.LocationID " & _
                                 "WHERE H.IsDelete = 0 AND F.IsDelete=0 AND H.AllTaxAmt > 0 AND H.SaleDate BETWEEN @FromDate And @ToDate" & cristr & _
                                 "UNION ALL " & _
                                 "Select H.OrderInvoiceID As VouNo,H.ReturnDate AS SaleDate,F.ItemCategoryID, C.ItemCategory, D.OrderInvoiceDetailID As SaleInvoiceDetailID, " & _
                                 "Isnull((select sum(GemTax) from tbl_OrderReturnGemsItem where OrderInvoiceDetailID=D.OrderInvoiceDetailID),0) AS GemTaxAmount," & _
                                 "D.ItemTax AS ItemTaxAmount, D.TotalAmount As ItemAmount,H.AllTaxAmt,R.CustomerID,'Order' As Type ,Cu.CustomerName " & _
                                 "FROM tbl_OrderReturnHeader H " & _
                                 "LEFT JOIN tbl_OrderInvoice R ON R.OrderInvoiceID = H.OrderInvoiceID " & _
                                 "LEFT JOIN tbl_OrderReturnDetail D ON D.OrderReturnHeaderID=H.OrderReturnHeaderID " & _
                                 "LEFT JOIN tbl_OrderReturnGemsItem G ON G.OrderInvoiceDetailID=D.OrderInvoiceDetailID " & _
                                 "LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 "LEFT JOIN tbl_Customer Cu ON Cu.CustomerID = R.CustomerID LEFT JOIN tbl_Location L on L.LocationID = H.LocationID " & _
                                 "WHERE H.IsDelete=0 AND R.IsDelete=0 AND F.IsDelete=0 AND H.AllTaxAmt > 0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & cristr

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
        Public Function GetSaleTaxSummaryReport(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetSaleTaxSummaryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "Select H.SaleInvoiceHeaderID As SaleInvoiceHeaderID,H.SaleDate As SaleDate,F.ItemCategoryID, C.ItemCategory, D.SaleInvoiceDetailID, " & _
                                 "Isnull((select sum(GemTax) from tbl_SalesInvoiceGemItem where SaleInvoiceDetailID=D.SaleInvoiceDetailID),0) AS GemTaxAmount, " & _
                                 "D.ItemTax AS ItemTaxAmount, D.TotalAmount As ItemAmount,H.AllTaxAmt,H.CustomerID,'Sale' As Type " & _
                                 "FROM tbl_SaleInvoiceHeader H " & _
                                 "LEFT JOIN tbl_SaleInvoiceDetail D ON D.SaleInvoiceHeaderID=H.SaleInvoiceHeaderID " & _
                                 "LEFT JOIN tbl_SalesInvoiceGemItem G ON G.SaleInvoiceDetailID=D.SaleInvoiceDetailID " & _
                                 "LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 "LEFT JOIN tbl_Customer Cu ON Cu.CustomerID = H.CustomerID " & _
                                 "LEFT JOIN tbl_Location L on L.LocationID = H.LocationID " & _
                                 "WHERE H.IsDelete = 0 And F.IsDelete=0 AND H.AllTaxAmt > 0 " & _
                                 "And H.SaleDate BETWEEN @FromDate And @ToDate" & Cristr & " Order by  [SaleDate] DESC "
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

        Public Function GetOrderTaxSummaryReport(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements ISalesItemInvoiceDA.GetOrderTaxSummaryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "Select H.OrderReturnHeaderID,H.ReturnDate,F.ItemCategoryID, C.ItemCategory, D.OrderInvoiceDetailID, " & _
                                 "Isnull((select sum(GemTax) from tbl_OrderReturnGemsItem where OrderInvoiceDetailID=D.OrderInvoiceDetailID),0) AS GemTaxAmount," & _
                                 "D.ItemTax AS ItemTaxAmount, D.TotalAmount As ItemAmount,H.AllTaxAmt,R.CustomerID,'Order' As Type " & _
                                 "FROM tbl_OrderReturnHeader H " & _
                                 "LEFT JOIN tbl_OrderInvoice R ON R.OrderInvoiceID = H.OrderInvoiceID " & _
                                 "LEFT JOIN tbl_OrderReturnDetail D ON D.OrderReturnHeaderID=H.OrderReturnHeaderID " & _
                                 "LEFT JOIN tbl_OrderReturnGemsItem G ON G.OrderInvoiceDetailID=D.OrderInvoiceDetailID " & _
                                 "LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 "LEFT JOIN tbl_Customer Cu ON Cu.CustomerID = R.CustomerID LEFT JOIN tbl_Location L on L.LocationID = H.LocationID " & _
                                 "WHERE H.IsDelete=0 AND R.IsDelete=0 AND F.IsDelete=0 AND H.AllTaxAmt > 0 AND H.ReturnDate BETWEEN @FromDate And @ToDate" & Cristr & " Order by  [ReturnDate] DESC"

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
        Public Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondID As String) As CommonInfo.SaleLooseDiamondHeaderInfo Implements ISalesItemInvoiceDA.GetSaleLooseDiamondHeaderByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSaleLooseDiamond As New SaleLooseDiamondHeaderInfo
            Try
                strCommandText = " SELECT  SaleLooseDiamondID, SaleDate, CustomerID, StaffID , TotalAmount, AddOrSub, DiscountAmount, PaidAmount, Remark, PromotionDiscount, (TotalAmount*PromotionDiscount)/100 As PromotionAmount, PurchaseHeaderID, PurchaseAmount, IsOtherCash, OtherCashAmount,AllTaxAmt,SRTaxPer,SRTaxAmt"
                strCommandText += "  FROM tbl_SaleLooseDiamondHeader WHERE SaleLooseDiamondID= @SaleLooseDiamondID AND IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleLooseDiamondID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSaleLooseDiamond
                        .SaleLooseDiamondID = drResult("SaleLooseDiamondID")
                        .SaleDate = drResult("SaleDate")
                        .CustomerID = drResult("CustomerID")
                        .StaffID = drResult("StaffID")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .DiscountAmount = drResult("DiscountAmount")
                        .PaidAmount = drResult("PaidAmount")
                        .Remark = drResult("Remark")
                        .PromotionDiscount = drResult("PromotionDiscount")
                        .PromotionAmount = drResult("PromotionAmount")
                        .PurchaseHeaderID = drResult("PurchaseHeaderID")
                        .PurchaseAmount = drResult("PurchaseAmount")
                        .IsOtherCash = drResult("IsOtherCash")
                        .OtherCashAmount = drResult("OtherCashAmount")
                        .AllTaxAmt = drResult("AllTaxAmt")
                        .SRTaxPer = drResult("SRTaxPer")
                        .SRTaxAmt = drResult("SRTaxAmt")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objSaleLooseDiamond
        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements ISalesItemInvoiceDA.UpdateRedeemID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_SaleInvoiceHeader SET RedeemID=@RedeemID WHERE  SaleInvoiceHeaderID= @SaleInvoiceHeaderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceID)
                DB.AddInParameter(DBComm, "@RedeemID", DbType.String, RedeemID)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Update RedeemID for this invoice. ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean Implements ISalesItemInvoiceDA.UpdateTransactionID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_SaleInvoiceHeader SET TransactionID=@TransactionID WHERE  SaleInvoiceHeaderID= @SaleInvoiceHeaderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleInvoiceHeaderID", DbType.String, SaleInvoiceID)
                DB.AddInParameter(DBComm, "@TransactionID", DbType.String, TransactionID)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Update transactionID for this invoice. ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
    End Class
End Namespace

