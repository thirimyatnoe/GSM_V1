Imports DataAccess.MortgageInvoice
Imports CommonInfo
Namespace MortgageInvoice
    Public Class MortgageInvoiceController
        Implements IMortgageInvoiceController

#Region "Private Members"
        Private _objMortgageInvoiceDA As IMortgageInvoiceDA
       
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IMortgageInvoiceController = New MortgageInvoiceController
#End Region

#Region "Constructors"

        Private Sub New()
            _objMortgageInvoiceDA = DataAccess.Factory.Instance.CreateMortgageInvoiceDA
           
        End Sub
#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IMortgageInvoiceController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteMortgageInvoiceHeader(ByVal MortgageInvoiceID As String) As Boolean Implements IMortgageInvoiceController.DeleteMortgageInvoiceHeader
            If _objMortgageInvoiceDA.DeleteMortgageInvoiceHeader(MortgageInvoiceID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.MortgageInvoice.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       MortgageInvoiceID, _
                                       "Delete Mortgage Invoice")

                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllMortgageInvoice() As System.Data.DataTable Implements IMortgageInvoiceController.GetAllMortgageInvoice
            Return _objMortgageInvoiceDA.GetAllMortgageInvoice()
        End Function

        Public Function GetMortgageInvoice(ByVal MortgageInvoiceID As String) As CommonInfo.MortgageInvoiceInfo Implements IMortgageInvoiceController.GetMortgageInvoice
            Return _objMortgageInvoiceDA.GetMortgageInvoice(MortgageInvoiceID)
        End Function
        Public Function GetMortgageInvoiceForPaybackUpdate(ByVal MortgageInvoiceID As String, ByVal MortgagePaybackID As String) As CommonInfo.MortgageInvoiceInfo Implements IMortgageInvoiceController.GetMortgageInvoiceForPaybackUpdate
            Return _objMortgageInvoiceDA.GetMortgageInvoiceForPaybackUpdate(MortgageInvoiceID, MortgagePaybackID)
        End Function
        Public Function SaveMortgageInvoice(ByVal MortgageInvoiceObj As CommonInfo.MortgageInvoiceInfo, ByVal _dtMortgageInvoiceItem As System.Data.DataTable) As Boolean Implements IMortgageInvoiceController.SaveMortgageInvoice
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController

            Dim bolRet As Boolean = False
            If MortgageInvoiceObj.MortgageInvoiceID = "" Then

                'MortgageInvoiceObj.MortgageInvoiceID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.MortgageInvoice, "MortgageInvoiceID", MortgageInvoiceObj.ReceiveDate)
                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.MortgageReceive.ToString)
                MortgageInvoiceObj.MortgageInvoiceID = objGeneralController.GenerateKeyForFormat(objGenerateFormat, MortgageInvoiceObj.ReceiveDate)

                objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.GenerateKeyType.MortgageInvoiceCode.ToString)
                MortgageInvoiceObj.MortgageInvoiceCode = objGeneralController.GenerateKeyForFormat(objGenerateFormat, MortgageInvoiceObj.ReceiveDate)
                ' MortgageInvoiceObj.MortgageInvoiceCode = Format(MortgageInvoiceObj.ReceiveDate, "yyyy") + objGeneralController.GetGenerateKey(EnumSetting.GenerateKeyType.MortgageInvoiceCode, Format(MortgageInvoiceObj.ReceiveDate, "yyyy"), Now) 'objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.MortgageInvoiceCode, "MortgageInvoiceCode", MortgageInvoiceObj.ReceiveDate)
                bolRet = _objMortgageInvoiceDA.InsertMortgageInvoiceHeader(MortgageInvoiceObj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                      DateTime.Now, _
                                      Global_UserID, _
                                      CommonInfo.EnumSetting.GenerateKeyType.MortgageInvoice.ToString, _
                                      CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                      MortgageInvoiceObj.MortgageInvoiceID, _
                                      "Insert Mortgage Invoice")
            Else
                If (MortgageInvoiceObj.IsReturn = False) Then
                    bolRet = _objMortgageInvoiceDA.UpdateMortgageInvoiceHeader(MortgageInvoiceObj)
                Else
                    bolRet = _objMortgageInvoiceDA.UpdateMortgageReturnHeader(MortgageInvoiceObj)
                End If
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                      DateTime.Now, _
                                      Global_UserID, _
                                      CommonInfo.EnumSetting.GenerateKeyType.MortgageInvoice.ToString, _
                                      CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                      MortgageInvoiceObj.MortgageInvoiceID, _
                                      "Update Mortgage Invoice")
            End If
            If bolRet Then
               
                For Each dr As DataRow In _dtMortgageInvoiceItem.Rows
                    Dim objMortgageInvoiceItem As New MortgageInvoiceItemInfo
                    If dr.RowState = DataRowState.Added Then
                        With objMortgageInvoiceItem
                            .MortgageItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.MortgageInvoiceItem, "MortgageInvoiceItemID", MortgageInvoiceObj.ReceiveDate)
                            .MortgageInvoiceID = MortgageInvoiceObj.MortgageInvoiceID
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .MortgageRate = dr.Item("MortgageRate")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemName = dr.Item("ItemName")
                            .ItemNameID = dr.Item("ItemNameID")
                            .QTY = dr.Item("QTY")
                            .GoldK = dr.Item("GoldK")
                            .GoldP = dr.Item("GoldP")
                            .GoldY = dr.Item("GoldY")
                            '.GoldC = dr.Item("GoldC")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .Amount = dr.Item("Amount")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .IsDone = dr.Item("IsDone")
                            '.SaleRate = dr.Item("SaleRate")
                            .DonePercent = dr.Item("DonePercent")
                            .MortgageItemCode = dr.Item("MortgageItemCode")
                        End With
                        bolRet = _objMortgageInvoiceDA.InsertMortgageInvoiceItem(objMortgageInvoiceItem)
                    ElseIf dr.RowState = DataRowState.Modified Then
                        With objMortgageInvoiceItem
                            .MortgageInvoiceID = MortgageInvoiceObj.MortgageInvoiceID
                            .MortgageItemID = dr.Item("MortgageItemID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .MortgageRate = dr.Item("MortgageRate")
                            .ItemCategoryID = dr.Item("ItemCategoryID")
                            .ItemName = dr.Item("ItemName")
                            .ItemNameID = dr.Item("ItemNameID")
                            .QTY = dr.Item("QTY")
                            .GoldK = dr.Item("GoldK")
                            .GoldP = dr.Item("GoldP")
                            .GoldY = dr.Item("GoldY")
                            '.GoldC = dr.Item("GoldC")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .Amount = dr.Item("Amount")
                            .GoldTK = dr.Item("GoldTK")
                            .GoldTG = dr.Item("GoldTG")
                            .IsDone = dr.Item("IsDone")
                            '.SaleRate = dr.Item("SaleRate")
                            .DonePercent = dr.Item("DonePercent")
                            .MortgageItemCode = dr.Item("MortgageItemCode")
                        End With
                        bolRet = _objMortgageInvoiceDA.UpdateMortgageInvoiceItem(objMortgageInvoiceItem)
                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objMortgageInvoiceDA.DeleteMortgageInvoiceItem(CStr(dr.Item("MortgageItemID", DataRowVersion.Original)))
                    End If
                Next
            End If

            Return bolRet
        End Function

        Public Function GetMortgageInvoiceItem(ByVal _MortgageInvoiceItemID As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoiceItem
            Return _objMortgageInvoiceDA.GetMortgageInvoiceItem(_MortgageInvoiceItemID)
        End Function
        Public Function GetMortgageInvoiceReceiveItem(ByVal _MortgageInvoiceItemID As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoiceReceiveItem
            Return _objMortgageInvoiceDA.GetMortgageInvoiceReceiveItem(_MortgageInvoiceItemID)
        End Function
        Public Function GetAllMortgageReturnByMortgageInvoice(ByVal _MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetAllMortgageReturnByMortgageInvoice
            Return _objMortgageInvoiceDA.GetAllMortgageReturnByMortgageInvoice(_MortgageInvoiceID)
        End Function

        Public Function GetMortgageReturnFromInterest(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageReturnFromInterest
            Return _objMortgageInvoiceDA.GetMortgageReturnFromInterest(MortgageInvoiceID)
        End Function

        Public Function GetMortgageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageReport
            Return _objMortgageInvoiceDA.GetMortgageReport(FromDate, ToDate, cristr)
        End Function
        Public Function GetAllMortgageReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetAllMortgageReport
            Return _objMortgageInvoiceDA.GetAllMortgageReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetMortgagePaybackReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgagePaybackReport
            Return _objMortgageInvoiceDA.GetMortgagePaybackReport(FromDate, ToDate, cristr)
        End Function
        Public Function GetMortgagePaybackTotalReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgagePaybackTotalReport
            Return _objMortgageInvoiceDA.GetMortgagePaybackTotalReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetMortgageReturnReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageReturnReport
            Return _objMortgageInvoiceDA.GetMortgageReturnReport(FromDate, ToDate, cristr)
        End Function
        Public Function GetMortgageReturnReportNew(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageReturnReportNew
            Return _objMortgageInvoiceDA.GetMortgageReturnReportNew(FromDate, ToDate, cristr)
        End Function
        Public Function GetMortgageReturnReportSum(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageReturnReportSum
            Return _objMortgageInvoiceDA.GetMortgageReturnReportSum(FromDate, ToDate, cristr)
        End Function
        Public Function GetMortgageReturnReportSumNew(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageReturnReportSumNew
            Return _objMortgageInvoiceDA.GetMortgageReturnReportSumNew(FromDate, ToDate, cristr)
        End Function
        Public Function GetMortgageCustomerHistoryReport(Optional ByVal cristr As String = "") As System.Data.DataSet Implements IMortgageInvoiceController.GetMortgageCustomerHistoryReport
            Return _objMortgageInvoiceDA.GetMortgageCustomerHistoryReport(cristr)
        End Function
        Public Function GetMortgageInterestReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataSet Implements IMortgageInvoiceController.GetMortgageInterestReport
            Return _objMortgageInvoiceDA.GetMortgageInterestReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetMortgageDisable(ByVal InterestPeriod As Integer) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageDisable
            Return _objMortgageInvoiceDA.GetMortgageDisable(InterestPeriod)
        End Function

        Public Function SaveMortgageDisable(ByVal DisableDate As Date, ByVal _dtMortgageDisable As System.Data.DataTable) As Boolean Implements IMortgageInvoiceController.SaveMortgageDisable

            Dim bolRet As Boolean = False

            For Each dr As DataRow In _dtMortgageDisable.Rows
                Dim objMortgageInvoice As New MortgageInvoiceInfo
                If dr.RowState = DataRowState.Modified Then
                    With objMortgageInvoice
                        .MortgageInvoiceID = dr.Item("MortgageInvoiceID")
                        .IsDisable = IIf(dr.Item("IsDisable"), 1, 0)
                        .DisableDate = DisableDate.Date
                    End With
                    bolRet = _objMortgageInvoiceDA.UpdateMortgageDisable(objMortgageInvoice)
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                         DateTime.Now, _
                                                         Global_UserID, _
                                                         CommonInfo.EnumSetting.GenerateKeyType.MortgageInvoice.ToString, _
                                                         CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                         objMortgageInvoice.MortgageInvoiceID, _
                                                         "Mortgage Disable")
                End If
            Next

            Return bolRet
        End Function

        Public Function GetMortgageDisableSummaryByGoldQualityAndItemCategory(ByVal ForDate As Date, ByVal GoldQualityID As String, ByVal ItemCategoryID As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageDisableSummaryByGoldQualityAndItemCategory
            Return _objMortgageInvoiceDA.GetMortgageDisableSummaryByGoldQualityAndItemCategory(ForDate, GoldQualityID, ItemCategoryID)
        End Function

        Public Function GetAllMortgageInvoiceFromSearchBox() As System.Data.DataTable Implements IMortgageInvoiceController.GetAllMortgageInvoiceFromSearchBox
            Return _objMortgageInvoiceDA.GetAllMortgageInvoiceFromSearchBox()
        End Function

        Public Function GetMortgageInvoiceItemFromSearchBox(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoiceItemFromSearchBox
            Return _objMortgageInvoiceDA.GetMortgageInvoiceItemFromSearchBox(MortgageInvoiceID)
        End Function

        Public Function GetMortgageInvoicePrint(ByVal MortgageInvoiceID As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoicePrint
            Return _objMortgageInvoiceDA.GetMortgageInvoicePrint(MortgageInvoiceID)
        End Function

        Public Function GetMortgageInvoiceByMortgageCode(ByVal MortgageInvoiceCode As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoiceByMortgageCode
            Return _objMortgageInvoiceDA.GetMortgageInvoiceByMortgageCode(MortgageInvoiceCode)
        End Function

        Public Function GetMortgageInvoiceDisableReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoiceDisableReport
            Return _objMortgageInvoiceDA.GetMortgageInvoiceDisableReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetMortgageInvoiceExcludeInMortgageItems() As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoiceExcludeInMortgageItems
            Return _objMortgageInvoiceDA.GetMortgageInvoiceExcludeInMortgageItems()
        End Function

        Public Function GetAllMortgageInvoiceByIsRepayHeadOffice() As System.Data.DataTable Implements IMortgageInvoiceController.GetAllMortgageInvoiceByIsRepayHeadOffice
            Return _objMortgageInvoiceDA.GetAllMortgageInvoiceByIsRepayHeadOffice()
        End Function
        Public Function GetAllMortgageInvoiceByReturn() As System.Data.DataTable Implements IMortgageInvoiceController.GetAllMortgageInvoiceByReturn
            Return _objMortgageInvoiceDA.GetAllMortgageInvoiceByReturn()
        End Function
        Public Function GetMortgageInvoiceByMortgageCodeOnIsRepayHO(ByVal MortgageInvoiceCode As String) As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageInvoiceByMortgageCodeOnIsRepayHO
            Return _objMortgageInvoiceDA.GetMortgageInvoiceByMortgageCodeOnIsRepayHO(MortgageInvoiceCode)
        End Function
        Public Function GetMortgageDataByMortgageInvoiceID(ByVal argstr As String, Optional ByVal MortgageInvoiceID As String = "") As System.Data.DataTable Implements IMortgageInvoiceController.GetMortgageDataByMortgageInvoiceID
            Return _objMortgageInvoiceDA.GetMortgageDataByMortgageInvoiceID(argstr, MortgageInvoiceID)
        End Function

 

    End Class
End Namespace
