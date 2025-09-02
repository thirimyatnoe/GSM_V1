Update tbl_Version Set VersionNo='6.0000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Location' and column_name = 'IsHeadOffice')
alter table tbl_Location add  IsHeadOffice Bit NUll 
GO
Update tbl_Location Set IsHeadOffice=0;
GO

