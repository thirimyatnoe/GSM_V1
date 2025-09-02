Update tbl_Version Set VersionNo='3.5005',VersionDate=GETDATE();
GO
/****** Object:  Table [dbo].[tbl_ReturnAdvance]    Script Date: 07/06/2018 10:30:31 AM ******/
SET ANSI_NULLS ON
GO
/****** Object:  Table [dbo].[tbl_WasteSetupHeader]    Script Date: 07/07/17 4:07:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ReturnAdvance' )
CREATE TABLE [dbo].[tbl_ReturnAdvance](
	[ReturnAdvanceID] [varchar](50) NOT NULL,
	[ReturnAdvanceDate] [datetime] NULL,
	[StaffID] [varchar](50) NULL,
	[CustomerID] [varchar](50) NULL,
	[Remark] [varchar](50) NULL,
	[TotalTG] [decimal](18, 13) NULL,
	[TotalAmount] [bigint] NULL,
	[Discount] [bigint] NULL,
	[NetAmount] [bigint] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](50) NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
 CONSTRAINT [PK_tbl_ReturnAdvance] PRIMARY KEY CLUSTERED 
(
	[ReturnAdvanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_WasteSetupHeader' )
CREATE TABLE [dbo].[tbl_WasteSetupHeader](
	[WasteSetupHeaderID] [varchar](20) NOT NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemNameID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_WasteSetUp] PRIMARY KEY CLUSTERED 
(
	[WasteSetupHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ReturnAdvance' )
CREATE TABLE [dbo].[tbl_ReturnAdvance](
	[ReturnAdvanceID] [varchar](50) NOT NULL,
	[ReturnAdvanceDate] [datetime] NULL,
	[StaffID] [varchar](50) NULL,
	[CustomerID] [varchar](50) NULL,
	[Remark] [varchar](50) NULL,
	[TotalTG] [decimal](18, 13) NULL,
	[TotalAmount] [bigint] NULL,
	[Discount] [bigint] NULL,
	[NetAmount] [bigint] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](50) NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
 CONSTRAINT [PK_tbl_ReturnAdvance] PRIMARY KEY CLUSTERED 
(
	[ReturnAdvanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ReturnAdvanceItem]    Script Date: 07/06/2018 10:30:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ReturnAdvanceItem' )
CREATE TABLE [dbo].[tbl_ReturnAdvanceItem](
	[ReturnAdvanceItemID] [varchar](50) NOT NULL,
	[ReturnAdvanceID] [varchar](50) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[SaleRate] [bigint] NULL,
	[Amount] [bigint] NULL,
	[Remark] [varchar](50) NULL,
	[IsUsed] [bit] NULL,
 CONSTRAINT [PK_tbl_ReturnAdvanceItem] PRIMARY KEY CLUSTERED 
(
	[ReturnAdvanceItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_WasteSetupDetail]    Script Date: 07/07/17 4:08:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_WasteSetupDetail' )
CREATE TABLE [dbo].[tbl_WasteSetupDetail](
	[WasteSetupDetailID] [varchar](20) NOT NULL,
	[WasteSetupHeaderID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[MinNetWeightTK] [decimal](18, 13) NULL,
	[MinNetWeightTG] [decimal](18, 13) NULL,
	[MaxNetWeightTK] [decimal](18, 13) NULL,
	[MaxNetWeightTG] [decimal](18, 13) NULL,
	[MinWeightTKForSale] [decimal](18, 13) NULL,
	[MinWeightTGForSale] [decimal](18, 13) NULL,
 CONSTRAINT [PK_tbl_WasteSetupDetail] PRIMARY KEY CLUSTERED 
(
	[WasteSetupDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ReturnAdvanceItem' )
CREATE TABLE [dbo].[tbl_ReturnAdvanceItem](
	[ReturnAdvanceItemID] [varchar](50) NOT NULL,
	[ReturnAdvanceID] [varchar](50) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[SaleRate] [bigint] NULL,
	[Amount] [bigint] NULL,
	[Remark] [varchar](50) NULL,
	[IsUsed] [bit] NULL,
 CONSTRAINT [PK_tbl_ReturnAdvanceItem] PRIMARY KEY CLUSTERED 
(
	[ReturnAdvanceItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
------Stock Updat---------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowUpdate')
alter table tbl_GlobalSetting add IsAllowUpdate bit NULL
GO
update tbl_GlobalSetting set IsAllowUpdate=0;
GO
------Stock Update---------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowUpdate')
alter table tbl_GlobalSetting add IsAllowUpdate bit NULL
GO
update tbl_GlobalSetting set IsAllowUpdate=0;
GO


