CREATE TABLE [tbl_Customer](
	[CustomerID] TEXT(20) WITH COMPRESSION,
	[CustomerCode] TEXT(20) WITH COMPRESSION,
	[CustomerName] TEXT(50) WITH COMPRESSION,
	[CustomerAddress] TEXT(200) WITH COMPRESSION,
	[CustomerTel] TEXT(20) WITH COMPRESSION,
	[Remark] TEXT(100) WITH COMPRESSION,
	[IsInactive] Bit,
	[LastModifiedDate] DATETIME,
	[IsDelete] Bit,
	[IsUpload] Bit,
	[DOB] date,
	[LocationID] TEXT(20) WITH COMPRESSION,
	[NRC] TEXT(200) WITH COMPRESSION
)
GO
CREATE TABLE [tbl_Staff](
	[StaffID] TEXT(20) WITH COMPRESSION,
	[Staff] TEXT(100) WITH COMPRESSION,
	[IsUpload] BIT,
	[IsDelete] BIT,
	[LocationID] Text(20) WITH COMPRESSION,
	[LastModifiedDate] DATETIME 
)
GO
CREATE TABLE [tbl_Supplier](
	[SupplierID] TEXT(20) WITH COMPRESSION,
	[SupplierCode] TEXT(20) WITH COMPRESSION,
	[SupplierName] TEXT(50) WITH COMPRESSION,
	[SupplierAddress] TEXT(200) WITH COMPRESSION,
	[Email] TEXT(50) WITH COMPRESSION,
	[Website] TEXT(50) WITH COMPRESSION,
	[PhoneNo] Text(50) WITH COMPRESSION,
	[Remark] Text(200) WITH COMPRESSION,
	[IsDelete] BIT,
	[IsUpload] BIT,
	[LocationID] Text(20) WITH COMPRESSION,
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