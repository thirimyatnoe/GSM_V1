Update tbl_Version Set VersionNo='4.9000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'AllowStockWeight')
alter table tbl_GlobalSetting add  AllowStockWeight Decimal(18,3) NUll 
GO
Update tbl_GlobalSetting Set AllowStockWeight=0.0;
GO