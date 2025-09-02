---------Synchronization---------------------
Update tbl_Version Set VersionNo='4.1000',VersionDate=GETDATE();
GO
------Location---------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Location' and column_name = 'CurrentLocationID')
alter table tbl_Location add CurrentLocationID varchar(20) NULL
GO
update tbl_Location set CurrentLocationID='01'
GO
------Customer--------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Customer' and column_name = 'LocationID')
alter table tbl_Customer add LocationID varchar(20) NULL
GO
update tbl_Customer set LocationID='01'
GO
------Supplier--------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Supplier' and column_name = 'LocationID')
alter table tbl_Supplier add LocationID varchar(20) NULL
GO
update tbl_Supplier set LocationID='01'
GO
-----Staff-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Staff' and column_name = 'LocationID')
alter table tbl_Staff add LocationID varchar(20) NULL
GO
update tbl_Staff set LocationID='01'
GO
-----CashType-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_CashType' and column_name = 'LocationID')
alter table tbl_CashType add LocationID varchar(20) NULL
GO
update tbl_CashType set LocationID='01'
GO
-----GOldQuality-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GoldQuality' and column_name = 'LocationID')
alter table tbl_GoldQuality add LocationID varchar(20) NULL
GO
update tbl_GoldQuality set LocationID='01'
GO
-----ItemCateGOry-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ItemCategory' and column_name = 'LocationID')
alter table tbl_ItemCategory add LocationID varchar(20) NULL
GO
update tbl_ItemCategory set LocationID='01'
GO
-----ItemName-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ItemName' and column_name = 'LocationID')
alter table tbl_ItemName add LocationID varchar(20) NULL
GO
update tbl_ItemName set LocationID='01'
GO
-----GemsCateGory-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GemsCateGory' and column_name = 'LocationID')
alter table tbl_GemsCateGory add LocationID varchar(20) NULL
GO
update tbl_GemsCateGory set LocationID=''
GO
-----WasteSetup-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WasteSetupHeader' and column_name = 'IsUpload')
alter table tbl_WasteSetupHeader add IsUpload Bit NULL
GO
update tbl_WasteSetupHeader set IsUpload=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WasteSetupHeader' and column_name = 'IsDelete')
alter table tbl_WasteSetupHeader add IsDelete Bit NULL
GO
update tbl_WasteSetupHeader set IsDelete=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WasteSetupHeader' and column_name = 'LocationID')
alter table tbl_WasteSetupHeader add LocationID varchar(20) NULL
GO
update tbl_WasteSetupHeader set LocationID='01'
GO
---CurrentPrice---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_StandardRate' and column_name = 'LocationID')
alter table tbl_StandardRate add LocationID varchar(20) NULL
GO
update tbl_StandardRate set LocationID='01'
GO
---tbl_ReturnRepairHeader---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ReturnRepairHeader' and column_name = 'LocationID')
alter table tbl_ReturnRepairHeader add LocationID varchar(20) NULL
GO
update tbl_ReturnRepairHeader set LocationID='01'
GO
---tbl_PurchaseDetail---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'ConsignmentSaleItemID')
alter table tbl_PurchaseDetail add ConsignmentSaleItemID varchar(20) NULL
GO
update tbl_PurchaseDetail set ConsignmentSaleItemID=''
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ExportData' )
CREATE TABLE [dbo].[tbl_ExportData](
	[ExportID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [varchar](20) NULL,
	[OtherLocationID] [varchar](20) NULL,
	[LocationName] [nvarchar](100) NULL,
	[OtherLocationName] [nvarchar](100) NULL,
	[TransactionType] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[AllUse] [bit] NULL,
 CONSTRAINT [PK_tbl_ExportData] PRIMARY KEY CLUSTERED 
(
	[ExportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ExportServiceLogs' )
CREATE TABLE [dbo].[tbl_ExportServiceLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[logdatetime] [datetime] NULL,
	[logtype] [varchar](20) NULL,
	[synctype] [varchar](20) NULL,
	[status] [varchar](20) NULL,
	[ExportID] [varchar](20) NULL,
	[UploadBranchID] [varchar](20) NULL,
	[FailBranchID] [varchar](20) NULL,
	[ExportFilePath] [varchar](200) NULL,
	[message] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO