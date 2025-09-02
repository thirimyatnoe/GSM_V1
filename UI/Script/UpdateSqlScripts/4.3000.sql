---WholeSaleRemark---
Update tbl_Version Set VersionNo='4.3000',VersionDate=GETDATE();
GO
---WholeSaleRemark---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholeSaleInvoice' and column_name = 'Remark')
alter table tbl_WholeSaleInvoice add Remark nvarchar(500)
GO
update tbl_WholeSaleInvoice set Remark='';
GO
--For Order Return---
IF  EXISTS (SELECT  B.COLUMN_NAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS A, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE B
WHERE CONSTRAINT_TYPE = 'FOREIGN KEY' AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME And B.Column_Name='OrderReturnHeaderID' And a.Table_Name='tbl_OrderReturnDetail')
ALTER TABLE tbl_OrderReturnDetail DROP CONSTRAINT FK_tbl_OrderReturnDetail_tbl_OrderReturnHeader
GO
ALTER TABLE dbo.tbL_OrderReturnHeader ADD tempID int NULL;
GO
UPDATE tbl_OrderReturnHeader SET tempID=OrderReturnHeaderID;
GO
ALTER TABLE dbo.tbL_OrderReturnHeader DROP CONSTRAINT PK_tbL_OrderReturnHeader;
GO
ALTER TABLE dbo.tbL_OrderReturnHeader DROP COLUMN OrderReturnHeaderID;
GO
ALTER TABLE dbo.tbL_OrderReturnHeader ADD OrderReturnHeaderID Varchar(20) NULL;
GO
UPDATE dbo.tbL_OrderReturnHeader SET OrderReturnHeaderID=tempID;
GO
ALTER TABLE dbo.tbL_OrderReturnHeader ALTER COLUMN OrderReturnHeaderID varchar(20) NOT NULL;
GO
ALTER TABLE dbo.tbL_OrderReturnHeader DROP COLUMN tempID;
GO
ALTER TABLE dbo.tbL_OrderReturnHeader ADD CONSTRAINT PK_tbl_OrderReturnHeader PRIMARY KEY CLUSTERED (OrderReturnHeaderID) ON [PRIMARY]
GO
----For Return Repair ---------
-------Remove Foreign Key-------
IF  EXISTS (SELECT  B.COLUMN_NAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS A, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE B
WHERE CONSTRAINT_TYPE = 'FOREIGN KEY' AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME And B.Column_Name='ReturnRepairID' And a.Table_Name='tbl_ReturnRepairDetail')
ALTER TABLE tbl_ReturnRepairDetail DROP CONSTRAINT FK_tbl_ReturnRepairDetail_tbl_ReturnRepairHeader
GO
ALTER TABLE dbo.tbL_ReturnRepairHeader ADD tempID int NULL
GO
UPDATE tbL_ReturnRepairHeader SET tempID=ReturnRepairID
GO
ALTER TABLE dbo.tbL_ReturnRepairHeader DROP CONSTRAINT PK_tbL_ReturnRepairHeader
GO
ALTER TABLE dbo.tbL_ReturnRepairHeader DROP COLUMN ReturnRepairID
GO
ALTER TABLE dbo.tbL_ReturnRepairHeader ADD ReturnRepairID Varchar(20) NULL
GO
UPDATE dbo.tbL_ReturnRepairHeader SET ReturnRepairID=tempID
GO
ALTER TABLE dbo.tbL_ReturnRepairHeader ALTER COLUMN ReturnRepairID varchar(20) NOT NULL
GO
ALTER TABLE dbo.tbL_ReturnRepairHeader DROP COLUMN tempID
GO
ALTER TABLE dbo.tbL_ReturnRepairHeader ADD CONSTRAINT PK_tbL_ReturnRepairHeader PRIMARY KEY CLUSTERED (ReturnRepairID) ON [PRIMARY]
GO
ALTER TABLE dbo.tbl_OrderReturnDetail Alter Column OrderReturnHeaderID varchar(20)
GO
ALTER TABLE dbo.tbl_ReturnRepairDetail Alter Column ReturnRepairID varchar(20)
GO
Alter Table tbl_orderreturnDetail Alter Column OrderReturnHeaderID varchar(20) 
GO
Alter Table tbl_ReturnRepairDetail Alter Column ReturnRepairID Varchar(20)
GO

