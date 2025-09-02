---------ForSale---------------------
Update tbl_Version Set VersionNo='4.0002' ,VersionDate=GETDATE();
GO
------ForSale---------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'WReturnDate')
Alter table tbl_ForSale ADD WReturnDate DateTime
GO
Update tbl_ForSale Set WReturnDate= Null
GO
-------Remove Foreign Key-------
--ALTER TABLE tbl_ForSale DROP CONSTRAINT FK_tbl_ForSale_tbl_GoldQuality
--ALTER TABLE tbl_ForSale DROP CONSTRAINT FK_tbl_ForSale_tbl_ItemCategory
--ALTER TABLE tbl_ForSale DROP CONSTRAINT FK_tbl_ForSale_tbl_ItemName
--ALTER TABLE tbl_ForSale DROP CONSTRAINT FK_tbl_ForSale_tbl_Location
--ALTER TABLE tbl_ForSaleGemsItem DROP CONSTRAINT FK_tbl_ForSaleGemsItem_tbl_ForSale
--ALTER TABLE tbl_ForSaleGemsItem DROP CONSTRAINT FK_tbl_ForSaleGemsItem_tbl_GemsCategory
--ALTER TABLE tbl_PurchaseGem DROP CONSTRAINT FK_tbl_PurchaseGem_tbl_GemsCategory
--ALTER TABLE tbl_PurchaseGem DROP CONSTRAINT FK_tbl_PurchaseGem_tbl_PurchaseDetail
--ALTER TABLE tbl_OrderReceiveDetail DROP CONSTRAINT FK_tbl_OrderReceiveDetail_tbl_GoldQuality
--ALTER TABLE tbl_OrderReceiveDetail DROP CONSTRAINT FK_tbl_OrderReceiveDetail_tbl_ItemCategory
--ALTER TABLE tbl_OrderReceiveDetail DROP CONSTRAINT FK_tbl_OrderReceiveDetail_tbl_ItemName
--ALTER TABLE tbl_OrderReceiveDetail DROP CONSTRAINT FK_tbl_OrderReceiveDetail_tbl_OrderInvoice
--ALTER TABLE tbl_OrderReturnGemsItem DROP CONSTRAINT FK_tbl_OrderReturnGemsItem_tbl_GemsCategory
--ALTER TABLE tbl_OrderReturnGemsItem DROP CONSTRAINT FK_tbl_OrderReturnGemsItem_tbl_OrderReturnDetail
--ALTER TABLE tbl_PurchaseDetail DROP CONSTRAINT FK_tbl_PurchaseDetail_tbl_GoldQuality
--ALTER TABLE tbl_PurchaseDetail DROP CONSTRAINT FK_tbl_PurchaseDetail_tbl_ItemCategory
--ALTER TABLE tbl_PurchaseDetail DROP CONSTRAINT FK_tbl_PurchaseDetail_tbl_ItemName
--ALTER TABLE tbl_PurchaseDetail DROP CONSTRAINT FK_tbl_PurchaseDetail_tbl_PurchaseHeader
--ALTER TABLE tbl_RepairDetail DROP CONSTRAINT FK_tbl_RepairDetail_tbl_GoldQuality
--ALTER TABLE tbl_RepairDetail DROP CONSTRAINT FK_tbl_RepairDetail_tbl_RepairHeader
--ALTER TABLE tbl_SalesVolumeDetail DROP CONSTRAINT FK_tbl_SalesVolumeDetail_tbl_GoldQuality
--ALTER TABLE tbl_SalesVolumeDetail DROP CONSTRAINT FK_tbl_SalesVolumeDetail_tbl_ItemCategory
--ALTER TABLE tbl_SalesVolumeDetail DROP CONSTRAINT FK_tbl_SalesVolumeDetail_tbl_ItemName
--ALTER TABLE tbl_SalesVolumeDetail DROP CONSTRAINT FK_tbl_SaleVolumeDetail_tbl_SaleVolume
--ALTER TABLE tbl_OrderInvoice DROP CONSTRAINT FK_tbl_OrderInvoice_tbl_Customer
--ALTER TABLE tbl_OrderInvoice DROP CONSTRAINT FK_tbl_OrderInvoice_tbl_Location
--ALTER TABLE tbl_OrderInvoice DROP CONSTRAINT FK_tbl_OrderInvoice_tbl_Staff
--ALTER TABLE tbl_OrderReturnHeader DROP CONSTRAINT FK_tbl_OrderReturnHeader_tbl_Location
--ALTER TABLE tbl_OrderReturnHeader DROP CONSTRAINT FK_tbl_OrderReturnHeader_tbl_Staff
--ALTER TABLE tbl_PurchaseFromSupplier DROP CONSTRAINT FK_tbl_PurchaseFromSupplier_tbl_Location
--ALTER TABLE tbl_PurchaseFromSupplier DROP CONSTRAINT FK_tbl_PurchaseFromSupplier_tbl_PurchaseFromSupplier
--ALTER TABLE tbl_PurchaseFromSupplier DROP CONSTRAINT FK_tbl_PurchaseFromSupplier_tbl_Staff
--ALTER TABLE tbl_PurchaseHeader DROP CONSTRAINT FK_tbl_PurchaseHeader_tbl_Customer
--ALTER TABLE tbl_PurchaseHeader DROP CONSTRAINT FK_tbl_PurchaseHeader_tbl_Location
--ALTER TABLE tbl_PurchaseHeader DROP CONSTRAINT FK_tbl_PurchaseHeader_tbl_Staff
--ALTER TABLE tbl_RepairHeader DROP CONSTRAINT FK_tbl_RepairHeader_tbl_Customer
--ALTER TABLE tbl_RepairHeader DROP CONSTRAINT FK_tbl_RepairHeader_tbl_Location
--ALTER TABLE tbl_RepairHeader DROP CONSTRAINT FK_tbl_RepairHeader_tbl_Staff
--ALTER TABLE tbl_ReturnRepairHeader DROP CONSTRAINT FK_tbl_ReturnRepairHeader_tbl_Staff
--ALTER TABLE tbl_SaleGems DROP CONSTRAINT FK_tbl_SaleGems_tbl_Customer
--ALTER TABLE tbl_SaleGems DROP CONSTRAINT FK_tbl_SaleGems_tbl_Location
--ALTER TABLE tbl_SaleGems DROP CONSTRAINT FK_tbl_SaleGems_tbl_Staff
--ALTER TABLE tbl_SaleInvoiceHeader DROP CONSTRAINT FK_tbl_SaleInvoiceHeader_tbl_Staff
--ALTER TABLE tbl_SaleInvoiceHeader DROP CONSTRAINT FK_tbl_SaleInvoiceHeader_tbl_Customer
--ALTER TABLE tbl_SaleInvoiceHeader DROP CONSTRAINT FK_tbl_SaleInvoiceHeader_tbl_Location
--ALTER TABLE tbl_SalesVolume DROP CONSTRAINT FK_tbl_SalesVolume_tbl_Location
--ALTER TABLE tbl_SalesVolume DROP CONSTRAINT FK_tbl_SalesVolume_tbl_Customer
--ALTER TABLE tbl_SalesVolume DROP CONSTRAINT FK_tbl_SalesVolume_tbl_Staff
--ALTER TABLE tbl_ReturnRepairGem DROP CONSTRAINT FK_tbl_ReturnRepairGem_tbl_GemsCategory
--ALTER TABLE tbl_ReturnRepairGem DROP CONSTRAINT FK_tbl_ReturnRepairGem_tbl_ReturnRepairDetail
--ALTER TABLE tbl_SaleGemsItem DROP CONSTRAINT FK_tbl_SaleGemsItem_tbl_GemsCategory
--ALTER TABLE tbl_SaleGemsItem DROP CONSTRAINT FK_tbl_SaleGemsItem_tbl_SaleGems
--ALTER TABLE tbl_SalesInvoiceGemItem DROP CONSTRAINT FK_tbl_SalesInvoiceGemItem_tbl_GemsCategory
--ALTER TABLE tbl_SalesInvoiceGemItem DROP CONSTRAINT FK_tbl_SalesInvoiceGemItem_tbl_SaleInvoiceDetail
--ALTER TABLE tbl_OrderReturnDetail DROP CONSTRAINT FK_tbl_OrderReturnDetail_tbl_OrderReturnHeader
GO