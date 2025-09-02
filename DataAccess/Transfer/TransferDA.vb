Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Transfer
    Public Class TransferDA
        Implements ITransferDA




#Region "Private Transfer"

        Private DB As Database
        Private Shared ReadOnly _instance As ITransferDA = New TransferDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ITransferDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteTransfer(ByVal TransferID As String) As Boolean Implements ITransferDA.DeleteTransfer
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_Transfer  set IsDelete=1 WHERE  TransferID= @TransferID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, TransferID)
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

        Public Function DeleteTransferItem(ByVal TransferItemID As String) As Boolean Implements ITransferDA.DeleteTransferItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_TransferItem WHERE  TransferItemID= @TransferItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferItemID", DbType.String, TransferItemID)
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

        Public Function GetAllTransfer() As System.Data.DataTable Implements ITransferDA.GetAllTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select T.TransferID as [@TransferID],convert(varchar(10),T.TransferDate,105) as TransferDate,L.Location,S.Staff as [Name_],F.ItemCode, F.ItemName AS [ItemName_],F.ForSaleID AS [@ForSaleID],F.GoldQualityID as [@GoldQualityID],  IsNull(F.Length,'-') as Length,IsNull(F.Width,'-') as Width , F.TotalTG ," & _
                " T.Remark as [Remark_] " & _
                " from  tbl_Transfer T Inner Join tbl_TransferItem TI On T.TransferID=TI.TransferID " & _
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
        Public Function GetHeaderTransferList() As System.Data.DataTable Implements ITransferDA.GetHeaderTransferList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select T.TransferID as [TransferID],convert(varchar(10),T.TransferDate,105) as TransferDate, B.Location as  [BranchName_],S.Staff as [Name_]," & _
                " T.Remark as [Remark_],T.IsSync,T.IsConfirm" & _
                " from  tbl_Transfer T " & _
                " Left Join tbl_Location B ON T.LocationID=B.LocationID " & _
                " Left Join tbl_Staff S On T.StaffID=S.StaffID Where T.IsDelete=0 ORDER By T.TransferDate desc"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetTransferByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ITransferDA.GetTransferByForSaleID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from tbl_TransferItem Where ForSaleID=@ForsaleID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForsaleID", DbType.String, ForSaleID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetTransfer(ByVal TransferID As String) As CommonInfo.TransferInfo Implements ITransferDA.GetTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New TransferInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_Transfer WHERE TransferID= @TransferID AND IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, TransferID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .TransferID = drResult("TransferID")
                        .TransferDate = drResult("TransferDate")
                        .LocationID = drResult("LocationID")
                        .StaffID = drResult("StaffID")
                        .Remark = drResult("Remark")
                        .IsConfirm = drResult("IsConfirm")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function InsertTransfer(ByVal obj As CommonInfo.TransferInfo) As Boolean Implements ITransferDA.InsertTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_Transfer ( TransferID,TransferDate,LocationID,StaffID,Remark,LastModifiedLoginUserName,LastModifiedDate,IsDelete,IsSync,IsConfirm,CurrentLocationID)"
                strCommandText += " Values (@TransferID,@TransferDate,@LocationID,@StaffID,@Remark,@LastModifiedLoginUserName,GetDate(),0,0,@IsConfirm,@CurrentLocationID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, obj.TransferID)
                DB.AddInParameter(DBComm, "@TransferDate", DbType.DateTime, obj.TransferDate)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, obj.LocationID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsConfirm", DbType.Boolean, obj.IsConfirm)
                DB.AddInParameter(DBComm, "@CurrentLocationID", DbType.String, obj.CurrentLocationID)


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

        Public Function InsertTransferItem(ByVal obj As CommonInfo.TransferItemInfo) As Boolean Implements ITransferDA.InsertTransferItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_TransferItem ( TransferItemID,TransferID,ForSaleID,OriginalFixedPrice,OriginalPriceGram,OriginalPriceTK,OriginalGemsPrice,PriceCode,FixPrice,isReturn)"
                strCommandText += " Values (@TransferItemID,@TransferID,@ForSaleID,@OriginalFixedPrice,@OriginalPriceGram,@OriginalPriceTK,@OriginalGemsPrice,@PriceCode,@FixPrice,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferItemID", DbType.String, obj.TransferItemID)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, obj.TransferID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, obj.OriginalGemsPrice)
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

        Public Function UpdateTransfer(ByVal obj As CommonInfo.TransferInfo) As Boolean Implements ITransferDA.UpdateTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_Transfer set  TransferID= @TransferID , TransferDate= @TransferDate , LocationID= @LocationID , StaffID= @StaffID , Remark= @Remark,LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= GetDate(),IsConfirm=@IsConfirm,CurrentLocationID=@CurrentLocationID "
                strCommandText += " where TransferID= @TransferID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, obj.TransferID)
                DB.AddInParameter(DBComm, "@TransferDate", DbType.DateTime, obj.TransferDate)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, obj.LocationID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, obj.StaffID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, obj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@IsConfirm", DbType.Boolean, obj.IsConfirm)
                DB.AddInParameter(DBComm, "@CurrentLocationID", DbType.String, obj.CurrentLocationID)

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

        Public Function UpdateTransferItem(ByVal obj As CommonInfo.TransferItemInfo) As Boolean Implements ITransferDA.UpdateTransferItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_TransferItem set  TransferItemID= @TransferItemID , TransferID= @TransferID , ForSaleID= @ForSaleID,OriginalFixedPrice=@OriginalFixedPrice,OriginalPriceGram=@OriginalPriceGram,OriginalPriceTK=@OriginalPriceTK,OriginalGemsPrice=@OriginalGemsPrice,PriceCode=@OriginalGemsPrice,FixPrice=@FixPrice "
                strCommandText += " where TransferItemID= @TransferItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferItemID", DbType.String, obj.TransferItemID)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, obj.TransferID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, obj.OriginalPriceGram)
                DB.AddInParameter(DBComm, "@OriginalPriceTK", DbType.Int64, obj.OriginalPriceTK)
                DB.AddInParameter(DBComm, "@OriginalGemsPrice", DbType.Int64, obj.OriginalGemsPrice)
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
        Public Function UpdateTransferItemReturn(ByVal dr As System.Data.DataRow) As Boolean Implements ITransferDA.UpdateTransferItemReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_TransferItem set isReturn=1 Where ForSaleID=@ForSaleID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, dr.Item("ForSaleID"))


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

        Public Function GetForSaleIDByTransferID(ByVal TransferID As String) As System.Data.DataTable Implements ITransferDA.GetForSaleIDByTransferID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  S.ForSaleID,S.IsExit " & _
                "  FROM tbl_Transfer T INNER JOIN tbl_TransferItem TI ON T.TransferID=TI.TransferID " & _
                " INNER JOIN tbl_ForSale S ON S.ForSaleID=TI.ForSaleID  " & _
                 " WHERE T.TransferID=@TransferID AND T.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, TransferID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetTransferItem(ByVal TransferID As String) As System.Data.DataTable Implements ITransferDA.GetTransferItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ROW_NUMBER() OVER (ORDER BY TransferItemID) AS No, TI.TransferItemID,T.TransferID , TI.ForSaleID, S.ItemCode,S.OriginalCode, N.ItemName, S.Length,S.Width, S.ItemTG, S.ItemTK, CAST(S.ItemTG AS Decimal(18,3)) AS Gram, G.GoldQuality, C.ItemCategory, " & _
                " CAST(S.ItemTK AS INT) AS ItemK, CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY, " & _
                "TI.OriginalFixedPrice,TI.OriginalPriceGram,TI.OriginalPriceTK,TI.OriginalGemsPrice," & _
                "TI.PriceCode,TI.FixPrice,TI.IsReturn " & _
                " FROM tbl_Transfer T INNER JOIN tbl_TransferItem TI ON T.TransferID=TI.TransferID " & _
                " INNER JOIN tbl_ForSale S ON S.ForSaleID=TI.ForSaleID  " & _
                " LEFT JOIN tbl_ItemName N ON S.ItemNameID=N.ItemNameID " & _
                " LEFT JOIN tbl_GoldQuality G ON S.GoldQualityID=G.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory C ON S.ItemCategoryID=C.ItemCategoryID" & _
                " WHERE T.TransferID=@TransferID AND T.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, TransferID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferDA.GetBranchTransferReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.TransferID,H.TransferDate, HH.TransferItemID, H.LocationID, LL.Location As BranchName, S.ForSaleID, S.ItemCode, N.ItemName, S.Length, H.StaffID, ST.Staff, H.Remark,   " & _
                " S.GoldQualityID, G.GoldQuality, S.ItemCategoryID, C.ItemCategory, S.ItemTG,S.GoldTG, S.ItemTK, S.Width, " & _
                " CAST(S.ItemTK AS INT) AS ItemK, CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY,case when IsFixPrice=1 then S.OriginalFixedPrice else S.PriceCode  End  as PriceCode,S.OriginalCode " & _
                " FROM tbl_Transfer H" & _
                " LEFT JOIN tbl_TransferItem HH ON H.TransferID=HH.TransferID " & _
                " LEFT JOIN tbl_ForSale S ON S.ForSaleID=HH.ForSaleID " & _
                " LEFT JOIN tbl_Staff ST ON H.StaffID=ST.StaffID " & _
                " LEFT JOIN tbl_Location LL ON LL.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_GoldQuality G ON G.GoldQualityID=S.GoldQualityID " & _
                " LEFT JOIN tbl_ItemName N ON N.ItemNameID=S.ItemNameID " & _
                " LEFT JOIN tbl_ItemCategory C ON C.ItemCategoryID=S.ItemCategoryID " & _
                " WHERE H.IsDelete=0 AND H.TransferDate BETWEEN @FromDate and @ToDate " & criStr & " Order by H.TransferID "

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
