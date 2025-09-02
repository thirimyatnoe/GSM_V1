'Imports DataAccess.SalesInvoice
'Imports DataAccess.SalesItem
'Imports CommonInfo
'Namespace SalesInvoice
'    Public Class SalesInvoiceController
'        Implements ISalesInvoiceController

'#Region "Private Members"

'        Private _objSalesItemDA As ISalesItemDA
'        Private _objSalesInvoiceDA As ISalesInvoiceDA
'        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
'        Private Shared ReadOnly _instance As ISalesInvoiceController = New SalesInvoiceController

'#End Region

'#Region "Constructors"

'        Private Sub New()
'            _objSalesInvoiceDA = DataAccess.Factory.Instance.CreateSalesInvoiceDA
'            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
'        End Sub

'#End Region

'#Region "Public Properties"

'        Public Shared ReadOnly Property Instance() As ISalesInvoiceController
'            Get
'                Return _instance
'            End Get
'        End Property

'#End Region
'        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
'            Dim objSaleItem As New CommonInfo.SalesItemInfo
'            With objSaleItem
'                .ForSaleID = argForSaleID
'                .IsExit = argIsExit
'                .ExitDate = argExitDate
'            End With
'            _objSalesItemDA.UpdateSaleItemIsExit(objSaleItem)
'        End Sub

'        Public Function GetAllSalesInvoice() As System.Data.DataTable Implements ISalesInvoiceController.GetAllSalesInvoice
'            Return _objSalesInvoiceDA.GetAllSalesInvoice()
'        End Function

'        Public Function GetSalesInvoice(ByVal SalesInvoiceID As String) As CommonInfo.SalesInvoiceDetailInfo Implements ISalesInvoiceController.GetSalesInvoice
'            Return _objSalesInvoiceDA.GetSalesInvoice(SalesInvoiceID)
'        End Function

'        Public Function GetSalesInvoiceItem(ByVal SalesInvoiceID As String) As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoiceItem
'            Return _objSalesInvoiceDA.GetSalesInvoiceItem(SalesInvoiceID)
'        End Function

'        Public Function SaveSalesInvoice(ByVal obj As CommonInfo.SalesInvoiceDetailInfo, ByVal _dtSalesInvoiceItem As System.Data.DataTable) As Boolean Implements ISalesInvoiceController.SaveSalesInvoice
'            '    Dim objGeneralController As General.IGeneralController
'            '    objGeneralController = Factory.Instance.CreateGeneralController
'            '    Dim bolRet As Boolean = False
'            '    If obj.SaleInvoiceID = "" Then
'            '        obj.SaleInvoiceID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.SalesInvoice, CommonInfo.EnumSetting.GenerateKeyType.SalesInvoice.ToString, obj.SDate)
'            '        bolRet = _objSalesInvoiceDA.InsertSalesInvoice(obj)
'            '        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
'            '                               DateTime.Now, _
'            '                               Global_UserID, _
'            '                               CommonInfo.EnumSetting.GenerateKeyType.SalesInvoice.ToString, _
'            '                               CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
'            '                               obj.SaleInvoiceID, _
'            '                               "Insert Sales Invoice")

'            '        If bolRet = True Then
'            '            UpdateSalesItemByIsExit(obj.ForSaleID, True, obj.SDate)
'            '            For Each dr As DataRow In _dtSalesInvoiceItem.Rows
'            '                Dim objSalesInvoiceItemInfo As New SaleInvoiceGemsItemInfo
'            '                If dr.RowState = DataRowState.Unchanged Then
'            '                    With objSalesInvoiceItemInfo
'            '                        .SaleInvoiceID = obj.SaleInvoiceID
'            '                        .SaleInvoiceItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.SalesInvoiceGemsItem, CommonInfo.EnumSetting.GenerateKeyType.SalesInvoiceGemsItem.ToString, obj.SDate)
'            '                        .GemsCategoryID = dr.Item("@GemsCategoryID")
'            '                        .GemsName = dr.Item("GemsName")
'            '                        .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, 0, dr.Item("YOrCOrG"))
'            '                        .GemTW = dr.Item("GemsTW")
'            '                        .Qty = dr.Item("Qty")
'            '                        .GemsTK = dr.Item("GemsTK")
'            '                        .GemsTG = dr.Item("GemsTG")
'            '                        .Type = dr.Item("Type")
'            '                        .UnitPrice = dr.Item("UnitPrice")
'            '                        .Amount = dr.Item("Amount")
'            '                        .GemsRemark = dr.Item("GemsRemark")
'            '                    End With
'            '                    _objSalesInvoiceDA.InsertSalesInvoiceItem(objSalesInvoiceItemInfo)

'            '                ElseIf dr.RowState = DataRowState.Deleted Then
'            '                    _objSalesInvoiceDA.DeleteSalesInvoiceItem(CStr(dr.Item("SalesInvoiceItemID", DataRowVersion.Original)))
'            '                End If
'            '            Next
'            '        End If

'            '    Else
'            '        bolRet = _objSalesInvoiceDA.UpdateSalesInvoice(obj)
'            '        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
'            '                               DateTime.Now, _
'            '                               Global_UserID, _
'            '                               CommonInfo.EnumSetting.GenerateKeyType.SalesInvoice.ToString, _
'            '                               CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
'            '                               obj.SaleInvoiceID, _
'            '                               "Update Sales Invoice")

'            '    End If
'            '    Return bolRet
'        End Function

'        Public Function GetSalesInvoicePrint(ByVal SaleInvoiceID As String) As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoicePrint
'            Return _objSalesInvoiceDA.GetSalesInvoicePrint(SaleInvoiceID)
'        End Function

