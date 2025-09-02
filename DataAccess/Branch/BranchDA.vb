Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace Branch
    Public Class BranchDA
        Implements IBranchDA

#Region "Private Branch"

        Private DB As Database
        Private Shared ReadOnly _instance As IBranchDA = New BranchDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IBranchDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function InsertBranch(ByVal BranchName As String) As Boolean Implements IBranchDA.InsertBranch
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "Insert into tbl_Branch(BranchName,IsDelete,IsUpload) Values (@BranchName,0,0)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@BranchName", DbType.String, BranchName)

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
        Public Function UpdateBranch(ByVal BranchName As String, ByVal OldBranchName As String) As Boolean Implements IBranchDA.UpdateBranch
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = "UPDATE tbl_Branch SET BranchName=@BranchName Where BranchName=@OldBranchName "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@BranchName", DbType.String, BranchName)
                DB.AddInParameter(DBComm, "@OldBranchName", DbType.String, OldBranchName)

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
        

        Public Function GetAllBranchList() As System.Data.DataTable Implements IBranchDA.GetAllBranchList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT BranchID, BranchName from tbl_Branch where IsDelete=0 Order by BranchID"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function DeleteBranch(ByVal OldBranchName As String) As Boolean Implements IBranchDA.DeleteBranch
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "Update tbl_Branch SET IsDelete=1 WHERE  BranchName= @OldBranchName"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OldBranchName", DbType.String, OldBranchName)
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

    

        Public Function GetBranchByID(ByVal BranchID As Integer) As CommonInfo.BranchInfo Implements IBranchDA.GetBranchByID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objBranch As New BranchInfo
            Try
                strCommandText = "  SELECT * "
                strCommandText += " FROM tbl_Branch WHERE BranchID = @BranchID and IsDelete=0"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@BranchID", DbType.String, BranchID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objBranch
                        .BranchID = drResult("BranchID")
                        .Branch = drResult("Branch")

                        .Address = drResult("Address")
                        .Phone = drResult("Phone")
                        If CStr(drResult("Remark15")).Trim = "" Then
                            .Remark15 = ""
                        Else
                            .Remark15 = drResult("Remark15").ToString.Trim
                        End If
                        If CStr(drResult("RemarkDone")).Trim = "" Then
                            .RemarkDone = ""
                        Else
                            .RemarkDone = CStr(drResult("RemarkDone")).Trim
                        End If
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objBranch
        End Function

        Public Function HashBranch(ByVal BranchName As String, Optional OldBranchName As String = "") As Boolean Implements IBranchDA.HashBranch
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Dim objBranch As New BranchInfo
            Try
                If OldBranchName <> "" Then
                    strCommandText = "  SELECT * FROM tbl_Branch WHERE IsDelete=0 AND BranchName = N'" & BranchName & "' AND BranchName<>N'" & OldBranchName & "'"
                Else
                    strCommandText = "  SELECT * FROM tbl_Branch WHERE IsDelete=0 AND BranchName = N'" & BranchName & "'"
                End If
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                If dtResult.Rows.Count = 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

    End Class
End Namespace

