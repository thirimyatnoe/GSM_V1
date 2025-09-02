Imports DataAccess.Repair
Imports CommonInfo
Namespace Repair
    Public Class RepairController
        Implements IRepairController

#Region "Private Members"

        Private _objRepairDA As IRepairDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IRepairController = New RepairController

#End Region

#Region "Constructors"

        Private Sub New()
            _objRepairDA = DataAccess.Factory.Instance.CreateRepairDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IRepairController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function DeleteRepairReceive(ByVal RepairID As String) As Boolean Implements IRepairController.DeleteRepairReceive

            If _objRepairDA.CheckIsUseInRepairReturnHeader(RepairID) = False Then
                If _objRepairDA.DeleteRepairReceiveHeader(RepairID) Then
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.RepairStock.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                          RepairID, _
                                          "Delete Repair Stock")
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Function

        Public Function SaveRepairReceive(ByVal obj As CommonInfo.RepairHeaderInfo, ByVal _dtRepairDetail As DataTable) As Boolean Implements IRepairController.SaveRepairReceive
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim bolRet As Boolean = False

            If obj.RepairID = "0" Then
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.RepairStock.ToString)
                obj.RepairID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.RepairDate)
                bolRet = _objRepairDA.InsertRepairHeader(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.RepairStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.RepairID, _
                                       "Insert Repair Stock")
            Else
                bolRet = _objRepairDA.UpdateRepairHeader(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.RepairStock.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                                       obj.RepairID, _
                                                                       "Update Repair Stock")

            End If
            If bolRet = True Then
                For Each dr As DataRow In _dtRepairDetail.Rows
                    Dim objRepairDetail As New RepairDetailInfo
                    If dr.RowState = DataRowState.Added Then
                        With objRepairDetail
                            .RepairDetailID = dr.Item("RepairDetailID")
                            .RepairID = obj.RepairID
                            .IsFromShop = dr.Item("IsFromShop")
                            .BarcodeNo = dr.Item("BarcodeNo")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .LengthOrWidth = dr.Item("LengthOrWidth")
                            .CurrentPrice = dr.Item("CurrentPrice")
                            .Design = dr.Item("Design")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .IsExit = dr.Item("IsExit")
                            .DetailRemark = dr.Item("DetailRemark")
                        End With
                        bolRet = _objRepairDA.InsertRepairReceiveDetail(objRepairDetail)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objRepairDetail
                            .RepairDetailID = dr.Item("RepairDetailID")
                            .RepairID = dr.Item("RepairID")
                            .IsFromShop = dr.Item("IsFromShop")
                            .BarcodeNo = dr.Item("BarcodeNo")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .LengthOrWidth = dr.Item("LengthOrWidth")
                            .CurrentPrice = dr.Item("CurrentPrice")
                            .Design = dr.Item("Design")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .IsExit = dr.Item("IsExit")
                            .DetailRemark = dr.Item("DetailRemark")
                        End With
                        bolRet = _objRepairDA.UpdateRepairReceiveDetail(objRepairDetail)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objRepairDetail
                            .RepairDetailID = dr.Item("RepairDetailID")
                            .RepairID = dr.Item("RepairID")
                            .IsFromShop = dr.Item("IsFromShop")
                            .BarcodeNo = dr.Item("BarcodeNo")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemNameID = dr.Item("ItemNameID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .LengthOrWidth = dr.Item("LengthOrWidth")
                            .CurrentPrice = dr.Item("CurrentPrice")
                            .Design = dr.Item("Design")
                            .ItemTG = dr.Item("ItemTG")
                            .ItemTK = dr.Item("ItemTK")
                            .IsExit = dr.Item("IsExit")
                            .DetailRemark = dr.Item("DetailRemark")
                        End With
                        bolRet = _objRepairDA.UpdateRepairReceiveDetail(objRepairDetail)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objRepairDA.DeleteRepairDetail(objRepairDetail.RepairDetailID)
                    End If
                Next
            End If
            Return bolRet

        End Function
        Public Function GetAllRepairHeader() As DataTable Implements IRepairController.GetAllRepairHeader
            Return _objRepairDA.GetAllRepairHeader()
        End Function

        Public Function GetRepairHeaderInfo(RepairID As String) As RepairHeaderInfo Implements IRepairController.GetRepairHeaderInfo
            Return _objRepairDA.GetRepairHeaderInfo(RepairID)
        End Function

        Public Function GetRepairReceiveDetail(RepairID As String) As DataTable Implements IRepairController.GetRepairReceiveDetail
            Return _objRepairDA.GetRepairReceiveDetail(RepairID)
        End Function
        Public Function GetReturnRepairHeaderForIsPaid() As DataTable Implements IRepairController.GetReturnRepairHeaderForIsPaid
            Return _objRepairDA.GetReturnRepairHeaderForIsPaid()
        End Function
        Public Function GetForRepairReturnbyRepairDetail(RepairID As String, Optional ByVal BarcodeNo As String = "") As DataTable Implements IRepairController.GetForRepairReturnbyRepairDetail
            Return _objRepairDA.GetForRepairReturnbyRepairDetail(RepairID, BarcodeNo)
        End Function

        Public Function GetRepairReceiveDetailForUpdate(RepairID As String) As DataTable Implements IRepairController.GetRepairReceiveDetailForUpdate
            Return _objRepairDA.GetRepairReceiveDetailForUpdate(RepairID)
        End Function

        Public Function GetRepairDetailInfo(BarcodeNo As String) As RepairDetailInfo Implements IRepairController.GetRepairDetailInfo
            Return _objRepairDA.GetRepairDetailInfo(BarcodeNo)
        End Function
        Public Function GetBalaceAmountByReceiveID(RepairID As String) As DataTable Implements IRepairController.GetBalaceAmountByReceiveID
            Return _objRepairDA.GetBalaceAmountByReceiveID(RepairID)
        End Function
        Public Function GetRepairReceiveForVoucher(RepairID As String) As DataTable Implements IRepairController.GetRepairReceiveForVoucher
            Return _objRepairDA.GetRepairReceiveForVoucher(RepairID)
        End Function
        Public Function GetRepairReceiveSummary(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements IRepairController.GetRepairReceiveSummary
            Return _objRepairDA.GetRepairReceiveSummary(FromDate, ToDate, Cristr)
        End Function
        Public Function GetRepairStockDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairDetailInfo Implements IRepairController.GetRepairStockDetailForTotal
            Return _objRepairDA.GetRepairStockDetailForTotal(FromDate, ToDate, criStr)
        End Function
    End Class
End Namespace

