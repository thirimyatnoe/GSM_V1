

Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace ItemCategory
    Public Class ItemCategoryDA
        Implements IItemCategoryDA


#Region "Private ItemCategory"

        Private DB As Database
        Private Shared ReadOnly _instance As IItemCategoryDA = New ItemCategoryDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IItemCategoryDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteItemCategory(ByVal ItemCategoryID As String) As Boolean Implements IItemCategoryDA.DeleteItemCategory
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_ItemCategory Set IsDelete=1 WHERE  ItemCategoryID= @ItemCategoryID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)
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

        Public Function GetAllItemCategory() As System.Data.DataTable Implements IItemCategoryDA.GetAllItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim OrderBy As String = ""
            Try
                If Global_IsCash = False Then
                    OrderBy = " ORDER By ItemCategory DESC"
                Else
                    OrderBy = " ORDER By ItemCategoryID ASC"
                End If

                strCommandText = "SELECT ItemCategoryID as [@ItemCategoryID],ItemCategory AS [ItemCategory_],Prefix,ItemTaxPer From tbl_ItemCategory where IsDelete=0 " & OrderBy

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllItemCategoryByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IItemCategoryDA.GetAllItemCategoryByLocation
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim OrderBy As String = ""
            Try
                If Global_IsCash = False Then
                    OrderBy = " ORDER By ItemCategory DESC"
                Else
                    OrderBy = " ORDER By ItemCategoryID ASC"
                End If

                strCommandText = "SELECT ItemCategoryID as [@ItemCategoryID],ItemCategory AS [ItemCategory_],Prefix,ItemTaxPer From tbl_ItemCategory where IsDelete=0  and LocationID= @LocationID " & OrderBy

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, LocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetItemCategory(ByVal ItemCategoryID As String) As CommonInfo.ItemCategoryInfo Implements IItemCategoryDA.GetItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objItemCategory As New ItemCategoryInfo
            Try
                strCommandText = "  SELECT ItemCategoryID,ItemCategory,Prefix,ItemTaxPer FROM tbl_ItemCategory WHERE ItemCategoryID = @ItemCategoryID and IsDelete = 0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objItemCategory
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemCategory = drResult("ItemCategory")
                        .Prefix = drResult("Prefix")
                        .ItemTaxPer = Format(drResult("ItemTaxPer"), "###,##0.##")
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objItemCategory
        End Function

        Public Function InsertItemCategory(ByVal ItemCategoryObj As CommonInfo.ItemCategoryInfo) As Boolean Implements IItemCategoryDA.InsertItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_ItemCategory ( ItemCategoryID,ItemCategory,Prefix,ItemTaxPer,IsDelete,IsUpload,LocationID,LastModifiedDate)"
                strCommandText += " Values (@ItemCategoryID,@ItemCategory,@Prefix,@ItemTaxPer,0,0,@LocationID,getdate())"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemCategory", DbType.String, ItemCategoryObj.ItemCategory)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, ItemCategoryObj.Prefix)
                DB.AddInParameter(DBComm, "@ItemTaxPer", DbType.String, ItemCategoryObj.ItemTaxPer)
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

        Public Function UpdateItemCategory(ByVal ItemCategoryObj As CommonInfo.ItemCategoryInfo) As Boolean Implements IItemCategoryDA.UpdateItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Update tbl_ItemCategory set  ItemCategoryID= @ItemCategoryID , ItemCategory= @ItemCategory ,Prefix=@Prefix,ItemTaxPer=@ItemTaxPer,LocationID=@LocationID,IsUpload=0,LastModifiedDate=getdate() "
                strCommandText += " where ItemCategoryID= @ItemCategoryID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryObj.ItemCategoryID)
                DB.AddInParameter(DBComm, "@ItemCategory", DbType.String, ItemCategoryObj.ItemCategory)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, ItemCategoryObj.Prefix)
                DB.AddInParameter(DBComm, "@ItemTaxPer", DbType.String, ItemCategoryObj.ItemTaxPer)
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

        Public Function GetItemCategoryName(ByVal ItemCategory As String) As CommonInfo.ItemCategoryInfo Implements IItemCategoryDA.GetItemCategoryName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objItemCategory As New ItemCategoryInfo
            Try
                strCommandText = "  SELECT ItemCategoryID,ItemCategory FROM tbl_ItemCategory WHERE ItemCategory = @ItemCategory and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategory", DbType.String, ItemCategory)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objItemCategory
                        .ItemCategoryID = drResult("ItemCategoryID")
                        .ItemCategory = drResult("ItemCategory")

                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objItemCategory
        End Function

        Public Function GetAllItemCategoryFromSearchBox() As System.Data.DataTable Implements IItemCategoryDA.GetAllItemCategoryFromSearchBox
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ItemCategoryID,ItemCategory AS [ItemCategory_],Prefix,ItemTaxPer From tbl_ItemCategory where IsDelete= 0 Order by ItemCategoryID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasItemCategory(ByVal ItemCategoryName As String, ByVal Prefix As String) As DataTable Implements IItemCategoryDA.HasItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_ItemCategory  "
                strCommandText += " where (ItemCategory= @ItemCategory and Prefix=@Prefix) or ItemCategory= @ItemCategory or Prefix=@Prefix and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategory", DbType.String, ItemCategoryName)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, Prefix)


                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function HasItemCategoryForUpdate(ByVal ItemCategoryName As String, ByVal OldItemCategory As String, ByVal OldPrefix As String, ByVal OldTax As Integer) As System.Data.DataTable Implements IItemCategoryDA.HasItemCategoryForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_ItemCategory where IsDelete=0 "
                strCommandText += " and (ItemCategory= @ItemCategory )  and ItemCategory <> '" & OldItemCategory & "' and Prefix<>'" & OldPrefix & "'"
                strCommandText += "and ItemTaxPer <> ' " & OldTax & " '"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategory", DbType.String, ItemCategoryName)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasPrefixForUpdate(ByVal prefix As String, ByVal OldPrefix As String) As System.Data.DataTable Implements IItemCategoryDA.HasPrefixForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_ItemCategory where IsDelete=0 "
                strCommandText += " and (Prefix= @Prefix )   and Prefix<>'" & OldPrefix & "'"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, prefix)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function HasTaxForUpdate(ByVal Tax As String, ByVal OldTax As String) As System.Data.DataTable Implements IItemCategoryDA.HasTaxForUpdate
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_ItemCategory where IsDelete=0 "
                strCommandText += " and (ItemTaxPer= @ItemTaxPer )   and ItemTaxPer<>" & OldTax & ""
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemTaxPer", DbType.String, Tax)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasPrefixForUpdateUseItemCode(ByVal ItemCategoryId As String) As System.Data.DataTable Implements IItemCategoryDA.HasPrefixForUpdateUseItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "  select * from  tbl_ItemCategory  C inner join tbl_ForSale  F on C.ItemCategoryID=F.ItemCategoryID where C.IsDelete=0"
                strCommandText += " And C.ItemCategoryID=" & ItemCategoryId & ""
                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function HasTaxForUpdateUseItemCode(ByVal ItemCategoryId As String) As System.Data.DataTable Implements IItemCategoryDA.HasTaxForUpdateUseItemCode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "  select * from  tbl_ItemCategory  C inner join tbl_ForSale  F on C.ItemCategoryID=F.ItemCategoryID where C.IsDelete=0"
                strCommandText += " and C.ItemCategoryID=" & ItemCategoryId & ""
                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function


        Public Function GetrptItemCategory() As DataTable Implements IItemCategoryDA.GetrptItemCategory
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT ItemCategoryID, ItemCategory, Prefix, ItemTaxPer FROM tbl_ItemCategory where IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function HasItemCategoryAndPrefix(ByVal ItemCategoryName As String, ByVal Prefix As String, ByVal ItemCategoryID As String) As DataTable Implements IItemCategoryDA.HasItemCategoryAndPrefix
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select * from  tbl_ItemCategory where IsDelete=0 "
                strCommandText += " and ItemCategory= @ItemCategory AND  Prefix=@Prefix AND ItemCategoryID=@ItemCategoryID  "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategory", DbType.String, ItemCategoryName)
                DB.AddInParameter(DBComm, "@Prefix", DbType.String, Prefix)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, ItemCategoryID)


                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetBalance(ByVal CategoryID As String) As System.Data.DataTable Implements IItemCategoryDA.GetBalance
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "select F.ItemCode,I.ItemCategory,F.GoldTG,I.ItemCategoryID from tbl_ForSale F Inner Join tbl_ItemCategory I On F.ItemCategoryID=I.ItemCategoryID Where IsExit=0 and F.IsDelete=0 and F.isclosed=0 and F.IsCheck=0 and F.ItemCategoryID=@ItemCategoryID And F.LocationID=@LocationID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemCategoryID", DbType.String, CategoryID)
                DB.AddInParameter(DBComm, "@LocationID", DbType.String, Global_CurrentLocationID)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

