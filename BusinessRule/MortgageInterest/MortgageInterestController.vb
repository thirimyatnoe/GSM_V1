Imports DataAccess.MortgageInterest

Imports CommonInfo
Namespace MortgageInterest
    Public Class MortgageInterestController
        Implements IMortgageInterestController

#Region "Private Members"

        Private _objMortgageInterestDA As IMortgageInterestDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IMortgageInterestController = New MortgageInterestController

#End Region

#Region "Constructors"

        Private Sub New()
            _objMortgageInterestDA = DataAccess.Factory.Instance.CreateMortgageInterestDA
           
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgageInterestController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteMortgageInterest(ByVal MortgageInvoiceID As String) As Boolean Implements IMortgageInterestController.DeleteMortgageInterest
            If _objMortgageInterestDA.DeleteMortgageInterest(MortgageInvoiceID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.MortgageInterest.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       MortgageInvoiceID, _
                                                       "Delete Mortgage Interest")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetMortgageInterest(ByVal MortgageInterestID As String) As CommonInfo.MortgageInterestInfo Implements IMortgageInterestController.GetMortgageInterest
            Return _objMortgageInterestDA.GetMortgageInterest(MortgageInterestID)
        End Function

        Public Function SaveMortgageInterest(ByVal MortgageInterestObj As CommonInfo.MortgageInterestInfo) As Boolean Implements IMortgageInterestController.SaveMortgageInterest
            Dim objGeneralController As General.IGeneralController

            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            If MortgageInterestObj.MortgageInterestID = "0" Then
                MortgageInterestObj.MortgageInterestID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.MortgageInterest, "MortgageInterestID", MortgageInterestObj.FromDate)
                bolRet = _objMortgageInterestDA.InsertMortgageInterest(MortgageInterestObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.MortgageInterest.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       MortgageInterestObj.MortgageInterestID, _
                                       "Insert Mortgage Interest")
            Else
                bolRet = _objMortgageInterestDA.UpdateMortgageInterest(MortgageInterestObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.MortgageInterest.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                        MortgageInterestObj.MortgageInterestID, _
                                       "Update Mortgage Interest")
            End If
            
            Return bolRet
        End Function

        Public Function GetMortgageInterestDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestController.GetMortgageInterestDataTable
            Return _objMortgageInterestDA.GetMortgageInterestDataTable(MortgageInvoiceID)
        End Function

        Public Function GetMortgageInterestHistoryDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestController.GetMortgageInterestHistoryDataTable
            Return _objMortgageInterestDA.GetMortgageInterestHistoryDataTable(MortgageInvoiceID)
        End Function

        Public Function GetAllMortgageInterestList() As System.Data.DataTable Implements IMortgageInterestController.GetAllMortgageInterestList
            Return _objMortgageInterestDA.GetAllMortgageInterestList
        End Function

        Public Function GetAllMortgageInterestFromSearchBox() As System.Data.DataTable Implements IMortgageInterestController.GetAllMortgageInterestFromSearchBox
            Return _objMortgageInterestDA.GetAllMortgageInterestFromSearchBox()
        End Function
        Public Function GetMortgageInterestPrint(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestController.GetMortgageInterestPrint
            Return _objMortgageInterestDA.GetMortgageInterestPrint(MortgageInvoiceID)
        End Function
        Public Function GetMortgageInterestDate(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInterestController.GetMortgageInterestDate
            Return _objMortgageInterestDA.GetMortgageInterestDate(MortgageInvoiceID)
        End Function
        Public Function GetMortgageInterestByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As System.Data.DataTable Implements IMortgageInterestController.GetMortgageInterestByDate
            Return _objMortgageInterestDA.GetMortgageInterestByDate(MortgageInvoiceID, TDate)
        End Function

    End Class
End Namespace

