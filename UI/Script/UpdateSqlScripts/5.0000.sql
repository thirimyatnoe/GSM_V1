Update tbl_Version Set VersionNo='5.0000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GoldQuality' and column_name = 'LastModifiedDate')
alter table tbl_GoldQuality add  LastModifiedDate Datetime NUll 
GO
Update tbl_GoldQuality Set LastModifiedDate=GetDate();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ItemName' and column_name = 'LastModifiedDate')
alter table tbl_ItemName add  LastModifiedDate Datetime NUll 
GO
Update tbl_ItemName Set LastModifiedDate=GetDate();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GemsCategory' and column_name = 'LastModifiedDate')
alter table tbl_GemsCategory add  LastModifiedDate Datetime NUll 
GO
Update tbl_GemsCategory Set LastModifiedDate=GetDate();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WasteSetupHeader' and column_name = 'LastModifiedDate')
alter table tbl_WasteSetupHeader add  LastModifiedDate Datetime NUll 
GO
Update tbl_WasteSetupHeader Set LastModifiedDate=GetDate();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ItemCategory' and column_name = 'LastModifiedDate')
alter table tbl_ItemCategory add  LastModifiedDate Datetime NUll 
GO
Update tbl_ItemCategory Set LastModifiedDate=GetDate();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Location' and column_name = 'LastModifiedDate')
alter table tbl_Location add  LastModifiedDate Datetime NUll 
GO
Update tbl_Location Set LastModifiedDate=GetDate();
GO