Update tbl_Version Set VersionNo='6.4500',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ExportData' and column_name = 'CompanyName')
alter table tbl_ExportData add  CompanyName nVarchar(500) NUll 
GO
Update tbl_ExportData Set CompanyName='-';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ExportData' and column_name = 'ToMail')
alter table tbl_ExportData add  ToMail Varchar(300) NUll 
GO
Update tbl_ExportData Set ToMail='pos.service@globalwave.com.mm';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ExportData' and column_name = 'CCMail')
alter table tbl_ExportData add  CCMail Varchar(300) NUll 
GO
Update tbl_ExportData Set CCMail='pos.service@globalwave.com.mm';
GO

