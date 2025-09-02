Update tbl_Version Set VersionNo='6.4600',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'DisPercent')
alter table tbl_WholesaleInvoice add  DisPercent Int NUll 
GO
Update tbl_WholesaleInvoice Set DisPercent=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoiceItem' and column_name = 'ItemDisPercent')
alter table tbl_WholesaleInvoiceItem add  ItemDisPercent Int NUll 
GO
Update tbl_WholesaleInvoiceItem Set ItemDisPercent=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoiceItem' and column_name = 'ItemDisAmount')
alter table tbl_WholesaleInvoiceItem add  ItemDisAmount Int NUll 
GO
Update tbl_WholesaleInvoiceItem Set ItemDisAmount=0;
GO

