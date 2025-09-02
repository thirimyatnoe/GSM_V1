Imports DataAccess.CashType
Imports CommonInfo
Namespace CashType
    Public Class CashTypeController
        Implements ICashTypeController

#Region "Private Members"

        Private _objCashTypeDA As ICashTypeDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ICashTypeController = New CashTypeController

#End Region

#Region "Constructors"

        Private Sub New()
            _objCashTypeDA = DataAccess.Factory.Instance.CreateCashTypeDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICashTypeController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetCashTypeList() As System.Data.DataTable Implements ICashTypeController.GetCashTypeList
            Return _objCashTypeDA.GetCashTypeList
        End Function
        Public Function SaveCashType(ByVal _dtCash As System.Data.DataTable) As Boolean Implements ICashTypeController.SaveCashType
            Dim dr As DataRow
            Dim obj As CommonInfo.CashTypeInfo
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            For Each dr In _dtCash.Rows
                obj = New CommonInfo.CashTypeInfo

                If dr.RowState = DataRowState.Deleted Then
                    _objCashTypeDA.DeleteCashType(dr("CashTypeID", DataRowVersion.Original))
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                        DateTime.Now, _
                                        Global_UserID, _
                                        CommonInfo.EnumSetting.GenerateKeyType.CashType.ToString, _
                                        CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                        dr("CashTypeID", DataRowVersion.Original), _
                                        "Delete CashType")

                ElseIf dr.RowState = DataRowState.Added Then

                    If Not IsDBNull(dr("CashType")) Then
                        obj.CashTypeID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.CashType, "CashTypeID", Now)
                        obj.CashType = dr("CashType")
                        _objCashTypeDA.InsertCashType(obj)
                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                        DateTime.Now, _
                                        Global_UserID, _
                                        CommonInfo.EnumSetting.GenerateKeyType.CashType.ToString, _
                                        CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                        obj.CashTypeID, _
                                        "Insert CashType")
                    End If

                ElseIf dr.RowState = DataRowState.Modified Then

                    If Not IsDBNull(dr("CashType")) Then
                        obj.CashTypeID = dr("CashTypeID")
                        obj.CashType = dr("CashType")
                        _objCashTypeDA.UpdateCashType(obj)
                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.CashType.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.CashTypeID, _
                                       "Update CashType")

                    End If
                End If
            Next
            Return True
        End Function

        Public Function GetCashTypeDataByCashType(ByVal CashType As String, Optional CashTypeID As String = "") As DataTable Implements ICashTypeController.GetCashTypeDataByCashType
            Return _objCashTypeDA.GetCashTypeDataByCashType(CashType, CashTypeID)
        End Function
    End Class
End Namespace

