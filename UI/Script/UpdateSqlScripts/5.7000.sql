Update tbl_Version Set VersionNo='5.7000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'SellingRate')
alter table tbl_ForSale add  SellingRate Int NUll 
GO
Update tbl_ForSale Set SellingRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceDetail' and column_name = 'SellingRate')
alter table tbl_SaleInvoiceDetail add  SellingRate Int NUll 
GO
Update tbl_SaleInvoiceDetail Set SellingRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceDetail' and column_name = 'SellingAmt')
alter table tbl_SaleInvoiceDetail add  SellingAmt bigInt NUll 
GO
Update tbl_SaleInvoiceDetail Set SellingAmt=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GeneralLedgerByLocation' and column_name = 'LastModifiedDate')
alter table tbl_GeneralLedgerByLocation add  LastModifiedDate datetime NUll 
GO
Update tbl_GeneralLedgerByLocation Set LastModifiedDate=getdate();
GO
ALTER TABLE dbo.tbl_GeneralLedgerBYLocation ADD tempID int NULL;
GO
UPDATE tbl_GeneralLedgerBYLocation SET tempID=GLByLocationID;
GO
ALTER TABLE dbo.tbl_GeneralLedgerBYLocation DROP COLUMN GLByLocationID;
GO
ALTER TABLE dbo.tbl_GeneralLedgerBYLocation ADD GLByLocationID Varchar(20) NULL;
GO
UPDATE dbo.tbl_GeneralLedgerBYLocation SET GLByLocationID=tempID;
GO
ALTER TABLE dbo.tbl_GeneralLedgerBYLocation ALTER COLUMN GLByLocationID varchar(20) NOT NULL;
GO
ALTER TABLE dbo.tbl_GeneralLedgerBYLocation DROP COLUMN tempID;
GO

