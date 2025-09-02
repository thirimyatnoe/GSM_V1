Imports DataAccess.Customer
Imports CommonInfo
Namespace Customer
    Public Class CustomerController
        Implements ICustomerController

#Region "Private Members"

        Private _objCustomerDA As ICustomerDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ICustomerController = New CustomerController

#End Region

#Region "Constructors"

        Private Sub New()
            _objCustomerDA = DataAccess.Factory.Instance.CreateCustomerDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICustomerController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteCustomer(ByVal CustomerID As String) As Boolean Implements ICustomerController.DeleteCustomer
            If _objCustomerDA.DeleteCustomer(CustomerID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Customer.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                          CustomerID, _
                                          "Delete Customer")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function SaveCustomer(ByVal obj As CommonInfo.CustomerInfo) As String Implements ICustomerController.SaveCustomer
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False
            If obj.CustomerID = "" Then
                obj.CustomerID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.Customer, CommonInfo.EnumSetting.GenerateKeyType.Customer.ToString, Now)
                obj.CustomerCode = objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.CustomerCode, EnumSetting.GenerateKeyType.CustomerCode, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Customer.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                          obj.CustomerID, _
                                          "Insert Customer")
                bolRet = _objCustomerDA.InsertCustomer(obj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Customer.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                          obj.CustomerID, _
                                          "Update Customer")
                bolRet = _objCustomerDA.UpdateCustomer(obj)
            End If
            If bolRet = True Then
                Return obj.CustomerID
            Else
                Return ""
            End If
        End Function

        Public Function GetAllCustomer() As System.Data.DataTable Implements ICustomerController.GetAllCustomer
            Return _objCustomerDA.GetAllCustomer()
        End Function
        Public Function GetCustomerByID(ByVal CustomerID As String) As CommonInfo.CustomerInfo Implements ICustomerController.GetCustomerByID
            Return _objCustomerDA.GetCustomerByID(CustomerID)
        End Function

        Public Function GetCustomerCode(ByVal _CustomerCode As String) As CommonInfo.CustomerInfo Implements ICustomerController.GetCustomerCode
            Return _objCustomerDA.GetCustomerCode(_CustomerCode)
        End Function


        Public Function GetCustomer() As DataTable Implements ICustomerController.GetCustomer
            Return _objCustomerDA.GetCustomer()
        End Function


        Public Function GetCustomerDataByCustomerName(ByVal CustomerName As String, Optional CustomerID As String = "") As DataTable Implements ICustomerController.GetCustomerDataByCustomerName
            Return _objCustomerDA.GetCustomerDataByCustomerName(CustomerName, CustomerID)
        End Function
        Public Function GetAllCustomerAutoCompleteData(Optional CustomerName As String = "") As System.Data.DataTable Implements ICustomerController.GetAllCustomerAutoCompleteData
            Return _objCustomerDA.GetAllCustomerAutoCompleteData(CustomerName)
        End Function
        Public Function GetAllCustomerForSearch(Optional CustomerName As String = "") As System.Data.DataTable Implements ICustomerController.GetAllCustomerForSearch
            Return _objCustomerDA.GetAllCustomerForSearch(CustomerName)
        End Function
    End Class
End Namespace

