Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace TransferDiamondReturn
    Public Class TransferDiamondReturnDA
        Implements ITransferDiamondReturnDA




#Region "Private Transfer"

        Private DB As Database
        Private Shared ReadOnly _instance As ITransferDiamondReturnDA = New TransferDiamondReturnDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ITransferDiamondReturnDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteTransferReturn(ByVal TransferReturnID As String) As Boolean Implements ITransferDiamondReturnDA.DeleteTransferReturn
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_TransferReturnDiamond  set IsDelete=1 WHERE  TransferReturnID= @TransferReturnID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, TransferReturnID)
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

        Public Function DeleteTransferReturnItem(ByVal TransferReturnItemID As String) As Boolean Implements ITransferDiamondReturnDA.DeleteTransferReturnItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_TransferReturnDiamondItem WHERE  TransferReturnItemID= @TransferReturnItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnItemID", DbType.String, TransferReturnItemID)
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

        Public Function GetAllTransferReturn() As System.Data.DataTable Implements ITransferDiamondReturnDA.GetAllTransferReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select T.TransferReturnID as [@TransferReturnID],convert(varchar(10),T.TransferReturnDate,105) as TransferReturnDate,S.Staff as [Name_],F.ItemCode, F.ItemName AS [ItemName_],F.ForSaleID AS [@ForSaleID],F.GoldQualityID as [@GoldQualityID],  IsNull(F.Length,'-') as Length,IsNull(F.Width,'-') as Width , F.TotalTG ," & _
                " T.Remark as [Remark_] " & _
                " from  tbl_TransferReturnDiamond T Inner Join tbl_TransferReturnDiamondItem TI On T.TransferReturnID=TI.TransferReturnID " & _
                " Left Join tbl_ForSale F On F.ForSaleID=TI.ForSaleID " & _
                " Left Join tbl_Location L ON T.LocationID=L.LocationID " & _
                " Left Join tbl_Staff S On T.StaffID=S.StaffID " & _
                " Where T.IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetHeaderTransferReturnList() As System.Data.DataTable Implements ITransferDiamondReturnDA.GetHeaderTransferReturnList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select T.TransferReturnID as [TransferReturnID],convert(varchar(10),T.TransferReturnDate,105) as TransferReturnDate, B.Location as  [BranchName_],S.Staff as [Name_]," & _
                " T.Remark as [Remark_],T.IsUpload " & _
                " from  tbl_TransferReturnDiamond T " & _
                " Left Join tbl_Location B ON T.CurrentLocationID=B.LocationID " & _
                " Left Join tbl_Staff S On T.StaffID=S.StaffID Where T.IsDelete=0 ORDER By T.TransferReturnID desc"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetTransferReturnByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ITransferDiamondReturnDA.GetTransferReturnByForSaleID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from tbl_TransferReturnDiamondItem Where ForSaleID=@ForsaleID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForsaleID", DbType.String, ForSaleID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetTransferReturn(ByVal TransferReturnID As String) As CommonInfo.TransferReturnDiamondInfo Implements ITransferDiamondReturnDA.GetTransferReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New TransferReturnDiamondInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_TransferReturnDiamond WHERE TransferReturnID= @TransferReturnID AND IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, TransferReturnID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .TransferReturnID = drResult("TransferReturnID")
                        .TransferReturnDate = drResult("TransferReturnDate")
                        .LocationID = drResult("CurrentLocationID")
                        .StaffID = drResult("StaffID")
                        .Remark = drResult("Remark")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertTransferReturn(ByVal obj As CommonInfo.TransferReturnDiamondInfo) As Boolean Implements ITransferDiamondReturnDA.InsertTransferReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_TransferReturnDiamond ( TransferReturnID,TransferReturnDate,CurrentLocationID,StaffID,Remark,LastModifiedLoginUserName,LastModifiedDate,IsDelete,IsUpload)"
                strCommandText += " Values (@TransferReturnID,@TransferReturnDate,@CurrentLocationID,@StaffID,@Remark,@LastModifiedLoginUserName,GetDate(),0,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, obj.TransferReturnID)
                DB.AddInParameter(DBComm, "@TransferReturnDate", DbType.Date, obj.TransferReturnDate)
                DB.AddInParameter(DBComm, "@CurrentLocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
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

        Public Function InsertTransferReturnItem(ByVal obj As CommonInfo.TransferReturnDiamondItemInfo) As Boolean Implements ITransferDiamondReturnDA.InsertTransferReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_TransferReturnDiamondItem ( TransferReturnItemID,TransferReturnID,ForSaleID,OriginalFixedPrice,OriginalPriceCarat,PriceCode,FixPrice)"
                strCommandText += " Values (@TransferReturnItemID,@TransferReturnID,@ForSaleID,@OriginalFixedPrice,@OriginalPriceCarat,@PriceCode,@FixPrice)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnItemID", DbType.String, obj.TransferReturnItemID)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, obj.TransferReturnID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalPriceCarat", DbType.Int64, obj.OriginalPriceCarat)
                DB.AddInParameter(DBComm, "@PriceCode", DbType.String, obj.PriceCode)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, obj.FixPrice)

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

        Public Function UpdateTransferReturn(ByVal obj As CommonInfo.TransferReturnDiamondInfo) As Boolean Implements ITransferDiamondReturnDA.UpdateTransferReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_TransferReturnDiamond set  TransferReturnID= @TransferReturnID , TransferReturnDate= @TransferReturnDate , CurrentLocationID= @CurrentLocationID , StaffID= @StaffID , Remark= @Remark,LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= GetDate() "
                strCommandText += " where TransferReturnID= @TransferReturnID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, obj.TransferReturnID)
                DB.AddInParameter(DBComm, "@TransferReturnDate", DbType.Date, obj.TransferReturnDate)
                DB.AddInParameter(DBComm, "@CurrentLocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
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

        Public Function UpdateTransferReturnItem(ByVal obj As CommonInfo.TransferReturnDiamondItemInfo) As Boolean Implements ITransferDiamondReturnDA.UpdateTransferReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_TransferReturnDiamondItem set  TransferReturnItemID= @TransferReturnItemID , TransferReturnID= @TransferReturnID , ForSaleID= @ForSaleID,OriginalFixedPrice=@OriginalFixedPrice,OriginalPriceCarat=@OriginalPriceCarat,PriceCode=@PriceCode,FixPrice=@FixPrice "
                strCommandText += " where TransferReturnItemID= @TransferReturnItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnItemID", DbType.String, obj.TransferReturnItemID)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, obj.TransferReturnID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalPriceCarat", DbType.Int64, obj.OriginalPriceCarat)
                DB.AddInParameter(DBComm, "@PriceCode", DbType.String, obj.PriceCode)
                DB.AddInParameter(DBComm, "@FixPrice", DbType.Int64, obj.FixPrice)

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

        Public Function GetForSaleIDByTransferReturnID(ByVal TransferReturnID As String) As System.Data.DataTable Implements ITransferDiamondReturnDA.GetForSaleIDByTransferReturnID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  S.ForSaleID,S.IsExit " & _
                "  FROM tbl_TransferReturnDiamond T INNER JOIN tbl_TransferReturnDiamondItem TI ON T.TransferReturnID=TI.TransferReturnID " & _
                " INNER JOIN tbl_ForSale S ON S.ForSaleID=TI.ForSaleID  " & _
                 " WHERE T.TransferReturnID=@TransferReturnID AND T.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, TransferReturnID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetTransferReturnItem(ByVal TransferReturnID As String) As System.Data.DataTable Implements ITransferDiamondReturnDA.GetTransferReturnItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ROW_NUMBER() OVER (ORDER BY TransferReturnItemID) AS No, TI.TransferReturnItemID,T.TransferReturnID , TI.ForSaleID, S.ItemCode,S.OriginalCode, S.Color,S.Shape,S.Clarity,S.SDGemsName, S.ItemTG, S.ItemTK, CAST(S.ItemTG AS Decimal(18,3)) AS Gram, C.GemsCategory, " & _
                " CAST(S.ItemTK AS INT) AS ItemK, CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY, " & _
                "TI.OriginalFixedPrice,TI.OriginalPriceCarat," & _
                "TI.PriceCode,TI.FixPrice " & _
                " FROM tbl_TransferReturnDiamond T INNER JOIN tbl_TransferReturnDiamondItem TI ON T.TransferReturnID=TI.TransferReturnID " & _
                " INNER JOIN tbl_ForSale S ON S.ForSaleID=TI.ForSaleID  " & _
                " LEFT JOIN tbl_GemsCategory C ON S.SDGemsCategoryID=C.GemsCategoryID" & _
                " WHERE T.TransferReturnID=@TransferReturnID AND T.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferReturnID", DbType.String, TransferReturnID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetBranchTransferReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferDiamondReturnDA.GetBranchTransferReturnReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.TransferReturnID,H.TransferReturnDate, HH.TransferReturnItemID, H.CurrentLocationID, LL.Location As BranchName, S.ForSaleID, S.ItemCode,H.StaffID, ST.Staff, H.Remark,   " & _
                "  S.SDGemsCategoryID as ItemCategoryID, C.GemsCategory as ItemCategory,S.SDGemsName as ItemName, S.ItemTG, S.ItemTK, S.Color,S.Shape,S.Clarity,S.SDYOrCOrG as YOrCOrG,S.LossQty as Qty,S.OriginalCode, " & _
                " CAST(S.ItemTK AS INT) AS ItemK, CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY,case when IsFixPrice=1 then S.OriginalFixedPrice else S.PriceCode  End  as PriceCode " & _
                " FROM tbl_TransferReturnDiamond H" & _
                " LEFT JOIN tbl_TransferReturnDiamondItem HH ON H.TransferReturnID=HH.TransferReturnID " & _
                " LEFT JOIN tbl_ForSale S ON S.ForSaleID=HH.ForSaleID " & _
                " LEFT JOIN tbl_Staff ST ON H.StaffID=ST.StaffID " & _
                " LEFT JOIN tbl_Location LL ON LL.LocationID=S.LocationID " & _
                " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=S.SDGemsCategoryID " & _
                " WHERE H.IsDelete=0 AND H.TransferReturnDate BETWEEN @FromDate AND @ToDate " & criStr & " Order by H.TransferReturnID "

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
    End Class

End Namespace
