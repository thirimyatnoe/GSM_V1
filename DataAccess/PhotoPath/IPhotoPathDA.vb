Imports CommonInfo

Namespace PhotoPath
    Public Interface IPhotoPathDA

        Function InsertPhotoPath(ByVal PhtotoPathobj As PhotoPathInfo) As Boolean
        Function UpdatePhotoPath(ByVal PhtotoPathobj As PhotoPathInfo) As Boolean
        Function DeletePhotoPath() As Boolean
        Function GetPhotoPathList() As DataTable
        Function GetPhotoPathByID() As CommonInfo.PhotoPathInfo
        Function GetPhotoPathForOneFinger(ByVal OneFinger As Integer) As String



    End Interface
End Namespace





