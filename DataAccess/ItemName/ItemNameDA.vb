Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace ItemName
    Public Class ItemNameDA
        Implements IItemNameDA
#Region "Private ItemName"

        Private DB As Database
        Private Shared ReadOnly _instance As IItemNameDA = New ItemNameDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IItemNameDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteItemName(ByVal ItemNameID As String) As Boolean Implements IItemNameDA.DeleteItemName
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_ItemName SET IsDelete =1 WHERE  ItemNameID= @ItemNameID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameID)
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

        Public Function GetItemName(ByVal ItemNameID As String) As CommonInfo.ItemNameInfo Implements IItemNameDA.GetItemName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objItemNameInfo As New ItemNameInfo
            Try
                strCommandText = " select ItemNameID,ItemName from tbl_ItemName where ItemNameID= @ItemNameID and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objItemNameInfo
                        .ItemNameID = drResult("ItemNameID")
                        .ItemName = drResult("ItemName")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objItemNameInfo
        End Function

        Public Function GetItemNameList() As System.Data.DataTable Implements IItemNameDA.GetItemNameList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ItemNameID, ItemCategory AS [ItemCategory_], ItemName AS [ItemName_] " & _
                " FROM tbl_ItemName I INNER JOIN tbl_ItemCategory C ON I.ItemCategoryID=C.ItemCategoryID where C.IsDelete=0" & _
                " ORDER BY ItemNameID "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertItemName(ByVal ItemNameObj As CommonInfo.ItemNameInfo) As Boolean Implements IItemNameDA.InsertItemName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ItemName (ItemNameID,ItemName,ItemCategoryID,IsDelete,IsUpload,LocationID,LastModifiedDate)"
                strCommandText += " Values (@ItemNameID,@ItemName,@ItemCategoryID,0,0,@LocationID,getdate())"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameObj.ItemNameID)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemNameObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ItemNameObj.ItemName)
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

        Public Function InsertItemPhoto(ByVal ItemNameObj As CommonInfo.ItemNameInfo) As Boolean Implements IItemNameDA.InsertItemPhoto
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ItemName (ItemCategoryID ,ItemPhoto)"
                strCommandText += " Values (@ItemCategoryID,@ItemPhoto)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemNameObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemPhoto", DbType.Binary, ItemNameObj.ItemPhoto)

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

        Public Function UpdateItemName(ByVal ItemNameObj As CommonInfo.ItemNameInfo) As Boolean Implements IItemNameDA.UpdateItemName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ItemName set  ItemCategoryID=@ItemCategoryID, ItemName=@ItemName ,LocationID=@LocationID,IsUpload=0,LastModifiedDate=getdate() "
                strCommandText += " where ItemNameID= @ItemNameID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemNameObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ItemNameObj.ItemName)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameObj.ItemNameID)
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

        Public Function UpdateItemPhoto(ByVal ItemNameObj As CommonInfo.ItemNameInfo) As Boolean Implements IItemNameDA.UpdateItemPhoto
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ItemName set ItemCategoryID=@ItemCategoryID, ItemPhoto=@ItemPhoto "
                strCommandText += " where ItemNameID= @ItemNameID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemNameObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameObj.ItemNameID)
                DB.AddInParameter(DBComm, "@ItemPhoto", DbType.Binary, ItemNameObj.ItemPhoto)

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

        Public Function GetItemNameID(ByVal ItemName As String) As CommonInfo.ItemNameInfo Implements IItemNameDA.GetItemNameID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objItemNameInfo As New ItemNameInfo
            Try
                strCommandText = " select ItemNameID,ItemName from tbl_ItemName where ItemName= @ItemName and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ItemName)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objItemNameInfo
                        .ItemNameID = drResult("ItemNameID")
                        .ItemName = drResult("ItemName")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objItemNameInfo
        End Function

        Public Function GetItemNameListByItemCategory(ByVal ItemCategoryID As String) As System.Data.DataTable Implements IItemNameDA.GetItemNameListByItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ItemNameID, ItemCategory as [ItemCategory_], ItemName as [ItemName_]" & _
                " FROM tbl_ItemName I INNER JOIN tbl_ItemCategory C ON I.ItemCategoryID=C.ItemCategoryID " & _
                " WHERE C.ItemCategoryID=@ItemCategoryID and C.IsDelete=0" & _
                " ORDER BY ItemName ASC "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetItemID(ByVal ItemID As String) As System.Data.DataTable Implements IItemNameDA.GetItemID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ItemNameID AS [@ItemNameID] , ItemCategory, ItemName " & _
                " FROM tbl_ItemName I INNER JOIN tbl_ItemCategory C ON I.ItemCategoryID=C.ItemCategoryID " & _
                " WHERE I.ItemNameID=@ItemID and C.IsDelete=0 " & _
                " ORDER BY ItemNameID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetItemNamePhoto(ByVal ItemNameID As String) As CommonInfo.ItemNameInfo Implements IItemNameDA.GetItemNamePhoto
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objItemNameInfo As New ItemNameInfo
            Try
                strCommandText = " select ItemPhoto from tbl_ItemName where ItemNameID= @ItemNameID and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objItemNameInfo
                        .ItemPhoto = drResult("ItemPhoto")
                        '.ItemName = drResult("ItemName")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objItemNameInfo
        End Function

        Public Function GetItemNameByCategory(ByVal ItemCategoryID As String) As CommonInfo.ItemNameInfo Implements IItemNameDA.GetItemNameByCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objItemNameInfo As New ItemNameInfo
            Try
                strCommandText = "SELECT ItemNameID AS [@ItemNameID] " & _
                " FROM tbl_ItemName I INNER JOIN tbl_ItemCategory C ON I.ItemCategoryID=C.ItemCategoryID " & _
                " WHERE C.ItemCategoryID=@ItemCategoryID and C.IsDelete=0 "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objItemNameInfo
                        .ItemNameID = drResult("@ItemNameID")


                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objItemNameInfo
        End Function

        Public Function GetItemPhoto(ByVal ItemNameID As String) As CommonInfo.ItemNameInfo Implements IItemNameDA.GetItemPhoto
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objItemNameInfo As New ItemNameInfo
            Try
                strCommandText = " select ItemPhoto from tbl_ItemName where ItemNameID= @ItemNameID and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameID)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objItemNameInfo
                        .ItemPhoto = drResult("ItemPhoto")

                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objItemNameInfo
        End Function

        Public Function GetrptItemName() As DataTable Implements IItemNameDA.GetrptItemName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT N.ItemNameID, N.ItemCategoryID, C.ItemCategory, N.ItemName, N.ItemPhoto FROM tbl_ItemName N Left Join tbl_ItemCategory C on C.ItemCategoryID=N.ItemCategoryID where N.IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetAllItemName() As DataTable Implements IItemNameDA.GetAllItemName
            Dim strcommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strcommandText = "SELECT ItemNameID as [@ItemNameID],  ItemCategoryID as [@ItemCategoryID], ItemName As [ItemName_] FROM tbl_ItemName where IsDelete = 0 ORDER BY ItemNameID"
                DBComm = DB.GetSqlStringCommand(strcommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetItemNameByItemName(ByVal ItemName As String, ByVal ItemCategoryID As String) As DataTable Implements IItemNameDA.GetItemNameByItemName
            Dim strcommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strcommandText = "SELECT * FROM tbl_ItemName WHERE ItemName=@ItemName AND ItemCategoryID=@ItemCategoryID And IsDelete = 0"
                DBComm = DB.GetSqlStringCommand(strcommandText)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ItemName)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetItemNameByIDForUpdate(ByVal ItemNameID As String, ByVal ItemName As String, ByVal ItemCategoryID As String) As DataTable Implements IItemNameDA.GetItemNameByIDForUpdate
            Dim strcommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strcommandText = "SELECT * FROM tbl_ItemName WHERE ItemNameID<>@ItemNameID AND ItemName=@ItemName AND ItemCategoryID=@ItemCategoryID AND IsDelete = 0"
                DBComm = DB.GetSqlStringCommand(strcommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, ItemName)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasPrefixForUpdateUseItemCode(ByVal ItemNameID As String) As System.Data.DataTable Implements IItemNameDA.HasPrefixForUpdateUseItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "  select * from  tbl_ItemName  C inner join tbl_ForSale  F on C.ItemNameID=F.ItemNameID "
                strCommandText += " where F.ItemNameID=@ItemNameID and C.IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemNameID", DbType.String, ItemNameID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

