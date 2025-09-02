Imports DataAccess.ConsignmentSale
Imports DataAccess.SalesItem
Imports DataAccess.WholeSaleInvoice
Imports CommonInfo
Namespace ConsignmentSale
    Public Class ConsignmentSaleController
        Implements IConsignmentSaleController

#Region "Private Members"

        Private _objConsignSaleDA As IConsignmentSaleDA
        Private _objForSaleDA As SalesItemDA
        Private _objWholeSaleDA As IWholeSaleInvoiceDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IConsignmentSaleController = New ConsignmentSaleController

#End Region

#Region "Constructors"

        Private Sub New()
            _objConsignSaleDA = DataAccess.Factory.Instance.CreateConsignmentSaleDA
            _objForSaleDA = DataAccess.Factory.Instance.CreateSalesItemDA
            _objWholeSaleDA = DataAccess.Factory.Instance.CreateWholeSaleInvoiceDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IConsignmentSaleController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objForSale As New CommonInfo.SalesItemInfo

            With objForSale
                .IsExit = argIsExit
                .ForSaleID = argForSaleID
                .ExitDate = argExitDate
            End With
            _objForSaleDA.UpdateSaleItemIsExit(objForSale)
        End Sub
        Private Sub UpdateSalesItem(ByVal argForSaleID As String, ByVal argIsSale As Boolean)
            Dim objWholeSaleItem As New CommonInfo.WholeSaleInvoiceItemInfo

            With objWholeSaleItem
                .IsSale = argIsSale
                .ForSaleID = argForSaleID

            End With
            _objWholeSaleDA.UpdateSalesItem(objWholeSaleItem)
        End Sub

        Public Function DeleteConsignmentSale(ByVal ConsignmentSaleID As String) As Boolean Implements IConsignmentSaleController.DeleteConsignmentSale
            Dim tmpdt As New DataTable

            'tmpdt = _objConsignSaleDA.GetConsignmentSaleItemByID(ConsignmentSaleID)
            'If tmpdt.Rows.Count > 0 Then
            '    For Each tmpdr As DataRow In tmpdt.Rows
            '        UpdateSalesItemByIsExit(tmpdr.Item("@ForSaleID"), False, Now)
            '    Next
            'End If

            If _objConsignSaleDA.DeleteConsignmentSale(ConsignmentSaleID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.ConsignmentSale.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       ConsignmentSaleID, _
                                       "Delete Consignment Sale")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllConsignmentSale() As System.Data.DataTable Implements IConsignmentSaleController.GetAllConsignmentSale
            Return _objConsignSaleDA.GetAllConsignmentSale()
        End Function


        Public Function GetBarcodeDataByConsignmentSaleID(ByVal argstr As String, Optional ByVal ConsignmentSaleID As String = "") As System.Data.DataTable Implements IConsignmentSaleController.GetBarcodeDataByConsignmentSaleID
            Return _objConsignSaleDA.GetBarcodeDataByConsignmentSaleID(argstr, ConsignmentSaleID)
        End Function
        Public Function GetItemCodeByConsignmentSaleID(Optional ByVal ConsignmentSaleID As String = "") As System.Data.DataTable Implements IConsignmentSaleController.GetItemCodeByConsignmentSaleID
            Return _objConsignSaleDA.GetItemCodeByConsignmentSaleID(ConsignmentSaleID)
        End Function


        Public Function GetBarcodeByConsignmentSaleID(ByVal argstr As String, Optional ByVal cristr As String = "", Optional ByVal ConsignmentSaleID As String = "") As CommonInfo.ConsignmentSaleItemInfo Implements IConsignmentSaleController.GetBarcodeByConsignmentSaleID
            Return _objConsignSaleDA.GetBarcodeByConsignmentSaleID(argstr, cristr, ConsignmentSaleID)
        End Function


        Public Function GetConsignmentSaleByID(ByVal ConsignmentSaleID As String) As CommonInfo.ConsignmentSaleInfo Implements IConsignmentSaleController.GetConsignmentSaleByID
            Return _objConsignSaleDA.GetConsignmentSaleByID(ConsignmentSaleID)
        End Function

        Public Function GetConsignmentSaleItemByID(ByVal ConsignmentSaleID As String) As System.Data.DataTable Implements IConsignmentSaleController.GetConsignmentSaleItemByID
            Return _objConsignSaleDA.GetConsignmentSaleItemByID(ConsignmentSaleID)
        End Function

        Public Function GetConsignmentSaleItem(ByVal ConsignmentSaleItemID As String) As System.Data.DataTable Implements IConsignmentSaleController.GetConsignmentSaleItem
            Return _objConsignSaleDA.GetConsignmentSaleItem(ConsignmentSaleItemID)
        End Function

        Public Function SaveConsignmentSale(ByVal obj As CommonInfo.ConsignmentSaleInfo, ByVal _dtConsignSaleItem As System.Data.DataTable) As Boolean Implements IConsignmentSaleController.SaveConsignmentSale
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController

            Dim dtTest As New DataTable

            Dim bolRet As Boolean = False
            If obj.ConsignmentSaleID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.ConsignmentSaleStock.ToString)
                obj.ConsignmentSaleID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.ConsignDate)

                bolRet = _objConsignSaleDA.InsertConsignmentSale(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.ConsignmentSale.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.ConsignmentSaleID, _
                                       "Insert Consignment Sale")


            Else
                bolRet = _objConsignSaleDA.UpdateConsignmentSale(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.ConsignmentSale.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.ConsignmentSaleID, _
                                       "Update Consignment Sale")

                'Dim tmpdt As New DataTable
                'tmpdt = _objConsignSaleDA.GetConsignmentSaleItemByID(obj.ConsignmentSaleID)
                'If tmpdt.Rows.Count > 0 Then
                '    For Each tmpdr As DataRow In tmpdt.Rows
                '        UpdateSalesItemByIsExit(tmpdr.Item("@ForSaleID"), False, Now)
                '    Next
                'End If

            End If
            If bolRet = True Then
                dtTest = _objConsignSaleDA.GetConsignmentSaleItemByID(obj.ConsignmentSaleID)
                For Each dr As DataRow In _dtConsignSaleItem.Rows
                    Dim objConsignItem As New ConsignmentSaleItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objConsignItem
                            .ConsignmentSaleItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ConsignmentSaleItem, "ConsignmentSaleItem", obj.ConsignDate)
                            .ConsignmentSaleID = obj.ConsignmentSaleID
                            .ForSaleID = dr.Item("@ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .IsReturn = dr.Item("Pay")
                            .IsSale = dr.Item("Sale")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("Amount")
                            .FixPrice = dr.Item("FixPrice")


                        End With
                        bolRet = _objConsignSaleDA.InsertConsignmentSaleItem(objConsignItem)
                        'UpdateSalesItemByIsExit(objConsignItem.ForSaleID, True, obj.ConsignDate)
                        UpdateSalesItem(objConsignItem.ForSaleID, True)
                    ElseIf dr.RowState = DataRowState.Modified Then
                        If dtTest.Rows.Count <= 0 Then
                            With objConsignItem
                                .ConsignmentSaleItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ConsignmentSaleItem, "ConsignmentSaleItem", obj.ConsignDate)
                                .ConsignmentSaleID = obj.ConsignmentSaleID
                                .ForSaleID = dr.Item("@ForSaleID")
                                .ItemCode = dr.Item("ItemCode")
                                .ItemNameID = dr.Item("ItemNameID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .IsReturn = dr.Item("Pay")
                                .IsSale = dr.Item("Sale")
                                .ItemTG = dr.Item("ItemTG")
                                .ItemTK = dr.Item("ItemTK")
                                .GemsTK = dr.Item("GemsTK")
                                .GemsTG = dr.Item("GemsTG")
                                .WasteTK = dr.Item("WasteTK")
                                .WasteTG = dr.Item("WasteTG")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")
                                .SalesRate = dr.Item("SalesRate")
                                .GoldPrice = dr.Item("Amount")
                                .FixPrice = dr.Item("FixPrice")

                            End With
                        Else
                            With objConsignItem
                                .ConsignmentSaleItemID = dr.Item("ConsignmentsaleItemID")
                                .ConsignmentSaleID = obj.ConsignmentSaleID
                                .ForSaleID = dr.Item("ForSaleID")
                                .ItemCode = dr.Item("ItemCode")
                                .ItemNameID = dr.Item("ItemNameID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .IsReturn = dr.Item("Pay")
                                .IsSale = dr.Item("Sale")
                                .ItemTG = dr.Item("ItemTG")
                                .ItemTK = dr.Item("ItemTK")
                                .GemsTK = dr.Item("GemsTK")
                                .GemsTG = dr.Item("GemsTG")
                                .WasteTK = dr.Item("WasteTK")
                                .WasteTG = dr.Item("WasteTG")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")
                                .SalesRate = dr.Item("SalesRate")
                                .GoldPrice = dr.Item("Amount")
                                .FixPrice = dr.Item("FixPrice")

                            End With
                        End If
                        'With objConsignItem
                        '    .ConsignmentSaleItemID = dr.Item("ConsignmentItemID")
                        '    .ConsignmentSaleID = obj.ConsignmentSaleID
                        '    .ForSaleID = dr.Item("@ForSaleID")
                        '    .ItemCode = dr.Item("ItemCode")
                        '    .ItemNameID = dr.Item("ItemNameID")
                        '    .GoldQualityID = dr.Item("GoldQualityID")
                        '    .IsReturn = dr.Item("Pay")
                        '    .IsSale = dr.Item("Sale")
                        '    .ItemTG = dr.Item("ItemTG")
                        '    .ItemTK = dr.Item("ItemTK")
                        '    .GemsTK = dr.Item("GemsTK")
                        '    .GemsTG = dr.Item("GemsTG")
                        '    .WasteTK = dr.Item("WasteTK")
                        '    .WasteTG = dr.Item("WasteTG")
                        '    .GoldTK = dr.Item("GoldTK")
                        '    .GoldTG = dr.Item("GoldTG")
                        '    .SalesRate = dr.Item("SalesRate")
                        '    .GoldPrice = dr.Item("Amount")
                        '    .FixPrice = dr.Item("FixPrice")

                        'End With
                        If dtTest.Rows.Count > 0 Then
                            bolRet = _objConsignSaleDA.UpdateConsignmentSaleItem(objConsignItem)
                        Else
                            bolRet = _objConsignSaleDA.InsertConsignmentSaleItem(objConsignItem)
                        End If
                        'UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, obj.ConsignDate)
                        UpdateSalesItem(objConsignItem.ForSaleID, True)
                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objConsignSaleDA.DeleteConsignmentSaleItem(CStr(dr.Item("ConsignmentSaleItemID", DataRowVersion.Original)))
                        UpdateSalesItem(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), False)

                    ElseIf dr.RowState = DataRowState.Unchanged Then

                        If dtTest.Rows.Count() = 0 Then

                            With objConsignItem
                                .ConsignmentSaleItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ConsignmentSaleItem, "ConsignmentSaleItem", obj.ConsignDate)
                                .ConsignmentSaleID = obj.ConsignmentSaleID
                                .ForSaleID = dr.Item("@ForSaleID")
                                .ItemCode = dr.Item("ItemCode")
                                .ItemNameID = dr.Item("ItemNameID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .IsReturn = dr.Item("Pay")
                                .IsSale = dr.Item("Sale")
                                .ItemTG = dr.Item("ItemTG")
                                .ItemTK = dr.Item("ItemTK")
                                .GemsTK = dr.Item("GemsTK")
                                .GemsTG = dr.Item("GemsTG")
                                .WasteTK = dr.Item("WasteTK")
                                .WasteTG = dr.Item("WasteTG")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")
                                .SalesRate = dr.Item("SalesRate")
                                .GoldPrice = dr.Item("Amount")
                                .FixPrice = dr.Item("FixPrice")
                            End With
                            bolRet = _objConsignSaleDA.InsertConsignmentSaleItem(objConsignItem)
                            'UpdateSalesItemByIsExit(dr.Item("@ForSaleID"), True, obj.ConsignDate)
                            UpdateSalesItem(objConsignItem.ForSaleID, True)
                        Else
                            'UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, obj.ConsignDate)
                            UpdateSalesItem(objConsignItem.ForSaleID, True)
                        End If


                    End If
                Next
            End If
            Return bolRet
        End Function

        Public Function GetConsignmentSaleReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IConsignmentSaleController.GetConsignmentSaleReport
            Return _objConsignSaleDA.GetConsignmentSaleReport(FromDate, ToDate, GetFilterString)
        End Function
        Public Function GetConsignmentSaleReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IConsignmentSaleController.GetConsignmentSaleReportAmount
            Return _objConsignSaleDA.GetConsignmentSaleReportAmount(FromDate, ToDate, GetFilterString)
        End Function

        Public Function GetConsignmentSalePrint(ByVal ConsignmentSaleID As String) As System.Data.DataTable Implements IConsignmentSaleController.GetConsignmentSalePrint
            Return _objConsignSaleDA.GetConsignmentSalePrint(ConsignmentSaleID)
        End Function
        Public Function UpdateTransactionID(ByVal TransactionID As String, ByVal SaleVolumeID As String) As Boolean Implements IConsignmentSaleController.UpdateTransactionID

            If _objConsignSaleDA.UpdateTransactionID(TransactionID, SaleVolumeID) Then

                Return True
            Else
                Return False
            End If
        End Function
        Public Function UpdateRedeemID(ByVal RedeemID As String, ByVal SaleInvoiceID As String) As Boolean Implements IConsignmentSaleController.UpdateRedeemID

            If _objConsignSaleDA.UpdateRedeemID(RedeemID, SaleInvoiceID) Then

                Return True
            Else
                Return False
            End If
        End Function
    End Class

End Namespace


