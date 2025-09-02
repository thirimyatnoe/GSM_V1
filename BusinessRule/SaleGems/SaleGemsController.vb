Imports DataAccess.SaleGems
Imports CommonInfo
Namespace SaleGems
    Public Class SaleGemsController
        Implements ISaleGemsController
#Region "Private Members"

        Private _objSaleGemsDA As ISaleGemsDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISaleGemsController = New SaleGemsController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSaleGemsDA = DataAccess.Factory.Instance.CreateSaleGemsDA
        End Sub
#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISaleGemsController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteSaleGems(ByVal SaleGemsID As String) As Boolean Implements ISaleGemsController.DeleteSaleGems
            If _objSaleGemsDA.DeleteSaleGems(SaleGemsID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesGem.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       SaleGemsID, _
                                                       "Delete Sale Gems")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetSaleGems(ByVal SaleGemsID As String) As CommonInfo.SaleGemsInfo Implements ISaleGemsController.GetSaleGems
            Return _objSaleGemsDA.GetSaleGems(SaleGemsID)
        End Function

        Public Function GetSaleGemsItem(ByVal SaleGemsID As String) As System.Data.DataTable Implements ISaleGemsController.GetSaleGemsItem
            Return _objSaleGemsDA.GetSaleGemsItem(SaleGemsID)
        End Function
        Public Function GetSaleGemsDataByCustomerIDAndItemCode(CustomerID As String, Optional criStr As String = "") As DataTable Implements ISaleGemsController.GetSaleGemsDataByCustomerIDAndItemCode
            Return _objSaleGemsDA.GetSaleGemsDataByCustomerIDAndItemCode(CustomerID, criStr)
        End Function
        Public Function SaveSaleGems(ByVal SaleGemsObj As CommonInfo.SaleGemsInfo, ByVal _dtSaleGemsItem As System.Data.DataTable, ByVal _dtOtherCash As System.Data.DataTable) As Boolean Implements ISaleGemsController.SaveSaleGems
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo '
            Dim bolRet As Boolean = True

            If SaleGemsObj.SaleGemsID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.SalesGem.ToString)
                SaleGemsObj.SaleGemsID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, SaleGemsObj.SDate)

                bolRet = _objSaleGemsDA.InsertSaleGems(SaleGemsObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesGem.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                       SaleGemsObj.SaleGemsID, _
                                                       "Insert Sale Gems")
            Else
                bolRet = _objSaleGemsDA.UpdateSaleGems(SaleGemsObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesGem.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                       SaleGemsObj.SaleGemsID, _
                                                       "Update Sale Gems")
            End If
            If bolRet Then
                For Each dr As DataRow In _dtSaleGemsItem.Rows
                    Dim objSaleGemsItem As New SaleGemsItemInfo
                    If dr.RowState = DataRowState.Added Then
                        With objSaleGemsItem
                            .SaleGemsID = SaleGemsObj.SaleGemsID
                            .SaleGemsItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.SaleGemsItem, "SaleGemsItemID", SaleGemsObj.SDate)
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = IIf(IsDBNull(dr.Item("GemsName")) = True, "-", dr.Item("GemsName"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, "-", dr.Item("YOrCOrG"))
                            .GemTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
                            If Not IsDBNull(dr.Item("FixType")) Then
                                If (dr.Item("FixType") = "Fix") Then
                                    .FixType = 1
                                ElseIf (dr.Item("FixType") = "ByWeight") Then
                                    .FixType = 2
                                Else
                                    .FixType = 3
                                End If
                            Else
                                .FixType = 0
                            End If


                            .SaleRate = IIf(IsDBNull(dr.Item("SaleRate")) = True, 0, dr.Item("SaleRate"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")) = True, 0, dr.Item("Amount"))
                            .IsReturn = IIf(IsDBNull(dr.Item("IsReturn")) = True, 0, dr.Item("IsReturn"))
                        End With
                        bolRet = _objSaleGemsDA.InsertSaleGemsItem(objSaleGemsItem)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objSaleGemsItem
                            .SaleGemsID = SaleGemsObj.SaleGemsID
                            .SaleGemsItemID = dr.Item("SaleGemsItemID")
                            .GemsCategoryID = dr.Item("GemsCategoryID")
                            .GemsName = IIf(IsDBNull(dr.Item("GemsName")) = True, "-", dr.Item("GemsName"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")) = True, 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")) = True, 0, dr.Item("GemsTG"))

                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, "-", dr.Item("YOrCOrG"))
                            .GemTW = IIf(IsDBNull(dr.Item("GemsTW")) = True, 0, dr.Item("GemsTW"))
                            .Qty = IIf(IsDBNull(dr.Item("Qty")) = True, 0, dr.Item("Qty"))
                            If Not IsDBNull(dr.Item("FixType")) Then
                                If (dr.Item("FixType") = "Fix") Then
                                    .FixType = 1
                                ElseIf (dr.Item("FixType") = "ByWeight") Then
                                    .FixType = 2
                                Else
                                    .FixType = 3
                                End If
                            Else
                                .FixType = 0
                            End If

                            .SaleRate = IIf(IsDBNull(dr.Item("SaleRate")) = True, 0, dr.Item("SaleRate"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")) = True, 0, dr.Item("Amount"))
                            .IsReturn = IIf(IsDBNull(dr.Item("IsReturn")) = True, 0, dr.Item("IsReturn"))
                        End With
                        bolRet = _objSaleGemsDA.UpdateSaleGemsItem(objSaleGemsItem)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objSaleGemsDA.DeleteSaleGemsItem(CStr(dr.Item("SaleGemsItemID", DataRowVersion.Original)))
                    End If
                Next
            End If
            If _dtOtherCash.Rows.Count > 0 Then
                If bolRet = True Then
                    For Each drRecord As DataRow In _dtOtherCash.Rows
                        Dim objRecord As New CommonInfo.OtherCashInfo
                        If drRecord.RowState = DataRowState.Added Then
                            With objRecord
                                .RecordCashID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.OtherCash, EnumSetting.GenerateKeyType.OtherCash.ToString, Now.Date)
                                .VoucherNo = SaleGemsObj.SaleGemsID
                                .CashTypeID = drRecord.Item("CashTypeID")
                                .ExchangeRate = drRecord.Item("ExchangeRate")
                                .Amount = drRecord.Item("Amount")
                            End With
                            bolRet = _objSaleGemsDA.InsertRecordCash(objRecord)
                        ElseIf drRecord.RowState = DataRowState.Modified Then
                            With objRecord
                                .RecordCashID = drRecord.Item("RecordCashID")
                                .VoucherNo = SaleGemsObj.SaleGemsID
                                .CashTypeID = drRecord.Item("CashTypeID")
                                .ExchangeRate = drRecord.Item("ExchangeRate")
                                .Amount = drRecord.Item("Amount")
                            End With
                            bolRet = _objSaleGemsDA.UpdateRecordCash(objRecord)
                        ElseIf drRecord.RowState = DataRowState.Deleted Then
                            _objSaleGemsDA.DeleteRecordCash(CStr(drRecord.Item("RecordCashID", DataRowVersion.Original)))
                        End If
                    Next
                End If
            End If


            Return bolRet
        End Function
        Public Function GetAllSaleGems() As System.Data.DataTable Implements ISaleGemsController.GetAllSaleGems
            Return _objSaleGemsDA.GetAllSaleGems()
        End Function

        Public Function GetSaleGemsItemForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsController.GetSaleGemsItemForRpt
            Return _objSaleGemsDA.GetSaleGemsItemForRpt(FromDate, ToDate, cristr)
        End Function

        Public Function GetAllSaleGemsForRpt(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsController.GetAllSaleGemsForRpt
            Return _objSaleGemsDA.GetAllSaleGemsForRpt(FromDate, ToDate, cristr)
        End Function

        Public Function GetAllSaleGem() As System.Data.DataTable Implements ISaleGemsController.GetAllSaleGem
            Return _objSaleGemsDA.GetAllSaleGem()
        End Function
        Public Function GetSaleGemsPrint(ByVal SaleGemsID As String) As System.Data.DataTable Implements ISaleGemsController.GetSaleGemsPrint
            Return _objSaleGemsDA.GetSaleGemsPrint(SaleGemsID)
        End Function
        Public Function InsertSaleGemsUserID(ByVal SaleGemsID As String, ByVal UserID As String) As Boolean Implements ISaleGemsController.InsertSaleGemsUserID
            Return _objSaleGemsDA.InsertSaleGemsUserID(SaleGemsID, UserID)
        End Function
        Public Function GetSaleGemsReportForTotal(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsController.GetSaleGemsReportForTotal
            Return _objSaleGemsDA.GetSaleGemsReportForTotal(FromDate, ToDate, cristr)
        End Function

        Public Function GetSaleGemsBalanceStockByGemsCategoryID(ByVal GemsCategoryID As String) As CommonInfo.SaleGemsItemInfo Implements ISaleGemsController.GetSaleGemsBalanceStockByGemsCategoryID
            Return _objSaleGemsDA.GetSaleGemsBalanceStockByGemsCategoryID(GemsCategoryID)
        End Function
        Public Function GetAllSaleGemsVoucherPrint(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISaleGemsController.GetAllSaleGemsVoucherPrint
            Return _objSaleGemsDA.GetAllSaleGemsVoucherPrint(FromDate, ToDate, cristr)
        End Function
        Public Function GetSaleGemsItemBySaleGemsItemID(ByVal SaleGemsItemID As String) As System.Data.DataTable Implements ISaleGemsController.GetSaleGemsItemBySaleGemsItemID
            Return _objSaleGemsDA.GetSaleGemsItemBySaleGemsItemID(SaleGemsItemID)
        End Function
        Public Function GetOtherCashDataByVoucherNo(ByVal SaleGemsID As String) As DataTable Implements ISaleGemsController.GetOtherCashDataByVoucherNo
            Return _objSaleGemsDA.GetOtherCashDataByVoucherNo(SaleGemsID)
        End Function

    End Class
End Namespace

