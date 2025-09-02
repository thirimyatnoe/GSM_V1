Public Class Factory

    Private Shared ReadOnly _instance As Factory = New Factory()

    Private Sub New()

    End Sub

    Public Shared ReadOnly Property Instance() As Factory
        Get
            Return _instance
        End Get
    End Property

    Public Function CreateGeneralController() As General.IGeneralController
        Return General.GeneralController.Instance()
    End Function

    Public Function CreateExportDataController() As ExportData.IExportDataController
        Return ExportData.ExportDataController.Instance()
    End Function


    'Public Function CreateDamageController() As Damage.IDamageController
    '    Return Damage.DamageController.Instance
    'End Function

    Public Function CreateLocationController() As Location.ILocationController
        Return Location.LocationController.Instance
    End Function
    Public Function CreateCurrentPriceController() As CurrentPrice.ICurrentPriceController
        Return CurrentPrice.CurrentPriceController.Instance
    End Function
    Public Function CreateGoldQualityController() As GoldQuality.IGoldQualityController
        Return GoldQuality.GoldQualityController.Instance
    End Function
    Public Function CreateItemCategoryController() As ItemCategory.IItemCategoryController
        Return ItemCategory.ItemCategoryController.Instance
    End Function
    Public Function CreateGemsCategoryController() As GemsCategory.IGemsCategoryController
        Return GemsCategory.GemsCategoryController.Instance
    End Function
    Public Function CreateStaffController() As Staff.IStaffController
        Return Staff.StaffController.Instance
    End Function
    Public Function CreateConverterController() As Converter.IConverterController
        Return Converter.ConverterController.Instance
    End Function
    Public Function CreatePurchaseGemsController() As PurchaseGems.IPurchaseGemsController
        Return PurchaseGems.PurchaseGemsController.Instance
    End Function
    'Public Function CreatePurchaseInvoiceController() As PurchaseInvoice.IPurchaseInvoiceController
    '    Return PurchaseInvoice.PurchaseInvoiceController.Instance
    'End Function
    Public Function CreateSaleGemsController() As SaleGems.ISaleGemsController
        Return SaleGems.SaleGemsController.Instance
    End Function
    'Public Function CreateSaleInvoiceController() As SalesInvoice.ISalesInvoiceController
    '    Return SalesInvoice.SalesInvoiceController.Instance
    'End Function

    Public Function CreateMeasurementController() As Measurement.IMeasurementController
        Return Measurement.MeasurementController.Instance
    End Function
    Public Function CreateOrderInvoiceController() As OrderInvoice.IOrderInvoiceController
        Return OrderInvoice.OrderInvoiceController.Instance
    End Function
    Public Function CreateItemName() As ItemName.IItemNameController
        Return ItemName.ItemNameController.Instance
    End Function

    Public Function CreatecustomerController() As Customer.ICustomerController
        Return Customer.CustomerController.Instance
    End Function
    Public Function CreateGoldSmithController() As GoldSmith.IGoldSmithController
        Return GoldSmith.GoldSmithController.Instance
    End Function
    Public Function CreateGeneralLedgerByLocationController() As GeneralLedgerByLocation.IGeneralLedgerByLocationController
        Return GeneralLedgerByLocation.GeneralLedgerByLocationController.Instance
    End Function
    Public Function CreateTransferController() As Transfer.ITransferController
        Return Transfer.TransferController.Instance
    End Function
    Public Function CreateCashReceiptController() As CashReceipt.ICashReceiptController
        Return CashReceipt.CashReceiptController.Instance
    End Function

    Public Function CreatePhotoPathController() As PhotoPath.IPhotoPathController
        Return PhotoPath.PhotoPathController.Instance
    End Function
    Public Function CreatePurchaseItemController() As PurchaseItem.IPurchaseItemController
        Return PurchaseItem.PurchaseItemController.Instance
    End Function
    Public Function CreateSaleItemInvoiceController() As SalesItemInvoice.ISalesItemInvoiceController
        Return SalesItemInvoice.SalesItemInvoiceController.Instance
    End Function
    Public Function CreateSalesVolumeController() As SalesVolume.ISalesVolumeController
        Return SalesVolume.SalesVolumeController.Instance
    End Function
    Function CreateGenerateFormatController() As GenerateFormat.IGenerateFormatController
        Return GenerateFormat.GenerateFormatController.Instance
    End Function
    Public Function CreateGlobalSettingController() As GlobalSetting.IGlobalSettingController
        Return GlobalSetting.GlobalSettingController.Instance
    End Function
    Function CreateCustomReportController() As CustomReport.ICustomReportController
        Return CustomReport.CustomReportController.Instance
    End Function
    Function CreateCompanyProfileController() As CompanyProfile.ICompanyProfileController
        Return CompanyProfile.CompanyProfileController.Instance
    End Function
    Public Function CreateSettingController() As Setting.ISettingController
        Try
            Return Setting.SettingController.Instance
        Catch ex As Exception
            If ex.Message = "The type initializer for 'BusinessRule.Setting.SettingController' threw an exception." Then
                MsgBox("Please Check Your Connection String!", MsgBoxStyle.Exclamation, "Jewelry Shop Management System")
                Exit Function
            End If

        End Try

    End Function

    Function CreateRepairController() As Repair.IRepairController
        Return Repair.RepairController.Instance
    End Function
    Function CreateRepairReturnController() As RepairReturn.IRepairReturnController
        Return RepairReturn.RepairReturnController.Instance
    End Function
    Function CreateSupplierController() As Supplier.ISupplierController
        Return Supplier.SupplierController.Instance
    End Function

    Function CreateCashTypeController() As CashType.ICashTypeController
        Return CashType.CashTypeController.Instance
    End Function
    Function CreateBranchController() As Branch.IBranchController
        Return Branch.BranchController.Instance
    End Function
    Public Function CreateWasteSetup() As WasteSetup.IWasteSetupController
        Return WasteSetup.WasteSetupController.Instance
    End Function
