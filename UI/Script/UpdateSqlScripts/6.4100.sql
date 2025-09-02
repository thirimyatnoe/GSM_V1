Update tbl_Version Set VersionNo='6.4100',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgagePaybackItem' and column_name = 'NetAmount')
alter table tbl_MortgagePaybackItem add  NetAmount Integer NULL
GO
Update tbl_MortgagePaybackItem Set NetAmount=0;
GO

