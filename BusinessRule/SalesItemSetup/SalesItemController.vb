Imports DataAccess.SalesItem
Imports CommonInfo
Namespace SalesItem
    Public Class SalesItemController
        Implements ISalesItemController

#Region "Private Members"

        Private _objSalesItemDA As ISalesItemDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ISalesItemController = New SalesItemController

#End Region

#Region "Constructors"

        Private Sub New()
            _objSalesItemDA = DataAccess.Factory.Instance.CreateSalesItemDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ISalesItemController
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function GetSalesItemGems(ByVal SalesItemGemsID As String) As System.Data.DataTable Implements ISalesItemController.GetSalesItemGems
            Return _objSalesItemDA.GetSalesItemGems(SalesItemGemsID)
        End Function
        Public Function GetForSalesItemForSaleInvoice(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSalesItemForSaleInvoice
            Return _objSalesItemDA.GetForSalesItemForSaleInvoice(cristr)
        End Function
        Public Function GetForSalesItemForSaleLooseDiamond(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSalesItemForSaleLooseDiamond
            Return _objSalesItemDA.GetForSalesItemForSaleLooseDiamond(cristr)
        End Function
        Public Function GetForSaleForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal status As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleForReportByDatePeriod
            Return _objSalesItemDA.GetForSaleForReportByDatePeriod(FromDate, ToDate, cristr, status)
        End Function
        Public Function GetForSaleReportByLocation(ByVal FilterString As String, ByVal LocationID As String) As System.Data.DataTable Implements ISalesItemController.GetForSaleReportByLocation
            Return _objSalesItemDA.GetForSaleReportByLocation(FilterString, LocationID)
        End Function
        Public Function GetForSaleForReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleForReport
            Return _objSalesItemDA.GetForSaleForReport(cristr)
        End Function
        Public Function GetStockBalanceFromHO(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesItemController.GetStockBalanceFromHO
            Return _objSalesItemDA.GetStockBalanceFromHO(cristr, LocationID)
        End Function
        Public Function GetForSaleForIsCloseReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleForIsCloseReport
            Return _objSalesItemDA.GetForSaleForIsCloseReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetSalesIDByItemCode(ByVal argItemCode As String) As String Implements ISalesItemController.GetSalesIDByItemCode
            Return _objSalesItemDA.GetSalesIDByItemCode(argItemCode)
        End Function

        Public Function GetAllForSaleHeader(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetAllForSaleHeader
            Return _objSalesItemDA.GetAllForSaleHeader(cristr)
        End Function


        Public Function GetForSaleInfo(ByVal ForSaleID As String) As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleInfo
            Return _objSalesItemDA.GetForSaleInfo(ForSaleID)
        End Function
        Public Function GetForSaleInfo_History(ByVal ForSaleID As String) As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleInfo_History
            Return _objSalesItemDA.GetForSaleInfo_History(ForSaleID)
        End Function

        Public Function GetForSaleInfoY(ByVal ForSaleID As String) As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleInfoY
            Return _objSalesItemDA.GetForSaleInfo(ForSaleID)
        End Function
        Public Function SaveForSaleHeader(ByVal H_obj As CommonInfo.SalesItemInfo, ByVal obj As CommonInfo.SalesItemInfo, ByVal objItemCat As CommonInfo.ItemCategoryInfo, ByVal _dtSalesGems As System.Data.DataTable, Optional ByVal _GetItemCodeFromGF As String = "") As Boolean Implements ISalesItemController.SaveForSaleHeader
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController
            Dim objForSaleGems As New CommonInfo.SalesItemGemsInfo
            Dim objForSale As New CommonInfo.SalesItemInfo
            Dim _ItemCode As String = ""
            Dim _ForSaleID As String = ""
            Dim _ChangeItemStrValue As String = ""
            Dim _ChangeWasteStrValue As String = ""
            Dim _HisItemStrValue As String = ""
            Dim _HisWasteStrValue As String = ""

            Dim _GenerateFormatController As GenerateFormat.IGenerateFormatController
            _GenerateFormatController = Factory.Instance.CreateGenerateFormatController
            Dim _SalesItemCon As SalesItem.ISalesItemController

            _SalesItemCon = Factory.Instance.CreateSalesItemController

            Dim bolRet As Boolean = False
            If obj.ForSaleID = "0" Then
                obj.ForSaleID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ForSale, CommonInfo.EnumSetting.GenerateKeyType.ForSale.ToString, obj.GivenDate)

                If _GetItemCodeFromGF = obj.ItemCode Then
                    Dim objGenerateFormat As CommonInfo.GenerateFormatInfo
                    If obj.IsLooseDiamond = True Then
                        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.DiamondBarcode.ToString)
                    Else
                        objGenerateFormat = _GenerateFormatController.GetGenerateFormatByFormat(EnumSetting.TableType.Barcode.ToString)
                    End If

                    '_ItemCode = obj.ItemCode

                    If obj.IsVolume = True Then
                        If objGenerateFormat.GenerateFormat IsNot Nothing Then

                            If objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                                obj.ItemCode = "V" + objItemCat.Prefix + objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, "V" + objItemCat.Prefix)

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                                obj.ItemCode = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, "V" + objItemCat.Prefix) + "V" + objItemCat.Prefix

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                                obj.ItemCode = "V" + objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, "V")
                            End If
                        End If
                    ElseIf obj.IsLooseDiamond = True Then
                        If objGenerateFormat.GenerateFormat IsNot Nothing Then

                            If objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                                obj.ItemCode = objItemCat.Prefix + objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, objItemCat.Prefix)

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                                obj.ItemCode = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, "" + objItemCat.Prefix) + objItemCat.Prefix

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                                obj.ItemCode = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, "")
                            End If
                        End If

                    Else
                        If objGenerateFormat.GenerateFormat IsNot Nothing Then

                            If objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                                obj.ItemCode = objItemCat.Prefix + objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, objItemCat.Prefix, obj.ForSaleID)

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                                obj.ItemCode = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, objItemCat.Prefix, obj.ForSaleID) + objItemCat.Prefix

                            ElseIf objGenerateFormat.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                                obj.ItemCode = objGeneralController.GenerateKeyForFormat(objGenerateFormat, obj.GivenDate, "", obj.ForSaleID)
                            End If
                        End If
                    End If
                End If


                bolRet = _objSalesItemDA.InsertForSale(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.BarcodeNo.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                       obj.ItemCode & ", " & obj.ForSaleID, _
                                       "Insert Stock Item")



                If obj.IsOrder = True Then
                    UpdateOrderInvoiceReceiveForIsBarcodeNo(obj.OrderReceiveDetailID, True)
                End If

                'If obj.IsClosed Then
                '    UpdateSalesItemByIsExit(obj.ForSaleID, True, Now)
                'End If
            Else
                _ForSaleID = obj.ForSaleID
                H_obj = _SalesItemCon.GetForSaleInfo_History(_ForSaleID)
                'If H_obj.HItemK <> obj.ItemK Then
                '    _ChangeItemStrValue = H_obj.HItemK & " -> " & obj.ItemK & "K "
                'End If
                'If H_obj.HItemP <> obj.ItemP Then
                '    _ChangeItemStrValue += H_obj.HItemP & " -> " & obj.ItemP & "P "
                'End If
                'If H_obj.HItemY <> obj.ItemY Then
                '    _ChangeItemStrValue += H_obj.HItemY & " -> " & obj.ItemY & "Y "
                'End If

                'If H_obj.HWasteK <> obj.WasteK Then
                '    _ChangeWasteStrValue = H_obj.HWasteK & " -> " & obj.WasteK & "K "
                'End If
                'If H_obj.HWasteP <> obj.WasteP Then
                '    _ChangeWasteStrValue += H_obj.HWasteP & " -> " & obj.WasteP & "P "
                'End If
                'If H_obj.HWasteY <> obj.WasteY Then
                '    _ChangeWasteStrValue += H_obj.HWasteY & " -> " & obj.WasteY & "Y "
                'End If
                _HisItemStrValue = H_obj.HItemK & " - " & H_obj.HItemP & " - " & H_obj.HItemY & " "
                _HisWasteStrValue = H_obj.HWasteK & " - " & H_obj.HWasteP & " - " & H_obj.HWasteY & " "
                _ChangeItemStrValue = obj.ItemK & " - " & obj.ItemP & " - " & obj.ItemY & " "
                _ChangeWasteStrValue = obj.WasteK & " - " & obj.WasteP & " - " & obj.WasteY & " "

                bolRet = _objSalesItemDA.UpdateForSale(obj)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.BarcodeNo.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       obj.ItemCode & ", " & obj.ForSaleID, _
                                       "Update Stock Item: " & "OrgItemW= " & _HisItemStrValue & " OrgWaste= " & _HisWasteStrValue & "NewItemW= " & _ChangeItemStrValue & "NewWaste= " & _ChangeWasteStrValue)

                If obj.IsOrder Then
                    If obj.OldIsOrder = True Then
                        UpdateOrderInvoiceReceiveForIsBarcodeNo(obj.OldOrderReceiveDetailID, False)
                        UpdateOrderInvoiceReceiveForIsBarcodeNo(obj.OrderReceiveDetailID, True)
                    Else
                        UpdateOrderInvoiceReceiveForIsBarcodeNo(obj.OrderReceiveDetailID, True)
                    End If
                Else
                    If obj.OldIsOrder = True Then
                        UpdateOrderInvoiceReceiveForIsBarcodeNo(obj.OldOrderReceiveDetailID, False)
                    End If
                End If
                If obj.IsClosed Then
                    UpdateSalesItemByIsExit(obj.ForSaleID, True, Now)
                Else
                    UpdateSalesItemByIsExit(obj.ForSaleID, False, Now)
                End If

            End If

            If bolRet Then
                If Not _dtSalesGems Is Nothing Then
                    For Each drGem As DataRow In _dtSalesGems.Rows
                        If drGem.RowState = DataRowState.Added Then
                            With objForSaleGems
                                .ForSaleID = obj.ForSaleID
                                .ForSaleGemsItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ForSalesGemsItem, CommonInfo.EnumSetting.GenerateKeyType.ForSalesGemsItem.ToString, obj.GivenDate)
                                .GemsCategoryID = IIf(IsDBNull(drGem.Item("GemsCategoryID")) = True, "", drGem.Item("GemsCategoryID"))
                                .GemsName = IIf(IsDBNull(drGem.Item("GemsName")) = True, "-", drGem.Item("GemsName"))
                                .GemsTK = IIf(IsDBNull(drGem.Item("GemsTK")), "0", drGem.Item("GemsTK"))
                                .GemsTG = IIf(IsDBNull(drGem.Item("GemsTG")), "0", drGem.Item("GemsTG"))
                                .YOrCOrG = IIf(IsDBNull(drGem.Item("YOrCOrG")) = True, 0, drGem.Item("YOrCOrG"))
                                .GemsTW = IIf(IsDBNull(drGem.Item("GemsTW")) = True, 0, drGem.Item("GemsTW"))
                                .Qty = IIf(IsDBNull(drGem.Item("Qty")) = True, 0, drGem.Item("Qty"))
                                .Type = IIf(IsDBNull(drGem.Item("Type")) = True, "", drGem.Item("Type"))
                                .UnitPrice = IIf(IsDBNull(drGem.Item("UnitPrice")) = True, 0, drGem.Item("UnitPrice"))
                                .Amount = IIf(IsDBNull(drGem.Item("Amount")) = True, 0, drGem.Item("Amount"))
                                .GemsRemark = IIf(IsDBNull(drGem.Item("GemsRemark")) = True, "-", drGem.Item("GemsRemark"))  'drGem.Item("GemsRemark")
                                .SaleByDefinePrice = IIf(IsDBNull(drGem.Item("SaleByDefinePrice")) = True, 0, drGem.Item("SaleByDefinePrice"))
                            End With
                            _objSalesItemDA.InsertForSaleGems(objForSaleGems)

                        ElseIf drGem.RowState = DataRowState.Unchanged Then
                            If drGem.Item("ForSaleGemsItemID") = "" Or drGem.Item("ForSaleGemsItemID") = "0" Or IsDBNull(drGem.Item("ForSaleGemsItemID")) Then
                                With objForSaleGems
                                    .ForSaleID = obj.ForSaleID
                                    .ForSaleGemsItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ForSalesGemsItem, CommonInfo.EnumSetting.GenerateKeyType.ForSalesGemsItem.ToString, obj.GivenDate)
                                    .GemsCategoryID = IIf(IsDBNull(drGem.Item("GemsCategoryID")) = True, "", drGem.Item("GemsCategoryID"))
                                    .GemsName = IIf(IsDBNull(drGem.Item("GemsName")) = True, "-", drGem.Item("GemsName"))
                                    .GemsTK = IIf(IsDBNull(drGem.Item("GemsTK")), "0", drGem.Item("GemsTK"))
                                    .GemsTG = IIf(IsDBNull(drGem.Item("GemsTG")), "0", drGem.Item("GemsTG"))
                                    .YOrCOrG = IIf(IsDBNull(drGem.Item("YOrCOrG")) = True, 0, drGem.Item("YOrCOrG"))
                                    .GemsTW = IIf(IsDBNull(drGem.Item("GemsTW")) = True, 0, drGem.Item("GemsTW"))
                                    .Qty = IIf(IsDBNull(drGem.Item("Qty")) = True, 0, drGem.Item("Qty"))
                                    .Type = IIf(IsDBNull(drGem.Item("Type")) = True, "", drGem.Item("Type"))
                                    .UnitPrice = IIf(IsDBNull(drGem.Item("UnitPrice")) = True, 0, drGem.Item("UnitPrice"))
                                    .Amount = IIf(IsDBNull(drGem.Item("Amount")) = True, 0, drGem.Item("Amount"))
                                    .GemsRemark = IIf(IsDBNull(drGem.Item("GemsRemark")) = True, "-", drGem.Item("GemsRemark"))  'drGem.Item("GemsRemark")
                                    .SaleByDefinePrice = IIf(IsDBNull(drGem.Item("SaleByDefinePrice")) = True, 0, drGem.Item("SaleByDefinePrice"))
                                End With
                                _objSalesItemDA.InsertForSaleGems(objForSaleGems)
                            Else
                                With objForSaleGems
                                    .ForSaleID = obj.ForSaleID
                                    .ForSaleGemsItemID = drGem.Item("ForSaleGemsItemID")
                                    .GemsCategoryID = IIf(IsDBNull(drGem.Item("GemsCategoryID")) = True, "", drGem.Item("GemsCategoryID"))
                                    .GemsName = IIf(IsDBNull(drGem.Item("GemsName")) = True, "-", drGem.Item("GemsName"))
                                    .GemsTK = IIf(IsDBNull(drGem.Item("GemsTK")), "0", drGem.Item("GemsTK"))
                                    .GemsTG = IIf(IsDBNull(drGem.Item("GemsTG")), "0", drGem.Item("GemsTG"))
                                    .YOrCOrG = IIf(IsDBNull(drGem.Item("YOrCOrG")) = True, 0, drGem.Item("YOrCOrG"))
                                    .GemsTW = IIf(IsDBNull(drGem.Item("GemsTW")) = True, 0, drGem.Item("GemsTW"))
                                    .Qty = IIf(IsDBNull(drGem.Item("Qty")) = True, 0, drGem.Item("Qty"))
                                    .Type = IIf(IsDBNull(drGem.Item("Type")) = True, "", drGem.Item("Type"))
                                    .UnitPrice = IIf(IsDBNull(drGem.Item("UnitPrice")) = True, 0, drGem.Item("UnitPrice"))
                                    .Amount = IIf(IsDBNull(drGem.Item("Amount")) = True, 0, drGem.Item("Amount"))
                                    .GemsRemark = IIf(IsDBNull(drGem.Item("GemsRemark")) = True, "-", drGem.Item("GemsRemark"))  'drGem.Item("GemsRemark")
                                    .SaleByDefinePrice = IIf(IsDBNull(drGem.Item("SaleByDefinePrice")) = True, 0, drGem.Item("SaleByDefinePrice"))
                                End With
                                _objSalesItemDA.UpdateForSaleGems(objForSaleGems)
                            End If


                        ElseIf drGem.RowState = DataRowState.Modified Then
                            If drGem.Item("ForSaleGemsItemID") = "" Or drGem.Item("ForSaleGemsItemID") = "0" Or IsDBNull(drGem.Item("ForSaleGemsItemID")) Then
                                With objForSaleGems
                                    .ForSaleID = obj.ForSaleID
                                    .ForSaleGemsItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.ForSalesGemsItem, CommonInfo.EnumSetting.GenerateKeyType.ForSalesGemsItem.ToString, obj.GivenDate)
                                    .GemsCategoryID = IIf(IsDBNull(drGem.Item("GemsCategoryID")) = True, "", drGem.Item("GemsCategoryID"))
                                    .GemsName = IIf(IsDBNull(drGem.Item("GemsName")) = True, "-", drGem.Item("GemsName"))
                                    .GemsTK = IIf(IsDBNull(drGem.Item("GemsTK")), "0", drGem.Item("GemsTK"))
                                    .GemsTG = IIf(IsDBNull(drGem.Item("GemsTG")), "0", drGem.Item("GemsTG"))
                                    .YOrCOrG = IIf(IsDBNull(drGem.Item("YOrCOrG")) = True, 0, drGem.Item("YOrCOrG"))
                                    .GemsTW = IIf(IsDBNull(drGem.Item("GemsTW")) = True, 0, drGem.Item("GemsTW"))
                                    .Qty = IIf(IsDBNull(drGem.Item("Qty")) = True, 0, drGem.Item("Qty"))
                                    .Type = IIf(IsDBNull(drGem.Item("Type")) = True, "", drGem.Item("Type"))
                                    .UnitPrice = IIf(IsDBNull(drGem.Item("UnitPrice")) = True, 0, drGem.Item("UnitPrice"))
                                    .Amount = IIf(IsDBNull(drGem.Item("Amount")) = True, 0, drGem.Item("Amount"))
                                    .GemsRemark = IIf(IsDBNull(drGem.Item("GemsRemark")) = True, "-", drGem.Item("GemsRemark"))  'drGem.Item("GemsRemark")
                                    .SaleByDefinePrice = IIf(IsDBNull(drGem.Item("SaleByDefinePrice")) = True, 0, drGem.Item("SaleByDefinePrice"))
                                End With
                                _objSalesItemDA.InsertForSaleGems(objForSaleGems)
                            Else
                                With objForSaleGems
                                    .ForSaleID = obj.ForSaleID
                                    .ForSaleGemsItemID = drGem.Item("ForSaleGemsItemID")
                                    .GemsCategoryID = IIf(IsDBNull(drGem.Item("GemsCategoryID")) = True, "", drGem.Item("GemsCategoryID"))
                                    .GemsName = IIf(IsDBNull(drGem.Item("GemsName")) = True, "-", drGem.Item("GemsName"))
                                    .GemsTK = IIf(IsDBNull(drGem.Item("GemsTK")), "0", drGem.Item("GemsTK"))
                                    .GemsTG = IIf(IsDBNull(drGem.Item("GemsTG")), "0", drGem.Item("GemsTG"))
                                    .YOrCOrG = IIf(IsDBNull(drGem.Item("YOrCOrG")) = True, 0, drGem.Item("YOrCOrG"))
                                    .GemsTW = IIf(IsDBNull(drGem.Item("GemsTW")) = True, 0, drGem.Item("GemsTW"))
                                    .Qty = IIf(IsDBNull(drGem.Item("Qty")) = True, 0, drGem.Item("Qty"))
                                    .Type = IIf(IsDBNull(drGem.Item("Type")) = True, "", drGem.Item("Type"))
                                    .UnitPrice = IIf(IsDBNull(drGem.Item("UnitPrice")) = True, 0, drGem.Item("UnitPrice"))
                                    .Amount = IIf(IsDBNull(drGem.Item("Amount")) = True, 0, drGem.Item("Amount"))
                                    .GemsRemark = IIf(IsDBNull(drGem.Item("GemsRemark")) = True, "-", drGem.Item("GemsRemark"))  'drGem.Item("GemsRemark")
                                    .SaleByDefinePrice = IIf(IsDBNull(drGem.Item("SaleByDefinePrice")) = True, 0, drGem.Item("SaleByDefinePrice"))
                                End With
                                _objSalesItemDA.UpdateForSaleGems(objForSaleGems)
                            End If
                        ElseIf drGem.RowState = DataRowState.Deleted Then
                            _objSalesItemDA.DeleteForSaleGems(CStr(drGem.Item("ForSaleGemsItemID", DataRowVersion.Original)))
                        End If
                    Next
                End If
            End If
            Return bolRet
        End Function
        Private Sub UpdateSalesItemByIsExit(ByVal argForSaleID As String, ByVal argIsExit As Boolean, ByVal argExitDate As Date)
            Dim objSaleItem As New CommonInfo.SalesItemInfo
            With objSaleItem
                .ForSaleID = argForSaleID
                .IsExit = argIsExit
                .ExitDate = argExitDate
            End With
            _objSalesItemDA.UpdateSaleItemIsExit(objSaleItem)
        End Sub
        Private Sub UpdateOrderInvoiceReceiveForIsBarcodeNo(ByVal OrderReceiveDetailID As String, ByVal IsBarcode As Boolean)
            _objSalesItemDA.UpdateOrderInvoiceReceiveForIsBarcodeNo(OrderReceiveDetailID, IsBarcode)
        End Sub

        Public Function GetForSaleGems(ByVal ForSaleID As String) As System.Data.DataTable Implements ISalesItemController.GetForSaleGems
            Return _objSalesItemDA.GetForSaleGems(ForSaleID)
        End Function

        Public Function GetForSaleItemInfo(ByVal ForSaleHeaderID As String, ByVal cristr As String) As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleItemInfo
            Return _objSalesItemDA.GetForSaleItemInfo(ForSaleHeaderID, cristr)
        End Function

        Public Function DeleteForSale(ByVal ForSaleID As String) As Boolean Implements ISalesItemController.DeleteForSale
            Dim obj As CommonInfo.SalesItemInfo
            obj = _objSalesItemDA.GetForSaleInfo(ForSaleID)
            If obj.IsOrder = True Then
                UpdateOrderInvoiceReceiveForIsBarcodeNo(obj.OrderReceiveDetailID, False)
            End If

            If _objSalesItemDA.DeleteForSale(ForSaleID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.BarcodeNo.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       obj.ItemCode, _
                                       "Delete Stock Item")
                Return True
            Else
                Return False
            End If

        End Function
        
        Public Function GetForSaleInfoByItemCode(ByVal ItemCode As String, Optional ByVal argForSaleIDStr As String = "") As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleInfoByItemCode
            Return _objSalesItemDA.GetForSaleInfoByItemCode(ItemCode, argForSaleIDStr)
        End Function
        Public Function GetForSaleInfoByWSItemCode(ByVal ItemCode As String, ByVal argForSaleIDStr As String) As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleInfoByWSItemCode
            Return _objSalesItemDA.GetForSaleInfoByWSItemCode(ItemCode, argForSaleIDStr)
        End Function

        Public Function GetForSaleDataByItemCode(ByVal StrCri As String) As System.Data.DataTable Implements ISalesItemController.GetForSaleDataByItemCode
            Return _objSalesItemDA.GetForSaleDataByItemCode(StrCri)
        End Function

        Public Function GetForSalesItemForOrderInvoice(ByVal OrderInvoiceID As String, Optional ByVal argForSaleIDStr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSalesItemForOrderInvoice
            Return _objSalesItemDA.GetForSalesItemForOrderInvoice(OrderInvoiceID, argForSaleIDStr)
        End Function

        Public Function GetForSaleInfoByItemCodeANDOrderInvoiceID(ByVal ItemCode As String, ByVal OrderInvoiceID As String, Optional ByVal argForSaleIDStr As String = "") As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleInfoByItemCodeANDOrderInvoiceID
            Return _objSalesItemDA.GetForSaleInfoByItemCodeANDOrderInvoiceID(ItemCode, OrderInvoiceID, argForSaleIDStr)
        End Function

        Public Function GetForSaleForReportByTotalWeight(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleForReportByTotalWeight
            Return _objSalesItemDA.GetForSaleForReportByTotalWeight(cristr)
        End Function
        Public Function GetBalanceFromHOByTotalWeight(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesItemController.GetBalanceFromHOByTotalWeight
            Return _objSalesItemDA.GetBalanceFromHOByTotalWeight(cristr, LocationID)
        End Function
        Public Function GetForSaleForReportByDatePeriodAndTotalWeight(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleForReportByDatePeriodAndTotalWeight
            Return _objSalesItemDA.GetForSaleForReportByDatePeriodAndTotalWeight(FromDate, ToDate, cristr)
        End Function

        Public Function GetForSaleVolumeForReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleVolumeForReport
            Return _objSalesItemDA.GetForSaleVolumeForReport(cristr)
        End Function
        Public Function GetForSaleLooseDiamondForReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleLooseDiamondForReport
            Return _objSalesItemDA.GetForSaleLooseDiamondForReport(cristr)
        End Function

        Public Function GetForSaleVolumeForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleVolumeForReportByDatePeriod
            Return _objSalesItemDA.GetForSaleVolumeForReportByDatePeriod(FromDate, ToDate, cristr)
        End Function
        Public Function GetForSaleLooseDiamondForReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleLooseDiamondForReportByDatePeriod
            Return _objSalesItemDA.GetForSaleLooseDiamondForReportByDatePeriod(FromDate, ToDate, cristr)
        End Function

        Public Function GetForSaleDataBySummaryReport(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleDataBySummaryReport
            Return _objSalesItemDA.GetForSaleDataBySummaryReport(cristr)
        End Function
        Public Function GetForSaleDataBySummaryReportByLocation(Optional ByVal cristr As String = "", Optional ByVal LocationID As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleDataBySummaryReportByLocation
            Return _objSalesItemDA.GetForSaleDataBySummaryReportByLocation(cristr, LocationID)
        End Function
        Public Function GetForSaleDataBySummaryIsCloseReport(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleDataBySummaryIsCloseReport
            Return _objSalesItemDA.GetForSaleDataBySummaryIsCloseReport(FromDate, ToDate, cristr)
        End Function
        Public Function GetForSaleForSummaryReportByDatePeriod(ByVal FromDate As Date, ByVal ToDate As Date, Optional ByVal cristr As String = "", Optional ByVal Status As String = "") As System.Data.DataTable Implements ISalesItemController.GetForSaleForSummaryReportByDatePeriod
            Return _objSalesItemDA.GetForSaleForSummaryReportByDatePeriod(FromDate, ToDate, cristr, Status)
        End Function
        Public Function GetForSalesVolumeItemForSaleInvoice(Optional ByVal cristr As String = "", Optional ByVal CheckState As Boolean = False) As System.Data.DataTable Implements ISalesItemController.GetForSalesVolumeItemForSaleInvoice
            Return _objSalesItemDA.GetForSalesVolumeItemForSaleInvoice(cristr, CheckState)
        End Function
        Public Function GetSaleItemDataByItemCodeAndForSaleID(ByVal ItemCode As String, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ISalesItemController.GetSaleItemDataByItemCodeAndForSaleID
            Return _objSalesItemDA.GetSaleItemDataByItemCodeAndForSaleID(ItemCode, cristr)
        End Function
        Public Function GetForSaleDataByItemCodeAndIsExit(ItemCode As String, IsExit As Boolean, Optional cristr As String = "") As DataTable Implements ISalesItemController.GetForSaleDataByItemCodeAndIsExit
            Return _objSalesItemDA.GetForSaleDataByItemCodeAndIsExit(ItemCode, IsExit, cristr)
        End Function
        Public Function GetSaleItemDataByOrderReceiveDetailID(ByVal OrderReceiveDetailID As String) As System.Data.DataTable Implements ISalesItemController.GetSaleItemDataByOrderReceiveDetailID
            Return _objSalesItemDA.GetSaleItemDataByOrderReceiveDetailID(OrderReceiveDetailID)
        End Function
        Public Function GetSaleInvoiceForRepair(Optional cristr As String = "", Optional ByVal BarcodeNo As String = "") As DataTable Implements ISalesItemController.GetSaleInvoiceForRepair
            Return _objSalesItemDA.GetSaleInvoiceForRepair(cristr, BarcodeNo)
        End Function

        Public Function GetSaleInvoiceObjDataForRepair(cristr As String) As SalesInvoiceDetailInfo Implements ISalesItemController.GetSaleInvoiceObjDataForRepair
            Return _objSalesItemDA.GetSaleInvoiceObjDataForRepair(cristr)
        End Function
        Public Function GetBalanceStockForDate(FromDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "") As DataTable Implements ISalesItemController.GetBalanceStockForDate
            Return _objSalesItemDA.GetBalanceStockForDate(FromDate, cristr, LocationID)
        End Function
        Public Function GetStockItemCardReport(FromDate As Date, ToDate As Date, Optional cristr As String = "") As DataTable Implements ISalesItemController.GetStockItemCardReport
            Return _objSalesItemDA.GetStockItemCardReport(FromDate, ToDate, cristr)
        End Function

        Public Function GetAllStockDataForMonthly(FromDate As Date, ToDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False, Optional ByVal global_isHOToBranch As Boolean = False) As DataTable Implements ISalesItemController.GetAllStockDataForMonthly
            Return _objSalesItemDA.GetAllStockDataForMonthly(FromDate, ToDate, cristr, LocationID, global_isHeadOffice, global_isHOToBranch)
        End Function
        Public Function GetAllStockDataForMonthlyByTransferDate(FromDate As Date, ToDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False, Optional ByVal global_isHOToBranch As Boolean = False) As DataTable Implements ISalesItemController.GetAllStockDataForMonthlyByTransferDate
            Return _objSalesItemDA.GetAllStockDataForMonthlyByTransferDate(FromDate, ToDate, cristr, LocationID, global_isHeadOffice, global_isHOToBranch)
        End Function

        Public Function GetAllStockDataForMonthlyForOffline(FromDate As Date, ToDate As Date, Optional cristr As String = "", Optional ByVal LocationID As String = "", Optional ByVal global_isHeadOffice As Boolean = False) As DataTable Implements ISalesItemController.GetAllStockDataForMonthlyForOffline
            Return _objSalesItemDA.GetAllStockDataForMonthlyForOffline(FromDate, ToDate, cristr, LocationID, global_isHeadOffice)
        End Function

        Public Function GetForSaleItemListForTransfer(ByVal argForSaleIDStr As String) As System.Data.DataTable Implements ISalesItemController.GetForSaleItemListForTransfer
            Return _objSalesItemDA.GetForSaleItemListForTransfer(argForSaleIDStr)
        End Function
        Public Function GetForSaleItemListForTransferDiamond(ByVal argForSaleIDStr As String) As System.Data.DataTable Implements ISalesItemController.GetForSaleItemListForTransferDiamond
            Return _objSalesItemDA.GetForSaleItemListForTransferDiamond(argForSaleIDStr)
        End Function
        Public Function GetForSaleItemListForWholesales(ByVal argForSaleIDStr As String) As System.Data.DataTable Implements ISalesItemController.GetForSaleItemListForWholesales
            Return _objSalesItemDA.GetForSaleItemListForWholesales(argForSaleIDStr)
        End Function

        Public Function GetForSaleItembyItemCode(ByVal ItemCode As String, ByVal argForSaleIDStr As String) As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleItembyItemCode
            Return _objSalesItemDA.GetForSaleItembyItemCode(ItemCode, argForSaleIDStr)
        End Function
        Public Function GetForSaleInfoByBarcodeNo(ByVal ItemCode As String, ByVal ItemCategoryID As String, ByVal argForSaleIDStr As String) As CommonInfo.SalesItemInfo Implements ISalesItemController.GetForSaleInfoByBarCodeNo
            Return _objSalesItemDA.GetForSaleInfoByBarcodeNo(ItemCode, ItemCategoryID, argForSaleIDStr)
        End Function
        Public Function GetBarcodeTrack(ByVal BarcodeNo As String) As System.Data.DataTable Implements ISalesItemController.GetBarcodeTrack
            Return _objSalesItemDA.GetBarcodeTrack(BarcodeNo)
        End Function
    End Class
End Namespace

