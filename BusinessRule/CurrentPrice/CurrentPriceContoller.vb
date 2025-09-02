Imports DataAccess.CurrentPrice
Namespace CurrentPrice
    Public Class CurrentPriceController
        Implements ICurrentPriceController


#Region "Private Members"

        Private _objCurrentPriceDA As ICurrentPriceDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ICurrentPriceController = New CurrentPriceController

#End Region

#Region "Constructors"

        Private Sub New()
            _objCurrentPriceDA = DataAccess.Factory.Instance.CreateCurrentPriceDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICurrentPriceController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function DeleteCurrentPrice(ByVal DefineID As String) As Boolean Implements ICurrentPriceController.DeleteCurrentPrice
            If _objCurrentPriceDA.DeleteCurrentPrice(DefineID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                DateTime.Now, _
                Global_UserID, _
                CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, _
                CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                DefineID, _
                "Delete CurrentPrice")
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetAllCurrentPrice() As System.Data.DataTable Implements ICurrentPriceController.GetAllCurrentPrice
            Return _objCurrentPriceDA.GetAllCurrentPrice()
        End Function
        Public Function GetCurrentPrice(ByVal DefineID As String) As CommonInfo.CurrentPriceInfo Implements ICurrentPriceController.GetCurrentPrice
            Return _objCurrentPriceDA.GetCurrentPrice(DefineID)
        End Function

        Public Function InsertCurrentPrice(ByVal CurrentPriceObj As CommonInfo.CurrentPriceInfo) As Boolean Implements ICurrentPriceController.InsertCurrentPrice
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            If CurrentPriceObj.DefineID = "0" Then
                CurrentPriceObj.DefineID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice, CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, CurrentPriceObj.DefineDateTime)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                DateTime.Now, _
                Global_UserID, _
                CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, _
                CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                CurrentPriceObj.DefineID, _
                "Insert CurrentPrice")
                Return _objCurrentPriceDA.InsertCurrentPrice(CurrentPriceObj)
            Else
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                DateTime.Now, _
                Global_UserID, _
                CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, _
                CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                CurrentPriceObj.DefineID, _
                "Update CurrentPrice")
                Return _objCurrentPriceDA.UpdateCurrentPrice(CurrentPriceObj)
            End If
        End Function

        Public Function GetCurrentPriceByGoldID(ByVal GoldQualityID As String) As CommonInfo.CurrentPriceInfo Implements ICurrentPriceController.GetCurrentPriceByGoldID
            Return _objCurrentPriceDA.GetCurrentPriceByGoldID(GoldQualityID)
        End Function
        Public Function GetAllCurrentPriceForPreview(Optional cristr As String = "") As System.Data.DataTable Implements ICurrentPriceController.GetAllCurrentPriceForPreview
            Return _objCurrentPriceDA.GetAllCurrentPriceForPreview(cristr)
        End Function

        Public Function SaveGoldPriceData(ByVal dtGoldPrice As DataTable, ByVal DefineDate As Date) As Boolean Implements ICurrentPriceController.SaveGoldPriceData
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim bolRet As Boolean = False

            For Each dr As DataRow In dtGoldPrice.Rows
                Dim CurrentPriceObj As New CommonInfo.CurrentPriceInfo

                With CurrentPriceObj
                    .DefineID = dr.Item("DefineID")
                    .DefineDateTime = DefineDate
                    .GoldQualityID = dr.Item("GoldQualityID")
                    .SalesRate = dr.Item("SalesRate")
                    If Not IsDBNull(dr.Item("PurchaseRate")) Then
                        If dr.Item("PurchaseRate") <> "" Then
                            .PurchaseRate = dr.Item("PurchaseRate")
                        Else
                            .PurchaseRate = 0
                        End If
                    Else
                        .PurchaseRate = 0
                    End If

                    If Not IsDBNull(dr.Item("ExchangeRate")) Then
                        If dr.Item("ExchangeRate") <> "" Then
                            .ExchangeRate = dr.Item("ExchangeRate")
                        Else
                            .ExchangeRate = 0
                        End If
                    Else
                        .ExchangeRate = 0
                    End If

                    If Not IsDBNull(dr.Item("PercentPurchaseRate")) Then
                        If dr.Item("PercentPurchaseRate") <> "" Then
                            .PercentPurchaseRate = dr.Item("PercentPurchaseRate")
                        Else
                            .PercentPurchaseRate = 0
                        End If
                    Else
                        .PercentPurchaseRate = 0
                    End If

                    If Not IsDBNull(dr.Item("PercentExchangeRate")) Then
                        If dr.Item("PercentExchangeRate") <> "" Then
                            .PercentExchangeRate = dr.Item("PercentExchangeRate")
                        Else
                            .PercentExchangeRate = 0
                        End If
                    Else
                        .PercentExchangeRate = 0
                    End If

                    If Not IsDBNull(dr.Item("PercentDamageRate")) Then
                        If dr.Item("PercentDamageRate") <> "" Then
                            .PercentDamageRate = dr.Item("PercentDamageRate")
                        Else
                            .PercentDamageRate = 0
                        End If
                    Else
                        .PercentDamageRate = 0
                    End If
                    .Remark = IIf(dr.Item("Remark") = "", "-", dr.Item("Remark"))
                End With

                If CurrentPriceObj.DefineID = "0" Then
                    CurrentPriceObj.DefineID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice, CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, CurrentPriceObj.DefineDateTime)
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                    DateTime.Now, _
                    Global_UserID, _
                    CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, _
                    CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                    CurrentPriceObj.DefineID, _
                    "Insert CurrentPrice")
                    bolRet = _objCurrentPriceDA.InsertCurrentPrice(CurrentPriceObj)
                Else
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                    DateTime.Now, _
                    Global_UserID, _
                    CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, _
                    CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                    CurrentPriceObj.DefineID, _
                    "Update CurrentPrice")
                    bolRet = _objCurrentPriceDA.UpdateCurrentPrice(CurrentPriceObj)
                End If
            Next
            Return bolRet
            
        End Function

        Public Function GetAllGoldPriceListByDateTime() As System.Data.DataTable Implements ICurrentPriceController.GetAllGoldPriceListByDateTime
            Return _objCurrentPriceDA.GetAllGoldPriceListByDateTime()
        End Function

        Public Function GetGoldPriceDataByDateTime(ByVal DefineDate As Date) As System.Data.DataTable Implements ICurrentPriceController.GetGoldPriceDataByDateTime
            Return _objCurrentPriceDA.GetGoldPriceDataByDateTime(DefineDate)
        End Function
        Public Function GetSolidGoldPrice() As System.Data.DataTable Implements ICurrentPriceController.GetSolidGoldPrice
            Return _objCurrentPriceDA.GetSolidGoldPrice()
        End Function
        Public Function DeleteGoldPriceData(ByVal dtGoldPrice As DataTable) As Boolean Implements ICurrentPriceController.DeleteGoldPriceData
            For Each dr As DataRow In dtGoldPrice.Rows
                If _objCurrentPriceDA.DeleteCurrentPrice(dr.Item("DefineID")) = True Then
                    _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                    DateTime.Now, _
                    Global_UserID, _
                    CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, _
                    CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                    dr.Item("DefineID"), _
                    "Delete CurrentPrice")
                    Return True
                Else
                    Return False
                End If
            Next
        End Function
        
    End Class

End Namespace
