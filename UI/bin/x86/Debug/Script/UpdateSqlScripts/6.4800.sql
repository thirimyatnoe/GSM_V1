Update tbl_Version Set VersionNo='6.4800',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_DiamondPriceRate' and column_name = 'PurchaseRate')
alter table tbl_DiamondPriceRate add  PurchaseRate decimal(19, 1)
GO
Update tbl_DiamondPriceRate Set PurchaseRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholeSaleInvoiceItem' and column_name = 'GemsPrice')
alter table tbl_WholeSaleInvoiceItem add  GemsPrice Int
GO
Update tbl_WholeSaleInvoiceItem Set GemsPrice=0;
GO