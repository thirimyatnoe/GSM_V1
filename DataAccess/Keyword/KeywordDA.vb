Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.data
Imports System.Data.Common
Imports CommonInfo

Namespace Keyword
    Public Class KeywordDA
        Implements IKeywordDA

#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As IKeywordDA = New KeywordDA

#End Region

#Region "Constructor"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IKeywordDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteKeywordHeader(ByVal KeywordHeaderID As Integer) As Boolean Implements IKeywordDA.DeleteKeywordHeader
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_KeywordHeader WHERE KeywordID = @KeywordID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@KeywordID", DbType.Int16, KeywordHeaderID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Stationery Management")
            End Try
        End Function

        Public Function DeleteKeywordItem(ByVal KeywordItemID As Integer) As Boolean Implements IKeywordDA.DeleteKeywordItem
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_KeywordItem WHERE ItemID = @ItemID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ItemID", DbType.Int16, KeywordItemID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Stationery Management")
                Return False
            End Try
        End Function

        Public Function GetKeywordHeader(ByVal KeywordHeaderID As Integer) As CommonInfo.KeywordHeaderInfo Implements IKeywordDA.GetKeywordHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objKeyword As New KeywordHeaderInfo

            Try

                strCommandText = "  SELECT KeywordID, KeywordName FROM tbl_KeywordHeader WHERE KeywordID = @KeywordID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeywordID", DbType.String, KeywordHeaderID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objKeyword
                        .KeywordID = drResult("KeywordID")
                        .KeywordName = drResult("KeywordName")
                    End With
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
            End Try

            Return objKeyword
        End Function

        Public Function GetKeywordHeaderList() As System.Data.DataTable Implements IKeywordDA.GetKeywordHeaderList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "  SELECT KeywordID as [HideKeywordID], KeywordName FROM tbl_KeywordHeader "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
                Return New DataTable
            End Try
        End Function

        Public Function GetKeywordItems(ByVal KeywordHeaderID As Object) As System.Data.DataTable Implements IKeywordDA.GetKeywordItems
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "  SELECT ItemID, ItemName FROM tbl_KeywordItem WHERE KeywordID  = @KeywordID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeywordID", DbType.Int16, KeywordHeaderID)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
                Return New DataTable
            End Try
        End Function

        Public Function GetKeywordItemsByHeaderName(ByVal KeywordName As String) As System.Data.DataTable Implements IKeywordDA.GetKeywordItemsByHeaderName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try

                strCommandText = "SELECT I.ItemID,I.ItemName FROM tbl_KeywordItem I INNER JOIN tbl_KeywordHeader H ON I.KeywordID=H.KeywordID WHERE H.KeywordName = @KeywordName "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeywordName", DbType.String, KeywordName)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
                Return New DataTable
            End Try
        End Function

        Public Function InsertKeywordHeader(ByVal KeywordHeaderObj As CommonInfo.KeywordHeaderInfo) As Boolean Implements IKeywordDA.InsertKeywordHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = "INSERT INTO tbl_KeywordHeader(KeywordID,KeywordName) VALUES(@KeywordID,@KeywordName)"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@KeywordID", DbType.Int16, KeywordHeaderObj.KeywordID)
                DB.AddInParameter(DBComm, "@KeywordName", DbType.String, KeywordHeaderObj.KeywordName)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
            End Try
        End Function

        Public Function InsertKeywordItem(ByVal KeywordItemObj As CommonInfo.KeywordItemInfo) As Boolean Implements IKeywordDA.InsertKeywordItem
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "INSERT INTO tbl_KeywordItem (ItemID, KeywordID, ItemName) VALUES (@ItemID, @KeywordID, @ItemName)"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ItemID", DbType.Int16, KeywordItemObj.ItemID)
                DB.AddInParameter(DBComm, "@KeywordID", DbType.Int16, KeywordItemObj.KeywordID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, KeywordItemObj.ItemName)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
                Return False
            End Try
        End Function

        Public Function UpdateKeywordHeader(ByVal KeywordHeaderObj As CommonInfo.KeywordHeaderInfo) As Boolean Implements IKeywordDA.UpdateKeywordHeader
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = " UPDATE tbl_KeywordHeader SET KeywordID = @KeywordID, KeywordName = @KeywordName WHERE KeywordID = @KeywordID "
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@KeywordID", DbType.Int16, KeywordHeaderObj.KeywordID)
                DB.AddInParameter(DBComm, "@KeywordName", DbType.String, KeywordHeaderObj.KeywordName)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
            End Try
        End Function

        Public Function UpdateKeywordItem(ByVal KeywordItemObj As CommonInfo.KeywordItemInfo) As Boolean Implements IKeywordDA.UpdateKeywordItem
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = " UPDATE tbl_KeywordItem SET ItemID = @ItemID, KeywordID = @KeywordID, ItemName = @ItemName WHERE ItemID = @ItemID "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ItemID", DbType.Int16, KeywordItemObj.ItemID)
                DB.AddInParameter(DBComm, "@KeywordID", DbType.Int16, KeywordItemObj.KeywordID)
                DB.AddInParameter(DBComm, "@ItemName", DbType.String, KeywordItemObj.ItemName)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
                Return False
            End Try
        End Function

        Public Function GetKeywordItemsName() As System.Data.DataTable Implements IKeywordDA.GetKeywordItemsName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = "select ItemName from tbl_KeywordItem"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")

                Return New DataTable
            End Try
        End Function

        Public Function GetKeywordHeaderIDByName(ByVal KeywordName As String) As System.Data.DataTable Implements IKeywordDA.GetKeywordHeaderIDByName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable


            Try
                strCommandText = "SELECT  KeywordID FROM dbo.tbl_KeywordHeader WHERE (KeywordName = @KeywordName)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeywordName", DbType.String, KeywordName)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")

                Return New DataTable
            End Try
        End Function

        Public Function GetKeywordItemByItemID(ByVal ItemID As String) As CommonInfo.KeywordItemInfo Implements IKeywordDA.GetKeywordItemByItemID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objKeywordItem As New KeywordItemInfo

            Try

                strCommandText = "  SELECT * FROM tbl_KeywordItem WHERE ItemID = @ItemID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ItemID", DbType.String, ItemID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objKeywordItem
                        .ItemID = drResult("ItemID")
                        .ItemName = drResult("ItemName")
                        .KeywordID = drResult("KeywordID")
                    End With
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stationery Management")
            End Try

            Return objKeywordItem
        End Function
    End Class
End Namespace