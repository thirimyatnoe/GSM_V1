---------Synchronization---------------------
Update tbl_Version Set VersionNo='4.2000',VersionDate=GETDATE();
GO
------ForSale---------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'WReturnDate')
alter table tbl_ForSale add WReturnDate DateTime NULL
GO
update tbl_ForSale set WReturnDate=Null;
GO
------ConsignmentSale---------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'PurchaseHeaderID')
alter table tbl_ConsignmentSale add PurchaseHeaderID varchar(20) NULL
GO
update tbl_ConsignmentSale set PurchaseHeaderID='';
GO
------ConsignmentSale---------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'PurchaseAmount')
alter table tbl_ConsignmentSale add PurchaseAmount BigInt NULL
GO
update tbl_ConsignmentSale set PurchaseAmount=0;
GO