#Region " Daily Income Expense "
    Public Function CreateDailyExpenseController() As DailyExpense.IDailyExpenseController
        Return DailyExpense.DailyExpenseController.Instance
    End Function
    Public Function CreateDailyIncomeController() As DailyIncome.IDailyIncomeController
        Return DailyIncome.DailyIncomeController.Instance
    End Function
    Public Function CreateBarcodeSettingController() As Barcodesetting.IBarcodeSettingController
        Return Barcodesetting.BarcodeSettingController.Instance
    End Function
    Public Function CreateVoucherSettingController() As VoucherSetting.IVoucherSettingController
        Return VoucherSetting.VoucherSettingController.Instance
    End Function
    Public Function CreateReturnAdvanceController() As ReturnAdvance.IReturnAdvanceController
        Return ReturnAdvance.ReturnAdvanceController.Instance
    End Function
    Public Function CreateMortgageInvoiceController() As MortgageInvoice.IMortgageInvoiceController
        Return MortgageInvoice.MortgageInvoiceController.Instance
    End Function
    Public Function CreateKeyWordController() As Keyword.IKeywordController
        Return Keyword.KeywordController.Instance
    End Function
    Public Function CreateMortgageInterestController() As MortgageInterest.IMortgageInterestController
        Return MortgageInterest.MortgageInterestController.Instance
    End Function
    Public Function CreateMortgagePaybackController() As MortgagePayback.IMortgagePaybackController
        Return MortgagePayback.MortgagePaybackController.Instance
    End Function
    Public Function CreateMortgageReturnController() As MortgageReturn.IMortgageReturnController
        Return MortgageReturn.MortgageReturnController.Instance
    End Function

    Public Function CreateWholeSaleInvoiceController() As WholeSaleInvoice.IWholeSaleInvoiceController
        Return WholeSaleInvoice.WholeSaleInvoiceController.Instance
    End Function

    Public Function CreateWholeSaleReturnController() As WholeSaleReturn.IWholeSaleReturnController
        Return WholeSaleReturn.WholeSaleReturnController.Instance
    End Function
    Public Function CreateConsignmentSaleController() As ConsignmentSale.IConsignmentSaleController
        Return ConsignmentSale.ConsignmentSaleController.Instance
    End Function
#End Region
#Region " Database Export Import "
    Public Function CreateDatabaseExportImportController() As DatabaseExportImport.IDatabaseExportImportController
        Return DatabaseExportImport.DatabaseExportImportController.Instance
    End Function
#End Region
    Public Function CreateExportServiceLogsController() As ExportServiceLogs.IExportServiceLogsController
        Return ExportServiceLogs.ExportServiceLogsController.Instance
    End Function
#Region " Transfer Return "
    Public Function CreateTransferReturnController() As TransferReturn.ITransferReturnController
        Return TransferReturn.TransferReturnController.Instance
    End Function
#End Region
    Public Function CreateSalesItemController() As SalesItem.ISalesItemController
        Return SalesItem.SalesItemController.Instance
    End Function
    Public Function CreateCheckStockController() As CheckStock.ICheckStockController
        Return CheckStock.CheckStockController.Instance
    End Function
    Public Function CreateDashboardController() As Dashboard.IDashboardController
        Return Dashboard.DashboardController.Instance
    End Function
    Public Function CreateIntDiamondController() As InternationalDiamond.IIntDiamondPriceRateController
        Return InternationalDiamond.IntDiamondPriceRateController.Instance
    End Function
    Public Function CreateSaleLooseDiamondController() As SaleLooseDiamond.ISaleLooseDiamondController
        Return SaleLooseDiamond.SaleLooseDiamondController.Instance
    End Function
    Public Function CreateTransferDiamondController() As TransferDiamond.ITransferDiamondController
        Return TransferDiamond.TransferDiamondController.Instance
    End Function
    Public Function CreateTransferDiamondReturnController() As TransferDiamondReturn.ITransferDiamondReturnController
        Return TransferDiamondReturn.TransferDiamondReturnController.Instance()
    End Function
End Class
