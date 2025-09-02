Update tbl_Version Set VersionNo='5.6000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'IsFixPrice')
alter table tbl_BarcodeSetting add  IsFixPrice Bit NUll 
GO
Update tbl_BarcodeSetting Set IsFixPrice=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'LeftFontSize')
alter table tbl_BarcodeSetting add  LeftFontSize int NUll 
GO
Update tbl_BarcodeSetting Set LeftFontSize=19;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'RightFontSize')
alter table tbl_BarcodeSetting add  RightFontSize Bit NUll 
GO
Update tbl_BarcodeSetting Set RightFontSize=19;
GO

