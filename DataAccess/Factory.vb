
Public Class Factory

    Private Shared ReadOnly _instance As Factory = New Factory()

    Private Sub New()

    End Sub

    Public Shared ReadOnly Property Instance() As Factory
        Get
            Return _instance
        End Get
    End Property

    Public Function CreateGeneralDA() As General.IGeneralDA
        Return General.GeneralDA.Instance()
    End Function
    Public Function CreateGoldQualityDA() As GoldQuality.IGoldQualityDA
        Return GoldQuality.GoldQualityDA.Instance
    End Function
    Public Function CreateLocationDA() As Location.ILocationDA
        Return Location.LocationDA.Instance
    End Function
    Public Function CreateCurrentPriceDA() As CurrentPrice.ICurrentPriceDA
        Return CurrentPrice.CurrentPriceDA.Instance
    End Function
    Public Function CreateConverterDA() As Converter.IConverterDA
        Return Converter.ConverterDA.Instance
    End Function
    Public Function CreateItemCategoryDA() As ItemCategory.IItemCategoryDA
        Return ItemCategory.ItemCategoryDA.Instance
    End Function
    Public Function CreateGemsCategoryDA() As GemsCategory.IGemsCategoryDA
        Return GemsCategory.GemsCategoryDA.Instance
    End Function

    Public Function CreateExportDataDA() As ExportData.IExportDataDA
        Return ExportData.ExportDataDA.Instance
    End Function

    'Public Function CreateDamageDA() As Damage.IDamageDA
    '    Return Damage.DamageDA.Instance
    'End Function
    Public Function CreateStaffDA() As Staff.IStaffDA
        Return Staff.StaffDA.Instance
    End Function
    Public Function CreateSalesItemDA() As SalesItem.ISalesItemDA
        Return SalesItem.SalesItemDA.Instance
    End Function
    Public Function CreatePurchaseGemsDA() As PurchaseGems.IPurchaseGemsDA
        Return PurchaseGems.PurchaseGemsDA.Instance
    End Function
    'Public Function CreatePurchaseInvoiceDA() As PurchaseInvoice.IPurchaseInvoiceDA
    '    Return PurchaseInvoice.PurchaseInvoiceDA.Instance
    'End Function
    Public Function CreateMeasurementDA() As Measurement.IMeasurementDA
        Return Measurement.MeasurementDA.Instance
    End Function
    'Public Function CreateSalesInvoiceDA() As SalesInvoice.ISalesInvoiceDA
    '    Return SalesInvoice.SalesInvoiceDA.Instance
    'End Function
    Public Function CreateSaleGemsDA() As SaleGems.ISaleGemsDA
        Return SaleGems.SaleGemsDA.Instance
    End Function
    Public Function CreateReturnAdvanceDA() As ReturnAdvance.IReturnAdvanceDA
        Return ReturnAdvance.ReturnAdvanceDA.Instance
    End Function
    Public Function CreateOrderInvoiceDA() As OrderInvoice.IOrderInvoiceDA
        Return OrderInvoice.OrderInvoiceDA.Instance
    End Function
    'Public Function CreateSalesOrderDA() As SalesOrder.ISalesOrderDA
    '    Return SalesOrder.SalesOrderDA.Instance
    'End Function
    Public Function CreateItemName() As ItemName.IItemNameDA
        Return ItemName.ItemNameDA.Instance
    End Function
    Public Function CreateCustomerDA() As Customer.ICustomerDA
        Return Customer.CustomerDA.Instance
    End Function
    Public Function CreateGoldSmithDA() As GoldSmith.IGoldSmithDA
        Return GoldSmith.GoldSmithDA.Instance
    End Function

    Public Function CreateGeneralLedgerByLocationDA() As GeneralLedgerByLocation.IGeneralLedgerByLocationDA
        Return GeneralLedgerByLocation.GeneralLedgerByLocationDA.Instance
    End Function
    Public Function CreateTransferDA() As Transfer.ITransferDA
        Return Transfer.TransferDA.Instance
    End Function
    Public Function CreateCashReceiptDA() As CashReceipt.ICashReceiptDA
        Return CashReceipt.CashReceiptDA.Instance
    End Function
    Public Function CreateWasteSetup() As WasteSetup.IWasteSetupDA
        Return WasteSetup.WasteSetupDA.Instance
    End Function
    Public Function CreatePhotoPathDA() As PhotoPath.IPhotoPathDA
        Return PhotoPath.PhotoPathDA.Instance
    End Function
    Public Function CreatePurchaseItemDA() As PurchaseItem.IPurchaseItemDA
        Return PurchaseItem.PurchaseItemDA.Instance
    End Function

    Public Function CreateSalesItemInvoiceDA() As SalesItemInvoice.ISalesItemInvoiceDA
        Return SalesItemInvoice.SalesItemInvoiceDA.Instance
    End Function
    Public Function CreateSalesVolumeDA() As SalesVolume.ISalesVolumeDA
        Return SalesVolume.SalesVolumeDA.Instance
    End Function
    Function CreateGenerateFormatDA() As GenerateFormat.IGenerateFormatDA
        Return GenerateFormat.GenerateFormatDA.Instance
    End Function
    Function CreateBarCodeSettingDA() As BarcodeSetting.IBarcodeSettingDA
        Return BarcodeSetting.BarCodeSettingDA.Instance
    End Function

    Function CreateVoucherSettingDA() As VoucherSetting.IVoucherSettingDA
        Return VoucherSetting.VoucherSettingDA.Instance
    End Function
    Function CreateGlobalSettingDA() As GlobalSetting.IGlobalSettingDA
        Return GlobalSetting.GlobalSettingDA.Instance
    End Function
    Function CreateCustomReportDA() As CustomReport.ICustomReportDA
        Return CustomReport.CustomReportDA.Instance
    End Function
    Function CreateCompanyProfileDA() As CompanyProfile.ICompanyProfileDA
        Return CompanyProfile.CompanyProfileDA.Instance
    End Function
    Public Function CreateSettingDA() As Setting.ISettingDA
        Return Setting.SettingDA.Instance()
    End Function

    Function CreateRepairDA() As Repair.IRepairDA
        Return Repair.RepairDA.Instance
    End Function
    Function CreateRepairReturnDA() As RepairReturn.IRepairReturnDA
        Return RepairReturn.RepairReturnDA.Instance
    End Function
    Function CreateSupplierDA() As Supplier.ISupplierDA
        Return Supplier.SupplierDA.Instance
    End Function
    Function CreateCashTypeDA() As CashType.ICashTypeDA
        Return CashType.CashTypeDA.Instance
    End Function
    Function CreateBranchDA() As Branch.IBranchDA
        Return Branch.BranchDA.Instance
    End Function
    Public Function CreateMortgageInvoiceDA() As MortgageInvoice.IMortgageInvoiceDA
        Return MortgageInvoice.MortgageInvoiceDA.Instance
    End Function
    Public Function CreateKeyWordDA() As Keyword.IKeywordDA
        Return Keyword.KeywordDA.Instance
    End Function
    Public Function CreateMortgageInterestDA() As MortgageInterest.IMortgageInterestDA
        Return MortgageInterest.MortgageInterestDA.Instance
    End Function
    Public Function CreateMortgagePaybackDA() As MortgagePayback.IMortgagePaybackDA
        Return MortgagePayback.MortgagePaybackDA.Instance
    End Function
    Public Function CreateMortgageReturnDA() As MortgageReturn.IMortgageReturnDA
        Return MortgageReturn.MortgageReturnDA.Instance
    End Function
    Public Function CreateWholeSaleInvoiceDA() As WholeSaleInvoice.IWholeSaleInvoiceDA
        Return WholeSaleInvoice.WholeSaleInvoiceDA.Instance
    End Function
    Public Function CreateWholeSaleReturnDA() As WholeSaleReturn.IWholeSaleReturnDA
        Return WholeSaleReturn.WholeSaleReturnDA.Instance
    End Function
    Public Function CreateConsignmentSaleDA() As ConsignmentSale.IConsignmentSaleDA
        Return ConsignmentSale.ConsignmentSaleDA.Instance
    End Function
     Public Function CreateExportServiceLogsDA() As ExportServiceLogs.IExportServiceLogsDA
       
        Return ExportServiceLogs.ExportServiceLogsDA.Instance
    End Function

   

