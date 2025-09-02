Update tbl_Version Set VersionNo='6.3000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Transfer' and column_name = 'CurrentLocationID')
alter table tbl_Transfer add  CurrentLocationID varchar(20) NULL
GO
Update tbl_Transfer Set CurrentLocationID='';
GO
SET ANSI_NULLS ON
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_MortgageReturn' )
CREATE TABLE [dbo].[tbl_MortgageReturn](
	[MortgageReturnID] [varchar](20) NOT NULL,
	[MortgageInvoiceID] [varchar](20) NOT NULL,
	[FromDate] [DateTime] NULL,
	[ToDate] [DateTime] NULL,
	[ReturnAmount] [Numeric] NULL,
	[PaidAmount] [Numeric] NULL,
	[InterestAmount] [Numeric] NULL,
	[AddOrSub] [Numeric] NULL,
	[LastLoginUserName] [Varchar](20) NULL,
	[LastModifiedDate] [DateTime] NULL,
	[LocationID] [varchar](20) NULL,
	[ReturnDate] [DateTime] NULL,
	[IsUpload] [Bit] NULL,
	[Remark] [Varchar](20) NULL,
	[IsDelete] [Bit] NULL,
	[TotalAmount] [Numeric] NULL
CONSTRAINT [PK_tbl_MortgageReturn] PRIMARY KEY CLUSTERED 
(
	[MortgageReturnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_MortgageReturnItem' )
CREATE TABLE [dbo].[tbl_MortgageReturnItem](
	[MortgageReturnItemID] [varchar](20) NOT NULL,
	[MortgageReturnID] [varchar](20) NOT NULL,
	[MortgageItemID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[ItemCategoryID] [nvarchar](20) NULL,
	[ItemName] [nvarchar](100) NULL,
	[GoldTK] [Numeric] (18,13) NULL,
	[GoldTG] [Numeric] (18,13) NULL,
	[Amount] [int] NULL,
	[IsDone] [Bit] NULL,
	[DonePercent] [int] NULL
CONSTRAINT [PK_tbl_MortgageReturnItem] PRIMARY KEY CLUSTERED 
(
	[MortgageReturnItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO