Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Repair
    Public Class RepairDA
        Implements IRepairDA


#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As IRepairDA = New DataAccess.Repair.RepairDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IRepairDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function InsertRepairHeader(ByVal Obj As CommonInfo.RepairHeaderInfo) As Boolean Implements IRepairDA.InsertRepairHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "INSERT INTO tbl_RepairHeader(RepairID, RepairDate, CustomerID, StaffID, Remark, AdvanceRepairAmount, DueDate, IsAllReturn, LastModifiedLoginUserName, LastModifiedDate, LocationID,IsDelete,IsSync)"
                strCommandText += " VALUES(@RepairID, @RepairDate, @CustomerID, @StaffID, @Remark, @AdvanceRepairAmount, @DueDate,@IsAllReturn, @LastModifiedLoginUserName, @LastModifiedDate, @LocationID,0,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, Obj.RepairID)
                DB.AddInParameter(DBComm, "@RepairDate", DbType.DateTime, Obj.RepairDate)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@AdvanceRepairAmount", DbType.Int32, Obj.AdvanceRepairAmount)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, Obj.DueDate)
                DB.AddInParameter(DBComm, "@IsAllReturn", DbType.Boolean, False)
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

        Public Function UpdateRepairHeader(ByVal Obj As CommonInfo.RepairHeaderInfo) As Boolean Implements IRepairDA.UpdateRepairHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_RepairHeader Set  RepairDate=@RepairDate, CustomerID=@CustomerID, StaffID=@StaffID, Remark=@Remark, AdvanceRepairAmount=@AdvanceRepairAmount, DueDate=@DueDate, LastModifiedLoginUserName=@LastModifiedLoginUserName,LastModifiedDate=@LastModifiedDate, LocationID=@LocationID,IsSync=0 "
                strCommandText += " where RepairID= @RepairID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, Obj.RepairID)
                DB.AddInParameter(DBComm, "@RepairDate", DbType.DateTime, Obj.RepairDate)
                DB.AddInParameter(DBComm, "@CustomerID", DbType.String, Obj.CustomerID)
                DB.AddInParameter(DBComm, "@StaffID", DbType.String, Obj.StaffID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, Obj.Remark)
                DB.AddInParameter(DBComm, "@AdvanceRepairAmount", DbType.Int32, Obj.AdvanceRepairAmount)
                DB.AddInParameter(DBComm, "@DueDate", DbType.Date, Obj.DueDate)
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


        Public Function InsertRepairReceiveDetail(DetailObj As RepairDetailInfo) As Boolean Implements IRepairDA.InsertRepairReceiveDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_RepairDetail ( RepairDetailID,RepairID,IsFromShop,BarcodeNo,ItemCategoryID,ItemNameID,GoldQualityID,LengthOrWidth,CurrentPrice,Design,ItemTK,ItemTG,IsExit,DetailRemark)"
                strCommandText += " Values (@RepairDetailID,@RepairID,@IsFromShop,@BarcodeNo,@ItemCategoryID,@ItemNameID,@GoldQualityID,@LengthOrWidth,@CurrentPrice,@Design,@ItemTK,@ItemTG,@IsExit,@DetailRemark)"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@RepairDetailID", DbType.String, DetailObj.RepairDetailID)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, DetailObj.RepairID)
                DB.AddInParameter(DBComm, "@BarcodeNo", DbType.String, DetailObj.BarcodeNo)
                DB.AddInParameter(DBComm, "@LengthOrWidth", DbType.String, DetailObj.LengthOrWidth)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, DetailObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, DetailObj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, DetailObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@CurrentPrice", DbType.Int32, DetailObj.CurrentPrice)
                DB.AddInParameter(DBComm, "@Design", DbType.String, DetailObj.Design)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, DetailObj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, DetailObj.ItemTG)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, False)
                DB.AddInParameter(DBComm, "@IsFromShop", DbType.Boolean, DetailObj.IsFromShop)
                DB.AddInParameter(DBComm, "@DetailRemark", DbType.String, DetailObj.DetailRemark)

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

        Public Function UpdateRepairReceiveDetail(DetailObj As RepairDetailInfo) As Boolean Implements IRepairDA.UpdateRepairReceiveDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_RepairDetail Set  RepairID=@RepairID,IsFromShop=@IsFromShop,BarcodeNo=@BarcodeNo,ItemCategoryID=@ItemCategoryID,ItemNameID=@ItemNameID,GoldQualityID=@GoldQualityID,LengthOrWidth=@LengthOrWidth,CurrentPrice=@CurrentPrice,Design=@Design,ItemTK=@ItemTK,ItemTG=@ItemTG,IsExit=@IsExit,DetailRemark=@DetailRemark "
                strCommandText += " where RepairDetailID= @RepairDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@RepairDetailID", DbType.String, DetailObj.RepairDetailID)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, DetailObj.RepairID)
                DB.AddInParameter(DBComm, "@BarcodeNo", DbType.String, DetailObj.BarcodeNo)
                DB.AddInParameter(DBComm, "@LengthOrWidth", DbType.String, DetailObj.LengthOrWidth)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, DetailObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, DetailObj.ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, DetailObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@CurrentPrice", DbType.Int32, DetailObj.CurrentPrice)
                DB.AddInParameter(DBComm, "@Design", DbType.String, DetailObj.Design)
                DB.AddInParameter(DBComm, "@ItemTK", DbType.Decimal, DetailObj.ItemTK)
                DB.AddInParameter(DBComm, "@ItemTG", DbType.Decimal, DetailObj.ItemTG)
                DB.AddInParameter(DBComm, "@IsExit", DbType.Boolean, False)
                DB.AddInParameter(DBComm, "@IsFromShop", DbType.Boolean, DetailObj.IsFromShop)
                DB.AddInParameter(DBComm, "@DetailRemark", DbType.String, DetailObj.DetailRemark)

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
        Public Function DeleteRepairDetail(ByVal RepairDetailID As String) As Boolean Implements IRepairDA.DeleteRepairDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_RepairDetail Where RepairDetailID = @RepairDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@RepairDetailID", DbType.String, RepairDetailID)

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

        Public Function GetAllRepairHeader() As DataTable Implements IRepairDA.GetAllRepairHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.RepairID AS VoucherNo, H.RepairID AS [@RepairID], CONVERT(VARCHAR(10),H.RepairDate,105) as RepairDate,H.CustomerID As [@CustomerID],H.StaffID As [@StaffID]," & _
                                 " C.CustomerName As [Customer_],C.CustomerAddress As [Address_], C.CustomerCode, S.Staff As [Staff_], H.Remark AS [Remark_], H.AdvanceRepairAmount, CONVERT(VARCHAR(10),H.DueDate,105) as DueDate" & _
                                 " From tbl_RepairHeader H LEFT JOIN tbl_Customer C on C.CustomerID=H.CustomerID LEFT JOIN tbl_Staff  S on S.StaffID=H.StaffID " & _
                                 " Where H.IsAllReturn=0 And H.IsDelete=0 Order By H.RepairDate DESC , H.RepairID DESC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetRepairHeaderInfo(RepairID As String) As RepairHeaderInfo Implements IRepairDA.GetRepairHeaderInfo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objOrderInvoice As New RepairHeaderInfo
            Try
                strCommandText = "Select * From tbl_RepairHeader  WHERE RepairID=@RepairID And IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, RepairID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objOrderInvoice
                        .RepairID = drResult("RepairID")
                        .RepairDate = drResult("RepairDate")
                        .DueDate = drResult("DueDate")
                        .CustomerID = drResult("CustomerID")
                        .StaffID = drResult("StaffID")
                        .AdvanceRepairAmount = drResult("AdvanceRepairAmount")
                        .Remark = drResult("Remark")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objOrderInvoice
        End Function

        Public Function GetRepairReceiveDetail(RepairID As String) As DataTable Implements IRepairDA.GetRepairReceiveDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Try
                strCommandText = " Select D.RepairDetailID,D.RepairID,D.IsFromShop,D.BarcodeNo,D.ItemCategoryID,D.ItemNameID,D.GoldQualityID,D.LengthOrWidth,D.CurrentPrice,D.Design, " & _
                                 " D.ItemTK, D.ItemTG, D.DetailRemark, CASE D.DetailRemark WHEN '' THEN N.ItemName ELSE D.DetailRemark END AS ItemName, G.GoldQuality,CAST(D.ItemTK AS INT) AS ItemK,CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY ,D.IsExit" & _
                                 " From tbl_RepairDetail D  LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID " & _
                                 " LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=D.GoldQualityID " & _
                                 " Where D.RepairID ='" & RepairID & "' Order By RepairDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetReturnRepairHeaderForIsPaid() As DataTable Implements IRepairDA.GetReturnRepairHeaderForIsPaid
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select RH.RepairID, Convert(varchar,RH.RepairDate,105) As RepairDate, Convert(varchar,RH.DueDate,105) As DueDate, C.CustomerCode,C.CustomerName AS [Customer_], C.CustomerAddress AS [Address_], RH.Remark AS [Remark_],RH.RepairDate As [@RDate] " & _
                                 " From tbl_RepairHeader RH LEFT JOIN tbl_Customer C on C.CustomerID=RH.CustomerID Where RH.IsAllReturn=0 And RH.IsDelete=0 Order By [@RDate] desc, RepairID DESC"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetForRepairReturnbyRepairDetail(ByVal RepairID As String, Optional ByVal RepairDetailID As String = "") As DataTable Implements IRepairDA.GetForRepairReturnbyRepairDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Dim strWhere As String = ""
            If RepairDetailID <> "" Then
                strWhere = " AND D.RepairDetailID NOT IN (" & RepairDetailID & ")"
            End If
            Try
                strCommandText = " Select D.RepairDetailID As [@RepairDetailID],D.RepairID AS [@RepairID],D.BarcodeNo,CASE D.DetailRemark WHEN '' THEN N.ItemName ELSE D.DetailRemark END AS [ItemName_], IsNull(I.ItemCategory,'-') As [ItemCategory_], G.GoldQuality AS [GoldQuality_],D.LengthOrWidth AS [LengthOrWidth_], " & _
                                 " D.CurrentPrice, D.DetailRemark AS [DetailRemark_], D.Design AS [Design_], D.ItemTK As [@OrgItemTK],D.ItemTG As [@OrgItemTG], CAST(D.ItemTG AS DECIMAL(18,3))  As OrgItemTG, " & _
                                 " CAST(D.ItemTK AS INT) AS OrgItemK,CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS OrgItemP, " & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OrgItemY,  " & _
                                 " D.IsFromShop, D.ItemCategoryID AS [@ItemCategoryID],D.ItemNameID As [@ItemNameID],D.GoldQualityID As [@GoldQualityID]" & _
                                 " from tbl_RepairDetail D  LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID " & _
                                 " LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=D.GoldQualityID Where D.IsExit=0 And D.RepairID='" & RepairID & "'" & strWhere

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
       
        Public Function GetRepairReceiveDetailForUpdate(RepairID As String) As DataTable Implements IRepairDA.GetRepairReceiveDetailForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Try
                strCommandText = " Select D.RepairDetailID,D.RepairID,D.IsFromShop,D.BarcodeNo,D.ItemCategoryID,I.ItemCategory,D.ItemNameID,N.ItemName,D.GoldQualityID,G.GoldQuality,D.LengthOrWidth,  " & _
                                 " D.CurrentPrice,D.Design,D.ItemTK,D.ItemTG,D.IsExit,D.DetailRemarkFrom tbl_RepairDetail D " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID  " & _
                                 " LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=D.GoldQualityID Where D.RepairID='" & RepairID & "'Order By RepairDetailID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetRepairDetailInfo(BarcodeNo As String) As RepairDetailInfo Implements IRepairDA.GetRepairDetailInfo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objOrderInvoice As New RepairDetailInfo
            Try
                strCommandText = "Select * ,CAST(ItemTK AS INT) AS OrgItemK,CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT) AS OrgItemP, " & _
                                 " CAST((((ItemTK-CAST(ItemTK AS INT))*16)-CAST((ItemTK-CAST(ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS OrgItemY From tbl_RepairDetail  WHERE BarcodeNo=@BarcodeNo "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@BarcodeNo", DbType.String, BarcodeNo)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objOrderInvoice
                        .RepairDetailID = drResult("RepairDetailID")
                        .RepairID = drResult("RepairID")
                        .IsFromShop = drResult("IsFromShop")
                        .BarcodeNo = drResult("BarcodeNo")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemNameID = drResult("ItemNameID")
                        .GoldQualityID = drResult("GoldQualityID")
                        .LengthOrWidth = drResult("LengthOrWidth")
                        .CurrentPrice = drResult("CurrentPrice")
                        .Design = drResult("Design")
                        .ItemTK = drResult("ItemTK")
                        .ItemTG = drResult("ItemTG")
                        .DetailRemark = drResult("DetailRemark")
                        .OrgItemK = drResult("OrgItemK")
                        .OrgItemP = drResult("OrgItemP")
                        .OrgItemY = drResult("OrgItemY")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objOrderInvoice
        End Function

        Public Function GetRepairReceiveForVoucher(RepairID As String) As DataTable Implements IRepairDA.GetRepairReceiveForVoucher
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.RepairID,H.RepairDate,H.ReturnDate,H.CustomerID,H.StaffID,H.Remark,H.AdvanceRepairAmount,H.DueDate, " & _
                                  " C.CustomerName,C.CustomerAddress,C.Customertel,S.Staff, " & _
                                  " D.RepairDetailID,D.BarcodeNo,D.ItemCategoryID,D.ItemNameID,D.GoldQualityID,D.LengthOrWidth,D.CurrentPrice,D.Design,D.ItemTK,D.ItemtG,D.DetailRemark, " & _
                                  " I.ItemCategory,N.ItemName,G.GoldQuality, G.IsGramRate, " & _
                                  " CAST(D.ItemTK AS INT) AS ItemK,CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                  " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS ItemY " & _
                                  " From tbl_RepairHeader H LEFT JOIN tbl_RepairDetail D on D.RepairID=H.RepairID " & _
                                  " LEFT JOIN tbl_Customer C on C.CustomerID=H.CustomerID LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID  " & _
                                  " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID " & _
                                  " LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=D.GoldQualityID Where H.RepairID=@RepairID AND H.IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, RepairID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try

        End Function


        Public Function CheckIsUseInRepairReturnHeader(RepairID As String) As Boolean Implements IRepairDA.CheckIsUseInRepairReturnHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from tbl_ReturnRepairHeader where RepairID='" & RepairID & "' And IsDelete=0 "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                If dtResult.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function DeleteRepairReceiveHeader(ByVal RepairID As String) As Boolean Implements IRepairDA.DeleteRepairReceiveHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "Update tbl_RepairHeader Set IsDelete=1  Where RepairID = @RepairID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, RepairID)
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

        Public Function GetBalaceAmountByReceiveID(RepairID As String) As DataTable Implements IRepairDA.GetBalaceAmountByReceiveID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As New DataTable
            Try
                strCommandText = "SELECT BalanceAmount FROM tbl_ReturnRepairHeader WHERE RepairID=@RepairID AND ReturnRepairID=(SELECT Max(ReturnRepairID) FROM tbl_ReturnRepairHeader WHERE RepairID=@RepairID) And IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@RepairID", DbType.String, RepairID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetRepairReceiveSummary(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements IRepairDA.GetRepairReceiveSummary
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " Select H.RepairID, H.RepairDate,H.ReturnDate,H.CustomerID,H.StaffID,H.Remark,H.AdvanceRepairAmount,  " & _
                                 " H.DueDate,C.CustomerName,C.CustomerAddress,C.CustomerTel,S.Staff,D.IsFromShop,  " & _
                                 " D.RepairDetailID,D.BarcodeNo,D.ItemNameID,D.ItemCategoryID,D.GoldQualityID,D.LengthOrWidth,D.CurrentPrice,D.Design,D.ItemTK,D.ItemTG,D.DetailRemark,  " & _
                                 " CAST(D.ItemTK AS INT) AS ItemK,CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT) AS ItemP,  " & _
                                 " CAST((((D.ItemTK-CAST(D.ItemTK AS INT))*16)-CAST((D.ItemTK-CAST(D.ItemTK AS INT))*16 AS INT))*'" & Global_PToY & "'  AS DECIMAL(18,2)) AS ItemY,  " & _
                                 " I.ItemCategory, CASE D.DetailRemark WHEN '' THEN N.ItemName ELSE D.DetailRemark END AS ItemName ,G.GoldQuality, H.RepairDate As [@RDate], '' As Others, 0 AS TotalAdvanceAmount   From tbl_RepairDetail D LEFT JOIN tbl_RepairHeader H on H.RepairID=D.RepairID  " & _
                                 " LEFT JOIN tbl_Customer C on C.CustomerID=H.CustomerID LEFT JOIN tbl_Staff S on S.StaffID=H.StaffID   " & _
                                 " LEFT JOIN tbl_ItemCategory I on I.ItemCategoryID=D.ItemCategoryID LEFT JOIN tbl_ItemName N on N.ItemNameID=D.ItemNameID  " & _
                                 " LEFT JOIN tbl_GoldQuality G on G.GoldQualityID=D.GoldQualityID  " & _
                                 " WHERE H.IsDelete=0 AND H.RepairDate BETWEEN @FromDate And @ToDate " & Cristr & " Order By RepairID  DESC "

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
        Public Function GetRepairStockDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairDetailInfo Implements IRepairDA.GetRepairStockDetailForTotal
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New CommonInfo.RepairDetailInfo
            Try
                strCommandText = " Select Sum(CAST((D.ItemTG) AS DECIMAL(18,3)))As ItemTG , Sum(D.ItemTK) As ItemTK " & _
                                 " From tbl_RepairDetail D LEFT JOIN tbl_RepairHeader H on H.RepairID=D.RepairID  " & criStr & "And IsDelete=0"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .ItemTG = drResult("ItemTG")
                        .ItemTK = drResult("ItemTK")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function
    End Class
End Namespace

