Imports DataAccess.CashReceipt
Imports DataAccess.SalesItem
Imports DataAccess.ReturnAdvance
Imports CommonInfo
Namespace CashReceipt
    Public Class CashReceiptController
        Implements ICashReceiptController

#Region "Private Members"

        Private _ReturnAdvanceDA As IReturnAdvanceDA
        Private _objCashReceiptDA As ICashReceiptDA
        Private _objSaleItemDA As ISalesItemDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ICashReceiptController = New CashReceiptController

#End Region

#Region "Constructors"

        Private Sub New()
            _objCashReceiptDA = DataAccess.Factory.Instance.CreateCashReceiptDA
            _objSaleItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
            _ReturnAdvanceDA = DataAccess.Factory.Instance.CreateReturnAdvanceDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICashReceiptController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetOrderCashReceipt(ByVal IsCash As Boolean) As System.Data.DataTable Implements ICashReceiptController.GetOrderCashReceipt
            Return _objCashReceiptDA.GetOrderCashReceipt(IsCash)
        End Function

        Public Function GetPurchaseGemsCashReceipt() As System.Data.DataTable Implements ICashReceiptController.GetPurchaseGemsCashReceipt
            Return _objCashReceiptDA.GetPurchaseGemsCashReceipt
        End Function

        Public Function GetPurchaseInvoiceCashReceipt() As System.Data.DataTable Implements ICashReceiptController.GetPurchaseInvoiceCashReceipt
            Return _objCashReceiptDA.GetPurchaseInvoiceCashReceipt
        End Function

        Public Function GetSaleGemsCashReceipt(ByVal IsCash As Boolean) As System.Data.DataTable Implements ICashReceiptController.GetSaleGemsCashReceipt
            Return _objCashReceiptDA.GetSaleGemsCashReceipt(IsCash)
        End Function

        Public Function GetSaleInvoiceCashReceipt() As System.Data.DataTable Implements ICashReceiptController.GetSaleInvoiceCashReceipt
            Return _objCashReceiptDA.GetSaleInvoiceCashReceipt
        End Function

        Public Function GetWholeSalesCashReceipt() As System.Data.DataTable Implements ICashReceiptController.GetWholeSalesCashReceipt
            Return _objCashReceiptDA.GetWholeSalesCashReceipt
        End Function

        Public Function GetCashReceipt(ByVal VoucherNo As String, Optional ByVal Type As String = "") As System.Data.DataTable Implements ICashReceiptController.GetCashReceipt
            Return _objCashReceiptDA.GetCashReceipt(VoucherNo, Type)
        End Function
        Public Function GetDebtDataByType(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "", Optional ByVal Str As String = "") As DataTable Implements ICashReceiptController.GetDebtDataByType
            Return _objCashReceiptDA.GetDebtDataByType(argType, FromDate, ToDate, criStr, Str)
        End Function

        Public Function GetDebtInDataByType(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "", Optional ByVal Str As String = "") As DataTable Implements ICashReceiptController.GetDebtInDataByType
            Return _objCashReceiptDA.GetDebtInDataByType(argType, FromDate, ToDate, criStr, Str)
        End Function

        Public Function DeleteCashReceipt(ByVal CashReceiptID As String) As Boolean Implements ICashReceiptController.DeleteCashReceipt
            If _objCashReceiptDA.DeleteCashReceipt(CashReceiptID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                           DateTime.Now, _
                           Global_UserID, _
                           CommonInfo.EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString, _
                           CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                           CashReceiptID, _
                           "Delete Cash Receipt")
                Return True
            Else
                Return False
            End If
        End Function
        Private Sub UpdateReturnAdvanceItemIsUsed(ByVal agrReturnAdvanceID As String, ByVal argIsUsed As Boolean)
            Dim objReturnAdvanceItem As New CommonInfo.ReturnAdvanceItemInfo
            With objReturnAdvanceItem
                .ReturnAdvanceID = agrReturnAdvanceID
                .IsUsed = argIsUsed
            End With
            _ReturnAdvanceDA.UpdateReturnAdvanceItemIsUsed(objReturnAdvanceItem)
        End Sub

        Public Function SaveCashReceipt(ByVal VoucherNo As String, ByVal Type As String, ByVal dt As System.Data.DataTable) As Boolean Implements ICashReceiptController.SaveCashReceipt
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController


            Dim bolItemRet As Boolean = False

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objCashReceipt As New CashReceiptInfo

                    If dr.RowState = DataRowState.Added Then
                        With objCashReceipt
                            .CashReceiptID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CashReceiptOnCredit, EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString, dr("ReceiptDate"))
                            .Type = Type
                            .VoucherNo = VoucherNo
                            .PayDate = dr("ReceiptDate")
                            .PayAmount = IIf(IsDBNull(dr("Amount")) = True, 0, dr("Amount"))
                            .Remark = IIf(dr("Remark") = "", "-", dr("Remark"))
                            .IsBank = dr("IsBank")
                            .RAdvanceID = dr("VoucherNo")
                        End With
                        bolItemRet = _objCashReceiptDA.InsertCashReceipt(objCashReceipt)

                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                         DateTime.Now, _
                         Global_UserID, _
                         CommonInfo.EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString, _
                         CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                         objCashReceipt.CashReceiptID, _
                         "Insert Cash Receipt")

                        Dim tmpdt As New DataTable
                        tmpdt = _ReturnAdvanceDA.GetReturnAdvanceItem(dr("VoucherNo"))
                        If tmpdt.Rows.Count > 0 Then
                            'For Each tmpdr As DataRow In tmpdt.Rows
                            '        UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, obj.SaleDate)
                            'Next
                            UpdateReturnAdvanceItemIsUsed(dr("VoucherNo"), True)
                        End If

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objCashReceipt
                            .CashReceiptID = dr("@CashReceiptID")
                            .Type = Type
                            .VoucherNo = VoucherNo
                            .PayDate = dr("ReceiptDate")
                            .PayAmount = IIf(IsDBNull(dr("Amount")) = True, 0, dr("Amount"))
                            .Remark = IIf(dr("Remark") = "", "-", dr("Remark"))
                            .IsBank = dr("IsBank")
                            .RAdvanceID = dr("VoucherNo")
                        End With
                        bolItemRet = _objCashReceiptDA.UpdateCashReceipt(objCashReceipt)

                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                        DateTime.Now, _
                        Global_UserID, _
                        CommonInfo.EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString, _
                        CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                        objCashReceipt.CashReceiptID, _
                        "Update Cash Receipt")

                        Dim tmpdt As New DataTable
                        tmpdt = _ReturnAdvanceDA.GetReturnAdvanceItem(objCashReceipt.RAdvanceID)
                        If tmpdt.Rows.Count > 0 Then
                            UpdateReturnAdvanceItemIsUsed(dr("VoucherNo"), True)
                        End If

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolItemRet = _objCashReceiptDA.DeleteCashReceipt(CStr(dr.Item("@CashReceiptID", DataRowVersion.Original)))
                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                         DateTime.Now, _
                                         Global_UserID, _
                                         CommonInfo.EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString, _
                                         CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                         dr.Item("@CashReceiptID", DataRowVersion.Original), _
                                         "Delete Cash Receipt")

                        Dim tmpdt As New DataTable
                        tmpdt = _ReturnAdvanceDA.GetReturnAdvanceItem(CStr(dr.Item("VoucherNo", DataRowVersion.Original)))
                        If tmpdt.Rows.Count > 0 Then
                            UpdateReturnAdvanceItemIsUsed(CStr(dr.Item("VoucherNo", DataRowVersion.Original)), False)
                        End If
                    Else
                        bolItemRet = True
                    End If
                Next


            End If

            Return bolItemRet
        End Function

        Public Function GetPurchaseHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptController.GetPurchaseHeaderCashReceipt
            Return _objCashReceiptDA.GetPurchaseHeaderCashReceipt(IsCash)
        End Function

        Public Function GetSaleInvoiceHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptController.GetSaleInvoiceHeaderCashReceipt
            Return _objCashReceiptDA.GetSaleInvoiceHeaderCashReceipt(IsCash)
        End Function
        Public Function GetSaleLooseDiamondHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptController.GetSaleLooseDiamondHeaderCashReceipt
            Return _objCashReceiptDA.GetSaleLooseDiamondHeaderCashReceipt(IsCash)
        End Function

        Public Function GetWholeSaleInvoiceHeaderCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptController.GetWholeSaleInvoiceHeaderCashReceipt
            Return _objCashReceiptDA.GetWholeSaleInvoiceHeaderCashReceipt(IsCash)
        End Function
        Public Function GetConsignmentSaleCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptController.GetConsignmentSaleCashReceipt
            Return _objCashReceiptDA.GetConsignmentSaleCashReceipt(IsCash)
        End Function

        Public Function GetSaleInvoiceVolumeCashReceipt(ByVal IsCash As Boolean) As DataTable Implements ICashReceiptController.GetSaleInvoiceVolumeCashReceipt
            Return _objCashReceiptDA.GerSaleInvoiceVolumeCashReceipt(IsCash)
        End Function

        Public Function GetCashReceiptforPrint(ByVal VoucherNo As String, Optional ByVal Type As String = "") As DataTable Implements ICashReceiptController.GetCashReceiptforPrint
            Return _objCashReceiptDA.GetCashReceiptforPrint(VoucherNo, Type)
        End Function

        Public Function GetReturnRepairStockCashReceipt(IsCash As Boolean) As DataTable Implements ICashReceiptController.GetReturnRepairStockCashReceipt
            Return _objCashReceiptDA.GetReturnRepairStockCashReceipt(IsCash)
        End Function
    End Class
End Namespace

