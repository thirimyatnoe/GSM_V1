Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace PurchaseGems
    Public Class PurchaseGemsDA
        Implements IPurchaseGemsDA
#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As IPurchaseGemsDA = New PurchaseGemsDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IPurchaseGemsDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeletePurchaseGems(ByVal PurchaseGemsID As String) As Boolean Implements IPurchaseGemsDA.DeletePurchaseGems

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_PurchaseGems WHERE  PurchaseGemsID= @PurchaseGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, PurchaseGemsID)
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

        Public Function GetAllPurchaseGems() As System.Data.DataTable Implements IPurchaseGemsDA.GetAllPurchaseGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select PurchaseGemsID,convert(varchar(10),PG.PDate,105) as PurchaseDate,PG.StaffID as [@StaffID],S.Staff as [Staff_],PG.Customer as [Customer_],PG.Address as [Address_],PG.PaidAmount,PG.TotalAmount,PG.Remark as [Remark_]  From tbl_PurchaseGems PG left join tbl_Staff S on PG.StaffID=S.StaffID  Order by PG.PurchaseGemsID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseGems(ByVal PurchaseGemsID As String) As CommonInfo.PurchaseGemsInfo Implements IPurchaseGemsDA.GetPurchaseGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objPurchaseInfo As New PurchaseGemsInfo
            Try
                strCommandText = " SELECT * FROM tbl_PurchaseGems WHERE PurchaseGemsID= @PurchaseGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, PurchaseGemsID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objPurchaseInfo
                        .PurchaseGemsID = drResult("PurchaseGemsID")
                        .PDate = drResult("PDate")
                        .StaffID = drResult("StaffID")
                        .Customer = drResult("Customer")
                        .Address = drResult("Address")
                        .TotalAmount = drResult("TotalAmount")
                        .AddOrSub = drResult("AddOrSub")
                        .PaidAmount = drResult("PaidAmount")
                        .Remark = drResult("Remark")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objPurchaseInfo
        End Function

        Public Function InsertPurchaseGems(ByVal PurchaseGemsObj As CommonInfo.PurchaseGemsInfo) As Boolean Implements IPurchaseGemsDA.InsertPurchaseGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_PurchaseGems ( PurchaseGemsID,PDate,StaffID,Customer,Address,TotalAmount,AddOrSub,PaidAmount,Remark,LastModifiedLoginUserName,LastModifiedDate,LocationID)"
                strCommandText += " Values (@PurchaseGemsID,@PDate,@StaffID,@Customer,@Address,@TotalAmount,@AddOrSub,@PaidAmount,@Remark,@LastModifiedLoginUserName,@LastModifiedDate,@LocationID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, PurchaseGemsObj.PurchaseGemsID)
                DB.AddInParameter(DBComm, "@PDate", DbType.Date, PurchaseGemsObj.PDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, PurchaseGemsObj.StaffID)
                DB.AddInParameter(DBComm, "@Customer", DbType.String, PurchaseGemsObj.Customer)
                DB.AddInParameter(DBComm, "@Address", DbType.String, PurchaseGemsObj.Address)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, PurchaseGemsObj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, PurchaseGemsObj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, PurchaseGemsObj.PaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, PurchaseGemsObj.Remark)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
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

        Public Function UpdatePurchaseGems(ByVal PurchaseGemsObj As CommonInfo.PurchaseGemsInfo) As Boolean Implements IPurchaseGemsDA.UpdatePurchaseGems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_PurchaseGems set   PDate= @PDate , StaffID= @StaffID , Customer= @Customer , Address= @Address , TotalAmount= @TotalAmount , AddOrSub= @AddOrSub , PaidAmount= @PaidAmount , Remark= @Remark ,LocationID=@LocationID, LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= @LastModifiedDate "
                strCommandText += " where PurchaseGemsID= @PurchaseGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, PurchaseGemsObj.PurchaseGemsID)
                DB.AddInParameter(DBComm, "@PDate", DbType.Date, PurchaseGemsObj.PDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, PurchaseGemsObj.StaffID)
                DB.AddInParameter(DBComm, "@Customer", DbType.String, PurchaseGemsObj.Customer)
                DB.AddInParameter(DBComm, "@Address", DbType.String, PurchaseGemsObj.Address)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int32, PurchaseGemsObj.TotalAmount)
                DB.AddInParameter(DBComm, "@AddOrSub", DbType.Int32, PurchaseGemsObj.AddOrSub)
                DB.AddInParameter(DBComm, "@PaidAmount", DbType.Int32, PurchaseGemsObj.PaidAmount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, PurchaseGemsObj.Remark)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
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

        Public Function DeletePurchaseGemsItem(ByVal PurchaseGemsItemID As String) As Boolean Implements IPurchaseGemsDA.DeletePurchaseGemsItem

            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_PurchaseGemsItem WHERE  PurchaseGemsItemID= @PurchaseGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsItemID", DbType.String, PurchaseGemsItemID)
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

        Public Function GetPurchaseGemsItem(ByVal PurchaseGemsID As String) As System.Data.DataTable Implements IPurchaseGemsDA.GetPurchaseGemsItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT P.PurchaseGemsItemID,P.PurchaseGemsID,P.GemsCategoryID,P.GemsName,P.Clarity,P.SizeMM, " & _
                " CAST(P.GemsTK AS INT) AS GemsK," & _
                " CAST((P.GemsTK-CAST(P.GemsTK AS INT))*16 AS INT) AS GemsP," & _
                " CAST((((P.GemsTK-CAST(P.GemsTK AS INT))*16)-CAST((P.GemsTK-CAST(P.GemsTK AS INT))*16 AS INT))*8 AS INT) AS GemsY," & _
                " CAST(((((P.GemsTK-CAST(P.GemsTK AS INT))*16)-CAST((P.GemsTK-CAST(P.GemsTK AS INT))*16 AS INT))*8)-CAST((((P.GemsTK-CAST(P.GemsTK AS INT))*16)-CAST((P.GemsTK-CAST(P.GemsTK AS INT))*16 AS INT))*8 AS INT)AS DECIMAL(18,3)) AS GemsC, " & _
                " P.GemsTK, P.GemsTG, P.YOrCOrG, P.GemsTW, P.QTY, CASE P.FixType WHEN 1 THEN 'Fix' WHEN 2 THEN 'ByWeight' WHEN 3 THEN 'ByQty' END AS FixType,P.PurchaseRate,P.Amount " & _
                " FROM tbl_PurchaseGemsItem P " & _
                " WHERE P.PurchaseGemsID=@PurchaseGemsID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, PurchaseGemsID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertPurchaseGemsItem(ByVal ObjPurGemsItem As CommonInfo.PurchaseGemsItemInfo) As Boolean Implements IPurchaseGemsDA.InsertPurchaseGemsItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_PurchaseGemsItem ( PurchaseGemsItemID,PurchaseGemsID,GemsCategoryID,GemsName,Clarity,SizeMM,GemsTK,GemsTG,YOrCOrG,GemsTW,QTY,FixType,PurchaseRate,Amount)"
                strCommandText += " Values (@PurchaseGemsItemID,@PurchaseGemsID,@GemsCategoryID,@GemsName,@Clarity,@SizeMM,@GemsTK,@GemsTG,@YOrCOrG,@GemsTW,@QTY,@FixType,@PurchaseRate,@Amount)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsItemID", DbType.String, ObjPurGemsItem.PurchaseGemsItemID)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, ObjPurGemsItem.PurchaseGemsID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, ObjPurGemsItem.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, ObjPurGemsItem.GemsName)
                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, ObjPurGemsItem.GemsK)
                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, ObjPurGemsItem.GemsP)
                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, ObjPurGemsItem.GemsY)
                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, ObjPurGemsItem.GemsC)

                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjPurGemsItem.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjPurGemsItem.GemsTG)


                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjPurGemsItem.YOrCOrG)
                'DB.AddInParameter(DBComm, "@GemY", DbType.Int32, ObjPurGemsItem.GemY)
                'DB.AddInParameter(DBComm, "@GemBCG", DbType.Decimal, ObjPurGemsItem.GemBCG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, ObjPurGemsItem.GemTW)
                'DB.AddInParameter(DBComm, "@GemTK", DbType.Decimal, ObjPurGemsItem.GemTK)

                'DB.AddInParameter(DBComm, "@GemP", DbType.Decimal, ObjPurGemsItem.GemP)

                DB.AddInParameter(DBComm, "@QTY", DbType.Decimal, ObjPurGemsItem.QTY)
                DB.AddInParameter(DBComm, "@FixType", DbType.Int32, ObjPurGemsItem.FixType)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Decimal, ObjPurGemsItem.PurchaseRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, ObjPurGemsItem.Amount)

                DB.AddInParameter(DBComm, "@Clarity", DbType.String, ObjPurGemsItem.Clarity)
                DB.AddInParameter(DBComm, "@SizeMM", DbType.String, ObjPurGemsItem.SizeMM)

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

        Public Function UpdatePurchaseGemsItem(ByVal ObjPurGemsItem As CommonInfo.PurchaseGemsItemInfo) As Boolean Implements IPurchaseGemsDA.UpdatePurchaseGemsItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_PurchaseGemsItem set  PurchaseGemsID= @PurchaseGemsID , GemsCategoryID= @GemsCategoryID , GemsName= @GemsName ,Clarity=@Clarity,SizeMM=@SizeMM, GemsTK=@GemsTK , GemsTG=@GemsTG ,  YOrCOrG= @YOrCOrG , GemsTW= @GemsTW , QTY= @QTY , FixType= @FixType , PurchaseRate= @PurchaseRate , Amount= @Amount "
                strCommandText += " where PurchaseGemsItemID= @PurchaseGemsItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsItemID", DbType.String, ObjPurGemsItem.PurchaseGemsItemID)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, ObjPurGemsItem.PurchaseGemsID)
                DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, ObjPurGemsItem.GemsCategoryID)
                DB.AddInParameter(DBComm, "@GemsName", DbType.String, ObjPurGemsItem.GemsName)
                'DB.AddInParameter(DBComm, "@GemsK", DbType.Int32, ObjPurGemsItem.GemsK)
                'DB.AddInParameter(DBComm, "@GemsP", DbType.Int32, ObjPurGemsItem.GemsP)
                'DB.AddInParameter(DBComm, "@GemsY", DbType.Int32, ObjPurGemsItem.GemsY)
                'DB.AddInParameter(DBComm, "@GemsC", DbType.Decimal, ObjPurGemsItem.GemsC)
                DB.AddInParameter(DBComm, "@GemsTK", DbType.Decimal, ObjPurGemsItem.GemsTK)
                DB.AddInParameter(DBComm, "@GemsTG", DbType.Decimal, ObjPurGemsItem.GemsTG)

                DB.AddInParameter(DBComm, "@YOrCOrG", DbType.String, ObjPurGemsItem.YOrCOrG)
                'DB.AddInParameter(DBComm, "@GemY", DbType.Int32, ObjPurGemsItem.GemY)
                'DB.AddInParameter(DBComm, "@GemBCG", DbType.Decimal, ObjPurGemsItem.GemBCG)
                DB.AddInParameter(DBComm, "@GemsTW", DbType.Decimal, ObjPurGemsItem.GemTW)
                'DB.AddInParameter(DBComm, "@GemTK", DbType.Decimal, ObjPurGemsItem.GemTK)
                'DB.AddInParameter(DBComm, "@GemP", DbType.Int32, ObjPurGemsItem.GemP)
                DB.AddInParameter(DBComm, "@QTY", DbType.Decimal, ObjPurGemsItem.QTY)
                DB.AddInParameter(DBComm, "@FixType", DbType.Int32, ObjPurGemsItem.FixType)
                DB.AddInParameter(DBComm, "@PurchaseRate", DbType.Decimal, ObjPurGemsItem.PurchaseRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int32, ObjPurGemsItem.Amount)
                DB.AddInParameter(DBComm, "@Clarity", DbType.String, ObjPurGemsItem.Clarity)
                DB.AddInParameter(DBComm, "@SizeMM", DbType.String, ObjPurGemsItem.SizeMM)
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
        Public Function GetPurchaseGemsReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IPurchaseGemsDA.GetPurchaseGemsReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.PurchaseGemsID, H.PDate, S.Staff,I.Clarity,I.SizeMM, I.GemsCategoryID, GemsCategory, GemsName, " & _
                " GemsTK, GemsTG, YOrCOrG, GemsTW, QTY, CASE FixType WHEN 1 THEN 'Fix' WHEN 2 THEN 'ByWeight' WHEN 3 THEN 'ByQty' END AS FixType,PurchaseRate,Amount, H.TotalAmount, H.PaidAmount,H.AddOrSub, H.Remark, H.Customer  " & _
                " FROM tbl_PurchaseGems H INNER JOIN tbl_PurchaseGemsItem I ON H.PurchaseGemsID=I.PurchaseGemsID " & _
                 " LEFT JOIN tbl_GemsCategory C ON I.GemsCategoryID=C.GemsCategoryID " & _
                " LEFT JOIN tbl_Staff S ON H.StaffID=S.StaffID " & _
                " WHERE PDate BETWEEN @FromDate AND @ToDate " & criStr & ""
                ''" CAST(GemsTK AS INT) AS GemsK," & _
                ''" CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP," & _
                ''" CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
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

        Public Function GetAllPurchaseGem() As System.Data.DataTable Implements IPurchaseGemsDA.GetAllPurchaseGem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select PurchaseGemsID as [PurchaseGemsID] ,convert(varchar(10),PG.PDate,105) as PurchaseDate,PG.StaffID as [@StaffID],S.Staff as [Stff_],PG.Customer as [Customer_],PG.PaidAmount,PG.TotalAmount  From tbl_PurchaseGems PG left join tbl_Staff S on PG.StaffID=S.StaffID  Order by PG.PurchaseGemsID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetPurchaseGemsPrint(ByVal PurchaseGemsID As String) As System.Data.DataTable Implements IPurchaseGemsDA.GetPurchaseGemsPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.PurchaseGemsID, PDate, S.Staff as Staff, Customer, H.Address,H.TotalAmount,H.AddOrSub, H.PaidAmount,H.LastModifiedLoginUserName, " & _
                " L.Location, I.GemsCategory, GI.Qty, GI.YOrCOrG, GI.GemsName, GI.Clarity,GI.SizeMM, GI.GemsTK, GI.GemsTG, GI.GemsTW, GI.FixType, GI.PurchaseRate, GI.Amount,  " & _
                " CAST(GI.GemsTK AS INT) AS GemsK, " & _
                " CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
                " CAST((((GI.GemsTK-CAST(GI.GemsTK AS INT))*16)-CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY, " & _
                " U.UserName  " & _
                " FROM tbl_PurchaseGems H " & _
                " LEFT JOIN tbl_PurchaseGemsItem GI ON GI.PurchaseGemsID=H.PurchaseGemsID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                " LEFT JOIN tbl_GemsCategory I ON I.GemsCategoryID=GI.GemsCategoryID " & _
                " LEFT JOIN tb_GE_SystemUser U ON H.UserID=U.UserID " & _
                " WHERE H.PurchaseGemsID= @PurchaseGemsID"
                '" LEFT JOIN tbl_Counter C ON C.CounterID=H.CounterID " & _
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, PurchaseGemsID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertPurchaseGemsUserID(ByVal PurchaseGemsID As String, ByVal UserID As String) As Boolean Implements IPurchaseGemsDA.InsertPurchaseGemsUserID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_PurchaseGems set   UserID= @UserID "
                strCommandText += " where PurchaseGemsID= @PurchaseGemsID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PurchaseGemsID", DbType.String, PurchaseGemsID)
                DB.AddInParameter(DBComm, "@UserID", DbType.String, UserID)
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
    End Class
End Namespace
