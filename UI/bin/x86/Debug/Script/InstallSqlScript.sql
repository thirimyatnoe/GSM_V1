USE [globalgold]
/****** Object:  Table [dbo].[tb_GE_EventLogs]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_GE_EventLogs](
	[LogType] [varchar](20) NOT NULL,
	[LogDateTime] [datetime] NULL,
	[LogInUser] [varchar](50) NULL,
	[Source] [varchar](50) NULL,
	[DataChange] [varchar](50) NULL,
	[AffectedID] [varchar](50) NULL,
	[LogMessage] [varchar](200) NULL,
	[ErrMessage] [varchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_GE_SystemUser]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_GE_SystemUser](
	[SysID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](50) NULL,
	[UserName] [varchar](255) NULL,
	[Password] [varchar](50) NULL,
	[UserLevelID] [int] NULL,
	[Remark] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_GE_UserLevel]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_GE_UserLevel](
	[SysID] [int] NOT NULL,
	[UserLevel] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[IsAdministrator] [bit] NULL,
	[Remark] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_GE_UserMenu]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_GE_UserMenu](
	[MenuID] [nvarchar](50) NOT NULL,
	[MenuName] [nvarchar](100) NULL,
	[_4] [bit] NULL,
	[_7] [bit] NULL,
	[_2] [bit] NULL,
	[_3] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BarcodeSetting]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BarcodeSetting](
	[PaperWidth] [varchar](50) NOT NULL,
	[PaperHeight] [varchar](50) NOT NULL,
	[X] [varchar](50) NOT NULL,
	[Y] [varchar](50) NOT NULL,
	[Height] [varchar](50) NOT NULL,
	[Narrow] [varchar](50) NOT NULL,
	[Wide] [varchar](50) NOT NULL,
	[PrinterName] [varchar](100) NOT NULL,
	[IsLocationName] [bit] NULL,
	[EngName] [varchar](50) NULL,
	[MMName] [nvarchar](100) NULL,
	[RightPositionX] [varchar](50) NULL,
	[IsIncludeGemWgt] [bit] NULL,
	[IsIncludeGemPrice] [bit] NULL,
	[IsPrefix] [bit] NULL,
	[IsLength] [bit] NULL,
	[IsFixItem] [bit] NULL,
	[IsFixGold] [bit] NULL,
	[IsFixGemQTY] [bit] NULL,
	[IsFixGemWeight] [bit] NULL,
	[IsAllDetail] [bit] NULL,
	[IsOriginalCode] [bit] NULL,
	[IsPriceCode] [bit] NULL,
	[IsWaste] [bit] NULL,
	[IsDescription] [bit] NULL,
	[IsGram] [bit] NULL,
	[IsShowGW] [bit] NULL,
	[IsFixPrice] [bit] NULL,
	[LeftFontSize] [int] NUll,
	[RightFontSize] [int] NUll
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Branch]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Branch](
	[BranchID] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](100) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
 CONSTRAINT [PK_tbl_b] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CashReceipt]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CashReceipt](
	[CashReceiptID] [varchar](20) NOT NULL,
	[VoucherNo] [varchar](20) NULL,
	[PayDate] [datetime] NULL,
	[PayAmount] [bigint] NULL,
	[Remark] [nvarchar](400) NULL,
	[LocationID] [varchar](20) NULL,
	[Type] [varchar](50) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[IsBank] [bit] NULL,
	[ReturnAdvanceID] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_CashReceipt] PRIMARY KEY CLUSTERED 
(
	[CashReceiptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CashType]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CashType](
	[CashTypeID] [varchar](20) NOT NULL,
	[CashType] [nvarchar](20) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_CashType] PRIMARY KEY CLUSTERED 
(
	[CashTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CompanyProfile]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CompanyProfile](
	[CompanyID] [varchar](50) NOT NULL,
	[CompanyName] [nvarchar](100) NULL,
	[CompanyLoGO] [image] NULL,
	[Telephone] [varchar](100) NULL,
	[EMail] [varchar](100) NULL,
	[Address] [nvarchar](1000) NULL,
	[WebSite] [varchar](100) NULL,
	[Fax] [varchar](50) NULL,
	[HeadOffice] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ConsignmentSale]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[PurchaseHeaderID] [varchar](20) NULL,
	[PurchaseAmount] [bigint] NULL,
	[MemberID] [varchar](20) NULL,
	[MemberCode] [varchar](50) NULL,
	[MemberName] [nvarchar](100) NULL,
	[RedeemID] [nvarchar](100) NULL,
	[TopupPoint] [int] NULL,
	[TopupValue] [int] NULL,
	[RedeemPoint] [int] NULL,
	[RedeemValue] [int] NULL,
	[IsRedeemInvoice] [Bit] NULL,
	[MemberDis] [int] NULL,
	[MemberDiscountAmt] [int] NULL,
	[TransactionID] [nvarchar](100) NULL,
	[InvoiceStatus] [int] NULL,
 CONSTRAINT [PK_tbl_ConsignmentSale] PRIMARY KEY CLUSTERED 
(
	[ConsignmentSaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ConsignmentSaleItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[GoldQualityID] [varchar](20) NULL,
	[GoldPrice] [bigint] NULL,
	[FixPrice] [bigint] NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
 CONSTRAINT [PK_tbl_ConsignmentSaleItem] PRIMARY KEY CLUSTERED 
(
	[ConsignmentSaleItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CurrentLocation]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CurrentLocation](
	[CurrentLocationID] [varchar](20) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Customer]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Customer](
	[CustomerID] [varchar](20) NOT NULL,
	[CustomerCode] [varchar](20) NULL,
	[CustomerName] [nvarchar](50) NULL,
	[CustomerAddress] [nvarchar](200) NULL,
	[CustomerTel] [varchar](20) NULL,
	[Remark] [nvarchar](200) NULL,
	[IsInactive] [bit] NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[DOB] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[NRC] [nvarchar](40) NULL,
	[MemberCode] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CustomReport]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_CustomReport](
	[ReportID] [varchar](20) NOT NULL,
	[ReportName] [varchar](100) NULL,
	[ReportCode] [varchar](5000) NULL,
	[CriCustomer] [bit] NULL,
	[CriGoldQuality] [bit] NULL,
	[CriItemCategory] [bit] NULL,
	[CriItemName] [bit] NULL,
	[CriGemsCategory] [bit] NULL,
	[CriStaff] [bit] NULL,
	[CriFromDate] [bit] NULL,
	[CriToDate] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DailyExpense]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DailyExpense](
	[DailyExpenseID] [varchar](20) NOT NULL,
	[ExpenseDate] [smalldatetime] NULL,
	[Remark] [nvarchar](1000) NULL,
	[TotalAmount] [numeric](18, 0) NULL,
	[LocationID] [varchar](20) NULL,
	[ReturnAmount] [numeric](18, 0) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[IsBank] [bit] NULL,
 CONSTRAINT [PK_tbl_DailyExpense] PRIMARY KEY CLUSTERED 
(
	[DailyExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DailyExpenseItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DailyExpenseItem](
	[DailyExpenseItemID] [varchar](20) NOT NULL,
	[DailyExpenseID] [varchar](20) NULL,
	[Description] [nvarchar](1000) NULL,
	[QTY] [bigint] NULL,
	[UnitPrice] [bigint] NULL,
	[Amount] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_DailyExpenseItem] PRIMARY KEY CLUSTERED 
(
	[DailyExpenseItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DailyIncome]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DailyIncome](
	[DailyIncomeID] [varchar](20) NOT NULL,
	[IncomeDate] [smalldatetime] NULL,
	[Remark] [nvarchar](1000) NULL,
	[TotalAmount] [numeric](18, 0) NULL,
	[LocationID] [varchar](50) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[IsBank] [bit] NULL,
 CONSTRAINT [PK_tbl_DailyIncome] PRIMARY KEY CLUSTERED 
(
	[DailyIncomeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DailyIncomeItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DailyIncomeItem](
	[DailyIncomeItemID] [varchar](20) NOT NULL,
	[DailyIncomeID] [varchar](20) NULL,
	[Description] [nvarchar](1000) NULL,
	[QTY] [bigint] NULL,
	[UnitPrice] [bigint] NULL,
	[Amount] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_DailyIncomeItem] PRIMARY KEY CLUSTERED 
(
	[DailyIncomeItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Damage]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Damage](
	[DamageID] [varchar](20) NOT NULL,
	[DDate] [datetime] NULL,
	[LocationID] [varchar](50) NULL,
	[Remark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[SaleStaffID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_Damage] PRIMARY KEY CLUSTERED 
(
	[DamageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DamageItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DamageItem](
	[DamageItemID] [varchar](20) NOT NULL,
	[DamageID] [varchar](20) NOT NULL,
	[ForSaleID] [varchar](20) NULL,
	[IsReAdd] [bit] NULL,
	[ReAddDate] [datetime] NULL,
	[Remark] [nvarchar](500) NULL,
 CONSTRAINT [PK_tbl_DamageItem] PRIMARY KEY CLUSTERED 
(
	[DamageItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ExportData]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ExportData](
	[ExportID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [varchar](20) NULL,
	[OtherLocationID] [varchar](20) NULL,
	[LocationName] [nvarchar](100) NULL,
	[OtherLocationName] [nvarchar](100) NULL,
	[TransactionType] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[AllUse] [bit] NULL,
	[CompanyName] [nvarchar](500) NULL,
	[ToMail] [varchar](300) NULL,
	[CCMail] [varchar](300) NULL,
 CONSTRAINT [PK_tbl_ExportData] PRIMARY KEY CLUSTERED 
(
	[ExportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ExportServiceLogs]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ExportServiceLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[logdatetime] [datetime] NULL,
	[logtype] [varchar](50) NULL,
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
/****** Object:  Table [dbo].[tbl_ForSale]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ForSale](
	[ForSaleID] [varchar](20) NOT NULL,
	[ItemCode] [varchar](20) NULL,
	[ItemNameID] [varchar](20) NULL,
	[Length] [nvarchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[GivenDate] [datetime] NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[TotalTK] [decimal](18, 13) NULL,
	[TotalTG] [decimal](18, 13) NULL,
	[IsExit] [bit] NULL,
	[ExitDate] [datetime] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[Width] [nvarchar](20) NULL,
	[IsFixPrice] [bit] NULL,
	[FixPrice] [bigint] NULL,
	[DesignCharges] [bigint] NULL,
	[PlatingCharges] [bigint] NULL,
	[MountingCharges] [bigint] NULL,
	[WhiteCharges] [bigint] NULL,
	[IsOriginalFixedPrice] [bit] NULL,
	[OriginalFixedPrice] [bigint] NULL,
	[IsOriginalPriceGram] [bit] NULL,
	[OriginalPriceGram] [bigint] NULL,
	[OriginalPriceTK] [bigint] NULL,
	[OriginalGemsPrice] [bigint] NULL,
	[OriginalOtherPrice] [bigint] NULL,
	[Photo] [nvarchar](500) NULL,
	[SellingPrice] [varchar](50) NULL,
	[LocationID] [varchar](20) NULL,
	[IsOrder] [bit] NULL,
	[IsClosed] [bit] NULL,
	[OrderReceiveDetailID] [varchar](20) NULL,
	[IsVolume] [bit] NULL,
	[QTY] [int] NULL,
	[StaffID] [varchar](20) NULL,
	[LossQTY] [int] NULL,
	[LossItemTK] [decimal](18, 13) NULL,
	[LossItemTG] [decimal](18, 13) NULL,
	[TotalGemPrice] [bigint] NULL,
	[PurchaseWasteTK] [decimal](18, 13) NULL,
	[PurchaseWasteTG] [decimal](18, 13) NULL,
	[GoldSmith] [nvarchar](50) NULL,
	[Remark] [nvarchar](200) NULL,
	[IsDiamond] [bit] NULL,
	[OriginalCode] [varchar](50) NULL,
	[PriceCode] [varchar](10) NULL,
	[Color] [varchar](20) NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[IsCheck] [bit] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[tbl_ForSale] ADD [SupplierID] [varchar](20) NULL
ALTER TABLE [dbo].[tbl_ForSale] ADD [SupplierVou] [varchar](20) NULL
ALTER TABLE [dbo].[tbl_ForSale] ADD [GoldSmithID] [varchar](20) NULL
ALTER TABLE [dbo].[tbl_ForSale] ADD [WReturnDate] [datetime] NULL
ALTER TABLE [dbo].[tbl_ForSale] ADD [IsSolidVolume] [Bit] NULL
ALTER TABLE [dbo].[tbl_ForSale] ADD [SellingRate] [int] NULL
 CONSTRAINT [PK_tbl_ForSale] PRIMARY KEY CLUSTERED 
(
	[ForSaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ForSaleGemsItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ForSaleGemsItem](
	[ForSaleGemsItemID] [varchar](20) NOT NULL,
	[ForSaleID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[Type] [varchar](20) NULL,
	[UnitPrice] [bigint] NULL,
	[Amount] [bigint] NULL,
	[GemsRemark] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_ForSaleGemsItem] PRIMARY KEY CLUSTERED 
(
	[ForSaleGemsItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_GemsCategory]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_GemsCategory](
	[GemsCategoryID] [varchar](20) NOT NULL,
	[GemsCategory] [nvarchar](50) NULL,
	[StoneType] [varchar](50) NULL,
	[GemTaxPer] [decimal](5, 2) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] DateTime NULL,
 CONSTRAINT [PK_tbl_GemsCategory] PRIMARY KEY CLUSTERED 
(
	[GemsCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_GeneralLedgerByLocation]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_GeneralLedgerByLocation](
	[GLByLocationID] [varchar] (20) NULL,
	[LocationID] [varchar](20) NULL,
	[GLDate] [datetime] NULL,
	[Title] [nvarchar](100) NULL,
	[DebitAmount] [bigint] NULL,
	[CreditAmount] [bigint] NULL,
	[Type] [varchar](50) NULL,
	[MyanTitle] [nvarchar](1000) NULL,
	[LastModifiedDate] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_GenerateFormat]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_GenerateFormat](
	[GenerateFormatID] [int] NOT NULL,
	[GenerateFormat] [varchar](50) NOT NULL,
	[Prefix] [varchar](50) NOT NULL,
	[FormatDate1] [varchar](50) NOT NULL,
	[FormatDate2] [varchar](50) NOT NULL,
	[Prefix2] [varchar](50) NOT NULL,
	[NumberCount] [varchar](50) NOT NULL,
	[PrefixPlace] [varchar](50) NULL,
	[IsGenerate] [bit] NULL,
 CONSTRAINT [PK_tbl_GenerateFormat] PRIMARY KEY CLUSTERED 
(
	[GenerateFormatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_GlobalSetting]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_GlobalSetting](
	[Photo] [nvarchar](50) NULL,
	[DatabaseSharePath] [varchar](100) NULL,
	[IsCarat] [int] NULL,
	[IsReuseBarcode] [bit] NULL,
	[AllowDis] [int] NULL,
	[IsCash] [bit] NULL,
	[IsSpeedEntry] [bit] NULL,
	[IsExactPrice] [bit] NULL,
	[DiffPurchaseRate] [int] NULL,
	[DiffChangeRate] [int] NULL,
	[DecimalFormat] [int] NULL,
	[IsAllowUpdate] [bit] NULL,
	[InterestRate] [int] NULL,
	[InterestPeriod] [int] NULL,
	[EnablePaidAmount] [bit] NULL,
	[IsAllowSaleReturn] [bit] NULL,
	[IsAllowSale] [bit] NULL,
	[IsAllowStock] [bit] NULL,
	[IsUsedSettingPeriod] [Bit] NULL,
	[AllowStockWeight] [decimal](18, 3) NULL,
	[IsOneMonthCalculation] [Bit] NULL,
	[OverDay] [int] NULL,
	[IsHoToBranch] [Bit] NULL,
	[MachineType] [varchar](20) NULL,
	[Prefix] [int] NULL,
	[Postfix] [int] NULL,
	[IsHoMaster] [Bit] NULL,
	[SoftwareVendorSetting] [Bit] NULL,
	[IsFixPrice] [Bit] NULL,
	[IsUseMember] [Bit] NULL,
	[IsMemberCustomer] [Bit] NULL,
	[RegName] [Varchar](50) NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[tbl_GlobalSetting] ADD [QRCode] [varchar](500) NULL
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_GoldQuality]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_GoldQuality](
	[GoldQualityID] [varchar](20) NOT NULL,
	[GoldQuality] [nvarchar](50) NULL,
	[Prefix] [varchar](20) NULL,
	[IsGramRate] [bit] NULL,
	[IsSolidGold] [bit] NULL,
	[MultiplyBy] [decimal](18, 2) NULL,
	[DividedBy] [decimal](18, 2) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] datetime NULL,
	[BarcodeStatus] [Int] NULL,
 CONSTRAINT [PK_tbl_GoldQuality] PRIMARY KEY CLUSTERED 
(
	[GoldQualityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_GoldSmith]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_GoldSmith](
	[GoldSmithID] [varchar](20) NOT NULL,
	[GoldSmithCode] [varchar](20) NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Remark] [nvarchar](500) NULL,
	[IsInactive] [bit] NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_GoldSmith] PRIMARY KEY CLUSTERED 
(
	[GoldSmithID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ItemCategory]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ItemCategory](
	[ItemCategoryID] [varchar](20) NOT NULL,
	[ItemCategory] [nvarchar](50) NULL,
	[Prefix] [varchar](20) NULL,
	[ItemTaxPer] [decimal](5, 2) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] DateTime NULL,
 CONSTRAINT [PK_tbl_ItemCategory] PRIMARY KEY CLUSTERED 
(
	[ItemCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ItemName]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ItemName](
	[ItemNameID] [varchar](20) NOT NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemName] [nvarchar](100) NULL,
	[ItemPhoto] [image] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] DateTime NULL,
 CONSTRAINT [PK_tbl_ItemName] PRIMARY KEY CLUSTERED 
(
	[ItemNameID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_key_generate]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_key_generate](
	[GenerateKeyType] [varchar](100) NULL,
	[GenerateFormat] [varchar](100) NULL,
	[GenerateID] [bigint] NULL,
	[GenerateOn] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_KeywordHeader]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[tbl_KeywordItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[tbl_Location]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Location](
	[LocationID] [varchar](20) NOT NULL,
	[Location] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
	[Phone] [nvarchar](100) NULL,
	[Remark15] [nvarchar](1000) NULL,
	[RemarkDone] [nvarchar](1000) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[CurrentLocationID] [varchar](20) NULL,
	[LastModifiedDate] DateTime NULL,
	[IsHeadOffice] Bit NULL,
 CONSTRAINT [PK_tbl_Location] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Measurement]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Measurement](
	[FromMeasurement] [varchar](10) NOT NULL,
	[ToMeasurement] [varchar](10) NOT NULL,
	[Equivalent] [numeric](18, 13) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MortgageInterest]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[InterestMonth] [int] NULL,
 CONSTRAINT [PK_tbl_MortgageInterest] PRIMARY KEY CLUSTERED 
(
	[MortgageInterestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MortgageInvoice]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[tbl_MortgageInvoiceItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_MortgageInvoiceItem](
	[MortgageItemID] [varchar](20) NOT NULL,
	[MortgageInvoiceID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemName] [nvarchar](100) NULL,
	[QTY] [int] NULL,
	[GoldTK] [numeric](18, 13) NULL,
	[GoldTG] [numeric](18, 13) NULL,
	[Amount] [bigint] NULL,
	[MortgageRate] [int] NULL,
	[IsDone] [bit] NULL,
	[DonePercent] [varchar](10) NULL,
	[ItemNameID] [varchar](20) NULL,
	[IsPayback] [Bit] NULL,
	[MortgageItemCode] [varchar](50) NULL
 CONSTRAINT [PK_tbl_MortgageInvoiceItem] PRIMARY KEY CLUSTERED 
(
	[MortgageItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_MortgagePayback]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[TotalAmount] [int] NULL,
	[IsDelete] [Bit] NULL,
 CONSTRAINT [PK_tbl_MortgagePayback] PRIMARY KEY CLUSTERED 
(
	[MortgagePaybackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OrderInvoice]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_OrderInvoice](
	[OrderInvoiceID] [varchar](20) NOT NULL,
	[OrderDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [varchar](20) NULL,
	[PayGoldQualityID] [varchar](20) NULL,
	[PayGoldTK] [decimal](18, 13) NULL,
	[PayGoldTG] [decimal](18, 13) NULL,
	[Remark] [nvarchar](500) NULL,
	[AllTotalAmount] [bigint] NULL,
	[AllAddOrSub] [bigint] NULL,
	[AdvanceAmount] [bigint] NULL,
	[SecondAdvanceAmount] [bigint] NULL,
	[SecondAdvanceDate] [datetime] NULL,
	[IsCancel] [bit] NULL,
	[IsRetrieved] [bit] NULL,
	[OrderRetrieveDate] [datetime] NULL,
	[IsRepayment] [bit] NULL,
	[RepaymentDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
 CONSTRAINT [PK_tbl_OrderInvoice] PRIMARY KEY CLUSTERED 
(
	[OrderInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OrderInvoiceGemsItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_OrderInvoiceGemsItem](
	[OrderInvoiceGemsItemID] [varchar](20) NOT NULL,
	[OrderReceiveDetailID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[UnitPrice] [bigint] NULL,
	[Type] [varchar](50) NULL,
	[Amount] [bigint] NULL,
	[IsCustomerGem] [bit] NULL,
 CONSTRAINT [PK_tbl_OrderInvoiceGemsItem] PRIMARY KEY CLUSTERED 
(
	[OrderInvoiceGemsItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OrderReceiveDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_OrderReceiveDetail](
	[OrderReceiveDetailID] [varchar](20) NOT NULL,
	[OrderInvoiceID] [varchar](20) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[OrderRate] [int] NULL,
	[Length] [nvarchar](20) NULL,
	[Width] [nvarchar](20) NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[TotalGemTK] [decimal](18, 13) NULL,
	[TotalGemTG] [decimal](18, 13) NULL,
	[GoldPrice] [bigint] NULL,
	[GemPrice] [bigint] NULL,
	[DesignCharges] [bigint] NULL,
	[PlatingFee] [bigint] NULL,
	[WhiteCharges] [bigint] NULL,
	[MountingFee] [bigint] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[IsBarcode] [bit] NULL,
	[Design] [nvarchar](500) NULL,
	[IsDiamond] [bit] NULL,
	[GoldSmithID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_OrderReceiveDetail] PRIMARY KEY CLUSTERED 
(
	[OrderReceiveDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OrderReturnDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_OrderReturnDetail](
	[OrderInvoiceDetailID] [varchar](20) NOT NULL,
	[OrderReturnHeaderID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NULL,
	[SalesRate] [int] NULL,
	[GoldPrice] [int] NULL,
	[GemsPrice] [int] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [int] NULL,
	[IsOriginalFixedPrice] [bit] NULL,
	[OriginalFixedPrice] [bigint] NULL,
	[IsOriginalPriceGram] [bit] NULL,
	[OriginalPriceGram] [bigint] NULL,
	[OriginalPriceTK] [bigint] NULL,
	[OriginalGemsPrice] [bigint] NULL,
	[OriginalOtherPrice] [bigint] NULL,
	[PurchaseWasteTK] [decimal](18, 13) NULL,
	[PurchaseWasteTG] [decimal](18, 13) NULL,
	[IsReturn] [bit] NULL,
	[ItemTaxPer] [decimal](5, 2) NULL,
	[ItemTax] [int] NULL,
 CONSTRAINT [PK_tbl_OrderReturnDetail] PRIMARY KEY CLUSTERED 
(
	[OrderInvoiceDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OrderReturnGemsItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_OrderReturnGemsItem](
	[OrderReturnGemID] [varchar](20) NOT NULL,
	[OrderInvoiceDetailID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[SaleType] [varchar](20) NULL,
	[UnitPrice] [bigint] NULL,
	[Amount] [bigint] NULL,
	[GemsRemark] [nvarchar](50) NULL,
	[GemTax] [int] NULL,
	[GemTaxPer] [decimal](5, 2) NULL,
 CONSTRAINT [PK_tbl_OrderReturnGemsItem_1] PRIMARY KEY CLUSTERED 
(
	[OrderReturnGemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OrderReturnHeader]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_OrderReturnHeader](
	[OrderReturnHeaderID] [varchar](20) NOT NULL,
	[ReturnDate] [datetime] NULL,
	[OrderInvoiceID] [varchar](20) NULL,
	[AllTotalAmount] [bigint] NULL,
	[AllAddOrSub] [bigint] NULL,
	[FromGoldAmount] [bigint] NULL,
	[StaffID] [varchar](20) NULL,
	[IsAddGold] [bit] NULL,
	[Remark] [nvarchar](1000) NULL,
	[DiscountAmount] [bigint] NULL,
	[BalanceAmount] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[AdvanceAmount] [bigint] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[AllTaxAmt] [int] NULL,
	[AddGoldTaxPer] [decimal](18, 2) NULL,
	[AddGoldTax] [int] NULL,
CONSTRAINT [PK_tbl_OrderReturnHeader] PRIMARY KEY CLUSTERED 
(
	[OrderReturnHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PhotoPath]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhotoPath](
	[PhotoPath] [nvarchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PurchaseDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseDetail](
	[PurchaseDetailID] [varchar](20) NOT NULL,
	[PurchaseHeaderID] [varchar](20) NOT NULL,
	[SaleInvoiceDetailID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[BarcodeNo] [varchar](20) NULL,
	[OldSaleAmount] [bigint] NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[CurrentPrice] [int] NULL,
	[TotalTK] [decimal](18, 13) NULL,
	[TotalTG] [decimal](18, 13) NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[TotalGemTK] [decimal](18, 13) NULL,
	[TotalGemTG] [decimal](18, 13) NULL,
	[Length] [varchar](20) NULL,
	[QTY] [int] NULL,
	[IsDamage] [bit] NULL,
	[IsChange] [bit] NULL,
	[TotalAmount] [bigint] NULL,
	[IsClose] [bit] NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemTW] [decimal](18, 13) NULL,
	[FixType] [varchar](20) NULL,
	[ItemName] [nvarchar](100) NULL,
	[GoldPrice] [bigint] NULL,
	[GemsPrice] [bigint] NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[PWasteTK] [decimal](18, 13) NULL,
	[PWasteTG] [decimal](18, 13) NULL,
	[SaleRate] [int] NULL,
	[IsDone] [bit] NULL,
	[DoneAmount] [bigint] NULL,
	[IsSalePercent] [bit] NULL,
	[SalePercent] [int] NULL,
	[SalePercentAmount] [bigint] NULL,
	[AddSub] [bigint] NULL,
	[IsOrder] [bit] NULL,
	[IsShop] [bit] NULL,
	[SaleGemsItemID] [varchar](20) NULL,
	[ConsignmentSaleItemID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[PurchaseDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseFromSupplier]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseFromSupplier](
	[PurchaseFromSupplierID] [varchar](20) NOT NULL,
	[PDate] [datetime] NULL,
	[StaffID] [varchar](20) NULL,
	[SupplierID] [varchar](20) NULL,
	[Remark] [nvarchar](500) NULL,
	[Voucher] [varchar](20) NULL,
	[ExchangeRate] [bigint] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[DiscountRate] [int] NULL,
	[Expense] [bigint] NULL,
	[CommissionRate] [int] NULL,
	[PayType] [int] NULL,
	[DueDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
 CONSTRAINT [PK_PurchaseFromSupplier] PRIMARY KEY CLUSTERED 
(
	[PurchaseFromSupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseFromSupplierItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[tbl_PurchaseFromSupplierItem](
	[PurchaseFromSupplierItemID] [varchar](20) NOT NULL,
	[PurchaseFromSupplierID] [varchar](20) NULL,
	[OriginalCode] [varchar](20) NULL,
	[GramWeight] [decimal](18, 3) NULL,
	[QTY] [int] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[IsReject] [bit] NULL,
 CONSTRAINT [PK_tbl_PurchaseFromSupplierItem] PRIMARY KEY CLUSTERED 
(
	[PurchaseFromSupplierItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseGem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseGem](
	[PurchaseGemID] [varchar](20) NOT NULL,
	[PurchaseDetailID] [varchar](20) NOT NULL,
	[GemsCategoryID] [varchar](20) NOT NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemTW] [decimal](18, 13) NULL,
	[QTY] [int] NULL,
	[FixType] [varchar](20) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[Amount] [bigint] NULL,
	[IsOutGem] [bit] NULL,
	[Discount] [int] NULL,
 CONSTRAINT [PK_tbl_PurchaseGem] PRIMARY KEY CLUSTERED 
(
	[PurchaseGemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseGems]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseGems](
	[PurchaseGemsID] [varchar](20) NOT NULL,
	[PDate] [datetime] NULL,
	[StaffID] [varchar](20) NULL,
	[Customer] [nvarchar](20) NULL,
	[Address] [nvarchar](1000) NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[UserID] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_PurchaseGems] PRIMARY KEY CLUSTERED 
(
	[PurchaseGemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseGemsItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseGemsItem](
	[PurchaseGemsItemID] [varchar](20) NOT NULL,
	[PurchaseGemsID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[QTY] [int] NULL,
	[FixType] [int] NULL,
	[PurchaseRate] [decimal](18, 0) NULL,
	[Amount] [bigint] NULL,
	[Clarity] [varchar](50) NULL,
	[SizeMM] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_PurchaseGemsItem] PRIMARY KEY CLUSTERED 
(
	[PurchaseGemsItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseHeader]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseHeader](
	[PurchaseHeaderID] [varchar](20) NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[StaffID] [varchar](20) NOT NULL,
	[CustomerID] [varchar](20) NULL,
	[Address] [nvarchar](200) NULL,
	[Remark] [nvarchar](500) NULL,
	[AllTotalAmount] [bigint] NULL,
	[AllAddOrSub] [int] NULL,
	[AllPaidAmount] [bigint] NULL,
	[GoldPrice] [bigint] NULL,
	[GemsPrice] [bigint] NULL,
	[IsGem] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[IsChange] [bit] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[IsUpload] [bit] NULL,
 CONSTRAINT [PK_tbl_PurchaseHeader] PRIMARY KEY CLUSTERED 
(
	[PurchaseHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseInvoice]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseInvoice](
	[PurchaseInvoiceID] [varchar](20) NOT NULL,
	[PDate] [datetime] NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [nvarchar](50) NULL,
	[Address] [nvarchar](1000) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemName] [nvarchar](50) NULL,
	[Length] [varchar](100) NULL,
	[Qty] [int] NULL,
	[GoldQualityID] [varchar](20) NULL,
	[SubGoldQualityID] [varchar](20) NULL,
	[PurchaseRate] [bigint] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[GoldPrice] [bigint] NULL,
	[GemsPrice] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[IsExchange] [bit] NULL,
	[FromShopOrCustomer] [bit] NULL,
	[OldSaleInvoiceID] [varchar](20) NULL,
	[OldSaleAmount] [bigint] NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[TotalTK] [decimal](18, 13) NULL,
	[TotalTG] [decimal](18, 13) NULL,
	[IsDamage] [bit] NULL,
	[UserID] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_PurchaseInvoice] PRIMARY KEY CLUSTERED 
(
	[PurchaseInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseInvoiceGemsItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseInvoiceGemsItem](
	[PurchaseInvoiceGemsItemID] [varchar](20) NOT NULL,
	[PurchaseInvoiceID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[PurchaseRate] [bigint] NULL,
	[FixType] [int] NULL,
	[Amount] [bigint] NULL,
 CONSTRAINT [PK_tbl_PurchaseInvoiceGemsItem] PRIMARY KEY CLUSTERED 
(
	[PurchaseInvoiceGemsItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseOutItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseOutItem](
	[PurchaseOutID] [varchar](20) NOT NULL,
	[OutDate] [datetime] NOT NULL,
	[StaffID] [varchar](20) NOT NULL,
	[PurchaseHeaderID] [varchar](20) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_PurchaseOutItem] PRIMARY KEY CLUSTERED 
(
	[PurchaseOutID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_PurchaseOutItemDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_PurchaseOutItemDetail](
	[PurchaseOutDetailID] [varchar](20) NOT NULL,
	[PurchaseOutID] [varchar](20) NOT NULL,
	[PurchaseDetailID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](100) NULL,
	[DivideType] [varchar](50) NULL,
	[QTY] [int] NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemTW] [decimal](18, 13) NULL,
 CONSTRAINT [PK_tbl_PurchaseOutItemDetail] PRIMARY KEY CLUSTERED 
(
	[PurchaseOutDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_RecordOtherCash]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_RecordOtherCash](
	[RecordCashID] [varchar](20) NOT NULL,
	[VoucherNo] [varchar](20) NOT NULL,
	[CashTypeID] [varchar](20) NOT NULL,
	[ExchangeRate] [int] NULL,
	[Amount] [int] NULL,
 CONSTRAINT [PK_tbl_RecordOtherCash] PRIMARY KEY CLUSTERED 
(
	[RecordCashID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_RepairDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_RepairDetail](
	[RepairDetailID] [varchar](20) NOT NULL,
	[RepairID] [varchar](20) NULL,
	[IsFromShop] [bit] NULL,
	[BarcodeNo] [varchar](20) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[LengthOrWidth] [nvarchar](50) NULL,
	[CurrentPrice] [bigint] NULL,
	[Design] [nvarchar](300) NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[IsExit] [bit] NULL,
	[DetailRemark] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_RepairDetail] PRIMARY KEY CLUSTERED 
(
	[RepairDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_RepairHeader]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_RepairHeader](
	[RepairID] [varchar](20) NOT NULL,
	[RepairDate] [datetime] NULL,
	[ReturnDate] [datetime] NULL,
	[CustomerID] [varchar](20) NOT NULL,
	[StaffID] [varchar](20) NULL,
	[Remark] [nvarchar](300) NULL,
	[AdvanceRepairAmount] [bigint] NULL,
	[DueDate] [datetime] NULL,
	[IsAllReturn] [bit] NULL,
	[LastModifiedLoginUserName] [varchar](20) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
 CONSTRAINT [PK_tbl_RepairHeader] PRIMARY KEY CLUSTERED 
(
	[RepairID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ReturnAdvance]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[tbl_ReturnAdvanceItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[tbl_ReturnRepairHeader]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ReturnRepairHeader](
    [ReturnRepairID] [varchar](20) NOT NULL,
	[RepairID] [varchar](20) NULL,
	[ReturnDate] [datetime] NULL,
	[AllReturnTotalAmount] [bigint] NULL,
	[AllReturnAddOrSub] [bigint] NULL,
	[ReturnDiscountAmount] [bigint] NULL,
	[ReturnPaidAmount] [bigint] NULL,
	[Remark] [nvarchar](300) NULL,
	[LastModifiedLoginUserName] [varchar](20) NULL,
	[LastModifiedDate] [datetime] NULL,
	[AdvanceAmount] [bigint] NULL,
	[BalanceAmount] [bigint] NULL,
	[StaffID] [varchar](20) NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[LocationID] [varchar](20) NULL,
CONSTRAINT [PK_tbL_ReturnRepairHeader] PRIMARY KEY CLUSTERED 
(
	[ReturnRepairID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ReturnRepairDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ReturnRepairDetail](
	[ReturnRepairDetailID] [varchar](20) NOT NULL,
	[ReturnRepairID] [varchar](20) NULL,
	[RepairDetailID] [varchar](20) NULL,
	[ChangeSaleRate] [bigint] NULL,
	[ReturnItemTK] [decimal](18, 13) NULL,
	[ReturnItemTG] [decimal](18, 13) NULL,
	[ReturnGoldTK] [decimal](18, 13) NULL,
	[ReturnGoldTG] [decimal](18, 13) NULL,
	[OrgGoldTK] [decimal](18, 13) NULL,
	[OrgGoldTG] [decimal](18, 13) NULL,
	[OrgGemTK] [decimal](18, 13) NULL,
	[OrgGemTG] [decimal](18, 13) NULL,
	[ReturnGemTK] [decimal](18, 13) NULL,
	[ReturnGemTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[DesignCharges] [bigint] NULL,
	[WhiteCharges] [bigint] NULL,
	[PlatingCharges] [bigint] NULL,
	[MountingCharges] [bigint] NULL,
	[ReturnGoldPrice] [bigint] NULL,
	[ReturnGemPrice] [bigint] NULL,
	[ReturnTotalAmount] [bigint] NULL,
	[ReturnAddOrSub] [bigint] NULL,
 CONSTRAINT [PK_tbl_ReturnRepairDetail] PRIMARY KEY CLUSTERED 
(
	[ReturnRepairDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ReturnRepairGem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ReturnRepairGem](
	[ReturnRepairGemID] [varchar](20) NOT NULL,
	[ReturnRepairDetailID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[Description] [nvarchar](20) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[QTY] [int] NULL,
	[Type] [varchar](20) NULL,
	[UnitPrice] [bigint] NULL,
	[Amount] [bigint] NULL,
	[IsNewGems] [bit] NULL,
 CONSTRAINT [PK_tbl_ReturnRepairGem] PRIMARY KEY CLUSTERED 
(
	[ReturnRepairGemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleGems]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleGems](
	[SaleGemsID] [varchar](20) NOT NULL,
	[SDate] [datetime] NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [varchar](20) NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[Remark] [nvarchar](1000) NULL,
	[LocationID] [varchar](20) NULL,
	[UserID] [varchar](50) NULL,
	[DiscountAmount] [bigint] NULL,
	[PromotionDiscount] [int] NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[tbl_SaleGems] ADD [PurchaseHeaderID] [varchar](20) NULL
ALTER TABLE [dbo].[tbl_SaleGems] ADD [PurchaseAmount] [bigint] NULL
ALTER TABLE [dbo].[tbl_SaleGems] ADD [IsOtherCash] [bit] NULL
ALTER TABLE [dbo].[tbl_SaleGems] ADD [OtherCashAmount] [bigint] NULL
 CONSTRAINT [PK_tbl_SaleGems] PRIMARY KEY CLUSTERED 
(
	[SaleGemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleGemsItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleGemsItem](
	[SaleGemsItemID] [varchar](20) NOT NULL,
	[SaleGemsID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[FixType] [int] NULL,
	[SaleRate] [bigint] NULL,
	[Amount] [bigint] NULL,
	[Clarity] [varchar](50) NULL,
	[SizeMM] [varchar](50) NULL,
	[IsReturn] [bit] NULL,
 CONSTRAINT [PK_tbl_SaleGemsItem] PRIMARY KEY CLUSTERED 
(
	[SaleGemsItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleInvoice]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleInvoice](
	[SaleInvoiceID] [varchar](20) NOT NULL,
	[SDate] [datetime] NOT NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [varchar](50) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NULL,
	[ItemName] [nvarchar](50) NULL,
	[Length] [varchar](20) NULL,
	[Width] [varchar](100) NULL,
	[SalesRate] [bigint] NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[TotalTK] [decimal](18, 13) NULL,
	[TotalTG] [decimal](18, 13) NULL,
	[TotalPayment] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[DiscountAmount] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[GoldPrice] [bigint] NULL,
	[GemsPrice] [bigint] NULL,
	[PlatingCharges] [bigint] NULL,
	[MountingCharges] [bigint] NULL,
	[WhiteCharges] [bigint] NULL,
	[DesignCharges] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LocationID] [varchar](20) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[IsSalesReturn] [bit] NULL,
	[DoneRate] [int] NULL,
	[SaleGramRate] [bigint] NULL,
	[UserID] [varchar](50) NULL,
	[ItemTG] [decimal](18, 3) NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[DImage] [image] NULL,
 CONSTRAINT [PK_tbl_SaleInvoice] PRIMARY KEY CLUSTERED 
(
	[SaleInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleInvoiceDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleInvoiceDetail](
	[SaleInvoiceDetailID] [varchar](20) NOT NULL,
	[SaleInvoiceHeaderID] [varchar](20) NOT NULL,
	[ForSaleID] [varchar](20) NOT NULL,
	[ItemCode] [varchar](20) NOT NULL,
	[SalesRate] [int] NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[GoldPrice] [int] NULL,
	[GemsPrice] [int] NULL,
	[IsFixPrice] [bit] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [int] NULL,
	[IsOriginalFixedPrice] [bit] NULL,
	[OriginalFixedPrice] [bigint] NULL,
	[IsOriginalPriceGram] [bit] NULL,
	[OriginalPriceGram] [bigint] NULL,
	[OriginalPriceTK] [bigint] NULL,
	[OriginalGemsPrice] [bigint] NULL,
	[OriginalOtherPrice] [bigint] NULL,
	[PurchaseWasteTK] [decimal](18, 13) NULL,
	[PurchaseWasteTG] [decimal](18, 13) NULL,
	[IsReturn] [bit] NULL,
	[IsSaleReturn] [bit] NULL,
	[ItemTaxPer] [decimal](5, 2) NULL,
	[ItemTax] [int] NULL,
	[WhiteCharges] [int] NULL,
	[PlatingCharges] [int] NULL,
	[MountingCharges] [int] NULL,
	[DesignCharges] [int] NULL,
	[DesignChargesRate] [int] NULL,
	[SellingRate] [int] NULL,
	[SellingAmt] [bigint] NULL,
 CONSTRAINT [PK_tbl_SaleInvoiceDetail_1] PRIMARY KEY CLUSTERED 
(
	[SaleInvoiceDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SaleInvoiceHeader]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SaleInvoiceHeader](
	[SaleInvoiceHeaderID] [varchar](20) NOT NULL,
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
	[IsAdvance] [bit] NULL,
	[EntryAdvanceDate] [datetime] NULL,
	[AllAdvanceAmount] [bigint] NULL,
	[IsCancel] [bit] NULL,
	[CancelDate] [datetime] NULL,
	[IsOtherCash] [bit] NULL,
	[OtherCashAmount] [int] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[IsSync] [bit] NULL,
	[AllTaxAmt] [int] NULL,
	[SRTaxPer] [decimal](18, 2) NULL,
	[SRTaxAmt] [int] NULL,
	[MemberID] [varchar](20) NULL,
	[MemberCode] [varchar](50) NULL,
	[MemberName] [nvarchar](100) NULL,
	[RedeemID] [nvarchar](100) NULL,
	[TopupPoint] [int] NULL,
	[TopupValue] [int] NULL,
	[RedeemPoint] [int] NULL,
	[RedeemValue] [int] NULL,
	[IsRedeemInvoice] [Bit] NULL,
	[MemberDis] [int] NULL,
	[MemberDiscountAmt] [int] NULL,
	[TransactionID] [nvarchar](100) NULL,
	[InvoiceStatus] [int] NULL,
 CONSTRAINT [PK_tbl_SaleInvoiceHeader] PRIMARY KEY CLUSTERED 
(
	[SaleInvoiceHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalesInvoiceGemItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalesInvoiceGemItem](
	[SalesInvoiceGemItemID] [varchar](20) NOT NULL,
	[SaleInvoiceDetailID] [varchar](20) NOT NULL,
	[GemsCategoryID] [varchar](20) NOT NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](50) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[Type] [varchar](20) NULL,
	[UnitPrice] [bigint] NULL,
	[Amount] [bigint] NULL,
	[GemsRemark] [nvarchar](100) NULL,
	[GemTax] [int] NULL,
	[GemTaxPer] [decimal](5, 2) NULL,
 CONSTRAINT [PK_tbl_SaleInvoiceGem] PRIMARY KEY CLUSTERED 
(
	[SalesInvoiceGemItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalesOrder]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalesOrder](
	[SaleOrderID] [varchar](20) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[IsReturn] [bit] NULL,
	[OrderRetrieveDate] [smalldatetime] NULL,
	[StaffID] [varchar](20) NULL,
	[Customer] [nvarchar](20) NULL,
	[Address] [nvarchar](200) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NULL,
	[ItemName] [nvarchar](50) NULL,
	[Length] [varchar](20) NULL,
	[SalesRate] [bigint] NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[TotalTK] [decimal](18, 13) NULL,
	[TotalTG] [decimal](18, 13) NULL,
	[TotalPayment] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[AdvanceAmount] [bigint] NULL,
	[PaidAmount] [bigint] NULL,
	[DiscountAmount] [bigint] NULL,
	[GoldPrice] [bigint] NULL,
	[GemsPrice] [bigint] NULL,
	[DesignCharges] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
	[CounterID] [varchar](20) NULL,
	[LocationID] [varchar](20) NULL,
	[IsSalesReturn] [bit] NULL,
	[Width] [varchar](100) NULL,
	[IsRepayment] [bit] NULL,
	[IsCancel] [bit] NULL,
	[SecondAdvAmount] [bigint] NULL,
	[SecondAdvDate] [datetime] NULL,
	[SaleGramRate] [bigint] NULL,
 CONSTRAINT [PK_tbl_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[SaleOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalesOrderGemsItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalesOrderGemsItem](
	[SaleOrderItemID] [varchar](20) NOT NULL,
	[SaleOrderID] [varchar](20) NULL,
	[GemsCategoryID] [varchar](20) NULL,
	[GemsName] [nvarchar](50) NULL,
	[GemsTK] [decimal](18, 13) NULL,
	[GemsTG] [decimal](18, 13) NULL,
	[YOrCOrG] [varchar](20) NULL,
	[GemsTW] [decimal](18, 13) NULL,
	[Qty] [int] NULL,
	[UnitPrice] [bigint] NULL,
	[Type] [varchar](20) NULL,
	[Amount] [bigint] NULL,
 CONSTRAINT [PK_tbl_SalesOrderGemsItem] PRIMARY KEY CLUSTERED 
(
	[SaleOrderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalesVolume]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalesVolume](
	[SalesVolumeID] [varchar](20) NOT NULL,
	[SaleDate] [datetime] NULL,
	[StaffID] [varchar](20) NULL,
	[CustomerID] [varchar](20) NULL,
	[Remark] [nvarchar](500) NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[DiscountAmount] [bigint] NULL,
	[PromotionDiscount] [int] NULL,
	[PaidAmount] [bigint] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedLoginUserName] [varchar](50) NULL,
	[LastModifiedDate] [smalldatetime] NULL,
	[PurchaseHeaderID] [varchar](20) NULL,
	[PurchaseAmount] [varchar](20) NULL,
	[IsDelete] [bit] NULL,
	[IsSync] [bit] NULL,
	[IsSolidVolume] [Bit] NULL,
	[MemberID] [varchar](20) NULL,
	[MemberCode] [varchar](50) NULL,
	[MemberName] [nvarchar](100) NULL,
	[RedeemID] [nvarchar](100) NULL,
	[TopupPoint] [int] NULL,
	[TopupValue] [int] NULL,
	[RedeemPoint] [int] NULL,
	[RedeemValue] [int] NULL,
	[IsRedeemInvoice] [Bit] NULL,
	[MemberDis] [int] NULL,
	[MemberDiscountAmt] [int] NULL,
	[TransactionID] [nvarchar](100) NULL,
	[InvoiceStatus] [int] NULL,
 CONSTRAINT [PK_tbl_SaleVolume] PRIMARY KEY CLUSTERED 
(
	[SalesVolumeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SalesVolumeDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SalesVolumeDetail](
	[SalesVolumeDetailID] [varchar](20) NOT NULL,
	[SalesVolumeID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[ItemCode] [varchar](20) NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[ItemNameID] [varchar](20) NULL,
	[Length] [varchar](20) NULL,
	[SalesRate] [bigint] NULL,
	[QTY] [int] NULL,
	[ItemTK] [decimal](18, 13) NULL,
	[ItemTG] [decimal](18, 13) NULL,
	[WasteTK] [decimal](18, 13) NULL,
	[WasteTG] [decimal](18, 13) NULL,
	[IsFixPrice] [bit] NULL,
	[FixPrice] [bigint] NULL,
	[GoldPrice] [bigint] NULL,
	[TotalAmount] [bigint] NULL,
	[AddOrSub] [bigint] NULL,
	[DesignCharges] [int] NULL,
	[DesignChargesRate] [int] NULL,
 CONSTRAINT [PK_tbl_SaleVolumeDetail] PRIMARY KEY CLUSTERED 
(
	[SalesVolumeDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Setting]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Setting](
	[KeyName] [varchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[KeyValue] [nvarchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Staff]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Staff](
	[StaffID] [varchar](20) NOT NULL,
	[Staff] [nvarchar](50) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_Staff] PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_StandardRate]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_StandardRate](
	[DefineID] [varchar](20) NOT NULL,
	[DefineDateTime] [datetime] NULL,
	[GoldQualityID] [varchar](20) NULL,
	[SalesRate] [bigint] NULL,
	[PurchaseRate] [bigint] NULL,
	[ExchangeRate] [bigint] NULL,
	[Remark] [nvarchar](1000) NULL,
	[PercentPurchaseRate] [int] NULL,
	[PercentExchangeRate] [int] NULL,
	[PercentDamageRate] [int] NULL,
	[DamageRate] [int] NULL,
	[SaleRatePerGram] [int] NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_StandardRate] PRIMARY KEY CLUSTERED 
(
	[DefineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Supplier]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Supplier](
	[SupplierID] [varchar](20) NOT NULL,
	[SupplierCode] [varchar](20) NULL,
	[SupplierName] [nvarchar](50) NULL,
	[SupplierAddress] [nvarchar](200) NULL,
	[Email] [varchar](50) NULL,
	[Website] [varchar](50) NULL,
	[PhoneNo] [varchar](20) NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[IsDelete] [bit] NULL,
	[IsUpload] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Transfer]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Transfer](
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
	[CurrentLocationID] [varchar] (20) NULL,
 CONSTRAINT [PK_tbl_Transfer] PRIMARY KEY CLUSTERED 
(
	[TransferID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TransferItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_TransferItem](
	[TransferItemID] [varchar](20) NOT NULL,
	[TransferID] [varchar](20) NULL,
	[ForSaleID] [varchar](20) NULL,
	[OriginalFixedPrice] [int] NULL,
	[OriginalPriceGram] [int] NULL,
	[OriginalPriceTK] [int] NULL,
	[OriginalGemsPrice] [int] NULL,
	[PriceCode] [varchar](10) NULL,
	[FixPrice] [int] NULL,
	[isReturn] [bit] NULL,
 CONSTRAINT [PK_tbl_TransferItem] PRIMARY KEY CLUSTERED 
(
	[TransferItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Version]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Version](
	[VersionNo] [varchar](20) NULL,
	[VersionDate] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_VoucherSetting]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_VoucherSetting](
	[TitleType] [varchar](50) NULL,
	[Title] [nvarchar](200) NULL,
	[FontName] [varchar](50) NULL,
	[Bold] [bit] NULL,
	[Italic] [bit] NULL,
	[FontSize] [int] NULL,
	[FontColor] [nvarchar](50) NULL,
	[Photo] [nvarchar](50) NULL,
	[FontR] [bigint] NULL,
	[FontG] [bigint] NULL,
	[FontB] [bigint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_WasteSetupDetail]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[tbl_WasteSetupHeader]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_WasteSetupHeader](
	[WasteSetupHeaderID] [varchar](20) NOT NULL,
	[ItemCategoryID] [varchar](20) NULL,
	[ItemNameID] [varchar](20) NULL,
	[IsUpload] [bit] NULL,
	[IsDelete] [bit] NULL,
	[LocationID] [varchar](20) NULL,
	[LastModifiedDate] DateTime NULL,
 CONSTRAINT [PK_tbl_WasteSetUp] PRIMARY KEY CLUSTERED 
(
	[WasteSetupHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_WholesaleInvoice]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[Remark] [nvarchar](500) NULL,
	[TotalDesignCharges] [bigint] NULL,
	[DisPercent] [bigint] NULL,
	[MemberID] [varchar](20) NULL,
	[MemberCode] [varchar](50) NULL,
	[MemberName] [nvarchar](100) NULL,
	[RedeemID] [nvarchar](100) NULL,
	[TopupPoint] [int] NULL,
	[TopupValue] [int] NULL,
	[RedeemPoint] [int] NULL,
	[RedeemValue] [int] NULL,
	[IsRedeemInvoice] [Bit] NULL,
	[MemberDis] [int] NULL,
	[MemberDiscountAmt] [int] NULL,
	[TransactionID] [nvarchar](100) NULL,
	[InvoiceStatus] [int] NULL,
 CONSTRAINT [PK_tbl_WholesaleInvoice] PRIMARY KEY CLUSTERED 
(
	[WholesaleInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_WholesaleInvoiceItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[GoldPrice] [int] NULL,
	[FixPrice] [int] NULL,
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[DesignCharges] [int] NULL,
	[DesignChargesRate] [int] NULL,
	[ItemDisPercent] [int] NULL,
	[ItemDisAmount] [int] NULL,
	[GemsPrice] [int] NULL,
 CONSTRAINT [PK_tbl_WholesaleInvoiceItem] PRIMARY KEY CLUSTERED 
(
	[WholesaleInvoiceItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_WholesaleReturn]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
/****** Object:  Table [dbo].[tbl_WholesaleReturnItem]    Script Date: 6/29/2019 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[GoldTK] [decimal](18, 13) NULL,
	[GoldTG] [decimal](18, 13) NULL,
	[ItemNameID] [varchar](20) NULL,
	[GoldQualityID] [varchar](20) NULL,
	[GoldPrice] [int] NULL,
	[FixPrice] [nchar](10) NULL,
 CONSTRAINT [PK_tbl_WholesaleReturnItem] PRIMARY KEY CLUSTERED 
(
	[WholesaleReturnItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
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
	[DonePercent] [int] NULL,
	[NetAmount] [int] NULL
 CONSTRAINT [PK_tbl_MortgagePaybackItem] PRIMARY KEY CLUSTERED 
(
	[MortgagePaybackItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
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
	[LastModifiedUserName] [varchar](200) NULL,
	[PurchaseRate] [decimal](19,1) NULL
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
	[MemberID] [varchar](20) NULL,
	[MemberCode] [varchar](50) NULL,
	[MemberName] [nvarchar](100) NULL,
	[RedeemID] [nvarchar](100) NULL,
	[TopupPoint] [int] NULL,
	[TopupValue] [int] NULL,
	[RedeemPoint] [int] NULL,
	[RedeemValue] [int] NULL,
	[IsRedeemInvoice] [Bit] NULL,
	[MemberDis] [int] NULL,
	[MemberDiscountAmt] [int] NULL,
	[TransactionID] [nvarchar](100) NULL,
	[InvoiceStatus] [int] NULL,
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
INSERT INTO tb_GE_UserLevel (SysID, UserLevel, Description, IsAdministrator, Remark) VALUES (1, 'Administrator', 'Administrator', 1, 'Built In Administrator Level')
INSERT INTO tbl_key_generate (GenerateKeyType, GenerateFormat, GenerateID,GenerateOn) VALUES ('UserLevel', '0', 1,'UserLevelID')
INSERT INTO tbl_Setting (KeyName, Description, KeyValue) VALUES ('CustomerReferenceNo', 'Purchase License Name', 'YYVRGQLQNOEBV7B') 
INSERT INTO tbl_Setting (KeyName, Description, KeyValue) VALUES ('LicensedDay', 'Purchase Licensed Day', '1') 
INSERT INTO tb_GE_SystemUser (UserID, UserName, [Password], UserLevelID, Remark) VALUES ('Administrator', 'Administrator', '975H4RAcGAw=', 1, 'Built In Administrator Account')
INSERT INTO tbl_Version (VersionNo, VersionDate) VALUES ('6.4800', '2020-08-26')
INSERT INTO tbl_BarcodeSetting (PaperWidth,PaperHeight,X,Y,Height,Narrow,Wide,PrinterName, IsLocationName,EngName,MMName,RightPositionX,IsIncludeGemWgt,IsIncludeGemPrice,IsPrefix,IsLength,IsFixItem,IsFixGold,IsFixGemQTY,IsFixGemWeight,IsAllDetail,IsPriceCode,IsOriginalCode,IsWaste,IsDescription,IsGram,IsShowGW,IsFixPrice,LeftFontSize,RightFontSize) VALUES ('79','13','20','8','25','1','2','TSC TTP-244 Plus', 1,'GLOBAL','','180',0,1,0,0,1,0,1,0,1,0,0,0,0,0,0,0,19,19)
INSERT INTO tbl_GlobalSetting (Photo, DatabaseSharePath, IsCarat,IsReuseBarcode, AllowDis, IsCash, IsExactPrice, DiffPurchaseRate, DiffChangeRate, IsSpeedEntry,DecimalFormat,IsAllowUpdate,InterestRate,InterestPeriod,EnablePaidAmount,IsAllowSaleReturn,IsAllowSale,IsAllowStock,QRCode,IsUsedSettingPeriod,AllowStockWeight,IsOneMonthCalculation,OverDay,IsHoToBranch,MachineType,Prefix,Postfix,IsHoMaster,IsFixPrice,IsUseMember,IsMemberCustomer,RegName) VALUES ('', '', 0,0,0,1,0,'20000','10000',0,1,0,1,1,0,0,0,0,'',0,0.0,0,0,1,'Default',0,0,1,0,0,0,'')
INSERT INTO tbl_CompanyProfile(CompanyID, CompanyName, Telephone, Email, Address, WebSite, Fax ,HeadOffice) VALUES ( '01', 'Global Wave', '01560067', 'www.globalwavetechnology.com.mm', 'Yangon', '', '', '1')
INSERT INTO tbl_Measurement (FromMeasurement, ToMeasurement, Equivalent) VALUES ('Gram', 'Karat', '5.0000000000000') 
INSERT INTO tbl_Measurement (FromMeasurement, ToMeasurement, Equivalent) VALUES ('Karat', 'Yati', '1.1000000000000') 
INSERT INTO tbl_Measurement (FromMeasurement, ToMeasurement, Equivalent) VALUES ('Yati', 'B', '20.0000000000000') 
INSERT INTO tbl_Measurement (FromMeasurement, ToMeasurement, Equivalent) VALUES ('Kyat', 'Gram', '16.6000000000000') 
INSERT INTO tbl_Measurement (FromMeasurement, ToMeasurement, Equivalent) VALUES ('B', 'P', '8.0000000000000') 
INSERT INTO tbl_Measurement (FromMeasurement, ToMeasurement, Equivalent) VALUES ('P', 'Y', '8.0000000000000') 
INSERT tbl_KeywordHeader(KeywordID, KeywordName) VALUES (1, N'DonePercent')
INSERT tbl_KeywordItem (ItemID, KeywordID, ItemName) VALUES (1, 1, N'3')
INSERT INTO tbl_key_generate (GenerateKeyType, GenerateFormat, GenerateID,GenerateOn) VALUES ('KeywordItem', '0', 1,'KeywordItemID')
GO
