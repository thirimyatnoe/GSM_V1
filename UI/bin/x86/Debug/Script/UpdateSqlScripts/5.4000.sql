Update tbl_Version Set VersionNo='5.4000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'IsSolidVolume')
alter table tbl_ForSale add  IsSolidVolume Bit NUll 
GO
Update tbl_ForSale Set IsSolidVolume=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_salesvolume' and column_name = 'IsSolidVolume')
alter table tbl_salesvolume add  IsSolidVolume Bit NUll 
GO
Update tbl_salesvolume Set IsSolidVolume=0;
GO
