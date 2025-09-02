Imports CommonInfo
Namespace Branch
    Public Interface IBranchController
        Function InsertBranch(ByVal BranchName As String) As Boolean
        Function UpdateBranch(ByVal BranchName As String, ByVal OldBranchName As String) As Boolean
        Function GetAllBranchList() As DataTable
        Function DeleteBranch(ByVal OldBranchName As String) As Boolean
        Function GetBranchByID(ByVal BranchID As Integer) As BranchInfo
        Function HashBranch(ByVal BranchName As String, Optional OldBranchName As String = "") As Boolean

    End Interface
End Namespace

