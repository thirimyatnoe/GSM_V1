Imports DataAccess.ReturnAdvance
Imports CommonInfo
Namespace ReturnAdvance
    Public Class ReturnAdvanceController
        Implements IReturnAdvanceController
#Region "Private Members"

        Private _objReturnAdvanceDA As IReturnAdvanceDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IReturnAdvanceController = New ReturnAdvanceController

#End Region

#Region "Constructors"

        Private Sub New()
            _objReturnAdvanceDA = DataAccess.Factory.Instance.CreateReturnAdvanceDA
        End Sub
#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IReturnAdvanceController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteReturnAdvance(ByVal ReturnAdvanceID As String) As Boolean Implements IReturnAdvanceController.DeleteReturnAdvance
            If _objReturnAdvanceDA.DeleteReturnAdvance(ReturnAdvanceID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesGem.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       ReturnAdvanceID, _
                                                       "Delete Return Advance")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetReturnAdvance(ByVal ReturnAdvanceID As String) As CommonInfo.ReturnAdvanceInfo Implements IReturnAdvanceController.GetReturnAdvance
            Return _objReturnAdvanceDA.GetReturnAdvance(ReturnAdvanceID)
        End Function

        Public Function GetReturnAdvanceItem(ByVal ReturnAdvanceID As String) As System.Data.DataTable Implements IReturnAdvanceController.GetReturnAdvanceItem
            Return _objReturnAdvanceDA.GetReturnAdvanceItem(ReturnAdvanceID)
        End Function

        Public Function SaveReturnAdvance(ByVal ReturnAdvanceObj As CommonInfo.ReturnAdvanceInfo, ByVal _dtReturnAdvanceItem As System.Data.DataTable) As Boolean Implements IReturnAdvanceController.SaveReturnAdvance
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo '
            Dim bolRet As Boolean = True
            If ReturnAdvanceObj.ReturnAdvanceID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.ReturnAdvance.ToString)
                ReturnAdvanceObj.ReturnAdvanceID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, ReturnAdvanceObj.ReturnAdvanceDate)

                bolRet = _objReturnAdvanceDA.InsertReturnAdvance(ReturnAdvanceObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesGem.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                       ReturnAdvanceObj.ReturnAdvanceID, _
                                                       "Insert Return Advance")
            Else
                bolRet = _objReturnAdvanceDA.UpdateReturnAdvance(ReturnAdvanceObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesGem.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                       ReturnAdvanceObj.ReturnAdvanceID, _
                                                       "Update Return Advance")
            End If
            If bolRet Then
                For Each dr As DataRow In _dtReturnAdvanceItem.Rows
                    Dim objReturnAdvanceItem As New ReturnAdvanceItemInfo
                    If dr.RowState = DataRowState.Added Then
                        With objReturnAdvanceItem
                            .ReturnAdvanceID = ReturnAdvanceObj.ReturnAdvanceID
                            .ReturnAdvanceItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ReturnAdvanceItem, "ReturnAdvanceItemID", ReturnAdvanceObj.ReturnAdvanceDate)
                            .ItemTG = IIf(IsDBNull(dr.Item("ItemTG")) = True, 0, dr.Item("ItemTG"))
                            .SaleRate = IIf(IsDBNull(dr.Item("SaleRate")) = True, 0, dr.Item("SaleRate"))
                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")) = True, 0, dr.Item("Amount"))
                            .Remark = IIf(IsDBNull(dr.Item("Remark")) = True, "-", dr.Item("Remark"))
                            .IsUsed = IIf(IsDBNull(dr.Item("IsUsed")) = True, False, dr.Item("IsUsed"))
                        End With
                        bolRet = _objReturnAdvanceDA.InsertReturnAdvanceItem(objReturnAdvanceItem)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objReturnAdvanceItem
                            .ReturnAdvanceID = ReturnAdvanceObj.ReturnAdvanceID
                            .ReturnAdvanceItemID = dr.Item("ReturnAdvanceItemID")
                            .ItemTG = IIf(IsDBNull(dr.Item("ItemTG")) = True, 0, dr.Item("ItemTG"))
                            .SaleRate = IIf(IsDBNull(dr.Item("SaleRate")) = True, 0, dr.Item("SaleRate"))
                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")) = True, 0, dr.Item("Amount"))
                            .Remark = IIf(IsDBNull(dr.Item("Remark")) = True, "-", dr.Item("Remark"))
                            .IsUsed = IIf(IsDBNull(dr.Item("IsUsed")) = True, False, dr.Item("IsUsed"))
                        End With
                        bolRet = _objReturnAdvanceDA.UpdateReturnAdvanceItem(objReturnAdvanceItem)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objReturnAdvanceDA.DeleteReturnAdvanceItem(CStr(dr.Item("ReturnAdvanceItemID", DataRowVersion.Original)))
                    End If
                Next
            End If

            Return bolRet
        End Function

        Public Function GetReturnAdvanceItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal optType As String = "") As System.Data.DataTable Implements IReturnAdvanceController.GetReturnAdvanceItemForRpt
            Return _objReturnAdvanceDA.GetReturnAdvanceItemForRpt(FromDate, ToDate, cristr, optType)
        End Function

        'Public Function GetAllReturnAdvanceForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IReturnAdvanceController.GetAllReturnAdvanceForRpt
        '    Return _objReturnAdvanceDA.GetAllReturnAdvanceForRpt(FromDate, ToDate, cristr)
        'End Function

        Public Function GetAllReturnAdvance() As System.Data.DataTable Implements IReturnAdvanceController.GetAllReturnAdvance
            Return _objReturnAdvanceDA.GetAllReturnAdvance()
        End Function
        Public Function GetAllReturnAdvanceInCashReceipt() As System.Data.DataTable Implements IReturnAdvanceController.GetAllReturnAdvanceInCashReceipt
            Return _objReturnAdvanceDA.GetAllReturnAdvanceInCashReceipt()
        End Function
        Public Function GetReturnAdvancePrint(ByVal ReturnAdvanceID As String) As System.Data.DataTable Implements IReturnAdvanceController.GetReturnAdvancePrint
            Return _objReturnAdvanceDA.GetReturnAdvancePrint(ReturnAdvanceID)
        End Function
        'Public Function InsertReturnAdvanceUserID(ByVal ReturnAdvanceID As String, ByVal UserID As String) As Boolean Implements IReturnAdvanceController.InsertReturnAdvanceUserID
        '    Return _objReturnAdvanceDA.InsertReturnAdvanceUserID(ReturnAdvanceID, UserID)
        'End Function
        Public Function GetReturnAdvanceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IReturnAdvanceController.GetReturnAdvanceReportForTotal
            Return _objReturnAdvanceDA.GetReturnAdvanceReportForTotal(FromDate, ToDate, cristr)
        End Function

        'Public Function GetReturnAdvanceBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As CommonInfo.ReturnAdvanceItemInfo Implements IReturnAdvanceController.GetReturnAdvanceBalanceStockByGemsCategoryID
        '    Return _objReturnAdvanceDA.GetReturnAdvanceBalanceStockByGemsCategoryID(GemsCategoryID)
        'End Function
        'Public Function GetAllReturnAdvanceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IReturnAdvanceController.GetAllReturnAdvanceVoucherPrint
        '    Return _objReturnAdvanceDA.GetAllReturnAdvanceVoucherPrint(FromDate, ToDate, cristr)
        'End Function
        'Public Function GetReturnAdvanceItemByReturnAdvanceItemID(ByVal ReturnAdvanceItemID As String) As System.Data.DataTable Implements IReturnAdvanceController.GetReturnAdvanceItemByReturnAdvanceItemID
        '    Return _objReturnAdvanceDA.GetReturnAdvanceItemByReturnAdvanceItemID(ReturnAdvanceItemID)
        'End Function
    End Class
End Namespace

