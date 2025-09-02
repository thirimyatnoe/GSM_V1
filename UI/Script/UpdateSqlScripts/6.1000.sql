Update tbl_Version Set VersionNo='6.1000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Customer' and column_name = 'NRC')
alter table tbl_Customer add  NRC nvarchar(40) NULL
GO
Update tbl_Customer Set NRC='-';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsOneMonthCalculation')
alter table tbl_GlobalSetting add  IsOneMonthCalculation Bit NULL
GO
Update tbl_GlobalSetting Set IsOneMonthCalculation=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageInvoiceItem' and column_name = 'isPayback')
alter table tbl_MortgageInvoiceItem add  isPayback Bit NULL
GO
Update tbl_MortgageInvoiceItem Set isPayback=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgagePayback' and column_name = 'TotalAmouunt')
alter table tbl_MortgagePayback add  TotalAmount Int NULL
GO
Update tbl_MortgagePayback Set TotalAmount=0;
GO
SET ANSI_NULLS ON
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_MortgagePaybackItem' )
CREATE TABLE [dbo].[tbl_MortgagePaybackItem](
	[MortgagePaybackItemID] [varchar](20) NOT NULL,
	[MortgagePaybackID] [varchar](20) NOT NULL,
	[MortgageItemID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[ItemCategoryID] [nvarchar](20) NULL,
	[ItemName] [nvarchar](100) NULL,
	[GoldTK] [Numeric] (18,13) NULL,
	[GoldTG] [Numeric] (18,13) NULL,
	[Amount] [int] NULL,
	[MortgageRate] [int] NULL,
	[IsDone] [Bit] NULL,
	[DonePercent] [int] NULL
 CONSTRAINT [PK_tbl_MortgagePaybackItem] PRIMARY KEY CLUSTERED 
(
	[MortgagePaybackItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgagePayback' and column_name = 'isDelete')
alter table tbl_MortgagePayback add  isDelete Bit NULL
GO
Update tbl_MortgagePayback Set isDelete=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Transfer' and column_name = 'IsConfirm')
alter table tbl_Transfer add  IsConfirm Bit NULL
GO
Update tbl_Transfer Set IsConfirm=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'OverDay')
alter table tbl_GlobalSetting add  OverDay Integer NULL
GO
Update tbl_GlobalSetting Set OverDay=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_MortgageInvoiceItem' and column_name = 'MortgageItemCode')
alter table tbl_MortgageInvoiceItem add  MortgageItemCode Varchar(40) NULL
GO
Update tbl_MortgageInvoiceItem Set MortgageItemCode=0;
GO