#Region " Daily Income Expense "
    Public Function CreateDailyExpenseDA() As DailyExpense.IDailyExpenseDA
        Return DailyExpense.DailyExpenseDA.Instance
    End Function
    Public Function CreateDailyIncomeDA() As DailyIncome.IDailyIncomeDA
        Return DailyIncome.DailyIncomeDA.Instance
    End Function
#End Region

#Region " Database Export Import "
    Public Function CreateDatabaseExportImportDA() As DatabaseExportImport.IDatabaseExportImportDA
        Return DatabaseExportImport.DatabaseExportImportDA.Instance
    End Function
#End Region

#Region " Transfer Return "
    Public Function CreateTransferReturnDA() As TransferReturn.ITransferReturnDA
        Return TransferReturn.TransferReturnDA.Instance
    End Function
#End Region
    Public Function CreateCheckStockDA() As CheckStock.ICheckStockDA
        Return CheckStock.CheckStockDA.Instance
    End Function
    Public Function CreateDashboardDA() As Dashboard.IDashboardDA
        Return Dashboard.DashboardDA.Instance
    End Function
    Public Function CreateIntDiamondDA() As InternationalDiamond.IIntDiamondPriceRateDA
        Return InternationalDiamond.IntDiamondPriceRateDA.Instance
    End Function
    Public Function CreateSaleLooseDiamondDA() As SaleLooseDiamond.ISaleLooseDiamondDA
        Return SaleLooseDiamond.SaleLooseDiamondDA.Instance
    End Function
    Public Function CreateTransferDiamondDA() As TransferDiamond.ITransferDiamondDA
        Return TransferDiamond.TransferDiamondDA.Instance
    End Function
    Public Function CreateTransferDiamondReturnDA() As TransferDiamondReturn.ITransferDiamondReturnDA
        Return TransferDiamondReturn.TransferDiamondReturnDA.Instance
    End Function
End Class
