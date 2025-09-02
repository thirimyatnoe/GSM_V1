Update tbl_Version Set VersionNo='6.2000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsHoToBranch')
alter table tbl_GlobalSetting add  IsHoToBranch Bit NULL
GO
Update tbl_GlobalSetting Set IsHoToBranch=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageInterest' and column_name = 'InterestMonth')
alter table tbl_MortgageInterest add  InterestMonth Integer NULL
GO
Update tbl_MortgageInterest Set InterestMonth=0;
GO
