Imports CommonInfo
Imports DataAccess.PhotoPath


Namespace PhotoPath
    Friend Class PhotoPathController
        Implements IPhotoPathController



#Region "Private Members"

        Private _objPhotoPathDA As IPhotoPathDA
        Private Shared ReadOnly _instance As IPhotoPathController = New PhotoPathController

#End Region

#Region "Constructors"

        Private Sub New()
            _objPhotoPathDA = DataAccess.Factory.Instance.CreatePhotoPathDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IPhotoPathController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeletePhotoPath() As Boolean Implements IPhotoPathController.DeletePhotoPath
            Return _objPhotoPathDA.DeletePhotoPath()
        End Function

        Public Function GetPhotoPathByID() As CommonInfo.PhotoPathInfo Implements IPhotoPathController.GetPhotoPathByID
            Return _objPhotoPathDA.GetPhotoPathByID()
        End Function

        Public Function GetPhotoPathList() As System.Data.DataTable Implements IPhotoPathController.GetPhotoPathList
            Return _objPhotoPathDA.GetPhotoPathList()
        End Function

        Public Function SavePhotoPath(ByVal PhtotoPathobj As CommonInfo.PhotoPathInfo, ByVal OldPhoto As String) As Long Implements IPhotoPathController.SavePhotoPath
            Dim bolret As Boolean
            Dim objGeneralController As General.IGeneralController = Factory.Instance.CreateGeneralController
            If OldPhoto = "" Then

                'PhtotoPathobj.PhotoPathID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.PhotoPath, "PhotoPath", Now)

                'PhtotoPathobj.PhotoPathID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.PhotoPath)

                bolret = _objPhotoPathDA.InsertPhotoPath(PhtotoPathobj)
            Else
                bolret = _objPhotoPathDA.UpdatePhotoPath(PhtotoPathobj)
            End If
            If bolret = True Then Return 1 Else Return 0
        End Function

        Public Function GetPhotoPathForOneFinger(ByVal OneFinger As Integer) As String Implements IPhotoPathController.GetPhotoPathForOneFinger
            Return _objPhotoPathDA.GetPhotoPathForOneFinger(OneFinger)
        End Function
    End Class
End Namespace

