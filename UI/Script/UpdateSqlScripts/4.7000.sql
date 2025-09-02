Update tbl_Version Set VersionNo='4.7000',VersionDate=GETDATE();
GO
-----GoldSmith------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GoldSmith' and column_name = 'LocationID')
alter table tbl_GoldSmith add LocationID varchar(20) NULL
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GoldSmith' and column_name = 'LocationID')
update tbl_GoldSmith set LocationID=0;
GO