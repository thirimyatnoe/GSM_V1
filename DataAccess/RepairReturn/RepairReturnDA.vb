Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common

Namespace RepairReturn
    Public Class RepairReturnDA
        Implements IRepairReturnDA

#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As IRepairReturnDA = New DataAccess.RepairReturn.RepairReturnDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IRepairReturnDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function InsertRepairReturnHeader(ByVal Obj As CommonInfo.RepairReturnHeaderInfo) As Boolean Implements IRepairReturnDA.InsertRepairReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            'Dim strgetID As String
            Dim id As Integer = 0
            Try
                strCommandText = "INSERT INTO tbl_ReturnRepairHeader(ReturnRepairID,RepairID,ReturnDate,AllReturnTotalAmount, AllReturnAddOrSub, ReturnDiscountAmount, ReturnPaidAmount, Remark, LastModifiedLoginUserName, LastModifiedDate, AdvanceAmount, BalanceAmount, StaffID,IsDelete,IsSync,LocationID)"
                strCommandText += " VALUES(@ReturnRepairID,@RepairID,@ReturnDate,@AllReturnTotalAmount,@AllReturnAddOrSub,@ReturnDiscountAmount,@ReturnPaidAmount,@Remark,@LastModifiedLoginUserName, @LastModifiedDate, @AdvanceAmount, @BalanceAmount, @StaffID,0,0,@LocationID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ReturnRepairID", DbType.String, Obj.ReturnRepairID)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, Obj.RepairID)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.Date, Obj.ReturnDate)
                DB.AddInParameter(DBComm, "@AllReturnTotalAmount", DbType.Int32, Obj.AllReturnTotalAmount)
                DB.AddInParameter(DBComm, "@AllReturnAddOrSub", DbType.Int32, Obj.AllReturnAddOrSub)
                DB.AddInParameter(DBComm, "@ReturnDiscountAmount", DbType.Int32, Obj.ReturnDiscountAmount)
                DB.AddInParameter(DBComm, "@ReturnPaidAmount", DbType.Int32, Obj.ReturnPaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int32, Obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@BalanceAmount", DbType.Int32, Obj.BalanceAmount)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    'strgetID = "Select Max(ReturnRepairID) from tbl_ReturnRepairHeader"
                    'DBComm = DB.GetSqlStringCommand(strgetID)
                    'id = CInt(DB.ExecuteScalar(DBComm))
                    'Return id
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
        Public Function UpdateRepairReturnHeader(ByVal Obj As CommonInfo.RepairReturnHeaderInfo) As Boolean Implements IRepairReturnDA.UpdateRepairReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_ReturnRepairHeader Set  RepairID=@RepairID,ReturnDate=@ReturnDate,AllReturnTotalAmount=@AllReturnTotalAmount,AllReturnAddOrSub=@AllReturnAddOrSub,ReturnDiscountAmount=@ReturnDiscountAmount,ReturnPaidAmount=@ReturnPaidAmount,Remark=@Remark,LastModifiedLoginUserName=@LastModifiedLoginUserName,LastModifiedDate=@LastModifiedDate, AdvanceAmount=@AdvanceAmount, BalanceAmount=@BalanceAmount, StaffID=@StaffID,IsSync=0"
                strCommandText += " where ReturnRepairID= @ReturnRepairID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairID", DbType.String, Obj.ReturnRepairID)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, Obj.RepairID)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.Date, Obj.ReturnDate)
                DB.AddInParameter(DBComm, "@AllReturnTotalAmount", DbType.Int32, Obj.AllReturnTotalAmount)
                DB.AddInParameter(DBComm, "@AllReturnAddOrSub", DbType.Int32, Obj.AllReturnAddOrSub)
                DB.AddInParameter(DBComm, "@ReturnDiscountAmount", DbType.Int32, Obj.ReturnDiscountAmount)
                DB.AddInParameter(DBComm, "@ReturnPaidAmount", DbType.Int32, Obj.ReturnPaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)
                DB.AddInParameter(DBComm, "@AdvanceAmount", DbType.Int32, Obj.AdvanceAmount)
                DB.AddInParameter(DBComm, "@BalanceAmount", DbType.Int32, Obj.BalanceAmount)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
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
        Public Function InsertRepairRreturnDetail(obj As CommonInfo.RepairReturnDetailInfo) As Boolean Implements IRepairReturnDA.InsertRepairRreturnDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ReturnRepairDetail ( ReturnRepairDetailID,ReturnRepairID,RepairDetailID,ChangeSaleRate,ReturnItemTK,ReturnItemTG,ReturnGoldTK,ReturnGoldTG,ReturnGemTK,ReturnGemTG,OrgGoldTK,OrgGoldTG,OrgGemTK,OrgGemTG,WasteTK,WasteTG, DesignCharges, WhiteCharges, PlatingCharges,MountingCharges,ReturnGoldPrice,ReturnGemPrice,ReturnTotalAmount,ReturnAddOrSub)"
                strCommandText += " Values (@ReturnRepairDetailID,@ReturnRepairID,@RepairDetailID,@ChangeSaleRate,@ReturnItemTK,@ReturnItemTG,@ReturnGoldTK,@ReturnGoldTG,@ReturnGemTK,@ReturnGemTG,@OrgGoldTK,@OrgGoldTG,@OrgGemTK,@OrgGemTG,@WasteTK,@WasteTG,@DesignCharges, @WhiteCharges, @PlatingCharges,@MountingCharges,@ReturnGoldPrice,@ReturnGemPrice,@ReturnTotalAmount,@ReturnAddOrSub)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairDetailID", DbType.String, obj.ReturnRepairDetailID)
                DB.AddInParameter(DBComm, "@ReturnRepairID", DbType.String, obj.ReturnRepairID)
                DB.AddInParameter(DBComm, "@RepairDetailID", DbType.String, obj.RepairDetailID)
                DB.AddInParameter(DBComm, "@ChangeSaleRate", DbType.Int64, obj.ChangeSaleRate)
                DB.AddInParameter(DBComm, "@ReturnItemTG", DbType.Decimal, obj.ReturnItemTG)
                DB.AddInParameter(DBComm, "@ReturnItemTK", DbType.Decimal, obj.ReturnItemTK)
                DB.AddInParameter(DBComm, "@ReturnGoldTG", DbType.Decimal, obj.ReturnGoldTG)
                DB.AddInParameter(DBComm, "@ReturnGoldTK", DbType.Decimal, obj.ReturnGoldTK)
                DB.AddInParameter(DBComm, "@ReturnGemTG", DbType.Decimal, obj.ReturnGemTG)
                DB.AddInParameter(DBComm, "@ReturnGemTK", DbType.Decimal, obj.ReturnGemTK)
                DB.AddInParameter(DBComm, "@OrgGemTG", DbType.Decimal, obj.OrgGemTG)
                DB.AddInParameter(DBComm, "@OrgGemTK", DbType.Decimal, obj.OrgGemTK)
                DB.AddInParameter(DBComm, "@OrgGoldTG", DbType.Decimal, obj.OrgGoldTG)
                DB.AddInParameter(DBComm, "@OrgGoldTK", DbType.Decimal, obj.OrgGoldTK)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, obj.DesignCharges)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, obj.MountingCharges)
                DB.AddInParameter(DBComm, "@ReturnGoldPrice", DbType.Int64, obj.ReturnGoldPrice)
                DB.AddInParameter(DBComm, "@ReturnGemPrice", DbType.Int64, obj.ReturnGemPrice)
                DB.AddInParameter(DBComm, "@ReturnTotalAmount", DbType.Int64, obj.ReturnTotalAmount)
                DB.AddInParameter(DBComm, "@ReturnAddOrSub", DbType.Int64, obj.ReturnAddOrSub)

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

        Public Function UpdateRepairRreturnDetail(obj As CommonInfo.RepairReturnDetailInfo) As Boolean Implements IRepairReturnDA.UpdateRepairRreturnDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_ReturnRepairDetail Set  ReturnRepairID=@ReturnRepairID,RepairDetailID=@RepairDetailID,ChangeSaleRate=@ChangeSaleRate,ReturnItemTK=@ReturnItemTK,ReturnItemTG=@ReturnItemTG, ReturnGoldTK=@ReturnGoldTK, ReturnGoldTG=@ReturnGoldTG, ReturnGemTK=@ReturnGemTK,ReturnGemTG=@ReturnGemTG, OrgGoldTK=@OrgGoldTK, OrgGoldTG=@OrgGoldTG, OrgGemTK=@OrgGemTK, OrgGemTG=@OrgGemTG, WasteTK=@WasteTK, WasteTG=@WasteTG,"
                strCommandText += " DesignCharges=@DesignCharges, WhiteCharges=@WhiteCharges, MountingCharges=@MountingCharges, PlatingCharges=@PlatingCharges, ReturnGoldPrice=@ReturnGoldPrice, ReturnGemPrice=@ReturnGemPrice, ReturnTotalAmount=@ReturnTotalAmount, ReturnAddOrSub=@ReturnAddOrSub"
                strCommandText += " where ReturnRepairDetailID= @ReturnRepairDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairDetailID", DbType.String, obj.ReturnRepairDetailID)
                DB.AddInParameter(DBComm, "@ReturnRepairID", DbType.String, obj.ReturnRepairID)
                DB.AddInParameter(DBComm, "@RepairDetailID", DbType.String, obj.RepairDetailID)
                DB.AddInParameter(DBComm, "@ChangeSaleRate", DbType.Int64, obj.ChangeSaleRate)
                DB.AddInParameter(DBComm, "@ReturnItemTG", DbType.Decimal, obj.ReturnItemTG)
                DB.AddInParameter(DBComm, "@ReturnItemTK", DbType.Decimal, obj.ReturnItemTK)
                DB.AddInParameter(DBComm, "@ReturnGoldTG", DbType.Decimal, obj.ReturnGoldTG)
                DB.AddInParameter(DBComm, "@ReturnGoldTK", DbType.Decimal, obj.ReturnGoldTK)
                DB.AddInParameter(DBComm, "@ReturnGemTG", DbType.Decimal, obj.ReturnGemTG)
                DB.AddInParameter(DBComm, "@ReturnGemTK", DbType.Decimal, obj.ReturnGemTK)
                DB.AddInParameter(DBComm, "@OrgGemTG", DbType.Decimal, obj.OrgGemTG)
                DB.AddInParameter(DBComm, "@OrgGemTK", DbType.Decimal, obj.OrgGemTK)
                DB.AddInParameter(DBComm, "@OrgGoldTG", DbType.Decimal, obj.OrgGoldTG)
                DB.AddInParameter(DBComm, "@OrgGoldTK", DbType.Decimal, obj.OrgGoldTK)
                DB.AddInParameter(DBComm, "@WasteTK", DbType.Decimal, obj.WasteTK)
                DB.AddInParameter(DBComm, "@WasteTG", DbType.Decimal, obj.WasteTG)
                DB.AddInParameter(DBComm, "@DesignCharges", DbType.Int64, obj.DesignCharges)
                DB.AddInParameter(DBComm, "@WhiteCharges", DbType.Int64, obj.WhiteCharges)
                DB.AddInParameter(DBComm, "@PlatingCharges", DbType.Int64, obj.PlatingCharges)
                DB.AddInParameter(DBComm, "@MountingCharges", DbType.Int64, obj.MountingCharges)
                DB.AddInParameter(DBComm, "@ReturnGoldPrice", DbType.Int64, obj.ReturnGoldPrice)
                DB.AddInParameter(DBComm, "@ReturnGemPrice", DbType.Int64, obj.ReturnGemPrice)
                DB.AddInParameter(DBComm, "@ReturnTotalAmount", DbType.Int64, obj.ReturnTotalAmount)
                DB.AddInParameter(DBComm, "@ReturnAddOrSub", DbType.Int64, obj.ReturnAddOrSub)
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
        Public Function DeleteReturnRepairDetail(ByVal ReturnRepairDetailID As String) As Boolean Implements IRepairReturnDA.DeleteReturnRepairDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_ReturnRepairDetail WHERE  ReturnRepairDetailID= @ReturnRepairDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairDetailID", DbType.String, ReturnRepairDetailID)
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
        Public Function GetReturnRepairGemItemByDetailID(ByVal ReturnRepairDetailID As String) As System.Data.DataTable Implements IRepairReturnDA.GetReturnRepairGemItemByDetailID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.ReturnRepairGemID ,G.ReturnRepairDetailID, G.GemsCategoryID, C.GemsCategory, " & _
                                 " G.Description, G.GemsTK, G.GemsTG, YOrCOrG, GemsTW, G.QTY, G.Type, G.UnitPrice, G.Amount,G.IsNewGems,  " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY  " & _
                                 " From tbl_ReturnRepairGem G LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=G.GemsCategoryID " & _
                                 " Where G.ReturnRepairDetailID = '" & ReturnRepairDetailID & "' "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function DeleteRepairReturnGemsItemByGemsID(ByVal ReturnRepairGemID As String) As Boolean Implements IRepairReturnDA.DeleteRepairReturnGemsItemByGemsID
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_ReturnRepairGem WHERE  ReturnRepairGemID= @ReturnRepairGemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairGemID", DbType.String, ReturnRepairGemID)
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
        Public Function InsertRepairReturnGemItem(ByVal Obj As CommonInfo.RepairReturnGemInfo) As Boolean Implements IRepairReturnDA.InsertRepairReturnGemItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_ReturnRepairGem ( ReturnRepairGemID, ReturnRepairDetailID, GemsCategoryID, Description, GemsTK, GemsTG, YOrCOrG, GemsTW, QTY, Type, UnitPrice, Amount, IsNewGems)"
                strCommandText += " VALUES(@ReturnRepairGemID, @ReturnRepairDetailID, @GemsCategoryID, @Description, @GemsTK, @GemsTG, @YOrCOrG, @GemsTW, @QTY, @Type, @UnitPrice, @Amount, @IsNewGems)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairGemID", DbType.String, Obj.ReturnRepairGemID)
                DB.AddInParameter(DBComm, "@ReturnRepairDetailID", DbType.String, Obj.ReturnRepairDetailID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, Obj.GemsCategoryID)
                DB.AddInParameter(DBComm, "@Description", DbType.String, Obj.Description)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, Obj.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, Obj.GemsTG)
                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, Obj.YOrCOrG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, Obj.GemsTW)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, Obj.QTY)
                DB.AddInParameter(DBComm, "@Type", DbType.String, Obj.Type)
                DB.AddInParameter(DBComm, "@UnitPrice", DbType.Int64, Obj.UnitPrice)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)
                DB.AddInParameter(DBComm, "@IsNewGems", DbType.Boolean, Obj.IsNewGems)
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
        Public Function GetRepairReturnDetailForUpdate(RepairID As String) As DataTable Implements IRepairReturnDA.GetRepairReturnDetailForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Try
                strCommandText = "Select D.*,H.RepairID,H.ReturnDate,H.AllReturnTotalAmount,H.AllReturnAddOrSub,H.ReturnDiscountAmount,H.AllReturnTotalAmount-(H.AllReturnAddOrSub + H.ReturnDiscountAmount) As AllNetAmount,H.ReturnPaidAmount, " & _
                                 " ((H.AllReturnTotalAmount-(H.AllReturnAddOrSub + H.ReturnDiscountAmount))-H.ReturnPaidAmount) As AllBalanceAmount,H.Remark,H.IsPaid,I.ItemCategory,N.ItemName,G.GoldQuality,RH.ItemCategoryID,RH.ItemNameID,RH.GoldQualityID,RH.BarcodeNo " & _
                                 " From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H on H.ReturnRepairID=D.ReturnRepairID LEFT JOIN tbl_RepairDetail RH on RH.RepairID=H.RepairID LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=RH.ItemCategoryID " & _
                                 " LEFT JOIN tbl_ItemName N on N.ItemNameID=RH.ItemNameID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=RH.GoldQualityID" & _
                                 " Where H.RepairID ='" & RepairID & "' Order By D.ReturnRepairDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function UpdateRepairReceiveHeaderByIsReturn(ByVal Obj As CommonInfo.RepairHeaderInfo) As Boolean Implements IRepairReturnDA.UpdateRepairReceiveHeaderByIsReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try

                strCommandText = "UPDATE tbl_RepairHeader SET IsAllReturn=@IsAllReturn, ReturnDate=(CASE @IsAllReturn WHEN 0 THEN NULL ELSE @ReturnDate END) " & _
              " WHERE RepairID=@RepairID  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, Obj.RepairID)
                DB.AddInParameter(DBComm, "@IsAllReturn", DbType.Boolean, Obj.IsAllReturn)
                DB.AddInParameter(DBComm, "@ReturnDate", DbType.Date, IIf(Obj.ReturnDate <> Nothing, Obj.ReturnDate, DBNull.Value))
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

        Public Function GetRepairReturnDetailByHeaderID(ByVal RepairReturnHeaderID As String) As System.Data.DataTable Implements IRepairReturnDA.GetRepairReturnDetailByHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select D.ReturnRepairDetailID , D.ReturnRepairID, D.RepairDetailID, R.BarcodeNo, CASE R.DetailRemark WHEN '' THEN N.ItemName ELSE R.DetailRemark END AS ItemName, R.GoldQualityID, GQ.GoldQuality, R.LengthOrWidth," & _
                                 " D.ChangeSaleRate, D.ReturnItemTK, D.ReturnItemTG, D.ReturnGoldTK, D.ReturnGoldTG, D.ReturnGemTK, D.ReturnGemTG, D.WasteTK, D.WasteTG, R.ItemTK As OrgItemTK, R.ItemTG AS OrgItemTG,  " & _
                                 " D.OrgGoldTK, D.OrgGoldTG, D.OrgGemTK, D.OrgGemTG, D.DesignCharges, D.WhiteCharges, D.PlatingCharges, D.MountingCharges, D.ReturnGoldPrice, D.ReturnGemPrice, D.ReturnTotalAmount, D.ReturnAddOrSub, (D.ReturnTotalAmount+D.ReturnAddOrSub) As ReturnNetAmount, " & _
                                 " CAST(D.ReturnItemTK AS INT) AS RItemK,CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT) AS RItemP, " & _
                                 " CAST((((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16)-CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RItemY  " & _
                                 " From tbl_ReturnRepairDetail D INNER JOIN tbl_RepairDetail R ON D.RepairDetailID=R.RepairDetailID  " & _
                                 " LEFT JOIN tbl_ItemName N On N.ItemNameID=R.ItemNameID LEFT JOIN tbl_GoldQuality GQ ON GQ.GoldQualityID=R.GoldQualityID " & _
                                 " Where D.ReturnRepairID = '" & RepairReturnHeaderID & "' "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function UpdateRepairDetailByIsExit(ByVal Obj As CommonInfo.RepairDetailInfo) As Boolean Implements IRepairReturnDA.UpdateRepairDetailByIsExit
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "UPDATE tbl_RepairDetail SET IsExit=@IsExit " & _
               " WHERE RepairDetailID=@RepairDetailID  "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairDetailID", DbType.String, Obj.RepairDetailID)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, Obj.IsExit)
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

        Public Function GetRepairReturnDetailByRepairID(ByVal RepairID As String) As System.Data.DataTable Implements IRepairReturnDA.GetRepairReturnDetailByRepairID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select * FROM tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H ON D.ReturnRepairID=H.ReturnRepairID " & _
                                 " Where H.RepairID = '" & RepairID & "' "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function DeleteRepairReturn(ByVal RepairReturnID As String) As Boolean Implements IRepairReturnDA.DeleteRepairReturn
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_ReturnRepairHeader WHERE  ReturnRepairID= @ReturnRepairID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairID", DbType.String, RepairReturnID)
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

        Public Function GetAllRepairReturnHeader() As System.Data.DataTable Implements IRepairReturnDA.GetAllRepairReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.ReturnRepairID AS [@ReturnRepairID], H.RepairID AS VoucherNo, CONVERT(VARCHAR(10),H.ReturnDate,105) as ReturnDate, C.CustomerName AS [Customer_], C.CustomerAddress As [Address_], S.Staff AS [Staff_],((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) AS NetAmount, H.AllReturnTotalAmount AS TotalAmount, H.AllReturnAddOrSub AS AddSub, H.ReturnDiscountAmount AS DiscountAmount, H.ReturnPaidAmount AS PaidAmount, H.AdvanceAmount, " & _
                                "  H.BalanceAmount, H.Remark As [Remark_], H.ReturnDate AS [@RDate] " & _
                                " From tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R on H.RepairID=R.RepairID LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID " & _
                                " LEFT JOIN tbl_Customer C on C.CustomerID=R.CustomerID ORDER BY [@RDate] DESC, H.ReturnRepairID DESC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetRepairReturnHeaderInfoByID(ByVal RepairReturnID As String) As CommonInfo.RepairReturnHeaderInfo Implements IRepairReturnDA.GetRepairReturnHeaderInfoByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objRepairReturn As New RepairReturnHeaderInfo
            Try
                strCommandText = " SELECT H.*, R.CustomerID FROM tbl_ReturnRepairHeader H LEFT JOIN tbl_RepairHeader R ON H.RepairID=R.RepairID WHERE H.ReturnRepairID= @ReturnRepairID  "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairID", DbType.String, RepairReturnID)
                drResult = DB.ExecuteReader(DBComm)

                If drResult.Read() Then
                    With objRepairReturn
                        .ReturnRepairID = drResult.Item("ReturnRepairID")
                        .RepairID = drResult.Item("RepairID")
                        .ReturnDate = drResult.Item("ReturnDate")
                        .AllReturnTotalAmount = drResult.Item("AllReturnTotalAmount")
                        .AllReturnAddOrSub = drResult.Item("AllReturnAddOrSub")
                        .ReturnDiscountAmount = drResult.Item("ReturnDiscountAmount")
                        .ReturnPaidAmount = drResult.Item("ReturnPaidAmount")
                        .Remark = drResult.Item("Remark")
                        .BalanceAmount = drResult.Item("BalanceAmount")
                        .AdvanceAmount = drResult.Item("AdvanceAmount")
                        .StaffID = drResult.Item("StaffID")
                        .CustomerID = drResult.Item("CustomerID")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objRepairReturn
        End Function

        Public Function GetRepairReturnGemDataByHeaderID(ByVal RepairReturnID As String) As System.Data.DataTable Implements IRepairReturnDA.GetRepairReturnGemDataByHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select G.ReturnRepairGemID ,G.ReturnRepairDetailID, G.GemsCategoryID, C.GemsCategory, " & _
                                 " G.Description,G.GemsTK, G.GemsTG, YOrCOrG, GemsTW, G.QTY, G.Type, G.UnitPrice, G.Amount,G.IsNewGems,  " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK, CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,  " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY  " & _
                                 " From tbl_ReturnRepairGem G LEFT JOIN tbl_ReturnRepairDetail D On G.ReturnRepairDetailID=D.ReturnRepairDetailID " & _
                                 " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=G.GemsCategoryID " & _
                                 " Where D.ReturnRepairID = '" & RepairReturnID & "' "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetRepairReturnForVoucher(ByVal RepairID As String) As System.Data.DataTable Implements IRepairReturnDA.GetRepairReturnForVoucher
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.ReturnRepairID,H.RepairID,H.ReturnDate,H.AllReturnTotalAmount,H.AllReturnAddOrSub, " & _
                                 " H.ReturnDiscountAmount,H.ReturnPaidAmount,H.AdvanceAmount,H.BalanceAmount,H.StaffID,S.Staff,RH.CustomerID,C.CustomerName,C.CustomerAddress,C.CustomerTel, " & _
                                 " RD.BarcodeNo,RD.ItemCategoryID,RD.ItemNameID,RD.GoldQualityID,RD.LengthOrWidth,RD.CurrentPrice,RD.Design,RD.ItemTK,RD.ItemTG, " & _
                                 " D.ReturnRepairDetailID,D.RepairDetailID,D.ChangeSaleRate,D.DesignCharges,D.WhiteCharges,PlatingCharges,MountingCharges,RD.DetailRemark, " & _
                                 " I.ItemCategory,N.ItemName,Q.GoldQuality, Q.IsGramRate, (D.DesignCharges+D.PlatingCharges+D.MountingCharges+D.WhiteCharges) As TotalCharges, " & _
                                 " CAST(RD.ItemTK AS INT) AS OItemK,CAST((RD.ItemTK-CAST(RD.ItemTK AS INT))*16 AS INT) AS OItemP,   " & _
                                 " CAST((((RD.ItemTK-CAST(RD.ItemTK AS INT))*16)-CAST((RD.ItemTK-CAST(RD.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OItemY,  " & _
                                 " D.ReturnGoldPrice,D.ReturnGemPrice,D.ReturnTotalAmount,D.ReturnAddOrSub, " & _
                                 " D.ReturnItemTK,D.ReturnItemTG,CAST(D.ReturnItemTK AS INT) AS RItemK,CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT) AS RItemP,   " & _
                                 " CAST((((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16)-CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RItemY,  " & _
                                 " D.ReturnGoldTG,D.ReturnGoldTK,CAST(D.ReturnGoldTK AS INT) AS RGoldK,CAST((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16 AS INT) AS RGoldP,   " & _
                                 " CAST((((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16)-CAST((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RGoldY,  " & _
                                 " D.ReturnGemTK,D.ReturnGemTG,CAST(D.ReturnGemTK AS INT) AS RGemK,CAST((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16 AS INT) AS RGemP,   " & _
                                 " CAST((((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16)-CAST((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RGemY,  " & _
                                 " D.OrgGoldTK,D.OrgGoldTG,CAST(D.OrgGoldTK AS INT) AS OGoldK,CAST((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16 AS INT) AS OGoldP,  " & _
                                 " CAST((((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16)-CAST((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OGoldY,  " & _
                                 " D.OrgGemTK,D.OrgGemTG,CAST(D.OrgGemTK AS INT) AS OGemK,CAST((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16 AS INT) AS OGemP,    " & _
                                 " CAST((((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16)-CAST((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS OGemY,  " & _
                                 " D.WasteTK,D.WasteTG,CAST(D.WasteTK AS INT) AS WasteK,CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " G.ReturnRepairGemID, G.GemsCategoryID, O.GemsCategory, G.Description,G.YOrCOrG,G.GemsTG,G.GemsTK,G.GemsTG,G.QTY,G.Type,G.UnitPrice,G.Amount,G.IsNewGems, " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK,CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,   " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY,  " & _
                                 " D.ReturnGoldTK-D.OrgGoldTK As DiffTK, D.ReturnGoldTG-D.OrgGoldTG As DiffTG, " & _
                                 " CAST((D.ReturnGoldTK+D.WasteTK)-D.OrgGoldTK AS INT) AS DiffK,CAST(((D.ReturnGoldTK+D.WasteTK)-D.OrgGoldTK-CAST((D.ReturnGoldTK+D.WasteTK)-D.OrgGoldTK AS INT))*16 AS INT) AS DiffP,    " & _
                                 " CAST(((((D.ReturnGoldTK+D.WasteTK)-D.OrgGoldTK-CAST((D.ReturnGoldTK+D.WasteTK)-D.OrgGoldTK AS INT))*16)-CAST(((D.ReturnGoldTK+D.WasteTK)-D.OrgGoldTK-CAST((D.ReturnGoldTK+D.WasteTK)-D.OrgGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS DiffY, " & _
                                 " CAST((D.ReturnGoldTK+D.WasteTK) AS INT) AS WGK,CAST(((D.ReturnGoldTK+D.WasteTK)-CAST((D.ReturnGoldTK+D.WasteTK) AS INT))*16 AS INT) AS WGP,    " & _
                                 " CAST(((((D.ReturnGoldTK+D.WasteTK)-CAST((D.ReturnGoldTK+D.WasteTK) AS INT))*16)-CAST(((D.ReturnGoldTK+D.WasteTK)-CAST((D.ReturnGoldTK+D.WasteTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WGY,H.Remark " & _
                                 " from tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H on H.ReturnRepairID=D.ReturnRepairID " & _
                                 " LEFT JOIN tbl_ReturnRepairGem G on G.ReturnRepairDetailID=D.ReturnRepairDetailID " & _
                                 " LEFT JOIN tbl_RepairHeader RH on RH.RepairID=H.RepairID LEFT JOIN tbl_RepairDetail RD on RD.RepairDetailID=D.RepairDetailID " & _
                                 " LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID LEFT JOIN tbl_Customer C on C.CustomerID=RH.CustomerID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=RD.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=RD.ItemNameID " & _
                                 " LEFT JOIN tbl_GoldQuality Q on Q.GoldQualityID=RD.GoldQualityID  " & _
                                 " LEFT JOIN tbl_GemsCategory O ON G.GemsCategoryID=O.GemsCategoryID " & _
                                 " Where H.RepairID = '" & RepairID & "' "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function

        Public Function GetRepairReturnSummary(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements IRepairReturnDA.GetRepairReturnSummary
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.ReturnRepairID, H.RepairID, H.ReturnDate, H.AllReturnTotalAmount, H.AllReturnAddOrSub, ((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) As AllReturnNetAmount," & _
                                " H.ReturnDiscountAmount, (H.ReturnDiscountAmount-H.AllReturnAddOrSub) AS TotalDiscountAmount, CASE WHEN H.ReturnPaidAmount<0 THEN H.ReturnPaidAmount ELSE (H.ReturnPaidAmount+H.AdvanceAmount) END AS PaidAmount, H.AdvanceAmount, H.BalanceAmount, H.StaffID, S.Staff, RH.CustomerID, C.CustomerName, C.CustomerAddress, C.CustomerTel, " & _
                                " RD.BarcodeNo, RD.ItemCategoryID,RD.ItemNameID,RD.GoldQualityID,RD.LengthOrWidth,RD.CurrentPrice, RD.Design, CASE RD.DetailRemark WHEN '' THEN N.ItemName ELSE RD.DetailRemark END AS ItemName, " & _
                                " D.ReturnRepairDetailID,D.RepairDetailID,D.ChangeSaleRate,D.DesignCharges,D.WhiteCharges,PlatingCharges,MountingCharges,RD.DetailRemark, " & _
                                " I.ItemCategory, Q.GoldQuality,(D.DesignCharges+D.PlatingCharges+D.MountingCharges+D.WhiteCharges) As TotalCharges, " & _
                                " RD.ItemTK As OrgItemTK, RD.ItemTG AS OrgItemTG,CAST(RD.ItemTK AS INT) AS OrgItemK,CAST((RD.ItemTK-CAST(RD.ItemTK AS INT))*16 AS INT) AS OrgItemP,   " & _
                                " CAST((((RD.ItemTK-CAST(RD.ItemTK AS INT))*16)-CAST((RD.ItemTK-CAST(RD.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OrgItemY,  " & _
                                " D.ReturnGoldPrice,D.ReturnGemPrice,D.ReturnTotalAmount,D.ReturnAddOrSub, (D.ReturnTotalAmount+D.ReturnAddOrSub) AS ReturnNetAmount, " & _
                                " D.ReturnItemTK AS RItemTK,D.ReturnItemTG As RItemTG,CAST(D.ReturnItemTK AS INT) AS RItemK,CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT) AS RItemP,   " & _
                                " CAST((((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16)-CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RItemY,  " & _
                                " D.ReturnGoldTG As RGoldTG,D.ReturnGoldTK AS RGold,CAST(D.ReturnGoldTK AS INT) AS RGoldK,CAST((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16 AS INT) AS RGoldP,   " & _
                                " CAST((((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16)-CAST((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RGoldY,  " & _
                                " D.ReturnGemTK AS RGemTK,D.ReturnGemTG AS RGemTG,CAST(D.ReturnGemTK AS INT) AS RGemK,CAST((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16 AS INT) AS RGemP,   " & _
                                " CAST((((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16)-CAST((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS RGemY,  " & _
                                " D.OrgGoldTK,D.OrgGoldTG,CAST(D.OrgGoldTK AS INT) AS OrgGoldK,CAST((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16 AS INT) AS OrgGoldP,  " & _
                                " CAST((((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16)-CAST((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OrgGoldY,  " & _
                                " D.OrgGemTK,D.OrgGemTG,CAST(D.OrgGemTK AS INT) AS OrgGemK,CAST((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16 AS INT) AS OrgGemP,    " & _
                                " CAST((((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16)-CAST((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS OrgGemY,  " & _
                                " D.WasteTK,D.WasteTG,CAST(D.WasteTK AS INT) AS WasteK,CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                " (D.ReturnItemTK-RD.ItemTK) As DiffItemTK, (D.ReturnItemTG-RD.ItemTG) As DiffItemTG, " & _
                                " CAST((D.ReturnItemTK-RD.ItemTK) AS INT) AS DiffItemK,CAST(((D.ReturnItemTK-RD.ItemTK)-CAST((D.ReturnItemTK-RD.ItemTK) AS INT))*16 AS INT) AS DiffItemP,   " & _
                                " CAST(((((D.ReturnItemTK-RD.ItemTK)-CAST((D.ReturnItemTK-RD.ItemTK) AS INT))*16)-CAST(((D.ReturnItemTK-RD.ItemTK)-CAST((D.ReturnItemTK-RD.ItemTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS DiffItemY,  " & _
                                " (D.ReturnGoldTK-D.OrgGoldTK) As DiffGoldTK, (D.ReturnGoldTG-D.OrgGoldTG) As DiffGoldTG, " & _
                                " CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT) AS DiffGoldK,CAST(((D.ReturnGoldTK-D.OrgGoldTK)-CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT))*16 AS INT) AS DiffGoldP,   " & _
                                " CAST(((((D.ReturnGoldTK-D.OrgGoldTK)-CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT))*16)-CAST(((D.ReturnGoldTK-D.OrgGoldTK)-CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS DiffGoldY,  " & _
                                " 0 As SumTotalAmount ,H.ReturnDate As [@RDate] " & _
                                " from tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H on H.ReturnRepairID=D.ReturnRepairID " & _
                                " LEFT JOIN tbl_RepairHeader RH on RH.RepairID=H.RepairID LEFT JOIN tbl_RepairDetail RD on RD.RepairDetailID=D.RepairDetailID " & _
                                " LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID LEFT JOIN tbl_Customer C on C.CustomerID=RH.CustomerID " & _
                                " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=RD.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=RD.ItemNameID " & _
                                " LEFT JOIN tbl_GoldQuality Q on Q.GoldQualityID=RD.GoldQualityID  " & _
                                " WHERE H.ReturnDate BETWEEN @FromDate And @ToDate " & Cristr & "Order By [@RDate] DESC "

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

        Public Function GetRepairReturnInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairReturnDetailInfo Implements IRepairReturnDA.GetRepairReturnInvoiceDetailForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New RepairReturnDetailInfo
            Try


                strCommandText = " Select Sum(CAST((D.WasteTG) AS DECIMAL(18,3)))As WasteTG, Sum(D.WasteTK) As WasteTK,   " & _
                                 " Sum(CAST(D.OrgGoldTG AS DECIMAL(18,3))) As OrgGoldTG, Sum(D.OrgGoldTK) As OrgGoldTK, Sum(CAST(D.ReturnGoldTG AS DECIMAL(18,3))) As ReturnGoldTG, Sum(D.ReturnGoldTK) As ReturnGoldTK,  " & _
                                 " Sum(CAST(D.ReturnItemTG AS DECIMAL(18,3))) As ReturnItemTG,Sum(D.ReturnItemTK) As ReturnItemTK,Sum(CAST(RD.ItemTG AS DECIMAL(18,3))) As ItemTG,Sum(RD.ItemTK) As ItemTK,  " & _
                                 " Sum(CAST(D.OrgGemTG AS DECIMAL(18,3))) As OrgGemTG,Sum(D.OrgGemTK) As OrgGemTK,Sum(CAST(D.ReturnGemTG AS DECIMAL(18,3))) As ReturnGemTG,Sum(D.ReturnGemTK) As ReturnGemTK,  " & _
                                 " Sum(D.ReturnTotalAmount+D.ReturnAddOrSub) As ItemAmount" & _
                                 " From tbl_ReturnRepairHeader H LEFT JOIN tbl_ReturnRepairDetail D on D.ReturnRepairID=H.ReturnRepairID LEFT JOIN tbl_RepairDetail RD on RD.RepairDetailID=D.RepairDetailID LEFT JOIN tbl_RepairHeader RH on RH.RepairID=H.RepairID  " & _
                                 " WHERE H.ReturnDate BETWEEN @FromDate And @ToDate " & criStr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(FromDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(ToDate.Date & " 23:59:59"))
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ItemTG = IIf(IsDBNull(drResult("ItemTG")), 0, drResult("ItemTG"))
                        .ItemTK = IIf(IsDBNull(drResult("ItemTK")), 0, drResult("ItemTK"))
                        .ReturnItemTK = IIf(IsDBNull(drResult("ReturnItemTK")), 0, drResult("ReturnItemTK"))
                        .ReturnItemTG = IIf(IsDBNull(drResult("ReturnItemTG")), 0, drResult("ReturnItemTG"))
                        .OrgGoldTG = IIf(IsDBNull(drResult("OrgGoldTG")), 0, drResult("OrgGoldTG"))
                        .OrgGoldTK = IIf(IsDBNull(drResult("OrgGoldTK")), 0, drResult("OrgGoldTK"))
                        .ReturnGoldTG = IIf(IsDBNull(drResult("ReturnGoldTG")), 0, drResult("ReturnGoldTG"))
                        .ReturnGoldTK = IIf(IsDBNull(drResult("ReturnGoldTK")), 0, drResult("ReturnGoldTK"))
                        .OrgGemTG = IIf(IsDBNull(drResult("OrgGemTG")), 0, drResult("OrgGemTG"))
                        .OrgGemTK = IIf(IsDBNull(drResult("OrgGemTK")), 0, drResult("OrgGemTK"))
                        .ReturnGemTK = IIf(IsDBNull(drResult("ReturnGemTK")), 0, drResult("ReturnGemTK"))
                        .ReturnGemTG = IIf(IsDBNull(drResult("ReturnGemTG")), 0, drResult("ReturnGemTG"))
                        .WasteTG = IIf(IsDBNull(drResult("WasteTG")), 0, drResult("WasteTG"))
                        .WasteTK = IIf(IsDBNull(drResult("WasteTK")), 0, drResult("WasteTK"))
                        .ItemAmount = IIf(IsDBNull(drResult("ItemAmount")), 0, drResult("ItemAmount"))
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetRepairReturnStockReportForTotalByDetail(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IRepairReturnDA.GetRepairReturnStockReportForTotalByDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " select Distinct(H.ReturnRepairID), H.AllReturnTotalAmount, ((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) As NetAmount,H.ReturnDiscountAmount,CASE WHEN H.ReturnPaidAmount<0 THEN H.ReturnPaidAmount ELSE (H.ReturnPaidAmount+H.AdvanceAmount) END AS PaidAmount,H.BalanceAmount" & _
                                 " From tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H on H.ReturnRepairID=D.ReturnRepairID  LEFT JOIN tbl_RepairDetail RD on RD.RepairDetailID=D.RepairDetailID LEFT JOIN tbl_RepairHeader RH on RH.RepairID=H.RepairID " & _
                                 " WHERE H.ReturnDate BETWEEN @FromDate And @ToDate " & criStr

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
        Public Function GetReturnRepairDetail(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements IRepairReturnDA.GetReturnRepairDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.ReturnRepairID, H.RepairID, H.ReturnDate, H.AllReturnTotalAmount, H.AllReturnAddOrSub, ((H.AllReturnTotalAmount+H.AllReturnAddOrSub)-H.ReturnDiscountAmount) As AllReturnNetAmount," & _
                                 " H.ReturnDiscountAmount, (H.ReturnDiscountAmount-H.AllReturnAddOrSub) AS TotalDiscountAmount, CASE WHEN H.ReturnPaidAmount<0 THEN H.ReturnPaidAmount ELSE (H.ReturnPaidAmount+H.AdvanceAmount) END AS PaidAmount, H.AdvanceAmount, H.BalanceAmount, H.StaffID, S.Staff, RH.CustomerID, C.CustomerName, C.CustomerAddress, C.CustomerTel, " & _
                                 " RD.BarcodeNo, RD.ItemCategoryID,RD.ItemNameID,RD.GoldQualityID,RD.LengthOrWidth,RD.CurrentPrice, RD.Design, CASE RD.DetailRemark WHEN '' THEN N.ItemName ELSE RD.DetailRemark END AS ItemName, " & _
                                 " D.ReturnRepairDetailID,D.RepairDetailID,D.ChangeSaleRate,D.DesignCharges,D.WhiteCharges,PlatingCharges,MountingCharges,RD.DetailRemark, " & _
                                 " I.ItemCategory, Q.GoldQuality,(D.DesignCharges+D.PlatingCharges+D.MountingCharges+D.WhiteCharges) As TotalCharges, " & _
                                 " RD.ItemTK As OrgItemTK, RD.ItemTG AS OrgItemTG,CAST(RD.ItemTK AS INT) AS OrgItemK,CAST((RD.ItemTK-CAST(RD.ItemTK AS INT))*16 AS INT) AS OrgItemP,   " & _
                                 " CAST((((RD.ItemTK-CAST(RD.ItemTK AS INT))*16)-CAST((RD.ItemTK-CAST(RD.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OrgItemY,  " & _
                                 " D.ReturnGoldPrice,D.ReturnGemPrice,D.ReturnTotalAmount,D.ReturnAddOrSub, (D.ReturnTotalAmount+D.ReturnAddOrSub) AS ReturnNetAmount, " & _
                                 " D.ReturnItemTK AS RItemTK,D.ReturnItemTG As RItemTG,CAST(D.ReturnItemTK AS INT) AS RItemK,CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT) AS RItemP,   " & _
                                 " CAST((((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16)-CAST((D.ReturnItemTK-CAST(D.ReturnItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RItemY,  " & _
                                 " D.ReturnGoldTG As RGoldTG,D.ReturnGoldTK AS RGold,CAST(D.ReturnGoldTK AS INT) AS RGoldK,CAST((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16 AS INT) AS RGoldP,   " & _
                                 " CAST((((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16)-CAST((D.ReturnGoldTK-CAST(D.ReturnGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS RGoldY,  " & _
                                 " D.ReturnGemTK AS RGemTK,D.ReturnGemTG AS RGemTG,CAST(D.ReturnGemTK AS INT) AS RGemK,CAST((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16 AS INT) AS RGemP,   " & _
                                 " CAST((((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16)-CAST((D.ReturnGemTK-CAST(D.ReturnGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS RGemY,  " & _
                                 " D.OrgGoldTK,D.OrgGoldTG,CAST(D.OrgGoldTK AS INT) AS OrgGoldK,CAST((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16 AS INT) AS OrgGoldP,  " & _
                                 " CAST((((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16)-CAST((D.OrgGoldTK-CAST(D.OrgGoldTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OrgGoldY,  " & _
                                 " D.OrgGemTK,D.OrgGemTG,CAST(D.OrgGemTK AS INT) AS OrgGemK,CAST((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16 AS INT) AS OrgGemP,    " & _
                                 " CAST((((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16)-CAST((D.OrgGemTK-CAST(D.OrgGemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS OrgGemY,  " & _
                                 " D.WasteTK,D.WasteTG,CAST(D.WasteTK AS INT) AS WasteK,CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT) AS WasteP,  " & _
                                 " CAST((((D.WasteTK-CAST(D.WasteTK AS INT))*16)-CAST((D.WasteTK-CAST(D.WasteTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS WasteY, " & _
                                 " G.ReturnRepairGemID,G.GemsCategoryID, GC.GemsCategory, G.Description, G.YOrCOrG, G.GemsTG, G.GemsTK, G.GemsTG, G.QTY, G.Type, G.UnitPrice, G.Amount, G.IsNewGems, " & _
                                 " CAST(G.GemsTK AS INT) AS GemsK,CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT) AS GemsP,   " & _
                                 " CAST((((G.GemsTK-CAST(G.GemsTK AS INT))*16)-CAST((G.GemsTK-CAST(G.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS GemsY,  " & _
                                 " (D.ReturnItemTK-RD.ItemTK) As DiffItemTK, (D.ReturnItemTG-RD.ItemTG) As DiffItemTG, " & _
                                 " CAST((D.ReturnItemTK-RD.ItemTK) AS INT) AS DiffItemK,CAST(((D.ReturnItemTK-RD.ItemTK)-CAST((D.ReturnItemTK-RD.ItemTK) AS INT))*16 AS INT) AS DiffItemP,   " & _
                                 " CAST(((((D.ReturnItemTK-RD.ItemTK)-CAST((D.ReturnItemTK-RD.ItemTK) AS INT))*16)-CAST(((D.ReturnItemTK-RD.ItemTK)-CAST((D.ReturnItemTK-RD.ItemTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS DiffItemY,  " & _
                                 " (D.ReturnGoldTK-D.OrgGoldTK) As DiffGoldTK, (D.ReturnGoldTG-D.OrgGoldTG) As DiffGoldTG, " & _
                                 " CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT) AS DiffGoldK,CAST(((D.ReturnGoldTK-D.OrgGoldTK)-CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT))*16 AS INT) AS DiffGoldP,   " & _
                                 " CAST(((((D.ReturnGoldTK-D.OrgGoldTK)-CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT))*16)-CAST(((D.ReturnGoldTK-D.OrgGoldTK)-CAST((D.ReturnGoldTK-D.OrgGoldTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS DiffGoldY,  " & _
                                 " 0 As SumTotalAmount ,H.ReturnDate As [@RDate] " & _
                                 " from tbl_ReturnRepairDetail D LEFT JOIN tbl_ReturnRepairHeader H on H.ReturnRepairID=D.ReturnRepairID " & _
                                 " LEFT JOIN tbl_ReturnRepairGem G on G.ReturnRepairDetailID=D.ReturnRepairDetailID " & _
                                 " LEFT JOIN tbl_RepairHeader RH on RH.RepairID=H.RepairID LEFT JOIN tbl_RepairDetail RD on RD.RepairDetailID=D.RepairDetailID " & _
                                 " LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID LEFT JOIN tbl_Customer C on C.CustomerID=RH.CustomerID " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=RD.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=RD.ItemNameID " & _
                                 " LEFT JOIN tbl_GoldQuality Q on Q.GoldQualityID=RD.GoldQualityID  " & _
                                 " LEFT JOIN tbl_GemsCategory GC on G.GemsCategoryID = GC.GemsCategoryID  " & _
                                 " WHERE H.ReturnDate BETWEEN @FromDate And @ToDate " & Cristr & "Order By [@RDate] DESC "

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
        Public Function GetRepairReturnDetailByRepairReturnDetailGem(ByVal ReturnRepairGemID As String) As DataTable Implements IRepairReturnDA.GetRepairReturnDetailByRepairReturnDetailGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "select * from tbl_ReturnRepairGem "
                strCommandText += " where ReturnRepairGemID=@ReturnRepairGemID"


                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnRepairGemID", DbType.String, ReturnRepairGemID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace


