Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace SaleLooseDiamond
    Public Class SaleLooseDiamondDA
        Implements ISaleLooseDiamondDA

#Region "Private SalesVolume"

        Private DB As Database
        Private Shared ReadOnly _instance As ISaleLooseDiamondDA = New SaleLooseDiamondDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISaleLooseDiamondDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function InsertSaleLooseDiamondHeader(ByVal Obj As CommonInfo.SaleLooseDiamondHeaderInfo) As Boolean Implements ISaleLooseDiamondDA.InsertSaleLooseDiamondHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_SaleLooseDiamondHeader(SaleLooseDiamondID,SaleDate,CustomerID,StaffID,Remark,TotalAmount,AddOrSub,DiscountAmount,PaidAmount,PromotionDiscount,LocationID,LastModifiedLoginUserName,LastModifiedDate,PurchaseHeaderID,PurchaseAmount,IsOtherCash,OtherCashAmount,IsDelete,IsUpload,AllTaxAmt,SRTaxPer,SRTaxAmt,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus)"
                strCommandText += " VALUES(@SaleLooseDiamondID,@SaleDate,@CustomerID,@StaffID,@Remark,@TotalAmount,@AddOrSub,@DiscountAmount,@PaidAmount,@PromotionDiscount,@LocationID,@LastModifiedLoginUserName,getDate(),@PurchaseHeaderID,@PurchaseAmount,@IsOtherCash,@OtherCashAmount,0,0,@AllTaxAmt,@SRTaxPer,@SRTaxAmt,@MemberID,@MemberName,@MemberCode,@RedeemID,@TopupPoint,@TopupValue,@RedeemPoint,@RedeemValue,@IsRedeemInvoice,@MemberDis,@MemberDiscountAmt,@InvoiceStatus)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, Obj.SaleLooseDiamondID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@SaleDate", DbType.DateTime, Obj.SaleDate)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int32, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@PromotionDiscount", DbType.Int32, Obj.PromotionDiscount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int64, Obj.PurchaseAmount)
                DB.AddInParameter(DBComm, "@IsOtherCash", DbType.Boolean, Obj.IsOtherCash)
                DB.AddInParameter(DBComm, "@OtherCashAmount", DbType.Int32, Obj.OtherCashAmount)
                DB.AddInParameter(DBComm, "@IsDelete", DbType.Boolean, 0)
                DB.AddInParameter(DBComm, "@IsUpload", DbType.Boolean, 0)
                DB.AddInParameter(DBComm, "@AllTaxAmt", DbType.Int32, Obj.AllTaxAmt)
                DB.AddInParameter(DBComm, "@SRTaxPer", DbType.Int32, Obj.SRTaxPer)
                DB.AddInParameter(DBComm, "@SRTaxAmt", DbType.Int32, Obj.SRTaxAmt)
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

        Public Function UpdateSaleLooseDiamondHeader(ByVal Obj As SaleLooseDiamondHeaderInfo) As Boolean Implements ISaleLooseDiamondDA.UpdateSaleLooseDiamondHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleLooseDiamondHeader set  SaleDate=@SaleDate, StaffID=@StaffID, CustomerID=@CustomerID, Remark=@Remark, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub, DiscountAmount=@DiscountAmount, PromotionDiscount=@PromotionDiscount, PaidAmount=@PaidAmount, LocationID=@LocationID, LastModifiedLoginUserName=@LastModifiedLoginUserName, LastModifiedDate=@LastModifiedDate, PurchaseHeaderID=@PurchaseHeaderID, PurchaseAmount=@PurchaseAmount,IsOtherCash=@IsOtherCash,OtherCashAmount=@OtherCashAmount,AllTaxAmt=@AllTaxAmt,SRTaxPer=@SRTaxPer,MemberID=@MemberID,MemberName=@MemberName,MemberCode=@MemberCode,RedeemID=@RedeemID,TopupPoint=@TopupPoint,TopupValue=@TopupValue,RedeemPoint=@RedeemPoint,RedeemValue=@RedeemValue,IsRedeemInvoice=@IsRedeemInvoice,MemberDis=@MemberDis,MemberDiscountAmt=@MemberDiscountAmt,InvoiceStatus=@InvoiceStatus "
                strCommandText += " where SaleLooseDiamondID= @SaleLooseDiamondID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, Obj.SaleLooseDiamondID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@SaleDate", DbType.DateTime, Obj.SaleDate)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int32, Obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, Obj.PaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@PromotionDiscount", DbType.Int32, Obj.PromotionDiscount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@PurchaseHeaderID", DbType.String, Obj.PurchaseHeaderID)
                DB.AddInParameter(DBComm, "@PurchaseAmount", DbType.Int64, Obj.PurchaseAmount)
                DB.AddInParameter(DBComm, "@IsOtherCash", DbType.Boolean, Obj.IsOtherCash)
                DB.AddInParameter(DBComm, "@OtherCashAmount", DbType.Int32, Obj.OtherCashAmount)
                DB.AddInParameter(DBComm, "@IsDelete", DbType.Boolean, 0)
                DB.AddInParameter(DBComm, "@IsUpload", DbType.Boolean, 0)
                DB.AddInParameter(DBComm, "@AllTaxAmt", DbType.Int32, Obj.AllTaxAmt)
                DB.AddInParameter(DBComm, "@SRTaxPer", DbType.Int32, Obj.SRTaxPer)
                DB.AddInParameter(DBComm, "@SRTaxAmt", DbType.Int32, Obj.SRTaxAmt)
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

        Public Function DeleteSaleLooseDiamondHeader(ByVal SaleLooseDiamondID As String) As Boolean Implements ISaleLooseDiamondDA.DeleteSaleLooseDiamondHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_SaleLooseDiamondHeader SET IsDelete=1  WHERE  SaleLooseDiamondID= @SaleLooseDiamondID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleLooseDiamondID)
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

        Public Function InsertSaleLooseDiamondDetail(ByVal Obj As CommonInfo.SaleLooseDiamondDetailInfo) As Boolean Implements ISaleLooseDiamondDA.InsertSaleLooseDiamondDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_SaleLooseDiamondDetail ( SaleLooseDiamondDetailID, SaleLooseDiamondID, ForSaleID, ItemCode, GemsCategoryID, Shape, Color, Clarity, GemsName, SalesRate, Qty, ItemTK,ItemTG, IsFixPrice, FixPrice, GemsPrice, TotalAmount, AddOrSub,DesignCharges,DesignChargesRate,WhiteCharges,PlatingCharges,MountingCharges,IsSaleReturn,SellingRate,SellingAmt,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceCarat,OriginalPriceCarat,YOrCOrG,GemsTW,IsReturn)"
                strCommandText += " VALUES(@SaleLooseDiamondDetailID, @SaleLooseDiamondID, @ForSaleID, @ItemCode, @GemsCategoryID, @Shape, @Color, @Clarity, @GemsName, @SalesRate, @Qty,@ItemTK, @ItemTG, @IsFixPrice, @FixPrice, @GemsPrice, @TotalAmount, @AddOrSub,@DesignCharges,@DesignChargesRate,@WhiteCharges,@PlatingCharges,@MountingCharges,@IsSaleReturn,@SellingRate,@SellingAmt,@IsOriginalFixedPrice,@OriginalFixedPrice,@IsOriginalPriceCarat,@OriginalPriceCarat,@YOrCOrG,@GemsTW,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondDetailID", DbType.String, Obj.SaleLooseDiamondDetailID)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, Obj.SaleLooseDiamondID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@Shape", DbType.String, Obj.Shape)
                DB.AddInParameter(DBComm, "@Color", DbType.String, Obj.Color)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, Obj.Clarity)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int32, Obj.SalesRate)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int64, Obj.QTY)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, Obj.FixPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int64, Obj.DesignChargesRate)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, Obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, Obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, Obj.MountingCharges)
                DB.AddInParameter(DBComm, "@IsSaleReturn", DbType.Boolean, Obj.IsSaleReturn)
                DB.AddInParameter(DBComm, "@SellingRate", DbType.Int64, Obj.SellingRate)
                DB.AddInParameter(DBComm, "@SellingAmt", DbType.Int64, Obj.SellingAmt)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, Obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, Obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceCarat", DbType.Boolean, Obj.IsOriginalPriceCarat)
                DB.AddInParameter(DBComm, "@OriginalPriceCarat", DbType.Int64, Obj.OriginalPriceCarat)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemsTW)
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

        Public Function UpdateSaleLooseDiamondDetail(ByVal Obj As CommonInfo.SaleLooseDiamondDetailInfo) As Boolean Implements ISaleLooseDiamondDA.UpdateSaleLooseDiamondDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SaleLooseDiamondDetail set SaleLooseDiamondID=@SaleLooseDiamondID, ForSaleID=@ForSaleID, ItemCode=@ItemCode, GemsCategoryID=@GemsCategoryID, Shape=@Shape, Color=@Color, Clarity=@Clarity,GemsName=@GemsName, SalesRate=@SalesRate, QTY=@QTY, ItemTK=@ItemTK, ItemTG=@ItemTG, IsFixPrice=@IsFixPrice, FixPrice=@FixPrice, GemsPrice=@GemsPrice, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub,DesignCharges=@DesignCharges,DesignChargesRate=@DesignChargesRate,WhiteCharges=@WhiteCharges,PlatingCharges=@PlatingCharges,MountingCharges=@MountingCharges,IsSaleReturn=@IsSaleReturn,SellingRate=@SellingRate,SellingAmt=@SellingAmt,IsOriginalFixedPrice=@IsOriginalFixedPrice,OriginalFixedPrice=@OriginalFixedPrice,IsOriginalPriceCarat=@IsOriginalPriceCarat,OriginalPriceCarat=@OriginalPriceCarat "
                strCommandText += " where SaleLooseDiamondDetailID= @SaleLooseDiamondDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondDetailID", DbType.String, Obj.SaleLooseDiamondDetailID)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, Obj.SaleLooseDiamondID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@Shape", DbType.String, Obj.Shape)
                DB.AddInParameter(DBComm, "@Color", DbType.String, Obj.Color)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, Obj.Clarity)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int32, Obj.SalesRate)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int64, Obj.QTY)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, Obj.FixPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, Obj.GemsPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int64, Obj.DesignChargesRate)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, Obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, Obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, Obj.MountingCharges)
                DB.AddInParameter(DBComm, "@IsSaleReturn", DbType.Boolean, Obj.IsSaleReturn)
                DB.AddInParameter(DBComm, "@SellingRate", DbType.Int64, Obj.SellingRate)
                DB.AddInParameter(DBComm, "@SellingAmt", DbType.Int64, Obj.SellingAmt)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, Obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, Obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceCarat", DbType.Boolean, Obj.IsOriginalPriceCarat)
                DB.AddInParameter(DBComm, "@OriginalPriceCarat", DbType.Int64, Obj.OriginalPriceCarat)

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
        Public Function DeleteSaleLooseDiamondDetail(ByVal SaleLooseDiamondDetailID As String) As Boolean Implements ISaleLooseDiamondDA.DeleteSaleLooseDiamondDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SaleLooseDiamondDetail WHERE  SaleLooseDiamondDetailID= @SaleLooseDiamondDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondDetailID", DbType.String, SaleLooseDiamondDetailID)
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


        Public Function GetSaleLooseDiamondDetailByID(ByVal SaleLooseDiamondID As String) As System.Data.DataTable Implements ISaleLooseDiamondDA.GetSaleLooseDiamondDetailByID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select D.SaleLooseDiamondDetailID, D.SaleLooseDiamondID, D.ForSaleID, D.ItemCode, D.GemsCategoryID, I.GemsCategory, D.Color,D.Shape,D.Clarity, D.SalesRate, D.QTY, D.ItemTK, D.ItemTG, D.IsFixPrice, D.FixPrice, D.GemsPrice, D.TotalAmount, D.AddOrSub, (D.TotalAmount+D.AddOrSub) AS NetAmount,D.DesignCharges,D.DesignChargesRate, "
                strCommandText += " D.WhiteCharges,D.PlatingCharges,D.MountingCharges,D.IsOriginalFixedPrice,D.OriginalFixedPrice,D.IsOriginalPriceCarat,D.OriginalPriceCarat,D.GemsTW,D.YOrCOrG,D.IsSaleReturn,D.SellingAmt,D.SellingRate,D.GemsName,D.OriginalCode "
                strCommandText += " from tbl_SaleLooseDiamondDetail D "
                strCommandText += " LEFT JOIN tbl_GemsCategory I ON I.GemsCategoryID=D.GemsCategoryID "
                strCommandText += " Where SaleLooseDiamondID ='" & SaleLooseDiamondID & "' Order By SaleLooseDiamondDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetAllSaleLooseDiamond() As System.Data.DataTable Implements ISaleLooseDiamondDA.GetAllSaleLooseDiamond
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select SaleLooseDiamondID As [VoucherNo], convert(varchar(10),SaleDate,105) as SaleDate, H.CustomerID As [@CustomerID], CustomerName As [Customer_], H.StaffID AS [@StaffID], Staff AS [Staff_], TotalAmount, AddOrSub, DiscountAmount, PaidAmount, H.Remark AS [Remark_], H.PromotionDiscount,SaleDate as [@SDate], PurchaseHeaderID, PurchaseAmount "
                strCommandText += " from tbl_SaleLooseDiamondHeader H left join tbl_Staff S on H.StaffID=S.StaffID left join tbl_Customer C on H.CustomerID=C.CustomerID WHERE H.IsDelete=0 order by [@SDate] desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleLooseDiamondPrint(ByVal SaleLooseDiamondID As String) As System.Data.DataTable Implements ISaleLooseDiamondDA.GetSaleLooseDiamondPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT  D.SaleLooseDiamondDetailID,H.SaleLooseDiamondID, D.ForSaleID, D.ItemCode, D.Color, C.GemsCategory, D.Shape,D.Clarity, " & _
                                "  D.GemsCategoryID,D.GemsName, D.FixPrice, D.IsFixPrice,D.IsOriginalFixedPrice,D.OriginalFixedPrice,D.IsOriginalPriceCarat,D.OriginalPriceCarat, F.Photo, D.SalesRate, " & _
                                " D.TotalAmount AS ItemAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, D.ItemTK, D.ItemTG, " & _
                                " D.QTY, D.GemsPrice,D.YOrCOrG,H.IsOtherCash,H.OtherCashAmount,H.AllTaxAmt,H.SRTaxPer,H.SRTaxAmt, " & _
                                " H.SaleLooseDiamondID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,Cus.CustomerTel as PhoneNo,H.StaffID, S.Staff, H.Remark, " & _
                                " H.TotalAmount,H.AddOrSub,(H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, " & _
                                " H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt) )-(H.PaidAmount+H.PurchaseAmount)) As BalanceAmount,H.PurchaseHeaderID,H.PurchaseAmount,D.DesignChargesRate,D.DesignCharges,H.MemberID,H.MemberName,H.MemberCode,H.RedeemID,H.TopupPoint,H.TopupValue,H.RedeemPoint,H.RedeemValue,H.IsRedeemInvoice,H.MemberDis,H.MemberDiscountAmt,H.InvoiceStatus,H.TransactionID,0 as PointBalance " & _
                                " FROM tbl_SaleLooseDiamondDetail D " & _
                                " LEFT JOIN  tbl_SaleLooseDiamondHeader H  ON H.SaleLooseDiamondID=D.SaleLooseDiamondID " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID " & _
                                " LEFT JOIN tbl_Customer Cus On H.CustomerID=Cus.CustomerID " & _
                                " LEFT JOIN tbl_Staff S On S.StaffID=H.StaffID " & _
                                " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=D.GemsCategoryID" & _
                                " WHERE H.SaleLooseDiamondID=@SaleLooseDiamondID And H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleLooseDiamondID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleLooseDiamondHeaderByID(ByVal SaleLooseDiamondID As String) As CommonInfo.SaleLooseDiamondHeaderInfo Implements ISaleLooseDiamondDA.GetSaleLooseDiamondHeaderByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSalesVolume As New SaleLooseDiamondHeaderInfo
            Try
                strCommandText = " SELECT  SaleLooseDiamondID, SaleDate, CustomerID, StaffID , TotalAmount, AddOrSub, DiscountAmount, PaidAmount, Remark, PromotionDiscount, (TotalAmount*PromotionDiscount)/100 As PromotionAmount, PurchaseHeaderID, PurchaseAmount,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus,TransactionID  "
                strCommandText += "  FROM tbl_SaleLooseDiamondHeader WHERE SaleLooseDiamondID= @SaleLooseDiamondID And IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleLooseDiamondID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSalesVolume
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
                        .MemberID = drResult("MemberID")
                        .MemberCode = drResult("MemberCode")
                        .MemberName = drResult("MemberName")
                        .RedeemID = drResult("RedeemID")
                        .RedeemPoint = drResult("RedeemPoint")
                        .RedeemValue = drResult("RedeemValue")
                        .TopupPoint = drResult("TopupPoint")
                        .TopupValue = drResult("TopupValue")
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
            Return objSalesVolume
        End Function

        Public Function GetSalesVolumeDataByHeaderIDAndItemCode(SaleLooseDiamondID As String, Optional ItemCode As String = "") As DataTable Implements ISaleLooseDiamondDA.GetSalesVolumeDataByHeaderIDAndItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select S.SalesVolumeDetailID as [@SalesVolumeDetailID],S.ForSaleID as [@ForSaleID],S.ItemCode,(S.TotalAmount +S.AddorSub) as TotalAmount," & _
                        " CAST(S.ItemTK AS INT) AS TotalK," & _
                        " CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS TotalP," & _
                        " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY," & _
                        " CAST((S.ItemTK - S.GemsTK) AS INT) AS GoldK," & _
                        " CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT) AS GoldP," & _
                        " CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GoldY," & _
                        " CAST(S.GemsTK AS INT) AS GemsK," & _
                        " CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT) AS GemsP," & _
                        " CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY," & _
                        " F.ItemCategoryID,F.ItemNameID,F.GoldQualityID,F.Length,F.IsFixPrice,F.FixPrice,S.ItemTK,S.ItemTG,(S.ItemTK - S.GemsTK) as GoldTK,(S.ItemTG - S.GemsTG) as GoldTG,S.GemsTK,S.GemsTG,S.SalesRate from tbl_SalesVolumeDetail S" & _
                        " Left Join tbl_ForSale F on S.ForSaleID = F.ForSaleID " & _
                        " where S.SaleLooseDiamondID = @SaleLooseDiamondID and F.IsExit = '1' " & ItemCode

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleLooseDiamondID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ISaleLooseDiamondDA.GetSalesVolumeDateByForSaleID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select SalesVolumeDetailID, SaleLooseDiamondID, ForSaleID, ItemCode, ItemCategoryID, GoldQualityID, ItemNameID, Length, SalesRate, QTY, ItemTK, ItemTG, WasteTK, WasteTG, IsFixPrice, FixPrice, GoldPrice, TotalAmount, AddOrSub "
                strCommandText += " from tbl_SalesVolumeDetail Where ForSaleID ='" & ForSaleID & "' Order By SalesVolumeDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetSaleLooseDiamondByID(SaleLooseDiamondID As String) As SaleLooseDiamondHeaderInfo Implements ISaleLooseDiamondDA.GetSaleLooseDiamondByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SaleLooseDiamondHeaderInfo
            Try
                strCommandText = " Select D.SaleLooseDiamondID As [@SaleLooseDiamondID] ,D.SaleDate,D.StaffID As [@StaffID],S.Staff,D.CustomerID As [@CustomerID],C.CustomerName,D.TotalAmount,(D.TotalAmount*D.PromotionDiscount)/100 as PromotionAmount," &
                                  "  D.AddOrSub,D.PaidAmount,D.Remark,D.DiscountAmount,D.PromotionDiscount,D.PurchaseHeaderID,D.PurchaseAmount From tbl_SaleLooseDiamondHeader D Inner Join tbl_Staff S on S.StaffID=D.StaffID" &
                                   " Inner Join tbl_Customer C on C.CustomerID=D.CustomerID  Where D.SaleLooseDiamondID=@SaleLooseDiamondID And D.IsDelete=0 order By SaleLooseDiamondID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleLooseDiamondID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .SaleLooseDiamondID = drResult("@SaleLooseDiamondID")
                        .SaleDate = drResult("SaleDate")
                        .StaffID = drResult("@StaffID")
                        .CustomerID = drResult("@CustomerID")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .PaidAmount = drResult("PaidAmount")
                        .Remark = drResult("Remark")
                        .DiscountAmount = drResult("DiscountAmount")
                        .PromotionDiscount = drResult("PromotionDiscount")
                        .PromotionAmount = drResult("PromotionAmount")
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

        Public Function GetSalesInvoiceLooseDiamondReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondDA.GetSalesInvoiceLooseDiamondReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT D.SaleLooseDiamondDetailID, D.ForSaleID, D.ItemCode,F.IsLooseDiamond,F.SDGemsCategoryID,F.Shape,F.Clarity,F.Color,F.SDGemsName as GemsName,F.SDYOrCOrG,F.OriginalPriceCarat,F.IsOriginalPriceCarat,F.SDGemsTW,F.TotalCost,D.YOrCOrG," & _
                                 "  D.IsFixPrice,C.GemsCategory, D.FixPrice, D.QTY,F.Photo, D.SalesRate, D.ItemTK, CAST((D.ItemTG) AS DECIMAL(18,3)) as ItemTG, " & _
                                 " CAST(D.ItemTK AS INT) AS ItemK, " & _
                                 " CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                 " D.GemsPrice, D.TotalAmount AS  ItemTotalAmount, D.AddOrSub AS ItemAddOrSub,(D.TotalAmount+D.AddOrSub) As ItemAmount," & _
                                 " H.SaleLooseDiamondID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, H.TotalAmount,H.AddOrSub,  " & _
                                 " H.PromotionDiscount, ((H.TotalAmount+H.AddOrSub)-(H.DiscountAmount+(H.TotalAmount*PromotionDiscount)/100)+H.RedeemValue+H.MemberDiscountAmt) AS NetAmount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, ((H.DiscountAmount+(H.TotalAmount*PromotionDiscount)/100)-H.AddOrSub+H.RedeemValue+H.MemberDiscountAmt) AS TotalDiscountAmount, " & _
                                 " H.DiscountAmount,H.RedeemValue,H.RedeemPoint,H.MemberID,H.MemberName,H.MemberCode,H.RedeemID,H.TopupPoint,H.TopupValue,H.IsRedeemInvoice,H.MemberDis,H.MemberDiscountAmt,H.TransactionID,H.InvoiceStatus, CASE WHEN H.PaidAmount<0 THEN H.PaidAmount ELSE (H.PaidAmount+H.PurchaseAmount+H.OtherCashAmount) END AS PaidAmount, (((H.TotalAmount+H.AddOrSub+H.OtherCashAmount)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt))-H.PaidAmount) As BalanceAmount, H.SaleDate AS [@SDate], CASE H.PurchaseHeaderID WHEN '' THEN 0 ELSE 1 END As IsChange, H.PurchaseHeaderID, H.PurchaseAmount, 0 AS TotalNetAmount " & _
                                 " FROM tbl_SaleLooseDiamondDetail D " & _
                                 " LEFT JOIN  tbl_SaleLooseDiamondHeader H  ON H.SaleLooseDiamondID=D.SaleLooseDiamondID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 "  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                                 " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=F.SDGemsCategoryID " & _
                                 " WHERE H.SaleDate BETWEEN @FromDate And @ToDate " & criStr & " And H.IsDelete=0 Order by [@SDate] DESC, H.SaleLooseDiamondID,D.ItemCode ASC  "



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

        Public Function GetSaleLooseDiamondReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondDA.GetSaleLooseDiamondReportForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT Distinct(H.SaleLooseDiamondID), H.SaleDate, H.TotalAmount,H.AddOrSub, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount) ) As NetAmount, " & _
                                 "  H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, H.DiscountAmount, CASE WHEN H.PaidAmount<0 THEN H.PaidAmount ELSE (H.PaidAmount+H.PurchaseAmount+OtherCashAmount) END AS PaidAmount, " & _
                                 " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount) )-H.PaidAmount-H.OtherCashAmount) As BalanceAmount" & _
                                 " FROM tbl_SaleLooseDiamondDetail D " & _
                                 " LEFT JOIN  tbl_SaleLooseDiamondHeader H  ON H.SaleLooseDiamondID=D.SaleLooseDiamondID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 "  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                                 " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=F.SDGemsCategoryID " & _
                                 " WHERE H.IsDelete=0 AND H.SaleDate BETWEEN @FromDate and @ToDate " & criStr
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

        Public Function GetProfitForSaleDiamondItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondDA.GetProfitForSaleDiamondItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT F.ExitDate, F.ForSaleID, F.ItemCode,F.Color,F.Shape,F.Clarity,F.SDGemsName as ItemName,F.SDGemsCategoryID as ItemCategoryID,C.GemsCategory , Sum(D.ItemTK) AS ItemTK, Sum(D.ItemTG) AS ItemTG,F.SDYOrCOrG as YOrCOrG, " & _
                                 " F.IsFixPrice, F.FixPrice, case F.IsOriginalFixedPrice when 1 Then F.OriginalFixedPrice Else (F.PriceCode+F.DesignCharges+F.WhiteCharges+F.PlatingCharges+F.MountingCharges) End as PriceCode, F.GivenDate,Sum(D.TotalAmount+D.AddOrSub) As NetAmount,D.TotalAmount as TotalAmount,D.AddorSub as ADDOrSub, Sum(D.QTY) AS QTY " & _
                                 " FROM tbl_SaleLooseDiamondDetail D " & _
                                 " LEFT JOIN  tbl_SaleLooseDiamondHeader H  ON H.SaleLooseDiamondID=D.SaleLooseDiamondID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=F.SDGemsCategoryID " & _
                                 " WHERE F.ExitDate BETWEEN @FromDate and @ToDate " & " AND F.IsLooseDiamond=1 AND F.IsExit=1 " & criStr & " GROUP BY F.ForSaleID, F.ItemCode, F.SDGemsCategoryID, F.Color,F.Shape,F.Clarity, C.GemsCategory,F.SDGemsName, F.ItemTK, F.ItemTG,F.IsFixPrice, F.FixPrice, F.IsOriginalFixedPrice, F.OriginalFixedPrice, F.ExitDate,F.GivenDate,D.TotalAmount,D.AddOrSub,F.PriceCode,F.DesignCharges,F.PlatingCharges,F.MountingCharges,F.WhiteCharges,F.SDYOrCOrG "

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

        Public Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISaleLooseDiamondDA.GetAllSalesVolumeVoucherPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT  D.SalesVolumeDetailID, D.ForSaleID, D.ItemCode, D.ItemNameID, I.ItemName, F.GoldQualityID, " & _
                                " GQ.GoldQuality, D.ItemCategoryID, C.ItemCategory, F.Width, D.Length, D.FixPrice, D.IsFixPrice, F.Photo, D.SalesRate, " & _
                                " D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, D.ItemTK, CAST((D.ItemTG) AS DECIMAL(18,3)) as ItemTG, " & _
                                " D.WasteTK, CAST((D.WasteTG) AS DECIMAL(18,3)) As WasteTG, D.QTY, D.GoldPrice,  (D.ItemTK+D.WasteTK) AS TotalTK, (CAST((D.ItemTG) AS DECIMAL(18,3)) + CAST((D.WasteTG) AS DECIMAL(18,3))) As TotalTG, " & _
                                " CAST(D.ItemTK AS INT) AS ItemK," & _
                                " CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS ItemY, " & _
                                " CAST(D.WasteTK AS INT) AS WasteK,  CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS WasteY, " & _
                                " CAST(D.ItemTK+D.WasteTK AS INT) AS TotalK,  CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT) AS TotalP,  " & _
                                " CAST((((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16)-CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY," & _
                                " H.SaleLooseDiamondID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, " & _
                                " H.TotalAmount,H.AddOrSub,(H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, " & _
                                " H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount) )-H.PaidAmount) As BalanceAmount, H.SaleDate AS [@SDate] " & _
                                " FROM tbl_SalesVolumeDetail D " & _
                                " LEFT JOIN  tbl_SalesVolume H  ON H.SaleLooseDiamondID=D.SaleLooseDiamondID " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID " & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=D.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                " ON GQ.GoldQualityID=F.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=D.ItemCategoryID" & _
                                " WHERE H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr & " ORDER BY [@SDate] DESC, H.SaleLooseDiamondID ASC"

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

        Public Function GetSaleLooseDiamondDetailByDetailID(ByVal SaleLooseDiamondDetailID As String) As System.Data.DataTable Implements ISaleLooseDiamondDA.GetSaleLooseDiamondDetailByDetailID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select *  from tbl_SaleLooseDiamondDetail D Where SaleLooseDiamondDetailID ='" & SaleLooseDiamondDetailID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetSaleLooseDiamondDataByCustomerIDAndItemCode(CustomerID As String, Optional argForSaleIDStr As String = "") As DataTable Implements ISaleLooseDiamondDA.GetSaleLooseDiamondDataByCustomerIDAndItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String = ""

            If CustomerID <> "" Then
                strWhere = argForSaleIDStr + " AND CustomerID='" & CustomerID & "' "
            Else
                strWhere = argForSaleIDStr
            End If
            Try
                strCommandText = " select S.ItemCode, H.SaleLooseDiamondID AS VoucherNo, H.SaleLooseDiamondID AS [@SaleLooseDiamondID], CONVERT(VARCHAR(10),H.SaleDate,105) as Date, I.GemsCategory As [GemsCategory_], CONVERT(VARCHAR,CAST(S.ItemTG AS DECIMAL(18,3))) AS ItemTG, CASE WHEN (select count(SaleLooseDiamondDetailID) from tbl_SaleLooseDiamondDetail where SaleLooseDiamondID=H.SaleLooseDiamondID AND IsReturn=0)=1 THEN CONVERT(varchar,((H.TotalAmount +H.AddOrSub)-H.DiscountAmount)) ELSE CONVERT(varchar,(S.TotalAmount +S.AddorSub)) END as TotalAmount, " & _
                                 " S.Color,S.Shape,S.Clarity,S.GemsName, Convert(VARCHAR,S.SalesRate) As SaleRate, CASE WHEN (select count(SaleLooseDiamondDetailID) from tbl_SaleLooseDiamondDetail where SaleLooseDiamondID=H.SaleLooseDiamondID AND IsReturn=0)=1 THEN CONVERT(varchar,((H.TotalAmount +H.AddOrSub)-H.DiscountAmount)) ELSE CONVERT(varchar,(S.TotalAmount +S.AddorSub)) END as [@TotalAmount], S.SalesRate As [@SalesRate],  H.SaleDate AS [@SDate], H.CustomerID AS [@CustomerID], S.SaleLooseDiamondDetailID as [@SaleLooseDiamondDetailID],S.ForSaleID as [@ForSaleID],(S.TotalAmount +S.AddorSub) as [@ItemNetAmount], " & _
                                 " S.ItemTK [@ItemTK], S.ItemTG AS [@ItemTG],S.YOrCOrG," & _
                                 " S.GemsCategoryID as [@GemsCategoryID],S.IsFixPrice as [@IsFixPrice], S.FixPrice AS [@FixPrice], F.Photo AS [@Photo], F.OriginalCode As [@OriginalCode], F.IsLooseDiamond AS [@IsLooseDiamond], S.GemsPrice AS [@GemsPrice],S.AddOrSub As [@AddOrSub], (F.DesignCharges+ F.PlatingCharges+F.MountingCharges+F.WhiteCharges) AS [@TotalCharges],S.Qty " & _
                                 " FROM tbl_SaleLooseDiamondDetail S " & _
                                 " LEFT JOIN tbl_SaleLooseDiamondHeader H ON H.SaleLooseDiamondID=S.SaleLooseDiamondID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=S.ForSaleID " & _
                                 " Left Join tbl_GemsCategory I On I.GemsCategoryID = S.GemsCategoryID" & _
                                 " where S.IsReturn=0 AND H.IsDelete=0 and F.IsDelete=0  " & strWhere & " ORDER BY [@SDate] DESC, VoucherNo DESC"



                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements ISaleLooseDiamondDA.UpdateRedeemID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_SaleLooseDiamondHeader SET RedeemID=@RedeemID WHERE  SaleLooseDiamondID= @SaleLooseDiamondID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleInvoiceID)
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

        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleInvoiceID As String) As Boolean Implements ISaleLooseDiamondDA.UpdateTransactionID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_SaleLooseDiamondHeader SET TransactionID=@TransactionID WHERE  SaleLooseDiamondID= @SaleLooseDiamondID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SaleLooseDiamondID", DbType.String, SaleInvoiceID)
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

