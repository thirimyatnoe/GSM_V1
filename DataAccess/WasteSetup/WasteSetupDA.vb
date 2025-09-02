Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace WasteSetup
    Public Class WasteSetupDA
        Implements IWasteSetupDA
#Region "Private WasteSetup"

        Private DB As Database
        Private Shared ReadOnly _instance As IWasteSetupDA = New WasteSetupDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IWasteSetupDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteWasteSetup(WasteSetupHeaderID As String) As Boolean Implements IWasteSetupDA.DeleteWasteSetup
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "UPDATE tbl_WasteSetupHeader  SET  IsDelete=1 WHERE  WasteSetupHeaderID= @WasteSetupHeaderID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WasteSetupHeaderID", DbType.String, WasteSetupHeaderID)
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

        Public Function DeleteWasteSetupDetail(WasteSetupDetailID As String) As Boolean Implements IWasteSetupDA.DeleteWasteSetupDetail
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_WasteSetupDetail WHERE  WasteSetupDetailID= @WasteSetupDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WasteSetupDetailID", DbType.String, WasteSetupDetailID)
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

        Public Function GetWasteSetup() As DataTable Implements IWasteSetupDA.GetWasteSetup
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = " select W.WasteSetupHeaderID, W.ItemCategoryID as [@ItemCategoryID], W.ItemNameID as [@ItemNameID],I.ItemCategory, N.ItemName from tbl_WasteSetupHeader W LEFT JOIN tbl_ItemCategory I ON W.ItemCategoryID=I.ItemCategoryID LEFT JOIN tbl_ItemName N ON W.ItemNameID=N.ItemNameID " & _
                                 " Where W.IsDelete =0 ORDER BY WasteSetupHeaderID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWasteSetupDetail(WasteSetupHeaderID As String) As DataTable Implements IWasteSetupDA.GetWasteSetupDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = " select WasteSetupDetailID,WasteSetupHeaderID,GoldQualityID as [@GoldQualityID]," & _
                         " CAST(MinNetWeightTK AS INT) AS MinNetWeightK," & _
                         " CAST((MinNetWeightTK-CAST(MinNetWeightTK AS INT))*16 AS INT) AS MinNetWeightP," & _
                         " CAST((((MinNetWeightTK-CAST(MinNetWeightTK AS INT))*16)-CAST((MinNetWeightTK-CAST(MinNetWeightTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS MinNetWeightY," & _
                         " CAST(MaxNetWeightTK AS INT) AS MaxNetWeightK," & _
                         " CAST((MaxNetWeightTK-CAST(MaxNetWeightTK AS INT))*16 AS INT) AS MaxNetWeightP," & _
                         " CAST((((MaxNetWeightTK-CAST(MaxNetWeightTK AS INT))*16)-CAST((MaxNetWeightTK-CAST(MaxNetWeightTK AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS MaxNetWeightY," & _
                         " CAST((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16 AS INT) AS MinWeightPForSale," & _
                         " CAST((((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16)-CAST((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,3)) AS MinWeightYForSale," & _
                         " MinWeightTKForSale,MinWeightTGForSale,MinNetWeightTK,MinNetWeightTG,MaxNetWeightTK,MaxNetWeightTG " & _
                         " from tbl_WasteSetupDetail where WasteSetupHeaderID= @WasteSetupHeaderID Order by GoldQualityID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WasteSetupHeaderID", DbType.String, WasteSetupHeaderID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetWasteSetupHeaderID(WasteSetupHeaderID As String) As WasteSetupHeaderInfo Implements IWasteSetupDA.GetWasteSetupHeaderID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objWasteSetupInfo As New WasteSetupHeaderInfo
            Try

                strCommandText = " select WasteSetupHeaderID,ItemCategoryID,ItemNameID from tbl_WasteSetupHeader where IsDelete=0 and WasteSetupHeaderID= @WasteSetupHeaderID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WasteSetupHeaderID", DbType.String, WasteSetupHeaderID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objWasteSetupInfo
                        .WasteSetupHeaderID = drResult("WasteSetupHeaderID")
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemNameID = drResult("ItemNameID")


                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objWasteSetupInfo
        End Function

        Public Function InsertWasteSetup(WasteSetupObj As WasteSetupHeaderInfo) As Boolean Implements IWasteSetupDA.InsertWasteSetup
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_WasteSetupHeader (WasteSetupHeaderID,ItemCategoryID,ItemNameID,IsUpload,IsDelete,LocationID,LastModifiedDate)"
                strCommandText += " Values (@WasteSetupHeaderID,@ItemCategoryID,@ItemNameID,0,0,@LocationID,getdate())"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WasteSetupHeaderID", DbType.String, WasteSetupObj.WasteSetupHeaderID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, WasteSetupObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, WasteSetupObj.ItemNameID)
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

        Public Function UpdateWasteSetup(WasteSetupObj As WasteSetupHeaderInfo) As Boolean Implements IWasteSetupDA.UpdateWasteSetup
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
               

                strCommandText = "Update tbl_WasteSetupHeader set WasteSetupHeaderID=@WasteSetupHeaderID ,ItemCategoryID=@ItemCategoryID,ItemNameID=@ItemNameID,LocationID=@LocationID,IsUpload=0,LastModifiedDate=getdate()"
                strCommandText += " where WasteSetupHeaderID= @WasteSetupHeaderID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WasteSetupHeaderID", DbType.String, WasteSetupObj.WasteSetupHeaderID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, WasteSetupObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, WasteSetupObj.ItemNameID)
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

        Public Function InsertWasteSetupDetail(WasteSetupDetailObj As WasteSetupDetailInfo) As Boolean Implements IWasteSetupDA.InsertWasteSetupDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_WasteSetupDetail (WasteSetupHeaderID,WasteSetupDetailID,GoldQualityID,MinNetWeightTK,MinNetWeightTG,MaxNetWeightTK,MaxNetWeightTG,MinWeightTKForSale,MinWeightTGForSale )"
                strCommandText += " Values (@WasteSetupHeaderID,@WasteSetupDetailID,@GoldQualityID,@MinNetWeightTK,@MinNetWeightTG,@MaxNetWeightTK,@MaxNetWeightTG,@MinWeightTKForSale,@MinWeightTGForSale)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@WasteSetupHeaderID", DbType.String, WasteSetupDetailObj.WasteSetupHeaderID)
                DB.AddInParameter(DBComm, "@WasteSetupDetailID", DbType.String, WasteSetupDetailObj.WasteSetupDetailID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, WasteSetupDetailObj.GoldQualityID)
                DB.AddInParameter(DBComm, "@MinNetWeightTK", DbType.Decimal, WasteSetupDetailObj.MinNetWeightTK)
                DB.AddInParameter(DBComm, "@MinNetWeightTG", DbType.Decimal, WasteSetupDetailObj.MinNetWeightTG)
                DB.AddInParameter(DBComm, "@MaxNetWeightTK", DbType.Decimal, WasteSetupDetailObj.MaxNetWeightTK)
                DB.AddInParameter(DBComm, "@MaxNetWeightTG", DbType.Decimal, WasteSetupDetailObj.MaxNetWeightTG)
                DB.AddInParameter(DBComm, "@MinWeightTKForSale", DbType.Decimal, WasteSetupDetailObj.MinWeightTKForSale)
                DB.AddInParameter(DBComm, "@MinWeightTGForSale", DbType.Decimal, WasteSetupDetailObj.MinWeightTGForSale)

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


        Public Function UpdateWasteSetupDetail(ObjDetail As WasteSetupDetailInfo) As Boolean Implements IWasteSetupDA.UpdateWasteSetupDetail
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_WasteSetupDetail set  WasteSetupHeaderID=@WasteSetupHeaderID,WasteSetupDetailID= @WasteSetupDetailID,GoldQualityID=@GoldQualityID,MinNetWeightTK=@MinNetWeightTK,MinNetWeightTG=@MinNetWeightTG,MaxNetWeightTK=@MaxNetWeightTK,MaxNetWeightTG=@MaxNetWeightTG,MinWeightTKForSale= @MinWeightTKForSale,MinWeightTGForSale= @MinWeightTGForSale "
                strCommandText += " where WasteSetupDetailID= @WasteSetupDetailID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@WasteSetupHeaderID", DbType.String, ObjDetail.WasteSetupHeaderID)
                DB.AddInParameter(DBComm, "@WasteSetupDetailID", DbType.String, ObjDetail.WasteSetupDetailID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, ObjDetail.GoldQualityID)
                DB.AddInParameter(DBComm, "@MinNetWeightTK", DbType.Decimal, ObjDetail.MinNetWeightTK)
                DB.AddInParameter(DBComm, "@MinNetWeightTG", DbType.Decimal, ObjDetail.MinNetWeightTG)
                DB.AddInParameter(DBComm, "@MaxNetWeightTK", DbType.Decimal, ObjDetail.MaxNetWeightTK)
                DB.AddInParameter(DBComm, "@MaxNetWeightTG", DbType.Decimal, ObjDetail.MaxNetWeightTG)
                DB.AddInParameter(DBComm, "@MinWeightTKForSale", DbType.Decimal, ObjDetail.MinWeightTKForSale)
                DB.AddInParameter(DBComm, "@MinWeightTGForSale", DbType.Decimal, ObjDetail.MinWeightTGForSale)

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

        Public Function GetWasetSetupInfoByStockWeight(ByVal ItemTK As Decimal, ByVal ItemCategoryID As String, ByVal ItemNameID As String, ByVal GoldQualityID As String) As CommonInfo.WasteSetupDetailInfo Implements IWasteSetupDA.GetWasetSetupInfoByStockWeight
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objWasteSetupDetailInfo As New WasteSetupDetailInfo
            Try
                strCommandText = " select H.WasteSetupHeaderID, MinWeightTKForSale,MinWeightTGForSale,  " & _
                                 " CAST((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16 AS INT) AS MinWeightPForSale," & _
                                 " CAST((((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16)-CAST((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16 AS INT))*'" & Global_PToY & "' AS DECIMAL(18,2)) AS MinWeightYForSale, " & _
                                 " CAST(((((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16)-CAST((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16 AS INT))*'" & Global_PToY & "')-CAST((((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16)-CAST((MinWeightTKForSale-CAST(MinWeightTKForSale AS INT))*16 AS INT))*'" & Global_PToY & "' AS INT)AS DECIMAL(18,3)) AS MinWeightCForSale " & _
                                 " from tbl_WasteSetupHeader H inner join tbl_WasteSetupDetail D ON H.WasteSetupHeaderID=D.WasteSetupHeaderID where ('" & ItemTK & "' between MinNetWeightTK And MaxNetWeightTK) And ItemNameID=@ItemNameID And ItemCategoryID=@ItemCategoryID And GoldQualityID=@GoldQualityID  And H.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameID)
                DB.AddInParameter(DBComm, "@GoldQualityID", DbType.String, GoldQualityID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objWasteSetupDetailInfo
                        .WasteSetupHeaderID = drResult("WasteSetupHeaderID")
                        .MinWeightTKForSale = IIf(IsDBNull(drResult("MinWeightTKForSale")), 0, drResult("MinWeightTKForSale"))
                        .MinWeightTGForSale = IIf(IsDBNull(drResult("MinWeightTGForSale")), 0, drResult("MinWeightTGForSale"))
                        .MinWeightPForSale = IIf(IsDBNull(drResult("MinWeightPForSale")), 0, drResult("MinWeightPForSale"))
                        .MinWeightYForSale = IIf(IsDBNull(drResult("MinWeightYForSale")), 0, drResult("MinWeightYForSale"))
                        .MinWeightCForSale = IIf(IsDBNull(drResult("MinWeightCForSale")), 0, drResult("MinWeightCForSale"))
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objWasteSetupDetailInfo
        End Function
    End Class
End Namespace

