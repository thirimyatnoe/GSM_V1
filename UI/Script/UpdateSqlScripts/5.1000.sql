Update tbl_Version Set VersionNo='5.1000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_supplier' and column_name = 'LastModifiedDate')
alter table tbl_supplier add  LastModifiedDate Datetime NUll 
GO
Update tbl_supplier Set LastModifiedDate=GetDate();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_staff' and column_name = 'LastModifiedDate')
alter table tbl_staff add  LastModifiedDate Datetime NUll 
GO
Update tbl_staff Set LastModifiedDate=GetDate();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_StandardRate' and column_name = 'LastModifiedDate')
alter table tbl_StandardRate add  LastModifiedDate Datetime NUll 
GO
Update tbl_StandardRate Set LastModifiedDate=GetDate();
GO