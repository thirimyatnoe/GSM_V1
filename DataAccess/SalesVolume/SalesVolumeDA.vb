Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace SalesVolume
    Public Class SalesVolumeDA
        Implements ISalesVolumeDA

#Region "Private SalesVolume"

        Private DB As Database
        Private Shared ReadOnly _instance As ISalesVolumeDA = New SalesVolumeDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesVolumeDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function InsertSalesVolumeHeader(ByVal Obj As CommonInfo.SalesVolumeHeaderInfo) As Boolean Implements ISalesVolumeDA.InsertSalesVolumeHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_SalesVolume(SalesVolumeID, SaleDate, StaffID, CustomerID, Remark, TotalAmount, AddOrSub, DiscountAmount, PromotionDiscount, PaidAmount, LocationID, LastModifiedLoginUserName, LastModifiedDate, PurchaseHeaderID, PurchaseAmount,IsSync,IsDelete,IsSolidVolume,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus)"
                strCommandText += " VALUES(@SalesVolumeID, @SaleDate, @StaffID, @CustomerID, @Remark, @TotalAmount, @AddOrSub, @DiscountAmount, @PromotionDiscount, @PaidAmount, @LocationID, @LastModifiedLoginUserName, @LastModifiedDate, @PurchaseHeaderID, @PurchaseAmount,0,0,@IsSolidVolume,@MemberID,@MemberName,@MemberCode,@RedeemID,@TopupPoint,@TopupValue,@RedeemPoint,@RedeemValue,@IsRedeemInvoice,@MemberDis,@MemberDiscountAmt,@InvoiceStatus)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, Obj.SalesVolumeID)
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
                DB.AddInParameter(DBComm, "@IsSolidVolume", DbType.Boolean, Obj.IsSolidVolume)
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

        Public Function UpdateSalesVolumeHeader(ByVal Obj As SalesVolumeHeaderInfo) As Boolean Implements ISalesVolumeDA.UpdateSalesVolumeHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SalesVolume set  SaleDate=@SaleDate, StaffID=@StaffID, CustomerID=@CustomerID, Remark=@Remark, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub, DiscountAmount=@DiscountAmount, PromotionDiscount=@PromotionDiscount, PaidAmount=@PaidAmount, LocationID=@LocationID, LastModifiedLoginUserName=@LastModifiedLoginUserName, LastModifiedDate=@LastModifiedDate, PurchaseHeaderID=@PurchaseHeaderID, PurchaseAmount=@PurchaseAmount,IsSync=0,MemberID=@MemberID,MemberName=@MemberName,MemberCode=@MemberCode,RedeemID=@RedeemID,TopupPoint=@TopupPoint,TopupValue=@TopupValue,RedeemPoint=@RedeemPoint,RedeemValue=@RedeemValue,IsRedeemInvoice=@IsRedeemInvoice,MemberDis=@MemberDis,MemberDiscountAmt=@MemberDiscountAmt,InvoiceStatus=@InvoiceStatus "
                strCommandText += " where SalesVolumeID= @SalesVolumeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, Obj.SalesVolumeID)
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

        Public Function DeleteSalesVolumeHeader(ByVal SalesVolumeID As String) As Boolean Implements ISalesVolumeDA.DeleteSalesVolumeHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_SalesVolume SET IsDelete=1  WHERE  SalesVolumeID= @SalesVolumeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SalesVolumeID)
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

        Public Function InsertSalesVolumeDetail(ByVal Obj As CommonInfo.SalesVolumeDetailInfo) As Boolean Implements ISalesVolumeDA.InsertSalesVolumeDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_SalesVolumeDetail ( SalesVolumeDetailID, SalesVolumeID, ForSaleID, ItemCode, ItemCategoryID, GoldQualityID, ItemNameID, Length, SalesRate, QTY, ItemTK, ItemTG, WasteTK, WasteTG, IsFixPrice, FixPrice, GoldPrice, TotalAmount, AddOrSub,DesignCharges,DesignChargesRate)"
                strCommandText += " VALUES(@SalesVolumeDetailID, @SalesVolumeID, @ForSaleID, @ItemCode, @ItemCategoryID, @GoldQualityID, @ItemNameID, @Length, @SalesRate, @QTY, @ItemTK, @ItemTG, @WasteTK, @WasteTG, @IsFixPrice, @FixPrice, @GoldPrice, @TotalAmount, @AddOrSub,@DesignCharges,@DesignChargesRate)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeDetailID", DbType.String, Obj.SalesVolumeDetailID)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, Obj.SalesVolumeID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, Obj.ItemNameID)
                DB.AddInParameter(DBComm, "@Length", DbType.String, Obj.Length)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, Obj.SalesRate)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int64, Obj.QTY)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, Obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, Obj.WasteTG)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, Obj.FixPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int64, Obj.DesignChargesRate)

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

        Public Function UpdateSalesVolumeDetail(ByVal Obj As CommonInfo.SalesVolumeDetailInfo) As Boolean Implements ISalesVolumeDA.UpdateSalesVolumeDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_SalesVolumeDetail set SalesVolumeID=@SalesVolumeID, ForSaleID=@ForSaleID, ItemCode=@ItemCode, ItemCategoryID=@ItemCategoryID, GoldQualityID=@GoldQualityID, ItemNameID=@ItemNameID, Length=@Length, SalesRate=@SalesRate, QTY=@QTY, ItemTK=@ItemTK, ItemTG=@ItemTG, WasteTK=@WasteTK, WasteTG=@WasteTG, IsFixPrice=@IsFixPrice, FixPrice=@FixPrice, GoldPrice=@GoldPrice, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub,DesignCharges=@DesignCharges,DesignChargesRate=@DesignChargesRate "
                strCommandText += " where SalesVolumeDetailID= @SalesVolumeDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeDetailID", DbType.String, Obj.SalesVolumeDetailID)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, Obj.SalesVolumeID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, Obj.ItemNameID)
                DB.AddInParameter(DBComm, "@Length", DbType.String, Obj.Length)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, Obj.SalesRate)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int64, Obj.QTY)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, Obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, Obj.WasteTG)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, Obj.GoldPrice)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, Obj.FixPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, Obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, Obj.AddOrSub)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@DesignChargesRate", DbType.Int64, Obj.DesignChargesRate)

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
        Public Function DeleteSalesVolumeDetail(ByVal SalesVolumeDetailID As String) As Boolean Implements ISalesVolumeDA.DeleteSalesVolumeDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_SalesVolumeDetail WHERE  SalesVolumeDetailID= @SalesVolumeDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeDetailID", DbType.String, SalesVolumeDetailID)
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


        Public Function GetSalesVolumeDetailByID(ByVal SalesVolumeID As String) As System.Data.DataTable Implements ISalesVolumeDA.GetSalesVolumeDetailByID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select D.SalesVolumeDetailID, D.SalesVolumeID, D.ForSaleID, D.ItemCode, D.ItemCategoryID, D.GoldQualityID, D.ItemNameID, G.GoldQuality, I.ItemCategory, N.ItemName, D.Length, D.SalesRate, D.QTY, D.ItemTK, D.ItemTG, D.WasteTK, D.WasteTG, D.IsFixPrice, D.FixPrice, D.GoldPrice, D.TotalAmount, D.AddOrSub, (D.TotalAmount+D.AddOrSub) AS NetAmount,D.DesignCharges,D.DesignChargesRate "
                strCommandText += " from tbl_SalesVolumeDetail D "
                strCommandText += " LEFT JOIN tbl_GoldQuality  G ON G.GoldQualityID=D.GoldQualityID "
                strCommandText += " LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=D.ItemCategoryID "
                strCommandText += " LEFT JOIN tbl_ItemName N ON N.ItemNameID=D.ItemNameID  "
                strCommandText += " Where SalesVolumeID ='" & SalesVolumeID & "' Order By SalesVolumeDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetAllSalesVolume() As System.Data.DataTable Implements ISalesVolumeDA.GetAllSalesVolume
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select SalesVolumeID As [VoucherNo], convert(varchar(10),SaleDate,105) as SaleDate, H.CustomerID As [@CustomerID], CustomerName As [Customer_], H.StaffID AS [@StaffID], Staff AS [Staff_], TotalAmount, AddOrSub, DiscountAmount, PaidAmount, H.Remark AS [Remark_], H.PromotionDiscount,SaleDate as [@SDate], PurchaseHeaderID, PurchaseAmount "
                strCommandText += " from tbl_SalesVolume H left join tbl_Staff S on H.StaffID=S.StaffID left join tbl_Customer C on H.CustomerID=C.CustomerID WHERE H.IsDelete=0 order by [@SDate] desc "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesVolumePrint(ByVal SalesVolumeID As String) As System.Data.DataTable Implements ISalesVolumeDA.GetSalesVolumePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT  D.SalesVolumeDetailID, D.ForSaleID, D.ItemCode, D.ItemNameID, I.ItemName, F.GoldQualityID, " & _
                                " GQ.GoldQuality,GQ.IsGramRate, D.ItemCategoryID, C.ItemCategory, F.Width, D.Length, D.FixPrice, D.IsFixPrice, F.Photo, D.SalesRate, " & _
                                " D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, D.ItemTK, D.ItemTG, " & _
                                " D.WasteTK, D.WasteTG, D.QTY, D.GoldPrice,  (D.ItemTK+D.WasteTK) AS TotalTK, (D.ItemTG+D.WasteTG) AS TotalTG, " & _
                                " CAST(D.ItemTK AS INT) AS ItemK," & _
                                " CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS ItemY, " & _
                                " CAST(D.WasteTK AS INT) AS WasteK,  CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS WasteY, " & _
                                " CAST(D.ItemTK+D.WasteTK AS INT) AS TotalK,  CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT) AS TotalP,  " & _
                                " CAST((((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16)-CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY," & _
                                " H.SalesVolumeID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,Cus.CustomerTel,H.StaffID, S.Staff, H.Remark, " & _
                                " H.TotalAmount,H.AddOrSub,(H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, " & _
                                " H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount) )-(H.PaidAmount+H.PurchaseAmount+H.RedeemValue+H.MemberDiscountAmt)) As BalanceAmount,H.PurchaseHeaderID,H.PurchaseAmount,D.DesignChargesRate,D.DesignCharges,H.MemberID,H.MemberName,H.MemberCode,H.RedeemID,H.TopupPoint,H.TopupValue,H.RedeemPoint,H.RedeemValue,H.IsRedeemInvoice,H.MemberDis,H.MemberDiscountAmt,H.InvoiceStatus,H.TransactionID,0 as PointBalance " & _
                                " FROM tbl_SalesVolumeDetail D " & _
                                " LEFT JOIN  tbl_SalesVolume H  ON H.SalesVolumeID=D.SalesVolumeID " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID " & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=D.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                " ON GQ.GoldQualityID=F.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=D.ItemCategoryID" & _
                                " WHERE H.SalesVolumeID=@SalesVolumeID And H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SalesVolumeID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesVolumeHeaderByID(ByVal SalesVolumeID As String) As CommonInfo.SalesVolumeHeaderInfo Implements ISalesVolumeDA.GetSalesVolumeHeaderByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objSalesVolume As New SalesVolumeHeaderInfo
            Try
                strCommandText = " SELECT  SalesVolumeID, SaleDate, CustomerID, StaffID , TotalAmount, AddOrSub, DiscountAmount, PaidAmount, Remark, PromotionDiscount, (TotalAmount*PromotionDiscount)/100 As PromotionAmount, PurchaseHeaderID, PurchaseAmount,IsSolidVolume,MemberID,MemberName,MemberCode,RedeemID,TopupPoint,TopupValue,RedeemPoint,RedeemValue,IsRedeemInvoice,MemberDis,MemberDiscountAmt,InvoiceStatus,TransactionID "
                strCommandText += "  FROM tbl_SalesVolume WHERE SalesVolumeID= @SalesVolumeID And IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SalesVolumeID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSalesVolume
                        .SalesVolumeID = drResult("SalesVolumeID")
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
                        .IsSolidVolume = drResult("IsSolidVolume")
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

        Public Function GetSalesVolumeDataByHeaderIDAndItemCode(SalesVolumeID As String, Optional ItemCode As String = "") As DataTable Implements ISalesVolumeDA.GetSalesVolumeDataByHeaderIDAndItemCode
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
                        " where S.SalesVolumeID = @SalesVolumeID and F.IsExit = '1' " & ItemCode

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SalesVolumeID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSalesVolumeDateByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesVolumeDA.GetSalesVolumeDateByForSaleID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select SalesVolumeDetailID, SalesVolumeID, ForSaleID, ItemCode, ItemCategoryID, GoldQualityID, ItemNameID, Length, SalesRate, QTY, ItemTK, ItemTG, WasteTK, WasteTG, IsFixPrice, FixPrice, GoldPrice, TotalAmount, AddOrSub "
                strCommandText += " from tbl_SalesVolumeDetail Where ForSaleID ='" & ForSaleID & "' Order By SalesVolumeDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetSaleVolumeByID(SalesVolumeID As String) As SalesVolumeHeaderInfo Implements ISalesVolumeDA.GetSaleVolumeByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesVolumeHeaderInfo
            Try
                strCommandText = " Select V.SalesVolumeID As [@SalesVolumeID] ,V.SaleDate,V.StaffID As [@StaffID],S.Staff,V.CustomerID As [@CustomerID],C.CustomerName,V.TotalAmount,(V.TotalAmount*V.PromotionDiscount)/100 as PromotionAmount," &
                                  "  V.AddOrSub,V.PaidAmount,V.Remark,V.DiscountAmount,V.PromotionDiscount,V.PurchaseHeaderID,V.PurchaseAmount From tbl_SalesVolume V Inner Join tbl_Staff S on S.StaffID=V.StaffID" &
                                   " Inner Join tbl_Customer C on C.CustomerID=V.CustomerID  Where V.SalesVolumeID=@SalesVolumeID And V.IsDelete=0 order By SalesVolumeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SalesVolumeID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .SalesVolumeID = drResult("@SalesVolumeID")
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

        Public Function GetSalesInvoiceVolumeReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeDA.GetSalesInvoiceVolumeReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT D.SalesVolumeDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID, " & _
                                 " GQ.GoldQuality, F.ItemCategoryID, C.ItemCategory, F.Width, D.IsFixPrice, D.FixPrice, D.QTY,F.Photo, D.SalesRate, D.ItemTK, CAST((D.ItemTG) AS DECIMAL(18,3)) as ItemTG, D.WasteTK, CAST((D.WasteTG) AS DECIMAL(18,3)) as WasteTG, D.ItemTK+D.WasteTK As TotalTK, (CAST((D.ItemTG) AS DECIMAL(18,3)) + CAST((D.WasteTG) AS DECIMAL(18,3))) as TotalTG, " & _
                                 " CAST(D.ItemTK AS INT) AS ItemK, " & _
                                 " CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                 " CAST(D.WasteTK AS INT) AS WasteK," & _
                                 " CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP," & _
                                 " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " CAST(D.ItemTK+D.WasteTK AS INT) AS TotalK," & _
                                 " CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT) AS TotalP," & _
                                 " CAST((((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16)-CAST((D.ItemTK+D.WasteTK-CAST(D.ItemTK+D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY," & _
                                 " D.GoldPrice, D.TotalAmount AS  ItemTotalAmount, D.AddOrSub AS ItemAddOrSub,(D.TotalAmount+D.AddOrSub) As ItemNetAmount," & _
                                 " H.SalesVolumeID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, H.TotalAmount,H.AddOrSub,  " & _
                                 " H.PromotionDiscount, ((H.TotalAmount+H.AddOrSub)-(H.DiscountAmount+(H.TotalAmount*PromotionDiscount)/100)) AS NetAmount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, ((H.DiscountAmount+(H.TotalAmount*PromotionDiscount)/100)-H.AddOrSub+H.RedeemValue+H.MemberDiscountAmt) AS TotalDiscountAmount, " & _
                                 " H.DiscountAmount, CASE WHEN H.PaidAmount<0 THEN H.PaidAmount ELSE (H.PaidAmount+H.PurchaseAmount) END AS PaidAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount))-H.PaidAmount-H.RedeemValue-H.MemberDiscountAmt) As BalanceAmount, H.SaleDate AS [@SDate], CASE H.PurchaseHeaderID WHEN '' THEN 0 ELSE 1 END As IsChange, H.PurchaseHeaderID, H.PurchaseAmount, 0 AS TotalNetAmount " & _
                                 " FROM tbl_SalesVolumeDetail D " & _
                                 " LEFT JOIN  tbl_SalesVolume H  ON H.SalesVolumeID=D.SalesVolumeID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                 "  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " WHERE H.SaleDate BETWEEN @FromDate And @ToDate " & criStr & " And H.IsDelete=0 Order by [@SDate] DESC, H.SalesVolumeID,D.ItemCode ASC  "



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

        Public Function GetSalesInvoiceVolumeReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeDA.GetSalesInvoiceVolumeReportForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT Distinct(H.SalesVolumeID), H.SaleDate, H.TotalAmount,H.AddOrSub, ((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount) ) As NetAmount, " & _
                                 "  H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, H.DiscountAmount, CASE WHEN H.PaidAmount<0 THEN H.PaidAmount ELSE (H.PaidAmount+H.PurchaseAmount) END AS PaidAmount, " & _
                                 " (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount+H.RedeemValue+H.MemberDiscountAmt) )-H.PaidAmount) As BalanceAmount" & _
                                 " FROM tbl_SalesVolumeDetail D " & _
                                 " LEFT JOIN  tbl_SalesVolume H  ON H.SalesVolumeID=D.SalesVolumeID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                 "  left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID  LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
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

        Public Function GetProfitForSaleVoulumeItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeDA.GetProfitForSaleVoulumeItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT F.ExitDate, F.ForSaleID, F.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID, GQ.GoldQuality, F.ItemCategoryID, C.ItemCategory, Sum(D.ItemTK) AS ItemTK, Sum(D.ItemTG) AS ItemTG, Sum(D.WasteTK) AS WasteTK, Sum(D.WasteTG) AS WasteTG, " & _
                                 " F.IsFixPrice, F.FixPrice, F.IsOriginalFixedPrice, F.OriginalFixedPrice, F.GivenDate,Sum(D.TotalAmount+D.AddOrSub) As NetAmount, Sum(D.QTY) AS QTY " & _
                                 " FROM tbl_SalesVolumeDetail D " & _
                                 " LEFT JOIN  tbl_SalesVolume H  ON H.SalesVolumeID=D.SalesVolumeID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID" & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID" & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & _
                                 " WHERE F.ExitDate BETWEEN @FromDate and @ToDate " & " AND F.IsVolume=1 AND F.IsExit=1 " & criStr & " GROUP BY F.ForSaleID, F.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID, GQ.GoldQuality, F.ItemCategoryID, C.ItemCategory, F.ItemTK, F.ItemTG,F.IsFixPrice, F.FixPrice, F.IsOriginalFixedPrice, F.OriginalFixedPrice, F.ExitDate, F.QTY,F.GivenDate "

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

        Public Function GetAllSalesVolumeVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesVolumeDA.GetAllSalesVolumeVoucherPrint
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
                                " H.SalesVolumeID, H.SaleDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.Remark, " & _
                                " H.TotalAmount,H.AddOrSub,(H.TotalAmount+H.AddOrSub) As NetAmount, H.PromotionDiscount, (H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, " & _
                                " H.DiscountAmount, H.PaidAmount, (((H.TotalAmount+H.AddOrSub)-(((H.TotalAmount*PromotionDiscount)/100)+H.DiscountAmount) )-H.PaidAmount) As BalanceAmount, H.SaleDate AS [@SDate] " & _
                                " FROM tbl_SalesVolumeDetail D " & _
                                " LEFT JOIN  tbl_SalesVolume H  ON H.SalesVolumeID=D.SalesVolumeID " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID " & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=D.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                " ON GQ.GoldQualityID=F.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=D.ItemCategoryID" & _
                                " WHERE H.SaleDate BETWEEN @FromDate AND @ToDate " & criStr & " ORDER BY [@SDate] DESC, H.SalesVolumeID ASC"

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

        Public Function GetSaleVolumeDetailByDetailID(ByVal SalesVolumeDetailID As String) As System.Data.DataTable Implements ISalesVolumeDA.GetSaleVolumeDetailByDetailID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select *  from tbl_SalesVolumeDetail D Where SalesVolumeDetailID ='" & SalesVolumeDetailID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleVolumeID As String) As Boolean Implements ISalesVolumeDA.UpdateRedeemID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_SalesVolume SET RedeemID=@RedeemID WHERE  SalesVolumeID= @SalesVolumeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SaleVolumeID)
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

        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleVolumeID As String) As Boolean Implements ISalesVolumeDA.UpdateTransactionID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_SalesVolume SET TransactionID=@TransactionID WHERE  SalesVolumeID= @SalesVolumeID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@SalesVolumeID", DbType.String, SaleVolumeID)
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

