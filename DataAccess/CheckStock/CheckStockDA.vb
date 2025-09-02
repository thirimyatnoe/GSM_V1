Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace CheckStock
    Public Class CheckStockDA
        Implements ICheckStockDA

#Region "Private Stock"

        Private DB As Database
        Private Shared ReadOnly _instance As ICheckStockDA = New CheckStockDA

#End Region

#Region "Constructors"

        Public Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICheckStockDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        

        Public Function InsertCheckStock(ByVal objCheckStock As CommonInfo.CheckStockInfo) As Boolean Implements ICheckStockDA.InsertCheckStock
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_CheckStock (CheckStockID,checkdatetime,ItemCategoryID,StaffID,QTY,GoldTG,MQTY,MGoldTG,FQTY,FGoldTG,Remark,LocationID,LastModifiedLoginUserName,LastModifiedDate,IsUpload,IsDelete)"
                strCommandText += " Values (@CheckStockID,@checkdatetime,@ItemCategoryID,@StaffID,@QTY,@GoldTG,@MQTY,@MGoldTG,@FQTY,@FGoldTG,@Remark,@LocationID,@LastModifiedLoginUserName,getdate(),CONVERT(bit,0),CONVERT(bit,0))"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, objCheckStock.CheckStockID)
                DB.AddInParameter(DBComm, "@checkdatetime", DbType.DateTime, objCheckStock.checkdatetime)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, objCheckStock.ItemCategoryID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, objCheckStock.StaffID)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, objCheckStock.QTY)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, objCheckStock.GoldTG)
                DB.AddInParameter(DBComm, "@MQTY", DbType.Int32, objCheckStock.MQTY)
                DB.AddInParameter(DBComm, "@MGoldTG", DbType.Decimal, objCheckStock.MGoldTG)
                DB.AddInParameter(DBComm, "@FQTY", DbType.Int32, objCheckStock.FQTY)
                DB.AddInParameter(DBComm, "@FGoldTG", DbType.Decimal, objCheckStock.FGoldTG)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, objCheckStock.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
                Return False
            End Try
        End Function
        Public Function InsertCheckStockItem(ByVal objCheckStockItem As CommonInfo.CheckStockItemInfo) As Boolean Implements ICheckStockDA.InsertCheckStockItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_CheckStockItem (CheckStockItemID,CheckStockID,MBarcodeNo,MGoldTG,MItemCategoryID)"
                strCommandText += " Values (@CheckStockItemID,@CheckStockID,@MBarcodeNo,@MGoldTG,@MItemCategoryID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockItemID", DbType.String, objCheckStockItem.CheckStockItemID)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, objCheckStockItem.CheckStockID)
                DB.AddInParameter(DBComm, "@MBarcodeNo", DbType.String, objCheckStockItem.MBarcodeNo)
                DB.AddInParameter(DBComm, "@MGoldTG", DbType.Decimal, objCheckStockItem.MGoldTG)
                DB.AddInParameter(DBComm, "@MItemCategoryID", DbType.String, objCheckStockItem.MItemCategoryID)


                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
                Return False
            End Try
        End Function

        Public Function InsertECheckStockItem(ByVal objECheckStockItem As CommonInfo.ECheckStockItemInfo) As Boolean Implements ICheckStockDA.InsertECheckStockItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ECheckStockItem (ECheckStockItemID,CheckStockID,EBarcodeNo,Weight)"
                strCommandText += " Values (@ECheckStockItemID,@CheckStockID,@EBarcodeNo,@Weight)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ECheckStockItemID", DbType.String, objECheckStockItem.ECheckStockItemID)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, objECheckStockItem.CheckStockID)
                DB.AddInParameter(DBComm, "@EBarcodeNo", DbType.String, objECheckStockItem.EBarcodeNo)
                DB.AddInParameter(DBComm, "@Weight", DbType.Decimal, objECheckStockItem.Weight)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
                Return False
            End Try
        End Function
        Public Function GetCheckStockPrint(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockDA.GetCheckStockPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CS.CheckStockID,CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,CS.ItemCategoryID,CS.StaffID,CS.QTY,CS.GoldTG," & _
                                " CS.MQTY,CS.MGoldTG,CS.FQTY,CS.FGoldTG,CS.Remark,ST.Staff,CS.LocationID,L.Location  From tbl_CheckStock CS " & _
                                " INNER JOIN tbl_Location L ON CS.LocationID=L.LocationID  " & _
                                " INNER JOIN tbl_Staff ST ON ST.StaffID=CS.StaffID  " & _
                                " where CS.CheckStockID = @CheckStockID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMCheckStockPrint(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockDA.GetMCheckStockPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CSI.CheckStockItemID,CS.CheckStockID,CSI.MBarcodeNo,CSI.MGoldTG,CSI.MItemCategoryID,CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,CS.ItemCategoryID,CS.StaffID,CS.QTY,CS.GoldTG," & _
                                " CS.MQTY,CS.MGoldTG,CS.FQTY,CS.FGoldTG,CS.Remark From tbl_CheckStock CS " & _
                                " INNER JOIN tbl_CheckStockItem CSI  ON CSI.CheckStockID=CS.CheckStockID " & _
                                " where CS.CheckStockID = @CheckStockID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetECheckStockPrint(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockDA.GetECheckStockPrint
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CS.CheckStockID,E.ECheckStockItemID,E.EBarcodeNo,CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,CS.ItemCategoryID,CS.StaffID,CS.QTY,CS.GoldTG," & _
                                " CS.MQTY,CS.MGoldTG,CS.FQTY,CS.FGoldTG,CS.Remark,E.Weight From tbl_CheckStock CS " & _
                                " Inner JOIN tbl_ECheckStockItem E  ON CS.CheckStockID=E.CheckStockID   " & _
                                " where CS.CheckStockID = @CheckStockID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetCheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockDA.GetCheckStockReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                ' strCommandText = "SELECT CS.CheckStockID,CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,CS.ItemCategoryID,CS.StaffID,CS.QTY,CS.GoldTG," & _
                '" CS.MQTY,CS.MGoldTG,CS.FQTY,CS.FGoldTG,CS.Remark,ST.Staff,CS.LocationID,L.Location,ItemCategory  From tbl_CheckStock CS " & _
                ' " INNER JOIN tbl_ItemCategory I ON CS.ItemCategoryID=I.ItemCategoryID  " & _
                '" INNER JOIN tbl_Location L ON CS.LocationID=L.LocationID  " & _
                ' " INNER JOIN tbl_Staff ST ON ST.StaffID=CS.StaffID  " & _
                ' " where CS.checkdatetime between '" & dtpDate & " 00:00:00' AND '" & dtpDate & " 23:59:59'" & cristr
                strCommandText = "SELECT CS.CheckStockID,CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,CS.ItemCategoryID,CS.StaffID,CS.QTY,CS.GoldTG, " & _
                                 " CS.MQTY,CS.MGoldTG,CS.FQTY,CS.FGoldTG,CS.Remark,ST.Staff,CS.LocationID,L.Location,ItemCategory,isnull(CI.CheckStockItemID,0) as CheckStockItemID,CI.MBarcodeNo,CI.MGoldTG as MGoldTGD, " & _
                                 " CI.MItemCategoryID,isnull(EC.ECheckStockItemID,0) as ECheckStockItemID,EC.EBarcodeNo,Ec.Weight as ExtraWeight " & _
                                 " From tbl_CheckStock CS  INNER JOIN tbl_ItemCategory I ON CS.ItemCategoryID=I.ItemCategoryID  " & _
                                 " INNER JOIN tbl_Location L ON CS.LocationID=L.LocationID   INNER JOIN tbl_Staff ST ON ST.StaffID=CS.StaffID" & _
                                 " LEFT JOIN tbl_CheckStockItem CI on CS.CheckSTockId=CI.CheckStockID " & _
                                 " LEFT JOIN tbl_ECheckStockItem EC on Cs.CheckStockID=EC.CheckStockID " & _
                                 " where CS.IsDelete=0 And CS.checkdatetime between @FromDate And @ToDate " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(dtpDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(dtpDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetMCheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockDA.GetMCheckStockReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CSI.CheckStockItemID,CS.CheckStockID,CSI.MBarcodeNo,CSI.MGoldTG,CSI.MItemCategoryID,CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,CS.ItemCategoryID,CS.StaffID,CS.QTY,CS.GoldTG," & _
                                " CS.MQTY,CS.MGoldTG,CS.FQTY,CS.FGoldTG,CS.Remark,I.ItemCategory From tbl_CheckStock CS " & _
                                " INNER JOIN tbl_CheckStockItem CSI  ON CSI.CheckStockID=CS.CheckStockID " & _
                                " INNER JOIN tbl_ItemCategory I  ON CSI.MItemCategoryID=I.ItemCategoryID " & _
                                " where CS.checkdatetime between @FromDate And @ToDate " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(dtpDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(dtpDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
       
        Public Function GetAllCheckStockLists(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockDA.GetAllCheckStockLists
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,S.Staff,L.Location,I.ItemCategory,QTY,CS.StaffID,GoldTG,MQTY,MGoldTG,FQTY,FGoldTG " & _
                                " From tbl_CheckStock CS Inner join tbl_Staff S On CS.StaffID=S.StaffID Inner Join tbl_ItemCategory I on CS.ItemCategoryID=I.ItemCategoryID Inner Join tbl_Location L on CS.LocationID=L.LocationID WHERE CS.IsDelete=0  Order by CS.checkdatetime desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetECheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockDA.GetECheckStockReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT CS.CheckStockID,E.ECheckStockItemID,E.EBarcodeNo,CS.CheckStockID,convert(varchar(10),CS.checkdatetime,105) as checkdatetime,CS.ItemCategoryID,CS.StaffID,CS.QTY,CS.GoldTG," & _
                                " CS.MQTY,CS.MGoldTG,CS.FQTY,CS.FGoldTG,CS.Remark,E.Weight From tbl_CheckStock CS " & _
                                " Inner JOIN tbl_ECheckStockItem E  ON CS.CheckStockID=E.CheckStockID   " & _
                                " where CS.checkdatetime between @FromDate And @ToDate " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)
                DB.AddInParameter(DBComm, "@FromDate", DbType.DateTime, CDate(dtpDate.Date & " 00:00:00"))
                DB.AddInParameter(DBComm, "@ToDate", DbType.DateTime, CDate(dtpDate.Date & " 23:59:59"))
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetCheckStockByID(ByVal CheckStockID As String) As CommonInfo.CheckStockInfo Implements ICheckStockDA.GetCheckStockByID

            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CheckStockInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_CheckStock WHERE CheckStockID= @CheckStockID AND IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .CheckStockID = drResult("CheckStockID")
                        .checkdatetime = drResult("checkdatetime")
                        .StaffID = drResult("StaffID")
                        .GoldTG = drResult("GoldTG")
                        .QTY = drResult("QTY")
                        .MGoldTG = drResult("MGoldTG")
                        .MQTY = drResult("MQTY")
                        .FGoldTG = drResult("FGoldTG")
                        .FQTY = drResult("FQTY")
                        .Remark = drResult("Remark")
                        .LocationID = drResult("LocationID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
        Public Function GetCheckStockItem(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockDA.GetCheckStockItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT SI.CheckStockItemID,SI.CheckStockID,SI.MBarcodeNo as ItemCode,SI.MGoldTG as GoldTG,SI.MItemCategoryID as ItemCategoryID,I.ItemCategory  " & _
                                " From tbl_CheckStockItem SI INNER JOIN tbl_ItemCategory I ON SI.MItemCategoryID=I.ItemCategoryID where SI.CheckStockID = @CheckStockID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetCheckStockEItem(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockDA.GetCheckStockEItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                'strCommandText = "SELECT SI.ECheckStockItemID,SI.CheckStockID,SI.EBarcodeNo as BarcodeNo,SI.Weight as GoldTG " & _
                '" From tbl_ECheckStockItem SI  where SI.CheckStockID = @CheckStockID "
                strCommandText = "SELECT SI.EBarcodeNo as ItemCode,SI.Weight as GoldTG " & _
                                " From tbl_ECheckStockItem SI  where SI.CheckStockID = @CheckStockID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function UpdateCheckStock(ByVal objCheckStock As CommonInfo.CheckStockInfo) As Boolean Implements ICheckStockDA.UpdateCheckStock
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_CheckStock set  CheckStockID=@CheckStockID,checkdatetime=@checkdatetime,ItemCategoryID=@ItemCategoryID,StaffID=@StaffID,QTY=@QTY,GoldTG=@GoldTG,MQTY=@MQTY,MGoldTG=@MGoldTG,FQTY=@FQTY,FGoldTG=@FGoldTG,Remark=@Remark, LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= getDate(), IsUpload=CONVERT(bit,0)"
                strCommandText += " where CheckStockID= @CheckStockID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, objCheckStock.CheckStockID)
                DB.AddInParameter(DBComm, "@checkdatetime", DbType.DateTime, objCheckStock.checkdatetime)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, objCheckStock.ItemCategoryID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, objCheckStock.StaffID)
                DB.AddInParameter(DBComm, "@QTY", DbType.Int32, objCheckStock.QTY)
                DB.AddInParameter(DBComm, "@GoldTG", DbType.Decimal, objCheckStock.GoldTG)
                DB.AddInParameter(DBComm, "@MQTY", DbType.Int32, objCheckStock.MQTY)
                DB.AddInParameter(DBComm, "@MGoldTG", DbType.Decimal, objCheckStock.MGoldTG)
                DB.AddInParameter(DBComm, "@FQTY", DbType.Int32, objCheckStock.FQTY)
                DB.AddInParameter(DBComm, "@FGoldTG", DbType.Decimal, objCheckStock.FGoldTG)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, objCheckStock.Remark)
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
        Public Function UpdateMCheckStock(ByVal objCheckStockItem As CommonInfo.CheckStockItemInfo) As Boolean Implements ICheckStockDA.UpdateMCheckStock
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_CheckStockItem set  CheckStockItemID=@CheckStockItemID,CheckStockID=@CheckStockID,MBarcodeNo=@MBarcodeNo,MGoldTG=@MGoldTG,MItemCategoryID=@MItemCategoryID"
                strCommandText += " where CheckStockItemID= @CheckStockItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockItemID", DbType.String, objCheckStockItem.CheckStockItemID)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, objCheckStockItem.CheckStockID)
                DB.AddInParameter(DBComm, "@MBarcodeNo", DbType.String, objCheckStockItem.MBarcodeNo)
                DB.AddInParameter(DBComm, "@MGoldTG", DbType.Decimal, objCheckStockItem.MGoldTG)
                DB.AddInParameter(DBComm, "@MItemCategoryID", DbType.String, objCheckStockItem.MItemCategoryID)


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
        Public Function UpdateECheckStock(ByVal objECheckStockItem As CommonInfo.ECheckStockItemInfo) As Boolean Implements ICheckStockDA.UpdateECheckStock
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ECheckStockItem set  ECheckStockItemID=@ECheckStockItemID,CheckStockID=@CheckStockID,EBarcodeNo=@EBarcodeNo,Weight=@Weight"
                strCommandText += " where ECheckStockItemID= @ECheckStockItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ECheckStockItemID", DbType.String, objECheckStockItem.ECheckStockItemID)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, objECheckStockItem.CheckStockID)
                DB.AddInParameter(DBComm, "@EBarcodeNo", DbType.String, objECheckStockItem.EBarcodeNo)
                DB.AddInParameter(DBComm, "@Weight", DbType.Decimal, objECheckStockItem.Weight)

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
        Public Function DeleteCheckStock(ByVal CheckStockID As String) As Boolean Implements ICheckStockDA.DeleteCheckStock
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_CheckStock SET IsDelete =CONVERT(bit,1),IsUpload= CONVERT(bit,0) WHERE  CheckStockID= @CheckStockID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)

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

        Public Function DeleteECheckStock(ByVal ECheckStockItemID As String) As Boolean Implements ICheckStockDA.DeleteECheckStock
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_ECheckStockItem WHERE  ECheckStockItemID= @ECheckStockItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ECheckStockItemID", DbType.String, ECheckStockItemID)

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

        Public Function DeleteMCheckStock(ByVal CheckStockID As String) As Boolean Implements ICheckStockDA.DeleteMCheckStock
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_CheckStockItem WHERE  CheckStockID= @CheckStockID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CheckStockID", DbType.String, CheckStockID)

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
        Public Function UpdateIsCheck(ByVal ItemCode As String, ByVal IsCheck As Boolean) As Boolean Implements ICheckStockDA.UpdateIsCheck
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_ForSale SET IsCheck =@IsCheck WHERE  ItemCode= @ItemCode"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCode", DbType.String, ItemCode)
                DB.AddInParameter(DBComm, "@IsCheck", DbType.Boolean, IsCheck)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Update ! ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
        'test
        Public Function ResetIsCheck(ByVal ItemCategoryID As String) As Boolean Implements ICheckStockDA.ResetIsCheck
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_ForSale SET IsCheck =0 WHERE IsExit=0 And IsClosed=0 and IsDelete=0 And  ItemCategoryID= @ItemCategoryID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Reset ! ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
    End Class
End Namespace

