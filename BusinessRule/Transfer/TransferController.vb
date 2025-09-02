Imports DataAccess.Transfer
Imports DataAccess.SalesItem
Imports CommonInfo
Namespace Transfer
    Public Class TransferController
        Implements ITransferController




#Region "Private Members"

        Private _objTransferDA As ITransferDA
        Private _objSaleItemDA As DataAccess.SalesItem.ISalesItemDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ITransferController = New TransferController

#End Region

#Region "Constructors"

        Private Sub New()
            _objTransferDA = DataAccess.Factory.Instance.CreateTransferDA
            _objSaleItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ITransferController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .LocationID = Global_CurrentLocationID
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate
            End With
            _objSaleItemDA.UpdateSaleItemIsExitForTransfer(objSaleItem)
        End Sub
        Private Sub UpdateTransferItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date, ByVal OtherLocationID As String)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .LocationID = OtherLocationID
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate
            End With
            _objSaleItemDA.UpdateTransferItemIsExitForTransfer(objSaleItem)
        End Sub

        Public Function DeleteTransfer(ByVal TransferID As String) As Boolean Implements ITransferController.DeleteTransfer
            Dim tmpdt As New DataTable
            tmpdt = _objTransferDA.GetForSaleIDByTransferID(TransferID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateTransferItemByIsExit(tmpdr.Item("ForSaleID"), False, Now, Global_CurrentLocationID)
                Next
            End If
            If _objTransferDA.DeleteTransfer(TransferID) Then

                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       TransferID, _
                                       "Delete Transfer")
                Return True
            Else
                Return False
            End If

        End Function

        Public Function GetAllTransfer() As System.Data.DataTable Implements ITransferController.GetAllTransfer
            Return _objTransferDA.GetAllTransfer()
        End Function

        Public Function GetTransfer(ByVal TransferID As String) As CommonInfo.TransferInfo Implements ITransferController.GetTransfer
            Return _objTransferDA.GetTransfer(TransferID)
        End Function

        Public Function GetTransferItem(ByVal TransferID As String) As System.Data.DataTable Implements ITransferController.GetTransferItem
            Return _objTransferDA.GetTransferItem(TransferID)
        End Function

        Public Function SaveTransfer(ByVal TransferObj As CommonInfo.TransferInfo, ByVal _dtTransferItem As System.Data.DataTable) As Boolean Implements ITransferController.SaveTransfer
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            Dim bolRet As Boolean = False
            Dim bolItemRet As Boolean = False
            If TransferObj.TransferID = "" Then
                TransferObj.TransferID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.Transfer, CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, TransferObj.TransferDate)

                bolRet = _objTransferDA.InsertTransfer(TransferObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       TransferObj.TransferID, _
                                       "Insert Transfer")
            Else
                bolRet = _objTransferDA.UpdateTransfer(TransferObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.Transfer.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       TransferObj.TransferID, _
                                       "Update Transfer")


                Dim tmpdt As New DataTable
                tmpdt = _objTransferDA.GetForSaleIDByTransferID(TransferObj.TransferID)

                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateSalesItemByIsExit(tmpdr.Item("ForSaleID"), False, Now)
                        'If (tmpdr.Item("IsConfirm") = True) Then
                        '    UpdateTransferItemByIsExit(tmpdr.Item("ForSaleID"), False, Now, TransferObj.LocationID)
                        'Else
                        '    
                        'End If

                    Next
                End If
            End If

            If bolRet Then
                For Each dr As DataRow In _dtTransferItem.Rows
                    Dim objTransferItem As New TransferItemInfo
                    Dim objForSale As New SalesItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objTransferItem
                            .TransferID = TransferObj.TransferID
                            .TransferItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.TransferItem, EnumSetting.GenerateKeyType.TransferItem.ToString, TransferObj.TransferDate)
                            .ForSaleID = dr.Item("ForSaleID")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .OriginalGemsPrice = dr.Item("OriginalGemsPrice")
                            .OriginalPriceGram = dr.Item("OriginalPriceGram")
                            .OriginalPriceTK = dr.Item("OriginalPriceTK")
                            .PriceCode = dr.Item("PriceCode")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolItemRet = _objTransferDA.InsertTransferItem(objTransferItem)
                        'If TransferObj.IsConfirm = True Then
                        '    UpdateTransferItemByIsExit(dr.Item("ForSaleID"), False, TransferObj.TransferDate, TransferObj.LocationID)
                        'Else
                        '    UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, TransferObj.TransferDate)
                        'End If
                        UpdateTransferItemByIsExit(dr.Item("ForSaleID"), False, TransferObj.TransferDate, TransferObj.LocationID)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objTransferItem
                            .TransferID = TransferObj.TransferID
                            .TransferItemID = dr.Item("TransferItemID")
                            .ForSaleID = dr.Item("ForSaleID")
                            .OriginalFixedPrice = dr.Item("OriginalFixedPrice")
                            .OriginalGemsPrice = dr.Item("OriginalGemsPrice")
                            .OriginalPriceGram = dr.Item("OriginalPriceGram")
                            .OriginalPriceTK = dr.Item("OriginalPriceTK")
                            .PriceCode = dr.Item("PriceCode")
                            .FixPrice = dr.Item("FixPrice")
                        End With
                        bolItemRet = _objTransferDA.UpdateTransferItem(objTransferItem)
                        'If TransferObj.IsConfirm = True Then
                        '    UpdateTransferItemByIsExit(dr.Item("ForSaleID"), False, TransferObj.TransferDate, TransferObj.LocationID)
                        'Else
                        '    UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, TransferObj.TransferDate)
                        'End If
                        UpdateTransferItemByIsExit(dr.Item("ForSaleID"), False, TransferObj.TransferDate, TransferObj.LocationID)
                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        'If TransferObj.IsConfirm = True Then
                        '    UpdateTransferItemByIsExit(dr.Item("ForSaleID"), False, TransferObj.TransferDate, TransferObj.LocationID)
                        'Else
                        '    UpdateSalesItemByIsExit(dr.Item("ForSaleID"), True, TransferObj.TransferDate)
                        'End If
                        UpdateTransferItemByIsExit(dr.Item("ForSaleID"), False, TransferObj.TransferDate, TransferObj.LocationID)
                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolItemRet = _objTransferDA.DeleteTransferItem(CStr(dr.Item("TransferItemID", DataRowVersion.Original)))
                        UpdateTransferItemByIsExit(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), False, TransferObj.TransferDate, TransferObj.CurrentLocationID)
                    End If
                Next


            End If

            Return bolRet
        End Function

        Public Function GetHeaderTransferList() As System.Data.DataTable Implements ITransferController.GetHeaderTransferList
            Return _objTransferDA.GetHeaderTransferList()
        End Function
        Public Function GetBranchTransferReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal criStr As String = "") As System.Data.DataTable Implements ITransferController.GetBranchTransferReport
            Return _objTransferDA.GetBranchTransferReport(FromDate, ToDate, criStr)
        End Function
    End Class
End Namespace
