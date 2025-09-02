Update tbl_Version Set VersionNo='5.2000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GoldQuality' and column_name = 'BarcodeStatus')
alter table tbl_GoldQuality add  BarcodeStatus Integer NUll 
GO
Update tbl_GoldQuality Set BarcodeStatus=0;
GO
