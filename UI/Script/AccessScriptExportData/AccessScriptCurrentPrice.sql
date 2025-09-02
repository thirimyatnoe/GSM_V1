CREATE TABLE [tbl_StandardRate](
	[DefineID] TEXT(20) WITH COMPRESSION,
	[DefineDateTime] DATETIME,
	[GoldQualityID] TEXT(50) WITH COMPRESSION,
	[SalesRate] Numeric,
	[PurchaseRate] Numeric,
	[ExchangeRate] Numeric,	
	[Remark] TEXT(200) WITH COMPRESSION,
	[PercentPurchaseRate] Numeric,
	[PercentExchangeRate] Numeric,
	[PercentDamageRate] Numeric,
	[DamageRate] Numeric,
	[SaleRatePerGram] Numeric,
	[IsUpload] Numeric,
	[IsDelete] Numeric,
	[LocationID] TEXT(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME
)
GO
CREATE TABLE [tbl_Transaction] (
	[TransactionName] TEXT(255) WITH COMPRESSION,
	[FromDate] DATETIME NULL,
	[ToDate]  DATETIME NULL,
	[CompanyID] TEXT(50) WITH COMPRESSION
)
GO