Update tbl_Version Set VersionNo='5.8000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'LeftFontSize')
alter table tbl_BarcodeSetting add  LeftFontSize Int NUll 
GO
Update tbl_BarcodeSetting Set LeftFontSize=19;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'RightFontSize')
alter table tbl_BarcodeSetting add  RightFontSize Int NUll 
GO
Update tbl_BarcodeSetting Set RightFontSize=19;
GO


