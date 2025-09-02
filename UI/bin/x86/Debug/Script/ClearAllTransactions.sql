if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader')
Delete From tbl_SaleInvoiceHeader
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceDetail')
Delete From tbl_SaleInvoiceDetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesInvoiceGemItem')
Delete From tbl_SalesInvoiceGemItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale')
Delete From tbl_ConsignmentSale
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSaleItem')
Delete From tbl_ConsignmentSaleItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesOrder')
Delete From tbl_SalesOrder
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesOrderGemsItem')
Delete From tbl_SalesOrderGemsItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleReturn')
Delete From tbl_WholesaleReturn
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleReturnItem')
Delete From tbl_WholesaleReturnItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice')
Delete From tbl_WholesaleInvoice
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoiceItem')
Delete From tbl_WholesaleInvoiceItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume')
Delete From tbl_SalesVolume
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolumeDetail')
Delete From tbl_SalesVolumeDetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGems')
Delete From tbl_SaleGems
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGemsItem')
Delete From tbl_SaleGemsItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ReturnAdvance')
Delete From tbl_ReturnAdvance
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ReturnAdvanceItem')
Delete From tbl_ReturnAdvanceItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ReturnRepairHeader')
Delete From tbl_ReturnRepairHeader
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ReturnRepairDetail')
Delete From tbl_ReturnRepairDetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ReturnRepairGem')
Delete From tbl_ReturnRepairGem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_RepairHeader')
Delete From tbl_RepairHeader
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_RepairDetail')
Delete From tbl_RepairDetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseOutItem')
Delete From tbl_PurchaseOutItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseOutItemDetail')
Delete from tbl_PurchaseOutItemDetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseHeader')
Delete From tbl_PurchaseHeader
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail')
Delete From tbl_PurchaseDetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseGem')
Delete From tbl_PurchaseGem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseFromSupplier')
Delete From tbl_PurchaseFromSupplier
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseFromSupplierItem')
Delete From tbl_PurchaseFromSupplierItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_OrderInvoice')
Delete From tbl_OrderInvoice
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_OrderReceiveDetail')
Delete From tbl_OrderReceiveDetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_OrderInvoiceGemsItem')
Delete From tbl_OrderInvoiceGemsItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_orderreturnheader')
Delete From tbl_orderreturnheader
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_orderreturndetail')
Delete From tbl_orderreturndetail
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_OrderReturnGemsItem')
Delete From tbl_OrderReturnGemsItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageInvoice')
Delete From tbl_MortgageInvoice
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageInterest')
Delete From tbl_MortgageInterest
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageInvoiceItem')
Delete From tbl_MortgageInvoiceItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgagePayback')
Delete From tbl_MortgagePayback
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_cashreceipt')
Delete From tbl_cashreceipt
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_forsale')
Delete From tbl_forsale
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_forsalegemsitem')
Delete From tbl_forsalegemsitem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_dailyexpense')
Delete From tbl_dailyexpense
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_dailyincome')
Delete From tbl_dailyincome
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_TransferReturn')
Delete From tbl_TransferReturn
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_TransferReturnItem')
Delete From tbl_TransferReturnItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Transfer')
Delete From tbl_Transfer
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_TransferItem')
Delete From tbl_TransferItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgagePaybackItem')
Delete From tbl_MortgagePaybackItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageReturn')
Delete From tbl_MortgageReturn
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageReturnItem')
Delete From tbl_MortgageReturnItem
GO
if exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tb_GE_Eventlogs')
Delete From tb_GE_Eventlogs
GO
Delete From tbl_Key_Generate Where GenerateKeyType='SaleInvoiceDetail'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='SaleStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='CashReceiptOnCredit'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='ForSale'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='MortgageInterest'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='MortgageInvoice'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='MortgageInvoiceItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='MortgagePayback'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='MortgagePaybackItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='PurchaseDetail'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='PurchaseStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='RepairDetail'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='RepairReturnDetail'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='RepairReturnHeader'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='RepairStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='SalesVolumeDetail'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='SaleVolumeStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='Transfer'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='TransferItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='TransferReturn'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='TransferReturnItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='WholeSaleInvoiceItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='WholeSaleReturnItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='WholeSaleReturnStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='WholeSaleStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='SaleInvoiceGemItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='SalesGem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='ReturnAdvance'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='ReturnAdvanceItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='ConsignmentSaleItem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='ConsignmentSaleStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='CheckStock'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='PurchaseGem'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='MortgageReturn'
GO
Delete From tbl_Key_Generate Where GenerateKeyType='MortgageReturnItem'
GO