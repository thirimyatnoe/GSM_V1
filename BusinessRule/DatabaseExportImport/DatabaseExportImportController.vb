Imports DataAccess.DatabaseExportImport
Namespace DatabaseExportImport
    Public Class DatabaseExportImportController
        Implements IDatabaseExportImportController



#Region "Private Members"

        Private _objDatabaseExportImportDA As IDatabaseExportImportDA
        Private Shared ReadOnly _instance As IDatabaseExportImportController = New DatabaseExportImportController

#End Region

#Region "Constructors"

        Public Sub New()
            _objDatabaseExportImportDA = DataAccess.Factory.Instance.CreateDatabaseExportImportDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDatabaseExportImportController
            Get
                Return _instance
            End Get
        End Property

#End Region





        Public Function CheckAccessDatabase(ByVal FileName As String) As Integer Implements IDatabaseExportImportController.CheckAccessDatabase
            '0=Access Database OK, 1=File Not Found, 2=Incorrect Password, 3=Invalid Database structure
            Return _objDatabaseExportImportDA.CheckAccessDatabase(FileName)
        End Function

        Public Function AccessBackup(ByVal sourceFile As String, ByVal destinationFile As String) As String Implements IDatabaseExportImportController.AccessBackup
            Return _objDatabaseExportImportDA.AccessBackup(sourceFile, destinationFile)
        End Function

        Public Function AccessRestore(ByVal sourceFile As String, ByVal destinationFile As String) As String Implements IDatabaseExportImportController.AccessRestore
            Return _objDatabaseExportImportDA.AccessRestore(sourceFile, destinationFile)
        End Function

        Public Function CreateAccessConnectionString(ByVal FileName As String, ByVal PPassword As String) As String Implements IDatabaseExportImportController.CreateAccessConnectionString
            Return _objDatabaseExportImportDA.CreateAccessConnectionString(FileName, PPassword)
        End Function

        Public Function CreateDataBaseForBranch(ByVal ScriptFile As String, ByVal DatabasePath As String, ByVal BranchID As Integer, ByVal UserIDs As String, ByVal UserLevelIDs As String) As Boolean Implements IDatabaseExportImportController.CreateDataBaseForBranch
            'Return _objDatabaseExportImportDA.CreateDataBaseForBranch(ScriptFile, DatabasePath, BranchID, UserIDs, UserLevelIDs)
        End Function

        Public Function ExportDatabaseForHO(ByVal DatabasePath As String, ByVal ForDate As Date) As Boolean Implements IDatabaseExportImportController.ExportDatabaseForHO
            Return _objDatabaseExportImportDA.ExportDatabaseForHO(DatabasePath, ForDate)
        End Function

        Public Function ImportDatabaseFromBranch(ByVal DatabasePath As String) As Boolean Implements IDatabaseExportImportController.ImportDatabaseFromBranch
            Return _objDatabaseExportImportDA.ImportDatabaseFromBranch(DatabasePath)
        End Function

        Public Function ImportDatabaseFromHO(ByVal DatabasePath As String, ByVal isStock As Boolean, ByVal status As String, ByVal isDivided As Boolean, ByVal ExecuteScriptFile As String) As Boolean Implements IDatabaseExportImportController.ImportDatabaseFromHO
            Return _objDatabaseExportImportDA.ImportDatabaseFromHO(DatabasePath, isStock, status, isDivided, ExecuteScriptFile)
        End Function

        Public Function IsExistsConfiguration() As Boolean Implements IDatabaseExportImportController.IsExistsConfiguration
            Return _objDatabaseExportImportDA.IsExistsConfiguration
        End Function

        Public Function ExportDataBaseForBranch(ByVal DatabasePath As String, ByVal BranchID As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Boolean Implements IDatabaseExportImportController.ExportDataBaseForBranch
            Return _objDatabaseExportImportDA.ExportDataBaseForBranch(DatabasePath, BranchID, FromDate, ToDate)
        End Function

        Public Function CreateDataBaseForTransfer(ByVal ScriptFile As String, ByVal DatabasePath As String, ByVal TransferID As String, ByVal UserIDs As String, ByVal UserLevelIDs As String) As Boolean Implements IDatabaseExportImportController.CreateDataBaseForTransfer
            Return _objDatabaseExportImportDA.CreateDataBaseForTransfer(ScriptFile, DatabasePath, TransferID, UserIDs, UserLevelIDs)

        End Function

        Public Function ExportDataBaseForTransfer(ByVal DatabasePath As String, ByVal TransferID As String) As Boolean Implements IDatabaseExportImportController.ExportDataBaseForTransfer
            Return _objDatabaseExportImportDA.ExportDataBaseForTransfer(DatabasePath, TransferID)
        End Function

        Public Function ImportDatabaseFromTransfer(ByVal DatabasePath As String) As Boolean Implements IDatabaseExportImportController.ImportDatabaseFromTransfer
            Return _objDatabaseExportImportDA.ImportDatabaseFromTransfer(DatabasePath)
        End Function

        Public Function DivideByLocation(ByVal DatabasePath As String, ByVal BranchID As String) As Object Implements IDatabaseExportImportController.DivideByLocation
            Return _objDatabaseExportImportDA.DivideByLocation(DatabasePath, BranchID)
        End Function
        Public Function CreateDatabaseForExportData(ByVal DatabasePath As String, ByVal posdate As DateTime, ByVal nowdate As DateTime, ByVal BranchID As String, ByVal Exportdatatypte As String, ByVal FName As String, ByVal bolAll As Boolean, ByVal CurrentCompanyID As String) As Boolean Implements IDatabaseExportImportController.CreateDatabaseForExportData
            Return _objDatabaseExportImportDA.CreateDatabaseForExportData(DatabasePath, posdate, nowdate, BranchID, Exportdatatypte, FName, bolAll, CurrentCompanyID)
        End Function
        Public Function ImportData(ByVal DatabasePath As String, ByVal BranchID As String, ByVal Exportdatatypte As String) As Boolean Implements IDatabaseExportImportController.ImportData
            Return _objDatabaseExportImportDA.ImportData(DatabasePath, BranchID, Exportdatatypte)
        End Function
        'Public Function GetExportServiceDataList(Optional ByVal cri As String = "") As DataTable
        '    Return _objDatabaseExportImportDA.GetExportServiceDataList(cri)
        'End Function
        'Public Function DeleteExportServiceData(ByVal cri As String) As Boolean
        '    If _objDatabaseExportImportDA.DeleteExportServiceData(cri) Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'End Function
        Public Function SaveExportServiceData(ByVal ExportObj As CommonInfo.ExportDataInfo) As Boolean
            Dim objGeneralController = New General.GeneralController
            Dim bolRet As Boolean
            If ExportObj.ExportID = "" Then
                If _objDatabaseExportImportDA.InsertExportServiceData(ExportObj) Then
                    bolRet = True
                Else
                    bolRet = False
                End If
            Else
                If _objDatabaseExportImportDA.UpdateExportServiceData(ExportObj) Then
                    bolRet = True
                Else
                    bolRet = False
                End If
            End If
            Return bolRet
        End Function
        Public Function InsertExportServiceData(ByVal ExportObj As CommonInfo.ExportDataInfo) As Boolean Implements IDatabaseExportImportController.InsertExportServiceData
            Return _objDatabaseExportImportDA.InsertExportServiceData(ExportObj)
        End Function
        Public Function UpdateExportServiceData(ByVal ExportObj As CommonInfo.ExportDataInfo) As Boolean Implements IDatabaseExportImportController.UpdateExportServiceData
            Return _objDatabaseExportImportDA.UpdateExportServiceData(ExportObj)
        End Function

        Public Function DeleteExportServiceData(ByVal cri As String) As Boolean Implements IDatabaseExportImportController.DeleteExportServiceData
            If _objDatabaseExportImportDA.DeleteExportServiceData(cri) Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Function GetExportServiceDataList(Optional ByVal cri As String = "") As DataTable Implements IDatabaseExportImportController.GetExportServiceDataList
            Return _objDatabaseExportImportDA.GetExportServiceDataList(cri)
        End Function

        Public Function UpdateIsUploaded(ByVal dt As String) As Boolean Implements IDatabaseExportImportController.UpdateIsUploaded
            Return _objDatabaseExportImportDA.UpdateIsUploaded(dt)
        End Function
        Public Function SendEmail(ByVal msg As String, ByVal branchID As String, ByVal MailServer As String, ByVal FromEMail As String, ByVal FromName As String, ByVal User As String, ByVal PWD As String, ByVal ToEMail As String, ByVal CCMail As String, ByVal Port As String, ByVal SMTP As String, ByVal CompanyName As String) As Boolean Implements IDatabaseExportImportController.SendEmail
            Return _objDatabaseExportImportDA.SendEmail(msg, branchID, MailServer, FromEMail, FromName, User, PWD, ToEMail, CCMail, Port, SMTP, CompanyName)
        End Function
        Public Function CreateDatabaseForMobileExportData(ByVal DatabasePath As String, ByVal posdate As DateTime, ByVal nowdate As DateTime, ByVal BranchID As String, ByVal FName As String) As Boolean Implements IDatabaseExportImportController.CreateDatabaseForMobileExportData
            Return _objDatabaseExportImportDA.CreateDatabaseForMobileExportData(DatabasePath, posdate, nowdate, BranchID, FName)
        End Function
    End Class
End Namespace

