Option Explicit On
Imports CommonInfo.GenerateFormatInfo
Imports DataAccess.GenerateFormat
Imports DataAccess.General
Namespace GenerateFormat
    Public Class GenerateFormatController
        Implements IGenerateFormatController

#Region "Private Members"
        Dim _objGenerateFormatDA As IGenerateFormatDA = DataAccess.Factory.Instance.CreateGenerateFormatDA
        Dim _objGeneralDA As IGeneralDA = DataAccess.Factory.Instance.CreateGeneralDA
        Private Shared ReadOnly _instance As IGenerateFormatController = New GenerateFormatController

#End Region

#Region "Constructor"
        Private Sub New()
            _objGenerateFormatDA = DataAccess.Factory.Instance.CreateGenerateFormatDA
            _objGeneralDA = DataAccess.Factory.Instance.CreateGeneralDA
        End Sub
#End Region

#Region "Public Property"
        Public Shared ReadOnly Property Instance() As IGenerateFormatController
            Get
                Return _instance
            End Get
        End Property
#End Region



        Public Function Save_GenerateFormat(ByVal objGenerateFormat As CommonInfo.GenerateFormatInfo) As Boolean Implements IGenerateFormatController.Save_GenerateFormat
            Dim _GeneralController As BusinessRule.General.IGeneralController = BusinessRule.Factory.Instance.CreateGeneralController

            If objGenerateFormat.GenerateFormatID = 0 Then objGenerateFormat.GenerateFormatID = _objGeneralDA.GetMaxID("tbl_GenerateFormat", "GenerateFormatID")
            Return _objGenerateFormatDA.Insert_GenerateFormat(objGenerateFormat)

        End Function

        Public Function GetDateFormatData() As System.Data.DataTable Implements IGenerateFormatController.GetDateFormatData
            Return _objGenerateFormatDA.GetDateFormat()
        End Function

        Public Function GetSecondDateFormatData() As System.Data.DataTable Implements IGenerateFormatController.GetSecondDateFormatData
            Return _objGenerateFormatDA.GetSecondDateFormat()
        End Function

        Public Function GetGenerateFormat(ByVal GenerateFormatID As Integer) As CommonInfo.GenerateFormatInfo Implements IGenerateFormatController.GetGenerateFormat
            Return _objGenerateFormatDA.Get_GenerateFormatByID(GenerateFormatID)
        End Function

        Public Function GetGenerateFormatList() As System.Data.DataTable Implements IGenerateFormatController.GetGenerateFormatList
            Return _objGenerateFormatDA.Get_GenerateFormat()
        End Function

        Public Function DeleteGenerateFormat(ByVal GenerateFormatID As Integer) As Boolean Implements IGenerateFormatController.DeleteGenerateFormat
            Return _objGenerateFormatDA.Delete_GenerateFormat(GenerateFormatID)
        End Function

        Public Function GetGenerateFormatByFormat(GenerateFormat As String) As CommonInfo.GenerateFormatInfo Implements IGenerateFormatController.GetGenerateFormatByFormat
            Return _objGenerateFormatDA.GetGenerateFormatByFormat(GenerateFormat)
        End Function
    End Class
End Namespace

