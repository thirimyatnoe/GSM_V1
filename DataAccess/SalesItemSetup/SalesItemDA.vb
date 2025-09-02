Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace SalesItem
    Public Class SalesItemDA
        Implements ISalesItemDA

#Region "Private SalesItem"

        Private DB As Database
        Private Shared ReadOnly _instance As ISalesItemDA = New SalesItemDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesItemDA
            Get
                Return _instance
            End Get
        End Property

#End Region



        Public Function GetSalesItemGems(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesItemDA.GetSalesItemGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select SG.ForSaleGemsItemID,SG.ForSaleID, '' AS OrderReturnGemID, '' As OrderInvoiceDetailID, SG.GemsCategoryID, SG.GemsCategoryID AS [@GemsCategoryID], GC.GemsCategory, SG.GemsName,GC.GemTaxPer,(SG.Amount*(GC.GemTaxPer)/100) as GemTax,"
                strCommandText += " CAST(SG.GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((SG.GemsTK-CAST(SG.GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((SG.GemsTK-CAST(SG.GemsTK AS INT))*16)-CAST((SG.GemsTK-CAST(SG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY,"
                strCommandText += " CAST(((((SG.GemsTK-CAST(SG.GemsTK AS INT))*16)-CAST((SG.GemsTK-CAST(SG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((SG.GemsTK-CAST(SG.GemsTK AS INT))*16)-CAST((SG.GemsTK-CAST(SG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " SG.GemsTK, SG.GemsTG, SG.YOrCOrG, SG.GemsTW,SG.SaleByDefinePrice, "
                strCommandText += " SG.Qty, SG.Type, SG.Type AS SaleType, SG.UnitPrice,( (SG.Amount*(GC.GemTaxPer)/100)+SG.Amount) as Amount , GemsRemark, '' As SalesInvoiceGemItemID, '' [@SaleInvoiceDetailID] "
                strCommandText += " from tbl_ForSaleGemsItem SG left join tbl_ForSale S on SG.ForSaleID=S.ForSaleID  "
                strCommandText += " left join tbl_GemsCategory GC on GC.GemsCategoryID=SG.GemsCategoryID where S.IsDelete=0 And SG.ForSaleID='" & ForSaleID & "' "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSalesItemForSaleInvoice(Optional ByVal Itemcode As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSalesItemForSaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If Itemcode <> "" Then
                strWhere = " and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND F.ItemCode NOT IN (" & Itemcode & ")"
            Else
                strWhere = " and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND 1=1"
            End If
            Try
                strCommandText = "SELECT F.IsDiamond AS [$IsDiamond], F.ForSaleID AS [@ForSaleID],F.ItemCode, CONVERT(VARCHAR,CAST(ItemTG AS DECIMAL(18,3))) AS Gram, convert(varchar(10),F.GivenDate,105)as GivenDate, F.ItemCategoryID AS [@ItemCategoryID],I.ItemCategory as [ItemCategory_],N.ItemName as [ItemName_], G.GoldQuality AS [GoldQuality_], F.Length as [Length_], IsNull(F.Width,'') as [Width_],"
                strCommandText += "F.GoldQualityID AS [@GoldQualityID], F.OriginalCode, F.PriceCode, CONVERT(VARCHAR,F.FixPrice) As FixPrice, "
                strCommandText += " CONVERT(VARCHAR,CAST(ItemTK AS INT)) AS ItemK,"
                strCommandText += " CONVERT(VARCHAR,CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT)) AS ItemP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS ItemY,"
                strCommandText += " CONVERT(VARCHAR,CAST(ItemTG AS DECIMAL(18,3))) AS ItemTG,"
                strCommandText += " CONVERT(VARCHAR,CAST(WasteTK AS INT)) AS WasteK,"
                strCommandText += " CONVERT(VARCHAR,CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT)) AS WasteP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY,"
                strCommandText += " CONVERT(VARCHAR,CAST(WasteTG AS DECIMAL(18,3))) AS WasteTG,"
                strCommandText += " CONVERT(VARCHAR,CAST(GoldTK AS INT)) AS GoldK,"
                strCommandText += " CONVERT(VARCHAR,CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT)) AS GoldP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS GoldY,"
                strCommandText += " CONVERT(VARCHAR,CAST(GoldTG AS DECIMAL(18,3))) AS GoldTG,"
                strCommandText += " CONVERT(VARCHAR,CAST(GemsTK AS INT)) AS GemsK,"
                strCommandText += " CONVERT(VARCHAR,CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT)) AS GemsP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) AS GemsY,"
                strCommandText += " CONVERT(VARCHAR,CAST(GemsTG AS DECIMAL(18,3))) AS GemsTG,"
                strCommandText += " F.IsExit AS [@IsExit], "
                strCommandText += " F.IsFixPrice AS [@IsFixPrice],CONVERT(VARCHAR,F.FixPrice) As FixPrice, CONVERT(VARCHAR,DesignCharges) As DesignCharges, CONVERT(VARCHAR,PlatingCharges) AS PlatingCharges, CONVERT(VARCHAR,MountingCharges) AS MountingCharges,  CONVERT(VARCHAR,WhiteCharges) AS WhiteCharges,F.GivenDate as [@GDate],F.Color "
                strCommandText += " FROM tbl_ForSale F INNER JOIN tbl_GoldQuality G ON F.GoldQualityID = G.GoldQualityID "
                strCommandText += " INNER JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID "
                strCommandText += " INNER JOIN tbl_ItemCategory I ON F.ItemCategoryID = I.ItemCategoryID where F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='0' AND IsClosed='0'  AND F.IsDelete=0  " & strWhere & " Order By [@GDate] desc"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal Status As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleForReportByDatePeriod
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                Select Case Status
                    Case "All"
                        strCommandText = "SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                        " IsExit,OriginalGemsPrice, ExitDate, (H.ItemTK-H.GemsTK) AS GoldTK, H.GemsTK, TotalTK,  WasteTK ,CAST((ItemTG-H.GemsTG) as DECIMAL(18,3)) as GoldTG, CAST(H.GemsTG as DECIMAL(18,3)) as GemsTG , CAST(WasteTG as DECIMAL(18,3)) as WasteTG, " & _
                        " CAST(TotalTG as DECIMAL(18,3)) as TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                        " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                        " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                        " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                        " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                        " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                        " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                        " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                        " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                        " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,  " & _
                        " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY," & _
                        " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                        " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,  " & _
                        " HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate], H.IsDiamond, " & _
                        " SP.SupplierName as Supplier,H.SellingRate,H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith,H.PriceCode,H.PurchaseWasteTK,H.PurchaseWasteTG " & _
                        " FROM tbl_ForSale H LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                        " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                        " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                        " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                        " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                        " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                        " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                        " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                        " LEFT JOIN tbl_Supplier SP ON SP.SupplierID=H.SupplierID " & _
                        " WHERE H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & " Order by ItemCode ASC"
                    Case "Exit"
                        strCommandText = "SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                        " IsExit,OriginalGemsPrice, ExitDate, (H.ItemTK-H.GemsTK) AS GoldTK, H.GemsTK, TotalTK,  WasteTK ,CAST((ItemTG-H.GemsTG) as DECIMAL(18,3)) as GoldTG, CAST(H.GemsTG as DECIMAL(18,3)) as GemsTG , CAST(WasteTG as DECIMAL(18,3)) as WasteTG, " & _
                        " CAST(TotalTG as DECIMAL(18,3)) as TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                        " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                        " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                        " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                        " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                        " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                        " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                        " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                        " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                        " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,  " & _
                        " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY," & _
                        " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                        " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,  " & _
                        " HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate]," & _
                        " SP.SupplierName as Supplier,H.SellingRate,H.IsDiamond, H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith,H.PriceCode,H.PurchaseWasteTK,H.PurchaseWasteTG " & _
                        " FROM tbl_ForSale H LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                        " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                        " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                        " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                        " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                        " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                        " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                        " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                        " LEFT JOIN tbl_Supplier SP on SP.SupplierID=H.SupplierID " & _
                        " WHERE H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & " And H.ExitDate BETWEEN @FromDate And @ToDate  " & " And H.isDelete=0 And H.IsClosed=0 Order by ItemCode ASC"
                    Case "Balance"
                        strCommandText = "SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                        " IsExit,OriginalGemsPrice, ExitDate, (H.ItemTK-H.GemsTK) AS GoldTK, H.GemsTK, TotalTK,  WasteTK ,CAST((ItemTG-H.GemsTG) as DECIMAL(18,3)) as GoldTG, CAST(H.GemsTG as DECIMAL(18,3)) as GemsTG , CAST(WasteTG as DECIMAL(18,3)) as WasteTG, " & _
                        " CAST(TotalTG as DECIMAL(18,3)) as TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                        " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                        " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                        " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                        " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                        " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                        " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                        " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                        " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                        " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,  " & _
                        " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY," & _
                        " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                        " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,  " & _
                        " HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate], H.IsDiamond," & _
                        " SP.SupplierName as Supplier,H.SellingRate,H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith,H.PriceCode,H.PurchaseWasteTK,H.PurchaseWasteTG  " & _
                        " FROM tbl_ForSale H LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                        " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                        " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                        " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                        " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                        " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                        " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                        " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                        " LEFT JOIN tbl_Supplier SP on SP.SupplierID=H.SupplierID " & _
                        " WHERE H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & "  And H.isExit<>1 " & _
                        " Union All " & _
                        " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                        " IsExit,OriginalGemsPrice, ExitDate, (H.ItemTK-H.GemsTK) AS GoldTK, H.GemsTK, TotalTK,  WasteTK ,CAST((ItemTG-H.GemsTG) as DECIMAL(18,3)) as GoldTG, CAST(H.GemsTG as DECIMAL(18,3)) as GemsTG , CAST(WasteTG as DECIMAL(18,3)) as WasteTG, " & _
                        " CAST(TotalTG as DECIMAL(18,3)) as TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                        " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                        " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                        " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                        " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                        " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                        " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                        " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                        " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                        " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,  " & _
                        " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY," & _
                        " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                        " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,  " & _
                        " HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate], H.IsDiamond," & _
                        " SP.SupplierName as Supplier,H.SellingRate,H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith,H.PriceCode,H.PurchaseWasteTK,H.PurchaseWasteTG  " & _
                        " FROM tbl_ForSale H LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                        " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                        " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                        " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                        " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                        " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                        " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                        " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                        " LEFT JOIN tbl_Supplier SP on SP.SupplierID=H.SupplierID " & _
                        " WHERE H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & " And H.isExit=1 And ExitDate> @ToDate "
                End Select
                'strCommandText = "SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, H.GoldSmith, " & _
                ' " IsExit,OriginalGemsPrice, ExitDate, (H.ItemTK-H.GemsTK) AS GoldTK, H.GemsTK, TotalTK,  WasteTK ,CAST((ItemTG-H.GemsTG) as DECIMAL(18,3)) as GoldTG, CAST(H.GemsTG as DECIMAL(18,3)) as GemsTG , CAST(WasteTG as DECIMAL(18,3)) as WasteTG, " & _
                ' " CAST(TotalTG as DECIMAL(18,3)) as TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                ' " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                ' " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                ' " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                ' " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                ' " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                ' " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                ' " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                ' " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                ' " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                ' " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,  " & _
                ' " HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate], H.IsDiamond, H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,GG.Name  " & _
                ' " FROM tbl_ForSale H LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                ' " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                ' " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                ' " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                ' " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                ' " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                ' "LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                ' " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                ' " WHERE H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & " Order by ItemCode ASC"

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
        Public Function GetForSaleReportByLocation(ByVal FilterString As String, ByVal LocationID As String) As System.Data.DataTable Implements ISalesItemDA.GetForSaleReportByLocation
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = "SELECT H.ForSaleID, CAST((ItemTG-H.GemsTG) as DECIMAL(18,4)) as GoldTG, CAST(H.GemsTG AS DECIMAL(18,3)) as GemsTG , CAST(ItemTG as DECIMAL(18,4)) as " & _
                '                " ItemTG, CAST(H.WasteTG as DECIMAL(18,3)) as WasteTG , ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, " & _
                '                " ItemCategory, GivenDate,  IsExit,H.OriginalGemsPrice, ExitDate, (ItemTK-H.GemsTK) AS GoldTK,  H.GemsTK, TotalTK,  WasteTK ,TotalTG, ItemTK,  Width, " & _
                '                " H.IsFixPrice, H.FixPrice, DesignCharges,   CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                '                " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,  " & _
                '                " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                '                " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'8.0' AS " & _
                '                " DECIMAL(18,2)) AS GoldY,  CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                '                " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                '                " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP,  CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)" & _
                '                " -CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS GemsY,  CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, " & _
                '                " CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP, CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)" & _
                '                " -CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY, PlatingCharges, MountingCharges, " & _
                '                " WhiteCharges, IsOriginalFixedPrice, H.OriginalFixedPrice, IsOriginalPriceGram, H.OriginalPriceGram, H.OriginalPriceTK,  H.OriginalGemsPrice, " & _
                '                " OriginalOtherPrice, H.LocationID,L.Location, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, " & _
                '                " H.LossItemTG ,   HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, " & _
                '                " HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate], H.GoldSmith, H.IsDiamond, H.Remark ," & _
                '                " GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,H.GoldSmithID ,GG.Name FROM tbl_Transfer T" & _
                '                " Left JOIN tbl_TransferItem TI On T.Transferid=TI.TransferID " & _
                '                " LEFT JOIN tbl_Forsale H on H.Forsaleid=TI.ForsaleID " & _
                '                " LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID  " & _
                '                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID  " & _
                '                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID   " & _
                '                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID   " & _
                '                " LEFT JOIN tbl_Location L ON L.LocationID=T.LocationID  " & _
                '                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                '                " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID  " & _
                '                " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID  WHERE T.IsDelete=0  And T.LocationID='" & LocationID & "' " & FilterString & _
                '                " AND TI.ForSaleID NOT IN   (SELECT ForSaleID FROM tbl_SaleInvoiceDetail SD " & _
                '                " INNER JOIN tbl_SaleInvoiceHeader SI  on SD.SaleInvoiceHeaderID=SI.SaleInvoiceHeaderID Where SI.isDelete=0 And SI.LocationID='" & LocationID & "' )" & _
                '                " AND TI.ForSaleID NOT IN (SELECT ForSaleID FROM tbl_WholesaleInvoiceItem WI " & _
                '                " INNER JOIN tbl_WholeSaleInvoice W on W.WholeSaleInvoiceID=WI.WholesaleInvoiceID  Where W.isDelete=0 And W.LocationID='" & LocationID & "' And  WI.IsReturn=0) " & _
                '                " AND TI.ForSaleID NOT IN (SELECT ForSaleID from tbl_OrderReturnDetail D Inner Join tbl_OrderReturnHeader H on D.OrderReturnHeaderID=H.OrderReturnHeaderID " & _
                '                " Where H.Isdelete=0 And H.LocationID='" & LocationID & "')" & " Order by ItemCode ASC "

                strCommandText = "SELECT H.ForSaleID, CAST((ItemTG-H.GemsTG) as DECIMAL(18,4)) as GoldTG, CAST(H.GemsTG AS DECIMAL(18,3)) as GemsTG , CAST(ItemTG as DECIMAL(18,4)) as " & _
                               " ItemTG, CAST(H.WasteTG as DECIMAL(18,3)) as WasteTG , ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, " & _
                               " ItemCategory, GivenDate,  IsExit,H.OriginalGemsPrice, ExitDate, (ItemTK-H.GemsTK) AS GoldTK,  H.GemsTK, TotalTK,  WasteTK ,TotalTG, ItemTK,  Width, " & _
                               " H.IsFixPrice, H.FixPrice, DesignCharges,   CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                               " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,  " & _
                               " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP,  " & _
                               " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'8.0' AS " & _
                               " DECIMAL(18,2)) AS GoldY,  CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                               " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                               " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP,  CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)" & _
                               " -CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS GemsY,  CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, " & _
                               " CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP, CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)" & _
                               " -CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY, PlatingCharges, MountingCharges, " & _
                               " WhiteCharges, IsOriginalFixedPrice, H.OriginalFixedPrice, IsOriginalPriceGram, H.OriginalPriceGram, H.OriginalPriceTK,  H.OriginalGemsPrice, " & _
                               " OriginalOtherPrice, H.LocationID,L.Location, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, " & _
                               " H.LossItemTG ,   HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, " & _
                               " HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate], H.GoldSmith, H.IsDiamond, H.Remark ," & _
                               " GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,H.GoldSmithID ,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith,H.PurchaseWasteTK,H.PurchaseWasteTG FROM tbl_ForSale H" & _
                               " LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID  " & _
                               " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID  " & _
                               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID   " & _
                               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID   " & _
                               " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                               " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  " & _
                               " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID  " & _
                               " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                               " Where H.Isdelete=0 And H.LocationID='" & LocationID & "'" & FilterString & " Order by ItemCode ASC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleForReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleForReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT H.ForSaleID, CAST((ItemTG-H.GemsTG) as DECIMAL(18,4)) as GoldTG, CAST(H.GemsTG AS DECIMAL(18,3)) as GemsTG , CAST(ItemTG as DECIMAL(18,4)) as ItemTG, CAST(H.WasteTG as DECIMAL(18,3)) as WasteTG , ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-H.GemsTK) AS GoldTK,  H.GemsTK, TotalTK,  WasteTK ,TotalTG, ItemTK,  Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP," & _
                " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY," & _
                " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,  " & _
                " HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate],  H.IsDiamond, H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,H.GoldSmithID ,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith,SP.SupplierName as Supplier,H.SellingRate,H.PriceCode,H.PurchaseWasteTK,H.PurchaseWasteTG" & _
                " FROM tbl_ForSale H LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                " LEFT JOIN tbl_Supplier SP On SP.SupplierID=H.SupplierID " & _
                " WHERE 1=1 " & cristr & " Order by ItemCode ASC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DBComm.CommandTimeout = 0
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleForIsCloseReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleForIsCloseReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT H.ForSaleID, CAST((ItemTG-H.GemsTG) as DECIMAL(18,4)) as GoldTG, CAST(H.GemsTG AS DECIMAL(18,3)) as GemsTG , CAST(ItemTG as DECIMAL(18,4)) as ItemTG, CAST(H.WasteTG as DECIMAL(18,3)) as WasteTG , ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-H.GemsTK) AS GoldTK,  H.GemsTK, TotalTK,  WasteTK ,TotalTG, ItemTK,  Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                " CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                " CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                " CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,  " & _
                " HI.ForSaleGemsItemID, HI.GemsCategoryID, GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, HI.GemsRemark, GivenDate as [@GDate], H.GoldSmith, H.IsDiamond, H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode,H.GoldSmithID ,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith,H.PurchaseWasteTK,H.PurchaseWasteTG" & _
                " FROM tbl_ForSale H LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                " WHERE H.IsDelete=0 and 1=1  And H.ExitDate Between @FromDate And @ToDate " & cristr & " Order by ExitDate asc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                DBComm.CommandTimeout = 0
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateSaleItemIsExit(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateSaleItemIsExit
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                If Obj.IsSolidVolume = True Then
                    strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END),LastModifiedDate=GetDate() " & _
                    " WHERE ForSaleID=@ForSaleID and LossItemTG <=0 And IsSolidVolume=1 "

                    strCommandText += "UPDATE tbl_ForSale SET IsExit=0, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END),LastModifiedDate=GetDate() " & _
                   " WHERE ForSaleID=@ForSaleID and LossItemTG >0 And IsSolidVolume=1 "
                ElseIf Obj.IsLooseDiamond = True Then
                    strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END),LastModifiedDate=GetDate() " & _
                    " WHERE ForSaleID=@ForSaleID and LossItemTG <=0 And IsLooseDiamond=1 "

                    strCommandText += "UPDATE tbl_ForSale SET IsExit=0, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END),LastModifiedDate=GetDate() " & _
                   " WHERE ForSaleID=@ForSaleID and LossItemTG >0 And IsLooseDiamond=1 "
                Else
                    strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END),LastModifiedDate=GetDate() " & _
                  " WHERE ForSaleID=@ForSaleID and LossQTY = '0' "

                    strCommandText += "UPDATE tbl_ForSale SET IsExit=0, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END),LastModifiedDate=GetDate() " & _
                  " WHERE ForSaleID=@ForSaleID and LossQTY <>'0' "
                End If
                '  strCommandText = "UPDATE tbl_ForSale SET IsExit=0, ExitDate= (CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END) " & _
                '" WHERE ForSaleID=@ForSaleID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, Obj.IsExit)
                DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, IIf(Obj.ExitDate <> Nothing, Obj.ExitDate, DBNull.Value))
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
        Public Function UpdateSaleItemIsExitForTransfer(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateSaleItemIsExitForTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit, ExitDate= @ExitDate,LastModifiedDate=GetDate() " & _
                    " WHERE ForSaleID=@ForSaleID  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, Obj.IsExit)
                DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, IIf(Obj.ExitDate <> Nothing, Obj.ExitDate, DBNull.Value))
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
        Public Function UpdateSaleItemIsExitForTransferReturn(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateSaleItemIsExitForTransferReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit, ExitDate= @ExitDate,LocationID=@LocationID " & _
                    " WHERE ForSaleID=@ForSaleID  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, Obj.IsExit)
                DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, IIf(Obj.ExitDate <> Nothing, Obj.ExitDate, DBNull.Value))
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
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
        Public Function UpdateTransferItemIsExitForTransfer(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateTransferItemIsExitForTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit, ExitDate= @ExitDate,LocationID=@LocationID,LastModifiedDate=getDate() " & _
                    " WHERE ForSaleID=@ForSaleID  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, Obj.IsExit)
                DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, DBNull.Value)
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

        Public Function UpdateWholeSalesReturnDeleteItemIsExit(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateWholeSalesReturnDeleteItemIsExit
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit,WReturnDate=null " & _
              " WHERE ForSaleID=@ForSaleID and LossQTY = '0' "

                strCommandText += "UPDATE tbl_ForSale SET IsExit=@IsExit,WReturnDate=null " & _
              " WHERE ForSaleID=@ForSaleID and LossQTY <>'0' "

                '  strCommandText = "UPDATE tbl_ForSale SET IsExit=0, ExitDate= (CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END) " & _
                '" WHERE ForSaleID=@ForSaleID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, Obj.IsExit)
                DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, IIf(Obj.ExitDate <> Nothing, Obj.ExitDate, DBNull.Value))
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

        Public Function UpdateWholeSaleReturnItemIsExit(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateWholeSaleReturnItemIsExit
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                '  strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END) " & _
                '" WHERE ForSaleID=@ForSaleID and LossQTY = '0' "

                '  strCommandText += "UPDATE tbl_ForSale SET IsExit=0, ExitDate=(CASE @IsExit WHEN 0 THEN NULL ELSE @ExitDate END) " & _
                '" WHERE ForSaleID=@ForSaleID and LossQTY <>'0' "

                strCommandText = "UPDATE tbl_ForSale SET IsExit=@IsExit,WReturnDate=@WReturnDate " & _
              " WHERE ForSaleID=@ForSaleID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, Obj.IsExit)
                DB.AddInParameter(DBComm, "@WReturnDate", DbType.Date, IIf(Obj.WReturnDate <> Nothing, Obj.WReturnDate, DBNull.Value))
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
        Public Function GetSalesIDByItemCode(ByVal argItemCode As String) As String Implements ISalesItemDA.GetSalesIDByItemCode
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "SELECT ForSaleID FROM tbl_ForSale WHERE ItemCode = @ItemCode and IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, argItemCode)

                GetSalesIDByItemCode = CStr(DB.ExecuteScalar(DBComm))
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function DeleteForSale(ByVal ForSaleID As String) As Boolean Implements ISalesItemDA.DeleteForSale
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_ForSale SET IsDelete=1,LastModifiedDate=getDate() WHERE  ForSaleID= @ForSaleID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
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

        Public Function DeleteForSaleGems(ByVal ForSaleGemsItemID As String) As Boolean Implements ISalesItemDA.DeleteForSaleGems
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_ForSaleGemsItem Where ForSaleGemsItemID = @ForSaleGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ForSaleGemsItemID", DbType.String, ForSaleGemsItemID)

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

        Public Function GetAllForSaleHeader(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetAllForSaleHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select ForSaleID as [@ForSaleID],F.IsExit AS [@IsExit], F.IsVolume AS [@IsVolume],  F.IsOrder as [$IsOrder], F.IsDiamond as [$IsDiamond], F.IsClosed As [@IsClosed], F.ItemCode, CONVERT(VARCHAR,CAST(F.ItemTG AS DECIMAL(18,3))) AS Gram, Convert(varchar(10),F.GivenDate,105) as GivenDate, F.GoldQualityID as [@GoldQualityID], F.ItemNameID as [@ItemNameID], F.ItemCategoryID as [@ItemCategoryID],F.LocationID as [@LocationID], I.ItemCategory as [ItemCategory_],N.ItemName as [ItemName_], G.GoldQuality as [GoldQuality_], F.OriginalCode, F.PriceCode, F.Color,I.ItemTaxPer,F.GoldSmithID,"
                strCommandText += " CONVERT(VARCHAR,CAST(F.ItemTK AS INT)) AS ItemK,"
                strCommandText += " CONVERT(VARCHAR,CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT)) AS ItemP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS ItemY,"
                strCommandText += " IsNull(R.OrderInvoiceID,'-') AS OrderVoucherNo, S.Staff As [Staff_], F.GoldSmith AS [GoldSmith_], F.Remark AS [Remark_], F.OrderReceiveDetailID As [@OrderReceiveDetailID], F.GivenDate as [@GDate],F.Color as [@Color],F.SupplierID As SupplierID,F.SupplierVou As SupplierVou"
                strCommandText += " From  tbl_ForSale F Left Join tbl_GoldQuality G On F.GoldQualityID=G.GoldQualityID Left Join tbl_ItemCategory I On F.ItemCategoryID=I.ItemCategoryID LEFT JOIN tbl_Location L ON L.LocationID=F.LocationID "
                strCommandText += " LEFT JOIN tbl_OrderReceiveDetail R ON R.OrderReceiveDetailID=F.OrderReceiveDetailID "
                strCommandText += " LEFT JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID LEFT JOIN tbl_Staff S On S.StaffID=F.StaffID " & cristr & ""
                strCommandText += " and F.LocationID= '" & Global_CurrentLocationID & "'" & " and F.IsDelete = 0 order by [@GDate] DESC, ForSaleID DESC, CASE OrderInvoiceID WHEN '-' THEN ItemCode Else OrderInvoiceID END "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)

                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleInfo(ByVal ForSaleID As String) As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleInfo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo



            Try
                strCommandText = "Select ForSaleID,ItemCode,ItemNameID,Length,GoldQualityID,ItemCategoryID,GivenDate,LocationID,GoldSmithID,"
                strCommandText += " CAST((ItemTK-GemsTK) AS INT) AS GoldK,"
                strCommandText += " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS  DECIMAL(18,2)) AS GoldY,"
                strCommandText += " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC,"
                strCommandText += " (ItemTK-GemsTK) As GoldTK,(ItemTG-GemsTG) AS GoldTG,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " GemsTK,GemsTG,"
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,"
                strCommandText += " WasteTK,WasteTG,"
                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,"
                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                strCommandText += " ItemTK,ItemTG,"
                strCommandText += " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK,"
                strCommandText += " CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,"
                strCommandText += " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PurchaseWasteY,"
                strCommandText += " CAST(((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS PurchaseWasteC,"
                strCommandText += " PurchaseWasteTK,PurchaseWasteTG,"
                strCommandText += " CAST((ItemTK+WasteTK) AS INT) AS TotalK,"
                strCommandText += " CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,"
                strCommandText += " CAST((((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS TotalC,(ItemTK+WasteTK) AS TotalTK, (ItemTG+WasteTG) AS TotalTG,"
                strCommandText += " LossItemTK,LossItemTG, LossQTY, "
                strCommandText += " IsExit,IsNull(Width,'-') as Width,IsFixPrice,FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, IsOrder, IsClosed, IsVolume, QTY, StaffID , OrderReceiveDetailID, GoldSmith, Remark , IsDiamond , OriginalCode , PriceCode,Color,SupplierID,SupplierVou,IsSolidVolume,SellingRate,WSFixPrice, "
                strCommandText += " IsLooseDiamond,Shape,Clarity,IsOriginalPriceCarat,OriginalPriceCarat,SDYOrCOrG,SDGemsTW,SDGemsCategoryID,SDGemsName,TotalCost "
                strCommandText += " From tbl_ForSale  where IsDelete=0 and ForSaleID = '" & ForSaleID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemNameID = drResult("ItemNameID")
                        .Length = drResult("Length")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .LocationID = drResult("LocationID")
                        .GivenDate = drResult("GivenDate")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .GoldC = drResult("GoldC")

                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .GemsK = drResult("GemsK")
                        .GemsP = drResult("GemsP")
                        .GemsY = drResult("GemsY")
                        .GemsC = drResult("GemsC")

                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .WasteK = drResult("WasteK")
                        .WasteP = drResult("WasteP")
                        .WasteY = drResult("WasteY")
                        .WasteC = drResult("WasteC")

                        .ItemTK = drResult("ItemTK")
                        .ItemTG = drResult("ItemTG")
                        .ItemK = drResult("ItemK")
                        .ItemP = drResult("ItemP")
                        .ItemY = drResult("ItemY")
                        .ItemC = drResult("ItemC")

                        .TotalTK = drResult("TotalTK")
                        .TotalTG = drResult("TotalTG")
                        .TotalK = drResult("TotalK")
                        .TotalP = drResult("TotalP")
                        .TotalY = drResult("TotalY")
                        .TotalC = drResult("TotalC")

                        .LossItemTG = drResult("LossItemTG")
                        .LossItemTK = drResult("LossItemTK")
                        .LossQTY = drResult("LossQTY")

                        .PurchaseWasteK = drResult("PurchaseWasteK")
                        .PurchaseWasteP = drResult("PurchaseWasteP")
                        .PurchaseWasteY = drResult("PurchaseWasteY")
                        .PurchaseWasteC = drResult("PurchaseWasteC")
                        .PurchaseWasteTK = drResult("PurchaseWasteTK")
                        .PurchaseWasteTG = drResult("PurchaseWasteTG")

                        .IsExit = drResult("IsExit")
                        .Width = drResult("Width")
                        .DesignCharges = drResult("DesignCharges")
                        .PlatingCharges = drResult("PlatingCharges")
                        .MountingCharges = drResult("MountingCharges")
                        .WhiteCharges = drResult("WhiteCharges")
                        .IsFixPrice = drResult("IsFixPrice")
                        .FixPrice = drResult("FixPrice")
                        .IsOriginalFixedPrice = drResult("IsOriginalFixedPrice")
                        .OriginalFixedPrice = drResult("OriginalFixedPrice")

                        .IsOriginalPriceGram = drResult("IsOriginalPriceGram")
                        .OriginalPriceGram = drResult("OriginalPriceGram")
                        .OriginalPriceTK = drResult("OriginalPriceTK")
                        .OriginalGemsPrice = drResult("OriginalGemsPrice")
                        .OriginalOtherPrice = drResult("OriginalOtherPrice")
                        .Photo = drResult("Photo")
                        .SellingPrice = drResult("SellingPrice")

                        .IsClosed = drResult("IsClosed")
                        .IsOrder = drResult("IsOrder")
                        .QTY = drResult("QTY")
                        .IsVolume = drResult("IsVolume")
                        .StaffID = drResult("StaffID")
                        .OrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .GoldSmith = drResult("GoldSmith")
                        .Remark = drResult("Remark")
                        .IsDiamond = drResult("IsDiamond")
                        .OriginalCode = drResult("OriginalCode")
                        .PriceCode = drResult("PriceCode")
                        .Color = drResult("Color")
                        .SupplierID = drResult("SupplierID")
                        .SupplierVou = drResult("SupplierVou")
                        .GoldSmithID = drResult("GoldSmithID")
                        .IsSolidVolume = drResult("IsSolidVolume")
                        .SellingRate = drResult("SellingRate")
                        .WSFixPrice = drResult("WSFixPrice")
                        .IsLooseDiamond = drResult("IsLooseDiamond")
                        .Shape = drResult("Shape")
                        .Clarity = drResult("Clarity")
                        .IsOriginalPriceCarat = drResult("IsOriginalPriceCarat")
                        .OriginalPriceCarat = drResult("OriginalPriceCarat")
                        .SDYOrCOrG = drResult("SDYOrCOrG")
                        .SDGemsTW = drResult("SDGemsTW")
                        .SDGemsCategoryID = drResult("SDGemsCategoryID")
                        .SDGemsName = drResult("SDGemsName")
                        .TotalCost = drResult("TotalCost")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
        Public Function GetForSaleInfo_History(ByVal ForSaleID As String) As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleInfo_History
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo



            Try
                strCommandText = "Select ForSaleID,ItemCode,ItemNameID,Length,GoldQualityID,ItemCategoryID,GivenDate,LocationID,GoldSmithID,"
                strCommandText += " CAST((ItemTK-GemsTK) AS INT) AS GoldK,"
                strCommandText += " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS  DECIMAL(18,2)) AS GoldY,"
                strCommandText += " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC,"
                strCommandText += " (ItemTK-GemsTK) As GoldTK,(ItemTG-GemsTG) AS GoldTG,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " GemsTK,GemsTG,"
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,"
                strCommandText += " WasteTK,WasteTG,"
                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,"
                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                strCommandText += " ItemTK,ItemTG,"
                strCommandText += " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK,"
                strCommandText += " CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,"
                strCommandText += " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PurchaseWasteY,"
                strCommandText += " CAST(((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS PurchaseWasteC,"
                strCommandText += " PurchaseWasteTK,PurchaseWasteTG,"
                strCommandText += " CAST((ItemTK+WasteTK) AS INT) AS TotalK,"
                strCommandText += " CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,"
                strCommandText += " CAST((((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS TotalC,(ItemTK+WasteTK) AS TotalTK, (ItemTG+WasteTG) AS TotalTG,"
                strCommandText += " LossItemTK,LossItemTG, LossQTY, "
                strCommandText += " IsExit,IsNull(Width,'-') as Width,IsFixPrice,FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, IsOrder, IsClosed, IsVolume, QTY, StaffID , OrderReceiveDetailID, GoldSmith, Remark , IsDiamond , OriginalCode , PriceCode,Color,SupplierID,SupplierVou"
                strCommandText += " From tbl_ForSale  where IsDelete=0 and ForSaleID = '" & ForSaleID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .HForSaleID = drResult("ForSaleID")
                        .HItemCode = drResult("ItemCode")
                        .HItemNameID = drResult("ItemNameID")
                        .HLength = drResult("Length")
                        .HGoldQualityID = drResult("GoldQualityID")
                        .HItemCategoryID = drResult("ItemCategoryID")
                        .HLocationID = drResult("LocationID")
                        .HGivenDate = drResult("GivenDate")
                        .HGoldTK = drResult("GoldTK")
                        .HGoldTG = drResult("GoldTG")
                        .HGoldK = drResult("GoldK")
                        .HGoldP = drResult("GoldP")
                        .HGoldY = drResult("GoldY")
                        .HGoldC = drResult("GoldC")

                        .HGemsTK = drResult("GemsTK")
                        .HGemsTG = drResult("GemsTG")
                        .HGemsK = drResult("GemsK")
                        .HGemsP = drResult("GemsP")
                        .HGemsY = drResult("GemsY")
                        .HGemsC = drResult("GemsC")

                        .HWasteTK = drResult("WasteTK")
                        .HWasteTG = drResult("WasteTG")
                        .HWasteK = drResult("WasteK")
                        .HWasteP = drResult("WasteP")
                        .HWasteY = drResult("WasteY")
                        .HWasteC = drResult("WasteC")

                        .HItemTK = drResult("ItemTK")
                        .HItemTG = drResult("ItemTG")
                        .HItemK = drResult("ItemK")
                        .HItemP = drResult("ItemP")
                        .HItemY = drResult("ItemY")
                        .HItemC = drResult("ItemC")

                        .HTotalTK = drResult("TotalTK")
                        .HTotalTG = drResult("TotalTG")
                        .HTotalK = drResult("TotalK")
                        .HTotalP = drResult("TotalP")
                        .HTotalY = drResult("TotalY")
                        .HTotalC = drResult("TotalC")

                        .HLossItemTG = drResult("LossItemTG")
                        .HLossItemTK = drResult("LossItemTK")
                        .HLossQTY = drResult("LossQTY")

                        .HPurchaseWasteK = drResult("PurchaseWasteK")
                        .HPurchaseWasteP = drResult("PurchaseWasteP")
                        .HPurchaseWasteY = drResult("PurchaseWasteY")
                        .HPurchaseWasteC = drResult("PurchaseWasteC")
                        .HPurchaseWasteTK = drResult("PurchaseWasteTK")
                        .HPurchaseWasteTG = drResult("PurchaseWasteTG")

                        '.IsExit = drResult("IsExit")
                        .HWidth = drResult("Width")
                        .HDesignCharges = drResult("DesignCharges")
                        .HPlatingCharges = drResult("PlatingCharges")
                        .HMountingCharges = drResult("MountingCharges")
                        .HWhiteCharges = drResult("WhiteCharges")
                        .HIsFixPrice = drResult("IsFixPrice")
                        .HFixPrice = drResult("FixPrice")
                        .HIsOriginalFixedPrice = drResult("IsOriginalFixedPrice")
                        .HOriginalFixedPrice = drResult("OriginalFixedPrice")

                        .HIsOriginalPriceGram = drResult("IsOriginalPriceGram")
                        .HOriginalPriceGram = drResult("OriginalPriceGram")
                        .HOriginalPriceTK = drResult("OriginalPriceTK")
                        .HOriginalGemsPrice = drResult("OriginalGemsPrice")
                        .HOriginalOtherPrice = drResult("OriginalOtherPrice")
                        .HPhoto = drResult("Photo")
                        .HSellingPrice = drResult("SellingPrice")

                        .HIsClosed = drResult("IsClosed")
                        .HIsOrder = drResult("IsOrder")
                        .HQTY = drResult("QTY")
                        .HIsVolume = drResult("IsVolume")
                        .HStaffID = drResult("StaffID")
                        .HOrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .HGoldSmith = drResult("GoldSmith")
                        .HRemark = drResult("Remark")
                        .HIsDiamond = drResult("IsDiamond")
                        .HOriginalCode = drResult("OriginalCode")
                        .HPriceCode = drResult("PriceCode")
                        .HColor = drResult("Color")
                        .HSupplierID = drResult("SupplierID")
                        .HSupplierVou = drResult("SupplierVou")
                        .HGoldSmithID = drResult("GoldSmithID")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetForSaleInfoY(ByVal ForSaleID As String) As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleInfoY
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo



            Try
                strCommandText = "Select ForSaleID,ItemCode,ItemNameID,Length,GoldQualityID,ItemCategoryID,GivenDate,LocationID,"
                strCommandText += " CAST((ItemTK-GemsTK) AS INT) AS GoldK,"
                strCommandText += " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS  DECIMAL(18,2)) AS GoldY,"
                strCommandText += " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC,"
                strCommandText += " (ItemTK-GemsTK) As GoldTK,(ItemTG-GemsTG) AS GoldTG,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " GemsTK,GemsTG,"
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,"
                strCommandText += " WasteTK,WasteTG,"
                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,"
                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                strCommandText += " ItemTK,ItemTG,"
                strCommandText += " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK,"
                strCommandText += " CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,"
                strCommandText += " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PurchaseWasteY,"
                strCommandText += " CAST(((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS PurchaseWasteC,"
                strCommandText += " PurchaseWasteTK,PurchaseWasteTG,"
                strCommandText += " CAST((ItemTK+WasteTK) AS INT) AS TotalK,"
                strCommandText += " CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,"
                strCommandText += " CAST((((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS TotalC,(ItemTK+WasteTK) AS TotalTK, (ItemTG+WasteTG) AS TotalTG,"
                strCommandText += " LossItemTK,LossItemTG, LossQTY, "
                strCommandText += " IsExit,IsNull(Width,'-') as Width,IsFixPrice,FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, IsOrder, IsClosed, IsVolume, QTY, StaffID , OrderReceiveDetailID, GoldSmith, Remark , IsDiamond , OriginalCode , PriceCode,Color,SupplierID,SupplierVou"
                strCommandText += " From tbl_ForSale  where IsDelete=0 and ForSaleID = '" & ForSaleID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemNameID = drResult("ItemNameID")
                        .Length = drResult("Length")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .LocationID = drResult("LocationID")
                        .GivenDate = drResult("GivenDate")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .GoldC = drResult("GoldC")

                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .GemsK = drResult("GemsK")
                        .GemsP = drResult("GemsP")
                        .GemsY = drResult("GemsY")
                        .GemsC = drResult("GemsC")

                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .WasteK = drResult("WasteK")
                        .WasteP = drResult("WasteP")
                        .WasteY = drResult("WasteY")
                        .WasteC = drResult("WasteC")

                        .ItemTK = drResult("ItemTK")
                        .ItemTG = drResult("ItemTG")
                        .ItemK = drResult("ItemK")
                        .ItemP = drResult("ItemP")
                        .ItemY = drResult("ItemY")
                        .ItemC = drResult("ItemC")

                        .TotalTK = drResult("TotalTK")
                        .TotalTG = drResult("TotalTG")
                        .TotalK = drResult("TotalK")
                        .TotalP = drResult("TotalP")
                        .TotalY = drResult("TotalY")
                        .TotalC = drResult("TotalC")

                        .LossItemTG = drResult("LossItemTG")
                        .LossItemTK = drResult("LossItemTK")
                        .LossQTY = drResult("LossQTY")

                        .PurchaseWasteK = drResult("PurchaseWasteK")
                        .PurchaseWasteP = drResult("PurchaseWasteP")
                        .PurchaseWasteY = drResult("PurchaseWasteY")
                        .PurchaseWasteC = drResult("PurchaseWasteC")
                        .PurchaseWasteTK = drResult("PurchaseWasteTK")
                        .PurchaseWasteTG = drResult("PurchaseWasteTG")

                        .IsExit = drResult("IsExit")
                        .Width = drResult("Width")
                        .DesignCharges = drResult("DesignCharges")
                        .PlatingCharges = drResult("PlatingCharges")
                        .MountingCharges = drResult("MountingCharges")
                        .WhiteCharges = drResult("WhiteCharges")
                        .IsFixPrice = drResult("IsFixPrice")
                        .FixPrice = drResult("FixPrice")
                        .IsOriginalFixedPrice = drResult("IsOriginalFixedPrice")
                        .OriginalFixedPrice = drResult("OriginalFixedPrice")

                        .IsOriginalPriceGram = drResult("IsOriginalPriceGram")
                        .OriginalPriceGram = drResult("OriginalPriceGram")
                        .OriginalPriceTK = drResult("OriginalPriceTK")
                        .OriginalGemsPrice = drResult("OriginalGemsPrice")
                        .OriginalOtherPrice = drResult("OriginalOtherPrice")
                        .Photo = drResult("Photo")
                        .SellingPrice = drResult("SellingPrice")

                        .IsClosed = drResult("IsClosed")
                        .IsOrder = drResult("IsOrder")
                        .QTY = drResult("QTY")
                        .IsVolume = drResult("IsVolume")
                        .StaffID = drResult("StaffID")
                        .OrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .GoldSmith = drResult("GoldSmith")
                        .Remark = drResult("Remark")
                        .IsDiamond = drResult("IsDiamond")
                        .OriginalCode = drResult("OriginalCode")
                        .PriceCode = drResult("PriceCode")
                        .Color = drResult("Color")
                        .SupplierID = drResult("SupplierID")
                        .SupplierVou = drResult("SupplierVou")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
        Public Function InsertForSale(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.InsertForSale
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ForSale (ForSaleID,ItemCode,ItemNameID,Length,GoldQualityID,ItemCategoryID,GivenDate,GoldTK,GoldTG,GemsTK,GemsTG,WasteTK,WasteTG,ItemTK,ItemTG,TotalTK,TotalTG,IsExit,LastModifiedLoginUserName,LastModifiedDate,Width,IsFixPrice,FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceGram,OriginalPriceTK,OriginalGemsPrice,OriginalOtherPrice,Photo,SellingPrice,LocationID, IsClosed, IsOrder, IsVolume, QTY, StaffID, LossQTY, LossItemTK, LossItemTG,TotalGemPrice, PurchaseWasteTK, PurchaseWasteTG,OrderReceiveDetailID, GoldSmith, Remark,IsDiamond,OriginalCode,PriceCode,Color,IsDelete,IsSync,SupplierID,SupplierVou,GoldSmithID,IsSolidVolume,SellingRate,IsCheck,WSFixPrice,SDGemsCategoryID,IsLooseDiamond,Shape,Clarity,SDGemsName,IsOriginalPriceCarat,OriginalPriceCarat,SDGemsTW,SDYOrCOrG,TotalCost)"
                strCommandText += " Values (@ForSaleID,@ItemCode,@ItemNameID,@Length,@GoldQualityID,@ItemCategoryID,@GivenDate,@GoldTK,@GoldTG,@GemsTK,@GemsTG,@WasteTK,@WasteTG,@ItemTK,@ItemTG,@TotalTK,@TotalTG,@IsExit,@LastModifiedLoginUserName,getdate(),@Width,@IsFixPrice,@FixPrice,@DesignCharges,@PlatingCharges,@MountingCharges,@WhiteCharges,@IsOriginalFixedPrice,@OriginalFixedPrice,@IsOriginalPriceGram,@OriginalPriceGram,@OriginalPriceTK,@OriginalGemsPrice,@OriginalOtherPrice,@Photo,@SellingPrice, @LocationID, @IsClosed, @IsOrder, @IsVolume, @QTY, @StaffID, @LossQTY, @LossItemTK, @LossItemTG,@TotalGemPrice, @PurchaseWasteTK, @PurchaseWasteTG, @OrderReceiveDetailID, @GoldSmith, @Remark,@IsDiamond,@OriginalCode,@PriceCode,@Color,0,0,@SupplierID,@SupplierVou,@GoldSmithID,@IsSolidVolume,@SellingRate,0,@WSFixPrice,@SDGemsCategoryID,@IsLooseDiamond,@Shape,@Clarity,@SDGemsName,@IsOriginalPriceCarat,@OriginalPriceCarat,@SDGemsTW,@SDYOrCOrG,@TotalCost)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, Obj.ItemNameID)
                DB.AddInParameter(DBComm, "@Length", DbType.String, Obj.Length)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GivenDate", DbType.DateTime, Obj.GivenDate)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, Obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, Obj.WasteTG)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, Obj.TotalTK)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, Obj.TotalTG)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Int32, Obj.IsExit)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, Obj.FixPrice)
                DB.AddInParameter(DBComm, "@Width", DbType.String, Obj.Width)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, Obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, Obj.MountingCharges)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, Obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, Obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, Obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceGram", DbType.Boolean, Obj.IsOriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, Obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, Obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, Obj.OriginalGemsPrice)
                DB.AddInParameter(DBComm, "@OriginalOtherPrice", DbType.Int64, Obj.OriginalOtherPrice)
                DB.AddInParameter(DBComm, "@Photo", DbType.String, Obj.Photo)
                DB.AddInParameter(DBComm, "@SellingPrice", DbType.String, Obj.SellingPrice)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                DB.AddInParameter(DBComm, "@IsOrder", DbType.Boolean, Obj.IsOrder)
                DB.AddInParameter(DBComm, "@IsClosed", DbType.Boolean, Obj.IsClosed)
                DB.AddInParameter(DBComm, "@IsVolume", DbType.Boolean, Obj.IsVolume)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, Obj.QTY)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@LossQTY", DbType.Int32, Obj.LossQTY)
                DB.AddInParameter(DBComm, "@LossItemTK", DbType.Decimal, Obj.LossItemTK)
                DB.AddInParameter(DBComm, "@LossItemTG", DbType.Decimal, Obj.LossItemTG)
                DB.AddInParameter(DBComm, "@TotalGemPrice", DbType.Int64, Obj.TotalGemPrice)
                DB.AddInParameter(DBComm, "@PurchaseWasteTK", DbType.Decimal, Obj.PurchaseWasteTK)
                DB.AddInParameter(DBComm, "@PurchaseWasteTG", DbType.Decimal, Obj.PurchaseWasteTG)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, Obj.OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@GoldSmith", DbType.String, Obj.GoldSmith)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@IsDiamond", DbType.Boolean, Obj.IsDiamond)
                DB.AddInParameter(DBComm, "@OriginalCode", DbType.String, Obj.OriginalCode)
                DB.AddInParameter(DBComm, "@PriceCode", DbType.String, Obj.PriceCode)
                DB.AddInParameter(DBComm, "@Color", DbType.String, Obj.Color)
                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, Obj.SupplierID)
                DB.AddInParameter(DBComm, "@SupplierVou", DbType.String, Obj.SupplierVou)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, Obj.GoldSmithID)
                DB.AddInParameter(DBComm, "@IsSolidVolume", DbType.Boolean, Obj.IsSolidVolume)
                DB.AddInParameter(DBComm, "@SellingRate", DbType.Int32, Obj.SellingRate)
                DB.AddInParameter(DBComm, "@WSFixPrice", DbType.Int32, Obj.WSFixPrice)
                DB.AddInParameter(DBComm, "@SDGemsCategoryID", DbType.String, Obj.SDGemsCategoryID)
                DB.AddInParameter(DBComm, "@IsLooseDiamond", DbType.Boolean, Obj.IsLooseDiamond)
                DB.AddInParameter(DBComm, "@Shape", DbType.String, Obj.Shape)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, Obj.Clarity)
                DB.AddInParameter(DBComm, "@SDGemsName", DbType.String, Obj.SDGemsName)
                DB.AddInParameter(DBComm, "@IsOriginalPriceCarat", DbType.Boolean, Obj.IsOriginalPriceCarat)
                DB.AddInParameter(DBComm, "@OriginalPriceCarat", DbType.Int32, Obj.OriginalPriceCarat)
                DB.AddInParameter(DBComm, "@SDGemsTW", DbType.Decimal, Obj.SDGemsTW)
                DB.AddInParameter(DBComm, "@SDYOrCOrG", DbType.String, Obj.SDYOrCOrG)
                DB.AddInParameter(DBComm, "@TotalCost", DbType.Int32, Obj.TotalCost)
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

        Public Function InsertForSaleGems(ByVal Obj As CommonInfo.SalesItemGemsInfo) As Boolean Implements ISalesItemDA.InsertForSaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ForSaleGemsItem ( ForSaleGemsItemID,ForSaleID,GemsCategoryID,GemsName,GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,Type,UnitPrice,Amount,GemsRemark,SaleByDefinePrice)"
                strCommandText += " Values (@ForSaleGemsItemID,@ForSaleID,@GemsCategoryID,@GemsName,@GemsTK,@GemsTG,@YOrCOrG,@GemsTW,@Qty,@Type,@UnitPrice,@Amount,@GemsRemark,@SaleByDefinePrice)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleGemsItemID", DbType.String, Obj.ForSaleGemsItemID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemsTW)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
                DB.AddInParameter(DBComm, "@Type", DbType.String, Obj.Type)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, Obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)
                DB.AddInParameter(DBComm, "@GemsRemark", DbType.String, Obj.GemsRemark)
                DB.AddInParameter(DBComm, "@SaleByDefinePrice", DbType.Boolean, Obj.SaleByDefinePrice)

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

        Public Function UpdateForSale(ByVal Obj As CommonInfo.SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateForSale
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ForSale set ItemCode=@ItemCode,ItemNameID= @ItemNameID , Length= @Length , GoldQualityID= @GoldQualityID , ItemCategoryID= @ItemCategoryID , GivenDate= @GivenDate , GoldTK= @GoldTK , GoldTG= @GoldTG , GemsTK= @GemsTK , GemsTG= @GemsTG , WasteTK= @WasteTK , WasteTG= @WasteTG , ItemTK=@ItemTK,ItemTG=@ItemTG, TotalTK= @TotalTK , TotalTG= @TotalTG , IsExit= @IsExit , LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= getdate(),Width=@Width,IsFixPrice=@IsFixPrice,FixPrice=@FixPrice , OriginalGemsPrice=@OriginalGemsPrice , DesignCharges= @DesignCharges , PlatingCharges= @PlatingCharges , MountingCharges= @MountingCharges , WhiteCharges= @WhiteCharges , IsOriginalFixedPrice= @IsOriginalFixedPrice , OriginalFixedPrice= @OriginalFixedPrice , IsOriginalPriceGram= @IsOriginalPriceGram , OriginalPriceGram= @OriginalPriceGram , OriginalPriceTK= @OriginalPriceTK, OriginalOtherPrice= @OriginalOtherPrice , Photo= @Photo,SellingPrice=@SellingPrice, LocationID=@LocationID, IsOrder=@IsOrder, IsClosed=@IsClosed , IsVolume=@IsVolume, QTY=@QTY, StaffID=@StaffID , LossQTY=@LossQTY, LossItemTK=@LossItemTK, LossItemTG=@LossItemTG,TotalGemPrice= @TotalGemPrice, PurchaseWasteTK=@PurchaseWasteTK, PurchaseWasteTG=@PurchaseWasteTG, OrderReceiveDetailID=@OrderReceiveDetailID, GoldSmith=@GoldSmith, Remark=@Remark , IsDiamond=@IsDiamond , OriginalCode=@OriginalCode, PriceCode=@PriceCode ,Color=@Color ,SupplierID=@SupplierID ,SupplierVou=@SupplierVou ,GoldSmithID=@GoldSmithID,IsSolidVolume=@IsSolidVolume,SellingRate=@SellingRate,WSFixPrice=@WSFixPrice,IsLooseDiamond=@IsLooseDiamond,SDGemsCategoryID=@SDGemsCategoryID,Shape=@Shape,Clarity=@Clarity,SDGemsName=@SDGemsName,OriginalPriceCarat=@OriginalPriceCarat,IsOriginalPriceCarat=@IsOriginalPriceCarat,SDGemsTW=@SDGemsTW,SDYOrCOrG=@SDYOrCOrG,TotalCost=@TotalCost"
                strCommandText += " where ForSaleID= @ForSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, Obj.ItemCode)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, Obj.ItemNameID)
                DB.AddInParameter(DBComm, "@Length", DbType.String, Obj.Length)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, Obj.GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, Obj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GivenDate", DbType.DateTime, Obj.GivenDate)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, Obj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, Obj.GoldTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, Obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, Obj.WasteTG)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, Obj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@TotalTK", DbType.Decimal, Obj.TotalTK)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, Obj.TotalTG)
                DB.AddInParameter(DBComm, "@Width", DbType.String, Obj.Width)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Int32, Obj.IsExit)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, Obj.IsFixPrice)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, Obj.FixPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, Obj.DesignCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, Obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, Obj.MountingCharges)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, Obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, Obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, Obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceGram", DbType.Boolean, Obj.IsOriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, Obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, Obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, Obj.OriginalGemsPrice)
                DB.AddInParameter(DBComm, "@OriginalOtherPrice", DbType.Int64, Obj.OriginalOtherPrice)
                DB.AddInParameter(DBComm, "@Photo", DbType.String, Obj.Photo)
                DB.AddInParameter(DBComm, "@SellingPrice", DbType.String, Obj.SellingPrice)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Obj.LocationID)
                DB.AddInParameter(DBComm, "@IsOrder", DbType.Boolean, Obj.IsOrder)
                DB.AddInParameter(DBComm, "@IsClosed", DbType.Boolean, Obj.IsClosed)
                DB.AddInParameter(DBComm, "@IsVolume", DbType.Boolean, Obj.IsVolume)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, Obj.QTY)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@LossQTY", DbType.Int32, Obj.LossQTY)
                DB.AddInParameter(DBComm, "@LossItemTK", DbType.Decimal, Obj.LossItemTK)
                DB.AddInParameter(DBComm, "@LossItemTG", DbType.Decimal, Obj.LossItemTG)
                DB.AddInParameter(DBComm, "@TotalGemPrice", DbType.Int64, Obj.TotalGemPrice)
                DB.AddInParameter(DBComm, "@PurchaseWasteTK", DbType.Decimal, Obj.PurchaseWasteTK)
                DB.AddInParameter(DBComm, "@PurchaseWasteTG", DbType.Decimal, Obj.PurchaseWasteTG)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, Obj.OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@GoldSmith", DbType.String, Obj.GoldSmith)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@IsDiamond", DbType.Boolean, Obj.IsDiamond)
                DB.AddInParameter(DBComm, "@OriginalCode", DbType.String, Obj.OriginalCode)
                DB.AddInParameter(DBComm, "@PriceCode", DbType.String, Obj.PriceCode)
                DB.AddInParameter(DBComm, "@Color", DbType.String, Obj.Color)
                DB.AddInParameter(DBComm, "@SupplierID", DbType.String, Obj.SupplierID)
                DB.AddInParameter(DBComm, "@SupplierVou", DbType.String, Obj.SupplierVou)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, Obj.GoldSmithID)
                DB.AddInParameter(DBComm, "@IsSolidVolume", DbType.Boolean, Obj.IsSolidVolume)
                DB.AddInParameter(DBComm, "@SellingRate", DbType.Int32, Obj.SellingRate)
                DB.AddInParameter(DBComm, "@WSFixPrice", DbType.Int32, Obj.WSFixPrice)
                DB.AddInParameter(DBComm, "@IsLooseDiamond", DbType.Boolean, Obj.IsLooseDiamond)
                DB.AddInParameter(DBComm, "@SDGemsCategoryID", DbType.String, Obj.SDGemsCategoryID)
                DB.AddInParameter(DBComm, "@Shape", DbType.String, Obj.Shape)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, Obj.Clarity)
                DB.AddInParameter(DBComm, "@SDGemsName", DbType.String, Obj.SDGemsName)
                DB.AddInParameter(DBComm, "@OriginalPriceCarat", DbType.Int32, Obj.OriginalPriceCarat)
                DB.AddInParameter(DBComm, "@IsOriginalPriceCarat", DbType.Boolean, Obj.IsOriginalPriceCarat)
                DB.AddInParameter(DBComm, "@SDGemsTW", DbType.Decimal, Obj.SDGemsTW)
                DB.AddInParameter(DBComm, "@SDYOrCOrG", DbType.String, Obj.SDYOrCOrG)
                DB.AddInParameter(DBComm, "@TotalCost", DbType.Int32, Obj.TotalCost)
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

        Public Function UpdateForSaleGems(ByVal Obj As CommonInfo.SalesItemGemsInfo) As Boolean Implements ISalesItemDA.UpdateForSaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ForSaleGemsItem set ForSaleID=@ForSaleID,GemsCategoryID= @GemsCategoryID , GemsName= @GemsName , GemsTK= @GemsTK , GemsTG= @GemsTG , YOrCOrG= @YOrCOrG , GemsTW= @GemsTW, Qty= @Qty , Type= @Type , UnitPrice= @UnitPrice , Amount= @Amount ,GemsRemark= @GemsRemark,SaleByDefinePrice=@SaleByDefinePrice "
                strCommandText += " where ForSaleGemsItemID= @ForSaleGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleGemsItemID", DbType.String, Obj.ForSaleGemsItemID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemsTW)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
                DB.AddInParameter(DBComm, "@Type", DbType.String, Obj.Type)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, Obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)
                DB.AddInParameter(DBComm, "@GemsRemark", DbType.String, Obj.GemsRemark)
                DB.AddInParameter(DBComm, "@SaleByDefinePrice", DbType.Boolean, Obj.SaleByDefinePrice)

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
        Public Function GetForSaleGems(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesItemDA.GetForSaleGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "Select ForSaleGemsItemID,ForSaleID,GemsCategoryID,GemsName, "
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,  CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,  "
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, "
                strCommandText += " GemsTK,GemsTG,YOrCOrG,GemsTW,Qty,CASE Type WHEN '_' THEN '' ELSE Type END AS Type,"
                strCommandText += " UnitPrice,Amount,GemsRemark,SaleByDefinePrice "
                strCommandText += " From tbl_ForSaleGemsItem where ForSaleID= '" & ForSaleID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)

                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleItemInfo(ByVal ForSaleHeaderID As String, ByVal cristr As String) As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleItemInfo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo
            Try
                strCommandText = "Select ForSaleID,ItemCode,IsNull(N.ItemName,'-')as ItemName,Length,GoldQualityID,F.ItemCategoryID,GivenDate,F.LocationID, F.ItemNameID, F.QTY, "
                strCommandText += " CAST(GoldTK AS INT) AS GoldK,"
                strCommandText += " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY,"
                strCommandText += " CAST(((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC,"
                strCommandText += " GoldTK, GoldTG,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " GemsTK,GemsTG,"
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,"
                strCommandText += " WasteTK,WasteTG,"
                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAl(18,2)) AS ItemY,"
                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                strCommandText += " ItemTK,ItemTG,"
                strCommandText += " CAST(TotalTK AS INT) AS TotalK,"
                strCommandText += " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,"
                strCommandText += " CAST(((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS TotalC,TotalTK,TotalTG,"
                strCommandText += " IsExit,IsNull(Width,'-') as Width,IsFixPrice,FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, LossQTY, LossItemTK, LossItemTG, IsClosed, PurchaseWasteTK, PurchaseWasteTG, IsDiamond ,F.OriginalCode,F.PriceCode ,F.Color,F.TotalCost"
                strCommandText += " From tbl_ForSale F LEFT JOIN tbl_ItemName N ON F.ItemNameID=N.ItemNameID where F.IsDelete=0 and F.ForSaleID = @ForSaleHeaderID  " & cristr & " Order by GivenDate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleHeaderID", DbType.String, ForSaleHeaderID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemName = drResult("ItemName")
                        .Length = drResult("Length")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemNameID = drResult("ItemNameID")
                        .LocationID = drResult("LocationID")
                        .GivenDate = drResult("GivenDate")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .GoldC = drResult("GoldC")

                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .GemsK = drResult("GemsK")
                        .GemsP = drResult("GemsP")
                        .GemsY = drResult("GemsY")
                        .GemsC = drResult("GemsC")

                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .WasteK = drResult("WasteK")
                        .WasteP = drResult("WasteP")
                        .WasteY = drResult("WasteY")
                        .WasteC = drResult("WasteC")

                        .ItemTG = drResult("ItemTG")
                        .ItemTK = drResult("ItemTK")
                        .ItemK = drResult("ItemK")
                        .ItemP = drResult("ItemP")
                        .ItemY = drResult("ItemY")
                        .ItemC = drResult("ItemC")

                        .TotalTK = drResult("TotalTK")
                        .TotalTG = drResult("TotalTG")
                        .TotalK = drResult("TotalK")
                        .TotalP = drResult("TotalP")
                        .TotalY = drResult("TotalY")
                        .TotalC = drResult("TotalC")

                        .IsExit = drResult("IsExit")
                        .Width = drResult("Width")
                        .DesignCharges = drResult("DesignCharges")
                        .PlatingCharges = drResult("PlatingCharges")
                        .MountingCharges = drResult("MountingCharges")
                        .WhiteCharges = drResult("WhiteCharges")
                        .IsFixPrice = drResult("IsFixPrice")
                        .FixPrice = drResult("FixPrice")
                        .IsOriginalFixedPrice = drResult("IsOriginalFixedPrice")
                        .OriginalFixedPrice = drResult("OriginalFixedPrice")
                        .IsClosed = drResult("IsClosed")
                        .IsOriginalPriceGram = drResult("IsOriginalPriceGram")
                        .OriginalPriceGram = drResult("OriginalPriceGram")
                        .OriginalPriceTK = drResult("OriginalPriceTK")
                        .OriginalGemsPrice = drResult("OriginalGemsPrice")
                        .OriginalOtherPrice = drResult("OriginalOtherPrice")
                        .Photo = drResult("Photo")
                        .SellingPrice = drResult("SellingPrice")
                        .QTY = drResult("QTY")
                        .LossQTY = drResult("LossQTY")
                        .LossItemTK = drResult("LossItemTK")
                        .LossItemTG = drResult("LossItemTG")
                        .PurchaseWasteTG = drResult("PurchaseWasteTG")
                        .PurchaseWasteTK = drResult("PurchaseWasteTK")
                        .IsDiamond = drResult("IsDiamond")
                        .OriginalCode = drResult("OriginalCode")
                        .PriceCode = drResult("PriceCode")
                        .Color = drResult("Color")
                        .TotalC = drResult("TotalCost")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetForSaleInfoByItemCode(ByVal ItemCode As String, Optional ByVal criExit As String = "") As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleInfoByItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo
            Try
                strCommandText = "Select ForSaleID,ItemCode,IsNull(N.ItemName,'-')as ItemName,Length,GoldQualityID,F.ItemCategoryID,GivenDate,F.LocationID, F.ItemNameID, F.QTY, "
                strCommandText += " CAST((ItemTK-GemsTK) AS INT) AS GoldK,"
                strCommandText += " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY,"
                strCommandText += " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC,"
                strCommandText += " (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " GemsTK,GemsTG,"
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,"
                strCommandText += " WasteTK,WasteTG,"
                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,"
                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                strCommandText += " ItemTK,ItemTG,"
                strCommandText += " CAST((ItemTK+WasteTK) AS INT) AS TotalK,"
                strCommandText += " CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,"
                strCommandText += " CAST((((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS TotalC,(ItemTK+WasteTK) AS TotalTK, (ItemTG+WasteTG) AS TotalTG,"
                strCommandText += " IsExit,IsNull(Width,'-') as Width,IsFixPrice,FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, LossQTY, LossItemTK, LossItemTG, IsClosed, PurchaseWasteTK, PurchaseWasteTG ,F.OrderReceiveDetailID, F.IsDiamond,F.OriginalCode,F.PriceCode,F.Color,F.SellingRate,F.Shape,F.Clarity,F.SDGemsName, "
                strCommandText += " F.SDGemsCategoryID,F.WSFixPrice,F.OriginalPriceCarat,F.IsOriginalPriceCarat,F.SDYOrCOrG,F.SDGemsTW,F.IsLooseDiamond,F.TotalCost "
                strCommandText += " From tbl_ForSale F LEFT JOIN tbl_ItemName N ON F.ItemNameID=N.ItemNameID Where ItemCode = @ItemCode and F.IsDelete=0 and F.LocationID= '" & Global_CurrentLocationID & "'" & criExit & " Order by GivenDate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemName = drResult("ItemName")
                        .Length = drResult("Length")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemNameID = drResult("ItemNameID")
                        .LocationID = drResult("LocationID")
                        .GivenDate = drResult("GivenDate")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .GoldC = drResult("GoldC")

                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .GemsK = drResult("GemsK")
                        .GemsP = drResult("GemsP")
                        .GemsY = drResult("GemsY")
                        .GemsC = drResult("GemsC")

                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .WasteK = drResult("WasteK")
                        .WasteP = drResult("WasteP")
                        .WasteY = drResult("WasteY")
                        .WasteC = drResult("WasteC")

                        .ItemTG = drResult("ItemTG")
                        .ItemTK = drResult("ItemTK")
                        .ItemK = drResult("ItemK")
                        .ItemP = drResult("ItemP")
                        .ItemY = drResult("ItemY")
                        .ItemC = drResult("ItemC")

                        .TotalTK = drResult("TotalTK")
                        .TotalTG = drResult("TotalTG")
                        .TotalK = drResult("TotalK")
                        .TotalP = drResult("TotalP")
                        .TotalY = drResult("TotalY")
                        .TotalC = drResult("TotalC")

                        .IsExit = drResult("IsExit")
                        .Width = drResult("Width")
                        .DesignCharges = drResult("DesignCharges")
                        .PlatingCharges = drResult("PlatingCharges")
                        .MountingCharges = drResult("MountingCharges")
                        .WhiteCharges = drResult("WhiteCharges")
                        .IsFixPrice = drResult("IsFixPrice")
                        .FixPrice = drResult("FixPrice")
                        .IsOriginalFixedPrice = drResult("IsOriginalFixedPrice")
                        .OriginalFixedPrice = drResult("OriginalFixedPrice")
                        .IsClosed = drResult("IsClosed")
                        .IsOriginalPriceGram = drResult("IsOriginalPriceGram")
                        .OriginalPriceGram = drResult("OriginalPriceGram")
                        .OriginalPriceTK = drResult("OriginalPriceTK")
                        .OriginalGemsPrice = drResult("OriginalGemsPrice")
                        .OriginalOtherPrice = drResult("OriginalOtherPrice")
                        .Photo = drResult("Photo")
                        .SellingPrice = drResult("SellingPrice")
                        .QTY = drResult("QTY")
                        .LossQTY = drResult("LossQTY")
                        .LossItemTK = drResult("LossItemTK")
                        .LossItemTG = drResult("LossItemTG")
                        .PurchaseWasteTG = drResult("PurchaseWasteTG")
                        .PurchaseWasteTK = drResult("PurchaseWasteTK")
                        .OrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .OriginalCode = drResult("OriginalCode")
                        .PriceCode = drResult("PriceCode")
                        .IsDiamond = drResult("IsDiamond")
                        .Color = drResult("Color")
                        .SellingRate = drResult("SellingRate")
                        .WSFixPrice = drResult("WSFixPrice")
                        .Shape = drResult("Shape")
                        .Clarity = drResult("Clarity")
                        .OriginalPriceCarat = drResult("OriginalPriceCarat")
                        .IsOriginalPriceCarat = drResult("IsOriginalPriceCarat")
                        .SDYOrCOrG = drResult("SDYOrCOrG")
                        .SDGemsTW = drResult("SDGemsTW")
                        .IsLooseDiamond = drResult("IsLooseDiamond")
                        .SDGemsCategoryID = drResult("SDGemsCategoryID")
                        .SDGemsName = drResult("SDGemsName")
                        .TotalCost = drResult("TotalCost")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function


        Public Function GetForSaleInfoByWSItemCode(ByVal ItemCode As String, ByVal argForSaleIDStr As String) As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleInfoByWSItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo
            Dim strWhere As String
            Try
                If argForSaleIDStr <> "" Then
                    strWhere = " and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND F.IsExit='0' AND F.ItemCode=@ItemCode AND F.ItemCode NOT IN (" & argForSaleIDStr & ")"
                Else
                    strWhere = "  and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND F.IsExit='0' AND F.ItemCode=@ItemCode"
                End If

                strCommandText = "Select ForSaleID,ItemCode,IsNull(N.ItemName,'-')as ItemName,Length,GoldQualityID,F.ItemCategoryID,GivenDate,F.LocationID, F.ItemNameID, F.QTY, "
                strCommandText += " CAST((ItemTK-GemsTK) AS INT) AS GoldK,"
                strCommandText += " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY,"
                strCommandText += " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC,"
                strCommandText += " (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " GemsTK,GemsTG,"
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,"
                strCommandText += " WasteTK,WasteTG,"
                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,"
                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                strCommandText += " ItemTK,ItemTG,"
                strCommandText += " CAST((ItemTK+WasteTK) AS INT) AS TotalK,"
                strCommandText += " CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,"
                strCommandText += " CAST((((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS TotalC,(ItemTK+WasteTK) AS TotalTK, (ItemTG+WasteTG) AS TotalTG,"
                strCommandText += " IsExit,IsNull(Width,'-') as Width,IsFixPrice,WSFixPrice as FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, LossQTY, LossItemTK, LossItemTG, IsClosed, PurchaseWasteTK, PurchaseWasteTG ,F.OrderReceiveDetailID, F.IsDiamond,F.OriginalCode,F.PriceCode,F.Color,F.TotalCost "
                strCommandText += " From tbl_ForSale F LEFT JOIN tbl_ItemName N ON F.ItemNameID=N.ItemNameID Where ItemCode = @ItemCode  And F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='0' AND IsClosed='0' " & strWhere & " Order by GivenDate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemName = drResult("ItemName")
                        .Length = drResult("Length")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemNameID = drResult("ItemNameID")
                        .LocationID = drResult("LocationID")
                        .GivenDate = drResult("GivenDate")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .GoldC = drResult("GoldC")

                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .GemsK = drResult("GemsK")
                        .GemsP = drResult("GemsP")
                        .GemsY = drResult("GemsY")
                        .GemsC = drResult("GemsC")

                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .WasteK = drResult("WasteK")
                        .WasteP = drResult("WasteP")
                        .WasteY = drResult("WasteY")
                        .WasteC = drResult("WasteC")

                        .ItemTG = drResult("ItemTG")
                        .ItemTK = drResult("ItemTK")
                        .ItemK = drResult("ItemK")
                        .ItemP = drResult("ItemP")
                        .ItemY = drResult("ItemY")
                        .ItemC = drResult("ItemC")

                        .TotalTK = drResult("TotalTK")
                        .TotalTG = drResult("TotalTG")
                        .TotalK = drResult("TotalK")
                        .TotalP = drResult("TotalP")
                        .TotalY = drResult("TotalY")
                        .TotalC = drResult("TotalC")

                        .IsExit = drResult("IsExit")
                        .Width = drResult("Width")
                        .DesignCharges = drResult("DesignCharges")
                        .PlatingCharges = drResult("PlatingCharges")
                        .MountingCharges = drResult("MountingCharges")
                        .WhiteCharges = drResult("WhiteCharges")
                        .IsFixPrice = drResult("IsFixPrice")
                        .FixPrice = drResult("FixPrice")
                        .IsOriginalFixedPrice = drResult("IsOriginalFixedPrice")
                        .OriginalFixedPrice = drResult("OriginalFixedPrice")
                        .IsClosed = drResult("IsClosed")
                        .IsOriginalPriceGram = drResult("IsOriginalPriceGram")
                        .OriginalPriceGram = drResult("OriginalPriceGram")
                        .OriginalPriceTK = drResult("OriginalPriceTK")
                        .OriginalGemsPrice = drResult("OriginalGemsPrice")
                        .OriginalOtherPrice = drResult("OriginalOtherPrice")
                        .Photo = drResult("Photo")
                        .SellingPrice = drResult("SellingPrice")
                        .QTY = drResult("QTY")
                        .LossQTY = drResult("LossQTY")
                        .LossItemTK = drResult("LossItemTK")
                        .LossItemTG = drResult("LossItemTG")
                        .PurchaseWasteTG = drResult("PurchaseWasteTG")
                        .PurchaseWasteTK = drResult("PurchaseWasteTK")
                        .OrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .OriginalCode = drResult("OriginalCode")
                        .PriceCode = drResult("PriceCode")
                        .IsDiamond = drResult("IsDiamond")
                        .Color = drResult("Color")
                        .TotalCost = drResult("TotalCost")


                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function



        Public Function GetForSaleDataByItemCode(ByVal StrCri As String) As System.Data.DataTable Implements ISalesItemDA.GetForSaleDataByItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Dim dtResult As DataTable
            Try
                strCommandText = "Select ForSaleID,ItemCode,IsNull(N.ItemName,'-')as ItemName,Length,GoldQualityID,F.ItemCategoryID,GivenDate,F.LocationID, F.ItemNameID, F.QTY, "
                strCommandText += " CAST((ItemTK-GemsTK) AS INT) AS GoldK,"
                strCommandText += " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP,"
                strCommandText += " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY,"
                strCommandText += " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC,"
                strCommandText += " (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,"
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,"
                strCommandText += " CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,"
                strCommandText += " GemsTK,GemsTG,"
                strCommandText += " CAST(WasteTK AS INT) AS WasteK,"
                strCommandText += " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,"
                strCommandText += " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,"
                strCommandText += " CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,"
                strCommandText += " WasteTK,WasteTG,"
                strCommandText += " CAST(ItemTK AS INT) AS ItemK,"
                strCommandText += " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,"
                strCommandText += " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,"
                strCommandText += " CAST(((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                strCommandText += " ItemTK,ItemTG,"
                strCommandText += " CAST((ItemTK+WasteTK) AS INT) AS TotalK,"
                strCommandText += " CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT) AS TotalP,"
                strCommandText += " CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,"
                strCommandText += " CAST((((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16)-CAST(((ItemTK+WasteTK)-CAST((ItemTK+WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS TotalC,(ItemTK+WasteTK) AS TotalTK, (ItemTK+WasteTK) AS TotalTG,"
                strCommandText += " IsExit,IsNull(Width,'-') as Width,IsFixPrice,FixPrice,DesignCharges,PlatingCharges,MountingCharges,WhiteCharges,IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, LossQTY, LossItemTK, LossItemTG, IsClosed, PurchaseWasteTK, PurchaseWasteTG ,F.OrderReceiveDetailID, F.IsDiamond,F.OriginalCode,F.PriceCode,F.Color,F.TotalCost "
                strCommandText += " From tbl_ForSale F LEFT JOIN tbl_ItemName N ON F.ItemNameID=N.ItemNameID Where " & StrCri
                DBComm = DB.GetSqlStringCommand(strCommandText)


                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function




        Public Function GetForSalesItemForOrderInvoice(ByVal OrderInvoiceID As String, Optional ByVal criExit As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSalesItemForOrderInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If criExit <> "" Then
                strWhere = " AND F.IsExit='0' AND F.ItemCode NOT IN (" & criExit & ") "
            Else
                strWhere = " AND F.IsExit='0'  "
            End If
            Try
                strCommandText = "Select F.IsDiamond AS [$IsDiamond], ForSaleID As [@ForSaleID], ItemCode, convert(varchar(10),F.GivenDate,105)as GivenDate, ItemCategory As [ItemCategory_], IsNull(N.ItemName,'-')as [ItemName_], GoldQuality AS [GoldQuality_], F.GoldQualityID AS [@GoldQualityID], F.ItemCategoryID AS [@ItemCategoryID], F.LocationID As [@LocationID], I.ItemTaxPer,"
                strCommandText += " CONVERT(VARCHAR,CAST(ItemTG AS DECIMAL(18,3))) AS ItemTG,"
                strCommandText += " CONVERT(VARCHAR,CAST(ItemTK AS INT)) AS ItemK,"
                strCommandText += " CONVERT(VARCHAR,CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT)) AS ItemP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS ItemY,"
                strCommandText += " CONVERT(VARCHAR,CAST((F.ItemTG-F.GemsTG) AS DECIMAL(18,3))) AS GoldTG, "
                strCommandText += " CONVERT(VARCHAR,CAST((F.ItemTK-F.GemsTK) AS INT)) AS GoldK,"
                strCommandText += " CONVERT(VARCHAR,CAST(((F.ItemTK-F.GemsTK) -CAST((F.ItemTK-F.GemsTK)  AS INT))*16 AS INT)) AS GoldP,"
                strCommandText += " CONVERT(VARCHAR,CAST(((((F.ItemTK-F.GemsTK) -CAST((F.ItemTK-F.GemsTK)  AS INT))*16)-CAST(((F.ItemTK-F.GemsTK) -CAST((F.ItemTK-F.GemsTK)  AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS GoldY, "
                strCommandText += " CONVERT(VARCHAR,CAST(F.GemsTG AS DECIMAL(18,3))) AS GemsTG,"
                strCommandText += " CONVERT(VARCHAR,CAST(F.GemsTK AS INT)) AS GemsK,"
                strCommandText += " CONVERT(VARCHAR,CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT)) AS GemsP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1))) AS GemsY,"
                strCommandText += " CONVERT(VARCHAR,CAST(F.WasteTG AS DECIMAL(18,3))) AS WasteTG,"
                strCommandText += " CONVERT(VARCHAR,CAST(F.WasteTK AS INT)) AS WasteK,"
                strCommandText += " CONVERT(VARCHAR,CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT)) AS WasteP,"
                strCommandText += " CONVERT(VARCHAR,CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY,"
                strCommandText += " F.IsFixPrice AS [@IsFixPrice], CONVERT(VARCHAR,F.FixPrice) As FixPrice, CONVERT(VARCHAR,F.DesignCharges) As DesignCharges, CONVERT(VARCHAR,F.PlatingCharges) AS PlatingCharges, CONVERT(VARCHAR,F.MountingCharges) AS MountingCharges,  CONVERT(VARCHAR,F.WhiteCharges) AS WhiteCharges, F.Length AS [Length_], F.Width As [Width_], F.GivenDate as [@GivenDate]  "
                strCommandText += " From tbl_ForSale F LEFT JOIN tbl_ItemName N ON F.ItemNameID=N.ItemNameID"
                strCommandText += " LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_OrderReceiveDetail O ON O.OrderReceiveDetailID=F.OrderReceiveDetailID "
                strCommandText += " LEFT JOIN tbl_GoldQuality Q ON Q.GoldQualityID=F.GoldQualityID "
                strCommandText += "Where O.OrderInvoiceID = @OrderInvoiceID AND IsOrder=1 AND IsClosed=0 And F.IsDelete=0 " & strWhere & " Order by [@GivenDate] DESC, ForSaleID DESC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Function UpdateSaleItemIsClose(Obj As SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateSaleItemIsClose
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ForSale set IsClosed=@IsClosed "
                strCommandText += " where ForSaleID=@ForSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsClosed", DbType.Boolean, Obj.IsClosed)

                'DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, IIf(Obj.ExitDate <> Nothing, Obj.ExitDate, DBNull.Value))
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

        Public Function UpdateOrderInvoiceReceiveForIsBarcodeNo(ByVal OrderReceiveDetailID As String, ByVal IsBarcode As Boolean) As Boolean Implements ISalesItemDA.UpdateOrderInvoiceReceiveForIsBarcodeNo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_OrderReceiveDetail set IsBarcode = @IsBarcode"
                strCommandText += " where OrderReceiveDetailID= @OrderReceiveDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@IsBarcode", DbType.Boolean, IsBarcode)
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

        Public Function GetForSaleInfoByItemCodeANDOrderInvoiceID(ByVal ItemCode As String, ByVal OrderInvoiceID As String, Optional ByVal criExit As String = "") As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleInfoByItemCodeANDOrderInvoiceID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo
            Try
                strCommandText = "Select ForSaleID,ItemCode,IsNull(N.ItemName,'-')as ItemName, F.Length, F.GoldQualityID, F.ItemCategoryID, GivenDate, F.LocationID,"
                strCommandText += " (F.ItemTK-F.GemsTK) AS GoldTK, (F.ItemTG-F.GemsTG) AS GoldTG, F.GemsTK, F.GemsTG, F.WasteTK, F.WasteTG, F.ItemTK, F.ItemTG, (F.ItemTK+F.WasteTK) AS TotalTK, (F.ItemTG+F.WasteTG) AS TotalTG, F.PurchaseWasteTK, F.PurchaseWasteTG,"
                strCommandText += " IsExit,IsNull(F.Width,'-') as Width, F.IsFixPrice, F.FixPrice, F.DesignCharges, F.PlatingCharges, F.MountingCharges, F.WhiteCharges, IsOriginalFixedPrice,OriginalFixedPrice,IsOriginalPriceGram,OriginalPriceTK,OriginalPriceGram,OriginalGemsPrice,OriginalOtherPrice,Photo ,SellingPrice, PurchaseWasteTK, PurchaseWasteTG, F.OrderReceiveDetailID, F.IsDiamond ,F.Color"
                strCommandText += " From tbl_ForSale  F LEFT JOIN tbl_OrderReceiveDetail D ON D.OrderReceiveDetailID=F.OrderReceiveDetailID LEFT JOIN tbl_ItemName N ON F.ItemNameID=N.ItemNameID Where ItemCode = @ItemCode AND D.OrderInvoiceID = @OrderInvoiceID AND IsClosed=0 AND F.IsDelete=0" & criExit & " Order by GivenDate DESC, F.ForSaleID DESC "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemName = drResult("ItemName")
                        .Length = drResult("Length")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .LocationID = drResult("LocationID")
                        .GivenDate = drResult("GivenDate")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GemsTK = drResult("GemsTK")
                        .GemsTG = drResult("GemsTG")
                        .WasteTK = drResult("WasteTK")
                        .WasteTG = drResult("WasteTG")
                        .ItemTK = drResult("ItemTK")
                        .ItemTG = drResult("ItemTG")
                        .TotalTK = drResult("TotalTK")
                        .TotalTG = drResult("TotalTG")
                        .PurchaseWasteTG = drResult("PurchaseWasteTG")
                        .PurchaseWasteTK = drResult("PurchaseWasteTK")
                        .IsExit = drResult("IsExit")
                        .Width = drResult("Width")
                        .DesignCharges = drResult("DesignCharges")
                        .PlatingCharges = drResult("PlatingCharges")
                        .MountingCharges = drResult("MountingCharges")
                        .WhiteCharges = drResult("WhiteCharges")
                        .IsFixPrice = drResult("IsFixPrice")
                        .FixPrice = drResult("FixPrice")
                        .IsOriginalFixedPrice = drResult("IsOriginalFixedPrice")
                        .OriginalFixedPrice = drResult("OriginalFixedPrice")
                        .IsOriginalPriceGram = drResult("IsOriginalPriceGram")
                        .OriginalPriceGram = drResult("OriginalPriceGram")
                        .OriginalPriceTK = drResult("OriginalPriceTK")
                        .OriginalGemsPrice = drResult("OriginalGemsPrice")
                        .OriginalOtherPrice = drResult("OriginalOtherPrice")
                        .OrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .Photo = drResult("Photo")
                        .SellingPrice = drResult("SellingPrice")
                        .OrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .IsDiamond = drResult("IsDiamond")
                        .Color = drResult("Color")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
        Public Function UpdateLossQTYandGramByForSaleID(ByVal ForSaleID As String, ByVal QTY As Integer, ByVal ItemTK As Decimal, ByVal ItemTG As Decimal, ByVal opt As String, Optional ByVal cristr As String = "") As Boolean Implements ISalesItemDA.UpdateLossQTYandGramByForSaleID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ForSale set LossQTY = LossQTY " & opt & " @QTY , LossItemTK = LossItemTK " & opt & " @ItemTK , LossItemTG = LossItemTG " & opt & " @ItemTG "
                strCommandText += " where ForSaleID= @ForSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, QTY)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, ItemTG)

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
        Public Function UpdateSalesItemByQTYandWeight(ByVal ForSaleID As String, ByVal QTY As Integer, ByVal ItemTK As Decimal, ByVal ItemTG As Decimal, ByVal opt As String, Optional ByVal cristr As String = "") As Boolean Implements ISalesItemDA.UpdateSalesItemByQTYandWeight
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ForSale set QTY = QTY " & opt & " @QTY , ItemTK = ItemTK " & opt & " @ItemTK , ItemTG = ItemTG " & opt & " @ItemTG "
                strCommandText += " where ForSaleID= @ForSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ForSaleID)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, QTY)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, ItemTG)

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
        Public Function GetForSaleForReportByTotalWeight(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleForReportByTotalWeight
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT Count(H.ForSaleID) As TotalQTY, Sum(ItemTK) AS ItemTK, Sum(ItemTK-GemsTK) As GoldTK, " & _
                                " Sum(WasteTK) As WasteTK, Sum(GemsTK)  As GemsTK, Sum(CAST((ItemTG-GemsTG) as DECIMAL(18,3))) As GoldTG," & _
                                " Sum(Cast(GemsTG as DECIMAL(18,3))) AS GemsTG, Sum(Cast(WasteTG as DECIMAL(18,3))) AS WasteTG, " & _
                                " Sum(CAST(ItemTG AS DECIMAL(18,3))) as ItemTG ,Sum(FixPrice) as TotalFixPrice " & _
                                " FROM tbl_ForSale H  " & _
                               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID" & _
                                " WHERE 1=1 AND H.IsDelete=0 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleForReportByDatePeriodAndTotalWeight(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleForReportByDatePeriodAndTotalWeight
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT Count(H.ForSaleID) As TotalQTY, Sum(ItemTK) AS ItemTK, Sum(CAST(ItemTG AS DECIMAL(18,3))) as ItemTG, Sum(CAST((ItemTG-GemsTG) as DECIMAL(18,3))) As GoldTG, Sum((ItemTK-GemsTK)) As GoldTK, " & _
                                  " Sum(Cast(WasteTG as DECIMAL(18,3))) AS WasteTG, Sum(WasteTK) As WasteTK, Sum(GemsTK)  As GemsTK, Sum(Cast(GemsTG as DECIMAL(18,3))) AS GemsTG,Sum(FixPrice) as TotalFixPrice " & _
                                  " FROM tbl_ForSale H  " & _
                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID " & _
                                   " WHERE H.IsDelete=0  And H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr

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

        Public Function GetForSaleVolumeForReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleVolumeForReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT H.ForSaleID, H.ItemCode, H.ItemNameID, I.ItemName, H.Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, " & _
                                 " ItemCategory, GivenDate,  IsExit,OriginalGemsPrice, ExitDate, H.ItemTK, H.ItemTG, Width, H.IsFixPrice, H.FixPrice,  " & _
                                 " IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK,  OriginalGemsPrice, " & _
                                 " OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, " & _
                                 " H.LossItemTG " & _
                                 " FROM tbl_ForSale H  " & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID  LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID   " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID   LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID WHERE 1=1 And H.IsDelete=0  " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleLooseDiamondForReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleLooseDiamondForReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT H.ForSaleID, H.ItemCode,H.SDGemsCategoryID,IsLooseDiamond,Color,Shape,Clarity,SDYOrCOrG,OriginalPriceCarat,IsOriginalPriceCarat,SDGemsTW,TotalCost, " & _
                                 " SDGemsName, GivenDate,  IsExit,OriginalGemsPrice, ExitDate, H.ItemTK, H.ItemTG, Width, H.IsFixPrice, H.FixPrice,  " & _
                                 " IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK,  OriginalGemsPrice, " & _
                                 " OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, " & _
                                 " H.LossItemTG,C.GemsCategory " & _
                                 " FROM tbl_ForSale H  " & _
                                 " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=H.SDGemsCategoryID   LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID WHERE 1=1 And H.IsDelete=0  " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleVolumeForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleVolumeForReportByDatePeriod
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT H.ForSaleID, H.ItemCode, H.ItemNameID, I.ItemName, H.Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, " & _
                              " ItemCategory, GivenDate,  IsExit,OriginalGemsPrice, ExitDate, H.ItemTK, H.ItemTG, Width, H.IsFixPrice, H.FixPrice,  " & _
                              " IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK,  OriginalGemsPrice, " & _
                              " OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, " & _
                              " H.LossItemTG " & _
                              " FROM tbl_ForSale H  " & _
                              " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID  LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID   " & _
                              " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID   LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                              " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID WHERE H.IsDelete=0 AND H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr

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
        Public Function GetForSaleLooseDiamondForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleLooseDiamondForReportByDatePeriod
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT H.ForSaleID, H.ItemCode,C.GemsCategory," & _
                              "  GivenDate,  IsExit,OriginalGemsPrice, ExitDate, H.ItemTK, H.ItemTG, Width, H.IsFixPrice, H.FixPrice,WSFixPrice,  " & _
                              " IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK,  OriginalGemsPrice, " & _
                              " OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.IsLooseDiamond, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, " & _
                              " H.LossItemTG,H.SDGemsName,H.SDGemsCategoryID,H.Color,H.Shape,H.Clarity,H.SDYOrCOrG,H.TotalCost,IsOriginalPriceCarat,OriginalPriceCarat " & _
                              " FROM tbl_ForSale H  " & _
                              " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=H.SDGemsCategoryID   LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                              " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID WHERE H.IsDelete=0 AND H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr

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

        Public Function GetForSaleDataBySummaryReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleDataBySummaryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                                " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) As GoldTK,(ItemTG-GemsTG) As GoldTG,  H.GemsTK, H.GemsTG, TotalTK,  WasteTK , WasteTG, TotalTG, ItemTK,  CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                                 " CAST(ItemTK AS INT) AS ItemK," & _
                                " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                " CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP,  " & _
                                " CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY," & _
                                " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                                " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG, " & _
                                " CONVERT(VARCHAR,CAST(H.WasteTK AS INT)) AS WasteK," & _
                               " CONVERT(VARCHAR,CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT)) AS WasteP," & _
                               " CONVERT(VARCHAR,CAST((((H.WasteTK-CAST(H.WasteTK AS INT))*16)-CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY " & _
                                " FROM tbl_ForSale H  " & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                " WHERE 1=1 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleDataBySummaryReportByLocation(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleDataBySummaryReportByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = "SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate,  IsExit," & _
                '                 " H.OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) As GoldTK,(ItemTG-GemsTG) As GoldTG,  H.GemsTK, H.GemsTG, TotalTK,  WasteTK , WasteTG, TotalTG, ItemTK,  " & _
                '                 " CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, H.FixPrice, DesignCharges,   CAST(ItemTK AS INT) AS ItemK, " & _
                '                 " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'8.0' AS " & _
                '                 " DECIMAL(18,2)) AS ItemY, CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS " & _
                '                 " PurchaseWasteP,   CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS " & _
                '                 " DECIMAL(18,2)) AS PurchaseWasteY, PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, H.OriginalFixedPrice, IsOriginalPriceGram, " & _
                '                 " H.OriginalPriceGram, H.OriginalPriceTK,  H.OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, " & _
                '                 " H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG  FROM tbl_Transfer T" & _
                '                 " Left Join tbl_TransferItem TI on TI.TransferID=T.TransferID   " & _
                '                 " left Join tbl_ForSale H on H.ForSaleID=TI.Forsaleid " & _
                '                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID  " & _
                '                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID   " & _
                '                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID   " & _
                '                 " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                '                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  WHERE T.IsDelete =0 " & cristr & _
                '                 " AND TI.ForSaleID NOT IN   (SELECT ForSaleID FROM tbl_SaleInvoiceDetail SD " & _
                '                 " INNER JOIN tbl_SaleInvoiceHeader SI  on SD.SaleInvoiceHeaderID=SI.SaleInvoiceHeaderID Where SI.isDelete=0 And SI.LocationID='" & LocationID & "' )" & _
                '                 " AND TI.ForSaleID NOT IN (SELECT ForSaleID FROM tbl_WholesaleInvoiceItem WI " & _
                '                 " INNER JOIN tbl_WholeSaleInvoice W on W.WholeSaleInvoiceID=WI.WholesaleInvoiceID  Where W.isDelete=0 And W.LocationID='" & LocationID & "' And  WI.IsReturn=0) " & _
                '                 " AND TI.ForSaleID NOT IN (SELECT ForSaleID from tbl_OrderReturnDetail D Inner Join tbl_OrderReturnHeader H on D.OrderReturnHeaderID=H.OrderReturnHeaderID" & _
                '                 " Where H.Isdelete=0 And H.LocationID='" & LocationID & "')"

                strCommandText = "SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate,  IsExit," & _
                                 " H.OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) As GoldTK,(ItemTG-GemsTG) As GoldTG,  H.GemsTK, H.GemsTG, TotalTK,  WasteTK , WasteTG, TotalTG, ItemTK,  " & _
                                 " CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, H.FixPrice, DesignCharges,   CAST(ItemTK AS INT) AS ItemK, " & _
                                 " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'8.0' AS " & _
                                 " DECIMAL(18,2)) AS ItemY, CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT) AS " & _
                                 " PurchaseWasteP,   CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS " & _
                                 " DECIMAL(18,2)) AS PurchaseWasteY, PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, H.OriginalFixedPrice, IsOriginalPriceGram, " & _
                                 " H.OriginalPriceGram, H.OriginalPriceTK,  H.OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, " & _
                                 " H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG From tbl_ForSale H " & _
                                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID  " & _
                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID   " & _
                                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID   " & _
                                 " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID  " & _
                                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  WHERE " & cristr & _
                                 " And H.LocationID='" & LocationID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleDataBySummaryIsCloseReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleDataBySummaryIsCloseReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                                " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) As GoldTK,(ItemTG-GemsTG) As GoldTG,  H.GemsTK, H.GemsTG, TotalTK,  WasteTK , WasteTG, TotalTG, ItemTK,  CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                                 " CAST(ItemTK AS INT) AS ItemK," & _
                                " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                                " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG " & _
                                " FROM tbl_ForSale H  " & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                " WHERE 1=1 And H.ExitDate Between @FromDate And @ToDate " & cristr & " Order By ExitDate asc"

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

        Public Function GetForSaleForSummaryReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional Status As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSaleForSummaryReportByDatePeriod
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                '                 " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,  H.GemsTK, TotalTK,  WasteTK , H.GemsTG , WasteTG, TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                '                 " CAST(ItemTK AS INT) AS ItemK, " & _
                '                 " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP," & _
                '                 " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                '                 " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                '                 " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG,H.OriginalCode " & _
                '                 " FROM tbl_ForSale H  " & _
                '                 " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                '                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                '                 " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                '                 " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                '                 " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                '                 " WHERE H.IsDelete=0 And H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr
                Select Case Status
                    Case "All"
                        strCommandText = " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                               " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,  H.GemsTK, TotalTK,  WasteTK , H.GemsTG , WasteTG, TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                               " CAST(ItemTK AS INT) AS ItemK, " & _
                               " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP," & _
                               " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                               " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                               " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG,H.OriginalCode, " & _
                               " CONVERT(VARCHAR,CAST(H.WasteTK AS INT)) AS WasteK," & _
                               " CONVERT(VARCHAR,CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT)) AS WasteP," & _
                               " CONVERT(VARCHAR,CAST((((H.WasteTK-CAST(H.WasteTK AS INT))*16)-CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY " & _
                               " FROM tbl_ForSale H  " & _
                               " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                               " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                               " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                               " WHERE H.IsDelete=0 And H.GivenDate BETWEEN @FromDate And @ToDate " & cristr
                    Case "Exit"
                        strCommandText = " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                               " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,  H.GemsTK, TotalTK,  WasteTK , H.GemsTG , WasteTG, TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                               " CAST(ItemTK AS INT) AS ItemK, " & _
                               " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP," & _
                               " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                               " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                               " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG,H.OriginalCode, " & _
                               " CONVERT(VARCHAR,CAST(H.WasteTK AS INT)) AS WasteK," & _
                               " CONVERT(VARCHAR,CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT)) AS WasteP," & _
                               " CONVERT(VARCHAR,CAST((((H.WasteTK-CAST(H.WasteTK AS INT))*16)-CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY " & _
                               " FROM tbl_ForSale H  " & _
                               " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                               " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                               " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                               " WHERE H.IsDelete=0 And H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & " And H.ExitDate BETWEEN @FromDate And @ToDate And H.isClosed=0"
                    Case "Balance"
                        strCommandText = " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                               " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,  H.GemsTK, TotalTK,  WasteTK , H.GemsTG , WasteTG, TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                               " CAST(ItemTK AS INT) AS ItemK, " & _
                               " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP," & _
                               " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                               " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                               " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG,H.OriginalCode, " & _
                               " CONVERT(VARCHAR,CAST(H.WasteTK AS INT)) AS WasteK," & _
                               " CONVERT(VARCHAR,CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT)) AS WasteP," & _
                               " CONVERT(VARCHAR,CAST((((H.WasteTK-CAST(H.WasteTK AS INT))*16)-CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY " & _
                               " FROM tbl_ForSale H  " & _
                               " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                               " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                               " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                               " WHERE H.IsDelete=0 And H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & " And H.IsExit<>1 And H.isDelete=0" & _
                               " Union All" & _
                               " SELECT H.ForSaleID, ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory, GivenDate, " & _
                               " IsExit,OriginalGemsPrice, ExitDate, (ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,  H.GemsTK, TotalTK,  WasteTK , H.GemsTG , WasteTG, TotalTG, ItemTK, CAST(ItemTG as DECIMAL(18,3)) as ItemTG, Width, H.IsFixPrice, FixPrice, DesignCharges,  " & _
                               " CAST(ItemTK AS INT) AS ItemK, " & _
                               " CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP," & _
                               " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                               " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                               " OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG,H.OriginalCode, " & _
                                " CONVERT(VARCHAR,CAST(H.WasteTK AS INT)) AS WasteK," & _
                               " CONVERT(VARCHAR,CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT)) AS WasteP," & _
                               " CONVERT(VARCHAR,CAST((((H.WasteTK-CAST(H.WasteTK AS INT))*16)-CAST((H.WasteTK-CAST(H.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2))) AS WasteY " & _
                               " FROM tbl_ForSale H  " & _
                               " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                               " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  " & _
                               " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID  " & _
                               " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                               " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                               " WHERE H.IsDelete=0 And H.GivenDate BETWEEN @FromDate And @ToDate  " & cristr & " And H.IsExit=1 And ExitDate> @ToDate "
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

        Public Function UpdateSaleItemIsOrder(Obj As SalesItemInfo) As Boolean Implements ISalesItemDA.UpdateSaleItemIsOrder
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_ForSale set IsOrder=@IsOrder "
                strCommandText += " where ForSaleID=@ForSaleID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, Obj.ForSaleID)
                DB.AddInParameter(DBComm, "@IsOrder", DbType.Boolean, Obj.IsOrder)
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

        Public Function UpdateForSaleItemByOrderInvoiceID(ByVal OrderReceiveDetailID As String) As Boolean Implements ISalesItemDA.UpdateForSaleItemByOrderInvoiceID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ForSale set IsOrder=0, OrderReceiveDetailID=''  "
                strCommandText += " where OrderReceiveDetailID= @OrderReceiveDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, OrderReceiveDetailID)
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

        Public Function GetForSalesVolumeItemForSaleInvoice(Optional ByVal Itemcode As String = "", Optional ByVal CheckState As Boolean = False) As System.Data.DataTable Implements ISalesItemDA.GetForSalesVolumeItemForSaleInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If Itemcode <> "" Then
                strWhere = " and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND F.ItemCode NOT IN (" & Itemcode & ")"
            Else
                strWhere = " and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND 1=1 "
            End If
            Try
                If CheckState = True Then
                    strCommandText = "SELECT F.ForSaleID AS [@ForSaleID], F.ItemCode,CONVERT(VARCHAR(10),F.GivenDate,105) as GivenDate , F.ItemCategoryID AS [@ItemCategoryID],I.ItemCategory as [ItemCategory_],N.ItemName as [ItemName_],F.Length as [Length_],IsNull(F.Width,'') as [Width_],"
                    strCommandText += "F.GoldQualityID AS [@GoldQualityID],G.GoldQuality AS [GoldQuality_], LossQTY As QTY, "
                    strCommandText += " CAST(LossItemTK AS INT) AS ItemK,"
                    strCommandText += " CAST((LossItemTK-CAST(LossItemTK AS INT))*16 AS INT) AS ItemP,"
                    strCommandText += " CAST((((LossItemTK-CAST(LossItemTK AS INT))*16)-CAST((LossItemTK-CAST(LossItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS ItemY,"
                    strCommandText += " CAST(((((LossItemTK-CAST(LossItemTK AS INT))*16)-CAST((LossItemTK-CAST(LossItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((LossItemTK-CAST(LossItemTK AS INT))*16)-CAST((LossItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                    strCommandText += " LossItemTK, LossItemTG,"
                    strCommandText += " F.IsExit AS [@IsExit], "
                    strCommandText += " F.IsFixPrice AS [@IsFixPrice], F.FixPrice, Photo, F.GivenDate As [@GDate]"
                    strCommandText += " FROM tbl_ForSale F INNER JOIN tbl_GoldQuality G ON F.GoldQualityID = G.GoldQualityID "
                    strCommandText += " INNER JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID "
                    strCommandText += " INNER JOIN tbl_ItemCategory I ON F.ItemCategoryID = I.ItemCategoryID where F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='0' And IsSolidVolume='1' AND IsClosed='0'  " & strWhere & " And F.IsDelete=0 Order By [@GDate] DESC"
                Else
                    strCommandText = "SELECT F.ForSaleID AS [@ForSaleID], F.ItemCode,CONVERT(VARCHAR(10),F.GivenDate,105) as GivenDate , F.ItemCategoryID AS [@ItemCategoryID],I.ItemCategory as [ItemCategory_],N.ItemName as [ItemName_],F.Length as [Length_],IsNull(F.Width,'') as [Width_],"
                    strCommandText += "F.GoldQualityID AS [@GoldQualityID],G.GoldQuality AS [GoldQuality_], LossQTY As QTY, "
                    strCommandText += " CAST(LossItemTK AS INT) AS ItemK,"
                    strCommandText += " CAST((LossItemTK-CAST(LossItemTK AS INT))*16 AS INT) AS ItemP,"
                    strCommandText += " CAST((((LossItemTK-CAST(LossItemTK AS INT))*16)-CAST((LossItemTK-CAST(LossItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS ItemY,"
                    strCommandText += " CAST(((((LossItemTK-CAST(LossItemTK AS INT))*16)-CAST((LossItemTK-CAST(LossItemTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((LossItemTK-CAST(LossItemTK AS INT))*16)-CAST((LossItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS ItemC,"
                    strCommandText += " LossItemTK, LossItemTG,"
                    strCommandText += " F.IsExit AS [@IsExit], "
                    strCommandText += " F.IsFixPrice AS [@IsFixPrice], F.FixPrice, Photo, F.GivenDate As [@GDate]"
                    strCommandText += " FROM tbl_ForSale F INNER JOIN tbl_GoldQuality G ON F.GoldQualityID = G.GoldQualityID "
                    strCommandText += " INNER JOIN tbl_ItemName N ON N.ItemNameID=F.ItemNameID "
                    strCommandText += " INNER JOIN tbl_ItemCategory I ON F.ItemCategoryID = I.ItemCategoryID where F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='1' AND IsClosed='0'  " & strWhere & " And F.IsDelete=0 Order By [@GDate] DESC"
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleItemDataByItemCodeAndForSaleID(ByVal ItemCode As String, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetSaleItemDataByItemCodeAndForSaleID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT  H.* FROM tbl_ForSale H WHERE H.ItemCode=@ItemCode and H.IsDelete=0 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleDataByItemCodeAndIsExit(ItemCode As String, IsExit As Boolean, Optional cristr As String = "") As DataTable Implements ISalesItemDA.GetForSaleDataByItemCodeAndIsExit
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select ForSaleID,ItemCode,GivenDate,IsExit from tbl_ForSale Where ItemCode = @ItemCode and IsExit = @IsExit And IsDelete=0 " & cristr & ""

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, IsExit)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateStockForReuse(obj As SalesItemInfo, ByVal TempCode As String) As Boolean Implements ISalesItemDA.UpdateStockForReuse
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                strCommandText = "UPDATE tbl_ForSale SET ItemCode = @TempCode" & _
              " WHERE ItemCode=@ItemCode and IsExit = @IsExit"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, True)
                DB.AddInParameter(DBComm, "@TempCode", DbType.String, TempCode)
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

        Public Function GetSaleItemDataByOrderReceiveDetailID(OrderReceiveDetailID As String) As DataTable Implements ISalesItemDA.GetSaleItemDataByOrderReceiveDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select * from tbl_ForSale Where OrderReceiveDetailID=@OrderReceiveDetailID And IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, OrderReceiveDetailID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetSaleInvoiceForRepair(Optional cristr As String = "", Optional ByVal BarcodeNo As String = "") As DataTable Implements ISalesItemDA.GetSaleInvoiceForRepair
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            If BarcodeNo <> "" Then
                cristr = cristr & " AND M.BarcodeNo NOT IN (" & BarcodeNo & ")"
            End If
            Try
                strCommandText = " SELECT M.* FROM " & _
                                 " (Select  D.SaleInvoiceDetailID As [@SaleInvoiceDetailID],D.SaleInvoiceHeaderID As VoucherNo, convert(varchar(10),SaleDate,105) as SaleDate,D.ItemCode As BarcodeNo, D.ForSaleID As [@ForSaleID]," & _
                                 " I.ItemCategory AS [ItemCategory_], N.ItemName As [ItemName_], G.GoldQuality AS [GoldQuality_], F.ItemNameID As [@ItemNameID],F.ItemCategoryID AS [@ItemCategoryID],F.GoldQualityID AS [@GoldQualityID],F.Length AS [Length_], F.Width AS [Width_], " & _
                                 " CAST(D.ItemTK AS INT) AS ItemK,CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,  CAST(F.ItemTG AS Decimal(18,3)) AS ItemTG, F.ItemTK AS [@ItemTK], F.ItemTG AS [@ItemTG], H.CustomerID  AS [@CustomerID] " & _
                                 " From tbl_SaleInvoiceDetail D LEFT JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                 " LEFT JOIN tbl_ForSale F on F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID  " & _
                                 " WHERE H.IsCancel=0 And D.IsReturn=0 AND F.IsDelete=0 AND I.IsDelete=0 AND N.IsDelete=0 AND G.IsDelete=0 " & _
                                 " UNION ALL " & _
                                 " Select  OD.OrderInvoiceDetailID As [@SaleInvoiceDetailID], OH.OrderInvoiceID As VoucherNo, convert(varchar(10),ReturnDate,105) as SaleDate,OD.ItemCode As BarcodeNo, OD.ForSaleID As [@ForSaleID], " & _
                                 " I.ItemCategory AS [ItemCategory_], N.ItemName As [ItemName_], G.GoldQuality AS [GoldQuality_], F.ItemNameID As [@ItemNameID],F.ItemCategoryID AS [@ItemCategoryID],F.GoldQualityID AS [@GoldQualityID],F.Length AS [Length_], F.Width AS [Width_], " & _
                                 " CAST(F.ItemTK AS INT) AS ItemK,CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, CAST(F.ItemTG AS Decimal(18,3)) AS ItemTG, F.ItemTK AS [@ItemTK], F.ItemTG AS [@ItemTG], R.CustomerID AS [@CustomerID] " & _
                                 " From tbl_OrderReturnDetail OD LEFT JOIN tbl_OrderReturnHeader OH on OH.OrderReturnHeaderID=OD.OrderReturnHeaderID " & _
                                 " LEFT JOIN tbl_OrderInvoice  R ON R.OrderInvoiceID=OH.OrderInvoiceID " & _
                                 " LEFT JOIN tbl_ForSale F on F.ForSaleID=OD.ForSaleID LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID " & _
                                 " LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID  " & _
                                 " WHERE OD.IsReturn=0 And OH.IsDelete=0 And R.IsDelete=0 AND F.IsDelete=0) AS M WHERE 1=1 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetSaleInvoiceObjDataForRepair(ByVal cristr As String) As SalesInvoiceDetailInfo Implements ISalesItemDA.GetSaleInvoiceObjDataForRepair
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesInvoiceDetailInfo
            Try
                strCommandText = " SELECT M.* FROM " & _
                                 " (Select  D.ItemCode, F.ItemNameID, F.ItemCategoryID,F.GoldQualityID,F.Length,F.Width, H.CustomerID, " & _
                                 " CAST(D.ItemTK AS INT) AS ItemK,CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, D.ItemTK, D.ItemTG " & _
                                 " From tbl_SaleInvoiceDetail D LEFT JOIN tbl_SaleInvoiceHeader H on H.SaleInvoiceHeaderID=D.SaleInvoiceHeaderID " & _
                                 " LEFT JOIN tbl_ForSale F on F.ForSaleID=D.ForSaleID " & _
                                 " WHERE H.IsCancel=0 AND H.IsDelete=0 AND F.IsDelete=0 " & _
                                 " UNION ALL " & _
                                 " Select  D.ItemCode, F.ItemNameID, F.ItemCategoryID, F.GoldQualityID, F.Length, F.Width, R.CustomerID, " & _
                                 " CAST(F.ItemTK AS INT) AS ItemK,CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, F.ItemTK, F.ItemTG " & _
                                 " From tbl_OrderReturnDetail D LEFT JOIN tbl_OrderReturnHeader H on H.OrderReturnHeaderID=D.OrderReturnHeaderID " & _
                                 " LEFT JOIN tbl_OrderInvoice R ON R.OrderInvoiceID=H.OrderInvoiceID " & _
                                 " LEFT JOIN tbl_ForSale F on F.ForSaleID=D.ForSaleID" & _
                                 " Where R.IsDelete=0 AND F.IsDelete=0 AND H.IsDelete=0) AS M WHERE 1=1" & cristr


                DBComm = DB.GetSqlStringCommand(strCommandText)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ItemCode = drResult("ItemCode")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemNameID = drResult("ItemNameID")
                        .Length = drResult("Length")
                        .Width = drResult("Width")
                        .ItemTK = drResult("ItemTK")
                        .ItemTG = drResult("ItemTG")
                        .ItemK = drResult("ItemK")
                        .ItemP = drResult("ItemP")
                        .ItemY = drResult("ItemY")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetBalanceStockForDate(ByVal FromDate As Date, Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesItemDA.GetBalanceStockForDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim lastday As Date = FromDate.Subtract(New TimeSpan(1)).Date
            Dim TomDate As Date = FromDate.AddDays(1)
            Try
                strCommandText = " Select H.ItemCategoryID,H.GoldQualityID, ItemCategory, GoldQuality, IsNull(TotalStock,0) As TotalStock, IsNull(TotalSaleStock,0) As TotalSaleStock, " & _
                                " (Isnull(TotalOpeningStock,0)-Isnull(BackStock,0)) AS TotalOpeningStock, (((Isnull(TotalOpeningStock,0)-Isnull(BackStock,0))+IsNull(TodayEntry,0))-IsNull(TodaySaleStock,0)) AS FinalStock," & _
                                " IsNull(BackStock,0) AS BackStock, IsNull(TodayEntry,0) AS TodayEntry, IsNull(TodaySaleStock,0) AS TodaySaleStock  from " & _
                                " (select F.ItemCategoryID,F.GoldQualityID,Count(ForSaleID) as TotalStock from tbl_ForSale F WHERE IsVolume=0 AND F.IsDelete=0 AND F.GivenDate <= @ToDate And F.LocationID=" & LocationID & _
                                " Group By ItemCategoryID,GoldQualityID) as H" & _
                                " LEFT JOIN " & _
                                " (select F.ItemCategoryID,F.GoldQualityID,Count(ForSaleID) as TotalSaleStock from tbl_ForSale F WHERE IsVolume=0 AND F.IsDelete=0 AND F.ExitDate <= @ToDate  And F.LocationID=" & LocationID & _
                                " Group By ItemCategoryID,GoldQualityID) as F" & _
                                " On H.ItemCategoryID = F.ItemCategoryID And H.GoldQualityID = F.GoldQualityID " & _
                                " LEFT JOIN " & _
                                " (select F.ItemCategoryID,F.GoldQualityID,Count(ForSaleID) as TotalOpeningStock from tbl_ForSale F Where IsVolume=0 AND F.IsDelete=0 AND F.GivenDate <= @lastday And F.LocationID = " & LocationID & _
                                " Group By ItemCategoryID,GoldQualityID) as B " & _
                                " On H.ItemCategoryID = B.ItemCategoryID And H.GoldQualityID = B.GoldQualityID " & _
                                " LEFT JOIN " & _
                                " (select F.ItemCategoryID,F.GoldQualityID,Count(F.ForSaleID) as BackStock from  tbl_ForSale F " & _
                                " Where F.IsDelete=0 AND IsVolume=0 AND F.GivenDate <= @lastday And F.ExitDate <= @lastday And F.LocationID=" & LocationID & _
                                " Group By ItemCategoryID,GoldQualityID) as C" & _
                                " On H.ItemCategoryID = C.ItemCategoryID  And H.GoldQualityID = C.GoldQualityID  " & _
                                " LEFT JOIN " & _
                                " (select F.ItemCategoryID,F.GoldQualityID,Count(ForSaleID) as TodayEntry from tbl_ForSale F Where IsVolume=0 AND F.IsDelete=0 AND F.GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & _
                                " Group By ItemCategoryID,GoldQualityID) as D" & _
                                " On H.ItemCategoryID = D.ItemCategoryID And H.GoldQualityID = D.GoldQualityID" & _
                                " LEFT JOIN " & _
                                " (select F.ItemCategoryID,F.GoldQualityID,Count(ForSaleID) as TodaySaleStock from tbl_ForSale F Where IsVolume=0 AND F.IsDelete=0 AND F.ExitDate Between @FromDate And @ToDate  And F.LocationID=" & LocationID & _
                                " Group By ItemCategoryID,GoldQualityID) as E" & _
                                " On H.ItemCategoryID = E.ItemCategoryID And H.GoldQualityID = E.GoldQualityID" & _
                                " Left Join tbl_ItemCategory I On H.ItemCategoryID = I.ItemCategoryID Left Join tbl_GoldQuality G " & _
                                " On H.GoldQualityID = G.GoldQualityID WHERE 1=1 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(FromDate.Date & " 23:59:59"))
                DB.AddInParameter(DBComm, "@lastday", DbType.DateTime, CDate(lastday.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetStockItemCardReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemDA.GetStockItemCardReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT F.ForSaleID, F.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.Width, F.GoldQualityID, GoldQuality, F.ItemCategoryID, ItemCategory, F.GivenDate, CAST((F.ItemTG-F.GemsTG) as DECIMAL(18,3)) as GoldTG, CAST(F.GemsTG AS DECIMAL(18,3)) as GemsTG , CAST(F.ItemTG as DECIMAL(18,3)) as ItemTG, CAST(F.WasteTG as DECIMAL(18,3)) as WasteTG , " & _
                " F.IsExit, F.OriginalGemsPrice, F.ExitDate, (F.ItemTK-F.GemsTK) AS GoldTK,  F.GemsTK, F.TotalTK,  F.WasteTK , F.TotalTG, F.ItemTK, F.IsFixPrice, F.FixPrice, F.DesignCharges,  " & _
                " CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                " CAST((F.ItemTK-F.GemsTK) AS INT) AS GoldK, CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                " CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                " CAST(F.GemsTK AS INT) AS GemsK, CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                " CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, " & _
                " PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, " & _
                " OriginalGemsPrice, OriginalOtherPrice, F.LocationID, F.IsOrder, F.IsClosed, F.OrderReceiveDetailID, F.IsVolume, F.QTY, F.StaffID, F.LossQTY, F.LossItemTK, F.LossItemTG , F.GivenDate as [@GDate], F.GoldSmith, F.IsDiamond, F.OriginalCode, F.PriceCode   " & _
                " FROM tbl_ForSale F " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID  " & _
                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID " & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=F.GoldQualityID  " & _
                " WHERE F.IsDelete=0 And F.GivenDate BETWEEN @FromDate And @ToDate " & cristr & " ORDER BY F.GivenDate ASC "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllStockDataForMonthly(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False, Optional ByVal global_isHOToBranch As Boolean = False) As System.Data.DataTable Implements ISalesItemDA.GetAllStockDataForMonthly
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim dtForSale As DataTable
            Try

                strCommandText = "select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                     " Where F.IsDelete=0   AND  GivenDate< @FromDate And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DBComm.CommandTimeout = 0
                dtForSale = DB.ExecuteDataSet(DBComm).Tables(0)


                If global_isHOToBranch Then
                    If global_isHeadOffice Then
                        ' Master And Stock  များ ကို HO to Branch Data Sync အသုံးပြုပြီး HO တွင် stock Transaction Report ကြည့်
                        strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                     " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                     " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                     " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                     " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                     " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                     " (select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                     " Where F.IsDelete=0   AND  GivenDate< @FromDate " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                     " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate And W.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     "  UNION ALL " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                     "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                     "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                     "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                     "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                     "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     "  UNION ALL  " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     "  0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID, " & _
                                     "  F.IsOrder From tbl_TransferItem D " & _
                                     "  Left Join tbl_Transfer W On D.TransferID=W.TransferID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID  " & _
                                     "  Where W.IsDelete=0 AND  transferDate < @FromDate And W.CurrentLocationID= " & LocationID & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_TransferReturnItem D " & _
                                     " Left Join tbl_TransferReturn W on D.TransferReturnID=W.TransferReturnID " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.TransferReturnDate< @FromDate " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " ) AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK, " & _
                                     " 0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder," & _
                                     " CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_ForSale F " & _
                                     " Where F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                     " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                     " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_Transfer T " & _
                                     " left Join tbl_TransferItem I on t.TransferID=I.TransferID" & _
                                     " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                     " Where T.IsDelete=0 And F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID<>" & LocationID & " And T.CurrentLocationID=" & LocationID & " " & cristr & " And I.IsReturn=0  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                     " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                     " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_TransferReturn T " & _
                                     " left Join tbl_TransferReturnItem I on t.TransferReturnID=I.TransferReturnID" & _
                                     " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                     " Where T.IsDelete=0 And F.IsDelete=0 AND TransferReturnDate BETWEEN @FromDate And @ToDate  " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where  W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                     " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                     " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                     " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION ALL" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                     " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                     " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                     " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                     " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                     " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                     " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                     " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                     " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "


                    Else
                        ' Master And Stock  များ ကို HO to Branch Data Sync အသုံးပြုပြီး Branch တွင် stock Transaction Report ကြည့်
                        strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                     " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                     " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                     " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                     " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                     " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                     " (select Count(I.ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_Transfer T " & _
                                     " Left Join tbl_TransferItem I on T.TransferID=I.TransferID Left Join tbl_ForSale F on  I.ForSaleID=F.ForSaleID " & _
                                     " Where F.IsDelete=0 AND T.IsDelete=0 And T.LocationID=" & LocationID & " AND  GivenDate< @FromDate " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                     " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate  And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     "  UNION ALL " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                     "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                     "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                     "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                     "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                     "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                      "  UNION ALL  " & _
                                     " select 0 as TotalStock, 0 AS TotalGoldTG, 0 AS TotalGoldTK,Count(F.ForSaleID) AS SaleStock,  Sum(F.ItemTG) AS SaleGoldTG, " & _
                                     " Sum(F.ItemTK) AS SaleGoldTK,  0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                     " From tbl_TransferReturnItem D Left Join tbl_TransferReturn W On D.TransferReturnID=W.TransferReturnID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  TransferReturnDate < @FromDate And W.CurrentLocationID= " & LocationID & cristr & _
                                     " Group By F.ItemCategoryID, F.IsOrder" & _
                                     " ) AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                     "UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                     " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                     " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_Transfer T " & _
                                     " left Join tbl_TransferItem I on t.TransferID=I.TransferID" & _
                                     " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                     " Where T.IsDelete=0 And F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate   And T.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where  W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     "  UNION ALL  " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   Count(F.ForSaleID) AS SaleStock, " & _
                                     " Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder " & _
                                     " WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' " & _
                                     " ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_TransferReturnItem D Left Join tbl_TransferReturn W On D.TransferReturnID=W.TransferReturnID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  TransferReturnDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID= " & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID, F.IsOrder" & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                     " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                     " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                     " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION ALL" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                     " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                     " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                     " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                     " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                     " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                     " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                     " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And W.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                     " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "

                    End If

                Else

                    If LocationID = Global_CurrentLocationID Then
                        'If global_isHeadOffice Then
                        If dtForSale.Rows.Count > 0 Then
                            strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                     " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                     " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                     " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                     " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                     " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                     " (select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                     " Where F.IsDelete=0   AND  GivenDate< @FromDate And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                     " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate And W.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     "  UNION ALL " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                     "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                     "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select Count(F.ForSaleID) as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,0 AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK,   " & _
                                     " 0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder From tbl_TransferItem D   Left Join tbl_Transfer W On D.TransferID=W.TransferID  " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  transferDate > @TFromDate And F.LocationID<>" & LocationID & " And W.CurrentLocationID=" & LocationID & " " & cristr & "   Group By F.ItemCategoryID, " & _
                                     " F.IsOrder " & _
                                      " UNION ALL " & _
                                     " select Count(F.ForSaleID) as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,0 AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK,   " & _
                                     " 0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder From tbl_TransferItem D   Left Join tbl_Transfer W On D.TransferID=W.TransferID  " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And F.LocationID<>" & LocationID & " And W.CurrentLocationID=" & LocationID & " " & cristr & "   Group By F.ItemCategoryID, " & _
                                     " F.IsOrder " & _
                                     "  UNION ALL  " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                     "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                     "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                     "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " ) AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK, " & _
                                     " 0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder," & _
                                     " CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_ForSale F " & _
                                     " Where F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     "UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                     " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                     " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_Transfer T " & _
                                     " left Join tbl_TransferItem I on t.TransferID=I.TransferID" & _
                                     " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                     " Where T.IsDelete=0 And F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID<>" & LocationID & " And T.CurrentLocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where  W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                     " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                     " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                     " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION ALL" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                     " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                     " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                     " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                     " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                     " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                     " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                     " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                     " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "
                        Else
                            strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                     " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                     " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                     " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                     " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                     " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                     " (select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                     " Where F.IsDelete=0   AND  GivenDate< @FromDate And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                     " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate And W.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     "  UNION ALL " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                     "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                     "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                     "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                     "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                     "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                     "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " ) AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK, " & _
                                     " 0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder," & _
                                     " CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_ForSale F " & _
                                     " Where F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     "UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                     " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                     " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_Transfer T " & _
                                     " left Join tbl_TransferItem I on t.TransferID=I.TransferID" & _
                                     " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                     " Where T.IsDelete=0 And F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID<>" & LocationID & " And T.CurrentLocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION All " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                     " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where  W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                     " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                     " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                     " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                     " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                     " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                     " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                     " UNION ALL " & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                     " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION ALL" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                     " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                     " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID" & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                     " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                     " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                     " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                     " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID " & _
                                     " UNION All" & _
                                     " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                     " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                     " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                     " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                     " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                     " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & _
                                     " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                     " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "

                        End If
                    Else
                        ' Master And Stock  များ ကို Branch to HO Data Sync အသုံးပြုပြီး HO တွင် stock Transaction Report ကြည့်
                        strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                             " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                             " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                             " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                             " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                             " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                             " (select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                             " Where F.IsDelete=0   AND  GivenDate< @FromDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                             " UNION ALL " & _
                                             " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                             " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                             " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                             " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                             " UNION ALL " & _
                                             " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                             " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                             " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                             " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                             " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate  And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                             "  UNION ALL " & _
                                             "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                             "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                             "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                             "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                             "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate  And F.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                             "  UNION ALL  " & _
                                             "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                             "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                             "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                             "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate  And F.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                             " ) AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                             " UNION All" & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK, " & _
                                             " 0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder," & _
                                             " CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_ForSale F " & _
                                             " Where F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                             " UNION ALL " & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                             " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                             " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                             " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                             " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                             " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where  W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                             " UNION ALL " & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                             " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                             " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                             " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                             " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                             " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                             " UNION ALL " & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                             " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                             " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                             " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                             " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                             " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                             " UNION ALL " & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                             " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                             " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                             " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                             " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                             " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                             " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And F.LocationID<>" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                             " UNION ALL " & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                             " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                             " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " " & cristr & _
                                             " Group By F.ItemCategoryID" & _
                                             " UNION ALL" & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                             " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                             " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                             " Group By F.ItemCategoryID " & _
                                             " UNION All" & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                             " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                             " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                             " Group By F.ItemCategoryID" & _
                                             " UNION All" & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                             " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                             " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                             " Group By F.ItemCategoryID " & _
                                             " UNION All" & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                             " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                             " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                             " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                             " Group By F.ItemCategoryID " & _
                                             " UNION All" & _
                                             " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                             " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                             " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                             " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                             " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                             " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & _
                                             " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                             " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "
                    End If

                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                DB.AddInParameter(DBComm, "@TFromDate", DbType.DateTime, CDate(FromDate.Date & " 23:59:59"))
                DBComm.CommandTimeout = 0
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllStockDataForMonthlyForOffline(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False) As System.Data.DataTable Implements ISalesItemDA.GetAllStockDataForMonthlyForOffline
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                 " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                 " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                 " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                 " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                 " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                 " (select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                 " Where F.IsDelete=0   AND  GivenDate< @FromDate And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                 " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                 " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                 " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                 " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 "  UNION ALL " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                 "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                 "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate And F.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 "  UNION ALL  " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 "  0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID, " & _
                                 "  F.IsOrder From tbl_TransferItem D " & _
                                 "  Left Join tbl_Transfer W On D.TransferID=W.TransferID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID  " & _
                                 "  Where W.IsDelete=0 AND  transferDate < @FromDate And F.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 "  UNION ALL  " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                 "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                 "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                 "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate And F.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 ") AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK, " & _
                                 " 0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder," & _
                                 " CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_ForSale F " & _
                                 " Where F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                 " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where         W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                 " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                 " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                 " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                 " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                 " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID" & _
                                 " UNION ALL" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                 " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                 " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID" & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                 " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                 " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                 " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                 " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                 " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                 " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                 " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                DBComm.CommandTimeout = 0
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleItemListForTransfer(ByVal argForSaleIDStr As String) As System.Data.DataTable Implements ISalesItemDA.GetForSaleItemListForTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If argForSaleIDStr <> "" Then
                strWhere = " And I.IsExit='0' AND I.ForSaleID NOT IN (" & argForSaleIDStr & ")"
            Else
                strWhere = " And I.IsExit='0' "
            End If
            Try
                strCommandText = "SELECT cast(0 as bit) as [$IsCheck], I.ItemCode,I.OriginalCode, C.ItemCategory AS [ItemCategory_], N.ItemName AS [ItemName_],I.ForSaleID AS [@ForSaleID],I.GoldQualityID as [@GoldQualityID], G.GoldQuality As [GoldQuality_], IsNull(I.Length,'-') as Length, " & _
                " IsNull(I.Width,'-') as Width, CAST(I.ItemTG AS DECIMAL(18,3)) as [Gram], I.ItemTG As [@ItemTG], I.ItemTK As [@ItemTK],  " & _
                " CAST(I.ItemTK AS INT) AS ItemK, CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                " CAST(OriginalPriceGram AS INT) AS OriginalPriceGram,CAST(OriginalPriceTK AS INT) AS OriginalPriceTK,CAST(OriginalFixedPrice AS INT) AS OriginalFixedPrice,CAST(OriginalGemsPrice AS INT) AS OriginalGemsPrice,PriceCode AS PriceCode,CAST(FixPrice AS INT) AS FixPrice " & _
                " FROM tbl_ForSale I " & _
                " LEFT JOIN tbl_GoldQuality G ON I.GoldQualityID=G.GoldQualityID  " & _
                " LEFT JOIN tbl_ItemName N ON I.ItemNameID=N.ItemNameID  " & _
                " LEFT JOIN tbl_ItemCategory C ON I.ItemCategoryID=C.ItemCategoryID Where I.IsDelete=0  and I.IsLooseDiamond=0 and I.LocationID= '" & Global_CurrentLocationID & "'" & strWhere

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@LocationID", DbType.String, argLocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleItemListForTransferDiamond(ByVal argForSaleIDStr As String) As System.Data.DataTable Implements ISalesItemDA.GetForSaleItemListForTransferDiamond
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If argForSaleIDStr <> "" Then
                strWhere = " And I.IsExit='0' AND I.ForSaleID NOT IN (" & argForSaleIDStr & ")"
            Else
                strWhere = " And I.IsExit='0' "
            End If
            Try
                strCommandText = "SELECT cast(0 as bit) as [$IsCheck], I.ItemCode,I.OriginalCode, C.GemsCategory AS [GemsCategory_], I.SDGemsName AS [GemsName_],I.ForSaleID AS [@ForSaleID], IsNull(I.Color,'-') as Color, IsNull(I.Shape,'-') as Shape, IsNull(I.Clarity,'-') as Clarity, " & _
                " CAST(I.ItemTG AS DECIMAL(18,3)) as [Gram], I.ItemTG As [@ItemTG], I.ItemTK As [@ItemTK],  " & _
                " CAST(I.ItemTK AS INT) AS ItemK, CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                " CAST(OriginalPriceCarat AS INT) AS OriginalPriceCarat,CAST(OriginalFixedPrice AS INT) AS OriginalFixedPrice,PriceCode AS PriceCode,CAST(FixPrice AS INT) AS FixPrice " & _
                " FROM tbl_ForSale I " & _
                " LEFT JOIN tbl_GemsCategory C ON I.SDGemsCategoryID=C.GemsCategoryID Where I.IsDelete=0  and I.IsLooseDiamond=1 and I.LocationID= '" & Global_CurrentLocationID & "'" & strWhere

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@LocationID", DbType.String, argLocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleItemListForWholesales(ByVal argForSaleIDStr As String) As System.Data.DataTable Implements ISalesItemDA.GetForSaleItemListForWholeSales
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If argForSaleIDStr <> "" Then
                strWhere = " and I.LocationID= '" & Global_CurrentLocationID & "'" & " And I.IsExit='0' AND I.ForSaleID NOT IN (" & argForSaleIDStr & ")"
            Else
                strWhere = " and I.LocationID= '" & Global_CurrentLocationID & "'" & " And I.IsExit='0' "
            End If
            Try
                strCommandText = "SELECT  I.ItemCode,I.OriginalCode, C.ItemCategory AS [ItemCategory_],I.ItemNameID, N.ItemName AS [ItemName_],I.ForSaleID AS [@ForSaleID],I.GoldQualityID as [@GoldQualityID], G.GoldQuality As [GoldQuality_], IsNull(I.Length,'-') as Length, " & _
                " IsNull(I.Width,'-') as Width, CAST(I.ItemTG AS DECIMAL(18,3)) as [Gram], I.ItemTG As [@ItemTG], I.ItemTK As [@ItemTK],(ItemTK-GemsTK) AS GoldTK, (ItemTG-GemsTG) AS GoldTG,  " & _
                " CAST((ItemTK-GemsTK) AS INT) AS GoldK, " & _
                " CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                " CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                " CAST((((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST(((((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16)-CAST(((ItemTK-GemsTK)-CAST((ItemTK-GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GoldC, " & _
                " CAST(I.ItemTK AS INT) AS ItemK, CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY, " & _
                " CAST(GemsTK AS INT) AS GemsK,CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP,CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemsY,CAST(((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS GemsC,GemsTK,GemsTG," & _
                " CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP,CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,CAST(((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS WasteC,WasteTK,WasteTG,CAST(OriginalPriceGram AS INT) AS OriginalPriceGram,CAST(OriginalPriceTK AS INT) AS OriginalPriceTK,CAST(OriginalFixedPrice AS INT) AS OriginalFixedPrice,CAST(OriginalGemsPrice AS INT) AS OriginalGemsPrice,PriceCode AS PriceCode,CAST(WSFixPrice AS INT) AS FixPrice,0 As DesignCharges,0 as DesignChargesRate,0 As DisPercent,0 As DisAmount " & _
                " FROM tbl_ForSale I " & _
                " LEFT JOIN tbl_GoldQuality G ON I.GoldQualityID=G.GoldQualityID  " & _
                " LEFT JOIN tbl_ItemName N ON I.ItemNameID=N.ItemNameID  " & _
                " LEFT JOIN tbl_ItemCategory C ON I.ItemCategoryID=C.ItemCategoryID Where I.IsDelete=0 And I.IsExit = '0' AND I.IsOrder='0' AND IsVolume='0' And IsSolidVolume='0' AND IsLooseDiamond=0 AND IsClosed='0' " & strWhere & " Order By [GivenDate] desc"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetForSaleItembyItemCode(ByVal ItemCode As String, ByVal argForSaleIDStr As String) As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleItembyItemCode
            Dim DBComm As DbCommand
            Dim strCommandText As String
            Dim drResult As IDataReader
            Dim objSaleItem As New SalesItemInfo
            Dim strWhere As String
            Try
                If argForSaleIDStr <> "" Then
                    strWhere = " AND I.IsExit='0' AND I.ItemCode=@ItemCode AND I.ItemCode NOT IN (" & argForSaleIDStr & ")"
                Else
                    strWhere = " AND I.IsExit='0' AND I.ItemCode=@ItemCode"
                End If
                strCommandText = "SELECT  I.ItemCode, N.ItemName, C.ItemCategory,I.ForSaleID,I.GoldQualityID,  IsNull(I.Length,'-') as Length, G.GoldQuality, " & _
                " IsNull(I.Width,'-') as Width,I.ItemTG, I.ItemTK,  " & _
                " CAST(I.ItemTK AS INT) AS ItemK, CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((I.ItemTK-CAST(I.ItemTK AS INT))*16)-CAST((I.ItemTK-CAST(I.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY ," & _
                "I.OriginalPriceGram,I.OriginalPriceTK,I.OriginalFixedPrice,I.OriginalGemsPrice,I.PriceCode,I.FixPrice " & _
                " FROM tbl_ForSale I " & _
                " LEFT JOIN tbl_GoldQuality G ON I.GoldQualityID=G.GoldQualityID  " & _
                " LEFT JOIN tbl_ItemName N ON I.ItemNameID=N.ItemNameID  " & _
                " LEFT JOIN tbl_ItemCategory C ON I.ItemCategoryID=C.ItemCategoryID WHERE I.IsDelete=0 and I.LocationID= '" & Global_CurrentLocationID & "'" & strWhere

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                ''DB.AddInParameter(DBComm, "@LocationID", DbType.String, argLocationID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objSaleItem
                        .ForSaleID = drResult("ForSaleID")
                        .ItemCode = drResult("ItemCode")
                        .ItemName = drResult("ItemName")
                        .ItemCategory = drResult("ItemCategory")
                        .GoldQuality = drResult("GoldQuality")
                        .Length = drResult("Length")
                        .Width = drResult("Width")
                        .ItemTG = drResult("ItemTG")
                        .ItemTK = drResult("ItemTK")
                        .ItemK = drResult("ItemK")
                        .ItemP = drResult("ItemP")
                        .ItemY = drResult("ItemY")
                        .OriginalFixedPrice = drResult("OriginalFixedPrice")
                        .OriginalGemsPrice = drResult("OriginalGemsPrice")
                        .OriginalPriceTK = drResult("OriginalPriceTK")
                        .OriginalPriceGram = drResult("OriginalPriceGram")
                        .PriceCode = drResult("PriceCode")
                        .FixPrice = drResult("FixPrice")
                        .GoldQualityID = drResult("GoldQualityID")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objSaleItem
        End Function
        Public Function GetForSaleDataByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesItemDA.GetForSaleDataByForSaleID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "Select * from tbl_ForSale Where ForSaleID='" & ForSaleID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)

                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSaleInfoByBarcodeNo(ByVal ItemCode As String, ByVal ItemCategoryID As String, ByVal argItemcodeStr As String) As CommonInfo.SalesItemInfo Implements ISalesItemDA.GetForSaleInfoByBarcodeNo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.SalesItemInfo
            Dim strWhere As String
            Try
                If argItemcodeStr <> "" Then
                    strWhere = " AND F.ItemCode=@ItemCode AND F.ItemCategoryID=@ItemCategoryID AND F.ItemCode NOT IN (" & argItemcodeStr & ")"
                Else
                    strWhere = " AND F.ItemCode=@ItemCode AND F.ItemCategoryID=@ItemCategoryID"
                End If

                strCommandText = "Select ItemCode,F.ItemCategoryID,F.GoldTG"
                strCommandText += " From tbl_ForSale F Inner JOIN tbl_ItemCategory I ON F.ItemCategoryID=I.ItemCategoryID  Where 1 =1  " & strWhere
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj

                        .ItemCode = drResult("ItemCode")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .GoldTG = drResult("GoldTG")


                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
        Public Function GetBarcodeTrack(ByVal BarcodeNo As String) As System.Data.DataTable Implements ISalesItemDA.GetBarcodeTrack
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT ItemCode As BarcodeNo,TotalG, ActionID,CustomerName,Location, ActionDate, Status,DStatus,OriginalCode FROM  ( " & _
                                 " SELECT ItemCode,GoldTG as TotalG, 'Entry' AS ActionID , ''as CustomerName,'' as " & _
                                 " Location,GivenDate AS ActionDate,CASE IsExit WHEN 1 THEN 'No' ELSE 'YES' END AS Status,CASE IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,OriginalCode  FROM tbl_ForSale   WHERE ItemCode LIKE @BarcodeNo" & _
                                 " UNION ALL " & _
                                 " SELECT  F.ItemCode,F.TotalTG as TotalG, T.TransferID AS ActionID,'' as CustomerName,L.Location, T.TransferDate AS ActionDate,  'T' AS Status,CASE T.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode  FROM tbl_Transfer " & _
                                 " T INNER JOIN tbl_TransferItem TI ON T.TransferID=TI.TransferID  INNER JOIN tbl_Location L ON T.LocationID=L.LocationID " & _
                                 " INNER JOIN tbl_ForSale F on TI.ForSaleID=F.ForSaleID   WHERE F.ItemCode LIKE @BarcodeNo " & _
                                 " UNION ALL  " & _
                                 " SELECT  CI.ItemCode,CI.ItemTG, C.WholesaleInvoiceID AS ActionID,CU.CustomerName,L.Location, C.WDate AS ActionDate,   case  when (ci.IsSale=1) AND (CI.IsReturn=0) " & _
                                 " then 'S' when (ci.IsSale=0) AND CI.IsReturn=0 then 'P' end  AS Status,CASE C.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode  FROM tbl_WholeSaleInvoice C " & _
                                 " INNER JOIN tbl_WholeSaleInvoiceItem CI ON C.WholesaleInvoiceID=CI.WholesaleInvoiceID  " & _
                                 " inner join tbl_Customer CU On C.CustomerID=CU.CustomerID  " & _
                                 " inner join tbl_ForSale F On CI.ForSaleID=F.ForSaleID  " & _
                                 " INNER JOIN tbl_Location L ON C.LocationID=L.LocationID    WHERE CI.ItemCode LIKE @BarcodeNo  " & _
                                 " UNION ALL  " & _
                                 " SELECT  CI.ItemCode,CI.ItemTG, C.WholesaleReturnID AS ActionID,CU.CustomerName,L.Location, C.WReturnDate AS ActionDate,   case when (ci.IsReturn=1)and (ci.IsSale=1) " & _
                                 " then 'SR'  when   (ci.IsReturn=1)and (ci.IsSale=0) then 'PR' when (ci.IsSale=1) AND (CI.IsReturn=0) then 'S' when (ci.IsSale=0) AND CI.IsReturn=0 then 'P' end  AS " & _
                                 " Status,CASE C.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode  FROM tbl_WholeSaleReturn C " & _
                                 " INNER JOIN tbl_WholeSaleReturnItem CI ON C.WholesaleReturnID=CI.WholesaleReturnID  " & _
                                 " inner join tbl_Customer CU On C.CustomerID=CU.CustomerID  " & _
                                 " inner join tbl_ForSale F On CI.ForSaleID=F.ForSaleID  " & _
                                 " INNER JOIN tbl_Location L ON C.LocationID=L.LocationID     WHERE CI.ItemCode LIKE @BarcodeNo   " & _
                                 " UNION ALL " & _
                                 " SELECT  CI.ItemCode,CI.ItemTG, C.ConsignmentSaleID AS ActionID,CU.CustomerName,L.Location, C.ConsignDate AS ActionDate,   case when (ci.IsReturn=1)" & _
                                 " and (ci.IsSale=1) then 'SR'  when   (ci.IsReturn=1)and (ci.IsSale=0) then 'PR' when (ci.IsSale=1) AND (CI.IsReturn=0) then 'S' when (ci.IsSale=0) AND CI.IsReturn=0 " & _
                                 " then 'P' end  AS Status,CASE C.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode  FROM tbl_ConsignmentSale C " & _
                                 " INNER JOIN tbl_ConsignmentSaleItem CI ON C.ConsignmentSaleID=CI.ConsignmentSaleID  " & _
                                 " inner join tbl_Customer CU On C.CustomerID=CU.CustomerID   " & _
                                 " inner join tbl_ForSale F On CI.ForSaleID=F.ForSaleID  " & _
                                 " INNER JOIN tbl_Location L ON C.LocationID=L.LocationID   WHERE CI.ItemCode LIKE @BarcodeNo   " & _
                                 " UNION ALL " & _
                                 " SELECT  CI.ItemCode,CI.ItemTG as TotalG, C.SaleInvoiceHeaderID AS ActionID,CU.CustomerName,L.Location, C.SaleDate AS ActionDate, 'Retail' AS Status,CASE C.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode " & _
                                 " FROM tbl_SaleInvoiceHeader C  " & _
                                 " INNER JOIN tbl_SaleInvoiceDetail CI ON C.SaleInvoiceHeaderID=CI.SaleInvoiceHeaderID  " & _
                                 " inner join tbl_Customer CU On C.CustomerID=CU.CustomerID  " & _
                                 " inner join tbl_ForSale F On CI.ForSaleID=F.ForSaleID  " & _
                                 " INNER JOIN tbl_Location L ON C.LocationID=L.LocationID   WHERE CI.ItemCode LIKE @BarcodeNo" & _
                                 " UNION ALL " & _
                                 " SELECT  CI.BarcodeNo,CI.TotalTG as TotalG, C.PurchaseHeaderID  AS ActionID,CU.CustomerName,L.Location, C.PurchaseDate AS ActionDate, 'Return' AS Status,CASE C.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode  " & _
                                 " FROM tbl_PurchaseHeader  C " & _
                                 " INNER JOIN tbl_PurchaseDetail  CI ON C.PurchaseHeaderID=CI.PurchaseHeaderID " & _
                                 " inner join tbl_Customer CU On C.CustomerID=CU.CustomerID  " & _
                                 " inner join tbl_ForSale F On CI.ForSaleID=F.ForSaleID  " & _
                                 " INNER JOIN tbl_Location L ON C.LocationID=L.LocationID  WHERE BarcodeNo LIKE @BarcodeNo  " & _
                                 " Union All   " & _
                                 " SELECT  F.ItemCode,F.ItemTG  as TotalG,C.TransferReturnID AS ActionID,'' AS CustomerName,L.Location, C.TransferReturnDate AS ActionDate, 'TR' AS Status,CASE C.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode  " & _
                                 " FROM tbl_TransferReturn C " & _
                                 " INNER JOIN tbl_TransferReturnItem CI ON C.TransferReturnID=CI.TransferReturnID  " & _
                                 " inner join tbl_Location L On C.CurrentLocationID=L.LocationID" & _
                                 " Inner JOIN tbl_forsale F On CI.ForSaleID=F.ForSaleID  WHERE F.ItemCode LIKE  @BarcodeNo " & _
                                 " Union All   " & _
                                 " SELECT  F.ItemCode,F.ItemTG  as TotalG,O.OrderReturnHeaderID  AS ActionID,'' AS CustomerName,L.Location, O.ReturnDate AS ActionDate, 'Order Return' AS Status,CASE O.IsDelete WHEN 1 THEN 'Yes' ELSE 'No' END AS DStatus,F.OriginalCode  " & _
                                 " FROM tbl_OrderReturnHeader  O " & _
                                 " INNER JOIN tbl_OrderReturnDetail OI ON O.OrderReturnHeaderID=OI.OrderReturnHeaderID   " & _
                                 " inner join tbl_Location L On O.LocationID=L.LocationID" & _
                                 " Inner JOIN tbl_forsale F On OI.ForSaleID=F.ForSaleID  WHERE F.ItemCode LIKE  @BarcodeNo " & _
                                 " ) AS Main  ORDER BY ItemCode, ActionDate,ActionID,CustomerName,Location, " & _
                                 " Status"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@BarcodeNo", DbType.String, BarcodeNo)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllStockDataForMonthlyByTransferDate(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False, Optional ByVal global_isHOToBranch As Boolean = False) As System.Data.DataTable Implements ISalesItemDA.GetAllStockDataForMonthlyByTransferDate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim dtForSale As DataTable
            Try

                strCommandText = "select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                     " Where F.IsDelete=0   AND  GivenDate< @FromDate And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DBComm.CommandTimeout = 0
                dtForSale = DB.ExecuteDataSet(DBComm).Tables(0)

                If global_isHeadOffice Then
                    ' Master And Stock  များ ကို HO to Branch Data Sync အသုံးပြုပြီး HO တွင် stock Transaction Report ကြည့်
                    strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                 " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                 " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                 " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                 " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                 " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                 " (select Count(ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_ForSale F " & _
                                 " Where F.IsDelete=0   AND  GivenDate< @FromDate " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                 " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                 " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                 " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                 " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate And W.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 "  UNION ALL " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                 "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                 "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                 "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                 "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                 "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 "  UNION ALL  " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 "  0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID, " & _
                                 "  F.IsOrder From tbl_TransferItem D " & _
                                 "  Left Join tbl_Transfer W On D.TransferID=W.TransferID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID  " & _
                                 "  Where W.IsDelete=0 AND  transferDate < @FromDate And W.CurrentLocationID= " & LocationID & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_TransferReturnItem D " & _
                                 " Left Join tbl_TransferReturn W on D.TransferReturnID=W.TransferReturnID " & _
                                 " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.TransferReturnDate< @FromDate " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " ) AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK, " & _
                                 " 0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder," & _
                                 " CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_ForSale F " & _
                                 " Where F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION All " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                 " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                 " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_Transfer T " & _
                                 " left Join tbl_TransferItem I on t.TransferID=I.TransferID" & _
                                 " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                 " Where T.IsDelete=0 And F.IsDelete=0 AND GivenDate BETWEEN @FromDate And @ToDate  And F.LocationID<>" & LocationID & " And T.CurrentLocationID=" & LocationID & " " & cristr & " And I.IsReturn=0  Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION All " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                 " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                 " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_TransferReturn T " & _
                                 " left Join tbl_TransferReturnItem I on t.TransferReturnID=I.TransferReturnID" & _
                                 " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                 " Where T.IsDelete=0 And F.IsDelete=0 AND TransferReturnDate BETWEEN @FromDate And @ToDate  " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION All " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                 " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where  W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                 " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                 " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                 " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                 " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                 " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID" & _
                                 " UNION ALL" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                 " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                 " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID" & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                 " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                 " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                 " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                 " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                 " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                 " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                 " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "


                Else
                    ' Master And Stock  များ ကို HO to Branch Data Sync အသုံးပြုပြီး Branch တွင် stock Transaction Report ကြည့်
                    strCommandText = " SELECT Sum(Opening) AS Opening, Sum(OpeningTG) AS OpeningTG, Sum(OpeningTK) AS OpeningTK, " & _
                                 " Sum(EntryStock) AS EntryStock, Sum(EntryGoldTG) AS EntryGoldTG, Sum(EntryGoldTK) AS EntryGoldTK," & _
                                 " Sum(SaleStock) AS SaleStock, Sum(SaleGoldTG) AS SaleGoldTG, Sum(SaleGoldTK) AS SaleGoldTK,Sum(WSaleReturn) As WSaleReturn,Sum(WSaleGoldTG) As WSaleGoldTG,Sum(WSaleGoldTK) As WSaleGoldTK, N.ItemCategoryID, IC.ItemCategory, N.Type, N.ForOrder, N.MyanType FROM " & _
                                 " (Select Sum(A.TotalStock-Isnull(A.SaleStock,0)) AS Opening, Sum(A.TotalGoldTG-Isnull(A.SaleGoldTG,0)) As OpeningTG, Sum(A.TotalGoldTK-isnull(A.SaleGoldTK,0)) As OpeningTK, " & _
                                 " 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,0 AS SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK," & _
                                 " A.ItemCategoryID, CASE A.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, CASE A.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE A.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType from " & _
                                 " (select Count(I.ForSaleID) AS TotalStock, Sum(ItemTG) AS TotalGoldTG, Sum(ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_Transfer T " & _
                                 " Left Join tbl_TransferItem I on T.TransferID=I.TransferID Left Join tbl_ForSale F on  I.ForSaleID=F.ForSaleID " & _
                                 " Where F.IsDelete=0 AND T.IsDelete=0 And T.LocationID=" & LocationID & " AND  T.TransferDate< @FromDate " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select Count(F.ForSaleID) AS TotalStock, Sum(F.ItemTG) AS TotalGoldTG, Sum(F.ItemTK) AS TotalGoldTK, 0 SaleStock, 0 AS SaleGoldTG, " & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder From tbl_WholesaleReturnItem D " & _
                                 " Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID " & _
                                 " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate< @FromDate  And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK, Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 " 0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, F.IsOrder " & _
                                 " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                 " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where   W.IsDelete=0 AND  WDate < @FromDate And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 "  UNION ALL " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG,Sum(F.ItemTK) AS SaleGoldTK, " & _
                                 "  0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 "  0 As WSaleGoldTK, F.ItemCategoryID,F.IsOrder  " & _
                                 "  From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID  " & _
                                 "  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 "  select 0 as TotalStock, 0 AS TotalGoldTG,0 As TotalGoldTK,Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, " & _
                                 "  Sum(F.ItemTK) AS SaleGoldTK,   0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                 "  From tbl_OrderReturnDetail D   Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  " & _
                                 "  On D.ForSaleID=F.ForSaleID    Where W.IsDelete=0 AND  ReturnDate  < @FromDate And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                  "  UNION ALL  " & _
                                 " select 0 as TotalStock, 0 AS TotalGoldTG, 0 AS TotalGoldTK,Count(F.ForSaleID) AS SaleStock,  Sum(F.ItemTG) AS SaleGoldTG, " & _
                                 " Sum(F.ItemTK) AS SaleGoldTK,  0 As WSaleReturn,0 As WSaleGoldTG, 0 As WSaleGoldTK, F.ItemCategoryID,   F.IsOrder " & _
                                 " From tbl_TransferReturnItem D Left Join tbl_TransferReturn W On D.TransferReturnID=W.TransferReturnID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  TransferReturnDate < @FromDate And W.CurrentLocationID= " & LocationID & cristr & _
                                 " Group By F.ItemCategoryID, F.IsOrder" & _
                                 " ) AS A Group By A.ItemCategoryID, A.IsOrder " & _
                                 "UNION All " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(F.ForSaleID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, " & _
                                 " 0 AS SaleGoldTG, 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' " & _
                                 " END AS Type, CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_Transfer T " & _
                                 " left Join tbl_TransferItem I on t.TransferID=I.TransferID" & _
                                 " left Join tbl_Forsale F on I.ForSaleID=F.ForSaleID  " & _
                                 " Where T.IsDelete=0 And F.IsDelete=0 AND T.TransferDate BETWEEN @FromDate And @ToDate   And T.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION All " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_WholeSaleInvoiceItem D Left Join tbl_WholeSaleInvoice W On D.WholeSaleInvoiceID=W.WholeSaleInvoiceID " & _
                                 " Left Join tbl_ForSale F On F.ForSaleID=D.ForSaleID Where  W.IsDelete=0 AND  WDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & "  Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG, " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_SaleInvoiceDetail D Left Join tbl_SaleInvoiceHeader W On D.SaleInvoiceHeaderID=W.SaleInvoiceHeaderID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  SaleDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 "  UNION ALL  " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   Count(F.ForSaleID) AS SaleStock, " & _
                                 " Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder " & _
                                 " WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' " & _
                                 " ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_TransferReturnItem D Left Join tbl_TransferReturn W On D.TransferReturnID=W.TransferReturnID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 AND  TransferReturnDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID= " & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID, F.IsOrder" & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,   " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,  " & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type,  CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' " & _
                                 " END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  From tbl_OrderReturnDetail D " & _
                                 " Left Join tbl_OrderReturnHeader W On D.OrderReturnHeaderID=W.OrderReturnHeaderID  Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                 " Where W.IsDelete = 0 And ReturnDate BETWEEN @FromDate And @ToDate  And F.LocationID=" & LocationID & " And W.LocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " Count(F.ForSaleID) AS SaleStock, Sum(F.ItemTG) AS SaleGoldTG, Sum(F.ItemTK) AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG," & _
                                 " 0 As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder, CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType " & _
                                 " From tbl_TransferItem D Left Join tbl_Transfer W On D.TransferID=W.TransferID " & _
                                 " Left Join tbl_ForSale F  On D.ForSaleID=F.ForSaleID " & _
                                 " Where W.IsDelete=0 AND  transferDate BETWEEN @FromDate And @ToDate  And W.CurrentLocationID=" & LocationID & " " & cristr & " Group By F.ItemCategoryID, F.IsOrder " & _
                                 " UNION ALL " & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'PurchaseStock' AS Type, " & _
                                 " 'C' AS ForOrder, N'အဝယ်' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=0 AND H.IsDelete=0 And H.LocationID=" & LocationID & " And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID" & _
                                 " UNION ALL" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(PurchaseDetailID) AS EntryStock, Sum(TotalTG) AS EntryGoldTG, Sum(TotalTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'ChangeStock' AS Type, " & _
                                 " 'D' AS ForOrder, N'အလဲ' AS MyanType From tbl_PurchaseDetail F LEFT JOIN tbl_PurchaseHeader H ON F.PurchaseHeaderID=H.PurchaseHeaderID Where H.IsDelete=0 AND PurchaseDate BETWEEN @FromDate And @ToDate  AnD IsGem=0 AND H.IsChange=1 And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(OrderReceiveDetailID) AS EntryStock, Sum(GoldTG) AS EntryGoldTG, Sum(GoldTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'OrderReceive' AS Type, " & _
                                 " 'E' AS ForOrder, N'အော်ဒါ' AS MyanType From tbl_OrderReceiveDetail F LEFT JOIN tbl_OrderInvoice H ON F.OrderInvoiceID=H.OrderInvoiceID Where H.IsDelete=0 AND OrderDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID" & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(RepairDetailID) AS EntryStock, Sum(ItemTG) AS EntryGoldTG, Sum(ItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReceive' AS Type, " & _
                                 " 'F' AS ForOrder, N'ပြင်ထည်' AS MyanType From tbl_RepairDetail F LEFT JOIN tbl_RepairHeader H ON F.RepairID=H.RepairID Where H.IsDelete=0 AND RepairDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, Count(ReturnRepairDetailID) AS EntryStock, Sum(ReturnItemTG) AS EntryGoldTG, Sum(ReturnItemTK) AS EntryGoldTK,  0 AS SaleStock, 0 AS SaleGoldTG," & _
                                 " 0 AS SaleGoldTK,0 As WSaleReturn,0 As WSaleGoldTG,0 As WSaleGoldTK, F.ItemCategoryID, 'RepairReturn' AS Type, " & _
                                 " 'G' AS ForOrder, N'ပြင်ထည်ရွေး' AS MyanType From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                 " LEFT JOIN tbl_RepairDetail F ON F.RepairDetailID=D.RepairDetailID Where H.IsDelete=0 AND ReturnDate BETWEEN @FromDate And @ToDate  And H.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID " & _
                                 " UNION All" & _
                                 " select 0 as Opening, 0 AS OpeningTG, 0 AS OpeningTK, 0 AS EntryStock, 0 AS EntryGoldTG, 0 AS EntryGoldTK,  " & _
                                 " 0 AS SaleStock, 0 AS SaleGoldTG,  0 AS SaleGoldTK,count(F.ForSaleID) As WSaleReturn,Sum(F.ItemTG) As WSaleGoldTG, " & _
                                 " Sum(F.ItemTK) As WSaleGoldTK, F.ItemCategoryID, CASE F.IsOrder WHEN 0 THEN 'Stock' ELSE 'OrderStock' END AS Type, " & _
                                 " CASE F.IsOrder WHEN 0 THEN 'A' ELSE 'B' END AS ForOrder,  CASE F.IsOrder WHEN 0 THEN N'ဆိုင်ပစ္စည်း' ELSE N'အော်ဒါပစ္စည်း' END AS MyanType  " & _
                                 " From tbl_WholesaleReturnItem D  Left Join tbl_WholeSaleReturn W on D.WholesaleReturnID=W.WholesaleReturnID  " & _
                                 " Left Join tbl_ForSale F On D.ForSaleID=F.ForSaleID Where W.IsDelete=0 and F.IsDelete=0  and W.WReturndate BETWEEN @FromDate And @ToDate  And W.LocationID=" & LocationID & " " & cristr & _
                                 " Group By F.ItemCategoryID, F.IsOrder) As N" & _
                                 " LEFT JOIN tbl_ItemCategory IC On N.ItemCategoryID=IC.ItemCategoryID GROUP BY IC.ItemCategory, N.ItemCategoryID, N.Type, N.ForOrder, N.MyanType ORDER By IC.ItemCategory,N.ForOrder "

                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                DBComm.CommandTimeout = 0
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetStockBalanceFromHO(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesItemDA.GetStockBalanceFromHO
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT H.ForSaleID, CAST((ItemTG-H.GemsTG) as DECIMAL(18,4)) as GoldTG, CAST(H.GemsTG AS DECIMAL(18,3)) as GemsTG , CAST(ItemTG as DECIMAL(18,4)) as ItemTG, " & _
                                " CAST(H.WasteTG as DECIMAL(18,3)) as WasteTG , ItemCode, H.ItemNameID, I.ItemName, Length, H.GoldQualityID, GoldQuality, H.ItemCategoryID, ItemCategory," & _
                                " GivenDate,  IsExit,H.OriginalGemsPrice, ExitDate, (ItemTK-H.GemsTK) AS GoldTK,  H.GemsTK, TotalTK,  WasteTK ,TotalTG, ItemTK,  Width, H.IsFixPrice, " & _
                                " H.FixPrice, DesignCharges,   CAST(ItemTK AS INT) AS ItemK, CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS ItemP,  CAST((((ItemTK-CAST(ItemTK AS INT))*16)" & _
                                " -CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS ItemY,  CAST((ItemTK-H.GemsTK) AS INT) AS GoldK, CAST(((ItemTK-H.GemsTK)" & _
                                " -CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT) AS GoldP,  CAST(((((ItemTK-H.GemsTK)-CAST((ItemTK-H.GemsTK) AS INT))*16)-CAST(((ItemTK-H.GemsTK) " & _
                                " -CAST((ItemTK-H.GemsTK) AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS GoldY,  CAST(WasteTK AS INT) AS WasteK, CAST((WasteTK-CAST(WasteTK AS INT))*16 AS " & _
                                " INT) AS WasteP,  CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS WasteY,  " & _
                                " CAST(H.GemsTK AS INT) AS GemsK, CAST((H.GemsTK-CAST(H.GemsTK AS INT))*16 AS INT) AS GemsP,  CAST((((H.GemsTK-CAST(H.GemsTK AS INT))*16)-CAST((H.GemsTK " & _
                                " -CAST(H.GemsTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,1)) AS GemsY,  CAST(PurchaseWasteTK AS INT) AS PurchaseWasteK, CAST((PurchaseWasteTK " & _
                                " -CAST(PurchaseWasteTK AS INT))*16 AS INT) AS PurchaseWasteP, CAST((((PurchaseWasteTK-CAST(PurchaseWasteTK AS INT))*16)-CAST((PurchaseWasteTK " & _
                                " -CAST(PurchaseWasteTK AS INT))*16 AS INT))*'8.0' AS DECIMAL(18,2)) AS PurchaseWasteY, PlatingCharges, MountingCharges, WhiteCharges, IsOriginalFixedPrice, " & _
                                " H.OriginalFixedPrice, IsOriginalPriceGram, H.OriginalPriceGram, H.OriginalPriceTK,  H.OriginalGemsPrice, OriginalOtherPrice, H.LocationID, H.IsOrder, H.IsClosed, " & _
                                " H.OrderReceiveDetailID, H.IsVolume, H.QTY, H.StaffID, S.Staff, H.LossQTY, H.LossItemTK, H.LossItemTG ,   HI.ForSaleGemsItemID, HI.GemsCategoryID,  " & _
                                " GC.GemsCategory, HI.GemsName, HI.GemsTK As ItemGemsIK, HI.GemsTG As ItemGemsTG, HI.YOrCOrG, HI.GemsTW, HI.Qty As GemQTY, HI.Type, HI.UnitPrice, HI.Amount, " & _
                                " HI.GemsRemark, GivenDate as [@GDate], H.GoldSmith, H.IsDiamond, H.Remark ,GC.GemTaxPer,C.ItemTaxPer,H.OriginalCode ,CASE H.GoldSmithID When '0' Then H.GoldSmith else GG.Name END as GoldSmith ,SP.SupplierName " & _
                                " as Supplier,H.SellingRate,H.PriceCode,H.PurchaseWasteTK,H.PurchaseWasteTG FROM tbl_Transfer T " & _
                                " LEFT JOIN tbl_TransferItem TI ON T.TransferID=TI.TransferID " & _
                                " INNER JOIN  tbl_ForSale H ON H.ForSaleID=TI.ForSaleID " & _
                                " LEFT JOIN  tbl_ForSaleGemsItem HI   ON H.ForSaleID=HI.ForSaleID " & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=H.ItemNameID " & _
                                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID " & _
                                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=H.ItemCategoryID " & _
                                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                " LEFT JOIN tbl_GoldSmith GG ON GG.GoldSmithID=H.GoldSmithID " & _
                                " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=HI.GemsCategoryID " & _
                                " LEFT JOIN tbl_Supplier SP On SP.SupplierID=H.SupplierID  WHERE T.IsDelete=0 And T.LocationID = " & LocationID & _
                                " AND TI.ForSaleID NOT IN   ( " & _
                                " SELECT ForSaleID FROM tbl_SaleInvoiceDetail SD INNER JOIN tbl_SaleInvoiceHeader SI  on SD.SaleInvoiceHeaderID=SI.SaleInvoiceHeaderID Where SI.isDelete=0 And SI.LocationID=" & LocationID & _
                                " ) AND TI.ForSaleID NOT IN ( " & _
                                " SELECT ForSaleID FROM tbl_WholesaleInvoiceItem WI INNER JOIN tbl_WholeSaleInvoice W on W.WholeSaleInvoiceID=WI.WholesaleInvoiceID  Where W.isDelete=0  And W.LocationID=" & LocationID & _
                                " ) AND TI.ForSaleID NOT IN ( " & _
                                " SELECT ForSaleID FROM tbl_OrderReturnDetail WI INNER JOIN tbl_OrderReturnHeader W on W.OrderReturnHeaderID=WI.OrderReturnHeaderID  Where W.isDelete=0 " & _
                                " And W.LocationID= " & LocationID & " And  WI.IsReturn=0)  And TI.isReturn=0 " & cristr & " Order by ItemCode ASC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DBComm.CommandTimeout = 0
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetBalanceFromHOByTotalWeight(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesItemDA.GetBalanceFromHOByTotalWeight
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " SELECT Count(H.ForSaleID) As TotalQTY, Sum(H.ItemTK) AS ItemTK, Sum(H.ItemTK-H.GemsTK) As GoldTK, " & _
                                " Sum(H.WasteTK) As WasteTK, Sum(H.GemsTK)  As GemsTK, Sum(CAST((H.ItemTG-GemsTG) as DECIMAL(18,3))) As GoldTG," & _
                                " Sum(Cast(H.GemsTG as DECIMAL(18,3))) AS GemsTG, Sum(Cast(H.WasteTG as DECIMAL(18,3))) AS WasteTG, " & _
                                " Sum(CAST(H.ItemTG AS DECIMAL(18,3))) as ItemTG ,Sum(H.FixPrice) as TotalFixPrice " & _
                                " FROM tbl_Transfer T  " & _
                                " LEFT JOIN tbl_TransferItem TI ON T.TransferID=TI.TransferID " & _
                                " INNER JOIN  tbl_ForSale H ON H.ForSaleID=TI.ForSaleID " & _
                                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.GoldQualityID  WHERE T.IsDelete=0 And T.LocationID = " & LocationID & _
                                " AND TI.ForSaleID NOT IN   ( " & _
                                " SELECT ForSaleID FROM tbl_SaleInvoiceDetail SD INNER JOIN tbl_SaleInvoiceHeader SI  on SD.SaleInvoiceHeaderID=SI.SaleInvoiceHeaderID Where SI.isDelete=0 And SI.LocationID=" & LocationID & _
                                " ) AND TI.ForSaleID NOT IN ( " & _
                                " SELECT ForSaleID FROM tbl_WholesaleInvoiceItem WI INNER JOIN tbl_WholeSaleInvoice W on W.WholeSaleInvoiceID=WI.WholesaleInvoiceID  Where W.isDelete=0  And W.LocationID=" & LocationID & _
                                " ) AND TI.ForSaleID NOT IN ( " & _
                                " SELECT ForSaleID FROM tbl_OrderReturnDetail WI INNER JOIN tbl_OrderReturnHeader W on W.OrderReturnHeaderID=WI.OrderReturnHeaderID  Where W.isDelete=0 " & _
                                " And W.LocationID= " & LocationID & " And  WI.IsReturn=0)  And TI.isReturn=0 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DBComm.CommandTimeout = 0
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForSalesItemForSaleLooseDiamond(Optional ByVal Itemcode As String = "") As System.Data.DataTable Implements ISalesItemDA.GetForSalesItemForSaleLooseDiamond
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim strWhere As String
            If Itemcode <> "" Then
                strWhere = " and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND F.ItemCode NOT IN (" & Itemcode & ")"
            Else
                strWhere = " and F.LocationID= '" & Global_CurrentLocationID & "'" & " AND 1=1"
            End If
            Try
                strCommandText = "SELECT  F.ForSaleID AS [@ForSaleID],F.ItemCode, SDYOrCOrG as [YOrCOrG_] , CONVERT(VARCHAR,CAST(ItemTG AS DECIMAL(18,3))) AS Gram, convert(varchar(10),F.GivenDate,105)as GivenDate, F.SDGemsCategoryID AS [@GemsCategoryID],I.GemsCategory as [GemsCategory_],F.SDGemsName as [GemsName_], F.Color AS [Color_], F.Shape as [Shape_], IsNull(F.Clarity,'') as [Clarity_],"
                strCommandText += " F.OriginalCode, F.PriceCode, CONVERT(VARCHAR,F.FixPrice) As FixPrice, "
                strCommandText += " F.IsExit AS [@IsExit], "
                strCommandText += " F.IsFixPrice AS [@IsFixPrice],CONVERT(VARCHAR,F.FixPrice) As FixPrice, CONVERT(VARCHAR,DesignCharges) As DesignCharges, CONVERT(VARCHAR,PlatingCharges) AS PlatingCharges, CONVERT(VARCHAR,MountingCharges) AS MountingCharges,  CONVERT(VARCHAR,WhiteCharges) AS WhiteCharges,F.GivenDate as [@GDate] "
                strCommandText += " FROM tbl_ForSale F "
                strCommandText += " INNER JOIN tbl_GemsCategory I ON F.SDGemsCategoryID = I.GemsCategoryID where F.IsExit = '0' AND F.IsOrder='0' AND IsVolume='0' AND IsClosed='0' And IsLooseDiamond='1' AND F.IsDelete=0  " & strWhere & " Order By [@GDate] desc"
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

