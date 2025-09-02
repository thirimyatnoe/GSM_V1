Imports DataAccess.MortgagePayback
Imports DataAccess.MortgageInvoice
Imports CommonInfo
Namespace MortgagePayback
    Public Class MortgagePaybackController
        Implements IMortgagePaybackController

#Region "Private Members"

        Private _objMortgagePaybackDA As IMortgagePaybackDA
        Private _objMortgageInvoiceDA As IMortgageInvoiceDA

        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IMortgagePaybackController = New MortgagePaybackController

#End Region

#Region "Constructors"

        Private Sub New()
            _objMortgagePaybackDA = DataAccess.Factory.Instance.CreateMortgagePaybackDA
            _objMortgageInvoiceDA = DataAccess.Factory.Instance.CreateMortgageInvoiceDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgagePaybackController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteMortgagePayback(ByVal MortgageInvoiceID As String, ByVal MortgagePaybackID As String, ByVal _dtMortgageInvoice As DataTable) As Boolean Implements IMortgagePaybackController.DeleteMortgagePayback
            Dim Bool As Boolean

            If _objMortgagePaybackDA.DeleteMortgagePayback(MortgagePaybackID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                       DateTime.Now, _
                                                       Global_UserID, _
                                                       CommonInfo.EnumSetting.GenerateKeyType.MortgagePayback.ToString, _
                                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                                       MortgageInvoiceID, _
                                                       "Delete Mortgage Payback")
                Bool = True
                If Bool = True Then
                    UpdateMortgageInvoiceByPayback(MortgageInvoiceID, False, MortgagePaybackID)

                    For Each dr As DataRow In _dtMortgageInvoice.Rows
                        UpdateMortgageInvoiceItemPayback(dr.Item("MortgageItemID"), False)
                    Next
                End If

                Return True
            Else
                Return False
            End If

        End Function
        Private Sub UpdateMortgageInvoiceByPayback(ByVal MortgageInvoiceID As String, ByVal argIsExit As Boolean, ByVal MortgagePaybackID As String)
            Dim objMortgage As New CommonInfo.MortgageInvoiceInfo
            With objMortgage
                .MortgageInvoiceID = MortgageInvoiceID
                .IsPayback = argIsExit
                .PaybackAmt = 0
                .PaybackInterestAmt = 0
            End With
            _objMortgagePaybackDA.UpdateMortgageInvoiceByPayback(objMortgage, MortgagePaybackID)
        End Sub


        Public Function GetMortgagePayback(ByVal MortgagePaybackID As String) As CommonInfo.MortgagePaybackInfo Implements IMortgagePaybackController.GetMortgagePayback
            Return _objMortgagePaybackDA.GetMortgagePayback(MortgagePaybackID)
        End Function

        Public Function SaveMortgagePayback(ByVal MortgagePaybackObj As CommonInfo.MortgagePaybackInfo, ByVal MortgageInvoicePaybackobj As CommonInfo.MortgageInvoiceInfo, ByVal dtMortgageInvocieItem As DataTable) As Boolean Implements IMortgagePaybackController.SaveMortgagePayback
            Dim objGeneralController As General.IGeneralController

            objGeneralController = Factory.Instance.CreateGeneralController
            Dim dtTest As New DataTable

            Dim bolRet As Boolean = False
            If MortgagePaybackObj.MortgagePaybackID = "0" Then
                MortgagePaybackObj.MortgagePaybackID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.MortgagePayback, "MortgagePaybackID", Now)
                bolRet = _objMortgagePaybackDA.InsertMortgagePayback(MortgagePaybackObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.MortgagePayback.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       MortgagePaybackObj.MortgagePaybackID, _
                                       "Insert Mortgage Payback")
            Else
                bolRet = _objMortgagePaybackDA.UpdateMortgagePayback(MortgagePaybackObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.MortgagePayback.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                        MortgagePaybackObj.MortgagePaybackID, _
                                       "Update Mortgage Payback")

                Dim tmpdt As New DataTable
                tmpdt = _objMortgagePaybackDA.GetMortgagePaybackItem(MortgagePaybackObj.MortgagePaybackID)
                If tmpdt.Rows.Count > 0 Then

                    _objMortgageInvoiceDA.UpdateMortgagePaybackHeader(MortgageInvoicePaybackobj)

                End If

            End If

            If bolRet = True Then

                dtTest = _objMortgagePaybackDA.GetMortgagePaybackItem(MortgagePaybackObj.MortgagePaybackID)
                For Each dr As DataRow In dtMortgageInvocieItem.Rows
                    Dim objPayback As New MortgagePaybackItemInfo

                    If dr.RowState = DataRowState.Added Then
                        With objPayback
                            .MortgagePaybackItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.MortgagePaybackItem, "MortgagePaybackItemID", Now)
                            .MortgagePaybackID = MortgagePaybackObj.MortgagePaybackID
                            .MortgageItemID = dr.Item("MortgageItemID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemName = dr.Item("ItemName_")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .Amount = dr.Item("PaybackAmount")
                            .MortgageRate = dr.Item("MortgageRate")
                            .IsDone = dr.Item("IsDone")
                            .DonePercent = dr.Item("DonePercent")
                            .NetAmount = dr.Item("Amount")
                        End With
                        bolRet = _objMortgagePaybackDA.InsertMortgagePaybackItem(objPayback)
                        UpdateMortgageInvoiceItemPayback(dr.Item("MortgageItemID"), True)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        If dtTest.Rows.Count <= 0 Then
                            With objPayback
                                .MortgagePaybackItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.MortgagePaybackItem, "MortgagePaybackItemID", Now)
                                .MortgagePaybackID = MortgagePaybackObj.MortgagePaybackID
                                .MortgageItemID = dr.Item("MortgageItemID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .ItemCategoryID = dr.Item("ItemCategoryID")
                                .ItemName = dr.Item("ItemName_")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")
                                .Amount = dr.Item("PaybackAmount")
                                .MortgageRate = dr.Item("MortgageRate")
                                .IsDone = dr.Item("IsDone")
                                .DonePercent = dr.Item("DonePercent")
                                .NetAmount = dr.Item("Amount")

                            End With
                        Else
                            With objPayback
                                .MortgagePaybackItemID = dr.Item("MortgagePaybackItemID")
                                .MortgagePaybackID = MortgagePaybackObj.MortgagePaybackID
                                .MortgageItemID = dr.Item("MortgageItemID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .ItemCategoryID = dr.Item("ItemCategoryID")
                                .ItemName = dr.Item("ItemName_")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")
                                .Amount = dr.Item("PaybackAmount")
                                .MortgageRate = dr.Item("MortgageRate")
                                .IsDone = dr.Item("IsDone")
                                .DonePercent = dr.Item("DonePercent")
                                .NetAmount = dr.Item("Amount")

                            End With
                        End If
                        If dtTest.Rows.Count > 0 Then
                            bolRet = _objMortgagePaybackDA.UpdateMortgagePaybackItem(objPayback)
                        Else
                            bolRet = _objMortgagePaybackDA.InsertMortgagePaybackItem(objPayback)
                        End If
                        UpdateMortgageInvoiceItemPayback(dr.Item("MortgageItemID"), True)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        UpdateMortgageInvoiceItemPayback(CStr(dr.Item("MortgageItemID", DataRowVersion.Original)), False)
                        bolRet = _objMortgagePaybackDA.DeleteMortgagePaybackItem(CStr(dr.Item("MortgageItemID", DataRowVersion.Original)))

                    ElseIf dr.RowState = DataRowState.Unchanged Then

                        If dtTest.Rows.Count <= 0 Then
                            With objPayback
                                .MortgagePaybackItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.MortgagePaybackItem, "MortgagePaybackItemID", Now)
                                .MortgagePaybackID = MortgagePaybackObj.MortgagePaybackID
                                .MortgageItemID = dr.Item("MortgageItemID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .ItemCategoryID = dr.Item("ItemCategoryID")
                                .ItemName = dr.Item("ItemName_")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")
                                .Amount = dr.Item("PaybackAmount")
                                .MortgageRate = dr.Item("MortgageRate")
                                .IsDone = dr.Item("IsDone")
                                .DonePercent = dr.Item("DonePercent")
                                .NetAmount = dr.Item("Amount")

                            End With

                        Else
                            With objPayback
                                .MortgagePaybackItemID = dr.Item("MortgagePaybackItemID")
                                .MortgagePaybackID = MortgagePaybackObj.MortgagePaybackID
                                .MortgageItemID = dr.Item("MortgageItemID")
                                .GoldQualityID = dr.Item("GoldQualityID")
                                .ItemCategoryID = dr.Item("ItemCategoryID")
                                .ItemName = dr.Item("ItemName_")
                                .GoldTK = dr.Item("GoldTK")
                                .GoldTG = dr.Item("GoldTG")
                                .Amount = dr.Item("PaybackAmount")
                                .MortgageRate = dr.Item("MortgageRate")
                                .IsDone = dr.Item("IsDone")
                                .DonePercent = dr.Item("DonePercent")
                                .NetAmount = dr.Item("Amount")

                            End With
                        End If
                        If dtTest.Rows.Count > 0 Then
                            bolRet = _objMortgagePaybackDA.UpdateMortgagePaybackItem(objPayback)
                        Else
                            bolRet = _objMortgagePaybackDA.InsertMortgagePaybackItem(objPayback)
                        End If
                        UpdateMortgageInvoiceItemPayback(CStr(dr.Item("MortgageItemID", DataRowVersion.Original)), True)
                    End If
                Next

                _objMortgageInvoiceDA.UpdateMortgagePaybackHeader(MortgageInvoicePaybackobj)
            End If

            Return bolRet


        End Function
        'Private Sub UpdateMortgagePaybackDeleteItemByIsPayback(ByVal argMortgageItemID As String, ByVal argIsPayback As Boolean)
        '    Dim objPayback As New CommonInfo.MortgageInvoiceItemInfo

        '    With objPayback
        '        .IsPayback = argIsPayback
        '        .MortgageItemID = argMortgageItemID
        '    End With
        '    _objMortgageInvoiceDA.UpdateMortgagePaybackDeleteItemByIsPayback(objPayback)
        'End Sub

        Private Sub UpdateMortgageInvoiceItemPayback(ByVal MortgageItemID As String, ByVal argIsPayback As Boolean)
            Dim objMortgageItem As New CommonInfo.MortgageInvoiceItemInfo

            With objMortgageItem
                .IsPayback = argIsPayback
                .MortgageItemID = MortgageItemID
            End With
            _objMortgageInvoiceDA.UpdateMortgageInvoiceItemPayback(objMortgageItem)
        End Sub

        Public Function GetMortgagePaybackDataTable(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackController.GetMortgagePaybackDataTable
            Return _objMortgagePaybackDA.GetMortgagePaybackDataTable(MortgageInvoiceID)
        End Function

        Public Function GetAllMortgagePaybackList() As System.Data.DataTable Implements IMortgagePaybackController.GetAllMortgagePaybackList
            Return _objMortgagePaybackDA.GetAllMortgagePaybackList
        End Function

        Public Function GetAllMortgagePaybackFromSearchBox() As System.Data.DataTable Implements IMortgagePaybackController.GetAllMortgagePaybackFromSearchBox
            Return _objMortgagePaybackDA.GetAllMortgagePaybackFromSearchBox()
        End Function
        Public Function GetMortgagePaybackPrint(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackController.GetMortgagePaybackPrint
            Return _objMortgagePaybackDA.GetMortgagePaybackPrint(MortgageInvoiceID)
        End Function
        Public Function GetMortgagePaybackItemPrint(ByVal MortgagePaybackID As String) As System.Data.DataTable Implements IMortgagePaybackController.GetMortgagePaybackItemPrint
            Return _objMortgagePaybackDA.GetMortgagePaybackItemPrint(MortgagePaybackID)
        End Function
        Public Function GetMortgagePaybackFromInterest(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackController.GetMortgagePaybackFromInterest
            Return _objMortgagePaybackDA.GetMortgagePaybackFromInterest(MortgageInvoiceID)
        End Function

        Public Function GetMortgagePaybackDate(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgagePaybackController.GetMortgagePaybackDate
            Return _objMortgagePaybackDA.GetMortgagePaybackDate(MortgageInvoiceID)
        End Function
        Public Function GetAllMortgagePayback() As System.Data.DataTable Implements IMortgagePaybackController.GetAllMortgagePayback
            Return _objMortgagePaybackDA.GetAllMortgagePayback()
        End Function
        Public Function GetMortgagePaybackByID(ByVal MortgagePaybackID As String) As CommonInfo.MortgagePaybackInfo Implements IMortgagePaybackController.GetMortgagePaybackByID
            Return _objMortgagePaybackDA.GetMortgagePaybackByID(MortgagePaybackID)
        End Function
        Public Function GetMortgagePaybackItem(ByVal MortgagePaybackID As String) As System.Data.DataTable Implements IMortgagePaybackController.GetMortgagePaybackItem
            Return _objMortgagePaybackDA.GetMortgagePaybackItem(MortgagePaybackID)
        End Function
        Public Function GetMortgagePaybackByDate(ByVal MortgageInvoiceID As String, ByVal TDate As Date) As System.Data.DataTable Implements IMortgagePaybackController.GetMortgagePaybackByDate
            Return _objMortgagePaybackDA.GetMortgagePaybackByDate(MortgageInvoiceID, TDate)
        End Function
    End Class
End Namespace

