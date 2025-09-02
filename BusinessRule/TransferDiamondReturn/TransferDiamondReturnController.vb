Imports DataAccess.TransferDiamondReturn
Imports DataAccess.SalesItem
Imports DataAccess.Location
Imports CommonInfo
Namespace TransferDiamondReturn
    Public Class TransferDiamondReturnController
        Implements ITransferDiamondReturnController




#Region "Private Members"

        Private _objTransferDiamondReturnDA As ITransferDiamondReturnDA
        Private _objSaleItemDA As DataAccess.SalesItem.ISalesItemDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private _objLocationDA As DataAccess.Location.ILocationDA
        Private Shared ReadOnly _instance As ITransferDiamondReturnController = New TransferDiamondReturnController

#End Region

#Region "Constructors"

        Private Sub New()
            _objTransferDiamondReturnDA = DataAccess.Factory.Instance.CreateTransferDiamondReturnDA
            _objSaleItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ITransferDiamondReturnController
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
        Public Function DeleteTransferReturn(ByVal TransferReturnID As String) As Boolean Implements ITransferDiamondReturnController.DeleteTransferReturn
            Dim tmpdt As New DataTable
            Dim dtHeadLocation As New DataTable
            tmpdt = _objTransferDiamondReturnDA.GetForSaleIDByTransferReturnID(TransferReturnID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateSalesItemByIsExitForTransferReturn(tmpdr.Item("ForSaleID"), False, Now, Global_CurrentLocationID)
                Next
            End If
            If _objTransferDiamondReturnDA.DeleteTransferReturn(TransferReturnID) Then

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

        Public Function GetAllTransferReturn() As System.Data.DataTable Implements ITransferDiamondReturnController.GetAllTransferReturn
            Return _objTransferDiamondReturnDA.GetAllTransferReturn()
        End Function

        Public Function GetTransferReturn(ByVal TransferReturnID As String) As CommonInfo.TransferReturnDiamondInfo Implements ITransferDiamondReturnController.GetTransferReturn
            Return _objTransferDiamondReturnDA.GetTransferReturn(TransferReturnID)
        End Function

        Public Function GetTransferReturnItem(ByVal TransferReturnID As String) As System.Data.DataTable Implements ITransferDiamondReturnController.GetTransferReturnItem
            Return _objTransferDiamondReturnDA.GetTransferReturnItem(TransferReturnID)
        End Function

        Public Function SaveTransferReturn(ByVal TransferObj As CommonInfo.TransferReturnDiamondInfo, ByVal _dtTransferReturnItem As System.Data.DataTable) As Boolean Implements ITransferDiamondReturnController.SaveTransferReturn
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            Dim bolItemRet As Boolean = False
            If TransferObj.TransferReturnID = "" Then
                TransferObj.TransferReturnID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.TransferReturnDiamond, CommonInfo.EnumSetting.GenerateKeyType.TransferReturnDiamond.ToString, TransferObj.TransferReturnDate)

                bolRet = _objTransferDiamondReturnDA.InsertTransferReturn(TransferObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       TransferObj.TransferReturnID, _
                                       "Insert Transfer")
            Else
                bolRet = _objTransferDiamondReturnDA.UpdateTransferReturn(TransferObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       TransferObj.TransferReturnID, _
                                       "Update Transfer")


                Dim tmpdt As New DataTable
                tmpdt = _objTransferDiamondReturnDA.GetForSaleIDByTransferReturnID(TransferObj.TransferReturnID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByIsExitForTransferReturn(tmpdr.Item("ForSaleID"), False, Now, TransferObj.LocationID)
                    Next
                End If
            End If

            If bolRet Then
                For Each dr As DataRow In _dtTransferReturnItem.Rows
                    Dim objTransferItem As New TransferReturnDiamondItemInfo
                    Dim objForSale As New SalesItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objTransferItem
                            .TransferReturnID = TransferObj.TransferReturnID
                            .TransferReturnItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.TransferReturnDiamondItem, EnumSetting.GenerateKeyType.TransferReturnDiamondItem.ToString, TransferObj.TransferReturnDate)
                            .ForSaleID = dr.Item("ForSaleID")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .OriginalPriceCarat = dr.Item("OriginalPriceCarat")
                            .PriceCode = dr.Item("PriceCode")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolItemRet = _objTransferDiamondReturnDA.InsertTransferReturnItem(objTransferItem)
                        UpdateSalesItemByIsExitForTransferReturn(dr.Item("ForSaleID"), True, TransferObj.TransferReturnDate, TransferObj.LocationID)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objTransferItem
                            .TransferReturnID = TransferObj.TransferReturnID
                            .TransferReturnItemID = dr.Item("TransferReturnItemID")
                            .ForSaleID = dr.Item("ForSaleID")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .OriginalPriceCarat = dr.Item("OriginalPriceCarat")
                            .PriceCode = dr.Item("PriceCode")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolItemRet = _objTransferDiamondReturnDA.UpdateTransferReturnItem(objTransferItem)
                        UpdateSalesItemByIsExitForTransferReturn(dr.Item("ForSaleID"), True, TransferObj.TransferReturnDate, TransferObj.LocationID)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        UpdateSalesItemByIsExitForTransferReturn(dr.Item("ForSaleID"), True, TransferObj.TransferReturnDate, TransferObj.LocationID)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolItemRet = _objTransferDiamondReturnDA.DeleteTransferReturnItem(CStr(dr.Item("TransferReturnItemID", DataRowVersion.Original)))
                        UpdateSalesItemByIsExitForTransferReturn(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), False, TransferObj.TransferReturnDate, Global_CurrentLocationID)
                    End If
                Next


            End If

            Return bolRet
        End Function

        Public Function GetHeaderTransferReturnList() As System.Data.DataTable Implements ITransferDiamondReturnController.GetHeaderTransferReturnList
            Return _objTransferDiamondReturnDA.GetHeaderTransferReturnList()
        End Function
        Public Function GetBranchTransferReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferDiamondReturnController.GetBranchTransferReturnReport
            Return _objTransferDiamondReturnDA.GetBranchTransferReturnReport(FromDate, ToDate, criStr)
        End Function
        'Public Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferDiamondReturnController.GetBranchTransferReport
        '    Return _objTransferDiamondReturnDA.GetBranchTransferReport(FromDate, ToDate, criStr)
        'End Function
    End Class
End Namespace
