Imports DataAccess.Supplier
Imports CommonInfo
Namespace Supplier
    Public Class SupplierController
        Implements ISupplierController

#Region "Private Members"

        Private _objSupplierDA As ISupplierDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISupplierController = New SupplierController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSupplierDA = DataAccess.Factory.Instance.CreateSupplierDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISupplierController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteSupplier(ByVal SupplierID As String) As Boolean Implements ISupplierController.DeleteSupplier
            If _objSupplierDA.DeleteSupplier(SupplierID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Supplier.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                          SupplierID, _
                                          "Delete Supplier")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function SaveSupplier(ByVal obj As CommonInfo.SupplierInfo) As String Implements ISupplierController.SaveSupplier
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False
            If obj.SupplierID = "" Then
                obj.SupplierID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.Supplier, CommonInfo.EnumSetting.GenerateKeyType.Supplier.ToString, Now)
                obj.SupplierCode = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.SupplierCode, EnumSetting.GenerateKeyType.SupplierCode, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Supplier.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                          obj.SupplierID, _
                                          "Insert Supplier")
                bolRet = _objSupplierDA.InsertSupplier(obj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Supplier.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                          obj.SupplierID, _
                                          "Update Supplier")
                bolRet = _objSupplierDA.UpdateSupplier(obj)
            End If
            If bolRet = True Then
                Return obj.SupplierID
            Else
                Return ""
            End If
        End Function

        Public Function GetAllSupplier() As System.Data.DataTable Implements ISupplierController.GetAllSupplier
            Return _objSupplierDA.GetAllSupplier()
        End Function
        Public Function GetSupplierByID(ByVal SupplierID As String) As CommonInfo.SupplierInfo Implements ISupplierController.GetSupplierByID
            Return _objSupplierDA.GetSupplierByID(SupplierID)
        End Function
        Public Function GetSupplierDataByCode(ByVal SupplierCode As String) As CommonInfo.SupplierInfo Implements ISupplierController.GetSupplierDataByCode
            Return _objSupplierDA.GetSupplierDataByCode(SupplierCode)
        End Function
        Public Function GetAllSupplierList() As System.Data.DataTable Implements ISupplierController.GetAllSupplierList
            Return _objSupplierDA.GetAllSupplierList()
        End Function
        Public Function GetAllSupplierListByLocation(ByVal LocationID As String) As System.Data.DataTable Implements ISupplierController.GetAllSupplierListByLocation
            Return _objSupplierDA.GetAllSupplierListByLocation(LocationID)
        End Function

    End Class
End Namespace

