Imports DataAccess.RepairReturn
Imports DataAccess.Repair
Imports CommonInfo
Namespace RepairReturn
    Public Class RepairReturnController
        Implements IRepairReturnController


#Region "Private Members"

        Private _objRepairReturnDA As IRepairReturnDA
        Private _objRepairDA As IRepairDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IRepairReturnController = New RepairReturnController

#End Region

#Region "Constructors"

        Private Sub New()
            _objRepairReturnDA = DataAccess.Factory.Instance.CreateRepairReturnDA
            _objRepairDA = DataAccess.Factory.Instance.CreateRepairDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IRepairReturnController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteRepairReturn(ByVal RepairReturnHeaderID As String, ByVal RepairID As String) As Boolean Implements IRepairReturnController.DeleteRepairReturn
            Dim tmpdt As New DataTable
            Dim Obj As New OrderReturnHeader

            tmpdt = _objRepairReturnDA.GetRepairReturnDetailByHeaderID(RepairReturnHeaderID)
            If tmpdt.Rows.Count > 0 Then
                For Each tmpdr As DataRow In tmpdt.Rows
                    UpdateRepairDetailByIsExit(tmpdr.Item("RepairDetailID"), False)
                Next
                UpdateRepairReceiveByIsReturn(RepairID, False, Now)
            End If

            If _objRepairReturnDA.DeleteRepairReturn(RepairReturnHeaderID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.RepairStockReturn.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       RepairReturnHeaderID, _
                                                       "Delete Repair Stock Return")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function SaveRepairReturn(objReturn As RepairReturnHeaderInfo, _dtRepairDetail As DataTable, _dtAllGem As DataTable) As Boolean Implements IRepairReturnController.SaveRepairReturn
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim _ObjRepairController As BusinessRule.Repair.IRepairController = Factory.Instance.CreateRepairController
            Dim bolRet As Boolean = False
            Dim _GeneralCon As General.IGeneralController = Factory.Instance.CreateGeneralController
            'Dim StrODHeaderID As Integer

            If objReturn.ReturnRepairID = "0" Then
                objReturn.ReturnRepairID = _GeneralCon.GenerateKey(EnumSetting.GenerateKeyType.RepairReturnHeader, EnumSetting.GenerateKeyType.RepairReturnHeader.ToString, Now)
                bolRet = _objRepairReturnDA.InsertRepairReturnHeader(objReturn)
                'If (StrODHeaderID > 0) Then
                '    bolRet = True
                'Else
                '    bolRet = False

                'End If
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                      DateTime.Now, _
                                      Global_UserID, _
                                      CommonInfo.EnumSetting.GenerateKeyType.RepairStockReturn.ToString, _
                                      CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                      objReturn.ReturnRepairID, _
                                      "Insert Repair Stock Return")
            Else
                bolRet = _objRepairReturnDA.UpdateRepairReturnHeader(objReturn)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                                       DateTime.Now, _
                                                                       Global_UserID, _
                                                                       CommonInfo.EnumSetting.GenerateKeyType.RepairStockReturn.ToString, _
                                                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                                      objReturn.ReturnRepairID, _
                                                                   "Update Repair Stock Return")

                Dim tmpdt As New DataTable
                tmpdt = _objRepairReturnDA.GetRepairReturnDetailByHeaderID(objReturn.ReturnRepairID)
                If tmpdt.Rows.Count > 0 Then
                    For Each tmpdr As DataRow In tmpdt.Rows
                        UpdateRepairDetailByIsExit(tmpdr.Item("RepairDetailID"), False)
                    Next
                End If

            End If

            If bolRet = True Then
                Dim objReturnDetailInfo As New RepairReturnDetailInfo
                For Each dr As DataRow In _dtRepairDetail.Rows
                    If dr.RowState = DataRowState.Added Then
                        With objReturnDetailInfo
                            .ReturnRepairDetailID = dr.Item("ReturnRepairDetailID")
                            'If objReturn.ReturnRepairID = "0" Then
                            '    .ReturnRepairID = StrODHeaderID
                            'Else
                            .ReturnRepairID = objReturn.ReturnRepairID
                            'End If
                            .RepairDetailID = dr.Item("RepairDetailID")
                            .ChangeSaleRate = dr.Item("ChangeSaleRate")
                            .ReturnItemTG = dr.Item("ReturnItemTG")
                            .ReturnItemTK = dr.Item("ReturnItemTK")
                            .ReturnGemTG = dr.Item("ReturnGemTG")
                            .ReturnGemTK = dr.Item("ReturnGemTK")
                            .ReturnGoldTK = dr.Item("ReturnGoldTK")
                            .ReturnGoldTG = dr.Item("ReturnGoldTG")
                            .OrgGoldTG = dr.Item("OrgGoldTG")
                            .OrgGoldTK = dr.Item("OrgGoldTK")
                            .OrgGemTG = dr.Item("OrgGemTG")
                            .OrgGemTK = dr.Item("OrgGemTK")
                            .WasteTG = dr.Item("WasteTG")
                            .WasteTK = dr.Item("WasteTK")
                            .DesignCharges = dr.Item("DesignCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .ReturnGoldPrice = dr.Item("ReturnGoldPrice")
                            .ReturnGemPrice = dr.Item("ReturnGemPrice")
                            .ReturnTotalAmount = dr.Item("ReturnTotalAmount")
                            .ReturnAddOrSub = dr.Item("ReturnAddOrSub")
                        End With
                        _objRepairReturnDA.InsertRepairRreturnDetail(objReturnDetailInfo)
                        UpdateRepairDetailByIsExit(dr.Item("RepairDetailID"), True)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objReturnDetailInfo
                            .ReturnRepairDetailID = dr.Item("ReturnRepairDetailID")
                            'If objReturn.ReturnRepairID = "0" Then
                            '    .ReturnRepairID = StrODHeaderID
                            'Else
                            .ReturnRepairID = objReturn.ReturnRepairID
                            'End If
                            .RepairDetailID = dr.Item("RepairDetailID")
                            .ChangeSaleRate = dr.Item("ChangeSaleRate")
                            .ReturnItemTG = dr.Item("ReturnItemTG")
                            .ReturnItemTK = dr.Item("ReturnItemTK")
                            .ReturnGemTG = dr.Item("ReturnGemTG")
                            .ReturnGemTK = dr.Item("ReturnGemTK")
                            .ReturnGoldTK = dr.Item("ReturnGoldTK")
                            .ReturnGoldTG = dr.Item("ReturnGoldTG")
                            .OrgGoldTG = dr.Item("OrgGoldTG")
                            .OrgGoldTK = dr.Item("OrgGoldTK")
                            .OrgGemTG = dr.Item("OrgGemTG")
                            .OrgGemTK = dr.Item("OrgGemTK")
                            .WasteTG = dr.Item("WasteTG")
                            .WasteTK = dr.Item("WasteTK")
                            .DesignCharges = dr.Item("DesignCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .ReturnGoldPrice = dr.Item("ReturnGoldPrice")
                            .ReturnGemPrice = dr.Item("ReturnGemPrice")
                            .ReturnTotalAmount = dr.Item("ReturnTotalAmount")
                            .ReturnAddOrSub = dr.Item("ReturnAddOrSub")
                        End With
                        _objRepairReturnDA.UpdateRepairRreturnDetail(objReturnDetailInfo)
                        UpdateRepairDetailByIsExit(dr.Item("RepairDetailID"), True)

                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objReturnDetailInfo
                            .ReturnRepairDetailID = dr.Item("ReturnRepairDetailID")
                            'If objReturn.ReturnRepairID = "0" Then
                            '    .ReturnRepairID = StrODHeaderID
                            'Else
                            .ReturnRepairID = objReturn.ReturnRepairID
                            'End If
                            .RepairDetailID = dr.Item("RepairDetailID")
                            .ChangeSaleRate = dr.Item("ChangeSaleRate")
                            .ReturnItemTG = dr.Item("ReturnItemTG")
                            .ReturnItemTK = dr.Item("ReturnItemTK")
                            .ReturnGemTG = dr.Item("ReturnGemTG")
                            .ReturnGemTK = dr.Item("ReturnGemTK")
                            .ReturnGoldTK = dr.Item("ReturnGoldTK")
                            .ReturnGoldTG = dr.Item("ReturnGoldTG")
                            .OrgGoldTG = dr.Item("OrgGoldTG")
                            .OrgGoldTK = dr.Item("OrgGoldTK")
                            .OrgGemTG = dr.Item("OrgGemTG")
                            .OrgGemTK = dr.Item("OrgGemTK")
                            .WasteTG = dr.Item("WasteTG")
                            .WasteTK = dr.Item("WasteTK")
                            .DesignCharges = dr.Item("DesignCharges")
                            .PlatingCharges = dr.Item("PlatingCharges")
                            .MountingCharges = dr.Item("MountingCharges")
                            .WhiteCharges = dr.Item("WhiteCharges")
                            .ReturnGoldPrice = dr.Item("ReturnGoldPrice")
                            .ReturnGemPrice = dr.Item("ReturnGemPrice")
                            .ReturnTotalAmount = dr.Item("ReturnTotalAmount")
                            .ReturnAddOrSub = dr.Item("ReturnAddOrSub")
                        End With
                        _objRepairReturnDA.UpdateRepairRreturnDetail(objReturnDetailInfo)
                        UpdateRepairDetailByIsExit(dr.Item("RepairDetailID"), True)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        Dim row As DataRow
                        Dim j As Integer = _dtAllGem.Rows.Count() - 1
                        While j >= 0
                            row = _dtAllGem.Rows(j)
                            If row.Item("ReturnRepairDetailID") = dr.Item("ReturnRepairDetailID", DataRowVersion.Original) Then
                                _dtAllGem.Rows.Remove(row)
                            End If
                            j = j - 1
                        End While
                        bolRet = _objRepairReturnDA.DeleteReturnRepairDetail(CStr(dr.Item("ReturnRepairDetailID", DataRowVersion.Original)))
                    End If

                    If dr.RowState <> DataRowState.Deleted Then
                        Dim dt As New DataTable
                        dt = _objRepairReturnDA.GetReturnRepairGemItemByDetailID(dr.Item("ReturnRepairDetailID"))
                        If dt.Rows.Count > 0 Then
                            For Each tmpdr As DataRow In dt.Rows
                                _objRepairReturnDA.DeleteRepairReturnGemsItemByGemsID(tmpdr.Item("ReturnRepairGemID"))
                            Next
                        End If
                    End If
                Next
            End If
            If bolRet = True Then
                For Each dr As DataRow In _dtAllGem.Rows
                    Dim objGemItem As New RepairReturnGemInfo
                    If dr.RowState = DataRowState.Added Then
                        With objGemItem
                            .ReturnRepairGemID = dr.Item("ReturnRepairGemID")
                            .ReturnRepairDetailID = dr.Item("ReturnRepairDetailID")
                            .GemsCategoryID = IIf(IsDBNull(dr.Item("GemsCategoryID")), "", dr.Item("GemsCategoryID"))
                            .Description = IIf(IsDBNull(dr.Item("Description")), "-", dr.Item("Description"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")), 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")), 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")), "", dr.Item("YOrCOrG"))
                            .GemsTW = IIf(IsDBNull(dr.Item("GemsTW")), 0, dr.Item("GemsTW"))
                            .QTY = IIf(IsDBNull(dr.Item("QTY")), 0, dr.Item("QTY"))
                            .UnitPrice = IIf(IsDBNull(dr.Item("UnitPrice")), 0, dr.Item("UnitPrice"))
                            .Type = IIf(IsDBNull(dr.Item("Type")), "", dr.Item("Type"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")), 0, dr.Item("Amount"))
                            .IsNewGems = IIf(IsDBNull(dr.Item("IsNewGems")), False, dr.Item("IsNewGems"))
                        End With

                        bolRet = _objRepairReturnDA.InsertRepairReturnGemItem(objGemItem)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objGemItem
                            .ReturnRepairGemID = dr.Item("ReturnRepairGemID")
                            .ReturnRepairDetailID = dr.Item("ReturnRepairDetailID")
                            .GemsCategoryID = IIf(IsDBNull(dr.Item("GemsCategoryID")), "", dr.Item("GemsCategoryID"))
                            .Description = IIf(IsDBNull(dr.Item("Description")), "-", dr.Item("Description"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")), 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")), 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")), "", dr.Item("YOrCOrG"))
                            .GemsTW = IIf(IsDBNull(dr.Item("GemsTW")), 0, dr.Item("GemsTW"))
                            .QTY = IIf(IsDBNull(dr.Item("QTY")), 0, dr.Item("QTY"))
                            .UnitPrice = IIf(IsDBNull(dr.Item("UnitPrice")), 0, dr.Item("UnitPrice"))
                            .Type = IIf(IsDBNull(dr.Item("Type")), "", dr.Item("Type"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")), 0, dr.Item("Amount"))
                            .IsNewGems = IIf(IsDBNull(dr.Item("IsNewGems")), False, dr.Item("IsNewGems"))
                        End With
                        bolRet = _objRepairReturnDA.InsertRepairReturnGemItem(objGemItem)
                    ElseIf dr.RowState = DataRowState.Unchanged Then
                        With objGemItem
                            .ReturnRepairGemID = dr.Item("ReturnRepairGemID")
                            .ReturnRepairDetailID = dr.Item("ReturnRepairDetailID")
                            .GemsCategoryID = IIf(IsDBNull(dr.Item("GemsCategoryID")), "", dr.Item("GemsCategoryID"))
                            .Description = IIf(IsDBNull(dr.Item("Description")), "-", dr.Item("Description"))
                            .GemsTK = IIf(IsDBNull(dr.Item("GemsTK")), 0, dr.Item("GemsTK"))
                            .GemsTG = IIf(IsDBNull(dr.Item("GemsTG")), 0, dr.Item("GemsTG"))
                            .YOrCOrG = IIf(IsDBNull(dr.Item("YOrCOrG")), "", dr.Item("YOrCOrG"))
                            .GemsTW = IIf(IsDBNull(dr.Item("GemsTW")), 0, dr.Item("GemsTW"))
                            .QTY = IIf(IsDBNull(dr.Item("QTY")), 0, dr.Item("QTY"))
                            .UnitPrice = IIf(IsDBNull(dr.Item("UnitPrice")), "", dr.Item("UnitPrice"))
                            .Type = IIf(IsDBNull(dr.Item("Type")), "", dr.Item("Type"))
                            .Amount = IIf(IsDBNull(dr.Item("Amount")), "", dr.Item("Amount"))
                            .IsNewGems = IIf(IsDBNull(dr.Item("IsNewGems")), False, dr.Item("IsNewGems"))
                        End With
                        bolRet = _objRepairReturnDA.InsertRepairReturnGemItem(objGemItem)
                    End If

                Next
            End If
            If objReturn.RepairID <> "" Then
                Dim dtreceive As DataTable
                Dim dtreturn As New DataTable
                Dim ReceiveQTY As Integer = 0
                Dim ReturnQTY As Integer = 0
                Dim obj As CommonInfo.RepairHeaderInfo

                dtreceive = _objRepairDA.GetRepairReceiveDetail(objReturn.RepairID)
                ReceiveQTY = dtreceive.Rows.Count
                dtreturn = _objRepairReturnDA.GetRepairReturnDetailByRepairID(objReturn.RepairID)
                ReturnQTY = dtreturn.Rows.Count

                If (ReceiveQTY = ReturnQTY) Then
                    UpdateRepairReceiveByIsReturn(objReturn.RepairID, True, Now)
                ElseIf objReturn.ReturnRepairID <> "0" Then
                    UpdateRepairReceiveByIsReturn(objReturn.RepairID, False, Now)
                End If
            End If

            'If (bolRet = True) Then
            '    If objReturn.ReturnRepairID = 0 Then
            '        Return StrODHeaderID
            '    Else
            '        Return objReturn.ReturnRepairID
            '    End If
            'Else
            '    Return 0
            'End If
            Return bolRet
        End Function
        Private Sub UpdateRepairReceiveByIsReturn(ByVal argRepairID As String, ByVal argIsReturn As Boolean, ByVal argReturnDate As Date)
            Dim objRepair As New CommonInfo.RepairHeaderInfo
            With objRepair
                .RepairID = argRepairID
                .IsAllReturn = argIsReturn
                .ReturnDate = argReturnDate
            End With
            _objRepairReturnDA.UpdateRepairReceiveHeaderByIsReturn(objRepair)
        End Sub

        Private Sub UpdateRepairDetailByIsExit(ByVal argRepairDetailID As String, ByVal argIsExit As Boolean)
            Dim objRepair As New CommonInfo.RepairDetailInfo
            With objRepair
                .RepairDetailID = argRepairDetailID
                .IsExit = argIsExit
            End With
            _objRepairReturnDA.UpdateRepairDetailByIsExit(objRepair)
        End Sub
        Public Function GetAllRepairReturnHeader() As DataTable Implements IRepairReturnController.GetAllRepairReturnHeader
            Return _objRepairReturnDA.GetAllRepairReturnHeader()
        End Function
        Public Function GetRepairReturnHeaderInfoByID(ByVal RepairReturnID As String) As RepairReturnHeaderInfo Implements IRepairReturnController.GetRepairReturnHeaderInfoByID
            Return _objRepairReturnDA.GetRepairReturnHeaderInfoByID(RepairReturnID)
        End Function
        Public Function GetRepairReturnDetailByHeaderID(ByVal RepairReturnID As String) As DataTable Implements IRepairReturnController.GetRepairReturnDetailByHeaderID
            Return _objRepairReturnDA.GetRepairReturnDetailByHeaderID(RepairReturnID)
        End Function
        Public Function GetRepairReturnGemDataByHeaderID(ByVal RepairReturnID As String) As DataTable Implements IRepairReturnController.GetRepairReturnGemDataByHeaderID
            Return _objRepairReturnDA.GetRepairReturnGemDataByHeaderID(RepairReturnID)
        End Function
        Public Function GetRepairReturnForVoucher(ByVal ReturnID As String) As DataTable Implements IRepairReturnController.GetRepairReturnForVoucher
            Return _objRepairReturnDA.GetRepairReturnForVoucher(ReturnID)
        End Function
        Public Function GetRepairReturnSummary(FromDate As Date, ToDate As Date, Optional criStr As String = "") As DataTable Implements IRepairReturnController.GetRepairReturnSummary
            Return _objRepairReturnDA.GetRepairReturnSummary(FromDate, ToDate, criStr)
        End Function
        Public Function GetRepairReturnInvoiceDetailForTotal(FromDate As Date, ToDate As Date, Optional criStr As String = "") As RepairReturnDetailInfo Implements IRepairReturnController.GetRepairReturnInvoiceDetailForTotal
            Return _objRepairReturnDA.GetRepairReturnInvoiceDetailForTotal(FromDate, ToDate, criStr)
        End Function

        Public Function GetRepairReturnStockReportForTotalByDetail(FromDate As Date, ToDate As Date, Optional criStr As String = "") As DataTable Implements IRepairReturnController.GetRepairReturnStockReportForTotalByDetail
            Return _objRepairReturnDA.GetRepairReturnStockReportForTotalByDetail(FromDate, ToDate, criStr)
        End Function
        Public Function GetRepairReturnDetailByRepairReturnDetailGem(ByVal ReturnRepairGemID As String) As DataTable Implements IRepairReturnController.GetRepairReturnDetailByRepairReturnDetailGem
            Return _objRepairReturnDA.GetRepairReturnDetailByRepairReturnDetailGem(ReturnRepairGemID)
        End Function
        Public Function GetReturnRepairDetail(FromDate As Date, ToDate As Date, Optional Cristr As String = "") As DataTable Implements IRepairReturnController.GetReturnRepairDetail
            Return _objRepairReturnDA.GetReturnRepairDetail(FromDate, ToDate, Cristr)
        End Function
    End Class
End Namespace


