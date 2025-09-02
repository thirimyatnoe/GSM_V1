Imports DataAccess.WholeSaleReturn
Imports DataAccess.SalesItem
Imports DataAccess.WholeSaleInvoice
Imports CommonInfo
Namespace WholeSaleReturn
    Public Class WholeSaleReturnController
        Implements IWholeSaleReturnController


#Region "Private Members"

        Private _objWholeSaleReturnDA As IWholeSaleReturnDA
        Private _objForSaleDA As ISalesItemDA
        Private _objWholeSaleDA As IWholeSaleInvoiceDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IWholeSaleReturnController = New WholeSaleReturnController

#End Region

#Region "Constructors"

        Private Sub New()
            _objWholeSaleReturnDA = DataAccess.Factory.Instance.CreateWholeSaleReturnDA
            _objForSaleDA = DataAccess.Factory.Instance.CreateSalesItemDA
            _objWholeSaleDA = DataAccess.Factory.Instance.CreateWholeSaleInvoiceDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IWholeSaleReturnController
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

        Private Sub UpdateWholeSalesReturnDeleteItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objForSale As New CommonInfo.SalesItemInfo

            With objForSale
                .IsExit = argIsExit
                .ForSaleID = argForSaleID
                .ExitDate = argExitDate
            End With
            _objForSaleDA.UpdateWholeSalesReturnDeleteItemIsExit(objForSale)
        End Sub

        Private Sub UpdateWholeSalesReturnItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argWReturnDate As Date)
            Dim objForSale As New CommonInfo.SalesItemInfo

            With objForSale
                .IsExit = argIsExit
                .ForSaleID = argForSaleID
                .WReturnDate = argWReturnDate
            End With
            _objForSaleDA.UpdateWholeSaleReturnItemIsExit(objForSale)
        End Sub
        Private Sub UpdateWholeSalesItem(ByVal argForSaleID As String, ByVal argIsReturn As Boolean, ByVal argWholesaleReturnItemID As String)
            Dim objWholeSaleItem As New CommonInfo.WholeSaleInvoiceItemInfo

            With objWholeSaleItem
                .IsReturn = argIsReturn
                .ForSaleID = argForSaleID
                .WholesaleInvoiceID = argWholesaleReturnItemID
            End With
            _objWholeSaleDA.UpdateWholeSalesItem(objWholeSaleItem)
        End Sub


        Public Function DeleteWholeSaleReturn(ByVal WholeSaleReturnID As String) As Boolean Implements IWholeSaleReturnController.DeleteWholeSaleReturn
            Dim tmpdt As New DataTable

            tmpdt = _objWholeSaleReturnDA.GetWholeSaleReturnItem(WholeSaleReturnID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateWholeSalesReturnDeleteItemByIsExit(tmpdr.Item("ForSaleID"), True, Now)

                    UpdateWholeSalesItem(tmpdr.Item("ForSaleID"), False, tmpdr.Item("WholeSaleReturnItemID"))
                Next
            End If

            If _objWholeSaleReturnDA.DeleteWholeSaleReturn(WholeSaleReturnID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.WholeSaleReturn.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       WholeSaleReturnID, _
                                       "Delete WholeSales Return")

                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllWholeSaleReturn() As System.Data.DataTable Implements IWholeSaleReturnController.GetAllWholeSaleReturn
            Return _objWholeSaleReturnDA.GetAllWholeSaleReturn
        End Function

        Public Function GetWholeSaleReturnByID(ByVal WholeSaleReturnID As String) As CommonInfo.WholeSaleReturnInfo Implements IWholeSaleReturnController.GetWholeSaleReturnByID
            Return _objWholeSaleReturnDA.GetWholeSaleReturnByID(WholeSaleReturnID)
        End Function

        Public Function GetWholeSaleReturnItemByID(ByVal WholeSaleReturnID As String) As System.Data.DataTable Implements IWholeSaleReturnController.GetWholeSaleReturnItemByID
            Return _objWholeSaleReturnDA.GetWholeSaleReturnItemByID(WholeSaleReturnID)
        End Function
        Public Function GetWholeSaleReturnByWSID(ByVal WholeSaleInvoiceID As String) As System.Data.DataTable Implements IWholeSaleReturnController.GetWholeSaleReturnByWSID
            Return _objWholeSaleReturnDA.GetWholeSaleReturnByWSID(WholeSaleInvoiceID)
        End Function

        Public Function SaveWholeSaleReturn(ByVal obj As CommonInfo.WholeSaleReturnInfo, ByVal _dtWSReturnItem As System.Data.DataTable) As Boolean Implements IWholeSaleReturnController.SaveWholeSaleReturn
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim ConsignmentSaleID As String
            Dim dtTest As New DataTable
            Dim bolRet As Boolean = False

            If obj.WholesaleReturnID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.WholeSaleReturnStock.ToString)
                obj.WholesaleReturnID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.WReturnDate)
                ConsignmentSaleID = obj.ConsignmentSaleID

                bolRet = _objWholeSaleReturnDA.InsertWholeSaleReturn(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.WholeSaleReturn.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.WholesaleReturnID, _
                                       "Insert WholeSale Return")


            Else
                bolRet = _objWholeSaleReturnDA.UpdateWholeSaleReturn(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.WholeSaleReturn.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.WholesaleReturnID, _
                                       "Update WholeSales Return")

                Dim tmpdt As New DataTable
                tmpdt = _objWholeSaleReturnDA.GetWholeSaleReturnItemByID(obj.WholesaleReturnID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        ' If (ConsignmentSaleID = "-") Then

                        UpdateWholeSalesReturnItemByIsExit(tmpdr.Item("ForSaleID"), False, obj.WReturnDate)

                        ' End If

                    Next
                End If

            End If
            If bolRet = True Then
                dtTest = _objWholeSaleReturnDA.GetWholeSaleReturnItemByID(obj.WholesaleReturnID)
                For Each dr As DataRow In _dtWSReturnItem.Rows
                    Dim objWSReturnItem As New WholeSaleReturnItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objWSReturnItem
                            .WholesaleReturnItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.WholeSaleReturnItem, "WholeSaleReturnItem", obj.WReturnDate)
                            .WholesaleReturnID = obj.WholesaleReturnID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .IsReturn = dr.Item("IsReturn")
                            .IsSale = dr.Item("IsSale")
                            .SalesRate = dr.Item("SalesRate")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .GoldPrice = dr.Item("Amount")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolRet = _objWholeSaleReturnDA.InsertWholeSaleReturnItem(objWSReturnItem)
                        UpdateWholeSalesReturnItemByIsExit(objWSReturnItem.ForSaleID, False, obj.WReturnDate)
                        'For Whole sale item is return 
                        UpdateWholeSalesItem(objWSReturnItem.ForSaleID, True, objWSReturnItem.WholesaleReturnItemID)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objWSReturnItem
                            .WholesaleReturnItemID = dr.Item("WSReturnItemID")
                            '.WholesaleReturnItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.WholeSaleReturnItem, "WholeSaleReturnItem", obj.WReturnDate)
                            .WholesaleReturnID = obj.WholesaleReturnID
                            .ForSaleID = dr.Item("ForSaleID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .ItemCode = dr.Item("ItemCode")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .WasteTK = dr.Item("WasteTK")
                            .WasteTG = dr.Item("WasteTG")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .IsReturn = dr.Item("IsReturn")
                            .IsSale = dr.Item("IsSale")
                            .SalesRate = dr.Item("SalesRate")
                            .GoldPrice = dr.Item("Amount")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolRet = _objWholeSaleReturnDA.UpdateWholeSaleReturnItem(objWSReturnItem)
                        'bolRet = _objWholeSaleReturnDA.InsertWholeSaleReturnItem(objWSReturnItem)
                        UpdateWholeSalesReturnItemByIsExit(objWSReturnItem.ForSaleID, False, obj.WReturnDate)
                        UpdateWholeSalesItem(objWSReturnItem.ForSaleID, True, objWSReturnItem.WholesaleReturnItemID)
                    ElseIf dr.RowState = DataRowState.Deleted Then
                        UpdateWholeSalesReturnDeleteItemByIsExit(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), True, obj.WReturnDate)
                        UpdateWholeSalesItem(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), False, CStr(dr.Item("WSReturnItemID", DataRowVersion.Original)))
                        bolRet = _objWholeSaleReturnDA.DeleteWholeSaleReturnItem(CStr(dr.Item("WSReturnItemID", DataRowVersion.Original)))



                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateWholeSalesReturnItemByIsExit(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), False, obj.WReturnDate)
                        UpdateWholeSalesItem(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), True, CStr(dr.Item("WSReturnItemID", DataRowVersion.Original)))
                    End If
                Next
            End If
            Return bolRet
        End Function

        Public Function GetWholeSalesReturnItem(ByVal WholeSalesReturnID As String) As System.Data.DataTable Implements IWholeSaleReturnController.GetWholeSalesReturnItem
            Return _objWholeSaleReturnDA.GetWholeSaleReturnItem(WholeSalesReturnID)
        End Function
        Public Function GetWholeSaleReturnPrint(ByVal WholeSalesReturnID As String) As System.Data.DataTable Implements IWholeSaleReturnController.GetWholeSaleReturnPrint
            Return _objWholeSaleReturnDA.GetWholeSaleReturnPrint(WholeSalesReturnID)
        End Function

        Public Function GetWholeSaleReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleReturnController.GetWholeSaleReturnReport
            Return _objWholeSaleReturnDA.GetWholeSaleReturnReport(FromDate, ToDate, GetFilterString)
        End Function
        Public Function GetWholeSaleReturnReportAmount(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal GetFilterString As String = "") As System.Data.DataTable Implements IWholeSaleReturnController.GetWholeSaleReturnReportAmount
            Return _objWholeSaleReturnDA.GetWholeSaleReturnReportAmount(FromDate, ToDate, GetFilterString)
        End Function
    End Class
End Namespace