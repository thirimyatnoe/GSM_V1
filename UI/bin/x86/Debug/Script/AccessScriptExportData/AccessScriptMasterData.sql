CREATE TABLE [tbl_GoldQuality](
	[GoldQualityID] TEXT(20) WITH COMPRESSION,
	[GoldQuality] TEXT(100) WITH COMPRESSION,
	[Prefix] TEXT(20) WITH COMPRESSION,
	[IsGramRate] BIT,
	[IsSolidGold] BIT,	
	[MultiplyBy] Decimal(18,2),
	[DividedBy] Decimal(18,2),
	[IsUpload] BIT,
	[IsDelete] BIT,
	[LocationID] Text(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME,
	[BarcodeStatus] Bit
)
GO
CREATE TABLE [tbl_ItemCategory](
	[ItemCategoryID] TEXT(20) WITH COMPRESSION,
	[ItemCategory] TEXT(50) WITH COMPRESSION,
	[Prefix] TEXT(20) WITH COMPRESSION,
	[ItemTaxPer] DECIMAL(5,2),
	[IsDelete] Bit,
	[IsUpload] Bit,
	[LocationID] TEXT(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME
)
GO
CREATE TABLE [tbl_ItemName](
	[ItemNameID] TEXT(20) WITH COMPRESSION,
	[ItemCategoryID] TEXT(20) WITH COMPRESSION,
	[ItemName] TEXT(100) WITH COMPRESSION,
	[ItemPhoto] TEXT(100) WITH COMPRESSION,
	[IsDelete] Bit,
	[IsUpload] Bit,
	[LocationID] TEXT(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME
)
GO
CREATE TABLE [tbl_GoldSmith](
	[GoldSmithID] TEXT(20) WITH COMPRESSION,
	[GoldSmithCode] TEXT(20) WITH COMPRESSION,
	[Name] TEXT(50) WITH COMPRESSION,
	[Address] TEXT(200) WITH COMPRESSION,
	[Phone] TEXT(50) WITH COMPRESSION,
	[Remark] TEXT(200) WITH COMPRESSION,
	[IsInactive] Bit,
	[LastModifiedDate] DATETIME,
	[IsDelete] Bit,
	[IsUpload] Bit,
	[LocationID] TEXT(20) WITH COMPRESSION
)
GO
CREATE TABLE [tbl_GemsCategory](
	[GemsCategoryID] TEXT(20) WITH COMPRESSION,
	[GemsCategory] TEXT(50) WITH COMPRESSION,
	[StoneType] TEXT(50) WITH COMPRESSION,
	[GemTaxPer] Decimal(5,2),
	[IsDelete] Bit,
	[IsUpload] Bit,
	[LocationID] TEXT(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME
)
GO
CREATE TABLE [tbl_WasteSetupDetail](
	[WasteSetupDetailID] TEXT(20) WITH COMPRESSION,
	[WasteSetupHeaderID] TEXT(20) WITH COMPRESSION,
	[GoldQualityID] TEXT(20) WITH COMPRESSION,
	[MinNetWeightTK] decimal(18,13),
	[MinNetWeightTG] decimal(18,13),
	[MaxNetWeightTK] decimal(18,13),
	[MaxNetWeightTG] decimal(18,13),
	[MinWeightTKForSale] decimal(18,13),
	[MinWeightTGForSale] decimal(18,13)
)
GO
CREATE TABLE [tbl_WasteSetupHeader](
	[WasteSetupHeaderID] TEXT(20) WITH COMPRESSION,
	[ItemCategoryID] TEXT(20) WITH COMPRESSION,
	[ItemNameID] TEXT(50) WITH COMPRESSION,
	[IsUpload] Bit,
	[IsDelete] Bit,
	[LocationID] TEXT(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME
)
GO
CREATE TABLE [tbl_Location](
	[LocationID] TEXT(20) WITH COMPRESSION,
	[Location] TEXT(100) WITH COMPRESSION,
	[Address] TEXT(200) WITH COMPRESSION,
	[Phone] TEXT(100) WITH COMPRESSION,
	[Remark15] TEXT(200) WITH COMPRESSION,
	[RemarkDone] TEXT(200) WITH COMPRESSION,
	[IsDelete] Bit,
	[IsUpload] Bit,
	[CurrentLocationID] TEXT(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME,
	[IsHeadOffice] Bit
)
GO