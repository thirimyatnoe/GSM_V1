Update tbl_Version Set VersionNo='5.9000',VersionDate=GETDATE();
GO
/****** Object:  Table [dbo].[tbl_CheckStock]    Script Date: 8/22/2019 1:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_CheckStock' )
CREATE TABLE [dbo].[tbl_CheckStock](
	[CheckStockID] [varchar](50) NOT NULL,
	[checkdatetime] [datetime] NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[StaffID] [varchar](50) NULL,
	[QTY] [int] NULL,
	[GoldTG] [decimal](18, 3) NULL,
	[MQTY] [int] NULL,
	[MGoldTG] [decimal](18, 3) NULL,
	[FQTY] [int] NULL,
	[FGoldTG] [decimal](18, 3) NULL,
	[Remark] [nvarchar](500) NULL,
	[LastModifiedLoginUserName] [varchar](20) NULL,
	[LastModifiedDate] [varchar](20) NULL,
	[IsUpload] Bit NULL,
	[LocationID] [varchar] (20) NULL,
	[isDelete] Bit NULL
 CONSTRAINT [PK__tbl_CheckStock] PRIMARY KEY CLUSTERED 
(
	[CheckStockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CheckStockItem]    Script Date: 8/22/2019 1:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_CheckStockItem' )
CREATE TABLE [dbo].[tbl_CheckStockItem](
	[CheckStockItemID] [varchar](50) NOT NULL,
	[CheckStockID] [varchar](50) NULL,
	[MBarcodeNo] [varchar](50) NULL,
	[MGoldTG] [decimal](18, 3) NULL,
	[MItemCategoryID] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_CheckStockItem] PRIMARY KEY CLUSTERED 
(
	[CheckStockItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ECheckStockItem]    Script Date: 8/22/2019 4:53:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ECheckStockItem' )
CREATE TABLE [dbo].[tbl_ECheckStockItem](
	[ECheckStockItemID] [varchar](50) NULL,
	[CheckStockID] [varchar](50) NULL,
	[EBarcodeNo] [varchar](50) NULL,
	[Weight] [Decimal](18,3) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO


