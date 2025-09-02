Imports DataAccess.TransferReturn
Imports DataAccess.SalesItem
Imports DataAccess.Location
Imports CommonInfo
Namespace TransferReturn
    Public Class TransferReturnController
        Implements ITransferReturnController




#Region "Private Members"

        Private _objTransferReturnDA As ITransferReturnDA
        Private _objSaleItemDA As DataAccess.SalesItem.ISalesItemDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private _objLocationDA As DataAccess.Location.ILocationDA
        Private Shared ReadOnly _instance As ITransferReturnController = New TransferReturnController

#End Region

#Region "Constructors"

        Private Sub New()
            _objTransferReturnDA = DataAccess.Factory.Instance.CreateTransferReturnDA
            _objSaleItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ITransferReturnController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Private Sub UpdateSalesItemByIsExitForTransferReturn(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date, ByVal LocationID As String)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate
                .LocationID = LocationID
            End With
            _objSaleItemDA.UpdateSaleItemIsExitForTransferReturn(objSaleItem)
        End Sub
        Public Function DeleteTransferReturn(ByVal TransferReturnID As String) As Boolean Implements ITransferReturnController.DeleteTransferReturn
            Dim tmpdt As New DataTable
            Dim dtHeadLocation As New DataTable
            tmpdt = _objTransferReturnDA.GetForSaleIDByTransferReturnID(TransferReturnID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByIsExitForTransferReturn(tmpdr.Item("ForSaleID"), False, Now, Global_CurrentLocationID)
                Next
            End If
            If _objTransferReturnDA.DeleteTransferReturn(TransferReturnID) Then

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       TransferReturnID, _
                                       "Delete Transfer")
                Return True
            Else
                Return False
            End If

        End Function

        Public Function GetAllTransferReturn() As System.Data.DataTable Implements ITransferReturnController.GetAllTransferReturn
            Return _objTransferReturnDA.GetAllTransferReturn()
        End Function

        Public Function GetTransferReturn(ByVal TransferReturnID As String) As CommonInfo.TransferReturnInfo Implements ITransferReturnController.GetTransferReturn
            Return _objTransferReturnDA.GetTransferReturn(TransferReturnID)
        End Function

        Public Function GetTransferReturnItem(ByVal TransferReturnID As String) As System.Data.DataTable Implements ITransferReturnController.GetTransferReturnItem
            Return _objTransferReturnDA.GetTransferReturnItem(TransferReturnID)
        End Function

        Public Function SaveTransferReturn(ByVal TransferObj As CommonInfo.TransferReturnInfo, ByVal _dtTransferReturnItem As System.Data.DataTable) As Boolean Implements ITransferReturnController.SaveTransferReturn
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            Dim bolItemRet As Boolean = False
            If TransferObj.TransferReturnID = "" Then
                TransferObj.TransferReturnID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.TransferReturn, CommonInfo.EnumSetting.GenerateKeyType.TransferReturn.ToString, TransferObj.TransferReturnDate)

                bolRet = _objTransferReturnDA.InsertTransferReturn(TransferObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       TransferObj.TransferReturnID, _
                                       "Insert Transfer")
            Else
                bolRet = _objTransferReturnDA.UpdateTransferReturn(TransferObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       TransferObj.TransferReturnID, _
                                       "Update Transfer")


                Dim tmpdt As New DataTable
                tmpdt = _objTransferReturnDA.GetForSaleIDByTransferReturnID(TransferObj.TransferReturnID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByIsExitForTransferReturn(tmpdr.Item("ForSaleID"), False, Now, TransferObj.LocationID)
                    Next
                End If
            End If

            If bolRet Then
                For Each dr As DataRow In _dtTransferReturnItem.Rows
                    Dim objTransferItem As New TransferReturnItemInfo
                    Dim objForSale As New SalesItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objTransferItem
                            .TransferReturnID = TransferObj.TransferReturnID
                            .TransferReturnItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.TransferReturnItem, EnumSetting.GenerateKeyType.TransferReturnItem.ToString, TransferObj.TransferReturnDate)
                            .ForSaleID = dr.Item("ForSaleID")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .OriginalGemsPrice = dr.Item("OriginalGemsPrice")
                            .OriginalPriceGram = dr.Item("OriginalPriceGram")
                            .OriginalPriceTK = dr.Item("OriginalPriceTK")
                            .PriceCode = dr.Item("PriceCode")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolItemRet = _objTransferReturnDA.InsertTransferReturnItem(objTransferItem)
                        UpdateSalesItemByIsExitForTransferReturn(dr.Item("ForSaleID"), True, TransferObj.TransferReturnDate, TransferObj.LocationID)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objTransferItem
                            .TransferReturnID = TransferObj.TransferReturnID
                            .TransferReturnItemID = dr.Item("TransferReturnItemID")
                            .ForSaleID = dr.Item("ForSaleID")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .OriginalGemsPrice = dr.Item("OriginalGemsPrice")
                            .OriginalPriceGram = dr.Item("OriginalPriceGram")
                            .OriginalPriceTK = dr.Item("OriginalPriceTK")
                            .PriceCode = dr.Item("PriceCode")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolItemRet = _objTransferReturnDA.UpdateTransferReturnItem(objTransferItem)
                        UpdateSalesItemByIsExitForTransferReturn(dr.Item("ForSaleID"), True, TransferObj.TransferReturnDate, TransferObj.LocationID)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateSalesItemByIsExitForTransferReturn(dr.Item("ForSaleID"), True, TransferObj.TransferReturnDate, TransferObj.LocationID)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolItemRet = _objTransferReturnDA.DeleteTransferReturnItem(CStr(dr.Item("TransferReturnItemID", DataRowVersion.Original)))
                        UpdateSalesItemByIsExitForTransferReturn(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), False, TransferObj.TransferReturnDate, Global_CurrentLocationID)
                    End If
                Next


            End If

            Return bolRet
        End Function

        Public Function GetHeaderTransferReturnList() As System.Data.DataTable Implements ITransferReturnController.GetHeaderTransferReturnList
            Return _objTransferReturnDA.GetHeaderTransferReturnList()
        End Function
        Public Function GetBranchTransferReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferReturnController.GetBranchTransferReturnReport
            Return _objTransferReturnDA.GetBranchTransferReturnReport(FromDate, ToDate, criStr)
        End Function
        'Public Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferReturnController.GetBranchTransferReport
        '    Return _objTransferReturnDA.GetBranchTransferReport(FromDate, ToDate, criStr)
        'End Function
    End Class
End Namespace
