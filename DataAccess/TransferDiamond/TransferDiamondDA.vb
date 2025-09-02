Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace TransferDiamond
    Public Class TransferDiamondDA
        Implements ITransferDiamondDA




#Region "Private Transfer"

        Private DB As Database
        Private Shared ReadOnly _instance As ITransferDiamondDA = New TransferDiamondDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ITransferDiamondDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteTransfer(ByVal TransferID As String) As Boolean Implements ITransferDiamondDA.DeleteTransfer
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_TransferLooseDiamond  set IsDelete=1 WHERE  TransferID= @TransferID"
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

        Public Function DeleteTransferItem(ByVal TransferItemID As String) As Boolean Implements ITransferDiamondDA.DeleteTransferItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_TransferLooseDiamondItem WHERE  TransferItemID= @TransferItemID"
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

        Public Function GetAllTransfer() As System.Data.DataTable Implements ITransferDiamondDA.GetAllTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select T.TransferID as [@TransferID],convert(varchar(10),T.TransferDate,105) as TransferDate,L.Location,S.Staff as [Name_],F.ItemCode, F.SDGemsName AS [ItemName_],F.ForSaleID AS [@ForSaleID], IsNull(F.Color,'-') as Color,IsNull(F.Shape,'-') as Shape,IsNull(F.Clarity,'-') as Clarity , F.TotalTG ," & _
                " T.Remark as [Remark_] " & _
                " from  tbl_TransferLooseDiamond T Inner Join tbl_TransferLooseDiamondItem TI On T.TransferID=TI.TransferID " & _
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
        Public Function GetHeaderTransferList() As System.Data.DataTable Implements ITransferDiamondDA.GetHeaderTransferList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select T.TransferID as [TransferID],convert(varchar(10),T.TransferDate,105) as TransferDate, B.Location as  [BranchName_],S.Staff as [Name_]," & _
                " T.Remark as [Remark_],T.IsSync,T.IsConfirm" & _
                " from  tbl_TransferLooseDiamond T " & _
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
        Public Function GetTransferByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ITransferDiamondDA.GetTransferByForSaleID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from tbl_TransferLooseDiamondItem Where ForSaleID=@ForsaleID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForsaleID", DbType.String, ForSaleID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetTransfer(ByVal TransferID As String) As CommonInfo.TransferDiamondInfo Implements ITransferDiamondDA.GetTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New TransferDiamondInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_TransferLooseDiamond WHERE TransferID= @TransferID AND IsDelete=0"
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

        Public Function InsertTransfer(ByVal obj As CommonInfo.TransferDiamondInfo) As Boolean Implements ITransferDiamondDA.InsertTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_TransferLooseDiamond ( TransferID,TransferDate,LocationID,StaffID,Remark,LastModifiedLoginUserName,LastModifiedDate,IsDelete,IsSync,IsConfirm,CurrentLocationID)"
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

        Public Function InsertTransferItem(ByVal obj As CommonInfo.TransferDiamondItemInfo) As Boolean Implements ITransferDiamondDA.InsertTransferItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_TransferLooseDiamondItem ( TransferItemID,TransferID,ForSaleID,OriginalFixedPrice,OriginalPriceCarat,PriceCode,FixPrice,isReturn)"
                strCommandText += " Values (@TransferItemID,@TransferID,@ForSaleID,@OriginalFixedPrice,@OriginalPriceCarat,@PriceCode,@FixPrice,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferItemID", DbType.String, obj.TransferItemID)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, obj.TransferID)
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

        Public Function UpdateTransfer(ByVal obj As CommonInfo.TransferDiamondInfo) As Boolean Implements ITransferDiamondDA.UpdateTransfer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_TransferLooseDiamond set  TransferID= @TransferID , TransferDate= @TransferDate , LocationID= @LocationID , StaffID= @StaffID , Remark= @Remark,LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= GetDate(),IsConfirm=@IsConfirm,CurrentLocationID=@CurrentLocationID "
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

        Public Function UpdateTransferItem(ByVal obj As CommonInfo.TransferDiamondItemInfo) As Boolean Implements ITransferDiamondDA.UpdateTransferItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_TransferLooseDiamondItem set  TransferItemID= @TransferItemID , TransferID= @TransferID , ForSaleID= @ForSaleID,OriginalFixedPrice=@OriginalFixedPrice,OriginalPriceCarat=@OriginalPriceCarat,PriceCode=@OriginalGemsPrice,FixPrice=@FixPrice "
                strCommandText += " where TransferItemID= @TransferItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TransferItemID", DbType.String, obj.TransferItemID)
                DB.AddInParameter(DBComm, "@TransferID", DbType.String, obj.TransferID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, obj.ForSaleID)
                DB.AddInParameter(DBComm, "@OriginalFixedPrice", DbType.Int64, obj.OriginalFixedPrice)
                DB.AddInParameter(DBComm, "@OriginalPriceGram", DbType.Int64, obj.OriginalPriceCarat)
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
        Public Function UpdateTransferItemReturn(ByVal dr As System.Data.DataRow) As Boolean Implements ITransferDiamondDA.UpdateTransferItemReturn
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_TransferLooseDiamondItem set isReturn=1 Where ForSaleID=@ForSaleID "
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

        Public Function GetForSaleIDByTransferID(ByVal TransferID As String) As System.Data.DataTable Implements ITransferDiamondDA.GetForSaleIDByTransferID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT  S.ForSaleID,S.IsExit " & _
                "  FROM tbl_TransferLooseDiamond T INNER JOIN tbl_TransferLooseDiamondItem TI ON T.TransferID=TI.TransferID " & _
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

        Public Function GetTransferItem(ByVal TransferID As String) As System.Data.DataTable Implements ITransferDiamondDA.GetTransferItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ROW_NUMBER() OVER (ORDER BY TransferItemID) AS No, TI.TransferItemID,T.TransferID , TI.ForSaleID, S.ItemCode,S.OriginalCode, S.Color,S.Shape,S.Clarity, S.ItemTG, S.ItemTK, CAST(S.ItemTG AS Decimal(18,3)) AS Gram, C.GemsCategory,S.SDGemsName as GemsName, " & _
                " CAST(S.ItemTK AS INT) AS ItemK, CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY, " & _
                "TI.OriginalFixedPrice,TI.OriginalPriceCarat," & _
                "TI.PriceCode,TI.FixPrice,TI.IsReturn " & _
                " FROM tbl_TransferLooseDiamond T INNER JOIN tbl_TransferLooseDiamondItem TI ON T.TransferID=TI.TransferID " & _
                " INNER JOIN tbl_ForSale S ON S.ForSaleID=TI.ForSaleID  " & _
                " LEFT JOIN tbl_GemsCategory C ON S.SDGemsCategoryID=C.GemsCategoryID" & _
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

        Public Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferDiamondDA.GetBranchTransferReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.TransferID,H.TransferDate, HH.TransferItemID, H.LocationID, LL.Location As BranchName, S.ForSaleID, S.ItemCode, S.Color,S.Shape,S.Clarity, H.StaffID, ST.Staff, H.Remark,   " & _
                " S.SDGemsCategoryID, S.ItemTG, S.ItemTK,C.GemsCategory as ItemCategory,S.SDYOrCOrG as YOrCOrG,S.SDGemsName as ItemName ,S.LossQty as Qty, " & _
                " CAST(S.ItemTK AS INT) AS ItemK, CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT) AS ItemP, " & _
                " CAST((((S.ItemTK-CAST(S.ItemTK AS INT))*16)-CAST((S.ItemTK-CAST(S.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,1)) AS ItemY,case when IsFixPrice=1 then S.OriginalFixedPrice else S.PriceCode  End  as PriceCode,S.OriginalCode " & _
                " FROM tbl_TransferLooseDiamond H" & _
                " LEFT JOIN tbl_TransferLooseDiamondItem HH ON H.TransferID=HH.TransferID " & _
                " LEFT JOIN tbl_ForSale S ON S.ForSaleID=HH.ForSaleID " & _
                " LEFT JOIN tbl_Staff ST ON H.StaffID=ST.StaffID " & _
                " LEFT JOIN tbl_Location LL ON LL.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_GemsCategory C ON C.GemsCategoryID=S.SDGemsCategoryID " & _
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
