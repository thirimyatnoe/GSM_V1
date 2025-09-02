---TransferReturn---
Update tbl_Version Set VersionNo='4.6000',VersionDate=GETDATE();
GO
-----GlobalSetting------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsUsedSettingPeriod')
alter table tbl_GlobalSetting add IsUsedSettingPeriod Bit NULL
GO
update tbl_GlobalSetting set IsUsedSettingPeriod=0;
GO