Update tbl_Version Set VersionNo='6.4200',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsHoMaster')
alter table tbl_GlobalSetting add  IsHoMaster Bit NUll 
GO
Update tbl_GlobalSetting Set IsHoMaster=1;
GO

