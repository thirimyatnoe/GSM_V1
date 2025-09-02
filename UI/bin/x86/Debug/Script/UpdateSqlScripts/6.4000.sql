Update tbl_Version Set VersionNo='6.4000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'MachineType')
alter table tbl_GlobalSetting add  MachineType varchar(20) NULL
GO
Update tbl_GlobalSetting Set MachineType='Default';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'Prefix')
alter table tbl_GlobalSetting add  Prefix Integer NULL
GO
Update tbl_GlobalSetting Set Prefix=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'Postfix')
alter table tbl_GlobalSetting add  Postfix Integer NULL
GO
Update tbl_GlobalSetting Set Postfix=0;
GO

