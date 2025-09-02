Imports DataAccess.CheckStock
Imports DataAccess.GoodItems
Imports CommonInfo
Namespace CheckStock
    Public Class CheckStockController
        Implements ICheckStockController

#Region "Private Members"

        Private _objCheckStockDA As ICheckStockDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As ICheckStockController = New CheckStockController
        Dim _objCheckStockItemDA As New DataAccess.CheckStock.CheckStockDA

#End Region
#Region "Constructors"

        Private Sub New()
            _objCheckStockDA = DataAccess.Factory.Instance.CreateCheckStockDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As ICheckStockController
            Get
                Return _instance
            End Get
        End Property

#End Region

        

        Public Function InsertCheckStock(ByVal objCheckStock As CommonInfo.CheckStockInfo, ByVal _dtMissing As System.Data.DataTable, ByVal _dtExtra As DataTable, ByVal _dtFind As DataTable) As Boolean Implements ICheckStockController.InsertCheckStock
            Dim objGeneralController As General.IGeneralController
            Dim bolRet As Boolean
            Dim tmpdt As New DataTable
            objGeneralController = Factory.Instance.CreateGeneralController
            If objCheckStock.CheckStockID = "" Then
                objCheckStock.CheckStockID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.CheckStock, CommonInfo.EnumSetting.GenerateKeyType.CheckStock.ToString, objCheckStock.checkdatetime)
                bolRet = _objCheckStockDA.InsertCheckStock(objCheckStock)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                       DateTime.Now, _
                       Global_UserID, _
                       CommonInfo.EnumSetting.GenerateKeyType.ForSale.ToString, _
                       CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                      objCheckStock.CheckStockID, _
                       "Insert Check Stock")
                If bolRet = True Then
                    For Each drMissing As DataRow In _dtMissing.Rows
                        Dim objMissingItem As New CommonInfo.CheckStockItemInfo


                        With objMissingItem
                            .CheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "CheckStockItem", objCheckStock.checkdatetime)
                            .CheckStockID = objCheckStock.CheckStockID
                            .MItemCategoryID = IIf(IsDBNull(drMissing.Item("ItemCategoryID")) = True, 0, drMissing.Item("ItemCategoryID"))
                            .MBarcodeNo = IIf(IsDBNull(drMissing.Item("ItemCode")) = True, 0, drMissing.Item("ItemCode"))
                            .MGoldTG = IIf(IsDBNull(drMissing.Item("GoldTG")) = True, 0, drMissing.Item("GoldTG"))

                        End With

                        bolRet = _objCheckStockItemDA.InsertCheckStockItem(objMissingItem)
                    Next
                End If

                If bolRet = True Then
                    Dim objECheckStockItem As New CommonInfo.ECheckStockItemInfo
                    For Each drExtra As DataRow In _dtExtra.Rows

                        With objECheckStockItem
                            .ECheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "ECheckStockItem", objCheckStock.checkdatetime)
                            .CheckStockID = objCheckStock.CheckStockID
                            .EBarcodeNo = IIf(IsDBNull(drExtra.Item("ItemCode")) = True, "", drExtra.Item("ItemCode"))
                            .Weight = IIf(IsDBNull(drExtra.Item("GoldTG")) = True, 0.0, drExtra.Item("GoldTG"))
                        End With
                        bolRet = _objCheckStockItemDA.InsertECheckStockItem(objECheckStockItem)
                    Next
                End If

                If bolRet = True Then
                    For Each drFind As DataRow In _dtFind.Rows
                        bolRet = _objCheckStockItemDA.UpdateIsCheck(drFind.Item("ItemCode"), True)
                    Next
                End If

            Else
                bolRet = _objCheckStockDA.UpdateCheckStock(objCheckStock)
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.CheckStock.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                       objCheckStock.CheckStockID, _
                                       "Update Check Stock")
                _objCheckStockItemDA.DeleteMCheckStock(CStr(objCheckStock.CheckStockID))
                _objCheckStockItemDA.DeleteECheckStock(CStr(objCheckStock.CheckStockID))
                If bolRet = True Then
                    For Each drMissing As DataRow In _dtMissing.Rows
                        Dim objMissingItem As New CommonInfo.CheckStockItemInfo


                        With objMissingItem
                            .CheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "CheckStockItem", objCheckStock.checkdatetime)
                            .CheckStockID = objCheckStock.CheckStockID
                            .MItemCategoryID = IIf(IsDBNull(drMissing.Item("ItemCategoryID")) = True, 0, drMissing.Item("ItemCategoryID"))
                            .MBarcodeNo = IIf(IsDBNull(drMissing.Item("ItemCode")) = True, 0, drMissing.Item("ItemCode"))
                            .MGoldTG = IIf(IsDBNull(drMissing.Item("GoldTG")) = True, 0, drMissing.Item("GoldTG"))

                        End With

                        bolRet = _objCheckStockItemDA.InsertCheckStockItem(objMissingItem)
                    Next
                End If
                If bolRet = True Then
                    Dim objECheckStockItem As New CommonInfo.ECheckStockItemInfo
                    For Each drExtra As DataRow In _dtExtra.Rows

                        With objECheckStockItem
                            .ECheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "ECheckStockItem", objCheckStock.checkdatetime)
                            .CheckStockID = objCheckStock.CheckStockID
                            .EBarcodeNo = IIf(IsDBNull(drExtra.Item("ItemCode")) = True, "", drExtra.Item("ItemCode"))
                            .Weight = IIf(IsDBNull(drExtra.Item("GoldTG")) = True, 0, drExtra.Item("GoldTG"))
                        End With
                        bolRet = _objCheckStockItemDA.InsertECheckStockItem(objECheckStockItem)
                    Next
                End If

                If bolRet = True Then
                    For Each drFind As DataRow In _dtFind.Rows
                        bolRet = _objCheckStockItemDA.UpdateIsCheck(drFind.Item("ItemCode"), True)
                    Next
                End If
            End If

            'If bolRet = True Then
            '    For Each drMissing As DataRow In _dtMissing.Rows
            '        Dim objMissingItem As New CommonInfo.CheckStockItemInfo


            '        With objMissingItem
            '            .CheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "CheckStockItem", objCheckStock.checkdatetime)
            '            .CheckStockID = objCheckStock.CheckStockID
            '            .MItemCategoryID = IIf(IsDBNull(drMissing.Item("ItemCategoryID")) = True, 0, drMissing.Item("ItemCategoryID"))
            '            .MBarcodeNo = IIf(IsDBNull(drMissing.Item("BarcodeNo")) = True, 0, drMissing.Item("BarcodeNo"))
            '            .MGoldTG = IIf(IsDBNull(drMissing.Item("GoldTG")) = True, 0, drMissing.Item("GoldTG"))

            '        End With
            '        bolRet = _objCheckStockItemDA.DeleteMCheckStock(CStr(drMissing.Item("CheckStockItemID", DataRowVersion.Original)))
            '        bolRet = _objCheckStockItemDA.InsertCheckStockItem(objMissingItem)
            '    Next

            'End If
            'If bolRet = True Then
            '    Dim objECheckStockItem As New CommonInfo.ECheckStockItemInfo
            '    For Each drExtra As DataRow In _dtExtra.Rows

            '        With objECheckStockItem
            '            .ECheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "ECheckStockItem", objCheckStock.checkdatetime)
            '            .CheckStockID = objCheckStock.CheckStockID
            '            .EBarcodeNo = IIf(IsDBNull(drExtra.Item("BarcodeNo")) = True, "", drExtra.Item("BarcodeNo"))
            '            .Weight = IIf(IsDBNull(drExtra.Item("GoldTG")) = True, 0, drExtra.Item("GoldTG"))
            '        End With
            '        bolRet = _objCheckStockItemDA.InsertECheckStockItem(objECheckStockItem)
            '    Next
            'End If
            'If bolRet = True Then
            '    For Each drMissing As DataRow In _dtMissing.Rows
            '        Dim objMissingItem As New CommonInfo.CheckStockItemInfo
            '        If drMissing.RowState = DataRowState.Added Then
            '            With objMissingItem
            '                .CheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "CheckStockItemID", objCheckStock.checkdatetime)
            '                .CheckStockID = objMissingItem.CheckStockID
            '                .MItemCategoryID = IIf(IsDBNull(drMissing.Item("ItemCategoryID")) = True, 0, drMissing.Item("ItemCategoryID"))
            '                .MBarcodeNo = IIf(IsDBNull(drMissing.Item("BarcodeNo")) = True, 0, drMissing.Item("BarcodeNo"))
            '                .MGoldTG = IIf(IsDBNull(drMissing.Item("GoldTG")) = True, 0, drMissing.Item("GoldTG"))

            '            End With
            '            bolRet = _objCheckStockItemDA.InsertCheckStockItem(objMissingItem)


            '        ElseIf drMissing.RowState = DataRowState.Modified Then
            '            With objMissingItem
            '                .CheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "CheckStockItemID", objCheckStock.checkdatetime)
            '                .CheckStockID = objMissingItem.CheckStockID
            '                .MItemCategoryID = IIf(IsDBNull(drMissing.Item("ItemCategoryID")) = True, 0, drMissing.Item("ItemCategoryID"))
            '                .MBarcodeNo = IIf(IsDBNull(drMissing.Item("BarcodeNo")) = True, 0, drMissing.Item("BarcodeNo"))
            '                .MGoldTG = IIf(IsDBNull(drMissing.Item("GoldTG")) = True, 0, drMissing.Item("GoldTG"))

            '            End With
            '            bolRet = _objCheckStockDA.UpdateMCheckStock(objMissingItem)


            '        ElseIf drMissing.RowState = DataRowState.Unchanged Then
            '            With objMissingItem
            '                .CheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "CheckStockItemID", objCheckStock.checkdatetime)
            '                .CheckStockID = objMissingItem.CheckStockID
            '                .MItemCategoryID = IIf(IsDBNull(drMissing.Item("ItemCategoryID")) = True, 0, drMissing.Item("ItemCategoryID"))
            '                .MBarcodeNo = IIf(IsDBNull(drMissing.Item("BarcodeNo")) = True, 0, drMissing.Item("BarcodeNo"))
            '                .MGoldTG = IIf(IsDBNull(drMissing.Item("GoldTG")) = True, 0, drMissing.Item("GoldTG"))

            '            End With
            '            bolRet = _objCheckStockItemDA.InsertCheckStockItem(objMissingItem)

            '        ElseIf drMissing.RowState = DataRowState.Deleted Then
            '            bolRet = _objCheckStockItemDA.DeleteMCheckStock(CStr(drMissing.Item("CheckStockItemID", DataRowVersion.Original)))

            '        End If

            '    Next
            'End If

            'If bolRet = True Then
            '    Dim objECheckStockItem As New CommonInfo.ECheckStockItemInfo

            '    For Each drExtra As DataRow In _dtExtra.Rows
            '        If drExtra.RowState = DataRowState.Added Then
            '            With objECheckStockItem
            '                .ECheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "ECheckStockItem", objCheckStock.checkdatetime)
            '                .CheckStockID = objCheckStock.CheckStockID
            '                .EBarcodeNo = IIf(IsDBNull(drExtra.Item("BarcodeNo")) = True, "", drExtra.Item("BarcodeNo"))
            '                .Weight = IIf(IsDBNull(drExtra.Item("GoldTG")) = True, 0, drExtra.Item("GoldTG"))
            '            End With
            '            bolRet = _objCheckStockItemDA.InsertECheckStockItem(objECheckStockItem)
            '        ElseIf drExtra.RowState = DataRowState.Modified Then
            '            With objECheckStockItem
            '                .ECheckStockItemID = objGeneralController.GenerateKey(EnumSetting.GenerateKeyType.CheckStockItem, "ECheckStockItem", objCheckStock.checkdatetime)
            '                .CheckStockID = objCheckStock.CheckStockID
            '                .EBarcodeNo = IIf(IsDBNull(drExtra.Item("BarcodeNo")) = True, "", drExtra.Item("BarcodeNo"))
            '                .Weight = IIf(IsDBNull(drExtra.Item("GoldTG")) = True, 0, drExtra.Item("GoldTG"))
            '            End With
            '            bolRet = _objCheckStockItemDA.UpdateECheckStock(objECheckStockItem)


            '        ElseIf drExtra.RowState = DataRowState.Unchanged Then


            '        ElseIf drExtra.RowState = DataRowState.Deleted Then
            '            bolRet = _objCheckStockItemDA.DeleteECheckStock(CStr(drExtra.Item("ECheckStockItemID", DataRowVersion.Original)))

            '        End If

            '    Next
            'End If

            Return bolRet




        End Function

        Public Function GetCheckStockPrint(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockController.GetCheckStockPrint
            Return _objCheckStockDA.GetCheckStockPrint(CheckStockID)
        End Function
        Public Function GetMCheckStockPrint(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockController.GetMCheckStockPrint
            Return _objCheckStockDA.GetMCheckStockPrint(CheckStockID)
        End Function
        Public Function GetCheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockController.GetCheckStockReport
            Return _objCheckStockDA.GetCheckStockReport(dtpDate, cristr)
        End Function
        Public Function GetECheckStockPrint(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockController.GetECheckStockPrint
            Return _objCheckStockDA.GetECheckStockPrint(CheckStockID)
        End Function
        Public Function GetMCheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockController.GetMCheckStockReport
            Return _objCheckStockDA.GetMCheckStockReport(dtpDate, cristr)
        End Function
        Public Function GetECheckStockReport(ByVal dtpDate As Date, Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockController.GetECheckStockReport
            Return _objCheckStockDA.GetECheckStockReport(dtpDate, cristr)
        End Function
        Public Function GetCheckStockItem(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockController.GetCheckStockItem
            Return _objCheckStockDA.GetCheckStockItem(CheckStockID)
        End Function
        Public Function GetAllCheckStockLists(Optional ByVal cristr As String = "") As System.Data.DataTable Implements ICheckStockController.GetAllCheckStockLists
            Return _objCheckStockDA.GetAllCheckStockLists(cristr)
        End Function
        Public Function GetCheckStockEItem(ByVal CheckStockID As String) As System.Data.DataTable Implements ICheckStockController.GetCheckStockEItem
            Return _objCheckStockDA.GetCheckStockEItem(CheckStockID)
        End Function
        Public Function DeleteCheckStock(ByVal CheckStockID As String) As Boolean Implements ICheckStockController.DeleteCheckStock
            Dim tmpdt As New DataTable


            If _objCheckStockDA.DeleteCheckStock(CheckStockID) Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                       DateTime.Now, _
                                       Global_UserID, _
                                       CommonInfo.EnumSetting.GenerateKeyType.SaleInvoiceHeader.ToString, _
                                       CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                       CheckStockID, _
                                       "Delete Check Stock")

                Return True
            Else
                Return False
            End If
        End Function
        Public Function GetCheckStockByID(ByVal CheckStockID As String) As CommonInfo.CheckStockInfo Implements ICheckStockController.GetCheckStockByID
            Return _objCheckStockDA.GetCheckStockByID(CheckStockID)
        End Function
        'test
        Public Function ResetIsCheck(ByVal ItemCategoryID As String) As Boolean Implements ICheckStockController.ResetIsCheck
            Return _objCheckStockDA.ResetIsCheck(ItemCategoryID)
        End Function
    End Class
End Namespace

