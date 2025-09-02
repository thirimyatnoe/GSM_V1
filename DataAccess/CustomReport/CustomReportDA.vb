Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Namespace CustomReport
    Public Class CustomReportDA
        Implements ICustomReportDA

#Region "Private Custom Report"

        Private DB As Database
        Private Shared ReadOnly _instance As ICustomReportDA = New CustomReportDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICustomReportDA
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteCustomReport(CustomReportID As String) As Boolean Implements ICustomReportDA.DeleteCustomReport
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_CustomReport WHERE ReportID = @ReportID"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ReportID", DbType.String, CustomReportID)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Jewellery Shop Management System")
                Return False
            End Try
        End Function

        Public Function GetCustomReport(CustomReportID As String) As CustomReportInfo Implements ICustomReportDA.GetCustomReport
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objCustomReport As New CustomReportInfo

            Try

                strCommandText = "  SELECT ReportID, isnull(ReportName,'')AS ReportName, isnull(ReportCode,'')AS ReportCode, CriCustomer,CriGoldQuality,CriItemCategory,CriItemName,CriGemsCategory,CriFromDate,CriToDate,CriStaff "
                strCommandText += " FROM tbl_CustomReport WHERE ReportID = @ReportID"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReportID", DbType.String, CustomReportID)

                drResult = DB.ExecuteReader(DBComm)

                If drResult.Read() Then
                    With objCustomReport
                        .ReportID = IIf(IsDBNull(drResult("ReportID")), "", drResult("ReportID"))
                        .ReportName = IIf(IsDBNull(drResult("ReportName")), "", drResult("ReportName"))
                        .ReportCode = IIf(IsDBNull(drResult("ReportCode")), "", drResult("ReportCode"))

                        .CriCustomer = IIf(IsDBNull(drResult("CriCustomer")), False, drResult("CriCustomer"))
                        .CriGoldQuality = IIf(IsDBNull(drResult("CriGoldQuality")), False, drResult("CriGoldQuality"))
                        .CriItemCategory = IIf(IsDBNull(drResult("CriItemCategory")), False, drResult("CriItemCategory"))
                        .CriItemName = IIf(IsDBNull(drResult("CriItemName")), False, drResult("CriItemName"))
                        .CriGemsCategory = IIf(IsDBNull(drResult("CriGemsCategory")), False, drResult("CriGemsCategory"))
                        .CriFromDate = IIf(IsDBNull(drResult("CriFromDate")), False, drResult("CriFromDate"))
                        .CriToDate = IIf(IsDBNull(drResult("CriToDate")), False, drResult("CriToDate"))
                        .CriStaff = IIf(IsDBNull(drResult("CriStaff")), False, drResult("CriStaff"))
                      
                    End With
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewellery Shop Management System")
            End Try

            Return objCustomReport
        End Function

        Public Function GetCustomReportByStr(cristr As String) As DataTable Implements ICustomReportDA.GetCustomReportByStr
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = " Select * from tbl_CustomReport where 1 = 1 " & cristr

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewellery Shop Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertCustomReport(CustomReportObj As CustomReportInfo) As Boolean Implements ICustomReportDA.InsertCustomReport
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "INSERT INTO tbl_CustomReport (ReportID,ReportName,ReportCode,CriCustomer,CriGoldQuality,CriItemCategory,CriItemName,CriGemsCategory,CriFromDate,CriToDate,CriStaff) "
                strCommandText += " VALUES ( @ReportID,@ReportName,@ReportCode,@CriCustomer,@CriGoldQuality,@CriItemCategory,@CriItemName,@CriGemsCategory,@CriFromDate,@CriToDate,@CriStaff)"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@ReportID", DbType.String, CustomReportObj.ReportID)
                DB.AddInParameter(DBComm, "@ReportName", DbType.String, CustomReportObj.ReportName)
                DB.AddInParameter(DBComm, "@ReportCode", DbType.String, CustomReportObj.ReportCode)
                DB.AddInParameter(DBComm, "@CriCustomer", DbType.Boolean, CustomReportObj.CriCustomer)
                DB.AddInParameter(DBComm, "@CriGoldQuality", DbType.Boolean, CustomReportObj.CriGoldQuality)
                DB.AddInParameter(DBComm, "@CriItemCategory", DbType.Boolean, CustomReportObj.CriItemCategory)
                DB.AddInParameter(DBComm, "@CriItemName", DbType.Boolean, CustomReportObj.CriItemName)
                DB.AddInParameter(DBComm, "@CriGemsCategory", DbType.Boolean, CustomReportObj.CriGemsCategory)
                DB.AddInParameter(DBComm, "@CriFromDate", DbType.Boolean, CustomReportObj.CriFromDate)
                DB.AddInParameter(DBComm, "@CriToDate", DbType.Boolean, CustomReportObj.CriToDate)
                DB.AddInParameter(DBComm, "@CriStaff", DbType.Boolean, CustomReportObj.CriStaff)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewellery Shop Management System")
                Return False
            End Try
        End Function

        Public Function UpdateCustomReport(CustomReportObj As CustomReportInfo) As Boolean Implements ICustomReportDA.UpdateCustomReport
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "  UPDATE tbl_CustomReport SET ReportID = @ReportID, ReportName = @ReportName, ReportCode = @ReportCode, CriCustomer = @CriCustomer, CriGoldQuality = @CriGoldQuality, CriItemCategory = @CriItemCategory, "
                strCommandText += "  CriItemName=@CriItemName, CriGemsCategory = @CriGemsCategory,CriFromDate = @CriFromDate,CriToDate = @CriToDate,CriStaff = @CriStaff WHERE ReportID = @ReportID "

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@ReportID", DbType.String, CustomReportObj.ReportID)
                DB.AddInParameter(DBComm, "@ReportName", DbType.String, CustomReportObj.ReportName)
                DB.AddInParameter(DBComm, "@ReportCode", DbType.String, CustomReportObj.ReportCode)
                DB.AddInParameter(DBComm, "@CriCustomer", DbType.Boolean, CustomReportObj.CriCustomer)
                DB.AddInParameter(DBComm, "@CriGoldQuality", DbType.Boolean, CustomReportObj.CriGoldQuality)
                DB.AddInParameter(DBComm, "@CriItemCategory", DbType.Boolean, CustomReportObj.CriItemCategory)
                DB.AddInParameter(DBComm, "@CriItemName", DbType.Boolean, CustomReportObj.CriItemName)
                DB.AddInParameter(DBComm, "@CriGemsCategory", DbType.Boolean, CustomReportObj.CriGemsCategory)
                DB.AddInParameter(DBComm, "@CriFromDate", DbType.Boolean, CustomReportObj.CriFromDate)
                DB.AddInParameter(DBComm, "@CriToDate", DbType.Boolean, CustomReportObj.CriToDate)
                DB.AddInParameter(DBComm, "@CriStaff", DbType.Boolean, CustomReportObj.CriStaff)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Jewellery Shop Management System")
                Return False
            End Try

        End Function
    End Class
End Namespace

