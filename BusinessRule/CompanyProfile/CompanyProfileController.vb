Imports DataAccess.CompanyProfile
Namespace CompanyProfile
    Public Class CompanyProfileController
        Implements ICompanyProfileController

#Region "Private Members"

        Private _objCompanyDA As ICompanyProfileDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ICompanyProfileController = New CompanyProfileController

#End Region

#Region "Constructors"

        Private Sub New()
            _objCompanyDA = DataAccess.Factory.Instance.CreateCompanyProfileDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICompanyProfileController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteCompanyProfile(CompanyProfileID As String) As Boolean Implements ICompanyProfileController.DeleteCompanyProfile

        End Function

        Public Function GetCompanyProfileList(Optional str As String = "") As DataTable Implements ICompanyProfileController.GetCompanyProfileList
            Return _objCompanyDA.GetCompanyProfileList(str)
        End Function

        Public Function GetCompanyProfile(CompanyProfileID As String) As CommonInfo.CompanyProfileInfo Implements ICompanyProfileController.GetCompanyProfile
            Return _objCompanyDA.GetCompanyProfile(CompanyProfileID)
        End Function

        Public Function SaveCompanyProfile(ByRef obj As CommonInfo.CompanyProfileInfo) As Boolean Implements ICompanyProfileController.SaveCompanyProfile
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            If obj.CompanyID = "" Then
                obj.CompanyID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.CompanyProfile, "CompanyProfile", Now.Date)
                Return _objCompanyDA.InsertCompanyProfile(obj)
            Else
                Return _objCompanyDA.UpdateCompanyProfile(obj)
            End If

        End Function

        Public Function InsertCompanyProfileByKeyGenerate(CompanyProfileObj As CommonInfo.CompanyProfileInfo) As Boolean Implements ICompanyProfileController.InsertCompanyProfileByKeyGenerate
            Return _objCompanyDA.InsertCompanyProfileByKeyGenerate(CompanyProfileObj)
        End Function

        Public Function UpdateCompanyProfileByKeyGenerate(CompanyProfileObj As CommonInfo.CompanyProfileInfo, OldCompanyID As String) As Boolean Implements ICompanyProfileController.UpdateCompanyProfileByKeyGenerate
            Return _objCompanyDA.UpdateCompanyProfileByKeyGenerate(CompanyProfileObj, OldCompanyID)
        End Function
    End Class

End Namespace
