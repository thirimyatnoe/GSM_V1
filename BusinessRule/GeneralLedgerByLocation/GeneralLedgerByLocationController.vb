Imports DataAccess.GeneralLedgerByLocation
Namespace GeneralLedgerByLocation
    Public Class GeneralLedgerByLocationController
        Implements IGeneralLedgerByLocationController

#Region "Private Members"

        Private _objGeneralLedgerByLocationDA As IGeneralLedgerByLocationDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IGeneralLedgerByLocationController = New GeneralLedgerByLocationController

#End Region

#Region "Constructors"

        Private Sub New()
            _objGeneralLedgerByLocationDA = DataAccess.Factory.Instance.CreateGeneralLedgerByLocationDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGeneralLedgerByLocationController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function GetGeneralLedgerByLocationByDateFromTransaction(ByVal cristr As String, ByVal ForDate As Date, ByVal ToDate As Date) As System.Data.DataTable Implements IGeneralLedgerByLocationController.GetGeneralLedgerByLocationByDateFromTransaction
            Return _objGeneralLedgerByLocationDA.GetGeneralLedgerByLocationByDateFromTransaction(cristr, ForDate, ToDate)
        End Function

        Public Function SaveGeneralLedgerByLocationByDate(ByVal LocationID As String, ByVal GLDate As DateTime, ByVal StaffID As String, ByVal dtGLByLocationByDate As DataTable) As Boolean Implements IGeneralLedgerByLocationController.SaveGeneralLedgerByLocationByDate
            Dim bolret As Boolean
            Dim LedgerId As String
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            bolret = _objGeneralLedgerByLocationDA.DeleteGeneralLedgerForLocationByDate(LocationID, GLDate.Date)
            For Each dr As DataRow In dtGLByLocationByDate.Rows
                LedgerId = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.GeneralLedgerByLocation, CommonInfo.EnumSetting.GenerateKeyType.GeneralLedgerByLocation.ToString, Now)
                bolret = _objGeneralLedgerByLocationDA.InsertGeneralLedgerForLocationByDate(LocationID, GLDate, StaffID, dr.Item("Title"), dr.Item("DebitAmount"), dr.Item("CreditAmount"), dr.Item("Type"), dr.Item("MyanTitle"), Now, LedgerId)
            Next
            _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                               DateTime.Now, _
                               Global_UserID, _
                               CommonInfo.EnumSetting.GenerateKeyType.GeneralLedgerByLocation.ToString, _
                               CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                               LocationID & " - " & GLDate.ToString("dd-MM-yyyy"), _
                              "Insert GeneralLedgerByLocation")
            Return bolret
        End Function

        Public Function GetGeneralLedgerByLocationByDate(ByVal LocationID As String, ByVal ForDate As Date) As System.Data.DataTable Implements IGeneralLedgerByLocationController.GetGeneralLedgerByLocationByDate
            Return _objGeneralLedgerByLocationDA.GetGeneralLedgerByLocationByDate(LocationID, ForDate)
        End Function

        Public Function GetGeneralLedgerByLocation(ByVal ForDate As Date, ByVal Cristr As String, ByVal LocationID As String) As DataTable Implements IGeneralLedgerByLocationController.GetGeneralLedgerByLocation
            Return _objGeneralLedgerByLocationDA.GetGeneralLedgerByLocation(ForDate, Cristr, LocationID)
        End Function

        Public Function GetAllCashInCashOutReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal CriStr As String, ByVal LocationID As String) As System.Data.DataSet Implements IGeneralLedgerByLocationController.GetAllCashInCashOutReport
            Return _objGeneralLedgerByLocationDA.GetAllCashInCashOutReport(FromDate, ToDate, CriStr, LocationID)
        End Function

        Public Function GetDailyTransactonByLocation(ForDate As Date, LocationID As String, ByVal CriStr As String) As DataTable Implements IGeneralLedgerByLocationController.GetDailyTransactonByLocation
            Return _objGeneralLedgerByLocationDA.GetDailyTransactonByLocation(ForDate, LocationID, CriStr)
        End Function
        Public Function GetAllOtherCashDataByGeneralLedger(ByVal ForDate As Date) As DataTable Implements IGeneralLedgerByLocationController.GetAllOtherCashDataByGeneralLedger
            Return _objGeneralLedgerByLocationDA.GetAllOtherCashDataByGeneralLedger(ForDate)
        End Function
        Public Function GetAllRecordOtherCashDataByDate(ByVal ForDate As Date) As DataTable Implements IGeneralLedgerByLocationController.GetAllRecordOtherCashDataByDate
            Return _objGeneralLedgerByLocationDA.GetAllRecordOtherCashDataByDate(ForDate)
        End Function
        Public Function GetAllRecordOtherCashData(ByVal FromDate As Date, ByVal ToDate As Date) As System.Data.DataTable Implements IGeneralLedgerByLocationController.GetAllRecordOtherCashData
            Return _objGeneralLedgerByLocationDA.GetAllRecordOtherCashData(FromDate, ToDate)
        End Function

        Public Function GetAllCustomerTransaction(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IGeneralLedgerByLocationController.GetAllCustomerTransaction
            Return _objGeneralLedgerByLocationDA.GetAllCustomerTransaction(FromDate, ToDate, cristr)
        End Function
        Public Function GetAllCustomerReceipt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IGeneralLedgerByLocationController.GetAllCustomerReceipt
            Return _objGeneralLedgerByLocationDA.GetAllCustomerReceipt(FromDate, ToDate, cristr)
        End Function
        Public Function GetAllCashTransaction(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal Type As String = "", Optional ByVal GoldQualityID As String = "", Optional ByVal CustomerCode As String = "", Optional ByVal LocationID As String = "", Optional ByVal CustomerID As String = "", Optional ByVal str As String = "") As System.Data.DataTable Implements IGeneralLedgerByLocationController.GetAllCashTransaction
            Return _objGeneralLedgerByLocationDA.GetAllCashTransaction(FromDate, ToDate, cristr, Type, GoldQualityID, CustomerCode, LocationID, CustomerID, str)
        End Function
    End Class
End Namespace

