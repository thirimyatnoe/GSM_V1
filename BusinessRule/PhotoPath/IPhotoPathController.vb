Imports CommonInfo

Namespace PhotoPath
    Public Interface IPhotoPathController

        Function SavePhotoPath(ByVal PhtotoPathobj As PhotoPathInfo, ByVal OldPhoto As String) As Long
        Function DeletePhotoPath() As Boolean
        Function GetPhotoPathList() As DataTable
        Function GetPhotoPathByID() As CommonInfo.PhotoPathInfo
        Function GetPhotoPathForOneFinger(ByVal OneFinger As Integer) As String

    End Interface
End Namespace




