Imports DataAccess.Branch
Namespace Branch
    Public Class BranchController
        Implements IBranchController

#Region "Private Members"

        Private _objBranchDA As IBranchDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IBranchController = New BranchController

#End Region

#Region "Constructors"

        Private Sub New()
            _objBranchDA = DataAccess.Factory.Instance.CreateBranchDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IBranchController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function InsertBranch(ByVal BranchName As String) As Boolean Implements IBranchController.InsertBranch
            Return _objBranchDA.InsertBranch(BranchName)
        End Function
        Public Function UpdateBranch(ByVal BranchName As String, ByVal OldBranchName As String) As Boolean Implements IBranchController.UpdateBranch
            Return _objBranchDA.UpdateBranch(BranchName, OldBranchName)
        End Function
        Public Function GetAllBranchList() As System.Data.DataTable Implements IBranchController.GetAllBranchList
            Return _objBranchDA.GetAllBranchList()
        End Function
        Public Function DeleteBranch(ByVal OldBranchName As String) As Boolean Implements IBranchController.DeleteBranch
            If _objBranchDA.DeleteBranch(OldBranchName) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Branch.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       OldBranchName, _
                                       "Delete Branch")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetBranchByID(ByVal BranchID As Integer) As CommonInfo.BranchInfo Implements IBranchController.GetBranchByID
            Return _objBranchDA.GetBranchByID(BranchID)
        End Function

        Public Function HashBranch(ByVal BranchName As String, Optional OldBranchName As String = "") As Boolean Implements IBranchController.HashBranch
            Return _objBranchDA.HashBranch(BranchName, OldBranchName)
        End Function
    End Class
End Namespace

