---------MortgageGlobalSetting---------------------
Update tbl_Version Set VersionNo='4.0000' ,VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'InterestRate')
alter table tbl_GlobalSetting add InterestRate int NULL
GO
update tbl_GlobalSetting set InterestRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'InterestPeriod')
alter table tbl_GlobalSetting add InterestPeriod int NULL
GO
update tbl_GlobalSetting set InterestPeriod=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'EnablePaidAmount')
alter table tbl_GlobalSetting add EnablePaidAmount bit NULL
GO
update tbl_GlobalSetting set EnablePaidAmount=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'IsShowGW')
alter table tbl_BarcodeSetting add IsShowGW bit NULL
GO
update tbl_BarcodeSetting set IsShowGW=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowSaleReturn')
alter table tbl_GlobalSetting add IsAllowSaleReturn bit NULL
GO
update tbl_GlobalSetting set IsAllowSaleReturn=1;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowSale')
alter table tbl_GlobalSetting add IsAllowSale bit NULL
GO
update tbl_GlobalSetting set IsAllowSale=1;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowStock')
alter table tbl_GlobalSetting add IsAllowStock bit NULL
GO
update tbl_GlobalSetting set IsAllowStock=1;
GO
Update tbl_GlobalSetting Set InterestRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'InterestPeriod')
Alter table tbl_GlobalSetting ADD InterestPeriod int
GO
Update tbl_GlobalSetting Set InterestPeriod=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'EnablePaidAmount')
Alter table tbl_GlobalSetting ADD EnablePaidAmount bit
GO
Update tbl_GlobalSetting Set EnablePaidAmount=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'IsShowGW')
Alter table tbl_BarcodeSetting ADD IsShowGW bit
GO
Update tbl_BarcodeSetting Set IsShowGW=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowSaleReturn')
Alter table tbl_GlobalSetting ADD IsAllowSaleReturn bit
GO
Update tbl_GlobalSetting Set IsAllowSaleReturn=1;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowSale')
Alter table tbl_GlobalSetting ADD IsAllowSale bit
GO
Update tbl_GlobalSetting Set IsAllowSale=1;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'InterestRate')
alter table tbl_GlobalSetting add InterestRate int NULL
GO
update tbl_GlobalSetting set InterestRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'InterestPeriod')
alter table tbl_GlobalSetting add InterestPeriod int NULL
GO
update tbl_GlobalSetting set InterestPeriod=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'EnablePaidAmount')
alter table tbl_GlobalSetting add EnablePaidAmount bit NULL
GO
update tbl_GlobalSetting set EnablePaidAmount=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_BarcodeSetting' and column_name = 'IsShowGW')
alter table tbl_BarcodeSetting add IsShowGW bit NULL
GO
update tbl_BarcodeSetting set IsShowGW=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowSaleReturn')
alter table tbl_GlobalSetting add IsAllowSaleReturn bit NULL
GO
update tbl_GlobalSetting set IsAllowSaleReturn=1;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowSale')
alter table tbl_GlobalSetting add IsAllowSale bit NULL
GO
update tbl_GlobalSetting set IsAllowSale=1;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsAllowStock')
alter table tbl_GlobalSetting add IsAllowStock bit NULL
GO
update tbl_GlobalSetting set IsAllowStock=1;
GO
----------SaleGems-----------------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGemsItem' and column_name = 'IsReturn')
alter table tbl_SaleGemsItem add IsReturn bit NULL
GO
update tbl_SaleGemsItem set IsReturn=0;
GO
-----------------Purchase----------------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_PurchaseDetail' and column_name = 'SaleGemsItemID')
alter table tbl_PurchaseDetail add SaleGemsItemID Varchar(20) NULL
GO
update tbl_PurchaseDetail set SaleGemsItemID='';
GO
--------WholeSaleAnd Consignment---------
/****** Object:  Table [dbo].[tbl_ConsignmentSale]    Script Date: 2/15/2019 2:37:14 PM ******/
SET ANSI_NULLS ON
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ConsignmentSale' )
CREATE TABLE [dbo].[tbl_ConsignmentSale](
	[ConsignmentSaleID] [varchar](20) NOT NULL,
	[ConsignDate] [datetime] NULL,
	[WholesaleInvoiceID] [varchar](50) NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [nvarchar](20) NULL,
	[Remark] [varchar](100) NULL,
	[NetAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[Discount] [int] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[JC_IsUpload] [bit] NULL,
	[PaidAmount] [bigint] NULL,
 CONSTRAINT [PK_tbl_ConsignmentSale] PRIMARY KEY CLUSTERED 
(
	[ConsignmentSaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_ConsignmentSaleItem' )
CREATE TABLE [dbo].[tbl_ConsignmentSaleItem](
	[ConsignmentSaleItemID] [varchar](20) NOT NULL,
	[ConsignmentSaleID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NULL,
	[IsReturn] [bit] NULL,
	[IsSale] [bit] NULL,
	[SalesRate] [bigint] NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GOldQualityID] [varchar](20) NULL,
	[GOldPrice] [bigint] NULL,
	[FixPrice] [bigint] NULL,
	[GOldTK] [decimal](18, 13) NULL,
	[GOldTG] [decimal](18, 13) NULL,
 CONSTRAINT [PK_tbl_ConsignmentSaleItem] PRIMARY KEY CLUSTERED 
(
	[ConsignmentSaleItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_WholesaleInvoice' )
CREATE TABLE [dbo].[tbl_WholesaleInvoice](
	[WholesaleInvoiceID] [varchar](20) NOT NULL,
	[WDate] [datetime] NOT NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [nvarchar](20) NULL,
	[NetAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[Discount] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[DueDate] [datetime] NULL,
	[PayType] [int] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[JC_IsUpload] [bit] NULL,
 CONSTRAINT [PK_tbl_WholesaleInvoice] PRIMARY KEY CLUSTERED 
(
	[WholesaleInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_WholesaleInvoiceItem' )
CREATE TABLE [dbo].[tbl_WholesaleInvoiceItem](
	[WholesaleInvoiceItemID] [varchar](20) NOT NULL,
	[WholesaleInvoiceID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NOT NULL,
	[IsReturn] [bit] NULL,
	[IsSale] [bit] NULL,
	[SalesRate] [int] NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[GOldPrice] [int] NULL,
	[FixPrice] [int] NULL,
	[GOldTK] [decimal](18, 13) NULL,
	[GOldTG] [decimal](18, 13) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GOldQualityID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_WholesaleInvoiceItem] PRIMARY KEY CLUSTERED 
(
	[WholesaleInvoiceItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_WholesaleReturn' )
CREATE TABLE [dbo].[tbl_WholesaleReturn](
	[WholesaleReturnID] [varchar](20) NOT NULL,
	[WReturnDate] [datetime] NOT NULL,
	[WholesaleInvoiceID] [varchar](50) NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [nvarchar](20) NULL,
	[Remark] [nvarchar](100) NULL,
	[SaleAmount] [bigint] NULL,
	[SaleReturnAmount] [bigint] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[ConsignmentSaleID] [varchar](20) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[Discount] [bigint] NULL,
 CONSTRAINT [PK_tbl_WholesaleReturn] PRIMARY KEY CLUSTERED 
(
	[WholesaleReturnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_WholesaleReturnItem' )
CREATE TABLE [dbo].[tbl_WholesaleReturnItem](
	[WholesaleReturnItemID] [varchar](20) NOT NULL,
	[WholesaleReturnID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NULL,
	[IsReturn] [bit] NULL,
	[IsSale] [bit] NULL,
	[SalesRate] [bigint] NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[GOldTK] [decimal](18, 13) NULL,
	[GOldTG] [decimal](18, 13) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GOldQualityID] [varchar](20) NULL,
	[GOldPrice] [int] NULL,
	[FixPrice] [nchar](10) NULL,
 CONSTRAINT [PK_tbl_WholesaleReturnItem] PRIMARY KEY CLUSTERED 
(
	[WholesaleReturnItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
-----------------SaleGem----------------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGems' and column_name = 'PurchaseHeaderID')
alter table tbl_SaleGems add PurchaseHeaderID Varchar(20) NULL
GO
update tbl_SaleGems set PurchaseHeaderID='';
GO
-----------------SaleGem------------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGems' and column_name = 'PurchaseAmount')
alter table tbl_SaleGems add PurchaseAmount BigInt NULL
GO
update tbl_SaleGems set PurchaseAmount=0;
GO
--------FK Remove-----
--if exists(ALTER TABLE tbl_RecordOtherCash DROP CONSTRAINT FK_tbl_RecordOtherCash_tbl_SaleInvoiceHeader)
--------OrderInvoice-----
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_OrderReceiveDetail' and column_name = 'GOldSmithID')
alter table tbl_OrderReceiveDetail add GOldSmithID Varchar(20) NULL
GO
update tbl_OrderReceiveDetail set GOldSmithID='';
GO
------QRCode---------------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'QRCode')
alter table tbl_GlobalSetting add QRCode Varchar(500) NULL
GO
update tbl_GlobalSetting set QRCode='';
GO
----SaleGem----
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGems' and column_name = 'IsOtherCash')
alter table tbl_SaleGems add IsOtherCash Bit NULL
GO
update tbl_SaleGems set IsOtherCash=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGems' and column_name = 'OtherCashAmount')
alter table tbl_SaleGems add OtherCashAmount BigInt NULL
GO
update tbl_SaleGems set OtherCashAmount=0;
GO
------------Mortgage----------------
/****** Object:  Table [dbo].[tbl_MortgageInterest]    Script Date: 2/20/2019 3:20:58 PM ******/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGems' and column_name = 'IsOtherCash')
Alter table tbl_SaleGems ADD IsOtherCash bit
GO
Update tbl_SaleGems Set IsOtherCash=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleGems' and column_name = 'OtherCashAmount')
Alter table tbl_SaleGems ADD OtherCashAmount BigInt 
GO
Update tbl_SaleGems Set OtherCashAmount=0;
GO
------------Mortgage----------------
/****** Object:  Table [dbo].[tbl_MortgageInterest]    Script Date: 2/20/2019 3:20:58 PM ******/
SET ANSI_NULLS ON
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_MortgageInterest' )
CREATE TABLE [dbo].[tbl_MortgageInterest](
	[MortgageInterestID] [varchar](20) NOT NULL,
	[MortgageInvoiceID] [varchar](20) NULL,
	[FromDate] [smalldatetime] NULL,
	[ToDate] [smalldatetime] NULL,
	[InterestAmount] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[InterestPaidDate] [smalldatetime] NULL,
	[DiscountAmount] [int] NULL,
	[IsUpload] [bit] NULL,
	[IsDelete] [bit] NULL,
	[Remark] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_MortgageInterest] PRIMARY KEY CLUSTERED 
(
	[MortgageInterestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_MortgageInvoice' )
CREATE TABLE [dbo].[tbl_MortgageInvoice](
	[MortgageInvoiceID] [varchar](20) NOT NULL,
	[ReceiveDate] [smalldatetime] NULL,
	[MortgageStaff] [nvarchar](50) NULL,
	[InterestRate] [numeric](18, 2) NULL,
	[TotalAmount] [bigint] NULL,
	[TotalQTY] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[IsDisable] [bit] NULL,
	[DisableDate] [smalldatetime] NULL,
	[IsReturn] [bit] NULL,
	[ReturnDate] [smalldatetime] NULL,
	[InterestAmount] [bigint] NULL,
	[NetAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[RRemark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[MortgageInvoiceCode] [varchar](20) NULL,
	[IsRepayByHeadOffice] [bit] NULL,
	[CustomerID] [varchar](20) NULL,
	[IsUpload] [bit] NULL,
	[IsDelete] [bit] NULL,
	[InterestPeriod] [int] NULL,
	[PaybackAmt] [bigint] NULL,
	[PaybackInterestAmt] [bigint] NULL,
	[IsPayBack] [bit] NULL,
 CONSTRAINT [PK_tbl_MortgageInvoice] PRIMARY KEY CLUSTERED 
(
	[MortgageInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_MortgageInvoiceItem' )
CREATE TABLE [dbo].[tbl_MortgageInvoiceItem](
	[MortgageItemID] [varchar](20) NOT NULL,
	[MortgageInvoiceID] [varchar](20) NULL,
	[GOldQualityID] [varchar](20) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemName] [nvarchar](100) NULL,
	[QTY] [int] NULL,
	[GOldTK] [numeric](18, 13) NULL,
	[GOldTG] [numeric](18, 13) NULL,
	[Amount] [bigint] NULL,
	[MortgageRate] [int] NULL,
	[IsDone] [bit] NULL,
	[DonePercent] [varchar](10) NULL,
	[ItemNameID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_MortgageInvoiceItem] PRIMARY KEY CLUSTERED 
(
	[MortgageItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_MortgagePayback' )
CREATE TABLE [dbo].[tbl_MortgagePayback](
	[MortgagePaybackID] [varchar](20) NOT NULL,
	[MortgageInvoiceID] [varchar](20) NULL,
	[FromDate] [smalldatetime] NULL,
	[ToDate] [smalldatetime] NULL,
	[PaybackAmount] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[PaybackDate] [smalldatetime] NULL,
	[DiscountAmount] [int] NULL,
	[IsUpload] [bit] NULL,
	[Remark] [varchar](20) NULL,
	[InterestAmt] [bigint] NULL,
 CONSTRAINT [PK_tbl_MortgagePayback] PRIMARY KEY CLUSTERED 
(
	[MortgagePaybackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
------tbl_KeywordHeader-------------
/****** Object:  Table [dbo].[tbl_KeywordHeader]    Script Date: 01/17/2019 09:58:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
------tbl_KeywordHeader-------------
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_KeywordHeader' )
CREATE TABLE [dbo].[tbl_KeywordHeader](
	[KeywordID] [int] NOT NULL,
	[KeywordName] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_KeywordHeader] PRIMARY KEY CLUSTERED 
(
	[KeywordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'tbl_KeywordItem' )
CREATE TABLE [dbo].[tbl_KeywordItem](
	[ItemID] [int] NOT NULL,
	[KeywordID] [int] NOT NULL,
	[ItemName] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_KeywordItem] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
----DailyExpense--------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_DailyExpense' and column_name = 'IsBank')
alter table tbl_DailyExpense add IsBank Bit NULL
GO
update tbl_DailyExpense set IsBank=0;
GO
----DailyIncome-------
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_DailyIncome' and column_name = 'IsBank')
alter table tbl_DailyIncome add IsBank BigInt NULL
GO
update tbl_DailyIncome set IsBank=0;
GO

