Update tbl_Version Set VersionNo='6.4400',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'SoftWareVendorSetting')
alter table tbl_GlobalSetting add  SoftWareVendorSetting Bit NUll 
GO
Update tbl_GlobalSetting Set SoftWareVendorSetting=0;
GO

