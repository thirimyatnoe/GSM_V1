Imports DataAccess.SalesOrder
Imports DataAccess.SalesItem
Imports CommonInfo
Namespace SalesOrder
    Public Class SalesOrderController
        Implements ISalesOrderController

#Region "Private Members"

        Private _objSalesItemDA As ISalesItemDA
        Private _objSalesOrderDA As ISalesOrderDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISalesOrderController = New SalesOrderController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSalesOrderDA = DataAccess.Factory.Instance.CreateSalesOrderDA
            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesOrderController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate
            End With
            _objSalesItemDA.UpdateSaleItemIsExit(objSaleItem)
        End Sub
        Public Function DeleteSalesOrder(ByVal obj As SalesOrderInfo) As Boolean Implements ISalesOrderController.DeleteSalesOrder
            If _objSalesOrderDA.DeleteSalesOrder(obj.SaleOrderID) Then
                UpdateSalesItemByIsExit(obj.ForSaleID, False, Now)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesOrder.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       obj.SaleOrderID, _
                                                       "Delete Sales Order")
                Return True
            Else
                Return False
            End If
            '********* Set IsAvaliable=false to Sale itemcode
        End Function

        Public Function GetAllSalesOrder(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesOrderController.GetAllSalesOrder
            Return _objSalesOrderDA.GetAllSalesOrder(cristr)
        End Function

        Public Function GetSalesOrder(ByVal SalesOrderID As String) As CommonInfo.SalesOrderInfo Implements ISalesOrderController.GetSalesOrder
            Return _objSalesOrderDA.GetSalesOrder(SalesOrderID)
        End Function

        Public Function GetSalesOrderItem(ByVal SalesOrderID As String) As System.Data.DataTable Implements ISalesOrderController.GetSalesOrderItem
            Return _objSalesOrderDA.GetSalesOrderItem(SalesOrderID)
        End Function

        Public Function SaveSalesOrder(ByVal obj As CommonInfo.SalesOrderInfo, ByVal _dtSalesOrderItem As System.Data.DataTable) As Boolean Implements ISalesOrderController.SaveSalesOrder
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False
            If obj.SaleOrderID = "" Then
                obj.SaleOrderID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.SalesOrder, CommonInfo.EnumSetting.GenerateKeyType.SalesOrder.ToString, obj.OrderDate)
                bolRet = _objSalesOrderDA.InsertSalesOrder(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesOrder.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                                                       obj.SaleOrderID, _
                                                                       "Insert Sales Order")
            Else
                bolRet = _objSalesOrderDA.UpdateSalesOrder(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.SalesOrder.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                       obj.SaleOrderID, _
                                                       "Update Sales Order")
            End If
            If bolRet = True Then
                '********* Set IsAvaliable=false to Sale itemcode
                UpdateSalesItemByIsExit(obj.ForSaleID, True, obj.OrderDate)
                For Each dr As DataRow In _dtSalesOrderItem.Rows
                    Dim objSalesOrderItemInfo As New SalesOrderGemsItemInfo
                    If dr.RowState = DataRowState.Unchanged Then
                        With objSalesOrderItemInfo
                            .SaleOrderID = obj.SaleOrderID
                            .SaleOrderItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.SalesOrderGemsItem, CommonInfo.EnumSetting.GenerateKeyType.SalesOrderGemsItem.ToString, obj.OrderDate)
                            .GemsCategoryID = dr.Item("@GemsCategoryID")
                            .GemsName = dr.Item("GemsName")
                            .GemsTK = dr.Item("GemsTK")
                            .GemsTG = dr.Item("GemsTG")
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")) = True, 0, dr.Item("YOrCOrG"))
                            .GemsTW = dr.Item("GemsTW")
                            .Qty = dr.Item("Qty")
                            .Type = dr.Item("Type")
                            .UnitPrice = dr.Item("UnitPrice")
                            .Amount = dr.Item("Amount")

                        End With
                        bolRet = _objSalesOrderDA.InsertSalesOrderItem(objSalesOrderItemInfo)
                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objSalesOrderDA.DeleteSalesOrderItem(CStr(dr.Item("SalesOrderItemID", DataRowVersion.Original)))
                    End If
                Next
            End If
            Return bolRet
        End Function

        Public Function GetSalesOrderForReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsReturn As Boolean, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesOrderController.GetSalesOrderForReport
            Return _objSalesOrderDA.GetSalesOrderForReport(FromDate, ToDate, IsReturn, cristr)
        End Function

        Public Function GetSalesOrderPrint(ByVal SaleOrderID As String) As System.Data.DataTable Implements ISalesOrderController.GetSalesOrderPrint
            Return _objSalesOrderDA.GetSalesOrderPrint(SaleOrderID)
        End Function

        Public Function GetAllSaleOrderFromSearchBox() As System.Data.DataTable Implements ISalesOrderController.GetAllSaleOrderFromSearchBox
            Return _objSalesOrderDA.GetAllSaleOrderFromSearchBox()
        End Function
    End Class
End Namespace

