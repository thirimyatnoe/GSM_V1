Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace OrderInvoice
    Public Class OrderInvoiceDA
        Implements IOrderInvoiceDA

#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As IOrderInvoiceDA = New OrderInvoiceDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IOrderInvoiceDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteOrderInvoice(ByVal OrderInvoiceID As String) As Boolean Implements IOrderInvoiceDA.DeleteOrderInvoice
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_OrderInvoice SET IsDelete=1,LastModifiedDate=GetDate()  WHERE  OrderInvoiceID= @OrderInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
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
        Public Function DeleteOrderInvoiceReturn(ByVal OrderReturnHeaderID As String) As Boolean Implements IOrderInvoiceDA.DeleteOrderInvoiceReturn
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = " Update tbl_OrderReturnHeader SET IsDelete=1,LastModifiedDate=GetDate() WHERE  OrderReturnHeaderID= @OrderReturnHeaderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, OrderReturnHeaderID)
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
        Public Function DeleteOrderInvoiceDetail(ByVal OrderInvoiceDetailID As String) As Boolean Implements IOrderInvoiceDA.DeleteOrderInvoiceDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_OrderReturnDetail WHERE  OrderInvoiceDetailID= @OrderInvoiceDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceDetailID", DbType.String, OrderInvoiceDetailID)
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

        Public Function DeleteOrderInvoiceItem(ByVal OrderInvoiceGemsItemID As String) As Boolean Implements IOrderInvoiceDA.DeleteOrderInvoiceItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_OrderInvoiceGemsItem WHERE  OrderInvoiceGemsItemID= @OrderInvoiceGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceGemsItemID", DbType.String, OrderInvoiceGemsItemID)
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

        Public Function GetAllOrderInvoiceOrderList() As System.Data.DataTable Implements IOrderInvoiceDA.GetAllOrderInvoiceOrderList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "  Select O.OrderInvoiceID AS VoucherNo, convert(varchar(10),O.OrderDate,105) AS OrderDate, convert(varchar(10),O.DueDate,105) AS DueDate,C.CustomerName As [Customer_], C.CustomerAddress As [Address_], S.Staff AS [Staff_], " & _
                                 " IsNull(G.GoldQuality,'-') AS [PayGoldQuality_], O.OrderDate As  [@ODate], O.Remark As [Remark_], (AdvanceAmount+SecondAdvanceAmount) AS AdvanceAmount,(AllTotalAmount+AllAddOrSub) As NetAmount, " & _
                                 " CAST(PayGoldTK AS INT) AS PayGoldK," & _
                                 " CAST((PayGoldTK-CAST(PayGoldTK AS INT))*16 AS INT) AS PayGoldP," & _
                                 " CAST((((PayGoldTK-CAST(PayGoldTK AS INT))*16)-CAST((PayGoldTK-CAST(PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PayGoldY " & _
                                 "  from tbl_OrderInvoice O LEFT JOIN tbl_Customer C on C.CustomerID=O.CustomerID LEFT JOIN tbl_Staff S on S.StaffID=O.StaffID " & _
                                 " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=O.PayGoldQualityID " & _
                                 " Where O.IsCancel=0 And O.IsRetrieved=0 And O.IsDelete=0 ORDER BY [@ODate] DESC, OrderInvoiceID DESC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderInvoice(ByVal OrderInvoiceID As String) As CommonInfo.OrderInvoiceInfo Implements IOrderInvoiceDA.GetOrderInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objOrderInvoice As New OrderInvoiceInfo
            Try
                strCommandText = " SELECT  *,  "
                strCommandText += " CAST(PayGoldTK AS INT) AS PayGoldK,  "
                strCommandText += " CAST((PayGoldTK-CAST(PayGoldTK AS INT))*16 AS INT) AS PayGoldP,"
                strCommandText += " CAST((((PayGoldTK-CAST(PayGoldTK AS INT))*16)-CAST((PayGoldTK-CAST(PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS PayGoldY,"
                strCommandText += " CAST(((((PayGoldTK-CAST(PayGoldTK AS INT))*16)-CAST((PayGoldTK-CAST(PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((PayGoldTK-CAST(PayGoldTK AS INT))*16)-CAST((PayGoldTK-CAST(PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS PayGoldC "
                strCommandText += " FROM tbl_OrderInvoice  WHERE OrderInvoiceID= @OrderInvoiceID And IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                drResult = DB.ExecuteReader(DBComm)

                If drResult.Read() Then
                    With objOrderInvoice
                        .OrderInvoiceID = drResult.Item("OrderInvoiceID")
                        .OrderDate = drResult.Item("OrderDate")
                        .DueDate = drResult.Item("DueDate")
                        .StaffID = drResult.Item("StaffID")
                        .CustomerID = drResult.Item("CustomerID")
                        .PayGoldQualityID = drResult.Item("PayGoldQualityID")
                        .PayGoldTG = drResult.Item("PayGoldTG")
                        .PayGoldTK = drResult.Item("PayGoldTK")
                        .PayGoldK = drResult.Item("PayGoldK")
                        .PayGoldP = drResult.Item("PayGoldP")
                        .PayGoldY = drResult.Item("PayGoldY")
                        .PayGoldC = drResult.Item("PayGoldC")
                        .Remark = drResult.Item("Remark")
                        .AllTotalAmount = drResult.Item("AllTotalAmount")
                        .AllAddOrSub = drResult.Item("AllAddOrSub")
                        .AdvanceAmount = drResult.Item("AdvanceAmount")
                        .SecondAdvanceAmount = drResult.Item("SecondAdvanceAmount")
                        .SecondAdvanceDate = drResult.Item("SecondAdvanceDate")
                        .IsCancel = drResult.Item("IsCancel")
                        .IsRetrieved = drResult.Item("IsRetrieved")
                        .LocationID = drResult.Item("LocationID")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objOrderInvoice
        End Function

        Public Function InsertOrderInvoice(ByVal obj As CommonInfo.OrderInvoiceInfo) As Boolean Implements IOrderInvoiceDA.InsertOrderInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_OrderInvoice ( OrderInvoiceID, OrderDate, DueDate, StaffID, CustomerID, PayGoldQualityID, PayGoldTK, PayGoldTG, Remark, AllTotalAmount, AllAddOrSub, AdvanceAmount, SecondAdvanceAmount, SecondAdvanceDate, IsCancel, IsRetrieved, LocationID, LastModifiedLoginUserName, LastModifiedDate,IsDelete,IsSync)"
                strCommandText += " Values (@OrderInvoiceID, @OrderDate, @DueDate, @StaffID, @CustomerID, @PayGoldQualityID, @PayGoldTK, @PayGoldTG, @Remark, @AllTotalAmount, @AllAddOrSub, @AdvanceAmount, @SecondAdvanceAmount, @SecondAdvanceDate, @IsCancel, @IsRetrieved, @LocationID, @LastModifiedLoginUserName, @LastModifiedDate,0,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, obj.OrderInvoiceID)
                DB.AddInParameter(DBComm, "@OrderDate", DbType.DateTime, obj.OrderDate)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, obj.DueDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, obj.CustomerID)
                DB.AddInParameter(DBComm, "@PayGoldQualityID", DbType.String, obj.PayGoldQualityID)
                DB.AddInParameter(DBComm, "@PayGoldTK", DbType.Decimal, obj.PayGoldTK)
                DB.AddInParameter(DBComm, "@PayGoldTG", DbType.Decimal, obj.PayGoldTG)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@AllTotalAmount", DbType.Int64, obj.AllTotalAmount)
                DB.AddInParameter(DBComm, "@AllAddOrSub", DbType.Int32, obj.AllAddOrSub)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int32, obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@SecondAdvanceAmount", DbType.Int32, obj.SecondAdvanceAmount)
                DB.AddInParameter(DBComm, "@SecondAdvanceDate", DbType.Date, obj.SecondAdvanceDate)
                DB.AddInParameter(DBComm, "@IsCancel", DbType.Boolean, obj.IsCancel)
                DB.AddInParameter(DBComm, "@IsRetrieved", DbType.Boolean, False)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)


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

        Public Function InsertOrderInvoiceItem(ByVal obj As CommonInfo.OrderInvoiceGemsItemInfo) As Boolean Implements IOrderInvoiceDA.InsertOrderInvoiceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_OrderInvoiceGemsItem ( OrderInvoiceGemsItemID,OrderReceiveDetailID,GemsCategoryID,GemsName,GemsTK,YOrCOrG,GemsTW,GemsTG,Qty,UnitPrice,Type,Amount,IsCustomerGem)"
                strCommandText += " Values (@OrderInvoiceGemsItemID,@OrderReceiveDetailID,@GemsCategoryID,@GemsName,@GemsTK,@YOrCOrG,@GemsTW,@GemsTG,@Qty,@UnitPrice,@Type,@Amount,@IsCustomerGem)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceGemsItemID", DbType.String, obj.OrderInvoiceGemsItemID)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, obj.OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, obj.GemsTW)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, obj.Qty)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Type", DbType.String, obj.Type)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, obj.Amount)
                DB.AddInParameter(DBComm, "@IsCustomerGem", DbType.Boolean, obj.IsCustomerGem)

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
        Public Function UpdateOrderInvoice(ByVal obj As CommonInfo.OrderInvoiceInfo) As Boolean Implements IOrderInvoiceDA.UpdateOrderInvoice
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_OrderInvoice set  OrderInvoiceID=@OrderInvoiceID, OrderDate=@OrderDate, DueDate=@DueDate, StaffID=@StaffID, CustomerID=@CustomerID, PayGoldQualityID=@PayGoldQualityID, PayGoldTK=@PayGoldTK, PayGoldTG=@PayGoldTG, Remark=@Remark, AllTotalAmount=@AllTotalAmount, AllAddOrSub=@AllAddOrSub, AdvanceAmount=@AdvanceAmount, SecondAdvanceAmount=@SecondAdvanceAmount, SecondAdvanceDate=@SecondAdvanceDate, IsCancel=@IsCancel, LocationID=@LocationID, LastModifiedLoginUserName=@LastModifiedLoginUserName, LastModifiedDate=@LastModifiedDate,IsSync=0 "
                strCommandText += " where OrderInvoiceID= @OrderInvoiceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, obj.OrderInvoiceID)
                DB.AddInParameter(DBComm, "@OrderDate", DbType.DateTime, obj.OrderDate)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, obj.DueDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, obj.CustomerID)
                DB.AddInParameter(DBComm, "@PayGoldQualityID", DbType.String, obj.PayGoldQualityID)
                DB.AddInParameter(DBComm, "@PayGoldTK", DbType.Decimal, obj.PayGoldTK)
                DB.AddInParameter(DBComm, "@PayGoldTG", DbType.Decimal, obj.PayGoldTG)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@AllTotalAmount", DbType.Int64, obj.AllTotalAmount)
                DB.AddInParameter(DBComm, "@AllAddOrSub", DbType.Int32, obj.AllAddOrSub)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int32, obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@SecondAdvanceAmount", DbType.Int32, obj.SecondAdvanceAmount)
                DB.AddInParameter(DBComm, "@SecondAdvanceDate", DbType.Date, obj.SecondAdvanceDate)
                DB.AddInParameter(DBComm, "@IsCancel", DbType.Boolean, obj.IsCancel)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)


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


        Public Function UpdateOrderInvoiceItem(ByVal obj As CommonInfo.OrderInvoiceGemsItemInfo) As Boolean Implements IOrderInvoiceDA.UpdateOrderInvoiceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_OrderInvoiceGemsItem set OrderReceiveDetailID= @OrderReceiveDetailID ,OrderInvoiceID=@OrderInvoiceID, GemsCategoryID= @GemsCategoryID , GemsName= @GemsName , GemsTK=@GemsTK, YOrCOrG= @YOrCOrG , GemsTW= @GemsTW , GemsTG= @GemsTG ,Qty= @Qty , UnitPrice= @UnitPrice , Type= @Type , Amount= @Amount ,IsCustomerGem=@IsCustomerGem "
                strCommandText += " where OrderInvoiceGemsItemID= @OrderInvoiceGemsItemID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceGemsItemID", DbType.String, obj.OrderInvoiceGemsItemID)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, obj.OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, obj.OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, obj.GemsTK)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, obj.GemsTW)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, obj.GemsTG)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, obj.Qty)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Type", DbType.String, obj.Type)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, obj.Amount)
                DB.AddInParameter(DBComm, "@IsCustomerGem", DbType.Boolean, obj.IsCustomerGem)

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

        Public Function GetOrderInvoiceDetailReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IOrderInvoiceDA.GetOrderInvoiceDetailReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable


            Try
                strCommandText = " Select H.OrderInvoiceID,H.OrderDate,H.DueDate, H.PayGoldTK,H.PayGoldTG,H.Remark,H.AllTotalAmount,H.AllAddOrSub,H.AdvanceAmount+H.SecondAdvanceAmount As AdvanceAmount, H.IsRetrieved, H.OrderRetrieveDate, " & _
                                 " CAST(H.PayGoldTK AS INT) AS PayGoldK,  CAST((H.PayGoldTK-CAST(H.PayGoldTK AS INT))*16 AS INT) AS PayGoldP,   " & _
                                 " CAST((((H.PayGoldTK-CAST(H.PayGoldTK AS INT))*16)-CAST((H.PayGoldTK-CAST(H.PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PayGoldY, " & _
                                 " D.OrderReceiveDetailID, N.ItemName,I.ItemCategory,G.GoldQuality,D.Length,D.Width,D.OrderRate, D.GoldTK, CAST((D.GoldTG) AS DECIMAL(18,3)) AS GoldTG, CAST(D.GoldTK AS INT) AS GoldK, CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT) AS GoldP,   " & _
                                 " CAST((((D.GoldTK-CAST(D.GoldTK AS INT))*16)-CAST((D.GoldTK-CAST(D.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                                 " D.TotalGemTK, D.TotalGemTG, CAST(D.TotalGemTK AS INT) AS TotalGemK, CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT) AS TotalGemP, CAST((((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16)-CAST((D.TotalGemTK-CAST(D.TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemY," & _
                                 " (D.TotalAmount+D.AddOrSub) AS ItemNetAmount, 0 AS TotalNetAmount, F.IsDiamond, " & _
                                 " S.Staff, C.CustomerName,C.CustomerAddress,C.CustomerTel, 0 AS TotalAdvanceAmount, H.OrderDate As [@ODate], D.IsBarcode, IsNull(F.ItemCode,'-') AS ItemCode, F.IsExit, F.ExitDate,  " & _
                                 " OG.OrderInvoiceGemsItemID, OG.GemsCategoryID, GC.GemsCategory+ CASE OG.GemsName WHEN '-' THEN '' ELSE '/'+ OG.GemsName END As GemsCategory, OG.GemsTK, OG.GemsTG, OG.YOrCOrG, OG.GemsTW, OG.Qty, OG.UnitPrice, OG.Type, OG.Amount, CASE OG.IsCustomerGem WHEN 0 THEN N'ဆိုင်ကျောက်' ELSE N'ပေးကျောက်' END AS CustomerGem, " & _
                                 " CAST(OG.GemsTK AS INT) AS GemsK,  CAST((OG.GemsTK-CAST(OG.GemsTK AS INT))*16 AS INT) AS GemsP,   " & _
                                 " CAST((((OG.GemsTK-CAST(OG.GemsTK AS INT))*16)-CAST((OG.GemsTK-CAST(OG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY,D.GoldSmithID,GS.Name as GoldSmith " & _
                                 " From tbl_OrderReceiveDetail D  LEFT JOIN tbl_OrderInvoice H on D.OrderInvoiceID=H.OrderInvoiceID   " & _
                                 " LEFT JOIN tbl_OrderInvoiceGemsItem OG ON OG.OrderReceiveDetailID=D.OrderReceiveDetailID " & _
                                 " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=OG.GemsCategoryID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.OrderReceiveDetailID=D.OrderReceiveDetailID " & _
                                 " LEFT JOIN tbl_GoldSmith GS ON D.GoldSmithID=GS.GoldSmithID " & _
                                 " LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID  LEFT JOIN tbl_Customer C on C.CustomerID=H.CustomerID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID " & _
                                 " LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=D.GoldQualityID " & cristr

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


        Public Function GetOrderInvoicePrint(ByVal OrderInvoiceID As String) As System.Data.DataTable Implements IOrderInvoiceDA.GetOrderInvoicePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select O.OrderInvoiceID,O.ItemName,O.CustomerID,C.CustomerName,C.CustomerAddress,O.Width,O.OrderDate, O.DueDate, O.StaffID, Staff, O.QTY, O.PayGoldQualityID, GQ.GoldQuality As PayGoldQuality,N.Photo,O.DesignCharges,   " & _
                                 " CAST(O.PayGoldTK AS INT) AS PayGoldK, CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT) AS PayGoldP," & _
                                 " CAST((((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16)-CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS PayGoldY," & _
                                 " CAST(O.EstimateGoldTK AS INT) AS EstimateGoldK, CAST((O.EstimateGoldTK-CAST(O.EstimateGoldTK AS INT))*16 AS INT) AS EstimateGoldP, " & _
                                 " CAST((((O.EstimateGoldTK-CAST(O.EstimateGoldTK AS INT))*16)-CAST((O.EstimateGoldTK-CAST(O.EstimateGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS EstimateGoldY," & _
                                 " CAST(O.WasteGoldTK AS INT) AS WasteK, CAST((O.WasteGoldTK-CAST(O.WasteGoldTK AS INT))*16 AS INT) AS WasteP," & _
                                 " CAST((((O.WasteGoldTK-CAST(O.WasteGoldTK AS INT))*16)-CAST((O.WasteGoldTK-CAST(O.WasteGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS WasteY," & _
                                 " CAST(O.GemsTK AS INT) AS GemsK, CAST((O.GemsTK-CAST(O.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((O.GemsTK-CAST(O.GemsTK AS INT))*16)-CAST((O.GemsTK-CAST(O.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, " & _
                                 " CAST(O.TotalTK AS INT) AS TotalK, CAST((O.TotalTK-CAST(O.TotalTK AS INT))*16 AS INT) AS TotalP," & _
                                 " CAST((((O.TotalTK-CAST(O.TotalTK AS INT))*16)-CAST((O.TotalTK-CAST(O.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY, " & _
                                 " O.PayGoldTK, O.PayGoldTG, O.EstimateGoldTK, O.EstimateGoldTG, O.WasteGoldTK, O.WasteGoldTG, O.GemsTK, O.GemsTG, O.TotalTK, O.TotalTG,OG.QTY AS GemsQTY,OG.YOrCOrG,OG.Amount As GemsAmount, " & _
                                 " OG.OrderInvoiceGemsItemID, OG.GemsCategoryID, G.GemsCategory, OG.GemsName, D.ItemCode, O.TotalAmount, O.AddOrSub, O.AdvanceAmount, O.SecondAdvanceAmount, I.ItemCategory,O.GoldPrice,O.GemsPrice,A.GoldQuality,OG.IsShopGems,O.OrderRate, ((O.TotalAmount+O.AddOrSub)-(O.AdvanceAmount+O.SecondAdvanceAmount)) As BalanceAmount, " & _
                                 " OG.GemsTK AS ItemGemsTK, OG.GemsTG As ItemGemsTG, " & _
                                 " CAST(OG.GemsTK AS INT) AS ItemGemsK, CAST((OG.GemsTK-CAST(OG.GemsTK AS INT))*16 AS INT) AS ItemGemsP," & _
                                 " CAST((((OG.GemsTK-CAST(OG.GemsTK AS INT))*16)-CAST((OG.GemsTK-CAST(OG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS ItemGemsY  " & _
                                 " From tbl_OrderInvoice O  Left Join tbl_GoldQuality A on A.GoldQualityID=O.GoldQualityID " & _
                                 " Left Join tbl_Customer C on C.CustomerID=O.CustomerID Left Join tbl_OrderReturnDetail D on D.OrderInvoiceID=O.OrderInvoiceID " & _
                                 " Left Join tbl_OrderInvoiceGemsItem OG On OG.OrderInvoiceID=O.OrderInvoiceID Left Join tbl_ForSale N on N.ForSaleID=D.ForSaleID " & _
                                 " Left Join tbl_ItemCategory I on I.ItemCategoryID=N.ItemCategoryID  Left Join tbl_GemsCategory G on G.GemsCategoryID = OG.GemsCategoryID " & _
                                 " LEFT JOIN tbl_GoldQuality GQ ON O.PayGoldQualityID=GQ.GoldQualityID " & _
                                 " LEFT JOIN tbl_Staff ST ON ST.StaffID=O.StaffID " & _
                                 " WHERE O.OrderInvoiceID=@OrderInvoiceID And O.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetOrderReturnPrint(ByVal OrderReturnHeaderID As String) As System.Data.DataTable Implements IOrderInvoiceDA.GetOrderReturnPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.OrderReturnGemID,G.GemsCategoryID,GC.GemsCategory,G.GemsName,G.GemstK,G.GemsTK,G.YOrCOrG,G.GemsTW,G.Qty,G.SaleType,G.UnitPrice,G.Amount As GemsAmount,  " & _
                                " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,    " & _
                                " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY,  " & _
                                " D.OrderInvoiceDetailID,D.ForSaleID,D.ItemCode,D.SalesRate,D.GoldPrice,D.GemsPrice,D.TotalAmount As ItemTotalAmount,D.AddOrSub As ItemAddOrSub,  " & _
                                " (D.TotalAmount+D.AddOrSub) As ItemNetAmount,(F.DesignCharges+F.PlatingCharges+F.MountingCharges+F.WhiteCharges) As TotalCharges, F.Photo, " & _
                                " F.ItemNameID,F.ItemCategoryID,F.GoldQualityID,(F.ItemTK-F.GemsTK) AS GoldTK,(F.ItemTG-F.GemsTG) AS GoldTG, F.GemsTK As TotalGemsTK,F.GemsTG As TotalGemsTG,F.WasteTK,F.WasteTG,F.ItemTK,F.ItemTG,(F.GoldTK+F.WasteTK) As TotalTK,(F.GoldTG+F.WasteTG) As TotalTG,O.PayGoldTK,O.PayGoldTG,  " & _
                                " CAST(F.ItemTK AS INT) AS ItemK, CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP,    " & _
                                " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,    " & _
                                " CAST(F.GemsTK AS INT) AS TotalGemsK, CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT) AS TotalGemsP,    " & _
                                " CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS TotalGemsY,    " & _
                                " CAST(F.WasteTK AS INT) AS WasteK,  CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT) AS WasteP,     " & _
                                " CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,    " & _
                                " CAST(F.GoldTK+F.WasteTK AS INT) AS TotalK,  CAST((F.GoldTK+F.WasteTK-CAST(F.GoldTK+F.WasteTK AS INT))*16 AS INT) AS TotalP,     " & _
                                " CAST((((F.GoldTK+F.WasteTK-CAST(F.GoldTK+F.WasteTK AS INT))*16)-CAST((F.GoldTK+F.WasteTK-CAST(F.GoldTK+F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY,   " & _
                                " CAST((F.ItemTK-F.GemsTK) AS INT) AS GoldK,  CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT) AS GoldP,    " & _
                                " CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY,    " & _
                                " CAST(O.PayGoldTK AS INT) AS PayGoldK,  CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT) AS PayGoldP,   " & _
                                " CAST((((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16)-CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PayGoldY,  " & _
                                " (((F.ItemTK-F.GemsTK) +F.WasteTK)- O.PayGoldTK) As AddGoldTK,(((F.ItemTG-F.GemsTG) +F.WasteTG)-O.PayGoldTG) As AddGoldTG ,  " & _
                                " CAST((((F.ItemTK-F.GemsTK) +F.WasteTK)-O.PayGoldTK) AS INT) AS AddGoldK, CAST(((((F.ItemTK-F.GemsTK) +F.WasteTK)-O.PayGoldTK)-CAST((((F.ItemTK-F.GemsTK) +F.WasteTK)-O.PayGoldTK) AS INT))*16 AS INT) AS AddGoldP,   " & _
                                " CAST(((((((F.ItemTK-F.GemsTK) +F.WasteTK)-O.PayGoldTK)-CAST((((F.ItemTK-F.GemsTK) +F.WasteTK)-O.PayGoldTK) AS INT))*16)-CAST(((((F.ItemTK-F.GemsTK) +F.WasteTK)-O.PayGoldTK)-CAST((((F.ItemTK-F.GemsTK) +F.WasteTK)-O.PayGoldTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS AddGoldY, " & _
                                " H.OrderReturnHeaderID,H.OrderInvoiceID,H.AllTotalAmount,H.AllAddOrSub as AllAddOrSub,((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount) As NetAmount,H.FromGoldAmount,H.DiscountAmount,H.AllTaxAmt,G.GemTax,G.GemTaxPer,  " & _
                                " H.BalanceAmount, H.PaidAmount, H.AdvanceAmount, H.IsAddGold, H.StaffID, H.ReturnDate, D.OrderReturnHeaderID,  " & _
                                " I.ItemCategory, N.ItemName, Q.GoldQuality,Q.IsGramRate, S.Staff, C.CustomerName, C.CustomerAddress, C.CustomerTel, O.CustomerID,O.OrderDate, F.Length, F.Width,F.IsDiamond ,H.AddGoldTaxPer,H.AddGoldTax,D.ItemTax,D.ItemTaxPer,F.DesignCharges,F.PlatingCharges,F.MountingCharges,F.WhiteCharges ,H.Remark,F.GoldSmith,F.OriginalCode,'' as QRCode,F.FixPrice,F.IsFixPrice,0 As SaleRate,'' as VouNo,O.PayGoldQualityID,(Select Staff from tbl_Staff S Inner Join tbl_OrderInvoice O On O.StaffID=S.StaffID Where O.OrderInvoiceID In (select OrderInvoiceID from tbl_OrderReturnHeader where OrderReturnHeaderID=@OrderReturnHeaderID)) as ReceiveStaff " & _
                                " From tbl_OrderReturnDetail D LEFT JOIN tbl_OrderReturnGemsItem G on D.OrderInvoiceDetailID=G.OrderInvoiceDetailID  " & _
                                " LEFT JOIN tbl_OrderReturnHeader H on H.OrderReturnHeaderID=D.OrderReturnHeaderID  " & _
                                " LEFT JOIN tbl_OrderInvoice O on O.OrderInvoiceID=H.OrderInvoiceID   " & _
                                " LEFT JOIN tbl_ForSale F on F.ForSaleId=D.ForSaleID LEFT JOIN tbl_GemsCategory GC on GC.GemsCategoryID=G.GemsCategoryID   " & _
                                " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID   " & _
                                " LEFT JOIN tbl_GoldQuality Q on Q.GoldQualityID=F.GoldQualityID LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID   " & _
                                " LEFT JOIN tbl_Customer C on C.CustomerID=O.CustomerID " & _
                               " WHERE H.OrderReturnHeaderID=@OrderReturnHeaderID And H.IsDelete=0 ORDER BY D.OrderInvoiceDetailID "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, OrderReturnHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertOrderInvoiceDetail(ByVal obj As CommonInfo.OrderInvoiceDetailInfo) As Boolean Implements IOrderInvoiceDA.InsertOrderInvoiceDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_OrderReturnDetail ( OrderInvoiceDetailID, OrderReturnHeaderID, ForSaleID, ItemCode, SalesRate, GoldPrice, GemsPrice, TotalAmount, AddOrSub, IsOriginalFixedPrice, OriginalFixedPrice, IsOriginalPriceGram, OriginalPriceGram, OriginalPriceTK, OriginalGemsPrice, OriginalOtherPrice, PurchaseWasteTK, PurchaseWasteTG, IsReturn,ItemTaxPer,ItemTax)"
                strCommandText += " Values (@OrderInvoiceDetailID, @OrderReturnHeaderID, @ForSaleID, @ItemCode, @SalesRate, @GoldPrice, @GemsPrice, @TotalAmount, @AddOrSub, @IsOriginalFixedPrice, @OriginalFixedPrice, @IsOriginalPriceGram, @OriginalPriceGram, @OriginalPriceTK, @OriginalGemsPrice, @OriginalOtherPrice, @PurchaseWasteTK, @PurchaseWasteTG, @IsReturn,@ItemTaxPer,@ItemTax)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceDetailID", DbType.String, obj.OrderInvoiceDetailID)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, obj.OrderReturnHeaderID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, obj.SalesRate)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, obj.GemsPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, obj.AddOrSub)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceGram", DbType.Boolean, obj.IsOriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, obj.OriginalGemsPrice)
                DB.AddInParameter(DBComm, "@OriginalOtherPrice", DbType.Int64, obj.OriginalOtherPrice)
                DB.AddInParameter(DBComm, "@PurchaseWasteTK", DbType.Decimal, obj.PurchaseWasteTK)
                DB.AddInParameter(DBComm, "@PurchaseWasteTG", DbType.Decimal, obj.PurchaseWasteTG)
                DB.AddInParameter(DBComm, "@IsReturn", DbType.Boolean, False)
                DB.AddInParameter(DBComm, "@ItemTaxPer", DbType.Decimal, obj.ItemTaxPer)
                DB.AddInParameter(DBComm, "@ItemTax", DbType.Int64, obj.ItemTax)

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

        Public Function UpdateOrderInvoiceDetail(ByVal obj As CommonInfo.OrderInvoiceDetailInfo) As Boolean Implements IOrderInvoiceDA.UpdateOrderInvoiceDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_OrderReturnDetail set  OrderInvoiceDetailID=@OrderInvoiceDetailID, OrderReturnHeaderID=@OrderReturnHeaderID, ForSaleID=@ForSaleID, ItemCode=@ItemCode, SalesRate=@SalesRate, GoldPrice=@GoldPrice, GemsPrice=@GemsPrice, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub , IsOriginalFixedPrice=@IsOriginalFixedPrice, OriginalFixedPrice=@OriginalFixedPrice, IsOriginalPriceGram=@IsOriginalPriceGram, OriginalPriceGram=@OriginalPriceGram, OriginalPriceTK=@OriginalPriceTK, OriginalGemsPrice=@OriginalGemsPrice, OriginalOtherPrice=@OriginalOtherPrice, PurchaseWasteTK=@PurchaseWasteTK, PurchaseWasteTG=@PurchaseWasteTG ,ItemTaxPer=@ItemTaxPer,ItemTax=@ItemTax "
                strCommandText += " where OrderInvoiceDetailID= @OrderInvoiceDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceDetailID", DbType.String, obj.OrderInvoiceDetailID)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, obj.OrderReturnHeaderID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, obj.ItemCode)
                DB.AddInParameter(DBComm, "@SalesRate", DbType.Int64, obj.SalesRate)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, obj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemsPrice", DbType.Int64, obj.GemsPrice)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, obj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, obj.AddOrSub)
                DB.AddInParameter(DBComm, "@IsOriginalFixedPrice", DbType.Boolean, obj.IsOriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@IsOriginalPriceGram", DbType.Boolean, obj.IsOriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, obj.OriginalGemsPrice)
                DB.AddInParameter(DBComm, "@OriginalOtherPrice", DbType.Int64, obj.OriginalOtherPrice)
                DB.AddInParameter(DBComm, "@PurchaseWasteTK", DbType.Decimal, obj.PurchaseWasteTK)
                DB.AddInParameter(DBComm, "@PurchaseWasteTG", DbType.Decimal, obj.PurchaseWasteTG)
                DB.AddInParameter(DBComm, "@ItemTaxPer", DbType.Decimal, obj.ItemTaxPer)
                DB.AddInParameter(DBComm, "@ItemTax", DbType.Int64, obj.ItemTax)
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

        Public Function GetOrderReturnDetailReport(FromDate As Date, ToDate As Date, IsReturn As Boolean, Optional cristr As String = "") As Object Implements IOrderInvoiceDA.GetOrderReturnDetailReport
            Dim strCommandText As String

            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select D.OrderInvoiceDetailID,F.ForSaleID,D.ItemCode,D.SalesRate,D.GoldPrice,D.GemsPrice,D.TotalAmount As ItemTotalAmount," & _
                                 " D.AddOrSub As ItemAddOrSub,(D.TotalAmount+D.AddOrSub) As ItemNetAmount," & _
                                 " F.Length,F.GoldQualityID,F.ItemNameID,F.ItemCategoryID,(F.ItemTK-F.GemsTK) AS GoldTK, (F.ItemTG-F.GemsTG) AS GoldTG, F.GemsTK,F.GemsTG,F.WasteTK As ReturnWasteTK,F.WasteTG As ReturnWasteTG,F.ItemTK, CAST((F.ItemTG) AS DECIMAL(18,3)) AS ItemTG," & _
                                 " (F.ItemTK+F.WasteTK) As TotalTK, (F.ItemTG+F.WasteTG) As TotalTG,F.Width,F.IsFixPrice,F.FixPrice,F.DesignCharges,F.PlatingCharges,F.MountingCharges,F.WhiteCharges," & _
                                 " (F.DesignCharges+F.PlatingCharges+F.MountingCharges+F.WhiteCharges) As TotalCharges,I.ItemCategory,N.ItemName,G.GoldQuality," & _
                                 " CAST(F.GemsTK AS INT) AS GemsK, CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                                 " CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY," & _
                                 " CAST(F.ItemTK AS INT) AS ItemK,CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                 " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                 " CAST((F.ItemTK-F.GemsTK) AS INT) AS GoldK, CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT) AS GoldP," & _
                                 " CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY," & _
                                 " CAST((F.ItemTK+F.WasteTK) AS INT) AS TotalK, CAST(((F.ItemTK+F.WasteTK)-CAST((F.ItemTK+F.WasteTK) AS INT))*16 AS INT) AS TotalP," & _
                                 " CAST(((((F.ItemTK+F.WasteTK)-CAST((F.ItemTK+F.WasteTK) AS INT))*16)-CAST(((F.ItemTK+F.WasteTK)-CAST((F.ItemTK+F.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY," & _
                                 " CAST(F.WasteTK AS INT) AS ReturnWasteK, CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT) AS ReturnWasteP," & _
                                 " CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ReturnWasteY," & _
                                 " H.OrderReturnHeaderID, H.ReturnDate,H.OrderInvoiceID,H.AllTotalAmount, H.AllAddOrSub,H.FromGoldAmount,H.StaffID,H.IsAddGold, (H.DiscountAmount-H.AllAddOrSub) AS DiscountAmount," & _
                                 " H.BalanceAmount, H.PaidAmount,H.AdvanceAmount,0 As SumTotalAmount,(H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount As AllNetAmount," & _
                                 " CAST(GI.GemsTK AS INT) AS GemK, CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT) AS GemP, " & _
                                 " CAST((((GI.GemsTK-CAST(GI.GemsTK AS INT))*16)-CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemY," & _
                                 " GI.OrderReturnGemID,GI.GemsCategoryID,GC.GemsCategory,GI.GemsName,GI.GemsTK As GemTK,GI.GemsTG As GemTG,GI.YOrCOrG,GI.GemsTW,GI.Qty As GemsQTY,GI.SaleType As Type,GI.UnitPrice,GI.Amount As GemsAmount,GI.GemsRemark,OH.IsRetrieved, C.CustomerName, OH.CustomerID, C.CustomerAddress, C.CustomerTel, S.Staff, F.IsDiamond ,D.ItemTaxPer,D.ItemTax,GI.GemTaxPer,GI.GemTax" & _
                                 " From tbl_OrderReturnDetail D  LEFT JOIN tbl_OrderReturnGemsItem GI on GI.OrderInvoiceDetailID=D.OrderInvoiceDetailID " & _
                                 " LEFT JOIN tbl_OrderReturnHeader H  on H.OrderReturnHeaderID=D.OrderReturnHeaderID LEFT JOIN tbl_ForSale F on F.ForSaleID=D.ForSaleID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID " & _
                                 " LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID LEFT JOIN tbl_OrderInvoice OH on OH.OrderInvoiceID=H.OrderInvoiceID  LEFT JOIN tbl_Customer C on C.CustomerID=OH.CustomerID  " & _
                                 " LEFT JOIN tbl_GemsCategory GC on GC.GemsCategoryID=GI.GemsCategoryID " & cristr & " Order by H.ReturnDate desc"
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

        Public Function GetOrderInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As OrderInvoiceDetailInfo Implements IOrderInvoiceDA.GetOrderInvoiceDetailForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New OrderInvoiceDetailInfo
            Try

                strCommandText = " Select Count(F.ForSaleID) As Qty,Sum(CAST((F.ItemTG) AS DECIMAL(18,3)))As ItemTG,Sum(F.ItemTK) As ItemTK, Sum(CAST((F.GemsTG) AS DECIMAL(18,3))) As GemsTG, " & _
                                 " Sum(F.GemsTK)As GemsTK,  Sum(CAST(F.WasteTG As DECIMAL(18,3))) As WasteTG,Sum(F.WasteTK) As WasteTK,Sum(CAST((F.ItemTG+F.WasteTG) AS DECIMAL(18,3))) As TotalTG, " & _
                                 " Sum(F.ItemTK+F.WasteTK)As TotalTK,Sum(CAST((F.ItemTG-F.GemsTG) AS DECIMAL(18,3))) As GoldTG,Sum(F.ItemTK-F.GemsTK) As GoldTK, Sum(D.TotalAmount+D.AddOrSub) As ItemAmount ,Sum(H.AllTaxAmt) as TaxAmount" & _
                                 " From tbl_OrderReturnDetail D LEFT JOIN tbl_OrderReturnHeader H on H.OrderReturnHeaderID=D.OrderReturnHeaderID  LEFT JOIN tbl_OrderInvoice OH on OH.OrderInvoiceID=H.OrderInvoiceID " & _
                                 " LEFT JOIN tbl_ForSale F on F.ForSaleID=D.ForSaleID LEFT Join tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID  " & _
                                 " LEFT Join tbl_ItemName N on N.ItemNameID = F.ItemNameID LEFT Join tbl_GoldQuality Q on Q.GoldQualityID=F.GoldQualityID " & _
                                 " LEFT Join tbl_Staff S on S.StaffID=H.StaffID  LEFT JOIN tbl_Customer C on C.CustomerID=OH.CustomerID " & criStr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .QTY = IIf(IsDBNull(drResult("QTY")), 0, drResult("QTY"))
                        .ItemTG = IIf(IsDBNull(drResult("ItemTG")), 0, drResult("ItemTG"))
                        .ItemTK = IIf(IsDBNull(drResult("ItemTK")), 0, drResult("ItemTK"))
                        .GoldTG = IIf(IsDBNull(drResult("GoldTG")), 0, drResult("GoldTG"))
                        .GoldTK = IIf(IsDBNull(drResult("GoldTK")), 0, drResult("GoldTK"))
                        .WasteTG = IIf(IsDBNull(drResult("WasteTG")), 0, drResult("WasteTG"))
                        .WasteTK = IIf(IsDBNull(drResult("WasteTK")), 0, drResult("WasteTK"))
                        .GemsTG = IIf(IsDBNull(drResult("GemsTG")), 0, drResult("GemsTG"))
                        .GemsTK = IIf(IsDBNull(drResult("GemsTK")), 0, drResult("GemsTK"))
                        .TotalTG = IIf(IsDBNull(drResult("TotalTG")), 0, drResult("TotalTG"))
                        .TotalTK = IIf(IsDBNull(drResult("TotalTK")), 0, drResult("TotalTK"))
                        .ItemAmount = IIf(IsDBNull(drResult("ItemAmount")), 0, drResult("ItemAmount"))
                        '  .TaxAmount = IIf(IsDBNull(drResult("ItemTax")), 0, drResult("ItemTax"))
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function


        Public Function GetOrderInvoiceDataByHeaderIDAndItemCode(OrderInvoiceID As String, Optional ItemCode As String = "", Optional ByVal argForSaleIDStr As String = "") As DataTable Implements IOrderInvoiceDA.GetOrderInvoiceDataByHeaderIDAndItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select S.IsFixPrice as [$FixPrice],O.OrderInvoiceDetailID as [@SaleInvoiceDetailID],O.ForSaleID as [@ForSaleID],O.ItemCode,I.ItemCategory,N.ItemName,G.GoldQuality,O.SalesRate,(O.TotalAmount +O.AddorSub) as TotalAmount," & _
                                 " S.ItemTK,S.ItemTG, " & _
                                 " CAST(S.ItemTK AS INT) AS ItemK," & _
                                 " CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP," & _
                                 " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY," & _
                                 " (S.ItemTK - S.GemsTK) as GoldTK,(S.ItemTG - S.GemsTG) as GoldTG, " & _
                                 " CAST((S.ItemTK - S.GemsTK) AS INT) AS GoldK," & _
                                 " CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT) AS GoldP," & _
                                 " CAST(((((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16)-CAST(((S.ItemTK - S.GemsTK)-CAST((S.ItemTK - S.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY," & _
                                 " S.GemsTK,S.GemsTG," & _
                                 " CAST(S.GemsTK AS INT) AS GemsK," & _
                                 " CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT) AS GemsP," & _
                                 " CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY," & _
                                 " S.WasteTK, S.WasteTG," & _
                                 " CAST(S.WasteTK AS INT) AS WasteK," & _
                                 " CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT) AS WasteP," & _
                                 " CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY," & _
                                 " S.ItemCategoryID as [@ItemCategoryID],S.ItemNameID as [@ItemNameID],S.GoldQualityID as [@GoldQualityID],S.IsFixPrice as [@IsFixPrice],S.FixPrice,S.Length from tbl_OrderReturnDetail O" & _
                                 " Left Join tbl_ForSale S on S.ForSaleID = O.ForSaleID " & _
                                 " Left Join tbl_GoldQuality G On G.GoldQualityID = S.GoldQualityID" & _
                                 " Left Join tbl_ItemCategory I On I.ItemCategoryID = S.ItemCategoryID" & _
                                 " Left Join tbl_ItemName N On N.ItemNameID = S.ItemNameID" & _
                                 " where O.OrderReturnHeaderID = @OrderInvoiceID AND IsReturn=0 " & ItemCode

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderInvoiceGemDataByOrderDetailID(OrderInvoiceDetailID As String) As DataTable Implements IOrderInvoiceDA.GetOrderInvoiceGemDataByOrderDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select  '' as PurchaseGemID, '' AS PurchaseDetailID, G.GemsCategoryID, " & _
                                 " G.GemsName,G.GemsTK, G.GemsTG, YOrCOrG, GemsTW as GemTW, G.Qty as QTY, G.SaleType as FixType, 0 AS Discount, G.UnitPrice as PurchaseRate, G.Amount, " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY,G.GemTax,G.GemTaxPer " & _
                                 " From tbl_OrderReturnGemsItem G " & _
                                 " Where G.OrderInvoiceDetailID = '" & OrderInvoiceDetailID & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertOrderReturnGemItem(ByVal Obj As CommonInfo.OrderReturnGemsItemInfo) As Boolean Implements IOrderInvoiceDA.InsertOrderReturnGemItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_OrderReturnGemsItem ( OrderReturnGemID, OrderInvoiceDetailID, GemsCategoryID, GemsName, GemsTK, GemsTG, YOrCOrG, GemsTW, Qty, SaleType, UnitPrice, Amount, GemsRemark,GemTaxPer,GemTax)"
                strCommandText += " VALUES(@OrderReturnGemID, @OrderInvoiceDetailID, @GemsCategoryID, @GemsName, @GemsTK, @GemsTG, @YOrCOrG, @GemsTW, @Qty, @SaleType, @UnitPrice, @Amount, @GemsRemark,@GemTaxPer,@GemTax)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnGemID", DbType.String, Obj.OrderReturnGemID)
                DB.AddInParameter(DBComm, "@OrderInvoiceDetailID", DbType.String, Obj.OrderInvoiceDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, Obj.GemsName)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemsTW)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
                DB.AddInParameter(DBComm, "@SaleType", DbType.String, Obj.SaleType)
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

        Public Function UpdateOrderInvoiceDetailGem(ByVal Obj As CommonInfo.OrderInvoiceDetailGemInfo) As Boolean Implements IOrderInvoiceDA.UpdateOrderInvoiceDetailGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_OrderInvoiceDetailGems set OrderInvoiceDetailID=@OrderInvoiceDetailID, GemsCategoryID=@GemsCategoryID, GemsName=@GemsName, GemsTK=@GemsTK, GemsTG=@GemsTG, YOrCOrG=@YOrCOrG, GemsTW=@GemsTW, Qty=@Qty, Type=@Type, UnitPrice=@UnitPrice, Amount=@Amount, GemsRemark=@GemsRemark"
                strCommandText += " where OrderInvoiceDetailGemsID=@OrderInvoiceDetailGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceDetailGemsID", DbType.String, Obj.OrderInvoiceDetailGemsID)
                DB.AddInParameter(DBComm, "@OrderInvoiceDetailID", DbType.String, Obj.OrderInvoiceDetailID)
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
        Public Function DeleteOrderReturnGemsItemByGemsID(ByVal OrderReturnGemID As String) As Boolean Implements IOrderInvoiceDA.DeleteOrderReturnGemsItemByGemsID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_OrderReturnGemsItem WHERE  OrderReturnGemID= @OrderReturnGemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnGemID", DbType.String, OrderReturnGemID)
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

        Public Function DeleteOrderReceiveDetailGemsItemByGemsID(ByVal OrderInvoiceGemsItemID As String) As Boolean Implements IOrderInvoiceDA.DeleteOrderReceiveDetailGemsItemByGemsID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_OrderInvoiceGemsItem WHERE  OrderInvoiceGemsItemID= @OrderInvoiceGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceGemsItemID", DbType.String, OrderInvoiceGemsItemID)
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

        Public Function GetOrderInvoiceReportForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As OrderInvoiceInfo Implements IOrderInvoiceDA.GetOrderInvoiceReportForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New OrderInvoiceInfo
            Try

                strCommandText = " Select Count(H.OrderInvoiceID) As QTY,  Sum(CAST((D.GoldTG) AS DECIMAL(18,3))) AS GoldTG, Sum(D.GoldTK) AS GoldTK, Sum(CAST((D.TotalGemTG) AS DECIMAL(18,3))) AS TotalGemTG, Sum(D.TotalGemTK) AS TotalGemTK, " & _
                                 " SUM(H.AllTotalAmount) AS TotalAmount,SUM(H.AllTotalAmount+H.AllAddOrSub) As NetAmount  " & _
                                 " From tbl_OrderInvoice H LEFT JOIN tbl_OrderReceiveDetail D on H.OrderInvoiceID=D.OrderInvoiceID " & criStr & "And H.IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .QTY = IIf(IsDBNull(drResult("QTY")), 0, drResult("QTY"))
                        .EstimateGoldTG = IIf(IsDBNull(drResult("GoldTG")), 0, drResult("GoldTG"))
                        .EstimateGoldTK = IIf(IsDBNull(drResult("GoldTK")), 0, drResult("GoldTK"))
                        .GemsTG = IIf(IsDBNull(drResult("TotalGemTG")), 0, drResult("TotalGemTG"))
                        .GemsTK = IIf(IsDBNull(drResult("TotalGemTK")), 0, drResult("TotalGemTK"))
                        .NetAmount = IIf(IsDBNull(drResult("TotalAmount")), 0, drResult("TotalAmount"))
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
        Public Function GetOrderReturnReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IOrderInvoiceDA.GetOrderReturnReportForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select Distinct(H.OrderReturnHeaderID), H.AllTotalAmount As TotalAmount, H.AllAddOrSub As AddOrSub,((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount) As NetAmount, H.DiscountAmount, H.BalanceAmount, CASE WHEN H.PaidAmount<0 THEN H.PaidAmount ELSE (H.PaidAmount+H.AdvanceAmount+H.FromGoldAmount) END AS PaidAmount ,H.AllTaxAmt" & _
                                 " FROM tbl_OrderReturnDetail D LEFT JOIN  tbl_OrderReturnHeader H ON H.OrderReturnHeaderID=D.OrderReturnHeaderID LEFT JOIN tbl_OrderInvoice OH ON OH.OrderInvoiceID=H.OrderInvoiceID " & _
                                 " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=F.GoldQualityID " & _
                                 " left join tbl_Customer Cus on OH.CustomerID=Cus.CustomerID   LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID  LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID " & criStr

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

        Public Function GetOrderReturnSummaryReport(FromDate As Date, ToDate As Date, IsReturn As Boolean, Optional cristr As String = "") As Object Implements IOrderInvoiceDA.GetOrderReturnSummaryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = " Select  H.OrderReturnHeaderID,H.ReturnDate,H.OrderInvoiceID,H.StaffID,H.IsAddGold,D.OrderInvoiceDetailID,D.ForSaleID,D.ItemCode,F.ItemTK,Cast((F.ItemTG) As DECIMAL(18,3)) As ItemTG," & _
                                 " F.WasteTK As ReturnWasteTK,Cast((F.WasteTG) As DECIMAL(18,3)) As ReturnWasteTG,F.ItemNameID,F.GoldQualityID,F.ItemCategoryID,N.ItemName,I.ItemCategory,G.GoldQuality,C.CustomerName,C.CustomerAddress,C.CustomerTel,H.AllTaxAmt " & _
                                 " From tbl_OrderReturnHeader H LEFT JOIN tbl_OrderReturnDetail D on D.OrderReturnHeaderID=H.OrderReturnHeaderID  LEFT JOIN tbl_OrderInvoice OH on OH.OrderInvoiceID=H.OrderInvoiceID " & _
                                 " LEFT JOIN tbl_ForSale F on F.ForSaleID=D.ForSaleID LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID  LEFT JOIN tbl_Customer C on C.CustomerID=OH.CustomerID " & cristr & " And H.IsDelete=0 "

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

        Public Function GerOrderSummaryReport(FromDate As Date, ToDate As Date, IsReturn As Boolean, Optional cristr As String = "") As Object Implements IOrderInvoiceDA.GerOrderSummaryReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select  H.OrderInvoiceID, H.OrderDate, S.Staff,C.CustomerName,H.PayGoldQualityID,H.PayGoldTK,CAST((PayGoldTG) AS DECIMAL(18,3)) As PayGoldTG,  " & _
                                 " H.IsRetrieved,Convert(varchar,H.OrderRetrieveDate,105) as OrderRetrieveDate ,H.OrderDate AS [@ODate] ,H.Remark, D.OrderReceiveDetailID,D.ItemCategoryID,D.ItemNameID,D.GoldQualityID,D.OrderRate,D.GoldTK, " & _
                                 " CAST((D.GoldTG) AS DECIMAL(18,3)) As GoldTG,D.WasteTK,CAST((D.WasteTG) AS DECIMAL(18,3)) As WasteTG,D.TotalGemTK,CAST((D.TotalGemTG) AS DECIMAL(18,3)) As TotalGemTG,I.ItemCategory,G.GoldQuality,N.ItemName,D.GoldSmithID,GS.Name as GoldSmith " & _
                                 " From tbl_OrderInvoice H LEFT JOIN tbl_OrderReceiveDetail D on D.OrderInvoiceID=H.OrderInvoiceID LEFT JOIN tbl_GoldSmith GS on D.GoldSmithID=GS.GoldSmithID LEFT Join tbl_Staff S on S.StaffID=H.StaffID LEFT Join tbl_Customer C On C.CustomerID=H.CustomerID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID " & _
                                 " LEFT Join tbl_GoldQuality G on G.GoldQualityID=D.GoldQualityID   " & cristr & " And H.IsDelete=0 Order By [@ODate] DESC"

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

        Public Function GetAllOrderInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IOrderInvoiceDA.GetAllOrderInvoiceVoucherPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select O.OrderInvoiceID,O.ItemName,O.CustomerID,C.CustomerName,C.CustomerAddress,O.Width,O.OrderDate, O.QTY, O.PayGoldQualityID, GQ.GoldQuality As PayGoldQuality,N.Photo,O.DesignCharges,  " & _
                                " CAST(O.PayGoldTK AS INT) AS PayGoldK, CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT) AS PayGoldP," & _
                                " CAST((((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16)-CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS PayGoldY," & _
                                " CAST(O.EstimateGoldTK AS INT) AS EstimateGoldK, CAST((O.EstimateGoldTK-CAST(O.EstimateGoldTK AS INT))*16 AS INT) AS EstimateGoldP, " & _
                                " CAST((((O.EstimateGoldTK-CAST(O.EstimateGoldTK AS INT))*16)-CAST((O.EstimateGoldTK-CAST(O.EstimateGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS EstimateGoldY," & _
                                " CAST(O.WasteGoldTK AS INT) AS WasteK, CAST((O.WasteGoldTK-CAST(O.WasteGoldTK AS INT))*16 AS INT) AS WasteP," & _
                                " CAST((((O.WasteGoldTK-CAST(O.WasteGoldTK AS INT))*16)-CAST((O.WasteGoldTK-CAST(O.WasteGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS WasteY," & _
                                " CAST(O.GemsTK AS INT) AS GemsK, CAST((O.GemsTK-CAST(O.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                " CAST((((O.GemsTK-CAST(O.GemsTK AS INT))*16)-CAST((O.GemsTK-CAST(O.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, " & _
                                " CAST(O.TotalTK AS INT) AS TotalK, CAST((O.TotalTK-CAST(O.TotalTK AS INT))*16 AS INT) AS TotalP," & _
                                " CAST((((O.TotalTK-CAST(O.TotalTK AS INT))*16)-CAST((O.TotalTK-CAST(O.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY, " & _
                                " O.PayGoldTK, CAST((O.PayGoldTG) AS DECIMAL(18,3)) as PayGoldTG, O.EstimateGoldTK, CAST((O.EstimateGoldTG) AS DECIMAL(18,3)) as EstimateGoldTG, O.WasteGoldTK, CAST((O.WasteGoldTG) AS DECIMAL(18,3)) as WasteGoldTG, O.GemsTK, CAST((O.GemsTG) AS DECIMAL(18,3)) as GemsTG, O.TotalTK, (CAST((O.GemsTG) AS DECIMAL(18,3)) + CAST((EstimateGoldTG) AS DECIMAL(18,3))) as TotalTG,OG.QTY AS GemsQTY,OG.YOrCOrG,OG.Amount As GemsAmount, " & _
                                " OG.OrderInvoiceGemsItemID, OG.GemsCategoryID, G.GemsCategory, OG.GemsName, D.ItemCode, O.TotalAmount, O.AddOrSub, O.AdvanceAmount, O.SecondAdvanceAmount, I.ItemCategory,O.GoldPrice,O.GemsPrice,A.GoldQuality,OG.IsShopGems,O.OrderRate, ((O.TotalAmount+O.AddOrSub)-(O.AdvanceAmount+O.SecondAdvanceAmount)) As BalanceAmount, " & _
                                " OG.GemsTK AS ItemGemsTK, OG.GemsTG As ItemGemsTG, " & _
                                " CAST(OG.GemsTK AS INT) AS ItemGemsK, CAST((OG.GemsTK-CAST(OG.GemsTK AS INT))*16 AS INT) AS ItemGemsP," & _
                                " CAST((((OG.GemsTK-CAST(OG.GemsTK AS INT))*16)-CAST((OG.GemsTK-CAST(OG.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS ItemGemsY, O.OrderDate AS [@ODate] " & _
                                " From tbl_OrderInvoice O  Left Join tbl_GoldQuality A on A.GoldQualityID=O.GoldQualityID " & _
                                " Left Join tbl_Customer C on C.CustomerID=O.CustomerID Left Join tbl_OrderReturnDetail D on D.OrderInvoiceID=O.OrderInvoiceID " & _
                                " Left Join tbl_OrderInvoiceGemsItem OG On OG.OrderInvoiceID=O.OrderInvoiceID Left Join tbl_ForSale N on N.ForSaleID=D.ForSaleID " & _
                                " Left Join tbl_ItemCategory I on I.ItemCategoryID=N.ItemCategoryID  Left Join tbl_GemsCategory G on G.GemsCategoryID = OG.GemsCategoryID " & _
                                " LEFT JOIN tbl_GoldQuality GQ ON O.PayGoldQualityID=GQ.GoldQualityID " & _
                                " WHERE O.OrderDate BETWEEN @FromDate AND @ToDate " & criStr & " And O.IsDelete=0 ORDER BY [@ODate] DESC, O.OrderInvoiceID ASC"
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

        Public Function GetAllOrderReturnVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IOrderInvoiceDA.GetAllOrderReturnVoucherPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT G.OrderInvoiceDetailGemsID, G.GemsCategoryID, GC.GemsCategory, G.GemsName, " & _
                                " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY,G.GemsTK, G.GemsTG, G.YOrCOrG, G.GemsTW, G.Qty , G.Type, G.UnitPrice, " & _
                                " G.Amount As GemsAmount, G.GemsRemark,   D.OrderInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, I.ItemName, F.Length, F.GoldQualityID, " & _
                                " GQ.GoldQuality, F.ItemCategoryID, C.ItemCategory, F.Width, F.Length, F.FixPrice, F.IsFixPrice, F.DesignCharges, F.PlatingCharges, F.MountingCharges,  F.WhiteCharges, F.Photo, D.SalesRate, " & _
                                " D.GoldPrice, D.GemsPrice, D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, F.ItemTK, CAST((F.ItemTG) AS DECIMAL(18,3)) as ItemTG, " & _
                                " F.GemsTK As TotalGemsTK, CAST((F.GemsTG) AS DECIMAL(18,3)) AS TotalGemsTG, F.WasteTK, CAST((F.WasteTG) AS DECIMAL(18,3)) as WasteTG, F.GoldTK, CAST((F.GoldTG) AS DECIMAL(18,3)) as GoldTG,F.TotalTK,(CAST((F.GemsTG) AS DECIMAL(18,3)) + CAST((F.GoldTG) AS DECIMAL(18,3)) + CAST((F.WasteTG) AS DECIMAL(18,3))) as TotalTG,  " & _
                                " CAST(F.ItemTK AS INT) AS ItemK," & _
                                " CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS ItemY, " & _
                                " CAST(F.GemsTK AS INT) AS TotalGemsK," & _
                                " CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT) AS TotalGemsP,  CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalGemsY, " & _
                                " CAST(F.WasteTK AS INT) AS WasteK,  CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                " CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS WasteY, " & _
                                " CAST(F.TotalTK AS INT) AS TotalK,  CAST((F.TotalTK-CAST(F.TotalTK AS INT))*16 AS INT) AS TotalP,  " & _
                                " CAST((((F.TotalTK-CAST(F.TotalTK AS INT))*16)-CAST((F.TotalTK-CAST(F.TotalTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY," & _
                                " CAST(F.GoldTK AS INT) AS GoldK,  CAST((F.GoldTK-CAST(F.GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                " CAST((((F.GoldTK-CAST(F.GoldTK AS INT))*16)-CAST((F.GoldTK-CAST(F.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GoldY, " & _
                                " H.OrderInvoiceID, H.OrderDate, H.CustomerID, Cus.CustomerName, Cus.CustomerAddress,H.StaffID, S.Staff, H.RRemark AS Remark, H.RTotalAmount AS TotalAmount,H.RAddOrSub AS AddOrSub, " & _
                                " (H.RTotalAmount+H.RAddOrSub) As NetAmount, H.AdvanceAmount, H.SecondAdvanceAmount, (H.AdvanceAmount+H.SecondAdvanceAmount) As TotalAdvanceAmount, H.FromGoldAmount,  " & _
                               " H.DiscountAmount, H.PaidAmount, (((H.RTotalAmount+H.RAddOrSub)-(H.DiscountAmount+H.AdvanceAmount+H.SecondAdvanceAmount+H.FromGoldAmount))-H.PaidAmount) As BalanceAmount,F.Photo, H.OrderRetrieveDate, H.OrderRetrieveDate AS [@ODate] ,D.ItemTax" & _
                                " FROM tbl_OrderReturnDetail D LEFT JOIN tbl_OrderInvoiceDetailGems G ON G.OrderInvoiceDetailID=D.OrderInvoiceDetailID" & _
                                " LEFT JOIN  tbl_OrderInvoice H  ON H.OrderInvoiceID=D.OrderInvoiceID " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=D.ForSaleID LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID" & _
                                " LEFT JOIN tbl_ItemName I ON I.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality GQ" & _
                                " ON GQ.GoldQualityID=F.GoldQualityID   left join tbl_Customer Cus on H.CustomerID=Cus.CustomerID" & _
                                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID   LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=F.ItemCategoryID" & _
                                " WHERE H.OrderRetrieveDate BETWEEN @FromDate AND @ToDate " & criStr & " ORDER BY [@ODate] DESC, H.OrderInvoiceID ASC "
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

        Public Function GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID(ByVal OrderReturnGemID As String) As DataTable Implements IOrderInvoiceDA.GetOrderInvoiceDetailGemsDataByOrderInvoiceDetailGemsID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select * from tbl_OrderReturnGemsItem "
                strCommandText += " where OrderReturnGemID=@OrderReturnGemID "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnGemID", DbType.String, OrderReturnGemID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderReturnDetailPrint(ByVal OrderReturnHeaderID As String) As System.Data.DataTable Implements IOrderInvoiceDA.GetOrderReturnDetailPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select  D.OrderInvoiceDetailID, D.ForSaleID, D.ItemCode, F.ItemNameID, N.ItemName, F.Length, F.GoldQualityID,  " & _
                                 " GQ.GoldQuality, GQ.IsGramRate, F.ItemCategoryID, I.ItemCategory, F.Width, F.Length, F.FixPrice, F.IsFixPrice, F.DesignCharges, F.PlatingCharges, F.MountingCharges,  F.WhiteCharges, F.Photo, D.SalesRate,  " & _
                                 " D.SalesRate,D.GoldPrice, D.GemsPrice, D.TotalAmount AS ItemTotalAmount, D.AddOrSub AS ItemAddOrSub, (D.TotalAmount+D.AddOrSub) As ItemNetAmount, F.ItemTK, F.ItemTG,  " & _
                                 " F.GemsTK As TotalGemsTK, F.GemsTG AS TotalGemsTG, F.WasteTK, F.WasteTG, (F.ItemTK+F.WasteTK) AS TotalTK, (F.ItemTG+F.WasteTG) AS TotalTG, ((F.ItemTK-F.GemsTK)+F.WasteTK) AS TotalGoldTK, ((F.ItemTG-F.GemsTG)+F.WasteTG) AS TotalGoldTG, (F.ItemTK-F.GemsTK) AS GoldTK, (F.ItemTG-F.GemsTG)  AS GoldTG,   " & _
                                 " CAST(((F.ItemTK-F.GemsTK)+F.WasteTK) AS INT) AS TotalGoldK,CAST((((F.ItemTK-F.GemsTK)+F.WasteTK)-CAST(((F.ItemTK-F.GemsTK)+F.WasteTK) AS INT))*16 AS INT) AS TotalGoldP,  " & _
                                 " CAST((((((F.ItemTK-F.GemsTK)+F.WasteTK)-CAST(((F.ItemTK-F.GemsTK)+F.WasteTK) AS INT))*16)-CAST((((F.ItemTK-F.GemsTK)+F.WasteTK)-CAST(((F.ItemTK-F.GemsTK)+F.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalGoldY,  " & _
                                 " CAST(F.ItemTK AS INT) AS ItemK,CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                 " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY,  " & _
                                 " CAST(F.GemsTK AS INT) AS TotalGemsK,CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT) AS TotalGemsP,   " & _
                                 " CAST((((F.GemsTK-CAST(F.GemsTK AS INT))*16)-CAST((F.GemsTK-CAST(F.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalGemsY, " & _
                                 " CAST(F.WasteTK AS INT) AS WasteK,  CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 " CAST((((F.WasteTK-CAST(F.WasteTK AS INT))*16)-CAST((F.WasteTK-CAST(F.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY,  " & _
                                 " CAST((F.ItemTK+F.WasteTK) AS INT) AS TotalK,  CAST(((F.ItemTK+F.WasteTK)-CAST((F.ItemTK+F.WasteTK) AS INT))*16 AS INT) AS TotalP,   " & _
                                 " CAST(((((F.ItemTK+F.WasteTK)-CAST((F.ItemTK+F.WasteTK) AS INT))*16)-CAST(((F.ItemTK+F.WasteTK)-CAST((F.ItemTK+F.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS TotalY, " & _
                                 " CAST((F.ItemTK-F.GemsTK) AS INT) AS GoldK,  CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST(((((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16)-CAST(((F.ItemTK-F.GemsTK)-CAST((F.ItemTK-F.GemsTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY, " & _
                                 " H.OrderReturnHeaderID,H.ReturnDate,H.OrderInvoiceID,H.AllTotalAmount,H.AllAddOrSub , H.IsAddGold, ((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount) As NetAmount, " & _
                                 " H.FromGoldAmount,H.DiscountAmount,H.BalanceAmount,H.PaidAmount,H.AdvanceAmount,H.StaffID,S.Staff,(F.DesignCharges+F.PlatingCharges+F.MountingCharges+F.WhiteCharges) As TotalCharges, O.PayGoldQualityID,O.OrderDate, " & _
                                 " O.PayGoldTK,O.PayGoldTG, CAST(O.PayGoldTK AS INT) AS PayGoldK,  CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT) AS PayGoldP, " & _
                                 " CAST((((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16)-CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PayGoldY,C.CustomerName,C.CustomerAddress,C.CustomerTel, F.Width, F.Length,F.IsDiamond,D.ItemTaxPer,D.ItemTax,H.AllTaxAmt,F.OriginalCode,0 as PayGoldC,0 as EstimateGoldK,0 as EstimateGoldTK,0 as EstimateGoldP,0 as EstimateGoldY,0 as EstimateGoldC,0 As WasteGoldTK,0 As ReturnWasteK,0 As ReturnWasteP,0 As ReturnWasteY,0 As WasteC " & _
                                 " From tbl_OrderReturnDetail D LEFT JOIN tbl_OrderReturnHeader H on H.OrderReturnHeaderID = D.OrderReturnHeaderID " & _
                                 " LEFT JOIN tbl_OrderInvoice O on O.OrderInvoiceID=H.OrderInvoiceID LEFT JOIN tbl_ForSale F on F.ForSaleID=D.ForSaleID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID  " & _
                                 " LEFT JOIN tbl_GoldQuality GQ on GQ.GoldQualityID=F.GoldQualityID LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID LEFT JOIN tbl_Customer C on C.CustomerID=O.CustomerID " & _
                                 " WHERE H.OrderReturnHeaderID=@OrderReturnHeaderID ORDER BY D.OrderInvoiceDetailID "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, OrderReturnHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertOrderReceiveDetail(DetailObj As OrderReceiveDetailInfo) As Boolean Implements IOrderInvoiceDA.InsertOrderReceiveDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_OrderReceiveDetail ( OrderReceiveDetailID,OrderInvoiceID,Length,Width,ItemCategoryID,GoldSmithID,ItemNameID,GoldQualityID,GoldTK,GoldTG,WasteTK,WasteTG,TotalGemTK,TotalGemTG,OrderRate,GoldPrice,GemPrice,DesignCharges,PlatingFee,WhiteCharges,MountingFee,TotalAmount,AddOrSub,IsBarcode, Design,IsDiamond)"
                strCommandText += " Values (@OrderReceiveDetailID,@OrderInvoiceID,@Length,@Width,@ItemCategoryID,@GoldSmithID,@ItemNameID,@GoldQualityID,@GoldTK,@GoldTG,@WasteTK,@WasteTG,@TotalGemTK,@TotalGemTG,@OrderRate,@GoldPrice,@GemPrice,@DesignCharges,@PlatingFee,@WhiteCharges,@MountingFee,@TotalAmount,@AddOrSub,@IsBarcode, @Design,@IsDiamond)"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, DetailObj.OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, DetailObj.OrderInvoiceID)
                DB.AddInParameter(DBComm, "@Length", DbType.String, DetailObj.Length)
                DB.AddInParameter(DBComm, "@Width", DbType.String, DetailObj.Width)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, DetailObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, DetailObj.GoldSmithID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, DetailObj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, DetailObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, DetailObj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, DetailObj.GoldTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, DetailObj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, DetailObj.WasteTG)
                DB.AddInParameter(DBComm, "@TotalGemTK", DbType.Decimal, DetailObj.TotalGemTK)
                DB.AddInParameter(DBComm, "@TotalGemTG", DbType.Decimal, DetailObj.TotalGemTG)
                DB.AddInParameter(DBComm, "@OrderRate", DbType.Int64, DetailObj.OrderRate)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, DetailObj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemPrice", DbType.Int64, DetailObj.GemPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, DetailObj.DesignCharges)
                DB.AddInParameter(DBComm, "@PlatingFee", DbType.Int64, DetailObj.PlatingFee)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, DetailObj.WhiteCharges)
                DB.AddInParameter(DBComm, "@MountingFee", DbType.Int64, DetailObj.MountingFee)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, DetailObj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, DetailObj.AddOrSub)
                DB.AddInParameter(DBComm, "@IsBarcode", DbType.Boolean, False)
                DB.AddInParameter(DBComm, "@Design", DbType.String, DetailObj.Design)
                DB.AddInParameter(DBComm, "@IsDiamond", DbType.Boolean, DetailObj.IsDiamond)
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

        Public Function UpdateOrderReceiveDetail(DetailObj As OrderReceiveDetailInfo) As Boolean Implements IOrderInvoiceDA.UpdateOrderReceiveDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_OrderReceiveDetail set OrderReceiveDetailID=@OrderReceiveDetailID, OrderInvoiceID=@OrderInvoiceID, Length=@Length, Width=@Width, ItemCategoryID=@ItemCategoryID,GoldSmithID=@GoldSmithID, ItemNameID=@ItemNameID, GoldQualityID=@GoldQualityID, GoldTK=@GoldTK, GoldTG=@GoldTG, WasteTK=@WasteTK, WasteTG=@WasteTG, TotalGemTK=@TotalGemTK,TotalGemTG=@TotalGemTG,OrderRate=@OrderRate,GoldPrice=@GoldPrice, GemPrice=@GemPrice, DesignCharges=@DesignCharges, PlatingFee=@PlatingFee, WhiteCharges=@WhiteCharges, MountingFee=@MountingFee, TotalAmount=@TotalAmount, AddOrSub=@AddOrSub, Design=@Design, IsDiamond= @IsDiamond"
                strCommandText += " where OrderReceiveDetailID= @OrderReceiveDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, DetailObj.OrderReceiveDetailID)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, DetailObj.OrderInvoiceID)
                DB.AddInParameter(DBComm, "@Length", DbType.String, DetailObj.Length)
                DB.AddInParameter(DBComm, "@Width", DbType.String, DetailObj.Width)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, DetailObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldSmithID", DbType.String, DetailObj.GoldSmithID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, DetailObj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, DetailObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@GoldTK", DbType.Decimal, DetailObj.GoldTK)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, DetailObj.GoldTG)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, DetailObj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, DetailObj.WasteTG)
                DB.AddInParameter(DBComm, "@TotalGemTK", DbType.Decimal, DetailObj.TotalGemTK)
                DB.AddInParameter(DBComm, "@TotalGemTG", DbType.Decimal, DetailObj.TotalGemTG)
                DB.AddInParameter(DBComm, "@OrderRate", DbType.Int64, DetailObj.OrderRate)
                DB.AddInParameter(DBComm, "@GoldPrice", DbType.Int64, DetailObj.GoldPrice)
                DB.AddInParameter(DBComm, "@GemPrice", DbType.Int64, DetailObj.GemPrice)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, DetailObj.DesignCharges)
                DB.AddInParameter(DBComm, "@PlatingFee", DbType.Int64, DetailObj.PlatingFee)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, DetailObj.WhiteCharges)
                DB.AddInParameter(DBComm, "@MountingFee", DbType.Int64, DetailObj.MountingFee)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, DetailObj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int64, DetailObj.AddOrSub)
                DB.AddInParameter(DBComm, "@IsBarcodeNo", DbType.String, DetailObj.IsBarcodeNo)
                DB.AddInParameter(DBComm, "@Design", DbType.String, DetailObj.Design)
                DB.AddInParameter(DBComm, "@IsDiamond", DbType.Boolean, DetailObj.IsDiamond)
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

        Public Function DeleteOrderReceiveDetail(OrderReceiveDetailID As String) As Boolean Implements IOrderInvoiceDA.DeleteOrderReceiveDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_OrderReceiveDetail WHERE  OrderReceiveDetailID= @OrderReceiveDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, OrderReceiveDetailID)
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

        Public Function GetOrderInvoiceHeaderID(OrderInvoiceID As String) As OrderInvoiceInfo Implements IOrderInvoiceDA.GetOrderInvoiceHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objOrderInvoice As New OrderInvoiceInfo
            Try
                strCommandText = "Select * From tbl_OrderInvoice WHERE OrderInvoiceID=@OrderInvoiceID and IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objOrderInvoice
                        .OrderInvoiceID = drResult("OrderInvoiceID")
                        .OrderDate = drResult("OrderDate")
                        .DueDate = drResult("DueDate")
                        .CustomerID = drResult("CustomerID")
                        .StaffID = drResult("StaffID")
                        .PayGoldQualityID = drResult("PayGoldQualityID")
                        .PayGoldTK = drResult("PayGoldTK")
                        .PayGoldTG = drResult("PayGoldTG")
                        .Remark = drResult("Remark")
                        .AllTotalAmount = drResult("AllTotalAmount")
                        .AllAddOrSub = drResult("AllAddOrSub")
                        .AdvanceAmount = drResult("AdvanceAmount")
                        .SecondAdvanceDate = drResult("SecondAdvanceDate")
                        .SecondAdvanceAmount = drResult("SecondAdvanceAmount")
                        .IsCancel = drResult("IsCancel")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objOrderInvoice
        End Function

        Public Function GetOrderReceiveDetail(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceDA.GetOrderReceiveDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Try
                strCommandText = "Select R.OrderReceiveDetailID,R.OrderInvoiceID,R.ItemCategoryID,C.ItemCategory,N.ItemName,R.ItemNameID,R.GoldQualityID,G.GoldQuality,R.OrderRate, " & _
                                " R.Length, R.Width, R.GoldTK, R.GoldTG, R.WasteTK ,WasteTG, R.TotalGemTK, R.TotalGemTG," & _
                                " R.GoldPrice,R.GemPrice,R.DesignCharges,R.PlatingFee,R.WhiteCharges,R.MountingFee,R.TotalAmount,R.AddOrSub,(R.TotalAmount+R.AddOrSub) As NetAmount, Design, R.IsDiamond ,R.GoldSmithID,GS.Name As GoldSmith" & _
                                " From tbl_OrderReceiveDetail R " & _
                                " LEFT JOIN tbl_GoldSmith GS on R.GoldSmithID=GS.GoldSmithID " & _
                                " LEFT JOIN tbl_ItemCategory C on C.ItemCategoryID=R.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=R.ItemNameID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=R.GoldQualityID " & _
                                " Where OrderInvoiceID ='" & OrderInvoiceID & "' Order By OrderReceiveDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderInvoiceGemsItemHeaderID(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceDA.GetOrderInvoiceGemsItemHeaderID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select OrderInvoiceGemsItemID, G.OrderReceiveDetailID, GemsCategoryID, GemsName, GemsTK, GemsTG, YOrCOrG, GemsTW,Qty, Type, UnitPrice, Amount, IsCustomerGem, "
                strCommandText += " CAST(GemsTK AS INT) AS GemsK, "
                strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY "
                strCommandText += " FROM tbl_OrderInvoiceGemsItem G LEFT JOIN tbl_OrderReceiveDetail D ON D.OrderReceiveDetailID=G.OrderReceiveDetailID "
                strCommandText += " Where D.OrderInvoiceID ='" & OrderInvoiceID & "' "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderReceiveDetailGemByID(OrderReceiveDetailID As String) As DataTable Implements IOrderInvoiceDA.GetOrderReceiveDetailGemByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select OrderInvoiceGemsItemID,OrderReceiveDetailID,GemsCategoryID, GemsName, GemsTK, GemsTG, YOrCOrG, GemsTW,Qty, Type, UnitPrice, Amount,"
                strCommandText += " CAST(GemsTK AS INT) AS GemsK,CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP, "
                strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, IsCustomerGem"
                strCommandText += " from tbl_OrderInvoiceGemsItem Where OrderReceiveDetailID ='" & OrderReceiveDetailID & "' Order By OrderReceiveDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function


        Public Function GetReceiveDataByOrderInvocieID(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceDA.GetReceiveDataByOrderInvocieID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select * From tbl_OrderReceiveDetail " & _
                                 "Where OrderInvoiceID=@OrderInvoiceID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetOrderReceivePrint(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceDA.GetOrderReceivePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select O.OrderInvoiceID,O.CustomerID,O.StaffID,O.OrderDate,O.DueDate,O.PayGoldTK,O.PayGoldTG,O.AllTotalAmount,O.AllAddOrSub,O.AdvanceAmount,O.SecondAdvanceAmount,O.SecondAdvanceDate,O.PayGoldQualityID,PQ.GoldQuality As PayGoldQuality,G.Type as FixType,G.UnitPrice,G.Amount As GemsAmount," & _
                                 " (R.GoldTK+R.TotalGemTK) As TotalTK, (R.GoldTG+R.TotalGemTG) As TotalTG," & _
                                 " CAST( (R.GoldTK+R.TotalGemTK) AS INT) AS TotalK, CAST(((R.GoldTK+R.TotalGemTK)-CAST((R.GoldTK+R.TotalGemTK) AS INT))*16 AS INT) AS TotalP, " & _
                                 " CAST(((((R.GoldTK+R.TotalGemTK)-CAST((R.GoldTK+R.TotalGemTK) AS INT))*16)-CAST(((R.GoldTK+R.TotalGemTK)-CAST((R.GoldTK+R.TotalGemTK) AS INT))*16 AS INT)) *'" & Global_PToY & "' AS DECIMAL(18,2)) AS TotalY," & _
                                 " CAST(O.PayGoldTK AS INT) AS PayGoldK, CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT) AS PayGoldP, " & _
                                 " CAST((((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16)-CAST((O.PayGoldTK-CAST(O.PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PayGoldY," & _
                                 " R.OrderReceiveDetailID,R.ItemNameID,R.ItemCategoryID,R.GoldQualityID,Q.GoldQuality,Q.IsGramRate,R.OrderRate,R.Length,R.Width,R.GoldTK,R.GoldTG," & _
                                 " CAST(GoldTK AS INT) AS GoldK,CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY ," & _
                                 " CAST(WasteTK AS INT) AS WasteK,CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                 " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " CAST(TotalGemTK AS INT) AS GemsK,CAST((TotalGemTK-CAST(TotalGemTK AS INT))*16 AS INT) AS GemsP," & _
                                 " CAST((((TotalGemTK-CAST(TotalGemTK AS INT))*16)-CAST((TotalGemTK-CAST(TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY," & _
                                 " R.WasteTK,R.WasteTG,R.TotalGemTK,R.TotalGemTG,C.CustomerCode,C.CustomerName,C.CustomerAddress,C.CustomerTel,S.Staff,I.ItemCategory,N.ItemName,R.GoldPrice,R.GemPrice,R.DesignCharges,R.PlatingFee,R.WhiteCharges,R.MountingFee,(R.DesignCharges+R.PlatingFee+R.WhiteCharges+R.MountingFee) As OtherCharges,R.TotalAmount, R.AddOrSub,R.IsDiamond, " & _
                                 " G.OrderInvoiceGemsItemID,G.GemsCategoryID,GC.GemsCategory,G.GemsName,G.GemsTK,CAST(G.GemsTK AS INT) AS GemK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemP, CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemY,G.GemsTG, G.YOrCOrG, G.GemsTW, G.Qty, G.UnitPrice, G.Type, G.Amount, G.IsCustomerGem ,O.Remark,R.GoldSmithID,GS.Name as GoldSmith,R.Design " & _
                                 " From tbl_OrderInvoice O LEFT JOIN tbl_OrderReceiveDetail R ON R.OrderInvoiceID=O.OrderInvoiceID " & _
                                 " LEFT JOIN tbl_OrderInvoiceGemsItem G ON G.OrderReceiveDetailID=R.OrderReceiveDetailID LEFT JOIN tbl_Staff S ON S.StaffID=O.StaffID  LEFT JOIN tbl_Customer C ON C.CustomerID=O.CustomerID " & _
                                 " LEFT JOIN tbl_GoldSmith GS ON R.GoldSmithID=GS.GoldSmithID LEFT JOIN tbl_GoldQuality PQ ON PQ.GoldQualityID=O.PayGoldQualityID LEFT JOIN tbl_GoldQuality Q ON Q.GoldQualityID=R.GoldQualityID LEFT JOIN tbl_ItemCategory I ON I.ItemCategoryID=R.ItemCategoryID LEFT JOIN tbl_ItemName N ON N.ItemNameID=R.ItemNameID " & _
                                 " LEFT JOIN tbl_GemsCategory GC ON GC.GemsCategoryID=G.GemsCategoryID WHERE O.OrderInvoiceID=@OrderInvoiceID And O.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetAllOrderReceive() As DataTable Implements IOrderInvoiceDA.GetAllOrderReceive
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select R.IsDiamond as [$IsDiamond],R.OrderInvoiceID As VoucherNo, convert(varchar(10),O.OrderDate,105) as OrderDate, A.CustomerName As [Customer_], R.OrderReceiveDetailID As [@OrderReceiveDetailID], C.ItemCategory As [ItemCategory_],  " & _
                                 " N.ItemName AS [ItemName_], G.GoldQuality AS [GoldQuality_], R.OrderRate, R.Length As [Length_], R.Width AS [Width_], S.Staff AS [Staff_], O.OrderDate AS [@ODate], R.ItemCategoryID AS [@ItemCategoryID], R.ItemNameID AS [@ItemNameID],R.GoldQualityID AS [@GoldQualityID],R.PlatingFee  As [@PlatingFee],R.WhiteCharges As [@WhiteCharges],R.DesignCharges As [@DesignCharges],R.MountingFee As [@MountingFee], " & _
                                 " R.GoldTK AS [@GoldTK], R.GoldTG AS [@GoldTG], R.WasteTK AS [@WasteTK], R.WasteTG  AS [@WasteTG], R.TotalGemTK AS [@TotalGemTK], R.TotalGemTG AS [@TotalGemTG], (R.GoldTK+R.TotalGemTK) AS [@ItemTK],  (R.GoldTG+R.TotalGemTG) AS [@ItemTG], (R.GoldTK+R.TotalGemTK+R.WasteTK) AS [@TotalTK],  (R.GoldTG+R.TotalGemTG+R.WasteTG) AS [@TotalTG]," & _
                                 " CAST(R.GoldTG AS DECIMAL(18,3)) AS GoldTG, CAST(R.GoldTK AS INT) AS GoldK,CAST((R.GoldTK-CAST(R.GoldTK AS INT))*16 AS INT) AS GoldP, " & _
                                 " CAST((((R.GoldTK-CAST(R.GoldTK AS INT))*16)-CAST((R.GoldTK-CAST(R.GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS GoldY ," & _
                                 " CAST(R.WasteTG AS DECIMAL(18,3)) AS WasteTG, CAST(R.WasteTK AS INT) AS WasteK,CAST((R.WasteTK-CAST(R.WasteTK AS INT))*16 AS INT) AS WasteP, " & _
                                 " CAST((((R.WasteTK-CAST(R.WasteTK AS INT))*16)-CAST((R.WasteTK-CAST(R.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " R.GoldPrice, R.GemPrice, O.CustomerID AS [@CustomerID], O.StaffID as [@StaffID],R.GoldSmithID,GS.Name as GoldSmith From tbl_OrderReceiveDetail R  " & _
                                 " LEFT JOIN tbl_OrderInvoice O ON O.OrderINvoiceID=R.OrderInvoiceID LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=R.ItemCategoryID LEFT JOIN tbl_GoldSmith GS ON R.GoldSmithID=GS.GoldSmithID " & _
                                 " LEFT JOIN tbl_ItemName N ON N.ItemNameID=R.ItemNameID LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=R.GoldQualityID  " & _
                                 " LEFT JOIN tbl_Customer A ON A.CustomerID=O.CustomerID LEFT JOIN tbl_Staff S on S.StaffID=O.StaffID " & _
                                 " WHERE O.IsCancel=0 And R.IsBarcode=0 And O.IsDelete=0 Order By [@ODate] DESC"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderGemsByReceive(OrderReceiveDetailID As String) As DataTable Implements IOrderInvoiceDA.GetOrderGemsByReceive
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.OrderInvoiceGemsItemID, R.OrderReceiveDetailID, '' AS ForSaleGemsItemID, '' AS ForSaleID, G.GemsCategoryID, G.GemsName, C.GemsCategory, G.GemsTK, G.GemsTG, G.YOrCorG AS YOrCOrG, G.GemsTW,G.Qty, " & _
                                 " G.UnitPrice,G.Type,G.Amount,CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY, '' As GemsRemark,R.GoldSmithID,GS.Name as GoldSmith,0 As SaleByDefinePrice  " & _
                                 " From tbl_OrderInvoiceGemsItem G LEFT JOIN tbl_OrderReceiveDetail R on R.OrderReceiveDetailID=G.OrderReceiveDetailID " & _
                                 " LEFT JOIN tbl_GoldSmith GS on R.GoldSmithID=GS.GoldSmithID LEFT JOIN tbl_GemsCategory C on C.GemsCategoryID=G.GemsCategoryID Where R.OrderReceiveDetailID=@OrderReceiveDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, OrderReceiveDetailID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderReceiveDetailID(OrderReceiveDetailID As String) As OrderReceiveDetailInfo Implements IOrderInvoiceDA.GetOrderReceiveDetailID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objOrderInvoice As New OrderReceiveDetailInfo
            Try
                strCommandText = "Select *,CAST(GoldTK AS INT) AS GoldK, " & _
                                 " CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT) AS GoldP," & _
                                 " CAST((((GoldTK-CAST(GoldTK AS INT))*16)-CAST((GoldTK-CAST(GoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GoldY," & _
                                 " CAST(WasteTK AS INT) AS WasteK, " & _
                                 " CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT) AS WasteP," & _
                                 " CAST((((WasteTK-CAST(WasteTK AS INT))*16)-CAST((WasteTK-CAST(WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS WasteY," & _
                                 " CAST(TotalGemTK AS INT) AS GemK, " & _
                                 " CAST((TotalGemTK-CAST(TotalGemTK AS INT))*16 AS INT) AS GemP," & _
                                 " CAST((((TotalGemTK-CAST(TotalGemTK AS INT))*16)-CAST((TotalGemTK-CAST(TotalGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT) AS GemY" & _
                                 " From tbl_OrderReceiveDetail where OrderReceiveDetailID=@OrderReceiveDetailID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, OrderReceiveDetailID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objOrderInvoice
                        .OrderReceiveDetailID = drResult("OrderReceiveDetailID")
                        .OrderInvoiceID = drResult("OrderInvoiceID")
                        .OrderRate = drResult("OrderRate")
                        .GoldQualityID = drResult("GoldQualityID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .GoldSmithID = drResult("GoldSmithID")
                        .ItemNameID = drResult("ItemNameID")
                        .Length = drResult("Length")
                        .Width = drResult("Width")
                        .GoldTK = drResult("GoldTK")
                        .GoldTG = drResult("GoldTG")
                        .GoldK = drResult("GoldK")
                        .GoldP = drResult("GoldP")
                        .GoldY = drResult("GoldY")
                        .WasteTG = drResult("WasteTG")
                        .WasteTK = drResult("WasteTK")
                        .WasteK = drResult("WasteK")
                        .WasteP = drResult("WasteP")
                        .WasteY = drResult("WasteY")
                        .TotalGemTG = drResult("TotalGemTG")
                        .TotalGemTK = drResult("TotalGemTK")
                        .TotalGemK = drResult("GemK")
                        .TotalGemP = drResult("GemP")
                        .TotalGemY = drResult("GemY")
                        .GoldPrice = drResult("GoldPrice")
                        .GemPrice = drResult("GemPrice")
                        .DesignCharges = drResult("DesignCharges")
                        .MountingFee = drResult("MountingFee")
                        .WhiteCharges = drResult("WhiteCharges")
                        .PlatingFee = drResult("PlatingFee")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objOrderInvoice
        End Function

        Public Function GetAllOrderReceiveHeader() As DataTable Implements IOrderInvoiceDA.GetAllOrderReceiveHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " SELECT H.OrderInvoiceID AS VoucherNo, CONVERT(VARCHAR(10),H.OrderDate,105) as OrderDate, CONVERT(VARCHAR(10),H.DueDate,105) as DueDate, S.Staff AS [Staff_], C.CustomerName AS [CustomerName_], G.GoldQuality As [PayGoldQuality_]," & _
                                " CAST(H.PayGoldTK AS INT) AS PayGoldK, " & _
                                " CAST((H.PayGoldTK-CAST(H.PayGoldTK AS INT))*16 AS INT) AS PayGoldP, " & _
                                " CAST((((H.PayGoldTK-CAST(H.PayGoldTK AS INT))*16)-CAST((H.PayGoldTK-CAST(H.PayGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS PayGoldY, " & _
                                " (H.AllTotalAmount+H.AllAddOrSub) AS NetAmount, H.AdvanceAmount, H.SecondAdvanceAmount, H.Remark As [Remark_], H.OrderDate AS [@ODate] " & _
                                " From tbl_OrderInvoice H LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                                " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=H.PayGoldQualityID " & _
                                " WHERE H.IsCancel=0 AND H.IsRetrieved=0 And H.IsDelete=0 Order By [@ODate] DESC, H.OrderInvoiceID DESC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderInvoiceInfoByDetailID(OrderReceiveDetailID As String) As OrderInvoiceInfo Implements IOrderInvoiceDA.GetOrderInvoiceInfoByDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objOrderInvoice As New OrderInvoiceInfo
            Try
                strCommandText = "Select H.*, C.CustomerName From tbl_OrderInvoice H INNER JOIN tbl_OrderReceiveDetail D ON D.OrderInvoiceID=H.OrderInvoiceID INNER JOIN tbl_Customer C ON C.CustomerID=H.CustomerID WHERE D.OrderReceiveDetailID=@OrderReceiveDetailID And H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReceiveDetailID", DbType.String, OrderReceiveDetailID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objOrderInvoice
                        .OrderInvoiceID = drResult("OrderInvoiceID")
                        .OrderDate = drResult("OrderDate")
                        .DueDate = drResult("OrderDate")
                        .CustomerID = drResult("CustomerID")
                        .StaffID = drResult("StaffID")
                        .PayGoldQualityID = drResult("PayGoldQualityID")
                        .PayGoldTK = drResult("PayGoldTK")
                        .PayGoldTG = drResult("PayGoldTG")
                        .Remark = drResult("Remark")
                        .AllTotalAmount = drResult("AllTotalAmount")
                        .AllAddOrSub = drResult("AllAddOrSub")
                        .AdvanceAmount = drResult("AdvanceAmount")
                        .SecondAdvanceDate = drResult("SecondAdvanceDate")
                        .SecondAdvanceAmount = drResult("SecondAdvanceAmount")
                        .IsCancel = drResult("IsCancel")
                        .CustomerName = drResult("CustomerName")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objOrderInvoice
        End Function
        Public Function InsertOrderInvoiceReturn(ByVal obj As CommonInfo.OrderReturnHeader) As Boolean Implements IOrderInvoiceDA.InsertOrderInvoiceReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim strgetID As String
            Dim id As Integer = 0
            Try
                strCommandText = "Insert into tbl_OrderReturnHeader (OrderReturnHeaderID,ReturnDate, OrderInvoiceID, AllTotalAmount, AllAddOrSub, FromGoldAmount, StaffID, IsAddGold, Remark, DiscountAmount, BalanceAmount, PaidAmount, AdvanceAmount, LocationID, LastModifiedLoginUserName, LastModifiedDate,AllTaxAmt,IsDelete,IsSync,AddGoldTaxPer,AddGoldTax)"
                strCommandText += " Values (@OrderReturnHeaderID,@ReturnDate, @OrderInvoiceID, @AllTotalAmount, @AllAddOrSub, @FromGoldAmount, @StaffID, @IsAddGold, @Remark, @DiscountAmount, @BalanceAmount, @PaidAmount,  @AdvanceAmount, @LocationID, @LastModifiedLoginUserName, @LastModifiedDate,@AllTaxAmt,0,0,@AddGoldTaxPer,@AddGoldTax)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, obj.OrderReturnHeaderID)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.Date, obj.ReturnDate)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, obj.OrderInvoiceID)
                DB.AddInParameter(DBComm, "@AllTotalAmount", DbType.Int64, obj.AllTotalAmount)
                DB.AddInParameter(DBComm, "@AllAddOrSub", DbType.Int64, obj.AllAddOrSub)
                DB.AddInParameter(DBComm, "@FromGoldAmount", DbType.Int64, obj.FromGoldAmount)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@IsAddGold", DbType.Boolean, obj.IsAddGold)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@BalanceAmount", DbType.Int64, obj.BalanceAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int64, obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@AllTaxAmt", DbType.Int64, obj.AllTaxAmt)
                DB.AddInParameter(DBComm, "@AddGoldTaxPer", DbType.Decimal, obj.AddGoldTaxPer)
                DB.AddInParameter(DBComm, "@AddGoldTax", DbType.Int64, obj.AddGoldTax)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    'strgetID = "Select Max(OrderReturnHeaderID) from tbl_OrderReturnHeader"
                    'DBComm = DB.GetSqlStringCommand(strgetID)
                    'id = CInt(DB.ExecuteScalar(DBComm))
                    'Return id
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return 0
            End Try
        End Function
        Public Function UpdateOrderReturnHeader(obj As OrderReturnHeader) As Boolean Implements IOrderInvoiceDA.UpdateOrderReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_OrderReturnHeader set  OrderInvoiceID=@OrderInvoiceID, ReturnDate=@ReturnDate, AllTotalAmount=@AllTotalAmount, AllAddOrSub=@AllAddOrSub, FromGoldAmount=@FromGoldAmount, StaffID=@StaffID, IsAddGold=@IsAddGold, Remark=@Remark, DiscountAmount= @DiscountAmount , BalanceAmount= @BalanceAmount, PaidAmount=@PaidAmount, AdvanceAmount=@AdvanceAmount, LocationID=@LocationID, LastModifiedLoginUserName=@LastModifiedLoginUserName, LastModifiedDate=@LastModifiedDate,AllTaxAmt=@AllTaxAmt,AddGoldTaxPer=@AddGoldTaxPer,AddGoldTax=@AddGoldTax,IsSync=0"
                strCommandText += " where OrderReturnHeaderID= @OrderReturnHeaderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, obj.OrderReturnHeaderID)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.DateTime, obj.ReturnDate)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, obj.OrderInvoiceID)
                DB.AddInParameter(DBComm, "@AllTotalAmount", DbType.Int64, obj.AllTotalAmount)
                DB.AddInParameter(DBComm, "@AllAddOrSub", DbType.Int64, obj.AllAddOrSub)
                DB.AddInParameter(DBComm, "@FromGoldAmount", DbType.Int64, obj.FromGoldAmount)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@IsAddGold", DbType.Boolean, obj.IsAddGold)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@DiscountAmount", DbType.Int64, obj.DiscountAmount)
                DB.AddInParameter(DBComm, "@BalanceAmount", DbType.Int64, obj.BalanceAmount)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int64, obj.PaidAmount)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int64, obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.Date, Now.Date)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@AllTaxAmt", DbType.Int64, obj.AllTaxAmt)
                DB.AddInParameter(DBComm, "@AddGoldTaxPer", DbType.Decimal, obj.AddGoldTaxPer)
                DB.AddInParameter(DBComm, "@AddGoldTax", DbType.Int64, obj.AddGoldTax)
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

        Public Function GetBalanceAmountByOrderInvoiceID(OrderInvoiceID As String, Optional OrderReturnHeaderID As String = "") As DataTable Implements IOrderInvoiceDA.GetBalanceAmountByOrderInvoiceID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim cristr As String = ""

            Try
                If OrderReturnHeaderID = "" Then
                    strCommandText = "Select OrderReturnHeaderID, BalanceAmount, IsAddGold, FromGoldAmount From tbl_OrderReturnHeader WHERE OrderInvoiceID=@OrderInvoiceID And IsDelete=0 Order By ReturnDate DESC, OrderReturnHeaderID DESC "
                Else
                    strCommandText = "Select OrderReturnHeaderID, BalanceAmount, IsAddGold, FromGoldAmount From tbl_OrderReturnHeader WHERE OrderInvoiceID=@OrderInvoiceID AND OrderReturnHeaderID<>@OrderReturnHeaderID And IsDelete=0 Order By ReturnDate DESC, OrderReturnHeaderID DESC "
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, OrderReturnHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderInvoiceReturnHeader(ByVal OrderReturnHeaderID As String) As CommonInfo.OrderReturnHeader Implements IOrderInvoiceDA.GetOrderInvoiceReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objOrderReturn As New OrderReturnHeader
            Try
                strCommandText = " SELECT * FROM tbl_OrderReturnHeader  WHERE OrderReturnHeaderID= @OrderReturnHeaderID And IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, OrderReturnHeaderID)
                drResult = DB.ExecuteReader(DBComm)

                If drResult.Read() Then
                    With objOrderReturn
                        .OrderReturnHeaderID = drResult.Item("OrderReturnHeaderID")
                        .ReturnDate = drResult.Item("ReturnDate")
                        .OrderInvoiceID = drResult.Item("OrderInvoiceID")
                        .AllTotalAmount = drResult.Item("AllTotalAmount")
                        .AllAddOrSub = drResult.Item("AllAddOrSub")
                        .FromGoldAmount = drResult.Item("FromGoldAmount")
                        .StaffID = drResult.Item("StaffID")
                        .IsAddGold = drResult.Item("IsAddGold")
                        .Remark = drResult.Item("Remark")
                        .DiscountAmount = drResult.Item("DiscountAmount")
                        .BalanceAmount = drResult.Item("BalanceAmount")
                        .PaidAmount = drResult.Item("PaidAmount")
                        .AdvanceAmount = drResult.Item("AdvanceAmount")
                        .AllTaxAmt = drResult.Item("AllTaxAmt")
                        .AddGoldTaxPer = drResult.Item("AddGoldTaxPer")
                        .AddGoldTax = drResult.Item("AddGoldTax")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objOrderReturn
        End Function

        Public Function GetOrderReturnDetailByOrderInvoiceID(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceDA.GetOrderReturnDetailByOrderInvoiceID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Try
                strCommandText = "Select R.OrderInvoiceDetailID,R.OrderReturnHeaderID,R.ForSaleID, R.ItemCode, R.SalesRate, R.GoldPrice, R.GemsPrice, R.TotalAmount, R.AddOrSub,  " & _
                                " F.ItemCategoryID,C.ItemCategory,N.ItemName,F.ItemNameID,F.GoldQualityID,G.GoldQuality," & _
                                " CAST(F.ItemTK AS INT) AS ItemK," & _
                                " CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                                " CAST((((F.ItemTK-CAST(F.ItemTK AS INT))*16)-CAST((F.ItemTK-CAST(F.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS ItemY, " & _
                                " F.GoldTK, F.GoldTG, F.GemsTK, F.GemsTG, F.WasteTK, F.WasteTG, F.ItemTK, F.ItemTG, F.TotalTK, F.TotalTG,  F.IsFixPrice, F.FixPrice," & _
                                " F.Length,F.Width, " & _
                                " F.DesignCharges, F.PlatingCharges, F.WhiteCharges, F.MountingCharges,(R.TotalAmount+R.AddOrSub) As NetAmount, R.IsReturn ,R.ItemTaxPer,R.ItemTax,H.AllTaxAmt" & _
                                " From tbl_OrderReturnDetail R LEFT JOIN tbl_OrderReturnHeader H ON H.OrderReturnHeaderID=R.OrderReturnHeaderID" & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=R.ForSaleID " & _
                                " LEFT JOIN tbl_ItemCategory C on C.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID " & _
                                " Where H.IsDelete=0 AND H.OrderInvoiceID ='" & OrderInvoiceID & "' Order By R.OrderInvoiceDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function UpdateOrderReceiveByIsReturn(ByVal Obj As CommonInfo.OrderInvoiceInfo) As Boolean Implements IOrderInvoiceDA.UpdateOrderReceiveByIsReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                strCommandText = "UPDATE tbl_OrderInvoice SET IsRetrieved=@IsRetrieved, OrderRetrieveDate=(CASE @IsRetrieved WHEN 0 THEN NULL ELSE @OrderRetrieveDate END) " & _
              " WHERE OrderInvoiceID=@OrderInvoiceID  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, Obj.OrderInvoiceID)
                DB.AddInParameter(DBComm, "@IsRetrieved", DbType.Boolean, Obj.IsRetrieved)
                DB.AddInParameter(DBComm, "@OrderRetrieveDate", DbType.Date, IIf(Obj.OrderRetrieveDate <> Nothing, Obj.OrderRetrieveDate, DBNull.Value))
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

        Public Function GetAllOrderReturnHeader() As System.Data.DataTable Implements IOrderInvoiceDA.GetAllOrderReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.OrderReturnHeaderID AS [@OrderReturnHeaderID], H.OrderInvoiceID AS VoucherNo, CONVERT(VARCHAR(10),H.ReturnDate,105) as ReturnDate, H.IsAddGold AS [$IsAddGold], C.CustomerName AS [Customer_], C.CustomerAddress As [Address_], S.Staff AS [Staff_],((H.AllTotalAmount+H.AllAddOrSub)-H.DiscountAmount) AS NetAmount, H.AllTotalAmount, H.AllAddOrSub,H.DiscountAmount,H.FromGoldAmount, H.PaidAmount, H.AdvanceAmount, " & _
                                 "  H.BalanceAmount, H.Remark As [Remark_], H.ReturnDate AS [@RDate] ,H.AllTaxAmt,H.AddGoldTaxPer,H.AddGoldTax" & _
                                 " From tbl_OrderReturnHeader H LEFT JOIN tbl_OrderInvoice OH on OH.OrderInvoiceID=H.OrderInvoiceID LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID " & _
                                 " LEFT JOIN tbl_Customer C on C.CustomerID=OH.CustomerID Where H.IsDelete=0 ORDER BY [@RDate] DESC, H.OrderReturnHeaderID DESC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetOrderReturnDetailByHeaderID(OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceDA.GetOrderReturnDetailByHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Try
                strCommandText = "Select R.OrderInvoiceDetailID,R.OrderReturnHeaderID,R.ForSaleID, R.ItemCode, R.SalesRate, R.GoldPrice, R.GemsPrice, R.TotalAmount, R.AddOrSub, (R.TotalAmount+R.AddOrSub) As NetAmount, " & _
                                " F.ItemCategoryID,C.ItemCategory,N.ItemName,F.ItemNameID,F.GoldQualityID,G.GoldQuality,F.IsOriginalFixedPrice, F.OriginalFixedPrice, F.IsOriginalPriceGram, F.OriginalPriceGram, F.OriginalPriceTK, " & _
                                " F.OriginalGemsPrice, F.OriginalOtherPrice, F.PurchaseWasteTK, F.PurchaseWasteTG,R.ItemTaxPer,R.ItemTax " & _
                                " From tbl_OrderReturnDetail R " & _
                                " LEFT JOIN tbl_ForSale F ON F.ForSaleID=R.ForSaleID " & _
                                " LEFT JOIN tbl_ItemCategory C on C.ItemCategoryID=F.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=F.ItemNameID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=F.GoldQualityID " & _
                                " Where R.OrderReturnHeaderID ='" & OrderReturnHeaderID & "' Order By R.OrderInvoiceDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderReturnGemDataByHeaderID(ByVal OrderReturnHeaderID As String) As System.Data.DataTable Implements IOrderInvoiceDA.GetOrderReturnGemDataByHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.OrderReturnGemID ,G.OrderInvoiceDetailID, G.GemsCategoryID, C.GemsCategory, " & _
                                 " G.GemsName,G.GemsTK, G.GemsTG, YOrCOrG, GemsTW, G.Qty, G.SaleType, G.UnitPrice, G.Amount,G.GemsRemark,  " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY ,G.GemTax,G.GemTaxPer " & _
                                 " From tbl_OrderReturnGemsItem G LEFT JOIN tbl_OrderReturnDetail D On G.OrderInvoiceDetailID=D.OrderInvoiceDetailID " & _
                                 " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=G.GemsCategoryID " & _
                                 " Where D.OrderReturnHeaderID = '" & OrderReturnHeaderID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetOrderReturneGemsItemByDetailID(ByVal OrderInvoiceDetailID As String) As System.Data.DataTable Implements IOrderInvoiceDA.GetOrderReturneGemsItemByDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.OrderReturnGemID ,G.OrderInvoiceDetailID, G.GemsCategoryID, C.GemsCategory, " & _
                                 " G.GemsName,G.GemsTK, G.GemsTG, YOrCOrG, GemsTW, G.Qty, G.SaleType, G.UnitPrice, G.Amount,G.GemsRemark,  " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY ,G.GemTax " & _
                                 " From tbl_OrderReturnGemsItem G LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=G.GemsCategoryID " & _
                                 " Where G.OrderInvoiceDetailID = '" & OrderInvoiceDetailID & "'"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateOrderReturnDetailByIsReturn(Obj As OrderInvoiceDetailInfo) As Boolean Implements IOrderInvoiceDA.UpdateOrderReturnDetailByIsReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_OrderReturnDetail set IsReturn=@IsReturn "
                strCommandText += " where OrderInvoiceDetailID=@OrderInvoiceDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceDetailID", DbType.String, Obj.OrderInvoiceDetailID)
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

        Public Function GetOrderForItemName(OrderInvoiceID As String) As DataTable Implements IOrderInvoiceDA.GetOrderForItemName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.* From tbl_OrderInvoiceGemsItem G LEFT JOIN tbl_OrderReceiveDetail  D on D.OrderReceiveDetailID=G.OrderreceiveDetailID " & _
                                 " LEFT JOIN tbl_OrderInvoice  H on H.OrderInvoiceID=D.OrderInvoiceID LEFT JOIN tbl_OrderReturnHeader RH on RH.OrderInvoiceID=H.OrderInvoiceID Where H.OrderInvoiceID=@OrderInvoiceID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderInvoiceID", DbType.String, OrderInvoiceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetOrderReturnGem(OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceDA.GetOrderReturnGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.* From tbl_OrderReturnGemsItem G LEFT JOIN tbl_OrderReturnDetail D on D.OrderInvoiceDetailID=G.OrderInvoiceDetailID " & _
                                " LEFT JOIN tbl_OrderReturnHeader H on H.OrderReturnHeaderID=D.OrderReturnHeaderID Where H.OrderReturnHeaderID=@OrderReturnHeaderID And H.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, OrderReturnHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetOrderTaxVoucher(ByVal OrderReturnHeaderID As String) As DataTable Implements IOrderInvoiceDA.GetOrderTaxVoucher
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select ItemTaxPer, sum(ItemTax) AS ItemTaxAmount from (select D.ItemTaxPer, D.ItemTax  FROM tbl_OrderReturnDetail D " & _
                                 " INNER JOIN tbl_OrderReturnHeader H on H.OrderReturnHeaderID=D.OrderReturnHeaderID Where H.OrderReturnHeaderID = @OrderReturnHeaderID " & _
                                 " union all " & _
                                 "select G.GemTaxPer as ItemTaxPer, G.GemTax as ItemTax FROM tbl_OrderReturnGemsItem G " & _
                                 "INNER JOIN tbl_OrderReturnDetail D ON G.OrderInvoiceDetailID=D.OrderInvoiceDetailID " & _
                                 "INNER JOIN tbl_OrderReturnHeader H on H.OrderReturnHeaderID=D.OrderReturnHeaderID " & _
                                 "Where H.OrderReturnHeaderID = @OrderReturnHeaderID " & _
                                 "union all " & _
                                 "select A.AddGoldTaxPer as ItemTaxPer, sum(AddGoldTax) AS ItemTaxAmount " & _
                                 "from tbl_OrderReturnHeader A Where A.OrderReturnHeaderID=@OrderReturnHeaderID " & _
                                 "group By A.AddGoldTaxPer) As M group by ItemTaxPer "


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OrderReturnHeaderID", DbType.String, OrderReturnHeaderID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

