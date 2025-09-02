Update tbl_Version Set VersionNo='4.8000',VersionDate=GETDATE();
GO
/****** Object:  Table [dbo].[tbl_TransferReturn]    Script Date: 11/19/2019 10:51:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TransferReturn](
	[TransferReturnID] [varchar](20) NOT NULL,
	[TransferReturnDate] [datetime] NULL,
	[CurrentLocationID] [varchar](20) NULL,
	[StaffID] [varchar](20) NULL,
	[Remark] [nvarchar](100) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
 CONSTRAINT [PK_tbl_TransferReturn] PRIMARY KEY CLUSTERED 
(
	[TransferReturnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TransferReturnItem]    Script Date: 11/19/2019 10:54:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TransferReturnItem](
	[TransferReturnItemID] [varchar](20) NOT NULL,
	[TransferReturnID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[OriginalFixedPrice] [int] NULL,
	[OriginalPriceGram] [int] NULL,
	[OriginalPriceTK] [int] NULL,
	[OriginalGemsPrice] [int] NULL,
	[PriceCode] [varchar](10) NULL,
	[FixPrice] [int] NULL,
 CONSTRAINT [PK_tbl_TransferReturnItem] PRIMARY KEY CLUSTERED 
(
	[TransferReturnItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tbl_TransferReturnItem]  WITH CHECK ADD  CONSTRAINT [FK_tbl_TransferReturnItem_tbl_TransferReturn] FOREIGN KEY([TransferReturnID])
REFERENCES [dbo].[tbl_TransferReturn] ([TransferReturnID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_TransferReturnItem] CHECK CONSTRAINT [FK_tbl_TransferReturnItem_tbl_TransferReturn]
GO