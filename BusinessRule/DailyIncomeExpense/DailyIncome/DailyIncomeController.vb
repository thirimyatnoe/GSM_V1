Imports DataAccess.DailyIncome
'Imports DataAccess.JournalTransaction

Namespace DailyIncome
    Friend Class DailyIncomeController
        Implements IDailyIncomeController

#Region "Private Members"

        Private _objDailyIncomeDA As IDailyIncomeDA
        '       Private _objJournalTransactionDA As IJournalTransactionDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IDailyIncomeController = New DailyIncomeController

#End Region

#Region "Constructors"

        Private Sub New()
            _objDailyIncomeDA = DataAccess.Factory.Instance.CreateDailyIncomeDA
            '          _objJournalTransactionDA = SMS.DataAccess.Factory.Instance.CreateJournalTransactionDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDailyIncomeController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteDailyIncome(ByVal DailyIncomeHeaderID As String) As Boolean Implements IDailyIncomeController.DeleteDailyIncome
            If _objDailyIncomeDA.DeleteDailyIncomeHeader(DailyIncomeHeaderID) Then
                '**** update tbl_JournalTransaction  *****
                '  _objJournalTransactionDA.DeleteJournalTransaction(DailyIncomeHeaderID)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                    DateTime.Now, _
                                                    Global_UserID, _
                                                    CommonInfo.EnumSetting.GenerateKeyType.DailyIncome.ToString, _
                                                    CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                    DailyIncomeHeaderID, _
                                                    "Delete Daily Income")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetDailyIncomeHeader(ByVal DailyIncomeID As String) As CommonInfo.DailyIncomeInfo Implements IDailyIncomeController.GetDailyIncomeHeader
            Return _objDailyIncomeDA.GetDailyIncomeHeader(DailyIncomeID)
        End Function

        Public Function GetDailyIncomeItems(ByVal DailyIncomeID As String) As System.Data.DataTable Implements IDailyIncomeController.GetDailyIncomeItems
            Return _objDailyIncomeDA.GetDailyIncomeItems(DailyIncomeID)
        End Function

        Public Function GetDailyIncomeList() As System.Data.DataTable Implements IDailyIncomeController.GetDailyIncomeList
            Return _objDailyIncomeDA.GetDailyIncomeHeaderList
        End Function

        Public Function SaveDailyIncome(ByRef DailyIncomeObj As CommonInfo.DailyIncomeInfo, ByVal DailyIncomeItems As System.Data.DataTable) As Boolean Implements IDailyIncomeController.SaveDailyIncome
            Dim objGeneralController As General.IGeneralController
            Dim objDailyIncomeItem As CommonInfo.DailyIncomeItemInfo

            Dim dr As DataRow
            Dim bolRet As Boolean

            objGeneralController = Factory.Instance.CreateGeneralController

            If DailyIncomeObj.DailyIncomeID = "" Then
                DailyIncomeObj.DailyIncomeID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.DailyIncome, CommonInfo.EnumSetting.GenerateKeyType.DailyIncome.ToString, DailyIncomeObj.IncomeDate)
                bolRet = _objDailyIncomeDA.InsertDailyIncomeHeader(DailyIncomeObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                    DateTime.Now, _
                                                    Global_UserID, _
                                                    CommonInfo.EnumSetting.GenerateKeyType.DailyIncome.ToString, _
                                                    CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                    DailyIncomeObj.DailyIncomeID, _
                                                    "Insert Daily Income")
            Else
                bolRet = _objDailyIncomeDA.UpdateDailyIncomeHeader(DailyIncomeObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                    DateTime.Now, _
                                                    Global_UserID, _
                                                    CommonInfo.EnumSetting.GenerateKeyType.DailyIncome.ToString, _
                                                    CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                    DailyIncomeObj.DailyIncomeID, _
                                                    "Update Daily Income")
            End If

            '**** update tbl_JournalTransaction  *****
            '     _objJournalTransactionDA.DeleteJournalTransaction(DailyIncomeObj.ReturnID)

            If bolRet = True Then
                For Each dr In DailyIncomeItems.Rows
                    objDailyIncomeItem = New CommonInfo.DailyIncomeItemInfo

                    '**** update tbl_JournalTransaction  *****
                    If dr.RowState <> DataRowState.Deleted Then
                        '                _objJournalTransactionDA.InsertJournalTransaction(DailyIncomeObj.ReturnID, DailyIncomeObj.ReturnDate, dr("ItemStockID"), IIf(dr("UnitItemID") <> 0, dr("Quantity") / _objStockDA.GetPrepIncludedByStockIDandUnitItemID(dr("ItemStockID"), dr("UnitItemID")), dr("Quantity")), CommonInfo.EnumSetting.TransactionType.DailyIncome.ToString, dr("Amount"))
                    End If

                    If dr.RowState = DataRowState.Deleted Then
                        _objDailyIncomeDA.DeleteDailyIncomeItem(dr("DailyIncomeItemID", DataRowVersion.Original))
                    ElseIf dr.RowState = DataRowState.Added Then
                        objDailyIncomeItem.DailyIncomeItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.DailyIncomeItem, CommonInfo.EnumSetting.GenerateKeyType.DailyIncomeItem.ToString, DailyIncomeObj.IncomeDate)
                        objDailyIncomeItem.DailyIncomeID = DailyIncomeObj.DailyIncomeID
                        objDailyIncomeItem.Description = IIf(IsDBNull(dr("Description")), "", dr("Description"))
                        objDailyIncomeItem.QTY = IIf(IsDBNull(dr("QTY")), 0, dr("QTY"))
                        objDailyIncomeItem.UnitPrice = IIf(IsDBNull(dr("UnitPrice")), 0, dr("UnitPrice"))
                        objDailyIncomeItem.Amount = IIf(IsDBNull(dr("Amount")), 0, dr("Amount"))
                        objDailyIncomeItem.Remark = IIf(IsDBNull(dr("Remark")), "", dr("Remark"))
                        _objDailyIncomeDA.InsertDailyIncomeItem(objDailyIncomeItem)
                    ElseIf dr.RowState = DataRowState.Modified Then
                        objDailyIncomeItem.DailyIncomeItemID = dr("DailyIncomeItemID")
                        objDailyIncomeItem.DailyIncomeID = DailyIncomeObj.DailyIncomeID
                        objDailyIncomeItem.Description = IIf(IsDBNull(dr("Description")), 0, dr("Description"))
                        objDailyIncomeItem.QTY = IIf(IsDBNull(dr("QTY")), 0, dr("QTY"))
                        objDailyIncomeItem.UnitPrice = IIf(IsDBNull(dr("UnitPrice")), 0, dr("UnitPrice"))
                        objDailyIncomeItem.Amount = IIf(IsDBNull(dr("Amount")), 0, dr("Amount"))
                        objDailyIncomeItem.Remark = IIf(IsDBNull(dr("Remark")), "", dr("Remark"))
                        _objDailyIncomeDA.UpdateDailyIncomeItem(objDailyIncomeItem)
                    End If
                Next
            End If

            Return bolRet
        End Function

        Public Function GetDailyIncomeExpenseReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal CriStr1 As String, Optional ByVal criStr As String = "") As System.Data.DataTable Implements IDailyIncomeController.GetDailyIncomeExpenseReport
            Return _objDailyIncomeDA.GetDailyIncomeExpenseReport(FromDate, ToDate, CriStr1, criStr)
        End Function
    End Class
End Namespace
