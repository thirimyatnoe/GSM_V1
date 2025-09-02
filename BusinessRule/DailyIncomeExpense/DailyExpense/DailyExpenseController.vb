Imports DataAccess.DailyExpense
'Imports DataAccess.JournalTransaction

Namespace DailyExpense
    Friend Class DailyExpenseController
        Implements IDailyExpenseController


#Region "Private Members"

        Private _objDailyExpenseDA As IDailyExpenseDA
        '       Private _objJournalTransactionDA As IJournalTransactionDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IDailyExpenseController = New DailyExpenseController

#End Region

#Region "Constructors"

        Private Sub New()
            _objDailyExpenseDA = DataAccess.Factory.Instance.CreateDailyExpenseDA
            '          _objJournalTransactionDA = SMS.DataAccess.Factory.Instance.CreateJournalTransactionDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDailyExpenseController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteDailyExpense(ByVal DailyExpenseHeaderID As String) As Boolean Implements IDailyExpenseController.DeleteDailyExpense
            If _objDailyExpenseDA.DeleteDailyExpenseHeader(DailyExpenseHeaderID) Then
                '**** update tbl_JournalTransaction  *****
                '  _objJournalTransactionDA.DeleteJournalTransaction(DailyExpenseHeaderID)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                     DateTime.Now, _
                                                     Global_UserID, _
                                                     CommonInfo.EnumSetting.GenerateKeyType.DailyExpense.ToString, _
                                                     CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                     DailyExpenseHeaderID, _
                                                     "Delete Daily Expense")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetDailyExpenseHeader(ByVal DailyExpenseID As String) As CommonInfo.DailyExpenseInfo Implements IDailyExpenseController.GetDailyExpenseHeader
            Return _objDailyExpenseDA.GetDailyExpenseHeader(DailyExpenseID)
        End Function

        Public Function GetDailyExpenseItems(ByVal DailyExpenseID As String) As System.Data.DataTable Implements IDailyExpenseController.GetDailyExpenseItems
            Return _objDailyExpenseDA.GetDailyExpenseItems(DailyExpenseID)
        End Function

        Public Function GetDailyExpenseList() As System.Data.DataTable Implements IDailyExpenseController.GetDailyExpenseList
            Return _objDailyExpenseDA.GetDailyExpenseHeaderList
        End Function

        Public Function SaveDailyExpense(ByRef DailyExpenseObj As CommonInfo.DailyExpenseInfo, ByVal DailyExpenseItems As System.Data.DataTable) As Boolean Implements IDailyExpenseController.SaveDailyExpense
            Dim objGeneralController As General.IGeneralController
            Dim objDailyExpenseItem As CommonInfo.DailyExpenseItemInfo

            Dim dr As DataRow
            Dim bolRet As Boolean

            objGeneralController = Factory.Instance.CreateGeneralController

            If DailyExpenseObj.DailyExpenseID = "0" Then
                DailyExpenseObj.DailyExpenseID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.DailyExpense, CommonInfo.EnumSetting.GenerateKeyType.DailyExpense.ToString, DailyExpenseObj.ExpenseDate)
                bolRet = _objDailyExpenseDA.InsertDailyExpenseHeader(DailyExpenseObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                     DateTime.Now, _
                                                     Global_UserID, _
                                                     CommonInfo.EnumSetting.GenerateKeyType.DailyExpense.ToString, _
                                                     CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                     DailyExpenseObj.DailyExpenseID, _
                                                     "Insert Daily Expense")
            Else
                bolRet = _objDailyExpenseDA.UpdateDailyExpenseHeader(DailyExpenseObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                     DateTime.Now, _
                                                     Global_UserID, _
                                                     CommonInfo.EnumSetting.GenerateKeyType.DailyExpense.ToString, _
                                                     CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                     DailyExpenseObj.DailyExpenseID, _
                                                     "Update Daily Expense")
            End If

            '**** update tbl_JournalTransaction  *****
            '     _objJournalTransactionDA.DeleteJournalTransaction(DailyExpenseObj.ReturnID)

            If bolRet = True Then
                For Each dr In DailyExpenseItems.Rows
                    objDailyExpenseItem = New CommonInfo.DailyExpenseItemInfo


                    If dr.RowState = DataRowState.Deleted Then
                        _objDailyExpenseDA.DeleteDailyExpenseItem(dr("DailyExpenseItemID", DataRowVersion.Original))

                    ElseIf dr.RowState = DataRowState.Added Then

                        objDailyExpenseItem.DailyExpenseItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.DailyExpenseItem, CommonInfo.EnumSetting.GenerateKeyType.DailyExpenseItem.ToString, DailyExpenseObj.ExpenseDate)
                        objDailyExpenseItem.DailyExpenseID = DailyExpenseObj.DailyExpenseID
                        objDailyExpenseItem.Description = IIf(IsDBNull(dr("Description")), 0, dr("Description"))
                        objDailyExpenseItem.QTY = IIf(IsDBNull(dr("QTY")), 0, dr("QTY"))
                        objDailyExpenseItem.UnitPrice = IIf(IsDBNull(dr("UnitPrice")), 0, dr("UnitPrice"))
                        objDailyExpenseItem.Amount = IIf(IsDBNull(dr("Amount")), 0, dr("Amount"))
                        objDailyExpenseItem.Remark = IIf(IsDBNull(dr("Remark")), "", dr("Remark"))

                        _objDailyExpenseDA.InsertDailyExpenseItem(objDailyExpenseItem)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        objDailyExpenseItem.DailyExpenseItemID = dr("DailyExpenseItemID")
                        objDailyExpenseItem.DailyExpenseID = DailyExpenseObj.DailyExpenseID
                        objDailyExpenseItem.Description = IIf(IsDBNull(dr("Description")), 0, dr("Description"))
                        objDailyExpenseItem.QTY = IIf(IsDBNull(dr("QTY")), 0, dr("QTY"))
                        objDailyExpenseItem.UnitPrice = IIf(IsDBNull(dr("UnitPrice")), 0, dr("UnitPrice"))
                        objDailyExpenseItem.Amount = IIf(IsDBNull(dr("Amount")), 0, dr("Amount"))
                        objDailyExpenseItem.Remark = IIf(IsDBNull(dr("Remark")), "", dr("Remark"))

                        _objDailyExpenseDA.UpdateDailyExpenseItem(objDailyExpenseItem)
                    End If
                Next
            End If

            Return bolRet
        End Function

    End Class
End Namespace