'        Public Function GetSalesInvoicePrint2(ByVal SaleInvoiceID As String, Optional ByVal LocationId As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoicePrint2
'            Return _objSalesInvoiceDA.GetSalesInvoicePrint2(SaleInvoiceID, LocationId)

'        End Function


'        Public Function SetSalesInvoiceToReturn(ByVal SalesInvoiceID As String, Optional ByVal IsReturn As Integer = 0) As Boolean Implements ISalesInvoiceController.SetSalesInvoiceToReturn
'            Return _objSalesInvoiceDA.SetSalesInvoiceToReturn(SalesInvoiceID, IsReturn)
'        End Function

'        Public Function GetItemCodeBySaleInvoice(ByVal SalesInvoiceID As String) As String Implements ISalesInvoiceController.GetItemCodeBySaleInvoice
'            Return _objSalesInvoiceDA.GetItemCodeBySaleInvoice(SalesInvoiceID)
'        End Function

'        ''Public Function GetProfitForSaleItem(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetProfitForSaleItem
'        ''    Return _objSalesInvoiceDA.GetProfitForSaleItem(FromDate, ToDate, GetFilterString)
'        ''End Function
'        Public Function GetProfitForSaleItem(ByVal argType As String, ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetProfitForSaleItem
'            Return _objSalesInvoiceDA.GetProfitForSaleItem(argType, FromDate, ToDate, criStr)
'        End Function
'        Public Function GetAllSalesInvoiceDetail(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetAllSalesInvoiceDetail
'            Return _objSalesInvoiceDA.GetAllSalesInvoiceDetail(FromDate, ToDate, GetFilterString)
'        End Function

'        'Public Function GetSalesInvoiceReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoiceReport
'        '    Return _objSalesInvoiceDA.GetSalesInvoiceReport(FromDate, ToDate, criStr)
'        'End Function

'        Public Function GetSalesInvoiceForOrderDetail(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoiceForOrderDetail
'            Return _objSalesInvoiceDA.GetSalesInvoiceForOrderDetail(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetSalesInvoiceForOrderSummary(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoiceForOrderSummary
'            Return _objSalesInvoiceDA.GetSalesInvoiceForOrderSummary(FromDate, ToDate, cristr)
'        End Function

'        Public Function GetSaleInvoiceItemFromPurchaseInvoice(ByVal SalesInvoiceID As String) As System.Data.DataTable Implements ISalesInvoiceController.GetSaleInvoiceItemFromPurchaseInvoice
'            Return _objSalesInvoiceDA.GetSaleInvoiceItemFromPurchaseInvoice(SalesInvoiceID)
'        End Function

'        Public Function DeleteSalesInvoice(ByVal obj As CommonInfo.SalesInvoiceDetailInfo) As Boolean Implements ISalesInvoiceController.DeleteSalesInvoice
'            '    If _objSalesInvoiceDA.DeleteSalesInvoice(obj.SaleInvoiceID) Then
'            '        UpdateSalesItemByIsExit(obj.ForSaleID, False, Now)
'            '        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
'            '                               DateTime.Now, _
'            '                               Global_UserID, _
'            '                               CommonInfo.EnumSetting.GenerateKeyType.SalesInvoice.ToString, _
'            '                               CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
'            '                               obj.SaleInvoiceID, _
'            '                               "Delete Sales Invoice")
'            '        Return True
'            '    Else
'            '        Return False
'            '    End If
'        End Function

'        Public Function GetAllSaleInvoiceFromSearchBox() As System.Data.DataTable Implements ISalesInvoiceController.GetAllSaleInvoiceFromSearchBox
'            Return _objSalesInvoiceDA.GetAllSaleInvoiceFromSearchBox()
'        End Function

'        Public Function GetForSaleIDBySaleInvoice(ByVal SalesInvoiceID As String) As String Implements ISalesInvoiceController.GetForSaleIDBySaleInvoice
'            Return _objSalesInvoiceDA.GetForSaleIDBySaleInvoice(SalesInvoiceID)
'        End Function

'        Public Function InsertSalesInvoiceUserID(ByVal SalesInvoiceID As String, ByVal UserID As String) As Boolean Implements ISalesInvoiceController.InsertSalesInvoiceUserID
'            Return _objSalesInvoiceDA.InsertSalesInvoiceUserID(SalesInvoiceID, UserID)
'        End Function

'        'Public Function GetSaleInvoiceDetailForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As CommonInfo.SalesInvoiceDetailInfo Implements ISalesInvoiceController.GetSaleInvoiceDetailForTotal
'        '    Return _objSalesInvoiceDA.GetSaleInvoiceDetailForTotal(FromDate, ToDate, criStr)
'        'End Function
'        'Public Function GetSalesInvoiceReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoiceReportForTotal
'        '    Return _objSalesInvoiceDA.GetSalesInvoiceReportForTotal(FromDate, ToDate, criStr)
'        'End Function

'        'Public Function GetSalesInvoiceReportForSummaryReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetSalesInvoiceReportForSummaryReport
'        '    Return _objSalesInvoiceDA.GetSalesInvoiceReportForSummaryReport(FromDate, ToDate, criStr)
'        'End Function
'        Public Function GetAllSaleInvoiceVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ISalesInvoiceController.GetAllSaleInvoiceVoucherPrint
'            Return _objSalesInvoiceDA.GetAllSaleInvoiceVoucherPrint(FromDate, ToDate, criStr)
'        End Function
'    End Class
'End Namespace

