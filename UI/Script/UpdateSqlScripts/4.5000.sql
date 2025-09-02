---TransferReturn---
Update tbl_Version Set VersionNo='4.5000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_TransferItem' and column_name = 'isReturn')
alter table tbl_TransferItem add isReturn  Bit Null 
GO
update tbl_TransferItem set isReturn=0;
GO