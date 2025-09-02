Imports CommonInfo

Namespace DatabaseExportImport
    Public Interface IDatabaseExportImportController
        Function IsExistsConfiguration() As Boolean
        Function CheckAccessDatabase(ByVal FileName As String) As Integer
        Function CreateAccessConnectionString(ByVal FileName As String, ByVal PPassword As String) As String
        Function AccessBackup(ByVal sourceFile As String, ByVal destinationFile As String) As String
        Function AccessRestore(ByVal sourceFile As String, ByVal destinationFile As String) As String

        Function CreateDataBaseForBranch(ByVal ScriptFile As String, ByVal DatabasePath As String, ByVal BranchID As Integer, ByVal UserIDs As String, ByVal UserLevelIDs As String) As Boolean
        Function ExportDatabaseForHO(ByVal DatabasePath As String, ByVal ForDate As Date) As Boolean
        Function ImportDatabaseFromBranch(ByVal DatabasePath As String) As Boolean
        Function ExportDataBaseForBranch(ByVal DatabasePath As String, ByVal BranchID As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Boolean
        Function ImportDatabaseFromHO(ByVal DatabasePath As String, ByVal isStock As Boolean, ByVal Status As String, ByVal isDivided As Boolean, ByVal ExecuteScriptFile As String) As Boolean

        Function CreateDataBaseForTransfer(ByVal ScriptFile As String, ByVal DatabasePath As String, ByVal TransferID As String, ByVal UserIDs As String, ByVal UserLevelIDs As String) As Boolean
        Function ImportDatabaseFromTransfer(ByVal DatabasePath As String) As Boolean
        Function ExportDataBaseForTransfer(ByVal DatabasePath As String, ByVal TransferID As String) As Boolean

        Function DivideByLocation(ByVal DatabasePath As String, ByVal BranchID As String)
        Function CreateDatabaseForExportData(ByVal DatabasePath As String, ByVal posdate As DateTime, ByVal nowdate As DateTime, ByVal BranchID As String, ByVal Exportdatatypte As String, ByVal FName As String, ByVal bolAll As Boolean, ByVal CurrentCompanyID As String) As Boolean
        Function ImportData(ByVal DatabasePath As String, ByVal BranchID As String, ByVal Exportdatatypte As String) As Boolean

        Function DeleteExportServiceData(ByVal cri As String) As Boolean
        Function InsertExportServiceData(ByVal objExportData As ExportDataInfo) As Boolean
        Function UpdateExportServiceData(ByVal objExportData As ExportDataInfo) As Boolean


        Function GetExportServiceDataList(Optional ByVal cri As String = "") As DataTable
        Function SendEmail(ByVal msg As String, ByVal branchID As String, ByVal MailServer As String, ByVal FromEMail As String, ByVal FromName As String, ByVal User As String, ByVal PWD As String, ByVal ToEMail As String, ByVal CCMail As String, ByVal Port As String, ByVal SMTP As String, ByVal CompanyName As String) As Boolean
        Function UpdateIsUploaded(ByVal dt As String) As Boolean
        Function CreateDatabaseForMobileExportData(ByVal DatabasePath As String, ByVal posdate As DateTime, ByVal nowdate As DateTime, ByVal BranchID As String, ByVal FName As String) As Boolean

    End Interface
End Namespace

