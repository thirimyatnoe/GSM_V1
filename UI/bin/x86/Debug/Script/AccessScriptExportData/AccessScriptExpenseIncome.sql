CREATE TABLE[tbl_DailyExpense](
	[DailyExpenseID] TEXT(20) WITH COMPRESSION,
	[ExpenseDate] DateTime,
	[Remark] TEXT(200) WITH COMPRESSION,
	[TotalAmount] Numeric,
	[LocationID] TEXT(20) WITH COMPRESSION,
	[ReturnAmount] Numeric,
	[LastModifiedLoginUserName] TEXT(50) WITH COMPRESSION,
	[LastModifiedDate] DateTime,
	[IsDelete] Bit,
	[IsSync] Bit,
	[IsBank] Bit
)
GO
CREATE TABLE [tbl_DailyExpenseItem](
	[DailyExpenseItemID] TEXT(20) with COMPRESSION,
	[DailyExpenseID] TEXT(20) with COMPRESSION,
	[Description] TEXT(200) with COMPRESSION,
	[QTY] Numeric,
	[UnitPrice] Numeric,
	[Amount] Numeric,
	[Remark] TEXT(200) with COMPRESSION,
	[LastModifiedLoginUserName] TEXT(50) with COMPRESSION,
	[LastModifiedDate] DateTime
)
GO
CREATE TABLE [tbl_DailyIncome](
	[DailyIncomeID] TEXT(20) WITH COMPRESSION,
	[IncomeDate] DateTime,
	[Remark] TEXT(200) WITH COMPRESSION,
	[TotalAmount] Numeric,
	[LocationID] TEXT(50) WITH COMPRESSION,
	[LastModifiedLoginUserName] TEXT(50) WITH COMPRESSION,
	[LastModifiedDate] DateTime,
	[IsDelete] Bit,
	[IsSync] Bit,
	[IsBank] Bit
)
GO
CREATE TABLE [tbl_DailyIncomeItem](
	[DailyIncomeItemID] TEXT(20) WITH COMPRESSION,
	[DailyIncomeID] TEXT(20) WITH COMPRESSION,
	[Description] TEXT(200) WITH COMPRESSION,
	[QTY] NUMERIC,
	[UnitPrice] Numeric,
	[Amount] Numeric,
	[Remark] TEXT(200) WITH COMPRESSION,
	[LastModifiedLoginUserName] TEXT(50) WITH COMPRESSION,
	[LastModifiedDate] DateTime
)
GO
CREATE TABLE [tbl_Transaction] (
	[TransactionName] TEXT(255) WITH COMPRESSION,
	[FromDate] DATETIME NULL,
	[ToDate]  DATETIME NULL,
	[CompanyID] TEXT(50) WITH COMPRESSION
)
GO



