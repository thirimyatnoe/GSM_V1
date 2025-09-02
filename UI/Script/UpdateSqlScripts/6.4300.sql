Update tbl_Version Set VersionNo='6.4300',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'IsCheck')
alter table tbl_ForSale add  IsCheck Bit NUll 
GO
Update tbl_ForSale Set IsCheck=0;
GO

