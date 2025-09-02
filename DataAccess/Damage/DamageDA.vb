Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Damage
    Public Class DamageDA
        Implements IDamageDA

#Region "Private Damage"

        Private DB As Database
        Private Shared ReadOnly _instance As IDamageDA = New DamageDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDamageDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteDamage(ByVal DamageID As String) As Boolean Implements IDamageDA.DeleteDamage
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                'strCommandText = "  Update tbl_ForSale Set IsExit=0 Where ForSaleID In (Select ForSaleID From tbl_DamageItem Where DamageID= @DamageID)"
                strCommandText = "DELETE FROM tbl_Damage WHERE  DamageID= @DamageID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, DamageID)
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

        Public Function InsertDamage(ByVal DamageObj As CommonInfo.DamageInfo) As Boolean Implements IDamageDA.InsertDamage
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_Damage ( DamageID,DDate,Remark,LastModifiedLoginUserName,LastModifiedDate,LocationID,SaleStaffID)"
                strCommandText += " Values (@DamageID,@DDate,@Remark,@LastModifiedLoginUserName,GetDate(),@LocationID,@SaleStaffID)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, DamageObj.DamageID)
                DB.AddInParameter(DBComm, "@DDate", DbType.Date, DamageObj.DDate)
                'DB.AddInParameter(DBComm, "@CounterID", DbType.String, DamageObj.CounterID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DamageObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@SaleStaffID", DbType.String, DamageObj.SaleStaffID)

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

        Public Function UpdateDamage(ByVal DamageObj As CommonInfo.DamageInfo) As Boolean Implements IDamageDA.UpdateDamage
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_Damage set   DDate= @DDate , Remark= @Remark , LastModifiedLoginUserName= @LastModifiedLoginUserName , LastModifiedDate= GetDate(),LocationID= @LocationID, SaleStaffID=@SaleStaffID "
                strCommandText += " where DamageID= @DamageID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, DamageObj.DamageID)
                DB.AddInParameter(DBComm, "@DDate", DbType.Date, DamageObj.DDate)
                'DB.AddInParameter(DBComm, "@CounterID", DbType.String, DamageObj.CounterID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, DamageObj.Remark)
                DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                DB.AddInParameter(DBComm, "@SaleStaffID", DbType.String, DamageObj.SaleStaffID)

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
        Public Function UpdateReadded(ByVal DamageObj As CommonInfo.DamageItemInfo) As Boolean Implements IDamageDA.UpdateReadded
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_DamageItem set   ReAddDate= @ReAddDate "
                strCommandText += " where ForSaleID= @ForSaleID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, DamageObj.ForSaleID)
                DB.AddInParameter(DBComm, "@ReAddDate", DbType.Date, DamageObj.ReAddDate)
                'DB.AddInParameter(DBComm, "@CounterID", DbType.String, DamageObj.CounterID)
                'DB.AddInParameter(DBComm, "@Remark", DbType.String, DamageObj.Remark)
                'DB.AddInParameter(DBComm, "@LastModifiedLoginUserName", DbType.String, Global_UserID)
                'DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                'DB.AddInParameter(DBComm, "@SaleStaffID", DbType.String, DamageObj.SaleStaffID)

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

        ''Public Function GetAllDamage() As System.Data.DataTable Implements IDamageDA.GetAllDamage
        ''    Dim strCommandText As String
        ''    Dim DBComm As DbCommand
        ''    Dim dtResult As DataTable
        ''    Try
        ''        strCommandText = "Select D.DamageID as [@DamageID],convert(varchar(10),D.DDate,105) as DamageDate,L.Location,S.Staff as [Name_],F.ItemCode, F.ItemName AS [ItemName_],F.ForSaleID AS [@ForSaleID],F.GoldQualityID as [@GoldQualityID],  IsNull(F.Length,'-') as Length,IsNull(F.Width,'-') as Width , F.TotalTG ," & _
        ''        " D.Remark as [Remark_] " & _
        ''        " from  tbl_Damage D Inner Join tbl_DamageItem DI On D.DamageID=DI.DamageID " & _
        ''        " Left Join tbl_ForSale F On F.ForSaleID=DI.ForSaleID " & _
        ''        " Left Join tbl_Location L ON D.LocationID=L.LocationID " & _
        ''        " Left Join tbl_Staff S On D.SaleStaffID=S.StaffID "
        ''        '" group by D.DamageID,L.Location,S.Staff,D.Remark,D.DDate " & _
        ''        '" order by D.DDate "
        ''        '" CAST(Sum(F.TotalNoWasteTK) AS INT) AS TotalNoWasteK," & _
        ''        '" CAST((Sum(F.TotalNoWasteTK)-CAST(Sum(F.TotalNoWasteTK) AS INT))*16 AS INT) AS TotalNoWasteP," & _
        ''        '" CAST((((Sum(F.TotalNoWasteTK)-CAST(Sum(F.TotalNoWasteTK) AS INT))*16)-CAST((Sum(F.TotalNoWasteTK)-CAST(Sum(F.TotalNoWasteTK) AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS TotalNoWasteY," & _
        ''        DBComm = DB.GetSqlStringCommand(strCommandText)
        ''        dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
        ''        Return dtResult
        ''    Catch ex As Exception
        ''        MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
        ''        Return New DataTable
        ''    End Try
        ''End Function

        Public Function GetAllDamage() As System.Data.DataTable Implements IDamageDA.GetAllDamage
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "Select D.DamageID ,convert(varchar(10),D.DDate,105) as DamageDate,L.Location,S.Staff as [StaffName_]" & _
                " from  tbl_Damage D Left Join tbl_Location L ON D.LocationID=L.LocationID  " & _
                " Left Join tbl_Staff S On D.SaleStaffID=S.StaffID "
                '" group by D.DamageID,L.Location,S.Staff,D.Remark,D.DDate " & _
                '" order by D.DDate "
                '" CAST(Sum(F.TotalNoWasteTK) AS INT) AS TotalNoWasteK," & _
                '" CAST((Sum(F.TotalNoWasteTK)-CAST(Sum(F.TotalNoWasteTK) AS INT))*16 AS INT) AS TotalNoWasteP," & _
                '" CAST((((Sum(F.TotalNoWasteTK)-CAST(Sum(F.TotalNoWasteTK) AS INT))*16)-CAST((Sum(F.TotalNoWasteTK)-CAST(Sum(F.TotalNoWasteTK) AS INT))*16 AS INT))*8 AS Decimal(18,3)) AS TotalNoWasteY," & _
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetDamage(ByVal DamageID As String) As CommonInfo.DamageInfo Implements IDamageDA.GetDamage
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim obj As New DamageInfo
            Try
                strCommandText = " SELECT  *  FROM tbl_Damage WHERE DamageID= @DamageID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, DamageID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With obj
                        .DamageID = drResult("DamageID")
                        .DDate = drResult("DDate")
                        '.CustomerID = drResult("CustomerID")
                        '.LocationID = drResult("LocationID")
                        '.LocationID = drResult("LocationID")
                        '.CounterID = drResult("CounterID")
                        .Remark = drResult("Remark")
                        .SaleStaffID = drResult("SaleStaffID")


                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return obj
        End Function

        Public Function DeleteDamageItem(ByVal DamageItemID As String) As Boolean Implements IDamageDA.DeleteDamageItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                'strCommandText = " Update tbl_ForSale Set IsExit=0 Where ForSaleID=(Select ForSaleID from tbl_DamageItem Where DamageItemID= @DamageItemID ) "
                strCommandText = " DELETE FROM tbl_DamageItem WHERE  DamageItemID= @DamageItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageItemID", DbType.String, DamageItemID)
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

        Public Function GetDamageItem(ByVal DamageID As String) As System.Data.DataTable Implements IDamageDA.GetDamageItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT I.DamageItemID, H.DamageID, I.ForSaleID, S.ItemCode, N.ItemName as [ItemName_], S.Length,S.Width, S.TotalTG as GoldTG, S.TotalTK As GoldTK, G.GoldQuality, C.ItemCategory as [ItemCategory_], " & _
                     " CAST(TotalTK AS INT) AS GoldK," & _
                     " CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT) AS GoldP, " & _
                     " CAST((((TotalTK-CAST(TotalTK AS INT))*16)-CAST((TotalTK-CAST(TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY " & _
                     " FROM tbl_Damage H INNER JOIN tbl_DamageItem I ON H.DamageID=I.DamageID " & _
                     " INNER JOIN tbl_ForSale S ON S.ForSaleID=I.ForSaleID " & _
                     " LEFT JOIN tbl_GoldQuality G ON S.GoldQualityID=G.GoldQualityID  " & _
                     " LEFT JOIN tbl_ItemCategory C ON S.ItemCategoryID=C.ItemCategoryID" & _
                     " LEFT JOIN tbl_ItemName N ON N.ItemNameID=S.ItemNameID " & _
                     " WHERE H.DamageID=@DamageID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, DamageID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertDamageItem(ByVal ObjDmgItem As CommonInfo.DamageItemInfo) As Boolean Implements IDamageDA.InsertDamageItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_DamageItem ( DamageItemID,DamageID,ForSaleID,IsReAdd)"
                strCommandText += " Values (@DamageItemID,@DamageID,@ForSaleID,0) "
                strCommandText += " Update tbl_ForSale Set IsExit=1,ExitDate=@ExitDate WHere ForSaleID=@ForSaleID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageItemID", DbType.String, ObjDmgItem.DamageItemID)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, ObjDmgItem.DamageID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ObjDmgItem.ForSaleID)
                DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, Now)

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

        Public Function UpdateDamageItem(ByVal ObjDmgItem As CommonInfo.DamageItemInfo) As Boolean Implements IDamageDA.UpdateDamageItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " Update tbl_ForSale Set IsExit =0 Where ForSaleID=(Select ForSaleID from tbl_DamageItem Where DamageItemID= @DamageItemID And DamageID= @DamageID )"
                strCommandText += " Update tbl_DamageItem set DamageID= @DamageID , ForSaleID= @ForSaleID "
                strCommandText += " where DamageItemID= @DamageItemID"
                strCommandText += " Update tbl_ForSale Set IsExit=1,ExitDate=@ExitDate WHere ForSaleID=@ForSaleID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageItemID", DbType.String, ObjDmgItem.DamageItemID)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, ObjDmgItem.DamageID)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ObjDmgItem.ForSaleID)
                DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, Now)

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

        Public Function UpdateReaddedItem(ByVal ObjDmgItem As CommonInfo.DamageItemInfo) As Boolean Implements IDamageDA.UpdateReaddedItem
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                'strCommandText = " Update tbl_ForSale Set IsExit =0 Where ForSaleID=@ForSaleID"
                strCommandText = " Update tbl_DamageItem set ForSaleID= @ForSaleID ,IsReAdd=1, Remark=@Remark ,ReAddDate=@ReAddDate "
                strCommandText += " where ForSaleID= @ForSaleID"
                'strCommandText += " Update tbl_ForSale Set IsExit=1,ExitDate=@ExitDate WHere ForSaleID=@ForSaleID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                'DB.AddInParameter(DBComm, "@DamageItemID", DbType.String, ObjDmgItem.DamageItemID)
                DB.AddInParameter(DBComm, "@ReAddDate", DbType.Date, ObjDmgItem.ReAddDate)
                DB.AddInParameter(DBComm, "@ForSaleID", DbType.String, ObjDmgItem.ForSaleID)
                DB.AddInParameter(DBComm, "@Remark", DbType.String, ObjDmgItem.Remark)
                'DB.AddInParameter(DBComm, "@ExitDate", DbType.Date, Now)

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
        Public Function GetDamageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IDamageDA.GetDamageReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT DDate,D.LocationID,L.Location,S.GoldQualityID, GoldQuality, S.ItemCategoryID, ItemCategory, S.ItemCode, N.ItemName, S.Length, S.Width, ST.Staff, " & _
                " S.GoldTG, S.GemsTG, S.WasteTG, S.TotalTG, D.Remark " & _
                " FROM tbl_Damage D INNER JOIN tbl_DamageItem DI ON D.DamageID=DI.DamageID " & _
                " LEFT JOIN tbl_Location L ON D.LocationID=L.LocationID " & _
                " LEFT JOIN tbl_ForSale S ON DI.ForSaleID=S.ForSaleID " & _
                " LEFT JOIN tbl_GoldQuality G ON S.GoldQualityID=G.GoldQualityID " & _
                " LEFT JOIN tbl_ItemCategory I ON S.ItemCategoryID=I.ItemCategoryID" & _
                " LEFT JOIN tbl_Staff ST On D.SaleStaffID=ST.StaffID" & _
                " LEFT JOIN tbl_ItemName N on N.ItemNameID=S.ItemNameID" & _
                " WHERE DDate BETWEEN @FromDate AND @ToDate " & cristr & " Order by S.ItemCode"
                ''"CAST(S.GoldTG AS INT) AS GoldG," & _
                ''"CAST(S.GemsTG AS INT) AS GemsG," & _
                ''"CAST(S.WasteTG AS INT) AS WasteG," & _
                ''"CAST(S.TotalTG AS INT) AS TotalG," & _
                ''" CAST(S.GoldTK AS INT) AS GoldK," & _
                ''" CAST((S.GoldTK-CAST(S.GoldTK AS INT))*16 AS INT) AS GoldP," & _
                ''" CAST((((S.GoldTK-CAST(S.GoldTK AS INT))*16)-CAST((S.GoldTK-CAST(S.GoldTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GoldY," & _
                ''" CAST(S.GemsTK AS INT) AS GemsK," & _
                ''" CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT) AS GemsP," & _
                ''" CAST((((S.GemsTK-CAST(S.GemsTK AS INT))*16)-CAST((S.GemsTK-CAST(S.GemsTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS GemsY," & _
                ''" CAST(S.WasteTK AS INT) AS WasteK," & _
                ''" CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT) AS WasteP," & _
                ''" CAST((((S.WasteTK-CAST(S.WasteTK AS INT))*16)-CAST((S.WasteTK-CAST(S.WasteTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS WasteY," & _
                ''" CAST(S.TotalTK AS INT) AS TotalK," & _
                ''" CAST((S.TotalTK-CAST(S.TotalTK AS INT))*16 AS INT) AS TotalP," & _
                ''" CAST((((S.TotalTK-CAST(S.TotalTK AS INT))*16)-CAST((S.TotalTK-CAST(S.TotalTK AS INT))*16 AS INT))*8 AS DECIMAL(18,3)) AS TotalY," & _
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


        Public Function GetDamageSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As System.Data.DataTable Implements IDamageDA.GetDamageSummaryByGoldQualityAndItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT COUNT(I.ForSaleID) AS QTY, ISNULL(SUM(S.TotalNoWasteTK),0) AS GoldTK " & _
                " FROM tbl_Damage H INNER JOIN tbl_DamageItem I ON H.DamageID=I.DamageID " & _
                " INNER JOIN tbl_ForSale S ON I.ForSaleID=S.ForSaleID " & _
                " WHERE S.ItemCategoryID=@ItemCategoryID AND S.GoldQualityID=@GoldQualityID AND H.DDate=@ForDate"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
                DB.AddInParameter(DBComm, "@ForDate", DbType.Date, ForDate)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllDamageFromSearchBox() As System.Data.DataTable Implements IDamageDA.GetAllDamageFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT DamageID, CONVERT(VARCHAR(10),DDate,105) AS DamageDate, L.Location, C.Counter, Remark as [Remark_],ST.Name as [Name_] " & _
                " FROM tbl_Damage D " & _
                " LEFT JOIN tbl_Location L ON D.LocationID=L.LocationID" & _
                " LEFT JOIN tbl_Counter C ON D.CounterID=C.CounterID " & _
                 " LEFT JOIN tbl_StaffByLocation ST On D.SaleStaffID=ST.SaleStaffID" & _
                " ORDER BY DamageID DESC"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetForSaleIDByDamageID(ByVal DamageID As String) As System.Data.DataTable Implements IDamageDA.GetForSaleIDByDamageID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * from tbl_DamageItem WHERE DamageID=@DamageID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@DamageID", DbType.String, DamageID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class

End Namespace

