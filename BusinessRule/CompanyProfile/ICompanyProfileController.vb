Imports CommonInfo
Namespace CompanyProfile
    Public Interface ICompanyProfileController
        Function SaveCompanyProfile(ByRef obj As CompanyProfileInfo) As Boolean
        Function DeleteCompanyProfile(ByVal CompanyProfileID As String) As Boolean
        Function GetCompanyProfile(ByVal CompanyProfileID As String) As CompanyProfileInfo
        Function GetCompanyProfileList(Optional ByVal str As String = "") As DataTable
        Function InsertCompanyProfileByKeyGenerate(ByVal CompanyProfileObj As CompanyProfileInfo) As Boolean
        Function UpdateCompanyProfileByKeyGenerate(ByVal CompanyProfileObj As CompanyProfileInfo, ByVal OldCompanyID As String) As Boolean
    End Interface
End Namespace

