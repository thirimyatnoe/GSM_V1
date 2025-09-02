---WholeSaleRemark---
Update tbl_Version Set VersionNo='4.4000',VersionDate=GETDATE();
GO
---Return AdvanceID -----
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_CashReceipt' and column_name = 'ReturnAdvanceID')
alter table tbl_CashReceipt add  ReturnAdvanceID varchar(50) NUll 
GO
Update tbl_CashReceipt Set ReturnAdvanceID='';
GO