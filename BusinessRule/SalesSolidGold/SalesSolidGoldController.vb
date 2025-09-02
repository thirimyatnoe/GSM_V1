Imports DataAccess.SalesSolidGold
Namespace SalesSolidGold
    Public Class SalesSolidGoldController
        Implements ISalesSolidGoldController


#Region "Private Members"

        Private _objSaleSolidGoldDA As ISalesSolidGoldDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISalesSolidGoldController = New SalesSolidGoldController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSaleSolidGoldDA = DataAccess.Factory.Instance.CreateSalesSolidGoldDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesSolidGoldController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteSalesSolidGold(ByVal SaleSolidGoldObj As CommonInfo.SalesSolidGoldInfo) As Boolean Implements ISalesSolidGoldController.DeleteSalesSolidGold
            If _objSaleSolidGoldDA.DeleteSalesSolidGold(SaleSolidGoldObj.SaleSolidGoldID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                              DateTime.Now, _
                                              Global_UserID, _
                                              CommonInfo.EnumSetting.GenerateKeyType.SalesSolidGold.ToString, _
                                              CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                              SaleSolidGoldObj.SaleSolidGoldID, _
                                              "Delete SaleSolidGold")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllSalesSolidGold() As System.Data.DataTable Implements ISalesSolidGoldController.GetAllSalesSolidGold
            Return _objSaleSolidGoldDA.GetAllSalesSolidGold()
        End Function

        Public Function GetSalesSolidGold(ByVal SaleSolidGoldID As String) As CommonInfo.SalesSolidGoldInfo Implements ISalesSolidGoldController.GetSalesSolidGold
            Return _objSaleSolidGoldDA.GetSalesSolidGold(SaleSolidGoldID)
        End Function

        Public Function GetSalesSolidGoldPrint(ByVal SaleSolidGoldID As String) As System.Data.DataTable Implements ISalesSolidGoldController.GetSalesSolidGoldPrint
            Return _objSaleSolidGoldDA.GetSalesSolidGoldPrint(SaleSolidGoldID)
        End Function

        Public Function SaveSalesSolidGold(ByVal obj As CommonInfo.SalesSolidGoldInfo, ByVal dtItem As DataTable) As Boolean Implements ISalesSolidGoldController.SaveSalesSolidGold
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False

            If obj.SaleSolidGoldID = "" Then
                obj.SaleSolidGoldID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.SalesSolidGold, CommonInfo.EnumSetting.GenerateKeyType.SalesSolidGold.ToString, obj.SaleDate)
                bolRet = _objSaleSolidGoldDA.InsertSalesSolidGold(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                              DateTime.Now, _
                                              Global_UserID, _
                                              CommonInfo.EnumSetting.GenerateKeyType.SalesSolidGold.ToString, _
                                              CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                              obj.SaleSolidGoldID, _
                                              "Insert SaleSolidGold")
            Else
                bolRet = _objSaleSolidGoldDA.UpdateSalesSolidGold(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                                              DateTime.Now, _
                                                              Global_UserID, _
                                                              CommonInfo.EnumSetting.GenerateKeyType.SalesSolidGold.ToString, _
                                                              CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                                              obj.SaleSolidGoldID, _
                                                              "Update SaleSolidGold")
            End If
            If bolRet Then
                For Each dr As DataRow In dtItem.Rows
                    Dim objItem As New CommonInfo.SalesSolidGoldItemInfo
                    If dr.RowState = DataRowState.Added Then
                        With objItem
                            .SaleSolidGoldID = obj.SaleSolidGoldID
                            .SaleSolidGoldItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.SalesSolidGoldItem, "SalesSolidGoldItem", obj.SaleDate)
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .GoldTK = IIf(IsDBNull(dr.Item("GoldTK")) = True, 0, dr.Item("GoldTK"))
                            .GoldTG = IIf(IsDBNull(dr.Item("GoldTG")) = True, 0, dr.Item("GoldTG"))
                            .SalesRate = dr.Item("SaleRate")
                            .Amount = dr.Item("Amount")
                        End With
                        bolRet = _objSaleSolidGoldDA.InsertSalesSolidGoldItem(objItem)

                    ElseIf dr.RowState = DataRowState.Modified Then

                        With objItem
                            .SaleSolidGoldID = obj.SaleSolidGoldID
                            .SaleSolidGoldItemID = dr.Item("SaleSolidGoldItemID")
                            .GoldQualityID = dr.Item("GoldQualityID")
                            .GoldTK = IIf(IsDBNull(dr.Item("GoldTK")) = True, 0, dr.Item("GoldTK"))
                            .GoldTG = IIf(IsDBNull(dr.Item("GoldTG")) = True, 0, dr.Item("GoldTG"))
                            .SalesRate = dr.Item("SaleRate")
                            .Amount = dr.Item("Amount")
                        End With
                        bolRet = _objSaleSolidGoldDA.UpdateSalesSolidGoldItem(objItem)

                    ElseIf dr.RowState = DataRowState.Deleted Then
                        bolRet = _objSaleSolidGoldDA.DeleteSalesSolidGoldItem(CStr(dr.Item("SaleSolidGoldItemID", DataRowVersion.Original)))
                    End If
                Next
            End If
            Return bolRet
        End Function
        Public Function GetLeftGoldWgtByForSaleID(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesSolidGoldController.GetLeftGoldWgtByForSaleID
            Return _objSaleSolidGoldDA.GetLeftGoldWgtByForSaleID(ForSaleID)
        End Function

        Public Function GetSalesSolidGoldReport(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesSolidGoldController.GetSalesSolidGoldReport
            Return _objSaleSolidGoldDA.GetSalesSolidGoldReport(FromDate, ToDate, GetFilterString, cristr)
        End Function

        Public Function GetTotalGoldWeightbyDate(ByVal FromDate As Date, ByVal ToDate As Date, ByVal GetFilterString As String) As System.Data.DataTable Implements ISalesSolidGoldController.GetTotalGoldWeightbyDate
            Return _objSaleSolidGoldDA.GetTotalGoldWeightbyDate(FromDate, ToDate, GetFilterString)
        End Function

        Public Function GetSalesSolidGoldItem(ByVal SaleSolidGoldID As String) As System.Data.DataTable Implements ISalesSolidGoldController.GetSalesSolidGoldItem
            Return _objSaleSolidGoldDA.GetSalesSolidGoldItem(SaleSolidGoldID)
        End Function
    End Class
End Namespace

