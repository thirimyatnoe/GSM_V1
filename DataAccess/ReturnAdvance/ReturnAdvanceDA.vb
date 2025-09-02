Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace ReturnAdvance
    Public Class ReturnAdvanceDA
        Implements IReturnAdvanceDA
#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As IReturnAdvanceDA = New ReturnAdvanceDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IReturnAdvanceDA
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteReturnAdvance(ByVal ReturnAdvanceID As String) As Boolean Implements IReturnAdvanceDA.DeleteReturnAdvance
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_ReturnAdvance Set IsDelete=1 WHERE ReturnAdvanceID = @ReturnAdvanceID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, ReturnAdvanceID)
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

        Public Function DeleteReturnAdvanceItem(ByVal ReturnAdvanceItemID As String) As Boolean Implements IReturnAdvanceDA.DeleteReturnAdvanceItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_ReturnAdvanceItem WHERE ReturnAdvanceItemID= @ReturnAdvanceItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnAdvanceItemID", DbType.String, ReturnAdvanceItemID)
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

        Public Function GetReturnAdvance(ByVal ReturnAdvanceID As String) As CommonInfo.ReturnAdvanceInfo Implements IReturnAdvanceDA.GetReturnAdvance
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.ReturnAdvanceInfo
            Try
                strCommandText = " Select ReturnAdvanceID as [@ReturnAdvanceID],ReturnAdvanceDate,TotalTG,StaffID as [@StaffID],CustomerID,TotalAmount,"
                strCommandText += "  (TotalAmount-Discount) As NetAmount, Remark,Discount from tbl_ReturnAdvance  WHERE ReturnAdvanceID= @ReturnAdvanceID and IsDelete=0 Order by ReturnAdvanceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, ReturnAdvanceID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ReturnAdvanceID = drResult("@ReturnAdvanceID")
                        .ReturnAdvanceDate = drResult("ReturnAdvanceDate")
                        .TotalTG = drResult("TotalTG")
                        .StaffID = drResult("@StaffID")
                        .CustomerID = drResult("CustomerID")
                        .TotalAmount = drResult("TotalAmount")
                        .Remark = drResult("Remark")
                        .Discount = drResult("Discount")
                        .NetAmount = drResult("NetAmount")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function GetReturnAdvanceItem(ByVal ReturnAdvanceID As String) As System.Data.DataTable Implements IReturnAdvanceDA.GetReturnAdvanceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT ReturnAdvanceItemID,ReturnAdvanceID,ItemTG,SaleRate,Amount,Qty,Remark,IsUsed"
                strCommandText += " From tbl_ReturnAdvanceItem Where ReturnAdvanceID = '" & ReturnAdvanceID & "' Order By ReturnAdvanceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertReturnAdvance(ByVal ReturnAdvanceObj As CommonInfo.ReturnAdvanceInfo) As Boolean Implements IReturnAdvanceDA.InsertReturnAdvance
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ReturnAdvance ( ReturnAdvanceID,ReturnAdvanceDate,StaffID,CustomerID,TotalAmount,TotalTG,Discount,NetAmount,Remark,LastModifiedLoginUserName,LastModifiedDate,LocationID,IsDelete,IsSync)"
                strCommandText += " Values (@ReturnAdvanceID,@ReturnAdvanceDate,@StaffID,@CustomerID,@TotalAmount,@TotalTG,@Discount,@NetAmount,@Remark,@LastModifiedLoginUserName,@LastModifiedDate,@LocationID,0,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, ReturnAdvanceObj.ReturnAdvanceID)
                DB.AddInParameter(DBComm, "@ReturnAdvanceDate", DbType.DateTime, ReturnAdvanceObj.ReturnAdvanceDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, ReturnAdvanceObj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, ReturnAdvanceObj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, ReturnAdvanceObj.TotalAmount)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, ReturnAdvanceObj.TotalTG)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, ReturnAdvanceObj.Remark)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, ReturnAdvanceObj.Discount)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int32, ReturnAdvanceObj.NetAmount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)

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

        Public Function InsertReturnAdvanceItem(ByVal ObjReturnAdvanceItem As CommonInfo.ReturnAdvanceItemInfo) As Boolean Implements IReturnAdvanceDA.InsertReturnAdvanceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ReturnAdvanceItem ( ReturnAdvanceItemID,ReturnAdvanceID,ItemTG,Qty,SaleRate,Amount,Remark,IsUsed)"
                strCommandText += " Values (@ReturnAdvanceItemID,@ReturnAdvanceID,@ItemTG,@Qty,@SaleRate,@Amount,@Remark,@IsUsed)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnAdvanceItemID", DbType.String, ObjReturnAdvanceItem.ReturnAdvanceItemID)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, ObjReturnAdvanceItem.ReturnAdvanceID)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, ObjReturnAdvanceItem.ItemTG)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, ObjReturnAdvanceItem.Qty)
                DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, ObjReturnAdvanceItem.SaleRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, ObjReturnAdvanceItem.Amount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, ObjReturnAdvanceItem.Remark)
                DB.AddInParameter(DBComm, "@IsUsed", DbType.Boolean, ObjReturnAdvanceItem.IsUsed)
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

        Public Function UpdateReturnAdvance(ByVal ReturnAdvanceObj As CommonInfo.ReturnAdvanceInfo) As Boolean Implements IReturnAdvanceDA.UpdateReturnAdvance
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ReturnAdvance set  ReturnAdvanceDate= @ReturnAdvanceDate , StaffID= @StaffID , CustomerID= @CustomerID , TotalAmount= @TotalAmount , TotalTG= @TotalTG ,Discount=@Discount, Remark= @Remark ,LocationID=@LocationID, LastModifiedLoginUserName= @LastModifiedLoginUserName ,NetAmount=@NetAmount, LastModifiedDate= @LastModifiedDate,IsSync=0 "
                strCommandText += " where ReturnAdvanceID= @ReturnAdvanceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, ReturnAdvanceObj.ReturnAdvanceID)
                DB.AddInParameter(DBComm, "@ReturnAdvanceDate", DbType.DateTime, ReturnAdvanceObj.ReturnAdvanceDate)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, ReturnAdvanceObj.StaffID)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, ReturnAdvanceObj.CustomerID)
                DB.AddInParameter(DBComm, "@TotalAmount", DbType.Int64, ReturnAdvanceObj.TotalAmount)
                DB.AddInParameter(DBComm, "@TotalTG", DbType.Decimal, ReturnAdvanceObj.TotalTG)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, ReturnAdvanceObj.Remark)
                DB.AddInParameter(DBComm, "@Discount", DbType.Int32, ReturnAdvanceObj.Discount)
                DB.AddInParameter(DBComm, "@NetAmount", DbType.Int32, ReturnAdvanceObj.NetAmount)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LastModifiedDate", DbType.DateTime, Now)

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

        Public Function UpdateReturnAdvanceItem(ByVal Obj As CommonInfo.ReturnAdvanceItemInfo) As Boolean Implements IReturnAdvanceDA.UpdateReturnAdvanceItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ReturnAdvanceItem set ReturnAdvanceItemID= @ReturnAdvanceItemID , ReturnAdvanceID= @ReturnAdvanceID  , ItemTG= @ItemTG, Qty= @Qty, SaleRate=@SaleRate,Amount=@Amount,Remark=@Remark, IsUsed=@IsUsed "
                strCommandText += " where ReturnAdvanceItemID= @ReturnAdvanceItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ReturnAdvanceItemID", DbType.String, Obj.ReturnAdvanceItemID)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, Obj.ReturnAdvanceID)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, Obj.ItemTG)
                DB.AddInParameter(DBComm, "@Qty", DbType.Int32, Obj.Qty)
                DB.AddInParameter(DBComm, "@SaleRate", DbType.Int64, Obj.SaleRate)
                DB.AddInParameter(DBComm, "@Amount", DbType.Int64, Obj.Amount)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@IsUsed", DbType.Boolean, Obj.IsUsed)

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
        Public Function UpdateReturnAdvanceItemIsUsed(ByVal objReturnAdvanceItem As CommonInfo.ReturnAdvanceItemInfo) As Boolean Implements IReturnAdvanceDA.UpdateReturnAdvanceItemIsUsed
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ReturnAdvanceItem Set IsUsed=@IsUsed "
                strCommandText += " where ReturnAdvanceID= @ReturnAdvanceID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, objReturnAdvanceItem.ReturnAdvanceID)
                DB.AddInParameter(DBComm, "@IsUsed", DbType.String, objReturnAdvanceItem.IsUsed)

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


        Public Function GetAllReturnAdvance() As System.Data.DataTable Implements IReturnAdvanceDA.GetAllReturnAdvance
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "Select R.ReturnAdvanceID as VoucherNo ,convert(varchar(10),R.ReturnAdvanceDate,105) As ReturnAdvanceDate,R.StaffID As [@StaffID],ST.Staff as [Staff_],R.CustomerID as [@CustomerID], C.CustomerName As [Customer_],R.TotalAmount, R.Discount,R.ReturnAdvanceDate as [@ReturnAdvanceDate] "
                strCommandText += " From tbl_ReturnAdvance R Left Join tbl_Staff ST On R.StaffID=ST.StaffID LEFT JOIN tbl_Customer C On C.CustomerID=R.CustomerID where R.IsDelete =0 Order by [@ReturnAdvanceDate] desc,ReturnAdvanceID desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllReturnAdvanceInCashReceipt() As System.Data.DataTable Implements IReturnAdvanceDA.GetAllReturnAdvanceInCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "Select distinct R.ReturnAdvanceID as VoucherNo ,convert(varchar(10),R.ReturnAdvanceDate,105) As ReturnAdvanceDate,R.StaffID As [@StaffID],ST.Staff as [Staff_],R.CustomerID as [@CustomerID], C.CustomerName As [Customer_],R.TotalAmount, R.Discount,R.ReturnAdvanceDate as [@ReturnAdvanceDate] "
                strCommandText += " From tbl_ReturnAdvance R  Left Join tbl_ReturnAdvanceItem I On R.ReturnAdvanceID=I.ReturnAdvanceID  Left Join tbl_Staff ST On R.StaffID=ST.StaffID LEFT JOIN tbl_Customer C On C.CustomerID=R.CustomerID where R.IsDelete =0 And I.IsUsed=0 Order by [@ReturnAdvanceDate] desc,R.ReturnAdvanceID desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        'Public Function GetAllReturnAdvanceForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IReturnAdvanceDA.GetAllReturnAdvanceForRpt
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Dim dtResult As DataTable

        '    Try

        '        strCommandText = "Select S.ReturnAdvanceID,S.ReturnAdvanceDate,S.StaffID As [@StaffID],ST.Staff,S.CustomerID, C.CustomerName As Customer, C.CustomerAddress As Address, S.TotalAmount,S.Remark,S.AddOrSub,S.PaidAmount,L.Location "
        '        strCommandText += " From tbl_ReturnAdvance S Left Join tbl_Staff ST On S.StaffID=ST.StaffID LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID Left Join tbl_Location L on S.LocationID = L.LocationID "
        '        strCommandText += " Where 1=1 and S.IsDelete=0 And S.ReturnAdvanceDate between '" & FromDate & " 00:00:00' and '" & ToDate & " 23:59:59' " & cristr & " Order by S.ReturnAdvanceDate"

        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
        '        DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
        '        Return dtResult
        '    Catch ex As Exception
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
        '        Return New DataTable
        '    End Try
        'End Function

        Public Function GetReturnAdvanceItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal optType As String = "") As System.Data.DataTable Implements IReturnAdvanceDA.GetReturnAdvanceItemForRpt
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                'strCommandText = "Select S.ReturnAdvanceID,S.ReturnAdvanceDate,S.CustomerID, Cu.CustomerName As Customer, Cu.CustomerAddress As Address, S.StaffID, F.Staff, S.TotalAmount,G.Remark,S.Discount,G.ItemTG,G.Qty,G.Amount, " & _
                '                "(S.TotalAmount-S.Discount) As NetAmount ,S.ReturnAdvanceDate AS [@ReturnAdvanceDate],G.SaleRate From tbl_ReturnAdvance S Inner Join tbl_ReturnAdvanceItem G on G.ReturnAdvanceID=S.ReturnAdvanceID LEFT JOIN tbl_Customer Cu ON Cu.CustomerID=S.CustomerID  Inner Join tbl_Staff F on F.StaffID=S.StaffID Where 1=1 AND S.IsDelete=0 AND S.ReturnAdvanceDate " & _
                '                " BETWEEN '" & FromDate & " 00:00:00' AND '" & ToDate & " 23:59:59' " & cristr & " Order by [@ReturnAdvanceDate] DESC, S.ReturnAdvanceID, ReturnAdvanceItemID ASC  "
                Select Case optType
                    Case "All"
                        strCommandText = "select * from (Select R.CashReceiptID,R.VoucherNo,S.ReturnAdvanceID,G.ReturnAdvanceItemID,S.ReturnAdvanceDate,S.CustomerID, Cu.CustomerName As Customer, Cu.CustomerAddress As Address, S.StaffID," & _
                                       " F.Staff, S.TotalAmount,G.Remark,S.Discount,G.ItemTG,G.Qty,G.Amount, (S.TotalAmount-S.Discount) As NetAmount ,S.ReturnAdvanceDate AS [@ReturnAdvanceDate],G.SaleRate From tbl_ReturnAdvance S " & _
                                       " left join tbl_cashReceipt R On S.ReturnAdvanceID=R.ReturnAdvanceID " & _
                                       " Inner Join tbl_ReturnAdvanceItem G on G.ReturnAdvanceID=S.ReturnAdvanceID " & _
                                       " LEFT JOIN tbl_Customer Cu ON Cu.CustomerID=S.CustomerID  " & _
                                       " Inner Join tbl_Staff F on F.StaffID=S.StaffID Where 1=1 AND S.IsDelete=0 AND S.ReturnAdvanceDate  BETWEEN @FromDate And @ToDate  And R.isdelete=0  " & cristr & _
                                       " UNION ALL " & _
                                       " Select '' as CashReceiptID,'' As VoucherNo,S.ReturnAdvanceID,G.ReturnAdvanceItemID,S.ReturnAdvanceDate,S.CustomerID, Cu.CustomerName As Customer, Cu.CustomerAddress As Address, S.StaffID, F.Staff, S.TotalAmount,G.Remark,S.Discount,G.ItemTG," & _
                                       " G.Qty,G.Amount, (S.TotalAmount-S.Discount) As NetAmount ,S.ReturnAdvanceDate AS [@ReturnAdvanceDate],G.SaleRate From tbl_ReturnAdvance S " & _
                                       " Inner Join tbl_ReturnAdvanceItem G on G.ReturnAdvanceID=S.ReturnAdvanceID " & _
                                       " LEFT JOIN tbl_Customer Cu ON Cu.CustomerID=S.CustomerID  " & _
                                       " Inner Join tbl_Staff F on F.StaffID=S.StaffID Where 1=1 AND S.IsDelete=0 AND S.ReturnAdvanceDate  BETWEEN @FromDate And @ToDate And G.Isused=0" & cristr & _
                                       "  ) as M Order By [@ReturnAdvanceDate] DESC, ReturnAdvanceID, ReturnAdvanceItemID ASC "
                    Case "Used"
                        strCommandText = " Select R.CashReceiptID,R.VoucherNo,S.ReturnAdvanceID,G.ReturnAdvanceItemID,S.ReturnAdvanceDate,S.CustomerID, Cu.CustomerName As Customer, Cu.CustomerAddress As Address, S.StaffID," & _
                                      " F.Staff, S.TotalAmount,G.Remark,S.Discount,G.ItemTG,G.Qty,G.Amount, (S.TotalAmount-S.Discount) As NetAmount ,S.ReturnAdvanceDate AS [@ReturnAdvanceDate],G.SaleRate From tbl_ReturnAdvance S " & _
                                      " left join tbl_cashReceipt R On S.ReturnAdvanceID=R.ReturnAdvanceID " & _
                                      " Inner Join tbl_ReturnAdvanceItem G on G.ReturnAdvanceID=S.ReturnAdvanceID " & _
                                      " LEFT JOIN tbl_Customer Cu ON Cu.CustomerID=S.CustomerID  " & _
                                      " Inner Join tbl_Staff F on F.StaffID=S.StaffID Where 1=1 AND S.IsDelete=0 AND S.ReturnAdvanceDate  BETWEEN @FromDate And @ToDate  And R.isdelete=0  " & cristr & " Order By [@ReturnAdvanceDate] DESC, ReturnAdvanceID, ReturnAdvanceItemID ASC "
                    Case "NotUsed"
                        strCommandText = " Select '' as CashReceiptID,'' as VoucherNo,S.ReturnAdvanceID,G.ReturnAdvanceItemID,S.ReturnAdvanceDate,S.CustomerID, Cu.CustomerName As Customer, Cu.CustomerAddress As Address, S.StaffID, F.Staff, S.TotalAmount,G.Remark,S.Discount,G.ItemTG," & _
                                       " G.Qty,G.Amount, (S.TotalAmount-S.Discount) As NetAmount ,S.ReturnAdvanceDate AS [@ReturnAdvanceDate],G.SaleRate From tbl_ReturnAdvance S " & _
                                       " Inner Join tbl_ReturnAdvanceItem G on G.ReturnAdvanceID=S.ReturnAdvanceID " & _
                                       " LEFT JOIN tbl_Customer Cu ON Cu.CustomerID=S.CustomerID  " & _
                                       " Inner Join tbl_Staff F on F.StaffID=S.StaffID Where 1=1 AND S.IsDelete=0 AND S.ReturnAdvanceDate  BETWEEN @FromDate And @ToDate And G.Isused=0" & cristr & _
                                       "  Order By [@ReturnAdvanceDate] DESC, ReturnAdvanceID, ReturnAdvanceItemID ASC "
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

        'Public Function GetAllReturnAdvance() As System.Data.DataTable Implements IReturnAdvanceDA.GetAllReturnAdvance
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Dim dtResult As DataTable

        '    Try

        '        strCommandText = "Select S.ReturnAdvanceID ,convert(varchar(10),S.ReturnAdvanceDate,105) As SaleDate,S.StaffID As [@StaffID],ST.Name as [Name_], S.CustomerID AS [@CustomerID], C.CustomerName as [Customer_], S.PaidAmount,S.TotalAmount,S.Remark as [Remark_] "
        '        strCommandText += " From tbl_ReturnAdvance S Left Join tbl_StaffByLocation ST On S.StaffID=ST.SaleStaffID LEFT JOIN tbl_Customer C ON C.CustomerID=S.CustomerID where S.IsDelete=0 Order by ReturnAdvanceID"

        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
        '        Return dtResult
        '    Catch ex As Exception
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
        '        Return New DataTable
        '    End Try
        'End Function
        Public Function GetReturnAdvancePrint(ByVal ReturnAdvanceID As String) As System.Data.DataTable Implements IReturnAdvanceDA.GetReturnAdvancePrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT H.ReturnAdvanceID,GI.ReturnAdvanceItemID, ReturnAdvanceDate, H.StaffID, S.Staff as Staff, H.CustomerID,C.CustomerName AS Customer, C.CustomerAddress AS Address,C.CustomerTel,H.TotalAmount,GI.SaleRate,H.LastModifiedLoginUserName, " & _
                "  GI.Qty,GI.SaleRate, GI.Amount,GI.ItemTG,  " & _
                " H.Discount " & _
                " FROM tbl_ReturnAdvance H " & _
                " LEFT JOIN tbl_ReturnAdvanceItem GI ON GI.ReturnAdvanceID=H.ReturnAdvanceID " & _
                " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
                " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
                " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
                " WHERE H.ReturnAdvanceID= @ReturnAdvanceID and H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, ReturnAdvanceID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        'Public Function InsertReturnAdvanceUserID(ByVal ReturnAdvanceID As String, ByVal UserID As String) As Boolean Implements IReturnAdvanceDA.InsertReturnAdvanceUserID
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Try
        '        strCommandText = "Update tbl_ReturnAdvance set   UserID= @UserID "
        '        strCommandText += " where ReturnAdvanceID= @ReturnAdvanceID"
        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        DB.AddInParameter(DBComm, "@ReturnAdvanceID", DbType.String, ReturnAdvanceID)
        '        DB.AddInParameter(DBComm, "@UserID", DbType.String, UserID)
        '        If DB.ExecuteNonQuery(DBComm) > 0 Then
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
        '        Return False
        '    End Try
        'End Function
        Public Function GetReturnAdvanceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IReturnAdvanceDA.GetReturnAdvanceReportForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "Select  Distinct(S.ReturnAdvanceID), S.TotalAmount,  (S.TotalAmount-S.Discount) As NetAmount From tbl_ReturnAdvance S " & _
                                "Inner Join tbl_ReturnAdvanceItem G on G.ReturnAdvanceID=S.ReturnAdvanceID Inner JOIN tbl_Customer Cu ON Cu.CustomerID=S.CustomerID  Inner Join tbl_Staff F on F.StaffID=S.StaffID  Where 1=1 and S.IsDelete=0 And S.ReturnAdvanceDate " & _
                                " BETWEEN @FromDate And @ToDate " & cristr
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

        'Public Function GetReturnAdvanceBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As CommonInfo.ReturnAdvanceItemInfo Implements IReturnAdvanceDA.GetReturnAdvanceBalanceStockByGemsCategoryID
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Dim drResult As IDataReader
        '    Dim obj As New CommonInfo.ReturnAdvanceItemInfo
        '    Try
        '        strCommandText = "Select M.GemsCategoryID, M.GemsCategory, Sum(M.TotalQTY) AS TotalQTY, Sum(M.SaleQTY) As SaleQTY, Sum(M.TotalGemsTK) AS TotalGemsTK, Sum(M.TotalGemsTG) AS TotalGemsTG," & _
        '                       " Sum(M.ReturnAdvanceTK) AS ReturnAdvanceTK, Sum(M.ReturnAdvanceTG) AS ReturnAdvanceTG, SUM(M.TotalQTY-M.SaleQTY) AS BalanceQTY, SUM(M.TotalGemsTK-M.ReturnAdvanceTK) As BalanceGemsTK, SUM(M.TotalGemsTG-M.ReturnAdvanceTG) As BalanceGemsTG, " & _
        '                       " CAST(SUM(M.TotalGemsTK-M.ReturnAdvanceTK) AS INT) AS BalanceGemsK," & _
        '                       " CAST((SUM(M.TotalGemsTK-M.ReturnAdvanceTK)-CAST(SUM(M.TotalGemsTK-M.ReturnAdvanceTK) AS INT))*16 AS INT) AS BalanceGemsP," & _
        '                       " CAST((((SUM(M.TotalGemsTK-M.ReturnAdvanceTK)-CAST(SUM(M.TotalGemsTK-M.ReturnAdvanceTK) AS INT))*16)-CAST((SUM(M.TotalGemsTK-M.ReturnAdvanceTK)-CAST(SUM(M.TotalGemsTK-M.ReturnAdvanceTK) AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS BalanceGemsY " & _
        '                       " FROM (select PD.GemsCategoryID, G.GemsCategory, PD.QTY AS TotalQTY, 0 AS SaleQTY, PD.GemsTK AS TotalGemsTK, " & _
        '                       " PD.GemsTG AS TotalGemsTG, 0 AS ReturnAdvanceTK, 0 AS ReturnAdvanceTG from tbl_PurchaseOutItemDetail PD " & _
        '                       " LEFT JOIN tbl_GemsCategory G On G.GemsCategoryID=PD.GemsCategoryID Where DivideType='Stock' AND PD.GemsCategoryID=@GemsCategoryID " & _
        '                       " UNION ALL" & _
        '                       " select PD.GemsCategoryID, G.GemsCategory, 0 AS TotalQTY, PD.Qty AS SaleQTY, 0 AS TotalGemsTK, 0 AS TotalGemsTG," & _
        '                       " PD.GemsTK AS ReturnAdvanceTK, PD.GemsTG AS SaleTG   " & _
        '                       " from tbl_ReturnAdvanceItem PD LEFT JOIN tbl_GemsCategory G On G.GemsCategoryID=PD.GemsCategoryID Where PD.GemsCategoryID=@GemsCategoryID and PD.IsDelete=0 ) AS M GROUP BY  M.GemsCategoryID, M.GemsCategory "

        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        DB.AddInParameter(DBComm, "@GemsCategoryID", DbType.String, GemsCategoryID)
        '        drResult = DB.ExecuteReader(DBComm)
        '        If drResult.Read() Then
        '            With obj

        '                .Qty = drResult("SaleQTY")
        '            End With
        '        End If
        '        drResult.Close()
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
        '    End Try
        '    Return obj
        'End Function

        'Private Function DiscountAmount() As Object
        '    Throw New NotImplementedException
        'End Function

        'Private Function PromotionDiscount() As Object
        '    Throw New NotImplementedException
        'End Function
        'Public Function GetAllReturnAdvanceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IReturnAdvanceDA.GetAllReturnAdvanceVoucherPrint
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Dim dtResult As DataTable
        '    Try
        '        strCommandText = "SELECT H.ReturnAdvanceID, ReturnAdvanceDate, S.Staff as Staff, H.CustomerID, C.CustomerName AS Customer, C.CustomerAddress As Address, H.TotalAmount,H.AddOrSub,GI.SaleRate, H.PaidAmount,H.LastModifiedLoginUserName, " & _
        '        " L.Location, I.GemsCategory, GI.Qty, GI.YOrCOrG, GI.GemsName, GI.Clarity,GI.SizeMM,GI.GemsTK, GI.GemsTG, GI.GemsTW, GI.FixType, GI.SaleRate, GI.Amount,  " & _
        '        " CAST(GI.GemsTK AS INT) AS GemsK, " & _
        '        " CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT) AS GemsP, " & _
        '        " CAST((((GI.GemsTK-CAST(GI.GemsTK AS INT))*16)-CAST((GI.GemsTK-CAST(GI.GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, " & _
        '        " U.UserName,H.DiscountAmount,H.PromotionDiscount,(H.TotalAmount*PromotionDiscount)/100 As PromotionAmount, ReturnAdvanceDate AS [@ReturnAdvanceDate]  " & _
        '        " FROM tbl_ReturnAdvance H " & _
        '        " LEFT JOIN tbl_ReturnAdvanceItem GI ON GI.ReturnAdvanceID=H.ReturnAdvanceID " & _
        '        " LEFT JOIN tbl_Location L ON L.LocationID=H.LocationID " & _
        '        " LEFT JOIN tbl_Staff S ON S.StaffID=H.StaffID " & _
        '        " LEFT JOIN tbl_Customer C ON C.CustomerID=H.CustomerID " & _
        '        " LEFT JOIN tbl_GemsCategory I ON I.GemsCategoryID=GI.GemsCategoryID " & _
        '        " LEFT JOIN tb_GE_SystemUser U ON H.UserID=U.UserID " & _
        '         " WHERE ReturnAdvanceDate BETWEEN @FromDate AND @ToDate and H.IsDelete=0  " & cristr & " ORDER BY [@ReturnAdvanceDate] DESC, H.ReturnAdvanceID ASC"
        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        DB.AddInParameter(DBComm, "@FromDate", DbType.Date, FromDate)
        '        DB.AddInParameter(DBComm, "@ToDate", DbType.Date, ToDate)
        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
        '        Return dtResult
        '    Catch ex As Exception
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
        '        Return New DataTable
        '    End Try
        'End Function

        'Public Function GetReturnAdvanceItemByReturnAdvanceItemID(ByVal ReturnAdvanceItemID As String) As System.Data.DataTable Implements IReturnAdvanceDA.GetReturnAdvanceItemByReturnAdvanceItemID
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand
        '    Dim dtResult As DataTable
        '    Try

        '        strCommandText = "SELECT ReturnAdvanceItemID,ReturnAdvanceID,GemsCategoryID,GemsName,"
        '        strCommandText += " CAST(GemsTK AS INT) AS GemsK,"
        '        strCommandText += " CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT) AS GemsP, "
        '        strCommandText += " CAST((((GemsTK-CAST(GemsTK AS INT))*16)-CAST((GemsTK-CAST(GemsTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS GemsY, "
        '        strCommandText += " GemsTK, GemsTG, YOrCOrG, GemsTW, QTY, CASE FixType WHEN 1 THEN 'Fix' WHEN 2 THEN 'ByWeight' WHEN 3 THEN 'ByQty' END AS FixType,SaleRate,Amount "
        '        strCommandText += " From tbl_ReturnAdvanceItem Where ReturnAdvanceItemID = '" & ReturnAdvanceItemID & "' Order By ReturnAdvanceItemID"
        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
        '        Return dtResult

        '    Catch ex As Exception
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
        '        Return New DataTable
        '    End Try
        'End Function
    End Class
End Namespace

