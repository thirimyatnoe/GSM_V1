Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace CompanyProfile
    Public Class CompanyProfileDA
        Implements ICompanyProfileDA

#Region "Private CompanyProfile"

        Private DB As Database
        Private Shared ReadOnly _instance As ICompanyProfileDA = New CompanyProfileDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICompanyProfileDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteCompanyProfile(CompanyProfileID As String) As Boolean Implements ICompanyProfileDA.DeleteCompanyProfile

        End Function

        Public Function GetCompanyProfile(CompanyProfileID As String) As CompanyProfileInfo Implements ICompanyProfileDA.GetCompanyProfile
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objCompanyProfile As New CompanyProfileInfo

            Try

                strCommandText = " SELECT CompanyID, CompanyName, CompanyLogo, Telephone, Email, Address, WebSite, Fax ,HeadOffice " & _
                                    " FROM tbl_CompanyProfile WHERE CompanyID = @CompanyID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CompanyID", DbType.String, CompanyProfileID)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then

                    With objCompanyProfile
                        .CompanyID = drResult("CompanyID")
                        .CompanyName = drResult("CompanyName")
                        .CompanyLogo = drResult("CompanyLogo")
                        .Telephone = drResult("Telephone")
                        .Email = drResult("Email")
                        .Address = drResult("Address")
                        .WebSite = drResult("WebSite")
                        .Fax = drResult("Fax")
                        .HO = drResult("HeadOffice")


                    End With
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewelry Shop Management System")
            End Try

            Return objCompanyProfile
        End Function

        Public Function GetCompanyProfileList(Optional str As String = "") As DataTable Implements ICompanyProfileDA.GetCompanyProfileList
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "  SELECT CompanyID, CompanyName, Telephone, Email, Address, WebSite, Fax , HeadOffice " & _
                                    " FROM tbl_CompanyProfile " & str

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewelry Shop Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertCompanyProfile(obj As CompanyProfileInfo) As Boolean Implements ICompanyProfileDA.InsertCompanyProfile
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "INSERT INTO tbl_CompanyProfile(CompanyID, CompanyName, CompanyLogo, Telephone, Email, Address, WebSite, Fax ,HeadOffice)" & _
                               " VALUES ( @CompanyID, @CompanyName,  @CompanyLogo, @Telephone, @Email, @Address, @WebSite, @Fax, @HeadOffice)"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@CompanyID", DbType.String, obj.CompanyID)
                DB.AddInParameter(DBComm, "@CompanyName", DbType.String, obj.CompanyName)
                DB.AddInParameter(DBComm, "@CompanyLogo", DbType.Binary, obj.CompanyLogo)
                DB.AddInParameter(DBComm, "@Telephone", DbType.String, obj.Telephone)
                DB.AddInParameter(DBComm, "@Email", DbType.String, obj.Email)
                DB.AddInParameter(DBComm, "@Address", DbType.String, obj.Address)
                DB.AddInParameter(DBComm, "@WebSite", DbType.String, obj.WebSite)
                DB.AddInParameter(DBComm, "@Fax", DbType.String, obj.Fax)
                DB.AddInParameter(DBComm, "@HeadOffice", DbType.Boolean, obj.HO)


                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewelry Shop Management System")
                Return False
            End Try
        End Function

        Public Function UpdateCompanyProfile(obj As CompanyProfileInfo) As Boolean Implements ICompanyProfileDA.UpdateCompanyProfile
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try
                strCommandText = "UPDATE tbl_CompanyProfile SET CompanyName=@CompanyName," & _
                     " CompanyLogo=@CompanyLogo, Telephone=@Telephone, Email=@Email, Address=@Address, WebSite=@WebSite, Fax=@Fax, HeadOffice=@HeadOffice WHERE CompanyID=@CompanyID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CompanyID", DbType.String, obj.CompanyID)
                DB.AddInParameter(DBComm, "@CompanyName", DbType.String, obj.CompanyName)
                DB.AddInParameter(DBComm, "@CompanyLogo", DbType.Binary, obj.CompanyLogo)
                DB.AddInParameter(DBComm, "@Telephone", DbType.String, obj.Telephone)
                DB.AddInParameter(DBComm, "@Email", DbType.String, obj.Email)
                DB.AddInParameter(DBComm, "@Address", DbType.String, obj.Address)
                DB.AddInParameter(DBComm, "@WebSite", DbType.String, obj.WebSite)
                DB.AddInParameter(DBComm, "@Fax", DbType.String, obj.Fax)
                DB.AddInParameter(DBComm, "@HeadOffice", DbType.Boolean, obj.HO)


                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewelry Shop Management System")
                Return False
            End Try
        End Function

        Public Function InsertCompanyProfileByKeyGenerate(CompanyProfileObj As CompanyProfileInfo) As Boolean Implements ICompanyProfileDA.InsertCompanyProfileByKeyGenerate
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "INSERT INTO tbl_CompanyProfile(CompanyID, CompanyName, CompanyLogo, Telephone, Email, Address, WebSite, Fax ,HeadOffice)" & _
                               " VALUES ( @CompanyID, @CompanyName,  @CompanyLogo, @Telephone, @Email, @Address, @WebSite, @Fax, @HeadOffice)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CompanyID", DbType.String, CompanyProfileObj.CompanyID)
                DB.AddInParameter(DBComm, "@CompanyName", DbType.String, CompanyProfileObj.CompanyName)
                DB.AddInParameter(DBComm, "@CompanyLogo", DbType.Binary, CompanyProfileObj.CompanyLogo)
                DB.AddInParameter(DBComm, "@Telephone", DbType.String, CompanyProfileObj.Telephone)
                DB.AddInParameter(DBComm, "@Email", DbType.String, CompanyProfileObj.Email)
                DB.AddInParameter(DBComm, "@Address", DbType.String, CompanyProfileObj.Address)
                DB.AddInParameter(DBComm, "@WebSite", DbType.String, CompanyProfileObj.WebSite)
                DB.AddInParameter(DBComm, "@Fax", DbType.String, CompanyProfileObj.Fax)
                DB.AddInParameter(DBComm, "@HeadOffice", DbType.Boolean, CompanyProfileObj.HO)


                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stock Management System")
                Return False
            End Try
        End Function

        Public Function UpdateCompanyProfileByKeyGenerate(CompanyProfileObj As CompanyProfileInfo, OldCompanyID As String) As Boolean Implements ICompanyProfileDA.UpdateCompanyProfileByKeyGenerate
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "UPDATE tbl_CompanyProfile SET CompanyID = @CompanyID,CompanyName=@CompanyName," & _
                                " CompanyLogo=@CompanyLogo, Telephone=@Telephone, Email=@Email, Address=@Address, WebSite=@WebSite, Fax=@Fax, HeadOffice=@HeadOffice WHERE CompanyID= @OldCompanyID "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@CompanyID", DbType.String, CompanyProfileObj.CompanyID)
                DB.AddInParameter(DBComm, "@CompanyName", DbType.String, CompanyProfileObj.CompanyName)
                DB.AddInParameter(DBComm, "@CompanyLogo", DbType.Binary, CompanyProfileObj.CompanyLogo)
                DB.AddInParameter(DBComm, "@Telephone", DbType.String, CompanyProfileObj.Telephone)
                DB.AddInParameter(DBComm, "@Email", DbType.String, CompanyProfileObj.Email)
                DB.AddInParameter(DBComm, "@Address", DbType.String, CompanyProfileObj.Address)
                DB.AddInParameter(DBComm, "@WebSite", DbType.String, CompanyProfileObj.WebSite)
                DB.AddInParameter(DBComm, "@Fax", DbType.String, CompanyProfileObj.Fax)
                DB.AddInParameter(DBComm, "@HeadOffice", DbType.Boolean, CompanyProfileObj.HO)
                DB.AddInParameter(DBComm, "@OldCompanyID", DbType.String, OldCompanyID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Stock Management System")
                Return False
            End Try
            DBComm.Connection.Close()
        End Function
    End Class
End Namespace

