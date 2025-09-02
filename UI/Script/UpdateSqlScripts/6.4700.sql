Update tbl_Version Set VersionNo='6.4700',VersionDate=GETDATE();
GO
/****** Object:  Table [dbo].[tbl_DiamondPriceRate]    Script Date: 8/18/2020  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DiamondPriceRate](
	[DefineID] [varchar](20) NOT NULL,
	[DefineDateTime] [datetime] NULL,
	[CaratFrom] [decimal](18, 2) NULL,
	[CaratTo] [decimal](18, 2) NULL,
	[PriceRate] [decimal](19, 1) NULL,
	[WholeSaleRate] [decimal](19, 1) NULL,
	[PercentDirectChange] [int] NULL,
	[PercentReturn] [int] NULL,
	[IsDelete] [bit] NULL,
    [LastModifiedDate] [datetime] NULL,
	[LastModifiedUserName] [varchar](200) NULL
 CONSTRAINT [PK_tbl_DiamondPriceRate] PRIMARY KEY CLUSTERED 
(
	[DefineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'WSFixPrice')
alter table tbl_ForSale add  WSFixPrice BigInt NUll 
GO
Update tbl_ForSale Set WSFixPrice=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSaleGemsItem' and column_name = 'SaleByDefinePrice')
alter table tbl_ForSaleGemsItem add  SaleByDefinePrice Bit NUll 
GO
Update tbl_ForSaleGemsItem Set SaleByDefinePrice=0;
GO
INSERT tbl_KeywordHeader(KeywordID, KeywordName) VALUES (2, N'Color')
GO
INSERT tbl_KeywordHeader(KeywordID, KeywordName) VALUES (3, N'Shape')
GO
INSERT tbl_KeywordHeader(KeywordID, KeywordName) VALUES (4, N'Clarity')
GO
INSERT INTO tbl_key_generate (GenerateKeyType, GenerateFormat, GenerateID,GenerateOn) VALUES ('KeywordItem', '0', 1,'KeywordItemID')
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'IsLooseDiamond')
alter table tbl_ForSale add  IsLooseDiamond Bit NUll 
GO
Update tbl_ForSale Set IsLooseDiamond=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'SDGemsCategoryID')
alter table tbl_ForSale add  SDGemsCategoryID Varchar(20) NUll 
GO
Update tbl_ForSale Set SDGemsCategoryID='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'Shape')
alter table tbl_ForSale add  Shape Varchar(20) NUll 
GO
Update tbl_ForSale Set Shape='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'Clarity')
alter table tbl_ForSale add  Clarity Varchar(20) NUll 
GO
Update tbl_ForSale Set Clarity='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'SDGemsName')
alter table tbl_ForSale add  SDGemsName Varchar(100) NUll 
GO
Update tbl_ForSale Set SDGemsName='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'SDYOrCOrG')
alter table tbl_ForSale add  SDYOrCOrG Varchar(20) NUll 
GO
Update tbl_ForSale Set SDYOrCOrG='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'OriginalPriceCarat')
alter table tbl_ForSale add  OriginalPriceCarat Int NUll 
GO
Update tbl_ForSale Set OriginalPriceCarat=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'IsOriginalPriceCarat')
alter table tbl_ForSale add  IsOriginalPriceCarat Bit NUll 
GO
Update tbl_ForSale Set IsOriginalPriceCarat=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GemsCategory' and column_name = 'Prefix')
alter table tbl_GemsCategory add  Prefix varchar(20) NUll 
GO
Update tbl_GemsCategory Set Prefix='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'SDGemsTW')
alter table tbl_ForSale add  SDGemsTW decimal(18,13) NUll 
GO
Update tbl_ForSale Set SDGemsTW=0.0;
GO
/****** Object:  Table [dbo].[tbl_SaleLooseDiamondHeader]    Script Date: 21/08/2020 3:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleLooseDiamondHeader](
	[SaleLooseDiamondID] [varchar](20) NOT NULL,
	[SaleDate] [datetime] NULL,
	[CustomerID] [varchar](20) NULL,
	[StaffID] [varchar](20) NULL,
	[Remark] [nvarchar](500) NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [int] NULL,
	[DiscountAmount] [int] NULL,
	[PaidAmount] [bigint] NULL,
	[PromotionDiscount] [int] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[PurchaseHeaderID] [varchar](20) NULL,
	[PurchaseAmount] [bigint] NULL,
	[IsOtherCash] [bit] NULL,
	[OtherCashAmount] [int] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[AllTaxAmt] [int] NULL,
	[SRTaxPer] [decimal](18, 2) NULL,
	[SRTaxAmt] [int] NULL,
 CONSTRAINT [PK_tbl_SaleLooseDiamondHeader] PRIMARY KEY CLUSTERED 
(
	[SaleLooseDiamondID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleLooseDiamondDetail]    Script Date: 21/08/2020 3:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleLooseDiamondDetail](
	[SaleLooseDiamondDetailID] [varchar](20) NOT NULL,
	[SaleLooseDiamondID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[Shape] [varchar] (30) NULL,
	[Clarity] [varchar] (30) NULL,
	[Color] [varchar] (30) NULL,
	[GemsName] [varchar] (100) NULL,
	[SalesRate] [bigint] NULL,
	[QTY] [int] NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[GemsTW] [decimal](18,13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[IsFixPrice] [bit] NULL,
	[FixPrice] [bigint] NULL,
	[GemsPrice] [bigint] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[DesignCharges] [int] NULL,
	[DesignChargesRate] [int] NULL,
	[WhiteCharges] [int] NULL,
	[PlatingCharges] [int] NULL,
	[MountingCharges] [int] NULL,
	[IsSaleReturn] [bit] NULL,
	[SellingRate] [int] NULL,
	[SellingAmt] [bigint] NULL,
	[IsOriginalFixedPrice] [bit] NULL,
	[OriginalFixedPrice] [bigint] NULL,
	[IsOriginalPriceCarat] [bit] NULL,
	[OriginalPriceCarat] [bigint] NULL,
	[OriginalCode] [varchar] (20) NULL,
 CONSTRAINT [PK_tbl_SaleLooseDiamondDetail] PRIMARY KEY CLUSTERED 
(
	[SaleLooseDiamondDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleLooseDiamondDetail' and column_name = 'IsReturn')
alter table tbl_SaleLooseDiamondDetail add  IsReturn Bit NUll 
GO
Update tbl_SaleLooseDiamondDetail Set IsReturn=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'PGemsCategoryID')
alter table tbl_PurchaseDetail add  PGemsCategoryID Varchar(20) NUll 
GO
Update tbl_PurchaseDetail Set PGemsCategoryID='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'PGemsName')
alter table tbl_PurchaseDetail add  PGemsName Varchar(20) NUll 
GO
Update tbl_PurchaseDetail Set PGemsName='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'Color')
alter table tbl_PurchaseDetail add  Color Varchar(20) NUll 
GO
Update tbl_PurchaseDetail Set Color='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'Shape')
alter table tbl_PurchaseDetail add  Shape Varchar(20) NUll 
GO
Update tbl_PurchaseDetail Set Shape='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'Clarity')
alter table tbl_PurchaseDetail add  Clarity Varchar(20) NUll 
GO
Update tbl_PurchaseDetail Set Clarity='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'SaleLooseDiamondDetailID')
alter table tbl_PurchaseDetail add  SaleLooseDiamondDetailID Varchar(20) NUll 
GO
Update tbl_PurchaseDetail Set SaleLooseDiamondDetailID='';
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseHeader' and column_name = 'IsLooseDiamond')
alter table tbl_PurchaseHeader add  IsLooseDiamond Bit
GO
Update tbl_PurchaseHeader Set IsLooseDiamond=0;
GO
/****** Object:  Table [dbo].[tbl_TransferLooseDiamond]    Script Date: 02/09/2020 2:54:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TransferLooseDiamond](
	[TransferID] [varchar](20) NOT NULL,
	[TransferDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[StaffID] [varchar](20) NULL,
	[Remark] [nvarchar](100) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[IsConfirm] [bit] NULL,
	[CurrentLocationID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_TransferLooseDiamond] PRIMARY KEY CLUSTERED 
(
	[TransferID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TransferLooseDiamondItem]    Script Date: 02/09/2020 2:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TransferLooseDiamondItem](
	[TransferItemID] [varchar](20) NOT NULL,
	[TransferID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[OriginalFixedPrice] [int] NULL,
	[OriginalPriceCarat] [int] NULL,
	[PriceCode] [varchar](10) NULL,
	[FixPrice] [int] NULL,
	[isReturn] [bit] NULL,
 CONSTRAINT [PK_tbl_TransferLooseDiamondItem] PRIMARY KEY CLUSTERED 
(
	[TransferItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TransferReturnDiamond]    Script Date: 03/09/2020 9:17:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TransferReturnDiamond](
	[TransferReturnID] [varchar](20) NOT NULL,
	[TransferReturnDate] [datetime] NULL,
	[CurrentLocationID] [varchar](20) NULL,
	[StaffID] [varchar](20) NULL,
	[Remark] [nvarchar](100) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
 CONSTRAINT [PK_tbl_TransferReturnDiamond] PRIMARY KEY CLUSTERED 
(
	[TransferReturnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TransferReturnDiamondItem]    Script Date: 03/09/2020 9:17:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TransferReturnDiamondItem](
	[TransferReturnItemID] [varchar](20) NOT NULL,
	[TransferReturnID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[OriginalFixedPrice] [int] NULL,
	[OriginalPriceCarat] [int] NULL,
	[PriceCode] [varchar](10) NULL,
	[FixPrice] [int] NULL,
 CONSTRAINT [PK_tbl_TransferReturnDiamondItem] PRIMARY KEY CLUSTERED 
(
	[TransferReturnItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ForSale' and column_name = 'TotalCost')
alter table tbl_ForSale add  TotalCost Int
GO
Update tbl_ForSale Set TotalCost=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsFixPrice')
alter table tbl_GlobalSetting add  IsFixPrice Bit
GO
Update tbl_GlobalSetting Set IsFixPrice=0;
GO