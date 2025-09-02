Update tbl_Version Set VersionNo='6.4900',VersionDate=GETDATE();
GO
/*** tbl_AppSetting ***/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsUseMember')
alter table tbl_GlobalSetting add  IsUseMember Bit NULL
GO
Update tbl_GlobalSetting set IsUseMember=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'IsMemberCustomer')
alter table tbl_GlobalSetting add  IsMemberCustomer Bit NULL
GO
Update tbl_GlobalSetting set IsMemberCustomer=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_GlobalSetting' and column_name = 'RegName')
alter table tbl_GlobalSetting add  RegName varchar(50) NULL
GO
Update tbl_GlobalSetting set RegName=''
GO
/*** tbl_Customer ***/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_Customer' and column_name = 'MemberCode')
alter table tbl_Customer add  MemberCode varchar(50) NULL
GO
Update tbl_Customer set MemberCode=''
GO
/*** tbl_SaleInvoice ***/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'MemberID')
alter table tbl_SaleInvoiceHeader add  MemberID varchar(50) NULL
GO
Update tbl_SaleInvoiceHeader set MemberID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'MemberName')
alter table tbl_SaleInvoiceHeader add  MemberName nvarchar(100) NULL
GO
Update tbl_SaleInvoiceHeader set MemberName=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'MemberCode')
alter table tbl_SaleInvoiceHeader add  MemberCode varchar(50) NULL
GO
Update tbl_SaleInvoiceHeader set MemberCode=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'RedeemID')
Alter table tbl_SaleInvoiceHeader add RedeemID nvarchar(100) NULL
GO
update tbl_SaleInvoiceHeader set RedeemID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'TopupPoint')
Alter table tbl_SaleInvoiceHeader add TopupPoint int null
GO
update tbl_SaleInvoiceHeader set TopupPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'TopupValue')
Alter table tbl_SaleInvoiceHeader add TopupValue int null
GO
update tbl_SaleInvoiceHeader set TopupValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'RedeemPoint')
Alter table tbl_SaleInvoiceHeader add RedeemPoint int null
GO
update tbl_SaleInvoiceHeader set RedeemPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'RedeemValue')
Alter table tbl_SaleInvoiceHeader add RedeemValue int null
GO
update tbl_SaleInvoiceHeader set RedeemValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'IsRedeemInvoice')
Alter table tbl_SaleInvoiceHeader add IsRedeemInvoice Bit null
GO
update tbl_SaleInvoiceHeader set IsRedeemInvoice=0
GO
--Member Discount---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'MemberDis')
Alter table tbl_SaleInvoiceHeader add MemberDis decimal(18, 2) null
GO
update tbl_SaleInvoiceHeader set MemberDis=0.0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'MemberDiscountAmt')
Alter table tbl_SaleInvoiceHeader add MemberDiscountAmt bigInt null
GO
update tbl_SaleInvoiceHeader set MemberDiscountAmt=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'TransactionID')
Alter table tbl_SaleInvoiceHeader add TransactionID nvarchar(100) NULL
GO
update tbl_SaleInvoiceHeader set TransactionID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SaleInvoiceHeader' and column_name = 'InvoiceStatus')
alter table tbl_SaleInvoiceHeader add  InvoiceStatus Int NUll 
GO
Update tbl_SaleInvoiceHeader Set InvoiceStatus=0;
GO
/*** tbl_SalesVolume ***/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'MemberID')
alter table tbl_SalesVolume add  MemberID varchar(50) NULL
GO
Update tbl_SalesVolume set MemberID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'MemberName')
alter table tbl_SalesVolume add  MemberName nvarchar(100) NULL
GO
Update tbl_SalesVolume set MemberName=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'MemberCode')
alter table tbl_SalesVolume add  MemberCode varchar(50) NULL
GO
Update tbl_SalesVolume set MemberCode=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'RedeemID')
Alter table tbl_SalesVolume add RedeemID nvarchar(100) NULL
GO
update tbl_SalesVolume set RedeemID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'TopupPoint')
Alter table tbl_SalesVolume add TopupPoint int null
GO
update tbl_SalesVolume set TopupPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'TopupValue')
Alter table tbl_SalesVolume add TopupValue int null
GO
update tbl_SalesVolume set TopupValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'RedeemPoint')
Alter table tbl_SalesVolume add RedeemPoint int null
GO
update tbl_SalesVolume set RedeemPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'RedeemValue')
Alter table tbl_SalesVolume add RedeemValue int null
GO
update tbl_SalesVolume set RedeemValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'IsRedeemInvoice')
Alter table tbl_SalesVolume add IsRedeemInvoice Bit null
GO
update tbl_SalesVolume set IsRedeemInvoice=0
GO
--Member Discount---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'MemberDis')
Alter table tbl_SalesVolume add MemberDis decimal(18, 2) null
GO
update tbl_SalesVolume set MemberDis=0.0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'MemberDiscountAmt')
Alter table tbl_SalesVolume add MemberDiscountAmt bigInt null
GO
update tbl_SalesVolume set MemberDiscountAmt=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'TransactionID')
Alter table tbl_SalesVolume add TransactionID nvarchar(100) NULL
GO
update tbl_SalesVolume set TransactionID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolume' and column_name = 'InvoiceStatus')
alter table tbl_SalesVolume add  InvoiceStatus Int NUll 
GO
Update tbl_SalesVolume Set InvoiceStatus=0;
GO
/*** tbl_SaleLooseDiamondHeader ***/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'MemberID')
alter table tbl_SalelooseDiamondHeader add  MemberID varchar(50) NULL
GO
Update tbl_SalelooseDiamondHeader set MemberID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'MemberName')
alter table tbl_SalelooseDiamondHeader add  MemberName nvarchar(100) NULL
GO
Update tbl_SalelooseDiamondHeader set MemberName=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'MemberCode')
alter table tbl_SalelooseDiamondHeader add  MemberCode varchar(50) NULL
GO
Update tbl_SalelooseDiamondHeader set MemberCode=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'RedeemID')
Alter table tbl_SalelooseDiamondHeader add RedeemID nvarchar(100) NULL
GO
update tbl_SalelooseDiamondHeader set RedeemID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'TopupPoint')
Alter table tbl_SalelooseDiamondHeader add TopupPoint int null
GO
update tbl_SalelooseDiamondHeader set TopupPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'TopupValue')
Alter table tbl_SalelooseDiamondHeader add TopupValue int null
GO
update tbl_SalelooseDiamondHeader set TopupValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'RedeemPoint')
Alter table tbl_SalelooseDiamondHeader add RedeemPoint int null
GO
update tbl_SalelooseDiamondHeader set RedeemPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'RedeemValue')
Alter table tbl_SalelooseDiamondHeader add RedeemValue int null
GO
update tbl_SalelooseDiamondHeader set RedeemValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'IsRedeemInvoice')
Alter table tbl_SalelooseDiamondHeader add IsRedeemInvoice Bit null
GO
update tbl_SalelooseDiamondHeader set IsRedeemInvoice=0
GO
--Member Discount---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'MemberDis')
Alter table tbl_SalelooseDiamondHeader add MemberDis decimal(18, 2) null
GO
update tbl_SalelooseDiamondHeader set MemberDis=0.0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'MemberDiscountAmt')
Alter table tbl_SalelooseDiamondHeader add MemberDiscountAmt bigInt null
GO
update tbl_SalelooseDiamondHeader set MemberDiscountAmt=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'TransactionID')
Alter table tbl_SalelooseDiamondHeader add TransactionID nvarchar(100) NULL
GO
update tbl_SalelooseDiamondHeader set TransactionID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalelooseDiamondHeader' and column_name = 'InvoiceStatus')
alter table tbl_SalelooseDiamondHeader add  InvoiceStatus Int NUll 
GO
Update tbl_SalelooseDiamondHeader Set InvoiceStatus=0;
GO
/*** tbl_WholesaleInvoice ***/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'MemberID')
alter table tbl_WholesaleInvoice add  MemberID varchar(50) NULL
GO
Update tbl_WholesaleInvoice set MemberID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'MemberName')
alter table tbl_WholesaleInvoice add  MemberName nvarchar(100) NULL
GO
Update tbl_WholesaleInvoice set MemberName=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'MemberCode')
alter table tbl_WholesaleInvoice add  MemberCode varchar(50) NULL
GO
Update tbl_WholesaleInvoice set MemberCode=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'RedeemID')
Alter table tbl_WholesaleInvoice add RedeemID nvarchar(100) NULL
GO
update tbl_WholesaleInvoice set RedeemID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'TopupPoint')
Alter table tbl_WholesaleInvoice add TopupPoint int null
GO
update tbl_WholesaleInvoice set TopupPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'TopupValue')
Alter table tbl_WholesaleInvoice add TopupValue int null
GO
update tbl_WholesaleInvoice set TopupValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'RedeemPoint')
Alter table tbl_WholesaleInvoice add RedeemPoint int null
GO
update tbl_WholesaleInvoice set RedeemPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'RedeemValue')
Alter table tbl_WholesaleInvoice add RedeemValue int null
GO
update tbl_WholesaleInvoice set RedeemValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'IsRedeemInvoice')
Alter table tbl_WholesaleInvoice add IsRedeemInvoice Bit null
GO
update tbl_WholesaleInvoice set IsRedeemInvoice=0
GO
--Member Discount---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'MemberDis')
Alter table tbl_WholesaleInvoice add MemberDis decimal(18, 2) null
GO
update tbl_WholesaleInvoice set MemberDis=0.0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'MemberDiscountAmt')
Alter table tbl_WholesaleInvoice add MemberDiscountAmt bigInt null
GO
update tbl_WholesaleInvoice set MemberDiscountAmt=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'TransactionID')
Alter table tbl_WholesaleInvoice add TransactionID nvarchar(100) NULL
GO
update tbl_WholesaleInvoice set TransactionID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoice' and column_name = 'InvoiceStatus')
alter table tbl_WholesaleInvoice add  InvoiceStatus Int NUll 
GO
Update tbl_WholesaleInvoice Set InvoiceStatus=0;
GO
/*** tbl_ConsignmentSale ***/
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'MemberID')
alter table tbl_ConsignmentSale add  MemberID varchar(50) NULL
GO
Update tbl_ConsignmentSale set MemberID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'MemberName')
alter table tbl_ConsignmentSale add  MemberName nvarchar(100) NULL
GO
Update tbl_ConsignmentSale set MemberName=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'MemberCode')
alter table tbl_ConsignmentSale add  MemberCode varchar(50) NULL
GO
Update tbl_ConsignmentSale set MemberCode=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'RedeemID')
Alter table tbl_ConsignmentSale add RedeemID nvarchar(100) NULL
GO
update tbl_ConsignmentSale set RedeemID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'TopupPoint')
Alter table tbl_ConsignmentSale add TopupPoint int null
GO
update tbl_ConsignmentSale set TopupPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'TopupValue')
Alter table tbl_ConsignmentSale add TopupValue int null
GO
update tbl_ConsignmentSale set TopupValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'RedeemPoint')
Alter table tbl_ConsignmentSale add RedeemPoint int null
GO
update tbl_ConsignmentSale set RedeemPoint=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'RedeemValue')
Alter table tbl_ConsignmentSale add RedeemValue int null
GO
update tbl_ConsignmentSale set RedeemValue=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'IsRedeemInvoice')
Alter table tbl_ConsignmentSale add IsRedeemInvoice Bit null
GO
update tbl_ConsignmentSale set IsRedeemInvoice=0
GO
--Member Discount---
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'MemberDis')
Alter table tbl_ConsignmentSale add MemberDis decimal(18, 2) null
GO
update tbl_ConsignmentSale set MemberDis=0.0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'MemberDiscountAmt')
Alter table tbl_ConsignmentSale add MemberDiscountAmt bigInt null
GO
update tbl_ConsignmentSale set MemberDiscountAmt=0
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'TransactionID')
Alter table tbl_ConsignmentSale add TransactionID nvarchar(100) NULL
GO
update tbl_ConsignmentSale set TransactionID=''
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_ConsignmentSale' and column_name = 'InvoiceStatus')
alter table tbl_ConsignmentSale add  InvoiceStatus Int NUll 
GO
Update tbl_ConsignmentSale Set InvoiceStatus=0;
GO