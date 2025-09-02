Imports DataAccess.MortgageReturn
Imports DataAccess.MortgageInvoice
Imports CommonInfo
Namespace MortgageReturn
    Public Class MortgageReturnController
        Implements IMortgageReturnController

#Region "Private Members"

        Private _objMortgageReturnDA As IMortgageReturnDA
        Private _objMortgageInvoiceDA As IMortgageInvoiceDA

        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IMortgageReturnController = New MortgageReturnController

#End Region

#Region "Constructors"

        Private Sub New()
            _objMortgageReturnDA = DataAccess.Factory.Instance.CreateMortgageReturnDA
            _objMortgageInvoiceDA = DataAccess.Factory.Instance.CreateMortgageInvoiceDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgageReturnController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteMortgageReturn(ByVal MortgageReturnID As String) As Boolean Implements IMortgageReturnController.DeleteMortgageReturn
            Dim Bool As Boolean

            If _objMortgageReturnDA.DeleteMortgageReturn(MortgageReturnID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.MortgageReturn.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       MortgageReturnID, _
                                                       "Delete Mortgage Return")
                'Bool = True
                'If Bool = True Then
                '    UpdateMortgageInvoiceByPayback(MortgageInvoiceID, False, MortgageReturnID)

                '    For Each dr As DataRow In _dtMortgageInvoice.Rows
                '        UpdateMortgageInvoiceItemPayback(dr.Item("MortgageItemID"), False)
                '    Next
                'End If

                Return True
            Else
                Return False
            End If

        End Function
        Private Sub UpdateMortgageInvoiceByPayback(ByVal MortgageInvoiceID As String, ByVal argIsExit As Boolean, ByVal MortgageReturnID As String)
            Dim objMortgage As New CommonInfo.MortgageInvoiceInfo
            With objMortgage
                .MortgageInvoiceID = MortgageInvoiceID
                .IsPayback = argIsExit
                .PaybackAmt = 0
                .PaybackInterestAmt = 0
            End With
            _objMortgageReturnDA.UpdateMortgageInvoiceByPayback(objMortgage, MortgageReturnID)
        End Sub
        Public Function GetMortgageReturn(ByVal MortgageReturnID As String) As CommonInfo.MortgageReturnInfo Implements IMortgageReturnController.GetMortgageReturn
            Return _objMortgageReturnDA.GetMortgageReturn(MortgageReturnID)
        End Function

        Public Function SaveMortgageReturn(ByVal MortgageReturnObj As CommonInfo.MortgageReturnInfo, ByVal dtMortgageInvocieItem As DataTable) As Boolean Implements IMortgageReturnController.SaveMortgageReturn
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim ConsignmentSaleID As String
            Dim dtTest As New DataTable
            Dim bolRet As Boolean = False

            If MortgageReturnObj.MortgageReturnID = "0" Then
                MortgageReturnObj.MortgageReturnID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.MortgageReturn, "MortgageReturn", Now)

                
                bolRet = _objMortgageReturnDA.InsertMortgageReturn(MortgageReturnObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.MortgageReturn.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       MortgageReturnObj.MortgageReturnID, _
                                       "Insert Mortgage Return")


            Else
                bolRet = _objMortgageReturnDA.UpdateMortgageReturn(MortgageReturnObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.MortgageReturn.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       MortgageReturnObj.MortgageReturnID, _
                                       "Update Mortgage Return")

                'Dim tmpdt As New DataTable
                'tmpdt = _objMortgageReturnDA.GetMortgageReturnItemByID(MortgageReturnObj.MortgageReturnID)
                'If tmpdt.Rows.Count > 0 Then
                '    For Each tmpdr As DataRow In tmpdt.Rows
                '        ' If (ConsignmentSaleID = "-") Then

                '        UpdateWholeSalesReturnItemByIsExit(tmpdr.Item("ForSaleID"), False, obj.WReturnDate)

                '        ' End If

                '    Next
                'End If

            End If
            If bolRet = True Then
                dtTest = _objMortgageReturnDA.GetMortgageReturnItem(MortgageReturnObj.MortgageReturnID)
                For Each dr As DataRow In dtMortgageInvocieItem.Rows
                    Dim objMortgageReturnItem As New MortgageReturnItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objMortgageReturnItem
                            .MortgageReturnItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.MortgageReturnItem, "MortgageReturnItem", MortgageReturnObj.ReturnDate)
                            .MortgageReturnID = MortgageReturnObj.MortgageReturnID
                            .MortgageItemID = dr.Item("MortgageItemID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemName = dr.Item("ItemName_")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .Amount = dr.Item("Amount")
                            .IsDone = dr.Item("IsDone")
                            .DonePercent = dr.Item("DonePercent")
                        End With
                        bolRet = _objMortgageReturnDA.InsertMortgageReturnItem(objMortgageReturnItem)
                        'UpdateWholeSalesReturnItemByIsExit(objWSReturnItem.ForSaleID, False, MortgageReturnObj.WReturnDate)
                        'For Whole sale item is return 
                        'UpdateWholeSalesItem(objWSReturnItem.ForSaleID, True, objWSReturnItem.WholesaleReturnItemID)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objMortgageReturnItem
                            '.WholesaleReturnItemID = dr.Item("WholeSaleReturnItemID")
                            .MortgageReturnItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.WholeSaleReturnItem, "MortgageReturnItem", MortgageReturnObj.ReturnDate)
                            .MortgageReturnID = MortgageReturnObj.MortgageReturnID
                            .MortgageItemID = dr.Item("MortgageItemID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemName = dr.Item("ItemName_")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .Amount = dr.Item("Amount")
                            .IsDone = dr.Item("IsDone")
                            .DonePercent = dr.Item("DonePercent")
                        End With
                        bolRet = _objMortgageReturnDA.UpdateMortgageReturnItem(objMortgageReturnItem)
                        'bolRet = _objWholeSaleReturnDA.InsertWholeSaleReturnItem(objWSReturnItem)
                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objMortgageReturnDA.DeleteMortgageReturnItem(CStr(dr.Item("MortgageReturnItemID", DataRowVersion.Original)))



                        'ElseIf dr.RowState = DataRowState.Unchanged Then
                        '    UpdateWholeSalesReturnItemByIsExit(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), False, MortgageReturnObj.WReturnDate)
                        '    UpdateWholeSalesItem(CStr(dr.Item("ForSaleID", DataRowVersion.Original)), True, CStr(dr.Item("WSReturnItemID", DataRowVersion.Original)))
                    End If
                Next
            End If
            Return bolRet


        End Function
        'Private Sub UpdateMortgageReturnDeleteItemByIsPayback(ByVal argMortgageItemID As String, ByVal argIsPayback As Boolean)
        '    Dim objPayback As New CommonInfo.MortgageInvoiceItemInfo

        '    With objPayback
        '        .IsPayback = argIsPayback
        '        .MortgageItemID = argMortgageItemID
        '    End With
        '    _objMortgageInvoiceDA.UpdateMortgageReturnDeleteItemByIsPayback(objPayback)
        'End Sub

        Private Sub UpdateMortgageInvoiceItemPayback(ByVal MortgageItemID As String, ByVal argIsPayback As Boolean)
            Dim objMortgageItem As New CommonInfo.MortgageInvoiceItemInfo

            With objMortgageItem
                .IsPayback = argIsPayback
                .MortgageItemID = MortgageItemID
            End With
            _objMortgageInvoiceDA.UpdateMortgageInvoiceItemPayback(objMortgageItem)
        End Sub

        Public Function GetMortgageReturnDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageReturnController.GetMortgageReturnDataTable
            Return _objMortgageReturnDA.GetMortgageReturnDataTable(MortgageInvoiceID)
        End Function

        Public Function GetAllMortgageReturnList() As System.Data.DataTable Implements IMortgageReturnController.GetAllMortgageReturnList
            Return _objMortgageReturnDA.GetAllMortgageReturnList
        End Function

        Public Function GetAllMortgageReturnFromSearchBox() As System.Data.DataTable Implements IMortgageReturnController.GetAllMortgageReturnFromSearchBox
            Return _objMortgageReturnDA.GetAllMortgageReturnFromSearchBox()
        End Function
        Public Function GetMortgageReturnPrint(ByVal MortgageRetrunID As String) As System.Data.DataTable Implements IMortgageReturnController.GetMortgageReturnPrint
            Return _objMortgageReturnDA.GetMortgageReturnPrint(MortgageRetrunID)
        End Function
        Public Function GetMortgageReturnItemPrint(ByVal MortgageReturnID As String) As System.Data.DataTable Implements IMortgageReturnController.GetMortgageReturnItemPrint
            Return _objMortgageReturnDA.GetMortgageReturnItemPrint(MortgageReturnID)
        End Function
        Public Function GetMortgageReturnFromInterest(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageReturnController.GetMortgageReturnFromInterest
            Return _objMortgageReturnDA.GetMortgageReturnFromInterest(MortgageInvoiceID)
        End Function

        Public Function GetMortgageReturnDate(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageReturnController.GetMortgageReturnDate
            Return _objMortgageReturnDA.GetMortgageReturnDate(MortgageInvoiceID)
        End Function
        Public Function GetAllMortgageReturn() As System.Data.DataTable Implements IMortgageReturnController.GetAllMortgageReturn
            Return _objMortgageReturnDA.GetAllMortgageReturn()
        End Function
        Public Function GetMortgageReturnByID(ByVal MortgageReturnID As String) As CommonInfo.MortgageReturnInfo Implements IMortgageReturnController.GetMortgageReturnByID
            Return _objMortgageReturnDA.GetMortgageReturnByID(MortgageReturnID)
        End Function
        Public Function GetMortgageReturnItem(ByVal MortgageReturnID As String) As System.Data.DataTable Implements IMortgageReturnController.GetMortgageReturnItem
            Return _objMortgageReturnDA.GetMortgageReturnItem(MortgageReturnID)
        End Function
        Public Function GetMortgageReturnByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As System.Data.DataTable Implements IMortgageReturnController.GetMortgageReturnByDate
            Return _objMortgageReturnDA.GetMortgageReturnByDate(MortgageInvoiceID, TDate)
        End Function
    End Class
End Namespace

